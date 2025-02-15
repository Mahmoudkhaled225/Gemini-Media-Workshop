using AutoMapper;
using DomainLayer.Entities;
using DomainLayer.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using PresentationLayer.Dtos.Product;
using ServiceLayer.Abstractions;

namespace PresentationLayer.Controllers;


[Authorize]
public class ProductController : BaseController
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IUploadImgService _uploadImgService;
    private readonly IAuthService _authService;
    
    
    public ProductController(IUnitOfWork unitOfWork, IMapper mapper, IUploadImgService uploadImgService, IAuthService authService)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _uploadImgService = uploadImgService;
        _authService = authService;
    }
    
    
    private async Task<(string? imgUrl, string? publicId)> UploadUImg(IFormFile? image, string? folderName)
    {
        if (image == null)
            return (null, null);
        var uploadResult = await _uploadImgService.UploadImg(image, folderName);
        return uploadResult != null ? (uploadResult.Url.ToString(), uploadResult.PublicId) : (null, null);
    }

    
    [HttpPost("Add")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> AddProduct([FromForm] AddProductDto addProductDto)
    {
        var token = Request.Headers.Authorization.ToString()[7..];
        
        var userId = _authService.GetUserIdFromToken(token);
        if (userId is null)
            return BadRequest("Invalid token");
        
        var user = await _unitOfWork.UserRepository.Get(userId);
        if (user is null)
            return NotFound("User not found");

        var (imgUrl, publicId) = await UploadUImg(addProductDto.Image, "Product");

        // Retrieve categories from the database
        var categories = await _unitOfWork.CategoryRepository
            .GetAll(c => addProductDto.CategoriesId.Contains(c.Id));

        if (categories == null || !categories.Any())
        {
            return BadRequest("Invalid category IDs provided.");
        }

        // Map AddProductDto to Product entity
        var product = _mapper.Map<Product>(addProductDto);
        product.ImgUrl = imgUrl;
        product.PublicId = publicId;

        // Assign categories to the product
        product.Categories = categories.ToList();

        // Add product to the repository
        await _unitOfWork.ProductRepository.Add(product);
        // await _unitOfWork.ProductRepository.AddProductAsync(product);

        var flag = await _unitOfWork.Save();
        

        if (flag is not 0)
        {
            var productDto = _mapper.Map<ReturnedProductDto>(product);
            return Created("Product has been added", productDto);
        }

        return BadRequest("Error in adding product");
    }
    
    
    [HttpGet("GetAll")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAllProducts()
    {
        var token = Request.Headers.Authorization.ToString()[7..];
        
        var userId = _authService.GetUserIdFromToken(token);
        if (userId is null)
            return BadRequest("Invalid token");
        
        var user = await _unitOfWork.UserRepository.Get(userId);
        if (user is null)
            return NotFound("User not found");

        var products = await _unitOfWork.ProductRepository.GetAll();
        var productsDto = _mapper.Map<IEnumerable<ReturnedProductDto>>(products);
        return Ok(productsDto);
    }
    
    
    [HttpGet("GetById/{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetProductById(int id)
    {
        var token = Request.Headers.Authorization.ToString()[7..];
        
        var userId = _authService.GetUserIdFromToken(token);
        if (userId is null)
            return BadRequest("Invalid token");
        
        var user = await _unitOfWork.UserRepository.Get(userId);
        if (user is null)
            return NotFound("User not found");

        var product = await _unitOfWork.ProductRepository.Get(id);
        if (product == null)
        {
            return NotFound("Product not found");
        }

        var productDto = _mapper.Map<ReturnedProductDto>(product);
        return Ok(productDto);
    }
    
    
    [HttpPut("Update/{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateProduct(int id, [FromForm] UpdateProductDto updateProductDto)
    {
        var token = Request.Headers.Authorization.ToString()[7..];
        
        var userId = _authService.GetUserIdFromToken(token);
        if (userId is null)
            return BadRequest("Invalid token");
        
        var user = await _unitOfWork.UserRepository.Get(userId);
        if (user is null)
            return NotFound("User not found");

        var product = await _unitOfWork.ProductRepository.Get(id);
        if (product == null)
        {
            return NotFound("Product not found");
        }

        var (imgUrl, publicId) = await UploadUImg(updateProductDto.Image, "Product");

        // Retrieve categories from the database
        var categories = await _unitOfWork.CategoryRepository
            .GetAll(c => updateProductDto.CategoriesId.Contains(c.Id));

        if (categories == null || !categories.Any())
        {
            return BadRequest("Invalid category IDs provided.");
        }

        // Map UpdateProductDto to Product entity
        product = _mapper.Map(updateProductDto, product);
        product.ImgUrl = imgUrl;
        product.PublicId = publicId;

        // Assign categories to the product
        product.Categories = categories.ToList();

        // Update product in the repository
        _unitOfWork.ProductRepository.Update(product);
        // _unitOfWork.ProductRepository.UpdateProduct(product);

        var flag = await _unitOfWork.Save();
        
        if (flag is not 0)
        {
            var productDto = _mapper.Map<ReturnedProductDto>(product);
            return Ok(productDto);
        }

        return BadRequest("Error in updating product");
    }
    
    
    [HttpDelete("Delete/{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteProduct(int id)
    {
        var token = Request.Headers.Authorization.ToString()[7..];
        
        var userId = _authService.GetUserIdFromToken(token);
        if (userId is null)
            return BadRequest("Invalid token");
        
        var user = await _unitOfWork.UserRepository.Get(userId);
        if (user is null)
            return NotFound("User not found");

        var product = await _unitOfWork.ProductRepository.Get(id);
        if (product == null)
        {
            return NotFound("Product not found");
        }

        _unitOfWork.ProductRepository.Delete(product);
        // _unitOfWork.ProductRepository.DeleteProduct(product);

        var flag = await _unitOfWork.Save();
        
        if (flag is not 0)
        {
            return Ok("Product has been deleted");
        }

        return BadRequest("Error in deleting product");
    }
    
    


}