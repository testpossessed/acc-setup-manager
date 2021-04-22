using System;
using System.Windows;
using System.Windows.Controls;
using ACCSetupManager.ViewModels;

namespace ACCSetupManager
{
  public partial class TyresView : UserControl
  {
    public static readonly DependencyProperty SetupFileProperty = DependencyProperty.Register("SetupFile",
      typeof(SetupFileViewModel),
      typeof(TyresView),
      new PropertyMetadata(default(SetupFileViewModel)));

    public TyresView()
    {
      this.InitializeComponent();
    }

    public SetupFileViewModel SetupFile
    {
      get => (SetupFileViewModel)this.GetValue(SetupFileProperty);
      set => this.SetValue(SetupFileProperty, value);
    }
  }
}