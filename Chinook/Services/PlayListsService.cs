using Chinook.ClientModels;
using ChinookDataAccessLayer;
using ChinookDataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Linq;

namespace Chinook.Services
{
    /// <summary>
    /// Service for getting, setting Play lists
    /// </summary>
    public class PlayListsService
    {
        private readonly ChinookContext _dbContext;
        private readonly ILogger _logger;

        /// <summary>
        /// Injecting the DB context object using DI - Constructor injection
        /// </summary>
        /// <param name="dbContext"></param>
        public PlayListsService(ChinookContext dbContext, ILogger<PlayListsService> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        /// <summary>
        /// Gets all user playslists - Based on user id
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public List<UserPlaylistData> GetUserPlayLists(string userId)
        {
            var list = _dbContext.UserPlaylists.Where(x => x.UserId == userId)
                .Include(x => x.Playlist)
                .ThenInclude(x => x.Tracks)
                .OrderBy(x => x.PlaylistId)
                .ToList();

            list.Add(AddDefaultPlayList());

            return list.OrderBy(x => x.PlaylistId).ToList();
        }

        private UserPlaylistData AddDefaultPlayList()
        {
            var defaultPlayList = new UserPlaylistData();
            defaultPlayList.Playlist = new PlaylistData();
            defaultPlayList.PlaylistId = Constants.DefaultPlayListId;
            defaultPlayList.Playlist.Name = Constants.DefaultPlayListName;

            return defaultPlayList;
        }

        public Playlist GetUserPlayList(string currentUserId, long playListId)
        {
            return _dbContext.Playlists
            .Include(a => a.Tracks).ThenInclude(a => a.Album).ThenInclude(a => a.Artist)
            .Where(p => p.PlaylistId == playListId)
            .Select(p => new Playlist()
            {
                Name = p.Name,
                Tracks = p.Tracks.Select(t => new ClientModels.PlaylistTrack()
                {
                    AlbumTitle = t.Album.Title,
                    ArtistName = t.Album.Artist.Name,
                    TrackId = t.TrackId,
                    TrackName = t.Name,
                    IsFavorite = t.Playlists.Where(p => p.UserPlaylists.Any(up => up.UserId == currentUserId && up.Playlist.Name == Constants.MyFravouriteListName)).Any()
                }).ToList()
            })
            .FirstOrDefault();
        }

        /// <summary>
        /// Common method to add track to the play list. Used same one for Favourite and Other play lists since most of the part is common
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="playListId"></param>
        /// <param name="trackId"></param>
        /// <param name="addToFavouriteTracks"></param>
        /// <param name="isFavourite"></param>
        public void AddToPlayList(string userId, long playListId, long trackId, bool addToFavouriteTracks = false, bool isFavourite = false, string playListName = "")
        {
            if (playListId != Constants.MyFravouriteListId && !_dbContext.Playlists.Any(x => x.PlaylistId == playListId))
                playListId = _dbContext.Playlists.Select(x => x.PlaylistId).Max() + 1;

            var model = CreateUserPlayListModel(userId, playListId, trackId, addToFavouriteTracks, isFavourite, playListName);
            try
            {
                var existingUserPlayList = _dbContext.UserPlaylists.Local.SingleOrDefault(o => o.UserId == userId && o.Playlist.PlaylistId == playListId);
                if (existingUserPlayList != null)
                {
                    _dbContext.Entry(existingUserPlayList).State = EntityState.Detached;

                    var playList = _dbContext.Playlists.Local.SingleOrDefault(c => c.PlaylistId == playListId);
                    if (playList != null)
                    {
                        playList.Tracks = model.Playlist.Tracks;
                        model.Playlist = playList;
                    }
                    else
                        _dbContext.Attach(model.Playlist);

                    _dbContext.Update(model);
                    _dbContext.SaveChanges();
                }
                else
                {
                    _dbContext.Save.Add(model);
                    _dbContext.SaveChanges();
                }
                   
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
            }

        }

        /// <summary>
        /// Creating the play list model to save
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="playListId"></param>
        /// <param name="trackId"></param>
        /// <param name="addToFavouriteTracks"></param>
        /// <param name="isFavourite"></param>
        /// <returns></returns>
        private UserPlaylistData CreateUserPlayListModel(string userId, long listId, long trackId, bool addToFavouriteTracks = false, bool isFavourite = false, string playListName = "")
        {
            var model = new UserPlaylistData();
            if (trackId <= 0)
                return model;

            model.UserId = userId;
            model.Playlist = new PlaylistData();
            model.PlaylistId = listId;
            model.Playlist.PlaylistId = listId;
            if (!playListName.IsNullOrEmpty())
                model.Playlist.Name = playListName;
            var track = _dbContext.Tracks.FirstOrDefault(x => x.TrackId == trackId);
            var trackList = new List<TrackData>();

            var existingList = _dbContext.UserPlaylists.Include(x => x.Playlist).Include(y => y.Playlist.Tracks).FirstOrDefault(x => x.PlaylistId == listId);
            if (existingList != null)
            {
                trackList = existingList.Playlist.Tracks.ToList();
            }
            if (addToFavouriteTracks)
            {
                model.Playlist.Name = Constants.MyFravouriteListName;
                model.PlaylistId = Constants.MyFravouriteListId;
                if (isFavourite && existingList != null)
                {
                    existingList.Playlist.Tracks.Add(track);
                    trackList = existingList.Playlist.Tracks.ToList();
                }
                else if (!isFavourite && existingList != null)
                {
                    existingList.Playlist.Tracks.Remove(track);
                    trackList = existingList.Playlist.Tracks.ToList();
                }
                else
                {
                    trackList.Add(track);
                }
            }
            else
            {
                trackList.Add(track);

            }
            model.Playlist.Tracks = trackList;
            return model;
        }
    }
}
