using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace DataCutter_PlugIn
{
    // プラグインを作るときはこれを継承する

    // 参考:http://dobon.net/vb/dotnet/programing/plugin.html

    public interface PluginInterface
    {
        // プラグインの名前
        string Pluginame { get;}

        // プラグインが処理する拡張子設定名配列（1プラグインが複数処理するのを許容）
        List<String> Attributenames { get;}

        // プラグイン固有のオプションダイアログがあるかないかを表します
        bool OptionDialog { get;}

        // プラグイン固有の設定ファイル名
        // 設定ファイルがある場合クラスのコンストラクタやcutterメソッド先頭で読み込みさせる必要がある
        string Settingname { get;}

        // 無効化する_cutter引数をStringで返します
        // TODO 未実装・ここで渡されたパラメーターはGUI要素を無効化させる(例:連番桁など)
        List<String> DisposeArgument { get;}

        // プラグインのバージョン 
        String Version { get;}

        // プラグインの著作権
        String Copyright { get;}

        // PLUGIN TYPE <Normal> 処理したファイル数を返します
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

        /* プラグイン固有の設定ダイアログを表示
         * WidowsFormを継承したダイアログをshowするコードを書く
         * 設定ファイルに設定を記録(XMLが好ましい)
         * dll名+".xml" が設定ファイル名
        */
        void showOptionDialog();
    }
}
