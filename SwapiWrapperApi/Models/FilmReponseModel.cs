using System;

namespace SwapiWrapperApi.Models
{
    public class FilmReponseModel
    {
        public int? Count { get; set; }
        public string? Next { get; set; }
        public string? Previous { get; set; }
        public List<FilmModel>? Results { get; set; }
    }
}
