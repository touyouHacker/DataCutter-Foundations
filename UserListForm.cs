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
 * ���[�U�[���ǉ��Ŋg���w��ł��邽�߂̏����ł� 
 * START�w�b�_END�w�b�_�w�肾���ł͒��g�̏����i�w�b�_��1�t�@�C���̃o�C�g�������ߍ��܂�Ă���Ƃ��j
 * dat�̒��̐擪�Ƀt�@�C���������߂��܂�Ă�Ƃ��E�E������Í���ǂ���Ƃ��E�E
 * ���ł��Ȃ��̂Ńv���O�C��DLL(Interface�p���^)�`���ɂ��邩�e���v���[�g�Ƃ���TYPE1,TYPE2�̂悤�Ɏw��ł���悤��
 * �����ق��������ł��傤�B
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

        //�Q��ڈȍ~�t�H�[����\�����邳���͈ȑO�I�������t�@�C�������w�肷��
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

            //���̓`�F�b�N
            if (txtKakutyousi.Text.Length == 0)
            {
                MessageBox.Show("�g���q�����͂���Ă��܂���", "�G���[",
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            if (txtName.Text.Length == 0)
            {
                MessageBox.Show("���[�U�[�ݒ薼�����͂���Ă��܂���", "�G���[",
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            if (txtStart.Text.Length == 0)
            {
                MessageBox.Show("START�w�b�_�����͂���Ă��܂���", "�G���[",
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }


            kakutyoushi = txtKakutyousi.Text;
            name = txtName.Text;
            start = txtStart.Text;
            end = txtEnd.Text;

            // �����ݒ薼�������ꍇ�͌x��������
            if (true == isExistNameCheack(name))
            {
                MessageBox.Show("���[�U�[�ݒ薼�����łɂ���܂��A�ʂ̖��O�ɂ��Ă��������B", "�d���G���[",
      MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // http://msdn.microsoft.com/ja-jp/academic/cc987569.aspx

            XmlDocument doc = new XmlDocument();�@//�C���X�^���X�̐���
            doc.Load(selectedFileFullPath);�@//XML�����̓ǂݍ���
            XmlNode root = doc.DocumentElement;�@//���[�g�m�[�h�̎Q��

            XmlElement elem = doc.CreateElement("attribute");�@//�v�f�m�[�h�̍쐬
            root.PrependChild(elem);�@//���[�g�m�[�h�ɗv�f�m�[�h��t��

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


            elem = doc.CreateElement("name");�@//�v�f�m�[�h�̍쐬
            root.FirstChild.PrependChild(elem);�@//���[�g�m�[�h�ɗv�f�m�[�h��t��
            text = doc.CreateTextNode(name); //�e�L�X�g�m�[�h�̍쐬
            root.FirstChild.FirstChild.PrependChild(text);//�쐬�����v�f�m�[�h�Ƀe�L�X�g�m�[�h��t��

            doc.Save(selectedFileFullPath);�@//XML�t�@�C���̕ۑ�

            MessageBox.Show("���ڂ�ǉ����ĕۑ����܂����B", "�ۑ��m�F",
                  MessageBoxButtons.OK, MessageBoxIcon.Information);


            //�����[�h
            listGUIAddNameList(UserXmlLoad(selectedFileFullPath));

            // �ǂݍ��񂾕ʃt�@�C�����Z�[�u����̂��z��
        }

        private void btUserFileOpen_Click(object sender, EventArgs e)
        {
            String selectFileName = null;
            //�t�@�C���_�C�A���O
            openFileDialog1.FileName = null;
            openFileDialog1.Filter = "xml files (*.xml)|*.xml|All files (*.*)|*.*";


            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                selectedFileFullPath = openFileDialog1.FileName;
                selectFileName = Path.GetFileName(selectedFileFullPath);
            }

            //�w��t�@�C�����ă��[�h
            listGUIAddNameList(UserXmlLoad(selectedFileFullPath));
            lbUserFile.Text = selectFileName;
        }


        // �ʃN���X���g���Ƃ��ɕԋp�l���g�p
        public static ArrayList UserXmlLoad(String filename)
        {
            // �f�t�H���gXML�̃��[�h
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

                    //�V����DTO���쐬
                    if (tag == "name")
                    {
                        //UserSetAttDTO���ϐ���
                        usaDTOs.Add(new UserSetAttDTO());
                        reader.Read();
                        nodeValue = reader.Value;
                        ((UserSetAttDTO)usaDTOs[dtoCounter]).name = nodeValue;
                        ((UserSetAttDTO)usaDTOs[dtoCounter]).EndHead = ""; // END�����̓^�O�Ȃ������e���Ă�̂�

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

                        // DTO�z��J�E���^���C���N�������g
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
                //ListBox�̃C���f�b�N�X����c�s�n�ɕϊ�
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

        // ���łɃ��[�U�[���X�g�ɒ��o�g���q�������邩���ׂ܂��@�d�����Ă���true
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

    // XML���I�u�W�F�N�g�ɕϊ����܂�(DTO)
    // FAS(FileAttributeSet)�ƌ݊����������Ȃ��Ƃ����܂��� 
    // TODO �i�ǂ��炩���p������ׂ��j
    public class UserSetAttDTO
    {

        public String name = null;
        public String StartHead = null;
        public String EndHead = null;
        public int AnalyzType;

        //FAS���ڐA
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

        // 16�i�@��������o�C�g��ɕϊ�
        // 16�i�@�\�L�̕�����������Ɏ��܂�
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
            //�f�t�H���g�l
            AnalyzType = 0;
        }

        //UserDTO(XML �P��DTO)��FAS(C# �R�[�h�����N���X)�ɕϊ����܂�
        public static FileAttributeSet UserSetAttDTOtoFAS(UserSetAttDTO dto)
        {
            FileAttributeSet fas = new FileAttributeSet();
            fas.AnalysisFormatTYPE = dto.AnalyzType;
            fas.AttHeader = dto.StartHeaderBytes;
            fas.EndHeader = dto.EndHeaderBytes;
            fas.kakutyoushi = dto.kakutyoushi;

            //�G���[����
            fas.ERR = false;
            return fas;
        }

    }

    // �P������ TYPE0 �̂�Perl�R�[�h�̏o�͂��T�|�[�g perl�łƂ̓��ꐫ��ێ����邽��
    public class GenPerl
    {
    }
}