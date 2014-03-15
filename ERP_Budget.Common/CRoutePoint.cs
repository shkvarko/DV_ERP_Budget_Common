using System;
using System.Collections.Generic;
using System.Text;

namespace ERP_Budget.Common
{
    /// <summary>
    /// Класс "Точка маршрута"
    /// </summary>
    public class CRoutePoint : IBaseListItem
    {
        #region Переменные, Свойства, Константы
        /// <summary>
        /// Признак того, является ли точка маршрута выходом из маршрута
        /// </summary>
        private System.Boolean m_IsExit;
        /// <summary>
        /// Признак того, является ли точка маршрута выходом из маршрута
        /// </summary>
        public System.Boolean IsExit
        {
            get { return m_IsExit; }
            set { m_IsExit = value; }
        }
        /// <summary>
        /// Признак того, является ли точка маршрута началом маршрута
        /// </summary>
        private System.Boolean m_IsEnter;
        /// <summary>
        /// Признак того, является ли точка маршрута началом маршрута
        /// </summary>
        public System.Boolean IsEnter
        {
            get { return m_IsEnter; }
            set { m_IsEnter = value; }
        }
        /// <summary>
        /// Признак того, показывать точку маршрута в документе или нет
        /// </summary>
        private System.Boolean m_bShowInDocument;
        /// <summary>
        /// Признак того, показывать точку маршрута в документе или нет
        /// </summary>
        public System.Boolean ShowInDocument
        {
            get { return m_bShowInDocument; }
            set { m_bShowInDocument = value; }
        }
        /// <summary>
        /// Начальное сотояние документа
        /// </summary>
        private CBudgetDocState m_BudgetDocStateIN;
        /// <summary>
        /// Начальное сотояние документа
        /// </summary>
        public CBudgetDocState BudgetDocStateIN
        {
            get { return m_BudgetDocStateIN; }
            set { m_BudgetDocStateIN = value; }
        }
        /// <summary>
        /// Конечное сотояние документа
        /// </summary>
        private CBudgetDocState m_BudgetDocStateOUT;
        /// <summary>
        /// Конечное сотояние документа
        /// </summary>
        public CBudgetDocState BudgetDocStateOUT
        {
            get { return m_BudgetDocStateOUT; }
            set { m_BudgetDocStateOUT = value; }
        }
        /// <summary>
        /// Событие, переводящее документ из начального состояния в конечное
        /// </summary>
        private CBudgetDocEvent m_BudgetDocEvent;
        /// <summary>
        /// Событие, переводящее документ из начального состояния в конечное
        /// </summary>
        public CBudgetDocEvent BudgetDocEvent
        {
            get { return m_BudgetDocEvent; }
            set { m_BudgetDocEvent = value; }
        }
        /// <summary>
        /// Группа точек маршрута
        /// </summary>
        private CRoutePointGroup m_RoutePointGroup;
        /// <summary>
        /// Группа точек маршрута
        /// </summary>
        public CRoutePointGroup RoutePointGroup
        {
            get { return m_RoutePointGroup; }
            set { m_RoutePointGroup = value; }
        }
        /// <summary>
        /// Пользователь, который должен осуществить событие
        /// </summary>
        private CUser m_User;
        /// <summary>
        /// Пользователь, который должен осуществить событие
        /// </summary>
        public CUser UserEvent
        {
            get { return m_User; }
            set { m_User = value; }
        }
        /// <summary>
        /// Состояние объекта
        /// </summary>
        private System.Data.DataRowState m_Sate;
        /// <summary>
        /// Состояние объекта
        /// </summary>
        public System.Data.DataRowState State
        {
            get { return m_Sate; }
            set { m_Sate = value; }
        }
        #endregion

        #region Конструкторы
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
        /// Инициализация свойств класса
        /// </summary>
        /// <param name="objProfile">профайл</param>
        /// <param name="uuidID">уникальный идентификатор класса</param>
        /// <returns>true - успешная инициализация; false - ошибка</returns>
        public override System.Boolean Init( UniXP.Common.CProfile objProfile, System.Guid uuidID )
        {
            return false;
        }

        #endregion

