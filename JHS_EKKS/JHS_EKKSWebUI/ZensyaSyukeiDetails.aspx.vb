Option Explicit On
Option Strict On

Imports EMAB = Itis.ApplicationBlocks.ExceptionManagement.UnTrappedExceptionManager
Imports MyMethod = System.Reflection.MethodBase
Imports Lixil.JHS_EKKS.BizLogic
Imports Lixil.JHS_EKKS.Utilities
Imports System.Data

''' <summary>
''' �S�� �W�v �ڍ�
''' </summary>
''' <remarks>�S�� �W�v �ڍ�</remarks>
''' <history>
''' <para>2012/11/24 P-44979 ���V �V�K�쐬 </para>
''' </history>
Partial Class ZensyaSyukeiDetails
    Inherits System.Web.UI.Page

#Region "�v���C�x�[�g�ϐ�"
    '�C���X�^���X����
    Private objZensyaSyukeiDetailsBC As New Lixil.JHS_EKKS.BizLogic.ZensyaSyukeiDetailsBC
    Private objCommonBC As New Lixil.JHS_EKKS.BizLogic.CommonBC
    Private objCommon As New Common
    Private objCommonCheck As New CommonCheck
#End Region

#Region "�ϐ�"
    '���j���[�ԍ�
    Private mstrMenuno As String
#End Region

#Region "�C�x���g"
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'EMAB��Q�Ή����̊i�[����
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name, sender, e)

        Dim strYear As String                   '�N�x
        Dim strKi As String                     '��
        Dim strBeginMonth As String             '�J�n��
        Dim strEndMonth As String               '�I����
        Dim arrList As ArrayList                '�����X�g

        Dim dtKakoJiData As DataTable           '�H���䗦�f�[�^
        Dim dtEigyouData As DataTable           '�c�Ƃ̃f�[�^
        Dim dtSinkiData As DataTable            '�V�K�̃f�[�^
        Dim dtTokuhanData As DataTable          '���̂̃f�[�^
        Dim dtFCData As DataTable               'FC�̃f�[�^
        Dim dtAllData As DataTable              '�S�̂̃f�[�^
        Dim dtAllFCNozokuData As DataTable      '�S��FC�����̃f�[�^

        If Not IsPostBack Then
            mstrMenuno = "U009"         '�N�x��
            mstrMenuno = "U010"         '����
            mstrMenuno = "U011"         '����

            '�O��ʂ̈������擾����
            If Request.QueryString("strYear") Is Nothing Then
                strYear = String.Empty
            Else
                strYear = Request.QueryString("strYear")
            End If

            If Request.QueryString("strKi") Is Nothing Then
                strKi = String.Empty
            Else
                strKi = Request.QueryString("strKi")
            End If

            If Request.QueryString("strBeginMonth") Is Nothing Then
                strBeginMonth = String.Empty
            Else
                strBeginMonth = Request.QueryString("strBeginMonth")
            End If

            If Request.QueryString("strEndMonth") Is Nothing Then
                strEndMonth = strBeginMonth
            Else
                strEndMonth = Request.QueryString("strEndMonth")
                If strEndMonth.Equals(String.Empty) Then
                    strEndMonth = strBeginMonth
                End If
            End If

            '�^�C�g����ݒ肷��
            Me.Title = "�S�ЏW�v�ڍ�"
            Call SetTitle(strYear, strKi, strBeginMonth, strEndMonth)

            '�H���䗦��ݒ肷��
            dtKakoJiData = objZensyaSyukeiDetailsBC.GetKakoJittusekiKanriData(strYear)
            Call SetKojData(dtKakoJiData)

            '�N�x�A���A���𔻒f����
            If Not strKi.Equals(String.Empty) Then
                '���ʂ̏ꍇ
                arrList = objCommon.GetMonthKikan(CType(strKi, Common.MonthKikan))
            ElseIf Not strBeginMonth.Equals(String.Empty) Then
                '���ʂ̏ꍇ
                arrList = objCommon.GetMonthKikan(Convert.ToInt32(strBeginMonth), Convert.ToInt32(strEndMonth))
            Else
                '�N�x�ʂ̏ꍇ
                arrList = objCommon.GetMonthKikan(Common.MonthKikan.All)
            End If

            '�c�Ƃ̃f�[�^��߂�
            dtEigyouData = objZensyaSyukeiDetailsBC.GetSensyaSyuukeiData(strYear, "1", arrList)

            '�c�ƕ��̃f�[�^��ݒ肷��
            Call SetGridData(dtEigyouData, "grdEigyouMeisai")

            '�V�K�̃f�[�^��߂�
            dtSinkiData = objZensyaSyukeiDetailsBC.GetSensyaSyuukeiData(strYear, "2", arrList)

            '�V�K���̃f�[�^��ݒ肷��
            Call SetGridData(dtSinkiData, "grdSinkiMeisai")

            '���̂̃f�[�^��߂�
            dtTokuhanData = objZensyaSyukeiDetailsBC.GetSensyaSyuukeiData(strYear, "3", arrList)

            '���̕��̃f�[�^��ݒ肷��
            Call SetGridData(dtTokuhanData, "grdTokuhanMeisai")

            'FC�̃f�[�^��߂�
            dtFCData = objZensyaSyukeiDetailsBC.GetSensyaSyuukeiData(strYear, "4", arrList)

            'FC���̃f�[�^��ݒ肷��
            Call SetGridData(dtFCData, "grdFCMeisai")

            '�S�̂̃f�[�^��߂�
            dtAllData = objZensyaSyukeiDetailsBC.GetSensyaSyuukeiData(strYear, "1,2,3,4", arrList)

            '�S�̕��̃f�[�^��ݒ肷��
            Call SetGridData(dtAllData, "grdAllMeisai")

            '�S�̂̃f�[�^��߂�
            dtAllFCNozokuData = objZensyaSyukeiDetailsBC.GetSensyaSyuukeiData(strYear, "1,2,3", arrList)

            '�S��FC�������̃f�[�^��ݒ肷��
            Call SetGridData(dtAllFCNozokuData, "grdAllNotFCMeisai")
        End If
    End Sub


#End Region

#Region "�����b�h"
    ''' <summary>
    ''' �^�C�g����ݒ肷��
    ''' </summary>
    ''' <param name="strYear">�v��N�x</param>
    ''' <param name="strKi">����</param>
    ''' <param name="strBeginMonth">�J�n��</param>
    ''' <param name="strEndMonth">�I����</param>
    ''' <remarks></remarks>
    Private Sub SetTitle(ByVal strYear As String, ByVal strKi As String, _
                         ByVal strBeginMonth As String, ByVal strEndMonth As String)
        'EMAB��Q�Ή����̊i�[����
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name, strYear, strKi, strBeginMonth, strEndMonth)

        Dim strTitle As String
        Dim strPara(1) As String
        strTitle = "�S�ЏW�v {0} �ڍ�"

        strPara(0) = strYear

        If Not strKi.Equals(String.Empty) Then
            Select Case Convert.ToInt16(strKi)
                Case 2
                    strPara(1) = "��� "
                Case 3
                    strPara(1) = "���� "
                Case 4
                    strPara(1) = "�l����(4,5,6��) "
                Case 5
                    strPara(1) = "�l����(7,8,9��) "
                Case 6
                    strPara(1) = "�l����(10,11,12��) "
                Case 7
                    strPara(1) = "�l����(1,2,3��) "
                Case Else
                    strPara(1) = ""
            End Select
        ElseIf Not strBeginMonth.Equals(String.Empty) Then
            strPara(1) = strBeginMonth & "�� �` " & strEndMonth & "�� "
        Else
            strPara(1) = ""
        End If

        Me.lblNendo.Text = String.Format("�S�� �W�v {0}�N�x {1}�ڍ�", strPara)
    End Sub

    ''' <summary>
    ''' �H���䗦��ݒ肷��
    ''' </summary>
    ''' <param name="dtValue">�H���䗦�f�[�^</param>
    ''' <remarks>�H���䗦��ݒ肷��</remarks>
    Private Sub SetKojData(ByVal dtValue As DataTable)
        'EMAB��Q�Ή����̊i�[����
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name, dtValue)

        If dtValue Is Nothing OrElse dtValue.Rows.Count <= 0 Then
            Me.numKoj1_1.Value = "0"
            Me.numKoj1_2.Value = "0"
            Me.numKoj1_3.Value = "0"

            Me.numKoj2_1.Value = "0"
            Me.numKoj2_2.Value = "0"
            Me.numKoj2_3.Value = "0"

            Me.numKoj3_1.Value = "0"
            Me.numKoj3_2.Value = "0"
            Me.numKoj3_3.Value = "0"

            Me.numKoj4_1.Value = "0"
            Me.numKoj4_2.Value = "0"
            Me.numKoj4_3.Value = "0"

            Me.numKoj5_1.Value = "0"
            Me.numKoj5_2.Value = "0"
            Me.numKoj5_3.Value = "0"

            Me.numKoj6_1.Value = "0"
            Me.numKoj6_2.Value = "0"
            Me.numKoj6_3.Value = "0"
            Exit Sub
        End If

        Dim drTemp() As DataRow                     '����/�H���̃f�[�^

        '�c�Ƃ̏ꍇ
        drTemp = dtValue.Select("eigyou_kbn='1'")
        If drTemp.Length > 0 Then
            Me.numKoj1_1.Value = Convert.ToString(drTemp(0).Item("koj_hantei_ritu"))    '�H�����藦
            Me.numKoj1_2.Value = Convert.ToString(drTemp(0).Item("koj_jyuchuu_ritu"))   '�H���󒍗�
            Me.numKoj1_3.Value = Convert.ToString(drTemp(0).Item("tyoku_koj_ritu"))     '���H����
        Else
            Me.numKoj1_1.Value = "0"
            Me.numKoj1_2.Value = "0"
            Me.numKoj1_3.Value = "0"
        End If

        '�V�K�̏ꍇ
        drTemp = dtValue.Select("eigyou_kbn='2'")
        If drTemp.Length > 0 Then
            Me.numKoj2_1.Value = Convert.ToString(drTemp(0).Item("koj_hantei_ritu"))    '�H�����藦
            Me.numKoj2_2.Value = Convert.ToString(drTemp(0).Item("koj_jyuchuu_ritu"))   '�H���󒍗�
            Me.numKoj2_3.Value = Convert.ToString(drTemp(0).Item("tyoku_koj_ritu"))     '���H����
        Else
            Me.numKoj2_1.Value = "0"
            Me.numKoj2_2.Value = "0"
            Me.numKoj2_3.Value = "0"
        End If

        '���̂̏ꍇ
        drTemp = dtValue.Select("eigyou_kbn='3'")
        If drTemp.Length > 0 Then
            Me.numKoj3_1.Value = Convert.ToString(drTemp(0).Item("koj_hantei_ritu"))    '�H�����藦
            Me.numKoj3_2.Value = Convert.ToString(drTemp(0).Item("koj_jyuchuu_ritu"))   '�H���󒍗�
            Me.numKoj3_3.Value = Convert.ToString(drTemp(0).Item("tyoku_koj_ritu"))     '���H����
        Else
            Me.numKoj3_1.Value = "0"
            Me.numKoj3_2.Value = "0"
            Me.numKoj3_3.Value = "0"
        End If

        'FC�̏ꍇ
        drTemp = dtValue.Select("eigyou_kbn='4'")
        If drTemp.Length > 0 Then
            Me.numKoj4_1.Value = Convert.ToString(drTemp(0).Item("koj_hantei_ritu"))    '�H�����藦
            Me.numKoj4_2.Value = Convert.ToString(drTemp(0).Item("koj_jyuchuu_ritu"))   '�H���󒍗�
            Me.numKoj4_3.Value = Convert.ToString(drTemp(0).Item("tyoku_koj_ritu"))     '���H����
        Else
            Me.numKoj4_1.Value = "0"
            Me.numKoj4_2.Value = "0"
            Me.numKoj4_3.Value = "0"
        End If

        '�S�̂̏ꍇ
        Me.numKoj5_1.Value = Convert.ToString(dtValue.Compute("SUM(koj_hantei_ritu)", ""))    '�H�����藦
        Me.numKoj5_2.Value = Convert.ToString(dtValue.Compute("SUM(koj_jyuchuu_ritu)", ""))   '�H���󒍗�
        Me.numKoj5_3.Value = Convert.ToString(dtValue.Compute("SUM(tyoku_koj_ritu)", ""))     '���H����

        '�S��FC�����̏ꍇ
        Me.numKoj6_1.Value = Convert.ToString(dtValue.Compute("SUM(koj_hantei_ritu)", "eigyou_kbn in ('1','2','3')"))    '�H�����藦
        Me.numKoj6_2.Value = Convert.ToString(dtValue.Compute("SUM(koj_jyuchuu_ritu)", "eigyou_kbn in ('1','2','3')"))    '�H�����藦
        Me.numKoj6_3.Value = Convert.ToString(dtValue.Compute("SUM(tyoku_koj_ritu)", "eigyou_kbn in ('1','2','3')"))    '�H�����藦

    End Sub

    ''' <summary>
    ''' �O���b�h�̃f�[�^��ݒ肷��
    ''' </summary>
    ''' <param name="dtValue">�f�[�^��</param>
    ''' <param name="strGridName">�O���b�h��</param>
    ''' <remarks></remarks>
    Private Sub SetGridData(ByVal dtValue As DataTable, ByVal strGridName As String)
        'EMAB��Q�Ή����̊i�[����
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name, dtValue)

        Dim dtAllValue As DataTable                 '���v���f�[�^
        Dim dtTemp As DataTable                     '�e�c�Ƌ敪�̃f�[�^
        Dim drTemp() As DataRow                     '����/�H���̃f�[�^
        Dim drValue As DataRow                      '�Վ��f�[�^
        Dim numSumL(9) As Long                      '���v���f�[�^(������)
        Dim numSumD(5) As Decimal                   '���v���f�[�^(������)
        Dim intDataCount As Integer                 '���R�[�h��

        '���R�[�h����ݒ肷��
        intDataCount = dtValue.Rows.Count

        '�O���̏ꍇ�A�u1�v�ɐݒ肷��
        If intDataCount = 0 Then
            intDataCount = 1
        End If

        dtAllValue = dtValue.Clone()

        '�������̃f�[�^����������
        dtTemp = dtValue.Clone()
        drTemp = dtValue.Select("bunbetu_cd='1'")
        For Each drValue In drTemp
            dtTemp.ImportRow(drValue)
        Next

        '�f�[�^�Đݒ�A�f�[�^�����̍l��(4�s�ɐݒ肷��)
        Call SetFormatDataTable(dtTemp, 4)

        CType(Me.FindControl(strGridName & "1"), DataList).DataSource = dtTemp
        CType(Me.FindControl(strGridName & "1"), DataList).DataBind()
        CType(Me.FindControl(strGridName & "2"), DataList).DataSource = dtTemp
        CType(Me.FindControl(strGridName & "2"), DataList).DataBind()

        '�H�����̃f�[�^����������
        dtTemp = dtValue.Clone()
        drTemp = dtValue.Select("bunbetu_cd='2'")
        For Each drValue In drTemp
            dtTemp.ImportRow(drValue)
        Next

        '�f�[�^�Đݒ�A�f�[�^�����̍l��(2�s�ɐݒ肷��)
        Call SetFormatDataTable(dtTemp, 2)

        CType(Me.FindControl(strGridName & "3"), DataList).DataSource = dtTemp
        CType(Me.FindControl(strGridName & "3"), DataList).DataBind()
        CType(Me.FindControl(strGridName & "4"), DataList).DataSource = dtTemp
        CType(Me.FindControl(strGridName & "4"), DataList).DataBind()

        '���̑����̃f�[�^����������
        dtTemp = dtValue.Clone()
        drTemp = dtValue.Select("bunbetu_cd='9'")
        For Each drValue In drTemp
            dtTemp.ImportRow(drValue)
        Next

        '�f�[�^�Đݒ�A�f�[�^�����̍l��(1�s�ɐݒ肷��)
        Call SetFormatDataTable(dtTemp, 1)

        CType(Me.FindControl(strGridName & "5"), DataList).DataSource = dtTemp
        CType(Me.FindControl(strGridName & "5"), DataList).DataBind()
        CType(Me.FindControl(strGridName & "6"), DataList).DataSource = dtTemp
        CType(Me.FindControl(strGridName & "6"), DataList).DataBind()

        '���v���̃f�[�^
        numSumL(0) = 0
        numSumL(1) = 0
        numSumL(2) = 0
        numSumL(3) = 0
        numSumL(4) = 0
        numSumL(5) = 0
        numSumL(6) = 0
        numSumL(7) = 0
        numSumL(8) = 0
        numSumL(9) = 0
        numSumD(0) = 0
        numSumD(1) = 0
        numSumD(2) = 0
        numSumD(3) = 0
        numSumD(4) = 0
        numSumD(5) = 0
        For Each drValue In dtValue.Rows
            numSumL(0) = numSumL(0) + GetLongFromObj(drValue("g_jittuseki_kingaku"))    '�O�N���ы��z
            numSumL(1) = numSumL(1) + GetLongFromObj(drValue("g_jittuseki_arari"))      '�O�N���ёe��

            numSumL(2) = numSumL(2) + GetLongFromObj(drValue("keikaku_kingaku"))        '�v����z
            numSumL(3) = numSumL(3) + GetLongFromObj(drValue("keikaku_arari"))          '�v��e��

            numSumL(4) = numSumL(4) + GetLongFromObj(drValue("mikomi_kingaku"))         '�������z
            numSumL(5) = numSumL(5) + GetLongFromObj(drValue("mikomi_arari"))           '�����e��

            numSumL(6) = numSumL(6) + GetLongFromObj(drValue("mikomi_keikaku_kingaku")) '����-�v����z
            numSumL(7) = numSumL(7) + GetLongFromObj(drValue("mikomi_keikaku_arari"))   '����-�v��e��

            numSumL(8) = numSumL(8) + GetLongFromObj(drValue("jittuseki_kingaku"))      '���ы��z
            numSumL(9) = numSumL(9) + GetLongFromObj(drValue("jittuseki_arari"))        '���ёe��

            numSumD(0) = numSumD(0) + GetDecimalFromObj(drValue("tasseiritu_kensuu"))   '�v��B��������
            numSumD(1) = numSumD(1) + GetDecimalFromObj(drValue("tasseiritu_kingaku"))  '�v��B�������z
            numSumD(2) = numSumD(2) + GetDecimalFromObj(drValue("tasseiritu_arari"))    '�v��B�����e��

            numSumD(3) = numSumD(3) + GetDecimalFromObj(drValue("sintyokuritu_kensuu")) '�����i��������
            numSumD(4) = numSumD(4) + GetDecimalFromObj(drValue("sintyokuritu_kingaku")) '�����i�������z
            numSumD(5) = numSumD(5) + GetDecimalFromObj(drValue("sintyokuritu_arari"))  '�����i�����e��
        Next

        drValue = dtAllValue.NewRow()
        '�V�K��FC�̏ꍇ�A�󔒂ɐݒ肷��
        If strGridName.Equals("grdSinkiMeisai") OrElse strGridName.Equals("grdFCMeisai") Then
            drValue("g_jittuseki_kingaku") = ""
            drValue("g_jittuseki_arari") = ""
        Else
            drValue("g_jittuseki_kingaku") = numSumL(0)
            drValue("g_jittuseki_arari") = numSumL(1)
        End If

        drValue("keikaku_kingaku") = numSumL(2)
        drValue("keikaku_arari") = numSumL(3)
        drValue("mikomi_kingaku") = numSumL(4)
        drValue("mikomi_arari") = numSumL(5)
        drValue("mikomi_keikaku_kingaku") = numSumL(6)
        drValue("mikomi_keikaku_arari") = numSumL(7)
        drValue("jittuseki_kingaku") = numSumL(8)
        drValue("jittuseki_arari") = numSumL(9)

        drValue("tasseiritu_kensuu") = GetDecimalFromObj(numSumD(0) / intDataCount)
        drValue("tasseiritu_kingaku") = GetDecimalFromObj(numSumD(1) / intDataCount)
        drValue("tasseiritu_arari") = GetDecimalFromObj(numSumD(2) / intDataCount)
        drValue("sintyokuritu_kensuu") = GetDecimalFromObj(numSumD(3) / intDataCount)
        drValue("sintyokuritu_kingaku") = GetDecimalFromObj(numSumD(4) / intDataCount)
        drValue("sintyokuritu_arari") = GetDecimalFromObj(numSumD(5) / intDataCount)

        dtAllValue.Rows.Add(drValue)

        CType(Me.FindControl(strGridName & "Sum"), DataList).DataSource = dtAllValue
        CType(Me.FindControl(strGridName & "Sum"), DataList).DataBind()
    End Sub

    ''' <summary>
    ''' �󔒍s��ǉ�����
    ''' </summary>
    ''' <param name="dtValue">�f�[�^�Ώ�</param>
    ''' <param name="intAddRowCount">�󔒍s��</param>
    ''' <remarks></remarks>
    Private Sub SetFormatDataTable(ByRef dtValue As DataTable, ByVal intAddRowCount As Integer)
        Dim i As Integer
        Dim j As Integer
        Dim dr As DataRow

        If Not dtValue Is Nothing Then
            If dtValue.Rows.Count < intAddRowCount Then
                For i = dtValue.Rows.Count To intAddRowCount - 1
                    dr = dtValue.NewRow()
                    For j = 0 To dtValue.Columns.Count - 1
                        dr(j) = DBNull.Value
                    Next

                    dtValue.Rows.Add(dr)
                Next
            End If
        End If
    End Sub

    ''' <summary>
    ''' ���l��߂�
    ''' </summary>
    ''' <param name="objValue">���l�Ώ�</param>
    ''' <returns>���l</returns>
    ''' <remarks></remarks>
    Private Function GetLongFromObj(ByVal objValue As Object) As Long
        Dim strValue As String

        strValue = Convert.ToString(objValue)

        '�f�[�^��NULL�̏ꍇ
        If strValue.Equals(String.Empty) Then
            Return 0
        End If

        If IsNumeric(strValue) Then
            Return Convert.ToInt64(strValue)
        Else
            Return 0
        End If
    End Function

    ''' <summary>
    ''' ���l��߂�
    ''' </summary>
    ''' <param name="objValue">���l�Ώ�</param>
    ''' <returns>���l</returns>
    ''' <remarks></remarks>
    Private Function GetDecimalFromObj(ByVal objValue As Object) As Decimal
        Dim strValue As String

        strValue = Convert.ToString(objValue)

        '�f�[�^��NULL�̏ꍇ
        If strValue.Equals(String.Empty) Then
            Return 0
        End If

        If IsNumeric(strValue) Then
            Return Convert.ToDecimal(strValue)
        Else
            Return 0
        End If
    End Function
#End Region

End Class
