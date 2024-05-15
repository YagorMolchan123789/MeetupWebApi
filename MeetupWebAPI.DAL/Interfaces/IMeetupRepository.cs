using MeetupWebAPI.DAL.Entities;
using MeetupWebAPI.DAL.Helpers;
using MeetupWebAPI.DAL.Parameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetupWebAPI.DAL.Interfaces
{
    public interface IMeetupRepository:IRepositoryBase<Meetup>
    {
        Task<PagedList<Meetup>> GetMeetups(MeetupParameters parameters);

        Task<Meetup> GetMeetupById(int id);

        Task<bool> HasMeetup(int id);

        void CreateMeetup(Meetup meetup);

        //void AttachUserToMeetup(Meetup meetup, User user);

        void UpdateMeetup(Meetup meetup);

        void DeleteMeetup(Meetup meetup);

        Task SaveAsync();
    }
}