        #region Remove
        /// <summary>
        /// Удалить запись из БД
        /// </summary>
        /// <param name="objProfile">профайл</param>
        /// <param name="uuidID">уникальный идентификатор объекта</param>
        /// <returns>true - удачное завершение; false - ошибка</returns>
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
        /// Проверяет корректность свойств объекта перед сохранением в БД
        /// </summary>
        /// <param name="actType">тип предпринемаевого действия</param>
        /// <returns>true - свойства корректны; false - свойства некорректны</returns>
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
                            DevExpress.XtraEditors.XtraMessageBox.Show(  "Необходимо указать событие!", "Внимание",
                                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
                            iErrCount++;
                        }
                        //if( this.m_BudgetDocStateIN == null )
                        //{
                        //    DevExpress.XtraEditors.XtraMessageBox.Show(  "Необходимо указать начальное состояние бюджетного документа!", "Внимание",
                        //        System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
                        //    iErrCount++;
                        //}
                        if( this.m_BudgetDocStateOUT == null )
                        {
                            DevExpress.XtraEditors.XtraMessageBox.Show(  "Необходимо указать конечное состояние бюджетного документа!", "Внимание",
                                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
                            iErrCount++;
                        }
                        break;
                    }
                    case ActionType.UpdateInDB:
                    {
                        // уникальный идентификатор не должен быть пустым
                        if( this.m_uuidID == System.Guid.Empty )
                        {
                            DevExpress.XtraEditors.XtraMessageBox.Show(  "Недопустимое значение уникального идентификатора объекта", "Внимание",
                                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
                            iErrCount++;
                        }
                        if( this.m_BudgetDocEvent == null )
                        {
                            DevExpress.XtraEditors.XtraMessageBox.Show(  "Необходимо указать событие!", "Внимание",
                                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
                            iErrCount++;
                        }
                        if( this.m_BudgetDocStateIN == null )
                        {
                            DevExpress.XtraEditors.XtraMessageBox.Show(  "Необходимо указать начальное состояние бюджетного документа!", "Внимание",
                                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
                            iErrCount++;
                        }
                        if( this.m_BudgetDocStateOUT == null )
                        {
                            DevExpress.XtraEditors.XtraMessageBox.Show(  "Необходимо указать конечное состояние бюджетного документа!", "Внимание",
                                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
                            iErrCount++;
                        }
                        break;
                    }
                    case ActionType.RemoveFromDB:
                    {
                        if( this.m_uuidID == System.Guid.Empty )
                        {
                            DevExpress.XtraEditors.XtraMessageBox.Show(  "Недопустимое значение уникального идентификатора объекта", "Внимание",
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
                "Не удалось проверить свойства объекта 'Точка маршрута'.\n" + this.Name + f.Message, "Внимание",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
            }
            return bRet;
        }

        /// <summary>
        /// Добавить запись в БД (T_RouteDeclaration)
        /// </summary>
        /// <param name="objProfile">профайл</param>
        /// <param name="cmd">SQL-команда</param>
        /// <param name="uuidRouteID">уи шаблона маршрута</param>
        /// <returns>true - удачное завершение; false - ошибка</returns>
        public System.Boolean AddRouteItem( UniXP.Common.CProfile objProfile, System.Guid uuidRouteID,
             System.Data.SqlClient.SqlCommand cmd )
        {
            System.Boolean bRet = false;
            if( ( cmd == null ) || ( cmd.Connection == null ) ) { return bRet; }

            // проверка свойств объекта перед сохранением в БД
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
                            DevExpress.XtraEditors.XtraMessageBox.Show(  "Шаблона маршрута с заданным идентификатором не найден.\n" + uuidRouteID.ToString(), "Внимание",
                                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning );
                            break;
                        }
                        case 2:
                        {
                            DevExpress.XtraEditors.XtraMessageBox.Show(  "Начальное состояние бюджетного документа с заданным идентификатором не найдено.\n" +
                                    this.m_BudgetDocStateIN.uuidID.ToString(), "Внимание",
                                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning );
                            break;
                        }
                        case 3:
                        {
                            DevExpress.XtraEditors.XtraMessageBox.Show(  "Конечное состояние бюджетного документа с заданным идентификатором не найдено.\n" +
                                    this.m_BudgetDocStateOUT.uuidID.ToString(), "Внимание",
                                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning );
                            break;
                        }
                        case 4:
                        {
                            DevExpress.XtraEditors.XtraMessageBox.Show(  "Событие бюджетного документа с заданным идентификатором не найдено.\n" +
                                    this.m_BudgetDocEvent.uuidID.ToString(), "Внимание",
                                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning );
                            break;
                        }
                        case 5:
                        {
                            DevExpress.XtraEditors.XtraMessageBox.Show(  "Группа точек маршрута с заданным идентификатором не найдена.\n" +
                                    this.m_RoutePointGroup.uuidID.ToString(), "Внимание",
                                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning );
                            break;
                        }
                        default:
                        {
                            DevExpress.XtraEditors.XtraMessageBox.Show(  "Ошибка добавления информации в базу данных", "Внимание",
                                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
                            break;
                        }
                    }
                }
            }
            catch( System.Exception e )
            {
                DevExpress.XtraEditors.XtraMessageBox.Show( 
                "Не удалось создать пункт маршрута.\n" + e.Message, "Внимание",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
            }
            finally // очищаем занимаемые ресурсы
            {
            }
            return bRet;
        }

        /// <summary>
        /// Добавить запись в БД (T_BudgetDocRouteDeclaration)
        /// </summary>
        /// <param name="objProfile">профайл</param>
        /// <param name="cmd">SQL-команда</param>
        /// <param name="uuidBudgetDocID">уи бюджетного документа</param>
        /// <returns>true - удачное завершение; false - ошибка</returns>
        public System.Boolean AddBudgetDocRouteItem( UniXP.Common.CProfile objProfile, System.Guid uuidBudgetDocID,
             System.Data.SqlClient.SqlCommand cmd )
        {
            System.Boolean bRet = false;
            if( ( cmd == null ) || ( cmd.Connection == null ) ) { return bRet; }

            // проверка свойств объекта перед сохранением в БД
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
                            DevExpress.XtraEditors.XtraMessageBox.Show(  "Бюджетный документ с заданным идентификатором не найден.\n" + uuidBudgetDocID.ToString(), "Внимание",
                                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning );
                            break;
                        }
                        case 2:
                        {
                            DevExpress.XtraEditors.XtraMessageBox.Show(  "Начальное состояние бюджетного документа с заданным идентификатором не найдено.\n" +
                                    this.m_BudgetDocStateIN.uuidID.ToString(), "Внимание",
                                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning );
                            break;
                        }
                        case 3:
                        {
                            DevExpress.XtraEditors.XtraMessageBox.Show(  "Конечное состояние бюджетного документа с заданным идентификатором не найдено.\n" +
                                    this.m_BudgetDocStateOUT.uuidID.ToString(), "Внимание",
                                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning );
                            break;
                        }
                        case 4:
                        {
                            DevExpress.XtraEditors.XtraMessageBox.Show(  "Событие бюджетного документа с заданным идентификатором не найдено.\n" +
                                    this.m_BudgetDocEvent.uuidID.ToString(), "Внимание",
                                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning );
                            break;
                        }
                        case 5:
                        {
                            DevExpress.XtraEditors.XtraMessageBox.Show(  "Группа точек маршрута с заданным идентификатором не найдена.\n" +
                                    this.m_RoutePointGroup.uuidID.ToString(), "Внимание",
                                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning );
                            break;
                        }
                        case 6:
                        {
                            DevExpress.XtraEditors.XtraMessageBox.Show(  "Пользователь с заданным идентификатором не найден.\n" +
                                    this.m_User.ulID.ToString(), "Внимание",
                                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning );
                            break;
                        }
                        default:
                        {
                            DevExpress.XtraEditors.XtraMessageBox.Show(  "Ошибка добавления информации в базу данных", "Внимание",
                                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
                            break;
                        }
                    }
                }
            }
            catch( System.Exception e )
            {
                DevExpress.XtraEditors.XtraMessageBox.Show( 
                "Не удалось создать пункт маршрута.\n" + e.Message, "Внимание",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
            }
            finally // очищаем занимаемые ресурсы
            {
            }
            return bRet;
        }
        #endregion

        #region Update
        /// <summary>
        /// Сохранить изменения в БД
        /// </summary>
        /// <param name="objProfile">профайл</param>
        /// <returns>true - удачное завершение; false - ошибка</returns>
        public override System.Boolean Update( UniXP.Common.CProfile objProfile )
        {
            return false;
        }
        #endregion


    }
}
