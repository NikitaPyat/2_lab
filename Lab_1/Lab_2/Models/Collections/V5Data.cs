using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using System.ComponentModel;
using System.Collections;


namespace Lab_2.Models.Collections
{
    [Serializable]
    public abstract class V5Data : IEnumerable<DataItem>, INotifyPropertyChanged
    {
        public string info { get; set; }
        public DateTime date { get; set; }

        public V5Data() {}

        public V5Data(String i, DateTime d)
        {
            info = i;
            date = d;
        }

        public string GetinfoD()
        { return info; }

        public void SetinfoD(string value)
        { info = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("info")); }
        public DateTime dateD
        {
            get {return date; }
            set{date = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("time"));}
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public abstract Vector2[] NearEqual(float eps);

        public override string ToString()

        {
            return "Info: " + info.ToString() + " Date: " + date.ToString();
        }
        public abstract string ToLongString();

        public virtual string ToString(string format)
        {
            return "Info: " + info + "\n" + "Data: " + date.ToString() + "\n";
        }

        public IEnumerator<DataItem> GetEnumerator()
        {
            V5DataCollection tmp = new V5DataCollection();
            tmp = (V5DataCollection)this;
            return tmp.Ditems.GetEnumerator();
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            V5DataCollection tmp = new V5DataCollection();
            tmp = (V5DataCollection)this;
            return tmp.GetEnumerator();
        }
    }
}