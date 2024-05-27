using BattAnimeZone.Components.Models.Anime;
using BattAnimeZone.Components.Models.AnimeDTOs;

namespace BattAnimeZone.Services
{
    public partial class AnimeService
    {
        public async Task<List<LiGenreAnimeDTO>> GetAnimesForListGenreAnimes(int genre_id)
        {
            List<Anime> apgs;
            if (this.animesPerGenre.TryGetValue(genre_id, out apgs)) return mapper.Map<List<LiGenreAnimeDTO>>(apgs);
            return new List<LiGenreAnimeDTO> { new LiGenreAnimeDTO() };
        }
    }
}
