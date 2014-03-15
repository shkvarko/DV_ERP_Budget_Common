using System;
using System.Collections.Generic;
using System.Text;

namespace ERP_Budget.Common
{
    /// <summary>
    /// �����, ��������������� ��� ����������� ������� ������������ � �������
    /// </summary>
    public static class CBudgetDocViewAccess
    {
        /** <summary>
         * ���������� ������� ���� "������������ ����� - ��������" 
         * 
         *      �������� 1  �������� 2  �������� 3
         * �� 1     1           0           1
         * �� 2     0           1           0
         * </summary> */
        /// <param name="objProfile">�������</param>
        /// <returns>������� ���� "������������ ����� - ��������" </returns>
        public static System.Data.DataTable GetEventRightList( UniXP.Common.CProfile objProfile )
        {
            System.Data.DataTable dtEventRight = null;
            System.Data.DataTable dtSrcEventRight = new System.Data.DataTable( "dtSrcEventRight" );
            try
            {
                // ���������� ��������� ������������ �������
                dtEventRight = new System.Data.DataTable( "dtEventRight" );
                dtEventRight.Columns.Add( new System.Data.DataColumn( "RIGHT_ID", System.Type.GetType( "System.Int32" ) ) );
                dtEventRight.Columns.Add( new System.Data.DataColumn( "RIGHT_NAME", System.Type.GetType( "System.String" ) ) );
                dtEventRight.Columns[ 1 ].Caption = "������������ �����";
                // ��������� ������� "�������� 1", "�������� 2", ... "�������� n"
                System.Data.DataTable dtBudgetDocEvent = CBudgetDocEvent.GetBudgetDocEventTable( objProfile );
                System.Data.DataColumn clmn = null;
                foreach( System.Data.DataRow row in dtBudgetDocEvent.Rows )
                {
                    clmn = new System.Data.DataColumn( ( ( System.Guid )row[ "GUID_ID" ] ).ToString(), System.Type.GetType( "System.Boolean" ) );
                    clmn.Caption = ( System.String )row[ "BUDGETDOCEVENT_NAME" ];
                    dtEventRight.Columns.Add( clmn );
                }
                clmn = null;
                dtBudgetDocEvent = null;

                // ��������� ������
                System.Data.DataRow rowEventRight = null;
                System.Collections.Generic.List<CDynamicRight> objDynamicRightsList = CDynamicRight.GetDynamicRightsList( objProfile );
                for( System.Int32 i = 0; i < objDynamicRightsList.Count; i++ )
                {
                    if( ( objDynamicRightsList[ i ].Name == ERP_Budget.Global.Consts.strDRAddRight ) || 
                        ( objDynamicRightsList[ i ].Name == ERP_Budget.Global.Consts.strDREditRootDebitArticle ) || 
                        ( objDynamicRightsList[ i ].Name == ERP_Budget.Global.Consts.strDRRouteEditor ) )
                    { continue; }
                    rowEventRight = dtEventRight.NewRow();
                    rowEventRight[ "RIGHT_ID" ] = objDynamicRightsList[ i ].ID;
                    rowEventRight[ "RIGHT_NAME" ] = objDynamicRightsList[ i ].Name;
                    for( System.Int32 i2 = 2; i2 < dtEventRight.Columns.Count; i2++ ) { rowEventRight[ i2 ] = false; }
                    dtEventRight.Rows.Add( rowEventRight );
                }
                rowEventRight = null;
                objDynamicRightsList = null;

                // ���������� �������, � ������� ������ �� �� ���������� � ����� "������������ ����� - ��������"
                dtSrcEventRight.Columns.Add( new System.Data.DataColumn( "SRC_RIGHT_ID", System.Type.GetType( "System.Int32" ) ) );
                dtSrcEventRight.Columns.Add( new System.Data.DataColumn( "SRC_RIGHT_NAME", System.Type.GetType( "System.String" ) ) );
                dtSrcEventRight.Columns.Add( new System.Data.DataColumn( "SRC_BUDGETDOCEVENT_GUID_ID", System.Type.GetType( "System.Guid" ) ) );
                dtSrcEventRight.Columns.Add( new System.Data.DataColumn( "SRC_BUDGETDOCEVENT_NAME", System.Type.GetType( "System.String" ) ) );
                dtSrcEventRight.Columns.Add( new System.Data.DataColumn( "SRC_bAccess", System.Type.GetType( "System.Boolean" ) ) );
                // ������������ � ��
                System.Data.SqlClient.SqlConnection DBConnection = objProfile.GetDBSource();
                if( DBConnection != null )
                {
                    System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
                    cmd.Connection = DBConnection;
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.CommandText = System.String.Format( "[{0}].[dbo].[sp_GetBudgetDocEventRight]", objProfile.GetOptionsDllDBName() );
                    cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ( ( System.Byte )( 0 ) ), ( ( System.Byte )( 0 ) ), "", System.Data.DataRowVersion.Current, null ) );
                    System.Data.SqlClient.SqlDataReader rs = cmd.ExecuteReader();
                    if( rs.HasRows )
                    {
                        System.Data.DataRow row = null;
                        while( rs.Read() )
                        {
                            row = dtSrcEventRight.NewRow();
                            row[ "SRC_RIGHT_ID" ] = rs.GetInt32( 0 );
                            row[ "SRC_RIGHT_NAME" ] = rs.GetString( 1 );
                            row[ "SRC_BUDGETDOCEVENT_GUID_ID" ] = rs.GetGuid( 2 );
                            row[ "SRC_BUDGETDOCEVENT_NAME" ] = rs.GetString( 3 );
                            row[ "SRC_bAccess" ] = rs.GetBoolean( 4 );
                            dtSrcEventRight.Rows.Add( row );
                        }
                        dtSrcEventRight.AcceptChanges();
                    }
                    cmd.Dispose();
                    rs.Dispose();
                    DBConnection.Close();
                    // ������ ������ � ������������ ������� ����� ���������������� � ������� � dtSrcEventRight
                    if( ( dtSrcEventRight.Rows.Count > 0 ) && ( dtEventRight.Rows.Count > 0 ) )
                    {
                        System.String strClmnName = "";
                        foreach( System.Data.DataRow rowdtEventRight in dtEventRight.Rows )
                        {
                            foreach( System.Data.DataRow rowSrcEventRight in dtSrcEventRight.Rows )
                            {
                                if( ( System.Int32 )rowSrcEventRight[ "SRC_RIGHT_ID" ] == ( System.Int32 )rowdtEventRight[ "RIGHT_ID" ] )
                                {
                                    strClmnName = ( ( System.Guid )rowSrcEventRight[ "SRC_BUDGETDOCEVENT_GUID_ID" ] ).ToString();
                                    rowdtEventRight[ strClmnName ] = ( System.Boolean )rowSrcEventRight[ "SRC_bAccess" ];
                                }
                            }
                        }
                        dtEventRight.AcceptChanges();
                    }

                } // if( DBConnection != null ) 
            }
            catch( System.Exception f )
            {
                DevExpress.XtraEditors.XtraMessageBox.Show( 
                "�� ������� �������� ������� \"������������ ����� - ��������\".\n\n����� ������: " + 
                f.Message, "��������",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
            }
            finally
            {
                dtSrcEventRight = null;
                //dtSrcEventRight.Dispose();
            }

            return dtEventRight;
        }

