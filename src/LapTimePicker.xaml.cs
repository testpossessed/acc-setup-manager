using System;
using System.Windows;
using System.Windows.Controls;

namespace ACCSetupManager
{
  public partial class LapTimePicker : UserControl
  {
   
    public static readonly DependencyProperty MinutesProperty = DependencyProperty.Register(
      "Minutes",
      typeof(int),
      typeof(LapTimePicker),
      new PropertyMetadata(default(int)));

    public static readonly DependencyProperty SecondsProperty = DependencyProperty.Register(
      "Seconds",
      typeof(double),
      typeof(LapTimePicker),
      new PropertyMetadata(default(double)));

    public LapTimePicker()
    {
      this.InitializeComponent();
    }

    public int Minutes
    {
      get => (int)this.GetValue(MinutesProperty);
      set => this.SetValue(MinutesProperty, value);
    }

    public double Seconds
    {
      get => (double)this.GetValue(SecondsProperty);
      set => this.SetValue(SecondsProperty, value);
    }
  }
}