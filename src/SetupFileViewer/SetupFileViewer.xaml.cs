using System;
using System.Windows;
using System.Windows.Controls;
using ACCSetupManager.ViewModels;

namespace ACCSetupManager.SetupFileViewer
{
  public partial class SetupFileViewer : UserControl
  {
    public static readonly DependencyProperty SetupProperty = DependencyProperty.Register("Setup",
      typeof(SetupViewModel),
      typeof(SetupFileViewer),
      new PropertyMetadata(default(SetupViewModel)));

    public SetupFileViewer()
    {
      this.InitializeComponent();
    }

    public SetupViewModel Setup
    {
      get => (SetupViewModel)this.GetValue(SetupProperty);
      set => this.SetValue(SetupProperty, value);
    }
  }
}