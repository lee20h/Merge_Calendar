using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace ExamScheduleAPI
{
    public class ExamPlan
    {
        public string description;
        public string examstartdt,examenddt;
        public string examregstartdt, examregenddt;
        public string passstartdt, passenddt;
    }
    class Program
    {
        static HttpClient client = new HttpClient();
        static void Main(string[] args)
        {
            string url = "http://openapi.q-net.or.kr/api/service/rest/InquiryTestDatesNationalProfessionalQualificationSVC/getList"; // URL
            url += "?ServiceKey=" + "SNVlyyXcLkzN781n2F%2Bp4rspvnLpqSVIziDWEGgfNorh61NH04rS%2BKTtXsEwR1pp%2FIStlcOA18KAQ4dNxuEdlA%3D%3D"; // Service Key
            url += "&seriesCd=03";
            //url += "&serviceKey=SNVlyyXcLkzN781n2F%2Bp4rspvnLpqSVIziDWEGgfNorh61NH04rS%2BKTtXsEwR1pp%2FIStlcOA18KAQ4dNxuEdlA%3D%3D";

            var request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "GET";

            string results = string.Empty;
            HttpWebResponse response;
            using (response = request.GetResponse() as HttpWebResponse)
            {
                StreamReader reader = new StreamReader(response.GetResponseStream());
                results = reader.ReadToEnd();
            }
            XmlDocument xml = new XmlDocument();
            xml.LoadXml(results);
            XmlNodeList xmlNodeList = xml.GetElementsByTagName("item");

            foreach (XmlNode xn in xmlNodeList)
            {
                string description = xn["description"].InnerText;
                string examstartdt = xn["examstartdt"].InnerText;
                Console.WriteLine(description + '\n' + examstartdt + '\n');

            }
            Console.WriteLine("\n\n" + results);



        }
    }
}
