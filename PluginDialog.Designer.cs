namespace DataCutterFG
{
    partial class PluginDialog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PluginDialog));
            this.listBoxPluginList = new System.Windows.Forms.ListBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.button1 = new System.Windows.Forms.Button();
            this.lb = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lbvName = new System.Windows.Forms.Label();
            this.lbvKakutyoushi = new System.Windows.Forms.Label();
            this.lbvKobetu = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lbvAuthor = new System.Windows.Forms.Label();
            this.lbvVersion = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // listBoxPluginList
            // 
            this.listBoxPluginList.FormattingEnabled = true;
            this.listBoxPluginList.HorizontalScrollbar = true;
            this.listBoxPluginList.ItemHeight = 12;
            this.listBoxPluginList.Location = new System.Drawing.Point(6, 24);
            this.listBoxPluginList.Name = "listBoxPluginList";
            this.listBoxPluginList.Size = new System.Drawing.Size(227, 148);
            this.listBoxPluginList.TabIndex = 0;
            this.listBoxPluginList.SelectedIndexChanged += new System.EventHandler(this.listBox1_SelectedIndexChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lbvVersion);
            this.groupBox1.Controls.Add(this.lbvAuthor);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.lbvKobetu);
            this.groupBox1.Controls.Add(this.lbvKakutyoushi);
            this.groupBox1.Controls.Add(this.lbvName);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.lb);
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Location = new System.Drawing.Point(278, 23);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(229, 183);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "情報";
            // 
            // button1
            // 
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Location = new System.Drawing.Point(6, 147);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(85, 30);
            this.button1.TabIndex = 0;
            this.button1.Text = "個別設定";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // lb
            // 
            this.lb.AutoSize = true;
            this.lb.Location = new System.Drawing.Point(12, 18);
            this.lb.Name = "lb";
            this.lb.Size = new System.Drawing.Size(31, 12);
            this.lb.TabIndex = 1;
            this.lb.Text = "名前:";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.listBoxPluginList);
            this.groupBox2.Location = new System.Drawing.Point(12, 23);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(242, 183);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "プラグイン一覧";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 40);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "抽出拡張子:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 122);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(79, 12);
            this.label2.TabIndex = 3;
            this.label2.Text = "個別設定有無:";
            // 
            // lbvName
            // 
            this.lbvName.AutoSize = true;
            this.lbvName.Location = new System.Drawing.Point(53, 19);
            this.lbvName.Name = "lbvName";
            this.lbvName.Size = new System.Drawing.Size(0, 12);
            this.lbvName.TabIndex = 4;
            // 
            // lbvKakutyoushi
            // 
            this.lbvKakutyoushi.AutoSize = true;
            this.lbvKakutyoushi.Location = new System.Drawing.Point(91, 40);
            this.lbvKakutyoushi.Name = "lbvKakutyoushi";
            this.lbvKakutyoushi.Size = new System.Drawing.Size(0, 12);
            this.lbvKakutyoushi.TabIndex = 5;
            // 
            // lbvKobetu
            // 
            this.lbvKobetu.AutoSize = true;
            this.lbvKobetu.Location = new System.Drawing.Point(97, 122);
            this.lbvKobetu.Name = "lbvKobetu";
            this.lbvKobetu.Size = new System.Drawing.Size(0, 12);
            this.lbvKobetu.TabIndex = 6;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 69);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(52, 12);
            this.label3.TabIndex = 7;
            this.label3.Text = "バージョン:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(13, 91);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(43, 12);
            this.label4.TabIndex = 8;
            this.label4.Text = "作成者:";
            // 
            // lbvAuthor
            // 
            this.lbvAuthor.AutoSize = true;
            this.lbvAuthor.Location = new System.Drawing.Point(63, 91);
            this.lbvAuthor.Name = "lbvAuthor";
            this.lbvAuthor.Size = new System.Drawing.Size(0, 12);
            this.lbvAuthor.TabIndex = 9;
            // 
            // lbvVersion
            // 
            this.lbvVersion.AutoSize = true;
            this.lbvVersion.Location = new System.Drawing.Point(71, 69);
            this.lbvVersion.Name = "lbvVersion";
            this.lbvVersion.Size = new System.Drawing.Size(0, 12);
            this.lbvVersion.TabIndex = 10;
            // 
            // PluginDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(520, 232);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "PluginDialog";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "プラグイン設定";
            this.Load += new System.EventHandler(this.PluginDialog_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox listBoxPluginList;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label lb;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lbvKakutyoushi;
        private System.Windows.Forms.Label lbvName;
        private System.Windows.Forms.Label lbvKobetu;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lbvVersion;
        private System.Windows.Forms.Label lbvAuthor;
    }
}