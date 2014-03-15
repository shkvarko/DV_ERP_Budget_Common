using System;
using System.Collections.Generic;
using System.Text;

namespace ERP_Budget.Common
{
    public enum EnumCurrencyRateType
    {
        NationalBank = 0,
        Accounting = 1,
        Pricing = 2,
    }
    /// <summary>
    /// ����� "���� �����"
    /// </summary>
    public class CCurrencyRateItem : IBaseListItem
    {
        #region ����������, ��������, ���������
        /// <summary>
        /// ����
        /// </summary>
        private System.DateTime m_dtDate;
        /// <summary>
        /// ������
        /// </summary>
        private CCurrency m_objCurrencyIn;
        /// <summary>
        /// ������, � ������� ����������� CurrencyIn
        /// </summary>
        private CCurrency m_objCurrencyOut;
        /// <summary>
        /// ����
        /// </summary>
        private System.Decimal m_moneyValue;
        /// <summary>
        /// ����
        /// </summary>
        public System.DateTime Date
        {
            get
            {
                return m_dtDate;
            }
            set
            {
                m_dtDate = value;
            }
        }
        /// <summary>
        /// ������
        /// </summary>
        public CCurrency CurrencyIn
        {
            get
            {
                return m_objCurrencyIn;
            }
            set
            {
                m_objCurrencyIn = value;
            }
        }
        /// <summary>
        /// ������, � ������� ����������� CurrencyIn
        /// </summary>
        public CCurrency CurrencyOut
        {
            get
            {
                return m_objCurrencyOut;
            }
            set
            {
                m_objCurrencyOut = value;
            }
        }
        /// <summary>
        /// ����
        /// </summary>
        public System.Decimal Value
        {
            get
            {
                return m_moneyValue;
            }
            set
            {
                m_moneyValue = value;
            }
        }
        /// <summary>
        /// ��������� �������
        /// </summary>
        private System.Data.DataRowState m_State;
        /// <summary>
        /// ��������� �������
        /// </summary>
        public System.Data.DataRowState State
        {
            get
            {
                return m_State;
            }
            set
            {
                m_State = value;
            }
        }

        #endregion

        public CCurrencyRateItem()
        {
            this.m_uuidID = System.Guid.Empty;
            this.m_strName = "";
            this.m_dtDate = System.DateTime.Now;
            this.m_moneyValue = 0;
            this.m_objCurrencyIn = null;
            this.m_objCurrencyOut = null;
            this.m_State = System.Data.DataRowState.Detached;
        }

