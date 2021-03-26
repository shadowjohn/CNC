using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;

using System.Windows.Forms;
using CNC_選刀.inc;
using utility;
using System.IO;

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
            CC.version = "V1.3";
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
        List<int> listIndex = new List<int>();
        static List<string> listData = new List<string>();
        void load_data()
        {
            //var m = my.explode("\n", CC.data);
            listIndex = new List<int>();
            var m = CC.data.Trim().Split(new string[] { "\n" }, StringSplitOptions.RemoveEmptyEntries);
            listData = new List<string>(m);
            for (int i = 0, max_i = m.Length; i < max_i; i++)
            {
                if (m[i].ToString().Trim().IndexOf("M6T") == 0)
                    listIndex.Add(i);
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
                ComboBox b = new ComboBox();


                /*List<Item> items = new List<Item>();
                items.Add(new Item() { Text = "", Value = "1" });
                items.Add(new Item() { Text = "2號刀座", Value = "2" });
                items.Add(new Item() { Text = "3號刀座", Value = "3" });
                items.Add(new Item() { Text = "4號刀座", Value = "4" });
                items.Add(new Item() { Text = "切斷刀", Value = "5" });
                items.Add(new Item() { Text = "磨邊刀", Value = "6" });
                */
                a.Items.Insert(0, "--請選擇--");
                a.Items.Insert(1, "1號刀座");
                a.Items.Insert(2, "2號刀座");
                a.Items.Insert(3, "3號刀座");
                a.Items.Insert(4, "4號刀座");
                a.Items.Insert(5, "切斷刀／自銑刀");
                a.Items.Insert(6, "磨邊刀");

                b.Items.Insert(0, "--請選擇--");
                b.Items.Insert(1, "     ＋");
                b.Items.Insert(2, "     －");
                //a.DataSource = items;
                a.DropDownStyle = ComboBoxStyle.DropDownList;
                b.DropDownStyle = ComboBoxStyle.DropDownList;

                a.Visible = true;
                b.Visible = true;
                //a.MultiColumn = false;
                a.Size = new System.Drawing.Size(200, 40);
                a.Name = "選刀_" + i;
                a.Font = new Font("微軟正黑體", 18, FontStyle.Bold);

                b.Size = new System.Drawing.Size(140, 40);
                b.Name = "選正負_" + i;
                b.Font = new Font("微軟正黑體", 18, FontStyle.Bold);

                //a.BackColor = System.Drawing.Color.Orange;
                //a.ForeColor = System.Drawing.Color.Black;

                a.Location = new System.Drawing.Point(20, i * 50 + 40);
                a.SelectedIndex = 0;
                //選刀改變
                EventArgs ea = new EventArgs();

                a.SelectedIndexChanged += new System.EventHandler(this.a_SelectedIndexChanged);

                GBox.Controls.Add(a);



                b.Location = new System.Drawing.Point(260, i * 50 + 40);
                b.SelectedIndex = 0;
                GBox.Controls.Add(b);

            }


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
            List<string> CBoxs_minus_plus = new List<string>();
            foreach (Control ta in GBox.Controls)
            {
                if (ta is ComboBox && my.is_string_like(ta.Name, "選刀"))
                {
                    ComboBox ctl = ta as ComboBox;
                    if (ctl.SelectedIndex == 0)
                    {
                        message += "請先選擇...";
                        break;
                    }
                    CBoxs.Add("M6T" + ctl.SelectedIndex);
                }
                if (ta is ComboBox && my.is_string_like(ta.Name, "正負"))
                {
                    ComboBox ctl = ta as ComboBox;
                    if (ctl.SelectedIndex == 0)
                    {
                        message += "請先選擇...";
                        break;
                    }
                    string minus_plus = "";
                    switch (ctl.SelectedIndex)
                    {
                        case 1:
                            minus_plus = "+";
                            break;
                        case 2:
                            minus_plus = "-";
                            break;
                    }
                    CBoxs_minus_plus.Add(minus_plus);
                }
            }
            if (message != "")
            {
                MessageBox.Show(message);
                return;
            }

            // 開始處理資料

            List<string> output = new List<string>();
            for (int i = 0; i < CC.m_data.Count - 1; i++)
            {
                //2020-07-14 增加處理，針對 G1 Z"數字"所有"+0.07"改為"-0.07"
                //放到 tmp 裡處理
                if (i != 0)
                {
                    string _tmp = CC.m_data[i];
                    _tmp = _tmp.Replace("G1 Z-", "G1 Z" + CBoxs_minus_plus[i - 1].ToString());
                    _tmp = _tmp.Replace("G1 Z+", "G1 Z" + CBoxs_minus_plus[i - 1].ToString());
                    CC.m_data[i] = _tmp;
                    //MessageBox.Show(CC.m_data[i]);
                }
                output.Add(CC.m_data[i]);
                output.Add(CBoxs[i]);
            }

            string tmp = CC.m_data[CC.m_data.Count - 1];

            tmp = tmp.Replace("G1 Z-", "G1 Z" + CBoxs_minus_plus[CBoxs_minus_plus.Count - 1]);
            tmp = tmp.Replace("G1 Z+", "G1 Z" + CBoxs_minus_plus[CBoxs_minus_plus.Count - 1]);

            CC.m_data[CC.m_data.Count - 1] = tmp;
            output.Add(CC.m_data[CC.m_data.Count - 1]);

            //2021-03-26 如果遇到M6T，下一行如果不是GoSub user_4，就加上 GoSub user_4
            for (int i = 0; i < output.Count; i++)
            {
                if (my.is_string_like(output[i], "M6T"))
                {
                    if (output[i + 1].ToLower() != "GoSub user_4".ToLower())
                    {
                        output[i] += "\r\n" + "GoSub user_4";
                    }
                }
            }

            try
            {
                //my.file_put_contents(CC.orin_path, my.s2b(my.implode("\r\n", output)));
                File.WriteAllText(CC.orin_path, string.Join("\r\n", output.ToArray()));

                MessageBox.Show("儲存成功");
            }
            catch (Exception ex)
            {
                MessageBox.Show("儲存失敗...檔案目前被其他程式開啟了...");
            }
        }


        private void a_SelectedIndexChanged(object sender, EventArgs e)
        {
            //當選刀改變了
            ComboBox comboBox = (ComboBox)sender;

            string id = my.explode("_", comboBox.Name)[1];
            string knife_kind = comboBox.SelectedIndex.ToString();
            //this.
            //MessageBox.Show(id + "," + comboBox.SelectedIndex.ToString());
            foreach (Control ta in GBox.Controls)
            {
                if (ta is ComboBox && ta.Name == "選正負_" + id)
                {
                    switch (knife_kind)
                    {
                        case "0":
                            ((ComboBox)ta).SelectedIndex = 0;
                            break;
                        case "1":
                        case "2":
                        case "3":
                        case "4":
                        case "6":
                            ((ComboBox)ta).SelectedIndex = 1; // +
                            break;
                        case "5":
                            ((ComboBox)ta).SelectedIndex = 2; // -
                            break;
                    }
                    break;
                }
            }

        }
    }
}
