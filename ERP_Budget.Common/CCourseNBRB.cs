using System;
using System.Collections.Generic;
using System.Text;
using System.Globalization;
using System.Text.RegularExpressions;
using System.IO;
using System.Net;

namespace ERP_Budget.Common
{
    public class CCourseNBRB
    {

        private System.String m_strHTML = "";


        public bool LoadCourse()
        {
            return LoadCourse(System.DateTime.Parse("01.01.1970", new CultureInfo("ru-RU", true)));
        }



        public bool LoadCourse(System.DateTime dt)
        {

            System.String strURL = "http://www.nbrb.by/statistics/Rates/RatesPrint.asp";

            try
            {

                if (dt != null)
                {

                    strURL += "?fromDate=" + System.String.Format( "{0:yyyy-MM-dd}", dt );
                    //strURL += "?fromDate=" + System.String.Format("{0:d}", dt);
                    //strURL += "?fromDate=" + dt.ToString("yyyy-m-d");

                }



                HttpWebRequest req = (HttpWebRequest)HttpWebRequest.Create(strURL);

                HttpWebResponse resp = (HttpWebResponse)req.GetResponse();

                Stream stResp = resp.GetResponseStream();

                StreamReader rd = new StreamReader(stResp, Encoding.GetEncoding("windows-1251"));



                m_strHTML = rd.ReadToEnd();

            }

            catch
            {

                return false;

            }

            return true;

        }



        public System.DateTime GetDate()
        {

            System.Text.RegularExpressions.Regex reg = new System.Text.RegularExpressions.Regex("(<font\\sclass=stext><b>Официальные\\sкурсы\\sиностранных\\sвалют\\sна\\s)(?<Date>\\d{1,2}\\.\\d{1,2}\\.(?:\\d{4}|\\d{2}))");

            System.Text.RegularExpressions.Match res = reg.Match(m_strHTML);



            if (res.Success)
            {

                CultureInfo ci = new CultureInfo("ru-RU", false);



                return System.DateTime.Parse(res.Groups["Date"].Value, ci);

            }

            throw new Exception("Дата документа не определена, возможно сервер вернул документ в другом виде");

        }



        public System.Decimal GetCourse(System.String strCurrency)
        {

            System.Text.RegularExpressions.Regex reg = new System.Text.RegularExpressions.Regex("(<td\\salign=centrer>" + strCurrency + "<td\\snowrap>)([a-zA-Zа-яА-Я0-9\\s]*)(<td\\salign=right>)(?<Course>[0-9\\s.]+)");

            System.Text.RegularExpressions.Match res = reg.Match(m_strHTML);

            CultureInfo ci = new CultureInfo("en-US", false);



            ci.NumberFormat.NumberGroupSeparator = " ";

            if (res.Success)
            {

                String strVal = res.Groups["Course"].Value;



                strVal = strVal.Replace(Char.ConvertFromUtf32(160), " ");

                return System.Decimal.Parse(strVal, NumberStyles.Number, ci);

            }

            throw new Exception("Ошибка в коде валюты или не найден курс для данного кода валюты");

        }
    }
}
