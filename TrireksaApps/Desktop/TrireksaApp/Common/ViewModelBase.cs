using FirstFloor.ModernUI.Presentation;
using ModelsShared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrireksaApp.Common
{
    public class ViewModelBase:BaseNotify
    {
        private bool _isActive;

        public ViewModelBase()
        {
            this.MainVM = Common.ResourcesBase.GetMainWindowViewModel();
            RefreshCommand = new CommandHandler { CanExecuteAction = x => true, ExecuteAction = RefreshAction };
        }

        protected virtual void RefreshAction(object obj) { }

        public MainWindowVM MainVM { get; private set; }
        public CommandHandler RefreshCommand { get; }

        public bool ProgressIsActive
        {
            get { return _isActive; }
            set
            {
             SetProperty(ref   _isActive , value);
            }
        }
    }
}
