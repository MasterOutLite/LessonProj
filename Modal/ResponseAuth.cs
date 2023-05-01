namespace LessonProj.Modal
{
    public class ResponseAuth
    {
        public string Token { get; set; } = "";
        public string Uuid { get; set; } = "";
        public bool IsSuccess { get; set; } = false;

        public ResponseAuth (string token, bool isSuccess)
        {
            Token = token;
            IsSuccess = isSuccess;
        }

        public ResponseAuth ()
        {
        }
    }
}
