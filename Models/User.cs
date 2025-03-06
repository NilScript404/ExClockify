namespace ExClockify.Models
{
    // assuming that every user will be automaticly registered by a generated deviceId
    // when the app launches
    public class User 
    {
        public string deviceId {get; set;} // primary key, set with fluentapi
        public List<Track> Tracks {get; set;} = new List<Track>(); // navigation
    
        // might be neded later on 
        // public string Name {get; set;} = string.Empty 
        // public string LastName {get; set;} = string.Empty
        // public string Email {get; set;} = string.Empty
        // public int PhoneNumber {get; set;}
        // public bool IsEmailVerified {get; set;}
        // public bool IsPhoneVerified {get; set;} 
    }
}