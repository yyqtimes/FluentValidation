using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using FluentValidation;
using FluentValidation.Results;

namespace MVFModel.common
{
    /// <summary>
    /// 验证辅助类
    /// </summary>
    /// add by ljc 2015-07-31
    /// modify by ljc 2015-08-05 将验证器和验证方法放在一个全局的容器里，使得一种类型的验证器和验证方法，只需取一次就行了
    public static class ValidateHelper
    {
        #region 全局变量
        /// <summary>
        /// 验证器的全局容器
        /// </summary>
        private static Dictionary<string, object> _validators = new Dictionary<string, object>();

        /// <summary>
        /// 验证器内定义了规则的属性名集合的全局容器（key为验证器类型名,value为该model对应的验证器内所有定义了规则的属性名的集合）
        /// </summary>
        private static Dictionary<string, List<string>> _rulePropertyNamesDictionary = new Dictionary<string, List<string>>();

        /// <summary>
        /// 可定制字段验证的验证方法的全局容器
        /// </summary>
        private static Dictionary<string, MethodInfo> _customValidateMethods = new Dictionary<string, MethodInfo>();

        /// <summary>
        /// 全部规则验证的验证方法的全局容器
        /// </summary>
        private static Dictionary<string, MethodInfo> _fullValidateMethods = new Dictionary<string, MethodInfo>();

        /// <summary>
        /// 静态的扩展验证方法
        /// </summary>
        ///获取DefaultValidatorExtensions的方法：public static ValidationResult Validate<T>(this IValidator<T> validator, T instance, params string[] properties)
        private static MethodInfo _extensionMethod = typeof(DefaultValidatorExtensions).GetMethods().
               FirstOrDefault(p => p.Name == "Validate" &&
               p.GetParameters().Length == 3 && p.GetParameters()[2].ParameterType == typeof(string[]));
        #endregion

        #region 私有方法

        /// <summary>
        /// 根据指定的model类型获取其对应的验证器的可定制字段验证的验证方法
        /// </summary>
        /// <param name="modelType">指定的model类型</param>
        /// <returns></returns>
        private static MethodInfo GetCustomValidateMethod(Type modelType)
        {
            if (modelType == null) return null;

            var modelName = modelType.FullName;

            //字典里已有就直接取
            if (_customValidateMethods.ContainsKey(modelName))
            {
                return _customValidateMethods[modelName];
            }

            //字典里没有就去找
            if (_extensionMethod != null)
            {
                var method = _extensionMethod.MakeGenericMethod(new[] { modelType });
                _customValidateMethods.Add(modelName, method);
                return method;
            }

            //没找到就将空值加进去，这样可防止下次再找
            _customValidateMethods.Add(modelName, null);
            return null;
        }

        /// <summary>
        /// 根据指定的model对应的验证器类型获取其对应的验证器的全部规则验证的验证方法
        /// </summary>
        /// <param name="validatorType">指定的model对应的验证器的类型</param>
        /// <param name="modelType">指定的model类型</param>
        /// <returns></returns>
        private static MethodInfo GetFullValidateMethod(Type validatorType, Type modelType)
        {
            if (validatorType == null || modelType == null) return null;

            var validatorName = validatorType.FullName;

            //字典里已有就直接取
            if (_fullValidateMethods.ContainsKey(validatorName))
            {
                return _fullValidateMethods[validatorName];
            }

            //字典里没有就去找
            //获取AbstractValidator<T>的方法：public virtual ValidationResult Validate(T instance)
            var fullValidateMethod = validatorType.GetMethod("Validate", new[] { modelType });
            if (fullValidateMethod != null)
            {
                _fullValidateMethods.Add(validatorName, fullValidateMethod);
                return fullValidateMethod;
            }

            //没找到就将空值加进去，这样可防止下次再找
            _fullValidateMethods.Add(validatorName, null);
            return null;
        }

        /// <summary>
        /// 根据指定的model对应的验证器获取其对应的验证器内所有定义了规则的属性名的集合
        /// </summary>
        /// <param name="validator">验证器的类型</param>
        /// <returns></returns>
        public static List<string> GetRulePropertyNames(object validator)
        {
            if (validator == null) return null;

            var validatorType = validator.GetType();
            var validatorName = validatorType.FullName;

            //字典里已有就直接取
            if (_rulePropertyNamesDictionary.ContainsKey(validatorName))
            {
                return _rulePropertyNamesDictionary[validatorName];
            }

            //字典里没有就去找
            //每个验证器里都定义了一个RulePropertyNames属性，用于获取验证器里定义了规则的属性名集合
            var rulePropertyNamesProperty = validatorType.GetProperty("RulePropertyNames");
            if (rulePropertyNamesProperty != null)
            {
                var propertyValue = rulePropertyNamesProperty.GetValue(validator, BindingFlags.Public | BindingFlags.Static, null, null, null) as List<string>;
                _rulePropertyNamesDictionary.Add(validatorName, propertyValue);
                return propertyValue;
            }

            //没找到就将空值加进去，这样可防止下次再找
            _rulePropertyNamesDictionary.Add(validatorName, null);
            return null;
        }

        #endregion

        #region 公共方法

