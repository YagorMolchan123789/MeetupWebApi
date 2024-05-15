using MeetupWebAPI.DAL.EFCore;
using MeetupWebAPI.DAL.Entities;
using MeetupWebAPI.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetupWebAPI.DAL.Repositories
{
    public class UserRepository : RepositoryBase<User>, IUserRepository
    {
        public UserRepository(MeetupDbContext context):base(context)
        {
            
        }

        public void CreateUser(User user)
        {
            Create(user);
        }

        public async Task<User> GetUserById(int id)
        {
            return await GetByConditionWithInclude(u => u.Meetups, u => u.Id == id).DefaultIfEmpty().FirstOrDefaultAsync();
        }

        public async Task<User> GetUserByName(string name)
        {
            return await GetByConditionWithInclude(u => u.Meetups, u => u.UserName==name).DefaultIfEmpty().FirstOrDefaultAsync();
        }

        
        public async Task<bool> HasUser(string userName)
        {
            return await HasEntity(u => u.UserName == userName);
        }

        public bool HasMeetup(User user, Meetup meetup)
        {
            return user.Meetups.Contains(meetup);
        }

        public void AttachMeetupToUser(User user, Meetup meetup)
        {
            user.Meetups.Add(meetup);
        }
    }
}
