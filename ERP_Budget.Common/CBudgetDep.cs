using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace ERP_Budget.Common
{
    /// <summary>
    /// ����� "��������� �������������"
    /// </summary>
    public class CBudgetDep : IBaseListItem
    {
        #region ����������, ��������, ���������
        /// <summary>
        /// ������������ ��������� �������������
        /// </summary>
        private System.Guid m_uuidParentID;
        /// <summary>
        /// ������������� �������
        /// </summary>
        private CUser m_Manager;
        /// <summary>
        /// ������ �������������
        /// </summary>
        private CBaseList<CUser> m_UsesrList;

        /// <summary>
        /// ������������ ��������� �������������
        /// </summary>
        public System.Guid ParentID
        {
            get { return m_uuidParentID; }
            set { m_uuidParentID = value; }
        }

        /// <summary>
        /// ������������� �������
        /// </summary>
        public ERP_Budget.Common.CUser Manager
        {
            get { return m_Manager; }
            set { m_Manager = value; }
        }

        /// <summary>
        /// ������ �������������
        /// </summary>
        public ERP_Budget.Common.CBaseList<CUser> UsesrList
        {
            get { return m_UsesrList; }
            set { m_UsesrList = value; }
        }
        /// <summary>
        /// ������� "������ ��� ������"
        /// </summary>
        System.Boolean m_bReadOnly;
        /// <summary>
        /// �������� "������ ��� ������"
        /// </summary>
        public System.Boolean ReadOnly
        {
            get { return m_bReadOnly; }
            set { m_bReadOnly = value; }
        }
        /// <summary>
        /// ��������� �������
        /// </summary>
        private System.Data.DataRowState m_State;
        /// <summary>
        /// ��������� �������
        /// </summary>
        public System.Data.DataRowState State
        {
            get { return m_State; }
            set { m_State = value; }
        }
        /// <summary>
        /// ������� "������� �������� �������������"
        /// </summary>
        private System.Boolean m_bHasChildren;
        /// <summary>
        /// ������� "������� �������� �������������"
        /// </summary>
        public System.Boolean HasChildren
        {
            get { return m_bHasChildren; }
        }
        /// <summary>
        /// ������� "������� �������"
        /// </summary>
        private System.Boolean m_bHasBudget;
        /// <summary>
        /// ������� "������� �������"
        /// </summary>
        public System.Boolean HasBudget
        {
            get { return m_bHasBudget; }
        }
        /// <summary>
        /// ������ �������������� �������������� ������� ������
        /// </summary>
        private List<CUser> m_objAdditionalManagerList;
        /// <summary>
        /// ������ �������������� �������������� ������� ������
        /// </summary>
        public List<CUser> AdditionalManagerList
        {
            get { return m_objAdditionalManagerList; }
            set { m_objAdditionalManagerList = value; }
        }
        /// <summary>
        /// ������� "��� ��������� ��������" ������������ ������ ��� �������� "������ - ������ - ��� �������� - ������"
        /// </summary>
        private CBudgetExpenseType m_objBudgetExpenseType;
        /// <summary>
        /// ������� "��� ��������� ��������" ������������ ������ ��� �������� "������ - ������ - ��� �������� - ������"
        /// </summary>
        public CBudgetExpenseType BudgetExpenseType
        {
            get { return m_objBudgetExpenseType; }
            set { m_objBudgetExpenseType = value; }
        }
        /// <summary>
        /// ������� "������", ������������ ������ ��� �������� "������ - ������ - ��� �������� - ������"
        /// </summary>
        public CBudgetProject BudgetProject { get; set; }

    #endregion

        #region ������������ 
        public CBudgetDep()
        {
            this.m_uuidID = System.Guid.Empty;
            this.m_strName = "";
            this.m_Manager = null;
            this.m_uuidParentID = System.Guid.Empty;
            this.m_UsesrList = null;
            this.m_bReadOnly = false;
            this.m_UsesrList = new CBaseList<CUser>();
            this.m_objAdditionalManagerList = new List<CUser>();
            this.m_UsesrList.ClearList();
            this.m_bHasChildren = false;
            this.m_bHasBudget = false;
            m_objBudgetExpenseType = null;
            BudgetProject = null;
        }

        public CBudgetDep( System.Guid uuidID )
        {
            this.m_uuidID = uuidID;
            this.m_strName = "";
            this.m_Manager = null;
            this.m_uuidParentID = System.Guid.Empty;
            this.m_UsesrList = null;
            this.m_bReadOnly = false;
            this.m_UsesrList = new CBaseList<CUser>();
            this.m_objAdditionalManagerList = new List<CUser>();
            this.m_UsesrList.ClearList();
            this.m_bHasChildren = false;
            this.m_bHasBudget = false;
            m_objBudgetExpenseType = null;
            BudgetProject = null;
        }

        public CBudgetDep( System.Guid uuidID, System.String strName )
        {
            this.m_uuidID = uuidID;
            this.m_strName = strName;
            this.m_Manager = null;
            this.m_uuidParentID = System.Guid.Empty;
            this.m_UsesrList = null;
            this.m_bReadOnly = false;
            this.m_UsesrList = new CBaseList<CUser>();
            this.m_objAdditionalManagerList = new List<CUser>();
            this.m_UsesrList.ClearList();
            this.m_bHasChildren = false;
            this.m_bHasBudget = false;
            m_objBudgetExpenseType = null;
            BudgetProject = null;
        }
        public CBudgetDep( System.Guid uuidID, System.String strName, CUser objManager )
        {
            this.m_uuidID = uuidID;
            this.m_strName = strName;
            this.m_Manager = objManager;
            this.m_uuidParentID = System.Guid.Empty;
            this.m_UsesrList = null;
            this.m_bReadOnly = false;
            this.m_UsesrList = new CBaseList<CUser>();
            this.m_objAdditionalManagerList = new List<CUser>();
            this.m_UsesrList.ClearList();
            this.m_bHasChildren = false;
            this.m_bHasBudget = false;
            m_objBudgetExpenseType = null;
            BudgetProject = null;
        }
        #endregion 

        #region ������ ��������� ������������� 
        /// <summary>
        /// ���������� ������ ��������� �������������
        /// </summary>
        /// <param name="objProfile">�������</param>
        /// <param name="bInitUserList">������� ����, ����� �� ����������� ������ ���������� �������������</param>
        /// <returns>������ ��������� �������������</returns>
        public static List<CBudgetDep> GetBudgetDepsList( UniXP.Common.CProfile objProfile, System.Boolean bInitUserList )
        {
            List<CBudgetDep> objList = new List<CBudgetDep>();

            System.Data.SqlClient.SqlConnection DBConnection = objProfile.GetDBSource();
            if( DBConnection == null ) { return objList; }

            try
            {
                // ���������� � �� ��������, ����������� ������� �� ������� ������
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
                cmd.Connection = DBConnection;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = System.String.Format( "[{0}].[dbo].[sp_GetBudgetDep]", objProfile.GetOptionsDllDBName() );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ( ( System.Byte )( 0 ) ), ( ( System.Byte )( 0 ) ), "", System.Data.DataRowVersion.Current, null ) );
                System.Data.SqlClient.SqlDataReader rs = cmd.ExecuteReader();
                if( rs.HasRows )
                {
                    // ����� ������ ��������
                    CBudgetDep objBudgetDep = null;
                    while (rs.Read())
                    {
                        objBudgetDep = new CBudgetDep();
                        objBudgetDep.m_uuidID = (System.Guid)rs["GUID_ID"];
                        objBudgetDep.m_strName = (System.String)rs["BUDGETDEP_NAME"];

                        if (rs["PARENT_GUID_ID"] != System.DBNull.Value) { objBudgetDep.m_uuidParentID = (System.Guid)rs["PARENT_GUID_ID"]; }
                        
                        objBudgetDep.m_Manager = new CUser();
                        objBudgetDep.m_Manager.IsBlocked = System.Convert.ToBoolean(rs["IsUserBlocked"]);
                        objBudgetDep.m_Manager.Init(objProfile, (System.Int32)rs["BUDGETDEP_MANAGER"]);

                        objBudgetDep.m_bReadOnly = (System.Boolean)rs["ISREADONLY"];
                        objBudgetDep.m_bHasChildren = (System.Boolean)rs["HASCHILDREN"];
                        //if (rs["PARENT_GUID_ID"] != System.DBNull.Value)
                        //{
                        //    objBudgetDep.ParentID = (System.Guid)rs["PARENT_GUID_ID"];
                        //}
                        objList.Add(objBudgetDep);
                    }
                }
                rs.Close();
                rs.Dispose();

                if (bInitUserList == true)
                {
                    // ������ ������
                    foreach (CBudgetDep objBudgetDepItem in objList)
                    {
                        objBudgetDepItem.m_UsesrList = objBudgetDepItem.GetUserList(objProfile, objBudgetDepItem.m_uuidID);
                    }
                }

                // 20140313 �������� ���������
                // ��������� ���������� ������ �������������� ������
                //foreach (CBudgetDep objBudgetDepItem in objList)
                //{
                //    objBudgetDepItem.LoadAdditionalManagerList(objProfile, cmd);
                //}

                cmd.Dispose();
            }
            catch( System.Exception f )
            {
                DevExpress.XtraEditors.XtraMessageBox.Show( 
                "�� ������� �������� ������ ��������� �������������.\n\n����� ������: " + f.Message, "��������",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
            }
            finally // ������� ���������� �������
            {
                DBConnection.Close();
            }

            return objList;
        }
        /// <summary>
        /// ���������� ������ ��������� ������������� ��� �������� ������������� ������������� � ������� �������������
        /// </summary>
        /// <param name="objProfile">�������</param>
        /// <returns>������ ��������� �������������</returns>
        public static List<CBudgetDep> GetBudgetDepartmentListWhitoutManager(UniXP.Common.CProfile objProfile, ref System.String strErr)
        {
            List<CBudgetDep> objList = new List<CBudgetDep>();

            System.Data.SqlClient.SqlConnection DBConnection = objProfile.GetDBSource();
            if (DBConnection == null) { return objList; }

            try
            {
                // ���������� � �� ��������, ����������� ������� �� ������� ������
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
                cmd.Connection = DBConnection;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = System.String.Format("[{0}].[dbo].[sp_GetBudgetDep2]", objProfile.GetOptionsDllDBName());
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ((System.Byte)(0)), ((System.Byte)(0)), "", System.Data.DataRowVersion.Current, null));
                System.Data.SqlClient.SqlDataReader rs = cmd.ExecuteReader();
                if (rs.HasRows)
                {
                    // ����� ������ ��������
                    while (rs.Read())
                    {
                        objList.Add(new CBudgetDep()
                        {
                            m_uuidID = (System.Guid)rs["GUID_ID"],
                            Name = (System.String)rs["BUDGETDEP_NAME"],
                            //ParentID = ((rs["PARENT_GUID_ID"] != System.DBNull.Value) ? (System.Guid)rs["PARENT_GUID_ID"] : System.Guid.Empty),
                            Manager = new CUser(),
                            ReadOnly = true,
                            m_bHasChildren = System.Convert.ToBoolean( rs["HASCHILDREN"] )
                        }
                        );
                    }

                }
                rs.Close();
                rs.Dispose();

                cmd.Dispose();
            }
            catch (System.Exception f)
            {
                strErr += ("�� ������� �������� ������ ��������� �������������.\n\n����� ������: " + f.Message);
            }
            finally // ������� ���������� �������
            {
                DBConnection.Close();
            }

            return objList;
        }
        /// <summary>
        /// ���������� ������ ��������� �������������
        /// </summary>
        /// <param name="objProfile">�������</param>
        /// <returns>������ ��������� �������������</returns>
        public static List<CBudgetDep> GetBudgetDepsListForBudgetDoc( UniXP.Common.CProfile objProfile,
            System.Boolean bInitManager = true, System.Boolean bLoadAdditionalManagerList = true )
        {
            List<CBudgetDep> objList = new List<CBudgetDep>();

            System.Data.SqlClient.SqlConnection DBConnection = objProfile.GetDBSource();
            if (DBConnection == null) { return objList; }

            try
            {
                // ���������� � �� ��������, ����������� ������� �� ������� ������
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
                cmd.Connection = DBConnection;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = System.String.Format("[{0}].[dbo].[sp_GetBudgetDepForBudgetDoc]", objProfile.GetOptionsDllDBName());
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ((System.Byte)(0)), ((System.Byte)(0)), "", System.Data.DataRowVersion.Current, null));
                System.Data.SqlClient.SqlDataReader rs = cmd.ExecuteReader();
                if (rs.HasRows)
                {
                    // ����� ������ ��������
                    CBudgetDep objBudgetDep = null;
                    while (rs.Read())
                    {
                        objBudgetDep = new CBudgetDep();
                        objBudgetDep.m_uuidID = (System.Guid)rs["GUID_ID"];
                        objBudgetDep.m_strName = (System.String)rs["BUDGETDEP_NAME"];
                        if (rs["PARENT_GUID_ID"] != System.DBNull.Value) { objBudgetDep.m_uuidParentID = (System.Guid)rs["PARENT_GUID_ID"]; }
                        objBudgetDep.m_Manager = new CUser();
                        objBudgetDep.m_Manager.ulID = (System.Int32)rs["BUDGETDEP_MANAGER"];
                        objBudgetDep.m_Manager.ulUniXPID = (System.Int32)rs["ulUniXPID"];

                        if( bInitManager == true )
                        {
                            objBudgetDep.m_Manager.Init(objProfile, (System.Int32)rs["BUDGETDEP_MANAGER"]);
                        }

                        objList.Add(objBudgetDep);
                    }
                }
                rs.Close();
                rs.Dispose();

                if (bLoadAdditionalManagerList == true)
                {
                    // ������ �������������� �������������� ������
                    foreach (CBudgetDep objBudgetDepItem in objList)
                    {
                        objBudgetDepItem.LoadAdditionalManagerList(objProfile, cmd);
                    }
                }

                cmd.Dispose();
            }
            catch (System.Exception f)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(
                "�� ������� �������� ������ ��������� �������������.\n\n����� ������: " + f.Message, "��������",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
            finally // ������� ���������� �������
            {
                DBConnection.Close();
            }

            return objList;
        }
        /// <summary>
        /// ��������� ������ ��������� �������������
        /// </summary>
        /// <param name="objProfile">�������</param>
        /// <param name="cmd">SQL - �������</param>
        /// <param name="cmd">objList - ����������� ������</param>
        /// <returns></returns>
        public static void RefreshBudgetDepList( UniXP.Common.CProfile objProfile,
            System.Data.SqlClient.SqlCommand cmd, System.Collections.Generic.List<CBudgetDep> objList )
        {
            if( cmd == null ) { return; }
            objList.Clear();

            try
            {
                cmd.Parameters.Clear();
                cmd.CommandText = System.String.Format( "[{0}].[dbo].[sp_GetBudgetDepForBudgetDoc]", objProfile.GetOptionsDllDBName() );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ( ( System.Byte )( 0 ) ), ( ( System.Byte )( 0 ) ), "", System.Data.DataRowVersion.Current, null ) );
                System.Data.SqlClient.SqlDataReader rs = cmd.ExecuteReader();
                if( rs.HasRows )
                {
                    // ����� ������ ��������
                    while( rs.Read() )
                    {
                        CUser objManager = new CUser();
                        objManager.ulID = ( System.Int32 )rs[ "BUDGETDEP_MANAGER" ];
                        objManager.ulUniXPID = ( System.Int32 )rs[ "ulUniXPID" ];
                        objList.Add( new CBudgetDep( ( System.Guid )rs[ "GUID_ID" ], ( System.String )rs[ "BUDGETDEP_NAME" ], objManager ) );
                    }
                }
                rs.Close();
                rs.Dispose();

                // ��������� ���������� ������ �������������� ������
                foreach( CBudgetDep objBudgetDep in objList )
                {
                    objBudgetDep.LoadAdditionalManagerList( objProfile, cmd );
                }
            }
            catch( System.Exception e )
            {
                DevExpress.XtraEditors.XtraMessageBox.Show( 
                "�� ������� �������� ������ ��������� �������������.\n����� ������: " + e.Message, "��������",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
            }
            finally // ������� ���������� �������
            {
            }
            return ;
        }
        /// <summary>
        /// ��������� ������ �������������� �������������� �������
        /// </summary>
        /// <param name="objProfile">�������</param>
        /// <param name="cmd">SQL-�������</param>
        public void LoadAdditionalManagerList( UniXP.Common.CProfile objProfile, System.Data.SqlClient.SqlCommand cmd )
        {
            this.m_objAdditionalManagerList.Clear();
            if( cmd == null ) { return; }

            try
            {
                // ���������� � �� ��������, ����������� ������� �� ������� ������
                cmd.Parameters.Clear();
                cmd.CommandText = System.String.Format( "[{0}].[dbo].[sp_GetBudgetDepManagerList]", objProfile.GetOptionsDllDBName() );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ( ( System.Byte )( 0 ) ), ( ( System.Byte )( 0 ) ), "", System.Data.DataRowVersion.Current, null ) );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@BUDGETDEP_GUID_ID", System.Data.SqlDbType.UniqueIdentifier, 4 ) );
                cmd.Parameters[ "@BUDGETDEP_GUID_ID" ].Value = this.uuidID;
                System.Data.SqlClient.SqlDataReader rs = cmd.ExecuteReader();
                if( rs.HasRows )
                {
                    while( rs.Read() )
                    {
                        this.m_objAdditionalManagerList.Add( new CUser( ( System.Int32 )rs[ "ErpBudgetUserID" ], ( System.Int32 )rs[ "UniXPUserID" ],
                            ( System.String )rs[ "strLastName" ], ( System.String )rs[ "strFirstName" ] ) );
                    }
                }
                rs.Close();
                rs.Dispose();
            }
            catch( System.Exception e )
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(
                "�� ������� �������� ������ �������������� \n��� �������� ������.\n����� ������: " + e.Message, "��������",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
            }
            finally // ������� ���������� �������
            {
            }
            return;
        }
        /// <summary>
        /// ����������, �������� �� �������� ������������ �������������� ������
        /// </summary>
        /// <param name="objUser">������������</param>
        /// <returns>true - ��������; false - ���</returns>
        public System.Boolean IsBudgetDepManager( CUser objUser )
        {
            System.Boolean bRet = false;
            if( objUser == null ) { return bRet; }

            try
            {
                if( this.m_Manager != null )
                {
                    bRet = ( this.m_Manager.ulID == objUser.ulID );
                }
                if( bRet == false )
                {
                    // ������� � �������������� ��������������
                    if( ( this.m_objAdditionalManagerList != null ) && ( this.m_objAdditionalManagerList.Count > 0 ) )
                    {
                        foreach( CUser objUserItem in this.m_objAdditionalManagerList )
                        {
                            if( objUserItem.ulID == objUser.ulID )
                            {
                                bRet = true;
                                break;
                            }
                        }
                    }
                }
            }
            catch( System.Exception e )
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(
                "�� ������� ����������, �������� �� ������������ �������������� ������.\n����� ������: " + e.Message, "��������",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
            }
            finally // ������� ���������� �������
            {
            }
            return bRet;
        }
        /// <summary>
        /// ����������, �������� �� �������� ������������ �������������� ������
        /// </summary>
        /// <param name="SQLUserID">�� ������������ � �� UniXP</param>
        /// <returns>true - ��������; false - ���</returns>
        public System.Boolean IsBudgetDepManager( System.Int32 SQLUserID )
        {
            System.Boolean bRet = false;

            try
            {
                if( this.m_Manager != null )
                {
                    bRet = ( this.m_Manager.ulUniXPID == SQLUserID );
                }
                if( bRet == false )
                {
                    // ������� � �������������� ��������������
                    if( ( this.m_objAdditionalManagerList != null ) && ( this.m_objAdditionalManagerList.Count > 0 ) )
                    {
                        foreach( CUser objUserItem in this.m_objAdditionalManagerList )
                        {
                            if( objUserItem.ulUniXPID == SQLUserID )
                            {
                                bRet = true;
                                break;
                            }
                        }
                    }
                }
            }
            catch( System.Exception e )
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(
                "�� ������� ����������, �������� �� ������������ �������������� ������.\n����� ������: " + e.Message, "��������",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
            }
            finally // ������� ���������� �������
            {
            }
            return bRet;
        }

        /// <summary>
        /// ���������� ������ ��������� ������������� ��� �������� ������ ��������
        /// </summary>
        /// <param name="objProfile">�������</param>
        /// <param name="uuidDebitArticleID">������������ ������ ��������</param>
        /// <returns>������ ��������� �������������</returns>
        public static List<CBudgetDep> GetBudgetDepListForDebitArticle( UniXP.Common.CProfile objProfile, 
            System.Guid uuidDebitArticleID )
        {
            List<CBudgetDep> objList = new List<CBudgetDep>();

            System.Data.SqlClient.SqlConnection DBConnection = objProfile.GetDBSource();
            if( DBConnection == null ) { return objList; }

            try
            {
                // ���������� � �� ��������, ����������� ������� �� ������� ������
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand() { Connection = DBConnection, 
                    CommandType = System.Data.CommandType.StoredProcedure, CommandText = System.String.Format("[{0}].[dbo].[sp_GetDebitArticleBudgetDep]", 
                    objProfile.GetOptionsDllDBName()) };
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ( ( System.Byte )( 0 ) ), ( ( System.Byte )( 0 ) ), "", System.Data.DataRowVersion.Current, null ) );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@DEBITARTICLE_GUID_ID", System.Data.SqlDbType.UniqueIdentifier, 4 ) );
                cmd.Parameters[ "@DEBITARTICLE_GUID_ID" ].Value = uuidDebitArticleID;
                System.Data.SqlClient.SqlDataReader rs = cmd.ExecuteReader();
                if( rs.HasRows )
                {
                    // ����� ������ ��������
                    CBudgetDep objBudgetDep = null;
                    while( rs.Read() )
                    {
                        objBudgetDep = new CBudgetDep()
                        {
                            uuidID = (System.Guid)rs["BUDGETDEP_GUID_ID"],
                            Name = System.Convert.ToString(rs["BUDGETDEP_NAME"]),
                            ParentID = ((rs["PARENT_GUID_ID"] != System.DBNull.Value) ? (System.Guid)rs["PARENT_GUID_ID"] : System.Guid.Empty),
                            Manager = new CUser()
                        };

                        objBudgetDep.Manager.Init(objProfile, System.Convert.ToInt32(rs["BUDGETDEP_MANAGER"]));

                        objList.Add( objBudgetDep );
                    }
                }
                cmd.Dispose();
                rs.Dispose();
            }
            catch( System.Exception e )
            {
                DevExpress.XtraEditors.XtraMessageBox.Show( 
                "�� ������� �������� ������ ��������� ������������� \n��� �������� ������ ��������.\n" + e.Message, "��������",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
            }
            finally // ������� ���������� �������
            {
                DBConnection.Close();
            }
            return objList;
        }
        /// <summary>
        /// ���������� ������ ��������� ������������� ��� �������� ������ ��������
        /// </summary>
        /// <param name="objProfile">�������</param>
        /// <param name="objDebitArticle">������ ��������</param>
        /// <param name="cmd">SQL - �������</param>
        /// <returns>������ ��������� �������������</returns>
        public static void RefreshBudgetDepListForDebitArticle( UniXP.Common.CProfile objProfile,
            CDebitArticle objDebitArticle, System.Data.SqlClient.SqlCommand cmd )
        {
            objDebitArticle.BudgetDepList.Clear();
            if( cmd == null ) { return; }

            try
            {
                // ���������� � �� ��������, ����������� ������� �� ������� ������
                cmd.Parameters.Clear();
                cmd.CommandText = System.String.Format( "[{0}].[dbo].[sp_GetDebitArticleBudgetDep]", objProfile.GetOptionsDllDBName() );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ( ( System.Byte )( 0 ) ), ( ( System.Byte )( 0 ) ), "", System.Data.DataRowVersion.Current, null ) );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@DEBITARTICLE_GUID_ID", System.Data.SqlDbType.UniqueIdentifier, 4 ) );
                cmd.Parameters[ "@DEBITARTICLE_GUID_ID" ].Value = objDebitArticle.uuidID;
                System.Data.SqlClient.SqlDataReader rs = cmd.ExecuteReader();
                if( rs.HasRows )
                {
                    while( rs.Read() )
                    {
                        objDebitArticle.BudgetDepList.Add( new CBudgetDep( rs.GetGuid( 0 ), rs.GetString( 2 ) ) );
                    }
                }
                rs.Close();
                rs.Dispose();
            }
            catch( System.Exception e )
            {
                DevExpress.XtraEditors.XtraMessageBox.Show( 
                "�� ������� �������� ������ ��������� ������������� \n��� �������� ������ ��������.\n" + e.Message, "��������",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
            }
            finally // ������� ���������� �������
            {
            }
            return ;
        }
        /// <summary>
        /// ���������� ������ ���������� �������������
        /// </summary>
        /// <param name="objProfile">�������</param>
        /// <param name="uuidBudgetDepID">���������� ������������� ���������� �������������</param>
        /// <returns>������ �������������, �������� � ��������� �������������</returns>
        private CBaseList<CUser> GetUserList( UniXP.Common.CProfile objProfile, System.Guid uuidBudgetDepID )
        {
            CBaseList<CUser> objList = new CBaseList<CUser>();

            System.Data.SqlClient.SqlConnection DBConnection = objProfile.GetDBSource();
            if( DBConnection == null ) { return objList; }

            try
            {
                // ���������� � �� ��������, ����������� ������� �� ������� ������
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
                cmd.Connection = DBConnection;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = System.String.Format( "[{0}].[dbo].[sp_GetBudgetDepDeclaration]", objProfile.GetOptionsDllDBName() );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ( ( System.Byte )( 0 ) ), ( ( System.Byte )( 0 ) ), "", System.Data.DataRowVersion.Current, null ) );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@BUDGETDEP_GUID_ID", System.Data.SqlDbType.UniqueIdentifier ) );
                cmd.Parameters[ "@BUDGETDEP_GUID_ID" ].Value = uuidBudgetDepID;
                System.Data.SqlClient.SqlDataReader rs = cmd.ExecuteReader();
                if( rs.HasRows )
                {
                    CUser objUser = null;
                    while( rs.Read() )
                    {
                        objUser = new CUser();

                        objUser.UserLastName = System.Convert.ToString(rs["strLastName"]);
                        objUser.UserMiddleName = System.Convert.ToString(rs["strMiddleName"]);
                        objUser.UserFirstName = System.Convert.ToString(rs["strFirstName"]);
                        objUser.ulID = System.Convert.ToInt32(rs["ulUserID"]);
                        objUser.ulUniXPID = System.Convert.ToInt32(rs["UniXPUserID"]);
                        
                        ////objUser.Init( objProfile, rs.GetInt32( 1 ) );
                        
                        objList.AddItemToList( objUser );
                    }
                }
                cmd.Dispose();
                rs.Dispose();
            }
            catch( System.Exception e )
            {
                DevExpress.XtraEditors.XtraMessageBox.Show( 
                "�� ������� �������� ������ ���������� �������������.\n" + e.Message, "��������",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
            }
            finally // ������� ���������� �������
            {
                DBConnection.Close();
            }
            return objList;
        }

        public static System.Windows.Forms.TreeView GetBudgetTreeView(UniXP.Common.CProfile objProfile,
            UniXP.Common.MENUITEM objMenuItem, System.Boolean bSuperVisor, System.Boolean bInspector, System.Boolean bManager)
        {
            System.Windows.Forms.TreeView objTreeView = new System.Windows.Forms.TreeView();
            System.Data.SqlClient.SqlConnection DBConnection = objProfile.GetDBSource();
            if (DBConnection == null) { return objTreeView; }

            try
            {
                UniXP.Common.MENUITEM objMenuNode = null;
                UniXP.Common.MENUITEM objMenuNodeChild = null;

                // ����������� ������ ��������
                List<ERP_Budget.Common.CBudget> objBudgetList = ERP_Budget.Common.CBudget.GetBudgetList(objProfile);
                if ((objBudgetList == null) || (objBudgetList.Count == 0)) { return objTreeView; }

                // ���������� � �� ��������, ����������� ������� �� ������� ������
                List<System.Int32> YearList = new List<int>();
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
                cmd.Connection = DBConnection;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = System.String.Format("[{0}].[dbo].[sp_GetYearsForBudget]", objProfile.GetOptionsDllDBName());
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ((System.Byte)(0)), ((System.Byte)(0)), "", System.Data.DataRowVersion.Current, null));
                System.Data.SqlClient.SqlDataReader rs = cmd.ExecuteReader();
                if (rs.HasRows)
                {
                    // ����� ������ �������� - � ��� ���� ������ �����, ��� ������� ���� ������� �������
                    while (rs.Read())
                    {
                        // ��������� ������ � ������ ���������� �������������
                        objMenuNode = new UniXP.Common.MENUITEM();
                        objMenuNode.enMenuType = objMenuItem.enMenuType;
                        objMenuNode.strName = ( (System.Int32)rs["BudgetYear"] ).ToString();
                        objMenuNode.lClassID = 1;
                        objMenuNode.uuidFarClient = objMenuItem.uuidFarClient;
                        objMenuNode.strDescription = "";
                        objMenuNode.nImage = objMenuItem.nImage;
                        objMenuNode.strDLLName = objMenuItem.strDLLName;
                        objMenuNode.enCmdType = objMenuItem.enCmdType;
                        objMenuNode.enDLLType = objMenuItem.enDLLType;
                        objMenuNode.objProfile = objMenuItem.objProfile;
                        objMenuNode.objAdvancedParametr = null;

                        System.Windows.Forms.TreeNode objMenuTreeNode = new System.Windows.Forms.TreeNode();
                        objMenuTreeNode.Text = objMenuNode.strName;
                        objMenuTreeNode.ImageIndex = objMenuItem.nImage;
                        objMenuTreeNode.SelectedImageIndex = objMenuItem.nImage;
                        objMenuTreeNode.Tag = objMenuNode;
                        objTreeView.Nodes.Add(objMenuTreeNode);
                    }
                }

                if (objTreeView.Nodes.Count > 0)
                {
                    // �� ��������� ������ � ������, ������ ����� ��������� ������ � ���������� ���������������                    
                    foreach (System.Windows.Forms.TreeNode objNodeYear in objTreeView.Nodes)
                    {
                        List<CBudgetDep> ParentBudgetDepList = GetBudgetDepParenList(objProfile, cmd, System.Convert.ToInt32(objNodeYear.Text));
                        if ((ParentBudgetDepList != null) && (ParentBudgetDepList.Count > 0))
                        {
                            foreach (CBudgetDep objParentBudgetDep in ParentBudgetDepList)
                            {
                                // ���� � ������������ ��������� ��������������
                                objMenuNodeChild = new UniXP.Common.MENUITEM();
                                objMenuNodeChild.enMenuType = objMenuItem.enMenuType;
                                objMenuNodeChild.strName = objParentBudgetDep.Name; //objBudgetItem.Date.Year.ToString() + " (" + objBudgetDep.Name + ")"; // "������ " + objBudgetItem.Date.Year.ToString();
                                objMenuNodeChild.lClassID = 1;
                                objMenuNodeChild.uuidFarClient = objMenuItem.uuidFarClient;
                                objMenuNodeChild.strDescription = objParentBudgetDep.Name;
                                objMenuNodeChild.nImage = objMenuItem.nImage;
                                objMenuNodeChild.strDLLName = objMenuItem.strDLLName;
                                objMenuNodeChild.enCmdType = objMenuItem.enCmdType;
                                objMenuNodeChild.enDLLType = objMenuItem.enDLLType;
                                objMenuNodeChild.objProfile = objMenuItem.objProfile;
                                objMenuNodeChild.objAdvancedParametr = null;

                                System.Windows.Forms.TreeNode objMenuChildTreeNode = new System.Windows.Forms.TreeNode();
                                objMenuChildTreeNode.ImageIndex = objMenuItem.nImage;
                                objMenuChildTreeNode.SelectedImageIndex = objMenuItem.nImage;
                                objMenuChildTreeNode.Text = objMenuNodeChild.strName;
                                objMenuChildTreeNode.Tag = objMenuNodeChild;
                                objNodeYear.Nodes.Add(objMenuChildTreeNode);

                                if (objParentBudgetDep.HasBudget == true)
                                {
                                    foreach (ERP_Budget.Common.CBudget objBudgetItem in objBudgetList)
                                    {
                                        // ������ � ��������� "������������ �������" �� ���������� ������ ����������
                                        if ((objBudgetItem.OffExpenditures == true) && bSuperVisor) { continue; };
                                        if ((objBudgetItem.BudgetDep.uuidID.CompareTo(objParentBudgetDep.uuidID) == 0) && (objBudgetItem.Date.Year == System.Convert.ToInt32(objNodeYear.Text)))
                                        {
                                            objMenuNodeChild.objAdvancedParametr = objBudgetItem.uuidID;
                                            // ��� ��� �� ������ �� ��������� �������� 
                                        }
                                    }

                                }
                            }
                        }
                    }

                }

                rs.Close();
                rs.Dispose();
                cmd.Dispose();
            }
            catch (System.Exception f)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(
                "�� ������� �������� ������ ��������� �������������.\n\n����� ������: " + f.Message, "��������",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
            finally // ������� ���������� �������
            {
                DBConnection.Close();
            }

            return objTreeView;
        }

        /// <summary>
        /// ���������� ������ ������������ ��������� �������������
        /// </summary>
        /// <param name="objProfile">�������</param>
        /// <param name="iBudgetYear">���</param>
        /// <returns>������ ��������� �������������</returns>
        public static List<CBudgetDep> GetBudgetDepParenList(UniXP.Common.CProfile objProfile, 
            System.Data.SqlClient.SqlCommand cmd, System.Int32 iBudgetYear)
        {
            List<CBudgetDep> objList = new List<CBudgetDep>();

            if (cmd == null) { return objList; }

            try
            {
                cmd.Parameters.Clear();
                cmd.CommandText = System.String.Format("[{0}].[dbo].[sp_GetBudgetDepParent]", objProfile.GetOptionsDllDBName());
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ((System.Byte)(0)), ((System.Byte)(0)), "", System.Data.DataRowVersion.Current, null));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@BudgetYear", System.Data.SqlDbType.Int));
                cmd.Parameters["@BudgetYear"].Value = iBudgetYear;
                System.Data.SqlClient.SqlDataReader rs = cmd.ExecuteReader();
                if (rs.HasRows)
                {
                    // ����� ������ ��������
                    CBudgetDep objBudgetDep = null;
                    while (rs.Read())
                    {
                        objBudgetDep = new CBudgetDep();
                        objBudgetDep.m_uuidID = (System.Guid)rs["GUID_ID"];
                        objBudgetDep.m_strName = (System.String)rs["BUDGETDEP_NAME"];
                        if (rs["PARENT_GUID_ID"] != System.DBNull.Value) { objBudgetDep.m_uuidParentID = (System.Guid)rs["PARENT_GUID_ID"]; }
                        objBudgetDep.m_Manager = new CUser();
                        objBudgetDep.m_Manager.Init(objProfile, (System.Int32)rs["BUDGETDEP_MANAGER"]);
                        objBudgetDep.m_bHasBudget = (System.Boolean)rs["IsContainingBudget"];
                        objList.Add(objBudgetDep);
                    }
                }
                rs.Close();
                rs.Dispose();
            }
            catch (System.Exception f)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(
                "�� ������� �������� ������ ��������� �������������.\n\n����� ������: " + f.Message, "��������",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
            finally // ������� ���������� �������
            {
            }

            return objList;
        }
        /// <summary>
        /// ���������� ������ ������������ ��������� �������������
        /// </summary>
        /// <param name="objProfile">�������</param>
        /// <param name="uuidBudgetDepId">�� ������������� �������������</param>
        /// <param name="iBudgetYear">���</param>
        /// <returns>������ ��������� �������������</returns>
        public static List<CBudgetDep> GetBudgetDepChildList(UniXP.Common.CProfile objProfile, System.Guid uuidBudgetDepId, 
            System.Int32 iBudgetYear)
        {
            List<CBudgetDep> objList = new List<CBudgetDep>();

            System.Data.SqlClient.SqlConnection DBConnection = objProfile.GetDBSource();
            if (DBConnection == null) { return objList; }

            try
            {
                // ���������� � �� ��������, ����������� ������� �� ������� ������
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
                cmd.Connection = DBConnection;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = System.String.Format("[{0}].[dbo].[sp_GetBudgetDepChild]", objProfile.GetOptionsDllDBName());
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ((System.Byte)(0)), ((System.Byte)(0)), "", System.Data.DataRowVersion.Current, null));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@BudgetYear", System.Data.SqlDbType.Int));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@BudgetDepParent_Guid", System.Data.SqlDbType.UniqueIdentifier));
                cmd.Parameters["@BudgetYear"].Value = iBudgetYear;
                cmd.Parameters["@BudgetDepParent_Guid"].Value = uuidBudgetDepId;
                System.Data.SqlClient.SqlDataReader rs = cmd.ExecuteReader();
                if (rs.HasRows)
                {
                    // ����� ������ ��������
                    CBudgetDep objBudgetDep = null;
                    while (rs.Read())
                    {
                        objBudgetDep = new CBudgetDep();
                        objBudgetDep.m_uuidID = (System.Guid)rs["GUID_ID"];
                        objBudgetDep.m_strName = (System.String)rs["BUDGETDEP_NAME"];
                        if (rs["PARENT_GUID_ID"] != System.DBNull.Value) { objBudgetDep.m_uuidParentID = (System.Guid)rs["PARENT_GUID_ID"]; }
                        objBudgetDep.m_Manager = new CUser();
                        objBudgetDep.m_Manager.Init(objProfile, (System.Int32)rs["BUDGETDEP_MANAGER"]);
                        objBudgetDep.m_bHasBudget = (System.Boolean)rs["IsContainingBudget"];
                        objList.Add(objBudgetDep);
                    }
                }
                rs.Close();
                rs.Dispose();
                //// ��������� ���������� ������ �������������� ������
                //foreach (CBudgetDep objBudgetDepItem in objList)
                //{
                //    objBudgetDepItem.LoadAdditionalManagerList(objProfile, cmd);
                //}

                cmd.Dispose();
            }
            catch (System.Exception f)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(
                "�� ������� �������� ������ �������� ��������� �������������.\n\n����� ������: " + f.Message, "��������",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
            finally // ������� ���������� �������
            {
                DBConnection.Close();
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
                cmd.CommandText = System.String.Format( "[{0}].[dbo].[sp_GetBudgetDep]", objProfile.GetOptionsDllDBName() );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ( ( System.Byte )( 0 ) ), ( ( System.Byte )( 0 ) ), "", System.Data.DataRowVersion.Current, null ) );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@GUID_ID", System.Data.SqlDbType.UniqueIdentifier ) );
                cmd.Parameters[ "@GUID_ID" ].Value = uuidID;
                System.Data.SqlClient.SqlDataReader rs = cmd.ExecuteReader();
                if( rs.HasRows )
                {
                    // ����� ������ ��������, � ��� ��� ���������� ���� ������
                    rs.Read();
                    this.m_uuidID = rs.GetGuid( 0 );
                    this.m_strName = rs.GetString( 2 );
                    if( rs[ 1 ] != System.DBNull.Value ) { this.m_uuidParentID = rs.GetGuid( 1 ); }
                    CUser objManager = new CUser();
                    objManager.Init( objProfile, rs.GetInt32( 3 ) );
                    this.m_Manager = objManager;
                    this.m_UsesrList = this.GetUserList( objProfile, this.m_uuidID );
                    bRet = true;
                }
                else
                {
                    DevExpress.XtraEditors.XtraMessageBox.Show( 
                    "�� ������� ������������������� ����� CBudgetDep.\n� �� �� ������� ����������.", "��������",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning );

                }
                cmd.Dispose();
                rs.Dispose();
            }
            catch( System.Exception e )
            {
                DevExpress.XtraEditors.XtraMessageBox.Show( 
                "�� ������� ������������������� ����� CBudgetDep.\n" + e.Message, "��������",
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
        /// <param name="objProfile">�������</param>
        /// <returns>true - �������� �������������; false - ������</returns>
        public System.Boolean Init( UniXP.Common.CProfile objProfile )
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
                cmd.CommandText = System.String.Format( "[{0}].[dbo].[sp_GetBudgetDepFromManager]", objProfile.GetOptionsDllDBName() );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ( ( System.Byte )( 0 ) ), ( ( System.Byte )( 0 ) ), "", System.Data.DataRowVersion.Current, null ) );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@ulUniXPID", System.Data.SqlDbType.Int ) );
                cmd.Parameters[ "@ulUniXPID" ].Value = objProfile.m_nSQLUserID;
                System.Data.SqlClient.SqlDataReader rs = cmd.ExecuteReader();
                if( rs.HasRows )
                {
                    // ����� ������ ��������, � ��� ��� ���������� ���� ������
                    rs.Read();
                    this.m_uuidID = rs.GetGuid( 0 );
                    this.m_strName = rs.GetString( 2 );
                    if( rs[ 1 ] != System.DBNull.Value ) { this.m_uuidParentID = rs.GetGuid( 1 ); }
                    CUser objManager = new CUser();
                    objManager.Init( objProfile, rs.GetInt32( 3 ) );
                    this.m_Manager = objManager;
                    this.m_UsesrList = this.GetUserList( objProfile, this.m_uuidID );
                    bRet = true;
                }
                else
                {
                    DevExpress.XtraEditors.XtraMessageBox.Show( 
                    "�� ������� ������������������� ����� CBudgetDep.\n� �� �� ������� ����������.", "��������",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning );
                }
                cmd.Dispose();
                rs.Dispose();
            }
            catch( System.Exception e )
            {
                DevExpress.XtraEditors.XtraMessageBox.Show( 
                "�� ������� ������������������� ����� CBudgetDep.\n" + e.Message, "��������",
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
                cmd.CommandText = System.String.Format( "[{0}].[dbo].[sp_DeleteBudgetDep]", objProfile.GetOptionsDllDBName() );
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
                        DevExpress.XtraEditors.XtraMessageBox.Show(  "��������� ������������� ������� � ��������.\n�������� ����������", "��������",
                            System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning );
                    }
                    else
                    {
                        DevExpress.XtraEditors.XtraMessageBox.Show(  "������ �������� ���������� �������������", "������",
                            System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
                    }
                }
                cmd.Dispose();
            }
            catch( System.Exception e )
            {
                DevExpress.XtraEditors.XtraMessageBox.Show( 
                "�� ������� ������� ��������� �������������.\n" + e.Message, "��������",
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
                DevExpress.XtraEditors.XtraMessageBox.Show(  "������������ ������������ ���������� �������������", "��������",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning );
                return bRet;
            }
            // ���������� ������� ������������� �������
            if( this.m_Manager == null )
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(  "������� ������������� ���������� �������������", "��������",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning );
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
                cmd.CommandText = System.String.Format( "[{0}].[dbo].[sp_AddBudgetDep]", objProfile.GetOptionsDllDBName() );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ( ( System.Byte )( 0 ) ), ( ( System.Byte )( 0 ) ), "", System.Data.DataRowVersion.Current, null ) );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@GUID_ID", System.Data.SqlDbType.UniqueIdentifier, 4, System.Data.ParameterDirection.Output, false, ( ( System.Byte )( 0 ) ), ( ( System.Byte )( 0 ) ), "", System.Data.DataRowVersion.Current, null ) );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@BUDGETDEP_NAME", System.Data.DbType.String ) );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@BUDGETDEP_MANAGER", System.Data.SqlDbType.Int, 4 ) );
                if( this.ParentID.CompareTo( System.Guid.Empty ) != 0 )
                {
                    cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@PARENT_GUID_ID", System.Data.SqlDbType.UniqueIdentifier, 4 ) );
                    cmd.Parameters[ "@PARENT_GUID_ID" ].Value = this.m_uuidParentID; 
                }                
                cmd.Parameters[ "@BUDGETDEP_NAME" ].Value = this.m_strName;
                cmd.Parameters[ "@BUDGETDEP_MANAGER" ].Value = this.m_Manager.ulID;
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
                        DevExpress.XtraEditors.XtraMessageBox.Show(  "��������� ������������� '" + this.m_strName + "' ��� ���� � ��", "��������",
                            System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning );
                    }
                    else
                    {
                        DevExpress.XtraEditors.XtraMessageBox.Show(  "������ �������� ���������� �������������", "������",
                            System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
                    }
                }
                cmd.Dispose();
            }
            catch( System.Exception e )
            {
                DevExpress.XtraEditors.XtraMessageBox.Show( 
                "������ �������� ���������� �������������.\n" + e.Message, "��������",
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
                DevExpress.XtraEditors.XtraMessageBox.Show(  "������������ ������������ ���������� �������������", "��������",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning );
                return bRet;
            }
            // ���������� ������� ������������� �������
            if( this.m_Manager == null )
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(  "������� ������������� ���������� �������������", "��������",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning );
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
                cmd.CommandText = System.String.Format( "[{0}].[dbo].[sp_EditBudgetDep]", objProfile.GetOptionsDllDBName() );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ( ( System.Byte )( 0 ) ), ( ( System.Byte )( 0 ) ), "", System.Data.DataRowVersion.Current, null ) );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@GUID_ID", System.Data.SqlDbType.UniqueIdentifier, 4 ) );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@BUDGETDEP_NAME", System.Data.DbType.String ) );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@BUDGETDEP_MANAGER", System.Data.SqlDbType.Int, 4 ) );
                cmd.Parameters[ "@GUID_ID" ].Value = this.m_uuidID;
                if( this.m_uuidParentID.CompareTo( System.Guid.Empty ) != 0 )
                {
                    cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@PARENT_GUID_ID", System.Data.SqlDbType.UniqueIdentifier, 4 ) );
                    cmd.Parameters[ "@PARENT_GUID_ID" ].Value = this.m_uuidParentID; 
                }
                cmd.Parameters[ "@BUDGETDEP_NAME" ].Value = this.m_strName;
                cmd.Parameters[ "@BUDGETDEP_MANAGER" ].Value = this.m_Manager.ulID;
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
                        DevExpress.XtraEditors.XtraMessageBox.Show(  "��������� ������������� '" + this.m_strName + "' ��� ���� � ��", "��������",
                            System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning );
                        break;
                        case 2:
                        DevExpress.XtraEditors.XtraMessageBox.Show(  "������ � ��������� ��������������� �� ������� \n" + this.m_uuidID.ToString(), "��������",
                            System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
                        break;
                        default:
                        DevExpress.XtraEditors.XtraMessageBox.Show(  "������ ��������� ������� ���������� �������������", "��������",
                            System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
                        break;
                    }
                }
                cmd.Dispose();
            }
            catch( System.Exception e )
            {
                DevExpress.XtraEditors.XtraMessageBox.Show( 
                "������ ��������� ������� ���������� �������������.\n" + e.Message, "��������",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
            }
			finally // ������� ���������� �������
            {
                DBConnection.Close();
            }
            return bRet;
        }
        /// <summary>
        /// ��������� ���������� � ������� ���������� ������������� � ��
        /// </summary>
        /// <param name="objProfile">�������</param>
        /// <returns>true - �������� ����������; false - ������</returns>
        public System.Boolean SaveBudgetDepDeclaration( UniXP.Common.CProfile objProfile )
        {
            System.Boolean bRet = false;

            if( this.UsesrList.GetCountItems() == 0 ) { return true; }

            System.Data.SqlClient.SqlConnection DBConnection = objProfile.GetDBSource();
            if( DBConnection == null ) { return bRet; }

            try
            {
                // ������ ������� ����� ������������� � �������� ��������� ��������������
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
                cmd.Connection = DBConnection;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = System.String.Format( "[{0}].[dbo].[sp_DeleteBudgetDepDeclaration]", objProfile.GetOptionsDllDBName() );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ( ( System.Byte )( 0 ) ), ( ( System.Byte )( 0 ) ), "", System.Data.DataRowVersion.Current, null ) );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@BUDGETDEP_GUID_ID", System.Data.SqlDbType.UniqueIdentifier, 4 ) );
                cmd.Parameters[ "@BUDGETDEP_GUID_ID" ].Value = this.m_uuidID;
                cmd.ExecuteNonQuery();
                System.Int32 iRet = ( System.Int32 )cmd.Parameters[ "@RETURN_VALUE" ].Value;
                if( iRet == 0 )
                {
                    cmd.Parameters.Clear();
                    cmd.CommandText = System.String.Format( "[{0}].[dbo].[sp_AssignUserBudgetDep]", objProfile.GetOptionsDllDBName() );
                    cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@BUDGETDEP_GUID_ID", System.Data.SqlDbType.UniqueIdentifier, 4 ) );
                    cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@ulUserID", System.Data.SqlDbType.Int, 4 ) );
                    cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@bAssign", System.Data.SqlDbType.Bit, 1 ) );
                    cmd.Parameters[ "@BUDGETDEP_GUID_ID" ].Value = this.m_uuidID;
                    cmd.Parameters[ "@bAssign" ].Value = 1;
                    CUser objUser = null;
                    for( System.Int32 i = 0; i < this.UsesrList.GetCountItems(); i++ )
                    {
                        objUser = this.UsesrList.GetByIndex( i );
                        if( objUser == null ) { continue; }
                        try
                        {
                            cmd.Parameters[ "@ulUserID" ].Value = objUser.ulID;
                            cmd.ExecuteNonQuery();
                        }
                        catch( System.Exception e )
                        {
                            DevExpress.XtraEditors.XtraMessageBox.Show( 
                            "������ ���������� ������������ � ��������� �������������.\n������������: " + 
                            objUser.UserLastName + " " + objUser.UserFirstName + e.Message, "��������",
                            System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning );
                            continue;
                        }
                    }
                    bRet = true;
                }
                else
                {
                    switch( iRet )
                    {
                        case 1:
                        DevExpress.XtraEditors.XtraMessageBox.Show(  "������ � ��������� ��������������� �� ������� \n" + this.m_uuidID.ToString(), "��������",
                            System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
                        break;
                        default:
                        DevExpress.XtraEditors.XtraMessageBox.Show(  "������ �������� ������� ���������� �������������", "��������",
                            System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
                        break;
                    }
                }
                cmd.Dispose();
            }
            catch( System.Exception e )
            {
                DevExpress.XtraEditors.XtraMessageBox.Show( 
                "������ ���������� ������� ���������� �������������.\n" + e.Message, "��������",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
            }
			finally // ������� ���������� �������
            {
                DBConnection.Close();
            }
            return bRet;
        }
        /// <summary>
        /// ��������� �������� ������� �� ������� ������
        /// </summary>
        /// <returns></returns>
        private System.Boolean bIsValuesValidForSaveToDB()
        {
            System.Boolean bRet = false;
            try
            {
                if( this.m_uuidID == System.Guid.Empty )
                {
                    DevExpress.XtraEditors.XtraMessageBox.Show(  "������������ �������� ����������� �������������� �������", "��������",
                        System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
                    return bRet;
                }
                if( ( this.m_State == System.Data.DataRowState.Added ) || 
                    ( this.m_State == System.Data.DataRowState.Modified ) )
                {
                    // ������������ �� ������ ���� ������
                    if( this.Name == "" )
                    {
                        DevExpress.XtraEditors.XtraMessageBox.Show(  "������������ ������������ ���������� �������������", "��������",
                            System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
                        return bRet;
                    }
                    if( this.Manager == null )
                    {
                        DevExpress.XtraEditors.XtraMessageBox.Show(  "������������ ������������� ������ ���� ������", "��������",
                            System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
                        return bRet;
                    }
                }
                bRet = true;
            }
            catch( System.Exception e )
            {
                DevExpress.XtraEditors.XtraMessageBox.Show( 
                "������ �������� �������� ������� '��������� �������������'\n����� ����������� � ��" + e.Message, "��������",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
            }
            return bRet;
        }

        /// <summary>
        /// ��������� � �� ������ ��������� �������������
        /// </summary>
        /// <param name="BudgetDepList">������ ��������� �������������</param>
        /// <param name="objProfile">�������</param>
        /// <returns>true - �������� ����������; false - ������</returns>
        public static System.Boolean SaveBudgetDepListToDB( CBaseList<CBudgetDep> BudgetDepList,
            UniXP.Common.CProfile objProfile )
        {
            System.Boolean bRes = false;
            // ��������, � �� ������ �� ������
            if( BudgetDepList.GetCountItems() == 0 ) { return bRes; }
            // ���������� � ��
            System.Data.SqlClient.SqlConnection DBConnection = objProfile.GetDBSource();
            if( DBConnection == null ) { return bRes; }
            System.Data.SqlClient.SqlTransaction DBTransaction = null;
            try
            {
                // ���������� � �� ��������, ��������� ��������� � ���� ������ ���������� 
                // �� ���������� � ������ ������ ��������
                System.Int32 iObjectsCount = BudgetDepList.GetCountItems();
                CBudgetDep objBudgetDep = null;
                // ��������� ����������
                DBTransaction = DBConnection.BeginTransaction();
                // SQL-������� 
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
                cmd.Connection = DBConnection;
                cmd.Transaction = DBTransaction;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                System.Int32 iRet = -1;
                for( System.Int32 i = 0; i< iObjectsCount; i++ )
                {
                    objBudgetDep = BudgetDepList.GetByIndex( i );
                    if( objBudgetDep == null ) { continue; }
                    // ��������� ������ �� ������� ��������� ��������
                    if( objBudgetDep.bIsValuesValidForSaveToDB() == false )
                    {
                        DBTransaction.Rollback();
                        break;
                    }
                    // �������������� ��������� SQL-�������
                    cmd.Parameters.Clear();
                    iRet = -1;
                    switch( objBudgetDep.State )
                    {
                        case System.Data.DataRowState.Added:
                        {
                            // ����� �������������
                            cmd.CommandText = System.String.Format( "[{0}].[dbo].[sp_AddBudgetDep]", objProfile.GetOptionsDllDBName() );
                            cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@RETURN_VALUE", System.Data.SqlDbType.Int, 8, System.Data.ParameterDirection.ReturnValue, false, ( ( System.Byte )( 0 ) ), ( ( System.Byte )( 0 ) ), "", System.Data.DataRowVersion.Current, null ) );
                            cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@GUID_ID", System.Data.SqlDbType.UniqueIdentifier, 16, System.Data.ParameterDirection.Output, false, ( ( System.Byte )( 0 ) ), ( ( System.Byte )( 0 ) ), "", System.Data.DataRowVersion.Current, null ) );
                            cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@BUDGETDEP_GUID_ID", System.Data.SqlDbType.UniqueIdentifier, 16 ) );
                            cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@BUDGETDEP_NAME", System.Data.DbType.String ) );
                            cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@BUDGETDEP_MANAGER", System.Data.SqlDbType.Int, 8 ) );
                            cmd.Parameters[ "@BUDGETDEP_GUID_ID" ].Value = objBudgetDep.uuidID;
                            if( objBudgetDep.ParentID.CompareTo( System.Guid.Empty ) != 0 )
                            {
                                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@PARENT_GUID_ID", System.Data.SqlDbType.UniqueIdentifier, 16 ) );
                                cmd.Parameters[ "@PARENT_GUID_ID" ].Value = objBudgetDep.ParentID;
                            }
                            cmd.Parameters[ "@BUDGETDEP_NAME" ].Value = objBudgetDep.Name;
                            cmd.Parameters[ "@BUDGETDEP_MANAGER" ].Value = objBudgetDep.Manager.ulID;
                            cmd.ExecuteNonQuery();
                            iRet = ( System.Int32 )cmd.Parameters[ "@RETURN_VALUE" ].Value;
                            if( iRet != 0 )
                            {
                                DBTransaction.Rollback();
                                switch( iRet )
                                {
                                    case 1:
                                        {
                                            DevExpress.XtraEditors.XtraMessageBox.Show( 
                                            "��������� ������������� � �������� ������ ����������\n" + objBudgetDep.Name + 
                                            "\n������ ��������� ���������.", "��������",
                                            System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
                                            break;
                                        }
                                    default:
                                        {
                                            DevExpress.XtraEditors.XtraMessageBox.Show( 
                                            "������ ���������� � �� ������ ���������� �������������\n" + objBudgetDep.Name + 
                                            "\n������ ��������� ���������.", "��������",
                                            System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
                                            break;
                                        }
                                }
                            }
                            break;
                        }

                        case System.Data.DataRowState.Modified:
                        {
                            // ��������� ������� �������������
                            cmd.CommandText = System.String.Format( "[{0}].[dbo].[sp_EditBudgetDep]", objProfile.GetOptionsDllDBName() );
                            cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ( ( System.Byte )( 0 ) ), ( ( System.Byte )( 0 ) ), "", System.Data.DataRowVersion.Current, null ) );
                            cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@GUID_ID", System.Data.SqlDbType.UniqueIdentifier, 16 ) );
                            cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@BUDGETDEP_NAME", System.Data.DbType.String ) );
                            cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@BUDGETDEP_MANAGER", System.Data.SqlDbType.Int, 8 ) );
                            cmd.Parameters[ "@GUID_ID" ].Value = objBudgetDep.uuidID;
                            if( objBudgetDep.ParentID.CompareTo( System.Guid.Empty ) != 0 )
                            {
                                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@PARENT_GUID_ID", System.Data.SqlDbType.UniqueIdentifier, 16 ) );
                                cmd.Parameters[ "@PARENT_GUID_ID" ].Value = objBudgetDep.ParentID; 
                            }
                            cmd.Parameters[ "@BUDGETDEP_NAME" ].Value = objBudgetDep.Name;
                            cmd.Parameters[ "@BUDGETDEP_MANAGER" ].Value = objBudgetDep.Manager.ulID;
                            cmd.ExecuteNonQuery();
                            iRet = ( System.Int32 )cmd.Parameters[ "@RETURN_VALUE" ].Value;
                            if( iRet != 0 )
                            {
                                DBTransaction.Rollback();
                                switch( iRet )
                                {
                                    case 1:
                                        {
                                            DevExpress.XtraEditors.XtraMessageBox.Show( 
                                            "��������� ������������� � �������� ������ ����������\n" + objBudgetDep.Name + 
                                            "\n������ ��������� ���������.", "��������",
                                            System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
                                            break;
                                        }
                                    case 2:
                                        {
                                            DevExpress.XtraEditors.XtraMessageBox.Show( 
                                            "��������� ������������� � �������� ��������������� �� �������\n" + objBudgetDep.Name + "\n" + objBudgetDep.uuidID.ToString() + 
                                            "\n������ ��������� ���������.", "��������",
                                            System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
                                            break;
                                        }
                                    default:
                                        {
                                            DevExpress.XtraEditors.XtraMessageBox.Show( 
                                            "������ ��������� ������� ���������� �������������\n" + objBudgetDep.Name + 
                                            "\n������ ��������� ���������.", "��������",
                                            System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
                                            break;
                                        }
                                }
                            }

                            break;
                        } // case System.Data.DataRowState.Modified:

                        case System.Data.DataRowState.Deleted:
                        {
                            // �������� ���������� ������������� �� ��
                            cmd.CommandText = System.String.Format( "[{0}].[dbo].[sp_DeleteBudgetDepOne]", objProfile.GetOptionsDllDBName() );
                            cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@RETURN_VALUE", System.Data.SqlDbType.Int, 8, System.Data.ParameterDirection.ReturnValue, false, ( ( System.Byte )( 0 ) ), ( ( System.Byte )( 0 ) ), "", System.Data.DataRowVersion.Current, null ) );
                            cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@GUID_ID", System.Data.SqlDbType.UniqueIdentifier ) );
                            cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@ERROR_NUMBER", System.Data.SqlDbType.Int, 8, System.Data.ParameterDirection.Output, false, ( ( System.Byte )( 0 ) ), ( ( System.Byte )( 0 ) ), "", System.Data.DataRowVersion.Current, null ) );
                            cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@ERROR_MESSAGE", System.Data.DbType.String ) );
                            cmd.Parameters[ "@ERROR_MESSAGE" ].Direction = System.Data.ParameterDirection.Output;

                            cmd.Parameters[ "@GUID_ID" ].Value = objBudgetDep.uuidID;
                            cmd.ExecuteNonQuery();
                            iRet = ( System.Int32 )cmd.Parameters[ "@RETURN_VALUE" ].Value;
                            if( iRet == 3 )
                            {
                                //������ ������ ��� �� ���� ��������� � ��
                                iRet = 0;
                            }
                            if( iRet != 0 )
                            {
                                DBTransaction.Rollback();
                                switch( iRet )
                                {
                                    case 1:
                                        {
                                            DevExpress.XtraEditors.XtraMessageBox.Show( 
                                            "��������� ������������� ������� � �������� ��� ��������� ����������\n" + objBudgetDep.Name + 
                                            "\n������ ��������.", "��������",
                                            System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
                                            break;
                                        }
                                    case 4:
                                        {
                                            DevExpress.XtraEditors.XtraMessageBox.Show( 
                                            "��������� ������������� �������� �������� ��������� �������������\n" + objBudgetDep.Name + "\n" + objBudgetDep.uuidID.ToString() + 
                                            "\n������ ��������.", "��������",
                                            System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
                                            break;
                                        }
                                    default:
                                        {
                                            DevExpress.XtraEditors.XtraMessageBox.Show( 
                                            "������ �������� ���������� �������������\n" + objBudgetDep.Name + 
                                            "\n������ ��������.", "��������",
                                            System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
                                            break;
                                        }
                                }
                            } // if( iRet != 0 )
                            break;
                            } // case System.Data.DataRowState.Deleted:

                        } //switch( objBudgetDep.State )
                    //}
                    if( iRet != 0 ) { break; }
                    else
                    {
                        // ��������� ��������� ������ ����������� ��� ������� ���������� �������������
                        if( ( objBudgetDep.State == System.Data.DataRowState.Added ) || 
                            ( objBudgetDep.State == System.Data.DataRowState.Modified ) )
                        {
                            // ������ ������� ����� ������ �������� � ���������� ���������������
                            cmd.Parameters.Clear();
                            cmd.CommandText = System.String.Format( "[{0}].[dbo].[sp_DeleteBudgetDepDeclaration]", objProfile.GetOptionsDllDBName() );
                            cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@RETURN_VALUE", System.Data.SqlDbType.Int, 8, System.Data.ParameterDirection.ReturnValue, false, ( ( System.Byte )( 0 ) ), ( ( System.Byte )( 0 ) ), "", System.Data.DataRowVersion.Current, null ) );
                            cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@BUDGETDEP_GUID_ID", System.Data.SqlDbType.UniqueIdentifier ) );
                            cmd.Parameters[ "@BUDGETDEP_GUID_ID" ].Value = objBudgetDep.uuidID;
                            cmd.ExecuteNonQuery();
                            iRet = ( System.Int32 )cmd.Parameters[ "@RETURN_VALUE" ].Value;
                            if( iRet != 0 )
                            {
                                DBTransaction.Rollback();
                                switch( iRet )
                                {
                                    case 1:
                                        {
                                            DevExpress.XtraEditors.XtraMessageBox.Show( 
                                            "������ �������� ������ ����������� � ���������� �������������\n��������� ������������� � �������� ��������������� �� �������\n" + objBudgetDep.Name + " " + objBudgetDep.uuidID.ToString() + 
                                            "\n������ ��������� ���������.", "��������",
                                            System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
                                            break;
                                        }
                                    default:
                                        {
                                            DevExpress.XtraEditors.XtraMessageBox.Show( 
                                            "������ �������� ������ ����������� � ���������� �������������\n" + objBudgetDep.Name + 
                                            "\n������ ��������� ���������.", "��������",
                                            System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
                                            break;
                                        }
                                }
                            }
                            else
                            {
                                if( objBudgetDep.UsesrList.GetCountItems() > 0 )
                                {
                                    // ����������� ������ �����������
                                    cmd.Parameters.Clear();
                                    cmd.CommandText = System.String.Format( "[{0}].[dbo].[sp_AssignUserBudgetDep]", objProfile.GetOptionsDllDBName() );
                                    cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@RETURN_VALUE", System.Data.SqlDbType.Int, 8, System.Data.ParameterDirection.ReturnValue, false, ( ( System.Byte )( 0 ) ), ( ( System.Byte )( 0 ) ), "", System.Data.DataRowVersion.Current, null ) );
                                    cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@ulUserID", System.Data.SqlDbType.Int ) );
                                    cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@BUDGETDEP_GUID_ID", System.Data.SqlDbType.UniqueIdentifier ) );
                                    cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@bAssign", System.Data.SqlDbType.Bit ) );
                                    cmd.Parameters[ "@BUDGETDEP_GUID_ID" ].Value = objBudgetDep.uuidID;
                                    cmd.Parameters[ "@bAssign" ].Value = 1;
                                    CUser objUser = null;
                                    iRet = -1;
                                    for( System.Int32 i2 = 0; i2 < objBudgetDep.UsesrList.GetCountItems(); i2++ )
                                    {
                                        objUser = objBudgetDep.UsesrList.GetByIndex( i2 );
                                        if( objUser == null ) { continue; }
                                        try
                                        {
                                            cmd.Parameters[ "@ulUserID" ].Value = objUser.ulID;
                                            cmd.ExecuteNonQuery();
                                            iRet = ( System.Int32 )cmd.Parameters[ "@RETURN_VALUE" ].Value;
                                            if( iRet != 0 )
                                            {
                                                DBTransaction.Rollback();
                                                switch( iRet )
                                                {
                                                    case 1:
                                                    DevExpress.XtraEditors.XtraMessageBox.Show(  "��������� ������������� � �������� ��������������� �� �������.", "��������",
                                                        System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
                                                    break;
                                                    case 2:
                                                    DevExpress.XtraEditors.XtraMessageBox.Show(  "��������� � �������� ��������������� �� ������.", "��������",
                                                        System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
                                                    break;
                                                    default:
                                                    DevExpress.XtraEditors.XtraMessageBox.Show(  "������ ��������� ���������� � ������ ���������� �������������.", "��������",
                                                        System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
                                                    break;
                                                }
                                                // ����� �� �����
                                                break;
                                            }
                                        }
                                        catch( System.Exception e )
                                        {
                                            DBTransaction.Rollback();
                                            DevExpress.XtraEditors.XtraMessageBox.Show( 
                                            "������ ��������� ���������� � ������ ���������� �������������.\n��������� �������������: " + 
                                            objBudgetDep.Name + "\n���������" + objUser.UserLastName + " " + objUser.UserFirstName + "\n" + e.Message, "��������",
                                            System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning );
                                            break;
                                        }
                                    } // for ...
                                    if( iRet != 0 ) { break; }
                                }

                            }

                        } // if
                    }
                } // for
                if( iRet == 0 )
                {
                    DBTransaction.Commit();
                    bRes = true;
                }
            }
            catch( System.Exception e )
            {
                DBTransaction.Rollback();
                DevExpress.XtraEditors.XtraMessageBox.Show( 
                "�� ������� ��������� ��������� � ��.\n" + e.Message, "��������",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
            }
			finally // ������� ���������� �������
            {
                DBConnection.Close();
            }
            return bRes;
        }
        #endregion

        #region ������ �������������� �������������� �������������
        /// <summary>
        /// ��������� ������ �������������� �������������� �������������
        /// </summary>
        /// <param name="objProfile">�������</param>
        /// <param name="uuidBudgetDepID">�� �������������</param>
        /// <param name="objTreeList">������ �� �������</param>
        public static void RefreshBudgetDepManagerList(UniXP.Common.CProfile objProfile, System.Guid uuidBudgetDepID,
            DevExpress.XtraTreeList.TreeList objTreeList)
        {
            System.Data.SqlClient.SqlConnection DBConnection = objProfile.GetDBSource();
            if (DBConnection == null) 
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(
                "����������� ���������� � ����� ������.", "��������",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);

                return ; 
            }

            try
            {
                objTreeList.ClearNodes();

                // ���������� � �� ��������, ����������� ������� �� ������� ������
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
                cmd.Connection = DBConnection;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = System.String.Format("[{0}].[dbo].[sp_GetBudgetDepAdvancedManagerList]", objProfile.GetOptionsDllDBName());
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ((System.Byte)(0)), ((System.Byte)(0)), "", System.Data.DataRowVersion.Current, null));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@GUID_ID", System.Data.SqlDbType.UniqueIdentifier, 4));
                cmd.Parameters["@GUID_ID"].Value = uuidBudgetDepID;
                System.Data.SqlClient.SqlDataReader rs = cmd.ExecuteReader();
                if (rs.HasRows)
                {
                    // ����� ������ ��������
                    CUser objManager = null;
                    while (rs.Read())
                    {
                        objManager = new CUser((System.Int32)rs["ErpBudgetUserID"], (System.Int32)rs["UniXPUserID"],
                            (System.String)rs["strLastName"], (System.String)rs["strFirstName"]);

                        //��������� ���� � ������
                        DevExpress.XtraTreeList.Nodes.TreeListNode objNode =
                            objTreeList.AppendNode(new object[] { System.Convert.ToBoolean( rs["IsManager"] ), objManager.UserFullName}, null);

                        objNode.Tag = objManager;
                    }
                }
                rs.Close();
                rs.Dispose();

                cmd.Dispose();
            }
            catch (System.Exception f)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(
                "�� ������� �������� ������ �������������� �������������� �������������.\n\n����� ������: " + f.Message, "��������",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
            finally // ������� ���������� �������
            {
                DBConnection.Close();
            }

            return ;
        }
        /// <summary>
        /// ��������� � �� ������ �������������� �������������� �������
        /// </summary>
        /// <param name="objProfile">�������</param>
        /// <param name="uuidBudgetDepID">�� �������������</param>
        /// <param name="objTreeList">������ �� �������</param>
        /// <returns>true - �������� ���������� ��������; false - ������</returns>
        public static System.Boolean SaveBudgetDepManagerList(UniXP.Common.CProfile objProfile, System.Guid uuidBudgetDepID,
            DevExpress.XtraTreeList.TreeList objTreeList)
        {
            System.Boolean bRet = false;
            // ���������� ������������� �� ������ ���� ������
            if (uuidBudgetDepID == System.Guid.Empty)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show("������������ �������� ����������� �������������� �������������.\n" +
                    uuidBudgetDepID.ToString(), "��������",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                return bRet;
            }

            System.Data.SqlClient.SqlConnection DBConnection = objProfile.GetDBSource();
            if (DBConnection == null) 
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(
                "����������� ���������� � ����� ������.", "��������",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                return bRet;
            }
            System.Data.SqlClient.SqlTransaction DBTransaction = DBConnection.BeginTransaction();
            try
            {
                // ���������� � �� ��������, ����������� ������� �� �������� ������
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
                cmd.Connection = DBConnection;
                cmd.Transaction = DBTransaction;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = System.String.Format("[{0}].[dbo].[sp_DeleteBudgetDepAdvancedManagerList]", objProfile.GetOptionsDllDBName());
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ((System.Byte)(0)), ((System.Byte)(0)), "", System.Data.DataRowVersion.Current, null));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@GUID_ID", System.Data.SqlDbType.UniqueIdentifier));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ERROR_NUMBER", System.Data.SqlDbType.Int, 8, System.Data.ParameterDirection.Output, false, ((System.Byte)(0)), ((System.Byte)(0)), "", System.Data.DataRowVersion.Current, null));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ERROR_MESSAGE", System.Data.SqlDbType.NVarChar, 4000));
                cmd.Parameters["@ERROR_MESSAGE"].Direction = System.Data.ParameterDirection.Output;
                cmd.Parameters["@GUID_ID"].Value = uuidBudgetDepID;
                cmd.ExecuteNonQuery();
                System.Int32 iRet = (System.Int32)cmd.Parameters["@RETURN_VALUE"].Value;
                if (iRet == 0)
                {
                    if (objTreeList.Nodes.Count > 0)
                    {
                        cmd.CommandText = System.String.Format("[{0}].[dbo].[sp_AddBudgetDepAdvancedManager]", objProfile.GetOptionsDllDBName());
                        cmd.Parameters.Clear();
                        cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ((System.Byte)(0)), ((System.Byte)(0)), "", System.Data.DataRowVersion.Current, null));
                        cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@BUDGETDEP_GUID_ID", System.Data.SqlDbType.UniqueIdentifier));
                        cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@USER_ID", System.Data.SqlDbType.Int));

                        cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ERROR_NUMBER", System.Data.SqlDbType.Int, 8, System.Data.ParameterDirection.Output, false, ((System.Byte)(0)), ((System.Byte)(0)), "", System.Data.DataRowVersion.Current, null));
                        cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ERROR_MESSAGE", System.Data.SqlDbType.NVarChar, 4000));

                        cmd.Parameters["@ERROR_MESSAGE"].Direction = System.Data.ParameterDirection.Output;
                        cmd.Parameters["@BUDGETDEP_GUID_ID"].Value = uuidBudgetDepID;

                        foreach (DevExpress.XtraTreeList.Nodes.TreeListNode objNode in objTreeList.Nodes)
                        {
                            if ((System.Boolean)objNode.GetValue(objTreeList.Columns[0]) == true)
                            {
                                cmd.Parameters["@USER_ID"].Value = ( ( CUser )objNode.Tag ).ulID;
                                cmd.ExecuteNonQuery();
                                iRet = (System.Int32)cmd.Parameters["@RETURN_VALUE"].Value;
                                if (iRet != 0)
                                {
                                    // ���������� ����������
                                    DBTransaction.Rollback();
                                    break;
                                }
                            }
                        }
                    }
                    if (iRet == 0)
                    {
                        // ������������ ����������
                        DBTransaction.Commit();
                        bRet = true;
                    }
                }
                else
                {
                    // ���������� ����������
                    DBTransaction.Rollback();
                    DevExpress.XtraEditors.XtraMessageBox.Show("������ ���������� ������ �������������� ��������������.\n\n����� ������: " + 
                        (System.String)cmd.Parameters["@ERROR_MESSAGE"].Value, "��������",
                        System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                }
                cmd.Dispose();
            }
            catch (System.Exception e)
            {
                // ���������� ����������
                DBTransaction.Rollback();
                DevExpress.XtraEditors.XtraMessageBox.Show(
                "������ ���������� ������ �������������� ��������������.\n\n����� ������: " + e.Message, "��������",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
			finally // ������� ���������� �������
            {
                DBConnection.Close();
            }
            return bRet;
        }

        /// <summary>
        /// ���������� ������ �������������� �������������� � �������������� ���������� �������������
        /// </summary>
        /// <param name="objProfile">�������</param>
        /// <param name="uuidBudgetDepID">�� ���������� �������������</param>
        /// <param name="strErr">����� ������</param>
        /// <returns>������ �������� ������ "CUser"</returns>
        public static List<CUser> GetBudgetDepAdvManagerList(UniXP.Common.CProfile objProfile, System.Guid uuidBudgetDepID,
            ref System.String strErr )
        {
            List<CUser> objManagerList = new List<CUser>();
            
            System.Data.SqlClient.SqlConnection DBConnection = objProfile.GetDBSource();
            if (DBConnection == null)
            {
                strErr += ("\n����������� ���������� � ����� ������.");

                return objManagerList;
            }

            try
            {
                // ���������� � �� ��������, ����������� ������� �� ������� ������
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand() 
                { 
                    Connection = DBConnection, 
                    CommandType = System.Data.CommandType.StoredProcedure, 
                    CommandText = System.String.Format("[{0}].[dbo].[sp_GetBudgetDepAdvancedManagerList]", objProfile.GetOptionsDllDBName()) 
                };
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ((System.Byte)(0)), ((System.Byte)(0)), "", System.Data.DataRowVersion.Current, null));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@GUID_ID", System.Data.SqlDbType.UniqueIdentifier, 4));
                cmd.Parameters["@GUID_ID"].Value = uuidBudgetDepID;
                System.Data.SqlClient.SqlDataReader rs = cmd.ExecuteReader();
                if (rs.HasRows)
                {
                    // ����� ������ ��������
                    CUser objManager = null;
                    while (rs.Read())
                    {
                        objManager = new CUser(System.Convert.ToInt32(rs["ErpBudgetUserID"]), System.Convert.ToInt32( rs["UniXPUserID"] ),
                            System.Convert.ToString( rs["strLastName"] ), System.Convert.ToString(rs["strFirstName"]) );

                        objManager.IsBudgetDepManager = System.Convert.ToBoolean(rs["IsManager"]);
                        objManager.IsBudgetDepCoordinator = System.Convert.ToBoolean(rs["IsCoordinator"]);
                        objManager.IsBudgetDepController = System.Convert.ToBoolean(rs["IsController"]);

                        objManagerList.Add(objManager);
                    }
                }
                rs.Close();
                rs.Dispose();

                // ������������� ������������ ���� �������������
                if ((objManagerList != null) && (objManagerList.Count > 0))
                {
                    CDynamicRight objDynamicRight = new CDynamicRight();
                    foreach (CUser objUser in objManagerList)
                    {
                        objUser.DynamicRightsList = objDynamicRight.GetDynamicRightsList(objProfile, objUser.ulID);
                    }
                    objDynamicRight = null;
                }

                cmd.Dispose();
            }
            catch (System.Exception f)
            {
                strErr += ("\n�� ������� �������� ������ �������������� �������������� �������������.\n\n����� ������: " + f.Message);
            }
            finally // ������� ���������� �������
            {
                DBConnection.Close();
            }

            return objManagerList;
        }
        /// <summary>
        /// ��������� � ���� ������ ������ �������������� � �������������� ��������������
        /// ��� ���������� �������������
        /// </summary>
        /// <param name="objProfile">�������</param>
        /// <param name="uuidBudgetDepID">�� ���������� �������������</param>
        /// <param name="objManagerList">������ �������������</param>
        /// <param name="strErr">����� ������</param>
        /// <returns>true - ������� ���������� ��������; false - ������</returns>
        public static System.Boolean SaveBudgetDepManagerList(UniXP.Common.CProfile objProfile, System.Guid uuidBudgetDepID,
            List<CUser> objManagerList, ref System.String strErr)
        {
            System.Boolean bRet = false;
            // ���������� ������������� �� ������ ���� ������
            if (uuidBudgetDepID == System.Guid.Empty)
            {
                strErr += (String.Format("\n������������ �������� ����������� �������������� �������������.\n��: {0}", uuidBudgetDepID));
                return bRet;
            }

            System.Data.SqlClient.SqlConnection DBConnection = objProfile.GetDBSource();
            if (DBConnection == null)
            {
                strErr += ("\n����������� ���������� � ����� ������.");
                return bRet;
            }
            System.Data.SqlClient.SqlTransaction DBTransaction = DBConnection.BeginTransaction();
            try
            {
                // ���������� � �� ��������, ����������� ������� �� �������� ������
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
                cmd.Connection = DBConnection;
                cmd.Transaction = DBTransaction;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = System.String.Format("[{0}].[dbo].[sp_DeleteBudgetDepAdvancedManagerList]", objProfile.GetOptionsDllDBName());
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ((System.Byte)(0)), ((System.Byte)(0)), "", System.Data.DataRowVersion.Current, null));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@GUID_ID", System.Data.SqlDbType.UniqueIdentifier));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ERROR_NUMBER", System.Data.SqlDbType.Int, 8, System.Data.ParameterDirection.Output, false, ((System.Byte)(0)), ((System.Byte)(0)), "", System.Data.DataRowVersion.Current, null));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ERROR_MESSAGE", System.Data.SqlDbType.NVarChar, 4000));
                cmd.Parameters["@ERROR_MESSAGE"].Direction = System.Data.ParameterDirection.Output;
                cmd.Parameters["@GUID_ID"].Value = uuidBudgetDepID;
                cmd.ExecuteNonQuery();
                System.Int32 iRet = (System.Int32)cmd.Parameters["@RETURN_VALUE"].Value;
                if (iRet == 0)
                {
                    if( ( objManagerList != null ) && (objManagerList.Count > 0) )
                    {
                        cmd.CommandText = System.String.Format("[{0}].[dbo].[sp_AddBudgetDepAdvancedManager]", objProfile.GetOptionsDllDBName());
                        cmd.Parameters.Clear();
                        cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ((System.Byte)(0)), ((System.Byte)(0)), "", System.Data.DataRowVersion.Current, null));
                        cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@BUDGETDEP_GUID_ID", System.Data.SqlDbType.UniqueIdentifier));
                        cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@USER_ID", System.Data.SqlDbType.Int));
                        cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Rights_ID", System.Data.SqlDbType.Int));

                        cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ERROR_NUMBER", System.Data.SqlDbType.Int, 8, System.Data.ParameterDirection.Output, false, ((System.Byte)(0)), ((System.Byte)(0)), "", System.Data.DataRowVersion.Current, null));
                        cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ERROR_MESSAGE", System.Data.SqlDbType.NVarChar, 4000));

                        cmd.Parameters["@ERROR_MESSAGE"].Direction = System.Data.ParameterDirection.Output;
                        cmd.Parameters["@BUDGETDEP_GUID_ID"].Value = uuidBudgetDepID;

                        foreach (CUser objUser in objManagerList)
                        {
                            cmd.Parameters["@USER_ID"].Value = objUser.ulID;
                            
                            // ��������, �������� �� ������������ �������������� ��������������
                            if (objUser.IsBudgetDepManager == true)
                            {
                                cmd.Parameters["@Rights_ID"].Value = objUser.DynamicRightsList.FindByName( ERP_Budget.Global.Consts.strDRManager ).ID;
                                cmd.ExecuteNonQuery();
                                iRet = (System.Int32)cmd.Parameters["@RETURN_VALUE"].Value;
                            }

                            if (iRet == 0)
                            {
                                // ��������, �������� �� ������������ ��������������
                                if (objUser.IsBudgetDepCoordinator == true)
                                {
                                    cmd.Parameters["@Rights_ID"].Value = objUser.DynamicRightsList.FindByName(ERP_Budget.Global.Consts.strDRCoordinator).ID;
                                    cmd.ExecuteNonQuery();
                                    iRet = (System.Int32)cmd.Parameters["@RETURN_VALUE"].Value;
                                }

                                if (iRet == 0)
                                {
                                    // ��������, �������� �� ������������ �����������
                                    if (objUser.IsBudgetDepController == true)
                                    {
                                        cmd.Parameters["@Rights_ID"].Value = objUser.DynamicRightsList.FindByName(ERP_Budget.Global.Consts.strDRInspector).ID;
                                        cmd.ExecuteNonQuery();
                                        iRet = (System.Int32)cmd.Parameters["@RETURN_VALUE"].Value;
                                    }
                                    if (iRet != 0)
                                    {
                                        // ���������� ����������
                                        DBTransaction.Rollback();
                                        strErr += (System.Convert.ToString(cmd.Parameters["@ERROR_MESSAGE"].Value));
                                        break;
                                    }
                                }
                                else
                                {
                                    // ���������� ����������
                                    DBTransaction.Rollback();
                                    strErr += (System.Convert.ToString(cmd.Parameters["@ERROR_MESSAGE"].Value));
                                    break;
                                }

                            }
                            else
                            {
                                // ���������� ����������
                                DBTransaction.Rollback();
                                strErr += ( System.Convert.ToString( cmd.Parameters["@ERROR_MESSAGE"].Value ) );
                                break;
                            }

                        }
                    }
                    if (iRet == 0)
                    {
                        // ������������ ����������
                        DBTransaction.Commit();
                        bRet = true;
                    }
                }
                else
                {
                    // ���������� ����������
                    DBTransaction.Rollback();
                    strErr += (String.Format("������ ���������� ������ �������������� ��������������.\n\n{0}", System.Convert.ToString(cmd.Parameters["@ERROR_MESSAGE"].Value)));
                }
                cmd.Dispose();
            }
            catch (System.Exception e)
            {
                // ���������� ����������
                DBTransaction.Rollback();
                strErr += (String.Format("������ ���������� ������ �������������� ��������������.\n\n{0}", e.Message) );
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
