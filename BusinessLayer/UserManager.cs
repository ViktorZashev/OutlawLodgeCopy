using BusinessLayer;
using DataLayer;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class UserManager
    {
        private readonly UserContext userContext;

        public UserManager(UserContext userContext)
        {
            this.userContext = userContext;
        }

        public async Task CreateAsync(User user)
        {
            await userContext.CreateAsync(user);
        }

        public async Task<User> ReadAsync(Guid userId, bool useNavigationalProperties = false, bool isReadOnly = true)
        {
            return await userContext.ReadAsync(userId, useNavigationalProperties, isReadOnly);
        }

        public async Task<List<User>> ReadAllAsync(bool useNavigationalProperties = false, bool isReadOnly = true)
        {
            return await userContext.ReadAllAsync(useNavigationalProperties, isReadOnly);
        }

        public async Task UpdateAsync(User user, bool useNavigationalProperties = false)
        {
            await userContext.UpdateAsync(user, useNavigationalProperties);
        }

        public async Task DeleteAsync(Guid userId)
        {
            await userContext.DeleteAsync(userId);
        }
    }
}
