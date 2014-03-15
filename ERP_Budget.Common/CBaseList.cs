using System;
using System.Collections.Generic;
using System.Text;

namespace ERP_Budget.Common
{
    public class CBaseList<T> where T : IBaseListItem
    {
        #region ����������, ��������, ���������
        
        /// <summary>
        /// ������ ��� �������� ��������� ������ IBaseListItem
        /// </summary>
        private System.Collections.ArrayList m_BaseList;
        /// <summary>
        /// ���������� ������������� �������� ��-���������
        /// </summary>
        private System.Guid m_uuidDefaultID;
        /// <summary>
        /// ��� �������� ������ ��������, ������� ���������� ������
        /// </summary>
        private const System.String strItemBaseClassName = "IBaseListItem";
        /// <summary>
        /// ������� ��-���������
        /// </summary>
        /// <remarks>�������� �� ���������</remarks>
        private T m_DefValue;

        /// <summary>
        /// ���������� ������������� �������� ��-���������
        /// </summary>
        public System.Guid DefaultID
        {
            get
            {
                return m_uuidDefaultID;
            }
        }

        /// <summary>
        /// ������� ��-���������
        /// </summary>
        public T DefaultValue
        {
            get
            {
                return m_DefValue;
            }
        }
        #endregion

        public CBaseList()
        {
            m_BaseList = new System.Collections.ArrayList();
            m_BaseList.Clear();
        }

        /// <summary>
        /// ������� ������ m_BaseList
        /// </summary>
        public void ClearList()
        {
            try
            {
                if( m_BaseList == null ) { return; }
                m_BaseList.Clear();
            }
            catch( System.Exception e )
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(  "������ ������� ������\n" + e.Message, "������",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
            }
            return;
        }

        /// <summary>
        /// ���������� ���������� ��������� � ������
        /// </summary>
        /// <returns></returns>
        public System.Int32 GetCountItems()
        {
            System.Int32 iRet = 0;
            try
            {
                if( m_BaseList == null ) { return iRet; }
                iRet = m_BaseList.Count;
            }
            catch( System.Exception e )
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(  "������ ������ ���������� ��������� � ������\n" + e.Message, "������",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
            }
            return iRet;
        }

        /// <summary>
        /// ��������� ������� � ������
        /// </summary>
        /// <param name="objItem">����������� ������</param>
        public void AddItemToList( T objItem )
        {
            try
            {
                if( objItem == null ) { return; }
                if( objItem.GetType().BaseType.Name.Contains( strItemBaseClassName ) )
                {
                    this.m_BaseList.Add( objItem );
                }
            }
            catch( System.Exception e )
            { 
                DevExpress.XtraEditors.XtraMessageBox.Show( 
                    "������ ���������� �������� � ������\n" + e.Message, "������",
                    System.Windows.Forms.MessageBoxButtons.OK,
                    System.Windows.Forms.MessageBoxIcon.Error );

            }
            return;
        }

