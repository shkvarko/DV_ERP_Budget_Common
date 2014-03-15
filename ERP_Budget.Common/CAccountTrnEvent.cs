using System;
using System.Collections.Generic;
using System.Text;

namespace ERP_Budget.Common
{
    /// <summary>
    /// Класс "Действие - Тип документа - Динамическое право"
    /// </summary>
    public class CAccountTrnEvent : IBaseListItem
    {
        #region Переменные, Свойства, Константы 
        /// <summary>
        /// Действие
        /// </summary>
        private CBudgetDocEvent m_objBudgetDocEvent;
        /// <summary>
        /// Действие
        /// </summary>
        public CBudgetDocEvent BudgetDocEvent
        {
            get { return m_objBudgetDocEvent; }
            set { m_objBudgetDocEvent = value; }
        }
        /// <summary>
        /// Тип бюджетного документа
        /// </summary>
        private CBudgetDocType m_objBudgetDocType;
        /// <summary>
        /// Тип бюджетного документа
        /// </summary>
        public CBudgetDocType BudgetDocType
        {
            get { return m_objBudgetDocType; }
            set { m_objBudgetDocType = value; }
        }
        /// <summary>
        /// Динамическое право
        /// </summary>
        private CDynamicRight m_objDynamicRight;
        /// <summary>
        /// Динамическое право
        /// </summary>
        public CDynamicRight DynamicRight
        {
            get { return m_objDynamicRight; }
            set { m_objDynamicRight = value; }
        }
        /// <summary>
        /// Список типов бюджетных проводок
        /// </summary>
        private List<CAccountTrnType> m_AccountTrnTypeList;
        /// <summary>
        /// Список типов бюджетных проводок
        /// </summary>
        public List<CAccountTrnType> AccountTrnTypeList
        {
            get { return m_AccountTrnTypeList; }
            set { m_AccountTrnTypeList = value; }
        }
        #endregion 

        #region Конструкторы 
        public CAccountTrnEvent()
        {
            this.m_uuidID = System.Guid.Empty;
            this.m_strName = "";
            this.m_objBudgetDocEvent = null;
            this.m_objBudgetDocType = null;
            this.m_objDynamicRight = null;
            this.m_AccountTrnTypeList = new List<CAccountTrnType>();
        }
        public CAccountTrnEvent( System.Guid uuidID, System.String strName, CBudgetDocEvent objBudgetDocEvent,
           CBudgetDocType objBudgetDocType, CDynamicRight objDynamicRight )
        {
            this.m_uuidID = uuidID;
            this.m_strName = strName;
            this.m_objBudgetDocEvent = objBudgetDocEvent;
            this.m_objBudgetDocType = objBudgetDocType;
            this.m_objDynamicRight = objDynamicRight;
            this.m_AccountTrnTypeList = new List<CAccountTrnType>();
        }
        #endregion 

        #region Список объектов "Действие - Тип документа - Динамическое право" 
        /// <summary>
        /// Возвращает список объектов "Действие - Тип документа - Динамическое право"
        /// </summary>
        /// <param name="objProfile">профайл</param>
        /// <returns>список объектов "Действие - Тип документа - Динамическое право"</returns>
        public static System.Collections.Generic.List<CAccountTrnEvent> GetAccountTrnEventList( UniXP.Common.CProfile objProfile )
        {
            System.Collections.Generic.List<CAccountTrnEvent> objList = new List<CAccountTrnEvent>();
            try
            {
                System.Data.SqlClient.SqlConnection DBConnection = objProfile.GetDBSource();
                if( DBConnection == null ) { return objList; }

                // соединение с БД получено, прописываем команду на выборку данных
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
                cmd.Connection = DBConnection;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = System.String.Format( "[{0}].[dbo].[sp_GetAccTrnEvent]", objProfile.GetOptionsDllDBName() );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ( ( System.Byte )( 0 ) ), ( ( System.Byte )( 0 ) ), "", System.Data.DataRowVersion.Current, null ) );
                System.Data.SqlClient.SqlDataReader rs = cmd.ExecuteReader();
                if( rs.HasRows )
                {
                    while( rs.Read() )
                    {
                        objList.Add( new CAccountTrnEvent( rs.GetGuid( 0 ), rs.GetString( 1 ), 
                            new CBudgetDocEvent( rs.GetGuid( 2 ), rs.GetString( 5 ) ), 
                            new CBudgetDocType( rs.GetGuid( 3 ), rs.GetString( 6 ) ), 
                            new CDynamicRight( rs.GetInt32( 4 ), rs.GetString( 7 ), rs.GetString( 8 ), " " ) ) );
                    }
                    rs.Close();
                    for( System.Int32 i = 0; i < objList.Count; i++ )
                    {
                        // заполняем список типов бюджетных проводок
                        if( objList[ i ].InitDeclaration( cmd, objProfile ) == false )
                            break;
                    }
                }
                rs.Close();
                rs.Dispose();
                cmd.Dispose();
                DBConnection.Close();
            }
            catch( System.Exception e )
            {
                DevExpress.XtraEditors.XtraMessageBox.Show( 
                "Не удалось получить список объектов 'Действие - Тип документа - Динамическое право'.\n\nТекст ошибки: " + e.Message, "Внимание",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
            }
            return objList;
        }
        #endregion

