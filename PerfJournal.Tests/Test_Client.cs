using System;
using Xunit;
using PerfJournal.Client;

namespace PerfJournal.Tests
{
    public class Test_Client
    {
        private string _url = "http://localhost:7000";
        string _projectName = "Xunit";

        [Fact]
        public async void Test_ExistingProject()
        {
            if (Journal.HasProject(_url, _projectName).Result)
            {
                var projectIdResult = await Journal.GetProjectIdAsync(_url, _projectName);
                Assert.Equal(2, projectIdResult);
            }
            else
            {
                throw new Exception("Project not found");
            }
        }

        [Fact]

        public async void Test_ExistingTest()
        {
            string testName = "TestExample";

            if (Journal.HasTest(_url, _projectName, testName).Result)
            {
                var testResult = await Journal.GetTestIdAsync(_url, _projectName, testName);
                Assert.Equal(2, testResult);
            }
            else
            {
                throw new Exception("Test not found");
            }
        }

        [Fact]
        public async void Test_SaveResult()
        {
            string testName = "TestExample";

            var result = await Journal.SaveResult(_url, _projectName, testName, 4000, true);
            Assert.True(result);
        }

        [Fact]
        public async void Test_NewProject()
        {
            string projectName = "NewProject" + DateTime.Now.ToString();
            var hasProject = await Journal.HasProject(_url, projectName);
            if (!hasProject)
            {
                var result = await Journal.CreateProjectAsync(_url, projectName);
                Assert.True(result);
            }
            else
            {
                throw new Exception("Test already exists");
            }
        }
    }
}
