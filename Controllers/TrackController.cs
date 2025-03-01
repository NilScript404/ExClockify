using Microsoft.AspNetCore.Mvc;
using ExClockify.Models;
using ExClockify.Dtos;
using ExClockify.Services;
using Microsoft.EntityFrameworkCore;

namespace ExClockify.Controller
{
    
    [ApiController]
    [Route("api/[controller]")]
    public class TrackController : ControllerBase
    {
        private readonly ExClockifyContext _repository;
        private readonly DtoService _dtoService;
        
        public TrackController(ExClockifyContext repository, DtoService dtoservice)
        {
            _repository = repository;
            _dtoService = dtoservice;
        }
        
        // returning all tracks, based on deviceId
        [HttpGet("GetTracks")]
        public async Task<ActionResult> GetTracksByDeviceId(string deviceId)
        {
            var user = await _repository.Users
                .Where(u => u.deviceId == deviceId)
                .Include(u => u.Tracks)
                .FirstOrDefaultAsync();
            
            if (user == null)
            {
                return BadRequest("User Doesn't Exist");
            }
            if (user.Tracks == null)
            {
                return Ok("User doesn't have any Tracks"); 
            }
            
            return Ok(_dtoService.MapTracksToTracksDto(user.Tracks));        
        }
        
        [HttpPost("AddTrack")]
        public async Task<ActionResult> CreateNewTrackByDeviceId(TrackDto track)
        {
            // dont need this check after adding jwt => [Authorize] (probably)
            if (await _repository.Users.FindAsync(track.UserDeviceId) == null)
            {
                return BadRequest("User Doesnt Exist");
            }
            
            Track t = _dtoService.MapTrackDtoToTrack(track); 
            _repository.Tracks.Add(t);
            await _repository.SaveChangesAsync();
            
            // returning TrackDto with Id  => had to do this to avoid the cycle 
            // between Track.user and user.Tracks (if i return the Track object
            // directly, then a json object cycle happens)
            return Ok(_dtoService.MapTrackDtoToTrackDtoWithId(track , t.Id));
        }
        
        [HttpDelete("DeleteTrack{id}")]
        public async Task<ActionResult> DeleteTrack(int id)
        {
            var t = await _repository.Tracks.FindAsync(id);
            if (t == null)
            {
                return NotFound("the track was not found");
            }
            
            _repository.Tracks.Remove(t);
            await _repository.SaveChangesAsync();
            
            // could return TrackDtoResponse if needed, to show which track was deleted
            return Ok("the track was deleted");
        }
        
        // only updates if the id is found and wont add a new track
        // if the id was not found
        [HttpPut("UpdateTrack{id}")]
        public async Task<ActionResult> UpdateTrack(TrackDto t, int id )
        {
            var track = await _repository.Tracks.FindAsync(id);
            if (track == null)
            {
                return NotFound("the track was not found");
            }
            
            // update the track
            _dtoService.UpdateTrackByTrackDto(t, track);
            await _repository.SaveChangesAsync();
            
            // return the updated track with its id 
            return Ok(_dtoService.MapTrackDtoToTrackDtoWithId(t , id));
        }
        
        // Todo - login 
        [HttpPost("AddUser")]
        public async Task<ActionResult> CreateNewUserByDeviceId(string deviceId)
        {
            User u = new User();
            u.deviceId = deviceId;
            
            _repository.Users.Add(u); 
            await _repository.SaveChangesAsync();
            
            return Ok();
        }
    }
}