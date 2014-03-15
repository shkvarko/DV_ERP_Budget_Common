using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;


namespace ERP_Budget.Common
{

    #region Классы для элементов управления, связанных с переменными
    /// <summary>
    /// Интерфейсный класс для связи переменной с элементом управления
    /// </summary>
    public interface IRouteVariableEdit
    {
        System.String GetValue();
        void SetDefaultValue();
    }
    /// <summary>
    /// Класс, связывающий переменную "Сумма документа" c  элементом управления
    /// </summary>
    public class CRouteVariableDocMoney : DevExpress.XtraEditors.CalcEdit, IRouteVariableEdit
    {
        public CRouteVariableDocMoney()
        {
        }
        /// <summary>
        /// Валюта суммы
        /// </summary>
        private System.String m_CurrencyCode;
        /// <summary>
        /// Валюта суммы
        /// </summary>
        public System.String CurrencyCode
        {
            get { return m_CurrencyCode; }
            set { m_CurrencyCode = value; }
        }
        /// <summary>
        /// Валюта бюджета
        /// </summary>
        private System.String m_CurrencyCodeBudget;
        /// <summary>
        /// Валюта бюджета
        /// </summary>
        public System.String CurrencyCodeBudget
        {
            get { return m_CurrencyCodeBudget; }
            set { m_CurrencyCodeBudget = value; }
        }
        /// <summary>
        /// Курс пересчета CurrencyCode в CurrencyCodeBudget
        /// </summary>
        private System.Decimal m_CurremcyRate;
        /// <summary>
        /// Курс пересчета CurrencyCode в CurrencyCodeBudget
        /// </summary>
        public System.Decimal CurrencyRate
        {
            get { return m_CurremcyRate; }
            set { m_CurremcyRate = value; }
        }

        public System.String GetValue()
        {
            if ((this.Text == "") || (this.Text == "-") || (this.Value == 0) ||
                (this.CurrencyRate == 0) || (this.CurrencyCode == "") || (this.CurrencyCodeBudget == ""))
            {
                return "";
            }
            else
            {
                System.Decimal dRet = this.Value * System.Convert.ToDecimal( this.CurrencyRate );

                return System.String.Format("{0:#### ###.00}", (System.Math.Round(dRet, 2)));
            }
        }
        public void SetDefaultValue()
        {
            this.Text = "0";
            this.Value = 0;
        }
    }
    /// <summary>
    /// Класс, связывающий переменную "Форма оплаты" c  элементом управления
    /// </summary>
    public class CRouteVariablePaymentType : DevExpress.XtraEditors.ComboBoxEdit, IRouteVariableEdit
    {
        public CRouteVariablePaymentType()
        {
        }

        public System.String GetValue()
        {
            return this.Text;
        }
        public void SetDefaultValue()
        {
            this.Text = " ";
        }

    }
    /// <summary>
    /// Класс, связывающий переменную "Тип документа" c  элементом управления
    /// </summary>
    public class CRouteVariableDocType : DevExpress.XtraEditors.ComboBoxEdit, IRouteVariableEdit
    {
        public CRouteVariableDocType()
        {
        }

        public System.String GetValue()
        {
            return this.Text;
        }
        public void SetDefaultValue()
        {
            this.Text = " ";
        }
    }
    /// <summary>
    /// Класс, связывающий переменную "Динамическое право" c  элементом управления
    /// </summary>
    public class CRouteVariableDynamicRight : DevExpress.XtraEditors.TextEdit, IRouteVariableEdit
    {
        public CRouteVariableDynamicRight()
        {
        }

        public System.String GetValue()
        {
            return this.Text;
        }
        public void SetDefaultValue()
        {
            this.Text = " ";
        }
    }
    /// <summary>
    /// Класс, связывающий переменную "Статья расходов" c  элементом управления
    /// </summary>
    public class CRouteVariableDebitArticle : DevExpress.XtraEditors.ComboBoxEdit, IRouteVariableEdit
    {
        public CRouteVariableDebitArticle()
        {
        }

        public System.String GetValue()
        {
            System.String strRet = "";
            try
            {
                System.Text.RegularExpressions.Regex rx = new Regex(@"^\d{1,}[.]{0,1}[\d|.]*", RegexOptions.Compiled | RegexOptions.IgnoreCase);
                System.Text.RegularExpressions.Match match = rx.Match(this.Text);
                if ((match != null) && (match.Success))
                {
                    strRet = match.Value;
                }
                match = null;
                rx = null;
            }
            catch
            {
                strRet = "";
            }

            return strRet;
        }
        public void SetDefaultValue()
        {
            this.Text = " ";
        }
    }
    /// <summary>
    /// Класс, связывающий переменную "Бюджетное подразделение" c  элементом управления
    /// </summary>
    public class CRouteVariableBudgetDep : DevExpress.XtraEditors.ComboBoxEdit, IRouteVariableEdit
    {
        public CRouteVariableBudgetDep()
        {
        }

