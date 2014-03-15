using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;


namespace ERP_Budget.Common
{
    public class CNoteType : IBaseListItem
    {
        #region Переменные 
        /// <summary>
        /// Номер по порядку
        /// </summary>
        private System.Int32 m_iOrderNum;
        /// <summary>
        /// Номер по порядку
        /// </summary>
        [DisplayName("Номер по порядку")]
        [Description("Номер в логической последовательности пометок")]
        [Category("Обязательные значения")]
        public System.Int32 OrderNum
        {
            get { return m_iOrderNum; }
            set { m_iOrderNum = value; }
        }
        /// <summary>
        /// Картинка
        /// </summary>
        private System.Drawing.Image m_ImageSmall;
        [DisplayName("Картинка")]
        [Description("Картинка, соответствующая пометке")]
        [Category("Обязательные значения")]
        public System.Drawing.Image ImageSmall
        {
            get { return m_ImageSmall; }
            set { m_ImageSmall = value; }
        }
        /// <summary>
        /// Примечание
        /// </summary>
        private System.String m_strDescription;
        /// <summary>
        /// Имя
        /// </summary>
        [DisplayName("Примечание")]
        [Description("Описание объекта")]
        [Category("Дополнительные значения")]
        public System.String Description
        {
            get { return m_strDescription; }
            set { m_strDescription = value; }
        }
        #endregion

        #region Конструкторы 
        public CNoteType()
        {
            this.m_uuidID = System.Guid.Empty;
            this.m_strName = "";
            this.m_iOrderNum = 0;
            this.m_ImageSmall = null;
            this.m_strDescription = "";
        }
        public CNoteType(System.Guid uuidID, System.String strName, System.Int32 iOrderNum,
            System.Drawing.Image ImageSmall, System.String strDescription)
        {
            this.m_uuidID = uuidID;
            this.m_strName = strName;
            this.m_iOrderNum = iOrderNum;
            this.m_ImageSmall = ImageSmall;
            this.m_strDescription = strDescription;
        }
        #endregion

        #region Список пометок
        /// <summary>
        /// Возвращает список пометок
        /// </summary>
        /// <param name="objProfile">профайл</param>
        /// <returns>список пометок</returns>
        public static List<CNoteType> GetNoteTypeList(
            UniXP.Common.CProfile objProfile)
        {
            List<CNoteType> objList = new System.Collections.Generic.List<CNoteType>();

            System.Data.SqlClient.SqlConnection DBConnection = objProfile.GetDBSource();
            if (DBConnection == null) 
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(
                "Не удалось получить список пометок.\n\nОтсутствует соединение с базой данных.", "Внимание",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);
                return objList; 
            }

