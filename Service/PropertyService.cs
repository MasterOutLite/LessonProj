namespace LessonProj.Service
{
    public class PropertyService
    {
#if DEBUG
        public string URL => "http://localhost:8080";
#elif ANDROID

        public string URL => "http://192.168.3.11:8080";
#endif
        public HttpClient HttpClient { get; private set; }

        public PropertyService ()
        {
            HttpClient = new HttpClient ();
        }
    }
}
