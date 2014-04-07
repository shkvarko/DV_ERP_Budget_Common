using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace ERP_Budget.Common
{
    /// <summary>
    /// Класс "Бюджетное подразделение"
    /// </summary>
    public class CBudgetDep : IBaseListItem
    {
        #region Переменные, Свойства, Константы
        /// <summary>
        /// Родительское бюджетное подразделение
        /// </summary>
        private System.Guid m_uuidParentID;
        /// <summary>
        /// Распорядитель бюджета
        /// </summary>
        private CUser m_Manager;
        /// <summary>
        /// Список пользователей
        /// </summary>
        private CBaseList<CUser> m_UsesrList;

        /// <summary>
        /// Родительское бюджетное подразделение
        /// </summary>
        public System.Guid ParentID
        {
            get { return m_uuidParentID; }
            set { m_uuidParentID = value; }
        }

        /// <summary>
        /// Распорядитель бюджета
        /// </summary>
        public ERP_Budget.Common.CUser Manager
        {
            get { return m_Manager; }
            set { m_Manager = value; }
        }

        /// <summary>
        /// Список пользователей
        /// </summary>
        public ERP_Budget.Common.CBaseList<CUser> UsesrList
        {
            get { return m_UsesrList; }
            set { m_UsesrList = value; }
        }
        /// <summary>
        /// Признак "только для чтения"
        /// </summary>
        System.Boolean m_bReadOnly;
        /// <summary>
        /// Свойство "только для чтения"
        /// </summary>
        public System.Boolean ReadOnly
        {
            get { return m_bReadOnly; }
            set { m_bReadOnly = value; }
        }
        /// <summary>
        /// Состояние объекта
        /// </summary>
        private System.Data.DataRowState m_State;
        /// <summary>
        /// Состояние объекта
        /// </summary>
        public System.Data.DataRowState State
        {
            get { return m_State; }
            set { m_State = value; }
        }
        /// <summary>
        /// Признак "Наличие дочерних подразделений"
        /// </summary>
        private System.Boolean m_bHasChildren;
        /// <summary>
        /// Признак "Наличие дочерних подразделений"
        /// </summary>
        public System.Boolean HasChildren
        {
            get { return m_bHasChildren; }
        }
        /// <summary>
        /// Признак "Наличие бюджета"
        /// </summary>
        private System.Boolean m_bHasBudget;
        /// <summary>
        /// Признак "Наличие бюджета"
        /// </summary>
        public System.Boolean HasBudget
        {
            get { return m_bHasBudget; }
        }
        /// <summary>
        /// Список дополнительных распорядителей бюджета службы
        /// </summary>
        private List<CUser> m_objAdditionalManagerList;
        /// <summary>
        /// Список дополнительных распорядителей бюджета службы
        /// </summary>
        public List<CUser> AdditionalManagerList
        {
            get { return m_objAdditionalManagerList; }
            set { m_objAdditionalManagerList = value; }
        }
        /// <summary>
        /// Признак "тип бюджетных расходов" используется только для привязки "статья - служба - тип расходов - проект"
        /// </summary>
        private CBudgetExpenseType m_objBudgetExpenseType;
        /// <summary>
        /// Признак "тип бюджетных расходов" используется только для привязки "статья - служба - тип расходов - проект"
        /// </summary>
        public CBudgetExpenseType BudgetExpenseType
        {
            get { return m_objBudgetExpenseType; }
            set { m_objBudgetExpenseType = value; }
        }
        /// <summary>
        /// Признак "Проект", используется только для привязки "статья - служба - тип расходов - проект"
        /// </summary>
        public CBudgetProject BudgetProject { get; set; }

    #endregion

        #region Конструкторы 
        public CBudgetDep()
        {
            this.m_uuidID = System.Guid.Empty;
            this.m_strName = "";
            this.m_Manager = null;
            this.m_uuidParentID = System.Guid.Empty;
            this.m_UsesrList = null;
            this.m_bReadOnly = false;
            this.m_UsesrList = new CBaseList<CUser>();
            this.m_objAdditionalManagerList = new List<CUser>();
            this.m_UsesrList.ClearList();
            this.m_bHasChildren = false;
            this.m_bHasBudget = false;
            m_objBudgetExpenseType = null;
            BudgetProject = null;
        }

        public CBudgetDep( System.Guid uuidID )
        {
            this.m_uuidID = uuidID;
            this.m_strName = "";
            this.m_Manager = null;
            this.m_uuidParentID = System.Guid.Empty;
            this.m_UsesrList = null;
            this.m_bReadOnly = false;
            this.m_UsesrList = new CBaseList<CUser>();
            this.m_objAdditionalManagerList = new List<CUser>();
            this.m_UsesrList.ClearList();
            this.m_bHasChildren = false;
            this.m_bHasBudget = false;
            m_objBudgetExpenseType = null;
            BudgetProject = null;
        }

        public CBudgetDep( System.Guid uuidID, System.String strName )
        {
            this.m_uuidID = uuidID;
            this.m_strName = strName;
            this.m_Manager = null;
            this.m_uuidParentID = System.Guid.Empty;
            this.m_UsesrList = null;
            this.m_bReadOnly = false;
            this.m_UsesrList = new CBaseList<CUser>();
            this.m_objAdditionalManagerList = new List<CUser>();
            this.m_UsesrList.ClearList();
            this.m_bHasChildren = false;
            this.m_bHasBudget = false;
            m_objBudgetExpenseType = null;
            BudgetProject = null;
        }
        public CBudgetDep( System.Guid uuidID, System.String strName, CUser objManager )
        {
            this.m_uuidID = uuidID;
            this.m_strName = strName;
            this.m_Manager = objManager;
            this.m_uuidParentID = System.Guid.Empty;
            this.m_UsesrList = null;
            this.m_bReadOnly = false;
            this.m_UsesrList = new CBaseList<CUser>();
            this.m_objAdditionalManagerList = new List<CUser>();
            this.m_UsesrList.ClearList();
            this.m_bHasChildren = false;
            this.m_bHasBudget = false;
            m_objBudgetExpenseType = null;
            BudgetProject = null;
        }
        #endregion 

        #region Список бюджетных подразделений 
        /// <summary>
        /// Возвращает список бюджетных подразделений
        /// </summary>
        /// <param name="objProfile">профайл</param>
        /// <param name="bInitUserList">признак того, нужно ли запрашивать состав бюджетного подразделения</param>
        /// <returns>список бюджетных подразделений</returns>
        public static List<CBudgetDep> GetBudgetDepsList( UniXP.Common.CProfile objProfile, System.Boolean bInitUserList )
        {
            List<CBudgetDep> objList = new List<CBudgetDep>();

            System.Data.SqlClient.SqlConnection DBConnection = objProfile.GetDBSource();
            if( DBConnection == null ) { return objList; }

            try
            {
                // соединение с БД получено, прописываем команду на выборку данных
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
                cmd.Connection = DBConnection;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = System.String.Format( "[{0}].[dbo].[sp_GetBudgetDep]", objProfile.GetOptionsDllDBName() );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ( ( System.Byte )( 0 ) ), ( ( System.Byte )( 0 ) ), "", System.Data.DataRowVersion.Current, null ) );
                System.Data.SqlClient.SqlDataReader rs = cmd.ExecuteReader();
                if( rs.HasRows )
                {
                    // набор данных непустой
                    CBudgetDep objBudgetDep = null;
                    while (rs.Read())
                    {
                        objBudgetDep = new CBudgetDep();
                        objBudgetDep.m_uuidID = (System.Guid)rs["GUID_ID"];
                        objBudgetDep.m_strName = (System.String)rs["BUDGETDEP_NAME"];

                        if (rs["PARENT_GUID_ID"] != System.DBNull.Value) { objBudgetDep.m_uuidParentID = (System.Guid)rs["PARENT_GUID_ID"]; }
                        
                        objBudgetDep.m_Manager = new CUser();
                        objBudgetDep.m_Manager.IsBlocked = System.Convert.ToBoolean(rs["IsUserBlocked"]);
                        objBudgetDep.m_Manager.Init(objProfile, (System.Int32)rs["BUDGETDEP_MANAGER"]);

                        objBudgetDep.m_bReadOnly = (System.Boolean)rs["ISREADONLY"];
                        objBudgetDep.m_bHasChildren = (System.Boolean)rs["HASCHILDREN"];
                        //if (rs["PARENT_GUID_ID"] != System.DBNull.Value)
                        //{
                        //    objBudgetDep.ParentID = (System.Guid)rs["PARENT_GUID_ID"];
                        //}
                        objList.Add(objBudgetDep);
                    }
                }
                rs.Close();
                rs.Dispose();

                if (bInitUserList == true)
                {
                    // состав службы
                    foreach (CBudgetDep objBudgetDepItem in objList)
                    {
                        objBudgetDepItem.m_UsesrList = objBudgetDepItem.GetUserList(objProfile, objBudgetDepItem.m_uuidID);
                    }
                }

                // 20140313 попробую отключить
                // попробуем подгрузить список распорядителей службы
                //foreach (CBudgetDep objBudgetDepItem in objList)
                //{
                //    objBudgetDepItem.LoadAdditionalManagerList(objProfile, cmd);
                //}

                cmd.Dispose();
            }
            catch( System.Exception f )
            {
                DevExpress.XtraEditors.XtraMessageBox.Show( 
                "Не удалось получить список бюджетных подразделений.\n\nТекст ошибки: " + f.Message, "Внимание",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
            }
            finally // очищаем занимаемые ресурсы
            {
                DBConnection.Close();
            }

            return objList;
        }
        /// <summary>
        /// Возвращает список бюджетных подразделений без указания руководителей подразделений и состава подразделения
        /// </summary>
        /// <param name="objProfile">профайл</param>
        /// <returns>список бюджетных подразделений</returns>
        public static List<CBudgetDep> GetBudgetDepartmentListWhitoutManager(UniXP.Common.CProfile objProfile, ref System.String strErr)
        {
            List<CBudgetDep> objList = new List<CBudgetDep>();

            System.Data.SqlClient.SqlConnection DBConnection = objProfile.GetDBSource();
            if (DBConnection == null) { return objList; }

            try
            {
                // соединение с БД получено, прописываем команду на выборку данных
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
                cmd.Connection = DBConnection;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = System.String.Format("[{0}].[dbo].[sp_GetBudgetDep2]", objProfile.GetOptionsDllDBName());
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ((System.Byte)(0)), ((System.Byte)(0)), "", System.Data.DataRowVersion.Current, null));
                System.Data.SqlClient.SqlDataReader rs = cmd.ExecuteReader();
                if (rs.HasRows)
                {
                    // набор данных непустой
                    while (rs.Read())
                    {
                        objList.Add(new CBudgetDep()
                        {
                            m_uuidID = (System.Guid)rs["GUID_ID"],
                            Name = (System.String)rs["BUDGETDEP_NAME"],
                            //ParentID = ((rs["PARENT_GUID_ID"] != System.DBNull.Value) ? (System.Guid)rs["PARENT_GUID_ID"] : System.Guid.Empty),
                            Manager = new CUser(),
                            ReadOnly = true,
                            m_bHasChildren = System.Convert.ToBoolean( rs["HASCHILDREN"] )
                        }
                        );
                    }

                }
                rs.Close();
                rs.Dispose();

                cmd.Dispose();
            }
            catch (System.Exception f)
            {
                strErr += ("Не удалось получить список бюджетных подразделений.\n\nТекст ошибки: " + f.Message);
            }
            finally // очищаем занимаемые ресурсы
            {
                DBConnection.Close();
            }

            return objList;
        }
        /// <summary>
        /// Возвращает список бюджетных подразделений
        /// </summary>
        /// <param name="objProfile">профайл</param>
        /// <returns>список бюджетных подразделений</returns>
        public static List<CBudgetDep> GetBudgetDepsListForBudgetDoc( UniXP.Common.CProfile objProfile,
            System.Boolean bInitManager = true, System.Boolean bLoadAdditionalManagerList = true )
        {
            List<CBudgetDep> objList = new List<CBudgetDep>();

            System.Data.SqlClient.SqlConnection DBConnection = objProfile.GetDBSource();
            if (DBConnection == null) { return objList; }

            try
            {
                // соединение с БД получено, прописываем команду на выборку данных
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
                cmd.Connection = DBConnection;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = System.String.Format("[{0}].[dbo].[sp_GetBudgetDepForBudgetDoc]", objProfile.GetOptionsDllDBName());
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ((System.Byte)(0)), ((System.Byte)(0)), "", System.Data.DataRowVersion.Current, null));
                System.Data.SqlClient.SqlDataReader rs = cmd.ExecuteReader();
                if (rs.HasRows)
                {
                    // набор данных непустой
                    CBudgetDep objBudgetDep = null;
                    while (rs.Read())
                    {
                        objBudgetDep = new CBudgetDep();
                        objBudgetDep.m_uuidID = (System.Guid)rs["GUID_ID"];
                        objBudgetDep.m_strName = (System.String)rs["BUDGETDEP_NAME"];
                        if (rs["PARENT_GUID_ID"] != System.DBNull.Value) { objBudgetDep.m_uuidParentID = (System.Guid)rs["PARENT_GUID_ID"]; }
                        objBudgetDep.m_Manager = new CUser();
                        objBudgetDep.m_Manager.ulID = (System.Int32)rs["BUDGETDEP_MANAGER"];
                        objBudgetDep.m_Manager.ulUniXPID = (System.Int32)rs["ulUniXPID"];

                        if( bInitManager == true )
                        {
                            objBudgetDep.m_Manager.Init(objProfile, (System.Int32)rs["BUDGETDEP_MANAGER"]);
                        }

                        objList.Add(objBudgetDep);
                    }
                }
                rs.Close();
                rs.Dispose();

                if (bLoadAdditionalManagerList == true)
                {
                    // список дополнительных распорядителей службы
                    foreach (CBudgetDep objBudgetDepItem in objList)
                    {
                        objBudgetDepItem.LoadAdditionalManagerList(objProfile, cmd);
                    }
                }

                cmd.Dispose();
            }
            catch (System.Exception f)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(
                "Не удалось получить список бюджетных подразделений.\n\nТекст ошибки: " + f.Message, "Внимание",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
            finally // очищаем занимаемые ресурсы
            {
                DBConnection.Close();
            }

            return objList;
        }
        /// <summary>
        /// Обновляет список бюджетных подразделений
        /// </summary>
        /// <param name="objProfile">профайл</param>
        /// <param name="cmd">SQL - команда</param>
        /// <param name="cmd">objList - обновляемый список</param>
        /// <returns></returns>
        public static void RefreshBudgetDepList( UniXP.Common.CProfile objProfile,
            System.Data.SqlClient.SqlCommand cmd, System.Collections.Generic.List<CBudgetDep> objList )
        {
            if( cmd == null ) { return; }
            objList.Clear();

            try
            {
                cmd.Parameters.Clear();
                cmd.CommandText = System.String.Format( "[{0}].[dbo].[sp_GetBudgetDepForBudgetDoc]", objProfile.GetOptionsDllDBName() );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ( ( System.Byte )( 0 ) ), ( ( System.Byte )( 0 ) ), "", System.Data.DataRowVersion.Current, null ) );
                System.Data.SqlClient.SqlDataReader rs = cmd.ExecuteReader();
                if( rs.HasRows )
                {
                    // набор данных непустой
                    while( rs.Read() )
                    {
                        CUser objManager = new CUser();
                        objManager.ulID = ( System.Int32 )rs[ "BUDGETDEP_MANAGER" ];
                        objManager.ulUniXPID = ( System.Int32 )rs[ "ulUniXPID" ];
                        objList.Add( new CBudgetDep( ( System.Guid )rs[ "GUID_ID" ], ( System.String )rs[ "BUDGETDEP_NAME" ], objManager ) );
                    }
                }
                rs.Close();
                rs.Dispose();

                // попробуем подгрузить список распорядителей службы
                foreach( CBudgetDep objBudgetDep in objList )
                {
                    objBudgetDep.LoadAdditionalManagerList( objProfile, cmd );
                }
            }
            catch( System.Exception e )
            {
                DevExpress.XtraEditors.XtraMessageBox.Show( 
                "Не удалось получить список бюджетных подразделений.\nТекст ошибки: " + e.Message, "Внимание",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
            }
            finally // очищаем занимаемые ресурсы
            {
            }
            return ;
        }
        /// <summary>
        /// Загружает список дополнительных распорядителей бюджета
        /// </summary>
        /// <param name="objProfile">профайл</param>
        /// <param name="cmd">SQL-команда</param>
        public void LoadAdditionalManagerList( UniXP.Common.CProfile objProfile, System.Data.SqlClient.SqlCommand cmd )
        {
            this.m_objAdditionalManagerList.Clear();
            if( cmd == null ) { return; }

            try
            {
                // соединение с БД получено, прописываем команду на выборку данных
                cmd.Parameters.Clear();
                cmd.CommandText = System.String.Format( "[{0}].[dbo].[sp_GetBudgetDepManagerList]", objProfile.GetOptionsDllDBName() );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ( ( System.Byte )( 0 ) ), ( ( System.Byte )( 0 ) ), "", System.Data.DataRowVersion.Current, null ) );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@BUDGETDEP_GUID_ID", System.Data.SqlDbType.UniqueIdentifier, 4 ) );
                cmd.Parameters[ "@BUDGETDEP_GUID_ID" ].Value = this.uuidID;
                System.Data.SqlClient.SqlDataReader rs = cmd.ExecuteReader();
                if( rs.HasRows )
                {
                    while( rs.Read() )
                    {
                        this.m_objAdditionalManagerList.Add( new CUser( ( System.Int32 )rs[ "ErpBudgetUserID" ], ( System.Int32 )rs[ "UniXPUserID" ],
                            ( System.String )rs[ "strLastName" ], ( System.String )rs[ "strFirstName" ] ) );
                    }
                }
                rs.Close();
                rs.Dispose();
            }
            catch( System.Exception e )
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(
                "Не удалось получить список распорядителей \nдля заданной службы.\nТекст ошибки: " + e.Message, "Внимание",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
            }
            finally // очищаем занимаемые ресурсы
            {
            }
            return;
        }
        /// <summary>
        /// Определяет, является ли заданный пользователь распорядителем службы
        /// </summary>
        /// <param name="objUser">пользователь</param>
        /// <returns>true - является; false - нет</returns>
        public System.Boolean IsBudgetDepManager( CUser objUser )
        {
            System.Boolean bRet = false;
            if( objUser == null ) { return bRet; }

            try
            {
                if( this.m_Manager != null )
                {
                    bRet = ( this.m_Manager.ulID == objUser.ulID );
                }
                if( bRet == false )
                {
                    // поисчем в дополнительных распорядителях
                    if( ( this.m_objAdditionalManagerList != null ) && ( this.m_objAdditionalManagerList.Count > 0 ) )
                    {
                        foreach( CUser objUserItem in this.m_objAdditionalManagerList )
                        {
                            if( objUserItem.ulID == objUser.ulID )
                            {
                                bRet = true;
                                break;
                            }
                        }
                    }
                }
            }
            catch( System.Exception e )
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(
                "Не удалось определить, является ли пользователь распорядителем службы.\nТекст ошибки: " + e.Message, "Внимание",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
            }
            finally // очищаем занимаемые ресурсы
            {
            }
            return bRet;
        }
        /// <summary>
        /// Определяет, является ли заданный пользователь распорядителем службы
        /// </summary>
        /// <param name="SQLUserID">уи пользователя в БД UniXP</param>
        /// <returns>true - является; false - нет</returns>
        public System.Boolean IsBudgetDepManager( System.Int32 SQLUserID )
        {
            System.Boolean bRet = false;

            try
            {
                if( this.m_Manager != null )
                {
                    bRet = ( this.m_Manager.ulUniXPID == SQLUserID );
                }
                if( bRet == false )
                {
                    // поисчем в дополнительных распорядителях
                    if( ( this.m_objAdditionalManagerList != null ) && ( this.m_objAdditionalManagerList.Count > 0 ) )
                    {
                        foreach( CUser objUserItem in this.m_objAdditionalManagerList )
                        {
                            if( objUserItem.ulUniXPID == SQLUserID )
                            {
                                bRet = true;
                                break;
                            }
                        }
                    }
                }
            }
            catch( System.Exception e )
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(
                "Не удалось определить, является ли пользователь распорядителем службы.\nТекст ошибки: " + e.Message, "Внимание",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
            }
            finally // очищаем занимаемые ресурсы
            {
            }
            return bRet;
        }

        /// <summary>
        /// Возвращает список бюджетных подразделений для заданной статьи расходов
        /// </summary>
        /// <param name="objProfile">профайл</param>
        /// <param name="uuidDebitArticleID">идентификаор статьи расходов</param>
        /// <returns>список бюджетных подразделений</returns>
        public static List<CBudgetDep> GetBudgetDepListForDebitArticle( UniXP.Common.CProfile objProfile, 
            System.Guid uuidDebitArticleID )
        {
            List<CBudgetDep> objList = new List<CBudgetDep>();

            System.Data.SqlClient.SqlConnection DBConnection = objProfile.GetDBSource();
            if( DBConnection == null ) { return objList; }

            try
            {
                // соединение с БД получено, прописываем команду на выборку данных
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand() { Connection = DBConnection, 
                    CommandType = System.Data.CommandType.StoredProcedure, CommandText = System.String.Format("[{0}].[dbo].[sp_GetDebitArticleBudgetDep]", 
                    objProfile.GetOptionsDllDBName()) };
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ( ( System.Byte )( 0 ) ), ( ( System.Byte )( 0 ) ), "", System.Data.DataRowVersion.Current, null ) );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@DEBITARTICLE_GUID_ID", System.Data.SqlDbType.UniqueIdentifier, 4 ) );
                cmd.Parameters[ "@DEBITARTICLE_GUID_ID" ].Value = uuidDebitArticleID;
                System.Data.SqlClient.SqlDataReader rs = cmd.ExecuteReader();
                if( rs.HasRows )
                {
                    // набор данных непустой
                    CBudgetDep objBudgetDep = null;
                    while( rs.Read() )
                    {
                        objBudgetDep = new CBudgetDep()
                        {
                            uuidID = (System.Guid)rs["BUDGETDEP_GUID_ID"],
                            Name = System.Convert.ToString(rs["BUDGETDEP_NAME"]),
                            ParentID = ((rs["PARENT_GUID_ID"] != System.DBNull.Value) ? (System.Guid)rs["PARENT_GUID_ID"] : System.Guid.Empty),
                            Manager = new CUser()
                        };

                        objBudgetDep.Manager.Init(objProfile, System.Convert.ToInt32(rs["BUDGETDEP_MANAGER"]));

                        objList.Add( objBudgetDep );
                    }
                }
                cmd.Dispose();
                rs.Dispose();
            }
            catch( System.Exception e )
            {
                DevExpress.XtraEditors.XtraMessageBox.Show( 
                "Не удалось получить список бюджетных подразделений \nдля заданной статьи расходов.\n" + e.Message, "Внимание",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
            }
            finally // очищаем занимаемые ресурсы
            {
                DBConnection.Close();
            }
            return objList;
        }
        /// <summary>
        /// Возвращает список бюджетных подразделений для заданной статьи расходов
        /// </summary>
        /// <param name="objProfile">профайл</param>
        /// <param name="objDebitArticle">статья расходов</param>
        /// <param name="cmd">SQL - команда</param>
        /// <returns>список бюджетных подразделений</returns>
        public static void RefreshBudgetDepListForDebitArticle( UniXP.Common.CProfile objProfile,
            CDebitArticle objDebitArticle, System.Data.SqlClient.SqlCommand cmd )
        {
            objDebitArticle.BudgetDepList.Clear();
            if( cmd == null ) { return; }

            try
            {
                // соединение с БД получено, прописываем команду на выборку данных
                cmd.Parameters.Clear();
                cmd.CommandText = System.String.Format( "[{0}].[dbo].[sp_GetDebitArticleBudgetDep]", objProfile.GetOptionsDllDBName() );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ( ( System.Byte )( 0 ) ), ( ( System.Byte )( 0 ) ), "", System.Data.DataRowVersion.Current, null ) );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@DEBITARTICLE_GUID_ID", System.Data.SqlDbType.UniqueIdentifier, 4 ) );
                cmd.Parameters[ "@DEBITARTICLE_GUID_ID" ].Value = objDebitArticle.uuidID;
                System.Data.SqlClient.SqlDataReader rs = cmd.ExecuteReader();
                if( rs.HasRows )
                {
                    while( rs.Read() )
                    {
                        objDebitArticle.BudgetDepList.Add( new CBudgetDep( rs.GetGuid( 0 ), rs.GetString( 2 ) ) );
                    }
                }
                rs.Close();
                rs.Dispose();
            }
            catch( System.Exception e )
            {
                DevExpress.XtraEditors.XtraMessageBox.Show( 
                "Не удалось получить список бюджетных подразделений \nдля заданной статьи расходов.\n" + e.Message, "Внимание",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
            }
            finally // очищаем занимаемые ресурсы
            {
            }
            return ;
        }
        /// <summary>
        /// Возвращает состав бюджетного подразделения
        /// </summary>
        /// <param name="objProfile">профайл</param>
        /// <param name="uuidBudgetDepID">уникальный идентификатор бюджетного подразделения</param>
        /// <returns>список пользователей, входящих в бюджетное подразделение</returns>
        private CBaseList<CUser> GetUserList( UniXP.Common.CProfile objProfile, System.Guid uuidBudgetDepID )
        {
            CBaseList<CUser> objList = new CBaseList<CUser>();

            System.Data.SqlClient.SqlConnection DBConnection = objProfile.GetDBSource();
            if( DBConnection == null ) { return objList; }

            try
            {
                // соединение с БД получено, прописываем команду на выборку данных
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
                cmd.Connection = DBConnection;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = System.String.Format( "[{0}].[dbo].[sp_GetBudgetDepDeclaration]", objProfile.GetOptionsDllDBName() );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ( ( System.Byte )( 0 ) ), ( ( System.Byte )( 0 ) ), "", System.Data.DataRowVersion.Current, null ) );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@BUDGETDEP_GUID_ID", System.Data.SqlDbType.UniqueIdentifier ) );
                cmd.Parameters[ "@BUDGETDEP_GUID_ID" ].Value = uuidBudgetDepID;
                System.Data.SqlClient.SqlDataReader rs = cmd.ExecuteReader();
                if( rs.HasRows )
                {
                    CUser objUser = null;
                    while( rs.Read() )
                    {
                        objUser = new CUser();

                        objUser.UserLastName = System.Convert.ToString(rs["strLastName"]);
                        objUser.UserMiddleName = System.Convert.ToString(rs["strMiddleName"]);
                        objUser.UserFirstName = System.Convert.ToString(rs["strFirstName"]);
                        objUser.ulID = System.Convert.ToInt32(rs["ulUserID"]);
                        objUser.ulUniXPID = System.Convert.ToInt32(rs["UniXPUserID"]);
                        
                        ////objUser.Init( objProfile, rs.GetInt32( 1 ) );
                        
                        objList.AddItemToList( objUser );
                    }
                }
                cmd.Dispose();
                rs.Dispose();
            }
            catch( System.Exception e )
            {
                DevExpress.XtraEditors.XtraMessageBox.Show( 
                "Не удалось получить состав бюджетного подразделения.\n" + e.Message, "Внимание",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
            }
            finally // очищаем занимаемые ресурсы
            {
                DBConnection.Close();
            }
            return objList;
        }

        public static System.Windows.Forms.TreeView GetBudgetTreeView(UniXP.Common.CProfile objProfile,
            UniXP.Common.MENUITEM objMenuItem, System.Boolean bSuperVisor, System.Boolean bInspector, System.Boolean bManager)
        {
            System.Windows.Forms.TreeView objTreeView = new System.Windows.Forms.TreeView();
            System.Data.SqlClient.SqlConnection DBConnection = objProfile.GetDBSource();
            if (DBConnection == null) { return objTreeView; }

            try
            {
                UniXP.Common.MENUITEM objMenuNode = null;
                UniXP.Common.MENUITEM objMenuNodeChild = null;

                // запрашиваем список бюджетов
                List<ERP_Budget.Common.CBudget> objBudgetList = ERP_Budget.Common.CBudget.GetBudgetList(objProfile);
                if ((objBudgetList == null) || (objBudgetList.Count == 0)) { return objTreeView; }

                // соединение с БД получено, прописываем команду на выборку данных
                List<System.Int32> YearList = new List<int>();
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
                cmd.Connection = DBConnection;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = System.String.Format("[{0}].[dbo].[sp_GetYearsForBudget]", objProfile.GetOptionsDllDBName());
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ((System.Byte)(0)), ((System.Byte)(0)), "", System.Data.DataRowVersion.Current, null));
                System.Data.SqlClient.SqlDataReader rs = cmd.ExecuteReader();
                if (rs.HasRows)
                {
                    // набор данных непустой - у нас есть список годов, для которых были созданы бюджеты
                    while (rs.Read())
                    {
                        // добавляем узелок с именем бюджетного подразделения
                        objMenuNode = new UniXP.Common.MENUITEM();
                        objMenuNode.enMenuType = objMenuItem.enMenuType;
                        objMenuNode.strName = ( (System.Int32)rs["BudgetYear"] ).ToString();
                        objMenuNode.lClassID = 1;
                        objMenuNode.uuidFarClient = objMenuItem.uuidFarClient;
                        objMenuNode.strDescription = "";
                        objMenuNode.nImage = objMenuItem.nImage;
                        objMenuNode.strDLLName = objMenuItem.strDLLName;
                        objMenuNode.enCmdType = objMenuItem.enCmdType;
                        objMenuNode.enDLLType = objMenuItem.enDLLType;
                        objMenuNode.objProfile = objMenuItem.objProfile;
                        objMenuNode.objAdvancedParametr = null;

                        System.Windows.Forms.TreeNode objMenuTreeNode = new System.Windows.Forms.TreeNode();
                        objMenuTreeNode.Text = objMenuNode.strName;
                        objMenuTreeNode.ImageIndex = objMenuItem.nImage;
                        objMenuTreeNode.SelectedImageIndex = objMenuItem.nImage;
                        objMenuTreeNode.Tag = objMenuNode;
                        objTreeView.Nodes.Add(objMenuTreeNode);
                    }
                }

                if (objTreeView.Nodes.Count > 0)
                {
                    // мы построили узелки с годами, теперь нужно построить узелки с бюджетными подразделениями                    
                    foreach (System.Windows.Forms.TreeNode objNodeYear in objTreeView.Nodes)
                    {
                        List<CBudgetDep> ParentBudgetDepList = GetBudgetDepParenList(objProfile, cmd, System.Convert.ToInt32(objNodeYear.Text));
                        if ((ParentBudgetDepList != null) && (ParentBudgetDepList.Count > 0))
                        {
                            foreach (CBudgetDep objParentBudgetDep in ParentBudgetDepList)
                            {
                                // узел с родительским бюджетным подразделением
                                objMenuNodeChild = new UniXP.Common.MENUITEM();
                                objMenuNodeChild.enMenuType = objMenuItem.enMenuType;
                                objMenuNodeChild.strName = objParentBudgetDep.Name; //objBudgetItem.Date.Year.ToString() + " (" + objBudgetDep.Name + ")"; // "Бюджет " + objBudgetItem.Date.Year.ToString();
                                objMenuNodeChild.lClassID = 1;
                                objMenuNodeChild.uuidFarClient = objMenuItem.uuidFarClient;
                                objMenuNodeChild.strDescription = objParentBudgetDep.Name;
                                objMenuNodeChild.nImage = objMenuItem.nImage;
                                objMenuNodeChild.strDLLName = objMenuItem.strDLLName;
                                objMenuNodeChild.enCmdType = objMenuItem.enCmdType;
                                objMenuNodeChild.enDLLType = objMenuItem.enDLLType;
                                objMenuNodeChild.objProfile = objMenuItem.objProfile;
                                objMenuNodeChild.objAdvancedParametr = null;

                                System.Windows.Forms.TreeNode objMenuChildTreeNode = new System.Windows.Forms.TreeNode();
                                objMenuChildTreeNode.ImageIndex = objMenuItem.nImage;
                                objMenuChildTreeNode.SelectedImageIndex = objMenuItem.nImage;
                                objMenuChildTreeNode.Text = objMenuNodeChild.strName;
                                objMenuChildTreeNode.Tag = objMenuNodeChild;
                                objNodeYear.Nodes.Add(objMenuChildTreeNode);

                                if (objParentBudgetDep.HasBudget == true)
                                {
                                    foreach (ERP_Budget.Common.CBudget objBudgetItem in objBudgetList)
                                    {
                                        // бюджет с признаком "Внебюджетные расходы" мы показываем только контролеру
                                        if ((objBudgetItem.OffExpenditures == true) && bSuperVisor) { continue; };
                                        if ((objBudgetItem.BudgetDep.uuidID.CompareTo(objParentBudgetDep.uuidID) == 0) && (objBudgetItem.Date.Year == System.Convert.ToInt32(objNodeYear.Text)))
                                        {
                                            objMenuNodeChild.objAdvancedParametr = objBudgetItem.uuidID;
                                            // вот тут не мешало бы запустить рекурсию 
                                        }
                                    }

                                }
                            }
                        }
                    }

                }

                rs.Close();
                rs.Dispose();
                cmd.Dispose();
            }
            catch (System.Exception f)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(
                "Не удалось получить список бюджетных подразделений.\n\nТекст ошибки: " + f.Message, "Внимание",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
            finally // очищаем занимаемые ресурсы
            {
                DBConnection.Close();
            }

            return objTreeView;
        }

        /// <summary>
        /// Возвращает список родительских бюджетных подразделений
        /// </summary>
        /// <param name="objProfile">профайл</param>
        /// <param name="iBudgetYear">год</param>
        /// <returns>список бюджетных подразделений</returns>
        public static List<CBudgetDep> GetBudgetDepParenList(UniXP.Common.CProfile objProfile, 
            System.Data.SqlClient.SqlCommand cmd, System.Int32 iBudgetYear)
        {
            List<CBudgetDep> objList = new List<CBudgetDep>();

            if (cmd == null) { return objList; }

            try
            {
                cmd.Parameters.Clear();
                cmd.CommandText = System.String.Format("[{0}].[dbo].[sp_GetBudgetDepParent]", objProfile.GetOptionsDllDBName());
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ((System.Byte)(0)), ((System.Byte)(0)), "", System.Data.DataRowVersion.Current, null));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@BudgetYear", System.Data.SqlDbType.Int));
                cmd.Parameters["@BudgetYear"].Value = iBudgetYear;
                System.Data.SqlClient.SqlDataReader rs = cmd.ExecuteReader();
                if (rs.HasRows)
                {
                    // набор данных непустой
                    CBudgetDep objBudgetDep = null;
                    while (rs.Read())
                    {
                        objBudgetDep = new CBudgetDep();
                        objBudgetDep.m_uuidID = (System.Guid)rs["GUID_ID"];
                        objBudgetDep.m_strName = (System.String)rs["BUDGETDEP_NAME"];
                        if (rs["PARENT_GUID_ID"] != System.DBNull.Value) { objBudgetDep.m_uuidParentID = (System.Guid)rs["PARENT_GUID_ID"]; }
                        objBudgetDep.m_Manager = new CUser();
                        objBudgetDep.m_Manager.Init(objProfile, (System.Int32)rs["BUDGETDEP_MANAGER"]);
                        objBudgetDep.m_bHasBudget = (System.Boolean)rs["IsContainingBudget"];
                        objList.Add(objBudgetDep);
                    }
                }
                rs.Close();
                rs.Dispose();
            }
            catch (System.Exception f)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(
                "Не удалось получить список бюджетных подразделений.\n\nТекст ошибки: " + f.Message, "Внимание",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
            finally // очищаем занимаемые ресурсы
            {
            }

            return objList;
        }
        /// <summary>
        /// Возвращает список родительских бюджетных подразделений
        /// </summary>
        /// <param name="objProfile">профайл</param>
        /// <param name="uuidBudgetDepId">УИ родительского подразделения</param>
        /// <param name="iBudgetYear">год</param>
        /// <returns>список бюджетных подразделений</returns>
        public static List<CBudgetDep> GetBudgetDepChildList(UniXP.Common.CProfile objProfile, System.Guid uuidBudgetDepId, 
            System.Int32 iBudgetYear)
        {
            List<CBudgetDep> objList = new List<CBudgetDep>();

            System.Data.SqlClient.SqlConnection DBConnection = objProfile.GetDBSource();
            if (DBConnection == null) { return objList; }

            try
            {
                // соединение с БД получено, прописываем команду на выборку данных
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
                cmd.Connection = DBConnection;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = System.String.Format("[{0}].[dbo].[sp_GetBudgetDepChild]", objProfile.GetOptionsDllDBName());
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ((System.Byte)(0)), ((System.Byte)(0)), "", System.Data.DataRowVersion.Current, null));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@BudgetYear", System.Data.SqlDbType.Int));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@BudgetDepParent_Guid", System.Data.SqlDbType.UniqueIdentifier));
                cmd.Parameters["@BudgetYear"].Value = iBudgetYear;
                cmd.Parameters["@BudgetDepParent_Guid"].Value = uuidBudgetDepId;
                System.Data.SqlClient.SqlDataReader rs = cmd.ExecuteReader();
                if (rs.HasRows)
                {
                    // набор данных непустой
                    CBudgetDep objBudgetDep = null;
                    while (rs.Read())
                    {
                        objBudgetDep = new CBudgetDep();
                        objBudgetDep.m_uuidID = (System.Guid)rs["GUID_ID"];
                        objBudgetDep.m_strName = (System.String)rs["BUDGETDEP_NAME"];
                        if (rs["PARENT_GUID_ID"] != System.DBNull.Value) { objBudgetDep.m_uuidParentID = (System.Guid)rs["PARENT_GUID_ID"]; }
                        objBudgetDep.m_Manager = new CUser();
                        objBudgetDep.m_Manager.Init(objProfile, (System.Int32)rs["BUDGETDEP_MANAGER"]);
                        objBudgetDep.m_bHasBudget = (System.Boolean)rs["IsContainingBudget"];
                        objList.Add(objBudgetDep);
                    }
                }
                rs.Close();
                rs.Dispose();
                //// попробуем подгрузить список распорядителей службы
                //foreach (CBudgetDep objBudgetDepItem in objList)
                //{
                //    objBudgetDepItem.LoadAdditionalManagerList(objProfile, cmd);
                //}

                cmd.Dispose();
            }
            catch (System.Exception f)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(
                "Не удалось получить список дочерних бюджетных подразделений.\n\nТекст ошибки: " + f.Message, "Внимание",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
            finally // очищаем занимаемые ресурсы
            {
                DBConnection.Close();
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
        public override System.Boolean Init( UniXP.Common.CProfile objProfile, System.Guid uuidID )
        {
            System.Boolean bRet = false;

            System.Data.SqlClient.SqlConnection DBConnection = objProfile.GetDBSource();
            if( DBConnection == null ) { return bRet; }

            try
            {
                // соединение с БД получено, прописываем команду на выборку данных
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
                cmd.Connection = DBConnection;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = System.String.Format( "[{0}].[dbo].[sp_GetBudgetDep]", objProfile.GetOptionsDllDBName() );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ( ( System.Byte )( 0 ) ), ( ( System.Byte )( 0 ) ), "", System.Data.DataRowVersion.Current, null ) );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@GUID_ID", System.Data.SqlDbType.UniqueIdentifier ) );
                cmd.Parameters[ "@GUID_ID" ].Value = uuidID;
                System.Data.SqlClient.SqlDataReader rs = cmd.ExecuteReader();
                if( rs.HasRows )
                {
                    // набор данных непустой, в нем нас интересует одна запись
                    rs.Read();
                    this.m_uuidID = rs.GetGuid( 0 );
                    this.m_strName = rs.GetString( 2 );
                    if( rs[ 1 ] != System.DBNull.Value ) { this.m_uuidParentID = rs.GetGuid( 1 ); }
                    CUser objManager = new CUser();
                    objManager.Init( objProfile, rs.GetInt32( 3 ) );
                    this.m_Manager = objManager;
                    this.m_UsesrList = this.GetUserList( objProfile, this.m_uuidID );
                    bRet = true;
                }
                else
                {
                    DevExpress.XtraEditors.XtraMessageBox.Show( 
                    "Не удалось проинициализировать класс CBudgetDep.\nВ БД не найдена информация.", "Внимание",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning );

                }
                cmd.Dispose();
                rs.Dispose();
            }
            catch( System.Exception e )
            {
                DevExpress.XtraEditors.XtraMessageBox.Show( 
                "Не удалось проинициализировать класс CBudgetDep.\n" + e.Message, "Внимание",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
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
        /// <param name="objProfile">профайл</param>
        /// <returns>true - успешная инициализация; false - ошибка</returns>
        public System.Boolean Init( UniXP.Common.CProfile objProfile )
        {
            System.Boolean bRet = false;

            System.Data.SqlClient.SqlConnection DBConnection = objProfile.GetDBSource();
            if( DBConnection == null ) { return bRet; }

            try
            {
                // соединение с БД получено, прописываем команду на выборку данных
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
                cmd.Connection = DBConnection;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = System.String.Format( "[{0}].[dbo].[sp_GetBudgetDepFromManager]", objProfile.GetOptionsDllDBName() );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ( ( System.Byte )( 0 ) ), ( ( System.Byte )( 0 ) ), "", System.Data.DataRowVersion.Current, null ) );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@ulUniXPID", System.Data.SqlDbType.Int ) );
                cmd.Parameters[ "@ulUniXPID" ].Value = objProfile.m_nSQLUserID;
                System.Data.SqlClient.SqlDataReader rs = cmd.ExecuteReader();
                if( rs.HasRows )
                {
                    // набор данных непустой, в нем нас интересует одна запись
                    rs.Read();
                    this.m_uuidID = rs.GetGuid( 0 );
                    this.m_strName = rs.GetString( 2 );
                    if( rs[ 1 ] != System.DBNull.Value ) { this.m_uuidParentID = rs.GetGuid( 1 ); }
                    CUser objManager = new CUser();
                    objManager.Init( objProfile, rs.GetInt32( 3 ) );
                    this.m_Manager = objManager;
                    this.m_UsesrList = this.GetUserList( objProfile, this.m_uuidID );
                    bRet = true;
                }
                else
                {
                    DevExpress.XtraEditors.XtraMessageBox.Show( 
                    "Не удалось проинициализировать класс CBudgetDep.\nВ БД не найдена информация.", "Внимание",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning );
                }
                cmd.Dispose();
                rs.Dispose();
            }
            catch( System.Exception e )
            {
                DevExpress.XtraEditors.XtraMessageBox.Show( 
                "Не удалось проинициализировать класс CBudgetDep.\n" + e.Message, "Внимание",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
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
        public override System.Boolean Remove( UniXP.Common.CProfile objProfile )
        {
            System.Boolean bRet = false;
            // уникальный идентификатор не должен быть пустым
            if( this.m_uuidID == System.Guid.Empty )
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(  "Недопустимое значение уникального идентификатора объекта", "Внимание",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
                return bRet;
            }

            System.Data.SqlClient.SqlConnection DBConnection = objProfile.GetDBSource();
            if( DBConnection == null ) { return bRet; }

            try
            {
                // соединение с БД получено, прописываем команду на удаление данных
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
                cmd.Connection = DBConnection;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = System.String.Format( "[{0}].[dbo].[sp_DeleteBudgetDep]", objProfile.GetOptionsDllDBName() );
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
                    if( iRet == 1 )
                    {
                        DevExpress.XtraEditors.XtraMessageBox.Show(  "Бюджетное подразделение связано с бюджетом.\nУдаление невозможно", "Внимание",
                            System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning );
                    }
                    else
                    {
                        DevExpress.XtraEditors.XtraMessageBox.Show(  "Ошибка удаления бюджетного подразделения", "Ошибка",
                            System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
                    }
                }
                cmd.Dispose();
            }
            catch( System.Exception e )
            {
                DevExpress.XtraEditors.XtraMessageBox.Show( 
                "Не удалось удалить бюджетное подразделение.\n" + e.Message, "Внимание",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
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
        /// Добавить запись в БД
        /// </summary>
        /// <param name="objProfile">профайл</param>
        /// <returns>true - удачное завершение; false - ошибка</returns>
        public override System.Boolean Add( UniXP.Common.CProfile objProfile )
        {
            System.Boolean bRet = false;

            // наименование не должен быть пустым
            if( this.m_strName == "" )
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(  "Недопустимое наименование бюджетного подразделения", "Внимание",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning );
                return bRet;
            }
            // необходимо указать распорядителя бюджета
            if( this.m_Manager == null )
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(  "Укажите распорядителя бюджетного подразделения", "Внимание",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning );
                return bRet;
            }

            System.Data.SqlClient.SqlConnection DBConnection = objProfile.GetDBSource();
            if( DBConnection == null ) { return bRet; }

            try
            {
                // соединение с БД получено, прописываем команду на создание записи в БД
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
                cmd.Connection = DBConnection;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = System.String.Format( "[{0}].[dbo].[sp_AddBudgetDep]", objProfile.GetOptionsDllDBName() );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ( ( System.Byte )( 0 ) ), ( ( System.Byte )( 0 ) ), "", System.Data.DataRowVersion.Current, null ) );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@GUID_ID", System.Data.SqlDbType.UniqueIdentifier, 4, System.Data.ParameterDirection.Output, false, ( ( System.Byte )( 0 ) ), ( ( System.Byte )( 0 ) ), "", System.Data.DataRowVersion.Current, null ) );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@BUDGETDEP_NAME", System.Data.DbType.String ) );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@BUDGETDEP_MANAGER", System.Data.SqlDbType.Int, 4 ) );
                if( this.ParentID.CompareTo( System.Guid.Empty ) != 0 )
                {
                    cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@PARENT_GUID_ID", System.Data.SqlDbType.UniqueIdentifier, 4 ) );
                    cmd.Parameters[ "@PARENT_GUID_ID" ].Value = this.m_uuidParentID; 
                }                
                cmd.Parameters[ "@BUDGETDEP_NAME" ].Value = this.m_strName;
                cmd.Parameters[ "@BUDGETDEP_MANAGER" ].Value = this.m_Manager.ulID;
                cmd.ExecuteNonQuery();
                System.Int32 iRet = ( System.Int32 )cmd.Parameters[ "@RETURN_VALUE" ].Value;
                if( iRet == 0 )
                {
                    this.m_uuidID = ( System.Guid )cmd.Parameters[ "@GUID_ID" ].Value;
                    bRet = true;
                }
                else
                {
                    if( iRet == 1 )
                    {
                        DevExpress.XtraEditors.XtraMessageBox.Show(  "Бюджетное подразделение '" + this.m_strName + "' уже есть в БД", "Внимание",
                            System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning );
                    }
                    else
                    {
                        DevExpress.XtraEditors.XtraMessageBox.Show(  "Ошибка создания бюджетного подразделения", "Ошибка",
                            System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
                    }
                }
                cmd.Dispose();
            }
            catch( System.Exception e )
            {
                DevExpress.XtraEditors.XtraMessageBox.Show( 
                "Ошибка создания бюджетного подразделения.\n" + e.Message, "Внимание",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
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
        public override System.Boolean Update( UniXP.Common.CProfile objProfile )
        {
            System.Boolean bRet = false;

            // уникальный идентификатор не должен быть пустым
            if( this.m_uuidID == System.Guid.Empty )
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(  "Недопустимое значение уникального идентификатора объекта", "Внимание",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
                return bRet;
            }
            // наименование не должен быть пустым
            if( this.m_strName == "" )
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(  "Недопустимое наименование бюджетного подразделения", "Внимание",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning );
                return bRet;
            }
            // необходимо указать распорядителя бюджета
            if( this.m_Manager == null )
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(  "Укажите распорядителя бюджетного подразделения", "Внимание",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning );
                return bRet;
            }

            System.Data.SqlClient.SqlConnection DBConnection = objProfile.GetDBSource();
            if( DBConnection == null ) { return bRet; }

            try
            {
                // соединение с БД получено, прописываем команду на создание записи в БД
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
                cmd.Connection = DBConnection;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = System.String.Format( "[{0}].[dbo].[sp_EditBudgetDep]", objProfile.GetOptionsDllDBName() );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ( ( System.Byte )( 0 ) ), ( ( System.Byte )( 0 ) ), "", System.Data.DataRowVersion.Current, null ) );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@GUID_ID", System.Data.SqlDbType.UniqueIdentifier, 4 ) );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@BUDGETDEP_NAME", System.Data.DbType.String ) );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@BUDGETDEP_MANAGER", System.Data.SqlDbType.Int, 4 ) );
                cmd.Parameters[ "@GUID_ID" ].Value = this.m_uuidID;
                if( this.m_uuidParentID.CompareTo( System.Guid.Empty ) != 0 )
                {
                    cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@PARENT_GUID_ID", System.Data.SqlDbType.UniqueIdentifier, 4 ) );
                    cmd.Parameters[ "@PARENT_GUID_ID" ].Value = this.m_uuidParentID; 
                }
                cmd.Parameters[ "@BUDGETDEP_NAME" ].Value = this.m_strName;
                cmd.Parameters[ "@BUDGETDEP_MANAGER" ].Value = this.m_Manager.ulID;
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
                        DevExpress.XtraEditors.XtraMessageBox.Show(  "Бюджетное подразделение '" + this.m_strName + "' уже есть в БД", "Внимание",
                            System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning );
                        break;
                        case 2:
                        DevExpress.XtraEditors.XtraMessageBox.Show(  "Запись с указанным идентификатором не найдена \n" + this.m_uuidID.ToString(), "Внимание",
                            System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
                        break;
                        default:
                        DevExpress.XtraEditors.XtraMessageBox.Show(  "Ошибка изменения свойств бюджетного подразделения", "Внимание",
                            System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
                        break;
                    }
                }
                cmd.Dispose();
            }
            catch( System.Exception e )
            {
                DevExpress.XtraEditors.XtraMessageBox.Show( 
                "Ошибка изменения свойств бюджетного подразделения.\n" + e.Message, "Внимание",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
            }
			finally // очищаем занимаемые ресурсы
            {
                DBConnection.Close();
            }
            return bRet;
        }
        /// <summary>
        /// Сохраняет информацию о составе бюджетного подразделения в БД
        /// </summary>
        /// <param name="objProfile">профайл</param>
        /// <returns>true - успешное завершение; false - ошибка</returns>
        public System.Boolean SaveBudgetDepDeclaration( UniXP.Common.CProfile objProfile )
        {
            System.Boolean bRet = false;

            if( this.UsesrList.GetCountItems() == 0 ) { return true; }

            System.Data.SqlClient.SqlConnection DBConnection = objProfile.GetDBSource();
            if( DBConnection == null ) { return bRet; }

            try
            {
                // сперва удаляем связь пользователей с заданным бюджетным подразделением
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
                cmd.Connection = DBConnection;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = System.String.Format( "[{0}].[dbo].[sp_DeleteBudgetDepDeclaration]", objProfile.GetOptionsDllDBName() );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ( ( System.Byte )( 0 ) ), ( ( System.Byte )( 0 ) ), "", System.Data.DataRowVersion.Current, null ) );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@BUDGETDEP_GUID_ID", System.Data.SqlDbType.UniqueIdentifier, 4 ) );
                cmd.Parameters[ "@BUDGETDEP_GUID_ID" ].Value = this.m_uuidID;
                cmd.ExecuteNonQuery();
                System.Int32 iRet = ( System.Int32 )cmd.Parameters[ "@RETURN_VALUE" ].Value;
                if( iRet == 0 )
                {
                    cmd.Parameters.Clear();
                    cmd.CommandText = System.String.Format( "[{0}].[dbo].[sp_AssignUserBudgetDep]", objProfile.GetOptionsDllDBName() );
                    cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@BUDGETDEP_GUID_ID", System.Data.SqlDbType.UniqueIdentifier, 4 ) );
                    cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@ulUserID", System.Data.SqlDbType.Int, 4 ) );
                    cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@bAssign", System.Data.SqlDbType.Bit, 1 ) );
                    cmd.Parameters[ "@BUDGETDEP_GUID_ID" ].Value = this.m_uuidID;
                    cmd.Parameters[ "@bAssign" ].Value = 1;
                    CUser objUser = null;
                    for( System.Int32 i = 0; i < this.UsesrList.GetCountItems(); i++ )
                    {
                        objUser = this.UsesrList.GetByIndex( i );
                        if( objUser == null ) { continue; }
                        try
                        {
                            cmd.Parameters[ "@ulUserID" ].Value = objUser.ulID;
                            cmd.ExecuteNonQuery();
                        }
                        catch( System.Exception e )
                        {
                            DevExpress.XtraEditors.XtraMessageBox.Show( 
                            "Ошибка добавления пользователя в бюджетное подразделение.\nПользователь: " + 
                            objUser.UserLastName + " " + objUser.UserFirstName + e.Message, "Внимание",
                            System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning );
                            continue;
                        }
                    }
                    bRet = true;
                }
                else
                {
                    switch( iRet )
                    {
                        case 1:
                        DevExpress.XtraEditors.XtraMessageBox.Show(  "Запись с указанным идентификатором не найдена \n" + this.m_uuidID.ToString(), "Внимание",
                            System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
                        break;
                        default:
                        DevExpress.XtraEditors.XtraMessageBox.Show(  "Ошибка удаления состава бюджетного подразделения", "Внимание",
                            System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
                        break;
                    }
                }
                cmd.Dispose();
            }
            catch( System.Exception e )
            {
                DevExpress.XtraEditors.XtraMessageBox.Show( 
                "Ошибка сохранения состава бюджетного подразделения.\n" + e.Message, "Внимание",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
            }
			finally // очищаем занимаемые ресурсы
            {
                DBConnection.Close();
            }
            return bRet;
        }
        /// <summary>
        /// Проверяет свойства объекта на предмет ошибок
        /// </summary>
        /// <returns></returns>
        private System.Boolean bIsValuesValidForSaveToDB()
        {
            System.Boolean bRet = false;
            try
            {
                if( this.m_uuidID == System.Guid.Empty )
                {
                    DevExpress.XtraEditors.XtraMessageBox.Show(  "Недопустимое значение уникального идентификатора объекта", "Внимание",
                        System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
                    return bRet;
                }
                if( ( this.m_State == System.Data.DataRowState.Added ) || 
                    ( this.m_State == System.Data.DataRowState.Modified ) )
                {
                    // наименование не должен быть пустым
                    if( this.Name == "" )
                    {
                        DevExpress.XtraEditors.XtraMessageBox.Show(  "Недопустимое наименование бюджетного подразделения", "Внимание",
                            System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
                        return bRet;
                    }
                    if( this.Manager == null )
                    {
                        DevExpress.XtraEditors.XtraMessageBox.Show(  "Руководитель подразделения должен быть указан", "Внимание",
                            System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
                        return bRet;
                    }
                }
                bRet = true;
            }
            catch( System.Exception e )
            {
                DevExpress.XtraEditors.XtraMessageBox.Show( 
                "Ошибка проверки значений объекта 'Бюджетное подразделение'\nперед сохранением в БД" + e.Message, "Внимание",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
            }
            return bRet;
        }

        /// <summary>
        /// Сохраняет в БД список бюджетных подразделений
        /// </summary>
        /// <param name="BudgetDepList">список бюджетных подразделений</param>
        /// <param name="objProfile">профайл</param>
        /// <returns>true - успешное завершение; false - ошибка</returns>
        public static System.Boolean SaveBudgetDepListToDB( CBaseList<CBudgetDep> BudgetDepList,
            UniXP.Common.CProfile objProfile )
        {
            System.Boolean bRes = false;
            // проверим, а не пустой ли список
            if( BudgetDepList.GetCountItems() == 0 ) { return bRes; }
            // соединение с БД
            System.Data.SqlClient.SqlConnection DBConnection = objProfile.GetDBSource();
            if( DBConnection == null ) { return bRes; }
            System.Data.SqlClient.SqlTransaction DBTransaction = null;
            try
            {
                // соединение с БД получено, попробуем сохранить в базе данных информацию 
                // об изменениях в списке статей расходов
                System.Int32 iObjectsCount = BudgetDepList.GetCountItems();
                CBudgetDep objBudgetDep = null;
                // запускаем транзакцию
                DBTransaction = DBConnection.BeginTransaction();
                // SQL-команда 
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
                cmd.Connection = DBConnection;
                cmd.Transaction = DBTransaction;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                System.Int32 iRet = -1;
                for( System.Int32 i = 0; i< iObjectsCount; i++ )
                {
                    objBudgetDep = BudgetDepList.GetByIndex( i );
                    if( objBudgetDep == null ) { continue; }
                    // проверяем объект на предмет внесенных значений
                    if( objBudgetDep.bIsValuesValidForSaveToDB() == false )
                    {
                        DBTransaction.Rollback();
                        break;
                    }
                    // подготавливаем параметры SQL-команды
                    cmd.Parameters.Clear();
                    iRet = -1;
                    switch( objBudgetDep.State )
                    {
                        case System.Data.DataRowState.Added:
                        {
                            // новое подразделение
                            cmd.CommandText = System.String.Format( "[{0}].[dbo].[sp_AddBudgetDep]", objProfile.GetOptionsDllDBName() );
                            cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@RETURN_VALUE", System.Data.SqlDbType.Int, 8, System.Data.ParameterDirection.ReturnValue, false, ( ( System.Byte )( 0 ) ), ( ( System.Byte )( 0 ) ), "", System.Data.DataRowVersion.Current, null ) );
                            cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@GUID_ID", System.Data.SqlDbType.UniqueIdentifier, 16, System.Data.ParameterDirection.Output, false, ( ( System.Byte )( 0 ) ), ( ( System.Byte )( 0 ) ), "", System.Data.DataRowVersion.Current, null ) );
                            cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@BUDGETDEP_GUID_ID", System.Data.SqlDbType.UniqueIdentifier, 16 ) );
                            cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@BUDGETDEP_NAME", System.Data.DbType.String ) );
                            cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@BUDGETDEP_MANAGER", System.Data.SqlDbType.Int, 8 ) );
                            cmd.Parameters[ "@BUDGETDEP_GUID_ID" ].Value = objBudgetDep.uuidID;
                            if( objBudgetDep.ParentID.CompareTo( System.Guid.Empty ) != 0 )
                            {
                                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@PARENT_GUID_ID", System.Data.SqlDbType.UniqueIdentifier, 16 ) );
                                cmd.Parameters[ "@PARENT_GUID_ID" ].Value = objBudgetDep.ParentID;
                            }
                            cmd.Parameters[ "@BUDGETDEP_NAME" ].Value = objBudgetDep.Name;
                            cmd.Parameters[ "@BUDGETDEP_MANAGER" ].Value = objBudgetDep.Manager.ulID;
                            cmd.ExecuteNonQuery();
                            iRet = ( System.Int32 )cmd.Parameters[ "@RETURN_VALUE" ].Value;
                            if( iRet != 0 )
                            {
                                DBTransaction.Rollback();
                                switch( iRet )
                                {
                                    case 1:
                                        {
                                            DevExpress.XtraEditors.XtraMessageBox.Show( 
                                            "Бюджетное подразделение с заданным именем существует\n" + objBudgetDep.Name + 
                                            "\nОтмена внесенных изменений.", "Внимание",
                                            System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
                                            break;
                                        }
                                    default:
                                        {
                                            DevExpress.XtraEditors.XtraMessageBox.Show( 
                                            "Ошибка добавления в БД нового бюджетного подразделения\n" + objBudgetDep.Name + 
                                            "\nОтмена внесенных изменений.", "Внимание",
                                            System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
                                            break;
                                        }
                                }
                            }
                            break;
                        }

                        case System.Data.DataRowState.Modified:
                        {
                            // изменение свойств подразделения
                            cmd.CommandText = System.String.Format( "[{0}].[dbo].[sp_EditBudgetDep]", objProfile.GetOptionsDllDBName() );
                            cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ( ( System.Byte )( 0 ) ), ( ( System.Byte )( 0 ) ), "", System.Data.DataRowVersion.Current, null ) );
                            cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@GUID_ID", System.Data.SqlDbType.UniqueIdentifier, 16 ) );
                            cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@BUDGETDEP_NAME", System.Data.DbType.String ) );
                            cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@BUDGETDEP_MANAGER", System.Data.SqlDbType.Int, 8 ) );
                            cmd.Parameters[ "@GUID_ID" ].Value = objBudgetDep.uuidID;
                            if( objBudgetDep.ParentID.CompareTo( System.Guid.Empty ) != 0 )
                            {
                                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@PARENT_GUID_ID", System.Data.SqlDbType.UniqueIdentifier, 16 ) );
                                cmd.Parameters[ "@PARENT_GUID_ID" ].Value = objBudgetDep.ParentID; 
                            }
                            cmd.Parameters[ "@BUDGETDEP_NAME" ].Value = objBudgetDep.Name;
                            cmd.Parameters[ "@BUDGETDEP_MANAGER" ].Value = objBudgetDep.Manager.ulID;
                            cmd.ExecuteNonQuery();
                            iRet = ( System.Int32 )cmd.Parameters[ "@RETURN_VALUE" ].Value;
                            if( iRet != 0 )
                            {
                                DBTransaction.Rollback();
                                switch( iRet )
                                {
                                    case 1:
                                        {
                                            DevExpress.XtraEditors.XtraMessageBox.Show( 
                                            "Бюджетное подразделение с заданным именем существует\n" + objBudgetDep.Name + 
                                            "\nОтмена внесенных изменений.", "Внимание",
                                            System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
                                            break;
                                        }
                                    case 2:
                                        {
                                            DevExpress.XtraEditors.XtraMessageBox.Show( 
                                            "Бюджетное подразделение с заданным идентификатором не найдено\n" + objBudgetDep.Name + "\n" + objBudgetDep.uuidID.ToString() + 
                                            "\nОтмена внесенных изменений.", "Внимание",
                                            System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
                                            break;
                                        }
                                    default:
                                        {
                                            DevExpress.XtraEditors.XtraMessageBox.Show( 
                                            "Ошибка изменения свойств бюджетного подразделения\n" + objBudgetDep.Name + 
                                            "\nОтмена внесенных изменений.", "Внимание",
                                            System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
                                            break;
                                        }
                                }
                            }

                            break;
                        } // case System.Data.DataRowState.Modified:

                        case System.Data.DataRowState.Deleted:
                        {
                            // удаление бюджетного подразделения из БД
                            cmd.CommandText = System.String.Format( "[{0}].[dbo].[sp_DeleteBudgetDepOne]", objProfile.GetOptionsDllDBName() );
                            cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@RETURN_VALUE", System.Data.SqlDbType.Int, 8, System.Data.ParameterDirection.ReturnValue, false, ( ( System.Byte )( 0 ) ), ( ( System.Byte )( 0 ) ), "", System.Data.DataRowVersion.Current, null ) );
                            cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@GUID_ID", System.Data.SqlDbType.UniqueIdentifier ) );
                            cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@ERROR_NUMBER", System.Data.SqlDbType.Int, 8, System.Data.ParameterDirection.Output, false, ( ( System.Byte )( 0 ) ), ( ( System.Byte )( 0 ) ), "", System.Data.DataRowVersion.Current, null ) );
                            cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@ERROR_MESSAGE", System.Data.DbType.String ) );
                            cmd.Parameters[ "@ERROR_MESSAGE" ].Direction = System.Data.ParameterDirection.Output;

                            cmd.Parameters[ "@GUID_ID" ].Value = objBudgetDep.uuidID;
                            cmd.ExecuteNonQuery();
                            iRet = ( System.Int32 )cmd.Parameters[ "@RETURN_VALUE" ].Value;
                            if( iRet == 3 )
                            {
                                //данная статья еще не была сохранена в БД
                                iRet = 0;
                            }
                            if( iRet != 0 )
                            {
                                DBTransaction.Rollback();
                                switch( iRet )
                                {
                                    case 1:
                                        {
                                            DevExpress.XtraEditors.XtraMessageBox.Show( 
                                            "Бюджетное подразделение связано с бюджетом или бюджетным документом\n" + objBudgetDep.Name + 
                                            "\nОтмена удаления.", "Внимание",
                                            System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
                                            break;
                                        }
                                    case 4:
                                        {
                                            DevExpress.XtraEditors.XtraMessageBox.Show( 
                                            "Бюджетное подразделение содержит дочерние бюджетные подразделения\n" + objBudgetDep.Name + "\n" + objBudgetDep.uuidID.ToString() + 
                                            "\nОтмена удаления.", "Внимание",
                                            System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
                                            break;
                                        }
                                    default:
                                        {
                                            DevExpress.XtraEditors.XtraMessageBox.Show( 
                                            "Ошибка удаления бюджетного подразделения\n" + objBudgetDep.Name + 
                                            "\nОтмена удаления.", "Внимание",
                                            System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
                                            break;
                                        }
                                }
                            } // if( iRet != 0 )
                            break;
                            } // case System.Data.DataRowState.Deleted:

                        } //switch( objBudgetDep.State )
                    //}
                    if( iRet != 0 ) { break; }
                    else
                    {
                        // попробуем сохранить список сотрудников для данного бюджетного подразделения
                        if( ( objBudgetDep.State == System.Data.DataRowState.Added ) || 
                            ( objBudgetDep.State == System.Data.DataRowState.Modified ) )
                        {
                            // сперва удаляем связь статьи расходов с бюджетными подразделениями
                            cmd.Parameters.Clear();
                            cmd.CommandText = System.String.Format( "[{0}].[dbo].[sp_DeleteBudgetDepDeclaration]", objProfile.GetOptionsDllDBName() );
                            cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@RETURN_VALUE", System.Data.SqlDbType.Int, 8, System.Data.ParameterDirection.ReturnValue, false, ( ( System.Byte )( 0 ) ), ( ( System.Byte )( 0 ) ), "", System.Data.DataRowVersion.Current, null ) );
                            cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@BUDGETDEP_GUID_ID", System.Data.SqlDbType.UniqueIdentifier ) );
                            cmd.Parameters[ "@BUDGETDEP_GUID_ID" ].Value = objBudgetDep.uuidID;
                            cmd.ExecuteNonQuery();
                            iRet = ( System.Int32 )cmd.Parameters[ "@RETURN_VALUE" ].Value;
                            if( iRet != 0 )
                            {
                                DBTransaction.Rollback();
                                switch( iRet )
                                {
                                    case 1:
                                        {
                                            DevExpress.XtraEditors.XtraMessageBox.Show( 
                                            "Ошибка удаления списка сотрудников у бюджетного подразделения\nБюджетное подразделение с заданным идентификатором не найдено\n" + objBudgetDep.Name + " " + objBudgetDep.uuidID.ToString() + 
                                            "\nОтмена внесенных изменений.", "Внимание",
                                            System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
                                            break;
                                        }
                                    default:
                                        {
                                            DevExpress.XtraEditors.XtraMessageBox.Show( 
                                            "Ошибка удаления списка сотрудников у бюджетного подразделения\n" + objBudgetDep.Name + 
                                            "\nОтмена внесенных изменений.", "Внимание",
                                            System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
                                            break;
                                        }
                                }
                            }
                            else
                            {
                                if( objBudgetDep.UsesrList.GetCountItems() > 0 )
                                {
                                    // привязываем список сотрудников
                                    cmd.Parameters.Clear();
                                    cmd.CommandText = System.String.Format( "[{0}].[dbo].[sp_AssignUserBudgetDep]", objProfile.GetOptionsDllDBName() );
                                    cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@RETURN_VALUE", System.Data.SqlDbType.Int, 8, System.Data.ParameterDirection.ReturnValue, false, ( ( System.Byte )( 0 ) ), ( ( System.Byte )( 0 ) ), "", System.Data.DataRowVersion.Current, null ) );
                                    cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@ulUserID", System.Data.SqlDbType.Int ) );
                                    cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@BUDGETDEP_GUID_ID", System.Data.SqlDbType.UniqueIdentifier ) );
                                    cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@bAssign", System.Data.SqlDbType.Bit ) );
                                    cmd.Parameters[ "@BUDGETDEP_GUID_ID" ].Value = objBudgetDep.uuidID;
                                    cmd.Parameters[ "@bAssign" ].Value = 1;
                                    CUser objUser = null;
                                    iRet = -1;
                                    for( System.Int32 i2 = 0; i2 < objBudgetDep.UsesrList.GetCountItems(); i2++ )
                                    {
                                        objUser = objBudgetDep.UsesrList.GetByIndex( i2 );
                                        if( objUser == null ) { continue; }
                                        try
                                        {
                                            cmd.Parameters[ "@ulUserID" ].Value = objUser.ulID;
                                            cmd.ExecuteNonQuery();
                                            iRet = ( System.Int32 )cmd.Parameters[ "@RETURN_VALUE" ].Value;
                                            if( iRet != 0 )
                                            {
                                                DBTransaction.Rollback();
                                                switch( iRet )
                                                {
                                                    case 1:
                                                    DevExpress.XtraEditors.XtraMessageBox.Show(  "Бюджетное подразделение с заданным идентификатором не найдено.", "Внимание",
                                                        System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
                                                    break;
                                                    case 2:
                                                    DevExpress.XtraEditors.XtraMessageBox.Show(  "Сотрудник с заданным идентификатором не найден.", "Внимание",
                                                        System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
                                                    break;
                                                    default:
                                                    DevExpress.XtraEditors.XtraMessageBox.Show(  "Ошибка включения сотрудника в состав бюджетного подразделения.", "Внимание",
                                                        System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
                                                    break;
                                                }
                                                // выход из цикла
                                                break;
                                            }
                                        }
                                        catch( System.Exception e )
                                        {
                                            DBTransaction.Rollback();
                                            DevExpress.XtraEditors.XtraMessageBox.Show( 
                                            "Ошибка включения сотрудника в состав бюджетного подразделения.\nБюджетное подразделение: " + 
                                            objBudgetDep.Name + "\nСотрудник" + objUser.UserLastName + " " + objUser.UserFirstName + "\n" + e.Message, "Внимание",
                                            System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning );
                                            break;
                                        }
                                    } // for ...
                                    if( iRet != 0 ) { break; }
                                }

                            }

                        } // if
                    }
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
                "Не удалось сохранить изменения в БД.\n" + e.Message, "Внимание",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
            }
			finally // очищаем занимаемые ресурсы
            {
                DBConnection.Close();
            }
            return bRes;
        }
        #endregion

        #region Список дополнительных распорядителей подразделения
        /// <summary>
        /// Обновляет список дополнительных распорядителей подразделения
        /// </summary>
        /// <param name="objProfile">профайл</param>
        /// <param name="uuidBudgetDepID">уи подразделения</param>
        /// <param name="objTreeList">дерево со списком</param>
        public static void RefreshBudgetDepManagerList(UniXP.Common.CProfile objProfile, System.Guid uuidBudgetDepID,
            DevExpress.XtraTreeList.TreeList objTreeList)
        {
            System.Data.SqlClient.SqlConnection DBConnection = objProfile.GetDBSource();
            if (DBConnection == null) 
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(
                "Отсутствует соединение с базой данных.", "Внимание",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);

                return ; 
            }

            try
            {
                objTreeList.ClearNodes();

                // соединение с БД получено, прописываем команду на выборку данных
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
                cmd.Connection = DBConnection;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = System.String.Format("[{0}].[dbo].[sp_GetBudgetDepAdvancedManagerList]", objProfile.GetOptionsDllDBName());
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ((System.Byte)(0)), ((System.Byte)(0)), "", System.Data.DataRowVersion.Current, null));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@GUID_ID", System.Data.SqlDbType.UniqueIdentifier, 4));
                cmd.Parameters["@GUID_ID"].Value = uuidBudgetDepID;
                System.Data.SqlClient.SqlDataReader rs = cmd.ExecuteReader();
                if (rs.HasRows)
                {
                    // набор данных непустой
                    CUser objManager = null;
                    while (rs.Read())
                    {
                        objManager = new CUser((System.Int32)rs["ErpBudgetUserID"], (System.Int32)rs["UniXPUserID"],
                            (System.String)rs["strLastName"], (System.String)rs["strFirstName"]);

                        //добавляем узел в дерево
                        DevExpress.XtraTreeList.Nodes.TreeListNode objNode =
                            objTreeList.AppendNode(new object[] { System.Convert.ToBoolean( rs["IsManager"] ), objManager.UserFullName}, null);

                        objNode.Tag = objManager;
                    }
                }
                rs.Close();
                rs.Dispose();

                cmd.Dispose();
            }
            catch (System.Exception f)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(
                "Не удалось получить список дополнительных распорядителей подразделения.\n\nТекст ошибки: " + f.Message, "Внимание",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
            finally // очищаем занимаемые ресурсы
            {
                DBConnection.Close();
            }

            return ;
        }
        /// <summary>
        /// Сохраняет в БД список дополнительных распорядителей бюджета
        /// </summary>
        /// <param name="objProfile">профайл</param>
        /// <param name="uuidBudgetDepID">уи подразделения</param>
        /// <param name="objTreeList">дерево со списком</param>
        /// <returns>true - успешное завершение операции; false - ошибка</returns>
        public static System.Boolean SaveBudgetDepManagerList(UniXP.Common.CProfile objProfile, System.Guid uuidBudgetDepID,
            DevExpress.XtraTreeList.TreeList objTreeList)
        {
            System.Boolean bRet = false;
            // уникальный идентификатор не должен быть пустым
            if (uuidBudgetDepID == System.Guid.Empty)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show("Недопустимое значение уникального идентификатора подразделения.\n" +
                    uuidBudgetDepID.ToString(), "Внимание",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                return bRet;
            }

            System.Data.SqlClient.SqlConnection DBConnection = objProfile.GetDBSource();
            if (DBConnection == null) 
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(
                "Отсутствует соединение с базой данных.", "Внимание",
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
                cmd.CommandText = System.String.Format("[{0}].[dbo].[sp_DeleteBudgetDepAdvancedManagerList]", objProfile.GetOptionsDllDBName());
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ((System.Byte)(0)), ((System.Byte)(0)), "", System.Data.DataRowVersion.Current, null));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@GUID_ID", System.Data.SqlDbType.UniqueIdentifier));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ERROR_NUMBER", System.Data.SqlDbType.Int, 8, System.Data.ParameterDirection.Output, false, ((System.Byte)(0)), ((System.Byte)(0)), "", System.Data.DataRowVersion.Current, null));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ERROR_MESSAGE", System.Data.SqlDbType.NVarChar, 4000));
                cmd.Parameters["@ERROR_MESSAGE"].Direction = System.Data.ParameterDirection.Output;
                cmd.Parameters["@GUID_ID"].Value = uuidBudgetDepID;
                cmd.ExecuteNonQuery();
                System.Int32 iRet = (System.Int32)cmd.Parameters["@RETURN_VALUE"].Value;
                if (iRet == 0)
                {
                    if (objTreeList.Nodes.Count > 0)
                    {
                        cmd.CommandText = System.String.Format("[{0}].[dbo].[sp_AddBudgetDepAdvancedManager]", objProfile.GetOptionsDllDBName());
                        cmd.Parameters.Clear();
                        cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ((System.Byte)(0)), ((System.Byte)(0)), "", System.Data.DataRowVersion.Current, null));
                        cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@BUDGETDEP_GUID_ID", System.Data.SqlDbType.UniqueIdentifier));
                        cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@USER_ID", System.Data.SqlDbType.Int));

                        cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ERROR_NUMBER", System.Data.SqlDbType.Int, 8, System.Data.ParameterDirection.Output, false, ((System.Byte)(0)), ((System.Byte)(0)), "", System.Data.DataRowVersion.Current, null));
                        cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ERROR_MESSAGE", System.Data.SqlDbType.NVarChar, 4000));

                        cmd.Parameters["@ERROR_MESSAGE"].Direction = System.Data.ParameterDirection.Output;
                        cmd.Parameters["@BUDGETDEP_GUID_ID"].Value = uuidBudgetDepID;

                        foreach (DevExpress.XtraTreeList.Nodes.TreeListNode objNode in objTreeList.Nodes)
                        {
                            if ((System.Boolean)objNode.GetValue(objTreeList.Columns[0]) == true)
                            {
                                cmd.Parameters["@USER_ID"].Value = ( ( CUser )objNode.Tag ).ulID;
                                cmd.ExecuteNonQuery();
                                iRet = (System.Int32)cmd.Parameters["@RETURN_VALUE"].Value;
                                if (iRet != 0)
                                {
                                    // откатываем транзакцию
                                    DBTransaction.Rollback();
                                    break;
                                }
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
                    DevExpress.XtraEditors.XtraMessageBox.Show("Ошибка сохранения списка дополнительных распорядителей.\n\nТекст ошибки: " + 
                        (System.String)cmd.Parameters["@ERROR_MESSAGE"].Value, "Внимание",
                        System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                }
                cmd.Dispose();
            }
            catch (System.Exception e)
            {
                // откатываем транзакцию
                DBTransaction.Rollback();
                DevExpress.XtraEditors.XtraMessageBox.Show(
                "Ошибка сохранения списка дополнительных распорядителей.\n\nТекст ошибки: " + e.Message, "Внимание",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
			finally // очищаем занимаемые ресурсы
            {
                DBConnection.Close();
            }
            return bRet;
        }

        /// <summary>
        /// Возвращает список дополнительных распорядителей и согласователей бюджетного подразделения
        /// </summary>
        /// <param name="objProfile">профайл</param>
        /// <param name="uuidBudgetDepID">уи бюджетного подразделения</param>
        /// <param name="strErr">текст ошибки</param>
        /// <returns>список объектов класса "CUser"</returns>
        public static List<CUser> GetBudgetDepAdvManagerList(UniXP.Common.CProfile objProfile, System.Guid uuidBudgetDepID,
            ref System.String strErr )
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
                    CommandText = System.String.Format("[{0}].[dbo].[sp_GetBudgetDepAdvancedManagerList]", objProfile.GetOptionsDllDBName()) 
                };
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ((System.Byte)(0)), ((System.Byte)(0)), "", System.Data.DataRowVersion.Current, null));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@GUID_ID", System.Data.SqlDbType.UniqueIdentifier, 4));
                cmd.Parameters["@GUID_ID"].Value = uuidBudgetDepID;
                System.Data.SqlClient.SqlDataReader rs = cmd.ExecuteReader();
                if (rs.HasRows)
                {
                    // набор данных непустой
                    CUser objManager = null;
                    while (rs.Read())
                    {
                        objManager = new CUser(System.Convert.ToInt32(rs["ErpBudgetUserID"]), System.Convert.ToInt32( rs["UniXPUserID"] ),
                            System.Convert.ToString( rs["strLastName"] ), System.Convert.ToString(rs["strFirstName"]) );

                        objManager.IsBudgetDepManager = System.Convert.ToBoolean(rs["IsManager"]);
                        objManager.IsBudgetDepCoordinator = System.Convert.ToBoolean(rs["IsCoordinator"]);
                        objManager.IsBudgetDepController = System.Convert.ToBoolean(rs["IsController"]);

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

                cmd.Dispose();
            }
            catch (System.Exception f)
            {
                strErr += ("\nНе удалось получить список дополнительных распорядителей подразделения.\n\nТекст ошибки: " + f.Message);
            }
            finally // очищаем занимаемые ресурсы
            {
                DBConnection.Close();
            }

            return objManagerList;
        }
        /// <summary>
        /// Сохраняет в базе данных список согласователей и дополнительных распорядителей
        /// для бюджетного подразделения
        /// </summary>
        /// <param name="objProfile">профайл</param>
        /// <param name="uuidBudgetDepID">УИ бюджетного подразделения</param>
        /// <param name="objManagerList">список пользователей</param>
        /// <param name="strErr">текст ошибки</param>
        /// <returns>true - удачное завершение операции; false - ошибка</returns>
        public static System.Boolean SaveBudgetDepManagerList(UniXP.Common.CProfile objProfile, System.Guid uuidBudgetDepID,
            List<CUser> objManagerList, ref System.String strErr)
        {
            System.Boolean bRet = false;
            // уникальный идентификатор не должен быть пустым
            if (uuidBudgetDepID == System.Guid.Empty)
            {
                strErr += (String.Format("\nНедопустимое значение уникального идентификатора подразделения.\nУИ: {0}", uuidBudgetDepID));
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
                cmd.CommandText = System.String.Format("[{0}].[dbo].[sp_DeleteBudgetDepAdvancedManagerList]", objProfile.GetOptionsDllDBName());
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ((System.Byte)(0)), ((System.Byte)(0)), "", System.Data.DataRowVersion.Current, null));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@GUID_ID", System.Data.SqlDbType.UniqueIdentifier));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ERROR_NUMBER", System.Data.SqlDbType.Int, 8, System.Data.ParameterDirection.Output, false, ((System.Byte)(0)), ((System.Byte)(0)), "", System.Data.DataRowVersion.Current, null));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ERROR_MESSAGE", System.Data.SqlDbType.NVarChar, 4000));
                cmd.Parameters["@ERROR_MESSAGE"].Direction = System.Data.ParameterDirection.Output;
                cmd.Parameters["@GUID_ID"].Value = uuidBudgetDepID;
                cmd.ExecuteNonQuery();
                System.Int32 iRet = (System.Int32)cmd.Parameters["@RETURN_VALUE"].Value;
                if (iRet == 0)
                {
                    if( ( objManagerList != null ) && (objManagerList.Count > 0) )
                    {
                        cmd.CommandText = System.String.Format("[{0}].[dbo].[sp_AddBudgetDepAdvancedManager]", objProfile.GetOptionsDllDBName());
                        cmd.Parameters.Clear();
                        cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ((System.Byte)(0)), ((System.Byte)(0)), "", System.Data.DataRowVersion.Current, null));
                        cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@BUDGETDEP_GUID_ID", System.Data.SqlDbType.UniqueIdentifier));
                        cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@USER_ID", System.Data.SqlDbType.Int));
                        cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Rights_ID", System.Data.SqlDbType.Int));

                        cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ERROR_NUMBER", System.Data.SqlDbType.Int, 8, System.Data.ParameterDirection.Output, false, ((System.Byte)(0)), ((System.Byte)(0)), "", System.Data.DataRowVersion.Current, null));
                        cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ERROR_MESSAGE", System.Data.SqlDbType.NVarChar, 4000));

                        cmd.Parameters["@ERROR_MESSAGE"].Direction = System.Data.ParameterDirection.Output;
                        cmd.Parameters["@BUDGETDEP_GUID_ID"].Value = uuidBudgetDepID;

                        foreach (CUser objUser in objManagerList)
                        {
                            cmd.Parameters["@USER_ID"].Value = objUser.ulID;
                            
                            // проверка, назначен ли пользователь дополнительным распорядителем
                            if (objUser.IsBudgetDepManager == true)
                            {
                                cmd.Parameters["@Rights_ID"].Value = objUser.DynamicRightsList.FindByName( ERP_Budget.Global.Consts.strDRManager ).ID;
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
                                }

                                if (iRet == 0)
                                {
                                    // проверка, назначен ли пользователь контролером
                                    if (objUser.IsBudgetDepController == true)
                                    {
                                        cmd.Parameters["@Rights_ID"].Value = objUser.DynamicRightsList.FindByName(ERP_Budget.Global.Consts.strDRInspector).ID;
                                        cmd.ExecuteNonQuery();
                                        iRet = (System.Int32)cmd.Parameters["@RETURN_VALUE"].Value;
                                    }
                                    if (iRet != 0)
                                    {
                                        // откатываем транзакцию
                                        DBTransaction.Rollback();
                                        strErr += (System.Convert.ToString(cmd.Parameters["@ERROR_MESSAGE"].Value));
                                        break;
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
                            else
                            {
                                // откатываем транзакцию
                                DBTransaction.Rollback();
                                strErr += ( System.Convert.ToString( cmd.Parameters["@ERROR_MESSAGE"].Value ) );
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
                    strErr += (String.Format("Ошибка сохранения списка дополнительных распорядителей.\n\n{0}", System.Convert.ToString(cmd.Parameters["@ERROR_MESSAGE"].Value)));
                }
                cmd.Dispose();
            }
            catch (System.Exception e)
            {
                // откатываем транзакцию
                DBTransaction.Rollback();
                strErr += (String.Format("Ошибка сохранения списка дополнительных распорядителей.\n\n{0}", e.Message) );
            }
			finally // очищаем занимаемые ресурсы
            {
                DBConnection.Close();
            }

            return bRet;
        }

        #endregion

        public override string ToString()
        {
            return Name;
        }

    }
}
