using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PerfJournal.Client
{
    public struct Build
    {
        public int Id;
        public Project Project;
        public int Major;
        public int Minor;
        public int Patch;
    }
}
