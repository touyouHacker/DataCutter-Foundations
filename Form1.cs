using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.IO;
using System.Collections.Generic;

using System.Threading;

//DataCutter Foundations GUI
namespace DataCutterFG
{
    /// <summary>
    /// Form1 �̊T�v�̐����ł��B
    /// </summary>
    public class DCForm1 : System.Windows.Forms.Form
    {
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.StatusBar statusBar1;
        private System.Windows.Forms.MainMenu mainMenu1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button BTtarget;
        private System.Windows.Forms.Button BTsyutu;
        private System.Windows.Forms.ComboBox COMBkaku;
        private System.Windows.Forms.TextBox TBtarget;
        private System.Windows.Forms.TextBox TBsyutu;
        private System.Windows.Forms.TextBox TBBlockBuff;
        private System.Windows.Forms.TextBox TBDBlog;
        private System.Windows.Forms.CheckBox CBSymrate;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox TBCBN;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button BTexe;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.MenuItem menuItem1;
        private System.Windows.Forms.MenuItem menuItem2;
        private System.Windows.Forms.MenuItem menuItem3;
        private System.Windows.Forms.MenuItem menuItem4;
        private System.Windows.Forms.NumericUpDown NuupKETA;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label11;
        private IContainer components;


        // --------------------------------------------- MY ��`�ϐ�
        private bool WARKING = false;
        private ArrayList usaList;
        private UserListForm ULF;
        private String SelectKakutyousi = null;
        private MenuItem menuItem5;
        private MenuItem menuItem6;
        private MenuItem menuItem7;

        // �����œn���Ă����t�@�C�������i�[���܂�
        private static String ArgFilename = null;

        // �v���O�C�����̊i�[(DLL�t�@�C����)
        private PluginInfo[] pis = null;
        // �v���O�C���̃I�u�W�F�N�g�i�[
        private DataCutter_PlugIn.PluginInterface[] plugins = null;
        // �v���O�C��DLL���Ə����ł���g���q�̃y�A������z����i�[
        private String[] PluginNameAndKaku = null;


        // ---------------------------------------------------------

        public DCForm1()
        {
            //
            // Windows �t�H�[�� �f�U�C�i �T�|�[�g�ɕK�v�ł��B
            //
            InitializeComponent();

            if (ArgFilename != null)
            {
                FileGUI_Initializer(ArgFilename);
            }
        }

