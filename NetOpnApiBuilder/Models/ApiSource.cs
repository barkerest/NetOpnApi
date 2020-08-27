using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;

namespace NetOpnApiBuilder.Models
{
    public class ApiSource : IValidatableObject
    {
        [Key]
        public int ID { get; set; }
        
        /// <summary>
        /// The name of the API source within the repository.
        /// </summary>
        [Required()]
        [StringLength(120)]
        public string Name { get; set; }

        /// <summary>
        /// The source version.
        /// </summary>
        [Required]
        [StringLength(32)]
        public string Version { get; set; }
        
        /// <summary>
        /// The date of the last sync from this source.
        /// </summary>
        public DateTime? LastSync { get; set; }
        
        /// <summary>
        /// The temporary file path used at runtime.
        /// </summary>
        [NotMapped]
        public string TemporaryPath { get; set; }
        
        /// <summary>
        /// The modules from this source.
        /// </summary>
        public IList<ApiModule> Modules { get; set; }
        
        
        
        public override string ToString()
            => Name;

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var db    = validationContext.GetRequiredService<BuilderDb>();
            var nameUsed = db.ApiSources.Any(x => x.ID != ID && x.Name == Name);
            if (nameUsed)
            {
                yield return new ValidationResult("already taken", new []{nameof(Name)});
            }
        }
    }
}
