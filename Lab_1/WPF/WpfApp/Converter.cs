using System.Numerics;
using System.Windows.Data;
using System;
using System.Collections.Generic;
using Lab_2.Models.Collections;
using Lab_2.Models;

namespace WpfApp
{
    [ValueConversion(typeof(Grid2D), typeof(string))]
    public class GridConverter : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            Grid2D gr = (Grid2D)value;
            return $"Grid X: Step = {gr.x_kol}, \nStep Num = {gr.x_step}\n" + $"Grid Y: Step = {gr.y_kol}, \nStep Num = {gr.y_step}\n";
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture){
            return value;
        }
    }
}