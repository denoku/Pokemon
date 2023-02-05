using Sabio.Data;
using Sabio.Data.Providers;
using Sabio.Models;
using Sabio.Models.Domain;
using Sabio.Models.Requests;
using Sabio.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sabio.Services
{
    public class PokemonService : IPokemonService
    {
        IDataProvider _data = null;
        ILookUpService _lookUpService;

        public PokemonService(IDataProvider data, ILookUpService lookUpService)
        { 
            _data = data;
            _lookUpService = lookUpService;
        }

        public Paged<Pokemon> GetPaginated(int pageIndex, int pageSize, int sortId)
        {
            Paged<Pokemon> pagedList = null;
            List<Pokemon> list = null;
            int total = 0;

            _data.ExecuteCmd("[dbo].[Pokedex_Pagination]", delegate (SqlParameterCollection param)
            {
                param.AddWithValue("@PageIndex", pageIndex);
                param.AddWithValue("@PageSize", pageSize);
                param.AddWithValue("@SortId", sortId);

            }, delegate (IDataReader reader, short set)
            {
                int startingIndex = 0;
                Pokemon pokemon = MapSinglePokemon(reader, ref startingIndex);

                if (total == 0)
                {
                    total = reader.GetSafeInt32(startingIndex++);
                }
                if (list == null)
                {
                    list = new List<Pokemon>();
                }
                list.Add(pokemon);
            });
            if (list != null)
            {
                pagedList = new Paged<Pokemon>(list, pageIndex, pageSize, total);
            }
            return pagedList;
        }

        public Paged<Pokemon> SearchPaginated(int pageIndex, int pageSize, string query)
        {
            Paged<Pokemon> pagedList = null;
            List<Pokemon> list = null;
            int total = 0;

            _data.ExecuteCmd("[dbo].[Pokemon_Search_Pagination]", delegate (SqlParameterCollection param)
            {
                param.AddWithValue("@PageIndex", pageIndex);
                param.AddWithValue("@PageSize", pageSize);
                param.AddWithValue("@Query", query);

            }, delegate (IDataReader reader, short set)
            {
                int startingIndex = 0;
                Pokemon pokemon = MapSinglePokemon(reader, ref startingIndex);

                if (total == 0)
                {
                    total = reader.GetSafeInt32(startingIndex++);
                }
                if (list == null)
                {
                    list = new List<Pokemon>();
                }
                list.Add(pokemon);
            });
            if (list != null)
            {
                pagedList = new Paged<Pokemon>(list, pageIndex, pageSize, total);
            }
            return pagedList;
        }
        public Pokemon GetPokemonById(int id)
        {
            Pokemon pokemon = null;

            _data.ExecuteCmd("[dbo].[Pokedex_Select_By_Id]", delegate (SqlParameterCollection col)
            {
                col.AddWithValue("@pokedexId", id);
            }, delegate (IDataReader reader, short set)
            {
                int startingIndex = 0;
                pokemon = MapSinglePokemon(reader, ref startingIndex);
            });
            return pokemon;
        }

        public int AddPokemon(PokemonAddRequest model)
        {
            int id = 0;
            DataTable myParamValueOne = MapAbilitiesToTable(model.Abilities);
            DataTable myParamValue = MapTypeToTable(model.Type);
            DataTable myParamValueTwo = MapWeaknessesToTable(model.Weaknesses);
            _data.ExecuteNonQuery("[dbo].[Pokemon_Insert]", inputParamMapper: delegate (SqlParameterCollection col)
            {
                AddCommonParams(model, col);
                col.AddWithValue("@Abilities", myParamValueOne);
                col.AddWithValue("@Type", myParamValue);
                col.AddWithValue("@Weaknesses", myParamValueTwo);

                SqlParameter idOut = new SqlParameter("@Id", SqlDbType.Int);
                idOut.Direction = ParameterDirection.Output;
                col.Add(idOut);
            },
            returnParameters: delegate (SqlParameterCollection returnCollection)
            {
                object ooId = returnCollection["@Id"].Value;
                int.TryParse(ooId.ToString(), out id);
            });

            return id;
        }

        private static void AddCommonParams(PokemonAddRequest model, SqlParameterCollection col)
        {
            col.AddWithValue("@NationalPokédexNumber", model.NationalPokédexNumber);
            col.AddWithValue("@Name", model.Name);
            col.AddWithValue("@Height", model.Height);
            col.AddWithValue("@Weight", model.Weight);
            col.AddWithValue("@PrimaryImageUrl", model.PrimaryImageUrl);
            col.AddWithValue("@Summary", model.Summary);
            col.AddWithValue("@Gender", model.Gender);
            col.AddWithValue("@CategoryId", model.CategoryId);
        }

        public Pokemon MapSinglePokemon(IDataReader reader, ref int startingIndex)
        {
            var pokemon = new Pokemon();
            pokemon.Category = new LookUp();

            pokemon.Id = reader.GetSafeInt32(startingIndex++);
            pokemon.NationalPokédexNumber = reader.GetSafeString(startingIndex++);
            pokemon.Name = reader.GetString(startingIndex++);
            pokemon.Height = reader.GetSafeString(startingIndex++);
            pokemon.Weight = reader.GetSafeString(startingIndex++);
            pokemon.PrimaryImageUrl = reader.GetString(startingIndex++);
            pokemon.Summary = reader.GetString(startingIndex++);
            pokemon.Gender = reader.GetSafeBool(startingIndex++);
            pokemon.Category = _lookUpService.MapSingleLookUp(reader, ref startingIndex);
            pokemon.Abilities = reader.DeserializeObject<List<Ability>>(startingIndex++);
            pokemon.Types = reader.DeserializeObject<List<LookUp>>(startingIndex++);
            pokemon.Weaknesses = reader.DeserializeObject<List<LookUp>>(startingIndex++);
            pokemon.DateCreated = reader.GetSafeDateTime(startingIndex++);
            pokemon.DateModified = reader.GetSafeDateTime(startingIndex++);

            return pokemon;

        }
        private DataTable MapTypeToTable(List<int> types)
        {
            DataTable table = new DataTable();
            table.Columns.Add("Type", typeof(Int32));
            foreach (int type in types)
            {
                DataRow row = table.NewRow();
                int startingIndex = 0;
                row.SetField(startingIndex++, type);
                table.Rows.Add(row);
            }

            return table;
        }
        private DataTable MapWeaknessesToTable(List<int> weaknesses)
        {
            DataTable table = new DataTable();
            table.Columns.Add("Weaknesses", typeof(Int32));
            foreach (int weakness in weaknesses)
            {
                DataRow row = table.NewRow();
                int startingIndex = 0;
                row.SetField(startingIndex++, weakness);
                table.Rows.Add(row);
            }

            return table;
        }

        private DataTable MapAbilitiesToTable(List<int> abilities)
        {
            DataTable table = new DataTable();
            table.Columns.Add("Type", typeof(Int32));
            foreach (int ability in abilities)
            {
                DataRow row = table.NewRow();
                int startingIndex = 0;
                row.SetField(startingIndex++, ability);
                table.Rows.Add(row);
            }

            return table;
        }
    }
}
