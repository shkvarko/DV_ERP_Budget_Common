using System;
using System.Collections.Generic;
using System.Text;

namespace ERP_Budget.Common
{
    /// <summary>
    /// Класс "Бюджет"
    /// </summary>
    public class CBudget : IBaseListItem
    {
        #region Переменные, Свойства, Константы
        /// <summary>
        /// Бюджетное подразделение
        /// </summary>
        private CBudgetDep m_objBudgetDep;
        /// <summary>
        /// Бюджетное подразделение
        /// </summary>
        public CBudgetDep BudgetDep
        {
            get { return m_objBudgetDep; }
            set { m_objBudgetDep = value; }
        }
        /// <summary>
        /// Период
        /// </summary>
        private System.DateTime m_dtDate;
        /// <summary>
        /// Период
        /// </summary>
        public System.DateTime Date
        {
            get { return m_dtDate; }
            set { m_dtDate = value; }
        }
        /// <summary>
        /// Список статей расходов
        /// </summary>
        private List<CBudgetItem> m_objBudgetItemList;
        /// <summary>
        /// Список статей расходов
        /// </summary>
        public List<ERP_Budget.Common.CBudgetItem> BudgetItemList
        {
            get { return m_objBudgetItemList; }
            //set { m_objBudgetItemList = value; }
        }
        /// <summary>
        /// Дата утверждения
        /// </summary>
        private System.DateTime m_dtAcceptDate;
        /// <summary>
        /// Дата утверждения
        /// </summary>
        public System.DateTime AcceptDate
        {
            get { return m_dtAcceptDate; }
            set { m_dtAcceptDate = value; }
        }
        /// <summary>
        /// Признак "Бюджет утвержден"
        /// </summary>
        private System.Boolean m_IsAccept;
        /// <summary>
        /// Признак "Бюджет утвержден"
        /// </summary>
        public System.Boolean IsAccept
        {
            get { return m_IsAccept; }
            set { m_IsAccept = value; }
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
        /// Валюта бюджета
        /// </summary>
        private CCurrency m_objCurrency;
        /// <summary>
        /// Валюта бюджета
        /// </summary>
        public CCurrency Currency
        {
            get { return m_objCurrency; }
            set { m_objCurrency = value; }
        }
        /// <summary>
        /// Список курсов валют бюджета
        /// </summary>
        private CBudgetCurrencyRate m_objBudgetCurrencyRate;
        /// <summary>
        /// Список курсов валют бюджета
        /// </summary>
        public CBudgetCurrencyRate BudgetCurrencyRate
        {
            get { return m_objBudgetCurrencyRate; }
            set { m_objBudgetCurrencyRate = value; }
        }
        /// <summary>
        /// Проект
        /// </summary>
        public CBudgetProject BudgetProject { get; set; }
        /// <summary>
        /// Тип бюджета
        /// </summary>
        public CBudgetType BudgetType { get; set; }
        /// <summary>
        /// Доступ ограничен
        /// </summary>
        public System.Boolean IsAccessLimited { get; set; }
        /// <summary>
        /// Доступ ограничен
        /// </summary>
        public System.String IsAccessLimitedName 
        { get { return ( IsAccessLimited ? "назначен список распорядителей..." : System.String.Empty ); }  }
        #endregion

        #region Конструктор

        public CBudget()
        {
            this.m_uuidID = System.Guid.Empty;
            this.m_strName = "";
            this.m_dtDate = new DateTime(System.DateTime.Today.Year + 1, 1, 1);
            this.m_objBudgetDep = new CBudgetDep();
            this.m_objBudgetItemList = new List<CBudgetItem>();
            this.m_IsAccept = false;
            this.m_objCurrency = null;
            this.m_objBudgetCurrencyRate = null;
            this.m_OffExpenditures = false;
            BudgetProject = null;
            BudgetType = null;
            IsAccessLimited = false;
        }

        public CBudget(System.Guid uuidID, System.Guid BudgetDepID)
        {
            this.m_uuidID = uuidID;
            this.m_strName = "";
            this.m_dtDate = new DateTime(System.DateTime.Today.Year + 1, 1, 1);
            this.m_objBudgetDep = new CBudgetDep(BudgetDepID);
            this.m_objBudgetItemList = new List<CBudgetItem>();
            this.m_IsAccept = false;
            this.m_objCurrency = null;
            this.m_objBudgetCurrencyRate = null;
            this.m_OffExpenditures = false;
            BudgetProject = null;
            BudgetType = null;
            IsAccessLimited = false;
        }
        #endregion

        #region Список объектов "Бюджет"
        /// <summary>
        /// Возвращает список бюджетов 
        /// </summary>
        /// <param name="objProfile">профайл</param>
        /// <returns>список бюджетов </returns>
        public static List<CBudget> GetBudgetList(UniXP.Common.CProfile objProfile, System.Boolean bLoadCurrencyRateList = true)
        {
            List<CBudget> objList = new System.Collections.Generic.List<CBudget>();

            try
            {
                System.Data.SqlClient.SqlConnection DBConnection = objProfile.GetDBSource();
                if (DBConnection == null)
                {
                    DevExpress.XtraEditors.XtraMessageBox.Show("Отсутствует соединение с БД.", "Внимание",
                        System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);
                    return objList;
                }

                // соединение с БД получено, прописываем команду на выборку данных
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand() { Connection = DBConnection, 
                    CommandType = System.Data.CommandType.StoredProcedure, 
                    CommandText = System.String.Format("[{0}].[dbo].[sp_GetBudget]", objProfile.GetOptionsDllDBName()) };
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ((System.Byte)(0)), ((System.Byte)(0)), "", System.Data.DataRowVersion.Current, null));
                System.Data.SqlClient.SqlDataReader rs = cmd.ExecuteReader();
                if (rs.HasRows)
                {
                    // набор данных непустой
                    CBudget objBudget = null;
                    while (rs.Read())
                    {
                        objBudget = new CBudget();

                        objBudget.m_uuidID = (System.Guid)rs["GUID_ID"];
                        objBudget.m_strName = System.Convert.ToString( rs["BUDGET_NAME"] );
                        objBudget.m_dtDate = System.Convert.ToDateTime( rs["BUDGET_DATE"] );
                        if (rs["BUDGET_ACCEPT"] != System.DBNull.Value)
                        {
                            objBudget.m_IsAccept = (System.Boolean)rs["BUDGET_ACCEPT"];
                        }
                        if (rs["BUDGET_ACCEPTDATE"] != System.DBNull.Value)
                        {
                            objBudget.m_dtAcceptDate = System.Convert.ToDateTime(rs["BUDGET_ACCEPTDATE"]);
                        }

                        objBudget.m_objBudgetDep = new CBudgetDep((System.Guid)rs["BUDGETDEP_GUID_ID"], System.Convert.ToString( rs["BUDGETDEP_NAME"] ),
                            new CUser( System.Convert.ToInt32( rs["BUDGETDEP_MANAGER"] ), System.Convert.ToInt32( rs["UniXPUserID"] ), 
                                System.Convert.ToString( rs["strLastName"] ), System.Convert.ToString( rs["strFirstName"] )));

                        if (rs["PARENT_GUID_ID"] != System.DBNull.Value) { objBudget.m_objBudgetDep.ParentID = (System.Guid)rs["PARENT_GUID_ID"]; }

                        objBudget.m_objCurrency = new CCurrency((System.Guid)rs["CURRENCY_GUID_ID"], System.Convert.ToString( rs["CURRENCY_CODE"] ), System.Convert.ToString( rs["CURRENCY_NAME"] ));
                        objBudget.m_objBudgetCurrencyRate = new CBudgetCurrencyRate(objBudget.m_uuidID);
                        objBudget.m_OffExpenditures = (System.Boolean)rs["OFFBUDGET_EXPENDITURES"];
                        objBudget.IsAccessLimited = System.Convert.ToBoolean(rs["IsBudgetAccessLimited"]);

                        if(rs["BUDGETPROJECT_GUID"] != System.DBNull.Value)
                        {
                            objBudget.BudgetProject = new CBudgetProject()
                            {
                                uuidID = (System.Guid)rs["BUDGETPROJECT_GUID"],
                                Name = System.Convert.ToString(rs["BUDGETPROJECT_NAME"]),
                                Description = "",
                                IsActive = System.Convert.ToBoolean(rs["BUDGETPROJECT_ACTIVE"]),
                                CodeIn1C = System.Convert.ToInt32(rs["BUDGETPROJECT_1C_CODE"])
                            };
                        }

                        if (rs["BUDGETTYPE_GUID"] != System.DBNull.Value)
                        {
                            objBudget.BudgetType = new CBudgetType()
                            {
                                uuidID = (System.Guid)rs["BUDGETTYPE_GUID"],
                                Name = System.Convert.ToString(rs["BUDGETTYPE_NAME"]),
                                Description = ( ( rs["BUDGETTYPE_DESCRIPTION"] != System.DBNull.Value ) ? System.Convert.ToString( rs["BUDGETTYPE_DESCRIPTION"] ) : "" ),
                                IsActive = System.Convert.ToBoolean(rs["BUDGETTYPE_ACTIVE"])
                            };
                        }
                        objList.Add(objBudget);
                    }
                }
                rs.Close();
                rs.Dispose();

                if (bLoadCurrencyRateList == true)
                {
                    // теперь загрузим курсы валют
                    System.Boolean bRet = true;
                    foreach (CBudget objBudget in objList)
                    {
                        bRet = objBudget.m_objBudgetCurrencyRate.LoadCurrencyRateList(objProfile, cmd);
                        if (bRet == false) { break; }
                    }
                }

                cmd.Dispose();
                DBConnection.Close();

                //if (bRet == false) { objList.Clear(); }
            }
            catch (System.Exception f)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(
                "Не удалось получить список бюджетов.\n\nТекст ошибки: " + f.Message, "Внимание",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
            finally // очищаем занимаемые ресурсы
            {
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
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand() 
                { 
                    Connection = DBConnection, 
                    CommandType = System.Data.CommandType.StoredProcedure, 
                    CommandText = System.String.Format("[{0}].[dbo].[sp_GetBudget]", 
                    objProfile.GetOptionsDllDBName()) 
                };
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ((System.Byte)(0)), ((System.Byte)(0)), "", System.Data.DataRowVersion.Current, null));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@GUID_ID", System.Data.SqlDbType.UniqueIdentifier));
                cmd.Parameters["@GUID_ID"].Value = uuidID;
                System.Data.SqlClient.SqlDataReader rs = cmd.ExecuteReader();
                if (rs.HasRows)
                {
                    // набор данных непустой, в нем нас интересует одна запись
                    rs.Read();

                    this.m_uuidID = (System.Guid)rs["GUID_ID"];
                    this.m_strName = System.Convert.ToString(rs["BUDGET_NAME"]);
                    this.m_dtDate = System.Convert.ToDateTime(rs["BUDGET_DATE"]);
                    this.m_IsAccept = ((rs["BUDGET_ACCEPT"] != System.DBNull.Value) ? System.Convert.ToBoolean(rs["BUDGET_ACCEPT"]) : false);
                    this.m_dtAcceptDate = ((rs["BUDGET_ACCEPTDATE"] != System.DBNull.Value) ? System.Convert.ToDateTime(rs["BUDGET_ACCEPTDATE"]) : System.DateTime.MinValue);

                    this.m_objBudgetDep = new CBudgetDep((System.Guid)rs["BUDGETDEP_GUID_ID"], System.Convert.ToString(rs["BUDGETDEP_NAME"]),
                        new CUser(System.Convert.ToInt32(rs["BUDGETDEP_MANAGER"]), System.Convert.ToInt32(rs["UniXPUserID"]),
                            System.Convert.ToString(rs["strLastName"]), System.Convert.ToString(rs["strFirstName"])));

                    if (rs["PARENT_GUID_ID"] != System.DBNull.Value) { this.m_objBudgetDep.ParentID = (System.Guid)rs["PARENT_GUID_ID"]; }

                    this.m_objCurrency = new CCurrency((System.Guid)rs["CURRENCY_GUID_ID"], System.Convert.ToString(rs["CURRENCY_CODE"]), System.Convert.ToString(rs["CURRENCY_NAME"]));
                    this.m_objBudgetCurrencyRate = new CBudgetCurrencyRate(this.m_uuidID);
                    this.m_OffExpenditures = (System.Boolean)rs["OFFBUDGET_EXPENDITURES"];

                    if (rs["BUDGETPROJECT_GUID"] != System.DBNull.Value)
                    {
                        this.BudgetProject = new CBudgetProject()
                        {
                            uuidID = (System.Guid)rs["BUDGETPROJECT_GUID"],
                            Name = System.Convert.ToString(rs["BUDGETPROJECT_NAME"]),
                            Description = "",
                            IsActive = System.Convert.ToBoolean(rs["BUDGETPROJECT_ACTIVE"]),
                            CodeIn1C = System.Convert.ToInt32(rs["BUDGETPROJECT_1C_CODE"])
                        };
                    }
                    if (rs["BUDGETTYPE_GUID"] != System.DBNull.Value)
                    {
                        this.BudgetType = new CBudgetType()
                        {
                            uuidID = (System.Guid)rs["BUDGETTYPE_GUID"],
                            Name = System.Convert.ToString(rs["BUDGETTYPE_NAME"]),
                            Description = ((rs["BUDGETTYPE_DESCRIPTION"] != System.DBNull.Value) ? System.Convert.ToString(rs["BUDGETTYPE_DESCRIPTION"]) : ""),
                            IsActive = System.Convert.ToBoolean(rs["BUDGETTYPE_ACTIVE"])
                        };
                    }
                    
                    rs.Close();
                    rs.Dispose();

                    bRet = true;
                } // if( rs.HasRows )
                else
                {
                    DevExpress.XtraEditors.XtraMessageBox.Show(
                    "Не удалось проинициализировать класс \"Бюджет\".\nВ БД не найдена информация.", "Внимание",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);
                }

