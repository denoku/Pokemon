using Sabio.Data;
using Sabio.Data.Providers;
using Sabio.Models.Domain;
using System;
using System.Collections.Generic;
using System.Data;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Sabio.Services
{
    public class LookUpService : ILookUpService
    {
        IDataProvider _data = null;
        public LookUpService(IDataProvider data)
        {
            _data = data;
        }

        public List<LookUp> GetLookUp(string tableName)
        {
            string procName = $"dbo.{tableName}_SelectAll";

            List<LookUp> list = null;

            _data.ExecuteCmd(procName, inputParamMapper: null, singleRecordMapper: delegate (IDataReader reader, short set)
            {
                int startingIndex = 0;
                LookUp lookUp = MapSingleLookUp(reader, ref startingIndex);

                if (list == null)
                {
                    list = new List<LookUp>();
                };

                list.Add(lookUp);
            }

           );

            return list;
        }

        public Dictionary<string, List<LookUp>> GetTypes(string[] tableNames)
        {
            Dictionary<string, List<LookUp>> result = null;

            foreach (string table in tableNames)
            {
                string name = ToCamelCase(table);
                List<LookUp> list = GetLookUp(name);

                if (result == null)
                {
                    result = new Dictionary<string, List<LookUp>>();
                    result.Add(name, list);
                }
                else
                {
                    result.Add(name, list);
                }
            }
            return result;
        }

        private static string ToCamelCase(string str)
        {
            string name = null;
            if (str.Length > 0)
            {
                str = Regex.Replace(str, "([A-Z])([A-Z]+)($|[A-Z])", m => m.Groups[1].Value + m.Groups[2].Value.ToLower() + m.Groups[3].Value);
                name = char.ToLower(str[0]) + str.Substring(1);
            }
            return name;
        }
        public LookUp MapSingleLookUp(IDataReader reader, ref int startingIndex)
        {
            LookUp alookUp = new LookUp();

            alookUp.Id = reader.GetSafeInt32(startingIndex++);
            alookUp.Name = reader.GetSafeString(startingIndex++);
            return alookUp;
        }

        public Ability MapSingleLookUpV2(IDataReader reader, ref int startingIndex)
        {
            Ability alookUp = new Ability();

            alookUp.Id = reader.GetSafeInt32(startingIndex++);
            alookUp.Name = reader.GetSafeString(startingIndex++);
            alookUp.Description= reader.GetSafeString(startingIndex++);
            return alookUp;
        }
    }
}