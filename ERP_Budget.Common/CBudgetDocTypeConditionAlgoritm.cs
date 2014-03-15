using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Linq;

namespace ERP_Budget.Common
{
    public static class CBudgetDocTypeConditionAlgoritm
    {
        #region ����������, ��������, ��������� 

        private const System.String m_strOprMore = ">";
        private const System.String m_strOprSmaller = "<";
        private const System.String m_strOprEqual = "=";
        private const System.String m_strOprMoreEqual = ">=";
        private const System.String m_strOprSmallerEqual = "<=";
        private const System.String m_strOprNotEqual = "!=";

        private const System.String m_strSprtrCP = ";";

        #endregion

        #region �������� ������� ������ ���� ��������� 
        /// <summary>
        /// ��������� ��������� �������
        /// </summary>
        /// <param name="objRouteVariable">����������</param>
        /// <returns>����������� ������</returns>
        public static System.Boolean CheckConditionSyntax( CBudgetDocTypeVariable objBudgetDocTypeVariable )
        {
            System.Boolean bRet = false;
            try
            {
                if( objBudgetDocTypeVariable == null ) { return bRet; }
                // ��������� ���������� ���������
                System.Text.RegularExpressions.Regex rx = new Regex( objBudgetDocTypeVariable.PatternSrc, RegexOptions.Compiled | RegexOptions.IgnoreCase );
                if( rx == null ) { return bRet; }

                // Define some test strings.
                System.Text.RegularExpressions.MatchCollection matches = rx.Matches( objBudgetDocTypeVariable.m_strValue );
                if( matches.Count == 0 ) { return bRet; }

                // Report on each match.
                System.String strRet = "";
                foreach( Match match in matches )
                {
                    if( match.Success )
                    {
                        string strCondition = match.Groups[ "condition" ].Value;
                        string strValue = match.Groups[ "value" ].Value;
                        strRet = ( strRet == "" ) ? ( strCondition + strValue ) : ( strRet + m_strSprtrCP + strCondition + strValue );
                    }
                }
                matches = null;
                rx = null;
                bRet = ( strRet == objBudgetDocTypeVariable.m_strValue );
            }
            catch( System.Exception f )
            {
                DevExpress.XtraEditors.XtraMessageBox.Show( 
                "������ �������� ���������� �������.\n\n����� ������: " + f.Message, "��������",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
            }

            return bRet;
        }
        #endregion

        #region ����� ���� ���������� ��������� 
        /// <summary>
        /// ���������� ������ ����� ���������� ���������, ������� ������������� ������ ��������������������� ����������
        /// </summary>
        /// <param name="objInitBudgetDocTypeVariableList">������ ����������, �������� �� ����� ���� ���������</param>
        /// <param name="objBudgetDocTypeConditionList">������� ������ ����� ����������</param>
        /// <returns>������ ����� ���������� ���������</returns>
        public static List<CBudgetDocType> GetBudgetDocTypeList( List<CBudgetDocTypeVariable> objInitBudgetDocTypeVariableList,
            System.Collections.Generic.List<CBudgetDocTypeCondition> objBudgetDocTypeConditionList )
        {
            List<CBudgetDocType> objList = new List<CBudgetDocType>();
            try
            {
                if( ( objInitBudgetDocTypeVariableList == null ) || ( objInitBudgetDocTypeVariableList.Count == 0 ) || 
                    ( objBudgetDocTypeConditionList == null ) || ( objBudgetDocTypeConditionList.Count == 0 ) ||
                    (objInitBudgetDocTypeVariableList.Count<CBudgetDocTypeVariable>(x=>x.m_strValue!="") == 0))
                {
                    return objList;
                }

                System.Boolean bVariableListValidRoute = false;

                foreach (CBudgetDocTypeCondition objPatternBudgetDocType in objBudgetDocTypeConditionList)
                {
                    // ��� ������� ������� �����������, ������������� �� ���������� �� objInitBudgetDocTypeVariableList
                    // �������� � ������� ��������
                    if (objPatternBudgetDocType.BudgetDocTypeVariableList.Count<CBudgetDocTypeVariable>(x => x.m_strValue != "") == 0)
                    {
                        continue;
                    }

                    bVariableListValidRoute = true;
                    foreach (CBudgetDocTypeVariable objPatternVariable in objPatternBudgetDocType.BudgetDocTypeVariableList)
                    {
                        if (objPatternVariable.m_strValue == "") { continue; }
                        // � ������� ���������� �������������������
                        // ������ ����� �� ���������� � ������ objInitBudgetDocTypeVariableList � ����� �� ���������
                        if (objInitBudgetDocTypeVariableList.FirstOrDefault<CBudgetDocTypeVariable>(
                            x => ((x.m_strValue != "") && (x.m_uuidID.CompareTo(objPatternVariable.uuidID) == 0) 
                                && (bCheckRouteVariableValue(x, objPatternVariable) == true))) == null)
                        {
                            bVariableListValidRoute = false;
                            break;
                        }
                    }

                    if (bVariableListValidRoute == true)
                    {
                        // ������� ������� �����������
                        if (objList.FirstOrDefault<CBudgetDocType>(x => x.uuidID.CompareTo(objPatternBudgetDocType.BudgetDocType.uuidID) == 0) == null)
                        {
                            objList.Add(objPatternBudgetDocType.BudgetDocType);
                        }
                    }
                }

            }
            catch( System.Exception f )
            {
                objList.Clear();
                DevExpress.XtraEditors.XtraMessageBox.Show( 
                "������ ������ ����� ���������� ���������.\n\n����� ������: " + f.Message, "��������",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
            }

            return objList;
        }

        /// <summary>
        /// ���������, ����������� �� ������� ������
        /// </summary>
        /// <returns>true - ������� �����������; false - ������� �� �����������</returns>
        public static System.Boolean bCheckRouteVariableValue( CBudgetDocTypeVariable objInitBudgetDocTypeVariable,
            CBudgetDocTypeVariable objConditionBudgetDocTypeVariable )
        {
            System.Boolean bRet = false;
            try
            {
                // ������� ������ "���������� ���������" ��� ������� �������
                System.Text.RegularExpressions.Regex rx = new Regex( objConditionBudgetDocTypeVariable.Pattern, RegexOptions.Compiled | RegexOptions.IgnoreCase );
                if( rx == null )
                {
                    DevExpress.XtraEditors.XtraMessageBox.Show( 
                    "�� ������� ������� ������ \"���������� ���������\"��� ����������" +
                    objInitBudgetDocTypeVariable.Name + ".", "��������",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
                    return bRet;
                }

                // Define some test strings.
                System.Text.RegularExpressions.MatchCollection matches = rx.Matches( objConditionBudgetDocTypeVariable.m_strValue );
                if( matches.Count == 0 ) { return bRet; }

                // Report on each match.
                foreach( Match match in matches )
                {
                    if( match.Success )
                    {
                        string strMatchCondition = match.Groups[ "condition" ].Value;
                        string strMatchValue = match.Groups[ "value" ].Value;
                        if( bCheckCondition( strMatchCondition, strMatchValue, objInitBudgetDocTypeVariable ) )
                        {
                            bRet = true;
                            break;
                        }
                    }
                }
                matches = null;
                rx = null;

            }
            catch( System.Exception f )
            {
                DevExpress.XtraEditors.XtraMessageBox.Show( 
                "������ �������� ���������� �������.\n\n����� ������: " + f.Message, "��������",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
            }

            return bRet;
        }
        /// <summary>
        /// ��������� ����������� �� �������
        /// </summary>
        /// <param name="strCondition">���� ������� (������, ������, ����� � � �..)</param>
        /// <param name="strValue">�������� ����� ����� �������</param>
        /// <param name="objBudgetDocTypeVariable">����������� ��������</param>
        /// <returns>true - ������� �����������; false - ������� �� �����������</returns>
        public static System.Boolean bCheckCondition( System.String strCondition, System.String strValue,
            CBudgetDocTypeVariable objBudgetDocTypeVariable )
        {
            System.Boolean bRet = false;
            try
            {
                // 0 - �����; 1 - �����; 2 - bool
                System.Int32 iValueType = 0;
                if( objBudgetDocTypeVariable.DataTypeName == "System.String" ) { iValueType = 0; }
                if( objBudgetDocTypeVariable.DataTypeName == "System.Double" ) { iValueType = 1; }
                if( objBudgetDocTypeVariable.DataTypeName == "System.Boolean" ) { iValueType = 2; }

                switch( iValueType )
                {
                    case 0:
                    {
                        // ������� " = "
                        if( strCondition == m_strOprEqual )
                        {
                            bRet = ( objBudgetDocTypeVariable.m_strValue == strValue );
                        }
                        // ������� " != "
                        if( strCondition == m_strOprNotEqual )
                        {
                            bRet = ( objBudgetDocTypeVariable.m_strValue != strValue );
                        }
                        break;
                    }
                    case 1:
                    {
                        System.Double dblCondition = 0;
                        System.Double dblValue = 0;
                        try
                        {
                            dblCondition = System.Convert.ToDouble( objBudgetDocTypeVariable.m_strValue );
                            dblValue = System.Convert.ToDouble( strValue );
                        }
                        catch
                        {
                            DevExpress.XtraEditors.XtraMessageBox.Show( 
                            "������ �������������� ����� � �����.\n������ : " + objBudgetDocTypeVariable.m_strValue + 
                            " � " + strValue + ".", "��������",
                            System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
                            break;
                        }
                        // ������� " = "
                        if( strCondition == m_strOprEqual )
                        {
                            bRet = ( dblCondition == dblValue );
                        }
                        // ������� " != "
                        if( strCondition == m_strOprNotEqual )
                        {
                            bRet = ( dblCondition != dblValue );
                        }
                        // ������� " > "
                        if( strCondition == m_strOprMore )
                        {
                            bRet = ( dblCondition > dblValue );
                        }
                        // ������� " >= "
                        if( strCondition == m_strOprMoreEqual )
                        {
                            bRet = ( dblCondition >= dblValue );
                        }
                        // ������� " < "
                        if( strCondition == m_strOprSmaller )
                        {
                            bRet = ( dblCondition < dblValue );
                        }
                        // ������� " <= "
                        if( strCondition == m_strOprSmallerEqual )
                        {
                            bRet = ( dblCondition <= dblValue );
                        }
                        break;
                    }
                    case 2:
                    {
                        // ������� " = "
                        if( strCondition == m_strOprEqual )
                        {
                            bRet = ( objBudgetDocTypeVariable.m_strValue == strValue );
                        }
                        break;
                    }
                    default:
                    break;
                }

            }
            catch( System.Exception f )
            {
                DevExpress.XtraEditors.XtraMessageBox.Show( 
                "������ �������� ���������� �������.\n\n����� ������: " + f.Message, "��������",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
            }

            return bRet;
        }
        #endregion
    }
}
