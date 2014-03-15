using System;
using System.Collections.Generic;
using System.Text;

namespace ERP_Budget.Common
{
    /// <summary>
    /// ����� "�������� - ��� ��������� - ������������ �����"
    /// </summary>
    public class CAccountTrnEvent : IBaseListItem
    {
        #region ����������, ��������, ��������� 
        /// <summary>
        /// ��������
        /// </summary>
        private CBudgetDocEvent m_objBudgetDocEvent;
        /// <summary>
        /// ��������
        /// </summary>
        public CBudgetDocEvent BudgetDocEvent
        {
            get { return m_objBudgetDocEvent; }
            set { m_objBudgetDocEvent = value; }
        }
        /// <summary>
        /// ��� ���������� ���������
        /// </summary>
        private CBudgetDocType m_objBudgetDocType;
        /// <summary>
        /// ��� ���������� ���������
        /// </summary>
        public CBudgetDocType BudgetDocType
        {
            get { return m_objBudgetDocType; }
            set { m_objBudgetDocType = value; }
        }
        /// <summary>
        /// ������������ �����
        /// </summary>
        private CDynamicRight m_objDynamicRight;
        /// <summary>
        /// ������������ �����
        /// </summary>
        public CDynamicRight DynamicRight
        {
            get { return m_objDynamicRight; }
            set { m_objDynamicRight = value; }
        }
        /// <summary>
        /// ������ ����� ��������� ��������
        /// </summary>
        private List<CAccountTrnType> m_AccountTrnTypeList;
        /// <summary>
        /// ������ ����� ��������� ��������
        /// </summary>
        public List<CAccountTrnType> AccountTrnTypeList
        {
            get { return m_AccountTrnTypeList; }
            set { m_AccountTrnTypeList = value; }
        }
        #endregion 

        #region ������������ 
        public CAccountTrnEvent()
        {
            this.m_uuidID = System.Guid.Empty;
            this.m_strName = "";
            this.m_objBudgetDocEvent = null;
            this.m_objBudgetDocType = null;
            this.m_objDynamicRight = null;
            this.m_AccountTrnTypeList = new List<CAccountTrnType>();
        }
        public CAccountTrnEvent( System.Guid uuidID, System.String strName, CBudgetDocEvent objBudgetDocEvent,
           CBudgetDocType objBudgetDocType, CDynamicRight objDynamicRight )
        {
            this.m_uuidID = uuidID;
            this.m_strName = strName;
            this.m_objBudgetDocEvent = objBudgetDocEvent;
            this.m_objBudgetDocType = objBudgetDocType;
            this.m_objDynamicRight = objDynamicRight;
            this.m_AccountTrnTypeList = new List<CAccountTrnType>();
        }
        #endregion 

        #region ������ �������� "�������� - ��� ��������� - ������������ �����" 
        /// <summary>
        /// ���������� ������ �������� "�������� - ��� ��������� - ������������ �����"
        /// </summary>
        /// <param name="objProfile">�������</param>
        /// <returns>������ �������� "�������� - ��� ��������� - ������������ �����"</returns>
        public static System.Collections.Generic.List<CAccountTrnEvent> GetAccountTrnEventList( UniXP.Common.CProfile objProfile )
        {
            System.Collections.Generic.List<CAccountTrnEvent> objList = new List<CAccountTrnEvent>();
            try
            {
                System.Data.SqlClient.SqlConnection DBConnection = objProfile.GetDBSource();
                if( DBConnection == null ) { return objList; }

                // ���������� � �� ��������, ����������� ������� �� ������� ������
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
                cmd.Connection = DBConnection;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = System.String.Format( "[{0}].[dbo].[sp_GetAccTrnEvent]", objProfile.GetOptionsDllDBName() );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ( ( System.Byte )( 0 ) ), ( ( System.Byte )( 0 ) ), "", System.Data.DataRowVersion.Current, null ) );
                System.Data.SqlClient.SqlDataReader rs = cmd.ExecuteReader();
                if( rs.HasRows )
                {
                    while( rs.Read() )
                    {
                        objList.Add( new CAccountTrnEvent( rs.GetGuid( 0 ), rs.GetString( 1 ), 
                            new CBudgetDocEvent( rs.GetGuid( 2 ), rs.GetString( 5 ) ), 
                            new CBudgetDocType( rs.GetGuid( 3 ), rs.GetString( 6 ) ), 
                            new CDynamicRight( rs.GetInt32( 4 ), rs.GetString( 7 ), rs.GetString( 8 ), " " ) ) );
                    }
                    rs.Close();
                    for( System.Int32 i = 0; i < objList.Count; i++ )
                    {
                        // ��������� ������ ����� ��������� ��������
                        if( objList[ i ].InitDeclaration( cmd, objProfile ) == false )
                            break;
                    }
                }
                rs.Close();
                rs.Dispose();
                cmd.Dispose();
                DBConnection.Close();
            }
            catch( System.Exception e )
            {
                DevExpress.XtraEditors.XtraMessageBox.Show( 
                "�� ������� �������� ������ �������� '�������� - ��� ��������� - ������������ �����'.\n\n����� ������: " + e.Message, "��������",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
            }
            return objList;
        }
        #endregion

