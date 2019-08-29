﻿using Catel;
using Catel.MVVM;
using NuGetPackageManager.Management;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NuGetPackageManager.ViewModels
{
    public class ExtensiblesViewModel : ViewModelBase
    {
        private readonly IExtensibleProjectManager _extensiblesManager;

        public ExtensiblesViewModel(IExtensibleProjectManager extensiblesManager)
        {
            Argument.IsNotNull(() => extensiblesManager);

            _extensiblesManager = extensiblesManager;
        }

        protected override Task InitializeAsync()
        {
            ExtensiblesCollection = new ObservableCollection<CheckableUnit<IExtensibleProject>>(
                _extensiblesManager.GetAllExtensibleProjects().Select(
                    x => new CheckableUnit<IExtensibleProject>(_extensiblesManager.IsEnabled(x), x, ExtensibleProjectStatusChange))
                );
            return base.InitializeAsync();
        }

        public void ExtensibleProjectStatusChange(bool isShouldBeEnabled, IExtensibleProject project)
        {
            Argument.IsNotNull(() => project);

            if (isShouldBeEnabled)
            {
                _extensiblesManager.Enable(project);
            }
            else
            {
                _extensiblesManager.Disable(project); 
            }
        }

        public ObservableCollection<CheckableUnit<IExtensibleProject>> ExtensiblesCollection { get; set; }

        protected override Task<bool> SaveAsync()
        {
            //todo persist all changes in configuration file

            return base.SaveAsync();
        }
    }
}
