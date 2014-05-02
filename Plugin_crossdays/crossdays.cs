using System;
using System.Collections.Generic;
using System.Text;
using DataCutter_PlugIn;
using System.IO;
using System.Windows.Forms;

namespace Plugin_crossdays
{
    public class crossdays : PluginInterface
    {
        //�v���O�C���̖��O
        public string Pluginame { get { return "�N���X�f�C�Y"; } }

        //�v���O�C������������g���q�ݒ薼�z��i1�v���O�C����������������̂����e�j
        public List<String> Attributenames { get { 
            
        List<String> KakutyoushiList = new List<String>();
        KakutyoushiList.Add(".png");
        KakutyoushiList.Add(".wmv");
        //KakutyoushiList.Add(".ogg");
            return KakutyoushiList ;} }

        // �v���O�C���ŗL�̃I�v�V�����_�C�A���O�����邩�Ȃ�����\���܂�
        public bool OptionDialog { get { return false; } }

        // �v���O�C���ŗL�̐ݒ�t�@�C����
        public string Settingname { get { return null; } }

        // ����������_cutter������String�ŕԂ��܂�
        public List<String> DisposeArgument { get { return null; } }

        // �v���O�C���̃o�[�W����
        public String Version { get { return "1.0" ; } }

        // �v���O�C���̒��쌠
        public String Copyright { get { return "��11"; } }




