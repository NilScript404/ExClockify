namespace ExClockify.Models
{
    public class User 
    {
        public string deviceId {get; set;} // primary key, set with fluentapi
        public List<Track> Tracks {get; set;} = new List<Track>();
    }
}