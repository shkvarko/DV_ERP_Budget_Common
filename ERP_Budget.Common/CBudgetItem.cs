using System;
using System.Collections.Generic;
using System.Text;

namespace ERP_Budget.Common
{
    /// <summary>
    /// ��������� ������� ����
    /// </summary>
    public enum enumMonth { Unkown = 0, Jan = 1, Feb, Mar, Apr, May, Jun, Jul, Aug, Sep, Oct, Nov, Dec };
    /// <summary>
    /// ����������� ������ �������
    /// </summary>
    public class CBudgetItemDecode
    {
        #region ����������, ��������, ���������
        /// <summary>
        /// �����
        /// </summary>
        private enumMonth m_Month;
        /// <summary>
        /// �����
        /// </summary>
        public enumMonth Month
        {
            get { return m_Month; }
            set { m_Month = value; }
        }
        /// <summary>
        /// �������� ������ ��-������
        /// </summary>
        public System.String MonthTranslateRu
        {
            get 
            {
                System.String strRet = "";
                switch (m_Month)
                {
                    case enumMonth.Jan:
                        {
                            strRet = "������";
                            break;
                        }
                    case enumMonth.Feb:
                        {
                            strRet = "�������";
                            break;
                        }
                    case enumMonth.Mar:
                        {
                            strRet = "����";
                            break;
                        }
                    case enumMonth.Apr:
                        {
                            strRet = "������";
                            break;
                        }
                    case enumMonth.May:
                        {
                            strRet = "���";
                            break;
                        }
                    case enumMonth.Jun:
                        {
                            strRet = "����";
                            break;
                        }
                    case enumMonth.Jul:
                        {
                            strRet = "����";
                            break;
                        }
                    case enumMonth.Aug:
                        {
                            strRet = "������";
                            break;
                        }
                    case enumMonth.Sep:
                        {
                            strRet = "��������";
                            break;
                        }
                    case enumMonth.Oct:
                        {
                            strRet = "�������";
                            break;
                        }
                    case enumMonth.Nov:
                        {
                            strRet = "������";
                            break;
                        }
                    case enumMonth.Dec:
                        {
                            strRet = "�������";
                            break;
                        }

                    default:
                        break;
                }
                return strRet;
            }
        }
        /// <summary>
        /// ������
        /// </summary>
        private CCurrency m_objCurrency;
        /// <summary>
        /// ������
        /// </summary>
        public CCurrency Currency
        {
            get { return m_objCurrency; }
            set { m_objCurrency = value; }
        }
        /// <summary>
        /// ����������
        /// </summary>
        private System.String m_Description;
        /// <summary>
        /// ����������
        /// </summary>
        public System.String Description
        {
            get { return m_Description; }
            set { m_Description = value; }
        }
        /// <summary>
        /// ����� (����)
        /// </summary>
        private System.Double m_moneyMoneyPlan;
        /// <summary>
        /// ����� (����)
        /// </summary>
        public double MoneyPlan
        {
            get { return m_moneyMoneyPlan; }
            set { m_moneyMoneyPlan = value; }
        }
        /// <summary>
        /// ����� (���������)
        /// </summary>
        private double m_moneyMoneyPermit;
        /// <summary>
        /// ����� (���������)
        /// </summary>
        public double MoneyPermit
        {
            get { return m_moneyMoneyPermit; }
            set { m_moneyMoneyPermit = value; }
        }
        /// <summary>
        /// ����� (����)
        /// </summary>
        private double m_moneyMoneyFact;
        /// <summary>
        /// ����� (����)
        /// </summary>
        public double MoneyFact
        {
            get { return m_moneyMoneyFact; }
            set { m_moneyMoneyFact = value; }
        }
        /// <summary>
        /// ����� (���������������)
        /// </summary>
        private double m_moneyMoneyReserve;
        /// <summary>
        /// ����� (���������������)
        /// </summary>
        public double MoneyReserve
        {
            get { return m_moneyMoneyReserve; }
            set { m_moneyMoneyReserve = value; }
        }
        /// <summary>
        /// ������������ ����� ����� � ������ �������
        /// </summary>
        private double m_moneyMoneyPlanAccept;
        /// <summary>
        /// ������������ ����� ����� � ������ �������
        /// </summary>
        public double MoneyPlanAccept
        {
            get { return m_moneyMoneyPlanAccept; }
            set { m_moneyMoneyPlanAccept = value; }
        }
        /// <summary>
        /// ����� ����������� �� �����������
        /// </summary>
        private double m_moneyMoneyCredit;
        /// <summary>
        /// ����� ����������� �� �����������
        /// </summary>
        public double MoneyCredit
        {
            get { return m_moneyMoneyCredit; }
            set { m_moneyMoneyCredit = value; }
        }
        #endregion

        #region ������������
        public CBudgetItemDecode(enumMonth Month)
        {
            m_Month = Month;
            m_objCurrency = null;
            m_Description = "";
            m_moneyMoneyFact = 0;
            m_moneyMoneyPermit = 0;
            m_moneyMoneyPlan = 0;
            m_moneyMoneyReserve = 0;
            m_moneyMoneyPlanAccept = 0;
            m_moneyMoneyCredit = 0;
        }
        public CBudgetItemDecode(enumMonth Month, CCurrency objCurrency, System.String strDescription,
            double moneyMoneyFact, double moneyMoneyPermit, double moneyMoneyPlan, double moneyMoneyReserve)
        {
            m_Month = Month;
            m_objCurrency = objCurrency;
            m_Description = strDescription;
            m_moneyMoneyFact = moneyMoneyFact;
            m_moneyMoneyPermit = moneyMoneyPermit;
            m_moneyMoneyPlan = moneyMoneyPlan;
            m_moneyMoneyReserve = moneyMoneyReserve;
            m_moneyMoneyPlanAccept = 0;
            m_moneyMoneyCredit = 0;
        }
        public CBudgetItemDecode(enumMonth Month, CCurrency objCurrency, System.String strDescription,
            double moneyMoneyFact, double moneyMoneyPermit, double moneyMoneyPlan, double moneyMoneyReserve,
            double moneyMoneyPlanAccept)
        {
            m_Month = Month;
            m_objCurrency = objCurrency;
            m_Description = strDescription;
            m_moneyMoneyFact = moneyMoneyFact;
            m_moneyMoneyPermit = moneyMoneyPermit;
            m_moneyMoneyPlan = moneyMoneyPlan;
            m_moneyMoneyReserve = moneyMoneyReserve;
            m_moneyMoneyPlanAccept = moneyMoneyPlanAccept;
            m_moneyMoneyCredit = 0;
        }
        public CBudgetItemDecode(enumMonth Month, CCurrency objCurrency, System.String strDescription,
            double moneyMoneyPlan)
        {
            m_Month = Month;
            m_objCurrency = objCurrency;
            m_Description = strDescription;
            m_moneyMoneyFact = 0;
            m_moneyMoneyPermit = 0;
            m_moneyMoneyPlan = moneyMoneyPlan;
            m_moneyMoneyReserve = 0;
            m_moneyMoneyPlanAccept = 0;
            m_moneyMoneyCredit = 0;
        }
        #endregion

        #region ���������� ���������� � ����������� � ��
        /// <summary>
        /// ����������� ��� enumMonth � ��� System.Int32
        /// </summary>
        /// <param name="enMonth"></param>
        /// <returns></returns>
        private System.Int32 ConvertMonthToInt(enumMonth enMonth)
        {
            System.Int32 iRet = 0;
            try
            {
                switch (enMonth)
                {
                    case enumMonth.Jan:
                        {
                            iRet = 1;
                            break;
                        }
                    case enumMonth.Feb:
                        {
                            iRet = 2;
                            break;
                        }
                    case enumMonth.Mar:
                        {
                            iRet = 3;
                            break;
                        }
                    case enumMonth.Apr:
                        {
                            iRet = 4;
                            break;
                        }
                    case enumMonth.May:
                        {
                            iRet = 5;
                            break;
                        }
                    case enumMonth.Jun:
                        {
                            iRet = 6;
                            break;
                        }
                    case enumMonth.Jul:
                        {
                            iRet = 7;
                            break;
                        }
                    case enumMonth.Aug:
                        {
                            iRet = 8;
                            break;
                        }
                    case enumMonth.Sep:
                        {
                            iRet = 9;
                            break;
                        }
                    case enumMonth.Oct:
                        {
                            iRet = 10;
                            break;
                        }
                    case enumMonth.Nov:
                        {
                            iRet = 11;
                            break;
                        }
                    case enumMonth.Dec:
                        {
                            iRet = 12;
                            break;
                        }
                    default:
                        break;
                }
            }
            catch (System.Exception f)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(
                "������ �������������� ������ � �����.\n����� : " + enMonth.ToString() +
                "\n\n����� ������: " + f.Message, "��������",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }

            return iRet;
        }

        /// <summary>
        /// ��������� ���������� � ����������� � ��
        /// </summary>
        /// <param name="cmd">SQL-�������</param>
        /// <param name="objProfile">�������</param>
        /// <param name="uuidBudgetID">�� �������</param>
        /// <param name="uuidDebitArticleID">�� ������ ��������</param>
        /// <returns>true - �������� ����������; false - ������</returns>
        public System.Boolean SaveBudgetItemDecode(System.Data.SqlClient.SqlCommand cmd,
            UniXP.Common.CProfile objProfile, System.Guid uuidBudgetItemID)
        {
            System.Boolean bRet = false;
            if (uuidBudgetItemID.CompareTo(System.Guid.Empty) == 0)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show("���������� ������������� ������ ������� �� ������ ���� ����.", "��������",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);
                return bRet;
            }
            if (cmd == null)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show("����������� ���������� � ��.", "��������",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);
                return bRet;
            }

