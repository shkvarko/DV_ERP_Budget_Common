using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace ERP_Budget.Common
{
    /// <summary>
    /// Класс "Переменная для выбора типа документа"
    /// </summary>
    public class CBudgetDocTypeVariable : IBaseListItem
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
        public System.String m_strEditName;
        /// <summary>
        /// текущее значение переменной
        /// </summary>
        public System.String m_strValue;

        #endregion

        #region Конструкторы
        public CBudgetDocTypeVariable()
        {
            this.m_uuidID = System.Guid.Empty;
            this.m_strName = "";
            this.m_strEditClassName = "";
            this.m_strPatternSrc = "";
            this.m_strPattern = "";
            this.m_strDataTypeName = "";
            this.m_strEditName = "";
            this.m_strValue = "";
        }

        public CBudgetDocTypeVariable( System.Guid uuidID, System.String strName,
            System.String strEditClassName, System.String strPatternSrc, System.String strPattern,
            System.String strDataTypeName, System.String strValue )
        {
            this.m_uuidID = uuidID;
            this.m_strName = strName;
            this.m_strEditClassName = strEditClassName;
            this.m_strPatternSrc = strPatternSrc;
            this.m_strPattern = strPattern;
            this.m_strDataTypeName = strDataTypeName;
            this.m_strEditName = "";
            this.m_strValue = strValue;
        }
        #endregion

        #region Список переменных
        /// <summary>
        /// Возвращает список переменных
        /// </summary>
        /// <param name="objProfile">профайл</param>
        /// <returns>список условий выбора маршрута</returns>
        public static List<CBudgetDocTypeVariable> GetDocTypeVariableList( UniXP.Common.CProfile objProfile )
        {
            List<CBudgetDocTypeVariable> objList = new List<CBudgetDocTypeVariable>();
            try
            {
                System.Data.SqlClient.SqlConnection DBConnection = objProfile.GetDBSource();
                if( DBConnection == null ) { return objList; }

                // соединение с БД получено, прописываем команду на выборку данных
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
                cmd.Connection = DBConnection;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = System.String.Format( "[{0}].[dbo].[sp_GetDocTypeVariable]", objProfile.GetOptionsDllDBName() );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ( ( System.Byte )( 0 ) ), ( ( System.Byte )( 0 ) ), "", System.Data.DataRowVersion.Current, null ) );
                System.Data.SqlClient.SqlDataReader rs = cmd.ExecuteReader();
                if( rs.HasRows )
                {
                    while( rs.Read() )
                    {
                        objList.Add( new CBudgetDocTypeVariable( rs.GetGuid( 0 ), rs.GetString( 1 ),
                            rs.GetString( 2 ), rs.GetString( 3 ), rs.GetString( 4 ), rs.GetString( 5 ), "" ) );
                    }
                }
                cmd.Dispose();
                rs.Dispose();
                DBConnection.Close();
            }
            catch( System.Exception f )
            {
                DevExpress.XtraEditors.XtraMessageBox.Show( 
                "Не удалось получить список переменных.\n\nТекст ошибки: " + f.Message, "Внимание",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
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
                cmd.CommandText = System.String.Format( "[{0}].[dbo].[sp_GetDocTypeVariable]", objProfile.GetOptionsDllDBName() );
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
                    this.m_strEditClassName = rs.GetString( 2 );
                    this.m_strPatternSrc = rs.GetString( 3 );
                    this.m_strPattern = rs.GetString( 4 );
                    this.m_strDataTypeName = rs.GetString( 5 );
                    bRet = true;
                }
                else
                {
                    DevExpress.XtraEditors.XtraMessageBox.Show( 
                    "Не удалось проинициализировать класс CBudgetDocTypeVariable.\nВ БД не найдена информация.", "Внимание",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning );

                }
                cmd.Dispose();
                rs.Dispose();
            }
            catch( System.Exception e )
            {
                DevExpress.XtraEditors.XtraMessageBox.Show( 
                "Не удалось проинициализировать класс CBudgetDocTypeVariable.\n\nТекст ошибки: " + e.Message, "Внимание",
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
        /// <param name="cmd">SQL-команда</param>
        /// <param name="objProfile">профайл</param>
        /// <param name="uuidID">уникальный идентификатор класса</param>
        /// <returns>true - успешная инициализация; false - ошибка</returns>
        public System.Boolean Init( System.Data.SqlClient.SqlCommand cmd,
            UniXP.Common.CProfile objProfile, System.Guid uuidID )
        {
            System.Boolean bRet = false;
            if( ( cmd == null ) || ( cmd.Connection == null ) ) { return bRet; }

            try
            {
                // соединение с БД получено, прописываем команду на выборку данных
                cmd.Parameters.Clear();
                cmd.CommandText = System.String.Format( "[{0}].[dbo].[sp_GetDocTypeVariable]", objProfile.GetOptionsDllDBName() );
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
                    this.m_strEditClassName = rs.GetString( 2 );
                    this.m_strPatternSrc = rs.GetString( 3 );
                    this.m_strPattern = rs.GetString( 4 );
                    this.m_strDataTypeName = rs.GetString( 5 );
                    bRet = true;
                }
                else
                {
                    DevExpress.XtraEditors.XtraMessageBox.Show( 
                    "Не удалось проинициализировать класс CBudgetDocTypeVariable.\nВ БД не найдена информация.", "Внимание",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning );

                }
                rs.Dispose();
            }
            catch( System.Exception f )
            {
                DevExpress.XtraEditors.XtraMessageBox.Show( 
                "Не удалось проинициализировать класс CBudgetDocTypeVariable.\n\nТекст ошибки: " + f.Message, "Внимание",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
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
            System.Data.SqlClient.SqlTransaction DBTransaction = DBConnection.BeginTransaction();

            try
            {
                // соединение с БД получено, прописываем команду на удаление данных
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
                cmd.Connection = DBConnection;
                cmd.Transaction = DBTransaction;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = System.String.Format( "[{0}].[dbo].[sp_DeleteDocTypeVariable]", objProfile.GetOptionsDllDBName() );
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
                            DevExpress.XtraEditors.XtraMessageBox.Show(  "На запись с заданным идентификатором есть ссылка.\n" + this.uuidID.ToString(), "Внимание",
                                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning );
                            break;
                        }
                        default:
                        {
                            DevExpress.XtraEditors.XtraMessageBox.Show(  "Ошибка выполнения запроса на удаление переменной.\nПеременная : " + this.m_strName, "Внимание",
                                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
                            break;
                        }
                    }
                }
                cmd.Dispose();
            }
            catch( System.Exception f )
            {
                DBTransaction.Rollback();
                DevExpress.XtraEditors.XtraMessageBox.Show( 
                "Ошибка выполнения запроса на удаление переменной.\nПеременная : " + 
                this.m_strName + "\n\nТекст ошибки: " + f.Message, "Внимание",
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
        /// <param name="uuidID">уникальный идентификатор объекта</param>
        /// <returns>true - удачное завершение; false - ошибка</returns>
        public System.Boolean Remove( System.Data.SqlClient.SqlCommand cmd, UniXP.Common.CProfile objProfile )
        {
            System.Boolean bRet = false;
            if( ( cmd == null ) || ( cmd.Connection == null ) ) { return bRet; }
            // уникальный идентификатор не должен быть пустым
            if( this.m_uuidID == System.Guid.Empty )
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(  "Недопустимое значение уникального идентификатора объекта", "Внимание",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
                return bRet;
            }

            try
            {
                cmd.Parameters.Clear();
                cmd.CommandText = System.String.Format( "[{0}].[dbo].[sp_DeleteDocTypeVariable]", objProfile.GetOptionsDllDBName() );
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
                    switch( iRet )
                    {
                        case 1:
                        {
                            DevExpress.XtraEditors.XtraMessageBox.Show(  "На запись с заданным идентификатором есть ссылка.\n" + this.uuidID.ToString(), "Внимание",
                                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning );
                            break;
                        }
                        default:
                        {
                            DevExpress.XtraEditors.XtraMessageBox.Show(  "Ошибка выполнения запроса на удаление переменной.\nПеременная : " + this.m_strName, "Внимание",
                                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
                            break;
                        }
                    }
                }
            }
            catch( System.Exception f )
            {
                DevExpress.XtraEditors.XtraMessageBox.Show( 
                "Ошибка выполнения запроса на удаление переменной.\nПеременная : " + 
                this.m_strName + "\n\nТекст ошибки: " + f.Message, "Внимание",
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
                    DevExpress.XtraEditors.XtraMessageBox.Show(  "Недопустимое значение наименования объекта", "Внимание",
                        System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
                    return bRet;
                }

                // наименование класса не должно быть пустым
                if( this.m_strEditClassName.Trim() == "" )
                {
                    DevExpress.XtraEditors.XtraMessageBox.Show(  "Наименование класса, связанного с переменной не должно быть пустым" + this.m_strEditClassName, "Внимание",
                        System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
                    return bRet;
                }

                // наименование типа данных не должно быть пустым
                if( this.m_strDataTypeName.Trim() == "" )
                {
                    DevExpress.XtraEditors.XtraMessageBox.Show(  "Наименование типа данных не должно быть пустым" + this.m_strDataTypeName, "Внимание",
                        System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
                    return bRet;
                }

                // шаблон не должно быть пустым
                if( this.m_strPatternSrc.Trim() == "" )
                {
                    DevExpress.XtraEditors.XtraMessageBox.Show(  "Шаблон регулярного выражения для создания условия выбора маршрута\nне должен быть пустым" + this.m_strPatternSrc, "Внимание",
                        System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
                    return bRet;
                }

                // шаблон не должно быть пустым
                if( this.m_strPattern.Trim() == "" )
                {
                    DevExpress.XtraEditors.XtraMessageBox.Show(  "Шаблон регулярного выражения \nдля проверки выполнения условия выбора маршрута \nне должен быть пустым" + this.m_strPattern, "Внимание",
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
            if( DBConnection == null ) { return bRet; }
            System.Data.SqlClient.SqlTransaction DBTransaction = DBConnection.BeginTransaction();
            try
            {
                // соединение с БД получено, прописываем команду на создание записи в БД
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
                cmd.Connection = DBConnection;
                cmd.Transaction = DBTransaction;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = System.String.Format( "[{0}].[dbo].[sp_AddDocTypeVariable]", objProfile.GetOptionsDllDBName() );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ( ( System.Byte )( 0 ) ), ( ( System.Byte )( 0 ) ), "", System.Data.DataRowVersion.Current, null ) );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@GUID_ID", System.Data.SqlDbType.UniqueIdentifier, 4, System.Data.ParameterDirection.Output, false, ( ( System.Byte )( 0 ) ), ( ( System.Byte )( 0 ) ), "", System.Data.DataRowVersion.Current, null ) );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@DOCTYPEVARIABLE_NAME", System.Data.DbType.String ) );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@DOCTYPEVARIABLE_CLASSNAME", System.Data.DbType.String ) );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@DOCTYPEVARIABLE_PATTERNSRC", System.Data.DbType.String ) );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@DOCTYPEVARIABLE_PATTERN", System.Data.DbType.String ) );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@DATATYPE_NAME", System.Data.DbType.String ) );
                cmd.Parameters[ "@DOCTYPEVARIABLE_NAME" ].Value = this.m_strName;
                cmd.Parameters[ "@DOCTYPEVARIABLE_CLASSNAME" ].Value = this.m_strEditClassName;
                cmd.Parameters[ "@DOCTYPEVARIABLE_PATTERNSRC" ].Value = this.m_strPatternSrc;
                cmd.Parameters[ "@DOCTYPEVARIABLE_PATTERN" ].Value = this.m_strPattern;
                cmd.Parameters[ "@DATATYPE_NAME" ].Value = this.m_strDataTypeName;
                if( this.uuidID.CompareTo( System.Guid.Empty ) != 0 )
                {
                    cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@DOCTYPEVARIABLE_GUID_ID", System.Data.SqlDbType.UniqueIdentifier ) );
                    cmd.Parameters[ "@DOCTYPEVARIABLE_GUID_ID" ].Value = this.uuidID;
                }
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
                    switch( iRet )
                    {
                        case 1:
                        {
                            DevExpress.XtraEditors.XtraMessageBox.Show(  "Переменная с заданным именем существует : '" + this.m_strName + "'", "Внимание",
                                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning );
                            break;
                        }
                        default:
                        {
                            DevExpress.XtraEditors.XtraMessageBox.Show(  "Ошибка создания переменной : " + this.m_strName, "Ошибка",
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
                "Не удалось создать переменную\n" + this.m_strName + "\n" + e.Message, "Внимание",
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
        /// <param name="cmd">SQL-команда</param>
        /// <param name="objProfile">профайл</param>
        /// <returns>true - удачное завершение; false - ошибка</returns>
        public System.Boolean Add( System.Data.SqlClient.SqlCommand cmd, UniXP.Common.CProfile objProfile )
        {
            System.Boolean bRet = false;

            if( ( cmd == null ) || ( cmd.Connection == null ) ) { return bRet; }

            if( IsValidateProperties() == false ) { return bRet; }

            try
            {
                cmd.Parameters.Clear();
                cmd.CommandText = System.String.Format( "[{0}].[dbo].[sp_AddDocTypeVariable]", objProfile.GetOptionsDllDBName() );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ( ( System.Byte )( 0 ) ), ( ( System.Byte )( 0 ) ), "", System.Data.DataRowVersion.Current, null ) );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@GUID_ID", System.Data.SqlDbType.UniqueIdentifier, 4, System.Data.ParameterDirection.Output, false, ( ( System.Byte )( 0 ) ), ( ( System.Byte )( 0 ) ), "", System.Data.DataRowVersion.Current, null ) );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@DOCTYPEVARIABLE_NAME", System.Data.DbType.String ) );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@DOCTYPEVARIABLE_CLASSNAME", System.Data.DbType.String ) );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@DOCTYPEVARIABLE_PATTERNSRC", System.Data.DbType.String ) );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@DOCTYPEVARIABLE_PATTERN", System.Data.DbType.String ) );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@DATATYPE_NAME", System.Data.DbType.String ) );
                cmd.Parameters[ "@DOCTYPEVARIABLE_NAME" ].Value = this.m_strName;
                cmd.Parameters[ "@DOCTYPEVARIABLE_CLASSNAME" ].Value = this.m_strEditClassName;
                cmd.Parameters[ "@DOCTYPEVARIABLE_PATTERNSRC" ].Value = this.m_strPatternSrc;
                cmd.Parameters[ "@DOCTYPEVARIABLE_PATTERN" ].Value = this.m_strPattern;
                cmd.Parameters[ "@DATATYPE_NAME" ].Value = this.m_strDataTypeName;
                if( this.uuidID.CompareTo( System.Guid.Empty ) != 0 )
                {
                    cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@DOCTYPEVARIABLE_GUID_ID", System.Data.SqlDbType.UniqueIdentifier ) );
                    cmd.Parameters[ "@DOCTYPEVARIABLE_GUID_ID" ].Value = this.uuidID;
                }
                cmd.ExecuteNonQuery();
                System.Int32 iRet = ( System.Int32 )cmd.Parameters[ "@RETURN_VALUE" ].Value;
                if( iRet == 0 )
                {
                    this.m_uuidID = ( System.Guid )cmd.Parameters[ "@GUID_ID" ].Value;
                    bRet = true;
                }
                else
                {
                    switch( iRet )
                    {
                        case 1:
                        {
                            DevExpress.XtraEditors.XtraMessageBox.Show(  "Переменная с заданным именем существует : '" + this.m_strName + "'", "Внимание",
                                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning );
                            break;
                        }
                        default:
                        {
                            DevExpress.XtraEditors.XtraMessageBox.Show(  "Ошибка создания переменной : " + this.m_strName, "Ошибка",
                                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
                            break;
                        }
                    }
                }
            }
            catch( System.Exception e )
            {
                DevExpress.XtraEditors.XtraMessageBox.Show( 
                "Не удалось создать переменную\n" + this.m_strName + "\n" + e.Message, "Внимание",
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

            if( IsValidateProperties() == false ) { return bRet; }

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
                cmd.CommandText = System.String.Format( "[{0}].[dbo].[sp_EditDocTypeVariable]", objProfile.GetOptionsDllDBName() );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ( ( System.Byte )( 0 ) ), ( ( System.Byte )( 0 ) ), "", System.Data.DataRowVersion.Current, null ) );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@GUID_ID", System.Data.SqlDbType.UniqueIdentifier ) );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@DOCTYPEVARIABLE_NAME", System.Data.DbType.String ) );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@DOCTYPEVARIABLE_CLASSNAME", System.Data.DbType.String ) );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@DOCTYPEVARIABLE_PATTERNSRC", System.Data.DbType.String ) );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@DOCTYPEVARIABLE_PATTERN", System.Data.DbType.String ) );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@DATATYPE_NAME", System.Data.DbType.String ) );
                cmd.Parameters[ "@DOCTYPEVARIABLE_NAME" ].Value = this.m_strName;
                cmd.Parameters[ "@DOCTYPEVARIABLE_CLASSNAME" ].Value = this.m_strEditClassName;
                cmd.Parameters[ "@DOCTYPEVARIABLE_PATTERNSRC" ].Value = this.m_strPatternSrc;
                cmd.Parameters[ "@DOCTYPEVARIABLE_PATTERN" ].Value = this.m_strPattern;
                cmd.Parameters[ "@DATATYPE_NAME" ].Value = this.m_strDataTypeName;
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
                        DevExpress.XtraEditors.XtraMessageBox.Show(  "Переменная с заданным именем существует : '" + this.m_strName + "'", "Внимание",
                            System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning );
                        break;
                        case 2:
                        DevExpress.XtraEditors.XtraMessageBox.Show(  "Запись с указанным идентификатором не найдена \n" + this.m_uuidID.ToString(), "Внимание",
                            System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
                        break;
                        default:
                        DevExpress.XtraEditors.XtraMessageBox.Show(  "Ошибка изменения свойств переменной  : " + this.m_strName, "Внимание",
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
                "Ошибка изменения свойств переменной : " + this.m_strName + "\n" + e.Message, "Внимание",
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
        /// <param name="cmd">SQL-команда</param>
        /// <param name="objProfile">профайл</param>
        /// <returns>true - удачное завершение; false - ошибка</returns>
        public System.Boolean Update( System.Data.SqlClient.SqlCommand cmd, UniXP.Common.CProfile objProfile )
        {
            System.Boolean bRet = false;

            if( IsValidateProperties() == false ) { return bRet; }

            if( ( cmd == null ) || ( cmd.Connection == null ) ) { return bRet; }

            try
            {
                cmd.Parameters.Clear();
                cmd.CommandText = System.String.Format( "[{0}].[dbo].[sp_EditDocTypeVariable]", objProfile.GetOptionsDllDBName() );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ( ( System.Byte )( 0 ) ), ( ( System.Byte )( 0 ) ), "", System.Data.DataRowVersion.Current, null ) );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@GUID_ID", System.Data.SqlDbType.UniqueIdentifier ) );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@DOCTYPEVARIABLE_NAME", System.Data.DbType.String ) );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@DOCTYPEVARIABLE_CLASSNAME", System.Data.DbType.String ) );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@DOCTYPEVARIABLE_PATTERNSRC", System.Data.DbType.String ) );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@DOCTYPEVARIABLE_PATTERN", System.Data.DbType.String ) );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@DATATYPE_NAME", System.Data.DbType.String ) );
                cmd.Parameters[ "@DOCTYPEVARIABLE_NAME" ].Value = this.m_strName;
                cmd.Parameters[ "@DOCTYPEVARIABLE_CLASSNAME" ].Value = this.m_strEditClassName;
                cmd.Parameters[ "@DOCTYPEVARIABLE_PATTERNSRC" ].Value = this.m_strPatternSrc;
                cmd.Parameters[ "@DOCTYPEVARIABLE_PATTERN" ].Value = this.m_strPattern;
                cmd.Parameters[ "@DATATYPE_NAME" ].Value = this.m_strDataTypeName;
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
                        case 1:
                        DevExpress.XtraEditors.XtraMessageBox.Show(  "Переменная с заданным именем существует : '" + this.m_strName + "'", "Внимание",
                            System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning );
                        break;
                        case 2:
                        DevExpress.XtraEditors.XtraMessageBox.Show(  "Запись с указанным идентификатором не найдена \n" + this.m_uuidID.ToString(), "Внимание",
                            System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
                        break;
                        default:
                        DevExpress.XtraEditors.XtraMessageBox.Show(  "Ошибка изменения свойств переменной  : " + this.m_strName, "Внимание",
                            System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
                        break;
                    }
                }
            }
            catch( System.Exception e )
            {
                DevExpress.XtraEditors.XtraMessageBox.Show( 
                "Ошибка изменения свойств переменной : " + this.m_strName + "\n" + e.Message, "Внимание",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
            }
			finally // очищаем занимаемые ресурсы
            {
            }
            return bRet;
        }
        #endregion
    }

    #region Классы для элементов управления, связанных с переменными
    /// <summary>
    /// Интерфейсный класс для связи переменной с элементом управления
    /// </summary>
    public interface IBudgetDocTypeVariableEdit
    {
        System.String GetValue();
    }
    /// <summary>
    /// Класс, связывающий переменную "Дефицит средств" c  элементом управления
    /// </summary>
    public class CDocTypeVariableDeficitMoney : DevExpress.XtraEditors.CalcEdit, IBudgetDocTypeVariableEdit
    {
        public CDocTypeVariableDeficitMoney()
        {
        }

        public System.String GetValue()
        {
            if( ( this.Text == "" ) || ( this.Value == 0 ) )
            {
                return "0";
            }
            else
            {
                return this.Text;
            }
        }
    }
    /// <summary>
    /// Класс, связывающий переменную "Динамическое право" c  элементом управления
    /// </summary>
    public class CDocTypeVariableDynamicRight : DevExpress.XtraEditors.TextEdit, IBudgetDocTypeVariableEdit
    {
        public CDocTypeVariableDynamicRight()
        {
        }

        public System.String GetValue()
        {
            return this.Text;
        }
    }
    /// <summary>
    /// Класс, связывающий переменную "Возврат денег" c  элементом управления
    /// </summary>
    public class CDocTypeVariableBackMoney : DevExpress.XtraEditors.TextEdit, IBudgetDocTypeVariableEdit
    {
        public CDocTypeVariableBackMoney()
        {
        }

        public System.String GetValue()
        {
            return this.Text;
        }
    }
    /// <summary>
    /// Класс, связывающий переменную "Статья расходов" c  элементом управления
    /// </summary>
    public class CDocTypeVariableDebitArticle : DevExpress.XtraEditors.TextEdit, IBudgetDocTypeVariableEdit
    {
        public CDocTypeVariableDebitArticle()
        {
        }

        public System.String GetValue()
        {
            System.String strRet = "";
            try
            {
                System.Text.RegularExpressions.Regex rx = new Regex( @"^\d{1,}[.]{0,1}[\d|.]*", RegexOptions.Compiled | RegexOptions.IgnoreCase );
                System.Text.RegularExpressions.Match match = rx.Match( this.Text );
                if( ( match != null ) && ( match.Success ) )
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
    }

    #endregion

}
