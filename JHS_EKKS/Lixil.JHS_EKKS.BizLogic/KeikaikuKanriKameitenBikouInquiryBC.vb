Imports EMAB = Itis.ApplicationBlocks.ExceptionManagement.UnTrappedExceptionManager
Imports MyMethod = System.Reflection.MethodBase
Imports System.Transactions
Imports Lixil.JHS_EKKS.DataAccess

Public Class KeikaikuKanriKameitenBikouInquiryBC

    Private keikaikuKanriKameitenBikouInquiryDA As New DataAccess.KeikaikuKanriKameitenBikouInquiryDA

    ''' <summary>
    ''' �����X���l���擾
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>2013/09/02�@�ԗ�(��A���V�X�e����)�@�V�K�쐬</history>
    Public Function GetBikouInfo(ByVal strKameitenCd As String) As Data.DataTable

        'EMAB��Q�Ή����̊i�[����
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name, _
                                                                strKameitenCd)

        Return keikaikuKanriKameitenBikouInquiryDA.SelBikouInfo(strKameitenCd)

    End Function

    ''' <summary>
    ''' �����X���l�X�V���擾
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>2013/09/02�@�ԗ�(��A���V�X�e����)�@�V�K�쐬</history>
    Public Function GetKameitenBikouMaxUpdTime(ByVal strKameitenCd As String) As Data.DataTable

        'EMAB��Q�Ή����̊i�[����
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name, _
                                                                strKameitenCd)

        Return keikaikuKanriKameitenBikouInquiryDA.SelKameitenBikouMaxUpdTime(strKameitenCd)

    End Function
        ''' <summary>
        ''' �����X��ʎ擾
        ''' </summary>
        ''' <param name="code"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
    Public Function Getkameitensyubetu(ByVal code As String) As String

        'EMAB��Q�Ή����̊i�[����
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name, _
                                                                code)

        Return keikaikuKanriKameitenBikouInquiryDA.Selkameitensyubetu(code)

    End Function

    ''' <summary>
    ''' ���l�ǉ�
    ''' </summary>
    ''' <param name="dicPrm">�ǉ��f�[�^</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SetInsBikou(ByVal dicPrm As Dictionary(Of String, String)) As Boolean

        'EMAB��Q�Ή����̊i�[����
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name, _
                                                                dicPrm)

        Return keikaikuKanriKameitenBikouInquiryDA.InsBikou(dicPrm)

    End Function

    ''' <summary>
    ''' ���l�X�V
    ''' </summary>
    ''' <param name="dicPrm">�ǉ��f�[�^</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SetUpdBikou(ByVal dicPrm As Dictionary(Of String, String)) As Boolean

        'EMAB��Q�Ή����̊i�[����
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name, _
                                                                dicPrm)

        Return keikaikuKanriKameitenBikouInquiryDA.UpdBikou(dicPrm)

    End Function

    ''' <summary>
    ''' ���l�폜
    ''' </summary>
    ''' <param name="dicPrm">�ǉ��f�[�^</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SetDelBikou(ByVal dicPrm As Dictionary(Of String, String)) As Boolean

        'EMAB��Q�Ή����̊i�[����
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name, _
                                                                dicPrm)

        Return keikaikuKanriKameitenBikouInquiryDA.DelBikou(dicPrm)

    End Function

End Class
