using DataAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Xml;
using MySqlConnector;
using System.Net;

namespace DataAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Last2MonthsDataController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public Last2MonthsDataController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpPost]
        public ResponseData Run(RequestData request)
        {
            ResponseData result = new ResponseData();

            try
            {
                string tcblink = string.Format("https://www.tcmb.gov.tr/kurlar/{0}.xml",
                    (request.isToday) ? "today" : string.Format("{2}{1}/{0}{1}{2}"),
                    request.Day.ToString().PadLeft(2, '0'),
                    request.Month.ToString().PadLeft(2, '0'), request.Year);

                result.Data = new List<ResponseDataCurrency>();

                XmlDocument doc = new XmlDocument();

                doc.Load(tcblink);

                if (doc.SelectNodes("Tarih_Date").Count < 1)
                {
                    result.Hata = "Currency information not found!";
                    return result;
                }
                foreach (XmlNode item in doc.SelectNodes("Tarih_Date")[0].ChildNodes)
                {
                    ResponseDataCurrency currency = new ResponseDataCurrency();

                    currency.Code = item.Attributes["Kod"].Value;
                    currency.Ad = item["Isim"].InnerText;
                    currency.ForexSelling = Convert.ToDecimal("0" + item["ForexSelling"].InnerText.Replace(".", ","));
                    result.Data.Add(currency);
                }
            }
            catch (Exception ex)
            {

                result.Hata = ex.Message;
            }

            DateTime dateTime = DateTime.Today;

            string day = dateTime.Day.ToString();
            string month = dateTime.Month.ToString();
            int year = dateTime.Year;
            int counter = 0;

            var cxml = new XmlDocument();
            MySqlConnection conn = new MySqlConnection(_configuration.GetConnectionString("ConnStr"));

            string AddZero(string number)
            {
                if (number == "1" || number == "2" || number == "3" || number == "4" || number == "5" || number == "6" || number == "7" || number == "8" || number == "9")
                {
                    number = "0" + number;
                }
                return number;
            }
            string url = null;

            for (int k = year; k >= 0; k--)
            {
                for (int i = int.Parse(month); i >= 0; i--)
                {

                    for (int j = int.Parse(day); j >= 0; j--)
                    {
                        if (j != 0 && i != 0)
                        {
                            counter++;
                            if (counter == 61)
                            {
                                goto loopbreak;
                            }
                           
                            try
                            {
                                url = string.Format("http://www.tcmb.gov.tr/kurlar/{0}{1}/{2}{1}{0}.xml", year, AddZero(i.ToString()), AddZero(j.ToString()));
                                cxml.Load(url);
                                conn.Open();
                                DailyRecords usdtry = new DailyRecords()
                                {
                                    Exchangedate = Convert.ToDateTime(cxml.SelectSingleNode("//Tarih_Date").Attributes["Tarih"].Value),
                                    Forexselling = decimal.Parse(cxml.SelectSingleNode("Tarih_Date/Currency [@Kod = 'USD']/ForexSelling").InnerXml),
                                    Currencyid = 1
                                };
                                DailyRecords eurtry = new DailyRecords()
                                {
                                    Exchangedate = Convert.ToDateTime(cxml.SelectSingleNode("//Tarih_Date").Attributes["Tarih"].Value),
                                    Forexselling = decimal.Parse(cxml.SelectSingleNode("Tarih_Date/Currency [@Kod = 'EUR']/ForexSelling").InnerXml),
                                    Currencyid = 2
                                };
                                DailyRecords gbptry = new DailyRecords()
                                {
                                    Exchangedate = Convert.ToDateTime(cxml.SelectSingleNode("//Tarih_Date").Attributes["Tarih"].Value),
                                    Forexselling = decimal.Parse(cxml.SelectSingleNode("Tarih_Date/Currency [@Kod = 'GBP']/ForexSelling").InnerXml),
                                    Currencyid = 3
                                };

                                MySqlCommand cmd = new MySqlCommand("" +
                                    "INSERT INTO dailyrecords(Currencyid, Forexselling, Exchangedate) VALUES(@Currencyid1,@Forexselling1,@Exchangedate1);" +
                                    "INSERT INTO dailyrecords(Currencyid, Forexselling, Exchangedate) VALUES(@Currencyid2,@Forexselling2,@Exchangedate2);" +
                                    "INSERT INTO dailyrecords(Currencyid, Forexselling, Exchangedate) VALUES(@Currencyid3,@Forexselling3,@Exchangedate3)", conn);
                                cmd.Parameters.AddWithValue("@Currencyid1", usdtry.Currencyid);
                                cmd.Parameters.AddWithValue("@Forexselling1", usdtry.Forexselling);
                                cmd.Parameters.AddWithValue("@Exchangedate1", usdtry.Exchangedate);
                                cmd.Parameters.AddWithValue("@Currencyid2", eurtry.Currencyid);
                                cmd.Parameters.AddWithValue("@Forexselling2", eurtry.Forexselling);
                                cmd.Parameters.AddWithValue("@Exchangedate2", eurtry.Exchangedate);
                                cmd.Parameters.AddWithValue("@Currencyid3", gbptry.Currencyid);
                                cmd.Parameters.AddWithValue("@Forexselling3", gbptry.Forexselling);
                                cmd.Parameters.AddWithValue("@Exchangedate3", gbptry.Exchangedate);

                                cmd.ExecuteNonQuery();
                                conn.Close();
                            }
                            catch (Exception)
                            {
                                conn.Close();
                                counter--;
                            }

                        }
                    }
                    day = 31.ToString();
                }
                month = 12.ToString();
                year--;
            }
        loopbreak:;

            return result;
        }
    }
}
