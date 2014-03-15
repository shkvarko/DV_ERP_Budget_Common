using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Linq;

namespace ERP_Budget.Common
{
    public static class CBudgetDocTypeConditionAlgoritm
    {
        #region Переменные, свойства, константы 

        private const System.String m_strOprMore = ">";
        private const System.String m_strOprSmaller = "<";
        private const System.String m_strOprEqual = "=";
        private const System.String m_strOprMoreEqual = ">=";
        private const System.String m_strOprSmallerEqual = "<=";
        private const System.String m_strOprNotEqual = "!=";

        private const System.String m_strSprtrCP = ";";

        #endregion

        #region Проверка условия выбора типа документа 
        /// <summary>
        /// Проверяет синтаксис условия
        /// </summary>
        /// <param name="objRouteVariable">переменная</param>
        /// <returns>проверенную строку</returns>
        public static System.Boolean CheckConditionSyntax( CBudgetDocTypeVariable objBudgetDocTypeVariable )
        {
            System.Boolean bRet = false;
            try
            {
                if( objBudgetDocTypeVariable == null ) { return bRet; }
                // формируем регулярное выражение
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
                "Ошибка проверки синтаксиса условия.\n\nТекст ошибки: " + f.Message, "Внимание",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
            }

            return bRet;
        }
        #endregion

        #region Выбор типа бюджетного документа 
        /// <summary>
        /// Возвращает список типов бюджетного документа, который удовлетворяет списку проинициализированных переменных
        /// </summary>
        /// <param name="objInitBudgetDocTypeVariableList">список переменных, влияющих на выбор типа документа</param>
        /// <param name="objBudgetDocTypeConditionList">Шаблоны выбора типов документов</param>
        /// <returns>список типов бюджетного документа</returns>
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
                    // для каждого шаблона проверяется, соответствуют ли переменные из objInitBudgetDocTypeVariableList
                    // заданным в шаблоне условиям
                    if (objPatternBudgetDocType.BudgetDocTypeVariableList.Count<CBudgetDocTypeVariable>(x => x.m_strValue != "") == 0)
                    {
                        continue;
                    }

                    bVariableListValidRoute = true;
                    foreach (CBudgetDocTypeVariable objPatternVariable in objPatternBudgetDocType.BudgetDocTypeVariableList)
                    {
                        if (objPatternVariable.m_strValue == "") { continue; }
                        // в шаблоне переменная проинициализирована
                        // найдем такую же переменную в списке objInitBudgetDocTypeVariableList с таким же значением
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
                        // условия шаблона выполняются
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
                "Ошибка поиска типов бюджетного документа.\n\nТекст ошибки: " + f.Message, "Внимание",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
            }

            return objList;
        }

        /// <summary>
        /// Проверяет, выполняется ли условие выбора
        /// </summary>
        /// <returns>true - условие выполняется; false - условие не выполняется</returns>
        public static System.Boolean bCheckRouteVariableValue( CBudgetDocTypeVariable objInitBudgetDocTypeVariable,
            CBudgetDocTypeVariable objConditionBudgetDocTypeVariable )
        {
            System.Boolean bRet = false;
            try
            {
                // создаем объект "Регулярное выражение" для разбора условия
                System.Text.RegularExpressions.Regex rx = new Regex( objConditionBudgetDocTypeVariable.Pattern, RegexOptions.Compiled | RegexOptions.IgnoreCase );
                if( rx == null )
                {
                    DevExpress.XtraEditors.XtraMessageBox.Show( 
                    "Не удалось создать объект \"Регулярное выражение\"для переменной" +
                    objInitBudgetDocTypeVariable.Name + ".", "Внимание",
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
                "Ошибка проверки выполнения условия.\n\nТекст ошибки: " + f.Message, "Внимание",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
            }

            return bRet;
        }
        /// <summary>
        /// Проверяет выполняется ли условие
        /// </summary>
        /// <param name="strCondition">знак условия (больше, меньше, равно и т д..)</param>
        /// <param name="strValue">значение после знака условия</param>
        /// <param name="objBudgetDocTypeVariable">проверяемое значение</param>
        /// <returns>true - условие выполняется; false - условие не выполняется</returns>
        public static System.Boolean bCheckCondition( System.String strCondition, System.String strValue,
            CBudgetDocTypeVariable objBudgetDocTypeVariable )
        {
            System.Boolean bRet = false;
            try
            {
                // 0 - текст; 1 - число; 2 - bool
                System.Int32 iValueType = 0;
                if( objBudgetDocTypeVariable.DataTypeName == "System.String" ) { iValueType = 0; }
                if( objBudgetDocTypeVariable.DataTypeName == "System.Double" ) { iValueType = 1; }
                if( objBudgetDocTypeVariable.DataTypeName == "System.Boolean" ) { iValueType = 2; }

                switch( iValueType )
                {
                    case 0:
                    {
                        // условие " = "
                        if( strCondition == m_strOprEqual )
                        {
                            bRet = ( objBudgetDocTypeVariable.m_strValue == strValue );
                        }
                        // условие " != "
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
                            "Ошибка преобразования строк в число.\nСтроки : " + objBudgetDocTypeVariable.m_strValue + 
                            " и " + strValue + ".", "Внимание",
                            System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
                            break;
                        }
                        // условие " = "
                        if( strCondition == m_strOprEqual )
                        {
                            bRet = ( dblCondition == dblValue );
                        }
                        // условие " != "
                        if( strCondition == m_strOprNotEqual )
                        {
                            bRet = ( dblCondition != dblValue );
                        }
                        // условие " > "
                        if( strCondition == m_strOprMore )
                        {
                            bRet = ( dblCondition > dblValue );
                        }
                        // условие " >= "
                        if( strCondition == m_strOprMoreEqual )
                        {
                            bRet = ( dblCondition >= dblValue );
                        }
                        // условие " < "
                        if( strCondition == m_strOprSmaller )
                        {
                            bRet = ( dblCondition < dblValue );
                        }
                        // условие " <= "
                        if( strCondition == m_strOprSmallerEqual )
                        {
                            bRet = ( dblCondition <= dblValue );
                        }
                        break;
                    }
                    case 2:
                    {
                        // условие " = "
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
                "Ошибка проверки выполнения условия.\n\nТекст ошибки: " + f.Message, "Внимание",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
            }

            return bRet;
        }
        #endregion
    }
}
