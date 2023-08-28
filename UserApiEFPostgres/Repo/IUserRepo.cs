using System;
using Microsoft.EntityFrameworkCore;
using UserApiEFPostgres.Data;
using UserApiEFPostgres.Models;

namespace UserApiEFPostgres.Repo
{
	public interface IUserRepo
	{
		Task<UserModel> GetUserByIdAsync(int id);
		Task AddUserAsync(UserModel user);
		Task DeleteUserAsync(int id);
        Task UpdateUserByIdAsync(int id, UserModel updatedUser);
    }
	public class UserRepo : IUserRepo

	{
		private readonly AppDbContext _dbContext;

		public UserRepo(AppDbContext dbContext)
		{
			_dbContext = dbContext;
		}

		public async Task<UserModel> GetUserByIdAsync(int id)
		{
			return await _dbContext.Users.FindAsync(id);
		}
		public async Task AddUserAsync(UserModel user)
		{
			await _dbContext.Users.AddAsync(user);
			await _dbContext.SaveChangesAsync();
		}
		public async Task DeleteUserAsync(int id)
		{
			var user = await _dbContext.Users.FindAsync(id);
			if(user != null)
			{
				_dbContext.Users.Remove(user);
				await _dbContext.SaveChangesAsync();
			}
		}
		public async Task UpdateUserByIdAsync(int id, UserModel updatedUser)
		{
			var userToUpdate = await _dbContext.Users.FindAsync(id);
			if(userToUpdate != null)
			{
				userToUpdate.Name = updatedUser.Name;
				userToUpdate.Email = updatedUser.Email;

				_dbContext.Entry(userToUpdate).State = EntityState.Modified;
				await _dbContext.SaveChangesAsync();
			}
		}

	}
			
}

