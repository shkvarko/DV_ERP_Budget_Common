using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

namespace ERP_Budget.Common
{
    /// <summary>
    /// ����� "������� ��������� ���������� ����� ����������"
    /// </summary>
    public class CBudgetDocTypeTransformTypeItem
    {
        public CBudgetDocState CurrentBudgetDocState { get; set; }
        public CBudgetDocType NewBudgetDocType { get; set; }

        #region �������������� ������ � �������
        /// <summary>
        /// ����������� ���������� ������ � �������
        /// </summary>
        /// <param name="objTransformTypeItemList">�������� ��������� ����� � �������� ���������</param>
        /// <param name="uuidBudgetDocTypeID">�� ���� ���������� ��������� (��������)</param>
        /// <param name="strErr">��������� �� ������</param>
        /// <returns>�������</returns>
        public static System.Data.DataTable ConvertListToTable(List<CBudgetDocTypeTransformTypeItem> objTransformTypeItemList, 
            System.Guid uuidBudgetDocTypeID, ref System.String strErr)
        {
            if (objTransformTypeItemList == null) { return null; }
            System.Data.DataTable addedItems = new System.Data.DataTable();

            try
            {
                addedItems.Columns.Add(new System.Data.DataColumn("SRC_BUDGETDOCSTATE_GUID", typeof(System.Data.SqlTypes.SqlGuid)));
                addedItems.Columns.Add(new System.Data.DataColumn("SRC_BUDGETDOCTYPE_GUID", typeof(System.Data.SqlTypes.SqlGuid)));
                addedItems.Columns.Add(new System.Data.DataColumn("DST_BUDGETDOCTYPE_GUID", typeof(System.Data.SqlTypes.SqlGuid)));

                System.Data.DataRow newRow = null;
                foreach (CBudgetDocTypeTransformTypeItem objItem in objTransformTypeItemList)
                {
                    newRow = addedItems.NewRow();
                    newRow["SRC_BUDGETDOCSTATE_GUID"] = objItem.CurrentBudgetDocState.uuidID;
                    newRow["SRC_BUDGETDOCTYPE_GUID"] = uuidBudgetDocTypeID;
                    newRow["DST_BUDGETDOCTYPE_GUID"] = objItem.NewBudgetDocType.uuidID;
                    addedItems.Rows.Add(newRow);
                }

                addedItems.AcceptChanges();
            }
            catch (System.Exception f)
            {
                strErr = f.Message;
            }
            finally
            {
            }

            return addedItems;
        }
        #endregion

    }
    /// <summary>
    /// ����� "��� ���������� ���������"
    /// </summary>
    public class CBudgetDocType : IBaseListItem
    {
        #region ����������, ��������, ��������� 
        /// <summary>
        /// ��������
        /// </summary>
        private System.Drawing.Image m_ImageSmall;
        [DisplayName("�������� (16�16)")]
        [Description("��������, ��������������� ���� ���������� ���������")]
        [Category("������������ ��������")]
        public System.Drawing.Image ImageSmall
        {
            get { return m_ImageSmall; }
            set { m_ImageSmall = value; }
        }
        private System.Drawing.Image m_ImageBig;
        [DisplayName("�������� (24�24)")]
        [Description("��������, ��������������� ���� ���������� ���������")]
        [Category("������������ ��������")]
        public System.Drawing.Image ImageBig
        {
            get { return m_ImageBig; }
            set { m_ImageBig = value; }
        }

        private CBudgetDocTypeDraw m_objBudgetDocTypeDraw;
        [DisplayName( "�������� " )]
        [Description( "��������, ��������������� ���� ���������� ���������" )]
        [Category( "�������������� ��������" )]
        [ReadOnly( true )]
        public CBudgetDocTypeDraw BudgetDocTypeDraw
        {
            get { return m_objBudgetDocTypeDraw; }
            set { m_objBudgetDocTypeDraw = value; }
        }
        /// <summary>
        /// ��� ������ ���������
        /// </summary>
        private System.String m_strControlClassName;
        /// <summary>
        /// ��� ������ ���������
        /// </summary>
        [DisplayName("��������")]
        [Description("��� ������, ������� ������������ ��� ������ � ����������� ������� ����")]
        [ReadOnly(false)]
        [Category("������������ ��������")]
        public System.String ControlClassName
        {
            get { return m_strControlClassName; }
            set { m_strControlClassName = value; }
        }
        /// <summary>
        /// ���������
        /// </summary>
        private System.Int32 m_iPriority;
        /// <summary>
        /// ���������
        /// </summary>
        [DisplayName("���������")]
        [Description("��������� ������� ���� ��� ������ ��� �� ������ ��������� ��������")]
        [ReadOnly(false)]
        [Category("������������ ��������")]
        public System.Int32 Priority
        {
            get { return m_iPriority; }
            set { m_iPriority = value; }
        }
        /// <summary>
        /// ������� ����, ��� ��� ������� ���� ���������� ���������� ��������� ����� � ��������� �� ����������
        /// </summary>
        private System.Boolean m_bNeedDivision;
        /// <summary>
        /// ������� ����, ��� ��� ������� ���� ���������� ���������� ��������� ����� � ��������� �� ����������
        /// </summary>
        public System.Boolean NeedDivision
        {
            get { return m_bNeedDivision; }
            set { m_bNeedDivision = value; }
        }
        /// <summary>
        /// ������� "�������"
        /// </summary>
        public System.Boolean IsActive {get; set;}
        /// <summary>
        /// ������� "����� ������������� � ������ ��� ���������� ���������"
        /// </summary>
        public System.Boolean IsCanTransformInOtherType {get; set;}
        /// <summary>
        /// ������� "����� �������� ��� ���������� ��������� ��� ���������� ������ ���������� ���������"
        /// </summary>
        public System.Boolean IsCanChangeCategoryInBudgetDoc {get; set;}
        /// <summary>
        /// ��� ���������� ���������
        /// </summary>
        public CBudgetDocCategory BudgetDocCategory {get; set;}
        /// <summary>
        /// ��� ���������� ��������� (������������)
        /// </summary>
        public System.String BudgetDocCategoryName
        {
            get { return ((BudgetDocCategory != null) ? BudgetDocCategory.Name : ""); }
        }
        /// <summary>
        /// �������� ����� ���������, � ������� ����� ���� ������������ ������ ��� ���������
        /// </summary>
        public List<CBudgetDocTypeTransformTypeItem> BudgetDocTransformCollection { get; set; }
        /// <summary>
        /// ������ ����������� ����� ��������
        /// </summary>
        public List<CBudgetType> ValidBudgetTypeList { get; set; }
        #endregion

        #region ������������ 
        public CBudgetDocType()
        {
            this.m_uuidID = System.Guid.Empty;
            this.m_strName = "";
            this.m_objBudgetDocTypeDraw = null;
            this.m_bNeedDivision = false;
            this.m_strControlClassName = "";
            this.m_iPriority = -1;
            this.m_ImageSmall = null;
            this.m_ImageBig = null;
            IsActive = false;
            IsCanTransformInOtherType = false;
            IsCanChangeCategoryInBudgetDoc = false;
            BudgetDocCategory = null;
            ValidBudgetTypeList = null;
        }
        public CBudgetDocType( System.Guid uuidID, System.String strName )
        {
            this.m_uuidID = uuidID;
            this.m_strName = strName;
            this.m_objBudgetDocTypeDraw = null;
            this.m_bNeedDivision = false;
            this.m_strControlClassName = "";
            this.m_iPriority = -1;
            this.m_ImageSmall = null;
            this.m_ImageBig = null;
            IsActive = false;
            IsCanTransformInOtherType = false;
            IsCanChangeCategoryInBudgetDoc = false;
            BudgetDocCategory = null;
            ValidBudgetTypeList = null;

        }
        public CBudgetDocType( System.Guid uuidID, System.String strName, System.Boolean bNeedDivision,
            System.String strControlClassName, System.Int32 iPriority)
        {
            this.m_uuidID = uuidID;
            this.m_strName = strName;
            this.m_objBudgetDocTypeDraw = null;
            this.m_bNeedDivision = bNeedDivision;
            this.m_strControlClassName = strControlClassName;
            this.m_iPriority = iPriority;
            this.m_ImageSmall = null;
            this.m_ImageBig = null;
            IsActive = false;
            IsCanTransformInOtherType = false;
            IsCanChangeCategoryInBudgetDoc = false;
            BudgetDocCategory = null;
        }
        #endregion

