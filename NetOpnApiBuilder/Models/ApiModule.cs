using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using NetOpnApiBuilder.Attributes;

namespace NetOpnApiBuilder.Models
{
    public class ApiModule : IValidatableObject
    {
        [Key]
        public int ID { get; set; }
        
        [EditorBrowsable(EditorBrowsableState.Never)]
        public int SourceID { get; set; }
        
        /// <summary>
        /// The API source for this module.
        /// </summary>
        public ApiSource Source { get; set; }
        
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
        /// True to skip exporting this module.
        /// </summary>
        public bool Skip { get; set; }
        
        /// <summary>
        /// The controllers belonging to this module.
        /// </summary>
        public IList<ApiController> Controllers { get; set; }

        /// <summary>
        /// True if there are any commands with changes needing looked at.
        /// </summary>
        [NotMapped]
        public bool HasCommandChanges { get; set; }
        
        public override string ToString()
        {
            var name = string.IsNullOrEmpty(ClrName) ? ApiName : ClrName;
            return $"{Source}/{name}";
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var db       = validationContext.GetRequiredService<BuilderDb>();
            var nameUsed = db.ApiModules.Any(x => x.ID != ID && x.SourceID == SourceID && x.ApiName == ApiName);
            if (nameUsed)
            {
                yield return new ValidationResult("already used", new []{nameof(ApiName)});
            }
            nameUsed = db.ApiModules.Any(x => x.ID != ID && x.SourceID == SourceID && x.ClrName == ClrName);
            if (nameUsed)
            {
                yield return new ValidationResult("already used", new []{nameof(ClrName)});
            }
        }
    }
}
