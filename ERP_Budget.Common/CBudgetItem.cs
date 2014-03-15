using System;
using System.Collections.Generic;
using System.Text;

namespace ERP_Budget.Common
{
    /// <summary>
    /// Множество месяцев года
    /// </summary>
    public enum enumMonth { Unkown = 0, Jan = 1, Feb, Mar, Apr, May, Jun, Jul, Aug, Sep, Oct, Nov, Dec };
    /// <summary>
    /// Расшифровка статьи бюджета
    /// </summary>
    public class CBudgetItemDecode
    {
        #region Переменные, свойства, константы
        /// <summary>
        /// Месяц
        /// </summary>
        private enumMonth m_Month;
        /// <summary>
        /// Месяц
        /// </summary>
        public enumMonth Month
        {
            get { return m_Month; }
            set { m_Month = value; }
        }
        /// <summary>
        /// Название месяца по-русски
        /// </summary>
        public System.String MonthTranslateRu
        {
            get 
            {
                System.String strRet = "";
                switch (m_Month)
                {
                    case enumMonth.Jan:
                        {
                            strRet = "Январь";
                            break;
                        }
                    case enumMonth.Feb:
                        {
                            strRet = "Февраль";
                            break;
                        }
                    case enumMonth.Mar:
                        {
                            strRet = "Март";
                            break;
                        }
                    case enumMonth.Apr:
                        {
                            strRet = "Апрель";
                            break;
                        }
                    case enumMonth.May:
                        {
                            strRet = "Май";
                            break;
                        }
                    case enumMonth.Jun:
                        {
                            strRet = "Июнь";
                            break;
                        }
                    case enumMonth.Jul:
                        {
                            strRet = "Июль";
                            break;
                        }
                    case enumMonth.Aug:
                        {
                            strRet = "Август";
                            break;
                        }
                    case enumMonth.Sep:
                        {
                            strRet = "Сентябрь";
                            break;
                        }
                    case enumMonth.Oct:
                        {
                            strRet = "Октябрь";
                            break;
                        }
                    case enumMonth.Nov:
                        {
                            strRet = "Ноябрь";
                            break;
                        }
                    case enumMonth.Dec:
                        {
                            strRet = "Декабрь";
                            break;
                        }

                    default:
                        break;
                }
                return strRet;
            }
        }
        /// <summary>
        /// Валюта
        /// </summary>
        private CCurrency m_objCurrency;
        /// <summary>
        /// Валюта
        /// </summary>
        public CCurrency Currency
        {
            get { return m_objCurrency; }
            set { m_objCurrency = value; }
        }
        /// <summary>
        /// Примечание
        /// </summary>
        private System.String m_Description;
        /// <summary>
        /// Примечание
        /// </summary>
        public System.String Description
        {
            get { return m_Description; }
            set { m_Description = value; }
        }
        /// <summary>
        /// Сумма (план)
        /// </summary>
        private System.Double m_moneyMoneyPlan;
        /// <summary>
        /// Сумма (план)
        /// </summary>
        public double MoneyPlan
        {
            get { return m_moneyMoneyPlan; }
            set { m_moneyMoneyPlan = value; }
        }
        /// <summary>
        /// Сумма (разрешено)
        /// </summary>
        private double m_moneyMoneyPermit;
        /// <summary>
        /// Сумма (разрешено)
        /// </summary>
        public double MoneyPermit
        {
            get { return m_moneyMoneyPermit; }
            set { m_moneyMoneyPermit = value; }
        }
        /// <summary>
        /// Сумма (факт)
        /// </summary>
        private double m_moneyMoneyFact;
        /// <summary>
        /// Сумма (факт)
        /// </summary>
        public double MoneyFact
        {
            get { return m_moneyMoneyFact; }
            set { m_moneyMoneyFact = value; }
        }
        /// <summary>
        /// Сумма (зарезервировано)
        /// </summary>
        private double m_moneyMoneyReserve;
        /// <summary>
        /// Сумма (зарезервировано)
        /// </summary>
        public double MoneyReserve
        {
            get { return m_moneyMoneyReserve; }
            set { m_moneyMoneyReserve = value; }
        }
        /// <summary>
        /// Утвержденная сумма плана в валюте бюджета
        /// </summary>
        private double m_moneyMoneyPlanAccept;
        /// <summary>
        /// Утвержденная сумма плана в валюте бюджета
        /// </summary>
        public double MoneyPlanAccept
        {
            get { return m_moneyMoneyPlanAccept; }
            set { m_moneyMoneyPlanAccept = value; }
        }
        /// <summary>
        /// Сумма поступлений по расшифровке
        /// </summary>
        private double m_moneyMoneyCredit;
        /// <summary>
        /// Сумма поступлений по расшифровке
        /// </summary>
        public double MoneyCredit
        {
            get { return m_moneyMoneyCredit; }
            set { m_moneyMoneyCredit = value; }
        }
        #endregion

        #region Конструкторы
        public CBudgetItemDecode(enumMonth Month)
        {
            m_Month = Month;
            m_objCurrency = null;
            m_Description = "";
            m_moneyMoneyFact = 0;
            m_moneyMoneyPermit = 0;
            m_moneyMoneyPlan = 0;
            m_moneyMoneyReserve = 0;
            m_moneyMoneyPlanAccept = 0;
            m_moneyMoneyCredit = 0;
        }
        public CBudgetItemDecode(enumMonth Month, CCurrency objCurrency, System.String strDescription,
            double moneyMoneyFact, double moneyMoneyPermit, double moneyMoneyPlan, double moneyMoneyReserve)
        {
            m_Month = Month;
            m_objCurrency = objCurrency;
            m_Description = strDescription;
            m_moneyMoneyFact = moneyMoneyFact;
            m_moneyMoneyPermit = moneyMoneyPermit;
            m_moneyMoneyPlan = moneyMoneyPlan;
            m_moneyMoneyReserve = moneyMoneyReserve;
            m_moneyMoneyPlanAccept = 0;
            m_moneyMoneyCredit = 0;
        }
        public CBudgetItemDecode(enumMonth Month, CCurrency objCurrency, System.String strDescription,
            double moneyMoneyFact, double moneyMoneyPermit, double moneyMoneyPlan, double moneyMoneyReserve,
            double moneyMoneyPlanAccept)
        {
            m_Month = Month;
            m_objCurrency = objCurrency;
            m_Description = strDescription;
            m_moneyMoneyFact = moneyMoneyFact;
            m_moneyMoneyPermit = moneyMoneyPermit;
            m_moneyMoneyPlan = moneyMoneyPlan;
            m_moneyMoneyReserve = moneyMoneyReserve;
            m_moneyMoneyPlanAccept = moneyMoneyPlanAccept;
            m_moneyMoneyCredit = 0;
        }
        public CBudgetItemDecode(enumMonth Month, CCurrency objCurrency, System.String strDescription,
            double moneyMoneyPlan)
        {
            m_Month = Month;
            m_objCurrency = objCurrency;
            m_Description = strDescription;
            m_moneyMoneyFact = 0;
            m_moneyMoneyPermit = 0;
            m_moneyMoneyPlan = moneyMoneyPlan;
            m_moneyMoneyReserve = 0;
            m_moneyMoneyPlanAccept = 0;
            m_moneyMoneyCredit = 0;
        }
        #endregion

        #region Сохранение информации о расшифровке в БД
        /// <summary>
        /// Преобразует тип enumMonth в тип System.Int32
        /// </summary>
        /// <param name="enMonth"></param>
        /// <returns></returns>
        private System.Int32 ConvertMonthToInt(enumMonth enMonth)
        {
            System.Int32 iRet = 0;
            try
            {
                switch (enMonth)
                {
                    case enumMonth.Jan:
                        {
                            iRet = 1;
                            break;
                        }
                    case enumMonth.Feb:
                        {
                            iRet = 2;
                            break;
                        }
                    case enumMonth.Mar:
                        {
                            iRet = 3;
                            break;
                        }
                    case enumMonth.Apr:
                        {
                            iRet = 4;
                            break;
                        }
                    case enumMonth.May:
                        {
                            iRet = 5;
                            break;
                        }
                    case enumMonth.Jun:
                        {
                            iRet = 6;
                            break;
                        }
                    case enumMonth.Jul:
                        {
                            iRet = 7;
                            break;
                        }
                    case enumMonth.Aug:
                        {
                            iRet = 8;
                            break;
                        }
                    case enumMonth.Sep:
                        {
                            iRet = 9;
                            break;
                        }
                    case enumMonth.Oct:
                        {
                            iRet = 10;
                            break;
                        }
                    case enumMonth.Nov:
                        {
                            iRet = 11;
                            break;
                        }
                    case enumMonth.Dec:
                        {
                            iRet = 12;
                            break;
                        }
                    default:
                        break;
                }
            }
            catch (System.Exception f)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(
                "Ошибка преобразования месяца в число.\nМесяц : " + enMonth.ToString() +
                "\n\nТекст ошибки: " + f.Message, "Внимание",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }

            return iRet;
        }

        /// <summary>
        /// Сохраняет информацию о расшифровке в БД
        /// </summary>
        /// <param name="cmd">SQL-команда</param>
        /// <param name="objProfile">профайл</param>
        /// <param name="uuidBudgetID">уи бюджета</param>
        /// <param name="uuidDebitArticleID">уи статьи расходов</param>
        /// <returns>true - успешное завершение; false - ошибка</returns>
        public System.Boolean SaveBudgetItemDecode(System.Data.SqlClient.SqlCommand cmd,
            UniXP.Common.CProfile objProfile, System.Guid uuidBudgetItemID)
        {
            System.Boolean bRet = false;
            if (uuidBudgetItemID.CompareTo(System.Guid.Empty) == 0)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show("Уникальный идентификатор статьи бюджета не должен быть пуст.", "Внимание",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);
                return bRet;
            }
            if (cmd == null)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show("Отсутствует соединение с БД.", "Внимание",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);
                return bRet;
            }

