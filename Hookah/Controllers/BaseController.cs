using Hookah.Abstracts;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Dynamic.Core;
namespace Hookah.Controllers
{
    public class BaseController : Controller
    {
        protected new ActionResult Json(object data)
        {
            const string contentType = "application/json";
            var contentEncoding = Encoding.UTF8;
            var config = new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
            };
            var json = JsonConvert.SerializeObject(data, config);
            return Content(json, contentType, contentEncoding);
        }
        
        protected ActionResult AjaxFailureResult(Result response, Action action = null)
        {
            action?.Invoke();
            Response.StatusCode = (int)System.Net.HttpStatusCode.BadRequest;

            //https://docs.microsoft.com/en-us/dotnet/api/system.web.httpresponse.statusdescription?redirectedfrom=MSDN&view=netframework-4.8#System_Web_HttpResponse_StatusDescription
            //512 simboldan artiq ola bilmez
            /*            var description = response.FailureResult.FirstOrDefault();
                        if (string.IsNullOrWhiteSpace(description)) description = string.Empty;
                        if (description.Length > 500) description = description.Substring(0, 500) + "...";
                        Response.StatusDescription = description;*/
            return Json(response.FailureResult);
        }

        protected IActionResult Filter<T>(IQueryable<T> source, GridFilterModel options)
        {
            var totalCount = source.Count();
            var entityType = typeof(T);
            
            StringBuilder orderExpression = new StringBuilder();
            for (int i = 0; i < options.Order.Count; i++)
            {
                if (options.Columns[options.Order[i].Column].Orderable)
                {
                    orderExpression.Append(options.Columns[options.Order[i].Column].Data + " " + options.Order[i].Dir);
                    if (i < options.Order.Count - 1)
                    {
                        orderExpression.Append(", ");
                    }
                }
            }

            StringBuilder searchExpression = new StringBuilder();
            bool lastIsOrOperator = true;
            bool lastIsAndOperator = true;
            string andoperator = " AND ";
            string orOperator = " OR ";
            if (options.Columns.Any(m => m.Searchable == true && !String.IsNullOrEmpty(m.Search.Value)))
            {
                lastIsOrOperator = false;
                for (int i = 0; i < options.Columns.Count; i++)
                {
                    if (options.Columns[i].Searchable && !String.IsNullOrEmpty(options.Columns[i].Search.Value))
                    {
                        var propType = entityType.GetProperty(options.Columns[i].Data);

                        if (propType.PropertyType == typeof(string))
                        {
                            searchExpression.Append(options.Columns[i].Data + ".Contains(\"" + options.Search.Value + "\")");
                            lastIsAndOperator = false;
                          
                        }

                        if (i < options.Columns.Count - 1 && !lastIsAndOperator)
                        {
                            searchExpression.Append(andoperator);
                            lastIsAndOperator = true;
                        }
                    }
                }
            }
            else if (!String.IsNullOrEmpty(options.Search.Value))
            {
                lastIsAndOperator = false;
                for (int i = 0; i < options.Columns.Count; i++)
                {
                    if (!String.IsNullOrEmpty(options.Search.Value) && options.Columns[i].Searchable)
                    {
                        var propType = entityType.GetProperty(options.Columns[i].Data);

                        if (!lastIsOrOperator)
                        {
                            searchExpression.Append(orOperator);
                            lastIsOrOperator = true;
                        }
                      
                        if (propType.PropertyType == typeof(string))
                        {
                            searchExpression.Append(options.Columns[i].Data + ".Contains(\"" + options.Search.Value + "\")");
                            lastIsOrOperator = false;
                        }

                        if (i < options.Columns.Count - 1 && !lastIsOrOperator)
                        {
                            searchExpression.Append(orOperator);
                            lastIsOrOperator = true;
                        }
                    }
                }
            }


            StringBuilder selectExpression = new StringBuilder();
            selectExpression.Append("new { ");
            for (int i = 0; i < options.Columns.Count; i++)
            {
                if (!String.IsNullOrEmpty(options.Columns[i].Data))
                {
                    selectExpression.Append(options.Columns[i].Data);
                    if (i < options.Columns.Count - 1)
                    {
                        selectExpression.Append(", ");
                    }
                }
            }
            selectExpression.Append(" }");

            var orderExpressionResult = orderExpression.ToString();
            var searchExpressionResult = searchExpression.ToString();
            var selectExpressionResult = selectExpression.ToString();


            if (lastIsOrOperator && !String.IsNullOrEmpty(searchExpressionResult))
            {
                var indexOfOr = searchExpressionResult.LastIndexOf(orOperator);
                searchExpressionResult = searchExpressionResult.Substring(0, indexOfOr);
            }
            else if (lastIsAndOperator && !String.IsNullOrEmpty(searchExpressionResult))
            {
                var indexOfAnd = searchExpressionResult.LastIndexOf(andoperator);
                searchExpressionResult = searchExpressionResult.Substring(0, indexOfAnd);
            }

            if (!String.IsNullOrEmpty(searchExpressionResult))
            {
                source = source.Where(searchExpressionResult);
            }
            if (!String.IsNullOrEmpty(orderExpressionResult))
            {
                source = source.OrderBy(orderExpressionResult);
            }
            var users = source.Skip(options.Start).Take(options.Length).Select(selectExpressionResult);

            var jsonData = new { recordsFiltered = totalCount, recordsTotal = totalCount, data = users };
            return Json(jsonData);
        }
    }
}
