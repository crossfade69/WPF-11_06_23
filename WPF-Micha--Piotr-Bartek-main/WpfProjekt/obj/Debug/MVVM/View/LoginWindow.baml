   M S B A M L   `   `   `  ����E  AWpfProjekt, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null'clr-namespace:WpfProjekt
WpfProjekt  5   	   E'clr-namespace:WpfProjekt.MVVM.ViewModelWpfProjekt.MVVM.ViewModel  5   	   &     WpfProjekt.MVVM.View.LoginWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   *localclr-namespace:WpfProjekt     5   	   =	viewModel'clr-namespace:WpfProjekt.MVVM.ViewModel     5   	   -   5        $	��222��5
   	   $	��444��6   $	��222��5   	   $	��444��6   $	��222��5   	   $	��444��6     � WindowStyle$
  None=�5   	    � 
ResizeMode$ 	CanResize=�5   	   ���#272537 � AllowsTransparency . '  !System.Windows.Shell.WindowChrome   WindowChrome 5        5   
      GlassFrameThickness$ 0q�6      CornerRadius$ 0��6/      CaptionHeight$ 0Q�6@    � DataContext 5      -  'WpfProjekt.MVVM.ViewModel.MainViewModel  5   
    � 	Resources 5         WindowButtonStyle	 �� 
TargetType
 �� Property �� Value �� 
TargetType �� CornerRadius��%`  &	        �� 5   
   "	 ��6*   .�5      �� 
 � �� �	#FF3B3F466+   �� 5       
 � ��
 ���� 6,   �� 5!      
 � ��
 �����6+   �� 5"      
 � ��$ Segoe UI)�6+   �� 5#      
 � ��$ 16(�6)   �� 5$      
 � ��$ 5q�6'   �� 5%      
 � ��$ 10q�6(   �� 5&      
 � ��$ 40��6&   �� 5'      
 � ��$ 40��6'   �� 5(      
 � �� 5)      �� 5*      " ��6&   .R�5+      �� #��z��6!   #��z��5,      #��z��5-      $ 5��5.      ._�5/      �� $��Center=�6/   $��Center=�50   +   51      52      54      55      56      .��58      � .+�59   
   �  � RowDefinitions 5:      �� 5;      $��40 �6    �� 5<      .+�5?      �� $��Login��6   $��Center=�5@      $��Center=�5A      
�������$��22(�5C      $��20,0,0,0q�5D      �� 5E       �� Orientation$ 
Horizontal=�6   $��Right=�62   $	��120��6O   .+�5H      �� -   5        $��30��5I      $��20��6    $��Center=�5J      $��Right=�5K      #��[  5L      .��5N      F� ����    @KL р��@KL 	
�������$��2��5P      5Q      �� 5S      -   5        $��30��5T      $��20��6    $��Center=�5U      $
��Left=�5V      #��[  5W      .��5Y      F� +����        �    ��� ���� ��� ����     	
�������$��2��5[      5\      �� 5^      -   5        $��Center=�5_      $��Right=�5`      $��30��5a      $��20��6(   #��[  5b      .��5d      F� +����        ���� ��� ����     �    ��� 	
�������$��2��5f   !   5g      5h      �� 5j      -   5         ��Name$ LoginButton��5j      $��Log in��6*   $��Center=�5k      $��	0,124,0,0q�5l      $��1��6(   #��[  5m      $	��Top=�5o      $��40��5p      $	��120��5q       J� RenderTransformOrigin$ 0.525,0.619&�5s      �� 5v      -   5        $��UsernameTextBox��5v      $��Stretch=�6-   $��
130,19,0,0q�6K   $��1��6_    �� TextWrapping$
 Wrap=�6l   $	��Top=�6�   $	��282��6�   $��27��6�   $��18(�6�   6�   G� 5w      -   5        $��PasswordTextBox��5w      $��Stretch=�61   $��
130,75,0,0q�6O   $��1��6c   $	��Top=�6q   $	��282��6�   $��27��6�   $��18(�6�   6�   �� 5x      $
��Left=�6   $��
70,19,0,60q�63   $��1��6G   $
��Wrap=�6T   $
��Name��6h   $	��Top=�6t   
�������$��27��6�   $��55��6�   $��20(�6�   �� 5y      $
��Left=�6   $��
37,75,0,60q�63   $��1��6G   $
��Wrap=�6T   $��Password��6h   $	��Top=�6x   
�������$��27��6�   $��88��6�   $��20(�6�   5}      5      5�      