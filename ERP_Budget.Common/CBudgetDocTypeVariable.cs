using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace ERP_Budget.Common
{
    /// <summary>
    /// ����� "���������� ��� ������ ���� ���������"
    /// </summary>
    public class CBudgetDocTypeVariable : IBaseListItem
    {
        #region ����������, ��������, ���������
        /// <summary>
        /// ������������ ������, � ������� ������� ����������
        /// </summary>
        private System.String m_strEditClassName;
        /// <summary>
        /// ������������ ������, � ������� ������� ����������
        /// </summary>
        public System.String EditClassName
        {
            get { return m_strEditClassName; }
            set { m_strEditClassName = value; }
        }
        /// <summary>
        /// ������ ����������� ��������� ��� ������������ ������� ������ ��������
        /// </summary>
        private System.String m_strPatternSrc;
        /// <summary>
        /// ������ ����������� ��������� ��� ������������ ������� ������ ��������
        /// </summary>
        public System.String PatternSrc
        {
            get { return m_strPatternSrc; }
            set { m_strPatternSrc = value; }
        }
        /// <summary>
        /// ������� ��� ������� ����������� ���������
        /// </summary>
        private System.String m_strPattern;
        /// <summary>
        /// ������� ��� ������� ����������� ���������
        /// </summary>
        public System.String Pattern
        {
            get { return m_strPattern; }
            set { m_strPattern = value; }
        }
        /// <summary>
        /// ��� ������
        /// </summary>
        private System.String m_strDataTypeName;
        /// <summary>
        /// ��� ������
        /// </summary>
        public System.String DataTypeName
        {
            get { return m_strDataTypeName; }
            set { m_strDataTypeName = value; }
        }
        public System.String m_strEditName;
        /// <summary>
        /// ������� �������� ����������
        /// </summary>
        public System.String m_strValue;

        #endregion

        #region ������������
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

        #region ������ ����������
        /// <summary>
        /// ���������� ������ ����������
        /// </summary>
        /// <param name="objProfile">�������</param>
        /// <returns>������ ������� ������ ��������</returns>
        public static List<CBudgetDocTypeVariable> GetDocTypeVariableList( UniXP.Common.CProfile objProfile )
        {
            List<CBudgetDocTypeVariable> objList = new List<CBudgetDocTypeVariable>();
            try
            {
                System.Data.SqlClient.SqlConnection DBConnection = objProfile.GetDBSource();
                if( DBConnection == null ) { return objList; }

                // ���������� � �� ��������, ����������� ������� �� ������� ������
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
                "�� ������� �������� ������ ����������.\n\n����� ������: " + f.Message, "��������",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
            }
            return objList;
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
                cmd.CommandText = System.String.Format( "[{0}].[dbo].[sp_GetDocTypeVariable]", objProfile.GetOptionsDllDBName() );
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
                    this.m_strEditClassName = rs.GetString( 2 );
                    this.m_strPatternSrc = rs.GetString( 3 );
                    this.m_strPattern = rs.GetString( 4 );
                    this.m_strDataTypeName = rs.GetString( 5 );
                    bRet = true;
                }
                else
                {
                    DevExpress.XtraEditors.XtraMessageBox.Show( 
                    "�� ������� ������������������� ����� CBudgetDocTypeVariable.\n� �� �� ������� ����������.", "��������",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning );

                }
                cmd.Dispose();
                rs.Dispose();
            }
            catch( System.Exception e )
            {
                DevExpress.XtraEditors.XtraMessageBox.Show( 
                "�� ������� ������������������� ����� CBudgetDocTypeVariable.\n\n����� ������: " + e.Message, "��������",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
            }
			finally // ������� ���������� �������
            {
                DBConnection.Close();
            }
            return bRet;
        }

        /// <summary>
        /// ������������� ������� ������
        /// </summary>
        /// <param name="cmd">SQL-�������</param>
        /// <param name="objProfile">�������</param>
        /// <param name="uuidID">���������� ������������� ������</param>
        /// <returns>true - �������� �������������; false - ������</returns>
        public System.Boolean Init( System.Data.SqlClient.SqlCommand cmd,
            UniXP.Common.CProfile objProfile, System.Guid uuidID )
        {
            System.Boolean bRet = false;
            if( ( cmd == null ) || ( cmd.Connection == null ) ) { return bRet; }

            try
            {
                // ���������� � �� ��������, ����������� ������� �� ������� ������
                cmd.Parameters.Clear();
                cmd.CommandText = System.String.Format( "[{0}].[dbo].[sp_GetDocTypeVariable]", objProfile.GetOptionsDllDBName() );
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
                    this.m_strEditClassName = rs.GetString( 2 );
                    this.m_strPatternSrc = rs.GetString( 3 );
                    this.m_strPattern = rs.GetString( 4 );
                    this.m_strDataTypeName = rs.GetString( 5 );
                    bRet = true;
                }
                else
                {
                    DevExpress.XtraEditors.XtraMessageBox.Show( 
                    "�� ������� ������������������� ����� CBudgetDocTypeVariable.\n� �� �� ������� ����������.", "��������",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning );

                }
                rs.Dispose();
            }
            catch( System.Exception f )
            {
                DevExpress.XtraEditors.XtraMessageBox.Show( 
                "�� ������� ������������������� ����� CBudgetDocTypeVariable.\n\n����� ������: " + f.Message, "��������",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
            }
			finally // ������� ���������� �������
            {
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
            System.Data.SqlClient.SqlTransaction DBTransaction = DBConnection.BeginTransaction();

            try
            {
                // ���������� � �� ��������, ����������� ������� �� �������� ������
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
                    // ������������ ����������
                    DBTransaction.Commit();
                    bRet = true;
                }
                else
                {
                    // ���������� ����������
                    DBTransaction.Rollback();
                    switch( iRet )
                    {
                        case 1:
                        {
                            DevExpress.XtraEditors.XtraMessageBox.Show(  "�� ������ � �������� ��������������� ���� ������.\n" + this.uuidID.ToString(), "��������",
                                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning );
                            break;
                        }
                        default:
                        {
                            DevExpress.XtraEditors.XtraMessageBox.Show(  "������ ���������� ������� �� �������� ����������.\n���������� : " + this.m_strName, "��������",
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
                "������ ���������� ������� �� �������� ����������.\n���������� : " + 
                this.m_strName + "\n\n����� ������: " + f.Message, "��������",
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
        public System.Boolean Remove( System.Data.SqlClient.SqlCommand cmd, UniXP.Common.CProfile objProfile )
        {
            System.Boolean bRet = false;
            if( ( cmd == null ) || ( cmd.Connection == null ) ) { return bRet; }
            // ���������� ������������� �� ������ ���� ������
            if( this.m_uuidID == System.Guid.Empty )
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(  "������������ �������� ����������� �������������� �������", "��������",
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
                            DevExpress.XtraEditors.XtraMessageBox.Show(  "�� ������ � �������� ��������������� ���� ������.\n" + this.uuidID.ToString(), "��������",
                                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning );
                            break;
                        }
                        default:
                        {
                            DevExpress.XtraEditors.XtraMessageBox.Show(  "������ ���������� ������� �� �������� ����������.\n���������� : " + this.m_strName, "��������",
                                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
                            break;
                        }
                    }
                }
            }
            catch( System.Exception f )
            {
                DevExpress.XtraEditors.XtraMessageBox.Show( 
                "������ ���������� ������� �� �������� ����������.\n���������� : " + 
                this.m_strName + "\n\n����� ������: " + f.Message, "��������",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
            }
			finally // ������� ���������� �������
            {
            }
            return bRet;
        }
        #endregion

        #region Add
        /// <summary>
        /// ��������� �������� ������� �� ������� ������������� ����������
        /// </summary>
        /// <returns>true - ������ ���; false - ������</returns>
        public System.Boolean IsValidateProperties()
        {
            System.Boolean bRet = false;
            try
            {
                // ������������ �� ������ ���� ������
                if( this.m_strName == "" )
                {
                    DevExpress.XtraEditors.XtraMessageBox.Show(  "������������ �������� ������������ �������", "��������",
                        System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
                    return bRet;
                }

                // ������������ ������ �� ������ ���� ������
                if( this.m_strEditClassName.Trim() == "" )
                {
                    DevExpress.XtraEditors.XtraMessageBox.Show(  "������������ ������, ���������� � ���������� �� ������ ���� ������" + this.m_strEditClassName, "��������",
                        System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
                    return bRet;
                }

                // ������������ ���� ������ �� ������ ���� ������
                if( this.m_strDataTypeName.Trim() == "" )
                {
                    DevExpress.XtraEditors.XtraMessageBox.Show(  "������������ ���� ������ �� ������ ���� ������" + this.m_strDataTypeName, "��������",
                        System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
                    return bRet;
                }

                // ������ �� ������ ���� ������
                if( this.m_strPatternSrc.Trim() == "" )
                {
                    DevExpress.XtraEditors.XtraMessageBox.Show(  "������ ����������� ��������� ��� �������� ������� ������ ��������\n�� ������ ���� ������" + this.m_strPatternSrc, "��������",
                        System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
                    return bRet;
                }

                // ������ �� ������ ���� ������
                if( this.m_strPattern.Trim() == "" )
                {
                    DevExpress.XtraEditors.XtraMessageBox.Show(  "������ ����������� ��������� \n��� �������� ���������� ������� ������ �������� \n�� ������ ���� ������" + this.m_strPattern, "��������",
                        System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
                    return bRet;
                }
                bRet = true;
            }
            catch( System.Exception f )
            {
                DevExpress.XtraEditors.XtraMessageBox.Show( 
                "������ �������� ������� �������.\n\n����� ������: " + f.Message, "��������",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
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
            if( IsValidateProperties() == false ) { return bRet; }

            System.Data.SqlClient.SqlConnection DBConnection = objProfile.GetDBSource();
            if( DBConnection == null ) { return bRet; }
            System.Data.SqlClient.SqlTransaction DBTransaction = DBConnection.BeginTransaction();
            try
            {
                // ���������� � �� ��������, ����������� ������� �� �������� ������ � ��
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
                    // ������������ ����������
                    DBTransaction.Commit();
                    this.m_uuidID = ( System.Guid )cmd.Parameters[ "@GUID_ID" ].Value;
                    bRet = true;
                }
                else
                {
                    // ���������� ����������
                    DBTransaction.Rollback();
                    switch( iRet )
                    {
                        case 1:
                        {
                            DevExpress.XtraEditors.XtraMessageBox.Show(  "���������� � �������� ������ ���������� : '" + this.m_strName + "'", "��������",
                                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning );
                            break;
                        }
                        default:
                        {
                            DevExpress.XtraEditors.XtraMessageBox.Show(  "������ �������� ���������� : " + this.m_strName, "������",
                                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
                            break;
                        }
                    }
                }
                cmd.Dispose();
            }
            catch( System.Exception e )
            {
                // ���������� ����������
                DBTransaction.Rollback();
                DevExpress.XtraEditors.XtraMessageBox.Show( 
                "�� ������� ������� ����������\n" + this.m_strName + "\n" + e.Message, "��������",
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
        /// <param name="cmd">SQL-�������</param>
        /// <param name="objProfile">�������</param>
        /// <returns>true - ������� ����������; false - ������</returns>
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
                            DevExpress.XtraEditors.XtraMessageBox.Show(  "���������� � �������� ������ ���������� : '" + this.m_strName + "'", "��������",
                                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning );
                            break;
                        }
                        default:
                        {
                            DevExpress.XtraEditors.XtraMessageBox.Show(  "������ �������� ���������� : " + this.m_strName, "������",
                                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
                            break;
                        }
                    }
                }
            }
            catch( System.Exception e )
            {
                DevExpress.XtraEditors.XtraMessageBox.Show( 
                "�� ������� ������� ����������\n" + this.m_strName + "\n" + e.Message, "��������",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
            }
			finally // ������� ���������� �������
            {
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

            if( IsValidateProperties() == false ) { return bRet; }

            System.Data.SqlClient.SqlConnection DBConnection = objProfile.GetDBSource();
            if( DBConnection == null ) { return bRet; }
            System.Data.SqlClient.SqlTransaction DBTransaction = DBConnection.BeginTransaction();
            try
            {
                // ���������� � �� ��������, ����������� ������� �� �������� ������ � ��
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
                    // ������������ ����������
                    DBTransaction.Commit();
                    bRet = true;
                }
                else
                {
                    // ���������� ����������
                    DBTransaction.Rollback();
                    switch( iRet )
                    {
                        case 1:
                        DevExpress.XtraEditors.XtraMessageBox.Show(  "���������� � �������� ������ ���������� : '" + this.m_strName + "'", "��������",
                            System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning );
                        break;
                        case 2:
                        DevExpress.XtraEditors.XtraMessageBox.Show(  "������ � ��������� ��������������� �� ������� \n" + this.m_uuidID.ToString(), "��������",
                            System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
                        break;
                        default:
                        DevExpress.XtraEditors.XtraMessageBox.Show(  "������ ��������� ������� ����������  : " + this.m_strName, "��������",
                            System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
                        break;
                    }
                }
                cmd.Dispose();
            }
            catch( System.Exception e )
            {
                // ���������� ����������
                DBTransaction.Rollback();
                DevExpress.XtraEditors.XtraMessageBox.Show( 
                "������ ��������� ������� ���������� : " + this.m_strName + "\n" + e.Message, "��������",
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
        /// <param name="cmd">SQL-�������</param>
        /// <param name="objProfile">�������</param>
        /// <returns>true - ������� ����������; false - ������</returns>
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
                        DevExpress.XtraEditors.XtraMessageBox.Show(  "���������� � �������� ������ ���������� : '" + this.m_strName + "'", "��������",
                            System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning );
                        break;
                        case 2:
                        DevExpress.XtraEditors.XtraMessageBox.Show(  "������ � ��������� ��������������� �� ������� \n" + this.m_uuidID.ToString(), "��������",
                            System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
                        break;
                        default:
                        DevExpress.XtraEditors.XtraMessageBox.Show(  "������ ��������� ������� ����������  : " + this.m_strName, "��������",
                            System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
                        break;
                    }
                }
            }
            catch( System.Exception e )
            {
                DevExpress.XtraEditors.XtraMessageBox.Show( 
                "������ ��������� ������� ���������� : " + this.m_strName + "\n" + e.Message, "��������",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
            }
			finally // ������� ���������� �������
            {
            }
            return bRet;
        }
        #endregion
    }

    #region ������ ��� ��������� ����������, ��������� � �����������
    /// <summary>
    /// ������������ ����� ��� ����� ���������� � ��������� ����������
    /// </summary>
    public interface IBudgetDocTypeVariableEdit
    {
        System.String GetValue();
    }
    /// <summary>
    /// �����, ����������� ���������� "������� �������" c  ��������� ����������
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
    /// �����, ����������� ���������� "������������ �����" c  ��������� ����������
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
    /// �����, ����������� ���������� "������� �����" c  ��������� ����������
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
    /// �����, ����������� ���������� "������ ��������" c  ��������� ����������
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
