using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;


namespace ERP_Budget.Common
{
    /// <summary>
    /// Класс "Состояние бюджетного документа"
    /// </summary>
    public class CBudgetDocState : IBaseListItem
    {
        #region Переменные 
        /// <summary>
        /// Номер по порядку
        /// </summary>
        private System.Int32 m_iOrderNum;
        /// <summary>
        /// Номер по порядку
        /// </summary>
        [DisplayName( "Номер по порядку" )]
        [Description( "Номер в логической последовательности состояний документа" )]
        [Category( "Обязательные значения" )]
        public System.Int32 OrderNum
        {
            get { return m_iOrderNum; }
            set { m_iOrderNum = value; }
        }
        private CBudgetDocStateDraw m_objBudgetDocStateDraw;
        [DisplayName( "Картинка" )]
        [Description( "Картинка, соответствующая состоянию бюджетного документа" )]
        [Category( "Дополнительные значения" )]
        [ReadOnly( true )]
        public CBudgetDocStateDraw BudgetDocStateDraw
        {
            get { return m_objBudgetDocStateDraw; }
            set { m_objBudgetDocStateDraw = value; }
        }

        #endregion

        #region Конструкторы 
        public CBudgetDocState()
        {
            this.m_uuidID = System.Guid.Empty;
            this.m_strName = "";
            this.m_iOrderNum = 0;
            this.m_objBudgetDocStateDraw = null;
        }
        public CBudgetDocState(System.Guid uuidID)
        {
            this.m_uuidID = uuidID;
            this.m_strName = "";
            this.m_iOrderNum = 0;
            this.m_objBudgetDocStateDraw = null;
        }
        public CBudgetDocState(System.Guid uuidID, System.String strName)
        {
            this.m_uuidID = uuidID;
            this.m_strName = strName;
            this.m_iOrderNum = 0;
            this.m_objBudgetDocStateDraw = null;
        }
        public CBudgetDocState( System.Guid uuidID, System.String strName, System.Int32 iOrderNum )
        {
            this.m_uuidID = uuidID;
            this.m_strName = strName;
            this.m_iOrderNum = iOrderNum;
            this.m_objBudgetDocStateDraw = null;
        }
        #endregion

