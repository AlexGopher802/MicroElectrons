   M S B A M L   `   `   `  ���� P  LMicroElectronsManager, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null=#clr-namespace:MicroElectronsManagerMicroElectronsManager  +    %MicroElectronsManager.AddSupplyWindow   W SPresentationCore, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5local#clr-namespace:MicroElectronsManager     -     � Title$+  %Добавление поставки��$
��1000��$
��1000��$	��450��$	��450�� � WindowStyle$ SingleBorderWindow=� � WindowStartupLocation$ CenterScreen=�.���  � ColumnDefinitions �� $	��400 ��� $��* �.+�� $��0��$��Center=�$��Center=� � RowDefinitions �� �� �� �� �� �� �� .+�� $��0�� �� �� .+�	� -    	�Name$ RbIsSell��$��Продажа�� d� 	IsChecked$
 TrueY�$��0��$��18(�	� -   $ RbIsBuy��$��Покупка��$��1��$��18(�� $��1��$��0 0 0 25q� �� $��* ��� $
��auto �.+��� -    ��Name$ CbCounterparty��$��0��$	��200��$��Center=�$��Center=�$��18(�$��0 0 10 0q��� -    ��Name$ BtnAddCounterparty��$��+��$��1��$��Center=�$��Center=�$
��20 2q�$��18(����#ffffff���#212529���#212529� $��2��$��0 0 0 15q� �� $��* ��� $
��auto �.+��� -   $ 
CbProducts��$��0��$	��200��$��Center=�$��Center=�$��18(�$��0 0 10 0q��� -   $ BtnAddProduct��$��+��$��1��$��Center=�$��Center=�$
��20 2q�$��18(����#ffffff���#212529���#212529� $��3��$��0 0 0 15q� �� $	��120 ��� $
��auto �.+��� $��Количество:��$��18(�$��Center=��� -   	 ��Name$	 
TbQuantity��$��1��$	��150��$��Center=�$��Center=�$��18(�$��0 0 10 0q�� $��4��$��0 0 0 15q� �� $	��120 ��� $
��auto �.+��� $��Сумма:��$��18(�$��Center=��� -	   $	 TbPrice��$��1��$	��150��$��Center=�$��Center=�$��18(�$��0 0 10 0q��� -
   $ BtnAddProductInSupply��$(��"Добавить в корзину��$��5��$��Center=�$��Center=�$
��20 2q�$��18(�$��0 0 0 25q����#ffffff���#212529���#212529� $��6�� �� �� .+��� -   $ 
BtnConfirm��$��Подтвердить��$	��120��$��0��$��18(�$	��0 2q����#ffffff���#212529���#212529$��Center=�$��Center=��� -   $ 	BtnCancel��$��Отмена��$	��120��$��1��$��18(�$	��0 2q����#ffffff���#212529���#212529$��Center=�$��Center=�&   System.Windows.Controls.DataGrid  -   
  Name$
 DataGridProducts��$��1��$��Stretch=�$��Stretch=�$��18(�   CanUserAddRows$ False��   CanUserDeleteRows$ False��   CanUserResizeRows$ False��   AutoGenerateColumns$ False��   SelectionMode$ Single=�   RowHeaderWidth$ 0Q�$��2q�   HorizontalGridLinesBrush
 ��������#212529   GridLinesVisibility$ 
Horizontal=�   	RowHeight$ 35Q�   
IsReadOnly$
 True��   Columns 0  *System.Windows.Controls.DataGridTextColumn  ,  &System.Windows.Controls.DataGridColumn   Header$ Наименование��   Width5  /System.Windows.Controls.DataGridLengthConverter$ * 1  +System.Windows.Controls.DataGridBoundColumn   Binding ��  �� Path$
 Name�  $ Количество��$	 140  �� $ Quantity�  $ 
Сумма��$	 140  �� $ Price