        /// <summary>
        /// 根据指定的model类型获取其对应的验证器
        /// </summary>
        /// 备注：用两个参数的目的是考虑性能
        /// <param name="model">指定的model</param>
        /// <param name="modelType">指定的model的类型</param>
        /// <returns></returns>
        public static object GetValidator(object model, Type modelType)
        {
            if (modelType == null) return null;

            var modelName = modelType.FullName;

            //字典里已有就直接取
            if (_validators.ContainsKey(modelName))
            {
                return _validators[modelName];
            }

            //字典里没有就去找
            var validatorProperty = modelType.GetProperty("FluentValidator");
            if (validatorProperty != null)
            {
                var validator = validatorProperty.GetValue(model, null);
                _validators.Add(modelName, validator);
                return validator;
            }

            //没找到就将空值加进去，这样可防止下次再找
            _validators.Add(modelName, null);
            return null;
        }

        /// <summary>
        /// 获取验证错误信息（columnName为空则认为是全部验证）
        /// </summary>
        /// add by ljc 2015-07-30
        /// modify by ljc 2015-08-04 validator和methodInfo都改成从本类的全局容器里取
        /// <param name="model">BaseViewModel或BaseModel</param>
        /// <param name="columnNames">指定要验证的字段名数组（为空则认为是全部验证）</param>
        /// <returns></returns>
        public static string GetFluentValidationErrors(object model, params string[] columnNames)
        {
#if DEBUG
            var st = new Stopwatch();
            st.Start();
#endif
            if (model == null) return string.Empty;

            var modelType = model.GetType();
            var validator = GetValidator(model, modelType);
            if (validator == null) return string.Empty;

            var rulePropertyNames = GetRulePropertyNames(validator);
            //如果没有为columnName定义验证规则就直接返回，不进行验证
            if (rulePropertyNames == null || rulePropertyNames.Count == 0 ||
                (columnNames != null && columnNames.Length > 0 && !columnNames.Any(rulePropertyNames.Contains)))
            {
                return string.Empty;
            }

            ValidationResult result;
            if (columnNames != null && columnNames.Length > 0)//对指定属性进行验证
            {
                var validateMethod = GetCustomValidateMethod(modelType);
                if (validateMethod == null) return string.Empty;
                result = (ValidationResult)validateMethod.
                    Invoke(null, BindingFlags.Public | BindingFlags.Static, null,
                        new[] { validator, model, columnNames }, null);
            }
            else//对所有非RuleSet里的规则进行验证
            {
                var validateMethod = GetFullValidateMethod(validator.GetType(), modelType);
                if (validateMethod == null) return string.Empty;
                result = (ValidationResult)validateMethod.Invoke(validator, new[] { model });
            }

            if (result != null && !result.IsValid)
            {
                return string.Join(Environment.NewLine, result.Errors.Select(p => p.ErrorMessage));
            }

#if DEBUG
            st.Stop();
            if (ShouldCollectValidateInfo)
            {
                ValidateInfos.Add(new ValidateInfo()
                {
                    ModelType = modelType.ToString(),
                    Model = model,
                    ColumnNames = columnNames,
                    ValidateTimeSpan = st.Elapsed,
                    ValidateTime = DateTime.Now
                });
            }
#endif
            return string.Empty;
        }

        #endregion

#if DEBUG
        #region 调试代码

        /// <summary>
        /// 是否要搜集验证的时间消耗信息
        /// </summary>
        public static bool ShouldCollectValidateInfo;
        /// <summary>
        /// 验证的时间消耗信息数组
        /// </summary>
        public static List<ValidateInfo> ValidateInfos = new List<ValidateInfo>();

        public class ValidateInfo
        {
            public string ModelType;
            public object Model;
            public string[] ColumnNames;
            public TimeSpan ValidateTimeSpan;
            public DateTime ValidateTime;
        }

        public static void WriteValidateInfosToLog()
        {
            string itemsInfo = "Log Began:" + Environment.NewLine + string.Join(Environment.NewLine,
                ValidateInfos.Select(
                    p =>
                        p.ValidateTime + ":" + p.ModelType + ", ColumnName=" + p.ColumnNames.FirstOrDefault() + ", totalmilliseconds=" +
                        p.ValidateTimeSpan.TotalMilliseconds));
            string summaryInfo = Environment.NewLine + "ModelType ValidateTimeSpan Summary:" + Environment.NewLine;
            var modelTypes = ValidateInfos.Select(p => p.ModelType).Distinct();
            summaryInfo += string.Join(Environment.NewLine, modelTypes.
                Select(
                    p =>
                        p + ":TotalValidateCount=" + ValidateInfos.Count(z => z.ModelType == p)
                        + ",ColumnValidateCount=" + ValidateInfos.Count(z => z.ModelType == p && !string.IsNullOrEmpty(z.ColumnNames.FirstOrDefault()))
                        + ",FullValidateCount=" + ValidateInfos.Count(z => z.ModelType == p && string.IsNullOrEmpty(z.ColumnNames.FirstOrDefault()))
                        + ",TimeSpan=" + ValidateInfos.Where(q => q.ModelType == p).Sum(x => x.ValidateTimeSpan.TotalMilliseconds)));
            var totalTimeSpan = Environment.NewLine + "totalTimeSpan=" + ValidateInfos.Sum(p => p.ValidateTimeSpan.TotalMilliseconds);
        }

        #endregion
#endif
    }
}
