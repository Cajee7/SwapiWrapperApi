using SwapiWrapperApi.Models;
using RestSharp;
using Microsoft.Extensions.Caching.Memory;
using System.Net;

namespace SwapiWrapperApi.Services
{
    public class FilmService
    {
        private readonly RestClient _client;
        private readonly IMemoryCache _cache;

        public FilmService(IMemoryCache cache)
        {
            _client = new RestClient("https://swapi.dev/api");
            _cache = cache;
        }

        public async Task<List<FilmModel>?> GetAllFilms()
        {
            string cacheKey = $"films";

            if(_cache.TryGetValue(cacheKey, out List<FilmModel>? films))
            {
                return films;
            }


            var request = new RestRequest("films", Method.Get);
            var response = await _client.ExecuteAsync<FilmReponseModel>(request);

            if (!response.IsSuccessful)
            {
                if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    return null;
                }   
                throw new Exception($"Error fetching film: {response.ErrorMessage}");
            }

            var cacheEntryOptions = new MemoryCacheEntryOptions { AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5) };

            _cache.Set(cacheKey, response.Data?.Results, cacheEntryOptions);
            return response.Data?.Results;
        }

        public async Task<FilmModel?> GetFilmById(int id)
        {
            string cacheKey = $"film_{id}";

            if(_cache.TryGetValue(cacheKey, out FilmModel? cachedFilm))
            {
                return cachedFilm;
            }

            var request = new RestRequest($"films/{id}", Method.Get);
            var response = await _client.ExecuteAsync<FilmModel>(request);

            if (!response.IsSuccessful)
            {
                if (response.StatusCode == HttpStatusCode.NotFound)
                {
                    return null;
                }
                throw new Exception($"Error fetching film: {response.ErrorMessage}");
            }

            var cacheEntryOptions = new MemoryCacheEntryOptions { AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5) };

            _cache.Set(cacheKey, response.Data, cacheEntryOptions);
            return response.Data;
        }

        public async Task<List<CharacterModel>> GetCharactersForFilm(int filmId)
        {
            string cacheKey = $"characters_film_{filmId}";

            if (_cache.TryGetValue(cacheKey, out List<CharacterModel>? cachedCharacter))
            {
                return cachedCharacter;
            }

            var film = await GetFilmById(filmId);

            if (film == null || film.Characters == null)
            {
                return null;
            }

            var characters = new List<CharacterModel>();

            foreach (var characterUrl in film.Characters)
            {
                var relativePath = characterUrl.Replace("https://swapi.dev/api/", "");

                var request = new RestRequest(relativePath, Method.Get);
                var response = await _client.ExecuteAsync<CharacterModel>(request);

                if (response.IsSuccessful && response.Data != null)
                {
                    characters.Add(response.Data);
                }
            }

            var cacheEntryOptions = new MemoryCacheEntryOptions { AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5) };

            _cache.Set(cacheKey, characters, cacheEntryOptions);
            return characters;
        }

        public async Task<List<StarshipModel>> GetStarshipsForFilm(int filmId)
        {
            string cacheKey = $"starships_film_{filmId}";

            if(_cache.TryGetValue(cacheKey, out List<StarshipModel>? cachedStarships))
            {
                return cachedStarships;
            }

            var film = await GetFilmById(filmId);

            if (film == null || film.Starships == null)
            {
                return null;
            }

            var starships = new List<StarshipModel>();

            foreach (var starshipUrl in film.Starships)
            {
                var relativePath = starshipUrl.Replace("https://swapi.dev/api/", "");

                var request = new RestRequest(relativePath, Method.Get);
                var response = await _client.ExecuteAsync<StarshipModel>(request);

                if (response.IsSuccessful && response.Data != null)
                {
                    starships.Add(response.Data);
                }
            }

            var cacheEntryOptions = new MemoryCacheEntryOptions { AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5) };

            _cache.Set(cacheKey, starships, cacheEntryOptions);
            return starships;
        }
    }
}
