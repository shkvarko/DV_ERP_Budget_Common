using System;
using System.Collections.Generic;
using System.Text;

namespace ERP_Budget.Common
{
    /// <summary>
    /// ����� "����� ��������"
    /// </summary>
    public class CRoutePoint : IBaseListItem
    {
        #region ����������, ��������, ���������
        /// <summary>
        /// ������� ����, �������� �� ����� �������� ������� �� ��������
        /// </summary>
        private System.Boolean m_IsExit;
        /// <summary>
        /// ������� ����, �������� �� ����� �������� ������� �� ��������
        /// </summary>
        public System.Boolean IsExit
        {
            get { return m_IsExit; }
            set { m_IsExit = value; }
        }
        /// <summary>
        /// ������� ����, �������� �� ����� �������� ������� ��������
        /// </summary>
        private System.Boolean m_IsEnter;
        /// <summary>
        /// ������� ����, �������� �� ����� �������� ������� ��������
        /// </summary>
        public System.Boolean IsEnter
        {
            get { return m_IsEnter; }
            set { m_IsEnter = value; }
        }
        /// <summary>
        /// ������� ����, ���������� ����� �������� � ��������� ��� ���
        /// </summary>
        private System.Boolean m_bShowInDocument;
        /// <summary>
        /// ������� ����, ���������� ����� �������� � ��������� ��� ���
        /// </summary>
        public System.Boolean ShowInDocument
        {
            get { return m_bShowInDocument; }
            set { m_bShowInDocument = value; }
        }
        /// <summary>
        /// ��������� �������� ���������
        /// </summary>
        private CBudgetDocState m_BudgetDocStateIN;
        /// <summary>
        /// ��������� �������� ���������
        /// </summary>
        public CBudgetDocState BudgetDocStateIN
        {
            get { return m_BudgetDocStateIN; }
            set { m_BudgetDocStateIN = value; }
        }
        /// <summary>
        /// �������� �������� ���������
        /// </summary>
        private CBudgetDocState m_BudgetDocStateOUT;
        /// <summary>
        /// �������� �������� ���������
        /// </summary>
        public CBudgetDocState BudgetDocStateOUT
        {
            get { return m_BudgetDocStateOUT; }
            set { m_BudgetDocStateOUT = value; }
        }
        /// <summary>
        /// �������, ����������� �������� �� ���������� ��������� � ��������
        /// </summary>
        private CBudgetDocEvent m_BudgetDocEvent;
        /// <summary>
        /// �������, ����������� �������� �� ���������� ��������� � ��������
        /// </summary>
        public CBudgetDocEvent BudgetDocEvent
        {
            get { return m_BudgetDocEvent; }
            set { m_BudgetDocEvent = value; }
        }
        /// <summary>
        /// ������ ����� ��������
        /// </summary>
        private CRoutePointGroup m_RoutePointGroup;
        /// <summary>
        /// ������ ����� ��������
        /// </summary>
        public CRoutePointGroup RoutePointGroup
        {
            get { return m_RoutePointGroup; }
            set { m_RoutePointGroup = value; }
        }
        /// <summary>
        /// ������������, ������� ������ ����������� �������
        /// </summary>
        private CUser m_User;
        /// <summary>
        /// ������������, ������� ������ ����������� �������
        /// </summary>
        public CUser UserEvent
        {
            get { return m_User; }
            set { m_User = value; }
        }
        /// <summary>
        /// ��������� �������
        /// </summary>
        private System.Data.DataRowState m_Sate;
        /// <summary>
        /// ��������� �������
        /// </summary>
        public System.Data.DataRowState State
        {
            get { return m_Sate; }
            set { m_Sate = value; }
        }
        #endregion

        #region ������������
        public CRoutePoint()
        {
            this.m_uuidID = System.Guid.Empty;
            this.m_strName = "";
            this.m_Sate = System.Data.DataRowState.Unchanged;
            this.m_IsEnter = false;
            this.m_IsExit = false;
            this.m_bShowInDocument = false;
            this.m_BudgetDocEvent = null;
            this.m_BudgetDocStateIN = null;
            this.m_BudgetDocStateOUT = null;
            this.m_User = null;
            this.m_RoutePointGroup = null;
        }

