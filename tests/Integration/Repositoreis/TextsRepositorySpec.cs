using FluentAssertions;
using Infrastructure;
using Integration;
using Integration.Extensions;
using Listening.Core.ViewModels.Text;
using Listening.Server.Entities.Specialized.Text;
using Listening.Server.Repositories.Mongo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Integration.Repositoreis
{
    [Collection("Database collection")]
    public class TextsRepositorySpec : BaseIntegrationTest<TextsMongoRepository>
    {
        public TextsRepositorySpec(DatabaseFixture fixture)
        {
            _fixture = fixture;
            _sut = new TextsMongoRepository(_fixture.Configuration);
        }

        [Fact]
        public async Task CheckMongoTextsCRUD()
        {
            var texts = _fixture.GenerateTexts(5);

            await _sut.Insert(texts);

            foreach (var text in texts)
                (await _sut.GetById(text.Id)).Should().BeEquivalentTo(text);

            _fixture.BuildChangesForTexts(texts);

            await _sut.Update(texts);

            foreach (var text in texts)
                (await _sut.GetById(text.Id)).Should().BeEquivalentTo(text);

            await _sut.Delete(texts.Select(x => x.Id));
        }

        class FilteringData : EnumerableDataAbstract
        {
            public FilteringData()
            {
                var text = new Text();
                var textsArray = new Text[][] {
                    new Text[] { new Text { Title = $"{DatabaseFixture.FilteringTestLabel}title for text" } },
                    new Text[] { new Text { Title = $"{DatabaseFixture.FilteringTestLabel}another info" } },
                    new Text[] { new Text { Title = $"{DatabaseFixture.FilteringTestLabel}WAS IST DAS" } },
                    new Text[] { new Text { Title = $"{DatabaseFixture.FilteringTestLabel}one two three four" } },
                    new Text[] { new Text { Title = $"{DatabaseFixture.FilteringTestLabel}one two three four" } },
                    new Text[] { new Text { Title = $"{DatabaseFixture.FilteringTestLabel}one two three four" } },
                    new Text[] { new Text { Title = $"{DatabaseFixture.FilteringTestLabel}one two three four" } },
                    new Text[] { new Text { Title = $"{DatabaseFixture.FilteringTestLabel}some title" },
                        new Text { Title = $"{DatabaseFixture.FilteringTestLabel}some title" } },
                    new Text[] { new Text {
                        Title = $"{DatabaseFixture.FilteringTestLabel}complex title",
                        SubTitle = $"some subtitle",
                        Country = "UA",
                        AudioName = "audio123.mp3"
                    } },
                    new Text[] { new Text {
                        Title = $"{DatabaseFixture.FilteringTestLabel}another complex title",
                        SubTitle = $"some subtitle",
                        Country = "UK",
                        VideoName = "video.mp4"
                    } },
                };
                var filteringPropertiesArray = new Dictionary<string, string>[]
                {
                    new Dictionary<string, string> { { nameof( text.Title), "le for te" } },
                    new Dictionary<string, string> { { nameof( text.Title), "another in" } },
                    new Dictionary<string, string> { { nameof( text.Title), "das" } },
                    new Dictionary<string, string> { { nameof( text.Title), "one three" } },
                    new Dictionary<string, string> { { nameof( text.Title), "two four" } },
                    new Dictionary<string, string> { { nameof( text.Title), "two four    " } },
                    new Dictionary<string, string> { { nameof( text.Title), "   two four" } },
                    new Dictionary<string, string> { { nameof( text.Title), "some title" } },
                    new Dictionary<string, string> {
                        { nameof( text.Title), $"complex " },
                        { nameof( text.SubTitle), " subtitle" },
                        { nameof( text.Country), "UA" },
                        { nameof( text.AudioName), "audio123.mp3" },
                    },
                    new Dictionary<string, string> {
                        { nameof( text.Title), $"complex " },
                        { nameof( text.SubTitle), " subtitle" },
                        { nameof( text.Country), "UK" },
                        { nameof( text.VideoName), "video.mp4" },
                    },
                };

                for (int i = 0; i < textsArray.Length; i++)
                    _data.Add(new object[] { textsArray[i],
                        new TextQueryViewModel { FilteringProperties = filteringPropertiesArray[i] } });
            }
        }

        [Theory, ClassData(typeof(FilteringData))]
        public async Task CheckFiltering(Text[] texts, TextQueryViewModel query)
        {
            await _sut.Insert(texts);

            var result = (await _sut.GetPaged(query)).Data;

            result.Should().BeEquivalentTo(texts);

            await _sut.Delete(texts.Select(x => x.Id));
        }

        class SortingData : EnumerableDataAbstract
        {
            public SortingData()
            {
                var text = new Text();
                var textsArray = new Text[][] {
                    new Text[] {
                        new Text { Title = $"{DatabaseFixture.SortingTestLabel}a example for filtering" },
                        new Text { Title = $"{DatabaseFixture.SortingTestLabel}b example for filtering" },
                        new Text { Title = $"{DatabaseFixture.SortingTestLabel}c example for filtering" },
                        new Text { Title = $"{DatabaseFixture.SortingTestLabel}d example for filtering" },
                        new Text { Title = $"{DatabaseFixture.SortingTestLabel}e example for filtering" }
                    },
                    new Text[] {
                        new Text { Title = $"{DatabaseFixture.SortingTestLabel}e example for filtering" },
                        new Text { Title = $"{DatabaseFixture.SortingTestLabel}d example for filtering" },
                        new Text { Title = $"{DatabaseFixture.SortingTestLabel}c example for filtering" },
                        new Text { Title = $"{DatabaseFixture.SortingTestLabel}b example for filtering" },
                        new Text { Title = $"{DatabaseFixture.SortingTestLabel}a example for filtering" },
                    },

                    new Text[] {
                        new Text { Title = DatabaseFixture.SortingTestLabel,
                            SubTitle = $"{DatabaseFixture.SortingTestLabel}e example for filtering" },
                        new Text { Title = DatabaseFixture.SortingTestLabel,
                            SubTitle = $"{DatabaseFixture.SortingTestLabel}d example for filtering" },
                        new Text { Title = DatabaseFixture.SortingTestLabel,
                            SubTitle = $"{DatabaseFixture.SortingTestLabel}c example for filtering" },
                        new Text { Title = DatabaseFixture.SortingTestLabel,
                            SubTitle = $"{DatabaseFixture.SortingTestLabel}b example for filtering" },
                        new Text { Title = DatabaseFixture.SortingTestLabel,
                            SubTitle = $"{DatabaseFixture.SortingTestLabel}a example for filtering" },
                    },
                    new Text[] {
                        new Text { Title = DatabaseFixture.SortingTestLabel,
                            SubTitle = $"{DatabaseFixture.SortingTestLabel}a example for filtering" },
                        new Text { Title = DatabaseFixture.SortingTestLabel,
                            SubTitle = $"{DatabaseFixture.SortingTestLabel}b example for filtering" },
                        new Text { Title = DatabaseFixture.SortingTestLabel,
                            SubTitle = $"{DatabaseFixture.SortingTestLabel}c example for filtering" },
                        new Text { Title = DatabaseFixture.SortingTestLabel,
                            SubTitle = $"{DatabaseFixture.SortingTestLabel}d example for filtering" },
                        new Text { Title = DatabaseFixture.SortingTestLabel,
                            SubTitle = $"{DatabaseFixture.SortingTestLabel}e example for filtering" }
                    },
                    
                    //new Text[] {
                    //    new Text { Country = "TO" },
                    //    new Text { Country = "TT" },
                    //    new Text { Country = "TN" },
                    //    new Text { Country = "TR" },
                    //    new Text { Country = "TM" },
                    //},
                    //new Text[] {
                    //    new Text { Country = "TM" },
                    //    new Text { Country = "TR" },
                    //    new Text { Country = "TN" },
                    //    new Text { Country = "TT" },
                    //    new Text { Country = "TO" },
                    //},
                    //new Text[] {
                    //    new Text { Country = "MX" },
                    //    new Text { Country = "FM" },
                    //    new Text { Country = "MC" },
                    //    new Text { Country = "MN" },
                    //    new Text { Country = "ME" },
                    //},
                    //new Text[] {
                    //    new Text { Country = "ME" },
                    //    new Text { Country = "MN" },
                    //    new Text { Country = "MC" },
                    //    new Text { Country = "FM" },
                    //    new Text { Country = "MX" },
                    //},
                };
                var sortProps = new[] {
                    new { SortName = nameof(text.Title), IsAsc = true },
                    new { SortName = nameof(text.Title), IsAsc = false },
                    new { SortName = nameof(text.SubTitle), IsAsc = true },
                    new { SortName = nameof(text.SubTitle), IsAsc = false },
                    //new { SortName = nameof(text.Country), IsAsc = true },
                    //new { SortName = nameof(text.Country), IsAsc = false },
                    //new { SortName = nameof(text.Country), IsAsc = true },
                    //new { SortName = nameof(text.Country), IsAsc = false },
                };

                _data = new List<object[]>();

                for (int i = 0; i < textsArray.Length; i++)
                    _data.Add(new object[] { textsArray[i],
                        new TextQueryViewModel {
                            SortingName = sortProps[i].SortName,
                            IsAscending = sortProps[i].IsAsc,
                            FilteringProperties = new Dictionary<string, string>
                                { { nameof(text.Title),
                                    $@"{DatabaseFixture.SortingTestLabel.Substring(1, DatabaseFixture.SortingTestLabel.Length -2) }" } }
                        }});
            }
        }

        [Theory, ClassData(typeof(SortingData))]
        public async Task CheckSorting(Text[] texts, TextQueryViewModel query)
        {
            await _sut.Insert(texts.Reverse());

            var result = (await _sut.GetPaged(query)).Data;

            result.Should().BeEquivalentTo(texts);

            await _sut.Delete(texts.Select(x => x.Id));
        }

        class PagingData : EnumerableDataAbstract
        {
            public PagingData()
            {
                var text = new Text();
                var filteringProperty =
                    new Dictionary<string, string> {
                        { nameof(text.Title), DatabaseFixture.PagingTestLabel }
                    };

                var pages = new int[] { 1, 2, 3 };
                var queues = new Queue<int>[] {
                    new Queue<int>(new [] { 0,1 }),
                    new Queue<int>(new [] { 2,3 }),
                    new Queue<int>(new [] { 4,5 }),
                };
                _data = new List<object[]>();

                for (int i = 0; i < pages.Length; i++)
                    _data.Add(new object[] {
                        new TextQueryViewModel {
                            FilteringProperties = filteringProperty,
                            Page = pages[i],
                            ElementsPerPage = 2,
                            SortingName = nameof(text.Title),
                            IsAscending = true
                        },
                        queues[i]
                    });
            }
        }

        [Theory, ClassData(typeof(PagingData))]
        public async Task CheckPaging(TextQueryViewModel query, Queue<int> indexer)
        {
            var result = (await _sut.GetPaged(query)).Data;

            foreach (var item in result)
                item.Title.Should().Contain(
                    indexer.Dequeue().ToString());
        }
    }
}
