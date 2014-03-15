using System;
using System.Collections.Generic;
using System.Text;

namespace ERP_Budget.Common
{
    /// <summary>
    /// ����� "������� ���������� ���������"
    /// </summary>
    public class CBudgetRoute : IBaseListItem
    {
        #region ����������, ��������, ���������
        /// <summary>
        /// ������ ����� ��������
        /// </summary>
        private System.Collections.Generic.List<CRoutePoint> m_RoutePointList;
        /// <summary>
        /// ������ ����� ��������
        /// </summary>
        public System.Collections.Generic.List<CRoutePoint> RoutePointList
        {
            get { return m_RoutePointList; }
            set { }
        }
        /// <summary>
        /// ���������� ������������� ���������� ���������
        /// </summary>
        private System.Guid m_uuidBudgetDocID;
        /// <summary>
        /// ���������� ������������� ���������� ���������
        /// </summary>
        public System.Guid BudgetDocID
        {
            get { return m_uuidBudgetDocID; }
            set { m_uuidBudgetDocID = value; }
        }

        #endregion

        #region ������������
        public CBudgetRoute()
        {
            this.m_uuidID = System.Guid.Empty;
            this.m_strName = "";
            this.m_uuidBudgetDocID = System.Guid.Empty;
            this.m_RoutePointList = new List<CRoutePoint>();
            this.m_RoutePointList.Clear();
        }

        public CBudgetRoute( System.Guid uuidBudgetDocID )
        {
            this.m_uuidID = System.Guid.Empty;
            this.m_strName = "";
            this.m_uuidBudgetDocID = uuidBudgetDocID;
            this.m_RoutePointList = new List<CRoutePoint>();
            this.m_RoutePointList.Clear();
        }

        public CBudgetRoute( System.Guid uuidBudgetDocID, List<CRoutePoint> objListRoutePoint )
        {
            this.m_uuidID = System.Guid.Empty;
            this.m_strName = "";
            this.m_uuidBudgetDocID = uuidBudgetDocID;
            this.m_RoutePointList = objListRoutePoint;
        }

        public CBudgetRoute( System.Guid uuidBudgetDocID, System.Data.SqlClient.SqlCommand cmd, UniXP.Common.CProfile objProfile )
        {
            this.m_uuidID = System.Guid.Empty;
            this.m_strName = "";
            this.m_uuidBudgetDocID = uuidBudgetDocID;
            this.m_RoutePointList = new List<CRoutePoint>();
            this.InitRouteDeclaration( cmd, objProfile);
        }
        #endregion

