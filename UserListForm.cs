using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.Collections;
using System.IO;

/*
 * ユーザーが追加で拡張指定できるための処理です 
 * STARTヘッダENDヘッダ指定だけでは中身の処理（ヘッダに1ファイルのバイト数が埋め込まれているとか）
 * datの中の先頭にファイル名がうめこまれてるとか・・それを暗号解読するとか・・
 * ができないのでプラグインDLL(Interface継承型)形式にするかテンプレートとしてTYPE1,TYPE2のように指定できるように
 * したほうがいいでしょう。
 */
namespace DataCutterFG
{
    public partial class UserListForm : Form
    {

        public static String defaultfilename = "defaultUser.xml";
        public String selectedFileFullPath = defaultfilename;

        public static ArrayList usaDTOs;
        public UserListForm()
        {
            InitializeComponent();
#if DEBUG
            selectedFileFullPath = "../../" + defaultfilename;
#endif
        }

        //２回目以降フォームを表示するさいは以前選択したファイル名を指定する
        public UserListForm(String SelectedFilename)
        {
            defaultfilename = SelectedFilename; //FullPAth


            InitializeComponent();
        }

        private void UserListForm_Load(object sender, EventArgs e)
        {

            listGUIAddNameList(UserXmlLoad(defaultfilename));

        }

