using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TournamentSort
{
    public class Models
    {
        public class ArrayModel
        {
            public int elementsCount;
            public int[] array;
            public int[] unsortedArray;
            public List<int> sortedArray = new List<int>();

            public ArrayModel(int elementsCount)
            {
                this.elementsCount = elementsCount;
            }

            public ArrayModel()
            {

            }
        }
    }
}
