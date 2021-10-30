using System;
using System.Collections.Generic;

#nullable disable

namespace PerfJournal
{
    public partial class Tester
    {
        public Tester()
        {
            TestResults = new HashSet<TestResult>();
        }

        public int Id { get; set; }
        public string TesterName { get; set; }

        public virtual ICollection<TestResult> TestResults { get; set; }
    }
}
