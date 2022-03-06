using Integration.Extensions;
using Listening.Core.Entities.Custom;
using Listening.Core.Entities.Specialized.Feedback;
using Listening.Core.ViewModels.AccountViewModels;
using Listening.Infrastructure;
using Listening.Infrastructure.Repositories.Postgres;
using Listening.Server.Entities.Specialized.Result;
using Listening.Server.Entities.Specialized.Text;
using Listening.Server.Repositories.Mongo;
using Listening.Server.Repositories.Postgres;
using Listening.Web;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Integration
{
    public class DatabaseFixture : IDisposable
    {
        internal RegisterViewModel NewUserRegisterVM;
        internal List<Text> TextsCollisions;
        internal List<Text> AudioCheckTexts;
        internal List<Text> VideoCheckTexts;
        internal long MaxResultId;
        internal long MaxFeedbackId;

        internal string AudioPath;
        internal string VideoPath;
        internal string OcrImagePath;
        internal string OcrImageName;
        internal string OcrResultPath;
        internal string ResourcesPath;

        internal readonly ResultRepository ResultRepository;
        internal readonly FeedbackRepository FeedbackRepository;
        internal readonly TextsMongoRepository TextRepository;

        internal const string Resources = "Resources";
        internal const string Bin = "bin";
        internal const string TestLabel = "[Test]";
        internal const string TestLabelEnhanced = "[DefaultTestLabel{0}]";
        internal const string TestLabelSimple = "DefaultTestLabel";
        internal const string Unexisted = "Unexisted";
        internal const string OcrFileName = "OCR_Check.png";
        internal const string OtherTestLabel = "[OtherTest]";
        internal const string FilteringTestLabel = "[Filtering]";
        internal const string PagingTestLabel = "[Paging]";
        internal const string SortingTestLabel = "[Sorting]";
        internal const string CheckAudioLabel = "[CheckAudio]";
        internal const string CheckVideoLabel = "[CheckVideo]";
        internal const string AudioNameBase = "ExistedAudio";
        internal const string VideoNameBase = "ExistedVideo";
        internal const string UnexistedAudioNameBase = "unexistedAudio";
        internal const string UnexistedVideoNameBase = "unexistedVideo";
        internal const string AudioType = "mp3";
        internal const string VideoType = "mp4";

        public IConfiguration Configuration { get; set; }

        public TestServer Server { get; set; }
        public HttpClient AdminClient { get; set; }
        public HttpClient UserClient { get; set; }
        public HttpClient AnonymousClient { get; set; }
        public ApplicationUser NewUser { get; set; }
        public List<Result> Results { get; set; }
        public List<Text> Texts { get; set; }
        public List<Feedback> Feedbacks { get; set; }

        public string CurrentDirectory { get; set; }

        public DatabaseFixture()
        {
            ResourcesPath = Path.Combine(
                Directory.GetCurrentDirectory().Split(Bin).First(),
                Resources);
            CurrentDirectory = WebConfig.Instance.CurrentDirectory;
            Configuration = WebConfig.Instance.Configuration;

            var audioFolderName = Configuration["Data:FileStorage:AudioPath"];
            var videoFolderName = Configuration["Data:FileStorage:VideoPath"];
            var ocrAppPath = Configuration["Data:FileStorage:PictureForRecognition"];
            var recognitionResultPath = Configuration["Data:FileStorage:RecognitionResults"];

            AudioPath = $@"{CurrentDirectory}/wwwroot{audioFolderName}";
            VideoPath = $@"{CurrentDirectory}/wwwroot{videoFolderName}";
            OcrImagePath = $@"{CurrentDirectory}/wwwroot{ocrAppPath}";
            OcrResultPath = $@"{CurrentDirectory}/wwwroot{recognitionResultPath}";

            this.CreateStandartFolderIfNotExists();

            var host = new WebHostBuilder()
                .UseConfiguration(Configuration)
                .UseContentRoot(CurrentDirectory)
                .UseEnvironment("Development")
                .UseStartup<Startup>();

            Server = new TestServer(host);
            AdminClient = Server.CreateClient();
            UserClient = Server.CreateClient();
            AnonymousClient = Server.CreateClient();

            ResultRepository = new ResultRepository(Configuration);
            FeedbackRepository = new FeedbackRepository(Configuration);
            TextRepository = new TextsMongoRepository(Configuration);

            // to avoid possible collisions of id generating by mongo 
            // I store ids, which has been already generated
            TextsCollisions = new List<Text>();

            this.Initialize().GetAwaiter().GetResult();
        }
        public void Dispose()
        {
            CleanupAll().GetAwaiter().GetResult();
        }

        private async Task CleanupAll()
        {
            this.CleanFiles();
            await this.DeleteResults();
            await this.DeleteTexts();
            await this.DeleteNewUser();

            await ResultRepository.DeleteResultsAfterId(MaxResultId, true);
            await FeedbackRepository.DeleteResultsAfterId(MaxFeedbackId, true);
            AdminClient.Dispose();
            UserClient.Dispose();
            AnonymousClient.Dispose();
            Server.Dispose();
        }
    }
}
