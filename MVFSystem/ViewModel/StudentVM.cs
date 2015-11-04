using GalaSoft.MvvmLight;
using MVFModel;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using MVFService;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using System.Windows;

namespace MVFSystem.ViewModel
{
    /// <summary>
    /// 
    /// Version 1.0
    /// add by yyq at 2015/11/2 11:23:35
    /// </summary>
    public class StudentVM : ViewModelBase
    {
        StudentService service = new StudentService();
        public StudentVM()
        {

        }

        #region 属性
        private StudentModel _item=new StudentModel();
        public StudentModel Item
        {
            get { return _item; }
            set
            {
                _item = value;
                RaisePropertyChanged(() => Item);
            }
        }
        #endregion

        #region 命令
        /// <summary>
        /// 
        /// </summary>
        public RelayCommand Save
        {
            get
            {
                return new RelayCommand(() =>
                {
                    StudentModel model = _item;
                    MessageBox.Show(model.Error);
                });
            }
        }
        #endregion
    }
}
