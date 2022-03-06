using Dapper;
using Listening.Infrastructure.Repositories.Abstract;
using Listening.Server.Entities.Specialized.ServiceModels;
using Listening.Core.ViewModels;
using Microsoft.Extensions.Configuration;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Reflection;
using Microsoft.Extensions.Logging;

namespace Listening.Server.Repositories.Postgres
{
    /// <summary>
    /// Basic implementation for dapper repository
    /// </summary>
    /// <typeparam name="T">type of object</typeparam>
    /// <typeparam name="Y">type of identifier</typeparam>
    public class BasePostgresRepository<T, Y> : IRepositoryPostgres<T, Y>
    {
        protected const string ID = "Id";

        private readonly string[] _propertyNames;
        private readonly string _idName;
        private readonly string _maxIdQuery;

        private readonly string _connectionString;

        public BasePostgresRepository(
            IConfiguration configuration)
        {
            var attr = typeof(T).GetTypeInfo().CustomAttributes;
            TableName = attr.Count() > 0 ? attr.First().ConstructorArguments.First().Value.ToString() : $"{typeof(T).Name}s";
            var properties = typeof(T).GetProperties().Where(x => !x.GetMethod.IsVirtual).ToList();
            var idProperty = typeof(T).GetProperties().First(x => x.Name.Contains(ID));
            if (!properties.Contains(idProperty))
                properties.Insert(0, idProperty);

            _propertyNames = properties.Select(x => x.Name).ToArray();
            _idName = idProperty.Name;
            _connectionString = configuration["Data:SqlPostegresConnectionString"];
            _maxIdQuery = $@"select max(""{ID}"") from public.""{TableName}""";
        }

        protected string TableName { get; set; }

        protected IDbConnection Connection
        {
            get { return new NpgsqlConnection(_connectionString); }
        }

        protected NpgsqlConnection NpgConnection
        {
            get { return new NpgsqlConnection(_connectionString); }
        }

        public virtual async Task Delete(IEnumerable<Y> itemIds)
        {
            var ids = itemIds.Select(x => new { Id = x });
            using (var connection = Connection)
            {
                await connection.ExecuteAsync(
                    $@"delete from public.""{TableName}"" 
                            where ""{_idName}""=@{_idName}", ids);
            }
        }

        public virtual async Task<T> GetById(Y id)
        {
            using (var connection = Connection)
            {
                var result = await connection.QueryAsync<T>(
                    $@"select * from public.""{TableName}"" where ""{_idName}""=@{_idName}",
                        new { Id = id });
                return result.FirstOrDefault();
            }
        }

        public virtual async Task Insert(IEnumerable<T> items)
        {
            var filteredValues = PrepareProperties(items.First());
            var query = BuildInsert(filteredValues);

            using (var connection = NpgConnection)
            {
                await connection.ExecuteAsync(query, items);
            }
        }

        public virtual async Task<Y> InsertOne(T item)
        {
            var filteredValues = PrepareProperties(item);
            var query = BuildInsert(filteredValues);
            var getIdQuery = $@"RETURNING ""{ID}"";";
            var fullQuery = $@"{query} {getIdQuery}";

            using (var connection = NpgConnection)
            {
                var id = await connection.QueryFirstAsync<Y>(fullQuery, item);
                return id;
            }
        }

        public virtual async Task Update(IEnumerable<T> items)
        {
            var filteredValues = PrepareProperties(items.First());
            var query = BuildUpdate(filteredValues);

            using (var connection = Connection)
            {
                await connection.ExecuteAsync(query, items);
            }
        }

        public async Task<long> GetMaxId()
        {
            using (var connection = Connection)
            {
                var result = await connection.QuerySingleAsync<string>(_maxIdQuery);
                return Convert.ToInt64(result);
            }
        }

        public async Task DeleteResultsAfterId(long id, bool shouldReseed = false)
        {
            var query = $@"delete from public.""{TableName}"" 
                            where ""{ID}"">@{ID}";
            var sequenceName = $"{TableName.Split('_').Last()}_{ID}_seq";
            var resedQuery = $@"ALTER SEQUENCE public.""{sequenceName}"" RESTART WITH ";

            using (var connection = Connection)
            {
                if (!shouldReseed)
                    await connection.ExecuteAsync($"{query}", new { id });
                else
                {
                    var max = await connection.QuerySingleAsync<string>(
                        $"{query}; {_maxIdQuery}", new { id });
                    await connection.ExecuteAsync(
                        $"{resedQuery} {Convert.ToInt64(max) + 1}");
                }
            }
        }

        private IEnumerable<string> PrepareProperties(T item)
        {
            var idValue = (Y)typeof(T).GetProperty(_idName).GetValue(item);
            var excludeProp = idValue.Equals(default(Y)) ? _idName : string.Empty;

            var filteredValues = _propertyNames.Except(new string[] { excludeProp });
            return filteredValues;
        }

        private string BuildInsert(IEnumerable<string> filteredValues)
        {
            var namesString = string.Join(",", filteredValues.Select(x => $"\"{x}\""));
            var valuesString = string.Join(",", filteredValues.Select(x => $"@{x}"));
            var query = $@"INSERT INTO public.""{TableName}"" ({namesString}) VALUES ({valuesString})";
            return query;
        }

        private string BuildUpdate(IEnumerable<string> filteredValues)
        {
            var valuesString = string.Join(",",
                filteredValues.Except(new string[] { _idName })
                    .Select(x => $"\"{x}\"=@{x}"));

            var query = $@"update public.""{TableName}"" 
                        set {valuesString}
                        where ""{_idName}""=@{_idName}";

            return query;
        }

        //public IQueryable<T> Get()
        //{
        //    throw new NotImplementedException();
        //}

        public virtual async Task<PagedData<T>> GetPaged(QueryViewModel query)
        {
            throw new NotImplementedException();
        }
    }
}
