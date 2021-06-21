using AutoMapper;
using Hookah.Abstracts;
using Hookah.Areas.Administration.Models;
using Hookah.Constants;
using Hookah.Controllers;
using Hookah.Exceptions;
using Hookah.Models;
using Hookah.Resources;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Dynamic.Core;
namespace Hookah.Areas.Administration.Controllers
{
    [Area(AreaConstants.Admin)]
    [Authorize]
    public class AccountController : BaseController
    {
        public const string Name = "Account";
        private readonly UserManager<User> _userManager;
        //private readonly IEmailService _emailService;
        //private readonly CurrentUser _currentUser;
        //private readonly UserService _userService;
        private readonly SignInManager<User> _signInManager;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        // private readonly IAuthorizationService _authorizationService;
        public AccountController(UserManager<User> userManager,
                                    SignInManager<User> signInManager,
                                    IMapper mapper,
                                    IUnitOfWork unitOfWork
                                        //  IAuthorizationService authorizationService,
                                        //IEmailService emailService,
                                       // CurrentUser currentUser,
                                        //UserService userService
                                        )
        {
            _userManager = userManager;
            //_emailService = emailService;
            //_currentUser = currentUser;
           // _userService = userService;
            _signInManager = signInManager;
            this._mapper = mapper;
            this._unitOfWork = unitOfWork;
            //  _authorizationService = authorizationService;
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        [AllowAnonymous]
        public IActionResult NotAuthorize()
        {
            var model = new ErrorPageModel()
            {
                StatusCode = 403,
                Message = ExceptionMessages.UserAccessToThisPage
            };
            return View("Error", model);
        }

        [HttpGet]
        public async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction(nameof(SignIn));
        }
        
        [HttpGet]
        [AllowAnonymous]
        public IActionResult SignIn()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SignIn(SignInViewModel model, string returnUrl)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(model);
                }
                var user = await _userManager.FindByNameAsync(model.UserName);
                if (user == null)
                {
                    ModelState.AddModelError("ex", ExceptionMessages.UserNotFound);
                    return View(model);
                }
                if (user.IsBlocked)
                {
                    ModelState.AddModelError("ex", ExceptionMessages.UserIsBlocked);
                    return View(model);
                }
                var result = await _signInManager.PasswordSignInAsync(user, model.Password, model.RememberMe, false);
                if (result.Succeeded)
                    return RedirectToAction(nameof(PackageController.Index), PackageController.Name, new { area = AreaConstants.Admin });

