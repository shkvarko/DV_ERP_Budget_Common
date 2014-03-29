using System;
using System.Collections.Generic;
using System.Text;

namespace ERP_Budget.Common
{
    /// <summary>
    /// Класс "Бюджетная проводка"
    /// </summary>
    public class CAccountTransn
    {
        #region Переменные, Свойства, Константы
        /// <summary>
        /// Уникальный идентификатор
        /// </summary>
        private System.Guid m_uuidID;
        /// <summary>
        /// Уникальный идентификатор
        /// </summary>
        public System.Guid uuidID
        {
            get { return m_uuidID; }
            set { m_uuidID = value; }
        }
        /// <summary>
        /// Расшифровка статьи бюджетного документа
        /// </summary>
        private CBudgetItemDecode m_objBudgetItemDecode;
        /// <summary>
        /// Расшифровка статьи бюджетного документа
        /// </summary>
        public CBudgetItemDecode BudgetItemDecode
        {
            get { return m_objBudgetItemDecode; }
            set { m_objBudgetItemDecode = value; }
        }
        /// <summary>
        /// Валюта
        /// </summary>
        private CCurrency m_objCurrency;
        /// <summary>
        /// Валюта
        /// </summary>
        public CCurrency Currency
        {
            get { return m_objCurrency; }
            set { m_objCurrency = value; }
        }
        /// <summary>
        /// Бюджетный документ
        /// </summary>
        private CBudgetDoc m_objBudgetDoc;
        /// <summary>
        /// Бюджетный документ
        /// </summary>
        public CBudgetDoc BudgetDoc
        {
            get { return m_objBudgetDoc; }
            set { m_objBudgetDoc = value; }
        }
        /// <summary>
        /// Дата проводки
        /// </summary>
        private System.DateTime m_dtDate;
        /// <summary>
        /// Дата проводки
        /// </summary>
        public System.DateTime Date
        {
            get { return m_dtDate; }
            set { m_dtDate = value; }
        }
        /// <summary>
        /// Сумма проводки
        /// </summary>
        private double m_moneyMoney;
        /// <summary>
        /// Сумма проводки
        /// </summary>
        public double Money
        {
            get { return m_moneyMoney; }
            set { m_moneyMoney = value; }
        }
        /// <summary>
        /// Описание
        /// </summary>
        private System.String m_strDescription;
        /// <summary>
        /// Описание
        /// </summary>
        public System.String Description
        {
            get { return m_strDescription; }
            set { m_strDescription = value; }
        }
        /// <summary>
        /// Пользователь, создавший проводку
        /// </summary>
        private CUser m_objUser;
        /// <summary>
        /// Пользователь, создавший проводку
        /// </summary>
        public CUser User
        {
            get { return m_objUser; }
            set { m_objUser = value; }
        }
        /// <summary>
        /// Операция
        /// </summary>
        private System.String m_strOperName;
        /// <summary>
        /// Операция
        /// </summary>
        public System.String OperName
        {
            get { return m_strOperName; }
            set { m_strOperName = value; }
        }
        #endregion

        #region Конструкторы
        public CAccountTransn()
        {
            this.m_uuidID = System.Guid.Empty;
            this.m_dtDate = System.DateTime.Now;
            this.m_moneyMoney = 0;
            this.m_objBudgetDoc = null;
            this.m_objBudgetItemDecode = null;
            this.m_objCurrency = null;
            this.m_strDescription = "";
            this.m_objUser = null;
            this.m_strOperName = "";
        }
        public CAccountTransn( System.Guid uuidID, System.DateTime dtDate, System.Double Money,
            CCurrency objCurrency, System.String strDescription )
        {
            this.m_uuidID = uuidID;
            this.m_dtDate = dtDate;
            this.m_moneyMoney = Money;
            this.m_objBudgetDoc = null;
            this.m_objBudgetItemDecode = null;
            this.m_objCurrency = objCurrency;
            this.m_strDescription = strDescription;
            this.m_objUser = null;
            this.m_strOperName = "";
        }
        public CAccountTransn( System.Guid uuidID, System.DateTime dtDate, System.Double Money,
            CCurrency objCurrency, CBudgetDoc objBudgetDoc, System.String strDescription, CUser objUser,
            System.String strOperName )
        {
            this.m_uuidID = uuidID;
            this.m_dtDate = dtDate;
            this.m_moneyMoney = Money;
            this.m_objBudgetDoc = objBudgetDoc;
            this.m_objBudgetItemDecode = null;
            this.m_objCurrency = objCurrency;
            this.m_strDescription = strDescription;
            this.m_objUser = objUser;
            this.m_strOperName = strOperName;
        }
        #endregion

        #region Списки проводок
        /// <summary>
        /// Возвращает список проводок для бюджетного документа
        /// </summary>
        /// <param name="objProfile">профайл</param>
        /// <param name="objBudgetDoc">бюджетный документ</param>
        /// <returns>список проводок</returns>
        public static List<CAccountTransn> GetAccountTransnListForBudgetDoc( UniXP.Common.CProfile objProfile,
            CBudgetDoc objBudgetDoc )
        {
            List<CAccountTransn> objList = new List<CAccountTransn>();

            System.Data.SqlClient.SqlConnection DBConnection = objProfile.GetDBSource();
            if( DBConnection == null ) 
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(  "Отсутствует соединение с БД.", "Внимание",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning );
                return objList; 
            }

