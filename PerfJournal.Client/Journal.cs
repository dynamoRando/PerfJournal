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
    public static class Journal
    {
        public static async Task<bool> HasTest(string journalUrl, string projectName, string testName)
        {
            JournalApi.Url = journalUrl;
            return await JournalApi.HasTestAsync(projectName, testName);
        }

        public static async Task<bool> CreateProjectAsync(string journalUrl, string projectName)
        {
            JournalApi.Url = journalUrl;
            var result = await JournalApi.CreateProjectAsync(projectName);

            if (result.Id != 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static async Task<bool> HasProject(string journalUrl, string projectName)
        {
            JournalApi.Url = journalUrl;
            return await JournalApi.HasProjectAsync(projectName);
        }

        public static async Task<int> GetProjectIdAsync(string journalUrl, string projectName)
        {
            JournalApi.Url = journalUrl;
            var result = await JournalApi.GetProjectAsync(projectName);
            return result.Id;
        }

        public static async Task<int> GetTestIdAsync(string journalUrl, string projectName, string testName)
        {
            JournalApi.Url = journalUrl;
            var projectResult = await JournalApi.GetProjectAsync(projectName);
            var result = await JournalApi.GetTestAsync(testName, projectResult.Id);
            return result.Id;
        }

        public static async Task<bool> SaveResult(string journalUrl, string projectName, string testName, int totalTimeInMilliseconds, bool isSuccessful)
        {
            JournalApi.Url = journalUrl;
            if (JournalApi.HasProjectAsync(projectName).Result)
            {
                var projectResult = await JournalApi.GetProjectAsync(projectName);
                int projectId = projectResult.Id;
                if (JournalApi.HasTestAsync(testName, projectId).Result)
                {
                    var testResult = await JournalApi.GetTestAsync(testName, projectId);
                    int testId = testResult.Id;
                    return await JournalApi.SaveTestResult(projectId, testId, totalTimeInMilliseconds, isSuccessful);
                }
            }

            return false;
        }
    }
}
