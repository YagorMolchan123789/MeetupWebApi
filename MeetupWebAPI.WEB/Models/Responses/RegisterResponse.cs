using MeetupWebAPI.DAL.Entities;

namespace MeetupWebAPI.WEB.Models.Responses
{
    public class RegisterResponse
    {
        public int Id { get; set; }

        public string FullName { get; set; }

        public string UserName { get; set; }

        public RegisterResponse(User user)
        {
            Id = user.Id;
            UserName = user.UserName;
            FullName = user.FullName;
        }
    }
}