        public System.String GetValue()
        {
            return this.Text;
        }
        public void SetDefaultValue()
        {
            this.Text = " ";
        }
    }
    #endregion

    /// <summary>
    /// Класс "Переменная условия выбора маршрута"
    /// </summary>
    public class CRouteVariable : IBaseListItem
    {
        #region Переменные, свойства, константы
        /// <summary>
        /// Наименование класса, с которым связана переменная
        /// </summary>
        private System.String m_strEditClassName;
        /// <summary>
        /// Наименование класса, с которым связана переменная
        /// </summary>
        public System.String EditClassName
        {
            get { return m_strEditClassName; }
            set { m_strEditClassName = value; }
        }
        /// <summary>
        /// шаблон регулярного выражения для формирования условия выбора маршрута
        /// </summary>
        private System.String m_strPatternSrc;
        /// <summary>
        /// шаблон регулярного выражения для формирования условия выбора маршрута
        /// </summary>
        public System.String PatternSrc
        {
            get { return m_strPatternSrc; }
            set { m_strPatternSrc = value; }
        }
        /// <summary>
        /// Условие для шаблона регулярного выражения
        /// </summary>
        private System.String m_strPattern;
        /// <summary>
        /// Условие для шаблона регулярного выражения
        /// </summary>
        public System.String Pattern
        {
            get { return m_strPattern; }
            set { m_strPattern = value; }
        }
        /// <summary>
        /// Тип данных
        /// </summary>
        private System.String m_strDataTypeName;
        /// <summary>
        /// Тип данных
        /// </summary>
        public System.String DataTypeName
        {
            get { return m_strDataTypeName; }
            set { m_strDataTypeName = value; }
        }
        /// <summary>
        /// текущее значение переменной
        /// </summary>
        public System.String m_strValue;
        /// <summary>
        /// предыдущее значение переменной
        /// </summary>
        public System.String m_strPreviousValue;

        #endregion

        #region Конструкторы
        public CRouteVariable()
        {
            this.m_uuidID = System.Guid.Empty;
            this.m_strName = "";
            this.m_strEditClassName = "";
            this.m_strPatternSrc = "";
            this.m_strPattern = "";
            this.m_strDataTypeName = "";
            this.m_strValue = "";
            m_strPreviousValue = System.String.Empty;
        }

        public CRouteVariable(System.Guid uuidRouteVariableID)
        {
            this.m_uuidID = uuidRouteVariableID;
            this.m_strName = "";
            this.m_strEditClassName = "";
            this.m_strPatternSrc = "";
            this.m_strPattern = "";
            this.m_strDataTypeName = "";
            this.m_strValue = "";
            m_strPreviousValue = System.String.Empty;
        }

        public CRouteVariable(System.Guid uuidRouteVariableID, System.String strRouteVariableName)
        {
            this.m_uuidID = uuidRouteVariableID;
            this.m_strName = strRouteVariableName;
            this.m_strEditClassName = "";
            this.m_strPatternSrc = "";
            this.m_strPattern = "";
            this.m_strDataTypeName = "";
            this.m_strValue = "";
            m_strPreviousValue = System.String.Empty;
        }

        public CRouteVariable(System.Guid uuidRouteVariableID, System.String strRouteVariableName,
            System.String strEditClassName, System.String strPatternSrc, System.String strPattern,
            System.String strDataTypeName)
        {
            this.m_uuidID = uuidRouteVariableID;
            this.m_strName = strRouteVariableName;
            this.m_strEditClassName = strEditClassName;
            this.m_strPatternSrc = strPatternSrc;
            this.m_strPattern = strPattern;
            this.m_strDataTypeName = strDataTypeName;
            this.m_strValue = "";
            m_strPreviousValue = System.String.Empty;
        }
        #endregion

