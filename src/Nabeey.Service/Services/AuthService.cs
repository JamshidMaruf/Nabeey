﻿using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Nabeey.DataAccess.IRepositories;
using Nabeey.Domain.Entities.Users;
using Nabeey.Domain.Enums;
using Nabeey.Service.DTOs.Users;
using Nabeey.Service.Exceptions;
using Nabeey.Service.Interfaces;
using Nabeey.WebApi.Models;
using Newtonsoft.Json.Linq;
using Service.Helpers;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Nabeey.Service.Services;

public class AuthService : IAuthService
{
	private readonly IConfiguration configuration;
	private readonly IMapper mapper;
	private readonly IRepository<User> userRepository;

    public AuthService(IConfiguration configuration, IRepository<User> repository, IMapper mapper)
    {
        this.configuration = configuration;
        this.userRepository = repository;
        this.mapper = mapper;
    }

    public async ValueTask<UserResponseDto> GenerateTokenAsync(string phone, string originalPassword)
	{
		var user = await this.userRepository.SelectAsync(u => u.Phone.Equals(phone))
			?? throw new NotFoundException("This user is not found");

		bool verifiedPassword = PasswordHash.Verify(user.PasswordHash, originalPassword);
		if (!verifiedPassword)
			throw new CustomException(400, "Phone or password is invalid");

		var tokenHandler = new JwtSecurityTokenHandler();
		var tokenKey = Encoding.UTF8.GetBytes(configuration["JWT:Key"]);
		var tokenDescriptor = new SecurityTokenDescriptor
		{
			Subject = new ClaimsIdentity(new Claim[]
			{
				 new Claim("Phone", user.Phone),
				 new Claim("Id", user.Id.ToString()),
				 new Claim(ClaimTypes.Role, user.UserRole.ToString())
			}),
			Expires = DateTime.UtcNow.AddHours(1),
			SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature)
		};
		var token = tokenHandler.CreateToken(tokenDescriptor);

		var mapped = this.mapper.Map<UserResponseDto>(user);
		mapped.Token = tokenHandler.WriteToken(token);

        return mapped;
	}
}
