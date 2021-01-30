using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFDataValidation
{
    public class NameExists : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            var name = value as string;
            if (name != "Felix")
            {
                return false;
            }
            return true;
        }

        public override string FormatErrorMessage(string name)
        {
            return "输入正确的名字";
        }
    }
}
