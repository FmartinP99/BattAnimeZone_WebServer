﻿using BattAnimeZone.Components.Models;
using CsvHelper;
using Newtonsoft.Json;
using System.Globalization;
using System.Text;
using F23.StringSimilarity;
using System.Collections.Generic;
using BattAnimeZone.Components.Pages;

namespace BattAnimeZone.Services
{
	public class AnimeService
	{
		private Dictionary<int, Anime> animes = new Dictionary<int, Anime> { };
		private Dictionary<string, int> animes_name_id_pair = new Dictionary<string, int> { };

		public AnimeService()
		{
			using (var reader = new StreamReader("Files/mal_data_filtered_filled.csv"))
			using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
			{
				int counter = 1;

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
					relationship_str = DecodeJSString(relationship_str);
					List<Relations> relations = JsonConvert.DeserializeObject<List<Relations>>(relationship_str);

					List<External> externals = JsonConvert.DeserializeObject<List<External>>(csv.GetField("external"));
					List<Streaming> streamings = JsonConvert.DeserializeObject<List<Streaming>>(csv.GetField("streaming"));

					Anime new_anime = new Anime
					{
						Mal_id = csv.GetField<int>("mal_id"),
						Url = csv.GetField("url"),
						Title_english = csv.GetField("title_english"),
						Title_japanese = csv.GetField("title_japanese"),
						Ttile_synonyms = title_synonyms,
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
						Background = csv.GetField("background"),
						Season = csv.GetField("season"),
						Year = csv.GetField<float>("year"),
						Producers = producers,
						Licensors = licensors,
						Studios = studios,
						Genres = genres,
						Themes = themes,
						Relations = relations,
						Externals = externals,
						Streamings = streamings,
						Image_jpg_url = csv.GetField("images.jpg.image_url"),
						Image_small_jpg_url = csv.GetField("images.jpg.small_image_url"),
						Image_large_jpg_url = csv.GetField("images.jpg.large_image_url"),
						Image_webp_url = csv.GetField("images.webp.image_url"),
						Image_small_webp_url = csv.GetField("images.webp.small_image_url"),
						Image_large_webp_url = csv.GetField("images.webp.large_image_url"),
						Trailer_url = csv.GetField("trailer.url"),
						Trailer_embed_url = csv.GetField("trailer.embed_url"),
						Trailer_image_url = csv.GetField("trailer.images.image_url"),
						Trailer_image_small_url = csv.GetField("trailer.images.small_image_url"),
						Trailer_image_medium_url = csv.GetField("trailer.images.medium_image_url"),
						Trailer_image_large_url = csv.GetField("trailer.images.large_image_url"),
						Trailer_image_maximum_url = csv.GetField("trailer.images.maximum_image_url"),
						Aired_from_day = csv.GetField<float>("aired.prop.from.day"),
						Aired_from_month = csv.GetField<float>("aired.prop.from.month"),
						Aired_from_year = csv.GetField<float>("aired.prop.from.year"),
						Aired_to_day = csv.GetField<float>("aired.prop.to.day"),
						Aired_to_month = csv.GetField<float>("aired.prop.to.month"),
						Aired_to_year = csv.GetField<float>("aired.prop.to.year"),
						Aired_string = csv.GetField("aired.string"),
					};
					animes.Add(new_anime.Mal_id, new_anime);
					//animes_name_id_pair.Add(new_anime.Title_english, new_anime.Mal_id);
					//animes_name_id_pair.Add(new_anime.Title_japanese, new_anime.Mal_id);
					counter += 1;

				}
			}
		}



		public Task<Dictionary<int, Anime>> GetAnimes()
		{
			return Task.FromResult(this.animes);
		}

		public async Task<Anime> GetAnimeByID(int mal_id)
		{
			Anime return_anime;
			if (this.animes.TryGetValue(mal_id, out return_anime)) return return_anime;
			return new Anime();

		}

