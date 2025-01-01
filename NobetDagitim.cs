using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using iText.IO.Font;
using iText.Kernel.Font;
using iText.IO.Font.Constants;


namespace nobetdagitim
{

    public partial class NobetDagitim : Form
    {

         private void PdfKaydetme()
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "PDF Files|*.pdf";
            saveFileDialog.Title = "Nöbet Listesini PDF Olarak Kaydet";
            saveFileDialog.FileName = "NobetDagitimListesi.pdf";

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {

                    using (PdfWriter writer = new PdfWriter(saveFileDialog.FileName))
                    using (PdfDocument pdf = new PdfDocument(writer))
                    using (Document dokuman = new Document(pdf))
                    {

                        dokuman.Add(new Paragraph("Nöbet Dağıtım Listesi")
                            .SetFontSize(18)
                            .SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER));

                        dokuman.Add(new Paragraph("\n"));


                        for (int gun = 1; gun <= 7; gun++)
                        {

                            string gunAdi = gun switch
                            {
                                1 => "Pazartesi",
                                2 => "Salı",
                                3 => "Çarşamba",
                                4 => "Perşembe",
                                5 => "Cuma",
                                6 => "Cumartesi",
                                7 => "Pazar",
                                _ => ""
                            };

                            dokuman.Add(new Paragraph(gunAdi)
                                .SetFont(iText.Kernel.Font.PdfFontFactory.CreateFont(iText.IO.Font.Constants.StandardFonts.HELVETICA_BOLD))
                                .SetFontSize(14));



                            for (int saat = 1; saat <= 8; saat++)
                            {
                                TextBox txt = this.Controls.Find($"txtGun{gun}Saat{saat}", true).FirstOrDefault() as TextBox;
                                if (txt != null && !string.IsNullOrWhiteSpace(txt.Text))
                                {
                                    dokuman.Add(new Paragraph($"Saat {saat}: {txt.Text}")
                                        .SetFontSize(12));
                                }
                            }

                            dokuman.Add(new Paragraph("\n"));
                        }
                    }

                    MessageBox.Show("PDF başarıyla kaydedildi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"PDF kaydedilemedi: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        } 




        private MySqlConnection baglan;
        public NobetDagitim()
        {
            InitializeComponent();
            VeriGetir();
            baglan = new MySqlConnection("Server=localhost;Database=nobetDB;Uid=root;Pwd=root;");

        }
        private void Gorunurluk()
        {
            lblPzt.Visible = true;
            lblSali.Visible = true;
            lblCars.Visible = true;
            lblPers.Visible = true;
            lblCum.Visible = true;
            lblCmt.Visible = true;
            lblPaz.Visible = true;

            lblSaat1.Visible = true;
            lblSaat2.Visible = true;
            lblSaat3.Visible = true;
            lblSaat4.Visible = true;
            lblSaat5.Visible = true;
            lblSaat6.Visible = true;
            lblSaat7.Visible = true;
            lblSaat8.Visible = true;

            txtGun1Saat1.Visible = true;
            txtGun1Saat2.Visible = true;
            txtGun1Saat3.Visible = true;
            txtGun1Saat4.Visible = true;
            txtGun1Saat5.Visible = true;
            txtGun1Saat6.Visible = true;
            txtGun1Saat7.Visible = true;
            txtGun1Saat8.Visible = true;

            txtGun2Saat1.Visible = true;
            txtGun2Saat2.Visible = true;
            txtGun2Saat3.Visible = true;
            txtGun2Saat4.Visible = true;
            txtGun2Saat5.Visible = true;
            txtGun2Saat6.Visible = true;
            txtGun2Saat7.Visible = true;
            txtGun2Saat8.Visible = true;

            txtGun3Saat1.Visible = true;
            txtGun3Saat2.Visible = true;
            txtGun3Saat3.Visible = true;
            txtGun3Saat4.Visible = true;
            txtGun3Saat5.Visible = true;
            txtGun3Saat6.Visible = true;
            txtGun3Saat7.Visible = true;
            txtGun3Saat8.Visible = true;

            txtGun4Saat1.Visible = true;
            txtGun4Saat2.Visible = true;
            txtGun4Saat3.Visible = true;
            txtGun4Saat4.Visible = true;
            txtGun4Saat5.Visible = true;
            txtGun4Saat6.Visible = true;
            txtGun4Saat7.Visible = true;
            txtGun4Saat8.Visible = true;

            txtGun5Saat1.Visible = true;
            txtGun5Saat2.Visible = true;
            txtGun5Saat3.Visible = true;
            txtGun5Saat4.Visible = true;
            txtGun5Saat5.Visible = true;
            txtGun5Saat6.Visible = true;
            txtGun5Saat7.Visible = true;
            txtGun5Saat8.Visible = true;

            txtGun6Saat1.Visible = true;
            txtGun6Saat2.Visible = true;
            txtGun6Saat3.Visible = true;
            txtGun6Saat4.Visible = true;
            txtGun6Saat5.Visible = true;
            txtGun6Saat6.Visible = true;
            txtGun6Saat7.Visible = true;
            txtGun6Saat8.Visible = true;

            txtGun7Saat1.Visible = true;
            txtGun7Saat2.Visible = true;
            txtGun7Saat3.Visible = true;
            txtGun7Saat4.Visible = true;
            txtGun7Saat5.Visible = true;
            txtGun7Saat6.Visible = true;
            txtGun7Saat7.Visible = true;
            txtGun7Saat8.Visible = true;
        }
        private void VeriGetir()
        {
            string connectionString = "Server=localhost;Database=nobetDB;Uid=root;Pwd=root;";

            using (MySqlConnection baglan = new MySqlConnection(connectionString))
            {
                try
                {
                    baglan.Open();

                    string sorgu = "SELECT * FROM Personel";
                    MySqlDataAdapter adapter = new MySqlDataAdapter(sorgu, baglan);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    dgvPersonel.DataSource = dt;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Hata: " + ex.Message);
                }
            }
        }


