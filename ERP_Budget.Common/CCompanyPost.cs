using System;
using System.Collections.Generic;
using System.Text;

namespace ERP_Budget.Common
{
    /// <summary>
    /// ����� "��������� � ��������"
    /// </summary>
    public class CCompanyPost : IBaseListItem
    {
        #region ����������, ��������, ���������
        /// <summary>
        /// ��������
        /// </summary>
        private CCompany m_objCompany;
        /// <summary>
        /// ���������
        /// </summary>
        private CEmployeePost m_objEmploeePost;
        /// <summary>
        /// ���������� ������������� ������������
        /// </summary>
        private System.Int32 m_iUserID;
        /// <summary>
        /// ��������
        /// </summary>
        public CCompany Company
        {
            get
            {
                return m_objCompany;
            }
            set
            {
                m_objCompany = value;
            }
        }
        /// <summary>
        /// ���������
        /// </summary>
        public CEmployeePost EmploeePost
        {
            get
            {
                return m_objEmploeePost;
            }
            set
            {
                m_objEmploeePost = value;
            }
        }
        /// <summary>
        /// ���������� ������������� ������������
        /// </summary>
        public System.Int32 UserID
        {
            get
            { return m_iUserID; }
        }

        #endregion

        public CCompanyPost()
        {
            m_objCompany = null;
            m_objEmploeePost = null;
        }
        /// <summary>
        /// ���������� ������ ���������� ��� ��������� ������������
        /// </summary>
        /// <param name="objProfile">�������</param>
        /// <param name="iUserID">������������� ������������</param>
        /// <returns>������ ���������� ��� ��������� ������������</returns>
        public CBaseList<CCompanyPost> GetUserCompanyPostList( UniXP.Common.CProfile objProfile, 
            System.Int32 iUserID )
        {
            CBaseList<CCompanyPost> objList = new CBaseList<CCompanyPost>();

            System.Data.SqlClient.SqlConnection DBConnection = objProfile.GetDBSource();
            if( DBConnection == null ) { return objList; }

            try
            {
                // ���������� � �� ��������, ����������� ������� �� ������� ������
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
                cmd.Connection = DBConnection;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = System.String.Format( "[{0}].[dbo].[sp_GetUserCompanyList]", objProfile.GetOptionsDllDBName() );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ( ( System.Byte )( 0 ) ), ( ( System.Byte )( 0 ) ), "", System.Data.DataRowVersion.Current, null ) );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@ulUserID", System.Data.SqlDbType.Int, 4 ) );
                cmd.Parameters[ "@ulUserID" ].Value = iUserID;
                System.Data.SqlClient.SqlDataReader rs = cmd.ExecuteReader();
                if( rs.HasRows )
                {
                    // ����� ������ ��������
                    CCompanyPost objCompanyPost = null;
                    CCompany objCompany = null;
                    CEmployeePost objEmployeePost = null;
                    while( rs.Read() )
                    {
                        objCompanyPost = new CCompanyPost();
                        objCompanyPost.m_uuidID = rs.GetGuid( 0 );
                        objCompanyPost.m_strName = rs.GetString( 7 ) + " " + rs.GetString( 4 );
                        objCompanyPost.m_iUserID = rs.GetInt32( 3 );
                        
                        objCompany = new CCompany();
                        objCompany.uuidID = rs.GetGuid( 1 );
                        objCompany.Name = rs.GetString( 7 );
                        objCompany.CompanyAcronym = rs.GetString( 5 );
                        objCompany.Name = rs.GetString( 7 );
                        if( rs[ 6 ] != System.DBNull.Value )
                        {
                            objCompany.CompanyDescription = rs.GetString( 6 );
                        }
                        objCompanyPost.m_objCompany = objCompany;

                        objEmployeePost = new CEmployeePost();
                        objEmployeePost.uuidID = rs.GetGuid( 2 );
                        objEmployeePost.Name = rs.GetString( 4 );
                        objCompanyPost.m_objEmploeePost = objEmployeePost;

                        objList.AddItemToList( objCompanyPost );
                    }
                    objCompany = null;
                    objEmployeePost = null;
                }
                else
                {
                    //DevExpress.XtraEditors.XtraMessageBox.Show( 
                    //"�� ������� �������� ������ ���������� ��� ��������� ������������.\n� �� �� ������� ����������.", "��������",
                    //System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning );

                }
                cmd.Dispose();
                rs.Dispose();
            }
            catch( System.Exception e )
            {
                DevExpress.XtraEditors.XtraMessageBox.Show( 
                "�� ������� �������� ������ ���������� ��� ��������� ������������.\n" + e.Message, "��������",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
            }
            finally // ������� ���������� �������
            {
                DBConnection.Close();
            }
            return objList;
        }

