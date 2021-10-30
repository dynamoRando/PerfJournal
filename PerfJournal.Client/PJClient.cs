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
        private Journal _journal;
        private string _projectName;
        private bool _autoCreateObjects;

        private Project _currentProject;

        // testName (string), testId (int)
        private Dictionary<string, int> _tests;
        #endregion

        #region Public Properties
        public string Url => _journal.Url;
        public string Project => _projectName;

        /// <summary>
        /// Will attempt to create entries if they do not exist
        /// </summary>
        public bool CreateNewObjects => _autoCreateObjects;
        #endregion

        #region Constructors
        /// <summary>
        /// Constructs a new PerfJournal Client to make calls at the specified url API, i.e. "http://localhost:5000"
        /// </summary>
        /// <param name="url">The url, i.e. http://localhost</param>
        /// <param name="portNumber">The port number of the API</param>
        /// <param name="projectName">The name of the project this client will be saving results for</param>
        /// <param name="autoCreateObjects">Configures the client to automatically create the objct if it doesn't exist in the journal</param>
        public PJClient(string url, int portNumber, string projectName, bool autoCreateObjects)
        {
            _journal = new Journal(url + ":" + portNumber.ToString());

            _tests = new Dictionary<string, int>();
            _projectName = projectName;
            _autoCreateObjects = autoCreateObjects;
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Checks the Journal for <see cref="Project"/> and constructs the Project if configured by <see cref="CreateNewObjects"></see>.
        /// </summary>
        /// <returns></returns>
        public async Task<Project> ConfigureProjectAsync()
        {
            if (_journal.HasProjectAsync(_projectName).Result)
            {
                _currentProject = await _journal.GetProjectAsync(_projectName);
            }
            else
            {
                if (_autoCreateObjects)
                {
                    _currentProject = await _journal.CreateProjectAsync(_projectName);
                }
            }

            return _currentProject;
        }

        public async Task<Test> ConfigureTestAsync(string testName)
        {
            Test test = new Test { Id = 0, TestName = String.Empty, Project = _currentProject };
            if (_journal.HasTestAsync(testName, _currentProject.Id).Result)
            {
                test = await _journal.GetTestAsync(testName, _currentProject.Id);
                test.Project = _currentProject;
            }
            else
            {
                if (_autoCreateObjects)
                {
                    test = await _journal.CreateTestAsync(testName, _currentProject.Id);
                    test.Project = _currentProject;
                }
            }

            return test;
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
