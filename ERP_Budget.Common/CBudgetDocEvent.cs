using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

namespace ERP_Budget.Common
{
    /// <summary>
    /// Класс "Событие, связанное с бюджетным документом"
    /// </summary>
    public class CBudgetDocEvent : IBaseListItem
    {
        #region Переменные, Свойства, Константы
        /// <summary>
        /// Номер по порядку
        /// </summary>
        private System.Int32 m_iOrderNum;
        /// <summary>
        /// Номер по порядку
        /// </summary>
        [DisplayName("Номер по порядку")]
        [Description("Номер в логической последовательности действий над документом")]
        [Category("Обязательные значения")]
        public System.Int32 OrderNum
        {
            get { return m_iOrderNum; }
            set { m_iOrderNum = value; }
        }
        /// <summary>
        /// Список пользователей, имеющих доступ к событию
        /// </summary>
        private System.Collections.Generic.List<CUser> m_UserList;
        /// <summary>
        /// Список пользователей, имеющих доступ к событию
        /// </summary>
        [DisplayName("Список пользователей")]
        [Description("Список пользователей, имеющих доступ к событию")]
        [Category("Дополнительные значения")]
        [ReadOnly(true)]
        public System.Collections.Generic.List<CUser> UserList
        {
            get { return m_UserList; }
            set { }
        }
        /// <summary>
        /// Признак "Отображать сумму документа"
        /// </summary>
        private System.Boolean m_bShowMoney;
        [DisplayName("Признак \"Отображать сумму документа\"")]
        [Description("Признак \"Отображать сумму документа\" при соверешнии операции над документом")]
        [Category("Дополнительные значения")]
        //[ReadOnly(true)]
        public System.Boolean IsShowMoney
        {
            get { return m_bShowMoney; }
            set { m_bShowMoney = value;  }
        }
        /// <summary>
        /// Признак "Изменять сумму документа"
        /// </summary>
        private System.Boolean m_bCanChanhgeMoney;
        [DisplayName("Признак \"Изменять сумму документа\"")]
        [Description("Признак \"Возможность изменять сумму документа\" при соверешнии операции над документом")]
        [Category("Дополнительные значения")]
        //[ReadOnly(true)]
        public System.Boolean IsCanChanhgeMoney
        {
            get { return m_bCanChanhgeMoney; }
            set { m_bCanChanhgeMoney = value; }
        }
        /// <summary>
        /// Сумма, над которой будет произведено действие
        /// </summary>
        private double m_EventMoney;
        /// <summary>
        /// Сумма, над которой будет произведено действие
        /// </summary>
        public double EventMoney
        {
            get { return m_EventMoney; }
            set { m_EventMoney = value; }
        }
        #endregion

        #region Конструкторы
        public CBudgetDocEvent()
        {
            this.m_uuidID = System.Guid.Empty;
            this.m_strName = "";
            this.m_UserList = new List<CUser>();
            this.m_iOrderNum = -1;
            this.m_bShowMoney = false;
            this.m_bCanChanhgeMoney = false;
            this.m_EventMoney = 0;
        }

        public CBudgetDocEvent(System.Guid uuidBudgetDocEventID)
        {
            this.m_uuidID = uuidBudgetDocEventID;
            this.m_strName = "";
            this.m_UserList = new List<CUser>();
            this.m_iOrderNum = -1;
            this.m_bShowMoney = false;
            this.m_bCanChanhgeMoney = false;
            this.m_EventMoney = 0;
        }

