using AutoMapper;
using Newtonsoft.Json;
using MeetupWebAPI.BLL.DTO.Meetup;
using MeetupWebAPI.DAL.Entities;
using MeetupWebAPI.DAL.Interfaces;
using MeetupWebAPI.DAL.Parameters;
using MeetupWebAPI.DAL.Enumerations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MeetupWebAPI.DAL.Helpers;

namespace MeetupWebAPI.WEB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MeetupsController : ControllerBase
    {
        private readonly IMeetupRepository _meetupRepo;
        //private readonly IUrlHelper _urlHelper;
        private readonly IMapper _mapper;

        public MeetupsController(IMeetupRepository meetupRepo, IMapper mapper)
        {
            _meetupRepo = meetupRepo;
            _mapper = mapper;
        }

        [HttpGet(Name = "GetMeetups")]
        public async Task<ActionResult<PagedList<MeetupDTO>>> GetMeetups([FromQuery] MeetupParameters parameters)
        {
            PagedList<Meetup> meetups = null;

            meetups = await _meetupRepo.GetMeetups(parameters);

            var previousPageLink = meetups.HasPrevious ?
                CreateResourceUri(parameters, ResourceUriType.Previous) : null;

            var nextPageLink = meetups.HasNext ?
                CreateResourceUri(parameters, ResourceUriType.Next) : null;

            var paginationMetaData = new
            {
                TotalCount = meetups.TotalCount,
                PageSize = meetups.PageSize,
                CurrentPage = meetups.CurrentPage,
                TotalPages = meetups.TotalPages,
                PreviousPageLink = previousPageLink,
                NextPageLink = nextPageLink
            };

            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(paginationMetaData));
            var meetupDTOs = _mapper.Map<IEnumerable<MeetupDTO>>(meetups);
            return Ok(meetupDTOs);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<MeetupDTO>> GetMeetup(int id)
        {
            var meetup = await _meetupRepo.GetMeetupById(id);
            if (meetup == null)
            {
                return NotFound();
            }
            var meetupDTO = _mapper.Map<MeetupDTO>(meetup);
            return Ok(meetupDTO);
        }

        [HttpPost]
        public async Task<ActionResult> PostMeetup([FromBody] MeetupCreateDTO meetupDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var meetup = _mapper.Map<Meetup>(meetupDTO);
            _meetupRepo.CreateMeetup(meetup);
            await _meetupRepo.SaveAsync();
            return CreatedAtAction("GetMeetup", new { id = meetup.Id }, meetup);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<MeetupDTO>> PutMeetup(int id, [FromBody] MeetupUpdateDTO meetupDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (id != meetupDTO.Id)
            {
                return BadRequest();
            }

            var meetup = _mapper.Map<Meetup>(meetupDTO);
            _meetupRepo.UpdateMeetup(meetup);
            await _meetupRepo.SaveAsync();
            return Ok(_mapper.Map<MeetupDTO>(meetup));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteMeetup(int id)
        {
            var meetup = await _meetupRepo.GetMeetupById(id);
            if (meetup == null)
            {
                return NotFound();
            }
            _meetupRepo.DeleteMeetup(meetup);
            await _meetupRepo.SaveAsync();
            return NoContent();
        }

        private string CreateResourceUri(MeetupParameters parameters, ResourceUriType resourceUriType)
        {
            switch (resourceUriType)
            {
                case ResourceUriType.Previous:
                    return Url.RouteUrl("GetMeetups", new
                    {
                        PageNumber = parameters.PageNumber - 1,
                        PageSize = parameters.PageSize,
                        SortingType = parameters.SortingType
                    });
                case ResourceUriType.Next:
                    return Url.RouteUrl("GetMeetups", new
                    {
                        PageNumber = parameters.PageNumber + 1,
                        PageSize = parameters.PageSize,
                        SortingType = parameters.SortingType
                    });
                default:
                    return Url.RouteUrl("GetMeetups", new
                    {
                        PageNumber = parameters.PageNumber,
                        PageSize = parameters.PageSize,
                        SortingType = parameters.SortingType
                    });

            }
        }
    }
}
