using DapperExtensions.Mapper;
using FluentValidation.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVFModel
{
    /// <summary>
    /// 
    /// Version 1.0
    /// add by yyq at 2015/11/2 11:07:22
    /// </summary>
    public class StudentModel:INotifyPropertyChanged,IDataErrorInfo
    {
      
        private static StudentValidator _customerValidator;
        public StudentModel()
        {
            _customerValidator = new StudentValidator();
        }
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

    
        #region IDataInfo接口实现
        public string this[string columnName]
        {
            get
            {
                var result = _customerValidator.Validate(this);
                var errors = result.Errors;
                var firstOrDefault = errors.FirstOrDefault(lol => lol.PropertyName == columnName);
                if (firstOrDefault != null)
                    return _customerValidator != null ? firstOrDefault.ErrorMessage : "";
                return "";
            }
        }

        public string Error
        {
            get
            {
                if (_customerValidator != null)
                {
                    var results = _customerValidator.Validate(this);
                    if (results != null && results.Errors.Any())
                    {
                        var errors = string.Join(Environment.NewLine, results.Errors.Select(x => x.ErrorMessage).ToArray());
                        return errors;
                    }
                }
                return string.Empty;
            }
        }

        #endregion



        #region INotifyPropertyChanged接口实现
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion


      
    }
    public class StudentMapper:ClassMapper<StudentModel>
    {
        public StudentMapper()
        {
            Table("students");
            Map(m => m.AAA).Ignore();
            AutoMap();
        }
    }
}
