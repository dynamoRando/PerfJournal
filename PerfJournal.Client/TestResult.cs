using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PerfJournal.Client
{
    public struct TestResult
    {
        public int Id { get; set; }
        public int TestId { get; set; }
        public int ProjectId { get; set; }
        public int? BuildId { get; set; }
        public int? EnvironmentId { get; set; }
        public int? TesterId { get; set; }
        public DateTime TestDate { get; set; }
        public int TotalTimeInMilliseconds { get; set; }
        public string Notes { get; set; }
        public bool IsSuccessful { get; set; }

        public Build Build { get; set; }
        public Environment Environment { get; set; }
        public Project Project { get; set; }
        public Test Test { get; set; }
        public Tester Tester { get; set; }
    }
}
