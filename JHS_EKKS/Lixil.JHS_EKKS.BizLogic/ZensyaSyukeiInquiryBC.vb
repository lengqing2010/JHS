Option Explicit On
Option Strict On

Imports EMAB = Itis.ApplicationBlocks.ExceptionManagement.UnTrappedExceptionManager
Imports MyMethod = System.Reflection.MethodBase
Imports Itis.ApplicationBlocks.ExceptionManagement
Imports Itis.ApplicationBlocks.Data.SQLHelper
Imports Itis.ApplicationBlocks.Data
Imports System.Data
Imports System.Text

Public Class ZensyaSyukeiInquiryBC
    Private zensyaSyukeiInquiryDA As New DataAccess.ZensyaSyukeiInquiryDA
    
    '''' <summary>
    '''' �f�[�^�L��
    '''' </summary>
    '''' <param name="strKeikakuNendo">�v��_�N�x</param>
    '''' <returns>�N�x�f�[�^</returns>
    '''' <history>2012/11/30�@��~��(��A���V�X�e����)�@�V�K�쐬</history>
    'Public Function GetNendoData(ByVal strKeikakuNendo As String) As Data.DataTable
    '    'EMAB��Q�Ή����̊i�[����
    '    EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name, strKeikakuNendo)
    '    Return zensyaSyukeiInquiryDA.SelNendoData(strKeikakuNendo)
    'End Function

    ''' <summary>
    ''' 4���`3���̌v�挏���̏W�v�l�A�v����z�̏W�v�l�A�v��e���̏W�v�l
    ''' </summary>
    ''' <param name="strKeikakuNendo">�v��_�N�x</param>
    ''' <returns>�W�v�l�f�[�^</returns>
    ''' <history>2012/11/30�@��~��(��A���V�X�e����)�@�V�K�쐬</history>
    Public Function GetNendoKeikaku(ByVal strKeikakuNendo As String) As Data.DataTable

        'EMAB��Q�Ή����̊i�[����
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name, strKeikakuNendo)

        Return zensyaSyukeiInquiryDA.SelNendoKeikaku(strKeikakuNendo)

    End Function

    '''' <summary>
    '''' �f�[�^�L��
    '''' </summary>
    '''' <param name="strKeikakuNendo">�v��_�N�x</param>
    '''' <returns>�N�x�f�[�^</returns>
    '''' <history>2012/11/30�@��~��(��A���V�X�e����)�@�V�K�쐬</history>
    'Public Function GetNendoKensuuData(ByVal strKeikakuNendo As String) As Data.DataTable
    '    'EMAB��Q�Ή����̊i�[����
    '    EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name, strKeikakuNendo)
    '    Return zensyaSyukeiInquiryDA.SelNendoKensuuData(strKeikakuNendo)
    'End Function

    ''' <summary>
    ''' �I��N�x�ɉ������N�x�́u���ь����v
    ''' </summary>
    ''' <param name="strKeikakuNendo">�v��_�N�x</param>
    ''' <returns>���ь����W�v�l�f�[�^</returns>
    ''' <history>2012/11/30�@��~��(��A���V�X�e����)�@�V�K�쐬</history>
    Public Function GetNendoJissekiKensuu(ByVal strKeikakuNendo As String) As Data.DataTable

        'EMAB��Q�Ή����̊i�[����
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name, strKeikakuNendo)

        Return zensyaSyukeiInquiryDA.SelNendoJissekiKensuu(strKeikakuNendo)

    End Function

    ''' <summary>
    ''' ���ы��z�A���ёe���̏W�v�l
    ''' </summary>
    ''' <param name="strKeikakuNendo">�v��_�N�x</param>
    ''' <returns>���ы��z�A���ёe���̏W�v�l�̃f�[�^</returns>
    ''' <history>2012/11/30�@��~��(��A���V�X�e����)�@�V�K�쐬</history>
    Public Function GetNendoJisseki(ByVal strKeikakuNendo As String) As Data.DataTable

        'EMAB��Q�Ή����̊i�[����
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name, strKeikakuNendo)

        Return zensyaSyukeiInquiryDA.SelNendoJisseki(strKeikakuNendo)

    End Function

    ''' <summary>
    ''' ���Ԍv�挏���̏W�v�l�A�v����z�̏W�v�l�A�v��e���̏W�v�l
    ''' </summary>
    ''' <param name="strKeikakuNendo">�N�x</param>
    ''' <param name="strKikan">����</param>
    ''' <returns>�W�v�l�f�[�^</returns>
    ''' <history>2012/11/30�@��~��(��A���V�X�e����)�@�V�K�쐬</history>
    Public Function GetKikanKeikaku(ByVal strKeikakuNendo As String, ByVal strKikan As String) As Data.DataTable

        'EMAB��Q�Ή����̊i�[����
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name, strKeikakuNendo, strKikan)

        Return zensyaSyukeiInquiryDA.SelKikanKeikaku(strKeikakuNendo, strKikan)

    End Function

    ''' <summary>
    ''' ���Ԏ��ь����̏W�v�l
    ''' </summary>
    ''' <param name="strKeikakuNendo">�N�x</param>
    ''' <param name="strKikan">����</param>
    ''' <returns>�W�v�l�f�[�^</returns>
    ''' <history>2012/11/30�@��~��(��A���V�X�e����)�@�V�K�쐬</history>
    Public Function GetKikanJissekiKensuu(ByVal strKeikakuNendo As String, ByVal strKikan As String) As Data.DataTable

        'EMAB��Q�Ή����̊i�[����
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name, strKeikakuNendo, strKikan)

        Return zensyaSyukeiInquiryDA.SelKikanJissekiKensuu(strKeikakuNendo, strKikan)

    End Function

    ''' <summary>
    ''' ���Ԏ��ы��z�A�e���̏W�v�l
    ''' </summary>
    ''' <param name="strKeikakuNendo">�N�x</param>
    ''' <param name="strKikan">����</param>
    ''' <returns>�W�v�l�f�[�^</returns>
    ''' <history>2012/11/30�@��~��(��A���V�X�e����)�@�V�K�쐬</history>
    Public Function GetKikanJisseki(ByVal strKeikakuNendo As String, ByVal strKikan As String) As Data.DataTable

        'EMAB��Q�Ή����̊i�[����
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name, strKeikakuNendo, strKikan)

        Return zensyaSyukeiInquiryDA.SelKikanJisseki(strKeikakuNendo, strKikan)

    End Function

    ''' <summary>
    ''' �v�挏���A�v����z�A�v��e���̏W�v�l
    ''' </summary>
    ''' <param name="strKeikakuNendo">�N�x</param>
    ''' <param name="intBegin">������</param>
    ''' <param name="intEnd">���܂�</param>
    ''' <returns>�W�v�l�f�[�^</returns>
    ''' <history>2012/11/30�@��~��(��A���V�X�e����)�@�V�K�쐬</history>
    Public Function GetTukiKeikaku(ByVal strKeikakuNendo As String, ByVal intBegin As Integer, ByVal intEnd As Integer) As Data.DataTable

        'EMAB��Q�Ή����̊i�[����
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name, strKeikakuNendo, intBegin, intEnd)

        Return zensyaSyukeiInquiryDA.SelTukiKeikaku(strKeikakuNendo, intBegin, intEnd)

    End Function

    ''' <summary>
    ''' ���ь����̏W�v�l
    ''' </summary>
    ''' <param name="strKeikakuNendo">�N�x</param>
    ''' <param name="intBegin">������</param>
    ''' <param name="intEnd">���܂�</param>
    ''' <returns>�W�v�l�f�[�^</returns>
    ''' <history>2012/11/30�@��~��(��A���V�X�e����)�@�V�K�쐬</history>
    Public Function GetTukiJissekiKensuu(ByVal strKeikakuNendo As String, ByVal intBegin As Integer, ByVal intEnd As Integer) As Data.DataTable

        'EMAB��Q�Ή����̊i�[����
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name, strKeikakuNendo, intBegin, intEnd)

        Return zensyaSyukeiInquiryDA.SelTukiJissekiKensuu(strKeikakuNendo, intBegin, intEnd)

    End Function

    ''' <summary>
    ''' ���ы��z�A���ёe���̏W�v�l
    ''' </summary>
    ''' <param name="strKeikakuNendo">�N�x</param>
    ''' <param name="intBegin">������</param>
    ''' <param name="intEnd">���܂�</param>
    ''' <returns>�W�v�l�f�[�^</returns>
    ''' <history>2012/11/30�@��~��(��A���V�X�e����)�@�V�K�쐬</history>
    Public Function GetTukiJisseki(ByVal strKeikakuNendo As String, ByVal intBegin As Integer, ByVal intEnd As Integer) As Data.DataTable

        'EMAB��Q�Ή����̊i�[����
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name, strKeikakuNendo, intBegin, intEnd)

        Return zensyaSyukeiInquiryDA.SelTukiJisseki(strKeikakuNendo, intBegin, intEnd)

    End Function

End Class
