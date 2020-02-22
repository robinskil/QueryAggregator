using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace UnitTests
{
    public class TestBase
    {
        public ICollection<DbContext> contexts;

    }
}
