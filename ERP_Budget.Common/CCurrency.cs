using System;
using System.Collections.Generic;
using System.Text;

namespace ERP_Budget.Common
{
    /// <summary>
    /// Класс "Валюта"
    /// </summary>
    public class CCurrency : IBaseListItem
    {
        #region Переменные, Свойства, Константы
        /// <summary>
        /// Код валюты
        /// </summary>
        private string m_strCurrencyCode;
        /// <summary>
        /// Международный код валюты
        /// </summary>
        private int m_iCurrencyID;
        /// <summary>
        /// Описание
        /// </summary>
        private string m_strDescription;
        /// <summary>
        /// Признак "Основной"
        /// </summary>
        private bool m_bIsMain;
        /// <summary>
        /// Признак "Активен"
        /// </summary>
        private bool m_bIsActive;
        /// <summary>
        /// "Родительская" валюта
        /// </summary>
        private string m_strParentCurrencyCode;
        /// <summary>
        /// Коэффициент пересчета к "родительской" валюте
        /// </summary>
        private System.Double m_moneyParentCoeff;
        /// <summary>
        /// Кратность валюты
        /// </summary>
        private int m_iDivisible;
        /// <summary>
        /// Код валюты
        /// </summary>
        public string CurrencyCode
        {
            get
            {
                return m_strCurrencyCode;
            }
            set
            {
                m_strCurrencyCode = value;
            }
        }
        /// <summary>
        /// Международный код валюты
        /// </summary>
        public int ID
        {
            get
            {
                return m_iCurrencyID;
            }
            set
            {
                m_iCurrencyID = value;
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
        /// Признак "Основной"
        /// </summary>
        public bool IsMain
        {
            get
            {
                return m_bIsMain;
            }
            set
            {
                m_bIsMain = value;
            }
        }
        /// <summary>
        /// Признак "Активен"
        /// </summary>
        public bool IsActive
        {
            get
            {
                return m_bIsActive;
            }
            set
            {
                m_bIsActive = value;
            }
        }
        /// <summary>
        /// "Родительская" валюта
        /// </summary>
        /// <remarks>Если курс данной валюты привязан к другой, то в этом случае в этом поле содержится код родительскй валюты</remarks>
        public System.String ParentCurrencyCode
        {
            get
            {
                return m_strParentCurrencyCode;
            }
            set
            {
                m_strParentCurrencyCode = value;
            }
        }
        /// <summary>
        /// Коэффициент пересчета к "родительской" валюте
        /// </summary>
        /// <remarks>В случае присутствия кода родительской валюты, это поле содержит коэффициент, на основании которого вычисляется курс</remarks>
        public System.Double ParentCoeff
        {
            get
            {
                return m_moneyParentCoeff;
            }
            set
            {
                m_moneyParentCoeff = value;
            }
        }
        /// <summary>
        /// Кратность валюты
        /// </summary>
        /// <remarks>Кратность валюты, т.е. для какой величины устанавливается курс</remarks>
        public int Divisible
        {
            get
            {
                return m_iDivisible;
            }
            set
            {
                m_iDivisible = value;
            }
        }
        #endregion

        #region Конструкторы
        public CCurrency()
        {
            this.m_uuidID = System.Guid.Empty;
            this.m_strName = "";
            this.m_strParentCurrencyCode = "";
            this.m_bIsActive = false;
            this.m_bIsMain = false;
            this.m_iCurrencyID = 0;
            this.m_iDivisible = 0;
            this.m_moneyParentCoeff = 0;
            this.m_strCurrencyCode = "";
            this.m_strDescription = "";
        }
        public CCurrency( System.Guid uuidID, System.String strName )
        {
            this.m_uuidID = uuidID;
            this.m_strName = strName;
            this.m_strParentCurrencyCode = "";
            this.m_bIsActive = false;
            this.m_bIsMain = false;
            this.m_iCurrencyID = 0;
            this.m_iDivisible = 0;
            this.m_moneyParentCoeff = 0;
            this.m_strCurrencyCode = "";
            this.m_strDescription = "";
        }
        public CCurrency( System.Guid uuidID, System.String strCurrencyCode, System.String strName )
        {
            this.m_uuidID = uuidID;
            this.m_strName = strName;
            this.m_strParentCurrencyCode = "";
            this.m_bIsActive = false;
            this.m_bIsMain = false;
            this.m_iCurrencyID = 0;
            this.m_iDivisible = 0;
            this.m_moneyParentCoeff = 0;
            this.m_strCurrencyCode = strCurrencyCode;
            this.m_strDescription = "";
        }
        public CCurrency( System.Guid uuidID, System.String strName, System.Boolean bIsActive,
            System.Boolean bIsMain, System.Int32 iCurrencyID, System.Int32 iDivisible,
            System.Double moneyParentCoeff, System.String strCurrencyCode )
        {
            this.m_uuidID = uuidID;
            this.m_strName = strName;
            this.m_strParentCurrencyCode = "";
            this.m_bIsActive = bIsActive;
            this.m_bIsMain = bIsMain;
            this.m_iCurrencyID = iCurrencyID;
            this.m_iDivisible = iDivisible;
            this.m_moneyParentCoeff = moneyParentCoeff;
            this.m_strCurrencyCode = strCurrencyCode;
            this.m_strDescription = "";
        }
        public CCurrency( System.Guid CurrencyGuidID, UniXP.Common.CProfile objProfile )
        {
            this.m_uuidID = CurrencyGuidID;
            if( this.m_uuidID.CompareTo( System.Guid.Empty ) != 0 )
            {
                this.Init( objProfile, CurrencyGuidID );
            }
        }
        #endregion

        #region Список валют
        /// <summary>
        /// Возвращает список валют
        /// </summary>
        /// <param name="objProfile">профайл</param>
        /// <returns>список валют</returns>
        public CBaseList<CCurrency> GetCurrencyList( UniXP.Common.CProfile objProfile, 
            System.Boolean bAllList, System.Guid uuidDefValue )
        {
            CBaseList<CCurrency> objList = new CBaseList<CCurrency>();

            System.Data.SqlClient.SqlConnection DBConnection = objProfile.GetDBSource();
            if( DBConnection == null ) { return objList; }

            try
            {
                // соединение с БД получено, прописываем команду на выборку данных
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
                cmd.Connection = DBConnection;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = System.String.Format( "[{0}].[dbo].[sp_GetCurrency]", objProfile.GetOptionsDllDBName() );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ( ( System.Byte )( 0 ) ), ( ( System.Byte )( 0 ) ), "", System.Data.DataRowVersion.Current, null ) );
                System.Data.SqlClient.SqlDataReader rs = cmd.ExecuteReader();
                if( rs.HasRows )
                {
                    // набор данных непустой
                    CCurrency objCurrency = null;
                    while( rs.Read() )
                    {
                        objCurrency = new CCurrency();
                        objCurrency.m_uuidID = rs.GetGuid( 0 );
                        objCurrency.m_strCurrencyCode = rs.GetString( 1 );
                        objCurrency.m_iCurrencyID = rs.GetInt32( 2 );
                        if( rs[ 3 ] != System.DBNull.Value )
                        { objCurrency.m_strDescription = rs.GetString( 3 ); }
                        objCurrency.m_strName = rs.GetString( 4 );
                        objCurrency.m_bIsMain = rs.GetBoolean( 5 );
                        objCurrency.m_bIsActive = !rs.GetBoolean( 6 );
                        if( rs[ 7 ] != System.DBNull.Value )
                        { objCurrency.m_strParentCurrencyCode = rs.GetString( 7 ); }
                        objCurrency.m_moneyParentCoeff = ( System.Double )rs.GetSqlMoney( 8 ).Value;
                        objCurrency.m_iDivisible = rs.GetInt32( 9 );
                        objList.AddItemToList( objCurrency );
                    }
                    // устанавливаем значение по-умолчанию
                    if( ( objList.GetCountItems() > 0 ) && ( uuidDefValue.CompareTo( System.Guid.Empty ) != 0 ) )
                    { objList.SetDefaultValue( uuidDefValue ); }
                }
                else
                {
                    //DevExpress.XtraEditors.XtraMessageBox.Show( 
                    //"Не удалось получить список валют.\nВ БД не найдена информация.", "Внимание",
                    //System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning );

                }
                cmd.Dispose();
                rs.Dispose();
            }
            catch( System.Exception e )
            {
                DevExpress.XtraEditors.XtraMessageBox.Show( 
                "Не удалось получить список валют.\n" + e.Message, "Внимание",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
            }
			finally // очищаем занимаемые ресурсы
            {
                DBConnection.Close();
            }
            return objList;
        }

        /// <summary>
        /// Возвращает список валют
        /// </summary>
        /// <param name="objProfile">профайл</param>
        /// <returns>список валют</returns>
        public static List<CCurrency> GetCurrencyList( UniXP.Common.CProfile objProfile )
        {
            List<CCurrency> objList = new List<CCurrency>();

            System.Data.SqlClient.SqlConnection DBConnection = objProfile.GetDBSource();
            if( DBConnection == null ) { return objList; }

            try
            {
                // соединение с БД получено, прописываем команду на выборку данных
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
                cmd.Connection = DBConnection;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = System.String.Format( "[{0}].[dbo].[sp_GetCurrency]", objProfile.GetOptionsDllDBName() );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ( ( System.Byte )( 0 ) ), ( ( System.Byte )( 0 ) ), "", System.Data.DataRowVersion.Current, null ) );
                System.Data.SqlClient.SqlDataReader rs = cmd.ExecuteReader();
                if( rs.HasRows )
                {
                    // набор данных непустой
                    while( rs.Read() )
                    {
                        CCurrency objCurrency = new CCurrency();
                        objCurrency.m_uuidID = rs.GetGuid( 0 );
                        objCurrency.m_strCurrencyCode = rs.GetString( 1 );
                        objCurrency.m_iCurrencyID = rs.GetInt32( 2 );
                        if( rs[ 3 ] != System.DBNull.Value )
                        { objCurrency.m_strDescription = rs.GetString( 3 ); }
                        objCurrency.m_strName = rs.GetString( 4 );
                        objCurrency.m_bIsMain = rs.GetBoolean( 5 );
                        objCurrency.m_bIsActive = !rs.GetBoolean( 6 );
                        if( rs[ 7 ] != System.DBNull.Value )
                        { objCurrency.m_strParentCurrencyCode = rs.GetString( 7 ); }
                        objCurrency.m_moneyParentCoeff = ( System.Double )rs.GetSqlMoney( 8 ).Value;
                        objCurrency.m_iDivisible = rs.GetInt32( 9 );
                        objList.Add( objCurrency );
                    }
                }
                cmd.Dispose();
                rs.Dispose();
            }
            catch( System.Exception e )
            {
                DevExpress.XtraEditors.XtraMessageBox.Show( 
                "Не удалось получить список валют.\n" + e.Message, "Внимание",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
            }
			finally // очищаем занимаемые ресурсы
            {
                DBConnection.Close();
            }
            return objList;
        }