        private void btSaveUserFile_Click(object sender, EventArgs e)
        {
            String kakutyoushi = null;
            String name = null;
            String start = null;
            String end = null;
            String type = "0";

            //入力チェック
            if (txtKakutyousi.Text.Length == 0)
            {
                MessageBox.Show("拡張子が入力されていません", "エラー",
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            if (txtName.Text.Length == 0)
            {
                MessageBox.Show("ユーザー設定名が入力されていません", "エラー",
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            if (txtStart.Text.Length == 0)
            {
                MessageBox.Show("STARTヘッダが入力されていません", "エラー",
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }


            kakutyoushi = txtKakutyousi.Text;
            name = txtName.Text;
            start = txtStart.Text;
            end = txtEnd.Text;

            // 同じ設定名だった場合は警告をだす
            if (true == isExistNameCheack(name))
            {
                MessageBox.Show("ユーザー設定名がすでにあります、別の名前にしてください。", "重複エラー",
      MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // http://msdn.microsoft.com/ja-jp/academic/cc987569.aspx

            XmlDocument doc = new XmlDocument();　//インスタンスの生成
            doc.Load(selectedFileFullPath);　//XML文書の読み込み
            XmlNode root = doc.DocumentElement;　//ルートノードの参照

            XmlElement elem = doc.CreateElement("attribute");　//要素ノードの作成
            root.PrependChild(elem);　//ルートノードに要素ノードを付加

            elem = doc.CreateElement("type");
            root.FirstChild.PrependChild(elem);
            XmlCharacterData text = doc.CreateTextNode(type);
            root.FirstChild.FirstChild.PrependChild(text);

            if (end.Length > 0)
            {
                elem = doc.CreateElement("end");
                root.FirstChild.PrependChild(elem);
                text = doc.CreateTextNode(end);
                root.FirstChild.FirstChild.PrependChild(text);
            }


            elem = doc.CreateElement("start");
            root.FirstChild.PrependChild(elem);
            text = doc.CreateTextNode(start);
            root.FirstChild.FirstChild.PrependChild(text);


            elem = doc.CreateElement("kakutyoushi");
            root.FirstChild.PrependChild(elem);
            text = doc.CreateTextNode(kakutyoushi);
            root.FirstChild.FirstChild.PrependChild(text);


            elem = doc.CreateElement("name");　//要素ノードの作成
            root.FirstChild.PrependChild(elem);　//ルートノードに要素ノードを付加
            text = doc.CreateTextNode(name); //テキストノードの作成
            root.FirstChild.FirstChild.PrependChild(text);//作成した要素ノードにテキストノードを付加

            doc.Save(selectedFileFullPath);　//XMLファイルの保存

            MessageBox.Show("項目を追加して保存しました。", "保存確認",
                  MessageBoxButtons.OK, MessageBoxIcon.Information);


            //リロード
            listGUIAddNameList(UserXmlLoad(selectedFileFullPath));

            // 読み込んだ別ファイルをセーブするのも想定
        }

        private void btUserFileOpen_Click(object sender, EventArgs e)
        {
            String selectFileName = null;
            //ファイルダイアログ
            openFileDialog1.FileName = null;
            openFileDialog1.Filter = "xml files (*.xml)|*.xml|All files (*.*)|*.*";


            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                selectedFileFullPath = openFileDialog1.FileName;
                selectFileName = Path.GetFileName(selectedFileFullPath);
            }

            //指定ファイルを再ロード
            listGUIAddNameList(UserXmlLoad(selectedFileFullPath));
            lbUserFile.Text = selectFileName;
        }


        // 別クラスが使うときに返却値を使用
        public static ArrayList UserXmlLoad(String filename)
        {
            // デフォルトXMLのロード
#if DEBUG
            filename = "../../defaultUser.xml";

#endif

            XmlTextReader reader = new XmlTextReader(filename);
            String tag = null;
            String nodeValue = null;

            usaDTOs = new ArrayList();
            int dtoCounter = 0;
            while (reader.Read())
            {
                if (reader.NodeType == XmlNodeType.Element)
                {
                    tag = reader.Name;

                    //新たなDTOを作成
                    if (tag == "name")
                    {
                        //UserSetAttDTOを可変生成
                        usaDTOs.Add(new UserSetAttDTO());
                        reader.Read();
                        nodeValue = reader.Value;
                        ((UserSetAttDTO)usaDTOs[dtoCounter]).name = nodeValue;
                        ((UserSetAttDTO)usaDTOs[dtoCounter]).EndHead = ""; // ENDだけはタグなしを許容してるので

                    }


                    if (tag == "kakutyoushi")
                    {
                        reader.Read();
                        nodeValue = reader.Value;
                        ((UserSetAttDTO)usaDTOs[dtoCounter]).kakutyoushi = nodeValue;
                    }

                    if (tag == "start")
                    {
                        reader.Read();
                        nodeValue = reader.Value;
                        ((UserSetAttDTO)usaDTOs[dtoCounter]).StartHead = nodeValue;
                    }

                    if (tag == "end")
                    {
                        reader.Read();
                        nodeValue = reader.Value;
                        ((UserSetAttDTO)usaDTOs[dtoCounter]).EndHead = nodeValue;
                    }


                    if (tag == "type")
                    {
                        reader.Read();
                        nodeValue = reader.Value;
                        ((UserSetAttDTO)usaDTOs[dtoCounter]).AnalyzType = Convert.ToInt32(nodeValue);

                        // DTO配列カウンタをインクリメント
                        dtoCounter++;
                    }
                }
            }


            reader.Close();
            return usaDTOs;
        }

        private void lbUserList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lbUserList.SelectedIndex > -1)
            {
                //ListBoxのインデックスからＤＴＯに変換
                txtName.Text = ((UserSetAttDTO)usaDTOs[lbUserList.SelectedIndex]).name;
                txtStart.Text = ((UserSetAttDTO)usaDTOs[lbUserList.SelectedIndex]).StartHead;
                txtEnd.Text = ((UserSetAttDTO)usaDTOs[lbUserList.SelectedIndex]).EndHead;
                txtKakutyousi.Text = ((UserSetAttDTO)usaDTOs[lbUserList.SelectedIndex]).kakutyoushi;
            }

        }

        public static String[] UserSetAttDTONameList(ArrayList usaList)
        {
            String[] stringAr = new String[usaList.Count];


            for (int i = 0; i < usaList.Count; i++)
            {
                stringAr[i] = ((UserSetAttDTO)usaList[i]).name;
            }

            return stringAr;
        }

        private void listGUIAddNameList(ArrayList usaList)
        {
            lbUserList.Items.Clear();
            String[] stringAr = new String[usaList.Count];


            for (int i = 0; i < usaList.Count; i++)
            {

                lbUserList.Items.Add(((UserSetAttDTO)usaList[i]).name);

            }
        }

        // すでにユーザーリストに抽出拡張子名があるか調べます　重複してたらtrue
        private bool isExistNameCheack(String name)
        {
            String[] star = UserSetAttDTONameList(usaDTOs);

            foreach (String registerName in star)
            {
                if (registerName == name)
                {
                    return true;
                }
            }

            return false;
        }

    }

    // XMLをオブジェクトに変換します(DTO)
    // FAS(FileAttributeSet)と互換性をもたないといけません 
    // TODO （どちらかが継承するべき）
    public class UserSetAttDTO
    {

        public String name = null;
        public String StartHead = null;
        public String EndHead = null;
        public int AnalyzType;

        //FASより移植
        public string kakutyoushi;

        public byte[] StartHeaderBytes
        {
            get
            {
                if (StartHead.Length == 0) { return null; }
                return ConvertStringToByteArray(StartHead);
            }
        }

        public byte[] EndHeaderBytes
        {
            get
            {
                if (EndHead.Length == 0) { return null; }
                return ConvertStringToByteArray(EndHead);
            }
        }

        // 16進法文字列をバイト列に変換
        // 16進法表記の文字列を引数に取ります
        public static byte[] ConvertStringToByteArray(String x16String)
        {
            // http://www.atmarkit.co.jp/fdotnet/dotnettips/057convhex/convhex.html

            byte[] byteArray = new byte[x16String.Length / 2];
            int x16;
            int AttCounter = 0;
            for (int i = 0; i < x16String.Length; i += 2)
            {
                String s2 = x16String.Substring(i, 2);
                x16 = Convert.ToInt32(s2, 16);
                byteArray[AttCounter] = Convert.ToByte(x16);
                AttCounter++;
            }

            return byteArray;
        }

        public UserSetAttDTO()
        {
            //デフォルト値
            AnalyzType = 0;
        }

        //UserDTO(XML 単純DTO)をFAS(C# コード処理クラス)に変換します
        public static FileAttributeSet UserSetAttDTOtoFAS(UserSetAttDTO dto)
        {
            FileAttributeSet fas = new FileAttributeSet();
            fas.AnalysisFormatTYPE = dto.AnalyzType;
            fas.AttHeader = dto.StartHeaderBytes;
            fas.EndHeader = dto.EndHeaderBytes;
            fas.kakutyoushi = dto.kakutyoushi;

            //エラー解除
            fas.ERR = false;
            return fas;
        }

    }

    // 単純検索 TYPE0 のみPerlコードの出力をサポート perl版との同一性を保持するため
    public class GenPerl
    {
    }
}