using System;
using System.Collections.Generic;
using System.Text;

namespace ERP_Budget.Common
{
    /// <summary>
    /// ����� "������������"
    /// </summary>
    public class CUser : IBaseListItem
    {
        #region ����������, ��������, ���������

        /// <summary>
        /// ���������� ������������� � ERP_Budget
        /// </summary>
        private int m_ulID;
        /// <summary>
        /// ���������� ������������� � UniXP
        /// </summary>
        private int m_ulUniXPID;
        /// <summary>
        /// ���������� ������������� ���������� �������
        /// </summary>
        private System.Guid m_uuidOptionID;
        /// <summary>
        /// �������
        /// </summary>
        private System.String m_strUserLastName;
        /// <summary>
        /// ���
        /// </summary>
        private string m_strUserFirstName;
        /// <summary>
        /// ��������
        /// </summary>
        private string m_strUserMiddleName;
        /// <summary>
        /// ������ email-�������
        /// </summary>
        private CBaseList<CEmail> m_objEmailList;
        /// <summary>
        /// ������ ���������� ����������
        /// </summary>
        private CBaseList<CCompanyPost> m_objEmployeePostList;
        /// <summary>
        /// ������ ������������ ����
        /// </summary>
        private CBaseList<CDynamicRight> m_objDynamicRightsList;
        /// <summary>
        /// ���������� ������������� � ERP_Budget
        /// </summary>
        public int ulID
        {
            get { return m_ulID; }
            set { m_ulID = value; }
        }
        /// <summary>
        /// ���������� ������������� � UniXP
        /// </summary>
        public int ulUniXPID
        {
            get
            {
                return m_ulUniXPID;
            }
            set
            {
                m_ulUniXPID = value;
            }
        }
        /// <summary>
        /// ���������� ������������� ���������� �������
        /// </summary>
        public System.Guid uuidOptionID
        {
            get
            {
                return m_uuidOptionID;
            }
            set
            {
                m_uuidOptionID = value;
            }
        }
        /// <summary>
        /// �������
        /// </summary>
        public string UserLastName
        {
            get
            {
                return m_strUserLastName;
            }
            set
            {
                m_strUserLastName = value;
            }
        }
        /// <summary>
        /// ���
        /// </summary>
        public string UserFirstName
        {
            get
            {
                return m_strUserFirstName;
            }
            set
            {
                m_strUserFirstName = value;
            }
        }
        /// <summary>
        /// ��������
        /// </summary>
        public string UserMiddleName
        {
            get
            {
                return m_strUserMiddleName;
            }
            set
            {
                m_strUserFirstName = value;
            }
        }
        /// <summary>
        /// ������� ���
        /// </summary>
        public System.String UserFullName
        {
            get { return (UserLastName + " " + UserFirstName); }
        }
        /// <summary>
        /// ��������
        /// </summary>
        private System.String m_strUserDescription;
        /// <summary>
        /// ��������
        /// </summary>
        public string UserDescription
        {
            get
            {
                return m_strUserDescription;
            }
            set
            {
                m_strUserDescription = value;
            }
        }
        /// <summary>
        /// ������ email-�������
        /// </summary>
        public ERP_Budget.Common.CBaseList<ERP_Budget.Common.CEmail> EmailList
        {
            get
            {
                return m_objEmailList;
            }
            set
            {
                m_objEmailList = value;
            }
        }
        /// <summary>
        /// ������ ���������� ����������
        /// </summary>
        public ERP_Budget.Common.CBaseList<ERP_Budget.Common.CCompanyPost> EmployeePostList
        {
            get
            {
                return m_objEmployeePostList;
            }
            set
            {
                m_objEmployeePostList = value;
            }
        }
        /// <summary>
        /// ������ ������������ ����
        /// </summary>
        public ERP_Budget.Common.CBaseList<ERP_Budget.Common.CDynamicRight> DynamicRightsList
        {
            get
            {
                return m_objDynamicRightsList;
            }
            set
            {
                m_objDynamicRightsList = value;
            }
        }
        /// <summary>
        /// ������ ��������� �������� ��� ��������� ����������
        /// </summary>
        private List<CBudgetDocEvent> m_objBudgetDocEventList;
        /// <summary>
        /// ������ ��������� �������� ��� ��������� ����������
        /// </summary>
        public List<CBudgetDocEvent> BudgetDocEventList
        {
            get { return m_objBudgetDocEventList; }
            set { m_objBudgetDocEventList = value; }
        }

        public System.Boolean IsBudgetDepManager { get; set; }
        public System.Boolean IsBudgetDepCoordinator { get; set; }
        public System.Boolean IsBudgetDepController { get; set; }
        public System.Boolean IsBlocked { get; set; }

        #endregion

        #region ������������
        public CUser()
        {
            this.m_uuidID = System.Guid.Empty;
            this.m_strName = "";
            this.m_objDynamicRightsList = null;
            this.m_objEmailList = null;
            this.m_objEmployeePostList = null;
            this.m_strUserFirstName = "";
            this.m_strUserLastName = "";
            this.m_strUserMiddleName = "";
            this.m_strUserDescription = "";
            this.m_ulID = 0;
            this.m_ulUniXPID = 0;
            this.m_uuidOptionID = System.Guid.Empty;
            this.BudgetDocEventList = new List<CBudgetDocEvent>();
            IsBudgetDepManager = false;
            IsBudgetDepCoordinator = false;
            IsBudgetDepController = false;
            IsBlocked = false;
        }

