   M S B A M L   `   `   `  ����H  DPuzzleCreator, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null-clr-namespace:PuzzleCreatorPuzzleCreator  5   	   $    PuzzleCreator.ServerConnection   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35U QWPFToolkit, Version=3.5.40128.1, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   5puzzleCreatorclr-namespace:PuzzleCreator     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     6;   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	     � Title$  ServerConnection��5   	   $	��260��6"   $	��350��6/   .��5      � $��0,0,2,1q�6   .+�5   
   � .+�5	      �� -   5        $��	Connected��5	      $��10(�6%   $
��Left=�63   $��
28,155,0,0q�6N   $	��Top=�6b   $	��290��6z   $��23��6�   5
      �� 5   
   $��30��6   $��15(�6   $��
Ip-Address��6*   $
��Left=�6?   $��	19,72,0,0q�6Z   $	��Top=�6m   �� 5   
   -   5        $��
textB_Host��5      $��15(�6$   $��30��62   $
��Left=�6>   $��
123,72,0,0q�6Y    �� TextWrapping$
 Wrap=�6m   $	��Top=�6�   $	��198��6�   ��6�   �� *Host+�� 5   
   -   5        $��15(�5      $��30��6    $��3q�6,   $��Connect��6@   $��Center=�6R   $��177,0,19,10q�6o   $��Bottom=�6�   $	��144��6�   ��5      �� 5       �� 
TargetType" ��6   .�5      ��  �� Property � {� �� Value .  62    �� Triggers 5      �� 5       �� Binding 6&   ��  �� ElementName$ 
textB_Port�� �� Path$ (Validation.HasError)�	 �� Value		 False6m   .a�5      ��  � {� . 6:   5      5      5      �� 5   
   -   5        $��15(�5      $��30��6    $��3q�6,   $
��Stop��6@   $��Center=�6O   $��27,0,169,10q�6l   $��Bottom=�6�   $	��144��6�   6�   �� 5   
   -   5        $��
textB_Port��5      $��15(�6$   $
��Left=�62   $��30��6M   $��123,117,0,0q�6Y   $
 Wrap=�6n   $	��Top=�6�   $	��197��6�   .��6�   ��5      �� 5      $
 Port�6   
 �� UpdateSourceTrigger$
 PropertyChanged=�6&    �� ValidationRules 5      5  /System.Windows.Controls.ExceptionValidationRule  5      5       .��5!      5"      �� 5$   
   $��30��6   $��15(�6   $
��Port��6*   $
��Left=�69   $��
19,114,0,0q�6T   $	��Top=�6h   �� 5%   
   $��Connect to Game Server��6   $��25(�61   $��Center=�6?   $��6,6,6,0q�6\   $	��Top=�6m    J� RenderTransformOrigin$ -0.261,0.009&�6�   5'      5(      