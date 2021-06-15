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
    public partial class Form2 : Form
    {
        public event SetsChange eventus;
        /*int r1;
        public int GetR1 { get { return r1; } }*/
        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            /*r1 = 10;*/
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            int i = Convert.ToInt32(Rad1TrackBar.Value);
            if (eventus != null)
            {
                eventus(this, new RadiusEventArgs(i));
            }
            /*r1 = Convert.ToInt32(Rad1TrackBar.Value);*/
        }
    }

    public delegate void SetsChange(object sender, RadiusEventArgs e);

    public class RadiusEventArgs: EventArgs
    {
        public int Radius_Change { set; get; }
        public RadiusEventArgs(int R)
        {
            Radius_Change = R;
        }
    }
}
