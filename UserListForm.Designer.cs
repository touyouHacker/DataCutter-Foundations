namespace DataCutterFG
{
    partial class UserListForm
    {
        /// <summary>
        /// 必要なデザイナ変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージ リソースが破棄される場合 true、破棄されない場合は false です。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナで生成されたコード

        /// <summary>
        /// デザイナ サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディタで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UserListForm));
            this.lbUserList = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtStart = new System.Windows.Forms.TextBox();
            this.txtEnd = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lbUserFile = new System.Windows.Forms.Label();
            this.btUserFileOpen = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.btSaveUserFile = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.txtKakutyousi = new System.Windows.Forms.TextBox();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // lbUserList
            // 
            this.lbUserList.FormattingEnabled = true;
            this.lbUserList.ItemHeight = 12;
            this.lbUserList.Location = new System.Drawing.Point(12, 12);
            this.lbUserList.Name = "lbUserList";
            this.lbUserList.Size = new System.Drawing.Size(255, 112);
            this.lbUserList.TabIndex = 0;
            this.lbUserList.SelectedIndexChanged += new System.EventHandler(this.lbUserList_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 179);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(100, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "START ヘッダ　(＊)";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(10, 229);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(54, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "ENDヘッダ";
            // 
            // txtStart
            // 
            this.txtStart.Location = new System.Drawing.Point(12, 194);
            this.txtStart.Name = "txtStart";
            this.txtStart.Size = new System.Drawing.Size(426, 19);
            this.txtStart.TabIndex = 3;
            // 
            // txtEnd
            // 
            this.txtEnd.Location = new System.Drawing.Point(12, 244);
            this.txtEnd.Name = "txtEnd";
            this.txtEnd.Size = new System.Drawing.Size(426, 19);
            this.txtEnd.TabIndex = 4;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lbUserFile);
            this.groupBox1.Controls.Add(this.btUserFileOpen);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Location = new System.Drawing.Point(277, 15);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(187, 108);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "情報";
            // 
            // lbUserFile
            // 
            this.lbUserFile.AutoSize = true;
            this.lbUserFile.Location = new System.Drawing.Point(13, 44);
            this.lbUserFile.Name = "lbUserFile";
            this.lbUserFile.Size = new System.Drawing.Size(84, 12);
            this.lbUserFile.TabIndex = 2;
            this.lbUserFile.Text = "defaultUser.xml";
            // 
            // btUserFileOpen
            // 
            this.btUserFileOpen.Location = new System.Drawing.Point(12, 74);
            this.btUserFileOpen.Name = "btUserFileOpen";
            this.btUserFileOpen.Size = new System.Drawing.Size(85, 24);
            this.btUserFileOpen.TabIndex = 1;
            this.btUserFileOpen.Text = "ファイル参照";
            this.btUserFileOpen.UseVisualStyleBackColor = true;
            this.btUserFileOpen.Click += new System.EventHandler(this.btUserFileOpen_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(10, 22);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(91, 12);
            this.label3.TabIndex = 0;
            this.label3.Text = "ユーザーファイル名";
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(13, 300);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(187, 19);
            this.txtName.TabIndex = 6;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 280);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(101, 12);
            this.label4.TabIndex = 7;
            this.label4.Text = "ユーザー設定名(＊)";
            // 
            // btSaveUserFile
            // 
            this.btSaveUserFile.Location = new System.Drawing.Point(206, 296);
            this.btSaveUserFile.Name = "btSaveUserFile";
            this.btSaveUserFile.Size = new System.Drawing.Size(76, 27);
            this.btSaveUserFile.TabIndex = 8;
            this.btSaveUserFile.Text = "新規追加";
            this.btSaveUserFile.UseVisualStyleBackColor = true;
            this.btSaveUserFile.Click += new System.EventHandler(this.btSaveUserFile_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Location = new System.Drawing.Point(12, 344);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(373, 87);
            this.groupBox2.TabIndex = 9;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "説明";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(16, 19);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(344, 60);
            this.label5.TabIndex = 0;
            this.label5.Text = "※STARTヘッダ＆ENDヘッダには16進数表記で\r\nバイト配列を指定してください、(＊)項目は必須です。\r\nENDがない場合STARTヘッダを自動設定します。\r" +
                "\nデフォルトでは defaultUser.xml を読み込みますが別ユーザーの指定した\r\n設定ファイルを読み込ませることができます。";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(12, 138);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(143, 12);
            this.label6.TabIndex = 10;
            this.label6.Text = "拡張子（ファイル展開用）(＊)";
            // 
            // txtKakutyousi
            // 
            this.txtKakutyousi.Location = new System.Drawing.Point(14, 153);
            this.txtKakutyousi.Name = "txtKakutyousi";
            this.txtKakutyousi.Size = new System.Drawing.Size(121, 19);
            this.txtKakutyousi.TabIndex = 11;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // UserListForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(484, 443);
            this.Controls.Add(this.txtKakutyousi);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.btSaveUserFile);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.txtEnd);
            this.Controls.Add(this.txtStart);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lbUserList);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "UserListForm";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "ユーザー設定編集";
            this.Load += new System.EventHandler(this.UserListForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox lbUserList;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtStart;
        private System.Windows.Forms.TextBox txtEnd;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btUserFileOpen;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btSaveUserFile;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtKakutyousi;
        private System.Windows.Forms.Label lbUserFile;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
    }
}