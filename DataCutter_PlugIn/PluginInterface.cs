using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace DataCutter_PlugIn
{
    // �v���O�C�������Ƃ��͂�����p������

    // �Q�l:http://dobon.net/vb/dotnet/programing/plugin.html

    public interface PluginInterface
    {
        // �v���O�C���̖��O
        string Pluginame { get;}

        // �v���O�C������������g���q�ݒ薼�z��i1�v���O�C����������������̂����e�j
        List<String> Attributenames { get;}

        // �v���O�C���ŗL�̃I�v�V�����_�C�A���O�����邩�Ȃ�����\���܂�
        bool OptionDialog { get;}

        // �v���O�C���ŗL�̐ݒ�t�@�C����
        // �ݒ�t�@�C��������ꍇ�N���X�̃R���X�g���N�^��cutter���\�b�h�擪�œǂݍ��݂�����K�v������
        string Settingname { get;}

        // ����������_cutter������String�ŕԂ��܂�
        // TODO �������E�����œn���ꂽ�p�����[�^�[��GUI�v�f�𖳌���������(��:�A�Ԍ��Ȃ�)
        List<String> DisposeArgument { get;}

        // �v���O�C���̃o�[�W���� 
        String Version { get;}

        // �v���O�C���̒��쌠
        String Copyright { get;}

        // PLUGIN TYPE <Normal> ���������t�@�C������Ԃ��܂�
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
             );

        /* �v���O�C���ŗL�̐ݒ�_�C�A���O��\��
         * WidowsForm���p�������_�C�A���O��show����R�[�h������
         * �ݒ�t�@�C���ɐݒ���L�^(XML���D�܂���)
         * dll��+".xml" ���ݒ�t�@�C����
        */
        void showOptionDialog();
    }
}
