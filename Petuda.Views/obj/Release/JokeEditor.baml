   M S B A M L   `   `   `  ���� O  Kmscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e0891&clr-namespace:System;assembly=mscorlibSystem  G CPetuda.Views, Version=1.0.0.2, Culture=neutral, PublicKeyToken=null?$clr-namespace:Petuda.Views.ResourcesPetuda.Views.Resources    Petuda.Views.JokeEditor   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     9system&clr-namespace:System;assembly=mscorlib     5resx$clr-namespace:Petuda.Views.Resources        JokeEditorStyle#��[    � WindowStartupLocation$  CenterScreen=� � Title �� *
EditMode+  jokeEditorTitleConverter �� 	Converter# [  � 	Resources   JokeTextWidth  JokeTextRowHeight��%1   &	       &	      X�	600.0Q��30.0 �.��� .+��� -    ��Name$ WindowBorder��  PopupBorderRectangleStyle#��[ ]� $
��0,10q���.  .+��� $	��Top=�  LabelHeaderStyle#��[ $��Center=����� *
EditMode+# [ � $
��20,0q�$	��Top=�$	��Top=� � RowDefinitions �� $
��Auto ��� #��[ .+���  �� Orientation$ 
Horizontal=�$  Petuda.Views.Resources.Strings   RequiredToolTip � ToolTip# Z .+��� $
��Left=�$��Center=�	   Required#��Z	 
���  ��$��15(�$��0��$��	-10,0,0,0q�y��� *NameIsNotValid+  hiddenVisibilityConverter# [ �� 
   	NameTitle#��Z
 $��0��$
��Left=�$��Bottom=�  LabelSubHeaderStyle#��[ $��	-6,0,0,-5q�� $��1��.+��� -    ��Name$ textBoxName��#��[   InputControlBasicStyle#��[ $
��Left=����� *Name+ �� Mode$ TwoWay=� �� UpdateSourceTrigger$ PropertyChanged=�� $
��20,0q�$	��Top=�$	��Top=� �� $
��Auto ��� #��[ .+���    
ThemeTitle#��Z $��0��$
��Left=�$��Bottom=�#��[ $��	-6,0,0,-5q��� -    ��Name$ comboBoxTheme��$��1��$
��Left=�#��[  �� 
IsEditable . #��[  �� SelectedItem �� *Theme+$ TwoWay=�$ PropertyChanged=� �� Text �� *Theme+$ TwoWay=�$ PropertyChanged=����� *Themes+� $
��20,0q�$	��Top=�$	��Top=� �� $
��Auto ��� $��* �.+���    Text#��Z $��0��$
��Left=�$	��Top=�#��[ $��	-6,0,0,-5q��� -   $ textBoxText��$��1��$
��Left=�#��[ " �� VerticalScrollBarVisibility$
 Auto=�$	��410�� �� TextWrapping$
 Wrap=� �� AcceptsReturn . #��[ $	��Top=����� *Text+$ TwoWay=�� $��
20,10,20,5q�$��60��$	��Top=�$	��Top=� � ColumnDefinitions �� $
��Auto ��� $��* ��� $
��Auto �.+���    Tags#��Z $��0��$��Right=�$	��Top=�#��[ $��0,-5,0,0q� _� .����  	 TextBlockToolTipStyle#��[	    TagsToolTip#��Z �� -   $ textBoxTags��$��1��$��Stretch=�$
 Wrap=� . #��[ $	��Top=����� *Tags+$ TwoWay=�.�� _� .���� #��[	 #��Z .���� $ 
Horizontal=�$��2��.+��� -    ��Name$ buttonOk�� 
 PetudaButtonStyle#��[
 $��Bottom=�   Ready
 � Tag# Z $
��10,0q����� *SaveJokeCommand+ _� .���� #��[	    SaveJokeToolTip#��Z ���� $��/Images/checkmark.png��$��40��$��40���� -   $ buttonCancel��#��[
 $��Bottom=�   Cancel# Z $��10,0,0,0q����� $��/Images/block.png��$��40��$��40��