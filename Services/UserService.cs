using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using P2PLendingAPI.DTOs;
using P2PLendingAPI.Models;
using P2PLendingAPI.Repositories.Interfaces;
using P2PLendingAPI.Services.Interfaces;

namespace P2PLendingAPI.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserService(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<UserDto> GetByIdAsync(string id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            return _mapper.Map<UserDto>(user);
        }

        public async Task<UserDto> GetByEmailAsync(string email)
        {
            var user = await _userRepository.GetByEmailAsync(email);
            return _mapper.Map<UserDto>(user);
        }

        public async Task<IEnumerable<UserDto>> GetAllAsync()
        {
            var users = await _userRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<UserDto>>(users);
        }

        public async Task<UserDto> CreateAsync(CreateUserDto createUserDto)
        {
            var user = _mapper.Map<User>(createUserDto);
            user.Id = Guid.NewGuid().ToString();
            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(createUserDto.Password);
            user.Balance = 0;
            user.CreatedAt = DateTime.UtcNow;
            user.UpdatedAt = DateTime.UtcNow;

            var createdUser = await _userRepository.CreateAsync(user);
            return _mapper.Map<UserDto>(createdUser);
        }

        public async Task UpdateAsync(string id, UpdateUserDto updateUserDto)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user == null)
            {
                throw new KeyNotFoundException("User not found");
            }

            _mapper.Map(updateUserDto, user);
            user.UpdatedAt = DateTime.UtcNow;

            await _userRepository.UpdateAsync(user);
        }

        public async Task DeleteAsync(string id)
        {
            await _userRepository.DeleteAsync(id);
        }

        public async Task UpdateBalanceAsync(string id, decimal amount)
        {
            var user = await _userRepository.GetByIdAsync(id) ?? throw new KeyNotFoundException("User not found");
            if (user == null)
                throw new KeyNotFoundException("User not found");

            user.Balance += amount;
            user.UpdatedAt = DateTime.UtcNow;

            await _userRepository.UpdateAsync(user);
        }

        // GetByEmailAndRoleAsync
        public async Task<UserDto> GetByEmailAndRoleAsync(string email, string role)
        {
            var user = await _userRepository.GetByEmailAndRoleAsync(email, role);
            return _mapper.Map<UserDto>(user);
        }
    }
}