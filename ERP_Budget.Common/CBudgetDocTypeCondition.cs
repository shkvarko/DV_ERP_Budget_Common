using System;
using System.Collections.Generic;
using System.Text;

namespace ERP_Budget.Common
{
    /// <summary>
    /// ����� "������� ������ ���� ���������"
    /// </summary>
    public class CBudgetDocTypeCondition : IBaseListItem
    {
        #region ����������, ��������, ��������� 
        /// <summary>
        /// ������ ���������� �� ����������
        /// </summary>
        private System.Collections.Generic.List<CBudgetDocTypeVariable> m_BudgetDocTypeVariableList;
        /// <summary>
        /// ������ ���������� �� ����������
        /// </summary>
        public System.Collections.Generic.List<CBudgetDocTypeVariable> BudgetDocTypeVariableList
        {
            get { return m_BudgetDocTypeVariableList; }
            set { m_BudgetDocTypeVariableList = value; }
        }
        /// <summary>
        /// ��� ���������
        /// </summary>
        private CBudgetDocType m_objBudgetDocType;
        /// <summary>
        /// ��� ���������
        /// </summary>
        public CBudgetDocType BudgetDocType
        {
            get { return m_objBudgetDocType; }
            set { m_objBudgetDocType = value; }
        }
        #endregion

        #region ������������ 
        public CBudgetDocTypeCondition()
        {
            this.m_uuidID = System.Guid.Empty;
            this.m_strName = "";
            this.m_BudgetDocTypeVariableList = new List<CBudgetDocTypeVariable>();
            this.m_objBudgetDocType = null;
        }

        public CBudgetDocTypeCondition( System.Guid uuidID, System.String strName, CBudgetDocType objBudgetDocType )
        {
            this.m_uuidID = uuidID;
            this.m_strName = strName;
            this.m_BudgetDocTypeVariableList = new List<CBudgetDocTypeVariable>();
            this.m_objBudgetDocType = objBudgetDocType;
        }

        public CBudgetDocTypeCondition( System.Guid uuidID, System.String strName, 
            List<CBudgetDocTypeVariable> objBudgetDocTypeVariableList,  CBudgetDocType objBudgetDocType )
        {
            this.m_uuidID = uuidID;
            this.m_strName = strName;
            this.m_BudgetDocTypeVariableList = new List<CBudgetDocTypeVariable>();
            foreach( CBudgetDocTypeVariable objBudgetDocTypeVariable in objBudgetDocTypeVariableList )
            {
                this.m_BudgetDocTypeVariableList.Add( new CBudgetDocTypeVariable( objBudgetDocTypeVariable.uuidID, 
                    objBudgetDocTypeVariable.Name, objBudgetDocTypeVariable.EditClassName, 
                    objBudgetDocTypeVariable.PatternSrc, objBudgetDocTypeVariable.Pattern,
                    objBudgetDocTypeVariable.DataTypeName, objBudgetDocTypeVariable.m_strValue ) );
            }
            this.m_objBudgetDocType = objBudgetDocType;
        }

        #endregion

