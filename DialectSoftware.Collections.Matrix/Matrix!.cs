using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Xml.Serialization;
using System.Xml;
using System.IO;
using System.Collections.ObjectModel;

/// ******************************************************************************************************************
/// * Copyright (c) 2011 Dialect Software LLC                                                                        *
/// * This software is distributed under the terms of the Apache License http://www.apache.org/licenses/LICENSE-2.0  *
/// *                                                                                                                *
/// ******************************************************************************************************************

namespace DialectSoftware.Collections.Generics
{
    public class Matrix<T> : Axes, IMatrix<T>, IXmlSerializable
    {
        private Hashtable buffer;

        protected Matrix():base()
        {
            buffer = new Hashtable();
        }

        public Matrix(IEnumerable<Axis> axes): this(new Axes(axes))
        {
            
        }

        public Matrix(Axes axes):base(axes)
        {
            buffer = new Hashtable();
        }

        public ReadOnlyCollection<Axis> Axes
        {
            get { return this.AsReadOnly(); }
        }

        public bool Contains(Int64 hashCode)
        {
            return buffer[hashCode] != null;
        }

        public bool Contains(params Int64[] coordinates)
        {
            return Contains(GetHashCode(coordinates));
        }

        public T this[Int64 hashCode]
        {
            get
            {
                return (T)buffer[hashCode];
            }
            set
            {
                buffer[hashCode] = value;
            }
        }

        public T this[params Int64[] coordinates]
        {
            get
            {
                return GetValue(coordinates);
            }
            set
            {
                SetValue(value, coordinates);
            }
        }

        protected T GetValue(params Int64[] coordinates) 
        {
            if (Contains(coordinates))
                return (T)buffer[GetHashCode(coordinates)];
            else
                return default(T);
        }

        protected void SetValue(T value, params Int64[] coordinates)
        {
            buffer[GetHashCode(coordinates)] = value;
        }

        #region IXmlSerializable Members

        public new void ReadXml(XmlReader reader)
        {
            reader.ReadToFollowing("ArrayOfAxis");
            XmlSerializer serializer = new XmlSerializer(typeof(List<Axis>));
            this.AddRange((List<Axis>)serializer.Deserialize(reader));
            reader.ReadToFollowing("Buffer");
            Type type = typeof(T);
            while(reader.ReadToFollowing("Value"))
            {
                Int64 hash = Int64.Parse(reader.GetAttribute("Hash"));
                XmlSerializer innerSerializer = new XmlSerializer(type);
                reader.Read();
                T value = (T)innerSerializer.Deserialize(reader);
                buffer[hash] = value;
            }
                        
        }

        public new void WriteXml(System.Xml.XmlWriter writer)
        {
            base.WriteXml(writer); 
            writer.WriteStartElement("Buffer");
            foreach(Int64 key in buffer.Keys)
            {
                writer.WriteStartElement("Value");
                writer.WriteAttributeString("Hash", key.ToString());
                XmlSerializer innerSerializer = new XmlSerializer(typeof(T));
                innerSerializer.Serialize(writer, (T)buffer[key]);
                writer.WriteEndElement();
            }
            writer.WriteEndElement();
        }

        #endregion

    }
}