        public CUser(System.Int32 ulID, System.Int32 ulUniXPID, System.String strUserLastName,
            System.String strUserFirstName)
        {
            this.m_uuidID = System.Guid.Empty;
            this.m_strName = "";
            this.m_objDynamicRightsList = null;
            this.m_objEmailList = null;
            this.m_objEmployeePostList = null;
            this.m_strUserFirstName = strUserFirstName;
            this.m_strUserLastName = strUserLastName;
            this.m_strUserMiddleName = "";
            this.m_strUserDescription = "";
            this.m_ulID = ulID;
            this.m_ulUniXPID = ulUniXPID;
            this.m_uuidOptionID = System.Guid.Empty;
            this.BudgetDocEventList = new List<CBudgetDocEvent>();
            IsBudgetDepManager = false;
            IsBudgetDepCoordinator = false;
            IsBudgetDepController = false;
            IsBlocked = false;
        }
        #endregion

        #region ������ �������������
        /// <summary>
        /// ���������� ������ ������� CUser
        /// </summary>
        /// <param name="objProfile">�������</param>
        /// <returns>true - �������� �������������; false - ������</returns>
        public CBaseList<CUser> GetUserList(UniXP.Common.CProfile objProfile,
            System.Boolean bInitDynamicRightsList = true,
            System.Boolean bInitEmailList = true,
            System.Boolean bInitEmployeePostList = true )
        {
            CBaseList<CUser> objList = new CBaseList<CUser>();

            System.Data.SqlClient.SqlConnection DBConnection = objProfile.GetDBSource();
            if (DBConnection == null) { return objList; }

            try
            {
                // ���������� � �� ��������, ����������� ������� �� ������� ������
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
                cmd.Connection = DBConnection;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = System.String.Format("[{0}].[dbo].[sp_GetUser]", objProfile.GetOptionsDllDBName());
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ((System.Byte)(0)), ((System.Byte)(0)), "", System.Data.DataRowVersion.Current, null));
                System.Data.SqlClient.SqlDataReader rs = cmd.ExecuteReader();
                if (rs.HasRows)
                {
                    // ����� ������ ��������
                    CUser objUser = null;
                    CDynamicRight objDynamicRight = new CDynamicRight();
                    CEmail objEmail = new CEmail();
                    CCompanyPost objCompanyPost = new CCompanyPost();
                    while (rs.Read())
                    {

                        objUser = new CUser();
                        objUser.m_strName = rs.GetString(0);
                        objUser.m_strUserFirstName = rs.GetString(1);
                        objUser.m_strUserMiddleName = rs.GetString(2);
                        objUser.m_strUserLastName = rs.GetString(3);
                        objUser.m_strUserDescription = rs.GetString(4);
                        objUser.m_ulID = rs.GetInt32(7);
                        objUser.m_ulUniXPID = rs.GetInt32(6);
                        objUser.m_uuidOptionID = rs.GetGuid(5);
                        objUser.IsBlocked = System.Convert.ToBoolean(rs["IsUserBlocked"]);
                        if (bInitDynamicRightsList == true)
                        {
                            objUser.m_objDynamicRightsList = objDynamicRight.GetDynamicRightsList(objProfile, objUser.m_ulID);
                        }
                        if (bInitEmailList == true)
                        {
                            objUser.m_objEmailList = objEmail.GetEmailList(objProfile, objUser.m_ulID);
                        }
                        if (bInitEmployeePostList == true)
                        {
                            objUser.m_objEmployeePostList = objCompanyPost.GetUserCompanyPostList(objProfile, objUser.m_ulID);
                        }
                        objList.AddItemToList(objUser);
                    }
                    objDynamicRight = null;
                    objEmail = null;
                    objCompanyPost = null;
                }
                else
                {
                    DevExpress.XtraEditors.XtraMessageBox.Show(
                    "�� ������� �������� ������ ������� CUser.\n� �� �� ������� ����������.", "��������",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);

                }
                cmd.Dispose();
                rs.Dispose();
            }
            catch (System.Exception e)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(
                "�� ������� �������� ������ ������� CUser.\n" + e.Message, "��������",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
            finally // ������� ���������� �������
            {
                DBConnection.Close();
            }
            return objList;
        }

