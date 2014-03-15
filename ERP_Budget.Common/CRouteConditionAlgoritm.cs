using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Linq;


namespace ERP_Budget.Common
{
    public static class CRouteConditionAlgoritm
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

        #region �������� ������� �������������� �������� 

        /// <summary>
        /// ��������� ��������� �������
        /// </summary>
        /// <param name="objRouteVariable">����������</param>
        /// <returns>����������� ������</returns>
        public static System.Boolean CheckConditionSyntax(CRouteVariable objstructRouteVariable)
        {
            System.Boolean bRet = false;
            try
            {
                if (objstructRouteVariable == null) { return bRet; }
                // ��������� ���������� ���������
                System.Text.RegularExpressions.Regex rx = new Regex(objstructRouteVariable.PatternSrc, RegexOptions.Compiled | RegexOptions.IgnoreCase);
                if (rx == null) { return bRet; }

                // Define some test strings.
                System.Text.RegularExpressions.MatchCollection matches = rx.Matches(objstructRouteVariable.m_strValue);
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
                bRet = (strRet == objstructRouteVariable.m_strValue);
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

        #region ����� �������� ���������� ��������� 
        
        /// <summary>
        /// ���������� ������ ��������, ������� ������������� ������ ��������������������� ����������
        /// </summary>
        /// <param name="objStructRouteVariableList">������ ��������������������� ���������� ��� ������ ������� ��������</param>
        /// <returns>������ ��������</returns>
        public static System.Int32 GetRouteConditionIndx( List<CRouteVariable> objInitRouteVariableList,
            System.Collections.Generic.List<CRouteCondition> objRouteConditionList)
        {
            System.Int32 iRet = -1;
            try
            {
                if ((objRouteConditionList == null) || (objRouteConditionList.Count == 0) || 
                    (objInitRouteVariableList == null) || (objInitRouteVariableList.Count == 0 ))
                {
                    return iRet;
                }
                CRouteCondition objRouteCondition = null;
                for (System.Int32 i = 0; i < objRouteConditionList.Count; i++ )
                {
                    objRouteCondition = objRouteConditionList[i];
                    System.Boolean bFind = false;
                    foreach (CRouteVariable objRouteVariable in objRouteCondition.RouteVariableList)
                    {
                        if( objRouteVariable.m_strValue != "" )
                        {
                            // � ������� ��� ���������� �������������������
                            // ������ ����� �� ���������� � ������ objInitRouteVariableList
                            foreach (CRouteVariable objInitRouteVariable in objInitRouteVariableList)
                            {
                                if (objInitRouteVariable.uuidID.CompareTo(objRouteVariable.uuidID) == 0)
                                {
                                    bFind = bCheckRouteVariableValue(objInitRouteVariable, objRouteVariable);
                                    break;
                                }
                            }
                            // ���� �������� ����������������� � ������ ���������� 
                            // �� �������� ��� �������, �� ������� �� ��������
                            if( bFind == false ) { break; }
                        }
                    } // foreach
                    if (bFind == true)
                    {
                        // ������� ������
                        iRet = i;
                        break;
                    }
                } // foreach
            }
            catch( System.Exception f )
            {
                DevExpress.XtraEditors.XtraMessageBox.Show( 
                "������ ������ ��������� ���������� ���������.\n\n����� ������: " + f.Message, "��������",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
            }

            return iRet;
        }

        /// <summary>
        /// ���������� ������ ��������, ��������������� ������ �������
        /// </summary>
        /// <param name="objInitRouteVariableList">������ ������� (����������� ����������)</param>
        /// <param name="objRouteConditionList">������ �������� ���������</param>
        /// <param name="iRet">��� �������� ������</param>
        /// <param name="strErr">��������� �� ������</param>
        /// <returns>������ ��������</returns>
        public static CRouteCondition GetRouteTemplateForCondition(List<CRouteVariable> objInitRouteVariableList,
            System.Collections.Generic.List<CRouteCondition> objRouteConditionList, ref System.Int32 iRet, ref System.String strErr )
        {
            CRouteCondition objRet = null;
            try
            {
                if ((objRouteConditionList != null) && (objRouteConditionList.Count > 0) &&
                    (objInitRouteVariableList != null) && (objInitRouteVariableList.Count > 0))
                {
                    // ������ �������� ��������� � ������ ���������� ��� ������ �������� ���������
                    // ����� ������� �� ������ �������, ���������������� ��������� ���������� �� objInitRouteVariableList
                    objRet = objRouteConditionList.FirstOrDefault<CRouteCondition>(x => x.VariableListIsEqualRouteTemplate(objInitRouteVariableList) == true);
                }
            }
            catch (System.Exception f)
            {
                iRet = -1;
                strErr += (String.Format("������ ������ ����������� ��������. ����� ������: {0}", f.Message));
            }

            return objRet;
        }

        /// <summary>
        /// ���������, ������������� �� ��������� objInitRouteVariable ������� ������ �������� strCondition
        /// </summary>
        /// <returns>true - ������� �����������; false - ������� �� �����������</returns>
        public static System.Boolean bCheckRouteVariableValue( CRouteVariable objInitRouteVariable,
            CRouteVariable objConditionRouteVariable)
        {
            System.Boolean bRet = false;
            try
            {
                // ������� ������ "���������� ���������" ��� ������� �������
                System.Text.RegularExpressions.Regex rx = new Regex(objConditionRouteVariable.Pattern, RegexOptions.Compiled | RegexOptions.IgnoreCase);
                if( rx == null ) 
                {
                    DevExpress.XtraEditors.XtraMessageBox.Show( 
                    "�� ������� ������� ������ \"���������� ���������\"��� ����������" +
                    objInitRouteVariable.Name + ".", "��������",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
                    return bRet; 
                }

                // Define some test strings.
                System.Text.RegularExpressions.MatchCollection matches = rx.Matches(objConditionRouteVariable.m_strValue);
                if( matches.Count == 0 ) { return bRet; }

                // Report on each match.
                foreach( Match match in matches )
                {
                    if( match.Success )
                    {
                        string strMatchCondition = match.Groups[ "condition" ].Value;
                        string strMatchValue = match.Groups[ "value" ].Value;
                        if (bCheckCondition(strMatchCondition, strMatchValue, objInitRouteVariable))
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
        /// <param name="objStructRouteVariable">����������� ��������</param>
        /// <returns>true - ������� �����������; false - ������� �� �����������</returns>
        public static System.Boolean bCheckCondition( System.String strCondition, System.String strValue,
            CRouteVariable objRouteVariable )
        {
            System.Boolean bRet = false;
            try
            {
                // 0 - �����; 1 - �����; 2 - bool
                System.Int32 iValueType = 0;
                if( objRouteVariable.DataTypeName == "System.String" ) { iValueType = 0; }
                if( objRouteVariable.DataTypeName == "System.Double" ) { iValueType = 1; }

                switch( iValueType )
                {
                    case 0:
                    {
                        // ������� " = "
                        if( strCondition == m_strOprEqual )
                        {
                            bRet = ( objRouteVariable.m_strValue == strValue );
                        }
                        // ������� " != "
                        if( strCondition == m_strOprNotEqual )
                        {
                            bRet = ( objRouteVariable.m_strValue != strValue );
                        }
                        break;
                    }
                    case 1:
                    {
                        System.Double dblCondition = 0;
                        System.Double dblValue = 0;
                        try
                        {
                            dblCondition = System.Convert.ToDouble( objRouteVariable.m_strValue );
                            dblValue = System.Convert.ToDouble( strValue );
                        }
                        catch
                        {
                            DevExpress.XtraEditors.XtraMessageBox.Show( 
                            "������ �������������� ����� � �����.\n������ : " + objRouteVariable.m_strValue + 
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
