   M S B A M L   `   `   `  ����Q  MBoxingRoundIntervalWFP, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null?$clr-namespace:BoxingRoundIntervalWFPBoxingRoundIntervalWFP  5   	   '    !BoxingRoundIntervalWFP.MainWindow   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   6local$clr-namespace:BoxingRoundIntervalWFP     5   	     � Title$  BoxingRoundInterval��5   	   $	��450��6%   $	��800��62   
���Gc��.��5      � .+�5   
   �� $��48(�5      $��Center=�5      $��Center=�5      ��5      ��  �� Path$ CountdownText� �� Mode$ OneWay=�5      �� 5   
   $��48(�5      $��Center=�5      $��Center=�5      $��0,50,0,0q�5      ��5      �� $ 	FightText�$ OneWay=�5      �� 5   
   $��48(�5      $��Right=�5      $	��Top=�5      $��100,50,50,0q�5      ��5      �� $ 	RoundGame�$ OneWay=�5      �� 5   
   $��48(�5      $��Right=�5      $	��Top=�5      $��	0,50,20,0q�5       ��5      �� $ TotalRoundGame�$ OneWay=�5       �� 5"   
   $��48(�5#      $��Center=�5$      $��Center=�5%      $��	0,100,0,0q�5&      $y�Visible=�5'      ��5"      �� $ GameRestLength�$ OneWay=�5'      �� 5)   
   $��48(�5*      $��Center=�5+      $��0,50,0,0q�5,      $y�Visible=�5-      ��5)      �� $ TotalTrainingLengthMenu�$ OneWay=�5-      �� 5/   
   $��48(�50      $��Center=�51      $��	0,150,0,0q�52      ��5/      �� $ RoundLengthMenu�$ OneWay=�52      �� 54   
   -   5         ��Name$ subtractRoundLengthButton��54      $��-��65   $��24(�6A   $
��Bold"�6O   $��Right=�55      $	��Top=�6+   $��0,170,500,0q�56      $��50��6$   6/   �� 58   
   -   5        $ addRoundLengthButton��58      $��+��6/   $��24(�6;   $
��Bold"�6I   $��Right=�59      $	��Top=�6+   $��0,170,600,0q�5:      $��50��6$   6/   �� 5<   
   $��48(�5=      $��Center=�5>      $��	0,250,0,0q�5?      $y�Visible=�5@      ��5<      �� $ RestTimeLengthMenu�$ OneWay=�5@      �� 5B   
   -   5        $ subtractRestLengthButton��5B      $��-��63   $��24(�6?   $
��Bold"�6M   $��Right=�5C      $	��Top=�6+   $��0,265,500,0q�5D      $��50��6$   6/   �� 5F   
   -   5        $ addRestLengthButton��5F      $��+��6.   $��24(�6:   $
��Bold"�6H   $��Right=�5G      $	��Top=�6+   $��0,265,600,0q�5H      $��50��6$   6/   �� 5J   
   $��48(�5K      $��Center=�5L      $��	0,350,0,0q�5M      $y�Visible=�5N      ��5J      �� $ 
RoundsMenu�$ OneWay=�5N      �� 5P   
   -   5        $ subtractRoundsButton��5P      $��-��6/   $��24(�6;   $
��Bold"�6I   $��Right=�5Q      $	��Top=�6'   $��0,370,500,0q�5R      $��50��6    6+   �� 5T   
   -   5        $ addRoundsButton��5T      $��+��6*   $��24(�66   $
��Bold"�6D   $��Right=�5U      $	��Top=�6'   $��0,370,600,0q�5V      $��50��6    6+   �� 5X   
   -   5        $��Begin Training��5X      $��24(�6*   $
��Bold"�68   $��Center=�5Y      $	��Top=�6(   $��	0,450,0,0q�5Z      6   5[      5\      