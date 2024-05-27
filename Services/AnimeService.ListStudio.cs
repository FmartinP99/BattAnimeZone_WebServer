﻿using BattAnimeZone.Components.Models.ProductionEntity;
using BattAnimeZone.Components.Models.ProductionEntityDTOs;

namespace BattAnimeZone.Services
{
    public partial class AnimeService
    {
        public Task<Dictionary<int, LiProductionEntityDTO>> GetProductionEntities()
        {
            Dictionary<int, ProductionEntity> prodents = this.productionEntities;
            return Task.FromResult(productionEntityMapper.Map<Dictionary<int, LiProductionEntityDTO>>(prodents));
        }
    }
}