   M S B A M L   `   `   `  ����S  OListOfExpensesAndIncomes, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullC&clr-namespace:ListOfExpensesAndIncomesListOfExpensesAndIncomes  5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R8clr-namespace:System.ComponentModel;assembly=WindowsBaseSystem.ComponentModel 5   	   O Kmscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089I2clr-namespace:System.Collections;assembly=mscorlibSystem.Collections 5	   	   1&clr-namespace:System;assembly=mscorlibSystem 5
   	   )    #ListOfExpensesAndIncomes.MainWindow   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   _ [MaterialDesignThemes.Wpf, Version=4.5.0.0, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5   	   2local&clr-namespace:ListOfExpensesAndIncomes  5   	   Bscm8clr-namespace:System.ComponentModel;assembly=WindowsBase  5   	   <col2clr-namespace:System.Collections;assembly=mscorlib  5	   	   0sys&clr-namespace:System;assembly=mscorlib  5
   	   -   5          � Title$%  My List Of Expenses And Incomes��5   	   $	��500��61   $	��900��6>   ���#ffdada$��Georgia)�5       � 	Resources 5       	  Title �� 
TargetType �� Property �� Value 	 stack"  System.Collections.ArrayList 	 types��%+  &	        &	 p     &	 �     �� 5   
   " ��6   .�5      ��  � ��$ 20(�6)   �� 5       � ��$ Center=�64   5      �� 5   
   " ��6   .�5      ��  � ��$ 25q�6'   5        5   
   ��5      
Income��6!   ��5      Purchase��6#   5      5      .��5      ]� ��. .+�5   
   � ���#ffdada � ColumnDefinitions 5       �� 5!      �� 5"      �� 5#      �� 5$      �� 5%      �� 5&      .+�5)      �� $��	20 0 10 0q�6   
�������$��Center=�6;   $	��380��6V    �� CornerRadius$ 25��6e   $��2��6w    J� Effect 5*      3  -System.Windows.Media.Effects.DropShadowEffect  5+         
BlurRadius$ 50Q�6'   	   ShadowDepth$	 0Q�67   
   Color$
 	LightGray��6G   ._�5.      �� #���  6   .+�5/      �� $��New data��6    #���   60   �� 51      -   5         ��Name$ 	typeField��51   O   $��Georgia)�6   $	��0 5q�64   $	��0 5q�6B   .@�52      �� -   5         ��Name$ IncomeComboBoxItem��52   J   $��Income��6'    �� 
IsSelected . 6J   �� 53      -   5        $ PurchaseComboBoxItem��53   :   $��Purchase��6'   6:   54      (  "System.Windows.Controls.DatePicker  56      -   5          Name$ 	dateField��56   T   )  #MaterialDesignThemes.Wpf.HintAssist   Hint$
 Date��6!   $	��0 5q�6G   6T   )  #MaterialDesignThemes.Wpf.TimePicker  57      -   5          Name$ 	timeField��57   V   $
 Time��60   6V   �� 59      -   5         ��Name$ sumField��59   e   $
 Summ��6   $	��0 5q�6D    �� TextWrapping$
 Wrap=�6Q   6e   �� 5:      -   5        $ 
descrField��5:   |   $ Description��6   $	��0 5q�6K   $	��100��6X   $
 Wrap=�6h   6|   �� 5;      -	   5        $��0 5 0 0q�5;      $��Enter��6.   6>   5<      5=      �� 5@      $��2��6   $��4��6%   $��20q�69   
�������$��Stretch=�6X   $ 25��6t    5A        5B      $ 50Q�6'   $	 0Q�67   $
 	LightGray��6G   ._�5D      ]� $��20q�6   .+�5E      �� #���   6    $��History��6@   $	��Top=�6O   $��5q�6d   &   System.Windows.Controls.DataGrid  5F      -
   5          Name$ historyGrid��5F      
�������$	��500��5G         AutoGenerateColumns .     GridLinesVisibility$ Vertical=�6J      RowStyle 5H      �� 5I      )  #System.Windows.Controls.DataGridRow"  6$   .�5J   "   ��  �@J�IsHitTestVisible . 6T    �� Triggers 5K   "   �� 5L   &    �� Binding 62   �� *Type+ �� Value
 Income6K   .a�5M   *   ��  � ��
 � d �6G   5N   '   �� 5O   &    62   �� *Type+ Purchase6K   .a�5P   .   ��  � ��
 � E��6K   5Q   '   5S         Columns 5U      0  *System.Windows.Controls.DataGridTextColumn  5V      ,	  &System.Windows.Controls.DataGridColumn 	  Header$
 Date��61    	  Width5
  /System.Windows.Controls.DataGridLengthConverter$	 170
 6�   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6?   ��  �� Path$ TimeOfTransaction� �� UpdateSourceTrigger$ PropertyChanged=�  �� StringFormat$  dd/MM/yyyy hh:mmtt��6�     5W      $
 Summ��61   $ 80
 6�    6?   �� $
 Summ�$ PropertyChanged=�6�     5X      $ Balance��61   $ 85
 6s   ! 	  
IsReadOnly! .  6B   �� $ BalanceAfterTransaction�6~   4  .System.Windows.Controls.DataGridComboBoxColumn  5Y      $
 Type��65   $	 100
 6�   -   5        
typeInGrid��"   SelectedItemBinding" 5Y   C   �� $
 Type�$ PropertyChanged=�#   ItemsSource# 6�   �� $ �� Source#$ [   5Z      $ Description��61   $ *
 6�    6F   �� $ Description�$ PropertyChanged=�6�   5\      5]      5^      5_      5`      5a      