        #region Init 
        /// <summary>
        /// ��������� ������ ��������� ��������
        /// </summary>
        /// <param name="cmd">SQL-�������</param>
        /// <param name="objProfile">�������</param>
        /// <returns>true - �������� ���������� ��������; false - ������</returns>
        public System.Boolean InitDeclaration( System.Data.SqlClient.SqlCommand cmd, UniXP.Common.CProfile objProfile )
        {
            System.Boolean bRet = false;

            if( cmd == null ) { return bRet; }
            try
            {
                // ���������� � �� ��������, ����������� ������� �� ������� ������
                cmd.Parameters.Clear();
                cmd.CommandText = System.String.Format( "[{0}].[dbo].[sp_GetAccTrnEventDeclrn]", objProfile.GetOptionsDllDBName() );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ( ( System.Byte )( 0 ) ), ( ( System.Byte )( 0 ) ), "", System.Data.DataRowVersion.Current, null ) );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@ACCTRNEVENT_GUID_ID", System.Data.SqlDbType.UniqueIdentifier ) );
                cmd.Parameters[ "@ACCTRNEVENT_GUID_ID" ].Value = this.uuidID;
                System.Data.SqlClient.SqlDataReader rs = cmd.ExecuteReader();
                if( rs.HasRows )
                {
                    while( rs.Read() )
                    {
                        this.m_AccountTrnTypeList.Add( new CAccountTrnType( rs.GetGuid( 1 ), 
                            rs.GetString( 2 ), rs.GetInt32( 3 ), rs.GetString( 4 ) ) );
                    }
                    bRet = true;
                }
                else
                {
                    DevExpress.XtraEditors.XtraMessageBox.Show( 
                    "�� ������� �������� ������ ����� ��������� �������� ��� " + this.Name + "\n� �� �� ������� ����������.", "��������",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning );

                }
                rs.Dispose();
            }
            catch( System.Exception e )
            {
                DevExpress.XtraEditors.XtraMessageBox.Show( 
                "�� ������� �������� ������ ����� ��������� �������� ��� " + this.Name + "\n\n" + e.Message, "��������",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
            }
            finally // ������� ���������� �������
            {
            }
            return bRet;
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
                cmd.CommandText = System.String.Format( "[{0}].[dbo].[sp_GetAccTrnEvent]", objProfile.GetOptionsDllDBName() );
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
                    this.m_objBudgetDocEvent = new CBudgetDocEvent( rs.GetGuid( 2 ), rs.GetString( 5 ) );
                    this.m_objBudgetDocType = new CBudgetDocType( rs.GetGuid( 32 ), rs.GetString( 6 ) );
                    this.m_objDynamicRight = new CDynamicRight( rs.GetInt32( 4 ), rs.GetString( 7 ), rs.GetString( 8 ), " " );
                    rs.Close();

                    // ��������� ������ ����� ��������� ��������
                    bRet = InitDeclaration( cmd, objProfile );
                }
                else
                {
                    DevExpress.XtraEditors.XtraMessageBox.Show( 
                    "�� ������� ������������������� ����� CAccountTrnEvent.\n� �� �� ������� ����������.", "��������",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning );

                }
                cmd.Dispose();
                rs.Dispose();
            }
            catch( System.Exception e )
            {
                DevExpress.XtraEditors.XtraMessageBox.Show( 
                "�� ������� ������������������� ����� CAccountTrnEvent.\n\n����� ������: " + e.Message, "��������",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
            }
            finally // ������� ���������� �������
            {
                DBConnection.Close();
            }
            return bRet;
        }
        /// <summary>
        /// ���������� ������ ������ CAccountTrnEvent ��� ��������� ���� ���������, ������������� ����� � ��������
        /// </summary>
        /// <param name="objProfile">�������</param>
        /// <param name="objBudgetDocEvent">��������</param>
        /// <param name="objBudgetDocType">��� ���������� ���������</param>
        /// <param name="objDynamicRight">������������ �����</param>
        /// <returns></returns>
        public static CAccountTrnEvent GetAccountTrnEvent( UniXP.Common.CProfile objProfile,
           CBudgetDocEvent objBudgetDocEvent, CBudgetDocType objBudgetDocType, CDynamicRight objDynamicRight )
        {
            CAccountTrnEvent objRet = null;

            System.Data.SqlClient.SqlConnection DBConnection = objProfile.GetDBSource();
            if( DBConnection == null ) { return objRet; }

            try
            {
                // ���������� � �� ��������, ����������� ������� �� ������� ������
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
                cmd.Connection = DBConnection;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = System.String.Format( "[{0}].[dbo].[sp_GetAccTrnEvent]", objProfile.GetOptionsDllDBName() );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ( ( System.Byte )( 0 ) ), ( ( System.Byte )( 0 ) ), "", System.Data.DataRowVersion.Current, null ) );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@BUDGETDOCEVENT_GUID_ID", System.Data.SqlDbType.UniqueIdentifier ) );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@BUDGETDOCTYPE_GUID_ID", System.Data.SqlDbType.UniqueIdentifier ) );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@RIGHT_ID", System.Data.SqlDbType.Int ) );
                cmd.Parameters[ "@BUDGETDOCEVENT_GUID_ID" ].Value = objBudgetDocEvent.uuidID;
                cmd.Parameters[ "@BUDGETDOCTYPE_GUID_ID" ].Value = objBudgetDocType.uuidID;
                cmd.Parameters[ "@RIGHT_ID" ].Value = objDynamicRight.ID;
                System.Data.SqlClient.SqlDataReader rs = cmd.ExecuteReader();
                if( rs.HasRows )
                {
                    // ����� ������ ��������, � ��� ��� ���������� ���� ������
                    rs.Read();
                    objRet = new CAccountTrnEvent( rs.GetGuid( 0 ), rs.GetString( 1 ),
                        new CBudgetDocEvent( objBudgetDocEvent.uuidID, objBudgetDocEvent.Name ),
                        new CBudgetDocType( objBudgetDocType.uuidID, objBudgetDocType.Name ),
                        new CDynamicRight( objDynamicRight.ID, objDynamicRight.Name, objDynamicRight.Role, objDynamicRight.Description ) );
                    rs.Close();

                    // ��������� ������ ����� ��������� ��������
                    if( objRet.InitDeclaration( cmd, objProfile ) == false ) { objRet = null; }
                }
                else
                {
                    //DevExpress.XtraEditors.XtraMessageBox.Show( 
                    //"�� ������� ������������������� ����� CAccountTrnEvent.\n� �� �� ������� ����������.", "��������",
                    //System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning );

                }
                cmd.Dispose();
                rs.Dispose();
            }
            catch( System.Exception e )
            {
                DevExpress.XtraEditors.XtraMessageBox.Show( 
                "�� ������� ������������������� ����� CAccountTrnEvent.\n\n����� ������: " + e.Message, "��������",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
            }
            finally // ������� ���������� �������
            {
                DBConnection.Close();
            }
            return objRet;
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
                DevExpress.XtraEditors.XtraMessageBox.Show(  "������������ �������� ����������� �������������� �������.\n" + this.uuidID.ToString(), "��������",
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
                cmd.CommandText = System.String.Format( "[{0}].[dbo].[sp_DeleteAccTrnEvent]", objProfile.GetOptionsDllDBName() );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ( ( System.Byte )( 0 ) ), ( ( System.Byte )( 0 ) ), "", System.Data.DataRowVersion.Current, null ) );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@GUID_ID", System.Data.SqlDbType.UniqueIdentifier ) );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@ERROR_NUMBER", System.Data.SqlDbType.Int, 8, System.Data.ParameterDirection.Output, false, ( ( System.Byte )( 0 ) ), ( ( System.Byte )( 0 ) ), "", System.Data.DataRowVersion.Current, null ) );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@ERROR_MESSAGE", System.Data.DbType.String ) );
                cmd.Parameters[ "@ERROR_MESSAGE" ].Direction = System.Data.ParameterDirection.Output;
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
                        case 2:
                        {
                            DevExpress.XtraEditors.XtraMessageBox.Show(  "������ ���������� ������� �� �������� " + this.m_strName + 
                                "\n������ : " + ( System.String )cmd.Parameters[ "@ERROR_MESSAGE" ].Value, "��������",
                                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
                            break;
                        }
                        default:
                        {
                            DevExpress.XtraEditors.XtraMessageBox.Show(  "������ ���������� ������� �� �������� " + this.m_strName + 
                                "\n������ : " + ( System.String )cmd.Parameters[ "@ERROR_MESSAGE" ].Value, "��������",
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
                "������ ���������� ������� �� �������� " + this.m_strName + "\n\n����� ������: " + e.Message, "��������",
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
        /// <param name="cmd">SQL-�������</param>
        /// <returns>true - ������� ����������; false - ������</returns>
        private System.Boolean Remove( UniXP.Common.CProfile objProfile, System.Data.SqlClient.SqlCommand cmd )
        {
            System.Boolean bRet = false;
            // ���������� ������������� �� ������ ���� ������
            if( this.m_uuidID == System.Guid.Empty )
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(  "������������ �������� ����������� �������������� �������.\n" + this.uuidID.ToString(), "��������",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
                return bRet;
            }

            if( cmd == null ) { return bRet; }
            try
            {
                // ���������� � �� ��������, ����������� ������� �� �������� ������
                cmd.CommandText = System.String.Format( "[{0}].[dbo].[sp_DeleteAccTrnEvent]", objProfile.GetOptionsDllDBName() );
                cmd.Parameters.Clear();
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ( ( System.Byte )( 0 ) ), ( ( System.Byte )( 0 ) ), "", System.Data.DataRowVersion.Current, null ) );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@GUID_ID", System.Data.SqlDbType.UniqueIdentifier ) );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@ERROR_NUMBER", System.Data.SqlDbType.Int, 8, System.Data.ParameterDirection.Output, false, ( ( System.Byte )( 0 ) ), ( ( System.Byte )( 0 ) ), "", System.Data.DataRowVersion.Current, null ) );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@ERROR_MESSAGE", System.Data.DbType.String ) );
                cmd.Parameters[ "@ERROR_MESSAGE" ].Direction = System.Data.ParameterDirection.Output;
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
                        case 2:
                        {
                            DevExpress.XtraEditors.XtraMessageBox.Show(  "������ ���������� ������� �� �������� " + this.m_strName + 
                                "\n������ : " + ( System.String )cmd.Parameters[ "@ERROR_MESSAGE" ].Value, "��������",
                                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
                            break;
                        }
                        default:
                        {
                            DevExpress.XtraEditors.XtraMessageBox.Show(  "������ ���������� ������� �� �������� " + this.m_strName + 
                                "\n������ : " + ( System.String )cmd.Parameters[ "@ERROR_MESSAGE" ].Value, "��������",
                                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
                            break;
                        }
                    }
                }
            }
            catch( System.Exception e )
            {
                DevExpress.XtraEditors.XtraMessageBox.Show( 
                "������ ���������� ������� �� �������� " + this.m_strName + "\n\n����� ������: " + e.Message, "��������",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
            }
			finally // ������� ���������� �������
            {
            }
            return bRet;
        }
        /// <summary>
        /// ������� �� �� ��� ������� "�������� - ��� ��������� - ������������ �����"
        /// </summary>
        /// <param name="objProfile">�������</param>
        /// <returns>true - ������� ����������; false - ������</returns>
        public static System.Boolean RemoveAll( UniXP.Common.CProfile objProfile, 
            System.Data.SqlClient.SqlCommand cmd )
        {
            System.Boolean bRet = false;

            if( cmd == null ) { return bRet; }
            try
            {
                cmd.Parameters.Clear();
                cmd.CommandText = System.String.Format( "[{0}].[dbo].[sp_DeleteAccTrnEvent]", objProfile.GetOptionsDllDBName() );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ( ( System.Byte )( 0 ) ), ( ( System.Byte )( 0 ) ), "", System.Data.DataRowVersion.Current, null ) );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@ERROR_NUMBER", System.Data.SqlDbType.Int, 8, System.Data.ParameterDirection.Output, false, ( ( System.Byte )( 0 ) ), ( ( System.Byte )( 0 ) ), "", System.Data.DataRowVersion.Current, null ) );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@ERROR_MESSAGE", System.Data.DbType.String ) );
                cmd.Parameters[ "@ERROR_MESSAGE" ].Direction = System.Data.ParameterDirection.Output;
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
                        case 2:
                        {
                            DevExpress.XtraEditors.XtraMessageBox.Show(  "������ ���������� ������� �� �������� " + 
                                "\n������ : " + ( System.String )cmd.Parameters[ "@ERROR_MESSAGE" ].Value, "��������",
                                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
                            break;
                        }
                        default:
                        {
                            DevExpress.XtraEditors.XtraMessageBox.Show(  "������ ���������� ������� �� �������� " + 
                                "\n������ : " + ( System.String )cmd.Parameters[ "@ERROR_MESSAGE" ].Value, "��������",
                                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
                            break;
                        }
                    }
                }
            }
            catch( System.Exception e )
            {
                DevExpress.XtraEditors.XtraMessageBox.Show( 
                "������ ���������� ������� �� �������� ���� ��������.\n\n����� ������: " + e.Message, "��������",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
            }
			finally // ������� ���������� �������
            {
            }
            return bRet;
        }
        /// <summary>
        /// ������� ������ ����� ��������� �������� �� ��
        /// </summary>
        /// <param name="objProfile">�������</param>
        /// <param name="cmd">SQL-�������</param>
        /// <returns>true - ������� ����������; false - ������</returns>
        private System.Boolean RemoveDeclaration( UniXP.Common.CProfile objProfile, System.Data.SqlClient.SqlCommand cmd )
        {
            System.Boolean bRet = false;
            // ���������� ������������� �� ������ ���� ������
            if( this.m_uuidID == System.Guid.Empty )
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(  "������������ �������� ����������� �������������� �������.\n" + this.uuidID.ToString(), "��������",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
                return bRet;
            }

            if( cmd == null ) { return bRet; }
            try
            {
                // ���������� � �� ��������, ����������� ������� �� �������� ������
                cmd.CommandText = System.String.Format( "[{0}].[dbo].[sp_DeleteAccTrnEventDeclrn]", objProfile.GetOptionsDllDBName() );
                cmd.Parameters.Clear();
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ( ( System.Byte )( 0 ) ), ( ( System.Byte )( 0 ) ), "", System.Data.DataRowVersion.Current, null ) );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@GUID_ID", System.Data.SqlDbType.UniqueIdentifier ) );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@ERROR_NUMBER", System.Data.SqlDbType.Int, 8, System.Data.ParameterDirection.Output, false, ( ( System.Byte )( 0 ) ), ( ( System.Byte )( 0 ) ), "", System.Data.DataRowVersion.Current, null ) );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@ERROR_MESSAGE", System.Data.DbType.String ) );
                cmd.Parameters[ "@ERROR_MESSAGE" ].Direction = System.Data.ParameterDirection.Output;
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
                        case 2:
                        {
                            DevExpress.XtraEditors.XtraMessageBox.Show(  "������ ���������� ������� �� �������� " + this.m_strName + 
                                "\n������ : " + ( System.String )cmd.Parameters[ "@ERROR_MESSAGE" ].Value, "��������",
                                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
                            break;
                        }
                        default:
                        {
                            DevExpress.XtraEditors.XtraMessageBox.Show(  "������ ���������� ������� �� �������� " + this.m_strName + 
                                "\n������ : " + ( System.String )cmd.Parameters[ "@ERROR_MESSAGE" ].Value, "��������",
                                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
                            break;
                        }
                    }
                }
            }
            catch( System.Exception e )
            {
                DevExpress.XtraEditors.XtraMessageBox.Show( 
                "������ ���������� ������� �� �������� " + this.m_strName + "\n\n����� ������: " + e.Message, "��������",
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
                    DevExpress.XtraEditors.XtraMessageBox.Show(  "��� ������� ����� ����������!", "��������",
                        System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning );
                    return bRet;
                }
                // ��������
                if( this.m_objBudgetDocEvent == null )
                {
                    DevExpress.XtraEditors.XtraMessageBox.Show(  "���������� ������� ��������!\n������: " + this.m_strName, "��������",
                        System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning );
                    return bRet;
                }
                // ��� ���������
                if( this.m_objBudgetDocType == null )
                {
                    DevExpress.XtraEditors.XtraMessageBox.Show(  "���������� ������� ��� ���������!\n������: " + this.m_strName, "��������",
                        System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning );
                    return bRet;
                }
                // ������������ �����
                if( this.m_objBudgetDocType == null )
                {
                    DevExpress.XtraEditors.XtraMessageBox.Show(  "���������� ������� ������������ �����!\n������: " + this.m_strName, "��������",
                        System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning );
                    return bRet;
                }
                // �������� �� ������ �� ���������
                if( this.m_AccountTrnTypeList.Count == 0 )
                {
                    DevExpress.XtraEditors.XtraMessageBox.Show(  "� ������� ����������� ������ ����� ��������� ��������!", "��������",
                        System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning );
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
            // ��������� ��������
            if( this.IsValidateProperties() == false ) { return bRet; }

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
                cmd.CommandText = System.String.Format( "[{0}].[dbo].[sp_AddAccTrnEvent]", objProfile.GetOptionsDllDBName() );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ( ( System.Byte )( 0 ) ), ( ( System.Byte )( 0 ) ), "", System.Data.DataRowVersion.Current, null ) );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@GUID_ID", System.Data.SqlDbType.UniqueIdentifier, 4, System.Data.ParameterDirection.Output, false, ( ( System.Byte )( 0 ) ), ( ( System.Byte )( 0 ) ), "", System.Data.DataRowVersion.Current, null ) );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@ACCTRNEVENT_NAME", System.Data.DbType.String ) );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@BUDGETDOCEVENT_GUID_ID", System.Data.SqlDbType.UniqueIdentifier ) );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@BUDGETDOCTYPE_GUID_ID", System.Data.SqlDbType.UniqueIdentifier ) );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@RIGHT_ID", System.Data.SqlDbType.Int ) );
                if( this.m_uuidID.CompareTo( System.Guid.Empty ) != 0 )
                {
                    cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@ACCTRNEVENT_GUID_ID", System.Data.SqlDbType.UniqueIdentifier ) );
                    cmd.Parameters[ "@ACCTRNEVENT_GUID_ID" ].Value = this.m_uuidID;
                }
                cmd.Parameters[ "@ACCTRNEVENT_NAME" ].Value = this.m_strName;
                cmd.Parameters[ "@BUDGETDOCEVENT_GUID_ID" ].Value = this.m_objBudgetDocEvent.uuidID;
                cmd.Parameters[ "@BUDGETDOCTYPE_GUID_ID" ].Value = this.m_objBudgetDocType.uuidID;
                cmd.Parameters[ "@RIGHT_ID" ].Value = this.m_objDynamicRight.ID;
                cmd.ExecuteNonQuery();
                System.Int32 iRet = ( System.Int32 )cmd.Parameters[ "@RETURN_VALUE" ].Value;
                if( iRet == 0 )
                {
                    this.m_uuidID = ( System.Guid )cmd.Parameters[ "@GUID_ID" ].Value;
                    // ��������� ������ ����� ��������� ��������
                    bRet = this.SaveDeclaration( objProfile, cmd );
                    if( bRet )
                    {
                        // ������������ ����������
                        DBTransaction.Commit();
                    }
                    else
                    {
                        // ���������� ����������
                        DBTransaction.Rollback();
                    }
                }
                else
                {
                    // ���������� ����������
                    DBTransaction.Rollback();
                    switch( iRet )
                    {
                        case 1:
                        {
                            DevExpress.XtraEditors.XtraMessageBox.Show(  "������ � �������� ������ ���������� : '" + this.m_strName + "'", "��������",
                                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning );
                            break;
                        }
                        case 2:
                        {
                            DevExpress.XtraEditors.XtraMessageBox.Show(  "�������� � ��������� ��������������� �� ������� : '" + this.m_objBudgetDocEvent.uuidID.ToString() + "'", "��������",
                                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning );
                            break;
                        }
                        case 3:
                        {
                            DevExpress.XtraEditors.XtraMessageBox.Show(  "��� ��������� � ��������� ��������������� �� ������ : '" + this.m_objBudgetDocType.uuidID.ToString() + "'", "��������",
                                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning );
                            break;
                        }
                        case 4:
                        {
                            DevExpress.XtraEditors.XtraMessageBox.Show(  "������������ ����� � ��������� ��������������� �� �������: '" + this.m_objDynamicRight.ID.ToString() + "'", "��������",
                                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning );
                            break;
                        }
                        default:
                        {
                            DevExpress.XtraEditors.XtraMessageBox.Show(  "������ �������� ������� : " + this.m_strName, "������",
                                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
                            break;
                        }
                    }
                }

                cmd.Dispose();
            }
            catch( System.Exception f )
            {
                // ���������� ����������
                DBTransaction.Rollback();
                DevExpress.XtraEditors.XtraMessageBox.Show( 
                "�� ������� ������� ������ : " + this.m_strName + "\n\n����� ������: " + f.Message, "��������",
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
        /// <param name="cmd">SQL-�������</param>
        /// <returns>true - ������� ����������; false - ������</returns>
        private System.Boolean Add( UniXP.Common.CProfile objProfile, System.Data.SqlClient.SqlCommand cmd )
        {
            System.Boolean bRet = false;
            // ��������� ��������
            if( this.IsValidateProperties() == false ) { return bRet; }

            if( cmd == null ) { return bRet; }
            try
            {
                // ���������� � �� ��������, ����������� ������� �� �������� ������ � ��
                cmd.Parameters.Clear();
                cmd.CommandText = System.String.Format( "[{0}].[dbo].[sp_AddAccTrnEvent]", objProfile.GetOptionsDllDBName() );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ( ( System.Byte )( 0 ) ), ( ( System.Byte )( 0 ) ), "", System.Data.DataRowVersion.Current, null ) );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@GUID_ID", System.Data.SqlDbType.UniqueIdentifier, 4, System.Data.ParameterDirection.Output, false, ( ( System.Byte )( 0 ) ), ( ( System.Byte )( 0 ) ), "", System.Data.DataRowVersion.Current, null ) );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@ACCTRNEVENT_NAME", System.Data.DbType.String ) );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@BUDGETDOCEVENT_GUID_ID", System.Data.SqlDbType.UniqueIdentifier ) );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@BUDGETDOCTYPE_GUID_ID", System.Data.SqlDbType.UniqueIdentifier ) );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@RIGHT_ID", System.Data.SqlDbType.Int ) );
                if( this.m_uuidID.CompareTo( System.Guid.Empty ) != 0 )
                {
                    cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@ACCTRNEVENT_GUID_ID", System.Data.SqlDbType.UniqueIdentifier ) );
                    cmd.Parameters[ "@ACCTRNEVENT_GUID_ID" ].Value = this.m_uuidID;
                }
                cmd.Parameters[ "@ACCTRNEVENT_NAME" ].Value = this.m_strName;
                cmd.Parameters[ "@BUDGETDOCEVENT_GUID_ID" ].Value = this.m_objBudgetDocEvent.uuidID;
                cmd.Parameters[ "@BUDGETDOCTYPE_GUID_ID" ].Value = this.m_objBudgetDocType.uuidID;
                cmd.Parameters[ "@RIGHT_ID" ].Value = this.m_objDynamicRight.ID;
                cmd.ExecuteNonQuery();
                System.Int32 iRet = ( System.Int32 )cmd.Parameters[ "@RETURN_VALUE" ].Value;
                if( iRet == 0 )
                {
                    this.m_uuidID = ( System.Guid )cmd.Parameters[ "@GUID_ID" ].Value;
                    // ��������� ������ ����� ��������� ��������
                    bRet = this.SaveDeclaration( objProfile, cmd );
                }
                else
                {
                    switch( iRet )
                    {
                        case 1:
                        {
                            DevExpress.XtraEditors.XtraMessageBox.Show(  "������ � �������� ������ ���������� : '" + this.m_strName + "'", "��������",
                                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning );
                            break;
                        }
                        case 2:
                        {
                            DevExpress.XtraEditors.XtraMessageBox.Show(  "�������� � ��������� ��������������� �� ������� : '" + this.m_objBudgetDocEvent.uuidID.ToString() + "'", "��������",
                                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning );
                            break;
                        }
                        case 3:
                        {
                            DevExpress.XtraEditors.XtraMessageBox.Show(  "��� ��������� � ��������� ��������������� �� ������ : '" + this.m_objBudgetDocType.uuidID.ToString() + "'", "��������",
                                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning );
                            break;
                        }
                        case 4:
                        {
                            DevExpress.XtraEditors.XtraMessageBox.Show(  "������������ ����� � ��������� ��������������� �� �������: '" + this.m_objDynamicRight.ID.ToString() + "'", "��������",
                                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning );
                            break;
                        }
                        default:
                        {
                            DevExpress.XtraEditors.XtraMessageBox.Show(  "������ �������� ������� : " + this.m_strName, "������",
                                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
                            break;
                        }
                    }
                }
            }
            catch( System.Exception f )
            {
                DevExpress.XtraEditors.XtraMessageBox.Show( 
                "�� ������� ������� ������ : " + this.m_strName + "\n\n����� ������: " + f.Message, "��������",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
            }
			finally // ������� ���������� �������
            {
            }
            return bRet;
        }
        /// <summary>
        /// ��������� ������ ����� ��������� �������� � ��
        /// </summary>
        /// <param name="objProfile">�������</param>
        /// <param name="cmd">SQL-�������</param>
        /// <returns>true - ������� ����������; false - ������</returns>
        private System.Boolean SaveDeclaration( UniXP.Common.CProfile objProfile, System.Data.SqlClient.SqlCommand cmd )
        {
            System.Boolean bRet = false;

            // ������ ����� ��������� �������� �� ������ ���� ����
            if( ( this.m_AccountTrnTypeList == null ) || ( this.m_AccountTrnTypeList.Count == 0 ) ) 
            {
                DevExpress.XtraEditors.XtraMessageBox.Show( 
                "������ ����� ��������� �������� �� ������ ���� ����.", "��������",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning );
                return bRet; 
            }

            if( cmd == null ) { return bRet; }
            try
            {
                // ������ ������� ������� ������
                bRet = this.RemoveDeclaration( objProfile, cmd );
                if( bRet == true )
                {
                    cmd.Parameters.Clear();
                    cmd.CommandText = System.String.Format( "[{0}].[dbo].[sp_AddAccTrnEventDeclrn]", objProfile.GetOptionsDllDBName() );
                    cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ( ( System.Byte )( 0 ) ), ( ( System.Byte )( 0 ) ), "", System.Data.DataRowVersion.Current, null ) );
                    cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@ACCTRNEVENT_GUID_ID", System.Data.SqlDbType.UniqueIdentifier ) );
                    cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@ACCTRNTYPE_GUID_ID", System.Data.SqlDbType.UniqueIdentifier ) );
                    cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@ORDERNUM", System.Data.SqlDbType.Int ) );
                    cmd.Parameters[ "@ACCTRNEVENT_GUID_ID" ].Value = this.m_uuidID;
                    System.Int32 iRet = 0;
                    for( System.Int32 i = 0; i < this.m_AccountTrnTypeList.Count; i++ )
                    {
                        cmd.Parameters[ "@ORDERNUM" ].Value = i;
                        cmd.Parameters[ "@ACCTRNTYPE_GUID_ID" ].Value = this.m_AccountTrnTypeList[ i ].uuidID;
                        cmd.ExecuteNonQuery();

                        iRet = ( System.Int32 )cmd.Parameters[ "@RETURN_VALUE" ].Value;
                        if( iRet != 0 )
                        {
                            switch( iRet )
                            {
                                case 1:
                                {
                                    DevExpress.XtraEditors.XtraMessageBox.Show(  "��� ���������� � ��������� ��������������� �� ������ : \n'" + this.m_AccountTrnTypeList[ i ].uuidID.ToString() + "'", "��������",
                                        System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning );
                                    break;
                                }
                                case 2:
                                {
                                    DevExpress.XtraEditors.XtraMessageBox.Show(  "������ '��� ���-�� - ������������ ����� - ��������' \n� ��������� ��������������� �� ������ : \n" + this.uuidID.ToString() + "'", "��������",
                                        System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning );
                                    break;
                                }
                                default:
                                {
                                    DevExpress.XtraEditors.XtraMessageBox.Show(  "������ ���������� ���� ��������� �������� � ������ : " + this.m_AccountTrnTypeList[ i ].Name, "������",
                                        System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
                                    break;
                                }
                            }

                            break;
                        }
                    }
                    bRet = ( iRet == 0 );
                }
            }
            catch( System.Exception f )
            {
                DevExpress.XtraEditors.XtraMessageBox.Show( 
                "�� ������� ��������� ������ ����� ��������� ��������.\n\n����� ������: " + f.Message, "��������",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
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
            if( this.IsValidateProperties() == false )
            {
                return bRet;
            }

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
                cmd.CommandText = System.String.Format( "[{0}].[dbo].[sp_EditAccTrnEvent]", objProfile.GetOptionsDllDBName() );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ( ( System.Byte )( 0 ) ), ( ( System.Byte )( 0 ) ), "", System.Data.DataRowVersion.Current, null ) );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@GUID_ID", System.Data.SqlDbType.UniqueIdentifier ) );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@ACCTRNEVENT_NAME", System.Data.DbType.String ) );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@BUDGETDOCEVENT_GUID_ID", System.Data.SqlDbType.UniqueIdentifier ) );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@BUDGETDOCTYPE_GUID_ID", System.Data.SqlDbType.UniqueIdentifier ) );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@RIGHT_ID", System.Data.SqlDbType.Int ) );
                cmd.Parameters[ "@GUID_ID" ].Value = this.m_uuidID;
                cmd.Parameters[ "@ACCTRNEVENT_NAME" ].Value = this.m_strName;
                cmd.Parameters[ "@BUDGETDOCEVENT_GUID_ID" ].Value = this.m_objBudgetDocEvent.uuidID;
                cmd.Parameters[ "@BUDGETDOCTYPE_GUID_ID" ].Value = this.m_objBudgetDocType.uuidID;
                cmd.Parameters[ "@RIGHT_ID" ].Value = this.m_objDynamicRight.ID;
                cmd.ExecuteNonQuery();
                System.Int32 iRet = ( System.Int32 )cmd.Parameters[ "@RETURN_VALUE" ].Value;
                if( iRet == 0 )
                {
                    // ��������� ��������� ������� ��������
                    bRet = this.SaveDeclaration( objProfile, cmd );
                    if( bRet )
                    {
                        // ������������ ����������
                        DBTransaction.Commit();
                    }
                    else
                    {
                        // ���������� ����������
                        DBTransaction.Rollback();
                    }
                }
                else
                {
                    // ���������� ����������
                    DBTransaction.Rollback();
                    switch( iRet )
                    {
                        case 1:
                        {
                            DevExpress.XtraEditors.XtraMessageBox.Show(  "������ � �������� ������ ���������� : '" + this.m_strName + "'", "��������",
                                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning );
                            break;
                        }
                        case 2:
                        {
                            DevExpress.XtraEditors.XtraMessageBox.Show(  "������ � ��������� ��������������� �� ������ :\n'" + this.uuidID.ToString() + "'", "��������",
                                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning );
                            break;
                        }
                        case 3:
                        {
                            DevExpress.XtraEditors.XtraMessageBox.Show(  "�������� � ��������� ��������������� �� ������� :\n'" + this.m_objBudgetDocEvent.uuidID.ToString() + "'", "��������",
                                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning );
                            break;
                        }
                        case 4:
                        {
                            DevExpress.XtraEditors.XtraMessageBox.Show(  "��� ��������� � ��������� ��������������� �� ������ :\n'" + this.m_objBudgetDocType.uuidID.ToString() + "'", "��������",
                                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning );
                            break;
                        }
                        case 5:
                        {
                            DevExpress.XtraEditors.XtraMessageBox.Show(  "������������ ����� � ��������� ��������������� �� �������:\n'" + this.m_objDynamicRight.ID.ToString() + "'", "��������",
                                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning );
                            break;
                        }
                        default:
                        {
                            DevExpress.XtraEditors.XtraMessageBox.Show(  "������ �������� ������� : " + this.m_strName, "������",
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
                "������ ��������� ������� ������� : " + this.m_strName + "\n" + e.Message, "��������",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
            }
			finally // ������� ���������� �������
            {
                DBConnection.Close();
            }
            return bRet;
        }
        #endregion

        #region ��������� ������ �������� "�������� - ��� ��������� - ������������ �����" � ��  
        /// <summary>
        /// ��������� ������ �������� "�������� - ��� ��������� - ������������ �����" � ��
        /// </summary>
        /// <param name="objProfile">�������</param>
        /// <param name="objList">������ ��������</param>
        /// <returns>true - ������� ����������; false - ������</returns>    
        public static System.Boolean SaveAccountTrnEventList( UniXP.Common.CProfile objProfile,
            List<CAccountTrnEvent> objList )
        {
            System.Boolean bRet = false;
            if( ( objList == null ) || ( objList.Count == 0 ) )
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(  "������ ����������� �������� �� ������ ���� ������!", "��������",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
                return bRet;
            }

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

                // ������ ������� ��� ��� �������
                bRet = CAccountTrnEvent.RemoveAll( objProfile, cmd );
                if( bRet == false )
                {
                    // ���������� ����������
                    DBTransaction.Rollback();
                }
                else
                {
                    foreach( CAccountTrnEvent objAccountTrnEvent in objList )
                    {
                        // ��������� ������ � ��
                        bRet = objAccountTrnEvent.Add( objProfile, cmd );
                        if( bRet == false )
                        {
                            break;
                        }
                    }
                    if( bRet == false )
                    {
                        // ���������� ����������
                        DBTransaction.Rollback();
                    }
                    else
                    {
                        // ������������ ����������
                        DBTransaction.Commit();
                    }
                }

                cmd.Dispose();
            }
            catch( System.Exception f )
            {
                // ���������� ����������
                DBTransaction.Rollback();
                DevExpress.XtraEditors.XtraMessageBox.Show( 
                "������ ���������� ������ �������� � ��.\n\n����� ������: " + f.Message, "��������",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
            }
			finally // ������� ���������� �������
            {
                DBConnection.Close();
            }

            return bRet;
        }

        #endregion 
    }
}
