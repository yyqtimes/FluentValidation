using GalaSoft.MvvmLight;
using MVFModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVFSystem.ViewModel
{
    /// <summary>
    /// 
    /// Version 1.0
    /// add by yyq at 2015/11/3 17:45:25
    /// </summary>
    public class WPFValidatorVM:ViewModelBase
    {
        #region 属性
        private WPFValidatorModel _item = new WPFValidatorModel();
        public WPFValidatorModel Item
        {
            get { return _item; }
            set
            {
                _item = value;
                RaisePropertyChanged(() => Item);
            }
        }
        #endregion
    }
}
