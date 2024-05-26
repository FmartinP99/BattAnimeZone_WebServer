using CsvHelper;
using Newtonsoft.Json;
using System.Globalization;
using System.Text;
using F23.StringSimilarity;
using BattAnimeZone.Components.Models.Anime;
using BattAnimeZone.Components.Models.AnimeDTOs;
using BattAnimeZone.Components.Models.ProductionEntity;
using BattAnimeZone.Components.Models.Genre;
using System.Collections;
using BattAnimeZone.Components.Pages;
using System.Runtime.CompilerServices;
using System.ComponentModel;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using BattAnimeZone.Utilities;
using AutoMapper;

namespace BattAnimeZone.Services
{
	public partial class AnimeService
	{

		//mapper for AnimeDTO's
		IMapper mapper;


		private Dictionary<int, Anime> animes = new Dictionary<int, Anime> { };

        private Dictionary<int, ProductionEntity> productionEntities = new Dictionary<int, ProductionEntity> { };
        private Dictionary<int, HashSet<int>> animesPerProducerIdHash = new Dictionary<int, HashSet<int>> { };
        private Dictionary<int, HashSet<int>> animesPerLicensorIdHash = new Dictionary<int, HashSet<int>> { };
        private Dictionary<int, HashSet<int>> animesPerStudioIdHash = new Dictionary<int, HashSet<int>> { };

        private Dictionary<int, AnimeGenre> genres = new Dictionary<int, AnimeGenre> { };
		private Dictionary<int, HashSet<int>> animesPerGenreIdsHash = new Dictionary<int, HashSet<int>>();
		private Dictionary<int, List<Anime>> animesPerGenre = new Dictionary<int, List<Anime>>();

		private List<string> mediaTypes = new List<string>();
		private Dictionary<string, HashSet<int>> animesPerMediaTypeIdsHash = new Dictionary<string, HashSet<int>>();

		public AnimeService()
		{
			MapperConfiguration config = new MapperConfiguration(cfg => cfg.AddProfile<MappingProfile>());
			mapper = config.CreateMapper();



			FillAnimesAndMedia();
			FillProductionEntities();
			FillProductionIdHashes();
			FillGenres();
			FillAnimesPerGenreIdsHash();
			FillAnimesPerGenreList();
			FillAnimesPerMediaTypeIdsHas();

        }



