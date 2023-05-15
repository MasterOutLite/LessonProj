namespace LessonProj.Modal
{
    public class RequestAuth
    {
        public string Token { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }

        public RequestAuth (string token)
        {
            Token = token;
        }

        public RequestAuth (string login, string password)
        {
            Login = login;
            Password = password;
        }

        public RequestAuth (string token, string login, string password) : this(login, password)
        {
            Token = token;
        }
    }
}
