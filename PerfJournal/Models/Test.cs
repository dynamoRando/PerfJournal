using System;
using System.Collections.Generic;

#nullable disable

namespace PerfJournal
{
    public partial class Test
    {
        public Test()
        {
            TestResults = new HashSet<TestResult>();
        }

        public int Id { get; set; }
        public int ProjectId { get; set; }
        public string TestName { get; set; }
        public string TestDescription { get; set; }

        public virtual Project Project { get; set; }
        public virtual ICollection<TestResult> TestResults { get; set; }
    }
}
