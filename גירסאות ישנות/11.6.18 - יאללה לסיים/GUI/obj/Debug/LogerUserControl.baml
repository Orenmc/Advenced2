   M S B A M L   `   `   `  ����>  :GUI, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullclr-namespace:GUIGUI  5          GUI.LogerUserControl   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      $local1clr-namespace:GUI     5        � 	Resources  5
      #  GUI.ColorTypeMessageConvertor   colorConvertor  FancyLog �� Property �� Value��%!  &	        &	        5   
   6+   �� 5   
   .�5      ��  � ��
 �   �61   �� 5       � ��
 � ���61   �� 5       � ��$ 35(�6/   �� 5       � ��$ Heavy"�61   �� 5       � ��
 ��  �62   �� 5       � ��$ 3q�66   5      5      .��5      � $
��auto��6   $
��auto��6    � RowDefinitions 5   
   �� 5      $
��auto �6   �� 5      $��* �6   .+�5   
   �� $
��Logs��6   #��[ 6   $��Center=�6A   �� 5   
   $��Resources/log.jpg��6   $��Right=�6+   $��50��6G   $��	710,5,0,0q�6S   $��Center=�6f   $��60��6�    J� RenderTransformOrigin$ -0.802,0.671&�6�   $��1��6�   �� 5   
   $��Resources/log.jpg��6   $
��Left=�6+   $��50��6F   $��7,5,0,0q�6R   $��Center=�6c   $��58��6~   $ 0.257,0&�6�   �� 5   
   $��Disabled=�6   ��. $��2��6X   .��5      &   System.Windows.Controls.DataGrid  -   5        $��LogMessageGrid��5         CanUserSortColumns .     AutoGenerateColumns .     CanUserAddRows .  ��6I   �� *
VM_LogList+   Columns 5      0  *System.Windows.Controls.DataGridTextColumn  5       ,  &System.Windows.Controls.DataGridColumn	   Header$
	 Type��6)   
   Width5  /System.Windows.Controls.DataGridLengthConverter$

 Auto 67      
IsReadOnly .    FontSize$ 15Q�6v      
FontWeight$
 Bold"�6�   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6V   ��  �� Path$ Status�   ElementStyle 5!      �� 5"       �� 
TargetType" ��6$   .�5#   "   ��  � �� 6?   �� $ Status� �� 	Converter# [  5$      5&        5'      $	 Message��6)   $
 * 6:    . $ 15Q�6w    6V   �� $ Message�6w   5)      5*      5+      5-      