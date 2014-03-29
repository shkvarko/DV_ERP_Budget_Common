using System;
using System.Collections.Generic;
using System.Text;

namespace ERP_Budget.Common
{
    /// <summary>
    /// ������ ����� ������� ������
    /// </summary>
    public enum enumViewMode
    {
        Err = 0,  // ������
        Work,   // �������������� ������
        Worked, // ������������ ������ 
        Arj     // ����� ������
    };
    /// <summary>
    /// ��������� "������ � ���������� ������ ������ �������"
    /// </summary>
    public class CPopupBudgetItem
    {
        public CBudgetItem ParentBudgetItem;
        public System.Guid BudgetDepID;
        public List<CBudgetItem> ChlildBudgetItemList;


        public CPopupBudgetItem()
        {
            this.ParentBudgetItem = null;
            this.BudgetDepID = System.Guid.Empty;
            this.ChlildBudgetItemList = new List<CBudgetItem>();
        }

        public override string ToString()
        {
            return ParentBudgetItem.BudgetItemFullName;
        }

    }
    /// <summary>
    /// ���������, ��������������� ������ � ������� ��������� ���������� ���������
    /// </summary>
    public struct structBudgetDocStateHistory
    {
        /// <summary>
        /// ����
        /// </summary>
        public System.DateTime HistoryDate;
        /// <summary>
        /// ��������� ���������� ���������
        /// </summary>
        public CBudgetDocState BudgetDocState;
        /// <summary>
        /// ������������, ���������� ��������� ���������� ���������
        /// </summary>
        public CUser UserHistory;
    }

    /// <summary>
    /// ����� "������ � ������� ����� ���������� ���������"
    /// </summary>
    public class CBudgetDocPaymentItem
    {
        #region ��������
        /// <summary>
        /// ���������� �������������
        /// </summary>
        public System.Guid ID { get; set; }
        /// <summary>
        /// ���� ������
        /// </summary>
        public System.DateTime PaymentDate { get; set; }
        /// <summary>
        /// ����� �������
        /// </summary>
        public double PaymentValue { get; set; }
        /// <summary>
        /// ������ �������
        /// </summary>
        public CCurrency Currency { get; set; }
        /// <summary>
        /// ����� ������� �����������
        /// </summary>
        public double FactPaymentValue { get; set; }
        /// <summary>
        /// ������ ������� �����������
        /// </summary>
        public CCurrency FactCurrency { get; set; }
        #endregion

        #region ����������
        public CBudgetDocPaymentItem()
        {
            ID = System.Guid.Empty;
            PaymentDate = System.DateTime.MinValue;
            PaymentValue = 0;
            Currency = null;
            FactPaymentValue = 0;
            FactCurrency = null;
        }
        #endregion

        #region ������ ����� ���������� ���������
        /// <summary>
        /// ���������� ������ ����� ���������� ���������
        /// </summary>
        /// <param name="objProfile">�������</param>
        /// <param name="BudgetDoc_Guid">�� ���������� ���������</param>
        /// <param name="cmdSQL">SQL-�������</param>
        /// <param name="strErr">����� ������</param>
        /// <returns>������ �������� ������ "CBudgetDocPaymentItem"</returns>
        public static List<CBudgetDocPaymentItem> GetBudgetDocPaymentItemList(UniXP.Common.CProfile objProfile,
            System.Guid BudgetDoc_Guid,
            System.Data.SqlClient.SqlCommand cmdSQL, ref System.String strErr)
        {
            List<CBudgetDocPaymentItem> objList = new List<CBudgetDocPaymentItem>();
            System.Data.SqlClient.SqlConnection DBConnection = null;
            System.Data.SqlClient.SqlCommand cmd = null;
            try
            {
                if (cmdSQL == null)
                {
                    DBConnection = objProfile.GetDBSource();
                    if (DBConnection == null)
                    {
                        strErr += ("\n�� ������� �������� ���������� � ����� ������.");
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

                cmd.CommandText = System.String.Format("[{0}].[dbo].[usp_GetBudgetDocPayments]", objProfile.GetOptionsDllDBName());
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ((System.Byte)(0)), ((System.Byte)(0)), "", System.Data.DataRowVersion.Current, null));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@BudgetDoc_Guid", System.Data.SqlDbType.UniqueIdentifier));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ERROR_NUM", System.Data.SqlDbType.Int, 8, System.Data.ParameterDirection.Output, false, ((System.Byte)(0)), ((System.Byte)(0)), "", System.Data.DataRowVersion.Current, null));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ERROR_MES", System.Data.SqlDbType.NVarChar, 4000));
                cmd.Parameters["@ERROR_MES"].Direction = System.Data.ParameterDirection.Output;
                cmd.Parameters["@BudgetDoc_Guid"].Value = BudgetDoc_Guid;
                System.Data.SqlClient.SqlDataReader rs = cmd.ExecuteReader();
                if (rs.HasRows)
                {
                    while (rs.Read())
                    {
                        objList.Add(
                            new CBudgetDocPaymentItem()
                            {
                                ID = (System.Guid)rs["GUID_ID"],
                                PaymentValue = ((rs["PAYMENT_MONEY_IN_BUDGETDOC_CURRENCY"] == System.DBNull.Value) ? 0 : System.Convert.ToDouble(rs["PAYMENT_MONEY_IN_BUDGETDOC_CURRENCY"])),
                                PaymentDate = ((rs["PAYMENT_DATE"] == System.DBNull.Value) ? System.DateTime.MinValue : System.Convert.ToDateTime(rs["PAYMENT_DATE"])),
                                Currency = ((rs["CURRENCY_GUID_ID"] == System.DBNull.Value) ? null : new CCurrency() { uuidID = (System.Guid)rs["CURRENCY_GUID_ID"], CurrencyCode = System.Convert.ToString(rs["CURRENCY_CODE"]) }),
                                FactCurrency = ((rs["PAYMENT_CURRENCY_GUID"] == System.DBNull.Value) ? null : new CCurrency() { uuidID = (System.Guid)rs["PAYMENT_CURRENCY_GUID"], CurrencyCode = System.Convert.ToString(rs["PAYMENT_CURRENCY_CODE"]) }),
                                FactPaymentValue = ((rs["PAYMENT_MONEY_IN_BUDGETDOC_CURRENCY"] == System.DBNull.Value) ? 0 : System.Convert.ToDouble(rs["PAYMENT_MONEY"]))
                            }
                            );
                    }
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
                strErr += ("\n�� ������� �������� ������ ����� ���������. ����� ������: " + f.Message);
            }
            return objList;
        }
        #endregion

