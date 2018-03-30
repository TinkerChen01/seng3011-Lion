using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using newsapi.Models;
using System.Text.RegularExpressions;

namespace newsapi.Controllers
{
    public class NewsDataSetController : ApiController
    {

        /*[Route("api/allnewsdata/")]//get all
        public IEnumerable<NewsData> GetAllNewsDataSet()
        {
            return newsDataSet;
        }*/

        

        [Route("api/newsdata/start_date={fromtime}/end_date={totime}")] //Input all news in a period
        [HttpGet]
        public IHttpActionResult GetNewsDataByTime(string fromtime, string totime)
        {

            string parameters ="&from-date="+fromtime+"&to-date="+totime;
            string link = "http://content.guardianapis.com/search?api-key=660d5313-dd89-4989-a0d6-dbf0877f277b&section=news"+parameters;
            var request = (HttpWebRequest)WebRequest.Create(link);
            var response = (HttpWebResponse)request.GetResponse();
            var responseString = new System.IO.StreamReader(response.GetResponseStream()).ReadToEnd();

            string pattern1 = "webPublicationDate";
            string pattern2 = "webTitle";
            string pattern3 = "webUrl";
            string pattern4 = "\"sectionId\":\"[a-z]*\"";
            string pattern5 = "\"sectionName\":\"[A-Z][a-z]*\"";

            Regex rgx = new Regex(pattern1);
            Regex rgx2 = new Regex(pattern2);
            Regex rgx3 = new Regex(pattern3);
            Regex rgx4 = new Regex(pattern4);
            Regex rgx5 = new Regex(pattern5);


            responseString = rgx.Replace(responseString,"TimeStamp");
            responseString = rgx2.Replace(responseString, "Headline");
            responseString = rgx3.Replace(responseString, "NewsText");
            responseString = rgx4.Replace(responseString, "\"InstrumentIDs\":\"\"");
            responseString = rgx5.Replace(responseString, "\"CompanyNames\":\"\"");

            

            return Ok(responseString);

            /*var set1 = newsDataSet.Where((p) => Tonumber(p.TimeStamp) <= Tonumber(totime) && Tonumber(p.TimeStamp) >= Tonumber(fromtime));
            if (set1 == null)
            {
                return NotFound();
            }



            return Ok(set1);*/
        }

        [Route("api/newsdata/start_date={fromtime}/end_date={totime}/companynames={companynames}")] //Input period and company name
        [HttpGet]
        public IHttpActionResult GetNewsDataByCompanyNames(string companynames, string fromtime, string totime)
        {
            //string[] namelist = companynames.Split(',');
            string parameters = "&from-date=" + fromtime + "&to-date=" + totime +"&q="+companynames;
            string link = "http://content.guardianapis.com/search?api-key=660d5313-dd89-4989-a0d6-dbf0877f277b&section=news" + parameters;
            var request = (HttpWebRequest)WebRequest.Create(link);
            var response = (HttpWebResponse)request.GetResponse();
            var responseString = new System.IO.StreamReader(response.GetResponseStream()).ReadToEnd();

            
            string pattern1 = "webPublicationDate";
            string pattern2 = "webTitle";
            string pattern3 = "webUrl";
            string pattern4 = "\"sectionId\":\"[a-z]*\"";
            string pattern5 = "\"sectionName\":\"[A-Z][a-z]*\"";
            string compName = "\"CompanyNames\":\"" + companynames + "\"";
            Regex rgx = new Regex(pattern1);
            Regex rgx2 = new Regex(pattern2);
            Regex rgx3 = new Regex(pattern3);
            Regex rgx4 = new Regex(pattern4);
            Regex rgx5 = new Regex(pattern5);

            
            responseString = rgx.Replace(responseString, "TimeStamp");
            responseString = rgx2.Replace(responseString, "Headline");
            responseString = rgx3.Replace(responseString, "NewsText");
            responseString = rgx4.Replace(responseString, "\"InstrumentIDs\":\"\"");
            responseString = rgx5.Replace(responseString, compName);
            
            return Ok(responseString);


            /*var set1 = newsDataSet.Where((p) => p.CompanyNames.Any(x => x==companynames) && Tonumber(p.TimeStamp) <= Tonumber(totime) && Tonumber(p.TimeStamp) >= Tonumber(fromtime));
            if (set1 == null)
            {
                return NotFound();
            }



            return Ok(set1);*/
        }

        [Route("api/newsdata/start_date={fromtime}/end_date={totime}/instrumentids={instrumentids}")] //Input period and instrument id
        [HttpGet]
        public IHttpActionResult GetNewsDataByInstrumentIDs(string instrumentids, string fromtime, string totime)
        {
            //string[] namelist = companynames.Split(',');
            string parameters = "&from-date=" + fromtime + "&to-date=" + totime + "&q=" + instrumentids;
            string link = "http://content.guardianapis.com/search?api-key=660d5313-dd89-4989-a0d6-dbf0877f277b&section=news" + parameters;
            var request = (HttpWebRequest)WebRequest.Create(link);
            var response = (HttpWebResponse)request.GetResponse();
            var responseString = new System.IO.StreamReader(response.GetResponseStream()).ReadToEnd();

            string pattern1 = "webPublicationDate";
            string pattern2 = "webTitle";
            string pattern3 = "webUrl";
            string pattern4 = "\"sectionId\":\"[a-z]*\"";
            string pattern5 = "\"sectionName\":\"[A-Z][a-z]*\"";
            string compId = "\"InstrumentIDs\":\""+instrumentids+"\"";
            string compName = "\"CompanyNames\":\"" + instrumentids + "\"";
            Regex rgx = new Regex(pattern1);
            Regex rgx2 = new Regex(pattern2);
            Regex rgx3 = new Regex(pattern3);
            Regex rgx4 = new Regex(pattern4);
            Regex rgx5 = new Regex(pattern5);


            responseString = rgx.Replace(responseString, "TimeStamp");
            responseString = rgx2.Replace(responseString, "Headline");
            responseString = rgx3.Replace(responseString, "NewsText");
            responseString = rgx4.Replace(responseString,compId );
            responseString = rgx5.Replace(responseString, compName);

            return Ok(responseString);
        
            /*var set1 = newsDataSet.Where((p) => Tonumber(p.TimeStamp) <= Tonumber(totime) && Tonumber(p.TimeStamp) >= Tonumber(fromtime) && p.InstrumentIDs.Any(x => x == instrumentids));
            if (set1 == null)
            {
                return NotFound();
            }



            return Ok(set1);*/
        }

        /*public decimal Tonumber(string time)
        {
            decimal result = 0;
            if (time != null && time != string.Empty)
            {
                // 正则表达式剔除非数字字符（不包含小数点.）
                //time = Regex.Replace(time, @"[^/d./d]", "");
                time = Regex.Replace(time, @"[^\d.\d]", "");
                // 如果是数字，则转换为decimal类型
                if (Regex.IsMatch(time, @"^[+-]?\d*[.]?\d*$"))
                {
                    result = decimal.Parse(time);
                }
            }
            return result;
        }*/

        
        
    }
}
