using Dapper;
using Listening.Core.Entities.Custom;
using Listening.Core.Entities.Specialized.Chat;
using Listening.Core.ViewModels.Chat;
using Listening.Infrastructure.Repositories.Abstract;
using Listening.Server.Repositories.Postgres;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Threading.Tasks;

namespace Listening.Infrastructure.Repositories.Postgres
{
    public class ChatRepository : BasePostgresRepository<Dialogue, long>, IChatRepository
    {
        private const string UserTableName = "AspNetUsers";
        private readonly ILogger<ChatRepository> _logger;

        public ChatRepository(
            IConfiguration configuration,
            ILogger<ChatRepository> logger)
            : base(configuration)
        {
            _logger = logger;
        }

        public async Task InsertMessageAsync(MessageForSignalRSaveDto messageForSaveDto)
        {
            var query = $@"insert into public.""{TableName}"" 
                    (""{nameof(Dialogue.FromUserId)}"",""{nameof(Dialogue.ToUserId)}"",""{nameof(Dialogue.Message)}"", 
                        ""{nameof(Dialogue.Time)}"")
                select @fromUserId, ""{nameof(ApplicationUser.Id)}"", @message, now() from public.""{UserTableName}""
   		            where ""{nameof(ApplicationUser.SignalRId)}"" = @toUserSignalRId";

            using (var connection = Connection)
            {
                await connection.ExecuteAsync(query,
                    new
                    {
                        messageForSaveDto.FromUserId,
                        messageForSaveDto.ToUserSignalRId,
                        messageForSaveDto.Message
                    });
            }
        }

        public async Task<string> InsertMessageReturnSignalRReceiverIdAsync(MessageForSaveDto messageForSaveDto)
        {
            var query = $@"insert into public.""{TableName}"" 
                    (""{nameof(Dialogue.FromUserId)}"",""{nameof(Dialogue.ToUserId)}"",""{nameof(Dialogue.Message)}"", 
                        ""{nameof(Dialogue.Time)}"")
                VALUES (@fromUserId, @toUserId, @message, now());

                SELECT ""SignalRId"" FROM public.""{UserTableName}""
                    where ""Id"" = @toUserId;";

            using (var connection = Connection)
            {
                var result = await connection.QuerySingleAsync<string>(query,
                    new
                    {
                        messageForSaveDto.FromUserId,
                        messageForSaveDto.ToUserId,
                        messageForSaveDto.Message
                    });

                return result;
            }
        }

        public async Task<UserMessageDto[]> GetDialogueMessagesAsync(
            MessagesParamsDto lastMessagesParamsDto)
        {
            var subQuery = lastMessagesParamsDto.LastId != 0 ? @"and ""Id"" < @lastId" : "";
            var query = $@"
                with DialogueMessages as (
                SELECT ""{nameof(Dialogue.Id)}"",                 
                    CASE WHEN ""{nameof(Dialogue.FromUserId)}"" = @fromUserId THEN true
                            ELSE false
                       END as IsMine,
                	""{nameof(Dialogue.Message)}"", ""{nameof(Dialogue.Time)}""
                  FROM public.""{TableName}""
                  where (""{nameof(Dialogue.FromUserId)}"" = @fromUserId or ""{nameof(Dialogue.FromUserId)}"" = @toUserId)                
                    and (""{nameof(Dialogue.ToUserId)}"" = @fromUserId or ""{nameof(Dialogue.ToUserId)}"" = @toUserId)
                    {subQuery}
                  order by ""{nameof(Dialogue.Time)}"" desc
                  --offset @countOfMessages * @page
                  limit @countOfMessages
                )
                
                select * from DialogueMessages
                order by ""{nameof(Dialogue.Time)}"" asc";

            using (var connection = Connection)
            {
                var messages = await connection.QueryAsync<UserMessageDto>(query,
                    new
                    {
                        lastMessagesParamsDto.FromUserId,
                        lastMessagesParamsDto.ToUserId,
                        lastMessagesParamsDto.CountOfMessages,
                        lastMessagesParamsDto.LastId
                    });

                return messages.ToArray();
            }
        }

        public async Task<long> InsertMessageReturnsIdAsync(MessageForSignalRSaveDto messageForSaveDto)
        {
            var query = $@"insert into public.""{TableName}"" 
                    (""{nameof(Dialogue.FromUserId)}"",""{nameof(Dialogue.ToUserId)}"",""{nameof(Dialogue.Message)}"", 
                        ""{nameof(Dialogue.Time)}"")
                select @fromUserId, ""{nameof(ApplicationUser.Id)}"", @message, now() from public.""{UserTableName}""
   		            where ""{nameof(ApplicationUser.SignalRId)}"" = @toUserSignalRId
                returning ""{nameof(ApplicationUser.Id)}"";";

            using (var connection = Connection)
            {
                var resultId = await connection.QuerySingleAsync<long>(query,
                    new
                    {
                        messageForSaveDto.FromUserId,
                        messageForSaveDto.ToUserSignalRId,
                        messageForSaveDto.Message
                    });

                return resultId;
            }
        }
    }
}