        #region Список состояний бюджетного документа 
        /// <summary>
        /// Возвращает список состояний бюджетного документа
        /// </summary>
        /// <param name="objProfile">профайл</param>
        /// <returns>список событий бюджетного документа в виде таблицы</returns>
        public static System.Data.DataTable GetBudgetDocStateTable( UniXP.Common.CProfile objProfile )
        {
            System.Data.DataTable dtBudgetDocState = null;
            try
            {
                // сформируем таблицу
                dtBudgetDocState = new System.Data.DataTable( "dtBudgetDocState" );
                dtBudgetDocState.Columns.Add( new System.Data.DataColumn( "GUID_ID", System.Type.GetType( "System.Guid" ) ) );
                dtBudgetDocState.Columns.Add( new System.Data.DataColumn( "BUDGETDOCSTATE_NAME", System.Type.GetType( "System.String" ) ) );

                System.Data.SqlClient.SqlConnection DBConnection = objProfile.GetDBSource();
                if( DBConnection == null ) { return dtBudgetDocState; }

                // соединение с БД получено, прописываем команду на выборку данных
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
                cmd.Connection = DBConnection;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = System.String.Format( "[{0}].[dbo].[sp_GetBudgetDocState]", objProfile.GetOptionsDllDBName() );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ( ( System.Byte )( 0 ) ), ( ( System.Byte )( 0 ) ), "", System.Data.DataRowVersion.Current, null ) );
                System.Data.SqlClient.SqlDataReader rs = cmd.ExecuteReader();
                if( rs.HasRows )
                {
                    System.Data.DataRow row = null;
                    while( rs.Read() )
                    {
                        row = dtBudgetDocState.NewRow();
                        row[ "GUID_ID" ] = rs.GetGuid( 0 );
                        row[ "BUDGETDOCSTATE_NAME" ] = rs.GetString( 1 );
                        dtBudgetDocState.Rows.Add( row );
                    }
                    dtBudgetDocState.AcceptChanges();
                }
                cmd.Dispose();
                rs.Dispose();
                DBConnection.Close();
            }
            catch( System.Exception e )
            {
                DevExpress.XtraEditors.XtraMessageBox.Show( 
                "Не удалось получить список состояний бюджетного документа.\n" + e.Message, "Внимание",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
            }
            return dtBudgetDocState;
        }
        /// <summary>
        /// Возвращает список состояний бюджетного документа
        /// </summary>
        /// <param name="objProfile">профайл</param>
        /// <returns>список классов состояний бюджетного документа</returns>
        public CBaseList<CBudgetDocState> GetBudgetDocStateList( UniXP.Common.CProfile objProfile )
        {
            CBaseList<CBudgetDocState> objList = new CBaseList<CBudgetDocState>();

            System.Data.SqlClient.SqlConnection DBConnection = objProfile.GetDBSource();
            if( DBConnection == null ) { return objList; }

            try
            {
                // соединение с БД получено, прописываем команду на выборку данных
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
                cmd.Connection = DBConnection;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = System.String.Format( "[{0}].[dbo].[sp_GetBudgetDocState]", objProfile.GetOptionsDllDBName() );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ( ( System.Byte )( 0 ) ), ( ( System.Byte )( 0 ) ), "", System.Data.DataRowVersion.Current, null ) );
                System.Data.SqlClient.SqlDataReader rs = cmd.ExecuteReader();
                if( rs.HasRows )
                {
                    // набор данных непустой
                    CBudgetDocState objBudgetDocState = null;
                    while( rs.Read() )
                    {
                        objBudgetDocState = new CBudgetDocState();
                        objBudgetDocState.m_uuidID = rs.GetGuid( 0 );
                        objBudgetDocState.m_strName = rs.GetString( 1 );
                        objBudgetDocState.m_iOrderNum = rs.GetInt32( 2 );
                        objList.AddItemToList( objBudgetDocState );
                    }
                }
                else
                {
                    DevExpress.XtraEditors.XtraMessageBox.Show( 
                    "Не удалось получить список состояний бюджетного документа.\nВ БД не найдена информация.", "Внимание",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning );

                }
                cmd.Dispose();
                rs.Dispose();
            }
            catch( System.Exception e )
            {
                DevExpress.XtraEditors.XtraMessageBox.Show( 
                "Не удалось получить список состояний бюджетного документа.\n" + e.Message, "Внимание",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
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
        /// <param name="objProfile">профайл</param>
        /// <returns>список классов состояний бюджетного документа</returns>
        public static System.Collections.Generic.List<CBudgetDocState> GetBudgetDocStateListObj( UniXP.Common.CProfile objProfile )
        {
            System.Collections.Generic.List<CBudgetDocState> objList = new System.Collections.Generic.List<CBudgetDocState>();

            System.Data.SqlClient.SqlConnection DBConnection = objProfile.GetDBSource();
            if( DBConnection == null ) { return objList; }

            try
            {
                // соединение с БД получено, прописываем команду на выборку данных
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
                cmd.Connection = DBConnection;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = System.String.Format( "[{0}].[dbo].[sp_GetBudgetDocState]", objProfile.GetOptionsDllDBName() );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ( ( System.Byte )( 0 ) ), ( ( System.Byte )( 0 ) ), "", System.Data.DataRowVersion.Current, null ) );
                System.Data.SqlClient.SqlDataReader rs = cmd.ExecuteReader();
                if( rs.HasRows )
                {
                    while( rs.Read() )
                    {
                        objList.Add( new CBudgetDocState( rs.GetGuid( 0 ), rs.GetString( 1 ), rs.GetInt32( 2 ) ) );
                    }
                }
                rs.Close();
                rs.Dispose();
                cmd.Dispose();
            }
            catch( System.Exception f )
            {
                DevExpress.XtraEditors.XtraMessageBox.Show( 
                "Не удалось получить список состояний бюджетного документа.\n\nТекст ошибки: " + f.Message, "Внимание",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
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
        /// <param name="objProfile">профайл</param>
        /// <returns>список классов состояний бюджетного документа</returns>
        public static System.Collections.Generic.List<CBudgetDocState> GetBudgetDocStateListPictures( 
            UniXP.Common.CProfile objProfile )
        {
            System.Collections.Generic.List<CBudgetDocState> objList = new System.Collections.Generic.List<CBudgetDocState>();

            System.Data.SqlClient.SqlConnection DBConnection = objProfile.GetDBSource();
            if( DBConnection == null ) { return objList; }

            try
            {
                // соединение с БД получено, прописываем команду на выборку данных
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
                cmd.Connection = DBConnection;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = System.String.Format( "[{0}].[dbo].[sp_GetBudgetDocState]", objProfile.GetOptionsDllDBName() );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ( ( System.Byte )( 0 ) ), ( ( System.Byte )( 0 ) ), "", System.Data.DataRowVersion.Current, null ) );
                System.Data.SqlClient.SqlDataReader rs = cmd.ExecuteReader();
                if( rs.HasRows )
                {
                    while( rs.Read() )
                    {
                        objList.Add( new CBudgetDocState( rs.GetGuid( 0 ), rs.GetString( 1 ), rs.GetInt32( 2 ) ) );
                    }
                }
                rs.Close();

                // теперь загрузим туда картинки
                foreach( CBudgetDocState objBudgetDocState in objList )
                {
                    objBudgetDocState.BudgetDocStateDraw = CBudgetDocStateDraw.GetBudgetDocStateDraw( objProfile, objBudgetDocState, cmd );
                }

                rs.Dispose();
                cmd.Dispose();
            }
            catch( System.Exception f )
            {
                DevExpress.XtraEditors.XtraMessageBox.Show( 
                "Не удалось получить список состояний бюджетного документа.\n\nТекст ошибки: " + f.Message, "Внимание",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
            }
			finally // очищаем занимаемые ресурсы
            {
                DBConnection.Close();
            }
            return objList;
        }

        /// <summary>
        /// Обновляет список состояний бюджетных документов
        /// </summary>
        /// <param name="objProfile">профайл</param>
        /// <param name="cmd">SQL - команда</param>
        /// <param name="objList">обновляемый список</param>
        /// <returns></returns>
        public static void RefreshBudgetDocStateList( UniXP.Common.CProfile objProfile,
            System.Data.SqlClient.SqlCommand cmd, System.Collections.Generic.List<CBudgetDocState> objList )
        {
            if( cmd == null ) { return; }
            objList.Clear();

            try
            {
                cmd.Parameters.Clear();
                cmd.CommandText = System.String.Format( "[{0}].[dbo].[sp_GetBudgetDocState]", objProfile.GetOptionsDllDBName() );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ( ( System.Byte )( 0 ) ), ( ( System.Byte )( 0 ) ), "", System.Data.DataRowVersion.Current, null ) );
                System.Data.SqlClient.SqlDataReader rs = cmd.ExecuteReader();
                if( rs.HasRows )
                {
                    while( rs.Read() )
                    {
                        objList.Add( new CBudgetDocState( rs.GetGuid( 0 ), rs.GetString( 1 ), rs.GetInt32( 2 ) ) );
                    }
                }
                rs.Close();
                rs.Dispose();
            }
            catch( System.Exception e )
            {
                DevExpress.XtraEditors.XtraMessageBox.Show( 
                "Не удалось получить список состояний бюджетного документа.\nТекст ошибки: " + e.Message, "Внимание",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
            }
			finally // очищаем занимаемые ресурсы
            {
            }
            return;
        }
        /// <summary>
        /// Возвращает состояние с номером 6 (удалена)
        /// </summary>
        /// <param name="objProfile">профайл</param>
        /// <returns>объект класса CBudgetDocState</returns>
        public static CBudgetDocState GetDropBudgetDocState( UniXP.Common.CProfile objProfile )
        {
            CBudgetDocState objRet = null;
            try
            {
                System.Data.SqlClient.SqlConnection DBConnection = objProfile.GetDBSource();
                if( DBConnection == null ) { return objRet; }

                // соединение с БД получено, прописываем команду на выборку данных
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
                cmd.Connection = DBConnection;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = System.String.Format( "[{0}].[dbo].[sp_GetBudgetDocState]", objProfile.GetOptionsDllDBName() );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ( ( System.Byte )( 0 ) ), ( ( System.Byte )( 0 ) ), "", System.Data.DataRowVersion.Current, null ) );
                System.Data.SqlClient.SqlDataReader rs = cmd.ExecuteReader();
                if( rs.HasRows )
                {
                    while( rs.Read() )
                    {
                        if( ( ( System.Int32 )rs[ 2 ] ) == 6 )
                        {
                            // состояние с идентификатором 6
                            objRet = new CBudgetDocState( rs.GetGuid( 0 ), rs.GetString( 1 ), 6 );
                            break;
                        }
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
                "Не удалось получить состояние бюджетного документа.\n" + e.Message, "Внимание",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
            }
            return objRet;
        }
        /// <summary>
        /// Обновляет дерево со списком состояний бюджетных документов
        /// </summary>
        /// <param name="objProfile">профайл</param>
        /// <param name="objTreeList">дерево</param>
        /// <returns>true - удачное завершение; false - ошибка</returns>
        public static System.Boolean bRefreshBudgetDocTypeTree( UniXP.Common.CProfile objProfile,
            DevExpress.XtraTreeList.TreeList objTreeList )
        {
            System.Boolean bRet = false;
            // очищаем дерево
            objTreeList.Nodes.Clear();
            // запрашиваем соединение с БД
            System.Data.SqlClient.SqlConnection DBConnection = objProfile.GetDBSource();
            if( DBConnection == null )
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(
                    "Не удалось обновить список состояний бюджетных документов.\nОтсутствует соединение с БД.", "Внимание",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
                return bRet;
            }

            try
            {
                // соединение с БД получено, прописываем команду на выборку данных
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
                cmd.Connection = DBConnection;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = System.String.Format( "[{0}].[dbo].[sp_GetBudgetDocState]", objProfile.GetOptionsDllDBName() );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ( ( System.Byte )( 0 ) ), ( ( System.Byte )( 0 ) ), "", System.Data.DataRowVersion.Current, null ) );
                System.Data.SqlClient.SqlDataReader rs = cmd.ExecuteReader();
                if( rs.HasRows )
                {
                    // набор данных непустой
                    CBudgetDocState objBudgetDocState = null;
                    while( rs.Read() )
                    {
                        // создаем объект класса CBudgetDocState
                        objBudgetDocState = new CBudgetDocState( ( System.Guid )rs[ "GUID_ID" ],
                            ( System.String )rs[ "BUDGETDOCSTATE_NAME" ], ( System.Int32 )rs[ "BUDGETDOCSTATE_ID" ] );
                        // создаем узел дерева
                        DevExpress.XtraTreeList.Nodes.TreeListNode objNode = 
                                objTreeList.AppendNode( new object[] { objBudgetDocState.Name }, null );
                        objNode.Tag = objBudgetDocState;

                    }
                }
                rs.Close();
                rs.Dispose();
                cmd.Dispose();

                bRet = true;
            }
            catch( System.Exception f )
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(
                "Не удалось обновить список состояний бюджетных документов.\n\nТекст ошибки: " + f.Message, "Внимание",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
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
                cmd.CommandText = System.String.Format( "[{0}].[dbo].[sp_GetBudgetDocState]", objProfile.GetOptionsDllDBName() );
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
                    this.m_iOrderNum = rs.GetInt32( 2 );
                    bRet = true;
                }
                else
                {
                    DevExpress.XtraEditors.XtraMessageBox.Show( 
                    "Не удалось проинициализировать класс CBudgetDocState.\nВ БД не найдена информация.", "Внимание",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning );

                }
                cmd.Dispose();
                rs.Dispose();
            }
            catch( System.Exception e )
            {
                DevExpress.XtraEditors.XtraMessageBox.Show( 
                "Не удалось проинициализировать класс CBudgetDocState.\n" + e.Message, "Внимание",
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
                DevExpress.XtraEditors.XtraMessageBox.Show( "Недопустимое значение уникального идентификатора объекта", "Внимание",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
                return bRet;
            }

            System.Data.SqlClient.SqlConnection DBConnection = objProfile.GetDBSource();
            if( DBConnection == null )
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(
                "Не удалось установить соединение с базой данных.", "Внимание",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
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
                cmd.CommandText = System.String.Format( "[{0}].[dbo].[sp_DeleteBudgetDocState]", objProfile.GetOptionsDllDBName() );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ( ( System.Byte )( 0 ) ), ( ( System.Byte )( 0 ) ), "", System.Data.DataRowVersion.Current, null ) );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@GUID_ID", System.Data.SqlDbType.UniqueIdentifier ) );
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
                        case 1:
                        {
                            DevExpress.XtraEditors.XtraMessageBox.Show(  "На состояние бюджетного документа есть ссылка в бюджетном документе.\nУИ типа документа: " + this.uuidID.ToString(), "Внимание",
                                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning );
                            break;
                        }
                        case 2:
                        {
                            DevExpress.XtraEditors.XtraMessageBox.Show(  "На состояние бюджетного документа есть ссылка в описании.\nУИ типа документа: " + this.uuidID.ToString(), "Внимание",
                                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning );
                            break;
                        }
                        default:
                        {
                            DevExpress.XtraEditors.XtraMessageBox.Show(  "Ошибка выполнения запроса на удаление состояния бюджетного документа.\nСостояние бюджетного документа : " + this.m_strName, "Внимание",
                                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
                            break;
                        }
                    }
                }
                cmd.Dispose();
            }
            catch( System.Exception e )
            {
                DBTransaction.Rollback();
                DevExpress.XtraEditors.XtraMessageBox.Show( 
                "Не удалось удалить состояние бюджетного документа.\nТекст ошибки: " + e.Message, "Внимание",
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
                    DevExpress.XtraEditors.XtraMessageBox.Show( "Недопустимое значение наименования объекта", "Внимание",
                        System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
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
            if( IsValidateProperties() == false ) { return bRet; }

            System.Data.SqlClient.SqlConnection DBConnection = objProfile.GetDBSource();
            if( DBConnection == null )
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(
                "Не удалось установить соединение с базой данных.", "Внимание",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
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
                cmd.CommandText = System.String.Format( "[{0}].[dbo].[sp_AddBudgetDocState]", objProfile.GetOptionsDllDBName() );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ( ( System.Byte )( 0 ) ), ( ( System.Byte )( 0 ) ), "", System.Data.DataRowVersion.Current, null ) );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@GUID_ID", System.Data.SqlDbType.UniqueIdentifier, 4, System.Data.ParameterDirection.Output, false, ( ( System.Byte )( 0 ) ), ( ( System.Byte )( 0 ) ), "", System.Data.DataRowVersion.Current, null ) );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@BUDGETDOCSTATE_NAME", System.Data.DbType.String ) );

                cmd.Parameters[ "@BUDGETDOCSTATE_NAME" ].Value = this.m_strName;
                cmd.ExecuteNonQuery();
                System.Int32 iRet = ( System.Int32 )cmd.Parameters[ "@RETURN_VALUE" ].Value;
                if( iRet == 0 )
                {
                    // подтверждаем транзакцию
                    DBTransaction.Commit();
                    this.m_uuidID = ( System.Guid )cmd.Parameters[ "@GUID_ID" ].Value;
                    bRet = true;
                }
                else
                {
                    // откатываем транзакцию
                    DBTransaction.Rollback();
                    if( iRet == 1 )
                    {
                        DevExpress.XtraEditors.XtraMessageBox.Show(  "Состояние бюджетного документа  '" + this.m_strName + "' уже есть в БД", "Внимание",
                            System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning );
                    }
                    else
                    {
                        DevExpress.XtraEditors.XtraMessageBox.Show(  "Ошибка создания состояния бюджетного документа ", "Ошибка",
                            System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
                    }
                }
                cmd.Dispose();
            }
            catch( System.Exception e )
            {
                // откатываем транзакцию
                DBTransaction.Rollback();
                DevExpress.XtraEditors.XtraMessageBox.Show( 
                "Не удалось создать состояние бюджетного документа .\n" + e.Message, "Внимание",
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
            if( IsValidateProperties() == false ) { return bRet; }

            System.Data.SqlClient.SqlConnection DBConnection = objProfile.GetDBSource();
            if( DBConnection == null )
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(
                "Не удалось установить соединение с базой данных.", "Внимание",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
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
                cmd.CommandText = System.String.Format( "[{0}].[dbo].[sp_EditBudgetDocStateType]", objProfile.GetOptionsDllDBName() );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ( ( System.Byte )( 0 ) ), ( ( System.Byte )( 0 ) ), "", System.Data.DataRowVersion.Current, null ) );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@GUID_ID", System.Data.SqlDbType.UniqueIdentifier ) );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@BUDGETDOCSTATE_NAME", System.Data.DbType.String ) );

                cmd.Parameters[ "@GUID_ID" ].Value = this.m_uuidID;
                cmd.Parameters[ "@BUDGETDOCSTATE_NAME" ].Value = this.m_strName;
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
                        case 1:
                        DevExpress.XtraEditors.XtraMessageBox.Show(  "Состояние бюджетного документа '" + this.m_strName + "' уже есть в БД", "Внимание",
                            System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning );
                        break;
                        case 2:
                        DevExpress.XtraEditors.XtraMessageBox.Show(  "Запись с указанным идентификатором не найдена \n" + this.m_uuidID.ToString(), "Внимание",
                            System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
                        break;
                        default:
                        DevExpress.XtraEditors.XtraMessageBox.Show(  "Ошибка изменения состояния бюджетного документа", "Внимание",
                            System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
                        break;
                    }
                }
                cmd.Dispose();
            }
            catch( System.Exception e )
            {
                // откатываем транзакцию
                DBTransaction.Rollback();
                DevExpress.XtraEditors.XtraMessageBox.Show( 
                "Не удалось изменить состояние бюджетного документа.\n" + e.Message, "Внимание",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
            }
			finally // очищаем занимаемые ресурсы
            {
                DBConnection.Close();
            }
            return bRet;
        }
        #endregion

        /// <summary>
        /// Определяет способ отрисовки типа бюджетного документа
        /// </summary>
        /// <param name="objProfile">профайл</param>
        public void LoadPicture( UniXP.Common.CProfile objProfile )
        {
            try
            {
                this.m_objBudgetDocStateDraw = CBudgetDocStateDraw.GetBudgetDocStateDraw( objProfile, this );
            }
            catch( System.Exception f )
            {
                DevExpress.XtraEditors.XtraMessageBox.Show( 
                "Не удалось определить тип отображения картинки.\n\nТекст ошибки: " + f.Message, "Внимание",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
            }
            return;
        }

        public override string ToString()
        {
            return Name;
        }

    }
}