        #region ������ ������� 
        /// <summary>
        /// ���������� ������ ������� ������ ���� ���������� ���������
        /// </summary>
        /// <param name="objProfile">�������</param>
        /// <returns>������ ������� ������ ���� ���������� ���������</returns>
        public static System.Collections.Generic.List<CBudgetDocTypeCondition> GetDocTypeConditionList(
            UniXP.Common.CProfile objProfile )
        {
            System.Collections.Generic.List<CBudgetDocTypeCondition> objList = new List<CBudgetDocTypeCondition>();
            try
            {
                // ���������� �������� ���������� � ��
                System.Data.SqlClient.SqlConnection DBConnection = objProfile.GetDBSource();
                if( DBConnection == null ) { return objList; }

                // ����������� ������ �������
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand() { Connection = DBConnection, CommandType = System.Data.CommandType.StoredProcedure, CommandText = System.String.Format("[{0}].[dbo].[sp_GetDocTypeCondition]", objProfile.GetOptionsDllDBName()) };
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ( ( System.Byte )( 0 ) ), ( ( System.Byte )( 0 ) ), "", System.Data.DataRowVersion.Current, null ) );
                System.Data.SqlClient.SqlDataReader rs = cmd.ExecuteReader();
                if( rs.HasRows )
                {
                    while( rs.Read() )
                    {
                        objList.Add(new CBudgetDocTypeCondition() 
                        { 
                            uuidID = (System.Guid)rs["GUID_ID"], Name = System.Convert.ToString( rs["DOCTYPECONDITION_NAME"] ),
                            BudgetDocType = new CBudgetDocType()
                            {
                                uuidID = (System.Guid)rs["BUDGETDOCTYPE_GUID_ID"],
                                Name = System.Convert.ToString(rs["BUDGETDOCTYPE_NAME"]),
                                ControlClassName = System.Convert.ToString(rs["CLASS_NAME"]),
                                Priority = System.Convert.ToInt32(rs["PRIORITY"]),
                                NeedDivision = System.Convert.ToBoolean(rs["NEEDDISION"]),
                                IsCanChangeCategoryInBudgetDoc = System.Convert.ToBoolean(rs["BUDGETDOCTYPE_CANCHANGECATEGORYINBUDGETDOC"]),
                                IsCanTransformInOtherType = System.Convert.ToBoolean(rs["BUDGETDOCTYPE_CANTRANSFORMINOTHERTYPE"]),
                                IsActive = System.Convert.ToBoolean(rs["BUDGETDOCTYPE_ACTIVE"]),
                                BudgetDocCategory = ((rs["BUDGETDOCCATEGORY_GUID_ID"] == System.DBNull.Value) ? null : new CBudgetDocCategory() { uuidID = (System.Guid)rs["BUDGETDOCCATEGORY_GUID_ID"], Name = System.Convert.ToString(rs["BUDGETDOCCATEGORY_NAME"]) })
                            }
                        }
                                                        
                        );
                    }
                }
                rs.Close();

                // ������ ����������� ��������� ������� �������
                foreach( CBudgetDocTypeCondition objBudgetDocTypeCondition in objList )
                {
                    if( objBudgetDocTypeCondition.LoadDocTypeConditionDeclrn( cmd, objProfile ) == false )
                    {
                        objList.Clear();
                        break;
                    }
                    // ������ ����������� ������ ��������� ���� ���������� ��������� 
                    objBudgetDocTypeCondition.BudgetDocType.BudgetDocTypeDraw = CBudgetDocTypeDraw.GetBudgetDocTypeDraw( objProfile, objBudgetDocTypeCondition.BudgetDocType, cmd );
                }

                // ������ ����� ��������, �� ������� ����� ���������� �������� ��������� ����
                System.String strErr = "";
                foreach (CBudgetDocTypeCondition objBudgetDocTypeCondition in objList)
                {
                    objBudgetDocTypeCondition.BudgetDocType.ValidBudgetTypeList = CBudgetTypeDataBaseModel.GetValidBudgetTypeList(objBudgetDocTypeCondition.BudgetDocType.uuidID, objProfile, null, ref strErr);
                }

