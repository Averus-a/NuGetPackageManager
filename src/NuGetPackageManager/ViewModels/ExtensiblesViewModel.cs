﻿namespace NuGetPackageManager.ViewModels
{
    using Catel;
    using Catel.MVVM;
    using NuGetPackageManager.Management;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Threading.Tasks;

    public class ExtensiblesViewModel : ViewModelBase
    {
        private readonly IExtensibleProjectManager _extensiblesManager;

        public ExtensiblesViewModel(IExtensibleProjectManager extensiblesManager)
        {
            Argument.IsNotNull(() => extensiblesManager);

            _extensiblesManager = extensiblesManager;

            Title = "Project extensions";
        }

        protected override Task InitializeAsync()
        {
            var registeredProjects = _extensiblesManager.GetAllExtensibleProjects();

            _extensiblesManager.RestoreStateFromConfig();

            ExtensiblesCollection = new ObservableCollection<CheckableUnit<IExtensibleProject>>(
                registeredProjects
                .Select(
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
            _extensiblesManager.PersistChanges();

            return base.SaveAsync();
        }
    }
}