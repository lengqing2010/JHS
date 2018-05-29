Imports Itis.Earth.DataAccess
Imports System.Transactions

''' <summary>
''' �������Ϗ��쐬�w��
''' </summary>
''' <remarks></remarks>
''' <history>2013/11/13 ���F(��A���V�X�e����) �V�K�쐬</history>
Public Class TyousaMitumorisyoSakuseiInquiryLogic

    '�C���X�^���X����
    Private TyousaMitumorisyoSakuseiInquiryDA As New TyousaMitumorisyoSakuseiInquiryDataAccess

    ''' <summary>
    ''' �\���Z�� �I��
    ''' </summary>
    ''' <returns>DataTable</returns>
    ''' <remarks></remarks>
    ''' <history>2013/11/13 ���F(��A���V�X�e����) �V�K�쐬</history>
    Public Function GetJyuusyoInfo() As Data.DataTable

        '�f�[�^�擾
        Return TyousaMitumorisyoSakuseiInquiryDA.SelJyuusyoInfo()

    End Function

    ''' <summary>
    ''' �u���Ϗ��쐬�񐔁v���擾����
    ''' </summary>
    ''' <param name="strKbn">�敪</param>
    ''' <param name="strBultukennNo">���Ϗ��쐬��</param>
    ''' <returns>DataTable</returns>
    ''' <remarks></remarks>
    ''' <history>2013/11/13 ���F(��A���V�X�e����) �V�K�쐬</history>
    Public Function GetSakuseiKaisuu(ByVal strKbn As String, ByVal strBultukennNo As String) As Data.DataTable

        '�f�[�^�擾
        Return TyousaMitumorisyoSakuseiInquiryDA.SelSakuseiKaisuu(strKbn, strBultukennNo)

    End Function

    ''' <summary>
    ''' ���F�� �I��
    ''' </summary>
    ''' <returns>DataTable</returns>
    ''' <remarks></remarks>
    ''' <history>2013/11/13 ���F(��A���V�X�e����) �V�K�쐬</history>
    Public Function GetSyouninSyaInfo() As Data.DataTable

        '�f�[�^�擾
        Return TyousaMitumorisyoSakuseiInquiryDA.SelSyouninSyaInfo()

    End Function

    ''' <summary>
    ''' ���Ϗ��̑��݂𔻝Ђ���
    ''' </summary>
    ''' <param name="strKbn">�敪</param>
    ''' <param name="strBultukennNo">�ۏ؏�NO</param>
    ''' <returns>DataTable</returns>
    ''' <remarks></remarks>
    ''' <history>2013/11/13 ���F(��A���V�X�e����) �V�K�쐬</history>
    Public Function GetMitumoriCount(ByVal strKbn As String, ByVal strBultukennNo As String) As Data.DataTable

        '�f�[�^�擾
        Return TyousaMitumorisyoSakuseiInquiryDA.SelMitumoriCount(strKbn, strBultukennNo)

    End Function

    ''' <summary>
    ''' ���Ϗ��쐬�񐔂��X�V����
    ''' </summary>
    ''' <param name="strKbn">�敪</param>
    ''' <param name="strBultukennNo">�ۏ؏�NO</param>
    ''' <param name="inMitSakuseiKaisuu">���Ϗ��쐬��</param>
    ''' <param name="inSyouhizeiHyouji">����ŕ\��</param>
    ''' <param name="inMooruTenkaiFlg">���[���W�JFLG</param>
    ''' <param name="inHyoujiJyuusyoNo">�\���Z��_�Ǘ�No</param>
    ''' <param name="strTourokuId">�S����ID</param>
    ''' <param name="strTantousyaMei">�S���Җ�</param>
    ''' <param name="strSyouninsyaId">���F��ID</param>
    ''' <param name="strSyouninsyaMei">���F�Җ�</param>
    ''' <returns>True/False</returns>
    ''' <remarks></remarks>
    ''' <history>2013/11/15 ���F(��A���V�X�e����) �V�K�쐬</history>
    Public Function GetUpdMitumoriKaisu(ByVal strKbn As String, _
                                    ByVal strBultukennNo As String, _
                                    ByVal inMitSakuseiKaisuu As Integer, _
                                    ByVal inSyouhizeiHyouji As Integer, _
                                    ByVal inMooruTenkaiFlg As Integer, _
                                    ByVal inHyoujiJyuusyoNo As Integer, _
                                    ByVal strTourokuId As String, _
                                    ByVal strTantousyaMei As String, _
                                    ByVal strSyouninsyaId As String, _
                                        ByVal strSyouninsyaMei As String, _
                                        ByVal strSakuseiDate As String, _
                                        ByVal strIraiTantousyaMei As String) As Boolean

        '�f�[�^�擾
        Return TyousaMitumorisyoSakuseiInquiryDA.UpdMitumoriKaisu(strKbn, strBultukennNo, inMitSakuseiKaisuu, inSyouhizeiHyouji, _
                                                                  inMooruTenkaiFlg, inHyoujiJyuusyoNo, strTourokuId, strTantousyaMei, _
                                                                  strSyouninsyaId, strSyouninsyaMei, strSakuseiDate, strIraiTantousyaMei)

    End Function

    ''' <summary>
    ''' �u�������Ϗ��쐬�Ǘ��e�[�u���v�ɓo�^����
    ''' </summary>
    ''' <param name="strKbn">�敪</param>
    ''' <param name="strBultukennNo">�ۏ؏�NO</param>
    ''' <param name="inMitSakuseiKaisuu">���ύ쐬��</param>
    ''' <param name="inSyouhizeiHyouji">����ŕ\��</param>
    ''' <param name="inMooruTenkaiFlg">���[���W�JFLG</param>
    ''' <param name="inHyoujiJyuusyoNo">�\���Z��_�Ǘ�No</param>
    ''' <param name="strTourokuId">�S����ID</param>
    ''' <param name="strTantousyaMei">�S���Җ�</param>
    ''' <param name="strSyouninsyaId">���F��ID</param>
    ''' <param name="strSyouninsyaMei">���F�Җ�</param>
    ''' <param name="strSakuseiDate">�������Ϗ��쐬��</param>
    ''' <param name="strIraiTantousyaMei">�������Ϗ�_�˗��S����</param>
    ''' <returns>True/False</returns>
    ''' <remarks></remarks>
    ''' <history>2013/11/14 ���F(��A���V�X�e����) �V�K�쐬</history>
    Public Function GetInsMitumoriKaisu(ByVal strKbn As String, _
                                        ByVal strBultukennNo As String, _
                                        ByVal inMitSakuseiKaisuu As Integer, _
                                        ByVal inSyouhizeiHyouji As Integer, _
                                        ByVal inMooruTenkaiFlg As Integer, _
                                        ByVal inHyoujiJyuusyoNo As Integer, _
                                        ByVal strTourokuId As String, _
                                        ByVal strTantousyaMei As String, _
                                        ByVal strSyouninsyaId As String, _
                                        ByVal strSyouninsyaMei As String, _
                                        ByVal strSakuseiDate As String, _
                                        ByVal strIraiTantousyaMei As String) As Boolean

        '�f�[�^�擾
        Return TyousaMitumorisyoSakuseiInquiryDA.InsMitumoriKaisu(strKbn, strBultukennNo, inMitSakuseiKaisuu, inSyouhizeiHyouji, _
                                                                  inMooruTenkaiFlg, inHyoujiJyuusyoNo, strTourokuId, strTantousyaMei, _
                                                                  strSyouninsyaId, strSyouninsyaMei, strSakuseiDate, strIraiTantousyaMei)

    End Function

    ''' <summary>
    ''' �S����ID�̑��݂𔻝Ђ���
    ''' </summary>
    ''' <param name="strTantousyaId">�S����ID</param>
    ''' <returns>DataTable</returns>
    ''' <remarks></remarks>
    ''' <history>2013/11/15 ���F(��A���V�X�e����) �V�K�쐬</history>
    Public Function GetSonzaiHandan(ByVal strTantousyaId As String) As Data.DataTable

        '�f�[�^�擾
        Return TyousaMitumorisyoSakuseiInquiryDA.SelSonzaiHandan(strTantousyaId)

    End Function

    ''' <summary>
    ''' ���F�ҕR�t�����F��Ǘ��}�X�^����y���F��z���擾����
    ''' </summary>
    ''' <param name="strKbn">�敪</param>
    ''' <param name="strBultukennNo">�ۏ؏�NO</param>
    ''' <returns>DataTable</returns>
    ''' <remarks></remarks>
    ''' <history>2013/11/13 ���F(��A���V�X�e����) �V�K�쐬</history>
    Public Function GetSyouninIn(ByVal strKbn As String, ByVal strBultukennNo As String) As Data.DataTable

        '�f�[�^�擾
        Return TyousaMitumorisyoSakuseiInquiryDA.SelSyouninIn(strKbn, strBultukennNo)

    End Function

    ''' <summary>
    ''' �S���ҕR�t�����F��Ǘ��}�X�^����y���F��z���擾����
    ''' </summary>
    ''' <param name="strKbn">�敪</param>
    ''' <param name="strBultukennNo">�ۏ؏�NO</param>
    ''' <returns>DataTable</returns>
    ''' <remarks></remarks>
    ''' <history>2013/11/13 ���F(��A���V�X�e����) �V�K�쐬</history>
    Public Function GetTantouIn(ByVal strKbn As String, ByVal strBultukennNo As String) As Data.DataTable

        '�f�[�^�擾
        Return TyousaMitumorisyoSakuseiInquiryDA.SelTantouIn(strKbn, strBultukennNo)

    End Function

    ''' <summary>
    ''' [Fixed Data Section]ONE
    ''' </summary>
    ''' <param name="strKbn">�敪</param>
    ''' <param name="strBultukennNo">�ۏ؏�NO</param>
    ''' <returns>DataTable</returns>
    ''' <remarks></remarks>
    ''' <history>2013/11/19 ���F(��A���V�X�e����) �V�K�쐬</history>
    Public Function GetKihonInfoOne(ByVal strKbn As String, ByVal strBultukennNo As String) As Data.DataTable

        '�f�[�^�擾
        Return TyousaMitumorisyoSakuseiInquiryDA.SelKihonInfoOne(strKbn, strBultukennNo)

    End Function

    ''' <summary>
    ''' [Fixed Data Section]TWO
    ''' </summary>
    ''' <param name="strKbn">�敪</param>
    ''' <param name="strBultukennNo">�ۏ؏�NO</param>
    ''' <returns>DataTable</returns>
    ''' <remarks></remarks>
    ''' <history>2013/11/19 ���F(��A���V�X�e����) �V�K�쐬</history>
    Public Function GetKihonInfoTwo(ByVal strKbn As String, ByVal strBultukennNo As String) As Data.DataTable

        '�f�[�^�擾
        Return TyousaMitumorisyoSakuseiInquiryDA.SelKihonInfoTwo(strKbn, strBultukennNo)

    End Function

    ''' <summary>
    ''' �䌩�Ϗ��̃f�[�^
    ''' </summary>
    ''' <param name="strKbn">�敪</param>
    ''' <param name="strBultukennNo">�����ԍ�</param>
    ''' <param name="flg">�Ŕ��Ɛō��̋敪</param>
    ''' <returns>DataTable</returns>
    ''' <remarks></remarks>
    ''' <history>2013/11/19 ���F(��A���V�X�e����) �V�K�쐬</history>
    Public Function GetTyouhyouDate(ByVal strKbn As String, _
                                    ByVal strBultukennNo As String, _
                                    ByVal flg As String) As Data.DataTable

        '�f�[�^�擾
        Return TyousaMitumorisyoSakuseiInquiryDA.SelTyouhyouDate(strKbn, strBultukennNo, flg)

    End Function

    ''' <summary>
    ''' �u���[���W�J�v�̃Z�b�g�𔻝Ђ���
    ''' </summary>
    ''' <param name="strKbn">�敪</param>
    ''' <param name="strBultukennNo">�����ԍ�</param>
    ''' <returns>DataTable</returns>
    ''' <remarks></remarks>
    ''' <history>2013/12/02 ���F(��A���V�X�e����) �V�K�쐬</history>
    Public Function GetMoruHandan(ByVal strKbn As String, ByVal strBultukennNo As String) As Data.DataTable

        '�f�[�^�擾
        Return TyousaMitumorisyoSakuseiInquiryDA.SelMoruHandan(strKbn, strBultukennNo)

    End Function

    ''' <summary>
    ''' �ŗ���ǉ�����
    ''' </summary>
    ''' <returns>DataTable</returns>
    ''' <remarks></remarks>
    ''' <history>2014/02/17 ���F(��A���V�X�e����) �V�K�쐬</history>
    Public Function GetZeiritu(ByVal strKbn As String) As Data.DataTable

        '�f�[�^���擾����
        Return TyousaMitumorisyoSakuseiInquiryDA.SelZeiritu(strKbn)

    End Function

    ''' <summary>
    ''' �V�X�e������
    ''' </summary>
    ''' <returns></returns>
    Public Function GetSysTime() As Date

        Return TyousaMitumorisyoSakuseiInquiryDA.SelSysTime()

    End Function


    ''' <summary>
    ''' �u�������Ϗ�_�˗��S���ҁv���擾����
    ''' </summary>
    ''' <param name="strKbn">�敪</param>
    ''' <param name="strBultukennNo">�ۏ؏�NO</param>
    ''' <returns>DataTable</returns>
    ''' <remarks></remarks>
    Public Function GetIraiTantousya(ByVal strKbn As String, ByVal strBultukennNo As String) As Data.DataTable

        Return TyousaMitumorisyoSakuseiInquiryDA.SelIraiTantousya(strKbn, strBultukennNo)

    End Function

End Class
