   M S B A M L   `   `   `  ���� P  LMicroElectronsManager, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null=#clr-namespace:MicroElectronsManagerMicroElectronsManager  +    %MicroElectronsManager.HRmanagerWindow   W SPresentationCore, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5local#clr-namespace:MicroElectronsManager     -     � Title$  Отдел кадров��$
��1366��$
��1366��$	��768��$	��768�� � WindowState$ 	Maximized=� � WindowStyle$ SingleBorderWindow=� � WindowStartupLocation$ CenterScreen=�.���  � RowDefinitions �� $	��120 ��� $
��auto ��� $��* �.+�� $��0�����#212529.+��� -    ��Name$ 	TbWelcome��$0��*Здравствуйте, юзернейм�����#ffffff$��18(�$��Center=�$��Center=��� -    ��Name$ BtnExit��$��
Выход��$
��Left=�$��Center=�$��20 0 0 0q�$
��30 5q����#ffffff���#ffffff���#212529$��15(�$��18(��� $��/res/logo.png��$��Right=�$��
0 15 20 15q�o� $��1��$��18(�.@�m� -    m�Name$ MenuEmployee��$��Сотрудники��${�False�����#212529$	��5 3q�m� -   $ 
MenuLabors��$'��!Трудовые договора�����#212529$	��5 3q�m� -   $ MenuDismiss��$2��,Заявления на увольнение�����#212529$	��5 3q�m� -   $ MenuHoliday��$*��$Заявления на отпуск�����#212529$	��5 3q�� -    �Name$ ContentEmployee��$��2��$y�Visible=�.+�&   System.Windows.Controls.DataGrid  -	   	  Name$	 DataGridEmployee��$��2��$��Stretch=�$��Stretch=�$��18(�
   CanUserAddRows$
 False��   CanUserDeleteRows$ False��   CanUserResizeRows$ False��   AutoGenerateColumns$ False��   SelectionMode$ Single=�   RowHeaderWidth$ 0Q�$��2q�   HorizontalGridLinesBrush
 ��������#212529   GridLinesVisibility$ 
Horizontal=�   	RowHeight$ 35Q�   
IsReadOnly$
 True�� � ContextMenu �� .@�m� -
   $ CmAdd��$8��2Добавить нового сотрудника��m� -   $ 	CmDismiss��$)��#Уволить сотрудника��${�False��m� -   $ 	CmHoliday��$(��"Отправить в отпуск��${�False��   Columns 0  *System.Windows.Controls.DataGridTextColumn  ,  &System.Windows.Controls.DataGridColumn   Header$ Id��   Width5  /System.Windows.Controls.DataGridLengthConverter$ *    
Visibility$ 	Collapsed=�1  +System.Windows.Controls.DataGridBoundColumn   Binding ��  �� Path$ 
EmployeeId�  $ Фамилия��$ *  �� $ LastName�  $ Имя��$ *  �� $ 	FirstName�  $ Отчество��$ *  �� $ 
Patronymic�  $ Дата рождения��$	 160  �� $ Birthday�  $ 
Логин��$ *  �� $ Login�  $ Должность��$ *  �� $
 Post�  $ 
Оклад��$	 160  �� $ Salary�  $ Статус��$	 160  �� $ Status�� -   $ ContentLabors��$��2��$y�	Collapsed=�.+�  -   $	 DataGridLabor��$��2��$��Stretch=�$��Stretch=�$��18(�$
 False��$ False��$ False��$ False��$ Single=�$ 0Q�$��2q�
 ��������#212529$ 
Horizontal=�$ 35Q�$
 True��   $ Id��$ * $ 	Collapsed=� �� $ 
EmployeeId�  $ Фамилия��$ *  �� $ LastName�  $ Имя��$ *  �� $ 	FirstName�  $ Отчество��$ *  �� $ 
Patronymic�  $ Должность��$ *  �� $
 Post�  $ Дата��$	 160  �� $
 Date�� -   $ ContentDismiss��$��2��$y�	Collapsed=�.+�  -   $	 DataGridDismiss��$��2��$��Stretch=�$��Stretch=�$��18(�$
 False��$ False��$ False��$ False��$ Single=�$ 0Q�$��2q�
 ��������#212529$ 
Horizontal=�$ 35Q�$
 True��   $ Id��$ * $ 	Collapsed=� �� $ 
EmployeeId�  $ Фамилия��$ *  �� $ LastName�  $ Имя��$ *  �� $ 	FirstName�  $ Отчество��$ *  �� $ 
Patronymic�  $ Должность��$ *  �� $
 Post�  $ Дата��$	 160  �� $
 Date�� -   $ ContentHoliday��$��2��$y�	Collapsed=�.+�  -   $	 DataGridHoliday��$��2��$��Stretch=�$��Stretch=�$��18(�$
 False��$ False��$ False��$ False��$ Single=�$ 0Q�$��2q�
 ��������#212529$ 
Horizontal=�$ 35Q�$
 True��   $ Id��$ * $ 	Collapsed=� �� $ 
EmployeeId�  $ Фамилия��$ *  �� $ LastName�  $ Имя��$ *  �� $ 	FirstName�  $ Отчество��$ *  �� $ 
Patronymic�  $ Должность��$ *  �� $
 Post�  $ Дата начала��$	 160  �� $ 	DateStart�  $ Дата конца��$	 160  �� $ DateEnd