        public CBudgetDocEvent(System.Guid uuidBudgetDocEventID, System.String strName)
        {
            this.m_uuidID = uuidBudgetDocEventID;
            this.m_strName = strName;
            this.m_UserList = new List<CUser>();
            this.m_iOrderNum = -1;
            this.m_bShowMoney = false;
            this.m_bCanChanhgeMoney = false;
            this.m_EventMoney = 0;
        }
        public CBudgetDocEvent(System.Guid uuidBudgetDocEventID, System.String strName, System.Int32 iOrderNum)
        {
            this.m_uuidID = uuidBudgetDocEventID;
            this.m_strName = strName;
            this.m_UserList = new List<CUser>();
            this.m_iOrderNum = iOrderNum;
            this.m_bShowMoney = false;
            this.m_bCanChanhgeMoney = false;
            this.m_EventMoney = 0;
        }
        public CBudgetDocEvent(System.Guid uuidBudgetDocEventID, System.String strName, System.Int32 iOrderNum,
            System.Boolean bShowMoney, System.Boolean bCanChanhgeMoney)
        {
            this.m_uuidID = uuidBudgetDocEventID;
            this.m_strName = strName;
            this.m_UserList = new List<CUser>();
            this.m_iOrderNum = iOrderNum;
            this.m_bShowMoney = bShowMoney;
            this.m_bCanChanhgeMoney = bCanChanhgeMoney;
            this.m_EventMoney = 0;
        }
        #endregion

