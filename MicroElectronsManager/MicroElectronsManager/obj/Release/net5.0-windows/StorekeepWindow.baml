   M S B A M L   `   `   `  ���� P  LMicroElectronsManager, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null=#clr-namespace:MicroElectronsManagerMicroElectronsManager  +    %MicroElectronsManager.StorekeepWindow   W SPresentationCore, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5local#clr-namespace:MicroElectronsManager     -     � Title$  
Склад��$
��1366��$
��1366��$	��768��$	��768�� � WindowState$ 	Maximized=� � WindowStyle$ SingleBorderWindow=� � WindowStartupLocation$ CenterScreen=�.���  � RowDefinitions �� $	��120 ��� $��* �.+�� $��0�����#212529.+��� -    ��Name$ 	TbWelcome��$0��*Здравствуйте, юзернейм�����#ffffff$��18(�$��Center=�$��Center=��� -    ��Name$ BtnExit��$��
Выход��$
��Left=�$��Center=�$��20 0 0 0q�$
��30 5q����#ffffff���#ffffff���#212529$��15(�$��18(��� $��/res/logo.png��$��Right=�$��
0 15 20 15q�&   System.Windows.Controls.DataGrid  -     Name$ DataGridStorage��$��1��$��Stretch=�$��Stretch=�$��18(�   CanUserAddRows$ False��	   CanUserDeleteRows$	 False��
   CanUserResizeRows$
 False��   AutoGenerateColumns$ False��   SelectionMode$ Single=�   RowHeaderWidth$ 0Q�$��2q�   HorizontalGridLinesBrush
 ��������#212529   GridLinesVisibility$ 
Horizontal=�   	RowHeight$ 35Q�   
IsReadOnly$
 True�� � ContextMenu �� .@�m� -    m�Name$ CmChangeMethod��$4��.Изменить способ хранения��${�False��   Columns 0  *System.Windows.Controls.DataGridTextColumn  ,  &System.Windows.Controls.DataGridColumn   Header$ Наименование��   Width5  /System.Windows.Controls.DataGridLengthConverter$ * 1  +System.Windows.Controls.DataGridBoundColumn   Binding ��  �� Path$
 Name�  $ Категория��$ *  �� $ CategoryName�  $ Количество��$	 170  �� $ Quantity�  $# Способ хранения��$	 180  �� $ StorageMethod�  $ Id��$ *    
Visibility$ 	Collapsed=� �� $ 	ProductId