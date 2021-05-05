using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using BDDExampleDomain;
using FluentAssertions;
using Gherkin.Ast;
using Xunit.Gherkin.Quick;

namespace BDDExampleAPI.Tests.Automation
{
    [FeatureFile("./Specifications/Qualifications.feature")]
    public class QualificationsIntegrationTests : ApiBase, IDisposable
    {
        private readonly List<HttpResponseMessage> _httpResponses = new List<HttpResponseMessage>();
        private Qualification _currentQualification;

        [Given("there are no saved qualifications")]
        public async Task There_Are_No_Qualifications() => (await GetNumberOfQualificationsAsync()).Should().Be(0);

        [Given("the following qualifications have already been added")]
        [When("the following qualifications are added")]
        public async Task When_Qualifications_Added(DataTable qualifications)
        {
            foreach (var row in qualifications.Rows.Skip(1))
            {
                var cells = row.Cells.ToArray();

                var qualification = new Qualification
                {
                    Id = Guid.Parse(cells[0].Value),
                    CourseId = Guid.NewGuid(),
                    CourseName = cells[2].Value,
                    Awarded = DateTime.Now,
                    StudentId = Guid.NewGuid()
                };

                _httpResponses.Add(await AddQualificationAsync(qualification));
            }
        }

        [When(@"the qualification with Id (.+) is requested")]
        public async Task Request_Qualification(string id)
        {
            var response = await _client.GetAsync("Qualifications/" + id);

            if (response.IsSuccessStatusCode)
            {
                _currentQualification = await ConvertResponseContentToObjectAsync<Qualification>(response);
            }
        }

        [Then(@"the number of saved qualifications should be (.+)")]
        public async Task Should_Be_N_Saved_Qualifications(int expected)
        {
            var actualNumberOfQualifications = await GetNumberOfQualificationsAsync();

            actualNumberOfQualifications.Should().Be(expected);
        }

        [Then("the number of HTTP BadRequest status codes should be (.+)")]
        public void Should_Be_N_BadRequests(int expected)
            => _httpResponses.Count(r => r.StatusCode == HttpStatusCode.BadRequest).Should().Be(expected);

        [Then(@"the name of the course in the qualification returned should be (.+)")]
        public void Qualification_Name_Should_Be(string expected)
            => _currentQualification.CourseName.Should().Match(expected);

        private async Task<HttpResponseMessage> AddQualificationAsync(Qualification item)
            => await _client.PostAsync("Qualifications/", ConvertObjectToHttpContent(item));

        private async Task<int> GetNumberOfQualificationsAsync()
        {
            var response = await _client.GetAsync("Qualifications/");

            var qualificationsHistory = await ConvertResponseContentToObjectAsync<QualificationsHistory>(response);

            return qualificationsHistory.Qualifications.Count;
        }

        public override void Dispose()
        {
            _httpResponses.Clear();
            base.Dispose();
        }
    }
}