                cmd.Dispose();
                DBConnection.Close();
            }
            catch (System.Exception f)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(
                "Не удалось проинициализировать класс \"Бюджет\".\n\nТекст ошибки: " + f.Message, "Внимание",
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
                cmd.CommandText = System.String.Format("[{0}].[dbo].[sp_DeleteBudget]", objProfile.GetOptionsDllDBName());
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ((System.Byte)(0)), ((System.Byte)(0)), "", System.Data.DataRowVersion.Current, null));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@GUID_ID", System.Data.SqlDbType.UniqueIdentifier));
                cmd.Parameters["@GUID_ID"].Value = this.m_uuidID;
                cmd.ExecuteNonQuery();
                System.Int32 iRet = (System.Int32)cmd.Parameters["@RETURN_VALUE"].Value;
                if (iRet == 0)
                {
                    DBTransaction.Commit();
                    bRet = true;
                }
                else
                {
                    DBTransaction.Rollback();
                    if (iRet == 1)
                    {
                        DevExpress.XtraEditors.XtraMessageBox.Show("На статью бюджета есть ссылка в бюджетном документе.\nУдаление невозможно", "Внимание",
                            System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);
                    }
                    else
                    {
                        DevExpress.XtraEditors.XtraMessageBox.Show("Ошибка удаления бюджета", "Ошибка",
                            System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    }
                }
                cmd.Dispose();
            }
            catch (System.Exception e)
            {
                DBTransaction.Rollback();
                DevExpress.XtraEditors.XtraMessageBox.Show(
                "Не удалось удалить бюджет.\n" + e.Message, "Внимание",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
			finally // очищаем занимаемые ресурсы
            {
                DBConnection.Close();
            }
            return bRet;
        }
        #endregion

        #region Add
        /// <summary>
        /// Проверяет содержимое свойств бюджета
        /// </summary>
        /// <returns>true - все свойства указаны; false - ошибка</returns>
        public System.Boolean IsValidProperties( ref System.String strErr )
        {
            System.Boolean bRet = false;
            try
            {
                // список курсов валют бюджета не дб пустым
                if ((this.m_objBudgetCurrencyRate == null) || (this.m_objBudgetCurrencyRate.BudgetCurrencyRateItemList.Count == 0))
                {
                    strErr += ( "\nСписок курсов валют бюджета не должен быть пустым." );
                    return bRet;
                }
                // должно быть указано бюджетное подразделение
                if (this.m_objBudgetDep == null)
                {
                    strErr += ("\nНе указано бюджетное подразделение.");
                    return bRet;
                }
                // должна быть указана валюта
                if (this.m_objCurrency == null)
                {
                    strErr += ("\nНе указана валюта бюджета.");
                    return bRet;
                }
                // должен быть указан проект
                if (this.BudgetProject == null)
                {
                    strErr += ("\nНе указан проект бюджета.");
                    return bRet;
                }
                // должен быть указан тип бюджета
                if (this.BudgetType == null)
                {
                    strErr += ("\nНе указан тип бюджета.");
                    return bRet;
                }
                bRet = true;
            }
            catch (System.Exception f)
            {
                strErr += ("\nОшибка проверки свойств бюджета.\nТекст ошибки: " + f.Message);
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
        public System.Boolean Add(UniXP.Common.CProfile objProfile, ref System.String strErr)
        {
            System.Boolean bRet = false;

            if (this.IsValidProperties( ref strErr ) == false) { return bRet; }

            System.Data.SqlClient.SqlConnection DBConnection = objProfile.GetDBSource();
            if (DBConnection == null) { return bRet; }
            System.Data.SqlClient.SqlTransaction DBTransaction = DBConnection.BeginTransaction();

            try
            {
                // соединение с БД получено, прописываем команду на создание записи в БД
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand() { Connection = DBConnection, Transaction = DBTransaction, CommandType = System.Data.CommandType.StoredProcedure, CommandText = System.String.Format("[{0}].[dbo].[sp_AddBudget]", objProfile.GetOptionsDllDBName()) };
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ((System.Byte)(0)), ((System.Byte)(0)), "", System.Data.DataRowVersion.Current, null));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@GUID_ID", System.Data.SqlDbType.UniqueIdentifier, 16, System.Data.ParameterDirection.Output, false, ((System.Byte)(0)), ((System.Byte)(0)), "", System.Data.DataRowVersion.Current, null));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@BUDGET_NAME", System.Data.DbType.String));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@BUDGET_DATE", System.Data.SqlDbType.DateTime));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@BUDGETDEP_GUID_ID", System.Data.SqlDbType.UniqueIdentifier));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@CURRENCY_GUID_ID", System.Data.SqlDbType.UniqueIdentifier));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@OFFBUDGET_EXPENDITURES", System.Data.SqlDbType.Bit));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@BUDGETPROJECT_GUID", System.Data.SqlDbType.UniqueIdentifier));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@BUDGETTYPE_GUID", System.Data.SqlDbType.UniqueIdentifier));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ERROR_NUM", System.Data.SqlDbType.Int, 8, System.Data.ParameterDirection.Output, false, ((System.Byte)(0)), ((System.Byte)(0)), "", System.Data.DataRowVersion.Current, null));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ERROR_MES", System.Data.SqlDbType.NVarChar, 4000));
                cmd.Parameters["@ERROR_MES"].Direction = System.Data.ParameterDirection.Output;
                if (this.m_uuidID.CompareTo(System.Guid.Empty) != 0)
                {
                    cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@BUDGET_GUID_ID", System.Data.SqlDbType.UniqueIdentifier));
                    cmd.Parameters["@BUDGET_GUID_ID"].Value = this.m_uuidID;
                }
                cmd.Parameters["@BUDGET_NAME"].Value = this.Name;
                cmd.Parameters["@BUDGET_DATE"].Value = this.m_dtDate;
                cmd.Parameters["@BUDGETDEP_GUID_ID"].Value = this.m_objBudgetDep.uuidID;
                cmd.Parameters["@CURRENCY_GUID_ID"].Value = this.m_objCurrency.uuidID;
                cmd.Parameters["@OFFBUDGET_EXPENDITURES"].Value = this.m_OffExpenditures;
                cmd.Parameters["@BUDGETPROJECT_GUID"].Value = this.BudgetProject.uuidID;
                cmd.Parameters["@BUDGETTYPE_GUID"].Value = this.BudgetType.uuidID;
                cmd.ExecuteNonQuery();
                System.Int32 iRet = (System.Int32)cmd.Parameters["@RETURN_VALUE"].Value;
                if (iRet == 0)
                {
                    this.m_uuidID = (System.Guid)cmd.Parameters["@GUID_ID"].Value;
                    // сохраняем курсы валют 
                    this.m_objBudgetCurrencyRate.BudgetID = this.m_uuidID;
                    bRet = this.m_objBudgetCurrencyRate.SaveCurrencyRateList(objProfile, cmd);

                    // подтверждаем / откатываем транзакцию
                    if (bRet) { DBTransaction.Commit(); }
                    else { DBTransaction.Rollback(); }
                }
                else
                {
                    // откатываем транзакцию
                    DBTransaction.Rollback();
                    strErr += ((cmd.Parameters["@ERROR_MES"].Value == System.DBNull.Value) ? "" : (System.String)cmd.Parameters["@ERROR_MES"].Value);
                }
                bRet = (iRet == 0);
            }
            catch (System.Exception f)
            {
                DBTransaction.Rollback();

                strErr += ("Не удалось создать объект 'бюджет'. Текст ошибки: " + f.Message);
            }
            finally
            {
                DBConnection.Close();
            }
            return bRet;
        }
        #endregion

        #region Update
        public override System.Boolean Update(UniXP.Common.CProfile objProfile)
        {
            return false;
        }

        /// <summary>
        /// Сохранить изменения в БД
        /// </summary>
        /// <param name="objProfile">профайл</param>
        /// <returns>true - удачное завершение; false - ошибка</returns>
        public System.Boolean Update(UniXP.Common.CProfile objProfile, 
            ref System.String strErr)
        {
            System.Boolean bRet = false;

            // уникальный идентификатор не должен быть пустым
            if (this.m_uuidID == System.Guid.Empty)
            {
                strErr += ("Недопустимое значение уникального идентификатора объекта.");
                return bRet;
            }

            if (IsValidProperties( ref strErr ) == false) { return bRet; }

            System.Data.SqlClient.SqlConnection DBConnection = objProfile.GetDBSource();
            if (DBConnection == null)
            {
                strErr += ("Отсутствует соединение с БД.");
                return bRet;
            }
            System.Data.SqlClient.SqlTransaction DBTransaction = DBConnection.BeginTransaction();

            try
            {
                // соединение с БД получено, прописываем команду на создание записи в БД
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand() { Connection = DBConnection, Transaction = DBTransaction, CommandType = System.Data.CommandType.StoredProcedure, CommandText = System.String.Format("[{0}].[dbo].[sp_EditBudget]", objProfile.GetOptionsDllDBName()) };
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ((System.Byte)(0)), ((System.Byte)(0)), "", System.Data.DataRowVersion.Current, null));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@GUID_ID", System.Data.SqlDbType.UniqueIdentifier));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@BUDGET_NAME", System.Data.DbType.String));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@BUDGET_DATE", System.Data.SqlDbType.DateTime));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@BUDGETDEP_GUID_ID", System.Data.SqlDbType.UniqueIdentifier));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@CURRENCY_GUID_ID", System.Data.SqlDbType.UniqueIdentifier));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@OFFBUDGET_EXPENDITURES", System.Data.SqlDbType.Bit));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@BUDGETPROJECT_GUID", System.Data.SqlDbType.UniqueIdentifier));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@BUDGETTYPE_GUID", System.Data.SqlDbType.UniqueIdentifier));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ERROR_NUM", System.Data.SqlDbType.Int, 8, System.Data.ParameterDirection.Output, false, ((System.Byte)(0)), ((System.Byte)(0)), "", System.Data.DataRowVersion.Current, null));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ERROR_MES", System.Data.SqlDbType.NVarChar, 4000));
                cmd.Parameters["@ERROR_MES"].Direction = System.Data.ParameterDirection.Output;

                cmd.Parameters["@GUID_ID"].Value = this.m_uuidID;
                cmd.Parameters["@BUDGET_NAME"].Value = this.m_strName;
                cmd.Parameters["@BUDGET_DATE"].Value = this.m_dtDate;
                cmd.Parameters["@BUDGETDEP_GUID_ID"].Value = this.m_objBudgetDep.uuidID;
                cmd.Parameters["@CURRENCY_GUID_ID"].Value = this.m_objCurrency.uuidID;
                cmd.Parameters["@OFFBUDGET_EXPENDITURES"].Value = this.m_OffExpenditures;
                cmd.Parameters["@BUDGETPROJECT_GUID"].Value = this.BudgetProject.uuidID;
                cmd.Parameters["@BUDGETTYPE_GUID"].Value = this.BudgetType.uuidID;
                cmd.ExecuteNonQuery();
                System.Int32 iRet = (System.Int32)cmd.Parameters["@RETURN_VALUE"].Value;
                if (iRet == 0)
                {
                    // сохраняем курсы валют 
                    if (this.m_objBudgetCurrencyRate.SaveCurrencyRateList(objProfile, cmd) == true)
                    {
                        iRet = 0;
                        DBTransaction.Commit();
                    }
                    else { DBTransaction.Rollback(); }
                }
                else
                {
                    // откатываем транзакцию
                    DBTransaction.Rollback();
                    strErr += ((cmd.Parameters["@ERROR_MES"].Value == System.DBNull.Value) ? "" : (System.String)cmd.Parameters["@ERROR_MES"].Value);
                }
                bRet = (iRet == 0);
            }
            catch (System.Exception f)
            {
                DBTransaction.Rollback();

                strErr += ("Не удалось изменить свойства объекта 'бюджет'. Текст ошибки: " + f.Message);
            }
			finally // очищаем занимаемые ресурсы
            {
                DBConnection.Close();
            }
            return bRet;
        }


        /// <summary>
        /// Сохраняет в БД расшифровки статьей бюджета
        /// </summary>
        /// <param name="objTreeList">дерево статей бюджета</param>
        /// <param name="objProfile">профайл</param>
        /// <returns>true - удачное завершение; false - ошибка</returns>
        public System.Boolean SaveBudgetItemList(DevExpress.XtraTreeList.TreeList objTreeList,
            UniXP.Common.CProfile objProfile)
        {
            System.Boolean bRet = false;

            // уникальный идентификатор бюджета не должен быть пустым
            if (this.m_uuidID == System.Guid.Empty)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show("Недопустимое значение уникального идентификатора объекта.", "Внимание",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                return bRet;
            }

            // дерево статей расходов не должно быть пустым
            if ((objTreeList == null) || (objTreeList.Nodes.Count == 0))
            {
                DevExpress.XtraEditors.XtraMessageBox.Show("Список статей расходов пуст.", "Внимание",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                return bRet;
            }

            System.Data.SqlClient.SqlConnection DBConnection = objProfile.GetDBSource();
            if (DBConnection == null)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show("Отсутствует соединение с БД.", "Внимание",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);
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

                // пробегаем по дереву и сохраняем расшифровки
                foreach (DevExpress.XtraTreeList.Nodes.TreeListNode objNode in objTreeList.Nodes)
                {
                    // рекурсивный метод сохранения списка расшифровок
                    bRet = SaveBudgetItemList(objNode, objProfile, cmd);
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

            }
            catch (System.Exception f)
            {
                // откатываем транзакцию
                DBTransaction.Rollback();
                DevExpress.XtraEditors.XtraMessageBox.Show(
                "Не удалось сохранить расшифровки статей бюджета.\n\nТекст ошибки: " + f.Message, "Внимание",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
			finally // очищаем занимаемые ресурсы
            {
            }

            return bRet;
        }

        public System.Boolean SaveBudgetItemList(DevExpress.XtraTreeList.Nodes.TreeListNode objNode,
            UniXP.Common.CProfile objProfile, System.Data.SqlClient.SqlCommand cmd)
        {
            System.Boolean bRet = false;

            // дерево статей расходов не должно быть пустым
            if ((objNode == null) || (objNode.Tag == null))
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(
                    "Узел дерева не содержит информацию о статье бюджета.", "Внимание",
                   System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
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
                // запрашиваем статью бюджета    
                CBudgetItem objBudgetItem = (CBudgetItem)objNode.Tag;
                bRet = true;

                // проверим, не было ли изменений в статье бюджета
                if (objBudgetItem.ReadOnly == true)
                {
                    // проводились изменения в нумерации статьи бюджета
                    bRet = objBudgetItem.Update(cmd, objProfile, false);
                }

                // сохраняем расшифровки 
                if (bRet == true) { bRet = objBudgetItem.SaveBudgetItem(cmd, objProfile); }

                if (bRet == true)
                {
                    // проверим дочерние узлы
                    foreach (DevExpress.XtraTreeList.Nodes.TreeListNode objChildNode in objNode.Nodes)
                    {
                        bRet = SaveBudgetItemList(objChildNode, objProfile, cmd);
                        if (bRet == false) { break; }
                    }
                }
            }
            catch (System.Exception f)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(
                "Ошибка выполнения рекурсивного метода.\nНе удалось сохранить расшифровки статей бюджета.\n\nТекст ошибки: " + f.Message, "Внимание",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
			finally // очищаем занимаемые ресурсы
            {
            }

            return bRet;
        }

        #endregion

        #region Утвердить бюджет

        /// <summary>
        /// Утверждает бюджет
        /// </summary>
        /// <param name="objProfile">профайл</param>
        /// <param name="bAccept">признак "Утвердить/Отменить утверждение"</param>
        /// <param name="objParentBudgetItemList">список родительских статей бюджета, у которых есть дочерние статьи с суммами"</param>
        /// <returns>true - удачное завершение; false - ошибка</returns>
        public System.Boolean Accept(UniXP.Common.CProfile objProfile, System.Boolean bAccept,
            List<CBudgetItem> objParentBudgetItemList)
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

                System.Boolean bSaveParentBudgetItem = true;
                if (bAccept == true)
                {
                    // прежде чем утвердить бюджет, необходимо добавить расшифровки по родительским статьям, 
                    // которые содержат подстатьи с суммами
                    // в эти расшифровки будет записана СУММА по дочерним расшифровкам
                    foreach (CBudgetItem objParentBudgetItem in objParentBudgetItemList)
                    {
                        if (objParentBudgetItem.SaveBudgetItem(cmd, objProfile) == false)
                        {
                            bSaveParentBudgetItem = false;
                            break;
                        }
                    }
                }

                if (bSaveParentBudgetItem == false)
                {
                    DBTransaction.Rollback();
                }
                else
                {
                    // пересчитываем плановые показатели расшифровок в валюту бюджета
                    // и делаем пометку об утверждении бюджета
                    cmd.Parameters.Clear();
                    cmd.CommandText = System.String.Format("[{0}].[dbo].[sp_AcceptBudget]", objProfile.GetOptionsDllDBName());
                    cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ((System.Byte)(0)), ((System.Byte)(0)), "", System.Data.DataRowVersion.Current, null));
                    cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@BUDGET_ACCEPT", System.Data.SqlDbType.Bit));
                    cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@GUID_ID", System.Data.SqlDbType.UniqueIdentifier));
                    cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ERROR_NUMBER", System.Data.SqlDbType.Int, 8, System.Data.ParameterDirection.Output, false, ((System.Byte)(0)), ((System.Byte)(0)), "", System.Data.DataRowVersion.Current, null));
                    cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ERROR_MESSAGE", System.Data.SqlDbType.NVarChar, 4000));
                    cmd.Parameters["@ERROR_MESSAGE"].Direction = System.Data.ParameterDirection.Output;
                    cmd.Parameters["@GUID_ID"].Value = this.m_uuidID;
                    cmd.Parameters["@BUDGET_ACCEPT"].Value = bAccept;
                    if (bAccept)
                    {
                        cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@BUDGET_ACCEPTDATE", System.Data.SqlDbType.DateTime));
                        cmd.Parameters["@BUDGET_ACCEPTDATE"].Value = System.DateTime.Today;
                    }
                    cmd.ExecuteNonQuery();
                    System.Int32 iRet = (System.Int32)cmd.Parameters["@RETURN_VALUE"].Value;
                    if (iRet == 0)
                    {
                        DBTransaction.Commit();
                        this.m_IsAccept = bAccept;
                        if (bAccept)
                        { this.m_dtAcceptDate = (System.DateTime)cmd.Parameters["@BUDGET_ACCEPTDATE"].Value; }
                        bRet = true;
                    }
                    else
                    {
                        DBTransaction.Rollback();
                        switch (iRet)
                        {
                            case 1:
                                {
                                    DevExpress.XtraEditors.XtraMessageBox.Show("Ошибка установки признака \"Утвержден\" для бюджета.\nНе найдена информация о бюджете с заданным идентификатором.\n\n" + this.m_uuidID.ToString(), "Внимание",
                                        System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);
                                    break;
                                }
                            case 2:
                                {
                                    DevExpress.XtraEditors.XtraMessageBox.Show("Ошибка установки признака \"Утвержден\" для бюджета.\nНедопустимое значение даты.", "Внимание",
                                        System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);
                                    break;
                                }
                            default:
                                {
                                    DevExpress.XtraEditors.XtraMessageBox.Show("Ошибка установки признака \"Утвержден\" для бюджета.\n\nТекст ошибки: " +
                                        (System.String)cmd.Parameters["@ERROR_MESSAGE"].Value, "Внимание",
                                        System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);
                                    break;
                                }
                        }
                    }
                }
                cmd.Dispose();
            }
            catch (System.Exception f)
            {
                DBTransaction.Rollback();
                DevExpress.XtraEditors.XtraMessageBox.Show(
                "Ошибка установки признака \"Утвержден\" для бюджета.\n\nТекст ошибки: " + f.Message, "Внимание",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
			finally // очищаем занимаемые ресурсы
            {
                DBConnection.Close();
            }
            return bRet;
        }
        #endregion

        #region Копирование бюджета
        /// <summary>
        /// Копирует содержимое одного бюджета в другой бюджет
        /// </summary>
        /// <param name="objBudgetSrc">Бюджет источник</param>
        /// <param name="objBudgetDst">Бюджет приемник</param>
        /// <param name="objProfile">профайл</param>
        /// <returns>true - удачное завершение; false - ошибка</returns>
        public static System.Boolean CopyBudgetFromSrc(CBudget objBudgetSrc, CBudget objBudgetDst, UniXP.Common.CProfile objProfile)
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
                cmd.CommandTimeout = 600;
                cmd.Connection = DBConnection;
                cmd.Transaction = DBTransaction;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.CommandText = System.String.Format("[{0}].[dbo].[sp_CopyBudget]", objProfile.GetOptionsDllDBName());
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ((System.Byte)(0)), ((System.Byte)(0)), "", System.Data.DataRowVersion.Current, null));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@BUDGET_SRC_GUID_ID", System.Data.SqlDbType.UniqueIdentifier));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@BUDGET_DST_GUID_ID", System.Data.SqlDbType.UniqueIdentifier));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ERROR_NUM", System.Data.SqlDbType.Int, 8, System.Data.ParameterDirection.Output, false, ((System.Byte)(0)), ((System.Byte)(0)), "", System.Data.DataRowVersion.Current, null));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ERROR_MES", System.Data.SqlDbType.NVarChar, 4000));
                cmd.Parameters["@ERROR_MES"].Direction = System.Data.ParameterDirection.Output;
                cmd.Parameters["@BUDGET_SRC_GUID_ID"].Value = objBudgetSrc.uuidID;
                cmd.Parameters["@BUDGET_DST_GUID_ID"].Value = objBudgetDst.uuidID;

                cmd.ExecuteNonQuery();
                System.Int32 iRet = (System.Int32)cmd.Parameters["@RETURN_VALUE"].Value;
                if (iRet == 0)
                {
                    bRet = true;
                }
                else
                {
                    DevExpress.XtraEditors.XtraMessageBox.Show("Не удалось скопировать бюджет.\n\nТекст сообщения: " +
                        (System.String)cmd.Parameters["@ERROR_MES"].Value, "Внимание",
                        System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);
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
                "Не удалось скопировать бюджет.\n\nТекст ошибки: " + f.Message, "Внимание",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
            finally
            {
                DBConnection.Close();
            }

            return bRet;
        }

        #endregion

        #region Распорядители и согласователи бюджета
        /// <summary>
        /// Возвращает список распорядителей и согласователей бюджета
        /// </summary>
        /// <param name="objProfile">профайл</param>
        /// <param name="uuidBudgetID">уи бюджета</param>
        /// <param name="strErr">текст ошибки</param>
        /// <returns>список объектов класса "CUser"</returns>
        public static List<CUser> GetBudgetAdvManagerList(UniXP.Common.CProfile objProfile, System.Guid uuidBudgetID,
            ref System.String strErr)
        {
            List<CUser> objManagerList = new List<CUser>();

            System.Data.SqlClient.SqlConnection DBConnection = objProfile.GetDBSource();
            if (DBConnection == null)
            {
                strErr += ("\nОтсутствует соединение с базой данных.");

                return objManagerList;
            }

            try
            {
                // соединение с БД получено, прописываем команду на выборку данных
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand()
                {
                    Connection = DBConnection,
                    CommandType = System.Data.CommandType.StoredProcedure,
                    CommandText = System.String.Format("[{0}].[dbo].[usp_GetBudgetAdvancedManagerList]", objProfile.GetOptionsDllDBName())
                };
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ((System.Byte)(0)), ((System.Byte)(0)), "", System.Data.DataRowVersion.Current, null));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Budget_Guid", System.Data.SqlDbType.UniqueIdentifier, 4));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ERROR_NUMBER", System.Data.SqlDbType.Int, 8, System.Data.ParameterDirection.Output, false, ((System.Byte)(0)), ((System.Byte)(0)), "", System.Data.DataRowVersion.Current, null));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ERROR_MESSAGE", System.Data.SqlDbType.NVarChar, 4000) { Direction = System.Data.ParameterDirection.Output });
                cmd.Parameters["@Budget_Guid"].Value = uuidBudgetID;
                System.Data.SqlClient.SqlDataReader rs = cmd.ExecuteReader();
                if (rs.HasRows)
                {
                    // набор данных непустой
                    CUser objManager = null;
                    while (rs.Read())
                    {
                        objManager = new CUser(System.Convert.ToInt32(rs["ErpBudgetUserID"]), System.Convert.ToInt32(rs["UniXPUserID"]),
                            System.Convert.ToString(rs["strLastName"]), System.Convert.ToString(rs["strFirstName"]));

                        objManager.IsBudgetDepManager = System.Convert.ToBoolean(rs["IsManager"]);
                        objManager.IsBudgetDepCoordinator = System.Convert.ToBoolean(rs["IsCoordinator"]);

                        objManagerList.Add(objManager);
                    }
                }
                rs.Close();
                rs.Dispose();

                // инициализация динамических прав пользователей
                if ((objManagerList != null) && (objManagerList.Count > 0))
                {
                    CDynamicRight objDynamicRight = new CDynamicRight();
                    foreach (CUser objUser in objManagerList)
                    {
                        objUser.DynamicRightsList = objDynamicRight.GetDynamicRightsList(objProfile, objUser.ulID);
                    }
                    objDynamicRight = null;
                }

                if (objManagerList.Count == 0)
                {
                    strErr += ("\nУкажите, пожалуйста, список распорядителей и согласователей.\nдля бюджетного подразделения, а затем укажите ограничения для бюджета.");
                }

                cmd.Dispose();
            }
            catch (System.Exception f)
            {
                strErr += ("\nНе удалось получить список распорядителей и согласователей бюджета.\n\nТекст ошибки: " + f.Message);
            }
            finally // очищаем занимаемые ресурсы
            {
                DBConnection.Close();
            }

            return objManagerList;
        }
        /// <summary>
        /// Сохраняет в базе данных список согласователей и распорядителей для бюджета
        /// </summary>
        /// <param name="objProfile">профайл</param>
        /// <param name="uuidBudgetID">УИ бюджета</param>
        /// <param name="objManagerList">список пользователей</param>
        /// <param name="strErr">текст ошибки</param>
        /// <returns>true - удачное завершение операции; false - ошибка</returns>
        public static System.Boolean SaveBudgetManagerList(UniXP.Common.CProfile objProfile, System.Guid uuidBudgetID,
            List<CUser> objManagerList, ref System.String strErr)
        {
            System.Boolean bRet = false;
            // уникальный идентификатор не должен быть пустым
            if (uuidBudgetID == System.Guid.Empty)
            {
                strErr += (String.Format("\nНедопустимое значение уникального идентификатора бюджета.\nУИ: {0}", uuidBudgetID));
                return bRet;
            }

            System.Data.SqlClient.SqlConnection DBConnection = objProfile.GetDBSource();
            if (DBConnection == null)
            {
                strErr += ("\nОтсутствует соединение с базой данных.");
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
                cmd.CommandText = System.String.Format("[{0}].[dbo].[usp_DeleteBudgetAdvancedManagerList]", objProfile.GetOptionsDllDBName());
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ((System.Byte)(0)), ((System.Byte)(0)), "", System.Data.DataRowVersion.Current, null));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Budget_Guid", System.Data.SqlDbType.UniqueIdentifier));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ERROR_NUMBER", System.Data.SqlDbType.Int, 8, System.Data.ParameterDirection.Output, false, ((System.Byte)(0)), ((System.Byte)(0)), "", System.Data.DataRowVersion.Current, null));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ERROR_MESSAGE", System.Data.SqlDbType.NVarChar, 4000) { Direction = System.Data.ParameterDirection.Output });
                cmd.Parameters["@Budget_Guid"].Value = uuidBudgetID;
                cmd.ExecuteNonQuery();
                System.Int32 iRet = (System.Int32)cmd.Parameters["@RETURN_VALUE"].Value;
                if (iRet == 0)
                {
                    if ((objManagerList != null) && (objManagerList.Count > 0))
                    {
                        cmd.CommandText = System.String.Format("[{0}].[dbo].[sp_AddBudgetAdvancedManager]", objProfile.GetOptionsDllDBName());
                        cmd.Parameters.Clear();
                        cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ((System.Byte)(0)), ((System.Byte)(0)), "", System.Data.DataRowVersion.Current, null));
                        cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Budget_Guid", System.Data.SqlDbType.UniqueIdentifier));
                        cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@USER_ID", System.Data.SqlDbType.Int));
                        cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Rights_ID", System.Data.SqlDbType.Int));
                        cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ERROR_NUMBER", System.Data.SqlDbType.Int, 8, System.Data.ParameterDirection.Output, false, ((System.Byte)(0)), ((System.Byte)(0)), "", System.Data.DataRowVersion.Current, null));
                        cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ERROR_MESSAGE", System.Data.SqlDbType.NVarChar, 4000) { Direction = System.Data.ParameterDirection.Output });

                        cmd.Parameters["@Budget_Guid"].Value = uuidBudgetID;

                        foreach (CUser objUser in objManagerList)
                        {
                            cmd.Parameters["@USER_ID"].Value = objUser.ulID;

                            // проверка, назначен ли пользователь распорядителем
                            if (objUser.IsBudgetDepManager == true)
                            {
                                cmd.Parameters["@Rights_ID"].Value = objUser.DynamicRightsList.FindByName(ERP_Budget.Global.Consts.strDRManager).ID;
                                cmd.ExecuteNonQuery();
                                iRet = (System.Int32)cmd.Parameters["@RETURN_VALUE"].Value;
                            }

                            if (iRet == 0)
                            {
                                // проверка, назначен ли пользователь согласователем
                                if (objUser.IsBudgetDepCoordinator == true)
                                {
                                    cmd.Parameters["@Rights_ID"].Value = objUser.DynamicRightsList.FindByName(ERP_Budget.Global.Consts.strDRCoordinator).ID;
                                    cmd.ExecuteNonQuery();
                                    iRet = (System.Int32)cmd.Parameters["@RETURN_VALUE"].Value;

                                    if (iRet != 0)
                                    {
                                        // откатываем транзакцию
                                        DBTransaction.Rollback();
                                        strErr += (System.Convert.ToString(cmd.Parameters["@ERROR_MESSAGE"].Value));
                                        break;
                                    }
                                }
                            }
                            else
                            {
                                // откатываем транзакцию
                                DBTransaction.Rollback();
                                strErr += (System.Convert.ToString(cmd.Parameters["@ERROR_MESSAGE"].Value));
                                break;
                            }

                        }
                    }
                    if (iRet == 0)
                    {
                        // подтверждаем транзакцию
                        DBTransaction.Commit();
                        bRet = true;
                    }
                }
                else
                {
                    // откатываем транзакцию
                    DBTransaction.Rollback();
                    strErr += (String.Format("Ошибка сохранения списка распорядителей и согласователей для бюджета.\n\n{0}", System.Convert.ToString(cmd.Parameters["@ERROR_MESSAGE"].Value)));
                }
                cmd.Dispose();
            }
            catch (System.Exception e)
            {
                // откатываем транзакцию
                DBTransaction.Rollback();
                strErr += (String.Format("Ошибка сохранения списка распорядителей и согласователей для бюджета.\n\n{0}", e.Message));
            }
			finally // очищаем занимаемые ресурсы
            {
                DBConnection.Close();
            }

            return bRet;
        }
        #endregion

        #region Список бюджетов для профайла
        /// <summary>
        /// Возвращает список бюджетов для отображения в ветке профайла
        /// </summary>
        /// <param name="objProfile">профайл</param>
        /// <param name="UniXPUserID">УИ пользователя в БД "UniXP"</param>
        /// <param name="strErr">текст ошибки</param>
        /// <returns>список объектов класса "CBudget"</returns>
        public static List<CBudget> GetBudgetListForProfile(UniXP.Common.CProfile objProfile,
            System.Int32 UniXPUserID,  ref System.String strErr)
        {
            List<CBudget> objBudgetList = new List<CBudget>();

            System.Data.SqlClient.SqlConnection DBConnection = objProfile.GetDBSource();
            if (DBConnection == null)
            {
                strErr += ("\nОтсутствует соединение с базой данных.");

                return objBudgetList;
            }

            try
            {
                // соединение с БД получено, прописываем команду на выборку данных
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand()
                {
                    Connection = DBConnection,
                    CommandType = System.Data.CommandType.StoredProcedure,
                    CommandText = System.String.Format("[{0}].[dbo].[usp_GetBudgetListForProfile]", objProfile.GetOptionsDllDBName())
                };
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ((System.Byte)(0)), ((System.Byte)(0)), "", System.Data.DataRowVersion.Current, null));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@UniXPUserID", System.Data.SqlDbType.UniqueIdentifier, 4));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ERROR_NUMBER", System.Data.SqlDbType.Int, 8, System.Data.ParameterDirection.Output, false, ((System.Byte)(0)), ((System.Byte)(0)), "", System.Data.DataRowVersion.Current, null));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ERROR_MESSAGE", System.Data.SqlDbType.NVarChar, 4000) { Direction = System.Data.ParameterDirection.Output });
                cmd.Parameters["@UniXPUserID"].Value = UniXPUserID;
                System.Data.SqlClient.SqlDataReader rs = cmd.ExecuteReader();
                if (rs.HasRows)
                {
                    // набор данных непустой
                    CBudget objBudget = null;
                    while (rs.Read())
                    {
                        objBudget = new CBudget()
                        {
                            uuidID = (System.Guid)rs["Budget_Guid"],
                            Name = System.Convert.ToString(rs["BUDGET_NAME"]),
                            Date = System.Convert.ToDateTime(rs["BUDGET_DATE"]),
                            BudgetDep = new CBudgetDep()
                            {
                                uuidID = (System.Guid)rs["BUDGETDEP_GUID_ID"],
                                ParentID = ((rs["BUDGETDEP_GUID_ID"] != System.DBNull.Value) ? (System.Guid)rs["BUDGETDEP_GUID_ID"] : System.Guid.Empty ),
                                Name = System.Convert.ToString(rs["BUDGETDEP_NAME"])
                            }
                        };

                        objBudgetList.Add(objBudget);
                    }
                }
                rs.Close();
                rs.Dispose();
                cmd.Dispose();
            }
            catch (System.Exception f)
            {
                strErr += ("\nНе удалось получить список бюджетов для профайла.\n\nТекст ошибки: " + f.Message);
            }
            finally // очищаем занимаемые ресурсы
            {
                DBConnection.Close();
            }

            return objBudgetList;
        }
        #endregion

        public override string ToString()
        {
            return Name;
        }

    }
}
