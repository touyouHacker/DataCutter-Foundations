/*
  --------------------------------
   DataCutter
   Copyright (c) 2004 GreenShell
  --------------------------------
*/
//csc /t:library Datacutter.cs

//���X�R�}���h���C�����s�t�@�C���z��ō��ꂽ����

using System;
using System.IO;
using System.Windows.Forms;

public class DataCutter
{

  
	//bool Exe=true�Ŏ��s�Afalse�ŃV�~�����[�g�̂�
	// buffer_Level ��wmv�̂Ƃ���4096�łnk
	/*�e�g���q�ɑΉ��ł���悤�ėp������������
	  ���[�U�[���T�[�`�o�C�g���w��ł���悤�ɂ���
	  ArraySerch�Ƃ��ĕʂɔėp�֐��������ă��C�u����DLL�����Ƃ�
		 CFN������ �f�B���N�g���w��ł���悤�ɁA�f�B���N�g�����Ȃ��ꍇ�쐬
		  �����t�@�C���𐶐����邩�������ɂ���
		  �Ԃ�l=cut�ł���/cut�ł���t�@�C����
	*/
	public  int _cutter(
        string filename,
        FileAttributeSet FAS,
		int buffer_Level,
        bool Exe,
		bool IndexFileCreate,
		string CreateFileBaseName,
        string BaseDirectory, 
        int RenbanKeta, 
        TextBox DebugTB)
	{
		if( CreateFileBaseName == null)
		{
			CreateFileBaseName=filename+"cut";
		}
//�f�B���N�g�������݂��邩���Ȃ��̃`�F�b�N�͊O���ł���
		if( BaseDirectory != null)
		{
			//	CreateFileBaseName=BaseDirectory+"\\"+CreateFileBaseName;
			CreateFileBaseName=BaseDirectory+CreateFileBaseName;//GUI����+\\���Ƃ�
		}

//�A�ԃt�@�C��string�\�z
		string stRebnan=Convert.ToString(RenbanKeta);
		stRebnan="d"+stRebnan;

		StreamWriter SWidx=null;
		string stidxs;
		if(IndexFileCreate == true )
		{
			//�C���f�b�N�X�t�@�C������filename+"�g���q"+".idx"
			FileStream FSidx=new FileStream(filename+"_"+FAS.kakutyoushi+".idx",
				FileMode.Create,FileAccess.Write);
			SWidx = new StreamWriter(FSidx);
			SWidx.WriteLine("FileNo,BlockNo,KITEN,Filesize");             
		}
		int SBIdx=0;
		int ReturnInt=0;
		int COUNT=buffer_Level;//4096=�f�t�H���g

		FileStream sr=new FileStream(filename,FileMode.Open,FileAccess.Read);
		BinaryReader br=new BinaryReader(sr);
		byte [] buffer=new byte[COUNT];
		bool flag,WriteOK=true;
		int ix,BlockNO=1,KITEN,beforeKITEN=0,cFiles,FileNO=0,ESBIdx=0;
		string CuttingFileName=null;
		FileStream CFN=null;
		BinaryWriter CFNBR=null;

		int [] KITENS;
		//�����t�@�C�����́@filename+"cut"+FileNO+".�g���q"
		if(Exe == true)
		{
			CuttingFileName=String.Format("{0}{1:"+stRebnan+"}{2}", CreateFileBaseName,FileNO,FAS.kakutyoushi);
			CFN=new FileStream(CuttingFileName,FileMode.Create,FileAccess.Write);
			CFNBR = new BinaryWriter(CFN);
		}

		
		if(FAS.AnalysisFormatTYPE == 0)
		{
		
			/*������ƍ��A���S���Y����kuth/Boyer�g�����ق�������*/
			for(int idx=0;idx<sr.Length;idx+=COUNT)
			{
				br.Read(buffer,0,COUNT);

				//---------- �o�b�t�@Block�T�[�`�������[�`��
				//�u���b�N�̖����ɃT�[�`�Ώۂ�����ꍇ�����[�`�����K�v
				BlockNO++;

				ix=0;

				while(ix < COUNT)
				{
					flag=false;
					//true�ɂȂ�܂ŏ������݁{����
					while(flag == false)
					{
						//write�o�b�t�@�͂��߂����8096�P�ʂŏ�������
						//���̂܂܂��Ɗ����t�@�C���̖����ɃS�~�w�b�_����
						if(Exe == true && WriteOK == true ){CFNBR.Write(buffer[ix]);} 
						if(buffer[ix] == FAS.AttHeader[SBIdx])
						{
							SBIdx++;
							if(SBIdx == FAS.AttHeader.Length)
							{
								flag = true;
								SBIdx=0;

							}
						}
						else
						{
							SBIdx=0;
						}

						if( FAS.EndHeader!=null && buffer[ix] == FAS.EndHeader[ESBIdx])
						{
							ESBIdx++;
							if(ESBIdx == FAS.EndHeader.Length)
							{
								WriteOK=false;
								ESBIdx=0;
							}                                 
						}
						else	{ESBIdx=0;}
						ix++;
						if(ix >= COUNT){break;}
					}
					if(flag == true)
					{
						ReturnInt++;
						KITEN=idx+ix-FAS.AttHeader.Length+1;//+1�͔z��[0]�̂���
						cFiles=KITEN-beforeKITEN;//EndHeder�̎��͕ʏ���
						beforeKITEN=KITEN;
						if(IndexFileCreate == true )
						{
							stidxs=String.Format("{0},{1},{2},{3}\n",ReturnInt,BlockNO,KITEN,cFiles);
							SWidx.Write(stidxs);
						}
						if(DebugTB != null)
						{
							//stidxs=String.Format("{0}    {1}    {2}\r\n",ReturnInt,BlockNO,KITEN);
							//�Ȃ�textbox�G���[�ł�̂�
							stidxs=String.Format("{0}\r\n",ReturnInt);
							DebugTB.AppendText(stidxs);
						}
						if(Exe == true)
						{
							//	CFNBR.Write(buffer);
							CFNBR.Flush();
							CFNBR.Close();
							CFN.Close();
							FileNO++;
							Console.WriteLine(FileNO);
							//1�u���b�N�ɕ����t�@�C��������Ƃ��̃��[�`�����K�v[clear!]
							CuttingFileName=String.Format("{0}{1:"+stRebnan+"}{2}",CreateFileBaseName,FileNO,FAS.kakutyoushi);
							CFN=new FileStream(CuttingFileName,FileMode.Create,FileAccess.Write);
							CFNBR = new BinaryWriter(CFN);
							CFNBR.Write(FAS.AttHeader);//�w�b�_�̏�������
							WriteOK = true;
							//	CFNBR.Write(buffer,ix-SearchBytes.Length,buffer.Length-(ix-SearchBytes.Length));
					
						}

					}
					//	SBIdx=0;//�폜����Ή��i�u���b�N�����ɂȂ�[clear!]
				}
				//	if(Exe == true && FirstWrited == false){CFNBR.Write(buffer);}
			}
		}//if(FAS.AnalysisFormatTYPE == 0)

			//WAV&RIFF/AVI�p
		else	if(FAS.AnalysisFormatTYPE == 1)
		{
			BlockNO=0;//1�ɂȂ��Ă�̂�0�ɏ�����
			int KadanPosition=0,FindCount=0,sw,oneWrite,sSize=0;
			uint Size=0;
			WriteOK=false;
			byte [] Size4byte=new byte[4];
			for(int idx=0;idx<sr.Length;idx+=COUNT)
			{
				br.Read(buffer,0,COUNT);
				 
				KITENS=SimpleArraySearchS(buffer,FAS.AttHeader,0,ref KadanPosition
					,ref FindCount);
			
				if(KITENS[0] !=-1)
				{
					for(int ic=0;ic<FindCount;ic++)
					{
						ReturnInt++;
						Console.WriteLine("block {0} ,\t bk{1},KITEN{2}",
							BlockNO,
							KITENS[ic],(BlockNO*COUNT)+KITENS[ic]);
						sw=KITENS[ic];
						Size=8+ReverceEdian(buffer[sw+4],buffer[sw+5],buffer[sw+6],buffer[sw+7]);
						Console.WriteLine("@ {0:x2}{1:x2}{2:x2}{3:x2} SIZE={4}+8",
							buffer[sw+4],buffer[sw+5],buffer[sw+6],buffer[sw+7],Size);
					
						if(Exe == true)
						{
							if(WriteOK == true)
							{
								CFNBR.Write(buffer,0,KITENS[ic]);
							}
							CFNBR.Flush();
							CFNBR.Close();
							CFN.Close();
							FileNO++;
							Console.WriteLine("FileCreate{0}",FileNO);
							CuttingFileName=String.Format("{0}{1:"+stRebnan+"}{2}",CreateFileBaseName,FileNO,FAS.kakutyoushi);
							CFN=new FileStream(CuttingFileName,FileMode.Create,FileAccess.Write);
							CFNBR = new BinaryWriter(CFN);
							CFNBR.Write(buffer,KITENS[ic],buffer.Length-KITENS[ic]);
							sSize=(int)Size;
							sSize-=buffer.Length-KITENS[ic];
						WriteOK = true;					
						}
					}
				}
				else
				{
					if(Exe == true && WriteOK == true)
					{
						
						if(sSize < buffer.Length)
						{
							//oneWrite=sSize;
							CFNBR.Write(buffer,0,sSize);
							//CFNBR.Write(buffer,0,sSize+8);//?�b��
							WriteOK = false;
						}
						else
						{
							sSize-=buffer.Length;
							CFNBR.Write(buffer);
						}
					}
				}
				BlockNO++;
			}
		//	if(Exe == true){CFN.Close();}
		}
		if(Exe == true){CFNBR.Flush();CFNBR.Close();
		//00file���폜(�b��)
			File.Delete(String.Format("{0}{1:"+stRebnan+"}{2}",CreateFileBaseName,0,FAS.kakutyoushi));
		}
		br.Close();
		sr.Close();
	       
		if(IndexFileCreate == true ){SWidx.Flush();SWidx.Close();}
		return ReturnInt;
	}//_cut

