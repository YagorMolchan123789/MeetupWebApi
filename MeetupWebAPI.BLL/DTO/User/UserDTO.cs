using MeetupWebAPI.BLL.DTO.Meetup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetupWebAPI.BLL.DTO.User
{
    public class UserDTO
    {
        public int Id { get; set; }

        public string FullName { get; set; }

        public string UserName { get; set; }

        public List<MeetupDTO> Meetups { get; set; }
    }
}
