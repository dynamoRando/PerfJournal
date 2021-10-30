using System;
using System.Collections.Generic;

#nullable disable

namespace PerfJournal
{
    public partial class TestResult
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

        public virtual Build Build { get; set; }
        public virtual Environment Environment { get; set; }
        public virtual Project Project { get; set; }
        public virtual Test Test { get; set; }
        public virtual Tester Tester { get; set; }
    }
}
