   M S B A M L   `   `   `  ����E  ATachograph, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null'clr-namespace:Tachograph
Tachograph  5   	       Tachograph.MainWindow   5   	   W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   *localclr-namespace:Tachograph     5   	     � Title$/  )Nastavovací a čtecí program Tachografu��5   	    � 
ResizeMode$ NoResize=�68    � WindowStartupLocation$ CenterScreen=�6N   $	��800��6s   $
��1000��6�    � Icon 5	      �� 5
   
    �� 	UriSource$[ UC:\Users\david.manasek\Documents\projekty\csharp\Tachograph\Tachograph\tacho-icon.ico?�6   .��5      �  � RowDefinitions 5   
   �� 5      $��20 �6   �� 5      $��40 �6   �� 5      $��40 �6   �� 5      $��* �6   �� 5      $��40 �6    � ColumnDefinitions 5   
   �� 5      �� 5      $	��250 �6   .+�5   
   ]� $��0��6   $��3��6!   .+�5      o� $	��Top=�6   .@�5      m� $��_Soubor��6   .@�5      m� $��
_Otevřít��6   m� 5      $��_Uložit��6   m� 5      $��_Uložit jako��6   m� 5       $��	_Zavřít��6   5!      m� 5"      $��_Nastavení��6   .@�5#      m� $��
_Otevřít��6   5$      m� 5%      $��
_Nástroje��6   .@�5&      m� $��_Heslo��6   5'      m� 5(      $��_O aplikaci��6   .@�5)      m� $��
_Otevřít��6   5*      5+      5,      �� 5.   
   $��1��6   $��0��6"   $��Center=�63   $
��Left=�6N    �� Orientation$ 
Horizontal=�6i   $��5q�6�    � 	Resources 5/      	 �� 
TargetType
 �� Property �� Value��%w   '��       �� 50      "	 ��6   .�51      �� 
 � ��$	 166��6.   �� 52      
 � ��$ 30��6/   53      54      .+�55      �� -   5        $��setTimeAndDateBtn��55   4   $��Nastavit datum a čas��6   6M   �� 56      -   5        $��setCarParametersBtn��56   I   $��Nastavit parametry vozu��6   $��10,0,0,0q�67   6d   �� 57      -   5        $��setTaphoParametersBtn��57   O   $#��Nastavit parametry tachografu��6   $��10,0,0,0q�6=   6l   �� 58      -   5        $��setCountersBtn��58   D   $��Nastavit počítadla��6   $��10,0,0,0q�62   6Z   59      �� 5;   
    �� Stretch$
 Fill=�6   
�������	 �� X2$ 1��6,   $��1��63   $��2��6@   $	��Top=�6T   �� 5<   
   $
 Fill=�6   
�������$ 1��6,   $��1��63   $��2��6@   $��Bottom=�6U   �� 5>   
   $��1��6   $��2��6"   $��Center=�62   $��Center=�6M   $ 
Horizontal=�6j   $��5q�6�   .+�5?      �� -   5        $��readAndSaveButton��5?      $��Číst data a uložit��6.   $��30��6k   $	��105��6w   � 5@      -   5        $��progressBar��5@      $��5,0,0,0q�6-   $	��120��6>   $��20��6J   $y�Hidden=�6V   5A      �� 5B   
   $
 Fill=�6   
�������$ 1��6,   $��2��63   $��2��6@   $��Bottom=�6T   �� 5D   
   -   5        $��pagesSwitcher��5D   M   $��2��6   $��0��6"   $��Center=�62   $
��Left=�6b   $ 
Horizontal=�6}   $��5q�6�    5E      ��%�   'd�       �� 5F      "	 d�6   .�5G      �� 
 � ��$	 100��6.   �� 5H      
 � ��$ 30��6/   -   5        5J      5K      .+�5L      d� -	   5        $��settingsBtn��5L   /   $��
Nastavení��6   6B   d� 5M      -
   5        $��
signalsBtn��5M   -   $��Signály��6   6?   d� 5N      -   5        $��
commentBtn��5N   .   $��	Poznámka��6   6@   d� 5O      -   5        $��	editorBtn��5O   ,   $��Editor��6   6=   5P       � 5Q   
   -   5        $��
pagesFrame��5Q   A   $��3��6   $��0��6   $��2��6-     � NavigationUIVisibility$ Hidden=�6S   � 5R   
   $��4��6   $��2��6    5S      �� 5T      �� 5U      �� 5V      .+�5X      �� $��0��6   $ 
Horizontal=�6)   $��Center=�6B   $��Center=�6_   .+�5Y      �� $��	Poznámka��6   �� 5Z      $	��200��6   5[      �� 5\      $��1��6   $ 
Horizontal=�6)   $��Center=�6B   $��Center=�6_   .+�5]      �� $��Pracovní adresář��6   5_      �� 5`      $��2��6   $ 
Horizontal=�6)   $��Center=�6B   $��Center=�6_   .+�5a      �� $��Adresář signálů��6   5c      5d      �� 5e   
   $
 Fill=�6   
�������$ 1��6,   $��3��63   $��2��6@   $��Bottom=�6T   5f      5g      