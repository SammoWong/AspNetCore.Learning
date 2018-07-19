using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCore.Learning.Basic.Filters
{
    public class MyResourceFilter : Attribute, IResourceFilter
    {
        private static readonly Dictionary<string, object> _cache = new Dictionary<string, object>();
        private string _cacheKey;

        public void OnResourceExecuting(ResourceExecutingContext context)
        {
            _cacheKey = context.HttpContext.Request.Path.ToString();
            if (_cache.ContainsKey(_cacheKey))
            {
                var cacheValue = _cache[_cacheKey] as DateTime?;
                if (cacheValue != null)
                {
                    context.Result = new JsonResult("MyResourceFilterAttribute设置:cacheValue=" + cacheValue);
                }
            }
        }

        public void OnResourceExecuted(ResourceExecutedContext context)
        {
            if (!string.IsNullOrEmpty(_cacheKey) && !_cache.ContainsKey(_cacheKey))
            {
                var result = context.Result as JsonResult;
                if (result != null)
                {
                    _cache.Add(_cacheKey, result.Value);
                }
            }
        }
    }
}
