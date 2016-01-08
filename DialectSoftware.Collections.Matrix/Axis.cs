using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;


/// ******************************************************************************************************************
/// * Copyright (c) 2011 Dialect Software LLC                                                                        *
/// * This software is distributed under the terms of the Apache License http://www.apache.org/licenses/LICENSE-2.0  *
/// *                                                                                                                *
/// ******************************************************************************************************************

namespace DialectSoftware.Collections
{
    //abstract definition ofcoordinate
    [Serializable]
    [XmlRoot("Axis")]
    public struct Axis
    {
        [XmlAttribute()]
        public Int64 Min;

        [XmlAttribute()]
        public Int64 Max;

        [XmlAttribute()]
        public Int64 Slope;

        public Point[] Points;

        [XmlAttribute()]
        public String Name;

        [XmlIgnore()]
        public int Count
        { 
            get
            {
                double d = ((double)((Max - Min) + 1)) / (double)Slope;
                return (int)Math.Ceiling(d);
            }
        }

        public Axis(String Name, int Min, int Max, int Slope)
        {
            this.Name = Name;
            this.Min = Min;
            this.Max = Max;
            this.Slope = Slope;
            this.Points = (Point[])Array.CreateInstance(typeof(Point), (int)Math.Ceiling(((double)((Max - Min) + 1)) / (double)Slope));
        }

        public Int64 this[int index]
        {
            get
            {
                //0 based
                if (index >= 0 && index <= Points.Length)
                {
                    return (index * Slope) + Min;
                }
                else
                    throw new IndexOutOfRangeException();
            }
        }

        public void Validate(Int64 i)
        {
            if ((i != 0 && i % Slope != 0) || !(i >= Min && i <= Max))
            {
                throw new IndexOutOfRangeException(String.Format("Invalid coordinate {0} {1}", Name, i));
            }
        }
    }
}
