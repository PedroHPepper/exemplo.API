using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace example.API.Models
{
    public class FieldValidatorViewModelOutput
    {
        public IEnumerable<string> Errors { get; set; }
        public FieldValidatorViewModelOutput(IEnumerable<string> errors)
        {
            Errors = errors;
        }
    }
}
