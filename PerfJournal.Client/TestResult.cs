using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace PerfJournal.Client
{
    internal class TestResult
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("testId")]
        public int TestId { get; set; }
        [JsonPropertyName("projectId")]
        public int ProjectId { get; set; }
        [JsonPropertyName("buildId")]
        public int? BuildId { get; set; }
        [JsonPropertyName("environmentId")]
        public int? EnvironmentId { get; set; }
        [JsonPropertyName("testerId")]
        public int? TesterId { get; set; }
        [JsonPropertyName("testDate")]
        public DateTime TestDate { get; set; }
        [JsonPropertyName("totalTimeInMilliseconds")]
        public int TotalTimeInMilliseconds { get; set; }
        [JsonPropertyName("notes")]
        public string Notes { get; set; }
        [JsonPropertyName("isSuccessful")]
        public bool IsSuccessful { get; set; }

        public Build Build { get; set; }
        public Environment Environment { get; set; }
        public Project Project { get; set; }
        public Test Test { get; set; }
        public Tester Tester { get; set; }

        public static async Task<bool> SaveTestResult(int projectId, int testId, int totalTimeInMilliseconds, bool isSuccessful, HttpClient client, string testApiUrl)
        {
            var testResult = new TestResult
            {
                ProjectId = projectId,
                TestId = testId,
                TotalTimeInMilliseconds = totalTimeInMilliseconds,
                IsSuccessful = isSuccessful,
                TestDate = DateTime.Now
            };

            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var response = await client.PostAsJsonAsync(testApiUrl, testResult);

            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