                cmd.Dispose();
                DBConnection.Close();
            }
            catch( System.Exception f )
            {
                DevExpress.XtraEditors.XtraMessageBox.Show( 
                "�� ������� �������� ������ ������� ������ ��������.\n\n����� ������: " + f.Message, "��������",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
            }
            return objList;
        }
        /// <summary>
        /// ��������� ������ ���������� ��� ��������� �������
        /// </summary>
        /// <param name="cmd">SQL-�������</param>
        /// <param name="objProfile">�������</param>
        /// <returns>true - �������� ����������; false - ������</returns>
        private System.Boolean LoadDocTypeConditionDeclrn( System.Data.SqlClient.SqlCommand cmd, 
            UniXP.Common.CProfile objProfile )
        {
            System.Boolean bRet = false;
            if( cmd == null ) { return bRet; }
            try
            {
                cmd.Parameters.Clear();
                cmd.CommandText = System.String.Format( "[{0}].[dbo].[sp_GetDocTypeConditionDeclrn]", objProfile.GetOptionsDllDBName() );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ( ( System.Byte )( 0 ) ), ( ( System.Byte )( 0 ) ), "", System.Data.DataRowVersion.Current, null ) );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@GUID_ID", System.Data.SqlDbType.UniqueIdentifier ) );
                cmd.Parameters[ "@GUID_ID" ].Value = this.m_uuidID;
                System.Data.SqlClient.SqlDataReader rs = cmd.ExecuteReader();
                if( rs.HasRows )
                {
                    while( rs.Read() )
                    {
                        this.m_BudgetDocTypeVariableList.Add(new CBudgetDocTypeVariable()
                            {
                                uuidID = (System.Guid)rs["DOCTYPEVARIABLE_GUID_ID"],
                                Name = System.Convert.ToString(rs["DOCTYPEVARIABLE_NAME"]),
                                EditClassName = System.Convert.ToString(rs["DOCTYPEVARIABLE_CLASSNAME"]),
                                PatternSrc = System.Convert.ToString(rs["DOCTYPEVARIABLE_PATTERNSRC"]),
                                Pattern = System.Convert.ToString(rs["DOCTYPEVARIABLE_PATTERN"]),
                                DataTypeName = System.Convert.ToString(rs["DATATYPE_NAME"]),
                                m_strValue = System.Convert.ToString(rs["CONDITION"])
                            }
                            );
                    }
                    bRet = true;
                }
                rs.Close();
                rs.Dispose();
            }
            catch( System.Exception f )
            {
                DevExpress.XtraEditors.XtraMessageBox.Show( 
                "�� ������� �������� ������ ���������� ��� �������:" + this.m_strName + 
                "\n\n����� ������: " + f.Message, "��������",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
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
            try
            {
                // ���������� �������� ���������� � ��
                System.Data.SqlClient.SqlConnection DBConnection = objProfile.GetDBSource();
                if( DBConnection == null ) { return bRet; }

                // ����������� ������ �������
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
                cmd.Connection = DBConnection;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = System.String.Format( "[{0}].[dbo].[sp_GetDocTypeCondition]", objProfile.GetOptionsDllDBName() );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ( ( System.Byte )( 0 ) ), ( ( System.Byte )( 0 ) ), "", System.Data.DataRowVersion.Current, null ) );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@GUID_ID", System.Data.SqlDbType.UniqueIdentifier ) );
                cmd.Parameters[ "@GUID_ID" ].Value = uuidID;
                System.Data.SqlClient.SqlDataReader rs = cmd.ExecuteReader();
                if( rs.HasRows )
                {
                    rs.Read();
                    this.m_uuidID = rs.GetGuid( 0 );
                    this.m_strName = rs.GetString( 1 );
                    this.m_objBudgetDocType = new CBudgetDocType( rs.GetGuid( 2 ), rs.GetString( 3 ) );
                }
                rs.Close();
                // ������ ����������� ��������� �������
                bRet = this.LoadDocTypeConditionDeclrn( cmd, objProfile );

                cmd.Dispose();
                DBConnection.Close();
            }
            catch( System.Exception f )
            {
                DevExpress.XtraEditors.XtraMessageBox.Show( 
                "�� ������� ������������������� ������� ������ ��������.\n��: " + this.m_uuidID + 
                "\n\n����� ������: " + f.Message, "��������",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
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
            System.Data.SqlClient.SqlTransaction DBTransaction = DBConnection.BeginTransaction();

            try
            {
                // ���������� � �� ��������, ����������� ������� �� �������� ������
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
                cmd.Connection = DBConnection;
                cmd.Transaction = DBTransaction;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = System.String.Format( "[{0}].[dbo].[sp_DeleteDocTypeCondition]", objProfile.GetOptionsDllDBName() );
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
                    DevExpress.XtraEditors.XtraMessageBox.Show(  "������ ���������� ������� \n�� �������� ������� ������ ���� ���������� ���������.\n������ : \n" + ( System.String )cmd.Parameters[ "@ERROR_MESSAGE" ].Value, "��������",
                        System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
                }
                cmd.Dispose();
            }
            catch( System.Exception f )
            {
                DBTransaction.Rollback();
                DevExpress.XtraEditors.XtraMessageBox.Show( 
                "������ ���������� ������� \n�� �������� ������� ������ ���� ���������� ���������.\n" + f.Message, "��������",
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
                    DevExpress.XtraEditors.XtraMessageBox.Show(  "��� ������� ����� ����������!", "��������",
                        System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning );
                    return bRet;
                }
                // ��� ���������� ��������� ������ ���� ������ 
                if( this.m_objBudgetDocType == null )
                {
                    DevExpress.XtraEditors.XtraMessageBox.Show(  "���������� ������� ��� ���������� ���������!\n������: " + this.m_strName, "��������",
                        System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning );
                    return bRet;
                }
                // �������� �� ������ �� ���������
                if( this.m_BudgetDocTypeVariableList.Count == 0 )
                {
                    DevExpress.XtraEditors.XtraMessageBox.Show(  "� ������� ����������� ������ ����������!", "��������",
                        System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning );
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
        /// ��������� � �� ��������� ������� ������ ���� ���������� ���������
        /// </summary>
        /// <param name="cmd">SQL-�������</param>
        /// <param name="objProfile">�������</param>
        /// <returns>true - ������� ����������; false - ������</returns>
        private System.Boolean SaveBudgetDocTypeConditionDeclrn( System.Data.SqlClient.SqlCommand cmd, 
            UniXP.Common.CProfile objProfile )
        {
            System.Boolean bRet = false;
            // ������ �� ������ ���� ������
            if( this.m_BudgetDocTypeVariableList.Count == 0 )
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(  "������ ���������� �� ������ ���� ������.", "��������",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
                return bRet;
            }

            if( cmd == null ) { return bRet; }

            try
            {
                // ������ ������� ��������� �������, � ����� ��������� ������
                cmd.Parameters.Clear();
                cmd.CommandText = System.String.Format( "[{0}].[dbo].[sp_DeleteDocTypeCondition]", objProfile.GetOptionsDllDBName() );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ( ( System.Byte )( 0 ) ), ( ( System.Byte )( 0 ) ), "", System.Data.DataRowVersion.Current, null ) );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@GUID_ID", System.Data.SqlDbType.UniqueIdentifier ) );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@ONLY_DECLRN", System.Data.SqlDbType.Bit ) );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@ERROR_NUMBER", System.Data.SqlDbType.Int, 8, System.Data.ParameterDirection.Output, false, ( ( System.Byte )( 0 ) ), ( ( System.Byte )( 0 ) ), "", System.Data.DataRowVersion.Current, null ) );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@ERROR_MESSAGE", System.Data.DbType.String ) );
                cmd.Parameters[ "@ERROR_MESSAGE" ].Direction = System.Data.ParameterDirection.Output;
                cmd.Parameters[ "@GUID_ID" ].Value = this.m_uuidID;
                cmd.Parameters[ "@ONLY_DECLRN" ].Value = 1;
                cmd.ExecuteNonQuery();
                System.Int32 iRet = ( System.Int32 )cmd.Parameters[ "@RETURN_VALUE" ].Value;
                if( iRet == 0 )
                {
                    cmd.Parameters.Clear();
                    cmd.CommandText = System.String.Format( "[{0}].[dbo].[sp_AddDocTypeConditionDeclrn]", objProfile.GetOptionsDllDBName() );
                    cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ( ( System.Byte )( 0 ) ), ( ( System.Byte )( 0 ) ), "", System.Data.DataRowVersion.Current, null ) );
                    cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@DOCTYPECONDITION_GUID_ID", System.Data.SqlDbType.UniqueIdentifier ) );
                    cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@DOCTYPEVARIABLE_GUID_ID", System.Data.SqlDbType.UniqueIdentifier ) );
                    cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@CONDITION", System.Data.SqlDbType.Text ) );
                    iRet = -1;
                    foreach( CBudgetDocTypeVariable objBudgetDocTypeVariable in this.m_BudgetDocTypeVariableList )
                    {
                        if( objBudgetDocTypeVariable.m_strValue == "" ) { continue; }
                        cmd.Parameters[ "@CONDITION" ].Value = objBudgetDocTypeVariable.m_strValue;
                        cmd.Parameters[ "@DOCTYPECONDITION_GUID_ID" ].Value = this.m_uuidID;
                        cmd.Parameters[ "@DOCTYPEVARIABLE_GUID_ID" ].Value = objBudgetDocTypeVariable.m_uuidID;
                        cmd.ExecuteNonQuery();
                        iRet = ( System.Int32 )cmd.Parameters[ "@RETURN_VALUE" ].Value;
                        if( iRet != 0 ) { break; }
                    }
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
                                DevExpress.XtraEditors.XtraMessageBox.Show(  "���������� � �������� ��������������� �� �������:\n" + this.uuidID.ToString(), "��������",
                                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning );
                                break;
                            }
                            case 2:
                            {
                                DevExpress.XtraEditors.XtraMessageBox.Show(  "������� ������ ���� ��������� �� �������.\n�� �������: '" + 
                                this.uuidID.ToString() + "'", "��������",
                                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning );
                                break;
                            }
                            default:
                            {
                                DevExpress.XtraEditors.XtraMessageBox.Show(  "������ ���������� ��������� ������� ������ ���� ���������", "������",
                                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
                                break;
                            }
                        }
                    }
                }
                else
                {
                    DevExpress.XtraEditors.XtraMessageBox.Show(  "������ ���������� ������� \n�� �������� ��������� ������� ������ ���� ���������.\n������ :\n" + 
                        ( System.String )cmd.Parameters[ "@ERROR_MESSAGE" ].Value, "��������",
                        System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
                }

                cmd.Dispose();
            }
            catch( System.Exception e )
            {
                DevExpress.XtraEditors.XtraMessageBox.Show( 
                "������ ���������� ��������� ������� ������ ���� ���������.\n����� ������:\n" + e.Message, "��������",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
            }
			finally // ������� ���������� �������
            {
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
            System.Boolean bRet = false;
            // ��������� ��������
            if( this.IsValidateProperties() == false ) { return bRet; }

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
                cmd.CommandText = System.String.Format( "[{0}].[dbo].[sp_AddDocTypeCondition]", objProfile.GetOptionsDllDBName() );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ( ( System.Byte )( 0 ) ), ( ( System.Byte )( 0 ) ), "", System.Data.DataRowVersion.Current, null ) );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@GUID_ID", System.Data.SqlDbType.UniqueIdentifier, 4, System.Data.ParameterDirection.Output, false, ( ( System.Byte )( 0 ) ), ( ( System.Byte )( 0 ) ), "", System.Data.DataRowVersion.Current, null ) );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@DOCTYPECONDITION_NAME", System.Data.DbType.String ) );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@BUDGETDOCTYPE_GUID_ID", System.Data.SqlDbType.UniqueIdentifier ) );
                if( this.m_uuidID.CompareTo( System.Guid.Empty ) != 0 )
                {
                    cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@DOCTYPECONDITION_GUID_ID", System.Data.SqlDbType.UniqueIdentifier ) );
                    cmd.Parameters[ "@DOCTYPECONDITION_GUID_ID" ].Value = this.m_uuidID;
                }
                cmd.Parameters[ "@DOCTYPECONDITION_NAME" ].Value = this.m_strName;
                cmd.Parameters[ "@BUDGETDOCTYPE_GUID_ID" ].Value = this.m_objBudgetDocType.uuidID;
                cmd.ExecuteNonQuery();
                System.Int32 iRet = ( System.Int32 )cmd.Parameters[ "@RETURN_VALUE" ].Value;
                if( iRet == 0 )
                {
                    this.m_uuidID = ( System.Guid )cmd.Parameters[ "@GUID_ID" ].Value;
                    // ��������� ��������
                    bRet = this.SaveBudgetDocTypeConditionDeclrn( cmd, objProfile );
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
                            DevExpress.XtraEditors.XtraMessageBox.Show(  "������ � �������� ������ ���������� : '" + this.m_strName + "'", "��������",
                                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning );
                            break;
                        }
                        case 2:
                        {
                            DevExpress.XtraEditors.XtraMessageBox.Show(  "��� ��������� � ��������� ��������������� �� ������ : '" + this.m_objBudgetDocType.uuidID.ToString() + "'", "��������",
                                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning );
                            break;
                        }
                        default:
                        {
                            DevExpress.XtraEditors.XtraMessageBox.Show(  "������ �������� ������� : " + this.m_strName, "������",
                                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
                            break;
                        }
                    }
                }

                cmd.Dispose();
            }
            catch( System.Exception f )
            {
                // ���������� ����������
                DBTransaction.Rollback();
                DevExpress.XtraEditors.XtraMessageBox.Show( 
                "�� ������� ������� ������ : " + this.m_strName + "\n\n����� ������: " + f.Message, "��������",
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
            if( this.IsValidateProperties() == false )
            {
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
                cmd.CommandText = System.String.Format( "[{0}].[dbo].[sp_EditDocTypeCondition]", objProfile.GetOptionsDllDBName() );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ( ( System.Byte )( 0 ) ), ( ( System.Byte )( 0 ) ), "", System.Data.DataRowVersion.Current, null ) );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@GUID_ID", System.Data.SqlDbType.UniqueIdentifier ) );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@DOCTYPECONDITION_NAME", System.Data.DbType.String ) );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@BUDGETDOCTYPE_GUID_ID", System.Data.SqlDbType.UniqueIdentifier ) );
                cmd.Parameters[ "@GUID_ID" ].Value = this.m_uuidID;
                cmd.Parameters[ "@DOCTYPECONDITION_NAME" ].Value = this.m_strName;
                cmd.Parameters[ "@BUDGETDOCTYPE_GUID_ID" ].Value = this.m_objBudgetDocType.uuidID;
                cmd.ExecuteNonQuery();
                System.Int32 iRet = ( System.Int32 )cmd.Parameters[ "@RETURN_VALUE" ].Value;
                if( iRet == 0 )
                {
                    // ��������� ��������
                    bRet = this.SaveBudgetDocTypeConditionDeclrn( cmd, objProfile );
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
                            DevExpress.XtraEditors.XtraMessageBox.Show(  "������ � �������� ������ ���������� : '" + this.m_strName + "'", "��������",
                                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning );
                            break;
                        }
                        case 2:
                        {
                            DevExpress.XtraEditors.XtraMessageBox.Show(  "������ � ��������� ��������������� �� ������ :\n'" + this.uuidID.ToString() + "'", "��������",
                                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning );
                            break;
                        }
                        case 3:
                        {
                            DevExpress.XtraEditors.XtraMessageBox.Show(  "��� ��������� � ��������� ��������������� �� ������ :\n'" + this.m_objBudgetDocType.uuidID.ToString() + "'", "��������",
                                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning );
                            break;
                        }
                        default:
                        {
                            DevExpress.XtraEditors.XtraMessageBox.Show(  "������ ��������� ������� ������� : " + this.m_strName, "������",
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
                "������ ��������� ������� ������� : " + this.m_strName + "\n" + e.Message, "��������",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
            }
			finally // ������� ���������� �������
            {
                DBConnection.Close();
            }
            return bRet;
        }
        #endregion

    }
}
