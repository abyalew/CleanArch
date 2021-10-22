using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArch.Application.IntegrationTest
{
    using static Testing;
    public class TestBase
    {
        [SetUp]
        public void Setup()
        {
            ResetDatabaseState();
        }
    }
}
