using Dapper;
using Listening.Server.Entities.Specialized.Result;
using Listening.Server.Entities.Specialized.ServiceModels;
using Listening.Infrastructure.Repositories.Abstract;
using Listening.Core.ViewModels.Text;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Listening.Server.Repositories.Postgres
{
    public class ErrorForSeparatedRepository : BaseErrorRepository<ErrorForSeparated, long>,
                                                IErrorsForSeparatedRepository
    {
        private const string ErrorValue = nameof(ErrorForSeparated.ErrorValue);
        private const string ParagraphIndex = nameof(ErrorForSeparated.ParagraphIndex);
        private const string WordIndex = nameof(ErrorForSeparated.WordIndex);

        public ErrorForSeparatedRepository(IConfiguration configuration)
            : base(configuration)
        {
            TableName = "Listening_ErrorsForSeparated";
        }

        public async Task<ErrorForSeparatedDto[]> GetErrors(long resultId)
        {
            var query = $@"select ""{ParagraphIndex}"", ""{WordIndex}"", ""{ErrorValue}""
                            from public.""{TableName}"" 
                            where ""{ResultId}""=@{ResultId}";

            using (var connection = Connection)
            {
                var errorsForSeparated = await connection.QueryAsync<ErrorForSeparated>(
                    query, new { ResultId = resultId });

                if (!errorsForSeparated.Any())
                    return new ErrorForSeparatedDto[0];

                return ConvertToDto(errorsForSeparated);
            }
        }

        private ErrorForSeparatedDto[] ConvertToDto(
            IEnumerable<ErrorForSeparated> errorsForSeparated)
        {
            var errorForSeparatedDtoList = new List<ErrorForSeparatedDto>();
            var wordAddresses = errorsForSeparated.Select(x =>
                                new WordAddress
                                {
                                    ParagraphIndex = x.ParagraphIndex,
                                    WordIndex = x.WordIndex
                                })
                                .Distinct(new WordAddress());

            foreach (var wordAddress in wordAddresses)
            {
                var errors = errorsForSeparated.Where(x => x.WordIndex == wordAddress.WordIndex
                      && x.ParagraphIndex == wordAddress.ParagraphIndex)
                    .Select(x => x.ErrorValue).Distinct().ToArray();

                errorForSeparatedDtoList.Add(new ErrorForSeparatedDto
                {
                    WordAddress = wordAddress,
                    Errors = errors
                });
            }

            return errorForSeparatedDtoList.ToArray();
        }
    }
}