		private void FillAnimesAndMedia()
		{
			using (var reader = new StreamReader("Files/mal_data_filtered_filled.csv"))
			using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
			{
				csv.Read();
				csv.ReadHeader();
				while (csv.Read())
				{

					/*
					csv.GetField<int>("mal_id");
					csv.GetField("url");
					csv.GetField("title_english");
					csv.GetField("title_japanese");
					csv.GetField("type");
					csv.GetField("source");
					csv.GetField<float>("episodes");
					csv.GetField("status");
					csv.GetField("duration");
					csv.GetField("rating");
					csv.GetField<float>("score");
					csv.GetField<float>("scored_by");
					csv.GetField<float>("rank");
					csv.GetField<float>("popularity");
					csv.GetField<float>("members");
					csv.GetField<float>("favorites");
					csv.GetField("synopsis");
					csv.GetField("background");
					csv.GetField("season");
					csv.GetField<float>("year");
					new List<Producer> { new Producer() };
						new List<Licensor> { new Licensor() };
					new List<Studio> { new Studio() };
					new List<Genre> { new Genre() };
					new List<Theme> { new Theme() };
					new List<Relation> { new Relation() };
					new List<External> { new External() };
					new List<Streaming> { new Streaming() };
					csv.GetField("images.jpg.image_url");
					csv.GetField("images.jpg.small_image_url");
					csv.GetField("images.jpg.large_image_url");
					csv.GetField("images.webp.image_url");
					csv.GetField("images.webp.small_image_url");
					csv.GetField("images.webp.large_image_url");
					csv.GetField("trailer.url");
					csv.GetField("trailer.embed_url");
					csv.GetField("trailer.images.image_url");
					csv.GetField("trailer.images.small_image_url");
					csv.GetField("trailer.images.medium_image_url");
					csv.GetField("trailer.images.large_image_url");
					csv.GetField("trailer.images.maximum_image_url");
					csv.GetField<float>("aired.prop.from.day");
					csv.GetField<float>("aired.prop.from.month");
					csv.GetField<float>("aired.prop.from.year");
					csv.GetField<float>("aired.prop.to.day");
					csv.GetField<float>("aired.prop.to.month");
					csv.GetField<float>("aired.prop.to.year");
					csv.GetField("aired.string");
					*/

					List<string> title_synonyms = JsonConvert.DeserializeObject<List<string>>(csv.GetField("title_synonyms"));
					List<Producer> producers = JsonConvert.DeserializeObject<List<Producer>>(csv.GetField("producers"));
					List<Licensor> licensors = JsonConvert.DeserializeObject<List<Licensor>>(csv.GetField("licensors"));
					List<Studio> studios = JsonConvert.DeserializeObject<List<Studio>>(csv.GetField("studios"));
					List<Genre> genres = JsonConvert.DeserializeObject<List<Genre>>(csv.GetField("genres"));
					List<Theme> themes = JsonConvert.DeserializeObject<List<Theme>>(csv.GetField("themes"));

					string relationship_str = csv.GetField("relations");
					relationship_str = JsonStringProcessor.DecodeJSString(relationship_str);
					List<Relations> relations = JsonConvert.DeserializeObject<List<Relations>>(relationship_str);

					List<External> externals = JsonConvert.DeserializeObject<List<External>>(csv.GetField("external"));
					List<Streaming> streamings = JsonConvert.DeserializeObject<List<Streaming>>(csv.GetField("streaming"));

					Anime new_anime = new Anime
					{
						Mal_id = csv.GetField<int>("mal_id"),
						//Url = csv.GetField("url"),
						Title = csv.GetField("title"),
						Title_english = csv.GetField("title_english"),
						Title_japanese = csv.GetField("title_japanese"),
						//Ttile_synonyms = title_synonyms,
						Media_type = csv.GetField("type"),
						Source = csv.GetField("source"),
						Episodes = csv.GetField<float>("episodes"),
						Status = csv.GetField("status"),
						Duration = csv.GetField("duration"),
						Rating = csv.GetField("rating"),
						Score = csv.GetField<float>("score"),
						Scored_by = csv.GetField<float>("scored_by"),
						Rank = csv.GetField<float>("rank"),
						Popularity = csv.GetField<float>("popularity"),
						Members = csv.GetField<float>("members"),
						Favorites = csv.GetField<float>("favorites"),
						Synopsis = csv.GetField("synopsis"),
						//Background = csv.GetField("background"),
						Season = csv.GetField("season"),
						Year = csv.GetField<float>("year"),
						Producers = producers,
						Licensors = licensors,
						Studios = studios,
						Genres = genres,
						Themes = themes,
						Relations = relations,
						//Externals = externals,
						//Streamings = streamings,
						//Image_jpg_url = csv.GetField("images.jpg.image_url"),
						//Image_small_jpg_url = csv.GetField("images.jpg.small_image_url"),
						//Image_large_jpg_url = csv.GetField("images.jpg.large_image_url"),
						//Image_webp_url = csv.GetField("images.webp.image_url"),
						//Image_small_webp_url = csv.GetField("images.webp.small_image_url"),
						Image_large_webp_url = csv.GetField("images.webp.large_image_url"),
						//Trailer_url = csv.GetField("trailer.url"),
						//Trailer_embed_url = csv.GetField("trailer.embed_url"),
						//Trailer_image_url = csv.GetField("trailer.images.image_url"),
						//Trailer_image_small_url = csv.GetField("trailer.images.small_image_url"),
						//Trailer_image_medium_url = csv.GetField("trailer.images.medium_image_url"),
						//Trailer_image_large_url = csv.GetField("trailer.images.large_image_url"),
						//Trailer_image_maximum_url = csv.GetField("trailer.images.maximum_image_url"),
						Aired_from_day = csv.GetField<float>("aired.prop.from.day"),
						Aired_from_month = csv.GetField<float>("aired.prop.from.month"),
						Aired_from_year = csv.GetField<float>("aired.prop.from.year"),
						Aired_to_day = csv.GetField<float>("aired.prop.to.day"),
						Aired_to_month = csv.GetField<float>("aired.prop.to.month"),
						Aired_to_year = csv.GetField<float>("aired.prop.to.year"),
						Aired_string = csv.GetField("aired.string"),
					};
					animes.Add(new_anime.Mal_id, new_anime);
					if (!mediaTypes.Contains(new_anime.Media_type)) mediaTypes.Add(new_anime.Media_type);

				}
			}
		}



