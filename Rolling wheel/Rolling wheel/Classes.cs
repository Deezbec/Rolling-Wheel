using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Rolling_wheel
{
    class Dot
    {
        double x;
        double y;
        public double GetSetX { get { return x; } set { x = value; } }
        public double GetSetY { get { return y; } set { y = value; } }
        public Dot(double x, double y) //Собсна конструктор 
        {
            this.x = x;
            this.y = y;
        }
        public void Draw(Graphics g, Color color, Circle main) //рисование линии от точки до центра заданное окружности 
        {
            SolidBrush brush = new SolidBrush(color);
            g.DrawLine(new Pen(color), (float)x, (float)y, (float)main.GetSetX, (float)main.GetSetY);
        }
        public void Draw(Graphics g, Color color) //рисование точки 
        {
            SolidBrush brush = new SolidBrush(color);
            g.FillRectangle(new SolidBrush(color), (float)x, (float)y, 1, 1);
        }
    }
    public class Circle 
    {
        int R;
        double x;
        double y;
        public double GetSetX { get { return x; }  set { x = value; } }
        public double GetSetY { get { return y; } set { y = value; } }
        public int GetSetR { get { return R; } set { R = value; } }
        double lyambda;
        double fi;
        double fidelta;
        public double GetSetLyambda { get { return lyambda; } set { lyambda = value; } }
        public double GetSetFi { get { return fi; } set { fi = value; } }
        public double GetSetFiDelta { get { return fidelta; } set { fidelta = value; } }
        public Circle(double x, double y, int R) //Собсна конструктор (fi и lyam задаются ПУ)
        {
            this.x = x;
            this.y = y;
            this.R = R;
            this.lyambda = 1;
            this.fi = 0;
            this.fidelta = 1;
        }
        public void Draw(Graphics g, Color color) //рисование окружности 
        {
            SolidBrush brush = new SolidBrush(color);
            g.FillEllipse(brush, (float)x - (float)R, (float)y - (float)R, (float)2 * (float)R, (float)2 * (float)R);
        }
    }
}  