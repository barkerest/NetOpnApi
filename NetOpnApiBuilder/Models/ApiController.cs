using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.DependencyInjection;
using NetOpnApiBuilder.Attributes;

namespace NetOpnApiBuilder.Models
{
    public class ApiController : IValidatableObject
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

        [EditorBrowsable(EditorBrowsableState.Never)]
        public int ModuleID { get; set; }
        
        /// <summary>
        /// The module this controller belongs to.
        /// </summary>
        [Required]
        public ApiModule Module { get; set; }
        
        /// <summary>
        /// True to skip exporting this controller.
        /// </summary>
        public bool Skip { get; set; }
        
        /// <summary>
        /// The commands belonging to this controller.
        /// </summary>
        public IList<ApiCommand> Commands { get; set; }

        /// <summary>
        /// True if there are any commands with changes needing looked at.
        /// </summary>
        [NotMapped]
        public bool HasCommandChanges { get; set; }

        public override string ToString()
        {
            var name = string.IsNullOrEmpty(ClrName) ? ApiName : ClrName;
            return $"{Module}/{name}";
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var db       = validationContext.GetRequiredService<BuilderDb>();
            var nameUsed = db.ApiControllers.Any(x => x.ID != ID && x.ModuleID == ModuleID && x.ApiName == ApiName);
            if (nameUsed)
            {
                yield return new ValidationResult("already used", new []{nameof(ApiName)});
            }
            nameUsed = db.ApiControllers.Any(x => x.ID != ID && x.ModuleID == ModuleID && x.ClrName == ClrName);
            if (nameUsed)
            {
                yield return new ValidationResult("already used", new []{nameof(ClrName)});
            }
        }
    }
}
