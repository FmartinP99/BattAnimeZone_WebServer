﻿using BattAnimeZone.Components.Models.Anime;
using BattAnimeZone.Components.Models.AnimeDTOs;

namespace BattAnimeZone.Services
{
    public partial class AnimeService
    {
        public async Task<AnimePageDTO> GetAnimePageDTOByID(int mal_id)
        {
            Anime return_anime;
            if (this.animes.TryGetValue(mal_id, out return_anime)) return animeMapper.Map<AnimePageDTO>(return_anime); ;
            return new AnimePageDTO();
        }
    }
}
