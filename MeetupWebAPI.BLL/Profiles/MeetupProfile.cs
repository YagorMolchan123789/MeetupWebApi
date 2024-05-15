using AutoMapper;
using MeetupWebAPI.BLL.DTO.Meetup;
using MeetupWebAPI.DAL.Entities;
using MeetupWebAPI.DAL.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetupWebAPI.BLL.Profiles
{
    public class MeetupProfile:Profile
    {
        public MeetupProfile()
        {
            CreateMap<Meetup, MeetupDTO>();
            CreateMap<MeetupCreateDTO, Meetup>();
            CreateMap<MeetupUpdateDTO, Meetup>();
        }
    }
}
