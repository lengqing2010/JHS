Imports Itis.Earth.DataAccess
Imports System.Transactions


Public Class SinnseiKbnJyouhouLogic

    Private SinnseiKbnJyouhouDA As New SinnseiKbnJyouhouDataAccess
    Private KakakuJyouhouBL As New KakakuseikyuJyouhouLogic()

    ''' <summary>基本情報を更新する</summary>
    Public Function UpdSinnseiInfo(ByVal kameiten_cd As String _
                                        , ByVal sinnsei_syosiki As String _
                                        , ByVal shinsei_kbn_shinki As String _
                                        , ByVal shinsei_kbn_sonota As String _
                                        , ByVal shinsei_kbn_jig_shinki As String _
                                        , ByVal shinsei_kbn_jig_fudousan As String _
                                        , ByVal shinsei_kbn_jig_reform As String _
                                        , ByVal shinsei_kbn_jig_sonota As String _
                                        , ByVal shinsei_kbn_ser_jibantyousa As String _
                                        , ByVal shinsei_kbn_ser_tatemonokensa As String _
                                        , ByVal shinsei_kbn_jig_sonota_hosoku As String _
                                        , ByVal shinsei_kbn_ser_sonota As String _
                                        , ByVal shinsei_kbn_ser_sonota_hosoku As String _
                                        , ByVal upd_login_user_id As String) As String

        Dim kino As String

        kino = "申請区分情報の登録"

        If SinnseiKbnJyouhouDA.UpdSinnseiInfo(kameiten_cd _
                                                        , sinnsei_syosiki _
                                                        , shinsei_kbn_shinki _
                                                        , shinsei_kbn_sonota _
                                                        , shinsei_kbn_jig_shinki _
                                                        , shinsei_kbn_jig_fudousan _
                                                        , shinsei_kbn_jig_reform _
                                                        , shinsei_kbn_jig_sonota _
                                                        , shinsei_kbn_ser_jibantyousa _
                                                        , shinsei_kbn_ser_tatemonokensa _
                                                        , shinsei_kbn_jig_sonota_hosoku _
                                                        , shinsei_kbn_ser_sonota _
                                                        , shinsei_kbn_ser_sonota_hosoku _
                                                        , upd_login_user_id) Then
            Return KakakuJyouhouBL.MakeMessage(Messages.Instance.MSG018S, kino)

        Else
            Return KakakuJyouhouBL.MakeMessage(Messages.Instance.MSG019E, kino)
        End If

    End Function

    ''' <summary>基本情報を取得する</summary>
    Public Function GetSinnseiInfo(ByVal kameiten_cd As String) As DataTable

        Return SinnseiKbnJyouhouDA.SelSinnseiInfo(kameiten_cd)

    End Function
End Class
