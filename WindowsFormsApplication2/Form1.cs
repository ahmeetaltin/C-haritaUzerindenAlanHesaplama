using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WindowsFormsApplication2
{
    public partial class Form1 : Form
    {
        int x, y, i = 0, nokta1 = -1, nokta2 = 0, max = -1, toplamAlan = 0;
        double cevre = 0;
        double toplamCevre = 0;
        Point[] points = new Point[200];// noktaları bi dizide tutarız
        public Form1()
        {
            InitializeComponent();
        }

        private void panel1_MouseClick(object sender, MouseEventArgs e)
        {
            Point p = new Point(e.X, e.Y);
            x = p.X;
            y = p.Y;// x ve y koordinatlarını değişkenlere atıp
            panel1.Invalidate();// paneli refresh yapar
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = panel1.CreateGraphics();
            Pen p = new Pen(Color.White);//çizgi rengi
            g.DrawEllipse(p, x - 3, y - 3, 6, 6);// tıklandıgında çıkan kare ve çember iç içe
            g.DrawRectangle(p, x - 3, y - 3, 6, 6);// olması için bu 2 kod gerekli

            points[i] = new Point(x, y);

            i++;
            if (nokta2 > 1)// 2. nokta 1 den büyük olmalıki bir önceki nokta ile şuandaki nokta
            {               // arasında çizgi çizebilelim
                //b.DrawPolygon(p, points);
                //g.DrawLine(p, points[nokta1].X, points[nokta1].Y, points[nokta2].X, points[nokta2].Y);
                for (int q = 2; q <= nokta2; q++)
                {
                    int q2 = q - 1; // her seferinde ilk noktadan son noktaya kadar çiziyor
                    g.DrawLine(p, points[q2].X, points[q2].Y, points[q].X, points[q].Y);
                    
                }
            }
            if (i != 1) // bütün koordinatları listboxa ekliyor 1 de 0,0 var onu eklemiyor
                listBox1.Items.Add(" X : " + x + " ~ Y : " + y);
            nokta1++; nokta2++; max++;
            // SON KOORDİNAT İLE İLK GİRİLEN KOORDİNAT BİRBİRİNE 5 PX DEN DAHA AZ YAKINSA BİTİRİR.
            if (points[max].X > points[1].X - 5 && points[max].X < points[1].X + 5 &&
                points[max].Y > points[1].Y - 5 && points[max].Y < points[1].Y + 5 && max > 2)
            {
                for (i = 2; i <= max; i++)
                {
                    //MessageBox.Show(points[i].ToString());

                    int k = i - 1;
                    nokta1 = points[k].X;//1. noktanın x'i
                    nokta2 = points[k].Y;//1. noktanın y'si
                    int nokta3 = points[i].X;// 2. noktanın x'i
                    int nokta4 = points[i].Y;// 2. noktanın y'si
                    toplamAlan = toplamAlan + nokta1 * nokta4 - nokta2 * nokta3;
                    cevre=toplamCevre+((nokta1-nokta3)*(nokta1-nokta3)+(nokta2-nokta4)*(nokta2-nokta4));
                    cevre = Math.Sqrt(cevre);
                    toplamCevre = toplamCevre + cevre;
                    // pdfdeki hesaplamaları burda yapıyoruz
                }
                if (toplamAlan < 0)
                    toplamAlan = toplamAlan * -1;
                label1.Text = (toplamAlan/2).ToString();
                label2.Text = toplamCevre.ToString();
                MessageBox.Show("Alan : " + toplamAlan / 2+"\nÇevre : "+Convert.ToInt32(toplamCevre),"Hesaplandı",MessageBoxButtons.OK,MessageBoxIcon.Asterisk);
                this.Close();
                
            }

        }
    }
        
}
