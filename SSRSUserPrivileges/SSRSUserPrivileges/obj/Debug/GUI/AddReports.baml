   M S B A M L   `   `   `  ����  SSRSUserPrivileges'    !SSRSUserPrivileges.GUI.AddReports   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	     � Title$  
AddReports��5   	   $	��600��6   $��535.802��6)    � 	Resources 5         rvReportItemTemplate d� 	IsChecked��%j   &	        �� 5   
   .R�5      �� ��6   �� *Name+ 60   �� *	IsChecked+6R   5      5	      .��5      �  � ColumnDefinitions 5   
   �� 5      $��* �6   6+   �� 5      $��5 �6   6+   �� 5      $	��100 �6   6-    � RowDefinitions 5   
   �� 5      $��30 �6   6*   �� 5      6   �� 5      $��5 �6   6)   .+�5   
   �� $��5,0,0,0q�6   .��6!   Select Reports61   R� 5   
   -   5        $��	tvReports��5      $��1��6$   $��5,0,0,0q�61   ��5      ��  �� RelativeSource ��  �� AncestorType$ WindowL� �� Mode$ FindAncestor=� �� Path$ Items���5      �� 5      	 �� ItemsSource	 6+   �� *
Children+.R�5       �� ��6   �� *Name+ 68   �� *	IsChecked+6Z   5!      5%      �� 5'   
   $��1��6   $��2��6"   
 �� Orientation$
 Vertical=�62   .+�5(      �� -   5        $��btnAdd��5(      $��50��6#   $
��Left=�6.   $��10,0,0,0q�6I   .��6p   Add6u   �� 5)      -   5        $��	btnCancel��5)      $��50��6&   $
��Left=�61   $��10,5,0,0q�6L   .��6v   Cancel6~   5*      5+      5,      