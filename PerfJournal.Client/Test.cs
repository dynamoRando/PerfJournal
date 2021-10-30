using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
    }
}