        /// <summary>
        /// ���������� ������ ������� CUser
        /// </summary>
        /// <param name="objProfile">�������</param>
        /// <returns>true - �������� �������������; false - ������</returns>
        public List<CUser> GetBudgetUserList(UniXP.Common.CProfile objProfile,
            System.Boolean bInitDynamicRightsList = true,
            System.Boolean bInitEmailList = true,
            System.Boolean bInitEmployeePostList = true)
        {
            List<CUser> objList = new List<CUser>();

            System.Data.SqlClient.SqlConnection DBConnection = objProfile.GetDBSource();
            if (DBConnection == null) { return objList; }

            try
            {
                // ���������� � �� ��������, ����������� ������� �� ������� ������
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
                cmd.Connection = DBConnection;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = System.String.Format("[{0}].[dbo].[sp_GetUser]", objProfile.GetOptionsDllDBName());
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ((System.Byte)(0)), ((System.Byte)(0)), "", System.Data.DataRowVersion.Current, null));
                System.Data.SqlClient.SqlDataReader rs = cmd.ExecuteReader();
                if (rs.HasRows)
                {
                    // ����� ������ ��������
                    CUser objUser = null;
                    CDynamicRight objDynamicRight = new CDynamicRight();
                    CEmail objEmail = new CEmail();
                    CCompanyPost objCompanyPost = new CCompanyPost();
                    while (rs.Read())
                    {

                        objUser = new CUser();
                        objUser.m_strName = rs.GetString(0);
                        objUser.m_strUserFirstName = rs.GetString(1);
                        objUser.m_strUserMiddleName = rs.GetString(2);
                        objUser.m_strUserLastName = rs.GetString(3);
                        objUser.m_strUserDescription = rs.GetString(4);
                        objUser.m_ulID = rs.GetInt32(7);
                        objUser.m_ulUniXPID = rs.GetInt32(6);
                        objUser.m_uuidOptionID = rs.GetGuid(5);
                        objUser.IsBlocked = System.Convert.ToBoolean(rs["IsUserBlocked"]);
                        if (bInitDynamicRightsList == true)
                        {
                            objUser.m_objDynamicRightsList = objDynamicRight.GetDynamicRightsList(objProfile, objUser.m_ulID);
                        }
                        if (bInitEmailList == true)
                        {
                            objUser.m_objEmailList = objEmail.GetEmailList(objProfile, objUser.m_ulID);
                        }
                        if (bInitEmployeePostList == true)
                        {
                            objUser.m_objEmployeePostList = objCompanyPost.GetUserCompanyPostList(objProfile, objUser.m_ulID);
                        }
                        objList.Add(objUser);
                    }
                    objDynamicRight = null;
                    objEmail = null;
                    objCompanyPost = null;
                }
                else
                {
                    DevExpress.XtraEditors.XtraMessageBox.Show(
                    "�� ������� �������� ������ ������� CUser.\n� �� �� ������� ����������.", "��������",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);

                }
                cmd.Dispose();
                rs.Dispose();
            }
            catch (System.Exception e)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(
                "�� ������� �������� ������ ������� CUser.\n" + e.Message, "��������",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
            finally // ������� ���������� �������
            {
                DBConnection.Close();
            }
            return objList;
        }
        
        /// <summary>
        /// ���������� ������ ������� CUser
        /// </summary>
        /// <param name="objProfile">�������</param>
        /// <returns>������ �������</returns>
        public static List<CUser> GetUsersList(UniXP.Common.CProfile objProfile)
        {
            List<CUser> objList = new List<CUser>();

            System.Data.SqlClient.SqlConnection DBConnection = objProfile.GetDBSource();
            if (DBConnection == null) { return objList; }

            try
            {
                // ���������� � �� ��������, ����������� ������� �� ������� ������
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
                cmd.Connection = DBConnection;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = System.String.Format("[{0}].[dbo].[sp_GetUser]", objProfile.GetOptionsDllDBName());
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ((System.Byte)(0)), ((System.Byte)(0)), "", System.Data.DataRowVersion.Current, null));
                System.Data.SqlClient.SqlDataReader rs = cmd.ExecuteReader();
                if (rs.HasRows)
                {
                    // ����� ������ ��������
                    CUser objUser = null;
                    CDynamicRight objDynamicRight = new CDynamicRight();
                    CEmail objEmail = new CEmail();
                    CCompanyPost objCompanyPost = new CCompanyPost();
                    while (rs.Read())
                    {

                        objUser = new CUser();
                        objUser.m_strName = rs.GetString(0);
                        objUser.m_strUserFirstName = rs.GetString(1);
                        objUser.m_strUserMiddleName = rs.GetString(2);
                        objUser.m_strUserLastName = rs.GetString(3);
                        objUser.m_strUserDescription = rs.GetString(4);
                        objUser.m_ulID = rs.GetInt32(7);
                        objUser.m_ulUniXPID = rs.GetInt32(6);
                        objUser.m_uuidOptionID = rs.GetGuid(5);
                        objUser.m_objDynamicRightsList = objDynamicRight.GetDynamicRightsList(objProfile, objUser.m_ulID);
                        objUser.m_objEmailList = objEmail.GetEmailList(objProfile, objUser.m_ulID);
                        objUser.m_objEmployeePostList = objCompanyPost.GetUserCompanyPostList(objProfile, objUser.m_ulID);
                        objList.Add(objUser);
                    }
                    objDynamicRight = null;
                    objEmail = null;
                    objCompanyPost = null;
                }
                else
                {
                    DevExpress.XtraEditors.XtraMessageBox.Show(
                    "�� ������� �������� ������ ������� CUser.\n� �� �� ������� ����������.", "��������",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);

                }
                cmd.Dispose();
                rs.Dispose();
            }
            catch (System.Exception e)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(
                "�� ������� �������� ������ ������� CUser.\n" + e.Message, "��������",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
            finally // ������� ���������� �������
            {
                DBConnection.Close();
            }
            return objList;
        }
        /// <summary>
        /// ���������� ������ ������ ������������ �� ��������� ��������
        /// </summary>
        /// <param name="objProfile">�������</param>
        /// <returns>������ ������ ������������</returns>
        public static CUser GetUsersInfo(UniXP.Common.CProfile objProfile)
        {
            CUser objUser = new CUser();
            System.Data.SqlClient.SqlConnection DBConnection = objProfile.GetDBSource();
            if (DBConnection == null) { return objUser; }

            try
            {
                // ���������� � �� ��������, ����������� ������� �� ������� ������
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
                cmd.Connection = DBConnection;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = System.String.Format("[{0}].[dbo].[sp_GetUserByUniXPID]", objProfile.GetOptionsDllDBName());
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ((System.Byte)(0)), ((System.Byte)(0)), "", System.Data.DataRowVersion.Current, null));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ulUniXPID", System.Data.SqlDbType.Int, 4));
                cmd.Parameters["@ulUniXPID"].Value = objProfile.m_nSQLUserID;
                System.Data.SqlClient.SqlDataReader rs = cmd.ExecuteReader();
                if (rs.HasRows)
                {
                    rs.Read();
                    objUser.m_strName = rs.GetString(0);
                    objUser.m_strUserFirstName = rs.GetString(1);
                    objUser.m_strUserMiddleName = rs.GetString(2);
                    objUser.m_strUserLastName = rs.GetString(3);
                    objUser.m_strUserDescription = rs.GetString(4);
                    objUser.m_ulID = rs.GetInt32(7);
                    objUser.m_ulUniXPID = rs.GetInt32(6);
                    objUser.m_uuidOptionID = rs.GetGuid(5);
                }
                rs.Close();
                rs.Dispose();
                cmd.Dispose();
            }
            catch (System.Exception e)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(
                "�� ������� �������� ���������� � ������������.\n�� ������������: " + objProfile.m_nSQLUserID.ToString() + "\n\n����� ������: " + e.Message, "��������",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
            finally // ������� ���������� �������
            {
                DBConnection.Close();
            }
            return objUser;
        }

        #endregion

        #region Init
        /// <summary>
        /// ������������� ������� ������ CUser
        /// </summary>
        /// <param name="objProfile">�������</param>
        /// <param name="uuidID">���������� ������������� ������</param>
        /// <returns>true - �������� �������������; false - ������</returns>
        public override System.Boolean Init(UniXP.Common.CProfile objProfile, System.Guid uuidID)
        {
            System.Boolean bRet = false;

            return bRet;
        }
        /// <summary>
        /// ������������� ������� ������ CUser
        /// </summary>
        /// <param name="objProfile">�������</param>
        /// <param name="iUserID">���������� ������������� ������������</param>
        /// <returns>true - �������� �������������; false - ������</returns>
        public System.Boolean Init(UniXP.Common.CProfile objProfile, System.Int32 iUserID)
        {
            System.Boolean bRet = false;

            System.Data.SqlClient.SqlConnection DBConnection = objProfile.GetDBSource();
            if (DBConnection == null) { return bRet; }

            try
            {
                // ���������� � �� ��������, ����������� ������� �� ������� ������
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
                cmd.Connection = DBConnection;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = System.String.Format("[{0}].[dbo].[sp_GetUser]", objProfile.GetOptionsDllDBName());
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ((System.Byte)(0)), ((System.Byte)(0)), "", System.Data.DataRowVersion.Current, null));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ulUserID", System.Data.SqlDbType.Int, 4));
                cmd.Parameters["@ulUserID"].Value = iUserID;
                System.Data.SqlClient.SqlDataReader rs = cmd.ExecuteReader();
                if (rs.HasRows)
                {
                    // ����� ������ ��������, � ��� ��� ���������� ���� ������
                    rs.Read();

                    this.m_strName = rs.GetString(0);
                    this.m_strUserFirstName = rs.GetString(1);
                    this.m_strUserMiddleName = rs.GetString(2);
                    this.m_strUserLastName = rs.GetString(3);
                    this.m_strUserDescription = rs.GetString(4);
                    this.m_ulID = rs.GetInt32(7);
                    this.m_ulUniXPID = rs.GetInt32(6);
                    this.m_uuidOptionID = rs.GetGuid(5);

                    CDynamicRight objDynamicRight = new CDynamicRight();
                    this.m_objDynamicRightsList = objDynamicRight.GetDynamicRightsList(objProfile, this.m_ulID);
                    objDynamicRight = null;
                    
                    CEmail objEmail = new CEmail();
                    this.m_objEmailList = objEmail.GetEmailList(objProfile, this.m_ulID);
                    objEmail = null;
                    
                    CCompanyPost objCompanyPost = new CCompanyPost();
                    this.m_objEmployeePostList = objCompanyPost.GetUserCompanyPostList(objProfile, this.m_ulID);
                    objCompanyPost = null;
                    
                    bRet = true;
                }
                else
                {
                    DevExpress.XtraEditors.XtraMessageBox.Show(
                    "�� ������� ������������������� ����� CUser.\n� �� �� ������� ����������.", "��������",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);

                }
                cmd.Dispose();
                rs.Dispose();
            }
            catch (System.Exception e)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(
                "�� ������� ������������������� ����� CUser.\n" + e.Message, "��������",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
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
        public override System.Boolean Remove(UniXP.Common.CProfile objProfile)
        {
            return false;
        }

        #endregion

        #region Add
        /// <summary>
        /// �������� ������ � ��
        /// </summary>
        /// <param name="objProfile">�������</param>
        /// <returns>true - ������� ����������; false - ������</returns>
        public override System.Boolean Add(UniXP.Common.CProfile objProfile)
        {
            return false;
        }

        #endregion

        #region Update
        /// <summary>
        /// ��������� ��������� � ��
        /// </summary>
        /// <param name="objProfile">�������</param>
        /// <returns>true - ������� ����������; false - ������</returns>
        public override System.Boolean Update(UniXP.Common.CProfile objProfile)
        {
            System.Boolean bRet = false;
            System.Data.SqlClient.SqlConnection DBConnection = objProfile.GetDBSource();
            if (DBConnection == null) { return bRet; }
            System.Boolean bErrUpdate = false;
            System.Int32 iRet = -1;
            System.Data.SqlClient.SqlTransaction DBTransaction = DBConnection.BeginTransaction();
            try
            {
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
                cmd.Connection = DBConnection;
                cmd.Transaction = DBTransaction;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                // ������ ���������� � ��������� ��������
                // ������� ��� �������� ������ � ������������
                cmd.CommandText = System.String.Format("[{0}].[dbo].[sp_DeleteUserEmail]", objProfile.GetOptionsDllDBName());
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ((System.Byte)(0)), ((System.Byte)(0)), "", System.Data.DataRowVersion.Current, null));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ulUserID", System.Data.SqlDbType.Int, 4));
                cmd.Parameters["@ulUserID"].Value = this.ulID;
                cmd.ExecuteNonQuery();
                iRet = (System.Int32)cmd.Parameters["@RETURN_VALUE"].Value;
                if (iRet == 0)
                {
                    if (this.EmailList.GetCountItems() > 0)
                    {
                        //��������� � �� ������ �������� ������� ������������
                        cmd.Parameters.Clear();
                        cmd.CommandText = System.String.Format("[{0}].[dbo].[sp_AssignUserEmailByEmailAddress]", objProfile.GetOptionsDllDBName());
                        cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ((System.Byte)(0)), ((System.Byte)(0)), "", System.Data.DataRowVersion.Current, null));
                        cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@EMAIL_ADDRESS", System.Data.DbType.String));
                        cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ulUserID", System.Data.SqlDbType.Int, 4));
                        cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@bAssign", System.Data.SqlDbType.Bit, 1));
                        cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@EMAIL_MAIN", System.Data.SqlDbType.Bit, 1));
                        cmd.Parameters["@ulUserID"].Value = this.ulID;
                        cmd.Parameters["@bAssign"].Value = 1;
                        CEmail objEmail = null;
                        for (System.Int32 i = 0; i < this.EmailList.GetCountItems(); i++)
                        {
                            objEmail = this.EmailList.GetByIndex(i);
                            if (objEmail == null) { continue; }
                            try
                            {
                                cmd.Parameters["@EMAIL_ADDRESS"].Value = objEmail.Adress;
                                cmd.Parameters["@EMAIL_MAIN"].Value = objEmail.IsMain;
                                cmd.ExecuteNonQuery();
                                iRet = (System.Int32)cmd.Parameters["@RETURN_VALUE"].Value;
                                if (iRet != 0)
                                {
                                    bErrUpdate = true;
                                    switch (iRet)
                                    {
                                        case 1:
                                            DevExpress.XtraEditors.XtraMessageBox.Show("Email � ��������� ��������������� �� ������ \n" + this.m_uuidID.ToString(), "��������",
                                                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                                            break;
                                        case 2:
                                            DevExpress.XtraEditors.XtraMessageBox.Show("������������ � ��������� ��������������� �� ������ \n" + this.m_uuidID.ToString(), "��������",
                                                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                                            break;
                                        default:
                                            DevExpress.XtraEditors.XtraMessageBox.Show("������ ���������� ������������ email", "��������",
                                                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                                            break;
                                    }
                                    break;
                                }
                            }
                            catch (System.Exception e)
                            {
                                DevExpress.XtraEditors.XtraMessageBox.Show(
                                "������ ���������� email ������������.\nEmail: " +
                            objEmail.Adress + "\n" + e.Message, "��������",
                                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);
                                break;
                            }
                        }
                        bRet = true;
                    }
                }
                else
                {
                    bErrUpdate = true;
                    switch (iRet)
                    {
                        case 1:
                            DevExpress.XtraEditors.XtraMessageBox.Show("������������ � ��������� ��������������� �� ������ \n" + this.m_uuidID.ToString(), "��������",
                                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                            break;
                        default:
                            DevExpress.XtraEditors.XtraMessageBox.Show("������ �������� � ������������ email", "��������",
                                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                            break;
                    }
                }
                if (bErrUpdate)
                {
                    DBTransaction.Rollback();
                    DevExpress.XtraEditors.XtraMessageBox.Show("�� ����� ���������� ��������� � �� �������� ������.\n��� ��������� � �� ���� ��������.", "��������",
                        System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                }
                else
                {
                    // ������ ���������� � ����������� � ����������
                    cmd.Parameters.Clear();
                    cmd.CommandText = System.String.Format("[{0}].[dbo].[sp_DeleteUserCompany]", objProfile.GetOptionsDllDBName());
                    cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ((System.Byte)(0)), ((System.Byte)(0)), "", System.Data.DataRowVersion.Current, null));
                    cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ulUserID", System.Data.SqlDbType.Int, 4));
                    cmd.Parameters["@ulUserID"].Value = this.ulID;
                    cmd.ExecuteNonQuery();
                    iRet = (System.Int32)cmd.Parameters["@RETURN_VALUE"].Value;
                    if (iRet == 0)
                    {
                        if (this.EmployeePostList.GetCountItems() > 0)
                        {
                            //��������� � �� ������ �������� ������� ������������
                            cmd.Parameters.Clear();
                            cmd.CommandText = System.String.Format("[{0}].[dbo].[sp_AssignUserCompany]", objProfile.GetOptionsDllDBName());
                            cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ((System.Byte)(0)), ((System.Byte)(0)), "", System.Data.DataRowVersion.Current, null));
                            cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@COMPANY_GUID_ID", System.Data.SqlDbType.UniqueIdentifier, 4));
                            cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@EMPLOYEEPOST_GUID_ID", System.Data.SqlDbType.UniqueIdentifier, 4));
                            cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ulUserID", System.Data.SqlDbType.Int, 4));
                            cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@bAssign", System.Data.SqlDbType.Bit, 1));
                            cmd.Parameters["@ulUserID"].Value = this.ulID;
                            cmd.Parameters["@bAssign"].Value = 1;
                            CCompanyPost objCompanyPost = null;
                            for (System.Int32 i = 0; i < this.EmployeePostList.GetCountItems(); i++)
                            {
                                objCompanyPost = this.EmployeePostList.GetByIndex(i);
                                if (objCompanyPost == null) { continue; }
                                try
                                {
                                    cmd.Parameters["@COMPANY_GUID_ID"].Value = objCompanyPost.Company.uuidID;
                                    cmd.Parameters["@EMPLOYEEPOST_GUID_ID"].Value = objCompanyPost.EmploeePost.uuidID;
                                    cmd.ExecuteNonQuery();
                                    iRet = (System.Int32)cmd.Parameters["@RETURN_VALUE"].Value;
                                    if (iRet != 0)
                                    {
                                        bErrUpdate = true;
                                        switch (iRet)
                                        {
                                            case 1:
                                                DevExpress.XtraEditors.XtraMessageBox.Show("�������� � ��������� ��������������� �� ������� \n" + this.m_uuidID.ToString(), "��������",
                                                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                                                break;
                                            case 2:
                                                DevExpress.XtraEditors.XtraMessageBox.Show("��������� � ��������� ��������������� �� ������� \n" + this.m_uuidID.ToString(), "��������",
                                                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                                                break;
                                            case 3:
                                                DevExpress.XtraEditors.XtraMessageBox.Show("������������ � ��������� �������������� �� ������ \n" + this.m_uuidID.ToString(), "��������",
                                                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                                                break;
                                            default:
                                                DevExpress.XtraEditors.XtraMessageBox.Show("������ ���������� ������������ ���������", "��������",
                                                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                                                break;
                                        }
                                        break;
                                    }
                                }
                                catch (System.Exception e)
                                {
                                    DevExpress.XtraEditors.XtraMessageBox.Show(
                                    "������ ���������� ������������ ���������\n" + e.Message, "��������",
                                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);
                                    continue;
                                }
                            }
                        }
                    }
                    else
                    {
                        bErrUpdate = true;
                        switch (iRet)
                        {
                            case 1:
                                DevExpress.XtraEditors.XtraMessageBox.Show("������������ � ��������� ��������������� �� ������ \n" + this.m_uuidID.ToString(), "��������",
                                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                                break;
                            default:
                                DevExpress.XtraEditors.XtraMessageBox.Show("������ �������� � ������������ email", "��������",
                                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                                break;
                        }
                    }

                    if (bErrUpdate)
                    {
                        DBTransaction.Rollback();
                        DevExpress.XtraEditors.XtraMessageBox.Show("�� ����� ���������� ��������� � �� �������� ������.\n��� ��������� � �� ���� ��������.", "��������",
                            System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    }
                }
                if (bErrUpdate == false)
                {
                    DBTransaction.Commit();
                    bRet = true;
                }
                cmd.Dispose();

            }
            catch (System.Exception e)
            {
                DBTransaction.Rollback();
                DevExpress.XtraEditors.XtraMessageBox.Show(
                "������ ���������� ������� ������������.\n" + e.Message, "��������",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
			finally // ������� ���������� �������
            {
                DBConnection.Close();
            }

            return bRet;
        }
        /// <summary>
        /// ��������� � �� ������ email ������������
        /// </summary>
        /// <param name="objProfile">�������</param>
        /// <returns>true - ������� ����������; false - ������</returns>
        public System.Boolean SaveUserEmailList(UniXP.Common.CProfile objProfile)
        {
            System.Boolean bRet = false;

            if (this.EmailList.GetCountItems() == 0) { return true; }

            System.Data.SqlClient.SqlConnection DBConnection = objProfile.GetDBSource();
            if (DBConnection == null) { return bRet; }

            try
            {
                // ������ ������� ����� ������������ � ��������� ��������
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
                cmd.Connection = DBConnection;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = System.String.Format("[{0}].[dbo].[sp_DeleteUserEmail]", objProfile.GetOptionsDllDBName());
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ((System.Byte)(0)), ((System.Byte)(0)), "", System.Data.DataRowVersion.Current, null));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ulUserID", System.Data.SqlDbType.Int, 4));
                cmd.Parameters["@ulUserID"].Value = this.ulID;
                cmd.ExecuteNonQuery();
                System.Int32 iRet = (System.Int32)cmd.Parameters["@RETURN_VALUE"].Value;
                if (iRet == 0)
                {
                    //��������� � �� ������ �������� ������� ������������
                    cmd.Parameters.Clear();
                    cmd.CommandText = System.String.Format("[{0}].[dbo].[sp_AssignUserEmail]", objProfile.GetOptionsDllDBName());
                    cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@EMAIL_GUID_ID", System.Data.SqlDbType.UniqueIdentifier, 4));
                    cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ulUserID", System.Data.SqlDbType.Int, 4));
                    cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@bAssign", System.Data.SqlDbType.Bit, 1));
                    cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@EMAIL_MAIN", System.Data.SqlDbType.Bit, 1));
                    cmd.Parameters["@ulUserID"].Value = this.ulID;
                    cmd.Parameters["@bAssign"].Value = 1;
                    CEmail objEmail = null;
                    for (System.Int32 i = 0; i < this.EmailList.GetCountItems(); i++)
                    {
                        objEmail = this.EmailList.GetByIndex(i);
                        if (objEmail == null) { continue; }
                        try
                        {
                            cmd.Parameters["@EMAIL_GUID_ID"].Value = objEmail.uuidID;
                            cmd.Parameters["@EMAIL_MAIN"].Value = objEmail.IsMain;
                            cmd.ExecuteNonQuery();
                        }
                        catch (System.Exception e)
                        {
                            DevExpress.XtraEditors.XtraMessageBox.Show(
                            "������ ���������� email ������������.\nEmail: " +
                            objEmail.Adress + "\n" + e.Message, "��������",
                            System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);
                            continue;
                        }
                    }
                    bRet = true;
                }
                else
                {
                    switch (iRet)
                    {
                        case 1:
                            DevExpress.XtraEditors.XtraMessageBox.Show("Email � ��������� ��������������� �� ������ \n" + this.m_uuidID.ToString(), "��������",
                                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                            break;
                        case 2:
                            DevExpress.XtraEditors.XtraMessageBox.Show("������������ � ��������� ��������������� �� ������ \n" + this.m_uuidID.ToString(), "��������",
                                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                            break;
                        default:
                            DevExpress.XtraEditors.XtraMessageBox.Show("������ ���������� ������������ email", "��������",
                                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                            break;
                    }
                }
                cmd.Dispose();
            }
            catch (System.Exception e)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(
                "������ ���������� ������������ email.\n" + e.Message, "��������",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
			finally // ������� ���������� �������
            {
                DBConnection.Close();
            }
            return bRet;
        }
        /// <summary>
        /// ��������� � �� ������ ���������� ������������
        /// </summary>
        /// <param name="objProfile">�������</param>
        /// <returns>true - ������� ����������; false - ������</returns>
        public System.Boolean SaveUserCompanyPostList(UniXP.Common.CProfile objProfile)
        {
            System.Boolean bRet = false;

            if (this.EmployeePostList.GetCountItems() == 0) { return true; }

            System.Data.SqlClient.SqlConnection DBConnection = objProfile.GetDBSource();
            if (DBConnection == null) { return bRet; }

            try
            {
                // ������ ������� ����� ������������ � �����������
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
                cmd.Connection = DBConnection;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = System.String.Format("[{0}].[dbo].[sp_DeleteUserCompany]", objProfile.GetOptionsDllDBName());
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ((System.Byte)(0)), ((System.Byte)(0)), "", System.Data.DataRowVersion.Current, null));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ulUserID", System.Data.SqlDbType.Int, 4));
                cmd.Parameters["@ulUserID"].Value = this.ulID;
                cmd.ExecuteNonQuery();
                System.Int32 iRet = (System.Int32)cmd.Parameters["@RETURN_VALUE"].Value;
                if (iRet == 0)
                {
                    //��������� � �� ������ ���������� ������������
                    cmd.Parameters.Clear();
                    cmd.CommandText = System.String.Format("[{0}].[dbo].[sp_AssignUserCompany]", objProfile.GetOptionsDllDBName());
                    cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@COMPANY_GUID_ID", System.Data.SqlDbType.UniqueIdentifier, 4));
                    cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@EMPLOYEEPOST_GUID_ID", System.Data.SqlDbType.UniqueIdentifier, 4));
                    cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ulUserID", System.Data.SqlDbType.Int, 4));
                    cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@bAssign", System.Data.SqlDbType.Bit, 1));
                    cmd.Parameters["@ulUserID"].Value = this.ulID;
                    cmd.Parameters["@bAssign"].Value = 1;
                    CCompanyPost objCompanyPost = null;
                    for (System.Int32 i = 0; i < this.EmployeePostList.GetCountItems(); i++)
                    {
                        objCompanyPost = this.EmployeePostList.GetByIndex(i);
                        if (objCompanyPost == null) { continue; }
                        try
                        {
                            cmd.Parameters["@COMPANY_GUID_ID"].Value = objCompanyPost.Company.uuidID;
                            cmd.Parameters["@EMPLOYEEPOST_GUID_ID"].Value = objCompanyPost.EmploeePost.uuidID;
                            cmd.ExecuteNonQuery();
                        }
                        catch (System.Exception e)
                        {
                            DevExpress.XtraEditors.XtraMessageBox.Show(
                            "������ ���������� ��������� ������������.\nEmail: " +
                            objCompanyPost.Company.Name + " " + objCompanyPost.EmploeePost.Name + "\n" + e.Message, "��������",
                            System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);
                            continue;
                        }
                    }
                    bRet = true;
                }
                else
                {
                    switch (iRet)
                    {
                        case 1:
                            DevExpress.XtraEditors.XtraMessageBox.Show("�������� � ��������� ��������������� �� ������� \n" + this.m_uuidID.ToString(), "��������",
                                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                            break;
                        case 2:
                            DevExpress.XtraEditors.XtraMessageBox.Show("��������� � ��������� ��������������� �� ������� \n" + this.m_uuidID.ToString(), "��������",
                                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                            break;
                        case 3:
                            DevExpress.XtraEditors.XtraMessageBox.Show("������������ � ��������� �������������� �� ������ \n" + this.m_uuidID.ToString(), "��������",
                                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                            break;
                        default:
                            DevExpress.XtraEditors.XtraMessageBox.Show("������ ���������� ������������ ���������", "��������",
                                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                            break;
                    }
                }
                cmd.Dispose();
            }
            catch (System.Exception e)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(
                "������ ���������� ������������ ���������.\n" + e.Message, "��������",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
			finally // ������� ���������� �������
            {
                DBConnection.Close();
            }
            return bRet;
        }
        #endregion

        #region ������ ��������� �������� ��� �������
        /// <summary>
        /// ��������� ������ ��������� �������� (��� �������)
        /// </summary>
        /// <param name="objProfile">�������</param>
        /// <returns>true - ������� ����������; false - ������</returns>
        public System.Boolean LoadDocEventList(UniXP.Common.CProfile objProfile)
        {
            System.Boolean bRet = false;

            System.Data.SqlClient.SqlConnection DBConnection = objProfile.GetDBSource();
            if (DBConnection == null) { return bRet; }

            try
            {
                // ���������� � �� ��������, ����������� ������� �� ������� ������
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
                cmd.Connection = DBConnection;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = System.String.Format("[{0}].[dbo].[sp_GetUserBudgetDocEventList]", objProfile.GetOptionsDllDBName());
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ((System.Byte)(0)), ((System.Byte)(0)), "", System.Data.DataRowVersion.Current, null));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ulUserID", System.Data.SqlDbType.Int, 4));
                cmd.Parameters["@ulUserID"].Value = this.ulID;
                System.Data.SqlClient.SqlDataReader rs = cmd.ExecuteReader();
                if (rs.HasRows)
                {
                    while (rs.Read())
                    {
                        this.BudgetDocEventList.Add(new CBudgetDocEvent(rs.GetGuid(0), rs.GetString(1)));
                    }
                }
                rs.Close();
                rs.Dispose();
                cmd.Dispose();
                bRet = true;
            }
            catch (System.Exception f)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(
                "�� ������� �������� ������ ��������� ��������.\n\n����� ������: " + f.Message, "��������",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
            finally // ������� ���������� �������
            {
                DBConnection.Close();
            }
            return bRet;
        }
        /// <summary>
        /// ���������, ����� �� ������������ ������ � ��������
        /// </summary>
        /// <param name="objBudgetDocEvent">��������</param>
        /// <returns>true - ����� ������; false - ������� ���</returns>
        public System.Boolean IsAccessToDocEvent(CBudgetDocEvent objBudgetDocEvent)
        {
            System.Boolean bRet = false;

            if (this.BudgetDocEventList.Count == 0) { return bRet; }
            try
            {
                foreach (CBudgetDocEvent objDocEvent in this.BudgetDocEventList)
                {
                    if (objDocEvent.uuidID.CompareTo(objBudgetDocEvent.uuidID) == 0)
                    {
                        bRet = true;
                        break;
                    }
                }
            }
            catch (System.Exception f)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(
                "������ �������� ������� ������������ � ��������.\n������������:" + this.UserLastName + " "
                + this.UserFirstName + "\n��������: " + objBudgetDocEvent.Name + "\n\n����� ������: " + f.Message, "��������",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
            return bRet;
        }
        #endregion

        public override string ToString()
        {
            return UserFullName;
        }
    }
    /// <summary>
    /// ����� "����� ������������ � ������� "������""
    /// </summary>
    public class CUserBudgetRights
    {
        #region ����������, ��������, ���������
        /// <summary>
        /// ������������
        /// </summary>
        private CUser m_objUser;
        /// <summary>
        /// ������������
        /// </summary>
        public CUser User
        {
            get { return m_objUser; }
            set { m_objUser = value; }
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
        /// ��������� �������������
        /// </summary>
        private CBudgetDep m_objBudgetDep;
        /// <summary>
        /// ��������� �������������
        /// </summary>
        public CBudgetDep BudgetDep
        {
            get { return m_objBudgetDep; }
            set { m_objBudgetDep = value; }
        }
        /// <summary>
        /// ��������� (��������/���������)
        /// </summary>
        private System.Boolean m_bEnable;
        /// <summary>
        /// ��������� (��������/���������)
        /// </summary>
        public System.Boolean Enable
        {
            get { return m_bEnable; }
            set { m_bEnable = value; }
        }

        #endregion

        #region �����������
        public CUserBudgetRights()
        {
            m_objUser = null;
            m_objDynamicRight = null;
            m_bEnable = false;
            m_objBudgetDep = null;
        }
        public CUserBudgetRights(CUser objUser, CDynamicRight objDynamicRight, CBudgetDep objBudgetDep, System.Boolean bEnable)
        {
            m_objUser = objUser;
            m_objDynamicRight = objDynamicRight;
            m_objBudgetDep = objBudgetDep;
            m_bEnable = bEnable;
        }
        #endregion

        #region ������ ��������
        /// <summary>
        /// ���������� ������ �������� ������ CUserBudgetRights
        /// </summary>
        /// <param name="objProfile">�������</param>
        /// <param name="cmdSQL">SQL-�������</param>
        /// <param name="UniXPUserID">�� ������������ � �� UniXP</param>
        /// <returns>������ �������� ������ CUserBudgetRights</returns>
        public static List<CUserBudgetRights> GetUserBudgetRightsList(UniXP.Common.CProfile objProfile,
            System.Data.SqlClient.SqlCommand cmdSQL, System.Int32 UniXPUserID 
            )
        {
            List<CUserBudgetRights> objList = new List<CUserBudgetRights>();
            System.Data.SqlClient.SqlConnection DBConnection = null;
            System.Data.SqlClient.SqlCommand cmd = null;
            try
            {
                if (cmdSQL == null)
                {
                    DBConnection = objProfile.GetDBSource();
                    if (DBConnection == null)
                    {
                        DevExpress.XtraEditors.XtraMessageBox.Show(
                            "�� ������� �������� ���������� � ����� ������.", "��������",
                            System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                        return objList;
                    }
                    cmd = new System.Data.SqlClient.SqlCommand();
                    cmd.Connection = DBConnection;
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                }
                else
                {
                    cmd = cmdSQL;
                    cmd.Parameters.Clear();
                }

                cmd.CommandText = System.String.Format("[{0}].[dbo].[sp_GetUserBudgetRights]", objProfile.GetOptionsDllDBName());
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ((System.Byte)(0)), ((System.Byte)(0)), "", System.Data.DataRowVersion.Current, null));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@UniXPUserID", System.Data.SqlDbType.Int));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ERROR_NUM", System.Data.SqlDbType.Int, 8, System.Data.ParameterDirection.Output, false, ((System.Byte)(0)), ((System.Byte)(0)), "", System.Data.DataRowVersion.Current, null));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ERROR_MES", System.Data.SqlDbType.NVarChar, 4000));
                cmd.Parameters["@ERROR_MES"].Direction = System.Data.ParameterDirection.Output;
                cmd.Parameters["@UniXPUserID"].Value = UniXPUserID;
                System.Data.SqlClient.SqlDataReader rs = cmd.ExecuteReader();
                if (rs.HasRows)
                {
                    while (rs.Read())
                    {
                        objList.Add(
                            new CUserBudgetRights(
                                new CUser(System.Convert.ToInt32(rs["ulUserID"]), System.Convert.ToInt32(rs["UniXPUserID"]), System.Convert.ToString(rs["strLastName"]), System.Convert.ToString(rs["strFirstName"])),
                                new CDynamicRight(System.Convert.ToInt32(rs["iRightsID"]), System.Convert.ToString(rs["strName"]), System.Convert.ToString(rs["strRole"]), ""),
                                new CBudgetDep((System.Guid)rs["BUDGETDEP_GUID_ID"], System.Convert.ToString(rs["BUDGETDEP_NAME"])),
                                System.Convert.ToBoolean(rs["bState"])
                                )

                            );
                    }
                }
                rs.Dispose();
                if (cmdSQL == null)
                {
                    cmd.Dispose();
                    DBConnection.Close();
                }
            }
            catch (System.Exception f)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(
                "�� ������� �������� ������ ���� ������������ � ������� \"������\".\n\n����� ������: " + f.Message, "��������",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
            return objList;
        }
        #endregion
    }
}