        #region ������ ������
        /// <summary>
        /// ���������� ������ ������ �����
        /// </summary>
        /// <param name="enCurrencyRateType">��� �����</param>
        /// <param name="dtBegin">������ �������</param>
        /// <param name="dtEnd">����� �������</param>
        /// <param name="objProfile">�������</param>
        /// <param name="cmdSQL">SQL-�������</param>
        /// <returns>������ ������ �����</returns>
        public static List<CCurrencyRateItem> GetCurrencyRateList(EnumCurrencyRateType enCurrencyRateType, System.DateTime dtBegin, System.DateTime dtEnd,
            UniXP.Common.CProfile objProfile, System.Data.SqlClient.SqlCommand cmdSQL)
        {
            List<CCurrencyRateItem> objList = new List<CCurrencyRateItem>();

            System.Data.SqlClient.SqlConnection DBConnection = null;
            System.Data.SqlClient.SqlCommand cmd = null;
            try
            {
                if (cmdSQL == null)
                {
                    DBConnection = objProfile.GetDBSource();
                    if (DBConnection == null)
                    {
                        DevExpress.XtraEditors.XtraMessageBox.Show(
                            "�� ������� �������� ���������� � ����� ������.", "��������",
                            System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                        return objList;
                    }
                    cmd = new System.Data.SqlClient.SqlCommand();
                    cmd.Connection = DBConnection;
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                }
                else
                {
                    cmd = cmdSQL;
                    cmd.Parameters.Clear();
                }

                cmd.CommandText = System.String.Format("[{0}].[dbo].[usp_GetCurrencyRateList]", objProfile.GetOptionsDllDBName());
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ((System.Byte)(0)), ((System.Byte)(0)), "", System.Data.DataRowVersion.Current, null));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@CurrencyRateTypeId", System.Data.SqlDbType.Int));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ERROR_NUM", System.Data.SqlDbType.Int, 8, System.Data.ParameterDirection.Output, false, ((System.Byte)(0)), ((System.Byte)(0)), "", System.Data.DataRowVersion.Current, null));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ERROR_MES", System.Data.SqlDbType.NVarChar, 4000));
                cmd.Parameters["@CurrencyRateTypeId"].Value = System.Convert.ToInt32(enCurrencyRateType);
                if (dtBegin != null)
                {
                    cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@BEGIN_DATE", System.Data.SqlDbType.DateTime, 4));
                    cmd.Parameters["@BEGIN_DATE"].Value = dtBegin;
                }
                if (dtEnd != null)
                {
                    cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@END_DATE", System.Data.SqlDbType.DateTime, 4));
                    cmd.Parameters["@END_DATE"].Value = dtEnd;
                }
                cmd.Parameters["@ERROR_MES"].Direction = System.Data.ParameterDirection.Output;
                System.Data.SqlClient.SqlDataReader rs = cmd.ExecuteReader();
                if (rs.HasRows)
                {
                    CCurrencyRateItem objItem = null;
                    while (rs.Read())
                    {
                        objItem = new CCurrencyRateItem();
                        objItem.m_uuidID = (System.Guid)rs["GUID_ID"];
                        objItem.m_dtDate = System.Convert.ToDateTime(rs["CURRENCYRATE_DATE"]);
                        objItem.m_moneyValue = System.Convert.ToDecimal(rs["CURRENCYRATE_VALUE"]);
                        objItem.m_objCurrencyIn = new CCurrency((System.Guid)rs["CURRENCY_IN_GUID_ID"], System.Convert.ToString(rs["CURRENCY_CODE_IN"]),
                            System.Convert.ToString(rs["CURRENCY_NAME_IN"]));
                        objItem.m_objCurrencyOut = new CCurrency((System.Guid)rs["CURRENCY_OUT_GUID_ID"], System.Convert.ToString(rs["CURRENCY_CODE_OUT"]),
                            System.Convert.ToString(rs["CURRENCY_NAME_OUT"]));

                        objList.Add(objItem);
                    }
                    objItem = null;
                }
                rs.Dispose();
                if (cmdSQL == null)
                {
                    cmd.Dispose();
                    DBConnection.Close();
                }
            }
            catch (System.Exception f)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(
                "�� ������� �������� ������ ������ �����.\n\n����� ������: " + f.Message, "��������",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
            return objList;
        }

        #endregion

        #region ���������� ��������� 

        #region IsAllParametersValid
        /// <summary>
        /// �������� ������� ����� �����������
        /// </summary>
        /// <returns>true - ������ ���; false - ������</returns>
        private System.Boolean IsAllParametersValid()
        {
            System.Boolean bRet = false;
            try
            {
                // ���������� ������� ������ 1
                if (this.m_objCurrencyIn == null)
                {
                    DevExpress.XtraEditors.XtraMessageBox.Show("������� ������", "��������",
                        System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);
                    return bRet;
                }
                // ���������� ������� ������ ���������
                if (this.m_objCurrencyOut == null)
                {
                    DevExpress.XtraEditors.XtraMessageBox.Show("������� ������ ���������", "��������",
                        System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);
                    return bRet;
                }
                // ���������� ������� ����
                if (this.m_moneyValue < 0)
                {
                    DevExpress.XtraEditors.XtraMessageBox.Show("�������� ����", "��������",
                        System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);
                    return bRet;
                }

                bRet = true;
            }
            catch (System.Exception f)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(
                "������ �������� �������. ����� ������: " + f.Message, "��������",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
            return bRet;
        }
        #endregion

        #region �������� ������ �� ��������� ����
        /// <summary>
        /// ������� ����� �� ��������� ����
        /// </summary>
        /// <param name="dtCurrencyRateDate">����</param>
        /// <param name="objProfile">�������</param>
        /// <param name="cmdSQL">SQL-�������</param>
        /// <param name="strErr">��������� �� ������</param>
        /// <returns>true - ������� ���������� ��������; false - ������</returns>
        public static System.Boolean RemoveAgreementWithCustomer(EnumCurrencyRateType enCurrencyRateType, System.DateTime dtCurrencyRateDate, UniXP.Common.CProfile objProfile, System.Data.SqlClient.SqlCommand cmdSQL, ref System.String strErr)
        {
            System.Boolean bRet = false;
            System.Data.SqlClient.SqlConnection DBConnection = null;
            System.Data.SqlClient.SqlCommand cmd = null;
            System.Data.SqlClient.SqlTransaction DBTransaction = null;
            try
            {
                if (cmdSQL == null)
                {
                    DBConnection = objProfile.GetDBSource();
                    if (DBConnection == null)
                    {
                        strErr = "�� ������� �������� ���������� � ����� ������.";
                        return bRet;
                    }
                    DBTransaction = DBConnection.BeginTransaction();
                    cmd = new System.Data.SqlClient.SqlCommand();
                    cmd.Connection = DBConnection;
                    cmd.Transaction = DBTransaction;
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                }
                else
                {
                    cmd = cmdSQL;
                    cmd.Parameters.Clear();
                }

                cmd.CommandText = System.String.Format("[{0}].[dbo].[usp_DeleteCurrencyRateByDate]", objProfile.GetOptionsDllDBName());
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ((System.Byte)(0)), ((System.Byte)(0)), "", System.Data.DataRowVersion.Current, null));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@CurrencyRateTypeId", System.Data.SqlDbType.Int));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@CURRENCYRATE_DATE", System.Data.SqlDbType.DateTime));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ERROR_NUM", System.Data.SqlDbType.Int, 8, System.Data.ParameterDirection.Output, false, ((System.Byte)(0)), ((System.Byte)(0)), "", System.Data.DataRowVersion.Current, null));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ERROR_MES", System.Data.SqlDbType.NVarChar, 4000));
                cmd.Parameters["@ERROR_MES"].Direction = System.Data.ParameterDirection.Output;
                cmd.Parameters["@CurrencyRateTypeId"].Value = System.Convert.ToInt32(enCurrencyRateType);
                cmd.Parameters["@CURRENCYRATE_DATE"].Value = dtCurrencyRateDate;
                cmd.ExecuteNonQuery();
                System.Int32 iRes = (System.Int32)cmd.Parameters["@RETURN_VALUE"].Value;
                bRet = (iRes == 0);
                if (iRes != 0)
                {
                    strErr = (System.String)cmd.Parameters["@ERROR_MES"].Value;
                }


                if (cmdSQL == null)
                {
                    if (iRes == 0)
                    {
                        // ������������ ����������
                        if (DBTransaction != null)
                        {
                            DBTransaction.Commit();
                        }
                    }
                    else
                    {
                        // ���������� ����������
                        if (DBTransaction != null)
                        {
                            DBTransaction.Rollback();
                        }
                    }
                    DBConnection.Close();
                }
                bRet = (iRes == 0);
            }
            catch (System.Exception f)
            {
                if ((cmdSQL == null) && (DBTransaction != null))
                {
                    DBTransaction.Rollback();
                }
                strErr = f.Message;
            }
            finally
            {
                if (DBConnection != null)
                {
                    DBConnection.Close();
                }
            }
            return bRet;
        }
        #endregion

        #region ���������� ������ �� �������� ���� � ��
        /// <summary>
        /// ���������� ������ �� �������� ���� � ��
        /// </summary>
        /// <param name="enCurrencyRateType">��� ����� (������� ��� �������)</param>
        /// <param name="dtCurrencyRateDate">����</param>
        /// <param name="objCurrencyRateItemList">������ ������</param>
        /// <param name="objProfile">�������</param>
        /// <param name="cmdSQL">SQl-�������</param>
        /// <param name="strErr">��������� �� ������</param>
        /// <returns>true - ������� ���������� ��������; false - ������</returns>
        public static System.Boolean SaveCurrencyRateListByDate(EnumCurrencyRateType enCurrencyRateType, System.DateTime dtCurrencyRateDate,
            List<CCurrencyRateItem> objCurrencyRateItemList, UniXP.Common.CProfile objProfile,
            System.Data.SqlClient.SqlCommand cmdSQL, ref System.String strErr)
        {
            System.Boolean bRet = true;
            foreach (CCurrencyRateItem objCurrencyRateItem in objCurrencyRateItemList)
            {
                if (objCurrencyRateItem.IsAllParametersValid() == false) 
                {
                    bRet = false;
                    break;
                }
            }
            if (bRet == false) { return bRet; }

            System.Data.SqlClient.SqlConnection DBConnection = null;
            System.Data.SqlClient.SqlCommand cmd = null;
            System.Data.SqlClient.SqlTransaction DBTransaction = null;
            try
            {
                if (cmdSQL == null)
                {
                    DBConnection = objProfile.GetDBSource();
                    if (DBConnection == null)
                    {
                        strErr = "�� ������� �������� ���������� � ����� ������.";
                        return bRet;
                    }
                    DBTransaction = DBConnection.BeginTransaction();
                    cmd = new System.Data.SqlClient.SqlCommand();
                    cmd.Connection = DBConnection;
                    cmd.Transaction = DBTransaction;
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                }
                else
                {
                    cmd = cmdSQL;
                    cmd.Parameters.Clear();
                }

                // ������ �� ������� ����� �� ��������� ����, � ����� �������� �� ������ � ��������

                bRet = RemoveAgreementWithCustomer(enCurrencyRateType, dtCurrencyRateDate, objProfile, cmd, ref  strErr);
                if (bRet == true)
                {
                    cmd.Parameters.Clear();
                    cmd.CommandText = System.String.Format("[{0}].[dbo].[usp_AddCurrencyRate]", objProfile.GetOptionsDllDBName());
                    cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ((System.Byte)(0)), ((System.Byte)(0)), "", System.Data.DataRowVersion.Current, null));
                    cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@GUID_ID", System.Data.SqlDbType.UniqueIdentifier, 4, System.Data.ParameterDirection.Output, false, ((System.Byte)(0)), ((System.Byte)(0)), "", System.Data.DataRowVersion.Current, null));
                    cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@CurrencyRateTypeId", System.Data.SqlDbType.Int));
                    cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@CURRENCY_IN_GUID_ID", System.Data.SqlDbType.UniqueIdentifier, 4));
                    cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@CURRENCY_OUT_GUID_ID", System.Data.SqlDbType.UniqueIdentifier, 4));
                    cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@CURRENCYRATE_DATE", System.Data.SqlDbType.DateTime, 4));
                    cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@CURRENCYRATE_VALUE", System.Data.SqlDbType.Money, 8));

                    cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ERROR_NUM", System.Data.SqlDbType.Int, 8, System.Data.ParameterDirection.Output, false, ((System.Byte)(0)), ((System.Byte)(0)), "", System.Data.DataRowVersion.Current, null));
                    cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ERROR_MES", System.Data.SqlDbType.NVarChar, 4000));
                    cmd.Parameters["@ERROR_MES"].Direction = System.Data.ParameterDirection.Output;
                    cmd.Parameters["@CurrencyRateTypeId"].Value = System.Convert.ToInt32(enCurrencyRateType);
                    System.Int32 iRet = 0;

                    foreach (CCurrencyRateItem objItem in objCurrencyRateItemList)
                    {
                        cmd.Parameters["@CURRENCY_IN_GUID_ID"].Value = objItem.m_objCurrencyIn.uuidID;
                        cmd.Parameters["@CURRENCY_OUT_GUID_ID"].Value = objItem.m_objCurrencyOut.uuidID;
                        cmd.Parameters["@CURRENCYRATE_DATE"].Value = objItem.m_dtDate;
                        cmd.Parameters["@CURRENCYRATE_VALUE"].Value = objItem.m_moneyValue;

                        cmd.ExecuteNonQuery();
                        iRet = (System.Int32)cmd.Parameters["@RETURN_VALUE"].Value;
                        if (iRet != 0)
                        {
                            bRet = false;
                            strErr = (System.String)cmd.Parameters["@ERROR_MES"].Value;

                            break;
                        }
                    }
                }


                if (cmdSQL == null)
                {
                    if (bRet == true)
                    {
                        // ������������ ����������
                        if (DBTransaction != null)
                        {
                            DBTransaction.Commit();
                        }
                    }
                    else
                    {
                        // ���������� ����������
                        if (DBTransaction != null)
                        {
                            DBTransaction.Rollback();
                        }
                    }
                    DBConnection.Close();
                }
            }
            catch (System.Exception f)
            {
                if ((cmdSQL == null) && (DBTransaction != null))
                {
                    DBTransaction.Rollback();
                }
                strErr = f.Message;
            }
            finally
            {
                if (DBConnection != null)
                {
                    DBConnection.Close();
                }
            }

            return bRet;
        }
        #endregion

        #endregion

        /// <summary>
        /// ���������� ������ ��� ������ ����� �� �������� ������
        /// </summary>
        /// <param name="objProfile">�������</param>
        /// <returns>������ ������ �����</returns>
        public System.Collections.ArrayList GetCurrencyDateList( UniXP.Common.CProfile objProfile )
        {
            System.Collections.ArrayList objList = new System.Collections.ArrayList();

            System.Data.SqlClient.SqlConnection DBConnection = objProfile.GetDBSource();
            if( DBConnection == null ) { return objList; }

            try
            {
                // ���������� � �� ��������, ����������� ������� �� ������� ������
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
                cmd.Connection = DBConnection;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = System.String.Format( "[{0}].[dbo].[sp_GetCurrencyRateDate]", objProfile.GetOptionsDllDBName() );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ( ( System.Byte )( 0 ) ), ( ( System.Byte )( 0 ) ), "", System.Data.DataRowVersion.Current, null ) );
                System.Data.SqlClient.SqlDataReader rs = cmd.ExecuteReader();
                if( rs.HasRows )
                {
                    // ����� ������ ��������
                    while( rs.Read() )
                    {
                        objList.Add( rs.GetDateTime( 0 ) );
                    }
                }
                else
                {
                    //DevExpress.XtraEditors.XtraMessageBox.Show( 
                    //"�� ������� �������� ������ ���.\n� �� �� ������� ����������.", "��������",
                    //System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning );

                }
                cmd.Dispose();
                rs.Dispose();
            }
            catch( System.Exception e )
            {
                DevExpress.XtraEditors.XtraMessageBox.Show( 
                "�� ������� �������� ������ ���.\n" + e.Message, "��������",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
            }
            finally // ������� ���������� �������
            {
                DBConnection.Close();
            }
            return objList;
        }

        /// <summary>
        /// ���������� ������ ������ ����� �� �������� ������
        /// </summary>
        /// <param name="objProfile">�������</param>
        /// <returns>������ ������ �����</returns>
        public CBaseList<CCurrencyRateItem> GetCurrencyRateList( UniXP.Common.CProfile objProfile )
        {
            CBaseList<CCurrencyRateItem> objList = new CBaseList<CCurrencyRateItem>();

            System.Data.SqlClient.SqlConnection DBConnection = objProfile.GetDBSource();
            if( DBConnection == null ) { return objList; }

            try
            {
                // ���������� � �� ��������, ����������� ������� �� ������� ������
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
                cmd.Connection = DBConnection;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = System.String.Format( "[{0}].[dbo].[sp_GetCurrencyRateList]", objProfile.GetOptionsDllDBName() );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ( ( System.Byte )( 0 ) ), ( ( System.Byte )( 0 ) ), "", System.Data.DataRowVersion.Current, null ) );
                System.Data.SqlClient.SqlDataReader rs = cmd.ExecuteReader();
                if( rs.HasRows )
                {
                    // ����� ������ ��������
                    CCurrencyRateItem objCurrencyRateItem = null;
                    CCurrency objCurrencyIN = null;
                    CCurrency objCurrencyOUT = null;
                    while( rs.Read() )
                    {
                        objCurrencyRateItem = new CCurrencyRateItem();
                        if( rs[ 1 ] == System.DBNull.Value )
                        {
                            objCurrencyRateItem.m_uuidID = System.Guid.Empty;
                        }
                        else
                        {
                            objCurrencyRateItem.m_uuidID = rs.GetGuid( 1 );
                        }
                        objCurrencyRateItem.m_dtDate = rs.GetDateTime( 0 );
                        if( rs[ 4 ] == System.DBNull.Value )
                        {
                            objCurrencyRateItem.m_moneyValue = -1;
                        }
                        else
                        {
                            objCurrencyRateItem.m_moneyValue = System.Convert.ToDecimal(rs[4]);
                        }
                        objCurrencyIN = new CCurrency();
                        objCurrencyIN.uuidID = rs.GetGuid( 2 );
                        objCurrencyIN.CurrencyCode = rs.GetString( 5 );
                        objCurrencyIN.Name = rs.GetString( 6 );
                        objCurrencyRateItem.m_objCurrencyIn = objCurrencyIN;
                        objCurrencyOUT = new CCurrency();
                        objCurrencyOUT.uuidID = rs.GetGuid( 3 );
                        objCurrencyOUT.CurrencyCode = rs.GetString( 7 );
                        objCurrencyRateItem.m_objCurrencyOut = objCurrencyOUT;

                        objList.AddItemToList( objCurrencyRateItem );
                    }
                    objCurrencyIN = null;
                    objCurrencyOUT = null;
                }
                else
                {
                    //DevExpress.XtraEditors.XtraMessageBox.Show( 
                    //"�� ������� �������� ������ ������ �����.\n� �� �� ������� ����������.", "��������",
                    //System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning );

                }
                cmd.Dispose();
                rs.Dispose();
            }
            catch( System.Exception e )
            {
                DevExpress.XtraEditors.XtraMessageBox.Show( 
                "�� ������� �������� ������ ������ �����.\n" + e.Message, "��������",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
            }
            finally // ������� ���������� �������
            {
                DBConnection.Close();
            }
            return objList;
        }

        /// <summary>
        /// ���������� ������ ������ ����� �� �������� ������
        /// </summary>
        /// <param name="objProfile">�������</param>
        /// <param name="dtBegin">������ �������</param>
        /// <param name="dtEnd">����� �������</param>
        /// <returns>������ ������ �����</returns>
        public CBaseList<CCurrencyRateItem> GetCurrencyRateList( UniXP.Common.CProfile objProfile,
            System.DateTime dtBegin, System.DateTime dtEnd )
        {
            CBaseList<CCurrencyRateItem> objList = new CBaseList<CCurrencyRateItem>();

            System.Data.SqlClient.SqlConnection DBConnection = objProfile.GetDBSource();
            if( DBConnection == null ) { return objList; }

            try
            {
                // ���������� � �� ��������, ����������� ������� �� ������� ������
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
                cmd.Connection = DBConnection;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = System.String.Format( "[{0}].[dbo].[sp_GetCurrencyRateList]", objProfile.GetOptionsDllDBName() );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ( ( System.Byte )( 0 ) ), ( ( System.Byte )( 0 ) ), "", System.Data.DataRowVersion.Current, null ) );
                if( dtBegin != null )
                {
                    cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@BEGIN_DATE", System.Data.SqlDbType.DateTime, 4 ) );
                    cmd.Parameters[ "@BEGIN_DATE" ].Value = dtBegin;
                }
                if( dtEnd != null )
                {
                    cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@END_DATE", System.Data.SqlDbType.DateTime, 4 ) );
                    cmd.Parameters[ "@END_DATE" ].Value = dtEnd;
                }
                System.Data.SqlClient.SqlDataReader rs = cmd.ExecuteReader();
                if( rs.HasRows )
                {
                    // ����� ������ ��������
                    CCurrencyRateItem objCurrencyRateItem = null;
                    CCurrency objCurrencyIN = null;
                    CCurrency objCurrencyOUT = null;
                    while( rs.Read() )
                    {
                        objCurrencyRateItem = new CCurrencyRateItem();
                        if( rs[ 1 ] == System.DBNull.Value )
                        {
                            objCurrencyRateItem.m_uuidID = System.Guid.Empty;
                        }
                        else
                        {
                            objCurrencyRateItem.m_uuidID = rs.GetGuid( 1 );
                        }
                        objCurrencyRateItem.m_dtDate = rs.GetDateTime( 0 );
                        if( rs[ 4 ] == System.DBNull.Value )
                        {
                            objCurrencyRateItem.m_moneyValue = -1;
                        }
                        else
                        {
                            objCurrencyRateItem.m_moneyValue = System.Convert.ToDecimal(rs[4]);
                        }
                        objCurrencyIN = new CCurrency();
                        objCurrencyIN.uuidID = rs.GetGuid( 2 );
                        objCurrencyIN.CurrencyCode = rs.GetString( 5 );
                        objCurrencyIN.Name = rs.GetString( 6 );
                        objCurrencyRateItem.m_objCurrencyIn = objCurrencyIN;
                        objCurrencyOUT = new CCurrency();
                        objCurrencyOUT.uuidID = rs.GetGuid( 3 );
                        objCurrencyOUT.CurrencyCode = rs.GetString( 7 );
                        objCurrencyRateItem.m_objCurrencyOut = objCurrencyOUT;

                        objList.AddItemToList( objCurrencyRateItem );
                    }
                    objCurrencyIN = null;
                    objCurrencyOUT = null;
                }
                else
                {
                    //DevExpress.XtraEditors.XtraMessageBox.Show( 
                    //"�� ������� �������� ������ ������ �����.\n� �� �� ������� ����������.", "��������",
                    //System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning );

                }
                cmd.Dispose();
                rs.Dispose();
            }
            catch( System.Exception e )
            {
                DevExpress.XtraEditors.XtraMessageBox.Show( 
                "�� ������� �������� ������ ������ �����.\n" + e.Message, "��������",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
            }
            finally // ������� ���������� �������
            {
                DBConnection.Close();
            }
            return objList;
        }

        /// <summary>
        /// ���������� ������ ������ ����� �� ��������� � �������� ������ �� ���������� ����
        /// </summary>
        /// <param name="objProfile">�������</param>
        /// <param name="uuidCurrency">������ ���������</param>
        /// <param name="dtEnd">����</param>
        /// <returns>������ ������ �����</returns>
        public static System.Collections.Generic.List<CCurrencyRateItem> GetCurrencyRateList( UniXP.Common.CProfile objProfile,
            System.DateTime dtEnd, System.Guid uuidCurrency )
        {
            System.Collections.Generic.List<CCurrencyRateItem> objList = new System.Collections.Generic.List<CCurrencyRateItem>();

            System.Data.SqlClient.SqlConnection DBConnection = objProfile.GetDBSource();
            if( DBConnection == null ) { return objList; }

            try
            {
                // ���������� � �� ��������, ����������� ������� �� ������� ������
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
                cmd.Connection = DBConnection;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = System.String.Format( "[{0}].[dbo].[sp_GetCurrencyRate]", objProfile.GetOptionsDllDBName() );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ( ( System.Byte )( 0 ) ), ( ( System.Byte )( 0 ) ), "", System.Data.DataRowVersion.Current, null ) );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@CURRENCY_OUT_GUID_ID", System.Data.SqlDbType.UniqueIdentifier ) );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@END_DATE", System.Data.SqlDbType.DateTime ) );
                cmd.Parameters[ "@CURRENCY_OUT_GUID_ID" ].Value = uuidCurrency;
                cmd.Parameters[ "@END_DATE" ].Value = dtEnd;
                System.Data.SqlClient.SqlDataReader rs = cmd.ExecuteReader();
                if( rs.HasRows )
                {
                    // ����� ������ ��������
                    CCurrencyRateItem objCurrencyRateItem = null;
                    CCurrency objCurrencyIN = null;
                    CCurrency objCurrencyOUT = null;
                    while( rs.Read() )
                    {
                        objCurrencyRateItem = new CCurrencyRateItem();
                        if( rs[ 1 ] == System.DBNull.Value )
                        {
                            objCurrencyRateItem.m_uuidID = System.Guid.Empty;
                        }
                        else
                        {
                            objCurrencyRateItem.m_uuidID = rs.GetGuid( 1 );
                        }
                        objCurrencyRateItem.m_dtDate = rs.GetDateTime( 0 );
                        if( rs[ 4 ] == System.DBNull.Value )
                        {
                            objCurrencyRateItem.m_moneyValue = 0;
                        }
                        else
                        {
                            objCurrencyRateItem.m_moneyValue = System.Convert.ToDecimal( rs[4] );
                        }
                        objCurrencyIN = new CCurrency();
                        objCurrencyIN.uuidID = rs.GetGuid( 2 );
                        objCurrencyIN.CurrencyCode = rs.GetString( 5 );
                        objCurrencyIN.Name = rs.GetString( 6 );
                        objCurrencyRateItem.m_objCurrencyIn = objCurrencyIN;
                        objCurrencyOUT = new CCurrency();
                        objCurrencyOUT.uuidID = rs.GetGuid( 3 );
                        objCurrencyOUT.CurrencyCode = rs.GetString( 7 );
                        objCurrencyRateItem.m_objCurrencyOut = objCurrencyOUT;

                        objList.Add( objCurrencyRateItem );
                    }
                    objCurrencyIN = null;
                    objCurrencyOUT = null;
                }
                else
                {
                    DevExpress.XtraEditors.XtraMessageBox.Show( 
                    "�� ������� �������� ������ ������ �����.\n� �� �� ������� ����������.", "��������",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning );

                }
                cmd.Dispose();
                rs.Dispose();
            }
            catch( System.Exception e )
            {
                DevExpress.XtraEditors.XtraMessageBox.Show( 
                "�� ������� �������� ������ ������ �����.\n" + e.Message, "��������",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
            }
            finally // ������� ���������� �������
            {
                DBConnection.Close();
            }
            return objList;
        }

        /// <summary>
        /// ������������� ������� ������
        /// </summary>
        /// <param name="objProfile">�������</param>
        /// <param name="uuidID">���������� ������������� ������</param>
        /// <returns>true - �������� �������������; false - ������</returns>
        public override System.Boolean Init( UniXP.Common.CProfile objProfile, System.Guid uuidID )
        {
            System.Boolean bRet = false;

            System.Data.SqlClient.SqlConnection DBConnection = objProfile.GetDBSource();
            if( DBConnection == null ) { return bRet; }

            try
            {
                // ���������� � �� ��������, ����������� ������� �� ������� ������
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
                cmd.Connection = DBConnection;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = System.String.Format( "[{0}].[dbo].[sp_GetCurrencyRateItem]", objProfile.GetOptionsDllDBName() );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ( ( System.Byte )( 0 ) ), ( ( System.Byte )( 0 ) ), "", System.Data.DataRowVersion.Current, null ) );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@GUID_ID", System.Data.SqlDbType.UniqueIdentifier ) );
                cmd.Parameters[ "@GUID_ID" ].Value = uuidID;
                System.Data.SqlClient.SqlDataReader rs = cmd.ExecuteReader();
                if( rs.HasRows )
                {
                    // ����� ������ ��������, � ��� ��� ���������� ���� ������
                    rs.Read();
                    this.m_uuidID = rs.GetGuid( 1 );
                    this.m_dtDate = rs.GetDateTime( 0 );
                    this.m_moneyValue = System.Convert.ToDecimal( rs[4] );
                    CCurrency objCurrencyIN = new CCurrency();
                    objCurrencyIN.Init( objProfile, rs.GetGuid( 2 ) );
                    this.m_objCurrencyIn = objCurrencyIN;
                    CCurrency objCurrencyOUT = new CCurrency();
                    objCurrencyOUT.Init( objProfile, rs.GetGuid( 3 ) );
                    this.m_objCurrencyOut = objCurrencyOUT;
                    bRet = true;
                }
                else
                {
                    DevExpress.XtraEditors.XtraMessageBox.Show( 
                    "�� ������� ������������������� ����� CCurrencyRateItem.\n� �� �� ������� ����������.", "��������",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning );

                }
                cmd.Dispose();
                rs.Dispose();
            }
            catch( System.Exception e )
            {
                DevExpress.XtraEditors.XtraMessageBox.Show( 
                "�� ������� ������������������� ����� CCurrencyRateItem.\n" + e.Message, "��������",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
            }
            finally // ������� ���������� �������
            {
                DBConnection.Close();
            }
            return bRet;
        }

        /// <summary>
        /// ������� ������ �� ��
        /// </summary>
        /// <param name="objProfile">�������</param>
        /// <param name="uuidID">���������� ������������� �������</param>
        /// <returns>true - ������� ����������; false - ������</returns>
        public override System.Boolean Remove( UniXP.Common.CProfile objProfile )
        {
            System.Boolean bRet = false;
            // ���������� ������������� �� ������ ���� ������
            if( this.m_uuidID == System.Guid.Empty )
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(  "������������ �������� ����������� �������������� �������", "��������",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
                return bRet;
            }

            System.Data.SqlClient.SqlConnection DBConnection = objProfile.GetDBSource();
            if( DBConnection == null ) { return bRet; }

            try
            {
                // ���������� � �� ��������, ����������� ������� �� �������� ������
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
                cmd.Connection = DBConnection;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = System.String.Format( "[{0}].[dbo].[sp_DeleteCurrencyRate]", objProfile.GetOptionsDllDBName() );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ( ( System.Byte )( 0 ) ), ( ( System.Byte )( 0 ) ), "", System.Data.DataRowVersion.Current, null ) );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@GUID_ID", System.Data.SqlDbType.UniqueIdentifier ) );
                cmd.Parameters[ "@GUID_ID" ].Value = this.m_uuidID;
                cmd.ExecuteNonQuery();
                System.Int32 iRet = ( System.Int32 )cmd.Parameters[ "@RETURN_VALUE" ].Value;
                if( iRet == 0 )
                {
                    bRet = true;
                }
                else
                {
                    //if( iRet == 1 )
                    //{
                    //    DevExpress.XtraEditors.XtraMessageBox.Show(  "�� ������� ��������� ���� ������ � ��������� ���������.\n�������� ����������", "��������",
                    //        System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning );
                    //}
                    //else
                    //{
                        DevExpress.XtraEditors.XtraMessageBox.Show(  "������ �������� ����� �����\n" + this.m_dtDate.ToShortDateString() + 
                            this.m_objCurrencyIn.Name + " - " + this.m_objCurrencyOut.Name, "������",
                            System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
                    //}
                }
                cmd.Dispose();
            }
            catch( System.Exception e )
            {
                DevExpress.XtraEditors.XtraMessageBox.Show( 
                "�� ������� ������� ���� �����.\n" + this.m_dtDate.ToShortDateString() + 
                            this.m_objCurrencyIn.Name + " - " + this.m_objCurrencyOut.Name + "\n" + e.Message, "��������",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
            }
			finally // ������� ���������� �������
            {
                DBConnection.Close();
            }
            return bRet;
        }

        /// <summary>
        /// �������� ������ � ��
        /// </summary>
        /// <param name="objProfile">�������</param>
        /// <returns>true - ������� ����������; false - ������</returns>
        public override System.Boolean Add( UniXP.Common.CProfile objProfile )
        {
            System.Boolean bRet = false;

            // ���������� ������� ������ 1
            if( this.m_objCurrencyIn == null )
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(  "������� ������", "��������",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning );
                return bRet;
            }
            // ���������� ������� ������ ���������
            if( this.m_objCurrencyOut == null )
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(  "������� ������ ���������", "��������",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning );
                return bRet;
            }
            // ���������� ������� ����
            if( this.m_moneyValue < 0 )
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(  "�������� ����", "��������",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning );
                return bRet;
            }

            System.Data.SqlClient.SqlConnection DBConnection = objProfile.GetDBSource();
            if( DBConnection == null ) { return bRet; }

            try
            {
                // ���������� � �� ��������, ����������� ������� �� �������� ������ � ��
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
                cmd.Connection = DBConnection;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = System.String.Format( "[{0}].[dbo].[sp_AddCurrencyRate]", objProfile.GetOptionsDllDBName() );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ( ( System.Byte )( 0 ) ), ( ( System.Byte )( 0 ) ), "", System.Data.DataRowVersion.Current, null ) );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@GUID_ID", System.Data.SqlDbType.UniqueIdentifier, 4, System.Data.ParameterDirection.Output, false, ( ( System.Byte )( 0 ) ), ( ( System.Byte )( 0 ) ), "", System.Data.DataRowVersion.Current, null ) );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@CURRENCY_IN_GUID_ID", System.Data.SqlDbType.UniqueIdentifier, 4 ) );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@CURRENCY_OUT_GUID_ID", System.Data.SqlDbType.UniqueIdentifier, 4 ) );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@CURRENCYRATE_DATE", System.Data.SqlDbType.DateTime, 4 ) );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@CURRENCYRATE_VALUE", System.Data.SqlDbType.Money, 8 ) );

                cmd.Parameters[ "@CURRENCY_IN_GUID_ID" ].Value = this.m_objCurrencyIn.uuidID;
                cmd.Parameters[ "@CURRENCY_OUT_GUID_ID" ].Value = this.m_objCurrencyOut.uuidID;
                cmd.Parameters[ "@CURRENCYRATE_DATE" ].Value = this.m_dtDate;
                cmd.Parameters[ "@CURRENCYRATE_VALUE" ].Value = this.m_moneyValue;
                cmd.ExecuteNonQuery();
                System.Int32 iRet = ( System.Int32 )cmd.Parameters[ "@RETURN_VALUE" ].Value;
                if( iRet == 0 )
                {
                    this.m_uuidID = ( System.Guid )cmd.Parameters[ "@GUID_ID" ].Value;
                    bRet = true;
                }
                else
                {
                    switch( iRet )
                    {
                        case 1:
                        DevExpress.XtraEditors.XtraMessageBox.Show(  "������ � ��������������� " + this.m_objCurrencyIn.uuidID.ToString() + " �� �������", "��������",
                            System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning );
                        break;
                        case 2:
                        DevExpress.XtraEditors.XtraMessageBox.Show(  "������ � ��������������� " + this.m_objCurrencyOut.uuidID.ToString() + " �� �������", "��������",
                            System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
                        break;
                        case 3:
                        DevExpress.XtraEditors.XtraMessageBox.Show(  "��� �������� ����� �� �������� ���� ���� ��� ����������", "��������",
                            System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
                        break;
                        default:
                        DevExpress.XtraEditors.XtraMessageBox.Show(  "������ �������� ����� �����", "��������",
                            System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
                        break;
                    }
                }
                cmd.Dispose();
            }
            catch( System.Exception e )
            {
                DevExpress.XtraEditors.XtraMessageBox.Show( 
                "�� ������� ������� ���� �����.\n" + e.Message, "��������",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
            }
			finally // ������� ���������� �������
            {
                DBConnection.Close();
            }
            return bRet;
        }

        /// <summary>
        /// ��������� ��������� � ��
        /// </summary>
        /// <param name="objProfile">�������</param>
        /// <returns>true - ������� ����������; false - ������</returns>
        public override System.Boolean Update( UniXP.Common.CProfile objProfile )
        {
            System.Boolean bRet = false;

            // ���������� ������������� �� ������ ���� ������
            if( this.m_uuidID == System.Guid.Empty )
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(  "������������ �������� ����������� �������������� �������", "��������",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
                return bRet;
            }
            // ���������� ������� ������ 1
            if( this.m_objCurrencyIn == null )
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(  "������� ������", "��������",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning );
                return bRet;
            }
            // ���������� ������� ������ ���������
            if( this.m_objCurrencyOut == null )
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(  "������� ������ ���������", "��������",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning );
                return bRet;
            }
            // ���������� ������� ����
            if( this.m_moneyValue <= 0 )
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(  "�������� ����", "��������",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning );
                return bRet;
            }

            System.Data.SqlClient.SqlConnection DBConnection = objProfile.GetDBSource();
            if( DBConnection == null ) { return bRet; }

            try
            {
                // ���������� � �� ��������, ����������� ������� �� �������� ������ � ��
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
                cmd.Connection = DBConnection;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = System.String.Format( "[{0}].[dbo].[sp_EditCurrencyRate]", objProfile.GetOptionsDllDBName() );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ( ( System.Byte )( 0 ) ), ( ( System.Byte )( 0 ) ), "", System.Data.DataRowVersion.Current, null ) );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@GUID_ID", System.Data.SqlDbType.UniqueIdentifier, 4 ) );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@CURRENCY_IN_GUID_ID", System.Data.SqlDbType.UniqueIdentifier, 4 ) );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@CURRENCY_OUT_GUID_ID", System.Data.SqlDbType.UniqueIdentifier, 4 ) );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@CURRENCYRATE_DATE", System.Data.SqlDbType.DateTime, 4 ) );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@CURRENCYRATE_VALUE", System.Data.SqlDbType.Money, 8 ) );

                cmd.Parameters[ "@GUID_ID" ].Value = this.m_uuidID;
                cmd.Parameters[ "@CURRENCY_IN_GUID_ID" ].Value = this.m_objCurrencyIn.uuidID;
                cmd.Parameters[ "@CURRENCY_OUT_GUID_ID" ].Value = this.m_objCurrencyOut.uuidID;
                cmd.Parameters[ "@CURRENCYRATE_DATE" ].Value = this.m_dtDate;
                cmd.Parameters[ "@CURRENCYRATE_VALUE" ].Value = this.m_moneyValue;
                cmd.ExecuteNonQuery();
                System.Int32 iRet = ( System.Int32 )cmd.Parameters[ "@RETURN_VALUE" ].Value;
                if( iRet == 0 )
                {
                    bRet = true;
                }
                else
                {
                    switch( iRet )
                    {
                        case 1:
                        DevExpress.XtraEditors.XtraMessageBox.Show(  "������ � ��������������� " + this.m_objCurrencyIn.uuidID.ToString() + " �� �������", "��������",
                            System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning );
                        break;
                        case 2:
                        DevExpress.XtraEditors.XtraMessageBox.Show(  "������ � ��������������� " + this.m_objCurrencyOut.uuidID.ToString() + " �� �������", "��������",
                            System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
                        break;
                        case 3:
                        DevExpress.XtraEditors.XtraMessageBox.Show(  "������ � �������� ��������������� �� �������", "��������",
                            System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
                        break;
                        case 4:
                        DevExpress.XtraEditors.XtraMessageBox.Show(  "��� �������� ����� �� �������� ���� ���� ��� ����������", "��������",
                            System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
                        break;
                        default:
                        DevExpress.XtraEditors.XtraMessageBox.Show(  "������ ��������� ����� �����", "��������",
                            System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
                        break;
                    }
                }
                cmd.Dispose();
            }
            catch( System.Exception e )
            {
                DevExpress.XtraEditors.XtraMessageBox.Show( 
                "�� ������� �������� ���� �����.\n" + e.Message, "��������",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
            }
			finally // ������� ���������� �������
            {
                DBConnection.Close();
            }
            return bRet;
        }
        /// <summary>
        /// ��������� �������� �������� ������� "���� ������" �� ����������� ������ � ��
        /// </summary>
        /// <returns></returns>
        private System.Boolean IsValidateForSaveToDB()
        {
            System.Boolean bRet = false;
            try
            {
                if( ( this.m_State == System.Data.DataRowState.Modified ) ||
                    ( this.m_State == System.Data.DataRowState.Deleted ) )
                {
                    if( this.uuidID.CompareTo( System.Guid.Empty ) == 0 )
                    {
                        DevExpress.XtraEditors.XtraMessageBox.Show(  "������������ �������� ����������� �������������� �������", "�������� �������� � ������ ����� �����",
                            System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
                        return bRet;
                    }
                }
                if( ( this.m_State == System.Data.DataRowState.Added ) || 
                    ( this.m_State == System.Data.DataRowState.Modified ) )
                {
                    if( this.m_objCurrencyIn == null )
                    {
                        DevExpress.XtraEditors.XtraMessageBox.Show(  "������������ �������� ������", "�������� �������� � ������ ����� �����",
                            System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
                        return bRet;
                    }
                    if( this.m_objCurrencyOut == null )
                    {
                        DevExpress.XtraEditors.XtraMessageBox.Show(  "������������ �������� ������ ���������", "�������� �������� � ������ ����� �����",
                            System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
                        return bRet;
                    }
                    if( this.m_moneyValue <= 0 )
                    {
                        DevExpress.XtraEditors.XtraMessageBox.Show(  "������������ �������� �����", "�������� �������� � ������ ����� �����",
                            System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
                        return bRet;
                    }
                }
                bRet = true;
            }
            catch( System.Exception f )
            {
                DevExpress.XtraEditors.XtraMessageBox.Show( 
                "������ �������� ����� �� ����������� ���������� � ��.\n" + f.Message, "��������",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
            }
            return bRet;
        }
        /// <summary>
        /// ��������� � �� ������ ������ �����
        /// </summary>
        /// <param name="CurrencyRateList">������ ������ �����</param>
        /// <param name="objProfile">�������</param>
        /// <returns>true - �������� ����������; false - ������</returns>
        public static System.Boolean SaveCurrencyRateListToDB( CBaseList<CCurrencyRateItem> CurrencyRateList,
            UniXP.Common.CProfile objProfile )
        {
            System.Boolean bRes = false;
            // ��������, � �� ������ �� ������
            if( CurrencyRateList.GetCountItems() == 0 ) { return bRes; }
            // ���������� � ��
            System.Data.SqlClient.SqlConnection DBConnection = objProfile.GetDBSource();
            if( DBConnection == null ) { return bRes; }
            System.Data.SqlClient.SqlTransaction DBTransaction = null;
            try
            {
                // ���������� � �� ��������, ��������� ��������� � ���� ������ ���������� 
                // �� ���������� � ������ ������ �����
                System.Int32 iObjectsCount = CurrencyRateList.GetCountItems();
                CCurrencyRateItem objCurrencyRateItem = null;
                // ��������� ����������
                DBTransaction = DBConnection.BeginTransaction();
                // SQL-������� 
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
                cmd.Connection = DBConnection;
                cmd.Transaction = DBTransaction;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                System.Int32 iRet = -1;
                for( System.Int32 i = 0; i< iObjectsCount; i++ )
                {
                    objCurrencyRateItem = CurrencyRateList.GetByIndex( i );
                    if( objCurrencyRateItem == null ) { continue; }
                    // ��������� ������ �� ������� ��������� ��������
                    if( objCurrencyRateItem.IsValidateForSaveToDB() == false )
                    {
                        DBTransaction.Rollback();
                        break;
                    }

                    // �������������� ��������� SQL-�������
                    cmd.Parameters.Clear();
                    iRet = -1;
                    switch( objCurrencyRateItem.State )
                    {
                        case System.Data.DataRowState.Added:
                        {

                            // ����� ������/��������� ��������
                            cmd.CommandText = System.String.Format( "[{0}].[dbo].[sp_AddCurrencyRate]", objProfile.GetOptionsDllDBName() );
                            cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@RETURN_VALUE", System.Data.SqlDbType.Int, 8, System.Data.ParameterDirection.ReturnValue, false, ( ( System.Byte )( 0 ) ), ( ( System.Byte )( 0 ) ), "", System.Data.DataRowVersion.Current, null ) );
                            cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@GUID_ID", System.Data.SqlDbType.UniqueIdentifier, 16, System.Data.ParameterDirection.Output, false, ( ( System.Byte )( 0 ) ), ( ( System.Byte )( 0 ) ), "", System.Data.DataRowVersion.Current, null ) );
                            cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@CURRENCY_IN_GUID_ID", System.Data.SqlDbType.UniqueIdentifier ) );
                            cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@CURRENCY_OUT_GUID_ID", System.Data.SqlDbType.UniqueIdentifier ) );
                            cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@CURRENCYRATE_DATE", System.Data.SqlDbType.SmallDateTime  ) );
                            cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@CURRENCYRATE_VALUE", System.Data.SqlDbType.Money ) );
                            cmd.Parameters[ "@CURRENCY_IN_GUID_ID" ].Value = objCurrencyRateItem.CurrencyIn.uuidID;
                            cmd.Parameters[ "@CURRENCY_OUT_GUID_ID" ].Value = objCurrencyRateItem.CurrencyOut.uuidID;
                            cmd.Parameters[ "@CURRENCYRATE_DATE" ].Value = objCurrencyRateItem.Date;
                            cmd.Parameters[ "@CURRENCYRATE_VALUE" ].Value = System.Convert.ToDouble( objCurrencyRateItem.Value );

                            cmd.ExecuteNonQuery();
                            iRet = ( System.Int32 )cmd.Parameters[ "@RETURN_VALUE" ].Value;
                            if( iRet != 0 )
                            {
                                DBTransaction.Rollback();
                                DevExpress.XtraEditors.XtraMessageBox.Show( 
                                "������ ���������� ����� ����� � ��\n" + objCurrencyRateItem.CurrencyIn.CurrencyCode + " -> " + 
                                objCurrencyRateItem.CurrencyOut.CurrencyCode + "  " + 
                                objCurrencyRateItem.Date.ToShortDateString() + " ���� = " + 
                                objCurrencyRateItem.Value.ToString() + "\n��� ������ : " + iRet.ToString() +  
                                "\n������ ��������� ���������.", "��������", 
                                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
                            }
                            break;
                        }

                        case System.Data.DataRowState.Modified:
                        {
                            // ��������� ������/���������
                            cmd.CommandText = System.String.Format( "[{0}].[dbo].[sp_EditCurrencyRate]", objProfile.GetOptionsDllDBName() );
                            cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ( ( System.Byte )( 0 ) ), ( ( System.Byte )( 0 ) ), "", System.Data.DataRowVersion.Current, null ) );
                            cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@GUID_ID", System.Data.SqlDbType.UniqueIdentifier ) );
                            cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@CURRENCY_IN_GUID_ID", System.Data.SqlDbType.UniqueIdentifier ) );
                            cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@CURRENCY_OUT_GUID_ID", System.Data.SqlDbType.UniqueIdentifier ) );
                            cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@CURRENCYRATE_DATE", System.Data.SqlDbType.SmallDateTime, 8 ) );
                            cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@CURRENCYRATE_VALUE", System.Data.SqlDbType.Money, 16 ) );
                            cmd.Parameters[ "@GUID_ID" ].Value = objCurrencyRateItem.uuidID;
                            cmd.Parameters[ "@CURRENCY_IN_GUID_ID" ].Value = objCurrencyRateItem.CurrencyIn.uuidID;
                            cmd.Parameters[ "@CURRENCY_OUT_GUID_ID" ].Value = objCurrencyRateItem.CurrencyOut.uuidID;
                            cmd.Parameters[ "@CURRENCYRATE_DATE" ].Value = objCurrencyRateItem.Date;
                            cmd.Parameters[ "@CURRENCYRATE_VALUE" ].Value = System.Convert.ToDouble( objCurrencyRateItem.Value );
                            cmd.ExecuteNonQuery();
                            iRet = ( System.Int32 )cmd.Parameters[ "@RETURN_VALUE" ].Value;
                            if( iRet != 0 )
                            {
                                DBTransaction.Rollback();
                                DevExpress.XtraEditors.XtraMessageBox.Show( 
                                "������ ��������� ����� ����� � ��\n" + objCurrencyRateItem.CurrencyIn.CurrencyCode + " -> " + 
                                objCurrencyRateItem.CurrencyOut.CurrencyCode + "  " + 
                                objCurrencyRateItem.Date.ToShortDateString() + " ���� = " + 
                                objCurrencyRateItem.Value.ToString() + "\n��� ������ : " + iRet.ToString() +
                                "\n������ ��������� ���������.", "��������",
                                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
                            }

                            break;
                        }

                        case System.Data.DataRowState.Deleted:
                        {
                            // �������� ������/��������� �������� �� ��
                            cmd.CommandText = System.String.Format( "[{0}].[dbo].[sp_DeleteCurrencyRate]", objProfile.GetOptionsDllDBName() );
                            cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ( ( System.Byte )( 0 ) ), ( ( System.Byte )( 0 ) ), "", System.Data.DataRowVersion.Current, null ) );
                            cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@GUID_ID", System.Data.SqlDbType.UniqueIdentifier ) );
                            cmd.Parameters[ "@GUID_ID" ].Value = objCurrencyRateItem.uuidID;
                            cmd.ExecuteNonQuery();
                            iRet = ( System.Int32 )cmd.Parameters[ "@RETURN_VALUE" ].Value;
                            if( iRet != 0 )
                            {
                                DBTransaction.Rollback();
                                DevExpress.XtraEditors.XtraMessageBox.Show( 
                                "������ �������� ����� ����� � ��\n" + objCurrencyRateItem.CurrencyIn.CurrencyCode + " -> " + 
                                objCurrencyRateItem.CurrencyOut.CurrencyCode + "  " + 
                                objCurrencyRateItem.Date.ToShortDateString() + " ���� = " + 
                                objCurrencyRateItem.Value.ToString() + "\n��� ������ : " + iRet.ToString() +
                                "\n������ ��������� ���������.", "��������",
                                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
                            }

                            break;
                        }
                    } // switch( objCurrencyRateItem.State )
                    if( iRet != 0 ) { break; }
                } // for
                if( iRet == 0 )
                {
                    DBTransaction.Commit();
                    bRes = true;
                }
            }
            catch( System.Exception e )
            {
                DBTransaction.Rollback();
                DevExpress.XtraEditors.XtraMessageBox.Show( 
                "�� ������� ��������� ��������� � ��.\n" + e.Message, "��������",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
            }
			finally // ������� ���������� �������
            {
                DBConnection.Close();
            }
            return bRes;
        }

    }
}
