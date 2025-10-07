using System;
using System.Collections.Generic;

namespace SwapiWrapperApi.Models
{
    public class FilmModel
    {
        public string? Title { get; set; }
        public int? Episode_id { get; set; }
        public string? Opening_crawl { get; set; }
        public string? Director { get; set; }
        public string? Producer { get; set; }
        public DateTime? Release_date { get; set; }
        public List<string>? Species { get; set; }
        public List<string>? Starships { get; set; }
        public List<string>? Vehicles { get; set; }
        public List<string>? Characters { get; set; }
        public List<string>? Planets { get; set; }
        public string? Url { get; set; }
        public string? Created { get; set; }
        public string? Edited { get; set; }
    }
}
