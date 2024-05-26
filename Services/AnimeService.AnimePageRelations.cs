using BattAnimeZone.Components.Models.Anime;
using BattAnimeZone.Components.Models.AnimeDTOs;

namespace BattAnimeZone.Services
{
    public partial class AnimeService
    {
        public async Task<AnimeRelationsKeyDTO> GetAnimeRelationsKeyByID(int mal_id)
        {
            Anime return_anime;
            if (this.animes.TryGetValue(mal_id, out return_anime)) return mapper.Map<AnimeRelationsKeyDTO>(return_anime);
            return new AnimeRelationsKeyDTO();
        }

        public async Task<List<AnimeRelationDTO>> GetRelations(AnimeRelationsKeyDTO anime)
        {

            List<Anime> relational_animes = new List<Anime>();
            foreach (var relation in anime.Relations)
            {
                foreach (var entry in relation.Entry)
                {
                    if (entry.Type == "anime")
                    {
                        relational_animes.Add(await this.GetAnimeByID(entry.Mal_id));
                    }
                }
            }
            return mapper.Map<List<AnimeRelationDTO>> (relational_animes);
        }
    }
}
