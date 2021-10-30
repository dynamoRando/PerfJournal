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
    public struct Test
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("projectId")]
        public int ProjectId { get; set; }
        [JsonPropertyName("testName")]
        public string TestName { get; set; }
        [JsonPropertyName("testDescription")]
        public string TestDescription { get; set; }

        public Project Project { get; set; }
        public ICollection<TestResult> TestResults { get; set; }


        public static async Task<bool> HasTestAsync(string testName, int projectId, HttpClient client, string testApiUrl)
        {
            var testsTask = client.GetStreamAsync(testApiUrl);
            var tests = await JsonSerializer.DeserializeAsync<List<Test>>(await testsTask);

            foreach (var test in tests)
            {
                if (string.Equals(test.TestName, testName, StringComparison.OrdinalIgnoreCase) && test.ProjectId == projectId)
                {
                    return true;
                }
            }

            return false;
        }

        public static async Task<Test> GetTestAsync(string testName, int projectId, HttpClient client, string testApiUrl)
        {
            var testsTask = client.GetStreamAsync(testApiUrl);
            var tests = await JsonSerializer.DeserializeAsync<List<Test>>(await testsTask);

            foreach (var test in tests)
            {
                if (string.Equals(test.TestName, testName, StringComparison.OrdinalIgnoreCase) && test.ProjectId == projectId)
                {
                    return test;
                }
            }

            return new Test();
        }

        public static async Task<Test> CreateTestAsync(string testName, int projectId, HttpClient client, string testApiUrl)
        {
            var test = new Test { TestName = testName, ProjectId = projectId };
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var response = await client.PostAsJsonAsync(testApiUrl, test);

            if (response.IsSuccessStatusCode)
            {
                test = JsonSerializer.Deserialize<Test>(response.Content.ReadAsStringAsync().Result);
            }
            else
            {
                test = new Test();
            }

            return test;
        }
    }
}
