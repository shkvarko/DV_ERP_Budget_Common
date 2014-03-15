using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Linq;

namespace ERP_Budget.Common
{
    /// <summary>
    /// ����� "��������"
    /// </summary>
    public class CCompany : IBaseListItem
    {
        #region ����������, ��������, ���������
        /// <summary>
        /// TypeConverter ��� ������ ��������
        /// </summary>
        class AccountPlanTypeConverter : TypeConverter
        {
            /// <summary>
            /// ����� ������������� ����� �� ������
            /// </summary>
            public override bool GetStandardValuesSupported(
              ITypeDescriptorContext context)
            {
                return true;
            }
            /// <summary>
            /// ... � ������ �� ������
            /// </summary>
            public override bool GetStandardValuesExclusive(
              ITypeDescriptorContext context)
            {
                // false - ����� ������� �������
                // true - ������ ����� �� ������
                return true;
            }

            /// <summary>
            /// � ��� � ������
            /// </summary>
            public override StandardValuesCollection GetStandardValues(
              ITypeDescriptorContext context)
            {
                // ���������� ������ ����� �� �������� ���������
                // (���� ������, �������� � �.�.)

                CCompany objCompany = (CCompany)context.Instance;
                System.Collections.Generic.List<CAccountPlan> objList = objCompany.AllAccountPlanList;

                return new StandardValuesCollection(objList);
            }
        }

        /// <summary>
        /// ������������
        /// </summary>
        private string m_strCompanyAcronym;
        /// <summary>
        /// ��������
        /// </summary>
        private string m_strCompanyDescription;
        /// <summary>
        /// ����
        /// </summary>
        private string m_strCompanyOKPO;
        /// <summary>
        /// �����
        /// </summary>
        private string m_strCompanyOKULP;
        /// <summary>
        /// ���
        /// </summary>
        private string m_strCompanyUNN;
        /// <summary>
        /// ������� "�������"
        /// </summary>
        private bool m_bIsActive;
        [DisplayName("������������")]
        [Description("����������� ������������ ��������")]
        [Category("1. ������������ ��������")]
        public string CompanyAcronym
        {
            get { return m_strCompanyAcronym; }
            set { m_strCompanyAcronym = value; }
        }
        //[DisplayName("������������")]
        //[Description("����������� �������� ��������")]
        //[Category("1. ������������ ��������")]
        //public string CompanyName
        //{
        //    get { return m_strName; }
        //    set { m_strName = value; }
        //}
        [DisplayName("��������")]
        [Description("����������")]
        [Category("2. �������������� ��������")]
        public string CompanyDescription
        {
            get { return m_strCompanyDescription; }
            set { m_strCompanyDescription = value; }
        }
        [DisplayName("����")]
        [Description("��������������� ����� ����������� � �������������� ��������")]
        [Category("1. ������������ ��������")]
        public string CompanyOKPO
        {
            get { return m_strCompanyOKPO; }
            set { m_strCompanyOKPO = value; }
        }
        [DisplayName("�����")]
        [Description("������������������� ������������� ���������� �������� \"����������� ���� � �������������� ���������������\" ")]
        [Category("1. ������������ ��������")]
        public string CompanyOKULP
        {
            get { return m_strCompanyOKULP;  }
            set { m_strCompanyOKULP = value; }
        }
        [DisplayName("���")]
        [Description("���������� ����� �����������������")]
        [Category("1. ������������ ��������")]
        public string CompanyUNN
        {
            get { return m_strCompanyUNN; }
            set { m_strCompanyUNN = value; }
        }
        [DisplayName("�������")]
        [Description("������� ���������� ������")]
        [Category("2. �������������� ��������")]
        [TypeConverter(typeof(BooleanTypeConverter))]
        public bool IsActive
        {
            get { return m_bIsActive; }
            set { m_bIsActive = value; }
        }
        /// <summary>
        /// ����
        /// </summary>
        [BrowsableAttribute(false)]
        public List<CAccountPlan> AllAccountPlanList { get; set; }


        /// <summary>
        /// ����
        /// </summary>
        [DisplayName("����")]
        [Description("���� ������")]
        [Category("1. ������������ ��������")]
        [TypeConverter(typeof(AccountPlanTypeConverter))]
        [ReadOnly(false)]
        [BrowsableAttribute(false)]
        public CAccountPlan AccountPlan{get; set;}

        /// <summary>
        /// ����
        /// </summary>
        [DisplayName("����")]
        [Description("���� ������")]
        [Category("1. ������������ ��������")]
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
                "�� ������� ���������� �������� ���.\n\n����� ������: " + f.Message, "��������",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
            return;
        }


        #endregion

        #region ������������ 
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