		public async Task<Dictionary<string, int>> GetAnimeNameIdPairs()
		{
			return animes_name_id_pair;
		}


		public async Task<List<Anime>> GetSimilarAnimes(int n, string name)
		{
			name = name.ToLower();

			var distance_metric = new JaroWinkler();

			Dictionary<int, double> distances = new Dictionary<int, double>();

			foreach (Anime anime in this.animes.Values)
			{
				double eng_distance = double.MaxValue;
				double jp_distance = double.MaxValue;
				if (anime.Title_english != "") eng_distance = distance_metric.Distance(name, anime.Title_english.ToLower());
				if (anime.Title_japanese != "") jp_distance =  distance_metric.Distance(name, anime.Title_japanese.ToLower());
				distances.Add(anime.Mal_id, eng_distance < jp_distance ? eng_distance : jp_distance);
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


		public static string DecodeJSString(string s)
		{
			StringBuilder builder;
			char ch, ch2;
			int num, num2, num3, num4, num5, num6, num7, num8;
			if (string.IsNullOrEmpty(s) || !s.Contains(@"\"))
			{
				return s;
			}
			builder = new StringBuilder();
			num = s.Length;
			num2 = 0;
			while (num2 < num)
			{
				ch = s[num2];
				if (ch != 0x5c)
				{
					builder.Append(ch);
				}
				else if (num2 < (num - 5) && s[num2 + 1] == 0x75)
				{
					num3 = HexToInt(s[num2 + 2]);
					num4 = HexToInt(s[num2 + 3]);
					num5 = HexToInt(s[num2 + 4]);
					num6 = HexToInt(s[num2 + 5]);
					if (num3 < 0 || num4 < 0 | num5 < 0 || num6 < 0)
					{
						builder.Append(ch);
					}
					else
					{
						ch = (char)((((num3 << 12) | (num4 << 8)) | (num5 << 4)) | num6);
						num2 += 5;
						builder.Append(ch);
					}
				}
				else if (num2 < (num - 3) && s[num2 + 1] == 0x78)
				{
					num7 = HexToInt(s[num2 + 2]);
					num8 = HexToInt(s[num2 + 3]);
					if (num7 < 0 || num8 < 0)
					{
						builder.Append(ch);
					}
					else
					{
						ch = (char)((num7 << 4) | num8);
						num2 += 3;
						builder.Append(ch);
					}
				}
				else
				{
					if (num2 < (num - 1))
					{
						ch2 = s[num2 + 1];
						if (ch2 == 0x5c)
						{
							builder.Append(@"\");
							num2 += 1;
						}
						else if (ch2 == 110)
						{
							builder.Append("\n");
							num2 += 1;
						}
						else if (ch2 == 0x74)
						{
							builder.Append("\t");
							num2 += 1;
						}
					}
					builder.Append(ch);
				}
				num2 += 1;
			}
			return builder.ToString();
		}

		public static string EncodeJSString(string sInput)
		{
			StringBuilder builder;
			string str;
			char ch;
			int num;
			builder = new StringBuilder(sInput);
			builder.Replace(@"\", @"\\");
			builder.Replace("\r", @"\r");
			builder.Replace("\n", @"\n");
			builder.Replace("\"", "\\\"");
			str = builder.ToString();
			builder = new StringBuilder();
			num = 0;
			while (num < str.Length)
			{
				ch = str[num];
				if (0x7f >= ch)
				{
					builder.Append(ch);
				}
				else
				{
					builder.AppendFormat(@"\u{0:X4}", (int)ch);
				}
				num += 1;
			}
			return builder.ToString();
		}

		private static int HexToInt(char h)
		{
			if (h < 0x30 || h > 0x39)
			{
				if (h < 0x61 || h > 0x66)
				{
					if (h < 0x41 || h > 0x46)
					{
						return -1;
					}
					return ((h - 0x41) + 10);
				}
				return ((h - 0x61) + 10);
			}
			return (h - 0x30);
		}
	}
}