            try
            {
                // соединение с БД получено, прописываем команду на выборку данных
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
                cmd.Connection = DBConnection;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = System.String.Format("[{0}].[dbo].[sp_GetNoteType]", objProfile.GetOptionsDllDBName());
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ((System.Byte)(0)), ((System.Byte)(0)), "", System.Data.DataRowVersion.Current, null));
                System.Data.SqlClient.SqlDataReader rs = cmd.ExecuteReader();
                if (rs.HasRows)
                {
                    while (rs.Read())
                    {
                        CNoteType objNoteType = new CNoteType();
                        objNoteType.m_uuidID = (System.Guid)rs["GUID_ID"];
                        objNoteType.m_strName = (System.String)rs["NOTETYPE_NAME"];
                        objNoteType.m_iOrderNum = (System.Int32)rs["NOTETYPE_ID"];
                        if (rs["NOTETYPE_DESCRIPTION"] != System.DBNull.Value)
                        {
                            objNoteType.m_strDescription = (System.String)rs["NOTETYPE_DESCRIPTION"];
                        }
                        byte[] b = rs.GetSqlBytes( 3 ).Buffer;
                        System.IO.MemoryStream mem = new System.IO.MemoryStream(b);
                        objNoteType.m_ImageSmall = System.Drawing.Image.FromStream(mem);

                        objList.Add(objNoteType);
                    }
                }
                rs.Close();
                rs.Dispose();
                cmd.Dispose();
            }
            catch (System.Exception f)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(
                "Не удалось получить список пометок.\n\nТекст ошибки: " + f.Message, "Внимание",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
			finally // очищаем занимаемые ресурсы
            {
                DBConnection.Close();
            }
            return objList;
        }
        /// <summary>
        /// Обновляет дерево со списком пометок
        /// </summary>
        /// <param name="objProfile">профайл</param>
        /// <param name="objTreeList">дерево</param>
        /// <returns>true - удачное завершение; false - ошибка</returns>
        public static System.Boolean bRefreshNoteTypeTree(UniXP.Common.CProfile objProfile,
            DevExpress.XtraTreeList.TreeList objTreeList)
        {
            System.Boolean bRet = false;
            // очищаем дерево
            objTreeList.Nodes.Clear();
            try
            {
                List<CNoteType> objList = GetNoteTypeList(objProfile);
                if ( objList != null )
                {
                    foreach ( CNoteType objNoteType in objList )
                    {
                        DevExpress.XtraTreeList.Nodes.TreeListNode objNode =
                            objTreeList.AppendNode(new object[] { objNoteType.Name }, null);
                        objNode.Tag = objNoteType;
                    }
                    bRet = true;
                    objList = null;
                }
            }
            catch (System.Exception f)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(
                "Не удалось обновить список пометок.\n\nТекст ошибки: " + f.Message, "Внимание",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
			finally // очищаем занимаемые ресурсы
            {
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

            System.Data.SqlClient.SqlConnection DBConnection = objProfile.GetDBSource();
            if (DBConnection == null)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(
                "Не удалось получить список пометок.\n\nОтсутствует соединение с базой данных.", "Внимание",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);
                return bRet;
            }

            try
            {
                // соединение с БД получено, прописываем команду на выборку данных
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
                cmd.Connection = DBConnection;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = System.String.Format("[{0}].[dbo].[sp_GetNoteType]", objProfile.GetOptionsDllDBName());
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ((System.Byte)(0)), ((System.Byte)(0)), "", System.Data.DataRowVersion.Current, null));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@GUID_ID", System.Data.SqlDbType.UniqueIdentifier));
                cmd.Parameters["@GUID_ID"].Value = uuidID;
                System.Data.SqlClient.SqlDataReader rs = cmd.ExecuteReader();
                if (rs.HasRows)
                {
                    // набор данных непустой, в нем нас интересует одна запись
                    rs.Read();
                    this.m_uuidID = (System.Guid)rs["GUID_ID"];
                    this.m_strName = (System.String)rs["NOTETYPE_NAME"];
                    this.m_iOrderNum = (System.Int32)rs["NOTETYPE_ID"];
                    if (rs["NOTETYPE_DESCRIPTION"] != System.DBNull.Value)
                    {
                        this.m_strDescription = (System.String)rs["NOTETYPE_DESCRIPTION"];
                    }
                    byte[] b = rs.GetSqlBytes(3).Buffer;
                    System.IO.MemoryStream mem = new System.IO.MemoryStream(b);
                    this.m_ImageSmall = System.Drawing.Image.FromStream(mem);
                    bRet = true;
                }
                else
                {
                    DevExpress.XtraEditors.XtraMessageBox.Show(
                    "Не удалось проинициализировать класс CNoteType.\nВ БД не найдена информация.", "Внимание",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);

                }
                cmd.Dispose();
                rs.Dispose();
            }
            catch (System.Exception f)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(
                "Не удалось проинициализировать класс CNoteType.\n" + f.Message, "Внимание",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
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
            if (DBConnection == null)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(
                "Не удалось установить соединение с базой данных.", "Внимание",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                return bRet;
            }
            System.Data.SqlClient.SqlTransaction DBTransaction = DBConnection.BeginTransaction();

            try
            {
                // соединение с БД получено, прописываем команду на создание записи в БД
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
                cmd.Connection = DBConnection;
                cmd.Transaction = DBTransaction;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = System.String.Format("[{0}].[dbo].[sp_DeleteNoteType]", objProfile.GetOptionsDllDBName());
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ((System.Byte)(0)), ((System.Byte)(0)), "", System.Data.DataRowVersion.Current, null));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@GUID_ID", System.Data.SqlDbType.UniqueIdentifier));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ERROR_NUM", System.Data.SqlDbType.Int, 8, System.Data.ParameterDirection.Output, false, ((System.Byte)(0)), ((System.Byte)(0)), "", System.Data.DataRowVersion.Current, null));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ERROR_MES", System.Data.SqlDbType.NVarChar, 4000));
                cmd.Parameters["@ERROR_MES"].Direction = System.Data.ParameterDirection.Output;
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
                    DevExpress.XtraEditors.XtraMessageBox.Show(
                    "Не удалось удалить пометку.\nТекст ошибки: " + (System.String)cmd.Parameters["@ERROR_MES"].Value, "Внимание",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);
                }
                cmd.Dispose();
            }
            catch (System.Exception f)
            {
                DBTransaction.Rollback();
                DevExpress.XtraEditors.XtraMessageBox.Show(
                "Не удалось удалить пометку.\nТекст ошибки: " + f.Message, "Внимание",
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
        /// Проверяет свойства объекта на предмет обязательного заполнения
        /// </summary>
        /// <returns>true - ошибок нет; false - ошибка</returns>
        public System.Boolean IsValidateProperties()
        {
            System.Boolean bRet = false;
            try
            {
                // наименование не должен быть пустым
                if (this.m_strName == "")
                {
                    DevExpress.XtraEditors.XtraMessageBox.Show("Необходимо указать название пометки.", "Внимание",
                        System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);
                    return bRet;
                }
                // № по-порядку
                if (this.m_iOrderNum < 0 )
                {
                    DevExpress.XtraEditors.XtraMessageBox.Show("Необходимо указать № по-порядку.", "Внимание",
                        System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);
                    return bRet;
                }
                // Картинка
                if (this.m_ImageSmall == null)
                {
                    DevExpress.XtraEditors.XtraMessageBox.Show("Необходимо назначить картинку пометке.", "Внимание",
                        System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);
                    return bRet;
                }

                bRet = true;
            }
            catch (System.Exception f)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(
                "Ошибка проверки свойств объекта.\n\nТекст ошибки: " + f.Message, "Внимание",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
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
            if (IsValidateProperties() == false) { return bRet; }

            System.Data.SqlClient.SqlConnection DBConnection = objProfile.GetDBSource();
            if (DBConnection == null)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(
                "Не удалось установить соединение с базой данных.", "Внимание",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                return bRet;
            }
            System.Data.SqlClient.SqlTransaction DBTransaction = DBConnection.BeginTransaction();

            try
            {
                // соединение с БД получено, прописываем команду на создание записи в БД
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
                cmd.Connection = DBConnection;
                cmd.Transaction = DBTransaction;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = System.String.Format("[{0}].[dbo].[sp_AddNoteType]", objProfile.GetOptionsDllDBName());
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ((System.Byte)(0)), ((System.Byte)(0)), "", System.Data.DataRowVersion.Current, null));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@GUID_ID", System.Data.SqlDbType.UniqueIdentifier, 4, System.Data.ParameterDirection.Output, false, ((System.Byte)(0)), ((System.Byte)(0)), "", System.Data.DataRowVersion.Current, null));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@NOTETYPE_NAME", System.Data.DbType.String));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@NOTETYPE_ID", System.Data.SqlDbType.Int));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@NOTETYPE_PICTURE_SMALL", System.Data.SqlDbType.Image));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ERROR_NUM", System.Data.SqlDbType.Int, 8, System.Data.ParameterDirection.Output, false, ((System.Byte)(0)), ((System.Byte)(0)), "", System.Data.DataRowVersion.Current, null));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ERROR_MES", System.Data.SqlDbType.NVarChar, 4000));
                cmd.Parameters["@ERROR_MES"].Direction = System.Data.ParameterDirection.Output;

                cmd.Parameters["@NOTETYPE_NAME"].Value = this.m_strName;
                if (this.m_strDescription != "")
                {
                    cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@NOTETYPE_DESCRIPTION", System.Data.DbType.String));
                    cmd.Parameters["@NOTETYPE_DESCRIPTION"].Value = this.m_strDescription;
                }
                cmd.Parameters["@NOTETYPE_ID"].Value = this.m_iOrderNum;

                System.IO.MemoryStream mstr = new System.IO.MemoryStream();
                this.m_ImageSmall.Save(mstr, this.m_ImageSmall.RawFormat);
                byte[] arrImage = mstr.GetBuffer();

                cmd.Parameters["@NOTETYPE_PICTURE_SMALL"].Value = arrImage;

                cmd.ExecuteNonQuery();
                System.Int32 iRet = (System.Int32)cmd.Parameters["@RETURN_VALUE"].Value;
                if (iRet == 0)
                {
                    // подтверждаем транзакцию
                    DBTransaction.Commit();
                    this.m_uuidID = (System.Guid)cmd.Parameters["@GUID_ID"].Value;
                    bRet = true;
                }
                else
                {
                    // откатываем транзакцию
                    DBTransaction.Rollback();
                    DevExpress.XtraEditors.XtraMessageBox.Show(
                    "Не удалось создать пометку.\nТекст ошибки: " + (System.String)cmd.Parameters["@ERROR_MES"].Value, "Внимание",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);
                }
                cmd.Dispose();
            }
            catch (System.Exception f)
            {
                // откатываем транзакцию
                DBTransaction.Rollback();
                DevExpress.XtraEditors.XtraMessageBox.Show(
                "Не удалось создать пометку.\nТекст ошибки: " + f.Message, "Внимание",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
			finally // очищаем занимаемые ресурсы
            {
                DBConnection.Close();
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
            System.Boolean bRet = false;
            if (IsValidateProperties() == false) { return bRet; }

            System.Data.SqlClient.SqlConnection DBConnection = objProfile.GetDBSource();
            if (DBConnection == null)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(
                "Не удалось установить соединение с базой данных.", "Внимание",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                return bRet;
            }
            System.Data.SqlClient.SqlTransaction DBTransaction = DBConnection.BeginTransaction();

            try
            {
                // соединение с БД получено, прописываем команду на создание записи в БД
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
                cmd.Connection = DBConnection;
                cmd.Transaction = DBTransaction;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = System.String.Format("[{0}].[dbo].[sp_EditNoteType]", objProfile.GetOptionsDllDBName());
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ((System.Byte)(0)), ((System.Byte)(0)), "", System.Data.DataRowVersion.Current, null));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@GUID_ID", System.Data.SqlDbType.UniqueIdentifier));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@NOTETYPE_NAME", System.Data.DbType.String));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@NOTETYPE_ID", System.Data.SqlDbType.Int));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@NOTETYPE_PICTURE_SMALL", System.Data.SqlDbType.Image));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ERROR_NUM", System.Data.SqlDbType.Int, 8, System.Data.ParameterDirection.Output, false, ((System.Byte)(0)), ((System.Byte)(0)), "", System.Data.DataRowVersion.Current, null));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ERROR_MES", System.Data.SqlDbType.NVarChar, 4000));
                cmd.Parameters["@ERROR_MES"].Direction = System.Data.ParameterDirection.Output;

                cmd.Parameters["@NOTETYPE_NAME"].Value = this.m_strName;
                if (this.m_strDescription != "")
                {
                    cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@NOTETYPE_DESCRIPTION", System.Data.DbType.String));
                    cmd.Parameters["@NOTETYPE_DESCRIPTION"].Value = this.m_strDescription;
                }
                cmd.Parameters["@NOTETYPE_ID"].Value = this.m_iOrderNum;

                System.IO.MemoryStream mstr = new System.IO.MemoryStream();
                this.m_ImageSmall.Save(mstr, this.m_ImageSmall.RawFormat);
                byte[] arrImage = mstr.GetBuffer();

                cmd.Parameters["@NOTETYPE_PICTURE_SMALL"].Value = arrImage;

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
                    DevExpress.XtraEditors.XtraMessageBox.Show(
                    "Не удалось изменить пометку.\nТекст ошибки: " + (System.String)cmd.Parameters["@ERROR_MES"].Value, "Внимание",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);
                }
                cmd.Dispose();
            }
            catch (System.Exception f)
            {
                // откатываем транзакцию
                DBTransaction.Rollback();
                DevExpress.XtraEditors.XtraMessageBox.Show(
                "Не удалось изменить пометку.\nТекст ошибки: " + f.Message, "Внимание",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
			finally // очищаем занимаемые ресурсы
            {
                DBConnection.Close();
            }
            return bRet;
        }
        #endregion

        #region Добавить/удалить пометку бюджетному документу
        /// <summary>
        /// Назначает пометку бюджетному документу
        /// </summary>
        /// <param name="objProfile">профайл</param>
        /// <param name="uuidBudgetDocID">уи бюджетного документа</param>
        /// <param name="uuidNoteTypeID">уи пометки</param>
        /// <returns>true - удачное завершение; false - ошибка</returns>
        public static System.Boolean MakeNoteToBudgetDoc( UniXP.Common.CProfile objProfile, 
            System.Guid uuidBudgetDocID, System.Guid uuidNoteTypeID )
        {
            System.Boolean bRet = false;

            System.Data.SqlClient.SqlConnection DBConnection = objProfile.GetDBSource();
            if (DBConnection == null)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(
                "Не удалось установить соединение с базой данных.", "Внимание",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                return bRet;
            }
            System.Data.SqlClient.SqlTransaction DBTransaction = DBConnection.BeginTransaction();

            try
            {
                // соединение с БД получено, прописываем команду на создание записи в БД
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
                cmd.Connection = DBConnection;
                cmd.Transaction = DBTransaction;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = System.String.Format("[{0}].[dbo].[sp_AddNoteTypeToBudgetDoc]", objProfile.GetOptionsDllDBName());
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ((System.Byte)(0)), ((System.Byte)(0)), "", System.Data.DataRowVersion.Current, null));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@BUDGETDOC_GUID_ID", System.Data.SqlDbType.UniqueIdentifier));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@NOTETYPE_GUID_ID", System.Data.SqlDbType.UniqueIdentifier));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ulUniXPUser_ID", System.Data.SqlDbType.Int));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ERROR_NUM", System.Data.SqlDbType.Int, 8, System.Data.ParameterDirection.Output, false, ((System.Byte)(0)), ((System.Byte)(0)), "", System.Data.DataRowVersion.Current, null));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ERROR_MES", System.Data.SqlDbType.NVarChar, 4000));

                cmd.Parameters["@ERROR_MES"].Direction = System.Data.ParameterDirection.Output;

                cmd.Parameters["@BUDGETDOC_GUID_ID"].Value = uuidBudgetDocID;
                cmd.Parameters["@NOTETYPE_GUID_ID"].Value = uuidNoteTypeID;
                cmd.Parameters["@ulUniXPUser_ID"].Value = objProfile.m_nSQLUserID;
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
                    DevExpress.XtraEditors.XtraMessageBox.Show(
                    "Не удалось назначить пометку бюджетному документу.\nТекст ошибки: " + (System.String)cmd.Parameters["@ERROR_MES"].Value, "Внимание",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);
                }
                cmd.Dispose();
            }
            catch (System.Exception f)
            {
                // откатываем транзакцию
                DBTransaction.Rollback();
                DevExpress.XtraEditors.XtraMessageBox.Show(
                "Не удалось назначить пометку бюджетному документу.\nТекст ошибки: " + f.Message, "Внимание",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
			finally // очищаем занимаемые ресурсы
            {
                DBConnection.Close();
            }
            return bRet;
        }
        /// <summary>
        /// Удаляет пометку у бюджетного документа
        /// </summary>
        /// <param name="objProfile">профайл</param>
        /// <param name="uuidBudgetDocID">уи бюджетного документа</param>
        /// <returns>true - удачное завершение; false - ошибка</returns>
        public static System.Boolean DeleteNoteFromBudgetDoc(UniXP.Common.CProfile objProfile,
            System.Guid uuidBudgetDocID)
        {
            System.Boolean bRet = false;

            System.Data.SqlClient.SqlConnection DBConnection = objProfile.GetDBSource();
            if (DBConnection == null)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(
                "Не удалось установить соединение с базой данных.", "Внимание",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                return bRet;
            }
            System.Data.SqlClient.SqlTransaction DBTransaction = DBConnection.BeginTransaction();

            try
            {
                // соединение с БД получено, прописываем команду на создание записи в БД
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
                cmd.Connection = DBConnection;
                cmd.Transaction = DBTransaction;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = System.String.Format("[{0}].[dbo].[sp_DeleteNoteTypeForBudgetDoc]", objProfile.GetOptionsDllDBName());
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ((System.Byte)(0)), ((System.Byte)(0)), "", System.Data.DataRowVersion.Current, null));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@BUDGETDOC_GUID_ID", System.Data.SqlDbType.UniqueIdentifier));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ulUniXPUser_ID", System.Data.SqlDbType.Int));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ERROR_NUM", System.Data.SqlDbType.Int, 8, System.Data.ParameterDirection.Output, false, ((System.Byte)(0)), ((System.Byte)(0)), "", System.Data.DataRowVersion.Current, null));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ERROR_MES", System.Data.SqlDbType.NVarChar, 4000));

                cmd.Parameters["@ERROR_MES"].Direction = System.Data.ParameterDirection.Output;

                cmd.Parameters["@BUDGETDOC_GUID_ID"].Value = uuidBudgetDocID;
                cmd.Parameters["@ulUniXPUser_ID"].Value = objProfile.m_nSQLUserID;
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
                    DevExpress.XtraEditors.XtraMessageBox.Show(
                    "Не удалось удалить пометку у бюджетного документа.\nТекст ошибки: " + (System.String)cmd.Parameters["@ERROR_MES"].Value, "Внимание",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);
                }
                cmd.Dispose();
            }
            catch (System.Exception f)
            {
                // откатываем транзакцию
                DBTransaction.Rollback();
                DevExpress.XtraEditors.XtraMessageBox.Show(
                "Не удалось удалить пометку у бюджетного документа.\nТекст ошибки: " + f.Message, "Внимание",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
			finally // очищаем занимаемые ресурсы
            {
                DBConnection.Close();
            }
            return bRet;
        }
        #endregion

        #region Добавить/удалить комментарий к бюджетному документу
        /// <summary>
        /// Назначает комментарий бюджетному документу
        /// </summary>
        /// <param name="objProfile">профайл</param>
        /// <param name="uuidBudgetDocID">уи бюджетного документа</param>
        /// <param name="strComment">комментарий</param>
        /// <returns>true - удачное завершение; false - ошибка</returns>
        public static System.Boolean MakeCommentToBudgetDoc(UniXP.Common.CProfile objProfile,
            System.Guid uuidBudgetDocID, System.String strComment )
        {
            System.Boolean bRet = false;

            System.Data.SqlClient.SqlConnection DBConnection = objProfile.GetDBSource();
            if (DBConnection == null)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(
                "Не удалось установить соединение с базой данных.", "Внимание",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                return bRet;
            }
            System.Data.SqlClient.SqlTransaction DBTransaction = DBConnection.BeginTransaction();

            try
            {
                // соединение с БД получено, прописываем команду на создание записи в БД
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
                cmd.Connection = DBConnection;
                cmd.Transaction = DBTransaction;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = System.String.Format("[{0}].[dbo].[sp_AddUserCommentToBudgetDoc]", objProfile.GetOptionsDllDBName());
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ((System.Byte)(0)), ((System.Byte)(0)), "", System.Data.DataRowVersion.Current, null));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@BUDGETDOC_GUID_ID", System.Data.SqlDbType.UniqueIdentifier));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@USERCOMMENT", System.Data.DbType.String));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ulUniXPUser_ID", System.Data.SqlDbType.Int));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ERROR_NUM", System.Data.SqlDbType.Int, 8, System.Data.ParameterDirection.Output, false, ((System.Byte)(0)), ((System.Byte)(0)), "", System.Data.DataRowVersion.Current, null));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ERROR_MES", System.Data.SqlDbType.NVarChar, 4000));

                cmd.Parameters["@ERROR_MES"].Direction = System.Data.ParameterDirection.Output;

                cmd.Parameters["@BUDGETDOC_GUID_ID"].Value = uuidBudgetDocID;
                cmd.Parameters["@USERCOMMENT"].Value = strComment;
                cmd.Parameters["@ulUniXPUser_ID"].Value = objProfile.m_nSQLUserID;
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
                    DevExpress.XtraEditors.XtraMessageBox.Show(
                    "Не удалось назначить комментарий бюджетному документу.\nТекст ошибки: " + (System.String)cmd.Parameters["@ERROR_MES"].Value, "Внимание",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);
                }
                cmd.Dispose();
            }
            catch (System.Exception f)
            {
                // откатываем транзакцию
                DBTransaction.Rollback();
                DevExpress.XtraEditors.XtraMessageBox.Show(
                "Не удалось назначить комментарий бюджетному документу.\nТекст ошибки: " + f.Message, "Внимание",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
			finally // очищаем занимаемые ресурсы
            {
                DBConnection.Close();
            }
            return bRet;
        }
        /// <summary>
        /// Назначает комментарий бюджетному документу
        /// </summary>
        /// <param name="objProfile">профайл</param>
        /// <param name="uuidBudgetDocID">уи бюджетного документа</param>
        /// <param name="strComment">комментарий</param>
        /// <returns>true - удачное завершение; false - ошибка</returns>
        public static System.Boolean EditCommentToBudgetDoc(UniXP.Common.CProfile objProfile,
            System.Guid uuidBudgetDocID, System.String strComment)
        {
            System.Boolean bRet = false;

            System.Data.SqlClient.SqlConnection DBConnection = objProfile.GetDBSource();
            if (DBConnection == null)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(
                "Не удалось установить соединение с базой данных.", "Внимание",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                return bRet;
            }
            System.Data.SqlClient.SqlTransaction DBTransaction = DBConnection.BeginTransaction();

            try
            {
                // соединение с БД получено, прописываем команду на создание записи в БД
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
                cmd.Connection = DBConnection;
                cmd.Transaction = DBTransaction;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = System.String.Format("[{0}].[dbo].[sp_EditUserCommentToBudgetDoc]", objProfile.GetOptionsDllDBName());
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ((System.Byte)(0)), ((System.Byte)(0)), "", System.Data.DataRowVersion.Current, null));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@BUDGETDOC_GUID_ID", System.Data.SqlDbType.UniqueIdentifier));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@USERCOMMENT", System.Data.DbType.String));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ulUniXPUser_ID", System.Data.SqlDbType.Int));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ERROR_NUM", System.Data.SqlDbType.Int, 8, System.Data.ParameterDirection.Output, false, ((System.Byte)(0)), ((System.Byte)(0)), "", System.Data.DataRowVersion.Current, null));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ERROR_MES", System.Data.SqlDbType.NVarChar, 4000));

                cmd.Parameters["@ERROR_MES"].Direction = System.Data.ParameterDirection.Output;

                cmd.Parameters["@BUDGETDOC_GUID_ID"].Value = uuidBudgetDocID;
                cmd.Parameters["@USERCOMMENT"].Value = strComment;
                cmd.Parameters["@ulUniXPUser_ID"].Value = objProfile.m_nSQLUserID;
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
                    DevExpress.XtraEditors.XtraMessageBox.Show(
                    "Не удалось изменить комментарий бюджетному документу.\nТекст ошибки: " + (System.String)cmd.Parameters["@ERROR_MES"].Value, "Внимание",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);
                }
                cmd.Dispose();
            }
            catch (System.Exception f)
            {
                // откатываем транзакцию
                DBTransaction.Rollback();
                DevExpress.XtraEditors.XtraMessageBox.Show(
                "Не удалось изменить комментарий бюджетному документу.\nТекст ошибки: " + f.Message, "Внимание",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
			finally // очищаем занимаемые ресурсы
            {
                DBConnection.Close();
            }
            return bRet;
        }
        /// <summary>
        /// Удаляет комментарий у бюджетного документа
        /// </summary>
        /// <param name="objProfile">профайл</param>
        /// <param name="uuidBudgetDocID">уи бюджетного документа</param>
        /// <returns>true - удачное завершение; false - ошибка</returns>
        public static System.Boolean DeleteCommentFromBudgetDoc(UniXP.Common.CProfile objProfile,
            System.Guid uuidBudgetDocID)
        {
            System.Boolean bRet = false;

            System.Data.SqlClient.SqlConnection DBConnection = objProfile.GetDBSource();
            if (DBConnection == null)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(
                "Не удалось установить соединение с базой данных.", "Внимание",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                return bRet;
            }
            System.Data.SqlClient.SqlTransaction DBTransaction = DBConnection.BeginTransaction();

            try
            {
                // соединение с БД получено, прописываем команду на создание записи в БД
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
                cmd.Connection = DBConnection;
                cmd.Transaction = DBTransaction;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = System.String.Format("[{0}].[dbo].[sp_DeleteUserCommentForBudgetDoc]", objProfile.GetOptionsDllDBName());
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ((System.Byte)(0)), ((System.Byte)(0)), "", System.Data.DataRowVersion.Current, null));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@BUDGETDOC_GUID_ID", System.Data.SqlDbType.UniqueIdentifier));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ulUniXPUser_ID", System.Data.SqlDbType.Int));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ERROR_NUM", System.Data.SqlDbType.Int, 8, System.Data.ParameterDirection.Output, false, ((System.Byte)(0)), ((System.Byte)(0)), "", System.Data.DataRowVersion.Current, null));
                cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ERROR_MES", System.Data.SqlDbType.NVarChar, 4000));

                cmd.Parameters["@ERROR_MES"].Direction = System.Data.ParameterDirection.Output;

                cmd.Parameters["@BUDGETDOC_GUID_ID"].Value = uuidBudgetDocID;
                cmd.Parameters["@ulUniXPUser_ID"].Value = objProfile.m_nSQLUserID;
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
                    DevExpress.XtraEditors.XtraMessageBox.Show(
                    "Не удалось удалить комментарий у бюджетного документа.\nТекст ошибки: " + (System.String)cmd.Parameters["@ERROR_MES"].Value, "Внимание",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);
                }
                cmd.Dispose();
            }
            catch (System.Exception f)
            {
                // откатываем транзакцию
                DBTransaction.Rollback();
                DevExpress.XtraEditors.XtraMessageBox.Show(
                "Не удалось удалить комментарий у бюджетного документа.\nТекст ошибки: " + f.Message, "Внимание",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
			finally // очищаем занимаемые ресурсы
            {
                DBConnection.Close();
            }
            return bRet;
        }
        #endregion

    }
}
