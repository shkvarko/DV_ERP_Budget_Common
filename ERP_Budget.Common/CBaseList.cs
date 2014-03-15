using System;
using System.Collections.Generic;
using System.Text;

namespace ERP_Budget.Common
{
    public class CBaseList<T> where T : IBaseListItem
    {
        #region Переменные, Свойства, Константы
        
        /// <summary>
        /// Список для хранения элементов класса IBaseListItem
        /// </summary>
        private System.Collections.ArrayList m_BaseList;
        /// <summary>
        /// Уникальный идентификатор элемента по-умолчанию
        /// </summary>
        private System.Guid m_uuidDefaultID;
        /// <summary>
        /// Имя базового класса объектов, которые составляют список
        /// </summary>
        private const System.String strItemBaseClassName = "IBaseListItem";
        /// <summary>
        /// Элемент по-умолчанию
        /// </summary>
        /// <remarks>Значение по умолчанию</remarks>
        private T m_DefValue;

        /// <summary>
        /// Уникальный идентификатор элемента по-умолчанию
        /// </summary>
        public System.Guid DefaultID
        {
            get
            {
                return m_uuidDefaultID;
            }
        }

        /// <summary>
        /// Элемент по-умолчанию
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
        /// Очищает список m_BaseList
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
                DevExpress.XtraEditors.XtraMessageBox.Show(  "Ошибка очистки списка\n" + e.Message, "Ошибка",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
            }
            return;
        }

        /// <summary>
        /// Возвращает количество элементов в списке
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
                DevExpress.XtraEditors.XtraMessageBox.Show(  "Ошибка поиска количества элементов в списке\n" + e.Message, "Ошибка",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
            }
            return iRet;
        }

        /// <summary>
        /// Добавляет элемент в список
        /// </summary>
        /// <param name="objItem">добавляемый объект</param>
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
                    "Ошибка добавления элемента в список\n" + e.Message, "Ошибка",
                    System.Windows.Forms.MessageBoxButtons.OK,
                    System.Windows.Forms.MessageBoxIcon.Error );

            }
            return;
        }

        /// <summary>
        /// Удаление элемента из списка
        /// </summary>
        /// <param name="objItem">удаляемый элемент</param>
        public System.Boolean RemoveItemFromList( T objItem )
        {
            System.Boolean bRet = false;
            try
            {
                if( objItem == null ) { return bRet; }
                if( this.m_BaseList.Count == 0 ) 
                {
                    DevExpress.XtraEditors.XtraMessageBox.Show(  "Список пуст", "Внимание",
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
                    "Ошибка удаления элемента из списка\n" + e.Message, "Ошибка",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
            }
            return bRet;
        }

        /// <summary>
        /// Удаление элемента из списка
        /// </summary>
        /// <param name="iIndex">индекс удаляемого элемента</param>
        public System.Boolean RemoveItemFromListAt( System.Int32 iIndex )
        {
            System.Boolean bRet = false;
            try
            {
                if( this.m_BaseList.Count == 0 )
                {
                    DevExpress.XtraEditors.XtraMessageBox.Show(  "Список пуст", "Внимание",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning );
                    return bRet;
                }
                if( ( iIndex < 0 ) || ( iIndex > ( this.m_BaseList.Count - 1 ) ) )
                {
                    DevExpress.XtraEditors.XtraMessageBox.Show(  "Индекс выходит за границы списка", "Внимание",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning );
                    return bRet;
                }
                this.m_BaseList.RemoveAt( iIndex );
                bRet = true;
            }
            catch( System.Exception e )
            {
                DevExpress.XtraEditors.XtraMessageBox.Show( 
                    "Ошибка удаления элемента из списка\n" + e.Message, "Ошибка",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
            }
            return bRet;
        }
        /// <summary>
        /// Возвращает объект по заданному индексу массива
        /// </summary>
        /// <param name="iIndx">индекс</param>
        /// <returns>элемент массива m_BaseList</returns>
        public T GetByIndex( System.Int32 iIndx )
        {
            T tRet = default( T );
            try
            {
                if( this.m_BaseList.Count == 0 )
                {
                    DevExpress.XtraEditors.XtraMessageBox.Show(  "Список пуст", "Внимание",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning );
                    return tRet;
                }
                if( ( iIndx < 0 ) || ( iIndx > ( this.m_BaseList.Count - 1 ) ) )
                {
                    DevExpress.XtraEditors.XtraMessageBox.Show(  "Индекс элемента выходит за границы массива", "Внимание",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning );
                    return tRet;
                }
                tRet = ( T )m_BaseList[ iIndx ];
            }
            catch( System.Exception e )
            {
                DevExpress.XtraEditors.XtraMessageBox.Show( 
                    "Ошибка поиска элемента в списке по индексу массива\n" + e.Message, "Ошибка",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
            }
            return tRet;
        }

        /// <summary>
        /// Поиск элемента в списке по уникальному идентификатору
        /// </summary>
        /// <param name="uuidID">уникальный идентификатор искомого объекта</param>
        public T FindByID( System.Guid uuidID )
        {
            T tRet = default( T );
            try
            {
                if( this.m_BaseList.Count == 0 )
                {
                    DevExpress.XtraEditors.XtraMessageBox.Show(  "Список пуст", "Внимание",
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
                    "Ошибка поиска элемента в списке по уникальному идентификатору\n" + e.Message, "Ошибка",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
            }
            return tRet;
        }

        /// <summary>
        /// Поиск элемента в списке по имени
        /// </summary>
        /// <param name="strName">имя искомого объекта</param>
        public T FindByName( System.String strName )
        {
            T tRet = default( T );
            try
            {
                if( this.m_BaseList.Count == 0 )
                {
                    DevExpress.XtraEditors.XtraMessageBox.Show(  "Список пуст", "Внимание",
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
                    "Ошибка поиска элемента в списке по имени\n" + e.Message, "Ошибка",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
            }
            return tRet;
        }

        /// <summary>
        /// Устанавливает значение по-умолчанию
        /// </summary>
        public void SetDefaultValue( System.Guid uuidID )
        {
            try
            {
                if( this.m_BaseList.Count == 0 ) 
                {
                    DevExpress.XtraEditors.XtraMessageBox.Show(  "Список пуст", "Внимание",
                        System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning );
                    return;
                }
                if( uuidID.CompareTo( System.Guid.Empty ) == 0 ) 
                {
                    DevExpress.XtraEditors.XtraMessageBox.Show(  "Уникальный идентификатор пустой", "Внимание",
                        System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning );
                    return; 
                }
                this.m_uuidDefaultID = uuidID;
                m_DefValue = FindByID( m_uuidDefaultID );
            }
            catch( System.Exception e )
            {
                DevExpress.XtraEditors.XtraMessageBox.Show( 
                    "Ошибка установки значения по-умолчанию\n" + e.Message, "Ошибка",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
            }
            return ;
        }

    }
}
