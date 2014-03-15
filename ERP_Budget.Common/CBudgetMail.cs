using System;
using System.Collections.Generic;
using System.Text;

namespace ERP_Budget.Common
{
    /// <summary>
    /// Класс для создания почтовых сообщений
    /// </summary>
    public class CBudgetMail
    {
        /// <summary>
        /// Создает почтовое сообщение и помещает его в очередь
        /// </summary>
        /// <param name="objProfile">профайл</param>
        /// <param name="cmd">SQL-команда</param>
        /// <param name="uuidBudgetDocID">УИ бюджетного документа</param>
        /// <returns>true - удачное завершение операции; false - ошибка</returns>
        public static System.Boolean AddMailInQueue( UniXP.Common.CProfile objProfile, 
            System.Data.SqlClient.SqlCommand cmd, System.Guid uuidBudgetDocID )
        {
            System.Boolean bRet = false;
            if( cmd == null ) { return bRet; }
            try
            {
                // соединение с БД получено, прописываем команду на создание записи в БД
                cmd.Parameters.Clear();
                cmd.CommandText = System.String.Format( "[{0}].[dbo].[sp_AddMail]", objProfile.GetOptionsDllDBName() );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ( ( System.Byte )( 0 ) ), ( ( System.Byte )( 0 ) ), "", System.Data.DataRowVersion.Current, null ) );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@BUDGETDOC_GUID_ID", System.Data.SqlDbType.UniqueIdentifier, 4 ) );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@ERROR_NUM", System.Data.SqlDbType.Int, 8, System.Data.ParameterDirection.Output, false, ( ( System.Byte )( 0 ) ), ( ( System.Byte )( 0 ) ), "", System.Data.DataRowVersion.Current, null ) );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@ERROR_MES", System.Data.SqlDbType.NVarChar, 4000 ) );
                cmd.Parameters[ "@ERROR_MES" ].Direction = System.Data.ParameterDirection.Output;

                cmd.Parameters[ "@BUDGETDOC_GUID_ID" ].Value = uuidBudgetDocID;
                cmd.ExecuteNonQuery();
                System.Int32 iRet = ( System.Int32 )cmd.Parameters[ "@RETURN_VALUE" ].Value;
                if( iRet == 0 )
                {
                    bRet = true;
                }
                else
                {
                    DevExpress.XtraEditors.XtraMessageBox.Show( "Ошибка создания почтового сообщения.\nТекст ошибки: " +
                                ( System.String )cmd.Parameters[ "@ERROR_MES" ].Value, "Внимание",
                        System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
                }
            }
            catch( System.Exception f )
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(
                    "Ошибка создания почтового сообщения.\n\nТекст ошибки: " + f.Message, "Внимание",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
            }
			finally // очищаем занимаемые ресурсы
            {
            }
            return bRet;
        }
    }
}