            try
            {
                // соединение с БД получено, прописываем команду на выборку данных
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
                cmd.Connection = DBConnection;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = System.String.Format( "[{0}].[dbo].[sp_GetAccountTransnForBudgetDoc]", objProfile.GetOptionsDllDBName() );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ( ( System.Byte )( 0 ) ), ( ( System.Byte )( 0 ) ), "", System.Data.DataRowVersion.Current, null ) );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@GUID_ID", System.Data.SqlDbType.UniqueIdentifier ) );
                cmd.Parameters[ "@GUID_ID" ].Value = objBudgetDoc.uuidID;
                System.Data.SqlClient.SqlDataReader rs = cmd.ExecuteReader();
                System.String strDescription = "";
                if( rs.HasRows )
                {
                    while( rs.Read() )
                    {
                        CUser objUser = new CUser( ( System.Int32 )rs[ "ERPBUDGET_USERID" ], ( System.Int32 )rs[ "UNIXP_USERID" ],
                            ( System.String )rs[ "USER_LASTNAME" ], ( System.String )rs[ "USER_FIRSTNAME" ] );
                        CCurrency objCurrency = new CCurrency( ( System.Guid )rs[ "CURRENCY_GUID_ID" ], 
                            ( System.String )rs[ "CURRENCY_CODE" ], ( System.String )rs[ "CURRENCY_NAME" ] );
                        System.String strOperName = "";
                        System.Int32 iOperType = rs.GetInt32( 16 );
                        switch( iOperType )
                        {
                            case 0:
                            strOperName = "Резерв средств";
                            break;
                            case 1:
                            strOperName = "Снятие средств с резерва";
                            break;
                            case 2:
                            strOperName = "Увеличение суммы статьи расходов";
                            break;
                            case 3:
                            strOperName = "Уменьшение суммы статьи расходов";
                            break;
                            case 4:
                            strOperName = "Оплата";
                            break;
                            case 5:
                            strOperName = "Возврат средств";
                            break;
                            case 6:
                            strOperName = "Курсовая разница";
                            break;
                            default:
                            break;
                        }
                        strDescription = ( rs[ "ACCOUNTTRANSN_DESCRIPTION" ] == System.DBNull.Value ) ? "" : ( System.String )rs[ "ACCOUNTTRANSN_DESCRIPTION" ];
                        objList.Add( new CAccountTransn( ( System.Guid )rs[ "GUID_ID" ], ( System.DateTime )rs[ "ACCOUNTTRANSN_DATE" ],
                            System.Convert.ToDouble( rs[ "ACCOUNTTRANSN_MONEY" ] ),
                            objCurrency, objBudgetDoc, strDescription, objUser, strOperName ) );
                    }
                }
                rs.Close();
                rs = null;
                cmd = null;
            }
            catch( System.Exception f )
            {
                DevExpress.XtraEditors.XtraMessageBox.Show( 
                "Не удалось получить список проводок.\n\nТекст ошибки: " + f.Message, "Внимание",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
            }
            finally // очищаем занимаемые ресурсы
            {
                DBConnection.Close();
            }
            return objList;
        }
        /// <summary>
        /// Возвращает список проводок для бюджетного документа
        /// </summary>
        /// <param name="objProfile">профайл</param>
        /// <param name="cmd">SQL-команда</param>
        /// <param name="uuidBudgetDocID">уи бюджетного документа</param>
        /// <returns>список проводок</returns>
        public static List<CAccountTransn> GetAccountTransnListForManagerBudgetDoc(UniXP.Common.CProfile objProfile, 
            System.Data.SqlClient.SqlCommand cmd, System.Guid uuidBudgetDocID)
        {
            List<CAccountTransn> objList = new List<CAccountTransn>();

            if (cmd == null)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show("Отсутствует соединение с БД.", "Внимание",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);
                return objList;
            }

            try
            {
                // соединение с БД получено, прописываем команду на выборку данных
                cmd.Parameters.Clear();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = System.String.Format("[{0}].[dbo].[sp_GetAccountTransnForBudgetDoc]", objProfile.GetOptionsDllDBName());
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ((System.Byte)(0)), ((System.Byte)(0)), "", System.Data.DataRowVersion.Current, null));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@GUID_ID", System.Data.SqlDbType.UniqueIdentifier));
                cmd.Parameters["@GUID_ID"].Value = uuidBudgetDocID;
                System.Data.SqlClient.SqlDataReader rs = cmd.ExecuteReader();
                System.String strDescription = "";
                CAccountTransn objAccountTransn = null;
                if (rs.HasRows)
                {
                    while (rs.Read())
                    {
                        CUser objUser = new CUser((System.Int32)rs["ERPBUDGET_USERID"], (System.Int32)rs["UNIXP_USERID"],
                            (System.String)rs["USER_LASTNAME"], (System.String)rs["USER_FIRSTNAME"]);
                        CCurrency objCurrency = new CCurrency((System.Guid)rs["CURRENCY_GUID_ID"],
                            (System.String)rs["CURRENCY_CODE"], (System.String)rs["CURRENCY_NAME"]);
                        System.String strOperName = "";
                        System.Int32 iOperType = rs.GetInt32(16);
                        switch (iOperType)
                        {
                            case 0:
                                strOperName = "Резерв средств";
                                break;
                            case 1:
                                strOperName = "Снятие средств с резерва";
                                break;
                            case 2:
                                strOperName = "Увеличение суммы статьи расходов";
                                break;
                            case 3:
                                strOperName = "Уменьшение суммы статьи расходов";
                                break;
                            case 4:
                                strOperName = "Оплата";
                                break;
                            case 5:
                                strOperName = "Возврат средств";
                                break;
                            case 6:
                                strOperName = "Курсовая разница";
                                break;
                            default:
                                break;
                        }
                        strDescription = (rs["ACCOUNTTRANSN_DESCRIPTION"] == System.DBNull.Value) ? "" : (System.String)rs["ACCOUNTTRANSN_DESCRIPTION"];
                        objAccountTransn = new CAccountTransn();
                        objAccountTransn.uuidID = (System.Guid)rs["GUID_ID"];
                        objAccountTransn.Date = (System.DateTime)rs["ACCOUNTTRANSN_DATE"];
                        objAccountTransn.Money = System.Convert.ToDouble(rs["ACCOUNTTRANSN_MONEY"]);
                        objAccountTransn.Currency = objCurrency;
                        objAccountTransn.User = objUser;
                        objAccountTransn.OperName = strOperName;
                        objAccountTransn.Description = strDescription;
                        objList.Add(objAccountTransn);
                    }
                }
                rs.Close();
                rs = null;
            }
            catch (System.Exception f)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(
                "Не удалось получить список проводок.\n\nТекст ошибки: " + f.Message, "Внимание",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
            finally // очищаем занимаемые ресурсы
            {
            }
            return objList;
        }

        #endregion

        #region Резерв суммы по заявке ID = 0 
        /// <summary>
        /// Резервирует сумму для бюджетного документа
        /// </summary>
        /// <param name="objBudgetDoc">бюджетный документ</param>
        /// <param name="dtDate">дата операции</param>
        /// <param name="objProfile">профайл</param>
        /// <param name="cmd">SQL-команда</param>
        /// <returns>true - успешно; false - ошибка</returns>
        public static System.Boolean ResrveMoney( CBudgetDoc objBudgetDoc, System.DateTime dtDate,
            UniXP.Common.CProfile objProfile, System.Data.SqlClient.SqlCommand cmd, System.Int32 iUserID )
        {
            System.Boolean bRet = false;
            if( cmd == null ) { return bRet; }

            try
            {
                cmd.Parameters.Clear();
                cmd.CommandText = System.String.Format( "[{0}].[dbo].[sp_AccTrnReservMoney]", objProfile.GetOptionsDllDBName() );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ( ( System.Byte )( 0 ) ), ( ( System.Byte )( 0 ) ), "", System.Data.DataRowVersion.Current, null ) );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@RESERV_MONEY", System.Data.SqlDbType.Money, 8, System.Data.ParameterDirection.Output, false, ( ( System.Byte )( 0 ) ), ( ( System.Byte )( 0 ) ), "", System.Data.DataRowVersion.Current, null ) );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@DEFICIT_MONEY", System.Data.SqlDbType.Money, 8, System.Data.ParameterDirection.Output, false, ( ( System.Byte )( 0 ) ), ( ( System.Byte )( 0 ) ), "", System.Data.DataRowVersion.Current, null ) );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@BUDGETDOC_GUID_ID", System.Data.SqlDbType.UniqueIdentifier ) );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@ACCOUNTTRANSN_DATE", System.Data.SqlDbType.DateTime ) );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@USERS_ID", System.Data.SqlDbType.Int, 4 ) );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@ERROR_NUM", System.Data.SqlDbType.Int, 8, System.Data.ParameterDirection.Output, false, ( ( System.Byte )( 0 ) ), ( ( System.Byte )( 0 ) ), "", System.Data.DataRowVersion.Current, null ) );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@ERROR_MES", System.Data.SqlDbType.NVarChar, 4000 ) );
                cmd.Parameters[ "@ERROR_MES" ].Direction = System.Data.ParameterDirection.Output;
                cmd.Parameters[ "@BUDGETDOC_GUID_ID" ].Value = objBudgetDoc.uuidID;
                cmd.Parameters[ "@ACCOUNTTRANSN_DATE" ].Value = dtDate;
                cmd.Parameters[ "@USERS_ID" ].Value = iUserID;

                cmd.ExecuteNonQuery();
                System.Int32 iRet = ( System.Int32 )cmd.Parameters[ "@RETURN_VALUE" ].Value;
                if( iRet != 0 )
                {
                    DevExpress.XtraEditors.XtraMessageBox.Show(  "Ошибка резерва суммы.\n\nТекст ошибки: " + 
                        ( System.String )cmd.Parameters[ "@ERROR_MES" ].Value, "Ошибка",
                        System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
                } 

                bRet = ( iRet == 0 );
            }
            catch( System.Exception f )
            {
                DevExpress.XtraEditors.XtraMessageBox.Show( 
                "Не удалось зарезервировать сумму.\n\nТекст ошибки: " + f.Message, "Внимание",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
            }

            return bRet;
        }

        /// <summary>
        /// Возвращает размер суммы резерва для бюджетного документа
        /// </summary>
        /// <param name="objBudgetDoc">бюджетный документ</param>
        /// <param name="objProfile">профайл</param>
        /// <returns>true - успешно; false - ошибка</returns>
        public static System.Double GetResrveMoney( CBudgetDoc objBudgetDoc, UniXP.Common.CProfile objProfile, System.Data.SqlClient.SqlCommand cmd )
        {
            System.Double moneyRet = -999999;
            if( cmd == null ) { return moneyRet; }
            try
            {
                // соединение с БД получено, прописываем команду на выборку данных
                cmd.Parameters.Clear();
                cmd.CommandText = System.String.Format( "[{0}].[dbo].[sp_AccTrnCheckReservMoney]", objProfile.GetOptionsDllDBName() );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ( ( System.Byte )( 0 ) ), ( ( System.Byte )( 0 ) ), "", System.Data.DataRowVersion.Current, null ) );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@BUDGETDOC_GUID_ID", System.Data.SqlDbType.UniqueIdentifier ) );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@RESERV_MONEY", System.Data.SqlDbType.Money, 8, System.Data.ParameterDirection.Output, false, ( ( System.Byte )( 0 ) ), ( ( System.Byte )( 0 ) ), "", System.Data.DataRowVersion.Current, null ) );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@ERROR_NUM", System.Data.SqlDbType.Int, 8, System.Data.ParameterDirection.Output, false, ( ( System.Byte )( 0 ) ), ( ( System.Byte )( 0 ) ), "", System.Data.DataRowVersion.Current, null ) );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@ERROR_MES", System.Data.SqlDbType.NVarChar, 4000 ) );
                cmd.Parameters[ "@ERROR_MES" ].Direction = System.Data.ParameterDirection.Output;
                cmd.Parameters[ "@BUDGETDOC_GUID_ID" ].Value = objBudgetDoc.uuidID;

                cmd.ExecuteNonQuery();
                System.Int32 iRet = ( System.Int32 )cmd.Parameters[ "@RETURN_VALUE" ].Value;
                if( iRet != 0 )
                {
                    DevExpress.XtraEditors.XtraMessageBox.Show(  "Ошибка поиска зарезервированной суммы.\n\nТекст ошибки: " + 
                        ( System.String )cmd.Parameters[ "@ERROR_MES" ].Value, "Ошибка",
                        System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
                } // if( iRet != 0 )
                else
                {
                    moneyRet = System.Convert.ToDouble( cmd.Parameters[ "@RESERV_MONEY" ].Value );
                }
            }
            catch( System.Exception f )
            {
                DevExpress.XtraEditors.XtraMessageBox.Show( 
                "Ошибка поиска зарезервированной суммы.\n\nТекст ошибки: " + 
                    ( System.String )cmd.Parameters[ "@ERROR_MES" ].Value + "\n\n" + f.Message, "Внимание",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
            }

            return moneyRet;
        }
        #endregion

        #region Снятие суммы с резерва ID = 1 
        /// <summary>
        /// Снимает сумму с резерва для бюджетного документа
        /// </summary>
        /// <param name="objBudgetDoc">бюджетный документ</param>
        /// <param name="dtDate">дата операции</param>
        /// <param name="objProfile">профайл</param>
        /// <param name="cmd">SQL-команда</param>
        /// <returns>true - успешно; false - ошибка</returns>
        public static System.Boolean DeResrveMoney( CBudgetDoc objBudgetDoc, System.DateTime dtDate,
            UniXP.Common.CProfile objProfile, System.Data.SqlClient.SqlCommand cmd, System.Int32 iUserID )
        {
            System.Boolean bRet = false;
            if( cmd == null ) { return bRet; }

            try
            {
                if ((objBudgetDoc.DocState == null) && (objProfile.GetClientsRight().GetState( ERP_Budget.Global.Consts.strDRAccountant ) != true))
                {
                    DevExpress.XtraEditors.XtraMessageBox.Show( 
                    "У документа не определено состояние.\nСнять сумму с резерва нельзя.", "Внимание",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning );
                    return bRet;
                }
                // сперва проверим, был ли резерв средств, если да, то его нужно снять
                System.Double moneyReserv = GetResrveMoney( objBudgetDoc, objProfile, cmd );
                if( moneyReserv == -999999 )
                {
                    DevExpress.XtraEditors.XtraMessageBox.Show( 
                    "Не удалось проверить наличие резерва по заявке.", "Внимание",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning );
                    return bRet;
                }
                else
                {
                    if( moneyReserv == 0 )
                    {
                        DevExpress.XtraEditors.XtraMessageBox.Show( 
                        "По данной заявке найденная сумма резерва равна нулю.\nСнять сумму с резерва не представляется возможным.", "Внимание",
                        System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning );
                        return bRet;
                    }
                }

                cmd.Parameters.Clear();
                cmd.CommandText = System.String.Format( "[{0}].[dbo].[sp_AccTrnDeReservMoney]", objProfile.GetOptionsDllDBName() );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ( ( System.Byte )( 0 ) ), ( ( System.Byte )( 0 ) ), "", System.Data.DataRowVersion.Current, null ) );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@RESERV_MONEY", System.Data.SqlDbType.Money, 8, System.Data.ParameterDirection.Output, false, ( ( System.Byte )( 0 ) ), ( ( System.Byte )( 0 ) ), "", System.Data.DataRowVersion.Current, null ) );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@BUDGETDOC_GUID_ID", System.Data.SqlDbType.UniqueIdentifier ) );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@ACCOUNTTRANSN_DATE", System.Data.SqlDbType.DateTime ) );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@USERS_ID", System.Data.SqlDbType.Int, 4 ) );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@ERROR_NUM", System.Data.SqlDbType.Int, 8, System.Data.ParameterDirection.Output, false, ( ( System.Byte )( 0 ) ), ( ( System.Byte )( 0 ) ), "", System.Data.DataRowVersion.Current, null ) );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@ERROR_MES", System.Data.SqlDbType.NVarChar, 4000 ) );
                cmd.Parameters[ "@ERROR_MES" ].Direction = System.Data.ParameterDirection.Output;
                cmd.Parameters[ "@BUDGETDOC_GUID_ID" ].Value = objBudgetDoc.uuidID;
                cmd.Parameters[ "@ACCOUNTTRANSN_DATE" ].Value = dtDate;
                cmd.Parameters[ "@USERS_ID" ].Value = iUserID;

                cmd.ExecuteNonQuery();
                System.Int32 iRet = ( System.Int32 )cmd.Parameters[ "@RETURN_VALUE" ].Value;
                if( iRet != 0 )
                {
                    DevExpress.XtraEditors.XtraMessageBox.Show(  "Ошибка снятия суммы с резерва.\n\nТекст ошибки: " + 
                        ( System.String )cmd.Parameters[ "@ERROR_MES" ].Value, "Ошибка",
                       System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
                } 

                bRet = ( iRet == 0 );
            }
            catch( System.Exception f )
            {
                DevExpress.XtraEditors.XtraMessageBox.Show( 
                "Не удалось снять с резерва сумму.\n\nТекст ошибки: " + f.Message, "Внимание",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
            }

            return bRet;
        }
        #endregion

        #region Оплата бюджетного документа  ID = 2 
        /// <summary>
        /// Оплачивает бюджетный документ
        /// </summary>
        /// <param name="objBudgetDoc">бюджетный документ</param>
        /// <param name="dtDate">дата операции</param>
        /// <param name="objProfile">профайл</param>
        /// <param name="cmd">SQL-команда</param>
        /// <returns>true - успешно; false - ошибка</returns>
        public static System.Boolean PayMoney( CBudgetDoc objBudgetDoc, System.DateTime dtDate,
            UniXP.Common.CProfile objProfile, System.Data.SqlClient.SqlCommand cmd, System.Int32 iUserID, 
            double PayMoney, double FactPayMoney,
            System.Guid FactPayCurrency_Guid )
        {
            System.Boolean bRet = false;
            if ((objBudgetDoc.DocState == null) && (objProfile.GetClientsRight().GetState(ERP_Budget.Global.Consts.strDRAccountant) != true))
            {
                DevExpress.XtraEditors.XtraMessageBox.Show( 
                "У документа не определено состояние.\nОплатить его нельзя.", "Внимание",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning );
                return bRet;
            }
            if( cmd == null ) { return bRet; }

            try
            {
                cmd.Parameters.Clear();
                cmd.CommandText = System.String.Format( "[{0}].[dbo].[sp_AccTrnPayDocument]", objProfile.GetOptionsDllDBName() );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ( ( System.Byte )( 0 ) ), ( ( System.Byte )( 0 ) ), "", System.Data.DataRowVersion.Current, null ) );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@PAY_MONEY_IN", System.Data.SqlDbType.Money ) );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@PAY_MONEY", System.Data.SqlDbType.Money, 8, System.Data.ParameterDirection.Output, false, ( ( System.Byte )( 0 ) ), ( ( System.Byte )( 0 ) ), "", System.Data.DataRowVersion.Current, null ) );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@BUDGETDOC_GUID_ID", System.Data.SqlDbType.UniqueIdentifier ) );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@ACCOUNTTRANSN_DATE", System.Data.SqlDbType.DateTime ) );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@USERS_ID", System.Data.SqlDbType.Int, 4 ) );
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ERROR_NUM", System.Data.SqlDbType.Int, 8, System.Data.ParameterDirection.Output, false, ((System.Byte)(0)), ((System.Byte)(0)), "", System.Data.DataRowVersion.Current, null));
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@ERROR_MES", System.Data.SqlDbType.NVarChar, 4000 ) );
                cmd.Parameters[ "@ERROR_MES" ].Direction = System.Data.ParameterDirection.Output;
                cmd.Parameters[ "@BUDGETDOC_GUID_ID" ].Value = objBudgetDoc.uuidID;
                cmd.Parameters[ "@ACCOUNTTRANSN_DATE" ].Value = dtDate;
                cmd.Parameters[ "@USERS_ID" ].Value = iUserID;

                if (PayMoney == 0)
                {
                    cmd.Parameters["@PAY_MONEY_IN"].IsNullable = true;
                    cmd.Parameters["@PAY_MONEY_IN"].Value = null;
                }
                else
                {
                    cmd.Parameters["@PAY_MONEY_IN"].IsNullable = false;
                    cmd.Parameters["@PAY_MONEY_IN"].Value = PayMoney;
                }

                if (FactPayMoney != 0)
                {
                    cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@PAY_MONEY_IN_FACT", System.Data.SqlDbType.Money));
                    cmd.Parameters["@PAY_MONEY_IN_FACT"].Value = FactPayMoney;
                }

                if (FactPayCurrency_Guid.CompareTo( System.Guid.Empty ) != 0)
                {
                    cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@PAY_CURRENCY_GUID", System.Data.SqlDbType.UniqueIdentifier));
                    cmd.Parameters["@PAY_CURRENCY_GUID"].Value = FactPayCurrency_Guid;
                }

                cmd.ExecuteNonQuery();
                System.Int32 iRet = ( System.Int32 )cmd.Parameters[ "@RETURN_VALUE" ].Value;
                if( iRet != 0 )
                {
                    DevExpress.XtraEditors.XtraMessageBox.Show(  "Ошибка оплаты документа.\n\nТекст ошибки: " + 
                        ( System.String )cmd.Parameters[ "@ERROR_MES" ].Value, "Ошибка",
                       System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
                } 

                bRet = ( iRet == 0 );
            }
            catch( System.Exception f )
            {
                DevExpress.XtraEditors.XtraMessageBox.Show( 
                "Не удалось оплатить документ.\n\nТекст ошибки: " + f.Message, "Внимание",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
            }

            return bRet;
        }
        #endregion

        #region Подтверждение ноты ID = 3 
        /// <summary>
        /// Подтверждает ноту
        /// </summary>
        /// <param name="objBudgetDoc">бюджетный документ</param>
        /// <param name="dtDate">дата операции</param>
        /// <param name="objProfile">профайл</param>
        /// <param name="cmd">SQL-команда</param>
        /// <returns>true - успешно; false - ошибка</returns>
        public static System.Boolean AcceptNote( CBudgetDoc objBudgetDoc, System.DateTime dtDate,
            UniXP.Common.CProfile objProfile, System.Data.SqlClient.SqlCommand cmd, System.Int32 iUserID )
        {
            System.Boolean bRet = false;
            if( cmd == null ) { return bRet; }

            try
            {
                cmd.Parameters.Clear();
                if (objBudgetDoc.IsResetBudgetItem == true)
                {
                    cmd.CommandText = System.String.Format("[{0}].[dbo].[sp_AccTrnAcceptNoteUp]", objProfile.GetOptionsDllDBName());
                } 
                else
                {
                    cmd.CommandText = System.String.Format("[{0}].[dbo].[sp_AccTrnAcceptNote]", objProfile.GetOptionsDllDBName());
                }
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ( ( System.Byte )( 0 ) ), ( ( System.Byte )( 0 ) ), "", System.Data.DataRowVersion.Current, null ) );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@RESERV_MONEY", System.Data.SqlDbType.Money, 8, System.Data.ParameterDirection.Output, false, ( ( System.Byte )( 0 ) ), ( ( System.Byte )( 0 ) ), "", System.Data.DataRowVersion.Current, null ) );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@BUDGETDOC_GUID_ID", System.Data.SqlDbType.UniqueIdentifier ) );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@ACCOUNTTRANSN_DATE", System.Data.SqlDbType.DateTime ) );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@USERS_ID", System.Data.SqlDbType.Int, 4 ) );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@ERROR_NUM", System.Data.SqlDbType.Int, 8, System.Data.ParameterDirection.Output, false, ( ( System.Byte )( 0 ) ), ( ( System.Byte )( 0 ) ), "", System.Data.DataRowVersion.Current, null ) );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@ERROR_MES", System.Data.SqlDbType.NVarChar, 4000 ) );
                cmd.Parameters[ "@ERROR_MES" ].Direction = System.Data.ParameterDirection.Output;
                cmd.Parameters[ "@BUDGETDOC_GUID_ID" ].Value = objBudgetDoc.uuidID;
                cmd.Parameters[ "@ACCOUNTTRANSN_DATE" ].Value = dtDate;
                cmd.Parameters[ "@USERS_ID" ].Value = iUserID;

                cmd.ExecuteNonQuery();
                System.Int32 iRet = ( System.Int32 )cmd.Parameters[ "@RETURN_VALUE" ].Value;
                if( iRet != 0 )
                {
                    DevExpress.XtraEditors.XtraMessageBox.Show(  "Ошибка подтверждения ноты.\n\nТекст ошибки: " + 
                        ( System.String )cmd.Parameters[ "@ERROR_MES" ].Value, "Ошибка",
                       System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
                } 

                bRet = ( iRet == 0 );
            }
            catch( System.Exception f )
            {
                DevExpress.XtraEditors.XtraMessageBox.Show( 
                "Ошибка подтверждения ноты.\n\nТекст ошибки: " + f.Message, "Внимание",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
            }

            return bRet;
        }
        /// <summary>
        /// Возвращает размер суммы, на которую была увеличена статья бюджета для ноты
        /// </summary>
        /// <param name="objBudgetDoc">бюджетный документ (нота)</param>
        /// <param name="objProfile">профайл</param>
        /// <returns>true - успешно; false - ошибка</returns>
        public static System.Double GetAcceptMoney( CBudgetDoc objBudgetDoc,
            UniXP.Common.CProfile objProfile, System.Data.SqlClient.SqlCommand cmd )
        {
            System.Double moneyRet = -99999;
            if( cmd == null ) { return moneyRet; }
            try
            {
                cmd.Parameters.Clear();
                cmd.CommandText = System.String.Format( "[{0}].[dbo].[sp_AccTrnCheckAcceptNote]", objProfile.GetOptionsDllDBName() );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ( ( System.Byte )( 0 ) ), ( ( System.Byte )( 0 ) ), "", System.Data.DataRowVersion.Current, null ) );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@RESERV_MONEY", System.Data.SqlDbType.Money, 8, System.Data.ParameterDirection.Output, false, ( ( System.Byte )( 0 ) ), ( ( System.Byte )( 0 ) ), "", System.Data.DataRowVersion.Current, null ) );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@BUDGETDOC_GUID_ID", System.Data.SqlDbType.UniqueIdentifier ) );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@ERROR_NUM", System.Data.SqlDbType.Int, 8, System.Data.ParameterDirection.Output, false, ( ( System.Byte )( 0 ) ), ( ( System.Byte )( 0 ) ), "", System.Data.DataRowVersion.Current, null ) );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@ERROR_MES", System.Data.SqlDbType.NVarChar, 4000 ) );
                cmd.Parameters[ "@ERROR_MES" ].Direction = System.Data.ParameterDirection.Output;
                cmd.Parameters[ "@BUDGETDOC_GUID_ID" ].Value = objBudgetDoc.uuidID;

                cmd.ExecuteNonQuery();
                System.Int32 iRet = ( System.Int32 )cmd.Parameters[ "@RETURN_VALUE" ].Value;
                if( iRet != 0 )
                {
                    DevExpress.XtraEditors.XtraMessageBox.Show(  "Ошибка поиска подтвержденной суммы.\n\nТекст ошибки: " + 
                        ( System.String )cmd.Parameters[ "@ERROR_MES" ].Value, "Ошибка",
                       System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
                } 
                else
                {
                    moneyRet = System.Convert.ToDouble( cmd.Parameters[ "@RESERV_MONEY" ].Value );
                }
            }
            catch( System.Exception f )
            {
                DevExpress.XtraEditors.XtraMessageBox.Show( 
                "Ошибка поиска подтвержденной суммы.\n\nТекст ошибки: " + f.Message, "Внимание",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
            }

            return moneyRet;
        }

        #endregion

        #region Отклонение ноты ID = 4 
        /// <summary>
        /// Отклоняет ноту
        /// </summary>
        /// <param name="objBudgetDoc">бюджетный документ</param>
        /// <param name="dtDate">дата операции</param>
        /// <param name="objProfile">профайл</param>
        /// <param name="cmd">SQL-команда</param>
        /// <returns>true - успешно; false - ошибка</returns>
        public static System.Boolean DeAcceptNote( CBudgetDoc objBudgetDoc, System.DateTime dtDate,
            UniXP.Common.CProfile objProfile, System.Data.SqlClient.SqlCommand cmd, System.Int32 iUserID )
        {
            System.Boolean bRet = false;
            if( cmd == null ) { return bRet; }

            try
            {
                if( objBudgetDoc.DocState == null )
                {
                    DevExpress.XtraEditors.XtraMessageBox.Show( 
                    "У документа не определено состояние.\nСнять сумму с резерва нельзя.", "Внимание",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning );
                    return bRet;
                }
                // сперва проверим, производились резерв средств и увеличение разрешенной суммы, если да, то нужно произвести обратные действия
                System.Double moneyAccept = GetAcceptMoney( objBudgetDoc, objProfile, cmd );
                if( moneyAccept == -99999 )
                {
                    DevExpress.XtraEditors.XtraMessageBox.Show( 
                    "Не удалось проверить, производилось ли увеличение \nразрешенной суммы по статье бюджета.", "Внимание",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning );
                    return bRet;
                }
                else
                {
                    if( moneyAccept == 0 )
                    {
                        // по ноте резерва средств не было, поэтому и снимать с резерва нечего
                        bRet = true;

                        //DevExpress.XtraEditors.XtraMessageBox.Show( 
                        //"По данной ноте найденная сумма резерва равна нулю.\nСнять сумму с резерва не представляется возможным.", "Внимание",
                        //System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning );
                        return bRet;
                    }
                }

                cmd.Parameters.Clear();
                cmd.CommandText = System.String.Format( "[{0}].[dbo].[sp_AccTrnDeAcceptNote]", objProfile.GetOptionsDllDBName() );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ( ( System.Byte )( 0 ) ), ( ( System.Byte )( 0 ) ), "", System.Data.DataRowVersion.Current, null ) );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@RESERV_MONEY", System.Data.SqlDbType.Money, 8, System.Data.ParameterDirection.Output, false, ( ( System.Byte )( 0 ) ), ( ( System.Byte )( 0 ) ), "", System.Data.DataRowVersion.Current, null ) );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@BUDGETDOC_GUID_ID", System.Data.SqlDbType.UniqueIdentifier ) );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@ACCOUNTTRANSN_DATE", System.Data.SqlDbType.DateTime ) );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@USERS_ID", System.Data.SqlDbType.Int, 4 ) );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@ERROR_NUM", System.Data.SqlDbType.Int, 8, System.Data.ParameterDirection.Output, false, ( ( System.Byte )( 0 ) ), ( ( System.Byte )( 0 ) ), "", System.Data.DataRowVersion.Current, null ) );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@ERROR_MES", System.Data.SqlDbType.NVarChar, 4000 ) );
                cmd.Parameters[ "@ERROR_MES" ].Direction = System.Data.ParameterDirection.Output;
                cmd.Parameters[ "@BUDGETDOC_GUID_ID" ].Value = objBudgetDoc.uuidID;
                cmd.Parameters[ "@ACCOUNTTRANSN_DATE" ].Value = dtDate;
                cmd.Parameters[ "@USERS_ID" ].Value = iUserID;

                cmd.ExecuteNonQuery();
                System.Int32 iRet = ( System.Int32 )cmd.Parameters[ "@RETURN_VALUE" ].Value;
                if( iRet != 0 )
                {
                    DevExpress.XtraEditors.XtraMessageBox.Show(  "Ошибка отклонения ноты.\n\nТекст ошибки: " + 
                        ( System.String )cmd.Parameters[ "@ERROR_MES" ].Value, "Ошибка",
                       System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
                } 

                bRet = ( iRet == 0 );
            }
            catch( System.Exception f )
            {
                DevExpress.XtraEditors.XtraMessageBox.Show( 
                "Ошибка отклонения ноты.\n\nТекст ошибки: " + f.Message, "Внимание",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
            }

            return bRet;
        }
        #endregion

        #region Возврат средств ID = 5 
        /// <summary>
        /// Возвращает сумму оплаты по документу с заданным уникальным идентификатором
        /// </summary>
        /// <param name="uuidBudgetDocID">уи оплаченного документа</param>
        /// <param name="objProfile">профайл</param>
        /// <param name="cmd">SQL-команда</param>
        /// <returns>сумма оплаты по документу</returns>
        public static System.Double GetPayMoney( System.Guid uuidBudgetDocID,
            UniXP.Common.CProfile objProfile, System.Data.SqlClient.SqlCommand cmd )
        {
            System.Double moneyRet = 0;
            if( cmd == null ) { return moneyRet; }
            try
            {
                cmd.Parameters.Clear();
                cmd.CommandText = System.String.Format( "[{0}].[dbo].[sp_AccTrnCheckPayMoney]", objProfile.GetOptionsDllDBName() );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ( ( System.Byte )( 0 ) ), ( ( System.Byte )( 0 ) ), "", System.Data.DataRowVersion.Current, null ) );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@PAY_MONEY", System.Data.SqlDbType.Money, 8, System.Data.ParameterDirection.Output, false, ( ( System.Byte )( 0 ) ), ( ( System.Byte )( 0 ) ), "", System.Data.DataRowVersion.Current, null ) );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@BUDGETDOC_GUID_ID", System.Data.SqlDbType.UniqueIdentifier ) );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@ERROR_NUM", System.Data.SqlDbType.Int, 8, System.Data.ParameterDirection.Output, false, ( ( System.Byte )( 0 ) ), ( ( System.Byte )( 0 ) ), "", System.Data.DataRowVersion.Current, null ) );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@ERROR_MES", System.Data.SqlDbType.NVarChar, 4000 ) );
                cmd.Parameters[ "@ERROR_MES" ].Direction = System.Data.ParameterDirection.Output;
                cmd.Parameters[ "@BUDGETDOC_GUID_ID" ].Value = uuidBudgetDocID;

                cmd.ExecuteNonQuery();
                System.Int32 iRet = ( System.Int32 )cmd.Parameters[ "@RETURN_VALUE" ].Value;
                if( iRet == 0 )
                {
                    moneyRet = System.Convert.ToDouble( cmd.Parameters[ "@PAY_MONEY" ].Value );
                } // if( iRet != 0 )
                else
                {
                    DevExpress.XtraEditors.XtraMessageBox.Show(  "Ошибка поиска оплаченной суммы.\n\nТекст ошибки: " + 
                        ( System.String )cmd.Parameters[ "@ERROR_MES" ].Value, "Ошибка",
                       System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
                }
            }
            catch( System.Exception f )
            {
                DevExpress.XtraEditors.XtraMessageBox.Show( 
                "Ошибка поиска оплаченной суммы.\n\nТекст ошибки: " + f.Message, "Внимание",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
            }

            return moneyRet;
        }

        /// <summary>
        /// Возвращает сумму оплаты по документу с заданным уникальным идентификатором
        /// </summary>
        /// <param name="uuidBudgetDocID">уи оплаченного документа</param>
        /// <param name="objProfile">профайл</param>
        /// <returns>сумма оплаты по документу</returns>
        public static System.Double GetPayMoney( System.Guid uuidBudgetDocID, UniXP.Common.CProfile objProfile )
        {
            System.Double moneyRet = 0;
            System.Data.SqlClient.SqlConnection DBConnection = objProfile.GetDBSource();
            if( DBConnection == null ) { return moneyRet; }

            try
            {
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
                cmd.Connection = DBConnection;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = System.String.Format( "[{0}].[dbo].[sp_AccTrnCheckPayMoney]", objProfile.GetOptionsDllDBName() );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ( ( System.Byte )( 0 ) ), ( ( System.Byte )( 0 ) ), "", System.Data.DataRowVersion.Current, null ) );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@PAY_MONEY", System.Data.SqlDbType.Money, 8, System.Data.ParameterDirection.Output, false, ( ( System.Byte )( 0 ) ), ( ( System.Byte )( 0 ) ), "", System.Data.DataRowVersion.Current, null ) );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@BUDGETDOC_GUID_ID", System.Data.SqlDbType.UniqueIdentifier ) );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@ERROR_NUM", System.Data.SqlDbType.Int, 8, System.Data.ParameterDirection.Output, false, ( ( System.Byte )( 0 ) ), ( ( System.Byte )( 0 ) ), "", System.Data.DataRowVersion.Current, null ) );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@ERROR_MES", System.Data.SqlDbType.NVarChar, 4000 ) );
                cmd.Parameters[ "@ERROR_MES" ].Direction = System.Data.ParameterDirection.Output;
                cmd.Parameters[ "@BUDGETDOC_GUID_ID" ].Value = uuidBudgetDocID;

                cmd.ExecuteNonQuery();
                System.Int32 iRet = ( System.Int32 )cmd.Parameters[ "@RETURN_VALUE" ].Value;
                if( iRet == 0 )
                {
                    moneyRet = System.Convert.ToDouble( cmd.Parameters[ "@PAY_MONEY" ].Value );
                } // if( iRet != 0 )
                else
                {
                    DevExpress.XtraEditors.XtraMessageBox.Show(  "Ошибка поиска оплаченной суммы.\n\nТекст ошибки: " + 
                        ( System.String )cmd.Parameters[ "@ERROR_MES" ].Value, "Ошибка",
                       System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
                }

            }
            catch( System.Exception f )
            {
                DevExpress.XtraEditors.XtraMessageBox.Show( 
                "Ошибка поиска оплаченной суммы.\n\nТекст ошибки: " + f.Message, "Внимание",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
            }
            finally
            {
                DBConnection.Close();
            }

            return moneyRet;
        }

        /// <summary>
        /// Возврат средств
        /// </summary>
        /// <param name="uuidBudgetDocSrc">уи оплаченного бюджетного документа</param>
        /// <param name="objBudgetDoc">бюджетный документ (корректирующая заявка)</param>
        /// <param name="dtDate">дата операции</param>
        /// <param name="objProfile">профайл</param>
        /// <param name="cmd">SQL-команда</param>
        /// <returns>true - успешно; false - ошибка</returns>
        public static System.Boolean DePayDoc( System.Guid uuidBudgetDocSrc, CBudgetDoc objBudgetDoc, 
            System.DateTime dtDate, UniXP.Common.CProfile objProfile, System.Data.SqlClient.SqlCommand cmd, 
            System.Int32 iUserID )
        {
            System.Boolean bRet = false;
            if( cmd == null ) { return bRet; }

            try
            {
                //if( objBudgetDoc.DocState == null )
                //{
                //    DevExpress.XtraEditors.XtraMessageBox.Show( 
                //    "У документа не определено состояние.\nВозврат средст отменен.", "Внимание",
                //    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning );
                //    return bRet;
                //}
                // сперва проверим, производилась ли оплата
                if (uuidBudgetDocSrc.CompareTo(System.Guid.Empty) != 0)
                {
                    System.Double moneyPay = GetPayMoney(uuidBudgetDocSrc, objProfile, cmd);
                    if (moneyPay == 0)
                    {
                        DevExpress.XtraEditors.XtraMessageBox.Show(
                        "Найденная сумма оплаты равна нулю.\nВозврат средст отменен.", "Внимание",
                        System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);
                        return bRet;
                    }
                }
                cmd.Parameters.Clear();
                cmd.CommandText = System.String.Format( "[{0}].[dbo].[sp_AccTrnDePayDocument]", objProfile.GetOptionsDllDBName() );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ( ( System.Byte )( 0 ) ), ( ( System.Byte )( 0 ) ), "", System.Data.DataRowVersion.Current, null ) );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@DEPAY_MONEY", System.Data.SqlDbType.Money, 8, System.Data.ParameterDirection.Output, false, ( ( System.Byte )( 0 ) ), ( ( System.Byte )( 0 ) ), "", System.Data.DataRowVersion.Current, null ) );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@PAY_MONEY", System.Data.SqlDbType.Money, 8, System.Data.ParameterDirection.Output, false, ( ( System.Byte )( 0 ) ), ( ( System.Byte )( 0 ) ), "", System.Data.DataRowVersion.Current, null ) );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@BACK_MONEY", System.Data.SqlDbType.Money, 8, System.Data.ParameterDirection.Output, false, ( ( System.Byte )( 0 ) ), ( ( System.Byte )( 0 ) ), "", System.Data.DataRowVersion.Current, null ) );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@BUDGETDOC_GUID_ID", System.Data.SqlDbType.UniqueIdentifier ) );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@BUDGETDOC_SRC_GUID_ID", System.Data.SqlDbType.UniqueIdentifier ) );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@ACCOUNTTRANSN_DATE", System.Data.SqlDbType.DateTime ) );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@USERS_ID", System.Data.SqlDbType.Int, 4 ) );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@ERROR_NUM", System.Data.SqlDbType.Int, 8, System.Data.ParameterDirection.Output, false, ( ( System.Byte )( 0 ) ), ( ( System.Byte )( 0 ) ), "", System.Data.DataRowVersion.Current, null ) );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@ERROR_MES", System.Data.SqlDbType.NVarChar, 4000 ) );
                cmd.Parameters[ "@ERROR_MES" ].Direction = System.Data.ParameterDirection.Output;
                cmd.Parameters[ "@BUDGETDOC_GUID_ID" ].Value = objBudgetDoc.uuidID;
                if (uuidBudgetDocSrc.CompareTo(System.Guid.Empty) == 0)
                {
                    cmd.Parameters["@BUDGETDOC_SRC_GUID_ID"].IsNullable = true;
                    cmd.Parameters["@BUDGETDOC_SRC_GUID_ID"].Value = null;
                }
                else
                {
                    cmd.Parameters["@BUDGETDOC_SRC_GUID_ID"].Value = uuidBudgetDocSrc;
                }
                cmd.Parameters[ "@ACCOUNTTRANSN_DATE" ].Value = dtDate;
                cmd.Parameters[ "@USERS_ID" ].Value = iUserID;

                cmd.ExecuteNonQuery();
                System.Int32 iRet = ( System.Int32 )cmd.Parameters[ "@RETURN_VALUE" ].Value;
                if( iRet != 0 )
                {
                    DevExpress.XtraEditors.XtraMessageBox.Show(  "Ошибка возврата средств.\n\nТекст ошибки: " + 
                        ( System.String )cmd.Parameters[ "@ERROR_MES" ].Value, "Ошибка",
                       System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
                } 

                bRet = ( iRet == 0 );
            }
            catch( System.Exception f )
            {
                DevExpress.XtraEditors.XtraMessageBox.Show( 
                "Ошибка возврата средств.\n\nТекст ошибки: " + f.Message, "Внимание",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
            }

            return bRet;
        }

        #endregion 
    }

    /// <summary>
    /// список действий, которые можно совершить над бюджетным документом
    /// </summary>
    public enum enumAccTrnEvent
    {
        ReservMoney=0,      // 0 резерв средств по заявке
        DeReservMoney,      // 1 снятие с резерва средств по заявке
        PayMoney,           // 2 оплатить
        AcceptNote,         // 3 подтвердить ноту
        DeAcceptNote,       // 4 отклонить ноту
        DePayMoney,         // 5 возврат средств
        RateDiff            // 6 курсовая разница
    };

}
