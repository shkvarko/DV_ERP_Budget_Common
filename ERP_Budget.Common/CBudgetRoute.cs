using System;
using System.Collections.Generic;
using System.Text;

namespace ERP_Budget.Common
{
    /// <summary>
    /// Класс "Маршрут бюджетного документа"
    /// </summary>
    public class CBudgetRoute : IBaseListItem
    {
        #region Переменные, Свойства, Константы
        /// <summary>
        /// Список узлов маршрута
        /// </summary>
        private System.Collections.Generic.List<CRoutePoint> m_RoutePointList;
        /// <summary>
        /// Список узлов маршрута
        /// </summary>
        public System.Collections.Generic.List<CRoutePoint> RoutePointList
        {
            get { return m_RoutePointList; }
            set { }
        }
        /// <summary>
        /// Уникальный идентификатор бюджетного документа
        /// </summary>
        private System.Guid m_uuidBudgetDocID;
        /// <summary>
        /// Уникальный идентификатор бюджетного документа
        /// </summary>
        public System.Guid BudgetDocID
        {
            get { return m_uuidBudgetDocID; }
            set { m_uuidBudgetDocID = value; }
        }

        #endregion

        #region Конструкторы
        public CBudgetRoute()
        {
            this.m_uuidID = System.Guid.Empty;
            this.m_strName = "";
            this.m_uuidBudgetDocID = System.Guid.Empty;
            this.m_RoutePointList = new List<CRoutePoint>();
            this.m_RoutePointList.Clear();
        }

        public CBudgetRoute( System.Guid uuidBudgetDocID )
        {
            this.m_uuidID = System.Guid.Empty;
            this.m_strName = "";
            this.m_uuidBudgetDocID = uuidBudgetDocID;
            this.m_RoutePointList = new List<CRoutePoint>();
            this.m_RoutePointList.Clear();
        }

        public CBudgetRoute( System.Guid uuidBudgetDocID, List<CRoutePoint> objListRoutePoint )
        {
            this.m_uuidID = System.Guid.Empty;
            this.m_strName = "";
            this.m_uuidBudgetDocID = uuidBudgetDocID;
            this.m_RoutePointList = objListRoutePoint;
        }

        public CBudgetRoute( System.Guid uuidBudgetDocID, System.Data.SqlClient.SqlCommand cmd, UniXP.Common.CProfile objProfile )
        {
            this.m_uuidID = System.Guid.Empty;
            this.m_strName = "";
            this.m_uuidBudgetDocID = uuidBudgetDocID;
            this.m_RoutePointList = new List<CRoutePoint>();
            this.InitRouteDeclaration( cmd, objProfile);
        }
        #endregion