        public CRoutePoint( System.Boolean bIsEnter, System.Boolean bIsExit, CBudgetDocEvent objBudgetDocEvent,
            CBudgetDocState objBudgetDocStateIN, CBudgetDocState objBudgetDocStateOUT )
        {
            this.m_uuidID = System.Guid.Empty;
            this.m_strName = "";
            this.m_Sate = System.Data.DataRowState.Unchanged;
            this.m_IsEnter = bIsEnter;
            this.m_IsExit = bIsExit;
            this.m_bShowInDocument = false;
            this.m_BudgetDocEvent = objBudgetDocEvent;
            this.m_BudgetDocStateIN = objBudgetDocStateIN;
            this.m_BudgetDocStateOUT = objBudgetDocStateOUT;
            this.m_User = null;
            this.m_RoutePointGroup = null;
        }

        public CRoutePoint( System.Boolean bIsEnter, System.Boolean bIsExit,
            CBudgetDocEvent objBudgetDocEvent, CBudgetDocState objBudgetDocStateIN, CBudgetDocState objBudgetDocStateOUT,
            CRoutePointGroup objRoutePointGroup, System.Boolean bShowInDocument )
        {
            this.m_uuidID = System.Guid.Empty;
            this.m_strName = "";
            this.m_Sate = System.Data.DataRowState.Unchanged;
            this.m_IsEnter = bIsEnter;
            this.m_IsExit = bIsExit;
            this.m_bShowInDocument = bShowInDocument;
            this.m_BudgetDocEvent = objBudgetDocEvent;
            this.m_BudgetDocStateIN = objBudgetDocStateIN;
            this.m_BudgetDocStateOUT = objBudgetDocStateOUT;
            this.m_User = null;
            this.m_RoutePointGroup = objRoutePointGroup;
        }

        public CRoutePoint( System.Boolean bIsEnter, System.Boolean bIsExit,
            CBudgetDocEvent objBudgetDocEvent, CBudgetDocState objBudgetDocStateOUT,
            CRoutePointGroup objRoutePointGroup, System.Boolean bShowInDocument )
        {
            this.m_uuidID = System.Guid.Empty;
            this.m_strName = "";
            this.m_Sate = System.Data.DataRowState.Unchanged;
            this.m_IsEnter = bIsEnter;
            this.m_IsExit = bIsExit;
            this.m_bShowInDocument = bShowInDocument;
            this.m_BudgetDocEvent = objBudgetDocEvent;
            this.m_BudgetDocStateIN = null;
            this.m_BudgetDocStateOUT = objBudgetDocStateOUT;
            this.m_User = null;
            this.m_RoutePointGroup = objRoutePointGroup;
        }
        public CRoutePoint( System.Boolean bIsEnter, System.Boolean bIsExit,
            CBudgetDocEvent objBudgetDocEvent, CBudgetDocState objBudgetDocStateIN, CBudgetDocState objBudgetDocStateOUT,
            CRoutePointGroup objRoutePointGroup, System.Boolean bShowInDocument, CUser objUser )
        {
            this.m_uuidID = System.Guid.Empty;
            this.m_strName = "";
            this.m_Sate = System.Data.DataRowState.Unchanged;
            this.m_IsEnter = bIsEnter;
            this.m_IsExit = bIsExit;
            this.m_bShowInDocument = bShowInDocument;
            this.m_BudgetDocEvent = objBudgetDocEvent;
            this.m_BudgetDocStateIN = objBudgetDocStateIN;
            this.m_BudgetDocStateOUT = objBudgetDocStateOUT;
            this.m_User = objUser;
            this.m_RoutePointGroup = objRoutePointGroup;
        }

        public CRoutePoint( System.Boolean bIsEnter, System.Boolean bIsExit,
            CBudgetDocEvent objBudgetDocEvent, CBudgetDocState objBudgetDocStateOUT,
            CRoutePointGroup objRoutePointGroup, System.Boolean bShowInDocument, CUser objUser )
        {
            this.m_uuidID = System.Guid.Empty;
            this.m_strName = "";
            this.m_Sate = System.Data.DataRowState.Unchanged;
            this.m_IsEnter = bIsEnter;
            this.m_IsExit = bIsExit;
            this.m_bShowInDocument = bShowInDocument;
            this.m_BudgetDocEvent = objBudgetDocEvent;
            this.m_BudgetDocStateIN = null;
            this.m_BudgetDocStateOUT = objBudgetDocStateOUT;
            this.m_User = objUser;
            this.m_RoutePointGroup = objRoutePointGroup;
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
            return false;
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
            return false;
        }
        #endregion

        #region Add
        public override System.Boolean Add( UniXP.Common.CProfile objProfile )
        {
            return false;
        }