                if (result.IsLockedOut)
                    ModelState.AddModelError("ex", ExceptionMessages.UserIsLockedOut);
                else if (result.IsNotAllowed)
                    ModelState.AddModelError("ex", ExceptionMessages.UserIsNotAllowed);
                else
                    ModelState.AddModelError("ex", ExceptionMessages.UserCredentialsIncorrect);
                return View(model);
            }
            catch (Exception e)
            {
                ModelState.AddModelError("ex", e.Message);
                return View(model);
            }
        }
        [HttpGet]
        public async Task<IActionResult> ChangePersonalInfo()
        {
            PersonalInfoViewModel model = new PersonalInfoViewModel();
            var user = await _userManager.GetUserAsync(User);
            if (user != null)
            {
                model.FirstName = user.FirstName;
                model.LastName = user.LastName;
                model.UserId = user.Id;
                model.PhoneNumber = user.PhoneNumber;
                model.Email = user.Email;
            }
            return View("PersonalInformation", model);
        }
        [HttpPost]
        public async Task<IActionResult> ChangePersonalInfo(PersonalInfoViewModel model)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(model.UserId.ToString());
                if (user == null)
                    return AjaxFailureResult(Result.Failure(ExceptionMessages.UserNotFound));

                var userUpdated = _mapper.Map(model, user);
                var result = await _userManager.UpdateAsync(userUpdated);
                if (result.Succeeded)
                {
                    return RedirectToAction(nameof(LogOut));
                }
                if (result.Errors.Any())
                {
                    var errorMessage = this.GetErrorMessageByErrorCode(result.Errors.FirstOrDefault().Code);
                    if (errorMessage.IsSucceed)
                    {
                        return AjaxFailureResult(Result.Failure(errorMessage.Data));
                    }
                }
                return AjaxFailureResult(Result.Failure(ExceptionMessages.UserNotUpdated));
            }
            catch (BaseException exc)
            {
                return AjaxFailureResult(Result.Failure(exc));
            }
            catch (Exception unknownExc)
            {
                var fatalExc = new FatalException(unknownExc);
                return AjaxFailureResult(Result.Failure(fatalExc));
            }
        }
        [HttpPost]
        [Authorize(Roles = RoleNames.SuperAdmin)]
        public async Task<IActionResult> DeleteUser(Guid userId)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            if (user == null)
            {
                return AjaxFailureResult(Result.Failure(ExceptionMessages.UserNotFound));
            }
            var result = await _userManager.DeleteAsync(user);
            if (result.Succeeded)
                return Json("Ok");
            return AjaxFailureResult(Result.Failure(result.Errors.FirstOrDefault()?.Description));
        }
        [HttpPost]
        [Authorize(Roles = RoleNames.SuperAdmin)]
        public async Task<IActionResult> ChangeStatusOfUser(Guid userId, bool isBlocked)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(userId.ToString());
                if (user == null)
                    return AjaxFailureResult(Result.Failure(ExceptionMessages.UserNotFound));

                user.IsBlocked = isBlocked;
                var result = await _userManager.UpdateAsync(user);
                if (result.Succeeded)
                    return Json("Ok");

                var errorMessageResult = GetErrorMessageByErrorCode(result.Errors.FirstOrDefault().Code);
                if (errorMessageResult.IsSucceed)
                    return AjaxFailureResult(Result.Failure(errorMessageResult.Data));
                return AjaxFailureResult(Result.Failure(result.Errors.FirstOrDefault().Description));
            }
            catch (BaseException exc)
            {
                return AjaxFailureResult(Result.Failure(exc));
            }
            catch (Exception unknownExc)
            {
                var fatalExc = new FatalException(unknownExc);
                return AjaxFailureResult(Result.Failure(fatalExc));
            }
            
        }
        [HttpGet]
        [Authorize(Roles = RoleNames.SuperAdmin)]
        public IActionResult AllUsers()
        {
            return View();
        }
        [HttpPost]
        public IActionResult AllUsersData(GridFilterModel options)
        {
            var userSearchQuery = _userManager.Users;
            return Filter(userSearchQuery,options);
        }
        [HttpGet]
        [Authorize(Roles = RoleNames.SuperAdmin)]
        public IActionResult AddNewUser()
        {
            var model = new SignUpViewModel();
            return View("NewUser",model);
        }

        [HttpPost]
        [Authorize(Roles = RoleNames.SuperAdmin)]
        public async Task<IActionResult> AddNewUser(SignUpViewModel model)
        {
            try
            {
                if (model.ConfirmPassword != model.Password)
                {
                    return AjaxFailureResult(Result.Failure(ExceptionMessages.PasswordAndConfirmPasswordNotSame));
                }

                var user = _mapper.Map<User>(model);

                using (var scope = _unitOfWork.CreateScoppedTransaction())
                {
                    var result = await _userManager.CreateAsync(user, model.Password);
                    if (result.Succeeded)
                    {

                        var roleResult = await _userManager.AddToRoleAsync(user, RoleNames.Admin);
                        if (!roleResult.Succeeded)
                            return AjaxFailureResult(Result.Failure($"{RoleNames.Admin} adlı rol əlavə oluna bilmədi"));

                        await _unitOfWork.CommitAsync();
                        return RedirectToAction(nameof(AllUsers));
                    }
                    if (result.Errors.Any())
                    {
                        var errorMessage = this.GetErrorMessageByErrorCode(result.Errors.FirstOrDefault().Code);
                        if (errorMessage.IsSucceed)
                        {
                            return AjaxFailureResult(Result.Failure(errorMessage.Data));
                        }
                    }
                    return AjaxFailureResult(Result.Failure(ExceptionMessages.UserNotCreated));

                }
            }
            catch (BaseException exc)
            {
                return AjaxFailureResult(Result.Failure(exc));
            }
            catch (Exception unknownExc)
            {
                var fatalExc = new FatalException(unknownExc);
                return AjaxFailureResult(Result.Failure(fatalExc));
            }
        }
        [HttpGet]
        public IActionResult UserProfile()
        {
            return View("UserProfileOverview");
        }

        [HttpGet]
        public IActionResult ChangePassword()
        {
            var model = new ChangePasswordViewModel();
            return View("ChangePassword", model);
        }

        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
                return AjaxFailureResult(Result.Failure(ExceptionMessages.UserNotFound));
            if (model.NewPasword != model.ConfirmPassword)
                return AjaxFailureResult(Result.Failure(ExceptionMessages.PasswordAndConfirmPasswordNotSame));

            var result = await _userManager.ChangePasswordAsync(user, model.CurrentPassword, model.NewPasword);
            if (!result.Succeeded)
            {
                StringBuilder errors = new StringBuilder();
                foreach (var error in result.Errors)
                {
                    errors.Append(error.Description + ", ");
                }
                return AjaxFailureResult(Result.Failure(errors.ToString().Trim().Substring(0, errors.Length - 2)));
            }
            return RedirectToAction(nameof(LogOut));
        }

        private Result<string> GetErrorMessageByErrorCode(string code)
        {
            try
            {
                return Result<String>.Succeed(ExceptionMessages.ResourceManager.GetString(code));
            }
            catch (BaseException exc)
            {
                return Result<string>.Failure(exc);
            }
            catch (Exception unknownExc)
            {
                var fatalExc = new FatalException(unknownExc);
                return Result<string>.Failure(fatalExc);
            }
        }
    }
}