        /// <summary>
        /// Обновляет список валют
        /// </summary>
        /// <param name="objProfile">профайл</param>
        /// <param name="cmd">SQL - команда</param>
        /// <param name="cmd">objList - обновляемый список</param>
        /// <returns></returns>
        public static void RefreshCurrencyList( UniXP.Common.CProfile objProfile,
            System.Data.SqlClient.SqlCommand cmd, System.Collections.Generic.List<CCurrency> objList )
        {
            if( cmd == null ) { return; }
            objList.Clear();

            try
            {
                cmd.Parameters.Clear();
                cmd.CommandText = System.String.Format( "[{0}].[dbo].[sp_GetCurrency]", objProfile.GetOptionsDllDBName() );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ( ( System.Byte )( 0 ) ), ( ( System.Byte )( 0 ) ), "", System.Data.DataRowVersion.Current, null ) );
                System.Data.SqlClient.SqlDataReader rs = cmd.ExecuteReader();
                if( rs.HasRows )
                {
                    while( rs.Read() )
                    {
                        objList.Add( new CCurrency( rs.GetGuid( 0 ), rs.GetString( 4 ), ( !rs.GetBoolean( 6 ) ),
                            rs.GetBoolean( 5 ), rs.GetInt32( 2 ), rs.GetInt32( 9 ),
                            ( System.Double )rs.GetSqlMoney( 8 ).Value , rs.GetString( 1 ) ) );
                    }
                }
                rs.Close();
                rs.Dispose();
            }
            catch( System.Exception e )
            {
                DevExpress.XtraEditors.XtraMessageBox.Show( 
                "Не удалось получить список валют.\nТекст ошибки: " + e.Message, "Внимание",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
            }
			finally // очищаем занимаемые ресурсы
            {
            }
            return;
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
                cmd.CommandText = System.String.Format( "[{0}].[dbo].[sp_GetCurrency]", objProfile.GetOptionsDllDBName() );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ( ( System.Byte )( 0 ) ), ( ( System.Byte )( 0 ) ), "", System.Data.DataRowVersion.Current, null ) );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@GUID_ID", System.Data.SqlDbType.UniqueIdentifier ) );
                cmd.Parameters[ "@GUID_ID" ].Value = uuidID;
                System.Data.SqlClient.SqlDataReader rs = cmd.ExecuteReader();
                if( rs.HasRows )
                {
                    // набор данных непустой, в нем нас интересует одна запись
                    rs.Read();
                    this.m_uuidID = rs.GetGuid( 0 );
                    this.m_strCurrencyCode = rs.GetString( 1 );
                    this.m_iCurrencyID = rs.GetInt32( 2 );
                    if( rs[ 3 ] != System.DBNull.Value )
                    { this.m_strDescription = rs.GetString( 3 ); }
                    this.m_strName = rs.GetString( 4 );
                    this.m_bIsMain = rs.GetBoolean( 5 );
                    this.m_bIsActive = !rs.GetBoolean( 6 );
                    if( rs[ 7 ] != System.DBNull.Value )
                    { this.m_strParentCurrencyCode = rs.GetString( 7 ); }
                    this.m_moneyParentCoeff = ( System.Double )rs.GetSqlMoney( 8 ).Value;
                    this.m_iDivisible = rs.GetInt32( 9 );
                    bRet = true;
                }
                else
                {
                    DevExpress.XtraEditors.XtraMessageBox.Show( 
                    "Не удалось проинициализировать класс CCurrency.\nВ БД не найдена информация.", "Внимание",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning );

                }
                cmd.Dispose();
                rs.Dispose();
            }
            catch( System.Exception e )
            {
                DevExpress.XtraEditors.XtraMessageBox.Show( 
                "Не удалось проинициализировать класс CCurrency.\n" + e.Message, "Внимание",
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
                cmd.CommandText = System.String.Format( "[{0}].[dbo].[sp_DeleteCurrency]", objProfile.GetOptionsDllDBName() );
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
                        DevExpress.XtraEditors.XtraMessageBox.Show(  "На валюту есть ссылка в БД.\nУдаление невозможно", "Внимание",
                            System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning );
                    }
                    else
                    {
                        DevExpress.XtraEditors.XtraMessageBox.Show(  "Ошибка удаления валюты", "Ошибка",
                            System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
                    }
                }
                cmd.Dispose();
            }
            catch( System.Exception e )
            {
                DevExpress.XtraEditors.XtraMessageBox.Show( 
                "Не удалось удалить валюту.\n" + e.Message, "Внимание",
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
                DevExpress.XtraEditors.XtraMessageBox.Show(  "Недопустимое значение наименования объекта", "Внимание",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
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
                cmd.CommandText = System.String.Format( "[{0}].[dbo].[sp_AddCurrency]", objProfile.GetOptionsDllDBName() );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ( ( System.Byte )( 0 ) ), ( ( System.Byte )( 0 ) ), "", System.Data.DataRowVersion.Current, null ) );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@GUID_ID", System.Data.SqlDbType.UniqueIdentifier, 4, System.Data.ParameterDirection.Output, false, ( ( System.Byte )( 0 ) ), ( ( System.Byte )( 0 ) ), "", System.Data.DataRowVersion.Current, null ) );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@CURRENCY_CODE", System.Data.DbType.String ) );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@CURRENCY_ID", System.Data.SqlDbType.Int, 4 ) );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@CURRENCY_NAME", System.Data.DbType.String ) );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@CURRENCY_DESCRIPTION", System.Data.DbType.String ) );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@CURRENCY_MAIN", System.Data.SqlDbType.Bit, 1 ) );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@CURRENCY_NOTACTIVE", System.Data.SqlDbType.Bit, 1 ) );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@CURRENCY_PARENTCOEFF", System.Data.SqlDbType.Money, 8 ) );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@CURRENCY_DIVISIBLE", System.Data.SqlDbType.Int, 4 ) );

                cmd.Parameters[ "@CURRENCY_CODE" ].Value = this.m_strCurrencyCode;
                cmd.Parameters[ "@CURRENCY_ID" ].Value = this.m_iCurrencyID;
                cmd.Parameters[ "@CURRENCY_NAME" ].Value = this.m_strName;
                cmd.Parameters[ "@CURRENCY_DESCRIPTION" ].Value = this.m_strDescription;
                cmd.Parameters[ "@CURRENCY_MAIN" ].Value = this.m_bIsMain;
                cmd.Parameters[ "@CURRENCY_NOTACTIVE" ].Value = !( this.m_bIsActive );
                cmd.Parameters[ "@CURRENCY_PARENTCOEFF" ].Value = this.m_moneyParentCoeff;
                cmd.Parameters[ "@CURRENCY_DIVISIBLE" ].Value = this.m_iDivisible;
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
                        DevExpress.XtraEditors.XtraMessageBox.Show(  "Валюта '" + this.m_strCurrencyCode + "' уже есть в БД", "Внимание",
                            System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning );
                    }
                    else
                    {
                        DevExpress.XtraEditors.XtraMessageBox.Show(  "Ошибка создания валюты", "Ошибка",
                            System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
                    }
                }
                cmd.Dispose();
            }
            catch( System.Exception e )
            {
                DevExpress.XtraEditors.XtraMessageBox.Show( 
                "Не удалось создать валюту.\n" + e.Message, "Внимание",
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
            // наименование не должно быть пустым
            if( this.m_strName == "" )
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(  "Недопустимое значение наименования объекта", "Внимание",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
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
                cmd.CommandText = System.String.Format( "[{0}].[dbo].[sp_EditCurrency]", objProfile.GetOptionsDllDBName() );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ( ( System.Byte )( 0 ) ), ( ( System.Byte )( 0 ) ), "", System.Data.DataRowVersion.Current, null ) );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@GUID_ID", System.Data.SqlDbType.UniqueIdentifier ) );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@CURRENCY_CODE", System.Data.DbType.String ) );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@CURRENCY_ID", System.Data.SqlDbType.Int, 4 ) );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@CURRENCY_NAME", System.Data.DbType.String ) );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@CURRENCY_DESCRIPTION", System.Data.DbType.String ) );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@CURRENCY_MAIN", System.Data.SqlDbType.Bit, 1 ) );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@CURRENCY_NOTACTIVE", System.Data.SqlDbType.Bit, 1 ) );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@CURRENCY_PARENTCOEFF", System.Data.SqlDbType.Money, 8 ) );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@CURRENCY_DIVISIBLE", System.Data.SqlDbType.Int, 4 ) );

                cmd.Parameters[ "@GUID_ID" ].Value = this.m_uuidID;
                cmd.Parameters[ "@CURRENCY_CODE" ].Value = this.m_strCurrencyCode;
                cmd.Parameters[ "@CURRENCY_ID" ].Value = this.m_iCurrencyID;
                cmd.Parameters[ "@CURRENCY_NAME" ].Value = this.m_strName;
                cmd.Parameters[ "@CURRENCY_DESCRIPTION" ].Value = this.m_strDescription;
                cmd.Parameters[ "@CURRENCY_MAIN" ].Value = this.m_bIsMain;
                cmd.Parameters[ "@CURRENCY_NOTACTIVE" ].Value = !( this.m_bIsActive );
                cmd.Parameters[ "@CURRENCY_PARENTCOEFF" ].Value = this.m_moneyParentCoeff;
                cmd.Parameters[ "@CURRENCY_DIVISIBLE" ].Value = this.m_iDivisible;
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
                        DevExpress.XtraEditors.XtraMessageBox.Show(  "Валюта '" + this.m_strCurrencyCode + "' уже есть в БД", "Внимание",
                            System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning );
                        break;
                        case 2:
                        DevExpress.XtraEditors.XtraMessageBox.Show(  "Запись с указанным идентификатором не найдена \n" + this.m_uuidID.ToString(), "Внимание",
                            System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
                        break;
                        default:
                        DevExpress.XtraEditors.XtraMessageBox.Show(  "Ошибка изменения свойств валюты", "Внимание",
                            System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
                        break;
                    }
                }
                cmd.Dispose();
            }
            catch( System.Exception e )
            {
                DevExpress.XtraEditors.XtraMessageBox.Show( 
                "Не удалось изменить свойства валюты.\n" + e.Message, "Внимание",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
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
            return CurrencyCode;
        }

    }
}
