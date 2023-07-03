using Chinook.Models;
using ChinookDataAccessLayer;
using ChinookDataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace Chinook.Services
{
    /// <summary>
    /// Service for Artist related operations
    /// </summary>
    public class ArtistsService
    {
        private readonly ChinookContext _dbContext;
        private readonly ILogger _logger;

        /// <summary>
        /// Injecting the db context class using DI - Constructor injection
        /// </summary>
        /// <param name="dbContext"></param>
        public ArtistsService(ChinookContext dbContext, ILogger<ArtistsService> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        /// <summary>
        /// Get All artists
        /// </summary>
        /// <returns></returns>
        public async Task<List<ArtistData>> GetArtists(string searchTerm = "")
        {
            try
            {
                var users = _dbContext.Users.Include(a => a.UserPlaylists).ToList();
                var artists = searchTerm.IsNullOrEmpty() ? _dbContext.Artists.ToList() : _dbContext.Artists.Where(x => x.Name.Contains(searchTerm)).ToList();

                return artists;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return new List<ArtistData>();
            }

        }

        /// <summary>
        /// Get Albums of an artist based on artistId
        /// </summary>
        /// <param name="artistId"></param>
        /// <returns></returns>
        public List<AlbumData> GetAlbumsForArtist(int artistId)
        {
            try
            {
                return _dbContext.Albums.Where(a => a.ArtistId == artistId).ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return new List<AlbumData>();
            }
        }

    }
}
