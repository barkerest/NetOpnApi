﻿using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using NetOpnApiBuilder.Attributes;

namespace NetOpnApiBuilder.Models
{
    public class ApiModule
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
        public bool HasCommandChanges => Controllers?.Any(x => x.HasCommandChanges) ?? false;
        
        public override string ToString()
        {
            var name = string.IsNullOrEmpty(ClrName) ? ApiName : ClrName;
            return $"{Source}/{name}";
        }
    }
}
