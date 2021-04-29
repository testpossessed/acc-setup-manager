using System;
using System.Windows;
using System.Windows.Controls;
using ACCSetupManager.ViewModels;

namespace ACCSetupManager
{
  public partial class SetupFileComparisonViewer : UserControl
  {
    public static readonly DependencyProperty SetupProperty = DependencyProperty.Register("Setup",
      typeof(SetupViewModel),
      typeof(SetupFileComparisonViewer),
      new PropertyMetadata(default(SetupViewModel)));

    public SetupFileComparisonViewer()
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