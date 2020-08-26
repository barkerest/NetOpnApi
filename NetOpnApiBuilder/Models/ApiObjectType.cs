using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using NetOpnApiBuilder.Attributes;

namespace NetOpnApiBuilder.Models
{
    public class ApiObjectType
    {
        [Key]
        public int ID { get; set; }
        
        /// <summary>
        /// CLR model name.
        /// </summary>
        [Required]
        [StringLength(100)]
        [SafeClrName]
        public string Name { get; set; }

        /// <summary>
        /// The API version this object type was designed against.
        /// </summary>
        [Required]
        [StringLength(32)]
        public string ApiVersion { get; set; }
        
        /// <summary>
        /// The properties for this object type.
        /// </summary>
        public IList<ApiObjectProperty> Properties { get; set; }
        
        public override string ToString() => Name;
        
    }
}
