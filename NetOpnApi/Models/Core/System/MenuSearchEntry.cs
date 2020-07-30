using System.Text.Json.Serialization;
using NetOpnApi.JsonConverters;

namespace NetOpnApi.Models.Core.System
{
    /// <summary>
    /// An entry in the menu tree from the device matching the search term.
    /// </summary>
    public class MenuSearchEntry
    {
        /// <summary>
        /// The ID of the menu entry.
        /// </summary>
        [JsonPropertyName("Id")]
        public string ID { get; set; }
        
        /// <summary>
        /// The sort order for the menu entry.
        /// </summary>
        [JsonConverter(typeof(AlwaysInt))]
        public int Order { get; set; }
        
        /// <summary>
        /// The display name.
        /// </summary>
        public string VisibleName { get; set; }
        
        /// <summary>
        /// The CSS class (for rendering).
        /// </summary>
        public string CssClass { get; set; }
        
        /// <summary>
        /// The URL.
        /// </summary>
        public string Url { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        [JsonConverter(typeof(AlwaysBool))]
        public bool IsExternal { get; set; }
        
        /// <summary>
        /// The visibility of the menu entry.
        /// </summary>
        public string Visibility { get; set; }
        
        /// <summary>
        /// True if the menu entry is currently selected (for rendering).
        /// </summary>
        [JsonConverter(typeof(AlwaysBool))]
        public bool Selected { get; set; }
        
        /// <summary>
        /// True if the menu entry is currently visible.
        /// </summary>
        [JsonConverter(typeof(AlwaysBool))]
        [JsonPropertyName("isVisible")]
        public bool IsVisible { get; set; }
        
        /// <summary>
        /// The breadcrumb for the menu entry (to construct a tree from the search results).
        /// </summary>
        [JsonPropertyName("breadcrumb")]
        public string Breadcrumb { get; set; }
        
        /// <summary>
        /// The depth of the entry in the search results (to construct a tree from the search results).
        /// </summary>
        [JsonPropertyName("depth")]
        [JsonConverter(typeof(AlwaysInt))]
        public int Depth { get; set; }
    }
}