	private uint ReverceEdian(byte B1,byte B2,byte B3,byte B4)
	{
		uint R1,R2,R3,R4;
		R1=B1;
		R2=B2;
		R3=B3;
		R4=B4;
		R1=R1+( R2 << 8);
		R1=R1+( R3 << 16);
		R1=R1+( R4 << 24);
		return R1;
	}

	//APosition=0;�T���ʒu
	//����KMP��Boyer(BM)�@��p�ӂ���
	public int SimpleArraySearch(byte [] ArrayA,byte [] ArrayB,int APosition)
	{
		int BIdx=0;
		while(APosition < ArrayA.Length)
		{
			if( ArrayA[APosition] == ArrayB[BIdx])
			{
				BIdx++;
				if(BIdx == ArrayB.Length)
				{
					return APosition-ArrayB.Length;
				}
			}
			else
			{
				BIdx=0;
			}
			APosition++;
		}
		return -1;//������Ȃ�����
	}

	
	//���������|�W�V���������ׂĂ�����
	public int [] SimpleArraySearchS(byte [] ArrayA,byte [] ArrayB,
		int APosition,ref int KadanPoint,ref int FindCount)
	{
		FindCount=0;
		int BIdx=KadanPoint;
		int [] RInt=new int[100];//1�u���b�N��100�ȓ��Ƒz��
		//ArrayList�g�p
		RInt[0]=-1;
		int Rindex=0;
		while(APosition < ArrayA.Length)
		{
			if( ArrayA[APosition] == ArrayB[BIdx])
			{
				BIdx++;
				if(BIdx == ArrayB.Length)
				{
					RInt[Rindex]=APosition+1-ArrayB.Length;
					Rindex++;
					BIdx=0;
				}
			}
			else
			{
				BIdx=0;
			}
			APosition++;
		}
		KadanPoint=BIdx;
		FindCount=Rindex;
		return RInt;//������Ȃ�������[0]=-1
	}
}

