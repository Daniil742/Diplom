using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diplom
{
    class InFlag
    {
        public int f;

        public int Pin { get; } = -1;
        public int Qin { get; } = 1;
        public int Unknown { get; } = 0;

        public InFlag()
        {
            f = Unknown;
        }
    }
}
