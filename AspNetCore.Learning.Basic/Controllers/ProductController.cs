using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetCore.Learning.Basic.Dtos;
using AspNetCore.Learning.Basic.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace AspNetCore.Learning.Basic.Controllers
{
    [Route("api/[controller]")]
    public class ProductController : Controller
    {
        [HttpGet]
        public IActionResult GetProducts()
        {
            return Ok(ProductService.Current.Products);
        }

        [HttpGet]
        [Route("{id}",Name = "GetProduct")]
        public IActionResult GetProduct(int id)
        {
            var product = ProductService.Current.Products.SingleOrDefault(p => p.Id == id);
            if(product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }

        [HttpPost]
        public IActionResult Post(ProductCreation product)
        {
            if(product == null)
            {
                return BadRequest();
            }

            if (product.Name == "产品")
            {
                ModelState.AddModelError("Name", "产品的名称不可以是'产品'二字");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var maxId = ProductService.Current.Products.Max(x => x.Id);
            var newProduct = new Product
            {
                Id = ++maxId,
                Name = product.Name,
                Price = product.Price,
                Description = product.Description
            };
            ProductService.Current.Products.Add(newProduct);
            return CreatedAtRoute("GetProduct", new { id = newProduct.Id }, newProduct);
        }

        //HttpPut：整体修改，更新所有属性；Patch：部分更新
        [HttpPut("{id}")]
        public IActionResult Put(int id, ProductModification product)
        {
            if (product == null)
            {
                return BadRequest();
            }

            if (product.Name == "产品")
            {
                ModelState.AddModelError("Name", "产品的名称不可以是'产品'二字");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var model = ProductService.Current.Products.SingleOrDefault(x => x.Id == id);
            if(model == null)
            {
                return NotFound();
            }
            model.Name = product.Name;
            model.Price = product.Price;
            model.Description = product.Description;
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var model = ProductService.Current.Products.SingleOrDefault(x => x.Id == id);
            if (model == null)
            {
                return NotFound();
            }
            ProductService.Current.Products.Remove(model);
            return NoContent();
        }
    }
}