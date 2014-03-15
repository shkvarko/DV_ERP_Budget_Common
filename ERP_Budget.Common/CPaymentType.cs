using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

namespace ERP_Budget.Common
{
    /// <summary>
    /// ����� "����� ������"
    /// </summary>
    public class CPaymentType : IBaseListItem
    {
        #region ��������, ����, ��������� 
        /// <summary>
        /// ����������
        /// </summary>
        private System.String m_strDescription;
        /// <summary>
        /// ���
        /// </summary>
        [DisplayName( "����������" )]
        [Description( "�������� �������" )]
        [Category( "�������������� ��������" )]
        public System.String Description
        {
            get { return m_strDescription; }
            set { m_strDescription = value; }
        }

        #endregion 

        #region ����������� 
        public CPaymentType()
        {
            this.m_uuidID = System.Guid.Empty;
            this.m_strName = "";
            this.m_strDescription = "";
        }

        public CPaymentType( System.Guid uuidID, System.String strName )
        {
            this.m_uuidID = uuidID;
            this.m_strName = strName;
            this.m_strDescription = "";
        }

        public CPaymentType( System.Guid uuidID, System.String strName, System.String strDescription )
        {
            this.m_uuidID = uuidID;
            this.m_strName = strName;
            this.m_strDescription = strDescription;
        }

        #endregion 

        #region ������ ���� ������ 
        /// <summary>
        /// ���������� ������ ���� ������
        /// </summary>
        /// <param name="objProfile">�������</param>
        /// <returns>������ ���� ������</returns>
        public CBaseList<CPaymentType> GetPaymentTypeList( UniXP.Common.CProfile objProfile )
        {
            CBaseList<CPaymentType> objList = new CBaseList<CPaymentType>();

            System.Data.SqlClient.SqlConnection DBConnection = objProfile.GetDBSource();
            if( DBConnection == null ) { return objList; }

            try
            {
                // ���������� � �� ��������, ����������� ������� �� ������� ������
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
                cmd.Connection = DBConnection;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = System.String.Format( "[{0}].[dbo].[sp_GetPaymentType]", objProfile.GetOptionsDllDBName() );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ( ( System.Byte )( 0 ) ), ( ( System.Byte )( 0 ) ), "", System.Data.DataRowVersion.Current, null ) );
                System.Data.SqlClient.SqlDataReader rs = cmd.ExecuteReader();
                if( rs.HasRows )
                {
                    // ����� ������ ��������
                    CPaymentType objPaymentType = null;
                    while( rs.Read() )
                    {
                        objPaymentType = new CPaymentType();
                        objPaymentType.m_uuidID = rs.GetGuid( 0 );
                        objPaymentType.m_strName = rs.GetString( 1 );
                        if( rs[ 2 ] != System.DBNull.Value )
                        {
                            objPaymentType.m_strDescription = rs.GetString( 2 );
                        }
                        objList.AddItemToList( objPaymentType );
                    }
                }
                else
                {
                    //DevExpress.XtraEditors.XtraMessageBox.Show( 
                    //"�� ������� �������� ������ ���� ������.\n� �� �� ������� ����������.", "��������",
                    //System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning );

                }
                cmd.Dispose();
                rs.Dispose();
            }
            catch( System.Exception e )
            {
                DevExpress.XtraEditors.XtraMessageBox.Show( 
                "�� ������� �������� ������ ���� ������.\n" + e.Message, "��������",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
            }
			finally // ������� ���������� �������
            {
                DBConnection.Close();
            }
            return objList;
        }

