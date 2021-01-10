using FirstFloor.ModernUI.Windows.Controls;
using ModelsShared;
using ModelsShared.Models;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Data;
using TrireksaApp.Common;
using System.Linq;
using System.Collections.Generic;

namespace TrireksaApp.Contents.Users
{
    public class ManageUserViewModel : BaseNotify
    {
        private bool _isShowData;
        private Userprofile _selected;
        private Roles _RoleSelected;
        private Roles _AddRoleSelected;

        public ObservableCollection<Roles> RoleSource { get; set; }

        public ManageUserViewModel()
        {
            AddNewRole = new CommandHandler { CanExecuteAction = AddNewRoleValidate, ExecuteAction = AddNewRoleAction };
            RemoveRole = new CommandHandler { CanExecuteAction = x => RoleSelected != null && SelectedItem!=null, ExecuteAction= RemoveRoleActio };
            CreateUserCommand = new CommandHandler { CanExecuteAction = x => true, ExecuteAction = CreateUserAction };
            RefreshCommand = new CommandHandler { CanExecuteAction = x => true, ExecuteAction = RefreshAction };
            this.MainVM= Common.ResourcesBase.GetMainWindowViewModel();
            RoleSource = new ObservableCollection<Roles>();
            RolesView = (CollectionView)CollectionViewSource.GetDefaultView(RoleSource);
            InitAsync();
            
        }

        private async void RefreshAction(object obj)
        {
           await MainVM.UserProfileCollections.Refresh();
        }

        private async void CreateUserAction(object obj)
        {
            var vm = new AddEditViewModel();
            var cnt = new AddEditUser
            {
                DataContext = vm
            };
            var dlg = new ModernDialog
            {
                Title = "Create New User",
                Content = cnt
            };
            dlg.Buttons = null;
            vm.WindowClose= dlg.Close;

            dlg.ShowDialog();

            if (vm.ModelValid)
            {

                RegisterModel model = 
                    new RegisterModel { Email=vm.Email, FullName=vm.FullName, Password=vm.Password, UserName=vm.UserName };
                var user  = await MainVM.UserProfileCollections.Register(model);
                if (user!=null)
                {
                  await  MainVM.UserProfileCollections.Refresh();
                    ModernDialog.ShowMessage("User Berhasil Dibuat !", "Message Dialog", System.Windows.MessageBoxButton.OK);
                }
            }
        }

        private async void RemoveRoleActio(object obj)
        {
          var removed = await MainVM.UserProfileCollections.RemoveRole(SelectedItem, RoleSelected);
            if(removed)
            {
                var userRole = SelectedItem.User.Userrole.SingleOrDefault(x => x.RoleId == RoleSelected.Id);
                SelectedItem.User.Userrole.Remove(userRole);
                RoleSource.Remove(RoleSelected);
                RolesView.Refresh();
                Roles.Refresh();
            }
        }

        private async void AddNewRoleAction(object obj)
        {
            var role = (Roles)obj;
            if(role!=null)
            {
                var roleSaved = await MainVM.UserProfileCollections.AddNewUserRole(SelectedItem, role);
                if(roleSaved!=null)
                {
                    SelectedItem.User.Userrole.Add(new Userrole { Role=role, RoleId=role.Id, User=SelectedItem.User, UserId=SelectedItem.UserId } );
                    RoleSource.Add(role);
                    RolesView.Refresh();
                    Roles.Refresh();
                }
               
            }
             
        }

        private bool AddNewRoleValidate(object obj)
        {
            if (SelectedItem != null)
                return true;
            return false;
        }

        public object UserLogin { get;  set; }
        public CommandHandler AddNewRole { get; }
        public CommandHandler RemoveRole { get; }
        public CommandHandler CreateUserCommand { get; }
        public CommandHandler RefreshCommand { get; }
        public MainWindowVM MainVM { get; }

        public bool IsShowData
        {
            get
            {
                return _isShowData;
            }
            set
            {
            SetProperty(ref    _isShowData , value);
            }
        }

        private List<Roles> roles;

        public CollectionView Roles { get;  set; }
        public CollectionView RolesView { get; }

        private async void InitAsync()
        {

            if(ResourcesBase.User.Roles.Where(x=>x.ToLower()=="administrator").Count()>0)
                IsShowData =true;
            roles= await MainVM.UserProfileCollections.GetRoles();
            if(roles!=null)
            {
                Roles = (CollectionView)CollectionViewSource.GetDefaultView(roles);
                Roles.Filter = RolesFilter;
                Roles.Refresh();
            }

        }

        private bool RolesFilter(object obj)
        {
            Roles role = (Roles)obj;
            if(role!=null && SelectedItem!=null && SelectedItem.User.Userrole!=null)
            {
                foreach(var item in SelectedItem.User.Userrole)
                {
                    if (role.Id == item.RoleId)
                        return false;
                }
            }

            return true;
        }

        public Userprofile SelectedItem
        {
            get { return _selected; }
            set
            {
                SetProperty(ref    _selected , value);
                ChangeRoleViews(_selected);
            }
        }

        public Roles AddRoleSelected
        {
            get
            {
                return _AddRoleSelected;
            }
            set
            {
                SetProperty(ref    _AddRoleSelected , value);
                if (_AddRoleSelected != null)
                    AddNewRole.Execute(_AddRoleSelected);
            }
        }

        public Roles RoleSelected
        {
            get { return _RoleSelected; }
            set
            {
                 SetProperty(ref    _RoleSelected , value);
            }
        }

        private void ChangeRoleViews(Userprofile selected)
        {
           if(selected!=null && selected.User!=null)
            {
                RoleSource.Clear();
                foreach(var item in selected.User.Userrole)
                {
                    RoleSource.Add(item.Role);
                }
                RolesView.Refresh();
                Roles.Refresh();
            }
        }
    }
}