        #region ������ ��������
        /// <summary>
        /// ���������� ������ ������� CCompany
        /// </summary>
        /// <param name="objProfile">�������</param>
        /// <param name="bAllList">���������� ���� ������</param>
        /// <param name="uuidDefValue">������������� �������� ��-���������</param>
        /// <returns>������ ������� ��������</returns>
        public CBaseList<CCompany> GetCompanyList( UniXP.Common.CProfile objProfile,
            System.Boolean bAllList, System.Guid uuidDefValue )
        {
            if( ( bAllList == false ) && ( uuidDefValue.CompareTo( System.Guid.Empty ) == 0 ) )
            {
                // ���� ��� ����� ������ ���� �������� � ������,
                // �� ���������� ������� ��� ���������� �������������
                DevExpress.XtraEditors.XtraMessageBox.Show( 
                    "�� ������ ���������� �������������!", "��������",
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
                "�� ������� �������� ������ ������� CCompany.\n" + e.Message, "��������",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
			finally // ������� ���������� �������
            {
            }
            return objList;
        }

        /// <summary>
        /// ���������� ������ ������� CCompany
        /// </summary>
        /// <param name="objProfile">�������</param>
        /// <returns>������ ������� ��������</returns>
        public static List<CCompany> GetCompanyList(UniXP.Common.CProfile objProfile)
        {
            List<CCompany> objList = new List<CCompany>();

            System.Data.SqlClient.SqlConnection DBConnection = objProfile.GetDBSource();
            if (DBConnection == null) { return objList; }

            try
            {
                // ���������� � �� ��������, ����������� ������� �� ������� ������
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
                    // ����� ������ ��������
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
                "�� ������� �������� ������ ������� CCompany.\n" + e.Message, "��������",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
			finally // ������� ���������� �������
            {
                DBConnection.Close();
            }
            return objList;
        }
        /// <summary>
        /// ��������� ������ ��������
        /// </summary>
        /// <param name="objProfile">�������</param>
        /// <param name="cmd">SQL - �������</param>
        /// <param name="objList">����������� ������</param>
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
                "�� ������� �������� ������ ��������.\n����� ������: " + e.Message, "��������",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
            }
			finally // ������� ���������� �������
            {
            }
            return;
        }
        /// <summary>
        /// ��������� � AllAccountPlanList ������ ������
        /// </summary>
        /// <param name="objProfile">�������</param>
        /// <param name="cmdSQL">SQL-�������</param>
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
                "�� ������� ��������� ���� ������.\n\n����� ������: " + f.Message, "��������",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
            return;
        }

        #endregion

