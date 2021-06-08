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
        int TrashSlash = 2; // временный переключатель заданий
        List<Dot> Dots = new List<Dot>(); // Список искомых точек
        Circle main; //Главный круг
        Dot dot; // Просто точа
        Circle deSecond; // Второй круг, используется в задании б и, может, г
        public Form1() //Не мое, ни в чем не виноват 
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            if (TrashSlash == 0)  // Код варианта а, вскоре будет переключатель
            {
                main = new Circle(0, 100, 25);
                dot = new Dot(main.GetSetX, main.GetSetY + main.GetSetR * main.GetSetLyambda); //Создание точки на основе данных первого круга
                LyambdaBox.Text = Convert.ToString(main.GetSetLyambda); // ↓
                fiBox.Text = Convert.ToString(main.GetSetFiDelta);      // Подготовка текстовых окон
            }
            if (TrashSlash == 1 || TrashSlash == 2)  // Код варианта б
            {
                if (TrashSlash == 1) main = new Circle(250, 150, 15); else main = new Circle(250, 150, 30);
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
                dot.GetSetX = main.GetSetX + main.GetSetR * (float)Math.Sin(Math.PI * main.GetSetFi / 180) * main.GetSetLyambda; //↓
                dot.GetSetY = main.GetSetY + main.GetSetR * (float)Math.Cos(Math.PI * main.GetSetFi / 180) * main.GetSetLyambda; //Координаты искомой точки
                main.Draw(e.Graphics, Color.Black); //Рисование главного крга
                dot.Draw(e.Graphics, Color.Red, main); //Мой лень слишком большой чтобы писать комментарии дальше
                for (int i = 0; i < Dots.Count; i++) Dots[i].Draw(e.Graphics, Color.Green);
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
                for (int i = 0; i < Dots.Count; i++) Dots[i].Draw(e.Graphics, Color.Green);
            }
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

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (TrashSlash == 0)  // Код варианта а, вскоре будет переключатель
            {
                main.GetSetFi -= 180 / (main.GetSetR * (float)Math.PI) * main.GetSetFiDelta; // Изменение угла поворота
                main.GetSetX++;
            }
            if (TrashSlash == 1 || TrashSlash == 2)  // Код варианта б и в
            {
                main.GetSetFi -= 180 / (main.GetSetR * (float)Math.PI) * main.GetSetFiDelta; // Изменение угла поворота для главного круга
                deSecond.GetSetFi -= 180 / (deSecond.GetSetR * (float)Math.PI) * deSecond.GetSetFiDelta; // Изменение угла поворота для второстепенного круга
            }
            Dots.Add(new Dot(dot.GetSetX, dot.GetSetY)); //Добавление искомых точек в список искомых точек 
            Refresh();
        }
    }
}