        private enum ActionType { AddToDB=0, UpdateInDB, RemoveFromDB };
        /// <summary>
        /// ��������� ������������ ������� ������� ����� ����������� � ��
        /// </summary>
        /// <param name="actType">��� ���������������� ��������</param>
        /// <returns>true - �������� ���������; false - �������� �����������</returns>
        private System.Boolean CheckValidProperties( ActionType actType )
        {
            System.Boolean bRet = false;
            System.Int32 iErrCount = 0;
            try
            {
                switch( actType )
                {
                    case ActionType.AddToDB:
                    {
                        if( this.m_BudgetDocEvent == null )
                        {
                            DevExpress.XtraEditors.XtraMessageBox.Show(  "���������� ������� �������!", "��������",
                                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
                            iErrCount++;
                        }
                        //if( this.m_BudgetDocStateIN == null )
                        //{
                        //    DevExpress.XtraEditors.XtraMessageBox.Show(  "���������� ������� ��������� ��������� ���������� ���������!", "��������",
                        //        System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
                        //    iErrCount++;
                        //}
                        if( this.m_BudgetDocStateOUT == null )
                        {
                            DevExpress.XtraEditors.XtraMessageBox.Show(  "���������� ������� �������� ��������� ���������� ���������!", "��������",
                                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
                            iErrCount++;
                        }
                        break;
                    }
                    case ActionType.UpdateInDB:
                    {
                        // ���������� ������������� �� ������ ���� ������
                        if( this.m_uuidID == System.Guid.Empty )
                        {
                            DevExpress.XtraEditors.XtraMessageBox.Show(  "������������ �������� ����������� �������������� �������", "��������",
                                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
                            iErrCount++;
                        }
                        if( this.m_BudgetDocEvent == null )
                        {
                            DevExpress.XtraEditors.XtraMessageBox.Show(  "���������� ������� �������!", "��������",
                                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
                            iErrCount++;
                        }
                        if( this.m_BudgetDocStateIN == null )
                        {
                            DevExpress.XtraEditors.XtraMessageBox.Show(  "���������� ������� ��������� ��������� ���������� ���������!", "��������",
                                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
                            iErrCount++;
                        }
                        if( this.m_BudgetDocStateOUT == null )
                        {
                            DevExpress.XtraEditors.XtraMessageBox.Show(  "���������� ������� �������� ��������� ���������� ���������!", "��������",
                                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
                            iErrCount++;
                        }
                        break;
                    }
                    case ActionType.RemoveFromDB:
                    {
                        if( this.m_uuidID == System.Guid.Empty )
                        {
                            DevExpress.XtraEditors.XtraMessageBox.Show(  "������������ �������� ����������� �������������� �������", "��������",
                                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
                            iErrCount++;
                        }
                        break;
                    }
                    default:
                    {
                        break;
                    }
                }
                bRet = ( iErrCount == 0 );
            }
            catch( System.Exception f )
            {
                DevExpress.XtraEditors.XtraMessageBox.Show( 
                "�� ������� ��������� �������� ������� '����� ��������'.\n" + this.Name + f.Message, "��������",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
            }
            return bRet;
        }

        /// <summary>
        /// �������� ������ � �� (T_RouteDeclaration)
        /// </summary>
        /// <param name="objProfile">�������</param>
        /// <param name="cmd">SQL-�������</param>
        /// <param name="uuidRouteID">�� ������� ��������</param>
        /// <returns>true - ������� ����������; false - ������</returns>
        public System.Boolean AddRouteItem( UniXP.Common.CProfile objProfile, System.Guid uuidRouteID,
             System.Data.SqlClient.SqlCommand cmd )
        {
            System.Boolean bRet = false;
            if( ( cmd == null ) || ( cmd.Connection == null ) ) { return bRet; }

            // �������� ������� ������� ����� ����������� � ��
            if( this.CheckValidProperties( ActionType.AddToDB ) == false ) { return bRet; }

            try
            {
                cmd.Parameters.Clear();
                cmd.CommandText = System.String.Format( "[{0}].[dbo].[sp_AddRouteItem]", objProfile.GetOptionsDllDBName() );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ( ( System.Byte )( 0 ) ), ( ( System.Byte )( 0 ) ), "", System.Data.DataRowVersion.Current, null ) );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@ROUTE_GUID_ID", System.Data.SqlDbType.UniqueIdentifier ) );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@BUDGETDOCSTATE_OUT_GUID_ID", System.Data.SqlDbType.UniqueIdentifier ) );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@BUDGETDOCEVENT_GUID_ID", System.Data.SqlDbType.UniqueIdentifier ) );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@POINT_ENTER", System.Data.SqlDbType.Bit ) );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@POINT_EXIT", System.Data.SqlDbType.Bit ) );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@POINT_SHOW", System.Data.SqlDbType.Bit ) );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@ROUTEPOINTGROUP_GUID_ID", System.Data.SqlDbType.UniqueIdentifier ) );
                cmd.Parameters[ "@ROUTE_GUID_ID" ].Value = uuidRouteID;
                cmd.Parameters[ "@BUDGETDOCSTATE_OUT_GUID_ID" ].Value = this.m_BudgetDocStateOUT.uuidID;
                cmd.Parameters[ "@BUDGETDOCEVENT_GUID_ID" ].Value = this.m_BudgetDocEvent.uuidID;
                cmd.Parameters[ "@POINT_ENTER" ].Value = this.m_IsEnter;
                cmd.Parameters[ "@POINT_EXIT" ].Value = this.m_IsExit;
                cmd.Parameters[ "@POINT_SHOW" ].Value = this.m_bShowInDocument;
                cmd.Parameters[ "@ROUTEPOINTGROUP_GUID_ID" ].Value = this.m_RoutePointGroup.uuidID;
                if( this.BudgetDocStateIN != null )
                {
                    cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@BUDGETDOCSTATE_IN_GUID_ID", System.Data.SqlDbType.UniqueIdentifier ) );
                    cmd.Parameters[ "@BUDGETDOCSTATE_IN_GUID_ID" ].Value = this.m_BudgetDocStateIN.uuidID;
                }
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
                            DevExpress.XtraEditors.XtraMessageBox.Show(  "������� �������� � �������� ��������������� �� ������.\n" + uuidRouteID.ToString(), "��������",
                                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning );
                            break;
                        }
                        case 2:
                        {
                            DevExpress.XtraEditors.XtraMessageBox.Show(  "��������� ��������� ���������� ��������� � �������� ��������������� �� �������.\n" +
                                    this.m_BudgetDocStateIN.uuidID.ToString(), "��������",
                                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning );
                            break;
                        }
                        case 3:
                        {
                            DevExpress.XtraEditors.XtraMessageBox.Show(  "�������� ��������� ���������� ��������� � �������� ��������������� �� �������.\n" +
                                    this.m_BudgetDocStateOUT.uuidID.ToString(), "��������",
                                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning );
                            break;
                        }
                        case 4:
                        {
                            DevExpress.XtraEditors.XtraMessageBox.Show(  "������� ���������� ��������� � �������� ��������������� �� �������.\n" +
                                    this.m_BudgetDocEvent.uuidID.ToString(), "��������",
                                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning );
                            break;
                        }
                        case 5:
                        {
                            DevExpress.XtraEditors.XtraMessageBox.Show(  "������ ����� �������� � �������� ��������������� �� �������.\n" +
                                    this.m_RoutePointGroup.uuidID.ToString(), "��������",
                                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning );
                            break;
                        }
                        default:
                        {
                            DevExpress.XtraEditors.XtraMessageBox.Show(  "������ ���������� ���������� � ���� ������", "��������",
                                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
                            break;
                        }
                    }
                }
            }
            catch( System.Exception e )
            {
                DevExpress.XtraEditors.XtraMessageBox.Show( 
                "�� ������� ������� ����� ��������.\n" + e.Message, "��������",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
            }
            finally // ������� ���������� �������
            {
            }
            return bRet;
        }

