using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace ERP_Budget.Common
{
    /// <summary>
    /// ����� "������� ������ ��������"
    /// </summary>
    public class CRouteCondition : IBaseListItem
    {
        #region ����������, ��������, ���������
        /// <summary>
        /// ������ ���������� �� ����������
        /// </summary>
        private System.Collections.Generic.List<CRouteVariable> m_RouteVariableList;
        /// <summary>
        /// ������ ���������� �� ����������
        /// </summary>
        public System.Collections.Generic.List<CRouteVariable> RouteVariableList
        {
            get { return m_RouteVariableList; }
            set { m_RouteVariableList = value; }
        }
        /// <summary>
        /// �������
        /// </summary>
        private CRouteTemplate m_objRouteTemplate;
        /// <summary>
        /// �������
        /// </summary>
        public CRouteTemplate RouteTemplate
        {
            get { return m_objRouteTemplate; }
            set { m_objRouteTemplate = value; }
        }
        /// <summary>
        /// ���������
        /// </summary>
        private System.Int32 m_iPriority;
        /// <summary>
        /// ���������
        /// </summary>
        public System.Int32 Priority
        {
            get { return m_iPriority; }
            set { m_iPriority = value; }
        }
        #endregion

        #region ������������
        public CRouteCondition()
        {
            this.m_uuidID = System.Guid.Empty;
            this.m_strName = "";
            this.m_RouteVariableList = new List<CRouteVariable>();
            this.m_objRouteTemplate = null;
            this.m_iPriority = -1;
        }

        public CRouteCondition(List<CRouteVariable> objRouteVariableList, CRouteTemplate objRouteTemplate)
        {
            this.m_uuidID = System.Guid.Empty;
            this.m_strName = "";
            this.m_RouteVariableList = new List<CRouteVariable>();
            foreach (CRouteVariable objRouteVariable in objRouteVariableList)
            {
                this.m_RouteVariableList.Add(new CRouteVariable(objRouteVariable.uuidID,
                    objRouteVariable.Name, objRouteVariable.EditClassName, objRouteVariable.PatternSrc,
                    objRouteVariable.Pattern, objRouteVariable.DataTypeName));
            }
            this.m_objRouteTemplate = objRouteTemplate;
            this.m_iPriority = -1;
        }

        public CRouteCondition(List<CRouteVariable> objRouteVariableList, CRouteTemplate objRouteTemplate,
            System.Int32 iPriority)
        {
            this.m_uuidID = System.Guid.Empty;
            this.m_strName = "";
            this.m_RouteVariableList = objRouteVariableList;
            this.m_objRouteTemplate = objRouteTemplate;
            this.m_iPriority = iPriority;
        }

        public CRouteCondition(System.Guid uuidID, List<CRouteVariable> objRouteVariableList,
            CRouteTemplate objRouteTemplate, System.Int32 iPriority)
        {
            this.m_uuidID = uuidID;
            this.m_strName = "";
            this.m_RouteVariableList = objRouteVariableList;
            this.m_objRouteTemplate = objRouteTemplate;
            this.m_iPriority = iPriority;
        }

        #endregion

        #region ������ �������
        /// <summary>
        /// ���������� ������ ������� ������ �������� ��� ���������� ���������
        /// </summary>
        /// <param name="objProfile">�������</param>
        /// <returns>������ ������� ������ ��������</returns>
        public static List<CRouteCondition> GetRouteConditionList(
            UniXP.Common.CProfile objProfile)
        {
            List<CRouteCondition> objList = new List<CRouteCondition>();
            try
            {
                // ���������� �������� ���������� � ��
                System.Data.SqlClient.SqlConnection DBConnection = objProfile.GetDBSource();
                if (DBConnection == null) { return objList; }
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
                cmd.Connection = DBConnection;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                // ������ ��� ����������� ������ �������� ���������
                List<CRouteTemplate> objRouteTemplateList = CRouteTemplate.GetRouteTemplateList(cmd, objProfile);

                // ������ ��� ����� ������ ���������� ������ �������
                List<CRouteVariable> objRouteVariableList = CRouteVariable.GetRouteVariableList(cmd, objProfile);

                // ������ �� ������ ���� �������
                if ((objRouteTemplateList.Count == 0) || (objRouteVariableList.Count == 0))
                {
                    // ���������� ������ ������
                    return objList;
                }

                // �������� ������ ������� ��������� � ���������������������� ��������� ��������� � 
                // ����������� (��� �������� )
                foreach (CRouteTemplate objRouteTemplate in objRouteTemplateList)
                {
                    objList.Add(new CRouteCondition(objRouteVariableList, objRouteTemplate));
                }

                // ������� ����� ��������� ���������� �� ��������. 
                // ��� ����� ������� ���������� ������� T_ROUTECONDITION
                cmd.Parameters.Clear();
                cmd.CommandText = System.String.Format("[{0}].[dbo].[sp_GetRouteCondition]", objProfile.GetOptionsDllDBName());
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ((System.Byte)(0)), ((System.Byte)(0)), "", System.Data.DataRowVersion.Current, null));
                System.Data.SqlClient.SqlDataReader rs = cmd.ExecuteReader();
                if (rs.HasRows)
                {
                    while (rs.Read())
                    {
                        if (SetRouteVariableValue(objList, rs.GetGuid(1), rs.GetGuid(3), rs.GetString(2),
                            rs.GetInt32(5)) == false)
                        {
                            DevExpress.XtraEditors.XtraMessageBox.Show(
                            "�� ������� ��������� ���������� ��������.\n������������� ��������: " + rs.GetGuid(3).ToString() +
                            "\n������������� ����������: " + rs.GetGuid(1).ToString() + "\n�������� ����������: " +
                            rs.GetString(2), "��������",
                            System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                            break;
                        }
                    }
                }
                rs.Close();
                rs.Dispose();
                cmd.Dispose();
                DBConnection.Close();
            }
            catch (System.Exception f)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(
                "�� ������� �������� ������ ������� ������ ��������.\n\n����� ������: " + f.Message, "��������",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
            return objList;
        }
        /// <summary>
        /// ������� ������ "������� ������ ��������" � ������ � ����������� �������� ����������
        /// </summary>
        /// <param name="objList">������ �������� "������� ������ ��������"</param>
        /// <param name="uuidRouteVariableID">�� ����������</param>
        /// <param name="uuidRouteID">�� ��������</param>
        /// <param name="strVariableValue">�������� ����������</param>
        /// <param name="iPriority">���������</param>
        /// <returns>true - ���������� �������, �������� ���������; false - ���������� �� �������</returns>
        private static System.Boolean SetRouteVariableValue(List<CRouteCondition> objList, System.Guid uuidRouteVariableID,
            System.Guid uuidRouteID, System.String strVariableValue, System.Int32 iPriority)
        {
            System.Boolean bRet = false;
            try
            {
                // ������� ������ ������ "������� ������ ��������" � ������
                foreach (CRouteCondition objRouteCondition in objList)
                {
                    if (objRouteCondition.RouteTemplate.uuidID.CompareTo(uuidRouteID) == 0)
                    {
                        objRouteCondition.m_iPriority = iPriority;
                        // ������� ������ ����������
                        foreach (CRouteVariable objRouteVariable in objRouteCondition.RouteVariableList)
                        {
                            if (objRouteVariable.uuidID.CompareTo(uuidRouteVariableID) == 0)
                            {
                                // ����� ����... ������ ������ - ����������� ��������
                                objRouteVariable.m_strValue = strVariableValue;
                                bRet = true;
                                break;
                            }
                        }
                    }
                }
            }
            catch (System.Exception f)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(
                "�� ������� ��������� ���������� ��������.\n������������� ��������: " + uuidRouteID.ToString() +
                "\n������������� ����������: " + uuidRouteVariableID.ToString() + "\n�������� ����������: " +
                strVariableValue + "\n\n����� ������: " + f.Message, "��������",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
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
        public override System.Boolean Init(UniXP.Common.CProfile objProfile, System.Guid uuidID)
        {
            System.Boolean bRet = false;
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
            System.Boolean bRet = false;
            // ���������� ������������� �� ������ ���� ������
            if (this.m_uuidID == System.Guid.Empty)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show("������������ �������� ����������� �������������� �������", "��������",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                return bRet;
            }

            System.Data.SqlClient.SqlConnection DBConnection = objProfile.GetDBSource();
            if (DBConnection == null) { return bRet; }
            System.Data.SqlClient.SqlTransaction DBTransaction = DBConnection.BeginTransaction();

            try
            {
                // ���������� � �� ��������, ����������� ������� �� �������� ������
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
                cmd.Connection = DBConnection;
                cmd.Transaction = DBTransaction;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = System.String.Format("[{0}].[dbo].[sp_DeleteRouteCondition]", objProfile.GetOptionsDllDBName());
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ((System.Byte)(0)), ((System.Byte)(0)), "", System.Data.DataRowVersion.Current, null));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@GUID_ID", System.Data.SqlDbType.UniqueIdentifier));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ERROR_NUMBER", System.Data.SqlDbType.Int, 8, System.Data.ParameterDirection.Output, false, ((System.Byte)(0)), ((System.Byte)(0)), "", System.Data.DataRowVersion.Current, null));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ERROR_MESSAGE", System.Data.DbType.String));
                cmd.Parameters["@ERROR_MESSAGE"].Direction = System.Data.ParameterDirection.Output;
                cmd.Parameters["@GUID_ID"].Value = this.m_uuidID;
                cmd.ExecuteNonQuery();
                System.Int32 iRet = (System.Int32)cmd.Parameters["@RETURN_VALUE"].Value;
                if (iRet == 0)
                {
                    // ������������ ����������
                    DBTransaction.Commit();
                    bRet = true;
                }
                else
                {
                    // ���������� ����������
                    DBTransaction.Rollback();
                    DevExpress.XtraEditors.XtraMessageBox.Show("������ ���������� ������� \n�� �������� ������� ������ ��������.\n������ : \n" + (System.String)cmd.Parameters["@ERROR_MESSAGE"].Value, "��������",
                        System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                }
                cmd.Dispose();
            }
            catch (System.Exception e)
            {
                DBTransaction.Rollback();
                DevExpress.XtraEditors.XtraMessageBox.Show(
                "������ ���������� ������� \n�� �������� ������� ������ ��������.\n" + e.Message, "��������",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
			finally // ������� ���������� �������
            {
                DBConnection.Close();
            }
            return bRet;
        }

        /// <summary>
        /// ������� ��� ������ �� �������� �� ��
        /// </summary>
        /// <param name="objProfile">�������</param>
        /// <returns>true - ������� ����������; false - ������</returns>
        public static System.Boolean RemoveAll(UniXP.Common.CProfile objProfile)
        {
            System.Boolean bRet = false;
            System.Data.SqlClient.SqlConnection DBConnection = objProfile.GetDBSource();
            if (DBConnection == null) { return bRet; }
            System.Data.SqlClient.SqlTransaction DBTransaction = DBConnection.BeginTransaction();

            try
            {
                // ���������� � �� ��������, ����������� ������� �� �������� ������
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
                cmd.Connection = DBConnection;
                cmd.Transaction = DBTransaction;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = System.String.Format("[{0}].[dbo].[sp_DeleteRouteCondition]", objProfile.GetOptionsDllDBName());
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ((System.Byte)(0)), ((System.Byte)(0)), "", System.Data.DataRowVersion.Current, null));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ERROR_NUMBER", System.Data.SqlDbType.Int, 8, System.Data.ParameterDirection.Output, false, ((System.Byte)(0)), ((System.Byte)(0)), "", System.Data.DataRowVersion.Current, null));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ERROR_MESSAGE", System.Data.DbType.String));
                cmd.Parameters["@ERROR_MESSAGE"].Direction = System.Data.ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                System.Int32 iRet = (System.Int32)cmd.Parameters["@RETURN_VALUE"].Value;
                if (iRet == 0)
                {
                    // ������������ ����������
                    DBTransaction.Commit();
                    bRet = true;
                }
                else
                {
                    // ���������� ����������
                    DBTransaction.Rollback();
                    DevExpress.XtraEditors.XtraMessageBox.Show("������ ���������� ������� \n�� �������� ������� ������ ��������.\n������ : \n" +
                        (System.String)cmd.Parameters["@ERROR_MESSAGE"].Value, "��������",
                        System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                }
                cmd.Dispose();
            }
            catch (System.Exception e)
            {
                DBTransaction.Rollback();
                DevExpress.XtraEditors.XtraMessageBox.Show(
                "������ ���������� ������� \n�� �������� ������� ������ ��������.\n" + e.Message, "��������",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
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
        /// ��������� � �� ������ ������� ������ ���������
        /// </summary>
        /// <param name="objProfile">�������</param>
        /// <returns>true - ������� ����������; false - ������</returns>
        public static System.Boolean SaveRouteConditionList(UniXP.Common.CProfile objProfile,
            List<CRouteCondition> objList)
        {
            System.Boolean bRet = false;
            // ������ �� ������ ���� ������
            if (objList.Count == 0)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show("������ ������� �������������� ��������� �� ������ ���� ������.", "��������",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                return bRet;
            }

            System.Data.SqlClient.SqlConnection DBConnection = objProfile.GetDBSource();
            if (DBConnection == null) { return bRet; }
            System.Data.SqlClient.SqlTransaction DBTransaction = DBConnection.BeginTransaction();
            try
            {
                // ���������� � �� ��������, ����������� ������� �� �������� ������ � ��
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
                cmd.Connection = DBConnection;
                cmd.Transaction = DBTransaction;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                // ������ ������� ��� �������, � ����� ��������� ������
                cmd.Parameters.Clear();
                cmd.CommandText = System.String.Format("[{0}].[dbo].[sp_DeleteRouteCondition]", objProfile.GetOptionsDllDBName());
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ((System.Byte)(0)), ((System.Byte)(0)), "", System.Data.DataRowVersion.Current, null));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ERROR_NUMBER", System.Data.SqlDbType.Int, 8, System.Data.ParameterDirection.Output, false, ((System.Byte)(0)), ((System.Byte)(0)), "", System.Data.DataRowVersion.Current, null));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ERROR_MESSAGE", System.Data.DbType.String));
                cmd.Parameters["@ERROR_MESSAGE"].Direction = System.Data.ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                System.Int32 iRet = (System.Int32)cmd.Parameters["@RETURN_VALUE"].Value;
                if (iRet == 0)
                {
                    cmd.Parameters.Clear();
                    cmd.CommandText = System.String.Format("[{0}].[dbo].[sp_AddRouteCondition]", objProfile.GetOptionsDllDBName());
                    cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ((System.Byte)(0)), ((System.Byte)(0)), "", System.Data.DataRowVersion.Current, null));
                    cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@GUID_ID", System.Data.SqlDbType.UniqueIdentifier, 4, System.Data.ParameterDirection.Output, false, ((System.Byte)(0)), ((System.Byte)(0)), "", System.Data.DataRowVersion.Current, null));
                    cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ROUTEVARIABLE_GUID_ID", System.Data.SqlDbType.UniqueIdentifier));
                    cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ROUTE_GUID_ID", System.Data.SqlDbType.UniqueIdentifier));
                    cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@CONDITION", System.Data.SqlDbType.Text));
                    cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@PRIORITY", System.Data.SqlDbType.Int));
                    CRouteCondition objRouteCondition = null;
                    iRet = -1;
                    for (System.Int32 i = 0; i < objList.Count; i++)
                    {
                        objRouteCondition = objList[i];
                        foreach (CRouteVariable objRouteVariable in objRouteCondition.m_RouteVariableList)
                        {
                            if (objRouteVariable.m_strValue.Trim() != "")
                            {
                                cmd.Parameters["@CONDITION"].Value = objRouteVariable.m_strValue;
                                cmd.Parameters["@ROUTE_GUID_ID"].Value = objRouteCondition.RouteTemplate.uuidID;
                                cmd.Parameters["@ROUTEVARIABLE_GUID_ID"].Value = objRouteVariable.uuidID;
                                cmd.Parameters["@PRIORITY"].Value = i;
                                cmd.ExecuteNonQuery();
                                iRet = (System.Int32)cmd.Parameters["@RETURN_VALUE"].Value;
                                if (iRet != 0) { break; }
                            }
                        }
                        if (iRet != 0) { break; }
                    }
                    if (iRet == 0)
                    {
                        // ������������ ����������
                        DBTransaction.Commit();
                        bRet = true;
                    }
                    else
                    {
                        // ���������� ����������
                        DBTransaction.Rollback();
                        switch (iRet)
                        {
                            case 1:
                                {
                                    DevExpress.XtraEditors.XtraMessageBox.Show("���������� ��� ������� ������ �������� � �������� ��������������� �� �������: '", "��������",
                                        System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);
                                    break;
                                }
                            case 2:
                                {
                                    DevExpress.XtraEditors.XtraMessageBox.Show("������� �������� � ��������� ��������������� �� ������: '" +
                                    objRouteCondition.RouteTemplate.uuidID.ToString() + "'", "��������",
                                        System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);
                                    break;
                                }
                            case 3:
                                {
                                    DevExpress.XtraEditors.XtraMessageBox.Show("������� ������ ������� �������� ��� �������� ���������� � �������� ��� ����������: '\n" +
                                    objRouteCondition.RouteTemplate.Name, "��������",
                                        System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);
                                    break;
                                }
                            default:
                                {
                                    DevExpress.XtraEditors.XtraMessageBox.Show("������ �������� ������� ������ ��������", "������",
                                        System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                                    break;
                                }
                        }
                    }
                    objRouteCondition = null;
                }
                else
                {
                    // ���������� ����������
                    DBTransaction.Rollback();
                    DevExpress.XtraEditors.XtraMessageBox.Show("������ ���������� ������� \n�� �������� ������� ������ ��������.\n������ :\n" +
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
                "������ ���������� ������� ������ ��������.\n����� ������:\n" + e.Message, "��������",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
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
        /// <returns>true - ������� ����������; false - ������</returns>
        public override System.Boolean Add(UniXP.Common.CProfile objProfile)
        {
            System.Boolean bRet = false;

            return bRet;
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

            return bRet;
        }
        #endregion

        #region �������� �� ���������� �������
        /// <summary>
        /// �������� �� ������������ �������� ������� ���������� ��� ���������, ������ ������� � ������� ��������
        /// </summary>
        /// <param name="objCRouteCondition">������ ��������</param>
        /// <param name="objInitRouteVariableList">������ ���������� ��� ��������� � ��������</param>
        /// <returns>true - ������ ���������� ������������� ������� ��������; false - ������ ���������� �� ������������� ������� ��������</returns>
        public System.Boolean VariableListIsEqualRouteTemplate( List<CRouteVariable> objInitRouteVariableList )
        {
            System.Boolean bRet = false;

            try
            {
                if ((this == null) || (this.RouteVariableList == null) || (this.RouteVariableList.Count == 0))
                {
                    return bRet;
                }

                if ((objInitRouteVariableList == null) || (objInitRouteVariableList.Count == 0))
                {
                    return bRet;
                }

                CRouteVariable objInitRouteVariable = null;
                bRet = true;

                foreach (CRouteVariable objRouteVariable in this.RouteVariableList)
                {
                    if (objRouteVariable.m_strValue != "")
                    {
                        objInitRouteVariable = objInitRouteVariableList.SingleOrDefault<CRouteVariable>(x => x.uuidID.CompareTo(objRouteVariable.uuidID) == 0);
                        if (objInitRouteVariable != null)
                        {
                            if (CRouteConditionAlgoritm.bCheckRouteVariableValue(objInitRouteVariable, objRouteVariable) == false)
                            {
                                bRet = false;

                                break;
                            }
                        }
                        else
                        {
                            // ��� ���������� � ������� �������� �� ������� ������������ � ������ ������� ����������
                            bRet = false;

                            break;
                        }
                    }
                } // foreach

            }
            catch
            {
                bRet = false;
            }

            return bRet;
        }
        #endregion
    }
}
