using AutoMapper;
using BattAnimeZone.Components.Models.ProductionEntity;
using BattAnimeZone.Components.Models.ProductionEntityDTOs;

namespace BattAnimeZone.Utilities
{
    public class MappingProfileProductionEntity : Profile
    {
        public MappingProfileProductionEntity()
        {
            CreateMap<ProductionEntity, LiProductionEntityDTO>();
        }
    }
}
