   M S B A M L   `   `   `  ����
  Duties    Duties.View.AddDuty   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      .��5      �   � ColumnDefinitions  5   
   �� 5      $��* �6   �� 5      $��* �6   �� 5	      $��* �6    � RowDefinitions 5   
   �� 5      $��25 �6   �� 5      $��25 �6   �� 5      $
��auto �6   .+�5   
   �� $��0��6   $��3��6$   $��0��68   $��	10,10,0,0q�6E   $��Dodaj dyżur��6X   �� 5   
   $��0��6   $��1��6$   $��	10,10,0,0q�61   $
��Wrap=�6D   $��Dzień:��6X   �� 5   
   $��1��6   $��1��6$   $��	10,10,0,0q�61   $
��Wrap=�6D   $��Godzina od:��6X   �� 5   
   $��2��6   $��1��6$   $��	10,10,0,0q�61   $
��Wrap=�6D   $��Godzina do:��6X   �� 5   
   $��0��6   $��2��6#   $��	10,10,0,0q�60   ��6C   ��  �� Path$ 	DaySource� �� ValidatesOnDataErrors .  �� ValidatesOnExceptions .  �� UpdateSourceTrigger$ PropertyChanged=� �� SelectedValue 6�   �� *SelectedDay+�� 5      $��1��6   $��2��6*   $��	10,10,0,0q�67    �� TextWrapping$
 Wrap=�6J   ��6^   �� $ HourFrom� .  . $ PropertyChanged=��� 5   
   $��2��6   $��2��6"   $��	10,10,0,0q�6/   $
 Wrap=�6B   ��6V   �� $ HourTo� .  . $ PropertyChanged=�5      5      