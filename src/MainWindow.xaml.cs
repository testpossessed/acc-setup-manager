using System;
using Syncfusion.SfSkinManager;
using Syncfusion.Windows.Shared;

namespace ACCSetupManager
{
  public partial class MainWindow : ChromelessWindow
  {
    public MainWindow()
    {
      SfSkinManager.SetTheme(this, new Theme("Blend"));

      this.InitializeComponent();
    }
  }
}