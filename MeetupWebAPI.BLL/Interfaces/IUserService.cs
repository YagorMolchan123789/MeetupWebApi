using MeetupWebAPI.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetupWebAPI.BLL.Interfaces
{
    public interface IUserService
    {
        string GenerateJwtToken(User user);
    }
}
