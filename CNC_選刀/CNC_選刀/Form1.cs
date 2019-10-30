using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;

using System.Windows.Forms;
using CNC_選刀.inc;
using utility;
namespace CNC_選刀
{

    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();
            this.AllowDrop = true;
            this.DragEnter += new DragEventHandler(Form1_DragEnter);
            this.DragDrop += new DragEventHandler(Form1_DragDrop);
        }
        CNC_Class CC;
        myinclude my;
        private void Form1_Load(object sender, EventArgs e)
        {
            my = new myinclude();
            CC = new CNC_Class();
            CC.version = "V1.0";
            CC.author = "羽山";
            this.Text = "CNC選刀程式 - " + CC.version;
            save_btn.Visible = false;
        }


        void Form1_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Copy;
            }
        }

        void Form1_DragDrop(object sender, DragEventArgs e)
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            //foreach (string file in files) Console.WriteLine(file);
            Console.WriteLine(files.Length);
            if (files.Length >= 2)
            {
                MessageBox.Show("一次只能處理一個檔案...");
                reset_data();
                return;
            }
            string sn = my.subname(files[0].ToString());

            if (sn != "nc" && sn != "txt")
            {
                MessageBox.Show("副檔名只能是 nc 或 txt");
                reset_data();
                return;
            }
            CC.orin_path = files[0].ToString();
            INPUT_PATH.Text = CC.orin_path;
            CC.data = my.b2s(my.file_get_contents(CC.orin_path));
            CC.data = my.trim(CC.data.Replace("\r", ""));
            CC.m_data = new List<string>();
            load_data();
        }
        void load_data()
        {
            var m = my.explode("\n", CC.data);
            for (int i = 0, max_i = m.Length; i < max_i; i++)
            {
                if (!my.is_istring_like(my.strtoupper(m[i].Trim()), "M6T"))
                {
                    if (CC.m_data.Count == 0)
                    {
                        CC.m_data.Add(m[i] + "\n");
                    }
                    else
                    {
                        CC.m_data[CC.m_data.Count - 1] += m[i] + "\n";
                    }
                }
                else
                {
                    CC.m_data[CC.m_data.Count - 1] = CC.m_data[CC.m_data.Count - 1].Trim();
                    CC.m_data.Add("");
                }
            }

            //MessageBox.Show(string.Join<string>("#######", CC.m_data));

            //MessageBox.Show(CC.m_data.Count.ToString()); 3 -> 4
            //載入畫面
            save_btn.Visible = true;
            load_ui();
        }
        void load_ui()
        {
            GBox.Controls.Clear();
            //List<TextBox> myTextboxList = new List<TextBox>();
            for (int i = 0; i < CC.m_data.Count - 1; i++)
            {
                //myTextboxList.Add(new TextBox());
                ComboBox a = new ComboBox();
                /*List<Item> items = new List<Item>();
                items.Add(new Item() { Text = "", Value = "1" });
                items.Add(new Item() { Text = "1.6K線刀", Value = "2" });
                items.Add(new Item() { Text = "切斷刀", Value = "3" });
                items.Add(new Item() { Text = "自銑刀", Value = "4" });
                items.Add(new Item() { Text = "基準圓棒", Value = "5" });
                items.Add(new Item() { Text = "磨邊刀", Value = "6" });
                */
                a.Items.Insert(0, "--請選擇--");
                a.Items.Insert(1, "1.3K線刀");
                a.Items.Insert(2, "1.6K線刀");
                a.Items.Insert(3, "切斷刀");
                a.Items.Insert(4, "自銑刀");
                a.Items.Insert(5, "基準圓棒");
                a.Items.Insert(6, "磨邊刀");
                //a.DataSource = items;
                a.DropDownStyle = ComboBoxStyle.DropDownList;
                a.Visible = true;
                //a.MultiColumn = false;
                a.Size = new System.Drawing.Size(200, 40);
                a.Name = "comboBox" + i.ToString();
                a.Font = new Font("微軟正黑體", 18, FontStyle.Bold);
                //a.BackColor = System.Drawing.Color.Orange;
                //a.ForeColor = System.Drawing.Color.Black;

                a.Location = new System.Drawing.Point(20, i * 50 + 40);
                a.SelectedIndex = 0;
                GBox.Controls.Add(a);


            }


        }
        void save_data()
        {
            MessageBox.Show("儲存了...");
        }
        void reset_data()
        {
            CC.orin_path = "";
            INPUT_PATH.Text = "";
            CC.data = "";
            load_data();

            save_btn.Visible = false;
        }

        private void save_btn_Click(object sender, EventArgs e)
        {
            int items = CC.m_data.Count - 1;
            string message = "";
            List<string> CBoxs = new List<string>();
            foreach (Control tb in GBox.Controls)
            {
                if (tb is ComboBox)
                {
                    ComboBox ctl = tb as ComboBox;
                    if (ctl.SelectedIndex == 0)
                    {
                        message += "請先選擇...";
                        break;
                    }
                    CBoxs.Add("M6T" + ctl.SelectedIndex);
                }
            }
            if (message != "")
            {
                MessageBox.Show(message);
                return;
            }
            List<string> output = new List<string>();
            for(int i=0;i<CC.m_data.Count-1;i++)
            {
                output.Add(CC.m_data[i]);
                output.Add(CBoxs[i]);
            }
            output.Add(CC.m_data[CC.m_data.Count - 1]);
            try
            {
                my.file_put_contents(CC.orin_path, my.s2b(my.implode("\r\n", output)));
                MessageBox.Show("儲存成功");
            }
            catch(Exception ex)
            {
                MessageBox.Show("儲存失敗...檔案目前被其他程式開啟了...");
            }
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {

        }
    }
}