        #region ������ ����� ���������� 
        /// <summary>
        /// ���������� ������ ����� ��������� ����������
        /// </summary>
        /// <param name="objProfile">�������</param>
        /// <returns>������ ����� ��������� ����������</returns>
        public CBaseList<CBudgetDocType> GetBudgetDocTypeList( UniXP.Common.CProfile objProfile )
        {
            CBaseList<CBudgetDocType> objList = new CBaseList<CBudgetDocType>();

            System.Data.SqlClient.SqlConnection DBConnection = objProfile.GetDBSource();
            if( DBConnection == null ) { return objList; }

            try
            {
                // ���������� � �� ��������, ����������� ������� �� ������� ������
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
                cmd.Connection = DBConnection;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = System.String.Format( "[{0}].[dbo].[sp_GetBudgetDocType]", objProfile.GetOptionsDllDBName() );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ( ( System.Byte )( 0 ) ), ( ( System.Byte )( 0 ) ), "", System.Data.DataRowVersion.Current, null ) );
                System.Data.SqlClient.SqlDataReader rs = cmd.ExecuteReader();
                if( rs.HasRows )
                {
                    // ����� ������ ��������
                    CBudgetDocType objBudgetDocType = null;
                    while( rs.Read() )
                    {
                        objBudgetDocType = new CBudgetDocType();
                        objBudgetDocType.m_uuidID = rs.GetGuid( 0 );
                        objBudgetDocType.m_strName = rs.GetString( 1 );
                        objList.AddItemToList( objBudgetDocType );
                    }
                }
                else
                {
                    //DevExpress.XtraEditors.XtraMessageBox.Show( 
                    //"�� ������� �������� ������ ����� ��������� ����������.\n� �� �� ������� ����������.", "��������",
                    //System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning );

                }
                cmd.Dispose();
                rs.Dispose();
            }
            catch( System.Exception e )
            {
                DevExpress.XtraEditors.XtraMessageBox.Show( 
                "�� ������� �������� ������ ����� ��������� ����������.\n" + e.Message, "��������",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
            }
			finally // ������� ���������� �������
            {
                DBConnection.Close();
            }
            return objList;
        }
        /// <summary>
        /// ���������� ������ ����� ��������� ����������
        /// </summary>
        /// <param name="objProfile">�������</param>
        /// <returns>������ ����� ��������� ����������</returns>
        public static List<CBudgetDocType> GetDocTypeList( UniXP.Common.CProfile objProfile )
        {
            List<CBudgetDocType> objList = new List<CBudgetDocType>();

            System.Data.SqlClient.SqlConnection DBConnection = objProfile.GetDBSource();
            if( DBConnection == null ) { return objList; }

            try
            {
                // ���������� � �� ��������, ����������� ������� �� ������� ������
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
                cmd.Connection = DBConnection;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = System.String.Format( "[{0}].[dbo].[sp_GetBudgetDocType]", objProfile.GetOptionsDllDBName() );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ( ( System.Byte )( 0 ) ), ( ( System.Byte )( 0 ) ), "", System.Data.DataRowVersion.Current, null ) );
                System.Data.SqlClient.SqlDataReader rs = cmd.ExecuteReader();
                if( rs.HasRows )
                {
                    // ����� ������ ��������
                    while( rs.Read() )
                    {
                        objList.Add(new CBudgetDocType((System.Guid)rs["GUID_ID"],
                            (System.String)rs["BUDGETDOCTYPE_NAME"], (System.Boolean)rs["NEEDDISION"],
                            (System.String)rs["CLASS_NAME"], (System.Int32)rs["PRIORITY"]));
                    }
                }
                rs.Close();
                rs.Dispose();
                cmd.Dispose();
            }
            catch( System.Exception f )
            {
                DevExpress.XtraEditors.XtraMessageBox.Show( 
                "�� ������� �������� ������ ����� ��������� ����������.\n\n����� ������: " + f.Message, "��������",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
            }
			finally // ������� ���������� �������
            {
                DBConnection.Close();
            }
            return objList;
        }
        /// <summary>
        /// ���������� ������ ����� ��������� ����������
        /// </summary>
        /// <param name="objProfile">�������</param>
        /// <returns>������ ����� ��������� ����������</returns>
        public static List<CBudgetDocType> GetDocTypeListPictures( UniXP.Common.CProfile objProfile )
        {
            List<CBudgetDocType> objList = new List<CBudgetDocType>();

            System.Data.SqlClient.SqlConnection DBConnection = objProfile.GetDBSource();
            if( DBConnection == null ) { return objList; }

            try
            {
                // ���������� � �� ��������, ����������� ������� �� ������� ������
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
                cmd.Connection = DBConnection;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = System.String.Format( "[{0}].[dbo].[sp_GetBudgetDocType]", objProfile.GetOptionsDllDBName() );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ( ( System.Byte )( 0 ) ), ( ( System.Byte )( 0 ) ), "", System.Data.DataRowVersion.Current, null ) );
                System.Data.SqlClient.SqlDataReader rs = cmd.ExecuteReader();
                if( rs.HasRows )
                {
                    // ����� ������ ��������
                    while( rs.Read() )
                    {
                        objList.Add(new CBudgetDocType(( System.Guid )rs[ "GUID_ID" ],
                            (System.String)rs["BUDGETDOCTYPE_NAME"], (System.Boolean)rs["NEEDDISION"], 
                            (System.String)rs["CLASS_NAME"], (System.Int32)rs["PRIORITY"]));
                    }
                }
                rs.Close();
                // ������ �������� ���� ��������
                foreach( CBudgetDocType objBudgetDocType in objList )
                {
                    objBudgetDocType.BudgetDocTypeDraw = CBudgetDocTypeDraw.GetBudgetDocTypeDraw( objProfile, objBudgetDocType, cmd );
                }

                rs.Dispose();
                cmd.Dispose();
            }
            catch( System.Exception f )
            {
                DevExpress.XtraEditors.XtraMessageBox.Show( 
                "�� ������� �������� ������ ����� ��������� ����������.\n\n����� ������: " + f.Message, "��������",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
            }
			finally // ������� ���������� �������
            {
                DBConnection.Close();
            }
            return objList;
        }
        /// <summary>
        /// ��������� ������ ����� ��������� ����������
        /// </summary>
        /// <param name="objProfile">�������</param>
        /// <param name="cmd">SQL - �������</param>
        /// <param name="objList">����������� ������</param>
        /// <returns></returns>
        public static void RefreshBudgetDocTypeList( UniXP.Common.CProfile objProfile,
            System.Data.SqlClient.SqlCommand cmd, System.Collections.Generic.List<CBudgetDocType> objList )
        {
            if( cmd == null ) { return; }
            objList.Clear();

            try
            {
                cmd.Parameters.Clear();
                cmd.CommandText = System.String.Format( "[{0}].[dbo].[sp_GetBudgetDocType]", objProfile.GetOptionsDllDBName() );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ( ( System.Byte )( 0 ) ), ( ( System.Byte )( 0 ) ), "", System.Data.DataRowVersion.Current, null ) );
                System.Data.SqlClient.SqlDataReader rs = cmd.ExecuteReader();
                if( rs.HasRows )
                {
                    while( rs.Read() )
                    {
                        objList.Add( new CBudgetDocType( ( System.Guid )rs[ "GUID_ID" ],
                            (System.String)rs["BUDGETDOCTYPE_NAME"], (System.Boolean)rs["NEEDDISION"], 
                            (System.String)rs["CLASS_NAME"], (System.Int32)rs["PRIORITY"]));
                    }
                }
                rs.Close();
                rs.Dispose();
            }
            catch( System.Exception e )
            {
                DevExpress.XtraEditors.XtraMessageBox.Show( 
                "�� ������� �������� ������ ����� ���������� ���������.\n����� ������: " + e.Message, "��������",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
            }
			finally // ������� ���������� �������
            {
            }
            return;
        }

        /// <summary>
        /// ��������� ������ �� ������� ����� ��������� ����������
        /// </summary>
        /// <param name="objProfile">�������</param>
        /// <param name="objTreeList">������</param>
        /// <returns>true - ������� ����������; false - ������</returns>
        public static System.Boolean bRefreshBudgetDocTypeTree( UniXP.Common.CProfile objProfile,
            DevExpress.XtraTreeList.TreeList objTreeList )
        {
            System.Boolean bRet = false;
            // ������� ������
            objTreeList.Nodes.Clear();
            // ����������� ���������� � ��
            System.Data.SqlClient.SqlConnection DBConnection = objProfile.GetDBSource();
            if( DBConnection == null )
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(
                    "�� ������� �������� ������ ����� ��������� ����������.\n����������� ���������� � ��.", "��������",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
                return bRet;
            }

            try
            {
                // ���������� � �� ��������, ����������� ������� �� ������� ������
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
                cmd.Connection = DBConnection;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = System.String.Format( "[{0}].[dbo].[sp_GetBudgetDocType]", objProfile.GetOptionsDllDBName() );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ( ( System.Byte )( 0 ) ), ( ( System.Byte )( 0 ) ), "", System.Data.DataRowVersion.Current, null ) );
                System.Data.SqlClient.SqlDataReader rs = cmd.ExecuteReader();
                if( rs.HasRows )
                {
                    // ����� ������ ��������
                    CBudgetDocType objBudgetDocType = null;
                    while( rs.Read() )
                    {
                        // ������� ������ ������ CBudgetDocType
                        objBudgetDocType = new CBudgetDocType( ( System.Guid )rs[ "GUID_ID" ],
                            (System.String)rs["BUDGETDOCTYPE_NAME"], (System.Boolean)rs["NEEDDISION"], 
                            (System.String)rs["CLASS_NAME"], (System.Int32)rs["PRIORITY"]);
                        // ������� ���� ������
                        DevExpress.XtraTreeList.Nodes.TreeListNode objNode = 
                                objTreeList.AppendNode( new object[] { objBudgetDocType.Name }, null );
                        objNode.Tag = objBudgetDocType;

                    }
                }
                rs.Close();
                // ������ �������� ���� ��������
                foreach (DevExpress.XtraTreeList.Nodes.TreeListNode objNodeItem in objTreeList.Nodes)
                {
                    CBudgetDocType objBudgetDocTypeItem = (CBudgetDocType)objNodeItem.Tag;
                    objBudgetDocTypeItem.BudgetDocTypeDraw = CBudgetDocTypeDraw.GetBudgetDocTypeDraw(objProfile, objBudgetDocTypeItem, cmd);
                    objBudgetDocTypeItem.ImageSmall = objBudgetDocTypeItem.BudgetDocTypeDraw.ImageDocTypeSmall;
                    objBudgetDocTypeItem.ImageBig = objBudgetDocTypeItem.BudgetDocTypeDraw.ImageDocType;
                }
                rs.Dispose();
                cmd.Dispose();

                bRet = true;
            }
            catch( System.Exception f )
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(
                "�� ������� �������� ������ ����� ��������� ����������.\n\n����� ������: " + f.Message, "��������",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
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
                cmd.CommandText = System.String.Format( "[{0}].[dbo].[sp_GetBudgetDocType]", objProfile.GetOptionsDllDBName() );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ( ( System.Byte )( 0 ) ), ( ( System.Byte )( 0 ) ), "", System.Data.DataRowVersion.Current, null ) );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@GUID_ID", System.Data.SqlDbType.UniqueIdentifier ) );
                cmd.Parameters[ "@GUID_ID" ].Value = uuidID;
                System.Data.SqlClient.SqlDataReader rs = cmd.ExecuteReader();
                if( rs.HasRows )
                {
                    // ����� ������ ��������, � ��� ��� ���������� ���� ������
                    rs.Read();

                    this.uuidID = (System.Guid)rs["GUID_ID"];
                    this.Name = System.Convert.ToString(rs["BUDGETDOCTYPE_NAME"]);
                    this.ControlClassName = System.Convert.ToString(rs["CLASS_NAME"]);
                    this.Priority  = System.Convert.ToInt32(rs["PRIORITY"]); 
                    this.NeedDivision = System.Convert.ToBoolean(rs["NEEDDISION"]);
                    this.IsCanChangeCategoryInBudgetDoc = System.Convert.ToBoolean(rs["BUDGETDOCTYPE_CANCHANGECATEGORYINBUDGETDOC"]);
                    this.IsCanTransformInOtherType = System.Convert.ToBoolean(rs["BUDGETDOCTYPE_CANTRANSFORMINOTHERTYPE"]);
                    this.IsActive = System.Convert.ToBoolean(rs["BUDGETDOCTYPE_ACTIVE"]);
                    this.BudgetDocCategory = ((rs["BUDGETDOCCATEGORY_GUID_ID"] == System.DBNull.Value) ? null : new CBudgetDocCategory() { uuidID = (System.Guid)rs["BUDGETDOCCATEGORY_GUID_ID"], Name = System.Convert.ToString(rs["BUDGETDOCCATEGORY_NAME"]) }); 

                    bRet = true;
                }
                else
                {
                    DevExpress.XtraEditors.XtraMessageBox.Show( 
                    "�� ������� ������������������� ����� CBudgetDocType.\n� �� �� ������� ����������.", "��������",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning );

                }
                cmd.Dispose();
                rs.Dispose();
            }
            catch( System.Exception e )
            {
                DevExpress.XtraEditors.XtraMessageBox.Show( 
                "�� ������� ������������������� ����� CBudgetDocType.\n" + e.Message, "��������",
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
                DevExpress.XtraEditors.XtraMessageBox.Show( "������������ �������� ����������� �������������� �������", "��������",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
                return bRet;
            }

            System.Data.SqlClient.SqlConnection DBConnection = objProfile.GetDBSource();
            if( DBConnection == null )
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(
                "�� ������� ���������� ���������� � ����� ������.", "��������",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
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
                cmd.CommandText = System.String.Format( "[{0}].[dbo].[sp_DeleteBudgetDocType]", objProfile.GetOptionsDllDBName() );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ( ( System.Byte )( 0 ) ), ( ( System.Byte )( 0 ) ), "", System.Data.DataRowVersion.Current, null ) );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@GUID_ID", System.Data.SqlDbType.UniqueIdentifier ) );
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
                            DevExpress.XtraEditors.XtraMessageBox.Show(  "�� ��� ��������� ���� ������ � ��������� ���������.\n�� ���� ���������: " + this.uuidID.ToString(), "��������",
                                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning );
                            break;
                        }
                        case 2:
                        {
                            DevExpress.XtraEditors.XtraMessageBox.Show(  "�� ��� ��������� ���� ������ � �������� ����.\n�� ���� ���������: " + this.uuidID.ToString(), "��������",
                                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning );
                            break;
                        }
                        default:
                        {
                            DevExpress.XtraEditors.XtraMessageBox.Show(  "������ ���������� ������� �� �������� ���� ���������.\n��� ���������� ��������� : " + this.m_strName, "��������",
                                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
                            break;
                        }
                    }
                }
                cmd.Dispose();
            }
            catch( System.Exception e )
            {
                DBTransaction.Rollback();
                DevExpress.XtraEditors.XtraMessageBox.Show( 
                "�� ������� ������� ��� ���������� ���������.\n" + e.Message, "��������",
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
        /// ��������� �������� ������� �� ������� ������������� ����������
        /// </summary>
        /// <returns>true - ������ ���; false - ������</returns>
        public System.Boolean IsValidateProperties()
        {
            System.Boolean bRet = false;
            try
            {
                // ������������ �� ������ ���� ������
                if( this.m_strName == "" )
                {
                    DevExpress.XtraEditors.XtraMessageBox.Show( "������������ �������� ������������ �������", "��������",
                        System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
                    return bRet;
                }
                // �������� 16�16
                if (this.m_ImageSmall == null)
                {
                    DevExpress.XtraEditors.XtraMessageBox.Show("���������� ������� �������� �������� 16�16 ��������", "��������",
                        System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    return bRet;
                }
                // �������� 24�24
                if (this.m_ImageBig == null)
                {
                    DevExpress.XtraEditors.XtraMessageBox.Show("���������� ������� �������� �������� 24�24 �������", "��������",
                        System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    return bRet;
                }

                bRet = true;
            }
            catch( System.Exception f )
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(
                "������ �������� ������� �������.\n\n����� ������: " + f.Message, "��������",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
            }

            return bRet;
        }
        /// <summary>
        /// �������� ������ � ��
        /// </summary>
        /// <param name="objProfile">�������</param>
        /// <returns>true - ������� ����������; false - ������</returns>
        public override System.Boolean Add( UniXP.Common.CProfile objProfile )
        {
            System.String strErr = "";

            System.Guid GUID_ID = System.Guid.Empty;

            System.Boolean bRet = CBudgetDocTypeDataBaseModel.AddNewObjectToDataBase(this.Name, this.NeedDivision, this.IsActive, this.ControlClassName,
                this.Priority, this.BudgetDocCategory.uuidID, this.IsCanTransformInOtherType, this.IsCanChangeCategoryInBudgetDoc, 
                CBudgetDocTypeTransformTypeItem.ConvertListToTable( this.BudgetDocTransformCollection, this.uuidID, ref strErr), 
                this.ImageBig, this.ImageSmall, ref GUID_ID, objProfile, ref strErr);
            if (bRet == true)
            {
                this.uuidID = GUID_ID;
            }
            else
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(strErr, "��������",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
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
            System.String strErr = "";

            System.Boolean bRet = CBudgetDocTypeDataBaseModel.EditObjectInDataBase( this.uuidID, this.Name, this.NeedDivision, this.IsActive, this.ControlClassName,
                this.Priority, this.BudgetDocCategory.uuidID, this.IsCanTransformInOtherType, this.IsCanChangeCategoryInBudgetDoc,
                CBudgetDocTypeTransformTypeItem.ConvertListToTable(this.BudgetDocTransformCollection, this.uuidID, ref strErr),
                this.ImageBig, this.ImageSmall, objProfile, ref strErr);
            if (bRet == false)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(strErr, "��������",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }

            return bRet;
        }
        #endregion

        /// <summary>
        /// ���������� ������ ��������� ���� ���������� ���������
        /// </summary>
        /// <param name="objProfile">�������</param>
        public void LoadPicture( UniXP.Common.CProfile objProfile )
        {
            try
            {
                this.m_objBudgetDocTypeDraw = CBudgetDocTypeDraw.GetBudgetDocTypeDraw( objProfile, this );
            }
            catch( System.Exception f )
            {
                DevExpress.XtraEditors.XtraMessageBox.Show( 
                "�� ������� ���������� ��� ����������� ��������.\n\n����� ������: " + f.Message, "��������",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
            }
            return ;
        }

        public override string ToString()
        {
            return Name;
        }

    }

    public static class CBudgetDocTypeDataBaseModel
    {
        #region ���������� ����� ������

        public static System.Boolean IsAllParametersValid(System.String BudgetDocType_Name, System.String BudgetDocType_ClassName,
            System.Int32 BudgetDocType_Priority, System.Guid BudgetDocCategory_Guid, 
            ref System.String strErr)
        {
            System.Boolean bRet = false;
            try
            {
                if (BudgetDocType_Name.Trim() == "")
                {
                    strErr += ("���������� ������� ������������ ���� ���������� ���������!");
                    return bRet;
                }
                if (BudgetDocType_ClassName.Trim() == "" )
                {
                    strErr += ("���������� ������� ������������ ������, ����������� �� ���������� ���� ���������� ���������!");
                    return bRet;
                }
                if (BudgetDocType_Priority < 0)
                {
                    strErr += ("�������� ���������� ������ ���� ������ ���� ����� ����.");
                    return bRet;
                }
                if (BudgetDocCategory_Guid.Equals(System.Guid.Empty) == true)
                {
                    strErr += ("���������� ������� ��� ���������� ��������� �� ���������!");
                    return bRet;
                }

                bRet = true;
            }
            catch (System.Exception f)
            {
                strErr += ("������ �������� ������� ������� '��� ���������� ���������'. ����� ������: " + f.Message);
            }
            return bRet;
        }

        public static System.Boolean AddNewObjectToDataBase(System.String BUDGETDOCTYPE_NAME,
            System.Boolean NEEDDISION, System.Boolean BUDGETDOCTYPE_ACTIVE, System.String CLASS_NAME, 
            System.Int32 PRIORITY, System.Guid BUDGETDOCCATEGORY_GUID_ID, System.Boolean BUDGETDOCTYPE_CANTRANSFORMINOTHERTYPE,
            System.Boolean BUDGETDOCTYPE_CANCHANGECATEGORYINBUDGETDOC, System.Data.DataTable addedItems,
            System.Drawing.Image ImageBig, System.Drawing.Image ImageSmall,
            ref System.Guid GUID_ID, UniXP.Common.CProfile objProfile, ref System.String strErr)
        {
            System.Boolean bRet = false;

            if (IsAllParametersValid(BUDGETDOCTYPE_NAME, CLASS_NAME, PRIORITY, BUDGETDOCCATEGORY_GUID_ID, ref strErr) == false) { return bRet; }

            System.Data.SqlClient.SqlConnection DBConnection = null;
            System.Data.SqlClient.SqlCommand cmd = null;
            System.Data.SqlClient.SqlTransaction DBTransaction = null;

            try
            {
                DBConnection = objProfile.GetDBSource();
                if (DBConnection == null)
                {
                    strErr += ("�� ������� �������� ���������� � ����� ������.");
                    return bRet;
                }
                DBTransaction = DBConnection.BeginTransaction();
                cmd = new System.Data.SqlClient.SqlCommand();
                cmd.Connection = DBConnection;
                cmd.Transaction = DBTransaction;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = System.String.Format("[{0}].[dbo].[usp_AddBudgeDocType]", objProfile.GetOptionsDllDBName());
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ((System.Byte)(0)), ((System.Byte)(0)), "", System.Data.DataRowVersion.Current, null));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@GUID_ID", System.Data.SqlDbType.UniqueIdentifier, 4, System.Data.ParameterDirection.Output, false, ((System.Byte)(0)), ((System.Byte)(0)), "", System.Data.DataRowVersion.Current, null));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@BUDGETDOCTYPE_NAME", System.Data.DbType.String));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@NEEDDISION", System.Data.SqlDbType.Bit));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@BUDGETDOCTYPE_ACTIVE", System.Data.SqlDbType.Bit));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@CLASS_NAME", System.Data.DbType.String));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@PRIORITY", System.Data.SqlDbType.Int));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@BUDGETDOCCATEGORY_GUID_ID", System.Data.SqlDbType.UniqueIdentifier));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@BUDGETDOCTYPE_CANTRANSFORMINOTHERTYPE", System.Data.SqlDbType.Bit));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@BUDGETDOCTYPE_CANCHANGECATEGORYINBUDGETDOC", System.Data.SqlDbType.Bit));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@BUDGETDOCTYPE_PICTURE_SMALL", System.Data.SqlDbType.Image));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@BUDGETDOCTYPE_PICTURE", System.Data.SqlDbType.Image));
                cmd.Parameters.AddWithValue("@tBudgetDocTypeTransform", addedItems);
                cmd.Parameters["@tBudgetDocTypeTransform"].SqlDbType = System.Data.SqlDbType.Structured;
                cmd.Parameters["@tBudgetDocTypeTransform"].TypeName = "dbo.udt_BudgetDocTypeTransform";
                
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ERROR_NUM", System.Data.SqlDbType.Int, 8, System.Data.ParameterDirection.Output, false, ((System.Byte)(0)), ((System.Byte)(0)), "", System.Data.DataRowVersion.Current, null));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ERROR_MES", System.Data.SqlDbType.NVarChar, 4000));
                cmd.Parameters["@ERROR_MES"].Direction = System.Data.ParameterDirection.Output;
                cmd.Parameters["@BUDGETDOCTYPE_NAME"].Value = BUDGETDOCTYPE_NAME;
                cmd.Parameters["@NEEDDISION"].Value = NEEDDISION;
                cmd.Parameters["@BUDGETDOCTYPE_ACTIVE"].Value = BUDGETDOCTYPE_ACTIVE;
                cmd.Parameters["@CLASS_NAME"].Value = CLASS_NAME;
                cmd.Parameters["@PRIORITY"].Value = PRIORITY;
                cmd.Parameters["@BUDGETDOCCATEGORY_GUID_ID"].Value = BUDGETDOCCATEGORY_GUID_ID;
                cmd.Parameters["@BUDGETDOCTYPE_CANTRANSFORMINOTHERTYPE"].Value = BUDGETDOCTYPE_CANTRANSFORMINOTHERTYPE;
                cmd.Parameters["@BUDGETDOCTYPE_CANCHANGECATEGORYINBUDGETDOC"].Value = BUDGETDOCTYPE_CANCHANGECATEGORYINBUDGETDOC;

                System.IO.MemoryStream mstr = new System.IO.MemoryStream();
                ImageSmall.Save(mstr, ImageSmall.RawFormat);
                byte[] arrImage = mstr.GetBuffer();

                System.IO.MemoryStream mstr2 = new System.IO.MemoryStream();
                ImageBig.Save(mstr2, ImageBig.RawFormat);
                byte[] arrImageBig = mstr2.GetBuffer();

                cmd.Parameters["@BUDGETDOCTYPE_PICTURE_SMALL"].Value = arrImage;
                cmd.Parameters["@BUDGETDOCTYPE_PICTURE"].Value = arrImageBig;

                cmd.ExecuteNonQuery();
                System.Int32 iRes = (System.Int32)cmd.Parameters["@RETURN_VALUE"].Value;
                if (iRes == 0)
                {
                    GUID_ID = (System.Guid)cmd.Parameters["@GUID_ID"].Value;
                    // ������������ ����������
                    DBTransaction.Commit();
                }
                else
                {
                    DBTransaction.Rollback();
                    strErr += ((cmd.Parameters["@ERROR_MES"].Value == System.DBNull.Value) ? "" : (System.String)cmd.Parameters["@ERROR_MES"].Value);
                }

                cmd.Dispose();
                bRet = (iRes == 0);
            }
            catch (System.Exception f)
            {
                DBTransaction.Rollback();

                strErr += ("�� ������� ������� ������ '��� ���������� ���������'. ����� ������: " + f.Message);
            }
            finally
            {
                DBConnection.Close();
            }
            return bRet;
        }

        public static System.Boolean AddNewObjectToDataBase2(System.String BUDGETDOCTYPE_NAME,
            System.Boolean NEEDDISION, System.Boolean BUDGETDOCTYPE_ACTIVE, System.String CLASS_NAME,
            System.Int32 PRIORITY, System.Guid BUDGETDOCCATEGORY_GUID_ID, System.Boolean BUDGETDOCTYPE_CANTRANSFORMINOTHERTYPE,
            System.Boolean BUDGETDOCTYPE_CANCHANGECATEGORYINBUDGETDOC, System.Data.DataTable addedItems,
            System.Drawing.Image ImageBig, System.Drawing.Image ImageSmall,
            ref System.Guid GUID_ID, UniXP.Common.CProfile objProfile, ref System.String strErr)
        {
            System.Boolean bRet = false;

            if (IsAllParametersValid(BUDGETDOCTYPE_NAME, CLASS_NAME, PRIORITY, BUDGETDOCCATEGORY_GUID_ID, ref strErr) == false) { return bRet; }

            System.Data.SqlClient.SqlConnection DBConnection = null;
            System.Data.SqlClient.SqlCommand cmd = null;
            System.Data.SqlClient.SqlTransaction DBTransaction = null;

            try
            {
                DBConnection = objProfile.GetDBSource();
                if (DBConnection == null)
                {
                    strErr += ("�� ������� �������� ���������� � ����� ������.");
                    return bRet;
                }
                DBTransaction = DBConnection.BeginTransaction();
                cmd = new System.Data.SqlClient.SqlCommand();
                cmd.Connection = DBConnection;
                cmd.Transaction = DBTransaction;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = System.String.Format("[{0}].[dbo].[usp_AddBudgeDocType2]", objProfile.GetOptionsDllDBName());
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ((System.Byte)(0)), ((System.Byte)(0)), "", System.Data.DataRowVersion.Current, null));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@GUID_ID", System.Data.SqlDbType.UniqueIdentifier, 4, System.Data.ParameterDirection.Output, false, ((System.Byte)(0)), ((System.Byte)(0)), "", System.Data.DataRowVersion.Current, null));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@BUDGETDOCTYPE_NAME", System.Data.DbType.String));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@NEEDDISION", System.Data.SqlDbType.Bit));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@BUDGETDOCTYPE_ACTIVE", System.Data.SqlDbType.Bit));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@CLASS_NAME", System.Data.DbType.String));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@PRIORITY", System.Data.SqlDbType.Int));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@BUDGETDOCCATEGORY_GUID_ID", System.Data.SqlDbType.UniqueIdentifier));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@BUDGETDOCTYPE_CANTRANSFORMINOTHERTYPE", System.Data.SqlDbType.Bit));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@BUDGETDOCTYPE_CANCHANGECATEGORYINBUDGETDOC", System.Data.SqlDbType.Bit));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@BUDGETDOCTYPE_PICTURE_SMALL", System.Data.SqlDbType.Image));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@BUDGETDOCTYPE_PICTURE", System.Data.SqlDbType.Image));

                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ERROR_NUM", System.Data.SqlDbType.Int, 8, System.Data.ParameterDirection.Output, false, ((System.Byte)(0)), ((System.Byte)(0)), "", System.Data.DataRowVersion.Current, null));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ERROR_MES", System.Data.SqlDbType.NVarChar, 4000));
                cmd.Parameters["@ERROR_MES"].Direction = System.Data.ParameterDirection.Output;
                cmd.Parameters["@BUDGETDOCTYPE_NAME"].Value = BUDGETDOCTYPE_NAME;
                cmd.Parameters["@NEEDDISION"].Value = NEEDDISION;
                cmd.Parameters["@BUDGETDOCTYPE_ACTIVE"].Value = BUDGETDOCTYPE_ACTIVE;
                cmd.Parameters["@CLASS_NAME"].Value = CLASS_NAME;
                cmd.Parameters["@PRIORITY"].Value = PRIORITY;
                cmd.Parameters["@BUDGETDOCCATEGORY_GUID_ID"].Value = BUDGETDOCCATEGORY_GUID_ID;
                cmd.Parameters["@BUDGETDOCTYPE_CANTRANSFORMINOTHERTYPE"].Value = BUDGETDOCTYPE_CANTRANSFORMINOTHERTYPE;
                cmd.Parameters["@BUDGETDOCTYPE_CANCHANGECATEGORYINBUDGETDOC"].Value = BUDGETDOCTYPE_CANCHANGECATEGORYINBUDGETDOC;

                System.IO.MemoryStream mstr = new System.IO.MemoryStream();
                ImageSmall.Save(mstr, ImageSmall.RawFormat);
                byte[] arrImage = mstr.GetBuffer();

                System.IO.MemoryStream mstr2 = new System.IO.MemoryStream();
                ImageBig.Save(mstr2, ImageBig.RawFormat);
                byte[] arrImageBig = mstr2.GetBuffer();

                cmd.Parameters["@BUDGETDOCTYPE_PICTURE_SMALL"].Value = arrImage;
                cmd.Parameters["@BUDGETDOCTYPE_PICTURE"].Value = arrImageBig;

                cmd.ExecuteNonQuery();
                System.Int32 iRes = (System.Int32)cmd.Parameters["@RETURN_VALUE"].Value;
                if (iRes == 0)
                {
                    GUID_ID = (System.Guid)cmd.Parameters["@GUID_ID"].Value;
                    if (RemoveBudgetDocTypeTransformCollection(GUID_ID, addedItems, objProfile, DBConnection, cmd, ref strErr) == true)
                    {
                        if (SaveBudgetDocTypeTransformCollection2(GUID_ID, addedItems, objProfile, DBConnection, cmd, ref strErr) == true)
                        {
                            // ������������ ����������
                            DBTransaction.Commit();
                            bRet = true;
                        }
                        else
                        {
                            strErr += ((cmd.Parameters["@ERROR_MES"].Value == System.DBNull.Value) ? "" : (System.String)cmd.Parameters["@ERROR_MES"].Value);
                        }
                    }
                    else
                    {
                        strErr += ((cmd.Parameters["@ERROR_MES"].Value == System.DBNull.Value) ? "" : (System.String)cmd.Parameters["@ERROR_MES"].Value);
                    }
                }
                else
                {
                    DBTransaction.Rollback();
                    strErr += ((cmd.Parameters["@ERROR_MES"].Value == System.DBNull.Value) ? "" : (System.String)cmd.Parameters["@ERROR_MES"].Value);
                    bRet = false;
                }

                cmd.Dispose();
                if (bRet == false)
                {
                    GUID_ID = System.Guid.Empty;
                }
            }
            catch (System.Exception f)
            {
                DBTransaction.Rollback();

                strErr += ("�� ������� ������� ������ '��� ���������� ���������'. ����� ������: " + f.Message);
            }
            finally
            {
                DBConnection.Close();
            }
            return bRet;
        }

        #endregion

        #region ������������� ������ � ���� ������
        public static System.Boolean EditObjectInDataBase(System.Guid GUID_ID, System.String BUDGETDOCTYPE_NAME,
            System.Boolean NEEDDISION, System.Boolean BUDGETDOCTYPE_ACTIVE, System.String CLASS_NAME, 
            System.Int32 PRIORITY, System.Guid BUDGETDOCCATEGORY_GUID_ID, System.Boolean BUDGETDOCTYPE_CANTRANSFORMINOTHERTYPE,
            System.Boolean BUDGETDOCTYPE_CANCHANGECATEGORYINBUDGETDOC, System.Data.DataTable addedItems,
            System.Drawing.Image ImageBig, System.Drawing.Image ImageSmall,
            UniXP.Common.CProfile objProfile, ref System.String strErr)
        {
            System.Boolean bRet = false;

            if (IsAllParametersValid(BUDGETDOCTYPE_NAME, CLASS_NAME, PRIORITY, BUDGETDOCCATEGORY_GUID_ID, ref strErr) == false) { return bRet; }

            System.Data.SqlClient.SqlConnection DBConnection = null;
            System.Data.SqlClient.SqlCommand cmd = null;
            System.Data.SqlClient.SqlTransaction DBTransaction = null;

            try
            {
                DBConnection = objProfile.GetDBSource();
                if (DBConnection == null)
                {
                    strErr += ("�� ������� �������� ���������� � ����� ������.");
                    return bRet;
                }
                DBTransaction = DBConnection.BeginTransaction();
                cmd = new System.Data.SqlClient.SqlCommand();
                cmd.Connection = DBConnection;
                cmd.Transaction = DBTransaction;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = System.String.Format("[{0}].[dbo].[usp_EditBudgeDocType]", objProfile.GetOptionsDllDBName());
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ((System.Byte)(0)), ((System.Byte)(0)), "", System.Data.DataRowVersion.Current, null));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@GUID_ID", System.Data.SqlDbType.UniqueIdentifier));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@BUDGETDOCTYPE_NAME", System.Data.DbType.String));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@NEEDDISION", System.Data.SqlDbType.Bit));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@BUDGETDOCTYPE_ACTIVE", System.Data.SqlDbType.Bit));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@CLASS_NAME", System.Data.DbType.String));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@PRIORITY", System.Data.SqlDbType.Int));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@BUDGETDOCCATEGORY_GUID_ID", System.Data.SqlDbType.UniqueIdentifier));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@BUDGETDOCTYPE_CANTRANSFORMINOTHERTYPE", System.Data.SqlDbType.Bit));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@BUDGETDOCTYPE_CANCHANGECATEGORYINBUDGETDOC", System.Data.SqlDbType.Bit));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@BUDGETDOCTYPE_PICTURE_SMALL", System.Data.SqlDbType.Image));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@BUDGETDOCTYPE_PICTURE", System.Data.SqlDbType.Image));
                cmd.Parameters.AddWithValue("@tBudgetDocTypeTransform", addedItems);
                cmd.Parameters["@tBudgetDocTypeTransform"].SqlDbType = System.Data.SqlDbType.Structured;
                cmd.Parameters["@tBudgetDocTypeTransform"].TypeName = "dbo.udt_BudgetDocTypeTransform";

                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ERROR_NUM", System.Data.SqlDbType.Int, 8, System.Data.ParameterDirection.Output, false, ((System.Byte)(0)), ((System.Byte)(0)), "", System.Data.DataRowVersion.Current, null));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ERROR_MES", System.Data.SqlDbType.NVarChar, 4000));
                cmd.Parameters["@ERROR_MES"].Direction = System.Data.ParameterDirection.Output;
                cmd.Parameters["@GUID_ID"].Value = GUID_ID;
                cmd.Parameters["@BUDGETDOCTYPE_NAME"].Value = BUDGETDOCTYPE_NAME;
                cmd.Parameters["@NEEDDISION"].Value = NEEDDISION;
                cmd.Parameters["@BUDGETDOCTYPE_ACTIVE"].Value = BUDGETDOCTYPE_ACTIVE;
                cmd.Parameters["@CLASS_NAME"].Value = CLASS_NAME;
                cmd.Parameters["@PRIORITY"].Value = PRIORITY;
                cmd.Parameters["@BUDGETDOCCATEGORY_GUID_ID"].Value = BUDGETDOCCATEGORY_GUID_ID;
                cmd.Parameters["@BUDGETDOCTYPE_CANTRANSFORMINOTHERTYPE"].Value = BUDGETDOCTYPE_CANTRANSFORMINOTHERTYPE;
                cmd.Parameters["@BUDGETDOCTYPE_CANCHANGECATEGORYINBUDGETDOC"].Value = BUDGETDOCTYPE_CANCHANGECATEGORYINBUDGETDOC;

                System.IO.MemoryStream mstr = new System.IO.MemoryStream();
                ImageSmall.Save(mstr, ImageSmall.RawFormat);
                byte[] arrImage = mstr.GetBuffer();

                System.IO.MemoryStream mstr2 = new System.IO.MemoryStream();
                ImageBig.Save(mstr2, ImageBig.RawFormat);
                byte[] arrImageBig = mstr2.GetBuffer();

                cmd.Parameters["@BUDGETDOCTYPE_PICTURE_SMALL"].Value = arrImage;
                cmd.Parameters["@BUDGETDOCTYPE_PICTURE"].Value = arrImageBig;

                cmd.ExecuteNonQuery();
                System.Int32 iRes = (System.Int32)cmd.Parameters["@RETURN_VALUE"].Value;
                if (iRes == 0)
                {
                    // ������������ ����������
                    DBTransaction.Commit();
                }
                else
                {
                    DBTransaction.Rollback();
                    strErr += ((cmd.Parameters["@ERROR_MES"].Value == System.DBNull.Value) ? "" : (System.String)cmd.Parameters["@ERROR_MES"].Value);
                }

                cmd.Dispose();
                bRet = (iRes == 0);
            }
            catch (System.Exception f)
            {
                DBTransaction.Rollback();

                strErr += ("�� ������� ������ ��������� � �������� ������� '��� ���������� ���������'. ����� ������: " + f.Message);
            }
            finally
            {
                DBConnection.Close();
            }
            return bRet;
        }

        public static System.Boolean EditObjectInDataBase2(System.Guid GUID_ID, System.String BUDGETDOCTYPE_NAME,
            System.Boolean NEEDDISION, System.Boolean BUDGETDOCTYPE_ACTIVE, System.String CLASS_NAME,
            System.Int32 PRIORITY, System.Guid BUDGETDOCCATEGORY_GUID_ID, System.Boolean BUDGETDOCTYPE_CANTRANSFORMINOTHERTYPE,
            System.Boolean BUDGETDOCTYPE_CANCHANGECATEGORYINBUDGETDOC, System.Data.DataTable addedItems,
            System.Drawing.Image ImageBig, System.Drawing.Image ImageSmall,
            UniXP.Common.CProfile objProfile, ref System.String strErr)
        {
            System.Boolean bRet = false;

            if (IsAllParametersValid(BUDGETDOCTYPE_NAME, CLASS_NAME, PRIORITY, BUDGETDOCCATEGORY_GUID_ID, ref strErr) == false) { return bRet; }

            System.Data.SqlClient.SqlConnection DBConnection = null;
            System.Data.SqlClient.SqlCommand cmd = null;
            System.Data.SqlClient.SqlTransaction DBTransaction = null;

            try
            {
                DBConnection = objProfile.GetDBSource();
                if (DBConnection == null)
                {
                    strErr += ("�� ������� �������� ���������� � ����� ������.");
                    return bRet;
                }
                DBTransaction = DBConnection.BeginTransaction();
                cmd = new System.Data.SqlClient.SqlCommand();
                cmd.Connection = DBConnection;
                cmd.Transaction = DBTransaction;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = System.String.Format("[{0}].[dbo].[usp_EditBudgeDocType2]", objProfile.GetOptionsDllDBName());
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ((System.Byte)(0)), ((System.Byte)(0)), "", System.Data.DataRowVersion.Current, null));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@GUID_ID", System.Data.SqlDbType.UniqueIdentifier));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@BUDGETDOCTYPE_NAME", System.Data.DbType.String));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@NEEDDISION", System.Data.SqlDbType.Bit));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@BUDGETDOCTYPE_ACTIVE", System.Data.SqlDbType.Bit));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@CLASS_NAME", System.Data.DbType.String));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@PRIORITY", System.Data.SqlDbType.Int));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@BUDGETDOCCATEGORY_GUID_ID", System.Data.SqlDbType.UniqueIdentifier));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@BUDGETDOCTYPE_CANTRANSFORMINOTHERTYPE", System.Data.SqlDbType.Bit));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@BUDGETDOCTYPE_CANCHANGECATEGORYINBUDGETDOC", System.Data.SqlDbType.Bit));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@BUDGETDOCTYPE_PICTURE_SMALL", System.Data.SqlDbType.Image));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@BUDGETDOCTYPE_PICTURE", System.Data.SqlDbType.Image));

                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ERROR_NUM", System.Data.SqlDbType.Int, 8, System.Data.ParameterDirection.Output, false, ((System.Byte)(0)), ((System.Byte)(0)), "", System.Data.DataRowVersion.Current, null));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ERROR_MES", System.Data.SqlDbType.NVarChar, 4000));
                cmd.Parameters["@ERROR_MES"].Direction = System.Data.ParameterDirection.Output;
                cmd.Parameters["@GUID_ID"].Value = GUID_ID;
                cmd.Parameters["@BUDGETDOCTYPE_NAME"].Value = BUDGETDOCTYPE_NAME;
                cmd.Parameters["@NEEDDISION"].Value = NEEDDISION;
                cmd.Parameters["@BUDGETDOCTYPE_ACTIVE"].Value = BUDGETDOCTYPE_ACTIVE;
                cmd.Parameters["@CLASS_NAME"].Value = CLASS_NAME;
                cmd.Parameters["@PRIORITY"].Value = PRIORITY;
                cmd.Parameters["@BUDGETDOCCATEGORY_GUID_ID"].Value = BUDGETDOCCATEGORY_GUID_ID;
                cmd.Parameters["@BUDGETDOCTYPE_CANTRANSFORMINOTHERTYPE"].Value = BUDGETDOCTYPE_CANTRANSFORMINOTHERTYPE;
                cmd.Parameters["@BUDGETDOCTYPE_CANCHANGECATEGORYINBUDGETDOC"].Value = BUDGETDOCTYPE_CANCHANGECATEGORYINBUDGETDOC;

                System.IO.MemoryStream mstr = new System.IO.MemoryStream();
                ImageSmall.Save(mstr, ImageSmall.RawFormat);
                byte[] arrImage = mstr.GetBuffer();

                System.IO.MemoryStream mstr2 = new System.IO.MemoryStream();
                ImageBig.Save(mstr2, ImageBig.RawFormat);
                byte[] arrImageBig = mstr2.GetBuffer();

                cmd.Parameters["@BUDGETDOCTYPE_PICTURE_SMALL"].Value = arrImage;
                cmd.Parameters["@BUDGETDOCTYPE_PICTURE"].Value = arrImageBig;

                cmd.ExecuteNonQuery();
                System.Int32 iRes = (System.Int32)cmd.Parameters["@RETURN_VALUE"].Value;
                if (iRes == 0)
                {
                    if (RemoveBudgetDocTypeTransformCollection(GUID_ID, addedItems, objProfile, DBConnection, cmd, ref strErr) == true)
                    {
                        if (SaveBudgetDocTypeTransformCollection2(GUID_ID, addedItems, objProfile, DBConnection, cmd, ref strErr) == true)
                        {
                            // ������������ ����������
                            DBTransaction.Commit();
                            bRet = true;
                        }
                        else
                        {
                            strErr += ((cmd.Parameters["@ERROR_MES"].Value == System.DBNull.Value) ? "" : (System.String)cmd.Parameters["@ERROR_MES"].Value);
                        }
                    }
                    else
                    {
                        strErr += ((cmd.Parameters["@ERROR_MES"].Value == System.DBNull.Value) ? "" : (System.String)cmd.Parameters["@ERROR_MES"].Value);
                    }
                }
                else
                {
                    bRet = false;
                    DBTransaction.Rollback();
                    strErr += ((cmd.Parameters["@ERROR_MES"].Value == System.DBNull.Value) ? "" : (System.String)cmd.Parameters["@ERROR_MES"].Value);
                }

                cmd.Dispose();
            }
            catch (System.Exception f)
            {
                DBTransaction.Rollback();

                strErr += ("�� ������� ������ ��������� � �������� ������� '��� ���������� ���������'. ����� ������: " + f.Message);
            }
            finally
            {
                DBConnection.Close();
            }
            return bRet;
        }

        public static System.Boolean SaveBudgetDocTypeTransformCollection(System.Guid SRC_BUDGETDOCTYPE_GUID, System.Data.DataTable addedItems,
            UniXP.Common.CProfile objProfile, System.Data.SqlClient.SqlConnection DBConnection, System.Data.SqlClient.SqlCommand cmdSQL, ref System.String strErr)
        {
            System.Boolean bRet = false;

            try
            {
                if (DBConnection == null)
                {
                    strErr += ("�� ������� �������� ���������� � ����� ������.");
                    return bRet;
                }
                cmdSQL.CommandType = System.Data.CommandType.StoredProcedure;
                cmdSQL.Parameters.Clear();
                cmdSQL.CommandText = System.String.Format("[{0}].[dbo].[usp_AssignBudgetDocTypeTransform]", objProfile.GetOptionsDllDBName());
                cmdSQL.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ((System.Byte)(0)), ((System.Byte)(0)), "", System.Data.DataRowVersion.Current, null));
                cmdSQL.Parameters.Add(new System.Data.SqlClient.SqlParameter("@SRC_BUDGETDOCTYPE_GUID", System.Data.SqlDbType.UniqueIdentifier));
                cmdSQL.Parameters.AddWithValue("@tBudgetDocTypeTransform", addedItems);
                cmdSQL.Parameters["@tBudgetDocTypeTransform"].SqlDbType = System.Data.SqlDbType.Structured;
                cmdSQL.Parameters["@tBudgetDocTypeTransform"].TypeName = "dbo.udt_BudgetDocTypeTransform";
                cmdSQL.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ERROR_NUM", System.Data.SqlDbType.Int, 8, System.Data.ParameterDirection.Output, false, ((System.Byte)(0)), ((System.Byte)(0)), "", System.Data.DataRowVersion.Current, null));
                cmdSQL.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ERROR_MES", System.Data.SqlDbType.NVarChar, 4000));
                cmdSQL.Parameters["@ERROR_MES"].Direction = System.Data.ParameterDirection.Output;
                cmdSQL.Parameters["@SRC_BUDGETDOCTYPE_GUID"].Value = SRC_BUDGETDOCTYPE_GUID;

                cmdSQL.ExecuteNonQuery();
                System.Int32 iRes = (System.Int32)cmdSQL.Parameters["@RETURN_VALUE"].Value;

                strErr += ((cmdSQL.Parameters["@ERROR_MES"].Value == System.DBNull.Value) ? "" : (System.String)cmdSQL.Parameters["@ERROR_MES"].Value);
                bRet = (iRes == 0);
            }
            catch (System.Exception f)
            {
                strErr += ("�� ������� ��������� �������� ��������� ����� ����������. ����� ������: " + f.Message);
            }
            finally
            {
                DBConnection.Close();
            }
            return bRet;
        }

        public static System.Boolean RemoveBudgetDocTypeTransformCollection(System.Guid SRC_BUDGETDOCTYPE_GUID, System.Data.DataTable addedItems,
            UniXP.Common.CProfile objProfile, System.Data.SqlClient.SqlConnection DBConnection, System.Data.SqlClient.SqlCommand cmdSQL, ref System.String strErr)
        {
            System.Boolean bRet = false;

            try
            {
                if (DBConnection == null)
                {
                    strErr += ("�� ������� �������� ���������� � ����� ������.");
                    return bRet;
                }
                bRet = true;
                cmdSQL.CommandType = System.Data.CommandType.StoredProcedure;
                cmdSQL.Parameters.Clear();
                cmdSQL.CommandText = System.String.Format("[{0}].[dbo].[usp_RemoveBudgetDocTypeTransformCollection]", objProfile.GetOptionsDllDBName());
                cmdSQL.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ((System.Byte)(0)), ((System.Byte)(0)), "", System.Data.DataRowVersion.Current, null));
                cmdSQL.Parameters.Add(new System.Data.SqlClient.SqlParameter("@SRC_BUDGETDOCTYPE_GUID", System.Data.SqlDbType.UniqueIdentifier));
                cmdSQL.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ERROR_NUM", System.Data.SqlDbType.Int, 8, System.Data.ParameterDirection.Output, false, ((System.Byte)(0)), ((System.Byte)(0)), "", System.Data.DataRowVersion.Current, null));
                cmdSQL.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ERROR_MES", System.Data.SqlDbType.NVarChar, 4000));
                cmdSQL.Parameters["@ERROR_MES"].Direction = System.Data.ParameterDirection.Output;
                cmdSQL.Parameters["@SRC_BUDGETDOCTYPE_GUID"].Value = SRC_BUDGETDOCTYPE_GUID;
                cmdSQL.ExecuteNonQuery();

                System.Int32 iRes = (System.Int32)cmdSQL.Parameters["@RETURN_VALUE"].Value;
                bRet = (iRes == 0);
                if (bRet == false)
                {
                    strErr += ((cmdSQL.Parameters["@ERROR_MES"].Value == System.DBNull.Value) ? "" : (System.String)cmdSQL.Parameters["@ERROR_MES"].Value);
                }

                bRet = (iRes == 0);
            }
            catch (System.Exception f)
            {
                strErr += ("�� ������� ������� �������� ��������� ����� ����������. ����� ������: " + f.Message);
            }
            finally
            {
            }
            return bRet;
        }

        public static System.Boolean SaveBudgetDocTypeTransformCollection2(System.Guid SRC_BUDGETDOCTYPE_GUID, System.Data.DataTable addedItems,
            UniXP.Common.CProfile objProfile, System.Data.SqlClient.SqlConnection DBConnection, System.Data.SqlClient.SqlCommand cmdSQL, ref System.String strErr)
        {
            System.Boolean bRet = false;

            try
            {
                if (DBConnection == null)
                {
                    strErr += ("�� ������� �������� ���������� � ����� ������.");
                    return bRet;
                }
                bRet = true;
                cmdSQL.CommandType = System.Data.CommandType.StoredProcedure;
                cmdSQL.Parameters.Clear();
                cmdSQL.CommandText = System.String.Format("[{0}].[dbo].[usp_AssignBudgetDocTypeTransformItem]", objProfile.GetOptionsDllDBName());
                cmdSQL.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ((System.Byte)(0)), ((System.Byte)(0)), "", System.Data.DataRowVersion.Current, null));
                cmdSQL.Parameters.Add(new System.Data.SqlClient.SqlParameter("@SRC_BUDGETDOCTYPE_GUID", System.Data.SqlDbType.UniqueIdentifier));
                cmdSQL.Parameters.Add(new System.Data.SqlClient.SqlParameter("@SRC_BUDGETDOCSTATE_GUID", System.Data.SqlDbType.UniqueIdentifier));
                cmdSQL.Parameters.Add(new System.Data.SqlClient.SqlParameter("@DST_BUDGETDOCTYPE_GUID", System.Data.SqlDbType.UniqueIdentifier));
                cmdSQL.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ERROR_NUM", System.Data.SqlDbType.Int, 8, System.Data.ParameterDirection.Output, false, ((System.Byte)(0)), ((System.Byte)(0)), "", System.Data.DataRowVersion.Current, null));
                cmdSQL.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ERROR_MES", System.Data.SqlDbType.NVarChar, 4000));
                cmdSQL.Parameters["@ERROR_MES"].Direction = System.Data.ParameterDirection.Output;
                cmdSQL.Parameters["@SRC_BUDGETDOCTYPE_GUID"].Value = SRC_BUDGETDOCTYPE_GUID;
                System.Int32 iRes = 0;

                foreach (System.Data.DataRow objRow in addedItems.Rows)
                {
                    cmdSQL.Parameters["@SRC_BUDGETDOCSTATE_GUID"].Value = objRow["SRC_BUDGETDOCSTATE_GUID"];
                    cmdSQL.Parameters["@DST_BUDGETDOCTYPE_GUID"].Value = objRow["DST_BUDGETDOCTYPE_GUID"];

                    cmdSQL.ExecuteNonQuery();
                    iRes = (System.Int32)cmdSQL.Parameters["@RETURN_VALUE"].Value;
                    bRet = (iRes == 0);
                    if (bRet == false) 
                    {
                        strErr += ((cmdSQL.Parameters["@ERROR_MES"].Value == System.DBNull.Value) ? "" : (System.String)cmdSQL.Parameters["@ERROR_MES"].Value);
                        break; 
                    }
                }
                

                bRet = (iRes == 0);
            }
            catch (System.Exception f)
            {
                strErr += ("�� ������� ��������� �������� ��������� ����� ����������. ����� ������: " + f.Message);
            }
            finally
            {
            }
            return bRet;
        }
        #endregion

        #region ������� ������ �� ���� ������
        public static System.Boolean RemoveObjectFromDataBase(System.Guid GUID_ID,
           UniXP.Common.CProfile objProfile, ref System.String strErr)
        {
            System.Boolean bRet = false;

            System.Data.SqlClient.SqlConnection DBConnection = null;
            System.Data.SqlClient.SqlCommand cmd = null;
            System.Data.SqlClient.SqlTransaction DBTransaction = null;

            try
            {
                DBConnection = objProfile.GetDBSource();
                if (DBConnection == null)
                {
                    strErr += ("�� ������� �������� ���������� � ����� ������.");
                    return bRet;
                }
                DBTransaction = DBConnection.BeginTransaction();
                cmd = new System.Data.SqlClient.SqlCommand();
                cmd.Connection = DBConnection;
                cmd.Transaction = DBTransaction;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = System.String.Format("[{0}].[dbo].[usp_DeleteBudgetDocType]", objProfile.GetOptionsDllDBName());
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ((System.Byte)(0)), ((System.Byte)(0)), "", System.Data.DataRowVersion.Current, null));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@GUID_ID", System.Data.SqlDbType.UniqueIdentifier));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ERROR_NUM", System.Data.SqlDbType.Int, 8, System.Data.ParameterDirection.Output, false, ((System.Byte)(0)), ((System.Byte)(0)), "", System.Data.DataRowVersion.Current, null));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ERROR_MES", System.Data.SqlDbType.NVarChar, 4000));
                cmd.Parameters["@ERROR_MES"].Direction = System.Data.ParameterDirection.Output;
                cmd.Parameters["@GUID_ID"].Value = GUID_ID;
                cmd.ExecuteNonQuery();
                System.Int32 iRes = (System.Int32)cmd.Parameters["@RETURN_VALUE"].Value;
                if (iRes == 0)
                {
                    // ������������ ����������
                    DBTransaction.Commit();
                }
                else
                {
                    DBTransaction.Rollback();
                    strErr += ((cmd.Parameters["@ERROR_MES"].Value == System.DBNull.Value) ? "" : (System.String)cmd.Parameters["@ERROR_MES"].Value);
                }

                cmd.Dispose();
                bRet = (iRes == 0);
            }
            catch (System.Exception f)
            {
                DBTransaction.Rollback();

                strErr += ("�� ������� ������� ������ '��� ���������� ���������'. ����� ������: " + f.Message);
            }
            finally
            {
                DBConnection.Close();
            }
            return bRet;
        }
        #endregion

        #region ������ ��������
        public static List<CBudgetDocType> GetBudgetDocTypeList(UniXP.Common.CProfile objProfile,
            System.Data.SqlClient.SqlCommand cmdSQL, ref System.String strErr)
        {
            List<CBudgetDocType> objList = new List<CBudgetDocType>();
            System.Data.SqlClient.SqlConnection DBConnection = null;
            System.Data.SqlClient.SqlCommand cmd = null;
            try
            {
                if (cmdSQL == null)
                {
                    DBConnection = objProfile.GetDBSource();
                    if (DBConnection == null)
                    {
                        strErr += ("\n�� ������� �������� ���������� � ����� ������.");
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

                cmd.CommandText = System.String.Format("[{0}].[dbo].[usp_GetBudgetDocType]", objProfile.GetOptionsDllDBName());
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ((System.Byte)(0)), ((System.Byte)(0)), "", System.Data.DataRowVersion.Current, null));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ERROR_NUM", System.Data.SqlDbType.Int, 8, System.Data.ParameterDirection.Output, false, ((System.Byte)(0)), ((System.Byte)(0)), "", System.Data.DataRowVersion.Current, null));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ERROR_MES", System.Data.SqlDbType.NVarChar, 4000));
                cmd.Parameters["@ERROR_MES"].Direction = System.Data.ParameterDirection.Output;
                System.Data.SqlClient.SqlDataReader rs = cmd.ExecuteReader();
                if (rs.HasRows)
                {
                    while (rs.Read())
                    {
                        objList.Add(
                            new CBudgetDocType()
                            {
                                uuidID = (System.Guid)rs["GUID_ID"],
                                Name = System.Convert.ToString(rs["BUDGETDOCTYPE_NAME"]),
                                ControlClassName = System.Convert.ToString(rs["CLASS_NAME"]), 
                                Priority  = System.Convert.ToInt32(rs["PRIORITY"]), 
                                NeedDivision = System.Convert.ToBoolean(rs["NEEDDISION"]), 
                                IsCanChangeCategoryInBudgetDoc = System.Convert.ToBoolean(rs["BUDGETDOCTYPE_CANCHANGECATEGORYINBUDGETDOC"]),
                                IsCanTransformInOtherType = System.Convert.ToBoolean(rs["BUDGETDOCTYPE_CANTRANSFORMINOTHERTYPE"]),
                                IsActive = System.Convert.ToBoolean(rs["BUDGETDOCTYPE_ACTIVE"]),
                                BudgetDocCategory = ((rs["BUDGETDOCCATEGORY_GUID_ID"] == System.DBNull.Value) ? null : new CBudgetDocCategory() { uuidID = (System.Guid)rs["BUDGETDOCCATEGORY_GUID_ID"], Name = System.Convert.ToString(rs["BUDGETDOCCATEGORY_NAME"]) }) 
                            }
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
                strErr += ("\n�� ������� �������� ������ �������� '��� ���������� ���������'. ����� ������: " + f.Message);
            }
            return objList;
        }
        /// <summary>
        /// ��� ��������� ���� ��������� ���������� ������ �����, � ������� �� ����� ���� ������������ � ������ ��������� ���������
        /// </summary>
        /// <param name="SRC_BUDGETDOCTYPE_GUID">�� ��������� ���� ���������</param>
        /// <param name="objProfile">�������</param>
        /// <param name="cmdSQL">SQL-�������</param>
        /// <param name="strErr">��������� �� ������</param>
        /// <returns>������ ����� ���������� � ������ ��������� ���������</returns>
        public static List<CBudgetDocTypeTransformTypeItem> GetBudgetDocTransformCollection( System.Guid SRC_BUDGETDOCTYPE_GUID,
            UniXP.Common.CProfile objProfile,  System.Data.SqlClient.SqlCommand cmdSQL, ref System.String strErr)
        {
            List<CBudgetDocTypeTransformTypeItem> objList = new List<CBudgetDocTypeTransformTypeItem>();
            System.Data.SqlClient.SqlConnection DBConnection = null;
            System.Data.SqlClient.SqlCommand cmd = null;
            try
            {
                if (cmdSQL == null)
                {
                    DBConnection = objProfile.GetDBSource();
                    if (DBConnection == null)
                    {
                        strErr += ("\n�� ������� �������� ���������� � ����� ������.");
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

                cmd.CommandText = System.String.Format("[{0}].[dbo].[usp_GetBudgetDocTypeTransform]", objProfile.GetOptionsDllDBName());
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ((System.Byte)(0)), ((System.Byte)(0)), "", System.Data.DataRowVersion.Current, null));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@SRC_BUDGETDOCTYPE_GUID", System.Data.SqlDbType.UniqueIdentifier));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ERROR_NUM", System.Data.SqlDbType.Int, 8, System.Data.ParameterDirection.Output, false, ((System.Byte)(0)), ((System.Byte)(0)), "", System.Data.DataRowVersion.Current, null));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ERROR_MES", System.Data.SqlDbType.NVarChar, 4000));
                cmd.Parameters["@ERROR_MES"].Direction = System.Data.ParameterDirection.Output;
                cmd.Parameters["@SRC_BUDGETDOCTYPE_GUID"].Value = SRC_BUDGETDOCTYPE_GUID;
                System.Data.SqlClient.SqlDataReader rs = cmd.ExecuteReader();
                if (rs.HasRows)
                {
                    while (rs.Read())
                    {
                        objList.Add(
                            new CBudgetDocTypeTransformTypeItem()
                            {
                                CurrentBudgetDocState = new CBudgetDocState() 
                                { 
                                    uuidID = (System.Guid)rs["SRC_BUDGETDOCSTATE_GUID"], 
                                    Name = System.Convert.ToString(rs["BUDGETDOCSTATE_NAME"]), 
                                    OrderNum = System.Convert.ToInt32(rs["BUDGETDOCSTATE_ID"]) 
                                }, 


                                NewBudgetDocType = new CBudgetDocType()
                                {
                                    uuidID = (System.Guid)rs["DST_BUDGETDOCTYPE_GUID"],
                                    Name = System.Convert.ToString(rs["DstBUDGETDOCTYPE_NAME"]),
                                    ControlClassName = System.Convert.ToString(rs["DstCLASS_NAME"]),
                                    Priority = System.Convert.ToInt32(rs["DstPRIORITY"]),
                                    NeedDivision = System.Convert.ToBoolean(rs["DstNEEDDISION"]),
                                    IsCanChangeCategoryInBudgetDoc = System.Convert.ToBoolean(rs["DstBUDGETDOCTYPE_CANCHANGECATEGORYINBUDGETDOC"]),
                                    IsCanTransformInOtherType = System.Convert.ToBoolean(rs["DstBUDGETDOCTYPE_CANTRANSFORMINOTHERTYPE"]),
                                    IsActive = System.Convert.ToBoolean(rs["DstBUDGETDOCTYPE_ACTIVE"]),
                                    BudgetDocCategory = ((rs["DstBUDGETDOCCATEGORY_GUID_ID"] == System.DBNull.Value) ? null : new CBudgetDocCategory() { uuidID = (System.Guid)rs["DstBUDGETDOCCATEGORY_GUID_ID"], Name = System.Convert.ToString(rs["DstBUDGETDOCCATEGORY_NAME"]) }) 
                                }

                            }
                            );
                    }
                }
                rs.Close();
                rs.Dispose();
                // �������� ������ ��������� ����� �������
                foreach (CBudgetDocTypeTransformTypeItem objTransformTypeItem in objList)
                {
                    objTransformTypeItem.NewBudgetDocType.ValidBudgetTypeList = CBudgetTypeDataBaseModel.GetValidBudgetTypeList(objTransformTypeItem.NewBudgetDocType.uuidID, objProfile,
                        cmd, ref strErr);
                }

                if (cmdSQL == null)
                {
                    cmd.Dispose();
                    DBConnection.Close();
                }
            }
            catch (System.Exception f)
            {
                strErr += ("\n�� ������� �������� �������� �����, � ������� ����� ���� ������������ �������� ��� ���������� ���������. ����� ������: " + f.Message);
            }
            return objList;
        }
        #endregion

        #region ��������� ���� � ���������� ���������
        /// <summary>
        /// ��������� �������� ���� ��������� �� ���������
        /// </summary>
        /// <param name="BUDGETDOC_GUID_ID">�� ���������� ���������</param>
        /// <param name="BUDGETDOC_MONEY">����� ��������� � ������ ���������</param>
        /// <param name="BUDGETDOC_MONEYAGREE">����� ��������� � ������ �������</param>
        /// <param name="BUDGETDOCTYPE_GUID_ID">�� ������ ���� ���������</param>
        /// <param name="BUDGETITEM_GUID_ID">�� ������ �������</param>
        /// <param name="ACCOUNTPLAN_GUID">�� ����� ������</param>
        /// <param name="USERS_ID">�� ������������</param>
        /// <param name="BUDGETPROJECT_GUID">�� �������</param>
        /// <param name="objBudgetItemList">������ ������ � ��������� ���������</param>
        /// <param name="objProfile">�������</param>
        /// <param name="iRetCode">��� �������� ���������</param>
        /// <param name="strErr">��������� �� ������</param>
        /// <returns>true - ������� ���������� ��������; false - ������</returns>
        public static System.Boolean ChangeDocTypeInBudgetDoc( System.Guid BUDGETDOC_GUID_ID,  System.Double BUDGETDOC_MONEY, 
            System.Double BUDGETDOC_MONEYAGREE, System.Guid BUDGETDOCTYPE_GUID_ID, System.Guid BUDGETITEM_GUID_ID,
            System.Guid ACCOUNTPLAN_GUID, System.Int32 USERS_ID, System.Guid BUDGETPROJECT_GUID, List<CBudgetItem> objBudgetItemList,
            UniXP.Common.CProfile objProfile, ref System.Int32 iRetCode, ref System.String strErr)
        {
            System.Boolean bRet = false;

            System.Data.SqlClient.SqlConnection DBConnection = null;
            System.Data.SqlClient.SqlCommand cmd = null;
            System.Data.SqlClient.SqlTransaction DBTransaction = null;

            try
            {
                DBConnection = objProfile.GetDBSource();
                if (DBConnection == null)
                {
                    strErr += ("�� ������� �������� ���������� � ����� ������.");
                    return bRet;
                }
                DBTransaction = DBConnection.BeginTransaction();
                cmd = new System.Data.SqlClient.SqlCommand();
                cmd.Connection = DBConnection;
                cmd.Transaction = DBTransaction;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                if (CBudgetDoc.SaveBudgetItemList(BUDGETDOC_GUID_ID, objBudgetItemList, cmd, objProfile, ref iRetCode, ref strErr) == true)
                {
                    cmd.CommandText = System.String.Format("[{0}].[dbo].[sp_EditBudgetDocTypeInBudgetDoc]", objProfile.GetOptionsDllDBName());
                    cmd.Parameters.Clear();
                    cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ((System.Byte)(0)), ((System.Byte)(0)), "", System.Data.DataRowVersion.Current, null));
                    cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@BUDGETDOC_GUID_ID", System.Data.SqlDbType.UniqueIdentifier));
                    cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@BUDGETDOC_MONEY", System.Data.SqlDbType.Money, 8));
                    cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@BUDGETDOC_MONEYAGREE", System.Data.SqlDbType.Money, 8));
                    cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@BUDGETDOCTYPE_GUID_ID", System.Data.SqlDbType.UniqueIdentifier));
                    cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@BUDGETITEM_GUID_ID", System.Data.SqlDbType.UniqueIdentifier));
                    cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@USERS_ID", System.Data.SqlDbType.Int));
                    cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@BUDGETPROJECT_GUID", System.Data.SqlDbType.UniqueIdentifier));

                    cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ERROR_NUM", System.Data.SqlDbType.Int, 8, System.Data.ParameterDirection.Output, false, ((System.Byte)(0)), ((System.Byte)(0)), "", System.Data.DataRowVersion.Current, null));
                    cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ERROR_MES", System.Data.SqlDbType.NVarChar, 4000));
                    cmd.Parameters["@ERROR_MES"].Direction = System.Data.ParameterDirection.Output;

                    cmd.Parameters["@BUDGETDOC_GUID_ID"].Value = BUDGETDOC_GUID_ID;
                    cmd.Parameters["@BUDGETDOC_MONEY"].Value = BUDGETDOC_MONEY;
                    cmd.Parameters["@BUDGETDOC_MONEYAGREE"].Value = BUDGETDOC_MONEYAGREE;
                    cmd.Parameters["@BUDGETDOCTYPE_GUID_ID"].Value = BUDGETDOCTYPE_GUID_ID;
                    cmd.Parameters["@BUDGETITEM_GUID_ID"].Value = BUDGETITEM_GUID_ID;
                    cmd.Parameters["@USERS_ID"].Value = USERS_ID;
                    cmd.Parameters["@BUDGETPROJECT_GUID"].Value = BUDGETPROJECT_GUID;

                    if (ACCOUNTPLAN_GUID.CompareTo(System.Guid.Empty) != 0)
                    {
                        cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ACCOUNTPLAN_GUID", System.Data.SqlDbType.UniqueIdentifier));
                        cmd.Parameters["@ACCOUNTPLAN_GUID"].Value = ACCOUNTPLAN_GUID;
                    }

                    cmd.ExecuteNonQuery();
                    System.Int32 iRes = (System.Int32)cmd.Parameters["@RETURN_VALUE"].Value;
                    iRetCode = iRes;

                    if (iRes == 0)
                    {
                        // ������������ ����������
                        DBTransaction.Commit();
                        bRet = true;
                    }
                    else
                    {
                        DBTransaction.Rollback();
                        strErr += ((cmd.Parameters["@ERROR_MES"].Value == System.DBNull.Value) ? "" : (System.String)cmd.Parameters["@ERROR_MES"].Value);
                    }
                }
                cmd.Dispose();
            }
            catch (System.Exception f)
            {
                DBTransaction.Rollback();

                strErr += ("�� ������� �������� ��� ���������� ���������. ����� ������: " + f.Message);
            }
            finally
            {
                DBConnection.Close();
            }
            return bRet;
        }
        #endregion

    }
}
