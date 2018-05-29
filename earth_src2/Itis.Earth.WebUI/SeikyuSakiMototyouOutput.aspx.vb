Imports Itis.Earth.BizLogic
Imports Itis.ApplicationBlocks.ExceptionManagement
Imports System.Reflection
Imports System.Data
''' <summary>
''' �����挳�����[�o�͏���
''' </summary>
''' <history>
''' <para>2010/07/14�@���Ǘz(��A���V�X�e����)�@�V�K�쐬</para>
''' </history>
Partial Public Class SeikyuSakiMototyouOutput1
    Inherits System.Web.UI.Page

#Region "�v���C�x�[�g�ϐ�"

    Private seikyuSakiMototyouOutputLogic As New SeikyuSakiMototyouOutputLogic
    '���O�C�����[�U�[���擾����B
    Private ninsyou As New Ninsyou()

    'FCW�̃N���X���쐬
    Private fcw As Itis.Earth.BizLogic.FcwUtility
    Private kinouId As String = "seikyuusaki"

    Public Const COMMA As Char = ","c

    Enum PDFStatus As Integer
        OK = 0                              '����
        IOException = 1                     '�G���[(���̃��[�U���t�@�C�����J���Ă���)
        UnauthorizedAccessException = 2     '�G���[(�t�@�C�����쐬����p�X���s��)
        NoData = 3                          '�Ώۂ̃f�[�^���擾�ł��܂���B

    End Enum

#End Region

    ''' <summary>
    ''' �y�[�W���[�h
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.Page.Form.Attributes.Add("onkeydown", "if(event.keyCode==13){return false;}")

        If Not IsPostBack Then
            '��{�F��
            If ninsyou.GetUserID() = "" Then
                Context.Items("strFailureMsg") = Messages.Instance.MSG2024E
                Server.Transfer("CommonErr.aspx")
            Else

                ViewState("strSeikyuuSakiCd") = Context.Request("seiCd")
                ViewState("strSeikyuuSakiBrc") = Context.Request("seiBrc")
                ViewState("strSeikyuuSakiKbn") = Context.Request("seiKbn")
                ViewState("strFromDate") = Context.Request("fromDate")
                ViewState("strToDate") = Context.Request("toDate")
                ViewState("syainCd") = ninsyou.GetUserID()
            End If
        End If

        '�����挳�����[�o��
        Call CreateDataSource()

    End Sub

    ''' <summary>
    ''' �f�[�^�t�@�C�������܂�
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CreateDataSource()
        'EMAB��Q�Ή����̊i�[����
        UnTrappedExceptionManager.AddMethodEntrance(MyClass.GetType.FullName & "." & MethodBase.GetCurrentMethod.Name)

        '�C���X�^���X�̐���
        Dim dtUriageNyukinDataTable As Data.DataTable   '��������f�[�^dt
        Dim dtSeikyuuNmDataTable As Data.DataTable      '�����於dt
        Dim strSeikyuuSakiCd As String = String.Empty   '������R�[�h			
        Dim strSeiNm As String = String.Empty           '�����於 
        Dim strSeikyuuSakiBrc As String = String.Empty  '������}��
        Dim strSeikyuuSakiKbn As String = String.Empty  '������敪
        Dim strFromDate As String = String.Empty        '���o����FROM(YYYY / MM / DD)
        Dim strToDate As String = String.Empty          '���o����TO(YYYY / MM / DD)
        Dim lngTyokuzengyouzandaka As Long = 0          '���O�s�̎c��
        Dim lngGoukeikin As Long = 0                    '�ŏI�s_���z(����f�[�^�e�[�u��.������z�{�O�Ŋz �̍��v�l)
        Dim errMsg As String = String.Empty
        Dim syainCd As String                           '�Ј��R�[�h
        Dim KakusyuDataSyuturyokuMenuBL As New KakusyuDataSyuturyokuMenuLogic

        strSeikyuuSakiCd = ViewState("strSeikyuuSakiCd")
        strSeikyuuSakiBrc = ViewState("strSeikyuuSakiBrc")
        strSeikyuuSakiKbn = ViewState("strSeikyuuSakiKbn")
        strFromDate = ViewState("strFromDate")
        strToDate = ViewState("strToDate")
        syainCd = ViewState("syainCd")

        '�����於�̎擾
        dtSeikyuuNmDataTable = KakusyuDataSyuturyokuMenuBL.GetSeikyuuSakiInfo(strSeikyuuSakiCd.Trim, strSeikyuuSakiBrc.Trim, strSeikyuuSakiKbn.Trim, String.Empty, String.Empty, 1, 1, 10, True)

        If dtSeikyuuNmDataTable.Rows.Count <> 0 Then
            strSeiNm = dtSeikyuuNmDataTable.Rows(0).Item("seikyuu_saki_mei").ToString.Trim
        Else
            strSeiNm = String.Empty
        End If

        fcw = New FcwUtility(Page, syainCd, kinouId)

        '�J�z�c�����擾
        Dim kurikosiZan As Long = seikyuSakiMototyouOutputLogic.GetKurikosiZandaData(strSeikyuuSakiCd, strSeikyuuSakiBrc, strSeikyuuSakiKbn, strFromDate)

        '��������f�[�^�擾
        dtUriageNyukinDataTable = seikyuSakiMototyouOutputLogic.GetUriageNyukinData(strSeikyuuSakiCd, strSeikyuuSakiBrc, strSeikyuuSakiKbn, strFromDate, strToDate)

        If dtUriageNyukinDataTable.Rows.Count > 0 Then
            '---���������f�[�^��ҏW����B---st
            Dim editDt As New DataTable
            Dim editDR As Data.DataRow = editDt.NewRow
            Dim sb As New StringBuilder
            Dim intCount As Integer

            editDt.Columns.Add("seiKbn", GetType(String))
            editDt.Columns.Add("seiCdBrc", GetType(String))
            editDt.Columns.Add("seiNm", GetType(String))
            editDt.Columns.Add("fromDate", GetType(String))
            editDt.Columns.Add("toDate", GetType(String))
            editDt.Columns.Add("pageHide", GetType(String))
            editDt.Columns.Add("pagesTotal", GetType(String))
            editDt.Columns.Add("denpyou_uri_date", GetType(String))
            editDt.Columns.Add("kamoku", GetType(String))
            editDt.Columns.Add("syouhin_cd", GetType(String))
            editDt.Columns.Add("hinmei", GetType(String))
            editDt.Columns.Add("kbnbangou", GetType(String))
            editDt.Columns.Add("jutyuu_bukken_mei", GetType(String))
            editDt.Columns.Add("suu", GetType(String))
            editDt.Columns.Add("tanka", GetType(String))
            editDt.Columns.Add("uri_gaku", GetType(String))
            editDt.Columns.Add("sotozei_gaku", GetType(String))
            editDt.Columns.Add("gaku", GetType(String))
            editDt.Columns.Add("zendaka", GetType(String))
            editDt.Columns.Add("seikyuu_date", GetType(String))
            editDt.Columns.Add("kaisyuu_yotei_date", GetType(String))
            editDt.Columns.Add("denpyou_no", GetType(String))

            '������敪
            If strSeikyuuSakiKbn = 0 Then
                editDR("seiKbn") = "�����X"
            ElseIf strSeikyuuSakiKbn = 1 Then
                editDR("seiKbn") = "�������"
            ElseIf strSeikyuuSakiKbn = 2 Then
                editDR("seiKbn") = "�c�Ə�"
            Else
                editDR("seiKbn") = ""
            End If

            '������R�[�h�{�}��
            editDR("seiCdBrc") = strSeikyuuSakiCd & "-" & strSeikyuuSakiBrc
            '�����於
            editDR("seiNm") = strSeiNm
            '���o����FROM(YYYY/MM/DD)
            editDR("fromDate") = NengoChange(CDate(strFromDate), False)
            '���o����TO(YYYY/MM/DD)
            editDR("toDate") = NengoChange(CDate(strToDate), False)

            If dtUriageNyukinDataTable.Rows.Count + 2 > 21 Then
                editDR("pageHide") = String.Empty
            Else
                editDR("pageHide") = " "
            End If

            intCount = dtUriageNyukinDataTable.Rows.Count

            editDR("pagesTotal") = Math.Ceiling((intCount + 2) / 21)

            '�擪�s����(�J�z�c���̎擾����)

            editDR("kamoku") = "�J�z�c��"
            editDR("zendaka") = kurikosiZan
            lngTyokuzengyouzandaka = kurikosiZan
            editDt.Rows.Add(editDR)
            editDR = editDt.NewRow

            '�f�[�^�s���ڂ̎擾����
            For j As Integer = 0 To dtUriageNyukinDataTable.Rows.Count - 1
                '�N����
                If TrimNull(dtUriageNyukinDataTable.Rows(j).Item("denpyou_uri_date")) = String.Empty Then
                    editDR("denpyou_uri_date") = String.Empty
                Else
                    editDR("denpyou_uri_date") = NengoChange(TrimNull(dtUriageNyukinDataTable.Rows(j).Item("denpyou_uri_date")), True)
                End If
                '�Ȗ�
                editDR("kamoku") = TrimNull(dtUriageNyukinDataTable.Rows(j).Item("kamoku"))
                '���i�R�[�h
                editDR("syouhin_cd") = TrimNull(dtUriageNyukinDataTable.Rows(j).Item("syouhin_cd"))
                '���i��/������ʂȂ�
                editDR("hinmei") = TrimNull(dtUriageNyukinDataTable.Rows(j).Item("hinmei"))
                '�ڋq�ԍ�
                editDR("kbnbangou") = TrimNull(dtUriageNyukinDataTable.Rows(j).Item("kbnbangou"))
                '������/�E�v�Ȃ�
                editDR("jutyuu_bukken_mei") = TrimNull(dtUriageNyukinDataTable.Rows(j).Item("jutyuu_bukken_mei"))
                '����
                editDR("suu") = TrimNull(dtUriageNyukinDataTable.Rows(j).Item("suu"))
                '�P��
                editDR("tanka") = TrimNull(dtUriageNyukinDataTable.Rows(j).Item("tanka"))
                '�Ŕ����z
                editDR("uri_gaku") = TrimNull(dtUriageNyukinDataTable.Rows(j).Item("uri_gaku"))
                '�����
                editDR("sotozei_gaku") = TrimNull(dtUriageNyukinDataTable.Rows(j).Item("sotozei_gaku"))
                '���z
                editDR("gaku") = TrimNull(dtUriageNyukinDataTable.Rows(j).Item("kingaku"))
                '�c��
                If TrimNull(dtUriageNyukinDataTable.Rows(j).Item("kamoku")) = "����" Then
                    editDR("zendaka") = lngTyokuzengyouzandaka + TrimNull(dtUriageNyukinDataTable.Rows(j).Item("kingaku"))
                    lngTyokuzengyouzandaka = lngTyokuzengyouzandaka + TrimNull(dtUriageNyukinDataTable.Rows(j).Item("kingaku"))
                Else
                    editDR("zendaka") = lngTyokuzengyouzandaka - TrimNull(dtUriageNyukinDataTable.Rows(j).Item("kingaku"))
                    lngTyokuzengyouzandaka = lngTyokuzengyouzandaka - TrimNull(dtUriageNyukinDataTable.Rows(j).Item("kingaku"))
                End If
                '���������s��
                If TrimNull(dtUriageNyukinDataTable.Rows(j).Item("seikyuu_date")) = String.Empty Then
                    editDR("seikyuu_date") = String.Empty
                Else
                    editDR("seikyuu_date") = NengoChange(TrimNull(dtUriageNyukinDataTable.Rows(j).Item("seikyuu_date")), True)
                End If
                '����\���
                If TrimNull(dtUriageNyukinDataTable.Rows(j).Item("kaisyuu_yotei_date")) = String.Empty Then
                    editDR("kaisyuu_yotei_date") = String.Empty
                Else
                    editDR("kaisyuu_yotei_date") = NengoChange(TrimNull(dtUriageNyukinDataTable.Rows(j).Item("kaisyuu_yotei_date")), True)
                End If
                '�`�[�ԍ�
                editDR("denpyou_no") = TrimNull(dtUriageNyukinDataTable.Rows(j).Item("denpyou_no"))
                '�ŏI�s_���z
                If TrimNull(dtUriageNyukinDataTable.Rows(j).Item("kamoku")) = "����" Then
                    lngGoukeikin = lngGoukeikin + TrimNull(dtUriageNyukinDataTable.Rows(j).Item("kingaku"))
                End If

                editDt.Rows.Add(editDR)
                editDR = editDt.NewRow
            Next
            '�Ȗ�
            editDR("kamoku") = "���v"
            editDR("hinmei") = "���ԍ��v"
            editDR("gaku") = lngGoukeikin
            editDR("zendaka") = lngTyokuzengyouzandaka
            editDt.Rows.Add(editDR)
            editDR = editDt.NewRow

            '---���������f�[�^��ҏW����B---ed
            'DAT�f�[�^���쐬
            ' �w�b�_�[���쐬
            sb.Append(fcw.CreateDatHeader())
            '   [FixedDataSection] ���쐬
            sb.Append(fcw.CreateFixedDataSection(GetFixedDataSection(editDt)))
            '   [TableDataSection] ���쐬
            sb.Append(fcw.CreateTableDataSection(GetTableDataSection(editDt)))

            errMsg = fcw.GetErrMsg(fcw.WriteData(sb.ToString))
        Else
            errMsg = fcw.GetErrMsg(PDFStatus.NoData)
        End If

        ' �����挳�����[�f�[�^PDF�@�o��
        If Not errMsg.Equals(String.Empty) Then
            '���b�Z�[�W
            Context.Items("strFailureMsg") = errMsg
            Server.Transfer("CommonErr.aspx")
        Else
            fcw.OpenPdf()
        End If
    End Sub

    ''' <summary>
    ''' GetFixedDataSection�̃f�[�^���擾
    ''' </summary>
    ''' <param name="data">�f�[�^</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetFixedDataSection(ByVal data As DataTable) As String

        'EMAB��Q�Ή����̊i�[����
        UnTrappedExceptionManager.AddMethodEntrance(MyClass.GetType.FullName & "." & MethodBase.GetCurrentMethod.Name)

        '�f�[�^���擾
        Return fcw.GetFixedDataSection( _
                                                     "seiKbn" & _
                                                     ",seiCdBrc" & _
                                                     ",seiNm" & _
                                                     ",fromDate" & _
                                                     ",toDate" & _
                                                     ",pageHide" & _
                                                     ",pagesTotal", data)

    End Function

    ''' <summary>
    ''' GetTableDataSection�̃f�[�^���擾
    ''' </summary>
    ''' <param name="data">�f�[�^</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetTableDataSection(ByVal data As DataTable) As String

        'EMAB��Q�Ή����̊i�[����
        UnTrappedExceptionManager.AddMethodEntrance(MyClass.GetType.FullName & "." & MethodBase.GetCurrentMethod.Name)

        '����CLASS
        Dim earthAction As New EarthAction

        '�f�[�^���擾
        Return earthAction.JoinDataTable( _
                                                    data, _
                                                    COMMA, _
                                                    "denpyou_uri_date" & _
                                                    ",kamoku" & _
                                                    ",syouhin_cd" & _
                                                    ",hinmei" & _
                                                    ",kbnbangou" & _
                                                    ",jutyuu_bukken_mei" & _
                                                    ",suu" & _
                                                    ",tanka" & _
                                                    ",uri_gaku" & _
                                                    ",sotozei_gaku" & _
                                                    ",gaku" & _
                                                    ",zendaka" & _
                                                    ",seikyuu_date" & _
                                                    ",kaisyuu_yotei_date" & _
                                                    ",denpyou_no")

    End Function

    ''' <summary>�󔒕����̍폜����</summary>
    Private Function TrimNull(ByVal objStr As Object) As String
        If IsDBNull(objStr) Then
            TrimNull = ""
        Else
            TrimNull = objStr.ToString.Trim
        End If
    End Function

    ''' <summary>
    ''' ->YYYY�NMM��DD����->YY�NMM��DD��
    ''' </summary>
    ''' <param name="nowDate">���t</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function NengoChange(ByVal nowDate As Date, Optional ByVal strFlg As Boolean = False) As String
        If strFlg = False Then
            Return nowDate.Year() & "�N" & Right("0" & nowDate.Month, 2) & "��" & Right("0" & nowDate.Day, 2) & "��"
        Else
            Return Right(nowDate.Year(), 2) & "/" & Right("0" & nowDate.Month, 2) & "/" & Right("0" & nowDate.Day, 2)
        End If
    End Function

End Class