   M S B A M L   `   `   `  ����O  Kmscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089I2clr-namespace:System.Collections;assembly=mscorlibSystem.Collections  5      1&clr-namespace:System;assembly=mscorlibSystem  5      S OListOfExpensesAndIncomes, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullO,clr-namespace:ListOfExpensesAndIncomes.ViewsListOfExpensesAndIncomes.Views 5      [2clr-namespace:ListOfExpensesAndIncomes.ViewsModels$ListOfExpensesAndIncomes.ViewsModels 5
      U/clr-namespace:ListOfExpensesAndIncomes.Services!ListOfExpensesAndIncomes.Services 5      0   *ListOfExpensesAndIncomes.Views.HistoryView   5      W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      Bcol2clr-namespace:System.Collections;assembly=mscorlib     5      6sys&clr-namespace:System;assembly=mscorlib     5      >local,clr-namespace:ListOfExpensesAndIncomes.Views     5      _ [Microsoft.Xaml.Behaviors, Version=1.1.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a3i+http://schemas.microsoft.com/xaml/behaviors  5	      ;vm2clr-namespace:ListOfExpensesAndIncomes.ViewsModels  5
      :serv/clr-namespace:ListOfExpensesAndIncomes.Services  5      $��Georgia)�5        � 	Resources  5      U QSystem.Runtime, Version=6.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"  System.Collections.ArrayList 	  types; 5ListOfExpensesAndIncomes.Services.TypeToTextConverter  ttot��%|   &	        &	 U       5   
   ��5      
Income��6!   ��5      Purchase��6#   5        5   
   6#   5      .��5      �� $	��Top=�6   $��Stretch=�6)   .+�5   
   �� $��20q�6   
�������$	��300��5       �� CornerRadius$ 25��6     J� Effect 5      3  -System.Windows.Media.Effects.DropShadowEffect  5         
BlurRadius$ 40Q�6#      ShadowDepth$ 0Q�5   #      Color$ 	LightGray��5   #   ._�5       &   System.Windows.Controls.DataGrid    DataGridTheme#��[ 6   ��5!      �� *UserHistory.Transactions+ �� Mode$ TwoWay=� �� SelectedItem 5"      ��  �� RelativeSource �� 	 �� AncestorType	 UserControl
 �� Path$%
 DataContext.SelectedTransaction�$ TwoWay=�   RowStyle 5%      �� 5&       �� 
TargetType)  #System.Windows.Controls.DataGridRow"  6   .�5'      ��  �� Property � �� �� Value$ Noto Sans Japanese)�67    �� Triggers 5(      �� 5)       �� Binding 6*   �� *Type+ �� Value
 Income6C   .a�5*   "   ��  � ��
 � d �6?   5+      �� 5,       6*   �� *Type+ Purchase6C   .a�5-   "   ��  � ��
 � E��6?   5.      P� 50       P� Property �@J�IsMouseOver P� Value$
 True��6=   . �51   "   ��  � �� �#f6f6f66?   �� 52   "    �@�Cursor$
 Hand��6;   53      55         Columns 58      0  *System.Windows.Controls.DataGridTextColumn  59      ,  &System.Windows.Controls.DataGridColumn   Header$
 Date��6)      Width5  /System.Windows.Controls.DataGridLengthConverter$	 110 5;   )     TextColumnStyle1	  +System.Windows.Controls.DataGridBoundColumn 	  ElementStyle# [ 5<   )     EditingTextColumnStyle 	  EditingElementStyle# [ 5=   )    	  Binding 5:   )   �� *TimeOfTransaction+$ TwoWay=� �� StringFormat$ {0:MM/dd/yyyy HH:mm}��5=   )     5>      $ Amount��6)   $	 100 5?   )   # [ 5A   )   # [ 5B   )    5@   )   �� *Summ+$ TwoWay=� �� ValidatesOnDataErrors$
 True��5B   )     5C      $ Balance��6)   $	 100 5E   )      
IsReadOnly$
 True��65     BalanceTextColumnStyle# [ 5F   )    5D   )   �� *BalanceAfterTransaction+$ TwoWay=�$ {0:0.00}��5F   )   4
  .System.Windows.Controls.DataGridTemplateColumn
  5G      $
 Type��6-   $	 100 5H   -    
  CellTemplate 5I      �� 5J      .R�5K   "   �� $��Center=�6-   .+�5L   &   ��   DataGridComboBox#��[ 5N   /    5L   /   �� *Type+$ TwoWay=� �� UpdateSourceTrigger$ PropertyChanged=���5M   /   ��  �� Source# [  *  $Microsoft.Xaml.Behaviors.Interaction    Triggers  5O   *   +  %Microsoft.Xaml.Behaviors.EventTrigger  5P   .   !   	EventName$! SelectionChanged��6=   *  $Microsoft.Xaml.Behaviors.TriggerBase"   Actions." 5Q   2   2  ,Microsoft.Xaml.Behaviors.InvokeCommandAction  #   Command# 5R   A   ��  �� 	 UserControl$#
 DataContext.UpdateInfoCommand�$   CommandParameter$ 5T   A   ��  �� 	 UserControl$%
 DataContext.SelectedTransaction�$ TwoWay=�5V   /   5X   '   5Y   #   5Z      5\        5^      $ Description��6)   %   CanUserResize$% False��5_   )   $ * 5a   )   # [ 5b   )   # [ 5c   )    5`   )   �� *Description+$ TwoWay=�5c   )   
  5e      $% False��6-    5f      �� 5g      .R�5h   "   �� .+�5i   &   �� & ��Name$& 
bin_button��5o   -   
������ 
������ $y�Hidden=�5k   -   $��30��5l   -   $��Center=�5m   -   $��Center=�5n   -   ��5p   -   ��  �� 	 UserControl$
 DataContext.DeleteCommand���5r   -   ��  �� 	 UserControl$%
 DataContext.SelectedTransaction�$ TwoWay=�.��5u   *   �� $k��eC:\Users\user\source\repos\ListOfExpensesAndIncomes\ListOfExpensesAndIncomes\Data\Images\bin_icon.png��60   $��Center=�5v   0   $��Center=�5w   0   $��20��5x   0   5y   '   5z   #   ' �� Triggers' 5|   "   �� 5}   &   $
 True��6�    62   �� *IsMouseOver+ �� 	 DataGridRow.a�5~   *   �� ( �� 
TargetName$( 
bin_button��61    � y�$ Visible=�6_   5   '   P� 5�   &    �@J�IsMouseOver$
 True��6E   . �5�   *   ��  �@�Cursor$
 Hand��6C   5�   '   5�      5�      5�      5�      5�      5�      