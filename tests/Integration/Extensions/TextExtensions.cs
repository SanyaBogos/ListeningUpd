using Listening.Server.Entities.Specialized.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Infrastructure.Helpers;

namespace Integration.Extensions
{
    internal static class TextExtensions
    {
        internal static async Task InsertTexts(this DatabaseFixture fixture)
        {
            fixture.Texts = fixture.GenerateTexts(20, DatabaseFixture.FilteringTestLabel);
            var pagingTexts = fixture.GenerateTexts(6, $"{DatabaseFixture.PagingTestLabel}test");
            fixture.AudioCheckTexts = fixture.GenerateTexts(3, DatabaseFixture.CheckAudioLabel, DatabaseFixture.AudioNameBase);
            fixture.VideoCheckTexts = fixture.GenerateTexts(3, DatabaseFixture.CheckVideoLabel, videoName: DatabaseFixture.VideoNameBase);
            fixture.Texts.AddRange(pagingTexts);
            fixture.Texts.AddRange(fixture.AudioCheckTexts);
            fixture.Texts.AddRange(fixture.VideoCheckTexts);

            var existedItemsAsync = fixture.Texts.Select(async x =>
                            await fixture.TextRepository.GetById(x.Id));
            var existedItems = (await Task.WhenAll(existedItemsAsync))
                .Where(x => x == null);
            await fixture.TextRepository.Insert(fixture.Texts.Except(existedItems));
        }

        internal static List<Text> GenerateTexts(this DatabaseFixture fixture, int count = 5, string title = "",
            string audioName = "", string videoName = "")
        {
            var texts = new List<Text>();

            for (int i = 0; i < count; i++)
            {
                var wordsCount = RandomHelper.Quantity() % 50 + 3;
                var wordsInParagraphs = RandomHelper.StringsArrays(wordsCount);
                var quantity = wordsInParagraphs.First().Sum(x => x.Length);
                var titleValue = string.IsNullOrEmpty(title)
                    ? RandomHelper.String()
                    : $"{title}{i}";

                var text = new Text
                {
                    Id = MongoDB.Bson.ObjectId.GenerateNewId(),
                    AudioName = RandomHelper.String(),
                    Title = titleValue,
                    SubTitle = $"{DatabaseFixture.FilteringTestLabel}{RandomHelper.String()}",
                    SymbolsCount = quantity,
                    Country = RandomHelper.CountryCode(),
                    WordsInParagraphs = wordsInParagraphs
                };

                if (!string.IsNullOrEmpty(audioName))
                    text.AudioName = $"{audioName}{i}.{DatabaseFixture.AudioType}";
                else
                    text.VideoName = $"{videoName}{i}.{DatabaseFixture.VideoType}";

                texts.Add(text);
            }

            return texts;
        }

        internal static async Task CleanupTexts(this DatabaseFixture fixture)
        {
            var toDelete = fixture.TextRepository.Get()
                .Where(x => string.IsNullOrEmpty(x.Title)
                    || x.Title.Contains(DatabaseFixture.FilteringTestLabel)
                    || x.Title.Contains(DatabaseFixture.PagingTestLabel)
                    || x.Title.Contains(DatabaseFixture.SortingTestLabel)
                    || x.Title.Contains(DatabaseFixture.CheckAudioLabel)
                    || x.Title.Contains(DatabaseFixture.CheckVideoLabel)
                    || x.Title.Contains(DatabaseFixture.OtherTestLabel)
                    || x.Title.Contains(DatabaseFixture.TestLabel)
                    )
                .ToArray();

            await fixture.TextRepository.Delete(toDelete.Select(x => x.Id));
        }

        internal static async Task DeleteTexts(this DatabaseFixture fixture)
        {
            await fixture.TextRepository.Delete(
                fixture.Texts.Except(fixture.TextsCollisions).Select(x => x.Id));
        }

        internal static List<Text> BuildChangesForTexts(this DatabaseFixture fixture, List<Text> texts)
        {
            foreach (var text in texts)
            {
                var wordsCount = RandomHelper.Quantity() % 50 + 3;
                var wordsInParagraphs = RandomHelper.StringsArrays(wordsCount);
                var quantity = wordsInParagraphs.First().Sum(x => x.Length);

                text.AudioName = RandomHelper.String();
                text.Title = $"{DatabaseFixture.FilteringTestLabel}{RandomHelper.String()}";
                text.SubTitle = $"{DatabaseFixture.FilteringTestLabel}{RandomHelper.String()}";
                text.SymbolsCount = quantity;
                text.VideoName = RandomHelper.String();
                text.Country = RandomHelper.CountryCode();
                text.WordsInParagraphs = wordsInParagraphs;
            }

            return texts;
        }
    }
}
