using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;
using NetOpnApiBuilder.Enums;
using NetOpnApiBuilder.Extensions;

namespace NetOpnApiBuilder.ViewModels
{
    public class ApiDataTypeList
    {
        public IReadOnlyList<SelectListItem> ListItems { get; }
            = Enum.GetNames(typeof(ApiDataType)).Select(x => new SelectListItem(x.ToSpacedName(), x)).ToArray();
    }
}
