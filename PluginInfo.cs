using System;
using System.Collections.Generic;
using System.Text;

// http://dobon.net/vb/dotnet/programing/plugin.html
namespace DataCutterFG
{
    public class PluginInfo
    {


        /// <summary>
        /// �A�Z���u���t�@�C���̃p�X
        /// </summary>
        public string Location;
        /// <summary>
        /// �N���X�̖��O
        /// </summary>
        public string ClassName;

        //Location����Dll�����������Ă�������
        public String DllName;

        /// <summary>
        /// PluginInfo�N���X�̃R���X�g���N�^
        /// </summary>
        /// <param name="path">�A�Z���u���t�@�C���̃p�X</param>
        /// <param name="cls">�N���X�̖��O</param>
        private PluginInfo(string path, string cls)
        {
            this.Location = path;
            this.ClassName = cls;
            
            String [] parse = path.Split(new char [] {'\\'});

            this.DllName = parse[parse.Length - 1];
        }



        /// <summary>
        /// �L���ȃv���O�C����T��
        /// </summary>
        /// <returns>�L���ȃv���O�C����PluginInfo�z��</returns>
        public static PluginInfo[] FindPlugins()
        {
            System.Collections.ArrayList plugins =
                new System.Collections.ArrayList();
            //IPlugin�^�̖��O
            string ipluginName = typeof(DataCutter_PlugIn.PluginInterface).FullName;

            //�v���O�C���t�H���_
            string folder = System.IO.Path.GetDirectoryName(
                System.Reflection.Assembly
                .GetExecutingAssembly().Location);
            folder += "\\plugin";
            if (!System.IO.Directory.Exists(folder))
                throw new ApplicationException(
                    "�v���O�C���t�H���_\"" + folder +
                    "\"��������܂���ł����B");

            //.dll�t�@�C����T��
            string[] dlls =
                System.IO.Directory.GetFiles(folder, "*.dll");

            foreach (string dll in dlls)
            {
                try
                {
                    //�A�Z���u���Ƃ��ēǂݍ���
                    System.Reflection.Assembly asm =
                        System.Reflection.Assembly.LoadFrom(dll);
                    foreach (Type t in asm.GetTypes())
                    {
                        //�A�Z���u�����̂��ׂĂ̌^�ɂ��āA
                        //�v���O�C���Ƃ��ėL�������ׂ�
                        if (t.IsClass && t.IsPublic && !t.IsAbstract &&
                            t.GetInterface(ipluginName) != null)
                        {
                            //PluginInfo���R���N�V�����ɒǉ�����
                            plugins.Add(
                                new PluginInfo(dll, t.FullName));
                        }
                    }
                }
                catch
                {
                }
            }

            //�R���N�V������z��ɂ��ĕԂ�
            return (PluginInfo[])plugins.ToArray(typeof(PluginInfo));
        }

        /// <summary>
        /// �v���O�C���N���X�̃C���X�^���X���쐬����
        /// </summary>
        /// <returns>�v���O�C���N���X�̃C���X�^���X</returns>
        public DataCutter_PlugIn.PluginInterface CreateInstance()
        {
            try
            {
                //�A�Z���u����ǂݍ���
                System.Reflection.Assembly asm =
                    System.Reflection.Assembly.LoadFrom(this.Location);
                //�N���X������C���X�^���X���쐬����
                return (DataCutter_PlugIn.PluginInterface)
                    asm.CreateInstance(this.ClassName);
            }
            catch
            {
                return null;
            }
        }
    }
}
