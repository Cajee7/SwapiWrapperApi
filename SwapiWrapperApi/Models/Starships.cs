namespace SwapiWrapperApi.Models
{
    public class StarshipModel
    {
        public string? name { get; set; }
        public string? model { get; set; }
        public string? starship_class { get; set; }
        public string? manufacturer { get; set; }
        public string? cost_in_credits { get; set; }
        public string? length { get; set; }
        public string? crew { get; set; }
        public string? passengers { get; set; }
        public string? max_athmosphering_speed { get; set; }
        public string? hyperdrive_rating { get; set; }
        public string? MGLT { get; set; }
        public string? cargo_capacity { get; set; }
        public string? consumables { get; set; }
        public List<string>? films { get; set; }
        public List<string>? pilots { get; set; }
        public string? url { get; set; }
        public string? created { get; set; }
        public string? edited { get; set; }
    }
}
