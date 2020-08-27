using System.ComponentModel.DataAnnotations;

namespace NetOpnApiBuilder.Models
{
    public class ApiObjectTypeReferences
    {
        public int ObjectTypeID { get; set; }
        
        public int? PropertyID { get; set; }
        
        public int? PostCommandID { get; set; }
        
        public int? ResponseCommandID { get; set; }
    }
}
