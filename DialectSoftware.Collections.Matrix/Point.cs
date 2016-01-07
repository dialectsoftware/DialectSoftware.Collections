using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace DialectSoftware.Collections
{
    [Serializable]
    [XmlRoot("Point")]
    public struct Point
    {
        String label;

        [XmlAttribute()]
        public String Label
        {
            get { return label ?? String.Empty; }
            set { label = value; }
        }

        public Point(String Label)
        {
            this.label = Label;
        }

    }

}
