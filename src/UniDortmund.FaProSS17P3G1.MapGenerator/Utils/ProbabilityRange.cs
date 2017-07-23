using System;
using System.Linq;
using System.Collections.Generic;

namespace UniDortmund.FaProSS17P3G1.MapGenerator.Utils
{
    public class ProbabilityRange<T>
    {
        private readonly SortedList<float, T> mValues;

        public ProbabilityRange(IEnumerable<(float RelProbability, T Value)> entries)
            : this(entries.ToList())
        {
        }

        public ProbabilityRange(params (float RelProbability, T Value)[] entries)
            : this((IList<(float RelProbability, T Value)>)entries)
        {
        }

        public ProbabilityRange(IList<(float RelProbability, T Value)> entries)
        {
            if (entries == null)
            {
                throw new ArgumentNullException(nameof(entries));
            }
            entries = entries.GroupBy(e => e.Value)
                .Select(g => new ValueTuple<float, T>(g.Select(e => e.RelProbability)
                    .Aggregate((a, b) => a + b), g.Key))
                .ToList();

            if (entries.Count == 0)
            {
                throw new ArgumentException("the probability range must contain at least one element", nameof(entries));
            }
            mValues = new SortedList<float, T>(entries.Count);

            var sum = entries.Select(typed => typed.RelProbability)
                .Aggregate((l, r) => l + r);
            sum /= 2;

            var current = -1f;
            foreach (var entry in entries)
            {
                mValues[current += entry.RelProbability / sum] = entry.Value;
            }
        }

        public T this[float key]
        {
            get
            {
                if (key < -1 || key > 1)
                {
                    throw new ArgumentOutOfRangeException(nameof(key));
                }
                var realKey = mValues.LowerBound(key);
                return mValues.Values[realKey];
            }
        }
    }
}
