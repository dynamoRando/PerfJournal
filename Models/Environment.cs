using System;
using System.Collections.Generic;

#nullable disable

namespace PerfJournal
{
    public partial class Environment
    {
        public Environment()
        {
            TestResults = new HashSet<TestResult>();
        }

        public int Id { get; set; }
        public string EnvironmentName { get; set; }

        public virtual ICollection<TestResult> TestResults { get; set; }
    }
}
