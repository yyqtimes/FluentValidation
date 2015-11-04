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
        #region ����
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
                //������Ϣ��ʾѡ�е���Ϣ
                Messenger.Default.Send(value, "showMessage");
                RaisePropertyChanged(() => CurrentItem);
            }
        }
        #endregion

        #region ����
        public RelayCommand AddCommand
        {
            get
            {
                return new RelayCommand(() =>
                {    /*
                    //�˷���Ϊ�򻯷���
                    //ʹ�ü򻯷�������Ҫ��XAMLҳ������ DataContext="{Binding StudentForm,Source={StaticResource Locator}}"
                    //Ҳ����Ҫ��ViewModelLocator��ע��StudentVM
                    var vm = AutofacInstaceFactory.GetInstance<StudentVM>();
                     fromWin.DataContext = vm;
                    */
                    var fromWin = new StudentForm();
                    fromWin.Title = "����";
                    fromWin.Show();
                });
            }
        }
        /// <summary>
        /// ��װ��֤demo
        /// </summary>
        public RelayCommand AddCommandTo
        {
            get
            {
                return new RelayCommand(() =>
                {
                  
                    var formWin = new WPFValidator();
                    formWin.Title = "�̳�BaseModel��װ��֤";
                   
                    formWin.Show();
                });
            }
        }
        #endregion
    }
}