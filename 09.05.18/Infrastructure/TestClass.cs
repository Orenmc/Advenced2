using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    class TestClass : ITest2
    {
        private int member1;
        public TestClass()
        {
            member1 = 8;
        }
        public int Member1
        {
            get => this.member1;
            set => this.member1 = value;
        }

    }
}
