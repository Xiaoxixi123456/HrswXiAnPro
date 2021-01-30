using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrsw.XiAnPro.Utilities
{
    public static class ValidationExtension
    {
        public static string ValidateProperty<MetadataType>(this object obj, string propertyName)
        {
            if (string.IsNullOrEmpty(propertyName))
            {
                return string.Empty;
            }

            var targetType = obj.GetType();
            if (targetType != typeof(MetadataType))
            {
                TypeDescriptor.AddProviderTransparent(new AssociatedMetadataTypeTypeDescriptionProvider(targetType, typeof(MetadataType)), targetType);
            }

            var propertyValue = targetType.GetProperty(propertyName).GetValue(obj, null);
            var validationContext = new ValidationContext(obj, null, null) { MemberName = propertyName };
            var validationResults = new List<ValidationResult>();
            Validator.TryValidateProperty(propertyValue, validationContext, validationResults);
            if (validationResults.Count > 0)
            {
                return validationResults.First().ErrorMessage;
            }
            return string.Empty;
        }

        public static bool ValidateProperty<MetadataType>(this object obj, int index)
        {
            var targetType = obj.GetType();
            var propertyName = obj.GetType().GetProperties()[index].Name;
            if (propertyName == "Item" || propertyName == "Error")
            {
                return false;
            }
            bool isError = false;
            if (targetType != typeof(MetadataType))
            {
                TypeDescriptor.AddProviderTransparent(new AssociatedMetadataTypeTypeDescriptionProvider(targetType, typeof(MetadataType)), targetType);
            }

            var propertyValue = targetType.GetProperty(propertyName).GetValue(obj, null);
            var validationContext = new ValidationContext(obj, null, null) { MemberName = propertyName };
            var validationResults = new List<ValidationResult>();
            Validator.TryValidateProperty(propertyValue, validationContext, validationResults);
            if (validationResults.Count > 0)
            {
                isError = true;
            }
            else
            {
                isError = false;
            }
            return isError;
        }
    }
}
