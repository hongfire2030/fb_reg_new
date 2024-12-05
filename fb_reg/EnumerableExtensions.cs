using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fb_reg
{
    public static class EnumerableExtensions
    {

        public static IList<T> Shuffle<T>(this IEnumerable<T> sequence)
        {
            return sequence.Shuffle(new Random());
        }

        public static IList<T> Shuffle<T>(this IEnumerable<T> sequence, Random randomNumberGenerator)
        {
            if (sequence == null)
            {
                throw new ArgumentNullException("sequence");
            }

            if (randomNumberGenerator == null)
            {
                throw new ArgumentNullException("randomNumberGenerator");
            }

            T swapTemp;
            List<T> values = sequence.ToList();
            int currentlySelecting = values.Count;
            while (currentlySelecting > 1)
            {
                int selectedElement = randomNumberGenerator.Next(currentlySelecting);
                --currentlySelecting;
                if (currentlySelecting != selectedElement)
                {
                    swapTemp = values[currentlySelecting];
                    values[currentlySelecting] = values[selectedElement];
                    values[selectedElement] = swapTemp;
                }
            }

            return values;
        }
    }
}
