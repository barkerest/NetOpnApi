using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using NetOpnApiBuilder.Enums;

namespace NetOpnApiBuilder.Models
{
    public class ApiUrlParam : IValidatableObject
    {
        [Key]
        public int ID { get; set; }
        
        [EditorBrowsable(EditorBrowsableState.Never)]
        public int CommandID { get; set; }
        
        /// <summary>
        /// The command this URL parameter belongs to.
        /// </summary>
        [Required]
        public ApiCommand Command { get; set; }
        
        /// <summary>
        /// The order for this parameter to appear in the URL.
        /// </summary>
        public int Order { get; set; }
        
        /// <summary>
        /// The name for this parameter in the API. 
        /// </summary>
        [Required]
        [StringLength(100)]
        public string ApiName { get; set; }
        
        /// <summary>
        /// The name for this parameter in the CLR.
        /// </summary>
        [StringLength(100)]
        public string ClrName { get; set; }
        
        /// <summary>
        /// The data type of this parameter (cannot include Object, Array, or Dictionary).
        /// </summary>
        public ApiDataType DataType { get; set; }
        
        /// <summary>
        /// True if the parameter can be omitted from the URL.
        /// </summary>
        public bool AllowNull { get; set; }

        public override string ToString() => ApiName;
        
        IEnumerable<ValidationResult> IValidatableObject.Validate(ValidationContext validationContext)
        {
            if ((DataType & ApiDataType.Object) == ApiDataType.Object)
            {
                yield return new ValidationResult("cannot specify an object", new []{nameof(DataType)});
            }
            
            if ((DataType & ApiDataType.Array) == ApiDataType.Array)
            {
                yield return new ValidationResult("cannot specify an array", new []{nameof(DataType)});
            }

            if ((DataType & ApiDataType.Dictionary) == ApiDataType.Dictionary)
            {
                yield return new ValidationResult("cannot specify a dictionary", new []{nameof(DataType)});
            }
        }
    }
}
