using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CekilisYap
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        void DosyadanYukle(string dosyayeri)
        {
            string dosyaicerigi = File.ReadAllText(dosyayeri);
            string[] kisiler = dosyaicerigi.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);
            foreach (string kisi in kisiler)
            {
                KisiEkle(kisi);
            } 
        }

        void KisiGuncelle()
        {
            label1.Text = "Kişi Sayısı: " + listBox1.Items.Count;
            if (listBox1.Items.Count == 0)
            {
                numericUpDown1.Maximum = 0;
            }
            else
            {
                numericUpDown1.Maximum = listBox1.Items.Count - 1;
            }
        }
        
        void DosyayaYaz(string dosyayeri)
        {
            string yazilacaksey = "";
            foreach (string kisi in listBox1.Items)
            {
                if (yazilacaksey != "")
                {
                    yazilacaksey += Environment.NewLine + kisi;
                }
                else
                {
                    yazilacaksey += kisi; //Boş sıra koruması
                }
            }
            File.WriteAllText(dosyayeri, yazilacaksey);
        }

        void CekilisYap(List<string> kisiler, int yedeksayisi)
        {
            string listeayraci = "•";
            ListBox kutu = new ListBox();
            foreach (string kisi in kisiler)
            {
                kutu.Items.Add(kisi);
            }
            Random rnd = new Random();
            int kazanan = rnd.Next(0, kutu.Items.Count); //0 ila maximum kişi
            kutu.SelectedIndex = kazanan;
            string kazananisim = kutu.SelectedItem.ToString();
            kutu.Items.Remove(kazananisim);

            string yedekler = Environment.NewLine + "Yedekler: ";

            for (int i = 0; i < yedeksayisi; i++)
            {
                kazanan = rnd.Next(0, kutu.Items.Count);
                kutu.SelectedIndex = kazanan;
                yedekler += Environment.NewLine + listeayraci + kutu.SelectedItem.ToString();
                kutu.Items.Remove(kutu.SelectedItem.ToString());
            }

            MessageBox.Show("Kazanan: " + Environment.NewLine + kazananisim + yedekler);
        }

        void KisiEkle(string isim)
        {
            listBox1.Items.Add(isim);
            KisiGuncelle();
        }

        void KisiSil(string isim)
        {
            listBox1.Items.Remove(isim);
            KisiGuncelle();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //yükle
            openFileDialog1.Filter = "Çekiliş Dosyaları (*.cekilis)|*.cekilis";
            openFileDialog1.InitialDirectory = AppDomain.CurrentDomain.BaseDirectory; //Uygulamanın çalıştığı klasör
            openFileDialog1.DefaultExt = "cekilis";
            openFileDialog1.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //kaydet
            saveFileDialog1.Filter = "Çekiliş Dosyaları (*.cekilis)|*.cekilis";
            saveFileDialog1.InitialDirectory = AppDomain.CurrentDomain.BaseDirectory; //Uygulamanın çalıştığı klasör
            saveFileDialog1.DefaultExt = "cekilis";
            saveFileDialog1.ShowDialog();
        }

        private void saveFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            DosyayaYaz(saveFileDialog1.FileName);
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            DosyadanYukle(openFileDialog1.FileName);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            KisiEkle(textBox1.Text);
            textBox1.Text = "";
        }

        private void button4_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            KisiGuncelle();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            List<string> kisiler = new List<string>();
            foreach (string kisi in listBox1.Items)
            {
                kisiler.Add(kisi);
            }
            CekilisYap(kisiler, Convert.ToInt32(numericUpDown1.Value));
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedItem != null)
            {
                KisiSil(listBox1.SelectedItem.ToString());
            }
        }
    }
}
