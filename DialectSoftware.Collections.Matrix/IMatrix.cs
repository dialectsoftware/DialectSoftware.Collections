using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Collections.ObjectModel;

/// ******************************************************************************************************************
/// * Copyright (c) 2011 Dialect Software LLC                                                                        *
/// * This software is distributed under the terms of the Apache License http://www.apache.org/licenses/LICENSE-2.0  *
/// *                                                                                                                *
/// ******************************************************************************************************************

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
