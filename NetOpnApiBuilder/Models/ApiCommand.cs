using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using NetOpnApiBuilder.Attributes;
using NetOpnApiBuilder.Enums;

namespace NetOpnApiBuilder.Models
{
    public class ApiCommand : IValidatableObject
    {
        [Key]
        public int ID { get; set; }

        /// <summary>
        /// The name according to the API.
        /// </summary>
        [Required]
        [StringLength(100)]
        public string ApiName { get; set; }

        /// <summary>
        /// The name we'll use in the CLR.
        /// </summary>
        [Required] 
        [StringLength(100)]
        [SafeClrName]
        public string ClrName { get; set; }

        /// <summary>
        /// True if the command uses POST, false if the command uses GET.
        /// </summary>
        public bool UsePost { get; set; }
        
        /// <summary>
        /// The function signature for the command.
        /// </summary>
        [Required]
        public string Signature { get; set; }
        
        /// <summary>
        /// The comment preceding the function definition in the source file.
        /// </summary>
        public string Comment { get; set; }
        
        /// <summary>
        /// The body of the function from the source file.
        /// </summary>
        [Required]
        public string Body { get; set; }
        
        [EditorBrowsable(EditorBrowsableState.Never)]
        public int ControllerID { get; set; }
        
        /// <summary>
        /// The controller this command belongs to.
        /// </summary>
        [Required]
        public ApiController Controller { get; set; }

        /// <summary>
        /// True if the command is a new addition.
        /// </summary>
        public bool NewCommand { get; set; }
        
        /// <summary>
        /// True if the command definition has changed.
        /// </summary>
        public bool CommandChanged { get; set; }
        
        /// <summary>
        /// The URL parameters for this command.
        /// </summary>
        public IList<ApiUrlParam> UrlParams { get; set; }
        
        /// <summary>
        /// The query parameters for this command.
        /// </summary>
        public IList<ApiQueryParam> QueryParams { get; set; }

        /// <summary>
        /// The data type for post requests.
        /// </summary>
        public ApiDataType? PostBodyDataType { get; set; }
        
        [EditorBrowsable(EditorBrowsableState.Never)]
        public int? PostBodyObjectTypeID { get; set; }
        
        /// <summary>
        /// The object type definition for post requests.
        /// </summary>
        public ApiObjectType PostBodyObjectType { get; set; }
        
        /// <summary>
        /// The data type for the response.
        /// </summary>
        public ApiDataType? ResponseBodyDataType { get; set; }
        
        [EditorBrowsable(EditorBrowsableState.Never)]
        public int? ResponseBodyObjectTypeID { get; set; }

        /// <summary>
        /// The object type definition for the response.
        /// </summary>
        public ApiObjectType ResponseBodyObjectType { get; set; }
        
        /// <summary>
        /// The source version this command was loaded from.
        /// </summary>
        public string SourceVersion { get; set; }
        
        /// <summary>
        /// True to skip exporting this command.
        /// </summary>
        public bool Skip { get; set; }
        
        public override string ToString()
        {
            var name = string.IsNullOrEmpty(ClrName) ? ApiName : ClrName;
            return $"{Controller}/{name}";
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var db       = validationContext.GetRequiredService<BuilderDb>();
            var nameUsed = db.ApiCommands.Any(x => x.ID != ID && x.ControllerID == ControllerID && x.ApiName == ApiName);
            if (nameUsed)
            {
                yield return new ValidationResult("already used", new []{nameof(ApiName)});
            }
            nameUsed = db.ApiCommands.Any(x => x.ID != ID && x.ControllerID == ControllerID && x.ClrName == ClrName);
            if (nameUsed)
            {
                yield return new ValidationResult("already used", new []{nameof(ClrName)});
            }

            if (PostBodyDataType is null)
            {
                if (PostBodyObjectTypeID != null)
                {
                    yield return new ValidationResult("must be null when data type does not specify an object", new[] {nameof(PostBodyObjectTypeID)});
                }
            }
            else
            {
                if ((PostBodyDataType.Value & ApiDataType.ArrayOfStrings) == ApiDataType.ArrayOfStrings &&
                    (PostBodyDataType.Value & ApiDataType.DictionaryOfStrings) == ApiDataType.DictionaryOfStrings)
                {
                    yield return new ValidationResult("cannot specify both array and dictionary", new[] {nameof(PostBodyDataType)});
                }

                if ((PostBodyDataType.Value & ApiDataType.Object) == ApiDataType.Object)
                {
                    if (PostBodyObjectTypeID is null)
                    {
                        yield return new ValidationResult("is required when data type specifies an object", new[] {nameof(PostBodyObjectTypeID), nameof(PostBodyObjectType)});
                    }
                }
                else
                {
                    if (PostBodyObjectTypeID != null)
                    {
                        yield return new ValidationResult("must be null when data type does not specify an object", new[] {nameof(PostBodyObjectTypeID), nameof(PostBodyObjectType)});
                    }
                }
            }
            
            if (ResponseBodyDataType is null)
            {
                if (ResponseBodyObjectTypeID != null)
                {
                    yield return new ValidationResult("must be null when data type does not specify an object", new[] {nameof(ResponseBodyObjectTypeID)});
                }
            }
            else
            {
                if ((ResponseBodyDataType.Value & ApiDataType.ArrayOfStrings) == ApiDataType.ArrayOfStrings &&
                    (ResponseBodyDataType.Value & ApiDataType.DictionaryOfStrings) == ApiDataType.DictionaryOfStrings)
                {
                    yield return new ValidationResult("cannot specify both array and dictionary", new[] {nameof(ResponseBodyDataType)});
                }

                if ((ResponseBodyDataType.Value & ApiDataType.Object) == ApiDataType.Object)
                {
                    if (ResponseBodyObjectTypeID is null)
                    {
                        yield return new ValidationResult("is required when data type specifies an object", new[] {nameof(ResponseBodyObjectTypeID), nameof(ResponseBodyObjectType)});
                    }
                }
                else
                {
                    if (ResponseBodyObjectTypeID != null)
                    {
                        yield return new ValidationResult("must be null when data type does not specify an object", new[] {nameof(ResponseBodyObjectTypeID), nameof(ResponseBodyObjectType)});
                    }
                }
            }
        }
    }
}
