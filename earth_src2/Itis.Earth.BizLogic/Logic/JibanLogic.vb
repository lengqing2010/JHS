Imports Itis.Earth.DataAccess
Imports System.Transactions
Imports System.Data
Imports System.Data.SqlClient
Imports System.Web.UI

''' <summary>
''' �n�Ջ��ʏ����N���X
''' </summary>
''' <remarks></remarks>
Public Class JibanLogic


#Region "������̃o�C�g�����擾�iShift-JIS�j"
    ''' <summary>
    ''' ������̃o�C�g�����擾�iShift-JIS�j
    ''' </summary>
    ''' <param name="str">�Ώە�����</param>
    ''' <returns>Integer�F�o�C�g��</returns>
    ''' <remarks></remarks>
    Public Function getStrByteSJIS(ByVal str As String) As Integer

        'Shift-JIS�ł̃o�C�g�����擾
        Return System.Text.Encoding.GetEncoding("Shift_JIS").GetByteCount(str)

    End Function
#End Region



End Class
