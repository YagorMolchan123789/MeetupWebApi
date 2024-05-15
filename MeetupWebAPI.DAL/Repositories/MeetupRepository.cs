using MeetupWebAPI.DAL.EFCore;
using MeetupWebAPI.DAL.Entities;
using MeetupWebAPI.DAL.Enumerations;
using MeetupWebAPI.DAL.Helpers;
using MeetupWebAPI.DAL.Interfaces;
using MeetupWebAPI.DAL.Parameters;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetupWebAPI.DAL.Repositories
{
    public class MeetupRepository : RepositoryBase<Meetup>, IMeetupRepository
    {
        public MeetupRepository(MeetupDbContext dbContext):base(dbContext)
        {
            
        }

        public async Task<PagedList<Meetup>> GetMeetups(MeetupParameters parameters)
        {
            var meetups = GetByConditionWithInclude(m => m.Users, m => m.Date >= parameters.StartDate && m.Date <= parameters.FinishDate);

            SearchByName(ref meetups, parameters.Name);

            SearchByPlace(ref meetups, parameters.Place);

            OrderByDate(ref meetups, parameters.SortingType);

            return await PagedList<Meetup>.ToPagedListAsync(meetups,
               parameters.PageNumber, parameters.PageSize);
        }

        private void SearchByName(ref IQueryable<Meetup> meetups, string name)
        {
            if (!string.IsNullOrEmpty(name))
            {
                meetups = meetups.Where(m => m.Name.Contains(name));
            }
        }

        private void SearchByPlace(ref IQueryable<Meetup> meetups, string place)
        {
            if (!string.IsNullOrEmpty(place))
            {
                meetups = meetups.Where(m => m.Place.Contains(place));
            }
        }

        private void OrderByDate(ref IQueryable<Meetup> query, SortingType sortingType)
        {
            switch (sortingType)
            {
                case SortingType.Ascending:
                    query = query.OrderBy(m => m.Date).AsQueryable();
                    break;
                case SortingType.Descending:
                    query = query.OrderByDescending(m => m.Date).AsQueryable();
                    break;
                case SortingType.None:
                    query = query.OrderBy(m => m.Id).AsQueryable();
                    break;
            }
        } 

        public async Task<Meetup> GetMeetupById(int id)
        {
            return await GetByConditionWithInclude(m => m.Users, m => m.Id == id).DefaultIfEmpty().FirstOrDefaultAsync();
        }

        public async Task<bool> HasMeetup(int id)
        {
            return await HasEntity(m => m.Id == id);
        }

        public void CreateMeetup(Meetup meetup)
        {
            Create(meetup);
        }

        public void UpdateMeetup(Meetup meetup)
        {
            Update(meetup);
        }

        public void DeleteMeetup(Meetup meetup)
        {
            Delete(meetup);
        }

        
    }
}
