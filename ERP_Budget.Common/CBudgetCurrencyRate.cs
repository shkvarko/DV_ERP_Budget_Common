using System;
using System.Collections.Generic;
using System.Text;

namespace ERP_Budget.Common
{
    /// <summary>
    /// ����� "������ � ����� ����� �������"
    /// </summary>
    public class CBudgetCurrencyRateItem
    {
        #region ����������, ��������, ���������
        /// <summary>
        /// ������
        /// </summary>
        private CCurrency m_objCurrencyIn;
        /// <summary>
        /// ������
        /// </summary>
        public CCurrency CurrencyIn
        {
            get { return m_objCurrencyIn; }
            set { m_objCurrencyIn = value; }
        }
        /// <summary>
        /// ������, � ������� ����������� CurrencyIn
        /// </summary>
        private CCurrency m_objCurrencyOut;
        /// <summary>
        /// ������, � ������� ����������� CurrencyIn
        /// </summary>
        public CCurrency CurrencyOut
        {
            get { return m_objCurrencyOut; }
            set { m_objCurrencyOut = value; }
        }
        /// <summary>
        /// ����
        /// </summary>
        private System.Double m_moneyValue;
        /// <summary>
        /// ����
        /// </summary>
        public System.Double Value
        {
            get { return m_moneyValue; }
            set { m_moneyValue = value; }
        }

        #endregion

        #region �����������
        public CBudgetCurrencyRateItem()
        {
            this.m_objCurrencyIn = null;
            this.m_objCurrencyOut = null;
            this.m_moneyValue = 0;
        }
        public CBudgetCurrencyRateItem( CCurrency objCurrencyIn, CCurrency objCurrencyOut, System.Double moneyValue )
        {
            this.m_objCurrencyIn = objCurrencyIn;
            this.m_objCurrencyOut = objCurrencyOut;
            this.m_moneyValue = moneyValue;
        }
        #endregion

        #region �������� ������ � ��

        /// <summary>
        /// �������� ������ � ��
        /// </summary>
        /// <param name="objProfile">�������</param>
        /// <param name="cmd">SQL-�������</param>
        /// <param name="uuidBudgetID">�� �������</param>
        /// <returns>true - ������� ����������; false - ������</returns>
        public System.Boolean Add( UniXP.Common.CProfile objProfile, System.Data.SqlClient.SqlCommand cmd,
            System.Guid uuidBudgetID )
        {
            System.Boolean bRet = false;

            if( cmd == null )
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(  "����������� ���������� � ��/", "��������",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning );
                return bRet;
            }