        #region ������ ����� ��������
        /// <summary>
        /// ��������� ������ ����� ��������
        /// </summary>
        /// <param name="cmd">SQL - �������</param>
        /// <param name="objProfile">�������</param>
        /// <returns>true - ������� ����������; false - ������</returns>
        private System.Boolean InitRouteDeclaration( System.Data.SqlClient.SqlCommand cmd, UniXP.Common.CProfile objProfile)
        {
            System.Boolean bRet = false;

            if( cmd == null ) { return bRet; }
            try
            {
                this.m_RoutePointList.Clear();
                // ���������� � �� ��������, ����������� ������� �� ������� ������
                cmd.Parameters.Clear();
                cmd.CommandText = System.String.Format( "[{0}].[dbo].[sp_GetBudgetDocRouteDeclaration]", objProfile.GetOptionsDllDBName() );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ( ( System.Byte )( 0 ) ), ( ( System.Byte )( 0 ) ), "", System.Data.DataRowVersion.Current, null ) );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@BUDGETDOC_GUID_ID", System.Data.SqlDbType.UniqueIdentifier ) );
                cmd.Parameters["@BUDGETDOC_GUID_ID"].Value = this.m_uuidBudgetDocID;
                System.Data.SqlClient.SqlDataReader rs = cmd.ExecuteReader();
                if( rs.HasRows )
                {
                    while( rs.Read() )
                    {
                        // ����������������� ������ ����� ��������
                        if( rs[ 0 ] == System.DBNull.Value )
                        {
                            // ��������� ��������� null
                            this.m_RoutePointList.Add( new CRoutePoint( rs.GetBoolean( 3 ), rs.GetBoolean( 4 ),
                                new CBudgetDocEvent( rs.GetGuid( 2 ), rs.GetString( 5 ), rs.GetInt32( 15 ), System.Convert.ToBoolean(rs["BUDGETDOCEVENT_SHOWMONEY"] ), System.Convert.ToBoolean(rs["BUDGETDOCEVENT_CANCHANGEMONEY"]) ),
                                new CBudgetDocState( rs.GetGuid( 1 ), rs.GetString( 7 ), rs.GetInt32( 17 ) ),
                                new CRoutePointGroup( rs.GetGuid( 8 ), rs.GetString( 9 ) ), rs.GetBoolean( 10 ),
                                new CUser( rs.GetInt32( 11 ), rs.GetInt32( 12 ), rs.GetString( 13 ), rs.GetString( 14 ) ) ) );
                        }
                        else
                        {
                            this.m_RoutePointList.Add( new CRoutePoint( rs.GetBoolean( 3 ), rs.GetBoolean( 4 ),
                                new CBudgetDocEvent( rs.GetGuid( 2 ), rs.GetString( 5 ), rs.GetInt32( 15 ), System.Convert.ToBoolean(rs["BUDGETDOCEVENT_SHOWMONEY"]), System.Convert.ToBoolean(rs["BUDGETDOCEVENT_CANCHANGEMONEY"]) ),
                                new CBudgetDocState( rs.GetGuid( 0 ), rs.GetString( 6 ), rs.GetInt32( 16 ) ),
                                new CBudgetDocState( rs.GetGuid( 1 ), rs.GetString( 7 ), rs.GetInt32( 17 ) ),
                                new CRoutePointGroup( rs.GetGuid( 8 ), rs.GetString( 9 ) ), rs.GetBoolean( 10 ),
                                new CUser( rs.GetInt32( 11 ), rs.GetInt32( 12 ), rs.GetString( 13 ), rs.GetString( 14 ) ) ) );
                        }

                    }

                    bRet = true;
                }
                else
                {
                    DevExpress.XtraEditors.XtraMessageBox.Show( 
                    "�� ������� �������� ������ ����� �������� ��� ���������� ���������.\n�� ���������� ��������� : " + 
                    this.m_uuidBudgetDocID.ToString() + "\n� �� �� ������� ����������.", "��������",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning );

                }
                rs.Dispose();
            }
            catch( System.Exception e )
            {
                DevExpress.XtraEditors.XtraMessageBox.Show( 
                "�� ������� �������� ������ ����� �������� ��� ���������� ���������.\n�� ���������� ��������� : " + 
                    this.m_uuidBudgetDocID.ToString() + "\n" + e.Message, "��������",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
            }
            finally // ������� ���������� �������
            {
            }
            return bRet;
        }
        /// <summary>
        /// ��������� ������ ����� �������� � ������ ����������
        /// </summary>
        /// <param name="objProfile">�������</param>
        /// <param name="uuidBudgetDocID">�� ���������� ���������</param>
        /// <param name="iOrderBy">��� ����������</param>
        /// <returns>true - ������� ����������; false - ������</returns>
        public System.Boolean LoadPointList(UniXP.Common.CProfile objProfile, System.Guid uuidBudgetDocID, System.Int32 iOrderBy )
        {
            System.Boolean bRet = false;

            System.Data.SqlClient.SqlConnection DBConnection = objProfile.GetDBSource();
            if (DBConnection == null) { return bRet; }

            try
            {
                this.m_RoutePointList.Clear();
                if (this.m_uuidBudgetDocID.CompareTo(System.Guid.Empty) == 0)
                {
                    this.m_uuidBudgetDocID = uuidID;
                }

                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
                cmd.Connection = DBConnection;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = System.String.Format("[{0}].[dbo].[sp_GetBudgetDocRouteDeclarationOrderBy]", objProfile.GetOptionsDllDBName());
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ((System.Byte)(0)), ((System.Byte)(0)), "", System.Data.DataRowVersion.Current, null));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@BUDGETDOC_GUID_ID", System.Data.SqlDbType.UniqueIdentifier));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ORDER_BY", System.Data.SqlDbType.Int));
                cmd.Parameters["@BUDGETDOC_GUID_ID"].Value = this.m_uuidBudgetDocID;
                cmd.Parameters["@ORDER_BY"].Value = iOrderBy;
                System.Data.SqlClient.SqlDataReader rs = cmd.ExecuteReader();
                if (rs.HasRows)
                {
                    while (rs.Read())
                    {
                        // ����������������� ������ ����� ��������
                        if (rs[0] == System.DBNull.Value)
                        {
                            // ��������� ��������� null
                            this.m_RoutePointList.Add(new CRoutePoint(rs.GetBoolean(3), rs.GetBoolean(4),
                                new CBudgetDocEvent(rs.GetGuid(2), rs.GetString(5), rs.GetInt32(15)),
                                new CBudgetDocState(rs.GetGuid(1), rs.GetString(7), rs.GetInt32(17)),
                                new CRoutePointGroup(rs.GetGuid(8), rs.GetString(9)), rs.GetBoolean(10),
                                new CUser(rs.GetInt32(11), rs.GetInt32(12), rs.GetString(13), rs.GetString(14))));
                        }
                        else
                        {
                            this.m_RoutePointList.Add(new CRoutePoint(rs.GetBoolean(3), rs.GetBoolean(4),
                                new CBudgetDocEvent(rs.GetGuid(2), rs.GetString(5), rs.GetInt32(15)),
                                new CBudgetDocState(rs.GetGuid(0), rs.GetString(6), rs.GetInt32(16)),
                                new CBudgetDocState(rs.GetGuid(1), rs.GetString(7), rs.GetInt32(17)),
                                new CRoutePointGroup(rs.GetGuid(8), rs.GetString(9)), rs.GetBoolean(10),
                                new CUser(rs.GetInt32(11), rs.GetInt32(12), rs.GetString(13), rs.GetString(14))));
                        }

                    }

                    bRet = true;
                }
                else
                {
                    DevExpress.XtraEditors.XtraMessageBox.Show(
                    "�� ������� �������� ������ ����� �������� ��� ���������� ���������.\n�� ���������� ��������� : " +
                    this.m_uuidBudgetDocID.ToString() + "\n� �� �� ������� ����������.", "��������",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);

                }
                rs.Close();
                rs.Dispose();
                cmd.Dispose();
            }
            catch (System.Exception f)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(
                "�� ������� ��������� ������ ����� �������� ��� ���������� ���������.\n\n����� ������: " + f.Message, "��������",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
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
        /// <param name="uuidID">���������� ������������� ���������� ���������</param>
        /// <returns>true - �������� �������������; false - ������</returns>
        public override System.Boolean Init( UniXP.Common.CProfile objProfile, System.Guid uuidID )
        {
            System.Boolean bRet = false;

            System.Data.SqlClient.SqlConnection DBConnection = objProfile.GetDBSource();
            if( DBConnection == null ) { return bRet; }

            try
            {
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
                cmd.Connection = DBConnection;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                if( this.m_uuidBudgetDocID.CompareTo( System.Guid.Empty ) == 0 )
                {
                    this.m_uuidBudgetDocID = uuidID;
                }
                // ��������� ������ ����� ��������
                bRet = InitRouteDeclaration( cmd, objProfile );
                cmd.Dispose();
            }
            catch( System.Exception e )
            {
                DevExpress.XtraEditors.XtraMessageBox.Show( 
                "�� ������� ������������������� ����� CBudgetRoute.\n����� ������: " + e.Message, "��������",
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
        /// <returns>true - ������� ����������; false - ������</returns>
        public override System.Boolean Remove( UniXP.Common.CProfile objProfile )
        {
            System.Boolean bRet = false;
            // ���������� ������������� ���������� ��������� �� ������ ���� ������
            if( this.m_uuidBudgetDocID.CompareTo( System.Guid.Empty ) == 0 )
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(  "������������ �������� ����������� �������������� ���������� ���������.\n�� : " + this.uuidID.ToString(), "��������",
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
                cmd.CommandText = System.String.Format( "[{0}].[dbo].[sp_DeleteBudgetDocRouteDeclaration]", objProfile.GetOptionsDllDBName() );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ( ( System.Byte )( 0 ) ), ( ( System.Byte )( 0 ) ), "", System.Data.DataRowVersion.Current, null ) );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@BUDGETDOC_GUID_ID", System.Data.SqlDbType.UniqueIdentifier ) );
                cmd.Parameters[ "@BUDGETDOC_GUID_ID" ].Value = this.m_uuidBudgetDocID;
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
                            DevExpress.XtraEditors.XtraMessageBox.Show(  "��������� �������� � �������� ��������������� �� ������.\n�� : " + 
                                this.m_uuidBudgetDocID.ToString(), "��������",
                                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning );
                            break;
                        }
                        default:
                        {
                            DevExpress.XtraEditors.XtraMessageBox.Show(  "������ ���������� ������� �� �������� �������� ���������� ���������.\n�� : " + 
                                this.m_uuidBudgetDocID.ToString(), "��������",
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
                "������ ���������� ������� �� �������� �������� ���������� ���������.\n�� : " + 
                this.m_uuidBudgetDocID.ToString() + "\n" + e.Message, "��������",
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
        /// �������� ���������� � �������� ���������� ��������� � ��
        /// </summary>
        /// <param name="objProfile">�������</param>
        /// <returns>true - ������� ����������; false - ������</returns>
        public override System.Boolean Add( UniXP.Common.CProfile objProfile )
        {
            System.Boolean bRet = false;

            // ���������� ������������� ���������� ��������� �� ������ ���� ������
            if( this.m_uuidBudgetDocID.CompareTo( System.Guid.Empty ) == 0 )
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(  "������������ �������� ����������� �������������� ���������� ���������.\n�� : " + 
                    this.uuidID.ToString(), "��������",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
                return bRet;
            }
            // �������� �� ������ �� ��������� �������
            if( this.m_RoutePointList.Count == 0 )
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(  "� �������� ����������� ���������!", "��������",
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

                cmd.Dispose();
            }
            catch( System.Exception e )
            {
                // ���������� ����������
                DBTransaction.Rollback();
                DevExpress.XtraEditors.XtraMessageBox.Show( 
                "�� ������� ������� ������� ��� ���������� ���������.\n�� ���������� ��������� : " + 
                this.m_uuidBudgetDocID.ToString() + "\n����� ������: " + e.Message, "��������",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
            }
			finally // ������� ���������� �������
            {
                DBConnection.Close();
            }
            return bRet;
        }

        /// <summary>
        /// �������� ���������� � �������� ���������� ��������� � ��
        /// </summary>
        /// <param name="objProfile">�������</param>
        /// <param name="cmd">SQL - �������</param>
        /// <returns>true - ������� ����������; false - ������</returns>
        public System.Boolean Add( UniXP.Common.CProfile objProfile,
            System.Data.SqlClient.SqlCommand cmd )
        {
            System.Boolean bRet = false;
            if( cmd == null ) { return bRet; }

            // ���������� ������������� ���������� ��������� �� ������ ���� ������
            if( this.m_uuidBudgetDocID.CompareTo( System.Guid.Empty ) == 0 )
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(  "������������ �������� ����������� �������������� ���������� ���������.\n�� : " + 
                    this.uuidID.ToString(), "��������",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
                return bRet;
            }
            // �������� �� ������ �� ��������� �������
            if( this.m_RoutePointList.Count == 0 )
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(  "� �������� ����������� ���������!", "��������",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
                return bRet;
            }

            try
            {
                // ��������� ��������� ������� ��������
                bRet = this.SaveRouteDeclaration( cmd, objProfile );
            }
            catch( System.Exception e )
            {
                DevExpress.XtraEditors.XtraMessageBox.Show( 
                "�� ������� ������� ������� ��� ���������� ���������.\n�� ���������� ��������� : " + 
                this.m_uuidBudgetDocID.ToString() + "\n����� ������: " + e.Message, "��������",
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

            // ���������� ������������� ���������� ��������� �� ������ ���� ������
            if( this.m_uuidBudgetDocID.CompareTo( System.Guid.Empty ) == 0 )
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(  "������������ �������� ����������� �������������� ���������� ���������.\n�� : " + 
                    this.m_uuidBudgetDocID.ToString(), "��������",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
                return bRet;
            }
            // �������� �� ������ �� ��������� �������
            if( this.m_RoutePointList.Count == 0 )
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(  "� �������� ����������� ���������!", "��������",
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
                cmd.Dispose();
            }
            catch( System.Exception e )
            {
                // ���������� ����������
                DBTransaction.Rollback();
                DevExpress.XtraEditors.XtraMessageBox.Show( 
                "������ ��������� ������� �������� ���������� ���������.\n�� ���������� ���������: " + 
                this.m_uuidBudgetDocID.ToString() + "\n����� ������: " + e.Message, "��������",
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
        /// <param name="cmd">SQL - �������</param>
        /// <returns>true - ������� ����������; false - ������</returns>
        public System.Boolean Update( UniXP.Common.CProfile objProfile,
            System.Data.SqlClient.SqlCommand cmd )
        {
            System.Boolean bRet = false;
            if( cmd == null ) { return bRet; }

            // ���������� ������������� ���������� ��������� �� ������ ���� ������
            if( this.m_uuidBudgetDocID.CompareTo( System.Guid.Empty ) == 0 )
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(  "������������ �������� ����������� �������������� ���������� ���������.\n�� : " + 
                    this.m_uuidBudgetDocID.ToString(), "��������",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
                return bRet;
            }
            // �������� �� ������ �� ��������� �������
            if( this.m_RoutePointList.Count == 0 )
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(  "� �������� ����������� ���������!", "��������",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
                return bRet;
            }

            try
            {
                // ������� ��������� �������
                bRet = this.DeleteRouteDeclaration( cmd, objProfile );
                if( bRet )
                {
                    // ��������� ��������� ������� ��������
                    bRet = this.SaveRouteDeclaration( cmd, objProfile );
                }
            }
            catch( System.Exception e )
            {
                // ���������� ����������
                DevExpress.XtraEditors.XtraMessageBox.Show( 
                "������ ��������� ������� �������� ���������� ���������.\n�� ���������� ���������: " + 
                this.m_uuidBudgetDocID.ToString() + "\n����� ������: " + e.Message, "��������",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
            }
			finally // ������� ���������� �������
            {
            }
            return bRet;
        }

        /// <summary>
        /// ������� ������� ���������� ���������
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
            // ���������� ������������� ���������� ��������� �� ������ ���� ������
            if( this.m_uuidBudgetDocID.CompareTo( System.Guid.Empty ) == 0 )
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(  "������������ �������� ����������� �������������� ���������� ���������.\n�� : " + 
                    this.m_uuidBudgetDocID.ToString(), "��������",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
                return bRet;
            }

            try
            {
                cmd.Parameters.Clear();
                cmd.CommandText = System.String.Format( "[{0}].[dbo].[sp_DeleteBudgetDocRouteDeclaration]", objProfile.GetOptionsDllDBName() );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ( ( System.Byte )( 0 ) ), ( ( System.Byte )( 0 ) ), "", System.Data.DataRowVersion.Current, null ) );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@BUDGETDOC_GUID_ID", System.Data.SqlDbType.UniqueIdentifier ) );

                cmd.Parameters[ "@BUDGETDOC_GUID_ID" ].Value = this.m_uuidBudgetDocID;
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
                            DevExpress.XtraEditors.XtraMessageBox.Show(  "��������� �������� � �������� ��������������� �� ������\n�� ���������� ��������� : " + 
                                this.m_uuidBudgetDocID.ToString(), "��������",
                                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
                            break;
                        }
                        default:
                        {
                            DevExpress.XtraEditors.XtraMessageBox.Show(  "������ �������� ��������� ��������.\n�� ���������� ��������� : " + 
                                this.m_uuidBudgetDocID.ToString(), "��������",
                                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
                            break;
                        }
                    }
                }
            }
            catch( System.Exception e )
            {
                DevExpress.XtraEditors.XtraMessageBox.Show( 
                "������ �������� ��������� ��������.\n�� ���������� ��������� : " + 
                this.m_uuidBudgetDocID.ToString() + "\n����� ������: " + e.Message, "��������",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
            }
			finally // ������� ���������� �������
            {
            }
            return bRet;
        }
        /// <summary>
        /// ��������� � �� ��������� �������� ���������� ���������
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

            // ���������� ������������� ���������� ��������� �� ������ ���� ������
            if( this.m_uuidBudgetDocID == System.Guid.Empty )
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(  "������������ �������� ����������� �������������� ���������� ���������", "��������",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
                return bRet;
            }

            try
            {
                // ��������� �� ��������� �������� � ��������� �� � �� 
                bRet = true;
                for( System.Int32 i = 0; i < this.m_RoutePointList.Count; i++ )
                {
                    if( this.m_RoutePointList[ i ].AddBudgetDocRouteItem( objProfile, this.m_uuidBudgetDocID, cmd ) == false )
                    {
                        bRet = false;
                        break;
                    }
                }
            }
            catch( System.Exception e )
            {
                DevExpress.XtraEditors.XtraMessageBox.Show( 
                "�� ������� ��������� ������ ��������.\n����� ������: " + e.Message, "��������",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
            }
			finally // ������� ���������� �������
            {
            }
            return bRet;
        }


        #endregion
    }
}
