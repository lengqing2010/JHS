Imports Itis.Earth.DataAccess
Imports System.Data
Imports System.Data.SqlClient
''' <summary>�����挳�����[�o�͏���</summary>
''' <history>
''' <para>2010/07/14�@���Ǘz(��A���V�X�e����)�@�V�K�쐬</para>
''' </history>
Public Class SeikyuSakiMototyouOutputLogic

    ''' <summary>�����挳�����[�o�̓N���X�̃C���X�^���X���� </summary>
    Private seikyuSakiMototyouOutputDataAccess As New SeikyuSakiMototyouOutputDataAccess

#Region "�����挳��_�J�z�c���擾"
    ''' <summary>
    ''' �����挳��_�J�z�c���擾
    ''' </summary>
    ''' <param name="seikyuuSakiCd">������R�[�h</param>
    ''' <param name="seikyuuSakiBrc">������}��</param>
    ''' <param name="seikyuuSakiKbn">������敪</param>
    ''' <param name="fromDate">�N����FROM</param>
    ''' <returns>�J�z�c��(Long�^)</returns>
    ''' <remarks></remarks>
    Public Function GetKurikosiZandaData(ByVal seikyuuSakiCd As String, _
                                                      ByVal seikyuuSakiBrc As String, _
                                                      ByVal seikyuuSakiKbn As String, _
                                                      ByVal fromDate As String _
                                                      ) As Long


        Dim Data As Data.DataTable
        Dim retData As Long

        '�f�[�^�擾
        Data = seikyuSakiMototyouOutputDataAccess.SelKurikosiZandaData(seikyuuSakiCd, seikyuuSakiBrc, seikyuuSakiKbn, fromDate)

        ' �擾�o���Ȃ��ꍇ�A�󔒂�ԋp
        If Data Is Nothing OrElse IsDBNull(Data) Then
            retData = 0
        Else
            Try
                'Long�ɃL���X�g
                retData = CType(Data.Rows(0).Item("kurikosi_zan").ToString.Trim, Long)
            Catch ex As Exception
                '���s������[��
                retData = 0
            End Try
        End If

        '�l�߂�
        Return retData

    End Function
#End Region

    ''' <summary>����f�[�^�e�[�u���A�����f�[�^�e�[�u���f�[�^���擾����</summary>
    ''' <param name="strSeikyuuSakiCd"> ������R�[�h</param>
    ''' <param name="strSeikyuuSakiBrc"> ������}��</param>
    ''' <param name="strSeikyuuSakiKbn"> ������敪</param>
    ''' <param name="strFromDate"> ���o����FROM(YYYY/MM/DD)</param>
    ''' <param name="strToDate"> ���o����TO(YYYY/MM/DD)</param>
    ''' <returns>����A�����̃f�[�^</returns>
    Public Function GetUriageNyukinData(ByVal strSeikyuuSakiCd As String, ByVal strSeikyuuSakiBrc As String, ByVal strSeikyuuSakiKbn As String, ByVal strFromDate As String, ByVal strToDate As String) As Data.DataTable
        '�f�[�^�擾
        Return SeikyuSakiMototyouOutputDataAccess.SelUriageNyukinData(strSeikyuuSakiCd, strSeikyuuSakiBrc, strSeikyuuSakiKbn, strFromDate, strToDate)

    End Function

End Class
