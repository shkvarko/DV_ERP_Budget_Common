using System;
using System.Collections.Generic;
using System.Text;

namespace ERP_Budget.Common
{
    /// <summary>
    /// Класс "Динамическое право"
    /// </summary>
    public class CDynamicRight : IBaseListItem
    {
        #region Переменные, Свойства, Константы
        /// <summary>
        /// Уникальный идентификатор
        /// </summary>
        private int m_iID;
        /// <summary>
        /// Роль
        /// </summary>
        private string m_strRole;
        /// <summary>
        /// Описание
        /// </summary>
        private string m_strDescription;

        /// <summary>
        /// Уникальный идентификатор
        /// </summary>
        public int ID
        {
            get
            {
                return m_iID;
            }
            set
            {
                m_iID = value;
            }
        }

        /// <summary>
        /// Роль
        /// </summary>
        public string Role
        {
            get
            {
                return m_strRole;
            }
            set
            {
                m_strRole = value;
            }
        }

        /// <summary>
        /// Описание
        /// </summary>
        public string Description
        {
            get
            {
                return m_strDescription;
            }
            set
            {
                m_strDescription = value;
            }
        }
        /// <summary>
        /// состояние
        /// </summary>
        public System.Boolean IsEnable { get; set; }
        #endregion

        public CDynamicRight()
        {
            m_iID = 0;
            m_strRole = "";
            m_strDescription = "";
            IsEnable = false;
        }

        public CDynamicRight( System.Int32 iID, System.String strName, System.String strRole, System.String strDescription )
        {
            this.m_iID = iID;
            this.m_strName = strName;
            this.m_strRole = strRole;
            this.m_strDescription = strDescription;
            IsEnable = false;
        }