        /// <summary>
        /// ���������� ������ ���� ������
        /// </summary>
        /// <param name="objProfile">�������</param>
        /// <returns>������ ���� ������</returns>
        public static System.Collections.Generic.List<CPaymentType> GetPaymentTypesList( UniXP.Common.CProfile objProfile )
        {
            System.Collections.Generic.List<CPaymentType> objList = new System.Collections.Generic.List<CPaymentType>();

            System.Data.SqlClient.SqlConnection DBConnection = objProfile.GetDBSource();
            if( DBConnection == null ) { return objList; }

            try
            {
                // ���������� � �� ��������, ����������� ������� �� ������� ������
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
                cmd.Connection = DBConnection;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = System.String.Format( "[{0}].[dbo].[sp_GetPaymentType]", objProfile.GetOptionsDllDBName() );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ( ( System.Byte )( 0 ) ), ( ( System.Byte )( 0 ) ), "", System.Data.DataRowVersion.Current, null ) );
                System.Data.SqlClient.SqlDataReader rs = cmd.ExecuteReader();
                if( rs.HasRows )
                {
                    System.String strDecrpn = "";
                    while( rs.Read() )
                    {
                        strDecrpn = ( rs[ 2 ] == System.DBNull.Value ) ? "" : ( System.String )rs[ 2 ];
                        objList.Add( new CPaymentType( rs.GetGuid( 0 ), rs.GetString( 1 ), strDecrpn ) );
                    }
                }
                rs.Close();
                rs.Dispose();
                cmd.Dispose();
            }
            catch( System.Exception f )
            {
                DevExpress.XtraEditors.XtraMessageBox.Show( 
                "�� ������� �������� ������ ���� ������.\n\n����� ������: " + f.Message, "��������",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
            }
			finally // ������� ���������� �������
            {
                DBConnection.Close();
            }
            return objList;
        }

        /// <summary>
        /// ��������� ������ ���� ������
        /// </summary>
        /// <param name="objProfile">�������</param>
        /// <param name="cmd">SQL - �������</param>
        /// <param name="cmd">objList - ����������� ������</param>
        /// <returns></returns>
        public static void RefreshPaymentTypeList( UniXP.Common.CProfile objProfile,
            System.Data.SqlClient.SqlCommand cmd, System.Collections.Generic.List<CPaymentType> objList )
        {
            if( cmd == null ) { return; }
            objList.Clear();

            try
            {
                cmd.Parameters.Clear();
                cmd.CommandText = System.String.Format( "[{0}].[dbo].[sp_GetPaymentType]", objProfile.GetOptionsDllDBName() );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ( ( System.Byte )( 0 ) ), ( ( System.Byte )( 0 ) ), "", System.Data.DataRowVersion.Current, null ) );
                System.Data.SqlClient.SqlDataReader rs = cmd.ExecuteReader();
                if( rs.HasRows )
                {
                    System.String strDecrpn = "";
                    while( rs.Read() )
                    {
                        strDecrpn = ( rs[ 2 ] == System.DBNull.Value ) ? "" : ( System.String )rs[ 2 ];
                        objList.Add( new CPaymentType( rs.GetGuid( 0 ), rs.GetString( 1 ), strDecrpn ) );
                    }
                }
                rs.Close();
                rs.Dispose();
            }
            catch( System.Exception e )
            {
                DevExpress.XtraEditors.XtraMessageBox.Show( 
                "�� ������� �������� ������ ���� ������.\n����� ������: " + e.Message, "��������",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
            }
			finally // ������� ���������� �������
            {
            }
            return ;
        }

        /// <summary>
        /// ��������� ������ �� ������� ���� ������
        /// </summary>
        /// <param name="objProfile">�������</param>
        /// <param name="objTreeList">������</param>
        /// <returns>true - ������� ����������; false - ������</returns>
        public static System.Boolean bRefreshPaymentTypeTree( UniXP.Common.CProfile objProfile,
            DevExpress.XtraTreeList.TreeList objTreeList )
        {
            System.Boolean bRet = false;
            // ������� ������
            objTreeList.Nodes.Clear();
            // ����������� ���������� � ��
            System.Data.SqlClient.SqlConnection DBConnection = objProfile.GetDBSource();
            if( DBConnection == null )
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(
                    "�� ������� �������� ������ ���� ������.\n����������� ���������� � ��.", "��������",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
                return bRet;
            }

            try
            {
                // ���������� � �� ��������, ����������� ������� �� ������� ������
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
                cmd.Connection = DBConnection;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = System.String.Format( "[{0}].[dbo].[sp_GetPaymentType]", objProfile.GetOptionsDllDBName() );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ( ( System.Byte )( 0 ) ), ( ( System.Byte )( 0 ) ), "", System.Data.DataRowVersion.Current, null ) );
                System.Data.SqlClient.SqlDataReader rs = cmd.ExecuteReader();
                if( rs.HasRows )
                {
                    // ����� ������ ��������
                    CPaymentType objPaymentType = null;
                    System.String strDecrpn = "";
                    while( rs.Read() )
                    {
                        strDecrpn = ( rs[ "PAYMENTTYPE_DESCRIPTION" ] == System.DBNull.Value ) ? "" : ( System.String )rs[ "PAYMENTTYPE_DESCRIPTION" ];
                        // ������� ������ ������ CEmployeePost
                        objPaymentType = new CPaymentType( ( System.Guid )rs[ "GUID_ID" ],
                            ( System.String )rs[ "PAYMENTTYPE_NAME" ], strDecrpn );
                        // ������� ���� ������
                        DevExpress.XtraTreeList.Nodes.TreeListNode objNode = 
                                objTreeList.AppendNode( new object[] { objPaymentType.Name }, null );
                        objNode.Tag = objPaymentType;

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
                "�� ������� �������� ������ ���� ������.\n\n����� ������: " + f.Message, "��������",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
            }
			finally // ������� ���������� �������
            {
                DBConnection.Close();
            }

            return bRet;
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
                cmd.CommandText = System.String.Format( "[{0}].[dbo].[sp_GetPaymentType]", objProfile.GetOptionsDllDBName() );
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
                    if( rs[ 2 ] != System.DBNull.Value )
                    {
                        this.m_strDescription = rs.GetString( 2 );
                    }
                    bRet = true;
                }
                else
                {
                    DevExpress.XtraEditors.XtraMessageBox.Show( 
                    "�� ������� ������������������� ����� CPaymentType.\n� �� �� ������� ����������.", "��������",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning );

                }
                cmd.Dispose();
                rs.Dispose();
            }
            catch( System.Exception e )
            {
                DevExpress.XtraEditors.XtraMessageBox.Show( 
                "�� ������� ������������������� ����� CPaymentType.\n" + e.Message, "��������",
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
                DevExpress.XtraEditors.XtraMessageBox.Show( "������������ �������� ����������� �������������� �������", "��������",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
                return bRet;
            }

            System.Data.SqlClient.SqlConnection DBConnection = objProfile.GetDBSource();
            if( DBConnection == null )
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(
                "�� ������� ���������� ���������� � ����� ������.", "��������",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
                return bRet;
            }
            System.Data.SqlClient.SqlTransaction DBTransaction = DBConnection.BeginTransaction();

            try
            {
                // ���������� � �� ��������, ����������� ������� �� �������� ������ � ��
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
                cmd.Connection = DBConnection;
                cmd.Transaction = DBTransaction;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = System.String.Format( "[{0}].[dbo].[sp_DeletePaymentType]", objProfile.GetOptionsDllDBName() );
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
                    if( iRet == 1 )
                    {
                        DevExpress.XtraEditors.XtraMessageBox.Show(  "�� ����� ������ ���� ������ � ��������� ���������.\n�������� ����������", "��������",
                            System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning );
                    }
                    else
                    {
                        DevExpress.XtraEditors.XtraMessageBox.Show(  "������ �������� ����� ������", "������",
                            System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
                    }
                }
                cmd.Dispose();
            }
            catch( System.Exception e )
            {
                DBTransaction.Rollback();
                DevExpress.XtraEditors.XtraMessageBox.Show( 
                "�� ������� ������� ����� ������.\n" + e.Message, "��������",
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
                    DevExpress.XtraEditors.XtraMessageBox.Show( "������������ �������� ������������ �������", "��������",
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
            if( DBConnection == null )
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(
                "�� ������� ���������� ���������� � ����� ������.", "��������",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
                return bRet;
            }
            System.Data.SqlClient.SqlTransaction DBTransaction = DBConnection.BeginTransaction();

            try
            {
                // ���������� � �� ��������, ����������� ������� �� �������� ������ � ��
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
                cmd.Connection = DBConnection;
                cmd.Transaction = DBTransaction;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = System.String.Format( "[{0}].[dbo].[sp_AddPaymentType]", objProfile.GetOptionsDllDBName() );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ( ( System.Byte )( 0 ) ), ( ( System.Byte )( 0 ) ), "", System.Data.DataRowVersion.Current, null ) );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@GUID_ID", System.Data.SqlDbType.UniqueIdentifier, 4, System.Data.ParameterDirection.Output, false, ( ( System.Byte )( 0 ) ), ( ( System.Byte )( 0 ) ), "", System.Data.DataRowVersion.Current, null ) );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@PAYMENTTYPE_NAME", System.Data.DbType.String ) );
                if( this.m_strDescription != "" )
                {
                    cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@PAYMENTTYPE_DESCRIPTION", System.Data.DbType.String ) );
                    cmd.Parameters[ "@PAYMENTTYPE_DESCRIPTION" ].Value = this.m_strDescription;
                }
                cmd.Parameters[ "@PAYMENTTYPE_NAME" ].Value = this.m_strName;
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
                    if( iRet == 1 )
                    {
                        DevExpress.XtraEditors.XtraMessageBox.Show(  "����� ������ '" + this.m_strName + "' ��� ���� � ��", "��������",
                            System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning );
                    }
                    else
                    {
                        DevExpress.XtraEditors.XtraMessageBox.Show(  "������ �������� ����� ������", "������",
                            System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
                    }
                }
                cmd.Dispose();
            }
            catch( System.Exception e )
            {
                // ���������� ����������
                DBTransaction.Rollback();
                DevExpress.XtraEditors.XtraMessageBox.Show( 
                "�� ������� ������� ����� ������.\n" + e.Message, "��������",
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
            if( IsValidateProperties() == false ) { return bRet; }

            System.Data.SqlClient.SqlConnection DBConnection = objProfile.GetDBSource();
            if( DBConnection == null )
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(
                "�� ������� ���������� ���������� � ����� ������.", "��������",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
                return bRet;
            }
            System.Data.SqlClient.SqlTransaction DBTransaction = DBConnection.BeginTransaction();

            try
            {
                // ���������� � �� ��������, ����������� ������� �� �������� ������ � ��
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
                cmd.Connection = DBConnection;
                cmd.Transaction = DBTransaction;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = System.String.Format( "[{0}].[dbo].[sp_EditPaymentType]", objProfile.GetOptionsDllDBName() );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ( ( System.Byte )( 0 ) ), ( ( System.Byte )( 0 ) ), "", System.Data.DataRowVersion.Current, null ) );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@GUID_ID", System.Data.SqlDbType.UniqueIdentifier ) );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@PAYMENTTYPE_NAME", System.Data.DbType.String ) );
                if( this.m_strDescription != "" )
                {
                    cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@PAYMENTTYPE_DESCRIPTION", System.Data.DbType.String ) );
                    cmd.Parameters[ "@PAYMENTTYPE_DESCRIPTION" ].Value = this.m_strDescription;
                }

                cmd.Parameters[ "@GUID_ID" ].Value = this.m_uuidID;
                cmd.Parameters[ "@PAYMENTTYPE_NAME" ].Value = this.m_strName;
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
                        DevExpress.XtraEditors.XtraMessageBox.Show(  "����� ������ '" + this.m_strName + "' ��� ���� � ��", "��������",
                            System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning );
                        break;
                        case 2:
                        DevExpress.XtraEditors.XtraMessageBox.Show(  "������ � ��������� ��������������� �� ������� \n" + this.m_uuidID.ToString(), "��������",
                            System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
                        break;
                        default:
                        DevExpress.XtraEditors.XtraMessageBox.Show(  "������ ��������� ����� ������", "��������",
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
                "�� ������� �������� ����� ������.\n" + e.Message, "��������",
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
            return Name;
        }

    }
}
