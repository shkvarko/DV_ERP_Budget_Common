using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Linq;

namespace ERP_Budget.Common
{
    /// <summary>
    /// Класс "Компания"
    /// </summary>
    public class CCompany : IBaseListItem
    {
        #region Переменные, Свойства, Константы
        /// <summary>
        /// TypeConverter для списка регионов
        /// </summary>
        class AccountPlanTypeConverter : TypeConverter
        {
            /// <summary>
            /// Будем предоставлять выбор из списка
            /// </summary>
            public override bool GetStandardValuesSupported(
              ITypeDescriptorContext context)
            {
                return true;
            }
            /// <summary>
            /// ... и только из списка
            /// </summary>
            public override bool GetStandardValuesExclusive(
              ITypeDescriptorContext context)
            {
                // false - можно вводить вручную
                // true - только выбор из списка
                return true;
            }

            /// <summary>
            /// А вот и список
            /// </summary>
            public override StandardValuesCollection GetStandardValues(
              ITypeDescriptorContext context)
            {
                // возвращаем список строк из настроек программы
                // (базы данных, интернет и т.д.)

                CCompany objCompany = (CCompany)context.Instance;
                System.Collections.Generic.List<CAccountPlan> objList = objCompany.AllAccountPlanList;

                return new StandardValuesCollection(objList);
            }
        }

        /// <summary>
        /// Аббревиатура
        /// </summary>
        private string m_strCompanyAcronym;
        /// <summary>
        /// Описание
        /// </summary>
        private string m_strCompanyDescription;
        /// <summary>
        /// ОКПО
        /// </summary>
        private string m_strCompanyOKPO;
        /// <summary>
        /// ОКЮЛП
        /// </summary>
        private string m_strCompanyOKULP;
        /// <summary>
        /// УНН
        /// </summary>
        private string m_strCompanyUNN;
        /// <summary>
        /// Признак "Активен"
        /// </summary>
        private bool m_bIsActive;
        [DisplayName("Аббревиатура")]
        [Description("Сокращённое наименование компании")]
        [Category("1. Обязательные значения")]
        public string CompanyAcronym
        {
            get { return m_strCompanyAcronym; }
            set { m_strCompanyAcronym = value; }
        }
        //[DisplayName("Наименование")]
        //[Description("Юридическое название компании")]
        //[Category("1. Обязательные значения")]
        //public string CompanyName
        //{
        //    get { return m_strName; }
        //    set { m_strName = value; }
        //}
        [DisplayName("Описание")]
        [Description("Примечание")]
        [Category("2. Необязательные значения")]
        public string CompanyDescription
        {
            get { return m_strCompanyDescription; }
            set { m_strCompanyDescription = value; }
        }
        [DisplayName("ОКПО")]
        [Description("Регистрационный номер респондента в статистическом регистре")]
        [Category("1. Обязательные значения")]
        public string CompanyOKPO
        {
            get { return m_strCompanyOKPO; }
            set { m_strCompanyOKPO = value; }
        }
        [DisplayName("ОКЮЛП")]
        [Description("Общегосударственный классификатор Республики Беларусь \"Юридические лица и индивидуальные предприниматели\" ")]
        [Category("1. Обязательные значения")]
        public string CompanyOKULP
        {
            get { return m_strCompanyOKULP;  }
            set { m_strCompanyOKULP = value; }
        }
        [DisplayName("УНН")]
        [Description("Уникальный номер налогоплательщика")]
        [Category("1. Обязательные значения")]
        public string CompanyUNN
        {
            get { return m_strCompanyUNN; }
            set { m_strCompanyUNN = value; }
        }
        [DisplayName("Активна")]
        [Description("Признак активности записи")]
        [Category("2. Необязательные значения")]
        [TypeConverter(typeof(BooleanTypeConverter))]
        public bool IsActive
        {
            get { return m_bIsActive; }
            set { m_bIsActive = value; }
        }
        /// <summary>
        /// Счёт
        /// </summary>
        [BrowsableAttribute(false)]
        public List<CAccountPlan> AllAccountPlanList { get; set; }


