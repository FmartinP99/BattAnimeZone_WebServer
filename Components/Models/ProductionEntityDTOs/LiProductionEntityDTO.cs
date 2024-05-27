using BattAnimeZone.Components.Models.ProductionEntity;

namespace BattAnimeZone.Components.Models.ProductionEntityDTOs
{
    public class LiProductionEntityDTO
    {
            public int Mal_id { get; set; } = -1;
            public List<ProductionEntityTitle> Titles { get; set; } = new List<ProductionEntityTitle>();
            public int Favorites { get; set; } = -1;
            public int Count { get; set; } = -1;
            public string Image_url { get; set; } = string.Empty;
    }
}
