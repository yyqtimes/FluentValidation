using MVFModel.ModelValidator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVFModel
{
    /// <summary>
    /// 
    /// Version 1.0
    /// add by yyq at 2015/11/3 17:36:20
    /// </summary>
    public class WPFValidatorModel:BaseModel
    {
        #region FluentValidation验证器
        //属性名称不能修改 在ValidateHelper通过反射获取FluentValidator
        private static WPFValidator _fluentValidator;
        public static WPFValidator FluentValidator
        {
            get { return _fluentValidator ?? (_fluentValidator = new WPFValidator()); }
        }
        #endregion

        #region Model
        private int _id;
        private string _sname;
        private string _saddress;
        private int? _sage;
        private string _emails;
        private decimal? _sfee;
        /// <summary>
        /// 
        /// </summary>
        public int Id
        {
            set
            {
                if (_id == value) return;
                _id = value;
                OnPropertyChanged("Id");
            }
            get { return _id; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string sname
        {
            set
            {
                if (_sname == value) return;
                _sname = value;
                OnPropertyChanged("sname");
            }
            get { return _sname; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string saddress
        {
            set
            {
                if (_saddress == value) return;
                _saddress = value;
                OnPropertyChanged("saddress");
            }
            get { return _saddress; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? sage
        {
            set
            {
                if (_sage == value) return;
                _sage = value;
                OnPropertyChanged("sage");
            }
            get { return _sage; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string emails
        {
            set
            {
                if (_emails == value) return;
                _emails = value;
                OnPropertyChanged("emails");
            }
            get { return _emails; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal? sfee
        {
            set
            {
                if (sfee == value) return;
                _sfee = value;
                OnPropertyChanged("sfee");
            }
            get { return _sfee; }
        }
        public string AAA
        {
            get;
            set;
        }
        #endregion Model
    }
}
