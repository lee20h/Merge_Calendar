using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using BalanceManage_Start;
using BalanceManage_Start.Classes;

namespace BalanceManage_Start.View
{
    //-----------------------------------
    //2. Label의 파생 클래스인 WarningSign 컨트롤을 구현하시오.
    //   - WarningSign은 integer 타입의 dangerLevel dependency property를 갖는다.
    //   - dangerLevel의 변경이 발생할 시, OnDangerLevelChanged 콜백이 호출된다.
    //     -콜백에서는 dangerLevel integer 체크하여 아래와 같은 문장을 Contents에 표시한다.
    //      값이 50000 이하일 경우 "잔고 매우 위험!"
    //      값이 50000 이상, 100000 이하일 경우 "잔고 위험!"
    //      값이 200000 이상일 경우 "잔고 넉넉함!"
    //-----------------------------------

    class WarningSign : Label
    {

        public static DependencyProperty dangerLevelproperty
            = DependencyProperty.Register("dangerLevel", typeof(int), typeof(WarningSign),
            new PropertyMetadata(0, OndangerLevelChanged));

        public int dangerLevel //<-- new method. getvalue와 setvalue를 사용
        {
            //아래 get/set은 직접 호출되지 않음... 콜백을 사용해야 함
            get
            {
                return (int)GetValue(dangerLevelproperty);
            }
            set
            {
                SetValue(dangerLevelproperty, value);
            }
        }

        //콜백 함수
        private static void OndangerLevelChanged(DependencyObject source, DependencyPropertyChangedEventArgs e)
        {
            WarningSign warningsign = source as WarningSign;

            if (warningsign.dangerLevel > 100000)
            {
                warningsign.Content = "잔고 넉넉함!";
            }
            else if (warningsign.dangerLevel <= 100000 && warningsign.dangerLevel >= 50000)
            {
                warningsign.Content = "잔고 위험!";
            }
            else if (warningsign.dangerLevel < 50000)
            {
                warningsign.Content = "잔고 매우 위험!";
            }
        }

        //Coerce 콜백
        //private static object CoercedangerLevelChanged(DependencyObject d, object baseValue)
        //{
        //    WarningSign warningsign = d as WarningSign;

        //    if (warningsign.dangerLevel > 100000)
        //    {
        //        return warningsign.Content = "잔고 넉넉함!";
        //    }
        //    else if(warningsign.dangerLevel >= 100000 && warningsign.dangerLevel <= 50000)
        //    {
        //        return warningsign.Content = "잔고 위험!";
        //    }
        //    else
        //    {
        //        return warningsign.Content = "잔고 매우 위험!";
        //    }

        //}
    }
}


