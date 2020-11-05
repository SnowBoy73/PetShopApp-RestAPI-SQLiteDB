using System;
using System.Collections.Generic;
using PetShop.Core.Entity;

namespace PetShop.Core.DomainService
{
    public interface IUserRepository<User>
    {
        // Create User
        // No id when enter, id when exit
        User CreateUser(User user);

        // Read User(s)
        User ReadById(int id);
        IEnumerable<User> ReadAllUsers();

        // Update User
        User UpdateUser(User userUpdate);

        // Delete User
        User DeleteUser(int id);
    }

}
