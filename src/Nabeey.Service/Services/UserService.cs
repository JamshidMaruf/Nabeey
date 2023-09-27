using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Nabeey.DataAccess.IRepositories;
using Nabeey.Domain.Configurations;
using Nabeey.Domain.Entities.Users;
using Nabeey.Domain.Enums;
using Nabeey.Service.DTOs.Users;
using Nabeey.Service.Exceptions;
using Nabeey.Service.Extensions;
using Nabeey.Service.Interfaces;
using Service.Helpers;

namespace Nabeey.Service.Services;

public class UserService : IUserService
{
	private readonly IMapper mapper;
	private readonly IRepository<User> userRepository;

	public UserService(IRepository<User> repository, IMapper mapper)
	{
		this.mapper = mapper;
		this.userRepository = repository;
	}

	public async ValueTask<UserResultDto> AddAsync(UserCreationDto dto)
	{
		User user = await this.userRepository.SelectAsync(x => x.Phone.Equals(dto.Phone));
		if (user is not null)
			throw new AlreadyExistException($"This phone is already exist");

		var mappedUser = this.mapper.Map<User>(dto);

		mappedUser.PasswordHash = PasswordHash.Encrypt(dto.Password);
		await this.userRepository.InsertAsync(mappedUser);
		await this.userRepository.SaveAsync();

		return this.mapper.Map<UserResultDto>(mappedUser);
	}

	public async ValueTask<UserResultDto> ModifyAsync(UserUpdateDto dto)
	{
		User existUser = await this.userRepository.SelectAsync(u => u.Id.Equals(dto.Id))
			?? throw new NotFoundException($"This user is not found with ID = {dto.Id}");

		this.mapper.Map(dto, existUser);

		existUser.PasswordHash = PasswordHash.Encrypt(dto.Password);
		this.userRepository.Update(existUser);
		await this.userRepository.SaveAsync();

		return this.mapper.Map<UserResultDto>(existUser);
	}

	public async ValueTask<bool> RemoveAsync(long id)
	{
		User existUser = await this.userRepository.SelectAsync(u => u.Id.Equals(id))
			?? throw new NotFoundException($"This user is not found with ID = {id}");

		this.userRepository.Delete(existUser);
		await this.userRepository.SaveAsync();

		return true;
	}

	public async ValueTask<IEnumerable<UserResultDto>> RetrieveAllAsync(PaginationParams @params, string search = null)
	{
		var users = await this.userRepository.SelectAll(includes: new[] { "Asset" })
			.ToPaginate(@params)
			.ToListAsync();

		if (search is not null)
			users = users.Where(user => user.FirstName.Contains(search, StringComparison.OrdinalIgnoreCase)).ToList();

		return this.mapper.Map<IEnumerable<UserResultDto>>(users);
	}

	public async ValueTask<IEnumerable<UserResultDto>> RetrieveAllAsync()
	{
		var users = await this.userRepository.SelectAll(includes: new[] { "Asset" }).ToListAsync();
		return this.mapper.Map<IEnumerable<UserResultDto>>(users);
	}

	public async ValueTask<UserResultDto> RetrieveByIdAsync(long id)
	{
		User existUser = await this.userRepository.SelectAsync(expression: u => u.Id.Equals(id), includes: new[] { "Asset" })
			?? throw new NotFoundException($"This user is not found with ID = {id}");

		return this.mapper.Map<UserResultDto>(existUser);
	}

	public async ValueTask<UserResultDto> UpgradeRoleAsync(long id, Role role)
	{
		User existUser = await this.userRepository.SelectAsync(expression: u => u.Id.Equals(id), includes: new[] { "Asset" })
			?? throw new NotFoundException($"This user is not found with ID = {id}");

		existUser.UserRole = role;
		await this.userRepository.SaveAsync();

		return this.mapper.Map<UserResultDto>(existUser);
	}
}