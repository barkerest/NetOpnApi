using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using NetOpnApiBuilder.Attributes;

namespace NetOpnApiBuilder.Models
{
    public class ApiObjectType : IValidatableObject
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
        /// The properties for this object type.
        /// </summary>
        public IList<ApiObjectProperty> Properties { get; set; }

        public override string ToString() => Name;

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var db       = validationContext.GetRequiredService<BuilderDb>();
            var nameUsed = db.ApiObjectTypes.Any(x => x.ID != ID && x.Name == Name);
            if (nameUsed)
            {
                yield return new ValidationResult("already used", new[] {nameof(Name)});
            }
        }
    }
}
