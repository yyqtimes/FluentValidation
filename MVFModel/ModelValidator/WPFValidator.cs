using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
namespace MVFModel.ModelValidator
{
    /// <summary>
    /// 
    /// Version 1.0
    /// add by yyq at 2015/11/3 17:37:12
    /// </summary>
    public class WPFValidator:AbstractValidator<WPFValidatorModel>
    {
        public WPFValidator()
        {
            RuleFor(m => m.sname).NotEmpty().Equal("闫燕清").Length(1, 5).WithName("名称");
        }
        // 属性名称不能修改 在ValidateHelper通过反射获取RulePropertyNames
        private static List<string> _rulePropertyNames;
        public static List<string> RulePropertyNames
        {
            get
            {
                if (_rulePropertyNames != null)
                {
                    return _rulePropertyNames;
                }
                return _rulePropertyNames = new WPFValidator().CreateDescriptor().
                   GetMembersWithValidators().Select(p => p.Key).ToList();
            }
        }
    }
}
