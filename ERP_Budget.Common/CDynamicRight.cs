using System;
using System.Collections.Generic;
using System.Text;

namespace ERP_Budget.Common
{
    /// <summary>
    /// ����� "������������ �����"
    /// </summary>
    public class CDynamicRight : IBaseListItem
    {
        #region ����������, ��������, ���������
        /// <summary>
        /// ���������� �������������
        /// </summary>
        private int m_iID;
        /// <summary>
        /// ����
        /// </summary>
        private string m_strRole;
        /// <summary>
        /// ��������
        /// </summary>
        private string m_strDescription;

        /// <summary>
        /// ���������� �������������
        /// </summary>
        public int ID
        {
            get
            {
                return m_iID;
            }
            set
            {
                m_iID = value;
            }
        }

        /// <summary>
        /// ����
        /// </summary>
        public string Role
        {
            get
            {
                return m_strRole;
            }
            set
            {
                m_strRole = value;
            }
        }

        /// <summary>
        /// ��������
        /// </summary>
        public string Description
        {
            get
            {
                return m_strDescription;
            }
            set
            {
                m_strDescription = value;
            }
        }
        /// <summary>
        /// ���������
        /// </summary>
        public System.Boolean IsEnable { get; set; }
        #endregion

        public CDynamicRight()
        {
            m_iID = 0;
            m_strRole = "";
            m_strDescription = "";
            IsEnable = false;
        }

        public CDynamicRight( System.Int32 iID, System.String strName, System.String strRole, System.String strDescription )
        {
            this.m_iID = iID;
            this.m_strName = strName;
            this.m_strRole = strRole;
            this.m_strDescription = strDescription;
            IsEnable = false;
        }

