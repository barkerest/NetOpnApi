using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore.Internal;
using NetOpnApiBuilder.Enums;
using NetOpnApiBuilder.Extensions;

namespace NetOpnApiBuilder.ViewModels
{
    public class UrlApiDataTypeList
    {
        public IReadOnlyList<SelectListItem> ListItems { get; }
            = Enum.GetNames(typeof(ApiDataType))
                  .Where(x => 
                             !x.StartsWith("Array")
                             && !x.StartsWith("Dictionary")
                             && !x.Equals("Object"))
                  .Select(x => new SelectListItem(x.ToSpacedName(), x))
                  .ToArray();
    }
}
