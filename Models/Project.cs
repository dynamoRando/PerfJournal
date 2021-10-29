using System;
using System.Collections.Generic;

#nullable disable

namespace PerfJournal
{
    public partial class Project
    {
        public Project()
        {
            Builds = new HashSet<Build>();
            TestResults = new HashSet<TestResult>();
            Tests = new HashSet<Test>();
        }

        public int Id { get; set; }
        public string ProjectName { get; set; }

        public virtual ICollection<Build> Builds { get; set; }
        public virtual ICollection<TestResult> TestResults { get; set; }
        public virtual ICollection<Test> Tests { get; set; }
    }
}
