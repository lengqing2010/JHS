Imports EMAB = Itis.ApplicationBlocks.ExceptionManagement.UnTrappedExceptionManager
Imports MyMethod = System.Reflection.MethodBase
Imports Microsoft.VisualBasic
Imports System.Web
Imports system.Web.HttpContext
Imports System.Configuration
Imports Lixil.JHS_EKKS.DataAccess

''' <summary>
''' �v��_�����X���Ɖ�
''' </summary>
''' <remarks></remarks>
Public Class KeikakuKameitenJyouhouInquiryBC

    Private KeikakuKameitenJyouhouInquiryDA As New DataAccess.KeikakuKameitenJyouhouInquiryDA

    ''' <summary>
    ''' ��{���f�[�^���擾����
    ''' </summary>
    ''' <param name="strKameitenCd">�����X�R�[�h</param>
    ''' <param name="strKeikakuNendo">�v��N�x</param>
    ''' <returns>DataTable</returns>
    ''' <remarks></remarks>
    ''' <history>2013/09/06�@���F(��A���V�X�e����)�@�V�K�쐬</history>
    Public Function GetKihonJyouSyutoku(ByVal strKameitenCd As String, ByVal strKeikakuNendo As String) As Data.DataTable

        'EMAB��Q�Ή����̊i�[����
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name, strKameitenCd, strKeikakuNendo)

        Return KeikakuKameitenJyouhouInquiryDA.SelKihonJyouSyutoku(strKameitenCd, strKeikakuNendo)

    End Function

    ''' <summary>
    ''' �v��Ǘ��p_�����X���f�[�^�擾
    ''' </summary>
    ''' <param name="strKameitenCd">�����X�R�[�h</param>
    ''' <returns>DataTable</returns>
    ''' <remarks></remarks>
    ''' <history>2013/09/09�@���F(��A���V�X�e����)�@�V�K�쐬</history>
    Public Function GetKameitenJyouhou(ByVal strKameitenCd As String, ByVal strKeikakuNendo As String) As Data.DataTable

        'EMAB��Q�Ή����̊i�[����
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name, strKameitenCd, strKeikakuNendo)

        Return KeikakuKameitenJyouhouInquiryDA.SelKameitenJyouhou(strKameitenCd, strKeikakuNendo)

    End Function

    ''' <summary>
    ''' ��ʂ̉����X���ނ��v��Ǘ�_�����X���}�X�^.�����X���ނɑ��݂��邩�ǂ����𔻒f����
    ''' </summary>
    ''' <param name="strKameitenCd">�����X�R�[�h</param>
    ''' <returns>DataTable</returns>
    ''' <remarks></remarks>
    ''' <history>2013/09/09�@���F(��A���V�X�e����)�@�V�K�쐬</history>
    Public Function GetCount(ByVal strKameitenCd As String) As Data.DataTable

        'EMAB��Q�Ή����̊i�[����
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name, strKameitenCd)

        Return KeikakuKameitenJyouhouInquiryDA.SelCount(strKameitenCd)

    End Function

    ''' <summary>
    ''' �u�v��Ǘ�_�����X���Ͻ��v�̒ǉ�����
    ''' </summary>
    ''' <param name="strKameitenCd">�����X����</param>
    ''' <param name="strKbn">�敪</param>
    ''' <param name="strKbnMei">�敪��</param>
    ''' <param name="strKeikakuyouKameitenmei">�v��p_�����X��</param>
    ''' <param name="inGyoutai">�Ƒ�</param>
    ''' <param name="strTouitsuHoujinCd">����@�l�R�[�h</param>
    ''' <param name="strHoujinCd">�@�l�R�[�h</param>
    ''' <param name="strKeikakuNayoseCd">�v�於��R�[�h</param>
    ''' <param name="inKeikakuyouNenkanTousuu">�v��p_�N�ԓ���</param>
    ''' <param name="strKeikakujiEigyouTantousyaId">�v�掞_�c�ƒS����</param>
    ''' <param name="strKeikakujiEigyouTantousyaMei">�v�掞_�c�ƒS���Җ�</param>
    ''' <param name="inKeikakuyouEigyouKbn">�v��p_�c�Ƌ敪</param>
    ''' <param name="strKeikakuyouKannkatsuSiten">�v��p_�Ǌ��x�X</param>
    ''' <param name="strKeikakuyouKannkatsuSitenMei">�v��p_�Ǌ��x�X��</param>
    ''' <param name="strSdsKaisiNengetu">SDS�J�n�N��</param>
    ''' <param name="inKameitenZokusei1">�����X����1</param>
    ''' <param name="inKameitenZokusei2">�����X����2</param>
    ''' <param name="inKameitenZokusei3">�����X����3</param>
    ''' <param name="strKameitenZokusei4">�����X����4</param>
    ''' <param name="strKameitenZokusei5">�����X����5</param>
    ''' <param name="strKameitenZokusei6">�����X����6</param>
    ''' <param name="strAddLoginUserId">�o�^���O�C�����[�U�[ID</param>
    ''' <returns>TRUE/FALSE</returns>
    ''' <remarks></remarks>
    ''' <history>2013/09/10�@���F(��A���V�X�e����)�@�V�K�쐬</history>
    Public Function GetInsMKeikakuKameitenInfo(ByVal strKameitenCd As String, ByVal strKbn As String, ByVal strKbnMei As String, _
                                               ByVal strKeikakuyouKameitenmei As String, ByVal inGyoutai As String, ByVal strTouitsuHoujinCd As String, _
                                               ByVal strHoujinCd As String, ByVal strKeikakuNayoseCd As String, ByVal inKeikakuyouNenkanTousuu As String, _
                                               ByVal strKeikakujiEigyouTantousyaId As String, ByVal strKeikakujiEigyouTantousyaMei As String, _
                                               ByVal inKeikakuyouEigyouKbn As String, ByVal strKeikakuyouKannkatsuSiten As String, _
                                               ByVal strKeikakuyouKannkatsuSitenMei As String, ByVal strSdsKaisiNengetu As String, _
                                               ByVal inKameitenZokusei1 As String, ByVal inKameitenZokusei2 As String, _
                                               ByVal inKameitenZokusei3 As String, ByVal strKameitenZokusei4 As String, _
                                               ByVal strKameitenZokusei5 As String, ByVal strKameitenZokusei6 As String, _
                                               ByVal strAddLoginUserId As String) As Boolean

        'EMAB��Q�Ή����̊i�[����
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name, strKameitenCd, strKbn, strKbnMei, _
                            strKeikakuyouKameitenmei, inGyoutai, strTouitsuHoujinCd, strHoujinCd, strKeikakuNayoseCd, inKeikakuyouNenkanTousuu, _
                            strKeikakujiEigyouTantousyaId, strKeikakujiEigyouTantousyaMei, inKeikakuyouEigyouKbn, strKeikakuyouKannkatsuSiten, _
                            strKeikakuyouKannkatsuSitenMei, strSdsKaisiNengetu, inKameitenZokusei1, inKameitenZokusei2, inKameitenZokusei3, _
                            strKameitenZokusei4, strKameitenZokusei5, strKameitenZokusei6, strAddLoginUserId)

        Return KeikakuKameitenJyouhouInquiryDA.InsMKeikakuKameitenInfo(strKameitenCd, strKbn, strKbnMei, _
                            strKeikakuyouKameitenmei, inGyoutai, strTouitsuHoujinCd, strHoujinCd, strKeikakuNayoseCd, inKeikakuyouNenkanTousuu, _
                            strKeikakujiEigyouTantousyaId, strKeikakujiEigyouTantousyaMei, inKeikakuyouEigyouKbn, strKeikakuyouKannkatsuSiten, _
                            strKeikakuyouKannkatsuSitenMei, strSdsKaisiNengetu, inKameitenZokusei1, inKameitenZokusei2, inKameitenZokusei3, _
                            strKameitenZokusei4, strKameitenZokusei5, strKameitenZokusei6, strAddLoginUserId)

    End Function

    ''' <summary>
    ''' �u�v��Ǘ�_�����X���Ͻ��v�̍X�V����
    ''' </summary>
    ''' <param name="strKameitenCd">�����X����</param>
    ''' <param name="strKbn">�敪</param>
    ''' <param name="strKbnMei">�敪��</param>
    ''' <param name="strKeikakuyouKameitenmei">�v��p_�����X��</param>
    ''' <param name="inGyoutai">�Ƒ�</param>
    ''' <param name="strTouitsuHoujinCd">����@�l�R�[�h</param>
    ''' <param name="strHoujinCd">�@�l�R�[�h</param>
    ''' <param name="strKeikakuNayoseCd">�v�於��R�[�h</param>
    ''' <param name="inKeikakuyouNenkanTousuu">�v��p_�N�ԓ���</param>
    ''' <param name="strKeikakujiEigyouTantousyaId">�v�掞_�c�ƒS����</param>
    ''' <param name="strKeikakujiEigyouTantousyaMei">�v�掞_�c�ƒS���Җ�</param>
    ''' <param name="inKeikakuyouEigyouKbn">�v��p_�c�Ƌ敪</param>
    ''' <param name="strKeikakuyouKannkatsuSiten">�v��p_�Ǌ��x�X</param>
    ''' <param name="strKeikakuyouKannkatsuSitenMei">�v��p_�Ǌ��x�X��</param>
    ''' <param name="strSdsKaisiNengetu">SDS�J�n�N��</param>
    ''' <param name="inKameitenZokusei1">�����X����1</param>
    ''' <param name="inKameitenZokusei2">�����X����2</param>
    ''' <param name="inKameitenZokusei3">�����X����3</param>
    ''' <param name="strKameitenZokusei4">�����X����4</param>
    ''' <param name="strKameitenZokusei5">�����X����5</param>
    ''' <param name="strKameitenZokusei6">�����X����6</param>
    ''' <param name="strUpdLoginUserId">�X�V���O�C�����[�U�[ID</param>
    ''' <returns>TRUE/FALSE</returns>
    ''' <remarks></remarks>
    ''' <history>2013/09/10�@���F(��A���V�X�e����)�@�V�K�쐬</history>
    Public Function GetUpdMKeikakuKameitenInfo(ByVal strKameitenCd As String, ByVal strKbn As String, ByVal strKbnMei As String, _
                                               ByVal strKeikakuyouKameitenmei As String, ByVal inGyoutai As String, ByVal strTouitsuHoujinCd As String, _
                                               ByVal strHoujinCd As String, ByVal strKeikakuNayoseCd As String, ByVal inKeikakuyouNenkanTousuu As String, _
                                               ByVal strKeikakujiEigyouTantousyaId As String, ByVal strKeikakujiEigyouTantousyaMei As String, _
                                               ByVal inKeikakuyouEigyouKbn As String, ByVal strKeikakuyouKannkatsuSiten As String, _
                                               ByVal strKeikakuyouKannkatsuSitenMei As String, ByVal strSdsKaisiNengetu As String, _
                                               ByVal inKameitenZokusei1 As String, ByVal inKameitenZokusei2 As String, _
                                               ByVal inKameitenZokusei3 As String, ByVal strKameitenZokusei4 As String, _
                                               ByVal strKameitenZokusei5 As String, ByVal strKameitenZokusei6 As String, _
                                               ByVal strUpdLoginUserId As String) As Boolean

        'EMAB��Q�Ή����̊i�[����
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name, strKameitenCd, strKbn, strKbnMei, _
                            strKeikakuyouKameitenmei, inGyoutai, strTouitsuHoujinCd, strHoujinCd, strKeikakuNayoseCd, inKeikakuyouNenkanTousuu, _
                            strKeikakujiEigyouTantousyaId, strKeikakujiEigyouTantousyaMei, inKeikakuyouEigyouKbn, strKeikakuyouKannkatsuSiten, _
                            strKeikakuyouKannkatsuSitenMei, strSdsKaisiNengetu, inKameitenZokusei1, inKameitenZokusei2, inKameitenZokusei3, _
                            strKameitenZokusei4, strKameitenZokusei5, strKameitenZokusei6, strUpdLoginUserId)

        Return KeikakuKameitenJyouhouInquiryDA.UpdMKeikakuKameitenInfo(strKameitenCd, strKbn, strKbnMei, _
                            strKeikakuyouKameitenmei, inGyoutai, strTouitsuHoujinCd, strHoujinCd, strKeikakuNayoseCd, inKeikakuyouNenkanTousuu, _
                            strKeikakujiEigyouTantousyaId, strKeikakujiEigyouTantousyaMei, inKeikakuyouEigyouKbn, strKeikakuyouKannkatsuSiten, _
                            strKeikakuyouKannkatsuSitenMei, strSdsKaisiNengetu, inKameitenZokusei1, inKameitenZokusei2, inKameitenZokusei3, _
                            strKameitenZokusei4, strKameitenZokusei5, strKameitenZokusei6, strUpdLoginUserId)

    End Function

    ''' <summary>
    ''' �v��Ǘ�_�����X���}�X�^�̔r������
    ''' </summary>
    ''' <param name="strKameitenCd">�����X�R�[�h</param>
    ''' <returns>DataTable</returns>
    ''' <remarks></remarks>
    ''' <history>2013/09/10�@���F(��A���V�X�e����)�@�V�K�쐬</history>
    Public Function GetKameitenMaxUpdTime(ByVal strKameitenCd As String) As Data.DataTable

        'EMAB��Q�Ή����̊i�[����
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name, _
                                                                strKameitenCd)

        Return KeikakuKameitenJyouhouInquiryDA.SelKameitenMaxUpdTime(strKameitenCd)

    End Function

    ''' <summary>
    ''' �c�ƒS���Җ����擾����
    ''' </summary>
    ''' <param name="strTantousyaId">�c�ƒS����ID</param>
    ''' <returns>DataTable</returns>
    ''' <remarks></remarks>
    ''' <history>2013/09/27�@���F(��A���V�X�e����)�@�V�K�쐬</history>
    Public Function GetEigyouTantousyaMei(ByVal strTantousyaId As String) As Data.DataTable

        'EMAB��Q�Ή����̊i�[����
        EMAB.AddMethodEntrance(MyClass.GetType.FullName & "." & MyMethod.GetCurrentMethod.Name, _
                                                                strTantousyaId)

        Return KeikakuKameitenJyouhouInquiryDA.SelEigyouTantousyaMei(strTantousyaId)

    End Function

End Class
