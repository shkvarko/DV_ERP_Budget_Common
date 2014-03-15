using System;
using System.Collections.Generic;
using System.Text;

namespace ERP_Budget.Global
{
    //public class RegKey
    //{
    //    public static readonly System.String KEYVAL_IBLINKEDSERVERNAME = "IBLinkedServerName";
    //}

    public class Consts
    {
        // названия динамических прав
        public static readonly System.String strDREditRootDebitArticle = "Редакитрование статей расходов";
        public static readonly System.String strDRInitiator = "Инициатор";
        public static readonly System.String strDRCashier = "Кассир";
        public static readonly System.String strDRInspector = "Контролер бюджета";
        public static readonly System.String strDRLook = "Просмотрщик";
        public static readonly System.String strDRManager = "Распорядитель бюджета";
        public static readonly System.String strDRCoordinator = "Согласователь бюджета";
        public static readonly System.String strDRAddRight = "Дополнительное право";
        public static readonly System.String strDRRouteEditor = "Редактирование маршрутов";
        public static readonly System.String strDRAccountant = "Бухгалтер";
        public static readonly System.String strDRBudgetEditor = "Редактирование бюджета";
        public static readonly System.String strDREditAccountPlanInBudgetDocument = "Изменение счета в бюджетном документе";
        public static readonly System.String strDRCreateUndefinedDocument = "Прочее";
        // валюта, в которую пересчитываются суммы бюджета
        public static readonly System.String strCurrencyBudget = "EUR";
        // перечень переменных, влияющих на выбор маршрута
        public static readonly System.String strRouteVariableDynamicRight = "Динамическое право";
        public static readonly System.String strRouteVariableDebitArticle = "Статья расходов";
        public static readonly System.String strRouteVariableSumma = "Сумма документа";
        public static readonly System.String strRouteVariablePaymentType = "Форма оплаты";
        public static readonly System.String strRouteVariableDocType = "Тип документа";

    }

}
