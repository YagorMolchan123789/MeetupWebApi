namespace MeetupWebAPI.WEB.Models.Requests
{
    public class AuthenticateRequest
    {
        public string UserName { get; set; }

        public string Password { get; set; }
    }
}
