using Chinook.Models;
using Chinook.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Chinook.Controllers
{
    public class PlayListsController : Controller
    {

        private readonly PlayListsService _playListsService;

        /// <summary>
        /// Construtor to initialize the service dependency.
        /// </summary>
        /// <param name="playListsService"></param>
        public PlayListsController(PlayListsService playListsService)
        {
            _playListsService = playListsService;
        }

        /// <summary>
        /// Common method to add tracks to playlist when a new playlist created, added to favourite list, removed from favourite list, added a track to existing playlist
        /// </summary>
        /// <param name="playListName"></param>
        /// <param name="userId"></param>
        /// <param name="playListId"></param>
        /// <param name="trackId"></param>
        /// <param name="addToFavouriteTracks"></param>
        /// <param name="isFavourite"></param>
        /// <returns></returns>
        [AllowAnonymous]
        public bool AddPlayList(string playListName, string userId, long playListId, long trackId, bool addToFavouriteTracks, bool isFavourite)
        {
            _playListsService.AddToPlayList(userId, playListId, trackId, addToFavouriteTracks, isFavourite, playListName);
            return true;
        }
    }
}
