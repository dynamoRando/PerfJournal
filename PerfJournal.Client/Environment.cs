using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PerfJournal.Client
{
    internal class Environment
    {
        public int Id { get; set; }
        public string EnvironmentName { get; set; }
        public ICollection<TestResult> TestResults { get; set; }
    }
}