        /// <summary>
        /// Возвращает список классов CDynamicRight
        /// </summary>
        /// <param name="objProfile">профайл</param>
        /// <param name="iUserID">идентификатор пользователя</param>
        /// <returns>true - успешная инициализация; false - ошибка</returns>
        public CBaseList<CDynamicRight> GetDynamicRightsList(UniXP.Common.CProfile objProfile, 
            System.Int32 iUserID )
        {
            CBaseList<CDynamicRight> objList = new CBaseList<CDynamicRight>();

            System.Data.SqlClient.SqlConnection DBConnection = objProfile.GetDBSource();
            if( DBConnection == null ) { return objList; }

            try
            {
                // соединение с БД получено, прописываем команду на выборку данных
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
                cmd.Connection = DBConnection;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = System.String.Format( "[{0}].[dbo].[sp_GetUserRights]", objProfile.GetOptionsDllDBName() );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ( ( System.Byte )( 0 ) ), ( ( System.Byte )( 0 ) ), "", System.Data.DataRowVersion.Current, null ) );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@ulUserID", System.Data.SqlDbType.Int, 4 ) );
                cmd.Parameters[ "@ulUserID" ].Value = iUserID;
                System.Data.SqlClient.SqlDataReader rs = cmd.ExecuteReader();
                if( rs.HasRows )
                {
                    // набор данных непустой
                    CDynamicRight objDynamicRight = null;
                    while( rs.Read() )
                    {
                        objDynamicRight = new CDynamicRight();
                        objDynamicRight.m_strName = rs.GetString( 4 );
                        objDynamicRight.m_strDescription = rs.GetString( 5 );
                        objDynamicRight.m_iID = rs.GetInt32( 6 );
                        objDynamicRight.m_strRole = rs.GetString( 7 );
                        objDynamicRight.IsEnable = System.Convert.ToBoolean(rs["bState"]);
                        objList.AddItemToList( objDynamicRight );
                    }
                }
                else
                {
                    //DevExpress.XtraEditors.XtraMessageBox.Show( 
                    //"Не удалось получить список классов CDynamicRight.\nВ БД не найдена информация.", "Внимание",
                    //System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning );

                }
                cmd.Dispose();
                rs.Dispose();
            }
            catch( System.Exception e )
            {
                DevExpress.XtraEditors.XtraMessageBox.Show( 
                "Не удалось получить список классов CDynamicRight.\n" + e.Message, "Внимание",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
            }
            finally // очищаем занимаемые ресурсы
            {
                DBConnection.Close();
            }
            return objList;
        }
        /// <summary>
        /// Возвращает список динамических прав пользователя в системе "Бюджет" (CDynamicRight)
        /// </summary>
        /// <param name="objProfile">профайл</param>
        /// <param name="iUserID">уи пользователя в системе "Бюджет"</param>
        /// <param name="strErr">сообщение об ошибке</param>
        /// <returns>список динамических прав пользователя в системе "Бюджет"</returns>
        public static List<CDynamicRight> GetDynamicRightsList(UniXP.Common.CProfile objProfile,
            System.Int32 iUserID, ref System.String strErr)
        {
            List<CDynamicRight> objList = new List<CDynamicRight>();

            System.Data.SqlClient.SqlConnection DBConnection = objProfile.GetDBSource();
            if (DBConnection == null) { return objList; }

            try
            {
                // соединение с БД получено, прописываем команду на выборку данных
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
                cmd.Connection = DBConnection;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = System.String.Format("[{0}].[dbo].[sp_GetUserRights]", objProfile.GetOptionsDllDBName());
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ((System.Byte)(0)), ((System.Byte)(0)), "", System.Data.DataRowVersion.Current, null));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ulUserID", System.Data.SqlDbType.Int, 4));
                cmd.Parameters["@ulUserID"].Value = iUserID;
                System.Data.SqlClient.SqlDataReader rs = cmd.ExecuteReader();
                if (rs.HasRows)
                {
                    // набор данных непустой
                    CDynamicRight objDynamicRight = null;
                    while (rs.Read())
                    {
                        objDynamicRight = new CDynamicRight();
                        objDynamicRight.m_strName = System.Convert.ToString( rs["strName"] );
                        objDynamicRight.m_strDescription = ((rs["strDescription"] != System.DBNull.Value) ? System.Convert.ToString(rs["strDescription"]) : System.String.Empty);
                        objDynamicRight.m_iID = System.Convert.ToInt32( rs["iID"] );
                        objDynamicRight.m_strRole = System.Convert.ToString(rs["strRole"]);
                        objDynamicRight.IsEnable = System.Convert.ToBoolean(rs["bState"]);
                        objList.Add(objDynamicRight);
                    }
                }
                cmd.Dispose();
                rs.Dispose();
            }
            catch (System.Exception e)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(
                "Не удалось получить список классов CDynamicRight.\n" + e.Message, "Внимание",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
            finally // очищаем занимаемые ресурсы
            {
                DBConnection.Close();
            }
            return objList;
        }
        /// <summary>
        /// Возвращает список классов CDynamicRight
        /// </summary>
        /// <param name="objProfile">профайл</param>
        /// <returns>true - успешная инициализация; false - ошибка</returns>
        public static System.Collections.Generic.List<CDynamicRight> GetDynamicRightsList( UniXP.Common.CProfile objProfile )
        {
            System.Collections.Generic.List<CDynamicRight> objList = new System.Collections.Generic.List<CDynamicRight>();

            System.Data.SqlClient.SqlConnection DBConnection = objProfile.GetDBSource();
            if( DBConnection == null ) { return objList; }

            try
            {
                // соединение с БД получено, прописываем команду на выборку данных
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
                cmd.Connection = DBConnection;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = System.String.Format( "[{0}].[dbo].[sp_GetRight]", objProfile.GetOptionsDllDBName() );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ( ( System.Byte )( 0 ) ), ( ( System.Byte )( 0 ) ), "", System.Data.DataRowVersion.Current, null ) );
                System.Data.SqlClient.SqlDataReader rs = cmd.ExecuteReader();
                if( rs.HasRows )
                {
                    // набор данных непустой
                    System.String strDescription = "";
                    while( rs.Read() )
                    {
                        strDescription = ( rs[ 2 ] == System.DBNull.Value ) ? "" : rs.GetString( 2 );
                        objList.Add( new CDynamicRight( rs.GetInt32( 0 ), rs.GetString( 1 ), strDescription, rs.GetString( 3 ) ) );
                    }
                }
                cmd.Dispose();
                rs.Dispose();
            }
            catch( System.Exception e )
            {
                DevExpress.XtraEditors.XtraMessageBox.Show( 
                "Не удалось получить список классов CDynamicRight.\nТекст ошибки: " + e.Message, "Внимание",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
            }
            finally // очищаем занимаемые ресурсы
            {
                DBConnection.Close();
            }
            return objList;
        }

        /// <summary>
        /// Возвращает главное динамическое право из списка доступных
        /// </summary>
        /// <param name="objProfile">профайл</param>
        /// <returns>наименование динамического права</returns>
        public static System.String GetMainDynamicRight( UniXP.Common.CProfile objProfile, CBudgetDep objBudgetDep )
        {
            System.String strDRName = "";
            try
            {
                // динамическое право "Согласователь бюджета"
                if( objProfile.GetClientsRight().GetState( ERP_Budget.Global.Consts.strDRCoordinator ) )
                {
                    strDRName = ERP_Budget.Global.Consts.strDRCoordinator;
                    return strDRName;
                }
                // динамическое право "Распорядитель бюджета"
                // оно конечно так... но есь нюанс!
                // для одного подразделения он может и распорядитель, а для другого простой инициатор
                if( objProfile.GetClientsRight().GetState( ERP_Budget.Global.Consts.strDRManager ) )
                {
                    if( objBudgetDep == null )
                    {
                        strDRName = ERP_Budget.Global.Consts.strDRManager;
                    }
                    else
                    {
                        if( objBudgetDep.IsBudgetDepManager( objProfile.m_nSQLUserID ) == true )
                        {
                            strDRName = ERP_Budget.Global.Consts.strDRManager;
                        }
                        else
                        {
                            strDRName = ERP_Budget.Global.Consts.strDRInitiator;
                        }
                    }
                    return strDRName;
                }
                // динамическое право "Контролер"
                if( objProfile.GetClientsRight().GetState( ERP_Budget.Global.Consts.strDRInspector ) )
                {
                    strDRName = ERP_Budget.Global.Consts.strDRInspector;
                    return strDRName;
                }
                // динамическое право "Бухгалтер"
                if (objProfile.GetClientsRight().GetState(ERP_Budget.Global.Consts.strDRAccountant))
                {
                    strDRName = ERP_Budget.Global.Consts.strDRAccountant;
                    return strDRName;
                }
                // динамическое право "Кассир"
                if( objProfile.GetClientsRight().GetState( ERP_Budget.Global.Consts.strDRCashier ) )
                {
                    strDRName = ERP_Budget.Global.Consts.strDRCashier;
                    return strDRName;
                }
                // динамическое право "Инициатор"
                if( objProfile.GetClientsRight().GetState( ERP_Budget.Global.Consts.strDRInitiator ) )
                {
                    strDRName = ERP_Budget.Global.Consts.strDRInitiator;
                    return strDRName;
                }
                // динамическое право "Просмотрщик"
                if( objProfile.GetClientsRight().GetState( ERP_Budget.Global.Consts.strDRLook ) )
                {
                    strDRName = ERP_Budget.Global.Consts.strDRLook;
                    return strDRName;
                }
            }
            catch( System.Exception f )
            {
                DevExpress.XtraEditors.XtraMessageBox.Show( 
                "Ошибка поиска динамического права.\n\nТекст ошибки: " + f.Message, "Внимание",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
            }

            return strDRName;
        }
        /// <summary>
        /// Возвращает динамическое право по его наименованию
        /// </summary>
        /// <param name="objProfile">профайл</param>
        /// <param name="strDRName">наименование динамического права</param>
        /// <returns>объект класса "Динамическое право"</returns>
        public static CDynamicRight Init( UniXP.Common.CProfile objProfile, System.String strDRName )
        {
            CDynamicRight objDynamicRight = null;
            System.Data.SqlClient.SqlConnection DBConnection = objProfile.GetDBSource();
            if( DBConnection == null ) { return objDynamicRight; }

            try
            {
                // соединение с БД получено, прописываем команду на выборку данных
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
                cmd.Connection = DBConnection;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = System.String.Format( "[{0}].[dbo].[sp_GetRightByName]", objProfile.GetOptionsDllDBName() );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ( ( System.Byte )( 0 ) ), ( ( System.Byte )( 0 ) ), "", System.Data.DataRowVersion.Current, null ) );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@RIGHT_NAME", System.Data.SqlDbType.VarChar, 50 ) );
                cmd.Parameters[ "@RIGHT_NAME" ].Value = strDRName;
                System.Data.SqlClient.SqlDataReader rs = cmd.ExecuteReader();
                if( rs.HasRows )
                {
                    // набор данных непустой
                    rs.Read();
                    System.String strDescription = ( rs[ 2 ] == System.DBNull.Value ) ? "" : rs.GetString( 2 );
                    objDynamicRight = new CDynamicRight( rs.GetInt32( 0 ), rs.GetString( 1 ), strDescription, rs.GetString( 3 ) );
                }
                else
                {
                    DevExpress.XtraEditors.XtraMessageBox.Show( 
                    "Не удалось получить информацию о динамическом праве: " + strDRName, "Внимание",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
                }
                cmd.Dispose();
                rs.Dispose();
            }
            catch( System.Exception e )
            {
                DevExpress.XtraEditors.XtraMessageBox.Show( 
                "Не удалось получить информацию о динамическом праве: " + strDRName + "\n\nТекст ошибки: " + e.Message, "Внимание",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
            }
            finally // очищаем занимаемые ресурсы
            {
                DBConnection.Close();
            }
            return objDynamicRight;
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

            return bRet;
        }

        /// <summary>
        /// Удалить запись из БД
        /// </summary>
        /// <param name="objProfile">профайл</param>
        /// <param name="uuidID">уникальный идентификатор объекта</param>
        /// <returns>true - удачное завершение; false - ошибка</returns>
        public override System.Boolean Remove( UniXP.Common.CProfile objProfile )
        {
            System.Boolean bRet = false;
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

            return bRet;
        }

        /// <summary>
        /// Сохранить изменения в БД
        /// </summary>
        /// <param name="objProfile">профайл</param>
        /// <returns>true - удачное завершение; false - ошибка</returns>
        public override System.Boolean Update( UniXP.Common.CProfile objProfile )
        {
            System.Boolean bRet = false;

            return bRet;
        }

    }
}
