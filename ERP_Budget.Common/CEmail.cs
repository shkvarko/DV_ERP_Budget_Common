using System;
using System.Collections.Generic;
using System.Text;

namespace ERP_Budget.Common
{
    /// <summary>
    /// ����� "����������� �����"
    /// </summary>
    public class CEmail : IBaseListItem
    {
        #region ����������, ��������, ���������
        /// <summary>
        /// Email �����
        /// </summary>
        private string m_strAdress;
        /// <summary>
        /// ������� "�������"
        /// </summary>
        private System.Boolean m_bActive;
        /// <summary>
        /// ������� "��������"
        /// </summary>
        private System.Boolean m_bMain;
        /// <summary>
        /// Email �����
        /// </summary>
        public string Adress
        {
            get
            {
                return m_strAdress;
            }
            set
            {
                m_strAdress = value;
            }
        }
        /// <summary>
        /// ������� "�������"
        /// </summary>
        public bool IsActive
        {
            get
            {
                return m_bActive;
            }
            set
            {
                m_bActive = value;
            }
        }
        /// <summary>
        /// ������� "��������"
        /// </summary>
        public bool IsMain
        {
            get
            {
                return m_bMain;
            }
            set
            {
                m_bMain = value;
            }
        }
        #endregion

        public CEmail()
        {
            this.m_uuidID = System.Guid.Empty;
            this.m_strName = "";
            this.m_strAdress = "";
            this.m_bActive = true;
            this.m_bMain = false;
        }