       // PLUGIN TYPE <Normal> ���������t�@�C������Ԃ��܂�
        public int _cutter(
             bool Exe,
             bool IndexFileCreate,
             int buffer_Level,
             int RenbanKeta,
             string filename,
             string kakutyoushi,
             string CreateFileBaseName,
             string BaseDirectory,
             TextBox DebugTB
             )
        {


            switch (kakutyoushi)
            {
                case ".png":
                    return png_cutter(Exe, IndexFileCreate, buffer_Level, RenbanKeta, filename, kakutyoushi, CreateFileBaseName,
                        BaseDirectory, DebugTB);
                case ".wmv":
                    return wmv_cutter(Exe, IndexFileCreate, buffer_Level, RenbanKeta, filename, kakutyoushi, CreateFileBaseName,
                        BaseDirectory, DebugTB);
                case ".ogg":
                    return ogg_cutter(Exe, IndexFileCreate, buffer_Level, RenbanKeta, filename, kakutyoushi, CreateFileBaseName,
                        BaseDirectory, DebugTB);
            }
            return 0;
        }
        
        
        // PNG����
        public int png_cutter(
             bool Exe,
             bool IndexFileCreate,
             int buffer_Level,
             int RenbanKeta,
             string filename,
             string kakutyoushi,
             string CreateFileBaseName,
             string BaseDirectory,
             TextBox DebugTB
             )
        {
            byte[] DeletedByte = { 0x89, 0x50, 0x4e, 0x47, 0x0d };// �Ӑ}�I�ɍ폜���ꂽPNG�w�b�_ [5�o�C�g]

            byte[] StartByte ={ 0x0a, 0x1a, 0x0a, 0x00, 0x00, 0x00, 0x0d, 0x49 };//�Ƃ肠����8�o�C�g�����ƍׂ����͂ł���
            byte[] EndByte ={0x00,0x00,0x00,0x00,
							   0x49,0x45,0x4e,0x44,//IEND
							   0xae,0x42,0x60,0x82};////IEND-CRC

            //�f�B���N�g�������݂��邩���Ȃ��̃`�F�b�N�͊O���ł���
            if (BaseDirectory != null)
            {
                //	CreateFileBaseName=BaseDirectory+"\\"+CreateFileBaseName;
                CreateFileBaseName = BaseDirectory + CreateFileBaseName;//GUI����+\\���Ƃ�
            }

            //�A�ԃt�@�C��string�\�z
            string stRebnan = Convert.ToString(RenbanKeta);
            stRebnan = "d" + stRebnan;

            StreamWriter SWidx = null;
            string stidxs;
            if (IndexFileCreate == true)
            {
                //�C���f�b�N�X�t�@�C������filename+"�g���q"+".idx"
                FileStream FSidx = new FileStream(filename + "_" + kakutyoushi + ".idx",
                    FileMode.Create, FileAccess.Write);
                SWidx = new StreamWriter(FSidx);
                SWidx.WriteLine("FileNo,BlockNo,KITEN,Filesize");
            }
            int SBIdx = 0;
            int ReturnInt = 0;
            int COUNT = buffer_Level;//4096=�f�t�H���g

            FileStream sr = new FileStream(filename, FileMode.Open, FileAccess.Read);
            BinaryReader br = new BinaryReader(sr);
            byte[] buffer = new byte[COUNT];
            bool flag, WriteOK = true;
            int ix, BlockNO = 1, KITEN, beforeKITEN = 0, cFiles, FileNO = 0, ESBIdx = 0;
            string CuttingFileName = null;
            FileStream CFN = null;
            BinaryWriter CFNBR = null;

            //�����t�@�C�����́@filename+"cut"+FileNO+".�g���q"
            if (Exe == true)
            {
                CuttingFileName = String.Format("{0}{1:" + stRebnan + "}{2}", CreateFileBaseName, FileNO, kakutyoushi);
                CFN = new FileStream(CuttingFileName, FileMode.Create, FileAccess.Write);
                CFNBR = new BinaryWriter(CFN);
            }


   
                /*������ƍ��A���S���Y����kuth/Boyer�g�����ق�������*/
                for (int idx = 0; idx < sr.Length; idx += COUNT)
                {
                    br.Read(buffer, 0, COUNT);

                    //---------- �o�b�t�@Block�T�[�`�������[�`��
                    //�u���b�N�̖����ɃT�[�`�Ώۂ�����ꍇ�����[�`�����K�v
                    BlockNO++;

                    ix = 0;

                    while (ix < COUNT)
                    {
                        flag = false;
                        //true�ɂȂ�܂ŏ������݁{����
                        while (flag == false)
                        {
                            //write�o�b�t�@�͂��߂����8096�P�ʂŏ�������
                            //���̂܂܂��Ɗ����t�@�C���̖����ɃS�~�w�b�_����
                            if (Exe == true && WriteOK == true) { CFNBR.Write(buffer[ix]); }
                            if (buffer[ix] == StartByte[SBIdx])
                            {
                                SBIdx++;
                                if (SBIdx == StartByte.Length)
                                {
                                    flag = true;
                                    SBIdx = 0;

                                }
                            }
                            else
                            {
                                SBIdx = 0;
                            }

                            if (EndByte != null && buffer[ix] == EndByte[ESBIdx])
                            {
                                ESBIdx++;
                                if (ESBIdx == EndByte.Length)
                                {
                                    WriteOK = false;
                                    ESBIdx = 0;
                                }
                            }
                            else { ESBIdx = 0; }
                            ix++;
                            if (ix >= COUNT) { break; }
                        }
                        if (flag == true)
                        {
                            ReturnInt++;
                            KITEN = idx + ix - StartByte.Length + 1;//+1�͔z��[0]�̂���
                            cFiles = KITEN - beforeKITEN;//EndHeder�̎��͕ʏ���
                            beforeKITEN = KITEN;
                            if (IndexFileCreate == true)
                            {
                                stidxs = String.Format("{0},{1},{2},{3}\n", ReturnInt, BlockNO, KITEN, cFiles);
                                SWidx.Write(stidxs);
                            }
                            if (DebugTB != null)
                            {
                                //stidxs=String.Format("{0}    {1}    {2}\r\n",ReturnInt,BlockNO,KITEN);
                                //�Ȃ�textbox�G���[�ł�̂�
                                stidxs = String.Format("{0}\r\n", ReturnInt);
                                DebugTB.AppendText(stidxs);
                            }
                            if (Exe == true)
                            {
                                //	CFNBR.Write(buffer);
                                CFNBR.Flush();
                                CFNBR.Close();
                                CFN.Close();
                                FileNO++;
                              
                                //1�u���b�N�ɕ����t�@�C��������Ƃ��̃��[�`�����K�v[clear!]
                                CuttingFileName = String.Format("{0}{1:" + stRebnan + "}{2}", CreateFileBaseName, FileNO, kakutyoushi);
                                CFN = new FileStream(CuttingFileName, FileMode.Create, FileAccess.Write);
                                CFNBR = new BinaryWriter(CFN);
                                
                                //����-�v���O�C���̏C���_[�����ȊO�͊�{Datecutter�Ɠ���]
                                CFNBR.Write(DeletedByte); // �폜���ꂽ�o�C�g��̏�������
                                //����

                                CFNBR.Write(StartByte); // �w�b�_�̏�������
                                
                                WriteOK = true;
                                //	CFNBR.Write(buffer,ix-SearchBytes.Length,buffer.Length-(ix-SearchBytes.Length));

                            }

                        }
                        //	SBIdx=0;//�폜����Ή��i�u���b�N�����ɂȂ�[clear!]
                    }
                    //	if(Exe == true && FirstWrited == false){CFNBR.Write(buffer);}
                }

                if (Exe == true)
                {
                    CFNBR.Flush(); CFNBR.Close();
                    //00file���폜(�b��)
                    File.Delete(String.Format("{0}{1:" + stRebnan + "}{2}", CreateFileBaseName, 0, kakutyoushi));
                }
                br.Close();
                sr.Close();

                if (IndexFileCreate == true) { SWidx.Flush(); SWidx.Close(); }
                return ReturnInt;
        }



