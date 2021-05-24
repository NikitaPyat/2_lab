using System;
using System.Collections.Generic;
using System.Text;

namespace Lab_2.Models.Collections
{
    public enum ChangeInfo
    {
        ItemChanged, Add, Remove, Replace
    }
    public delegate void DataChangedEventHandler(object source, DataChangedEventArgs args);
    public class DataChangedEventArgs
    {
        public ChangeInfo Change { get; set; }
        public string Info { get; set; }
        public DataChangedEventArgs(ChangeInfo x, string y)
        {
            Change = x;
            Info = y;
        }
        public override string ToString()
        {
            return "Change: " + Change + "\nInfo: " + Info + "\n";
        }
    }
}
