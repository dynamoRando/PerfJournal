using System;
using System.Collections.Generic;

#nullable disable

namespace PerfJournal
{
    public partial class Build
    {
        public Build()
        {
            TestResults = new HashSet<TestResult>();
        }

        public int Id { get; set; }
        public int ProjectId { get; set; }
        public int Major { get; set; }
        public int Minor { get; set; }
        public int Patch { get; set; }

        public virtual Project Project { get; set; }
        public virtual ICollection<TestResult> TestResults { get; set; }
    }
}
