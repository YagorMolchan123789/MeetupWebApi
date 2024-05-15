using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MeetupWebAPI.BLL.DTO.User;
using MeetupWebAPI.DAL.Entities;

namespace MeetupWebAPI.BLL.Profiles
{
    public class UserProfile:Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserDTO>();
        }
    }
}
