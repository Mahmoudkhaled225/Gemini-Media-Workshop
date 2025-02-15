using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using DomainLayer.Entities;
using DomainLayer.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using PresentationLayer.Dtos.User;
using ServiceLayer.Concretes;
using ServiceLayer.Utils;

namespace PresentationLayer.Controllers;

public class UserController : BaseController
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly ILogger<UserController> _logger;
    private readonly Jwt _jwt;
    
    public UserController(IUnitOfWork unitOfWork, IMapper mapper, ILogger<UserController> logger, IOptions<Jwt> jwt)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _logger = logger;
        _jwt = jwt.Value;
    }    
    
    private string GenerateTokenString(GenerateAccessTokenDto dto)
    {
        var claims = new List<Claim>
        {
            new Claim("Id", dto.Id.ToString())
        };
        var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwt.Secret));
        var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);
        
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Issuer= _jwt.Issuer,
            Audience= _jwt.Audience,
            Subject = new ClaimsIdentity(claims), 
            Expires = DateTime.UtcNow.AddHours(_jwt.DurationInHours),
            SigningCredentials = signingCredentials
        };
        
        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }

    private LogInResponse LogInTokensLogic(User user)
    {
        GenerateAccessTokenDto tokenDto = new()
        {
            Id = user.Id, 
        };
        var accessToken = GenerateTokenString(tokenDto);
        
        return new LogInResponse
        {
            AccessToken = accessToken,
        };
    }

    private async Task<User?> ValidateUser(LoginUser user)
    {
        var checkUser = await _unitOfWork.UserRepository.GetUserByEmail(user.Email);
        if (checkUser is null)
        {
            _logger.LogInformation($" Register first User with email {user.Email} not found");
            return null;
        }
        
        if (Hashing.VerifyPassword(user.Password, checkUser.Password) is false)
        {
            _logger.LogInformation($"Invalid password for user with email {user.Email}");
            return null;
        }

        _logger.LogInformation($"User with email {user.Email} validated successfully");
        return checkUser;
    }

    
    [HttpGet("GetAll")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]

    public async Task<IActionResult> GetUsers()
    {
        var users = await _unitOfWork.UserRepository.GetAll();
        return Ok(users);
    }
    
    [HttpGet("Get/{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetUser(int id)
    {
        var user = await _unitOfWork.UserRepository.Get(id);
        if (user == null)
            return NotFound("User not found");
        return Ok(user);
    }
    
    [HttpPost("Add")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> AddUser([FromBody] RegisterUser user)
    {
        user.Password = Hashing.HashPass(user.Password);
        var userEntity = _mapper.Map<User>(user);
        await _unitOfWork.UserRepository.Add(userEntity);
        var flag = await _unitOfWork.Save();
        if (flag is 1)
        {
            var userDto = _mapper.Map<ReturnedUserDto>(userEntity);
            return Ok(userDto);
        }
         
        return BadRequest("Error in adding user");
    }
    
    //login
    [HttpPost("Login")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Login([FromBody] LoginUser user)
    {
        var checkUser = await ValidateUser(user);
        if (checkUser is null)
            return BadRequest("Invalid email or password");
        var tokens = LogInTokensLogic(checkUser);
        return Ok(tokens);
        
    }

        
    
}