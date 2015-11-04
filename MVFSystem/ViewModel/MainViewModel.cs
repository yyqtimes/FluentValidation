using GalaSoft.MvvmLight;
using MVFModel;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using MVFService;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using MVFSystem.tools;
namespace MVFSystem.ViewModel
{

    public class MainViewModel : ViewModelBase
    {
        private StudentService service = new StudentService();
        public MainViewModel()
        {
            _StudentList = service.GetStudentList();

        }
        #region 属性
        private List<StudentModel> _StudentList;
        public List<StudentModel> StudentList
        {
            get { return _StudentList; }
            set
            {
                _StudentList = value;
                RaisePropertyChanged(() => StudentList);
            }
        }
        private StudentModel _currentItem;
        public StudentModel CurrentItem
        {
            get { return _currentItem; }
            set
            {
                if (_currentItem == value) return;
                _currentItem = value;
                //发送消息显示选中的信息
                Messenger.Default.Send(value, "showMessage");
                RaisePropertyChanged(() => CurrentItem);
            }
        }
        #endregion

        #region 命令
        public RelayCommand AddCommand
        {
            get
            {
                return new RelayCommand(() =>
                {    /*
                    //此方法为简化方法
                    //使用简化方法不需要在XAML页面声明 DataContext="{Binding StudentForm,Source={StaticResource Locator}}"
                    //也不需要在ViewModelLocator中注册StudentVM
                    var vm = AutofacInstaceFactory.GetInstance<StudentVM>();
                     fromWin.DataContext = vm;
                    */
                    var fromWin = new StudentForm();
                    fromWin.Title = "新增";
                    fromWin.Show();
                });
            }
        }
        /// <summary>
        /// 封装验证demo
        /// </summary>
        public RelayCommand AddCommandTo
        {
            get
            {
                return new RelayCommand(() =>
                {
                  
                    var formWin = new WPFValidator();
                    formWin.Title = "继承BaseModel封装验证";
                   
                    formWin.Show();
                });
            }
        }
        #endregion
    }
}