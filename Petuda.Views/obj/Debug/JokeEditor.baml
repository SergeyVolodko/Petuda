   M S B A M L   `   `   `  ����O  Kmscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e0891&clr-namespace:System;assembly=mscorlibSystem  5   	   G CPetuda.Views, Version=1.0.0.2, Culture=neutral, PublicKeyToken=null?$clr-namespace:Petuda.Views.ResourcesPetuda.Views.Resources 5   	      Petuda.Views.JokeEditor   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   9system&clr-namespace:System;assembly=mscorlib     5   	   5resx$clr-namespace:Petuda.Views.Resources     5   	      JokeEditorStyle#��[  5   	     � WindowStartupLocation$  CenterScreen=�5   	    � Title 5   	   �� *
EditMode+  jokeEditorTitleConverter �� 	Converter# [  � 	Resources 5
        JokeTextWidth  JokeTextRowHeight��%M   &	       &	      X�5   
   	600.0Q�65   �5   
   30.0 �65   5      .��5      � .+�5   
   �� -   5         ��Name$ WindowBorder��5        PopupBorderRectangleStyle#��[ 5      5      ]� 5   
   $
��0,10q�6   ��.  .+�5      �� $	��Top=�6     LabelHeaderStyle#��[ 5      $��Center=�5      ��5      �� *
EditMode+# [ � 5      $
��20,0q�6   $	��Top=�5      $	��Top=�5       � RowDefinitions 5      �� 5       $
��Auto �6$   �� 5!      #��[ 6$   .+�5#      ��  �� Orientation$ 
Horizontal=�6   $  Petuda.Views.Resources.Strings   RequiredToolTip � ToolTip# Z 5$      .+�5%      �� $
��Left=�6    $��Center=�5&       	   Required#��Z	 5'       
���  ��$��15(�5)       $��0��5*       $��	-10,0,0,0q�5+       y�5,       �� *NameIsNotValid+  hiddenVisibilityConverter# [ �� 5.      
   	NameTitle#��Z
 6   $��0��5/      $
��Left=�50      $��Bottom=�51        LabelSubHeaderStyle#��[ 52      $��	-6,0,0,-5q�53      55      � 56      $��1��6   .+�57      �� -   5         ��Name$ textBoxName��57      #��[ 58        InputControlBasicStyle#��[ 5:      $
��Left=�5;      ��59      �� *Name+ �� Mode$ TwoWay=� �� UpdateSourceTrigger$ PropertyChanged=�5;      5=      5>      � 5A      $
��20,0q�6   $	��Top=�5B      $	��Top=�5C       5D      �� 5E      $
��Auto �6$   �� 5F      #��[ 6$   .+�5I      ��    
ThemeTitle#��Z 6   $��0��5J      $
��Left=�5K      $��Bottom=�5L      #��[ 5M      $��	-6,0,0,-5q�5N      �� 5P      -   5         ��Name$ comboBoxTheme��5P      $��1��5Q      $
��Left=�5R      #��[ 5U       �� 
IsEditable . #��[ 5X       �� SelectedItem 5S      �� *Theme+$ TwoWay=�$ PropertyChanged=� �� Text 5T      �� *Theme+$ TwoWay=�$ PropertyChanged=���5V      �� *Themes+5X      5Z      � 5\      $
��20,0q�6   $	��Top=�5]      $	��Top=�5^       5_      �� 5`      $
��Auto �6$   �� 5a      $��* �6$   .+�5d      ��    Text#��Z 6   $��0��5e      $
��Left=�5f      $	��Top=�5g      #��[ 5h      $��	-6,0,0,-5q�5i      �� 5l      -   5        $ textBoxText��5l      $��1��5m      $
��Left=�5n      #��[ 5o      " �� VerticalScrollBarVisibility$
 Auto=�5p      $	��410��5q       �� TextWrapping$
 Wrap=�5r       �� AcceptsReturn . #��[ 5t      $	��Top=�5u      ��5s      �� *Text+$ TwoWay=�5u      5w      � 5y      $��
20,10,20,5q�6   $��60��5z      $	��Top=�5{      $	��Top=�5|       � ColumnDefinitions 5~      �� 5      $
��Auto �6'   �� 5�      $��* �6'   �� 5�      $
��Auto �6'   .+�5�      ��    Tags#��Z 6   $��0��5�      $��Right=�5�      $	��Top=�5�      #��[ 5�      $��0,-5,0,0q�5�       5�      _� 5�      .��5�      ��  	 TextBlockToolTipStyle#��[	 6(      TagsToolTip#��Z 5�   $   5�      5�      �� 5�      -   5        $ textBoxTags��5�      $��1��5�      $��Stretch=�5�      $
 Wrap=�5�       . #��[ 5�      $	��Top=�5�      ��5�      �� *Tags+$ TwoWay=�.��5�       5�      _� 5�      .��5�      �� #��[	 6(   #��Z 5�   (   5�      .��5�   '   5�      �� 5�      $ 
Horizontal=�6   $��2��5�      .+�5�      �� -   5         ��Name$ buttonOk��5�       
 PetudaButtonStyle#��[
 5�      $��Bottom=�5�         Ready
 � Tag# Z 5�      $
��10,0q�5�      ��5�      �� *SaveJokeCommand+ 5�      _� 5�      .��5�   "   �� #��[	 6,      SaveJokeToolTip#��Z 5�   ,   5�      ��5�      �� 5�      $��/Images/checkmark.png��6$   $��40��6C   $��40��6N   5�      �� 5�      -   5        $ buttonCancel��5�      #��[
 5�      $��Bottom=�5�         Cancel# Z 5�      $��10,0,0,0q�5�      ��5�      �� 5�      $��/Images/block.png��6$   $��40��6?   $��40��6J   5�      5�      5�      5�      5�      5�      