using System;
using System.Collections.Generic;
using System.Text;

namespace DataCutter_PlugIn
{
    public class PluginUtil
    {
        public static uint ReverceEdian(byte B1, byte B2, byte B3, byte B4)
        {
            uint R1, R2, R3, R4;
            R1 = B1;
            R2 = B2;
            R3 = B3;
            R4 = B4;
            R1 = R1 + (R2 << 8);
            R1 = R1 + (R3 << 16);
            R1 = R1 + (R4 << 24);
            return R1;
        }

        //APosition=0;�T���ʒu
        //����KMP��Boyer(BM)�@��p�ӂ���
        public static int SimpleArraySearch(byte[] ArrayA, byte[] ArrayB, int APosition)
        {
            int BIdx = 0;
            while (APosition < ArrayA.Length)
            {
                if (ArrayA[APosition] == ArrayB[BIdx])
                {
                    BIdx++;
                    if (BIdx == ArrayB.Length)
                    {
                        return APosition - ArrayB.Length;
                    }
                }
                else
                {
                    BIdx = 0;
                }
                APosition++;
            }
            return -1;//������Ȃ�����
        }


        //���������|�W�V���������ׂĂ�����
        public static int[] SimpleArraySearchS(byte[] ArrayA, byte[] ArrayB,
            int APosition, ref int KadanPoint, ref int FindCount)
        {
            FindCount = 0;
            int BIdx = KadanPoint;
            int[] RInt = new int[100];//1�u���b�N��100�ȓ��Ƒz��
            //ArrayList�g�p
            RInt[0] = -1;
            int Rindex = 0;
            while (APosition < ArrayA.Length)
            {
                if (ArrayA[APosition] == ArrayB[BIdx])
                {
                    BIdx++;
                    if (BIdx == ArrayB.Length)
                    {
                        RInt[Rindex] = APosition + 1 - ArrayB.Length;
                        Rindex++;
                        BIdx = 0;
                    }
                }
                else
                {
                    BIdx = 0;
                }
                APosition++;
            }
            KadanPoint = BIdx;
            FindCount = Rindex;
            return RInt;//������Ȃ�������[0]=-1
        }
    }
}
