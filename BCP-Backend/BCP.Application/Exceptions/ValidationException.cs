using System;
using System.Collections.Generic;
using FluentValidation.Results;

namespace BCP.Application.Exceptions
{
    public class ValidationException : Exception
    {
        public List<string> Errors { get; set; }

        public ValidationException() : base("Errores de validación.")
        {
            Errors = new List<string>();
        }

        public ValidationException(IEnumerable<ValidationFailure> failures) : this()
        {
            foreach (var failure in failures)
            {
                Errors.Add(failure.ErrorMessage);
            }
        }
    }
}