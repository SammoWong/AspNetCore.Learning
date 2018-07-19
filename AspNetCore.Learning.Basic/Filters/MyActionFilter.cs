using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCore.Learning.Basic.Filters
{
    public class MyActionFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            context.Result = new JsonResult("MyActionFilter设置:"+context.ModelState.AllModelStateErrors().FirstOrDefault().Message);
        }
    }

    public static class ModelStateExtensions
    {
        public static IEnumerable<ModelStateError> AllModelStateErrors(this ModelStateDictionary modelState)
        {
            var results = new List<ModelStateError>();
            //找到出错字段以及出错信息
            var allErrors = modelState.Where(m => m.Value.Errors.Any()).Select(m => new { m.Key, m.Value.Errors });
            foreach (var error in from item in allErrors
                                  let key = item.Key
                                  select item.Errors.Select(e => new ModelStateError(key, e.ErrorMessage)))
            {
                results.AddRange(error);
            }

            return results;
        }
    }

    public class ModelStateError
    {
        public ModelStateError(string key, string message)
        {
            Key = key;
            Message = message;
        }

        public string Key { get; set; }

        public string Message { get; set; }
    }
}