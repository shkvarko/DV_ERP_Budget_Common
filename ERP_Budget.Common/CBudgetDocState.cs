using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;


namespace ERP_Budget.Common
{
    /// <summary>
    /// ����� "��������� ���������� ���������"
    /// </summary>
    public class CBudgetDocState : IBaseListItem
    {
        #region ���������� 
        /// <summary>
        /// ����� �� �������
        /// </summary>
        private System.Int32 m_iOrderNum;
        /// <summary>
        /// ����� �� �������
        /// </summary>
        [DisplayName( "����� �� �������" )]
        [Description( "����� � ���������� ������������������ ��������� ���������" )]
        [Category( "������������ ��������" )]
        public System.Int32 OrderNum
        {
            get { return m_iOrderNum; }
            set { m_iOrderNum = value; }
        }
        private CBudgetDocStateDraw m_objBudgetDocStateDraw;
        [DisplayName( "��������" )]
        [Description( "��������, ��������������� ��������� ���������� ���������" )]
        [Category( "�������������� ��������" )]
        [ReadOnly( true )]
        public CBudgetDocStateDraw BudgetDocStateDraw
        {
            get { return m_objBudgetDocStateDraw; }
            set { m_objBudgetDocStateDraw = value; }
        }

        #endregion

        #region ������������ 
        public CBudgetDocState()
        {
            this.m_uuidID = System.Guid.Empty;
            this.m_strName = "";
            this.m_iOrderNum = 0;
            this.m_objBudgetDocStateDraw = null;
        }
        public CBudgetDocState(System.Guid uuidID)
        {
            this.m_uuidID = uuidID;
            this.m_strName = "";
            this.m_iOrderNum = 0;
            this.m_objBudgetDocStateDraw = null;
        }
        public CBudgetDocState(System.Guid uuidID, System.String strName)
        {
            this.m_uuidID = uuidID;
            this.m_strName = strName;
            this.m_iOrderNum = 0;
            this.m_objBudgetDocStateDraw = null;
        }
        public CBudgetDocState( System.Guid uuidID, System.String strName, System.Int32 iOrderNum )
        {
            this.m_uuidID = uuidID;
            this.m_strName = strName;
            this.m_iOrderNum = iOrderNum;
            this.m_objBudgetDocStateDraw = null;
        }
        #endregion

