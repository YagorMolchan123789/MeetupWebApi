using MeetupWebAPI.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetupWebAPI.DAL.Interfaces
{
    public interface IUserRepository:IRepositoryBase<User>
    {
        Task<User> GetUserById(int id);
        Task<User> GetUserByName(string name);
        Task<bool> HasUser(string userName);
        void AttachMeetupToUser(User user, Meetup meetup);
        bool HasMeetup(User user, Meetup meetup);
        void CreateUser(User user);
    }
}
