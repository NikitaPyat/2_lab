using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;
namespace Lab_2.Models
{
    [Serializable]
    public struct DataItem:ISerializable
    {
        public Vector2 coord { get; set; }
        public Vector2 val { get; set; }

        public DataItem(Vector2 c, Vector2 v)
        {
            coord = c;
            val = v;
        }

        public override string ToString()
        {
            return coord.ToString() + " " + val.ToString();
        }

        public string ToString(string format)
        {
            return coord.ToString(format) + " " + val.ToString(format);
        }

        public string ToLongString(string format)
        {
            return coord.ToString(format) + " " + val.ToString(format) + " " + val.Length().ToString(format);
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("coord_x", coord.X);
            info.AddValue("coord_y", coord.Y);
            info.AddValue("val_x", val.X);
            info.AddValue("val_y", val.Y);
        }
        public DataItem(SerializationInfo info, StreamingContext context)
        {
            coord = new Vector2(info.GetSingle("coord_x"), info.GetSingle("coord_y"));
            val = new Vector2(info.GetSingle("val_x"), info.GetSingle("val_y"));
        }
    }
}
