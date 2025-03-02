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
                startTime = t.startTime,
                endTime = t.endTime,
                duration = t.duration,
                description = t.description,
                name = t.name
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
                startTime = t.startTime,
                endTime = t.endTime,
                duration = t.duration,
                description = t.description,
                UserDeviceId = t.UserDeviceId,
                name = t.name
            }).ToList();
        
            return TracksDto;
        }

        public void UpdateTrackByTrackDto(TrackDto Dto, Track t)
        {
            t.description = Dto.description;
            t.startTime = Dto.startTime;
            t.endTime = Dto.endTime;
            t.duration = Dto.duration;
            t.name = Dto.name;
        }
    
    }
}