using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace PerfJournal.Client
{
    public class Journal
    {
        #region Private Fields
        private string _url;
        private static readonly HttpClient _httpClient = new HttpClient();
        #endregion

        #region Public Properties
        public string Url => _url;
        #endregion

        #region Constructors
        public Journal(string url)
        {
            _url = url;
        }
        #endregion

        #region Public Methods
        public async Task<bool> HasProjectAsync(string projectName)
        {
            return await Project.HasProjectAsync(projectName, _httpClient, GetApiUrl(PJObject.Project));
        }

        public async Task<Project> GetProjectAsync(string projectName)
        {
            return await Project.GetProjectAsync(projectName, _httpClient, GetApiUrl(PJObject.Project));
        }

        public async Task<Project> CreateProjectAsync(string projectName)
        {
            return await Project.CreateProjectAsync(projectName, _httpClient, GetApiUrl(PJObject.Project));
        }

        public async Task<bool> HasTestAsync(string testName, int projectId)
        {
            return await Test.HasTestAsync(testName, projectId, _httpClient, GetApiUrl(PJObject.Test));
        }

        public async Task<Test> GetTestAsync(string testName, int projectId)
        {
            return await Test.GetTestAsync(testName, projectId, _httpClient, GetApiUrl(PJObject.Test));
        }

        public async Task<Test> CreateTestAsync(string testName, int projectId)
        {
            return await Test.CreateTestAsync(testName, projectId, _httpClient, GetApiUrl(PJObject.Test));
        }
        #endregion

        #region Private Methods
        private string GetApiUrl(PJObject pjObject)
        {
            switch (pjObject)
            {
                case PJObject.Project:
                    return _url + "/" + Constants.PROJECTS;
                case PJObject.Tester:
                    return _url + "/" + Constants.TESTERS;
                case PJObject.Test:
                    return _url + "/" + Constants.TESTS;
                case PJObject.Build:
                    return _url + "/" + Constants.BUILDS;
                case PJObject.Environment:
                    return _url + "/" + Constants.ENVIRONMENTS;
                case PJObject.TestResult:
                    return _url + "/" + Constants.TEST_RESULTS;
                case PJObject.Unknown:
                default:
                    throw new InvalidOperationException("Unknown object type");
            }
        }
        #endregion

    }
}
