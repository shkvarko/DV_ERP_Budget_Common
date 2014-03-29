using System;
using System.Collections.Generic;
using System.Text;

namespace ERP_Budget.Common
{
    /// <summary>
    /// список типов журнала заявок
    /// </summary>
    public enum enumViewMode
    {
        Err = 0,  // ошибка
        Work,   // необработанные заявки
        Worked, // обработанные заявки 
        Arj     // архив заявок
    };
    /// <summary>
    /// Структура "Запись в выпадающем списке статей бюджета"
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
    /// Структура, соответствующая записи в журнале состояний бюджетного документа
    /// </summary>
    public struct structBudgetDocStateHistory
    {
        /// <summary>
        /// Дата
        /// </summary>
        public System.DateTime HistoryDate;
        /// <summary>
        /// Состояние бюджетного документа
        /// </summary>
        public CBudgetDocState BudgetDocState;
        /// <summary>
        /// Пользователь, изменивший состояние бюджетного документа
        /// </summary>
        public CUser UserHistory;
    }

    /// <summary>
    /// Класс "Запись в журнале оплат бюджетного документа"
    /// </summary>
    public class CBudgetDocPaymentItem
    {
        #region Свойства
        /// <summary>
        /// Уникальный идентификатор
        /// </summary>
        public System.Guid ID { get; set; }
        /// <summary>
        /// Дата оплаты
        /// </summary>
        public System.DateTime PaymentDate { get; set; }
        /// <summary>
        /// Сумма платежа
        /// </summary>
        public double PaymentValue { get; set; }
        /// <summary>
        /// Валюта платежа
        /// </summary>
        public CCurrency Currency { get; set; }
        /// <summary>
        /// Сумма платежа фактическая
        /// </summary>
        public double FactPaymentValue { get; set; }
        /// <summary>
        /// Валюта платежа фактическая
        /// </summary>
        public CCurrency FactCurrency { get; set; }
        #endregion

        #region Констуктор
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

