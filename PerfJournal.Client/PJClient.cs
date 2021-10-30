using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace PerfJournal.Client
{
    public class PJClient
    {
        #region Private Fields
        private string _fullUrl;
        private string _projectName;
        private bool _autoCreateObjects;
        private static readonly HttpClient _httpClient = new HttpClient();

        private const string PROJECTS = "api/Projects";
        private const string TESTERS = "api/Testers";
        private const string TESTS = "api/Tests";
        private const string BUILDS = "api/Builds";
        private const string ENVIRONMENTS = "api/Environments";
        private const string TEST_RESULTS = "api/TestResults";

        // testName (string), testId (int)
        private Dictionary<string, int> _tests;
        #endregion

        #region Public Properties
        public string Url => _fullUrl;
        public string Project => _projectName;
        #endregion

        #region Constructors
        /// <summary>
        /// Constructs a new PerfJournal Client to make calls at the specified url API, i.e. "http://localhost:5000"
        /// </summary>
        /// <param name="url">The url, i.e. http://localhost</param>
        /// <param name="portNumber">The port number of the API</param>
        /// <param name="projectName">The name of the project this client will be saving results for</param>
        public PJClient(string url, int portNumber, string projectName, bool autoCreateObjects)
        {
            _fullUrl = url + ":" + portNumber.ToString();
            _tests = new Dictionary<string, int>();
            _projectName = projectName;
            _autoCreateObjects = autoCreateObjects;
        }
        #endregion

        #region Public Methods
        public async Task<Project> ConfigureProjectAsync()
        {
            Project project = new Project { Id = 0, ProjectName = String.Empty };
            int maxProjectId = 0;
            string url = _fullUrl + "/" + PROJECTS;

            var projectsTask = _httpClient.GetStreamAsync(url);
            var projects = await JsonSerializer.DeserializeAsync<List<Project>>(await projectsTask);

            foreach (var proj in projects)
            {
                if (proj.Id > maxProjectId)
                {
                    maxProjectId = proj.Id;
                }

                if (string.Equals(proj.ProjectName, _projectName, StringComparison.OrdinalIgnoreCase))
                {
                    project = proj;
                }
            }

            // if we didn't find the project, then if configured, go ahead and create it
            if (project.Id == 0)
            {
                if (_autoCreateObjects)
                {
                    project = new Project { ProjectName = _projectName };
                    _httpClient.DefaultRequestHeaders.Accept.Clear();
                    _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    var response = await _httpClient.PostAsJsonAsync(url, project);

                    if (response.IsSuccessStatusCode)
                    {
                        project = JsonSerializer.Deserialize<Project>(response.Content.ReadAsStringAsync().Result);
                    }
                }
            }

            return project;
        }
        public void SaveResult(Test test, int totalMilliseconds, bool isSuccessful)
        {
            throw new NotImplementedException();
        }

        public void SaveResult(Test test, Tester tester, int totalMilliseconds, bool isSucessful)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Private Methods
        #endregion

    }
}
