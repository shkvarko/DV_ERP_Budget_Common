using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace ERP_Budget.Common
{
    public enum enumSettingsType
    {
        Unkown = -1,
        DebitArticle = 0,
        BudgetEditor = 1
    }
    
    public class CSettingItemForImportData
    {
        #region ��������
        /// <summary>
        /// ������������� ���������
        /// </summary>
        public System.Int32 TOOLS_ID { get; set; }
        /// <summary>
        /// �������� (������������)
        /// </summary>
        public System.String TOOLS_NAME { get; set; }
        /// <summary>
        /// �������� (������������ ��� ����������� ������������)
        /// </summary>
        public System.String TOOLS_USERNAME { get; set; }
        /// <summary>
        /// �������� ���������
        /// </summary>
        public System.String TOOLS_DESCRIPTION { get; set; }
        /// <summary>
        /// �������� ���������
        /// </summary>
        public System.Int32 TOOLS_VALUE { get; set; }
        /// <summary>
        /// ��� ���������
        /// </summary>
        public System.Int32 TOOLSTYPE_ID { get; set; }
        #endregion

        #region �����������
        public CSettingItemForImportData()
        {
            TOOLS_ID = 0;
            TOOLS_NAME = "";
            TOOLS_USERNAME = "";
            TOOLS_DESCRIPTION = "";
            TOOLS_VALUE = 0;
            TOOLSTYPE_ID = 0;
        }
        #endregion
    }

    public class CSettingForImportData
    {
        #region ��������
        /// <summary>
        /// ���������� �������������
        /// </summary>
        public System.Guid ID { get; set; }
        /// <summary>
        /// ������������
        /// </summary>
        public System.String Name { get; set; }
        /// <summary>
        /// ������ ���������� � xml-����
        /// </summary>
        public System.Xml.XmlDocument XMLSettings { get; set; }
        /// <summary>
        /// ������ ����������
        /// </summary>
        public List<CSettingItemForImportData> SettingsList { get; set; }

        #endregion

        #region �����������
        public CSettingForImportData()
        {
            ID = System.Guid.Empty;
            Name = "";
            SettingsList = null;
            XMLSettings = null;
        }
        #endregion

        #region ������ ����������

        /// <summary>
        /// ���������� ��������� ��� ������� ������ � ���������� ������ ��������
        /// </summary>
        /// <param name="objProfile">�������</param>
        /// <param name="cmdSQL">SQL-�������</param>
        /// <returns>������ ������ "CSettingForImportData"</returns>
        public static CSettingForImportData GetSettingForImportDataInDebitArticle(UniXP.Common.CProfile objProfile, System.Data.SqlClient.SqlCommand cmdSQL)
        {
            CSettingForImportData objRet = null;
            System.String strErr = System.String.Empty;
            try
            {
                objRet = GetSettingForImportData(enumSettingsType.DebitArticle, objProfile, cmdSQL, ref strErr);
            }
            catch (System.Exception f)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(
                "�� ������� �������� ������ ��������.\n\n����� ������: " + f.Message, "��������",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
            return objRet;
        }
        /// <summary>
        /// ���������� ��������� ��� ������� ������ � ������ �������������
        /// </summary>
        /// <param name="objProfile">�������</param>
        /// <param name="cmdSQL">SQL-�������</param>
        /// <returns>������ ������ "CSettingForImportData"</returns>
        public static CSettingForImportData GetSettingForImportDataInBudgetEditor(UniXP.Common.CProfile objProfile,
            System.Data.SqlClient.SqlCommand cmdSQL, ref System.String strErr )
        {
            CSettingForImportData objRet = null;
            try
            {
                objRet = GetSettingForImportData(enumSettingsType.BudgetEditor, objProfile, cmdSQL, ref strErr);
            }
            catch (System.Exception f)
            {
                strErr += ( "�� ������� �������� ������ ��������.\n\n����� ������: " + f.Message );
            }
            return objRet;
        }
        /// <summary>
        /// ���������� ������ �������� ��� ������� ������
        /// </summary>
        /// <param name="enSettingsType">������� �������</param>
        /// <param name="objProfile">�������</param>
        /// <param name="cmdSQL">SQL-�������</param>
        /// <param name="strErr">����� ������</param>
        /// <returns>������ ������ "CSettingForImportData"</returns>
        public static CSettingForImportData GetSettingForImportData( enumSettingsType enSettingsType, 
            UniXP.Common.CProfile objProfile, System.Data.SqlClient.SqlCommand cmdSQL, ref System.String strErr)
        {
            CSettingForImportData objRet = null;
            System.Data.SqlClient.SqlConnection DBConnection = null;
            System.Data.SqlClient.SqlCommand cmd = null;
            try
            {
                if (cmdSQL == null)
                {
                    DBConnection = objProfile.GetDBSource();
                    if (DBConnection == null)
                    {
                        strErr += ("�� ������� �������� ���������� � ����� ������.");
                        return objRet;
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

                switch (enSettingsType)
                {
                    case enumSettingsType.DebitArticle:
                        cmd.CommandText = System.String.Format("[{0}].[dbo].[usp_GetSettingsForImportDataInDebitArticle]", objProfile.GetOptionsDllDBName());
                        break;
                    case enumSettingsType.BudgetEditor:
                        cmd.CommandText = System.String.Format("[{0}].[dbo].[usp_GetSettingsForImportDataInBudgetEditor]", objProfile.GetOptionsDllDBName());
                        break;
                    default:
                        cmd.CommandText = System.String.Empty;
                        break;
                }

                if (cmd.CommandText.Trim().Length == 0)
                {
                    strErr += ("�� ������� ���������� ����� �������.");
                    return objRet;
                }

                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ((System.Byte)(0)), ((System.Byte)(0)), "", System.Data.DataRowVersion.Current, null));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ERROR_NUM", System.Data.SqlDbType.Int, 8, System.Data.ParameterDirection.Output, false, ((System.Byte)(0)), ((System.Byte)(0)), "", System.Data.DataRowVersion.Current, null));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ERROR_MES", System.Data.SqlDbType.NVarChar, 4000));
                cmd.Parameters["@ERROR_MES"].Direction = System.Data.ParameterDirection.Output;
                System.Data.SqlClient.SqlDataReader rs = cmd.ExecuteReader();
                if (rs.HasRows)
                {

                    rs.Read();
                    {
                        objRet = new CSettingForImportData();
                        objRet.ID = (System.Guid)rs["Settings_Guid"];
                        objRet.Name = System.Convert.ToString(rs["Settings_Name"]);
                        objRet.SettingsList = new List<CSettingItemForImportData>();

                        objRet.XMLSettings = new System.Xml.XmlDocument();
                        objRet.XMLSettings.LoadXml(rs.GetSqlXml(2).Value);

                        foreach (System.Xml.XmlNode objNode in objRet.XMLSettings.ChildNodes)
                        {
                            foreach (System.Xml.XmlNode objChildNode in objNode.ChildNodes)
                            {
                                objRet.SettingsList.Add(new CSettingItemForImportData()
                                {
                                    TOOLS_ID = System.Convert.ToInt32(objChildNode.Attributes["TOOLS_ID"].Value),
                                    TOOLS_NAME = System.Convert.ToString(objChildNode.Attributes["TOOLS_NAME"].Value),
                                    TOOLS_USERNAME = System.Convert.ToString(objChildNode.Attributes["TOOLS_USERNAME"].Value),
                                    TOOLS_DESCRIPTION = System.Convert.ToString(objChildNode.Attributes["TOOLS_DESCRIPTION"].Value),
                                    TOOLS_VALUE = System.Convert.ToInt32(objChildNode.Attributes["TOOLS_VALUE"].Value),
                                    TOOLSTYPE_ID = System.Convert.ToInt32(objChildNode.Attributes["TOOLSTYPE_ID"].Value)

                                });
                            }
                        }
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
                strErr += ("�� ������� �������� ������ ��������.\n\n����� ������: " + f.Message);
            }
            return objRet;
        }
        #endregion

        #region ���������� �������� � ���� ������
        /// <summary>
        /// ��������� � �� ��������� ��� ������� ������ � �����
        /// </summary>
        /// <param name="objProfile">�������</param>
        /// <param name="cmdSQL">SQL-�������</param>
        /// <param name="strErr">������ � ���������� �� ������</param>
        /// <returns>true - �������� ���������� ��������; false - ������</returns>
        public System.Boolean SaveExportSetting(UniXP.Common.CProfile objProfile, System.Data.SqlClient.SqlCommand cmdSQL,
            ref System.String strErr)
        {
            System.Boolean bRet = false;
            try
            {
                bRet = CSetting.SaveSettingInDB(this.ID, this.XMLSettings.InnerXml, objProfile, cmdSQL, ref strErr);
            }
            catch (System.Exception f)
            {
                strErr += (f.Message);
            }
            finally
            {
            }
            return bRet;
        }
        #endregion

    }
}
