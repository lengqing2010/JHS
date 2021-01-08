Attribute VB_Name = "Cmdline"
Option Explicit

'*< TOSTEM >*******************************************************************'
'*
'*【プロシージャ名】
'*!     GetCommandLine
'*
'*【処理概要】
'*?     ｺﾏﾝﾄﾞﾗｲﾝ引数の値を取得する
'*      （区切りはスペースかタブ）
'*      指定した引数の数より多い場合は無視されます
'*      (指定なしは10以上は無視します)
'*      ※引数をﾀﾞﾌﾞﾙｺｰﾃｰｼｮﾝで囲ってもOKです（無視しますので）
'*
'*【パラメータ】
'*  <   Name            Type        Comment                               >
'*P     MaxArgs;        Variant;    引数の数(省略可能：省略時は最大で10まで）;
'*
'*【戻り値】
'*  <   Name            Type        Comment                               >
'*R     GetCommandLine; Array;      取得したｺﾏﾝﾄﾞﾗｲﾝ引数(引数が存在しない場合は空文字を返す）
'*
'******************************************************************************'
'*【変更履歴】
'*  < Ver          日付         名前(会社名)            説明                   >
'*M   Ver 1. 0. 0  2004/09/14;  K.Fukutomi;            新規作成;
'*
'*< Comment End >**************************************************************'
Public Function GetCommandLine(Optional MaxArgs)
On Error GoTo Err_GetCommandLine
    ' 変数を宣言します。
    Dim C As String
    Dim Cmdline As Variant
    Dim CmdLnLen As Long
    Dim InArg As Boolean
    Dim i As Long
    Dim NumArgs As Long
    Dim argArray() As String
    
    ' MaxArgs が提供されるかどうかを調べます。
    If IsMissing(MaxArgs) Then MaxArgs = 10
    
    NumArgs = 0: InArg = True
    
    ' コマンド ラインの引数を取得します。
    Cmdline = Command()
    CmdLnLen = Len(Cmdline)
    ' 同時にコマンド ラインの引数を取得します。
    If Trim(Cmdline) <> "" Then
        ReDim argArray(0)
    End If

    For i = 1 To CmdLnLen
        C = Mid(Cmdline, i, 1)
        ' スペースまたはタブを調べます。
        If (C <> " " And C <> vbTab) Then
            ' スペースまたはタブのいずれでもありません。
            ' 既に引数の中ではないかどうかを調べます。
            If Not InArg Then
                ' 新しい引数が始まります。
                ' 引数が多すぎないかを調べます。
                If NumArgs = MaxArgs Then Exit For
                NumArgs = NumArgs + 1
                ' 引数がすべて格納できるように配列のサイズを変更します。
                ReDim Preserve argArray(NumArgs)
                InArg = True
            End If

            'If C <> """" And C <> "'" Then
            If C <> """" Then
                ' 現在の引数に文字を追加します。
                argArray(NumArgs) = argArray(NumArgs) + C
            End If
        Else
            ' スペースまたはタブを見つけました。
            ' InArg フラグに False を設定します。
            InArg = False
        End If
    Next i
    If Trim(Cmdline) <> "" Then
        ReDim Preserve argArray(MaxArgs)
        ' 関数名に配列を返します。
        GetCommandLine = argArray
    Else
        Erase argArray
        GetCommandLine = ""
    End If
    
Exit_GetCommandLine:
    Exit Function
    
Err_GetCommandLine:
    Erase argArray
    GetCommandLine = "┗●●┛ERROR:GetCommandLine┗●●┛"
    Resume Exit_GetCommandLine
    
End Function



