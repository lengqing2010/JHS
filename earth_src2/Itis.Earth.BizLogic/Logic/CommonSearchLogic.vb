Imports Itis.Earth.DataAccess
Public Class CommonSearchLogic
    ''' <summary> ���C�����j���[�N���X�̃C���X�^���X���� </summary>
    Private CommonSearchDA As New CommonSearchDataAccess
    ''' <summary>�X�֔ԍ��f�[�^���擾����</summary>
    Public Function SelYuubinInfo(ByVal intRows As String, _
                                        ByVal strYuubinNo As String, _
                                        ByVal strYuubinMei As String) As CommonSearchDataSet.BirudaTableDataTable
        Return CommonSearchDA.SelYuubinInfo(intRows, strYuubinNo, strYuubinMei)
    End Function
    '''<summary>�X�֔ԍ����R�[�h�s�����擾����</summary>
    Public Function SelYuubinCount(ByVal strYuubinNo As String, _
                                            ByVal strYuubinMei As String) As Integer
        Return CommonSearchDA.SelYuubinCount(strYuubinNo, strYuubinMei)
    End Function
    '''<summary>���[�U�[�f�[�^���擾����</summary>
    Public Function GetUserInfo(ByVal intRows As String, _
                                            ByVal strUserId As String, _
                                            ByVal strUserMei As String, _
                                            ByVal blnDelete As Boolean) As CommonSearchDataSet.BirudaTableDataTable
        Return CommonSearchDA.SelUserInfo(intRows, strUserId, strUserMei, blnDelete)
    End Function
    '''<summary>�c�ƃf�[�^���擾����</summary>
    Public Function GetEigyouInfo(ByVal strUserId As String) As CommonSearchDataSet.BirudaTableDataTable
        Return CommonSearchDA.SelEigyouInfo(strUserId)
    End Function
    '''<summary>���[�U�[���R�[�h�s�����擾����</summary>
    Public Function GetUserCount(ByVal strUserId As String, _
                                            ByVal strUserMei As String, _
                                            ByVal blnDelete As Boolean) As Integer
        Return CommonSearchDA.SelUserCount(strUserId, strUserMei, blnDelete)
    End Function

    '''<summary>�n��f�[�^���擾����</summary>
    Public Function GetKojKaisyaKensakuInfo(ByVal intRows As String, _
                                    ByVal strCd As String, _
                                    ByVal strMei As String, _
                                    ByVal blnDelete As Boolean) As DataTable
        Return CommonSearchDA.SelKojKaisyaKensakuInfo(intRows, _
                                      strCd, _
                                      strMei, _
                                      blnDelete)

    End Function
    '''<summary>�c�Ə����R�[�h�s�����擾����</summary>
    Public Function GetKojKaisyaKensakuCount(ByVal strCd As String, _
                                        ByVal strMei As String, _
                                        ByVal blnDelete As Boolean) As Long
        Return CommonSearchDA.SelKojKaisyaKensakuCount(strCd, strMei, blnDelete)
    End Function
    '''<summary>�c�Ə��f�[�^���擾����</summary>
    Public Function GetEigyousyoInfo(ByVal intRows As String, _
                                    ByVal strEigyousyoCd As String, _
                                    ByVal strEigyousyoMei As String, _
                                    ByVal blnDelete As Boolean) As CommonSearchDataSet.EigyousyoTableDataTable
        Return CommonSearchDA.SelEigyousyoInfo(intRows, strEigyousyoCd, strEigyousyoMei, blnDelete)
    End Function
    '''<summary>�c�Ə����R�[�h�s�����擾����</summary>
    Public Function GetEigyousyoCount(ByVal strEigyousyoCd As String, _
                                        ByVal strEigyousyoMei As String, _
                                        ByVal blnDelete As Boolean) As Long
        Return CommonSearchDA.SelEigyousyoCount(strEigyousyoCd, strEigyousyoMei, blnDelete)
    End Function
    '''<summary>�����X�f�[�^���擾����</summary>
    Public Function GetKameitenKensakuInfo(ByVal intRows As String, _
                                        ByVal strKubun As String, _
                                        ByVal strKameitenCd As String, _
                                        ByVal strKameitenKana As String, _
                                        ByVal blnDelete As Boolean) As CommonSearchDataSet.KameitenSearchTableDataTable
        Return CommonSearchDA.SelKameitenKensakuInfo(intRows, _
                                      strKubun, _
                                      strKameitenCd, _
                                      strKameitenKana, _
                                      blnDelete)

    End Function
    '''<summary>�����X���R�[�h�s�����擾����</summary>
    Public Function GetKameitenKensakuCount(ByVal strKubun As String, _
                                      ByVal strKameitenCd As String, _
                                      ByVal strKameitenKana As String, _
                                      ByVal blnDelete As Boolean) As Integer
        Return CommonSearchDA.SelKameitenKensakuCount(strKubun, _
                                      strKameitenCd, _
                                      strKameitenKana, _
                                      blnDelete)
    End Function



    '''<summary>�n��f�[�^���擾����</summary>
    Public Function GetKeiretuKensakuInfo(ByVal intRows As String, _
                                      ByVal strKubun As String, _
                                      ByVal strKeiretuCd As String, _
                                      ByVal strKeiretuMei As String, _
                                      ByVal blnDelete As Boolean) As CommonSearchDataSet.KeiretuTableDataTable
        Return CommonSearchDA.SelKeiretuKensakuInfo(intRows, _
                                      strKubun, _
                                      strKeiretuCd, _
                                      strKeiretuMei, _
                                      blnDelete)

    End Function
    '''<summary>�n�񃌃R�[�h�s�����擾����</summary>
    Public Function GetKeiretuKensakuCount(ByVal strKubun As String, _
                                      ByVal strKeiretuCd As String, _
                                      ByVal strKeiretuMei As String, _
                                      ByVal blnDelete As Boolean) As Integer
        Return CommonSearchDA.SelKeiretuKensakuCount(strKubun, _
                                      strKeiretuCd, _
                                      strKeiretuMei, _
                                      blnDelete)
    End Function
    '''<summary>���i�f�[�^���擾����</summary>
    Public Function GetSyouhinInfo(ByVal intRows As String, _
                                    ByVal strSyouhinCd As String, _
                                    ByVal strSyouhinMei As String, _
                                     ByVal soukoCd As String, _
                                    Optional ByVal blnDelete As String = "") As CommonSearchDataSet.SyouhinTableDataTable
        Return CommonSearchDA.SelSyouhinInfo(intRows, strSyouhinCd, strSyouhinMei, soukoCd, blnDelete)
    End Function
    '''<summary>���i���R�[�h�s�����擾����</summary>
    Public Function GetSyouhinCount(ByVal strSyouhinCd As String, _
                                    ByVal strSyouhinMei As String, _
                                    ByVal soukoCd As String, _
                                    Optional ByVal blnDelete As String = "") As Long
        Return CommonSearchDA.SelSyouhinCount(strSyouhinCd, strSyouhinMei, soukoCd, blnDelete)
    End Function

    '==================2011/05/11 �ԗ� �����������\���ύX �ǉ� �J�n��==========================
    '''<summary>���i+�W�������i�f�[�^���擾����</summary>
    Public Function GetSyouhinKakakuInfo(ByVal intRows As String, _
                                    ByVal strSyouhinCd As String, _
                                    ByVal strSyouhinMei As String, _
                                     ByVal soukoCd As String, _
                                    Optional ByVal blnDelete As String = "") As Data.DataTable
        Return CommonSearchDA.SelSyouhinKakakuInfo(intRows, strSyouhinCd, strSyouhinMei, soukoCd, blnDelete)
    End Function
    '==================2011/05/11 �ԗ� �����������\���ύX �ǉ� �I����==========================

    '''<summary>�r���_�[�f�[�^���擾����</summary>

    Public Function GetBirudaInfo(ByVal intRows As String, _
                                    ByVal strBirudaCd As String, _
                                    ByVal strBirudaMei As String, _
                                      ByVal blnDelete As Boolean) As CommonSearchDataSet.BirudaTableDataTable
        Return CommonSearchDA.SelBirudaInfo(intRows, strBirudaCd, strBirudaMei, blnDelete)
    End Function
    '''<summary>�r���_�[���R�[�h�s�����擾����</summary>
    Public Function GetBirudaCount(ByVal strSyouhinCd As String, _
                                      ByVal strSyouhinMei As String, _
                                      ByVal blnDelete As Boolean) As Long
        Return CommonSearchDA.SelBirudaCount(strSyouhinCd, strSyouhinMei, blnDelete)
    End Function
    '''<summary>�����f�[�^���擾����</summary>
    Public Function GetKengen(ByVal strAccountNo As String) As CommonSearchDataSet.AccountTableDataTable
        Return CommonSearchDA.SelKengen(strAccountNo)
    End Function
    '''<summary>�Q�Ɨ����f�[�^��V�K����</summary>
    Public Function SetInsLog(ByVal strUrl As String, ByVal strUerId As String) As Boolean
        Return CommonSearchDA.InsUrlLog(strUrl, strUerId)
    End Function
    '''<summary>���ʃf�[�^���擾����</summary>
    Public Function GetCommonInfo(ByVal strCd As String, _
                                         ByVal strTableName As String, Optional ByVal strKubun As String = "") As CommonSearchDataSet.BirudaTableDataTable
        Return CommonSearchDA.SelCommonInfo(strCd, strTableName, strKubun)
    End Function
    '''<summary>�����X��ʂ��擾����</summary>
    Public Function Selkameitensyubetu(ByVal intRows As String, _
                    ByVal code As String, ByVal mei As String) As CommonSearchDataSet.meisyouTableDataTable
        Return CommonSearchDA.Selkameitensyubetu(intRows, code, mei)
    End Function

    '''<summary>�����X��ʃ��R�[�h�s�����擾����</summary>
    Public Function SelkameitensyubetuCount(ByVal code As String, ByVal mei As String) As Integer
        Return CommonSearchDA.SelkameitensyubetuCount(code, mei)
    End Function

    '''<summary>�d�l���擾����</summary>
    Public Function SelSiyouInfo(ByVal intRows As String, _
                    ByVal code As String, ByVal mei As String) As CommonSearchDataSet.IntTableDataTable
        Return CommonSearchDA.SelSiyouInfo(intRows, code, mei)
    End Function
    '''<summary>�d�l���R�[�h�s�����擾����</summary>
    Public Function SelSiyouCount(ByVal code As String, ByVal mei As String) As Integer
        Return CommonSearchDA.SelSiyouCount(code, mei)
    End Function
    Public Function GetSinkaikeiSiharaiSakiInfo(ByVal intRows As String, _
                                            ByVal strCd As String, _
                                            ByVal strCd2 As String, _
                                            ByVal strMei As String, _
                                            ByVal strMei2 As String) As DataTable

        Return CommonSearchDA.SelSinkaikeiSiharaiSakiInfo(intRows, strCd, strCd2, strMei, strMei2)
    End Function
    Public Function GetSinkaikeiSiharaiSakiCount(ByVal strCd As String, _
                                         ByVal strCd2 As String, _
                                         ByVal strMei As String, _
                                         ByVal strMei2 As String) As Integer

        Return CommonSearchDA.SelSinkaikeiSiharaiSakiCount(strCd, strCd2, strMei, strMei2)
    End Function

    ''' <summary>������Ѓf�[�^���擾����</summary>
    Public Function GetTyousaInfo(ByVal intRows As String, _
                                        ByVal strCd As String, _
                                        ByVal strMei As String, _
                                        ByVal strMei2 As String, _
                                        Optional ByVal blnDelete As Boolean = True) As CommonSearchDataSet.tyousakaisyaTableDataTable
        Return CommonSearchDA.SelTyousaInfo(intRows, strCd, strMei, strMei2, blnDelete)
    End Function

    ''' <summary>������Ѓf�[�^���擾����</summary>
    Public Function GetJigyousyoInfo(ByVal intRows As String, _
                                        ByVal strCd As String, _
                                        ByVal strMei As String, _
                                        ByVal strMei2 As String) As DataTable
        Return CommonSearchDA.SelJigyousyoInfo(intRows, strCd, strMei, strMei2)
    End Function
    ''' <summary>������Ѓf�[�^���擾����</summary>
    Public Function GetJigyousyoCount(ByVal strCd As String, _
                                        ByVal strMei As String, _
                                        ByVal strMei2 As String) As Integer
        Return CommonSearchDA.SelJigyousyoCount(strCd, strMei, strMei2)
    End Function

    ''' <summary>������Ѓ��R�[�h�s�����擾����</summary>
    Public Function GetTyousaCount(ByVal strCd As String, _
                                        ByVal strMei As String, _
                                        ByVal strMei2 As String, _
                                        Optional ByVal blnDelete As Boolean = True) As Integer
        Return CommonSearchDA.SelTyousaCount(strCd, strMei, strMei2, blnDelete)
    End Function
    ''' <summary>������}�X�^���擾����</summary>
    Public Function GetSeikyuuSakiInfo(ByVal intRows As String, _
                                        ByVal strCd As String, _
                                        ByVal strMei As String, _
                                        ByVal strMei2 As String, ByVal strMei3 As String, ByVal blnDelete As Boolean, Optional ByVal blnKana As Boolean = False) As CommonSearchDataSet.SeikyuuSakiTableDataTable
        Return CommonSearchDA.SelSeikyuuSakiInfo(intRows, strCd, strMei, strMei2, strMei3, blnDelete, blnKana)
    End Function
    ''' <summary>������}�X�^���R�[�h�s�����擾����</summary>
    Public Function GetSeikyuuSakiCount(ByVal strCd As String, _
                                        ByVal strMei As String, _
                                        ByVal strMei2 As String, ByVal strMei3 As String, ByVal blnDelete As Boolean, Optional ByVal blnKana As Boolean = False) As Integer
        Return CommonSearchDA.SelSeikyuuSakiCount(strCd, strMei, strMei2, strMei3, blnDelete, blnKana)
    End Function

    ''' <summary>�����f�[�^���擾����</summary>
    Public Function GetNayoseSakiInfo(ByVal intRows As String, _
                                            ByVal strCd As String, _
                                            ByVal strMei As String, _
                                            ByVal strMei2 As String) As DataTable

        Return CommonSearchDA.SelNayoseSakiInfo(intRows, strCd, strMei, strMei2)
    End Function
    ''' <summary>�����s�����擾����</summary>
    Public Function GetNayoseSakiCount(ByVal strCd As String, _
                                         ByVal strMei As String, _
                                         ByVal strMei2 As String) As Integer

        Return CommonSearchDA.SelNayoseSakiCount(strCd, strMei, strMei2)
    End Function

    ''' <summary>���ʑΉ��f�[�^���擾����</summary>
    Public Function GetTokubetuKaiouInfo(ByVal intRows As String, ByVal strCd As String, ByVal strMei As String _
                                            , ByVal blnDelete As Boolean) As DataTable
        Return CommonSearchDA.SelTokubetuTaiouInfo(intRows, strCd, strMei, blnDelete)
    End Function

    ''' <summary>���ʑΉ��s�����擾</summary>
    Public Function GetTokubetuKaiouCount(ByVal strCd As String, ByVal strMei As String, ByVal blnDelete As Boolean) As Integer
        Return CommonSearchDA.SelTokubetuTaiouCount(strCd, strMei, blnDelete)
    End Function

    ''' <summary>�R�[�h�擾���̃}�X�^�擾����</summary>
    Public Function SelSAPSiireSaki(ByVal top As Integer, ByVal a1_ktokk As String, ByVal a1_lifnr As String, ByVal a1_a_zz_sort As String, ByVal sort As String) As DataSet
        Return CommonSearchDA.SelSAPSiireSaki(top, a1_ktokk, a1_lifnr, a1_a_zz_sort, sort)
    End Function


    ''' <summary>�����ٰ�ߎ擾����</summary>
    Public Function SelDis_a1_ktokk() As DataTable
        Return CommonSearchDA.SelDis_a1_ktokk
    End Function

End Class
