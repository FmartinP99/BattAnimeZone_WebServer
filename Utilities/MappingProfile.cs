using AutoMapper;
using BattAnimeZone.Components.Models.Anime;
using BattAnimeZone.Components.Models.AnimeDTOs;

namespace BattAnimeZone.Utilities
{
	public class MappingProfile : Profile
	{
		public MappingProfile()
		{
			CreateMap<Anime, AnimeHomePageDTO>();
			CreateMap<Anime, AnimePageDTO>();
			CreateMap<Anime, LiAnimeDTO>();
			CreateMap<Anime, LiGenreAnimeDTO>();
			CreateMap<Anime, AnimeSearchResultDTO>();
			CreateMap<Anime, AnimeRelationsKeyDTO>();
			CreateMap<Anime, AnimeRelationDTO>();
		}

	}
}