        #region Init 
        /// <summary>
        /// Загружает список бюджетных проводок
        /// </summary>
        /// <param name="cmd">SQL-команда</param>
        /// <param name="objProfile">профайл</param>
        /// <returns>true - успешное завершение операции; false - ошибка</returns>
        public System.Boolean InitDeclaration( System.Data.SqlClient.SqlCommand cmd, UniXP.Common.CProfile objProfile )
        {
            System.Boolean bRet = false;

            if( cmd == null ) { return bRet; }
            try
            {
                // соединение с БД получено, прописываем команду на выборку данных
                cmd.Parameters.Clear();
                cmd.CommandText = System.String.Format( "[{0}].[dbo].[sp_GetAccTrnEventDeclrn]", objProfile.GetOptionsDllDBName() );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ( ( System.Byte )( 0 ) ), ( ( System.Byte )( 0 ) ), "", System.Data.DataRowVersion.Current, null ) );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@ACCTRNEVENT_GUID_ID", System.Data.SqlDbType.UniqueIdentifier ) );
                cmd.Parameters[ "@ACCTRNEVENT_GUID_ID" ].Value = this.uuidID;
                System.Data.SqlClient.SqlDataReader rs = cmd.ExecuteReader();
                if( rs.HasRows )
                {
                    while( rs.Read() )
                    {
                        this.m_AccountTrnTypeList.Add( new CAccountTrnType( rs.GetGuid( 1 ), 
                            rs.GetString( 2 ), rs.GetInt32( 3 ), rs.GetString( 4 ) ) );
                    }
                    bRet = true;
                }
                else
                {
                    DevExpress.XtraEditors.XtraMessageBox.Show( 
                    "Не удалось получить список типов бюджетных проводок для " + this.Name + "\nВ БД не найдена информация.", "Внимание",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning );

                }
                rs.Dispose();
            }
            catch( System.Exception e )
            {
                DevExpress.XtraEditors.XtraMessageBox.Show( 
                "Не удалось получить список типов бюджетных проводок для " + this.Name + "\n\n" + e.Message, "Внимание",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
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
                cmd.CommandText = System.String.Format( "[{0}].[dbo].[sp_GetAccTrnEvent]", objProfile.GetOptionsDllDBName() );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ( ( System.Byte )( 0 ) ), ( ( System.Byte )( 0 ) ), "", System.Data.DataRowVersion.Current, null ) );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@GUID_ID", System.Data.SqlDbType.UniqueIdentifier ) );
                cmd.Parameters[ "@GUID_ID" ].Value = uuidID;
                System.Data.SqlClient.SqlDataReader rs = cmd.ExecuteReader();
                if( rs.HasRows )
                {
                    // набор данных непустой, в нем нас интересует одна запись
                    rs.Read();
                    this.m_uuidID = rs.GetGuid( 0 );
                    this.m_strName = rs.GetString( 1 );
                    this.m_objBudgetDocEvent = new CBudgetDocEvent( rs.GetGuid( 2 ), rs.GetString( 5 ) );
                    this.m_objBudgetDocType = new CBudgetDocType( rs.GetGuid( 32 ), rs.GetString( 6 ) );
                    this.m_objDynamicRight = new CDynamicRight( rs.GetInt32( 4 ), rs.GetString( 7 ), rs.GetString( 8 ), " " );
                    rs.Close();

                    // заполняем список типов бюджетных проводок
                    bRet = InitDeclaration( cmd, objProfile );
                }
                else
                {
                    DevExpress.XtraEditors.XtraMessageBox.Show( 
                    "Не удалось проинициализировать класс CAccountTrnEvent.\nВ БД не найдена информация.", "Внимание",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning );

                }
                cmd.Dispose();
                rs.Dispose();
            }
            catch( System.Exception e )
            {
                DevExpress.XtraEditors.XtraMessageBox.Show( 
                "Не удалось проинициализировать класс CAccountTrnEvent.\n\nТекст ошибки: " + e.Message, "Внимание",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
            }
            finally // очищаем занимаемые ресурсы
            {
                DBConnection.Close();
            }
            return bRet;
        }
        /// <summary>
        /// Возвращает объект класса CAccountTrnEvent для заданного типа документа, динамического права и действия
        /// </summary>
        /// <param name="objProfile">профайл</param>
        /// <param name="objBudgetDocEvent">действие</param>
        /// <param name="objBudgetDocType">тип бюджетного документа</param>
        /// <param name="objDynamicRight">динамическое право</param>
        /// <returns></returns>
        public static CAccountTrnEvent GetAccountTrnEvent( UniXP.Common.CProfile objProfile,
           CBudgetDocEvent objBudgetDocEvent, CBudgetDocType objBudgetDocType, CDynamicRight objDynamicRight )
        {
            CAccountTrnEvent objRet = null;

            System.Data.SqlClient.SqlConnection DBConnection = objProfile.GetDBSource();
            if( DBConnection == null ) { return objRet; }

            try
            {
                // соединение с БД получено, прописываем команду на выборку данных
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
                cmd.Connection = DBConnection;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = System.String.Format( "[{0}].[dbo].[sp_GetAccTrnEvent]", objProfile.GetOptionsDllDBName() );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ( ( System.Byte )( 0 ) ), ( ( System.Byte )( 0 ) ), "", System.Data.DataRowVersion.Current, null ) );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@BUDGETDOCEVENT_GUID_ID", System.Data.SqlDbType.UniqueIdentifier ) );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@BUDGETDOCTYPE_GUID_ID", System.Data.SqlDbType.UniqueIdentifier ) );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@RIGHT_ID", System.Data.SqlDbType.Int ) );
                cmd.Parameters[ "@BUDGETDOCEVENT_GUID_ID" ].Value = objBudgetDocEvent.uuidID;
                cmd.Parameters[ "@BUDGETDOCTYPE_GUID_ID" ].Value = objBudgetDocType.uuidID;
                cmd.Parameters[ "@RIGHT_ID" ].Value = objDynamicRight.ID;
                System.Data.SqlClient.SqlDataReader rs = cmd.ExecuteReader();
                if( rs.HasRows )
                {
                    // набор данных непустой, в нем нас интересует одна запись
                    rs.Read();
                    objRet = new CAccountTrnEvent( rs.GetGuid( 0 ), rs.GetString( 1 ),
                        new CBudgetDocEvent( objBudgetDocEvent.uuidID, objBudgetDocEvent.Name ),
                        new CBudgetDocType( objBudgetDocType.uuidID, objBudgetDocType.Name ),
                        new CDynamicRight( objDynamicRight.ID, objDynamicRight.Name, objDynamicRight.Role, objDynamicRight.Description ) );
                    rs.Close();

                    // заполняем список типов бюджетных проводок
                    if( objRet.InitDeclaration( cmd, objProfile ) == false ) { objRet = null; }
                }
                else
                {
                    //DevExpress.XtraEditors.XtraMessageBox.Show( 
                    //"Не удалось проинициализировать класс CAccountTrnEvent.\nВ БД не найдена информация.", "Внимание",
                    //System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning );

                }
                cmd.Dispose();
                rs.Dispose();
            }
            catch( System.Exception e )
            {
                DevExpress.XtraEditors.XtraMessageBox.Show( 
                "Не удалось проинициализировать класс CAccountTrnEvent.\n\nТекст ошибки: " + e.Message, "Внимание",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
            }
            finally // очищаем занимаемые ресурсы
            {
                DBConnection.Close();
            }
            return objRet;
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
                DevExpress.XtraEditors.XtraMessageBox.Show(  "Недопустимое значение уникального идентификатора объекта.\n" + this.uuidID.ToString(), "Внимание",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
                return bRet;
            }

            System.Data.SqlClient.SqlConnection DBConnection = objProfile.GetDBSource();
            if( DBConnection == null ) { return bRet; }
            System.Data.SqlClient.SqlTransaction DBTransaction = DBConnection.BeginTransaction();
            try
            {
                // соединение с БД получено, прописываем команду на удаление данных
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
                cmd.Connection = DBConnection;
                cmd.Transaction = DBTransaction;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = System.String.Format( "[{0}].[dbo].[sp_DeleteAccTrnEvent]", objProfile.GetOptionsDllDBName() );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ( ( System.Byte )( 0 ) ), ( ( System.Byte )( 0 ) ), "", System.Data.DataRowVersion.Current, null ) );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@GUID_ID", System.Data.SqlDbType.UniqueIdentifier ) );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@ERROR_NUMBER", System.Data.SqlDbType.Int, 8, System.Data.ParameterDirection.Output, false, ( ( System.Byte )( 0 ) ), ( ( System.Byte )( 0 ) ), "", System.Data.DataRowVersion.Current, null ) );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@ERROR_MESSAGE", System.Data.DbType.String ) );
                cmd.Parameters[ "@ERROR_MESSAGE" ].Direction = System.Data.ParameterDirection.Output;
                cmd.Parameters[ "@GUID_ID" ].Value = this.m_uuidID;
                cmd.ExecuteNonQuery();
                System.Int32 iRet = ( System.Int32 )cmd.Parameters[ "@RETURN_VALUE" ].Value;
                if( iRet == 0 )
                {
                    // подтверждаем транзакцию
                    DBTransaction.Commit();
                    bRet = true;
                }
                else
                {
                    // откатываем транзакцию
                    DBTransaction.Rollback();
                    switch( iRet )
                    {
                        case 2:
                        {
                            DevExpress.XtraEditors.XtraMessageBox.Show(  "Ошибка выполнения запроса на удаление " + this.m_strName + 
                                "\nОшибка : " + ( System.String )cmd.Parameters[ "@ERROR_MESSAGE" ].Value, "Внимание",
                                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
                            break;
                        }
                        default:
                        {
                            DevExpress.XtraEditors.XtraMessageBox.Show(  "Ошибка выполнения запроса на удаление " + this.m_strName + 
                                "\nОшибка : " + ( System.String )cmd.Parameters[ "@ERROR_MESSAGE" ].Value, "Внимание",
                                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
                            break;
                        }
                    }
                }
                cmd.Dispose();
            }
            catch( System.Exception e )
            {
                // откатываем транзакцию
                DBTransaction.Rollback();
                DevExpress.XtraEditors.XtraMessageBox.Show( 
                "Ошибка выполнения запроса на удаление " + this.m_strName + "\n\nТекст ошибки: " + e.Message, "Внимание",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
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
        private System.Boolean Remove( UniXP.Common.CProfile objProfile, System.Data.SqlClient.SqlCommand cmd )
        {
            System.Boolean bRet = false;
            // уникальный идентификатор не должен быть пустым
            if( this.m_uuidID == System.Guid.Empty )
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(  "Недопустимое значение уникального идентификатора объекта.\n" + this.uuidID.ToString(), "Внимание",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
                return bRet;
            }

            if( cmd == null ) { return bRet; }
            try
            {
                // соединение с БД получено, прописываем команду на удаление данных
                cmd.CommandText = System.String.Format( "[{0}].[dbo].[sp_DeleteAccTrnEvent]", objProfile.GetOptionsDllDBName() );
                cmd.Parameters.Clear();
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ( ( System.Byte )( 0 ) ), ( ( System.Byte )( 0 ) ), "", System.Data.DataRowVersion.Current, null ) );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@GUID_ID", System.Data.SqlDbType.UniqueIdentifier ) );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@ERROR_NUMBER", System.Data.SqlDbType.Int, 8, System.Data.ParameterDirection.Output, false, ( ( System.Byte )( 0 ) ), ( ( System.Byte )( 0 ) ), "", System.Data.DataRowVersion.Current, null ) );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@ERROR_MESSAGE", System.Data.DbType.String ) );
                cmd.Parameters[ "@ERROR_MESSAGE" ].Direction = System.Data.ParameterDirection.Output;
                cmd.Parameters[ "@GUID_ID" ].Value = this.m_uuidID;
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
                        case 2:
                        {
                            DevExpress.XtraEditors.XtraMessageBox.Show(  "Ошибка выполнения запроса на удаление " + this.m_strName + 
                                "\nОшибка : " + ( System.String )cmd.Parameters[ "@ERROR_MESSAGE" ].Value, "Внимание",
                                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
                            break;
                        }
                        default:
                        {
                            DevExpress.XtraEditors.XtraMessageBox.Show(  "Ошибка выполнения запроса на удаление " + this.m_strName + 
                                "\nОшибка : " + ( System.String )cmd.Parameters[ "@ERROR_MESSAGE" ].Value, "Внимание",
                                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
                            break;
                        }
                    }
                }
            }
            catch( System.Exception e )
            {
                DevExpress.XtraEditors.XtraMessageBox.Show( 
                "Ошибка выполнения запроса на удаление " + this.m_strName + "\n\nТекст ошибки: " + e.Message, "Внимание",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
            }
			finally // очищаем занимаемые ресурсы
            {
            }
            return bRet;
        }
        /// <summary>
        /// Удаляет из БД все объекты "Действие - Тип документа - Динамическое право"
        /// </summary>
        /// <param name="objProfile">профайл</param>
        /// <returns>true - удачное завершение; false - ошибка</returns>
        public static System.Boolean RemoveAll( UniXP.Common.CProfile objProfile, 
            System.Data.SqlClient.SqlCommand cmd )
        {
            System.Boolean bRet = false;

            if( cmd == null ) { return bRet; }
            try
            {
                cmd.Parameters.Clear();
                cmd.CommandText = System.String.Format( "[{0}].[dbo].[sp_DeleteAccTrnEvent]", objProfile.GetOptionsDllDBName() );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ( ( System.Byte )( 0 ) ), ( ( System.Byte )( 0 ) ), "", System.Data.DataRowVersion.Current, null ) );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@ERROR_NUMBER", System.Data.SqlDbType.Int, 8, System.Data.ParameterDirection.Output, false, ( ( System.Byte )( 0 ) ), ( ( System.Byte )( 0 ) ), "", System.Data.DataRowVersion.Current, null ) );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@ERROR_MESSAGE", System.Data.DbType.String ) );
                cmd.Parameters[ "@ERROR_MESSAGE" ].Direction = System.Data.ParameterDirection.Output;
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
                        case 2:
                        {
                            DevExpress.XtraEditors.XtraMessageBox.Show(  "Ошибка выполнения запроса на удаление " + 
                                "\nОшибка : " + ( System.String )cmd.Parameters[ "@ERROR_MESSAGE" ].Value, "Внимание",
                                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
                            break;
                        }
                        default:
                        {
                            DevExpress.XtraEditors.XtraMessageBox.Show(  "Ошибка выполнения запроса на удаление " + 
                                "\nОшибка : " + ( System.String )cmd.Parameters[ "@ERROR_MESSAGE" ].Value, "Внимание",
                                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
                            break;
                        }
                    }
                }
            }
            catch( System.Exception e )
            {
                DevExpress.XtraEditors.XtraMessageBox.Show( 
                "Ошибка выполнения запроса на удаление всех объектов.\n\nТекст ошибки: " + e.Message, "Внимание",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
            }
			finally // очищаем занимаемые ресурсы
            {
            }
            return bRet;
        }
        /// <summary>
        /// Удалить список типов бюджетных проводок из БД
        /// </summary>
        /// <param name="objProfile">профайл</param>
        /// <param name="cmd">SQL-команда</param>
        /// <returns>true - удачное завершение; false - ошибка</returns>
        private System.Boolean RemoveDeclaration( UniXP.Common.CProfile objProfile, System.Data.SqlClient.SqlCommand cmd )
        {
            System.Boolean bRet = false;
            // уникальный идентификатор не должен быть пустым
            if( this.m_uuidID == System.Guid.Empty )
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(  "Недопустимое значение уникального идентификатора объекта.\n" + this.uuidID.ToString(), "Внимание",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
                return bRet;
            }

            if( cmd == null ) { return bRet; }
            try
            {
                // соединение с БД получено, прописываем команду на удаление данных
                cmd.CommandText = System.String.Format( "[{0}].[dbo].[sp_DeleteAccTrnEventDeclrn]", objProfile.GetOptionsDllDBName() );
                cmd.Parameters.Clear();
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ( ( System.Byte )( 0 ) ), ( ( System.Byte )( 0 ) ), "", System.Data.DataRowVersion.Current, null ) );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@GUID_ID", System.Data.SqlDbType.UniqueIdentifier ) );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@ERROR_NUMBER", System.Data.SqlDbType.Int, 8, System.Data.ParameterDirection.Output, false, ( ( System.Byte )( 0 ) ), ( ( System.Byte )( 0 ) ), "", System.Data.DataRowVersion.Current, null ) );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@ERROR_MESSAGE", System.Data.DbType.String ) );
                cmd.Parameters[ "@ERROR_MESSAGE" ].Direction = System.Data.ParameterDirection.Output;
                cmd.Parameters[ "@GUID_ID" ].Value = this.m_uuidID;
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
                        case 2:
                        {
                            DevExpress.XtraEditors.XtraMessageBox.Show(  "Ошибка выполнения запроса на удаление " + this.m_strName + 
                                "\nОшибка : " + ( System.String )cmd.Parameters[ "@ERROR_MESSAGE" ].Value, "Внимание",
                                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
                            break;
                        }
                        default:
                        {
                            DevExpress.XtraEditors.XtraMessageBox.Show(  "Ошибка выполнения запроса на удаление " + this.m_strName + 
                                "\nОшибка : " + ( System.String )cmd.Parameters[ "@ERROR_MESSAGE" ].Value, "Внимание",
                                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
                            break;
                        }
                    }
                }
            }
            catch( System.Exception e )
            {
                DevExpress.XtraEditors.XtraMessageBox.Show( 
                "Ошибка выполнения запроса на удаление " + this.m_strName + "\n\nТекст ошибки: " + e.Message, "Внимание",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
            }
			finally // очищаем занимаемые ресурсы
            {
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
                if( this.m_strName == "" )
                {
                    DevExpress.XtraEditors.XtraMessageBox.Show(  "Имя объекта нужно определить!", "Внимание",
                        System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning );
                    return bRet;
                }
                // действие
                if( this.m_objBudgetDocEvent == null )
                {
                    DevExpress.XtraEditors.XtraMessageBox.Show(  "Необходимо указать действие!\nОбъект: " + this.m_strName, "Внимание",
                        System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning );
                    return bRet;
                }
                // тип документа
                if( this.m_objBudgetDocType == null )
                {
                    DevExpress.XtraEditors.XtraMessageBox.Show(  "Необходимо указать тип документа!\nОбъект: " + this.m_strName, "Внимание",
                        System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning );
                    return bRet;
                }
                // динамическое право
                if( this.m_objBudgetDocType == null )
                {
                    DevExpress.XtraEditors.XtraMessageBox.Show(  "Необходимо указать динамическое право!\nОбъект: " + this.m_strName, "Внимание",
                        System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning );
                    return bRet;
                }
                // проверим не пустая ли структура
                if( this.m_AccountTrnTypeList.Count == 0 )
                {
                    DevExpress.XtraEditors.XtraMessageBox.Show(  "У объекта отсутствует список типов бюджетных проводок!", "Внимание",
                        System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning );
                    return bRet;
                }
                bRet = true;
            }
            catch( System.Exception f )
            {
                DevExpress.XtraEditors.XtraMessageBox.Show( 
                "Ошибка проверки свойств объекта.\n\nТекст ошибки: " + f.Message, "Внимание",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
            }

            return bRet;
        }
        /// <summary>
        /// Добавить запись в БД
        /// </summary>
        /// <param name="objProfile">профайл</param>
        /// <returns>true - удачное завершение; false - ошибка</returns>
        public override System.Boolean Add( UniXP.Common.CProfile objProfile )
        {
            System.Boolean bRet = false;
            // проверяем свойства
            if( this.IsValidateProperties() == false ) { return bRet; }

            System.Data.SqlClient.SqlConnection DBConnection = objProfile.GetDBSource();
            if( DBConnection == null ) { return bRet; }
            System.Data.SqlClient.SqlTransaction DBTransaction = DBConnection.BeginTransaction();

            try
            {
                // соединение с БД получено, прописываем команду на создание записи в БД
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
                cmd.Connection = DBConnection;
                cmd.Transaction = DBTransaction;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = System.String.Format( "[{0}].[dbo].[sp_AddAccTrnEvent]", objProfile.GetOptionsDllDBName() );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ( ( System.Byte )( 0 ) ), ( ( System.Byte )( 0 ) ), "", System.Data.DataRowVersion.Current, null ) );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@GUID_ID", System.Data.SqlDbType.UniqueIdentifier, 4, System.Data.ParameterDirection.Output, false, ( ( System.Byte )( 0 ) ), ( ( System.Byte )( 0 ) ), "", System.Data.DataRowVersion.Current, null ) );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@ACCTRNEVENT_NAME", System.Data.DbType.String ) );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@BUDGETDOCEVENT_GUID_ID", System.Data.SqlDbType.UniqueIdentifier ) );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@BUDGETDOCTYPE_GUID_ID", System.Data.SqlDbType.UniqueIdentifier ) );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@RIGHT_ID", System.Data.SqlDbType.Int ) );
                if( this.m_uuidID.CompareTo( System.Guid.Empty ) != 0 )
                {
                    cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@ACCTRNEVENT_GUID_ID", System.Data.SqlDbType.UniqueIdentifier ) );
                    cmd.Parameters[ "@ACCTRNEVENT_GUID_ID" ].Value = this.m_uuidID;
                }
                cmd.Parameters[ "@ACCTRNEVENT_NAME" ].Value = this.m_strName;
                cmd.Parameters[ "@BUDGETDOCEVENT_GUID_ID" ].Value = this.m_objBudgetDocEvent.uuidID;
                cmd.Parameters[ "@BUDGETDOCTYPE_GUID_ID" ].Value = this.m_objBudgetDocType.uuidID;
                cmd.Parameters[ "@RIGHT_ID" ].Value = this.m_objDynamicRight.ID;
                cmd.ExecuteNonQuery();
                System.Int32 iRet = ( System.Int32 )cmd.Parameters[ "@RETURN_VALUE" ].Value;
                if( iRet == 0 )
                {
                    this.m_uuidID = ( System.Guid )cmd.Parameters[ "@GUID_ID" ].Value;
                    // сохраняем список типов бюджетных проводок
                    bRet = this.SaveDeclaration( objProfile, cmd );
                    if( bRet )
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
                    switch( iRet )
                    {
                        case 1:
                        {
                            DevExpress.XtraEditors.XtraMessageBox.Show(  "Объект с заданным именем существует : '" + this.m_strName + "'", "Внимание",
                                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning );
                            break;
                        }
                        case 2:
                        {
                            DevExpress.XtraEditors.XtraMessageBox.Show(  "Действие с указанным идентификатором не найдено : '" + this.m_objBudgetDocEvent.uuidID.ToString() + "'", "Внимание",
                                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning );
                            break;
                        }
                        case 3:
                        {
                            DevExpress.XtraEditors.XtraMessageBox.Show(  "Тип документа с указанным идентификатором не найден : '" + this.m_objBudgetDocType.uuidID.ToString() + "'", "Внимание",
                                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning );
                            break;
                        }
                        case 4:
                        {
                            DevExpress.XtraEditors.XtraMessageBox.Show(  "Динамическое право с указанным идентификатором не найдено: '" + this.m_objDynamicRight.ID.ToString() + "'", "Внимание",
                                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning );
                            break;
                        }
                        default:
                        {
                            DevExpress.XtraEditors.XtraMessageBox.Show(  "Ошибка создания объекта : " + this.m_strName, "Ошибка",
                                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
                            break;
                        }
                    }
                }

                cmd.Dispose();
            }
            catch( System.Exception f )
            {
                // откатываем транзакцию
                DBTransaction.Rollback();
                DevExpress.XtraEditors.XtraMessageBox.Show( 
                "Не удалось создать объект : " + this.m_strName + "\n\nТекст ошибки: " + f.Message, "Внимание",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
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
        /// <param name="cmd">SQL-команда</param>
        /// <returns>true - удачное завершение; false - ошибка</returns>
        private System.Boolean Add( UniXP.Common.CProfile objProfile, System.Data.SqlClient.SqlCommand cmd )
        {
            System.Boolean bRet = false;
            // проверяем свойства
            if( this.IsValidateProperties() == false ) { return bRet; }

            if( cmd == null ) { return bRet; }
            try
            {
                // соединение с БД получено, прописываем команду на создание записи в БД
                cmd.Parameters.Clear();
                cmd.CommandText = System.String.Format( "[{0}].[dbo].[sp_AddAccTrnEvent]", objProfile.GetOptionsDllDBName() );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ( ( System.Byte )( 0 ) ), ( ( System.Byte )( 0 ) ), "", System.Data.DataRowVersion.Current, null ) );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@GUID_ID", System.Data.SqlDbType.UniqueIdentifier, 4, System.Data.ParameterDirection.Output, false, ( ( System.Byte )( 0 ) ), ( ( System.Byte )( 0 ) ), "", System.Data.DataRowVersion.Current, null ) );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@ACCTRNEVENT_NAME", System.Data.DbType.String ) );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@BUDGETDOCEVENT_GUID_ID", System.Data.SqlDbType.UniqueIdentifier ) );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@BUDGETDOCTYPE_GUID_ID", System.Data.SqlDbType.UniqueIdentifier ) );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@RIGHT_ID", System.Data.SqlDbType.Int ) );
                if( this.m_uuidID.CompareTo( System.Guid.Empty ) != 0 )
                {
                    cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@ACCTRNEVENT_GUID_ID", System.Data.SqlDbType.UniqueIdentifier ) );
                    cmd.Parameters[ "@ACCTRNEVENT_GUID_ID" ].Value = this.m_uuidID;
                }
                cmd.Parameters[ "@ACCTRNEVENT_NAME" ].Value = this.m_strName;
                cmd.Parameters[ "@BUDGETDOCEVENT_GUID_ID" ].Value = this.m_objBudgetDocEvent.uuidID;
                cmd.Parameters[ "@BUDGETDOCTYPE_GUID_ID" ].Value = this.m_objBudgetDocType.uuidID;
                cmd.Parameters[ "@RIGHT_ID" ].Value = this.m_objDynamicRight.ID;
                cmd.ExecuteNonQuery();
                System.Int32 iRet = ( System.Int32 )cmd.Parameters[ "@RETURN_VALUE" ].Value;
                if( iRet == 0 )
                {
                    this.m_uuidID = ( System.Guid )cmd.Parameters[ "@GUID_ID" ].Value;
                    // сохраняем список типов бюджетных проводок
                    bRet = this.SaveDeclaration( objProfile, cmd );
                }
                else
                {
                    switch( iRet )
                    {
                        case 1:
                        {
                            DevExpress.XtraEditors.XtraMessageBox.Show(  "Объект с заданным именем существует : '" + this.m_strName + "'", "Внимание",
                                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning );
                            break;
                        }
                        case 2:
                        {
                            DevExpress.XtraEditors.XtraMessageBox.Show(  "Действие с указанным идентификатором не найдено : '" + this.m_objBudgetDocEvent.uuidID.ToString() + "'", "Внимание",
                                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning );
                            break;
                        }
                        case 3:
                        {
                            DevExpress.XtraEditors.XtraMessageBox.Show(  "Тип документа с указанным идентификатором не найден : '" + this.m_objBudgetDocType.uuidID.ToString() + "'", "Внимание",
                                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning );
                            break;
                        }
                        case 4:
                        {
                            DevExpress.XtraEditors.XtraMessageBox.Show(  "Динамическое право с указанным идентификатором не найдено: '" + this.m_objDynamicRight.ID.ToString() + "'", "Внимание",
                                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning );
                            break;
                        }
                        default:
                        {
                            DevExpress.XtraEditors.XtraMessageBox.Show(  "Ошибка создания объекта : " + this.m_strName, "Ошибка",
                                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
                            break;
                        }
                    }
                }
            }
            catch( System.Exception f )
            {
                DevExpress.XtraEditors.XtraMessageBox.Show( 
                "Не удалось создать объект : " + this.m_strName + "\n\nТекст ошибки: " + f.Message, "Внимание",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
            }
			finally // очищаем занимаемые ресурсы
            {
            }
            return bRet;
        }
        /// <summary>
        /// Сохраняет список типов бюджетной проводки в БД
        /// </summary>
        /// <param name="objProfile">профайл</param>
        /// <param name="cmd">SQL-команда</param>
        /// <returns>true - удачное завершение; false - ошибка</returns>
        private System.Boolean SaveDeclaration( UniXP.Common.CProfile objProfile, System.Data.SqlClient.SqlCommand cmd )
        {
            System.Boolean bRet = false;

            // Список типов бюджетных проводок не должен быть пуст
            if( ( this.m_AccountTrnTypeList == null ) || ( this.m_AccountTrnTypeList.Count == 0 ) ) 
            {
                DevExpress.XtraEditors.XtraMessageBox.Show( 
                "Список типов бюджетных проводок не должен быть пуст.", "Внимание",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning );
                return bRet; 
            }

            if( cmd == null ) { return bRet; }
            try
            {
                // сперва удаляем прежний список
                bRet = this.RemoveDeclaration( objProfile, cmd );
                if( bRet == true )
                {
                    cmd.Parameters.Clear();
                    cmd.CommandText = System.String.Format( "[{0}].[dbo].[sp_AddAccTrnEventDeclrn]", objProfile.GetOptionsDllDBName() );
                    cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ( ( System.Byte )( 0 ) ), ( ( System.Byte )( 0 ) ), "", System.Data.DataRowVersion.Current, null ) );
                    cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@ACCTRNEVENT_GUID_ID", System.Data.SqlDbType.UniqueIdentifier ) );
                    cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@ACCTRNTYPE_GUID_ID", System.Data.SqlDbType.UniqueIdentifier ) );
                    cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@ORDERNUM", System.Data.SqlDbType.Int ) );
                    cmd.Parameters[ "@ACCTRNEVENT_GUID_ID" ].Value = this.m_uuidID;
                    System.Int32 iRet = 0;
                    for( System.Int32 i = 0; i < this.m_AccountTrnTypeList.Count; i++ )
                    {
                        cmd.Parameters[ "@ORDERNUM" ].Value = i;
                        cmd.Parameters[ "@ACCTRNTYPE_GUID_ID" ].Value = this.m_AccountTrnTypeList[ i ].uuidID;
                        cmd.ExecuteNonQuery();

                        iRet = ( System.Int32 )cmd.Parameters[ "@RETURN_VALUE" ].Value;
                        if( iRet != 0 )
                        {
                            switch( iRet )
                            {
                                case 1:
                                {
                                    DevExpress.XtraEditors.XtraMessageBox.Show(  "Тип транзакции с указанным идентификатором не найден : \n'" + this.m_AccountTrnTypeList[ i ].uuidID.ToString() + "'", "Внимание",
                                        System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning );
                                    break;
                                }
                                case 2:
                                {
                                    DevExpress.XtraEditors.XtraMessageBox.Show(  "Объект 'Тип док-та - Динамическое право - Действие' \nс указанным идентификатором не найден : \n" + this.uuidID.ToString() + "'", "Внимание",
                                        System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning );
                                    break;
                                }
                                default:
                                {
                                    DevExpress.XtraEditors.XtraMessageBox.Show(  "Ошибка добавления типа бюджетной проводки в список : " + this.m_AccountTrnTypeList[ i ].Name, "Ошибка",
                                        System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
                                    break;
                                }
                            }

                            break;
                        }
                    }
                    bRet = ( iRet == 0 );
                }
            }
            catch( System.Exception f )
            {
                DevExpress.XtraEditors.XtraMessageBox.Show( 
                "Не удалось сохранить список типов бюджетной проводки.\n\nТекст ошибки: " + f.Message, "Внимание",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
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
            if( this.IsValidateProperties() == false )
            {
                return bRet;
            }

            System.Data.SqlClient.SqlConnection DBConnection = objProfile.GetDBSource();
            if( DBConnection == null ) { return bRet; }
            System.Data.SqlClient.SqlTransaction DBTransaction = DBConnection.BeginTransaction();

            try
            {
                // соединение с БД получено, прописываем команду на создание записи в БД
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
                cmd.Connection = DBConnection;
                cmd.Transaction = DBTransaction;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = System.String.Format( "[{0}].[dbo].[sp_EditAccTrnEvent]", objProfile.GetOptionsDllDBName() );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ( ( System.Byte )( 0 ) ), ( ( System.Byte )( 0 ) ), "", System.Data.DataRowVersion.Current, null ) );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@GUID_ID", System.Data.SqlDbType.UniqueIdentifier ) );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@ACCTRNEVENT_NAME", System.Data.DbType.String ) );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@BUDGETDOCEVENT_GUID_ID", System.Data.SqlDbType.UniqueIdentifier ) );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@BUDGETDOCTYPE_GUID_ID", System.Data.SqlDbType.UniqueIdentifier ) );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@RIGHT_ID", System.Data.SqlDbType.Int ) );
                cmd.Parameters[ "@GUID_ID" ].Value = this.m_uuidID;
                cmd.Parameters[ "@ACCTRNEVENT_NAME" ].Value = this.m_strName;
                cmd.Parameters[ "@BUDGETDOCEVENT_GUID_ID" ].Value = this.m_objBudgetDocEvent.uuidID;
                cmd.Parameters[ "@BUDGETDOCTYPE_GUID_ID" ].Value = this.m_objBudgetDocType.uuidID;
                cmd.Parameters[ "@RIGHT_ID" ].Value = this.m_objDynamicRight.ID;
                cmd.ExecuteNonQuery();
                System.Int32 iRet = ( System.Int32 )cmd.Parameters[ "@RETURN_VALUE" ].Value;
                if( iRet == 0 )
                {
                    // сохраняем структуру шаблона маршрута
                    bRet = this.SaveDeclaration( objProfile, cmd );
                    if( bRet )
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
                    switch( iRet )
                    {
                        case 1:
                        {
                            DevExpress.XtraEditors.XtraMessageBox.Show(  "Объект с заданным именем существует : '" + this.m_strName + "'", "Внимание",
                                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning );
                            break;
                        }
                        case 2:
                        {
                            DevExpress.XtraEditors.XtraMessageBox.Show(  "Объект с указанным идентификатором не найден :\n'" + this.uuidID.ToString() + "'", "Внимание",
                                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning );
                            break;
                        }
                        case 3:
                        {
                            DevExpress.XtraEditors.XtraMessageBox.Show(  "Действие с указанным идентификатором не найдено :\n'" + this.m_objBudgetDocEvent.uuidID.ToString() + "'", "Внимание",
                                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning );
                            break;
                        }
                        case 4:
                        {
                            DevExpress.XtraEditors.XtraMessageBox.Show(  "Тип документа с указанным идентификатором не найден :\n'" + this.m_objBudgetDocType.uuidID.ToString() + "'", "Внимание",
                                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning );
                            break;
                        }
                        case 5:
                        {
                            DevExpress.XtraEditors.XtraMessageBox.Show(  "Динамическое право с указанным идентификатором не найдено:\n'" + this.m_objDynamicRight.ID.ToString() + "'", "Внимание",
                                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning );
                            break;
                        }
                        default:
                        {
                            DevExpress.XtraEditors.XtraMessageBox.Show(  "Ошибка создания объекта : " + this.m_strName, "Ошибка",
                                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
                            break;
                        }
                    }
                }
                cmd.Dispose();
            }
            catch( System.Exception e )
            {
                // откатываем транзакцию
                DBTransaction.Rollback();
                DevExpress.XtraEditors.XtraMessageBox.Show( 
                "Ошибка изменения свойств объекта : " + this.m_strName + "\n" + e.Message, "Внимание",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
            }
			finally // очищаем занимаемые ресурсы
            {
                DBConnection.Close();
            }
            return bRet;
        }
        #endregion

        #region Сохранить список объектов "Действие - Тип документа - Динамическое право" в БД  
        /// <summary>
        /// Сохраняет список объектов "Действие - Тип документа - Динамическое право" в БД
        /// </summary>
        /// <param name="objProfile">профайл</param>
        /// <param name="objList">список объектов</param>
        /// <returns>true - удачное завершение; false - ошибка</returns>    
        public static System.Boolean SaveAccountTrnEventList( UniXP.Common.CProfile objProfile,
            List<CAccountTrnEvent> objList )
        {
            System.Boolean bRet = false;
            if( ( objList == null ) || ( objList.Count == 0 ) )
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(  "Список сохраняемых объектов не должен быть пустым!", "Внимание",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
                return bRet;
            }

            System.Data.SqlClient.SqlConnection DBConnection = objProfile.GetDBSource();
            if( DBConnection == null ) { return bRet; }
            System.Data.SqlClient.SqlTransaction DBTransaction = DBConnection.BeginTransaction();

            try
            {
                // соединение с БД получено, прописываем команду на создание записи в БД
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
                cmd.Connection = DBConnection;
                cmd.Transaction = DBTransaction;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                // сперва удаляем нах все объекты
                bRet = CAccountTrnEvent.RemoveAll( objProfile, cmd );
                if( bRet == false )
                {
                    // откатываем транзакцию
                    DBTransaction.Rollback();
                }
                else
                {
                    foreach( CAccountTrnEvent objAccountTrnEvent in objList )
                    {
                        // сохраняем объект в БД
                        bRet = objAccountTrnEvent.Add( objProfile, cmd );
                        if( bRet == false )
                        {
                            break;
                        }
                    }
                    if( bRet == false )
                    {
                        // откатываем транзакцию
                        DBTransaction.Rollback();
                    }
                    else
                    {
                        // подтверждаем транзакцию
                        DBTransaction.Commit();
                    }
                }

                cmd.Dispose();
            }
            catch( System.Exception f )
            {
                // откатываем транзакцию
                DBTransaction.Rollback();
                DevExpress.XtraEditors.XtraMessageBox.Show( 
                "Ошибка сохранения списка объектов в БД.\n\nТекст ошибки: " + f.Message, "Внимание",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
            }
			finally // очищаем занимаемые ресурсы
            {
                DBConnection.Close();
            }

            return bRet;
        }

        #endregion 
    }
}
