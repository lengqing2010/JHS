''' <summary>
''' ReportIf�o�^�E�X�V�f�[�^�ݒ�p�̃��R�[�h�N���X�ł�
''' </summary>
''' <remarks></remarks>
Public Class ReportIfRecord

#Region "�ڋq�ԍ�"
    ''' <summary>
    ''' �ڋq�ԍ�
    ''' </summary>
    ''' <remarks></remarks>
    Private strKokyakuNo As String
    ''' <summary>
    ''' �ڋq�ԍ�
    ''' </summary>
    ''' <value></value>
    ''' <returns> �ڋq�ԍ�</returns>
    ''' <remarks></remarks>
    Public Property KokyakuNo() As String
        Get
            Return strKokyakuNo
        End Get
        Set(ByVal value As String)
            strKokyakuNo = value
        End Set
    End Property
#End Region

#Region "�ڋq�ԍ�-�ǔ�(��)"
    ''' <summary>
    ''' �ڋq�ԍ�-�ǔ�(��)
    ''' </summary>
    ''' <remarks></remarks>
    Private intKokyakuBrc As Integer
    ''' <summary>
    ''' �ڋq�ԍ�-�ǔ�(��)
    ''' </summary>
    ''' <value></value>
    ''' <returns> �ڋq�ԍ�-�ǔ�(��)</returns>
    ''' <remarks></remarks>
    Public Property KokyakuBrc() As Integer
        Get
            Return intKokyakuBrc
        End Get
        Set(ByVal value As Integer)
            intKokyakuBrc = value
        End Set
    End Property
#End Region

#Region "�T�[�r�X�敪"
    ''' <summary>
    ''' �T�[�r�X�敪
    ''' </summary>
    ''' <remarks></remarks>
    Private intServiceKbn As Integer
    ''' <summary>
    ''' �T�[�r�X�敪
    ''' </summary>
    ''' <value></value>
    ''' <returns> �T�[�r�X�敪</returns>
    ''' <remarks></remarks>
    Public Property ServiceKbn() As Integer
        Get
            Return intServiceKbn
        End Get
        Set(ByVal value As Integer)
            intServiceKbn = value
        End Set
    End Property
#End Region

#Region "�ی��،��ԍ�"
    ''' <summary>
    ''' �ی��،��ԍ�
    ''' </summary>
    ''' <remarks></remarks>
    Private strSyoukenNo As String
    ''' <summary>
    ''' �ی��،��ԍ�
    ''' </summary>
    ''' <value></value>
    ''' <returns> �ی��،��ԍ�</returns>
    ''' <remarks></remarks>
    Public Property SyoukenNo() As String
        Get
            Return strSyoukenNo
        End Get
        Set(ByVal value As String)
            strSyoukenNo = value
        End Set
    End Property
#End Region

#Region "�������@"
    ''' <summary>
    ''' �������@
    ''' </summary>
    ''' <remarks></remarks>
    Private strTyousa As String
    ''' <summary>
    ''' �������@
    ''' </summary>
    ''' <value></value>
    ''' <returns> �������@</returns>
    ''' <remarks></remarks>
    Public Property Tyousa() As String
        Get
            Return strTyousa
        End Get
        Set(ByVal value As String)
            strTyousa = value
        End Set
    End Property
#End Region

#Region "�������@NO"
    ''' <summary>
    ''' �������@NO
    ''' </summary>
    ''' <remarks></remarks>
    Private intTyousaNo As Integer
    ''' <summary>
    ''' �������@NO
    ''' </summary>
    ''' <value></value>
    ''' <returns> �������@NO</returns>
    ''' <remarks></remarks>
    Public Property TyousaNo() As Integer
        Get
            Return intTyousaNo
        End Get
        Set(ByVal value As Integer)
            intTyousaNo = value
        End Set
    End Property
#End Region

#Region "�v�挚��"
    ''' <summary>
    ''' �v�挚��
    ''' </summary>
    ''' <remarks></remarks>
    Private strKeikaku As String
    ''' <summary>
    ''' �v�挚��
    ''' </summary>
    ''' <value></value>
    ''' <returns> �v�挚��</returns>
    ''' <remarks></remarks>
    Public Property Keikaku() As String
        Get
            Return strKeikaku
        End Get
        Set(ByVal value As String)
            strKeikaku = value
        End Set
    End Property
#End Region

#Region "�\��NO"
    ''' <summary>
    ''' �\��NO
    ''' </summary>
    ''' <remarks></remarks>
    Private intKouzouNo As Integer
    ''' <summary>
    ''' �\��NO
    ''' </summary>
    ''' <value></value>
    ''' <returns> �\��NO</returns>
    ''' <remarks></remarks>
    Public Property KouzouNo() As Integer
        Get
            Return intKouzouNo
        End Get
        Set(ByVal value As Integer)
            intKouzouNo = value
        End Set
    End Property
#End Region

#Region "�{�喼"
    ''' <summary>
    ''' �{�喼
    ''' </summary>
    ''' <remarks></remarks>
    Private strSesyuName As String
    ''' <summary>
    ''' �{�喼
    ''' </summary>
    ''' <value></value>
    ''' <returns> �{�喼</returns>
    ''' <remarks></remarks>
    Public Property SesyuName() As String
        Get
            Return strSesyuName
        End Get
        Set(ByVal value As String)
            strSesyuName = value
        End Set
    End Property
#End Region

#Region "�����Z��1(1�s��)"
    ''' <summary>
    ''' �����Z��1(1�s��)
    ''' </summary>
    ''' <remarks></remarks>
    Private strBknAdr1 As String
    ''' <summary>
    ''' �����Z��1(1�s��)
    ''' </summary>
    ''' <value></value>
    ''' <returns> �����Z��1(1�s��)</returns>
    ''' <remarks></remarks>
    Public Property BknAdr1() As String
        Get
            Return strBknAdr1
        End Get
        Set(ByVal value As String)
            strBknAdr1 = value
        End Set
    End Property
#End Region

#Region "�����Z��2(2�s��)"
    ''' <summary>
    ''' �����Z��2(2�s��)
    ''' </summary>
    ''' <remarks></remarks>
    Private strBknAdr2 As String
    ''' <summary>
    ''' �����Z��2(2�s��)
    ''' </summary>
    ''' <value></value>
    ''' <returns> �����Z��2(2�s��)</returns>
    ''' <remarks></remarks>
    Public Property BknAdr2() As String
        Get
            Return strBknAdr2
        End Get
        Set(ByVal value As String)
            strBknAdr2 = value
        End Set
    End Property
#End Region

#Region "������]��"
    ''' <summary>
    ''' ������]��
    ''' </summary>
    ''' <remarks></remarks>
    Private dateChousaHopeDate As DateTime
    ''' <summary>
    ''' ������]��
    ''' </summary>
    ''' <value></value>
    ''' <returns> ������]��</returns>
    ''' <remarks></remarks>
    Public Property ChousaHopeDate() As DateTime
        Get
            Return dateChousaHopeDate
        End Get
        Set(ByVal value As DateTime)
            dateChousaHopeDate = value
        End Set
    End Property
#End Region

#Region "������]��(���ԓ�)"
    ''' <summary>
    ''' ������]��(���ԓ�)
    ''' </summary>
    ''' <remarks></remarks>
    Private strChousaHopeTime As String
    ''' <summary>
    ''' ������]��(���ԓ�)
    ''' </summary>
    ''' <value></value>
    ''' <returns> ������]��(���ԓ�)</returns>
    ''' <remarks></remarks>
    Public Property ChousaHopeTime() As String
        Get
            Return strChousaHopeTime
        End Get
        Set(ByVal value As String)
            strChousaHopeTime = value
        End Set
    End Property
#End Region

#Region "�����˗��S����"
    ''' <summary>
    ''' �����˗��S����
    ''' </summary>
    ''' <remarks></remarks>
    Private strChousaTanto As String
    ''' <summary>
    ''' �����˗��S����
    ''' </summary>
    ''' <value></value>
    ''' <returns> �����˗��S����</returns>
    ''' <remarks></remarks>
    Public Property ChousaTanto() As String
        Get
            Return strChousaTanto
        End Get
        Set(ByVal value As String)
            strChousaTanto = value
        End Set
    End Property
#End Region

#Region "���������"
    ''' <summary>
    ''' ���������
    ''' </summary>
    ''' <remarks></remarks>
    Private strChousaTachiai As String
    ''' <summary>
    ''' ���������
    ''' </summary>
    ''' <value></value>
    ''' <returns> ���������</returns>
    ''' <remarks></remarks>
    Public Property ChousaTachiai() As String
        Get
            Return strChousaTachiai
        End Get
        Set(ByVal value As String)
            strChousaTachiai = value
        End Set
    End Property
#End Region

#Region "�����X�R�[�h"
    ''' <summary>
    ''' �����X�R�[�h
    ''' </summary>
    ''' <remarks></remarks>
    Private strKameiCd As String
    ''' <summary>
    ''' �����X�R�[�h
    ''' </summary>
    ''' <value></value>
    ''' <returns> �����X�R�[�h</returns>
    ''' <remarks></remarks>
    Public Property KameiCd() As String
        Get
            Return strKameiCd
        End Get
        Set(ByVal value As String)
            strKameiCd = value
        End Set
    End Property
#End Region

#Region "�����X��"
    ''' <summary>
    ''' �����X��
    ''' </summary>
    ''' <remarks></remarks>
    Private strKameiName As String
    ''' <summary>
    ''' �����X��
    ''' </summary>
    ''' <value></value>
    ''' <returns> �����X��</returns>
    ''' <remarks></remarks>
    Public Property KameiName() As String
        Get
            Return strKameiName
        End Get
        Set(ByVal value As String)
            strKameiName = value
        End Set
    End Property
#End Region

#Region "�����XTEL"
    ''' <summary>
    ''' �����XTEL
    ''' </summary>
    ''' <remarks></remarks>
    Private strKameiTel As String
    ''' <summary>
    ''' �����XTEL
    ''' </summary>
    ''' <value></value>
    ''' <returns> �����XTEL</returns>
    ''' <remarks></remarks>
    Public Property KameiTel() As String
        Get
            Return strKameiTel
        End Get
        Set(ByVal value As String)
            strKameiTel = value
        End Set
    End Property
#End Region

#Region "�����XFAX"
    ''' <summary>
    ''' �����XFAX
    ''' </summary>
    ''' <remarks></remarks>
    Private strKameiFax As String
    ''' <summary>
    ''' �����XFAX
    ''' </summary>
    ''' <value></value>
    ''' <returns> �����XFAX</returns>
    ''' <remarks></remarks>
    Public Property KameiFax() As String
        Get
            Return strKameiFax
        End Get
        Set(ByVal value As String)
            strKameiFax = value
        End Set
    End Property
#End Region

#Region "�����XMAIL"
    ''' <summary>
    ''' �����XMAIL
    ''' </summary>
    ''' <remarks></remarks>
    Private strKameiMail As String
    ''' <summary>
    ''' �����XMAIL
    ''' </summary>
    ''' <value></value>
    ''' <returns> �����XMAIL</returns>
    ''' <remarks></remarks>
    Public Property KameiMail() As String
        Get
            Return strKameiMail
        End Get
        Set(ByVal value As String)
            strKameiMail = value
        End Set
    End Property
#End Region

#Region "������ЃR�[�h"
    ''' <summary>
    ''' ������ЃR�[�h
    ''' </summary>
    ''' <remarks></remarks>
    Private strTyousaCd As String
    ''' <summary>
    ''' ������ЃR�[�h
    ''' </summary>
    ''' <value></value>
    ''' <returns> ������ЃR�[�h</returns>
    ''' <remarks></remarks>
    Public Property TyousaCd() As String
        Get
            Return strTyousaCd
        End Get
        Set(ByVal value As String)
            strTyousaCd = value
        End Set
    End Property
#End Region

#Region "������Ў��Ə��R�[�h"
    ''' <summary>
    ''' ������Ў��Ə��R�[�h
    ''' </summary>
    ''' <remarks></remarks>
    Private strTyousaBrc As String
    ''' <summary>
    ''' ������Ў��Ə��R�[�h
    ''' </summary>
    ''' <value></value>
    ''' <returns> ������Ў��Ə��R�[�h</returns>
    ''' <remarks></remarks>
    Public Property TyousaBrc() As String
        Get
            Return strTyousaBrc
        End Get
        Set(ByVal value As String)
            strTyousaBrc = value
        End Set
    End Property
#End Region

#Region "������Ж���"
    ''' <summary>
    ''' ������Ж���
    ''' </summary>
    ''' <remarks></remarks>
    Private strTyousaName As String
    ''' <summary>
    ''' ������Ж���
    ''' </summary>
    ''' <value></value>
    ''' <returns> ������Ж���</returns>
    ''' <remarks></remarks>
    Public Property TyousaName() As String
        Get
            Return strTyousaName
        End Get
        Set(ByVal value As String)
            strTyousaName = value
        End Set
    End Property
#End Region

#Region "���|�[�gSS���͐���̃��R�[�h�X�V�^�C���X�^���v"
    ''' <summary>
    ''' ���|�[�gSS���͐���̃��R�[�h�X�V�^�C���X�^���v
    ''' </summary>
    ''' <remarks></remarks>
    Private dateReportUpdateTime As DateTime
    ''' <summary>
    ''' ���|�[�gSS���͐���̃��R�[�h�X�V�^�C���X�^���v
    ''' </summary>
    ''' <value></value>
    ''' <returns> ���|�[�gSS���͐���̃��R�[�h�X�V�^�C���X�^���v</returns>
    ''' <remarks></remarks>
    Public Property ReportUpdateTime() As DateTime
        Get
            ' ���ݒ莞�̓V�X�e�����t��ԋp
            If dateReportUpdateTime = DateTime.MinValue Then
                dateReportUpdateTime = DateTime.Now
            End If

            Return dateReportUpdateTime

        End Get
        Set(ByVal value As DateTime)
            dateReportUpdateTime = value
        End Set
    End Property
#End Region

#Region "�i���f�[�^���M�X�e�[�^�X"
    ''' <summary>
    ''' �i���f�[�^���M�X�e�[�^�X
    ''' </summary>
    ''' <remarks></remarks>
    Private strSendSts As String = "00"
    ''' <summary>
    ''' �i���f�[�^���M�X�e�[�^�X
    ''' </summary>
    ''' <value></value>
    ''' <returns> �i���f�[�^���M�X�e�[�^�X</returns>
    ''' <remarks></remarks>
    Public Property SendSts() As String
        Get
            Return strSendSts
        End Get
        Set(ByVal value As String)
            strSendSts = value
        End Set
    End Property
#End Region

#Region "�i���f�[�^��M�X�e�[�^�X"
    ''' <summary>
    ''' �i���f�[�^��M�X�e�[�^�X
    ''' </summary>
    ''' <remarks></remarks>
    Private strRecvSts As String = "00"
    ''' <summary>
    ''' �i���f�[�^��M�X�e�[�^�X
    ''' </summary>
    ''' <value></value>
    ''' <returns> �i���f�[�^��M�X�e�[�^�X</returns>
    ''' <remarks></remarks>
    Public Property RecvSts() As String
        Get
            Return strRecvSts
        End Get
        Set(ByVal value As String)
            strRecvSts = value
        End Set
    End Property
#End Region

#Region "PDF��M�X�e�[�^�X"
    ''' <summary>
    ''' PDF��M�X�e�[�^�X
    ''' </summary>
    ''' <remarks></remarks>
    Private strPdfSts As String = "00"
    ''' <summary>
    ''' PDF��M�X�e�[�^�X
    ''' </summary>
    ''' <value></value>
    ''' <returns> PDF��M�X�e�[�^�X</returns>
    ''' <remarks></remarks>
    Public Property PdfSts() As String
        Get
            Return strPdfSts
        End Get
        Set(ByVal value As String)
            strPdfSts = value
        End Set
    End Property
#End Region

#Region "�����A����_����"
    ''' <summary>
    ''' �����A����_����
    ''' </summary>
    ''' <remarks></remarks>
    Private strTysRenrakusakiAtesakiMei As String
    ''' <summary>
    ''' �����A����_����
    ''' </summary>
    ''' <value></value>
    ''' <returns> �����A����_����</returns>
    ''' <remarks></remarks>
    Public Property TysRenrakusakiAtesakiMei() As String
        Get
            Return strTysRenrakusakiAtesakiMei
        End Get
        Set(ByVal value As String)
            strTysRenrakusakiAtesakiMei = value
        End Set
    End Property
#End Region

#Region "�����A����_TEL"
    ''' <summary>
    ''' �����A����_TEL
    ''' </summary>
    ''' <remarks></remarks>
    Private strTysRenrakusakiTel As String
    ''' <summary>
    ''' �����A����_TEL
    ''' </summary>
    ''' <value></value>
    ''' <returns> �����A����_TEL</returns>
    ''' <remarks></remarks>
    Public Property TysRenrakusakiTel() As String
        Get
            Return strTysRenrakusakiTel
        End Get
        Set(ByVal value As String)
            strTysRenrakusakiTel = value
        End Set
    End Property
#End Region

#Region "�����A����_FAX"
    ''' <summary>
    ''' �����A����_FAX
    ''' </summary>
    ''' <remarks></remarks>
    Private strTysRenrakusakiFax As String
    ''' <summary>
    ''' �����A����_FAX
    ''' </summary>
    ''' <value></value>
    ''' <returns> �����A����_FAX</returns>
    ''' <remarks></remarks>
    Public Property TysRenrakusakiFax() As String
        Get
            Return strTysRenrakusakiFax
        End Get
        Set(ByVal value As String)
            strTysRenrakusakiFax = value
        End Set
    End Property
#End Region

#Region "�����A����_MAIL"
    ''' <summary>
    ''' �����A����_MAIL
    ''' </summary>
    ''' <remarks></remarks>
    Private strTysRenrakusakiMail As String
    ''' <summary>
    ''' �����A����_MAIL
    ''' </summary>
    ''' <value></value>
    ''' <returns> �����A����_MAIL</returns>
    ''' <remarks></remarks>
    Public Property TysRenrakusakiMail() As String
        Get
            Return strTysRenrakusakiMail
        End Get
        Set(ByVal value As String)
            strTysRenrakusakiMail = value
        End Set
    End Property
#End Region

#Region "�����A����_�S����"
    ''' <summary>
    ''' �����A����_�S����
    ''' </summary>
    ''' <remarks></remarks>
    Private strTysRenrakusakiTantouMei As String
    ''' <summary>
    ''' �����A����_�S����
    ''' </summary>
    ''' <value></value>
    ''' <returns> �����A����_�S����</returns>
    ''' <remarks></remarks>
    Public Property TysRenrakusakiTantouMei() As String
        Get
            Return strTysRenrakusakiTantouMei
        End Get
        Set(ByVal value As String)
            strTysRenrakusakiTantouMei = value
        End Set
    End Property
#End Region

#Region "�����X�J�i���̂P"
    ''' <summary>
    ''' �����X�J�i���̂P
    ''' </summary>
    ''' <remarks></remarks>
    Private strKameitenmeiKana1 As String
    ''' <summary>
    ''' �����X�J�i���̂P
    ''' </summary>
    ''' <value></value>
    ''' <returns> �����X�J�i���̂P</returns>
    ''' <remarks></remarks>
    Public Property KameitenmeiKana1() As String
        Get
            Return strKameitenmeiKana1
        End Get
        Set(ByVal value As String)
            strKameitenmeiKana1 = value
        End Set
    End Property
#End Region

#Region "�\��"
    ''' <summary>
    ''' �\��
    ''' </summary>
    ''' <remarks></remarks>
    Private strKouzou As String
    ''' <summary>
    ''' �\��
    ''' </summary>
    ''' <value></value>
    ''' <returns> �\��</returns>
    ''' <remarks></remarks>
    Public Property Kouzou() As String
        Get
            Return strKouzou
        End Get
        Set(ByVal value As String)
            strKouzou = value
        End Set
    End Property
#End Region

#Region "�K�w"
    ''' <summary>
    ''' �K�w
    ''' </summary>
    ''' <remarks></remarks>
    Private strKaisou As String
    ''' <summary>
    ''' �K�w
    ''' </summary>
    ''' <value></value>
    ''' <returns> �K�w</returns>
    ''' <remarks></remarks>
    Public Property Kaisou() As String
        Get
            Return strKaisou
        End Get
        Set(ByVal value As String)
            strKaisou = value
        End Set
    End Property
#End Region

#Region "�݌v���e�x����"
    ''' <summary>
    ''' �݌v���e�x����
    ''' </summary>
    ''' <remarks></remarks>
    Private deciSekkeiKyoyouSijiryoku As Decimal
    ''' <summary>
    ''' �݌v���e�x����
    ''' </summary>
    ''' <value></value>
    ''' <returns> �݌v���e�x����</returns>
    ''' <remarks></remarks>
    Public Property SekkeiKyoyouSijiryoku() As Decimal
        Get
            Return deciSekkeiKyoyouSijiryoku
        End Get
        Set(ByVal value As Decimal)
            deciSekkeiKyoyouSijiryoku = value
        End Set
    End Property
#End Region

#Region "���؂�[��"
    ''' <summary>
    ''' ���؂�[��
    ''' </summary>
    ''' <remarks></remarks>
    Private deciNegiriHukasa As Decimal
    ''' <summary>
    ''' ���؂�[��
    ''' </summary>
    ''' <value></value>
    ''' <returns> ���؂�[��</returns>
    ''' <remarks></remarks>
    Public Property NegiriHukasa() As Decimal
        Get
            Return deciNegiriHukasa
        End Get
        Set(ByVal value As Decimal)
            deciNegiriHukasa = value
        End Set
    End Property
#End Region

#Region "�\���b��"
    ''' <summary>
    ''' �\���b��
    ''' </summary>
    ''' <remarks></remarks>
    Private strYoteiKsMei As String
    ''' <summary>
    ''' �\���b��
    ''' </summary>
    ''' <value></value>
    ''' <returns> �\���b��</returns>
    ''' <remarks></remarks>
    Public Property YoteiKsMei() As String
        Get
            Return strYoteiKsMei
        End Get
        Set(ByVal value As String)
            strYoteiKsMei = value
        End Set
    End Property
#End Region

#Region "�Ԍ�"
    ''' <summary>
    ''' �Ԍ�
    ''' </summary>
    ''' <remarks></remarks>
    Private strSyako As String
    ''' <summary>
    ''' �Ԍ�
    ''' </summary>
    ''' <value></value>
    ''' <returns> �Ԍ�</returns>
    ''' <remarks></remarks>
    Public Property Syako() As String
        Get
            Return strSyako
        End Get
        Set(ByVal value As String)
            strSyako = value
        End Set
    End Property
#End Region

#Region "�\�萷�y����"
    ''' <summary>
    ''' �\�萷�y����
    ''' </summary>
    ''' <remarks></remarks>
    Private deciYoteiMoritutiAtusa As Decimal
    ''' <summary>
    ''' �\�萷�y����
    ''' </summary>
    ''' <value></value>
    ''' <returns> �\�萷�y����</returns>
    ''' <remarks></remarks>
    Public Property YoteiMoritutiAtusa() As Decimal
        Get
            Return deciYoteiMoritutiAtusa
        End Get
        Set(ByVal value As Decimal)
            deciYoteiMoritutiAtusa = value
        End Set
    End Property
#End Region

#Region "�����˗�����"
    ''' <summary>
    ''' �����˗�����
    ''' </summary>
    ''' <remarks></remarks>
    Private intDoujiIraiTousuu As Integer
    ''' <summary>
    ''' �����˗�����
    ''' </summary>
    ''' <value></value>
    ''' <returns> �����˗�����</returns>
    ''' <remarks></remarks>
    Public Property DoujiIraiTousuu() As Integer
        Get
            Return intDoujiIraiTousuu
        End Get
        Set(ByVal value As Integer)
            intDoujiIraiTousuu = value
        End Set
    End Property
#End Region

#Region "�����p�r��"
    ''' <summary>
    ''' �����p�r��
    ''' </summary>
    ''' <remarks></remarks>
    Private strTatemonoYoutoMei As String
    ''' <summary>
    ''' �����p�r��
    ''' </summary>
    ''' <value></value>
    ''' <returns> �����p�r��</returns>
    ''' <remarks></remarks>
    Public Property TatemonoYoutoMei() As String
        Get
            Return strTatemonoYoutoMei
        End Get
        Set(ByVal value As String)
            strTatemonoYoutoMei = value
        End Set
    End Property
#End Region

#Region "�o�R"
    ''' <summary>
    ''' �o�R
    ''' </summary>
    ''' <remarks></remarks>
    Private intKeiyu As Integer
    ''' <summary>
    ''' �o�R
    ''' </summary>
    ''' <value></value>
    ''' <returns> �o�R</returns>
    ''' <remarks></remarks>
    Public Property Keiyu() As Integer
        Get
            Return intKeiyu
        End Get
        Set(ByVal value As Integer)
            intKeiyu = value
        End Set
    End Property
#End Region

#Region "�I�v�V����"
    ''' <summary>
    ''' �I�v�V����
    ''' </summary>
    ''' <remarks></remarks>
    Private strOptions As String = String.Empty
    ''' <summary>
    ''' �I�v�V����
    ''' </summary>
    ''' <value></value>
    ''' <returns>�I�v�V����</returns>
    ''' <remarks></remarks>
    Public Property Options() As String
        Get
            Return strOptions
        End Get
        Set(ByVal value As String)
            strOptions = value
        End Set
    End Property
#End Region

End Class