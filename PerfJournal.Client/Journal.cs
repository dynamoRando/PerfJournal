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
        public static async Task<bool> HasTestAsync(string journalUrl, string projectName, string testName)
        {
            JournalApi.Url = journalUrl;
            return await JournalApi.HasTestAsync(projectName, testName);
        }

        public static async Task<bool> HasTestAsync(string journalUrl, int projectId, string testName)
        {
            JournalApi.Url = journalUrl;
            return await JournalApi.HasTestAsync(projectId, testName);
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

        public static async Task<bool> CreateTestAsync(string journalUrl, string projectName, string testName)
        {
            JournalApi.Url = journalUrl;

            var project = await JournalApi.GetProjectAsync(projectName);
            var test = await JournalApi.CreateTestAsync(testName, project.Id);

            if (test.Id != 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static async Task<bool> CreateTestAsync(string journalUrl, int projectId, string testName)
        {
            JournalApi.Url = journalUrl;

            var test = await JournalApi.CreateTestAsync(testName, projectId);

            if (test.Id != 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static async Task<bool> HasProjectAsync(string journalUrl, string projectName)
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

        public static async Task<int> GetTestIdAsync(string journalUrl, int projectId, string testName)
        {
            JournalApi.Url = journalUrl;
            var result = await JournalApi.GetTestAsync(testName, projectId);
            return result.Id;
        }

        public static async Task<bool> SaveResultAsync(string journalUrl, string projectName, string testName, int totalTimeInMilliseconds, bool isSuccessful)
        {
            JournalApi.Url = journalUrl;
            if (JournalApi.HasProjectAsync(projectName).Result)
            {
                var projectResult = await JournalApi.GetProjectAsync(projectName);
                int projectId = projectResult.Id;
                if (JournalApi.HasTestAsync(projectId, testName).Result)
                {
                    var testResult = await JournalApi.GetTestAsync(testName, projectId);
                    int testId = testResult.Id;
                    return await JournalApi.SaveTestResult(projectId, testId, totalTimeInMilliseconds, isSuccessful);
                }
            }

            return false;
        }

        public static async Task<bool> SaveResultAsync(string journalUrl, int projectId, int testId, int totalTimeInMilliseconds, bool isSuccessful)
        {
            JournalApi.Url = journalUrl;
            return await JournalApi.SaveTestResult(projectId, testId, totalTimeInMilliseconds, isSuccessful);
        }
    }
}
