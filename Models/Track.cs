namespace ExClockify.Models
{
    public class Track
    {
        public Guid Id {get; set;}
        public string UserDeviceId {get; set;} // foreign key, setup with fluentapi
        
        public string name {get; set;}
        public DateTime startTime { get; set; }
        public DateTime endTime {get; set;}
        public float duration {get; set;}
        public string description {get; set;}
        
        public User user {get; set;}
    }
}
