using BattAnimeZone.Components.Models.Anime;
using BattAnimeZone.Components.Models.AnimeDTOs;

namespace BattAnimeZone.Services
{
    public partial class AnimeService
    {
        public async Task<List<LiGenreDTO>> GetAnimesForListGenreAnimes(int genre_id)
        {
            List<Anime> apgs;
            if (this.animesPerGenre.TryGetValue(genre_id, out apgs)) return mapper.Map<List<LiGenreDTO>>(apgs);
            return new List<LiGenreDTO> { new LiGenreDTO() };
        }
    }
}
