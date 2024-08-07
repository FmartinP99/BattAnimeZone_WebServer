﻿using BattAnimeZone.Components.Models.Anime;
using BattAnimeZone.Components.Models.AnimeDTOs;

namespace BattAnimeZone.Services
{
    public partial class AnimeService
    {
        public async Task<List<LiAnimeDTO>> GetAnimesForListAnime(HashSet<int> ids)
        {
            List<Anime> animelist = new List<Anime>();
            foreach (int id in ids)
            {
                animelist.Add(await GetAnimeByID(id));
            }

            return animeMapper.Map<List<LiAnimeDTO>>(animelist);
        }
    }
}