        #region Список точек маршрута
        /// <summary>
        /// Заполняет список точек маршрута
        /// </summary>
        /// <param name="cmd">SQL - команда</param>
        /// <param name="objProfile">профайл</param>
        /// <returns>true - удачное завершение; false - ошибка</returns>
        private System.Boolean InitRouteDeclaration( System.Data.SqlClient.SqlCommand cmd, UniXP.Common.CProfile objProfile)
        {
            System.Boolean bRet = false;

            if( cmd == null ) { return bRet; }
            try
            {
                this.m_RoutePointList.Clear();
                // соединение с БД получено, прописываем команду на выборку данных
                cmd.Parameters.Clear();
                cmd.CommandText = System.String.Format( "[{0}].[dbo].[sp_GetBudgetDocRouteDeclaration]", objProfile.GetOptionsDllDBName() );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ( ( System.Byte )( 0 ) ), ( ( System.Byte )( 0 ) ), "", System.Data.DataRowVersion.Current, null ) );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@BUDGETDOC_GUID_ID", System.Data.SqlDbType.UniqueIdentifier ) );
                cmd.Parameters["@BUDGETDOC_GUID_ID"].Value = this.m_uuidBudgetDocID;
                System.Data.SqlClient.SqlDataReader rs = cmd.ExecuteReader();
                if( rs.HasRows )
                {
                    while( rs.Read() )
                    {
                        // проинициализируем список точек маршрута
                        if( rs[ 0 ] == System.DBNull.Value )
                        {
                            // начальное состояние null
                            this.m_RoutePointList.Add( new CRoutePoint( rs.GetBoolean( 3 ), rs.GetBoolean( 4 ),
                                new CBudgetDocEvent( rs.GetGuid( 2 ), rs.GetString( 5 ), rs.GetInt32( 15 ), System.Convert.ToBoolean(rs["BUDGETDOCEVENT_SHOWMONEY"] ), System.Convert.ToBoolean(rs["BUDGETDOCEVENT_CANCHANGEMONEY"]) ),
                                new CBudgetDocState( rs.GetGuid( 1 ), rs.GetString( 7 ), rs.GetInt32( 17 ) ),
                                new CRoutePointGroup( rs.GetGuid( 8 ), rs.GetString( 9 ) ), rs.GetBoolean( 10 ),
                                new CUser( rs.GetInt32( 11 ), rs.GetInt32( 12 ), rs.GetString( 13 ), rs.GetString( 14 ) ) ) );
                        }
                        else
                        {
                            this.m_RoutePointList.Add( new CRoutePoint( rs.GetBoolean( 3 ), rs.GetBoolean( 4 ),
                                new CBudgetDocEvent( rs.GetGuid( 2 ), rs.GetString( 5 ), rs.GetInt32( 15 ), System.Convert.ToBoolean(rs["BUDGETDOCEVENT_SHOWMONEY"]), System.Convert.ToBoolean(rs["BUDGETDOCEVENT_CANCHANGEMONEY"]) ),
                                new CBudgetDocState( rs.GetGuid( 0 ), rs.GetString( 6 ), rs.GetInt32( 16 ) ),
                                new CBudgetDocState( rs.GetGuid( 1 ), rs.GetString( 7 ), rs.GetInt32( 17 ) ),
                                new CRoutePointGroup( rs.GetGuid( 8 ), rs.GetString( 9 ) ), rs.GetBoolean( 10 ),
                                new CUser( rs.GetInt32( 11 ), rs.GetInt32( 12 ), rs.GetString( 13 ), rs.GetString( 14 ) ) ) );
                        }

                    }

                    bRet = true;
                }
                else
                {
                    DevExpress.XtraEditors.XtraMessageBox.Show( 
                    "Не удалось получить список точек маршрута для бюджетного документа.\nУИ бюджетного документа : " + 
                    this.m_uuidBudgetDocID.ToString() + "\nВ БД не найдена информация.", "Внимание",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning );

                }
                rs.Dispose();
            }
            catch( System.Exception e )
            {
                DevExpress.XtraEditors.XtraMessageBox.Show( 
                "Не удалось получить список точек маршрута для бюджетного документа.\nУИ бюджетного документа : " + 
                    this.m_uuidBudgetDocID.ToString() + "\n" + e.Message, "Внимание",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
            }
            finally // очищаем занимаемые ресурсы
            {
            }
            return bRet;
        }
        /// <summary>
        /// Обновляет список точек маршрута с учетом сортировки
        /// </summary>
        /// <param name="objProfile">профайл</param>
        /// <param name="uuidBudgetDocID">уи бюджетного документа</param>
        /// <param name="iOrderBy">код сортировки</param>
        /// <returns>true - удачное завершение; false - ошибка</returns>
        public System.Boolean LoadPointList(UniXP.Common.CProfile objProfile, System.Guid uuidBudgetDocID, System.Int32 iOrderBy )
        {
            System.Boolean bRet = false;

            System.Data.SqlClient.SqlConnection DBConnection = objProfile.GetDBSource();
            if (DBConnection == null) { return bRet; }

            try
            {
                this.m_RoutePointList.Clear();
                if (this.m_uuidBudgetDocID.CompareTo(System.Guid.Empty) == 0)
                {
                    this.m_uuidBudgetDocID = uuidID;
                }

                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
                cmd.Connection = DBConnection;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = System.String.Format("[{0}].[dbo].[sp_GetBudgetDocRouteDeclarationOrderBy]", objProfile.GetOptionsDllDBName());
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ((System.Byte)(0)), ((System.Byte)(0)), "", System.Data.DataRowVersion.Current, null));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@BUDGETDOC_GUID_ID", System.Data.SqlDbType.UniqueIdentifier));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ORDER_BY", System.Data.SqlDbType.Int));
                cmd.Parameters["@BUDGETDOC_GUID_ID"].Value = this.m_uuidBudgetDocID;
                cmd.Parameters["@ORDER_BY"].Value = iOrderBy;
                System.Data.SqlClient.SqlDataReader rs = cmd.ExecuteReader();
                if (rs.HasRows)
                {
                    while (rs.Read())
                    {
                        // проинициализируем список точек маршрута
                        if (rs[0] == System.DBNull.Value)
                        {
                            // начальное состояние null
                            this.m_RoutePointList.Add(new CRoutePoint(rs.GetBoolean(3), rs.GetBoolean(4),
                                new CBudgetDocEvent(rs.GetGuid(2), rs.GetString(5), rs.GetInt32(15)),
                                new CBudgetDocState(rs.GetGuid(1), rs.GetString(7), rs.GetInt32(17)),
                                new CRoutePointGroup(rs.GetGuid(8), rs.GetString(9)), rs.GetBoolean(10),
                                new CUser(rs.GetInt32(11), rs.GetInt32(12), rs.GetString(13), rs.GetString(14))));
                        }
                        else
                        {
                            this.m_RoutePointList.Add(new CRoutePoint(rs.GetBoolean(3), rs.GetBoolean(4),
                                new CBudgetDocEvent(rs.GetGuid(2), rs.GetString(5), rs.GetInt32(15)),
                                new CBudgetDocState(rs.GetGuid(0), rs.GetString(6), rs.GetInt32(16)),
                                new CBudgetDocState(rs.GetGuid(1), rs.GetString(7), rs.GetInt32(17)),
                                new CRoutePointGroup(rs.GetGuid(8), rs.GetString(9)), rs.GetBoolean(10),
                                new CUser(rs.GetInt32(11), rs.GetInt32(12), rs.GetString(13), rs.GetString(14))));
                        }

                    }

                    bRet = true;
                }
                else
                {
                    DevExpress.XtraEditors.XtraMessageBox.Show(
                    "Не удалось получить список точек маршрута для бюджетного документа.\nУИ бюджетного документа : " +
                    this.m_uuidBudgetDocID.ToString() + "\nВ БД не найдена информация.", "Внимание",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);

                }
                rs.Close();
                rs.Dispose();
                cmd.Dispose();
            }
            catch (System.Exception f)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(
                "Не удалось загрузить список точек маршрута для бюджетного документа.\n\nТекст ошибки: " + f.Message, "Внимание",
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
        /// Инициализация свойств класса
        /// </summary>
        /// <param name="objProfile">профайл</param>
        /// <param name="uuidID">уникальный идентификатор бюджетного документа</param>
        /// <returns>true - успешная инициализация; false - ошибка</returns>
        public override System.Boolean Init( UniXP.Common.CProfile objProfile, System.Guid uuidID )
        {
            System.Boolean bRet = false;

            System.Data.SqlClient.SqlConnection DBConnection = objProfile.GetDBSource();
            if( DBConnection == null ) { return bRet; }

            try
            {
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
                cmd.Connection = DBConnection;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                if( this.m_uuidBudgetDocID.CompareTo( System.Guid.Empty ) == 0 )
                {
                    this.m_uuidBudgetDocID = uuidID;
                }
                // заполняем список точек маршрута
                bRet = InitRouteDeclaration( cmd, objProfile );
                cmd.Dispose();
            }
            catch( System.Exception e )
            {
                DevExpress.XtraEditors.XtraMessageBox.Show( 
                "Не удалось проинициализировать класс CBudgetRoute.\nТекст ошибки: " + e.Message, "Внимание",
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
        /// <returns>true - удачное завершение; false - ошибка</returns>
        public override System.Boolean Remove( UniXP.Common.CProfile objProfile )
        {
            System.Boolean bRet = false;
            // уникальный идентификатор бюджетного документа не должен быть пустым
            if( this.m_uuidBudgetDocID.CompareTo( System.Guid.Empty ) == 0 )
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(  "Недопустимое значение уникального идентификатора бюджетного документа.\nУИ : " + this.uuidID.ToString(), "Внимание",
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
                cmd.CommandText = System.String.Format( "[{0}].[dbo].[sp_DeleteBudgetDocRouteDeclaration]", objProfile.GetOptionsDllDBName() );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ( ( System.Byte )( 0 ) ), ( ( System.Byte )( 0 ) ), "", System.Data.DataRowVersion.Current, null ) );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@BUDGETDOC_GUID_ID", System.Data.SqlDbType.UniqueIdentifier ) );
                cmd.Parameters[ "@BUDGETDOC_GUID_ID" ].Value = this.m_uuidBudgetDocID;
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
                            DevExpress.XtraEditors.XtraMessageBox.Show(  "Бюджетный документ с заданным идентификатором не найден.\nУИ : " + 
                                this.m_uuidBudgetDocID.ToString(), "Внимание",
                                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning );
                            break;
                        }
                        default:
                        {
                            DevExpress.XtraEditors.XtraMessageBox.Show(  "Ошибка выполнения запроса на удаление маршрута бюджетного документа.\nУИ : " + 
                                this.m_uuidBudgetDocID.ToString(), "Внимание",
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
                "Ошибка выполнения запроса на удаление маршрута бюджетного документа.\nУИ : " + 
                this.m_uuidBudgetDocID.ToString() + "\n" + e.Message, "Внимание",
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
        /// Добавить информацию о маршруте бюджетного документа в БД
        /// </summary>
        /// <param name="objProfile">профайл</param>
        /// <returns>true - удачное завершение; false - ошибка</returns>
        public override System.Boolean Add( UniXP.Common.CProfile objProfile )
        {
            System.Boolean bRet = false;

            // уникальный идентификатор бюджетного документа не должен быть пустым
            if( this.m_uuidBudgetDocID.CompareTo( System.Guid.Empty ) == 0 )
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(  "Недопустимое значение уникального идентификатора бюджетного документа.\nУИ : " + 
                    this.uuidID.ToString(), "Внимание",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
                return bRet;
            }
            // проверим не пустая ли структура шаблона
            if( this.m_RoutePointList.Count == 0 )
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(  "У маршрута отсутствует структура!", "Внимание",
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
                // сохраняем структуру шаблона маршрута
                bRet = this.SaveRouteDeclaration( cmd, objProfile );
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

                cmd.Dispose();
            }
            catch( System.Exception e )
            {
                // откатываем транзакцию
                DBTransaction.Rollback();
                DevExpress.XtraEditors.XtraMessageBox.Show( 
                "Не удалось создать маршрут для бюджетного документа.\nУИ бюджетного документа : " + 
                this.m_uuidBudgetDocID.ToString() + "\nТекст ошибки: " + e.Message, "Внимание",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
            }
			finally // очищаем занимаемые ресурсы
            {
                DBConnection.Close();
            }
            return bRet;
        }

        /// <summary>
        /// Добавить информацию о маршруте бюджетного документа в БД
        /// </summary>
        /// <param name="objProfile">профайл</param>
        /// <param name="cmd">SQL - команда</param>
        /// <returns>true - удачное завершение; false - ошибка</returns>
        public System.Boolean Add( UniXP.Common.CProfile objProfile,
            System.Data.SqlClient.SqlCommand cmd )
        {
            System.Boolean bRet = false;
            if( cmd == null ) { return bRet; }

            // уникальный идентификатор бюджетного документа не должен быть пустым
            if( this.m_uuidBudgetDocID.CompareTo( System.Guid.Empty ) == 0 )
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(  "Недопустимое значение уникального идентификатора бюджетного документа.\nУИ : " + 
                    this.uuidID.ToString(), "Внимание",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
                return bRet;
            }
            // проверим не пустая ли структура шаблона
            if( this.m_RoutePointList.Count == 0 )
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(  "У маршрута отсутствует структура!", "Внимание",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
                return bRet;
            }

            try
            {
                // сохраняем структуру шаблона маршрута
                bRet = this.SaveRouteDeclaration( cmd, objProfile );
            }
            catch( System.Exception e )
            {
                DevExpress.XtraEditors.XtraMessageBox.Show( 
                "Не удалось создать маршрут для бюджетного документа.\nУИ бюджетного документа : " + 
                this.m_uuidBudgetDocID.ToString() + "\nТекст ошибки: " + e.Message, "Внимание",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
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
        public override System.Boolean Update( UniXP.Common.CProfile objProfile )
        {
            System.Boolean bRet = false;

            // уникальный идентификатор бюджетного документа не должен быть пустым
            if( this.m_uuidBudgetDocID.CompareTo( System.Guid.Empty ) == 0 )
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(  "Недопустимое значение уникального идентификатора бюджетного документа.\nУИ : " + 
                    this.m_uuidBudgetDocID.ToString(), "Внимание",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
                return bRet;
            }
            // проверим не пустая ли структура шаблона
            if( this.m_RoutePointList.Count == 0 )
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(  "У маршрута отсутствует структура!", "Внимание",
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
                // сохраняем структуру шаблона
                bRet = this.DeleteRouteDeclaration( cmd, objProfile );
                if( bRet )
                {
                    // сохраняем структуру шаблона маршрута
                    bRet = this.SaveRouteDeclaration( cmd, objProfile );
                }
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
                cmd.Dispose();
            }
            catch( System.Exception e )
            {
                // откатываем транзакцию
                DBTransaction.Rollback();
                DevExpress.XtraEditors.XtraMessageBox.Show( 
                "Ошибка изменения свойств маршрута бюджетного документа.\nУИ бюджетного документа: " + 
                this.m_uuidBudgetDocID.ToString() + "\nТекст ошибки: " + e.Message, "Внимание",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
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
        /// <param name="cmd">SQL - команда</param>
        /// <returns>true - удачное завершение; false - ошибка</returns>
        public System.Boolean Update( UniXP.Common.CProfile objProfile,
            System.Data.SqlClient.SqlCommand cmd )
        {
            System.Boolean bRet = false;
            if( cmd == null ) { return bRet; }

            // уникальный идентификатор бюджетного документа не должен быть пустым
            if( this.m_uuidBudgetDocID.CompareTo( System.Guid.Empty ) == 0 )
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(  "Недопустимое значение уникального идентификатора бюджетного документа.\nУИ : " + 
                    this.m_uuidBudgetDocID.ToString(), "Внимание",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
                return bRet;
            }
            // проверим не пустая ли структура шаблона
            if( this.m_RoutePointList.Count == 0 )
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(  "У маршрута отсутствует структура!", "Внимание",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
                return bRet;
            }

            try
            {
                // удаляем структуру шаблона
                bRet = this.DeleteRouteDeclaration( cmd, objProfile );
                if( bRet )
                {
                    // сохраняем структуру шаблона маршрута
                    bRet = this.SaveRouteDeclaration( cmd, objProfile );
                }
            }
            catch( System.Exception e )
            {
                // откатываем транзакцию
                DevExpress.XtraEditors.XtraMessageBox.Show( 
                "Ошибка изменения свойств маршрута бюджетного документа.\nУИ бюджетного документа: " + 
                this.m_uuidBudgetDocID.ToString() + "\nТекст ошибки: " + e.Message, "Внимание",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
            }
			finally // очищаем занимаемые ресурсы
            {
            }
            return bRet;
        }

        /// <summary>
        /// Удаляет маршрут бюджетного документа
        /// </summary>
        /// <param name="cmd">SQL-команда</param>
        /// <param name="objProfile">профайл</param>
        /// <returns>true - удачное завершение; false - ошибка</returns>
        private System.Boolean DeleteRouteDeclaration( System.Data.SqlClient.SqlCommand cmd,
            UniXP.Common.CProfile objProfile )
        {
            System.Boolean bRet = false;
            if( ( cmd == null ) || ( cmd.Connection == null ) ) { return bRet; }
            // уникальный идентификатор не должен быть пустым
            // уникальный идентификатор бюджетного документа не должен быть пустым
            if( this.m_uuidBudgetDocID.CompareTo( System.Guid.Empty ) == 0 )
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(  "Недопустимое значение уникального идентификатора бюджетного документа.\nУИ : " + 
                    this.m_uuidBudgetDocID.ToString(), "Внимание",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
                return bRet;
            }

            try
            {
                cmd.Parameters.Clear();
                cmd.CommandText = System.String.Format( "[{0}].[dbo].[sp_DeleteBudgetDocRouteDeclaration]", objProfile.GetOptionsDllDBName() );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ( ( System.Byte )( 0 ) ), ( ( System.Byte )( 0 ) ), "", System.Data.DataRowVersion.Current, null ) );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@BUDGETDOC_GUID_ID", System.Data.SqlDbType.UniqueIdentifier ) );

                cmd.Parameters[ "@BUDGETDOC_GUID_ID" ].Value = this.m_uuidBudgetDocID;
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
                        {
                            DevExpress.XtraEditors.XtraMessageBox.Show(  "Бюджетный документ с заданным идентификатором не найден\nУИ бюджетного документа : " + 
                                this.m_uuidBudgetDocID.ToString(), "Внимание",
                                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
                            break;
                        }
                        default:
                        {
                            DevExpress.XtraEditors.XtraMessageBox.Show(  "Ошибка удаления структуры маршрута.\nУИ бюджетного документа : " + 
                                this.m_uuidBudgetDocID.ToString(), "Внимание",
                                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
                            break;
                        }
                    }
                }
            }
            catch( System.Exception e )
            {
                DevExpress.XtraEditors.XtraMessageBox.Show( 
                "Ошибка удаления структуры маршрута.\nУИ бюджетного документа : " + 
                this.m_uuidBudgetDocID.ToString() + "\nТекст ошибки: " + e.Message, "Внимание",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
            }
			finally // очищаем занимаемые ресурсы
            {
            }
            return bRet;
        }
        /// <summary>
        /// Сохраняет в БД структуру маршрута бюджетного документа
        /// </summary>
        /// <param name="cmd">SQL - команда</param>
        /// <param name="objProfile">профайл</param>
        /// <returns>true - удачное завершение; false - ошибка</returns>
        private System.Boolean SaveRouteDeclaration( System.Data.SqlClient.SqlCommand cmd,
            UniXP.Common.CProfile objProfile )
        {
            System.Boolean bRet = false;
            if( cmd == null ) { return bRet; }
            if( this.m_RoutePointList.Count == 0 ) { return true; }

            // уникальный идентификатор бюджетного документа не должен быть пустым
            if( this.m_uuidBudgetDocID == System.Guid.Empty )
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(  "Недопустимое значение уникального идентификатора бюджетного документа", "Внимание",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
                return bRet;
            }

            try
            {
                // пробегаем по структуре маршрута и сохраняем ее в бд 
                bRet = true;
                for( System.Int32 i = 0; i < this.m_RoutePointList.Count; i++ )
                {
                    if( this.m_RoutePointList[ i ].AddBudgetDocRouteItem( objProfile, this.m_uuidBudgetDocID, cmd ) == false )
                    {
                        bRet = false;
                        break;
                    }
                }
            }
            catch( System.Exception e )
            {
                DevExpress.XtraEditors.XtraMessageBox.Show( 
                "Не удалось сохранить пункты маршрута.\nТекст ошибки: " + e.Message, "Внимание",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
            }
			finally // очищаем занимаемые ресурсы
            {
            }
            return bRet;
        }


        #endregion
    }
}
