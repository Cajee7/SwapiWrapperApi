using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using SwapiWrapperApi.Models;
using SwapiWrapperApi.Services;
using System.Threading.Tasks;

namespace SwapiWrapperApi.Controllers
{
    [ApiController]
    [EnableRateLimiting("fixed")]
    public class FilmController : ControllerBase
    {
        private readonly FilmService _filmService;

        public FilmController(FilmService filmService)
        {
            _filmService = filmService;
        }

        [HttpGet("films")]
        [Authorize]
        public async Task<ActionResult<FilmModel>> GetAllFilms()
        {
            try
            {
                var films = await _filmService.GetAllFilms();

                if (films == null)
                {
                    return Ok(new List<FilmModel>());
                }
                   
                return Ok(films);
            }
            catch(Exception ex)
            {
                return StatusCode(500, "An error occured while getting films: " + ex.Message);
            }
        }

        [HttpGet("people/{filmId}")]
        [Authorize]
        public async Task<ActionResult<List<CharacterModel>>> GetCharactersForFilm(int filmId)
        {
            if(filmId <= 0)
            {
                return BadRequest("Film ID must be greater Than 0");
            }

            try
            {
                var characters = await _filmService.GetCharactersForFilm(filmId);
                if (characters == null)
                {
                    return Ok(new List<CharacterModel>());
                }
                return Ok(characters);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error " + ex.Message);
            }
        }

        [HttpGet("starships/{filmId}")]
        [Authorize]
        public async Task<ActionResult<List<StarshipModel>>> GetStarshipsForFilm(int filmId)
        {
            if (filmId <= 0)
            {
                return BadRequest("Film ID must be greater than 0");
            }

            try
            {
                var startships = await _filmService.GetStarshipsForFilm(filmId);

                if(startships == null)
                {
                    return Ok(new List<StarshipModel>());
                }

                return Ok(startships);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error getting starships: " + ex.Message);
            }
        }

    }
}