//�t�@�C���ŋL�q�ł���悤�ɂ���
//�t�@�C���I�[���L�q
//�w�b�_�������ɂ����ƃo�b�t�@�u���b�N�̒������K�v�ɂȂ�̂�4-10byte�ɗ}����[clear!]
public class FileAttributeSet
{
	public string kakutyoushi;
	public byte [] AttHeader;
	public byte [] EndHeader=null;
	public bool ERR=false;
	public int AnalysisFormatTYPE=0;//�t�H�[�}�b�g��̓^�C�v
	
	//�����ł���g���q��Ԃ�(GUI��comboBOx�Ŏg�p)
	public string [] printAttribute()
	{
		//���ƂŐݒ�t�@�C����
		string [] Rstrings={"-wmv","-ogg","-png","-wav","-avi"};
		return Rstrings;
	}


	public void SetHeader(string cmdline)
	{
		if(cmdline == "-wmv")
		{
			
			byte [] WMVbyte={0x30,0x26,0xB2,0x75,0x8E,0x66,0xCF,
								0x11,0xA6,0xD9,0x00,0xAA,0x00,0x62,0xCE,0x6C};//GUID
			//3026B2758E66CF11A6D900AA0062CE6C
			//ASF-EndStart C90349CB=���񂩃f�[�^�ɂłĂ���̂ł���ł͕����ł��Ȃ�
			//scholldays 021wmv C9 03 49 CB 38
			// C90349CB����500byte���ł�������J�b�g()         
			AttHeader=WMVbyte;
			//!!schoolDays�̎��̂݉��i�u���b�N��png�̐擪�w�b�_������
			byte [] WMVEnd={0x89,0x50,0x4e,0x47,0x0d,0x0a,0x1a,0x0a};
			EndHeader=WMVEnd;				
							
                  
			kakutyoushi=".wmv";
		}
		else if(cmdline == "-png")
		{
			//byte [] PNGbyte={0x89,0x50,0x4e,0x47};//.PNG [Ascii]�PNG
			byte [] PNGbyte={0x89,0x50,0x4e,0x47,0x0d,0x0a,0x1a,0x0a};//.PNG CR LF Ctl-Z CF 
			byte [] PNGEnd={0x00,0x00,0x00,0x00,
							   0x49,0x45,0x4e,0x44,//IEND
							   0xae,0x42,0x60,0x82};////IEND-CRC
			
			AttHeader=PNGbyte;
			EndHeader=PNGEnd;
			kakutyoushi=".png";
		
		}
		else if(cmdline == "-ogg")
		{
			byte [] OGGbyte={0x4f,0x67,0x67,0x53,0x00,0x02,0x00};//OggS�E�E�E
			AttHeader=OGGbyte;
			kakutyoushi=".ogg";
		}
		else if(cmdline == "-wav")
		{
			byte [] RIFF_WAV={0x52,0x49,0x46,0x46};//RIFF
			AttHeader=RIFF_WAV;
			//read(seek 4,readbuff 4) ->convertLittleEdian+8
			//FormatReadSize=convertLittleEdian+8;//
			kakutyoushi=".wav";	
			AnalysisFormatTYPE=1;
		}
		else if(cmdline == "-avi")
		{
			byte [] RIFF_={0x52,0x49,0x46,0x46};//RIFF
			AttHeader=RIFF_;
			kakutyoushi=".avi";	
			AnalysisFormatTYPE=1;
		}
	
		else{ERR=true;Console.WriteLine("cmdERR");}
	}
	/*
	  [�ǉ����郁�\�b�h]
    
	  public void SecondFileHeaderCheack(string File,string kakutyoushi){}
	  �t�@�C���w�b�_�̃p�����[�^�[�����݂�����l����]������
	  �t�@�C���̐��������t�@�C���J�b�g���Ƀ`�F�b�N����->�G���[�l�����𒴂���΃X�g�b�v
    
	  public void CRCs(string filetype or Method){}
	  PNG/ogg�Ȃǂ̊eCRC�v�Z�@���������ăp�����[�^�[�œn��
    
	  public void FileHeaderVervosePrint(string file,string kakustyoushi){}
	  �e�t�@�C���̃w�b�_�̏ڍׂ�\��

	  public string [] LookAttFile(string file){}
	  �w��t�@�C���ɂǂꂾ���̎�ނ̃t�@�C�������邩����
	  oggS,PNG/IHDR/IDAT,BM/BM6,JFIF/0xFF.0xD9/GIF89a/WMV/RIFF/MP3
	   [PNG��IDAT�̃J�E���g,�n�t�}���\,infate/defalate�W�J]
	 */
}