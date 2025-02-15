using AutoMapper;
using DomainLayer.Entities;
using DomainLayer.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PresentationLayer.Dtos.Category;
using RepositoryLayer.Specifications;
using ServiceLayer.Abstractions;

namespace PresentationLayer.Controllers;

[Authorize]
public class CategoryController : BaseController
{
    
    private readonly IMapper _mapper;
    private readonly ILogger _logger;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUploadImgService _uploadImgService;
    private readonly IAuthService _authService;

    
    public CategoryController(IMapper mapper, ILogger<CategoryController> logger, IUnitOfWork unitOfWork, IUploadImgService uploadImgService, IAuthService authService)
    {
        _mapper = mapper;
        _logger = logger;
        _unitOfWork = unitOfWork;
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
    public async Task<IActionResult> AddCategory([FromForm] AddCategoryDto addCategoryDto)
    {
        var token = Request.Headers.Authorization.ToString()[7..];
        
        var userId = _authService.GetUserIdFromToken(token);
        if (userId is null)
            return BadRequest("Invalid token");
        
        var user = await _unitOfWork.UserRepository.Get(userId);
        if (user is null)
            return NotFound("User not found");

        var (imgUrl, publicId) = await UploadUImg(addCategoryDto.Image, "Category");
        var category = _mapper.Map<Category>(addCategoryDto);
        category.ImgUrl = imgUrl;
        category.PublicId = publicId;
        await _unitOfWork.CategoryRepository.Add(category);
        var flag = await _unitOfWork.Save();
        if (flag is 1)
            return Created("Category has been added", category);
        _logger.LogError("Error in adding category");
        return BadRequest("Error in adding category");
    }    
    
    
    [HttpGet("GetAll")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetCategories([FromQuery] int? pageNumber, [FromQuery] int? pageSize)
    {
        
        var token = Request.Headers.Authorization.ToString()[7..];
        
        var userId = _authService.GetUserIdFromToken(token);
        if (userId is null)
            return BadRequest("Invalid token");
        
        var user = await _unitOfWork.UserRepository.Get(userId);
        if (user is null)
            return NotFound("User not found");

        var categories = await _unitOfWork.CategoryRepository.GetAllWithSpec(new CategorySpec(pageNumber, pageSize));
        var categoriesDto = _mapper.Map<List<ReturnedCategoryDto>>(categories);
        return Ok(categoriesDto);
    }
    
    [HttpGet("Get/{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetCategory(int id)
    {
        var token = Request.Headers.Authorization.ToString()[7..];
        
        var userId = _authService.GetUserIdFromToken(token);
        if (userId is null)
            return BadRequest("Invalid token");
        
        var user = await _unitOfWork.UserRepository.Get(userId);
        if (user is null)
            return NotFound("User not found");
        var category = await _unitOfWork.CategoryRepository.GetWithSpec(new CategorySpec(id));
        if (category == null)
            return NotFound("Category not found");
        var categoryDto = _mapper.Map<ReturnedCategoryDto>(category);
        return Ok(categoryDto);
    }

    [HttpPut("Update/{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateCategory(int id, [FromForm] UpdateCategoryDto updateCategoryDto)
    {
        var token = Request.Headers.Authorization.ToString()[7..];
        
        var userId = _authService.GetUserIdFromToken(token);
        if (userId is null)
            return BadRequest("Invalid token");
        
        var user = await _unitOfWork.UserRepository.Get(userId);
        if (user is null)
            return NotFound("User not found");

        var category = await _unitOfWork.CategoryRepository.Get(id);
        if (category == null)
            return NotFound("Category not found");

        if (updateCategoryDto.ParentCategoryId is not null)
        {
            var parentCategory = await _unitOfWork.CategoryRepository.Get(updateCategoryDto.ParentCategoryId.Value);
            if (parentCategory == null)
                return NotFound("Parent category not found");
            category.ParentCategoryId = updateCategoryDto.ParentCategoryId;
        }

        if (updateCategoryDto.Image is not null)
        {
            var (imgUrl, publicId) = await UploadUImg(updateCategoryDto.Image, "Category");
            category.ImgUrl = imgUrl;
            category.PublicId = publicId;
        }

        if (updateCategoryDto.Name is not null)
            category.Name = updateCategoryDto.Name;
        if (updateCategoryDto.Description is not null)
            category.Description = updateCategoryDto.Description;

        
        _unitOfWork.CategoryRepository.Update(category);
        var flag = await _unitOfWork.Save();
        if (flag is 1)
            return Ok("Category has been updated");
        _logger.LogError("Error in updating category");
        return BadRequest("Error in updating category");
    }
    
    [HttpDelete("Delete/{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteCategory(int id)
    {
        var token = Request.Headers.Authorization.ToString()[7..];
        
        var userId = _authService.GetUserIdFromToken(token);
        if (userId is null)
            return BadRequest("Invalid token");
        
        var user = await _unitOfWork.UserRepository.Get(userId);
        if (user is null)
            return NotFound("User not found");

        var category = await _unitOfWork.CategoryRepository.Get(id);
        if (category == null)
            return NotFound("Category not found");
        _unitOfWork.CategoryRepository.Delete(category);
        var flag = await _unitOfWork.Save();
        if (flag is 1)
            return Ok("Category has been deleted");
        _logger.LogError("Error in deleting category");
        return BadRequest("Error in deleting category");
    }
}