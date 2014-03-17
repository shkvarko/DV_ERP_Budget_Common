using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace ERP_Budget.Common
{
    /// <summary>
    /// Класс "Условие выбора маршрута"
    /// </summary>
    public class CRouteCondition : IBaseListItem
    {
        #region Переменные, свойства, константы
        /// <summary>
        /// список переменных со значениями
        /// </summary>
        private System.Collections.Generic.List<CRouteVariable> m_RouteVariableList;
        /// <summary>
        /// список переменных со значениями
        /// </summary>
        public System.Collections.Generic.List<CRouteVariable> RouteVariableList
        {
            get { return m_RouteVariableList; }
            set { m_RouteVariableList = value; }
        }
        /// <summary>
        /// Маршрут
        /// </summary>
        private CRouteTemplate m_objRouteTemplate;
        /// <summary>
        /// Маршрут
        /// </summary>
        public CRouteTemplate RouteTemplate
        {
            get { return m_objRouteTemplate; }
            set { m_objRouteTemplate = value; }
        }
        /// <summary>
        /// Приоритет
        /// </summary>
        private System.Int32 m_iPriority;
        /// <summary>
        /// Приоритет
        /// </summary>
        public System.Int32 Priority
        {
            get { return m_iPriority; }
            set { m_iPriority = value; }
        }
        #endregion

        #region Конструкторы
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

        #region Список условий
        /// <summary>
        /// Возвращает список условий выбора маршрута для бюджетного документа
        /// </summary>
        /// <param name="objProfile">профайл</param>
        /// <returns>список условий выбора маршрута</returns>
        public static List<CRouteCondition> GetRouteConditionList(
            UniXP.Common.CProfile objProfile)
        {
            List<CRouteCondition> objList = new List<CRouteCondition>();
            try
            {
                // необходимо получить соединение с БД
                System.Data.SqlClient.SqlConnection DBConnection = objProfile.GetDBSource();
                if (DBConnection == null) { return objList; }
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
                cmd.Connection = DBConnection;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                // сперва нам понадобится список шаблонов маршрутов
                List<CRouteTemplate> objRouteTemplateList = CRouteTemplate.GetRouteTemplateList(cmd, objProfile);

                // теперь нам нужен список переменных выбора условия
                List<CRouteVariable> objRouteVariableList = CRouteVariable.GetRouteVariableList(cmd, objProfile);

                // списки не должны быть пустыми
                if ((objRouteTemplateList.Count == 0) || (objRouteVariableList.Count == 0))
                {
                    // возвращаем пустой список
                    return objList;
                }

                // заполним список условий объектами с проинициализированными шаблонами маршрутов и 
                // переменными (без значений )
                foreach (CRouteTemplate objRouteTemplate in objRouteTemplateList)
                {
                    objList.Add(new CRouteCondition(objRouteVariableList, objRouteTemplate));
                }

                // настало время прописать переменным их значения. 
                // для этого получим содержимое таблицы T_ROUTECONDITION
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
                            "Не удалось присвоить переменной значение.\nИдентификатор маршрута: " + rs.GetGuid(3).ToString() +
                            "\nИдентификатор переменной: " + rs.GetGuid(1).ToString() + "\nЗначение переменной: " +
                            rs.GetString(2), "Внимание",
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
                "Не удалось получить список условий выбора маршрута.\n\nТекст ошибки: " + f.Message, "Внимание",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
            return objList;
        }
        /// <summary>
        /// Находит объект "Условие выбора маршрута" в списке и присваивает значение переменной
        /// </summary>
        /// <param name="objList">список объектов "Условие выбора маршрута"</param>
        /// <param name="uuidRouteVariableID">УИ переменной</param>
        /// <param name="uuidRouteID">УИ маршрута</param>
        /// <param name="strVariableValue">значение переменной</param>
        /// <param name="iPriority">приоритет</param>
        /// <returns>true - переменная найдена, значение присвоено; false - переменная не найдена</returns>
        private static System.Boolean SetRouteVariableValue(List<CRouteCondition> objList, System.Guid uuidRouteVariableID,
            System.Guid uuidRouteID, System.String strVariableValue, System.Int32 iPriority)
        {
            System.Boolean bRet = false;
            try
            {
                // поисчем нужный объект "Условие выбора маршрута" в списке
                foreach (CRouteCondition objRouteCondition in objList)
                {
                    if (objRouteCondition.RouteTemplate.uuidID.CompareTo(uuidRouteID) == 0)
                    {
                        objRouteCondition.m_iPriority = iPriority;
                        // поисчем нужную переменную
                        foreach (CRouteVariable objRouteVariable in objRouteCondition.RouteVariableList)
                        {
                            if (objRouteVariable.uuidID.CompareTo(uuidRouteVariableID) == 0)
                            {
                                // нашли таки... делать нечего - присваиваем значение
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
                "Не удалось присвоить переменной значение.\nИдентификатор маршрута: " + uuidRouteID.ToString() +
                "\nИдентификатор переменной: " + uuidRouteVariableID.ToString() + "\nЗначение переменной: " +
                strVariableValue + "\n\nТекст ошибки: " + f.Message, "Внимание",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
            return bRet;
        }

        #endregion

        #region Init
        /// <summary>
        /// Инициализация свойств класса
        /// </summary>
        /// <param name="objProfile">профайл</param>
        /// <param name="uuidID">уникальный идентификатор класса</param>
        /// <returns>true - успешная инициализация; false - ошибка</returns>
        public override System.Boolean Init(UniXP.Common.CProfile objProfile, System.Guid uuidID)
        {
            System.Boolean bRet = false;
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
        public override System.Boolean Remove(UniXP.Common.CProfile objProfile)
        {
            System.Boolean bRet = false;
            // уникальный идентификатор не должен быть пустым
            if (this.m_uuidID == System.Guid.Empty)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show("Недопустимое значение уникального идентификатора объекта", "Внимание",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                return bRet;
            }

            System.Data.SqlClient.SqlConnection DBConnection = objProfile.GetDBSource();
            if (DBConnection == null) { return bRet; }
            System.Data.SqlClient.SqlTransaction DBTransaction = DBConnection.BeginTransaction();

            try
            {
                // соединение с БД получено, прописываем команду на удаление данных
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
                    // подтверждаем транзакцию
                    DBTransaction.Commit();
                    bRet = true;
                }
                else
                {
                    // откатываем транзакцию
                    DBTransaction.Rollback();
                    DevExpress.XtraEditors.XtraMessageBox.Show("Ошибка выполнения запроса \nна удаление условия выбора маршрута.\nОшибка : \n" + (System.String)cmd.Parameters["@ERROR_MESSAGE"].Value, "Внимание",
                        System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                }
                cmd.Dispose();
            }
            catch (System.Exception e)
            {
                DBTransaction.Rollback();
                DevExpress.XtraEditors.XtraMessageBox.Show(
                "Ошибка выполнения запроса \nна удаление условия выбора маршрута.\n" + e.Message, "Внимание",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
			finally // очищаем занимаемые ресурсы
            {
                DBConnection.Close();
            }
            return bRet;
        }

        /// <summary>
        /// Удалить все записи об условиях из БД
        /// </summary>
        /// <param name="objProfile">профайл</param>
        /// <returns>true - удачное завершение; false - ошибка</returns>
        public static System.Boolean RemoveAll(UniXP.Common.CProfile objProfile)
        {
            System.Boolean bRet = false;
            System.Data.SqlClient.SqlConnection DBConnection = objProfile.GetDBSource();
            if (DBConnection == null) { return bRet; }
            System.Data.SqlClient.SqlTransaction DBTransaction = DBConnection.BeginTransaction();

            try
            {
                // соединение с БД получено, прописываем команду на удаление данных
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
                    // подтверждаем транзакцию
                    DBTransaction.Commit();
                    bRet = true;
                }
                else
                {
                    // откатываем транзакцию
                    DBTransaction.Rollback();
                    DevExpress.XtraEditors.XtraMessageBox.Show("Ошибка выполнения запроса \nна удаление условия выбора маршрута.\nОшибка : \n" +
                        (System.String)cmd.Parameters["@ERROR_MESSAGE"].Value, "Внимание",
                        System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                }
                cmd.Dispose();
            }
            catch (System.Exception e)
            {
                DBTransaction.Rollback();
                DevExpress.XtraEditors.XtraMessageBox.Show(
                "Ошибка выполнения запроса \nна удаление условия выбора маршрута.\n" + e.Message, "Внимание",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
			finally // очищаем занимаемые ресурсы
            {
                DBConnection.Close();
            }
            return bRet;
        }
        #endregion

        #region Add
        /// <summary>
        /// Сохраняет в БД список условий выбора маршрутов
        /// </summary>
        /// <param name="objProfile">профайл</param>
        /// <returns>true - удачное завершение; false - ошибка</returns>
        public static System.Boolean SaveRouteConditionList(UniXP.Common.CProfile objProfile,
            List<CRouteCondition> objList)
        {
            System.Boolean bRet = false;
            // список не должен быть пустым
            if (objList.Count == 0)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show("Список условий предоставления маршрутов не должен быть пустым.", "Внимание",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                return bRet;
            }

            System.Data.SqlClient.SqlConnection DBConnection = objProfile.GetDBSource();
            if (DBConnection == null) { return bRet; }
            System.Data.SqlClient.SqlTransaction DBTransaction = DBConnection.BeginTransaction();
            try
            {
                // соединение с БД получено, прописываем команду на создание записи в БД
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
                cmd.Connection = DBConnection;
                cmd.Transaction = DBTransaction;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                // сперва удаляем все условия, а затем сохраняем список
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
                        // подтверждаем транзакцию
                        DBTransaction.Commit();
                        bRet = true;
                    }
                    else
                    {
                        // откатываем транзакцию
                        DBTransaction.Rollback();
                        switch (iRet)
                        {
                            case 1:
                                {
                                    DevExpress.XtraEditors.XtraMessageBox.Show("Переменная для условия выбора маршрута с заданным идентификатором не найдена: '", "Внимание",
                                        System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);
                                    break;
                                }
                            case 2:
                                {
                                    DevExpress.XtraEditors.XtraMessageBox.Show("Шаблона маршрута с указанным идентификатором не найден: '" +
                                    objRouteCondition.RouteTemplate.uuidID.ToString() + "'", "Внимание",
                                        System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);
                                    break;
                                }
                            case 3:
                                {
                                    DevExpress.XtraEditors.XtraMessageBox.Show("Условие выбора шаблона маршрута для указаной переменной и маршрута уже существует: '\n" +
                                    objRouteCondition.RouteTemplate.Name, "Внимание",
                                        System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);
                                    break;
                                }
                            default:
                                {
                                    DevExpress.XtraEditors.XtraMessageBox.Show("Ошибка создания условия выбора маршрута", "Ошибка",
                                        System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                                    break;
                                }
                        }
                    }
                    objRouteCondition = null;
                }
                else
                {
                    // откатываем транзакцию
                    DBTransaction.Rollback();
                    DevExpress.XtraEditors.XtraMessageBox.Show("Ошибка выполнения запроса \nна удаление условий выбора маршрута.\nОшибка :\n" +
                        (System.String)cmd.Parameters["@ERROR_MESSAGE"].Value, "Внимание",
                        System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                }

                cmd.Dispose();
            }
            catch (System.Exception e)
            {
                // откатываем транзакцию
                DBTransaction.Rollback();
                DevExpress.XtraEditors.XtraMessageBox.Show(
                "Ошибка сохранения условий выбора маршрута.\nТекст ошибки:\n" + e.Message, "Внимание",
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
        public override System.Boolean Add(UniXP.Common.CProfile objProfile)
        {
            System.Boolean bRet = false;

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
            System.Boolean bRet = false;

            return bRet;
        }
        #endregion

        #region Проверка на выполнение условия
        /// <summary>
        /// Проверка на соответствие значений входных переменных тем значениям, коорые указаны в шаблоне маршрута
        /// </summary>
        /// <param name="objCRouteCondition">шаблон маршрута</param>
        /// <param name="objInitRouteVariableList">список переменных для сравнения с шаблоном</param>
        /// <returns>true - список переменных соответствует шаблону маршрута; false - список переменных НЕ удовлетворяет шаблону маршрута</returns>
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
                            // для переменной в шаблоне маршрута не найдено соответствие в наборе входных переменных
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
