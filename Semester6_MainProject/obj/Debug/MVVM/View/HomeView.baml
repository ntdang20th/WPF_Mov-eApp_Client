   M S B A M L   `   `   `  ����P  LSemester6_MainProject, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullQ-clr-namespace:Semester6_MainProject.MVVM.ViewSemester6_MainProject.MVVM.View  5      [2clr-namespace:Semester6_MainProject.MVVM.ViewModel$Semester6_MainProject.MVVM.ViewModel  5	      Q-clr-namespace:Semester6_MainProject.MVVM.ViewSemester6_MainProject.MVVM.View  5      .    (Semester6_MainProject.MVVM.View.HomeView   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      ?local-clr-namespace:Semester6_MainProject.MVVM.View     5      c _MahApps.Metro.IconPacks.Core, Version=4.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137ag cMahApps.Metro.IconPacks.Material, Version=4.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137a?	iconPacks-http://metro.mahapps.com/winfx/xaml/iconpacks   5      _ [MaterialDesignThemes.Wpf, Version=4.3.0.3, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5      B	viewModel2clr-namespace:Semester6_MainProject.MVVM.ViewModel  5	      c _System.Windows.Interactivity, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35Bi:http://schemas.microsoft.com/expression/2010/interactivity  5
      8view-clr-namespace:Semester6_MainProject.MVVM.View  5      
�������  � DataContext  5      8  2Semester6_MainProject.MVVM.ViewModel.HomeViewModel  5   
    � 	Resources 5       �� 
TargetType 	  title �� Property �� Value��%�   &	        �� 5   
   " ��6   .�5      ��  � ��$ 26(�6)   �� 5       � ��
 �����6+   �� 5       � ��$
 Left=�64   �� 5       � ��$ Bottom=�62   �� 5       � ��$ 15 0 0 0q�6'   5      5      .��5      7  1MaterialDesignThemes.Wpf.Transitions.Transitioner   �� SelectedIndex$ 0��6"   !   AutoApplyTransitionOrigins . .@�5   
   �  � RowDefinitions 5    
   �� 5!      $��30 �6   �� 5"      $	��230 �6   �� 5#      $��30 �6   �� 5$      $	��300 �6   .+�5'   
   �� $��Mới cập nhật��6   $��0��6(   #��[  65   � 5)   
   $��10q�6   $��1��6   .+�5*      �� $��Disabled=�5,      $��Disabled=�5-      ��5*      �� *
ListNews+ �� SelectedItem 5+      �� *SelectedItem+	 �� Mode$	 OneWayToSource=���5.      �� 5/      .R�50      � 51      ��53      �� 54      .R�55      �� $	��160��6!   $	��260��6.   
�������$��1q�56   4   
 �� CornerRadius$
 5��57   !    �� Padding$ 5q�62   $��10q�6>   ._�58      �  � ColumnDefinitions 59   "   �� 5:   &   �� 5;   &   $	��120 �67   .+�5=   "   � $��1��6'   ��5>   &   �� 5?   *    �� ImageSource 65   �� *Anh+5A   #   � 5C   "   $��1��6'   $��25��67   $	��Top=�6C    5D   &   �� 5E   *   $��20 �6;   �� 5F   *   $��30 �6;   .+�5H   &   �� $
 12��6-   $��2��6?   $��50��5I   -   ���#f5f3f45L   '   '  !MaterialDesignThemes.Wpf.PackIcon  5M   &      Kind$
 Star=�6>   $��0��6J   $��20��5N   A   $��20��5O   A   $��Center=�5P   A   $��Center=�5Q   A   ���#fca3115R   A   �� 5S   &   $��1��5T   0   $��16(�5U   0   $
��Bold"�5V   0   $��Center=�5W   1   $��Center=�5X   1   
���   ���5S   0   �� *DiemSo+5Y   1   5Z   #   � 5\   "   $��0��6'    5]   &   �� 5^   *   �� 5_   *   $��30 �68   �� 5`   *   $��50 �68   .+�5b   &   �� 
�������$��10q�5d   0   $��14(�5e   0    �� TextAlignment$
 Left=�5f   0   $
��Wrap=�5g   0   $��Center=�5h   0   $
��Left=�5i   0   $��0��5j   0   ��5b   0   �� *	TenPhim+5j   0   �� 5k   &   $	��140��60   $��12(�5l   0   $��5q�5m   0   
�������$
 Left=�5o   0   $
��Wrap=�5p   0   $��Center=�5q   0   $
��Left=�5r   0   $��1��5s   0   �� 5t   *   $�Lượt xem: ��6.    �� 5u   *   �6.   �� *	LuotXem+5v   '   �� 5w   &   $	��140��60   $��12(�5x   0   
�������$
 Left=�5z   0   $
��Wrap=�5{   0   $	��Top=�5|   0   $
��Left=�5}   0   $��5q�5~   0   $��2��5   0   �� 5�   *   $�Bình luận: ��6.    �� 5�   *   �6.   �� *LuotBinhLuan+5�   '   5�   #   5�      5�      5�      .  (System.Windows.Interactivity.Interaction   Triggers 5�      /  )System.Windows.Interactivity.EventTrigger  5�         	EventName$ SelectionChanged��6)   .  (System.Windows.Interactivity.TriggerBase   Actions. 5�      6  0System.Windows.Interactivity.InvokeCommandAction     MoveNextCommand   Command# Z 64   5�      5�      5�      �� 5�   
   $��Đề xuất��6   $��2��5�      #��[  5�      � 5�   
   $��10q�6   $��3��6   .+�5�      �� $��Disabled=�5�      $
��Auto=�5�      ��5�      �� *
ListDeXuat+ 5�      �� *SelectedItem+$	 OneWayToSource=���5�      �� 5�      .R�5�      � 5�      ��5�      �� 5�      .R�5�      .   (Semester6_MainProject.MVVM.View.ItemView  5�       5�        5�      $ SelectionChanged��6)   . 5�        # Z 64   5�      5�      5�      5�      <	  6MaterialDesignThemes.Wpf.Transitions.TransitionerSlide	  5�      C
  =MaterialDesignThemes.Wpf.Transitions.TransitioningContentBase 
  OpeningEffects 5�   
   ;  5MaterialDesignThemes.Wpf.Transitions.TransitionEffect  5�         Kind$ FadeIn=�6.      Duration$ 0:0:1h�6<   .��5�   
   2   ,Semester6_MainProject.MVVM.View.ItemMainView  5�      5�      5�      