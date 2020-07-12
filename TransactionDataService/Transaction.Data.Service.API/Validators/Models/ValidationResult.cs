using System.Collections.Generic;
using System.Linq;

namespace Transaction.Data.Service.API.Validators.Models
{
    public class ValidationResult
    {
        public ValidationResult(List<string> validationErrorMessagess)
        {
            ValidationErrorMessagess = validationErrorMessagess;
        }

        public IReadOnlyList<string> ValidationErrorMessagess { get; }

        public bool IsValid => !ValidationErrorMessagess.Any();

        public override string ToString()
        {
            const string NewLine = "\n";
            return string.Join(NewLine, ValidationErrorMessagess.ToArray());
        }
    }
}
