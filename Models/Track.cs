namespace ExClockify.Models
{
    public class Track 
    {
        public Guid Id {get; set;} // primary key
        public string UserDeviceId {get; set;} // foreign key, also setup with fluentapi
        public User User {get; set;} // navigation
        
        public string Name {get; set;} = string.Empty;
        public DateTime StartTime { get; set; }
        public DateTime EndTime {get; set;}
        public float Duration {get; set;}
        public string Description {get; set;} = string.Empty;
    }
}
