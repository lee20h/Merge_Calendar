   M S B A M L   `   `   `  ����O  Kmscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e0891&clr-namespace:System;assembly=mscorlibSystem  5   	    MergeCalendar   MergeCalendar.AddSchedule   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   6sys&clr-namespace:System;assembly=mscorlib     5   	   V RMaterialDesignThemes.Wpf, Version=2.5.1.1345, Culture=neutral, PublicKeyToken=nullFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5	   	     � Title$  
MainWindow��5   	   $	��200��6   $	��320��6)      MaterialDesignBody#���   5
      $��Regular"�5      $��13(�5      &   System.Windows.Media.TextOptions   TextFormattingMode$ Ideal=�5         TextRenderingMode$
 Auto=�5        MaterialDesignPaper#���  5        MaterialDesignFont#���  5      .��5      �  � ColumnDefinitions 5   
   �� 5      �� 5      �� 5       � RowDefinitions 5   
   �� 5      �� 5      �� 5      �� 5      .+�5   
   �� $��시작 날짜/시간 : ��6   $��0��6'   $��Center=�5      �� 5    
   $��종료 날짜/시간 : ��6   $��1��6&   $��Center=�5!      �� 5"   
   $��	일정 : ��6   $��2��6    $��Center=�5#      $��Right=�5$      �� 5%   
   -   5         ��Name$ acceptButton��5%      $��3��6'   $��3��64   $��일정 만들기��5&      5'      (  "System.Windows.Controls.DatePicker  5(   
   $��1��6   
 �� Now   SelectedDate# Z 5)        5*   
   $��1��6   $��1��6%   # Z 5+      )  #MaterialDesignThemes.Wpf.TimePicker  5,   
      SelectedTime$ 13:00X�6$   $��2��69   $��0��6I     5-   
   $ 13:00X�6$   $��2��6:   $��1��6J   �� 5.   
   -   5        	 ��Name$	 ScheduleTxtBox��5.      $��1��6*   $��2��6:   $��2��6G   5/      50      