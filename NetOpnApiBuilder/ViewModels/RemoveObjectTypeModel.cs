using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using NetOpnApiBuilder.Models;

namespace NetOpnApiBuilder.ViewModels
{
    public class RemoveObjectTypeModel
    {
        public ApiObjectType ObjectType { get; set; }
        
        public IReadOnlyList<ApiObjectType> OtherObjectTypes { get; set; }
        
    }
}
