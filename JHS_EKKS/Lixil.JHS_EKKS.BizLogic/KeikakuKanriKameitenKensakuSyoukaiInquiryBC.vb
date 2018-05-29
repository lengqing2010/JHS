Imports EMAB = Itis.ApplicationBlocks.ExceptionManagement.UnTrappedExceptionManager
Imports MyMethod = System.Reflection.MethodBase
Imports System.Transactions
Imports Lixil.JHS_EKKS.DataAccess

Public Class KeikakuKanriKameitenKensakuSyoukaiInquiryBC

    Private keikakuKanriKameitenKensakuSyoukaiInquiryDA As New DataAccess.KeikakuKanriKameitenKensakuSyoukaiInquiryDA

    ''' <summary>
    ''' �敪�����擾����
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>2013/09/02�@�ԗ�(��A���V�X�e����)�@�V�K�쐬</history>
    Public Function GetKbnInfo() As Data.DataTable

        'EMAB��Q�Ή����̊i�[����
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name)

        Return keikakuKanriKameitenKensakuSyoukaiInquiryDA.SelKbnInfo()

    End Function

    ''' <summary>
    ''' �Ǌ��x�X�����擾����
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>2013/09/02�@�ԗ�(��A���V�X�e����)�@�V�K�쐬</history>
    Public Function GetSitenInfo() As Data.DataTable

        'EMAB��Q�Ή����̊i�[����
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name)

        Return keikakuKanriKameitenKensakuSyoukaiInquiryDA.SelSitenInfo()

    End Function

    ''' <summary>
    ''' �s���{�������擾����
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>2013/09/02�@�ԗ�(��A���V�X�e����)�@�V�K�쐬</history>
    Public Function GetTodoufukenInfo() As Data.DataTable

        'EMAB��Q�Ή����̊i�[����
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name)

        Return keikakuKanriKameitenKensakuSyoukaiInquiryDA.SelTodoufukenInfo()

    End Function

    ''' <summary>
    ''' ���̏����擾����
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>2013/09/02�@�ԗ�(��A���V�X�e����)�@�V�K�쐬</history>
    Public Function GetMeisyouInfo(ByVal strMeisyouSyubetu As String) As Data.DataTable

        'EMAB��Q�Ή����̊i�[����
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name, strMeisyouSyubetu)

        Return keikakuKanriKameitenKensakuSyoukaiInquiryDA.SelMeisyouInfo(strMeisyouSyubetu)

    End Function

    ''' <summary>
    ''' �g�����̏����擾����
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>2013/09/02�@�ԗ�(��A���V�X�e����)�@�V�K�쐬</history>
    Public Function GetKakutyouMeisyouInfo(ByVal strMeisyouSyubetu As String) As Data.DataTable

        'EMAB��Q�Ή����̊i�[����
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name, strMeisyouSyubetu)

        Return keikakuKanriKameitenKensakuSyoukaiInquiryDA.SelKakutyouMeisyouInfo(strMeisyouSyubetu)

    End Function

    ''' <summary>
    ''' �����X���׏����擾����
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>2013/09/02�@�ԗ�(��A���V�X�e����)�@�V�K�쐬</history>
    Public Function GetKameitenInfo(ByVal dicPrm As Dictionary(Of String, String)) As Data.DataTable

        'EMAB��Q�Ή����̊i�[����
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name, dicPrm)

        Return keikakuKanriKameitenKensakuSyoukaiInquiryDA.SelKameitenInfo(dicPrm)

    End Function

    ''' <summary>
    ''' �����X���׌������擾����
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' <history>2013/09/02�@�ԗ�(��A���V�X�e����)�@�V�K�쐬</history>
    Public Function GetKameitenCount(ByVal dicPrm As Dictionary(Of String, String)) As Integer

        'EMAB��Q�Ή����̊i�[����
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name, dicPrm)

        Return keikakuKanriKameitenKensakuSyoukaiInquiryDA.SelKameitenCount(dicPrm)

    End Function

End Class
