   M S B A M L   `   `   `  ����E  ATachograph, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null'clr-namespace:Tachograph
Tachograph  5          Tachograph.SettingsPage   5      W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      *localclr-namespace:Tachograph     5        N� 
Background
  �����.��5
      � -   5        $��settingGrid��5
       � ColumnDefinitions 5   
   �� 5      �� 5      �� 5      $��2* �6    � RowDefinitions 5   
   �� 5      �� 5      $��50 �6   .+�5   
   �� $��2��6   $��1��6%    �� Orientation$ 
Horizontal=�62   $��Center=�6K   $��0,0,0,10q�6h   .+�5      �� -   5        $��pressActiveSignalsBtn��5   C   $	��140��6   $ ��Označit aktivní signály��6!   6`   �� 5      -   5        $��pressBreakSignalsBtn��5   S   $	��140��6   $��Označit brzdné signály��6!   $��5,0,0,0q�6B   6o   �� 5      -   5        $��pressInverseSignalsBtn��5   U   $	��140��6   $!��Označit inverzní signály��6!   $��5,0,0,0q�6D   6s   5      �� 5   
   $	��155��6   $��Center=�6!   $	��Top=�6>   $��0��6V   $��0,30,0,0q�6f   .+�5      �� .+�5      �� $��Parametry tacho��6   �� 5       �� CornerRadius$ 6��6   
�������
�������$��2q�6T    �� Padding$ 8q�6h   ._�5      �� .+�5      �� .+�5       �� $��Průměr kola [mm]��6$   �� 5!      -   5        $��wheelDiameterTxtBox��5!   &   $	��700��6A   5"      �� 5#      .+�5$      �� $��Číslo vozu��6$   �� 5%      -   5        $��carNumberTxtBox��5%   /   $��1��6&   6/   5&      5'      5(      5)      �� 5*      .+�5+      �� $��Počítadla��6   �� 5,      $ 6��6   
�������
�������$��2q�6T   $ 8q�6h   ._�5-      �� .+�5.      �� .+�5/      �� $��Ujeto celkem [km]��6$   �� 50      -   5        $!��totalKilometersDrivenTxtBox��50   /   $��0��6&   6/   51      �� 52      .+�53      �� $��Počítadlo 1��6$   �� 54      -   5        $��counter1TxtBox��54   /   $��0��6&   6/   55      �� 56      .+�57      �� $��Počítadlo 2��6$   �� 58      -	   5        $��counter2TxtBox��58   /   $��0��6&   6/   59      �� 5:      .+�5;      �� $��Počítadlo 3��6$   �� 5<      -
   5        $��counter3TxtBox��5<   /   $��0��6&   6/   5=      �� 5>      .+�5?      �� $��Počítadlo 4��6$   �� 5@      -   5        $��counter4TxtBox��5@   /   $��0��6&   6/   5A      �� 5B      .+�5C      �� $��Počítadlo 5��6$   �� 5D      -   5        $��counter5TxtBox��5D   /   $��0��6&   6/   5E      5F      5G      5H      �� 5I      .+�5J      �� $��Módy��6   �� 5K      $ 6��6   
�������
�������$��2q�6T   $ 8q�6h   ._�5L      �� $ 
Horizontal=�6!   $��Center=�6:   .+�5M      	� -   5        $��modeRadioButtonBaseValue��5M   a   $��0��6&    d� 	IsChecked$
 TrueY�62   6a   	� 5N      -   5        $��1��5N   &   $��8,0,0,0q�62   6C   	� 5O      -   5        $��2��5O   &   $��8,0,0,0q�62   6C   	� 5P      -   5        $��3��5P   &   $��8,0,0,0q�62   6C   5Q      5R      5S      5T      �� 5U   
   $	��155��6   $��1��6!   $	��Top=�61   $��Center=�6I   $��0,30,0,0q�6f   .+�5V      �� .+�5W      �� $��Parametry vozu��6   �� 5X      $ 6��6   
�������
�������$��2q�6T   $ 8q�6h   ._�5Y      �� .+�5Z      �� .+�5[      �� $��Typ vozu��6$   �� 5\      -   5        $��carTypeTxtBox��5\   5   $��Default��6&   65   5]      �� 5^      .+�5_      �� $��Převodový poměr��6$   �� 5`      -   5        $��gearRatioTxtBox��5`   0   $��52��6&   60   5a      �� 5b      .+�5c      �� $��Max. průměr kola [mm]��6$   �� 5d      -   5        $��maxWheelDiameterTxtBox��5d   1   $	��700��6&   61   5e      �� 5f      .+�5g      �� $��Max. rychlost [km/h]��6$   �� 5h      -   5        $��maxSpeedTxtBox��5h   0   $��60��6&   60   5i      �� 5j      .+�5k      �� $��K faktor��6$   �� 5l      -   5        $��kFactorTxtBox��5l   3   $��10000��6&   63   5m      5n      5o      5p      �� 5q      .+�5r      �� $��Typ záznamu rychlosti��6   �� 5s      $ 6��6   
�������
�������$��2q�6T   $ 8q�6h   ._�5t      �� $ 
Horizontal=�6!   $��Center=�6:   .+�5u      	� -   5        $)��#speedRecordTypeRadioButtonBaseValue��5u   n   $	��PR.��6&   $
 TrueY�64   6n   	� 5v      -   5        $
��Min.��5v   &   $��8,0,0,0q�67   6H   	� 5w      -   5        $
��Max.��5w   &   $��8,0,0,0q�67   6H   5x      5y      5z      �� 5{      .+�5|      �� $��Typ tachografu��6   �� 5}      $ 6��6   
�������
�������$��2q�6T   $ 8q�6h   ._�5~      �� $��Center=�6"   .+�5      	� -   5        $
��TT32��5   &   65   	� 5�      -   5        $��TT32-24��5�   &   68   	� 5�      -   5        $��TT32-48��5�   &   68   	� 5�      -   5        $��TT12/22��5�   &   68   	� 5�      -   5        $(��"tachographTypeRadioButtonBaseValue��5�   n   $
��TT62��6&   $
 TrueY�65   6n   5�      5�      5�      �� 5�      .+�5�      �� $��Krok záznamu��6   �� 5�      -   5        $��recordStepComboBox��5�      .@�5�      ��  �� 
IsSelected$
 True��6#   $	��1 m��65   �� 5�      $	��2 m��6#   �� 5�      $	��3 m��6#   �� 5�      $	��4 m��6#   �� 5�      $	��5 m��6#   �� 5�      $	��6 m��6#   �� 5�      $	��7 m��6#   �� 5�      $	��8 m��6#   �� 5�      $	��9 m��6#   �� 5�      $
��10 m��6#   �� 5�      $��0,25 m��6#   5�      5�      5�      5�      5�      