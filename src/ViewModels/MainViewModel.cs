using System;
using ACCSetupManager.Abstractions;
using Microsoft.Toolkit.Mvvm.ComponentModel;

namespace ACCSetupManager.ViewModels
{
    public class MainViewModel: ObservableObject
    {
        private readonly ISetupFileProvider setupFileProvider;
        private string statusMessage = "Started";

        public MainViewModel(ISetupFileProvider setupFileProvider)
        {
            this.setupFileProvider = setupFileProvider;
        }

        public string StatusMessage
        {
            get => this.statusMessage;
            set => this.SetProperty(ref this.statusMessage, value);
        }
    }
}