        /// <summary>
        /// ������������� ������� ������ CCompanyPost
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
                cmd.CommandText = System.String.Format( "[{0}].[dbo].[sp_GetUserCompany]", objProfile.GetOptionsDllDBName() );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ( ( System.Byte )( 0 ) ), ( ( System.Byte )( 0 ) ), "", System.Data.DataRowVersion.Current, null ) );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@GUID_ID", System.Data.SqlDbType.UniqueIdentifier ) );
                cmd.Parameters[ "@GUID_ID" ].Value = uuidID;
                System.Data.SqlClient.SqlDataReader rs = cmd.ExecuteReader();
                if( rs.HasRows )
                {
                    // ����� ������ ��������, � ��� ��� ���������� ���� ������
                    rs.Read();
                    this.m_uuidID = rs.GetGuid( 0 );
                    this.m_strName = rs.GetString( 7 ) + " " + rs.GetString( 4 );
                    this.m_iUserID = rs.GetInt32( 3 );
                    CCompany objCompany = new CCompany();
                    objCompany.Init( objProfile, rs.GetGuid( 1 ) );
                    this.m_objCompany = objCompany;
                    CEmployeePost objEmployeePost = new CEmployeePost();
                    objEmployeePost.Init( objProfile, rs.GetGuid( 2 ) );
                    this.m_objEmploeePost = objEmployeePost;
                    bRet = true;
                }
                else
                {
                    DevExpress.XtraEditors.XtraMessageBox.Show( 
                    "�� ������� ������������������� ����� CCompanyPost.\n� �� �� ������� ����������.", "��������",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning );

                }
                cmd.Dispose();
                rs.Dispose();
            }
            catch( System.Exception e )
            {
                DevExpress.XtraEditors.XtraMessageBox.Show( 
                "�� ������� ������������������� ����� CCompanyPost.\n" + e.Message, "��������",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
            }
            finally // ������� ���������� �������
            {
                DBConnection.Close();
            }
            return bRet;
        }

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
            if( this.Company.uuidID == System.Guid.Empty )
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(  "������������ �������� ����������� �������������� ��������", "��������",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
                return bRet;
            }
            // ���������� ������������� �� ������ ���� ������
            if( this.EmploeePost.uuidID == System.Guid.Empty )
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(  "������������ �������� ����������� �������������� ���������", "��������",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
                return bRet;
            }
            // ���������� ������������� �� ������ ���� ������
            if( this.UserID <= 0 )
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(  "������������ �������� ����������� �������������� ������������", "��������",
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
                cmd.CommandText = System.String.Format( "[{0}].[dbo].[sp_AssignUserCompany]", objProfile.GetOptionsDllDBName() );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ( ( System.Byte )( 0 ) ), ( ( System.Byte )( 0 ) ), "", System.Data.DataRowVersion.Current, null ) );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@COMPANY_GUID_ID", System.Data.SqlDbType.UniqueIdentifier ) );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@EMPLOYEEPOST_GUID_ID", System.Data.SqlDbType.UniqueIdentifier ) );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@ulUserID", System.Data.SqlDbType.Int, 4 ) );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@bAssign", System.Data.SqlDbType.Bit, 1 ) );
                cmd.Parameters[ "@COMPANY_GUID_ID" ].Value = this.m_objCompany.uuidID;
                cmd.Parameters[ "@EMPLOYEEPOST_GUID_ID" ].Value = this.m_objEmploeePost.uuidID;
                cmd.Parameters[ "@ulUserID" ].Value = this.UserID;
                cmd.Parameters[ "@bAssign" ].Value = 0;
                cmd.ExecuteNonQuery();
                System.Int32 iRet = ( System.Int32 )cmd.Parameters[ "@RETURN_VALUE" ].Value;
                if( iRet == 0 )
                {
                    bRet = true;
                }
                else
                {
                    DevExpress.XtraEditors.XtraMessageBox.Show(  "������ �������� ����� ������������ - �������� - ���������", "������",
                        System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
                }
                cmd.Dispose();
            }
            catch( System.Exception e )
            {
                DevExpress.XtraEditors.XtraMessageBox.Show( 
                "������ �������� ����� ������������ - �������� - ���������.\n" + e.Message, "��������",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
            }
			finally // ������� ���������� �������
            {
                DBConnection.Close();
            }
            return bRet;
        }

        /// <summary>
        /// �������� ������ � ��
        /// </summary>
        /// <param name="objProfile">�������</param>
        /// <returns>true - ������� ����������; false - ������</returns>
        public override System.Boolean Add( UniXP.Common.CProfile objProfile )
        {
            System.Boolean bRet = false;
            // ���������� ������������� �� ������ ���� ������
            if( this.Company.uuidID == System.Guid.Empty )
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(  "������������ �������� ����������� �������������� ��������", "��������",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
                return bRet;
            }
            // ���������� ������������� �� ������ ���� ������
            if( this.EmploeePost.uuidID == System.Guid.Empty )
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(  "������������ �������� ����������� �������������� ���������", "��������",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
                return bRet;
            }
            // ���������� ������������� �� ������ ���� ������
            if( this.UserID <= 0 )
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(  "������������ �������� ����������� �������������� ������������", "��������",
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
                cmd.CommandText = System.String.Format( "[{0}].[dbo].[sp_AssignUserCompany]", objProfile.GetOptionsDllDBName() );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ( ( System.Byte )( 0 ) ), ( ( System.Byte )( 0 ) ), "", System.Data.DataRowVersion.Current, null ) );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@COMPANY_GUID_ID", System.Data.SqlDbType.UniqueIdentifier ) );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@EMPLOYEEPOST_GUID_ID", System.Data.SqlDbType.UniqueIdentifier ) );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@ulUserID", System.Data.SqlDbType.Int, 4 ) );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@bAssign", System.Data.SqlDbType.Bit, 1 ) );
                cmd.Parameters[ "@COMPANY_GUID_ID" ].Value = this.m_objCompany.uuidID;
                cmd.Parameters[ "@EMPLOYEEPOST_GUID_ID" ].Value = this.m_objEmploeePost.uuidID;
                cmd.Parameters[ "@ulUserID" ].Value = this.UserID;
                cmd.Parameters[ "@bAssign" ].Value = 1;
                cmd.ExecuteNonQuery();
                System.Int32 iRet = ( System.Int32 )cmd.Parameters[ "@RETURN_VALUE" ].Value;
                if( iRet == 0 )
                {
                    bRet = true;
                }
                else
                {
                    DevExpress.XtraEditors.XtraMessageBox.Show(  "������ �������� ����� ������������ - �������� - ���������", "������",
                        System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
                }
                cmd.Dispose();
            }
            catch( System.Exception e )
            {
                DevExpress.XtraEditors.XtraMessageBox.Show( 
                "������ �������� ����� ������������ - �������� - ���������.\n" + e.Message, "��������",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
            }
			finally // ������� ���������� �������
            {
                DBConnection.Close();
            }
            return bRet;
        }

        /// <summary>
        /// ��������� ��������� � ��
        /// </summary>
        /// <param name="objProfile">�������</param>
        /// <returns>true - ������� ����������; false - ������</returns>
        public override System.Boolean Update( UniXP.Common.CProfile objProfile )
        {
            System.Boolean bRet = false;

            return bRet;
        }

    }
}