        /// <summary>
        /// �������� �������� �� ������
        /// </summary>
        /// <param name="objItem">��������� �������</param>
        public System.Boolean RemoveItemFromList( T objItem )
        {
            System.Boolean bRet = false;
            try
            {
                if( objItem == null ) { return bRet; }
                if( this.m_BaseList.Count == 0 ) 
                {
                    DevExpress.XtraEditors.XtraMessageBox.Show(  "������ ����", "��������",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning );
                    return bRet;
                }
                if( objItem.GetType().BaseType.Name.Contains( strItemBaseClassName ) )
                {
                    this.m_BaseList.Remove( objItem );
                    bRet = true;
                }
            }
            catch( System.Exception e )
            {
                DevExpress.XtraEditors.XtraMessageBox.Show( 
                    "������ �������� �������� �� ������\n" + e.Message, "������",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
            }
            return bRet;
        }

        /// <summary>
        /// �������� �������� �� ������
        /// </summary>
        /// <param name="iIndex">������ ���������� ��������</param>
        public System.Boolean RemoveItemFromListAt( System.Int32 iIndex )
        {
            System.Boolean bRet = false;
            try
            {
                if( this.m_BaseList.Count == 0 )
                {
                    DevExpress.XtraEditors.XtraMessageBox.Show(  "������ ����", "��������",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning );
                    return bRet;
                }
                if( ( iIndex < 0 ) || ( iIndex > ( this.m_BaseList.Count - 1 ) ) )
                {
                    DevExpress.XtraEditors.XtraMessageBox.Show(  "������ ������� �� ������� ������", "��������",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning );
                    return bRet;
                }
                this.m_BaseList.RemoveAt( iIndex );
                bRet = true;
            }
            catch( System.Exception e )
            {
                DevExpress.XtraEditors.XtraMessageBox.Show( 
                    "������ �������� �������� �� ������\n" + e.Message, "������",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
            }
            return bRet;
        }
        /// <summary>
        /// ���������� ������ �� ��������� ������� �������
        /// </summary>
        /// <param name="iIndx">������</param>
        /// <returns>������� ������� m_BaseList</returns>
        public T GetByIndex( System.Int32 iIndx )
        {
            T tRet = default( T );
            try
            {
                if( this.m_BaseList.Count == 0 )
                {
                    DevExpress.XtraEditors.XtraMessageBox.Show(  "������ ����", "��������",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning );
                    return tRet;
                }
                if( ( iIndx < 0 ) || ( iIndx > ( this.m_BaseList.Count - 1 ) ) )
                {
                    DevExpress.XtraEditors.XtraMessageBox.Show(  "������ �������� ������� �� ������� �������", "��������",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning );
                    return tRet;
                }
                tRet = ( T )m_BaseList[ iIndx ];
            }
            catch( System.Exception e )
            {
                DevExpress.XtraEditors.XtraMessageBox.Show( 
                    "������ ������ �������� � ������ �� ������� �������\n" + e.Message, "������",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
            }
            return tRet;
        }

        /// <summary>
        /// ����� �������� � ������ �� ����������� ��������������
        /// </summary>
        /// <param name="uuidID">���������� ������������� �������� �������</param>
        public T FindByID( System.Guid uuidID )
        {
            T tRet = default( T );
            try
            {
                if( this.m_BaseList.Count == 0 )
                {
                    DevExpress.XtraEditors.XtraMessageBox.Show(  "������ ����", "��������",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning );
                    return tRet;
                }
                foreach( T objItem in this.m_BaseList )
                { 
                    if( objItem.m_uuidID.CompareTo( uuidID ) == 0 )
                    {
                        tRet = objItem;
                        break;
                    }
                }
            }
            catch( System.Exception e )
            {
                DevExpress.XtraEditors.XtraMessageBox.Show( 
                    "������ ������ �������� � ������ �� ����������� ��������������\n" + e.Message, "������",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
            }
            return tRet;
        }

        /// <summary>
        /// ����� �������� � ������ �� �����
        /// </summary>
        /// <param name="strName">��� �������� �������</param>
        public T FindByName( System.String strName )
        {
            T tRet = default( T );
            try
            {
                if( this.m_BaseList.Count == 0 )
                {
                    DevExpress.XtraEditors.XtraMessageBox.Show(  "������ ����", "��������",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning );
                    return tRet;
                }
                foreach( T objItem in this.m_BaseList )
                {
                    if( objItem.m_strName == strName )
                    {
                        tRet = objItem;
                        break;
                    }
                }
            }
            catch( System.Exception e )
            {
                DevExpress.XtraEditors.XtraMessageBox.Show( 
                    "������ ������ �������� � ������ �� �����\n" + e.Message, "������",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
            }
            return tRet;
        }

        /// <summary>
        /// ������������� �������� ��-���������
        /// </summary>
        public void SetDefaultValue( System.Guid uuidID )
        {
            try
            {
                if( this.m_BaseList.Count == 0 ) 
                {
                    DevExpress.XtraEditors.XtraMessageBox.Show(  "������ ����", "��������",
                        System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning );
                    return;
                }
                if( uuidID.CompareTo( System.Guid.Empty ) == 0 ) 
                {
                    DevExpress.XtraEditors.XtraMessageBox.Show(  "���������� ������������� ������", "��������",
                        System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning );
                    return; 
                }
                this.m_uuidDefaultID = uuidID;
                m_DefValue = FindByID( m_uuidDefaultID );
            }
            catch( System.Exception e )
            {
                DevExpress.XtraEditors.XtraMessageBox.Show( 
                    "������ ��������� �������� ��-���������\n" + e.Message, "������",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
            }
            return ;
        }

    }
}