        // WMV����
        public int wmv_cutter(
             bool Exe,
             bool IndexFileCreate,
             int buffer_Level,
             int RenbanKeta,
             string filename,
             string kakutyoushi,
             string CreateFileBaseName,
             string BaseDirectory,
             TextBox DebugTB
             )
        {
            byte[] DeletedByte = { 0x30, 0x26, 0xB2, 0x75, 0x8E, 0x66, 0xCF,0x11 };

            byte[] StartByte ={ 0xA6,0xD9,0x00,0xAA,0x00,0x62,0xCE,0x6C,  0xDD , 0x13};
            //���C���e���W�F���X�ȋ�؂�ł͂Ȃ��i0xDD , 0x13�j�̓I�u�W�F�N�g�T�C�Y�Ȃ̂ł��ׂĂ�����ł���K�v������
            // WMV����؂�Ȃ�File Properties Object [8CABDCA1-A947-11CF-8EE4-00C00C205365] �o�C�g��� - �P�ʂŋt�ǂ�
            // [8CABDCA1-A947-11CF-8EE4-00C00C205365] �� A1DCAB9C-47A9�`
            // ����t�@�C���T�C�Y������o���̂����m�ȏo����

            byte[] EndByte = null;

            //�f�B���N�g�������݂��邩���Ȃ��̃`�F�b�N�͊O���ł���
            if (BaseDirectory != null)
            {
                //	CreateFileBaseName=BaseDirectory+"\\"+CreateFileBaseName;
                CreateFileBaseName = BaseDirectory + CreateFileBaseName;//GUI����+\\���Ƃ�
            }

            //�A�ԃt�@�C��string�\�z
            string stRebnan = Convert.ToString(RenbanKeta);
            stRebnan = "d" + stRebnan;

            StreamWriter SWidx = null;
            string stidxs;
            if (IndexFileCreate == true)
            {
                //�C���f�b�N�X�t�@�C������filename+"�g���q"+".idx"
                FileStream FSidx = new FileStream(filename + "_" + kakutyoushi + ".idx",
                    FileMode.Create, FileAccess.Write);
                SWidx = new StreamWriter(FSidx);
                SWidx.WriteLine("FileNo,BlockNo,KITEN,Filesize");
            }
            int SBIdx = 0;
            int ReturnInt = 0;
            int COUNT = buffer_Level;//4096=�f�t�H���g

            FileStream sr = new FileStream(filename, FileMode.Open, FileAccess.Read);
            BinaryReader br = new BinaryReader(sr);
            byte[] buffer = new byte[COUNT];
            bool flag, WriteOK = true;
            int ix, BlockNO = 1, KITEN, beforeKITEN = 0, cFiles, FileNO = 0, ESBIdx = 0;
            string CuttingFileName = null;
            FileStream CFN = null;
            BinaryWriter CFNBR = null;

            //�����t�@�C�����́@filename+"cut"+FileNO+".�g���q"
            if (Exe == true)
            {
                CuttingFileName = String.Format("{0}{1:" + stRebnan + "}{2}", CreateFileBaseName, FileNO, kakutyoushi);
                CFN = new FileStream(CuttingFileName, FileMode.Create, FileAccess.Write);
                CFNBR = new BinaryWriter(CFN);
            }



            /*������ƍ��A���S���Y����kuth/Boyer�g�����ق�������*/
            for (int idx = 0; idx < sr.Length; idx += COUNT)
            {
                br.Read(buffer, 0, COUNT);

                //---------- �o�b�t�@Block�T�[�`�������[�`��
                //�u���b�N�̖����ɃT�[�`�Ώۂ�����ꍇ�����[�`�����K�v
                BlockNO++;

                ix = 0;

                while (ix < COUNT)
                {
                    flag = false;
                    //true�ɂȂ�܂ŏ������݁{����
                    while (flag == false)
                    {
                        //write�o�b�t�@�͂��߂����8096�P�ʂŏ�������
                        //���̂܂܂��Ɗ����t�@�C���̖����ɃS�~�w�b�_����
                        if (Exe == true && WriteOK == true) { CFNBR.Write(buffer[ix]); }
                        if (buffer[ix] == StartByte[SBIdx])
                        {
                            SBIdx++;
                            if (SBIdx == StartByte.Length)
                            {
                                flag = true;
                                SBIdx = 0;

                            }
                        }
                        else
                        {
                            SBIdx = 0;
                        }

                        if (EndByte != null && buffer[ix] == EndByte[ESBIdx])
                        {
                            ESBIdx++;
                            if (ESBIdx == EndByte.Length)
                            {
                                WriteOK = false;
                                ESBIdx = 0;
                            }
                        }
                        else { ESBIdx = 0; }
                        ix++;
                        if (ix >= COUNT) { break; }
                    }
                    if (flag == true)
                    {
                        ReturnInt++;
                        KITEN = idx + ix - StartByte.Length + 1;//+1�͔z��[0]�̂���
                        cFiles = KITEN - beforeKITEN;//EndHeder�̎��͕ʏ���
                        beforeKITEN = KITEN;
                        if (IndexFileCreate == true)
                        {
                            stidxs = String.Format("{0},{1},{2},{3}\n", ReturnInt, BlockNO, KITEN, cFiles);
                            SWidx.Write(stidxs);
                        }
                        if (DebugTB != null)
                        {
                            //stidxs=String.Format("{0}    {1}    {2}\r\n",ReturnInt,BlockNO,KITEN);
                            //�Ȃ�textbox�G���[�ł�̂�
                            stidxs = String.Format("{0}\r\n", ReturnInt);
                            DebugTB.AppendText(stidxs);
                        }
                        if (Exe == true)
                        {
                            //	CFNBR.Write(buffer);
                            CFNBR.Flush();
                            CFNBR.Close();
                            CFN.Close();
                            FileNO++;

                            //1�u���b�N�ɕ����t�@�C��������Ƃ��̃��[�`�����K�v[clear!]
                            CuttingFileName = String.Format("{0}{1:" + stRebnan + "}{2}", CreateFileBaseName, FileNO, kakutyoushi);
                            CFN = new FileStream(CuttingFileName, FileMode.Create, FileAccess.Write);
                            CFNBR = new BinaryWriter(CFN);

                            //����-�v���O�C���̏C���_[�����ȊO�͊�{Datecutter�Ɠ���]
                            CFNBR.Write(DeletedByte); // �폜���ꂽ�o�C�g��̏�������
                            //����

                            CFNBR.Write(StartByte); // �w�b�_�̏�������

                            WriteOK = true;
                            //	CFNBR.Write(buffer,ix-SearchBytes.Length,buffer.Length-(ix-SearchBytes.Length));

                        }

                    }
                    //	SBIdx=0;//�폜����Ή��i�u���b�N�����ɂȂ�[clear!]
                }
                //	if(Exe == true && FirstWrited == false){CFNBR.Write(buffer);}
            }

            if (Exe == true)
            {
                CFNBR.Flush(); CFNBR.Close();
                //00file���폜(�b��)
                File.Delete(String.Format("{0}{1:" + stRebnan + "}{2}", CreateFileBaseName, 0, kakutyoushi));
            }
            br.Close();
            sr.Close();

            if (IndexFileCreate == true) { SWidx.Flush(); SWidx.Close(); }
            return ReturnInt;
        }

