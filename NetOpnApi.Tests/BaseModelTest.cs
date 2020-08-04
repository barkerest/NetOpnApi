using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text.Json;
using Xunit;
using Xunit.Abstractions;

namespace NetOpnApi.Tests
{
    public abstract class BaseModelTest<TModel, TParamList> 
        where TModel : class,new()
        where TParamList : class, IEnumerable<JParam>, new()
    {
        protected readonly ITestOutputHelper Output;

        protected BaseModelTest(ITestOutputHelper output)
        {
            Output = output ?? throw new ArgumentNullException(nameof(output));
        }

        protected abstract void Compare([NotNull]TModel expected, [NotNull]TModel actual);

        protected abstract TModel Expected { get; }
        
        public static IEnumerable<object[]> Data => new TParamList().Select(x => new object[]{x});

        [Theory]
        [MemberData(nameof(Data))]
        public void ParseCorrectly(JParam param)
        {
            Output.WriteLine("Parsing JSON: " + param.Description);
            var parsed = JsonSerializer.Deserialize<TModel>(param.Json);

            Assert.NotNull(Expected);
            Assert.NotNull(parsed);
            Compare(Expected, parsed);
        }
    }
}
