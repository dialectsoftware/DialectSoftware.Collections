using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Collections.ObjectModel;

namespace DialectSoftware.Collections
{
    public interface IMatrix<T> : IEnumerable
    {
        ReadOnlyCollection<Axis> Axes { get; }

        bool Contains(Int64 hashCode);

        bool Contains(params Int64[] coordinates);

        T this[Int64 hashcode]
        {
            get;
            set;
        }

        T this[Int64[] coordinates]
        {
            get;
            set;
        }

        Int64 GetHashCode(params Int64[] coordinates);
        
        Int64[] GetCoordinatesAt(Int64 hashCode);
        
    }
}
