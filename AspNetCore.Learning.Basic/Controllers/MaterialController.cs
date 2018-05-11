using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetCore.Learning.Basic.Services;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCore.Learning.Basic.Controllers
{
    [Route("api/product")]
    public class MaterialController : Controller
    {
        [Route("{productId}/materials")]
        public IActionResult GetMaterials(int productId)
        {
            var product = ProductService.Current.Products.SingleOrDefault(x => x.Id == productId);
            if(product == null)
            {
                return NotFound();
            }
            return Ok(product.Materials);
        }

        [Route("{productId}/material/{materialId}")]
        public IActionResult GetMaterial(int productId, int materialId)
        {
            var product = ProductService.Current.Products.SingleOrDefault(x => x.Id == productId);
            if (product == null)
            {
                return NotFound();
            }
            var material = product.Materials.SingleOrDefault(x => x.Id == materialId);
            if(material == null)
            {
                return NotFound();
            }
            return Ok(material);
        }
    }
}