using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
namespace MVFModel
{
    /// <summary>
    /// 
    /// Version 1.0
    /// add by yyq at 2015/11/2 11:15:50
    /// </summary>
    public class StudentValidator:AbstractValidator<StudentModel>
    {
        public StudentValidator()
        {
            RuleFor(p => p.sname).NotEmpty().WithName("名称");
        }
       
    }
}
