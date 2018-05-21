using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetCore.Learning.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCore.Learning.Basic.Controllers
{
    [Route("api/[controller]")]
    public class TestController : Controller
    {
        private MyContext _myContext;

        public TestController(MyContext myContext)
        {
            _myContext = myContext;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok();
        }
    }
}