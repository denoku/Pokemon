using Sabio.Models.Domain;
using System.Collections.Generic;
using System.Data;

namespace Sabio.Services
{
    public interface ILookUpService
    {
        List<LookUp> GetLookUp(string tableName);
        Dictionary<string, List<LookUp>> GetTypes(string[] tableNames);
        LookUp MapSingleLookUp(IDataReader reader, ref int startingIndex);
        Ability MapSingleLookUpV2(IDataReader reader, ref int startingIndex);
    }
}