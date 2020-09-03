using System;

namespace NetOpnApiBuilder.Enums
{
    [Flags]
    public enum ApiDataType
    {
        String                   = 0,
        Boolean                  = 1,
        Integer                  = 2,
        LongInteger              = 3,
        Double                   = 4,
        DateTime                 = 5,
        Guid                     = 6,
        Object                   = 7,
        DictionaryOfStrings      = 8,
        DictionaryOfBooleans     = 9,
        DictionaryOfIntegers     = 10,
        DictionaryOfLongIntegers = 11,
        DictionaryOfDoubles      = 12,
        DictionaryOfDateTimes    = 13,
        DictionaryOfGuids        = 14,
        DictionaryOfObjects      = 15,
        ArrayOfStrings           = 16,
        ArrayOfBooleans          = 17,
        ArrayOfIntegers          = 18,
        ArrayOfLongIntegers      = 19,
        ArrayOfDoubles           = 20,
        ArrayOfDateTimes         = 21,
        ArrayOfGuids             = 22,
        ArrayOfObjects           = 23,
        BooleanAsInteger         = 33,
        BooleanAsString          = 65
    }
}
