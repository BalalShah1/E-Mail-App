using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Mail;
using System.Net;
using System.Net.Sockets;

namespace EmailClient
{
    public partial class Form1 : Form
    {
        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn
        (
            int nLeftRect,     // x-coordinate of upper-left corner
            int nTopRect,      // y-coordinate of upper-left corner
            int nRightRect,    // x-coordinate of lower-right corner
            int nBottomRect,   // y-coordinate of lower-right corner
            int nWidthEllipse, // height of ellipse
            int nHeightEllipse // width of ellipse
        );

        public Form1()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None;
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 20, 20));
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {


        }

        private void label7_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBox4.Text == "" || textBox5.Text == "" || textBox1.Text == "" || textBox2.Text == "" || textBox3.Text == "")
                {
                    MessageBox.Show("All fields need to have date before sending.");
                }


                if (!textBox1.Text.Contains("@gmail.com"))
                {
                    MessageBox.Show("You need to provide an email @gmail.com");
                    return;
                }

                button1.Enabled = false;
                var sendMessage = new MailMessage
                {
                    From = new MailAddress(textBox1.Text),
                    Subject = textBox5.Text,
                    Body = textBox4.Text
                };

                foreach (var s in textBox3.Text.Split(';'))
                {
                    sendMessage.To.Add(s);
                }

                var client = new SmtpClient
                {
                    Credentials = new NetworkCredential(textBox1.Text, textBox2.Text),
                    Host = "smtp.gmail.com",
                    Port = 587,
                    EnableSsl = true
                };
                client.Send(sendMessage);
            }

            catch
            {
                MessageBox.Show(
                    "There was an error sending the message, check if you have entered the credentials correctly.",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                button1.Enabled = true;
            }

        }

        private void label8_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }
    }
}


