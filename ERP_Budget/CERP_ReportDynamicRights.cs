using UniXP.Common;

namespace ERP_Budget
   {
    public class CERP_BudgetDynamicRights : UniXP.Common.IDynamicRights
      {
        public CERP_BudgetDynamicRights()
         {
             m_strDBName = "ERP_Budget";
         }
      /// <summary>
      /// Имя базы данных.
      /// </summary>
      private System.String m_strDBName;
      /// <summary>
      /// Возвращает имя базы данных.
      /// </summary>
      public override System.String GetDBName()
         {
         return m_strDBName;
         }
      /// <summary>
      /// Загружает значения элементов прав пользователя из базы данных.
      /// </summary>
      /// <param name="objProfile">Профиль пользователя.</param>
      /// <param name="nSQLUserID">Идентификатор пользователя.</param>
      public override System.Boolean LoadState( UniXP.Common.CProfile objProfile, System.Int32 nSQLUserID )
         {
         System.Data.SqlClient.SqlConnection db;
         System.Data.SqlClient.SqlCommand cmd;
         System.Data.SqlClient.SqlDataReader rs;
         System.Boolean bRetCode = false;

         try
            {
            db = objProfile.GetDBSource();
            cmd = new System.Data.SqlClient.SqlCommand();
            cmd.Connection = db;
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = System.String.Format( "[{0}].[dbo].[GetRightsFromID]", GetDBName() );

            cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@UniXPUserID", System.Data.SqlDbType.Int, 4 ) ).Value = nSQLUserID;
            cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@RightName", System.Data.DbType.String ) ).Value = "";

            rs = cmd.ExecuteReader();
            while( rs.Read() )
               {
               System.String strName;
               System.String strDescription;
               System.UInt32 uiUser;
               System.Boolean bState;
               UniXP.Common.RIGHTINFO ri = new RIGHTINFO();

               strName = rs.GetString( 0 );
               strDescription = rs.GetString( 1 );
               uiUser = ( System.UInt32 ) rs.GetInt32( 2 );
               bState = rs.GetBoolean( 3 );
               ri.strName = strName;
               ri.strDescription = strDescription;
               ri.bState = bState;
               m_arRightsInfo[ strName ] = ri;
               }
            rs.Close();
            db.Close();
            bRetCode = true;
            }
            catch( System.Exception e )
            {
                // При работе с базой данных произошла ошибка
                System.Windows.Forms.MessageBox.Show( null, "LoadState\n" + e.Message,
                   "Ошибка", System.Windows.Forms.MessageBoxButtons.OK,
                   System.Windows.Forms.MessageBoxIcon.Error );
            }

         return bRetCode;
         }
      /// <summary>
      /// Сохраняет состояния элементов прав пользователя.
      /// </summary>
      /// <param name="objProfile">Профиль пользователя.</param>
      /// <param name="nSQLUserID">Идентификатор пользователя.</param>
      public override System.Boolean SaveState( UniXP.Common.CProfile objProfile, System.Int32 nSQLUserID )
         {
         System.Data.SqlClient.SqlConnection db;
         System.Data.SqlClient.SqlCommand cmd;
         System.Boolean bRetCode = false;
         System.Int32 res = 0;

         try
            {
            db = objProfile.GetDBSource();
            cmd = new System.Data.SqlClient.SqlCommand();
            cmd.Connection = db;
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = System.String.Format( "[{0}].[dbo].[SaveStateFromID]", GetDBName() );
            cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ( ( System.Byte ) ( 0 ) ), ( ( System.Byte ) ( 0 ) ), "", System.Data.DataRowVersion.Current, null ) );
            cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@UniXPUserID", System.Data.SqlDbType.Int, 4 ) );
            cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@RightName", System.Data.DbType.String ) );
            cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@bState", System.Data.SqlDbType.Bit ) );

            MoveFirst();
            for( System.Int32 ii = 0; ii < m_arRightsInfo.Count; ii++ )
               {
               cmd.Parameters[ "@UniXPUserID" ].Value = nSQLUserID;
               cmd.Parameters[ "@RightName" ].Value = m_strRightName;
               cmd.Parameters[ "@bState" ].Value = m_bRightState;
               cmd.ExecuteNonQuery();
               res = ( System.Int32 ) cmd.Parameters[ "@RETURN_VALUE" ].Value;
               if( res != 0 )
                  {
                  //Нужно будет написать что-нибудь злобное.
                  }
               MoveNext();
               }
            db.Close();
            bRetCode = true;
            }
         catch
            {
            // При работе с базой данных произошла ошибка
            bRetCode = false;
            }

         return bRetCode;
         }
      }

   public class DynamicRights : PlugIn.IDynamicRights
      {
      public UniXP.Common.IDynamicRights GetDynamicRights()
         {
             return new CERP_BudgetDynamicRights();
         }
      }
   }