            // ���������� ������� ������ 1
            if( this.m_objCurrencyIn == null )
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(  "������� ������", "��������",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning );
                return bRet;
            }
            // ���������� ������� ������ ���������
            if( this.m_objCurrencyOut == null )
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(  "������� ������ ���������", "��������",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning );
                return bRet;
            }
            // ���������� ������� ����
            if( this.m_moneyValue <= 0 )
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(  "�������� ����", "��������",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning );
                return bRet;
            }

            try
            {
                cmd.Parameters.Clear();
                cmd.CommandText = System.String.Format( "[{0}].[dbo].[sp_AddBudgetCurrencyRate]", objProfile.GetOptionsDllDBName() );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ( ( System.Byte )( 0 ) ), ( ( System.Byte )( 0 ) ), "", System.Data.DataRowVersion.Current, null ) );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@CURRENCY_IN_GUID_ID", System.Data.SqlDbType.UniqueIdentifier, 4 ) );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@CURRENCY_OUT_GUID_ID", System.Data.SqlDbType.UniqueIdentifier, 4 ) );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@BUDGET_GUID_ID", System.Data.SqlDbType.UniqueIdentifier, 4 ) );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@CURRENCYRATE_VALUE", System.Data.SqlDbType.Money, 8 ) );

                cmd.Parameters[ "@CURRENCY_IN_GUID_ID" ].Value = this.m_objCurrencyIn.uuidID;
                cmd.Parameters[ "@CURRENCY_OUT_GUID_ID" ].Value = this.m_objCurrencyOut.uuidID;
                cmd.Parameters[ "@BUDGET_GUID_ID" ].Value = uuidBudgetID;
                cmd.Parameters[ "@CURRENCYRATE_VALUE" ].Value = this.m_moneyValue;
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
                        DevExpress.XtraEditors.XtraMessageBox.Show(  "������ � ��������������� " + this.m_objCurrencyIn.uuidID.ToString() + " �� �������", "��������",
                            System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning );
                        break;
                        case 2:
                        DevExpress.XtraEditors.XtraMessageBox.Show(  "������ � ��������������� " + this.m_objCurrencyOut.uuidID.ToString() + " �� �������", "��������",
                            System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
                        break;
                        case 3:
                        DevExpress.XtraEditors.XtraMessageBox.Show(  "��� �������� ����� ���� ��� ����������", "��������",
                            System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
                        break;
                        case 4:
                        DevExpress.XtraEditors.XtraMessageBox.Show(  "������ � �������� ��������������� �� ������.\n�� �������: " + uuidBudgetID.ToString(), "��������",
                            System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
                        break;
                        default:
                        DevExpress.XtraEditors.XtraMessageBox.Show(  "������ �������� ����� �����", "��������",
                            System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
                        break;
                    }
                }
            }
            catch( System.Exception f )
            {
                DevExpress.XtraEditors.XtraMessageBox.Show( 
                "�� ������� ������� ���� �����.\n\n����� ������: " + f.Message, "��������",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
            }
			finally // ������� ���������� �������
            {
            }
            return bRet;
        }

        #endregion
    }

    /// <summary>
    /// ����� "����� ����� �������"
    /// </summary>
    public class CBudgetCurrencyRate
    {
        #region ����������, ��������, ���������
        /// <summary>
        /// �� �������
        /// </summary>
        private System.Guid m_uuidBudgetID;
        /// <summary>
        /// �� �������
        /// </summary>
        public System.Guid BudgetID
        {
            get { return m_uuidBudgetID; }
            set { m_uuidBudgetID = value; }
        }
        /// <summary>
        /// ������ ������ �����
        /// </summary>
        private List<CBudgetCurrencyRateItem> m_objBudgetCurrencyRateItemList;
        /// <summary>
        /// ������ ������ �����
        /// </summary>
        public List<CBudgetCurrencyRateItem> BudgetCurrencyRateItemList
        {
            get { return m_objBudgetCurrencyRateItemList; }
            set { m_objBudgetCurrencyRateItemList = value; }
        }
        #endregion

        #region �����������
        public CBudgetCurrencyRate( System.Guid uuidBudgetID )
        {
            this.m_uuidBudgetID = uuidBudgetID;
            this.m_objBudgetCurrencyRateItemList = new List<CBudgetCurrencyRateItem>();
        }
        #endregion

        #region ������ ������ �����
        /// <summary>
        /// ��������� ������ ������ �����
        /// </summary>
        /// <param name="objProfile">�������</param>
        /// <returns>true - �������� ����������; false - ������</returns>
        public System.Boolean LoadCurrencyRateList( UniXP.Common.CProfile objProfile, System.Data.SqlClient.SqlCommand cmd )
        {
            System.Boolean bRet = false;

            try
            {
                if( this.m_objBudgetCurrencyRateItemList == null )
                {
                    this.m_objBudgetCurrencyRateItemList = new List<CBudgetCurrencyRateItem>();
                }

                if( cmd == null )
                {
                    DevExpress.XtraEditors.XtraMessageBox.Show(  "����������� ���������� � ��.", "��������",
                        System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning );
                    return bRet;
                }

                cmd.Parameters.Clear();
                cmd.CommandText = System.String.Format( "[{0}].[dbo].[sp_GetBudgetCurrencyRateList]", objProfile.GetOptionsDllDBName() );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ( ( System.Byte )( 0 ) ), ( ( System.Byte )( 0 ) ), "", System.Data.DataRowVersion.Current, null ) );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@BUDGET_GUID_ID", System.Data.SqlDbType.UniqueIdentifier ) );
                cmd.Parameters[ "@BUDGET_GUID_ID" ].Value = this.m_uuidBudgetID;
                System.Data.SqlClient.SqlDataReader rs = cmd.ExecuteReader();
                if( rs.HasRows )
                {
                    // ����� ������ ��������
                    while( rs.Read() )
                    {
                        CCurrency objCurrencyIN = new CCurrency( rs.GetGuid( 0 ), rs.GetString( 3 ), rs.GetString( 4 ) );
                        CCurrency objCurrencyOUT = new CCurrency( rs.GetGuid( 1 ), rs.GetString( 5 ), rs.GetString( 5 ) );
                        System.Double CurrencyRate = ( rs[ 2 ] == System.DBNull.Value ) ? 0 : rs.GetSqlMoney( 2 ).ToDouble();
                        this.m_objBudgetCurrencyRateItemList.Add( new CBudgetCurrencyRateItem( objCurrencyIN,
                            objCurrencyOUT, CurrencyRate ) );
                    }
                    bRet = true;

                }
                else
                {
                    DevExpress.XtraEditors.XtraMessageBox.Show( 
                    "�� ������� �������� ������ ������ �����.\n� �� �� ������� ����������.", "��������",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning );

                }
                rs.Close();
                rs.Dispose();
            }
            catch( System.Exception f )
            {
                DevExpress.XtraEditors.XtraMessageBox.Show( 
                "�� ������� �������� ������ ������ �����.\n\n����� ������: " + f.Message, "��������",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
            }
            finally // ������� ���������� �������
            {
            }
            return bRet;
        }

        /// <summary>
        /// ��������� ������ ������ �����
        /// </summary>
        /// <param name="objProfile">�������</param>
        /// <returns>true - �������� ����������; false - ������</returns>
        public System.Boolean LoadCurrencyRateList( UniXP.Common.CProfile objProfile )
        {
            System.Boolean bRet = false;

            try
            {
                if( this.m_objBudgetCurrencyRateItemList == null )
                {
                    this.m_objBudgetCurrencyRateItemList = new List<CBudgetCurrencyRateItem>();
                }

                System.Data.SqlClient.SqlConnection DBConnection = objProfile.GetDBSource();
                if( DBConnection == null )
                {
                    DevExpress.XtraEditors.XtraMessageBox.Show(  "����������� ���������� � ��.", "��������",
                        System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning );
                    return bRet;
                }

                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
                cmd.Connection = DBConnection;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = System.String.Format( "[{0}].[dbo].[sp_GetBudgetCurrencyRateList]", objProfile.GetOptionsDllDBName() );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ( ( System.Byte )( 0 ) ), ( ( System.Byte )( 0 ) ), "", System.Data.DataRowVersion.Current, null ) );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@BUDGET_GUID_ID", System.Data.SqlDbType.UniqueIdentifier ) );
                cmd.Parameters[ "@BUDGET_GUID_ID" ].Value = this.m_uuidBudgetID;
                System.Data.SqlClient.SqlDataReader rs = cmd.ExecuteReader();
                if( rs.HasRows )
                {
                    // ����� ������ ��������
                    while( rs.Read() )
                    {
                        CCurrency objCurrencyIN = new CCurrency( rs.GetGuid( 0 ), rs.GetString( 3 ), rs.GetString( 4 ) );
                        CCurrency objCurrencyOUT = new CCurrency( rs.GetGuid( 1 ), rs.GetString( 5 ), rs.GetString( 5 ) );
                        System.Double CurrencyRate = ( rs[ 2 ] == System.DBNull.Value ) ? 0 : rs.GetSqlMoney( 2 ).ToDouble();
                        this.m_objBudgetCurrencyRateItemList.Add( new CBudgetCurrencyRateItem( objCurrencyIN,
                            objCurrencyOUT, CurrencyRate ) );
                    }
                    bRet = true;

                }
                else
                {
                    DevExpress.XtraEditors.XtraMessageBox.Show( 
                    "�� ������� �������� ������ ������ �����.\n� �� �� ������� ����������.", "��������",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning );

                }
                rs.Close();
                rs.Dispose();
                cmd.Dispose();
                DBConnection.Close();
            }
            catch( System.Exception f )
            {
                DevExpress.XtraEditors.XtraMessageBox.Show( 
                "�� ������� �������� ������ ������ �����.\n\n����� ������: " + f.Message, "��������",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
            }
            finally // ������� ���������� �������
            {
            }
            return bRet;
        }

        #endregion

        #region ���������� ������ ����� � ��
        /// <summary>
        /// ������� � ������� ����� �����
        /// </summary>
        /// <param name="objProfile">�������</param>
        /// <param name="cmd">SQL-�������</param>
        /// <returns>true - �������� ����������; false - ������</returns>
        private System.Boolean RemoveCurrencyRate( UniXP.Common.CProfile objProfile,
            System.Data.SqlClient.SqlCommand cmd )
        {
            System.Boolean bRet = false;
            if( cmd == null )
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(  "����������� ���������� � ��.", "��������",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
                return bRet;
            }

            try
            {
                cmd.Parameters.Clear();
                cmd.CommandText = System.String.Format( "[{0}].[dbo].[sp_DeleteBudgetCurrencyRate]", objProfile.GetOptionsDllDBName() );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@RETURN_VALUE", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.ReturnValue, false, ( ( System.Byte )( 0 ) ), ( ( System.Byte )( 0 ) ), "", System.Data.DataRowVersion.Current, null ) );
                cmd.Parameters.Add( new System.Data.SqlClient.SqlParameter( "@BUDGET_GUID_ID", System.Data.SqlDbType.UniqueIdentifier ) );
                cmd.Parameters[ "@BUDGET_GUID_ID" ].Value = this.m_uuidBudgetID;
                cmd.ExecuteNonQuery();
                System.Int32 iRet = ( System.Int32 )cmd.Parameters[ "@RETURN_VALUE" ].Value;
                if( iRet == 0 ) { bRet = true; }
                else
                {
                    DevExpress.XtraEditors.XtraMessageBox.Show(  "������ �������� ������ �����.", "������",
                        System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
                }
            }
            catch( System.Exception f )
            {
                DevExpress.XtraEditors.XtraMessageBox.Show( 
                "������ �������� ������ �����.\n\n����� ������: " + f.Message, "��������",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
            }
			finally // ������� ���������� �������
            {
            }
            return bRet;
        }
        /// <summary>
        /// ��������� � �� ������ ������ ����� �������
        /// </summary>
        /// <param name="objProfile">�������</param>
        /// <param name="cmd">SQL-�������</param>
        /// <returns>true - �������� ����������; false - ������</returns>
        public System.Boolean SaveCurrencyRateList( UniXP.Common.CProfile objProfile,
            System.Data.SqlClient.SqlCommand cmd )
        {
            System.Boolean bRet = false;
            if( cmd == null )
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(  "����������� ���������� � ��.", "��������",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
                return bRet;
            }

            // ��������, � �� ������ �� ������
            if( this.m_objBudgetCurrencyRateItemList.Count == 0 )
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(  "������ ������ ����� ����.", "��������",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
                return bRet;
            }

            try
            {
                // ������ ������� ��� ����� � ������� 
                bRet = RemoveCurrencyRate( objProfile, cmd );
                // ... � ������ �� �� ���������
                if( bRet == true )
                {
                    foreach( CBudgetCurrencyRateItem objBudgetCurrencyRateItem in this.m_objBudgetCurrencyRateItemList )
                    {
                        bRet = objBudgetCurrencyRateItem.Add( objProfile, cmd, this.m_uuidBudgetID );
                        if( bRet == false ) { break; }
                    }
                }
            }
            catch( System.Exception f )
            {
                DevExpress.XtraEditors.XtraMessageBox.Show( 
                "�� ������� ��������� ������ ������ ����� � ��.\n\n����� ������: " + f.Message, "��������",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
            }
			finally // ������� ���������� �������
            {
            }

            return bRet;
        }
        #endregion

        public System.Double GetCurrencyRate( System.Guid uuidCurrencyIn, System.Guid uuidCurrencyOut )
        {
            System.Double rateValue = 0;
            try
            {
                if( uuidCurrencyIn.CompareTo( uuidCurrencyOut ) == 0 ) { return 1; }
                if( this.BudgetCurrencyRateItemList.Count == 0 ) { return rateValue; }

                System.Boolean bFind = false;
                foreach( CBudgetCurrencyRateItem objRateItem in this.BudgetCurrencyRateItemList )
                {
                    if( ( objRateItem.CurrencyIn.uuidID.CompareTo( uuidCurrencyIn ) == 0 ) && 
                        ( objRateItem.CurrencyOut.uuidID.CompareTo( uuidCurrencyOut ) == 0 ) )
                    {
                        rateValue = objRateItem.Value;
                        bFind = true;
                        break;
                    }
                }
                // ����� �������� ���� 
                if( bFind == false )
                {
                    foreach( CBudgetCurrencyRateItem objRateItem in this.BudgetCurrencyRateItemList )
                    {
                        if( ( objRateItem.CurrencyIn.uuidID.CompareTo( uuidCurrencyOut ) == 0 ) && 
                        ( objRateItem.CurrencyOut.uuidID.CompareTo( uuidCurrencyIn ) == 0 ) )
                        {
                            rateValue = 1/objRateItem.Value;
                            //rateValue = Math.Round( ( ( 1/objRateItem.Value ) * 1000000 ), 0 )/1000000;
                            bFind = true;
                            break;
                        }
                    }
                }
                // ����� ���������
                if( bFind == false )
                {
                    System.Double rateValueIn = 0;
                    System.Double rateValueOut = 0;

                    foreach( CBudgetCurrencyRateItem objRateItem in this.BudgetCurrencyRateItemList )
                    {
                        if( objRateItem.CurrencyIn.uuidID.CompareTo( uuidCurrencyIn ) == 0 )
                        {
                            rateValueIn = objRateItem.Value;
                        }
                        if( objRateItem.CurrencyIn.uuidID.CompareTo( uuidCurrencyOut ) == 0 )
                        {
                            rateValueOut = objRateItem.Value;
                        }
                    }

                    if( ( rateValueOut != 0 ) && ( rateValueIn != 0 ) )
                    {
                        rateValue = rateValueIn/rateValueOut;
                    }
                }
            }
            catch( System.Exception f )
            {
                DevExpress.XtraEditors.XtraMessageBox.Show( 
                "�� ������� �������� ���� ������.\n\n����� ������: " + f.Message, "��������",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
            }
            return rateValue;
        }

    }

}
