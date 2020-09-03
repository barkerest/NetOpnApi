using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using NetOpnApiBuilder.Attributes;
using NetOpnApiBuilder.Enums;

namespace NetOpnApiBuilder.Models
{
    public class ApiObjectProperty : IValidatableObject
    {
        [Key]
        public int ID { get; set; }
        
        [EditorBrowsable(EditorBrowsableState.Never)]
        public int ObjectTypeID { get; set; }
        
        /// <summary>
        /// The object type this property belongs to.
        /// </summary>
        [Required]
        public ApiObjectType ObjectType { get; set; }
        
        /// <summary>
        /// The name of this property in JSON results from the API.
        /// </summary>
        [Required]
        [StringLength(100)]
        public string ApiName { get; set; }
        
        /// <summary>
        /// The name of this property in the CLR.
        /// </summary>
        [Required] 
        [StringLength(100)]
        [SafeClrName]
        public string ClrName { get; set; }
        
        /// <summary>
        /// The data type for this property.
        /// </summary>
        public ApiDataType DataType { get; set; }
        
        [EditorBrowsable(EditorBrowsableState.Never)]
        public int? DataTypeObjectTypeID { get; set; }
        
        /// <summary>
        /// The object type definition when (DataType & Object) == Object
        /// </summary>
        public ApiObjectType DataTypeObjectType { get; set; }
        
        /// <summary>
        /// True if the CLR property can be null.
        /// </summary>
        public bool CanBeNull { get; set; }
        
        /// <summary>
        /// Sample JSON used by the from-json action to update this model.
        /// </summary>
        public string ImportSample { get; set; }

        public override string ToString()
            => string.IsNullOrEmpty(ClrName) ? ApiName : ClrName;
        
        IEnumerable<ValidationResult> IValidatableObject.Validate(ValidationContext validationContext)
        {
            var db = validationContext.GetRequiredService<BuilderDb>();

            var nameUsed = db.ApiObjectProperties.Any(x => x.ID != ID && x.ObjectTypeID == ObjectTypeID && x.ApiName == ApiName);
            if (nameUsed)
            {
                yield return new ValidationResult("already used", new []{nameof(ApiName)});
            }
            nameUsed = db.ApiObjectProperties.Any(x => x.ID != ID && x.ObjectTypeID == ObjectTypeID && x.ClrName == ClrName);
            if (nameUsed)
            {
                yield return new ValidationResult("already used", new []{nameof(ClrName)});
            }

            if ((DataType & ApiDataType.ArrayOfStrings) == ApiDataType.ArrayOfStrings &&
                (DataType & ApiDataType.DictionaryOfStrings) == ApiDataType.DictionaryOfStrings)
            {
                yield return new ValidationResult("cannot specify both array and dictionary", new []{nameof(DataType)});
            }
            
            if ((DataType & ApiDataType.Object) == ApiDataType.Object)
            {
                if (DataTypeObjectTypeID is null)
                {
                    yield return new ValidationResult("is required when data type specifies an object", new []{nameof(DataTypeObjectTypeID), nameof(DataTypeObjectType)});
                }
            }
            else
            {
                if (DataTypeObjectTypeID != null)
                {
                    yield return new ValidationResult("must be null when data type does not specify an object", new []{nameof(DataTypeObjectTypeID), nameof(DataTypeObjectType)});
                }
            }
        }
    }
}
