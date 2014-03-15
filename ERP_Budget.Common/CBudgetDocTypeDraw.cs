using System;
using System.Collections.Generic;
using System.Text;

namespace ERP_Budget.Common
{
    public class CBudgetDocTypeDraw
    {
        #region Переменные, свойства, константы 
        private System.Int32 m_BackColorRed;
        public System.Int32 BackColorRed
        {
            get{ return m_BackColorRed; }
        }
        private System.Int32 m_BackColorGreen;
        public System.Int32 BackColorGreen
        {
            get{ return m_BackColorGreen; }
        }
        private System.Int32 m_BackColorBlue;
        public System.Int32 BackColorBlue
        {
            get{ return m_BackColorBlue; }
        }
        public System.Drawing.Image ImageDocType {get; set;}
        public System.Drawing.Image ImageDocTypeSmall {get; set;}
        #endregion

        #region Конструкторы 
        public CBudgetDocTypeDraw()
        {
            this.m_BackColorGreen = 0;
            this.m_BackColorGreen = 0;
            this.m_BackColorRed = 0;
            this.ImageDocType = null;
            this.ImageDocTypeSmall = null;
        }
        #endregion

        #region Свойства отрисовки типа бюджетного документа 
        public static CBudgetDocTypeDraw GetBudgetDocTypeDraw( UniXP.Common.CProfile objProfile,
            CBudgetDocType objBudgetDocType, System.Data.SqlClient.SqlCommand cmd )
        {
            CBudgetDocTypeDraw objBudgetDocTypeDraw = null;

            System.Data.SqlClient.SqlConnection DBConnection = objProfile.GetDBSource();
            if( DBConnection == null ) { return objBudgetDocTypeDraw; }

            try
            {
                // соединение с БД получено, прописываем команду на выборку данных
                cmd.Parameters.Clear();
                cmd.CommandText = System.String.Format( "[{0}].[dbo].[sp_GetBudgetDocTypeDraw]", objProfile.GetOptionsDllDBName() );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ( ( System.Byte )( 0 ) ), ( ( System.Byte )( 0 ) ), "", System.Data.DataRowVersion.Current, null ) );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@GUID_ID", System.Data.SqlDbType.UniqueIdentifier ) );
                cmd.Parameters[ "@GUID_ID" ].Value = objBudgetDocType.uuidID;
                System.Data.SqlClient.SqlDataReader rs = cmd.ExecuteReader();
                if( rs.HasRows )
                {
                    // набор данных непустой, в нем нас интересует одна запись
                    rs.Read();
                    objBudgetDocTypeDraw = new CBudgetDocTypeDraw();

                    objBudgetDocTypeDraw.m_BackColorRed = System.Convert.ToInt32( rs["BACKCOLOR_RGB_RED"] );
                    objBudgetDocTypeDraw.m_BackColorGreen = System.Convert.ToInt32(rs["BACKCOLOR_RGB_GREEN"]);
                    objBudgetDocTypeDraw.m_BackColorBlue = System.Convert.ToInt32(rs["BACKCOLOR_RGB_BLUE"]);

                    byte[] b = rs.GetSqlBytes( 3 ).Buffer;
                    byte[] b2 = rs.GetSqlBytes( 4 ).Buffer;

                    System.IO.MemoryStream mem = new System.IO.MemoryStream( b );
                    System.IO.MemoryStream mem2 = new System.IO.MemoryStream( b2 );
                    objBudgetDocTypeDraw.ImageDocType = System.Drawing.Image.FromStream( mem );
                    objBudgetDocTypeDraw.ImageDocTypeSmall = System.Drawing.Image.FromStream(mem2);

                    mem = null;
                    mem2 = null;
                }

                rs.Close();
                rs.Dispose();
                cmd.Dispose();
            }
            catch( System.Exception f )
            {
                DevExpress.XtraEditors.XtraMessageBox.Show( 
                "Не удалось получить свойства отрисовки типа бюджетного документа.\n\nТекст ошибки: " + f.Message, "Внимание",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
            }
			finally // очищаем занимаемые ресурсы
            {
                DBConnection.Close();
            }
            return objBudgetDocTypeDraw;
        }

        public static CBudgetDocTypeDraw GetBudgetDocTypeDraw( UniXP.Common.CProfile objProfile,
            CBudgetDocType objBudgetDocType )
        {
            CBudgetDocTypeDraw objBudgetDocTypeDraw = null;

            System.Data.SqlClient.SqlConnection DBConnection = objProfile.GetDBSource();
            if( DBConnection == null ) { return objBudgetDocTypeDraw; }

            try
            {
                // соединение с БД получено, прописываем команду на выборку данных
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
                cmd.Connection = DBConnection;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = System.String.Format( "[{0}].[dbo].[sp_GetBudgetDocTypeDraw]", objProfile.GetOptionsDllDBName() );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ( ( System.Byte )( 0 ) ), ( ( System.Byte )( 0 ) ), "", System.Data.DataRowVersion.Current, null ) );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@GUID_ID", System.Data.SqlDbType.UniqueIdentifier ) );
                cmd.Parameters[ "@GUID_ID" ].Value = objBudgetDocType.uuidID;
                System.Data.SqlClient.SqlDataReader rs = cmd.ExecuteReader();
                if( rs.HasRows )
                {
                    // набор данных непустой, в нем нас интересует одна запись
                    rs.Read();
                    objBudgetDocTypeDraw = new CBudgetDocTypeDraw();

                    objBudgetDocTypeDraw.m_BackColorRed = rs.GetInt32( 0 );
                    objBudgetDocTypeDraw.m_BackColorGreen = rs.GetInt32( 1 );
                    objBudgetDocTypeDraw.m_BackColorBlue = rs.GetInt32( 2 );

                    byte[] b = rs.GetSqlBytes( 3 ).Buffer;
                    byte[] b2 = rs.GetSqlBytes( 4 ).Buffer;

                    System.IO.MemoryStream mem = new System.IO.MemoryStream( b );
                    System.IO.MemoryStream mem2 = new System.IO.MemoryStream( b2 );
                    objBudgetDocTypeDraw.ImageDocType = System.Drawing.Image.FromStream(mem);
                    objBudgetDocTypeDraw.ImageDocTypeSmall = System.Drawing.Image.FromStream( mem2 );
                    
                    mem = null;
                    mem2 = null;
                }
                //else
                //{
                //    DevExpress.XtraEditors.XtraMessageBox.Show( 
                //    "Не удалось проинициализировать класс CBudgetDocTypeDraw.\nВ БД не найдена информация.", "Внимание",
                //    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning );

                //}
                rs.Close();
                rs.Dispose();
                cmd.Dispose();
            }
            catch( System.Exception f )
            {
                DevExpress.XtraEditors.XtraMessageBox.Show( 
                "Не удалось получить свойства отрисовки типа бюджетного документа.\n\nТекст ошибки: " + f.Message, "Внимание",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
            }
			finally // очищаем занимаемые ресурсы
            {
                DBConnection.Close();
            }
            return objBudgetDocTypeDraw;
        }
        #endregion 

    }
}
