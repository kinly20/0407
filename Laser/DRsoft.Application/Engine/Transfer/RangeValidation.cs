using System.Globalization;

namespace Engine.Transfer
{
    public class RangeValidation : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            double d = 0;
            try
            {
                if (double.TryParse(value.ToString(), out d))
                {
                    if (-100000 < d && d < 100000)
                        return new ValidationResult(true, null);
                    else
                        return new ValidationResult(false, null);
                }
                else
                    return new ValidationResult(false, null);
            }
            catch
            {
                return new ValidationResult(false, null);
            }
        }
    }
}
