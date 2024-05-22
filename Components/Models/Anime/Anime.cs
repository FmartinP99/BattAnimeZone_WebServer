namespace BattAnimeZone.Components.Models.Anime
{
	public class Anime
	{
		public int Mal_id { get; set; } = -1;
		public string Title { get; set; } = string.Empty;
		public string Title_english { get; set; } = string.Empty;
		public string Title_japanese { get; set; } = string.Empty;
		public string Media_type { get; set; } = string.Empty;
		public string Source { get; set; } = string.Empty;
		public float Episodes { get; set; } = -1;
		public string Status { get; set; } = string.Empty;
		public string Duration { get; set; } = string.Empty;
		public string Rating { get; set; } = string.Empty;
		public float Score { get; set; } = -1;
		public float Scored_by { get; set; } = -1;
		public float Rank {  get; set; } = -1;
		public float Popularity {  get; set; } = -1;
		public float Members {  get; set; } = -1;
		public float Favorites {  get; set; } = -1;
		public string Synopsis {  get; set; } = string.Empty;
		public string Season {  get; set; } = string.Empty;
		public float Year { get; set; } = -1;
		public List<Producer> Producers { get; set; }
		public List<Licensor> Licensors { get; set; }
		public List<Studio> Studios { get; set; }
		public List<Genre> Genres { get; set; }
		public List<Theme> Themes { get; set; }
		public List<Relations> Relations { get; set; }
		public string Image_large_webp_url { get; set; } = string.Empty;
		public float Aired_from_day { get; set; } = -1;
		public float Aired_from_month { get; set; } = -1;
		public float Aired_from_year { get; set; } = -1;
		public float Aired_to_day { get; set; } = -1;
		public float Aired_to_month { get; set; } = -1;
		public float Aired_to_year { get; set; } = -1;
		public string Aired_string { get; set; } = string.Empty;




	}
}
