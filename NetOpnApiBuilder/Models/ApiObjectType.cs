using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using NetOpnApiBuilder.Attributes;
using NetOpnApiBuilder.Enums;

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
        [SafeClrName(AllowDots = true)]
        public string Name { get; set; }

        /// <summary>
        /// The properties for this object type.
        /// </summary>
        public IList<ApiObjectProperty> Properties { get; set; }

        public override string ToString() => Name;

        private void AddPropValueToSample(StringBuilder builder, ApiObjectProperty prop, int level, BuilderDb db, bool prefixIndent = false)
        {
            var indent = new string(' ', (level + 1) * 2);
            
            if (prefixIndent)
            {
                builder.Append(indent);
            }
            
            switch ((prop.DataType) & ApiDataType.Object)
            {
                case ApiDataType.String:
                    builder.Append("\"abcd\"");
                    break;
                case ApiDataType.Boolean:
                    builder.Append("true");
                    break;
                case ApiDataType.Integer:
                    builder.Append("1234");
                    break;
                case ApiDataType.LongInteger:
                    builder.Append("\"9876543210123456789\"");
                    break;
                case ApiDataType.Double:
                    builder.Append("1234.56");
                    break;
                case ApiDataType.DateTime:
                    builder.Append("\"2020-01-01 12:30:00\"");
                    break;
                case ApiDataType.Guid:
                    builder.Append("\"123e4567-e89b-12d3-a456-426614174000\"");
                    break;
                case ApiDataType.Object:
                    var type = db.ApiObjectTypes.FirstOrDefault(x => x.ID == prop.DataTypeObjectTypeID);
                    if (type is null)
                    {
                        builder.Append("????");
                    }
                    else
                    {
                        type.AddToSample(builder, level + 1, db);
                    }
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        
        private void AddToSample(StringBuilder builder, int level, BuilderDb db, bool topLevel = false)
        {
            if (level > 4)
            {
                builder.Append("...");
                return;
            }

            var indent = new string(' ', (level + 1) * 2);
            
            builder.Append("{\n");

            bool? first = null;
            foreach (var prop in Properties.OrderBy(x => x.ID))
            {
                if (first.HasValue)
                {
                    if (first.Value)
                    {
                        first = false;
                        builder.Append("\n");
                    }
                    else
                    {
                        builder.Append(",\n");
                    }
                }
                else
                {
                    first = true;
                }
                builder.Append(indent);
                builder.Append('"').Append(prop.ApiName).Append("\" = ");
                if ((prop.DataType & ApiDataType.ArrayOfStrings) == ApiDataType.ArrayOfStrings)
                {
                    builder.Append("[\n");
                    AddPropValueToSample(builder, prop, level + 1, db, true);
                    if (topLevel)
                    {
                        builder.Append(",\n");
                        AddPropValueToSample(builder, prop, level + 1, db, true);
                        builder.Append(",\n");
                        AddPropValueToSample(builder, prop, level + 1, db, true);
                    }
                    builder.Append("\n").Append(indent).Append("]");
                }
                else if ((prop.DataType & ApiDataType.DictionaryOfStrings) == ApiDataType.DictionaryOfStrings)
                {
                    builder.Append("{\n");
                    builder.Append(indent).Append("  \"item1\" = ");
                    AddPropValueToSample(builder, prop, level + 2, db);
                    if (topLevel)
                    {
                        builder.Append(",\n");
                        builder.Append(indent).Append("  \"item2\" = ");
                        AddPropValueToSample(builder, prop, level + 2, db);
                        builder.Append(",\n");
                        builder.Append(indent).Append("  \"item3\" = ");
                        AddPropValueToSample(builder, prop, level + 2, db);
                    }
                    builder.Append("\n").Append(indent).Append("}");
                }
                else
                {
                    AddPropValueToSample(builder, prop, level + 1, db);
                }
            }

            builder.Append("\n").Append(' ', level * 2).Append("}");
        }
        
        public string GenerateSample(BuilderDb db)
        {
            var builder = new StringBuilder();
            AddToSample(builder, 0, db, true);
            return builder.ToString();
        }

        [NotMapped]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public string Sample { get; set; }
        
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