            try
            {
                cmd.Parameters.Clear();
                cmd.CommandText = System.String.Format("[{0}].[dbo].[sp_SaveBudgetItemDecode]", objProfile.GetOptionsDllDBName());
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ((System.Byte)(0)), ((System.Byte)(0)), "", System.Data.DataRowVersion.Current, null));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@BUDGETITEM_GUID_ID", System.Data.SqlDbType.UniqueIdentifier));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@BUDGETITEMDECODE_MONTH_ID", System.Data.SqlDbType.Int));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@BUDGETITEMDECODE_MONEYPLAN", System.Data.SqlDbType.Money));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@BUDGETITEMDECODE_DESCRIPTION", System.Data.SqlDbType.Text));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ERROR_NUMBER", System.Data.SqlDbType.Int, 8, System.Data.ParameterDirection.Output, false, ((System.Byte)(0)), ((System.Byte)(0)), "", System.Data.DataRowVersion.Current, null));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ERROR_MESSAGE", System.Data.SqlDbType.NVarChar, 4000));
                cmd.Parameters["@ERROR_MESSAGE"].Direction = System.Data.ParameterDirection.Output;
                if (this.m_objCurrency != null)
                {
                    cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@CURRENCY_GUID_ID", System.Data.SqlDbType.UniqueIdentifier));
                    cmd.Parameters["@CURRENCY_GUID_ID"].Value = this.m_objCurrency.uuidID;
                }
                cmd.Parameters["@BUDGETITEM_GUID_ID"].Value = uuidBudgetItemID;
                cmd.Parameters["@BUDGETITEMDECODE_MONTH_ID"].Value = ConvertMonthToInt(this.m_Month);
                cmd.Parameters["@BUDGETITEMDECODE_MONEYPLAN"].Value = this.m_moneyMoneyPlan;
                cmd.Parameters["@BUDGETITEMDECODE_DESCRIPTION"].Value = this.m_Description;

                cmd.ExecuteNonQuery();
                System.Int32 iRet = (System.Int32)cmd.Parameters["@RETURN_VALUE"].Value;

                if (iRet != 0)
                {
                    switch (iRet)
                    {
                        case 1:
                            {
                                DevExpress.XtraEditors.XtraMessageBox.Show(
                                "������ ���������� ����������� ������ �������.\n\nC����� ������� � �������� ��������������� �� �������: " +
                                    uuidBudgetItemID.ToString(), "������",
                                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                                break;
                            }
                        case 2:
                            {
                                DevExpress.XtraEditors.XtraMessageBox.Show(
                                "������ ���������� ����������� ������ �������.\n\n������� ������ �����: " +
                                    this.m_Month.ToString(), "������",
                                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                                break;
                            }
                        case 3:
                            {
                                DevExpress.XtraEditors.XtraMessageBox.Show(
                                "������ ���������� ����������� ������ �������.\n\n������ � �������� ��������������� �� �������: " +
                                    this.m_objCurrency.uuidID.ToString(), "������",
                                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                                break;
                            }
                        default:
                            {
                                DevExpress.XtraEditors.XtraMessageBox.Show(
                                "������ ���������� ����������� ������ �������.\n\n����� ������: " +
                                (System.String)cmd.Parameters["@ERROR_MESSAGE"].Value, "������",
                                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                                break;
                            }
                    }
                }

                bRet = (iRet == 0);
            }
            catch (System.Exception f)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(
                "������ ���������� ����������� ������ �������.\n�� ������ ������� : " + uuidBudgetItemID.ToString() +
                "\n�����: " + this.m_Month.ToString() +
                "\n\n����� ������: " + f.Message, "��������",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }

            return bRet;
        }

        /// <summary>
        /// ��������� ���������� � ����������� � ��
        /// </summary>
        /// <param name="cmd">SQL-�������</param>
        /// <param name="objProfile">�������</param>
        /// <param name="uuidBudgetItemID">�� ������ �������</param>
        /// <param name="uuidBudgetDocID">�� ���������� ���������</param>
        /// <returns>true - �������� ����������; false - ������</returns>
        public System.Boolean SaveBudgetItemDecodeForBudgetDoc(System.Data.SqlClient.SqlCommand cmd,
            UniXP.Common.CProfile objProfile, System.Guid uuidBudgetItemID, System.Guid uuidBudgetDocID )
        {
            System.Boolean bRet = false;
            if (uuidBudgetItemID.CompareTo(System.Guid.Empty) == 0)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show("���������� ������������� ������ ������� �� ������ ���� ����.", "��������",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);
                return bRet;
            }
            if (cmd == null)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show("����������� ���������� � ��.", "��������",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);
                return bRet;
            }

            try
            {
                cmd.Parameters.Clear();
                cmd.CommandText = System.String.Format("[{0}].[dbo].[sp_AddBudgetDocItemDecode]", objProfile.GetOptionsDllDBName());
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ((System.Byte)(0)), ((System.Byte)(0)), "", System.Data.DataRowVersion.Current, null));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@BUDGETDOC_GUID_ID", System.Data.SqlDbType.UniqueIdentifier));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@BUDGETITEM_GUID_ID", System.Data.SqlDbType.UniqueIdentifier));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@CURRENCY_GUID_ID", System.Data.SqlDbType.UniqueIdentifier));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@MONTH_ID", System.Data.SqlDbType.Int));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@MONEY", System.Data.SqlDbType.Money));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@MONEY_IN_BUDGETCURRENCY", System.Data.SqlDbType.Money));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ERROR_NUM", System.Data.SqlDbType.Int, 8, System.Data.ParameterDirection.Output, false, ((System.Byte)(0)), ((System.Byte)(0)), "", System.Data.DataRowVersion.Current, null));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ERROR_MES", System.Data.SqlDbType.NVarChar, 4000));
                cmd.Parameters["@ERROR_MES"].Direction = System.Data.ParameterDirection.Output;

                cmd.Parameters["@CURRENCY_GUID_ID"].Value = this.m_objCurrency.uuidID;
                cmd.Parameters["@BUDGETDOC_GUID_ID"].Value = uuidBudgetDocID;
                cmd.Parameters["@BUDGETITEM_GUID_ID"].Value = uuidBudgetItemID;
                cmd.Parameters["@MONTH_ID"].Value = ConvertMonthToInt(this.m_Month);
                cmd.Parameters["@MONEY"].Value = this.m_moneyMoneyPlan;
                cmd.Parameters["@MONEY_IN_BUDGETCURRENCY"].Value = this.MoneyPlanAccept;

                cmd.ExecuteNonQuery();
                System.Int32 iRet = (System.Int32)cmd.Parameters["@RETURN_VALUE"].Value;

                if (iRet != 0)
                {
                    DevExpress.XtraEditors.XtraMessageBox.Show(
                    "������ ���������� ����������� ������ �������.\n\n����� ������: " +
                    (System.String)cmd.Parameters["@ERROR_MES"].Value, "������",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                }

                bRet = (iRet == 0);
            }
            catch (System.Exception f)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(
                "������ ���������� ����������� ������ �������.\n�� ������ ������� : " + uuidBudgetItemID.ToString() +
                "\n�� ���������� ���������: " + uuidBudgetDocID.ToString() +
                "\n�����: " + this.m_Month.ToString() +
                "\n\n����� ������: " + f.Message, "��������",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }

            return bRet;
        }

        #endregion

    }
    /// <summary>
    /// ��������� "���������� ������� �� ������"
    /// </summary>
    public class CBudgetItemBalans
    {
        public CDebitArticle DebitArticle;
        public double MoneyPlan;
        public double MoneyFact;
        public enumMonth Month;
    }
    /// <summary>
    /// ���������, ��������������� ������ ��� ������� ����������
    /// </summary>
    public struct structBudgetItemAccTrn
    {
        /// <summary>
        /// ���� ����������
        /// </summary>
        public System.DateTime TrnDate;
        /// <summary>
        /// ����� ����������
        /// </summary>
        public System.Double MoneyTrn;
        /// <summary>
        /// ������ ����������
        /// </summary>
        public System.String CurrencyTrn;
        /// <summary>
        /// ������������, ����������� ����������
        /// </summary>
        public System.String UserTrn;
        /// <summary>
        /// ���� ���������� ���������
        /// </summary>
        public System.DateTime DocDate;
        /// <summary>
        /// ����� ���������� ���������
        /// </summary>
        public System.Double MoneyDoc;
        /// <summary>
        /// ������ ���������� ���������
        /// </summary>
        public System.String CurrencyDoc;
        /// <summary>
        /// ���� ���������� ���������
        /// </summary>
        public System.String ObjectiveDoc;
        /// <summary>
        /// ��� ����������
        /// </summary>
        public System.String Operation;
    }
    /// <summary>
    /// ���������, ��������������� ������ ��� ������� ����������
    /// </summary>
    public struct structBudgetItemDoc
    {
        /// <summary>
        /// ���� 
        /// </summary>
        public System.DateTime DocDate;
        /// <summary>
        /// ����� � ������ ���������
        /// </summary>
        public System.Double DocMoney;
        /// <summary>
        /// ������
        /// </summary>
        public System.String Currency;
        /// <summary>
        /// �����
        /// </summary>
        public System.Double Money;
        /// <summary>
        /// ������������
        /// </summary>
        public System.String User;
        /// <summary>
        /// ���� ���������� ���������
        /// </summary>
        public System.String ObjectiveDoc;
        /// <summary>
        /// ������ �������
        /// </summary>
        public System.String BudgetItemFullName;
        /// <summary>
        /// ��������� ���������
        /// </summary>
        public System.String DocState;
    }
    /// <summary>
    /// ����� "������ �������"
    /// </summary>
    public class CBudgetItem : IBaseListItem
    {

        #region ����������, ��������, ���������
        /// <summary>
        /// ���������� ������������� �������
        /// </summary>
        private System.Guid m_uuidBudgetID;
        /// <summary>
        /// ���������� ������������� �������
        /// </summary>
        public System.Guid BudgetGUID
        {
            get { return m_uuidBudgetID; }
            set { m_uuidBudgetID = value; }
        }
        /// <summary>
        /// �������� �������
        /// </summary>
        private System.String m_strBudgetName;
        /// <summary>
        /// �������� �������
        /// </summary>
        public System.String BudgetName
        {
            get { return m_strBudgetName; }
            set { m_strBudgetName = value; }
        }
        /// <summary>
        /// ������� "������������ �������"
        /// </summary>
        private System.Boolean m_OffExpenditures;
        /// <summary>
        /// ������� "������������ �������"
        /// </summary>
        public System.Boolean OffExpenditures
        {
            get { return m_OffExpenditures; }
            set { m_OffExpenditures = value; }
        }
        /// <summary>
        /// �� ������������ ������ �������
        /// </summary>
        private System.Guid m_uuidParentID;
        /// <summary>
        /// �� ������������ ������ �������
        /// </summary>
        public System.Guid ParentID
        {
            get { return m_uuidParentID; }
            set { m_uuidParentID = value; }
        }
        /// <summary>
        /// ����� ������ �������
        /// </summary>
        private string m_strBudgetItemNum;
        /// <summary>
        /// ����� ������ �������
        /// </summary>
        public string BudgetItemNum
        {
            get { return m_strBudgetItemNum; }
            set { m_strBudgetItemNum = value; }
        }
        /// <summary>
        /// �������� ������ �������
        /// </summary>
        private string m_strBudgetItemDescription;
        /// <summary>
        /// �������� ������ �������
        /// </summary>
        public string BudgetItemDescription
        {
            get { return m_strBudgetItemDescription; }
            set { m_strBudgetItemDescription = value; }
        }
        /// <summary>
        /// ����� �� ������� � �����
        /// </summary>
        private System.Int32 m_iBudgetItemID;
        /// <summary>
        /// ����� �� ������� � �����
        /// </summary>
        public System.Int32 BudgetItemID
        {
            get { return m_iBudgetItemID; }
            set { m_iBudgetItemID = value; }
        }
        /// <summary>
        /// ������� "����������� �������"
        /// </summary>
        private System.Boolean m_bTransprtRest;
        /// <summary>
        /// ������� "����������� �������"
        /// </summary>
        public System.Boolean TransprtRest
        {
            get { return m_bTransprtRest; }
            set { m_bTransprtRest = value; }
        }
        /// <summary>
        /// ������� "������ ���������� �������������"
        /// </summary>
        private System.Boolean m_bDontChange;
        /// <summary>
        /// ������� "������ ���������� �������������"
        /// </summary>
        public System.Boolean DontChange
        {
            get { return m_bDontChange; }
            set { m_bDontChange = value; }
        }
        /// <summary>
        /// ������ ������������ ������ (����� + ��������)
        /// </summary>
        public System.String BudgetItemFullName
        {
            get { return (m_strBudgetItemNum + " " + Name); }
        }
        /// <summary>
        /// ������� "������ ��� ������"
        /// </summary>
        private System.Boolean m_bReadOnly;
        /// <summary>
        /// ������� "������ ��� ������"
        /// </summary>
        public System.Boolean ReadOnly
        {
            get { return m_bReadOnly; }
            set { m_bReadOnly = value; }
        }
        /// <summary>
        /// ���������� ������������� ������ ��������
        /// </summary>
        private System.Guid m_uuidDebitArticleID;
        /// <summary>
        /// ���������� ������������� ������ ��������
        /// </summary>
        public System.Guid DebitArticleID
        {
            get { return m_uuidDebitArticleID; }
            set { m_uuidDebitArticleID = value; }
        }
        /// <summary>
        /// �����, ������� ����� ������� �� ������ ������� � ������ ���������� ��������� ( ��� ���������� ��������� )
        /// </summary>
        private System.Double m_MoneyInBudgetDocCurrency;
        /// <summary>
        /// �����, ������� ����� ������� �� ������ ������� � ������ ���������� ��������� ( ��� ���������� ��������� )
        /// </summary>
        public System.Double MoneyInBudgetDocCurrency
        {
            get { return m_MoneyInBudgetDocCurrency; }
            set { m_MoneyInBudgetDocCurrency = value; }
        }
        /// <summary>
        /// �����, ������� ����� ������� �� ������ ������� � ������ ������� ( ��� ���������� ��������� )
        /// </summary>
        private System.Double m_MoneyInBudgetCurrency;
        /// <summary>
        /// �����, ������� ����� ������� �� ������ ������� � ������ ������� ( ��� ���������� ��������� )
        /// </summary>
        public System.Double MoneyInBudgetCurrency
        {
            get { return m_MoneyInBudgetCurrency; }
            set { m_MoneyInBudgetCurrency = value; }
        }
        /// <summary>
        /// ������� ������� �� ������ ( ��� ���������� ��������� )
        /// </summary>
        private System.Double m_RestMoney;
        /// <summary>
        /// ������� ������� �� ������ ( ��� ���������� ��������� )
        /// </summary>
        public System.Double RestMoney
        {
            get { return m_RestMoney; }
            set { m_RestMoney = value; }
        }
        /// <summary>
        /// ������ ����������� ������ �������
        /// </summary>
        private System.Collections.ArrayList m_BudgetItemDecodeList;
        /// <summary>
        /// ������ ����������� ������ �������
        /// </summary>
        public System.Collections.ArrayList BudgetItemDecodeList
        {
            get { return m_BudgetItemDecodeList; }
        }
        /// <summary>
        /// ����������� �� ������
        /// </summary>
        public CBudgetItemDecode JanuaryDecode
        {
            get { return GetBudgetItemDecode(enumMonth.Jan); }
        }
        /// <summary>
        /// ����������� �� �������
        /// </summary>
        public CBudgetItemDecode FebruaryDecode
        {
            get { return GetBudgetItemDecode(enumMonth.Feb); }
        }
        /// <summary>
        /// ����������� �� ����
        /// </summary>
        public CBudgetItemDecode MarchDecode
        {
            get { return GetBudgetItemDecode(enumMonth.Mar); }
        }
        /// <summary>
        /// ����������� �� ������
        /// </summary>
        public CBudgetItemDecode AprilDecode
        {
            get { return GetBudgetItemDecode(enumMonth.Apr); }
        }
        /// <summary>
        /// ����������� �� ���
        /// </summary>
        public CBudgetItemDecode MayDecode
        {
            get { return GetBudgetItemDecode(enumMonth.May); }
        }
        /// <summary>
        /// ����������� �� ����
        /// </summary>
        public CBudgetItemDecode JuneDecode
        {
            get { return GetBudgetItemDecode(enumMonth.Jun); }
        }
        /// <summary>
        /// ����������� �� ����
        /// </summary>
        public CBudgetItemDecode JulyDecode
        {
            get { return GetBudgetItemDecode(enumMonth.Jul); }
        }
        /// <summary>
        /// ����������� �� ������
        /// </summary>
        public CBudgetItemDecode AugustDecode
        {
            get { return GetBudgetItemDecode(enumMonth.Aug); }
        }
        /// <summary>
        /// ����������� �� ��������
        /// </summary>
        public CBudgetItemDecode SeptemberDecode
        {
            get { return GetBudgetItemDecode(enumMonth.Sep); }
        }
        /// <summary>
        /// ����������� �� �������
        /// </summary>
        public CBudgetItemDecode OctoberDecode
        {
            get { return GetBudgetItemDecode(enumMonth.Oct); }
        }
        /// <summary>
        /// ����������� �� ������
        /// </summary>
        public CBudgetItemDecode NovemberDecode
        {
            get { return GetBudgetItemDecode(enumMonth.Nov); }
        }
        /// <summary>
        /// ����������� �� �������
        /// </summary>
        public CBudgetItemDecode DecemberDecode
        {
            get { return GetBudgetItemDecode(enumMonth.Dec); }
        }
        /// <summary>
        /// ������� "��� ��������� ��������" ������������ ������ ��� �������� "������ - ������ - ��� ��������"
        /// </summary>
        private CBudgetExpenseType m_objBudgetExpenseType;
        /// <summary>
        /// ������� "��� ��������� ��������" ������������ ������ ��� �������� "������ - ������ - ��� ��������"
        /// </summary>
        public CBudgetExpenseType BudgetExpenseType
        {
            get { return m_objBudgetExpenseType; }
            set { m_objBudgetExpenseType = value; }
        }
        private System.Boolean m_bIsDefficit;
        public System.Boolean IsDefficit
        {
            get { return m_bIsDefficit; }
            set { m_bIsDefficit = value; }
        }
        /// <summary>
        /// ����
        /// </summary>
        public CAccountPlan AccountPlan { get; set; }
        /// <summary>
        /// ������
        /// </summary>
        public CBudgetProject BudgetProject { get; set; }
        /// <summary>
        /// ��� �������
        /// </summary>
        public CBudgetType BudgetType { get; set; }
        #endregion

        #region ������������ *
        public CBudgetItem()
        {
            this.m_strName = "";
            this.m_strBudgetItemNum = "";
            this.m_strBudgetItemDescription = "";
            this.m_uuidID = System.Guid.Empty;
            this.m_uuidParentID = System.Guid.Empty;
            this.m_uuidBudgetID = System.Guid.Empty;
            this.m_strBudgetName = "";
            this.m_OffExpenditures = false;
            this.m_uuidDebitArticleID = System.Guid.Empty;
            this.m_bReadOnly = false;
            this.m_bTransprtRest = false;
            this.m_bDontChange = false;
            this.m_iBudgetItemID = 0;
            this.m_MoneyInBudgetCurrency = 0;
            this.m_MoneyInBudgetDocCurrency = 0;
            this.m_RestMoney = 0;
            this.m_BudgetItemDecodeList = new System.Collections.ArrayList();
            m_objBudgetExpenseType = null;
            m_bIsDefficit = false;
            AccountPlan = null;
            BudgetProject = null;
            BudgetType = null;

            FillDecodeList();
        }

        #endregion

        #region Copy  *
        /// <summary>
        /// ������� ������ "������ �������" �� ������ ������� �������
        /// </summary>
        /// <param name="objBudgetItemSrc">���������� ������ "������ �������"</param>
        /// <returns>������ "������ �������"</returns>
        public static CBudgetItem Copy(CBudgetItem objBudgetItemSrc)
        {
            CBudgetItem objBudgetItem = new CBudgetItem();

            try
            {
                objBudgetItem.uuidID = objBudgetItemSrc.uuidID;
                objBudgetItem.Name = objBudgetItemSrc.Name;
                objBudgetItem.m_bDontChange = objBudgetItemSrc.m_bDontChange;
                objBudgetItem.m_bTransprtRest = objBudgetItemSrc.m_bTransprtRest;
                objBudgetItem.m_iBudgetItemID = objBudgetItemSrc.m_iBudgetItemID;
                objBudgetItem.m_strBudgetItemDescription = objBudgetItemSrc.m_strBudgetItemDescription;
                objBudgetItem.m_strBudgetItemNum = objBudgetItemSrc.m_strBudgetItemNum;
                objBudgetItem.m_uuidBudgetID = objBudgetItemSrc.m_uuidBudgetID;
                objBudgetItem.m_strBudgetName = objBudgetItemSrc.m_strBudgetName;
                objBudgetItem.m_OffExpenditures = objBudgetItemSrc.m_OffExpenditures;
                objBudgetItem.m_uuidDebitArticleID = objBudgetItemSrc.m_uuidDebitArticleID;
                objBudgetItem.m_uuidParentID = objBudgetItemSrc.m_uuidParentID;
                objBudgetItem.BudgetExpenseType = objBudgetItemSrc.BudgetExpenseType;
                objBudgetItem.AccountPlan = objBudgetItemSrc.AccountPlan;
                objBudgetItem.BudgetProject = objBudgetItemSrc.BudgetProject;
                objBudgetItem.BudgetType = objBudgetItemSrc.BudgetType;
            }
            catch (System.Exception f)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(
                "�� ������� ����������� �������� ������� \"������ �������\".\n\n����� ������: " + f.Message, "��������",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
            return objBudgetItem;
        }
        /// <summary>
        /// �������� ������ �����������
        /// </summary>
        /// <param name="objBudgetItem">������ �������</param>
        public void CopyDecodeList(CBudgetItem objBudgetItem)
        {
            if ((objBudgetItem == null) || (objBudgetItem.m_BudgetItemDecodeList == null)) { return; }
            try
            {
                foreach (CBudgetItemDecode objBudgetItemDecode in objBudgetItem.m_BudgetItemDecodeList)
                {
                    CBudgetItemDecode objNewBudgetItemDecode = new CBudgetItemDecode(objBudgetItemDecode.Month);
                    if (objBudgetItemDecode.Currency != null)
                    {
                        objNewBudgetItemDecode.Currency = new CCurrency(objBudgetItemDecode.Currency.uuidID,
                           objBudgetItemDecode.Currency.CurrencyCode, objBudgetItemDecode.Currency.Name);
                    }
                    objNewBudgetItemDecode.Description = objBudgetItemDecode.Description;
                    objNewBudgetItemDecode.MoneyPlan = objBudgetItemDecode.MoneyPlan;
                    objNewBudgetItemDecode.MoneyFact = objBudgetItemDecode.MoneyFact;
                    objNewBudgetItemDecode.MoneyPermit = objBudgetItemDecode.MoneyPermit;
                    objNewBudgetItemDecode.MoneyPlanAccept = objBudgetItemDecode.MoneyPlanAccept;
                    objNewBudgetItemDecode.MoneyReserve = objBudgetItemDecode.MoneyReserve;

                    this.AddBudgetItemDecode(objNewBudgetItemDecode);
                }
            }
            catch (System.Exception f)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(
                "������ ����������� ������ �����������.\n\n����� ������: " + f.Message, "��������",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }

            return;
        }
        /// <summary>
        /// �������� �������������� �������� ���������� �������� ������ �������
        /// </summary>
        /// <param name="objBudgetItem">������ �������</param>
        public void CopyDecodeListPlan(CBudgetItem objBudgetItem)
        {
            if ((objBudgetItem == null) || (objBudgetItem.m_BudgetItemDecodeList == null)) { return; }
            try
            {
                foreach (CBudgetItemDecode objBudgetItemDecode in objBudgetItem.m_BudgetItemDecodeList)
                {
                    CBudgetItemDecode objNewBudgetItemDecode = new CBudgetItemDecode(objBudgetItemDecode.Month);
                    if (objBudgetItemDecode.Currency != null)
                    {
                        objNewBudgetItemDecode.Currency = new CCurrency(objBudgetItemDecode.Currency.uuidID,
                           objBudgetItemDecode.Currency.CurrencyCode, objBudgetItemDecode.Currency.Name);
                        objNewBudgetItemDecode.MoneyPlan = objBudgetItemDecode.MoneyPlan;
                    }

                    this.AddBudgetItemDecode(objNewBudgetItemDecode);
                }
            }
            catch (System.Exception f)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(
                "������ ����������� ������ �������� ����������� ������ �������.\n\n����� ������: " + f.Message, "��������",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }

            return;
        }
        #endregion

        #region ������ ����������� ������ ������� *
        /// <summary>
        /// ������� ������ ����������� ������ �������
        /// </summary>
        public void ClearDecodeList()
        {
            try
            {
                if ((m_BudgetItemDecodeList != null) && (m_BudgetItemDecodeList.Count > 0))
                {
                    m_BudgetItemDecodeList.Clear();
                }
            }
            catch (System.Exception f)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(
                "�� ������� �������� ������ ����������� ������ �������.\n" + f.Message, "��������",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }

            return;
        }
        /// <summary>
        /// ������� �������� ���������� � ������ ����������� ������ �������
        /// </summary>
        public void ClearPlanInDecodeList()
        {
            try
            {
                if ((m_BudgetItemDecodeList != null) && (m_BudgetItemDecodeList.Count > 0))
                {
                    for (System.Int32 i = 0; i < m_BudgetItemDecodeList.Count; i++)
                    {
                        ((CBudgetItemDecode)m_BudgetItemDecodeList[i]).Currency = null;
                        ((CBudgetItemDecode)m_BudgetItemDecodeList[i]).MoneyPlan = 0;
                    }
                }
            }
            catch (System.Exception f)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(
                "�� ������� �������� �������� ���������� � ������ ����������� ������ �������.\n\n����� ������: " + f.Message, "��������",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }

            return;
        }
        /// <summary>
        /// ��������� ������ ����������� ������ ������� "����������"
        /// </summary>
        public void FillDecodeList()
        {
            try
            {
                if (m_BudgetItemDecodeList == null) { return; }

                m_BudgetItemDecodeList.Clear();
                m_BudgetItemDecodeList.Add(new CBudgetItemDecode(enumMonth.Jan));
                m_BudgetItemDecodeList.Add(new CBudgetItemDecode(enumMonth.Feb));
                m_BudgetItemDecodeList.Add(new CBudgetItemDecode(enumMonth.Mar));
                m_BudgetItemDecodeList.Add(new CBudgetItemDecode(enumMonth.Apr));
                m_BudgetItemDecodeList.Add(new CBudgetItemDecode(enumMonth.May));
                m_BudgetItemDecodeList.Add(new CBudgetItemDecode(enumMonth.Jun));
                m_BudgetItemDecodeList.Add(new CBudgetItemDecode(enumMonth.Jul));
                m_BudgetItemDecodeList.Add(new CBudgetItemDecode(enumMonth.Aug));
                m_BudgetItemDecodeList.Add(new CBudgetItemDecode(enumMonth.Sep));
                m_BudgetItemDecodeList.Add(new CBudgetItemDecode(enumMonth.Oct));
                m_BudgetItemDecodeList.Add(new CBudgetItemDecode(enumMonth.Nov));
                m_BudgetItemDecodeList.Add(new CBudgetItemDecode(enumMonth.Dec));
            }
            catch (System.Exception f)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(
                "�� ��������� ������ ����������� ������ �������.\n\n����� ������: " + f.Message, "��������",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }

            return;
        }
        /// <summary>
        /// ���������� ������ "����������� ������ �������" ��� ���������� ������
        /// </summary>
        /// <param name="Month">�����</param>
        /// <returns>������ "����������� ������ �������"</returns>
        public CBudgetItemDecode GetBudgetItemDecode(enumMonth Month)
        {
            CBudgetItemDecode objRet = null;
            try
            {
                if (m_BudgetItemDecodeList.Count > 0)
                {
                    // ����� ����� ������ ������ � �������� ������
                    for (System.Int32 i = 0; i < m_BudgetItemDecodeList.Count; i++)
                    {
                        // ����� ������ � ������� Month
                        if (((CBudgetItemDecode)m_BudgetItemDecodeList[i]).Month == Month)
                        {
                            objRet = ((CBudgetItemDecode)m_BudgetItemDecodeList[i]);
                            break;
                        }
                    }
                }
            }
            catch (System.Exception f)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(
                "�� ������� �������� ����������� ������ �������.\n" + f.Message, "��������",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }

            return objRet;
        }

        public System.String GetBudgetItemDecodeInfo(enumMonth Month)
        {
            System.String strRet = "";
            try
            {
                if (m_BudgetItemDecodeList.Count > 0)
                {
                    // ����� ����� ������ ������ � �������� ������
                    for (System.Int32 i = 0; i < m_BudgetItemDecodeList.Count; i++)
                    {
                        // ����� ������ � ������� Month
                        if (((CBudgetItemDecode)m_BudgetItemDecodeList[i]).Month == Month)
                        {
                            if (((CBudgetItemDecode)m_BudgetItemDecodeList[i]).Currency != null)
                            {
                                strRet = ((CBudgetItemDecode)m_BudgetItemDecodeList[i]).MoneyPlan.ToString() + " " +
                                ((CBudgetItemDecode)m_BudgetItemDecodeList[i]).Currency.CurrencyCode + " " +
                                 ((CBudgetItemDecode)m_BudgetItemDecodeList[i]).Description;
                            }
                            break;
                        }
                    }
                }
            }
            catch (System.Exception f)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(
                "�� ������� �������� ����������� ������ �������.\n" + f.Message, "��������",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }

            return strRet;
        }

        /// <summary>
        /// ��������� ������ "����������� ������ �������" � ������
        /// </summary>
        /// <param name="objBudgetItemDecode">������ "����������� ������ �������"</param>
        public void AddBudgetItemDecode(CBudgetItemDecode objBudgetItemDecode)
        {
            try
            {
                if (m_BudgetItemDecodeList == null)
                {
                    DevExpress.XtraEditors.XtraMessageBox.Show(
                    "������\n\n������ ����������� �� ������������������!", "��������",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    return;
                }
                // ��������� ����� ������ � �������, ����������� � ������� ������� objBudgetItemDecode
                System.Boolean bFind = false;
                if (m_BudgetItemDecodeList.Count > 0)
                {
                    for (System.Int32 i = 0; i < m_BudgetItemDecodeList.Count; i++)
                    {
                        if (((CBudgetItemDecode)m_BudgetItemDecodeList[i]).Month == objBudgetItemDecode.Month)
                        {
                            // ������ ������, �������� ��� ��������
                            ((CBudgetItemDecode)m_BudgetItemDecodeList[i]).Currency = objBudgetItemDecode.Currency;
                            ((CBudgetItemDecode)m_BudgetItemDecodeList[i]).Description = objBudgetItemDecode.Description;
                            ((CBudgetItemDecode)m_BudgetItemDecodeList[i]).MoneyFact = objBudgetItemDecode.MoneyFact;
                            ((CBudgetItemDecode)m_BudgetItemDecodeList[i]).MoneyPermit = objBudgetItemDecode.MoneyPermit;
                            ((CBudgetItemDecode)m_BudgetItemDecodeList[i]).MoneyPlan = objBudgetItemDecode.MoneyPlan;
                            ((CBudgetItemDecode)m_BudgetItemDecodeList[i]).MoneyReserve = objBudgetItemDecode.MoneyReserve;
                            ((CBudgetItemDecode)m_BudgetItemDecodeList[i]).MoneyPlanAccept = objBudgetItemDecode.MoneyPlanAccept;
                            ((CBudgetItemDecode)m_BudgetItemDecodeList[i]).MoneyCredit = objBudgetItemDecode.MoneyCredit;
                            bFind = true;
                            break;
                        }
                    }
                }
                // ������ ������� � ������ ���, ���������� ������ � ������
                if (bFind == false)
                {
                    m_BudgetItemDecodeList.Add(objBudgetItemDecode);
                }
            }
            catch (System.Exception f)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(
                "�� ������� �������� � ������ ����������� ������ �������.\n" + f.Message, "��������",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }

            return;
        }
        /// <summary>
        /// ������� ������ "����������� ������ �������" �� ������
        /// </summary>
        /// <param name="Month">�����</param>
        public void DeleteBudgetItemDecode(enumMonth Month)
        {
            try
            {
                if (m_BudgetItemDecodeList.Count > 0)
                {
                    for (System.Int32 i = 0; i < m_BudgetItemDecodeList.Count; i++)
                    {
                        if (((CBudgetItemDecode)m_BudgetItemDecodeList[i]).Month == Month)
                        {
                            // ������ ������, ������� ���
                            m_BudgetItemDecodeList.RemoveAt(i);
                            break;
                        }
                    }
                }
            }
            catch (System.Exception f)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(
                "�� ������� ������� �� ������ ����������� ������ �������.\n" + f.Message, "��������",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }

            return;
        }
        public double GetMoneyAgreeSum()
        {
            double dRet = 0;
            try
            {
                if ((m_BudgetItemDecodeList != null) && (m_BudgetItemDecodeList.Count > 0))
                {
                    for (System.Int32 i = 0; i < m_BudgetItemDecodeList.Count; i++)
                    {
                        dRet += ((CBudgetItemDecode)m_BudgetItemDecodeList[i]).MoneyPlanAccept;
                    }
                }
            }
            catch (System.Exception f)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(
                "GetMoneyAgreeSum.\n\n����� ������: " + f.Message, "��������",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }

            return dRet;
        }
        public double GetMoneyPlanSum()
        {
            double dRet = 0;
            try
            {
                if ((m_BudgetItemDecodeList != null) && (m_BudgetItemDecodeList.Count > 0))
                {
                    for (System.Int32 i = 0; i < m_BudgetItemDecodeList.Count; i++)
                    {
                        dRet += ((CBudgetItemDecode)m_BudgetItemDecodeList[i]).MoneyPlan;
                    }
                }
            }
            catch (System.Exception f)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(
                "GetMoneyAgreeSum.\n\n����� ������: " + f.Message, "��������",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }

            return dRet;
        }
        #endregion

        #region ���������� ������ ����������� � �� *
        /// <summary>
        /// ��������� ����������� ������ ������� � ��
        /// </summary>
        /// <param name="objProfile">�������</param>
        /// <returns>true - ������� ����������; false - ������</returns>
        public System.Boolean SaveBudgetItem( UniXP.Common.CProfile objProfile)
        {
            System.Boolean bRet = false;

            if ((this.m_BudgetItemDecodeList == null) || (this.m_BudgetItemDecodeList.Count == 0))
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(
                    "������ ���������� ������ ����������� �� ������ �������.\n������ ����������� ����.\n������: " +
                    this.BudgetItemFullName, "������",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                return bRet;
            }

            System.Data.SqlClient.SqlConnection DBConnection = objProfile.GetDBSource();
            if( DBConnection == null ) { return bRet; }

            System.Data.SqlClient.SqlTransaction DBTransaction = DBConnection.BeginTransaction();
            System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
            cmd.Connection = DBConnection;
            cmd.Transaction = DBTransaction;
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            try
            {
                // ����� �������� �� ������ ����������� � ��������� ��� � ��
                bRet = true;
                foreach (CBudgetItemDecode objBudgetItemDecode in this.m_BudgetItemDecodeList)
                {
                    bRet = objBudgetItemDecode.SaveBudgetItemDecode(cmd, objProfile, this.uuidID);
                    if (bRet == false) { break; }
                }
                if ( bRet == true )
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
            catch (System.Exception f)
            {
                // ���������� ����������
                DBTransaction.Rollback();
                DevExpress.XtraEditors.XtraMessageBox.Show(
                "������ ���������� ������ ����������� �� ������ �������.\n������: " +
                this.BudgetItemFullName + ".\n\n����� ������: " + f.Message, "��������",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
            finally
            {
                DBConnection.Close();
            }
            return bRet;
        }
        /// <summary>
        /// ��������� ����������� ������ ������� � ��
        /// </summary>
        /// <param name="objProfile">�������</param>
        /// <param name="cmd">SQL-�������</param>
        /// <returns>true - ������� ����������; false - ������</returns>
        public System.Boolean SaveBudgetItem(System.Data.SqlClient.SqlCommand cmd,
            UniXP.Common.CProfile objProfile)
        {
            System.Boolean bRet = false;

            if (cmd == null) { return bRet; }

            if ((this.m_BudgetItemDecodeList == null) || (this.m_BudgetItemDecodeList.Count == 0))
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(
                    "������ ���������� ������ ����������� �� ������ �������.\n������ ����������� ����.\n������: " +
                    this.BudgetItemFullName, "������",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                return bRet;
            }

            try
            {
                // ����� �������� �� ������ ����������� � ��������� ��� � ��
                bRet = true;
                foreach (CBudgetItemDecode objBudgetItemDecode in this.m_BudgetItemDecodeList)
                {
                    bRet = objBudgetItemDecode.SaveBudgetItemDecode(cmd, objProfile, this.uuidID);
                    if (bRet == false) { break; }
                }

            }
            catch (System.Exception f)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(
                "������ ���������� ������ ����������� �� ������ �������.\n������: " +
                this.BudgetItemFullName + ".\n\n����� ������: " + f.Message, "��������",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }

            return bRet;
        }

        /// <summary>
        /// ��������� ����������� ���������� ��������� � ��
        /// </summary>
        /// <param name="objProfile">�������</param>
        /// <param name="cmd">SQL-�������</param>
        /// <returns>true - ������� ����������; false - ������</returns>
        public System.Boolean SaveBudgetItemDecodeListForBudgetDoc(System.Data.SqlClient.SqlCommand cmd,
            UniXP.Common.CProfile objProfile, System.Guid uuidBudgetDocID)
        {
            System.Boolean bRet = false;

            if (cmd == null) { return bRet; }

            if ((this.m_BudgetItemDecodeList == null) || (this.m_BudgetItemDecodeList.Count == 0))
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(
                    "������ ���������� ������ ����������� ��� ���������� ���������.\n������ ����������� ����.\n������: " +
                    this.BudgetItemFullName, "������",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                return bRet;
            }

            try
            {
                // ����� �������� �� ������ ����������� � ��������� ��� � ��
                bRet = true;
                foreach (CBudgetItemDecode objBudgetItemDecode in this.m_BudgetItemDecodeList)
                {
                    bRet = objBudgetItemDecode.SaveBudgetItemDecodeForBudgetDoc(cmd, objProfile, this.uuidID, uuidBudgetDocID);
                    if (bRet == false) { break; }
                }

            }
            catch (System.Exception f)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(
                "������ ���������� ������ ����������� ��� ���������� ���������.\n������: " +
                this.BudgetItemFullName + ".\n\n����� ������: " + f.Message, "��������",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }

            return bRet;
        }
        #endregion

        #region Init *
        /// <summary>
        /// ������������� ������� ������
        /// </summary>
        /// <param name="objProfile">�������</param>
        /// <param name="uuidID">���������� ������������� ������</param>
        /// <returns>true - �������� �������������; false - ������</returns>
        public override System.Boolean Init(UniXP.Common.CProfile objProfile, System.Guid uuidID)
        {
            System.Boolean bRet = false;

            System.Data.SqlClient.SqlConnection DBConnection = objProfile.GetDBSource();
            if (DBConnection == null)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show("����������� ���������� � ��.", "��������",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);
                return bRet;
            }
            try
            {
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
                cmd.Connection = DBConnection;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = System.String.Format("[{0}].[dbo].[sp_GetBudgetItem]", objProfile.GetOptionsDllDBName());
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ((System.Byte)(0)), ((System.Byte)(0)), "", System.Data.DataRowVersion.Current, null));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@GUID_ID", System.Data.SqlDbType.UniqueIdentifier));
                cmd.Parameters["@GUID_ID"].Value = uuidID;
                System.Data.SqlClient.SqlDataReader rs = cmd.ExecuteReader();
                if (rs.HasRows)
                {
                    // ����� ������ ��������
                    rs.Read();
                    this.m_uuidID = (System.Guid)rs["GUID_ID"];
                    this.m_uuidBudgetID = (System.Guid)rs["BUDGET_GUID_ID"];
                    this.BudgetName = System.Convert.ToString(rs["BUDGET_NAME"]);
                    if (rs["DEBITARTICLE_GUID_ID"] != System.DBNull.Value)
                    {
                        this.m_uuidDebitArticleID = (System.Guid)rs["DEBITARTICLE_GUID_ID"];
                    }
                    if (rs["PARENT_GUID_ID"] != System.DBNull.Value)
                    {
                        this.m_uuidParentID = (System.Guid)rs["PARENT_GUID_ID"];
                    }
                    if (rs["BUDGETITEM_DESCRIPTION"] != System.DBNull.Value)
                    {
                        this.m_strBudgetItemDescription = (System.String)rs["BUDGETITEM_DESCRIPTION"];
                    }
                    this.m_strBudgetItemNum = (System.String)rs["BUDGETITEM_NUM"];
                    this.Name = (System.String)rs["BUDGETITEM_NAME"];
                    this.m_bTransprtRest = (System.Boolean)rs["BUDGETITEM_TRANSPORTREST"];
                    this.m_bDontChange = (System.Boolean)rs["BUDGETITEM_DONTCHANGE"];
                    this.m_iBudgetItemID = (System.Int32)rs["BUDGETITEM_ID"];

                    this.AccountPlan = ((rs["ACCOUNTPLAN_GUID"] != System.DBNull.Value) ? new CAccountPlan()
                    {
                        uuidID = (System.Guid)rs["ACCOUNTPLAN_GUID"],
                        Name = System.Convert.ToString(rs["ACCOUNTPLAN_NAME"]),
                        IsActive = System.Convert.ToBoolean(rs["ACCOUNTPLAN_ACTIVE"]),
                        CodeIn1C = System.Convert.ToString(rs["ACCOUNTPLAN_1C_CODE"])
                    } : null);

                    this.BudgetExpenseType = ((rs["BUDGETEXPENSETYPE_GUID"] != System.DBNull.Value) ? new CBudgetExpenseType() 
                    { 
                        uuidID = (System.Guid)rs["BUDGETEXPENSETYPE_GUID"], 
                        Name = System.Convert.ToString( rs["BUDGETEXPENSETYPE_NAME"] ), 
                        CodeIn1C = System.Convert.ToInt32( rs["BUDGETEXPENSETYPE_1C_CODE"] ), 
                        IsActive = System.Convert.ToBoolean(rs["BUDGETEXPENSETYPE_ACTIVE"]) 
                    } : null);

                    this.BudgetProject = ( (rs["BUDGETPROJECT_GUID"] != System.DBNull.Value) ? new CBudgetProject()
                        {
                            uuidID = (System.Guid)rs["BUDGETPROJECT_GUID"],
                            Name = System.Convert.ToString(rs["BUDGETPROJECT_NAME"]),
                            Description = "",
                            IsActive = System.Convert.ToBoolean(rs["BUDGETPROJECT_ACTIVE"]),
                            CodeIn1C = System.Convert.ToInt32(rs["BUDGETPROJECT_1C_CODE"])
                        } : null );

                    this.BudgetType = ( (rs["BUDGETTYPE_GUID"] != System.DBNull.Value) ? new CBudgetType()
                        {
                            uuidID = (System.Guid)rs["BUDGETTYPE_GUID"],
                            Name = System.Convert.ToString(rs["BUDGETTYPE_NAME"]),
                            Description = ((rs["BUDGETTYPE_DESCRIPTION"] == System.DBNull.Value) ? "" : (System.String)rs["BUDGETTYPE_DESCRIPTION"]),
                            IsActive = System.Convert.ToBoolean(rs["BUDGETTYPE_ACTIVE"])
                        } : null);


                    bRet = true;
                }
                rs.Close();
                rs.Dispose();
                cmd.Dispose();
            }
            catch (System.Exception f)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(
                "�� ������� �������� ���������� � ������ �������.\n\n����� ������: " + f.Message, "��������",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
            finally // ������� ���������� �������
            {
                DBConnection.Close();
            }

            return bRet;
        }

        public System.Boolean LoadBudgetItemDecode(UniXP.Common.CProfile objProfile)
        {
            System.Boolean bRet = false;

            System.Data.SqlClient.SqlConnection DBConnection = objProfile.GetDBSource();
            if (DBConnection == null)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show("����������� ���������� � ��.", "��������",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);
                return bRet;
            }
            try
            {
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
                cmd.Connection = DBConnection;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                // ��������� ������ �����������
                if (this.m_uuidID.CompareTo(System.Guid.Empty) != 0)
                {
                    bRet = this.InitBudgetItem(objProfile, cmd);
                }
                else
                {
                    bRet = true;
                }

                cmd.Dispose();
            }
            catch (System.Exception f)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(
                "�� ������� �������� ���������� � ������ �������.\n\n����� ������: " + f.Message, "��������",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
            finally // ������� ���������� �������
            {
                DBConnection.Close();
            }

            return bRet;
        }

        #endregion

        #region Remove *
        /// <summary>
        /// ������� ������ �� ��
        /// </summary>
        /// <param name="objProfile">�������</param>
        /// <param name="uuidID">���������� ������������� �������</param>
        /// <returns>true - ������� ����������; false - ������</returns>
        public override System.Boolean Remove(UniXP.Common.CProfile objProfile)
        {
            System.Boolean bRet = false;

            System.Data.SqlClient.SqlConnection DBConnection = objProfile.GetDBSource();
            if (DBConnection == null)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show("����������� ���������� � ��.", "��������",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
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

                // ���������� ������� �������� ������ � ��
                bRet = this.Remove(cmd, objProfile);
                if (bRet == true)
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
            catch (System.Exception f)
            {
                // ���������� ����������
                DBTransaction.Rollback();
                DevExpress.XtraEditors.XtraMessageBox.Show(
                "�� ������� ������� ������ �������:" + this.BudgetItemFullName +
                "\n\n����� ������: " + f.Message, "��������",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
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
        public System.Boolean Remove(System.Data.SqlClient.SqlCommand cmd, UniXP.Common.CProfile objProfile)
        {
            System.Boolean bRet = false;
            if (cmd == null) { return bRet; }

            // ���������� ������������� �� ������ ���� ������
            if (this.m_uuidID == System.Guid.Empty)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show("������������ �������� ����������� �������������� �������", "��������",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                return bRet;
            }

            try
            {
                // ���������� � �� ��������, ����������� ������� �� �������� ������
                cmd.Parameters.Clear();
                cmd.CommandText = System.String.Format("[{0}].[dbo].[sp_DeleteBudgetItem]", objProfile.GetOptionsDllDBName());
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ((System.Byte)(0)), ((System.Byte)(0)), "", System.Data.DataRowVersion.Current, null));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@BUDGETITEM_GUID_ID", System.Data.SqlDbType.UniqueIdentifier));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ERROR_NUMBER", System.Data.SqlDbType.Int, 8, System.Data.ParameterDirection.Output, false, ((System.Byte)(0)), ((System.Byte)(0)), "", System.Data.DataRowVersion.Current, null));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ERROR_MESSAGE", System.Data.SqlDbType.NVarChar, 4000));
                cmd.Parameters["@ERROR_MESSAGE"].Direction = System.Data.ParameterDirection.Output;
                cmd.Parameters["@BUDGETITEM_GUID_ID"].Value = this.m_uuidID;
                cmd.ExecuteNonQuery();
                System.Int32 iRet = (System.Int32)cmd.Parameters["@RETURN_VALUE"].Value;
                if (iRet != 0)
                {
                    switch (iRet)
                    {
                        case 1:
                            {
                                DevExpress.XtraEditors.XtraMessageBox.Show("�������� ������ ������� ����������.\n������ ������� � �������� ��������������� �� �������.\n��: " +
                                    this.m_uuidID.ToString(), "��������",
                                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                                break;
                            }
                        case 2:
                            {
                                DevExpress.XtraEditors.XtraMessageBox.Show("�������� ������ ������� ����������.\n������ ������� ������� � ��������� ����������.", "��������",
                                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                                break;
                            }
                        case 3:
                            {
                                DevExpress.XtraEditors.XtraMessageBox.Show("�������� ������ ������� ����������.\n������ ������� ������� � �������������, ������� ������� ������.", "��������",
                                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                                break;
                            }
                        default:
                            {
                                DevExpress.XtraEditors.XtraMessageBox.Show("������ �������� ������ �������.\n\n����� ������: " +
                                        (System.String)cmd.Parameters["@ERROR_MESSAGE"].Value, "��������",
                                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                                break;
                            }
                    }
                }
                bRet = (iRet == 0);
            }
            catch (System.Exception f)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(
                "�� ������� ������� ������ �������: " + this.BudgetItemFullName + "\n\n����� ������: " + f.Message, "��������",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
            return bRet;
        }
        #endregion

        #region Add *
        /// <summary>
        /// ��������� ������������ ���������� ������������ ��������
        /// </summary>
        /// <returns>true - ��� � �������; false - �������� �������� </returns>
        public System.Boolean IsValidateProperties()
        {
            System.Boolean bRes = false;
            try
            {
                // ������������ �� ������ ���� ������
                if (this.Name == "")
                {
                    DevExpress.XtraEditors.XtraMessageBox.Show("������������ �������� ������������ ������ �������.", "��������",
                        System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    return bRes;
                }
                if (this.m_strBudgetItemNum == "")
                {
                    DevExpress.XtraEditors.XtraMessageBox.Show("������������ �������� ������ ������ �������.", "��������",
                        System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    return bRes;
                }
                //if (this.AccountPlan == null)
                //{
                //    DevExpress.XtraEditors.XtraMessageBox.Show("���������� ������� ����.", "��������",
                //        System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                //    return bRes;
                //}
                bRes = true;
            }
            catch (System.Exception f)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(
                "������ �������� ������� ������ �������.\n\n����� ������: " + f.Message, "��������",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
            return bRes;
        }
        /// <summary>
        /// �������� ������ � ��
        /// </summary>
        /// <param name="objProfile">�������</param>
        /// <returns>true - ������� ����������; false - ������</returns>
        public override System.Boolean Add(UniXP.Common.CProfile objProfile)
        {
            System.Boolean bRet = false;

            System.Data.SqlClient.SqlConnection DBConnection = objProfile.GetDBSource();
            if (DBConnection == null)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show("����������� ���������� � ��.", "��������",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
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

                // ���������� ������� ���������� ������ � ��
                bRet = this.Add(cmd, objProfile);
                if (bRet == true)
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
            catch (System.Exception f)
            {
                // ���������� ����������
                DBTransaction.Rollback();
                DevExpress.XtraEditors.XtraMessageBox.Show(
                "�� ������� ������� ������ �������:" + this.m_strBudgetItemNum + " " + this.Name +
                "\n\n����� ������: " + f.Message, "��������",
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
        public System.Boolean Add(System.Data.SqlClient.SqlCommand cmd, UniXP.Common.CProfile objProfile)
        {
            System.Boolean bRet = false;
            if (cmd == null)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show("����������� ���������� � ��.", "��������",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                return bRet;
            }

            if (IsValidateProperties() == false) { return bRet; }

            try
            {
                cmd.Parameters.Clear();
                cmd.CommandText = System.String.Format("[{0}].[dbo].[sp_AddBudgetItem]", objProfile.GetOptionsDllDBName());
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ((System.Byte)(0)), ((System.Byte)(0)), "", System.Data.DataRowVersion.Current, null));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@GUID_ID", System.Data.SqlDbType.UniqueIdentifier, 4, System.Data.ParameterDirection.Output, false, ((System.Byte)(0)), ((System.Byte)(0)), "", System.Data.DataRowVersion.Current, null));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@BUDGET_GUID_ID", System.Data.SqlDbType.UniqueIdentifier));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@BUDGETITEM_NAME", System.Data.DbType.String));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@BUDGETITEM_NUM", System.Data.DbType.String));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@BUDGETITEM_ID", System.Data.SqlDbType.Int));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@BUDGETITEM_TRANSPORTREST", System.Data.DbType.Boolean));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@BUDGETITEM_DONTCHANGE", System.Data.DbType.Boolean));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ERROR_NUMBER", System.Data.SqlDbType.Int, 8, System.Data.ParameterDirection.Output, false, ((System.Byte)(0)), ((System.Byte)(0)), "", System.Data.DataRowVersion.Current, null));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ERROR_MESSAGE", System.Data.SqlDbType.NVarChar, 4000));
                cmd.Parameters["@ERROR_MESSAGE"].Direction = System.Data.ParameterDirection.Output;
                cmd.Parameters["@BUDGET_GUID_ID"].Value = this.m_uuidBudgetID;
                cmd.Parameters["@BUDGETITEM_NAME"].Value = this.Name;
                cmd.Parameters["@BUDGETITEM_NUM"].Value = this.m_strBudgetItemNum;
                cmd.Parameters["@BUDGETITEM_TRANSPORTREST"].Value = this.m_bTransprtRest;
                cmd.Parameters["@BUDGETITEM_DONTCHANGE"].Value = this.m_bDontChange;
                cmd.Parameters["@BUDGETITEM_ID"].Value = this.m_iBudgetItemID;
                if (this.uuidID.CompareTo(System.Guid.Empty) != 0)
                {
                    cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@BUDGETITEM_GUID_ID", System.Data.SqlDbType.UniqueIdentifier));
                    cmd.Parameters["@BUDGETITEM_GUID_ID"].Value = this.uuidID;
                }
                if (this.m_uuidDebitArticleID.CompareTo(System.Guid.Empty) != 0)
                {
                    cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@DEBITARTICLE_GUID_ID", System.Data.SqlDbType.UniqueIdentifier));
                    cmd.Parameters["@DEBITARTICLE_GUID_ID"].Value = this.m_uuidDebitArticleID;
                }
                if (this.m_uuidParentID.CompareTo(System.Guid.Empty) != 0)
                {
                    cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@PARENT_GUID_ID", System.Data.SqlDbType.UniqueIdentifier));
                    cmd.Parameters["@PARENT_GUID_ID"].Value = this.m_uuidParentID;
                }
                if (this.m_strBudgetItemDescription.Length > 0)
                {
                    cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@BUDGETITEM_DESCRIPTION", System.Data.DbType.String));
                    cmd.Parameters["@BUDGETITEM_DESCRIPTION"].Value = this.m_strBudgetItemDescription;
                }
                if (this.BudgetExpenseType != null )
                {
                    cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@BUDGETEXPENSETYPE_GUID", System.Data.SqlDbType.UniqueIdentifier));
                    cmd.Parameters["@BUDGETEXPENSETYPE_GUID"].Value = this.BudgetExpenseType.uuidID;
                }
                if (this.AccountPlan != null)
                {
                    cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ACCOUNTPLAN_GUID", System.Data.SqlDbType.UniqueIdentifier));
                    cmd.Parameters["@ACCOUNTPLAN_GUID"].Value = this.AccountPlan.uuidID;
                }

                cmd.ExecuteNonQuery();
                System.Int32 iRet = (System.Int32)cmd.Parameters["@RETURN_VALUE"].Value;
                if (iRet == 0)
                {
                    bRet = true;
                }
                else
                {
                    DevExpress.XtraEditors.XtraMessageBox.Show("�� ������� ������� ������ �������.\n\n����� ������: " + 
                        ( System.String )cmd.Parameters["@ERROR_MESSAGE"].Value, "��������",
                        System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                }
            }
            catch (System.Exception f)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(
                "�� ������� ������� ������ �������.\n" + this.BudgetItemFullName + "\n\n����� ������: " + f.Message, "��������",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }

            return bRet;
        }
        #endregion

        #region Update *
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
        /// <summary>
        /// ��������� ��������� � ��
        /// </summary>
        /// <param name="objProfile">�������</param>
        /// <param name="bUpdateWarningParam">������� ����, ��� ����� �������� ������ �����</param>
        /// <returns>true - ������� ����������; false - ������</returns>
        public System.Boolean Update(UniXP.Common.CProfile objProfile, System.Boolean bUpdateWarningParam)
        {
            System.Boolean bRet = false;

            System.Data.SqlClient.SqlConnection DBConnection = objProfile.GetDBSource();
            if (DBConnection == null)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show("����������� ���������� � ��.", "��������",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
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

                // ���������� ������� ���������� � ��
                bRet = this.Update(cmd, objProfile, bUpdateWarningParam);
                if (bRet == true)
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
            catch (System.Exception f)
            {
                // ���������� ����������
                DBTransaction.Rollback();
                DevExpress.XtraEditors.XtraMessageBox.Show(
                "�� ������� �������� �������� ������ �������:" + this.BudgetItemFullName +
                "\n\n����� ������: " + f.Message, "��������",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
			finally // ������� ���������� �������
            {
                DBConnection.Close();
            }

            return bRet;
        }
        /// <summary>
        /// ��������� ��������� � ������ ������ �������
        /// </summary>
        /// <param name="objProfile">�������</param>
        /// <param name="objBudgetItemList">������ ������ �������</param>
        /// <param name="bUpdateWarningParam">������� ����, ��� ����� �������� ������ �����������</param>
        /// <returns>true - ������� ����������; false - ������</returns>
        public static System.Boolean UpdateList(UniXP.Common.CProfile objProfile, List<CBudgetItem> objBudgetItemList,
            System.Boolean bUpdateWarningParam)
        {
            System.Boolean bRet = false;
            if ((objBudgetItemList == null) || (objBudgetItemList.Count == 0))
            {
                DevExpress.XtraEditors.XtraMessageBox.Show("������ ������ �������� �������.", "��������",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                return bRet;
            }
            System.Data.SqlClient.SqlConnection DBConnection = objProfile.GetDBSource();
            if (DBConnection == null)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show("����������� ���������� � ��.", "��������",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
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
                foreach (CBudgetItem objBudgetItem in objBudgetItemList)
                {
                    // ���������� ������� ���������� � ��
                    bRet = objBudgetItem.Update(cmd, objProfile, bUpdateWarningParam);
                    if (bRet == false) { break; }
                }
                if (bRet == true)
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
            catch (System.Exception f)
            {
                // ���������� ����������
                DBTransaction.Rollback();
                DevExpress.XtraEditors.XtraMessageBox.Show(
                "�� ������� ��������� ��������� � ������ ������ �������.\n\n����� ������: " + f.Message, "��������",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
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
        /// <param name="cmd">SQL-�������</param>
        /// <param name="bUpdateWarningParam">������� ����, ��� ����� �������� ������ �����������</param>
        /// <returns>true - ������� ����������; false - ������</returns>
        public System.Boolean Update(System.Data.SqlClient.SqlCommand cmd,
            UniXP.Common.CProfile objProfile, System.Boolean bUpdateWarningParam)
        {
            System.Boolean bRet = false;
            if (cmd == null) { return bRet; }
            try
            {
                // �������� �������
                if (this.IsValidateProperties() == false) { return bRet; }

                cmd.Parameters.Clear();
                cmd.CommandText = System.String.Format("[{0}].[dbo].[sp_EditBudgetItem]", objProfile.GetOptionsDllDBName());
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ((System.Byte)(0)), ((System.Byte)(0)), "", System.Data.DataRowVersion.Current, null));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@GUID_ID", System.Data.SqlDbType.UniqueIdentifier));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@BUDGETITEM_NAME", System.Data.DbType.String));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@BUDGETITEM_NUM", System.Data.DbType.String));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@BUDGETITEM_TRANSPORTREST", System.Data.DbType.Boolean));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@BUDGETITEM_DONTCHANGE", System.Data.DbType.Boolean));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@BUDGETITEM_ID", System.Data.SqlDbType.Int));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ERROR_NUMBER", System.Data.SqlDbType.Int, 8, System.Data.ParameterDirection.Output, false, ((System.Byte)(0)), ((System.Byte)(0)), "", System.Data.DataRowVersion.Current, null));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ERROR_MESSAGE", System.Data.SqlDbType.NVarChar, 4000));
                cmd.Parameters["@ERROR_MESSAGE"].Direction = System.Data.ParameterDirection.Output;
                cmd.Parameters["@GUID_ID"].Value = this.m_uuidID;
                cmd.Parameters["@BUDGETITEM_NAME"].Value = this.Name;
                cmd.Parameters["@BUDGETITEM_NUM"].Value = this.m_strBudgetItemNum;
                cmd.Parameters["@BUDGETITEM_TRANSPORTREST"].Value = this.m_bTransprtRest;
                cmd.Parameters["@BUDGETITEM_DONTCHANGE"].Value = this.m_bDontChange;
                cmd.Parameters["@BUDGETITEM_ID"].Value = this.m_iBudgetItemID;
                if (this.m_uuidParentID.CompareTo(System.Guid.Empty) != 0)
                {
                    cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@PARENT_GUID_ID", System.Data.SqlDbType.UniqueIdentifier));
                    cmd.Parameters["@PARENT_GUID_ID"].Value = this.m_uuidParentID;
                }
                if (this.m_strBudgetItemDescription.Length > 0)
                {
                    cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@BUDGETITEM_DESCRIPTION", System.Data.DbType.String));
                    cmd.Parameters["@BUDGETITEM_DESCRIPTION"].Value = this.m_strBudgetItemDescription;
                }
                if (this.BudgetExpenseType != null)
                {
                    cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@BUDGETEXPENSETYPE_GUID", System.Data.SqlDbType.UniqueIdentifier));
                    cmd.Parameters["@BUDGETEXPENSETYPE_GUID"].Value = this.BudgetExpenseType.uuidID;
                }
                if (this.AccountPlan != null)
                {
                    cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ACCOUNTPLAN_GUID", System.Data.SqlDbType.UniqueIdentifier));
                    cmd.Parameters["@ACCOUNTPLAN_GUID"].Value = this.AccountPlan.uuidID;
                }
                cmd.ExecuteNonQuery();
                System.Int32 iRet = (System.Int32)cmd.Parameters["@RETURN_VALUE"].Value;
                if (iRet == 0)
                {
                    bRet = true;
                    if (bUpdateWarningParam == true)
                    {
                        bRet = this.SaveBudgetItem(cmd, objProfile);
                    }
                }
                else
                {
                    switch (iRet)
                    {
                        case 1:
                            DevExpress.XtraEditors.XtraMessageBox.Show("������ ������� � �������� ������ � ������� ��� ���� � ��\n" +
                                this.BudgetItemFullName, "��������",
                                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);
                            break;
                        case 2:
                            DevExpress.XtraEditors.XtraMessageBox.Show("������ ������� � �������� ��������������� �� �������.\n��: " + this.m_uuidID.ToString(), "��������",
                                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                            break;
                        default:
                            DevExpress.XtraEditors.XtraMessageBox.Show("������ ��������� ������� ������ ��������: \n" + this.BudgetItemFullName +
                                "\n\n����� ������: " + (System.String)cmd.Parameters["@ERROR_MESSAGE"].Value, "��������",
                                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                            break;
                    }
                }
            }
            catch (System.Exception f)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(
                "�� ������� �������� �������� ������ ��������: " + this.BudgetItemFullName +
                "\n\n����� ������: " + f.Message, "��������",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
            return bRet;
        }
        #endregion

        #region ������ ������ ������� � ������������� *
        public static System.Boolean LoadBudgetItemList(UniXP.Common.CProfile objProfile,
            DevExpress.XtraTreeList.TreeList objTreeList, CBudget objBudget)
        {
            System.Boolean bRet = false;
            if (objTreeList == null)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show("������ ������ �� ���������!", "��������",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);
                return bRet;
            }

            objTreeList.Enabled = false;
            objTreeList.ClearNodes();
            objBudget.BudgetItemList.Clear();

            // ������������ � ��
            System.Data.SqlClient.SqlConnection DBConnection = objProfile.GetDBSource();
            if (DBConnection == null)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show("����������� ���������� � ��.", "��������",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);
                return bRet;
            }

            try
            {
                // ���������� � �� ��������, ����������� ������� �� ������� ������
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
                cmd.Connection = DBConnection;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                // ����������� ������ ������������ ������ ��������
                List<CBudgetItem> objBudgetItemList = LoadParentBudgetItemList(objProfile, cmd, objBudget.uuidID);
                if (objBudgetItemList != null)
                {
                    // ��� ������� �������� ������ ������ ���� � ��������� ��� �� ������� �������� �����
                    if (objBudgetItemList.Count > 0)
                    {
                        foreach (CBudgetItem objBudgetItem in objBudgetItemList)
                        {
                            //��������� ���� � ������
                            DevExpress.XtraTreeList.Nodes.TreeListNode objNode =
                                objTreeList.AppendNode(new object[] { objBudgetItem.uuidID, 
                                objBudgetItem.m_uuidParentID, objBudgetItem.BudgetItemFullName, 
                                false, false, 
                                objBudgetItem.JanuaryDecode.MoneyPlan,
                                objBudgetItem.FebruaryDecode.MoneyPlan,
                                objBudgetItem.MarchDecode.MoneyPlan,
                                objBudgetItem.AprilDecode.MoneyPlan,
                                objBudgetItem.MayDecode.MoneyPlan,
                                objBudgetItem.JuneDecode.MoneyPlan,
                                objBudgetItem.JulyDecode.MoneyPlan,
                                objBudgetItem.AugustDecode.MoneyPlan,
                                objBudgetItem.SeptemberDecode.MoneyPlan,
                                objBudgetItem.OctoberDecode.MoneyPlan,
                                objBudgetItem.NovemberDecode.MoneyPlan,
                                objBudgetItem.DecemberDecode.MoneyPlan,
                                null }, null);

                            objNode.Tag = objBudgetItem;
                            objBudget.BudgetItemList.Add(objBudgetItem);

                            // �������� 
                            bRet = LoadChildBudgetItemNodes(objProfile, cmd, objBudget.uuidID, objNode, objTreeList);
                            if (bRet == false) { break; }
                        }
                    }
                    else
                    {
                        DevExpress.XtraEditors.XtraMessageBox.Show(
                        "�� ������� �������� ������ ������ ��������.", "��������",
                        System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);
                    }
                }

                cmd.Dispose();
            }
            catch (System.Exception f)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(
                "�� ������� �������� ������ ������ ��������.\n\n����� ������: " + f.Message, "��������",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
			finally // ������� ���������� �������
            {
                DBConnection.Close();
                objTreeList.Enabled = true;
                //System.DateTime dtEnd = System.DateTime.Now;
                //DevExpress.XtraEditors.XtraMessageBox.Show( 
                //"����� �������� ������: " + System.Convert.ToString( dtEnd - dtStart ), "��������",
                //System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information );
            }
            return bRet;
        }


        public static System.Boolean LoadChildBudgetItemNodes(UniXP.Common.CProfile objProfile,
            System.Data.SqlClient.SqlCommand cmd, System.Guid uuidBudgetID,
            DevExpress.XtraTreeList.Nodes.TreeListNode objNodeParent,
            DevExpress.XtraTreeList.TreeList objTreeList)
        {
            System.Boolean bRet = false;
            if (objTreeList == null)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show("������ ������ �� ���������!", "��������",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);
                return bRet;
            }

            if ((objNodeParent == null) || (objNodeParent.Tag == null))
            {
                DevExpress.XtraEditors.XtraMessageBox.Show("������ ���� ������ �� ���������!", "��������",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);
                return bRet;
            }

            if (cmd == null)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show("����������� ���������� � ��.", "��������",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);
                return bRet;
            }

            try
            {
                // ����������� ������ �������� ��������� �������
                List<CBudgetItem> objBudgetItemList = LoadChildBudgetItemList(objProfile, cmd, uuidBudgetID,
                    ((CBudgetItem)objNodeParent.Tag));
                if (objBudgetItemList != null)
                {
                    // ��� ������� �������� ������ ������ ���� � ��������� ��� �� ������� �������� �����
                    if (objBudgetItemList.Count > 0)
                    {
                        foreach (CBudgetItem objBudgetItem in objBudgetItemList)
                        {
                            //��������� ���� � ������
                            DevExpress.XtraTreeList.Nodes.TreeListNode objNode =
                                objTreeList.AppendNode(new object[] { objBudgetItem.uuidID, 
                                objBudgetItem.m_uuidParentID, objBudgetItem.BudgetItemFullName, 
                                false, false, 
                                objBudgetItem.JanuaryDecode.MoneyPlan,
                                objBudgetItem.FebruaryDecode.MoneyPlan,
                                objBudgetItem.MarchDecode.MoneyPlan,
                                objBudgetItem.AprilDecode.MoneyPlan,
                                objBudgetItem.MayDecode.MoneyPlan,
                                objBudgetItem.JuneDecode.MoneyPlan,
                                objBudgetItem.JulyDecode.MoneyPlan,
                                objBudgetItem.AugustDecode.MoneyPlan,
                                objBudgetItem.SeptemberDecode.MoneyPlan,
                                objBudgetItem.OctoberDecode.MoneyPlan,
                                objBudgetItem.NovemberDecode.MoneyPlan,
                                objBudgetItem.DecemberDecode.MoneyPlan,
                                null }, objNodeParent);

                            objNode.Tag = objBudgetItem;

                            // �������� 
                            bRet = LoadChildBudgetItemNodes(objProfile, cmd, uuidBudgetID, objNode, objTreeList);
                            if (bRet == false) { break; }
                        }
                    }
                    else
                    {
                        bRet = true;
                    }
                }
            }
            catch (System.Exception f)
            {
                bRet = false;

                DevExpress.XtraEditors.XtraMessageBox.Show(
                "�� ������� �������� ������ ��������� �������.\n\n����� ������: " + f.Message, "��������",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }

            return bRet;
        }

        public static List<CBudgetItem> LoadChildBudgetItemList(UniXP.Common.CProfile objProfile,
            System.Data.SqlClient.SqlCommand cmd, System.Guid uuidBudgetID, CBudgetItem objBudgetItemParent)
        {
            List<CBudgetItem> objList = null;

            if (objBudgetItemParent == null)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show("�� ���������� �������� ������ ��������.", "��������",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);
                return objList;
            }
            // ������������ � ��
            if (cmd == null)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show("����������� ���������� � ��.", "��������",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);
                return objList;
            }

            try
            {
                objList = new List<CBudgetItem>();

                // ������ ����������� ������ ������ ������� 
                cmd.Parameters.Clear();
                cmd.CommandText = System.String.Format("[{0}].[dbo].[sp_GetBudgetItemChild]", objProfile.GetOptionsDllDBName());
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ((System.Byte)(0)), ((System.Byte)(0)), "", System.Data.DataRowVersion.Current, null));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@BUDGETITEM_GUID_ID", System.Data.SqlDbType.UniqueIdentifier));
                cmd.Parameters["@BUDGETITEM_GUID_ID"].Value = objBudgetItemParent.uuidID;
                System.Data.SqlClient.SqlDataReader rs = cmd.ExecuteReader();

                if (rs.HasRows)
                {
                    while (rs.Read())
                    {
                        CBudgetItem objBudgetItemNew = new CBudgetItem();
                        objBudgetItemNew.uuidID = (System.Guid)rs["GUID_ID"];
                        objBudgetItemNew.m_strBudgetItemNum = (System.String)rs["BUDGETITEM_NUM"];
                        objBudgetItemNew.Name = (System.String)rs["BUDGETITEM_NAME"];
                        objBudgetItemNew.m_bTransprtRest = (System.Boolean)rs["BUDGETITEM_TRANSPORTREST"];
                        objBudgetItemNew.m_bDontChange = (System.Boolean)rs["BUDGETITEM_DONTCHANGE"];
                        objBudgetItemNew.m_iBudgetItemID = (System.Int32)rs["BUDGETITEM_ID"];
                        objBudgetItemNew.BudgetGUID = uuidBudgetID;
                        objBudgetItemNew.BudgetName = System.Convert.ToString(rs["BUDGET_NAME"]);

                        if (rs["PARENT_GUID_ID"] != System.DBNull.Value)
                        {
                            objBudgetItemNew.m_uuidParentID = (System.Guid)rs["PARENT_GUID_ID"];
                        }
                        if (rs["BUDGETITEM_DESCRIPTION"] != System.DBNull.Value)
                        {
                            objBudgetItemNew.m_strBudgetItemDescription = (System.String)rs["BUDGETITEM_DESCRIPTION"];
                        }
                        if (rs["DEBITARTICLE_GUID_ID"] != System.DBNull.Value)
                        {
                            objBudgetItemNew.m_uuidDebitArticleID = (System.Guid)rs["DEBITARTICLE_GUID_ID"];
                        }
                        if (rs["BUDGETEXPENSETYPE_GUID"] != System.DBNull.Value)
                        {
                            objBudgetItemNew.BudgetExpenseType = new CBudgetExpenseType()
                            {
                                uuidID = (System.Guid)rs["BUDGETEXPENSETYPE_GUID"],
                                Name = System.Convert.ToString(rs["BUDGETEXPENSETYPE_NAME"]),
                                CodeIn1C = System.Convert.ToInt32(rs["BUDGETEXPENSETYPE_1C_CODE"]),
                                IsActive = System.Convert.ToBoolean(rs["BUDGETEXPENSETYPE_ACTIVE"])
                            };
                        }
                        else
                        {
                            objBudgetItemNew.BudgetExpenseType = new CBudgetExpenseType(System.Guid.Empty, "", "");
                        }

                        objBudgetItemNew.AccountPlan = ( (rs["ACCOUNTPLAN_GUID"] != System.DBNull.Value) ?  new CAccountPlan()
                            {
                                uuidID = (System.Guid)rs["ACCOUNTPLAN_GUID"],
                                Name = System.Convert.ToString(rs["ACCOUNTPLAN_NAME"]),
                                IsActive = System.Convert.ToBoolean(rs["ACCOUNTPLAN_ACTIVE"]),
                                CodeIn1C = System.Convert.ToString(rs["ACCOUNTPLAN_1C_CODE"])
                            } : null );

                        objBudgetItemNew.BudgetProject = ((rs["BUDGETPROJECT_GUID"] != System.DBNull.Value) ? new CBudgetProject()
                        {
                            uuidID = (System.Guid)rs["BUDGETPROJECT_GUID"],
                            Name = System.Convert.ToString(rs["BUDGETPROJECT_NAME"]),
                            Description = "",
                            IsActive = System.Convert.ToBoolean(rs["BUDGETPROJECT_ACTIVE"]),
                            CodeIn1C = System.Convert.ToInt32(rs["BUDGETPROJECT_1C_CODE"])
                        } : null);

                        objBudgetItemNew.BudgetType = ((rs["BUDGETTYPE_GUID"] != System.DBNull.Value) ? new CBudgetType()
                        {
                            uuidID = (System.Guid)rs["BUDGETTYPE_GUID"],
                            Name = System.Convert.ToString(rs["BUDGETTYPE_NAME"]),
                            Description = ((rs["BUDGETTYPE_DESCRIPTION"] == System.DBNull.Value) ? "" : (System.String)rs["BUDGETTYPE_DESCRIPTION"]),
                            IsActive = System.Convert.ToBoolean(rs["BUDGETTYPE_ACTIVE"])
                        } : null);

                        objList.Add(objBudgetItemNew);
                    }
                }

                rs.Close();
                rs.Dispose();

                // ������ ��� ����� ������ �������� �����������
                System.Boolean bInitDecode = true;
                foreach (CBudgetItem objBudgetIt in objList)
                {
                    if (objBudgetIt.uuidID.CompareTo(System.Guid.Empty) == 0) { continue; }

                    if (InitBudgetItem(objProfile, cmd, objBudgetIt) == false)
                    {
                        bInitDecode = false;
                        break;
                    }
                }
                if (bInitDecode == false) { objList = null; }
            }
            catch (System.Exception f)
            {
                objList = null;

                DevExpress.XtraEditors.XtraMessageBox.Show(
                "�� ������� �������� ������ ��������� �������.\n\n����� ������: " + f.Message, "��������",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
			finally // ������� ���������� �������
            {
            }
            return objList;
        }

        public static System.Boolean InitBudgetItem(UniXP.Common.CProfile objProfile,
            System.Data.SqlClient.SqlCommand cmd, CBudgetItem objBudgetItem)
        {
            System.Boolean bRet = false;
            if (cmd == null)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show("����������� ���������� � ��.", "��������",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);
                return bRet;
            }
            try
            {
                // ������ ��� ������ ������� �������� �����������
                cmd.Parameters.Clear();
                cmd.CommandText = System.String.Format("[{0}].[dbo].[sp_GetBudgetItemDecode]", objProfile.GetOptionsDllDBName());
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ((System.Byte)(0)), ((System.Byte)(0)), "", System.Data.DataRowVersion.Current, null));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@BUDGETITEM_GUID_ID", System.Data.SqlDbType.UniqueIdentifier));
                cmd.Parameters["@BUDGETITEM_GUID_ID"].Value = objBudgetItem.uuidID;
                System.Data.SqlClient.SqlDataReader rs = cmd.ExecuteReader();
                if (rs.HasRows)
                {
                    while (rs.Read())
                    {
                        CBudgetItemDecode objBudgetItemDecode = new CBudgetItemDecode((enumMonth)rs["BUDGETITEMDECODE_MONTH_ID"]);
                        objBudgetItemDecode.Currency = new CCurrency((System.Guid)rs["CURRENCY_GUID_ID"], (System.String)rs["CURRENCY_CODE"], (System.String)rs["CURRENCY_NAME"]);
                        objBudgetItemDecode.Description = (rs["BUDGETITEMDECODE_DESCRIPTION"] == System.DBNull.Value) ? "" : (System.String)rs["BUDGETITEMDECODE_DESCRIPTION"];
                        objBudgetItemDecode.MoneyPlan = System.Convert.ToDouble(rs["BUDGETITEMDECODE_MONEYPLAN"]);
                        objBudgetItemDecode.MoneyPermit = System.Convert.ToDouble(rs["BUDGETITEMDECODE_MONEYPERMIT"]);
                        objBudgetItemDecode.MoneyFact = System.Convert.ToDouble(rs["BUDGETITEMDECODE_MONEYFACT"]);
                        objBudgetItemDecode.MoneyReserve = System.Convert.ToDouble(rs["BUDGETITEMDECODE_MONEYRESERVE"]);
                        objBudgetItemDecode.MoneyPlanAccept = System.Convert.ToDouble(rs["BUDGETITEMDECODE_MONEYPLANACCEPT"]);
                        objBudgetItemDecode.MoneyCredit = System.Convert.ToDouble(rs["BUDGETITEMDECODE_MONEYCREDIT"]);
                        objBudgetItem.AddBudgetItemDecode(objBudgetItemDecode);
                    }
                }
                rs.Close();
                rs.Dispose();

                bRet = true;
            }
            catch (System.Exception f)
            {
                bRet = false;

                DevExpress.XtraEditors.XtraMessageBox.Show(
                "�� ������� ��������� ������ �����������.\n\n����� ������: " + f.Message, "��������",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }

            return bRet;
        }
        /// <summary>
        /// ��������� � ������ ������� ������ �����������
        /// </summary>
        /// <param name="objProfile">�������</param>
        /// <param name="cmd">SQL-�������</param>
        /// <returns>true - ������� ����������; false - ������</returns>
        private System.Boolean InitBudgetItem(UniXP.Common.CProfile objProfile,
            System.Data.SqlClient.SqlCommand cmd)
        {
            System.Boolean bRet = false;
            if (cmd == null)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show("����������� ���������� � ��.", "��������",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);
                return bRet;
            }
            try
            {
                // ������ ��� ������ ������� �������� �����������
                cmd.Parameters.Clear();
                cmd.CommandText = System.String.Format("[{0}].[dbo].[sp_GetBudgetItemDecode]", objProfile.GetOptionsDllDBName());
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ((System.Byte)(0)), ((System.Byte)(0)), "", System.Data.DataRowVersion.Current, null));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@BUDGETITEM_GUID_ID", System.Data.SqlDbType.UniqueIdentifier));
                cmd.Parameters["@BUDGETITEM_GUID_ID"].Value = this.uuidID;
                System.Data.SqlClient.SqlDataReader rs = cmd.ExecuteReader();
                if (rs.HasRows)
                {
                    while (rs.Read())
                    {
                        CBudgetItemDecode objBudgetItemDecode = new CBudgetItemDecode((enumMonth)rs["BUDGETITEMDECODE_MONTH_ID"]);
                        objBudgetItemDecode.Currency = new CCurrency((System.Guid)rs["CURRENCY_GUID_ID"], (System.String)rs["CURRENCY_CODE"], (System.String)rs["CURRENCY_NAME"]);
                        objBudgetItemDecode.Description = (rs["BUDGETITEMDECODE_DESCRIPTION"] == System.DBNull.Value) ? "" : (System.String)rs["BUDGETITEMDECODE_DESCRIPTION"];
                        objBudgetItemDecode.MoneyPlan = System.Convert.ToDouble(rs["BUDGETITEMDECODE_MONEYPLAN"]);
                        objBudgetItemDecode.MoneyPermit = System.Convert.ToDouble(rs["BUDGETITEMDECODE_MONEYPERMIT"]);
                        objBudgetItemDecode.MoneyFact = System.Convert.ToDouble(rs["BUDGETITEMDECODE_MONEYFACT"]);
                        objBudgetItemDecode.MoneyReserve = System.Convert.ToDouble(rs["BUDGETITEMDECODE_MONEYRESERVE"]);
                        objBudgetItemDecode.MoneyPlanAccept = System.Convert.ToDouble(rs["BUDGETITEMDECODE_MONEYPLANACCEPT"]);
                        this.AddBudgetItemDecode(objBudgetItemDecode);
                    }
                }
                rs.Close();
                rs.Dispose();

                bRet = true;
            }
            catch (System.Exception f)
            {
                bRet = false;

                DevExpress.XtraEditors.XtraMessageBox.Show(
                "�� ������� ��������� ������ �����������.\n\n����� ������: " + f.Message, "��������",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }

            return bRet;
        }

        /// <summary>
        /// ��������� ������ ����������� ��� ������ �������, ��������� � ����� �� ���������� ��������� ����� ���� ������ �������
        /// </summary>
        /// <param name="objProfile">�������</param>
        /// <param name="uuidBudgetDocID">�� ���������� ��������� (����)</param>
        public void LoadBudgetDocItemDecodeList(UniXP.Common.CProfile objProfile,
            System.Guid uuidBudgetDocID)
        {
            // ������������ � ��
            System.Data.SqlClient.SqlConnection DBConnection = objProfile.GetDBSource();
            if (DBConnection == null)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show("����������� ���������� � ��.", "��������",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);
                return;
            }
            try
            {
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
                cmd.Connection = DBConnection;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Clear();
                cmd.CommandText = System.String.Format("[{0}].[dbo].[sp_GetBudgetDocItemDecodeList]", objProfile.GetOptionsDllDBName());
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ((System.Byte)(0)), ((System.Byte)(0)), "", System.Data.DataRowVersion.Current, null));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@BUDGETITEM_GUID_ID", System.Data.SqlDbType.UniqueIdentifier));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@BUDGETDOC_GUID_ID", System.Data.SqlDbType.UniqueIdentifier));
                cmd.Parameters["@BUDGETITEM_GUID_ID"].Value = this.uuidID;
                cmd.Parameters["@BUDGETDOC_GUID_ID"].Value = uuidBudgetDocID;
                System.Data.SqlClient.SqlDataReader rs = cmd.ExecuteReader();
                if (rs.HasRows)
                {
                    while (rs.Read())
                    {
                        CBudgetItemDecode objBudgetItemDecode = new CBudgetItemDecode((enumMonth)rs["MONTH_ID"]);
                        objBudgetItemDecode.Currency = new CCurrency((System.Guid)rs["CURRENCY_GUID_ID"], (System.String)rs["CURRENCY_CODE"], (System.String)rs["CURRENCY_NAME"]);
                        objBudgetItemDecode.MoneyPlan = System.Convert.ToDouble(rs["MONEY"]);
                        objBudgetItemDecode.MoneyPermit = 0;
                        objBudgetItemDecode.MoneyFact = 0;
                        objBudgetItemDecode.MoneyReserve = 0;
                        objBudgetItemDecode.MoneyPlanAccept = System.Convert.ToDouble(rs["MONEY_IN_BUDGETCURRENCY"]);
                        this.AddBudgetItemDecode(objBudgetItemDecode);
                    }
                }
                rs.Close();
                rs.Dispose();
            }
            catch (System.Exception f)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(
                "�� ������� ��������� ������ �����������.\n\n����� ������: " + f.Message, "��������",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
            finally
            {
                if (DBConnection != null) { DBConnection.Close(); }
            }

            return;
        }
        public static List<CBudgetItem> LoadParentBudgetItemList(UniXP.Common.CProfile objProfile,
            System.Data.SqlClient.SqlCommand cmd, System.Guid uuidBudgetID)
        {
            List<CBudgetItem> objList = null;

            // ������������ � ��
            if (cmd == null)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show("����������� ���������� � ��.", "��������",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);
                return objList;
            }

            try
            {
                objList = new List<CBudgetItem>();

                // ������ ����������� ������ ������ ������� 
                cmd.Parameters.Clear();
                cmd.CommandText = System.String.Format("[{0}].[dbo].[sp_GetBudgetItemParent]", objProfile.GetOptionsDllDBName());
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ((System.Byte)(0)), ((System.Byte)(0)), "", System.Data.DataRowVersion.Current, null));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@BUDGET_GUID_ID", System.Data.SqlDbType.UniqueIdentifier));
                cmd.Parameters["@BUDGET_GUID_ID"].Value = uuidBudgetID;
                System.Data.SqlClient.SqlDataReader rs = cmd.ExecuteReader();

                if (rs.HasRows)
                {
                    while (rs.Read())
                    {
                        CBudgetItem objBudgetItemNew = new CBudgetItem();
                        objBudgetItemNew.uuidID = (System.Guid)rs["GUID_ID"];
                        objBudgetItemNew.m_strBudgetItemNum = (System.String)rs["BUDGETITEM_NUM"];
                        objBudgetItemNew.Name = (System.String)rs["BUDGETITEM_NAME"];
                        objBudgetItemNew.m_bTransprtRest = (System.Boolean)rs["BUDGETITEM_TRANSPORTREST"];
                        objBudgetItemNew.m_bDontChange = (System.Boolean)rs["BUDGETITEM_DONTCHANGE"];
                        objBudgetItemNew.m_iBudgetItemID = (System.Int32)rs["BUDGETITEM_ID"];
                        objBudgetItemNew.BudgetGUID = uuidBudgetID;
                        objBudgetItemNew.BudgetName = System.Convert.ToString(rs["BUDGET_NAME"]);

                        if (rs["BUDGETITEM_DESCRIPTION"] != System.DBNull.Value)
                        {
                            objBudgetItemNew.m_strBudgetItemDescription = (System.String)rs["BUDGETITEM_DESCRIPTION"];
                        }
                        if (rs["DEBITARTICLE_GUID_ID"] != System.DBNull.Value)
                        {
                            objBudgetItemNew.m_uuidDebitArticleID = (System.Guid)rs["DEBITARTICLE_GUID_ID"];
                        }

                        objBudgetItemNew.BudgetExpenseType = ((rs["BUDGETEXPENSETYPE_GUID"] != System.DBNull.Value) ? new CBudgetExpenseType()
                        {
                            uuidID = (System.Guid)rs["BUDGETEXPENSETYPE_GUID"],
                            Name = System.Convert.ToString(rs["BUDGETEXPENSETYPE_NAME"]),
                            CodeIn1C = System.Convert.ToInt32(rs["BUDGETEXPENSETYPE_1C_CODE"]),
                            IsActive = System.Convert.ToBoolean(rs["BUDGETEXPENSETYPE_ACTIVE"])
                        } : null);

                        objBudgetItemNew.AccountPlan = ((rs["ACCOUNTPLAN_GUID"] != System.DBNull.Value) ? new CAccountPlan()
                            {
                                uuidID = (System.Guid)rs["ACCOUNTPLAN_GUID"],
                                Name = System.Convert.ToString(rs["ACCOUNTPLAN_NAME"]),
                                IsActive = System.Convert.ToBoolean(rs["ACCOUNTPLAN_ACTIVE"]),
                                CodeIn1C = System.Convert.ToString(rs["ACCOUNTPLAN_1C_CODE"])
                            } : null );

                        objBudgetItemNew.BudgetProject = ((rs["BUDGETPROJECT_GUID"] != System.DBNull.Value) ? new CBudgetProject()
                        {
                            uuidID = (System.Guid)rs["BUDGETPROJECT_GUID"],
                            Name = System.Convert.ToString(rs["BUDGETPROJECT_NAME"]),
                            Description = "",
                            IsActive = System.Convert.ToBoolean(rs["BUDGETPROJECT_ACTIVE"]),
                            CodeIn1C = System.Convert.ToInt32(rs["BUDGETPROJECT_1C_CODE"])
                        } : null);

                        objBudgetItemNew.BudgetType = ((rs["BUDGETTYPE_GUID"] != System.DBNull.Value) ? new CBudgetType()
                        {
                            uuidID = (System.Guid)rs["BUDGETTYPE_GUID"],
                            Name = System.Convert.ToString(rs["BUDGETTYPE_NAME"]),
                            Description = ((rs["BUDGETTYPE_DESCRIPTION"] == System.DBNull.Value) ? "" : (System.String)rs["BUDGETTYPE_DESCRIPTION"]),
                            IsActive = System.Convert.ToBoolean(rs["BUDGETTYPE_ACTIVE"])
                        } : null);
                        objList.Add(objBudgetItemNew);
                    }
                }
                rs.Close();
                rs.Dispose();

                // ������ ��� ����� ������ �������� �����������
                System.Boolean bInitDecode = true;
                foreach (CBudgetItem objBudgetIt in objList)
                {
                    if (objBudgetIt.uuidID.CompareTo(System.Guid.Empty) == 0) { continue; }

                    if (InitBudgetItem(objProfile, cmd, objBudgetIt) == false)
                    {
                        bInitDecode = false;
                        break;
                    }
                }
                if (bInitDecode == false) { objList.Clear(); }
            }
            catch (System.Exception f)
            {
                objList = null;

                DevExpress.XtraEditors.XtraMessageBox.Show(
                "�� ������� �������� ������ ��������� �������.\n\n����� ������: " + f.Message, "��������",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
			finally // ������� ���������� �������
            {
            }
            return objList;
        }

        #endregion

        #region ������ ������ ������� �������� ������ (PARENT = null)
        /// <summary>
        /// ���������� ������ ������������ ������ �������
        /// </summary>
        /// <param name="objProfile">�������</param>
        /// <param name="uuidBudgetID">�� �������</param>
        /// <returns>������ ������������ ������ �������</returns>
        public static List<CBudgetItem> LoadParentBudgetItemList(UniXP.Common.CProfile objProfile,
            System.Guid uuidBudgetID)
        {
            List<CBudgetItem> objList = null;

            // ������������ � ��
            System.Data.SqlClient.SqlConnection DBConnection = objProfile.GetDBSource();
            if (DBConnection == null)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show("����������� ���������� � ��.", "��������",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);
                return objList;
            }

            try
            {
                // ������ ����������� ������ ������ ������� 
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
                cmd.Connection = DBConnection;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = System.String.Format("[{0}].[dbo].[sp_GetBudgetItemParent]", objProfile.GetOptionsDllDBName());
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ((System.Byte)(0)), ((System.Byte)(0)), "", System.Data.DataRowVersion.Current, null));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@BUDGET_GUID_ID", System.Data.SqlDbType.UniqueIdentifier));
                cmd.Parameters["@BUDGET_GUID_ID"].Value = uuidBudgetID;
                System.Data.SqlClient.SqlDataReader rs = cmd.ExecuteReader();

                if (rs.HasRows)
                {
                    objList = new List<CBudgetItem>();
                    while (rs.Read())
                    {
                        CBudgetItem objBudgetItemNew = new CBudgetItem();
                        objBudgetItemNew.uuidID = (System.Guid)rs["GUID_ID"];
                        objBudgetItemNew.m_strBudgetItemNum = (System.String)rs["BUDGETITEM_NUM"];
                        objBudgetItemNew.Name = (System.String)rs["BUDGETITEM_NAME"];
                        objBudgetItemNew.m_bTransprtRest = (System.Boolean)rs["BUDGETITEM_TRANSPORTREST"];
                        objBudgetItemNew.m_bDontChange = (System.Boolean)rs["BUDGETITEM_DONTCHANGE"];
                        objBudgetItemNew.m_iBudgetItemID = (System.Int32)rs["BUDGETITEM_ID"];
                        objBudgetItemNew.BudgetGUID = uuidBudgetID;
                        objBudgetItemNew.BudgetName = System.Convert.ToString(rs["BUDGET_NAME"]);

                        if (rs["BUDGETITEM_DESCRIPTION"] != System.DBNull.Value)
                        {
                            objBudgetItemNew.m_strBudgetItemDescription = (System.String)rs["BUDGETITEM_DESCRIPTION"];
                        }
                        if (rs["DEBITARTICLE_GUID_ID"] != System.DBNull.Value)
                        {
                            objBudgetItemNew.m_uuidDebitArticleID = (System.Guid)rs["DEBITARTICLE_GUID_ID"];
                        }
                        objBudgetItemNew.BudgetExpenseType = ( (rs["BUDGETEXPENSETYPE_GUID"] != System.DBNull.Value) ? new CBudgetExpenseType()
                            {
                                uuidID = (System.Guid)rs["BUDGETEXPENSETYPE_GUID"],
                                Name = System.Convert.ToString(rs["BUDGETEXPENSETYPE_NAME"]),
                                CodeIn1C = System.Convert.ToInt32(rs["BUDGETEXPENSETYPE_1C_CODE"]),
                                IsActive = System.Convert.ToBoolean(rs["BUDGETEXPENSETYPE_ACTIVE"])
                            } : null );

                        objBudgetItemNew.AccountPlan = ((rs["ACCOUNTPLAN_GUID"] != System.DBNull.Value) ? new CAccountPlan()
                        {
                            uuidID = (System.Guid)rs["ACCOUNTPLAN_GUID"],
                            Name = System.Convert.ToString(rs["ACCOUNTPLAN_NAME"]),
                            IsActive = System.Convert.ToBoolean(rs["ACCOUNTPLAN_ACTIVE"]),
                            CodeIn1C = System.Convert.ToString(rs["ACCOUNTPLAN_1C_CODE"])
                        } : null);

                        objBudgetItemNew.BudgetProject = ((rs["BUDGETPROJECT_GUID"] != System.DBNull.Value) ? new CBudgetProject()
                        {
                            uuidID = (System.Guid)rs["BUDGETPROJECT_GUID"],
                            Name = System.Convert.ToString(rs["BUDGETPROJECT_NAME"]),
                            Description = "",
                            IsActive = System.Convert.ToBoolean(rs["BUDGETPROJECT_ACTIVE"]),
                            CodeIn1C = System.Convert.ToInt32(rs["BUDGETPROJECT_1C_CODE"])
                        } : null);
                        objBudgetItemNew.BudgetType = ((rs["BUDGETTYPE_GUID"] != System.DBNull.Value) ? new CBudgetType()
                        {
                            uuidID = (System.Guid)rs["BUDGETTYPE_GUID"],
                            Name = System.Convert.ToString(rs["BUDGETTYPE_NAME"]),
                            Description = ((rs["BUDGETTYPE_DESCRIPTION"] == System.DBNull.Value) ? "" : (System.String)rs["BUDGETTYPE_DESCRIPTION"]),
                            IsActive = System.Convert.ToBoolean(rs["BUDGETTYPE_ACTIVE"])
                        } : null);


                        objList.Add(objBudgetItemNew);
                    }
                }
                rs.Close();
                rs.Dispose();
            }
            catch (System.Exception f)
            {
                objList = null;

                DevExpress.XtraEditors.XtraMessageBox.Show(
                "�� ������� �������� ������ ������ �������.\n\n����� ������: " + f.Message, "��������",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
			finally // ������� ���������� �������
            {
                if (DBConnection != null) { DBConnection.Close(); }
            }

            return objList;
        }
        #endregion

        #region �������� ������ ������ ������� �� ������ ������ ������ �������� *
        /// <summary>
        /// ������� � �� ��������� ������ ������ ������� �� ������ ������ ������ ��������,
        /// ����������� ���������� �������������, � ������� ������ ������
        /// </summary>
        /// <param name="objProfile">�������</param>
        /// <param name="uuidBudgetID">�� �������</param>
        /// <returns>true - �������� ����������; false - ������</returns>
        public static System.Boolean ImportDebitArticleListToBudget(UniXP.Common.CProfile objProfile,
            System.Guid uuidBudgetID)
        {
            System.Boolean bRet = false;
            // ������������ � ��
            System.Data.SqlClient.SqlConnection DBConnection = objProfile.GetDBSource();
            if (DBConnection == null)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show("����������� ���������� � ��.", "��������",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);
                return bRet;
            }

            try
            {
                // ���������� � �� ��������, ����������� ������� �� ������� ������
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
                cmd.Connection = DBConnection;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = System.String.Format("[{0}].[dbo].[sp_ImportDebitArticleListToBudget]", objProfile.GetOptionsDllDBName());
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ((System.Byte)(0)), ((System.Byte)(0)), "", System.Data.DataRowVersion.Current, null));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@BUDGET_GUID_ID", System.Data.SqlDbType.UniqueIdentifier));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ERROR_NUMBER", System.Data.SqlDbType.Int, 8, System.Data.ParameterDirection.Output, false, ((System.Byte)(0)), ((System.Byte)(0)), "", System.Data.DataRowVersion.Current, null));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ERROR_MESSAGE", System.Data.SqlDbType.NVarChar, 4000));
                cmd.Parameters["@ERROR_MESSAGE"].Direction = System.Data.ParameterDirection.Output;
                cmd.Parameters["@BUDGET_GUID_ID"].Value = uuidBudgetID;
                cmd.ExecuteNonQuery();
                System.Int32 iRet = (System.Int32)cmd.Parameters["@RETURN_VALUE"].Value;
                if (iRet == 0)
                {
                    bRet = true;
                }
                else
                {
                    switch (iRet)
                    {
                        case 1:
                            DevExpress.XtraEditors.XtraMessageBox.Show("������ �������� ������ ������ ��������.\n������ � �������� ��������������� �� ������.\n�� �������: " +
                                uuidBudgetID.ToString(), "��������",
                                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                            break;
                        case 2:
                            DevExpress.XtraEditors.XtraMessageBox.Show("������ �������� ������ ������ ��������.\n������ ��� �������� ������ ������.", "��������",
                                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                            break;
                        default:
                            DevExpress.XtraEditors.XtraMessageBox.Show("������ �������� ������ ������ ��������.\n\n\n����� ������: " + (System.String)cmd.Parameters["@ERROR_MESSAGE"].Value, "��������",
                                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                            break;
                    }
                }

                cmd.Dispose();
            }
            catch (System.Exception f)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(
                "������ �������� ������ ������ ��������.\n\n����� ������: " + f.Message, "��������",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
			finally // ������� ���������� �������
            {
                DBConnection.Close();
            }

            return bRet;
        }

        #endregion

        #region �������� �� ����������� ���������/�������� ������ *
        /// <summary>
        /// ���������, ����� �� �������� ������ �������� �� ��
        /// </summary>
        /// <param name="objProfile">�������</param>
        /// <param name="objProfile">�������</param>
        /// <returns>true - �����; false - ������</returns>
        public System.Boolean IsPossibleChangeBudgetItem(UniXP.Common.CProfile objProfile,
            System.Boolean bShowMessage)
        {
            System.Boolean bRes = false;
            // ���������� � ��
            System.Data.SqlClient.SqlConnection DBConnection = objProfile.GetDBSource();
            if (DBConnection == null)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show("����������� ���������� � ��.", "��������",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                return bRes;
            }
            try
            {
                // SQL-������� 
                System.String strDebitArticleList = "";
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
                cmd.Connection = DBConnection;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = System.String.Format("[{0}].[dbo].[sp_ISPossibleChangeBudgetItem]", objProfile.GetOptionsDllDBName());
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ((System.Byte)(0)), ((System.Byte)(0)), "", System.Data.DataRowVersion.Current, null));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@GUID_ID", System.Data.SqlDbType.UniqueIdentifier, 16));
                cmd.Parameters["@GUID_ID"].Value = this.m_uuidID;
                System.Data.SqlClient.SqlDataReader rs = cmd.ExecuteReader();
                if (rs.HasRows)
                {
                    strDebitArticleList = "� ������ ������: " + this.BudgetItemFullName + "\n������ ������ ������ ����������� � �� ������ �������!\n";
                    while (rs.Read())
                    {
                        strDebitArticleList += ("\n" + rs.GetString(1) + " " + rs.GetString(2) + "\n" + rs.GetString(3));
                    }
                }
                else { bRes = true; }

                rs.Close();
                rs.Dispose();
                cmd.Dispose();

                if ((bRes == false) && (bShowMessage == true))
                {
                    DevExpress.XtraEditors.XtraMessageBox.Show(strDebitArticleList, "��������",
                      System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);
                }
            }
            catch (System.Exception e)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(
                "�� ������� ��������� �������� �� ����������� ��������� ������ �������.\n" + e.Message, "��������",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
			finally // ������� ���������� �������
            {
                DBConnection.Close();
            }
            return bRes;
        }
        #endregion

        #region ���������� �������  *
        /// <summary>
        /// ���������� ������ ������ ������� � ��������� ����� � ������������ ������� �� �������
        /// </summary>
        /// <param name="uuidBudgetID">�� �������</param>
        /// <param name="objProfile">�������</param>
        /// <returns>������ ������ �������</returns>
        public static List<CBudgetItemBalans> GetBudgetItemBalansList(System.Guid uuidBudgetID,
            UniXP.Common.CProfile objProfile)
        {
            List<CBudgetItemBalans> objList = new List<CBudgetItemBalans>();

            System.Data.SqlClient.SqlConnection DBConnection = objProfile.GetDBSource();
            if (DBConnection == null) { return objList; }
            try
            {
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
                cmd.Connection = DBConnection;
                cmd.CommandTimeout = 0;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = System.String.Format("[{0}].[dbo].[sp_GetBudgetItemBalansList]", objProfile.GetOptionsDllDBName());
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ((System.Byte)(0)), ((System.Byte)(0)), "", System.Data.DataRowVersion.Current, null));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@BUDGET_GUID_ID", System.Data.SqlDbType.UniqueIdentifier));
                cmd.Parameters["@BUDGET_GUID_ID"].Value = uuidBudgetID;
                System.Data.SqlClient.SqlDataReader rs = cmd.ExecuteReader();
                if (rs.HasRows)
                {
                    // ����� ������ ��������
                    while (rs.Read())
                    {
                        CBudgetItemBalans objBudgetItemBalans = new CBudgetItemBalans();
                        objBudgetItemBalans.DebitArticle = new CDebitArticle(rs.GetGuid(0), rs.GetString(1), rs.GetString(2));
                        objBudgetItemBalans.Month = (enumMonth)rs.GetInt32(3);
                        objBudgetItemBalans.MoneyPlan = (System.Double)rs.GetSqlMoney(4).Value;
                        objBudgetItemBalans.MoneyFact = (System.Double)rs.GetSqlMoney(5).Value;

                        objList.Add(objBudgetItemBalans);
                    }
                }
                rs.Close();
                rs.Dispose();
                cmd.Dispose();
            }
            catch (System.Exception f)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(
                "�� ������� �������� ����� � ���������� �������.\n\n����� ������: " + f.Message, "��������",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
            finally // ������� ���������� �������
            {
                DBConnection.Close();
            }
            return objList;
        }
        /// <summary>
        /// ���������� ������� ������� �� ������������ ������ �������
        /// </summary>
        /// <param name="uuidBudgetItemID">�� ������ �������</param>
        /// <param name="objProfile">�������</param>
        /// <returns>������� �������</returns>
        public static System.Double GetParentBudgetItemBalans(System.Guid uuidBudgetItemID,
            UniXP.Common.CProfile objProfile)
        {
            System.Double mRet = 0;

            System.Data.SqlClient.SqlConnection DBConnection = objProfile.GetDBSource();
            if (DBConnection == null)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show("������ ����������� � ���� ������!", "��������",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                return mRet;
            }
            try
            {
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
                cmd.Connection = DBConnection;
                cmd.CommandTimeout = 0;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = System.String.Format("[{0}].[dbo].[sp_GetBudgetParentItemBalans]", objProfile.GetOptionsDllDBName());
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ((System.Byte)(0)), ((System.Byte)(0)), "", System.Data.DataRowVersion.Current, null));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@BUDGETITEM_GUID_ID", System.Data.SqlDbType.UniqueIdentifier));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@BudgetParentItemBalans", System.Data.SqlDbType.Money));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ERROR_NUM", System.Data.SqlDbType.Int, 8, System.Data.ParameterDirection.Output, false, ((System.Byte)(0)), ((System.Byte)(0)), "", System.Data.DataRowVersion.Current, null));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ERROR_MES", System.Data.SqlDbType.NVarChar, 4000));
                cmd.Parameters["@ERROR_MES"].Direction = System.Data.ParameterDirection.Output;
                cmd.Parameters["@BudgetParentItemBalans"].Direction = System.Data.ParameterDirection.Output;
                cmd.Parameters["@BUDGETITEM_GUID_ID"].Value = uuidBudgetItemID;
                cmd.ExecuteNonQuery();

                if ((System.Int32)cmd.Parameters["@RETURN_VALUE"].Value == 0)
                {
                    mRet = System.Convert.ToDouble(cmd.Parameters["@BudgetParentItemBalans"].Value);
                }
                else
                {
                    DevExpress.XtraEditors.XtraMessageBox.Show(
                    "�� ������� �������� ������� �� ������.\n\n����� ������: " + (System.String)cmd.Parameters["@ERROR_MES"].Value, "��������",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                }
                cmd.Dispose();
            }
            catch (System.Exception f)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(
                "�� ������� �������� ������� �� ������.\n\n����� ������: " + f.Message, "��������",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
            finally // ������� ���������� �������
            {
                DBConnection.Close();
            }
            return mRet;
        }
        #endregion

        #region ������ ����������
        /// <summary>
        /// ���������� ������ ����������
        /// </summary>
        /// <param name="objProfile">�������</param>
        /// <returns>������ ����������</returns>
        public List<structBudgetItemAccTrn> GetAccountTransnList(UniXP.Common.CProfile objProfile)
        {
            List<structBudgetItemAccTrn> objList = new List<structBudgetItemAccTrn>();

            System.Data.SqlClient.SqlConnection DBConnection = objProfile.GetDBSource();
            if (DBConnection == null)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show("����������� ���������� � ��.", "��������",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);
                return objList;
            }

            try
            {
                // ���������� � �� ��������, ����������� ������� �� ������� ������
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
                cmd.Connection = DBConnection;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = System.String.Format("[{0}].[dbo].[sp_GetAccountTransnForBudgetItem]", objProfile.GetOptionsDllDBName());
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ((System.Byte)(0)), ((System.Byte)(0)), "", System.Data.DataRowVersion.Current, null));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@BUDGETITEM_GUID_ID", System.Data.SqlDbType.UniqueIdentifier));
                cmd.Parameters["@BUDGETITEM_GUID_ID"].Value = this.m_uuidID;
                System.Data.SqlClient.SqlDataReader rs = cmd.ExecuteReader();
                if (rs.HasRows)
                {
                    while (rs.Read())
                    {
                        structBudgetItemAccTrn objBudgetItemTrn = new structBudgetItemAccTrn();
                        objBudgetItemTrn.TrnDate = (System.DateTime)rs["ACCOUNTTRANSN_DATE"];
                        objBudgetItemTrn.MoneyTrn = System.Convert.ToDouble(rs["ACCOUNTTRANSN_MONEY"]);
                        objBudgetItemTrn.CurrencyTrn = (System.String)rs["TRN_CURRENCY_CODE"];
                        objBudgetItemTrn.DocDate = (System.DateTime)rs["BUDGETDOC_DATE"];
                        objBudgetItemTrn.MoneyDoc = System.Convert.ToDouble(rs["BUDGETDOC_MONEY"]);
                        objBudgetItemTrn.CurrencyDoc = (System.String)rs["DOC_CURRENCY_CODE"];
                        objBudgetItemTrn.ObjectiveDoc = (System.String)rs["BUDGETDOC_OBJECTIVE"];
                        objBudgetItemTrn.UserTrn = (System.String)rs["USER_LASTNAME"] + " " + (System.String)rs["USER_FIRSTNAME"];

                        System.String strOperName = "";
                        System.Int32 iOperType = (System.Int32)rs["OPERTYPE_ID"];
                        switch (iOperType)
                        {
                            case 0:
                                strOperName = "������ �������";
                                break;
                            case 1:
                                strOperName = "������ ������� � �������";
                                break;
                            case 2:
                                strOperName = "���������� ����� ������ ��������";
                                break;
                            case 3:
                                strOperName = "���������� ����� ������ ��������";
                                break;
                            case 4:
                                strOperName = "������";
                                break;
                            case 5:
                                strOperName = "������� �������";
                                break;
                            case 6:
                                strOperName = "�������� �������";
                                break;
                            default:
                                break;
                        }
                        objBudgetItemTrn.Operation = strOperName;

                        objList.Add(objBudgetItemTrn);
                    }
                }
                rs.Close();
                rs = null;
                cmd = null;
            }
            catch (System.Exception f)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(
                "�� ������� �������� ������ ��������.\n\n����� ������: " + f.Message, "��������",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
            finally // ������� ���������� �������
            {
                DBConnection.Close();
            }

            return objList;
        }

        #endregion

        #region ������ ����������
        /// <summary>
        /// ���������� ������ ��������� ����������
        /// </summary>
        /// <param name="objProfile">�������</param>
        /// <returns>������ ��������� ����������</returns>
        public List<structBudgetItemDoc> GetBudgetDocList(UniXP.Common.CProfile objProfile)
        {
            List<structBudgetItemDoc> objList = new List<structBudgetItemDoc>();

            System.Data.SqlClient.SqlConnection DBConnection = objProfile.GetDBSource();
            if (DBConnection == null)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show("����������� ���������� � ��.", "��������",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);
                return objList;
            }

            try
            {
                // ���������� � �� ��������, ����������� ������� �� ������� ������
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
                cmd.Connection = DBConnection;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = System.String.Format("[{0}].[dbo].[sp_GetBudgetDocListForBudgetItem]", objProfile.GetOptionsDllDBName());
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ((System.Byte)(0)), ((System.Byte)(0)), "", System.Data.DataRowVersion.Current, null));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@BUDGETITEM_GUID_ID", System.Data.SqlDbType.UniqueIdentifier));
                cmd.Parameters["@BUDGETITEM_GUID_ID"].Value = this.m_uuidID;
                System.Data.SqlClient.SqlDataReader rs = cmd.ExecuteReader();
                if (rs.HasRows)
                {
                    while (rs.Read())
                    {
                        structBudgetItemDoc objBudgetItemDoc = new structBudgetItemDoc();
                        objBudgetItemDoc.DocDate = (System.DateTime)rs["BUDGETDOC_DATE"];
                        objBudgetItemDoc.DocMoney = System.Convert.ToDouble(rs["BUDGETDOCITEM_DOCMONEY"]);
                        objBudgetItemDoc.Currency = (System.String)rs["CURRENCY_CODE"];
                        objBudgetItemDoc.Money = System.Convert.ToDouble(rs["BUDGETDOCITEM_MONEY"]);
                        objBudgetItemDoc.DocState = (System.String)rs["BUDGETDOCSTATE_NAME"];
                        objBudgetItemDoc.BudgetItemFullName = (System.String)rs["BUDGETITEM_NUM"] +
                            " " + (System.String)rs["BUDGETITEM_NAME"];
                        objBudgetItemDoc.ObjectiveDoc = (System.String)rs["BUDGETDOC_OBJECTIVE"];
                        objBudgetItemDoc.User = (System.String)rs["USER_LASTNAME"] + " " + (System.String)rs["USER_FIRSTNAME"];

                        objList.Add(objBudgetItemDoc);
                    }
                }
                rs.Close();
                rs = null;
                cmd = null;
            }
            catch (System.Exception f)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(
                "�� ������� �������� ������ ����������.\n\n����� ������: " + f.Message, "��������",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
            finally // ������� ���������� �������
            {
                DBConnection.Close();
            }

            return objList;
        }

        #endregion

        #region ����������� ������ �������
        /// <summary>
        /// ���������� ������ � ������������ ������ ������� � �� ���������
        /// </summary>
        /// <param name="uuidBudgetItemID">�� ������ �������</param>
        /// <returns>������ � ������������ ������ ������� � �� ���������</returns>
        public static List<CBudgetItem> GetBudgetItemDetailList(System.Guid uuidBudgetItemID, UniXP.Common.CProfile objProfile)
        {
            List<CBudgetItem> objList = new List<CBudgetItem>();

            System.Data.SqlClient.SqlConnection DBConnection = objProfile.GetDBSource();
            if (DBConnection == null)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show("����������� ���������� � ��.", "��������",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);
                return objList;
            }

            try
            {
                // ���������� � �� ��������, ����������� ������� �� ������� ������
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
                cmd.Connection = DBConnection;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = System.String.Format("[{0}].[dbo].[sp_GetBudgetItemDetail]", objProfile.GetOptionsDllDBName());
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ((System.Byte)(0)), ((System.Byte)(0)), "", System.Data.DataRowVersion.Current, null));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@BUDGETITEM_GUID_ID", System.Data.SqlDbType.UniqueIdentifier));
                cmd.Parameters["@BUDGETITEM_GUID_ID"].Value = uuidBudgetItemID;
                System.Data.SqlClient.SqlDataReader rs = cmd.ExecuteReader();
                if (rs.HasRows)
                {
                    CBudgetItem objBudgetItem = null;
                    CBudgetItemDecode objBudgetItemDecode = null;
                    System.Guid uuidId = System.Guid.Empty;
                    while (rs.Read())
                    {
                        if (uuidId.CompareTo((System.Guid)rs["BUDGETITEM_GUID_ID"]) != 0)
                        {
                            objBudgetItem = new CBudgetItem();
                            objBudgetItem.uuidID = (System.Guid)rs["BUDGETITEM_GUID_ID"];
                            if (rs["PARENT_GUID_ID"] != System.DBNull.Value)
                            {
                                objBudgetItem.ParentID = (System.Guid)rs["PARENT_GUID_ID"];
                            }
                            objBudgetItem.BudgetItemNum = (System.String)rs["BUDGETITEM_NUM"];
                            objBudgetItem.Name = (System.String)rs["BUDGETITEM_NAME"];
                            objBudgetItem.TransprtRest = (System.Boolean)rs["BUDGETITEM_TRANSPORTREST"];
                            objBudgetItem.DontChange = (System.Boolean)rs["BUDGETITEM_DONTCHANGE"];
                            objBudgetItem.BudgetItemID = (System.Int32)rs["BUDGETITEM_ID"];
                            uuidId = (System.Guid)rs["BUDGETITEM_GUID_ID"];

                            objList.Add(objBudgetItem);
                        }

                        objBudgetItemDecode = new CBudgetItemDecode((enumMonth)rs["MONTH_ID"]);
                        objBudgetItemDecode.MoneyPermit = System.Convert.ToDouble(rs["MONEYPERMIT"]);
                        objBudgetItemDecode.MoneyFact = System.Convert.ToDouble(rs["MONEYFACT"]);
                        objBudgetItemDecode.MoneyReserve = System.Convert.ToDouble(rs["MONEYRESERVE"]);
                        objBudgetItemDecode.MoneyPlanAccept = System.Convert.ToDouble(rs["MONEYPLANACCEPT"]);
                        objBudgetItemDecode.MoneyCredit = System.Convert.ToDouble(rs["MONEYCREDIT"]); 

                        objBudgetItem.AddBudgetItemDecode(objBudgetItemDecode);
                    }
                }
                rs.Close();
                rs = null;
                cmd = null;
            }
            catch (System.Exception f)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(
                "�� ������� �������� ����������� ������ �������.\n\n����� ������: " + f.Message, "��������",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
            finally // ������� ���������� �������
            {
                DBConnection.Close();
            }

            return objList;
        }
        #endregion

        public override string ToString()
        {
            return BudgetItemFullName;
        }

    }
}
