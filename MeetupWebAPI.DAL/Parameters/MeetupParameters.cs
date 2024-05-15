using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetupWebAPI.DAL.Parameters
{
    public class MeetupParameters:QueryStringParameters
    {
        //Filtering
        public DateTime StartDate { get; set; } = DateTime.Now;

        public DateTime FinishDate { get; set; } = DateTime.Now;

        public string Name { get; set; } = "";

        public string Place { get; set; } = "";


    }
}