        /// <summary>
        /// ���������� ������ Email ��� ��������� ������������
        /// </summary>
        /// <param name="objProfile">�������</param>
        /// <param name="iUserID">������������� ������������</param>
        /// <returns>������ Email ��� ��������� ������������</returns>
        public CBaseList<CEmail> GetEmailList( UniXP.Common.CProfile objProfile, 
            System.Int32 iUserID )
        {
            CBaseList<CEmail> objList = new CBaseList<CEmail>();

            System.Data.SqlClient.SqlConnection DBConnection = objProfile.GetDBSource();
            if( DBConnection == null ) { return objList; }

            try
            {
                // ���������� � �� ��������, ����������� ������� �� ������� ������
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
                cmd.Connection = DBConnection;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = System.String.Format( "[{0}].[dbo].[sp_GetUserEmail]", objProfile.GetOptionsDllDBName() );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ( ( System.Byte )( 0 ) ), ( ( System.Byte )( 0 ) ), "", System.Data.DataRowVersion.Current, null ) );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@ulUserID", System.Data.SqlDbType.Int, 4 ) );
                cmd.Parameters[ "@ulUserID" ].Value = iUserID;
                System.Data.SqlClient.SqlDataReader rs = cmd.ExecuteReader();
                if( rs.HasRows )
                {
                    // ����� ������ ��������
                    CEmail objEmail = null;
                    while( rs.Read() )
                    {
                        objEmail = new CEmail();
                        objEmail.m_uuidID = rs.GetGuid( 0 );
                        objEmail.m_strName = rs.GetString( 1 );
                        objEmail.m_strAdress = rs.GetString( 1 );
                        objEmail.m_bActive = !rs.GetBoolean( 2 );
                        objEmail.m_bMain = rs.GetBoolean( 3 );
                        objList.AddItemToList( objEmail );
                    }
                }
                else
                {
                    //DevExpress.XtraEditors.XtraMessageBox.Show( 
                    //"�� ������� �������� ������ Email ��� ��������� ������������.\n� �� �� ������� ����������.", "��������",
                    //System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning );

                }
                cmd.Dispose();
                rs.Dispose();
            }
            catch( System.Exception e )
            {
                DevExpress.XtraEditors.XtraMessageBox.Show( 
                "�� ������� �������� ������ Email ��� ��������� ������������.\n" + e.Message, "��������",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
            }
			finally // ������� ���������� �������
            {
                DBConnection.Close();
            }
            return objList;
        }

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
                cmd.CommandText = System.String.Format( "[{0}].[dbo].[sp_GetEmail]", objProfile.GetOptionsDllDBName() );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ( ( System.Byte )( 0 ) ), ( ( System.Byte )( 0 ) ), "", System.Data.DataRowVersion.Current, null ) );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@GUID_ID", System.Data.SqlDbType.UniqueIdentifier ) );
                cmd.Parameters[ "@GUID_ID" ].Value = uuidID;
                System.Data.SqlClient.SqlDataReader rs = cmd.ExecuteReader();
                if( rs.HasRows )
                {
                    // ����� ������ ��������, � ��� ��� ���������� ���� ������
                    rs.Read();
                    this.m_uuidID = rs.GetGuid( 0 );
                    this.m_strName = rs.GetString( 1 );
                    this.m_strAdress = rs.GetString( 1 );
                    this.m_bActive = !rs.GetBoolean( 2 );
                    bRet = true;
                }
                else
                {
                    DevExpress.XtraEditors.XtraMessageBox.Show( 
                    "�� ������� ������������������� ����� CMeasure.\n� �� �� ������� ����������.", "��������",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning );

                }
                cmd.Dispose();
                rs.Dispose();
            }
            catch( System.Exception e )
            {
                DevExpress.XtraEditors.XtraMessageBox.Show( 
                "�� ������� ������������������� ����� CEmail.\n" + e.Message, "��������",
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
                cmd.CommandText = System.String.Format( "[{0}].[dbo].[sp_DeleteEmail]", objProfile.GetOptionsDllDBName() );
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
                    DevExpress.XtraEditors.XtraMessageBox.Show(  "������ �������� email", "������",
                        System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
                }
                cmd.Dispose();
            }
            catch( System.Exception e )
            {
                DevExpress.XtraEditors.XtraMessageBox.Show( 
                "�� ������� ������� email.\n" + e.Message, "��������",
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

            // ������������ �� ������ ���� ������
            if( this.m_strAdress.Length == 0 )
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(  "������������ �������� �����", "��������",
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
                cmd.CommandText = System.String.Format( "[{0}].[dbo].[sp_AddEmail]", objProfile.GetOptionsDllDBName() );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ( ( System.Byte )( 0 ) ), ( ( System.Byte )( 0 ) ), "", System.Data.DataRowVersion.Current, null ) );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@GUID_ID", System.Data.SqlDbType.UniqueIdentifier, 4, System.Data.ParameterDirection.Output, false, ( ( System.Byte )( 0 ) ), ( ( System.Byte )( 0 ) ), "", System.Data.DataRowVersion.Current, null ) );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@EMAIL_ADDRESS", System.Data.DbType.String ) );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@EMAIL_NOTACTIVE", System.Data.SqlDbType.Bit, 1 ) );

                cmd.Parameters[ "@EMAIL_ADDRESS" ].Value = this.Adress;
                cmd.Parameters[ "@EMAIL_NOTACTIVE" ].Value = !( this.IsActive );
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
                        DevExpress.XtraEditors.XtraMessageBox.Show(  "email '" + this.m_strName + "' ��� ���� � ��", "��������",
                            System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning );
                    }
                    else
                    {
                        DevExpress.XtraEditors.XtraMessageBox.Show(  "������ �������� email", "������",
                            System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
                    }
                }
                cmd.Dispose();
            }
            catch( System.Exception e )
            {
                DevExpress.XtraEditors.XtraMessageBox.Show( 
                "�� ������� ������� email.\n" + e.Message, "��������",
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

            // ���������� ������������� �� ������ ���� ������
            if( this.m_uuidID == System.Guid.Empty )
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(  "������������ �������� ����������� �������������� �������", "��������",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
                return bRet;
            }
            // ������������ �� ������ ���� ������
            if( this.Adress.Length == 0 )
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(  "������������ �������� �����", "��������",
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
                cmd.CommandText = System.String.Format( "[{0}].[dbo].[sp_EditEmail]", objProfile.GetOptionsDllDBName() );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ( ( System.Byte )( 0 ) ), ( ( System.Byte )( 0 ) ), "", System.Data.DataRowVersion.Current, null ) );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@GUID_ID", System.Data.SqlDbType.UniqueIdentifier ) );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@EMAIL_ADDRESS", System.Data.DbType.String ) );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@EMAIL_NOTACTIVE", System.Data.SqlDbType.Bit, 1 ) );

                cmd.Parameters[ "@GUID_ID" ].Value = this.m_uuidID;
                cmd.Parameters[ "@EMAIL_ADDRESS" ].Value = this.Adress;
                cmd.Parameters[ "@EMAIL_NOTACTIVE" ].Value = !( this.IsActive );
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
                        DevExpress.XtraEditors.XtraMessageBox.Show(  "email '" + this.m_strName + "' ��� ���� � ��", "��������",
                            System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning );
                        break;
                        case 2:
                        DevExpress.XtraEditors.XtraMessageBox.Show(  "������ � ��������� ��������������� �� ������� \n" + this.m_uuidID.ToString(), "��������",
                            System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
                        break;
                        default:
                        DevExpress.XtraEditors.XtraMessageBox.Show(  "������ ��������� email", "��������",
                            System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
                        break;
                    }
                }
                cmd.Dispose();
            }
            catch( System.Exception e )
            {
                DevExpress.XtraEditors.XtraMessageBox.Show( 
                "�� ������� �������� email.\n" + e.Message, "��������",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
            }
			finally // ������� ���������� �������
            {
                DBConnection.Close();
            }
            return bRet;
        }

    }
}
