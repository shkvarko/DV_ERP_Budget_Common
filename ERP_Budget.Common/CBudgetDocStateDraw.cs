using System;
using System.Collections.Generic;
using System.Text;

namespace ERP_Budget.Common
{
    public class CBudgetDocStateDraw
    {
        #region Переменные, свойства, константы 
        private System.Drawing.Image m_ImageDocStateSmall;
        public System.Drawing.Image ImageDocStateSmall
        {
            get {return m_ImageDocStateSmall;}
        }
        #endregion

        #region Конструкторы 
        public CBudgetDocStateDraw()
        {
            this.m_ImageDocStateSmall = null;
        }
        #endregion

        #region Свойства отрисовки состояния бюджетного документа 
        public static CBudgetDocStateDraw GetBudgetDocStateDraw( UniXP.Common.CProfile objProfile,
            CBudgetDocState objBudgetDocState, System.Data.SqlClient.SqlCommand cmd )
        {
            CBudgetDocStateDraw objBudgetDocStateDraw = null;

            System.Data.SqlClient.SqlConnection DBConnection = objProfile.GetDBSource();
            if( DBConnection == null ) { return objBudgetDocStateDraw; }

            try
            {
                // соединение с БД получено, прописываем команду на выборку данных
                cmd.Parameters.Clear();
                cmd.CommandText = System.String.Format( "[{0}].[dbo].[sp_GetBudgetDocStateDraw]", objProfile.GetOptionsDllDBName() );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ( ( System.Byte )( 0 ) ), ( ( System.Byte )( 0 ) ), "", System.Data.DataRowVersion.Current, null ) );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@GUID_ID", System.Data.SqlDbType.UniqueIdentifier ) );
                cmd.Parameters[ "@GUID_ID" ].Value = objBudgetDocState.uuidID;
                System.Data.SqlClient.SqlDataReader rs = cmd.ExecuteReader();
                if( rs.HasRows )
                {
                    // набор данных непустой, в нем нас интересует одна запись
                    rs.Read();
                    byte[] b;
                    objBudgetDocStateDraw = new CBudgetDocStateDraw();
                    b = rs.GetSqlBytes( 0 ).Buffer;
                    System.IO.MemoryStream mem = new System.IO.MemoryStream( b );
                    objBudgetDocStateDraw.m_ImageDocStateSmall = System.Drawing.Image.FromStream( mem );
                }
                else
                {
                    DevExpress.XtraEditors.XtraMessageBox.Show( 
                    "Не удалось проинициализировать класс CBudgetDocStateDraw.\nВ БД не найдена информация.", "Внимание",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning );

                }
                rs.Close();
                rs.Dispose();
                cmd.Dispose();
            }
            catch( System.Exception f )
            {
                DevExpress.XtraEditors.XtraMessageBox.Show( 
                "Не удалось получить свойства отрисовки состояния бюджетного документа.\n\nТекст ошибки: " + f.Message, "Внимание",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
            }
			finally // очищаем занимаемые ресурсы
            {
                DBConnection.Close();
            }
            return objBudgetDocStateDraw;
        }

        public static CBudgetDocStateDraw GetBudgetDocStateDraw( UniXP.Common.CProfile objProfile,
            CBudgetDocState objBudgetDocState )
        {
            CBudgetDocStateDraw objBudgetDocStateDraw = null;

            System.Data.SqlClient.SqlConnection DBConnection = objProfile.GetDBSource();
            if( DBConnection == null ) { return objBudgetDocStateDraw; }

            try
            {
                // соединение с БД получено, прописываем команду на выборку данных
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
                cmd.Connection = DBConnection;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = System.String.Format( "[{0}].[dbo].[sp_GetBudgetDocStateDraw]", objProfile.GetOptionsDllDBName() );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ( ( System.Byte )( 0 ) ), ( ( System.Byte )( 0 ) ), "", System.Data.DataRowVersion.Current, null ) );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@GUID_ID", System.Data.SqlDbType.UniqueIdentifier ) );
                cmd.Parameters[ "@GUID_ID" ].Value = objBudgetDocState.uuidID;
                System.Data.SqlClient.SqlDataReader rs = cmd.ExecuteReader();
                if( rs.HasRows )
                {
                    // набор данных непустой, в нем нас интересует одна запись
                    rs.Read();
                    byte[] b;
                    objBudgetDocStateDraw = new CBudgetDocStateDraw();
                    b = rs.GetSqlBytes( 0 ).Buffer;
                    System.IO.MemoryStream mem = new System.IO.MemoryStream( b );
                    objBudgetDocStateDraw.m_ImageDocStateSmall = System.Drawing.Image.FromStream( mem );
                }
                else
                {
                    DevExpress.XtraEditors.XtraMessageBox.Show( 
                    "Не удалось проинициализировать класс CBudgetDocStateDraw.\nВ БД не найдена информация.", "Внимание",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning );
                }
                rs.Close();
                rs.Dispose();
                cmd.Dispose();
            }
            catch( System.Exception f )
            {
                DevExpress.XtraEditors.XtraMessageBox.Show( 
                "Не удалось получить свойства отрисовки состояния бюджетного документа.\n\nТекст ошибки: " + f.Message, "Внимание",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
            }
			finally // очищаем занимаемые ресурсы
            {
                DBConnection.Close();
            }
            return objBudgetDocStateDraw;
        }
        #endregion 

    }
}
