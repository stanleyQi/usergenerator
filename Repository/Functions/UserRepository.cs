using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Repository.DataContext;
using Entities.Entities;
using System.Linq;
using Contracts;
using System.Data.SqlClient;

namespace Repository.Functions
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _dbcontext;

        public UserRepository(AppDbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        public async Task<List<User>> GetUsersByCondition(int count=1,string searchNameKey="")
        {
            List<User> users = new List<User>();
            string sql = string.Format(
                @"select top {0} * from Users where LastName like '%{1}%' or FirstName like '%{1}%' order by id "
                //@"select * from Users where LastName like '%{1}%' or FirstName like '%{1}%' order by id limit {0} "
                , count, searchNameKey);
            users = await _dbcontext.Users.FromSql(sql).ToListAsync();

            return users;
        }

        public async Task<User> GetUserById(long id)
        {
            User user = await _dbcontext.Users.FindAsync(id);

            return user;
        }

        public int Update(User user)
        {
            _dbcontext.Users.Update(user);
            return _dbcontext.SaveChanges();
        }

        public int Delete(long id)
        {
            var user = new User { Id = id };
            _dbcontext.Users.Remove(user);
            return _dbcontext.SaveChanges();
        }

        public int Add(User user)
        {
            _dbcontext.Users.Add(user);
            return _dbcontext.SaveChanges();
        }

        public bool IsExistingUser(long id)
        {
            return _dbcontext.Users.Count<User>(u => u.Id == id)==0? false:true;
        }
    }
}
