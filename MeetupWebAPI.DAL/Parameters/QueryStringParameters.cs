using MeetupWebAPI.DAL.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetupWebAPI.DAL.Parameters
{
    public class QueryStringParameters
    {
        //Paging
        public int PageNumber { get; set; }

        public int PageSize { get; set; }

        //Sorting
        public SortingType SortingType { get; set; }
    }
}
