   M S B A M L   `   `   `  ����  	MVFSystem    MVFSystem.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	     � Title$  
MainWindow��5   	   $	��300��6   $	��600��6)    � WindowStartupLocation$ CenterScreen=�65    � DataContext 5   	   �� *StudentList+   Locator �� Source# [  .��5      �  � RowDefinitions 5   
   �� 5      $
��Auto �6   6,   �� 5	      $��* �6   6)   .+�5   
   ��  �� Orientation$ 
Horizontal=�6   $��0��6.   .+�5      �� $	��100��6   $��30��6!   $��0,5,0,5q�6-   $��不进行代码封装��6>   ��6P   �� *
AddCommand+6q   �� 5      $	��200��6   $��30��6!   $��20,5,0,5q�6-   $/��)封装验证(通过反射获取验证对)��6?   ��6Y   �� *AddCommandTo+6|   5      &   System.Windows.Controls.DataGrid  5   
   $��1��6      AutoGenerateColumns .  ��6    �� *StudentList+ �� SelectedItem 6D   �� *CurrentItem+   Columns 5      0  *System.Windows.Controls.DataGridTextColumn  5      ,  &System.Windows.Controls.DataGridColumn	   Header$	 序号��6%   
   Width5  /System.Windows.Controls.DataGridLengthConverter$

 auto 61   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6>   �� *Id+6W     5      $	 名称��6%   $

 auto 61    6>   �� *sname+6Z     5      $	 住址��6%   $

 auto 61    6>   �� *
saddress+6]     5      $	 年龄��6%   $

 auto 61    6>   �� *sage+6Y     5      $	 电子邮件��6%   $

 auto 63    6@   �� *emails+6]   5      5      5      