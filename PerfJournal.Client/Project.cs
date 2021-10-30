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
    public struct Project
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("projectName")]
        public string ProjectName { get; set; }
        [JsonPropertyName("builds")]
        public ICollection<Build> Builds { get; set; }
        [JsonPropertyName("testResults")]
        public ICollection<TestResult> TestResults { get; set; }
        [JsonPropertyName("tests")]
        public ICollection<Test> Tests { get; set; }

        public static async Task<bool> HasProjectAsync(string projectName, HttpClient client, string projectApiUrl)
        {
            var projectsTask = client.GetStreamAsync(projectApiUrl);
            var projects = await JsonSerializer.DeserializeAsync<List<Project>>(await projectsTask);

            foreach (var proj in projects)
            {
                if (string.Equals(proj.ProjectName, projectName, StringComparison.OrdinalIgnoreCase))
                {
                    return true;
                }
            }

            return false;
        }

        public static async Task<Project> GetProjectAsync(string projectName, HttpClient client, string projectApiUrl)
        {
            var projectsTask = client.GetStreamAsync(projectApiUrl);
            var projects = await JsonSerializer.DeserializeAsync<List<Project>>(await projectsTask);

            foreach (var proj in projects)
            {
                if (string.Equals(proj.ProjectName, projectName, StringComparison.OrdinalIgnoreCase))
                {
                    return proj;
                }
            }

            return new Project();
        }

        public static async Task<Project> CreateProjectAsync(string projectName, HttpClient client, string projectApiUrl)
        {
            var project = new Project { ProjectName = projectName };
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var response = await client.PostAsJsonAsync(projectApiUrl, project);

            if (response.IsSuccessStatusCode)
            {
                project = JsonSerializer.Deserialize<Project>(response.Content.ReadAsStringAsync().Result);
            }
            else
            {
                project = new Project();
            }

            return project;
        }
    }
}