        #region ������ ��������� ���������� ��������� 
        /// <summary>
        /// ���������� ������ ��������� ���������� ���������
        /// </summary>
        /// <param name="objProfile">�������</param>
        /// <returns>������ ������� ���������� ��������� � ���� �������</returns>
        public static System.Data.DataTable GetBudgetDocStateTable( UniXP.Common.CProfile objProfile )
        {
            System.Data.DataTable dtBudgetDocState = null;
            try
            {
                // ���������� �������
                dtBudgetDocState = new System.Data.DataTable( "dtBudgetDocState" );
                dtBudgetDocState.Columns.Add( new System.Data.DataColumn( "GUID_ID", System.Type.GetType( "System.Guid" ) ) );
                dtBudgetDocState.Columns.Add( new System.Data.DataColumn( "BUDGETDOCSTATE_NAME", System.Type.GetType( "System.String" ) ) );

                System.Data.SqlClient.SqlConnection DBConnection = objProfile.GetDBSource();
                if( DBConnection == null ) { return dtBudgetDocState; }

                // ���������� � �� ��������, ����������� ������� �� ������� ������
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
                cmd.Connection = DBConnection;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = System.String.Format( "[{0}].[dbo].[sp_GetBudgetDocState]", objProfile.GetOptionsDllDBName() );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ( ( System.Byte )( 0 ) ), ( ( System.Byte )( 0 ) ), "", System.Data.DataRowVersion.Current, null ) );
                System.Data.SqlClient.SqlDataReader rs = cmd.ExecuteReader();
                if( rs.HasRows )
                {
                    System.Data.DataRow row = null;
                    while( rs.Read() )
                    {
                        row = dtBudgetDocState.NewRow();
                        row[ "GUID_ID" ] = rs.GetGuid( 0 );
                        row[ "BUDGETDOCSTATE_NAME" ] = rs.GetString( 1 );
                        dtBudgetDocState.Rows.Add( row );
                    }
                    dtBudgetDocState.AcceptChanges();
                }
                cmd.Dispose();
                rs.Dispose();
                DBConnection.Close();
            }
            catch( System.Exception e )
            {
                DevExpress.XtraEditors.XtraMessageBox.Show( 
                "�� ������� �������� ������ ��������� ���������� ���������.\n" + e.Message, "��������",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
            }
            return dtBudgetDocState;
        }
        /// <summary>
        /// ���������� ������ ��������� ���������� ���������
        /// </summary>
        /// <param name="objProfile">�������</param>
        /// <returns>������ ������� ��������� ���������� ���������</returns>
        public CBaseList<CBudgetDocState> GetBudgetDocStateList( UniXP.Common.CProfile objProfile )
        {
            CBaseList<CBudgetDocState> objList = new CBaseList<CBudgetDocState>();

            System.Data.SqlClient.SqlConnection DBConnection = objProfile.GetDBSource();
            if( DBConnection == null ) { return objList; }

            try
            {
                // ���������� � �� ��������, ����������� ������� �� ������� ������
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
                cmd.Connection = DBConnection;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = System.String.Format( "[{0}].[dbo].[sp_GetBudgetDocState]", objProfile.GetOptionsDllDBName() );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ( ( System.Byte )( 0 ) ), ( ( System.Byte )( 0 ) ), "", System.Data.DataRowVersion.Current, null ) );
                System.Data.SqlClient.SqlDataReader rs = cmd.ExecuteReader();
                if( rs.HasRows )
                {
                    // ����� ������ ��������
                    CBudgetDocState objBudgetDocState = null;
                    while( rs.Read() )
                    {
                        objBudgetDocState = new CBudgetDocState();
                        objBudgetDocState.m_uuidID = rs.GetGuid( 0 );
                        objBudgetDocState.m_strName = rs.GetString( 1 );
                        objBudgetDocState.m_iOrderNum = rs.GetInt32( 2 );
                        objList.AddItemToList( objBudgetDocState );
                    }
                }
                else
                {
                    DevExpress.XtraEditors.XtraMessageBox.Show( 
                    "�� ������� �������� ������ ��������� ���������� ���������.\n� �� �� ������� ����������.", "��������",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning );

                }
                cmd.Dispose();
                rs.Dispose();
            }
            catch( System.Exception e )
            {
                DevExpress.XtraEditors.XtraMessageBox.Show( 
                "�� ������� �������� ������ ��������� ���������� ���������.\n" + e.Message, "��������",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
            }
			finally // ������� ���������� �������
            {
                DBConnection.Close();
            }
            return objList;
        }

        /// <summary>
        /// ���������� ������ ��������� ���������� ���������
        /// </summary>
        /// <param name="objProfile">�������</param>
        /// <returns>������ ������� ��������� ���������� ���������</returns>
        public static System.Collections.Generic.List<CBudgetDocState> GetBudgetDocStateListObj( UniXP.Common.CProfile objProfile )
        {
            System.Collections.Generic.List<CBudgetDocState> objList = new System.Collections.Generic.List<CBudgetDocState>();

            System.Data.SqlClient.SqlConnection DBConnection = objProfile.GetDBSource();
            if( DBConnection == null ) { return objList; }

            try
            {
                // ���������� � �� ��������, ����������� ������� �� ������� ������
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
                cmd.Connection = DBConnection;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = System.String.Format( "[{0}].[dbo].[sp_GetBudgetDocState]", objProfile.GetOptionsDllDBName() );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ( ( System.Byte )( 0 ) ), ( ( System.Byte )( 0 ) ), "", System.Data.DataRowVersion.Current, null ) );
                System.Data.SqlClient.SqlDataReader rs = cmd.ExecuteReader();
                if( rs.HasRows )
                {
                    while( rs.Read() )
                    {
                        objList.Add( new CBudgetDocState( rs.GetGuid( 0 ), rs.GetString( 1 ), rs.GetInt32( 2 ) ) );
                    }
                }
                rs.Close();
                rs.Dispose();
                cmd.Dispose();
            }
            catch( System.Exception f )
            {
                DevExpress.XtraEditors.XtraMessageBox.Show( 
                "�� ������� �������� ������ ��������� ���������� ���������.\n\n����� ������: " + f.Message, "��������",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
            }
			finally // ������� ���������� �������
            {
                DBConnection.Close();
            }
            return objList;
        }

        /// <summary>
        /// ���������� ������ ��������� ���������� ���������
        /// </summary>
        /// <param name="objProfile">�������</param>
        /// <returns>������ ������� ��������� ���������� ���������</returns>
        public static System.Collections.Generic.List<CBudgetDocState> GetBudgetDocStateListPictures( 
            UniXP.Common.CProfile objProfile )
        {
            System.Collections.Generic.List<CBudgetDocState> objList = new System.Collections.Generic.List<CBudgetDocState>();

            System.Data.SqlClient.SqlConnection DBConnection = objProfile.GetDBSource();
            if( DBConnection == null ) { return objList; }

            try
            {
                // ���������� � �� ��������, ����������� ������� �� ������� ������
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
                cmd.Connection = DBConnection;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = System.String.Format( "[{0}].[dbo].[sp_GetBudgetDocState]", objProfile.GetOptionsDllDBName() );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ( ( System.Byte )( 0 ) ), ( ( System.Byte )( 0 ) ), "", System.Data.DataRowVersion.Current, null ) );
                System.Data.SqlClient.SqlDataReader rs = cmd.ExecuteReader();
                if( rs.HasRows )
                {
                    while( rs.Read() )
                    {
                        objList.Add( new CBudgetDocState( rs.GetGuid( 0 ), rs.GetString( 1 ), rs.GetInt32( 2 ) ) );
                    }
                }
                rs.Close();

                // ������ �������� ���� ��������
                foreach( CBudgetDocState objBudgetDocState in objList )
                {
                    objBudgetDocState.BudgetDocStateDraw = CBudgetDocStateDraw.GetBudgetDocStateDraw( objProfile, objBudgetDocState, cmd );
                }

                rs.Dispose();
                cmd.Dispose();
            }
            catch( System.Exception f )
            {
                DevExpress.XtraEditors.XtraMessageBox.Show( 
                "�� ������� �������� ������ ��������� ���������� ���������.\n\n����� ������: " + f.Message, "��������",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
            }
			finally // ������� ���������� �������
            {
                DBConnection.Close();
            }
            return objList;
        }

        /// <summary>
        /// ��������� ������ ��������� ��������� ����������
        /// </summary>
        /// <param name="objProfile">�������</param>
        /// <param name="cmd">SQL - �������</param>
        /// <param name="objList">����������� ������</param>
        /// <returns></returns>
        public static void RefreshBudgetDocStateList( UniXP.Common.CProfile objProfile,
            System.Data.SqlClient.SqlCommand cmd, System.Collections.Generic.List<CBudgetDocState> objList )
        {
            if( cmd == null ) { return; }
            objList.Clear();

            try
            {
                cmd.Parameters.Clear();
                cmd.CommandText = System.String.Format( "[{0}].[dbo].[sp_GetBudgetDocState]", objProfile.GetOptionsDllDBName() );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ( ( System.Byte )( 0 ) ), ( ( System.Byte )( 0 ) ), "", System.Data.DataRowVersion.Current, null ) );
                System.Data.SqlClient.SqlDataReader rs = cmd.ExecuteReader();
                if( rs.HasRows )
                {
                    while( rs.Read() )
                    {
                        objList.Add( new CBudgetDocState( rs.GetGuid( 0 ), rs.GetString( 1 ), rs.GetInt32( 2 ) ) );
                    }
                }
                rs.Close();
                rs.Dispose();
            }
            catch( System.Exception e )
            {
                DevExpress.XtraEditors.XtraMessageBox.Show( 
                "�� ������� �������� ������ ��������� ���������� ���������.\n����� ������: " + e.Message, "��������",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
            }
			finally // ������� ���������� �������
            {
            }
            return;
        }
        /// <summary>
        /// ���������� ��������� � ������� 6 (�������)
        /// </summary>
        /// <param name="objProfile">�������</param>
        /// <returns>������ ������ CBudgetDocState</returns>
        public static CBudgetDocState GetDropBudgetDocState( UniXP.Common.CProfile objProfile )
        {
            CBudgetDocState objRet = null;
            try
            {
                System.Data.SqlClient.SqlConnection DBConnection = objProfile.GetDBSource();
                if( DBConnection == null ) { return objRet; }

                // ���������� � �� ��������, ����������� ������� �� ������� ������
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
                cmd.Connection = DBConnection;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = System.String.Format( "[{0}].[dbo].[sp_GetBudgetDocState]", objProfile.GetOptionsDllDBName() );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ( ( System.Byte )( 0 ) ), ( ( System.Byte )( 0 ) ), "", System.Data.DataRowVersion.Current, null ) );
                System.Data.SqlClient.SqlDataReader rs = cmd.ExecuteReader();
                if( rs.HasRows )
                {
                    while( rs.Read() )
                    {
                        if( ( ( System.Int32 )rs[ 2 ] ) == 6 )
                        {
                            // ��������� � ��������������� 6
                            objRet = new CBudgetDocState( rs.GetGuid( 0 ), rs.GetString( 1 ), 6 );
                            break;
                        }
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
                "�� ������� �������� ��������� ���������� ���������.\n" + e.Message, "��������",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
            }
            return objRet;
        }
        /// <summary>
        /// ��������� ������ �� ������� ��������� ��������� ����������
        /// </summary>
        /// <param name="objProfile">�������</param>
        /// <param name="objTreeList">������</param>
        /// <returns>true - ������� ����������; false - ������</returns>
        public static System.Boolean bRefreshBudgetDocTypeTree( UniXP.Common.CProfile objProfile,
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
                    "�� ������� �������� ������ ��������� ��������� ����������.\n����������� ���������� � ��.", "��������",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
                return bRet;
            }

            try
            {
                // ���������� � �� ��������, ����������� ������� �� ������� ������
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
                cmd.Connection = DBConnection;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = System.String.Format( "[{0}].[dbo].[sp_GetBudgetDocState]", objProfile.GetOptionsDllDBName() );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ( ( System.Byte )( 0 ) ), ( ( System.Byte )( 0 ) ), "", System.Data.DataRowVersion.Current, null ) );
                System.Data.SqlClient.SqlDataReader rs = cmd.ExecuteReader();
                if( rs.HasRows )
                {
                    // ����� ������ ��������
                    CBudgetDocState objBudgetDocState = null;
                    while( rs.Read() )
                    {
                        // ������� ������ ������ CBudgetDocState
                        objBudgetDocState = new CBudgetDocState( ( System.Guid )rs[ "GUID_ID" ],
                            ( System.String )rs[ "BUDGETDOCSTATE_NAME" ], ( System.Int32 )rs[ "BUDGETDOCSTATE_ID" ] );
                        // ������� ���� ������
                        DevExpress.XtraTreeList.Nodes.TreeListNode objNode = 
                                objTreeList.AppendNode( new object[] { objBudgetDocState.Name }, null );
                        objNode.Tag = objBudgetDocState;

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
                "�� ������� �������� ������ ��������� ��������� ����������.\n\n����� ������: " + f.Message, "��������",
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
                cmd.CommandText = System.String.Format( "[{0}].[dbo].[sp_GetBudgetDocState]", objProfile.GetOptionsDllDBName() );
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
                    this.m_iOrderNum = rs.GetInt32( 2 );
                    bRet = true;
                }
                else
                {
                    DevExpress.XtraEditors.XtraMessageBox.Show( 
                    "�� ������� ������������������� ����� CBudgetDocState.\n� �� �� ������� ����������.", "��������",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning );

                }
                cmd.Dispose();
                rs.Dispose();
            }
            catch( System.Exception e )
            {
                DevExpress.XtraEditors.XtraMessageBox.Show( 
                "�� ������� ������������������� ����� CBudgetDocState.\n" + e.Message, "��������",
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
                cmd.CommandText = System.String.Format( "[{0}].[dbo].[sp_DeleteBudgetDocState]", objProfile.GetOptionsDllDBName() );
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
                            DevExpress.XtraEditors.XtraMessageBox.Show(  "�� ��������� ���������� ��������� ���� ������ � ��������� ���������.\n�� ���� ���������: " + this.uuidID.ToString(), "��������",
                                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning );
                            break;
                        }
                        case 2:
                        {
                            DevExpress.XtraEditors.XtraMessageBox.Show(  "�� ��������� ���������� ��������� ���� ������ � ��������.\n�� ���� ���������: " + this.uuidID.ToString(), "��������",
                                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning );
                            break;
                        }
                        default:
                        {
                            DevExpress.XtraEditors.XtraMessageBox.Show(  "������ ���������� ������� �� �������� ��������� ���������� ���������.\n��������� ���������� ��������� : " + this.m_strName, "��������",
                                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
                            break;
                        }
                    }
                }
                cmd.Dispose();
            }
            catch( System.Exception e )
            {
                DBTransaction.Rollback();
                DevExpress.XtraEditors.XtraMessageBox.Show( 
                "�� ������� ������� ��������� ���������� ���������.\n����� ������: " + e.Message, "��������",
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
                cmd.CommandText = System.String.Format( "[{0}].[dbo].[sp_AddBudgetDocState]", objProfile.GetOptionsDllDBName() );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ( ( System.Byte )( 0 ) ), ( ( System.Byte )( 0 ) ), "", System.Data.DataRowVersion.Current, null ) );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@GUID_ID", System.Data.SqlDbType.UniqueIdentifier, 4, System.Data.ParameterDirection.Output, false, ( ( System.Byte )( 0 ) ), ( ( System.Byte )( 0 ) ), "", System.Data.DataRowVersion.Current, null ) );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@BUDGETDOCSTATE_NAME", System.Data.DbType.String ) );

                cmd.Parameters[ "@BUDGETDOCSTATE_NAME" ].Value = this.m_strName;
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
                        DevExpress.XtraEditors.XtraMessageBox.Show(  "��������� ���������� ���������  '" + this.m_strName + "' ��� ���� � ��", "��������",
                            System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning );
                    }
                    else
                    {
                        DevExpress.XtraEditors.XtraMessageBox.Show(  "������ �������� ��������� ���������� ��������� ", "������",
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
                "�� ������� ������� ��������� ���������� ��������� .\n" + e.Message, "��������",
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
                cmd.CommandText = System.String.Format( "[{0}].[dbo].[sp_EditBudgetDocStateType]", objProfile.GetOptionsDllDBName() );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ( ( System.Byte )( 0 ) ), ( ( System.Byte )( 0 ) ), "", System.Data.DataRowVersion.Current, null ) );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@GUID_ID", System.Data.SqlDbType.UniqueIdentifier ) );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@BUDGETDOCSTATE_NAME", System.Data.DbType.String ) );

                cmd.Parameters[ "@GUID_ID" ].Value = this.m_uuidID;
                cmd.Parameters[ "@BUDGETDOCSTATE_NAME" ].Value = this.m_strName;
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
                        DevExpress.XtraEditors.XtraMessageBox.Show(  "��������� ���������� ��������� '" + this.m_strName + "' ��� ���� � ��", "��������",
                            System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning );
                        break;
                        case 2:
                        DevExpress.XtraEditors.XtraMessageBox.Show(  "������ � ��������� ��������������� �� ������� \n" + this.m_uuidID.ToString(), "��������",
                            System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
                        break;
                        default:
                        DevExpress.XtraEditors.XtraMessageBox.Show(  "������ ��������� ��������� ���������� ���������", "��������",
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
                "�� ������� �������� ��������� ���������� ���������.\n" + e.Message, "��������",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
            }
			finally // ������� ���������� �������
            {
                DBConnection.Close();
            }
            return bRet;
        }
        #endregion

        /// <summary>
        /// ���������� ������ ��������� ���� ���������� ���������
        /// </summary>
        /// <param name="objProfile">�������</param>
        public void LoadPicture( UniXP.Common.CProfile objProfile )
        {
            try
            {
                this.m_objBudgetDocStateDraw = CBudgetDocStateDraw.GetBudgetDocStateDraw( objProfile, this );
            }
            catch( System.Exception f )
            {
                DevExpress.XtraEditors.XtraMessageBox.Show( 
                "�� ������� ���������� ��� ����������� ��������.\n\n����� ������: " + f.Message, "��������",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
            }
            return;
        }

        public override string ToString()
        {
            return Name;
        }

    }
}
