using System;

namespace NetOpnApiBuilder.Enums
{
    [Flags]
    public enum ApiDataType
    {
        String      = 0,
        Boolean     = 1,
        Integer     = 2,
        LongInteger = 3,
        Double      = 4,
        DateTime    = 5,
        Guid        = 6,
        Object      = 7,
        Dictionary  = 8,    // | (String,Boolean,Integer,LongInteger,Double,DateTime,Guid,Object)
        Array       = 16,   // | (String,Boolean,Integer,LongInteger,Double,DateTime,Guid,Object)
    }
}