        private void btnEkle_Click(object sender, EventArgs e)
        {
            try
            {
                string sorgu = "INSERT INTO Personel (ad, soyad, sehir) VALUES (@Ad, @Soyad, @Sehir)";
                MySqlCommand komut = new MySqlCommand(sorgu, baglan);
                komut.Parameters.AddWithValue("@Ad", txtad.Text);
                komut.Parameters.AddWithValue("@Soyad", txtsoyad.Text);
                komut.Parameters.AddWithValue("@Sehir", txtsehir.Text);

                baglan.Open();
                komut.ExecuteNonQuery();
                baglan.Close();

                VeriGetir();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            try
            {
                int id = Convert.ToInt32(dgvPersonel.CurrentRow.Cells[0].Value);
                string sorgu = "DELETE FROM Personel WHERE ID=@ID";
                MySqlCommand komut = new MySqlCommand(sorgu, baglan);
                komut.Parameters.AddWithValue("@ID", id);

                baglan.Open();
                komut.ExecuteNonQuery();
                baglan.Close();

                VeriGetir();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message);
            }
        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            try
            {
                int id = Convert.ToInt32(dgvPersonel.CurrentRow.Cells[0].Value);
                string sorgu = "UPDATE Personel SET ad=@Ad, soyad=@Soyad, sehir=@Sehir WHERE ID=@ID";
                MySqlCommand komut = new MySqlCommand(sorgu, baglan);
                komut.Parameters.AddWithValue("@Ad", txtad.Text);
                komut.Parameters.AddWithValue("@Soyad", txtsoyad.Text);
                komut.Parameters.AddWithValue("@Sehir", txtsehir.Text);
                komut.Parameters.AddWithValue("@ID", id);

                baglan.Open();
                komut.ExecuteNonQuery();
                baglan.Close();

                VeriGetir();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message);
            }
        }

        private void dgvPersonel_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            txtad.Text = dgvPersonel.CurrentRow.Cells[1].Value.ToString();
            txtsoyad.Text = dgvPersonel.CurrentRow.Cells[2].Value.ToString();
            txtsehir.Text = dgvPersonel.CurrentRow.Cells[3].Value.ToString();

        }

        private void btnDagit_Click(object sender, EventArgs e)
        {
            List<string> personeller = new List<string>();
            foreach (DataGridViewRow row in dgvPersonel.Rows)
            {
                if (row.Cells[1].Value != null)
                    personeller.Add(row.Cells[1].Value.ToString() + " " + row.Cells[2].Value.ToString());
            }

            Random rnd = new Random();
            HashSet<string> atananPersonel = new HashSet<string>();

            // Haftanın 7 günü, günde 8 saat
            for (int gun = 1; gun <= 7; gun++)
            {
                for (int saat = 1; saat <= 8; saat++)
                {
                    if (personeller.Count == 0)
                        break;

                    string secilen = personeller[rnd.Next(personeller.Count)];
                    while (atananPersonel.Contains(secilen))
                    {
                        secilen = personeller[rnd.Next(personeller.Count)];
                    }

                    atananPersonel.Add(secilen);

                    TextBox txt = this.Controls.Find($"txtGun{gun}Saat{saat}", true).FirstOrDefault() as TextBox;
                    if (txt != null)
                        txt.Text = secilen;
                }

                Gorunurluk();
            }
        }

        private void btnkaydet_Click(object sender, EventArgs e)
        {
            PdfKaydetme();
        }
    }
}
