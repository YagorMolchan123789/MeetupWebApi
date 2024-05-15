using MeetupWebAPI.DAL.Entities;

namespace MeetupWebAPI.WEB.Models.Responses
{
    public class AuthenticateResponse
    {
        public int Id { get; set; }

        public string FullName { get; set; }

        public string UserName { get; set; }

        public string Token { get; set; }

        public AuthenticateResponse(User user, string token)
        {
            Id = user.Id;
            FullName = user.FullName;
            UserName = user.UserName;
            Token = token;
        }
    }
}