        /// <summary>
        /// Счёт
        /// </summary>
        [DisplayName("Счёт")]
        [Description("План счетов")]
        [Category("1. Обязательные значения")]
        [TypeConverter(typeof(AccountPlanTypeConverter))]
        [ReadOnly(false)]
        [BrowsableAttribute(false)]
        public CAccountPlan AccountPlan{get; set;}

        /// <summary>
        /// Счёт
        /// </summary>
        [DisplayName("Счёт")]
        [Description("План счетов")]
        [Category("1. Обязательные значения")]
        [TypeConverter(typeof(AccountPlanTypeConverter))]
        public System.String AccountPlanFullName
        {
            get { return ((AccountPlan == null) ? "" : AccountPlan.FullName); }
            set { SetAccountPlanValue(value); }
        }
        private void SetAccountPlanValue(System.String strAccountPlanFullName)
        {
            try
            {
                if (AllAccountPlanList == null) { AccountPlan = null; }
                else
                {
                    AccountPlan = AllAccountPlanList.SingleOrDefault<CAccountPlan>(x => x.FullName == strAccountPlanFullName);
                }
            }
            catch (System.Exception f)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(
                "Не удалось установить значение ВТМ.\n\nТекст ошибки: " + f.Message, "Внимание",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
            return;
        }


        #endregion

        #region Конструкторы 
        public CCompany()
        {
            this.m_uuidID = System.Guid.Empty;
            this.m_strName = "";
            this.m_bIsActive = false;
            this.m_strCompanyAcronym = "";
            this.m_strCompanyDescription = "";
            this.m_strCompanyOKPO = "";
            this.m_strCompanyOKULP = "";
            this.m_strCompanyUNN = "";
            AccountPlan = null;
            AllAccountPlanList = null;
        }
        public CCompany( System.Guid uuidID, System.String strName )
        {
            this.m_uuidID = uuidID;
            this.m_strName = strName;
            this.m_bIsActive = false;
            this.m_strCompanyAcronym = "";
            this.m_strCompanyDescription = "";
            this.m_strCompanyOKPO = "";
            this.m_strCompanyOKULP = "";
            this.m_strCompanyUNN = "";
            AccountPlan = null;
            AllAccountPlanList = null;
        }
        public CCompany( System.Guid uuidID, System.String strName, System.String strCompanyAcronym )
        {
            this.m_uuidID = uuidID;
            this.m_strName = strName;
            this.m_bIsActive = false;
            this.m_strCompanyAcronym = strCompanyAcronym;
            this.m_strCompanyDescription = "";
            this.m_strCompanyOKPO = "";
            this.m_strCompanyOKULP = "";
            this.m_strCompanyUNN = "";
            AccountPlan = null;
            AllAccountPlanList = null;
        }
        public CCompany( System.Guid uuidID, System.String strName, System.Boolean bIsActive,
            System.String strCompanyAcronym, System.String strCompanyOKPO, System.String strCompanyOKULP,
            System.String strCompanyUNN )
        {
            this.m_uuidID = uuidID;
            this.m_strName = strName;
            this.m_bIsActive = bIsActive;
            this.m_strCompanyAcronym = strCompanyAcronym;
            this.m_strCompanyDescription = "";
            this.m_strCompanyOKPO = strCompanyOKPO;
            this.m_strCompanyOKULP = strCompanyOKULP;
            this.m_strCompanyUNN = strCompanyUNN;
            AccountPlan = null;
            AllAccountPlanList = null;
        }
        #endregion

        #region Список компаний
        /// <summary>
        /// Возвращает список классов CCompany
        /// </summary>
        /// <param name="objProfile">профайл</param>
        /// <param name="bAllList">возвращать весь список</param>
        /// <param name="uuidDefValue">идентификатор значения по-умолчанию</param>
        /// <returns>список классов компаний</returns>
        public CBaseList<CCompany> GetCompanyList( UniXP.Common.CProfile objProfile,
            System.Boolean bAllList, System.Guid uuidDefValue )
        {
            if( ( bAllList == false ) && ( uuidDefValue.CompareTo( System.Guid.Empty ) == 0 ) )
            {
                // если нам нужно только одно значение в списке,
                // то необходимо указать его уникальный идентификатор
                DevExpress.XtraEditors.XtraMessageBox.Show( 
                    "Не указан уникальный идентификатор!", "Внимание",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning );
                return null;
            }
            CBaseList<CCompany> objList = new CBaseList<CCompany>();

            try
            {
                System.Collections.Generic.List<CCompany> objListSrc = CCompany.GetCompanyList(objProfile);
                if (objListSrc != null)
                {
                    foreach (CCompany objCompany in objListSrc)
                    {
                        objList.AddItemToList(objCompany);
                    }
                }
            }
            catch (System.Exception e)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(
                "Не удалось получить список классов CCompany.\n" + e.Message, "Внимание",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
			finally // очищаем занимаемые ресурсы
            {
            }
            return objList;
        }

        /// <summary>
        /// Возвращает список классов CCompany
        /// </summary>
        /// <param name="objProfile">профайл</param>
        /// <returns>список классов компаний</returns>
        public static List<CCompany> GetCompanyList(UniXP.Common.CProfile objProfile)
        {
            List<CCompany> objList = new List<CCompany>();

            System.Data.SqlClient.SqlConnection DBConnection = objProfile.GetDBSource();
            if (DBConnection == null) { return objList; }

            try
            {
                // соединение с БД получено, прописываем команду на выборку данных
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand() { Connection = DBConnection, 
                    CommandType = System.Data.CommandType.StoredProcedure, 
                    CommandText = System.String.Format("[{0}].[dbo].[sp_GetCompany]", objProfile.GetOptionsDllDBName()) };
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ((System.Byte)(0)), ((System.Byte)(0)), "", System.Data.DataRowVersion.Current, null));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ERROR_NUM", System.Data.SqlDbType.Int, 8, System.Data.ParameterDirection.Output, false, ((System.Byte)(0)), ((System.Byte)(0)), "", System.Data.DataRowVersion.Current, null));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ERROR_MES", System.Data.SqlDbType.NVarChar, 4000));
                cmd.Parameters["@ERROR_MES"].Direction = System.Data.ParameterDirection.Output;
                System.Data.SqlClient.SqlDataReader rs = cmd.ExecuteReader();
                if (rs.HasRows)
                {
                    // набор данных непустой
                    while (rs.Read())
                    {
                        objList.Add( new CCompany()
                            {
                                m_uuidID = (System.Guid)rs["GUID_ID"],
                                CompanyAcronym = System.Convert.ToString(rs["COMPANY_ACRONYM"]),
                                Name = System.Convert.ToString(rs["COMPANY_NAME"]),
                                CompanyDescription = ((rs["COMPANY_DESCRIPTION"] != System.DBNull.Value) ? System.Convert.ToString(rs["COMPANY_DESCRIPTION"]) : ""),
                                CompanyUNN = System.Convert.ToString(rs["COMPANY_UNN"]),
                                CompanyOKULP = System.Convert.ToString(rs["COMPANY_OKULP"]),
                                CompanyOKPO = System.Convert.ToString(rs["COMPANY_OKPO"]),
                                IsActive = !(System.Convert.ToBoolean(rs["COMPANY_NOTACTIVE"])),
                                AccountPlan = ( ( rs["ACCOUNTPLAN_GUID"] != System.DBNull.Value ) ? new CAccountPlan()
                                {
                                    uuidID = (System.Guid)rs["ACCOUNTPLAN_GUID"],
                                    Name = System.Convert.ToString(rs["ACCOUNTPLAN_NAME"]),
                                    IsActive = System.Convert.ToBoolean(rs["ACCOUNTPLAN_ACTIVE"]),
                                    CodeIn1C = System.Convert.ToString(rs["ACCOUNTPLAN_1C_CODE"])
                                } : null) 
                            }
                        );

                    }
                }
                cmd.Dispose();
                rs.Dispose();

            }
            catch (System.Exception e)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(
                "Не удалось получить список классов CCompany.\n" + e.Message, "Внимание",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
			finally // очищаем занимаемые ресурсы
            {
                DBConnection.Close();
            }
            return objList;
        }
        /// <summary>
        /// Обновляет список компаний
        /// </summary>
        /// <param name="objProfile">профайл</param>
        /// <param name="cmd">SQL - команда</param>
        /// <param name="objList">обновляемый список</param>
        /// <returns></returns>
        public static void RefreshCompanyList( UniXP.Common.CProfile objProfile,
            System.Data.SqlClient.SqlCommand cmd, System.Collections.Generic.List<CCompany> objList )
        {
            if( cmd == null ) { return; }
            objList.Clear();

            try
            {
                System.Collections.Generic.List<CCompany> objListSrc = CCompany.GetCompanyList(objProfile);
                if (objListSrc != null)
                {
                    foreach (CCompany objCompany in objListSrc)
                    {
                        objList.Add(objCompany);
                    }
                }
            }
            catch( System.Exception e )
            {
                DevExpress.XtraEditors.XtraMessageBox.Show( 
                "Не удалось получить список компаний.\nТекст ошибки: " + e.Message, "Внимание",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
            }
			finally // очищаем занимаемые ресурсы
            {
            }
            return;
        }
        /// <summary>
        /// Загружает в AllAccountPlanList список счетов
        /// </summary>
        /// <param name="objProfile">профайл</param>
        /// <param name="cmdSQL">SQL-команда</param>
        public void InitAccountPlanList(UniXP.Common.CProfile objProfile, System.Data.SqlClient.SqlCommand cmdSQL)
        {
            try
            {
                System.String strErr = "";
                this.AllAccountPlanList = CAccountPlanDataBaseModel.GetAccountPlanList(objProfile, cmdSQL, ref strErr);
                if ((this.AllAccountPlanList != null) && (this.AllAccountPlanList.Count > 0))
                {
                    this.AccountPlan = this.AllAccountPlanList[0];
                }
            }
            catch (System.Exception f)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(
                "Не удалось загрузить план счетов.\n\nТекст ошибки: " + f.Message, "Внимание",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
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
                cmd.CommandText = System.String.Format( "[{0}].[dbo].[sp_GetCompany]", objProfile.GetOptionsDllDBName() );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ( ( System.Byte )( 0 ) ), ( ( System.Byte )( 0 ) ), "", System.Data.DataRowVersion.Current, null ) );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@GUID_ID", System.Data.SqlDbType.UniqueIdentifier ) );
                cmd.Parameters[ "@GUID_ID" ].Value = uuidID;
                System.Data.SqlClient.SqlDataReader rs = cmd.ExecuteReader();
                if( rs.HasRows )
                {
                    // набор данных непустой, в нем нас интересует одна запись
                    rs.Read();
                    this.m_uuidID = rs.GetGuid( 0 );
                    this.m_strCompanyAcronym = rs.GetString( 1 );
                    this.m_strName = rs.GetString( 2 );
                    if( rs[ 3 ] != System.DBNull.Value )
                    { this.m_strCompanyDescription = rs.GetString( 3 ); }
                    this.m_strCompanyUNN = rs.GetString( 4 );
                    this.m_strCompanyOKULP = rs.GetString( 5 );
                    this.m_strCompanyOKPO = rs.GetString( 6 );
                    this.m_bIsActive = !rs.GetBoolean( 7 );
                    
                    bRet = true;
                }
                else
                {
                    DevExpress.XtraEditors.XtraMessageBox.Show( 
                    "Не удалось проинициализировать класс CCompany.\nВ БД не найдена информация.", "Внимание",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning );

                }
                cmd.Dispose();
                rs.Dispose();
            }
            catch( System.Exception e )
            {
                DevExpress.XtraEditors.XtraMessageBox.Show( 
                "Не удалось проинициализировать класс CCompany.\n" + e.Message, "Внимание",
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
                cmd.CommandText = System.String.Format( "[{0}].[dbo].[sp_DeleteCompany]", objProfile.GetOptionsDllDBName() );
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
                        DevExpress.XtraEditors.XtraMessageBox.Show(  "На компанию есть ссылка в бюджетном документе.\nУдаление невозможно", "Внимание",
                            System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning );
                    }
                    else
                    {
                        DevExpress.XtraEditors.XtraMessageBox.Show(  "Ошибка удаления компании", "Ошибка",
                            System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
                    }
                }
                cmd.Dispose();
            }
            catch( System.Exception e )
            {
                DevExpress.XtraEditors.XtraMessageBox.Show( 
                "Не удалось удалить компанию.\n" + e.Message, "Внимание",
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
            // аббревиатура не должена быть пустым
            if( this.m_strCompanyAcronym == "" )
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(  "Недопустимое значение аббревиатуры", "Внимание",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
                return bRet;
            }
            // ОКПО не должен быть пустым
            if( this.m_strCompanyOKPO == "" )
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(  "Недопустимое значение ОКПО", "Внимание",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
                return bRet;
            }
            // ОКЮЛП не должен быть пустым
            if( this.m_strCompanyOKULP == "" )
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(  "Недопустимое значение ОКЮЛП", "Внимание",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
                return bRet;
            }
            // УНН не должен быть пустым
            if( this.m_strCompanyUNN == "" )
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(  "Недопустимое значение УНН", "Внимание",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
                return bRet;
            }

            System.Data.SqlClient.SqlConnection DBConnection = objProfile.GetDBSource();
            if( DBConnection == null ) { return bRet; }

            try
            {
                // соединение с БД получено, прописываем команду на создание записи в БД
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand() 
                { 
                    Connection = DBConnection, CommandType = System.Data.CommandType.StoredProcedure, 
                    CommandText = System.String.Format("[{0}].[dbo].[sp_AddCompany]", objProfile.GetOptionsDllDBName()) 
                };
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ( ( System.Byte )( 0 ) ), ( ( System.Byte )( 0 ) ), "", System.Data.DataRowVersion.Current, null ) );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@GUID_ID", System.Data.SqlDbType.UniqueIdentifier, 4, System.Data.ParameterDirection.Output, false, ( ( System.Byte )( 0 ) ), ( ( System.Byte )( 0 ) ), "", System.Data.DataRowVersion.Current, null ) );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@COMPANY_ACRONYM", System.Data.DbType.String ) );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@COMPANY_NAME", System.Data.DbType.String ) );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@COMPANY_OKPO", System.Data.DbType.String ) );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@COMPANY_OKULP", System.Data.DbType.String ) );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@COMPANY_UNN", System.Data.DbType.String ) );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@COMPANY_NOTACTIVE", System.Data.SqlDbType.Bit, 1 ) );
                if (this.m_strCompanyDescription.Length > 0)
                { 
                    cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@COMPANY_DESCRIPTION", System.Data.DbType.String ) );
                    cmd.Parameters[ "@COMPANY_DESCRIPTION" ].Value = this.m_strCompanyDescription;
                }

                cmd.Parameters[ "@COMPANY_ACRONYM" ].Value = this.m_strCompanyAcronym;
                cmd.Parameters[ "@COMPANY_NAME" ].Value = this.m_strName;
                cmd.Parameters[ "@COMPANY_OKPO" ].Value = this.m_strCompanyOKPO;
                cmd.Parameters[ "@COMPANY_OKULP" ].Value = this.m_strCompanyOKULP;
                cmd.Parameters[ "@COMPANY_UNN" ].Value = this.m_strCompanyUNN;
                cmd.Parameters[ "@COMPANY_NOTACTIVE" ].Value = !( this.m_bIsActive );
                if (this.AccountPlan != null)
                {
                    cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ACCOUNTPLAN_GUID", System.Data.SqlDbType.UniqueIdentifier));
                    cmd.Parameters["@ACCOUNTPLAN_GUID"].Value = this.AccountPlan.uuidID;
                }
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ERROR_NUM", System.Data.SqlDbType.Int, 8, System.Data.ParameterDirection.Output, false, ((System.Byte)(0)), ((System.Byte)(0)), "", System.Data.DataRowVersion.Current, null));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ERROR_MES", System.Data.SqlDbType.NVarChar, 4000));
                cmd.Parameters["@ERROR_MES"].Direction = System.Data.ParameterDirection.Output;
                
                cmd.ExecuteNonQuery();
                System.Int32 iRet = ( System.Int32 )cmd.Parameters[ "@RETURN_VALUE" ].Value;
                if( iRet == 0 )
                {
                    this.m_uuidID = ( System.Guid )cmd.Parameters[ "@GUID_ID" ].Value;
                    bRet = true;
                }
                else { bRet = false; }

                cmd.Dispose();

                if (bRet == false)
                {
                    DevExpress.XtraEditors.XtraMessageBox.Show(((cmd.Parameters["@ERROR_MES"].Value == System.DBNull.Value) ? "" : (System.String)cmd.Parameters["@ERROR_MES"].Value), "Внимание",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);
                }
            }
            catch( System.Exception e )
            {
                DevExpress.XtraEditors.XtraMessageBox.Show( 
                "Не удалось зарегистрировать компанию.\n" + e.Message, "Внимание",
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
                DevExpress.XtraEditors.XtraMessageBox.Show(  "Недопустимое значение наименования объекта", "Внимание",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
                return bRet;
            }
            // аббревиатура не должена быть пустым
            if( this.m_strCompanyAcronym == "" )
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(  "Недопустимое значение аббревиатуры", "Внимание",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
                return bRet;
            }
            // ОКПО не должен быть пустым
            if( this.m_strCompanyOKPO == "" )
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(  "Недопустимое значение ОКПО", "Внимание",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
                return bRet;
            }
            // ОКЮЛП не должен быть пустым
            if( this.m_strCompanyOKULP == "" )
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(  "Недопустимое значение ОКЮЛП", "Внимание",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
                return bRet;
            }
            // УНН не должен быть пустым
            if( this.m_strCompanyUNN == "" )
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(  "Недопустимое значение УНН", "Внимание",
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
                cmd.CommandText = System.String.Format( "[{0}].[dbo].[sp_EditCompany]", objProfile.GetOptionsDllDBName() );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ( ( System.Byte )( 0 ) ), ( ( System.Byte )( 0 ) ), "", System.Data.DataRowVersion.Current, null ) );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@GUID_ID", System.Data.SqlDbType.UniqueIdentifier ) );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@COMPANY_ACRONYM", System.Data.DbType.String ) );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@COMPANY_NAME", System.Data.DbType.String ) );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@COMPANY_OKPO", System.Data.DbType.String ) );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@COMPANY_OKULP", System.Data.DbType.String ) );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@COMPANY_UNN", System.Data.DbType.String ) );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@COMPANY_NOTACTIVE", System.Data.SqlDbType.Bit, 1 ) );
                if (this.m_strCompanyDescription.Length > 0)
                {
                    cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@COMPANY_DESCRIPTION", System.Data.DbType.String ) );
                    cmd.Parameters[ "@COMPANY_DESCRIPTION" ].Value = this.m_strCompanyDescription;
                }

                cmd.Parameters[ "@GUID_ID" ].Value = this.m_uuidID;
                cmd.Parameters[ "@COMPANY_ACRONYM" ].Value = this.m_strCompanyAcronym;
                cmd.Parameters[ "@COMPANY_NAME" ].Value = this.m_strName;
                cmd.Parameters[ "@COMPANY_OKPO" ].Value = this.m_strCompanyOKPO;
                cmd.Parameters[ "@COMPANY_OKULP" ].Value = this.m_strCompanyOKULP;
                cmd.Parameters[ "@COMPANY_UNN" ].Value = this.m_strCompanyUNN;
                cmd.Parameters[ "@COMPANY_NOTACTIVE" ].Value = !( this.m_bIsActive );
                if (this.AccountPlan != null)
                {
                    cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ACCOUNTPLAN_GUID", System.Data.SqlDbType.UniqueIdentifier));
                    cmd.Parameters["@ACCOUNTPLAN_GUID"].Value = this.AccountPlan.uuidID;
                }
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ERROR_NUM", System.Data.SqlDbType.Int, 8, System.Data.ParameterDirection.Output, false, ((System.Byte)(0)), ((System.Byte)(0)), "", System.Data.DataRowVersion.Current, null));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ERROR_MES", System.Data.SqlDbType.NVarChar, 4000));
                cmd.Parameters["@ERROR_MES"].Direction = System.Data.ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                System.Int32 iRet = ( System.Int32 )cmd.Parameters[ "@RETURN_VALUE" ].Value;
                bRet = ( iRet == 0 );
                cmd.Dispose();

                if (bRet == false)
                {
                    DevExpress.XtraEditors.XtraMessageBox.Show(((cmd.Parameters["@ERROR_MES"].Value == System.DBNull.Value) ? "" : (System.String)cmd.Parameters["@ERROR_MES"].Value), "Внимание",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);
                }
            }
            catch( System.Exception e )
            {
                DevExpress.XtraEditors.XtraMessageBox.Show( 
                "Не удалось изменить свойства компании.\n" + e.Message, "Внимание",
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
            return CompanyAcronym;
        }

    }
}