        /// <summary>
        /// �������� ������ � �� (T_BudgetDocRouteDeclaration)
        /// </summary>
        /// <param name="objProfile">�������</param>
        /// <param name="cmd">SQL-�������</param>
        /// <param name="uuidBudgetDocID">�� ���������� ���������</param>
        /// <returns>true - ������� ����������; false - ������</returns>
        public System.Boolean AddBudgetDocRouteItem( UniXP.Common.CProfile objProfile, System.Guid uuidBudgetDocID,
             System.Data.SqlClient.SqlCommand cmd )
        {
            System.Boolean bRet = false;
            if( ( cmd == null ) || ( cmd.Connection == null ) ) { return bRet; }

            // �������� ������� ������� ����� ����������� � ��
            if( this.CheckValidProperties( ActionType.AddToDB ) == false ) { return bRet; }

            try
            {
                cmd.Parameters.Clear();
                cmd.CommandText = System.String.Format( "[{0}].[dbo].[sp_AddBudgetDocRouteItem]", objProfile.GetOptionsDllDBName() );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ( ( System.Byte )( 0 ) ), ( ( System.Byte )( 0 ) ), "", System.Data.DataRowVersion.Current, null ) );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@BUDGETDOC_GUID_ID", System.Data.SqlDbType.UniqueIdentifier ) );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@BUDGETDOCSTATE_OUT_GUID_ID", System.Data.SqlDbType.UniqueIdentifier ) );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@BUDGETDOCEVENT_GUID_ID", System.Data.SqlDbType.UniqueIdentifier ) );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@POINT_ENTER", System.Data.SqlDbType.Bit ) );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@POINT_EXIT", System.Data.SqlDbType.Bit ) );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@POINT_SHOW", System.Data.SqlDbType.Bit ) );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@ROUTEPOINTGROUP_GUID_ID", System.Data.SqlDbType.UniqueIdentifier ) );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@USER_ID", System.Data.SqlDbType.Int ) );
                cmd.Parameters[ "@BUDGETDOC_GUID_ID" ].Value = uuidBudgetDocID;
                cmd.Parameters[ "@BUDGETDOCSTATE_OUT_GUID_ID" ].Value = this.m_BudgetDocStateOUT.uuidID;
                cmd.Parameters[ "@BUDGETDOCEVENT_GUID_ID" ].Value = this.m_BudgetDocEvent.uuidID;
                cmd.Parameters[ "@POINT_ENTER" ].Value = this.m_IsEnter;
                cmd.Parameters[ "@POINT_EXIT" ].Value = this.m_IsExit;
                cmd.Parameters[ "@POINT_SHOW" ].Value = this.m_bShowInDocument;
                cmd.Parameters[ "@ROUTEPOINTGROUP_GUID_ID" ].Value = this.m_RoutePointGroup.uuidID;
                cmd.Parameters[ "@USER_ID" ].Value = this.m_User.ulID;
                if( this.BudgetDocStateIN != null )
                {
                    cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@BUDGETDOCSTATE_IN_GUID_ID", System.Data.SqlDbType.UniqueIdentifier ) );
                    cmd.Parameters[ "@BUDGETDOCSTATE_IN_GUID_ID" ].Value = this.m_BudgetDocStateIN.uuidID;
                }
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
                            DevExpress.XtraEditors.XtraMessageBox.Show(  "��������� �������� � �������� ��������������� �� ������.\n" + uuidBudgetDocID.ToString(), "��������",
                                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning );
                            break;
                        }
                        case 2:
                        {
                            DevExpress.XtraEditors.XtraMessageBox.Show(  "��������� ��������� ���������� ��������� � �������� ��������������� �� �������.\n" +
                                    this.m_BudgetDocStateIN.uuidID.ToString(), "��������",
                                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning );
                            break;
                        }
                        case 3:
                        {
                            DevExpress.XtraEditors.XtraMessageBox.Show(  "�������� ��������� ���������� ��������� � �������� ��������������� �� �������.\n" +
                                    this.m_BudgetDocStateOUT.uuidID.ToString(), "��������",
                                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning );
                            break;
                        }
                        case 4:
                        {
                            DevExpress.XtraEditors.XtraMessageBox.Show(  "������� ���������� ��������� � �������� ��������������� �� �������.\n" +
                                    this.m_BudgetDocEvent.uuidID.ToString(), "��������",
                                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning );
                            break;
                        }
                        case 5:
                        {
                            DevExpress.XtraEditors.XtraMessageBox.Show(  "������ ����� �������� � �������� ��������������� �� �������.\n" +
                                    this.m_RoutePointGroup.uuidID.ToString(), "��������",
                                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning );
                            break;
                        }
                        case 6:
                        {
                            DevExpress.XtraEditors.XtraMessageBox.Show(  "������������ � �������� ��������������� �� ������.\n" +
                                    this.m_User.ulID.ToString(), "��������",
                                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning );
                            break;
                        }
                        default:
                        {
                            DevExpress.XtraEditors.XtraMessageBox.Show(  "������ ���������� ���������� � ���� ������", "��������",
                                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
                            break;
                        }
                    }
                }
            }
            catch( System.Exception e )
            {
                DevExpress.XtraEditors.XtraMessageBox.Show( 
                "�� ������� ������� ����� ��������.\n" + e.Message, "��������",
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
            return false;
        }
        #endregion


    }
}
