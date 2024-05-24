namespace BattAnimeZone.Components.Models.AnimeDTOs
{
	public class AnimeHomePageDTO
	{
		public int Mal_id { get; set; } = -1;
		//public string Url { get; set; } = string.Empty;
		public string Title { get; set; } = string.Empty;
		public string Title_english { get; set; } = string.Empty;
		public string Title_japanese { get; set; } = string.Empty;
		//public List<string> Ttile_synonyms { get; set; } = new List<string> {""};
		public string Media_type { get; set; } = string.Empty;
		public string Status { get; set; } = string.Empty;
		public float Score { get; set; } = -1;
		public string Season { get; set; } = string.Empty;
		public string Image_large_webp_url { get; set; } = string.Empty;
	}
}
