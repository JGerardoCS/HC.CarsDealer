using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using HC.CarsDealer.Application.Abstractions;
using HC.CarsDealer.Application.Business;
using HC.CarsDealer.Application.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HC.CarsDealer.Presentation.WebApi.Controllers
{
    //[EnableCors("DevPolicy")]
    [Route("[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductBusiness _business;

        public ProductsController(IConfiguration config, IProductBusiness business)
        {
            string uri = config.GetSection("LocalDb").GetSection("Uri").Value;
            _business = business;
        }
        
        [HttpGet]
        public Response<IEnumerable<ProductModel>> Get()
        {
            var response = _business.GetAll();
            return response;
        }

        [HttpGet("{id}")]
        public Response<ProductModel> Get(int id)
        {
            var response = _business.Get(id);
            return response;
        }

        [HttpPost]
        public Response<ProductModel> Post([FromBody] ProductModel product)
        {
            var response = _business.Create(product);
            return response;
        }

        [HttpPut]
        public Response Put([FromBody] ProductModel product)
        {
            var response = _business.Update(product);
            return response;
        }

        [HttpDelete("{id}")]
        public Response Delete(int id)
        {
            var response = _business.Delete(id);
            return response;
        }
    }
}
