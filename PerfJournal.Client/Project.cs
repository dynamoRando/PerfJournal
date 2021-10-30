using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
    }
}
