using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
    /// ScheduleControl.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class ScheduleControl : UserControl
    {


        public Schedule schedule
        {
            get { return (Schedule)GetValue(scheduleProperty); }
            set { SetValue(scheduleProperty, value); }
        }

        // Using a DependencyProperty as the backing store for schedule.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty scheduleProperty =
            DependencyProperty.Register("schedule", typeof(Schedule), typeof(ScheduleControl), new PropertyMetadata(new Schedule(), OnScheduleChanged));

        private static void OnScheduleChanged(DependencyObject obj, DependencyPropertyChangedEventArgs arg)
        {
            ScheduleControl control = obj as ScheduleControl;
            if(control != null)
            {
                control.descriptionTextBlock.Text = (arg.NewValue as Schedule).Description;
                control.startTextBlock.Text = (arg.NewValue as Schedule).Begin.ToString();
                control.endTextBlock.Text = (arg.NewValue as Schedule).End.ToString();
                control.sourceTextBox.Text = (arg.NewValue as Schedule).Source;
            }
        }
        public ScheduleControl()
        {
            InitializeComponent();
            
        }
    }
}
