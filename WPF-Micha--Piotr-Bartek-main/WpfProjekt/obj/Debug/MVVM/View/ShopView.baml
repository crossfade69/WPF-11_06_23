   M S B A M L   `   `   `  ����E  AWpfProjekt, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null;"clr-namespace:WpfProjekt.MVVM.ViewWpfProjekt.MVVM.View  5      #    WpfProjekt.MVVM.View.ShopView   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      4local"clr-namespace:WpfProjekt.MVVM.View     5      .��5      �   � RowDefinitions  5   
   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $��* �6   .+�5   
   �� $��
Game Store��6   $��24(�6&   $
��Bold"�64   $��Center=�6F   $��	0,10,0,20q�6c   
�������6v   �� 5   
   $��1��6    �� Orientation$ 
Horizontal=�6"   $
��Left=�6;   $��10q�6V   .+�5      �� $��Search: ��6   $��Center=�6(   $��0,0,5,0q�6C   
�������6T   �� 5      -   5        $��SearchTextBox��5      $	��200��6+   $��Center=�67   �� 5      -   5        $��SearchButton��5      $��Search��6)   $��5,0,0,0q�6U   5      �� 5   
   -   5        $��BuyAndDownloadButton��5      $��1��6   $��Buy and Download��6:   $��Right=�6U   $	��Top=�6q   $��10q�6�   $y�Hidden=�6�   6�   �� 5   
   -   5        $��GamesInStoreListView��5       $��2��6   ���	#FF232323���	#FF8C8C8C � 	Resources 5       �� 
TargetType �� Triggers P� Property P� Value �� Property �� Value��%�   '��       �� 5      " ��6    5      P� 5        �@J�IsMouseOver . . �5!      ��  � �� �	#FF4444446;   5"      P� 5#       �@��
IsSelected . . �5$      ��  � �� �	#FF4444446;   5%      5'      5(      	 �� View	 5)      �� 5*      .P�5+      �� $��Id��6%   
 �� Width$
 0��61    �� CellTemplate 5,      �� 5-      .R�5.   "   �� $y�Hidden=�6@   ��6,   �� *id+6@   5/      51      �� 52      $��Image��6%   $	
 150��64    53      �� 54      .R�55   "   �� $	��100��6A   $	��100��6M   ��6(   �� *image+6M   56      58      �� 59      $��Title��6%   $	
 100��64    �� HeaderContainerStyle 5:      �� 5;      " ��6$   .�5<   "   ��  � �� 6<   �� *SortByTitleCommand+5=       �� DisplayMemberBinding 5?      �� 5@       �� Path$ title�6&   5B      �� 5C      $��Category��6%   $	
 100��67    5D      �� 5E      " ��6$   .�5F   "   ��  � �� 6<   �� *SortByCategoryCommand+5G       5I      �� 5J      $ category�6&   5L      �� 5M      $��Rating��6%   $	
 200��65    5N      �� 5O      " ��6$   .�5P   "   ��  � �� 6<   �� *SortByRatingCommand+5Q       5S      �� 5T      $ rating�6&   5V      5Z      5\      5_      5`      