        #region Список переменных
        /// <summary>
        /// Возвращает список переменных условий выбора маршрута для бюджетного документа
        /// </summary>
        /// <param name="objProfile">профайл</param>
        /// <returns>список условий выбора маршрута</returns>
        public static System.Collections.Generic.List<CRouteVariable> GetRouteVariableList(UniXP.Common.CProfile objProfile)
        {
            System.Collections.Generic.List<CRouteVariable> objList = new List<CRouteVariable>();
            try
            {
                System.Data.SqlClient.SqlConnection DBConnection = objProfile.GetDBSource();
                if (DBConnection == null) { return objList; }

                // соединение с БД получено, прописываем команду на выборку данных
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
                cmd.Connection = DBConnection;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = System.String.Format("[{0}].[dbo].[sp_GetRouteVariable]", objProfile.GetOptionsDllDBName());
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ((System.Byte)(0)), ((System.Byte)(0)), "", System.Data.DataRowVersion.Current, null));
                System.Data.SqlClient.SqlDataReader rs = cmd.ExecuteReader();
                if (rs.HasRows)
                {
                    while (rs.Read())
                    {
                        objList.Add(new CRouteVariable(rs.GetGuid(0), rs.GetString(1), rs.GetString(2),
                                                         rs.GetString(3), rs.GetString(4), rs.GetString(5)));
                    }
                }
                cmd.Dispose();
                rs.Dispose();
                DBConnection.Close();
            }
            catch (System.Exception f)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(
                "Не удалось получить список список переменных условий выбора маршрута.\n\nТекст ошибки: " + f.Message, "Внимание",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
            return objList;
        }
        /// <summary>
        /// Возвращает список переменных условий выбора маршрута для бюджетного документа
        /// </summary>
        /// <param name="objProfile">профайл</param>
        /// <param name="cmd">SQL - команда</param>
        /// <returns>список переменных</returns>
        public static List<CRouteVariable> GetRouteVariableList(System.Data.SqlClient.SqlCommand cmd,
            UniXP.Common.CProfile objProfile)
        {
            System.Collections.Generic.List<CRouteVariable> objList = new List<CRouteVariable>();
            try
            {
                if (cmd == null) { return objList; }

                cmd.Parameters.Clear();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = System.String.Format("[{0}].[dbo].[sp_GetRouteVariable]", objProfile.GetOptionsDllDBName());
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ((System.Byte)(0)), ((System.Byte)(0)), "", System.Data.DataRowVersion.Current, null));
                System.Data.SqlClient.SqlDataReader rs = cmd.ExecuteReader();
                if (rs.HasRows)
                {
                    while (rs.Read())
                    {
                        objList.Add(new CRouteVariable(rs.GetGuid(0), rs.GetString(1), rs.GetString(2),
                                           rs.GetString(3), rs.GetString(4), rs.GetString(5)));
                    }
                }
                rs.Close();
                rs.Dispose();
            }
            catch (System.Exception f)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(
                "Не удалось получить список список переменных условий выбора маршрута.\n\nТекст ошибки: " + f.Message, "Внимание",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
            return objList;
        }
        #endregion

        #region Init
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
                cmd.CommandText = System.String.Format("[{0}].[dbo].[sp_GetRouteVariable]", objProfile.GetOptionsDllDBName());
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ((System.Byte)(0)), ((System.Byte)(0)), "", System.Data.DataRowVersion.Current, null));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@GUID_ID", System.Data.SqlDbType.UniqueIdentifier));
                cmd.Parameters["@GUID_ID"].Value = uuidID;
                System.Data.SqlClient.SqlDataReader rs = cmd.ExecuteReader();
                if (rs.HasRows)
                {
                    // набор данных непустой, в нем нас интересует одна запись
                    rs.Read();
                    this.m_uuidID = rs.GetGuid(0);
                    this.m_strName = rs.GetString(1);
                    this.m_strEditClassName = rs.GetString(2);
                    this.m_strPatternSrc = rs.GetString(3);
                    this.m_strPattern = rs.GetString(4);
                    this.m_strDataTypeName = rs.GetString(5);
                    bRet = true;
                }
                else
                {
                    DevExpress.XtraEditors.XtraMessageBox.Show(
                    "Не удалось проинициализировать класс CRouteVariable.\nВ БД не найдена информация.", "Внимание",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);

                }
                cmd.Dispose();
                rs.Dispose();
            }
            catch (System.Exception e)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(
                "Не удалось проинициализировать класс CRouteVariable.\n" + e.Message, "Внимание",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
			finally // очищаем занимаемые ресурсы
            {
                DBConnection.Close();
            }
            return bRet;
        }

        /// <summary>
        /// Инициализация свойств класса
        /// </summary>
        /// <param name="cmd">SQL-команда</param>
        /// <param name="objProfile">профайл</param>
        /// <param name="uuidID">уникальный идентификатор класса</param>
        /// <returns>true - успешная инициализация; false - ошибка</returns>
        public System.Boolean Init(System.Data.SqlClient.SqlCommand cmd,
            UniXP.Common.CProfile objProfile, System.Guid uuidID)
        {
            System.Boolean bRet = false;
            if ((cmd == null) || (cmd.Connection == null)) { return bRet; }

            try
            {
                // соединение с БД получено, прописываем команду на выборку данных
                cmd.Parameters.Clear();
                cmd.CommandText = System.String.Format("[{0}].[dbo].[sp_GetRouteVariable]", objProfile.GetOptionsDllDBName());
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ((System.Byte)(0)), ((System.Byte)(0)), "", System.Data.DataRowVersion.Current, null));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@GUID_ID", System.Data.SqlDbType.UniqueIdentifier));
                cmd.Parameters["@GUID_ID"].Value = uuidID;
                System.Data.SqlClient.SqlDataReader rs = cmd.ExecuteReader();
                if (rs.HasRows)
                {
                    // набор данных непустой, в нем нас интересует одна запись
                    rs.Read();
                    this.m_uuidID = rs.GetGuid(0);
                    this.m_strName = rs.GetString(1);
                    this.m_strEditClassName = rs.GetString(2);
                    this.m_strPatternSrc = rs.GetString(3);
                    this.m_strPattern = rs.GetString(4);
                    this.m_strDataTypeName = rs.GetString(5);
                    bRet = true;
                }
                else
                {
                    DevExpress.XtraEditors.XtraMessageBox.Show(
                    "Не удалось проинициализировать класс CRouteVariable.\nВ БД не найдена информация.", "Внимание",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);

                }
                rs.Dispose();
            }
            catch (System.Exception e)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(
                "Не удалось проинициализировать класс CRouteVariable.\n" + e.Message, "Внимание",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
			finally // очищаем занимаемые ресурсы
            {
            }
            return bRet;
        }
        #endregion

        #region Remove
        /// <summary>
        /// Удалить запись из БД
        /// </summary>
        /// <param name="objProfile">профайл</param>
        /// <param name="uuidID">уникальный идентификатор объекта</param>
        /// <returns>true - удачное завершение; false - ошибка</returns>
        public override System.Boolean Remove(UniXP.Common.CProfile objProfile)
        {
            System.Boolean bRet = false;
            // уникальный идентификатор не должен быть пустым
            if (this.m_uuidID == System.Guid.Empty)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show("Недопустимое значение уникального идентификатора объекта", "Внимание",
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
                cmd.CommandText = System.String.Format("[{0}].[dbo].[sp_DeleteRouteVariable]", objProfile.GetOptionsDllDBName());
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
                                DevExpress.XtraEditors.XtraMessageBox.Show("На запись с заданным идентификатором есть ссылка \nв таблице условий выбора шаблонов маршрутов.\n" + this.uuidID.ToString(), "Внимание",
                                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);
                                break;
                            }
                        default:
                            {
                                DevExpress.XtraEditors.XtraMessageBox.Show("Ошибка выполнения запроса \nна удаление переменной условия выбора маршрута.\nПеременная : " + this.m_strName, "Внимание",
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
                "Ошибка выполнения запроса \nна удаление переменной условия выбора маршрута.\nПеременная : " +
                this.m_strName + "\n" + e.Message, "Внимание",
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
        /// <param name="uuidID">уникальный идентификатор объекта</param>
        /// <returns>true - удачное завершение; false - ошибка</returns>
        public System.Boolean Remove(System.Data.SqlClient.SqlCommand cmd, UniXP.Common.CProfile objProfile)
        {
            System.Boolean bRet = false;
            if ((cmd == null) || (cmd.Connection == null)) { return bRet; }
            // уникальный идентификатор не должен быть пустым
            if (this.m_uuidID == System.Guid.Empty)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show("Недопустимое значение уникального идентификатора объекта", "Внимание",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                return bRet;
            }

            try
            {
                cmd.Parameters.Clear();
                cmd.CommandText = System.String.Format("[{0}].[dbo].[sp_DeleteRouteVariable]", objProfile.GetOptionsDllDBName());
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ((System.Byte)(0)), ((System.Byte)(0)), "", System.Data.DataRowVersion.Current, null));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@GUID_ID", System.Data.SqlDbType.UniqueIdentifier));
                cmd.Parameters["@GUID_ID"].Value = this.m_uuidID;
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
                            {
                                DevExpress.XtraEditors.XtraMessageBox.Show("На запись с заданным идентификатором есть ссылка \nв таблице условий выбора шаблонов маршрутов.\n" + this.uuidID.ToString(), "Внимание",
                                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);
                                break;
                            }
                        default:
                            {
                                DevExpress.XtraEditors.XtraMessageBox.Show("Ошибка выполнения запроса \nна удаление переменной условия выбора маршрута.\nПеременная : " + this.m_strName, "Внимание",
                                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                                break;
                            }
                    }
                }
            }
            catch (System.Exception e)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(
                "Ошибка выполнения запроса \nна удаление переменной условия выбора маршрута.\nПеременная : " +
                this.m_strName + "\n" + e.Message, "Внимание",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
			finally // очищаем занимаемые ресурсы
            {
            }
            return bRet;
        }
        #endregion

        #region Add
        /// <summary>
        /// Добавить запись в БД
        /// </summary>
        /// <param name="objProfile">профайл</param>
        /// <returns>true - удачное завершение; false - ошибка</returns>
        public override System.Boolean Add(UniXP.Common.CProfile objProfile)
        {
            System.Boolean bRet = false;

            // наименование не должен быть пустым
            if (this.m_strName == "")
            {
                DevExpress.XtraEditors.XtraMessageBox.Show("Недопустимое значение наименования объекта", "Внимание",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                return bRet;
            }

            // наименование класса не должно быть пустым
            if (this.m_strEditClassName.Trim() == "")
            {
                DevExpress.XtraEditors.XtraMessageBox.Show("Наименование класса, связанного с переменной не должно быть пустым" + this.m_strEditClassName, "Внимание",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                return bRet;
            }

            // наименование типа данных не должно быть пустым
            if (this.m_strDataTypeName.Trim() == "")
            {
                DevExpress.XtraEditors.XtraMessageBox.Show("Наименование типа данных не должно быть пустым" + this.m_strDataTypeName, "Внимание",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                return bRet;
            }

            // шаблон не должно быть пустым
            if (this.m_strPatternSrc.Trim() == "")
            {
                DevExpress.XtraEditors.XtraMessageBox.Show("Шаблон регулярного выражения для создания условия выбора маршрута\nне должен быть пустым" + this.m_strPatternSrc, "Внимание",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                return bRet;
            }

            // шаблон не должно быть пустым
            if (this.m_strPattern.Trim() == "")
            {
                DevExpress.XtraEditors.XtraMessageBox.Show("Шаблон регулярного выражения \nдля проверки выполнения условия выбора маршрута \nне должен быть пустым" + this.m_strPattern, "Внимание",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                return bRet;
            }

            System.Data.SqlClient.SqlConnection DBConnection = objProfile.GetDBSource();
            if (DBConnection == null) { return bRet; }
            System.Data.SqlClient.SqlTransaction DBTransaction = DBConnection.BeginTransaction();
            try
            {
                // соединение с БД получено, прописываем команду на создание записи в БД
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
                cmd.Connection = DBConnection;
                cmd.Transaction = DBTransaction;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = System.String.Format("[{0}].[dbo].[sp_AddRouteVariable]", objProfile.GetOptionsDllDBName());
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ((System.Byte)(0)), ((System.Byte)(0)), "", System.Data.DataRowVersion.Current, null));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@GUID_ID", System.Data.SqlDbType.UniqueIdentifier, 4, System.Data.ParameterDirection.Output, false, ((System.Byte)(0)), ((System.Byte)(0)), "", System.Data.DataRowVersion.Current, null));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ROUTEVARIABLE_NAME", System.Data.DbType.String));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ROUTEVARIABLE_CLASSNAME", System.Data.DbType.String));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ROUTEVARIABLE_PATTERNSRC", System.Data.DbType.String));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ROUTEVARIABLE_PATTERN", System.Data.DbType.String));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@DATATYPE_NAME", System.Data.DbType.String));
                cmd.Parameters["@ROUTEVARIABLE_NAME"].Value = this.m_strName;
                cmd.Parameters["@ROUTEVARIABLE_CLASSNAME"].Value = this.m_strEditClassName;
                cmd.Parameters["@ROUTEVARIABLE_PATTERNSRC"].Value = this.m_strPatternSrc;
                cmd.Parameters["@ROUTEVARIABLE_PATTERN"].Value = this.m_strPattern;
                cmd.Parameters["@DATATYPE_NAME"].Value = this.m_strDataTypeName;
                cmd.ExecuteNonQuery();
                System.Int32 iRet = (System.Int32)cmd.Parameters["@RETURN_VALUE"].Value;
                if (iRet == 0)
                {
                    // подтверждаем транзакцию
                    DBTransaction.Commit();
                    this.m_uuidID = (System.Guid)cmd.Parameters["@GUID_ID"].Value;
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
                                DevExpress.XtraEditors.XtraMessageBox.Show("Переменная для условия выбора маршрута с заданным именем существует : '" + this.m_strName + "'", "Внимание",
                                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);
                                break;
                            }
                        default:
                            {
                                DevExpress.XtraEditors.XtraMessageBox.Show("Ошибка создания переменной : " + this.m_strName, "Ошибка",
                                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                                break;
                            }
                    }
                }
                cmd.Dispose();
            }
            catch (System.Exception e)
            {
                // откатываем транзакцию
                DBTransaction.Rollback();
                DevExpress.XtraEditors.XtraMessageBox.Show(
                "Не удалось создать переменную\n" + this.m_strName + "\n" + e.Message, "Внимание",
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
        /// <param name="cmd">SQL-команда</param>
        /// <param name="objProfile">профайл</param>
        /// <returns>true - удачное завершение; false - ошибка</returns>
        public System.Boolean Add(System.Data.SqlClient.SqlCommand cmd, UniXP.Common.CProfile objProfile)
        {
            System.Boolean bRet = false;
            if ((cmd == null) || (cmd.Connection == null)) { return bRet; }

            // наименование не должен быть пустым
            if (this.m_strName == "")
            {
                DevExpress.XtraEditors.XtraMessageBox.Show("Недопустимое значение наименования объекта", "Внимание",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                return bRet;
            }

            // наименование класса не должно быть пустым
            if (this.m_strEditClassName.Trim() == "")
            {
                DevExpress.XtraEditors.XtraMessageBox.Show("Наименование класса, связанного с переменной не должно быть пустым" + this.m_strEditClassName, "Внимание",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                return bRet;
            }

            // наименование типа данных не должно быть пустым
            if (this.m_strDataTypeName.Trim() == "")
            {
                DevExpress.XtraEditors.XtraMessageBox.Show("Наименование типа данных не должно быть пустым" + this.m_strDataTypeName, "Внимание",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                return bRet;
            }

            // шаблон не должно быть пустым
            if (this.m_strPatternSrc.Trim() == "")
            {
                DevExpress.XtraEditors.XtraMessageBox.Show("Шаблон регулярного выражения для создания условия выбора маршрута\nне должен быть пустым" + this.m_strPatternSrc, "Внимание",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                return bRet;
            }

            // шаблон не должно быть пустым
            if (this.m_strPattern.Trim() == "")
            {
                DevExpress.XtraEditors.XtraMessageBox.Show("Шаблон регулярного выражения \nдля проверки выполнения условия выбора маршрута \nне должен быть пустым" + this.m_strPattern, "Внимание",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                return bRet;
            }

            try
            {
                cmd.Parameters.Clear();
                cmd.CommandText = System.String.Format("[{0}].[dbo].[sp_AddRouteVariable]", objProfile.GetOptionsDllDBName());
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ((System.Byte)(0)), ((System.Byte)(0)), "", System.Data.DataRowVersion.Current, null));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@GUID_ID", System.Data.SqlDbType.UniqueIdentifier, 4, System.Data.ParameterDirection.Output, false, ((System.Byte)(0)), ((System.Byte)(0)), "", System.Data.DataRowVersion.Current, null));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ROUTEVARIABLE_NAME", System.Data.DbType.String));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ROUTEVARIABLE_CLASSNAME", System.Data.DbType.String));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ROUTEVARIABLE_PATTERNSRC", System.Data.DbType.String));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ROUTEVARIABLE_PATTERN", System.Data.DbType.String));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@DATATYPE_NAME", System.Data.DbType.String));
                cmd.Parameters["@ROUTEVARIABLE_NAME"].Value = this.m_strName;
                cmd.Parameters["@ROUTEVARIABLE_CLASSNAME"].Value = this.m_strEditClassName;
                cmd.Parameters["@ROUTEVARIABLE_PATTERNSRC"].Value = this.m_strPatternSrc;
                cmd.Parameters["@ROUTEVARIABLE_PATTERN"].Value = this.m_strPattern;
                cmd.Parameters["@DATATYPE_NAME"].Value = this.m_strDataTypeName;
                cmd.ExecuteNonQuery();
                System.Int32 iRet = (System.Int32)cmd.Parameters["@RETURN_VALUE"].Value;
                if (iRet == 0)
                {
                    this.m_uuidID = (System.Guid)cmd.Parameters["@GUID_ID"].Value;
                    bRet = true;
                }
                else
                {
                    switch (iRet)
                    {
                        case 1:
                            {
                                DevExpress.XtraEditors.XtraMessageBox.Show("Переменная для условия выбора маршрута с заданным именем существует : '" + this.m_strName + "'", "Внимание",
                                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);
                                break;
                            }
                        default:
                            {
                                DevExpress.XtraEditors.XtraMessageBox.Show("Ошибка создания переменной : " + this.m_strName, "Ошибка",
                                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                                break;
                            }
                    }
                }
            }
            catch (System.Exception e)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(
                "Не удалось создать переменную\n" + this.m_strName + "\n" + e.Message, "Внимание",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
			finally // очищаем занимаемые ресурсы
            {
            }
            return bRet;
        }
        #endregion

        #region Update
        /// <summary>
        /// Сохранить изменения в БД
        /// </summary>
        /// <param name="objProfile">профайл</param>
        /// <returns>true - удачное завершение; false - ошибка</returns>
        public override System.Boolean Update(UniXP.Common.CProfile objProfile)
        {
            System.Boolean bRet = false;

            // уникальный идентификатор не должен быть пустым
            if (this.m_uuidID == System.Guid.Empty)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show("Недопустимое значение уникального идентификатора объекта", "Внимание",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                return bRet;
            }
            // наименование не должен быть пустым
            if (this.m_strName == "")
            {
                DevExpress.XtraEditors.XtraMessageBox.Show("Недопустимое значение наименования объекта", "Внимание",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                return bRet;
            }
            // наименование класса не должно быть пустым
            if (this.m_strEditClassName.Trim() == "")
            {
                DevExpress.XtraEditors.XtraMessageBox.Show("Наименование класса, связанного с переменной не должно быть пустым" + this.m_strEditClassName, "Внимание",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                return bRet;
            }
            // наименование типа данных не должно быть пустым
            if (this.m_strDataTypeName.Trim() == "")
            {
                DevExpress.XtraEditors.XtraMessageBox.Show("Наименование типа данных не должно быть пустым" + this.m_strDataTypeName, "Внимание",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                return bRet;
            }

            // шаблон не должно быть пустым
            if (this.m_strPatternSrc.Trim() == "")
            {
                DevExpress.XtraEditors.XtraMessageBox.Show("Шаблон регулярного выражения для создания условия выбора маршрута\nне должен быть пустым" + this.m_strPatternSrc, "Внимание",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                return bRet;
            }

            // шаблон не должно быть пустым
            if (this.m_strPattern.Trim() == "")
            {
                DevExpress.XtraEditors.XtraMessageBox.Show("Шаблон регулярного выражения \nдля проверки выполнения условия выбора маршрута \nне должен быть пустым" + this.m_strPattern, "Внимание",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                return bRet;
            }

            System.Data.SqlClient.SqlConnection DBConnection = objProfile.GetDBSource();
            if (DBConnection == null) { return bRet; }
            System.Data.SqlClient.SqlTransaction DBTransaction = DBConnection.BeginTransaction();
            try
            {
                // соединение с БД получено, прописываем команду на создание записи в БД
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
                cmd.Connection = DBConnection;
                cmd.Transaction = DBTransaction;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = System.String.Format("[{0}].[dbo].[sp_EditRouteVariable]", objProfile.GetOptionsDllDBName());
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ((System.Byte)(0)), ((System.Byte)(0)), "", System.Data.DataRowVersion.Current, null));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@GUID_ID", System.Data.SqlDbType.UniqueIdentifier));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ROUTEVARIABLE_NAME", System.Data.DbType.String));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ROUTEVARIABLE_CLASSNAME", System.Data.DbType.String));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ROUTEVARIABLE_PATTERNSRC", System.Data.DbType.String));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ROUTEVARIABLE_PATTERN", System.Data.DbType.String));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@DATATYPE_NAME", System.Data.DbType.String));
                cmd.Parameters["@GUID_ID"].Value = this.m_uuidID;
                cmd.Parameters["@ROUTEVARIABLE_NAME"].Value = this.m_strName;
                cmd.Parameters["@ROUTEVARIABLE_CLASSNAME"].Value = this.m_strEditClassName;
                cmd.Parameters["@ROUTEVARIABLE_PATTERNSRC"].Value = this.m_strPatternSrc;
                cmd.Parameters["@ROUTEVARIABLE_PATTERN"].Value = this.m_strPattern;
                cmd.Parameters["@DATATYPE_NAME"].Value = this.m_strDataTypeName;
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
                            DevExpress.XtraEditors.XtraMessageBox.Show("Переменная для условия выбора маршрута с заданным именем существует : '" + this.m_strName + "'", "Внимание",
                                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);
                            break;
                        case 2:
                            DevExpress.XtraEditors.XtraMessageBox.Show("Запись с указанным идентификатором не найдена \n" + this.m_uuidID.ToString(), "Внимание",
                                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                            break;
                        default:
                            DevExpress.XtraEditors.XtraMessageBox.Show("Ошибка изменения свойств переменной условия выбора маршрута : " + this.m_strName, "Внимание",
                                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                            break;
                    }
                }
                cmd.Dispose();
            }
            catch (System.Exception e)
            {
                // откатываем транзакцию
                DBTransaction.Rollback();
                DevExpress.XtraEditors.XtraMessageBox.Show(
                "Ошибка изменения свойств переменной условия выбора маршрута : " + this.m_strName + "\n" + e.Message, "Внимание",
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
        /// <param name="cmd">SQL-команда</param>
        /// <param name="objProfile">профайл</param>
        /// <returns>true - удачное завершение; false - ошибка</returns>
        public System.Boolean Update(System.Data.SqlClient.SqlCommand cmd, UniXP.Common.CProfile objProfile)
        {
            System.Boolean bRet = false;
            if ((cmd == null) || (cmd.Connection == null)) { return bRet; }

            // уникальный идентификатор не должен быть пустым
            if (this.m_uuidID == System.Guid.Empty)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show("Недопустимое значение уникального идентификатора объекта", "Внимание",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                return bRet;
            }
            // наименование не должен быть пустым
            if (this.m_strName == "")
            {
                DevExpress.XtraEditors.XtraMessageBox.Show("Недопустимое значение наименования объекта", "Внимание",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                return bRet;
            }
            // наименование класса не должно быть пустым
            if (this.m_strEditClassName.Trim() == "")
            {
                DevExpress.XtraEditors.XtraMessageBox.Show("Наименование класса, связанного с переменной не должно быть пустым" + this.m_strEditClassName, "Внимание",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                return bRet;
            }
            // наименование типа данных не должно быть пустым
            if (this.m_strDataTypeName.Trim() == "")
            {
                DevExpress.XtraEditors.XtraMessageBox.Show("Наименование типа данных не должно быть пустым" + this.m_strDataTypeName, "Внимание",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                return bRet;
            }

            // шаблон не должно быть пустым
            if (this.m_strPatternSrc.Trim() == "")
            {
                DevExpress.XtraEditors.XtraMessageBox.Show("Шаблон регулярного выражения для создания условия выбора маршрута\nне должен быть пустым" + this.m_strPatternSrc, "Внимание",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                return bRet;
            }

            // шаблон не должно быть пустым
            if (this.m_strPattern.Trim() == "")
            {
                DevExpress.XtraEditors.XtraMessageBox.Show("Шаблон регулярного выражения \nдля проверки выполнения условия выбора маршрута \nне должен быть пустым" + this.m_strPattern, "Внимание",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                return bRet;
            }

            try
            {
                cmd.Parameters.Clear();
                cmd.CommandText = System.String.Format("[{0}].[dbo].[sp_EditRouteVariable]", objProfile.GetOptionsDllDBName());
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ((System.Byte)(0)), ((System.Byte)(0)), "", System.Data.DataRowVersion.Current, null));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@GUID_ID", System.Data.SqlDbType.UniqueIdentifier));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ROUTEVARIABLE_NAME", System.Data.DbType.String));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ROUTEVARIABLE_CLASSNAME", System.Data.DbType.String));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ROUTEVARIABLE_PATTERNSRC", System.Data.DbType.String));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ROUTEVARIABLE_PATTERN", System.Data.DbType.String));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@DATATYPE_NAME", System.Data.DbType.String));
                cmd.Parameters["@GUID_ID"].Value = this.m_uuidID;
                cmd.Parameters["@ROUTEVARIABLE_NAME"].Value = this.m_strName;
                cmd.Parameters["@ROUTEVARIABLE_CLASSNAME"].Value = this.m_strEditClassName;
                cmd.Parameters["@ROUTEVARIABLE_PATTERNSRC"].Value = this.m_strPatternSrc;
                cmd.Parameters["@ROUTEVARIABLE_PATTERN"].Value = this.m_strPattern;
                cmd.Parameters["@DATATYPE_NAME"].Value = this.m_strDataTypeName;
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
                            DevExpress.XtraEditors.XtraMessageBox.Show("Переменная для условия выбора маршрута с заданным именем существует : '" + this.m_strName + "'", "Внимание",
                                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);
                            break;
                        case 2:
                            DevExpress.XtraEditors.XtraMessageBox.Show("Запись с указанным идентификатором не найдена \n" + this.m_uuidID.ToString(), "Внимание",
                                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                            break;
                        default:
                            DevExpress.XtraEditors.XtraMessageBox.Show("Ошибка изменения свойств переменной условия выбора маршрута : " + this.m_strName, "Внимание",
                                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                            break;
                    }
                }
            }
            catch (System.Exception e)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(
                "Ошибка изменения свойств переменной условия выбора маршрута : " + this.m_strName + "\n" + e.Message, "Внимание",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
			finally // очищаем занимаемые ресурсы
            {
            }
            return bRet;
        }
        #endregion
    }
}