            try
            {
                cmd.Parameters.Clear();
                cmd.CommandText = System.String.Format("[{0}].[dbo].[sp_SaveBudgetItemDecode]", objProfile.GetOptionsDllDBName());
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ((System.Byte)(0)), ((System.Byte)(0)), "", System.Data.DataRowVersion.Current, null));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@BUDGETITEM_GUID_ID", System.Data.SqlDbType.UniqueIdentifier));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@BUDGETITEMDECODE_MONTH_ID", System.Data.SqlDbType.Int));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@BUDGETITEMDECODE_MONEYPLAN", System.Data.SqlDbType.Money));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@BUDGETITEMDECODE_DESCRIPTION", System.Data.SqlDbType.Text));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ERROR_NUMBER", System.Data.SqlDbType.Int, 8, System.Data.ParameterDirection.Output, false, ((System.Byte)(0)), ((System.Byte)(0)), "", System.Data.DataRowVersion.Current, null));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ERROR_MESSAGE", System.Data.SqlDbType.NVarChar, 4000));
                cmd.Parameters["@ERROR_MESSAGE"].Direction = System.Data.ParameterDirection.Output;
                if (this.m_objCurrency != null)
                {
                    cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@CURRENCY_GUID_ID", System.Data.SqlDbType.UniqueIdentifier));
                    cmd.Parameters["@CURRENCY_GUID_ID"].Value = this.m_objCurrency.uuidID;
                }
                cmd.Parameters["@BUDGETITEM_GUID_ID"].Value = uuidBudgetItemID;
                cmd.Parameters["@BUDGETITEMDECODE_MONTH_ID"].Value = ConvertMonthToInt(this.m_Month);
                cmd.Parameters["@BUDGETITEMDECODE_MONEYPLAN"].Value = this.m_moneyMoneyPlan;
                cmd.Parameters["@BUDGETITEMDECODE_DESCRIPTION"].Value = this.m_Description;

                cmd.ExecuteNonQuery();
                System.Int32 iRet = (System.Int32)cmd.Parameters["@RETURN_VALUE"].Value;

                if (iRet != 0)
                {
                    switch (iRet)
                    {
                        case 1:
                            {
                                DevExpress.XtraEditors.XtraMessageBox.Show(
                                "Ошибка сохранения расшифровки статьи бюджета.\n\nCтатья бюджета с заданным идентификатором не найдена: " +
                                    uuidBudgetItemID.ToString(), "Ошибка",
                                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                                break;
                            }
                        case 2:
                            {
                                DevExpress.XtraEditors.XtraMessageBox.Show(
                                "Ошибка сохранения расшифровки статьи бюджета.\n\nНеверно указан месяц: " +
                                    this.m_Month.ToString(), "Ошибка",
                                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                                break;
                            }
                        case 3:
                            {
                                DevExpress.XtraEditors.XtraMessageBox.Show(
                                "Ошибка сохранения расшифровки статьи бюджета.\n\nВалюта с заданным идентификатором не найдена: " +
                                    this.m_objCurrency.uuidID.ToString(), "Ошибка",
                                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                                break;
                            }
                        default:
                            {
                                DevExpress.XtraEditors.XtraMessageBox.Show(
                                "Ошибка сохранения расшифровки статьи бюджета.\n\nТекст ошибки: " +
                                (System.String)cmd.Parameters["@ERROR_MESSAGE"].Value, "Ошибка",
                                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                                break;
                            }
                    }
                }

                bRet = (iRet == 0);
            }
            catch (System.Exception f)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(
                "Ошибка сохранения расшифровки статьи бюджета.\nУИ статьи бюджета : " + uuidBudgetItemID.ToString() +
                "\nМесяц: " + this.m_Month.ToString() +
                "\n\nТекст ошибки: " + f.Message, "Внимание",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }

            return bRet;
        }

        /// <summary>
        /// Сохраняет информацию о расшифровке в БД
        /// </summary>
        /// <param name="cmd">SQL-команда</param>
        /// <param name="objProfile">профайл</param>
        /// <param name="uuidBudgetItemID">уи статьи бюджета</param>
        /// <param name="uuidBudgetDocID">уи бюджетного документа</param>
        /// <returns>true - успешное завершение; false - ошибка</returns>
        public System.Boolean SaveBudgetItemDecodeForBudgetDoc(System.Data.SqlClient.SqlCommand cmd,
            UniXP.Common.CProfile objProfile, System.Guid uuidBudgetItemID, System.Guid uuidBudgetDocID )
        {
            System.Boolean bRet = false;
            if (uuidBudgetItemID.CompareTo(System.Guid.Empty) == 0)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show("Уникальный идентификатор статьи бюджета не должен быть пуст.", "Внимание",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);
                return bRet;
            }
            if (cmd == null)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show("Отсутствует соединение с БД.", "Внимание",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);
                return bRet;
            }

            try
            {
                cmd.Parameters.Clear();
                cmd.CommandText = System.String.Format("[{0}].[dbo].[sp_AddBudgetDocItemDecode]", objProfile.GetOptionsDllDBName());
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ((System.Byte)(0)), ((System.Byte)(0)), "", System.Data.DataRowVersion.Current, null));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@BUDGETDOC_GUID_ID", System.Data.SqlDbType.UniqueIdentifier));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@BUDGETITEM_GUID_ID", System.Data.SqlDbType.UniqueIdentifier));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@CURRENCY_GUID_ID", System.Data.SqlDbType.UniqueIdentifier));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@MONTH_ID", System.Data.SqlDbType.Int));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@MONEY", System.Data.SqlDbType.Money));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@MONEY_IN_BUDGETCURRENCY", System.Data.SqlDbType.Money));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ERROR_NUM", System.Data.SqlDbType.Int, 8, System.Data.ParameterDirection.Output, false, ((System.Byte)(0)), ((System.Byte)(0)), "", System.Data.DataRowVersion.Current, null));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ERROR_MES", System.Data.SqlDbType.NVarChar, 4000));
                cmd.Parameters["@ERROR_MES"].Direction = System.Data.ParameterDirection.Output;

                cmd.Parameters["@CURRENCY_GUID_ID"].Value = this.m_objCurrency.uuidID;
                cmd.Parameters["@BUDGETDOC_GUID_ID"].Value = uuidBudgetDocID;
                cmd.Parameters["@BUDGETITEM_GUID_ID"].Value = uuidBudgetItemID;
                cmd.Parameters["@MONTH_ID"].Value = ConvertMonthToInt(this.m_Month);
                cmd.Parameters["@MONEY"].Value = this.m_moneyMoneyPlan;
                cmd.Parameters["@MONEY_IN_BUDGETCURRENCY"].Value = this.MoneyPlanAccept;

                cmd.ExecuteNonQuery();
                System.Int32 iRet = (System.Int32)cmd.Parameters["@RETURN_VALUE"].Value;

                if (iRet != 0)
                {
                    DevExpress.XtraEditors.XtraMessageBox.Show(
                    "Ошибка сохранения расшифровки статьи бюджета.\n\nТекст ошибки: " +
                    (System.String)cmd.Parameters["@ERROR_MES"].Value, "Ошибка",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                }

                bRet = (iRet == 0);
            }
            catch (System.Exception f)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(
                "Ошибка сохранения расшифровки статьи бюджета.\nУИ статьи бюджета : " + uuidBudgetItemID.ToString() +
                "\nУИ бюджетного документа: " + uuidBudgetDocID.ToString() +
                "\nМесяц: " + this.m_Month.ToString() +
                "\n\nТекст ошибки: " + f.Message, "Внимание",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }

            return bRet;
        }

        #endregion

    }
    /// <summary>
    /// Структура "Выполнение бюджета по статье"
    /// </summary>
    public class CBudgetItemBalans
    {
        public CDebitArticle DebitArticle;
        public double MoneyPlan;
        public double MoneyFact;
        public enumMonth Month;
    }
    /// <summary>
    /// Структура, соответствующая записи для журнала транзакций
    /// </summary>
    public struct structBudgetItemAccTrn
    {
        /// <summary>
        /// Дата транзакции
        /// </summary>
        public System.DateTime TrnDate;
        /// <summary>
        /// Сумма транзакции
        /// </summary>
        public System.Double MoneyTrn;
        /// <summary>
        /// Валюта транзакции
        /// </summary>
        public System.String CurrencyTrn;
        /// <summary>
        /// Пользователь, совершивший транзакцию
        /// </summary>
        public System.String UserTrn;
        /// <summary>
        /// Дата бюджетного документа
        /// </summary>
        public System.DateTime DocDate;
        /// <summary>
        /// Сумма бюджетного документа
        /// </summary>
        public System.Double MoneyDoc;
        /// <summary>
        /// Валюта бюджетного документа
        /// </summary>
        public System.String CurrencyDoc;
        /// <summary>
        /// Цель бюджетного документа
        /// </summary>
        public System.String ObjectiveDoc;
        /// <summary>
        /// Тип транзакции
        /// </summary>
        public System.String Operation;
    }
    /// <summary>
    /// Структура, соответствующая записи для журнала документов
    /// </summary>
    public struct structBudgetItemDoc
    {
        /// <summary>
        /// Дата 
        /// </summary>
        public System.DateTime DocDate;
        /// <summary>
        /// Сумма в валюте документа
        /// </summary>
        public System.Double DocMoney;
        /// <summary>
        /// Валюта
        /// </summary>
        public System.String Currency;
        /// <summary>
        /// Сумма
        /// </summary>
        public System.Double Money;
        /// <summary>
        /// Пользователь
        /// </summary>
        public System.String User;
        /// <summary>
        /// Цель бюджетного документа
        /// </summary>
        public System.String ObjectiveDoc;
        /// <summary>
        /// Статья бюджета
        /// </summary>
        public System.String BudgetItemFullName;
        /// <summary>
        /// Состояние документа
        /// </summary>
        public System.String DocState;
    }
    /// <summary>
    /// Класс "Статья бюджета"
    /// </summary>
    public class CBudgetItem : IBaseListItem
    {

        #region Переменные, Свойства, Константы
        /// <summary>
        /// Уникальный идентификатор бюджета
        /// </summary>
        private System.Guid m_uuidBudgetID;
        /// <summary>
        /// Уникальный идентификатор бюджета
        /// </summary>
        public System.Guid BudgetGUID
        {
            get { return m_uuidBudgetID; }
            set { m_uuidBudgetID = value; }
        }
        /// <summary>
        /// Название бюджета
        /// </summary>
        private System.String m_strBudgetName;
        /// <summary>
        /// Название бюджета
        /// </summary>
        public System.String BudgetName
        {
            get { return m_strBudgetName; }
            set { m_strBudgetName = value; }
        }
        /// <summary>
        /// Признак "Внебюджетные расходы"
        /// </summary>
        private System.Boolean m_OffExpenditures;
        /// <summary>
        /// Признак "Внебюджетные расходы"
        /// </summary>
        public System.Boolean OffExpenditures
        {
            get { return m_OffExpenditures; }
            set { m_OffExpenditures = value; }
        }
        /// <summary>
        /// УИ родительской статьи бюджета
        /// </summary>
        private System.Guid m_uuidParentID;
        /// <summary>
        /// УИ родительской статьи бюджета
        /// </summary>
        public System.Guid ParentID
        {
            get { return m_uuidParentID; }
            set { m_uuidParentID = value; }
        }
        /// <summary>
        /// Номер статьи бюджета
        /// </summary>
        private string m_strBudgetItemNum;
        /// <summary>
        /// Номер статьи бюджета
        /// </summary>
        public string BudgetItemNum
        {
            get { return m_strBudgetItemNum; }
            set { m_strBudgetItemNum = value; }
        }
        /// <summary>
        /// Описание статьи бюджета
        /// </summary>
        private string m_strBudgetItemDescription;
        /// <summary>
        /// Описание статьи бюджета
        /// </summary>
        public string BudgetItemDescription
        {
            get { return m_strBudgetItemDescription; }
            set { m_strBudgetItemDescription = value; }
        }
        /// <summary>
        /// Номер по порядке в ветке
        /// </summary>
        private System.Int32 m_iBudgetItemID;
        /// <summary>
        /// Номер по порядке в ветке
        /// </summary>
        public System.Int32 BudgetItemID
        {
            get { return m_iBudgetItemID; }
            set { m_iBudgetItemID = value; }
        }
        /// <summary>
        /// Признак "Переходящий остаток"
        /// </summary>
        private System.Boolean m_bTransprtRest;
        /// <summary>
        /// Признак "Переходящий остаток"
        /// </summary>
        public System.Boolean TransprtRest
        {
            get { return m_bTransprtRest; }
            set { m_bTransprtRest = value; }
        }
        /// <summary>
        /// Признак "Запрет нецелевого использования"
        /// </summary>
        private System.Boolean m_bDontChange;
        /// <summary>
        /// Признак "Запрет нецелевого использования"
        /// </summary>
        public System.Boolean DontChange
        {
            get { return m_bDontChange; }
            set { m_bDontChange = value; }
        }
        /// <summary>
        /// Полное наименование статьи (номер + название)
        /// </summary>
        public System.String BudgetItemFullName
        {
            get { return (m_strBudgetItemNum + " " + Name); }
        }
        /// <summary>
        /// Признак "только для чтения"
        /// </summary>
        private System.Boolean m_bReadOnly;
        /// <summary>
        /// Признак "только для чтения"
        /// </summary>
        public System.Boolean ReadOnly
        {
            get { return m_bReadOnly; }
            set { m_bReadOnly = value; }
        }
        /// <summary>
        /// Уникальный идентификатор статьи расходов
        /// </summary>
        private System.Guid m_uuidDebitArticleID;
        /// <summary>
        /// Уникальный идентификатор статьи расходов
        /// </summary>
        public System.Guid DebitArticleID
        {
            get { return m_uuidDebitArticleID; }
            set { m_uuidDebitArticleID = value; }
        }
        /// <summary>
        /// Сумма, которую нужно списать со статьи бюджета в валюте бюджетного документа ( для бюджетного документа )
        /// </summary>
        private System.Double m_MoneyInBudgetDocCurrency;
        /// <summary>
        /// Сумма, которую нужно списать со статьи бюджета в валюте бюджетного документа ( для бюджетного документа )
        /// </summary>
        public System.Double MoneyInBudgetDocCurrency
        {
            get { return m_MoneyInBudgetDocCurrency; }
            set { m_MoneyInBudgetDocCurrency = value; }
        }
        /// <summary>
        /// Сумма, которую нужно списать со статьи бюджета в валюте бюджета ( для бюджетного документа )
        /// </summary>
        private System.Double m_MoneyInBudgetCurrency;
        /// <summary>
        /// Сумма, которую нужно списать со статьи бюджета в валюте бюджета ( для бюджетного документа )
        /// </summary>
        public System.Double MoneyInBudgetCurrency
        {
            get { return m_MoneyInBudgetCurrency; }
            set { m_MoneyInBudgetCurrency = value; }
        }
        /// <summary>
        /// Остаток средств по статье ( для бюджетного документа )
        /// </summary>
        private System.Double m_RestMoney;
        /// <summary>
        /// Остаток средств по статье ( для бюджетного документа )
        /// </summary>
        public System.Double RestMoney
        {
            get { return m_RestMoney; }
            set { m_RestMoney = value; }
        }
        /// <summary>
        /// Список расшифровок статьи бюджета
        /// </summary>
        private System.Collections.ArrayList m_BudgetItemDecodeList;
        /// <summary>
        /// Список расшифровок статьи бюджета
        /// </summary>
        public System.Collections.ArrayList BudgetItemDecodeList
        {
            get { return m_BudgetItemDecodeList; }
        }
        /// <summary>
        /// Расшифровка за январь
        /// </summary>
        public CBudgetItemDecode JanuaryDecode
        {
            get { return GetBudgetItemDecode(enumMonth.Jan); }
        }
        /// <summary>
        /// Расшифровка за февраль
        /// </summary>
        public CBudgetItemDecode FebruaryDecode
        {
            get { return GetBudgetItemDecode(enumMonth.Feb); }
        }
        /// <summary>
        /// Расшифровка за март
        /// </summary>
        public CBudgetItemDecode MarchDecode
        {
            get { return GetBudgetItemDecode(enumMonth.Mar); }
        }
        /// <summary>
        /// Расшифровка за апрель
        /// </summary>
        public CBudgetItemDecode AprilDecode
        {
            get { return GetBudgetItemDecode(enumMonth.Apr); }
        }
        /// <summary>
        /// Расшифровка за май
        /// </summary>
        public CBudgetItemDecode MayDecode
        {
            get { return GetBudgetItemDecode(enumMonth.May); }
        }
        /// <summary>
        /// Расшифровка за июнь
        /// </summary>
        public CBudgetItemDecode JuneDecode
        {
            get { return GetBudgetItemDecode(enumMonth.Jun); }
        }
        /// <summary>
        /// Расшифровка за июль
        /// </summary>
        public CBudgetItemDecode JulyDecode
        {
            get { return GetBudgetItemDecode(enumMonth.Jul); }
        }
        /// <summary>
        /// Расшифровка за август
        /// </summary>
        public CBudgetItemDecode AugustDecode
        {
            get { return GetBudgetItemDecode(enumMonth.Aug); }
        }
        /// <summary>
        /// Расшифровка за сентябрь
        /// </summary>
        public CBudgetItemDecode SeptemberDecode
        {
            get { return GetBudgetItemDecode(enumMonth.Sep); }
        }
        /// <summary>
        /// Расшифровка за октябрь
        /// </summary>
        public CBudgetItemDecode OctoberDecode
        {
            get { return GetBudgetItemDecode(enumMonth.Oct); }
        }
        /// <summary>
        /// Расшифровка за ноябрь
        /// </summary>
        public CBudgetItemDecode NovemberDecode
        {
            get { return GetBudgetItemDecode(enumMonth.Nov); }
        }
        /// <summary>
        /// Расшифровка за декабрь
        /// </summary>
        public CBudgetItemDecode DecemberDecode
        {
            get { return GetBudgetItemDecode(enumMonth.Dec); }
        }
        /// <summary>
        /// Признак "тип бюджетных расходов" используется только для привязки "статья - служба - тип расходов"
        /// </summary>
        private CBudgetExpenseType m_objBudgetExpenseType;
        /// <summary>
        /// Признак "тип бюджетных расходов" используется только для привязки "статья - служба - тип расходов"
        /// </summary>
        public CBudgetExpenseType BudgetExpenseType
        {
            get { return m_objBudgetExpenseType; }
            set { m_objBudgetExpenseType = value; }
        }
        private System.Boolean m_bIsDefficit;
        public System.Boolean IsDefficit
        {
            get { return m_bIsDefficit; }
            set { m_bIsDefficit = value; }
        }
        /// <summary>
        /// Счёт
        /// </summary>
        public CAccountPlan AccountPlan { get; set; }
        /// <summary>
        /// Проект
        /// </summary>
        public CBudgetProject BudgetProject { get; set; }
        /// <summary>
        /// Тип бюджета
        /// </summary>
        public CBudgetType BudgetType { get; set; }
        #endregion

        #region Конструкторы *
        public CBudgetItem()
        {
            this.m_strName = "";
            this.m_strBudgetItemNum = "";
            this.m_strBudgetItemDescription = "";
            this.m_uuidID = System.Guid.Empty;
            this.m_uuidParentID = System.Guid.Empty;
            this.m_uuidBudgetID = System.Guid.Empty;
            this.m_strBudgetName = "";
            this.m_OffExpenditures = false;
            this.m_uuidDebitArticleID = System.Guid.Empty;
            this.m_bReadOnly = false;
            this.m_bTransprtRest = false;
            this.m_bDontChange = false;
            this.m_iBudgetItemID = 0;
            this.m_MoneyInBudgetCurrency = 0;
            this.m_MoneyInBudgetDocCurrency = 0;
            this.m_RestMoney = 0;
            this.m_BudgetItemDecodeList = new System.Collections.ArrayList();
            m_objBudgetExpenseType = null;
            m_bIsDefficit = false;
            AccountPlan = null;
            BudgetProject = null;
            BudgetType = null;

            FillDecodeList();
        }

        #endregion

        #region Copy  *
        /// <summary>
        /// Создает объект "Статья бюджета" на основе другого объекта
        /// </summary>
        /// <param name="objBudgetItemSrc">копируемый объект "Статья бюджета"</param>
        /// <returns>объект "Статья бюджета"</returns>
        public static CBudgetItem Copy(CBudgetItem objBudgetItemSrc)
        {
            CBudgetItem objBudgetItem = new CBudgetItem();

            try
            {
                objBudgetItem.uuidID = objBudgetItemSrc.uuidID;
                objBudgetItem.Name = objBudgetItemSrc.Name;
                objBudgetItem.m_bDontChange = objBudgetItemSrc.m_bDontChange;
                objBudgetItem.m_bTransprtRest = objBudgetItemSrc.m_bTransprtRest;
                objBudgetItem.m_iBudgetItemID = objBudgetItemSrc.m_iBudgetItemID;
                objBudgetItem.m_strBudgetItemDescription = objBudgetItemSrc.m_strBudgetItemDescription;
                objBudgetItem.m_strBudgetItemNum = objBudgetItemSrc.m_strBudgetItemNum;
                objBudgetItem.m_uuidBudgetID = objBudgetItemSrc.m_uuidBudgetID;
                objBudgetItem.m_strBudgetName = objBudgetItemSrc.m_strBudgetName;
                objBudgetItem.m_OffExpenditures = objBudgetItemSrc.m_OffExpenditures;
                objBudgetItem.m_uuidDebitArticleID = objBudgetItemSrc.m_uuidDebitArticleID;
                objBudgetItem.m_uuidParentID = objBudgetItemSrc.m_uuidParentID;
                objBudgetItem.BudgetExpenseType = objBudgetItemSrc.BudgetExpenseType;
                objBudgetItem.AccountPlan = objBudgetItemSrc.AccountPlan;
                objBudgetItem.BudgetProject = objBudgetItemSrc.BudgetProject;
                objBudgetItem.BudgetType = objBudgetItemSrc.BudgetType;
            }
            catch (System.Exception f)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(
                "Не удалось скопировать свойства объекта \"Статья бюджета\".\n\nТекст ошибки: " + f.Message, "Внимание",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
            return objBudgetItem;
        }
        /// <summary>
        /// Копирует список расшифровок
        /// </summary>
        /// <param name="objBudgetItem">статья бюджета</param>
        public void CopyDecodeList(CBudgetItem objBudgetItem)
        {
            if ((objBudgetItem == null) || (objBudgetItem.m_BudgetItemDecodeList == null)) { return; }
            try
            {
                foreach (CBudgetItemDecode objBudgetItemDecode in objBudgetItem.m_BudgetItemDecodeList)
                {
                    CBudgetItemDecode objNewBudgetItemDecode = new CBudgetItemDecode(objBudgetItemDecode.Month);
                    if (objBudgetItemDecode.Currency != null)
                    {
                        objNewBudgetItemDecode.Currency = new CCurrency(objBudgetItemDecode.Currency.uuidID,
                           objBudgetItemDecode.Currency.CurrencyCode, objBudgetItemDecode.Currency.Name);
                    }
                    objNewBudgetItemDecode.Description = objBudgetItemDecode.Description;
                    objNewBudgetItemDecode.MoneyPlan = objBudgetItemDecode.MoneyPlan;
                    objNewBudgetItemDecode.MoneyFact = objBudgetItemDecode.MoneyFact;
                    objNewBudgetItemDecode.MoneyPermit = objBudgetItemDecode.MoneyPermit;
                    objNewBudgetItemDecode.MoneyPlanAccept = objBudgetItemDecode.MoneyPlanAccept;
                    objNewBudgetItemDecode.MoneyReserve = objBudgetItemDecode.MoneyReserve;

                    this.AddBudgetItemDecode(objNewBudgetItemDecode);
                }
            }
            catch (System.Exception f)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(
                "Ошибка копирования списка расшифровок.\n\nТекст ошибки: " + f.Message, "Внимание",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }

            return;
        }
        /// <summary>
        /// Копирует неутвержденные плановые показатели заданной статьи бюджета
        /// </summary>
        /// <param name="objBudgetItem">статья бюджета</param>
        public void CopyDecodeListPlan(CBudgetItem objBudgetItem)
        {
            if ((objBudgetItem == null) || (objBudgetItem.m_BudgetItemDecodeList == null)) { return; }
            try
            {
                foreach (CBudgetItemDecode objBudgetItemDecode in objBudgetItem.m_BudgetItemDecodeList)
                {
                    CBudgetItemDecode objNewBudgetItemDecode = new CBudgetItemDecode(objBudgetItemDecode.Month);
                    if (objBudgetItemDecode.Currency != null)
                    {
                        objNewBudgetItemDecode.Currency = new CCurrency(objBudgetItemDecode.Currency.uuidID,
                           objBudgetItemDecode.Currency.CurrencyCode, objBudgetItemDecode.Currency.Name);
                        objNewBudgetItemDecode.MoneyPlan = objBudgetItemDecode.MoneyPlan;
                    }

                    this.AddBudgetItemDecode(objNewBudgetItemDecode);
                }
            }
            catch (System.Exception f)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(
                "Ошибка копирования списка плановых показателей статьи бюджета.\n\nТекст ошибки: " + f.Message, "Внимание",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }

            return;
        }
        #endregion

        #region Список расшифровок статьи бюджета *
        /// <summary>
        /// Очищает список расшифровок статьи бюджета
        /// </summary>
        public void ClearDecodeList()
        {
            try
            {
                if ((m_BudgetItemDecodeList != null) && (m_BudgetItemDecodeList.Count > 0))
                {
                    m_BudgetItemDecodeList.Clear();
                }
            }
            catch (System.Exception f)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(
                "Не удалось очистить список расшифровок статьи бюджета.\n" + f.Message, "Внимание",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }

            return;
        }
        /// <summary>
        /// Очищает плановые показатели в списке расшифровок статьи бюджета
        /// </summary>
        public void ClearPlanInDecodeList()
        {
            try
            {
                if ((m_BudgetItemDecodeList != null) && (m_BudgetItemDecodeList.Count > 0))
                {
                    for (System.Int32 i = 0; i < m_BudgetItemDecodeList.Count; i++)
                    {
                        ((CBudgetItemDecode)m_BudgetItemDecodeList[i]).Currency = null;
                        ((CBudgetItemDecode)m_BudgetItemDecodeList[i]).MoneyPlan = 0;
                    }
                }
            }
            catch (System.Exception f)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(
                "Не удалось очистить плановые показатели в списке расшифровок статьи бюджета.\n\nТекст ошибки: " + f.Message, "Внимание",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }

            return;
        }
        /// <summary>
        /// заполняет список расшифровок статьи бюджета "пустышками"
        /// </summary>
        public void FillDecodeList()
        {
            try
            {
                if (m_BudgetItemDecodeList == null) { return; }

                m_BudgetItemDecodeList.Clear();
                m_BudgetItemDecodeList.Add(new CBudgetItemDecode(enumMonth.Jan));
                m_BudgetItemDecodeList.Add(new CBudgetItemDecode(enumMonth.Feb));
                m_BudgetItemDecodeList.Add(new CBudgetItemDecode(enumMonth.Mar));
                m_BudgetItemDecodeList.Add(new CBudgetItemDecode(enumMonth.Apr));
                m_BudgetItemDecodeList.Add(new CBudgetItemDecode(enumMonth.May));
                m_BudgetItemDecodeList.Add(new CBudgetItemDecode(enumMonth.Jun));
                m_BudgetItemDecodeList.Add(new CBudgetItemDecode(enumMonth.Jul));
                m_BudgetItemDecodeList.Add(new CBudgetItemDecode(enumMonth.Aug));
                m_BudgetItemDecodeList.Add(new CBudgetItemDecode(enumMonth.Sep));
                m_BudgetItemDecodeList.Add(new CBudgetItemDecode(enumMonth.Oct));
                m_BudgetItemDecodeList.Add(new CBudgetItemDecode(enumMonth.Nov));
                m_BudgetItemDecodeList.Add(new CBudgetItemDecode(enumMonth.Dec));
            }
            catch (System.Exception f)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(
                "Не заполнить список расшифровок статьи бюджета.\n\nТекст ошибки: " + f.Message, "Внимание",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }

            return;
        }
        /// <summary>
        /// Возвращает объект "Расшифровка статьи бюджета" для указанного месяца
        /// </summary>
        /// <param name="Month">Месяц</param>
        /// <returns>объект "Расшифровка статьи бюджета"</returns>
        public CBudgetItemDecode GetBudgetItemDecode(enumMonth Month)
        {
            CBudgetItemDecode objRet = null;
            try
            {
                if (m_BudgetItemDecodeList.Count > 0)
                {
                    // имеет смысл искать объект в непустом списке
                    for (System.Int32 i = 0; i < m_BudgetItemDecodeList.Count; i++)
                    {
                        // нужен объект с месяцем Month
                        if (((CBudgetItemDecode)m_BudgetItemDecodeList[i]).Month == Month)
                        {
                            objRet = ((CBudgetItemDecode)m_BudgetItemDecodeList[i]);
                            break;
                        }
                    }
                }
            }
            catch (System.Exception f)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(
                "Не удалось получить расшифровку статьи бюджета.\n" + f.Message, "Внимание",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }

            return objRet;
        }

        public System.String GetBudgetItemDecodeInfo(enumMonth Month)
        {
            System.String strRet = "";
            try
            {
                if (m_BudgetItemDecodeList.Count > 0)
                {
                    // имеет смысл искать объект в непустом списке
                    for (System.Int32 i = 0; i < m_BudgetItemDecodeList.Count; i++)
                    {
                        // нужен объект с месяцем Month
                        if (((CBudgetItemDecode)m_BudgetItemDecodeList[i]).Month == Month)
                        {
                            if (((CBudgetItemDecode)m_BudgetItemDecodeList[i]).Currency != null)
                            {
                                strRet = ((CBudgetItemDecode)m_BudgetItemDecodeList[i]).MoneyPlan.ToString() + " " +
                                ((CBudgetItemDecode)m_BudgetItemDecodeList[i]).Currency.CurrencyCode + " " +
                                 ((CBudgetItemDecode)m_BudgetItemDecodeList[i]).Description;
                            }
                            break;
                        }
                    }
                }
            }
            catch (System.Exception f)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(
                "Не удалось получить расшифровку статьи бюджета.\n" + f.Message, "Внимание",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }

            return strRet;
        }

        /// <summary>
        /// Добавляет объект "Расшифровка статьи бюджета" в список
        /// </summary>
        /// <param name="objBudgetItemDecode">объект "Расшифровка статьи бюджета"</param>
        public void AddBudgetItemDecode(CBudgetItemDecode objBudgetItemDecode)
        {
            try
            {
                if (m_BudgetItemDecodeList == null)
                {
                    DevExpress.XtraEditors.XtraMessageBox.Show(
                    "Ошибка\n\nСписок расшифровок не проинициализирован!", "Внимание",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    return;
                }
                // попробуем найти объект с месяцем, совпадающим с месяцем объекта objBudgetItemDecode
                System.Boolean bFind = false;
                if (m_BudgetItemDecodeList.Count > 0)
                {
                    for (System.Int32 i = 0; i < m_BudgetItemDecodeList.Count; i++)
                    {
                        if (((CBudgetItemDecode)m_BudgetItemDecodeList[i]).Month == objBudgetItemDecode.Month)
                        {
                            // объект найден, изменяем его свойства
                            ((CBudgetItemDecode)m_BudgetItemDecodeList[i]).Currency = objBudgetItemDecode.Currency;
                            ((CBudgetItemDecode)m_BudgetItemDecodeList[i]).Description = objBudgetItemDecode.Description;
                            ((CBudgetItemDecode)m_BudgetItemDecodeList[i]).MoneyFact = objBudgetItemDecode.MoneyFact;
                            ((CBudgetItemDecode)m_BudgetItemDecodeList[i]).MoneyPermit = objBudgetItemDecode.MoneyPermit;
                            ((CBudgetItemDecode)m_BudgetItemDecodeList[i]).MoneyPlan = objBudgetItemDecode.MoneyPlan;
                            ((CBudgetItemDecode)m_BudgetItemDecodeList[i]).MoneyReserve = objBudgetItemDecode.MoneyReserve;
                            ((CBudgetItemDecode)m_BudgetItemDecodeList[i]).MoneyPlanAccept = objBudgetItemDecode.MoneyPlanAccept;
                            ((CBudgetItemDecode)m_BudgetItemDecodeList[i]).MoneyCredit = objBudgetItemDecode.MoneyCredit;
                            bFind = true;
                            break;
                        }
                    }
                }
                // такого объекта в списке нет, добавлеяем объект в список
                if (bFind == false)
                {
                    m_BudgetItemDecodeList.Add(objBudgetItemDecode);
                }
            }
            catch (System.Exception f)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(
                "Не удалось добавить в список расшифровку статьи бюджета.\n" + f.Message, "Внимание",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }

            return;
        }
        /// <summary>
        /// Удаляет объект "Расшифровка статьи бюджета" из списка
        /// </summary>
        /// <param name="Month">Месяц</param>
        public void DeleteBudgetItemDecode(enumMonth Month)
        {
            try
            {
                if (m_BudgetItemDecodeList.Count > 0)
                {
                    for (System.Int32 i = 0; i < m_BudgetItemDecodeList.Count; i++)
                    {
                        if (((CBudgetItemDecode)m_BudgetItemDecodeList[i]).Month == Month)
                        {
                            // объект найден, удаляем его
                            m_BudgetItemDecodeList.RemoveAt(i);
                            break;
                        }
                    }
                }
            }
            catch (System.Exception f)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(
                "Не удалось удалить из списка расшифровку статьи бюджета.\n" + f.Message, "Внимание",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }

            return;
        }
        public double GetMoneyAgreeSum()
        {
            double dRet = 0;
            try
            {
                if ((m_BudgetItemDecodeList != null) && (m_BudgetItemDecodeList.Count > 0))
                {
                    for (System.Int32 i = 0; i < m_BudgetItemDecodeList.Count; i++)
                    {
                        dRet += ((CBudgetItemDecode)m_BudgetItemDecodeList[i]).MoneyPlanAccept;
                    }
                }
            }
            catch (System.Exception f)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(
                "GetMoneyAgreeSum.\n\nТекст ошибки: " + f.Message, "Внимание",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }

            return dRet;
        }
        public double GetMoneyPlanSum()
        {
            double dRet = 0;
            try
            {
                if ((m_BudgetItemDecodeList != null) && (m_BudgetItemDecodeList.Count > 0))
                {
                    for (System.Int32 i = 0; i < m_BudgetItemDecodeList.Count; i++)
                    {
                        dRet += ((CBudgetItemDecode)m_BudgetItemDecodeList[i]).MoneyPlan;
                    }
                }
            }
            catch (System.Exception f)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(
                "GetMoneyAgreeSum.\n\nТекст ошибки: " + f.Message, "Внимание",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }

            return dRet;
        }
        #endregion

        #region Сохранение списка расшифровок в БД *
        /// <summary>
        /// Сохраняет расшифровки статьи бюджета в БД
        /// </summary>
        /// <param name="objProfile">профайл</param>
        /// <returns>true - удачное завершение; false - ошибка</returns>
        public System.Boolean SaveBudgetItem( UniXP.Common.CProfile objProfile)
        {
            System.Boolean bRet = false;

            if ((this.m_BudgetItemDecodeList == null) || (this.m_BudgetItemDecodeList.Count == 0))
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(
                    "Ошибка сохранения списка расшифровок по статье бюджета.\nСписок расшифровок пуст.\nСтатья: " +
                    this.BudgetItemFullName, "Ошибка",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                return bRet;
            }

            System.Data.SqlClient.SqlConnection DBConnection = objProfile.GetDBSource();
            if( DBConnection == null ) { return bRet; }

            System.Data.SqlClient.SqlTransaction DBTransaction = DBConnection.BeginTransaction();
            System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
            cmd.Connection = DBConnection;
            cmd.Transaction = DBTransaction;
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            try
            {
                // нужно пройтись по списку расшифровок и сохранить его в БД
                bRet = true;
                foreach (CBudgetItemDecode objBudgetItemDecode in this.m_BudgetItemDecodeList)
                {
                    bRet = objBudgetItemDecode.SaveBudgetItemDecode(cmd, objProfile, this.uuidID);
                    if (bRet == false) { break; }
                }
                if ( bRet == true )
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
            catch (System.Exception f)
            {
                // откатываем транзакцию
                DBTransaction.Rollback();
                DevExpress.XtraEditors.XtraMessageBox.Show(
                "Ошибка сохранения списка расшифровок по статье бюджета.\nСтатья: " +
                this.BudgetItemFullName + ".\n\nТекст ошибки: " + f.Message, "Внимание",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
            finally
            {
                DBConnection.Close();
            }
            return bRet;
        }
        /// <summary>
        /// Сохраняет расшифровки статьи бюджета в БД
        /// </summary>
        /// <param name="objProfile">профайл</param>
        /// <param name="cmd">SQL-команда</param>
        /// <returns>true - удачное завершение; false - ошибка</returns>
        public System.Boolean SaveBudgetItem(System.Data.SqlClient.SqlCommand cmd,
            UniXP.Common.CProfile objProfile)
        {
            System.Boolean bRet = false;

            if (cmd == null) { return bRet; }

            if ((this.m_BudgetItemDecodeList == null) || (this.m_BudgetItemDecodeList.Count == 0))
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(
                    "Ошибка сохранения списка расшифровок по статье бюджета.\nСписок расшифровок пуст.\nСтатья: " +
                    this.BudgetItemFullName, "Ошибка",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                return bRet;
            }

            try
            {
                // нужно пройтись по списку расшифровок и сохранить его в БД
                bRet = true;
                foreach (CBudgetItemDecode objBudgetItemDecode in this.m_BudgetItemDecodeList)
                {
                    bRet = objBudgetItemDecode.SaveBudgetItemDecode(cmd, objProfile, this.uuidID);
                    if (bRet == false) { break; }
                }

            }
            catch (System.Exception f)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(
                "Ошибка сохранения списка расшифровок по статье бюджета.\nСтатья: " +
                this.BudgetItemFullName + ".\n\nТекст ошибки: " + f.Message, "Внимание",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }

            return bRet;
        }

        /// <summary>
        /// Сохраняет расшифровки бюджетного документа в БД
        /// </summary>
        /// <param name="objProfile">профайл</param>
        /// <param name="cmd">SQL-команда</param>
        /// <returns>true - удачное завершение; false - ошибка</returns>
        public System.Boolean SaveBudgetItemDecodeListForBudgetDoc(System.Data.SqlClient.SqlCommand cmd,
            UniXP.Common.CProfile objProfile, System.Guid uuidBudgetDocID)
        {
            System.Boolean bRet = false;

            if (cmd == null) { return bRet; }

            if ((this.m_BudgetItemDecodeList == null) || (this.m_BudgetItemDecodeList.Count == 0))
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(
                    "Ошибка сохранения списка расшифровок для бюджетного документа.\nСписок расшифровок пуст.\nСтатья: " +
                    this.BudgetItemFullName, "Ошибка",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                return bRet;
            }

            try
            {
                // нужно пройтись по списку расшифровок и сохранить его в БД
                bRet = true;
                foreach (CBudgetItemDecode objBudgetItemDecode in this.m_BudgetItemDecodeList)
                {
                    bRet = objBudgetItemDecode.SaveBudgetItemDecodeForBudgetDoc(cmd, objProfile, this.uuidID, uuidBudgetDocID);
                    if (bRet == false) { break; }
                }

            }
            catch (System.Exception f)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(
                "Ошибка сохранения списка расшифровок для бюджетного документа.\nСтатья: " +
                this.BudgetItemFullName + ".\n\nТекст ошибки: " + f.Message, "Внимание",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }

            return bRet;
        }
        #endregion

        #region Init *
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
            if (DBConnection == null)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show("Отсутствует соединение с БД.", "Внимание",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);
                return bRet;
            }
            try
            {
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
                cmd.Connection = DBConnection;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = System.String.Format("[{0}].[dbo].[sp_GetBudgetItem]", objProfile.GetOptionsDllDBName());
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ((System.Byte)(0)), ((System.Byte)(0)), "", System.Data.DataRowVersion.Current, null));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@GUID_ID", System.Data.SqlDbType.UniqueIdentifier));
                cmd.Parameters["@GUID_ID"].Value = uuidID;
                System.Data.SqlClient.SqlDataReader rs = cmd.ExecuteReader();
                if (rs.HasRows)
                {
                    // набор данных непустой
                    rs.Read();
                    this.m_uuidID = (System.Guid)rs["GUID_ID"];
                    this.m_uuidBudgetID = (System.Guid)rs["BUDGET_GUID_ID"];
                    this.BudgetName = System.Convert.ToString(rs["BUDGET_NAME"]);
                    if (rs["DEBITARTICLE_GUID_ID"] != System.DBNull.Value)
                    {
                        this.m_uuidDebitArticleID = (System.Guid)rs["DEBITARTICLE_GUID_ID"];
                    }
                    if (rs["PARENT_GUID_ID"] != System.DBNull.Value)
                    {
                        this.m_uuidParentID = (System.Guid)rs["PARENT_GUID_ID"];
                    }
                    if (rs["BUDGETITEM_DESCRIPTION"] != System.DBNull.Value)
                    {
                        this.m_strBudgetItemDescription = (System.String)rs["BUDGETITEM_DESCRIPTION"];
                    }
                    this.m_strBudgetItemNum = (System.String)rs["BUDGETITEM_NUM"];
                    this.Name = (System.String)rs["BUDGETITEM_NAME"];
                    this.m_bTransprtRest = (System.Boolean)rs["BUDGETITEM_TRANSPORTREST"];
                    this.m_bDontChange = (System.Boolean)rs["BUDGETITEM_DONTCHANGE"];
                    this.m_iBudgetItemID = (System.Int32)rs["BUDGETITEM_ID"];

                    this.AccountPlan = ((rs["ACCOUNTPLAN_GUID"] != System.DBNull.Value) ? new CAccountPlan()
                    {
                        uuidID = (System.Guid)rs["ACCOUNTPLAN_GUID"],
                        Name = System.Convert.ToString(rs["ACCOUNTPLAN_NAME"]),
                        IsActive = System.Convert.ToBoolean(rs["ACCOUNTPLAN_ACTIVE"]),
                        CodeIn1C = System.Convert.ToString(rs["ACCOUNTPLAN_1C_CODE"])
                    } : null);

                    this.BudgetExpenseType = ((rs["BUDGETEXPENSETYPE_GUID"] != System.DBNull.Value) ? new CBudgetExpenseType() 
                    { 
                        uuidID = (System.Guid)rs["BUDGETEXPENSETYPE_GUID"], 
                        Name = System.Convert.ToString( rs["BUDGETEXPENSETYPE_NAME"] ), 
                        CodeIn1C = System.Convert.ToInt32( rs["BUDGETEXPENSETYPE_1C_CODE"] ), 
                        IsActive = System.Convert.ToBoolean(rs["BUDGETEXPENSETYPE_ACTIVE"]) 
                    } : null);

                    this.BudgetProject = ( (rs["BUDGETPROJECT_GUID"] != System.DBNull.Value) ? new CBudgetProject()
                        {
                            uuidID = (System.Guid)rs["BUDGETPROJECT_GUID"],
                            Name = System.Convert.ToString(rs["BUDGETPROJECT_NAME"]),
                            Description = "",
                            IsActive = System.Convert.ToBoolean(rs["BUDGETPROJECT_ACTIVE"]),
                            CodeIn1C = System.Convert.ToInt32(rs["BUDGETPROJECT_1C_CODE"])
                        } : null );

                    this.BudgetType = ( (rs["BUDGETTYPE_GUID"] != System.DBNull.Value) ? new CBudgetType()
                        {
                            uuidID = (System.Guid)rs["BUDGETTYPE_GUID"],
                            Name = System.Convert.ToString(rs["BUDGETTYPE_NAME"]),
                            Description = ((rs["BUDGETTYPE_DESCRIPTION"] == System.DBNull.Value) ? "" : (System.String)rs["BUDGETTYPE_DESCRIPTION"]),
                            IsActive = System.Convert.ToBoolean(rs["BUDGETTYPE_ACTIVE"])
                        } : null);


                    bRet = true;
                }
                rs.Close();
                rs.Dispose();
                cmd.Dispose();
            }
            catch (System.Exception f)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(
                "Не удалось получить информацию о статье бюджета.\n\nТекст ошибки: " + f.Message, "Внимание",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
            finally // очищаем занимаемые ресурсы
            {
                DBConnection.Close();
            }

            return bRet;
        }

        public System.Boolean LoadBudgetItemDecode(UniXP.Common.CProfile objProfile)
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
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
                cmd.Connection = DBConnection;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                // загружаем список расшифровок
                if (this.m_uuidID.CompareTo(System.Guid.Empty) != 0)
                {
                    bRet = this.InitBudgetItem(objProfile, cmd);
                }
                else
                {
                    bRet = true;
                }

                cmd.Dispose();
            }
            catch (System.Exception f)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(
                "Не удалось получить информацию о статье бюджета.\n\nТекст ошибки: " + f.Message, "Внимание",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
            finally // очищаем занимаемые ресурсы
            {
                DBConnection.Close();
            }

            return bRet;
        }

        #endregion

        #region Remove *
        /// <summary>
        /// Удалить запись из БД
        /// </summary>
        /// <param name="objProfile">профайл</param>
        /// <param name="uuidID">уникальный идентификатор объекта</param>
        /// <returns>true - удачное завершение; false - ошибка</returns>
        public override System.Boolean Remove(UniXP.Common.CProfile objProfile)
        {
            System.Boolean bRet = false;

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
                // соединение с БД получено, прописываем команду на создание записи в БД
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
                cmd.Connection = DBConnection;
                cmd.Transaction = DBTransaction;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                // собственно процесс удаления статьи в БД
                bRet = this.Remove(cmd, objProfile);
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
                cmd.Dispose();
            }
            catch (System.Exception f)
            {
                // откатываем транзакцию
                DBTransaction.Rollback();
                DevExpress.XtraEditors.XtraMessageBox.Show(
                "Не удалось удалить статью бюджета:" + this.BudgetItemFullName +
                "\n\nТекст ошибки: " + f.Message, "Внимание",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
			finally // очищаем занимаемые ресурсы
            {
                DBConnection.Close();
            }

            return bRet;
        }

        /// <summary>
        /// Удалить запись из БД
        /// </summary>
        /// <param name="objProfile">профайл</param>
        /// <param name="cmd">SQL-команда</param>
        /// <returns>true - удачное завершение; false - ошибка</returns>
        public System.Boolean Remove(System.Data.SqlClient.SqlCommand cmd, UniXP.Common.CProfile objProfile)
        {
            System.Boolean bRet = false;
            if (cmd == null) { return bRet; }

            // уникальный идентификатор не должен быть пустым
            if (this.m_uuidID == System.Guid.Empty)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show("Недопустимое значение уникального идентификатора объекта", "Внимание",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                return bRet;
            }

            try
            {
                // соединение с БД получено, прописываем команду на удаление данных
                cmd.Parameters.Clear();
                cmd.CommandText = System.String.Format("[{0}].[dbo].[sp_DeleteBudgetItem]", objProfile.GetOptionsDllDBName());
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ((System.Byte)(0)), ((System.Byte)(0)), "", System.Data.DataRowVersion.Current, null));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@BUDGETITEM_GUID_ID", System.Data.SqlDbType.UniqueIdentifier));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ERROR_NUMBER", System.Data.SqlDbType.Int, 8, System.Data.ParameterDirection.Output, false, ((System.Byte)(0)), ((System.Byte)(0)), "", System.Data.DataRowVersion.Current, null));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ERROR_MESSAGE", System.Data.SqlDbType.NVarChar, 4000));
                cmd.Parameters["@ERROR_MESSAGE"].Direction = System.Data.ParameterDirection.Output;
                cmd.Parameters["@BUDGETITEM_GUID_ID"].Value = this.m_uuidID;
                cmd.ExecuteNonQuery();
                System.Int32 iRet = (System.Int32)cmd.Parameters["@RETURN_VALUE"].Value;
                if (iRet != 0)
                {
                    switch (iRet)
                    {
                        case 1:
                            {
                                DevExpress.XtraEditors.XtraMessageBox.Show("Удаление статьи бюджета невозможно.\nСтатья бюджета с заданным идентификатором не найдена.\nУИ: " +
                                    this.m_uuidID.ToString(), "Внимание",
                                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                                break;
                            }
                        case 2:
                            {
                                DevExpress.XtraEditors.XtraMessageBox.Show("Удаление статьи бюджета невозможно.\nСтатья бюджета связана с бюджетным документом.", "Внимание",
                                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                                break;
                            }
                        case 3:
                            {
                                DevExpress.XtraEditors.XtraMessageBox.Show("Удаление статьи бюджета невозможно.\nСтатья бюджета связана с расшифровками, которые удалять нельзя.", "Внимание",
                                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                                break;
                            }
                        default:
                            {
                                DevExpress.XtraEditors.XtraMessageBox.Show("Ошибка удаления статьи бюджета.\n\nТекст ошибки: " +
                                        (System.String)cmd.Parameters["@ERROR_MESSAGE"].Value, "Внимание",
                                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                                break;
                            }
                    }
                }
                bRet = (iRet == 0);
            }
            catch (System.Exception f)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(
                "Не удалось удалить статью бюджета: " + this.BudgetItemFullName + "\n\nТекст ошибки: " + f.Message, "Внимание",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
            return bRet;
        }
        #endregion

        #region Add *
        /// <summary>
        /// Проверяет правильность заполнения обязательных значений
        /// </summary>
        /// <returns>true - все в порядке; false - неверные значения </returns>
        public System.Boolean IsValidateProperties()
        {
            System.Boolean bRes = false;
            try
            {
                // наименование не должен быть пустым
                if (this.Name == "")
                {
                    DevExpress.XtraEditors.XtraMessageBox.Show("Недопустимое значение наименования статьи бюджета.", "Внимание",
                        System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    return bRes;
                }
                if (this.m_strBudgetItemNum == "")
                {
                    DevExpress.XtraEditors.XtraMessageBox.Show("Недопустимое значение номера статьи бюджета.", "Внимание",
                        System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    return bRes;
                }
                //if (this.AccountPlan == null)
                //{
                //    DevExpress.XtraEditors.XtraMessageBox.Show("Необходимо указать счёт.", "Внимание",
                //        System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                //    return bRes;
                //}
                bRes = true;
            }
            catch (System.Exception f)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(
                "Ошибка проверки свойств статьи бюджета.\n\nТекст ошибки: " + f.Message, "Внимание",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
            return bRes;
        }
        /// <summary>
        /// Добавить запись в БД
        /// </summary>
        /// <param name="objProfile">профайл</param>
        /// <returns>true - удачное завершение; false - ошибка</returns>
        public override System.Boolean Add(UniXP.Common.CProfile objProfile)
        {
            System.Boolean bRet = false;

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
                // соединение с БД получено, прописываем команду на создание записи в БД
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
                cmd.Connection = DBConnection;
                cmd.Transaction = DBTransaction;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                // собственно процесс добавления статьи в БД
                bRet = this.Add(cmd, objProfile);
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
                cmd.Dispose();
            }
            catch (System.Exception f)
            {
                // откатываем транзакцию
                DBTransaction.Rollback();
                DevExpress.XtraEditors.XtraMessageBox.Show(
                "Не удалось создать статью бюджета:" + this.m_strBudgetItemNum + " " + this.Name +
                "\n\nТекст ошибки: " + f.Message, "Внимание",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
			finally // очищаем занимаемые ресурсы
            {
                DBConnection.Close();
            }

            return bRet;
        }

        /// <summary>
        /// Добавить запись в БД
        /// </summary>
        /// <param name="objProfile">профайл</param>
        /// <returns>true - удачное завершение; false - ошибка</returns>
        public System.Boolean Add(System.Data.SqlClient.SqlCommand cmd, UniXP.Common.CProfile objProfile)
        {
            System.Boolean bRet = false;
            if (cmd == null)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show("Отсутствует соединение с БД.", "Внимание",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                return bRet;
            }

            if (IsValidateProperties() == false) { return bRet; }

            try
            {
                cmd.Parameters.Clear();
                cmd.CommandText = System.String.Format("[{0}].[dbo].[sp_AddBudgetItem]", objProfile.GetOptionsDllDBName());
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ((System.Byte)(0)), ((System.Byte)(0)), "", System.Data.DataRowVersion.Current, null));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@GUID_ID", System.Data.SqlDbType.UniqueIdentifier, 4, System.Data.ParameterDirection.Output, false, ((System.Byte)(0)), ((System.Byte)(0)), "", System.Data.DataRowVersion.Current, null));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@BUDGET_GUID_ID", System.Data.SqlDbType.UniqueIdentifier));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@BUDGETITEM_NAME", System.Data.DbType.String));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@BUDGETITEM_NUM", System.Data.DbType.String));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@BUDGETITEM_ID", System.Data.SqlDbType.Int));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@BUDGETITEM_TRANSPORTREST", System.Data.DbType.Boolean));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@BUDGETITEM_DONTCHANGE", System.Data.DbType.Boolean));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ERROR_NUMBER", System.Data.SqlDbType.Int, 8, System.Data.ParameterDirection.Output, false, ((System.Byte)(0)), ((System.Byte)(0)), "", System.Data.DataRowVersion.Current, null));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ERROR_MESSAGE", System.Data.SqlDbType.NVarChar, 4000));
                cmd.Parameters["@ERROR_MESSAGE"].Direction = System.Data.ParameterDirection.Output;
                cmd.Parameters["@BUDGET_GUID_ID"].Value = this.m_uuidBudgetID;
                cmd.Parameters["@BUDGETITEM_NAME"].Value = this.Name;
                cmd.Parameters["@BUDGETITEM_NUM"].Value = this.m_strBudgetItemNum;
                cmd.Parameters["@BUDGETITEM_TRANSPORTREST"].Value = this.m_bTransprtRest;
                cmd.Parameters["@BUDGETITEM_DONTCHANGE"].Value = this.m_bDontChange;
                cmd.Parameters["@BUDGETITEM_ID"].Value = this.m_iBudgetItemID;
                if (this.uuidID.CompareTo(System.Guid.Empty) != 0)
                {
                    cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@BUDGETITEM_GUID_ID", System.Data.SqlDbType.UniqueIdentifier));
                    cmd.Parameters["@BUDGETITEM_GUID_ID"].Value = this.uuidID;
                }
                if (this.m_uuidDebitArticleID.CompareTo(System.Guid.Empty) != 0)
                {
                    cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@DEBITARTICLE_GUID_ID", System.Data.SqlDbType.UniqueIdentifier));
                    cmd.Parameters["@DEBITARTICLE_GUID_ID"].Value = this.m_uuidDebitArticleID;
                }
                if (this.m_uuidParentID.CompareTo(System.Guid.Empty) != 0)
                {
                    cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@PARENT_GUID_ID", System.Data.SqlDbType.UniqueIdentifier));
                    cmd.Parameters["@PARENT_GUID_ID"].Value = this.m_uuidParentID;
                }
                if (this.m_strBudgetItemDescription.Length > 0)
                {
                    cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@BUDGETITEM_DESCRIPTION", System.Data.DbType.String));
                    cmd.Parameters["@BUDGETITEM_DESCRIPTION"].Value = this.m_strBudgetItemDescription;
                }
                if (this.BudgetExpenseType != null )
                {
                    cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@BUDGETEXPENSETYPE_GUID", System.Data.SqlDbType.UniqueIdentifier));
                    cmd.Parameters["@BUDGETEXPENSETYPE_GUID"].Value = this.BudgetExpenseType.uuidID;
                }
                if (this.AccountPlan != null)
                {
                    cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ACCOUNTPLAN_GUID", System.Data.SqlDbType.UniqueIdentifier));
                    cmd.Parameters["@ACCOUNTPLAN_GUID"].Value = this.AccountPlan.uuidID;
                }

                cmd.ExecuteNonQuery();
                System.Int32 iRet = (System.Int32)cmd.Parameters["@RETURN_VALUE"].Value;
                if (iRet == 0)
                {
                    bRet = true;
                }
                else
                {
                    DevExpress.XtraEditors.XtraMessageBox.Show("Не удалось создать статью бюджета.\n\nТекст ошибки: " + 
                        ( System.String )cmd.Parameters["@ERROR_MESSAGE"].Value, "Внимание",
                        System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                }
            }
            catch (System.Exception f)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(
                "Не удалось создать статью бюджета.\n" + this.BudgetItemFullName + "\n\nТекст ошибки: " + f.Message, "Внимание",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }

            return bRet;
        }
        #endregion

        #region Update *
        /// <summary>
        /// Сохранить изменения в БД
        /// </summary>
        /// <param name="objProfile">профайл</param>
        /// <returns>true - удачное завершение; false - ошибка</returns>
        public override System.Boolean Update(UniXP.Common.CProfile objProfile)
        {
            System.Boolean bRet = false;

            return bRet;
        }
        /// <summary>
        /// Сохранить изменения в БД
        /// </summary>
        /// <param name="objProfile">профайл</param>
        /// <param name="bUpdateWarningParam">признак того, что нужно обновить список служб</param>
        /// <returns>true - удачное завершение; false - ошибка</returns>
        public System.Boolean Update(UniXP.Common.CProfile objProfile, System.Boolean bUpdateWarningParam)
        {
            System.Boolean bRet = false;

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
                // соединение с БД получено, прописываем команду на создание записи в БД
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
                cmd.Connection = DBConnection;
                cmd.Transaction = DBTransaction;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                // собственно процесс сохранения в БД
                bRet = this.Update(cmd, objProfile, bUpdateWarningParam);
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
                cmd.Dispose();
            }
            catch (System.Exception f)
            {
                // откатываем транзакцию
                DBTransaction.Rollback();
                DevExpress.XtraEditors.XtraMessageBox.Show(
                "Не удалось изменить свойства статьи бюджета:" + this.BudgetItemFullName +
                "\n\nТекст ошибки: " + f.Message, "Внимание",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
			finally // очищаем занимаемые ресурсы
            {
                DBConnection.Close();
            }

            return bRet;
        }
        /// <summary>
        /// Сохранить изменения в списке статей бюджета
        /// </summary>
        /// <param name="objProfile">профайл</param>
        /// <param name="objBudgetItemList">список статей бюджета</param>
        /// <param name="bUpdateWarningParam">признак того, что нужно обновить список расшифровок</param>
        /// <returns>true - удачное завершение; false - ошибка</returns>
        public static System.Boolean UpdateList(UniXP.Common.CProfile objProfile, List<CBudgetItem> objBudgetItemList,
            System.Boolean bUpdateWarningParam)
        {
            System.Boolean bRet = false;
            if ((objBudgetItemList == null) || (objBudgetItemList.Count == 0))
            {
                DevExpress.XtraEditors.XtraMessageBox.Show("Список статей расходов бюджета.", "Внимание",
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
                // соединение с БД получено, прописываем команду на создание записи в БД
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
                cmd.Connection = DBConnection;
                cmd.Transaction = DBTransaction;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                foreach (CBudgetItem objBudgetItem in objBudgetItemList)
                {
                    // собственно процесс сохранения в БД
                    bRet = objBudgetItem.Update(cmd, objProfile, bUpdateWarningParam);
                    if (bRet == false) { break; }
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
                cmd.Dispose();
            }
            catch (System.Exception f)
            {
                // откатываем транзакцию
                DBTransaction.Rollback();
                DevExpress.XtraEditors.XtraMessageBox.Show(
                "Не удалось сохранить изменения в списке статей бюджета.\n\nТекст ошибки: " + f.Message, "Внимание",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
			finally // очищаем занимаемые ресурсы
            {
                DBConnection.Close();
            }

            return bRet;
        }
        /// <summary>
        /// Сохранить изменения в БД
        /// </summary>
        /// <param name="objProfile">профайл</param>
        /// <param name="cmd">SQL-команда</param>
        /// <param name="bUpdateWarningParam">признак того, что нужно обновить список расшифровок</param>
        /// <returns>true - удачное завершение; false - ошибка</returns>
        public System.Boolean Update(System.Data.SqlClient.SqlCommand cmd,
            UniXP.Common.CProfile objProfile, System.Boolean bUpdateWarningParam)
        {
            System.Boolean bRet = false;
            if (cmd == null) { return bRet; }
            try
            {
                // проверка свойств
                if (this.IsValidateProperties() == false) { return bRet; }

                cmd.Parameters.Clear();
                cmd.CommandText = System.String.Format("[{0}].[dbo].[sp_EditBudgetItem]", objProfile.GetOptionsDllDBName());
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ((System.Byte)(0)), ((System.Byte)(0)), "", System.Data.DataRowVersion.Current, null));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@GUID_ID", System.Data.SqlDbType.UniqueIdentifier));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@BUDGETITEM_NAME", System.Data.DbType.String));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@BUDGETITEM_NUM", System.Data.DbType.String));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@BUDGETITEM_TRANSPORTREST", System.Data.DbType.Boolean));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@BUDGETITEM_DONTCHANGE", System.Data.DbType.Boolean));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@BUDGETITEM_ID", System.Data.SqlDbType.Int));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ERROR_NUMBER", System.Data.SqlDbType.Int, 8, System.Data.ParameterDirection.Output, false, ((System.Byte)(0)), ((System.Byte)(0)), "", System.Data.DataRowVersion.Current, null));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ERROR_MESSAGE", System.Data.SqlDbType.NVarChar, 4000));
                cmd.Parameters["@ERROR_MESSAGE"].Direction = System.Data.ParameterDirection.Output;
                cmd.Parameters["@GUID_ID"].Value = this.m_uuidID;
                cmd.Parameters["@BUDGETITEM_NAME"].Value = this.Name;
                cmd.Parameters["@BUDGETITEM_NUM"].Value = this.m_strBudgetItemNum;
                cmd.Parameters["@BUDGETITEM_TRANSPORTREST"].Value = this.m_bTransprtRest;
                cmd.Parameters["@BUDGETITEM_DONTCHANGE"].Value = this.m_bDontChange;
                cmd.Parameters["@BUDGETITEM_ID"].Value = this.m_iBudgetItemID;
                if (this.m_uuidParentID.CompareTo(System.Guid.Empty) != 0)
                {
                    cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@PARENT_GUID_ID", System.Data.SqlDbType.UniqueIdentifier));
                    cmd.Parameters["@PARENT_GUID_ID"].Value = this.m_uuidParentID;
                }
                if (this.m_strBudgetItemDescription.Length > 0)
                {
                    cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@BUDGETITEM_DESCRIPTION", System.Data.DbType.String));
                    cmd.Parameters["@BUDGETITEM_DESCRIPTION"].Value = this.m_strBudgetItemDescription;
                }
                if (this.BudgetExpenseType != null)
                {
                    cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@BUDGETEXPENSETYPE_GUID", System.Data.SqlDbType.UniqueIdentifier));
                    cmd.Parameters["@BUDGETEXPENSETYPE_GUID"].Value = this.BudgetExpenseType.uuidID;
                }
                if (this.AccountPlan != null)
                {
                    cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ACCOUNTPLAN_GUID", System.Data.SqlDbType.UniqueIdentifier));
                    cmd.Parameters["@ACCOUNTPLAN_GUID"].Value = this.AccountPlan.uuidID;
                }
                cmd.ExecuteNonQuery();
                System.Int32 iRet = (System.Int32)cmd.Parameters["@RETURN_VALUE"].Value;
                if (iRet == 0)
                {
                    bRet = true;
                    if (bUpdateWarningParam == true)
                    {
                        bRet = this.SaveBudgetItem(cmd, objProfile);
                    }
                }
                else
                {
                    switch (iRet)
                    {
                        case 1:
                            DevExpress.XtraEditors.XtraMessageBox.Show("Статья бюджета с заданным именем и номером уже есть в БД\n" +
                                this.BudgetItemFullName, "Внимание",
                                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);
                            break;
                        case 2:
                            DevExpress.XtraEditors.XtraMessageBox.Show("Статья бюджета с заданным идентификатором не найдена.\nУИ: " + this.m_uuidID.ToString(), "Внимание",
                                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                            break;
                        default:
                            DevExpress.XtraEditors.XtraMessageBox.Show("Ошибка изменения свойств статьи расходов: \n" + this.BudgetItemFullName +
                                "\n\nТекст ошибки: " + (System.String)cmd.Parameters["@ERROR_MESSAGE"].Value, "Внимание",
                                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                            break;
                    }
                }
            }
            catch (System.Exception f)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(
                "Не удалось изменить свойства статьи расходов: " + this.BudgetItemFullName +
                "\n\nТекст ошибки: " + f.Message, "Внимание",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
            return bRet;
        }
        #endregion

        #region Дерево статей бюджета с расшифровками *
        public static System.Boolean LoadBudgetItemList(UniXP.Common.CProfile objProfile,
            DevExpress.XtraTreeList.TreeList objTreeList, CBudget objBudget)
        {
            System.Boolean bRet = false;
            if (objTreeList == null)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show("Объект дерево не определен!", "Внимание",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);
                return bRet;
            }

            objTreeList.Enabled = false;
            objTreeList.ClearNodes();
            objBudget.BudgetItemList.Clear();

            // подключаемся к БД
            System.Data.SqlClient.SqlConnection DBConnection = objProfile.GetDBSource();
            if (DBConnection == null)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show("Отсутствует соединение с БД.", "Внимание",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);
                return bRet;
            }

            try
            {
                // соединение с БД получено, прописываем команду на выборку данных
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
                cmd.Connection = DBConnection;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                // запрашиваем список родительских статей расходов
                List<CBudgetItem> objBudgetItemList = LoadParentBudgetItemList(objProfile, cmd, objBudget.uuidID);
                if (objBudgetItemList != null)
                {
                    // для каждого элемента списка строим узел и проверяем его на предмет дочерних узлов
                    if (objBudgetItemList.Count > 0)
                    {
                        foreach (CBudgetItem objBudgetItem in objBudgetItemList)
                        {
                            //добавляем узел в дерево
                            DevExpress.XtraTreeList.Nodes.TreeListNode objNode =
                                objTreeList.AppendNode(new object[] { objBudgetItem.uuidID, 
                                objBudgetItem.m_uuidParentID, objBudgetItem.BudgetItemFullName, 
                                false, false, 
                                objBudgetItem.JanuaryDecode.MoneyPlan,
                                objBudgetItem.FebruaryDecode.MoneyPlan,
                                objBudgetItem.MarchDecode.MoneyPlan,
                                objBudgetItem.AprilDecode.MoneyPlan,
                                objBudgetItem.MayDecode.MoneyPlan,
                                objBudgetItem.JuneDecode.MoneyPlan,
                                objBudgetItem.JulyDecode.MoneyPlan,
                                objBudgetItem.AugustDecode.MoneyPlan,
                                objBudgetItem.SeptemberDecode.MoneyPlan,
                                objBudgetItem.OctoberDecode.MoneyPlan,
                                objBudgetItem.NovemberDecode.MoneyPlan,
                                objBudgetItem.DecemberDecode.MoneyPlan,
                                null }, null);

                            objNode.Tag = objBudgetItem;
                            objBudget.BudgetItemList.Add(objBudgetItem);

                            // рекурсия 
                            bRet = LoadChildBudgetItemNodes(objProfile, cmd, objBudget.uuidID, objNode, objTreeList);
                            if (bRet == false) { break; }
                        }
                    }
                    else
                    {
                        DevExpress.XtraEditors.XtraMessageBox.Show(
                        "Не удалось получить список статей расходов.", "Внимание",
                        System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);
                    }
                }

                cmd.Dispose();
            }
            catch (System.Exception f)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(
                "Не удалось получить список статей расходов.\n\nТекст ошибки: " + f.Message, "Внимание",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
			finally // очищаем занимаемые ресурсы
            {
                DBConnection.Close();
                objTreeList.Enabled = true;
                //System.DateTime dtEnd = System.DateTime.Now;
                //DevExpress.XtraEditors.XtraMessageBox.Show( 
                //"Время загрузки данных: " + System.Convert.ToString( dtEnd - dtStart ), "Внимание",
                //System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information );
            }
            return bRet;
        }


        public static System.Boolean LoadChildBudgetItemNodes(UniXP.Common.CProfile objProfile,
            System.Data.SqlClient.SqlCommand cmd, System.Guid uuidBudgetID,
            DevExpress.XtraTreeList.Nodes.TreeListNode objNodeParent,
            DevExpress.XtraTreeList.TreeList objTreeList)
        {
            System.Boolean bRet = false;
            if (objTreeList == null)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show("Объект дерево не определен!", "Внимание",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);
                return bRet;
            }

            if ((objNodeParent == null) || (objNodeParent.Tag == null))
            {
                DevExpress.XtraEditors.XtraMessageBox.Show("Объект узел дерева не определен!", "Внимание",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);
                return bRet;
            }

            if (cmd == null)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show("Отсутствует соединение с БД.", "Внимание",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);
                return bRet;
            }

            try
            {
                // запрашиваем список дочерних подстатей бюджета
                List<CBudgetItem> objBudgetItemList = LoadChildBudgetItemList(objProfile, cmd, uuidBudgetID,
                    ((CBudgetItem)objNodeParent.Tag));
                if (objBudgetItemList != null)
                {
                    // для каждого элемента списка строим узел и проверяем его на предмет дочерних узлов
                    if (objBudgetItemList.Count > 0)
                    {
                        foreach (CBudgetItem objBudgetItem in objBudgetItemList)
                        {
                            //добавляем узел в дерево
                            DevExpress.XtraTreeList.Nodes.TreeListNode objNode =
                                objTreeList.AppendNode(new object[] { objBudgetItem.uuidID, 
                                objBudgetItem.m_uuidParentID, objBudgetItem.BudgetItemFullName, 
                                false, false, 
                                objBudgetItem.JanuaryDecode.MoneyPlan,
                                objBudgetItem.FebruaryDecode.MoneyPlan,
                                objBudgetItem.MarchDecode.MoneyPlan,
                                objBudgetItem.AprilDecode.MoneyPlan,
                                objBudgetItem.MayDecode.MoneyPlan,
                                objBudgetItem.JuneDecode.MoneyPlan,
                                objBudgetItem.JulyDecode.MoneyPlan,
                                objBudgetItem.AugustDecode.MoneyPlan,
                                objBudgetItem.SeptemberDecode.MoneyPlan,
                                objBudgetItem.OctoberDecode.MoneyPlan,
                                objBudgetItem.NovemberDecode.MoneyPlan,
                                objBudgetItem.DecemberDecode.MoneyPlan,
                                null }, objNodeParent);

                            objNode.Tag = objBudgetItem;

                            // рекурсия 
                            bRet = LoadChildBudgetItemNodes(objProfile, cmd, uuidBudgetID, objNode, objTreeList);
                            if (bRet == false) { break; }
                        }
                    }
                    else
                    {
                        bRet = true;
                    }
                }
            }
            catch (System.Exception f)
            {
                bRet = false;

                DevExpress.XtraEditors.XtraMessageBox.Show(
                "Не удалось получить список подстатей бюджета.\n\nТекст ошибки: " + f.Message, "Внимание",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }

            return bRet;
        }

        public static List<CBudgetItem> LoadChildBudgetItemList(UniXP.Common.CProfile objProfile,
            System.Data.SqlClient.SqlCommand cmd, System.Guid uuidBudgetID, CBudgetItem objBudgetItemParent)
        {
            List<CBudgetItem> objList = null;

            if (objBudgetItemParent == null)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show("Не определена дочерняя статья расходов.", "Внимание",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);
                return objList;
            }
            // подключаемся к БД
            if (cmd == null)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show("Отсутствует соединение с БД.", "Внимание",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);
                return objList;
            }

            try
            {
                objList = new List<CBudgetItem>();

                // сперва запрашиваем список статей бюджета 
                cmd.Parameters.Clear();
                cmd.CommandText = System.String.Format("[{0}].[dbo].[sp_GetBudgetItemChild]", objProfile.GetOptionsDllDBName());
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ((System.Byte)(0)), ((System.Byte)(0)), "", System.Data.DataRowVersion.Current, null));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@BUDGETITEM_GUID_ID", System.Data.SqlDbType.UniqueIdentifier));
                cmd.Parameters["@BUDGETITEM_GUID_ID"].Value = objBudgetItemParent.uuidID;
                System.Data.SqlClient.SqlDataReader rs = cmd.ExecuteReader();

                if (rs.HasRows)
                {
                    while (rs.Read())
                    {
                        CBudgetItem objBudgetItemNew = new CBudgetItem();
                        objBudgetItemNew.uuidID = (System.Guid)rs["GUID_ID"];
                        objBudgetItemNew.m_strBudgetItemNum = (System.String)rs["BUDGETITEM_NUM"];
                        objBudgetItemNew.Name = (System.String)rs["BUDGETITEM_NAME"];
                        objBudgetItemNew.m_bTransprtRest = (System.Boolean)rs["BUDGETITEM_TRANSPORTREST"];
                        objBudgetItemNew.m_bDontChange = (System.Boolean)rs["BUDGETITEM_DONTCHANGE"];
                        objBudgetItemNew.m_iBudgetItemID = (System.Int32)rs["BUDGETITEM_ID"];
                        objBudgetItemNew.BudgetGUID = uuidBudgetID;
                        objBudgetItemNew.BudgetName = System.Convert.ToString(rs["BUDGET_NAME"]);

                        if (rs["PARENT_GUID_ID"] != System.DBNull.Value)
                        {
                            objBudgetItemNew.m_uuidParentID = (System.Guid)rs["PARENT_GUID_ID"];
                        }
                        if (rs["BUDGETITEM_DESCRIPTION"] != System.DBNull.Value)
                        {
                            objBudgetItemNew.m_strBudgetItemDescription = (System.String)rs["BUDGETITEM_DESCRIPTION"];
                        }
                        if (rs["DEBITARTICLE_GUID_ID"] != System.DBNull.Value)
                        {
                            objBudgetItemNew.m_uuidDebitArticleID = (System.Guid)rs["DEBITARTICLE_GUID_ID"];
                        }
                        if (rs["BUDGETEXPENSETYPE_GUID"] != System.DBNull.Value)
                        {
                            objBudgetItemNew.BudgetExpenseType = new CBudgetExpenseType()
                            {
                                uuidID = (System.Guid)rs["BUDGETEXPENSETYPE_GUID"],
                                Name = System.Convert.ToString(rs["BUDGETEXPENSETYPE_NAME"]),
                                CodeIn1C = System.Convert.ToInt32(rs["BUDGETEXPENSETYPE_1C_CODE"]),
                                IsActive = System.Convert.ToBoolean(rs["BUDGETEXPENSETYPE_ACTIVE"])
                            };
                        }
                        else
                        {
                            objBudgetItemNew.BudgetExpenseType = new CBudgetExpenseType(System.Guid.Empty, "", "");
                        }

                        objBudgetItemNew.AccountPlan = ( (rs["ACCOUNTPLAN_GUID"] != System.DBNull.Value) ?  new CAccountPlan()
                            {
                                uuidID = (System.Guid)rs["ACCOUNTPLAN_GUID"],
                                Name = System.Convert.ToString(rs["ACCOUNTPLAN_NAME"]),
                                IsActive = System.Convert.ToBoolean(rs["ACCOUNTPLAN_ACTIVE"]),
                                CodeIn1C = System.Convert.ToString(rs["ACCOUNTPLAN_1C_CODE"])
                            } : null );

                        objBudgetItemNew.BudgetProject = ((rs["BUDGETPROJECT_GUID"] != System.DBNull.Value) ? new CBudgetProject()
                        {
                            uuidID = (System.Guid)rs["BUDGETPROJECT_GUID"],
                            Name = System.Convert.ToString(rs["BUDGETPROJECT_NAME"]),
                            Description = "",
                            IsActive = System.Convert.ToBoolean(rs["BUDGETPROJECT_ACTIVE"]),
                            CodeIn1C = System.Convert.ToInt32(rs["BUDGETPROJECT_1C_CODE"])
                        } : null);

                        objBudgetItemNew.BudgetType = ((rs["BUDGETTYPE_GUID"] != System.DBNull.Value) ? new CBudgetType()
                        {
                            uuidID = (System.Guid)rs["BUDGETTYPE_GUID"],
                            Name = System.Convert.ToString(rs["BUDGETTYPE_NAME"]),
                            Description = ((rs["BUDGETTYPE_DESCRIPTION"] == System.DBNull.Value) ? "" : (System.String)rs["BUDGETTYPE_DESCRIPTION"]),
                            IsActive = System.Convert.ToBoolean(rs["BUDGETTYPE_ACTIVE"])
                        } : null);

                        objList.Add(objBudgetItemNew);
                    }
                }

                rs.Close();
                rs.Dispose();

                // теперь для этого списка загрузим расшифровки
                System.Boolean bInitDecode = true;
                foreach (CBudgetItem objBudgetIt in objList)
                {
                    if (objBudgetIt.uuidID.CompareTo(System.Guid.Empty) == 0) { continue; }

                    if (InitBudgetItem(objProfile, cmd, objBudgetIt) == false)
                    {
                        bInitDecode = false;
                        break;
                    }
                }
                if (bInitDecode == false) { objList = null; }
            }
            catch (System.Exception f)
            {
                objList = null;

                DevExpress.XtraEditors.XtraMessageBox.Show(
                "Не удалось получить список подстатей бюджета.\n\nТекст ошибки: " + f.Message, "Внимание",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
			finally // очищаем занимаемые ресурсы
            {
            }
            return objList;
        }

        public static System.Boolean InitBudgetItem(UniXP.Common.CProfile objProfile,
            System.Data.SqlClient.SqlCommand cmd, CBudgetItem objBudgetItem)
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
                // теперь для статей бюджета загрузим расшифровки
                cmd.Parameters.Clear();
                cmd.CommandText = System.String.Format("[{0}].[dbo].[sp_GetBudgetItemDecode]", objProfile.GetOptionsDllDBName());
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ((System.Byte)(0)), ((System.Byte)(0)), "", System.Data.DataRowVersion.Current, null));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@BUDGETITEM_GUID_ID", System.Data.SqlDbType.UniqueIdentifier));
                cmd.Parameters["@BUDGETITEM_GUID_ID"].Value = objBudgetItem.uuidID;
                System.Data.SqlClient.SqlDataReader rs = cmd.ExecuteReader();
                if (rs.HasRows)
                {
                    while (rs.Read())
                    {
                        CBudgetItemDecode objBudgetItemDecode = new CBudgetItemDecode((enumMonth)rs["BUDGETITEMDECODE_MONTH_ID"]);
                        objBudgetItemDecode.Currency = new CCurrency((System.Guid)rs["CURRENCY_GUID_ID"], (System.String)rs["CURRENCY_CODE"], (System.String)rs["CURRENCY_NAME"]);
                        objBudgetItemDecode.Description = (rs["BUDGETITEMDECODE_DESCRIPTION"] == System.DBNull.Value) ? "" : (System.String)rs["BUDGETITEMDECODE_DESCRIPTION"];
                        objBudgetItemDecode.MoneyPlan = System.Convert.ToDouble(rs["BUDGETITEMDECODE_MONEYPLAN"]);
                        objBudgetItemDecode.MoneyPermit = System.Convert.ToDouble(rs["BUDGETITEMDECODE_MONEYPERMIT"]);
                        objBudgetItemDecode.MoneyFact = System.Convert.ToDouble(rs["BUDGETITEMDECODE_MONEYFACT"]);
                        objBudgetItemDecode.MoneyReserve = System.Convert.ToDouble(rs["BUDGETITEMDECODE_MONEYRESERVE"]);
                        objBudgetItemDecode.MoneyPlanAccept = System.Convert.ToDouble(rs["BUDGETITEMDECODE_MONEYPLANACCEPT"]);
                        objBudgetItemDecode.MoneyCredit = System.Convert.ToDouble(rs["BUDGETITEMDECODE_MONEYCREDIT"]);
                        objBudgetItem.AddBudgetItemDecode(objBudgetItemDecode);
                    }
                }
                rs.Close();
                rs.Dispose();

                bRet = true;
            }
            catch (System.Exception f)
            {
                bRet = false;

                DevExpress.XtraEditors.XtraMessageBox.Show(
                "Не удалось загрузить список расшифровок.\n\nТекст ошибки: " + f.Message, "Внимание",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }

            return bRet;
        }
        /// <summary>
        /// Загружает в статью бюджета список расшифровок
        /// </summary>
        /// <param name="objProfile">профайл</param>
        /// <param name="cmd">SQL-команда</param>
        /// <returns>true - удачное завершение; false - ошибка</returns>
        private System.Boolean InitBudgetItem(UniXP.Common.CProfile objProfile,
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
                // теперь для статей бюджета загрузим расшифровки
                cmd.Parameters.Clear();
                cmd.CommandText = System.String.Format("[{0}].[dbo].[sp_GetBudgetItemDecode]", objProfile.GetOptionsDllDBName());
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ((System.Byte)(0)), ((System.Byte)(0)), "", System.Data.DataRowVersion.Current, null));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@BUDGETITEM_GUID_ID", System.Data.SqlDbType.UniqueIdentifier));
                cmd.Parameters["@BUDGETITEM_GUID_ID"].Value = this.uuidID;
                System.Data.SqlClient.SqlDataReader rs = cmd.ExecuteReader();
                if (rs.HasRows)
                {
                    while (rs.Read())
                    {
                        CBudgetItemDecode objBudgetItemDecode = new CBudgetItemDecode((enumMonth)rs["BUDGETITEMDECODE_MONTH_ID"]);
                        objBudgetItemDecode.Currency = new CCurrency((System.Guid)rs["CURRENCY_GUID_ID"], (System.String)rs["CURRENCY_CODE"], (System.String)rs["CURRENCY_NAME"]);
                        objBudgetItemDecode.Description = (rs["BUDGETITEMDECODE_DESCRIPTION"] == System.DBNull.Value) ? "" : (System.String)rs["BUDGETITEMDECODE_DESCRIPTION"];
                        objBudgetItemDecode.MoneyPlan = System.Convert.ToDouble(rs["BUDGETITEMDECODE_MONEYPLAN"]);
                        objBudgetItemDecode.MoneyPermit = System.Convert.ToDouble(rs["BUDGETITEMDECODE_MONEYPERMIT"]);
                        objBudgetItemDecode.MoneyFact = System.Convert.ToDouble(rs["BUDGETITEMDECODE_MONEYFACT"]);
                        objBudgetItemDecode.MoneyReserve = System.Convert.ToDouble(rs["BUDGETITEMDECODE_MONEYRESERVE"]);
                        objBudgetItemDecode.MoneyPlanAccept = System.Convert.ToDouble(rs["BUDGETITEMDECODE_MONEYPLANACCEPT"]);
                        this.AddBudgetItemDecode(objBudgetItemDecode);
                    }
                }
                rs.Close();
                rs.Dispose();

                bRet = true;
            }
            catch (System.Exception f)
            {
                bRet = false;

                DevExpress.XtraEditors.XtraMessageBox.Show(
                "Не удалось загрузить список расшифровок.\n\nТекст ошибки: " + f.Message, "Внимание",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }

            return bRet;
        }

        /// <summary>
        /// Загружает список расшифровок для статьи бюджета, связанной с нотой на увеличение расходной части этой статьи бюджета
        /// </summary>
        /// <param name="objProfile">профайл</param>
        /// <param name="uuidBudgetDocID">УИ бюджетного документа (ноты)</param>
        public void LoadBudgetDocItemDecodeList(UniXP.Common.CProfile objProfile,
            System.Guid uuidBudgetDocID)
        {
            // подключаемся к БД
            System.Data.SqlClient.SqlConnection DBConnection = objProfile.GetDBSource();
            if (DBConnection == null)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show("Отсутствует соединение с БД.", "Внимание",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);
                return;
            }
            try
            {
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
                cmd.Connection = DBConnection;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Clear();
                cmd.CommandText = System.String.Format("[{0}].[dbo].[sp_GetBudgetDocItemDecodeList]", objProfile.GetOptionsDllDBName());
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ((System.Byte)(0)), ((System.Byte)(0)), "", System.Data.DataRowVersion.Current, null));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@BUDGETITEM_GUID_ID", System.Data.SqlDbType.UniqueIdentifier));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@BUDGETDOC_GUID_ID", System.Data.SqlDbType.UniqueIdentifier));
                cmd.Parameters["@BUDGETITEM_GUID_ID"].Value = this.uuidID;
                cmd.Parameters["@BUDGETDOC_GUID_ID"].Value = uuidBudgetDocID;
                System.Data.SqlClient.SqlDataReader rs = cmd.ExecuteReader();
                if (rs.HasRows)
                {
                    while (rs.Read())
                    {
                        CBudgetItemDecode objBudgetItemDecode = new CBudgetItemDecode((enumMonth)rs["MONTH_ID"]);
                        objBudgetItemDecode.Currency = new CCurrency((System.Guid)rs["CURRENCY_GUID_ID"], (System.String)rs["CURRENCY_CODE"], (System.String)rs["CURRENCY_NAME"]);
                        objBudgetItemDecode.MoneyPlan = System.Convert.ToDouble(rs["MONEY"]);
                        objBudgetItemDecode.MoneyPermit = 0;
                        objBudgetItemDecode.MoneyFact = 0;
                        objBudgetItemDecode.MoneyReserve = 0;
                        objBudgetItemDecode.MoneyPlanAccept = System.Convert.ToDouble(rs["MONEY_IN_BUDGETCURRENCY"]);
                        this.AddBudgetItemDecode(objBudgetItemDecode);
                    }
                }
                rs.Close();
                rs.Dispose();
            }
            catch (System.Exception f)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(
                "Не удалось загрузить список расшифровок.\n\nТекст ошибки: " + f.Message, "Внимание",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
            finally
            {
                if (DBConnection != null) { DBConnection.Close(); }
            }

            return;
        }
        public static List<CBudgetItem> LoadParentBudgetItemList(UniXP.Common.CProfile objProfile,
            System.Data.SqlClient.SqlCommand cmd, System.Guid uuidBudgetID)
        {
            List<CBudgetItem> objList = null;

            // подключаемся к БД
            if (cmd == null)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show("Отсутствует соединение с БД.", "Внимание",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);
                return objList;
            }

            try
            {
                objList = new List<CBudgetItem>();

                // сперва запрашиваем список статей бюджета 
                cmd.Parameters.Clear();
                cmd.CommandText = System.String.Format("[{0}].[dbo].[sp_GetBudgetItemParent]", objProfile.GetOptionsDllDBName());
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ((System.Byte)(0)), ((System.Byte)(0)), "", System.Data.DataRowVersion.Current, null));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@BUDGET_GUID_ID", System.Data.SqlDbType.UniqueIdentifier));
                cmd.Parameters["@BUDGET_GUID_ID"].Value = uuidBudgetID;
                System.Data.SqlClient.SqlDataReader rs = cmd.ExecuteReader();

                if (rs.HasRows)
                {
                    while (rs.Read())
                    {
                        CBudgetItem objBudgetItemNew = new CBudgetItem();
                        objBudgetItemNew.uuidID = (System.Guid)rs["GUID_ID"];
                        objBudgetItemNew.m_strBudgetItemNum = (System.String)rs["BUDGETITEM_NUM"];
                        objBudgetItemNew.Name = (System.String)rs["BUDGETITEM_NAME"];
                        objBudgetItemNew.m_bTransprtRest = (System.Boolean)rs["BUDGETITEM_TRANSPORTREST"];
                        objBudgetItemNew.m_bDontChange = (System.Boolean)rs["BUDGETITEM_DONTCHANGE"];
                        objBudgetItemNew.m_iBudgetItemID = (System.Int32)rs["BUDGETITEM_ID"];
                        objBudgetItemNew.BudgetGUID = uuidBudgetID;
                        objBudgetItemNew.BudgetName = System.Convert.ToString(rs["BUDGET_NAME"]);

                        if (rs["BUDGETITEM_DESCRIPTION"] != System.DBNull.Value)
                        {
                            objBudgetItemNew.m_strBudgetItemDescription = (System.String)rs["BUDGETITEM_DESCRIPTION"];
                        }
                        if (rs["DEBITARTICLE_GUID_ID"] != System.DBNull.Value)
                        {
                            objBudgetItemNew.m_uuidDebitArticleID = (System.Guid)rs["DEBITARTICLE_GUID_ID"];
                        }

                        objBudgetItemNew.BudgetExpenseType = ((rs["BUDGETEXPENSETYPE_GUID"] != System.DBNull.Value) ? new CBudgetExpenseType()
                        {
                            uuidID = (System.Guid)rs["BUDGETEXPENSETYPE_GUID"],
                            Name = System.Convert.ToString(rs["BUDGETEXPENSETYPE_NAME"]),
                            CodeIn1C = System.Convert.ToInt32(rs["BUDGETEXPENSETYPE_1C_CODE"]),
                            IsActive = System.Convert.ToBoolean(rs["BUDGETEXPENSETYPE_ACTIVE"])
                        } : null);

                        objBudgetItemNew.AccountPlan = ((rs["ACCOUNTPLAN_GUID"] != System.DBNull.Value) ? new CAccountPlan()
                            {
                                uuidID = (System.Guid)rs["ACCOUNTPLAN_GUID"],
                                Name = System.Convert.ToString(rs["ACCOUNTPLAN_NAME"]),
                                IsActive = System.Convert.ToBoolean(rs["ACCOUNTPLAN_ACTIVE"]),
                                CodeIn1C = System.Convert.ToString(rs["ACCOUNTPLAN_1C_CODE"])
                            } : null );

                        objBudgetItemNew.BudgetProject = ((rs["BUDGETPROJECT_GUID"] != System.DBNull.Value) ? new CBudgetProject()
                        {
                            uuidID = (System.Guid)rs["BUDGETPROJECT_GUID"],
                            Name = System.Convert.ToString(rs["BUDGETPROJECT_NAME"]),
                            Description = "",
                            IsActive = System.Convert.ToBoolean(rs["BUDGETPROJECT_ACTIVE"]),
                            CodeIn1C = System.Convert.ToInt32(rs["BUDGETPROJECT_1C_CODE"])
                        } : null);

                        objBudgetItemNew.BudgetType = ((rs["BUDGETTYPE_GUID"] != System.DBNull.Value) ? new CBudgetType()
                        {
                            uuidID = (System.Guid)rs["BUDGETTYPE_GUID"],
                            Name = System.Convert.ToString(rs["BUDGETTYPE_NAME"]),
                            Description = ((rs["BUDGETTYPE_DESCRIPTION"] == System.DBNull.Value) ? "" : (System.String)rs["BUDGETTYPE_DESCRIPTION"]),
                            IsActive = System.Convert.ToBoolean(rs["BUDGETTYPE_ACTIVE"])
                        } : null);
                        objList.Add(objBudgetItemNew);
                    }
                }
                rs.Close();
                rs.Dispose();

                // теперь для этого списка загрузим расшифровки
                System.Boolean bInitDecode = true;
                foreach (CBudgetItem objBudgetIt in objList)
                {
                    if (objBudgetIt.uuidID.CompareTo(System.Guid.Empty) == 0) { continue; }

                    if (InitBudgetItem(objProfile, cmd, objBudgetIt) == false)
                    {
                        bInitDecode = false;
                        break;
                    }
                }
                if (bInitDecode == false) { objList.Clear(); }
            }
            catch (System.Exception f)
            {
                objList = null;

                DevExpress.XtraEditors.XtraMessageBox.Show(
                "Не удалось получить список подстатей бюджета.\n\nТекст ошибки: " + f.Message, "Внимание",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
			finally // очищаем занимаемые ресурсы
            {
            }
            return objList;
        }

        #endregion

        #region Список статей бюджета верхнего уровня (PARENT = null)
        /// <summary>
        /// Возвращает список родительских статей бюджета
        /// </summary>
        /// <param name="objProfile">профайл</param>
        /// <param name="uuidBudgetID">УИ бюджета</param>
        /// <returns>список родительских статей бюджета</returns>
        public static List<CBudgetItem> LoadParentBudgetItemList(UniXP.Common.CProfile objProfile,
            System.Guid uuidBudgetID)
        {
            List<CBudgetItem> objList = null;

            // подключаемся к БД
            System.Data.SqlClient.SqlConnection DBConnection = objProfile.GetDBSource();
            if (DBConnection == null)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show("Отсутствует соединение с БД.", "Внимание",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);
                return objList;
            }

            try
            {
                // сперва запрашиваем список статей бюджета 
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
                cmd.Connection = DBConnection;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = System.String.Format("[{0}].[dbo].[sp_GetBudgetItemParent]", objProfile.GetOptionsDllDBName());
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ((System.Byte)(0)), ((System.Byte)(0)), "", System.Data.DataRowVersion.Current, null));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@BUDGET_GUID_ID", System.Data.SqlDbType.UniqueIdentifier));
                cmd.Parameters["@BUDGET_GUID_ID"].Value = uuidBudgetID;
                System.Data.SqlClient.SqlDataReader rs = cmd.ExecuteReader();

                if (rs.HasRows)
                {
                    objList = new List<CBudgetItem>();
                    while (rs.Read())
                    {
                        CBudgetItem objBudgetItemNew = new CBudgetItem();
                        objBudgetItemNew.uuidID = (System.Guid)rs["GUID_ID"];
                        objBudgetItemNew.m_strBudgetItemNum = (System.String)rs["BUDGETITEM_NUM"];
                        objBudgetItemNew.Name = (System.String)rs["BUDGETITEM_NAME"];
                        objBudgetItemNew.m_bTransprtRest = (System.Boolean)rs["BUDGETITEM_TRANSPORTREST"];
                        objBudgetItemNew.m_bDontChange = (System.Boolean)rs["BUDGETITEM_DONTCHANGE"];
                        objBudgetItemNew.m_iBudgetItemID = (System.Int32)rs["BUDGETITEM_ID"];
                        objBudgetItemNew.BudgetGUID = uuidBudgetID;
                        objBudgetItemNew.BudgetName = System.Convert.ToString(rs["BUDGET_NAME"]);

                        if (rs["BUDGETITEM_DESCRIPTION"] != System.DBNull.Value)
                        {
                            objBudgetItemNew.m_strBudgetItemDescription = (System.String)rs["BUDGETITEM_DESCRIPTION"];
                        }
                        if (rs["DEBITARTICLE_GUID_ID"] != System.DBNull.Value)
                        {
                            objBudgetItemNew.m_uuidDebitArticleID = (System.Guid)rs["DEBITARTICLE_GUID_ID"];
                        }
                        objBudgetItemNew.BudgetExpenseType = ( (rs["BUDGETEXPENSETYPE_GUID"] != System.DBNull.Value) ? new CBudgetExpenseType()
                            {
                                uuidID = (System.Guid)rs["BUDGETEXPENSETYPE_GUID"],
                                Name = System.Convert.ToString(rs["BUDGETEXPENSETYPE_NAME"]),
                                CodeIn1C = System.Convert.ToInt32(rs["BUDGETEXPENSETYPE_1C_CODE"]),
                                IsActive = System.Convert.ToBoolean(rs["BUDGETEXPENSETYPE_ACTIVE"])
                            } : null );

                        objBudgetItemNew.AccountPlan = ((rs["ACCOUNTPLAN_GUID"] != System.DBNull.Value) ? new CAccountPlan()
                        {
                            uuidID = (System.Guid)rs["ACCOUNTPLAN_GUID"],
                            Name = System.Convert.ToString(rs["ACCOUNTPLAN_NAME"]),
                            IsActive = System.Convert.ToBoolean(rs["ACCOUNTPLAN_ACTIVE"]),
                            CodeIn1C = System.Convert.ToString(rs["ACCOUNTPLAN_1C_CODE"])
                        } : null);

                        objBudgetItemNew.BudgetProject = ((rs["BUDGETPROJECT_GUID"] != System.DBNull.Value) ? new CBudgetProject()
                        {
                            uuidID = (System.Guid)rs["BUDGETPROJECT_GUID"],
                            Name = System.Convert.ToString(rs["BUDGETPROJECT_NAME"]),
                            Description = "",
                            IsActive = System.Convert.ToBoolean(rs["BUDGETPROJECT_ACTIVE"]),
                            CodeIn1C = System.Convert.ToInt32(rs["BUDGETPROJECT_1C_CODE"])
                        } : null);
                        objBudgetItemNew.BudgetType = ((rs["BUDGETTYPE_GUID"] != System.DBNull.Value) ? new CBudgetType()
                        {
                            uuidID = (System.Guid)rs["BUDGETTYPE_GUID"],
                            Name = System.Convert.ToString(rs["BUDGETTYPE_NAME"]),
                            Description = ((rs["BUDGETTYPE_DESCRIPTION"] == System.DBNull.Value) ? "" : (System.String)rs["BUDGETTYPE_DESCRIPTION"]),
                            IsActive = System.Convert.ToBoolean(rs["BUDGETTYPE_ACTIVE"])
                        } : null);


                        objList.Add(objBudgetItemNew);
                    }
                }
                rs.Close();
                rs.Dispose();
            }
            catch (System.Exception f)
            {
                objList = null;

                DevExpress.XtraEditors.XtraMessageBox.Show(
                "Не удалось получить список статей бюджета.\n\nТекст ошибки: " + f.Message, "Внимание",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
			finally // очищаем занимаемые ресурсы
            {
                if (DBConnection != null) { DBConnection.Close(); }
            }

            return objList;
        }
        #endregion

        #region Создание списка статей бюджета на основе списка статей расходов *
        /// <summary>
        /// Создает в БД структуру дерева статей бюджета на основе списка статей расходов,
        /// назначенных бюджетному подразделению, с которым связан бюджет
        /// </summary>
        /// <param name="objProfile">профайл</param>
        /// <param name="uuidBudgetID">уи бюджета</param>
        /// <returns>true - успешное завершение; false - ошибка</returns>
        public static System.Boolean ImportDebitArticleListToBudget(UniXP.Common.CProfile objProfile,
            System.Guid uuidBudgetID)
        {
            System.Boolean bRet = false;
            // подключаемся к БД
            System.Data.SqlClient.SqlConnection DBConnection = objProfile.GetDBSource();
            if (DBConnection == null)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show("Отсутствует соединение с БД.", "Внимание",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);
                return bRet;
            }

            try
            {
                // соединение с БД получено, прописываем команду на выборку данных
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
                cmd.Connection = DBConnection;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = System.String.Format("[{0}].[dbo].[sp_ImportDebitArticleListToBudget]", objProfile.GetOptionsDllDBName());
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ((System.Byte)(0)), ((System.Byte)(0)), "", System.Data.DataRowVersion.Current, null));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@BUDGET_GUID_ID", System.Data.SqlDbType.UniqueIdentifier));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ERROR_NUMBER", System.Data.SqlDbType.Int, 8, System.Data.ParameterDirection.Output, false, ((System.Byte)(0)), ((System.Byte)(0)), "", System.Data.DataRowVersion.Current, null));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ERROR_MESSAGE", System.Data.SqlDbType.NVarChar, 4000));
                cmd.Parameters["@ERROR_MESSAGE"].Direction = System.Data.ParameterDirection.Output;
                cmd.Parameters["@BUDGET_GUID_ID"].Value = uuidBudgetID;
                cmd.ExecuteNonQuery();
                System.Int32 iRet = (System.Int32)cmd.Parameters["@RETURN_VALUE"].Value;
                if (iRet == 0)
                {
                    bRet = true;
                }
                else
                {
                    switch (iRet)
                    {
                        case 1:
                            DevExpress.XtraEditors.XtraMessageBox.Show("Ошибка создания списка статей расходов.\nБюджет с заданным идентификатором не найден.\nУИ бюджета: " +
                                uuidBudgetID.ToString(), "Внимание",
                                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                            break;
                        case 2:
                            DevExpress.XtraEditors.XtraMessageBox.Show("Ошибка создания списка статей расходов.\nБюджет уже содержит список статей.", "Внимание",
                                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                            break;
                        default:
                            DevExpress.XtraEditors.XtraMessageBox.Show("Ошибка создания списка статей расходов.\n\n\nТекст ошибки: " + (System.String)cmd.Parameters["@ERROR_MESSAGE"].Value, "Внимание",
                                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                            break;
                    }
                }

                cmd.Dispose();
            }
            catch (System.Exception f)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(
                "Ошибка создания списка статей расходов.\n\nТекст ошибки: " + f.Message, "Внимание",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
			finally // очищаем занимаемые ресурсы
            {
                DBConnection.Close();
            }

            return bRet;
        }

        #endregion

        #region Проверка на возможность изменения/удаления статьи *
        /// <summary>
        /// Проверяет, можно ли изменить статью расходов из БД
        /// </summary>
        /// <param name="objProfile">профайл</param>
        /// <param name="objProfile">профайл</param>
        /// <returns>true - можно; false - нельзя</returns>
        public System.Boolean IsPossibleChangeBudgetItem(UniXP.Common.CProfile objProfile,
            System.Boolean bShowMessage)
        {
            System.Boolean bRes = false;
            // соединение с БД
            System.Data.SqlClient.SqlConnection DBConnection = objProfile.GetDBSource();
            if (DBConnection == null)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show("Отсутствует соединение с БД.", "Внимание",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                return bRes;
            }
            try
            {
                // SQL-команда 
                System.String strDebitArticleList = "";
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
                cmd.Connection = DBConnection;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = System.String.Format("[{0}].[dbo].[sp_ISPossibleChangeBudgetItem]", objProfile.GetOptionsDllDBName());
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ((System.Byte)(0)), ((System.Byte)(0)), "", System.Data.DataRowVersion.Current, null));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@GUID_ID", System.Data.SqlDbType.UniqueIdentifier, 16));
                cmd.Parameters["@GUID_ID"].Value = this.m_uuidID;
                System.Data.SqlClient.SqlDataReader rs = cmd.ExecuteReader();
                if (rs.HasRows)
                {
                    strDebitArticleList = "У данной статьи: " + this.BudgetItemFullName + "\nнельзя менять список расшифровок и ее нельзя удалять!\n";
                    while (rs.Read())
                    {
                        strDebitArticleList += ("\n" + rs.GetString(1) + " " + rs.GetString(2) + "\n" + rs.GetString(3));
                    }
                }
                else { bRes = true; }

                rs.Close();
                rs.Dispose();
                cmd.Dispose();

                if ((bRes == false) && (bShowMessage == true))
                {
                    DevExpress.XtraEditors.XtraMessageBox.Show(strDebitArticleList, "Внимание",
                      System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);
                }
            }
            catch (System.Exception e)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(
                "Не удалось выполнить проверку на возможность изменения статьи бюджета.\n" + e.Message, "Внимание",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
			finally // очищаем занимаемые ресурсы
            {
                DBConnection.Close();
            }
            return bRes;
        }
        #endregion

        #region Выполнение бюджета  *
        /// <summary>
        /// возвращает список статей бюджета с указанием плана и фактического расхода по месяцам
        /// </summary>
        /// <param name="uuidBudgetID">уи бюджета</param>
        /// <param name="objProfile">профайл</param>
        /// <returns>список статей бюджета</returns>
        public static List<CBudgetItemBalans> GetBudgetItemBalansList(System.Guid uuidBudgetID,
            UniXP.Common.CProfile objProfile)
        {
            List<CBudgetItemBalans> objList = new List<CBudgetItemBalans>();

            System.Data.SqlClient.SqlConnection DBConnection = objProfile.GetDBSource();
            if (DBConnection == null) { return objList; }
            try
            {
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
                cmd.Connection = DBConnection;
                cmd.CommandTimeout = 0;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = System.String.Format("[{0}].[dbo].[sp_GetBudgetItemBalansList]", objProfile.GetOptionsDllDBName());
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ((System.Byte)(0)), ((System.Byte)(0)), "", System.Data.DataRowVersion.Current, null));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@BUDGET_GUID_ID", System.Data.SqlDbType.UniqueIdentifier));
                cmd.Parameters["@BUDGET_GUID_ID"].Value = uuidBudgetID;
                System.Data.SqlClient.SqlDataReader rs = cmd.ExecuteReader();
                if (rs.HasRows)
                {
                    // набор данных непустой
                    while (rs.Read())
                    {
                        CBudgetItemBalans objBudgetItemBalans = new CBudgetItemBalans();
                        objBudgetItemBalans.DebitArticle = new CDebitArticle(rs.GetGuid(0), rs.GetString(1), rs.GetString(2));
                        objBudgetItemBalans.Month = (enumMonth)rs.GetInt32(3);
                        objBudgetItemBalans.MoneyPlan = (System.Double)rs.GetSqlMoney(4).Value;
                        objBudgetItemBalans.MoneyFact = (System.Double)rs.GetSqlMoney(5).Value;

                        objList.Add(objBudgetItemBalans);
                    }
                }
                rs.Close();
                rs.Dispose();
                cmd.Dispose();
            }
            catch (System.Exception f)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(
                "Не удалось получить отчет о выполнении бюджета.\n\nТекст ошибки: " + f.Message, "Внимание",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
            finally // очищаем занимаемые ресурсы
            {
                DBConnection.Close();
            }
            return objList;
        }
        /// <summary>
        /// Определяет остаток средств по родительской статье бюджета
        /// </summary>
        /// <param name="uuidBudgetItemID">уи статьи бюджета</param>
        /// <param name="objProfile">профайл</param>
        /// <returns>остаток средств</returns>
        public static System.Double GetParentBudgetItemBalans(System.Guid uuidBudgetItemID,
            UniXP.Common.CProfile objProfile)
        {
            System.Double mRet = 0;

            System.Data.SqlClient.SqlConnection DBConnection = objProfile.GetDBSource();
            if (DBConnection == null)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show("Ошибка подключения к базе данных!", "Внимание",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                return mRet;
            }
            try
            {
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
                cmd.Connection = DBConnection;
                cmd.CommandTimeout = 0;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = System.String.Format("[{0}].[dbo].[sp_GetBudgetParentItemBalans]", objProfile.GetOptionsDllDBName());
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ((System.Byte)(0)), ((System.Byte)(0)), "", System.Data.DataRowVersion.Current, null));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@BUDGETITEM_GUID_ID", System.Data.SqlDbType.UniqueIdentifier));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@BudgetParentItemBalans", System.Data.SqlDbType.Money));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ERROR_NUM", System.Data.SqlDbType.Int, 8, System.Data.ParameterDirection.Output, false, ((System.Byte)(0)), ((System.Byte)(0)), "", System.Data.DataRowVersion.Current, null));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ERROR_MES", System.Data.SqlDbType.NVarChar, 4000));
                cmd.Parameters["@ERROR_MES"].Direction = System.Data.ParameterDirection.Output;
                cmd.Parameters["@BudgetParentItemBalans"].Direction = System.Data.ParameterDirection.Output;
                cmd.Parameters["@BUDGETITEM_GUID_ID"].Value = uuidBudgetItemID;
                cmd.ExecuteNonQuery();

                if ((System.Int32)cmd.Parameters["@RETURN_VALUE"].Value == 0)
                {
                    mRet = System.Convert.ToDouble(cmd.Parameters["@BudgetParentItemBalans"].Value);
                }
                else
                {
                    DevExpress.XtraEditors.XtraMessageBox.Show(
                    "Не удалось получить остаток по статье.\n\nТекст ошибки: " + (System.String)cmd.Parameters["@ERROR_MES"].Value, "Внимание",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                }
                cmd.Dispose();
            }
            catch (System.Exception f)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(
                "Не удалось получить остаток по статье.\n\nТекст ошибки: " + f.Message, "Внимание",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
            finally // очищаем занимаемые ресурсы
            {
                DBConnection.Close();
            }
            return mRet;
        }
        #endregion

        #region Журнал транзакций
        /// <summary>
        /// Возвращает список транзакций
        /// </summary>
        /// <param name="objProfile">профайл</param>
        /// <returns>список транзакций</returns>
        public List<structBudgetItemAccTrn> GetAccountTransnList(UniXP.Common.CProfile objProfile)
        {
            List<structBudgetItemAccTrn> objList = new List<structBudgetItemAccTrn>();

            System.Data.SqlClient.SqlConnection DBConnection = objProfile.GetDBSource();
            if (DBConnection == null)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show("Отсутствует соединение с БД.", "Внимание",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);
                return objList;
            }

            try
            {
                // соединение с БД получено, прописываем команду на выборку данных
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
                cmd.Connection = DBConnection;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = System.String.Format("[{0}].[dbo].[sp_GetAccountTransnForBudgetItem]", objProfile.GetOptionsDllDBName());
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ((System.Byte)(0)), ((System.Byte)(0)), "", System.Data.DataRowVersion.Current, null));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@BUDGETITEM_GUID_ID", System.Data.SqlDbType.UniqueIdentifier));
                cmd.Parameters["@BUDGETITEM_GUID_ID"].Value = this.m_uuidID;
                System.Data.SqlClient.SqlDataReader rs = cmd.ExecuteReader();
                if (rs.HasRows)
                {
                    while (rs.Read())
                    {
                        structBudgetItemAccTrn objBudgetItemTrn = new structBudgetItemAccTrn();
                        objBudgetItemTrn.TrnDate = (System.DateTime)rs["ACCOUNTTRANSN_DATE"];
                        objBudgetItemTrn.MoneyTrn = System.Convert.ToDouble(rs["ACCOUNTTRANSN_MONEY"]);
                        objBudgetItemTrn.CurrencyTrn = (System.String)rs["TRN_CURRENCY_CODE"];
                        objBudgetItemTrn.DocDate = (System.DateTime)rs["BUDGETDOC_DATE"];
                        objBudgetItemTrn.MoneyDoc = System.Convert.ToDouble(rs["BUDGETDOC_MONEY"]);
                        objBudgetItemTrn.CurrencyDoc = (System.String)rs["DOC_CURRENCY_CODE"];
                        objBudgetItemTrn.ObjectiveDoc = (System.String)rs["BUDGETDOC_OBJECTIVE"];
                        objBudgetItemTrn.UserTrn = (System.String)rs["USER_LASTNAME"] + " " + (System.String)rs["USER_FIRSTNAME"];

                        System.String strOperName = "";
                        System.Int32 iOperType = (System.Int32)rs["OPERTYPE_ID"];
                        switch (iOperType)
                        {
                            case 0:
                                strOperName = "Резерв средств";
                                break;
                            case 1:
                                strOperName = "Снятие средств с резерва";
                                break;
                            case 2:
                                strOperName = "Увеличение суммы статьи расходов";
                                break;
                            case 3:
                                strOperName = "Уменьшение суммы статьи расходов";
                                break;
                            case 4:
                                strOperName = "Оплата";
                                break;
                            case 5:
                                strOperName = "Возврат средств";
                                break;
                            case 6:
                                strOperName = "Курсовая разница";
                                break;
                            default:
                                break;
                        }
                        objBudgetItemTrn.Operation = strOperName;

                        objList.Add(objBudgetItemTrn);
                    }
                }
                rs.Close();
                rs = null;
                cmd = null;
            }
            catch (System.Exception f)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(
                "Не удалось получить список проводок.\n\nТекст ошибки: " + f.Message, "Внимание",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
            finally // очищаем занимаемые ресурсы
            {
                DBConnection.Close();
            }

            return objList;
        }

        #endregion

        #region Журнал документов
        /// <summary>
        /// Возвращает список бюджетных документов
        /// </summary>
        /// <param name="objProfile">профайл</param>
        /// <returns>список бюджетных документов</returns>
        public List<structBudgetItemDoc> GetBudgetDocList(UniXP.Common.CProfile objProfile)
        {
            List<structBudgetItemDoc> objList = new List<structBudgetItemDoc>();

            System.Data.SqlClient.SqlConnection DBConnection = objProfile.GetDBSource();
            if (DBConnection == null)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show("Отсутствует соединение с БД.", "Внимание",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);
                return objList;
            }

            try
            {
                // соединение с БД получено, прописываем команду на выборку данных
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
                cmd.Connection = DBConnection;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = System.String.Format("[{0}].[dbo].[sp_GetBudgetDocListForBudgetItem]", objProfile.GetOptionsDllDBName());
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ((System.Byte)(0)), ((System.Byte)(0)), "", System.Data.DataRowVersion.Current, null));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@BUDGETITEM_GUID_ID", System.Data.SqlDbType.UniqueIdentifier));
                cmd.Parameters["@BUDGETITEM_GUID_ID"].Value = this.m_uuidID;
                System.Data.SqlClient.SqlDataReader rs = cmd.ExecuteReader();
                if (rs.HasRows)
                {
                    while (rs.Read())
                    {
                        structBudgetItemDoc objBudgetItemDoc = new structBudgetItemDoc();
                        objBudgetItemDoc.DocDate = (System.DateTime)rs["BUDGETDOC_DATE"];
                        objBudgetItemDoc.DocMoney = System.Convert.ToDouble(rs["BUDGETDOCITEM_DOCMONEY"]);
                        objBudgetItemDoc.Currency = (System.String)rs["CURRENCY_CODE"];
                        objBudgetItemDoc.Money = System.Convert.ToDouble(rs["BUDGETDOCITEM_MONEY"]);
                        objBudgetItemDoc.DocState = (System.String)rs["BUDGETDOCSTATE_NAME"];
                        objBudgetItemDoc.BudgetItemFullName = (System.String)rs["BUDGETITEM_NUM"] +
                            " " + (System.String)rs["BUDGETITEM_NAME"];
                        objBudgetItemDoc.ObjectiveDoc = (System.String)rs["BUDGETDOC_OBJECTIVE"];
                        objBudgetItemDoc.User = (System.String)rs["USER_LASTNAME"] + " " + (System.String)rs["USER_FIRSTNAME"];

                        objList.Add(objBudgetItemDoc);
                    }
                }
                rs.Close();
                rs = null;
                cmd = null;
            }
            catch (System.Exception f)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(
                "Не удалось получить список документов.\n\nТекст ошибки: " + f.Message, "Внимание",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
            finally // очищаем занимаемые ресурсы
            {
                DBConnection.Close();
            }

            return objList;
        }

        #endregion

        #region Детализация статьи бюджета
        /// <summary>
        /// Возвращает список с расшифровкой статьи бюджета и ее подстатей
        /// </summary>
        /// <param name="uuidBudgetItemID">УИ статьи бюджета</param>
        /// <returns>список с расшифровкой статьи бюджета и ее подстатей</returns>
        public static List<CBudgetItem> GetBudgetItemDetailList(System.Guid uuidBudgetItemID, UniXP.Common.CProfile objProfile)
        {
            List<CBudgetItem> objList = new List<CBudgetItem>();

            System.Data.SqlClient.SqlConnection DBConnection = objProfile.GetDBSource();
            if (DBConnection == null)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show("Отсутствует соединение с БД.", "Внимание",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);
                return objList;
            }

            try
            {
                // соединение с БД получено, прописываем команду на выборку данных
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
                cmd.Connection = DBConnection;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = System.String.Format("[{0}].[dbo].[sp_GetBudgetItemDetail]", objProfile.GetOptionsDllDBName());
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ((System.Byte)(0)), ((System.Byte)(0)), "", System.Data.DataRowVersion.Current, null));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@BUDGETITEM_GUID_ID", System.Data.SqlDbType.UniqueIdentifier));
                cmd.Parameters["@BUDGETITEM_GUID_ID"].Value = uuidBudgetItemID;
                System.Data.SqlClient.SqlDataReader rs = cmd.ExecuteReader();
                if (rs.HasRows)
                {
                    CBudgetItem objBudgetItem = null;
                    CBudgetItemDecode objBudgetItemDecode = null;
                    System.Guid uuidId = System.Guid.Empty;
                    while (rs.Read())
                    {
                        if (uuidId.CompareTo((System.Guid)rs["BUDGETITEM_GUID_ID"]) != 0)
                        {
                            objBudgetItem = new CBudgetItem();
                            objBudgetItem.uuidID = (System.Guid)rs["BUDGETITEM_GUID_ID"];
                            if (rs["PARENT_GUID_ID"] != System.DBNull.Value)
                            {
                                objBudgetItem.ParentID = (System.Guid)rs["PARENT_GUID_ID"];
                            }
                            objBudgetItem.BudgetItemNum = (System.String)rs["BUDGETITEM_NUM"];
                            objBudgetItem.Name = (System.String)rs["BUDGETITEM_NAME"];
                            objBudgetItem.TransprtRest = (System.Boolean)rs["BUDGETITEM_TRANSPORTREST"];
                            objBudgetItem.DontChange = (System.Boolean)rs["BUDGETITEM_DONTCHANGE"];
                            objBudgetItem.BudgetItemID = (System.Int32)rs["BUDGETITEM_ID"];
                            uuidId = (System.Guid)rs["BUDGETITEM_GUID_ID"];

                            objList.Add(objBudgetItem);
                        }

                        objBudgetItemDecode = new CBudgetItemDecode((enumMonth)rs["MONTH_ID"]);
                        objBudgetItemDecode.MoneyPermit = System.Convert.ToDouble(rs["MONEYPERMIT"]);
                        objBudgetItemDecode.MoneyFact = System.Convert.ToDouble(rs["MONEYFACT"]);
                        objBudgetItemDecode.MoneyReserve = System.Convert.ToDouble(rs["MONEYRESERVE"]);
                        objBudgetItemDecode.MoneyPlanAccept = System.Convert.ToDouble(rs["MONEYPLANACCEPT"]);
                        objBudgetItemDecode.MoneyCredit = System.Convert.ToDouble(rs["MONEYCREDIT"]); 

                        objBudgetItem.AddBudgetItemDecode(objBudgetItemDecode);
                    }
                }
                rs.Close();
                rs = null;
                cmd = null;
            }
            catch (System.Exception f)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(
                "Не удалось получить расшифровки статьи бюджета.\n\nТекст ошибки: " + f.Message, "Внимание",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
            finally // очищаем занимаемые ресурсы
            {
                DBConnection.Close();
            }

            return objList;
        }
        #endregion

        public override string ToString()
        {
            return BudgetItemFullName;
        }

    }
}
