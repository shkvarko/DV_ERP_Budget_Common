using System;
using System.Reflection;
using UniXP.Common;

namespace ERP_Budget.Global
{
    public class RegKey
    {
        public static readonly System.String KEYVAL_ANALISYSSERVERNAME = "ASName";
    }

    public class Consts
    {
        // �������� �� ���������
        public static readonly System.String strANALISYSSERVERNAME = "MIS";
    }
}

namespace ERP_Budget
   {

      
        public class CERPModuleInfo : UniXP.Common.CClientModuleInfo
        {
            public CERPModuleInfo()
             : base( Assembly.GetExecutingAssembly(),
                   UniXP.Common.EnumDLLType.typeOptions,
                   new System.Guid( "{A8E694DF-15A3-4713-80AC-304B3FE911E8}" ),
                   new System.Guid( "{A8E694DF-15A3-4713-80AC-304B3FE911E8}" ),
                   ERP_Budget.Properties.Resources.IMAGES_ERPBUDGET, ERP_Budget.Properties.Resources.IMAGES_ERPBUDGETSMALL )
             {
             }

            /// <summary>
            /// ��������� �������� �� �������� ������������ ��������� ������ � �������.
            /// </summary>
            /// <param name="objProfile">������� ������������.</param>
            public override System.Boolean Check( UniXP.Common.CProfile objProfile )
            {
                return true;
            }
            /// <summary>
            /// ��������� �������� �� ��������� ������ � �������.
            /// </summary>
            /// <param name="objProfile">������� ������������.</param>
            public override System.Boolean Install( UniXP.Common.CProfile objProfile )
            {
                return true;
            }
            /// <summary>
            /// ��������� �������� �� �������� ������ �� �������.
            /// </summary>
            /// <param name="objProfile">������� ������������.</param>
            public override System.Boolean UnInstall( UniXP.Common.CProfile objProfile )
            {
                return true;
            }
            /// <summary>
            /// ���������� �������� �� ���������� ��� ��������� ����� ������ ������������� ������.
            /// </summary>
            /// <param name="objProfile">������� ������������.</param>
            public override System.Boolean Update( UniXP.Common.CProfile objProfile )
            {
                return true;
            }
            /// <summary>
            /// ���������� ������ ��������� ������� � ������ ������.
            /// </summary>
            public override UniXP.Common.CModuleClassInfo GetClassInfo()
            {
                return null;
            }
        }

        public class ModuleInfo : PlugIn.IModuleInfo
        {
            public UniXP.Common.CClientModuleInfo GetModuleInfo()
            {
                return new CERPModuleInfo();
            }
        }
   }
