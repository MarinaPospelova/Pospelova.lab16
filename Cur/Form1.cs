using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Cur
{
    public partial class Form1 : Form
    {
        Model m = new Model();
        public Form1()
        {
            InitializeComponent();
            m.rate = edRate.Value;
            chart1.Series[0].Points.AddXY(0, m.rate);
        }

        const double k = 0.05;
        private void btStart_Click(object sender, EventArgs e)
        {
            m.rate = m.GetReat();
            edRate.Value = m.rate;
            chart1.Series[0].Points.AddXY(0, m.rate);
        }

        private void btBuy_Click(object sender, EventArgs e)
        {
            m.doll = edDoll.Value;
            m.rub = edRub.Value;
            m.rate = edRate.Value;
            if (m.rub >= (m.rate*edBuy.Value))
                {
                    m.BuyDoll(edBuy.Value);
                    edRub.Value = m.rub;
                    edDoll.Value = m.doll;
                    textBox1.Text = "Успешно!";
                }
            else
                textBox1.Text = "Недостаточно средств!";
        }

        private void btSell_Click(object sender, EventArgs e)
        {
            m.doll = edDoll.Value;
            m.rub = edRub.Value;
            m.rate = edRate.Value;
            if (m.doll >= edSell.Value)
            {
                m.SellDoll(edSell.Value);
                edRub.Value = m.rub;
                edDoll.Value = m.doll;
                textBox1.Text = "Успешно!";
            }
            else
                textBox1.Text = "Недостаточно средств!";
        }
    }
    public class Model
    {
        const decimal M = 0.10m;       
        public decimal rub;
        public decimal doll;
        public decimal rate;
        const decimal Sigma = 0.10m;
        double Step = 30d;
        double dt;
        Random rnd = new Random();

        public decimal GetReat()
        {
            double A1 = rnd.NextDouble();
            double A2 = rnd.NextDouble();
            decimal Xi = (decimal)(Math.Sqrt(-2.0 * Math.Log(A1)) * Math.Sin(2.0 * Math.PI * A2));
            dt = 1d / Step;
            decimal X = M * rate * (decimal)dt + Sigma * rate * Xi * (decimal)Math.Sqrt(dt);
            rate += X;
            return rate;
        }
        public void BuyDoll(decimal buy)
        {
            rub -= buy * rate;
            doll += buy;
        }
        public void SellDoll(decimal sell)
        {
            rub += sell * rate;
            doll -= sell;
        }
    }
}
