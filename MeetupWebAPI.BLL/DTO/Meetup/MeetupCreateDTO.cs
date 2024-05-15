using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetupWebAPI.BLL.DTO.Meetup
{
    public class MeetupCreateDTO
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public string TagText { get; set; }

        public string Place { get; set; }

        public DateTime Date { get; set; }
    }
}
