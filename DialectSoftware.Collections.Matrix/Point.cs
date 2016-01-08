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
