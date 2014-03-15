using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

namespace ERP_Budget.Common
{
    class BooleanTypeConverter : BooleanConverter
    {
        public override object ConvertTo(ITypeDescriptorContext context,
          System.Globalization.CultureInfo culture,
          object value,
          Type destType)
        {
            return (bool)value ?
              "Да" : "Нет";
        }

        public override object ConvertFrom(ITypeDescriptorContext context,
          System.Globalization.CultureInfo culture,
          object value)
        {
            return (string)value == "Да";
        }
    }

    public abstract class IBaseListItem
    {
        /// <summary>
        /// Уникальный идентификатор
        /// </summary>
        public System.Guid m_uuidID;
        /// <summary>
        /// Уникальный идентификатор
        /// </summary>
        [DisplayName( "Уникальный идентификатор" )]
        [Description( "Уникальный идентификатор объекта" )]
        [ReadOnly( true )]
        [Browsable( false)]
        [Category( "1. Обязательные значения" )]
        public System.Guid uuidID
        {
            get { return m_uuidID; }
            set { m_uuidID = value; }
        }
        /// <summary>
        /// Имя
        /// </summary>
        public System.String m_strName;
        /// <summary>
        /// Имя
        /// </summary>
        [DisplayName( "Наименование" )]
        [Description( "Наименование объекта" )]
        [Category( "1. Обязательные значения" )]
        public System.String Name
        {
            get { return m_strName; }
            set { m_strName = value; }
        }

        /// <summary>
        /// Инициализация свойств
        /// </summary>
        /// <param name="objProfile">профайл</param>
        /// <param name="uuidID">уникальный идентификатор объекта</param>
        /// <returns>true - удачное завершение; false - ошибка</returns>
        public abstract System.Boolean Init( UniXP.Common.CProfile objProfile, System.Guid uuidID );

        /// <summary>
        /// Добавить запись в БД
        /// </summary>
        /// <param name="objProfile">профайл</param>
        /// <returns>true - удачное завершение; false - ошибка</returns>
        public abstract System.Boolean Add( UniXP.Common.CProfile objProfile );

        /// <summary>
        /// Удалить запись из БД
        /// </summary>
        /// <param name="objProfile">профайл</param>
        /// <param name="uuidID">уникальный идентификатор объекта</param>
        /// <returns>true - удачное завершение; false - ошибка</returns>
        public abstract System.Boolean Remove( UniXP.Common.CProfile objProfile );

        /// <summary>
        /// Сохранить изменения в БД
        /// </summary>
        /// <param name="objProfile">профайл</param>
        /// <returns>true - удачное завершение; false - ошибка</returns>
        public abstract System.Boolean Update( UniXP.Common.CProfile objProfile );
    }
}
