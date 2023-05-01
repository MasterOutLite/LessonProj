namespace LessonProj.Modal
{
    public class RequestAuth
    {
        public string token { get; set; }
        public string login { get; set; }
        public string password { get; set; }

        public RequestAuth (string token)
        {
            this.token = token;
        }

        public RequestAuth (string login, string password)
        {
            this.login = login;
            this.password = password;
        }

        public RequestAuth (string token, string login, string password) : this(login, password)
        {
            this.token = token;
        }
    }
}
