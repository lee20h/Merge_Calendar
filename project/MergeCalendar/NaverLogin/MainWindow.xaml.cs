using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Web;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Net;
using System.IO;
using Newtonsoft.Json;

namespace NaverLogin
{
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindow : Window
    {
        public string AuthURL;
        string RedirectURL, ClientID, ClientSecret, token_get, State;

        private void Test_Click(object sender, RoutedEventArgs e)
        {
            //로그인 토큰
            MessageBox.Show(web.Source.AbsoluteUri);
            Uri myUri = new Uri(web.Source.AbsoluteUri);
            token_get = HttpUtility.ParseQueryString(myUri.Query).Get("code");

            WebClient webClient;

            string sAccessToken_Url = "https://nid.naver.com/oauth2.0/token";

            //쿼리 세팅
            webClient = new WebClient();
            webClient.QueryString.Add("grant_type", "authorization_code");
            webClient.QueryString.Add("client_id", ClientID);
            webClient.QueryString.Add("client_secret", ClientSecret);
            webClient.QueryString.Add("code", token_get);
            webClient.QueryString.Add("state", State);

            Stream stream_get = webClient.OpenRead(sAccessToken_Url);
            string sResultJson = new StreamReader(stream_get).ReadToEnd();
            //MessageBox.Show(sResultJson);

            // 접근 토큰
            AccessToken accessToken = (JsonConvert.DeserializeObject<AccessToken>(sResultJson));
            authentication.Text = accessToken.access_token;
            string token = accessToken.access_token;// 네이버 로그인 접근 토큰;
            string header = "Bearer " + token; // Bearer 다음에 공백 추가
            string apiURL = "https://openapi.naver.com/calendar/createSchedule.json";
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(apiURL);
            request.Headers.Add("X-Naver-Client-Id", ClientID);
            request.Headers.Add("X-Naver-Client-Secret", ClientSecret);
            request.Headers.Add("Authorization", header);
            request.Method = "POST";
            string calSum = "IS 하계 엠티";
            string calDes = "가기 싫다...";
            string calLoc = "완주 계곡";
            Random random = new Random();
            string uid = (random.Next()).ToString();

            string scheduleIcalString = "BEGIN:VCALENDAR\n" +
                  "VERSION:2.0\n" +
                  "PRODID:Naver Calendar\n" +
                  "CALSCALE:GREGORIAN\n" +
                  "BEGIN:VTIMEZONE\n" +
                  "TZID:Asia/Seoul\n" +
                  "BEGIN:STANDARD\n" +
                  "DTSTART:19700101T000000\n" +
                  "TZNAME:GMT%2B09:00\n" +
                  "TZOFFSETFROM:%2B0900\n" +
                  "TZOFFSETTO:%2B0900\n" +
                  "END:STANDARD\n" +
                  "END:VTIMEZONE\n" +
                  "BEGIN:VEVENT\n" +
                  "SEQUENCE:0\n" +
                  "CLASS:PUBLIC\n" +
                  "TRANSP:OPAQUE\n" +
                  "UID:" + uid + "\n" +                          // 일정 고유 아이디
                  "DTSTART;TZID=Asia/Seoul:20190518T130000\n" +  // 시작 일시
                  "DTEND;TZID=Asia/Seoul:20190519T173000\n" +    // 종료 일시
                  "SUMMARY:" + calSum + " \n" +                    // 일정 제목
                  "DESCRIPTION:" + calDes + " \n" +                // 일정 상세 내용
                  "LOCATION:" + calLoc + " \n" +                   // 장소
                  "RRULE:FREQ=YEARLY;BYDAY=FR;INTERVAL=1;UNTIL=20201231\n" +  // 일정 반복시 설정
                  "ORGANIZER;CN=관리자:mailto:admin@sample.com\n" + // 일정 만든 사람
                  "ATTENDEE;ROLE=REQ-PARTICIPANT;PARTSTAT=NEEDS-ACTION;CN=admin:mailto:user1@sample.com\n" + // 참석자
                  "CREATED:20161116T160000\n" +         // 일정 생성시각
                  "LAST-MODIFIED:20161116T160000\n" +   // 일정 수정시각
                  "DTSTAMP:20161116T160000\n" +         // 일정 타임스탬프
                  "END:VEVENT\n" +
                  "END:VCALENDAR";

            byte[] byteDataParams = Encoding.UTF8.GetBytes("calendarId=defaultCalendarId&scheduleIcalString=" + scheduleIcalString);
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = byteDataParams.Length;
            Stream st = request.GetRequestStream();
            st.Write(byteDataParams, 0, byteDataParams.Length);
            st.Close();
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            string status = response.StatusCode.ToString();
            if (status == "OK")
            {
                Stream stream = response.GetResponseStream();
                StreamReader reader = new StreamReader(stream, Encoding.UTF8);
                string text = reader.ReadToEnd();
                Console.WriteLine(text);
                MessageBox.Show(text);
            }
            else
            {
                Console.WriteLine("Error 발생=" + status);
                MessageBox.Show("Error 발생=" + status);
            }
            st.Close();
            response.Close();
        }

        public MainWindow()
        {
            InitializeComponent();
            ClientID = "zQwIkERyv2mJDHyEhtaj";
            ClientSecret = "vMf4JX0A5K";
            RedirectURL = "http://localhost";

            //확인용 키 생성
            State = (new Random()).Next().ToString();

            //인증요청용 전체 URL(쿼리 포함) 생성
            NameValueCollection listAuthURL_QueryString
                = HttpUtility.ParseQueryString(string.Empty);
            listAuthURL_QueryString["response_type"] = "code";
            listAuthURL_QueryString["client_id"] = ClientID;
            listAuthURL_QueryString["redirect_uri"] = RedirectURL;
            listAuthURL_QueryString["state"] = State;

            AuthURL = "https://nid.naver.com/oauth2.0/authorize?"
                + listAuthURL_QueryString.ToString();
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            web.Navigate(new Uri(AuthURL));
            
        }
    }
    public class AccessToken
    {
        public string access_token { get; set; }
        public string refresh_token { get; set; }
        public string token_type { get; set; }
        public string expires_in { get; set; }
    }

}
