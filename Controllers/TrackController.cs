using Microsoft.AspNetCore.Mvc;
using ExClockify.Models;
using ExClockify.Dtos;
using ExClockify.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authorization;

namespace ExClockify.Controller
{
    
    [ApiController]
    [Route("api/[controller]")]
    public class TaskController : ControllerBase
    {
        private readonly ExClockifyContext _repository;
        private readonly DtoService _dtoService;
        private readonly IConfiguration _config;
        
        public TaskController(ExClockifyContext repository, DtoService dtoservice , IConfiguration config)
        {
            _repository = repository;
            _dtoService = dtoservice;
            _config = config;
        }
        
        // returning all tracks, based on deviceId
        [HttpGet("GetTasks")]
        [Authorize]
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
        
        [HttpPost("AddTask")]
        [Authorize]
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
        
        [HttpDelete("DeleteTask{id}")]
        [Authorize]
        public async Task<ActionResult> DeleteTrack(int id)
        {
            var t = await _repository.Tracks.FindAsync(id);
            if (t == null)
            {
                return NotFound("the track was not found");
            }
            
            _repository.Tracks.Remove(t);
            await _repository.SaveChangesAsync();
            
            // could also return TrackDtoResponse if needed, to show which track was deleted
            return Ok("the track was deleted");
        }
        
        // only updates if the id is found and wont add a new track
        // if the id was not found
        [HttpPut("UpdateTask{id}")]
        [Authorize]
        public async Task<ActionResult> UpdateTrack(TrackDto t, Guid id )
        {
            var track = await _repository.Tracks.FindAsync(id);
            if (track == null)
            {
                return NotFound("the track was not found");
            }
            
            // update the track
            _dtoService.UpdateTrackByTrackDto(t, track);
            await _repository.SaveChangesAsync();
            
            // return the updated track with its id, the id might be needed
            // for our front end, if its not needed then just return TrackDto 
            return Ok(_dtoService.MapTrackDtoToTrackDtoWithId(t , id));
        }
    }
}