        /// <summary>
        /// ���������� ������ ������� CDynamicRight
        /// </summary>
        /// <param name="objProfile">�������</param>
        /// <param name="iUserID">������������� ������������</param>
        /// <returns>true - �������� �������������; false - ������</returns>
        public CBaseList<CDynamicRight> GetDynamicRightsList(UniXP.Common.CProfile objProfile, 
            System.Int32 iUserID )
        {
            CBaseList<CDynamicRight> objList = new CBaseList<CDynamicRight>();

            System.Data.SqlClient.SqlConnection DBConnection = objProfile.GetDBSource();
            if( DBConnection == null ) { return objList; }

            try
            {
                // ���������� � �� ��������, ����������� ������� �� ������� ������
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
                cmd.Connection = DBConnection;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = System.String.Format( "[{0}].[dbo].[sp_GetUserRights]", objProfile.GetOptionsDllDBName() );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ( ( System.Byte )( 0 ) ), ( ( System.Byte )( 0 ) ), "", System.Data.DataRowVersion.Current, null ) );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@ulUserID", System.Data.SqlDbType.Int, 4 ) );
                cmd.Parameters[ "@ulUserID" ].Value = iUserID;
                System.Data.SqlClient.SqlDataReader rs = cmd.ExecuteReader();
                if( rs.HasRows )
                {
                    // ����� ������ ��������
                    CDynamicRight objDynamicRight = null;
                    while( rs.Read() )
                    {
                        objDynamicRight = new CDynamicRight();
                        objDynamicRight.m_strName = rs.GetString( 4 );
                        objDynamicRight.m_strDescription = rs.GetString( 5 );
                        objDynamicRight.m_iID = rs.GetInt32( 6 );
                        objDynamicRight.m_strRole = rs.GetString( 7 );
                        objDynamicRight.IsEnable = System.Convert.ToBoolean(rs["bState"]);
                        objList.AddItemToList( objDynamicRight );
                    }
                }
                else
                {
                    //DevExpress.XtraEditors.XtraMessageBox.Show( 
                    //"�� ������� �������� ������ ������� CDynamicRight.\n� �� �� ������� ����������.", "��������",
                    //System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning );

                }
                cmd.Dispose();
                rs.Dispose();
            }
            catch( System.Exception e )
            {
                DevExpress.XtraEditors.XtraMessageBox.Show( 
                "�� ������� �������� ������ ������� CDynamicRight.\n" + e.Message, "��������",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
            }
            finally // ������� ���������� �������
            {
                DBConnection.Close();
            }
            return objList;
        }
        /// <summary>
        /// ���������� ������ ������������ ���� ������������ � ������� "������" (CDynamicRight)
        /// </summary>
        /// <param name="objProfile">�������</param>
        /// <param name="iUserID">�� ������������ � ������� "������"</param>
        /// <param name="strErr">��������� �� ������</param>
        /// <returns>������ ������������ ���� ������������ � ������� "������"</returns>
        public static List<CDynamicRight> GetDynamicRightsList(UniXP.Common.CProfile objProfile,
            System.Int32 iUserID, ref System.String strErr)
        {
            List<CDynamicRight> objList = new List<CDynamicRight>();

            System.Data.SqlClient.SqlConnection DBConnection = objProfile.GetDBSource();
            if (DBConnection == null) { return objList; }

            try
            {
                // ���������� � �� ��������, ����������� ������� �� ������� ������
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
                cmd.Connection = DBConnection;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = System.String.Format("[{0}].[dbo].[sp_GetUserRights]", objProfile.GetOptionsDllDBName());
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ((System.Byte)(0)), ((System.Byte)(0)), "", System.Data.DataRowVersion.Current, null));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ulUserID", System.Data.SqlDbType.Int, 4));
                cmd.Parameters["@ulUserID"].Value = iUserID;
                System.Data.SqlClient.SqlDataReader rs = cmd.ExecuteReader();
                if (rs.HasRows)
                {
                    // ����� ������ ��������
                    CDynamicRight objDynamicRight = null;
                    while (rs.Read())
                    {
                        objDynamicRight = new CDynamicRight();
                        objDynamicRight.m_strName = System.Convert.ToString( rs["strName"] );
                        objDynamicRight.m_strDescription = ((rs["strDescription"] != System.DBNull.Value) ? System.Convert.ToString(rs["strDescription"]) : System.String.Empty);
                        objDynamicRight.m_iID = System.Convert.ToInt32( rs["iID"] );
                        objDynamicRight.m_strRole = System.Convert.ToString(rs["strRole"]);
                        objDynamicRight.IsEnable = System.Convert.ToBoolean(rs["bState"]);
                        objList.Add(objDynamicRight);
                    }
                }
                cmd.Dispose();
                rs.Dispose();
            }
            catch (System.Exception e)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(
                "�� ������� �������� ������ ������� CDynamicRight.\n" + e.Message, "��������",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
            finally // ������� ���������� �������
            {
                DBConnection.Close();
            }
            return objList;
        }
        /// <summary>
        /// ���������� ������ ������� CDynamicRight
        /// </summary>
        /// <param name="objProfile">�������</param>
        /// <returns>true - �������� �������������; false - ������</returns>
        public static System.Collections.Generic.List<CDynamicRight> GetDynamicRightsList( UniXP.Common.CProfile objProfile )
        {
            System.Collections.Generic.List<CDynamicRight> objList = new System.Collections.Generic.List<CDynamicRight>();

            System.Data.SqlClient.SqlConnection DBConnection = objProfile.GetDBSource();
            if( DBConnection == null ) { return objList; }

            try
            {
                // ���������� � �� ��������, ����������� ������� �� ������� ������
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
                cmd.Connection = DBConnection;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = System.String.Format( "[{0}].[dbo].[sp_GetRight]", objProfile.GetOptionsDllDBName() );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ( ( System.Byte )( 0 ) ), ( ( System.Byte )( 0 ) ), "", System.Data.DataRowVersion.Current, null ) );
                System.Data.SqlClient.SqlDataReader rs = cmd.ExecuteReader();
                if( rs.HasRows )
                {
                    // ����� ������ ��������
                    System.String strDescription = "";
                    while( rs.Read() )
                    {
                        strDescription = ( rs[ 2 ] == System.DBNull.Value ) ? "" : rs.GetString( 2 );
                        objList.Add( new CDynamicRight( rs.GetInt32( 0 ), rs.GetString( 1 ), strDescription, rs.GetString( 3 ) ) );
                    }
                }
                cmd.Dispose();
                rs.Dispose();
            }
            catch( System.Exception e )
            {
                DevExpress.XtraEditors.XtraMessageBox.Show( 
                "�� ������� �������� ������ ������� CDynamicRight.\n����� ������: " + e.Message, "��������",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
            }
            finally // ������� ���������� �������
            {
                DBConnection.Close();
            }
            return objList;
        }

        /// <summary>
        /// ���������� ������� ������������ ����� �� ������ ���������
        /// </summary>
        /// <param name="objProfile">�������</param>
        /// <returns>������������ ������������� �����</returns>
        public static System.String GetMainDynamicRight( UniXP.Common.CProfile objProfile, CBudgetDep objBudgetDep )
        {
            System.String strDRName = "";
            try
            {
                // ������������ ����� "������������� �������"
                if( objProfile.GetClientsRight().GetState( ERP_Budget.Global.Consts.strDRCoordinator ) )
                {
                    strDRName = ERP_Budget.Global.Consts.strDRCoordinator;
                    return strDRName;
                }
                // ������������ ����� "������������� �������"
                // ��� ������� ���... �� ��� �����!
                // ��� ������ ������������� �� ����� � �������������, � ��� ������� ������� ���������
                if( objProfile.GetClientsRight().GetState( ERP_Budget.Global.Consts.strDRManager ) )
                {
                    if( objBudgetDep == null )
                    {
                        strDRName = ERP_Budget.Global.Consts.strDRManager;
                    }
                    else
                    {
                        if( objBudgetDep.IsBudgetDepManager( objProfile.m_nSQLUserID ) == true )
                        {
                            strDRName = ERP_Budget.Global.Consts.strDRManager;
                        }
                        else
                        {
                            strDRName = ERP_Budget.Global.Consts.strDRInitiator;
                        }
                    }
                    return strDRName;
                }
                // ������������ ����� "���������"
                if( objProfile.GetClientsRight().GetState( ERP_Budget.Global.Consts.strDRInspector ) )
                {
                    strDRName = ERP_Budget.Global.Consts.strDRInspector;
                    return strDRName;
                }
                // ������������ ����� "���������"
                if (objProfile.GetClientsRight().GetState(ERP_Budget.Global.Consts.strDRAccountant))
                {
                    strDRName = ERP_Budget.Global.Consts.strDRAccountant;
                    return strDRName;
                }
                // ������������ ����� "������"
                if( objProfile.GetClientsRight().GetState( ERP_Budget.Global.Consts.strDRCashier ) )
                {
                    strDRName = ERP_Budget.Global.Consts.strDRCashier;
                    return strDRName;
                }
                // ������������ ����� "���������"
                if( objProfile.GetClientsRight().GetState( ERP_Budget.Global.Consts.strDRInitiator ) )
                {
                    strDRName = ERP_Budget.Global.Consts.strDRInitiator;
                    return strDRName;
                }
                // ������������ ����� "�����������"
                if( objProfile.GetClientsRight().GetState( ERP_Budget.Global.Consts.strDRLook ) )
                {
                    strDRName = ERP_Budget.Global.Consts.strDRLook;
                    return strDRName;
                }
            }
            catch( System.Exception f )
            {
                DevExpress.XtraEditors.XtraMessageBox.Show( 
                "������ ������ ������������� �����.\n\n����� ������: " + f.Message, "��������",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
            }

            return strDRName;
        }
        /// <summary>
        /// ���������� ������������ ����� �� ��� ������������
        /// </summary>
        /// <param name="objProfile">�������</param>
        /// <param name="strDRName">������������ ������������� �����</param>
        /// <returns>������ ������ "������������ �����"</returns>
        public static CDynamicRight Init( UniXP.Common.CProfile objProfile, System.String strDRName )
        {
            CDynamicRight objDynamicRight = null;
            System.Data.SqlClient.SqlConnection DBConnection = objProfile.GetDBSource();
            if( DBConnection == null ) { return objDynamicRight; }

            try
            {
                // ���������� � �� ��������, ����������� ������� �� ������� ������
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
                cmd.Connection = DBConnection;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = System.String.Format( "[{0}].[dbo].[sp_GetRightByName]", objProfile.GetOptionsDllDBName() );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ( ( System.Byte )( 0 ) ), ( ( System.Byte )( 0 ) ), "", System.Data.DataRowVersion.Current, null ) );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@RIGHT_NAME", System.Data.SqlDbType.VarChar, 50 ) );
                cmd.Parameters[ "@RIGHT_NAME" ].Value = strDRName;
                System.Data.SqlClient.SqlDataReader rs = cmd.ExecuteReader();
                if( rs.HasRows )
                {
                    // ����� ������ ��������
                    rs.Read();
                    System.String strDescription = ( rs[ 2 ] == System.DBNull.Value ) ? "" : rs.GetString( 2 );
                    objDynamicRight = new CDynamicRight( rs.GetInt32( 0 ), rs.GetString( 1 ), strDescription, rs.GetString( 3 ) );
                }
                else
                {
                    DevExpress.XtraEditors.XtraMessageBox.Show( 
                    "�� ������� �������� ���������� � ������������ �����: " + strDRName, "��������",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
                }
                cmd.Dispose();
                rs.Dispose();
            }
            catch( System.Exception e )
            {
                DevExpress.XtraEditors.XtraMessageBox.Show( 
                "�� ������� �������� ���������� � ������������ �����: " + strDRName + "\n\n����� ������: " + e.Message, "��������",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
            }
            finally // ������� ���������� �������
            {
                DBConnection.Close();
            }
            return objDynamicRight;
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
