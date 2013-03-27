using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ProjectBase.Data.Domain
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public abstract class ValidatableObject : BaseObject
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public virtual bool IsValid()
        {
            return ValidationResults().Count == 0;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public virtual ICollection<ValidationResult> ValidationResults()
        {
            var validationResults = new List<ValidationResult>();
            Validator.TryValidateObject(this, new ValidationContext(this, null, null), validationResults, true);
            return validationResults;
        }
    }
}