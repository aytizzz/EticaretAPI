using EticaretAPI.Application.Features.Commands.Product.CreateProducts;
using EticaretAPI.Application.Features.Commands.Product.RemoveProduct;
using EticaretAPI.Application.Features.Commands.Product.UpdateProduct;
using EticaretAPI.Application.Features.Commands.ProductImageFile.RemoveProductImage;
using EticaretAPI.Application.Features.Commands.ProductImageFile.UploadProductImage;
using EticaretAPI.Application.Features.Queries.Product.GetAllProducts;
using EticaretAPI.Application.Features.Queries.Product.GetByIdProduct;
using EticaretAPI.Application.Features.Queries.ProductImageFile.GetProductImages;
using EticaretAPI.Application.Repositories;
using EticaretAPI.Application.RequestParametres;
//using EticaretAPI.Application.Services;
using EticaretAPI.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EticaretAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes ="Admin")]
    public class ProductsController : ControllerBase
    {
        readonly IMediator _mediator;

        public ProductsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] GetAllProductQueryRequest getAllProductQueryRequest)
        {
            GetAllProductQueryResponse  response= await _mediator.Send(getAllProductQueryRequest);//response dondurur
            return Ok(response);
        }
        [HttpGet("{id }")]
        public async Task<IActionResult>Get([FromRoute]GetByIdProductQueryRequest getByIdProductQueryRequest)
        {
           GetByIdProductQueryResponse response= await _mediator.Send(getByIdProductQueryRequest);
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> Post(CreateProductCommandRequest createProductCommandRequest)
        {
           CreateProductCommandResponse  response =await _mediator.Send(createProductCommandRequest);
            return Created();
        }
        [HttpPut]
        public async Task<IActionResult> Put([FromBody]UpdateProductCommandRequest updateProductCommandRequest)
        {
           UpdateProductCommandResponse response= await _mediator.Send(updateProductCommandRequest);
            return Ok();// geriye birsey dondurmeye gerek yok dedi 
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] RemoveProductCommandRequest removeProductCommandRequest)
        {
            RemoveProductCommandResponse response = await _mediator.Send(removeProductCommandRequest);
            return Ok();
        }


        [HttpPost("[action]")]
        public async Task<IActionResult> Upload([FromQuery] UploadProductImageCommandRequest uploadProductImageCommandRequest)
        {
            uploadProductImageCommandRequest.Files = Request.Form.Files;
            UploadProductImageCommandResponse response = await _mediator.Send(uploadProductImageCommandRequest);
            return Ok();
        }

        [HttpDelete("[action]/{id}")]
        public async Task<IActionResult>Remove([FromRoute]RemoveProductImageCommandRequest  removeProductImageCommandRequest, [FromQuery] int imageId)

        {

            removeProductImageCommandRequest.ImageId = imageId;
            RemoveProductImageCommandResponse response = await _mediator.Send(removeProductImageCommandRequest);
            return Ok();
          
        }

        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> GetProductImages([FromRoute]GetProductImageQueryRequest productImageQueryRequest)
        {
            List<GetProductImageQueryResponse> response = await _mediator.Send(productImageQueryRequest);
            return Ok(response);
        }
    }
    }

