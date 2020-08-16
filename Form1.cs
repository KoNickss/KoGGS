using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace KoBurner_Genie
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        public void wait(int milliseconds)
        {
            var timer1 = new System.Windows.Forms.Timer();
            if (milliseconds == 0 || milliseconds < 0) return;

            // Console.WriteLine("start wait timer");
            timer1.Interval = milliseconds;
            timer1.Enabled = true;
            timer1.Start();

            timer1.Tick += (s, e) =>
            {
                timer1.Enabled = false;
                timer1.Stop();
                // Console.WriteLine("stop wait timer");
            };

            while (timer1.Enabled)
            {
                Application.DoEvents();
            }
        }
        OpenFileDialog ofd = new OpenFileDialog();
        //konicks
        bool rl = true;
        private void button1_Click(object sender, EventArgs e)
        {
            ofd.Filter = "NES rom|*.nes";
            ofd.ShowDialog();
            panel1.Visible = false;
            panel2.Visible = true;
        }

        private void label3_Click(object sender, EventArgs e)
        {
            textBox1.Text += "A";
        }

        private void label4_Click(object sender, EventArgs e)
        {
            textBox1.Text += "P";
        }

        private void label5_Click(object sender, EventArgs e)
        {
            textBox1.Text += "Z";
        }

        private void label6_Click(object sender, EventArgs e)
        {
            textBox1.Text += "L";
        }

        private void label7_Click(object sender, EventArgs e)
        {
            textBox1.Text += "G";
        }

        private void label8_Click(object sender, EventArgs e)
        {
            textBox1.Text += "I";
        }

        private void label9_Click(object sender, EventArgs e)
        {
            textBox1.Text += "T";
        }

        private void label10_Click(object sender, EventArgs e)
        {
            textBox1.Text += "Y";
        }

        private void label11_Click(object sender, EventArgs e)
        {
            textBox1.Text += "E";
        }

        private void label12_Click(object sender, EventArgs e)
        {
            textBox1.Text += "O";
        }

        private void label13_Click(object sender, EventArgs e)
        {
            textBox1.Text += "X";
        }

        private void label14_Click(object sender, EventArgs e)
        {
            textBox1.Text += "U";
        }

        private void label15_Click(object sender, EventArgs e)
        {
            textBox1.Text += "K";
        }

        private void label16_Click(object sender, EventArgs e)
        {
            textBox1.Text += "S";
        }

        private void label17_Click(object sender, EventArgs e)
        {
            textBox1.Text += "V";
        }

        private void label18_Click(object sender, EventArgs e)
        {
            textBox1.Text += "N";
        }

        private void label19_Click(object sender, EventArgs e)
        {
            if(textBox1.TextLength >= 1){
                textBox1.Text = textBox1.Text.Substring(0, textBox1.Text.Length - 1);
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if(textBox1.TextLength >= 9 && rl == true)
            {
                textBox1.Text = textBox1.Text.Substring(0, textBox1.Text.Length - 1);
            }
            if(rl == false)
            {
                textBox1.Text = "Invalid Code";
                rl = true;
                wait(1000);
                textBox1.Text = "";
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string code = textBox1.Text;
            if(textBox1.TextLength != 6 && textBox1.TextLength != 8)
            {
                rl = false;
            }
            else
            {
                if (textBox1.TextLength == 6)
                {
                    byte[] codes = new byte[6];
                    for (int i = 0; i <= 5; i++)
                    {
                        switch (code[i])
                        {
                            case 'A':
                                codes[i] = 0x0;
                                break;
                            case 'P':
                                codes[i] = 0x1;
                                break;
                            case 'Z':
                                codes[i] = 0x2;
                                break;
                            case 'L':
                                codes[i] = 0x3;
                                break;
                            case 'G':
                                codes[i] = 0x4;
                                break;
                            case 'I':
                                codes[i] = 0x5;
                                break;
                            case 'T':
                                codes[i] = 0x6;
                                break;
                            case 'Y':
                                codes[i] = 0x7;
                                break;
                            case 'E':
                                codes[i] = 0x8;
                                break;
                            case 'O':
                                codes[i] = 0x9;
                                break;
                            case 'X':
                                codes[i] = 0xA;
                                break;
                            case 'U':
                                codes[i] = 0xB;
                                break;
                            case 'K':
                                codes[i] = 0xC;
                                break;
                            case 'S':
                                codes[i] = 0xD;
                                break;
                            case 'V':
                                codes[i] = 0xE;
                                break;
                            case 'N':
                                codes[i] = 0xF;
                                break;
                        }
                    }
                    textBox1.Text = "";
                    long adress = 0x8000 + ((codes[3] & 7) << 12) | ((codes[5] & 7) << 8) | ((codes[4] & 8) << 8) | ((codes[2] & 7) << 4) | ((codes[1] & 8) << 4) | (codes[4] & 7) | (codes[3] & 8);
                    ulong adressrom = Convert.ToUInt64(adress) - 0x7FF0;
                    //MessageBox.Show(adressrom.ToString("X4"));
                    int data = ((codes[1] & 7) << 4) | ((codes[0] & 8) << 4) | (codes[0] & 7) | (codes[5] & 8);
                    byte datarom = Convert.ToByte(data);
                    //MessageBox.Show(datarom.ToString());
                    BinaryWriter genie = new BinaryWriter(File.OpenWrite(ofd.FileName));
                    genie.BaseStream.Position = Convert.ToInt64(adressrom);
                    genie.Write(datarom);
                    genie.Close();
                    button2.Enabled = false;
                    textBox1.Text = "Write succesful";
                    wait(1000);
                    textBox1.Text = "";
                    button2.Enabled = true;
                }
                if(textBox1.TextLength == 8)
                {
                    byte[] codes = new byte[8];
                    for (int i = 0; i <= 7; i++)
                    {
                        switch (code[i])
                        {
                            case 'A':
                                codes[i] = 0x0;
                                break;
                            case 'P':
                                codes[i] = 0x1;
                                break;
                            case 'Z':
                                codes[i] = 0x2;
                                break;
                            case 'L':
                                codes[i] = 0x3;
                                break;
                            case 'G':
                                codes[i] = 0x4;
                                break;
                            case 'I':
                                codes[i] = 0x5;
                                break;
                            case 'T':
                                codes[i] = 0x6;
                                break;
                            case 'Y':
                                codes[i] = 0x7;
                                break;
                            case 'E':
                                codes[i] = 0x8;
                                break;
                            case 'O':
                                codes[i] = 0x9;
                                break;
                            case 'X':
                                codes[i] = 0xA;
                                break;
                            case 'U':
                                codes[i] = 0xB;
                                break;
                            case 'K':
                                codes[i] = 0xC;
                                break;
                            case 'S':
                                codes[i] = 0xD;
                                break;
                            case 'V':
                                codes[i] = 0xE;
                                break;
                            case 'N':
                                codes[i] = 0xF;
                                break;
                        }
                    }
                    textBox1.Text = "";
                    long adress = 0x8000 + ((codes[3] & 7) << 12) | ((codes[5] & 7) << 8) | ((codes[4] & 8) << 8) | ((codes[2] & 7) << 4) | ((codes[1] & 8) << 4) | (codes[4] & 7) | (codes[3] & 8);
                    ulong adressrom = Convert.ToUInt64(adress) - 0x7FF0;
                    int data = ((codes[1] & 7) << 4) | ((codes[0] & 8) << 4) | (codes[0] & 7) | (codes[7] & 8);
                    byte datarom = Convert.ToByte(data);
                    int compare = ((codes[7] & 7) << 4) | ((codes[6] & 8) << 4) | (codes[6] & 7) | (codes[5] & 8);
                    byte comparerom = Convert.ToByte(compare);
                    BinaryReader comp = new BinaryReader(File.OpenRead(ofd.FileName));
                    comp.BaseStream.Position = Convert.ToInt64(adressrom);
                    byte readvalue = comp.ReadByte();
                    comp.Close();
                    if (readvalue == comparerom)
                    {
                        BinaryWriter genie = new BinaryWriter(File.OpenWrite(ofd.FileName));
                        genie.BaseStream.Position = Convert.ToInt64(adressrom);
                        genie.Write(datarom);
                        genie.Close();
                    }
                    button2.Enabled = false;
                    textBox1.Text = "Write succesful";
                    wait(1000);
                    textBox1.Text = "";
                    button2.Enabled = true;
                }
            }
        }
    }
}
