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

    public partial class Form1 : Form
    {   

        public int TrashSlash = -1; 
        List<Dot> Dots = new List<Dot>(); // Список искомых точек
        Circle main; //Главный круг
        Dot dot, dotNew; // Просто точа
        Circle deSecond; // Второй круг, используется в задании б и, может, г
        public int r;
        int distToMain = 100;
        Form2 Sets;
        public Form1() //Не мое, ни в чем не виноват 
        {
            InitializeComponent();
        }
        void ReturnToDef()
        {
            label1.Visible = false;
            menuStrip1.Visible = true;
            Height = 350;
            Width = 650;
            button1.Visible = false;
            button2.Visible = false;
            button3.Visible = false;
            Dots.Clear();
        }
        void SecOrThirCase()
        {
            if (TrashSlash == 1) main = new Circle(250, 150, 50); else main = new Circle(250, 150, 30);
            deSecond = new Circle(main.GetSetX, main.GetSetY + main.GetSetR * main.GetSetLyambda + 15, 15); // Создание второстепенного круга на основе первого
            dot = new Dot(deSecond.GetSetX, deSecond.GetSetY + deSecond.GetSetR * deSecond.GetSetLyambda); // Создание точки на основе данных второго круга
            if (TrashSlash == 1) main.GetSetLyambda = 1 + (float)deSecond.GetSetR / (float)main.GetSetR; else main.GetSetLyambda = 1 - (float)deSecond.GetSetR / (float)main.GetSetR;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            menuStrip1.Visible = false;
            Height = 150;
            Width = 380;
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {

            if (TrashSlash == 0)   // Код варианта а, вскоре будет переключатель
            {
                if (main.GetSetX - main.GetSetR == Width) main.GetSetX = main.GetSetR;
                dot.GetSetX = main.GetSetX + main.GetSetR * (float)Math.Sin(Math.PI * main.GetSetFi / 180) * main.GetSetLyambda; //↓
                dot.GetSetY = main.GetSetY + main.GetSetR * (float)Math.Cos(Math.PI * main.GetSetFi / 180) * main.GetSetLyambda; //Координаты искомой точки
                main.Draw(e.Graphics, Color.Black); //Рисование главного крга
                dot.Draw(e.Graphics, Color.Red, main); //Мой лень слишком большой чтобы писать комментарии дальше
                if (main.GetSetX + main.GetSetR >= Width && main.GetSetX - main.GetSetR < Width)
                {
                    deSecond.GetSetX = main.GetSetX - Width;
                    dotNew.GetSetX = dot.GetSetX - Width;
                    dotNew.GetSetY = dot.GetSetY;
                    deSecond.Draw(e.Graphics, Color.Black);
                    dotNew.Draw(e.Graphics, Color.Red, deSecond);
                }
                for (int i = 0; i < Dots.Count; i++) Dots[i].Draw(e.Graphics, Color.Green);
                //e.Graphics.DrawLine(new Pen(Color.Gold) , main.GetSetR + distToMain, main.GetSetR * 2 + distToMain, main.GetSetR + 2 * (int)Math.PI * main.GetSetR + distToMain, main.GetSetR * 2 + distToMain);

            }
            if (TrashSlash == 1 || TrashSlash == 2)   // Код варианта б
            {
                deSecond.GetSetX = main.GetSetX + main.GetSetR * (float)Math.Sin(Math.PI * main.GetSetFi / 180) * main.GetSetLyambda; //↓
                deSecond.GetSetY = main.GetSetY + main.GetSetR * (float)Math.Cos(Math.PI * main.GetSetFi / 180) * main.GetSetLyambda; //Координаты центра круга
                dot.GetSetX = deSecond.GetSetX + deSecond.GetSetR * (float)Math.Sin(Math.PI * deSecond.GetSetFi / 180) * deSecond.GetSetLyambda; //↓
                dot.GetSetY = deSecond.GetSetY + deSecond.GetSetR * (float)Math.Cos(Math.PI * deSecond.GetSetFi / 180) * deSecond.GetSetLyambda; //Координаты искомой точки
                main.Draw(e.Graphics, Color.Black); //Рисование главного круга
                deSecond.Draw(e.Graphics, Color.OrangeRed); //Рисование второго круга, который и будет двигаться
                dot.Draw(e.Graphics, Color.Blue, deSecond);
                for (int i = 1; i < Dots.Count; i++) e.Graphics.DrawLine(new Pen(Color.Green), Dots[i - 1].GetSetX, Dots[i - 1].GetSetY, Dots[i].GetSetX, Dots[i].GetSetY);
            }
        }

        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            r = main.GetSetR;
            foreach (Form f in Application.OpenForms)
            {
                if (f.Name == "Form2")
                {
                    return;
                }
            }
            Sets = new Form2(main, deSecond, TrashSlash);
            Sets.Show();
            Sets.Rad1Change += Rad1_Delegate;
            Sets.FiChange += Fi_Delegate;
            Sets.DistChange += Dist_Delegate;
            Sets.Rad2Change += Rad2_Delegate;
        }
        private void Fi_Delegate(object sender, FiEventArgs a)
        {
            main.GetSetFiDelta = a.Fi_Change;
            if (TrashSlash == 0) deSecond.GetSetFiDelta = a.Fi_Change;

            Refresh();
        }

        private void Rad2_Delegate(object sender, Rad2EventArgs a)
        {
            deSecond.GetSetR = a.Rad2_Change;
            if (TrashSlash == 1 || TrashSlash == 2) if (TrashSlash == 1) main.GetSetLyambda = 1 + (float)deSecond.GetSetR / (float)main.GetSetR; else main.GetSetLyambda = 1 - (float)deSecond.GetSetR / (float)main.GetSetR;
            Refresh();
        }
        private void Rad1_Delegate(object sender, Rad1EventArgs a)
        {
            main.GetSetR = a.Radius_Change;
            if (TrashSlash == 0) deSecond.GetSetR = a.Radius_Change;
            if (TrashSlash == 1 || TrashSlash == 2) if (TrashSlash == 1) main.GetSetLyambda = 1 + (float)deSecond.GetSetR / (float)main.GetSetR; else main.GetSetLyambda = 1 - (float)deSecond.GetSetR / (float)main.GetSetR;
            Refresh();
        }
        private void Dist_Delegate(object sender, DistEventArgs a)
        {
            if (TrashSlash == 0) main.GetSetLyambda = a.Dist_Change;
            else deSecond.GetSetLyambda = a.Dist_Change;
            Refresh();
        }

        private void startToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (TrashSlash == -1) { }
            else if (!Timer.Enabled) { Timer.Start(); startToolStripMenuItem.Text = "Stop"; } else { Timer.Stop(); startToolStripMenuItem.Text = "Continue"; }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            TrashSlash = 0;
            ReturnToDef();
            main = new Circle(25, distToMain + 25, 25);
            dot = new Dot(main.GetSetX, main.GetSetY + main.GetSetR * main.GetSetLyambda); //Создание точки на основе данных первого круга
            deSecond = new Circle(0 - main.GetSetR, main.GetSetY, main.GetSetR);
            dotNew = new Dot(main.GetSetX, main.GetSetY + main.GetSetR * main.GetSetLyambda);
            if (Sets != null)
            {
                Sets.Close();
                settingsToolStripMenuItem_Click(sender, e);
            }
            Refresh();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            TrashSlash = 1;
            ReturnToDef();
            SecOrThirCase();
            if (Sets != null)
            {
                Sets.Close();
                settingsToolStripMenuItem_Click(sender, e);
            }
            Refresh();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            TrashSlash = 2;
            ReturnToDef();
            SecOrThirCase();
            if (Sets != null)
            {
                Sets.Close();
                settingsToolStripMenuItem_Click(sender, e);
            }
            Refresh();
        }

        private void clearToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Dots.Clear(); Refresh();
        }

        private void button1_MouseHover(object sender, EventArgs e)
        {
            if (sender.Equals(button1)) { label1.Text = @"
Колесо радиуса R катится по горизонтальной поверхности
 без проскальзывания, Точка А расположена на расстоянии a
от оси колеса (будем считать, что а может быть как меньше, так и   
больше R. такие точки можно найти, например, на реборде — выступе
 железнодорожного колеса). 
Требуется построить семейство траекторий точек колеса.
 "; Height = 250; }
            if (sender.Equals(button2)) { label1.Text = @"
Колесо радиуса R катится по внешней дуге колеса радиуса R1 без     
проскальзывания. Точка А расположена на расстоянии a от оси 
колеса.Требуется построить семейство траекторий точек колеса.
 "; Height = 210; }
            if (sender.Equals(button3)) { label1.Text = @"
Колесо радиуса R катится по внутренней дуге колеса радиуса R1 без      
проскальзывания. Точка А расположена на расстоянии a от оси 
колеса. Требуется построить семейство траекторий точек колеса.
 "; Height = 210; }

            label1.Visible = true;
        }

        private void button1_MouseLeave(object sender, EventArgs e)
        {
            if (TrashSlash == -1) Height = 150; else Height = 350;
            label1.Visible = false;
        }

        private void taskMenuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Timer.Stop();
            if (Sets != null) Sets.Close();
            TrashSlash = -1;
            menuStrip1.Visible = false;
            Height = 150;
            Width = 380;
            button1.Visible = true;
            button2.Visible = true;
            button3.Visible = true;
            Refresh();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (TrashSlash == 0)  // Код варианта а, вскоре будет переключатель
            {
                main.GetSetFi -= 180 / (main.GetSetR * (float)Math.PI) * main.GetSetFiDelta * (float)1.05; // Изменение угла поворота
                //main.GetSetFi -= (2 * main.GetSetR * (float)Math.PI) * main.GetSetFiDelta ?;
                main.GetSetX++;
                if (main.GetSetX + main.GetSetR >= Width) { deSecond.GetSetX++; Dots.Add(new Dot(dotNew.GetSetX, dotNew.GetSetY)); }
            }
            if (TrashSlash == 1 || TrashSlash == 2)  // Код варианта б и в
            {
                main.GetSetFi += 180 / (main.GetSetR * (float)Math.PI) * main.GetSetFiDelta * (float)1.05; // Изменение угла поворота для главного круга
                if (TrashSlash == 1) deSecond.GetSetFi += 180 / (deSecond.GetSetR * (float)Math.PI) * deSecond.GetSetFiDelta * (float)1.05;
                else     deSecond.GetSetFi -= 180 / (deSecond.GetSetR * (float)Math.PI) * deSecond.GetSetFiDelta * (float)1.05; // Изменение угла поворота для второстепенного круга
            }
            Dots.Add(new Dot(dot.GetSetX, dot.GetSetY)); //Добавление искомых точек в список искомых точек 
            Refresh();
        }
    }
}