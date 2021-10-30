using System;
using Xunit;
using PerfJournal.Client;

namespace PerfJournal.Tests
{
    public class Test_Client
    {
        [Fact]
        public void Test_NewProject()
        {
            string devserver = "http://localhost";
            int port = 7000;

            var client = new PJClient(devserver, port, "XUnit", true);
            var result = client.ConfigureProjectAsync();

            Assert.Equal(2, result.Result.Id);
        }
    }
}