        #region Список событий
        /// <summary>
        /// Возвращает список событий бюджетного документа
        /// </summary>
        /// <param name="objProfile">профайл</param>
        /// <returns>список событий бюджетного документа в виде таблицы</returns>
        public static System.Data.DataTable GetBudgetDocEventTable(UniXP.Common.CProfile objProfile)
        {
            System.Data.DataTable dtBudgetDocEvent = null;
            try
            {
                // сформируем таблицу
                dtBudgetDocEvent = new System.Data.DataTable("dtBudgetDocEvent");
                dtBudgetDocEvent.Columns.Add(new System.Data.DataColumn("GUID_ID", System.Type.GetType("System.Guid")));
                dtBudgetDocEvent.Columns.Add(new System.Data.DataColumn("BUDGETDOCEVENT_NAME", System.Type.GetType("System.String")));

                System.Data.SqlClient.SqlConnection DBConnection = objProfile.GetDBSource();
                if (DBConnection == null) { return dtBudgetDocEvent; }

                // соединение с БД получено, прописываем команду на выборку данных
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
                cmd.Connection = DBConnection;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = System.String.Format("[{0}].[dbo].[sp_GetBudgetDocEvent]", objProfile.GetOptionsDllDBName());
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ((System.Byte)(0)), ((System.Byte)(0)), "", System.Data.DataRowVersion.Current, null));
                System.Data.SqlClient.SqlDataReader rs = cmd.ExecuteReader();
                if (rs.HasRows)
                {
                    System.Data.DataRow row = null;
                    while (rs.Read())
                    {
                        row = dtBudgetDocEvent.NewRow();
                        row["GUID_ID"] = rs.GetGuid(0);
                        row["BUDGETDOCEVENT_NAME"] = rs.GetString(1);
                        dtBudgetDocEvent.Rows.Add(row);
                    }
                    dtBudgetDocEvent.AcceptChanges();
                }
                cmd.Dispose();
                rs.Dispose();
                DBConnection.Close();
            }
            catch (System.Exception e)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(
                "Не удалось получить список событий бюджетного документа.\n" + e.Message, "Внимание",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
            return dtBudgetDocEvent;
        }
        /// <summary>
        /// Возвращает список событий бюджетного документа
        /// </summary>
        /// <param name="objProfile">профайл</param>
        /// <returns>список событий бюджетного документа</returns>
        public static List<CBudgetDocEvent> GetBudgetDocEventList(UniXP.Common.CProfile objProfile)
        {
            List<CBudgetDocEvent> objList = new List<CBudgetDocEvent>();
            try
            {
                System.Data.SqlClient.SqlConnection DBConnection = objProfile.GetDBSource();
                if (DBConnection == null) { return objList; }

                // соединение с БД получено, прописываем команду на выборку данных
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
                cmd.Connection = DBConnection;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = System.String.Format("[{0}].[dbo].[sp_GetBudgetDocEvent]", objProfile.GetOptionsDllDBName());
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ((System.Byte)(0)), ((System.Byte)(0)), "", System.Data.DataRowVersion.Current, null));
                System.Data.SqlClient.SqlDataReader rs = cmd.ExecuteReader();
                if (rs.HasRows)
                {
                    CBudgetDocEvent objBudgetDocEvent = null;
                    while (rs.Read())
                    {
                        objBudgetDocEvent = new CBudgetDocEvent((System.Guid)rs["GUID_ID"],
                            (System.String)rs["BUDGETDOCEVENT_NAME"], (System.Int32)rs["BUDGETDOCEVENT_ID"]);
                        objBudgetDocEvent.m_bCanChanhgeMoney = System.Convert.ToBoolean(rs["BUDGETDOCEVENT_CANCHANGEMONEY"]);
                        objBudgetDocEvent.m_bShowMoney = System.Convert.ToBoolean(rs["BUDGETDOCEVENT_SHOWMONEY"]);

                        objList.Add( objBudgetDocEvent );
                    }
                }
                rs.Close();
                rs.Dispose();
                cmd.Dispose();
                DBConnection.Close();
            }
            catch (System.Exception e)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(
                "Не удалось получить список событий бюджетного документа.\n" + e.Message, "Внимание",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
            return objList;
        }
        /// <summary>
        /// Возвращает действие с номером 0
        /// </summary>
        /// <param name="objProfile">профайл</param>
        /// <returns>объект класса CBudgetDocEvent</returns>
        public static CBudgetDocEvent GetPimaryBudgetDocEvent(UniXP.Common.CProfile objProfile)
        {
            CBudgetDocEvent objRet = null;
            try
            {
                System.Data.SqlClient.SqlConnection DBConnection = objProfile.GetDBSource();
                if (DBConnection == null) { return objRet; }

                // соединение с БД получено, прописываем команду на выборку данных
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
                cmd.Connection = DBConnection;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = System.String.Format("[{0}].[dbo].[sp_GetBudgetDocEvent]", objProfile.GetOptionsDllDBName());
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ((System.Byte)(0)), ((System.Byte)(0)), "", System.Data.DataRowVersion.Current, null));
                System.Data.SqlClient.SqlDataReader rs = cmd.ExecuteReader();
                if (rs.HasRows)
                {
                    while (rs.Read())
                    {
                        if (((System.Int32)rs[2]) == 0)
                        {
                            // действие с идентификатором 0
                            objRet = new CBudgetDocEvent(rs.GetGuid(0), rs.GetString(1), 0);
                            break;
                        }
                    }
                }
                rs.Close();
                rs.Dispose();
                cmd.Dispose();
                DBConnection.Close();
            }
            catch (System.Exception e)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(
                "Не удалось получить событие бюджетного документа.\n" + e.Message, "Внимание",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
            return objRet;
        }

        /// <summary>
        /// Возвращает действие с номером 6 (удалить)
        /// </summary>
        /// <param name="objProfile">профайл</param>
        /// <returns>объект класса CBudgetDocEvent</returns>
        public static CBudgetDocEvent GetDropBudgetDocEvent(UniXP.Common.CProfile objProfile)
        {
            CBudgetDocEvent objRet = null;
            try
            {
                System.Data.SqlClient.SqlConnection DBConnection = objProfile.GetDBSource();
                if (DBConnection == null) { return objRet; }

                // соединение с БД получено, прописываем команду на выборку данных
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
                cmd.Connection = DBConnection;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = System.String.Format("[{0}].[dbo].[sp_GetBudgetDocEvent]", objProfile.GetOptionsDllDBName());
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ((System.Byte)(0)), ((System.Byte)(0)), "", System.Data.DataRowVersion.Current, null));
                System.Data.SqlClient.SqlDataReader rs = cmd.ExecuteReader();
                if (rs.HasRows)
                {
                    while (rs.Read())
                    {
                        if (((System.Int32)rs[2]) == 6)
                        {
                            // действие с идентификатором 6
                            objRet = new CBudgetDocEvent(rs.GetGuid(0), rs.GetString(1), 6);
                            break;
                        }
                    }
                }
                rs.Close();
                rs.Dispose();
                cmd.Dispose();
                DBConnection.Close();
            }
            catch (System.Exception e)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(
                "Не удалось получить событие бюджетного документа.\n" + e.Message, "Внимание",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
            return objRet;
        }
        /// <summary>
        /// Обновляет дерево со списком действий над бюджетными документами
        /// </summary>
        /// <param name="objProfile">профайл</param>
        /// <param name="objTreeList">дерево</param>
        /// <returns>true - удачное завершение; false - ошибка</returns>
        public static System.Boolean bRefreshBudgetDocEventTree(UniXP.Common.CProfile objProfile,
            DevExpress.XtraTreeList.TreeList objTreeList)
        {
            System.Boolean bRet = false;
            // очищаем дерево
            objTreeList.Nodes.Clear();
            // запрашиваем соединение с БД
            System.Data.SqlClient.SqlConnection DBConnection = objProfile.GetDBSource();
            if (DBConnection == null)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(
                    "Не удалось обновить список действий над бюджетными документами.\nОтсутствует соединение с БД.", "Внимание",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                return bRet;
            }

            try
            {
                // соединение с БД получено, прописываем команду на выборку данных
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
                cmd.Connection = DBConnection;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = System.String.Format("[{0}].[dbo].[sp_GetBudgetDocEvent]", objProfile.GetOptionsDllDBName());
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ((System.Byte)(0)), ((System.Byte)(0)), "", System.Data.DataRowVersion.Current, null));
                System.Data.SqlClient.SqlDataReader rs = cmd.ExecuteReader();
                if (rs.HasRows)
                {
                    // набор данных непустой
                    CBudgetDocEvent objBudgetDocEvent = null;
                    while (rs.Read())
                    {
                        // создаем объект класса CBudgetDocEvent
                        objBudgetDocEvent = new CBudgetDocEvent((System.Guid)rs["GUID_ID"],
                            (System.String)rs["BUDGETDOCEVENT_NAME"], (System.Int32)rs["BUDGETDOCEVENT_ID"]);
                        objBudgetDocEvent.m_bCanChanhgeMoney = System.Convert.ToBoolean(rs["BUDGETDOCEVENT_CANCHANGEMONEY"]);
                        objBudgetDocEvent.m_bShowMoney = System.Convert.ToBoolean(rs["BUDGETDOCEVENT_SHOWMONEY"]);
                        // создаем узел дерева
                        DevExpress.XtraTreeList.Nodes.TreeListNode objNode =
                                objTreeList.AppendNode(new object[] { objBudgetDocEvent.Name }, null);
                        objNode.Tag = objBudgetDocEvent;

                    }
                }
                rs.Close();
                rs.Dispose();
                cmd.Dispose();

                bRet = true;
            }
            catch (System.Exception f)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(
                "Не удалось обновить список действий над бюджетными документами.\n\nТекст ошибки: " + f.Message, "Внимание",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
			finally // очищаем занимаемые ресурсы
            {
                DBConnection.Close();
            }

            return bRet;
        }
        #endregion

        #region Init
        /// <summary>
        /// Загружает список пользователей, имеющих доступ к действию
        /// </summary>
        /// <param name="objProfile">профайл</param>
        /// <param name="cmd">SQL-команда</param>
        public void LoadUserList(UniXP.Common.CProfile objProfile, System.Data.SqlClient.SqlCommand cmd)
        {
            if (cmd == null) { return; }

            try
            {
                this.m_UserList.Clear();
                cmd.Parameters.Clear();
                cmd.CommandText = System.String.Format("[{0}].[dbo].[sp_GetBudgetDocEventUserList]", objProfile.GetOptionsDllDBName());
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ((System.Byte)(0)), ((System.Byte)(0)), "", System.Data.DataRowVersion.Current, null));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@BUDGETDOCEVENT_GUID_ID", System.Data.SqlDbType.UniqueIdentifier));
                cmd.Parameters["@BUDGETDOCEVENT_GUID_ID"].Value = uuidID;
                System.Data.SqlClient.SqlDataReader rs = cmd.ExecuteReader();
                if (rs.HasRows)
                {
                    while (rs.Read())
                    {
                        this.m_UserList.Add(new CUser(rs.GetInt32(0), rs.GetInt32(1), rs.GetString(2), rs.GetString(3)));
                    }
                }
                rs.Close();
                rs.Dispose();
            }
            catch (System.Exception e)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(
                "Не удалось получить список пользователей, \nимеющих доступ к действию с уникальным идентификатором : " + this.m_uuidID.ToString() + "\n\nТекст ошибки: " + e.Message, "Внимание",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
			finally // очищаем занимаемые ресурсы
            {
            }
            return;
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
                cmd.CommandText = System.String.Format("[{0}].[dbo].[sp_GetBudgetDocEvent]", objProfile.GetOptionsDllDBName());
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
                    bRet = true;
                }
                else
                {
                    DevExpress.XtraEditors.XtraMessageBox.Show(
                    "Не удалось проинициализировать класс CBudgetDocEvent.\nВ БД не найдена информация.", "Внимание",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);

                }
                cmd.Dispose();
                rs.Dispose();
            }
            catch (System.Exception e)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(
                "Не удалось проинициализировать класс CBudgetDocEvent.\n" + e.Message, "Внимание",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
			finally // очищаем занимаемые ресурсы
            {
                DBConnection.Close();
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
                cmd.CommandText = System.String.Format("[{0}].[dbo].[sp_DeleteBudgetDocEvent]", objProfile.GetOptionsDllDBName());
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
                                DevExpress.XtraEditors.XtraMessageBox.Show("На запись с заданным идентификатором есть ссылка \nв таблице шаблонов маршрутов.\n" + this.uuidID.ToString(), "Внимание",
                                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);
                                break;
                            }
                        case 2:
                            {
                                DevExpress.XtraEditors.XtraMessageBox.Show("На запись с заданным идентификатором есть ссылка \nв таблице маршрутов.\n" + this.uuidID.ToString(), "Внимание",
                                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);
                                break;
                            }
                        default:
                            {
                                DevExpress.XtraEditors.XtraMessageBox.Show("Ошибка выполнения запроса \nна удаление события бюджетного документа.\nСобытие : " + this.m_strName, "Внимание",
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
                "Ошибка выполнения запроса \nна удаление события бюджетного документа.\nСобытие : " +
                this.m_strName + "\n" + e.Message, "Внимание",
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
        /// Проверяет свойства объекта на предмет обязательного заполнения
        /// </summary>
        /// <returns>true - ошибок нет; false - ошибка</returns>
        public System.Boolean IsValidateProperties()
        {
            System.Boolean bRet = false;
            try
            {
                // наименование не должен быть пустым
                if (this.m_strName == "")
                {
                    DevExpress.XtraEditors.XtraMessageBox.Show("Недопустимое значение наименования объекта", "Внимание",
                        System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    return bRet;
                }

                bRet = true;
            }
            catch (System.Exception f)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(
                "Ошибка проверки свойств объекта.\n\nТекст ошибки: " + f.Message, "Внимание",
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
            System.Boolean bRet = false;
            if (IsValidateProperties() == false) { return bRet; }

            System.Data.SqlClient.SqlConnection DBConnection = objProfile.GetDBSource();
            if (DBConnection == null)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(
                "Не удалось установить соединение с базой данных.", "Внимание",
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
                cmd.CommandText = System.String.Format("[{0}].[dbo].[sp_AddBudgetDocEvent]", objProfile.GetOptionsDllDBName());
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ((System.Byte)(0)), ((System.Byte)(0)), "", System.Data.DataRowVersion.Current, null));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@GUID_ID", System.Data.SqlDbType.UniqueIdentifier, 4, System.Data.ParameterDirection.Output, false, ((System.Byte)(0)), ((System.Byte)(0)), "", System.Data.DataRowVersion.Current, null));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@BUDGETDOCEVENT_NAME", System.Data.DbType.String));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@BUDGETDOCEVENT_SHOWMONEY", System.Data.SqlDbType.Bit));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@BUDGETDOCEVENT_CANCHANGEMONEY", System.Data.SqlDbType.Bit));
                cmd.Parameters["@BUDGETDOCEVENT_NAME"].Value = this.m_strName;
                cmd.Parameters["@BUDGETDOCEVENT_SHOWMONEY"].Value = this.m_bShowMoney;
                cmd.Parameters["@BUDGETDOCEVENT_CANCHANGEMONEY"].Value = this.m_bCanChanhgeMoney;
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
                                DevExpress.XtraEditors.XtraMessageBox.Show("Событие с заданным именем существует : '" + this.m_strName + "'", "Внимание",
                                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);
                                break;
                            }
                        default:
                            {
                                DevExpress.XtraEditors.XtraMessageBox.Show("Ошибка создания события : " + this.m_strName, "Ошибка",
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
                "Не удалось создать событие\n" + this.m_strName + "\n" + e.Message, "Внимание",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
			finally // очищаем занимаемые ресурсы
            {
                DBConnection.Close();
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
            if (IsValidateProperties() == false) { return bRet; }

            System.Data.SqlClient.SqlConnection DBConnection = objProfile.GetDBSource();
            if (DBConnection == null)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(
                "Не удалось установить соединение с базой данных.", "Внимание",
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
                cmd.CommandText = System.String.Format("[{0}].[dbo].[sp_EditBudgetDocEvent]", objProfile.GetOptionsDllDBName());
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ((System.Byte)(0)), ((System.Byte)(0)), "", System.Data.DataRowVersion.Current, null));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@GUID_ID", System.Data.SqlDbType.UniqueIdentifier));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@BUDGETDOCEVENT_NAME", System.Data.DbType.String));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@BUDGETDOCEVENT_SHOWMONEY", System.Data.SqlDbType.Bit));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@BUDGETDOCEVENT_CANCHANGEMONEY", System.Data.SqlDbType.Bit));
                cmd.Parameters["@GUID_ID"].Value = this.m_uuidID;
                cmd.Parameters["@BUDGETDOCEVENT_NAME"].Value = this.m_strName;
                cmd.Parameters["@BUDGETDOCEVENT_SHOWMONEY"].Value = this.m_bShowMoney;
                cmd.Parameters["@BUDGETDOCEVENT_CANCHANGEMONEY"].Value = this.m_bCanChanhgeMoney;
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
                            DevExpress.XtraEditors.XtraMessageBox.Show("Событие '" + this.m_strName + "' уже есть в БД", "Внимание",
                                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);
                            break;
                        case 2:
                            DevExpress.XtraEditors.XtraMessageBox.Show("Запись с указанным идентификатором не найдена \n" + this.m_uuidID.ToString(), "Внимание",
                                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                            break;
                        default:
                            DevExpress.XtraEditors.XtraMessageBox.Show("Ошибка изменения свойств события : " + this.m_strName, "Внимание",
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
                "Ошибка изменения свойств события : " + this.m_strName + "\n" + e.Message, "Внимание",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
			finally // очищаем занимаемые ресурсы
            {
                DBConnection.Close();
            }
            return bRet;
        }
        #endregion

        #region Поиск
        /// <summary>
        /// Поиск пользователя в списке пользователей по имени
        /// </summary>
        /// <param name="strUserName">имя пользователя</param>
        /// <returns>объект класса "Пользователь"</returns>
        public CUser GetUserByName(System.String strUserName)
        {
            CUser objRet = null;
            if (this.m_UserList.Count == 0) { return objRet; }
            try
            {
                foreach (CUser objUser in this.m_UserList)
                {
                    if (objUser.UserFullName == strUserName)
                    {
                        objRet = objUser;
                        break;
                    }
                }
            }
            catch (System.Exception f)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(
                "Ошибка поиска пользователя.\n\nТекст ошибки: " + f.Message, "Внимание",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }

            return objRet;
        }
        #endregion


        #region Фильтрация списка пользователей
        /// <summary>
        /// Возвращает список пользователей, имеющих доступ к указанному действию
        /// </summary>
        /// <param name="objProfile">профайл</param>
        /// <param name="BudgetDocEvent_Guid">УИ действия</param>
        /// <param name="BudgetDep_Guid">УИ бюджетного подразделения</param>
        /// <param name="Budget_Guid">УИ бюджета</param>
        /// <param name="strErr">текст ошибки</param>
        /// <returns>список объектов класса "CUser"</returns>
        public static List<CUser> GetBudgetDocEventUserList(UniXP.Common.CProfile objProfile,
            System.Guid BudgetDocEvent_Guid, System.Guid BudgetDep_Guid, System.Guid Budget_Guid, 
            ref System.String strErr )
        {
            List<CUser> objList = new List<CUser>();

            System.Data.SqlClient.SqlConnection DBConnection = objProfile.GetDBSource();
            if (DBConnection == null) { return objList; }

            try
            {
                // соединение с БД получено, прописываем команду на выборку данных
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand() 
                { 
                    Connection = DBConnection, 
                    CommandType = System.Data.CommandType.StoredProcedure, 
                    CommandText = System.String.Format("[{0}].[dbo].[usp_GetBudgetDocEventUserList]", objProfile.GetOptionsDllDBName()) 
                };
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ((System.Byte)(0)), ((System.Byte)(0)), "", System.Data.DataRowVersion.Current, null));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ERROR_NUMBER", System.Data.SqlDbType.Int, 8, System.Data.ParameterDirection.Output, false, ((System.Byte)(0)), ((System.Byte)(0)), "", System.Data.DataRowVersion.Current, null));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ERROR_MESSAGE", System.Data.SqlDbType.NVarChar, 4000) { Direction = System.Data.ParameterDirection.Output });

                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@BudgetDocEvent_Guid", System.Data.SqlDbType.UniqueIdentifier));
                cmd.Parameters["@BudgetDocEvent_Guid"].Value = BudgetDocEvent_Guid;

                if (BudgetDep_Guid.CompareTo(System.Guid.Empty) != 0)
                {
                    cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@BudgetDep_Guid", System.Data.SqlDbType.UniqueIdentifier));
                    cmd.Parameters["@BudgetDep_Guid"].Value = BudgetDep_Guid;
                }

                if (Budget_Guid.CompareTo(System.Guid.Empty) != 0)
                {
                    cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Budget_Guid", System.Data.SqlDbType.UniqueIdentifier));
                    cmd.Parameters["@Budget_Guid"].Value = Budget_Guid;
                }

                System.Data.SqlClient.SqlDataReader rs = cmd.ExecuteReader();
                if (rs.HasRows)
                {
                    while (rs.Read())
                    {
                        objList.Add(new CUser()
                        {
                            ulID = System.Convert.ToInt32(rs["ErpBudgetUserID"]),
                            ulUniXPID = System.Convert.ToInt32(rs["UniXPUserID"]),
                            UserLastName = System.Convert.ToString(rs["strLastName"]),
                            UserFirstName = System.Convert.ToString(rs["strFirstName"]),
                            IsBlocked = System.Convert.ToBoolean(rs["IsUserBlocked"])
                        }
                        );
                    }
                }
                else
                {
                    strErr += ("\nСписок пользователей для заданного действия пуст.");
                }
                rs.Close();
                rs.Dispose();
                cmd.Dispose();
            }
            catch (System.Exception e)
            {
                strErr += ("\nЗапрос списка пользователей для заданного действия.\nТекст ошибки: " + e.Message);
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
            return Name;
        }
    }
}
