using MVFModel.common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using ValidationContext = System.ComponentModel.DataAnnotations.ValidationContext;

namespace MVFModel
{
    public class BaseModel:INotifyPropertyChanged,IDataErrorInfo
    {
        public BaseModel()
        {
            _type = GetType();
        }
        #region PropertyCheanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(propertyName));
        } 
        #endregion   

        #region 相关变量定义
        //本Model中的所有属性集合
        private readonly Dictionary<string, PropertyInfo> _propertyGetters = new Dictionary<string, PropertyInfo>();
        //本model中所有属性的验证器集合
        private readonly Dictionary<string, ValidationAttribute[]> _validators =
            new Dictionary<string, ValidationAttribute[]>();

        /// <summary>
        /// 存储不想进行Validation验证的属性的名称的集合
        /// </summary>
        private List<string> ExcludedValidatePropertyNames = new List<string>();

        private readonly Type _type;
        private bool IsValidateEnabled = true;
        #endregion

        #region IDataErrorInfo 接口方法
        public string Error
        {
            get
            {
                if (!IsValidateEnabled) return string.Empty;//未启用验证时不进行验证 add by ljc 2015-08-01

                if (ValidateHelper.GetValidator(this, _type) != null)
                {
                    //先判断有没有FluentValidation错误，有的话直接返回FluentValidation错误（没有的话还按原先的方式验证）add by ljc 2015-07-29
                    var flentValidationErrors = ValidateHelper.GetFluentValidationErrors(this, null);
                    if (!string.IsNullOrEmpty(flentValidationErrors))
                    {
                        return flentValidationErrors;
                    }
                    return string.Empty;//如果用了FluentValidation，就不再用老的方式验证了 add by ljc 2015-08-03
                }

                //获取_validators中所有验证器的错误信息
                var vc = new ValidationContext(this, null, null);
                var errors = new List<string>();
                foreach (var vs in _validators)
                {
                    if (ExcludedValidatePropertyNames != null && ExcludedValidatePropertyNames.Contains(vs.Key)) continue;//不想验证的属性就不进行验证
                    vc.MemberName = vs.Key;
                    object value = _propertyGetters[vs.Key].GetValue(this, null);
                    errors.AddRange(from v in vs.Value
                                    select v.GetValidationResult(value, vc)
                                        into result
                                        where result != null
                                        select result.ErrorMessage);
                }

                return string.Join(Environment.NewLine, errors.ToArray());
            }
        }

        public string this[string columnName]
        {
            get
            {
                if (!IsValidateEnabled) return string.Empty;//未启用验证时不进行验证 add by ljc 2015-08-01

                if (ValidateHelper.GetValidator(this, _type) != null)
                {
                    //先判断有没有FluentValidation错误，有的话直接返回FluentValidation错误（没有的话还按原先的方式验证）add by ljc 2015-07-29
                    var flentValidationErrors = ValidateHelper.GetFluentValidationErrors(this, columnName);
                    if (!string.IsNullOrEmpty(flentValidationErrors))
                    {
                        return flentValidationErrors;
                    }
                    return string.Empty;//如果用了FluentValidation，就不再用老的方式验证了 add by ljc 2015-08-03
                }

                //当ViewModel中值发生变化是触发规则验证
                var vc = new ValidationContext(this, null, null) { MemberName = columnName };
                if (_propertyGetters.ContainsKey(columnName))
                {
                    //不想验证的属性就不进行验证
                    if (ExcludedValidatePropertyNames != null && ExcludedValidatePropertyNames.Contains(columnName))
                    {
                        return string.Empty;
                    }
                    //获取触发的属性值
                    object value = _propertyGetters[columnName].GetValue(this, null);
                    //在_validators中找到相应的规则，并触发验证，返回错误信息
                    var errors = from v in _validators[columnName]
                                 select v.GetValidationResult(value, vc)
                                     into result
                                     where result != null
                                     select result.ErrorMessage;
                    //更新Error属性
                    //OnPropertyChanged("Error");//这句不能加，否则会与Error属性的Get方法形成死循环
                    return string.Join(Environment.NewLine, errors);
                }
                //OnPropertyChanged("Error");//这句不能加，否则会与Error属性的Get方法形成死循环
                return string.Empty;
            }
        } 
        #endregion

        /// <summary>
        /// 设置指定的属性不需要验证
        /// </summary>
        /// add by ljc 2015-07-11
        /// <param name="propertyNames">不需要验证的属性集合</param>
        public void SetNotValidateProperties(string[] propertyNames)
        {
            if (propertyNames != null && propertyNames.Length > 0)
            {
                foreach (var propertyName in propertyNames)
                {
                    if (!ExcludedValidatePropertyNames.Contains(propertyName))
                    {
                        ExcludedValidatePropertyNames.Add(propertyName);
                    }
                }
            }
        }


    }
}
