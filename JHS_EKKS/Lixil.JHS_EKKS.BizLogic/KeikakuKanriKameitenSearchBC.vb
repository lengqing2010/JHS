Imports EMAB = Itis.ApplicationBlocks.ExceptionManagement.UnTrappedExceptionManager
Imports MyMethod = System.Reflection.MethodBase
Imports System.Transactions
Imports Lixil.JHS_EKKS.DataAccess


''' <summary>
''' �v��Ǘ�_�����X�@����POPUP
''' </summary>
''' <remarks></remarks>
Public Class KeikakuKanriKameitenSearchBC

    Private KeikakuKanriKameitenSearchDA As New DataAccess.KeikakuKanriKameitenSearchDA

    ''' <summary>
    ''' �u�����X�R�[�h�v�A�u�����X���v�A�u�s���{�����v�̌�����������
    ''' </summary>
    ''' <param name="strRows">�����������</param>
    ''' <param name="strYear">�N�x</param>
    ''' <param name="strKameitenCd">�����X�R�[�h</param>
    ''' <param name="strKameitenMei">�����X��</param>
    ''' <param name="blnTorikesi">���</param>
    ''' <returns>DataTable</returns>
    ''' <remarks></remarks>
    ''' <history>2012/11/19�@���F(��A���V�X�e����)�@�V�K�쐬</history>
    Public Function GetKeikakuKanriKameiten(ByVal strRows As String, _
                                            ByVal strYear As String, _
                                            ByVal strKameitenCd As String, _
                                            ByVal strKameitenMei As String, _
                                            ByVal blnTorikesi As Boolean, Optional ByVal blnAimai As Boolean = True) As Data.DataTable

        'EMAB��Q�Ή����̊i�[����
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name, _
                                                                                          strRows, _
                                                                                          strYear, _
                                                                                          strKameitenCd, _
                                                                                          strKameitenMei, _
                                                                                          blnTorikesi)

        Return KeikakuKanriKameitenSearchDA.SelKeikakuKanriKameiten(strRows, strYear, strKameitenCd, strKameitenMei, blnTorikesi, blnAimai)

    End Function

    ''' <summary>
    ''' ���������f�[�^�������擾����
    ''' </summary>
    ''' <param name="strYear">�N�x</param>
    ''' <param name="strKameitenCd">�����X�R�[�h</param>
    ''' <param name="strKameitenMei">�����X��</param>
    ''' <param name="blnTorikesi">���</param>
    ''' <returns>DataTable</returns>
    ''' <remarks></remarks>
    ''' <history>2012/11/19�@���F(��A���V�X�e����)�@�V�K�쐬</history>
    Public Function GetKeikakuKanriKameitenCount(ByVal strYear As String, _
                                                 ByVal strKameitenCd As String, _
                                                 ByVal strKameitenMei As String, _
                                                 ByVal blnTorikesi As Boolean) As Data.DataTable

        'EMAB��Q�Ή����̊i�[����
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name, _
                                                                                          strYear, _
                                                                                          strKameitenCd, _
                                                                                          strKameitenMei, _
                                                                                          blnTorikesi)

        Return KeikakuKanriKameitenSearchDA.SelKeikakuKanriKameitenCount(strYear, strKameitenCd, strKameitenMei, blnTorikesi)

    End Function

End Class