        /// <summary>
        /// �g�p����Ă��郊�\�[�X�Ɍ㏈�������s���܂��B
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }
            }
            base.Dispose(disposing);
        }

        #region Windows �t�H�[�� �f�U�C�i�Ő������ꂽ�R�[�h
        /// <summary>
        /// �f�U�C�i �T�|�[�g�ɕK�v�ȃ��\�b�h�ł��B���̃��\�b�h�̓��e��
        /// �R�[�h �G�f�B�^�ŕύX���Ȃ��ł��������B
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DCForm1));
            this.BTtarget = new System.Windows.Forms.Button();
            this.BTsyutu = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.BTexe = new System.Windows.Forms.Button();
            this.panel3 = new System.Windows.Forms.Panel();
            this.TBDBlog = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.NuupKETA = new System.Windows.Forms.NumericUpDown();
            this.CBSymrate = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.TBBlockBuff = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.TBsyutu = new System.Windows.Forms.TextBox();
            this.TBtarget = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.TBCBN = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.COMBkaku = new System.Windows.Forms.ComboBox();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.statusBar1 = new System.Windows.Forms.StatusBar();
            this.mainMenu1 = new System.Windows.Forms.MainMenu(this.components);
            this.menuItem1 = new System.Windows.Forms.MenuItem();
            this.menuItem2 = new System.Windows.Forms.MenuItem();
            this.menuItem5 = new System.Windows.Forms.MenuItem();
            this.menuItem6 = new System.Windows.Forms.MenuItem();
            this.menuItem7 = new System.Windows.Forms.MenuItem();
            this.menuItem3 = new System.Windows.Forms.MenuItem();
            this.menuItem4 = new System.Windows.Forms.MenuItem();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.panel1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.NuupKETA)).BeginInit();
            this.SuspendLayout();
            // 
            // BTtarget
            // 
            this.BTtarget.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.BTtarget.Location = new System.Drawing.Point(440, 32);
            this.BTtarget.Name = "BTtarget";
            this.BTtarget.Size = new System.Drawing.Size(88, 24);
            this.BTtarget.TabIndex = 0;
            this.BTtarget.Text = "�J��(&O)";
            this.BTtarget.Click += new System.EventHandler(this.BTtarget_Click);
            // 
            // BTsyutu
            // 
            this.BTsyutu.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.BTsyutu.Location = new System.Drawing.Point(440, 72);
            this.BTsyutu.Name = "BTsyutu";
            this.BTsyutu.Size = new System.Drawing.Size(88, 24);
            this.BTsyutu.TabIndex = 1;
            this.BTsyutu.Text = "�J��(&D)";
            this.BTsyutu.Click += new System.EventHandler(this.BTsyutu_Click);
            // 
            // panel1
            // 
            this.panel1.AllowDrop = true;
            this.panel1.BackColor = System.Drawing.SystemColors.Control;
            this.panel1.Controls.Add(this.BTexe);
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.TBsyutu);
            this.panel1.Controls.Add(this.TBtarget);
            this.panel1.Controls.Add(this.BTsyutu);
            this.panel1.Controls.Add(this.BTtarget);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label10);
            this.panel1.Controls.Add(this.TBCBN);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.COMBkaku);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(568, 357);
            this.panel1.TabIndex = 2;
            this.panel1.DragEnter += new System.Windows.Forms.DragEventHandler(this.Drop);
            // 
            // BTexe
            // 
            this.BTexe.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.BTexe.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.BTexe.Location = new System.Drawing.Point(366, 125);
            this.BTexe.Name = "BTexe";
            this.BTexe.Size = new System.Drawing.Size(80, 24);
            this.BTexe.TabIndex = 19;
            this.BTexe.Text = "���s";
            this.BTexe.Click += new System.EventHandler(this.BTexe_Click);
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.TBDBlog);
            this.panel3.Controls.Add(this.label6);
            this.panel3.Location = new System.Drawing.Point(16, 168);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(304, 176);
            this.panel3.TabIndex = 18;
            // 
            // TBDBlog
            // 
            this.TBDBlog.Location = new System.Drawing.Point(8, 24);
            this.TBDBlog.MaxLength = 327670;
            this.TBDBlog.Multiline = true;
            this.TBDBlog.Name = "TBDBlog";
            this.TBDBlog.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.TBDBlog.Size = new System.Drawing.Size(288, 136);
            this.TBDBlog.TabIndex = 11;
            this.TBDBlog.WordWrap = false;
            // 
            // label6
            // 
            this.label6.Font = new System.Drawing.Font("MS UI Gothic", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label6.ForeColor = System.Drawing.Color.DarkBlue;
            this.label6.Location = new System.Drawing.Point(8, 8);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(96, 16);
            this.label6.TabIndex = 12;
            this.label6.Text = "�f�o�b�O���O";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label11);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.NuupKETA);
            this.groupBox1.Controls.Add(this.CBSymrate);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.TBBlockBuff);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Location = new System.Drawing.Point(336, 176);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(216, 112);
            this.groupBox1.TabIndex = 13;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "�I�v�V����";
            // 
            // label11
            // 
            this.label11.Location = new System.Drawing.Point(136, 56);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(24, 16);
            this.label11.TabIndex = 14;
            this.label11.Text = "��";
            // 
            // label9
            // 
            this.label9.Location = new System.Drawing.Point(16, 56);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(64, 16);
            this.label9.TabIndex = 13;
            this.label9.Text = "�A�Ԃ̌���";
            // 
            // NuupKETA
            // 
            this.NuupKETA.Location = new System.Drawing.Point(88, 56);
            this.NuupKETA.Maximum = new decimal(new int[] {
            8,
            0,
            0,
            0});
            this.NuupKETA.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.NuupKETA.Name = "NuupKETA";
            this.NuupKETA.Size = new System.Drawing.Size(40, 19);
            this.NuupKETA.TabIndex = 12;
            this.NuupKETA.Value = new decimal(new int[] {
            4,
            0,
            0,
            0});
            // 
            // CBSymrate
            // 
            this.CBSymrate.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.CBSymrate.Location = new System.Drawing.Point(16, 24);
            this.CBSymrate.Name = "CBSymrate";
            this.CBSymrate.Size = new System.Drawing.Size(192, 16);
            this.CBSymrate.TabIndex = 11;
            this.CBSymrate.Text = "�t�@�C�������܂܂�邩��������";
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(16, 80);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(72, 24);
            this.label1.TabIndex = 7;
            this.label1.Text = "�u���b�N�o�b�t�@";
            // 
            // TBBlockBuff
            // 
            this.TBBlockBuff.Location = new System.Drawing.Point(88, 80);
            this.TBBlockBuff.Name = "TBBlockBuff";
            this.TBBlockBuff.Size = new System.Drawing.Size(56, 19);
            this.TBBlockBuff.TabIndex = 6;
            this.TBBlockBuff.Text = "4096";
            this.TBBlockBuff.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label5
            // 
            this.label5.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label5.Location = new System.Drawing.Point(152, 80);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(40, 16);
            this.label5.TabIndex = 10;
            this.label5.Text = "Byte";
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(24, 72);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(88, 16);
            this.label4.TabIndex = 9;
            this.label4.Text = "�o�̓f�B���N�g��";
            // 
            // TBsyutu
            // 
            this.TBsyutu.Location = new System.Drawing.Point(120, 72);
            this.TBsyutu.Name = "TBsyutu";
            this.TBsyutu.Size = new System.Drawing.Size(312, 19);
            this.TBsyutu.TabIndex = 5;
            // 
            // TBtarget
            // 
            this.TBtarget.Location = new System.Drawing.Point(120, 32);
            this.TBtarget.Name = "TBtarget";
            this.TBtarget.Size = new System.Drawing.Size(312, 19);
            this.TBtarget.TabIndex = 4;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(24, 32);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(88, 23);
            this.label3.TabIndex = 5;
            this.label3.Text = "���o����t�@�C��";
            // 
            // label10
            // 
            this.label10.Location = new System.Drawing.Point(24, 104);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(144, 16);
            this.label10.TabIndex = 13;
            this.label10.Text = "�쐬����A�ԃt�@�C���̖��O";
            // 
            // TBCBN
            // 
            this.TBCBN.Location = new System.Drawing.Point(176, 104);
            this.TBCBN.Name = "TBCBN";
            this.TBCBN.Size = new System.Drawing.Size(184, 19);
            this.TBCBN.TabIndex = 12;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(88, 128);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(72, 16);
            this.label2.TabIndex = 8;
            this.label2.Text = "���o�g���q";
            // 
            // COMBkaku
            // 
            this.COMBkaku.Location = new System.Drawing.Point(176, 128);
            this.COMBkaku.Name = "COMBkaku";
            this.COMBkaku.Size = new System.Drawing.Size(184, 20);
            this.COMBkaku.TabIndex = 3;
            // 
            // statusBar1
            // 
            this.statusBar1.Location = new System.Drawing.Point(0, 341);
            this.statusBar1.Name = "statusBar1";
            this.statusBar1.ShowPanels = true;
            this.statusBar1.Size = new System.Drawing.Size(568, 16);
            this.statusBar1.SizingGrip = false;
            this.statusBar1.TabIndex = 4;
            // 
            // mainMenu1
            // 
            this.mainMenu1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem1,
            this.menuItem5,
            this.menuItem3});
            // 
            // menuItem1
            // 
            this.menuItem1.Index = 0;
            this.menuItem1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem2});
            this.menuItem1.Text = "�t�@�C�� (&F)";
            // 
            // menuItem2
            // 
            this.menuItem2.Index = 0;
            this.menuItem2.Text = "�I��(&X)";
            this.menuItem2.Click += new System.EventHandler(this.DCFG_EXIT);
            // 
            // menuItem5
            // 
            this.menuItem5.Index = 1;
            this.menuItem5.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem6,
            this.menuItem7});
            this.menuItem5.Text = "�c�[��(&T)";
            // 
            // menuItem6
            // 
            this.menuItem6.Index = 0;
            this.menuItem6.Text = "���[�U�[�ݒ�(&U)";
            this.menuItem6.Click += new System.EventHandler(this.menuItem6_Click);
            // 
            // menuItem7
            // 
            this.menuItem7.Index = 1;
            this.menuItem7.Text = "�v���O�C��(&P)";
            this.menuItem7.Click += new System.EventHandler(this.menuItem7_Click);
            // 
            // menuItem3
            // 
            this.menuItem3.Index = 2;
            this.menuItem3.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem4});
            this.menuItem3.Text = "�w���v (&H)";
            // 
            // menuItem4
            // 
            this.menuItem4.Index = 0;
            this.menuItem4.Text = "�o�[�W������� (&A)";
            this.menuItem4.Click += new System.EventHandler(this.VersionDialogShow);
            // 
            // DCForm1
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 12);
            this.ClientSize = new System.Drawing.Size(568, 357);
            this.Controls.Add(this.statusBar1);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Menu = this.mainMenu1;
            this.Name = "DCForm1";
            this.Text = "DataCutter Foundations";
            this.Load += new System.EventHandler(this.DCForm1_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.NuupKETA)).EndInit();
            this.ResumeLayout(false);

        }
        #endregion



        /// <summary>
        /// �A�v���P�[�V�����̃��C�� �G���g�� �|�C���g�ł��B
        /// </summary>
        [STAThread]
        static void Main(String[] args)
        {
            // �t�@�C�����������󂯎��
            if (args.Length != 0)
            {
                ArgFilename = args[0];
            }
            Application.Run(new DCForm1());
        }




        private void WARKING_SendMsgBox()
        {
            if (WARKING == true)
            {
                MessageBox.Show("�������s���ł�", "�G���[",
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
        private void BTtarget_Click(object sender, System.EventArgs e)
        {
            WARKING_SendMsgBox();
            FMOpenFile();

        }

        private void BTsyutu_Click(object sender, System.EventArgs e)
        {
            FMOpenDirectory();
        }

        private void FMOpenFile()
        {
            WARKING_SendMsgBox();
            if (WARKING == true) { return; }
            //string st1;
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                FileGUI_Initializer(openFileDialog1.FileName);
            }
            //�t�@�C����\�Ńp�[�X����TBsyutu��TBBasename�ɑ��

        }
        //FM=FormMethod
        private void FMOpenDirectory()
        {
            WARKING_SendMsgBox();
            if (WARKING == true) { return; }
            if (TBsyutu.Text.Length > 0)
            {
                folderBrowserDialog1.SelectedPath = TBsyutu.Text;
            }
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                TBsyutu.Text = folderBrowserDialog1.SelectedPath + "\\";
            }
        }

        private void BTexe_Click(object sender, System.EventArgs e)
        {
            TBDBlog.Clear();
            WARKING_SendMsgBox();
            if (WARKING == true) { return; }
            //----���̓G���[�`�F�b�N
            if (TBtarget.Text.Length == 0)
            {
                MessageBox.Show("�t�@�C�����I������Ă܂���", "�G���[",
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            if (TBsyutu.Text.Length == 0)
            {
                MessageBox.Show("�o�̓t�H���_���I������Ă܂���", "�G���[",
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            if (false == Directory.Exists(TBsyutu.Text))
            {
                MessageBox.Show("�o�̓t�H���_�����݂��܂���", "�G���[",
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            if (TBCBN.Text.Length == 0)
            {
                MessageBox.Show("�A�ԃt�@�C���̊�{�����s���ł�", "�G���[",
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            if (COMBkaku.Text.Length == 0)
            {
                MessageBox.Show("���o�g���q���I������Ă܂���", "�G���[",
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            //�X���b�h�����Ȃ���GUI���u���b�N����
         
            SelectKakutyousi = COMBkaku.SelectedItem.ToString(); // �ʃX���b�h�łf�t�h�v�f�͌ĂׂȂ�
#if DEBUG
            EXECUT();
#else
            
            Thread THEXECUT = new Thread(new ThreadStart(EXECUT));
            THEXECUT.Start();
#endif
            //THEXECUT.Join();
            //�������~�{�^��
            //�������~����Ƃ��͓r������J�n�ł���悤KITEN�Ȃǂ�ۑ����Ƃ�
            //THEXECUT.Abort();

        }

        private void EXECUT()
        {
            int BaseBaffuer = Convert.ToInt32(TBBlockBuff.Text);
            bool INDEXCREATE = false;
            bool EXECUTE = true;//�f�t�H��true
            int WorkedFileCount = 0;
            // �v���O�C���������𔻒肷��̂Ɏg���t���O
            bool PluginExeFlag = false;
            // �v���O�C���̔z��w��
            int PluginArgMentNumber = 0;

            FileAttributeSet FAS = new FileAttributeSet();
            FAS.SetHeader(SelectKakutyousi);

            //����FAS�ŃG���[�����������ꍇ���[�U�[��`�g���q�̉\��������̂ł�������T��
            if (FAS.ERR == true)
            {
                //FAS�̍č\�z
                foreach (UserSetAttDTO al in usaList)
                {
                    if (SelectKakutyousi == al.name)
                    {
                        FAS = UserSetAttDTO.UserSetAttDTOtoFAS(al);
                    }
                }
            }

            // �v���O�C���̌������s��
            if (FAS.ERR == true)
            {
                foreach (String pl in PluginNameAndKaku)
                {
                    if (SelectKakutyousi == pl)
                    {
                        PluginExeFlag = true;
                    }
                }
            }




            // FAS�̍č\�z���A�v���O�C���̌��������s������G���[
            if (FAS.ERR == true && PluginExeFlag == false)
            {
                MessageBox.Show("�I�������g���q�Z�b�g�ݒ肪�s����������܂���B", "�G���[",
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            if (CBSymrate.Checked == true)
            {
                EXECUTE = false;
            }
            WARKING = true;
            int Start = Environment.TickCount;


            int RenbanKeta = (int)NuupKETA.Value;
            string filename = TBtarget.Text;
            string CreateFileBaseName = TBCBN.Text;
            string BaseDirectory = TBsyutu.Text;

            // �v���O�C�����g�p���Ȃ����o�̎�
            if (PluginExeFlag == false)
            {
                DataCutter DC = new DataCutter();
                WorkedFileCount = DC._cutter(filename, FAS, BaseBaffuer, EXECUTE, INDEXCREATE,
                   CreateFileBaseName, BaseDirectory, RenbanKeta, TBDBlog);
            }
            else
            {
                String [] kakutyoushiAR = SelectKakutyousi.Split(new Char[]{ ':' });
                String kakutyoushi = kakutyoushiAR[kakutyoushiAR.Length - 1];
                String[] PluginNameAr = kakutyoushiAR[0].Split(new char[] { ' ' });
                String pluginname = PluginNameAr[PluginNameAr.Length - 1];

                int ifx = 0;
                foreach (DataCutter_PlugIn.PluginInterface pl in plugins)
                {
                    
                    if (pl.Pluginame == pluginname)
                    {
                        PluginArgMentNumber = ifx; 
                    }

                    ifx++;
                }
                // �v���O�C�����g�p���钊�o�̎�
                /*
                 * // PLUGIN TYPE <Normal> ���������t�@�C������Ԃ��܂�
         int _cutter(
              bool Exe,
              bool IndexFileCreate,
              int buffer_Level,
              int RenbanKeta,
              string filename,
              string kakutyoushi,
              string CreateFileBaseName,
              string BaseDirectory,
              TextBox DebugTB
              );*/
                WorkedFileCount = plugins[PluginArgMentNumber]._cutter(
                    EXECUTE,
                    INDEXCREATE,
                    BaseBaffuer,
                    RenbanKeta,
                    filename,
                    kakutyoushi,
                    CreateFileBaseName,
                    BaseDirectory,
                    TBDBlog
                 );
            }


            int END = Environment.TickCount;
            string apt = String.Format("���o�� {0} /:BaseBuff {1}\r\n", WorkedFileCount, BaseBaffuer);
            TBDBlog.AppendText(apt);
            TBDBlog.AppendText(String.Format("��������: {0}�@�~���b\r\n", END - Start));
            TBDBlog.AppendText("�����������܂���\r\n");
            WARKING = false;
        }



        private void DCForm1_Load(object sender, System.EventArgs e)
        {
            // �v���O�C����ǂݍ���
            PluginLoad();

            // ���ׂĂ̊g���q��ǂݍ���
            KakutyousiLoad();

#if DEBUG
            this.Text = this.Text + " �f�o�b�O";
#endif
        }

        private void VersionDialogShow(object sender, System.EventArgs e)
        {
            VersionDialog VD = new VersionDialog();
            VD.Show();

        }

        private void PluginLoad()
        {
            //�C���X�g�[������Ă���v���O�C���𒲂ׂ�
            pis = PluginInfo.FindPlugins();

            //���ׂẴv���O�C���N���X�̃C���X�^���X���쐬����
            plugins = new DataCutter_PlugIn.PluginInterface[pis.Length];

            for (int i = 0; i < plugins.Length; i++)
            {
                plugins[i] = pis[i].CreateInstance();
            }
        }

        // �g���q�����[�h����i�\�[�X�R�[�h�ׂ�������`��XML&Plugin�j
        private void KakutyousiLoad()
        {
            COMBkaku.Items.Clear();

            FileAttributeSet FAS = new FileAttributeSet();

            COMBkaku.Items.AddRange(FAS.printAttribute());

            // ���[�U�[���X�g�g���q�ǂݍ���
            usaList = UserListForm.UserXmlLoad(UserListForm.defaultfilename);
            COMBkaku.Items.AddRange(UserListForm.UserSetAttDTONameList(usaList));

            //�v���O�C���p�g���q
            COMBkaku.Items.AddRange(PluginWorkKakutyoushi());
        }

        // �v���O�C���̏����ł���g���q�z���Ԃ�([Plugin]�Ƃ������O��t�^)
        private String[] PluginWorkKakutyoushi()
        {

            List<String> kakuList = new List<String>();
            List<String> poolList = new List<String>();
            String[] kakuAr = null;

            foreach (DataCutter_PlugIn.PluginInterface pl in plugins)
            {
                foreach (String ka in pl.Attributenames)
                {
                    poolList.Add("[Plugin] " + pl.Pluginame + ":" + ka);
                }
            }

            kakuAr = new string[poolList.Count];

            for (int i = 0; i < poolList.Count; i++)
            {
                kakuAr[i] = poolList[i];
            }

            PluginNameAndKaku = kakuAr;
            return kakuAr;
        }

        private void DCFG_EXIT(object sender, System.EventArgs e)
        {
            //�������̂Ƃ��͊m�F����
            //�X���b�h���~���[�`��
            this.Close();
            Environment.Exit(0);
        }

        //�h���b�N&�h���b�v����
        private void Drop(object sender, DragEventArgs dea)
        {
            string[] astr = (string[])
                dea.Data.GetData(DataFormats.FileDrop);
            FileGUI_Initializer(astr[0]);
        }

        // �t�@�C������n���ăt�H�[���Ƀt�@�C������f�B���N�g�������͂���܂�
        private void FileGUI_Initializer(String fileame)
        {
            TBtarget.Text = fileame;
            TBsyutu.Text = Path.GetDirectoryName(TBtarget.Text) + "\\";
            TBCBN.Text = Path.GetFileName(TBtarget.Text);
        }

        private void menuItem6_Click(object sender, EventArgs e)
        {
            if (ULF == null)
            {
                ULF = new UserListForm();
            }
            else
            {
                ULF = new UserListForm(ULF.selectedFileFullPath);
            }
            ULF.ShowDialog();

            // ���[�U�[�ݒ�t�@�C���̃����[�h
            KakutyousiLoad();
        }

        private void menuItem7_Click(object sender, EventArgs e)
        {
            PluginDialog pd = new PluginDialog(pis, plugins);
            pd.Show();

            // �v���O�C���̍ă��[�h
        }
    }
}
