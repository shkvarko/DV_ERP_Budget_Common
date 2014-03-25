using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace ERP_Budget.Common
{
    /// <summary>
    /// ����� "������ ��������"
    /// </summary>
    public class CDebitArticle : IBaseListItem
    {
        #region ����������, ��������, ���������
        /// <summary>
        /// �� ������������ ������ ��������
        /// </summary>
        private System.Guid m_uuidParentID;
        /// <summary>
        /// �� ������������ ������ ��������
        /// </summary>
        public System.Guid ParentID
        {
            get { return m_uuidParentID; }
            set { m_uuidParentID = value; }
        }
        /// <summary>
        /// �������� ������ ��������
        /// </summary>
        private string m_strArticleDescription;
        /// <summary>
        /// �������� ������ ��������
        /// </summary>
        public string ArticleDescription
        {
            get { return m_strArticleDescription; }
            set { m_strArticleDescription = value; }
        }
        /// <summary>
        /// ����� ������ ��������
        /// </summary>
        private string m_strArticleNum;
        /// <summary>
        /// ����� ������ ��������
        /// </summary>
        public string ArticleNum
        {
            get { return m_strArticleNum; }
            set { m_strArticleNum = value; }
        }
        /// <summary>
        /// ����� �� ������� � �����
        /// </summary>
        private System.Int32 m_iArticleID;
        /// <summary>
        /// ����� �� ������� � �����
        /// </summary>
        public System.Int32 ArticleID
        {
            get { return m_iArticleID; }
            set { m_iArticleID = value; }
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
            get
            {
                return m_bReadOnly;
            }
            set
            {
                m_bReadOnly = value;
            }
        }
        /// <summary>
        /// ������� ����� � ����������� ���������� ���������������
        /// </summary>
        private System.Boolean m_bMultiBudgetDept;
        /// <summary>
        /// ������� ����� � ����������� ���������� ���������������
        /// </summary>
        public System.Boolean MultiBudgetDept
        {
            get { return m_bMultiBudgetDept; }
            set { m_bMultiBudgetDept = value; }
        }
        /// <summary>
        /// ������ ��������� �������������
        /// </summary>
        private List<CBudgetDep> m_objBudgetDepList;
        /// <summary>
        /// ������ ��������� �������������
        /// </summary>
        public List<ERP_Budget.Common.CBudgetDep> BudgetDepList
        {
            get { return m_objBudgetDepList; }
            set { m_objBudgetDepList = value; }
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
        public System.String ArticleFullName
        {
            get { return (String.Format("{0} {1}", m_strArticleNum, Name)); }
        }
        /// <summary>
        /// ���������� ���
        /// </summary>
        private System.Int32 m_iYear;
        /// <summary>
        /// ���������� ���
        /// </summary>
        public System.Int32 FinancislYear
        {
            get { return m_iYear; }
            set { m_iYear = value; }
        }
        /// <summary>
        /// ����
        /// </summary>
        public CAccountPlan AccountPlan { get; set; }
        /// <summary>
        /// ��� ��������
        /// </summary>
        public CBudgetExpenseType BudgetExpenseType { get; set; }
        #endregion

        #region ������������
        public CDebitArticle()
        {
            this.m_uuidID = System.Guid.Empty;
            this.m_strName = "";
            this.m_uuidParentID = System.Guid.Empty;
            this.m_strArticleDescription = "";
            this.m_strArticleNum = "";
            this.m_bReadOnly = false;
            this.m_objBudgetDepList = new List<ERP_Budget.Common.CBudgetDep>();
            this.TransprtRest = false;
            this.m_bDontChange = false;
            this.m_bMultiBudgetDept = false;
            this.m_iArticleID = 0;
            this.m_iYear = 0;
            AccountPlan = null;
            BudgetExpenseType = null;
        }

        public CDebitArticle( System.Guid uuidID )
        {
            this.m_uuidID = uuidID;
            this.m_strName = "";
            this.m_uuidParentID = System.Guid.Empty;
            this.m_strArticleDescription = "";
            this.m_strArticleNum = "";
            this.m_bReadOnly = false;
            this.m_objBudgetDepList = new List<ERP_Budget.Common.CBudgetDep>();
            this.TransprtRest = false;
            this.m_bDontChange = false;
            this.m_bMultiBudgetDept = false;
            this.m_iArticleID = 0;
            this.m_iYear = 0;
            AccountPlan = null;
            BudgetExpenseType = null;
        }

        public CDebitArticle( System.Guid uuidID, System.String strArticleNum, System.String m_strName )
        {
            this.m_uuidID = uuidID;
            this.m_strName = m_strName;
            this.m_uuidParentID = System.Guid.Empty;
            this.m_strArticleDescription = "";
            this.m_strArticleNum = strArticleNum;
            this.m_bReadOnly = false;
            this.m_objBudgetDepList = new List<CBudgetDep>();
            this.TransprtRest = false;
            this.m_bDontChange = false;
            this.m_bMultiBudgetDept = false;
            this.m_iArticleID = 0;
            this.m_iYear = 0;
            AccountPlan = null;
            BudgetExpenseType = null;
        }

        #endregion

        #region Copy
        /// <summary>
        /// ������� ������ "������ ��������" �� ������ ������� �������
        /// </summary>
        /// <param name="objDebitArticleSrc">���������� ������ "������ ��������"</param>
        /// <returns>������ "������ ��������"</returns>
        public static CDebitArticle Copy( CDebitArticle objDebitArticleSrc )
        {
            CDebitArticle objDebitArticle = new CDebitArticle();

            try
            {
                objDebitArticle.m_uuidID = objDebitArticleSrc.m_uuidID;
                objDebitArticle.m_uuidParentID = ( objDebitArticleSrc.m_uuidParentID.CompareTo( System.Guid.Empty ) == 0 ) ? System.Guid.Empty : objDebitArticleSrc.m_uuidParentID;
                objDebitArticle.m_strName = objDebitArticleSrc.Name;
                objDebitArticle.m_strArticleNum = objDebitArticleSrc.m_strArticleNum;
                objDebitArticle.m_strArticleDescription = objDebitArticleSrc.m_strArticleDescription;
                objDebitArticle.m_bTransprtRest = objDebitArticleSrc.m_bTransprtRest;
                objDebitArticle.m_bDontChange = objDebitArticleSrc.m_bDontChange;
                objDebitArticle.m_iArticleID = objDebitArticleSrc.m_iArticleID;
                objDebitArticle.m_iYear = objDebitArticleSrc.m_iYear;
                objDebitArticle.AccountPlan = objDebitArticleSrc.AccountPlan;
                CBudgetDep objBudgetDepCopy = null;
                foreach (CBudgetDep objBudgetDep in objDebitArticleSrc.m_objBudgetDepList)
                {
                    objBudgetDepCopy = new CBudgetDep(objBudgetDep.uuidID, objBudgetDep.Name, objBudgetDep.Manager);
                    objBudgetDepCopy.BudgetExpenseType = objBudgetDep.BudgetExpenseType;
                    objBudgetDepCopy.BudgetProject = objBudgetDep.BudgetProject;

                    objDebitArticle.m_objBudgetDepList.Add(objBudgetDepCopy);
                }
            }
            catch( System.Exception f )
            {
                DevExpress.XtraEditors.XtraMessageBox.Show( 
                "�� ������� ����������� �������� ������� \"������ ��������\".\n\n����� ������: " + f.Message, "��������",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
            }
            return objDebitArticle;
        }

        #endregion

        #region ������ ������ ��������
        /// <summary>
        /// ���������� ������ ������ ��������
        /// </summary>
        /// <param name="objProfile">�������</param>
        /// <returns>������ ������� ������ ��������</returns>
        public static List<CDebitArticle> GetDebitArticleList( UniXP.Common.CProfile objProfile )
        {

            List<CDebitArticle> objList = new List<CDebitArticle>();
            System.Data.SqlClient.SqlConnection DBConnection = objProfile.GetDBSource();
            if( DBConnection == null ) { return objList; }

            try
            {
                // ���������� � �� ��������, ����������� ������� �� ������� ������
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand() { Connection = DBConnection, 
                    CommandType = System.Data.CommandType.StoredProcedure, CommandText = System.String.Format("[{0}].[dbo].[sp_GetDebitArticle]", 
                    objProfile.GetOptionsDllDBName()) };
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ( ( System.Byte )( 0 ) ), ( ( System.Byte )( 0 ) ), "", System.Data.DataRowVersion.Current, null ) );
                System.Data.SqlClient.SqlDataReader rs = cmd.ExecuteReader();
                if( rs.HasRows )
                {
                    while( rs.Read() )
                    {
                        objList.Add( 
                            new CDebitArticle()
                                {
                                    uuidID = (System.Guid)rs["GUID_ID"],
                                    ArticleNum = System.Convert.ToString(rs["DEBITARTICLE_NUM"]), 
                                    Name = System.Convert.ToString( rs["DEBITARTICLE_NAME"] ), 
                                    FinancislYear = ( ( rs["DebitArticleYear"] != System.DBNull.Value ) ? System.Convert.ToInt32(rs["DebitArticleYear"]) : 0 ), 
                                    ParentID = ( ( rs["PARENT_GUID_ID"] != System.DBNull.Value ) ?(System.Guid)rs["PARENT_GUID_ID"] : System.Guid.Empty ), 
                                    ArticleDescription = ( ( rs["DEBITARTICLE_DESCRIPTION"] != System.DBNull.Value ) ? System.Convert.ToString(rs["DEBITARTICLE_DESCRIPTION"]) : "" ), 
                                    TransprtRest = ( ( rs["DEBITARTICLE_TRANSPORTREST"] != System.DBNull.Value ) ? System.Convert.ToBoolean( rs["DEBITARTICLE_TRANSPORTREST"] ) : false ), 
                                    DontChange = ( ( rs["DEBITARTICLE_DONTCHANGE"] != System.DBNull.Value ) ? System.Convert.ToBoolean( rs["DEBITARTICLE_DONTCHANGE"] ) : false ),
                                    ArticleID = System.Convert.ToInt32(rs["DEBITARTICLE_ID"]),
                                    AccountPlan = ( ( rs["ACCOUNTPLAN_GUID"] != System.DBNull.Value ) ? new CAccountPlan()
                                    {
                                        uuidID = (System.Guid)rs["ACCOUNTPLAN_GUID"],
                                        Name = System.Convert.ToString(rs["ACCOUNTPLAN_NAME"]),
                                        IsActive = System.Convert.ToBoolean(rs["ACCOUNTPLAN_ACTIVE"]),
                                        CodeIn1C = System.Convert.ToString(rs["ACCOUNTPLAN_1C_CODE"])
                                    } : null  )  
                                } 
                            );

                    }
                }
                rs.Close();
                rs.Dispose();
                cmd.Dispose();
            }
            catch( System.Exception f )
            {
                DevExpress.XtraEditors.XtraMessageBox.Show( 
                "�� ������� �������� ������ ������ ��������.\n\n����� ������: " + f.Message, "��������",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
            }
			finally // ������� ���������� �������
            {
                DBConnection.Close();
            }
            return objList;
        }
        /// <summary>
        /// ���������� ������ ������ ��������, ��������������� �������� ����������
        /// </summary>
        /// <param name="objProfile">�������</param>
        /// <param name="BudgetDep_Guid">�� ���������� �������������</param>
        /// <param name="DebitArticle_Num">� ������ ��������</param>
        /// <param name="DebitArticle_Name">������������ ������ ��������</param>
        /// <param name="PeriodBeginDate">������ �������� ������ ��������</param>
        /// <param name="strErr">����� ������</param>
        /// <returns>������ �������� ������ "CDebitArticle"</returns>
        public static List<CDebitArticle> GetDebitArticleList(UniXP.Common.CProfile objProfile, System.Guid BudgetDep_Guid,
            System.String DebitArticle_Num, System.String DebitArticle_Name, System.DateTime PeriodBeginDate, ref System.String strErr )
        {

            List<CDebitArticle> objList = new List<CDebitArticle>();
            System.Data.SqlClient.SqlConnection DBConnection = objProfile.GetDBSource();
            if (DBConnection == null) 
            { 
                strErr += ("\n�� ������� �������� ����������� � ��.");
                return objList; 
            }

            try
            {
                // ���������� � �� ��������, ����������� ������� �� ������� ������
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand()
                {
                    Connection = DBConnection,
                    CommandType = System.Data.CommandType.StoredProcedure,
                    CommandText = System.String.Format("[{0}].[dbo].[usp_GetDebitArticleByNumNamePeriodBudgetDep]",
                        objProfile.GetOptionsDllDBName())
                };
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@BudgetDep_Guid", System.Data.SqlDbType.UniqueIdentifier));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@DebitArticle_Num", System.Data.DbType.String));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@DebitArticle_Name", System.Data.DbType.String));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@PeriodBeginDate", System.Data.SqlDbType.DateTime));

                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ERROR_NUMBER", System.Data.SqlDbType.Int, 8, System.Data.ParameterDirection.Output, false, ((System.Byte)(0)), ((System.Byte)(0)), "", System.Data.DataRowVersion.Current, null));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ERROR_MESSAGE", System.Data.SqlDbType.NVarChar, 4000) { Direction = System.Data.ParameterDirection.Output });
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ((System.Byte)(0)), ((System.Byte)(0)), "", System.Data.DataRowVersion.Current, null));

                cmd.Parameters["@BudgetDep_Guid"].Value = BudgetDep_Guid;
                cmd.Parameters["@DebitArticle_Num"].Value = DebitArticle_Num;
                cmd.Parameters["@DebitArticle_Name"].Value = DebitArticle_Name;
                cmd.Parameters["@PeriodBeginDate"].Value = PeriodBeginDate;
                
                System.Data.SqlClient.SqlDataReader rs = cmd.ExecuteReader();
                if (rs.HasRows)
                {
                    while (rs.Read())
                    {
                        objList.Add(
                            new CDebitArticle()
                            {
                                uuidID = (System.Guid)rs["GUID_ID"],
                                ArticleNum = System.Convert.ToString(rs["DEBITARTICLE_NUM"]),
                                Name = System.Convert.ToString(rs["DEBITARTICLE_NAME"]),
                                FinancislYear = PeriodBeginDate.Year,
                                ParentID = ((rs["PARENT_GUID_ID"] != System.DBNull.Value) ? (System.Guid)rs["PARENT_GUID_ID"] : System.Guid.Empty),
                                ArticleDescription = ((rs["DEBITARTICLE_DESCRIPTION"] != System.DBNull.Value) ? System.Convert.ToString(rs["DEBITARTICLE_DESCRIPTION"]) : ""),
                                TransprtRest = ((rs["DEBITARTICLE_TRANSPORTREST"] != System.DBNull.Value) ? System.Convert.ToBoolean(rs["DEBITARTICLE_TRANSPORTREST"]) : false),
                                DontChange = ((rs["DEBITARTICLE_DONTCHANGE"] != System.DBNull.Value) ? System.Convert.ToBoolean(rs["DEBITARTICLE_DONTCHANGE"]) : false),
                                ArticleID = System.Convert.ToInt32(rs["DEBITARTICLE_ID"]),
                                AccountPlan = ((rs["ACCOUNTPLAN_GUID"] != System.DBNull.Value) ? new CAccountPlan()
                                {
                                    uuidID = (System.Guid)rs["ACCOUNTPLAN_GUID"],
                                    Name = System.Convert.ToString(rs["ACCOUNTPLAN_NAME"]),
                                    IsActive = System.Convert.ToBoolean(rs["ACCOUNTPLAN_ACTIVE"]),
                                    CodeIn1C = System.Convert.ToString(rs["ACCOUNTPLAN_1C_CODE"])
                                } : null), 
                                BudgetExpenseType = ((rs["BUDGETEXPENSETYPE_GUID"] != System.DBNull.Value) ? new CBudgetExpenseType()
                                {
                                    uuidID = (System.Guid)rs["BUDGETEXPENSETYPE_GUID"], 
                                    Name = System.Convert.ToString(rs["BUDGETEXPENSETYPE_NAME"]), 
                                    IsActive = System.Convert.ToBoolean(rs["BUDGETEXPENSETYPE_ACTIVE"])
                                } : null)
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
                strErr += ("\n�� ������� �������� ������ ������ ��������.\n\n����� ������: " + f.Message);
            }
			finally // ������� ���������� �������
            {
                DBConnection.Close();
            }
            return objList;
        }
        
        
        #endregion

        #region ���������� ������ ������ ��������
        /// <summary>
        /// ��������� ������ ������ �������� 
        /// </summary>
        /// <param name="objProfile">�������</param>
        /// <param name="objTreeList">������ ������ ��������</param>
        /// <returns>true - �������� ����������;false - ������</returns>
        public static System.Boolean LoadDebitArticleList( UniXP.Common.CProfile objProfile,
            DevExpress.XtraTreeList.TreeList objTreeList, DevExpress.XtraTreeList.Columns.TreeListColumn objColumnYear )
        {
            System.Boolean bRet = false;
            if( objTreeList == null )
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(  "������ ������ �� ���������!", "��������",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning );
                return bRet;
            }
            objTreeList.ClearNodes();

            // ������������ � ��
            System.Data.SqlClient.SqlConnection DBConnection = objProfile.GetDBSource();
            if( DBConnection == null )
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(  "����������� ���������� � ��.", "��������",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning );
                return bRet;
            }

            try
            {
                // ���������� � �� ��������, ����������� ������� �� ������� ������
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand() { Connection = DBConnection, CommandType = System.Data.CommandType.StoredProcedure };

                // ����������� ������ ������������ ������ ��������
                List<CDebitArticle> objDebitArticleParentList = GetDebitArticleParentList( objProfile, cmd );
                if( objDebitArticleParentList.Count > 0 )
                {
                    bRet = true;
                    // �������� ������ ����� ��� ������ ������ ��������
                    foreach( CDebitArticle objDebitArticleParent in objDebitArticleParentList )
                    {
                        bRet = LoadDebitArticleBudgetDepList( objProfile, cmd, objDebitArticleParent );
                        if( bRet == false ) { break; }
                    }
                    if( bRet == false ) { objTreeList.Nodes.Clear(); }
                    else
                    {
                        //������ �������� ���� �������� ������
                        DevExpress.XtraTreeList.Nodes.TreeListNode objNodeYear = null;
                        foreach (CDebitArticle objDebitArticle in objDebitArticleParentList)
                        {
                            //��������� ���� � ������
                            objNodeYear = null;
                            foreach (DevExpress.XtraTreeList.Nodes.TreeListNode objItem in objTreeList.Nodes)
                            {
                                if (System.Convert.ToString(objItem.GetValue( objColumnYear ) ).Equals( objDebitArticle.m_iYear.ToString() ) == true )
                                {
                                    objNodeYear = objItem;
                                    break;
                                }
                            }

                            if (objNodeYear == null)
                            {
                                objNodeYear = objTreeList.AppendNode(new object[] { System.Guid.Empty , null, 
                                objDebitArticle.m_iYear.ToString(), null,  false }, null);
                            }

                            DevExpress.XtraTreeList.Nodes.TreeListNode objNode = 
                            objTreeList.AppendNode( new object[] { objDebitArticle.m_uuidID, null, 
                                objDebitArticle.ArticleFullName, 
                                ( ( objDebitArticle.AccountPlan == null ) ? "" : objDebitArticle.AccountPlan.FullName ), 
                                false }, objNodeYear);

                            objNode.Tag = objDebitArticle;
                        }

                        // ��� ������� ���� �� ������� �������� ������ ������ �������� ���������
                        foreach (DevExpress.XtraTreeList.Nodes.TreeListNode objYear in objTreeList.Nodes)
                        {
                            foreach (DevExpress.XtraTreeList.Nodes.TreeListNode objNodeParent in objYear.Nodes)
                            {
                                // ��������� ����������� ������� ��� �������� ���������
                                bRet = LoadChildDebitArticleNodes(objProfile, cmd, objNodeParent, objTreeList);
                                if (bRet == false) { break; }
                            }
                        }
                        if( bRet == false ) { objTreeList.Nodes.Clear(); }
                    }
                }
                else
                {
                    bRet = true;
                }

                cmd.Dispose();
            }
            catch( System.Exception f )
            {
                objTreeList.Nodes.Clear();
                DevExpress.XtraEditors.XtraMessageBox.Show( 
                "�� ������� �������� ������ ������ ��������.\n\n����� ������: " + f.Message, "��������",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
            }
			finally // ������� ���������� �������
            {
                DBConnection.Close();
            }

            return bRet;
        }
        /// <summary>
        /// ��������� ������ ������ �������� ��� ������� 
        /// </summary>
        /// <param name="objProfile">�������</param>
        /// <param name="objTreeList">������ ������ ��������</param>
        /// <param name="objBudget">������</param>
        /// <returns>true - �������� ����������;false - ������</returns>
        public static System.Boolean LoadDebitArticleListForBudget(UniXP.Common.CProfile objProfile,
            DevExpress.XtraTreeList.TreeList objTreeList, CBudget objBudget, DevExpress.XtraTreeList.Columns.TreeListColumn objColumnYear)
        {
            System.Boolean bRet = false;
            if (objTreeList == null)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show("������ ������ �� ���������!", "��������",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);
                return bRet;
            }
            objTreeList.ClearNodes();

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
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand() { Connection = DBConnection, CommandType = System.Data.CommandType.StoredProcedure };

                // ����������� ������ ������������ ������ ��������
                List<CDebitArticle> objDebitArticleParentList = GetDebitArticleParentList(objProfile, cmd);
                if (objDebitArticleParentList.Count > 0)
                {
                    bRet = true;
                    // ������ "������" ������ - �� ��� ���������� ���
                    objDebitArticleParentList = objDebitArticleParentList.Where<CDebitArticle>(x => x.FinancislYear == objBudget.Date.Year).ToList<CDebitArticle>();
                    
                    // �������� ������ ����� ��� ������ ������ ��������
                    foreach (CDebitArticle objDebitArticleParent in objDebitArticleParentList)
                    {
                        LoadDebitArticleBudgetDepList(objProfile, cmd, objDebitArticleParent);
                    }

                    // ������ ����������� ������ ������ �� �������, ��� ������ ������������� �� ������
                    objDebitArticleParentList = objDebitArticleParentList.Where<CDebitArticle>(x => x.BudgetDepList != null).ToList<CDebitArticle>();
                    objDebitArticleParentList = objDebitArticleParentList.Where<CDebitArticle>(x => x.BudgetDepList.Count > 0).ToList<CDebitArticle>();

                    // ������ �������� ������ �� ������, ������� ��������� �������������, ���������� � ������� "������"
                    objDebitArticleParentList = objDebitArticleParentList.Where<CDebitArticle>(x => x.BudgetDepList.FirstOrDefault<CBudgetDep>(y => y.uuidID.Equals(objBudget.BudgetDep.uuidID)) != null).ToList<CDebitArticle>();

                    if( ( objDebitArticleParentList != null ) && ( objDebitArticleParentList.Count > 0 ) ) 
                    {
                        //������ �������� ���� �������� ������
                        DevExpress.XtraTreeList.Nodes.TreeListNode objNodeYear = null;
                        foreach (CDebitArticle objDebitArticle in objDebitArticleParentList)
                        {
                            //��������� ���� � ������
                            objNodeYear = null;
                            foreach (DevExpress.XtraTreeList.Nodes.TreeListNode objItem in objTreeList.Nodes)
                            {
                                if (System.Convert.ToString(objItem.GetValue(objColumnYear)) == objDebitArticle.m_iYear.ToString())
                                {
                                    objNodeYear = objItem;
                                    break;
                                }
                            }

                            if (objNodeYear == null)
                            {
                                objNodeYear = objTreeList.AppendNode(new object[] { System.Guid.Empty , null, 
                                objDebitArticle.m_iYear.ToString(), null,  false }, null);
                            }

                            DevExpress.XtraTreeList.Nodes.TreeListNode objNode =
                            objTreeList.AppendNode(new object[] { objDebitArticle.m_uuidID, null, 
                                objDebitArticle.ArticleFullName, 
                                ( ( objDebitArticle.AccountPlan == null ) ? "" : objDebitArticle.AccountPlan.FullName ), 
                                false }, objNodeYear);

                            objNode.Tag = objDebitArticle;
                        }

                        // ��� ������� ���� �� ������� �������� ������ ������ �������� ���������
                        foreach (DevExpress.XtraTreeList.Nodes.TreeListNode objYear in objTreeList.Nodes)
                        {
                            foreach (DevExpress.XtraTreeList.Nodes.TreeListNode objNodeParent in objYear.Nodes)
                            {
                                // ��������� ����������� ������� ��� �������� ���������
                                bRet = LoadChildDebitArticleNodes(objProfile, cmd, objNodeParent, objTreeList);
                                if (bRet == false) { break; }
                            }
                        }
                        if (bRet == false) { objTreeList.Nodes.Clear(); }
                    }
                }
                else
                {
                    bRet = true;
                }

                cmd.Dispose();
            }
            catch (System.Exception f)
            {
                objTreeList.Nodes.Clear();
                DevExpress.XtraEditors.XtraMessageBox.Show(
                "�� ������� �������� ������ ������ ��������.\n\n����� ������: " + f.Message, "��������",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
			finally // ������� ���������� �������
            {
                DBConnection.Close();
                objTreeList.Enabled = true;
            }

            return bRet;
        }
        /// <summary>
        /// ���������� ������ ������������ ������ ��������
        /// </summary>
        /// <param name="objProfile">�������</param>
        /// <param name="cmd">SQL-�������</param>
        /// <returns>������ ������������ ������ ��������</returns>
        public static List<CDebitArticle> GetDebitArticleParentList( UniXP.Common.CProfile objProfile,
            System.Data.SqlClient.SqlCommand cmd )
        {
            List<CDebitArticle> objList = new List<CDebitArticle>();

            if( cmd == null )
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(  "����������� ���������� � ��.", "��������",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning );
                return objList;
            }

            try
            {
                cmd.Parameters.Clear();
                cmd.CommandText = System.String.Format( "[{0}].[dbo].[sp_GetDebitArticleParent]", objProfile.GetOptionsDllDBName() );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ( ( System.Byte )( 0 ) ), ( ( System.Byte )( 0 ) ), "", System.Data.DataRowVersion.Current, null ) );
                System.Data.SqlClient.SqlDataReader rs = cmd.ExecuteReader();
                if( rs.HasRows )
                {
                    while( rs.Read() )
                    {
                        // ������� ������ ������ "������ ��������"
                        objList.Add(new CDebitArticle()
                            {
                                uuidID = (System.Guid)rs["GUID_ID"],
                                ArticleNum = System.Convert.ToString(rs["DEBITARTICLE_NUM"]), 
                                Name = System.Convert.ToString( rs["DEBITARTICLE_NAME"] ), 
                                FinancislYear = ( ( rs["DebitArticleYear"] != System.DBNull.Value ) ? System.Convert.ToInt32(rs["DebitArticleYear"]) : 0 ), 
                                ParentID = ( ( rs["PARENT_GUID_ID"] != System.DBNull.Value ) ?(System.Guid)rs["PARENT_GUID_ID"] : System.Guid.Empty ), 
                                ArticleDescription = ( ( rs["DEBITARTICLE_DESCRIPTION"] != System.DBNull.Value ) ? System.Convert.ToString(rs["DEBITARTICLE_DESCRIPTION"]) : "" ), 
                                TransprtRest = ( ( rs["DEBITARTICLE_TRANSPORTREST"] != System.DBNull.Value ) ? System.Convert.ToBoolean( rs["DEBITARTICLE_TRANSPORTREST"] ) : false ), 
                                DontChange = ( ( rs["DEBITARTICLE_DONTCHANGE"] != System.DBNull.Value ) ? System.Convert.ToBoolean( rs["DEBITARTICLE_DONTCHANGE"] ) : false ),
                                ArticleID = System.Convert.ToInt32(rs["DEBITARTICLE_ID"]),
                                AccountPlan = ( ( rs["ACCOUNTPLAN_GUID"] != System.DBNull.Value ) ? new CAccountPlan()
                                {
                                    uuidID = (System.Guid)rs["ACCOUNTPLAN_GUID"],
                                    Name = System.Convert.ToString(rs["ACCOUNTPLAN_NAME"]),
                                    IsActive = System.Convert.ToBoolean(rs["ACCOUNTPLAN_ACTIVE"]),
                                    CodeIn1C = System.Convert.ToString(rs["ACCOUNTPLAN_1C_CODE"])
                                } : null  )  
                            } 
                        );

                    }
                }

                rs.Close();
                rs.Dispose();
            }
            catch( System.Exception f )
            {
                DevExpress.XtraEditors.XtraMessageBox.Show( 
                "�� ������� �������� ������ ������ ��������.\n\n����� ������: " + f.Message, "��������",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
            }
			finally // ������� ���������� �������
            {
            }
            return objList;
        }

        /// <summary>
        /// ��������� ������ ��������� ������������� � ������ ��������
        /// </summary>
        /// <param name="objProfile">�������</param>
        /// <param name="cmd">SQL-�������</param>
        /// <param name="objDebitArticle">������ ��������</param>
        /// <returns>true - �������� ����������;false - ������</returns>
        public static System.Boolean LoadDebitArticleBudgetDepList( UniXP.Common.CProfile objProfile,
            System.Data.SqlClient.SqlCommand cmd, CDebitArticle objDebitArticle )
        {
            System.Boolean bRet = false;
            if( objDebitArticle == null )
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(  "������ \"������ ��������\" �� ���������!", "��������",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning );
                return bRet;
            }

            if( cmd == null )
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(  "����������� ���������� � ��.", "��������",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning );
                return bRet;
            }

            try
            {
                cmd.Parameters.Clear();
                cmd.CommandText = System.String.Format( "[{0}].[dbo].[sp_GetDebitArticleBudgetDep]", objProfile.GetOptionsDllDBName() );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ( ( System.Byte )( 0 ) ), ( ( System.Byte )( 0 ) ), "", System.Data.DataRowVersion.Current, null ) );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@DEBITARTICLE_GUID_ID", System.Data.SqlDbType.UniqueIdentifier ) );
                cmd.Parameters[ "@DEBITARTICLE_GUID_ID" ].Value = objDebitArticle.m_uuidID;
                System.Data.SqlClient.SqlDataReader rs = cmd.ExecuteReader();
                if( rs.HasRows )
                {
                    while( rs.Read() )
                    {
                        // ��������� ������ � ������ ��������� �������������
                        objDebitArticle.BudgetDepList.Add(
                            new CBudgetDep()
                            {
                                uuidID = (System.Guid)rs["BUDGETDEP_GUID_ID"],
                                Name = System.Convert.ToString(rs["BUDGETDEP_NAME"]),
                                ParentID = ((rs["PARENT_GUID_ID"] != System.DBNull.Value) ? (System.Guid)rs["PARENT_GUID_ID"] : System.Guid.Empty),
                                Manager = new CUser(System.Convert.ToInt32(rs["BUDGETDEP_MANAGER"]), System.Convert.ToInt32(rs["UniXPUserID"]),
                                    System.Convert.ToString(rs["strLastName"]), System.Convert.ToString(rs["strFirstName"])),
                                BudgetExpenseType = ((rs["BUDGETEXPENSETYPE_GUID"] != System.DBNull.Value) ? new CBudgetExpenseType((System.Guid)rs["BUDGETEXPENSETYPE_GUID"],
                                (System.String)rs["BUDGETEXPENSETYPE_NAME"],
                                ((rs["BUDGETEXPENSETYPE_DESCRIPTION"] == System.DBNull.Value) ? "" : (System.String)rs["BUDGETEXPENSETYPE_DESCRIPTION"])) : null),
                                BudgetProject = ((rs["BUDGETPROJECT_GUID"] != System.DBNull.Value) ? new CBudgetProject()
                                    {
                                        uuidID = (System.Guid)rs["BUDGETPROJECT_GUID"],
                                        Name = System.Convert.ToString(rs["BUDGETPROJECT_NAME"]),
                                        Description = "",
                                        IsActive = System.Convert.ToBoolean(rs["BUDGETPROJECT_ACTIVE"]),
                                        CodeIn1C = System.Convert.ToInt32(rs["BUDGETPROJECT_1C_CODE"])
                                    } : null)
                            }
                            );
                    }
                }

                rs.Close();
                rs.Dispose();
                bRet = true;
            }
            catch( System.Exception f )
            {
                DevExpress.XtraEditors.XtraMessageBox.Show( 
                "�� ������� �������� ������ ��������� ������������� ��� ������ ��������.\n\n����� ������: " + f.Message, "��������",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
            }
			finally // ������� ���������� �������
            {
            }
            return bRet;
        }

        /// <summary>
        /// ������� ������ ��������� ��������
        /// </summary>
        /// <param name="objProfile">�������</param>
        /// <param name="cmd">SQL-�������</param>
        /// <param name="objTreeList">������ ������ ��������</param>
        /// <param name="objNodeParent">���� ������</param>
        /// <returns>true - �������� ����������;false - ������</returns>
        public static System.Boolean LoadChildDebitArticleNodes( UniXP.Common.CProfile objProfile,
            System.Data.SqlClient.SqlCommand cmd,
            DevExpress.XtraTreeList.Nodes.TreeListNode objNodeParent,
            DevExpress.XtraTreeList.TreeList objTreeList )
        {
            System.Boolean bRet = false;
            if( ( objNodeParent == null ) || ( objNodeParent.Tag == null ) )
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(  "������ ���� ������ �� ��������� \n���� � ���� ����������� ������ �� ������ ��������!", "��������",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning );
                return bRet;
            }
            if( cmd == null )
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(  "����������� ���������� � ��.", "��������",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning );
                return bRet;
            }

            try
            {
                // � ������������ ���� � ���� ����� ������, �������� ��
                CDebitArticle objDebitArticleParent = ( CDebitArticle )objNodeParent.Tag;
                if( objDebitArticleParent == null )
                {
                    DevExpress.XtraEditors.XtraMessageBox.Show(  "�� ������� �������� ������������ ������ ��������.", "��������",
                        System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning );
                    return bRet;
                }
                else
                {
                    // ������ �������� ���������
                    bRet = true;
                    List<CDebitArticle> objDebitArticleList = GetChildDebitArticleList( objProfile, cmd, objDebitArticleParent.uuidID );
                    if( ( objDebitArticleList != null ) && ( objDebitArticleList.Count > 0) )
                    {
                        // ��������� � ��������� ������ �����
                        foreach( CDebitArticle objDebitArticle in objDebitArticleList )
                        {
                            LoadDebitArticleBudgetDepList( objProfile, cmd, objDebitArticle );
                        }
                        // ������ ��� ������ ��������� � ������ ������ ����
                        foreach (CDebitArticle objDebitArticleItem in objDebitArticleList)
                        {
                            //��������� ���� � ������
                            objTreeList.AppendNode(new object[] { 
                                objDebitArticleItem.m_uuidID, objDebitArticleItem.m_uuidParentID, 
                                objDebitArticleItem.ArticleFullName, 
                                ( ( objDebitArticleItem.AccountPlan == null ) ? "" : objDebitArticleItem.AccountPlan.FullName ), 
                                false }, objNodeParent).Tag = objDebitArticleItem;
                        }

                        // ��� ������� ���� � ���������� �������� ������ ������ �������� ���������
                        foreach (DevExpress.XtraTreeList.Nodes.TreeListNode objNodeChildt in objNodeParent.Nodes)
                        {
                            // ��������� ����������� ������� ��� �������� ���������
                            bRet = LoadChildDebitArticleNodes(objProfile, cmd, objNodeChildt, objTreeList);
                            if (bRet == false) { break; }
                        }
                    }
                }
            }
            catch( System.Exception f )
            {
                bRet = false;

                DevExpress.XtraEditors.XtraMessageBox.Show( 
                "�� ������� ��������� ������ ��������� ��������.\n\n����� ������: " + f.Message, "��������",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
            }
			finally // ������� ���������� �������
            {
            }
            return bRet;
        }

        /// <summary>
        /// ���������� ������ ���� ��������� � ��������� � ���������� �������������
        /// </summary>
        /// <param name="objProfile">�������</param>
        /// <param name="cmd">SQl-�������</param>
        /// <param name="uuidDebitArticleParent">�� ����������� ������ ��������</param>
        /// <returns>������ ���� ��������� � ��������� � ���������� �������������</returns>
        public static List<CDebitArticle> GetChildDebitArticleList( UniXP.Common.CProfile objProfile,
            System.Data.SqlClient.SqlCommand cmd, System.Guid uuidDebitArticleParent )
        {
            List<CDebitArticle> objChildDebitArticleList = new List<CDebitArticle>();
            // ������������ � ��
            if( cmd == null )
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(  "����������� ���������� � ��.", "��������",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning );
                return objChildDebitArticleList;
            }
            try
            {
                cmd.Parameters.Clear();
                cmd.CommandText = System.String.Format( "[{0}].[dbo].[sp_GetDebitArticleChild]", objProfile.GetOptionsDllDBName() );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ( ( System.Byte )( 0 ) ), ( ( System.Byte )( 0 ) ), "", System.Data.DataRowVersion.Current, null ) );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@GUID_ID", System.Data.SqlDbType.UniqueIdentifier ) );
                cmd.Parameters[ "@GUID_ID" ].Value = uuidDebitArticleParent;
                System.Data.SqlClient.SqlDataReader rs = cmd.ExecuteReader();

                if( rs.HasRows )
                {
                    while( rs.Read() )
                    {
                        objChildDebitArticleList.Add( new CDebitArticle()
                            {
                                uuidID = (System.Guid)rs["GUID_ID"],
                                ArticleNum = System.Convert.ToString(rs["DEBITARTICLE_NUM"]),
                                Name = System.Convert.ToString(rs["DEBITARTICLE_NAME"]),
                                FinancislYear = ((rs["DebitArticleYear"] != System.DBNull.Value) ? System.Convert.ToInt32(rs["DebitArticleYear"]) : 0),
                                ParentID = ((rs["PARENT_GUID_ID"] != System.DBNull.Value) ? (System.Guid)rs["PARENT_GUID_ID"] : System.Guid.Empty),
                                ArticleDescription = ((rs["DEBITARTICLE_DESCRIPTION"] != System.DBNull.Value) ? System.Convert.ToString(rs["DEBITARTICLE_DESCRIPTION"]) : ""),
                                TransprtRest = ((rs["DEBITARTICLE_TRANSPORTREST"] != System.DBNull.Value) ? System.Convert.ToBoolean(rs["DEBITARTICLE_TRANSPORTREST"]) : false),
                                DontChange = ((rs["DEBITARTICLE_DONTCHANGE"] != System.DBNull.Value) ? System.Convert.ToBoolean(rs["DEBITARTICLE_DONTCHANGE"]) : false),
                                ArticleID = System.Convert.ToInt32(rs["DEBITARTICLE_ID"]),
                                AccountPlan = ((rs["ACCOUNTPLAN_GUID"] != System.DBNull.Value) ? new CAccountPlan()
                                {
                                    uuidID = (System.Guid)rs["ACCOUNTPLAN_GUID"],
                                    Name = System.Convert.ToString(rs["ACCOUNTPLAN_NAME"]),
                                    IsActive = System.Convert.ToBoolean(rs["ACCOUNTPLAN_ACTIVE"]),
                                    CodeIn1C = System.Convert.ToString(rs["ACCOUNTPLAN_1C_CODE"])
                                } : null)
                            }
                        );
                    }
                    rs.Close();
                }

                rs.Dispose();
            }
            catch( System.Exception f )
            {
                objChildDebitArticleList.Clear();

                DevExpress.XtraEditors.XtraMessageBox.Show( 
                "�� ������� �������� ������ �������� ������ ��������.\n\n����� ������: " + f.Message, "��������",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
            }

            return objChildDebitArticleList;
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
                cmd.CommandText = System.String.Format( "[{0}].[dbo].[sp_GetDebitArticle]", objProfile.GetOptionsDllDBName() );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ( ( System.Byte )( 0 ) ), ( ( System.Byte )( 0 ) ), "", System.Data.DataRowVersion.Current, null ) );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@GUID_ID", System.Data.SqlDbType.UniqueIdentifier ) );
                cmd.Parameters[ "@GUID_ID" ].Value = uuidID;
                System.Data.SqlClient.SqlDataReader rs = cmd.ExecuteReader();
                if( rs.HasRows )
                {
                    // ����� ������ ��������, � ��� ��� ���������� ���� ������
                    rs.Read();

                    this.uuidID = (System.Guid)rs["GUID_ID"];
                    this.ArticleNum = System.Convert.ToString(rs["DEBITARTICLE_NUM"]);
                    this.Name = System.Convert.ToString(rs["DEBITARTICLE_NAME"]);
                    this.FinancislYear = ((rs["DebitArticleYear"] != System.DBNull.Value) ? System.Convert.ToInt32(rs["DebitArticleYear"]) : 0);
                    this.ParentID = ((rs["PARENT_GUID_ID"] != System.DBNull.Value) ? (System.Guid)rs["PARENT_GUID_ID"] : System.Guid.Empty);
                    this.ArticleDescription = ((rs["DEBITARTICLE_DESCRIPTION"] != System.DBNull.Value) ? System.Convert.ToString(rs["DEBITARTICLE_DESCRIPTION"]) : "");
                    this.TransprtRest = ((rs["DEBITARTICLE_TRANSPORTREST"] != System.DBNull.Value) ? System.Convert.ToBoolean(rs["DEBITARTICLE_TRANSPORTREST"]) : false);
                    this.DontChange = ((rs["DEBITARTICLE_DONTCHANGE"] != System.DBNull.Value) ? System.Convert.ToBoolean(rs["DEBITARTICLE_DONTCHANGE"]) : false);
                    this.ArticleID = System.Convert.ToInt32(rs["DEBITARTICLE_ID"]);
                    this.AccountPlan = ((rs["ACCOUNTPLAN_GUID"] != System.DBNull.Value) ? new CAccountPlan()
                    {
                        uuidID = (System.Guid)rs["ACCOUNTPLAN_GUID"],
                        Name = System.Convert.ToString(rs["ACCOUNTPLAN_NAME"]),
                        IsActive = System.Convert.ToBoolean(rs["ACCOUNTPLAN_ACTIVE"]),
                        CodeIn1C = System.Convert.ToString(rs["ACCOUNTPLAN_1C_CODE"])
                    } : null);
                    this.BudgetDepList = CBudgetDep.GetBudgetDepListForDebitArticle(objProfile, this.uuidID);

                    bRet = true;
                }
                else
                {
                    DevExpress.XtraEditors.XtraMessageBox.Show( 
                    "�� ������� ������������������� ����� CDebitArticle.\n� �� �� ������� ����������.", "��������",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning );

                }
                cmd.Dispose();
                rs.Dispose();
            }
            catch( System.Exception e )
            {
                DevExpress.XtraEditors.XtraMessageBox.Show( 
                "�� ������� ������������������� ����� CDebitArticle.\n" + e.Message, "��������",
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

            System.Data.SqlClient.SqlConnection DBConnection = objProfile.GetDBSource();
            if( DBConnection == null )
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(  "����������� ���������� � ��.", "��������",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
                return bRet;
            }
            System.Data.SqlClient.SqlTransaction DBTransaction = DBConnection.BeginTransaction();
            try
            {
                // ���������� � �� ��������, ����������� ������� �� �������� ������ � ��
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand() { Connection = DBConnection, Transaction = DBTransaction, 
                    CommandType = System.Data.CommandType.StoredProcedure };

                // ���������� ������� �������� ������ � ��
                bRet = this.Remove( cmd, objProfile );
                if( bRet == true )
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
            catch( System.Exception f )
            {
                // ���������� ����������
                DBTransaction.Rollback();
                DevExpress.XtraEditors.XtraMessageBox.Show( 
                "�� ������� ������� ������ ��������:" + this.m_strArticleNum + " " + this.Name + 
                "\n\n����� ������: " + f.Message, "��������",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
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
        public System.Boolean Remove( System.Data.SqlClient.SqlCommand cmd, UniXP.Common.CProfile objProfile )
        {
            System.Boolean bRet = false;
            if( cmd == null ) { return bRet; }

            // ���������� ������������� �� ������ ���� ������
            if( this.m_uuidID == System.Guid.Empty )
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(  "������������ �������� ����������� �������������� �������", "��������",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
                return bRet;
            }

            try
            {
                // ���������� � �� ��������, ����������� ������� �� �������� ������
                cmd.Parameters.Clear();
                cmd.CommandText = System.String.Format( "[{0}].[dbo].[sp_DeleteDebitArticle]", objProfile.GetOptionsDllDBName() );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ( ( System.Byte )( 0 ) ), ( ( System.Byte )( 0 ) ), "", System.Data.DataRowVersion.Current, null ) );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@DEBITARTICLE_GUID_ID", System.Data.SqlDbType.UniqueIdentifier ) );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@ERROR_NUMBER", System.Data.SqlDbType.Int, 8, System.Data.ParameterDirection.Output, false, ( ( System.Byte )( 0 ) ), ( ( System.Byte )( 0 ) ), "", System.Data.DataRowVersion.Current, null ) );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@ERROR_MESSAGE", System.Data.SqlDbType.NVarChar, 4000 ) );
                cmd.Parameters[ "@ERROR_MESSAGE" ].Direction = System.Data.ParameterDirection.Output;
                cmd.Parameters[ "@DEBITARTICLE_GUID_ID" ].Value = this.m_uuidID;
                cmd.ExecuteNonQuery();
                System.Int32 iRet = ( System.Int32 )cmd.Parameters[ "@RETURN_VALUE" ].Value;
                if( iRet != 0 )
                {
                    DevExpress.XtraEditors.XtraMessageBox.Show("������ �������� ������ ��������.\n\n����� ������: " +
                            (System.String)cmd.Parameters["@ERROR_MESSAGE"].Value, "��������",
                        System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                }
                bRet = ( iRet == 0 );
            }
            catch( System.Exception f )
            {
                DevExpress.XtraEditors.XtraMessageBox.Show( 
                "�� ������� ������� ������ ��������.\n\n����� ������: " + 
                ( System.String )cmd.Parameters[ "@ERROR_MESSAGE" ].Value + "\n" + f.Message, "��������",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
            }
            return bRet;
        }
        #endregion

        #region Add
        /// <summary>
        /// ��������� ������������ ���������� ������������ ��������
        /// </summary>
        /// <returns>true - ��� � �������; false - �������� �������� </returns>
        public System.Boolean IsValidateProperties( ref System.String strErr )
        {
            System.Boolean bRes = false;
            try
            {
                // ������������ �� ������ ���� ������
                if( this.Name == "" )
                {
                    strErr += ("\n������������ �������� ������������ ������ ��������.");
                    return bRes;
                }
                if( this.ArticleNum == "" )
                {
                    strErr += ("\n������������ �������� ������ ������ ��������.");
                    return bRes;
                }
                bRes = true;
            }
            catch( System.Exception f )
            {
                strErr += String.Format("\n������ �������� ������� ������ ��������.\n����� ������: {0}", f.Message);
            }
            return bRes;
        }
        /// <summary>
        /// �������� ������ � ��
        /// </summary>
        /// <param name="objProfile">�������</param>
        /// <returns>true - ������� ����������; false - ������</returns>
        public override System.Boolean Add( UniXP.Common.CProfile objProfile )
        {
            System.Boolean bRet = false;
            System.String strErr = "";

            System.Data.SqlClient.SqlConnection DBConnection = objProfile.GetDBSource();
            if( DBConnection == null )
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(  "����������� ���������� � ��.", "��������",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
                return bRet;
            }
            System.Data.SqlClient.SqlTransaction DBTransaction = DBConnection.BeginTransaction();
            try
            {
                // ���������� � �� ��������, ����������� ������� �� �������� ������ � ��
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand() { Connection = DBConnection, Transaction = DBTransaction, 
                    CommandType = System.Data.CommandType.StoredProcedure };

                // ���������� ������� ���������� ������ � ��
                bRet = this.Add(cmd, objProfile, ref strErr);
                if( bRet == true )
                {
                    // ������������ ����������
                    DBTransaction.Commit();
                }
                else
                {
                    // ���������� ����������
                    DBTransaction.Rollback();

                    DevExpress.XtraEditors.XtraMessageBox.Show(strErr, "��������",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                }
                cmd.Dispose();
            }
            catch( System.Exception f )
            {
                // ���������� ����������
                DBTransaction.Rollback();

                DevExpress.XtraEditors.XtraMessageBox.Show(
                String.Format("�� ������� ������� ������ ��������:{0} {1}\n\n����� ������: {2}", this.m_strArticleNum, this.Name, f.Message), "��������",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
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
        public System.Boolean Add(UniXP.Common.CProfile objProfile, ref System.String strErr)
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
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand()
                {
                    Connection = DBConnection,
                    Transaction = DBTransaction,
                    CommandType = System.Data.CommandType.StoredProcedure
                };

                // ���������� ������� ���������� ������ � ��
                bRet = this.Add(cmd, objProfile, ref strErr);
                if (bRet == true)
                {
                    // ������������ ����������
                    DBTransaction.Commit();
                }
                else
                {
                    // ���������� ����������
                    DBTransaction.Rollback();

                    DevExpress.XtraEditors.XtraMessageBox.Show(strErr, "��������",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                }
                cmd.Dispose();
            }
            catch (System.Exception f)
            {
                // ���������� ����������
                DBTransaction.Rollback();

                DevExpress.XtraEditors.XtraMessageBox.Show(
                String.Format("�� ������� ������� ������ ��������:{0} {1}\n\n����� ������: {2}", this.m_strArticleNum, this.Name, f.Message), "��������",
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
        public System.Boolean Add( System.Data.SqlClient.SqlCommand cmd, UniXP.Common.CProfile objProfile, ref System.String strErr )
        {
            System.Boolean bRet = false;
            if( cmd == null )
            {
                strErr += "\n����������� ���������� � ��.";
                return bRet;
            }

            if (IsValidateProperties(ref strErr) == false) { return bRet; }

            try
            {
                cmd.Parameters.Clear();
                cmd.CommandText = System.String.Format( "[{0}].[dbo].[sp_AddDebitArticle]", objProfile.GetOptionsDllDBName() );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ( ( System.Byte )( 0 ) ), ( ( System.Byte )( 0 ) ), "", System.Data.DataRowVersion.Current, null ) );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@GUID_ID", System.Data.SqlDbType.UniqueIdentifier, 4, System.Data.ParameterDirection.Output, false, ( ( System.Byte )( 0 ) ), ( ( System.Byte )( 0 ) ), "", System.Data.DataRowVersion.Current, null ) );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@DEBITARTICLE_GUID_ID", System.Data.SqlDbType.UniqueIdentifier, 4 ) );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@DEBITARTICLE_NAME", System.Data.DbType.String ) );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@DEBITARTICLE_NUM", System.Data.DbType.String ) );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@DEBITARTICLE_ID", System.Data.SqlDbType.Int ) );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@DEBITARTICLE_TRANSPORTREST", System.Data.DbType.Boolean ) );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@DEBITARTICLE_DONTCHANGE", System.Data.DbType.Boolean ) );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@ERROR_NUMBER", System.Data.SqlDbType.Int, 8, System.Data.ParameterDirection.Output, false, ( ( System.Byte )( 0 ) ), ( ( System.Byte )( 0 ) ), "", System.Data.DataRowVersion.Current, null ) );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@ERROR_MESSAGE", System.Data.SqlDbType.NVarChar, 4000 ) );
                cmd.Parameters[ "@ERROR_MESSAGE" ].Direction = System.Data.ParameterDirection.Output;
                cmd.Parameters[ "@DEBITARTICLE_GUID_ID" ].Value = this.uuidID;
                cmd.Parameters[ "@DEBITARTICLE_NAME" ].Value = this.Name;
                cmd.Parameters[ "@DEBITARTICLE_NUM" ].Value = this.m_strArticleNum;
                cmd.Parameters[ "@DEBITARTICLE_TRANSPORTREST" ].Value = this.m_bTransprtRest;
                cmd.Parameters[ "@DEBITARTICLE_DONTCHANGE" ].Value = this.m_bDontChange;
                cmd.Parameters[ "@DEBITARTICLE_ID" ].Value = this.m_iArticleID;
                if( this.ParentID.CompareTo( System.Guid.Empty ) != 0 )
                {
                    cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@PARENT_GUID_ID", System.Data.SqlDbType.UniqueIdentifier ) );
                    cmd.Parameters[ "@PARENT_GUID_ID" ].Value = this.m_uuidParentID;
                }
                if( this.ArticleDescription.Length > 0 )
                {
                    cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@DEBITARTICLE_DESCRIPTION", System.Data.DbType.String ) );
                    cmd.Parameters[ "@DEBITARTICLE_DESCRIPTION" ].Value = this.m_strArticleDescription;
                }
                if (this.AccountPlan != null)
                {
                    cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ACCOUNTPLAN_GUID", System.Data.SqlDbType.UniqueIdentifier));
                    cmd.Parameters["@ACCOUNTPLAN_GUID"].Value = this.AccountPlan.uuidID;
                }
                cmd.ExecuteNonQuery();
                System.Int32 iRet = ( System.Int32 )cmd.Parameters[ "@RETURN_VALUE" ].Value;
                if( iRet == 0 )
                {
                    // ���������� ������ ��������� ������������� ��� ������ ������ ��������
                    bRet = this.AssignDebitArticleBudgetDep( cmd, objProfile, ref strErr );
                    if (bRet == true)
                    {
                        bRet = this.AssignDebitArticleToFinancialPeriod(cmd, objProfile, ref strErr);
                    }
                }
                else
                {
                    strErr += String.Format("\n�� ������� ���������������� ������ ��������.\n{0} {1}\n����� ������: {2}", this.ArticleNum, this.Name, (System.String)cmd.Parameters["@ERROR_MESSAGE"].Value);
                }
            }
            catch( System.Exception f )
            {
                strErr += String.Format("\n�� ������� ������� ������ ��������.\n{0} {1}\n����� ������: {2}", this.ArticleNum, this.Name, f.Message);
            }
            return bRet;
        }
        /// <summary>
        /// �������� ������ �������� � ����������� ����
        /// </summary>
        /// <param name="cmd">SQL-�������</param>
        /// <param name="objProfile">�������</param>
        /// <returns>true - ������� ����������; false - ������</returns>
        private System.Boolean AssignDebitArticleToFinancialPeriod(System.Data.SqlClient.SqlCommand cmd,
            UniXP.Common.CProfile objProfile, ref System.String strErr)
        {
            System.Boolean bRet = false;
            if (cmd == null) { return bRet; }
            try
            {
                cmd.Parameters.Clear();
                cmd.CommandText = System.String.Format("[{0}].[dbo].[sp_AssignDebitArticleToFinancialPeriod]", objProfile.GetOptionsDllDBName());
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ((System.Byte)(0)), ((System.Byte)(0)), "", System.Data.DataRowVersion.Current, null));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@DEBITARTICLE_GUID_ID", System.Data.SqlDbType.UniqueIdentifier));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@BEGIN_DATE", System.Data.SqlDbType.DateTime));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@END_DATE", System.Data.SqlDbType.DateTime));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ERROR_NUMBER", System.Data.SqlDbType.Int, 8, System.Data.ParameterDirection.Output, false, ((System.Byte)(0)), ((System.Byte)(0)), "", System.Data.DataRowVersion.Current, null));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ERROR_MESSAGE", System.Data.SqlDbType.NVarChar, 4000));
                cmd.Parameters["@ERROR_MESSAGE"].Direction = System.Data.ParameterDirection.Output;
                cmd.Parameters["@DEBITARTICLE_GUID_ID"].Value = this.uuidID;
                cmd.Parameters["@BEGIN_DATE"].Value = new System.DateTime(this.m_iYear, 1, 1);
                cmd.Parameters["@END_DATE"].Value = new System.DateTime(this.m_iYear, 12, 31);
                cmd.ExecuteNonQuery();
                System.Int32 iRet = (System.Int32)cmd.Parameters["@RETURN_VALUE"].Value;
                if (iRet == 0)
                {
                    bRet = true;
                }
                else
                {
                    strErr += String.Format("\n������ �������� ������ �������� � ����������� ����.\n{0} {1}\n������ ��������� ���������.\n����� ������: {2}", 
                        this.ArticleNum, this.Name, System.Convert.ToString(cmd.Parameters["@ERROR_MESSAGE"].Value));
                }
            }
            catch (System.Exception f)
            {
                strErr += String.Format("\n������ �������� ������ �������� � ����������� ����.\n{0}", f.Message);
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
        public override System.Boolean Update(UniXP.Common.CProfile objProfile)
        {
            return false;
        }
        /// <summary>
        /// ��������� ��������� � ��
        /// </summary>
        /// <param name="objProfile">�������</param>
        /// <param name="bUpdateWarningParam">������� ����, ��� ����� �������� ������ �����</param>
        /// <returns>true - ������� ����������; false - ������</returns>
        public System.Boolean Update( UniXP.Common.CProfile objProfile, System.Boolean bUpdateWarningParam, ref System.String strErr )
        {
            System.Boolean bRet = false;

            System.Data.SqlClient.SqlConnection DBConnection = objProfile.GetDBSource();
            if( DBConnection == null )
            {
                strErr += ("\n����������� ���������� � ��.");
                return bRet;
            }
            System.Data.SqlClient.SqlTransaction DBTransaction = DBConnection.BeginTransaction();
            try
            {
                // ���������� � �� ��������, ����������� ������� �� �������� ������ � ��
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand() { Connection = DBConnection, Transaction = DBTransaction, 
                    CommandType = System.Data.CommandType.StoredProcedure };

                // ���������� ������� ���������� � ��
                bRet = this.Update( cmd, objProfile, bUpdateWarningParam, ref strErr );
                if( bRet == true )
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
            catch( System.Exception f )
            {
                // ���������� ����������
                DBTransaction.Rollback();
                strErr += String.Format("\n�� ������� �������� �������� ������ ��������:{0} {1}\n����� ������: {2}", this.m_strArticleNum, this.Name, f.Message);
            }
			finally // ������� ���������� �������
            {
                DBConnection.Close();
            }

            return bRet;
        }
        /// <summary>
        /// ��������� ��������� � ������ ������ ��������
        /// </summary>
        /// <param name="objProfile">�������</param>
        /// <param name="objDebitArticleList">������ ������ ��������</param>
        /// <param name="bUpdateWarningParam">������� ����, ��� ����� �������� ������ �����</param>
        /// <returns>true - ������� ����������; false - ������</returns>
        public static System.Boolean UpdateList( UniXP.Common.CProfile objProfile, List<CDebitArticle> objDebitArticleList,
            System.Boolean bUpdateWarningParam, ref System.String strErr )
        {
            System.Boolean bRet = false;
            if( ( objDebitArticleList == null ) || ( objDebitArticleList.Count == 0 ) )
            {
                strErr += ("\n������ ������ �������� ����.");
                return bRet;
            }
            System.Data.SqlClient.SqlConnection DBConnection = objProfile.GetDBSource();
            if( DBConnection == null )
            {
                strErr += ("\n����������� ���������� � ��.");
                return bRet;
            }
            System.Data.SqlClient.SqlTransaction DBTransaction = DBConnection.BeginTransaction();
            try
            {
                // ���������� � �� ��������, ����������� ������� �� �������� ������ � ��
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand() { Connection = DBConnection, Transaction = DBTransaction, 
                    CommandType = System.Data.CommandType.StoredProcedure };
                foreach( CDebitArticle objDebitArticle in objDebitArticleList )
                {
                    // ���������� ������� ���������� � ��
                    bRet = objDebitArticle.Update( cmd, objProfile, bUpdateWarningParam, ref strErr );
                    if( bRet == false ) { break; }
                }
                if( bRet == true )
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
            catch( System.Exception f )
            {
                // ���������� ����������
                DBTransaction.Rollback();
                strErr += ("\n�� ������� ��������� ��������� � ������ ������ ��������.\n����� ������: " + f.Message);
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
        /// <param name="bUpdateWarningParam">������� ����, ��� ����� �������� ������ �����</param>
        /// <returns>true - ������� ����������; false - ������</returns>
        public System.Boolean Update( System.Data.SqlClient.SqlCommand cmd,
            UniXP.Common.CProfile objProfile, System.Boolean bUpdateWarningParam, ref System.String strErr )
        {
            System.Boolean bRet = false;
            if( cmd == null ) { return bRet; }
            try
            {
                // �������� �������
                if (this.IsValidateProperties(ref strErr) == false) { return bRet; }

                cmd.Parameters.Clear();
                cmd.CommandText = System.String.Format( "[{0}].[dbo].[sp_EditDebitArticle]", objProfile.GetOptionsDllDBName() );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ( ( System.Byte )( 0 ) ), ( ( System.Byte )( 0 ) ), "", System.Data.DataRowVersion.Current, null ) );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@GUID_ID", System.Data.SqlDbType.UniqueIdentifier ) );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@DEBITARTICLE_NAME", System.Data.DbType.String ) );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@DEBITARTICLE_NUM", System.Data.DbType.String ) );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@DEBITARTICLE_TRANSPORTREST", System.Data.DbType.Boolean ) );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@DEBITARTICLE_DONTCHANGE", System.Data.DbType.Boolean ) );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@DEBITARTICLE_ID", System.Data.SqlDbType.Int ) );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@ERROR_NUMBER", System.Data.SqlDbType.Int, 8, System.Data.ParameterDirection.Output, false, ( ( System.Byte )( 0 ) ), ( ( System.Byte )( 0 ) ), "", System.Data.DataRowVersion.Current, null ) );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@ERROR_MESSAGE", System.Data.SqlDbType.NVarChar, 4000 ) );
                cmd.Parameters[ "@ERROR_MESSAGE" ].Direction = System.Data.ParameterDirection.Output;
                cmd.Parameters[ "@GUID_ID" ].Value = this.m_uuidID;
                cmd.Parameters[ "@DEBITARTICLE_NAME" ].Value = this.m_strName;
                cmd.Parameters[ "@DEBITARTICLE_NUM" ].Value = this.m_strArticleNum;
                cmd.Parameters[ "@DEBITARTICLE_TRANSPORTREST" ].Value = this.m_bTransprtRest;
                cmd.Parameters[ "@DEBITARTICLE_DONTCHANGE" ].Value = this.m_bDontChange;
                cmd.Parameters[ "@DEBITARTICLE_ID" ].Value = this.m_iArticleID;
                if( this.m_uuidParentID.CompareTo( System.Guid.Empty ) != 0 )
                {
                    cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@PARENT_GUID_ID", System.Data.SqlDbType.UniqueIdentifier ) );
                    cmd.Parameters[ "@PARENT_GUID_ID" ].Value = this.m_uuidParentID;
                }
                if( this.m_strArticleDescription.Length > 0 )
                {
                    cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@DEBITARTICLE_DESCRIPTION", System.Data.DbType.String ) );
                    cmd.Parameters[ "@DEBITARTICLE_DESCRIPTION" ].Value = this.m_strArticleDescription;
                }
                if (this.AccountPlan != null)
                {
                    cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ACCOUNTPLAN_GUID", System.Data.SqlDbType.UniqueIdentifier));
                    cmd.Parameters["@ACCOUNTPLAN_GUID"].Value = this.AccountPlan.uuidID;
                }
                cmd.ExecuteNonQuery();
                System.Int32 iRet = ( System.Int32 )cmd.Parameters[ "@RETURN_VALUE" ].Value;
                if( iRet == 0 )
                {
                    if( bUpdateWarningParam == true )
                    {
                        // ��������� ��������� ������ ��������� ������������� ��� ������ ������ ��������
                        bRet = this.AssignDebitArticleBudgetDep(cmd, objProfile, ref strErr);
                        if (bRet == true)
                        {
                            bRet = this.AssignDebitArticleToFinancialPeriod(cmd, objProfile, ref strErr);
                        }
                    }
                    else
                    {
                        bRet = true;
                    }
                }
                else
                {
                    strErr += String.Format("\n������ ��������� ������ ��������: \n{0} {1}\n����� ������: {2}", this.m_strArticleNum, this.Name, 
                        (System.String)cmd.Parameters["@ERROR_MESSAGE"].Value);
                }
            }
            catch( System.Exception f )
            {
                strErr += String.Format("\n�� ������� �������� �������� ������ ��������:{0} {1}\n����� ������: {2}", this.m_strArticleNum, this.Name, f.Message);
            }
            return bRet;
        }
        #endregion

        #region ����� � ���������� ���������������
        /// <summary>
        /// ������� ����� ������ �������� � ���������� ���������������
        /// </summary>
        /// <param name="objProfile">�������</param>
        /// <param name="cmd">SQL-�������</param>
        /// <returns>true - ������� ����������; false - ������</returns>
        private System.Boolean DeleteDebitArticleBudgetDep( System.Data.SqlClient.SqlCommand cmd,
            UniXP.Common.CProfile objProfile, System.String strErr )
        {
            System.Boolean bRet = false;
            if( cmd == null ) { return bRet; }
            try
            {
                cmd.Parameters.Clear();
                cmd.CommandText = System.String.Format( "[{0}].[dbo].[sp_DeleteDebitArticleBudgetDep]", objProfile.GetOptionsDllDBName() );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ( ( System.Byte )( 0 ) ), ( ( System.Byte )( 0 ) ), "", System.Data.DataRowVersion.Current, null ) );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@DEBITARTICLE_GUID_ID", System.Data.SqlDbType.UniqueIdentifier ) );
                cmd.Parameters[ "@DEBITARTICLE_GUID_ID" ].Value = this.uuidID;
                cmd.ExecuteNonQuery();
                System.Int32 iRet = ( System.Int32 )cmd.Parameters[ "@RETURN_VALUE" ].Value;
                if( iRet == 0 )
                {
                    bRet = true;
                }
                else
                {
                    strErr += String.Format("\n������ �������� ������ ��������� ������������� � ������ ��������\n{0} {1}\n������ ��������� ���������.", 
                        this.ArticleNum, this.Name);
                }
            }
            catch( System.Exception e )
            {
                strErr += String.Format("\n������ �������� ������ ��������� ������������� � ������ ��������.\n����� ������: {0}", e.Message);
            }
            return bRet;
        }
        /// <summary>
        /// ��������� ������ �������� � ���������� ���������������
        /// </summary>
        /// <param name="objProfile">�������</param>
        /// <param name="cmd">SQL-�������</param>
        /// <returns>true - ������� ����������; false - ������</returns>
        private System.Boolean AssignDebitArticleBudgetDep( System.Data.SqlClient.SqlCommand cmd,
            UniXP.Common.CProfile objProfile, ref System.String strErr )
        {
            System.Boolean bRet = false;
            if( cmd == null ) { return bRet; }
            try
            {
                // ����������� ������ ��������� ������������� 
                List<CBudgetDep> objBudgetDepList = CBudgetDep.GetBudgetDepsList( objProfile, false );
                if( objBudgetDepList.Count == 0 )
                {
                    strErr += ("\n� �� �� ������ ������ ��������� �������������.");
                    return bRet;
                }

                cmd.Parameters.Clear();
                cmd.CommandText = System.String.Format( "[{0}].[dbo].[sp_AssignDebitArticleBudgetDep]", objProfile.GetOptionsDllDBName() );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ( ( System.Byte )( 0 ) ), ( ( System.Byte )( 0 ) ), "", System.Data.DataRowVersion.Current, null ) );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@DEBITARTICLE_GUID_ID", System.Data.SqlDbType.UniqueIdentifier ) );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@BUDGETDEP_GUID_ID", System.Data.SqlDbType.UniqueIdentifier ) );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@bAssign", System.Data.SqlDbType.Bit, 1 ) );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@BUDGETEXPENSETYPE_GUID", System.Data.SqlDbType.UniqueIdentifier));
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@BUDGETPROJECT_GUID", System.Data.SqlDbType.UniqueIdentifier));
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@ERROR_NUMBER", System.Data.SqlDbType.Int, 8));
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@ERROR_MESSAGE", System.Data.SqlDbType.NVarChar, 4000 ) );
                cmd.Parameters[ "@ERROR_NUMBER" ].Direction = System.Data.ParameterDirection.Output;
                cmd.Parameters[ "@ERROR_MESSAGE" ].Direction = System.Data.ParameterDirection.Output;
                cmd.Parameters[ "@DEBITARTICLE_GUID_ID" ].Value = this.m_uuidID;


                // ��������� �� ������ ��������� �������������, 
                // ���� � ������ ������ �������� ������������� ���, �� ������ ����� ����� ���������
                System.Int32 iRet = -1;
                System.Guid uuidBudgetExpenseType = System.Guid.Empty;
                System.Guid uuidBudgetProject = System.Guid.Empty;
                CBudgetDep objItem = null;
                System.Boolean bAssign = false;

                foreach( CBudgetDep objBudgetDep in objBudgetDepList )
                {
                    objItem = this.BudgetDepList.SingleOrDefault<CBudgetDep>(x => x.uuidID.Equals(objBudgetDep.uuidID));

                    if( objItem == null) 
                    {
                        bAssign = false;
                        uuidBudgetExpenseType = System.Guid.Empty;
                        uuidBudgetProject = System.Guid.Empty;
                    }
                    else
                    {
                        bAssign = true;
                        uuidBudgetExpenseType = ( ( objItem.BudgetExpenseType == null ) ? System.Guid.Empty : objItem.BudgetExpenseType.uuidID );
                        uuidBudgetProject = ((objItem.BudgetProject == null) ? System.Guid.Empty : objItem.BudgetProject.uuidID);
                    }

                    cmd.Parameters[ "@bAssign" ].Value = ( bAssign == true ) ? 1 : 0;
                    cmd.Parameters[ "@BUDGETDEP_GUID_ID" ].Value = objBudgetDep.uuidID;
                    if (uuidBudgetExpenseType.CompareTo(System.Guid.Empty) == 0)
                    {
                        cmd.Parameters["@BUDGETEXPENSETYPE_GUID"].IsNullable = true;
                        cmd.Parameters["@BUDGETEXPENSETYPE_GUID"].Value = null;
                    }
                    else
                    {
                        cmd.Parameters["@BUDGETEXPENSETYPE_GUID"].IsNullable = false;
                        cmd.Parameters["@BUDGETEXPENSETYPE_GUID"].Value = uuidBudgetExpenseType;
                    }

                    if (uuidBudgetProject.CompareTo(System.Guid.Empty) == 0)
                    {
                        cmd.Parameters["@BUDGETPROJECT_GUID"].IsNullable = true;
                        cmd.Parameters["@BUDGETPROJECT_GUID"].Value = null;
                    }
                    else
                    {
                        cmd.Parameters["@BUDGETPROJECT_GUID"].IsNullable = false;
                        cmd.Parameters["@BUDGETPROJECT_GUID"].Value = uuidBudgetProject;
                    }
                    cmd.ExecuteNonQuery();
                    iRet = ( System.Int32 )cmd.Parameters[ "@RETURN_VALUE" ].Value;
                    if( iRet != 0 )
                    {
                        // ����� �� �����
                        break;
                    }
                }

                if (iRet != 0)
                {
                    strErr += String.Format("\n������ ���������� ���������� ������������� � ������� ������ ��������.\n����� ������: {0}", cmd.Parameters["@ERROR_MESSAGE"].Value as System.String);
                }

                bRet = (iRet == 0);
            }
            catch( System.Exception f )
            {
                strErr += String.Format("\n������ ����� ������ �������� � ���������� ��������������� � ���������.\n����� ������: {0}", f.Message);
            }
            return bRet;
        }
        /// <summary>
        /// ��������� ��������� �������� � ���������� ���������������
        /// </summary>
        /// <param name="objProfile">�������</param>
        /// <param name="cmd">SQL-�������</param>
        /// <returns>true - ������� ����������; false - ������</returns>
        private System.Boolean AssignDebitArticleChildBudgetDep( System.Data.SqlClient.SqlCommand cmd,
            UniXP.Common.CProfile objProfile, ref System.String strErr )
        {
            System.Boolean bRet = false;
            if( cmd == null ) { return bRet; }
            try
            {
                cmd.Parameters.Clear();
                cmd.CommandText = System.String.Format( "[{0}].[dbo].[sp_AssignDebitArticleBudgetDep]", objProfile.GetOptionsDllDBName() );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ( ( System.Byte )( 0 ) ), ( ( System.Byte )( 0 ) ), "", System.Data.DataRowVersion.Current, null ) );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@DEBITARTICLE_GUID_ID", System.Data.SqlDbType.UniqueIdentifier ) );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@BUDGETDEP_GUID_ID", System.Data.SqlDbType.UniqueIdentifier ) );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@bAssign", System.Data.SqlDbType.Bit, 1 ) );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@BUDGETEXPENSETYPE_GUID", System.Data.SqlDbType.UniqueIdentifier));
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@BUDGETPROJECT_GUID", System.Data.SqlDbType.UniqueIdentifier));
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@ERROR_NUMBER", System.Data.SqlDbType.Int, 8, System.Data.ParameterDirection.Output, false, ((System.Byte)(0)), ((System.Byte)(0)), "", System.Data.DataRowVersion.Current, null));
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@ERROR_MESSAGE", System.Data.SqlDbType.NVarChar, 4000 ) );
                cmd.Parameters[ "@ERROR_MESSAGE" ].Direction = System.Data.ParameterDirection.Output;
                cmd.Parameters[ "@DEBITARTICLE_GUID_ID" ].Value = this.m_uuidID;
                cmd.Parameters[ "@bAssign" ].Value = 1;
                cmd.Parameters[ "@BUDGETDEP_GUID_ID" ].Value = this.BudgetDepList[ 0 ].uuidID;
                
                if( (this.BudgetDepList[ 0 ].BudgetExpenseType == null ) ||
                    ( this.BudgetDepList[ 0 ].BudgetExpenseType.uuidID.CompareTo(System.Guid.Empty) == 0))
                {
                    cmd.Parameters["@BUDGETEXPENSETYPE_GUID"].IsNullable = true;
                    cmd.Parameters["@BUDGETEXPENSETYPE_GUID"].Value = null;
                }
                else
                {
                    cmd.Parameters["@BUDGETEXPENSETYPE_GUID"].IsNullable = false;
                    cmd.Parameters["@BUDGETEXPENSETYPE_GUID"].Value = this.BudgetDepList[0].BudgetExpenseType.uuidID;
                }

                if ((this.BudgetDepList[0].BudgetProject == null) ||
                    (this.BudgetDepList[0].BudgetProject.uuidID.CompareTo(System.Guid.Empty) == 0))
                {
                    cmd.Parameters["@BUDGETPROJECT_GUID"].IsNullable = true;
                    cmd.Parameters["@BUDGETPROJECT_GUID"].Value = null;
                }
                else
                {
                    cmd.Parameters["@BUDGETPROJECT_GUID"].IsNullable = false;
                    cmd.Parameters["@BUDGETPROJECT_GUID"].Value = this.BudgetDepList[0].BudgetProject.uuidID;
                }

                cmd.ExecuteNonQuery();
                System.Int32 iRet = ( System.Int32 )cmd.Parameters[ "@RETURN_VALUE" ].Value;

                if( iRet != 0 )
                {
                    strErr += String.Format("\n������ ���������� ���������� ������������� ��������� ��������.\n����� ������: {0}", 
                        (System.String)cmd.Parameters["@ERROR_MESSAGE"].Value);
                }

                bRet = ( iRet == 0 );
            }
            catch( System.Exception f )
            {
                strErr += String.Format("\n������ ����� ��������� �������� � ��������� ��������������.\n����� ������: {0}", f.Message);
            }
            return bRet;
        }
        #endregion

        #region ������ �������������, ����������� ������
        /// <summary>
        /// ���������� ������ �����
        /// </summary>
        /// <returns>������ �� ������� �����, ��������������� ������</returns>
        public System.String GetBudgetDepList()
        {
            System.String strRet = "";
            if( ( this.BudgetDepList == null ) || ( this.BudgetDepList.Count == 0 ) ) { return strRet; }
            try
            {
                System.Int32 iComaCount = this.BudgetDepList.Count - 1;
                foreach( CBudgetDep objBudgetDep in this.BudgetDepList )
                {
                    strRet += objBudgetDep.Name;
                    if( iComaCount > 0 )
                    {
                        strRet += ", ";
                        iComaCount--;
                    }
                }
            }
            catch( System.Exception f )
            {
                strRet = "";
                DevExpress.XtraEditors.XtraMessageBox.Show( 
                "�� ������� �������� ������ ����� ��� ������ ��������.\n\n����� ������: " + f.Message, "��������",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
            }
			finally // ������� ���������� �������
            {
            }

            return strRet;
        }
        #endregion

        public override string ToString()
        {
            return ArticleFullName;
        }


    }

}
