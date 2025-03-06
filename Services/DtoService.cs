using ExClockify.Dtos;
using ExClockify.Models;

namespace ExClockify.Services
{
    public class DtoService
    {
        public Track MapTrackDtoToTrack(TrackDto t) 
        {
            return new Track 
            {
                UserDeviceId = t.UserDeviceId,
                StartTime = t.startTime,
                EndTime = t.endTime,
                Duration = t.duration,
                Description = t.description,
                Name = t.name
            };
        }
        
        public TrackDtoWithId MapTrackDtoToTrackDtoWithId(TrackDto t, Guid id)    
        {
            return new TrackDtoWithId {
                startTime = t.startTime,
                endTime = t.endTime,
                duration = t.duration,
                description = t.description,
                UserDeviceId = t.UserDeviceId,
                name = t.name,
                Id = id
            };
        }
        
        public List<TrackDto> MapTracksToTracksDto(List<Track> tracks)
        {
            var TracksDto = tracks.Select(t => new TrackDto
            {
                startTime = t.StartTime,
                endTime = t.EndTime,
                duration = t.Duration,
                description = t.Description,
                UserDeviceId = t.UserDeviceId,
                name = t.Name
            }).ToList();
        
            return TracksDto;
        }
        
        public void UpdateTrackByTrackDto(TrackDto Dto, Track t)
        {
            t.Description = Dto.description;
            t.StartTime = Dto.startTime;
            t.EndTime = Dto.endTime;
            t.Duration = Dto.duration;
            t.Name = Dto.name;
        }
    
    }
}