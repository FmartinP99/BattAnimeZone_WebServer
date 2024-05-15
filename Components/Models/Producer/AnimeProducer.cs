namespace BattAnimeZone.Components.Models.Producer
{
	public class AnimeProducer
	{
		public int Mal_id { get; set; } = -1;
		public string Url { get; set; } = string.Empty;
		public List<ProducerTitle> Titles { get; set; } = new List<ProducerTitle>();
		public int Favorites { get; set; } = -1;
		public string Established { get; set; } = string.Empty;
		public string About { get; set; } = string.Empty;
		public int Count { get; set; } = -1;
		public string Image_url { get; set; } = string.Empty;
	}
}
