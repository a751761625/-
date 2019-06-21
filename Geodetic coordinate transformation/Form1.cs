using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace Geodetic_coordinate_transformation
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            Struct.Ellipse _elsp = Struct.SetEllipse(comboBox2.SelectedIndex);
            textBox12.Text = _elsp.Name;
            textBox11.Text = _elsp._a.ToString("F4");
            textBox10.Text = _elsp._b.ToString("F6");
            textBox9.Text = _elsp._e2.ToString("F12");
            textBox8.Text = _elsp._e1.ToString("F12");
            textBox7.Text = _elsp._f.ToString("F9");
            
        }

        private void 退出ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        

        private void 打开并导入经纬度坐标ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            listView1.Items.Clear();
            listView2.Items.Clear();
            OpenFileDialog openDlg = new OpenFileDialog();
            // 指定打开文本文件（后缀名为txt）
            openDlg.Filter = "文本文件|*.txt";
            if (openDlg.ShowDialog() == DialogResult.OK)
            {
                // 读出文本文件的所以行
                string[] lines = File.ReadAllLines(openDlg.FileName);
                // 先清空richtextBox1
                richTextBox1.Clear();
                // 在richtextBox1中显示
                foreach (string line in lines)
                {
                    richTextBox1.AppendText(line + Environment.NewLine);
                }
            }
            int row = richTextBox1.Lines.Length;
            string str = richTextBox1.Text;
            String[] str1 = str.Split(new char[2] { ',', '\n' });
            for (int i = 1; i <= row - 1; i++)
            {
                ListViewItem lvi = new ListViewItem();
                lvi.Text = i.ToString();
                lvi.SubItems.Add(str1[0 + (i - 1) * 5]);
                lvi.SubItems.Add(str1[1 + (i - 1) * 5]);
                lvi.SubItems.Add(str1[2 + (i - 1) * 5]);
                lvi.SubItems.Add(str1[3 + (i - 1) * 5]);
                lvi.SubItems.Add(str1[4 + (i - 1) * 5]);
                this.listView1.Items.Add(lvi);
            }
        }

        private void 打开ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            listView1.Items.Clear();
            listView2.Items.Clear();
            OpenFileDialog openDlg = new OpenFileDialog();
            // 指定打开文本文件（后缀名为txt）
            openDlg.Filter = "文本文件|*.txt";
            //int row=0;
            if (openDlg.ShowDialog() == DialogResult.OK)
            {
                // 读出文本文件的所以行
                string[] lines = File.ReadAllLines(openDlg.FileName);
                // 先清空richtextBox1
                //row = lines.Length;
                richTextBox1.Clear();
                // 在richtextBox1中显示
                foreach (string line in lines)
                {
                    richTextBox1.AppendText(line + Environment.NewLine);
                }
            }
            int row = richTextBox1.Lines.Length;
            string str = richTextBox1.Text;
            String[] str1 = str.Split(new char[2] { ',', '\n' });
            for (int i = 1; i <= row - 5; i++)
            {
                ListViewItem lvi = new ListViewItem();
                lvi.Text = i.ToString();
                lvi.SubItems.Add(str1[0 + (i - 1) * 5]);
                lvi.SubItems.Add(str1[1 + (i - 1) * 5]);
                lvi.SubItems.Add(str1[2 + (i - 1) * 5]);
                lvi.SubItems.Add(str1[3 + (i - 1) * 5]);
                lvi.SubItems.Add(str1[4 + (i - 1) * 5]);
                this.listView2.Items.Add(lvi);
            }
        }

        private void 批量转换为经纬度坐标ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int izCount = listView2.Items.Count;
            listView1.Items.Clear();
            for (int i = 0; i < izCount; i++)
            {
                string strName = listView2.Items[i].SubItems[1].Text;
                string strX = listView2.Items[i].SubItems[2].Text;
                string strY = listView2.Items[i].SubItems[3].Text;
                string strL0 = listView2.Items[i].SubItems[5].Text;
                double X = double.Parse(strX);
                double Y = double.Parse(strY);
                double L0 = double.Parse(strL0);
                AngleConvert aL0 = null;
                aL0 = new DmsToRad();
                aL0.Angle = L0;
                L0 = aL0.GetAngle;
                double a = double.Parse(textBox11.Text);
                double b = double.Parse(textBox10.Text);
                double e1 = double.Parse(textBox9.Text);
                double e2 = double.Parse(textBox8.Text);
                Gauss A = new Gauss(a, b, e1, e2);
                A.GetGPS(X, Y, L0);
                AngleConvert aL = null;
                aL = new RadToDms();
                aL.Angle = A.L;
                A.L = aL.GetAngle;
                AngleConvert aB = null;
                aB = new RadToDms();
                aB.Angle = A.B;
                A.B = aB.GetAngle;
                ListViewItem lvi = new ListViewItem();
                lvi.Text = (i + 1).ToString();
                lvi.SubItems.Add(strName);
                lvi.SubItems.Add(A.L.ToString());
                lvi.SubItems.Add(A.B.ToString());
                this.listView1.Items.Add(lvi);
            }
        }

        private void 批量转换为高斯投影坐标ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int izCount = listView1.Items.Count;
            listView2.Items.Clear();
            for (int i = 0; i < izCount; i++)
            {
                string strName = listView1.Items[i].SubItems[1].Text;
                string strL = listView1.Items[i].SubItems[2].Text;
                string strB = listView1.Items[i].SubItems[3].Text;
                string strL0 = listView1.Items[i].SubItems[5].Text;
                double L = double.Parse(strL);
                double B = double.Parse(strB);
                double L0 = double.Parse(strL0);
                AngleConvert aL = null;
                aL = new DmsToRad();
                aL.Angle = L;
                L = aL.GetAngle;
                AngleConvert aB = null;
                aB = new DmsToRad();
                aB.Angle = B;
                B = aB.GetAngle;
                AngleConvert aL0 = null;
                aL0 = new DmsToRad();
                aL0.Angle = L0;
                L0 = aL0.GetAngle;
                double a = double.Parse(textBox11.Text);
                double b = double.Parse(textBox10.Text);
                double e1 = double.Parse(textBox9.Text);
                double e2 = double.Parse(textBox8.Text);
                Gauss A = new Gauss(a, b, e1, e2);
                A.GetGauss(B, L, L0);
                ListViewItem lvi = new ListViewItem();
                lvi.Text = (i + 1).ToString();
                lvi.SubItems.Add(strName);
                lvi.SubItems.Add(A.x.ToString());
                lvi.SubItems.Add(A.y.ToString());
                this.listView2.Items.Add(lvi);

            }
        }

        private void 打开并导入换带计算坐标ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            listView3.Items.Clear();
            listView4.Items.Clear();
            OpenFileDialog openDlg = new OpenFileDialog();
            // 指定打开文本文件（后缀名为txt）
            openDlg.Filter = "文本文件|*.txt";
            if (openDlg.ShowDialog() == DialogResult.OK)
            {
                // 读出文本文件的所以行
                string[] lines = File.ReadAllLines(openDlg.FileName);
                // 先清空richtextBox1
                richTextBox1.Clear();
                // 在richtextBox1中显示
                foreach (string line in lines)
                {
                    richTextBox1.AppendText(line + Environment.NewLine);
                }
            }
            int row = richTextBox1.Lines.Length;
            string str = richTextBox1.Text;
            String[] str1 = str.Split(new char[2] { ',', '\n' });
            for (int i = 1; i <= row - 1; i++)
            {
                ListViewItem lvi = new ListViewItem();
                lvi.Text = i.ToString();
                lvi.SubItems.Add(str1[0 + (i - 1) * 5]);
                lvi.SubItems.Add(str1[1 + (i - 1) * 5]);
                lvi.SubItems.Add(str1[2 + (i - 1) * 5]);
                lvi.SubItems.Add(str1[3 + (i - 1) * 5]);
                lvi.SubItems.Add(str1[4 + (i - 1) * 5]);
                this.listView3.Items.Add(lvi);
            }
        }

        private void 保存高斯投影坐标ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                string path = string.Empty;             //文件路径
                SaveFileDialog SaveFile = new SaveFileDialog();
                SaveFile.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
                SaveFile.FilterIndex = 1;
                if (SaveFile.ShowDialog() == DialogResult.OK)
                {
                    path = SaveFile.FileName;

                    if (path != string.Empty)
                    {
                        using (System.IO.FileStream file = new System.IO.FileStream(path, System.IO.FileMode.Create, System.IO.FileAccess.Write))
                        {
                            using (System.IO.TextWriter text = new System.IO.StreamWriter(file, System.Text.Encoding.Default))
                            {

                                string Ttext = "";
                                for (int i = 0; i < listView2.Items.Count; i++)
                                {
                                    for (int j = 1; j < listView2.Items[i].SubItems.Count; j++)
                                    {

                                        if ((j) % 3 == 0)
                                            Ttext = Ttext + listView2.Items[i].SubItems[j].Text + "\r\n";
                                        else
                                            Ttext = Ttext + listView2.Items[i].SubItems[j].Text + ',';
                                    }
                                }
                                text.Write(Ttext);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void 保存经纬度坐标ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                string path = string.Empty;             //文件路径
                SaveFileDialog SaveFile = new SaveFileDialog();
                SaveFile.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
                SaveFile.FilterIndex = 1;
                if (SaveFile.ShowDialog() == DialogResult.OK)
                {
                    path = SaveFile.FileName;

                    if (path != string.Empty)
                    {
                        using (System.IO.FileStream file = new System.IO.FileStream(path, System.IO.FileMode.Create, System.IO.FileAccess.Write))
                        {
                            using (System.IO.TextWriter text = new System.IO.StreamWriter(file, System.Text.Encoding.Default))
                            {

                                string Ttext = "";
                                for (int i = 0; i < listView1.Items.Count; i++)
                                {
                                    for (int j = 1; j < listView1.Items[i].SubItems.Count; j++)
                                    {

                                        if ((j) % 3 == 0)
                                            Ttext = Ttext + listView1.Items[i].SubItems[j].Text + "\r\n";
                                        else
                                            Ttext = Ttext + listView1.Items[i].SubItems[j].Text + ',';
                                    }
                                }
                                text.Write(Ttext);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void 度带转3度带ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int izCount = listView3.Items.Count;
            listView1.Items.Clear();
            for (int i = 0; i < izCount; i++)
            {
                string strName = listView3.Items[i].SubItems[1].Text;
                string strX = listView3.Items[i].SubItems[2].Text;
                string strY = listView3.Items[i].SubItems[3].Text;
                string strL0 = listView3.Items[i].SubItems[4].Text;
                double X = double.Parse(strX);
                double Y = double.Parse(strY);
                double L0 = double.Parse(strL0);
                AngleConvert aL0 = null;
                aL0 = new DmsToRad();
                aL0.Angle = L0;
                L0 = aL0.GetAngle;
                double a = double.Parse(textBox11.Text);
                double b = double.Parse(textBox10.Text);
                double e1 = double.Parse(textBox9.Text);
                double e2 = double.Parse(textBox8.Text);
                Gauss A = new Gauss(a, b, e1, e2);
                A.GetGPS(X, Y, L0);
                AngleConvert aL = null;
                aL = new RadToDms();
                aL.Angle = A.L;
                A.L = aL.GetAngle;
                AngleConvert aB = null;
                aB = new RadToDms();
                aB.Angle = A.B;
                A.B = aB.GetAngle;
                ListViewItem lvi = new ListViewItem();
                lvi.Text = (i + 1).ToString();
                lvi.SubItems.Add(strName);
                lvi.SubItems.Add(A.L.ToString());
                lvi.SubItems.Add(A.B.ToString());
                this.listView2.Items.Add(lvi);
            }
            int izCount1 = listView2.Items.Count;
            for (int q = 0; q < izCount1; q++)
            {
                string strName1 = listView2.Items[q].SubItems[1].Text;
                string strL = listView2.Items[q].SubItems[2].Text;
                string strB = listView2.Items[q].SubItems[3].Text;
                string strL2 = listView3.Items[q].SubItems[4].Text;
                double L = double.Parse(strL);
                double B = double.Parse(strB);
                double L2 = double.Parse(strL2);
                AngleConvert aL1 = null;
                aL1 = new DmsToRad();
                aL1.Angle = L;
                L = aL1.GetAngle;
                AngleConvert aB1 = null;
                aB1 = new DmsToRad();
                aB1.Angle = B;
                B = aB1.GetAngle;
                AngleConvert aL2 = null;
                aL2 = new DmsToRad();
                aL2.Angle = L2;
                L2 = aL2.GetAngle;
                double a1 = double.Parse(textBox11.Text);
                double b1 = double.Parse(textBox10.Text);
                double e11 = double.Parse(textBox9.Text);
                double e21 = double.Parse(textBox8.Text);
                Gauss A1 = new Gauss(a1, b1, e11, e21);
                A1.GetGauss(B, L, L2);
                ListViewItem lvi1 = new ListViewItem();
                lvi1.Text = (q + 1).ToString();
                lvi1.SubItems.Add(strName1);
                lvi1.SubItems.Add(A1.x.ToString());
                lvi1.SubItems.Add(A1.y.ToString());
                this.listView4.Items.Add(lvi1);
            }
            listView2.Items.Clear();
        }

        private void 度带转6度带ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int izCount = listView3.Items.Count;
            listView1.Items.Clear();
            for (int i = 0; i < izCount; i++)
            {
                string strName = listView3.Items[i].SubItems[1].Text;
                string strX = listView3.Items[i].SubItems[2].Text;
                string strY = listView3.Items[i].SubItems[3].Text;
                string strL0 = listView3.Items[i].SubItems[4].Text;
                double X = double.Parse(strX);
                double Y = double.Parse(strY);
                double L0 = double.Parse(strL0);
                AngleConvert aL0 = null;
                aL0 = new DmsToRad();
                aL0.Angle = L0;
                L0 = aL0.GetAngle;
                double a = double.Parse(textBox11.Text);
                double b = double.Parse(textBox10.Text);
                double e1 = double.Parse(textBox9.Text);
                double e2 = double.Parse(textBox8.Text);
                Gauss A = new Gauss(a, b, e1, e2);
                A.GetGPS(X, Y, L0);
                AngleConvert aL = null;
                aL = new RadToDms();
                aL.Angle = A.L;
                A.L = aL.GetAngle;
                AngleConvert aB = null;
                aB = new RadToDms();
                aB.Angle = A.B;
                A.B = aB.GetAngle;
                ListViewItem lvi = new ListViewItem();
                lvi.Text = (i + 1).ToString();
                lvi.SubItems.Add(strName);
                lvi.SubItems.Add(A.L.ToString());
                lvi.SubItems.Add(A.B.ToString());
                this.listView2.Items.Add(lvi);
            }
            int izCount1 = listView2.Items.Count;
            for (int q = 0; q < izCount1; q++)
            {
                string strName1 = listView2.Items[q].SubItems[1].Text;
                string strL = listView2.Items[q].SubItems[2].Text;
                string strB = listView2.Items[q].SubItems[3].Text;
                string strL2 = listView3.Items[q].SubItems[4].Text;
                double L = double.Parse(strL);
                double B = double.Parse(strB);
                double L2 = double.Parse(strL2);
                AngleConvert aL1 = null;
                aL1 = new DmsToRad();
                aL1.Angle = L;
                L = aL1.GetAngle;
                AngleConvert aB1 = null;
                aB1 = new DmsToRad();
                aB1.Angle = B;
                B = aB1.GetAngle;
                AngleConvert aL2 = null;
                aL2 = new DmsToRad();
                aL2.Angle = L2;
                L2 = aL2.GetAngle;
                double a1 = double.Parse(textBox11.Text);
                double b1 = double.Parse(textBox10.Text);
                double e11 = double.Parse(textBox9.Text);
                double e21 = double.Parse(textBox8.Text);
                Gauss A1 = new Gauss(a1, b1, e11, e21);
                A1.GetGauss(B, L, L2);
                ListViewItem lvi1 = new ListViewItem();
                lvi1.Text = (q + 1).ToString();
                lvi1.SubItems.Add(strName1);
                lvi1.SubItems.Add(A1.x.ToString());
                lvi1.SubItems.Add(A1.y.ToString());
                this.listView4.Items.Add(lvi1);
            }
            listView2.Items.Clear();
        }

        private void 保存换带计算后坐标ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                string path = string.Empty;             //文件路径
                SaveFileDialog SaveFile = new SaveFileDialog();
                SaveFile.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
                SaveFile.FilterIndex = 1;
                if (SaveFile.ShowDialog() == DialogResult.OK)
                {
                    path = SaveFile.FileName;

                    if (path != string.Empty)
                    {
                        using (System.IO.FileStream file = new System.IO.FileStream(path, System.IO.FileMode.Create, System.IO.FileAccess.Write))
                        {
                            using (System.IO.TextWriter text = new System.IO.StreamWriter(file, System.Text.Encoding.Default))
                            {

                                string Ttext = "";
                                for (int i = 0; i < listView4.Items.Count; i++)
                                {
                                    for (int j = 1; j < listView4.Items[i].SubItems.Count; j++)
                                    {

                                        if ((j) % 3 == 0)
                                            Ttext = Ttext + listView4.Items[i].SubItems[j].Text + "\r\n";
                                        else
                                            Ttext = Ttext + listView4.Items[i].SubItems[j].Text + ',';
                                    }
                                }
                                text.Write(Ttext);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
