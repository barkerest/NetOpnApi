using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NetOpnApiBuilder.Models;

namespace NetOpnApiBuilder.ViewModels
{
    public class ObjectTypeList : IReadOnlyList<ApiObjectType>
    {
        public ObjectTypeList(BuilderDb db)
        {
            if (db is null) throw new ArgumentNullException(nameof(db));
            _objectTypes = db.ApiObjectTypes
                             .OrderBy(x => x.Name)
                             .ToArray();
        }

        private readonly IReadOnlyList<ApiObjectType> _objectTypes;

        public IEnumerator<ApiObjectType> GetEnumerator() => _objectTypes.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => _objectTypes.GetEnumerator();

        public int Count => _objectTypes.Count;

        public ApiObjectType this[int index] => _objectTypes[index];

        private IReadOnlyList<SelectListItem> _selectItems;
        
        public IReadOnlyList<SelectListItem> SelectItems
        {
            get
            {
                if (_selectItems != null) return _selectItems;
                _selectItems = _objectTypes.Select(x => new SelectListItem(x.Name, x.ID.ToString())).ToArray();
                return _selectItems;
            }
        }
    }
}
