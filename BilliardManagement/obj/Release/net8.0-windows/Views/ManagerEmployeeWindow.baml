   M S B A M L   `   `   `  ���� M  IBilliardManagement, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null7 clr-namespace:BilliardManagementBilliardManagement  C&clr-namespace:BilliardManagement.ViewsBilliardManagement.Views  4    .BilliardManagement.Views.ManagerEmployeeWindow   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     2local clr-namespace:BilliardManagement     _ [Microsoft.Xaml.Behaviors, Version=1.1.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a3i+http://schemas.microsoft.com/xaml/behaviors  /uc&clr-namespace:BilliardManagement.Views  c _MaterialDesignThemes.MahApps, Version=1.0.0.0, Culture=neutral, PublicKeyToken=df2a72020bd7962a_ [MaterialDesignThemes.Wpf, Version=5.0.0.0, Culture=neutral, PublicKeyToken=df2a72020bd7962aHmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes      MaterialDesignWindow#��[    � Title$  Manager Employee��$	��450��$	��800��  MaterialDesignBody#���  $��Regular"�$��13(� � WindowStartupLocation$ CenterScreen=�&   System.Windows.Media.TextOptions   TextFormattingMode$ Ideal=�   TextRenderingMode$
 Auto=�  MaterialDesignPaper#���    MaterialDesignFont#���    
EmployeeVM � DataContext# [ .��� .+��  � RowDefinitions �� $
��auto ��� $
��auto ��� $
��auto ��� .+�� $��2�� � ColumnDefinitions �� �� .+�� $��0��� $��1�� �� �� .+�� $��0��� $��1��.+��� -  'MaterialDesignThemes.Wpf.ComboBoxAssist   	MaxLength$	 200��)  #MaterialDesignThemes.Wpf.HintAssist   Hint$ Search��	   HintOpacity$		 .26Q�
 �� 
IsEditable$

 True�� �� Text �� *SearchTable+ �� UpdateSourceTrigger$ PropertyChanged=�#  MaterialDesignThemes.Wpf.Card  $��0��$��5q�.��� $��0 0 0 10q�.+��� $��5 5 15 5q�$ UserName�� % !MaterialDesignFloatingHintTextBox#��[ ���� *
Username+$ PropertyChanged=�G� -    G�Name$ FloatingPasswordBox��$��5 5 15 5q�$ Password�� ) %MaterialDesignFloatingHintPasswordBox#��[ *  $Microsoft.Xaml.Behaviors.Interaction   Triggers +  %Microsoft.Xaml.Behaviors.EventTrigger     	EventName$ PasswordChanged��*  $Microsoft.Xaml.Behaviors.TriggerBase   Actions. 2  ,Microsoft.Xaml.Behaviors.InvokeCommandAction     Command �� *PasswordChangedCommand+$ PropertyChanged=�   CommandParameter ��  �� ElementName$ FloatingPasswordBox��$ PropertyChanged=��� $��5 5 15 5q�$ 	Full Name��#��[ ���� *
FullName+$ PropertyChanged=��� $��5 5 15 5q�$ PhoneNumber��#��[ ���� *PhoneNumber+$ PropertyChanged=��� $��5 5 15 5q�$ Email��#��[ ���� *Email+$ PropertyChanged=��� $��5 5 15 5q�$
 Role�� & "MaterialDesignFloatingHintComboBox#��[ ���� *Roles+ �� SelectedItem �� *TestingRole+ �� Mode$ TwoWay=�$ PropertyChanged=�  $��1��$��5q�.��� .+��� $	��Add��$��5q�$	��100������ *
AddCommand+�� $
��Edit��$��5q�$	��100������ *EditCommand+�� $��Delete��$��5q�$	��100������ *DeleteCommand+  $��3��$��5q�$��Stretch=�.���� $
��Auto=�.���� $��Center=����� *List+ �� *SelectedItem+$ OneWayToSource=� �� View �� .P���  �� Width$
 auto��$��Id�� �� DisplayMemberBinding �� *
EmployeeId+�� $
 auto��$��UserName�� �� *
Username+�� $	 120��$��FullName�� �� *
FullName+�� $	 200��$��Phone Number�� �� *PhoneNumber+�� $
 auto��$��Email�� �� *Email+�� $
 auto��$
��Role�� �� *Role+