        #region Журнал оплат бюджетного документа
        /// <summary>
        /// Возвращает журнал оплат бюджетного документа
        /// </summary>
        /// <param name="objProfile">профайл</param>
        /// <param name="BudgetDoc_Guid">УИ бюджетного документа</param>
        /// <param name="cmdSQL">SQL-команда</param>
        /// <param name="strErr">текст ошибки</param>
        /// <returns>список объектов класса "CBudgetDocPaymentItem"</returns>
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
                        strErr += ("\nНе удалось получить соединение с базой данных.");
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
                strErr += ("\nНе удалось получить список оплат документа. Текст ошибки: " + f.Message);
            }
            return objList;
        }
        #endregion

        #region Редактировать объект в базе данных
        /// <summary>
        /// Проверка свойств перед сохранением в БД
        /// </summary>
        /// <param name="Payment_Date">дата оплаты</param>
        /// <param name="Payment_Value">сумма платежа</param>
        /// <param name="objCurrency">валюта платежа</param>
        /// <param name="strErr">текс ошибки</param>
        /// <returns>true - все параметры прошли проверку; false - проверка НЕ пройдена</returns>
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
                    strErr += ("Необходимо указать дату оплаты.");
                    return bRet;
                }
                if (Payment_Value <= 0)
                {
                    strErr += ("Необходимо указать сумму оплаты больше нуля.");
                    return bRet;
                }
                if (Currency_Guid.CompareTo(System.Guid.Empty) == 0)
                {
                    strErr += ("Необходимо указать валюту оплаты.");
                    return bRet;
                }
                if (FactPayment_Value <= 0)
                {
                    strErr += ("Необходимо указать сумму фактической оплаты больше нуля.");
                    return bRet;
                }
                if (FactCurrency_Guid.CompareTo(System.Guid.Empty) == 0)
                {
                    strErr += ("Необходимо указать валюту фактической оплаты.");
                    return bRet;
                }
                bRet = true;
            }
            catch (System.Exception f)
            {
                strErr += ("Ошибка проверки свойств объекта \"оплата бюджетного документа\". Текст ошибки: " + f.Message);
            }
            return bRet;
        }
        /// <summary>
        /// Редактирует свойства объекта "Оплата бюджетного документа" в БД
        /// </summary>
        /// <param name="BudgetDocPayment_Guid">УИ записи</param>
        /// <param name="Payment_Date">Дата оплаты</param>
        /// <param name="Payment_Value">Сумма оплаты</param>
        /// <param name="Currency_Guid">УИ валюты оплаты</param>
        /// <param name="FactPayment_Value">Сумма фактической оплаты</param>
        /// <param name="FactCurrency_Guid">УИ валюты фактической оплаты</param>
        /// <param name="objProfile">профайл</param>
        /// <param name="strErr">текст ошибки</param>
        /// <param name="LastPayment_Date">дата последней оплаты бюджетного документа</param>
        /// <returns>true - удачное завершение операции; false - ошибка</returns>
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
                    strErr += ("Не удалось получить соединение с базой данных.");
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
                    // подтверждаем транзакцию
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

                strErr += ("Не удалось внести изменения в объект \"оплата бюджетного документа\". Текст ошибки: " + f.Message);
            }
            finally
            {
                DBConnection.Close();
            }
            return bRet;
        }
        /// <summary>
        /// Сохраняет изменения в списке оплат заявки в БД
        /// </summary>
        /// <param name="objList">список оплат</param>
        /// <param name="objProfile">профайл</param>
        /// <param name="strErr">текст ошибки</param>
        /// <param name="LastPayment_Date">дата последней оплаты бюджетного документа</param>
        /// <returns>true - удачное завершение операции; false - ошибка</returns>
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
                    strErr += ("Не удалось получить соединение с базой данных.");
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
                    // подтверждаем транзакцию
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

                strErr += ("Не удалось внести изменения в список объектов \"оплата бюджетного документа\". Текст ошибки: " + f.Message);
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
        #region Свойства
        /// <summary>
        /// Ссылка на запись в журнале оплат
        /// </summary>
        public System.Guid ID { get; set; }
        /// <summary>
        /// Дата регистрации архивной записи
        /// </summary>
        public System.DateTime RecordUpdated { get; set; }
        /// <summary>
        /// Ссылка на пользователя, редактировавшего запись 
        /// </summary>
        public System.String RecordUserUdpated { get; set; }
        /// <summary>
        /// Дата оплаты новая
        /// </summary>
        public System.DateTime PaymentDateNew { get; set; }
        /// <summary>
        /// Дата оплаты предыдущая
        /// </summary>
        public System.DateTime PaymentDateOld { get; set; }
        /// <summary>
        /// Сумма платежа новая
        /// </summary>
        public double PaymentValueNew { get; set; }
        /// <summary>
        /// Сумма платежа предыдущая
        /// </summary>
        public double PaymentValueOld { get; set; }
        /// <summary>
        /// Сумма платежа по факту новая
        /// </summary>
        public double FactPaymentValueNew { get; set; }
        /// <summary>
        /// Сумма платежа  по факту предыдущая
        /// </summary>
        public double FactPaymentValueOld { get; set; }
        /// <summary>
        /// Валюта фактической оплаты предыдущая
        /// </summary>
        public CCurrency FactCurrencyOld { get; set; }
        /// <summary>
        /// Валюта фактической оплаты новая
        /// </summary>
        public CCurrency FactCurrencyNew { get; set; }
        #endregion

        #region Конструктор
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

        #region Архив изменений даты оплаты
        /// <summary>
        /// Возвращает архив оплат бюджетного документа
        /// </summary>
        /// <param name="objProfile">профайл</param>
        /// <param name="BudgetDoc_Guid">УИ бюджетного документа</param>
        /// <param name="cmdSQL">SQL-команда</param>
        /// <param name="strErr">текст ошибки</param>
        /// <returns>список объектов класса "CBudgetDocPaymentItem"</returns>
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
                        strErr += ("\nНе удалось получить соединение с базой данных.");
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
                strErr += ("\nНе удалось получить список оплат документа. Текст ошибки: " + f.Message);
            }
            return objList;
        }

        #endregion

    }

    /// <summary>
    /// Класс "Бюджетный документ"
    /// </summary>
    public class CBudgetDoc : IBaseListItem
    {
        #region Переменные, Свойства, Константы
        /// <summary>
        /// Тип журнала заявок
        /// </summary>
        private enumViewMode m_ViewMode;
        /// <summary>
        /// Тип журнала заявок
        /// </summary>
        public enumViewMode ViewMode
        {
            get { return m_ViewMode; }
            set { m_ViewMode = value; }
        }
        /// <summary>
        /// Дата
        /// </summary>
        private System.DateTime m_dtDate;
        /// <summary>
        /// Сумма
        /// </summary>
        private double m_moneyMoney;
        /// <summary>
        /// Сумма оплаченная
        /// </summary>
        private double m_moneyMoneyPayment;
        /// <summary>
        /// Цель
        /// </summary>
        private string m_strObjective;
        /// <summary>
        /// Срок оплаты
        /// </summary>
        private System.DateTime m_dtPaymentDate;
        /// <summary>
        /// Получатель средств
        /// </summary>
        private string m_strRecipient;
        /// <summary>
        /// Документальное обоснование
        /// </summary>
        private string m_strDocBasis;
        /// <summary>
        /// Описание
        /// </summary>
        private string m_strDescription;
        /// <summary>
        /// Сумма согласования
        /// </summary>
        private double m_moneyMoneyAgree;
        /// <summary>
        /// Признак "Активен"
        /// </summary>
        private bool m_bIsActive;
        /// <summary>
        /// Маршрут следования документа
        /// </summary>
        private CBudgetRoute m_objRoute;
        /// <summary>
        /// Состояние бюджетного документа
        /// </summary>
        private CBudgetDocState m_objDocState;
        /// <summary>
        /// Тип бюджетного документа
        /// </summary>
        private CBudgetDocType m_objDocType;
        /// <summary>
        /// Единица измерения
        /// </summary>
        private CMeasure m_objMeasure;
        /// <summary>
        /// Форма платежа
        /// </summary>
        private CPaymentType m_objPaymentType;
        /// <summary>
        /// Валюта
        /// </summary>
        private CCurrency m_objCurrency;
        /// <summary>
        /// Компания
        /// </summary>
        private CCompany m_objCompany;
        /// <summary>
        /// Родительская статья бюджета, с которых нужно списать сумму
        /// </summary>
        private CBudgetItem m_objBudgetItem;
        /// <summary>
        /// Список статей бюджета, с которых нужно списать сумму
        /// </summary>
        private List<CBudgetItem> m_objBudgetItemList;
        /// <summary>
        /// Бюджетное подразделение
        /// </summary>
        private CBudgetDep m_objBudgetDep;
        /// <summary>
        /// Дата
        /// </summary>
        public System.DateTime Date
        {
            get { return m_dtDate; }
            set { m_dtDate = value; }
        }
        /// <summary>
        /// Сумма
        /// </summary>
        public double Money
        {
            get { return m_moneyMoney; }
            set { m_moneyMoney = value; }
        }
        /// <summary>
        /// Сумма оплаченная
        /// </summary>
        public double MoneyPayment
        {
            get { return m_moneyMoneyPayment; }
            set { m_moneyMoneyPayment = value; }
        }
        /// <summary>
        /// Цель
        /// </summary>
        public string Objective
        {
            get { return m_strObjective; }
            set { m_strObjective = value; }
        }
        /// <summary>
        /// Срок оплаты
        /// </summary>
        public System.DateTime PaymentDate
        {
            get { return m_dtPaymentDate; }
            set { m_dtPaymentDate = value; }
        }
        /// <summary>
        /// Получатель средств
        /// </summary>
        public string Recipient
        {
            get { return m_strRecipient; }
            set { m_strRecipient = value; }
        }
        /// <summary>
        /// Документальное обоснование
        /// </summary>
        public string DocBasis
        {
            get { return m_strDocBasis; }
            set { m_strDocBasis = value; }
        }
        /// <summary>
        /// Описание
        /// </summary>
        public string Description
        {
            get { return m_strDescription; }
            set { m_strDescription = value; }
        }
        /// <summary>
        /// Сумма согласования
        /// </summary>
        public double MoneyAgree
        {
            get { return m_moneyMoneyAgree; }
            set { m_moneyMoneyAgree = value; }
        }
        /// <summary>
        /// Сумма транзакций по бюджетному документу
        /// </summary>
        private double m_moneyMoneyTrnSum;
        /// <summary>
        /// Сумма транзакций по бюджетному документу
        /// </summary>
        public double MoneyTrnSum
        {
            get { return m_moneyMoneyTrnSum; }
            set { m_moneyMoneyTrnSum = value; }
        }
        /// <summary>
        /// Признак "Активен"
        /// </summary>
        public bool IsActive
        {
            get { return m_bIsActive; }
            set { m_bIsActive = value; }
        }
        /// <summary>
        /// Маршрут следования документа
        /// </summary>
        public CBudgetRoute Route
        {
            get { return m_objRoute; }
            set { m_objRoute = value; }
        }
        /// <summary>
        /// Состояние бюджетного документа
        /// </summary>
        public CBudgetDocState DocState
        {
            get { return m_objDocState; }
            set { m_objDocState = value; }
        }
        /// <summary>
        /// Тип бюджетного документа
        /// </summary>
        public CBudgetDocType DocType
        {
            get { return m_objDocType; }
            set { m_objDocType = value; }
        }
        /// <summary>
        /// Единица измерения
        /// </summary>
        public CMeasure Measure
        {
            get { return m_objMeasure; }
            set { m_objMeasure = value; }
        }
        /// <summary>
        /// Форма платежа
        /// </summary>
        public CPaymentType PaymentType
        {
            get { return m_objPaymentType; }
            set { m_objPaymentType = value; }
        }
        /// <summary>
        /// Валюта
        /// </summary>
        public CCurrency Currency
        {
            get { return m_objCurrency; }
            set { m_objCurrency = value; }
        }
        /// <summary>
        /// Компания
        /// </summary>
        public CCompany Company
        {
            get { return m_objCompany; }
            set { m_objCompany = value; }
        }
        /// <summary>
        /// Родительская статья бюджета
        /// </summary>
        public CBudgetItem BudgetItem
        {
            get { return m_objBudgetItem; }
            set { m_objBudgetItem = value; }
        }
        /// <summary>
        /// Список статей бюджета, с которых нужно списать сумму
        /// </summary>
        public List<CBudgetItem> BudgetItemList
        {
            get { return m_objBudgetItemList; }
            set { m_objBudgetItemList = value; }
        }
        /// <summary>
        /// Бюджетное подразделение
        /// </summary>
        public CBudgetDep BudgetDep
        {
            get { return m_objBudgetDep; }
            set { m_objBudgetDep = value; }
        }
        /// <summary>
        /// Инициатор
        /// </summary>
        private CUser m_OwnerUser;
        /// <summary>
        /// Инициатор
        /// </summary>
        public CUser OwnerUser
        {
            get { return m_OwnerUser; }
            set { m_OwnerUser = value; }
        }
        /// <summary>
        /// Индикатор того, все ли необходимые для сохранения документа свойства определены
        /// </summary>
        public System.Boolean IsReadyForSave
        {
            get { return CheckReadyForSave(); }
        }
        /// <summary>
        /// Список допустимых значений бюджетных подразделений
        /// </summary>
        private System.Collections.Generic.List<CBudgetDep> m_PopupBudgetDepList;
        /// <summary>
        /// Список допустимых значений бюджетных подразделений
        /// </summary>
        public System.Collections.Generic.List<CBudgetDep> PopupBudgetDepList
        {
            get { return m_PopupBudgetDepList; }
            set { m_PopupBudgetDepList = value; }
        }
        /// <summary>
        /// Список допустимых значений форм оплаты
        /// </summary>
        private System.Collections.Generic.List<CPaymentType> m_PopupPaymentTypeList;
        /// <summary>
        /// Список допустимых значений форм оплаты
        /// </summary>
        public System.Collections.Generic.List<CPaymentType> PopupPaymentTypeList
        {
            get { return m_PopupPaymentTypeList; }
            set { m_PopupPaymentTypeList = value; }
        }
        /// <summary>
        /// Список допустимых значений единиц измерения
        /// </summary>
        private System.Collections.Generic.List<CMeasure> m_PopupMeasureList;
        /// <summary>
        /// Список допустимых значений единиц измерения
        /// </summary>
        public System.Collections.Generic.List<CMeasure> PopupMeasureList
        {
            get { return m_PopupMeasureList; }
            set { m_PopupMeasureList = value; }
        }
        /// <summary>
        /// Список допустимых значений валют
        /// </summary>
        private System.Collections.Generic.List<CCurrency> m_PopupCurrencyList;
        /// <summary>
        /// Список допустимых значений валют
        /// </summary>
        public System.Collections.Generic.List<CCurrency> PopupCurrencyList
        {
            get { return m_PopupCurrencyList; }
            set { m_PopupCurrencyList = value; }
        }
        /// <summary>
        /// Список допустимых значений состояний бюджетного документа
        /// </summary>
        private System.Collections.Generic.List<CBudgetDocState> m_PopupBudgetDocStateList;
        /// <summary>
        /// Список допустимых значений состояний бюджетного документа
        /// </summary>
        public System.Collections.Generic.List<CBudgetDocState> PopupBudgetDocStateList
        {
            get { return m_PopupBudgetDocStateList; }
            set { m_PopupBudgetDocStateList = value; }
        }
        /// <summary>
        /// Список допустимых значений типов бюджетного документа
        /// </summary>
        private System.Collections.Generic.List<CBudgetDocType> m_PopupBudgetDocTypeList;
        /// <summary>
        /// Список допустимых значений типов бюджетного документа
        /// </summary>
        public System.Collections.Generic.List<CBudgetDocType> PopupBudgetDocTypeList
        {
            get { return m_PopupBudgetDocTypeList; }
            set { m_PopupBudgetDocTypeList = value; }
        }
        /// <summary>
        /// Уникальный идентификатор бюджета
        /// </summary>
        private System.Guid m_uuidBudgetID;
        /// <summary>
        /// Уникальный идентификатор бюджета
        /// </summary>
        public System.Guid uuidBudgetID
        {
            get { return m_uuidBudgetID; }
            set { m_uuidBudgetID = value; }
        }
        /// <summary>
        /// Список допустимых значений статей бюджета
        /// </summary>
        private System.Collections.Generic.List<CPopupBudgetItem> m_PopupBudgetItemList;
        /// <summary>
        /// Список допустимых значений статей бюджета
        /// </summary>
        public System.Collections.Generic.List<CPopupBudgetItem> PopupBudgetItemList
        {
            get { return m_PopupBudgetItemList; }
            set { m_PopupBudgetItemList = value; }
        }
        /// <summary>
        /// Список допустимых значений компаний
        /// </summary>
        private System.Collections.Generic.List<CCompany> m_PopupCompanyList;
        /// <summary>
        /// Список допустимых значений компаний
        /// </summary>
        public System.Collections.Generic.List<CCompany> PopupCompanyList
        {
            get { return m_PopupCompanyList; }
            set { m_PopupCompanyList = value; }
        }
        /// <summary>
        /// Нехватка средств
        /// </summary>
        private System.Double m_BudgetDocDeficit;
        /// <summary>
        /// Нехватка средств
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
        /// Дата изменения последнего состояния
        /// </summary>
        private System.DateTime m_dtDateState;
        /// <summary>
        /// Дата изменения последнего состояния
        /// </summary>
        public System.DateTime DateState
        {
            get { return m_dtDateState; }
            set { m_dtDateState = value; }
        }
        /// <summary>
        /// Список прикрепленных вложений
        /// </summary>
        private List<CBudgetDocAttach> m_objAttachList;
        /// <summary>
        /// Список прикрепленных вложений
        /// </summary>
        public List<CBudgetDocAttach> AttachList
        {
            get { return m_objAttachList; }
            set { m_objAttachList = value; }
        }
        /// <summary>
        /// Ссылка на идентификатор "родительского документа"
        /// </summary>
        private System.Guid m_uuidSrcDocID;
        /// <summary>
        /// Ссылка на идентификатор "родительского документа"
        /// </summary>
        public System.Guid SrcDocID
        {
            get { return m_uuidSrcDocID; }
            set { m_uuidSrcDocID = value; }
        }
        /// <summary>
        /// Признак того, что к документу прикреплено вложение
        /// </summary>
        private System.Boolean m_bExistsAttach;
        /// <summary>
        /// Признак того, что к документу прикреплено вложение
        /// </summary>
        public System.Boolean ExistsAttach
        {
            get { return m_bExistsAttach; }
            set { m_bExistsAttach = value; }
        }
        /// <summary>
        /// Признак того, что этот документ "Изменение бюджета" 
        /// </summary>
        private System.Boolean m_bResetBudgetItem;
        /// <summary>
        /// Признак того, что этот документ "Изменение бюджета" 
        /// </summary>
        public System.Boolean IsResetBudgetItem
        {
            get { return m_bResetBudgetItem; }
            set { m_bResetBudgetItem = value; }
        }
        /// <summary>
        /// УИ пометки
        /// </summary>
        private System.Guid m_uuidNoteTypeID;
        /// <summary>
        /// УИ пометки
        /// </summary>
        public System.Guid NoteTypeID
        {
            get { return m_uuidNoteTypeID; }
            set { m_uuidNoteTypeID = value; }
        }
        /// <summary>
        /// Комментарий пользователя
        /// </summary>
        private System.String m_strUserComment;
        /// <summary>
        /// Комментарий пользователя
        /// </summary>
        public System.String UserComment
        {
            get { return m_strUserComment; }
            set { m_strUserComment = value; }
        }
        private const System.Int32 iCommandTimeout = 600;
        /// <summary>
        /// Проект
        /// </summary>
        public CBudgetProject BudgetProject { get; set; }
        /// <summary>
        /// Счёт
        /// </summary>
        public CAccountPlan AccountPlan { get; set; }
        /// <summary>
        /// Вид документа
        /// </summary>
        public CBudgetDocCategory BudgetDocCategory { get; set; }
        #endregion

        #region Конструкторы

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

        #region Список бюджетных документов
        /// <summary>
        /// Обновляет список бюджетных документов
        /// </summary>
        /// <param name="objProfile">профайл</param>
        /// <param name="objBudgetDocList">список активных заявок</param>
        /// <param name="objBudjetDocArjList">список неактивных заявок</param>
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
                "Отсутствует соединение с БД!", "Внимание",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                return;
            }

            try
            {
                // соединение с БД получено, прописываем команду на выборку данных
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
                    // набор данных непустой
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
                        // бюджетное подразделение
                        objBudgetDoc.m_objBudgetDep = new CBudgetDep((System.Guid)rs["BUDGETDEP_GUID_ID"], (System.String)rs["BUDGETDEP_NAME"]);
                        // форма платежа
                        objBudgetDoc.m_objPaymentType = new CPaymentType((System.Guid)rs["PAYMENTTYPE_GUID_ID"], (System.String)rs["PAYMENTTYPE_NAME"]);
                        // единица измерения
                        if (rs["MEASURE_GUID_ID"] != System.DBNull.Value)
                        {
                            objBudgetDoc.m_objMeasure = new CMeasure((System.Guid)rs["MEASURE_GUID_ID"], (System.String)rs["MEASURE_NAME"]);
                        }
                        //статья бюджета
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

                        // валюта
                        objBudgetDoc.m_objCurrency = new CCurrency((System.Guid)rs["CURRENCY_GUID_ID"], (System.String)rs["CURRENCY_CODE"], (System.String)rs["CURRENCY_CODE"]);
                        // компания
                        objBudgetDoc.m_objCompany = new CCompany((System.Guid)rs["COMPANY_GUID_ID"], (System.String)rs["COMPANY_NAME"], (System.String)rs["COMPANY_ACRONYM"]);
                        // состояние документа
                        objBudgetDoc.m_objDocState = new CBudgetDocState((System.Guid)rs["BUDGETDOCSTATE_GUID_ID"], (System.String)rs["BUDGETDOCSTATE_NAME"], (System.Int32)rs["BUDGETDOCSTATE_ID"]);
                        // тип документа
                        objBudgetDoc.m_objDocType = new CBudgetDocType((System.Guid)rs["BUDGETDOCTYPE_GUID_ID"], (System.String)rs["BUDGETDOCTYPE_NAME"]);
                        // инициатор
                        objBudgetDoc.m_OwnerUser = new CUser((System.Int32)rs["CREATEDUSER_ID"], (System.Int32)rs["UNIXPUSER_ID"], (System.String)rs["USER_LASTNAME"], (System.String)rs["USER_FIRSTNAME"]);
                        // журнал просмотра
                        objBudgetDoc.m_ViewMode = (enumViewMode)((System.Int32)rs["ViewTypeId"]);
                        // вложение
                        objBudgetDoc.m_bExistsAttach = (System.Boolean)rs["ATTACH"];
                        // проект
                        objBudgetDoc.BudgetProject = ((rs["BUDGETPROJECT_GUID"] != System.DBNull.Value) ? new CBudgetProject()
                            {
                                uuidID = (System.Guid)rs["BUDGETPROJECT_GUID"],
                                Name = System.Convert.ToString(rs["BUDGETPROJECT_NAME"]),
                                IsActive = System.Convert.ToBoolean(rs["BUDGETPROJECT_ACTIVE"]),
                                CodeIn1C = System.Convert.ToInt32(rs["BUDGETPROJECT_1C_CODE"])
                            }
                            : null);
                        // Счёт
                        objBudgetDoc.AccountPlan = ((rs["ACCOUNTPLAN_GUID"] != System.DBNull.Value) ? new CAccountPlan()
                            {
                                uuidID = (System.Guid)rs["ACCOUNTPLAN_GUID"],
                                Name = System.Convert.ToString(rs["ACCOUNTPLAN_NAME"]),
                                IsActive = System.Convert.ToBoolean(rs["ACCOUNTPLAN_ACTIVE"]),
                                CodeIn1C = System.Convert.ToString(rs["ACCOUNTPLAN_1C_CODE"])
                            }
                            : null );
                        // Вид документа
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
                        // уи пометки
                        if( rs["NOTETYPE"] != System.DBNull.Value )
                        {
                            objBudgetDoc.m_uuidNoteTypeID = (System.Guid)rs["NOTETYPE"];
                        }
                        // комментарий пользователя
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
                "Не удалось обновить список бюджетных документов.\n\nТекст ошибки: " + e.Message, "Внимание",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
            finally // очищаем занимаемые ресурсы
            {
                DBConnection.Close();
            }
            return;
        }
        /// <summary>
        /// Загружает список оплаченных бюджетных документов
        /// </summary>
        /// <param name="objProfile">профайл</param>
        /// <param name="objBudjetDocForBackMoneyList">список неактивных заявок</param>
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
                "Отсутствует соединение с БД!", "Внимание",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                return;
            }

            try
            {
                // соединение с БД получено, прописываем команду на выборку данных
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
                    // набор данных непустой
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
                        // бюджетное подразделение
                        objBudgetDoc.m_objBudgetDep = new CBudgetDep((System.Guid)rs["BUDGETDEP_GUID_ID"], (System.String)rs["BUDGETDEP_NAME"]);
                        // форма платежа
                        objBudgetDoc.m_objPaymentType = new CPaymentType((System.Guid)rs["PAYMENTTYPE_GUID_ID"], (System.String)rs["PAYMENTTYPE_NAME"]);
                        // единица измерения
                        if (rs["MEASURE_GUID_ID"] != System.DBNull.Value)
                        {
                            objBudgetDoc.m_objMeasure = new CMeasure((System.Guid)rs["MEASURE_GUID_ID"], (System.String)rs["MEASURE_NAME"]);
                        }
                        //статья бюджета
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

                        // валюта
                        objBudgetDoc.m_objCurrency = new CCurrency((System.Guid)rs["CURRENCY_GUID_ID"], (System.String)rs["CURRENCY_CODE"], (System.String)rs["CURRENCY_CODE"]);
                        // компания
                        objBudgetDoc.m_objCompany = new CCompany((System.Guid)rs["COMPANY_GUID_ID"], (System.String)rs["COMPANY_NAME"], (System.String)rs["COMPANY_ACRONYM"]);
                        // состояние документа
                        objBudgetDoc.m_objDocState = new CBudgetDocState((System.Guid)rs["BUDGETDOCSTATE_GUID_ID"], (System.String)rs["BUDGETDOCSTATE_NAME"], (System.Int32)rs["BUDGETDOCSTATE_ID"]);
                        // тип документа
                        objBudgetDoc.m_objDocType = new CBudgetDocType((System.Guid)rs["BUDGETDOCTYPE_GUID_ID"], (System.String)rs["BUDGETDOCTYPE_NAME"]);
                        // инициатор
                        objBudgetDoc.m_OwnerUser = new CUser((System.Int32)rs["CREATEDUSER_ID"], (System.Int32)rs["UniXPUserID"], (System.String)rs["strLastName"], (System.String)rs["strFirstName"]);
                        // проект
                        objBudgetDoc.BudgetProject = ((rs["BUDGETPROJECT_GUID"] != System.DBNull.Value) ? new CBudgetProject()
                        {
                            uuidID = (System.Guid)rs["BUDGETPROJECT_GUID"],
                            Name = System.Convert.ToString(rs["BUDGETPROJECT_NAME"]),
                            IsActive = System.Convert.ToBoolean(rs["BUDGETPROJECT_ACTIVE"]),
                            CodeIn1C = System.Convert.ToInt32(rs["BUDGETPROJECT_1C_CODE"])
                        }
                            : null);
                        // Счёт
                        objBudgetDoc.AccountPlan = ((rs["ACCOUNTPLAN_GUID"] != System.DBNull.Value) ? new CAccountPlan()
                        {
                            uuidID = (System.Guid)rs["ACCOUNTPLAN_GUID"],
                            Name = System.Convert.ToString(rs["ACCOUNTPLAN_NAME"]),
                            IsActive = System.Convert.ToBoolean(rs["ACCOUNTPLAN_ACTIVE"]),
                            CodeIn1C = System.Convert.ToString(rs["ACCOUNTPLAN_1C_CODE"])
                        }
                            : null);
                        // Вид документа
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
                "Не удалось обновить список оплаченных бюджетных документов.\n\nТекст ошибки: " + e.Message, "Внимание",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
            finally // очищаем занимаемые ресурсы
            {
                DBConnection.Close();
            }
            return;
        }
        #endregion

        #region New ( Новый бюджетный документ )
        /// <summary>
        /// Создает новый объект класса "Бюджетный документ"
        /// </summary>
        /// <param name="objProfile">профайл</param>
        /// <param name="uuidBudgetID">уи бюджета</param>
        /// <returns>объект класса "Бюджетный документ"</returns>
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
                // Выпадающие списки с допустимыми значениями
                objBudgetDoc.LoadPopupList(objProfile);
            }
            catch (System.Exception f)
            {
                objBudgetDoc = null;
                DevExpress.XtraEditors.XtraMessageBox.Show(
                "Не удалось создать новый бюджетный документ.\n\nТекст ошибки: " + f.Message, "Внимание",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
			finally // очищаем занимаемые ресурсы
            {
            }

            return objBudgetDoc;
        }
        #endregion

        #region Copy ( Копирование свойств бюджетного документа )
        /// <summary>
        /// Создает новый объект класса "Бюджетный документ" и присваивает ему свойства объекта с заданным УИ
        /// </summary>
        /// <param name="objProfile">профайл</param>
        /// <param name="uuidBudgetDocID">уи бюджетного документа</param>
        /// <returns>объект класса "Бюджетный документ"</returns>
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
                "Не удалось скопировать свойства бюджетного документа.\nУИ бюджетного документа : " +
                uuidBudgetDocID.ToString() + "\nТекст ошибки: " + f.Message, "Внимание",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
			finally // очищаем занимаемые ресурсы
            {
            }

            return objBudgetDoc;
        }
        /// <summary>
        /// Копирует свойства объекта "Бюджетный документ" текущему объекту "Бюджетный документ"
        /// </summary>
        /// <param name="objBudgetDoc">объект "Бюджетный документ"</param>
        public void Copy(CBudgetDoc objBudgetDoc)
        {
            try
            {
                
                this.BudgetDep = objBudgetDoc.BudgetDep;
                //this.Date = objBudgetDoc.Date;
                //this.PaymentDate = objBudgetDoc.PaymentDate;

                if (this.Date.Year == objBudgetDoc.Date.Year)
                {
                    // бюджет одного и того же года
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
                "Ошибка копирования свойств бюджетного документа.\n\nТекст ошибки: " + f.Message, "Внимание",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
			finally // очищаем занимаемые ресурсы
            {
            }
            return;
        }

        #endregion

        #region Init ( Инициализация свойств класса )
        /// <summary>
        /// Обновляет информацию в выпадающих списках с допустимыми значениями 
        /// </summary>
        /// <param name="objProfile">профайл</param>
        /// <returns>true - успешное обновление; false - ошибка</returns>
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

                // Выпадающие списки с допустимыми значениями
                // бюджетные подразделения
                if (this.m_PopupBudgetDepList == null)
                { this.m_PopupBudgetDepList = new List<CBudgetDep>(); }
                CBudgetDep.RefreshBudgetDepList(objProfile, cmd, this.m_PopupBudgetDepList);

                System.Int32 iPopupBudgetDepList = System.DateTime.Now.Second - dtStart.Second;

                System.DateTime dtStartPopupPaymentTypeList = System.DateTime.Now;
                // формы оплаты
                if (this.m_PopupPaymentTypeList == null)
                { this.m_PopupPaymentTypeList = new List<CPaymentType>(); }
                CPaymentType.RefreshPaymentTypeList(objProfile, cmd, this.m_PopupPaymentTypeList);

                System.Int32 iPopupPaymentTypeList = System.DateTime.Now.Second - dtStartPopupPaymentTypeList.Second;

                System.DateTime dtStartPopupMeasureList = System.DateTime.Now;

                // единицы измерения
                if (this.m_PopupMeasureList == null)
                { this.m_PopupMeasureList = new List<CMeasure>(); }
                CMeasure.RefreshMeasureList(objProfile, cmd, this.m_PopupMeasureList);

                System.Int32 iPopupMeasureList = System.DateTime.Now.Second - dtStartPopupMeasureList.Second;

                System.DateTime dtStartPopupCurrencyList = System.DateTime.Now;

                // валюты
                if (this.m_PopupCurrencyList == null)
                { this.m_PopupCurrencyList = new List<CCurrency>(); }
                CCurrency.RefreshCurrencyList(objProfile, cmd, this.m_PopupCurrencyList);

                System.Int32 iPopupCurrencyList = System.DateTime.Now.Second - dtStartPopupCurrencyList.Second;

                System.DateTime dtStartPopupBudgetDocStateList = System.DateTime.Now;

                // состояния документа
                if (this.m_PopupBudgetDocStateList == null)
                { this.m_PopupBudgetDocStateList = new List<CBudgetDocState>(); }
                CBudgetDocState.RefreshBudgetDocStateList(objProfile, cmd, this.m_PopupBudgetDocStateList);

                System.Int32 iPopupBudgetDocStateList = System.DateTime.Now.Second - dtStartPopupBudgetDocStateList.Second;

                System.DateTime dtStartPopupBudgetDocTypeList = System.DateTime.Now;

                // типы документа
                if (this.m_PopupBudgetDocTypeList == null)
                { this.m_PopupBudgetDocTypeList = new List<CBudgetDocType>(); }
                CBudgetDocType.RefreshBudgetDocTypeList(objProfile, cmd, this.m_PopupBudgetDocTypeList);

                System.Int32 iPopupBudgetDocTypeList = System.DateTime.Now.Second - dtStartPopupBudgetDocTypeList.Second;

                System.DateTime dtStartPopupBudgetItemList = System.DateTime.Now;

                //// статьи бюджета
                //if( this.m_PopupBudgetItemList == null )
                //{ this.m_PopupBudgetItemList = new List<CPopupBudgetItem>(); }
                //LoadPopupBudgetItemList( objProfile, cmd );

                System.Int32 iPopupBudgetItemList = System.DateTime.Now.Second - dtStartPopupBudgetItemList.Second;

                System.DateTime dtStartPopupCompanyList = System.DateTime.Now;
                // компании
                if (this.m_PopupCompanyList == null)
                { this.m_PopupCompanyList = new List<CCompany>(); }
                CCompany.RefreshCompanyList(objProfile, cmd, this.m_PopupCompanyList);

                System.Int32 iPopupCompanyList = System.DateTime.Now.Second - dtStartPopupCompanyList.Second;

                System.DateTime dtFinish = System.DateTime.Now;

                bRet = true;

                //DevExpress.XtraEditors.XtraMessageBox.Show(
                //( "Загрузка выпадающих списков.\n\n" + 
                //  "Время старта: " + dtStart.ToLongTimeString() + "\nВремя завершения: " + dtFinish.ToLongTimeString() + 
                //  "\nОбщее время подготовки выпадающих списков, сек.: " + ( ( System.Int32 )( dtFinish.Second - dtStart.Second ) ).ToString() + 
                //  "\n\nбюджетные подразделения, сек.: " + iPopupBudgetDepList.ToString() + 
                //  "\n\nформы оплаты, сек.: " + iPopupPaymentTypeList.ToString() + 
                //  "\n\nединицы измерения, сек.: " + iPopupMeasureList.ToString() + 
                //  "\n\nвалюты, сек.: " + iPopupCurrencyList.ToString() + 
                //  "\n\nсостояния документа, сек.: " + iPopupBudgetDocStateList.ToString() + 
                //  "\n\nтипы документа, сек.: " + iPopupBudgetDocTypeList.ToString() + 
                //  "\n\nстатьи бюджета, сек.: " + iPopupBudgetItemList.ToString() + 
                //  "\n\nкомпании, сек.: " + iPopupCompanyList.ToString() ), "Внимание",
                //System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information );
            }
            catch (System.Exception f)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(
                "Не удалось сформировать выпадающие списки для бюджетного документа.\nТекст ошибки: " + f.Message, "Внимание",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
			finally // очищаем занимаемые ресурсы
            {
                DBConnection.Close();
            }
            return bRet;
        }

        /// <summary>
        /// Обновляет информацию в выпадающих списках с допустимыми значениями 
        /// </summary>
        /// <param name="objProfile">профайл</param>
        /// <returns>true - успешное обновление; false - ошибка</returns>
        private System.Boolean LoadPopupList(UniXP.Common.CProfile objProfile,
            System.Data.SqlClient.SqlCommand cmd)
        {
            System.Boolean bRet = false;
            if (cmd == null)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show("Отсутствует соединение с БД.", "Внимание",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);
                return bRet;
            }

            try
            {
                // Выпадающие списки с допустимыми значениями
                // бюджетные подразделения
                if (this.m_PopupBudgetDepList == null)
                { this.m_PopupBudgetDepList = new List<CBudgetDep>(); }
                CBudgetDep.RefreshBudgetDepList(objProfile, cmd, this.m_PopupBudgetDepList);

                // формы оплаты
                if (this.m_PopupPaymentTypeList == null)
                { this.m_PopupPaymentTypeList = new List<CPaymentType>(); }
                CPaymentType.RefreshPaymentTypeList(objProfile, cmd, this.m_PopupPaymentTypeList);

                // единицы измерения
                if (this.m_PopupMeasureList == null)
                { this.m_PopupMeasureList = new List<CMeasure>(); }
                CMeasure.RefreshMeasureList(objProfile, cmd, this.m_PopupMeasureList);

                // валюты
                if (this.m_PopupCurrencyList == null)
                { this.m_PopupCurrencyList = new List<CCurrency>(); }
                CCurrency.RefreshCurrencyList(objProfile, cmd, this.m_PopupCurrencyList);

                // состояния документа
                if (this.m_PopupBudgetDocStateList == null)
                { this.m_PopupBudgetDocStateList = new List<CBudgetDocState>(); }
                CBudgetDocState.RefreshBudgetDocStateList(objProfile, cmd, this.m_PopupBudgetDocStateList);

                // типы документа
                if (this.m_PopupBudgetDocTypeList == null)
                { this.m_PopupBudgetDocTypeList = new List<CBudgetDocType>(); }
                CBudgetDocType.RefreshBudgetDocTypeList(objProfile, cmd, this.m_PopupBudgetDocTypeList);

                //// статьи бюджета
                //if( this.m_PopupBudgetItemList == null )
                //{ this.m_PopupBudgetItemList = new List<CPopupBudgetItem>(); }
                //LoadPopupBudgetItemList( objProfile, cmd );

                // компании
                if (this.m_PopupCompanyList == null)
                { this.m_PopupCompanyList = new List<CCompany>(); }
                CCompany.RefreshCompanyList(objProfile, cmd, this.m_PopupCompanyList);

                bRet = true;
            }
            catch (System.Exception f)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(
                "Не удалось сформировать выпадающие списки для бюджетного документа.\nТекст ошибки: " + f.Message, "Внимание",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
			finally // очищаем занимаемые ресурсы
            {
            }

            return bRet;
        }
        /// <summary>
        /// Загружает список статей бюджета
        /// </summary>
        /// <param name="objProfile">профайл</param>
        /// <param name="objBudget">бюджет</param>
        /// <returns>true - успешное завершение; false - ошибка</returns>
        public System.Boolean LoadPopupBudgetItemListForBudget(UniXP.Common.CProfile objProfile,
            ERP_Budget.Common.CBudget objBudget)
        {
            // выглядит немного странно, но сделано для того, что бы не плодить лишних хранимых процедур
            // запрашиваем список статей бюджетОВ для службы, которую мы возьмем из objBudget
            // а потом удаляюм статьи ненужных нам бюджетов

            System.Boolean bRet = false;
            this.m_PopupBudgetItemList.Clear();

            if (objBudget.uuidID.CompareTo(System.Guid.Empty) == 0) { return bRet; }

            System.Data.SqlClient.SqlConnection DBConnection = objProfile.GetDBSource();
            if (DBConnection == null)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show("Отсутствует соединение с БД.", "Внимание",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                return bRet;
            }
            try
            {
                // соединение с БД получено, прописываем команду на выборку данных
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
                cmd.Connection = DBConnection;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                // сперва сформируем список родительских статей бюджета
                bRet = LoadPopupParentBudgetItemListForBudget(objProfile, cmd, objBudget.uuidID);
                // для кажой родительской статьи получим список дочерних статей
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
                "Не удалось получить список статей бюджета.\n\nТекст ошибки: " + f.Message, "Внимание",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
			finally // очищаем занимаемые ресурсы
            {
            }
            return bRet;
        }
        /// <summary>
        /// Загружает список статей бюджета
        /// </summary>
        /// <param name="objProfile">профайл</param>
        /// <param name="uuidBudgetDepID">уи бюджетного подразделения</param>
        /// <returns>true - успешное завершение; false - ошибка</returns>
        public System.Boolean LoadPopupBudgetItemList(UniXP.Common.CProfile objProfile,
            System.Guid uuidBudgetDepID)
        {
            System.Boolean bRet = false;
            this.m_PopupBudgetItemList.Clear();

            System.Data.SqlClient.SqlConnection DBConnection = objProfile.GetDBSource();
            if (DBConnection == null)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show("Отсутствует соединение с БД.", "Внимание",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                return bRet;
            }
            try
            {
                // соединение с БД получено, прописываем команду на выборку данных
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
                cmd.Connection = DBConnection;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                // сперва сформируем список родительских статей бюджета
                bRet = LoadPopupParentBudgetItemList(objProfile, cmd, uuidBudgetDepID);

                // для кажой родительской статьи получим список дочерних статей
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
                "Не удалось получить список статей бюджета.\n\nТекст ошибки: " + f.Message, "Внимание",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
			finally // очищаем занимаемые ресурсы
            {
            }
            return bRet;
        }
        /// <summary>
        /// Загружает список статей бюджета
        /// </summary>
        /// <param name="objProfile">профайл</param>
        /// <param name="cmd">SQl-команда</param>
        /// <returns>true - успешное завершение; false - ошибка</returns>
        private System.Boolean LoadPopupBudgetItemList(UniXP.Common.CProfile objProfile,
            System.Data.SqlClient.SqlCommand cmd)
        {
            System.Boolean bRet = false;
            if (cmd == null)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show("Отсутствует соединение с БД.", "Внимание",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                return bRet;
            }
            try
            {
                //this.m_objBudgetItemList.Clear();
                // сперва сформируем список родительских статей бюджета
                bRet = LoadPopupParentBudgetItemList(objProfile, cmd);
                // для кажой родительской статьи получим список дочерних статей
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
                "Не удалось получить список статей бюджета.\n\nТекст ошибки: " + f.Message, "Внимание",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
			finally // очищаем занимаемые ресурсы
            {
            }
            return bRet;
        }
        /// <summary>
        /// Загружает список родительских статей бюжетов заданного подразделения
        /// </summary>
        /// <param name="objProfile">профайл</param>
        /// <param name="cmd">SQl-команда</param>
        /// <param name="uuidBudgetDepID">уи бюджетного подразделения</param>
        /// <returns>true - успешное завершение; false - ошибка</returns>
        private System.Boolean LoadPopupParentBudgetItemList(UniXP.Common.CProfile objProfile,
            System.Data.SqlClient.SqlCommand cmd, System.Guid uuidBudgetDepID)
        {
            System.Boolean bRet = false;
            if (cmd == null)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show("Отсутствует соединение с БД.", "Внимание",
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
                    DevExpress.XtraEditors.XtraMessageBox.Show("Список статей бюджета пуст!", "Внимание",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);
                }
                rs.Close();
                rs = null;
            }
            catch (System.Exception f)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(
                "Не удалось получить список родительских статей бюджета.\n\nТекст ошибки: " + f.Message, "Внимание",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
			finally // очищаем занимаемые ресурсы
            {
            }

            return bRet;
        }
        /// <summary>
        /// Загружает список родительских статей бюжетов всех подразделений
        /// </summary>
        /// <param name="objProfile">профайл</param>
        /// <param name="cmd">SQl-команда</param>
        /// <returns>true - успешное завершение; false - ошибка</returns>
        private System.Boolean LoadPopupParentBudgetItemList(UniXP.Common.CProfile objProfile,
            System.Data.SqlClient.SqlCommand cmd)
        {
            System.Boolean bRet = false;
            if (cmd == null)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show("Отсутствует соединение с БД.", "Внимание",
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
                    DevExpress.XtraEditors.XtraMessageBox.Show("Список статей бюджета пуст!", "Внимание",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);
                }
                rs.Close();
                rs = null;
            }
            catch (System.Exception f)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(
                "Не удалось получить список родительских статей бюджета.\n\nТекст ошибки: " + f.Message, "Внимание",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
			finally // очищаем занимаемые ресурсы
            {
            }

            return bRet;
        }
        /// <summary>
        /// Загружает список родительских статей заданного бюджета
        /// </summary>
        /// <param name="objProfile">профайл</param>
        /// <param name="cmd">SQl-команда</param>
        /// <param name="uuidBudgetID">уи бюджета</param>
        /// <returns>true - успешное завершение; false - ошибка</returns>
        private System.Boolean LoadPopupParentBudgetItemListForBudget(UniXP.Common.CProfile objProfile,
            System.Data.SqlClient.SqlCommand cmd, System.Guid uuidBudgetID)
        {
            System.Boolean bRet = false;
            if (cmd == null)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show("Отсутствует соединение с БД.", "Внимание",
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
                    DevExpress.XtraEditors.XtraMessageBox.Show("Список статей бюджета пуст!", "Внимание",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);
                }
                rs.Close();
                rs = null;
            }
            catch (System.Exception f)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(
                "Не удалось получить список родительских статей бюджета.\n\nТекст ошибки: " + f.Message, "Внимание",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
			finally // очищаем занимаемые ресурсы
            {
            }

            return bRet;
        }
        /// <summary>
        /// Загружает список дочерних статей бюжетов
        /// </summary>
        /// <param name="objProfile">профайл</param>
        /// <param name="cmd">SQl-команда</param>
        /// <param name="objPopupBudgetItem">Запись в выпадающем списке статей бюджета</param>
        /// <returns>true - успешное завершение; false - ошибка</returns>
        private System.Boolean LoadPopupChildBudgetItemList(UniXP.Common.CProfile objProfile,
            System.Data.SqlClient.SqlCommand cmd, CPopupBudgetItem objPopupBudgetItem)
        {
            System.Boolean bRet = false;
            if (cmd == null)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show("Отсутствует соединение с БД.", "Внимание",
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
                "Не удалось получить список дочерних статей бюджета.\nРодительская статья: " +
                objPopupBudgetItem.ParentBudgetItem.BudgetItemFullName + "\n\nТекст ошибки: " + f.Message, "Внимание",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
			finally // очищаем занимаемые ресурсы
            {
            }

            return bRet;
        }

        /// <summary>
        /// Инициализация свойств класса
        /// </summary>
        /// <param name="objProfile">профайл</param>
        /// <param name="cmd">SQL-команда</param>
        /// <returns>true - успешная инициализация; false - ошибка</returns>
        public System.Boolean Init(UniXP.Common.CProfile objProfile, System.Data.SqlClient.SqlCommand cmd)
        {
            System.Boolean bRet = false;

            if (cmd == null) { return bRet; }

            try
            {
                // соединение с БД получено, прописываем команду на выборку данных
                cmd.Parameters.Clear();
                cmd.CommandText = System.String.Format("[{0}].[dbo].[sp_GetBudgetDoc]", objProfile.GetOptionsDllDBName());
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ((System.Byte)(0)), ((System.Byte)(0)), "", System.Data.DataRowVersion.Current, null));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@GUID_ID", System.Data.SqlDbType.UniqueIdentifier));
                cmd.Parameters["@GUID_ID"].Value = this.m_uuidID;
                System.Data.SqlClient.SqlDataReader rs = cmd.ExecuteReader();
                if (rs.HasRows)
                {
                    // набор данных непустой, в нем нас интересует одна запись
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
                    // бюджетное подразделение
                    this.m_objBudgetDep = new CBudgetDep((System.Guid)rs["BUDGETDEP_GUID_ID"], (System.String)rs["BUDGETDEP_NAME"]);
                    // форма платежа
                    this.m_objPaymentType = new CPaymentType((System.Guid)rs["PAYMENTTYPE_GUID_ID"], (System.String)rs["PAYMENTTYPE_NAME"]);
                    // единица измерения
                    if (rs["MEASURE_GUID_ID"] != System.DBNull.Value)
                    {
                        this.m_objMeasure = new CMeasure((System.Guid)rs["MEASURE_GUID_ID"], (System.String)rs["MEASURE_NAME"]);
                    }
                    //родительская статья бюджета
                    CBudgetItem objBudgetItem = new CBudgetItem();
                    objBudgetItem.uuidID = (System.Guid)rs["BUDGETITEM_GUID_ID"];
                    objBudgetItem.BudgetItemNum = (System.String)rs["BUDGETITEM_NUM"];
                    objBudgetItem.Name = (System.String)rs["BUDGETITEM_NAME"];
                    if (rs["DEBITARTICLE_GUID_ID"] != System.DBNull.Value)
                    {
                        objBudgetItem.DebitArticleID = (System.Guid)rs["DEBITARTICLE_GUID_ID"];
                    }
                    this.m_objBudgetItem = objBudgetItem;

                    // валюта
                    this.m_objCurrency = new CCurrency((System.Guid)rs["CURRENCY_GUID_ID"], (System.String)rs["CURRENCY_CODE"], (System.String)rs["CURRENCY_CODE"]);
                    // компания
                    this.m_objCompany = new CCompany((System.Guid)rs["COMPANY_GUID_ID"], (System.String)rs["COMPANY_NAME"], (System.String)rs["COMPANY_ACRONYM"]);
                    // состояние документа
                    this.m_objDocState = new CBudgetDocState((System.Guid)rs["BUDGETDOCSTATE_GUID_ID"], (System.String)rs["BUDGETDOCSTATE_NAME"], (System.Int32)rs["BUDGETDOCSTATE_ID"]);
                    // тип документа
                    this.m_objDocType = new CBudgetDocType((System.Guid)rs["BUDGETDOCTYPE_GUID_ID"],
                        (System.String)rs["BUDGETDOCTYPE_NAME"], (System.Boolean)rs["NEEDDISION"],
                            (System.String)rs["CLASS_NAME"], (System.Int32)rs["PRIORITY"]);
                    // инициатор
                    this.m_OwnerUser = new CUser((System.Int32)rs["CREATEDUSER_ID"], (System.Int32)rs["UNIXPUSER_ID"], (System.String)rs["USER_LASTNAME"], (System.String)rs["USER_FIRSTNAME"]);
                    this.m_bDivision = (System.Boolean)rs["DIVISION"];
                    // проект
                    this.BudgetProject = ((rs["BUDGETPROJECT_GUID"] != System.DBNull.Value) ? new CBudgetProject()
                    {
                        uuidID = (System.Guid)rs["BUDGETPROJECT_GUID"],
                        Name = System.Convert.ToString(rs["BUDGETPROJECT_NAME"]),
                        IsActive = System.Convert.ToBoolean(rs["BUDGETPROJECT_ACTIVE"]),
                        CodeIn1C = System.Convert.ToInt32(rs["BUDGETPROJECT_1C_CODE"])
                    }
                        : null);
                    // Счёт
                    this.AccountPlan = ((rs["ACCOUNTPLAN_GUID"] != System.DBNull.Value) ? new CAccountPlan()
                    {
                        uuidID = (System.Guid)rs["ACCOUNTPLAN_GUID"],
                        Name = System.Convert.ToString(rs["ACCOUNTPLAN_NAME"]),
                        IsActive = System.Convert.ToBoolean(rs["ACCOUNTPLAN_ACTIVE"]),
                        CodeIn1C = System.Convert.ToString(rs["ACCOUNTPLAN_1C_CODE"])
                    }
                        : null);
                    // Вид документа
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
                    "Не удалось проинициализировать класс CBudgetDoc.\nВ БД не найдена информация.\nУИ бюджетного документа : " +
                    uuidID.ToString(), "Внимание",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);

                }
                rs.Close();
                rs = null;

                if (bRet == true)
                {
                    // список статей бюджета
                    bRet = LoadBudgetItemList(objProfile, cmd);
                }
                if (bRet == true)
                {
                    // маршрут
                    this.m_objRoute = new CBudgetRoute(this.m_uuidID, cmd, objProfile);
                }
                // загрузим список вложений
                CBudgetDocAttach.LoadAttachmentList(objProfile, cmd, this);
                cmd.Dispose();
            }
            catch (System.Exception e)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(
                "Не удалось проинициализировать класс CBudgetDoc.\nУИ бюджетного документа : " +
                    this.uuidID.ToString() + "\nТекст ошибки: " + e.Message, "Внимание",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
			finally // очищаем занимаемые ресурсы
            {
            }
            return bRet;
        }

        /// <summary>
        /// Инициализация свойств класса
        /// </summary>
        /// <param name="objProfile">профайл</param>
        /// <param name="uuidID">уникальный идентификатор класса</param>
        /// <returns>true - успешная инициализация; false - ошибка</returns>
        public override System.Boolean Init(UniXP.Common.CProfile objProfile, System.Guid uuidID)
        {
            System.Boolean bRet = false;

            System.Data.SqlClient.SqlConnection DBConnection = objProfile.GetDBSource();
            if (DBConnection == null) { return bRet; }

            try
            {
                // соединение с БД получено, прописываем команду на выборку данных
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
                    // набор данных непустой, в нем нас интересует одна запись
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
                    // бюджетное подразделение
                    this.m_objBudgetDep = new CBudgetDep((System.Guid)rs["BUDGETDEP_GUID_ID"], (System.String)rs["BUDGETDEP_NAME"]);
                    this.m_objBudgetDep.Manager = new CUser((System.Int32)rs["BUDGETDEP_MANAGER"],
                        (System.Int32)rs["BUDGETDEP_MANAGER_UniXPUserID"],
                        (System.String)rs["BUDGETDEP_MANAGER_LASTNAME"],
                        (System.String)rs["BUDGETDEP_MANAGER_FIRSTNAME"]);
                    // форма платежа
                    this.m_objPaymentType = new CPaymentType((System.Guid)rs["PAYMENTTYPE_GUID_ID"], (System.String)rs["PAYMENTTYPE_NAME"]);
                    // единица измерения
                    if (rs["MEASURE_GUID_ID"] != System.DBNull.Value)
                    {
                        this.m_objMeasure = new CMeasure((System.Guid)rs["MEASURE_GUID_ID"], (System.String)rs["MEASURE_NAME"]);
                    }
                    //родительская статья бюджета
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

                    // валюта
                    this.m_objCurrency = new CCurrency((System.Guid)rs["CURRENCY_GUID_ID"], (System.String)rs["CURRENCY_CODE"], (System.String)rs["CURRENCY_CODE"]);
                    // компания
                    this.m_objCompany = new CCompany((System.Guid)rs["COMPANY_GUID_ID"], (System.String)rs["COMPANY_NAME"], (System.String)rs["COMPANY_ACRONYM"]);
                    // состояние документа
                    this.m_objDocState = new CBudgetDocState((System.Guid)rs["BUDGETDOCSTATE_GUID_ID"], (System.String)rs["BUDGETDOCSTATE_NAME"], (System.Int32)rs["BUDGETDOCSTATE_ID"]);
                    // тип документа
                    this.m_objDocType = new CBudgetDocType((System.Guid)rs["BUDGETDOCTYPE_GUID_ID"],
                        (System.String)rs["BUDGETDOCTYPE_NAME"], (System.Boolean)rs["NEEDDISION"],
                            (System.String)rs["CLASS_NAME"], (System.Int32)rs["PRIORITY"]);
                    // инициатор
                    this.m_OwnerUser = new CUser((System.Int32)rs["CREATEDUSER_ID"], (System.Int32)rs["UNIXPUSER_ID"], (System.String)rs["USER_LASTNAME"], (System.String)rs["USER_FIRSTNAME"]);
                    this.m_bDivision = (System.Boolean)rs["DIVISION"];
                    // проект
                    this.BudgetProject = ((rs["BUDGETPROJECT_GUID"] != System.DBNull.Value) ? new CBudgetProject()
                    {
                        uuidID = (System.Guid)rs["BUDGETPROJECT_GUID"],
                        Name = System.Convert.ToString(rs["BUDGETPROJECT_NAME"]),
                        IsActive = System.Convert.ToBoolean(rs["BUDGETPROJECT_ACTIVE"]),
                        CodeIn1C = System.Convert.ToInt32(rs["BUDGETPROJECT_1C_CODE"])
                    }
                        : null);
                    // Счёт
                    this.AccountPlan = ((rs["ACCOUNTPLAN_GUID"] != System.DBNull.Value) ? new CAccountPlan()
                    {
                        uuidID = (System.Guid)rs["ACCOUNTPLAN_GUID"],
                        Name = System.Convert.ToString(rs["ACCOUNTPLAN_NAME"]),
                        IsActive = System.Convert.ToBoolean(rs["ACCOUNTPLAN_ACTIVE"]),
                        CodeIn1C = System.Convert.ToString(rs["ACCOUNTPLAN_1C_CODE"])
                    }
                        : null);

                    this.m_objBudgetItem.AccountPlan = this.AccountPlan;

                    // Вид документа
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
                    "Не удалось проинициализировать класс CBudgetDoc.\nВ БД не найдена информация.\nУИ бюджетного документа : " +
                    uuidID.ToString(), "Внимание",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);

                }
                rs.Close();
                rs = null;

                if (bRet == true)
                {
                    //сумма оплаты документа
                    this.MoneyPayment = GetSumMoneyPayment(objProfile, cmd);
                }
                if (bRet == true)
                {
                    // список статей бюджета
                    bRet = LoadBudgetItemList(objProfile, cmd);
                }
                if (bRet == true)
                {
                    // маршрут
                    this.m_objRoute = new CBudgetRoute(this.m_uuidID, cmd, objProfile);
                }
                //if( bRet == true )
                //{
                //    // Обновляем информацию в выпадающих списках с допустимыми значениями 
                //    bRet = this.LoadPopupList( objProfile, cmd );
                //}
                this.BudgetDep.LoadAdditionalManagerList(objProfile, cmd);
                // загрузим список вложений
                CBudgetDocAttach.LoadAttachmentList(objProfile, cmd, this);
                cmd.Dispose();
            }
            catch (System.Exception e)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(
                "Не удалось проинициализировать класс CBudgetDoc.\nУИ бюджетного документа : " +
                    uuidID.ToString() + "\nТекст ошибки: " + e.Message, "Внимание",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
			finally // очищаем занимаемые ресурсы
            {
                DBConnection.Close();
            }
            return bRet;
        }
        /// <summary>
        /// Определяет сумму оплаты заявки
        /// </summary>
        /// <param name="objProfile">профайл</param>
        /// <param name="cmd">SQL-команда</param>
        /// <returns>сумма оплаты заявки</returns>
        private double GetSumMoneyPayment(UniXP.Common.CProfile objProfile, System.Data.SqlClient.SqlCommand cmd)
        {
            double doubleRet = 0;
            if (cmd == null)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show("Отсутствует соединение с БД.", "Внимание",
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
                "Не удалось определить сумму оплаты документа.\n\nТекст ошибки: " + f.Message, "Внимание",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }

            return doubleRet;
        }
        /// <summary>
        /// Запрашивает список статей бюджета, связанных с документом
        /// </summary>
        /// <param name="objProfile">профайл</param>
        /// <param name="cmd">SQL-команда</param>
        /// <returns>true - успешное завершение; false - ошибка</returns>
        private System.Boolean LoadBudgetItemList(UniXP.Common.CProfile objProfile,
            System.Data.SqlClient.SqlCommand cmd)
        {
            System.Boolean bRet = false;
            if (cmd == null)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show("Отсутствует соединение с БД.", "Внимание",
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
                        //статья бюджета
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
                "Не удалось загрузить список статей бюджета.\n\nТекст ошибки: " + f.Message, "Внимание",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }

            return bRet;
        }

        /// <summary>
        /// Запрашивает список статей бюджета, связанных с документом
        /// </summary>
        /// <param name="objProfile">профайл</param>
        /// <returns>true - успешное завершение; false - ошибка</returns>
        public System.Boolean LoadBudgetItemList(UniXP.Common.CProfile objProfile)
        {
            System.Boolean bRet = false;
            System.Data.SqlClient.SqlConnection DBConnection = objProfile.GetDBSource();
            if (DBConnection == null)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show("Отсутствует соединение с БД.", "Внимание",
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
                        //статья бюджета
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
                "Не удалось загрузить список статей бюджета.\n\nТекст ошибки: " + f.Message, "Внимание",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
            finally
            {
                DBConnection.Close();
            }
            return bRet;
        }

        #endregion

        #region Remove ( Удалить объект из БД )
        /// <summary>
        /// Удалить запись из БД
        /// </summary>
        /// <param name="objProfile">профайл</param>
        /// <returns>true - удачное завершение; false - ошибка</returns>
        public override System.Boolean Remove(UniXP.Common.CProfile objProfile)
        {
            System.Boolean bRet = false;
            // уникальный идентификатор не должен быть пустым
            if (this.m_uuidID == System.Guid.Empty)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show("Недопустимое значение уникального идентификатора объекта\nУИ : " +
                    this.m_uuidID.ToString(), "Внимание",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                return bRet;
            }

            System.Data.SqlClient.SqlConnection DBConnection = objProfile.GetDBSource();
            if (DBConnection == null) { return bRet; }
            System.Data.SqlClient.SqlTransaction DBTransaction = DBConnection.BeginTransaction();

            try
            {
                // соединение с БД получено, прописываем команду на удаление данных
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
                    // подтверждаем транзакцию
                    DBTransaction.Commit();
                    bRet = true;
                }
                else
                {
                    // откатываем транзакцию
                    DBTransaction.Rollback();
                    switch (iRet)
                    {
                        case 1:
                            {
                                DevExpress.XtraEditors.XtraMessageBox.Show("Документ связан с проводкой.\nУдаление невозможно.\nУИ документа : " + this.uuidID.ToString(), "Внимание",
                                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);
                                break;
                            }
                        default:
                            {
                                DevExpress.XtraEditors.XtraMessageBox.Show("Ошибка удаления бюджетного документа\nУИ документа : " + this.uuidID.ToString(), "Внимание",
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
                "Не удалось удалить бюджетный документ.\nУИ документа : " + this.uuidID.ToString() + "\nТекст ошибки: " + e.Message, "Внимание",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
			finally // очищаем занимаемые ресурсы
            {
                DBConnection.Close();
            }
            return bRet;
        }
        /// <summary>
        /// Удаление бюджетного документа ( статус "неактивен", состояние "удален" )
        /// </summary>
        /// <param name="objProfile">профайл</param>
        /// <param name="iUserID">уи пользователя</param>
        /// <returns>true - удачное завершение; false - ошибка</returns>
        public System.Boolean Drop(UniXP.Common.CProfile objProfile, System.Int32 iUserID)
        {
            System.Boolean bRet = false;

            // уникальный идентификатор не должен быть пустым
            if (this.m_uuidID == System.Guid.Empty)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show("Недопустимое значение уникального идентификатора объекта\nУИ : " +
                    this.m_uuidID.ToString(), "Внимание",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                return bRet;
            }

            System.Data.SqlClient.SqlConnection DBConnection = objProfile.GetDBSource();
            if (DBConnection == null)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show("Отсутствует соединение с БД.", "Внимание",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                return bRet;
            }

            System.Data.SqlClient.SqlTransaction DBTransaction = DBConnection.BeginTransaction();

            try
            {
                // соединение с БД получено, прописываем команду на удаление данных
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
                    // подтверждаем транзакцию
                    DBTransaction.Commit();
                    bRet = true;
                }
                else
                {
                    // откатываем транзакцию
                    DBTransaction.Rollback();
                    DevExpress.XtraEditors.XtraMessageBox.Show("Отмена удаления документа.\n\n" +
                        (System.String)cmd.Parameters["@ERROR_MES"].Value, "Ошибка",
                       System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);
                }

                cmd = null;
            }
            catch (System.Exception f)
            {
                DBTransaction.Rollback();
                DevExpress.XtraEditors.XtraMessageBox.Show(
                "Не удалось удалить бюджетный документ.\nУИ документа : " + this.uuidID.ToString() + "\n\nТекст ошибки: " + f.Message, "Внимание",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
			finally // очищаем занимаемые ресурсы
            {
                DBConnection.Close();
            }

            return bRet;
        }
        #endregion

        #region Add ( Добавить объект в БД )
        /// <summary>
        /// Проверяет все ли необходимые для сохранения в БД свойства указаны
        /// </summary>
        /// <returns>true - все свойства указаны; false - не все или ошибка</returns>
        private System.Boolean CheckReadyForSave()
        {
            System.Boolean bRet = false;
            try
            {
                // бюджетное подразделение
                if (this.m_objBudgetDep == null) { return bRet; }
                // статья бюджета
                if ((this.m_objBudgetItem == null) || (this.m_objBudgetItem.AccountPlan == null)) { return bRet; }
                if ((this.m_objBudgetItemList == null) || (this.m_objBudgetItemList.Count == 0)) { return bRet; }
                else
                {
                    if (this.DocState == null)
                    {
                        // новая
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
                // компания
                if (this.m_objCompany == null) { return bRet; }
                // валюта
                if (this.m_objCurrency == null) { return bRet; }
                // тип документа
                if (this.m_objDocType == null) { return bRet; }
                // форма оплаты
                if (this.m_objPaymentType == null) { return bRet; }
                // маршрут документа
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
                // получатель средств
                if (this.m_strRecipient == "") { return bRet; }
                // цель
                if (this.m_strObjective == "") { return bRet; }

                bRet = true;
            }
            catch (System.Exception f)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(
                "Ошибка проверки свойств бюджетного документа.\nУИ документа: " + this.uuidID.ToString() + "\nТекст ошибки: " + f.Message, "Внимание",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
            return bRet;
        }
        /// <summary>
        /// Проверяет содержимое свойств бюджетного документа
        /// </summary>
        /// <returns>true - все свойства указаны; false - ошибка</returns>
        public System.Boolean CheckValidProperties()
        {
            System.Boolean bRet = false;
            try
            {
                // необходимо указать бюджетное подразделение
                if (this.m_objBudgetDep == null)
                {
                    DevExpress.XtraEditors.XtraMessageBox.Show("Укажите бюджетное подразделение", "Внимание",
                        System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);
                    return bRet;
                }
                // необходимо указать статью бюджета
                if ((this.m_objBudgetItemList == null) || (this.m_objBudgetItemList.Count == 0))
                {
                    DevExpress.XtraEditors.XtraMessageBox.Show("Укажите статью бюджета", "Внимание",
                        System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);
                    return bRet;
                }
                // необходимо указать компанию
                if (this.m_objCompany == null)
                {
                    DevExpress.XtraEditors.XtraMessageBox.Show("Укажите компанию", "Внимание",
                        System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);
                    return bRet;
                }
                // необходимо указать валюту
                if (this.m_objCurrency == null)
                {
                    DevExpress.XtraEditors.XtraMessageBox.Show("Укажите валюту", "Внимание",
                        System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);
                    return bRet;
                }
                // необходимо указать тип документа
                if (this.m_objDocType == null)
                {
                    DevExpress.XtraEditors.XtraMessageBox.Show("Укажите тип документа", "Внимание",
                        System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);
                    return bRet;
                }
                //// необходимо указать единицу измерения
                //if( this.m_objMeasure == null )
                //{
                //    DevExpress.XtraEditors.XtraMessageBox.Show(  "Укажите единицу измерения", "Внимание",
                //        System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning );
                //    return bRet;
                //}
                // необходимо указать форму оплаты
                if (this.m_objPaymentType == null)
                {
                    DevExpress.XtraEditors.XtraMessageBox.Show("Укажите форму оплаты", "Внимание",
                        System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);
                    return bRet;
                }
                // необходимо указать маршрут документа
                if (this.m_objRoute.RoutePointList.Count == 0)
                {
                    DevExpress.XtraEditors.XtraMessageBox.Show("Укажите маршрут документа", "Внимание",
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
                        DevExpress.XtraEditors.XtraMessageBox.Show("Не во всех точках маршрута указан пользователь.", "Внимание",
                            System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);
                        return bRet;
                    }
                }
                // необходимо указать получателя средств
                if (this.m_strRecipient == "")
                {
                    DevExpress.XtraEditors.XtraMessageBox.Show("Укажите получателя средств", "Внимание",
                        System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);
                    return bRet;
                }
                // необходимо указать цель
                if (this.m_strObjective == "")
                {
                    DevExpress.XtraEditors.XtraMessageBox.Show("Укажите цель", "Внимание",
                        System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);
                    return bRet;
                }
                //// необходимо указать документальное основание
                //if( this.m_strDocBasis == "" )
                //{
                //    DevExpress.XtraEditors.XtraMessageBox.Show( "Укажите документальное основание", "Внимание",
                //        System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning );
                //    return bRet;
                //}
                bRet = true;
            }
            catch (System.Exception f)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(
                "Ошибка проверки свойств бюджетного документа.\nУИ документа : " + this.uuidID.ToString() + "\nТекст ошибки: " + f.Message, "Внимание",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
            return bRet;
        }

        /// <summary>
        /// Добавить запись в БД
        /// </summary>
        /// <param name="objProfile">профайл</param>
        /// <returns>true - удачное завершение; false - ошибка</returns>
        public override System.Boolean Add(UniXP.Common.CProfile objProfile)
        {
            return false;
        }

        /// <summary>
        /// Добавить запись в БД
        /// </summary>
        /// <param name="objProfile">профайл</param>
        /// <param name="cmd">SQL-команда</param>
        /// <returns>true - удачное завершение; false - ошибка</returns>
        private System.Boolean AddBudgetDoc(UniXP.Common.CProfile objProfile, System.Data.SqlClient.SqlCommand cmd,
            ViewDocVariantCondition objDocVariantCondn)
        {
            System.Boolean bRet = false;

            // проверка свойств бюджетного документа
            if (this.CheckValidProperties() == false) { return bRet; }
            if (cmd == null) { return bRet; }
            try
            {
                // соединение с БД получено, прописываем команду на создание записи в БД
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
                    // сохраняем список статей бюджета
                    bRet = SaveBudgetItemList(cmd, objProfile);
                    if ( ( bRet == true ) && ( this.m_bResetBudgetItem == true ) )
                    {
                        // сохраняем список расшифровок
                        bRet = SaveBudgetDocItemList(cmd, objProfile);
                    }
                    if (bRet == true)
                    {
                        // сохраняем маршрут в БД
                        bRet = this.m_objRoute.Update(objProfile, cmd);
                        if (bRet == true)
                        {
                            // сохраняем список вложений
                            bRet = this.SaveAttachmentList(objProfile, cmd);
                            if (bRet == true)
                            {
                                // и делаем сообщение для следующего по маршруту пользователя
                                CBudgetMail.AddMailInQueue(objProfile, cmd, this.m_uuidID);
                            }
                        }
                    }
                }
                else
                {
                    DevExpress.XtraEditors.XtraMessageBox.Show("Ошибка создания бюджетного документа.\nТекст ошибки: " +
                            (System.String)cmd.Parameters["@ERROR_MES"].Value, "Внимание",
                        System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                }
            }
            catch (System.Exception f)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(
                    "Ошибка создания бюджетного документа.\nТекст ошибки: " +
                    (System.String)cmd.Parameters["@ERROR_MES"].Value + "\n\n" + f.Message, "Внимание",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
			finally // очищаем занимаемые ресурсы
            {
            }
            return bRet;
        }
        /// <summary>
        /// сохраняет список статей бюджета, связанных с бюджетным документом
        /// </summary>
        /// <param name="cmd">SQL-команда</param>
        /// <param name="objProfile">профайл</param>
        /// <returns>true - удачное завершение операции; false - ошибка</returns>
        private System.Boolean SaveBudgetItemList(System.Data.SqlClient.SqlCommand cmd,
            UniXP.Common.CProfile objProfile)
        {
            System.Boolean bRet = false;
            if (cmd == null)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show("Отсутствует соединение с БД.", "Внимание",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                return bRet;
            }
            if ((this.m_objBudgetItemList == null) || (this.m_objBudgetItemList.Count == 0))
            {
                DevExpress.XtraEditors.XtraMessageBox.Show("Бюджетному документу не назначена статья расходов.", "Внимание",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                return bRet;
            }

            try
            {
                // сперва удаляем список статей бюджета
                cmd.Parameters.Clear();
                cmd.CommandText = System.String.Format("[{0}].[dbo].[sp_DeleteBudgetDocItemList]", objProfile.GetOptionsDllDBName());
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ((System.Byte)(0)), ((System.Byte)(0)), "", System.Data.DataRowVersion.Current, null));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@BUDGETDOC_GUID_ID", System.Data.SqlDbType.UniqueIdentifier));

                cmd.Parameters["@BUDGETDOC_GUID_ID"].Value = this.m_uuidID;
                cmd.ExecuteNonQuery();
                System.Int32 iRet = (System.Int32)cmd.Parameters["@RETURN_VALUE"].Value;
                if (iRet == 0)
                {
                    // теперь сохраняем список статей расходов с суммами
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
                                    DevExpress.XtraEditors.XtraMessageBox.Show("Не найден бюджетный документ с заданным идентификатором\nУИ: " + this.uuidID.ToString(), "Внимание",
                                        System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);
                                    break;
                                case 2:
                                    DevExpress.XtraEditors.XtraMessageBox.Show("Не найдена статья бюджета с заданным идентификатором\nУИ: " + objBudgetItem.uuidID.ToString(), "Внимание",
                                        System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                                    break;
                                default:
                                    DevExpress.XtraEditors.XtraMessageBox.Show("Ошибка сохранения списка статей расходов.", "Внимание",
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
                    DevExpress.XtraEditors.XtraMessageBox.Show("Не удалось очистить список статей бюджета у документа.\nУИ докумета: " +
                        this.m_uuidID.ToString(), "Внимание",
                        System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                }
            }
            catch (System.Exception f)
            {
                bRet = false;
                DevExpress.XtraEditors.XtraMessageBox.Show(
                "Не удалось сохранить список статей расходов.\n\nТекст ошибки: " + f.Message, "Внимание",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
			finally // очищаем занимаемые ресурсы
            {
            }
            return bRet;
        }
        /// <summary>
        /// Сохраняет список статей бюджета, связанных с бюджетным документом
        /// </summary>
        /// <param name="BUDGETDOC_GUID_ID">УИ бюджетного документа</param>
        /// <param name="objBudgetItemList">список статей бюджета документа</param>
        /// <param name="cmd">SQL-команда</param>
        /// <param name="objProfile">профайл</param>
        /// <param name="iRetCode">код возврата хранимой процедуры</param>
        /// <param name="strErr">сообщение об ошибке</param>
        /// <returns>true - удачное завершение операции; false - ошибка</returns>
        public static System.Boolean SaveBudgetItemList( System.Guid BUDGETDOC_GUID_ID, List<CBudgetItem> objBudgetItemList, 
            System.Data.SqlClient.SqlCommand cmd,
            UniXP.Common.CProfile objProfile, ref System.Int32 iRetCode, ref System.String strErr)
        {
            System.Boolean bRet = false;
            if (cmd == null)
            {
                strErr += ("Отсутствует соединение с БД.");
                return bRet;
            }
            if ((objBudgetItemList == null) || (objBudgetItemList.Count == 0))
            {
                strErr += ("Бюджетному документу не назначена статья расходов.");
                return bRet;
            }

            try
            {
                // сперва удаляем список статей бюджета
                cmd.Parameters.Clear();
                cmd.CommandText = System.String.Format("[{0}].[dbo].[sp_DeleteBudgetDocItemList]", objProfile.GetOptionsDllDBName());
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ((System.Byte)(0)), ((System.Byte)(0)), "", System.Data.DataRowVersion.Current, null));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@BUDGETDOC_GUID_ID", System.Data.SqlDbType.UniqueIdentifier));

                cmd.Parameters["@BUDGETDOC_GUID_ID"].Value = BUDGETDOC_GUID_ID;
                cmd.ExecuteNonQuery();
                System.Int32 iRet = (System.Int32)cmd.Parameters["@RETURN_VALUE"].Value;
                if (iRet == 0)
                {
                    // теперь сохраняем список статей расходов с суммами
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
                                    strErr += String.Format("Не найден бюджетный документ с заданным идентификатором\nУИ: {0}", BUDGETDOC_GUID_ID);
                                    break;
                                case 2:
                                    strErr += String.Format("Не найдена статья бюджета с заданным идентификатором\nУИ: {0}", objBudgetItem.uuidID);
                                    break;
                                default:
                                    strErr += ("Ошибка сохранения списка статей расходов.");
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
                    strErr += ("Не удалось очистить список статей бюджета у документа.");
                }

            }
            catch (System.Exception f)
            {
                bRet = false;
                strErr += ("Не удалось сохранить список статей расходов. Текст ошибки: " + f.Message);
            }
			finally // очищаем занимаемые ресурсы
            {
            }
            return bRet;
        }
        /// <summary>
        /// сохраняет список расшифровок, связанных с бюджетным документом
        /// </summary>
        /// <param name="cmd">SQL-команда</param>
        /// <param name="objProfile">профайл</param>
        /// <returns>true - удачное завершение операции; false - ошибка</returns>
        private System.Boolean SaveBudgetDocItemList(System.Data.SqlClient.SqlCommand cmd,
            UniXP.Common.CProfile objProfile)
        {
            System.Boolean bRet = false;
            if (cmd == null)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show("Отсутствует соединение с БД.", "Внимание",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                return bRet;
            }
            if ((this.m_objBudgetItemList == null) || (this.m_objBudgetItemList.Count == 0))
            {
                DevExpress.XtraEditors.XtraMessageBox.Show("Бюджетному документу не назначена статья расходов.", "Внимание",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                return bRet;
            }
            if (this.IsResetBudgetItem == false)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show("Бюджетный документ не содержит списка расшифровок.", "Внимание",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                return bRet;
            }

            try
            {
                // сперва удаляем список расшифровок, связанных с бюджетным документом
                cmd.Parameters.Clear();
                cmd.CommandText = System.String.Format("[{0}].[dbo].[sp_DeleteBudgetDocItemDecode]", objProfile.GetOptionsDllDBName());
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ((System.Byte)(0)), ((System.Byte)(0)), "", System.Data.DataRowVersion.Current, null));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@BUDGETDOC_GUID_ID", System.Data.SqlDbType.UniqueIdentifier));

                cmd.Parameters["@BUDGETDOC_GUID_ID"].Value = this.m_uuidID;
                cmd.ExecuteNonQuery();
                System.Int32 iRet = (System.Int32)cmd.Parameters["@RETURN_VALUE"].Value;
                if (iRet == 0)
                {
                    // теперь сохраняем список расшифровок, связанных с бюджетным документом
                    foreach (CBudgetItem objBudgetItem in this.m_objBudgetItemList)
                    {
                        bRet = objBudgetItem.SaveBudgetItemDecodeListForBudgetDoc(cmd, objProfile, this.uuidID);
                        if (bRet == false) { break; }
                    }

                }
                else
                {
                    DevExpress.XtraEditors.XtraMessageBox.Show("Не удалось очистить список расшифровок у документа.\nУИ докумета: " +
                        this.m_uuidID.ToString(), "Внимание",
                        System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                }
            }
            catch (System.Exception f)
            {
                bRet = false;
                DevExpress.XtraEditors.XtraMessageBox.Show(
                "Не удалось сохранить список расшифровок для документа.\n\nТекст ошибки: " + f.Message, "Внимание",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
			finally // очищаем занимаемые ресурсы
            {
            }
            return bRet;
        }
        /// <summary>
        /// Сохраняет список вложений к бюджетному документу
        /// </summary>
        /// <param name="cmd">SQL-команда</param>
        /// <param name="objProfile">профайл</param>
        /// <returns>true - удачное завершение; false - ошибка</returns>
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
                    DevExpress.XtraEditors.XtraMessageBox.Show("Отсутствует соединение с БД.", "Внимание",
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
                "Не удалось сохранить список вложений.\n\nТекст ошибки: " + f.Message, "Внимание",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
			finally // очищаем занимаемые ресурсы
            {
            }

            return bRet;
        }
        #endregion

        #region Update ( Сохранить изменения в свойствах объекта в БД )
        /// <summary>
        /// Сохранить изменения в БД
        /// </summary>
        /// <param name="objProfile">профайл</param>
        /// <returns>true - удачное завершение; false - ошибка</returns>
        public override System.Boolean Update(UniXP.Common.CProfile objProfile)
        {
            return false;
        }

        /// <summary>
        /// Сохранить изменения в БД
        /// </summary>
        /// <param name="objProfile">профайл</param>
        /// <param name="cmd">SQL-команда</param>
        /// <param name="objDocVariantCondn"></param>
        /// <returns>true - удачное завершение; false - ошибка</returns>
        private System.Boolean UpdateBudgetDoc(UniXP.Common.CProfile objProfile,
            System.Data.SqlClient.SqlCommand cmd, ViewDocVariantCondition objDocVariantCondn)
        {
            System.Boolean bRet = false;

            // проверка свойств бюджетного документа
            if (this.CheckValidProperties() == false) { return bRet; }
            if (cmd == null) { return bRet; }
            try
            {
                // соединение с БД получено, прописываем команду на создание записи в БД
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
                    // сохраняем список статей бюджета
                    if (objDocVariantCondn.IsEndRoute == true)
                    {
                        // если это последнее действие над заявкой, то зачем переписывать её расшифровку?!
                        bRet = true;
                    }
                    else
                    {
                        bRet = SaveBudgetItemList(cmd, objProfile);
                    }
                    if ((bRet == true) && (this.m_bResetBudgetItem == true))
                    {
                        // сохраняем список расшифровок
                        bRet = SaveBudgetDocItemList(cmd, objProfile);
                    }
                    if (bRet == true)
                    {
                        // сохраняем маршрут в БД
                        bRet = this.m_objRoute.Update(objProfile, cmd);
                    }
                }
                else
                {
                    DevExpress.XtraEditors.XtraMessageBox.Show("Ошибка создания бюджетного документа.\nТекст ошибки: " +
                            (System.String)cmd.Parameters["@ERROR_MES"].Value, "Внимание",
                        System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                }
            }
            catch (System.Exception f)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(
                    "Ошибка создания бюджетного документа.\nТекст ошибки: " +
                    (System.String)cmd.Parameters["@ERROR_MES"].Value + "\n\n" + f.Message, "Внимание",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
			finally // очищаем занимаемые ресурсы
            {
            }
            return bRet;
        }

        #endregion

        #region Отправка по маршруту

        /// <summary>
        /// Вспомогательная структура для выбора варианта отображения заявки  и отправки по маршруту
        /// </summary>
        public class ViewDocVariantCondition
        {
            /// <summary>
            /// главное динамическое право
            /// </summary>
            public CDynamicRight DynamicRight;
            /// <summary>
            /// текущее состояние заявки
            /// </summary>
            public CBudgetDocState DocState;
            /// <summary>
            /// состояние, в которое перейдет заявкf
            /// </summary>
            public CBudgetDocState NextDocState;
            /// <summary>
            /// действие
            /// </summary>
            public CBudgetDocEvent DocEvent;
            /// <summary>
            /// наличие дополнительного права
            /// </summary>
            public System.Boolean AdvancedRight;
            /// <summary>
            /// создатель заявки
            /// </summary>
            public CUser Owner;
            /// <summary>
            /// текущий пользователь
            /// </summary>
            public CUser CurrentUser;
            /// <summary>
            /// бюджетное подразделение
            /// </summary>
            public CBudgetDep BudgetDep;
            /// <summary>
            /// признак активности заявки
            /// </summary>
            public System.Boolean IsActive;
            /// <summary>
            /// тип бюджетного документа
            /// </summary>
            public CBudgetDocType DocType;
            /// <summary>
            /// признак окончания маршрута
            /// </summary>
            public System.Boolean IsEndRoute;
            /// <summary>
            /// УИ исходного бюджетного документа
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
        /// Отправляет документ по маршруту
        /// </summary>
        /// <param name="objProfile">профайл</param>
        /// <param name="objDocVariantCondn"></param>
        /// <returns>true - удачное завершение; false - ошибка</returns>
        public System.Boolean bSendBudgetDoc(ViewDocVariantCondition objDocVariantCondn,
            UniXP.Common.CProfile objProfile)
        {
            System.Boolean bRet = false;
            try
            {
                // попробуем определить, какие проводки должны выполниться
                // для этого должны быть определены тип документа, действие и динамическое право
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
                    //"Не удалось определить тип операции с балансом статьи расходов.", "Внимание",
                    //System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning );
                    //return bRet;
                }

                // теперь можно сохранить бюджетный документ
                bRet = SaveBudgetDoc(objProfile, objDocVariantCondn, objAccountTrnEvent);
            }
            catch (System.Exception f)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(
                "Не удалось отправить документ по маршруту.\nТекст ошибки: " + f.Message, "Внимание",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
			finally // очищаем занимаемые ресурсы
            {
            }

            return bRet;
        }
        /// <summary>
        /// Сохраняет документ в БД
        /// </summary>
        /// <param name="objProfile">профайл</param>
        /// <param name="objDocVariantCondn"></param>
        /// <param name="objAccountTrnEvent">список действий над балансом статьи</param>
        /// <returns>true - удачное завершение; false - ошибка</returns>
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
                // сперва сохраняем бюджетный документ
                if (objDocVariantCondn.DocState == null)
                {
                    // новая заявка
                    bRet = AddBudgetDoc(objProfile, cmd, objDocVariantCondn);
                }
                else
                {
                    // ранее сохраненная заявка
                    bRet = UpdateBudgetDoc(objProfile, cmd, objDocVariantCondn);
                }
                if (bRet)
                {
                    // заявка сохранилась, изменяем баланс статьи
                    // 2009.04.20
                    // небольшое изменение... если это исключение станет расти, то нужно будет превратить его в правило
                    // кассир и бухгалтер могут создавать заявки задним числом и, как следствие,
                    // оплачивать их задним числом, поэтому дату резерва и оплаты будем брать из PaymentDate
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
                        // подтверждаем транзакцию
                        DBTransaction.Commit();
                    }
                    else
                    {
                        // откатываем транзакцию
                        DBTransaction.Rollback();
                    }
                }
                else
                {
                    // откатываем транзакцию
                    DBTransaction.Rollback();
                }

                cmd.Dispose();
            }
            catch (System.Exception f)
            {
                DBTransaction.Rollback();
                DevExpress.XtraEditors.XtraMessageBox.Show(
                    "Ошибка сохранения бюджетного документа.\nТекст ошибки: " + f.Message, "Внимание",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
			finally // очищаем занимаемые ресурсы
            {
                DBConnection.Close();
            }
            return bRet;
        }

        /// <summary>
        /// Устанавливает признак "Открыт" бюджетного документа
        /// </summary>
        /// <param name="isOpen">значение признака "Открыт"</param>
        /// <param name="objProfile">профайл</param>
        /// <returns>true - удачное завершение; false - ошибка</returns>
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
                    // подтверждаем транзакцию
                    DBTransaction.Commit();
                }
                else
                {
                    // откатываем транзакцию
                    DBTransaction.Rollback();
                    switch (iRet)
                    {
                        case 1:
                            DevExpress.XtraEditors.XtraMessageBox.Show("Документ с указанным идентификатором не найден\nУИ : " + this.uuidID.ToString(), "Внимание",
                                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);
                            break;
                        case 2:
                            DevExpress.XtraEditors.XtraMessageBox.Show("Не указан идентификатор пользователя.", "Внимание",
                                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                            break;
                        case 3:
                            DevExpress.XtraEditors.XtraMessageBox.Show("Пользователь с указанным идентификатором не найден\nУИ : " + objProfile.m_nSQLUserID.ToString(), "Внимание",
                                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                            break;
                        case 4:
                            DevExpress.XtraEditors.XtraMessageBox.Show("Документ с указанным идентификатором открыт другим пользователем\nУИ документа: " + this.uuidID.ToString() +
                                "\n\nДокумент открыт: " + (System.String)cmd.Parameters["@UserLastName"].Value +
                                 " " + (System.String)cmd.Parameters["@UserFirstName"].Value +
                                 " в " + ((System.DateTime)cmd.Parameters["@UserOpenDate"].Value).ToShortTimeString(), "Внимание",
                                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                            break;
                        default:
                            DevExpress.XtraEditors.XtraMessageBox.Show("Ошибка выполнения операции в базе данных.\n\n" +
                                (System.String)cmd.Parameters["@ERROR_MESSAGE"].Value, "Внимание",
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
                    "Ошибка установки признака \"Открыт\" бюджетного документа.\nТекст SQL-ошибки: " +
                    (System.String)cmd.Parameters["@ERROR_MESSAGE"].Value + "\n\nТекст ошибки: " + f.Message, "Внимание",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
			finally // очищаем занимаемые ресурсы
            {
                DBConnection.Close();
            }
            return bRet;
        }
        /// <summary>
        /// Проверяет, открыт ли документ в данный момент
        /// </summary>
        /// <param name="objProfile">профайл</param>
        /// <returns>если открыт возвращает строку с именем пользователя и датой открытия</returns>
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
                                 (System.String)cmd.Parameters["@UserFirstName"].Value + " в " +
                                 ((System.DateTime)cmd.Parameters["@UserOpenDate"].Value).ToShortTimeString();
                    }
                }
                else
                {
                    DevExpress.XtraEditors.XtraMessageBox.Show("Ошибка выполнения запроса к базе данных.\n" +
                        (System.String)cmd.Parameters["@ERROR_MESSAGE"].Value, "Внимание",
                        System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                }

                cmd.Dispose();
            }
            catch (System.Exception f)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(
                    "Ошибка проверки состояния бюджетного документа.\nТекст SQL-ошибки: " +
                    (System.String)cmd.Parameters["@ERROR_MESSAGE"].Value + "\n\nТекст ошибки: " + f.Message, "Внимание",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
			finally // очищаем занимаемые ресурсы
            {
                DBConnection.Close();
            }
            return strRet;
        }

        #endregion

        #region Журнал состояний бюджетного документа
        /// <summary>
        /// Возвращает список состояний бюджетного документа
        /// </summary>
        /// <param name="objProfile">профайл</param>
        /// <returns>список состояний бюджетного документа</returns>
        public System.Collections.Generic.List<structBudgetDocStateHistory> GetBudgetDocHistory(UniXP.Common.CProfile objProfile)
        {
            List<structBudgetDocStateHistory> objList = new List<structBudgetDocStateHistory>();

            System.Data.SqlClient.SqlConnection DBConnection = objProfile.GetDBSource();
            if (DBConnection == null)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show("Отсутствует соединение с БД.", "Внимание",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                return objList;
            }

            try
            {
                // соединение с БД получено, прописываем команду на выборку данных
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
                        // дата
                        objDocStateHistory.HistoryDate = (System.DateTime)rs["BUDGETDOCHISTORY_DATE"];
                        // состояние
                        objDocStateHistory.BudgetDocState =
                            new CBudgetDocState((System.Guid)rs["BUDGETDOCSTATE_GUID_ID"],
                            (System.String)rs["BUDGETDOCSTATE_NAME"]);
                        // пользователь
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
                "Не удалось получить список состояний бюджетного документа.\n\nТекст ошибки: " + e.Message, "Внимание",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
            finally // очищаем занимаемые ресурсы
            {
                DBConnection.Close();
            }

            return objList;
        }
        /// <summary>
        /// Возвращает список состояний бюджетного документа
        /// </summary>
        /// <param name="uuidBudgetDocID">уи бюджетного документа</param>
        /// <param name="cmd">команда SQL</param>
        /// <param name="objProfile">профайл</param>
        /// <returns>список состояний бюджетного документа</returns>
        public static System.Collections.Generic.List<structBudgetDocStateHistory> GetBudgetDocHistory(System.Guid uuidBudgetDocID,
            System.Data.SqlClient.SqlCommand cmd, UniXP.Common.CProfile objProfile)
        {
            List<structBudgetDocStateHistory> objList = new List<structBudgetDocStateHistory>();

            if (cmd == null)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show("Отсутствует соединение с БД.", "Внимание",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                return objList;
            }

            try
            {
                // соединение с БД получено, прописываем команду на выборку данных
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
                        // дата
                        objDocStateHistory.HistoryDate = (System.DateTime)rs["BUDGETDOCHISTORY_DATE"];
                        // состояние
                        objDocStateHistory.BudgetDocState =
                            new CBudgetDocState((System.Guid)rs["BUDGETDOCSTATE_GUID_ID"],
                            (System.String)rs["BUDGETDOCSTATE_NAME"]);
                        // пользователь
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
                "Не удалось получить список состояний бюджетного документа.\n\nТекст ошибки: " + e.Message, "Внимание",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
            finally // очищаем занимаемые ресурсы
            {
            }

            return objList;
        }
        #endregion

        #region Изменить дату и дату оплаты
        public System.Boolean ChangePaymentDate(UniXP.Common.CProfile objProfile, System.Int32 iUserID, 
            System.DateTime newBudgetDocDate, System.DateTime newBudgetDocPaymentDate )
        {
            System.Boolean bRet = false;

            // уникальный идентификатор не должен быть пустым
            if (this.m_uuidID == System.Guid.Empty)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show("Недопустимое значение уникального идентификатора объекта\nУИ : " +
                    this.m_uuidID.ToString(), "Внимание",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                return bRet;
            }

            System.Data.SqlClient.SqlConnection DBConnection = objProfile.GetDBSource();
            if (DBConnection == null)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show("Отсутствует соединение с БД.", "Внимание",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                return bRet;
            }

            System.Data.SqlClient.SqlTransaction DBTransaction = DBConnection.BeginTransaction();

            try
            {
                // соединение с БД получено, прописываем команду на удаление данных
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
                    // подтверждаем транзакцию
                    DBTransaction.Commit();
                    bRet = true;
                }
                else
                {
                    // откатываем транзакцию
                    DBTransaction.Rollback();
                    DevExpress.XtraEditors.XtraMessageBox.Show("Отмена изменения даты документа.\n\n" +
                        (System.String)cmd.Parameters["@ERROR_MES"].Value, "Ошибка",
                       System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);
                }

                cmd = null;
            }
            catch (System.Exception f)
            {
                DBTransaction.Rollback();
                DevExpress.XtraEditors.XtraMessageBox.Show(
                "Не удалось изменить дату бюджетного документа.\nУИ документа : " + this.uuidID.ToString() + "\n\nТекст ошибки: " + f.Message, "Внимание",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
			finally // очищаем занимаемые ресурсы
            {
                DBConnection.Close();
            }

            return bRet;
        }
        #endregion

        #region Аннулирование оплаченной заявки
        /// <summary>
        /// Аннулирование оплаченной заявки
        /// </summary>
        /// <param name="objProfile">профайл</param>
        /// <param name="iUserID">УИ пользователя</param>
        /// <returns>true - удачное завершение операции; false - ошибка</returns>
        public System.Boolean BackMoneyAfterPaid(UniXP.Common.CProfile objProfile, System.Int32 iUserID)
        {
            System.Boolean bRet = false;

            // уникальный идентификатор не должен быть пустым
            if (this.m_uuidID == System.Guid.Empty)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show("Недопустимое значение уникального идентификатора объекта\nУИ : " +
                    this.m_uuidID.ToString(), "Внимание",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                return bRet;
            }

            System.Data.SqlClient.SqlConnection DBConnection = objProfile.GetDBSource();
            if (DBConnection == null)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show("Отсутствует соединение с БД.", "Внимание",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                return bRet;
            }

            System.Data.SqlClient.SqlTransaction DBTransaction = DBConnection.BeginTransaction();

            try
            {
                // соединение с БД получено, прописываем команду на удаление данных
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
                    // подтверждаем транзакцию
                    DBTransaction.Commit();
                    bRet = true;
                }
                else
                {
                    // откатываем транзакцию
                    DBTransaction.Rollback();
                    DevExpress.XtraEditors.XtraMessageBox.Show("Ошибка аннулирования бюджетного документа.\n\n" +
                        (System.String)cmd.Parameters["@ERROR_MES"].Value, "Ошибка",
                       System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);
                }

                cmd = null;
            }
            catch (System.Exception f)
            {
                DBTransaction.Rollback();
                DevExpress.XtraEditors.XtraMessageBox.Show(
                "Не удалось аннулировать бюджетный документ.\nУИ документа : " + this.uuidID.ToString() + "\n\nТекст ошибки: " + f.Message, "Внимание",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
			finally // очищаем занимаемые ресурсы
            {
                DBConnection.Close();
            }

            return bRet;
        }

        #endregion

        #region Редактирование реквизитов заявки
        /// <summary>
        /// Редактирует параметр "компания" у заявки
        /// </summary>
        /// <param name="BudgetDoc_Guid">УИ заявки</param>
        /// <param name="Company_Guid">УИ компании</param>
        /// <param name="objProfile">профайл</param>
        /// <param name="strErr">текс ошибки</param>
        /// <returns>true - удачное завершение операции; false - ошибка</returns>
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
                    strErr += ("Не удалось получить соединение с базой данных.");
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
                    // подтверждаем транзакцию
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

                strErr += ("Не удалось внести изменения в параметр \"компания\". Текст ошибки: " + f.Message);
            }
            finally
            {
                DBConnection.Close();
            }
            return bRet;
        }

        /// <summary>
        /// Редактирует параметр "форма оплаты" у заявки
        /// </summary>
        /// <param name="BudgetDoc_Guid">УИ заявки</param>
        /// <param name="PaymentType_Guid">УИ формы оплаты</param>
        /// <param name="objProfile">профайл</param>
        /// <param name="strErr">текс ошибки</param>
        /// <returns>true - удачное завершение операции; false - ошибка</returns>
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
                    strErr += ("Не удалось получить соединение с базой данных.");
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
                    // подтверждаем транзакцию
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

                strErr += ("Не удалось внести изменения в параметр \"форма оплаты\". Текст ошибки: " + f.Message);
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
        #region Переменные, Свойства, Константы
        /// <summary>
        /// Сумма транзаций
        /// </summary>
        private double m_moneyAccTrnSum;
        /// <summary>
        /// Сумма транзаций
        /// </summary>
        public double AccTrnSum
        {
            get { return m_moneyAccTrnSum; }
            set { m_moneyAccTrnSum = value; }
        }
        /// <summary>
        /// Список состояний бюджетного документа
        /// </summary>
        private List<ERP_Budget.Common.structBudgetDocStateHistory> m_objStateHistoryList;
        /// <summary>
        /// Список состояний бюджетного документа
        /// </summary>
        public List<ERP_Budget.Common.structBudgetDocStateHistory> StateHistoryList
        {
            get { return m_objStateHistoryList; }
        }
        /// <summary>
        /// Список бюджетных проводок
        /// </summary>
        private List<ERP_Budget.Common.CAccountTransn> m_objAccountTransnList;
        /// <summary>
        /// Список бюджетных проводок
        /// </summary>
        public List<ERP_Budget.Common.CAccountTransn> AccountTransnList
        {
            get { return m_objAccountTransnList; }
        }

        #endregion

        #region Конструктор

        public CManagementBudgetDoc()
        {
            m_moneyAccTrnSum = 0;
            m_objStateHistoryList = null;
            m_objAccountTransnList = null;
        }

        #endregion

        #region Список бюджетных документов (управление)
        /// <summary>
        /// Возвращает список бюджетных документов
        /// </summary>
        /// <param name="objProfile">профайл</param>
        /// <param name="uuidBudgetDepID">уи бюджетного подразделения</param>
        /// <param name="uuidCompanyID">уи компании</param>
        /// <param name="uuidDebitArticleID">уи статьи расходов</param>
        /// <param name="iUserOwnerID">уи пользователя</param>
        /// <param name="dtBegin">дата начала периода</param>
        /// <param name="dtEnd">дата окончания периода</param>
        /// <returns>список бюджетных документов</returns>
        public static System.Collections.Generic.List<CManagementBudgetDoc> GetBudgetDocListManage(UniXP.Common.CProfile objProfile, 
            System.Guid uuidCompanyID, System.Guid uuidDebitArticleID, System.Guid uuidBudgetDepID, 
            System.Int32 iUserOwnerID, System.DateTime dtBegin, System.DateTime dtEnd)
        {
            System.Collections.Generic.List<CManagementBudgetDoc> objList = new System.Collections.Generic.List<CManagementBudgetDoc>();

            System.Data.SqlClient.SqlConnection DBConnection = objProfile.GetDBSource();
            if (DBConnection == null) { return objList; }

            try
            {
                // соединение с БД получено, прописываем команду на выборку данных
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
                    // набор данных непустой
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
                        // бюджетное подразделение
                        objBudgetDoc.BudgetDep = new CBudgetDep((System.Guid)rs["BUDGETDEP_GUID_ID"], (System.String)rs["BUDGETDEP_NAME"]);
                        // форма платежа
                        objBudgetDoc.PaymentType = new CPaymentType((System.Guid)rs["PAYMENTTYPE_GUID_ID"], (System.String)rs["PAYMENTTYPE_NAME"]);
                        //статья бюджета
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

                        // валюта
                        objBudgetDoc.Currency = new CCurrency((System.Guid)rs["CURRENCY_GUID_ID"], (System.String)rs["CURRENCY_CODE"], (System.String)rs["CURRENCY_CODE"]);
                        // компания
                        objBudgetDoc.Company = new CCompany((System.Guid)rs["COMPANY_GUID_ID"], (System.String)rs["COMPANY_NAME"], (System.String)rs["COMPANY_ACRONYM"]);
                        // состояние документа
                        objBudgetDoc.DocState = new CBudgetDocState((System.Guid)rs["BUDGETDOCSTATE_GUID_ID"], (System.String)rs["BUDGETDOCSTATE_NAME"], (System.Int32)rs["BUDGETDOCSTATE_ID"]);
                        // тип документа
                        objBudgetDoc.DocType = new CBudgetDocType((System.Guid)rs["BUDGETDOCTYPE_GUID_ID"], (System.String)rs["BUDGETDOCTYPE_NAME"]);
                        // инициатор
                        objBudgetDoc.OwnerUser = new CUser((System.Int32)rs["CREATEDUSER_ID"], (System.Int32)rs["UNIXPUSER_ID"], (System.String)rs["USER_LASTNAME"], (System.String)rs["USER_FIRSTNAME"]);
                        // вложение
                        objBudgetDoc.ExistsAttach = (System.Boolean)rs["ATTACH"];

                        objList.Add(objBudgetDoc);
                    }
                    objBudgetDoc = null;
                }
                rs.Dispose();
                rs.Close();
                // история состояний бюджетного документа
                foreach (CManagementBudgetDoc objBudgetDoc2 in objList)
                {

                    objBudgetDoc2.m_objStateHistoryList = ERP_Budget.Common.CBudgetDoc.GetBudgetDocHistory(objBudgetDoc2.uuidID, cmd, objProfile);
                }
                // история проводок бюджетного документа
                foreach (CManagementBudgetDoc objBudgetDoc3 in objList)
                {

                    objBudgetDoc3.m_objAccountTransnList = ERP_Budget.Common.CAccountTransn.GetAccountTransnListForManagerBudgetDoc( objProfile, cmd, objBudgetDoc3.uuidID);
                }
                cmd.Dispose();
            }
            catch (System.Exception f)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(
                "Не удалось получить список бюджетных документов.\n\nТекст ошибки: " + f.Message, "Внимание",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
            finally // очищаем занимаемые ресурсы
            {
                DBConnection.Close();
            }
            return objList;
        }
        #endregion

    }
}
