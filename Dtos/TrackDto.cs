namespace ExClockify.Dtos
{
    public class TrackDto
    {
        public string UserDeviceId {get; set;}         
        public string name {get; set;}
        public DateTime startTime { get; set; }
        public DateTime endTime {get; set;}
        public float duration {get; set;}
        public string description {get; set;}
    }
    
    public class TrackDtoWithId
    {
        public Guid Id {get; set;}
        public string UserDeviceId {get; set;}         
        public string name {get; set;}
        public DateTime startTime { get; set; }
        public DateTime endTime {get; set;}
        public float duration {get; set;}
        public string description {get; set;}
    }
}