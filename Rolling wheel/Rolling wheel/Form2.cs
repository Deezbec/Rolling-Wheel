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
    public delegate void Rad1Change(object sender, Rad1EventArgs e);
    public delegate void FiChange(object sender, FiEventArgs e);
    public delegate void DistChange(object sender, DistEventArgs e);
    public delegate void Rad2Change(object sender, Rad2EventArgs e);

    public partial class Form2 : Form
    {
        public event Rad1Change Rad1Change;
        public event FiChange FiChange;
        public event DistChange DistChange;
        public event Rad2Change Rad2Change;
        public Form2(Circle main, Circle DeSecond, int TrashSlash)
        {
            InitializeComponent();
            this.main = main;
            this.DeSecond = DeSecond;
            this.TrashSlash = TrashSlash;
        }
        Circle main, DeSecond;
        int TrashSlash;

        private void Form2_Load(object sender, EventArgs e)
        {
            int k;
            trackBar1.Value = main.GetSetR;
            
            if (TrashSlash == 0)
            {
                label4.Visible = false;
                trackBar4.Visible = false;
                label8.Visible = false;
                Height = 120;
                label9.Location = new Point(0, 80);
                trackBar2.Value = (int)main.GetSetFiDelta;
                trackBar3.Value = 100;
            }
            else
            {
                trackBar2.Value = (int)DeSecond.GetSetFiDelta;
                trackBar3.Value = 100;
                trackBar4.Value = DeSecond.GetSetR;
                k = label4.Location.Y;
                label8.Text = Convert.ToString(trackBar4.Value);
                if (TrashSlash == 2) { trackBar1.Minimum = trackBar4.Value; trackBar4.Maximum = trackBar1.Value; }
                Height = 150;
            }
            label5.Text = Convert.ToString(trackBar1.Value);
            label6.Text = Convert.ToString(trackBar2.Value);
            label7.Text = Convert.ToString(trackBar3.Value / 100);
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            label5.Text = Convert.ToString(trackBar1.Value);
            int i = Convert.ToInt32(trackBar1.Value);
            if (Rad1Change != null)
            {
                Rad1Change(this, new Rad1EventArgs(i));
            }
            if (TrashSlash == 2) trackBar4.Maximum = trackBar1.Value;
        }

        private void trackBar2_Scroll(object sender, EventArgs e)
        {
            label6.Text = Convert.ToString(trackBar2.Value);
            int i = Convert.ToInt32(trackBar2.Value);
            if (FiChange != null)
            {
                FiChange(this, new FiEventArgs(i));
            }
        }

        private void trackBar3_Scroll(object sender, EventArgs e)
        {
            label7.Text = Convert.ToString(Convert.ToDouble(trackBar3.Value) / 100);
            double i = Convert.ToDouble(trackBar3.Value) / 100;
            if (DistChange != null)
            {
                DistChange(this, new DistEventArgs(i));
            }
        }

        private void trackBar4_Scroll(object sender, EventArgs e)
        {
            label8.Text = Convert.ToString(trackBar4.Value);
            int i = Convert.ToInt32(trackBar4.Value);
            if (TrashSlash == 2) trackBar1.Minimum = trackBar4.Value;
            if (Rad2Change != null)
            {
                Rad2Change(this, new Rad2EventArgs(i));
            }
        }

        private void label1_MouseHover(object sender, EventArgs e)
        {
            if (sender.Equals(label1)) { label9.Text = @"
            Радиус первого круга. для первого задания - единственный                        
            круг на экране, для оставшихся двух это круг, по которому
            совершается движение. За единицу взят пиксель.
 "; Height = (TrashSlash == 0) ? 180 : 210; }
            if (sender.Equals(label2)) { label9.Text = @"
            Угол, на который поворачивается круг каждый тик таймера.                        
            За единицу взят угол, который проходит круг, перемещаясь
            на 1 пиксель.
            
 "; Height = (TrashSlash == 0) ? 180 : 210; }
            if (sender.Equals(label3)) { label9.Text = @"
            Расстояние между ""рисующим концом"" и центром движущейся                        
            окружности. За единицу взят радиус движущейся окружности
 "; Height = (TrashSlash == 0) ? 165 : 200; }
            if (sender.Equals(label4)) { label9.Text = @"
            Радиус второго круга. У первого задания такого нет,                                      
            для второго и третьего задания это круг, который                                        
            совершает движение. За единицу взят пиксель.                                                                          
 "; Height = (TrashSlash == 0) ? 180 : 210; }
            label9.Visible = true;
        }

        private void label1_MouseLeave(object sender, EventArgs e)
        {
            if (TrashSlash == 0) Height = 120;
            else Height = 150;
            label9.Visible = false;
        }
    }


    public class Rad1EventArgs : EventArgs
    {
        public int Radius_Change { set; get; }
        public Rad1EventArgs(int R) { Radius_Change = R; }
    }
    public class FiEventArgs : EventArgs
    {
        public int Fi_Change { set; get; }
        public FiEventArgs(int R) { Fi_Change = R; }
    }
    public class Rad2EventArgs : EventArgs
    {
        public int Rad2_Change { set; get; }
        public Rad2EventArgs(int R) { Rad2_Change = R; }
    }
    public class DistEventArgs : EventArgs
    {
        public double Dist_Change { set; get; }
        public DistEventArgs(double R) { Dist_Change = R; }
    }
}
