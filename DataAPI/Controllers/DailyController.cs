using DataAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Rename;
using Microsoft.Extensions.Options;
using MySqlConnector;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Xml;

namespace DataAPI.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class DailyController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public DailyController(IConfiguration configuration)
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


            const string bugun = "https://www.tcmb.gov.tr/kurlar/today.xml";
            var cxml = new XmlDocument();
            cxml.Load(bugun);

            MySqlConnection conn = new MySqlConnection(_configuration.GetConnectionString("ConnStr"));
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


            return result;
        }
    }
}

