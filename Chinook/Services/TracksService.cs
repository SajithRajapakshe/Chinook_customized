using Chinook.ClientModels;
using Chinook.Models;
using ChinookDataAccessLayer;
using ChinookDataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace Chinook.Services
{
    /// <summary>
    /// Service for getting, setting tracks
    /// </summary>
    public class TracksService
    {
        private readonly ChinookContext _dbContext;
        private readonly ILogger _logger;

        /// <summary>
        /// Injecting the db context using DI - Constructor injection
        /// </summary>
        /// <param name="dbContext"></param>
        public TracksService(ChinookContext dbContext, ILogger<TracksService> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        /// <summary>
        /// Get tracks based on Artist id and current user id
        /// </summary>
        /// <param name="ArtistId"></param>
        /// <param name="CurrentUserId"></param>
        /// <returns></returns>
        public List<PlaylistTrack> GetTracks(long ArtistId, string CurrentUserId)
        {
            try
            {
                var tracks = _dbContext.Tracks;
                return tracks.Where(a => a.Album.ArtistId == ArtistId)
                .Include(a => a.Album)
                .Select(t => new PlaylistTrack()
                {
                    AlbumTitle = (t.Album == null ? "-" : t.Album.Title),
                    TrackId = t.TrackId,
                    TrackName = t.Name,
                    IsFavorite = t.Playlists.Where(p => p.UserPlaylists.Any(up => up.UserId == CurrentUserId && up.Playlist.Name == Constants.MyFravouriteListName)).Any()
                })
                .ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return new List<PlaylistTrack>();
            }
           
        }



    }
}
