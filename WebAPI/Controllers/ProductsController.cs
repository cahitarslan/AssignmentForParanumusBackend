using AutoMapper;
using Business.Abstract;
using Entities.Concrete;
using Entities.Dtos.Product;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly IMapper _mapper;

        public ProductsController(IProductService productService, IMapper mapper)
        {
            _productService = productService;
            _mapper = mapper;
        }


        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _productService.GetAllAsync();
            if (result.Success)
            {
                var getProductDtos = _mapper.Map<List<GetProductDto>>(result.Data);
                return Ok(new { success = result.Success, data = getProductDtos, message = result.Message });
            }
            return BadRequest(result);
        }


        [HttpGet("GetById")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _productService.GetByIdAsync(id);
            if (result.Success)
            {
                var getProductDto = _mapper.Map<GetProductDto>(result.Data);
                return Ok(new { success = result.Success, data = getProductDto, message = result.Message });
            }
            return BadRequest(result);
        }


        [HttpPost("Add")]
        public async Task<IActionResult> Add(AddProductDto addProductDto)
        {
            var product = _mapper.Map<Product>(addProductDto);
            var result = await _productService.AddAsync(product);
            if (result.Success)
            {
                return Created("", result);
            }
            return BadRequest(result);
        }


        [HttpPatch("Update")]
        public async Task<IActionResult> Update(UpdateProductDto updateProductDto)
        {
            var product = _mapper.Map<Product>(updateProductDto);
            var result = await _productService.UpdateAsync(product);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }


        [HttpDelete("Delete")]
        public async Task<IActionResult> Delete(int id)
        {
            var productResult = await _productService.GetByIdAsync(id);
            var product = productResult.Data;

            var result = await _productService.DeleteAsync(product);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}
