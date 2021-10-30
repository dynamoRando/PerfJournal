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
    internal static class JournalApi
    {
        #region Private Fields
        private static readonly HttpClient _httpClient = new HttpClient();
        #endregion

        #region Public Properties
        public static string Url { get; set; }
        #endregion

        #region Constructors
        #endregion

        #region Public Methods
        public static async Task<bool> HasProjectAsync(string projectName)
        {
            return await Project.HasProjectAsync(projectName, _httpClient, GetApiUrl(PJObject.Project));
        }

        public static async Task<Project> GetProjectAsync(string projectName)
        {
            return await Project.GetProjectAsync(projectName, _httpClient, GetApiUrl(PJObject.Project));
        }

        public static async Task<Project> CreateProjectAsync(string projectName)
        {
            return await Project.CreateProjectAsync(projectName, _httpClient, GetApiUrl(PJObject.Project));
        }

        public static async Task<bool> HasTestAsync(string testName, int projectId)
        {
            return await Test.HasTestAsync(testName, projectId, _httpClient, GetApiUrl(PJObject.Test));
        }

        public static async Task<bool> HasTestAsync(string projectName, string testName)
        {
            int projectId = 0;
            if (HasProjectAsync(projectName).Result)
            {
                projectId = GetProjectAsync(projectName).Result.Id;
            }

            return await Test.HasTestAsync(testName, projectId, _httpClient, GetApiUrl(PJObject.Test));
        }

        public static async Task<Test> GetTestAsync(string testName, int projectId)
        {
            return await Test.GetTestAsync(testName, projectId, _httpClient, GetApiUrl(PJObject.Test));
        }

        public static async Task<Test> CreateTestAsync(string testName, int projectId)
        {
            return await Test.CreateTestAsync(testName, projectId, _httpClient, GetApiUrl(PJObject.Test));
        }

        public static async Task<bool> SaveTestResult(int projectId, int testId, int totalTimeInMilliseconds, bool isSuccessful)
        {
            return await TestResult.SaveTestResult(projectId, testId, totalTimeInMilliseconds, isSuccessful, _httpClient, GetApiUrl(PJObject.TestResult));
        }
        #endregion

        #region Private Methods
        private static string GetApiUrl(PJObject pjObject)
        {
            switch (pjObject)
            {
                case PJObject.Project:
                    return Url + "/" + Constants.PROJECTS;
                case PJObject.Tester:
                    return Url + "/" + Constants.TESTERS;
                case PJObject.Test:
                    return Url + "/" + Constants.TESTS;
                case PJObject.Build:
                    return Url + "/" + Constants.BUILDS;
                case PJObject.Environment:
                    return Url + "/" + Constants.ENVIRONMENTS;
                case PJObject.TestResult:
                    return Url + "/" + Constants.TEST_RESULTS;
                case PJObject.Unknown:
                default:
                    throw new InvalidOperationException("Unknown object type");
            }
        }
        #endregion

    }
}
