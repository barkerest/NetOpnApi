using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using NetOpnApiBuilder.Attributes;

namespace NetOpnApiBuilder.Models
{
    public class ApiCommand
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

        [EditorBrowsable(EditorBrowsableState.Never)]
        public int? PostBodyObjectTypeID { get; set; }
        
        /// <summary>
        /// The object type definition for post requests.
        /// </summary>
        public ApiObjectType PostBodyObjectType { get; set; }
        
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
    }
}