		public void FillProductionEntities()
		{
			using (var reader = new StreamReader("Files/mal_producers.csv"))
			using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
			{
				csv.Read();
				csv.ReadHeader();
				while (csv.Read())
				{
					List<ProductionEntityTitle> producerTitle = JsonConvert.DeserializeObject<List<ProductionEntityTitle>>(csv.GetField("titles"));

					ProductionEntity new_producer = new ProductionEntity
					{
						Mal_id = csv.GetField<int>("mal_id"),
						Url = csv.GetField("url"),
						Titles = producerTitle,
						Favorites = csv.GetField<int>("favorites"),
						Established = csv.GetField("established"),
						About = csv.GetField("about"),
						Count = csv.GetField<int>("count"),
						Image_url = csv.GetField("images.jpg.image_url"),

					};
					productionEntities.Add(new_producer.Mal_id, new_producer);

				}
			}
		}

		public void FillProductionIdHashes()
		{
			Dictionary<int, HashSet<int>> producerhashes = new Dictionary<int, HashSet<int>>();
			Dictionary<int, HashSet<int>> licensorhashes = new Dictionary<int, HashSet<int>>();
            Dictionary<int, HashSet<int>> studiohashes = new Dictionary<int, HashSet<int>>();

			foreach (int prodentkey in productionEntities.Keys) {
				producerhashes[prodentkey] = new HashSet<int>();
                licensorhashes[prodentkey] = new HashSet<int>();
                studiohashes[prodentkey] = new HashSet<int>();
			}

			foreach (Anime anim in this.animes.Values)
			{

				foreach(Producer prod in anim.Producers)
				{
					if(productionEntities.Keys.Contains(prod.Mal_id))
					producerhashes[prod.Mal_id].Add(anim.Mal_id);
				}

                foreach (Licensor licensor in anim.Licensors)
                {
                    if (productionEntities.Keys.Contains(licensor.Mal_id))
                        licensorhashes[licensor.Mal_id].Add(anim.Mal_id);
                }

                foreach (Studio studio in anim.Studios)
                {
                    if (productionEntities.Keys.Contains(studio.Mal_id))
                        studiohashes[studio.Mal_id].Add(anim.Mal_id);
                }

            }

			this.animesPerProducerIdHash = producerhashes;
			this.animesPerLicensorIdHash = licensorhashes;
			this.animesPerStudioIdHash = studiohashes;
        }


        public void FillGenres()
		{
			using (var reader = new StreamReader("Files/mal_anime_genres.csv"))
			using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
			{
				csv.Read();
				csv.ReadHeader();
				while (csv.Read())
				{
					AnimeGenre new_genre = new AnimeGenre
					{
						Mal_id = csv.GetField<int>("mal_id"),
						Name = csv.GetField("name"),
						Url = csv.GetField("url"),
						Count = csv.GetField<int>("count"),

					};
					genres.Add(new_genre.Mal_id, new_genre);

				}
			}
		}

		public void FillAnimesPerGenreIdsHash()
		{
			Dictionary<int, HashSet<int>> tempApG = new Dictionary<int, HashSet<int>>();

			foreach (var gen in this.genres)
			{
				tempApG.Add(gen.Key, new HashSet<int>());


			}

			foreach (var ani in this.animes)
			{
				foreach (var gen in ani.Value.Genres)
				{
					tempApG[gen.Mal_id].Add(ani.Value.Mal_id);
				}

				foreach (var gen in ani.Value.Themes)
				{
					tempApG[gen.Mal_id].Add(ani.Value.Mal_id);
				}
			}

			this.animesPerGenreIdsHash = tempApG;
		}


		public void FillAnimesPerGenreList()
		{
			Dictionary<int, List<Anime>> tempApG = new Dictionary<int, List<Anime>>();

			foreach (var ApGHash in this.animesPerGenreIdsHash)
			{

				var animes = ApGHash.Value.Select(ApGHashId => this.GetAnimeByIDSync(ApGHashId)).ToList();

				tempApG.Add(ApGHash.Key, animes);

			}

			this.animesPerGenre = tempApG;
		}

		public void FillAnimesPerMediaTypeIdsHas()
		{
            Dictionary<string, HashSet<int>> tempApM = new Dictionary<string, HashSet<int>>();

            foreach (var mtype in this.mediaTypes)
            {
                tempApM.Add(mtype, new HashSet<int>());
            }

            foreach (var ani in this.animes)
            {
               tempApM[ani.Value.Media_type].Add(ani.Value.Mal_id);
            }

			this.animesPerMediaTypeIdsHash = tempApM;
        }

