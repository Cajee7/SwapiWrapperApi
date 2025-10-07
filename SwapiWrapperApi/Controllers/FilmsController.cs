using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using SwapiWrapperApi.Models;
using SwapiWrapperApi.Services;
using System.Threading.Tasks;
using Swashbuckle.AspNetCore.Annotations;

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
        [SwaggerResponse(StatusCodes.Status200OK, "Success")]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, "Unauthorized")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Film not found")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Internal server error")]
        public async Task<ActionResult<FilmModel>> GetAllFilms()
        {
            try
            {
                var films = await _filmService.GetAllFilms();

                if (films == null)
                {
                    return NotFound("Film not found");
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
        [SwaggerResponse(StatusCodes.Status200OK, "Success")]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Film ID must be greater than 0")]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, "Unauthorized")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Character not found")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Internal server error")]
        public async Task<ActionResult<List<CharacterModel>>> GetCharactersForFilm(int filmId)
        {
            if(filmId <= 0)
            {
                return BadRequest("Film ID must be greater than 0");
            }

            try
            {
                var characters = await _filmService.GetCharactersForFilm(filmId);
                
                if (characters == null)
                {
                    return NotFound("No characters found");
                }
                return Ok(characters);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("starships/{filmId}")]
        [Authorize]
        [SwaggerResponse(StatusCodes.Status200OK, "Success")]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Film ID must be greater than 0")]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, "Unauthorized")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Starship not found")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Internal server error")]
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
                    return NotFound("No starships found");
                }

                return Ok(startships);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