        #region ������������� ������ � ���� ������
        /// <summary>
        /// �������� ������� ����� ����������� � ��
        /// </summary>
        /// <param name="Payment_Date">���� ������</param>
        /// <param name="Payment_Value">����� �������</param>
        /// <param name="objCurrency">������ �������</param>
        /// <param name="strErr">���� ������</param>
        /// <returns>true - ��� ��������� ������ ��������; false - �������� �� ��������</returns>
        public static System.Boolean IsAllParametersValid(System.DateTime Payment_Date,
            double Payment_Value, System.Guid Currency_Guid,
            double FactPayment_Value, System.Guid FactCurrency_Guid,
            ref System.String strErr)
        {
            System.Boolean bRet = false;
            try
            {
                if (Payment_Date.CompareTo( System.DateTime.MinValue ) == 0)
                {
                    strErr += ("���������� ������� ���� ������.");
                    return bRet;
                }
                if (Payment_Value <= 0)
                {
                    strErr += ("���������� ������� ����� ������ ������ ����.");
                    return bRet;
                }
                if (Currency_Guid.CompareTo(System.Guid.Empty) == 0)
                {
                    strErr += ("���������� ������� ������ ������.");
                    return bRet;
                }
                if (FactPayment_Value <= 0)
                {
                    strErr += ("���������� ������� ����� ����������� ������ ������ ����.");
                    return bRet;
                }
                if (FactCurrency_Guid.CompareTo(System.Guid.Empty) == 0)
                {
                    strErr += ("���������� ������� ������ ����������� ������.");
                    return bRet;
                }
                bRet = true;
            }
            catch (System.Exception f)
            {
                strErr += ("������ �������� ������� ������� \"������ ���������� ���������\". ����� ������: " + f.Message);
            }
            return bRet;
        }
        /// <summary>
        /// ����������� �������� ������� "������ ���������� ���������" � ��
        /// </summary>
        /// <param name="BudgetDocPayment_Guid">�� ������</param>
        /// <param name="Payment_Date">���� ������</param>
        /// <param name="Payment_Value">����� ������</param>
        /// <param name="Currency_Guid">�� ������ ������</param>
        /// <param name="FactPayment_Value">����� ����������� ������</param>
        /// <param name="FactCurrency_Guid">�� ������ ����������� ������</param>
        /// <param name="objProfile">�������</param>
        /// <param name="strErr">����� ������</param>
        /// <param name="LastPayment_Date">���� ��������� ������ ���������� ���������</param>
        /// <returns>true - ������� ���������� ��������; false - ������</returns>
        public static System.Boolean EditObjectInDataBase(System.Guid BudgetDocPayment_Guid,
            System.DateTime Payment_Date, double Payment_Value, System.Guid Currency_Guid,
            double FactPayment_Value, System.Guid FactCurrency_Guid,
           UniXP.Common.CProfile objProfile, ref System.String strErr, ref System.DateTime LastPayment_Date)
        {
            System.Boolean bRet = false;

            if (IsAllParametersValid(Payment_Date, Payment_Value, Currency_Guid, 
                  FactPayment_Value, FactCurrency_Guid,  ref strErr) == false) { return bRet; }

            System.Data.SqlClient.SqlConnection DBConnection = null;
            System.Data.SqlClient.SqlCommand cmd = null;
            System.Data.SqlClient.SqlTransaction DBTransaction = null;

            try
            {
                DBConnection = objProfile.GetDBSource();
                if (DBConnection == null)
                {
                    strErr += ("�� ������� �������� ���������� � ����� ������.");
                    return bRet;
                }
                DBTransaction = DBConnection.BeginTransaction();
                cmd = new System.Data.SqlClient.SqlCommand();
                cmd.Connection = DBConnection;
                cmd.Transaction = DBTransaction;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = System.String.Format("[{0}].[dbo].[usp_EditBudgetDocPaymentItem]", objProfile.GetOptionsDllDBName());
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ((System.Byte)(0)), ((System.Byte)(0)), "", System.Data.DataRowVersion.Current, null));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@BudgetDocPayment_Guid", System.Data.SqlDbType.UniqueIdentifier));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Payment_Date", System.Data.SqlDbType.DateTime));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@PaymentBudgetDocCurrency_Value", System.Data.SqlDbType.Money));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Currency_Guid", System.Data.SqlDbType.UniqueIdentifier));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Payment_Value", System.Data.SqlDbType.Money));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ERROR_NUM", System.Data.SqlDbType.Int, 8, System.Data.ParameterDirection.Output, false, ((System.Byte)(0)), ((System.Byte)(0)), "", System.Data.DataRowVersion.Current, null));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ERROR_MES", System.Data.SqlDbType.NVarChar, 4000));
                cmd.Parameters["@ERROR_MES"].Direction = System.Data.ParameterDirection.Output;
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@LastPayment_Date", System.Data.SqlDbType.DateTime, 4000));
                cmd.Parameters["@LastPayment_Date"].Direction = System.Data.ParameterDirection.Output;
                cmd.Parameters["@BudgetDocPayment_Guid"].Value = BudgetDocPayment_Guid;
                cmd.Parameters["@Payment_Date"].Value = Payment_Date;
                cmd.Parameters["@PaymentBudgetDocCurrency_Value"].Value = Payment_Value;
                cmd.Parameters["@Payment_Value"].Value = FactPayment_Value;
                cmd.Parameters["@Currency_Guid"].Value = FactCurrency_Guid;
                cmd.ExecuteNonQuery();
                System.Int32 iRes = (System.Int32)cmd.Parameters["@RETURN_VALUE"].Value;
                LastPayment_Date = System.Convert.ToDateTime(cmd.Parameters["@LastPayment_Date"].Value);
                if (iRes == 0)
                {
                    // ������������ ����������
                    DBTransaction.Commit();
                }
                else
                {
                    DBTransaction.Rollback();
                    strErr += ((cmd.Parameters["@ERROR_MES"].Value == System.DBNull.Value) ? "" : (System.String)cmd.Parameters["@ERROR_MES"].Value);
                }

                cmd.Dispose();
                bRet = (iRes == 0);
            }
            catch (System.Exception f)
            {
                DBTransaction.Rollback();

                strErr += ("�� ������� ������ ��������� � ������ \"������ ���������� ���������\". ����� ������: " + f.Message);
            }
            finally
            {
                DBConnection.Close();
            }
            return bRet;
        }
        /// <summary>
        /// ��������� ��������� � ������ ����� ������ � ��
        /// </summary>
        /// <param name="objList">������ �����</param>
        /// <param name="objProfile">�������</param>
        /// <param name="strErr">����� ������</param>
        /// <param name="LastPayment_Date">���� ��������� ������ ���������� ���������</param>
        /// <returns>true - ������� ���������� ��������; false - ������</returns>
        public static System.Boolean EditObjectListInDataBase(List<CBudgetDocPaymentItem> objList,
           UniXP.Common.CProfile objProfile, ref System.String strErr, ref System.DateTime LastPayment_Date)
        {
            System.Boolean bRet = false;

            System.Boolean bIsAllParametersValid = true;
            foreach (CBudgetDocPaymentItem objItem in objList)
            {
                if (IsAllParametersValid(objItem.PaymentDate, objItem.PaymentValue, objItem.Currency.uuidID, 
                    objItem.FactPaymentValue, objItem.FactCurrency.uuidID, ref strErr) == false) 
                {
                    bIsAllParametersValid = false;
                    break;
                }
            }
            if (bIsAllParametersValid == false) { return bRet; }

            System.Data.SqlClient.SqlConnection DBConnection = null;
            System.Data.SqlClient.SqlCommand cmd = null;
            System.Data.SqlClient.SqlTransaction DBTransaction = null;

            try
            {
                DBConnection = objProfile.GetDBSource();
                if (DBConnection == null)
                {
                    strErr += ("�� ������� �������� ���������� � ����� ������.");
                    return bRet;
                }
                DBTransaction = DBConnection.BeginTransaction();
                cmd = new System.Data.SqlClient.SqlCommand();
                cmd.Connection = DBConnection;
                cmd.Transaction = DBTransaction;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = System.String.Format("[{0}].[dbo].[usp_EditBudgetDocPaymentItem]", objProfile.GetOptionsDllDBName());
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ((System.Byte)(0)), ((System.Byte)(0)), "", System.Data.DataRowVersion.Current, null));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@BudgetDocPayment_Guid", System.Data.SqlDbType.UniqueIdentifier));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Payment_Date", System.Data.SqlDbType.DateTime));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@PaymentBudgetDocCurrency_Value", System.Data.SqlDbType.Money));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Currency_Guid", System.Data.SqlDbType.UniqueIdentifier));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Payment_Value", System.Data.SqlDbType.Money));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ERROR_NUM", System.Data.SqlDbType.Int, 8, System.Data.ParameterDirection.Output, false, ((System.Byte)(0)), ((System.Byte)(0)), "", System.Data.DataRowVersion.Current, null));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ERROR_MES", System.Data.SqlDbType.NVarChar, 4000));
                cmd.Parameters["@ERROR_MES"].Direction = System.Data.ParameterDirection.Output;
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@LastPayment_Date", System.Data.SqlDbType.DateTime, 4000));
                cmd.Parameters["@LastPayment_Date"].Direction = System.Data.ParameterDirection.Output;
                System.Int32 iRes = 0;

                foreach (CBudgetDocPaymentItem objItem in objList)
                {
                    cmd.Parameters["@BudgetDocPayment_Guid"].Value = objItem.ID;
                    cmd.Parameters["@Payment_Date"].Value = objItem.PaymentDate;
                    cmd.Parameters["@PaymentBudgetDocCurrency_Value"].Value = objItem.PaymentValue;
                    cmd.Parameters["@Payment_Value"].Value = objItem.FactPaymentValue;
                    cmd.Parameters["@Currency_Guid"].Value = objItem.FactCurrency.uuidID;
                    cmd.ExecuteNonQuery();

                    iRes = (System.Int32)cmd.Parameters["@RETURN_VALUE"].Value;
                    LastPayment_Date = System.Convert.ToDateTime(cmd.Parameters["@LastPayment_Date"].Value);
                    if (iRes != 0) { break; }
                }
                
                if (iRes == 0)
                {
                    // ������������ ����������
                    DBTransaction.Commit();
                }
                else
                {
                    DBTransaction.Rollback();
                    strErr += ((cmd.Parameters["@ERROR_MES"].Value == System.DBNull.Value) ? "" : (System.String)cmd.Parameters["@ERROR_MES"].Value);
                }

                cmd.Dispose();
                bRet = (iRes == 0);
            }
            catch (System.Exception f)
            {
                DBTransaction.Rollback();

                strErr += ("�� ������� ������ ��������� � ������ �������� \"������ ���������� ���������\". ����� ������: " + f.Message);
            }
            finally
            {
                DBConnection.Close();
            }
            return bRet;
        }
        
        #endregion

    }

    public class CBudgetDocPaymentItemArjive
    {
        #region ��������
        /// <summary>
        /// ������ �� ������ � ������� �����
        /// </summary>
        public System.Guid ID { get; set; }
        /// <summary>
        /// ���� ����������� �������� ������
        /// </summary>
        public System.DateTime RecordUpdated { get; set; }
        /// <summary>
        /// ������ �� ������������, ���������������� ������ 
        /// </summary>
        public System.String RecordUserUdpated { get; set; }
        /// <summary>
        /// ���� ������ �����
        /// </summary>
        public System.DateTime PaymentDateNew { get; set; }
        /// <summary>
        /// ���� ������ ����������
        /// </summary>
        public System.DateTime PaymentDateOld { get; set; }
        /// <summary>
        /// ����� ������� �����
        /// </summary>
        public double PaymentValueNew { get; set; }
        /// <summary>
        /// ����� ������� ����������
        /// </summary>
        public double PaymentValueOld { get; set; }
        /// <summary>
        /// ����� ������� �� ����� �����
        /// </summary>
        public double FactPaymentValueNew { get; set; }
        /// <summary>
        /// ����� �������  �� ����� ����������
        /// </summary>
        public double FactPaymentValueOld { get; set; }
        /// <summary>
        /// ������ ����������� ������ ����������
        /// </summary>
        public CCurrency FactCurrencyOld { get; set; }
        /// <summary>
        /// ������ ����������� ������ �����
        /// </summary>
        public CCurrency FactCurrencyNew { get; set; }
        #endregion

        #region �����������
        public CBudgetDocPaymentItemArjive()
        {
            ID = System.Guid.Empty;
            RecordUpdated = System.DateTime.MinValue;
            PaymentDateNew = System.DateTime.MinValue;
            PaymentDateOld = System.DateTime.MinValue;
            RecordUserUdpated = System.String.Empty;
            PaymentValueNew = 0;
            PaymentValueOld = 0;
            FactPaymentValueNew = 0;
            FactPaymentValueOld = 0;
            FactCurrencyOld = null;
            FactCurrencyNew = null;
        }
        #endregion

        #region ����� ��������� ���� ������
        /// <summary>
        /// ���������� ����� ����� ���������� ���������
        /// </summary>
        /// <param name="objProfile">�������</param>
        /// <param name="BudgetDoc_Guid">�� ���������� ���������</param>
        /// <param name="cmdSQL">SQL-�������</param>
        /// <param name="strErr">����� ������</param>
        /// <returns>������ �������� ������ "CBudgetDocPaymentItem"</returns>
        public static List<CBudgetDocPaymentItemArjive> GetBudgetDocPaymentItemArjive(UniXP.Common.CProfile objProfile,
            System.Guid BudgetDoc_Guid,
            System.Data.SqlClient.SqlCommand cmdSQL, ref System.String strErr)
        {
            List<CBudgetDocPaymentItemArjive> objList = new List<CBudgetDocPaymentItemArjive>();
            System.Data.SqlClient.SqlConnection DBConnection = null;
            System.Data.SqlClient.SqlCommand cmd = null;
            try
            {
                if (cmdSQL == null)
                {
                    DBConnection = objProfile.GetDBSource();
                    if (DBConnection == null)
                    {
                        strErr += ("\n�� ������� �������� ���������� � ����� ������.");
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

                cmd.CommandText = System.String.Format("[{0}].[dbo].[usp_GetBudgetDocPaymentsLog]", objProfile.GetOptionsDllDBName());
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ((System.Byte)(0)), ((System.Byte)(0)), "", System.Data.DataRowVersion.Current, null));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@BudgetDoc_Guid", System.Data.SqlDbType.UniqueIdentifier));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ERROR_NUM", System.Data.SqlDbType.Int, 8, System.Data.ParameterDirection.Output, false, ((System.Byte)(0)), ((System.Byte)(0)), "", System.Data.DataRowVersion.Current, null));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ERROR_MES", System.Data.SqlDbType.NVarChar, 4000));
                cmd.Parameters["@ERROR_MES"].Direction = System.Data.ParameterDirection.Output;
                cmd.Parameters["@BudgetDoc_Guid"].Value = BudgetDoc_Guid;
                System.Data.SqlClient.SqlDataReader rs = cmd.ExecuteReader();
                if (rs.HasRows)
                {
                    while (rs.Read())
                    {
                        objList.Add(
                            new CBudgetDocPaymentItemArjive()
                            {
                                ID = ((rs["BUDGETDOC_PAYMENTS_GUID_ID"] == System.DBNull.Value) ? System.Guid.Empty : (System.Guid)rs["BUDGETDOC_PAYMENTS_GUID_ID"]),
                                PaymentValueOld = ((rs["PAYMENT_MONEY_IN_BUDGETDOC_CURRENCY_OLD"] == System.DBNull.Value) ? 0 : System.Convert.ToDouble(rs["PAYMENT_MONEY_IN_BUDGETDOC_CURRENCY_OLD"])),
                                PaymentValueNew = ((rs["PAYMENT_MONEY_IN_BUDGETDOC_CURRENCY_NEW"] == System.DBNull.Value) ? 0 : System.Convert.ToDouble(rs["PAYMENT_MONEY_IN_BUDGETDOC_CURRENCY_NEW"])),
                                PaymentDateOld = ((rs["PAYMENT_DATE_OLD"] == System.DBNull.Value) ? System.DateTime.MinValue : System.Convert.ToDateTime(rs["PAYMENT_DATE_OLD"])),
                                PaymentDateNew = ((rs["PAYMENT_DATE_NEW"] == System.DBNull.Value) ? System.DateTime.MinValue : System.Convert.ToDateTime(rs["PAYMENT_DATE_NEW"])),
                                RecordUpdated = ((rs["Record_Updated"] == System.DBNull.Value) ? System.DateTime.MinValue : System.Convert.ToDateTime(rs["Record_Updated"])),
                                RecordUserUdpated = ((rs["Record_UserUdpated"] == System.DBNull.Value) ? System.String.Empty : System.Convert.ToString(rs["Record_UserUdpated"])),
                                FactPaymentValueOld = ((rs["PAYMENT_MONEY_OLD"] == System.DBNull.Value) ? 0 : System.Convert.ToDouble(rs["PAYMENT_MONEY_OLD"])),
                                FactPaymentValueNew = ((rs["PAYMENT_MONEY_NEW"] == System.DBNull.Value) ? 0 : System.Convert.ToDouble(rs["PAYMENT_MONEY_NEW"])),
                                FactCurrencyOld = ((rs["PAYMENT_CURRENCY_GUID_OLD"] == System.DBNull.Value) ? null : new CCurrency() { uuidID = (System.Guid)rs["PAYMENT_CURRENCY_GUID_OLD"], CurrencyCode = System.Convert.ToString(rs["PAYMENT_CURRENCY_CODE_OLD"]) }),
                                FactCurrencyNew = ((rs["PAYMENT_CURRENCY_GUID_NEW"] == System.DBNull.Value) ? null : new CCurrency() { uuidID = (System.Guid)rs["PAYMENT_CURRENCY_GUID_NEW"], CurrencyCode = System.Convert.ToString(rs["PAYMENT_CURRENCY_CODE_NEW"]) })
                            }
                            );
                    }
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
                strErr += ("\n�� ������� �������� ������ ����� ���������. ����� ������: " + f.Message);
            }
            return objList;
        }

        #endregion

    }

    /// <summary>
    /// ����� "��������� ��������"
    /// </summary>
    public class CBudgetDoc : IBaseListItem
    {
        #region ����������, ��������, ���������
        /// <summary>
        /// ��� ������� ������
        /// </summary>
        private enumViewMode m_ViewMode;
        /// <summary>
        /// ��� ������� ������
        /// </summary>
        public enumViewMode ViewMode
        {
            get { return m_ViewMode; }
            set { m_ViewMode = value; }
        }
        /// <summary>
        /// ����
        /// </summary>
        private System.DateTime m_dtDate;
        /// <summary>
        /// �����
        /// </summary>
        private double m_moneyMoney;
        /// <summary>
        /// ����� ����������
        /// </summary>
        private double m_moneyMoneyPayment;
        /// <summary>
        /// ����
        /// </summary>
        private string m_strObjective;
        /// <summary>
        /// ���� ������
        /// </summary>
        private System.DateTime m_dtPaymentDate;
        /// <summary>
        /// ���������� �������
        /// </summary>
        private string m_strRecipient;
        /// <summary>
        /// �������������� �����������
        /// </summary>
        private string m_strDocBasis;
        /// <summary>
        /// ��������
        /// </summary>
        private string m_strDescription;
        /// <summary>
        /// ����� ������������
        /// </summary>
        private double m_moneyMoneyAgree;
        /// <summary>
        /// ������� "�������"
        /// </summary>
        private bool m_bIsActive;
        /// <summary>
        /// ������� ���������� ���������
        /// </summary>
        private CBudgetRoute m_objRoute;
        /// <summary>
        /// ��������� ���������� ���������
        /// </summary>
        private CBudgetDocState m_objDocState;
        /// <summary>
        /// ��� ���������� ���������
        /// </summary>
        private CBudgetDocType m_objDocType;
        /// <summary>
        /// ������� ���������
        /// </summary>
        private CMeasure m_objMeasure;
        /// <summary>
        /// ����� �������
        /// </summary>
        private CPaymentType m_objPaymentType;
        /// <summary>
        /// ������
        /// </summary>
        private CCurrency m_objCurrency;
        /// <summary>
        /// ��������
        /// </summary>
        private CCompany m_objCompany;
        /// <summary>
        /// ������������ ������ �������, � ������� ����� ������� �����
        /// </summary>
        private CBudgetItem m_objBudgetItem;
        /// <summary>
        /// ������ ������ �������, � ������� ����� ������� �����
        /// </summary>
        private List<CBudgetItem> m_objBudgetItemList;
        /// <summary>
        /// ��������� �������������
        /// </summary>
        private CBudgetDep m_objBudgetDep;
        /// <summary>
        /// ����
        /// </summary>
        public System.DateTime Date
        {
            get { return m_dtDate; }
            set { m_dtDate = value; }
        }
        /// <summary>
        /// �����
        /// </summary>
        public double Money
        {
            get { return m_moneyMoney; }
            set { m_moneyMoney = value; }
        }
        /// <summary>
        /// ����� ����������
        /// </summary>
        public double MoneyPayment
        {
            get { return m_moneyMoneyPayment; }
            set { m_moneyMoneyPayment = value; }
        }
        /// <summary>
        /// ����
        /// </summary>
        public string Objective
        {
            get { return m_strObjective; }
            set { m_strObjective = value; }
        }
        /// <summary>
        /// ���� ������
        /// </summary>
        public System.DateTime PaymentDate
        {
            get { return m_dtPaymentDate; }
            set { m_dtPaymentDate = value; }
        }
        /// <summary>
        /// ���������� �������
        /// </summary>
        public string Recipient
        {
            get { return m_strRecipient; }
            set { m_strRecipient = value; }
        }
        /// <summary>
        /// �������������� �����������
        /// </summary>
        public string DocBasis
        {
            get { return m_strDocBasis; }
            set { m_strDocBasis = value; }
        }
        /// <summary>
        /// ��������
        /// </summary>
        public string Description
        {
            get { return m_strDescription; }
            set { m_strDescription = value; }
        }
        /// <summary>
        /// ����� ������������
        /// </summary>
        public double MoneyAgree
        {
            get { return m_moneyMoneyAgree; }
            set { m_moneyMoneyAgree = value; }
        }
        /// <summary>
        /// ����� ���������� �� ���������� ���������
        /// </summary>
        private double m_moneyMoneyTrnSum;
        /// <summary>
        /// ����� ���������� �� ���������� ���������
        /// </summary>
        public double MoneyTrnSum
        {
            get { return m_moneyMoneyTrnSum; }
            set { m_moneyMoneyTrnSum = value; }
        }
        /// <summary>
        /// ������� "�������"
        /// </summary>
        public bool IsActive
        {
            get { return m_bIsActive; }
            set { m_bIsActive = value; }
        }
        /// <summary>
        /// ������� ���������� ���������
        /// </summary>
        public CBudgetRoute Route
        {
            get { return m_objRoute; }
            set { m_objRoute = value; }
        }
        /// <summary>
        /// ��������� ���������� ���������
        /// </summary>
        public CBudgetDocState DocState
        {
            get { return m_objDocState; }
            set { m_objDocState = value; }
        }
        /// <summary>
        /// ��� ���������� ���������
        /// </summary>
        public CBudgetDocType DocType
        {
            get { return m_objDocType; }
            set { m_objDocType = value; }
        }
        /// <summary>
        /// ������� ���������
        /// </summary>
        public CMeasure Measure
        {
            get { return m_objMeasure; }
            set { m_objMeasure = value; }
        }
        /// <summary>
        /// ����� �������
        /// </summary>
        public CPaymentType PaymentType
        {
            get { return m_objPaymentType; }
            set { m_objPaymentType = value; }
        }
        /// <summary>
        /// ������
        /// </summary>
        public CCurrency Currency
        {
            get { return m_objCurrency; }
            set { m_objCurrency = value; }
        }
        /// <summary>
        /// ��������
        /// </summary>
        public CCompany Company
        {
            get { return m_objCompany; }
            set { m_objCompany = value; }
        }
        /// <summary>
        /// ������������ ������ �������
        /// </summary>
        public CBudgetItem BudgetItem
        {
            get { return m_objBudgetItem; }
            set { m_objBudgetItem = value; }
        }
        /// <summary>
        /// ������ ������ �������, � ������� ����� ������� �����
        /// </summary>
        public List<CBudgetItem> BudgetItemList
        {
            get { return m_objBudgetItemList; }
            set { m_objBudgetItemList = value; }
        }
        /// <summary>
        /// ��������� �������������
        /// </summary>
        public CBudgetDep BudgetDep
        {
            get { return m_objBudgetDep; }
            set { m_objBudgetDep = value; }
        }
        /// <summary>
        /// ���������
        /// </summary>
        private CUser m_OwnerUser;
        /// <summary>
        /// ���������
        /// </summary>
        public CUser OwnerUser
        {
            get { return m_OwnerUser; }
            set { m_OwnerUser = value; }
        }
        /// <summary>
        /// ��������� ����, ��� �� ����������� ��� ���������� ��������� �������� ����������
        /// </summary>
        public System.Boolean IsReadyForSave
        {
            get { return CheckReadyForSave(); }
        }
        /// <summary>
        /// ������ ���������� �������� ��������� �������������
        /// </summary>
        private System.Collections.Generic.List<CBudgetDep> m_PopupBudgetDepList;
        /// <summary>
        /// ������ ���������� �������� ��������� �������������
        /// </summary>
        public System.Collections.Generic.List<CBudgetDep> PopupBudgetDepList
        {
            get { return m_PopupBudgetDepList; }
            set { m_PopupBudgetDepList = value; }
        }
        /// <summary>
        /// ������ ���������� �������� ���� ������
        /// </summary>
        private System.Collections.Generic.List<CPaymentType> m_PopupPaymentTypeList;
        /// <summary>
        /// ������ ���������� �������� ���� ������
        /// </summary>
        public System.Collections.Generic.List<CPaymentType> PopupPaymentTypeList
        {
            get { return m_PopupPaymentTypeList; }
            set { m_PopupPaymentTypeList = value; }
        }
        /// <summary>
        /// ������ ���������� �������� ������ ���������
        /// </summary>
        private System.Collections.Generic.List<CMeasure> m_PopupMeasureList;
        /// <summary>
        /// ������ ���������� �������� ������ ���������
        /// </summary>
        public System.Collections.Generic.List<CMeasure> PopupMeasureList
        {
            get { return m_PopupMeasureList; }
            set { m_PopupMeasureList = value; }
        }
        /// <summary>
        /// ������ ���������� �������� �����
        /// </summary>
        private System.Collections.Generic.List<CCurrency> m_PopupCurrencyList;
        /// <summary>
        /// ������ ���������� �������� �����
        /// </summary>
        public System.Collections.Generic.List<CCurrency> PopupCurrencyList
        {
            get { return m_PopupCurrencyList; }
            set { m_PopupCurrencyList = value; }
        }
        /// <summary>
        /// ������ ���������� �������� ��������� ���������� ���������
        /// </summary>
        private System.Collections.Generic.List<CBudgetDocState> m_PopupBudgetDocStateList;
        /// <summary>
        /// ������ ���������� �������� ��������� ���������� ���������
        /// </summary>
        public System.Collections.Generic.List<CBudgetDocState> PopupBudgetDocStateList
        {
            get { return m_PopupBudgetDocStateList; }
            set { m_PopupBudgetDocStateList = value; }
        }
        /// <summary>
        /// ������ ���������� �������� ����� ���������� ���������
        /// </summary>
        private System.Collections.Generic.List<CBudgetDocType> m_PopupBudgetDocTypeList;
        /// <summary>
        /// ������ ���������� �������� ����� ���������� ���������
        /// </summary>
        public System.Collections.Generic.List<CBudgetDocType> PopupBudgetDocTypeList
        {
            get { return m_PopupBudgetDocTypeList; }
            set { m_PopupBudgetDocTypeList = value; }
        }
        /// <summary>
        /// ���������� ������������� �������
        /// </summary>
        private System.Guid m_uuidBudgetID;
        /// <summary>
        /// ���������� ������������� �������
        /// </summary>
        public System.Guid uuidBudgetID
        {
            get { return m_uuidBudgetID; }
            set { m_uuidBudgetID = value; }
        }
        /// <summary>
        /// ������ ���������� �������� ������ �������
        /// </summary>
        private System.Collections.Generic.List<CPopupBudgetItem> m_PopupBudgetItemList;
        /// <summary>
        /// ������ ���������� �������� ������ �������
        /// </summary>
        public System.Collections.Generic.List<CPopupBudgetItem> PopupBudgetItemList
        {
            get { return m_PopupBudgetItemList; }
            set { m_PopupBudgetItemList = value; }
        }
        /// <summary>
        /// ������ ���������� �������� ��������
        /// </summary>
        private System.Collections.Generic.List<CCompany> m_PopupCompanyList;
        /// <summary>
        /// ������ ���������� �������� ��������
        /// </summary>
        public System.Collections.Generic.List<CCompany> PopupCompanyList
        {
            get { return m_PopupCompanyList; }
            set { m_PopupCompanyList = value; }
        }
        /// <summary>
        /// �������� �������
        /// </summary>
        private System.Double m_BudgetDocDeficit;
        /// <summary>
        /// �������� �������
        /// </summary>
        public System.Double Deficit
        {
            get { return m_BudgetDocDeficit; }
            set { m_BudgetDocDeficit = value; }
        }
        private System.Boolean m_bDivision;
        public System.Boolean Division
        {
            get { return m_bDivision; }
            set { m_bDivision = value; }
        }
        /// <summary>
        /// ���� ��������� ���������� ���������
        /// </summary>
        private System.DateTime m_dtDateState;
        /// <summary>
        /// ���� ��������� ���������� ���������
        /// </summary>
        public System.DateTime DateState
        {
            get { return m_dtDateState; }
            set { m_dtDateState = value; }
        }
        /// <summary>
        /// ������ ������������� ��������
        /// </summary>
        private List<CBudgetDocAttach> m_objAttachList;
        /// <summary>
        /// ������ ������������� ��������
        /// </summary>
        public List<CBudgetDocAttach> AttachList
        {
            get { return m_objAttachList; }
            set { m_objAttachList = value; }
        }
        /// <summary>
        /// ������ �� ������������� "������������� ���������"
        /// </summary>
        private System.Guid m_uuidSrcDocID;
        /// <summary>
        /// ������ �� ������������� "������������� ���������"
        /// </summary>
        public System.Guid SrcDocID
        {
            get { return m_uuidSrcDocID; }
            set { m_uuidSrcDocID = value; }
        }
        /// <summary>
        /// ������� ����, ��� � ��������� ����������� ��������
        /// </summary>
        private System.Boolean m_bExistsAttach;
        /// <summary>
        /// ������� ����, ��� � ��������� ����������� ��������
        /// </summary>
        public System.Boolean ExistsAttach
        {
            get { return m_bExistsAttach; }
            set { m_bExistsAttach = value; }
        }
        /// <summary>
        /// ������� ����, ��� ���� �������� "��������� �������" 
        /// </summary>
        private System.Boolean m_bResetBudgetItem;
        /// <summary>
        /// ������� ����, ��� ���� �������� "��������� �������" 
        /// </summary>
        public System.Boolean IsResetBudgetItem
        {
            get { return m_bResetBudgetItem; }
            set { m_bResetBudgetItem = value; }
        }
        /// <summary>
        /// �� �������
        /// </summary>
        private System.Guid m_uuidNoteTypeID;
        /// <summary>
        /// �� �������
        /// </summary>
        public System.Guid NoteTypeID
        {
            get { return m_uuidNoteTypeID; }
            set { m_uuidNoteTypeID = value; }
        }
        /// <summary>
        /// ����������� ������������
        /// </summary>
        private System.String m_strUserComment;
        /// <summary>
        /// ����������� ������������
        /// </summary>
        public System.String UserComment
        {
            get { return m_strUserComment; }
            set { m_strUserComment = value; }
        }
        private const System.Int32 iCommandTimeout = 600;
        /// <summary>
        /// ������
        /// </summary>
        public CBudgetProject BudgetProject { get; set; }
        /// <summary>
        /// ����
        /// </summary>
        public CAccountPlan AccountPlan { get; set; }
        /// <summary>
        /// ��� ���������
        /// </summary>
        public CBudgetDocCategory BudgetDocCategory { get; set; }
        #endregion

        #region ������������

        public CBudgetDoc()
        {
            this.m_uuidID = System.Guid.Empty;
            this.m_uuidBudgetID = System.Guid.Empty;
            this.m_uuidNoteTypeID = System.Guid.Empty;
            this.m_strName = "";
            this.m_bIsActive = false;
            this.m_dtDate = System.DateTime.Now;
            this.m_dtPaymentDate = System.DateTime.Now;
            this.m_dtDateState = System.DateTime.Now;
            this.m_moneyMoney = 0;
            this.m_moneyMoneyAgree = 0;
            this.m_moneyMoneyTrnSum = 0;
            this.m_objBudgetDep = null;
            this.m_objBudgetItem = null;
            this.m_objBudgetItemList = new List<CBudgetItem>();
            this.m_objCompany = null;
            this.m_objCurrency = null;
            this.m_objDocState = null;
            this.m_objDocType = null;
            this.m_objMeasure = null;
            this.m_objPaymentType = null;
            this.m_objRoute = null;
            this.m_strDescription = "";
            this.m_strDocBasis = "";
            this.m_strObjective = "";
            this.m_strRecipient = "";
            this.m_PopupBudgetDepList = new List<CBudgetDep>();
            this.m_PopupPaymentTypeList = new List<CPaymentType>();
            this.m_PopupMeasureList = new List<CMeasure>();
            this.m_PopupCurrencyList = new List<CCurrency>();
            this.m_PopupBudgetDocStateList = new List<CBudgetDocState>();
            this.m_PopupBudgetDocTypeList = new List<CBudgetDocType>();
            this.m_PopupBudgetItemList = new List<CPopupBudgetItem>();
            this.m_PopupCompanyList = new List<CCompany>();
            this.m_objAttachList = null;
            this.m_OwnerUser = null;
            this.ViewMode = enumViewMode.Err;
            this.m_BudgetDocDeficit = 0;
            this.m_bDivision = false;
            this.m_uuidSrcDocID = System.Guid.Empty;
            this.m_bExistsAttach = false;
            this.m_bResetBudgetItem = false;
            this.m_strUserComment = "";
            this.m_moneyMoneyPayment = 0;
            BudgetProject = null;
            AccountPlan = null;
            BudgetDocCategory = null;
        }

        public CBudgetDoc(System.Guid uuidID, System.DateTime dtDate, System.String strObjective,
            System.Double mMoney, CCurrency objCurrency, CBudgetDep objBudgetDep)
        {
            this.m_uuidID = uuidID;
            this.m_uuidBudgetID = System.Guid.Empty;
            this.m_uuidNoteTypeID = System.Guid.Empty;
            this.m_strName = "";
            this.m_bIsActive = false;
            this.m_dtDate = dtDate;
            this.m_dtPaymentDate = System.DateTime.Now;
            this.m_dtDateState = System.DateTime.Now;
            this.m_moneyMoney = mMoney;
            this.m_moneyMoneyAgree = 0;
            this.m_moneyMoneyTrnSum = 0;
            this.m_objBudgetDep = objBudgetDep;
            this.m_objBudgetItem = null;
            this.m_objBudgetItemList = new List<CBudgetItem>();
            this.m_objCompany = null;
            this.m_objCurrency = objCurrency;
            this.m_objDocState = null;
            this.m_objDocType = null;
            this.m_objMeasure = null;
            this.m_objPaymentType = null;
            this.m_objRoute = null;
            this.m_strDescription = "";
            this.m_strDocBasis = "";
            this.m_strObjective = strObjective;
            this.m_strRecipient = "";
            this.m_PopupBudgetDepList = new List<CBudgetDep>();
            this.m_PopupPaymentTypeList = new List<CPaymentType>();
            this.m_PopupMeasureList = new List<CMeasure>();
            this.m_PopupCurrencyList = new List<CCurrency>();
            this.m_PopupBudgetDocStateList = new List<CBudgetDocState>();
            this.m_PopupBudgetDocTypeList = new List<CBudgetDocType>();
            this.m_PopupBudgetItemList = new List<CPopupBudgetItem>();
            this.m_PopupCompanyList = new List<CCompany>();
            this.m_objAttachList = null;
            this.m_OwnerUser = null;
            this.ViewMode = enumViewMode.Err;
            this.m_BudgetDocDeficit = 0;
            this.m_bDivision = false;
            this.m_uuidSrcDocID = System.Guid.Empty;
            this.m_bExistsAttach = false;
            this.m_bResetBudgetItem = false;
            this.m_strUserComment = "";
            this.m_moneyMoneyPayment = 0;
            BudgetProject = null;
            AccountPlan = null;
            BudgetDocCategory = null;
        }

        #endregion

        #region ������ ��������� ����������
        /// <summary>
        /// ��������� ������ ��������� ����������
        /// </summary>
        /// <param name="objProfile">�������</param>
        /// <param name="objBudgetDocList">������ �������� ������</param>
        /// <param name="objBudjetDocArjList">������ ���������� ������</param>
        public static void ReloadBudgetDocList(UniXP.Common.CProfile objProfile,
            List<ERP_Budget.Common.CBudgetDoc> objBudgetDocList,
            List<ERP_Budget.Common.CBudgetDoc> objBudjetDocArjList,
            System.DateTime dtBeginDate, System.DateTime dtEndDate)
        {
            if (objBudgetDocList == null)
            {
                objBudgetDocList = new List<ERP_Budget.Common.CBudgetDoc>();
            }
            if (objBudjetDocArjList == null)
            {
                objBudjetDocArjList = new List<ERP_Budget.Common.CBudgetDoc>();
            }

            System.Data.SqlClient.SqlConnection DBConnection = objProfile.GetDBSource();
            if (DBConnection == null)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(
                "����������� ���������� � ��!", "��������",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                return;
            }

            try
            {
                // ���������� � �� ��������, ����������� ������� �� ������� ������
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
                cmd.CommandTimeout = iCommandTimeout;
                cmd.Connection = DBConnection;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = System.String.Format("[{0}].[dbo].[sp_GetBudgetDocList]", objProfile.GetOptionsDllDBName());
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ((System.Byte)(0)), ((System.Byte)(0)), "", System.Data.DataRowVersion.Current, null));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ulUniXPID", System.Data.SqlDbType.Int));
                if (dtBeginDate != null)
                {
                    cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@BEGINDATE", System.Data.SqlDbType.DateTime));
                    cmd.Parameters["@BEGINDATE"].Value = dtBeginDate;
                }
                if (dtEndDate != null)
                {
                    cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ENDDATE", System.Data.SqlDbType.DateTime));
                    cmd.Parameters["@ENDDATE"].Value = dtEndDate;
                }
                cmd.Parameters["@ulUniXPID"].Value = objProfile.m_nSQLUserID;
                System.Data.SqlClient.SqlDataReader rs = cmd.ExecuteReader();
                if (rs.HasRows)
                {
                    // ����� ������ ��������
                    CBudgetDoc objBudgetDoc = null;
                    while (rs.Read())
                    {
                        objBudgetDoc = new CBudgetDoc();

                        objBudgetDoc.m_uuidID = (System.Guid)rs["GUID_ID"];
                        objBudgetDoc.m_dtDate = (System.DateTime)rs["BUDGETDOC_DATE"];
                        objBudgetDoc.m_moneyMoney = System.Convert.ToDouble(rs["BUDGETDOC_MONEY"]);
                        objBudgetDoc.m_moneyMoneyAgree = System.Convert.ToDouble(rs["BUDGETDOC_MONEYAGREE"]);
                        if (rs["ACCTRNSUM"] != System.DBNull.Value)
                        {
                            objBudgetDoc.m_moneyMoneyTrnSum = System.Convert.ToDouble(rs["ACCTRNSUM"]);
                        }
                        objBudgetDoc.m_strObjective = (System.String)rs["BUDGETDOC_OBJECTIVE"];
                        objBudgetDoc.m_dtPaymentDate = (System.DateTime)rs["BUDGETDOC_PAYMENTDATE"];
                        objBudgetDoc.m_strRecipient = (System.String)rs["BUDGETDOC_RECIPIENT"];
                        objBudgetDoc.m_strDocBasis = (System.String)rs["BUDGETDOC_DOCBASIS"];
                        if (rs["DATESTATE"] != System.DBNull.Value)
                        {
                            objBudgetDoc.DateState = (System.DateTime)rs["DATESTATE"];
                        }
                        if (rs["BUDGETDOC_DESCRIPTION"] != System.DBNull.Value)
                        {
                            objBudgetDoc.m_strDescription = (System.String)rs["BUDGETDOC_DESCRIPTION"];
                        }
                        objBudgetDoc.m_bIsActive = (System.Boolean)rs["BUDGETDOC_ACTIVE"];
                        // ��������� �������������
                        objBudgetDoc.m_objBudgetDep = new CBudgetDep((System.Guid)rs["BUDGETDEP_GUID_ID"], (System.String)rs["BUDGETDEP_NAME"]);
                        // ����� �������
                        objBudgetDoc.m_objPaymentType = new CPaymentType((System.Guid)rs["PAYMENTTYPE_GUID_ID"], (System.String)rs["PAYMENTTYPE_NAME"]);
                        // ������� ���������
                        if (rs["MEASURE_GUID_ID"] != System.DBNull.Value)
                        {
                            objBudgetDoc.m_objMeasure = new CMeasure((System.Guid)rs["MEASURE_GUID_ID"], (System.String)rs["MEASURE_NAME"]);
                        }
                        //������ �������
                        CBudgetItem objBudgetItem = new CBudgetItem();
                        objBudgetItem.uuidID = (System.Guid)rs["BUDGETITEM_GUID_ID"];
                        objBudgetItem.BudgetItemNum = (System.String)rs["BUDGETITEM_NUM"];
                        objBudgetItem.Name = (System.String)rs["BUDGETITEM_NAME"];
                        objBudgetItem.MoneyInBudgetDocCurrency = objBudgetDoc.m_moneyMoney;
                        objBudgetItem.MoneyInBudgetCurrency = objBudgetDoc.m_moneyMoneyAgree;
                        if (rs["DEBITARTICLE_GUID_ID"] != System.DBNull.Value)
                        {
                            objBudgetItem.DebitArticleID = (System.Guid)rs["DEBITARTICLE_GUID_ID"];
                        }
                        if (rs["BUDGETEXPENSETYPE_GUID"] != System.DBNull.Value)
                        {
                            objBudgetItem.BudgetExpenseType = new CBudgetExpenseType((System.Guid)rs["BUDGETEXPENSETYPE_GUID"],
                            (System.String)rs["BUDGETEXPENSETYPE_NAME"],
                            ((rs["BUDGETEXPENSETYPE_DESCRIPTION"] == System.DBNull.Value) ? "" : (System.String)rs["BUDGETEXPENSETYPE_DESCRIPTION"]));
                        }
                        else
                        {
                            objBudgetItem.BudgetExpenseType = new CBudgetExpenseType(System.Guid.Empty, "", "");
                        }
                        objBudgetDoc.m_objBudgetItem = objBudgetItem;

                        // ������
                        objBudgetDoc.m_objCurrency = new CCurrency((System.Guid)rs["CURRENCY_GUID_ID"], (System.String)rs["CURRENCY_CODE"], (System.String)rs["CURRENCY_CODE"]);
                        // ��������
                        objBudgetDoc.m_objCompany = new CCompany((System.Guid)rs["COMPANY_GUID_ID"], (System.String)rs["COMPANY_NAME"], (System.String)rs["COMPANY_ACRONYM"]);
                        // ��������� ���������
                        objBudgetDoc.m_objDocState = new CBudgetDocState((System.Guid)rs["BUDGETDOCSTATE_GUID_ID"], (System.String)rs["BUDGETDOCSTATE_NAME"], (System.Int32)rs["BUDGETDOCSTATE_ID"]);
                        // ��� ���������
                        objBudgetDoc.m_objDocType = new CBudgetDocType((System.Guid)rs["BUDGETDOCTYPE_GUID_ID"], (System.String)rs["BUDGETDOCTYPE_NAME"]);
                        // ���������
                        objBudgetDoc.m_OwnerUser = new CUser((System.Int32)rs["CREATEDUSER_ID"], (System.Int32)rs["UNIXPUSER_ID"], (System.String)rs["USER_LASTNAME"], (System.String)rs["USER_FIRSTNAME"]);
                        // ������ ���������
                        objBudgetDoc.m_ViewMode = (enumViewMode)((System.Int32)rs["ViewTypeId"]);
                        // ��������
                        objBudgetDoc.m_bExistsAttach = (System.Boolean)rs["ATTACH"];
                        // ������
                        objBudgetDoc.BudgetProject = ((rs["BUDGETPROJECT_GUID"] != System.DBNull.Value) ? new CBudgetProject()
                            {
                                uuidID = (System.Guid)rs["BUDGETPROJECT_GUID"],
                                Name = System.Convert.ToString(rs["BUDGETPROJECT_NAME"]),
                                IsActive = System.Convert.ToBoolean(rs["BUDGETPROJECT_ACTIVE"]),
                                CodeIn1C = System.Convert.ToInt32(rs["BUDGETPROJECT_1C_CODE"])
                            }
                            : null);
                        // ����
                        objBudgetDoc.AccountPlan = ((rs["ACCOUNTPLAN_GUID"] != System.DBNull.Value) ? new CAccountPlan()
                            {
                                uuidID = (System.Guid)rs["ACCOUNTPLAN_GUID"],
                                Name = System.Convert.ToString(rs["ACCOUNTPLAN_NAME"]),
                                IsActive = System.Convert.ToBoolean(rs["ACCOUNTPLAN_ACTIVE"]),
                                CodeIn1C = System.Convert.ToString(rs["ACCOUNTPLAN_1C_CODE"])
                            }
                            : null );
                        // ��� ���������
                        objBudgetDoc.BudgetDocCategory = ((rs["BUDGETDOCCATEGORY_GUID"] != System.DBNull.Value) ? new CBudgetDocCategory()
                            {
                                uuidID = (System.Guid)rs["BUDGETDOCCATEGORY_GUID"],
                                Name = System.Convert.ToString(rs["BUDGETDOCCATEGORY_NAME"]),
                                IsActive = System.Convert.ToBoolean(rs["BUDGETDOCCATEGORY_ACTIVE"])
                            }
                            : null );
                        if (objBudgetDoc.m_bIsActive == true)
                        {
                            if (objBudgetDoc.ViewMode != enumViewMode.Err)
                            {
                                objBudgetDocList.Add(objBudgetDoc);
                            }
                            else
                            {
                                objBudjetDocArjList.Add(objBudgetDoc);
                            }
                        }
                        else
                        {
                            objBudjetDocArjList.Add(objBudgetDoc);
                        }
                        // �� �������
                        if( rs["NOTETYPE"] != System.DBNull.Value )
                        {
                            objBudgetDoc.m_uuidNoteTypeID = (System.Guid)rs["NOTETYPE"];
                        }
                        // ����������� ������������
                        if (rs["USERCOMMENT"] != System.DBNull.Value)
                        {
                            objBudgetDoc.m_strUserComment = (System.String)rs["USERCOMMENT"];
                        }
                    }
                    objBudgetDoc = null;
                }
                cmd.Dispose();
                rs.Dispose();
            }
            catch (System.Exception e)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(
                "�� ������� �������� ������ ��������� ����������.\n\n����� ������: " + e.Message, "��������",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
            finally // ������� ���������� �������
            {
                DBConnection.Close();
            }
            return;
        }
        /// <summary>
        /// ��������� ������ ���������� ��������� ����������
        /// </summary>
        /// <param name="objProfile">�������</param>
        /// <param name="objBudjetDocForBackMoneyList">������ ���������� ������</param>
        public static void ReloadBudgetDocListForBackMoney(UniXP.Common.CProfile objProfile,
            List<ERP_Budget.Common.CBudgetDoc> objBudjetDocForBackMoneyList, 
            System.DateTime dtBeginDate, System.DateTime dtEndDate )
        {
            if (objBudjetDocForBackMoneyList == null)
            {
                objBudjetDocForBackMoneyList = new List<ERP_Budget.Common.CBudgetDoc>();
            }

            System.Data.SqlClient.SqlConnection DBConnection = objProfile.GetDBSource();
            if (DBConnection == null)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(
                "����������� ���������� � ��!", "��������",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                return;
            }

            try
            {
                // ���������� � �� ��������, ����������� ������� �� ������� ������
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
                cmd.Connection = DBConnection;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = System.String.Format("[{0}].[dbo].[sp_GetBudgetDocListForBackMoney2]", objProfile.GetOptionsDllDBName());
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ((System.Byte)(0)), ((System.Byte)(0)), "", System.Data.DataRowVersion.Current, null));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ulUniXPID", System.Data.SqlDbType.Int));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@BEGINDATE", System.Data.SqlDbType.DateTime));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ENDDATE", System.Data.SqlDbType.DateTime));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ERROR_NUM", System.Data.SqlDbType.Int, 8, System.Data.ParameterDirection.Output, false, ((System.Byte)(0)), ((System.Byte)(0)), "", System.Data.DataRowVersion.Current, null));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ERROR_MES", System.Data.SqlDbType.NVarChar, 4000));
                cmd.Parameters["@ERROR_MES"].Direction = System.Data.ParameterDirection.Output;
                cmd.Parameters["@ulUniXPID"].Value = objProfile.m_nSQLUserID;
                cmd.Parameters["@BEGINDATE"].Value = dtBeginDate;
                cmd.Parameters["@ENDDATE"].Value = dtEndDate;
                System.Data.SqlClient.SqlDataReader rs = cmd.ExecuteReader();
                if (rs.HasRows)
                {
                    // ����� ������ ��������
                    while (rs.Read())
                    {
                        CBudgetDoc objBudgetDoc = new CBudgetDoc();

                        objBudgetDoc.m_uuidID = (System.Guid)rs["GUID_ID"];
                        objBudgetDoc.m_dtDate = (System.DateTime)rs["BUDGETDOC_DATE"];
                        objBudgetDoc.m_moneyMoney = System.Convert.ToDouble(rs["BUDGETDOC_MONEY"]);
                        objBudgetDoc.m_strObjective = (System.String)rs["BUDGETDOC_OBJECTIVE"];
                        objBudgetDoc.m_dtPaymentDate = (System.DateTime)rs["BUDGETDOC_PAYMENTDATE"];
                        objBudgetDoc.m_strRecipient = (System.String)rs["BUDGETDOC_RECIPIENT"];
                        objBudgetDoc.m_strDocBasis = (System.String)rs["BUDGETDOC_DOCBASIS"];
                        if (rs["DATESTATE"] != System.DBNull.Value)
                        {
                            objBudgetDoc.DateState = (System.DateTime)rs["DATESTATE"];
                        }
                        if (rs["BUDGETDOC_DESCRIPTION"] != System.DBNull.Value)
                        {
                            objBudgetDoc.m_strDescription = (System.String)rs["BUDGETDOC_DESCRIPTION"];
                        }
                        objBudgetDoc.m_moneyMoneyAgree = System.Convert.ToDouble(rs["BUDGETDOC_MONEYAGREE"]);
                        objBudgetDoc.m_bIsActive = (System.Boolean)rs["BUDGETDOC_ACTIVE"];
                        // ��������� �������������
                        objBudgetDoc.m_objBudgetDep = new CBudgetDep((System.Guid)rs["BUDGETDEP_GUID_ID"], (System.String)rs["BUDGETDEP_NAME"]);
                        // ����� �������
                        objBudgetDoc.m_objPaymentType = new CPaymentType((System.Guid)rs["PAYMENTTYPE_GUID_ID"], (System.String)rs["PAYMENTTYPE_NAME"]);
                        // ������� ���������
                        if (rs["MEASURE_GUID_ID"] != System.DBNull.Value)
                        {
                            objBudgetDoc.m_objMeasure = new CMeasure((System.Guid)rs["MEASURE_GUID_ID"], (System.String)rs["MEASURE_NAME"]);
                        }
                        //������ �������
                        CBudgetItem objBudgetItem = new CBudgetItem();
                        objBudgetItem.uuidID = (System.Guid)rs["BUDGETITEM_GUID_ID"];
                        objBudgetItem.BudgetItemNum = (System.String)rs["BUDGETITEM_NUM"];
                        objBudgetItem.Name = (System.String)rs["BUDGETITEM_NAME"];
                        objBudgetItem.MoneyInBudgetDocCurrency = objBudgetDoc.m_moneyMoney;
                        objBudgetItem.MoneyInBudgetCurrency = objBudgetDoc.m_moneyMoneyAgree;
                        if (rs["DEBITARTICLE_GUID_ID"] != System.DBNull.Value)
                        {
                            objBudgetItem.DebitArticleID = (System.Guid)rs["DEBITARTICLE_GUID_ID"];
                        }
                        objBudgetDoc.m_objBudgetItem = objBudgetItem;

                        // ������
                        objBudgetDoc.m_objCurrency = new CCurrency((System.Guid)rs["CURRENCY_GUID_ID"], (System.String)rs["CURRENCY_CODE"], (System.String)rs["CURRENCY_CODE"]);
                        // ��������
                        objBudgetDoc.m_objCompany = new CCompany((System.Guid)rs["COMPANY_GUID_ID"], (System.String)rs["COMPANY_NAME"], (System.String)rs["COMPANY_ACRONYM"]);
                        // ��������� ���������
                        objBudgetDoc.m_objDocState = new CBudgetDocState((System.Guid)rs["BUDGETDOCSTATE_GUID_ID"], (System.String)rs["BUDGETDOCSTATE_NAME"], (System.Int32)rs["BUDGETDOCSTATE_ID"]);
                        // ��� ���������
                        objBudgetDoc.m_objDocType = new CBudgetDocType((System.Guid)rs["BUDGETDOCTYPE_GUID_ID"], (System.String)rs["BUDGETDOCTYPE_NAME"]);
                        // ���������
                        objBudgetDoc.m_OwnerUser = new CUser((System.Int32)rs["CREATEDUSER_ID"], (System.Int32)rs["UniXPUserID"], (System.String)rs["strLastName"], (System.String)rs["strFirstName"]);
                        // ������
                        objBudgetDoc.BudgetProject = ((rs["BUDGETPROJECT_GUID"] != System.DBNull.Value) ? new CBudgetProject()
                        {
                            uuidID = (System.Guid)rs["BUDGETPROJECT_GUID"],
                            Name = System.Convert.ToString(rs["BUDGETPROJECT_NAME"]),
                            IsActive = System.Convert.ToBoolean(rs["BUDGETPROJECT_ACTIVE"]),
                            CodeIn1C = System.Convert.ToInt32(rs["BUDGETPROJECT_1C_CODE"])
                        }
                            : null);
                        // ����
                        objBudgetDoc.AccountPlan = ((rs["ACCOUNTPLAN_GUID"] != System.DBNull.Value) ? new CAccountPlan()
                        {
                            uuidID = (System.Guid)rs["ACCOUNTPLAN_GUID"],
                            Name = System.Convert.ToString(rs["ACCOUNTPLAN_NAME"]),
                            IsActive = System.Convert.ToBoolean(rs["ACCOUNTPLAN_ACTIVE"]),
                            CodeIn1C = System.Convert.ToString(rs["ACCOUNTPLAN_1C_CODE"])
                        }
                            : null);
                        // ��� ���������
                        objBudgetDoc.BudgetDocCategory = ((rs["BUDGETDOCCATEGORY_GUID"] != System.DBNull.Value) ? new CBudgetDocCategory()
                        {
                            uuidID = (System.Guid)rs["BUDGETDOCCATEGORY_GUID"],
                            Name = System.Convert.ToString(rs["BUDGETDOCCATEGORY_NAME"]),
                            IsActive = System.Convert.ToBoolean(rs["BUDGETDOCCATEGORY_ACTIVE"])
                        }
                            : null);

                        objBudjetDocForBackMoneyList.Add(objBudgetDoc);
                    }
                }
                cmd.Dispose();
                rs.Dispose();
            }
            catch (System.Exception e)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(
                "�� ������� �������� ������ ���������� ��������� ����������.\n\n����� ������: " + e.Message, "��������",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
            finally // ������� ���������� �������
            {
                DBConnection.Close();
            }
            return;
        }
        #endregion

        #region New ( ����� ��������� �������� )
        /// <summary>
        /// ������� ����� ������ ������ "��������� ��������"
        /// </summary>
        /// <param name="objProfile">�������</param>
        /// <param name="uuidBudgetID">�� �������</param>
        /// <returns>������ ������ "��������� ��������"</returns>
        public static CBudgetDoc NewBudgetDoc(UniXP.Common.CProfile objProfile)
        {
            CBudgetDoc objBudgetDoc = new CBudgetDoc();
            try
            {
                objBudgetDoc.uuidID = System.Guid.NewGuid();
                objBudgetDoc.IsActive = true;
                objBudgetDoc.m_dtDate = System.DateTime.Now;
                objBudgetDoc.m_dtPaymentDate = System.DateTime.Now;
                objBudgetDoc.m_OwnerUser = new CUser();
                objBudgetDoc.m_OwnerUser.ulUniXPID = objProfile.m_nSQLUserID;
                objBudgetDoc.m_OwnerUser.UserLastName = objProfile.m_strLastName;
                objBudgetDoc.m_OwnerUser.UserFirstName = objProfile.m_strFirstName;
                // ���������� ������ � ����������� ����������
                objBudgetDoc.LoadPopupList(objProfile);
            }
            catch (System.Exception f)
            {
                objBudgetDoc = null;
                DevExpress.XtraEditors.XtraMessageBox.Show(
                "�� ������� ������� ����� ��������� ��������.\n\n����� ������: " + f.Message, "��������",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
			finally // ������� ���������� �������
            {
            }

            return objBudgetDoc;
        }
        #endregion

        #region Copy ( ����������� ������� ���������� ��������� )
        /// <summary>
        /// ������� ����� ������ ������ "��������� ��������" � ����������� ��� �������� ������� � �������� ��
        /// </summary>
        /// <param name="objProfile">�������</param>
        /// <param name="uuidBudgetDocID">�� ���������� ���������</param>
        /// <returns>������ ������ "��������� ��������"</returns>
        public static CBudgetDoc CopyBudgetDoc(UniXP.Common.CProfile objProfile, System.Guid uuidBudgetDocID)
        {
            CBudgetDoc objBudgetDoc = new CBudgetDoc();
            try
            {
                objBudgetDoc.Init(objProfile, uuidBudgetDocID);
                objBudgetDoc.uuidID = System.Guid.NewGuid();
                objBudgetDoc.m_dtDate = System.DateTime.Now;
                objBudgetDoc.m_dtPaymentDate = System.DateTime.Now;
                objBudgetDoc.AttachList.Clear();
            }
            catch (System.Exception f)
            {
                objBudgetDoc = null;
                DevExpress.XtraEditors.XtraMessageBox.Show(
                "�� ������� ����������� �������� ���������� ���������.\n�� ���������� ��������� : " +
                uuidBudgetDocID.ToString() + "\n����� ������: " + f.Message, "��������",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
			finally // ������� ���������� �������
            {
            }

            return objBudgetDoc;
        }
        /// <summary>
        /// �������� �������� ������� "��������� ��������" �������� ������� "��������� ��������"
        /// </summary>
        /// <param name="objBudgetDoc">������ "��������� ��������"</param>
        public void Copy(CBudgetDoc objBudgetDoc)
        {
            try
            {
                
                this.BudgetDep = objBudgetDoc.BudgetDep;
                //this.Date = objBudgetDoc.Date;
                //this.PaymentDate = objBudgetDoc.PaymentDate;

                if (this.Date.Year == objBudgetDoc.Date.Year)
                {
                    // ������ ������ � ���� �� ����
                    if (objBudgetDoc.m_objBudgetItemList != null)
                    {
                        foreach (CBudgetItem objBudgetItem in objBudgetDoc.m_objBudgetItemList)
                        {
                            this.m_objBudgetItemList.Add(CBudgetItem.Copy(objBudgetItem));
                        }
                    }
                    this.m_objBudgetItem = objBudgetDoc.BudgetItem;
                }

                this.Company = objBudgetDoc.Company;
                this.Currency = objBudgetDoc.Currency;
                this.Description = objBudgetDoc.Description;
                this.DocBasis = objBudgetDoc.DocBasis;
                this.Measure = objBudgetDoc.Measure;
                this.Objective = objBudgetDoc.Objective;
                this.PaymentType = objBudgetDoc.PaymentType;
                this.Money = objBudgetDoc.Money;
                this.Recipient = objBudgetDoc.Recipient;
                this.BudgetProject = objBudgetDoc.BudgetProject;
                this.AccountPlan = objBudgetDoc.AccountPlan;
                this.BudgetDocCategory = objBudgetDoc.BudgetDocCategory;
            }
            catch (System.Exception f)
            {
                objBudgetDoc = null;
                DevExpress.XtraEditors.XtraMessageBox.Show(
                "������ ����������� ������� ���������� ���������.\n\n����� ������: " + f.Message, "��������",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
			finally // ������� ���������� �������
            {
            }
            return;
        }

        #endregion

        #region Init ( ������������� ������� ������ )
        /// <summary>
        /// ��������� ���������� � ���������� ������� � ����������� ���������� 
        /// </summary>
        /// <param name="objProfile">�������</param>
        /// <returns>true - �������� ����������; false - ������</returns>
        public System.Boolean LoadPopupList(UniXP.Common.CProfile objProfile)
        {
            System.Boolean bRet = false;
            System.Data.SqlClient.SqlConnection DBConnection = objProfile.GetDBSource();
            if (DBConnection == null) { return bRet; }

            try
            {
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
                cmd.Connection = DBConnection;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                System.DateTime dtStart = System.DateTime.Now;

                // ���������� ������ � ����������� ����������
                // ��������� �������������
                if (this.m_PopupBudgetDepList == null)
                { this.m_PopupBudgetDepList = new List<CBudgetDep>(); }
                CBudgetDep.RefreshBudgetDepList(objProfile, cmd, this.m_PopupBudgetDepList);

                System.Int32 iPopupBudgetDepList = System.DateTime.Now.Second - dtStart.Second;

                System.DateTime dtStartPopupPaymentTypeList = System.DateTime.Now;
                // ����� ������
                if (this.m_PopupPaymentTypeList == null)
                { this.m_PopupPaymentTypeList = new List<CPaymentType>(); }
                CPaymentType.RefreshPaymentTypeList(objProfile, cmd, this.m_PopupPaymentTypeList);

                System.Int32 iPopupPaymentTypeList = System.DateTime.Now.Second - dtStartPopupPaymentTypeList.Second;

                System.DateTime dtStartPopupMeasureList = System.DateTime.Now;

                // ������� ���������
                if (this.m_PopupMeasureList == null)
                { this.m_PopupMeasureList = new List<CMeasure>(); }
                CMeasure.RefreshMeasureList(objProfile, cmd, this.m_PopupMeasureList);

                System.Int32 iPopupMeasureList = System.DateTime.Now.Second - dtStartPopupMeasureList.Second;

                System.DateTime dtStartPopupCurrencyList = System.DateTime.Now;

                // ������
                if (this.m_PopupCurrencyList == null)
                { this.m_PopupCurrencyList = new List<CCurrency>(); }
                CCurrency.RefreshCurrencyList(objProfile, cmd, this.m_PopupCurrencyList);

                System.Int32 iPopupCurrencyList = System.DateTime.Now.Second - dtStartPopupCurrencyList.Second;

                System.DateTime dtStartPopupBudgetDocStateList = System.DateTime.Now;

                // ��������� ���������
                if (this.m_PopupBudgetDocStateList == null)
                { this.m_PopupBudgetDocStateList = new List<CBudgetDocState>(); }
                CBudgetDocState.RefreshBudgetDocStateList(objProfile, cmd, this.m_PopupBudgetDocStateList);

                System.Int32 iPopupBudgetDocStateList = System.DateTime.Now.Second - dtStartPopupBudgetDocStateList.Second;

                System.DateTime dtStartPopupBudgetDocTypeList = System.DateTime.Now;

                // ���� ���������
                if (this.m_PopupBudgetDocTypeList == null)
                { this.m_PopupBudgetDocTypeList = new List<CBudgetDocType>(); }
                CBudgetDocType.RefreshBudgetDocTypeList(objProfile, cmd, this.m_PopupBudgetDocTypeList);

                System.Int32 iPopupBudgetDocTypeList = System.DateTime.Now.Second - dtStartPopupBudgetDocTypeList.Second;

                System.DateTime dtStartPopupBudgetItemList = System.DateTime.Now;

                //// ������ �������
                //if( this.m_PopupBudgetItemList == null )
                //{ this.m_PopupBudgetItemList = new List<CPopupBudgetItem>(); }
                //LoadPopupBudgetItemList( objProfile, cmd );

                System.Int32 iPopupBudgetItemList = System.DateTime.Now.Second - dtStartPopupBudgetItemList.Second;

                System.DateTime dtStartPopupCompanyList = System.DateTime.Now;
                // ��������
                if (this.m_PopupCompanyList == null)
                { this.m_PopupCompanyList = new List<CCompany>(); }
                CCompany.RefreshCompanyList(objProfile, cmd, this.m_PopupCompanyList);

                System.Int32 iPopupCompanyList = System.DateTime.Now.Second - dtStartPopupCompanyList.Second;

                System.DateTime dtFinish = System.DateTime.Now;

                bRet = true;

                //DevExpress.XtraEditors.XtraMessageBox.Show(
                //( "�������� ���������� �������.\n\n" + 
                //  "����� ������: " + dtStart.ToLongTimeString() + "\n����� ����������: " + dtFinish.ToLongTimeString() + 
                //  "\n����� ����� ���������� ���������� �������, ���.: " + ( ( System.Int32 )( dtFinish.Second - dtStart.Second ) ).ToString() + 
                //  "\n\n��������� �������������, ���.: " + iPopupBudgetDepList.ToString() + 
                //  "\n\n����� ������, ���.: " + iPopupPaymentTypeList.ToString() + 
                //  "\n\n������� ���������, ���.: " + iPopupMeasureList.ToString() + 
                //  "\n\n������, ���.: " + iPopupCurrencyList.ToString() + 
                //  "\n\n��������� ���������, ���.: " + iPopupBudgetDocStateList.ToString() + 
                //  "\n\n���� ���������, ���.: " + iPopupBudgetDocTypeList.ToString() + 
                //  "\n\n������ �������, ���.: " + iPopupBudgetItemList.ToString() + 
                //  "\n\n��������, ���.: " + iPopupCompanyList.ToString() ), "��������",
                //System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information );
            }
            catch (System.Exception f)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(
                "�� ������� ������������ ���������� ������ ��� ���������� ���������.\n����� ������: " + f.Message, "��������",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
			finally // ������� ���������� �������
            {
                DBConnection.Close();
            }
            return bRet;
        }

        /// <summary>
        /// ��������� ���������� � ���������� ������� � ����������� ���������� 
        /// </summary>
        /// <param name="objProfile">�������</param>
        /// <returns>true - �������� ����������; false - ������</returns>
        private System.Boolean LoadPopupList(UniXP.Common.CProfile objProfile,
            System.Data.SqlClient.SqlCommand cmd)
        {
            System.Boolean bRet = false;
            if (cmd == null)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show("����������� ���������� � ��.", "��������",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);
                return bRet;
            }

            try
            {
                // ���������� ������ � ����������� ����������
                // ��������� �������������
                if (this.m_PopupBudgetDepList == null)
                { this.m_PopupBudgetDepList = new List<CBudgetDep>(); }
                CBudgetDep.RefreshBudgetDepList(objProfile, cmd, this.m_PopupBudgetDepList);

                // ����� ������
                if (this.m_PopupPaymentTypeList == null)
                { this.m_PopupPaymentTypeList = new List<CPaymentType>(); }
                CPaymentType.RefreshPaymentTypeList(objProfile, cmd, this.m_PopupPaymentTypeList);

                // ������� ���������
                if (this.m_PopupMeasureList == null)
                { this.m_PopupMeasureList = new List<CMeasure>(); }
                CMeasure.RefreshMeasureList(objProfile, cmd, this.m_PopupMeasureList);

                // ������
                if (this.m_PopupCurrencyList == null)
                { this.m_PopupCurrencyList = new List<CCurrency>(); }
                CCurrency.RefreshCurrencyList(objProfile, cmd, this.m_PopupCurrencyList);

                // ��������� ���������
                if (this.m_PopupBudgetDocStateList == null)
                { this.m_PopupBudgetDocStateList = new List<CBudgetDocState>(); }
                CBudgetDocState.RefreshBudgetDocStateList(objProfile, cmd, this.m_PopupBudgetDocStateList);

                // ���� ���������
                if (this.m_PopupBudgetDocTypeList == null)
                { this.m_PopupBudgetDocTypeList = new List<CBudgetDocType>(); }
                CBudgetDocType.RefreshBudgetDocTypeList(objProfile, cmd, this.m_PopupBudgetDocTypeList);

                //// ������ �������
                //if( this.m_PopupBudgetItemList == null )
                //{ this.m_PopupBudgetItemList = new List<CPopupBudgetItem>(); }
                //LoadPopupBudgetItemList( objProfile, cmd );

                // ��������
                if (this.m_PopupCompanyList == null)
                { this.m_PopupCompanyList = new List<CCompany>(); }
                CCompany.RefreshCompanyList(objProfile, cmd, this.m_PopupCompanyList);

                bRet = true;
            }
            catch (System.Exception f)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(
                "�� ������� ������������ ���������� ������ ��� ���������� ���������.\n����� ������: " + f.Message, "��������",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
			finally // ������� ���������� �������
            {
            }

            return bRet;
        }
        /// <summary>
        /// ��������� ������ ������ �������
        /// </summary>
        /// <param name="objProfile">�������</param>
        /// <param name="objBudget">������</param>
        /// <returns>true - �������� ����������; false - ������</returns>
        public System.Boolean LoadPopupBudgetItemListForBudget(UniXP.Common.CProfile objProfile,
            ERP_Budget.Common.CBudget objBudget)
        {
            // �������� ������� �������, �� ������� ��� ����, ��� �� �� ������� ������ �������� ��������
            // ����������� ������ ������ �������� ��� ������, ������� �� ������� �� objBudget
            // � ����� ������� ������ �������� ��� ��������

            System.Boolean bRet = false;
            this.m_PopupBudgetItemList.Clear();

            if (objBudget.uuidID.CompareTo(System.Guid.Empty) == 0) { return bRet; }

            System.Data.SqlClient.SqlConnection DBConnection = objProfile.GetDBSource();
            if (DBConnection == null)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show("����������� ���������� � ��.", "��������",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                return bRet;
            }
            try
            {
                // ���������� � �� ��������, ����������� ������� �� ������� ������
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
                cmd.Connection = DBConnection;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                // ������ ���������� ������ ������������ ������ �������
                bRet = LoadPopupParentBudgetItemListForBudget(objProfile, cmd, objBudget.uuidID);
                // ��� ����� ������������ ������ ������� ������ �������� ������
                if (bRet == true)
                {
                    foreach (CPopupBudgetItem objPopupBudgetItem in this.m_PopupBudgetItemList)
                    {
                        bRet = LoadPopupChildBudgetItemList(objProfile, cmd, objPopupBudgetItem);
                        if (bRet == false) { break; }
                    }
                }
            }
            catch (System.Exception f)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(
                "�� ������� �������� ������ ������ �������.\n\n����� ������: " + f.Message, "��������",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
			finally // ������� ���������� �������
            {
            }
            return bRet;
        }
        /// <summary>
        /// ��������� ������ ������ �������
        /// </summary>
        /// <param name="objProfile">�������</param>
        /// <param name="uuidBudgetDepID">�� ���������� �������������</param>
        /// <returns>true - �������� ����������; false - ������</returns>
        public System.Boolean LoadPopupBudgetItemList(UniXP.Common.CProfile objProfile,
            System.Guid uuidBudgetDepID)
        {
            System.Boolean bRet = false;
            this.m_PopupBudgetItemList.Clear();

            System.Data.SqlClient.SqlConnection DBConnection = objProfile.GetDBSource();
            if (DBConnection == null)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show("����������� ���������� � ��.", "��������",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                return bRet;
            }
            try
            {
                // ���������� � �� ��������, ����������� ������� �� ������� ������
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
                cmd.Connection = DBConnection;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                // ������ ���������� ������ ������������ ������ �������
                bRet = LoadPopupParentBudgetItemList(objProfile, cmd, uuidBudgetDepID);

                // ��� ����� ������������ ������ ������� ������ �������� ������
                if (bRet == true)
                {
                    foreach (CPopupBudgetItem objPopupBudgetItem in this.m_PopupBudgetItemList)
                    {
                        bRet = LoadPopupChildBudgetItemList(objProfile, cmd, objPopupBudgetItem);
                        if (bRet == false) { break; }
                    }
                }
            }
            catch (System.Exception f)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(
                "�� ������� �������� ������ ������ �������.\n\n����� ������: " + f.Message, "��������",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
			finally // ������� ���������� �������
            {
            }
            return bRet;
        }
        /// <summary>
        /// ��������� ������ ������ �������
        /// </summary>
        /// <param name="objProfile">�������</param>
        /// <param name="cmd">SQl-�������</param>
        /// <returns>true - �������� ����������; false - ������</returns>
        private System.Boolean LoadPopupBudgetItemList(UniXP.Common.CProfile objProfile,
            System.Data.SqlClient.SqlCommand cmd)
        {
            System.Boolean bRet = false;
            if (cmd == null)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show("����������� ���������� � ��.", "��������",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                return bRet;
            }
            try
            {
                //this.m_objBudgetItemList.Clear();
                // ������ ���������� ������ ������������ ������ �������
                bRet = LoadPopupParentBudgetItemList(objProfile, cmd);
                // ��� ����� ������������ ������ ������� ������ �������� ������
                if (bRet == true)
                {
                    foreach (CPopupBudgetItem objPopupBudgetItem in this.m_PopupBudgetItemList)
                    {
                        bRet = LoadPopupChildBudgetItemList(objProfile, cmd, objPopupBudgetItem);
                        if (bRet == false) { break; }
                    }
                }
            }
            catch (System.Exception f)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(
                "�� ������� �������� ������ ������ �������.\n\n����� ������: " + f.Message, "��������",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
			finally // ������� ���������� �������
            {
            }
            return bRet;
        }
        /// <summary>
        /// ��������� ������ ������������ ������ ������� ��������� �������������
        /// </summary>
        /// <param name="objProfile">�������</param>
        /// <param name="cmd">SQl-�������</param>
        /// <param name="uuidBudgetDepID">�� ���������� �������������</param>
        /// <returns>true - �������� ����������; false - ������</returns>
        private System.Boolean LoadPopupParentBudgetItemList(UniXP.Common.CProfile objProfile,
            System.Data.SqlClient.SqlCommand cmd, System.Guid uuidBudgetDepID)
        {
            System.Boolean bRet = false;
            if (cmd == null)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show("����������� ���������� � ��.", "��������",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                return bRet;
            }

            try
            {
                cmd.Parameters.Clear();
                cmd.CommandText = System.String.Format("[{0}].[dbo].[sp_GetBudgetItemParentForBudgetDep]", objProfile.GetOptionsDllDBName());
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ((System.Byte)(0)), ((System.Byte)(0)), "", System.Data.DataRowVersion.Current, null));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@BUDGETDEP_GUID_ID", System.Data.SqlDbType.UniqueIdentifier));
                cmd.Parameters["@BUDGETDEP_GUID_ID"].Value = uuidBudgetDepID;
                System.Data.SqlClient.SqlDataReader rs = cmd.ExecuteReader();
                if (rs.HasRows)
                {
                    while (rs.Read())
                    {
                        CPopupBudgetItem objPopupBudgetItem = new CPopupBudgetItem();

                        objPopupBudgetItem.ParentBudgetItem = new CBudgetItem();
                        objPopupBudgetItem.ParentBudgetItem.uuidID = (System.Guid)rs["GUID_ID"];
                        objPopupBudgetItem.ParentBudgetItem.BudgetGUID = (System.Guid)rs["BUDGET_GUID_ID"];
                        objPopupBudgetItem.ParentBudgetItem.BudgetName = System.Convert.ToString(rs["BUDGET_NAME"]);

                        objPopupBudgetItem.BudgetDepID = (System.Guid)rs["BUDGETDEP_GUID_ID"];
                        if (rs["DEBITARTICLE_GUID_ID"] != System.DBNull.Value)
                        {
                            objPopupBudgetItem.ParentBudgetItem.DebitArticleID = (System.Guid)rs["DEBITARTICLE_GUID_ID"];
                        }
                        if (rs["PARENT_GUID_ID"] != System.DBNull.Value)
                        {
                            objPopupBudgetItem.ParentBudgetItem.ParentID = (System.Guid)rs["PARENT_GUID_ID"];
                        }
                        if (rs["BUDGETITEM_DESCRIPTION"] != System.DBNull.Value)
                        {
                            objPopupBudgetItem.ParentBudgetItem.BudgetItemDescription = (System.String)rs["BUDGETITEM_DESCRIPTION"];
                        }

                        objPopupBudgetItem.ParentBudgetItem.BudgetExpenseType = ((rs["BUDGETEXPENSETYPE_GUID"] != System.DBNull.Value) ? new CBudgetExpenseType()
                        {
                            uuidID = (System.Guid)rs["BUDGETEXPENSETYPE_GUID"],
                            Name = System.Convert.ToString(rs["BUDGETEXPENSETYPE_NAME"]),
                            CodeIn1C = System.Convert.ToInt32(rs["BUDGETEXPENSETYPE_1C_CODE"]),
                            IsActive = System.Convert.ToBoolean(rs["BUDGETEXPENSETYPE_ACTIVE"])
                        } : null);

                        objPopupBudgetItem.ParentBudgetItem.AccountPlan = ((rs["ACCOUNTPLAN_GUID"] != System.DBNull.Value) ? new CAccountPlan()
                        {
                            uuidID = (System.Guid)rs["ACCOUNTPLAN_GUID"],
                            Name = System.Convert.ToString(rs["ACCOUNTPLAN_NAME"]),
                            IsActive = System.Convert.ToBoolean(rs["ACCOUNTPLAN_ACTIVE"]),
                            CodeIn1C = System.Convert.ToString(rs["ACCOUNTPLAN_1C_CODE"])
                        } : null);

                        objPopupBudgetItem.ParentBudgetItem.BudgetProject = ((rs["BUDGETPROJECT_GUID"] != System.DBNull.Value) ? new CBudgetProject()
                        {
                            uuidID = (System.Guid)rs["BUDGETPROJECT_GUID"],
                            Name = System.Convert.ToString(rs["BUDGETPROJECT_NAME"]),
                            Description = "",
                            IsActive = System.Convert.ToBoolean(rs["BUDGETPROJECT_ACTIVE"]),
                            CodeIn1C = System.Convert.ToInt32(rs["BUDGETPROJECT_1C_CODE"])
                        } : null);

                        objPopupBudgetItem.ParentBudgetItem.BudgetType = ((rs["BUDGETTYPE_GUID"] != System.DBNull.Value) ? new CBudgetType()
                        {
                            uuidID = (System.Guid)rs["BUDGETTYPE_GUID"],
                                Name = System.Convert.ToString(rs["BUDGETTYPE_NAME"]),
                                Description = ((rs["BUDGETTYPE_DESCRIPTION"] == System.DBNull.Value) ? "" : (System.String)rs["BUDGETTYPE_DESCRIPTION"]),
                                IsActive = System.Convert.ToBoolean(rs["BUDGETTYPE_ACTIVE"])
                        } : null);

                        objPopupBudgetItem.ParentBudgetItem.BudgetItemNum = (System.String)rs["BUDGETITEM_NUM"];
                        objPopupBudgetItem.ParentBudgetItem.Name = (System.String)rs["BUDGETITEM_NAME"];
                        objPopupBudgetItem.ParentBudgetItem.TransprtRest = (System.Boolean)rs["BUDGETITEM_TRANSPORTREST"];
                        objPopupBudgetItem.ParentBudgetItem.DontChange = (System.Boolean)rs["BUDGETITEM_DONTCHANGE"];
                        objPopupBudgetItem.ParentBudgetItem.BudgetItemID = (System.Int32)rs["BUDGETITEM_ID"];
                        objPopupBudgetItem.ParentBudgetItem.RestMoney = System.Convert.ToDouble(rs["RESTMONEY"]);
                        objPopupBudgetItem.ParentBudgetItem.BudgetName = (System.String)rs["BUDGET_NAME"];
                        objPopupBudgetItem.ParentBudgetItem.OffExpenditures = (System.Boolean)rs["OFFBUDGET_EXPENDITURES"];
                        this.m_PopupBudgetItemList.Add(objPopupBudgetItem);
                    }
                    bRet = true;
                }
                else
                {
                    DevExpress.XtraEditors.XtraMessageBox.Show("������ ������ ������� ����!", "��������",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);
                }
                rs.Close();
                rs = null;
            }
            catch (System.Exception f)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(
                "�� ������� �������� ������ ������������ ������ �������.\n\n����� ������: " + f.Message, "��������",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
			finally // ������� ���������� �������
            {
            }

            return bRet;
        }
        /// <summary>
        /// ��������� ������ ������������ ������ ������� ���� �������������
        /// </summary>
        /// <param name="objProfile">�������</param>
        /// <param name="cmd">SQl-�������</param>
        /// <returns>true - �������� ����������; false - ������</returns>
        private System.Boolean LoadPopupParentBudgetItemList(UniXP.Common.CProfile objProfile,
            System.Data.SqlClient.SqlCommand cmd)
        {
            System.Boolean bRet = false;
            if (cmd == null)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show("����������� ���������� � ��.", "��������",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                return bRet;
            }

            try
            {
                cmd.Parameters.Clear();
                cmd.CommandText = System.String.Format("[{0}].[dbo].[sp_GetBudgetItemParentAll]", objProfile.GetOptionsDllDBName());
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ((System.Byte)(0)), ((System.Byte)(0)), "", System.Data.DataRowVersion.Current, null));
                System.Data.SqlClient.SqlDataReader rs = cmd.ExecuteReader();
                if (rs.HasRows)
                {
                    while (rs.Read())
                    {
                        CPopupBudgetItem objPopupBudgetItem = new CPopupBudgetItem();

                        objPopupBudgetItem.ParentBudgetItem = new CBudgetItem();
                        objPopupBudgetItem.ParentBudgetItem.uuidID = (System.Guid)rs["GUID_ID"];
                        objPopupBudgetItem.ParentBudgetItem.BudgetGUID = (System.Guid)rs["BUDGET_GUID_ID"];
                        objPopupBudgetItem.ParentBudgetItem.BudgetName = System.Convert.ToString(rs["BUDGET_NAME"]);
                        objPopupBudgetItem.BudgetDepID = (System.Guid)rs["BUDGETDEP_GUID_ID"];
                        if (rs["DEBITARTICLE_GUID_ID"] != System.DBNull.Value)
                        {
                            objPopupBudgetItem.ParentBudgetItem.DebitArticleID = (System.Guid)rs["DEBITARTICLE_GUID_ID"];
                        }
                        if (rs["PARENT_GUID_ID"] != System.DBNull.Value)
                        {
                            objPopupBudgetItem.ParentBudgetItem.ParentID = (System.Guid)rs["PARENT_GUID_ID"];
                        }
                        if (rs["BUDGETITEM_DESCRIPTION"] != System.DBNull.Value)
                        {
                            objPopupBudgetItem.ParentBudgetItem.BudgetItemDescription = (System.String)rs["BUDGETITEM_DESCRIPTION"];
                        }

                        objPopupBudgetItem.ParentBudgetItem.BudgetExpenseType = ((rs["BUDGETEXPENSETYPE_GUID"] != System.DBNull.Value) ? new CBudgetExpenseType()
                        {
                            uuidID = (System.Guid)rs["BUDGETEXPENSETYPE_GUID"],
                            Name = System.Convert.ToString(rs["BUDGETEXPENSETYPE_NAME"]),
                            CodeIn1C = System.Convert.ToInt32(rs["BUDGETEXPENSETYPE_1C_CODE"]),
                            IsActive = System.Convert.ToBoolean(rs["BUDGETEXPENSETYPE_ACTIVE"])
                        } : null);

                        objPopupBudgetItem.ParentBudgetItem.AccountPlan = ((rs["ACCOUNTPLAN_GUID"] != System.DBNull.Value) ? new CAccountPlan()
                        {
                            uuidID = (System.Guid)rs["ACCOUNTPLAN_GUID"],
                            Name = System.Convert.ToString(rs["ACCOUNTPLAN_NAME"]),
                            IsActive = System.Convert.ToBoolean(rs["ACCOUNTPLAN_ACTIVE"]),
                            CodeIn1C = System.Convert.ToString(rs["ACCOUNTPLAN_1C_CODE"])
                        } : null);

                        objPopupBudgetItem.ParentBudgetItem.BudgetProject = ((rs["BUDGETPROJECT_GUID"] != System.DBNull.Value) ? new CBudgetProject()
                        {
                            uuidID = (System.Guid)rs["BUDGETPROJECT_GUID"],
                            Name = System.Convert.ToString(rs["BUDGETPROJECT_NAME"]),
                            Description = "",
                            IsActive = System.Convert.ToBoolean(rs["BUDGETPROJECT_ACTIVE"]),
                            CodeIn1C = System.Convert.ToInt32(rs["BUDGETPROJECT_1C_CODE"])
                        } : null);

                        objPopupBudgetItem.ParentBudgetItem.BudgetType = ((rs["BUDGETTYPE_GUID"] != System.DBNull.Value) ? new CBudgetType()
                        {
                            uuidID = (System.Guid)rs["BUDGETTYPE_GUID"],
                            Name = System.Convert.ToString(rs["BUDGETTYPE_NAME"]),
                            Description = ((rs["BUDGETTYPE_DESCRIPTION"] == System.DBNull.Value) ? "" : (System.String)rs["BUDGETTYPE_DESCRIPTION"]),
                            IsActive = System.Convert.ToBoolean(rs["BUDGETTYPE_ACTIVE"])
                        } : null);

                        objPopupBudgetItem.ParentBudgetItem.BudgetItemNum = (System.String)rs["BUDGETITEM_NUM"];
                        objPopupBudgetItem.ParentBudgetItem.Name = (System.String)rs["BUDGETITEM_NAME"];
                        objPopupBudgetItem.ParentBudgetItem.TransprtRest = (System.Boolean)rs["BUDGETITEM_TRANSPORTREST"];
                        objPopupBudgetItem.ParentBudgetItem.DontChange = (System.Boolean)rs["BUDGETITEM_DONTCHANGE"];
                        objPopupBudgetItem.ParentBudgetItem.BudgetItemID = (System.Int32)rs["BUDGETITEM_ID"];
                        objPopupBudgetItem.ParentBudgetItem.RestMoney = System.Convert.ToDouble(rs["RESTMONEY"]);

                        this.m_PopupBudgetItemList.Add(objPopupBudgetItem);
                    }
                    bRet = true;
                }
                else
                {
                    DevExpress.XtraEditors.XtraMessageBox.Show("������ ������ ������� ����!", "��������",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);
                }
                rs.Close();
                rs = null;
            }
            catch (System.Exception f)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(
                "�� ������� �������� ������ ������������ ������ �������.\n\n����� ������: " + f.Message, "��������",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
			finally // ������� ���������� �������
            {
            }

            return bRet;
        }
        /// <summary>
        /// ��������� ������ ������������ ������ ��������� �������
        /// </summary>
        /// <param name="objProfile">�������</param>
        /// <param name="cmd">SQl-�������</param>
        /// <param name="uuidBudgetID">�� �������</param>
        /// <returns>true - �������� ����������; false - ������</returns>
        private System.Boolean LoadPopupParentBudgetItemListForBudget(UniXP.Common.CProfile objProfile,
            System.Data.SqlClient.SqlCommand cmd, System.Guid uuidBudgetID)
        {
            System.Boolean bRet = false;
            if (cmd == null)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show("����������� ���������� � ��.", "��������",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                return bRet;
            }

            try
            {
                cmd.Parameters.Clear();
                cmd.CommandText = System.String.Format("[{0}].[dbo].[sp_GetBudgetItemParentForBudget]", objProfile.GetOptionsDllDBName());
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ((System.Byte)(0)), ((System.Byte)(0)), "", System.Data.DataRowVersion.Current, null));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@BUDGET_GUID_ID", System.Data.SqlDbType.UniqueIdentifier));
                cmd.Parameters["@BUDGET_GUID_ID"].Value = uuidBudgetID;
                System.Data.SqlClient.SqlDataReader rs = cmd.ExecuteReader();
                if (rs.HasRows)
                {
                    while (rs.Read())
                    {
                        CPopupBudgetItem objPopupBudgetItem = new CPopupBudgetItem();

                        objPopupBudgetItem.ParentBudgetItem = new CBudgetItem();
                        objPopupBudgetItem.ParentBudgetItem.uuidID = (System.Guid)rs["GUID_ID"];
                        objPopupBudgetItem.ParentBudgetItem.BudgetGUID = (System.Guid)rs["BUDGET_GUID_ID"];
                        objPopupBudgetItem.ParentBudgetItem.BudgetName = System.Convert.ToString(rs["BUDGET_NAME"]);
                        objPopupBudgetItem.BudgetDepID = (System.Guid)rs["BUDGETDEP_GUID_ID"];
                        if (rs["DEBITARTICLE_GUID_ID"] != System.DBNull.Value)
                        {
                            objPopupBudgetItem.ParentBudgetItem.DebitArticleID = (System.Guid)rs["DEBITARTICLE_GUID_ID"];
                        }
                        if (rs["PARENT_GUID_ID"] != System.DBNull.Value)
                        {
                            objPopupBudgetItem.ParentBudgetItem.ParentID = (System.Guid)rs["PARENT_GUID_ID"];
                        }
                        if (rs["BUDGETITEM_DESCRIPTION"] != System.DBNull.Value)
                        {
                            objPopupBudgetItem.ParentBudgetItem.BudgetItemDescription = (System.String)rs["BUDGETITEM_DESCRIPTION"];
                        }

                        objPopupBudgetItem.ParentBudgetItem.BudgetExpenseType = ((rs["BUDGETEXPENSETYPE_GUID"] != System.DBNull.Value) ? new CBudgetExpenseType()
                        {
                            uuidID = (System.Guid)rs["BUDGETEXPENSETYPE_GUID"],
                            Name = System.Convert.ToString(rs["BUDGETEXPENSETYPE_NAME"]),
                            CodeIn1C = System.Convert.ToInt32(rs["BUDGETEXPENSETYPE_1C_CODE"]),
                            IsActive = System.Convert.ToBoolean(rs["BUDGETEXPENSETYPE_ACTIVE"])
                        } : null);

                        objPopupBudgetItem.ParentBudgetItem.AccountPlan = ((rs["ACCOUNTPLAN_GUID"] != System.DBNull.Value) ? new CAccountPlan()
                        {
                            uuidID = (System.Guid)rs["ACCOUNTPLAN_GUID"],
                            Name = System.Convert.ToString(rs["ACCOUNTPLAN_NAME"]),
                            IsActive = System.Convert.ToBoolean(rs["ACCOUNTPLAN_ACTIVE"]),
                            CodeIn1C = System.Convert.ToString(rs["ACCOUNTPLAN_1C_CODE"])
                        } : null);

                        objPopupBudgetItem.ParentBudgetItem.BudgetProject = ((rs["BUDGETPROJECT_GUID"] != System.DBNull.Value) ? new CBudgetProject()
                        {
                            uuidID = (System.Guid)rs["BUDGETPROJECT_GUID"],
                            Name = System.Convert.ToString(rs["BUDGETPROJECT_NAME"]),
                            Description = "",
                            IsActive = System.Convert.ToBoolean(rs["BUDGETPROJECT_ACTIVE"]),
                            CodeIn1C = System.Convert.ToInt32(rs["BUDGETPROJECT_1C_CODE"])
                        } : null);
                        objPopupBudgetItem.ParentBudgetItem.BudgetType = ((rs["BUDGETTYPE_GUID"] != System.DBNull.Value) ? new CBudgetType()
                        {
                            uuidID = (System.Guid)rs["BUDGETTYPE_GUID"],
                            Name = System.Convert.ToString(rs["BUDGETTYPE_NAME"]),
                            Description = ((rs["BUDGETTYPE_DESCRIPTION"] == System.DBNull.Value) ? "" : (System.String)rs["BUDGETTYPE_DESCRIPTION"]),
                            IsActive = System.Convert.ToBoolean(rs["BUDGETTYPE_ACTIVE"])
                        } : null);
                        
                        objPopupBudgetItem.ParentBudgetItem.BudgetItemNum = (System.String)rs["BUDGETITEM_NUM"];
                        objPopupBudgetItem.ParentBudgetItem.Name = (System.String)rs["BUDGETITEM_NAME"];
                        objPopupBudgetItem.ParentBudgetItem.TransprtRest = (System.Boolean)rs["BUDGETITEM_TRANSPORTREST"];
                        objPopupBudgetItem.ParentBudgetItem.DontChange = (System.Boolean)rs["BUDGETITEM_DONTCHANGE"];
                        objPopupBudgetItem.ParentBudgetItem.BudgetItemID = (System.Int32)rs["BUDGETITEM_ID"];
                        objPopupBudgetItem.ParentBudgetItem.RestMoney = System.Convert.ToDouble(rs["RESTMONEY"]);
                        objPopupBudgetItem.ParentBudgetItem.BudgetName = (System.String)rs["BUDGET_NAME"];
                        objPopupBudgetItem.ParentBudgetItem.OffExpenditures = (System.Boolean)rs["OFFBUDGET_EXPENDITURES"];
                        this.m_PopupBudgetItemList.Add(objPopupBudgetItem);
                    }
                    bRet = true;
                }
                else
                {
                    DevExpress.XtraEditors.XtraMessageBox.Show("������ ������ ������� ����!", "��������",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);
                }
                rs.Close();
                rs = null;
            }
            catch (System.Exception f)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(
                "�� ������� �������� ������ ������������ ������ �������.\n\n����� ������: " + f.Message, "��������",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
			finally // ������� ���������� �������
            {
            }

            return bRet;
        }
        /// <summary>
        /// ��������� ������ �������� ������ �������
        /// </summary>
        /// <param name="objProfile">�������</param>
        /// <param name="cmd">SQl-�������</param>
        /// <param name="objPopupBudgetItem">������ � ���������� ������ ������ �������</param>
        /// <returns>true - �������� ����������; false - ������</returns>
        private System.Boolean LoadPopupChildBudgetItemList(UniXP.Common.CProfile objProfile,
            System.Data.SqlClient.SqlCommand cmd, CPopupBudgetItem objPopupBudgetItem)
        {
            System.Boolean bRet = false;
            if (cmd == null)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show("����������� ���������� � ��.", "��������",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                return bRet;
            }

            try
            {
                objPopupBudgetItem.ChlildBudgetItemList.Clear();
                cmd.Parameters.Clear();
                cmd.CommandText = System.String.Format("[{0}].[dbo].[sp_GetBudgetItemChild4BudgetDoc]", objProfile.GetOptionsDllDBName());
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ((System.Byte)(0)), ((System.Byte)(0)), "", System.Data.DataRowVersion.Current, null));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@BUDGETITEM_GUID_ID", System.Data.SqlDbType.UniqueIdentifier));
                cmd.Parameters["@BUDGETITEM_GUID_ID"].Value = objPopupBudgetItem.ParentBudgetItem.uuidID;
                System.Data.SqlClient.SqlDataReader rs = cmd.ExecuteReader();
                if (rs.HasRows)
                {
                    while (rs.Read())
                    {
                        CBudgetItem objChildBudgetItem = new CBudgetItem();

                        objChildBudgetItem.uuidID = (System.Guid)rs["BUDGETITEM_GUID_ID"];
                        if (rs["PARENT_GUID_ID"] != System.DBNull.Value)
                        {
                            objChildBudgetItem.ParentID = (System.Guid)rs["PARENT_GUID_ID"];
                        }
                        if (rs["DESCRIPTION"] != System.DBNull.Value)
                        {
                            objChildBudgetItem.BudgetItemDescription = (System.String)rs["DESCRIPTION"];
                        }
                        objChildBudgetItem.BudgetItemNum = (System.String)rs["BUDGETITEM_NUM"];
                        objChildBudgetItem.Name = (System.String)rs["BUDGETITEM_NAME"];
                        objChildBudgetItem.TransprtRest = (System.Boolean)rs["BUDGETITEM_TRANSPORTREST"];
                        objChildBudgetItem.DontChange = (System.Boolean)rs["BUDGETITEM_DONTCHANGE"];
                        objChildBudgetItem.BudgetItemID = (System.Int32)rs["BUDGETITEM_ID"];
                        objChildBudgetItem.RestMoney = System.Convert.ToDouble(rs["RESTMONEY"]);
                        objChildBudgetItem.BudgetGUID = (System.Guid)rs["BUDGET_GUID"];
                        objChildBudgetItem.BudgetName = System.Convert.ToString(rs["BUDGET_NAME"]);

                        objChildBudgetItem.BudgetExpenseType = ((rs["BUDGETEXPENSETYPE_GUID"] != System.DBNull.Value) ? new CBudgetExpenseType()
                        {
                            uuidID = (System.Guid)rs["BUDGETEXPENSETYPE_GUID"],
                            Name = System.Convert.ToString(rs["BUDGETEXPENSETYPE_NAME"]),
                            CodeIn1C = System.Convert.ToInt32(rs["BUDGETEXPENSETYPE_1C_CODE"]),
                            IsActive = System.Convert.ToBoolean(rs["BUDGETEXPENSETYPE_ACTIVE"])
                        } : null);

                        objChildBudgetItem.AccountPlan = ((rs["ACCOUNTPLAN_GUID"] != System.DBNull.Value) ? new CAccountPlan()
                        {
                            uuidID = (System.Guid)rs["ACCOUNTPLAN_GUID"],
                            Name = System.Convert.ToString(rs["ACCOUNTPLAN_NAME"]),
                            IsActive = System.Convert.ToBoolean(rs["ACCOUNTPLAN_ACTIVE"]),
                            CodeIn1C = System.Convert.ToString(rs["ACCOUNTPLAN_1C_CODE"])
                        } : null);

                        objChildBudgetItem.BudgetProject = ((rs["BUDGETPROJECT_GUID"] != System.DBNull.Value) ? new CBudgetProject()
                        {
                            uuidID = (System.Guid)rs["BUDGETPROJECT_GUID"],
                            Name = System.Convert.ToString(rs["BUDGETPROJECT_NAME"]),
                            Description = "",
                            IsActive = System.Convert.ToBoolean(rs["BUDGETPROJECT_ACTIVE"]),
                            CodeIn1C = System.Convert.ToInt32(rs["BUDGETPROJECT_1C_CODE"])
                        } : null);
                        objChildBudgetItem.BudgetType = ((rs["BUDGETTYPE_GUID"] != System.DBNull.Value) ? new CBudgetType()
                        {
                            uuidID = (System.Guid)rs["BUDGETTYPE_GUID"],
                            Name = System.Convert.ToString(rs["BUDGETTYPE_NAME"]),
                            Description = ((rs["BUDGETTYPE_DESCRIPTION"] == System.DBNull.Value) ? "" : (System.String)rs["BUDGETTYPE_DESCRIPTION"]),
                            IsActive = System.Convert.ToBoolean(rs["BUDGETTYPE_ACTIVE"])
                        } : null);


                        objPopupBudgetItem.ChlildBudgetItemList.Add(objChildBudgetItem);
                    }
                }
                bRet = true;
                rs.Close();
                rs = null;
            }
            catch (System.Exception f)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(
                "�� ������� �������� ������ �������� ������ �������.\n������������ ������: " +
                objPopupBudgetItem.ParentBudgetItem.BudgetItemFullName + "\n\n����� ������: " + f.Message, "��������",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
			finally // ������� ���������� �������
            {
            }

            return bRet;
        }

        /// <summary>
        /// ������������� ������� ������
        /// </summary>
        /// <param name="objProfile">�������</param>
        /// <param name="cmd">SQL-�������</param>
        /// <returns>true - �������� �������������; false - ������</returns>
        public System.Boolean Init(UniXP.Common.CProfile objProfile, System.Data.SqlClient.SqlCommand cmd)
        {
            System.Boolean bRet = false;

            if (cmd == null) { return bRet; }

            try
            {
                // ���������� � �� ��������, ����������� ������� �� ������� ������
                cmd.Parameters.Clear();
                cmd.CommandText = System.String.Format("[{0}].[dbo].[sp_GetBudgetDoc]", objProfile.GetOptionsDllDBName());
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ((System.Byte)(0)), ((System.Byte)(0)), "", System.Data.DataRowVersion.Current, null));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@GUID_ID", System.Data.SqlDbType.UniqueIdentifier));
                cmd.Parameters["@GUID_ID"].Value = this.m_uuidID;
                System.Data.SqlClient.SqlDataReader rs = cmd.ExecuteReader();
                if (rs.HasRows)
                {
                    // ����� ������ ��������, � ��� ��� ���������� ���� ������
                    rs.Read();
                    this.m_uuidID = (System.Guid)rs["GUID_ID"];
                    this.m_dtDate = (System.DateTime)rs["BUDGETDOC_DATE"];
                    this.m_moneyMoney = System.Convert.ToDouble(rs["BUDGETDOC_MONEY"]);
                    this.MoneyAgree = System.Convert.ToDouble(rs["BUDGETDOC_MONEYAGREE"]);
                    this.m_strObjective = (System.String)rs["BUDGETDOC_OBJECTIVE"];
                    this.m_dtPaymentDate = (System.DateTime)rs["BUDGETDOC_PAYMENTDATE"];
                    this.m_strRecipient = (System.String)rs["BUDGETDOC_RECIPIENT"];
                    this.m_strDocBasis = (System.String)rs["BUDGETDOC_DOCBASIS"];
                    if (rs["BUDGETDOC_DESCRIPTION"] != System.DBNull.Value)
                    {
                        this.m_strDescription = (System.String)rs["BUDGETDOC_DESCRIPTION"];
                    }
                    this.m_moneyMoneyAgree = System.Convert.ToDouble(rs["BUDGETDOC_MONEYAGREE"]);
                    this.m_bIsActive = (System.Boolean)rs["BUDGETDOC_ACTIVE"];
                    // ��������� �������������
                    this.m_objBudgetDep = new CBudgetDep((System.Guid)rs["BUDGETDEP_GUID_ID"], (System.String)rs["BUDGETDEP_NAME"]);
                    // ����� �������
                    this.m_objPaymentType = new CPaymentType((System.Guid)rs["PAYMENTTYPE_GUID_ID"], (System.String)rs["PAYMENTTYPE_NAME"]);
                    // ������� ���������
                    if (rs["MEASURE_GUID_ID"] != System.DBNull.Value)
                    {
                        this.m_objMeasure = new CMeasure((System.Guid)rs["MEASURE_GUID_ID"], (System.String)rs["MEASURE_NAME"]);
                    }
                    //������������ ������ �������
                    CBudgetItem objBudgetItem = new CBudgetItem();
                    objBudgetItem.uuidID = (System.Guid)rs["BUDGETITEM_GUID_ID"];
                    objBudgetItem.BudgetItemNum = (System.String)rs["BUDGETITEM_NUM"];
                    objBudgetItem.Name = (System.String)rs["BUDGETITEM_NAME"];
                    if (rs["DEBITARTICLE_GUID_ID"] != System.DBNull.Value)
                    {
                        objBudgetItem.DebitArticleID = (System.Guid)rs["DEBITARTICLE_GUID_ID"];
                    }
                    this.m_objBudgetItem = objBudgetItem;

                    // ������
                    this.m_objCurrency = new CCurrency((System.Guid)rs["CURRENCY_GUID_ID"], (System.String)rs["CURRENCY_CODE"], (System.String)rs["CURRENCY_CODE"]);
                    // ��������
                    this.m_objCompany = new CCompany((System.Guid)rs["COMPANY_GUID_ID"], (System.String)rs["COMPANY_NAME"], (System.String)rs["COMPANY_ACRONYM"]);
                    // ��������� ���������
                    this.m_objDocState = new CBudgetDocState((System.Guid)rs["BUDGETDOCSTATE_GUID_ID"], (System.String)rs["BUDGETDOCSTATE_NAME"], (System.Int32)rs["BUDGETDOCSTATE_ID"]);
                    // ��� ���������
                    this.m_objDocType = new CBudgetDocType((System.Guid)rs["BUDGETDOCTYPE_GUID_ID"],
                        (System.String)rs["BUDGETDOCTYPE_NAME"], (System.Boolean)rs["NEEDDISION"],
                            (System.String)rs["CLASS_NAME"], (System.Int32)rs["PRIORITY"]);
                    // ���������
                    this.m_OwnerUser = new CUser((System.Int32)rs["CREATEDUSER_ID"], (System.Int32)rs["UNIXPUSER_ID"], (System.String)rs["USER_LASTNAME"], (System.String)rs["USER_FIRSTNAME"]);
                    this.m_bDivision = (System.Boolean)rs["DIVISION"];
                    // ������
                    this.BudgetProject = ((rs["BUDGETPROJECT_GUID"] != System.DBNull.Value) ? new CBudgetProject()
                    {
                        uuidID = (System.Guid)rs["BUDGETPROJECT_GUID"],
                        Name = System.Convert.ToString(rs["BUDGETPROJECT_NAME"]),
                        IsActive = System.Convert.ToBoolean(rs["BUDGETPROJECT_ACTIVE"]),
                        CodeIn1C = System.Convert.ToInt32(rs["BUDGETPROJECT_1C_CODE"])
                    }
                        : null);
                    // ����
                    this.AccountPlan = ((rs["ACCOUNTPLAN_GUID"] != System.DBNull.Value) ? new CAccountPlan()
                    {
                        uuidID = (System.Guid)rs["ACCOUNTPLAN_GUID"],
                        Name = System.Convert.ToString(rs["ACCOUNTPLAN_NAME"]),
                        IsActive = System.Convert.ToBoolean(rs["ACCOUNTPLAN_ACTIVE"]),
                        CodeIn1C = System.Convert.ToString(rs["ACCOUNTPLAN_1C_CODE"])
                    }
                        : null);
                    // ��� ���������
                    this.BudgetDocCategory = ((rs["BUDGETDOCCATEGORY_GUID"] != System.DBNull.Value) ? new CBudgetDocCategory()
                    {
                        uuidID = (System.Guid)rs["BUDGETDOCCATEGORY_GUID"],
                        Name = System.Convert.ToString(rs["BUDGETDOCCATEGORY_NAME"]),
                        IsActive = System.Convert.ToBoolean(rs["BUDGETDOCCATEGORY_ACTIVE"])
                    }
                        : null);

                    bRet = true;
                }
                else
                {
                    DevExpress.XtraEditors.XtraMessageBox.Show(
                    "�� ������� ������������������� ����� CBudgetDoc.\n� �� �� ������� ����������.\n�� ���������� ��������� : " +
                    uuidID.ToString(), "��������",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);

                }
                rs.Close();
                rs = null;

                if (bRet == true)
                {
                    // ������ ������ �������
                    bRet = LoadBudgetItemList(objProfile, cmd);
                }
                if (bRet == true)
                {
                    // �������
                    this.m_objRoute = new CBudgetRoute(this.m_uuidID, cmd, objProfile);
                }
                // �������� ������ ��������
                CBudgetDocAttach.LoadAttachmentList(objProfile, cmd, this);
                cmd.Dispose();
            }
            catch (System.Exception e)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(
                "�� ������� ������������������� ����� CBudgetDoc.\n�� ���������� ��������� : " +
                    this.uuidID.ToString() + "\n����� ������: " + e.Message, "��������",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
			finally // ������� ���������� �������
            {
            }
            return bRet;
        }

        /// <summary>
        /// ������������� ������� ������
        /// </summary>
        /// <param name="objProfile">�������</param>
        /// <param name="uuidID">���������� ������������� ������</param>
        /// <returns>true - �������� �������������; false - ������</returns>
        public override System.Boolean Init(UniXP.Common.CProfile objProfile, System.Guid uuidID)
        {
            System.Boolean bRet = false;

            System.Data.SqlClient.SqlConnection DBConnection = objProfile.GetDBSource();
            if (DBConnection == null) { return bRet; }

            try
            {
                // ���������� � �� ��������, ����������� ������� �� ������� ������
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
                cmd.Connection = DBConnection;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = System.String.Format("[{0}].[dbo].[sp_GetBudgetDoc]", objProfile.GetOptionsDllDBName());
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ((System.Byte)(0)), ((System.Byte)(0)), "", System.Data.DataRowVersion.Current, null));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@GUID_ID", System.Data.SqlDbType.UniqueIdentifier));
                cmd.Parameters["@GUID_ID"].Value = uuidID;
                System.Data.SqlClient.SqlDataReader rs = cmd.ExecuteReader();
                if (rs.HasRows)
                {
                    // ����� ������ ��������, � ��� ��� ���������� ���� ������
                    rs.Read();
                    this.m_uuidID = (System.Guid)rs["GUID_ID"];
                    this.m_dtDate = (System.DateTime)rs["BUDGETDOC_DATE"];
                    this.m_moneyMoney = System.Convert.ToDouble(rs["BUDGETDOC_MONEY"]);
                    this.m_strObjective = (System.String)rs["BUDGETDOC_OBJECTIVE"];
                    this.m_dtPaymentDate = (System.DateTime)rs["BUDGETDOC_PAYMENTDATE"];
                    this.m_strRecipient = (System.String)rs["BUDGETDOC_RECIPIENT"];
                    this.m_strDocBasis = (System.String)rs["BUDGETDOC_DOCBASIS"];
                    if (rs["BUDGETDOC_DESCRIPTION"] != System.DBNull.Value)
                    {
                        this.m_strDescription = (System.String)rs["BUDGETDOC_DESCRIPTION"];
                    }
                    this.m_moneyMoneyAgree = System.Convert.ToDouble(rs["BUDGETDOC_MONEYAGREE"]);
                    this.m_bIsActive = (System.Boolean)rs["BUDGETDOC_ACTIVE"];
                    // ��������� �������������
                    this.m_objBudgetDep = new CBudgetDep((System.Guid)rs["BUDGETDEP_GUID_ID"], (System.String)rs["BUDGETDEP_NAME"]);
                    this.m_objBudgetDep.Manager = new CUser((System.Int32)rs["BUDGETDEP_MANAGER"],
                        (System.Int32)rs["BUDGETDEP_MANAGER_UniXPUserID"],
                        (System.String)rs["BUDGETDEP_MANAGER_LASTNAME"],
                        (System.String)rs["BUDGETDEP_MANAGER_FIRSTNAME"]);
                    // ����� �������
                    this.m_objPaymentType = new CPaymentType((System.Guid)rs["PAYMENTTYPE_GUID_ID"], (System.String)rs["PAYMENTTYPE_NAME"]);
                    // ������� ���������
                    if (rs["MEASURE_GUID_ID"] != System.DBNull.Value)
                    {
                        this.m_objMeasure = new CMeasure((System.Guid)rs["MEASURE_GUID_ID"], (System.String)rs["MEASURE_NAME"]);
                    }
                    //������������ ������ �������
                    CBudgetItem objBudgetItem = new CBudgetItem();
                    objBudgetItem.uuidID = (System.Guid)rs["BUDGETITEM_GUID_ID"];
                    objBudgetItem.BudgetItemNum = (System.String)rs["BUDGETITEM_NUM"];
                    objBudgetItem.Name = (System.String)rs["BUDGETITEM_NAME"];
                    objBudgetItem.MoneyInBudgetDocCurrency = this.m_moneyMoney;
                    objBudgetItem.MoneyInBudgetCurrency = this.m_moneyMoneyAgree;
                    objBudgetItem.OffExpenditures = (System.Boolean)rs["OFFBUDGET_EXPENDITURES"];
                    if (rs["DEBITARTICLE_GUID_ID"] != System.DBNull.Value)
                    {
                        objBudgetItem.DebitArticleID = (System.Guid)rs["DEBITARTICLE_GUID_ID"];
                    }
                    if (rs["BUDGETEXPENSETYPE_GUID"] != System.DBNull.Value)
                    {
                        objBudgetItem.BudgetExpenseType = new CBudgetExpenseType((System.Guid)rs["BUDGETEXPENSETYPE_GUID"],
                        (System.String)rs["BUDGETEXPENSETYPE_NAME"], "");
                    }
                    else
                    {
                        objBudgetItem.BudgetExpenseType = new CBudgetExpenseType(System.Guid.Empty, "", "");
                    }
                    this.m_objBudgetItem = objBudgetItem;

                    // ������
                    this.m_objCurrency = new CCurrency((System.Guid)rs["CURRENCY_GUID_ID"], (System.String)rs["CURRENCY_CODE"], (System.String)rs["CURRENCY_CODE"]);
                    // ��������
                    this.m_objCompany = new CCompany((System.Guid)rs["COMPANY_GUID_ID"], (System.String)rs["COMPANY_NAME"], (System.String)rs["COMPANY_ACRONYM"]);
                    // ��������� ���������
                    this.m_objDocState = new CBudgetDocState((System.Guid)rs["BUDGETDOCSTATE_GUID_ID"], (System.String)rs["BUDGETDOCSTATE_NAME"], (System.Int32)rs["BUDGETDOCSTATE_ID"]);
                    // ��� ���������
                    this.m_objDocType = new CBudgetDocType((System.Guid)rs["BUDGETDOCTYPE_GUID_ID"],
                        (System.String)rs["BUDGETDOCTYPE_NAME"], (System.Boolean)rs["NEEDDISION"],
                            (System.String)rs["CLASS_NAME"], (System.Int32)rs["PRIORITY"]);
                    // ���������
                    this.m_OwnerUser = new CUser((System.Int32)rs["CREATEDUSER_ID"], (System.Int32)rs["UNIXPUSER_ID"], (System.String)rs["USER_LASTNAME"], (System.String)rs["USER_FIRSTNAME"]);
                    this.m_bDivision = (System.Boolean)rs["DIVISION"];
                    // ������
                    this.BudgetProject = ((rs["BUDGETPROJECT_GUID"] != System.DBNull.Value) ? new CBudgetProject()
                    {
                        uuidID = (System.Guid)rs["BUDGETPROJECT_GUID"],
                        Name = System.Convert.ToString(rs["BUDGETPROJECT_NAME"]),
                        IsActive = System.Convert.ToBoolean(rs["BUDGETPROJECT_ACTIVE"]),
                        CodeIn1C = System.Convert.ToInt32(rs["BUDGETPROJECT_1C_CODE"])
                    }
                        : null);
                    // ����
                    this.AccountPlan = ((rs["ACCOUNTPLAN_GUID"] != System.DBNull.Value) ? new CAccountPlan()
                    {
                        uuidID = (System.Guid)rs["ACCOUNTPLAN_GUID"],
                        Name = System.Convert.ToString(rs["ACCOUNTPLAN_NAME"]),
                        IsActive = System.Convert.ToBoolean(rs["ACCOUNTPLAN_ACTIVE"]),
                        CodeIn1C = System.Convert.ToString(rs["ACCOUNTPLAN_1C_CODE"])
                    }
                        : null);

                    this.m_objBudgetItem.AccountPlan = this.AccountPlan;

                    // ��� ���������
                    this.BudgetDocCategory = ((rs["BUDGETDOCCATEGORY_GUID"] != System.DBNull.Value) ? new CBudgetDocCategory()
                    {
                        uuidID = (System.Guid)rs["BUDGETDOCCATEGORY_GUID"],
                        Name = System.Convert.ToString(rs["BUDGETDOCCATEGORY_NAME"]),
                        IsActive = System.Convert.ToBoolean(rs["BUDGETDOCCATEGORY_ACTIVE"])
                    }
                        : null);

                    bRet = true;
                }
                else
                {
                    DevExpress.XtraEditors.XtraMessageBox.Show(
                    "�� ������� ������������������� ����� CBudgetDoc.\n� �� �� ������� ����������.\n�� ���������� ��������� : " +
                    uuidID.ToString(), "��������",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);

                }
                rs.Close();
                rs = null;

                if (bRet == true)
                {
                    //����� ������ ���������
                    this.MoneyPayment = GetSumMoneyPayment(objProfile, cmd);
                }
                if (bRet == true)
                {
                    // ������ ������ �������
                    bRet = LoadBudgetItemList(objProfile, cmd);
                }
                if (bRet == true)
                {
                    // �������
                    this.m_objRoute = new CBudgetRoute(this.m_uuidID, cmd, objProfile);
                }
                //if( bRet == true )
                //{
                //    // ��������� ���������� � ���������� ������� � ����������� ���������� 
                //    bRet = this.LoadPopupList( objProfile, cmd );
                //}
                this.BudgetDep.LoadAdditionalManagerList(objProfile, cmd);
                // �������� ������ ��������
                CBudgetDocAttach.LoadAttachmentList(objProfile, cmd, this);
                cmd.Dispose();
            }
            catch (System.Exception e)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(
                "�� ������� ������������������� ����� CBudgetDoc.\n�� ���������� ��������� : " +
                    uuidID.ToString() + "\n����� ������: " + e.Message, "��������",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
			finally // ������� ���������� �������
            {
                DBConnection.Close();
            }
            return bRet;
        }
        /// <summary>
        /// ���������� ����� ������ ������
        /// </summary>
        /// <param name="objProfile">�������</param>
        /// <param name="cmd">SQL-�������</param>
        /// <returns>����� ������ ������</returns>
        private double GetSumMoneyPayment(UniXP.Common.CProfile objProfile, System.Data.SqlClient.SqlCommand cmd)
        {
            double doubleRet = 0;
            if (cmd == null)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show("����������� ���������� � ��.", "��������",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);
                return doubleRet;
            }
            try
            {
                cmd.Parameters.Clear();
                cmd.CommandText = System.String.Format("[{0}].[dbo].[sp_GetBudgetDocSumMoneyPayment]", objProfile.GetOptionsDllDBName());
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ((System.Byte)(0)), ((System.Byte)(0)), "", System.Data.DataRowVersion.Current, null));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@GUID_ID", System.Data.SqlDbType.UniqueIdentifier));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@SumMoneyPayment", System.Data.SqlDbType.Money, 8));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ERROR_NUM", System.Data.SqlDbType.Int, 8, System.Data.ParameterDirection.Output, false, ((System.Byte)(0)), ((System.Byte)(0)), "", System.Data.DataRowVersion.Current, null));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ERROR_MES", System.Data.SqlDbType.NVarChar, 4000));
                cmd.Parameters["@SumMoneyPayment"].Direction = System.Data.ParameterDirection.Output;
                cmd.Parameters["@ERROR_MES"].Direction = System.Data.ParameterDirection.Output;
                cmd.Parameters["@GUID_ID"].Value = this.m_uuidID;
                cmd.ExecuteNonQuery();
                if ( System.Convert.ToInt32( cmd.Parameters["@RETURN_VALUE"].Value ) == 0)
                {
                    doubleRet = System.Convert.ToDouble(cmd.Parameters["@SumMoneyPayment"].Value);
                }
            }
            catch (System.Exception f)
            {
                doubleRet = 0;

                DevExpress.XtraEditors.XtraMessageBox.Show(
                "�� ������� ���������� ����� ������ ���������.\n\n����� ������: " + f.Message, "��������",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }

            return doubleRet;
        }
        /// <summary>
        /// ����������� ������ ������ �������, ��������� � ����������
        /// </summary>
        /// <param name="objProfile">�������</param>
        /// <param name="cmd">SQL-�������</param>
        /// <returns>true - �������� ����������; false - ������</returns>
        private System.Boolean LoadBudgetItemList(UniXP.Common.CProfile objProfile,
            System.Data.SqlClient.SqlCommand cmd)
        {
            System.Boolean bRet = false;
            if (cmd == null)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show("����������� ���������� � ��.", "��������",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);
                return bRet;
            }
            try
            {
                this.m_objBudgetItemList.Clear();
                cmd.Parameters.Clear();
                cmd.CommandText = System.String.Format("[{0}].[dbo].[sp_GetBudgetDocItemList]", objProfile.GetOptionsDllDBName());
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ((System.Byte)(0)), ((System.Byte)(0)), "", System.Data.DataRowVersion.Current, null));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@BUDGETDOC_GUID_ID", System.Data.SqlDbType.UniqueIdentifier));
                cmd.Parameters["@BUDGETDOC_GUID_ID"].Value = this.m_uuidID;
                System.Data.SqlClient.SqlDataReader rs = cmd.ExecuteReader();
                if (rs.HasRows)
                {
                    while (rs.Read())
                    {
                        //������ �������
                        CBudgetItem objBudgetItem = new CBudgetItem();
                        objBudgetItem.BudgetGUID = (System.Guid)rs["BUDGET_GUID_ID"];
                        objBudgetItem.BudgetName = System.Convert.ToString(rs["BUDGET_NAME"]);
                        objBudgetItem.uuidID = (System.Guid)rs["BUDGETITEM_GUID_ID"];
                        objBudgetItem.BudgetItemNum = (System.String)rs["BUDGETITEM_NUM"];
                        objBudgetItem.Name = (System.String)rs["BUDGETITEM_NAME"];
                        if (rs["DEBITARTICLE_GUID_ID"] != System.DBNull.Value)
                        {
                            objBudgetItem.DebitArticleID = (System.Guid)rs["DEBITARTICLE_GUID_ID"];
                        }
                        if (rs["PARENT_GUID_ID"] != System.DBNull.Value)
                        {
                            objBudgetItem.ParentID = (System.Guid)rs["PARENT_GUID_ID"];
                        }
                        objBudgetItem.TransprtRest = (System.Boolean)rs["BUDGETITEM_TRANSPORTREST"];
                        objBudgetItem.DontChange = (System.Boolean)rs["BUDGETITEM_DONTCHANGE"];
                        objBudgetItem.BudgetItemID = (System.Int32)rs["BUDGETITEM_ID"];
                        objBudgetItem.MoneyInBudgetDocCurrency = System.Convert.ToDouble(rs["BUDGETDOCITEM_DOCMONEY"]);
                        objBudgetItem.MoneyInBudgetCurrency = System.Convert.ToDouble(rs["BUDGETDOCITEM_MONEY"]);
                        objBudgetItem.RestMoney = System.Convert.ToDouble(rs["BUDGETITEMBALANS"]);

                        objBudgetItem.BudgetExpenseType = ((rs["BUDGETEXPENSETYPE_GUID"] != System.DBNull.Value) ? new CBudgetExpenseType()
                        {
                            uuidID = (System.Guid)rs["BUDGETEXPENSETYPE_GUID"],
                            Name = System.Convert.ToString(rs["BUDGETEXPENSETYPE_NAME"]),
                            CodeIn1C = System.Convert.ToInt32(rs["BUDGETEXPENSETYPE_1C_CODE"]),
                            IsActive = System.Convert.ToBoolean(rs["BUDGETEXPENSETYPE_ACTIVE"])
                        } : null);

                        objBudgetItem.AccountPlan = ((rs["ACCOUNTPLAN_GUID"] != System.DBNull.Value) ? new CAccountPlan()
                        {
                            uuidID = (System.Guid)rs["ACCOUNTPLAN_GUID"],
                            Name = System.Convert.ToString(rs["ACCOUNTPLAN_NAME"]),
                            IsActive = System.Convert.ToBoolean(rs["ACCOUNTPLAN_ACTIVE"]),
                            CodeIn1C = System.Convert.ToString(rs["ACCOUNTPLAN_1C_CODE"])
                        } : null);

                        objBudgetItem.BudgetProject = ((rs["BUDGETPROJECT_GUID"] != System.DBNull.Value) ? new CBudgetProject()
                        {
                            uuidID = (System.Guid)rs["BUDGETPROJECT_GUID"],
                            Name = System.Convert.ToString(rs["BUDGETPROJECT_NAME"]),
                            Description = "",
                            IsActive = System.Convert.ToBoolean(rs["BUDGETPROJECT_ACTIVE"]),
                            CodeIn1C = System.Convert.ToInt32(rs["BUDGETPROJECT_1C_CODE"])
                        } : null);

                        this.m_objBudgetItemList.Add(objBudgetItem);
                    }
                }
                rs.Close();
                rs = null;

                bRet = true;
            }
            catch (System.Exception f)
            {
                bRet = false;

                DevExpress.XtraEditors.XtraMessageBox.Show(
                "�� ������� ��������� ������ ������ �������.\n\n����� ������: " + f.Message, "��������",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }

            return bRet;
        }

        /// <summary>
        /// ����������� ������ ������ �������, ��������� � ����������
        /// </summary>
        /// <param name="objProfile">�������</param>
        /// <returns>true - �������� ����������; false - ������</returns>
        public System.Boolean LoadBudgetItemList(UniXP.Common.CProfile objProfile)
        {
            System.Boolean bRet = false;
            System.Data.SqlClient.SqlConnection DBConnection = objProfile.GetDBSource();
            if (DBConnection == null)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show("����������� ���������� � ��.", "��������",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);
                return bRet;
            }
            try
            {
                this.m_objBudgetItemList.Clear();
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
                cmd.Connection = DBConnection;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = System.String.Format("[{0}].[dbo].[sp_GetBudgetDocItemList]", objProfile.GetOptionsDllDBName());
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ((System.Byte)(0)), ((System.Byte)(0)), "", System.Data.DataRowVersion.Current, null));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@BUDGETDOC_GUID_ID", System.Data.SqlDbType.UniqueIdentifier));
                cmd.Parameters["@BUDGETDOC_GUID_ID"].Value = this.m_uuidID;
                System.Data.SqlClient.SqlDataReader rs = cmd.ExecuteReader();
                if (rs.HasRows)
                {
                    while (rs.Read())
                    {
                        //������ �������
                        CBudgetItem objBudgetItem = new CBudgetItem();
                        objBudgetItem.BudgetGUID = (System.Guid)rs["BUDGET_GUID_ID"];
                        objBudgetItem.BudgetName = System.Convert.ToString(rs["BUDGET_NAME"]);
                        objBudgetItem.uuidID = (System.Guid)rs["BUDGETITEM_GUID_ID"];
                        objBudgetItem.BudgetItemNum = (System.String)rs["BUDGETITEM_NUM"];
                        objBudgetItem.Name = (System.String)rs["BUDGETITEM_NAME"];
                        if (rs["DEBITARTICLE_GUID_ID"] != System.DBNull.Value)
                        {
                            objBudgetItem.DebitArticleID = (System.Guid)rs["DEBITARTICLE_GUID_ID"];
                        }
                        if (rs["PARENT_GUID_ID"] != System.DBNull.Value)
                        {
                            objBudgetItem.ParentID = (System.Guid)rs["PARENT_GUID_ID"];
                        }
                        objBudgetItem.TransprtRest = (System.Boolean)rs["BUDGETITEM_TRANSPORTREST"];
                        objBudgetItem.DontChange = (System.Boolean)rs["BUDGETITEM_DONTCHANGE"];
                        objBudgetItem.BudgetItemID = (System.Int32)rs["BUDGETITEM_ID"];
                        objBudgetItem.MoneyInBudgetDocCurrency = System.Convert.ToDouble(rs["BUDGETDOCITEM_DOCMONEY"]);
                        objBudgetItem.MoneyInBudgetCurrency = System.Convert.ToDouble(rs["BUDGETDOCITEM_MONEY"]);
                        objBudgetItem.RestMoney = System.Convert.ToDouble(rs["BUDGETITEMBALANS"]);

                        objBudgetItem.BudgetExpenseType = ((rs["BUDGETEXPENSETYPE_GUID"] != System.DBNull.Value) ? new CBudgetExpenseType()
                        {
                            uuidID = (System.Guid)rs["BUDGETEXPENSETYPE_GUID"],
                            Name = System.Convert.ToString(rs["BUDGETEXPENSETYPE_NAME"]),
                            CodeIn1C = System.Convert.ToInt32(rs["BUDGETEXPENSETYPE_1C_CODE"]),
                            IsActive = System.Convert.ToBoolean(rs["BUDGETEXPENSETYPE_ACTIVE"])
                        } : null);

                        objBudgetItem.AccountPlan = ((rs["ACCOUNTPLAN_GUID"] != System.DBNull.Value) ? new CAccountPlan()
                        {
                            uuidID = (System.Guid)rs["ACCOUNTPLAN_GUID"],
                            Name = System.Convert.ToString(rs["ACCOUNTPLAN_NAME"]),
                            IsActive = System.Convert.ToBoolean(rs["ACCOUNTPLAN_ACTIVE"]),
                            CodeIn1C = System.Convert.ToString(rs["ACCOUNTPLAN_1C_CODE"])
                        } : null);

                        objBudgetItem.BudgetProject = ((rs["BUDGETPROJECT_GUID"] != System.DBNull.Value) ? new CBudgetProject()
                        {
                            uuidID = (System.Guid)rs["BUDGETPROJECT_GUID"],
                            Name = System.Convert.ToString(rs["BUDGETPROJECT_NAME"]),
                            Description = "",
                            IsActive = System.Convert.ToBoolean(rs["BUDGETPROJECT_ACTIVE"]),
                            CodeIn1C = System.Convert.ToInt32(rs["BUDGETPROJECT_1C_CODE"])
                        } : null);

                        this.m_objBudgetItemList.Add(objBudgetItem);
                    }
                }
                rs.Close();

                foreach ( ERP_Budget.Common.CBudgetItem objItem in this.BudgetItemList )
                {
                    objItem.LoadBudgetDocItemDecodeList(objProfile, this.uuidID);
                }

                rs = null;
                cmd = null;

                bRet = true;
            }
            catch (System.Exception f)
            {
                bRet = false;

                DevExpress.XtraEditors.XtraMessageBox.Show(
                "�� ������� ��������� ������ ������ �������.\n\n����� ������: " + f.Message, "��������",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
            finally
            {
                DBConnection.Close();
            }
            return bRet;
        }

        #endregion

        #region Remove ( ������� ������ �� �� )
        /// <summary>
        /// ������� ������ �� ��
        /// </summary>
        /// <param name="objProfile">�������</param>
        /// <returns>true - ������� ����������; false - ������</returns>
        public override System.Boolean Remove(UniXP.Common.CProfile objProfile)
        {
            System.Boolean bRet = false;
            // ���������� ������������� �� ������ ���� ������
            if (this.m_uuidID == System.Guid.Empty)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show("������������ �������� ����������� �������������� �������\n�� : " +
                    this.m_uuidID.ToString(), "��������",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                return bRet;
            }

            System.Data.SqlClient.SqlConnection DBConnection = objProfile.GetDBSource();
            if (DBConnection == null) { return bRet; }
            System.Data.SqlClient.SqlTransaction DBTransaction = DBConnection.BeginTransaction();

            try
            {
                // ���������� � �� ��������, ����������� ������� �� �������� ������
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
                cmd.Connection = DBConnection;
                cmd.Transaction = DBTransaction;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = System.String.Format("[{0}].[dbo].[sp_DeleteBudgetDoc]", objProfile.GetOptionsDllDBName());
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ((System.Byte)(0)), ((System.Byte)(0)), "", System.Data.DataRowVersion.Current, null));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@GUID_ID", System.Data.SqlDbType.UniqueIdentifier));
                cmd.Parameters["@GUID_ID"].Value = this.m_uuidID;
                cmd.ExecuteNonQuery();
                System.Int32 iRet = (System.Int32)cmd.Parameters["@RETURN_VALUE"].Value;
                if (iRet == 0)
                {
                    // ������������ ����������
                    DBTransaction.Commit();
                    bRet = true;
                }
                else
                {
                    // ���������� ����������
                    DBTransaction.Rollback();
                    switch (iRet)
                    {
                        case 1:
                            {
                                DevExpress.XtraEditors.XtraMessageBox.Show("�������� ������ � ���������.\n�������� ����������.\n�� ��������� : " + this.uuidID.ToString(), "��������",
                                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);
                                break;
                            }
                        default:
                            {
                                DevExpress.XtraEditors.XtraMessageBox.Show("������ �������� ���������� ���������\n�� ��������� : " + this.uuidID.ToString(), "��������",
                                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                                break;
                            }
                    }
                }
                cmd.Dispose();
            }
            catch (System.Exception e)
            {
                DBTransaction.Rollback();
                DevExpress.XtraEditors.XtraMessageBox.Show(
                "�� ������� ������� ��������� ��������.\n�� ��������� : " + this.uuidID.ToString() + "\n����� ������: " + e.Message, "��������",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
			finally // ������� ���������� �������
            {
                DBConnection.Close();
            }
            return bRet;
        }
        /// <summary>
        /// �������� ���������� ��������� ( ������ "���������", ��������� "������" )
        /// </summary>
        /// <param name="objProfile">�������</param>
        /// <param name="iUserID">�� ������������</param>
        /// <returns>true - ������� ����������; false - ������</returns>
        public System.Boolean Drop(UniXP.Common.CProfile objProfile, System.Int32 iUserID)
        {
            System.Boolean bRet = false;

            // ���������� ������������� �� ������ ���� ������
            if (this.m_uuidID == System.Guid.Empty)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show("������������ �������� ����������� �������������� �������\n�� : " +
                    this.m_uuidID.ToString(), "��������",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                return bRet;
            }

            System.Data.SqlClient.SqlConnection DBConnection = objProfile.GetDBSource();
            if (DBConnection == null)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show("����������� ���������� � ��.", "��������",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                return bRet;
            }

            System.Data.SqlClient.SqlTransaction DBTransaction = DBConnection.BeginTransaction();

            try
            {
                // ���������� � �� ��������, ����������� ������� �� �������� ������
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
                cmd.Connection = DBConnection;
                cmd.Transaction = DBTransaction;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = System.String.Format("[{0}].[dbo].[sp_DropBudgetDoc]", objProfile.GetOptionsDllDBName());
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ((System.Byte)(0)), ((System.Byte)(0)), "", System.Data.DataRowVersion.Current, null));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@BUDGETDOC_GUID_ID", System.Data.SqlDbType.UniqueIdentifier));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@USER_ID", System.Data.SqlDbType.Int, 4));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ERROR_NUM", System.Data.SqlDbType.Int, 8, System.Data.ParameterDirection.Output, false, ((System.Byte)(0)), ((System.Byte)(0)), "", System.Data.DataRowVersion.Current, null));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ERROR_MES", System.Data.SqlDbType.NVarChar, 4000));
                cmd.Parameters["@ERROR_MES"].Direction = System.Data.ParameterDirection.Output;
                cmd.Parameters["@BUDGETDOC_GUID_ID"].Value = this.m_uuidID;
                cmd.Parameters["@USER_ID"].Value = iUserID;

                cmd.ExecuteNonQuery();
                System.Int32 iRet = (System.Int32)cmd.Parameters["@RETURN_VALUE"].Value;
                if (iRet == 0)
                {
                    // ������������ ����������
                    DBTransaction.Commit();
                    bRet = true;
                }
                else
                {
                    // ���������� ����������
                    DBTransaction.Rollback();
                    DevExpress.XtraEditors.XtraMessageBox.Show("������ �������� ���������.\n\n" +
                        (System.String)cmd.Parameters["@ERROR_MES"].Value, "������",
                       System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);
                }

                cmd = null;
            }
            catch (System.Exception f)
            {
                DBTransaction.Rollback();
                DevExpress.XtraEditors.XtraMessageBox.Show(
                "�� ������� ������� ��������� ��������.\n�� ��������� : " + this.uuidID.ToString() + "\n\n����� ������: " + f.Message, "��������",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
			finally // ������� ���������� �������
            {
                DBConnection.Close();
            }

            return bRet;
        }
        #endregion

        #region Add ( �������� ������ � �� )
        /// <summary>
        /// ��������� ��� �� ����������� ��� ���������� � �� �������� �������
        /// </summary>
        /// <returns>true - ��� �������� �������; false - �� ��� ��� ������</returns>
        private System.Boolean CheckReadyForSave()
        {
            System.Boolean bRet = false;
            try
            {
                // ��������� �������������
                if (this.m_objBudgetDep == null) { return bRet; }
                // ������ �������
                if ((this.m_objBudgetItem == null) || (this.m_objBudgetItem.AccountPlan == null)) { return bRet; }
                if ((this.m_objBudgetItemList == null) || (this.m_objBudgetItemList.Count == 0)) { return bRet; }
                else
                {
                    if (this.DocState == null)
                    {
                        // �����
                        System.Int32 iOkCount = 0;
                        System.Int32 iCancelCount = 0;

                        foreach (CBudgetItem objBudgetItem in m_objBudgetItemList)
                        {
                            if ((objBudgetItem.IsDefficit == true) || (objBudgetItem.AccountPlan == null)) { iCancelCount++; }
                            else { iOkCount++; }
                        }

                        if (((iOkCount == 0) || (iCancelCount == 0)) == false) { return bRet; }
                    }
                }
                // ��������
                if (this.m_objCompany == null) { return bRet; }
                // ������
                if (this.m_objCurrency == null) { return bRet; }
                // ��� ���������
                if (this.m_objDocType == null) { return bRet; }
                // ����� ������
                if (this.m_objPaymentType == null) { return bRet; }
                // ������� ���������
                if ((this.m_objRoute == null) || (this.m_objRoute.RoutePointList.Count == 0)) { return bRet; }
                else
                {
                    System.Boolean bOK = false;
                    foreach (ERP_Budget.Common.CRoutePoint objRoutePoint in this.m_objRoute.RoutePointList)
                    {
                        bOK = (objRoutePoint.UserEvent != null);
                        if (bOK == false) { break; }
                    }
                    if (bOK == false) { return bRet; }
                }
                // ���������� �������
                if (this.m_strRecipient == "") { return bRet; }
                // ����
                if (this.m_strObjective == "") { return bRet; }

                bRet = true;
            }
            catch (System.Exception f)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(
                "������ �������� ������� ���������� ���������.\n�� ���������: " + this.uuidID.ToString() + "\n����� ������: " + f.Message, "��������",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
            return bRet;
        }
        /// <summary>
        /// ��������� ���������� ������� ���������� ���������
        /// </summary>
        /// <returns>true - ��� �������� �������; false - ������</returns>
        public System.Boolean CheckValidProperties()
        {
            System.Boolean bRet = false;
            try
            {
                // ���������� ������� ��������� �������������
                if (this.m_objBudgetDep == null)
                {
                    DevExpress.XtraEditors.XtraMessageBox.Show("������� ��������� �������������", "��������",
                        System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);
                    return bRet;
                }
                // ���������� ������� ������ �������
                if ((this.m_objBudgetItemList == null) || (this.m_objBudgetItemList.Count == 0))
                {
                    DevExpress.XtraEditors.XtraMessageBox.Show("������� ������ �������", "��������",
                        System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);
                    return bRet;
                }
                // ���������� ������� ��������
                if (this.m_objCompany == null)
                {
                    DevExpress.XtraEditors.XtraMessageBox.Show("������� ��������", "��������",
                        System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);
                    return bRet;
                }
                // ���������� ������� ������
                if (this.m_objCurrency == null)
                {
                    DevExpress.XtraEditors.XtraMessageBox.Show("������� ������", "��������",
                        System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);
                    return bRet;
                }
                // ���������� ������� ��� ���������
                if (this.m_objDocType == null)
                {
                    DevExpress.XtraEditors.XtraMessageBox.Show("������� ��� ���������", "��������",
                        System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);
                    return bRet;
                }
                //// ���������� ������� ������� ���������
                //if( this.m_objMeasure == null )
                //{
                //    DevExpress.XtraEditors.XtraMessageBox.Show(  "������� ������� ���������", "��������",
                //        System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning );
                //    return bRet;
                //}
                // ���������� ������� ����� ������
                if (this.m_objPaymentType == null)
                {
                    DevExpress.XtraEditors.XtraMessageBox.Show("������� ����� ������", "��������",
                        System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);
                    return bRet;
                }
                // ���������� ������� ������� ���������
                if (this.m_objRoute.RoutePointList.Count == 0)
                {
                    DevExpress.XtraEditors.XtraMessageBox.Show("������� ������� ���������", "��������",
                        System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);
                    return bRet;
                }
                else
                {
                    System.Boolean bOK = false;
                    foreach (ERP_Budget.Common.CRoutePoint objRoutePoint in this.m_objRoute.RoutePointList)
                    {
                        bOK = (objRoutePoint.UserEvent != null);
                        if (bOK == false) { break; }
                    }
                    bRet = bOK;
                    if (bRet == false)
                    {
                        DevExpress.XtraEditors.XtraMessageBox.Show("�� �� ���� ������ �������� ������ ������������.", "��������",
                            System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);
                        return bRet;
                    }
                }
                // ���������� ������� ���������� �������
                if (this.m_strRecipient == "")
                {
                    DevExpress.XtraEditors.XtraMessageBox.Show("������� ���������� �������", "��������",
                        System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);
                    return bRet;
                }
                // ���������� ������� ����
                if (this.m_strObjective == "")
                {
                    DevExpress.XtraEditors.XtraMessageBox.Show("������� ����", "��������",
                        System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);
                    return bRet;
                }
                //// ���������� ������� �������������� ���������
                //if( this.m_strDocBasis == "" )
                //{
                //    DevExpress.XtraEditors.XtraMessageBox.Show( "������� �������������� ���������", "��������",
                //        System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning );
                //    return bRet;
                //}
                bRet = true;
            }
            catch (System.Exception f)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(
                "������ �������� ������� ���������� ���������.\n�� ��������� : " + this.uuidID.ToString() + "\n����� ������: " + f.Message, "��������",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
            return bRet;
        }

        /// <summary>
        /// �������� ������ � ��
        /// </summary>
        /// <param name="objProfile">�������</param>
        /// <returns>true - ������� ����������; false - ������</returns>
        public override System.Boolean Add(UniXP.Common.CProfile objProfile)
        {
            return false;
        }

        /// <summary>
        /// �������� ������ � ��
        /// </summary>
        /// <param name="objProfile">�������</param>
        /// <param name="cmd">SQL-�������</param>
        /// <returns>true - ������� ����������; false - ������</returns>
        private System.Boolean AddBudgetDoc(UniXP.Common.CProfile objProfile, System.Data.SqlClient.SqlCommand cmd,
            ViewDocVariantCondition objDocVariantCondn)
        {
            System.Boolean bRet = false;

            // �������� ������� ���������� ���������
            if (this.CheckValidProperties() == false) { return bRet; }
            if (cmd == null) { return bRet; }
            try
            {
                // ���������� � �� ��������, ����������� ������� �� �������� ������ � ��
                cmd.CommandTimeout = 600;
                cmd.Parameters.Clear();
                cmd.CommandText = System.String.Format("[{0}].[dbo].[sp_AddBudgetDoc]", objProfile.GetOptionsDllDBName());
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ((System.Byte)(0)), ((System.Byte)(0)), "", System.Data.DataRowVersion.Current, null));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@GUID_ID", System.Data.SqlDbType.UniqueIdentifier, 4, System.Data.ParameterDirection.Output, false, ((System.Byte)(0)), ((System.Byte)(0)), "", System.Data.DataRowVersion.Current, null));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@BUDGETDOC_DATE", System.Data.SqlDbType.DateTime, 4));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@BUDGETDOC_MONEY", System.Data.SqlDbType.Money, 8));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@BUDGETDOC_OBJECTIVE", System.Data.DbType.String));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@BUDGETDOC_PAYMENTDATE", System.Data.SqlDbType.DateTime, 4));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@BUDGETDOC_RECIPIENT", System.Data.DbType.String));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@BUDGETDOC_DOCBASIS", System.Data.DbType.String));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@BUDGETDOC_DESCRIPTION", System.Data.DbType.String));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@BUDGETDOC_MONEYAGREE", System.Data.SqlDbType.Money, 8));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@BUDGETDEP_GUID_ID", System.Data.SqlDbType.UniqueIdentifier, 4));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@BUDGETITEM_GUID_ID", System.Data.SqlDbType.UniqueIdentifier, 4));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@PAYMENTTYPE_GUID_ID", System.Data.SqlDbType.UniqueIdentifier, 4));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@CURRENCY_GUID_ID", System.Data.SqlDbType.UniqueIdentifier, 4));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@MEASURE_GUID_ID", System.Data.SqlDbType.UniqueIdentifier, 4));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@COMPANY_GUID_ID", System.Data.SqlDbType.UniqueIdentifier, 4));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@BUDGETDOCSTATE_GUID_ID", System.Data.SqlDbType.UniqueIdentifier, 4));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@BUDGETDOCTYPE_GUID_ID", System.Data.SqlDbType.UniqueIdentifier, 4));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@BUDGETDOC_ACTIVE", System.Data.SqlDbType.Bit, 1));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@BUDGETDOC_DEFICIT", System.Data.SqlDbType.Money, 8));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@CREATEDUSER_ID", System.Data.SqlDbType.Int, 4));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@DIVISION", System.Data.SqlDbType.Bit, 1));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@NEWGUID_ID", System.Data.SqlDbType.UniqueIdentifier, 4));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ERROR_NUM", System.Data.SqlDbType.Int, 8, System.Data.ParameterDirection.Output, false, ((System.Byte)(0)), ((System.Byte)(0)), "", System.Data.DataRowVersion.Current, null));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ERROR_MES", System.Data.SqlDbType.NVarChar, 4000));
                cmd.Parameters["@ERROR_MES"].Direction = System.Data.ParameterDirection.Output;

                if (this.BudgetProject != null)
                {
                    cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@BUDGETPROJECT_GUID", System.Data.SqlDbType.UniqueIdentifier));
                    cmd.Parameters["@BUDGETPROJECT_GUID"].Value = this.BudgetProject.uuidID; 
                }
                if (this.AccountPlan != null)
                {
                    cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ACCOUNTPLAN_GUID", System.Data.SqlDbType.UniqueIdentifier));
                    cmd.Parameters["@ACCOUNTPLAN_GUID"].Value = this.AccountPlan.uuidID;
                }
                if (this.BudgetDocCategory != null)
                {
                    cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@BUDGETDOCCATEGORY_GUID", System.Data.SqlDbType.UniqueIdentifier));
                    cmd.Parameters["@BUDGETDOCCATEGORY_GUID"].Value = this.BudgetDocCategory.uuidID;
                }

                cmd.Parameters["@NEWGUID_ID"].Value = this.m_uuidID;
                cmd.Parameters["@BUDGETDOC_DATE"].Value = this.m_dtDate;
                cmd.Parameters["@BUDGETDOC_MONEY"].Value = this.m_moneyMoney;
                cmd.Parameters["@BUDGETDOC_OBJECTIVE"].Value = this.m_strObjective;
                cmd.Parameters["@BUDGETDOC_PAYMENTDATE"].Value = this.m_dtPaymentDate;
                cmd.Parameters["@BUDGETDOC_RECIPIENT"].Value = this.m_strRecipient;
                cmd.Parameters["@BUDGETDOC_DOCBASIS"].Value = this.m_strDocBasis;
                cmd.Parameters["@BUDGETDOC_DESCRIPTION"].Value = this.m_strDescription;
                cmd.Parameters["@BUDGETDOC_MONEYAGREE"].Value = this.m_moneyMoneyAgree;
                cmd.Parameters["@BUDGETDEP_GUID_ID"].Value = this.m_objBudgetDep.uuidID;
                cmd.Parameters["@BUDGETITEM_GUID_ID"].Value = this.m_objBudgetItem.uuidID;
                cmd.Parameters["@PAYMENTTYPE_GUID_ID"].Value = this.m_objPaymentType.uuidID;
                cmd.Parameters["@CURRENCY_GUID_ID"].Value = this.m_objCurrency.uuidID;
                //cmd.Parameters[ "@MEASURE_GUID_ID" ].Value = this.m_objMeasure.uuidID;
                cmd.Parameters["@COMPANY_GUID_ID"].Value = this.m_objCompany.uuidID;
                cmd.Parameters["@BUDGETDOCSTATE_GUID_ID"].Value = objDocVariantCondn.NextDocState.uuidID;
                cmd.Parameters["@BUDGETDOCTYPE_GUID_ID"].Value = this.m_objDocType.uuidID;
                cmd.Parameters["@BUDGETDOC_ACTIVE"].Value = !(objDocVariantCondn.IsEndRoute); //this.m_bIsActive;
                cmd.Parameters["@BUDGETDOC_DEFICIT"].Value = this.m_BudgetDocDeficit;
                cmd.Parameters["@CREATEDUSER_ID"].Value = this.m_OwnerUser.ulID;
                cmd.Parameters["@DIVISION"].Value = this.m_bDivision;
                cmd.ExecuteNonQuery();
                System.Int32 iRet = (System.Int32)cmd.Parameters["@RETURN_VALUE"].Value;
                if (iRet == 0)
                {
                    this.m_uuidID = (System.Guid)cmd.Parameters["@GUID_ID"].Value;
                    // ��������� ������ ������ �������
                    bRet = SaveBudgetItemList(cmd, objProfile);
                    if ( ( bRet == true ) && ( this.m_bResetBudgetItem == true ) )
                    {
                        // ��������� ������ �����������
                        bRet = SaveBudgetDocItemList(cmd, objProfile);
                    }
                    if (bRet == true)
                    {
                        // ��������� ������� � ��
                        bRet = this.m_objRoute.Update(objProfile, cmd);
                        if (bRet == true)
                        {
                            // ��������� ������ ��������
                            bRet = this.SaveAttachmentList(objProfile, cmd);
                            if (bRet == true)
                            {
                                // � ������ ��������� ��� ���������� �� �������� ������������
                                CBudgetMail.AddMailInQueue(objProfile, cmd, this.m_uuidID);
                            }
                        }
                    }
                }
                else
                {
                    DevExpress.XtraEditors.XtraMessageBox.Show("������ �������� ���������� ���������.\n����� ������: " +
                            (System.String)cmd.Parameters["@ERROR_MES"].Value, "��������",
                        System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                }
            }
            catch (System.Exception f)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(
                    "������ �������� ���������� ���������.\n����� ������: " +
                    (System.String)cmd.Parameters["@ERROR_MES"].Value + "\n\n" + f.Message, "��������",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
			finally // ������� ���������� �������
            {
            }
            return bRet;
        }
        /// <summary>
        /// ��������� ������ ������ �������, ��������� � ��������� ����������
        /// </summary>
        /// <param name="cmd">SQL-�������</param>
        /// <param name="objProfile">�������</param>
        /// <returns>true - ������� ���������� ��������; false - ������</returns>
        private System.Boolean SaveBudgetItemList(System.Data.SqlClient.SqlCommand cmd,
            UniXP.Common.CProfile objProfile)
        {
            System.Boolean bRet = false;
            if (cmd == null)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show("����������� ���������� � ��.", "��������",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                return bRet;
            }
            if ((this.m_objBudgetItemList == null) || (this.m_objBudgetItemList.Count == 0))
            {
                DevExpress.XtraEditors.XtraMessageBox.Show("���������� ��������� �� ��������� ������ ��������.", "��������",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                return bRet;
            }

            try
            {
                // ������ ������� ������ ������ �������
                cmd.Parameters.Clear();
                cmd.CommandText = System.String.Format("[{0}].[dbo].[sp_DeleteBudgetDocItemList]", objProfile.GetOptionsDllDBName());
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ((System.Byte)(0)), ((System.Byte)(0)), "", System.Data.DataRowVersion.Current, null));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@BUDGETDOC_GUID_ID", System.Data.SqlDbType.UniqueIdentifier));

                cmd.Parameters["@BUDGETDOC_GUID_ID"].Value = this.m_uuidID;
                cmd.ExecuteNonQuery();
                System.Int32 iRet = (System.Int32)cmd.Parameters["@RETURN_VALUE"].Value;
                if (iRet == 0)
                {
                    // ������ ��������� ������ ������ �������� � �������
                    cmd.Parameters.Clear();
                    cmd.CommandText = System.String.Format("[{0}].[dbo].[sp_AddBudgetDocItem]", objProfile.GetOptionsDllDBName());
                    cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ((System.Byte)(0)), ((System.Byte)(0)), "", System.Data.DataRowVersion.Current, null));
                    cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@BUDGETDOC_GUID_ID", System.Data.SqlDbType.UniqueIdentifier));
                    cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@BUDGETITEM_GUID_ID", System.Data.SqlDbType.UniqueIdentifier));
                    cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@BUDGETDOCITEM_MONEY", System.Data.SqlDbType.Money));
                    cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@BUDGETDOCITEM_DOCMONEY", System.Data.SqlDbType.Money));

                    cmd.Parameters["@BUDGETDOC_GUID_ID"].Value = this.m_uuidID;
                    foreach (CBudgetItem objBudgetItem in this.m_objBudgetItemList)
                    {
                        cmd.Parameters["@BUDGETITEM_GUID_ID"].Value = objBudgetItem.uuidID;
                        cmd.Parameters["@BUDGETDOCITEM_MONEY"].Value = objBudgetItem.MoneyInBudgetCurrency;
                        cmd.Parameters["@BUDGETDOCITEM_DOCMONEY"].Value = objBudgetItem.MoneyInBudgetDocCurrency;
                        cmd.ExecuteNonQuery();
                        iRet = (System.Int32)cmd.Parameters["@RETURN_VALUE"].Value;
                        if (iRet != 0)
                        {
                            switch (iRet)
                            {
                                case 1:
                                    DevExpress.XtraEditors.XtraMessageBox.Show("�� ������ ��������� �������� � �������� ���������������\n��: " + this.uuidID.ToString(), "��������",
                                        System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);
                                    break;
                                case 2:
                                    DevExpress.XtraEditors.XtraMessageBox.Show("�� ������� ������ ������� � �������� ���������������\n��: " + objBudgetItem.uuidID.ToString(), "��������",
                                        System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                                    break;
                                default:
                                    DevExpress.XtraEditors.XtraMessageBox.Show("������ ���������� ������ ������ ��������.", "��������",
                                        System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                                    break;
                            }
                            break;
                        }
                    }

                    bRet = (iRet == 0);
                }
                else
                {
                    DevExpress.XtraEditors.XtraMessageBox.Show("�� ������� �������� ������ ������ ������� � ���������.\n�� ��������: " +
                        this.m_uuidID.ToString(), "��������",
                        System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                }
            }
            catch (System.Exception f)
            {
                bRet = false;
                DevExpress.XtraEditors.XtraMessageBox.Show(
                "�� ������� ��������� ������ ������ ��������.\n\n����� ������: " + f.Message, "��������",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
			finally // ������� ���������� �������
            {
            }
            return bRet;
        }
        /// <summary>
        /// ��������� ������ ������ �������, ��������� � ��������� ����������
        /// </summary>
        /// <param name="BUDGETDOC_GUID_ID">�� ���������� ���������</param>
        /// <param name="objBudgetItemList">������ ������ ������� ���������</param>
        /// <param name="cmd">SQL-�������</param>
        /// <param name="objProfile">�������</param>
        /// <param name="iRetCode">��� �������� �������� ���������</param>
        /// <param name="strErr">��������� �� ������</param>
        /// <returns>true - ������� ���������� ��������; false - ������</returns>
        public static System.Boolean SaveBudgetItemList( System.Guid BUDGETDOC_GUID_ID, List<CBudgetItem> objBudgetItemList, 
            System.Data.SqlClient.SqlCommand cmd,
            UniXP.Common.CProfile objProfile, ref System.Int32 iRetCode, ref System.String strErr)
        {
            System.Boolean bRet = false;
            if (cmd == null)
            {
                strErr += ("����������� ���������� � ��.");
                return bRet;
            }
            if ((objBudgetItemList == null) || (objBudgetItemList.Count == 0))
            {
                strErr += ("���������� ��������� �� ��������� ������ ��������.");
                return bRet;
            }

            try
            {
                // ������ ������� ������ ������ �������
                cmd.Parameters.Clear();
                cmd.CommandText = System.String.Format("[{0}].[dbo].[sp_DeleteBudgetDocItemList]", objProfile.GetOptionsDllDBName());
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ((System.Byte)(0)), ((System.Byte)(0)), "", System.Data.DataRowVersion.Current, null));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@BUDGETDOC_GUID_ID", System.Data.SqlDbType.UniqueIdentifier));

                cmd.Parameters["@BUDGETDOC_GUID_ID"].Value = BUDGETDOC_GUID_ID;
                cmd.ExecuteNonQuery();
                System.Int32 iRet = (System.Int32)cmd.Parameters["@RETURN_VALUE"].Value;
                if (iRet == 0)
                {
                    // ������ ��������� ������ ������ �������� � �������
                    cmd.Parameters.Clear();
                    cmd.CommandText = System.String.Format("[{0}].[dbo].[sp_AddBudgetDocItem]", objProfile.GetOptionsDllDBName());
                    cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ((System.Byte)(0)), ((System.Byte)(0)), "", System.Data.DataRowVersion.Current, null));
                    cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@BUDGETDOC_GUID_ID", System.Data.SqlDbType.UniqueIdentifier));
                    cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@BUDGETITEM_GUID_ID", System.Data.SqlDbType.UniqueIdentifier));
                    cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@BUDGETDOCITEM_MONEY", System.Data.SqlDbType.Money));
                    cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@BUDGETDOCITEM_DOCMONEY", System.Data.SqlDbType.Money));

                    cmd.Parameters["@BUDGETDOC_GUID_ID"].Value = BUDGETDOC_GUID_ID;
                    foreach (CBudgetItem objBudgetItem in objBudgetItemList)
                    {
                        cmd.Parameters["@BUDGETITEM_GUID_ID"].Value = objBudgetItem.uuidID;
                        cmd.Parameters["@BUDGETDOCITEM_MONEY"].Value = objBudgetItem.MoneyInBudgetCurrency;
                        cmd.Parameters["@BUDGETDOCITEM_DOCMONEY"].Value = objBudgetItem.MoneyInBudgetDocCurrency;
                        cmd.ExecuteNonQuery();
                        iRet = (System.Int32)cmd.Parameters["@RETURN_VALUE"].Value;
                        if (iRet != 0)
                        {
                            switch (iRet)
                            {
                                case 1:
                                    strErr += String.Format("�� ������ ��������� �������� � �������� ���������������\n��: {0}", BUDGETDOC_GUID_ID);
                                    break;
                                case 2:
                                    strErr += String.Format("�� ������� ������ ������� � �������� ���������������\n��: {0}", objBudgetItem.uuidID);
                                    break;
                                default:
                                    strErr += ("������ ���������� ������ ������ ��������.");
                                    break;
                            }
                            break;
                        }
                    }

                    bRet = (iRet == 0);
                    iRetCode = iRet;
                }
                else
                {
                    strErr += ("�� ������� �������� ������ ������ ������� � ���������.");
                }

            }
            catch (System.Exception f)
            {
                bRet = false;
                strErr += ("�� ������� ��������� ������ ������ ��������. ����� ������: " + f.Message);
            }
			finally // ������� ���������� �������
            {
            }
            return bRet;
        }
        /// <summary>
        /// ��������� ������ �����������, ��������� � ��������� ����������
        /// </summary>
        /// <param name="cmd">SQL-�������</param>
        /// <param name="objProfile">�������</param>
        /// <returns>true - ������� ���������� ��������; false - ������</returns>
        private System.Boolean SaveBudgetDocItemList(System.Data.SqlClient.SqlCommand cmd,
            UniXP.Common.CProfile objProfile)
        {
            System.Boolean bRet = false;
            if (cmd == null)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show("����������� ���������� � ��.", "��������",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                return bRet;
            }
            if ((this.m_objBudgetItemList == null) || (this.m_objBudgetItemList.Count == 0))
            {
                DevExpress.XtraEditors.XtraMessageBox.Show("���������� ��������� �� ��������� ������ ��������.", "��������",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                return bRet;
            }
            if (this.IsResetBudgetItem == false)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show("��������� �������� �� �������� ������ �����������.", "��������",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                return bRet;
            }

            try
            {
                // ������ ������� ������ �����������, ��������� � ��������� ����������
                cmd.Parameters.Clear();
                cmd.CommandText = System.String.Format("[{0}].[dbo].[sp_DeleteBudgetDocItemDecode]", objProfile.GetOptionsDllDBName());
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ((System.Byte)(0)), ((System.Byte)(0)), "", System.Data.DataRowVersion.Current, null));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@BUDGETDOC_GUID_ID", System.Data.SqlDbType.UniqueIdentifier));

                cmd.Parameters["@BUDGETDOC_GUID_ID"].Value = this.m_uuidID;
                cmd.ExecuteNonQuery();
                System.Int32 iRet = (System.Int32)cmd.Parameters["@RETURN_VALUE"].Value;
                if (iRet == 0)
                {
                    // ������ ��������� ������ �����������, ��������� � ��������� ����������
                    foreach (CBudgetItem objBudgetItem in this.m_objBudgetItemList)
                    {
                        bRet = objBudgetItem.SaveBudgetItemDecodeListForBudgetDoc(cmd, objProfile, this.uuidID);
                        if (bRet == false) { break; }
                    }

                }
                else
                {
                    DevExpress.XtraEditors.XtraMessageBox.Show("�� ������� �������� ������ ����������� � ���������.\n�� ��������: " +
                        this.m_uuidID.ToString(), "��������",
                        System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                }
            }
            catch (System.Exception f)
            {
                bRet = false;
                DevExpress.XtraEditors.XtraMessageBox.Show(
                "�� ������� ��������� ������ ����������� ��� ���������.\n\n����� ������: " + f.Message, "��������",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
			finally // ������� ���������� �������
            {
            }
            return bRet;
        }
        /// <summary>
        /// ��������� ������ �������� � ���������� ���������
        /// </summary>
        /// <param name="cmd">SQL-�������</param>
        /// <param name="objProfile">�������</param>
        /// <returns>true - ������� ����������; false - ������</returns>
        private System.Boolean SaveAttachmentList(UniXP.Common.CProfile objProfile,
            System.Data.SqlClient.SqlCommand cmd)
        {
            System.Boolean bRet = false;
            try
            {
                if ((this.AttachList == null) || (this.AttachList.Count == 0))
                {
                    bRet = true;
                    return bRet;
                }
                if (cmd == null)
                {
                    DevExpress.XtraEditors.XtraMessageBox.Show("����������� ���������� � ��.", "��������",
                        System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    return bRet;
                }

                System.Boolean bOk = false;
                foreach (CBudgetDocAttach objBudgetDocAttach in this.AttachList)
                {
                    bOk = objBudgetDocAttach.UploadFileToDatabase(objProfile, cmd, this.uuidID);
                    if (bOk == false)
                    {
                        break;
                    }
                }

                bRet = bOk;
            }
            catch (System.Exception f)
            {
                bRet = false;
                DevExpress.XtraEditors.XtraMessageBox.Show(
                "�� ������� ��������� ������ ��������.\n\n����� ������: " + f.Message, "��������",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
			finally // ������� ���������� �������
            {
            }

            return bRet;
        }
        #endregion

        #region Update ( ��������� ��������� � ��������� ������� � �� )
        /// <summary>
        /// ��������� ��������� � ��
        /// </summary>
        /// <param name="objProfile">�������</param>
        /// <returns>true - ������� ����������; false - ������</returns>
        public override System.Boolean Update(UniXP.Common.CProfile objProfile)
        {
            return false;
        }

        /// <summary>
        /// ��������� ��������� � ��
        /// </summary>
        /// <param name="objProfile">�������</param>
        /// <param name="cmd">SQL-�������</param>
        /// <param name="objDocVariantCondn"></param>
        /// <returns>true - ������� ����������; false - ������</returns>
        private System.Boolean UpdateBudgetDoc(UniXP.Common.CProfile objProfile,
            System.Data.SqlClient.SqlCommand cmd, ViewDocVariantCondition objDocVariantCondn)
        {
            System.Boolean bRet = false;

            // �������� ������� ���������� ���������
            if (this.CheckValidProperties() == false) { return bRet; }
            if (cmd == null) { return bRet; }
            try
            {
                // ���������� � �� ��������, ����������� ������� �� �������� ������ � ��
                cmd.Parameters.Clear();
                cmd.CommandText = System.String.Format("[{0}].[dbo].[sp_EditBudgetDoc]", objProfile.GetOptionsDllDBName());
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ((System.Byte)(0)), ((System.Byte)(0)), "", System.Data.DataRowVersion.Current, null));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@GUID_ID", System.Data.SqlDbType.UniqueIdentifier, 4));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@BUDGETDOC_DATE", System.Data.SqlDbType.DateTime, 4));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@BUDGETDOC_MONEY", System.Data.SqlDbType.Money, 8));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@BUDGETDOC_ADD_PAYMONEY", System.Data.SqlDbType.Money, 8));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@BUDGETDOC_OBJECTIVE", System.Data.DbType.String));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@BUDGETDOC_PAYMENTDATE", System.Data.SqlDbType.DateTime, 4));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@BUDGETDOC_RECIPIENT", System.Data.DbType.String));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@BUDGETDOC_DOCBASIS", System.Data.DbType.String));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@BUDGETDOC_DESCRIPTION", System.Data.DbType.String));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@BUDGETDOC_MONEYAGREE", System.Data.SqlDbType.Money, 8));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@BUDGETDEP_GUID_ID", System.Data.SqlDbType.UniqueIdentifier, 4));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@PAYMENTTYPE_GUID_ID", System.Data.SqlDbType.UniqueIdentifier, 4));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@MEASURE_GUID_ID", System.Data.SqlDbType.UniqueIdentifier, 4));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@CURRENCY_GUID_ID", System.Data.SqlDbType.UniqueIdentifier, 4));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@COMPANY_GUID_ID", System.Data.SqlDbType.UniqueIdentifier, 4));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@BUDGETDOCTYPE_GUID_ID", System.Data.SqlDbType.UniqueIdentifier, 4));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@BUDGETDOC_ACTIVE", System.Data.SqlDbType.Bit, 1));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@BUDGETDOC_DEFICIT", System.Data.SqlDbType.Money, 8));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@BUDGETITEM_GUID_ID", System.Data.SqlDbType.UniqueIdentifier, 4));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@USER_ID", System.Data.SqlDbType.Int, 4));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ERROR_NUM", System.Data.SqlDbType.Int, 8, System.Data.ParameterDirection.Output, false, ((System.Byte)(0)), ((System.Byte)(0)), "", System.Data.DataRowVersion.Current, null));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ERROR_MES", System.Data.SqlDbType.NVarChar, 4000));
                cmd.Parameters["@ERROR_MES"].Direction = System.Data.ParameterDirection.Output;

                if (this.BudgetProject != null)
                {
                    cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@BUDGETPROJECT_GUID", System.Data.SqlDbType.UniqueIdentifier));
                    cmd.Parameters["@BUDGETPROJECT_GUID"].Value = this.BudgetProject.uuidID;
                }
                if (this.AccountPlan != null)
                {
                    cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ACCOUNTPLAN_GUID", System.Data.SqlDbType.UniqueIdentifier));
                    cmd.Parameters["@ACCOUNTPLAN_GUID"].Value = this.AccountPlan.uuidID;
                }
                if (this.BudgetDocCategory != null)
                {
                    cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@BUDGETDOCCATEGORY_GUID", System.Data.SqlDbType.UniqueIdentifier));
                    cmd.Parameters["@BUDGETDOCCATEGORY_GUID"].Value = this.BudgetDocCategory.uuidID;
                }
                
                cmd.Parameters["@GUID_ID"].Value = this.m_uuidID;
                cmd.Parameters["@BUDGETITEM_GUID_ID"].Value = this.m_objBudgetItem.uuidID;
                cmd.Parameters["@BUDGETDOC_DATE"].Value = this.m_dtDate;
                cmd.Parameters["@BUDGETDOC_MONEY"].Value = this.m_moneyMoney;
                if (objDocVariantCondn.DocEvent == null)
                {
                    cmd.Parameters["@BUDGETDOC_ADD_PAYMONEY"].Value = 0;
                }
                else
                {
                    cmd.Parameters["@BUDGETDOC_ADD_PAYMONEY"].Value = ((objDocVariantCondn.DocEvent.IsCanChanhgeMoney == true)) ? objDocVariantCondn.DocEvent.EventMoney : 0;
                }
                cmd.Parameters["@BUDGETDOC_OBJECTIVE"].Value = this.m_strObjective;
                cmd.Parameters["@BUDGETDOC_PAYMENTDATE"].Value = this.m_dtPaymentDate;
                cmd.Parameters["@BUDGETDOC_RECIPIENT"].Value = this.m_strRecipient;
                cmd.Parameters["@BUDGETDOC_DOCBASIS"].Value = this.m_strDocBasis;
                cmd.Parameters["@BUDGETDOC_DESCRIPTION"].Value = this.m_strDescription;
                cmd.Parameters["@BUDGETDOC_MONEYAGREE"].Value = this.m_moneyMoneyAgree;
                cmd.Parameters["@BUDGETDEP_GUID_ID"].Value = this.m_objBudgetDep.uuidID;
                cmd.Parameters["@PAYMENTTYPE_GUID_ID"].Value = this.m_objPaymentType.uuidID;
                cmd.Parameters["@MEASURE_GUID_ID"].Value = this.m_objMeasure.uuidID;
                cmd.Parameters["@CURRENCY_GUID_ID"].Value = this.m_objCurrency.uuidID;
                cmd.Parameters["@COMPANY_GUID_ID"].Value = this.m_objCompany.uuidID;
                if (objDocVariantCondn.NextDocState != null)
                {
                    cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@BUDGETDOCSTATE_GUID_ID", System.Data.SqlDbType.UniqueIdentifier, 4));
                    cmd.Parameters["@BUDGETDOCSTATE_GUID_ID"].Value = objDocVariantCondn.NextDocState.uuidID;
                }
                cmd.Parameters["@BUDGETDOCTYPE_GUID_ID"].Value = this.m_objDocType.uuidID;
                cmd.Parameters["@BUDGETDOC_ACTIVE"].Value = !(objDocVariantCondn.IsEndRoute);
                cmd.Parameters["@BUDGETDOC_DEFICIT"].Value = this.m_BudgetDocDeficit;
                cmd.Parameters["@USER_ID"].Value = objDocVariantCondn.CurrentUser.ulID;
                cmd.ExecuteNonQuery();
                System.Int32 iRet = (System.Int32)cmd.Parameters["@RETURN_VALUE"].Value;
                if (iRet == 0)
                {
                    // ��������� ������ ������ �������
                    if (objDocVariantCondn.IsEndRoute == true)
                    {
                        // ���� ��� ��������� �������� ��� �������, �� ����� ������������ � �����������?!
                        bRet = true;
                    }
                    else
                    {
                        bRet = SaveBudgetItemList(cmd, objProfile);
                    }
                    if ((bRet == true) && (this.m_bResetBudgetItem == true))
                    {
                        // ��������� ������ �����������
                        bRet = SaveBudgetDocItemList(cmd, objProfile);
                    }
                    if (bRet == true)
                    {
                        // ��������� ������� � ��
                        bRet = this.m_objRoute.Update(objProfile, cmd);
                    }
                }
                else
                {
                    DevExpress.XtraEditors.XtraMessageBox.Show("������ �������� ���������� ���������.\n����� ������: " +
                            (System.String)cmd.Parameters["@ERROR_MES"].Value, "��������",
                        System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                }
            }
            catch (System.Exception f)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(
                    "������ �������� ���������� ���������.\n����� ������: " +
                    (System.String)cmd.Parameters["@ERROR_MES"].Value + "\n\n" + f.Message, "��������",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
			finally // ������� ���������� �������
            {
            }
            return bRet;
        }

        #endregion

        #region �������� �� ��������

        /// <summary>
        /// ��������������� ��������� ��� ������ �������� ����������� ������  � �������� �� ��������
        /// </summary>
        public class ViewDocVariantCondition
        {
            /// <summary>
            /// ������� ������������ �����
            /// </summary>
            public CDynamicRight DynamicRight;
            /// <summary>
            /// ������� ��������� ������
            /// </summary>
            public CBudgetDocState DocState;
            /// <summary>
            /// ���������, � ������� �������� �����f
            /// </summary>
            public CBudgetDocState NextDocState;
            /// <summary>
            /// ��������
            /// </summary>
            public CBudgetDocEvent DocEvent;
            /// <summary>
            /// ������� ��������������� �����
            /// </summary>
            public System.Boolean AdvancedRight;
            /// <summary>
            /// ��������� ������
            /// </summary>
            public CUser Owner;
            /// <summary>
            /// ������� ������������
            /// </summary>
            public CUser CurrentUser;
            /// <summary>
            /// ��������� �������������
            /// </summary>
            public CBudgetDep BudgetDep;
            /// <summary>
            /// ������� ���������� ������
            /// </summary>
            public System.Boolean IsActive;
            /// <summary>
            /// ��� ���������� ���������
            /// </summary>
            public CBudgetDocType DocType;
            /// <summary>
            /// ������� ��������� ��������
            /// </summary>
            public System.Boolean IsEndRoute;
            /// <summary>
            /// �� ��������� ���������� ���������
            /// </summary>
            public System.Guid BudgetDocSrcID;
            public ViewDocVariantCondition()
            {
                DynamicRight = null;
                DocState = null;
                NextDocState = null;
                DocEvent = null;
                AdvancedRight = false;
                Owner = null;
                CurrentUser = null;
                BudgetDep = null;
                IsActive = false;
                DocType = null;
                IsEndRoute = false;
            }
        }

        /// <summary>
        /// ���������� �������� �� ��������
        /// </summary>
        /// <param name="objProfile">�������</param>
        /// <param name="objDocVariantCondn"></param>
        /// <returns>true - ������� ����������; false - ������</returns>
        public System.Boolean bSendBudgetDoc(ViewDocVariantCondition objDocVariantCondn,
            UniXP.Common.CProfile objProfile)
        {
            System.Boolean bRet = false;
            try
            {
                // ��������� ����������, ����� �������� ������ �����������
                // ��� ����� ������ ���� ���������� ��� ���������, �������� � ������������ �����
                CAccountTrnEvent objAccountTrnEvent = null;
                if ((objDocVariantCondn.DocType != null) && (objDocVariantCondn.DocEvent != null)
                    && (objDocVariantCondn.DynamicRight != null))
                {
                    objAccountTrnEvent = CAccountTrnEvent.GetAccountTrnEvent(objProfile,
                        objDocVariantCondn.DocEvent, objDocVariantCondn.DocType,
                        objDocVariantCondn.DynamicRight);
                }
                if (objAccountTrnEvent == null)
                {
                    objAccountTrnEvent = new CAccountTrnEvent();
                    //DevExpress.XtraEditors.XtraMessageBox.Show( 
                    //"�� ������� ���������� ��� �������� � �������� ������ ��������.", "��������",
                    //System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning );
                    //return bRet;
                }

                // ������ ����� ��������� ��������� ��������
                bRet = SaveBudgetDoc(objProfile, objDocVariantCondn, objAccountTrnEvent);
            }
            catch (System.Exception f)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(
                "�� ������� ��������� �������� �� ��������.\n����� ������: " + f.Message, "��������",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
			finally // ������� ���������� �������
            {
            }

            return bRet;
        }
        /// <summary>
        /// ��������� �������� � ��
        /// </summary>
        /// <param name="objProfile">�������</param>
        /// <param name="objDocVariantCondn"></param>
        /// <param name="objAccountTrnEvent">������ �������� ��� �������� ������</param>
        /// <returns>true - ������� ����������; false - ������</returns>
        private System.Boolean SaveBudgetDoc(UniXP.Common.CProfile objProfile,
            ViewDocVariantCondition objDocVariantCondn, CAccountTrnEvent objAccountTrnEvent)
        {
            System.Boolean bRet = false;

            System.Data.SqlClient.SqlConnection DBConnection = objProfile.GetDBSource();
            if (DBConnection == null) { return bRet; }
            System.Data.SqlClient.SqlTransaction DBTransaction = DBConnection.BeginTransaction();
            System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
            try
            {
                cmd.Connection = DBConnection;
                cmd.Transaction = DBTransaction;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                // ������ ��������� ��������� ��������
                if (objDocVariantCondn.DocState == null)
                {
                    // ����� ������
                    bRet = AddBudgetDoc(objProfile, cmd, objDocVariantCondn);
                }
                else
                {
                    // ����� ����������� ������
                    bRet = UpdateBudgetDoc(objProfile, cmd, objDocVariantCondn);
                }
                if (bRet)
                {
                    // ������ �����������, �������� ������ ������
                    // 2009.04.20
                    // ��������� ���������... ���� ��� ���������� ������ �����, �� ����� ����� ���������� ��� � �������
                    // ������ � ��������� ����� ��������� ������ ������ ������ �, ��� ���������,
                    // ���������� �� ������ ������, ������� ���� ������� � ������ ����� ����� �� PaymentDate
                    System.Boolean bUsePaymentDate = ( ( objProfile.GetClientsRight().GetState( ERP_Budget.Global.Consts.strDRCashier ) == true ) || 
                        ( objProfile.GetClientsRight().GetState( ERP_Budget.Global.Consts.strDRAccountant ) == true ) );
                    if (objAccountTrnEvent.AccountTrnTypeList.Count > 0)
                    {
                        foreach (CAccountTrnType objAccountTrnType in objAccountTrnEvent.AccountTrnTypeList)
                        {
                            switch (objAccountTrnType.MetodID)
                            {
                                case (int)enumAccTrnEvent.ReservMoney:
                                    {
                                        if (bUsePaymentDate == true)
                                        {
                                            bRet = CAccountTransn.ResrveMoney(this, this.PaymentDate, objProfile, cmd, objDocVariantCondn.CurrentUser.ulID);
                                        }
                                        else
                                        {
                                            bRet = CAccountTransn.ResrveMoney(this, System.DateTime.Now, objProfile, cmd, objDocVariantCondn.CurrentUser.ulID);
                                        }
                                        break;
                                    }
                                case (int)enumAccTrnEvent.DeReservMoney:
                                    {
                                        bRet = CAccountTransn.DeResrveMoney(this, System.DateTime.Now, objProfile, cmd, objDocVariantCondn.CurrentUser.ulID);
                                        break;
                                    }
                                case (int)enumAccTrnEvent.AcceptNote:
                                    {
                                        bRet = CAccountTransn.AcceptNote(this, System.DateTime.Now, objProfile, cmd, objDocVariantCondn.CurrentUser.ulID);
                                        break;
                                    }
                                case (int)enumAccTrnEvent.DeAcceptNote:
                                    {
                                        bRet = CAccountTransn.DeAcceptNote(this, System.DateTime.Now, objProfile, cmd, objDocVariantCondn.CurrentUser.ulID);
                                        break;
                                    }
                                case (int)enumAccTrnEvent.PayMoney:
                                    {
                                        if (bUsePaymentDate == true)
                                        {
                                            bRet = CAccountTransn.PayMoney(this, this.PaymentDate, objProfile, cmd, objDocVariantCondn.CurrentUser.ulID, 
                                                objDocVariantCondn.DocEvent.EventMoney,
                                                objDocVariantCondn.DocEvent.EventFactMoney,
                                                ((objDocVariantCondn.DocEvent.EventFactCurrency != null ) ? objDocVariantCondn.DocEvent.EventFactCurrency.uuidID : System.Guid.Empty ) );
                                        }
                                        else
                                        {
                                            bRet = CAccountTransn.PayMoney(this, System.DateTime.Now, objProfile, cmd, objDocVariantCondn.CurrentUser.ulID, 
                                                objDocVariantCondn.DocEvent.EventMoney,
                                                objDocVariantCondn.DocEvent.EventFactMoney,
                                                ((objDocVariantCondn.DocEvent.EventFactCurrency != null) ? objDocVariantCondn.DocEvent.EventFactCurrency.uuidID : System.Guid.Empty));
                                        }
                                        break;
                                    }
                                case (int)enumAccTrnEvent.DePayMoney:
                                    {
                                        bRet = CAccountTransn.DePayDoc(objDocVariantCondn.BudgetDocSrcID, this, this.PaymentDate, objProfile, cmd, objDocVariantCondn.CurrentUser.ulID);
                                        break;
                                    }
                                default:
                                    {
                                        bRet = false;
                                        break;
                                    }
                            }
                            if (bRet == false) { break; }
                        }
                    }
                    if (bRet == true)
                    {
                        // ������������ ����������
                        DBTransaction.Commit();
                    }
                    else
                    {
                        // ���������� ����������
                        DBTransaction.Rollback();
                    }
                }
                else
                {
                    // ���������� ����������
                    DBTransaction.Rollback();
                }

                cmd.Dispose();
            }
            catch (System.Exception f)
            {
                DBTransaction.Rollback();
                DevExpress.XtraEditors.XtraMessageBox.Show(
                    "������ ���������� ���������� ���������.\n����� ������: " + f.Message, "��������",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
			finally // ������� ���������� �������
            {
                DBConnection.Close();
            }
            return bRet;
        }

        /// <summary>
        /// ������������� ������� "������" ���������� ���������
        /// </summary>
        /// <param name="isOpen">�������� �������� "������"</param>
        /// <param name="objProfile">�������</param>
        /// <returns>true - ������� ����������; false - ������</returns>
        public System.Boolean SetOpenPropertie(System.Boolean isOpen, UniXP.Common.CProfile objProfile)
        {
            System.Boolean bRet = false;

            System.Data.SqlClient.SqlConnection DBConnection = objProfile.GetDBSource();
            if (DBConnection == null) { return bRet; }
            System.Data.SqlClient.SqlTransaction DBTransaction = DBConnection.BeginTransaction();
            System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
            try
            {
                cmd.Connection = DBConnection;
                cmd.Transaction = DBTransaction;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = System.String.Format("[{0}].[dbo].[sp_SetBudgetDocOpen]", objProfile.GetOptionsDllDBName());
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ((System.Byte)(0)), ((System.Byte)(0)), "", System.Data.DataRowVersion.Current, null));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ulUniXPID", System.Data.SqlDbType.Int));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ISOPEN", System.Data.SqlDbType.Bit));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@BUDGETDOC_GUID_ID", System.Data.SqlDbType.UniqueIdentifier));

                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@UserLastName", System.Data.SqlDbType.NVarChar, 50));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@UserFirstName", System.Data.SqlDbType.NVarChar, 50));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@UserOpenDate", System.Data.SqlDbType.DateTime));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ERROR_NUMBER", System.Data.SqlDbType.Int, 8, System.Data.ParameterDirection.Output, false, ((System.Byte)(0)), ((System.Byte)(0)), "", System.Data.DataRowVersion.Current, null));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ERROR_MESSAGE", System.Data.SqlDbType.NVarChar, 4000));
                cmd.Parameters["@UserLastName"].Direction = System.Data.ParameterDirection.Output;
                cmd.Parameters["@UserFirstName"].Direction = System.Data.ParameterDirection.Output;
                cmd.Parameters["@UserOpenDate"].Direction = System.Data.ParameterDirection.Output;
                cmd.Parameters["@ERROR_MESSAGE"].Direction = System.Data.ParameterDirection.Output;

                cmd.Parameters["@ulUniXPID"].Value = objProfile.m_nSQLUserID;
                cmd.Parameters["@ISOPEN"].Value = isOpen;
                cmd.Parameters["@BUDGETDOC_GUID_ID"].Value = this.m_uuidID;

                cmd.ExecuteNonQuery();
                System.Int32 iRet = (System.Int32)cmd.Parameters["@RETURN_VALUE"].Value;
                if (iRet == 0)
                {
                    // ������������ ����������
                    DBTransaction.Commit();
                }
                else
                {
                    // ���������� ����������
                    DBTransaction.Rollback();
                    switch (iRet)
                    {
                        case 1:
                            DevExpress.XtraEditors.XtraMessageBox.Show("�������� � ��������� ��������������� �� ������\n�� : " + this.uuidID.ToString(), "��������",
                                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);
                            break;
                        case 2:
                            DevExpress.XtraEditors.XtraMessageBox.Show("�� ������ ������������� ������������.", "��������",
                                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                            break;
                        case 3:
                            DevExpress.XtraEditors.XtraMessageBox.Show("������������ � ��������� ��������������� �� ������\n�� : " + objProfile.m_nSQLUserID.ToString(), "��������",
                                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                            break;
                        case 4:
                            DevExpress.XtraEditors.XtraMessageBox.Show("�������� � ��������� ��������������� ������ ������ �������������\n�� ���������: " + this.uuidID.ToString() +
                                "\n\n�������� ������: " + (System.String)cmd.Parameters["@UserLastName"].Value +
                                 " " + (System.String)cmd.Parameters["@UserFirstName"].Value +
                                 " � " + ((System.DateTime)cmd.Parameters["@UserOpenDate"].Value).ToShortTimeString(), "��������",
                                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                            break;
                        default:
                            DevExpress.XtraEditors.XtraMessageBox.Show("������ ���������� �������� � ���� ������.\n\n" +
                                (System.String)cmd.Parameters["@ERROR_MESSAGE"].Value, "��������",
                                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                            break;
                    }
                }
                bRet = (iRet == 0);

                cmd.Dispose();
            }
            catch (System.Exception f)
            {
                DBTransaction.Rollback();
                DevExpress.XtraEditors.XtraMessageBox.Show(
                    "������ ��������� �������� \"������\" ���������� ���������.\n����� SQL-������: " +
                    (System.String)cmd.Parameters["@ERROR_MESSAGE"].Value + "\n\n����� ������: " + f.Message, "��������",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
			finally // ������� ���������� �������
            {
                DBConnection.Close();
            }
            return bRet;
        }
        /// <summary>
        /// ���������, ������ �� �������� � ������ ������
        /// </summary>
        /// <param name="objProfile">�������</param>
        /// <returns>���� ������ ���������� ������ � ������ ������������ � ����� ��������</returns>
        public System.String GetOpenPropertieState(UniXP.Common.CProfile objProfile)
        {
            System.String strRet = "";

            System.Data.SqlClient.SqlConnection DBConnection = objProfile.GetDBSource();
            if (DBConnection == null) { return strRet; }
            System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
            try
            {
                cmd.Connection = DBConnection;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = System.String.Format("[{0}].[dbo].[sp_CheckBudgetDocOpenState]", objProfile.GetOptionsDllDBName());
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ((System.Byte)(0)), ((System.Byte)(0)), "", System.Data.DataRowVersion.Current, null));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@BUDGETDOC_GUID_ID", System.Data.SqlDbType.UniqueIdentifier));

                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@IsOpen", System.Data.SqlDbType.Bit));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@UserLastName", System.Data.SqlDbType.NVarChar, 50));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@UserFirstName", System.Data.SqlDbType.NVarChar, 50));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@UserOpenDate", System.Data.SqlDbType.DateTime));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ERROR_NUMBER", System.Data.SqlDbType.Int, 8, System.Data.ParameterDirection.Output, false, ((System.Byte)(0)), ((System.Byte)(0)), "", System.Data.DataRowVersion.Current, null));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ERROR_MESSAGE", System.Data.SqlDbType.NVarChar, 4000));
                cmd.Parameters["@IsOpen"].Direction = System.Data.ParameterDirection.Output;
                cmd.Parameters["@UserLastName"].Direction = System.Data.ParameterDirection.Output;
                cmd.Parameters["@UserFirstName"].Direction = System.Data.ParameterDirection.Output;
                cmd.Parameters["@UserOpenDate"].Direction = System.Data.ParameterDirection.Output;
                cmd.Parameters["@ERROR_MESSAGE"].Direction = System.Data.ParameterDirection.Output;

                cmd.Parameters["@BUDGETDOC_GUID_ID"].Value = this.m_uuidID;

                cmd.ExecuteNonQuery();
                System.Int32 iRet = (System.Int32)cmd.Parameters["@RETURN_VALUE"].Value;
                if (iRet == 0)
                {
                    if (((System.Boolean)cmd.Parameters["@IsOpen"].Value) == true)
                    {
                        strRet = (System.String)cmd.Parameters["@UserLastName"].Value + " " +
                                 (System.String)cmd.Parameters["@UserFirstName"].Value + " � " +
                                 ((System.DateTime)cmd.Parameters["@UserOpenDate"].Value).ToShortTimeString();
                    }
                }
                else
                {
                    DevExpress.XtraEditors.XtraMessageBox.Show("������ ���������� ������� � ���� ������.\n" +
                        (System.String)cmd.Parameters["@ERROR_MESSAGE"].Value, "��������",
                        System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                }

                cmd.Dispose();
            }
            catch (System.Exception f)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(
                    "������ �������� ��������� ���������� ���������.\n����� SQL-������: " +
                    (System.String)cmd.Parameters["@ERROR_MESSAGE"].Value + "\n\n����� ������: " + f.Message, "��������",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
			finally // ������� ���������� �������
            {
                DBConnection.Close();
            }
            return strRet;
        }

        #endregion

        #region ������ ��������� ���������� ���������
        /// <summary>
        /// ���������� ������ ��������� ���������� ���������
        /// </summary>
        /// <param name="objProfile">�������</param>
        /// <returns>������ ��������� ���������� ���������</returns>
        public System.Collections.Generic.List<structBudgetDocStateHistory> GetBudgetDocHistory(UniXP.Common.CProfile objProfile)
        {
            List<structBudgetDocStateHistory> objList = new List<structBudgetDocStateHistory>();

            System.Data.SqlClient.SqlConnection DBConnection = objProfile.GetDBSource();
            if (DBConnection == null)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show("����������� ���������� � ��.", "��������",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                return objList;
            }

            try
            {
                // ���������� � �� ��������, ����������� ������� �� ������� ������
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
                cmd.Connection = DBConnection;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = System.String.Format("[{0}].[dbo].[sp_GetBudgetDocHistory]", objProfile.GetOptionsDllDBName());
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ((System.Byte)(0)), ((System.Byte)(0)), "", System.Data.DataRowVersion.Current, null));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@BUDGETDOC_GUID_ID", System.Data.SqlDbType.UniqueIdentifier));
                cmd.Parameters["@BUDGETDOC_GUID_ID"].Value = this.m_uuidID;
                System.Data.SqlClient.SqlDataReader rs = cmd.ExecuteReader();
                if (rs.HasRows)
                {
                    while (rs.Read())
                    {
                        structBudgetDocStateHistory objDocStateHistory = new structBudgetDocStateHistory();
                        // ����
                        objDocStateHistory.HistoryDate = (System.DateTime)rs["BUDGETDOCHISTORY_DATE"];
                        // ���������
                        objDocStateHistory.BudgetDocState =
                            new CBudgetDocState((System.Guid)rs["BUDGETDOCSTATE_GUID_ID"],
                            (System.String)rs["BUDGETDOCSTATE_NAME"]);
                        // ������������
                        objDocStateHistory.UserHistory = new CUser((System.Int32)rs["ulUserID"],
                            (System.Int32)rs["UniXPUserID"], (System.String)rs["strLastName"],
                            (System.String)rs["strFirstName"]);

                        objList.Add(objDocStateHistory);
                    }
                }
                rs.Close();
                rs = null;
                cmd = null;
            }
            catch (System.Exception e)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(
                "�� ������� �������� ������ ��������� ���������� ���������.\n\n����� ������: " + e.Message, "��������",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
            finally // ������� ���������� �������
            {
                DBConnection.Close();
            }

            return objList;
        }
        /// <summary>
        /// ���������� ������ ��������� ���������� ���������
        /// </summary>
        /// <param name="uuidBudgetDocID">�� ���������� ���������</param>
        /// <param name="cmd">������� SQL</param>
        /// <param name="objProfile">�������</param>
        /// <returns>������ ��������� ���������� ���������</returns>
        public static System.Collections.Generic.List<structBudgetDocStateHistory> GetBudgetDocHistory(System.Guid uuidBudgetDocID,
            System.Data.SqlClient.SqlCommand cmd, UniXP.Common.CProfile objProfile)
        {
            List<structBudgetDocStateHistory> objList = new List<structBudgetDocStateHistory>();

            if (cmd == null)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show("����������� ���������� � ��.", "��������",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                return objList;
            }

            try
            {
                // ���������� � �� ��������, ����������� ������� �� ������� ������
                cmd.Parameters.Clear();
                cmd.CommandText = System.String.Format("[{0}].[dbo].[sp_GetBudgetDocHistory]", objProfile.GetOptionsDllDBName());
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ((System.Byte)(0)), ((System.Byte)(0)), "", System.Data.DataRowVersion.Current, null));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@BUDGETDOC_GUID_ID", System.Data.SqlDbType.UniqueIdentifier));
                cmd.Parameters["@BUDGETDOC_GUID_ID"].Value = uuidBudgetDocID;
                System.Data.SqlClient.SqlDataReader rs = cmd.ExecuteReader();
                if (rs.HasRows)
                {
                    while (rs.Read())
                    {
                        structBudgetDocStateHistory objDocStateHistory = new structBudgetDocStateHistory();
                        // ����
                        objDocStateHistory.HistoryDate = (System.DateTime)rs["BUDGETDOCHISTORY_DATE"];
                        // ���������
                        objDocStateHistory.BudgetDocState =
                            new CBudgetDocState((System.Guid)rs["BUDGETDOCSTATE_GUID_ID"],
                            (System.String)rs["BUDGETDOCSTATE_NAME"]);
                        // ������������
                        objDocStateHistory.UserHistory = new CUser((System.Int32)rs["ulUserID"],
                            (System.Int32)rs["UniXPUserID"], (System.String)rs["strLastName"],
                            (System.String)rs["strFirstName"]);

                        objList.Add(objDocStateHistory);
                    }
                }
                rs.Close();
                rs = null;
            }
            catch (System.Exception e)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(
                "�� ������� �������� ������ ��������� ���������� ���������.\n\n����� ������: " + e.Message, "��������",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
            finally // ������� ���������� �������
            {
            }

            return objList;
        }
        #endregion

        #region �������� ���� � ���� ������
        public System.Boolean ChangePaymentDate(UniXP.Common.CProfile objProfile, System.Int32 iUserID, 
            System.DateTime newBudgetDocDate, System.DateTime newBudgetDocPaymentDate )
        {
            System.Boolean bRet = false;

            // ���������� ������������� �� ������ ���� ������
            if (this.m_uuidID == System.Guid.Empty)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show("������������ �������� ����������� �������������� �������\n�� : " +
                    this.m_uuidID.ToString(), "��������",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                return bRet;
            }

            System.Data.SqlClient.SqlConnection DBConnection = objProfile.GetDBSource();
            if (DBConnection == null)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show("����������� ���������� � ��.", "��������",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                return bRet;
            }

            System.Data.SqlClient.SqlTransaction DBTransaction = DBConnection.BeginTransaction();

            try
            {
                // ���������� � �� ��������, ����������� ������� �� �������� ������
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
                cmd.Connection = DBConnection;
                cmd.Transaction = DBTransaction;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = System.String.Format("[{0}].[dbo].[sp_ChangeBudgetDocDateAfterPayment]", objProfile.GetOptionsDllDBName());
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ((System.Byte)(0)), ((System.Byte)(0)), "", System.Data.DataRowVersion.Current, null));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@BUDGETDOC_GUID_ID", System.Data.SqlDbType.UniqueIdentifier));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@USERS_ID", System.Data.SqlDbType.Int, 4));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@NewBudgetDocDate", System.Data.SqlDbType.DateTime, 4));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@NewBudgetDocPaymentDate", System.Data.SqlDbType.DateTime, 4));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ERROR_NUM", System.Data.SqlDbType.Int, 8, System.Data.ParameterDirection.Output, false, ((System.Byte)(0)), ((System.Byte)(0)), "", System.Data.DataRowVersion.Current, null));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ERROR_MES", System.Data.SqlDbType.NVarChar, 4000));
                cmd.Parameters["@ERROR_MES"].Direction = System.Data.ParameterDirection.Output;
                cmd.Parameters["@BUDGETDOC_GUID_ID"].Value = this.m_uuidID;
                cmd.Parameters["@USERS_ID"].Value = iUserID;
                cmd.Parameters["@NewBudgetDocDate"].Value = newBudgetDocDate;
                cmd.Parameters["@NewBudgetDocPaymentDate"].Value = newBudgetDocPaymentDate;

                cmd.ExecuteNonQuery();
                System.Int32 iRet = (System.Int32)cmd.Parameters["@RETURN_VALUE"].Value;
                if (iRet == 0)
                {
                    // ������������ ����������
                    DBTransaction.Commit();
                    bRet = true;
                }
                else
                {
                    // ���������� ����������
                    DBTransaction.Rollback();
                    DevExpress.XtraEditors.XtraMessageBox.Show("������ ��������� ���� ���������.\n\n" +
                        (System.String)cmd.Parameters["@ERROR_MES"].Value, "������",
                       System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);
                }

                cmd = null;
            }
            catch (System.Exception f)
            {
                DBTransaction.Rollback();
                DevExpress.XtraEditors.XtraMessageBox.Show(
                "�� ������� �������� ���� ���������� ���������.\n�� ��������� : " + this.uuidID.ToString() + "\n\n����� ������: " + f.Message, "��������",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
			finally // ������� ���������� �������
            {
                DBConnection.Close();
            }

            return bRet;
        }
        #endregion

        #region ������������� ���������� ������
        /// <summary>
        /// ������������� ���������� ������
        /// </summary>
        /// <param name="objProfile">�������</param>
        /// <param name="iUserID">�� ������������</param>
        /// <returns>true - ������� ���������� ��������; false - ������</returns>
        public System.Boolean BackMoneyAfterPaid(UniXP.Common.CProfile objProfile, System.Int32 iUserID)
        {
            System.Boolean bRet = false;

            // ���������� ������������� �� ������ ���� ������
            if (this.m_uuidID == System.Guid.Empty)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show("������������ �������� ����������� �������������� �������\n�� : " +
                    this.m_uuidID.ToString(), "��������",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                return bRet;
            }

            System.Data.SqlClient.SqlConnection DBConnection = objProfile.GetDBSource();
            if (DBConnection == null)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show("����������� ���������� � ��.", "��������",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                return bRet;
            }

            System.Data.SqlClient.SqlTransaction DBTransaction = DBConnection.BeginTransaction();

            try
            {
                // ���������� � �� ��������, ����������� ������� �� �������� ������
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
                cmd.Connection = DBConnection;
                cmd.Transaction = DBTransaction;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = System.String.Format("[{0}].[dbo].[sp_DePayBudgetDocDateAfterPayment]", objProfile.GetOptionsDllDBName());
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ((System.Byte)(0)), ((System.Byte)(0)), "", System.Data.DataRowVersion.Current, null));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@BUDGETDOC_GUID_ID", System.Data.SqlDbType.UniqueIdentifier));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@USERS_ID", System.Data.SqlDbType.Int, 4));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ERROR_NUM", System.Data.SqlDbType.Int, 8, System.Data.ParameterDirection.Output, false, ((System.Byte)(0)), ((System.Byte)(0)), "", System.Data.DataRowVersion.Current, null));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ERROR_MES", System.Data.SqlDbType.NVarChar, 4000));
                cmd.Parameters["@ERROR_MES"].Direction = System.Data.ParameterDirection.Output;
                cmd.Parameters["@BUDGETDOC_GUID_ID"].Value = this.m_uuidID;
                cmd.Parameters["@USERS_ID"].Value = iUserID;

                cmd.ExecuteNonQuery();
                System.Int32 iRet = (System.Int32)cmd.Parameters["@RETURN_VALUE"].Value;
                if (iRet == 0)
                {
                    // ������������ ����������
                    DBTransaction.Commit();
                    bRet = true;
                }
                else
                {
                    // ���������� ����������
                    DBTransaction.Rollback();
                    DevExpress.XtraEditors.XtraMessageBox.Show("������ ������������� ���������� ���������.\n\n" +
                        (System.String)cmd.Parameters["@ERROR_MES"].Value, "������",
                       System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);
                }

                cmd = null;
            }
            catch (System.Exception f)
            {
                DBTransaction.Rollback();
                DevExpress.XtraEditors.XtraMessageBox.Show(
                "�� ������� ������������ ��������� ��������.\n�� ��������� : " + this.uuidID.ToString() + "\n\n����� ������: " + f.Message, "��������",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
			finally // ������� ���������� �������
            {
                DBConnection.Close();
            }

            return bRet;
        }

        #endregion

        #region �������������� ���������� ������
        /// <summary>
        /// ����������� �������� "��������" � ������
        /// </summary>
        /// <param name="BudgetDoc_Guid">�� ������</param>
        /// <param name="Company_Guid">�� ��������</param>
        /// <param name="objProfile">�������</param>
        /// <param name="strErr">���� ������</param>
        /// <returns>true - ������� ���������� ��������; false - ������</returns>
        public static System.Boolean ChangeBudgetDocCompany(System.Guid BudgetDoc_Guid, System.Guid Company_Guid, 
           UniXP.Common.CProfile objProfile, ref System.String strErr)
        {
            System.Boolean bRet = false;

            System.Data.SqlClient.SqlConnection DBConnection = null;
            System.Data.SqlClient.SqlCommand cmd = null;
            System.Data.SqlClient.SqlTransaction DBTransaction = null;

            try
            {
                DBConnection = objProfile.GetDBSource();
                if (DBConnection == null)
                {
                    strErr += ("�� ������� �������� ���������� � ����� ������.");
                    return bRet;
                }
                DBTransaction = DBConnection.BeginTransaction();
                cmd = new System.Data.SqlClient.SqlCommand();
                cmd.Connection = DBConnection;
                cmd.Transaction = DBTransaction;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = System.String.Format("[{0}].[dbo].[usp_ChangeBudgetDocCompany]", objProfile.GetOptionsDllDBName());
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ((System.Byte)(0)), ((System.Byte)(0)), "", System.Data.DataRowVersion.Current, null));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@BudgetDoc_Guid", System.Data.SqlDbType.UniqueIdentifier));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Company_Guid", System.Data.SqlDbType.UniqueIdentifier));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ERROR_NUM", System.Data.SqlDbType.Int, 8, System.Data.ParameterDirection.Output, false, ((System.Byte)(0)), ((System.Byte)(0)), "", System.Data.DataRowVersion.Current, null));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ERROR_MES", System.Data.SqlDbType.NVarChar, 4000));
                cmd.Parameters["@ERROR_MES"].Direction = System.Data.ParameterDirection.Output;
                cmd.Parameters["@BudgetDoc_Guid"].Value = BudgetDoc_Guid;
                cmd.Parameters["@Company_Guid"].Value = Company_Guid;
                cmd.ExecuteNonQuery();
                System.Int32 iRes = (System.Int32)cmd.Parameters["@RETURN_VALUE"].Value;
                if (iRes == 0)
                {
                    // ������������ ����������
                    DBTransaction.Commit();
                }
                else
                {
                    DBTransaction.Rollback();
                    strErr += ((cmd.Parameters["@ERROR_MES"].Value == System.DBNull.Value) ? "" : (System.String)cmd.Parameters["@ERROR_MES"].Value);
                }

                cmd.Dispose();
                bRet = (iRes == 0);
            }
            catch (System.Exception f)
            {
                DBTransaction.Rollback();

                strErr += ("�� ������� ������ ��������� � �������� \"��������\". ����� ������: " + f.Message);
            }
            finally
            {
                DBConnection.Close();
            }
            return bRet;
        }

        /// <summary>
        /// ����������� �������� "����� ������" � ������
        /// </summary>
        /// <param name="BudgetDoc_Guid">�� ������</param>
        /// <param name="PaymentType_Guid">�� ����� ������</param>
        /// <param name="objProfile">�������</param>
        /// <param name="strErr">���� ������</param>
        /// <returns>true - ������� ���������� ��������; false - ������</returns>
        public static System.Boolean ChangeBudgetDocPaymentType(System.Guid BudgetDoc_Guid, System.Guid PaymentType_Guid,
           UniXP.Common.CProfile objProfile, ref System.String strErr)
        {
            System.Boolean bRet = false;

            System.Data.SqlClient.SqlConnection DBConnection = null;
            System.Data.SqlClient.SqlCommand cmd = null;
            System.Data.SqlClient.SqlTransaction DBTransaction = null;

            try
            {
                DBConnection = objProfile.GetDBSource();
                if (DBConnection == null)
                {
                    strErr += ("�� ������� �������� ���������� � ����� ������.");
                    return bRet;
                }
                DBTransaction = DBConnection.BeginTransaction();
                cmd = new System.Data.SqlClient.SqlCommand();
                cmd.Connection = DBConnection;
                cmd.Transaction = DBTransaction;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = System.String.Format("[{0}].[dbo].[usp_ChangeBudgetDocPaymentType]", objProfile.GetOptionsDllDBName());
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ((System.Byte)(0)), ((System.Byte)(0)), "", System.Data.DataRowVersion.Current, null));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@BudgetDoc_Guid", System.Data.SqlDbType.UniqueIdentifier));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@PaymentType_Guid", System.Data.SqlDbType.UniqueIdentifier));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ERROR_NUM", System.Data.SqlDbType.Int, 8, System.Data.ParameterDirection.Output, false, ((System.Byte)(0)), ((System.Byte)(0)), "", System.Data.DataRowVersion.Current, null));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ERROR_MES", System.Data.SqlDbType.NVarChar, 4000));
                cmd.Parameters["@ERROR_MES"].Direction = System.Data.ParameterDirection.Output;
                cmd.Parameters["@BudgetDoc_Guid"].Value = BudgetDoc_Guid;
                cmd.Parameters["@PaymentType_Guid"].Value = PaymentType_Guid;
                cmd.ExecuteNonQuery();
                System.Int32 iRes = (System.Int32)cmd.Parameters["@RETURN_VALUE"].Value;
                if (iRes == 0)
                {
                    // ������������ ����������
                    DBTransaction.Commit();
                }
                else
                {
                    DBTransaction.Rollback();
                    strErr += ((cmd.Parameters["@ERROR_MES"].Value == System.DBNull.Value) ? "" : (System.String)cmd.Parameters["@ERROR_MES"].Value);
                }

                cmd.Dispose();
                bRet = (iRes == 0);
            }
            catch (System.Exception f)
            {
                DBTransaction.Rollback();

                strErr += ("�� ������� ������ ��������� � �������� \"����� ������\". ����� ������: " + f.Message);
            }
            finally
            {
                DBConnection.Close();
            }
            return bRet;
        }
        #endregion


    }

    public class CManagementBudgetDoc : CBudgetDoc
    {
        #region ����������, ��������, ���������
        /// <summary>
        /// ����� ���������
        /// </summary>
        private double m_moneyAccTrnSum;
        /// <summary>
        /// ����� ���������
        /// </summary>
        public double AccTrnSum
        {
            get { return m_moneyAccTrnSum; }
            set { m_moneyAccTrnSum = value; }
        }
        /// <summary>
        /// ������ ��������� ���������� ���������
        /// </summary>
        private List<ERP_Budget.Common.structBudgetDocStateHistory> m_objStateHistoryList;
        /// <summary>
        /// ������ ��������� ���������� ���������
        /// </summary>
        public List<ERP_Budget.Common.structBudgetDocStateHistory> StateHistoryList
        {
            get { return m_objStateHistoryList; }
        }
        /// <summary>
        /// ������ ��������� ��������
        /// </summary>
        private List<ERP_Budget.Common.CAccountTransn> m_objAccountTransnList;
        /// <summary>
        /// ������ ��������� ��������
        /// </summary>
        public List<ERP_Budget.Common.CAccountTransn> AccountTransnList
        {
            get { return m_objAccountTransnList; }
        }

        #endregion

        #region �����������

        public CManagementBudgetDoc()
        {
            m_moneyAccTrnSum = 0;
            m_objStateHistoryList = null;
            m_objAccountTransnList = null;
        }

        #endregion

        #region ������ ��������� ���������� (����������)
        /// <summary>
        /// ���������� ������ ��������� ����������
        /// </summary>
        /// <param name="objProfile">�������</param>
        /// <param name="uuidBudgetDepID">�� ���������� �������������</param>
        /// <param name="uuidCompanyID">�� ��������</param>
        /// <param name="uuidDebitArticleID">�� ������ ��������</param>
        /// <param name="iUserOwnerID">�� ������������</param>
        /// <param name="dtBegin">���� ������ �������</param>
        /// <param name="dtEnd">���� ��������� �������</param>
        /// <returns>������ ��������� ����������</returns>
        public static System.Collections.Generic.List<CManagementBudgetDoc> GetBudgetDocListManage(UniXP.Common.CProfile objProfile, 
            System.Guid uuidCompanyID, System.Guid uuidDebitArticleID, System.Guid uuidBudgetDepID, 
            System.Int32 iUserOwnerID, System.DateTime dtBegin, System.DateTime dtEnd)
        {
            System.Collections.Generic.List<CManagementBudgetDoc> objList = new System.Collections.Generic.List<CManagementBudgetDoc>();

            System.Data.SqlClient.SqlConnection DBConnection = objProfile.GetDBSource();
            if (DBConnection == null) { return objList; }

            try
            {
                // ���������� � �� ��������, ����������� ������� �� ������� ������
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
                cmd.Connection = DBConnection;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = System.String.Format("[{0}].[dbo].[sp_GetBudgetDocListManage]", objProfile.GetOptionsDllDBName());
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ((System.Byte)(0)), ((System.Byte)(0)), "", System.Data.DataRowVersion.Current, null));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@BEGINDATE", System.Data.SqlDbType.DateTime));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ENDDATE", System.Data.SqlDbType.DateTime));

                cmd.Parameters["@BEGINDATE"].Value = dtBegin;
                cmd.Parameters["@ENDDATE"].Value = dtEnd;
                if (uuidCompanyID.CompareTo(System.Guid.Empty) != 0)
                {
                    cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@COMPANY_GUID_ID", System.Data.SqlDbType.UniqueIdentifier));
                    cmd.Parameters["@COMPANY_GUID_ID"].Value = uuidCompanyID;
                }
                if (uuidDebitArticleID.CompareTo(System.Guid.Empty) != 0)
                {
                    cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@DEBITARTICLE_GUID_ID", System.Data.SqlDbType.UniqueIdentifier));
                    cmd.Parameters["@DEBITARTICLE_GUID_ID"].Value = uuidDebitArticleID;
                }
                if (uuidBudgetDepID.CompareTo(System.Guid.Empty) != 0)
                {
                    cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@BUDGETDEP_GUID_ID", System.Data.SqlDbType.UniqueIdentifier));
                    cmd.Parameters["@BUDGETDEP_GUID_ID"].Value = uuidBudgetDepID;
                }
                if (iUserOwnerID != 0)
                {
                    cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@CREATEDUSER_ID", System.Data.SqlDbType.Int));
                    cmd.Parameters["@CREATEDUSER_ID"].Value = iUserOwnerID;
                }

                System.Data.SqlClient.SqlDataReader rs = cmd.ExecuteReader();
                if (rs.HasRows)
                {
                    // ����� ������ ��������
                    CManagementBudgetDoc objBudgetDoc = null;
                    while (rs.Read())
                    {
                        objBudgetDoc = new CManagementBudgetDoc();

                        objBudgetDoc.uuidID = (System.Guid)rs["GUID_ID"];
                        objBudgetDoc.Date = (System.DateTime)rs["BUDGETDOC_DATE"];
                        objBudgetDoc.Money = System.Convert.ToDouble(rs["BUDGETDOC_MONEY"]);
                        objBudgetDoc.MoneyAgree = System.Convert.ToDouble(rs["BUDGETDOC_MONEYAGREE"]);
                        objBudgetDoc.Objective = (System.String)rs["BUDGETDOC_OBJECTIVE"];
                        objBudgetDoc.PaymentDate = (System.DateTime)rs["BUDGETDOC_PAYMENTDATE"];
                        objBudgetDoc.Recipient = (System.String)rs["BUDGETDOC_RECIPIENT"];
                        objBudgetDoc.DocBasis = (System.String)rs["BUDGETDOC_DOCBASIS"];
                        if (rs["DATESTATE"] != System.DBNull.Value)
                        {
                            objBudgetDoc.DateState = (System.DateTime)rs["DATESTATE"];
                        }
                        if (rs["BUDGETDOC_DESCRIPTION"] != System.DBNull.Value)
                        {
                            objBudgetDoc.Description = (System.String)rs["BUDGETDOC_DESCRIPTION"];
                        }
                        objBudgetDoc.IsActive = (System.Boolean)rs["BUDGETDOC_ACTIVE"];
                        // ��������� �������������
                        objBudgetDoc.BudgetDep = new CBudgetDep((System.Guid)rs["BUDGETDEP_GUID_ID"], (System.String)rs["BUDGETDEP_NAME"]);
                        // ����� �������
                        objBudgetDoc.PaymentType = new CPaymentType((System.Guid)rs["PAYMENTTYPE_GUID_ID"], (System.String)rs["PAYMENTTYPE_NAME"]);
                        //������ �������
                        CBudgetItem objBudgetItem = new CBudgetItem();
                        objBudgetItem.uuidID = (System.Guid)rs["BUDGETITEM_GUID_ID"];
                        objBudgetItem.BudgetItemNum = (System.String)rs["BUDGETITEM_NUM"];
                        objBudgetItem.Name = (System.String)rs["BUDGETITEM_NAME"];
                        objBudgetItem.MoneyInBudgetDocCurrency = objBudgetDoc.Money;
                        objBudgetItem.MoneyInBudgetCurrency = objBudgetDoc.MoneyAgree;
                        if (rs["DEBITARTICLE_GUID_ID"] != System.DBNull.Value)
                        {
                            objBudgetItem.DebitArticleID = (System.Guid)rs["DEBITARTICLE_GUID_ID"];
                        }
                        objBudgetDoc.BudgetItem = objBudgetItem;

                        // ������
                        objBudgetDoc.Currency = new CCurrency((System.Guid)rs["CURRENCY_GUID_ID"], (System.String)rs["CURRENCY_CODE"], (System.String)rs["CURRENCY_CODE"]);
                        // ��������
                        objBudgetDoc.Company = new CCompany((System.Guid)rs["COMPANY_GUID_ID"], (System.String)rs["COMPANY_NAME"], (System.String)rs["COMPANY_ACRONYM"]);
                        // ��������� ���������
                        objBudgetDoc.DocState = new CBudgetDocState((System.Guid)rs["BUDGETDOCSTATE_GUID_ID"], (System.String)rs["BUDGETDOCSTATE_NAME"], (System.Int32)rs["BUDGETDOCSTATE_ID"]);
                        // ��� ���������
                        objBudgetDoc.DocType = new CBudgetDocType((System.Guid)rs["BUDGETDOCTYPE_GUID_ID"], (System.String)rs["BUDGETDOCTYPE_NAME"]);
                        // ���������
                        objBudgetDoc.OwnerUser = new CUser((System.Int32)rs["CREATEDUSER_ID"], (System.Int32)rs["UNIXPUSER_ID"], (System.String)rs["USER_LASTNAME"], (System.String)rs["USER_FIRSTNAME"]);
                        // ��������
                        objBudgetDoc.ExistsAttach = (System.Boolean)rs["ATTACH"];

                        objList.Add(objBudgetDoc);
                    }
                    objBudgetDoc = null;
                }
                rs.Dispose();
                rs.Close();
                // ������� ��������� ���������� ���������
                foreach (CManagementBudgetDoc objBudgetDoc2 in objList)
                {

                    objBudgetDoc2.m_objStateHistoryList = ERP_Budget.Common.CBudgetDoc.GetBudgetDocHistory(objBudgetDoc2.uuidID, cmd, objProfile);
                }
                // ������� �������� ���������� ���������
                foreach (CManagementBudgetDoc objBudgetDoc3 in objList)
                {

                    objBudgetDoc3.m_objAccountTransnList = ERP_Budget.Common.CAccountTransn.GetAccountTransnListForManagerBudgetDoc( objProfile, cmd, objBudgetDoc3.uuidID);
                }
                cmd.Dispose();
            }
            catch (System.Exception f)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(
                "�� ������� �������� ������ ��������� ����������.\n\n����� ������: " + f.Message, "��������",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
            finally // ������� ���������� �������
            {
                DBConnection.Close();
            }
            return objList;
        }
        #endregion

    }
}
