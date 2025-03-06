namespace ExClockify.Dtos
{
    // in order to hide the user reference from the Track model 
    public class TrackDto
    {
        public string UserDeviceId {get; set;}         
        public string name {get; set;}
        public DateTime startTime { get; set; }
        public DateTime endTime {get; set;}
        public float duration {get; set;}
        public string description {get; set;}
    }
    
    // in order to show the front end the ID of TrackDto, it might be helpful
    // otherwise we can remove this and just keep using TrackDto in every endpoint
    // currently only used in the AddTrack and UpdateTrack endpoints
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