        // OGG����
        // TODO ���r��
        public int ogg_cutter(
             bool Exe,
             bool IndexFileCreate,
             int buffer_Level,
             int RenbanKeta,
             string filename,
             string kakutyoushi,
             string CreateFileBaseName,
             string BaseDirectory,
             TextBox DebugTB
             )
        {
            //byte[] DeletedByte = { 0x30, 0x26, 0xB2, 0x75, 0x8E, 0x66, 0xCF, 0x11 };

            byte[] StartByte ={ 0x4f, 0x67, 0x67, 0x53, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00};


            byte[] EndByte = null;

            //�f�B���N�g�������݂��邩���Ȃ��̃`�F�b�N�͊O���ł���
            if (BaseDirectory != null)
            {
                //	CreateFileBaseName=BaseDirectory+"\\"+CreateFileBaseName;
                CreateFileBaseName = BaseDirectory + CreateFileBaseName;//GUI����+\\���Ƃ�
            }

            //�A�ԃt�@�C��string�\�z
            string stRebnan = Convert.ToString(RenbanKeta);
            stRebnan = "d" + stRebnan;

            StreamWriter SWidx = null;
            string stidxs;
            if (IndexFileCreate == true)
            {
                //�C���f�b�N�X�t�@�C������filename+"�g���q"+".idx"
                FileStream FSidx = new FileStream(filename + "_" + kakutyoushi + ".idx",
                    FileMode.Create, FileAccess.Write);
                SWidx = new StreamWriter(FSidx);
                SWidx.WriteLine("FileNo,BlockNo,KITEN,Filesize");
            }
            int SBIdx = 0;
            int ReturnInt = 0;
            int COUNT = buffer_Level;//4096=�f�t�H���g

            FileStream sr = new FileStream(filename, FileMode.Open, FileAccess.Read);
            BinaryReader br = new BinaryReader(sr);
            byte[] buffer = new byte[COUNT];
            bool flag, WriteOK = true;
            int ix, BlockNO = 1, KITEN, beforeKITEN = 0, cFiles, FileNO = 0, ESBIdx = 0;
            string CuttingFileName = null;
            FileStream CFN = null;
            BinaryWriter CFNBR = null;

            //�����t�@�C�����́@filename+"cut"+FileNO+".�g���q"
            if (Exe == true)
            {
                CuttingFileName = String.Format("{0}{1:" + stRebnan + "}{2}", CreateFileBaseName, FileNO, kakutyoushi);
                CFN = new FileStream(CuttingFileName, FileMode.Create, FileAccess.Write);
                CFNBR = new BinaryWriter(CFN);
            }



            /*������ƍ��A���S���Y����kuth/Boyer�g�����ق�������*/
            for (int idx = 0; idx < sr.Length; idx += COUNT)
            {
                br.Read(buffer, 0, COUNT);

                //---------- �o�b�t�@Block�T�[�`�������[�`��
                //�u���b�N�̖����ɃT�[�`�Ώۂ�����ꍇ�����[�`�����K�v
                BlockNO++;

                ix = 0;

                while (ix < COUNT)
                {
                    flag = false;
                    //true�ɂȂ�܂ŏ������݁{����
                    while (flag == false)
                    {
                        //write�o�b�t�@�͂��߂����8096�P�ʂŏ�������
                        //���̂܂܂��Ɗ����t�@�C���̖����ɃS�~�w�b�_����
                        if (Exe == true && WriteOK == true) { CFNBR.Write(buffer[ix]); }
                        if (buffer[ix] == StartByte[SBIdx])
                        {
                            SBIdx++;
                            if (SBIdx == StartByte.Length)
                            {
                                flag = true;
                                SBIdx = 0;

                            }
                        }
                        else
                        {
                            SBIdx = 0;
                        }

                        if (EndByte != null && buffer[ix] == EndByte[ESBIdx])
                        {
                            ESBIdx++;
                            if (ESBIdx == EndByte.Length)
                            {
                                WriteOK = false;
                                ESBIdx = 0;
                            }
                        }
                        else { ESBIdx = 0; }
                        ix++;
                        if (ix >= COUNT) { break; }
                    }
                    if (flag == true)
                    {
                        ReturnInt++;
                        KITEN = idx + ix - StartByte.Length + 1;//+1�͔z��[0]�̂���
                        cFiles = KITEN - beforeKITEN;//EndHeder�̎��͕ʏ���
                        beforeKITEN = KITEN;
                        if (IndexFileCreate == true)
                        {
                            stidxs = String.Format("{0},{1},{2},{3}\n", ReturnInt, BlockNO, KITEN, cFiles);
                            SWidx.Write(stidxs);
                        }
                        if (DebugTB != null)
                        {
                            //stidxs=String.Format("{0}    {1}    {2}\r\n",ReturnInt,BlockNO,KITEN);
                            //�Ȃ�textbox�G���[�ł�̂�
                            stidxs = String.Format("{0}\r\n", ReturnInt);
                            DebugTB.AppendText(stidxs);
                        }
                        if (Exe == true)
                        {
                            //	CFNBR.Write(buffer);
                            CFNBR.Flush();
                            CFNBR.Close();
                            CFN.Close();
                            FileNO++;

                            //1�u���b�N�ɕ����t�@�C��������Ƃ��̃��[�`�����K�v[clear!]
                            CuttingFileName = String.Format("{0}{1:" + stRebnan + "}{2}", CreateFileBaseName, FileNO, kakutyoushi);
                            CFN = new FileStream(CuttingFileName, FileMode.Create, FileAccess.Write);
                            CFNBR = new BinaryWriter(CFN);

                            //����-�v���O�C���̏C���_[�����ȊO�͊�{Datecutter�Ɠ���]
                           // CFNBR.Write(DeletedByte); // �폜���ꂽ�o�C�g��̏�������
                            //����

                            CFNBR.Write(StartByte); // �w�b�_�̏�������

                            WriteOK = true;
                            //	CFNBR.Write(buffer,ix-SearchBytes.Length,buffer.Length-(ix-SearchBytes.Length));

                        }

                    }
                    //	SBIdx=0;//�폜����Ή��i�u���b�N�����ɂȂ�[clear!]
                }
                //	if(Exe == true && FirstWrited == false){CFNBR.Write(buffer);}
            }

            if (Exe == true)
            {
                CFNBR.Flush(); CFNBR.Close();
                //00file���폜(�b��)
                File.Delete(String.Format("{0}{1:" + stRebnan + "}{2}", CreateFileBaseName, 0, kakutyoushi));
            }
            br.Close();
            sr.Close();

            if (IndexFileCreate == true) { SWidx.Flush(); SWidx.Close(); }
            return ReturnInt;
        }


        // �ݒ�_�C�A���O�͂Ȃ�
        public void showOptionDialog() { }
    }


}
