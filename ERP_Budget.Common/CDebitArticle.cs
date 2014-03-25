using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace ERP_Budget.Common
{
    /// <summary>
    /// Класс "Статья расходов"
    /// </summary>
    public class CDebitArticle : IBaseListItem
    {
        #region Переменный, Свойства, Константы
        /// <summary>
        /// УИ родительской статьи расходов
        /// </summary>
        private System.Guid m_uuidParentID;
        /// <summary>
        /// УИ родительской статьи расходов
        /// </summary>
        public System.Guid ParentID
        {
            get { return m_uuidParentID; }
            set { m_uuidParentID = value; }
        }
        /// <summary>
        /// Описание статьи расходов
        /// </summary>
        private string m_strArticleDescription;
        /// <summary>
        /// Описание статьи расходов
        /// </summary>
        public string ArticleDescription
        {
            get { return m_strArticleDescription; }
            set { m_strArticleDescription = value; }
        }
        /// <summary>
        /// Номер статьи расходов
        /// </summary>
        private string m_strArticleNum;
        /// <summary>
        /// Номер статьи расходов
        /// </summary>
        public string ArticleNum
        {
            get { return m_strArticleNum; }
            set { m_strArticleNum = value; }
        }
        /// <summary>
        /// Номер по порядке в ветке
        /// </summary>
        private System.Int32 m_iArticleID;
        /// <summary>
        /// Номер по порядке в ветке
        /// </summary>
        public System.Int32 ArticleID
        {
            get { return m_iArticleID; }
            set { m_iArticleID = value; }
        }
        /// <summary>
        /// Признак "только для чтения"
        /// </summary>
        private System.Boolean m_bReadOnly;
        /// <summary>
        /// Признак "только для чтения"
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
        /// Признак связи с несколькими бюджетными подразделениями
        /// </summary>
        private System.Boolean m_bMultiBudgetDept;
        /// <summary>
        /// Признак связи с несколькими бюджетными подразделениями
        /// </summary>
        public System.Boolean MultiBudgetDept
        {
            get { return m_bMultiBudgetDept; }
            set { m_bMultiBudgetDept = value; }
        }
        /// <summary>
        /// Список бюджетных подразделений
        /// </summary>
        private List<CBudgetDep> m_objBudgetDepList;
        /// <summary>
        /// список бюджетных подразделений
        /// </summary>
        public List<ERP_Budget.Common.CBudgetDep> BudgetDepList
        {
            get { return m_objBudgetDepList; }
            set { m_objBudgetDepList = value; }
        }
        /// <summary>
        /// Признак "Переходящий остаток"
        /// </summary>
        private System.Boolean m_bTransprtRest;
        /// <summary>
        /// Признак "Переходящий остаток"
        /// </summary>
        public System.Boolean TransprtRest
        {
            get { return m_bTransprtRest; }
            set { m_bTransprtRest = value; }
        }
        /// <summary>
        /// Признак "Запрет нецелевого использования"
        /// </summary>
        private System.Boolean m_bDontChange;
        /// <summary>
        /// Признак "Запрет нецелевого использования"
        /// </summary>
        public System.Boolean DontChange
        {
            get { return m_bDontChange; }
            set { m_bDontChange = value; }
        }
        /// <summary>
        /// Полное наименование статьи (номер + название)
        /// </summary>
        public System.String ArticleFullName
        {
            get { return (String.Format("{0} {1}", m_strArticleNum, Name)); }
        }
        /// <summary>
        /// Финансовый год
        /// </summary>
        private System.Int32 m_iYear;
        /// <summary>
        /// Финансовый год
        /// </summary>
        public System.Int32 FinancislYear
        {
            get { return m_iYear; }
            set { m_iYear = value; }
        }
        /// <summary>
        /// счёт
        /// </summary>
        public CAccountPlan AccountPlan { get; set; }
        /// <summary>
        /// тип расходов
        /// </summary>
        public CBudgetExpenseType BudgetExpenseType { get; set; }
        #endregion

        #region Конструкторы
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
        /// Создает объект "Статья расходов" на основе другого объекта
        /// </summary>
        /// <param name="objDebitArticleSrc">копируемый объект "Статья расходов"</param>
        /// <returns>объект "Статья расходов"</returns>
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
                "Не удалось скопировать свойства объекта \"Статья расходов\".\n\nТекст ошибки: " + f.Message, "Внимание",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
            }
            return objDebitArticle;
        }

        #endregion

        #region Список статей расходов
        /// <summary>
        /// Возвращает список статей расходов
        /// </summary>
        /// <param name="objProfile">профайл</param>
        /// <returns>список классов статей расходов</returns>
        public static List<CDebitArticle> GetDebitArticleList( UniXP.Common.CProfile objProfile )
        {

            List<CDebitArticle> objList = new List<CDebitArticle>();
            System.Data.SqlClient.SqlConnection DBConnection = objProfile.GetDBSource();
            if( DBConnection == null ) { return objList; }

            try
            {
                // соединение с БД получено, прописываем команду на выборку данных
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
                "Не удалось получить список статей расходов.\n\nТекст ошибки: " + f.Message, "Внимание",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
            }
			finally // очищаем занимаемые ресурсы
            {
                DBConnection.Close();
            }
            return objList;
        }
        /// <summary>
        /// Возвращает список статей расходов, удовлетворяющих заданным параметрам
        /// </summary>
        /// <param name="objProfile">профайл</param>
        /// <param name="BudgetDep_Guid">УИ бюджетного подразделения</param>
        /// <param name="DebitArticle_Num">№ статьи расходов</param>
        /// <param name="DebitArticle_Name">Наименование статьи расходов</param>
        /// <param name="PeriodBeginDate">Период действия статьи расходов</param>
        /// <param name="strErr">текст ошибки</param>
        /// <returns>список объектов класса "CDebitArticle"</returns>
        public static List<CDebitArticle> GetDebitArticleList(UniXP.Common.CProfile objProfile, System.Guid BudgetDep_Guid,
            System.String DebitArticle_Num, System.String DebitArticle_Name, System.DateTime PeriodBeginDate, ref System.String strErr )
        {

            List<CDebitArticle> objList = new List<CDebitArticle>();
            System.Data.SqlClient.SqlConnection DBConnection = objProfile.GetDBSource();
            if (DBConnection == null) 
            { 
                strErr += ("\nНе удалось получить подключение к БД.");
                return objList; 
            }

            try
            {
                // соединение с БД получено, прописываем команду на выборку данных
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
                strErr += ("\nНе удалось получить список статей расходов.\n\nТекст ошибки: " + f.Message);
            }
			finally // очищаем занимаемые ресурсы
            {
                DBConnection.Close();
            }
            return objList;
        }
        
        
        #endregion

        #region Построение дерева статей расходов
        /// <summary>
        /// Обновляет дерево статей расходов 
        /// </summary>
        /// <param name="objProfile">профайл</param>
        /// <param name="objTreeList">дерево статей расходов</param>
        /// <returns>true - успешное завершение;false - ошибка</returns>
        public static System.Boolean LoadDebitArticleList( UniXP.Common.CProfile objProfile,
            DevExpress.XtraTreeList.TreeList objTreeList, DevExpress.XtraTreeList.Columns.TreeListColumn objColumnYear )
        {
            System.Boolean bRet = false;
            if( objTreeList == null )
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(  "Объект дерево не определен!", "Внимание",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning );
                return bRet;
            }
            objTreeList.ClearNodes();

            // подключаемся к БД
            System.Data.SqlClient.SqlConnection DBConnection = objProfile.GetDBSource();
            if( DBConnection == null )
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(  "Отсутствует соединение с БД.", "Внимание",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning );
                return bRet;
            }

            try
            {
                // соединение с БД получено, прописываем команду на выборку данных
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand() { Connection = DBConnection, CommandType = System.Data.CommandType.StoredProcedure };

                // запрашиваем список родительских статей расходов
                List<CDebitArticle> objDebitArticleParentList = GetDebitArticleParentList( objProfile, cmd );
                if( objDebitArticleParentList.Count > 0 )
                {
                    bRet = true;
                    // загрузим список служб для каждой статьи расходов
                    foreach( CDebitArticle objDebitArticleParent in objDebitArticleParentList )
                    {
                        bRet = LoadDebitArticleBudgetDepList( objProfile, cmd, objDebitArticleParent );
                        if( bRet == false ) { break; }
                    }
                    if( bRet == false ) { objTreeList.Nodes.Clear(); }
                    else
                    {
                        //теперь построим узлы верхнего уровня
                        DevExpress.XtraTreeList.Nodes.TreeListNode objNodeYear = null;
                        foreach (CDebitArticle objDebitArticle in objDebitArticleParentList)
                        {
                            //добавляем узел в дерево
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

                        // для каждого узла со статьей расходов строим дерево дочерних подстатей
                        foreach (DevExpress.XtraTreeList.Nodes.TreeListNode objYear in objTreeList.Nodes)
                        {
                            foreach (DevExpress.XtraTreeList.Nodes.TreeListNode objNodeParent in objYear.Nodes)
                            {
                                // запускаем рекурсивную функцию для дочерних подстатей
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
                "Не удалось получить список статей расходов.\n\nТекст ошибки: " + f.Message, "Внимание",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
            }
			finally // очищаем занимаемые ресурсы
            {
                DBConnection.Close();
            }

            return bRet;
        }
        /// <summary>
        /// Обновляет дерево статей расходов для бюджета 
        /// </summary>
        /// <param name="objProfile">профайл</param>
        /// <param name="objTreeList">дерево статей расходов</param>
        /// <param name="objBudget">бюджет</param>
        /// <returns>true - успешное завершение;false - ошибка</returns>
        public static System.Boolean LoadDebitArticleListForBudget(UniXP.Common.CProfile objProfile,
            DevExpress.XtraTreeList.TreeList objTreeList, CBudget objBudget, DevExpress.XtraTreeList.Columns.TreeListColumn objColumnYear)
        {
            System.Boolean bRet = false;
            if (objTreeList == null)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show("Объект дерево не определен!", "Внимание",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);
                return bRet;
            }
            objTreeList.ClearNodes();

            // подключаемся к БД
            System.Data.SqlClient.SqlConnection DBConnection = objProfile.GetDBSource();
            if (DBConnection == null)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show("Отсутствует соединение с БД.", "Внимание",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);
                return bRet;
            }

            try
            {
                // соединение с БД получено, прописываем команду на выборку данных
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand() { Connection = DBConnection, CommandType = System.Data.CommandType.StoredProcedure };

                // запрашиваем список родительских статей расходов
                List<CDebitArticle> objDebitArticleParentList = GetDebitArticleParentList(objProfile, cmd);
                if (objDebitArticleParentList.Count > 0)
                {
                    bRet = true;
                    // удалим "лишние" статьи - не тот финансовый год
                    objDebitArticleParentList = objDebitArticleParentList.Where<CDebitArticle>(x => x.FinancislYear == objBudget.Date.Year).ToList<CDebitArticle>();
                    
                    // загрузим список служб для каждой статьи расходов
                    foreach (CDebitArticle objDebitArticleParent in objDebitArticleParentList)
                    {
                        LoadDebitArticleBudgetDepList(objProfile, cmd, objDebitArticleParent);
                    }

                    // теперь отфильтруем список статей по условию, что список подразделений не пустой
                    objDebitArticleParentList = objDebitArticleParentList.Where<CDebitArticle>(x => x.BudgetDepList != null).ToList<CDebitArticle>();
                    objDebitArticleParentList = objDebitArticleParentList.Where<CDebitArticle>(x => x.BudgetDepList.Count > 0).ToList<CDebitArticle>();

                    // должны остаться только те статьи, которые назначены подразделению, указанному в объекте "Бюджет"
                    objDebitArticleParentList = objDebitArticleParentList.Where<CDebitArticle>(x => x.BudgetDepList.FirstOrDefault<CBudgetDep>(y => y.uuidID.Equals(objBudget.BudgetDep.uuidID)) != null).ToList<CDebitArticle>();

                    if( ( objDebitArticleParentList != null ) && ( objDebitArticleParentList.Count > 0 ) ) 
                    {
                        //теперь построим узлы верхнего уровня
                        DevExpress.XtraTreeList.Nodes.TreeListNode objNodeYear = null;
                        foreach (CDebitArticle objDebitArticle in objDebitArticleParentList)
                        {
                            //добавляем узел в дерево
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

                        // для каждого узла со статьёй расходов строим дерево дочерних подстатей
                        foreach (DevExpress.XtraTreeList.Nodes.TreeListNode objYear in objTreeList.Nodes)
                        {
                            foreach (DevExpress.XtraTreeList.Nodes.TreeListNode objNodeParent in objYear.Nodes)
                            {
                                // запускаем рекурсивную функцию для дочерних подстатей
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
                "Не удалось получить список статей расходов.\n\nТекст ошибки: " + f.Message, "Внимание",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
			finally // очищаем занимаемые ресурсы
            {
                DBConnection.Close();
                objTreeList.Enabled = true;
            }

            return bRet;
        }
        /// <summary>
        /// Возвращает список родительских статей расходов
        /// </summary>
        /// <param name="objProfile">профайл</param>
        /// <param name="cmd">SQL-команда</param>
        /// <returns>список родительских статей расходов</returns>
        public static List<CDebitArticle> GetDebitArticleParentList( UniXP.Common.CProfile objProfile,
            System.Data.SqlClient.SqlCommand cmd )
        {
            List<CDebitArticle> objList = new List<CDebitArticle>();

            if( cmd == null )
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(  "Отсутствует соединение с БД.", "Внимание",
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
                        // создаем объект класса "Статья расходов"
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
                "Не удалось получить список статей расходов.\n\nТекст ошибки: " + f.Message, "Внимание",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
            }
			finally // очищаем занимаемые ресурсы
            {
            }
            return objList;
        }

        /// <summary>
        /// Загружает список бюджетных подразделений в статью расходов
        /// </summary>
        /// <param name="objProfile">профайл</param>
        /// <param name="cmd">SQL-команда</param>
        /// <param name="objDebitArticle">статья расходов</param>
        /// <returns>true - успешное завершение;false - ошибка</returns>
        public static System.Boolean LoadDebitArticleBudgetDepList( UniXP.Common.CProfile objProfile,
            System.Data.SqlClient.SqlCommand cmd, CDebitArticle objDebitArticle )
        {
            System.Boolean bRet = false;
            if( objDebitArticle == null )
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(  "Объект \"Статья расходов\" не определен!", "Внимание",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning );
                return bRet;
            }

            if( cmd == null )
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(  "Отсутствует соединение с БД.", "Внимание",
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
                        // добавляем запись в список бюджетных подразделений
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
                "Не удалось получить список бюджетных подразделений для статьи расходов.\n\nТекст ошибки: " + f.Message, "Внимание",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
            }
			finally // очищаем занимаемые ресурсы
            {
            }
            return bRet;
        }

        /// <summary>
        /// Создает дерево подстатей расходов
        /// </summary>
        /// <param name="objProfile">профайл</param>
        /// <param name="cmd">SQL-команда</param>
        /// <param name="objTreeList">дерево статей расходов</param>
        /// <param name="objNodeParent">узел дерева</param>
        /// <returns>true - успешное завершение;false - ошибка</returns>
        public static System.Boolean LoadChildDebitArticleNodes( UniXP.Common.CProfile objProfile,
            System.Data.SqlClient.SqlCommand cmd,
            DevExpress.XtraTreeList.Nodes.TreeListNode objNodeParent,
            DevExpress.XtraTreeList.TreeList objTreeList )
        {
            System.Boolean bRet = false;
            if( ( objNodeParent == null ) || ( objNodeParent.Tag == null ) )
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(  "Объект узел дерева не определен \nлибо у него отсутствует ссылка на статью расходов!", "Внимание",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning );
                return bRet;
            }
            if( cmd == null )
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(  "Отсутствует соединение с БД.", "Внимание",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning );
                return bRet;
            }

            try
            {
                // в родительском узле в тэге лежит статья, извлечем ее
                CDebitArticle objDebitArticleParent = ( CDebitArticle )objNodeParent.Tag;
                if( objDebitArticleParent == null )
                {
                    DevExpress.XtraEditors.XtraMessageBox.Show(  "не удалось получить родительскую статью расходов.", "Внимание",
                        System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning );
                    return bRet;
                }
                else
                {
                    // список дочерних подстатей
                    bRet = true;
                    List<CDebitArticle> objDebitArticleList = GetChildDebitArticleList( objProfile, cmd, objDebitArticleParent.uuidID );
                    if( ( objDebitArticleList != null ) && ( objDebitArticleList.Count > 0) )
                    {
                        // загружаем в подстатьи список служб
                        foreach( CDebitArticle objDebitArticle in objDebitArticleList )
                        {
                            LoadDebitArticleBudgetDepList( objProfile, cmd, objDebitArticle );
                        }
                        // теперь для каждой подстатьи в списке строим узел
                        foreach (CDebitArticle objDebitArticleItem in objDebitArticleList)
                        {
                            //добавляем узел в дерево
                            objTreeList.AppendNode(new object[] { 
                                objDebitArticleItem.m_uuidID, objDebitArticleItem.m_uuidParentID, 
                                objDebitArticleItem.ArticleFullName, 
                                ( ( objDebitArticleItem.AccountPlan == null ) ? "" : objDebitArticleItem.AccountPlan.FullName ), 
                                false }, objNodeParent).Tag = objDebitArticleItem;
                        }

                        // для каждого узла с подстатьей расходов строим дерево дочерних подстатей
                        foreach (DevExpress.XtraTreeList.Nodes.TreeListNode objNodeChildt in objNodeParent.Nodes)
                        {
                            // запускаем рекурсивную функцию для дочерних подстатей
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
                "Не удалось построить дерево подстатей расходов.\n\nТекст ошибки: " + f.Message, "Внимание",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
            }
			finally // очищаем занимаемые ресурсы
            {
            }
            return bRet;
        }

        /// <summary>
        /// Возвращает список всех подстатей с привязкой к бюджетному подразделению
        /// </summary>
        /// <param name="objProfile">профайл</param>
        /// <param name="cmd">SQl-команда</param>
        /// <param name="uuidDebitArticleParent">УИ родителской статьи расходов</param>
        /// <returns>список всех подстатей с привязкой к бюджетному подразделению</returns>
        public static List<CDebitArticle> GetChildDebitArticleList( UniXP.Common.CProfile objProfile,
            System.Data.SqlClient.SqlCommand cmd, System.Guid uuidDebitArticleParent )
        {
            List<CDebitArticle> objChildDebitArticleList = new List<CDebitArticle>();
            // подключаемся к БД
            if( cmd == null )
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(  "Отсутствует соединение с БД.", "Внимание",
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
                "Не удалось получить список дочерних статей расходов.\n\nТекст ошибки: " + f.Message, "Внимание",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
            }

            return objChildDebitArticleList;
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
            System.Boolean bRet = false;

            System.Data.SqlClient.SqlConnection DBConnection = objProfile.GetDBSource();
            if( DBConnection == null ) { return bRet; }

            try
            {
                // соединение с БД получено, прописываем команду на выборку данных
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
                    // набор данных непустой, в нем нас интересует одна запись
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
                    "Не удалось проинициализировать класс CDebitArticle.\nВ БД не найдена информация.", "Внимание",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning );

                }
                cmd.Dispose();
                rs.Dispose();
            }
            catch( System.Exception e )
            {
                DevExpress.XtraEditors.XtraMessageBox.Show( 
                "Не удалось проинициализировать класс CDebitArticle.\n" + e.Message, "Внимание",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
            }
			finally // очищаем занимаемые ресурсы
            {
                DBConnection.Close();
            }
            return bRet;
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
            System.Boolean bRet = false;

            System.Data.SqlClient.SqlConnection DBConnection = objProfile.GetDBSource();
            if( DBConnection == null )
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(  "Отсутствует соединение с БД.", "Внимание",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
                return bRet;
            }
            System.Data.SqlClient.SqlTransaction DBTransaction = DBConnection.BeginTransaction();
            try
            {
                // соединение с БД получено, прописываем команду на создание записи в БД
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand() { Connection = DBConnection, Transaction = DBTransaction, 
                    CommandType = System.Data.CommandType.StoredProcedure };

                // собственно процесс удаления статьи в БД
                bRet = this.Remove( cmd, objProfile );
                if( bRet == true )
                {
                    // подтверждаем транзакцию
                    DBTransaction.Commit();
                }
                else
                {
                    // откатываем транзакцию
                    DBTransaction.Rollback();
                }
                cmd.Dispose();
            }
            catch( System.Exception f )
            {
                // откатываем транзакцию
                DBTransaction.Rollback();
                DevExpress.XtraEditors.XtraMessageBox.Show( 
                "Не удалось удалить статью расходов:" + this.m_strArticleNum + " " + this.Name + 
                "\n\nТекст ошибки: " + f.Message, "Внимание",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
            }
			finally // очищаем занимаемые ресурсы
            {
                DBConnection.Close();
            }

            return bRet;
        }

        /// <summary>
        /// Удалить запись из БД
        /// </summary>
        /// <param name="objProfile">профайл</param>
        /// <param name="cmd">SQL-команда</param>
        /// <returns>true - удачное завершение; false - ошибка</returns>
        public System.Boolean Remove( System.Data.SqlClient.SqlCommand cmd, UniXP.Common.CProfile objProfile )
        {
            System.Boolean bRet = false;
            if( cmd == null ) { return bRet; }

            // уникальный идентификатор не должен быть пустым
            if( this.m_uuidID == System.Guid.Empty )
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(  "Недопустимое значение уникального идентификатора объекта", "Внимание",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
                return bRet;
            }

            try
            {
                // соединение с БД получено, прописываем команду на удаление данных
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
                    DevExpress.XtraEditors.XtraMessageBox.Show("Ошибка удаления статьи расходов.\n\nТекст ошибки: " +
                            (System.String)cmd.Parameters["@ERROR_MESSAGE"].Value, "Внимание",
                        System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                }
                bRet = ( iRet == 0 );
            }
            catch( System.Exception f )
            {
                DevExpress.XtraEditors.XtraMessageBox.Show( 
                "Не удалось удалить статью расходов.\n\nТекст ошибки: " + 
                ( System.String )cmd.Parameters[ "@ERROR_MESSAGE" ].Value + "\n" + f.Message, "Внимание",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
            }
            return bRet;
        }
        #endregion

        #region Add
        /// <summary>
        /// Проверяет правильность заполнения обязательных значений
        /// </summary>
        /// <returns>true - все в порядке; false - неверные значения </returns>
        public System.Boolean IsValidateProperties( ref System.String strErr )
        {
            System.Boolean bRes = false;
            try
            {
                // наименование не должен быть пустым
                if( this.Name == "" )
                {
                    strErr += ("\nНедопустимое значение наименования статьи расходов.");
                    return bRes;
                }
                if( this.ArticleNum == "" )
                {
                    strErr += ("\nНедопустимое значение номера статьи расходов.");
                    return bRes;
                }
                bRes = true;
            }
            catch( System.Exception f )
            {
                strErr += String.Format("\nОшибка проверки свойств статьи расходов.\nТекст ошибки: {0}", f.Message);
            }
            return bRes;
        }
        /// <summary>
        /// Добавить запись в БД
        /// </summary>
        /// <param name="objProfile">профайл</param>
        /// <returns>true - удачное завершение; false - ошибка</returns>
        public override System.Boolean Add( UniXP.Common.CProfile objProfile )
        {
            System.Boolean bRet = false;
            System.String strErr = "";

            System.Data.SqlClient.SqlConnection DBConnection = objProfile.GetDBSource();
            if( DBConnection == null )
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(  "Отсутствует соединение с БД.", "Внимание",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
                return bRet;
            }
            System.Data.SqlClient.SqlTransaction DBTransaction = DBConnection.BeginTransaction();
            try
            {
                // соединение с БД получено, прописываем команду на создание записи в БД
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand() { Connection = DBConnection, Transaction = DBTransaction, 
                    CommandType = System.Data.CommandType.StoredProcedure };

                // собственно процесс добавления статьи в БД
                bRet = this.Add(cmd, objProfile, ref strErr);
                if( bRet == true )
                {
                    // подтверждаем транзакцию
                    DBTransaction.Commit();
                }
                else
                {
                    // откатываем транзакцию
                    DBTransaction.Rollback();

                    DevExpress.XtraEditors.XtraMessageBox.Show(strErr, "Внимание",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                }
                cmd.Dispose();
            }
            catch( System.Exception f )
            {
                // откатываем транзакцию
                DBTransaction.Rollback();

                DevExpress.XtraEditors.XtraMessageBox.Show(
                String.Format("Не удалось создать статью расходов:{0} {1}\n\nТекст ошибки: {2}", this.m_strArticleNum, this.Name, f.Message), "Внимание",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
            }
			finally // очищаем занимаемые ресурсы
            {
                DBConnection.Close();
            }

            return bRet;
        }

        /// <summary>
        /// Добавить запись в БД
        /// </summary>
        /// <param name="objProfile">профайл</param>
        /// <returns>true - удачное завершение; false - ошибка</returns>
        public System.Boolean Add(UniXP.Common.CProfile objProfile, ref System.String strErr)
        {
            System.Boolean bRet = false;

            System.Data.SqlClient.SqlConnection DBConnection = objProfile.GetDBSource();
            if (DBConnection == null)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show("Отсутствует соединение с БД.", "Внимание",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                return bRet;
            }
            System.Data.SqlClient.SqlTransaction DBTransaction = DBConnection.BeginTransaction();
            try
            {
                // соединение с БД получено, прописываем команду на создание записи в БД
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand()
                {
                    Connection = DBConnection,
                    Transaction = DBTransaction,
                    CommandType = System.Data.CommandType.StoredProcedure
                };

                // собственно процесс добавления статьи в БД
                bRet = this.Add(cmd, objProfile, ref strErr);
                if (bRet == true)
                {
                    // подтверждаем транзакцию
                    DBTransaction.Commit();
                }
                else
                {
                    // откатываем транзакцию
                    DBTransaction.Rollback();

                    DevExpress.XtraEditors.XtraMessageBox.Show(strErr, "Внимание",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                }
                cmd.Dispose();
            }
            catch (System.Exception f)
            {
                // откатываем транзакцию
                DBTransaction.Rollback();

                DevExpress.XtraEditors.XtraMessageBox.Show(
                String.Format("Не удалось создать статью расходов:{0} {1}\n\nТекст ошибки: {2}", this.m_strArticleNum, this.Name, f.Message), "Внимание",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
			finally // очищаем занимаемые ресурсы
            {
                DBConnection.Close();
            }

            return bRet;
        }

        /// <summary>
        /// Добавить запись в БД
        /// </summary>
        /// <param name="objProfile">профайл</param>
        /// <returns>true - удачное завершение; false - ошибка</returns>
        public System.Boolean Add( System.Data.SqlClient.SqlCommand cmd, UniXP.Common.CProfile objProfile, ref System.String strErr )
        {
            System.Boolean bRet = false;
            if( cmd == null )
            {
                strErr += "\nОтсутствует соединение с БД.";
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
                    // сохранение списка бюджетных подразделений для данной статьи расходов
                    bRet = this.AssignDebitArticleBudgetDep( cmd, objProfile, ref strErr );
                    if (bRet == true)
                    {
                        bRet = this.AssignDebitArticleToFinancialPeriod(cmd, objProfile, ref strErr);
                    }
                }
                else
                {
                    strErr += String.Format("\nНе удалось зарегистрировать статью расходов.\n{0} {1}\nТекст ошибки: {2}", this.ArticleNum, this.Name, (System.String)cmd.Parameters["@ERROR_MESSAGE"].Value);
                }
            }
            catch( System.Exception f )
            {
                strErr += String.Format("\nНе удалось создать статью расходов.\n{0} {1}\nТекст ошибки: {2}", this.ArticleNum, this.Name, f.Message);
            }
            return bRet;
        }
        /// <summary>
        /// Привязка статьи расходов к финансовому году
        /// </summary>
        /// <param name="cmd">SQL-команда</param>
        /// <param name="objProfile">профайл</param>
        /// <returns>true - удачное завершение; false - ошибка</returns>
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
                    strErr += String.Format("\nОшибка привязки статьи расходов к финансовому году.\n{0} {1}\nОтмена внесенных изменений.\nТекст ошибки: {2}", 
                        this.ArticleNum, this.Name, System.Convert.ToString(cmd.Parameters["@ERROR_MESSAGE"].Value));
                }
            }
            catch (System.Exception f)
            {
                strErr += String.Format("\nОшибка привязки статьи расходов к финансовому году.\n{0}", f.Message);
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
        public override System.Boolean Update(UniXP.Common.CProfile objProfile)
        {
            return false;
        }
        /// <summary>
        /// Сохранить изменения в БД
        /// </summary>
        /// <param name="objProfile">профайл</param>
        /// <param name="bUpdateWarningParam">признак того, что нужно обновить список служб</param>
        /// <returns>true - удачное завершение; false - ошибка</returns>
        public System.Boolean Update( UniXP.Common.CProfile objProfile, System.Boolean bUpdateWarningParam, ref System.String strErr )
        {
            System.Boolean bRet = false;

            System.Data.SqlClient.SqlConnection DBConnection = objProfile.GetDBSource();
            if( DBConnection == null )
            {
                strErr += ("\nОтсутствует соединение с БД.");
                return bRet;
            }
            System.Data.SqlClient.SqlTransaction DBTransaction = DBConnection.BeginTransaction();
            try
            {
                // соединение с БД получено, прописываем команду на создание записи в БД
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand() { Connection = DBConnection, Transaction = DBTransaction, 
                    CommandType = System.Data.CommandType.StoredProcedure };

                // собственно процесс сохранения в БД
                bRet = this.Update( cmd, objProfile, bUpdateWarningParam, ref strErr );
                if( bRet == true )
                {
                    // подтверждаем транзакцию
                    DBTransaction.Commit();
                }
                else
                {
                    // откатываем транзакцию
                    DBTransaction.Rollback();
                }
                cmd.Dispose();
            }
            catch( System.Exception f )
            {
                // откатываем транзакцию
                DBTransaction.Rollback();
                strErr += String.Format("\nНе удалось изменить свойства статьи расходов:{0} {1}\nТекст ошибки: {2}", this.m_strArticleNum, this.Name, f.Message);
            }
			finally // очищаем занимаемые ресурсы
            {
                DBConnection.Close();
            }

            return bRet;
        }
        /// <summary>
        /// Сохранить изменения в списке статей расходов
        /// </summary>
        /// <param name="objProfile">профайл</param>
        /// <param name="objDebitArticleList">список статей расходов</param>
        /// <param name="bUpdateWarningParam">признак того, что нужно обновить список служб</param>
        /// <returns>true - удачное завершение; false - ошибка</returns>
        public static System.Boolean UpdateList( UniXP.Common.CProfile objProfile, List<CDebitArticle> objDebitArticleList,
            System.Boolean bUpdateWarningParam, ref System.String strErr )
        {
            System.Boolean bRet = false;
            if( ( objDebitArticleList == null ) || ( objDebitArticleList.Count == 0 ) )
            {
                strErr += ("\nСписок статей расходов пуст.");
                return bRet;
            }
            System.Data.SqlClient.SqlConnection DBConnection = objProfile.GetDBSource();
            if( DBConnection == null )
            {
                strErr += ("\nОтсутствует соединение с БД.");
                return bRet;
            }
            System.Data.SqlClient.SqlTransaction DBTransaction = DBConnection.BeginTransaction();
            try
            {
                // соединение с БД получено, прописываем команду на создание записи в БД
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand() { Connection = DBConnection, Transaction = DBTransaction, 
                    CommandType = System.Data.CommandType.StoredProcedure };
                foreach( CDebitArticle objDebitArticle in objDebitArticleList )
                {
                    // собственно процесс сохранения в БД
                    bRet = objDebitArticle.Update( cmd, objProfile, bUpdateWarningParam, ref strErr );
                    if( bRet == false ) { break; }
                }
                if( bRet == true )
                {
                    // подтверждаем транзакцию
                    DBTransaction.Commit();
                }
                else
                {
                    // откатываем транзакцию
                    DBTransaction.Rollback();
                }
                cmd.Dispose();
            }
            catch( System.Exception f )
            {
                // откатываем транзакцию
                DBTransaction.Rollback();
                strErr += ("\nНе удалось сохранить изменения в списке статей расходов.\nТекст ошибки: " + f.Message);
            }
			finally // очищаем занимаемые ресурсы
            {
                DBConnection.Close();
            }

            return bRet;
        }
        /// <summary>
        /// Сохранить изменения в БД
        /// </summary>
        /// <param name="objProfile">профайл</param>
        /// <param name="cmd">SQL-команда</param>
        /// <param name="bUpdateWarningParam">признак того, что нужно обновить список служб</param>
        /// <returns>true - удачное завершение; false - ошибка</returns>
        public System.Boolean Update( System.Data.SqlClient.SqlCommand cmd,
            UniXP.Common.CProfile objProfile, System.Boolean bUpdateWarningParam, ref System.String strErr )
        {
            System.Boolean bRet = false;
            if( cmd == null ) { return bRet; }
            try
            {
                // проверка свойств
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
                        // попробуем сохранить список бюджетных подразделений для данной статьи расходов
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
                    strErr += String.Format("\nОшибка изменения статьи расходов: \n{0} {1}\nТекст ошибки: {2}", this.m_strArticleNum, this.Name, 
                        (System.String)cmd.Parameters["@ERROR_MESSAGE"].Value);
                }
            }
            catch( System.Exception f )
            {
                strErr += String.Format("\nНе удалось изменить свойства статьи расходов:{0} {1}\nТекст ошибки: {2}", this.m_strArticleNum, this.Name, f.Message);
            }
            return bRet;
        }
        #endregion

        #region Связь с бюджетными подразделениями
        /// <summary>
        /// Удаляет связь статьи расходов с бюджетными подразделениями
        /// </summary>
        /// <param name="objProfile">профайл</param>
        /// <param name="cmd">SQL-команда</param>
        /// <returns>true - удачное завершение; false - ошибка</returns>
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
                    strErr += String.Format("\nОшибка удаления списка бюджетных подразделений у статьи расходов\n{0} {1}\nОтмена внесенных изменений.", 
                        this.ArticleNum, this.Name);
                }
            }
            catch( System.Exception e )
            {
                strErr += String.Format("\nОшибка удаления списка бюджетных подразделений у статьи расходов.\nТекст ошибки: {0}", e.Message);
            }
            return bRet;
        }
        /// <summary>
        /// Связывает статью расходов с бюджетными подразделениями
        /// </summary>
        /// <param name="objProfile">профайл</param>
        /// <param name="cmd">SQL-команда</param>
        /// <returns>true - удачное завершение; false - ошибка</returns>
        private System.Boolean AssignDebitArticleBudgetDep( System.Data.SqlClient.SqlCommand cmd,
            UniXP.Common.CProfile objProfile, ref System.String strErr )
        {
            System.Boolean bRet = false;
            if( cmd == null ) { return bRet; }
            try
            {
                // Запрашиваем список бюджетных подразделений 
                List<CBudgetDep> objBudgetDepList = CBudgetDep.GetBudgetDepsList( objProfile, false );
                if( objBudgetDepList.Count == 0 )
                {
                    strErr += ("\nВ БД не найден список бюджетных подразделений.");
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


                // пробегаем по списку бюджетных подразделений, 
                // если в списке статьи расходов подразделения нет, то значит связь нужно разорвать
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
                        // выход из цикла
                        break;
                    }
                }

                if (iRet != 0)
                {
                    strErr += String.Format("\nОшибка присвоения бюджетного подразделения и проекта статье расходов.\nТекст ошибки: {0}", cmd.Parameters["@ERROR_MESSAGE"].Value as System.String);
                }

                bRet = (iRet == 0);
            }
            catch( System.Exception f )
            {
                strErr += String.Format("\nОшибка связи статьи расходов с бюджетными подразделениями и проектами.\nТекст ошибки: {0}", f.Message);
            }
            return bRet;
        }
        /// <summary>
        /// Связывает подстатью расходов с бюджетными подразделениями
        /// </summary>
        /// <param name="objProfile">профайл</param>
        /// <param name="cmd">SQL-команда</param>
        /// <returns>true - удачное завершение; false - ошибка</returns>
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
                    strErr += String.Format("\nОшибка присвоения бюджетного подразделения подстатье расходов.\nТекст ошибки: {0}", 
                        (System.String)cmd.Parameters["@ERROR_MESSAGE"].Value);
                }

                bRet = ( iRet == 0 );
            }
            catch( System.Exception f )
            {
                strErr += String.Format("\nОшибка связи подстатьи расходов с бюджетным подразделением.\nТекст ошибки: {0}", f.Message);
            }
            return bRet;
        }
        #endregion

        #region Список подразделений, назначенных статье
        /// <summary>
        /// Возвращает список служб
        /// </summary>
        /// <returns>строка со списком служб, соответствующих статье</returns>
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
                "Не удалось получить список служб для статьи расходов.\n\nТекст ошибки: " + f.Message, "Внимание",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
            }
			finally // очищаем занимаемые ресурсы
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
