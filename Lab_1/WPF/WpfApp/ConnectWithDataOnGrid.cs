using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Lab_2.Models;
using Lab_2.Models.Collections;

namespace WpfApp
{
    class ConnectWithDataOnGrid : IDataErrorInfo, INotifyPropertyChanged
    {
        private float x_step;
        private int x_num, y_num;
        private string str;

        public V5MainCollection MainCol;

        public ConnectWithDataOnGrid(ref V5MainCollection MC)
        {
            MainCol = MC;
        }

        public float Step
        {
            get
            {
                return x_step;
            }
            set
            {
                x_step = value;
                OnPropertyChanged("Step");
            }
        }

        public int X_num
        {
            get
            {
                return x_num;
            }
            set
            {
                x_num = value;
                OnPropertyChanged("X_num");
                OnPropertyChanged("Y_num");
            }
        }

        public int Y_num
        {
            get
            {
                return y_num;
            }
            set
            {
                y_num = value;
                OnPropertyChanged("Y_num");
                OnPropertyChanged("X_num");
            }
        }

        public string Str
        {
            get
            {
                return str;
            }
            set
            {
                str = value;
                OnPropertyChanged("Str");
            }
        }

        public string this[string columnName]
        {
            get
            {
                string outStr = null;
                switch (columnName)
                {
                    case "Str":
                        foreach (V5Data item in MainCol)
                            if (Str.Equals(item.info)) outStr = "Change string";
                        break;
                    case "Y_num":
                        if (Y_num < 3) outStr = "Y_num < 3";
                        break;
                    case "X_num":
                        if (X_num <= Y_num) outStr = "X_num <= Y_num";
                        break;
                    default:
                        break;
                }
                return outStr;
            }
        }

        public string Error
        {
            get
            {
                return "Error text";
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string property = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }

        public void Add()
        {
            V5DataOnGrid gr = new V5DataOnGrid(Str, DateTime.Now, new Grid2D(Step, X_num, Step, Y_num));
            gr.InitRandom();
            MainCol.Add(gr);
            OnPropertyChanged("X_num");
            OnPropertyChanged("Y_num");
            OnPropertyChanged("Step");
            OnPropertyChanged("Str");
        }
    }
}
