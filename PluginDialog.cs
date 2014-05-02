using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace DataCutterFG
{
    public partial class PluginDialog : Form
    {
        DataCutter_PlugIn.PluginInterface[] plugins = null;
        PluginInfo[] pis = null;

        public PluginDialog(PluginInfo [] _pis,DataCutter_PlugIn.PluginInterface[] _plugins)
        {
            pis = _pis;
            plugins = _plugins;
            InitializeComponent();
            listGUIAddNameList();
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBoxPluginList.SelectedIndex > -1)
            {
                lbvName.Text = plugins[listBoxPluginList.SelectedIndex].Pluginame;
              //  lbvKakutyoushi = plugins[listBoxPluginList.SelectedIndex].Attributenames;
                foreach (String kt in plugins[listBoxPluginList.SelectedIndex].Attributenames)
                {
                    lbvKakutyoushi.Text += kt + ",";
                }

                lbvVersion.Text = plugins[listBoxPluginList.SelectedIndex].Version;
                lbvAuthor.Text = plugins[listBoxPluginList.SelectedIndex].Copyright;

                if (plugins[listBoxPluginList.SelectedIndex].OptionDialog == true)
                {
                    lbvKobetu.Text = "あり";
                }
                else
                {
                    lbvKobetu.Text = "なし";
                }
            }
        }



        private void listGUIAddNameList()
        {
            listBoxPluginList.Items.Clear();
            
            for (int i = 0; i < plugins.Length; i++)
            {

                listBoxPluginList.Items.Add(pis[i].DllName);

            }
        }

        private void PluginDialog_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            // プラグイン固有の設定ダイアログを表示します
            plugins[listBoxPluginList.SelectedIndex].showOptionDialog();
        }
    }
}