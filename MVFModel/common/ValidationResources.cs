namespace MVFModel.common
{
    public class ValidationResources
    {
        public static string equal_error
        {
            get
            {
                return "“{PropertyName}”须等于“{ComparisonValue}”。";
            }
        }
        public static string exact_length_error
        {
            get
            {
                return "“{PropertyName}”字数必须为{MaxLength}，您已输入的字数为 {TotalLength}。";
            }
        }
        public static string exclusivebetween_error
        {
            get
            {
                return "“{PropertyName}”必须在 {From} 和 {To} 之间（不包含边界值），您输入的是 {Value}。";
            }
        }
        public static string greaterthan_error
        {
            get
            {
                return "“{PropertyName}”须大于“{ComparisonValue}”。";
            }
        }
        public static string greaterthanorequal_error
        {
            get
            {
                return "“{PropertyName}”须大于或等于“{ComparisonValue}”。";
            }
        }
        public static string inclusivebetween_error
        {
            get
            {
                return "“{PropertyName}”必须在 {From} 和 {To} 之间（包含边界值），您输入的是 {Value}。";
            }
        }
        public static string length_error
        {
            get
            {
                return "“{PropertyName}”字数必须在 {MinLength} 和 {MaxLength} 之间，您已输入的字数为 {TotalLength}。";
            }
        }
        public static string lessthan_error
        {
            get
            {
                return "“{PropertyName}”须小于“{ComparisonValue}”。";
            }
        }
        public static string lessthanorequal_error
        {
            get
            {
                return "“{PropertyName}”须小于或等于“{ComparisonValue}”。";
            }
        }
        public static string notempty_error
        {
            get
            {
                return "“{PropertyName}”必填！";
            }
        }
        public static string notequal_error
        {
            get
            {
                return "“{PropertyName}”不能等于“{ComparisonValue}”。";
            }
        }
        public static string notnull_error
        {
            get
            {
                return "“{PropertyName}”必填！";
            }
        }
        public static string predicate_error
        {
            get
            {
                return "“{PropertyName}”不符合条件。";
            }
        }
        public static string regex_error
        {
            get
            {
                return "“{PropertyName}”格式不正确。";
            }
        }
        public static string email_error
        {
            get
            {
                return "“{PropertyValue}”不是一个正确的邮箱。";
            }
        }
    }
}
