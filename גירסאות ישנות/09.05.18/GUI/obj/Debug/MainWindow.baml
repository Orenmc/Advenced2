   M S B A M L   `   `   `  ����>  :GUI, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullclr-namespace:GUIGUI  5   	       GUI.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   #localclr-namespace:GUI     5   	     � Title$!  Desktop App - Image Service��5   	   $	��600��6-   $	��800��6:    � 
ResizeMode$ CanMinimize=�6F    � Icon$ /Resources/Service.jpg��6_    � 	Resources 5	         FancyHeader �� Property �� Value��%�   &	        �� 5
      .�5      ��  � ��
 �����6*   �� 5       � ��
 �   �6*   �� 5       � ��$ 20(�6(   �� 5       � ��
 �   �6+   �� 5       � ��$ 1q�6/   5      5      .��5      � .+�5      �� $��3��6   $��0,0,-0.4,0.4q�6    J� IsHitTestVisible .  ��5      �� 5       �� 
TargetType" ��6   .�5      ��  � ��
 �����6$    �� Triggers 5      �� 5      	 �� Binding	 6   �� *VM_Connected+
 �� Value	
 False5      .a�5   	   ��  � ��
 �����6&   5   	   5      5       �� 5!      $��5,5,4.6,5.4q�6   $��3��6$   .@�5"      �� $��Settings��6   #��[  6   .��5#         GUI.SettingUserControl  5%      5&      �� 5'      $
��Logs��6   #��[  6   .��5(         GUI.LogerUserControl  5*      5+      5,      5-      5/      