using Chinook.ClientModels;
using Chinook.Services;
using ChinookDataAccessLayer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Chinook.Controllers
{
    public class ArtistController : Controller
    {
        private readonly ArtistsService _artistService;

        /// <summary>
        /// Construtor to initialize the service dependency.
        /// </summary>
        /// <param name="playListsService"></param>
        public ArtistController(ArtistsService artistService)
        {
            _artistService = artistService;
        }

        /// <summary>
        /// Search articst by artistName
        /// </summary>
        /// <param name="artistName"></param>
        /// <returns></returns>
        [AllowAnonymous]
        public ActionResult SearchArtist(string artistName)
        {
            var model = CreateSearchResultModel(_artistService.GetArtists(artistName).Result);
            return PartialView("_ArtistTable", model);
        }

        private ArtistSearchList CreateSearchResultModel(List<ArtistData> artists)
        {
            var model = new ArtistSearchList();
            model.Artists = artists;

            return model;
        }


    }
}