        #region Init 
        /// <summary>
        /// ������������� ������� ������
        /// </summary>
        /// <param name="objProfile">�������</param>
        /// <param name="uuidID">���������� ������������� ������</param>
        /// <returns>true - �������� �������������; false - ������</returns>
        public override System.Boolean Init( UniXP.Common.CProfile objProfile, System.Guid uuidID )
        {
            System.Boolean bRet = false;

            System.Data.SqlClient.SqlConnection DBConnection = objProfile.GetDBSource();
            if( DBConnection == null ) { return bRet; }

            try
            {
                // ���������� � �� ��������, ����������� ������� �� ������� ������
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
                    // ����� ������ ��������, � ��� ��� ���������� ���� ������
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
                    "�� ������� ������������������� ����� CCompany.\n� �� �� ������� ����������.", "��������",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning );

                }
                cmd.Dispose();
                rs.Dispose();
            }
            catch( System.Exception e )
            {
                DevExpress.XtraEditors.XtraMessageBox.Show( 
                "�� ������� ������������������� ����� CCompany.\n" + e.Message, "��������",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
            }
			finally // ������� ���������� �������
            {
                DBConnection.Close();
            }
            return bRet;
        }
        #endregion

        #region Remove
        /// <summary>
        /// ������� ������ �� ��
        /// </summary>
        /// <param name="objProfile">�������</param>
        /// <param name="uuidID">���������� ������������� �������</param>
        /// <returns>true - ������� ����������; false - ������</returns>
        public override System.Boolean Remove( UniXP.Common.CProfile objProfile )
        {
            System.Boolean bRet = false;
            // ���������� ������������� �� ������ ���� ������
            if( this.m_uuidID == System.Guid.Empty )
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(  "������������ �������� ����������� �������������� �������", "��������",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
                return bRet;
            }

            System.Data.SqlClient.SqlConnection DBConnection = objProfile.GetDBSource();
            if( DBConnection == null ) { return bRet; }

            try
            {
                // ���������� � �� ��������, ����������� ������� �� �������� ������
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
                        DevExpress.XtraEditors.XtraMessageBox.Show(  "�� �������� ���� ������ � ��������� ���������.\n�������� ����������", "��������",
                            System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning );
                    }
                    else
                    {
                        DevExpress.XtraEditors.XtraMessageBox.Show(  "������ �������� ��������", "������",
                            System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
                    }
                }
                cmd.Dispose();
            }
            catch( System.Exception e )
            {
                DevExpress.XtraEditors.XtraMessageBox.Show( 
                "�� ������� ������� ��������.\n" + e.Message, "��������",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
            }
			finally // ������� ���������� �������
            {
                DBConnection.Close();
            }
            return bRet;
        }
        #endregion

        #region Add
        /// <summary>
        /// �������� ������ � ��
        /// </summary>
        /// <param name="objProfile">�������</param>
        /// <returns>true - ������� ����������; false - ������</returns>
        public override System.Boolean Add( UniXP.Common.CProfile objProfile )
        {
            System.Boolean bRet = false;

            // ������������ �� ������ ���� ������
            if( this.m_strName == "" )
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(  "������������ �������� ������������ �������", "��������",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
                return bRet;
            }
            // ������������ �� ������� ���� ������
            if( this.m_strCompanyAcronym == "" )
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(  "������������ �������� ������������", "��������",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
                return bRet;
            }
            // ���� �� ������ ���� ������
            if( this.m_strCompanyOKPO == "" )
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(  "������������ �������� ����", "��������",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
                return bRet;
            }
            // ����� �� ������ ���� ������
            if( this.m_strCompanyOKULP == "" )
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(  "������������ �������� �����", "��������",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
                return bRet;
            }
            // ��� �� ������ ���� ������
            if( this.m_strCompanyUNN == "" )
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(  "������������ �������� ���", "��������",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
                return bRet;
            }

            System.Data.SqlClient.SqlConnection DBConnection = objProfile.GetDBSource();
            if( DBConnection == null ) { return bRet; }

            try
            {
                // ���������� � �� ��������, ����������� ������� �� �������� ������ � ��
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
                    DevExpress.XtraEditors.XtraMessageBox.Show(((cmd.Parameters["@ERROR_MES"].Value == System.DBNull.Value) ? "" : (System.String)cmd.Parameters["@ERROR_MES"].Value), "��������",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);
                }
            }
            catch( System.Exception e )
            {
                DevExpress.XtraEditors.XtraMessageBox.Show( 
                "�� ������� ���������������� ��������.\n" + e.Message, "��������",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
            }
			finally // ������� ���������� �������
            {
                DBConnection.Close();
            }
            return bRet;
        }
        #endregion

        #region Update
        /// <summary>
        /// ��������� ��������� � ��
        /// </summary>
        /// <param name="objProfile">�������</param>
        /// <returns>true - ������� ����������; false - ������</returns>
        public override System.Boolean Update( UniXP.Common.CProfile objProfile )
        {
            System.Boolean bRet = false;

            // ���������� ������������� �� ������ ���� ������
            if( this.m_uuidID == System.Guid.Empty )
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(  "������������ �������� ����������� �������������� �������", "��������",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
                return bRet;
            }
            // ������������ �� ������ ���� ������
            if( this.m_strName == "" )
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(  "������������ �������� ������������ �������", "��������",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
                return bRet;
            }
            // ������������ �� ������� ���� ������
            if( this.m_strCompanyAcronym == "" )
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(  "������������ �������� ������������", "��������",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
                return bRet;
            }
            // ���� �� ������ ���� ������
            if( this.m_strCompanyOKPO == "" )
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(  "������������ �������� ����", "��������",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
                return bRet;
            }
            // ����� �� ������ ���� ������
            if( this.m_strCompanyOKULP == "" )
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(  "������������ �������� �����", "��������",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
                return bRet;
            }
            // ��� �� ������ ���� ������
            if( this.m_strCompanyUNN == "" )
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(  "������������ �������� ���", "��������",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
                return bRet;
            }

            System.Data.SqlClient.SqlConnection DBConnection = objProfile.GetDBSource();
            if( DBConnection == null ) { return bRet; }

            try
            {
                // ���������� � �� ��������, ����������� ������� �� �������� ������ � ��
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
                    DevExpress.XtraEditors.XtraMessageBox.Show(((cmd.Parameters["@ERROR_MES"].Value == System.DBNull.Value) ? "" : (System.String)cmd.Parameters["@ERROR_MES"].Value), "��������",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);
                }
            }
            catch( System.Exception e )
            {
                DevExpress.XtraEditors.XtraMessageBox.Show( 
                "�� ������� �������� �������� ��������.\n" + e.Message, "��������",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
            }
			finally // ������� ���������� �������
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
