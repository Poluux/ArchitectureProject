﻿using DataAccessLayer;
using DataAccessLayer.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using WebAPI_ArchitectureProject.Extension;
using WebAPI_ArchitectureProject.Model;

namespace WebAPI_ArchitectureProject.Business
{
    public class BalanceService : IBalanceService
    {
        private readonly MS_SQLContext _sqlContext;

        public BalanceService(MS_SQLContext sqlContext)
        {
            _sqlContext = sqlContext;
        }

        public async Task<User> FetchBalanceByUsername(string username)
        {
            return await _sqlContext.Users
                                     .Where(u => u.Username == username)
                                     .Select(u => u)
                                     .FirstOrDefaultAsync()
                                     ?? throw new Exception("User not found");
        }

        public async Task<User> GetUsernameByCardId(int cardId)
        {
            var user = await _sqlContext.Users.FirstOrDefaultAsync(u => u.IdCard == cardId);
            if (user == null) throw new Exception("Card not linked to any user.");
            return user;
        }

    }
}
