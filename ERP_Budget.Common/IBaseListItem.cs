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
              "��" : "���";
        }

        public override object ConvertFrom(ITypeDescriptorContext context,
          System.Globalization.CultureInfo culture,
          object value)
        {
            return (string)value == "��";
        }
    }

    public abstract class IBaseListItem
    {
        /// <summary>
        /// ���������� �������������
        /// </summary>
        public System.Guid m_uuidID;
        /// <summary>
        /// ���������� �������������
        /// </summary>
        [DisplayName( "���������� �������������" )]
        [Description( "���������� ������������� �������" )]
        [ReadOnly( true )]
        [Browsable( false)]
        [Category( "1. ������������ ��������" )]
        public System.Guid uuidID
        {
            get { return m_uuidID; }
            set { m_uuidID = value; }
        }
        /// <summary>
        /// ���
        /// </summary>
        public System.String m_strName;
        /// <summary>
        /// ���
        /// </summary>
        [DisplayName( "������������" )]
        [Description( "������������ �������" )]
        [Category( "1. ������������ ��������" )]
        public System.String Name
        {
            get { return m_strName; }
            set { m_strName = value; }
        }

        /// <summary>
        /// ������������� �������
        /// </summary>
        /// <param name="objProfile">�������</param>
        /// <param name="uuidID">���������� ������������� �������</param>
        /// <returns>true - ������� ����������; false - ������</returns>
        public abstract System.Boolean Init( UniXP.Common.CProfile objProfile, System.Guid uuidID );

        /// <summary>
        /// �������� ������ � ��
        /// </summary>
        /// <param name="objProfile">�������</param>
        /// <returns>true - ������� ����������; false - ������</returns>
        public abstract System.Boolean Add( UniXP.Common.CProfile objProfile );

        /// <summary>
        /// ������� ������ �� ��
        /// </summary>
        /// <param name="objProfile">�������</param>
        /// <param name="uuidID">���������� ������������� �������</param>
        /// <returns>true - ������� ����������; false - ������</returns>
        public abstract System.Boolean Remove( UniXP.Common.CProfile objProfile );

        /// <summary>
        /// ��������� ��������� � ��
        /// </summary>
        /// <param name="objProfile">�������</param>
        /// <returns>true - ������� ����������; false - ������</returns>
        public abstract System.Boolean Update( UniXP.Common.CProfile objProfile );
    }
}
