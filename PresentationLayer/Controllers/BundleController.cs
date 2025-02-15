using AutoMapper;
using DomainLayer.Entities;
using DomainLayer.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PresentationLayer.Dtos.Bundle;
using RepositoryLayer.Specifications;
using ServiceLayer.Abstractions;

namespace PresentationLayer.Controllers;


[Authorize]
public class BundleController : BaseController
{
    
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IAuthService _authService;

    
    public BundleController(IUnitOfWork unitOfWork, IMapper mapper, IAuthService authService)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _authService = authService;
    }

    
    
    [HttpPost("Add")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> AddBundle([FromForm] AddBundleDto addBundleDto)
    {
        
        var token = Request.Headers.Authorization.ToString()[7..];
        
        var userId = _authService.GetUserIdFromToken(token);
        if (userId is null)
            return BadRequest("Invalid token");
        
        var user = await _unitOfWork.UserRepository.Get(userId);
        if (user is null)
            return NotFound("User not found");


        // Retrieve categories from the database
        var categories = await _unitOfWork.CategoryRepository
            .GetAll(c => addBundleDto.CategoriesId.Contains(c.Id));

        if (categories == null || !categories.Any())
        {
            return BadRequest("Invalid category IDs provided.");
        }
        
        var products = await _unitOfWork.ProductRepository
            .GetAll(p => addBundleDto.ProductsId.Contains(p.Id));
        
        if (products == null || !products.Any())
        {
            return BadRequest("Invalid product IDs provided.");
        }

        var bundle = _mapper.Map<Bundle>(addBundleDto);

        bundle.Categories = categories.ToList();
        
        bundle.Products = products.ToList();

        await _unitOfWork.BundleRepository.Add(bundle);
        var flag = await _unitOfWork.Save();
        if (flag is not 3 )
            return Created("Bundle has been added", bundle);
        return BadRequest("Error in adding bundle");
    }
    
    
    [HttpGet("GetAll")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAllBundles([FromQuery] int? pageNumber, [FromQuery] int? pageSize)
    {
        var token = Request.Headers.Authorization.ToString()[7..];
        
        var userId = _authService.GetUserIdFromToken(token);
        if (userId is null)
            return BadRequest("Invalid token");
        
        var user = await _unitOfWork.UserRepository.Get(userId);
        if (user is null)
            return NotFound("User not found");

        var bundles = await _unitOfWork.BundleRepository.GetAllWithSpec(new BundleSpec(pageNumber, pageSize));
        var bundlesDto = _mapper.Map<List<ReturnedBundleDto>>(bundles);
        return Ok(bundlesDto);
    }
    
    [HttpGet("Get/{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetBundle(int id)
    {
        var token = Request.Headers.Authorization.ToString()[7..];
        
        var userId = _authService.GetUserIdFromToken(token);
        if (userId is null)
            return BadRequest("Invalid token");
        
        var user = await _unitOfWork.UserRepository.Get(userId);
        if (user is null)
            return NotFound("User not found");

        var bundle = await _unitOfWork.BundleRepository.GetWithSpec(new BundleSpec(id));
        if (bundle == null)
            return NotFound("Bundle not found");
        var bundleDto = _mapper.Map<ReturnedBundleDto>(bundle);
        return Ok(bundleDto);
    }
    
    [HttpPut("Update/{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateBundle(int id, [FromForm] UpdateBundleDto updateBundleDto)
    {
        var token = Request.Headers.Authorization.ToString()[7..];
        
        var userId = _authService.GetUserIdFromToken(token);
        if (userId is null)
            return BadRequest("Invalid token");
        
        var user = await _unitOfWork.UserRepository.Get(userId);
        if (user is null)
            return NotFound("User not found");

        var bundle = await _unitOfWork.BundleRepository.Get(id);
        if (bundle == null)
            return NotFound("Bundle not found");

        var categories = await _unitOfWork.CategoryRepository
            .GetAll(c => updateBundleDto.CategoriesId.Contains(c.Id));

        if (categories == null || !categories.Any())
        {
            return BadRequest("Invalid category IDs provided.");
        }
        
        var products = await _unitOfWork.ProductRepository
            .GetAll(p => updateBundleDto.ProductsId.Contains(p.Id));
        
        if (products == null || !products.Any())
        {
            return BadRequest("Invalid product IDs provided.");
        }

        _mapper.Map(updateBundleDto, bundle);
        bundle.Categories = categories.ToList();
        bundle.Products = products.ToList();
        
        _unitOfWork.BundleRepository.Update(bundle);
        var flag = await _unitOfWork.Save();
        if (flag is 1)
            return Ok("Bundle has been updated");
        return BadRequest("Error in updating bundle");
    }
    
    [HttpDelete("Delete/{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteBundle(int id)
    {
        var token = Request.Headers.Authorization.ToString()[7..];
        
        var userId = _authService.GetUserIdFromToken(token);
        if (userId is null)
            return BadRequest("Invalid token");
        
        var user = await _unitOfWork.UserRepository.Get(userId);
        if (user is null)
            return NotFound("User not found");

        var bundle = await _unitOfWork.BundleRepository.Get(id);
        if (bundle == null)
            return NotFound("Bundle not found");
        _unitOfWork.BundleRepository.Delete(bundle);
        var flag = await _unitOfWork.Save();
        if (flag is 1)
            return Ok("Bundle has been deleted");
        return BadRequest("Error in deleting bundle");
    }

}