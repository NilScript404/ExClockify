namespace ExClockify.Dtos
{
    public class TrackDto
    {
        public string UserDeviceId {get; set;}         
        public string name {get; set;}
        public int startTime { get; set; }
        public int endTime {get; set;}
        public float duration {get; set;}
        public string description {get; set;}
    }
    
    public class TrackDtoWithId
    {
        public int Id {get; set;}
        public string UserDeviceId {get; set;}         
        public string name {get; set;}
        public int startTime { get; set; }
        public int endTime {get; set;}
        public float duration {get; set;}
        public string description {get; set;}
    }
}