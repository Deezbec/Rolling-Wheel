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
        int TrashSlash = 0; // временный переключатель заданий
        List<Dot> Dots = new List<Dot>(); // Список искомых точек
        Circle main; //Главный круг
        Dot dot, dotNew; // Просто точа
        Circle deSecond; // Второй круг, используется в задании б и, может, г
        Form2 sets = new Form2();
        public int r;
        public Form1() //Не мое, ни в чем не виноват 
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //sets.Show();           
            if (TrashSlash == 0)  // Код варианта а, вскоре будет переключатель
            {
                main = new Circle(50, 50, 50);
                dot = new Dot(main.GetSetX, main.GetSetY + main.GetSetR * main.GetSetLyambda); //Создание точки на основе данных первого круга
                deSecond = new Circle(0 - main.GetSetR, main.GetSetY, main.GetSetR);
                dotNew = new Dot(main.GetSetX, main.GetSetY + main.GetSetR * main.GetSetLyambda);
                LyambdaBox.Text = Convert.ToString(main.GetSetLyambda); // ↓
                fiBox.Text = Convert.ToString(main.GetSetFiDelta);      // Подготовка текстовых окон
            }
            if (TrashSlash == 1 || TrashSlash == 2)  // Код варианта б
            {
                if (TrashSlash == 1) main = new Circle(250, 150, 50); else main = new Circle(250, 150, 60);
                deSecond = new Circle(main.GetSetX, main.GetSetY + main.GetSetR * main.GetSetLyambda + 15, 15); // Создание второстепенного круга на основе первого
                dot = new Dot(deSecond.GetSetX, deSecond.GetSetY + deSecond.GetSetR * deSecond.GetSetLyambda); // Создание точки на основе данных второго круга
                LyambdaBox.Text = Convert.ToString(deSecond.GetSetLyambda); // ↓
                fiBox.Text = Convert.ToString(deSecond.GetSetFiDelta);      // Подготовка текстовых окон
                if (TrashSlash == 1) main.GetSetLyambda = 1 + deSecond.GetSetR / main.GetSetR; else main.GetSetLyambda = 1 - deSecond.GetSetR / main.GetSetR;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!Timer.Enabled) { Timer.Start(); StartButton.Text = "Stop"; } else { Timer.Stop(); StartButton.Text = "Continue"; }
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
                e.Graphics.DrawLine(new Pen(Color.Gold) , main.GetSetR, main.GetSetR * 2, main.GetSetR + 2 * (int)Math.PI * main.GetSetR, main.GetSetR * 2);
                if (main.GetSetX + main.GetSetR >= Width && main.GetSetX - main.GetSetR < Width)
                {
                    deSecond.GetSetX = main.GetSetX - Width;
                    dotNew.GetSetX = dot.GetSetX - Width;
                    dotNew.GetSetY = dot.GetSetY;
                    deSecond.Draw(e.Graphics, Color.Black);
                    dotNew.Draw(e.Graphics, Color.Red, deSecond);
                }

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
            }
            for (int i = 0; i < Dots.Count; i++) Dots[i].Draw(e.Graphics, Color.Green);
            //for (int i = 1; i < Dots.Count; i++) e.Graphics.DrawLine(new Pen(Color.Green), Dots[i - 1].GetSetX, Dots[i - 1].GetSetY, Dots[i].GetSetX, Dots[i].GetSetY);
        }

        private void LyambdaBox_TextChanged(object sender, EventArgs e)
        {
            if (TrashSlash == 0) main.InputCheck(LyambdaBox, "l"); // Код варианта а, вскоре будет переключатель
            if (TrashSlash == 1 || TrashSlash == 2) deSecond.InputCheck(LyambdaBox, "l"); // Код варианта б и в
            Refresh();
        }

        private void fiBox_TextChanged(object sender, EventArgs e)
        {
            if (TrashSlash == 0) main.InputCheck(fiBox, "f"); // Код варианта а, вскоре будет переключатель
            if (TrashSlash == 1 || TrashSlash == 2) deSecond.InputCheck(fiBox, "f"); // Код варианта б и в  
            Refresh();
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
            Form2 Sets = new Form2();
            Sets.Show();
            Sets.eventus += Sets_Delegate;
        }
        private void Sets_Delegate(object sender, RadiusEventArgs a)
        {
            main.GetSetR = a.Radius_Change;
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
                deSecond.GetSetFi -= 180 / (deSecond.GetSetR * (float)Math.PI) * deSecond.GetSetFiDelta * (float)1.05; // Изменение угла поворота для второстепенного круга
            }
            Dots.Add(new Dot(dot.GetSetX, dot.GetSetY)); //Добавление искомых точек в список искомых точек 
            Refresh();
        }
    }
}