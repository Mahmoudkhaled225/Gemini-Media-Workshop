using AutoMapper;
using DomainLayer.Entities;
using DomainLayer.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PresentationLayer.Dtos.Subcategory;
using RepositoryLayer.Specifications;
using ServiceLayer.Abstractions;

namespace PresentationLayer.Controllers;


[Authorize]
public class SubCategoryController : BaseController
{
    
    private readonly IMapper _mapper;
    private readonly ILogger _logger;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IAuthService _authService;
    
    public SubCategoryController(IMapper mapper, ILogger<SubCategoryController> logger, IUnitOfWork unitOfWork, IAuthService authService)
    {
        _mapper = mapper;
        _logger = logger;
        _unitOfWork = unitOfWork;
        _authService = authService;
    }
    
    
    [HttpPost("Add")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> AddSubCategory([FromBody] AddSubCategoryDto addSubCategoryDto)
    {
        var token = Request.Headers.Authorization.ToString()[7..];
        
        var userId = _authService.GetUserIdFromToken(token);
        if (userId is null)
            return BadRequest("Invalid token");
        
        var user = await _unitOfWork.UserRepository.Get(userId);
        if (user is null)
            return NotFound("User not found");

        var subCategory = _mapper.Map<SubCategory>(addSubCategoryDto);
        await _unitOfWork.SubcategoryRepository.Add(subCategory);
        var flag = await _unitOfWork.Save();
        if (flag is 1)
            return Created("SubCategory has been added", subCategory);
        _logger.LogError("Error in adding subcategory");
        return BadRequest("Error in adding subcategory");
    }    
    
    
    [HttpGet("GetAll")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetSubCategories([FromQuery] int? pageNumber, [FromQuery] int? pageSize)
    {
        var token = Request.Headers.Authorization.ToString()[7..];
        
        var userId = _authService.GetUserIdFromToken(token);
        if (userId is null)
            return BadRequest("Invalid token");
        
        var user = await _unitOfWork.UserRepository.Get(userId);
        if (user is null)
            return NotFound("User not found");

        var subCategories = await _unitOfWork.SubcategoryRepository.GetAllWithSpec(new SubCategorySpec(pageNumber, pageSize));
        var subCategoriesDto = _mapper.Map<List<ReturnedSubCategoryDto>>(subCategories);
        return Ok(subCategoriesDto);
    }
    
    [HttpGet("Get/{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]

    public async Task<IActionResult> GetSubCategory(int id)
    {
        var token = Request.Headers.Authorization.ToString()[7..];
        
        var userId = _authService.GetUserIdFromToken(token);
        if (userId is null)
            return BadRequest("Invalid token");
        
        var user = await _unitOfWork.UserRepository.Get(userId);
        if (user is null)
            return NotFound("User not found");

        var subCategory = await _unitOfWork.SubcategoryRepository.GetWithSpec(new SubCategorySpec(id));
        if (subCategory == null)
            return NotFound("SubCategory not found");
        var subCategoryDto = _mapper.Map<ReturnedSubCategoryDto>(subCategory);
        return Ok(subCategoryDto);
    }
    
    [HttpPut("Update/{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateSubCategory(int id, [FromBody] UpdateSubCategoryDto updateSubCategoryDto)
    {
        var token = Request.Headers.Authorization.ToString()[7..];
        
        var userId = _authService.GetUserIdFromToken(token);
        if (userId is null)
            return BadRequest("Invalid token");
        
        var user = await _unitOfWork.UserRepository.Get(userId);
        if (user is null)
            return NotFound("User not found");

        var subCategory = await _unitOfWork.SubcategoryRepository.Get(id);
        if (subCategory == null)
            return NotFound("SubCategory not found");

        if (updateSubCategoryDto.Name is not null)
            subCategory.Name = updateSubCategoryDto.Name;
        if (updateSubCategoryDto.CategoryId is not null)
            subCategory.CategoryId = updateSubCategoryDto.CategoryId.Value;
        
        _unitOfWork.SubcategoryRepository.Update(subCategory);
        var flag = await _unitOfWork.Save();
        if (flag is 1)
            return Ok("SubCategory has been updated");
        _logger.LogError("Error in updating subcategory");
        return BadRequest("Error in updating subcategory");
    }
    
    [HttpDelete("Delete/{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteSubCategory(int id)
    {
        var token = Request.Headers.Authorization.ToString()[7..];
        
        var userId = _authService.GetUserIdFromToken(token);
        if (userId is null)
            return BadRequest("Invalid token");
        
        var user = await _unitOfWork.UserRepository.Get(userId);
        if (user is null)
            return NotFound("User not found");

        var subCategory = await _unitOfWork.SubcategoryRepository.Get(id);
        if (subCategory == null)
            return NotFound("SubCategory not found");
        _unitOfWork.SubcategoryRepository.Delete(subCategory);
        var flag = await _unitOfWork.Save();
        if (flag is 1)
            return Ok("SubCategory has been deleted");
        _logger.LogError("Error in deleting subcategory");
        return BadRequest("Error in deleting subcategory");
    }
    
    
}