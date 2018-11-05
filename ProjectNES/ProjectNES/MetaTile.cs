using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectNES
{
    public static class MetaTile
    {
        public static int[][] meta = {
            // block A (0-3)
            new int[] { 1, 2, 5, 6 },
            new int[] { 3, 4, 7, 8 },
            new int[] { 9, 10, 13, 14 },
            new int[] { 11, 12, 15, 16 },
            // block B (4-7)
            new int[] { 1, 2, 3, 4 },
            new int[] { 5, 6, 7, 8 },
            new int[] { 9, 10, 11, 12 },
            new int[] { 13, 14, 15, 16 }
        };

        public static int[][] metameta = {
            new int[] { 0, 1, 2, 3 },
            new int[] { 4, 5, 6, 7 }
        };
    }
}
