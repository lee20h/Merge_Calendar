using Google.Apis.Auth.OAuth2;
using Google.Apis.Calendar.v3;
using Google.Apis.Calendar.v3.Data;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MergeCalendar.classes;

namespace MergeCalendar
{
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindow : Window
    {
        string google_clientId = "835449274544-0v5i8apf8n2i0nui1fosgu0emib0f30v.apps.googleusercontent.com";//From Google Developer console https://console.developers.google.com
        string google_clientSecret = "o6iRrypPzW381fqB8Xmk1Htf";//From Google Developer console https://console.developers.google.com
        char[] delimiterChars = { ' ', ',', '.', ':', '\n', '-' };
        int month;
        static string strPath = AppDomain.CurrentDomain.BaseDirectory;

        FileStream fs = new FileStream(strPath + "\\Todo.txt", FileMode.Append, FileAccess.Write);
        public MainWindow()
        {
            InitializeComponent();
            month = CalendarView.DisplayDate.Month;

            strPath = strPath + @"save";

            DirectoryInfo di = new DirectoryInfo(strPath);

            if (!File.Exists(strPath))
            {
                StreamWriter sw = new StreamWriter(strPath + "\\Todo.txt");
                sw.Close();

                StreamWriter sw2 = new StreamWriter(strPath + "\\finished.txt");
                sw2.Close();

                di.Create();
            }


            string[] textvalue = System.IO.File.ReadAllLines(strPath+"\\Todo.txt");
            if(textvalue.Length > 0 )
            {
                for(int i=0; i< textvalue.Length; i++)
                {
                    CheckBox box = new CheckBox();
                    box.Content = textvalue[i];
                    box.FlowDirection = FlowDirection.RightToLeft;
                    ToDolist.Items.Add(box);
                    box.Checked += CheckBox_Checked;
                }
            }

            textvalue = System.IO.File.ReadAllLines(strPath + "\\finished.txt");
            if (textvalue.Length > 0)
            {
                for (int i = 0; i < textvalue.Length; i++)
                {
                    CheckBox box = new CheckBox();
                    box.Content = textvalue[i];
                    box.FlowDirection = FlowDirection.RightToLeft;
                    finished.Items.Add(box);
                    box.IsChecked = true;
                }
            }


        }


        private void Sync_APItoCal_Click(object sender, RoutedEventArgs e)
        {
            EventListV.Items.Clear();
            string userName = "1";//new Random().Next().ToString();//  A string used to identify a user.
            string[] scopes = new string[] {
                CalendarService.Scope.Calendar, // Manage your calendars
 	            CalendarService.Scope.CalendarReadonly // View your Calendars
            };

            // here is where we Request the user to give us access, or use the Refresh Token that was previously stored in %AppData%
            UserCredential credential = GoogleWebAuthorizationBroker.AuthorizeAsync(new ClientSecrets
            {
                ClientId = google_clientId,
                ClientSecret = google_clientSecret
            }, scopes, userName, CancellationToken.None, new FileDataStore("Daimto.GoogleCalendar.Auth.Store")).Result;

            CalendarService service = new CalendarService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = "MergeCalendar",
            });

            //Console.WriteLine(service.ToString());
            EventsResource.ListRequest request = service.Events.List("primary");
            request.TimeMin = DateTime.Now;
            request.ShowDeleted = false;
            request.SingleEvents = true;
            request.MaxResults = 30;
            request.OrderBy = EventsResource.ListRequest.OrderByEnum.StartTime;

            // List events.
            Events events = request.Execute();

            if (events.Items != null && events.Items.Count > 0)
            {
                foreach (var eventItem in events.Items)
                {
                    string when = eventItem.Start.DateTime.ToString();
                    if (String.IsNullOrEmpty(when))
                    {
                        when = eventItem.Start.Date;
                    }
                    //Console.WriteLine("{0} ({1})", eventItem.Summary, when);
                    //EventListV.Items.Add(when + "\n" + eventItem.Summary);
                    string[] vs = when.Split(delimiterChars);

                    Schedule schedule = new Schedule();
                    schedule.Description = eventItem.Summary;
                    schedule.Begin = (DateTime)eventItem.Start.DateTime;
                    schedule.End = (DateTime)eventItem.End.DateTime;
                    //schedule.End 
                    Refresh_calendar(vs);
                }
            }
        }

        private void Add_Event_Click(object sender, RoutedEventArgs e)
        {
            AddSchedule addSchedule = new AddSchedule();
            addSchedule.ShowDialog();
        }

        private void Refresh_calendar(string[] words)
        {
            if (Convert.ToInt32(words[1]) == CalendarView.DisplayDate.Date.Month)
            {
                CalendarView.SelectedDates.Add(new DateTime(Convert.ToInt32(words[0]), Convert.ToInt32(words[1]), Convert.ToInt32(words[2])));
            }
        }

        private void CalendarView_DisplayDateChanged(object sender, CalendarDateChangedEventArgs e)
        {
            System.Windows.Controls.Calendar calendar = (System.Windows.Controls.Calendar)sender;
            if(month != calendar.DisplayDate.Month && EventListV != null)
            {
                
                foreach(var Event in EventListV.Items)
                {
                    Refresh_calendar(Event.ToString().Split(delimiterChars));
                }
                
            }
            month = calendar.DisplayDate.Month;
        }
        private void TodotxtBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                CheckBox box = new CheckBox();

                box.Content = TodotxtBox.Text;
                box.FlowDirection = FlowDirection.RightToLeft;
                ToDolist.Items.Insert(1, box);
                box.Checked += CheckBox_Checked;

                
                StreamWriter sw = new StreamWriter(fs);
                sw = File.AppendText(strPath + "\\Todo.txt");
                sw.WriteLine(TodotxtBox.Text);
                TodotxtBox.Text = "추가할 To-Do Item";
                sw.Close();
            }
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            if (sender == null) return;
            CheckBox box = new CheckBox();
            TextBlock txt = new TextBlock();
            txt.Text = (sender as CheckBox).Content.ToString();
            txt.TextDecorations = TextDecorations.Strikethrough;
            box.FlowDirection = FlowDirection.RightToLeft;
            box.Content = txt;
            box.IsChecked = true;




            StreamWriter sw = new StreamWriter(fs);
            sw = File.AppendText(strPath + "\\fishied.txt");
            sw.WriteLine((sender as TextBlock).Text);
            sw.Close();




            finished.Items.Add(box);
            ToDolist.Items.Remove(sender);
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
