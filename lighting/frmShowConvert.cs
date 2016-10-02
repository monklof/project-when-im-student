using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace HelloWorld
{
    public partial class frmShowConvert : Form
    {

        #region 变量
        public static bool tuiChanged = false;//判断数据是否已经修改
        public static bool ctrlChanged = false;
        #endregion
        public frmShowConvert()
        {
            InitializeComponent();
            LoadSet();
        }

        #region 其他
        //判断是否需要保存
        protected override void OnClosed(EventArgs e)
        {
            if ((tuiChanged || ctrlChanged) && MessageBox.Show("保存配置吗？", "保存配置", MessageBoxButtons.YesNo) == DialogResult.Yes)
                Save();
            base.OnClosed(e);
        }

        //这仅仅用来判断后面的数字
        public int GetLastNum(String str)
        {
            try
            {
                if ((char)str[str.Length - 2] < '9' && (char)str[str.Length - 2] > '0')
                    return Convert.ToInt32(str.Substring(str.Length - 2, 2));
                else
                    return Convert.ToInt32(str.Substring(str.Length - 1, 1));
            }
            catch (Exception err)
            {
                MessageBox.Show("error occured in GetLastNum!" + err.Message);
                return -1;
            }
        }
        #endregion

        #region 浏览图片按钮
        private void btn_seconClassValuePicBrs2_0_Click_1(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();

            frmHelloWorld.AddPic(0, "");
            if (frmHelloWorld.secondClassSet2Pic[0] != null && File.Exists(frmHelloWorld.secondClassSet2Pic[0]))
            {
                dlg.InitialDirectory = frmHelloWorld.secondClassSet2Pic[0];
                //dlg.FileName = frmHelloWorld.secondClassSet2Pic[0];
            }
            dlg.Filter = "支持的图片文件|*.jpg;*.jpeg;*.png;*.gif";
            if (dlg.ShowDialog(this) == DialogResult.OK)
            {
                FileInfo imageFile = new FileInfo(dlg.FileName);
                if (imageFile.Exists)
                {
                    frmHelloWorld.AddPic(0 + (int)numericUpDown_TuiIndex.Value * 15, imageFile.FullName);
                    checkBox_seconClassValue_Pic2_0.Checked = true;
                    //imageFile.FullName获取文件路径
                    MessageBox.Show(this, "图片已附加", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            tuiChanged = true;
        }

        private void btn_seconClassValuePicBrs2_1_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            frmHelloWorld.AddPic(1 + (int)numericUpDown_TuiIndex.Value * 15, null);
            if (frmHelloWorld.secondClassSet2Pic[1] != null && File.Exists(frmHelloWorld.secondClassSet2Pic[1]))
            {
                dlg.InitialDirectory = frmHelloWorld.secondClassSet2Pic[1];
                dlg.FileName = frmHelloWorld.secondClassSet2Pic[1];
            }
            dlg.Filter = "支持的图片文件|*.jpg;*.jpeg;*.png;*.gif";
            if (dlg.ShowDialog(this) == DialogResult.OK)
            {
                FileInfo imageFile = new FileInfo(dlg.FileName);
                if (imageFile.Exists)
                {
                    frmHelloWorld.AddPic(1 + (int)numericUpDown_TuiIndex.Value * 15, imageFile.FullName);
                    checkBox_seconClassValue_Pic2_1.Checked = true;
                    //imageFile.FullName获取文件路径
                    MessageBox.Show(this, "图片已附加", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            tuiChanged = true;
        }

        private void btn_seconClassValuePicBrs2_2_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            frmHelloWorld.AddPic(2 + (int)numericUpDown_TuiIndex.Value * 15, null);
            if (frmHelloWorld.secondClassSet2Pic[2] != null && File.Exists(frmHelloWorld.secondClassSet2Pic[2]))
            {
                dlg.InitialDirectory = frmHelloWorld.secondClassSet2Pic[2];
                dlg.FileName = frmHelloWorld.secondClassSet2Pic[2];
            }
            dlg.Filter = "支持的图片文件|*.jpg;*.jpeg;*.png;*.gif";
            if (dlg.ShowDialog(this) == DialogResult.OK)
            {
                FileInfo imageFile = new FileInfo(dlg.FileName);
                if (imageFile.Exists)
                {
                    frmHelloWorld.AddPic(2 + (int)numericUpDown_TuiIndex.Value * 15, imageFile.FullName);
                    checkBox_seconClassValue_Pic2_2.Checked = true;
                    //imageFile.FullName获取文件路径
                    MessageBox.Show(this, "图片已附加", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            tuiChanged = true;
        }

        private void btn_seconClassValuePicBrs2_3_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            frmHelloWorld.AddPic(3 + (int)numericUpDown_TuiIndex.Value * 15, null);

            if (frmHelloWorld.secondClassSet2Pic[3] != null && File.Exists(frmHelloWorld.secondClassSet2Pic[3]))
            {
                dlg.InitialDirectory = frmHelloWorld.secondClassSet2Pic[3];
                dlg.FileName = frmHelloWorld.secondClassSet2Pic[3];
            }
            dlg.Filter = "支持的图片文件|*.jpg;*.jpeg;*.png;*.gif";
            if (dlg.ShowDialog(this) == DialogResult.OK)
            {
                FileInfo imageFile = new FileInfo(dlg.FileName);
                if (imageFile.Exists)
                {
                    frmHelloWorld.AddPic(3 + (int)numericUpDown_TuiIndex.Value * 15, imageFile.FullName);//将路径存到缓冲区里面去
                    checkBox_seconClassValue_Pic2_3.Checked = true;
                    //imageFile.FullName获取文件路径
                    MessageBox.Show(this, "图片已附加", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            tuiChanged = true;
        }

        private void btn_seconClassValuePicBrs2_4_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            frmHelloWorld.AddPic(4 + (int)numericUpDown_TuiIndex.Value * 15, null);
            if (frmHelloWorld.secondClassSet2Pic[4] != null && File.Exists(frmHelloWorld.secondClassSet2Pic[4]))
            {
                dlg.InitialDirectory = frmHelloWorld.secondClassSet2Pic[4];
                dlg.FileName = frmHelloWorld.secondClassSet2Pic[4];
            }
            dlg.Filter = "支持的图片文件|*.jpg;*.jpeg;*.png;*.gif";
            if (dlg.ShowDialog(this) == DialogResult.OK)
            {
                FileInfo imageFile = new FileInfo(dlg.FileName);
                if (imageFile.Exists)
                {
                    frmHelloWorld.AddPic(4 + (int)numericUpDown_TuiIndex.Value * 15, imageFile.FullName);//将路径存到缓冲区里面去
                    checkBox_seconClassValue_Pic2_4.Checked = true;
                    //imageFile.FullName获取文件路径
                    MessageBox.Show(this, "图片已附加", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            tuiChanged = true;
        }

        private void btn_seconClassValuePicBrs2_5_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            frmHelloWorld.AddPic(5 + (int)numericUpDown_TuiIndex.Value * 15, null);
            if (frmHelloWorld.secondClassSet2Pic[5] != null && File.Exists(frmHelloWorld.secondClassSet2Pic[0]))
            {
                dlg.InitialDirectory = frmHelloWorld.secondClassSet2Pic[0];
                dlg.FileName = frmHelloWorld.secondClassSet2Pic[0];
            }
            dlg.Filter = "支持的图片文件|*.jpg;*.jpeg;*.png;*.gif";
            if (dlg.ShowDialog(this) == DialogResult.OK)
            {
                FileInfo imageFile = new FileInfo(dlg.FileName);
                if (imageFile.Exists)
                {
                    frmHelloWorld.AddPic(5 + (int)numericUpDown_TuiIndex.Value * 15, imageFile.FullName);//将路径存到缓冲区里面去
                    checkBox_seconClassValue_Pic2_5.Checked = true;
                    //imageFile.FullName获取文件路径
                    MessageBox.Show(this, "图片已附加", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            tuiChanged = true;
        }

        private void btn_seconClassValuePicBrs2_6_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            frmHelloWorld.AddPic(6 + (int)numericUpDown_TuiIndex.Value * 15, null);
            if (frmHelloWorld.secondClassSet2Pic[6] != null && File.Exists(frmHelloWorld.secondClassSet2Pic[6]))
            {
                dlg.InitialDirectory = frmHelloWorld.secondClassSet2Pic[6];
                dlg.FileName = frmHelloWorld.secondClassSet2Pic[6];
            }
            dlg.Filter = "支持的图片文件|*.jpg;*.jpeg;*.png;*.gif";
            if (dlg.ShowDialog(this) == DialogResult.OK)
            {
                FileInfo imageFile = new FileInfo(dlg.FileName);
                if (imageFile.Exists)
                {
                    frmHelloWorld.AddPic(6 + (int)numericUpDown_TuiIndex.Value * 15, imageFile.FullName);//将路径存到缓冲区里面去
                    checkBox_seconClassValue_Pic2_6.Checked = true;
                    //imageFile.FullName获取文件路径
                    MessageBox.Show(this, "图片已附加", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            tuiChanged = true;
        }

        private void btn_seconClassValuePicBrs2_7_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            frmHelloWorld.AddPic(7 + (int)numericUpDown_TuiIndex.Value * 15, null);
            if (frmHelloWorld.secondClassSet2Pic[7] != null && File.Exists(frmHelloWorld.secondClassSet2Pic[7]))
            {
                dlg.InitialDirectory = frmHelloWorld.secondClassSet2Pic[7];
                dlg.FileName = frmHelloWorld.secondClassSet2Pic[7];
            }
            dlg.Filter = "支持的图片文件|*.jpg;*.jpeg;*.png;*.gif";
            if (dlg.ShowDialog(this) == DialogResult.OK)
            {
                FileInfo imageFile = new FileInfo(dlg.FileName);
                if (imageFile.Exists)
                {
                    frmHelloWorld.AddPic(7 + (int)numericUpDown_TuiIndex.Value * 15, imageFile.FullName);//将路径存到缓冲区里面去
                    checkBox_seconClassValue_Pic2_7.Checked = true;
                    //imageFile.FullName获取文件路径
                    MessageBox.Show(this, "图片已附加", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            tuiChanged = true;
        }

        private void btn_seconClassValuePicBrs2_8_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            frmHelloWorld.AddPic(8 + (int)numericUpDown_TuiIndex.Value * 15, null);
            if (frmHelloWorld.secondClassSet2Pic[8] != null && File.Exists(frmHelloWorld.secondClassSet2Pic[8]))
            {
                dlg.InitialDirectory = frmHelloWorld.secondClassSet2Pic[8];
                dlg.FileName = frmHelloWorld.secondClassSet2Pic[8];
            }
            dlg.Filter = "支持的图片文件|*.jpg;*.jpeg;*.png;*.gif";
            if (dlg.ShowDialog(this) == DialogResult.OK)
            {
                FileInfo imageFile = new FileInfo(dlg.FileName);
                if (imageFile.Exists)
                {
                    frmHelloWorld.AddPic(8 + (int)numericUpDown_TuiIndex.Value * 15, imageFile.FullName);//将路径存到缓冲区里面去
                    checkBox_seconClassValue_Pic2_8.Checked = true;
                    //imageFile.FullName获取文件路径
                    MessageBox.Show(this, "图片已附加", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            tuiChanged = true;
        }

        private void btn_seconClassValuePicBrs2_9_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            frmHelloWorld.AddPic(9 + (int)numericUpDown_TuiIndex.Value * 15, null);
            if (frmHelloWorld.secondClassSet2Pic[9] != null && File.Exists(frmHelloWorld.secondClassSet2Pic[9]))
            {
                dlg.InitialDirectory = frmHelloWorld.secondClassSet2Pic[9];
                dlg.FileName = frmHelloWorld.secondClassSet2Pic[9];
            }
            dlg.Filter = "支持的图片文件|*.jpg;*.jpeg;*.png;*.gif";
            if (dlg.ShowDialog(this) == DialogResult.OK)
            {
                FileInfo imageFile = new FileInfo(dlg.FileName);
                if (imageFile.Exists)
                {
                    frmHelloWorld.AddPic(9 + (int)numericUpDown_TuiIndex.Value * 15, imageFile.FullName);//将路径存到缓冲区里面去
                    checkBox_seconClassValue_Pic2_9.Checked = true;
                    //imageFile.FullName获取文件路径
                    MessageBox.Show(this, "图片已附加", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            tuiChanged = true;
        }

        private void btn_seconClassValuePicBrs2_10_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            frmHelloWorld.AddPic(10 + (int)numericUpDown_TuiIndex.Value * 15, null);
            if (frmHelloWorld.secondClassSet2Pic[10] != null && File.Exists(frmHelloWorld.secondClassSet2Pic[10]))
            {
                dlg.InitialDirectory = frmHelloWorld.secondClassSet2Pic[10];
                dlg.FileName = frmHelloWorld.secondClassSet2Pic[10];
            }
            dlg.Filter = "支持的图片文件|*.jpg;*.jpeg;*.png;*.gif";
            if (dlg.ShowDialog(this) == DialogResult.OK)
            {
                FileInfo imageFile = new FileInfo(dlg.FileName);
                if (imageFile.Exists)
                {
                    frmHelloWorld.AddPic(10 + (int)numericUpDown_TuiIndex.Value * 15, imageFile.FullName);//将路径存到缓冲区里面去
                    checkBox_seconClassValue_Pic2_10.Checked = true;
                    //imageFile.FullName获取文件路径
                    MessageBox.Show(this, "图片已附加", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            tuiChanged = true;
        }

        private void btn_seconClassValuePicBrs2_11_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            frmHelloWorld.AddPic(11 + (int)numericUpDown_TuiIndex.Value * 15, null);
            if (frmHelloWorld.secondClassSet2Pic[11] != null && File.Exists(frmHelloWorld.secondClassSet2Pic[11]))
            {
                dlg.InitialDirectory = frmHelloWorld.secondClassSet2Pic[11];
                dlg.FileName = frmHelloWorld.secondClassSet2Pic[11];
            }
            dlg.Filter = "支持的图片文件|*.jpg;*.jpeg;*.png;*.gif";
            if (dlg.ShowDialog(this) == DialogResult.OK)
            {
                FileInfo imageFile = new FileInfo(dlg.FileName);
                if (imageFile.Exists)
                {
                    frmHelloWorld.AddPic(11 + (int)numericUpDown_TuiIndex.Value * 15, imageFile.FullName);//将路径存到缓冲区里面去
                    checkBox_seconClassValue_Pic2_11.Checked = true;
                    //imageFile.FullName获取文件路径
                    MessageBox.Show(this, "图片已附加", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            tuiChanged = true;
        }

        private void btn_seconClassValuePicBrs2_12_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            frmHelloWorld.AddPic(12 + (int)numericUpDown_TuiIndex.Value * 15, null);
            if (frmHelloWorld.secondClassSet2Pic[12] != null && File.Exists(frmHelloWorld.secondClassSet2Pic[12]))
            {
                dlg.InitialDirectory = frmHelloWorld.secondClassSet2Pic[12];
                dlg.FileName = frmHelloWorld.secondClassSet2Pic[12];
            }
            dlg.Filter = "支持的图片文件|*.jpg;*.jpeg;*.png;*.gif";
            if (dlg.ShowDialog(this) == DialogResult.OK)
            {
                FileInfo imageFile = new FileInfo(dlg.FileName);
                if (imageFile.Exists)
                {
                    frmHelloWorld.AddPic(12 + (int)numericUpDown_TuiIndex.Value * 15, imageFile.FullName);//将路径存到缓冲区里面去
                    checkBox_seconClassValue_Pic2_12.Checked = true;
                    //imageFile.FullName获取文件路径
                    MessageBox.Show(this, "图片已附加", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            tuiChanged = true;
        }

        private void btn_seconClassValuePicBrs2_13_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            frmHelloWorld.AddPic(13 + (int)numericUpDown_TuiIndex.Value * 15, null);
            if (frmHelloWorld.secondClassSet2Pic[13] != null && File.Exists(frmHelloWorld.secondClassSet2Pic[13]))
            {
                dlg.InitialDirectory = frmHelloWorld.secondClassSet2Pic[13];
                dlg.FileName = frmHelloWorld.secondClassSet2Pic[13];
            }
            dlg.Filter = "支持的图片文件|*.jpg;*.jpeg;*.png;*.gif";
            if (dlg.ShowDialog(this) == DialogResult.OK)
            {
                FileInfo imageFile = new FileInfo(dlg.FileName);
                if (imageFile.Exists)
                {
                    frmHelloWorld.AddPic(13 + (int)numericUpDown_TuiIndex.Value * 15, imageFile.FullName);//将路径存到缓冲区里面去
                    checkBox_seconClassValue_Pic2_13.Checked = true;
                    //imageFile.FullName获取文件路径
                    MessageBox.Show(this, "图片已附加", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            tuiChanged = true;
        }

        private void btn_seconClassValuePicBrs2_14_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            frmHelloWorld.AddPic(14 + (int)numericUpDown_TuiIndex.Value * 15, null);
            if (frmHelloWorld.secondClassSet2Pic[14] != null && File.Exists(frmHelloWorld.secondClassSet2Pic[14]))
            {
                dlg.InitialDirectory = frmHelloWorld.secondClassSet2Pic[14];
                dlg.FileName = frmHelloWorld.secondClassSet2Pic[14];
            }
            dlg.Filter = "支持的图片文件|*.jpg;*.jpeg;*.png;*.gif";
            if (dlg.ShowDialog(this) == DialogResult.OK)
            {
                FileInfo imageFile = new FileInfo(dlg.FileName);
                if (imageFile.Exists)
                {
                    frmHelloWorld.AddPic(14 + (int)numericUpDown_TuiIndex.Value * 15, imageFile.FullName);//将路径存到缓冲区里面去
                    checkBox_seconClassValue_Pic2_14.Checked = true;
                    //imageFile.FullName获取文件路径
                    MessageBox.Show(this, "图片已附加", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            tuiChanged = true;
        }

        #endregion

        #region 加载、保存串口设置
        //能自动保存缓冲区里面的东西
        private void btn_SaveSet_Click(object sender, EventArgs e)
        {
            Save();
            LoadSerialSet();//将设置读到缓冲区里
            LoadSet_Tui((int)numericUpDown_TuiIndex.Value);
            LoadSet_Ctrl((int)numericUpDown_CtrlIndex.Value);
        }
        ///加载串口配置
        public void LoadSerialSet()
        {

            //防止读错误，先实例化
            frmHelloWorld.secondClassSet[0] = new String[6] { "SerialName", "Bault", "CheckBit", "StatusBit", "StopBit", "DisplayType" };
            frmHelloWorld.secondClassSet[1] = new String[6] { "RoundEnable", "RoundInterval", "TuiInterval", "CtrlInterval", "DefaultBeginCh", "DefaultEndCh" };
            frmHelloWorld.secondClassSet[2] = new String[1];
            frmHelloWorld.secondClassSet[3] = new String[1];
            frmHelloWorld.secondClassSet2Pic = new String[1];

            frmHelloWorld.secondClassValue[0] = new String[6] { "COM1", "9600", "None", "8", "One", "Char" };
            frmHelloWorld.secondClassValue[1] = new String[6] { "False", "500", "2", "2", "@", "#" };

            frmHelloWorld.secondClassValue[2] = new String[1];
            frmHelloWorld.secondClassValue[3] = new String[1];
            //如果有配置 则读取配置文件

            if (File.Exists(Application.StartupPath + @"\SysSet.txt"))
            {
                try
                {
                    using (StreamReader sr = new StreamReader(Application.StartupPath + @"\SysSet.txt", Encoding.UTF8))//从文件流中读取配置文件
                    {
                        int Index = 0;//记录SecondClassSet[2]和secondClassSet[3]的下标
                        int k = -1;//记录大类的下标
                        bool mark = false;
                        #region 读取配置文件
                        while (!sr.EndOfStream)
                        {
                            String temp = sr.ReadLine();//用来读取一行数据
                            mark = false;//用来标志该行是不是一个大类的标志行
                            //判断该行是不是一个大类
                            for (int i = 0; i < frmHelloWorld.topClassSet.GetLength(0); i++)//注意不要写成topClassSet.Length，这样表示的是维数
                            {
                                String temp2;
                                if (temp.Length >= 3)
                                {
                                    temp2 = temp.Trim().Substring(1, temp.Length - 2);

                                    if (temp2 == frmHelloWorld.topClassSet[i])
                                    {
                                        k = i;
                                        frmHelloWorld.secondClassSet[i] = new String[1];//实例化string
                                        frmHelloWorld.secondClassValue[i] = new String[1];//
                                        mark = true;//这是一个大类，跳过
                                        break;
                                    }
                                }
                            }
                            if (mark)
                            {
                                Index = 0;
                                continue;
                            }
                            if (k == -1)
                            {
                                MessageBox.Show("配置有错！");
                                //默认配置
                                frmHelloWorld.secondClassSet[0] = new String[6] { "SerialName", "Bault", "CheckBit", "StatusBit", "StopBit", "DisplayType" };
                                frmHelloWorld.secondClassSet[1] = new String[6] { "RoundEnable", "RoundInterval", "TuiInterval", "CtrlInterval", "DefaultBeginCh", "DefaultEndCh" };
                                frmHelloWorld.secondClassSet[2] = new String[1];
                                frmHelloWorld.secondClassSet[3] = new String[1];
                                frmHelloWorld.secondClassValue[2] = new String[1];
                                frmHelloWorld.secondClassValue[3] = new String[1];
                                frmHelloWorld.secondClassSet2Pic = new String[1];

                                frmHelloWorld.secondClassValue[0] = new String[6] { "COM1", "9600", "None", "8", "One", "Char" };
                                frmHelloWorld.secondClassValue[1] = new String[6] { "False", "500", "2", "2", "@", "#" };
                                return;
                            }
                            else
                            {
                                if (temp == null)
                                    continue;
                                if (temp.Split('=').GetLength(0) == 3 && k == 2)
                                {
                                    frmHelloWorld.AddPic(Index, temp.Split('=')[2].Trim());
                                }
                                frmHelloWorld.AddString(k, Index, temp.Split('=')[0].Trim(), temp.Split('=')[1].Trim());
                                Index++;
                            }
                        }

                        #endregion
                    }
                }
                catch (Exception err)
                {
                    MessageBox.Show("配置有错！" + err.Message);
                    //默认配置
                    frmHelloWorld.secondClassSet[0] = new String[6] { "SerialName", "Bault", "CheckBit", "StatusBit", "StopBit", "DisplayType" };
                    frmHelloWorld.secondClassSet[1] = new String[6] { "RoundEnable", "RoundInterval", "TuiInterval", "CtrlInterval", "DefaultBeginCh", "DefaultEndCh" };
                    frmHelloWorld.secondClassSet[2] = new String[1];
                    frmHelloWorld.secondClassSet[3] = new String[1];
                    frmHelloWorld.secondClassSet2Pic = new String[1];

                    frmHelloWorld.secondClassValue[2] = new String[1];
                    frmHelloWorld.secondClassValue[3] = new String[1];
                    frmHelloWorld.secondClassValue[0] = new String[6] { "COM1", "9600", "None", "8", "One", "Char" };
                    frmHelloWorld.secondClassValue[1] = new String[6] { "False", "500", "2", "2", "@", "#" };
                }
            }
        }
        public void Save()
        {
            foreach (Control B in Controls)
            {
                if (B.Name == "groupBox_tuiConvert")//最上层，tuibox  textBox_seconClassValue2_4
                {
                    int indexLong = (int)(15 * (numericUpDown_TuiIndex.Value + 1));

                    String[] setTemp = new String[indexLong];//暂时用来存set数据
                    String[] valueTemp = new String[indexLong];//暂时用来存value数据

                    #region 具体到是set还是value,保存tui
                    foreach (Control gB in B.Controls)//
                    {
                        if (gB.Name == "groupBox_secondClassSet_Tui")
                        {
                            foreach (Control tB in gB.Controls)//找出来
                            {
                                if (tB.GetType().ToString() == "System.Windows.Forms.TextBox" && tB.Name.Length > "textBox_seconClass".Length)
                                {
                                    if (tB.Name.Substring(0, "textBox_seconClassS".Length) == "textBox_seconClassS"  && !string.IsNullOrEmpty(tB.Text.Trim()) && !string.IsNullOrWhiteSpace(tB.Text.Trim()))//set
                                    {
                                        setTemp[GetLastNum(tB.Name) + indexLong - 15] = tB.Text.Trim();
                                    }
                                    if (tB.Name.Substring(0, "textBox_seconClassV".Length) == "textBox_seconClassV" && !string.IsNullOrEmpty(tB.Text.Trim()) && !string.IsNullOrWhiteSpace(tB.Text.Trim()))//value,可以存空白,应对只发图片微博的情况
                                    {
                                        valueTemp[GetLastNum(tB.Name) + indexLong - 15] = tB.Text.Trim();
                                    }
                                    if (tB.Name.Substring(0, "textBox_seconClassV".Length) == "textBox_seconClassV" && string.IsNullOrEmpty(tB.Text.Trim()) && string.IsNullOrWhiteSpace(tB.Text.Trim()) )
                                    {
                                        if ( (GetLastNum(tB.Name) + indexLong - 15) < frmHelloWorld.secondClassSet2Pic.Length && !String.IsNullOrWhiteSpace( frmHelloWorld.secondClassSet2Pic[GetLastNum(tB.Name) + indexLong - 15]))
                                            valueTemp[GetLastNum(tB.Name) + indexLong - 15] = null;
                                    }
                                }
                            }
                            //放到缓冲区里面去
                            for (int i = indexLong - 15; i < indexLong; i++)
                            {

                                if ((!String.IsNullOrEmpty(setTemp[i])) && String.IsNullOrEmpty(valueTemp[i]))//要出去没有值value或者pic的
                                {
                                    if (i < frmHelloWorld.secondClassSet2Pic.Length)//图片数组存在
                                    {
                                        if (String.IsNullOrEmpty(frmHelloWorld.secondClassSet2Pic[i]) || String.IsNullOrWhiteSpace(frmHelloWorld.secondClassSet2Pic[i]))
                                        {
                                            continue;//跳
                                        }
                                    }
                                    else//图片数组都不存在
                                    {

                                        continue;
                                    }
                                }


                                frmHelloWorld.AddString(2, i, setTemp[i], valueTemp[i]);

                            }
                        }
                    }
                    #endregion
                }
                else if (B.Name == "groupBox_ConvertCtrl")//最上层，ctrlbox  textBox_seconClassValue2_4
                {
                    int indexLong = (int)(15 * (numericUpDown_CtrlIndex.Value + 1));
                    String[] setTemp = new String[indexLong];//暂时用来存set数据
                    String[] valueTemp = new String[indexLong];//暂时用来存value数据

                    #region 具体到是set还是value,保存ctrl
                    foreach (Control gB in B.Controls)//
                    {
                        if (gB.Name == "groupBox_secondClassSet_Ctrl")
                        {
                            foreach (Control tB in gB.Controls)
                            {

                                if (tB.GetType().ToString() == "System.Windows.Forms.TextBox" && tB.Name.Length > "textBox_seconClass".Length && !string.IsNullOrEmpty(tB.Text.Trim()) && !string.IsNullOrWhiteSpace(tB.Text.Trim()))
                                {
                                    if (tB.Name.Substring(0, "textBox_seconClassS".Length) == "textBox_seconClassS")//set
                                    {
                                        setTemp[GetLastNum(tB.Name) + indexLong - 15] = tB.Text.Trim();
                                    }
                                    else if (tB.Name.Substring(0, "textBox_seconClassV".Length) == "textBox_seconClassV")//value
                                    {
                                        valueTemp[GetLastNum(tB.Name) + indexLong - 15] = tB.Text.Trim();
                                    }
                                }
                            }
                            for (int i = indexLong - 15; i < indexLong; i++)
                            {
                                frmHelloWorld.AddString(3, i, setTemp[i], valueTemp[i]);
                            }
                        }
                    }
                    #endregion
                }

            }
            frmHelloWorld.SaveBufferSet();

        }
        //方法，设置textbox或者获取textbox里面的值，setString为null，表示获取，否则表示设置
        public void LoadSet()
        {
            LoadSet_Tui(0);
            LoadSet_Ctrl(0);
        }

        /// <summary>
        /// 加载tui设置
        /// </summary>
        /// <param name="tuiIndex">第几页，从0开始</param>
        public void LoadSet_Tui(int tuiIndex)
        {
            for (int i = tuiIndex * 15; i < (tuiIndex + 1) * 15 && i < frmHelloWorld.secondClassSet[2].Length; i++)
            {
                if (frmHelloWorld.secondClassSet[2][i] != null)
                {
                    SecondClassSet2_TextBox(i % 15, frmHelloWorld.secondClassSet[2][i]);
                    SecondClassValue2_TextBox(i % 15, frmHelloWorld.secondClassValue[2][i]);
                    if (i < frmHelloWorld.secondClassSet2Pic.Length && frmHelloWorld.secondClassSet2Pic[i] != null && frmHelloWorld.secondClassSet2Pic[i] != "")
                        checkBox_seconClassValue_Pic2(i % 15, true, true);
                    else
                        checkBox_seconClassValue_Pic2(i % 15, true, false);
                }
            }
            //用来将页面的没有值的部分填为零
            if (tuiIndex > 0 && frmHelloWorld.secondClassSet[2].Length / 15 >= tuiIndex)
            {
                for (int i = frmHelloWorld.secondClassSet[2].Length % 15; i < 15; i++)
                {
                    SecondClassSet2_TextBox(i % 15, "");
                    SecondClassValue2_TextBox(i % 15, "");
                    checkBox_seconClassValue_Pic2(i % 15, true, false);
                }
            }
            else if (tuiIndex > 0 && frmHelloWorld.secondClassSet[2].Length / 15 < tuiIndex)
            {
                for (int i = 0; i < 15; i++)
                {
                    SecondClassSet2_TextBox(i % 15, "");
                    SecondClassValue2_TextBox(i % 15, "");
                    checkBox_seconClassValue_Pic2(i % 15, true, false);
                }
            }
            else
            {
                for (int i = 0; i < frmHelloWorld.secondClassSet[2].Length % 15; i++)
                {
                    if (frmHelloWorld.secondClassSet[2][i] != null)
                    {
                        SecondClassSet2_TextBox(i % 15, frmHelloWorld.secondClassSet[2][i]);
                        SecondClassValue2_TextBox(i % 15, frmHelloWorld.secondClassValue[2][i]);
                        if (i < frmHelloWorld.secondClassSet2Pic.Length && frmHelloWorld.secondClassSet2Pic[i] != null && frmHelloWorld.secondClassSet2Pic[i] != "")
                            checkBox_seconClassValue_Pic2(i % 15, true, true);
                        else
                            checkBox_seconClassValue_Pic2(i % 15, true, false);
                    }
                }
                for (int i = frmHelloWorld.secondClassSet[2].Length; i < 15; i++)
                {
                    SecondClassSet2_TextBox(i % 15, "");
                    SecondClassValue2_TextBox(i % 15, "");
                    checkBox_seconClassValue_Pic2(i % 15, true, false);
                }
            }
            SetLabel_Tui(tuiIndex * 15);
            numericUpDown_TuiIndex.Value = tuiIndex;
            tuiChanged = false;
        }

        /// <summary>
        /// 加载ctrl
        /// </summary>
        /// <param name="ctrlIndex">第几页，从0开始</param>
        public void LoadSet_Ctrl(int ctrlIndex)
        {
            //在有内容的地方填上相关信息
            for (int i = ctrlIndex * 15; i < (ctrlIndex + 1) * 15 && i < frmHelloWorld.secondClassSet[3].Length; i++)
            {
                if (frmHelloWorld.secondClassSet[3][i] != null)
                {
                    SecondClassSet3_TextBox(i % 15, frmHelloWorld.secondClassSet[3][i]);
                    SecondClassValue3_TextBox(i % 15, frmHelloWorld.secondClassValue[3][i]);
                }
            }
            //用来将页面的没有值的部分填为零
            if (ctrlIndex > 0 && frmHelloWorld.secondClassSet[3].Length / 15 >= ctrlIndex)
            {
                for (int i = frmHelloWorld.secondClassSet[3].Length % 15; i < 15; i++)
                {
                    SecondClassSet3_TextBox(i % 15, "");
                    SecondClassValue3_TextBox(i % 15, "");
                }
            }
            else if (ctrlIndex > 0 && frmHelloWorld.secondClassSet[3].Length / 15 < ctrlIndex)
            {
                for (int i = 0; i < 15; i++)
                {
                    SecondClassSet3_TextBox(i % 15, "");
                    SecondClassValue3_TextBox(i % 15, "");
                }
            }
            else
            {
                for (int i = 0; i < frmHelloWorld.secondClassSet[3].Length % 15; i++)
                {
                    if (frmHelloWorld.secondClassSet[3][i] != null)
                    {
                        SecondClassSet3_TextBox(i % 15, frmHelloWorld.secondClassSet[3][i]);
                        SecondClassValue3_TextBox(i % 15, frmHelloWorld.secondClassValue[3][i]);
                    }
                }
                for (int i = frmHelloWorld.secondClassSet[3].Length; i < 15; i++)
                {
                    SecondClassSet3_TextBox(i, "");
                    SecondClassValue3_TextBox(i, "");
                }
            }
            SetLabel_Ctrl(ctrlIndex * 15);
            numericUpDown_CtrlIndex.Value = ctrlIndex;
            ctrlChanged = false;
        }

        #endregion
        
        #region 刷新界面
        private void numericUpDown_TuiIndex_ValueChanged(object sender, EventArgs e)
        {
            LoadSet_Tui((int)numericUpDown_TuiIndex.Value);
        }
        private void numericUpDown_CtrlIndex_ValueChanged(object sender, EventArgs e)
        {
            LoadSet_Ctrl((int)numericUpDown_CtrlIndex.Value);
        }


        private void btn_TuiPre_Click(object sender, EventArgs e)
        {
            if (tuiChanged && MessageBox.Show("保存配置吗？", "保存配置", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                Save();
            }
            if (numericUpDown_TuiIndex.Value >= 1)
            {
                numericUpDown_TuiIndex.Value -= 1;
            }
            else
                numericUpDown_TuiIndex.Value = 0;
            LoadSet_Tui((int)numericUpDown_TuiIndex.Value);//从新加载
        }

        private void btn_TuiNext_Click(object sender, EventArgs e)
        {
            if (tuiChanged && MessageBox.Show("保存配置吗？", "保存配置", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                Save();
            }
            if (numericUpDown_TuiIndex.Value >= 0)
            {
                numericUpDown_TuiIndex.Value += 1;
            }
            else
                numericUpDown_TuiIndex.Value = 0;
            LoadSet_Tui((int)numericUpDown_TuiIndex.Value);

        }

        private void btn_CtrlPre_Click(object sender, EventArgs e)
        {
            if (ctrlChanged && MessageBox.Show("保存配置吗？", "保存配置", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                Save();
            }
            if (numericUpDown_CtrlIndex.Value >= 1)
            {
                numericUpDown_CtrlIndex.Value -= 1;
            }
            else
                numericUpDown_CtrlIndex.Value = 0;
            LoadSet_Ctrl((int)numericUpDown_CtrlIndex.Value);//从新加载
        }

        private void btn_CtrlNext_Click(object sender, EventArgs e)
        {
            if (ctrlChanged && MessageBox.Show("保存配置吗？", "保存配置", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                Save();
            }
            if (numericUpDown_CtrlIndex.Value >= 0)
            {
                numericUpDown_CtrlIndex.Value += 1;
            }
            else
                numericUpDown_CtrlIndex.Value = 0;
            LoadSet_Ctrl((int)numericUpDown_CtrlIndex.Value);

        }

        /// <summary>
        /// 变化界面里的数组标
        /// </summary>
        /// <param name="startIndex">数组开始下标</param>
        public void SetLabel_Tui(int startIndex)
        {

            if (startIndex >= 0)
            {
                int i = startIndex;
                tB_TuiIndex_0.Text = i.ToString();
                i++;
                tB_TuiIndex_1.Text = i.ToString();
                i++;
                tB_TuiIndex_2.Text = i.ToString();
                i++;
                tB_TuiIndex_3.Text = i.ToString();
                i++;
                tB_TuiIndex_4.Text = i.ToString();
                i++;
                tB_TuiIndex_5.Text = i.ToString();
                i++;
                tB_TuiIndex_6.Text = i.ToString();
                i++;
                tB_TuiIndex_7.Text = i.ToString();
                i++;
                tB_TuiIndex_8.Text = i.ToString();
                i++;
                tB_TuiIndex_9.Text = i.ToString();
                i++;
                tB_TuiIndex_10.Text = i.ToString();
                i++;
                tB_TuiIndex_11.Text = i.ToString();
                i++;
                tB_TuiIndex_12.Text = i.ToString();
                i++;
                tB_TuiIndex_13.Text = i.ToString();
                i++;
                tB_TuiIndex_14.Text = i.ToString();
                i++;
            }
        }

        /// <summary>
        /// 加载ctrl下标
        /// </summary>
        /// <param name="startIndex">表示开始加的下标</param>
        public void SetLabel_Ctrl(int startIndex)
        {
            if (startIndex >= 0)
            {
                int i = startIndex;
                tB_CtrlIndex_0.Text = i.ToString();
                i++;
                tB_CtrlIndex_1.Text = i.ToString();
                i++;
                tB_CtrlIndex_2.Text = i.ToString();
                i++;
                tB_CtrlIndex_3.Text = i.ToString();
                i++;
                tB_CtrlIndex_4.Text = i.ToString();
                i++;
                tB_CtrlIndex_5.Text = i.ToString();
                i++;
                tB_CtrlIndex_6.Text = i.ToString();
                i++;
                tB_CtrlIndex_7.Text = i.ToString();
                i++;
                tB_CtrlIndex_8.Text = i.ToString();
                i++;
                tB_CtrlIndex_9.Text = i.ToString();
                i++;
                tB_CtrlIndex_10.Text = i.ToString();
                i++;
                tB_CtrlIndex_11.Text = i.ToString();
                i++;
                tB_CtrlIndex_12.Text = i.ToString();
                i++;
                tB_CtrlIndex_13.Text = i.ToString();
                i++;
                tB_CtrlIndex_14.Text = i.ToString();
                i++;
            }
        }

        public String SecondClassSet2_TextBox(int i, String setString)
        {

            if (setString == null)
            {
                if (i == 0)
                    return textBox_seconClassSet2_0.Text.Trim();
                if (i == 1)
                    return textBox_seconClassSet2_1.Text.Trim();
                if (i == 2)
                    return textBox_seconClassSet2_2.Text.Trim();
                if (i == 3)
                    return textBox_seconClassSet2_3.Text.Trim();
                if (i == 4)
                    return textBox_seconClassSet2_4.Text.Trim();
                if (i == 5)
                    return textBox_seconClassSet2_5.Text.Trim();
                if (i == 6)
                    return textBox_seconClassSet2_6.Text.Trim();
                if (i == 7)
                    return textBox_seconClassSet2_7.Text.Trim();
                if (i == 8)
                    return textBox_seconClassSet2_8.Text.Trim();
                if (i == 9)
                    return textBox_seconClassSet2_9.Text.Trim();
                if (i == 10)
                    return textBox_seconClassSet2_10.Text.Trim();
                if (i == 11)
                    return textBox_seconClassSet2_11.Text.Trim();
                if (i == 12)
                    return textBox_seconClassSet2_12.Text.Trim();
                if (i == 13)
                    return textBox_seconClassSet2_13.Text.Trim();
                if (i == 14)
                    return textBox_seconClassSet2_14.Text.Trim();
                //到这里都没有表示不再范围，return null
                return null;
            }
            else
            {
                if (i == 0)
                    textBox_seconClassSet2_0.Text = setString;
                if (i == 1)
                    textBox_seconClassSet2_1.Text = setString;
                if (i == 2)
                    textBox_seconClassSet2_2.Text = setString;
                if (i == 3)
                    textBox_seconClassSet2_3.Text = setString;
                if (i == 4)
                    textBox_seconClassSet2_4.Text = setString;
                if (i == 5)
                    textBox_seconClassSet2_5.Text = setString;
                if (i == 6)
                    textBox_seconClassSet2_6.Text = setString;
                if (i == 7)
                    textBox_seconClassSet2_7.Text = setString;
                if (i == 8)
                    textBox_seconClassSet2_8.Text = setString;
                if (i == 9)
                    textBox_seconClassSet2_9.Text = setString;
                if (i == 10)
                    textBox_seconClassSet2_10.Text = setString;
                if (i == 11)
                    textBox_seconClassSet2_11.Text = setString;
                if (i == 12)
                    textBox_seconClassSet2_12.Text = setString;
                if (i == 13)
                    textBox_seconClassSet2_13.Text = setString;
                if (i == 14)
                    textBox_seconClassSet2_14.Text = setString;

                if (i < 15 && i >= 0)
                    return "succeed";//设置成功
                else
                    return "fail";
            }
        }
        public String SecondClassValue2_TextBox(int i, String valueString)
        {

            if (valueString == null)
            {
                if (i == 0)
                    return textBox_seconClassValue2_0.Text.Trim();
                if (i == 1)
                    return textBox_seconClassValue2_1.Text.Trim();
                if (i == 2)
                    return textBox_seconClassValue2_2.Text.Trim();
                if (i == 3)
                    return textBox_seconClassValue2_3.Text.Trim();
                if (i == 4)
                    return textBox_seconClassValue2_4.Text.Trim();
                if (i == 5)
                    return textBox_seconClassValue2_5.Text.Trim();
                if (i == 6)
                    return textBox_seconClassValue2_6.Text.Trim();
                if (i == 7)
                    return textBox_seconClassValue2_7.Text.Trim();
                if (i == 8)
                    return textBox_seconClassValue2_8.Text.Trim();
                if (i == 9)
                    return textBox_seconClassValue2_9.Text.Trim();
                if (i == 10)
                    return textBox_seconClassValue2_10.Text.Trim();
                if (i == 11)
                    return textBox_seconClassValue2_11.Text.Trim();
                if (i == 12)
                    return textBox_seconClassValue2_12.Text.Trim();
                if (i == 13)
                    return textBox_seconClassValue2_13.Text.Trim();
                if (i == 14)
                    return textBox_seconClassValue2_14.Text.Trim();
                //到这里都没有表示不再范围，return null
                return null;
            }
            else
            {
                if (i == 0)
                    textBox_seconClassValue2_0.Text = valueString;
                if (i == 1)
                    textBox_seconClassValue2_1.Text = valueString;
                if (i == 2)
                    textBox_seconClassValue2_2.Text = valueString;
                if (i == 3)
                    textBox_seconClassValue2_3.Text = valueString;
                if (i == 4)
                    textBox_seconClassValue2_4.Text = valueString;
                if (i == 5)
                    textBox_seconClassValue2_5.Text = valueString;
                if (i == 6)
                    textBox_seconClassValue2_6.Text = valueString;
                if (i == 7)
                    textBox_seconClassValue2_7.Text = valueString;
                if (i == 8)
                    textBox_seconClassValue2_8.Text = valueString;
                if (i == 9)
                    textBox_seconClassValue2_9.Text = valueString;
                if (i == 10)
                    textBox_seconClassValue2_10.Text = valueString;
                if (i == 11)
                    textBox_seconClassValue2_11.Text = valueString;
                if (i == 12)
                    textBox_seconClassValue2_12.Text = valueString;
                if (i == 13)
                    textBox_seconClassValue2_13.Text = valueString;
                if (i == 14)
                    textBox_seconClassValue2_14.Text = valueString;

                if (i < 15 && i >= 0)
                    return "succeed";//设置成功
                else
                    return "fail";
            }
        }
        /// <summary>
        /// 给checkBox_seconClassValue_Pic2_CheckBox设置相关属性
        /// </summary>
        /// <param name="i">表示checkbox的下标</param>
        /// <param name="setTt">表示是设置还是读取，true是设置，false是读取</param>
        /// <param name="value">表示设置的值</param>
        /// <returns></returns>
        public bool checkBox_seconClassValue_Pic2(int i, bool setTt, bool value)
        {

            if (setTt == false)
            {
                if (i == 0)
                    return checkBox_seconClassValue_Pic2_0.Checked;
                if (i == 1)
                    return checkBox_seconClassValue_Pic2_1.Checked;
                if (i == 2)
                    return checkBox_seconClassValue_Pic2_2.Checked;
                if (i == 3)
                    return checkBox_seconClassValue_Pic2_3.Checked;
                if (i == 4)
                    return checkBox_seconClassValue_Pic2_4.Checked;
                if (i == 5)
                    return checkBox_seconClassValue_Pic2_5.Checked;
                if (i == 6)
                    return checkBox_seconClassValue_Pic2_6.Checked;
                if (i == 7)
                    return checkBox_seconClassValue_Pic2_7.Checked;
                if (i == 8)
                    return checkBox_seconClassValue_Pic2_8.Checked;
                if (i == 9)
                    return checkBox_seconClassValue_Pic2_9.Checked;
                if (i == 10)
                    return checkBox_seconClassValue_Pic2_10.Checked;
                if (i == 11)
                    return checkBox_seconClassValue_Pic2_11.Checked;
                if (i == 12)
                    return checkBox_seconClassValue_Pic2_12.Checked;
                if (i == 13)
                    return checkBox_seconClassValue_Pic2_13.Checked;
                if (i == 14)
                    return checkBox_seconClassValue_Pic2_14.Checked;
                //到这里都没有表示不再范围，return null
                return false;
            }
            else
            {
                if (i == 0)
                    checkBox_seconClassValue_Pic2_0.Checked = value;
                if (i == 1)
                    checkBox_seconClassValue_Pic2_1.Checked = value;
                if (i == 2)
                    checkBox_seconClassValue_Pic2_2.Checked = value;
                if (i == 3)
                    checkBox_seconClassValue_Pic2_3.Checked = value;
                if (i == 4)
                    checkBox_seconClassValue_Pic2_4.Checked = value;
                if (i == 5)
                    checkBox_seconClassValue_Pic2_5.Checked = value;
                if (i == 6)
                    checkBox_seconClassValue_Pic2_6.Checked = value;
                if (i == 7)
                    checkBox_seconClassValue_Pic2_7.Checked = value;
                if (i == 8)
                    checkBox_seconClassValue_Pic2_8.Checked = value;
                if (i == 9)
                    checkBox_seconClassValue_Pic2_9.Checked = value;
                if (i == 10)
                    checkBox_seconClassValue_Pic2_10.Checked = value;
                if (i == 11)
                    checkBox_seconClassValue_Pic2_11.Checked = value;
                if (i == 12)
                    checkBox_seconClassValue_Pic2_12.Checked = value;
                if (i == 13)
                    checkBox_seconClassValue_Pic2_13.Checked = value;
                if (i == 14)
                    checkBox_seconClassValue_Pic2_14.Checked = value;

                if (i < 15 && i >= 0)
                    return true;//设置成功
                else
                    return false;
            }
        }
        public String SecondClassSet3_TextBox(int i, String setString)
        {

            if (setString == null)
            {
                if (i == 0)
                    return textBox_seconClassSet3_0.Text.Trim();
                if (i == 1)
                    return textBox_seconClassSet3_1.Text.Trim();
                if (i == 2)
                    return textBox_seconClassSet3_2.Text.Trim();
                if (i == 3)
                    return textBox_seconClassSet3_3.Text.Trim();
                if (i == 4)
                    return textBox_seconClassSet3_4.Text.Trim();
                if (i == 5)
                    return textBox_seconClassSet3_5.Text.Trim();
                if (i == 6)
                    return textBox_seconClassSet3_6.Text.Trim();
                if (i == 7)
                    return textBox_seconClassSet3_7.Text.Trim();
                if (i == 8)
                    return textBox_seconClassSet3_8.Text.Trim();
                if (i == 9)
                    return textBox_seconClassSet3_9.Text.Trim();
                if (i == 10)
                    return textBox_seconClassSet3_10.Text.Trim();
                if (i == 11)
                    return textBox_seconClassSet3_11.Text.Trim();
                if (i == 12)
                    return textBox_seconClassSet3_12.Text.Trim();
                if (i == 13)
                    return textBox_seconClassSet3_13.Text.Trim();
                if (i == 14)
                    return textBox_seconClassSet3_14.Text.Trim();
                //到这里都没有表示不再范围，return null
                return null;
            }
            else
            {
                if (i == 0)
                    textBox_seconClassSet3_0.Text = setString;
                if (i == 1)
                    textBox_seconClassSet3_1.Text = setString;
                if (i == 2)
                    textBox_seconClassSet3_2.Text = setString;
                if (i == 3)
                    textBox_seconClassSet3_3.Text = setString;
                if (i == 4)
                    textBox_seconClassSet3_4.Text = setString;
                if (i == 5)
                    textBox_seconClassSet3_5.Text = setString;
                if (i == 6)
                    textBox_seconClassSet3_6.Text = setString;
                if (i == 7)
                    textBox_seconClassSet3_7.Text = setString;
                if (i == 8)
                    textBox_seconClassSet3_8.Text = setString;
                if (i == 9)
                    textBox_seconClassSet3_9.Text = setString;
                if (i == 10)
                    textBox_seconClassSet3_10.Text = setString;
                if (i == 11)
                    textBox_seconClassSet3_11.Text = setString;
                if (i == 12)
                    textBox_seconClassSet3_12.Text = setString;
                if (i == 13)
                    textBox_seconClassSet3_13.Text = setString;
                if (i == 14)
                    textBox_seconClassSet3_14.Text = setString;

                if (i < 15 && i >= 0)
                    return "succeed";//设置成功
                else
                    return "fail";
            }
        }
        public String SecondClassValue3_TextBox(int i, String valueString)
        {

            if (valueString == null)
            {
                if (i == 0)
                    return textBox_seconClassValue3_0.Text.Trim();
                if (i == 1)
                    return textBox_seconClassValue3_1.Text.Trim();
                if (i == 2)
                    return textBox_seconClassValue3_2.Text.Trim();
                if (i == 3)
                    return textBox_seconClassValue3_3.Text.Trim();
                if (i == 4)
                    return textBox_seconClassValue3_4.Text.Trim();
                if (i == 5)
                    return textBox_seconClassValue3_5.Text.Trim();
                if (i == 6)
                    return textBox_seconClassValue3_6.Text.Trim();
                if (i == 7)
                    return textBox_seconClassValue3_7.Text.Trim();
                if (i == 8)
                    return textBox_seconClassValue3_8.Text.Trim();
                if (i == 9)
                    return textBox_seconClassValue3_9.Text.Trim();
                if (i == 10)
                    return textBox_seconClassValue3_10.Text.Trim();
                if (i == 11)
                    return textBox_seconClassValue3_11.Text.Trim();
                if (i == 12)
                    return textBox_seconClassValue3_12.Text.Trim();
                if (i == 13)
                    return textBox_seconClassValue3_13.Text.Trim();
                if (i == 14)
                    return textBox_seconClassValue3_14.Text.Trim();
                //到这里都没有表示不再范围，return null
                return null;
            }
            else
            {
                if (i == 0)
                    textBox_seconClassValue3_0.Text = valueString;
                if (i == 1)
                    textBox_seconClassValue3_1.Text = valueString;
                if (i == 2)
                    textBox_seconClassValue3_2.Text = valueString;
                if (i == 3)
                    textBox_seconClassValue3_3.Text = valueString;
                if (i == 4)
                    textBox_seconClassValue3_4.Text = valueString;
                if (i == 5)
                    textBox_seconClassValue3_5.Text = valueString;
                if (i == 6)
                    textBox_seconClassValue3_6.Text = valueString;
                if (i == 7)
                    textBox_seconClassValue3_7.Text = valueString;
                if (i == 8)
                    textBox_seconClassValue3_8.Text = valueString;
                if (i == 9)
                    textBox_seconClassValue3_9.Text = valueString;
                if (i == 10)
                    textBox_seconClassValue3_10.Text = valueString;
                if (i == 11)
                    textBox_seconClassValue3_11.Text = valueString;
                if (i == 12)
                    textBox_seconClassValue3_12.Text = valueString;
                if (i == 13)
                    textBox_seconClassValue3_13.Text = valueString;
                if (i == 14)
                    textBox_seconClassValue3_14.Text = valueString;

                if (i < 15 && i >= 0)
                    return "succeed";//设置成功
                else
                    return "fail";
            }
        }
        public String[] SecondClassSet2_TextBox_Str = new String[2] { "textBox_seconClassSet2_0", "textBox_seconClassSet2_1" };

        #endregion

        #region 删除tui按钮
        private void btn_seconClassValueDel2_0_Click(object sender, EventArgs e)
        {
            //删除所有东西
            textBox_seconClassSet2_0.Text = "";
            textBox_seconClassValue2_0.Text = "";
            checkBox_seconClassValue_Pic2_0.Checked = false;

            try
            {
                frmHelloWorld.secondClassSet2Pic[0] = null;
            }
            catch { }
        }

        private void btn_seconClassValueDel2_1_Click(object sender, EventArgs e)
        {
            //删除所有东西
            textBox_seconClassSet2_1.Text = "";
            textBox_seconClassValue2_1.Text = "";
            checkBox_seconClassValue_Pic2_1.Checked = false;

            try
            {
                frmHelloWorld.secondClassSet2Pic[1] = null;
            }
            catch { }
        }

        private void btn_seconClassValueDel2_2_Click(object sender, EventArgs e)
        {
            //删除所有东西
            textBox_seconClassSet2_2.Text = "";
            textBox_seconClassValue2_2.Text = "";
            checkBox_seconClassValue_Pic2_2.Checked = false;

            try
            {
                frmHelloWorld.secondClassSet2Pic[2] = null;
            }
            catch { }
        }

        private void btn_seconClassValueDel2_3_Click(object sender, EventArgs e)
        {
            //删除所有东西
            textBox_seconClassSet2_3.Text = "";
            textBox_seconClassValue2_3.Text = "";
            checkBox_seconClassValue_Pic2_3.Checked = false;

            try
            {
                frmHelloWorld.secondClassSet2Pic[3] = null;
            }
            catch { }
        }

        private void btn_seconClassValueDel2_4_Click(object sender, EventArgs e)
        {
            //删除所有东西
            textBox_seconClassSet2_4.Text = "";
            textBox_seconClassValue2_4.Text = "";
            checkBox_seconClassValue_Pic2_4.Checked = false;

            try
            {
                frmHelloWorld.secondClassSet2Pic[4] = null;
            }
            catch { }
        }

        private void btn_seconClassValueDel2_5_Click(object sender, EventArgs e)
        {
            //删除所有东西
            textBox_seconClassSet2_5.Text = "";
            textBox_seconClassValue2_5.Text = "";
            checkBox_seconClassValue_Pic2_5.Checked = false;

            try
            {
                frmHelloWorld.secondClassSet2Pic[5] = null;
            }
            catch { }
        }

        private void btn_seconClassValueDel2_6_Click(object sender, EventArgs e)
        {
            //删除所有东西
            textBox_seconClassSet2_6.Text = "";
            textBox_seconClassValue2_6.Text = "";
            checkBox_seconClassValue_Pic2_6.Checked = false;

            try
            {
                frmHelloWorld.secondClassSet2Pic[6] = null;
            }
            catch { }
        }

        private void btn_seconClassValueDel2_7_Click(object sender, EventArgs e)
        {
            //删除所有东西
            textBox_seconClassSet2_7.Text = "";
            textBox_seconClassValue2_7.Text = "";
            checkBox_seconClassValue_Pic2_7.Checked = false;
            try
            {
                frmHelloWorld.secondClassSet2Pic[7] = null;
            }
            catch { }
        }

        private void btn_seconClassValueDel2_8_Click(object sender, EventArgs e)
        {
            //删除所有东西
            textBox_seconClassSet2_8.Text = "";
            textBox_seconClassValue2_8.Text = "";
            checkBox_seconClassValue_Pic2_8.Checked = false;

            try
            {
                frmHelloWorld.secondClassSet2Pic[8] = null;
            }
            catch { }
        }

        private void btn_seconClassValueDel2_9_Click(object sender, EventArgs e)
        {
            //删除所有东西
            textBox_seconClassSet2_9.Text = "";
            textBox_seconClassValue2_9.Text = "";
            checkBox_seconClassValue_Pic2_9.Checked = false;

            try
            {
                frmHelloWorld.secondClassSet2Pic[9] = null;
            }
            catch { }
        }

        private void btn_seconClassValueDel2_10_Click(object sender, EventArgs e)
        {
            //删除所有东西
            textBox_seconClassSet2_10.Text = "";
            textBox_seconClassValue2_10.Text = "";
            checkBox_seconClassValue_Pic2_10.Checked = false;

            try
            {
                frmHelloWorld.secondClassSet2Pic[10] = null;
            }
            catch { }
        }

        private void btn_seconClassValueDel2_11_Click(object sender, EventArgs e)
        {
            //删除所有东西
            textBox_seconClassSet2_11.Text = "";
            textBox_seconClassValue2_11.Text = "";
            checkBox_seconClassValue_Pic2_11.Checked = false;

            try
            {
                frmHelloWorld.secondClassSet2Pic[11] = null;
            }
            catch { }
        }

        private void btn_seconClassValueDel2_12_Click(object sender, EventArgs e)
        {
            //删除所有东西
            textBox_seconClassSet2_12.Text = "";
            textBox_seconClassValue2_12.Text = "";
            checkBox_seconClassValue_Pic2_12.Checked = false;

            try
            {
                frmHelloWorld.secondClassSet2Pic[12] = null;
            }
            catch { }
        }

        private void btn_seconClassValueDel2_13_Click(object sender, EventArgs e)
        {
            //删除所有东西
            textBox_seconClassSet2_13.Text = "";
            textBox_seconClassValue2_13.Text = "";
            checkBox_seconClassValue_Pic2_13.Checked = false;

            try
            {
                frmHelloWorld.secondClassSet2Pic[13] = null;
            }
            catch { }
        }

        private void btn_seconClassValueDel2_14_Click(object sender, EventArgs e)
        {
            //删除所有东西
            textBox_seconClassSet2_14.Text = "";
            textBox_seconClassValue2_14.Text = "";
            checkBox_seconClassValue_Pic2_14.Checked = false;

            try
            {
                frmHelloWorld.secondClassSet2Pic[14] = null;
            }
            catch { }
        }

        #endregion
        
        #region 删除ctrl按钮
        private void btn_seconClassValueDel3_0_Click(object sender, EventArgs e)
        {
            //删除所有东西
            textBox_seconClassSet3_0.Text = "";
            textBox_seconClassValue3_0.Text = "";
        }

        private void btn_seconClassValueDel3_1_Click(object sender, EventArgs e)
        {
            textBox_seconClassSet3_1.Text = "";
            textBox_seconClassValue3_1.Text = "";
        }

        private void btn_seconClassValueDel3_2_Click(object sender, EventArgs e)
        {
            textBox_seconClassSet3_2.Text = "";
            textBox_seconClassValue3_2.Text = "";
        }

        private void btn_seconClassValueDel3_3_Click(object sender, EventArgs e)
        {
            textBox_seconClassSet3_3.Text = "";
            textBox_seconClassValue3_3.Text = "";
        }

        private void btn_seconClassValueDel3_4_Click(object sender, EventArgs e)
        {
            textBox_seconClassSet3_4.Text = "";
            textBox_seconClassValue3_4.Text = "";
        }

        private void btn_seconClassValueDel3_5_Click(object sender, EventArgs e)
        {
            textBox_seconClassSet3_5.Text = "";
            textBox_seconClassValue3_5.Text = "";
        }

        private void btn_seconClassValueDel3_6_Click(object sender, EventArgs e)
        {
            textBox_seconClassSet3_6.Text = "";
            textBox_seconClassValue3_6.Text = "";
        }

        private void btn_seconClassValueDel3_7_Click(object sender, EventArgs e)
        {
            textBox_seconClassSet3_7.Text = "";
            textBox_seconClassValue3_7.Text = "";
        }

        private void btn_seconClassValueDel3_8_Click(object sender, EventArgs e)
        {
            textBox_seconClassSet3_8.Text = "";
            textBox_seconClassValue3_8.Text = "";
        }

        private void btn_seconClassValueDel3_9_Click(object sender, EventArgs e)
        {
            textBox_seconClassSet3_9.Text = "";
            textBox_seconClassValue3_9.Text = "";
        }

        private void btn_seconClassValueDel3_10_Click(object sender, EventArgs e)
        {
            textBox_seconClassSet3_10.Text = "";
            textBox_seconClassValue3_10.Text = "";
        }

        private void btn_seconClassValueDel3_11_Click(object sender, EventArgs e)
        {
            textBox_seconClassSet3_11.Text = "";
            textBox_seconClassValue3_11.Text = "";
        }

        private void btn_seconClassValueDel3_12_Click(object sender, EventArgs e)
        {
            textBox_seconClassSet3_12.Text = "";
            textBox_seconClassValue3_12.Text = "";
        }

        private void btn_seconClassValueDel3_13_Click(object sender, EventArgs e)
        {
            textBox_seconClassSet3_13.Text = "";
            textBox_seconClassValue3_13.Text = "";
        }

        private void btn_seconClassValueDel3_14_Click(object sender, EventArgs e)
        {
            textBox_seconClassSet3_14.Text = "";
            textBox_seconClassValue3_14.Text = "";
        }
        #endregion

        #region tuiChanged
        private void textBox_seconClassSet2_0_TextChanged(object sender, EventArgs e)
        {
            tuiChanged = true;
        }

        private void textBox_seconClassSet2_1_TextChanged(object sender, EventArgs e)
        {
            tuiChanged = true;
        }

        private void textBox_seconClassSet2_2_TextChanged(object sender, EventArgs e)
        {
            tuiChanged = true;
        }

        private void textBox_seconClassSet2_3_TextChanged(object sender, EventArgs e)
        {
            tuiChanged = true;
        }

        private void textBox_seconClassSet2_4_TextChanged(object sender, EventArgs e)
        {
            tuiChanged = true;
        }

        private void textBox_seconClassSet2_5_TextChanged(object sender, EventArgs e)
        {
            tuiChanged = true;
        }

        private void textBox_seconClassSet2_6_TextChanged(object sender, EventArgs e)
        {
            tuiChanged = true;
        }

        private void textBox_seconClassSet2_7_TextChanged(object sender, EventArgs e)
        {
            tuiChanged = true;
        }

        private void textBox_seconClassSet2_8_TextChanged(object sender, EventArgs e)
        {
            tuiChanged = true;
        }

        private void textBox_seconClassSet2_9_TextChanged(object sender, EventArgs e)
        {
            tuiChanged = true;

        }

        private void textBox_seconClassSet2_10_TextChanged(object sender, EventArgs e)
        {
            tuiChanged = true;
        }

        private void textBox_seconClassSet2_11_TextChanged(object sender, EventArgs e)
        {
            tuiChanged = true;

        }

        private void textBox_seconClassSet2_12_TextChanged(object sender, EventArgs e)
        {
            tuiChanged = true;
        }

        private void textBox_seconClassSet2_13_TextChanged(object sender, EventArgs e)
        {
            tuiChanged = true;
        }

        private void textBox_seconClassSet2_14_TextChanged(object sender, EventArgs e)
        {
            tuiChanged = true;
        }



        private void textBox_seconClassValue2_14_TextChanged(object sender, EventArgs e)
        {
            tuiChanged = true;
        }

        private void textBox_seconClassValue2_13_TextChanged(object sender, EventArgs e)
        {
            tuiChanged = true;
        }

        private void textBox_seconClassValue2_12_TextChanged(object sender, EventArgs e)
        {
            tuiChanged = true;

        }

        private void textBox_seconClassValue2_11_TextChanged(object sender, EventArgs e)
        {
            tuiChanged = true;
        }

        private void textBox_seconClassValue2_10_TextChanged(object sender, EventArgs e)
        {
            tuiChanged = true;

        }

        private void textBox_seconClassValue2_9_TextChanged(object sender, EventArgs e)
        {
            tuiChanged = true;

        }

        private void textBox_seconClassValue2_8_TextChanged(object sender, EventArgs e)
        {
            tuiChanged = true;

        }

        private void textBox_seconClassValue2_7_TextChanged(object sender, EventArgs e)
        {
            tuiChanged = true;

        }

        private void textBox_seconClassValue2_6_TextChanged(object sender, EventArgs e)
        {
            tuiChanged = true;

        }

        private void textBox_seconClassValue2_5_TextChanged(object sender, EventArgs e)
        {
            tuiChanged = true;

        }

        private void textBox_seconClassValue2_4_TextChanged(object sender, EventArgs e)
        {
            tuiChanged = true;

        }

        private void textBox_seconClassValue2_3_TextChanged(object sender, EventArgs e)
        {
            tuiChanged = true;

        }

        private void textBox_seconClassValue2_2_TextChanged(object sender, EventArgs e)
        {
            tuiChanged = true;

        }

        private void textBox_seconClassValue2_1_TextChanged(object sender, EventArgs e)
        {
            tuiChanged = true;

        }

        private void textBox_seconClassValue2_0_TextChanged(object sender, EventArgs e)
        {
            tuiChanged = true;

        }
        #endregion

        #region pic checkBox changed
        private void checkBox_seconClassValue_Pic2_0_CheckedChanged(object sender, EventArgs e)
        {
            int picIndex = (int)numericUpDown_TuiIndex.Value * 15 + 0;
            if (checkBox_seconClassValue_Pic2_0.Checked == false && picIndex < frmHelloWorld.secondClassSet2Pic.Length)
            {
                if (MessageBox.Show("这样操作将会删除图片，确定？", "删图提示", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    frmHelloWorld.AddPic(picIndex, null);
                else
                    checkBox_seconClassValue_Pic2_0.Checked = true;
            }
            tuiChanged = true;
        }

        private void checkBox_seconClassValue_Pic2_1_CheckedChanged(object sender, EventArgs e)
        {
            int picIndex = (int)numericUpDown_TuiIndex.Value * 15 + 1;
            if (checkBox_seconClassValue_Pic2_1.Checked == false && picIndex < frmHelloWorld.secondClassSet2Pic.Length)
            {
                if (MessageBox.Show("这样操作将会删除图片，确定？", "删图提示", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    frmHelloWorld.AddPic(picIndex, null);
                else
                    checkBox_seconClassValue_Pic2_1.Checked = true;
            }
            tuiChanged = true;
        }

        private void checkBox_seconClassValue_Pic2_2_CheckedChanged(object sender, EventArgs e)
        {
            int picIndex = (int)numericUpDown_TuiIndex.Value * 15 + 2;
            if (checkBox_seconClassValue_Pic2_2.Checked == false && picIndex < frmHelloWorld.secondClassSet2Pic.Length)
            {
                if (MessageBox.Show("这样操作将会删除图片，确定？", "删图提示", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    frmHelloWorld.AddPic(picIndex, null);
                else
                    checkBox_seconClassValue_Pic2_2.Checked = true;
            }
            tuiChanged = true;
        }

        private void checkBox_seconClassValue_Pic2_3_CheckedChanged(object sender, EventArgs e)
        {
            int picIndex = (int)numericUpDown_TuiIndex.Value * 15 + 3;
            if (checkBox_seconClassValue_Pic2_3.Checked == false && picIndex < frmHelloWorld.secondClassSet2Pic.Length)
            {
                if (MessageBox.Show("这样操作将会删除图片，确定？", "删图提示", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    frmHelloWorld.AddPic(picIndex, null);
                else
                    checkBox_seconClassValue_Pic2_3.Checked = true;
            }
            tuiChanged = true;
        }

        private void checkBox_seconClassValue_Pic2_4_CheckedChanged(object sender, EventArgs e)
        {
            int picIndex = (int)numericUpDown_TuiIndex.Value * 15 + 4;
            if (checkBox_seconClassValue_Pic2_4.Checked == false && picIndex < frmHelloWorld.secondClassSet2Pic.Length)
            {
                if (MessageBox.Show("这样操作将会删除图片，确定？", "删图提示", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    frmHelloWorld.AddPic(picIndex, null);
                else
                    checkBox_seconClassValue_Pic2_4.Checked = true;
            }
            tuiChanged = true;
        }

        private void checkBox_seconClassValue_Pic2_5_CheckedChanged(object sender, EventArgs e)
        {
            int picIndex = (int)numericUpDown_TuiIndex.Value * 15 + 5;
            if (checkBox_seconClassValue_Pic2_5.Checked == false && picIndex < frmHelloWorld.secondClassSet2Pic.Length)
            {
                if (MessageBox.Show("这样操作将会删除图片，确定？", "删图提示", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    frmHelloWorld.AddPic(picIndex, null);
                else
                    checkBox_seconClassValue_Pic2_5.Checked = true;
            }
            tuiChanged = true;
        }

        private void checkBox_seconClassValue_Pic2_14_CheckedChanged(object sender, EventArgs e)
        {
            int picIndex = (int)numericUpDown_TuiIndex.Value * 15 + 14;
            if (checkBox_seconClassValue_Pic2_14.Checked == false && picIndex < frmHelloWorld.secondClassSet2Pic.Length)
            {
                if (MessageBox.Show("这样操作将会删除图片，确定？", "删图提示", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    frmHelloWorld.AddPic(picIndex, null);
                else
                    checkBox_seconClassValue_Pic2_14.Checked = true;
            }
            tuiChanged = true;
        }

        private void checkBox_seconClassValue_Pic2_13_CheckedChanged(object sender, EventArgs e)
        {
            int picIndex = (int)numericUpDown_TuiIndex.Value * 15 + 13;
            if (checkBox_seconClassValue_Pic2_13.Checked == false && picIndex < frmHelloWorld.secondClassSet2Pic.Length)
            {
                if (MessageBox.Show("这样操作将会删除图片，确定？", "删图提示", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    frmHelloWorld.AddPic(picIndex, null);
                else
                    checkBox_seconClassValue_Pic2_13.Checked = true;
            }
            tuiChanged = true;
        }

        private void checkBox_seconClassValue_Pic2_12_CheckedChanged(object sender, EventArgs e)
        {
            int picIndex = (int)numericUpDown_TuiIndex.Value * 15 + 12;
            if (checkBox_seconClassValue_Pic2_12.Checked == false && picIndex < frmHelloWorld.secondClassSet2Pic.Length)
            {
                if (MessageBox.Show("这样操作将会删除图片，确定？", "删图提示", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    frmHelloWorld.AddPic(picIndex, null);
                else
                    checkBox_seconClassValue_Pic2_12.Checked = true;
            }
            tuiChanged = true;
        }

        private void checkBox_seconClassValue_Pic2_11_CheckedChanged(object sender, EventArgs e)
        {
            int picIndex = (int)numericUpDown_TuiIndex.Value * 15 + 11;
            if (checkBox_seconClassValue_Pic2_11.Checked == false && picIndex < frmHelloWorld.secondClassSet2Pic.Length)
            {
                if (MessageBox.Show("这样操作将会删除图片，确定？", "删图提示", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    frmHelloWorld.AddPic(picIndex, null);
                else
                    checkBox_seconClassValue_Pic2_11.Checked = true;
            }
            tuiChanged = true;
        }

        private void checkBox_seconClassValue_Pic2_10_CheckedChanged(object sender, EventArgs e)
        {
            int picIndex = (int)numericUpDown_TuiIndex.Value * 15 + 10;
            if (checkBox_seconClassValue_Pic2_10.Checked == false && picIndex < frmHelloWorld.secondClassSet2Pic.Length)
            {
                if (MessageBox.Show("这样操作将会删除图片，确定？", "删图提示", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    frmHelloWorld.AddPic(picIndex, null);
                else
                    checkBox_seconClassValue_Pic2_10.Checked = true;
            }
            tuiChanged = true;
        }

        private void checkBox_seconClassValue_Pic2_9_CheckedChanged(object sender, EventArgs e)
        {
            int picIndex = (int)numericUpDown_TuiIndex.Value * 15 + 9;
            if (checkBox_seconClassValue_Pic2_9.Checked == false && picIndex < frmHelloWorld.secondClassSet2Pic.Length)
            {
                if (MessageBox.Show("这样操作将会删除图片，确定？", "删图提示", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    frmHelloWorld.AddPic(picIndex, null);
                else
                    checkBox_seconClassValue_Pic2_9.Checked = true;
            }
            tuiChanged = true;
        }

        private void checkBox_seconClassValue_Pic2_8_CheckedChanged(object sender, EventArgs e)
        {
            int picIndex = (int)numericUpDown_TuiIndex.Value * 15 + 8;
            if (checkBox_seconClassValue_Pic2_8.Checked == false && picIndex < frmHelloWorld.secondClassSet2Pic.Length)
            {
                if (MessageBox.Show("这样操作将会删除图片，确定？", "删图提示", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    frmHelloWorld.AddPic(picIndex, null);
                else
                    checkBox_seconClassValue_Pic2_8.Checked = true;
            }
            tuiChanged = true;
        }

        private void checkBox_seconClassValue_Pic2_7_CheckedChanged(object sender, EventArgs e)
        {
            int picIndex = (int)numericUpDown_TuiIndex.Value * 15 + 7;
            if (checkBox_seconClassValue_Pic2_7.Checked == false && picIndex < frmHelloWorld.secondClassSet2Pic.Length)
            {
                if (MessageBox.Show("这样操作将会删除图片，确定？", "删图提示", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    frmHelloWorld.AddPic(picIndex, null);
                else
                    checkBox_seconClassValue_Pic2_7.Checked = true;
            }
            tuiChanged = true;
        }

        private void checkBox_seconClassValue_Pic2_6_CheckedChanged(object sender, EventArgs e)
        {
            int picIndex = (int)numericUpDown_TuiIndex.Value * 15 + 6;
            if (checkBox_seconClassValue_Pic2_6.Checked == false && picIndex < frmHelloWorld.secondClassSet2Pic.Length)
            {
                if (MessageBox.Show("这样操作将会删除图片，确定？", "删图提示", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    frmHelloWorld.AddPic(picIndex, null);
                else
                    checkBox_seconClassValue_Pic2_6.Checked = true;
            }
            tuiChanged = true;
        }

        #endregion
    }
}
