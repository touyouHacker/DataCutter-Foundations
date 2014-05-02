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
        //プラグインの名前
        public string Pluginame { get { return "クロスデイズ"; } }

        //プラグインが処理する拡張子設定名配列（1プラグインが複数処理するのを許容）
        public List<String> Attributenames { get { 
            
        List<String> KakutyoushiList = new List<String>();
        KakutyoushiList.Add(".png");
        KakutyoushiList.Add(".wmv");
        //KakutyoushiList.Add(".ogg");
            return KakutyoushiList ;} }

        // プラグイン固有のオプションダイアログがあるかないかを表します
        public bool OptionDialog { get { return false; } }

        // プラグイン固有の設定ファイル名
        public string Settingname { get { return null; } }

        // 無効化する_cutter引数をStringで返します
        public List<String> DisposeArgument { get { return null; } }

        // プラグインのバージョン
        public String Version { get { return "1.0" ; } }

        // プラグインの著作権
        public String Copyright { get { return "ω11"; } }




       // PLUGIN TYPE <Normal> 処理したファイル数を返します
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
        
        
        // PNG処理
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
            byte[] DeletedByte = { 0x89, 0x50, 0x4e, 0x47, 0x0d };// 意図的に削除されたPNGヘッダ [5バイト]

            byte[] StartByte ={ 0x0a, 0x1a, 0x0a, 0x00, 0x00, 0x00, 0x0d, 0x49 };//とりあえず8バイトもっと細かくはできる
            byte[] EndByte ={0x00,0x00,0x00,0x00,
							   0x49,0x45,0x4e,0x44,//IEND
							   0xae,0x42,0x60,0x82};////IEND-CRC

            //ディレクトリが存在するかしなかのチェックは外部でする
            if (BaseDirectory != null)
            {
                //	CreateFileBaseName=BaseDirectory+"\\"+CreateFileBaseName;
                CreateFileBaseName = BaseDirectory + CreateFileBaseName;//GUI側で+\\しとく
            }

            //連番ファイルstring構築
            string stRebnan = Convert.ToString(RenbanKeta);
            stRebnan = "d" + stRebnan;

            StreamWriter SWidx = null;
            string stidxs;
            if (IndexFileCreate == true)
            {
                //インデックスファイル名はfilename+"拡張子"+".idx"
                FileStream FSidx = new FileStream(filename + "_" + kakutyoushi + ".idx",
                    FileMode.Create, FileAccess.Write);
                SWidx = new StreamWriter(FSidx);
                SWidx.WriteLine("FileNo,BlockNo,KITEN,Filesize");
            }
            int SBIdx = 0;
            int ReturnInt = 0;
            int COUNT = buffer_Level;//4096=デフォルト

            FileStream sr = new FileStream(filename, FileMode.Open, FileAccess.Read);
            BinaryReader br = new BinaryReader(sr);
            byte[] buffer = new byte[COUNT];
            bool flag, WriteOK = true;
            int ix, BlockNO = 1, KITEN, beforeKITEN = 0, cFiles, FileNO = 0, ESBIdx = 0;
            string CuttingFileName = null;
            FileStream CFN = null;
            BinaryWriter CFNBR = null;

            //生成ファイル名は　filename+"cut"+FileNO+".拡張子"
            if (Exe == true)
            {
                CuttingFileName = String.Format("{0}{1:" + stRebnan + "}{2}", CreateFileBaseName, FileNO, kakutyoushi);
                CFN = new FileStream(CuttingFileName, FileMode.Create, FileAccess.Write);
                CFNBR = new BinaryWriter(CFN);
            }


   
                /*文字列照合アルゴリズムかkuth/Boyer使ったほうが速い*/
                for (int idx = 0; idx < sr.Length; idx += COUNT)
                {
                    br.Read(buffer, 0, COUNT);

                    //---------- バッファBlockサーチ処理ルーチン
                    //ブロックの末尾にサーチ対象がある場合もルーチンが必要
                    BlockNO++;

                    ix = 0;

                    while (ix < COUNT)
                    {
                        flag = false;
                        //trueになるまで書き込み＋調査
                        while (flag == false)
                        {
                            //writeバッファはためこんで8096単位で書き込み
                            //このままだと完成ファイルの末尾にゴミヘッダがつく
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
                            KITEN = idx + ix - StartByte.Length + 1;//+1は配列[0]のため
                            cFiles = KITEN - beforeKITEN;//EndHederの時は別処理
                            beforeKITEN = KITEN;
                            if (IndexFileCreate == true)
                            {
                                stidxs = String.Format("{0},{1},{2},{3}\n", ReturnInt, BlockNO, KITEN, cFiles);
                                SWidx.Write(stidxs);
                            }
                            if (DebugTB != null)
                            {
                                //stidxs=String.Format("{0}    {1}    {2}\r\n",ReturnInt,BlockNO,KITEN);
                                //なんかtextboxエラーでるので
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
                              
                                //1ブロックに複数ファイルがあるときのルーチンが必要[clear!]
                                CuttingFileName = String.Format("{0}{1:" + stRebnan + "}{2}", CreateFileBaseName, FileNO, kakutyoushi);
                                CFN = new FileStream(CuttingFileName, FileMode.Create, FileAccess.Write);
                                CFNBR = new BinaryWriter(CFN);
                                
                                //★★-プラグインの修正点[ここ以外は基本Datecutterと同じ]
                                CFNBR.Write(DeletedByte); // 削除されたバイト列の書き込み
                                //★★

                                CFNBR.Write(StartByte); // ヘッダの書き込み
                                
                                WriteOK = true;
                                //	CFNBR.Write(buffer,ix-SearchBytes.Length,buffer.Length-(ix-SearchBytes.Length));

                            }

                        }
                        //	SBIdx=0;//削除すれば下段ブロック処理になる[clear!]
                    }
                    //	if(Exe == true && FirstWrited == false){CFNBR.Write(buffer);}
                }

                if (Exe == true)
                {
                    CFNBR.Flush(); CFNBR.Close();
                    //00fileを削除(暫定)
                    File.Delete(String.Format("{0}{1:" + stRebnan + "}{2}", CreateFileBaseName, 0, kakutyoushi));
                }
                br.Close();
                sr.Close();

                if (IndexFileCreate == true) { SWidx.Flush(); SWidx.Close(); }
                return ReturnInt;
        }



        // WMV処理
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
            //↑インテリジェンスな区切りではない（0xDD , 0x13）はオブジェクトサイズなのですべてが同一である必要がある
            // WMVを区切るならFile Properties Object [8CABDCA1-A947-11CF-8EE4-00C00C205365] バイト列は - 単位で逆読み
            // [8CABDCA1-A947-11CF-8EE4-00C00C205365] → A1DCAB9C-47A9～
            // からファイルサイズを割り出すのが正確な出し方

            byte[] EndByte = null;

            //ディレクトリが存在するかしなかのチェックは外部でする
            if (BaseDirectory != null)
            {
                //	CreateFileBaseName=BaseDirectory+"\\"+CreateFileBaseName;
                CreateFileBaseName = BaseDirectory + CreateFileBaseName;//GUI側で+\\しとく
            }

            //連番ファイルstring構築
            string stRebnan = Convert.ToString(RenbanKeta);
            stRebnan = "d" + stRebnan;

            StreamWriter SWidx = null;
            string stidxs;
            if (IndexFileCreate == true)
            {
                //インデックスファイル名はfilename+"拡張子"+".idx"
                FileStream FSidx = new FileStream(filename + "_" + kakutyoushi + ".idx",
                    FileMode.Create, FileAccess.Write);
                SWidx = new StreamWriter(FSidx);
                SWidx.WriteLine("FileNo,BlockNo,KITEN,Filesize");
            }
            int SBIdx = 0;
            int ReturnInt = 0;
            int COUNT = buffer_Level;//4096=デフォルト

            FileStream sr = new FileStream(filename, FileMode.Open, FileAccess.Read);
            BinaryReader br = new BinaryReader(sr);
            byte[] buffer = new byte[COUNT];
            bool flag, WriteOK = true;
            int ix, BlockNO = 1, KITEN, beforeKITEN = 0, cFiles, FileNO = 0, ESBIdx = 0;
            string CuttingFileName = null;
            FileStream CFN = null;
            BinaryWriter CFNBR = null;

            //生成ファイル名は　filename+"cut"+FileNO+".拡張子"
            if (Exe == true)
            {
                CuttingFileName = String.Format("{0}{1:" + stRebnan + "}{2}", CreateFileBaseName, FileNO, kakutyoushi);
                CFN = new FileStream(CuttingFileName, FileMode.Create, FileAccess.Write);
                CFNBR = new BinaryWriter(CFN);
            }



            /*文字列照合アルゴリズムかkuth/Boyer使ったほうが速い*/
            for (int idx = 0; idx < sr.Length; idx += COUNT)
            {
                br.Read(buffer, 0, COUNT);

                //---------- バッファBlockサーチ処理ルーチン
                //ブロックの末尾にサーチ対象がある場合もルーチンが必要
                BlockNO++;

                ix = 0;

                while (ix < COUNT)
                {
                    flag = false;
                    //trueになるまで書き込み＋調査
                    while (flag == false)
                    {
                        //writeバッファはためこんで8096単位で書き込み
                        //このままだと完成ファイルの末尾にゴミヘッダがつく
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
                        KITEN = idx + ix - StartByte.Length + 1;//+1は配列[0]のため
                        cFiles = KITEN - beforeKITEN;//EndHederの時は別処理
                        beforeKITEN = KITEN;
                        if (IndexFileCreate == true)
                        {
                            stidxs = String.Format("{0},{1},{2},{3}\n", ReturnInt, BlockNO, KITEN, cFiles);
                            SWidx.Write(stidxs);
                        }
                        if (DebugTB != null)
                        {
                            //stidxs=String.Format("{0}    {1}    {2}\r\n",ReturnInt,BlockNO,KITEN);
                            //なんかtextboxエラーでるので
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

                            //1ブロックに複数ファイルがあるときのルーチンが必要[clear!]
                            CuttingFileName = String.Format("{0}{1:" + stRebnan + "}{2}", CreateFileBaseName, FileNO, kakutyoushi);
                            CFN = new FileStream(CuttingFileName, FileMode.Create, FileAccess.Write);
                            CFNBR = new BinaryWriter(CFN);

                            //★★-プラグインの修正点[ここ以外は基本Datecutterと同じ]
                            CFNBR.Write(DeletedByte); // 削除されたバイト列の書き込み
                            //★★

                            CFNBR.Write(StartByte); // ヘッダの書き込み

                            WriteOK = true;
                            //	CFNBR.Write(buffer,ix-SearchBytes.Length,buffer.Length-(ix-SearchBytes.Length));

                        }

                    }
                    //	SBIdx=0;//削除すれば下段ブロック処理になる[clear!]
                }
                //	if(Exe == true && FirstWrited == false){CFNBR.Write(buffer);}
            }

            if (Exe == true)
            {
                CFNBR.Flush(); CFNBR.Close();
                //00fileを削除(暫定)
                File.Delete(String.Format("{0}{1:" + stRebnan + "}{2}", CreateFileBaseName, 0, kakutyoushi));
            }
            br.Close();
            sr.Close();

            if (IndexFileCreate == true) { SWidx.Flush(); SWidx.Close(); }
            return ReturnInt;
        }

        // OGG処理
        // TODO 超途中
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

            //ディレクトリが存在するかしなかのチェックは外部でする
            if (BaseDirectory != null)
            {
                //	CreateFileBaseName=BaseDirectory+"\\"+CreateFileBaseName;
                CreateFileBaseName = BaseDirectory + CreateFileBaseName;//GUI側で+\\しとく
            }

            //連番ファイルstring構築
            string stRebnan = Convert.ToString(RenbanKeta);
            stRebnan = "d" + stRebnan;

            StreamWriter SWidx = null;
            string stidxs;
            if (IndexFileCreate == true)
            {
                //インデックスファイル名はfilename+"拡張子"+".idx"
                FileStream FSidx = new FileStream(filename + "_" + kakutyoushi + ".idx",
                    FileMode.Create, FileAccess.Write);
                SWidx = new StreamWriter(FSidx);
                SWidx.WriteLine("FileNo,BlockNo,KITEN,Filesize");
            }
            int SBIdx = 0;
            int ReturnInt = 0;
            int COUNT = buffer_Level;//4096=デフォルト

            FileStream sr = new FileStream(filename, FileMode.Open, FileAccess.Read);
            BinaryReader br = new BinaryReader(sr);
            byte[] buffer = new byte[COUNT];
            bool flag, WriteOK = true;
            int ix, BlockNO = 1, KITEN, beforeKITEN = 0, cFiles, FileNO = 0, ESBIdx = 0;
            string CuttingFileName = null;
            FileStream CFN = null;
            BinaryWriter CFNBR = null;

            //生成ファイル名は　filename+"cut"+FileNO+".拡張子"
            if (Exe == true)
            {
                CuttingFileName = String.Format("{0}{1:" + stRebnan + "}{2}", CreateFileBaseName, FileNO, kakutyoushi);
                CFN = new FileStream(CuttingFileName, FileMode.Create, FileAccess.Write);
                CFNBR = new BinaryWriter(CFN);
            }



            /*文字列照合アルゴリズムかkuth/Boyer使ったほうが速い*/
            for (int idx = 0; idx < sr.Length; idx += COUNT)
            {
                br.Read(buffer, 0, COUNT);

                //---------- バッファBlockサーチ処理ルーチン
                //ブロックの末尾にサーチ対象がある場合もルーチンが必要
                BlockNO++;

                ix = 0;

                while (ix < COUNT)
                {
                    flag = false;
                    //trueになるまで書き込み＋調査
                    while (flag == false)
                    {
                        //writeバッファはためこんで8096単位で書き込み
                        //このままだと完成ファイルの末尾にゴミヘッダがつく
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
                        KITEN = idx + ix - StartByte.Length + 1;//+1は配列[0]のため
                        cFiles = KITEN - beforeKITEN;//EndHederの時は別処理
                        beforeKITEN = KITEN;
                        if (IndexFileCreate == true)
                        {
                            stidxs = String.Format("{0},{1},{2},{3}\n", ReturnInt, BlockNO, KITEN, cFiles);
                            SWidx.Write(stidxs);
                        }
                        if (DebugTB != null)
                        {
                            //stidxs=String.Format("{0}    {1}    {2}\r\n",ReturnInt,BlockNO,KITEN);
                            //なんかtextboxエラーでるので
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

                            //1ブロックに複数ファイルがあるときのルーチンが必要[clear!]
                            CuttingFileName = String.Format("{0}{1:" + stRebnan + "}{2}", CreateFileBaseName, FileNO, kakutyoushi);
                            CFN = new FileStream(CuttingFileName, FileMode.Create, FileAccess.Write);
                            CFNBR = new BinaryWriter(CFN);

                            //★★-プラグインの修正点[ここ以外は基本Datecutterと同じ]
                           // CFNBR.Write(DeletedByte); // 削除されたバイト列の書き込み
                            //★★

                            CFNBR.Write(StartByte); // ヘッダの書き込み

                            WriteOK = true;
                            //	CFNBR.Write(buffer,ix-SearchBytes.Length,buffer.Length-(ix-SearchBytes.Length));

                        }

                    }
                    //	SBIdx=0;//削除すれば下段ブロック処理になる[clear!]
                }
                //	if(Exe == true && FirstWrited == false){CFNBR.Write(buffer);}
            }

            if (Exe == true)
            {
                CFNBR.Flush(); CFNBR.Close();
                //00fileを削除(暫定)
                File.Delete(String.Format("{0}{1:" + stRebnan + "}{2}", CreateFileBaseName, 0, kakutyoushi));
            }
            br.Close();
            sr.Close();

            if (IndexFileCreate == true) { SWidx.Flush(); SWidx.Close(); }
            return ReturnInt;
        }


        // 設定ダイアログはなし
        public void showOptionDialog() { }
    }


}