        /// <summary>
        /// ��������� � �� ������ "������������ ����� - ��������"
        /// </summary>
        /// <param name="objProfile">�������</param>
        /// <param name="dtEventRight">������ "������������ ����� - ��������"</param>
        /// <returns>true - �������� ����������; false - ������</returns>
        public static System.Boolean SaveEventRight( UniXP.Common.CProfile objProfile,
            System.Data.DataTable dtEventRight )
        {
            System.Boolean bRet = false;
            if( dtEventRight == null ) { return bRet; }
            System.Data.SqlClient.SqlConnection DBConnection = objProfile.GetDBSource();
            if( DBConnection == null ) { return bRet; }
            System.Data.SqlClient.SqlTransaction DBTransaction = DBConnection.BeginTransaction();

            try
            {
                // ���������� � �� ��������, ����������� SQL - �������
                // ������ ������� ��� ������ � T_BUDGETDOCEVENTRIGHT
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
                cmd.Connection = DBConnection;
                cmd.Transaction = DBTransaction;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = System.String.Format( "[{0}].[dbo].[sp_DeleteBudgetDocEventRight]", objProfile.GetOptionsDllDBName() );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ( ( System.Byte )( 0 ) ), ( ( System.Byte )( 0 ) ), "", System.Data.DataRowVersion.Current, null ) );
                cmd.ExecuteNonQuery();
                System.Int32 iRet = ( System.Int32 )cmd.Parameters[ "@RETURN_VALUE" ].Value;
                if( iRet == 0 )
                {
                    // ������ ��������� ������ "������������ ����� - ��������" � ��
                    cmd.Parameters.Clear();
                    cmd.CommandText = System.String.Format( "[{0}].[dbo].[sp_AssignBudgetDocEventRight]", objProfile.GetOptionsDllDBName() );
                    cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ( ( System.Byte )( 0 ) ), ( ( System.Byte )( 0 ) ), "", System.Data.DataRowVersion.Current, null ) );
                    cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@RIGHT_ID", System.Data.SqlDbType.Int ) );
                    cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@BUDGETDOCEVENT_GUID_ID", System.Data.SqlDbType.UniqueIdentifier ) );
                    cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@bAssign", System.Data.SqlDbType.Bit ) );
                    cmd.Parameters[ "@bAssign" ].Value = 1;
                    foreach( System.Data.DataRow row in dtEventRight.Rows )
                    {
                        cmd.Parameters[ "@RIGHT_ID" ].Value = ( System.Int32 )row[ "RIGHT_ID" ];
                        cmd.Parameters[ "@BUDGETDOCEVENT_GUID_ID" ].Value = ( System.Guid )row[ "BUDGETDOCEVENT_GUID_ID" ];
                        cmd.ExecuteNonQuery();
                        iRet = ( System.Int32 )cmd.Parameters[ "@RETURN_VALUE" ].Value;
                        if( iRet > 0 ) { break; }
                    }
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
                            DevExpress.XtraEditors.XtraMessageBox.Show(  "C������ ���������� ��������� � �������� ��������������� �� �������.\n�� : " + 
                                cmd.Parameters[ "@RIGHT_ID" ].Value.ToString(), "��������",
                                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning );
                            break;
                            case 2:
                            DevExpress.XtraEditors.XtraMessageBox.Show(  "������������ ����� � �������� ��������������� �� �������.\n�� : " + 
                                cmd.Parameters[ "@BUDGETDOCEVENT_GUID_ID" ].Value.ToString(), "��������",
                                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
                            break;
                            default:
                            DevExpress.XtraEditors.XtraMessageBox.Show(  "������ ���������� ���������� � ���� ������.", "��������",
                                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
                            break;
                        }
                    }
                }
                else
                {
                    // ���������� ����������
                    DBTransaction.Rollback();
                    DevExpress.XtraEditors.XtraMessageBox.Show(  "������ �������� ������� � ������� T_BUDGETDOCEVENTRIGHT.", "��������",
                        System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
                }
                cmd.Dispose();

            }
            catch( System.Exception f )
            {
                DevExpress.XtraEditors.XtraMessageBox.Show( 
                "�� ������� ��������� � �� �������� ������������ ���� � �������� ���������� ���������.\n\n����� ������: " + 
                f.Message, "��������",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
            }
            finally
            {
            }

            return bRet;
        }

        /** <summary>
         * ���������� ������� ���� "������������ ����� - ��������� - ��������" 
         * 
         * ��1
         * ---
         * ��N
         *              �������� 1  �������� 2  �������� 3
         * ��������� 1     1           0           1
         * ��������� 2     0           1           0
         * </summary> */
        /// <param name="objProfile">�������</param>
        /// <returns>������� ���� "������������ ����� - ��������� - ��������" </returns>
        public static System.Data.DataTable GetViewAccessList( UniXP.Common.CProfile objProfile )
        {
            System.Data.DataTable dtViewAccess = null;
            System.Data.DataTable dtSrcViewAccess = new System.Data.DataTable( "dtSrcViewAccess" );
            try
            {
                // ���������� ��������� ������������ �������
                dtViewAccess = new System.Data.DataTable( "dtViewAccess" );
                dtViewAccess.Columns.Add( new System.Data.DataColumn( "VA_RIGHT_ID", System.Type.GetType( "System.Int32" ) ) );
                dtViewAccess.Columns.Add( new System.Data.DataColumn( "VA_BUDGETDOCSTATE_GUID_ID", System.Type.GetType( "System.Guid" ) ) );
                dtViewAccess.Columns.Add( new System.Data.DataColumn( "VA_BUDGETDOCSTATE_NAME", System.Type.GetType( "System.String" ) ) );
                dtViewAccess.Columns.Add( new System.Data.DataColumn( "VA_ADDRIGHT", System.Type.GetType( "System.Boolean" ) ) );
                dtViewAccess.Columns[ "VA_BUDGETDOCSTATE_NAME" ].Caption = "������� ���������";
                dtViewAccess.Columns[ "VA_ADDRIGHT" ].Caption = "���. �����";
                // ��������� ������� "�������� 1", "�������� 2", ... "�������� n"
                System.Data.DataTable dtBudgetDocEvent = CBudgetDocEvent.GetBudgetDocEventTable( objProfile );
                System.Data.DataColumn clmn = null;
                foreach( System.Data.DataRow row in dtBudgetDocEvent.Rows )
                {
                    clmn = new System.Data.DataColumn( ( ( System.Guid )row[ "GUID_ID" ] ).ToString(), System.Type.GetType( "System.Boolean" ) );
                    clmn.Caption = ( System.String )row[ "BUDGETDOCEVENT_NAME" ];
                    dtViewAccess.Columns.Add( clmn );
                }
                clmn = null;
                dtBudgetDocEvent = null;

                // ��������� ������
                System.Data.DataRow rowViewAccess0 = null;
                System.Data.DataRow rowViewAccess1 = null;
                System.Collections.Generic.List<CDynamicRight> objDynamicRightsList = CDynamicRight.GetDynamicRightsList( objProfile );
                System.Collections.Generic.List<CBudgetDocState> objBudgetDocStateList = CBudgetDocState.GetBudgetDocStateListObj( objProfile );
                // ��������� �� ������ ������������ ����
                for( System.Int32 i = 0; i < objDynamicRightsList.Count; i++ )
                {
                    if( ( objDynamicRightsList[ i ].Name == ERP_Budget.Global.Consts.strDRAddRight ) || 
                        ( objDynamicRightsList[ i ].Name == ERP_Budget.Global.Consts.strDREditRootDebitArticle ) || 
                        ( objDynamicRightsList[ i ].Name == ERP_Budget.Global.Consts.strDRRouteEditor ) )
                    { continue; }

                    // ��������� �� ������ ���������
                    for( System.Int32 i2 = 0; i2 < objBudgetDocStateList.Count; i2++ )
                    {
                        rowViewAccess0 = dtViewAccess.NewRow();
                        rowViewAccess1 = dtViewAccess.NewRow();
                        rowViewAccess0[ "VA_RIGHT_ID" ] = objDynamicRightsList[ i ].ID;
                        rowViewAccess1[ "VA_RIGHT_ID" ] = objDynamicRightsList[ i ].ID;
                        rowViewAccess0[ "VA_ADDRIGHT" ] = false;
                        rowViewAccess1[ "VA_ADDRIGHT" ] = true;
                        rowViewAccess0[ "VA_BUDGETDOCSTATE_GUID_ID" ] = objBudgetDocStateList[ i2 ].uuidID;
                        rowViewAccess1[ "VA_BUDGETDOCSTATE_GUID_ID" ] = objBudgetDocStateList[ i2 ].uuidID;
                        rowViewAccess0[ "VA_BUDGETDOCSTATE_NAME" ] = objBudgetDocStateList[ i2 ].Name;
                        rowViewAccess1[ "VA_BUDGETDOCSTATE_NAME" ] = objBudgetDocStateList[ i2 ].Name;
                        // ��������� �� ������ �������
                        for( System.Int32 i3 = 4; i3 < dtViewAccess.Columns.Count; i3++ )
                        {
                            rowViewAccess0[ i3 ] = false;
                            rowViewAccess1[ i3 ] = false;
                        }
                        dtViewAccess.Rows.Add( rowViewAccess0 );
                        dtViewAccess.Rows.Add( rowViewAccess1 );
                    }
                }
                rowViewAccess0 = null;
                rowViewAccess1 = null;
                objDynamicRightsList = null;
                objBudgetDocStateList = null;

                // ���������� �������, � ������� ������ �� �� ���������� � ����� "������������ ����� - ������� ��������� - ��������"
                dtSrcViewAccess.Columns.Add( new System.Data.DataColumn( "SRC_RIGHT_ID", System.Type.GetType( "System.Int32" ) ) );
                dtSrcViewAccess.Columns.Add( new System.Data.DataColumn( "SRC_RIGHT_NAME", System.Type.GetType( "System.String" ) ) );
                dtSrcViewAccess.Columns.Add( new System.Data.DataColumn( "SRC_BUDGETDOCEVENT_GUID_ID", System.Type.GetType( "System.Guid" ) ) );
                dtSrcViewAccess.Columns.Add( new System.Data.DataColumn( "SRC_BUDGETDOCEVENT_NAME", System.Type.GetType( "System.String" ) ) );
                dtSrcViewAccess.Columns.Add( new System.Data.DataColumn( "SRC_BUDGETDOCSTATE_GUID_ID", System.Type.GetType( "System.Guid" ) ) );
                dtSrcViewAccess.Columns.Add( new System.Data.DataColumn( "SRC_BUDGETDOCSTATE_NAME", System.Type.GetType( "System.String" ) ) );
                dtSrcViewAccess.Columns.Add( new System.Data.DataColumn( "SRC_bAccess", System.Type.GetType( "System.Boolean" ) ) );
                dtSrcViewAccess.Columns.Add( new System.Data.DataColumn( "SRC_bAddRight", System.Type.GetType( "System.Boolean" ) ) );
                // ������������ � ��
                System.Data.SqlClient.SqlConnection DBConnection = objProfile.GetDBSource();
                if( DBConnection != null )
                {
                    System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
                    cmd.Connection = DBConnection;
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.CommandText = System.String.Format( "[{0}].[dbo].[sp_GetBudgetDocViewAccess]", objProfile.GetOptionsDllDBName() );
                    cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ( ( System.Byte )( 0 ) ), ( ( System.Byte )( 0 ) ), "", System.Data.DataRowVersion.Current, null ) );
                    System.Data.SqlClient.SqlDataReader rs = cmd.ExecuteReader();
                    if( rs.HasRows )
                    {
                        System.Data.DataRow row = null;
                        while( rs.Read() )
                        {
                            row = dtSrcViewAccess.NewRow();
                            row[ "SRC_RIGHT_ID" ] = rs.GetInt32( 0 );
                            row[ "SRC_RIGHT_NAME" ] = rs.GetString( 1 );
                            row[ "SRC_BUDGETDOCEVENT_GUID_ID" ] = rs.GetGuid( 2 );
                            row[ "SRC_BUDGETDOCEVENT_NAME" ] = rs.GetString( 3 );
                            row[ "SRC_BUDGETDOCSTATE_GUID_ID" ] = rs.GetGuid( 4 );
                            row[ "SRC_BUDGETDOCSTATE_NAME" ] = rs.GetString( 5 );
                            row[ "SRC_bAddRight" ] = rs.GetBoolean( 6 );
                            row[ "SRC_bAccess" ] = rs.GetBoolean( 7 );
                            dtSrcViewAccess.Rows.Add( row );
                        }
                        dtSrcViewAccess.AcceptChanges();
                    }
                    cmd.Dispose();
                    rs.Dispose();
                    DBConnection.Close();
                    // ������ ������ � ������������ ������� ����� ���������������� � ������� � dtSrcViewAccess
                    if( ( dtSrcViewAccess.Rows.Count > 0 ) && ( dtViewAccess.Rows.Count > 0 ) )
                    {
                        System.String strClmnName = "";
                        foreach( System.Data.DataRow rowdtViewAccess in dtViewAccess.Rows )
                        {
                            foreach( System.Data.DataRow rowSrcViewAccess in dtSrcViewAccess.Rows )
                            {
                                if( ( ( System.Int32 )rowSrcViewAccess[ "SRC_RIGHT_ID" ] == ( System.Int32 )rowdtViewAccess[ "VA_RIGHT_ID" ] ) && 
                                    ( ( ( System.Guid )rowSrcViewAccess[ "SRC_BUDGETDOCSTATE_GUID_ID" ] ).CompareTo( ( System.Guid )rowdtViewAccess[ "VA_BUDGETDOCSTATE_GUID_ID" ] ) == 0 ) &&
                                    ( ( System.Boolean )rowSrcViewAccess[ "SRC_bAddRight" ] == ( System.Boolean )rowdtViewAccess[ "VA_ADDRIGHT" ] ) )
                                {
                                    strClmnName = ( ( System.Guid )rowSrcViewAccess[ "SRC_BUDGETDOCEVENT_GUID_ID" ] ).ToString();
                                    rowdtViewAccess[ strClmnName ] = ( System.Boolean )rowSrcViewAccess[ "SRC_bAccess" ];
                                }
                            }
                        }
                        dtViewAccess.AcceptChanges();
                    }

                } // if( DBConnection != null ) 
            }
            catch( System.Exception f )
            {
                DevExpress.XtraEditors.XtraMessageBox.Show( 
                "�� ������� �������� ������� \"������������ ����� - ��������� - ��������\".\n\n����� ������: " + 
                f.Message, "��������",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
            }
            finally
            {
                dtSrcViewAccess = null;
            }

            return dtViewAccess;
        }

        /// <summary>
        /// ��������� � �� ������ "������������ ����� - ��������� - ��������"
        /// </summary>
        /// <param name="objProfile">�������</param>
        /// <param name="dtViewAccess">������ "������������ ����� - ��������� - ��������"</param>
        /// <returns>true - �������� ����������; false - ������</returns>
        public static System.Boolean SaveViewAccess( UniXP.Common.CProfile objProfile,
            System.Data.DataTable dtViewAccess )
        {
            System.Boolean bRet = false;
            if( dtViewAccess == null ) { return bRet; }
            System.Data.SqlClient.SqlConnection DBConnection = objProfile.GetDBSource();
            if( DBConnection == null ) { return bRet; }
            System.Data.SqlClient.SqlTransaction DBTransaction = DBConnection.BeginTransaction();

            try
            {
                // ���������� � �� ��������, ����������� SQL - �������
                // ������ ������� ��� ������ � T_BUDGETDOCEVENTRIGHT
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
                cmd.Connection = DBConnection;
                cmd.Transaction = DBTransaction;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = System.String.Format( "[{0}].[dbo].[sp_DeleteBudgetDocViewAccess]", objProfile.GetOptionsDllDBName() );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ( ( System.Byte )( 0 ) ), ( ( System.Byte )( 0 ) ), "", System.Data.DataRowVersion.Current, null ) );
                cmd.ExecuteNonQuery();
                System.Int32 iRet = ( System.Int32 )cmd.Parameters[ "@RETURN_VALUE" ].Value;
                if( iRet == 0 )
                {
                    // ������ ��������� ������ "������������ ����� - ��������" � ��
                    cmd.Parameters.Clear();
                    cmd.CommandText = System.String.Format( "[{0}].[dbo].[sp_AssignBudgetDocViewAccess]", objProfile.GetOptionsDllDBName() );
                    cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ( ( System.Byte )( 0 ) ), ( ( System.Byte )( 0 ) ), "", System.Data.DataRowVersion.Current, null ) );
                    cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@RIGHT_ID", System.Data.SqlDbType.Int ) );
                    cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@BUDGETDOCSTATE_GUID_ID", System.Data.SqlDbType.UniqueIdentifier ) );
                    cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@BUDGETDOCEVENT_GUID_ID", System.Data.SqlDbType.UniqueIdentifier ) );
                    cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@bAddRight", System.Data.SqlDbType.Bit ) );
                    cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@bAssign", System.Data.SqlDbType.Bit ) );
                    cmd.Parameters[ "@bAssign" ].Value = 1;
                    foreach( System.Data.DataRow row in dtViewAccess.Rows )
                    {
                        cmd.Parameters[ "@RIGHT_ID" ].Value = ( System.Int32 )row[ "RIGHT_ID" ];
                        cmd.Parameters[ "@BUDGETDOCSTATE_GUID_ID" ].Value = ( System.Guid )row[ "BUDGETDOCSTATE_GUID_ID" ];
                        cmd.Parameters[ "@BUDGETDOCEVENT_GUID_ID" ].Value = ( System.Guid )row[ "BUDGETDOCEVENT_GUID_ID" ];
                        cmd.Parameters[ "@bAddRight" ].Value = ( System.Boolean )row[ "bAddRight" ];
                        cmd.ExecuteNonQuery();
                        iRet = ( System.Int32 )cmd.Parameters[ "@RETURN_VALUE" ].Value;
                        if( iRet > 0 ) { break; }
                    }
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
                            DevExpress.XtraEditors.XtraMessageBox.Show(  "C������ ���������� ��������� � �������� ��������������� �� �������.\n�� : " + 
                                cmd.Parameters[ "@RIGHT_ID" ].Value.ToString(), "��������",
                                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning );
                            break;
                            case 2:
                            DevExpress.XtraEditors.XtraMessageBox.Show(  "������������ ����� � �������� ��������������� �� �������.\n�� : " + 
                                cmd.Parameters[ "@BUDGETDOCEVENT_GUID_ID" ].Value.ToString(), "��������",
                                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
                            break;
                            case 3:
                            DevExpress.XtraEditors.XtraMessageBox.Show(  "��������� ���������� ��������� � �������� ��������������� �� �������.\n�� : " + 
                                cmd.Parameters[ "@BUDGETDOCSTATE_GUID_ID" ].Value.ToString(), "��������",
                                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning );
                            break;
                            default:
                            DevExpress.XtraEditors.XtraMessageBox.Show(  "������ ���������� ���������� � ���� ������.", "��������",
                                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
                            break;
                        }
                    }
                }
                else
                {
                    // ���������� ����������
                    DBTransaction.Rollback();
                    DevExpress.XtraEditors.XtraMessageBox.Show(  "������ �������� ������� � ������� T_BUDGETDOCVIEWACCESS.", "��������",
                        System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
                }
                cmd.Dispose();

            }
            catch( System.Exception f )
            {
                DevExpress.XtraEditors.XtraMessageBox.Show( 
                "�� ������� ��������� � �� �������� ������������ ���� \n� �������� � ���������� ���������� ���������.\n\n����� ������: " + 
                f.Message, "��������",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
            }
            finally
            {
            }

            return bRet;
        }


    }
}
