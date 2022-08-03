using ProductCatalogue.Application.Common.Models;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ProductCatalogue.Application.Common.Exceptions
{
    public class ValidationException : Exception
    {
        #region Properties
        public Errors Errors { get; } 
        #endregion

        #region Constructors
        public ValidationException()
           : base("One or more validation failures have occurred.")
        {
            Errors = new Errors();
        }

        public ValidationException(IEnumerable<ValidationFailure> failures)
            : this()
        {
            Errors.AddErrors(failures
                .GroupBy(e => e.PropertyName, e => e.ErrorMessage)
                .ToDictionary(failureGroup => failureGroup.Key, failureGroup => failureGroup.ToArray()));
        } 
        #endregion

       
    }
}