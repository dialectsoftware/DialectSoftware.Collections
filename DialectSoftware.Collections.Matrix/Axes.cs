using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Xml.Serialization;
using System.Linq;

namespace DialectSoftware.Collections
{
    public class Axes : List<Axis>, IXmlSerializable
    {
        private Int64 max;
        private Int64[] factorization;

        protected Axes(): base()
        { 
        
        }

        public Axes(IEnumerable<Axis> axis):base(axis)
        {
            Initialize();
        }

        public Int64 GetHashCode(params Int64[] coordinates)
        {
            var invalid = coordinates.Select((c, i) =>
            {
                try
                {
                    this[i].Validate(c);
                    return null;
                }
                catch (Exception e)
                {
                    return e;
                }

            })
            .Where(e => e != null)
            .ToArray();

            if (invalid.Count() == 0)
            {
                return factorization.Select((f, i) =>
                {
                    return f * ((coordinates[i] - this[i].Min) / this[i].Slope);
                }).Sum();
            }

            throw invalid.First();
            
        }

        public Int64[] GetCoordinatesAt(Int64 hashCode)
        {
            if (hashCode < max)//0 based
            {
                Int64[] coordinates = factorization.Select((f, i) =>
                {
                    Int64 delta = (hashCode / f);
                    hashCode -= (delta * f);
                    return this[i].Min + (delta * this[i].Slope);
                }).ToArray();
                return coordinates;
            }
            return null;
        }

        public new Axis this[int index] 
        {
            get { return base[index]; }
            set 
            {
                base[index] = value;
                Initialize();
            } 
        }

        public new void Add(Axis axis)
        {
            base.Add(axis);
            Initialize();
        }

        public new void AddRange(IEnumerable<Axis> axes)
        {
            base.AddRange(axes);
            Initialize();
        }

        public new void Insert(int index, Axis item)
        {
            base.Insert(index, item);
            Initialize();
        }

        public new void InsertRange(int index, IEnumerable<Axis> collection)
        {
            base.InsertRange(index, collection);
            Initialize();
        }

        public new bool Remove(Axis item)
        {
            bool status = base.Remove(item);
            Initialize();
            return status;
        }

        public new int RemoveAll(Predicate<Axis> match)
        {
            int status = base.RemoveAll(match);
            Initialize();
            return status;
        }

        public new void RemoveAt(int index)
        {
            base.RemoveAt(index);
            Initialize();
        }

        public new void RemoveRange(int index, int count)
        {
            base.RemoveRange(index, count);
            Initialize();
        }

        public new void Reverse()
        {
            base.Reverse();
            Initialize();
        }

        public new void Reverse(int index, int count) 
        {
            base.Reverse(index, count);
            Initialize();
        }

        public new void Sort()
        {
            base.Sort();
            Initialize();
        }

        public new void Sort(Comparison<Axis> comparison)
        {
            base.Sort(comparison);
            Initialize(); 
        }

        public new void Sort(IComparer<Axis> comparer)
        {
            base.Sort(comparer);
            Initialize();
        
        }

        public new void Sort(int index, int count, IComparer<Axis> comparer)
        {
            base.Sort(index, count, comparer);
            Initialize();
        }

        public new void Clear() 
        {
            base.Clear();
            Initialize();
        }

        #region IEnumerable Members

        public new IEnumerator GetEnumerator()
        {
            return new Enumerator(this);
        }

        #endregion
    
        #region IXmlSerializable Members

        public virtual System.Xml.Schema.XmlSchema GetSchema()
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public virtual void ReadXml(System.Xml.XmlReader reader)
        {
            reader.Read();
            XmlSerializer serializer = new XmlSerializer(typeof(List<Axis>));
            var axes = (List<Axis>)serializer.Deserialize(reader);
            AddRange(axes);
        }

        public virtual void WriteXml(System.Xml.XmlWriter writer)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<Axis>));
            serializer.Serialize(writer, this);
        }

        #endregion   
        
        private void Reset()
        {
            max = 0;
            factorization = new Int64[] { };
        }

        private void Initialize()
        {
            max = this.Product();
            Int64 temp = max;
            factorization = this.Select((a, i) =>
            {
                double d = ((double)((a.Max - a.Min) + 1)) / (double)a.Slope;
                Int64 ii = (Int64)(Math.Ceiling(d)); 
                Int64 delta = (temp / ii);
                temp = delta;
                return delta;
            }).ToArray();
        }

        internal new class Enumerator : IEnumerator
        {
            Axes internalAxes;
            Int64 hashCode;

            public Enumerator(Axes axes)
            {
                internalAxes = axes;
                hashCode = -1;
            }

            public object Current
            {
                get 
                {
                    return internalAxes.GetCoordinatesAt(hashCode);
                }
            }

            public bool MoveNext()
            {
                hashCode++;
                return hashCode < internalAxes.max; 
            }

            public void Reset()
            {
                hashCode = -1; 
            }
        }
     }

    
}



 