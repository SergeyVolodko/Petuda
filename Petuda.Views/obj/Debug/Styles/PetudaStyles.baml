   M S B A M L   `   `   `  ����G  CPetuda.Views, Version=1.0.0.2, Culture=neutral, PublicKeyToken=nullA%clr-namespace:Petuda.Views.ConvertersPetuda.Views.Converters  5      O Kmscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e0891&clr-namespace:System;assembly=mscorlibSystem 5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e0898    2Petuda.Views.Converters.DoubleMinusDoubleConverter   minusConverter7   1Petuda.Views.Converters.DoublePlusDoubleConverter  plusConverter1   +Petuda.Views.Converters.MultiMinusConverter  multiMinusConverterB   <Petuda.Views.Converters.ConcatenateFieldsMultiValueConverter  concatConverter:   4Petuda.Views.Converters.BooleanToVisibilityConverter  visibilityConverter@   :Petuda.Views.Converters.BooleanToHiddenVisibilityConverter  hiddenVisibilityConverter0   *Petuda.Views.Converters.IsNotNullConverter  isNotNullConverter3   -Petuda.Views.Converters.IsNotNullToVisibility # isNotNullToVisiblilityConverter0   *Petuda.Views.Converters.IsNullToVisibility   isNullToVisiblilityConverterD	   >Petuda.Views.Converters.NegationOfBooleanToVisibilityConverter 	 negVisibilityConverterD
   >Petuda.Views.Converters.BooleanToTopVeritcalAlligmentConverter 
 topAllignConverter@   :Petuda.Views.Converters.BooleanToScriptMenuHeightConverter  menuHeightConverterA   ;Petuda.Views.Converters.BooleanToScriptEditorTitleConverter  scriptEditorTitleConverter?   9Petuda.Views.Converters.BooleanToJokeEditorTitleConverter  jokeEditorTitleConverter  BorderColorBrush  FillColorBrush  AccentColorBrush  BasicTextForeground  BasicTextFontSize  BasicTextFontFamily  SubHeaderTextFontSize  FilterInputControlHeight  HeaderTextFontSize  ButtonTextFontSize  MessageContentTextFontSize  SumOfMarginsOnTheMainPage  JokeEditorWidth  PetudaMaxRowHeight  popupLabelLeftMargin  PetudaToolTipWidth  menuMovementDuration  menuMovementTimeSpan   TextBlockToolTipStyle  �� 
TargetType �� Property �� Value ! BasicTextStyle " BasicTextBlockStyle # ButtonTextBlockStyle �� BasedOn  $ MessageContentTextBlockStyle % LabelHeaderStyle & LabelSubHeaderStyle ' TextBlockSubHeaderStyle ( LabelCloseStyle ) StartLinkTextBlockStyle �� 	Resources * Key g� 	BeginTime �� TargetProperty	 W� To g� Duration	 �� Triggers
 P� Property P� Value N� ExitActions + MouseOff N� EnterActions , MouseOn :� RoutedEvent - LinkPressed . HyperLinkTextBlockStyle / PetudaDataGridStyle&   System.Windows.Controls.DataGrid 0 DataGridColumnHeaderStyle=  7System.Windows.Controls.Primitives.DataGridColumnHeader 1 PetudaCellStyle*  $System.Windows.Controls.DataGridCell 2 InputControlBasicStyle  3 FilterInputControlBasicStyle 4 FilterCheckBoxStyle 5 MenuItemStyle 6 FilterBorderStyle 7 PopupBorderRectangleStyle 8 StartPageBorderStyle 9 MainWindowStyle : PopupWindowStyle ; StartPageStyle < JokeEditorStyle = MessageWindowStyle > ScriptEditorStyle ? UnderlineDatagridHeader  @ GenerateWorDocumentMenuStyle  A DraggingJokePopupBorderStyle #B DraggingJokePopupTextBlockStyle�� 5      D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    :x,http://schemas.microsoft.com/winfx/2006/xaml     5      4
converters%clr-namespace:Petuda.Views.Converters  5      1system&clr-namespace:System;assembly=mscorlib  5      %h#  &	        &	      &	 &     &	 9     &	 L     &	 _     &	 r     &	 �     &	 �     &		 �     &	
 �     &	 �     &	 �     &	 �     &	 
    &	 +    &	 H    &	 i    &	 �    &	 �    &	 �    &	 �    &	 �    &	     &	 *    &	 D    &	 ^    &	 {    &	 �    &	 �    &	 �    &	 �    &	      7  &	! x    7  7  7  &	" G    7  7  7  &	#     7 " 7  &	$ h    7 " 7  &	% �    7 ! 7  7  &	&     7 ! 7  &	' �    7 " 7  &	(     7 % &	) �    7 " 7  7 + 7 , 7 - &	. �    7 ) 7  7  &	/     7  &	0 �    7 ! 7  &	1 8    7 ! &	2 �    7 ! &	3 *    7 ! 7  &	4 �    7 ! 7  &	5 �    7  &	6 a    7  &	7 �    7  7  &	8 �    7  7  &	9 �    &	: �    &	; �    7 : &	< �    7 : 7  &	= k    7 : &	>     7 : &	? �    7  &	@ R    7  7  &	A ^    7  7  &	B C       5      6,     5      6+     5	      6%     5
      66     5      6.     5      64     5      6$     5      6'     5      6$   	  5      68   
  5      68     5      64     5      65     5      63   ��5      	#FFFC4F4F��6:   ��5      	White��64   ��5      	#FF0C97BF��6:   ��5      	Black��69   X�5      14Q�62   *�5      
"Sagoe UI")�69   X�5      16Q�66   X�5      24Q�69   X�5      18Q�63   X�5       12Q�63   X�5!      14Q�6;   X�5"      40Q�6:   X�5$      	640.0Q�63   X�5&      90.0Q�65   �5'      95.0 �64   X�5(      	300.0Q�66   E�5*      0:0:0.5D�65   i�5+      0:0:0.5h�6<   �� 5-      "  ��6*   .�5.   
   ��  � ��8   6"   �� 5/   
    � ��$
 Wrap=�6)   50      �� 52      "  ��6#   .�53   
   ��  � ��8   6'   �� 54   
    � ��8  6%   �� 55   
    � ��8  6'   �� 56   
    � ��
 ���� 6'   �� 57   
    � ��$	 0,0q�6#   58      �� 5:      "  ��6(   .�5;   
   ��  � ��8   6'   �� 5<   
    � ��8  6%   �� 5=   
    � ��8  6'   �� 5>   
    � ��
 ���� 6'   �� 5?   
    � ��$ WordEllipsis=�6)   5@      �� 5B      "  ��6)   8   .�5C   
   ��  � ��8  6%   5D      �� 5E      "  ��61   8   .�5F   
   ��  � ��8  6%   5G      �� 5I      8   "  ��6O   .�5J   
   ��  � ��8  6%   �� 5K   
    � ��8  6'   5L      �� 5N      8   "  ��6R   .�5O   
   ��  � ��8  6%   �� 5P   
    � ��
 ���� 6'   5Q      �� 5S      8   "  ��6[   .�5T   
   ��  � ��8  6%   �� 5U   
    � ��
 ���� 6'   �� 5V   
    � ��$
 Wrap=�6)   5W      �� 5Y      8   "  ��6P   .�5Z   
   ��  � ��$ Right=�60   �� 5[   
    � ��$	 Top=�6.   �� 5\   
    � ��$ 0,0,5,0q�6#   �� 5]   
    �@�Cursor$
 Hand��6#   5^      �� 5`      8   "  ��6[   .�5a   
   ��  � ��$ Center=�60   �� 5b   
    � ��$ Bottom=�6.   �� 5c   
    � ��$ 0,0,0,20q�6#   �� 5d   
    � ��$ 40(�6%   �� 5e   
    � ��8  6'   �� 5f   
    � ��$ 	underline{�6,   �� 5g   
    �@�Cursor$
 Hand��6#    5h   
   ���� 5i      MouseOn* .��5j      W� $ 00:00:00X�6"   $V P(UIElement.RenderTransform).(TransformGroup.Children)[0].(ScaleTransform.ScaleX)�5k   !   $
 1.03X�5l   !   $ 00:00:00.1000000D�5m   !   W� 5p      $ 00:00:00X�6"   $V P(UIElement.RenderTransform).(TransformGroup.Children)[0].(ScaleTransform.ScaleY)�5q   !   $
 1.03X�5r   !   $ 00:00:00.1000000D�5s   !   5u      �� 5w      MouseOff* .��5x      W� $ 00:00:00X�6"   $V P(UIElement.RenderTransform).(TransformGroup.Children)[0].(ScaleTransform.ScaleX)�5y   !   $ 00:00:00.1600000D�5z   !   W� 5|      $ 00:00:00X�6"   $ 00:00:00.1600000D�5}   !   $V P(UIElement.RenderTransform).(TransformGroup.Children)[0].(ScaleTransform.ScaleY)�5~   !   5�      �� 5�      LinkPressed* .��5�      W� $ 00:00:00X�6"   $V P(UIElement.RenderTransform).(TransformGroup.Children)[0].(ScaleTransform.ScaleX)�5�   !   $ 00:00:01.00000D�5�   !   $	 1.0X�5�   !   W� 5�      $ 00:00:00X�6"   $V P(UIElement.RenderTransform).(TransformGroup.Children)[0].(ScaleTransform.ScaleY)�5�   !   $ 00:00:01.00000D�5�   !   $	 1.0X�5�   !   5�      5�      	 5�   
   P� 5�      
 �@J�IsMouseOver .  5�      �� 5�      8�� 6&    5�      �� 5�      8�� 6&   5�      :� 5�      $ MouseLeftButtonDown��6   .[�5�      �� 8�� 6"   5�      5�      �� 5�      8   "  ��6_   .�5�   
   ��  � ��8  6%   �� 5�   
    � ��8  6'   5�      �� 5�      "   6(   .�5�   
   ��  �@ SelectionMode$ Single=�6*   �� 5�   
    �@ CanUserDeleteRows .  6.   �� 5�   
    �@ CanUserResizeRows .  6.   �� 5�   
    �@ CanUserAddRows .  6+   �� 5�   
    �@ CanUserResizeColumns .  61   �� 5�   
    �@ AutoGenerateColumns .  60   �� 5�   
    �@ 
IsReadOnly . 6'   �� 5�   
    �@J�	AllowDrop . 6&   �� 5�   
    � ��
 ���� 6'   �� 5�   
    � ��
 ���� 6(   �� 5�   
     �@ HorizontalGridLinesBrush8   65   �� 5�   
    �@ GridLinesVisibility$ 
Horizontal=�60   �� 5�   
    �@ HeadersVisibility$ Column=�6.   5�      �� 5�      8   "   6X   .�5�   
   ��  � ��8  6%   �� 5�   
    � ��$ 45��6#   5�      �� 5�      8   "   6N   .�5�   
   ��  � ��$ Center=�65   �� 5�   
    � ��
 ���� 6(   �� 5�   
    � ��$ DemiBold"�6'   5�      �� 5�      8   "  ��6U   .�5�   
   ��  � ��$ Center=�65   5�      �� 5�      8   "  ��6[   .�5�   
   ��  � ��$ Center=�65   �� 5�   
    � ��8  6#   �� 5�   
    � ��$ 0,3,5,5q�6#   �� 5�   
    � ��$ Center=�6.   5�      �� 5�      8   "  ��6R   .�5�   
   ��  � ��8  6(   �� 5�   
    � ��
 ���� 6'   �� 5�   
    � ��$ 5q�6#   �� 5�   
    � ��$ Center=�6.   5�      �� 5�      "  m�6"   .�5�   
   ��  � ��8   6'   �� 5�   
    �@�Cursor$
 Hand��6#   �� 5�   
    � ��$	 350��6"   �� 5�   
    � ��$	 0,5q�6#   5�      �� 5�      "  ��6&   .�5�   
   ��  � ��8   6(   �� 5�   
    � ��$ 1q�6,   �� 5�   
    �@��CornerRadius$ 5��6)   5�      �� 5�      "  ��6.   .�5�   
   ��  � ��8   6#   �� 5�   
    � ��$ 1��6,   �� 5�   
    � ��8  6!   �� 5�   
    �@��RadiusX$ 5��6$   �� 5�   
    �@��RadiusY$ 5��6$   5�      �� 5�      "  ��6)   .�5�   
   ��  � ��8   6(   �� 5�   
    � ��$ 2q�6,   �� 5�   
    � ��8  6'   �� 5�   
    �@��CornerRadius$ 7��6)   5�      �� 5�      "  �6$   .�5�   
   ��  � ��$	 600��6#   �� 5�   
    � ��$
 1000��6%   �� 5�   
    � ��$	 600��6&   �� 5�   
    �@J�	AllowDrop . 6&   �� 5�   
    �@�Icon$ ../Images/Application.ico��6!   �� 5�   
    �@�WindowState$ 	Maximized=�6(   5�      �� 5�      "  �6%   .�5�   
   ��  �@�WindowStyle$
 None=�6(   �� 5�   
    �@�
ResizeMode$ NoResize=�6'   �� 5�   
    �@�AllowsTransparency . 6/   �� 5�   
    � �� 6'   Z� 5�      �� 5      "  �6#   8   .�5  
   ��  �@�Icon$ ../Images/Application.ico��6!   �� 5  
    � ��$	 800��6%   �� 5  
    � ��$	 500��6&   �� 5  
    �@�
ResizeMode$ NoResize=�6'   �� 5  
    �@�
ResizeMode$ NoResize=�6'   5     �� 5     "  �6$   8   .�5	  
   ��  � ��$	 680��6#   �� 5
  
    � ��8  6"   �� 5  
    �@�Icon$ ../Images/edit.png��6!   5     �� 5     "  �6'   8   .�5  
   ��  � ��$	 220��6#   �� 5  
    � ��$	 520��6"   �� 5  
    �@�Icon$ ../Images/warning.png��6!   5     �� 5     "  �6&   8   .�5  
   ��  � ��$	 260��6#   �� 5  
    � ��$	 640��6"   �� 5  
    �@�Icon$ ../Images/edit.png��6!   5     �� 5     "  ��6-   .�5  
   ��  � ��$ 2��6#   �� 5  
    � ��$	 Top=�6.   �� 5  
    � ��8   6!   5     �� 5!     "  o�61   .�5"  
   ��  �@�Cursor$
 Hand��6#   �� 5#  
    � ��$ Stretch=�60   �� 5$  
    � ��$ Center=�6.   �� 5%  
    � ��$ 50,0,0,0q�6#   �� 5&  
    � ��8   6'   �� 5'  
    � ��8  6'   5(     �� 5+     "  ��61   .�5,  
   ��  � ��8   6'   �� 5-  
    � ��8  6(   �� 5.  
    � ��$ 1q�6,   �� 5/  
    �@��CornerRadius$ 10��6)   �� 50  
    �@J�Opacity$	 0.9Q�6$   51     �� 53     "  ��64   .�54  
   ��  � ��$	 200��6"   �� 55  
    � ��$ 20��6#   �� 56  
    � ��$ 12(�6%   �� 57  
    � ��$ Center=�6.   �� 58  
    � ��$
 Wrap=�6)   59     5;     