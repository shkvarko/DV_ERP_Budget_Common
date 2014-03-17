using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace ERP_Budget.Common
{
    /// <summary>
    /// ����� "������ ��������"
    /// </summary>
    public class CRouteTemplate : IBaseListItem
    {
        #region ����������, ��������, ���������
        /// <summary>
        /// ������ ����� ��������
        /// </summary>
        private List<CRoutePoint> m_RoutePointList;
        /// <summary>
        /// ������ ����� ��������
        /// </summary>
        public List<CRoutePoint> RoutePointList
        {
            get { return m_RoutePointList; }
            set { }
        }
        /// <summary>
        /// �������� ������� ���������
        /// </summary>
        private System.String m_strDescription;
        /// <summary>
        /// �������� ������� ���������
        /// </summary>
        public System.String Description
        {
            get { return m_strDescription; }
            set { m_strDescription = value; }
        }

        #endregion

        #region ������������

        public CRouteTemplate()
        {
            this.m_uuidID = System.Guid.Empty;
            this.m_strName = "";
            this.m_strDescription = "";
            this.m_RoutePointList = new List<CRoutePoint>();
            this.m_RoutePointList.Clear();
        }

        public CRouteTemplate( System.Guid uuidRouteTemplateID )
        {
            this.m_uuidID = uuidRouteTemplateID;
            this.m_strName = "";
            this.m_strDescription = "";
            this.m_RoutePointList = new List<CRoutePoint>();
            this.m_RoutePointList.Clear();
        }

        public CRouteTemplate( System.Guid uuidRouteTemplateID, System.String ctrRouteTemplateName, System.String strDescription )
        {
            this.m_uuidID = uuidRouteTemplateID;
            this.m_strName = ctrRouteTemplateName;
            this.m_strDescription = strDescription;
            this.m_RoutePointList = new List<CRoutePoint>();
            this.m_RoutePointList.Clear();
        }
        public CRouteTemplate( System.Guid uuidRouteTemplateID, System.String ctrRouteTemplateName )
        {
            this.m_uuidID = uuidRouteTemplateID;
            this.m_strName = ctrRouteTemplateName;
            this.m_strDescription = "";
            this.m_RoutePointList = new List<CRoutePoint>();
            this.m_RoutePointList.Clear();
        }
        #endregion

        #region ������ �������� ���������
        /// <summary>
        /// ���������� ������ �������� "������ ��������"
        /// </summary>
        /// <param name="objProfile">�������</param>
        /// <returns>������ �������� "������ ��������"</returns>
        public static System.Collections.Generic.List<CRouteTemplate> GetRouteTemplateList( UniXP.Common.CProfile objProfile )
        {
            System.Collections.Generic.List<CRouteTemplate> objList = new List<CRouteTemplate>();
            System.String strErr = System.String.Empty;
            try
            {
                System.Data.SqlClient.SqlConnection DBConnection = objProfile.GetDBSource();
                if( DBConnection == null ) { return objList; }

                // ���������� � �� ��������, ����������� ������� �� ������� ������
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
                cmd.Connection = DBConnection;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = System.String.Format( "[{0}].[dbo].[sp_GetRoute]", objProfile.GetOptionsDllDBName() );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ( ( System.Byte )( 0 ) ), ( ( System.Byte )( 0 ) ), "", System.Data.DataRowVersion.Current, null ) );
                System.Data.SqlClient.SqlDataReader rs = cmd.ExecuteReader();

                if (rs.HasRows)
                {
                    CRouteTemplate objRouteTemplate = null;

                    while (rs.Read())
                    {
                        objRouteTemplate = new CRouteTemplate();

                        objRouteTemplate.m_uuidID = rs.GetGuid(0);
                        objRouteTemplate.m_strName = rs.GetString(1);
                        objRouteTemplate.m_strDescription = (rs[2] == System.DBNull.Value) ? "" : rs.GetString(2);
                        objList.Add(objRouteTemplate);
                    }
                }
                rs.Close();
                rs.Dispose();

                List<CBudgetDocEvent> objBudgetDocEventList = CBudgetDocEvent.GetBudgetDocEventList(objProfile, cmd, ref strErr);

                foreach (CRouteTemplate objRouteTemplate in objList)
                {
                    objRouteTemplate.InitRouteDeclaration(cmd, objProfile, objBudgetDocEventList);
                }
                
                
                cmd.Dispose();
                DBConnection.Close();
            }
            catch( System.Exception e )
            {
                DevExpress.XtraEditors.XtraMessageBox.Show( 
                "�� ������� �������� ������ �������� ��������.\n" + e.Message, "��������",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
            }
            return objList;
        }

        /// <summary>
        /// ���������� ������ �������� "������ ��������"
        /// </summary>
        /// <param name="objProfile">�������</param>
        /// <param name="cmd">SQL - �������</param>
        /// <returns>������ �������� "������ ��������"</returns>
        public static System.Collections.Generic.List<CRouteTemplate> GetRouteTemplateList(
            System.Data.SqlClient.SqlCommand cmd, UniXP.Common.CProfile objProfile, System.Boolean bInitRouteDeclaration = true)
        {
            System.Collections.Generic.List<CRouteTemplate> objList = new List<CRouteTemplate>();
            System.String strErr = System.String.Empty;
            try
            {
                if( cmd == null ) { return objList; }

                cmd.Parameters.Clear();
                cmd.CommandText = System.String.Format( "[{0}].[dbo].[sp_GetRoute]", objProfile.GetOptionsDllDBName() );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ( ( System.Byte )( 0 ) ), ( ( System.Byte )( 0 ) ), "", System.Data.DataRowVersion.Current, null ) );
                System.Data.SqlClient.SqlDataReader rs = cmd.ExecuteReader();
                if( rs.HasRows )
                {
                    CRouteTemplate objRouteTemplate = null;
                    while( rs.Read() )
                    {
                        objRouteTemplate = new CRouteTemplate();
                        
                        objRouteTemplate.m_uuidID = rs.GetGuid( 0 );
                        objRouteTemplate.m_strName = rs.GetString( 1 );
                        objRouteTemplate.m_strDescription = ( rs[ 2 ] == System.DBNull.Value ) ? "" : rs.GetString( 2 );
                        objList.Add( objRouteTemplate );
                    }
                }
                rs.Close();
                rs.Dispose();

                List<CBudgetDocEvent> objBudgetDocEventList = CBudgetDocEvent.GetBudgetDocEventList(objProfile, cmd, ref strErr);

                if (bInitRouteDeclaration == true)
                {
                    foreach (CRouteTemplate objRouteTemplate in objList)
                    {
                        objRouteTemplate.InitRouteDeclaration(cmd, objProfile, objBudgetDocEventList);
                    }
                }
            }
            catch( System.Exception e )
            {
                DevExpress.XtraEditors.XtraMessageBox.Show( 
                "�� ������� �������� ������ �������� ��������.\n" + e.Message, "��������",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
            }
            return objList;
        }

        /// <summary>
        /// ��������� ������ ����� �������� �������
        /// </summary>
        /// <param name="cmd">SQL - �������</param>
        /// <param name="objProfile">�������</param>
        /// <returns>true - ������� ����������; false - ������</returns>
        public System.Boolean InitRouteDeclaration(System.Data.SqlClient.SqlCommand cmd, UniXP.Common.CProfile objProfile, List<CBudgetDocEvent> objBudgetDocEventList)
        {
            System.Boolean bRet = false;

            if( cmd == null ) { return bRet; }
            try
            {
                // ���������� � �� ��������, ����������� ������� �� ������� ������
                cmd.Parameters.Clear();
                cmd.CommandText = System.String.Format( "[{0}].[dbo].[sp_GetRouteDeclaration]", objProfile.GetOptionsDllDBName() );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ( ( System.Byte )( 0 ) ), ( ( System.Byte )( 0 ) ), "", System.Data.DataRowVersion.Current, null ) );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@ROUTE_GUID_ID", System.Data.SqlDbType.UniqueIdentifier ) );
                cmd.Parameters[ "@ROUTE_GUID_ID" ].Value = this.uuidID;
                System.Data.SqlClient.SqlDataReader rs = cmd.ExecuteReader();
                if( rs.HasRows )
                {
                    while( rs.Read() )
                    {
                        // ����������������� ������ ����� ��������
                        if( rs[ 0 ] == System.DBNull.Value )
                        {
                            this.m_RoutePointList.Add( new CRoutePoint( rs.GetBoolean( 3 ), rs.GetBoolean( 4 ),
                                new CBudgetDocEvent( rs.GetGuid( 2 ), rs.GetString( 5 ), rs.GetInt32( 11 ) ),
                                new CBudgetDocState( rs.GetGuid( 1 ), rs.GetString( 7 ) ),
                                new CRoutePointGroup( rs.GetGuid( 8 ), rs.GetString( 9 ) ), rs.GetBoolean( 10 ) ) );
                        }
                        else
                        {
                            this.m_RoutePointList.Add( new CRoutePoint( rs.GetBoolean( 3 ), rs.GetBoolean( 4 ),
                                new CBudgetDocEvent( rs.GetGuid( 2 ), rs.GetString( 5 ), rs.GetInt32( 11 ) ),
                                new CBudgetDocState( rs.GetGuid( 0 ), rs.GetString( 6 ) ),
                                new CBudgetDocState( rs.GetGuid( 1 ), rs.GetString( 7 ) ),
                                new CRoutePointGroup( rs.GetGuid( 8 ), rs.GetString( 9 ) ), rs.GetBoolean( 10 ) ) );
                        }
                    }
                    rs.Close();

                    // ��������� ������ �������������, ������� ������ � ��������
                    CBudgetDocEvent objBudgetDocEvent = null;
                    foreach (CRoutePoint objRoutePoint in this.m_RoutePointList)
                    {
                        if ((objBudgetDocEventList != null) && (objBudgetDocEventList.Count > 0))
                        {
                            objBudgetDocEvent = objBudgetDocEventList.SingleOrDefault<CBudgetDocEvent>(x => x.uuidID.CompareTo(objRoutePoint.BudgetDocEvent.uuidID) == 0);
                            if ((objBudgetDocEvent != null) && (objBudgetDocEvent.UserList != null) && (objBudgetDocEvent.UserList.Count > 0))
                            {
                                objRoutePoint.BudgetDocEvent.UserList.Clear();
                                objRoutePoint.BudgetDocEvent.UserList.AddRange(objBudgetDocEvent.UserList);
                            }
                        }
                        else
                        {
                            objRoutePoint.BudgetDocEvent.LoadUserList(objProfile, cmd);
                        }
                    }

                    bRet = true;
                }
                else
                {
                    DevExpress.XtraEditors.XtraMessageBox.Show( 
                    "�� ������� �������� ������ ����� �������� ��� ������� ." + this.Name + "\n� �� �� ������� ����������.", "��������",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning );

                }
                rs.Dispose();
            }
            catch( System.Exception e )
            {
                DevExpress.XtraEditors.XtraMessageBox.Show( 
                "�� ������� �������� ������ ����� �������� ��� ������� ." + this.Name + "\n" + e.Message, "��������",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
            }
            finally // ������� ���������� �������
            {
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
                cmd.CommandText = System.String.Format( "[{0}].[dbo].[sp_GetRoute]", objProfile.GetOptionsDllDBName() );
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
                    { this.m_strDescription = rs.GetString( 2 ); }

                    // ��������� ������ ����� ��������
                    bRet = InitRouteDeclaration( cmd, objProfile, null );
                }
                else
                {
                    DevExpress.XtraEditors.XtraMessageBox.Show( 
                    "�� ������� ������������������� ����� CRouteTemplate.\n� �� �� ������� ����������.", "��������",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning );

                }
                cmd.Dispose();
                rs.Dispose();
            }
            catch( System.Exception e )
            {
                DevExpress.XtraEditors.XtraMessageBox.Show( 
                "�� ������� ������������������� ����� CRouteTemplate.\n" + e.Message, "��������",
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
                cmd.CommandText = System.String.Format( "[{0}].[dbo].[sp_DeleteRoute]", objProfile.GetOptionsDllDBName() );
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
                        case 1:
                        {
                            DevExpress.XtraEditors.XtraMessageBox.Show(  "�� ������ �������� ���� ������ � �������� ������ ������� ��������.\n" + this.m_strName, "��������",
                                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning );
                            break;
                        }
                        default:
                        {
                            DevExpress.XtraEditors.XtraMessageBox.Show(  "������ ���������� ������� �� �������� ������� ���������.\n������ �������� : " + this.m_strName + 
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
                "������ ���������� ������� �� �������� ������� ���������.\n������ �������� : " + this.m_strName + "\n" + e.Message, "��������",
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
            // �������� �� ������ �� ��������� �������
            if( this.m_RoutePointList.Count == 0 )
            {
                if( DevExpress.XtraEditors.XtraMessageBox.Show(  "� ������� �������� ����������� ���������.\n��������� ������ � ����� ����?", "�������������",
                    System.Windows.Forms.MessageBoxButtons.YesNo, System.Windows.Forms.MessageBoxIcon.Question ) == System.Windows.Forms.DialogResult.No )
                {
                    return bRet;
                }
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
                cmd.CommandText = System.String.Format( "[{0}].[dbo].[sp_AddRoute]", objProfile.GetOptionsDllDBName() );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ( ( System.Byte )( 0 ) ), ( ( System.Byte )( 0 ) ), "", System.Data.DataRowVersion.Current, null ) );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@GUID_ID", System.Data.SqlDbType.UniqueIdentifier, 4, System.Data.ParameterDirection.Output, false, ( ( System.Byte )( 0 ) ), ( ( System.Byte )( 0 ) ), "", System.Data.DataRowVersion.Current, null ) );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@ROUTE_NAME", System.Data.DbType.String ) );
                cmd.Parameters[ "@ROUTE_NAME" ].Value = this.m_strName;
                if( this.m_strDescription != "" )
                {
                    cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@ROUTE_DESCRIPTION", System.Data.DbType.String ) );
                    cmd.Parameters[ "@ROUTE_DESCRIPTION" ].Value = this.m_strDescription;
                }
                cmd.ExecuteNonQuery();
                System.Int32 iRet = ( System.Int32 )cmd.Parameters[ "@RETURN_VALUE" ].Value;
                if( iRet == 0 )
                {
                    this.m_uuidID = ( System.Guid )cmd.Parameters[ "@GUID_ID" ].Value;
                    // ��������� ��������� ������� ��������
                    bRet = this.SaveRouteDeclaration( cmd, objProfile );
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
                            DevExpress.XtraEditors.XtraMessageBox.Show(  "������ �������� � �������� ������ ���������� : '" + this.m_strName + "'", "��������",
                                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning );
                            break;
                        }
                        default:
                        {
                            DevExpress.XtraEditors.XtraMessageBox.Show(  "������ �������� ������� �������� : " + this.m_strName, "������",
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
                "�� ������� ������� ������ �������� : " + this.m_strName + "\n" + e.Message, "��������",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
            }
			finally // ������� ���������� �������
            {
                DBConnection.Close();
            }
            return bRet;
        }
        #endregion

        #region Clone
        /// <summary>
        /// �������� ������ � ��
        /// </summary>
        /// <param name="objProfile">�������</param>
        /// <param name="uuidSrcRouteID">�� ����������� ������� ��������</param>
        /// <returns>true - ������� ����������; false - ������</returns>
        public System.Boolean Clone( UniXP.Common.CProfile objProfile, System.Guid uuidSrcRouteID )
        {
            System.Boolean bRet = false;

            // ������������ �� ������ ���� ������
            if( this.m_strName == "" )
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(  "������������ �������� ������������ �������", "��������",
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
                cmd.CommandText = System.String.Format( "[{0}].[dbo].[sp_CopyRoute]", objProfile.GetOptionsDllDBName() );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ( ( System.Byte )( 0 ) ), ( ( System.Byte )( 0 ) ), "", System.Data.DataRowVersion.Current, null ) );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@GUID_ID", System.Data.SqlDbType.UniqueIdentifier, 4, System.Data.ParameterDirection.Output, false, ( ( System.Byte )( 0 ) ), ( ( System.Byte )( 0 ) ), "", System.Data.DataRowVersion.Current, null ) );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@SRCROUTE_GUID_ID", System.Data.SqlDbType.UniqueIdentifier ) );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@ROUTE_NAME", System.Data.DbType.String ) );
                cmd.Parameters[ "@SRCROUTE_GUID_ID" ].Value = uuidSrcRouteID;
                cmd.Parameters[ "@ROUTE_NAME" ].Value = this.m_strName;
                if( this.m_strDescription != "" )
                {
                    cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@ROUTE_DESCRIPTION", System.Data.DbType.String ) );
                    cmd.Parameters[ "@ROUTE_DESCRIPTION" ].Value = this.m_strDescription;
                }
                cmd.ExecuteNonQuery();
                System.Int32 iRet = ( System.Int32 )cmd.Parameters[ "@RETURN_VALUE" ].Value;
                if( iRet == 0 )
                {
                    this.m_uuidID = ( System.Guid )cmd.Parameters[ "@GUID_ID" ].Value;
                    // ��������� ��������� ������� ��������
                    bRet = this.SaveRouteDeclaration( cmd, objProfile );
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
                            DevExpress.XtraEditors.XtraMessageBox.Show(  "������ �������� � �������� ������ ���������� : '" + this.m_strName + "'", "��������",
                                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning );
                            break;
                        }
                        default:
                        {
                            DevExpress.XtraEditors.XtraMessageBox.Show(  "������ �������� ������� �������� : " + this.m_strName, "������",
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
                "�� ������� ������� ������ �������� : " + this.m_strName + "\n" + e.Message, "��������",
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
                cmd.CommandText = System.String.Format( "[{0}].[dbo].[sp_EditRoute]", objProfile.GetOptionsDllDBName() );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ( ( System.Byte )( 0 ) ), ( ( System.Byte )( 0 ) ), "", System.Data.DataRowVersion.Current, null ) );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@GUID_ID", System.Data.SqlDbType.UniqueIdentifier ) );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@ROUTE_NAME", System.Data.DbType.String ) );
                cmd.Parameters[ "@GUID_ID" ].Value = this.m_uuidID;
                cmd.Parameters[ "@ROUTE_NAME" ].Value = this.m_strName;
                if( this.m_strDescription != "" )
                {
                    cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@ROUTE_DESCRIPTION", System.Data.DbType.String ) );
                    cmd.Parameters[ "@ROUTE_DESCRIPTION" ].Value = this.m_strDescription;
                }
                cmd.ExecuteNonQuery();
                System.Int32 iRet = ( System.Int32 )cmd.Parameters[ "@RETURN_VALUE" ].Value;
                if( iRet == 0 )
                {
                    // ��������� ��������� �������
                    bRet = this.DeleteRouteDeclaration( cmd, objProfile );
                    if( bRet )
                    {
                        // ��������� ��������� ������� ��������
                        bRet = this.SaveRouteDeclaration( cmd, objProfile );
                    }
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
                            DevExpress.XtraEditors.XtraMessageBox.Show(  "������ �������� � �������� ������ ����������\n" + this.m_strName, "��������",
                                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
                            break;
                        }
                        case 2:
                        {
                            DevExpress.XtraEditors.XtraMessageBox.Show(  "������ �������� � ��������� ��������������� �� ������\n" + this.m_uuidID.ToString(), "��������",
                                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
                            break;
                        }
                        default:
                        {
                            DevExpress.XtraEditors.XtraMessageBox.Show(  "������ ��������� ������� ������� �������� : " + this.m_strName, "��������",
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
                "������ ��������� ������� ������� �������� : " + this.m_strName + "\n" + e.Message, "��������",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
            }
			finally // ������� ���������� �������
            {
                DBConnection.Close();
            }
            return bRet;
        }
        #endregion

        #region RouteDeclaration
        /// <summary>
        /// ������� �������� ������� ��������
        /// </summary>
        /// <param name="cmd">SQL-�������</param>
        /// <param name="objProfile">�������</param>
        /// <returns>true - ������� ����������; false - ������</returns>
        private System.Boolean DeleteRouteDeclaration( System.Data.SqlClient.SqlCommand cmd,
            UniXP.Common.CProfile objProfile )
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
                cmd.CommandText = System.String.Format( "[{0}].[dbo].[sp_DeleteRouteDeclaration]", objProfile.GetOptionsDllDBName() );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ( ( System.Byte )( 0 ) ), ( ( System.Byte )( 0 ) ), "", System.Data.DataRowVersion.Current, null ) );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@ROUTE_GUID_ID", System.Data.SqlDbType.UniqueIdentifier ) );

                cmd.Parameters[ "@ROUTE_GUID_ID" ].Value = this.m_uuidID;
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
                            DevExpress.XtraEditors.XtraMessageBox.Show(  "������ �������� � ��������� ��������������� �� ������\n" + this.m_uuidID.ToString(), "��������",
                                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
                            break;
                        }
                        default:
                        {
                            DevExpress.XtraEditors.XtraMessageBox.Show(  "������ �������� ��������� ������� �������� : " + this.m_strName, "��������",
                                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
                            break;
                        }
                    }
                }
            }
            catch( System.Exception e )
            {
                DevExpress.XtraEditors.XtraMessageBox.Show( 
                "������ �������� ��������� ������� �������� : " + this.m_strName + "\n" + e.Message, "��������",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
            }
			finally // ������� ���������� �������
            {
            }
            return bRet;
        }
        /// <summary>
        /// ��������� � �� ��������� ������� ��������
        /// </summary>
        /// <param name="cmd">SQL - �������</param>
        /// <param name="objProfile">�������</param>
        /// <returns>true - ������� ����������; false - ������</returns>
        private System.Boolean SaveRouteDeclaration( System.Data.SqlClient.SqlCommand cmd,
            UniXP.Common.CProfile objProfile )
        {
            System.Boolean bRet = false;
            if( cmd == null ) { return bRet; }
            if( this.m_RoutePointList.Count == 0 ) { return true; }

            // ���������� ������������� �� ������ ���� ������
            if( this.m_uuidID == System.Guid.Empty )
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(  "������������ �������� ����������� �������������� �������", "��������",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
                return bRet;
            }

            try
            {
                // ��������� �� ��������� �������� � ��������� �� � �� 
                CRoutePoint objRoutePoint = null;
                System.Int32 iErrCount = 0;
                for( System.Int32 i = 0; i < this.m_RoutePointList.Count; i++ )
                {
                    objRoutePoint = this.m_RoutePointList[ i ];
                    objRoutePoint.IsEnter = ( i == 0 );
                    if( objRoutePoint.IsEnter && ( this.m_RoutePointList.Count > 1 ) ) { objRoutePoint.IsExit = false; }
                    if( objRoutePoint.AddRouteItem( objProfile, this.m_uuidID, cmd ) == false )
                    {
                        iErrCount++;
                    }
                    if( iErrCount > 0 ) { break; }
                }
                objRoutePoint = null;
                bRet = ( iErrCount == 0 );
            }
            catch( System.Exception e )
            {
                DevExpress.XtraEditors.XtraMessageBox.Show( 
                "�� ������� ��������� ������ ��������.\n" + e.Message, "��������",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
            }
			finally // ������� ���������� �������
            {
            }
            return bRet;
        }
        #endregion

        #region ����������� ��������

        // ��������!!!
        // ������ �� ����� ������� ����� ������ ���������� � �������������� ��������,
        // ����� � �������� � ������������� � ������������ ������� ���������� ������������ 


        private const System.String strManager = "�����������";
        private const System.String strCoordinator = "�����������";
        /// <summary>
        /// �������� ��������  �� ����������� �����������
        /// </summary>
        /// <param name="objRoutePointList">������ ����� ��������</param>
        /// <returns>true - ����� ��������������; false - ������ ��������������</returns>
        public static System.Boolean IsCanOptimize(List<CRoutePoint> objRoutePointList)
        {
            // ���� �������, ��� ������� ����� ��������������,
            // ���� � ������ "�����������" � ������ "�����������" ������ ���� � ��� �� �������
            // ��! ��� ���������� �������, �� ������� � �������������� � ���� ����� �� ����
            System.Boolean bRet = false;
            try
            {
                System.Boolean bOK = false;
                System.Boolean bDublicate = false;
                System.Guid uuidRoutePointGroupID = System.Guid.Empty;
                List<CUser> objUserList = new List<CUser>();
                objUserList.Clear();

                // ������� �� ������ �������� � ����� ��������� ������� �� ������� � ������ �����,
                // ���� �������, �� ��������� ������ ����������� � �������� �������������
                foreach (ERP_Budget.Common.CRoutePoint objRoutePoint in objRoutePointList)
                {
                    bOK = (objRoutePoint.UserEvent != null);
                    if (bOK == false) { break; }
                    else
                    {
                        if (objRoutePoint.ShowInDocument == true)
                        {
                            if (objRoutePoint.RoutePointGroup.uuidID.CompareTo(uuidRoutePointGroupID) != 0)
                            {
                                if ((objRoutePoint.RoutePointGroup.Name == strManager) ||
                                    (objRoutePoint.RoutePointGroup.Name == strCoordinator))
                                {
                                    objUserList.Add(objRoutePoint.UserEvent);
                                }
                                uuidRoutePointGroupID = objRoutePoint.RoutePointGroup.uuidID;
                            }

                        }
                    }
                }

                if ((bOK == true) && (objUserList.Count > 0))
                {
                    // ������� ��������� ��������, ��������� ������ ����������,
                    // ��� �� ������������� ������������� � ������ "�����������" � ������ "�����������"
                    System.Int32 iUserID = 0;
                    foreach (CUser objUser in objUserList)
                    {
                        bDublicate = (objUser.ulID == iUserID);
                        iUserID = objUser.ulID;
                        if (bDublicate) { break; }
                    }
                }


                bRet = bDublicate;
            }
            catch (System.Exception f)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(
                "������ �������� ��������  �� ����������� �����������.\n\n����� ������: " + f.Message, "��������",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }

            return bRet;
        }
        /// <summary>
        /// ������������ (���������) �������
        /// </summary>
        /// <param name="objRoutePointList">������ ����� ��������</param>
        /// <returns>true - ������� ����������; false - ������</returns>
        public static System.Boolean Optimize(List<CRoutePoint> objRoutePointList)
        {
            System.Boolean bRet = false;
            try
            {
                // ������ �������� �� ������ ������, ������������� �� ����� �������������� �������
                if ( CRouteTemplate.IsCanOptimize(objRoutePointList) == true)
                {
                    // �����!
                    // ������ ��� ����� ������ ����� "�����������", � ���� ���� ����� ��� �� ���������,
                    // �� � ������������ �� ��� ������ ����� �������� ������� ��� �����������
                    List<System.Int32> objListRemoved = new List<int>();
                    CBudgetDocState objINBudgetDocState = null;
                    CBudgetDocState objOutBudgetDocState = null;
                    for (System.Int32 i = 0; i < objRoutePointList.Count; i++)
                    {
                        if (objRoutePointList[i].RoutePointGroup.Name == strCoordinator)
                        {
                            objListRemoved.Add(i);
                            if (objRoutePointList[i].ShowInDocument == true)
                            {
                                objINBudgetDocState = objRoutePointList[i].BudgetDocStateIN;
                                objOutBudgetDocState = objRoutePointList[i].BudgetDocStateOUT;
                            }
                        }
                    }

                    if ((objListRemoved.Count > 0) && (objINBudgetDocState != null) && (objOutBudgetDocState != null))
                    {
                        foreach (ERP_Budget.Common.CRoutePoint objRoutePoint in objRoutePointList)
                        {
                            if (objRoutePoint.BudgetDocStateIN == null) { continue; }
                            if (objRoutePoint.BudgetDocStateIN.uuidID.CompareTo(objOutBudgetDocState.uuidID) == 0)
                            {
                                objRoutePoint.BudgetDocStateIN = objINBudgetDocState;
                            }
                        }

                        for (System.Int32 i2 = (objListRemoved.Count - 1); i2 >= 0; i2--)
                        {
                            objRoutePointList.RemoveAt(objListRemoved[i2]);
                        }

                        objListRemoved = null;
                        objINBudgetDocState = null;
                        objOutBudgetDocState = null;

                        bRet = true;
                    }
                   
                }
            }
            catch (System.Exception f)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(
                "������ ����������� ��������.\n\n����� ������: " + f.Message, "��������",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }

            return bRet;
        }
        #endregion
    }
}
