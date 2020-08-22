using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace NetOpnApiBuilder.Models
{
    public class ApiController
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

        [EditorBrowsable(EditorBrowsableState.Never)]
        public int ModuleID { get; set; }
        
        /// <summary>
        /// The module this controller belongs to.
        /// </summary>
        [Required]
        public ApiModule Module { get; set; }
        
        /// <summary>
        /// The commands belonging to this controller.
        /// </summary>
        public IList<ApiCommand> Commands { get; set; }


        public override string ToString()
            => $"{Module}/{ApiName}";
    }
}
