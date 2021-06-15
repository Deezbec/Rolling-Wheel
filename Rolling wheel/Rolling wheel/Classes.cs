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
        float x;
        float y;
        public float GetSetX { get { return x; } set { x = value; } }
        public float GetSetY { get { return y; } set { y = value; } }
        public Dot(float x, float y) //Собсна конструктор 
        {
            this.x = x;
            this.y = y;
        }
        public void Draw(Graphics g, Color color, Circle main) //рисование линии от точки до центра заданное окружности 
        {
            SolidBrush brush = new SolidBrush(color);
            g.DrawLine(new Pen(color), x, y, main.GetSetX, main.GetSetY);
        }
        public void Draw(Graphics g, Color color) //рисование точки 
        {
            SolidBrush brush = new SolidBrush(color);
            g.FillRectangle(new SolidBrush(color), x, y, 1, 1);
        }
    }
    class Circle 
    {
        int R;
        float x;
        float y;
        public float GetSetX { get { return x; }  set { x = value; } }
        public float GetSetY { get { return y; } set { y = value; } }
        public int GetSetR { get { return R; } set { R = value; } }
        //Shit stuff is under that statement 
        float lyambda;
        float fi;
        float fidelta;
        public float GetSetLyambda { get { return lyambda; } set { lyambda = value; } }
        public float GetSetFi { get { return fi; } set { fi = value; } }
        public float GetSetFiDelta { get { return fidelta; } set { fidelta = value; } }
        public Circle(float x, float y, int R) //Собсна конструктор (fi и lyam задаются ПУ)
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
            g.FillEllipse(brush, x - R, y - R, 2 * R, 2 * R);
        }
        public void InputCheck(TextBox Box, string s) //Функция для работы TextBox-ов
        {
            bool flag = false;
            if (Box.TextLength != 0)
            {
                for (int i = 0; i < Box.TextLength; i++) if ((Convert.ToInt32(Box.Text[i]) >= 48 && Box.Text[i] <= 57) || Box.Text[i] == 44 || Box.Text[i] == 45) flag = true; else { flag = false; break; }
                if (Box.TextLength == 1 && Box.Text == "-") flag = false;
            }
            if (flag)
            {
                if (s == "l") this.lyambda = Convert.ToSingle(Box.Text);
                if (s == "f") this.fidelta = Convert.ToSingle(Box.Text);
            }
        }
    }

    public class SettingsEventArgs : EventArgs
    {
        public int r1 { get; set; }
        public SettingsEventArgs(int r1)
        {
            this.r1 = r1;
        }
    }

    public delegate void SettingsChangedEventHandler(object Sender, SettingsEventArgs e);
    public partial class Smth : Form
    {
        public event SettingsChangedEventHandler SettingsChanged;
        public Smth()
        {
            //InitializeComponent();
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            //if (this.SettingsChanged != null) this.SettingsChanged(this, new SettingsEventArgs(Convert.ToInt32(trackBar1.Value.ToString())));
        }
    }
}  