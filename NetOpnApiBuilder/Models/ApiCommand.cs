using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

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
        public string ClrName { get; set; }

        /// <summary>
        /// True if the command uses POST, false if the command uses GET.
        /// </summary>
        public bool UsePost { get; set; }
        
        [EditorBrowsable(EditorBrowsableState.Never)]
        public int ControllerID { get; set; }
        
        /// <summary>
        /// The controller this command belongs to.
        /// </summary>
        [Required]
        public ApiController Controller { get; set; }

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

        /// <summary>
        /// The API version this command was designed against.
        /// </summary>
        public string ApiVersion { get; set; }
        
        public override string ToString()
            => $"{Controller}/{ApiName}";
    }
}
