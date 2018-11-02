using System.Collections.Generic;
using System.Linq;

namespace Apps.Common
{

    public class ValidationErrors : List<ValidationError>
    {
        /// <summary>
        /// 添加错误
        /// </summary>
        /// <param name="errorMessage">信息描述</param>
        public void Add(string errorMessage)
        {
            this.Add(new ValidationError { ErrorMessage = errorMessage });
        }

        /// <summary>
        /// Gets 获取错误集合
        /// </summary>
        public string Error
        {
            get
            {
                string error = string.Empty;
                this.All(a =>
                {
                    error += a.ErrorMessage;
                    return true;
                });
                return error;
            }
        }
    }
}
