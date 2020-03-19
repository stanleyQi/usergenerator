using Entities.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IUserRepository
    {
        // get the all users 
        Task<List<User>> GetUsersByCondition(int count=1,string searchNameKey="");

        // get user by id
        Task<User> GetUserById(long id);

        // check user exsitence by id
        bool IsExistingUser(long id);

        // update user
        int Update(User user);

        // delete user
        int Delete(long id);
    }
}
