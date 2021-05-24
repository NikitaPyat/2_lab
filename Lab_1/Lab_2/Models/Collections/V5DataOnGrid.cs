using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using System.Globalization;
using System.Runtime.Serialization;

namespace Lab_2.Models.Collections
{
    [Serializable]
    public class V5DataOnGrid : V5Data, IEnumerable<DataItem>, ISerializable
    {
        public Grid2D net { get; set; }
        public Vector2[,] mas { get; set; }

        public V5DataOnGrid(string s, DateTime d, Grid2D gr) : base(s, d)
        {
            net = gr;
            mas = new Vector2[net.x_kol, net.y_kol];
        }

        public void InitRandom(float minValue = 0, float maxValue = 15)
        {
            Random rand = new Random(123);
            float rand1, rand2, minv, maxv;
            Vector2 coordinate, value;

            for (int i = 0; i < net.x_kol; i++)
            {
                for (int j = 0; j < net.y_kol; j++)
                {
                    rand1 = (float)rand.NextDouble();
                    rand2 = (float)rand.NextDouble();
                    minv = minValue * rand1 + maxValue * (1 - rand1);
                    maxv = minValue * rand2 + maxValue * (1 - rand2);
                    mas[i, j] = new Vector2(minv, maxv);

                    coordinate.X = i;
                    coordinate.Y = j;
                    value.X = minv;
                    value.Y = maxv;
                }
            }
        }

        public V5DataOnGrid(string filename){
            StreamReader sr = null;

            try{
                sr = new StreamReader(filename);
                info = sr.ReadLine();
                date = DateTime.Parse(sr.ReadLine());

                Grid2D grid = new Grid2D {
                    x_kol = int.Parse(sr.ReadLine()),
                    x_step = float.Parse(sr.ReadLine()),
                    y_kol = int.Parse(sr.ReadLine()),
                    y_step = float.Parse(sr.ReadLine()),
                };
                net = grid;
                mas = new Vector2[net.x_kol, net.y_kol];

                for (int i = 0; i < net.x_kol; i++)
                    for (int j = 0; j < net.y_kol; j++) {
                        string[] data = sr.ReadLine().Split(' ');
                        mas[i, j] = new Vector2(
                             (float)Convert.ToDouble(data[0]),
                             (float)Convert.ToDouble(data[1]));
                    }
            }
            catch (ArgumentException) {
                Console.WriteLine("Filename is empty string");
            }
            catch (FileNotFoundException) {
                Console.WriteLine("File is not found");
            }
            catch (DirectoryNotFoundException) {
                Console.WriteLine("Directory is not found");
            }
            catch (IOException) {
                Console.WriteLine("Unacceptable filename");
            }
            catch (FormatException) {
                Console.WriteLine("String could not be parsed");
            }
            catch (Exception ex) {
                Console.WriteLine(ex.Message);
            }
            finally {
                if (sr != null) sr.Dispose();
            }
        }

        public override Vector2[] NearEqual(float eps) {
            List<Vector2> list = new List<Vector2>();
            for (int i = 0; i < net.x_kol; i++)
                for (int j = 0; j < net.y_kol; j++)
                    if (Math.Abs(mas[i, j].X - mas[i, j].Y) <= eps)
                        list.Add(mas[i, j]);
            Vector2[] array = list.ToArray();
            return array;
        }

        public override string ToString() {
            return "V5DataOnGrid\n" + info + " " + date.ToString() + " " + net.ToString() + "\n";
        }

        public override string ToLongString()
        {
            string str = "V5DataOnGrid\n";
            str += info + " " + date.ToString() + " " + net.ToString() + "\n";
            for (int i = 0; i < net.x_kol; i++)
                for (int j = 0; j < net.x_kol; j++)
                {
                    str += "[" + i + ", " + j + "] " + "(" + mas[i, j].X + ", " + mas[i, j].Y + ")\n";
                }
            str += "\n";
            return str;
        }

        public string ToLongString(string format)
        {
            string str = "V5DataOnGrid(ls):\n" + info + " " + date.ToString(format) + " " + net.ToString(format) + "\n";
            for (int i = 0; i < net.x_kol; i++)
                for (int j = 0; j < net.y_kol; j++)
                {
                    str += "Score for node " + "[" + i + "," + j + "] " + " is " + "(" + mas[i, j].X + "," + mas[i, j].Y + ")\n";
                }

            return str;
        }

        public static explicit operator V5DataCollection(V5DataOnGrid d)
        {
            V5DataCollection Out;
            Vector2 x, x_1;
            Out = new V5DataCollection(d.info, d.date);

            for (int i = 0; i < d.net.x_kol; i++)
                for (int j = 0; j < d.net.y_kol; j++)
                {
                    x = new Vector2(i, j);
                    x_1 = new Vector2(d.mas[i, j].X, d.mas[i, j].Y);
                    Out.dic.Add(x, x_1);
                }
            return Out;
        }

        public IEnumerator<DataItem> GetEnumerator()
        {
            List<DataItem> list = new List<DataItem>();
            DataItem tmp;
            Vector2 coordinate, value;
            for (int i = 0; i < net.x_kol; i++)
                for (int j = 0; j < net.y_kol; j++)
                {
                    coordinate.X = i;
                    coordinate.Y = j;
                    value.X = mas[i, j].X;
                    value.Y = mas[i, j].Y;
                    tmp = new DataItem(coordinate, value);
                    list.Add(tmp);
                }
            return list.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            List<DataItem> list = new List<DataItem>();
            DataItem tmp;
            Vector2 coordinate, value;
            for (int i = 0; i < net.x_kol; i++)
                for (int j = 0; j < net.y_kol; j++)
                {
                    coordinate.X = i;
                    coordinate.Y = j;
                    value.X = mas[i, j].X;
                    value.Y = mas[i, j].Y;
                    tmp = new DataItem(coordinate, value);
                    list.Add(tmp);
                }
            return list.GetEnumerator();
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context) {
            float[] valx = new float[net.x_kol];
            float[] valy = new float[net.y_kol];
            for (int i = 0; i < valx.Length; i++)
                for (int j = 0; j < valy.Length; j++) {
                    valx[i] = mas[i, j].X;
                    valy[j] = mas[i, j].Y;
                }
            info.AddValue("net", net);
            info.AddValue("valx", valx);
            info.AddValue("valy", valy);
            info.AddValue("info", base.info);
            info.AddValue("date", base.date);
        }
        public V5DataOnGrid(SerializationInfo info, StreamingContext contex) : base((string)info.GetValue("info", typeof(string)),
                       (DateTime)info.GetValue("date", typeof(DateTime))) {
            net = (Grid2D)info.GetValue("net", typeof(Grid2D));
            mas = new Vector2[net.x_kol, net.y_kol];
            float[] valx = (float[])info.GetValue("valx", typeof(float[]));
            float[] valy = (float[])info.GetValue("valy", typeof(float[]));
            for (int i = 0; i < valx.Length; i++)
                for (int j = 0; j < valy.Length; j++) {
                    mas[i, j].X = valx[i];
                    mas[i, j].Y = valy[j];
                }
        }
    }
}
