using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Linq;


namespace ERP_Budget.Common
{
    public static class CRouteConditionAlgoritm
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

        #region Проверка условия предоставления маршрута 

        /// <summary>
        /// Проверяет синтаксис условия
        /// </summary>
        /// <param name="objRouteVariable">переменная</param>
        /// <returns>проверенную строку</returns>
        public static System.Boolean CheckConditionSyntax(CRouteVariable objstructRouteVariable)
        {
            System.Boolean bRet = false;
            try
            {
                if (objstructRouteVariable == null) { return bRet; }
                // формируем регулярное выражение
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
                "Ошибка проверки синтаксиса условия.\n\nТекст ошибки: " + f.Message, "Внимание",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
            }

            return bRet;
        }
        #endregion

        #region Выбор маршрута следования документа 
        
        /// <summary>
        /// Возвращает шаблон маршрута, который удовлетворяет списку проинициализированных переменных
        /// </summary>
        /// <param name="objStructRouteVariableList">список проинициализированных переменных для выбора условия маршрута</param>
        /// <returns>шаблон маршрута</returns>
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
                            // в условии эта переменная проинициализирована
                            // найдем такую же переменную в списке objInitRouteVariableList
                            foreach (CRouteVariable objInitRouteVariable in objInitRouteVariableList)
                            {
                                if (objInitRouteVariable.uuidID.CompareTo(objRouteVariable.uuidID) == 0)
                                {
                                    bFind = bCheckRouteVariableValue(objInitRouteVariable, objRouteVariable);
                                    break;
                                }
                            }
                            // если значение проинициированной в заявке переменной 
                            // не подходит под условие, то маршрут не подходит
                            if( bFind == false ) { break; }
                        }
                    } // foreach
                    if (bFind == true)
                    {
                        // маршрут найден
                        iRet = i;
                        break;
                    }
                } // foreach
            }
            catch( System.Exception f )
            {
                DevExpress.XtraEditors.XtraMessageBox.Show( 
                "Ошибка поиска маршрутов следования документа.\n\nТекст ошибки: " + f.Message, "Внимание",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
            }

            return iRet;
        }

        /// <summary>
        /// Возвращает шаблон маршрута, соответствующий списку условий
        /// </summary>
        /// <param name="objInitRouteVariableList">список условий (заполненных переменных)</param>
        /// <param name="objRouteConditionList">список шаблонов маршрутов</param>
        /// <param name="iRet">код возврата метода</param>
        /// <param name="strErr">сообщение об ошибке</param>
        /// <returns>шаблон маршрута</returns>
        public static CRouteCondition GetRouteTemplateForCondition(List<CRouteVariable> objInitRouteVariableList,
            System.Collections.Generic.List<CRouteCondition> objRouteConditionList, ref System.Int32 iRet, ref System.String strErr )
        {
            CRouteCondition objRet = null;
            try
            {
                if ((objRouteConditionList != null) && (objRouteConditionList.Count > 0) &&
                    (objInitRouteVariableList != null) && (objInitRouteVariableList.Count > 0))
                {
                    // список шаблонов маршрутов и список переменных для поиска маршрута заполнены
                    // поиск первого по списку шаблона, удовлетворяющего значениям переменных из objInitRouteVariableList
                    objRet = objRouteConditionList.FirstOrDefault<CRouteCondition>(x => x.VariableListIsEqualRouteTemplate(objInitRouteVariableList) == true);
                }
            }
            catch (System.Exception f)
            {
                iRet = -1;
                strErr += (String.Format("Ошибка поиска подходящего маршрута. Текст ошибки: {0}", f.Message));
            }

            return objRet;
        }

        /// <summary>
        /// Проверяет, удовлетворяет ли структура objInitRouteVariable условию выбора маршрута strCondition
        /// </summary>
        /// <returns>true - условие выполняется; false - условие не выполняется</returns>
        public static System.Boolean bCheckRouteVariableValue( CRouteVariable objInitRouteVariable,
            CRouteVariable objConditionRouteVariable)
        {
            System.Boolean bRet = false;
            try
            {
                // создаем объект "Регулярное выражение" для разбора условия
                System.Text.RegularExpressions.Regex rx = new Regex(objConditionRouteVariable.Pattern, RegexOptions.Compiled | RegexOptions.IgnoreCase);
                if( rx == null ) 
                {
                    DevExpress.XtraEditors.XtraMessageBox.Show( 
                    "Не удалось создать объект \"Регулярное выражение\"для переменной" +
                    objInitRouteVariable.Name + ".", "Внимание",
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
        /// <param name="objStructRouteVariable">проверяемое значение</param>
        /// <returns>true - условие выполняется; false - условие не выполняется</returns>
        public static System.Boolean bCheckCondition( System.String strCondition, System.String strValue,
            CRouteVariable objRouteVariable )
        {
            System.Boolean bRet = false;
            try
            {
                // 0 - текст; 1 - число; 2 - bool
                System.Int32 iValueType = 0;
                if( objRouteVariable.DataTypeName == "System.String" ) { iValueType = 0; }
                if( objRouteVariable.DataTypeName == "System.Double" ) { iValueType = 1; }

                switch( iValueType )
                {
                    case 0:
                    {
                        // условие " = "
                        if( strCondition == m_strOprEqual )
                        {
                            bRet = ( objRouteVariable.m_strValue == strValue );
                        }
                        // условие " != "
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
                            "Ошибка преобразования строк в число.\nСтроки : " + objRouteVariable.m_strValue + 
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