		public Task<Dictionary<int, Anime>> GetAllAnimes()
		{
			return Task.FromResult(this.animes);
		}

		public async Task<Anime> GetAnimeByID(int mal_id)
		{
			Anime return_anime;
			if (this.animes.TryGetValue(mal_id, out return_anime)) return return_anime;
			return new Anime();

		}

		public Anime GetAnimeByIDSync(int mal_id)
		{
			Anime return_anime;
			if (this.animes.TryGetValue(mal_id, out return_anime)) return return_anime;
			return new Anime();

		}

		public async Task<List<Anime>> GetMultipleAnimes(HashSet<int> ids)
		{
			List<Anime> animelist = new List<Anime>();
			foreach (int id in ids)
			{
				animelist.Add(await GetAnimeByID(id));
			}
			return animelist;
		}

		public Task<Dictionary<int, AnimeGenre>> GetGenres()
		{
			return Task.FromResult(this.genres);
		}

		public Task<Dictionary<int, ProductionEntity>> GetProductionEntities()
		{
			return Task.FromResult(this.productionEntities);
		}

        public async Task<ProductionEntity> GetProductionEntityById(int prodid)
        {
            ProductionEntity prodent;
            if (this.productionEntities.TryGetValue(prodid, out prodent)) return prodent;
            return new ProductionEntity();
        }

        public Task<Dictionary<int, HashSet<int>>> GetAnimePerProducerIds()
        {
            return Task.FromResult(this.animesPerProducerIdHash);
        }

        public Task<Dictionary<int, HashSet<int>>> GetAnimePerLicensorIds()
        {
            return Task.FromResult(this.animesPerLicensorIdHash);
        }

        public Task<Dictionary<int, HashSet<int>>> GetAnimePerStudioIds()
        {
            return Task.FromResult(this.animesPerStudioIdHash);
        }


        public Task<HashSet<int>> GetAnimesOfProducer(int prodid)
        {
			HashSet<int> animes;
            if (this.animesPerProducerIdHash.TryGetValue(prodid, out animes)) return Task.FromResult(animes);
			return Task.FromResult(new HashSet<int>());
        }

        public Task<HashSet<int>> GetAnimesOfLicensor(int prodid)
        {
            HashSet<int> animes;
            if (this.animesPerLicensorIdHash.TryGetValue(prodid, out animes)) return Task.FromResult(animes);
            return Task.FromResult(new HashSet<int>());
        }

        public Task<HashSet<int>> GetAnimesOfStudio(int prodid)
        {
            HashSet<int> animes;
            if (this.animesPerStudioIdHash.TryGetValue(prodid, out animes)) return Task.FromResult(animes);
            return Task.FromResult(new HashSet<int>());
        }



        public async Task<List<Anime>> GetSimilarAnimes(int n, string name)
		{
			name = name.ToLower();

			var distance_metric = new JaroWinkler();

			Dictionary<int, double> distances = new Dictionary<int, double>();

			foreach (Anime anime in this.animes.Values)
			{
				double default_distance = double.MaxValue;
				double eng_distance = double.MaxValue;
				double jp_distance = double.MaxValue;
				if (anime.Title != "") default_distance = distance_metric.Distance(name, anime.Title_english.ToLower());
				if (anime.Title_english != "") eng_distance = distance_metric.Distance(name, anime.Title_japanese.ToLower());
				if (anime.Title_japanese != "") jp_distance = distance_metric.Distance(name, anime.Title_japanese.ToLower());
				double min_distance = Math.Min(jp_distance, Math.Min(eng_distance, default_distance));
				if (min_distance < 0.7) distances.Add(anime.Mal_id, min_distance);
			}

			var sorted_distances = distances.OrderBy(kv => kv.Value);
			var top_n = sorted_distances.Take(n).Select(kv => kv.Key);

			List<Anime> return_Animes = new List<Anime>();
			foreach (int id in top_n)
			{
				return_Animes.Add(await this.GetAnimeByID(id));
			}

			return return_Animes;


		}

		public async Task<List<Anime>> GetRelations(Anime anime)
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
			return relational_animes;
		}

		public async Task<Dictionary<int, HashSet<int>>> GetAnimesPerGenreIds()
		{
			return this.animesPerGenreIdsHash;
		}

        public async Task<Dictionary<string, HashSet<int>>> GetAnimesPerMediaTypeIds()
        {
            return this.animesPerMediaTypeIdsHash;
        }

        public async Task<List<string>> GetMediaTypes()
		{
			return this.mediaTypes;
		}

	}		
}

