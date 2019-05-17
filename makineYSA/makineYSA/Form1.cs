using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace makineYSA
{
    public partial class YSA : Form
    {
        /* BAŞLANGIÇ: Projede kullanılacak dizilerin ve sabitlerin oluşturulması.  */
        int araKatmanSayisi = 2;
        double[,] x = new double[8, 3];
        double[,] t = new double[8, 1];
        double[,] w_ = new double[3, 2];
        double[,] w = new double[2, 1];
        double[,] b_ = new double[2, 1];
        double[] b = new double[1];
        double[,] oP_ = new double[1, 2];
        double[] oP = new double[1];
        double alfa = 1;

        double giris1;
        double giris2;
        double giris3;

        int iterasyonSayisi;
        int epochSayisi;

        /* BİTİŞ: Projede kullanılacak dizilerin oluşturulması.  */
        public YSA()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
            /* BAŞLANGIÇ: Problemde bize verilen giriş, çıkış ve ilk ağırlık değerlerinin atanması. */
          
           
     
            t[0, 0] = 0;
            t[1, 0] = 1;
            t[2, 0] = 0;
            t[3, 0] = 0;
            t[4, 0] = 0;
            t[5, 0] = 0;
            t[6, 0] = 1;
            t[7, 0] = 1;

            w_[0, 0] = -2.11;
            w_[0, 1] = 0.69;
            w_[1, 0] = 1.83;
            w_[1, 1] = 1.12;
            w_[2, 0] = 1.49;
            w_[2, 1] = 1.97;

            w[0, 0] = -2.89;
            w[1, 0] = -1.36;

            b_[0, 0] = -0.24;
            b_[1, 0] = -2.4;

            b[0] = -2.12;

            /* BİTİŞ: Problemde bize verilen giriş, çıkış ve ilk ağırlık değerlerinin atanması. */
            iterasyonSayisi = 0;
            epochSayisi = 0;
           
           
        }

        private void verial_Click(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {

                giris1 = 0;
                giris2 = 0;
                giris3 = 0;
                verial.Visible = true;
            }
            else
            {
                x[0, 0] = 0;
                x[0, 1] = 0;
                x[0, 2] = 0;
            }
            if (radioButton2.Checked)
            {
                verial.Visible = true;
                giris1 = 0;
                giris2 = 0;
                giris3 = 1;
            }
            else
            {
                x[1, 0] = 0;
                x[1, 1] = 0;
                x[1, 2] = 1;
            }
            if (radioButton3.Checked)
            {
                verial.Visible = true;
                giris1 = 0;
                giris2 = 1;
                giris3 = 0;
            }
            else
            {
                x[2, 0] = 0;
                x[2, 1] = 1;
                x[2, 2] = 0;
            }
            if (radioButton4.Checked)
            {
                verial.Visible = true;
                giris1 = 0;
                giris2 = 1;
                giris3 = 1;
            }
            else
            {
                x[3, 0] = 0;
                x[3, 1] = 1;
                x[3, 2] = 1;
            }
            if (radioButton5.Checked)
            {
                verial.Visible = true;
                giris1 = 1;
                giris2 = 0;
                giris3 = 0;
            }
            else
            {
                x[4, 0] = 1;
                x[4, 1] = 0;
                x[4, 2] = 0;
            }
            if (radioButton6.Checked)
            {
                verial.Visible = true;
                giris1 = 1;
                giris2 = 0;
                giris3 = 1;
            }
            else
            {
                
                x[5, 0] = 1;
                x[5, 1] = 0;
                x[5, 2] = 1;
            }
            if (radioButton7.Checked)
            {
                verial.Visible = true;
                giris1 = 1;
                giris2 = 1;
                giris3 = 0;
            }
            else
            {
                x[6, 0] = 1;
                x[6, 1] = 1;
                x[6, 2] = 0;
            }
            if (radioButton8.Checked)
            {
                verial.Visible = true;
                giris1 = 1;
                giris2 = 1;
                giris3 = 1;
            }
            else
            {
                x[7, 0] = 1;
                x[7, 1] = 1;
                x[7, 2] = 1;

            }
            while (epochSayisi < 0000)
            {


                /* BAŞLANGIÇ: Ara nöron çıkışlarının hesaplanması. */
                for (int i = 0; i < araKatmanSayisi; i++)
                {
                    oP_[0, i] = sigmoidHesapla(aracikisBul(x, w_, b_, i, iterasyonSayisi));//2 gizli nöron çıkışını buluyor.
                }
                /* BİTİŞ: Ara nöron çıkışlarının hesaplanması. */
                /* BAŞLANGIÇ: Çıkış nöronunun çıkışının hesaplanması. */
                oP[0] = sigmoidHesapla(soncikisBul(oP_, w, b));//son nöronun çıkışını buluyor.
                /* BİTİŞ: Çıkış nöronunun çıkışının hesaplanması. */

                double e_ = 0.5 * (oP[0] - t[iterasyonSayisi, 0]) * (oP[0] - t[iterasyonSayisi, 0]);/*Çıkış hatasının hesaplanması. */


                if (e_ != 0)
                {
                    double geriyeYayilmaHataDegeri = geriyeYayilmaHatasi(t[iterasyonSayisi, 0], oP[0]);//Çıkış hatası 0'dan farklı ise geriye yayılma hatasının hesaplanması.

                    /* BAŞLANGIÇ: Çıkış nörona giden ağırlıkların ve bias ağırlığının geriye yayılma hatası ile hesaplanması ve güncellenmesi.*/
                    for (int i = 0; i < araKatmanSayisi; i++)
                    {
                        w[i, 0] = w[i, 0] + deltaW(geriyeYayilmaHataDegeri, oP_[0, i], alfa);

                    }


                    b[0] = b[0] + deltaB(geriyeYayilmaHataDegeri, alfa);//son katmanın delta bias'ı hesaplanıyor.
                                                                        /* BİTİŞ: Çıkış nörona giden ağırlıkların ve bias ağırlığının geriye yayılma hatası ile hesaplanması ve güncellenmesi*/


                    double[,] gizliKatmanGeriyeYayilmaHataDegeri = new double[2, 1];//Gizli katman da ki nöronun geriye yayılma hatasının hesaplanması.
                                                                                    /* BAŞLANGIÇ: Gizli nöronlara giden ağırlıkların ve bias ağırlığının geriye yayılma hatası ile hesaplanması ve güncellenmesi.*/
                    for (int i = 0; i < araKatmanSayisi; i++)
                    {
                        gizliKatmanGeriyeYayilmaHataDegeri[i, 0] = gizliKatmanGeriyeYayilmaHatasi(oP_[0, i], w[i, 0], geriyeYayilmaHataDegeri);
                        b_[i, 0] = b_[i, 0] + deltaB(gizliKatmanGeriyeYayilmaHataDegeri[i, 0], alfa);
                        for (int j = 0; j < 3; j++)
                        {
                            w_[j, i] = w_[j, i] + deltaW_(gizliKatmanGeriyeYayilmaHataDegeri[i, 0], x[iterasyonSayisi, j], alfa);
                        }
                    }
                    /* BİTİŞ: Gizli nöronlara giden ağırlıkların ve bias ağırlığının geriye yayılma hatası ile hesaplanması ve güncellenmesi.*/


                }
                /* BAŞLANGIÇ: Form Sayfasın a ki tablonun doldurulması.*/
                tabloDoldur(iterasyonSayisi, x[iterasyonSayisi, 0], x[iterasyonSayisi, 1], x[iterasyonSayisi, 2], w_[0, 0], w_[0, 1], w_[1, 0], w_[1, 1], w_[2, 0], w_[2, 1], w[0, 0], w[1, 0], b_[0, 0], b_[1, 0], b[0], t[iterasyonSayisi, 0], oP[0]);
                /* BİTİŞ: Form Sayfasın a ki tablonun doldurulması.*/
                iterasyonSayisi++;//Her bir adımdan sonra sıradaki iterasyona geçmesi için iterasyon sayısının arttırılması.
                if (iterasyonSayisi == 7)
                {
                    iterasyonSayisi = 0;//Toplam 7 tane iterasyon olduğu için iterasyon sayısı 8 olduğunda tekrar iteralson sayısına 0 atanması ve epoch sayısının arttırılması.
                    epochSayisi++;

                }
            }
            MessageBox.Show(epochSayisi + " epoch hesaplandı.");
            
          
        }
        /* Başlangıç: Form Sayfasın a ki tablonun doldurulması.*/
        public void tabloDoldur(int iterasyon, double x1, double x2, double x3, double w11, double w12, double w21, double w22, double w31, double w32, double w13,double w23, double wb1, double wb2, double wb3, double y, double bulunanY)
        {
            tblAgirlikDegerleri.Rows.Add(iterasyon, x1, x2, x3, w11, w12, w21, w22, w31, w32, w13, w23, wb1, wb2, wb3, y, bulunanY);
        }
        /* Başlangıç: Form Sayfasın a ki tablonun doldurulması.*/
        /* Başlangıç:Ağırlıkları güncellerken eklenecek olan deltaların hesaplanması.*/
        public static double deltaB(double hata,double alfa)
        {
            double deltaB = hata * alfa;
            return deltaB;
        }
        public static double deltaW(double hata, double oP, double alfa)
        {
            double deltaW = alfa * hata * oP;
            return deltaW;
        }
        public static double deltaW_(double hata, double girisDegeri, double alfa)
        {
            double deltaW = alfa * hata * girisDegeri;
            return deltaW;
        }
        /* Bitiş:Ağırlıkları güncellerken eklenecek olan deltaların hesaplanması.*/
        public static double geriyeYayilmaHatasi(double gercek, double bulunan)//Geriye yayılma hatasının hesaplanması.
        {
            double hata = (gercek - bulunan) * (bulunan) * (1 - bulunan);

            return hata;
        }
        public static double gizliKatmanGeriyeYayilmaHatasi(double gizliKatmanÇıkışDeğeri, double gizliKatmandanCikanAgirlik, double geriyeYayilmaHatasi)// Gizli katman nöronlarının geriye yayılma hatasının hesaplanması.
        {
            double hata = gizliKatmanÇıkışDeğeri*(1-gizliKatmanÇıkışDeğeri)*gizliKatmandanCikanAgirlik*geriyeYayilmaHatasi;

            return hata;
        }
        public static double sigmoidHesapla(double sayi)//Nöron çıkış değerlerinin sigmoid fonksiyonundan elde edilen değerlerin hesaplanması.
        {
            double sonuc = 1 / (1 + (Math.Exp(-1 * sayi)));

            return sonuc;
        }

        public static double aracikisBul(double[,] x, double[,] w_, double[,] b_, int k, int iterasyoSay)// Gizli katman nöronlarının çıkışlarının hesaplanması.
        {
            double toplam = 0;

            for (int j = 0; j < 3; j++)
            {
                toplam = toplam + x[iterasyoSay, j] * w_[j, k];
            }
            toplam = toplam + b_[k, 0];



            return toplam;
        }
        
        public static double aracikisBulTest(double x1, double x2, double x3,double[,] w_, double[,] b_, int k)// Gizli katman nöronlarının çıkışlarının hesaplanması.
        {
            double toplam = 0;

            
                toplam = toplam + x1 * w_[0, k];
            toplam = toplam + x2 * w_[1, k];
            toplam = toplam + x3 * w_[2, k];
            toplam = toplam + b_[k, 0];



            return toplam;
        }
        public double soncikisBul(double[,] oP_, double[,] w, double[] b)//Çıkış nöronunun çıkışının hesaplanması.
        {
            double toplam = 0;

            for (int j = 0; j < araKatmanSayisi; j++)
            {
                toplam = toplam + oP_[0, j] * w[j, 0];
            }
            toplam = toplam + b[0];



            return toplam;
        }
        public double soncikisBulTest(double[,] oP_, double[,] w, double[] b)//Çıkış nöronunun çıkışının hesaplanması.
        {
            double toplam = 0;

            for (int j = 0; j < araKatmanSayisi; j++)
            {
                toplam = toplam + oP_[0, j] * w[j, 0];
            }
            toplam = toplam + b[0];



            return toplam;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnTestEt_Click(object sender, EventArgs e)
        {
            

        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

            double[,] aracikis = new double[1, 2];
            double[] cikis = new double[1];
            for (int i = 0; i < araKatmanSayisi; i++)
            {

                aracikis[0, i] = sigmoidHesapla(aracikisBulTest(giris1, giris2, giris3, w_, b_, i));//2 gizli nöron çıkışını buluyor.
            }
            /* BİTİŞ: Ara nöron çıkışlarının hesaplanması. */
            /* BAŞLANGIÇ: Çıkış nöronunun çıkışının hesaplanması. */
            cikis[0] = sigmoidHesapla(soncikisBulTest(aracikis, w, b));//son nöronun çıkışını buluyor.
                                                                       /* BİTİŞ: Çıkış nöronunun çıkışının hesaplanması. */
            label2.Text = cikis[0].ToString();

            

        }
    }
}
