using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text.Json;
using Xunit;
using Xunit.Abstractions;

namespace NetOpnApi.Tests
{
    public abstract class BaseModelTest<TModel, TParamList> 
        where TModel : class,new()
        where TParamList : class, IEnumerable<ModelTestParam<TModel>>, new()
    {
        protected class ParamBuilder
        {
            private readonly string _defJson;

            private readonly List<ModelTestParam<TModel>> _params;
            
            public ParamBuilder(string defaultJson)
            {
                _defJson = defaultJson ?? throw new ArgumentNullException(nameof(defaultJson));
                if (string.IsNullOrEmpty(_defJson)) throw new ArgumentNullException(nameof(defaultJson));
                JsonSerializer.Deserialize<TModel>(_defJson);    // make sure it parses as is.
                _params = new List<ModelTestParam<TModel>>()
                {
                    new ModelTestParam<TModel>(
                        "Default",
                        _defJson
                    )
                };
            }

            public ParamBuilder AddTestsFor(Expression<Func<TModel, string>> prop)
            {
                _params.AddRange(ModelTestParam<TModel>.CreateForProperty(_defJson, prop));
                return this;
            }

            public ParamBuilder AddTestsFor(Expression<Func<TModel, int>> prop)
            {
                _params.AddRange(ModelTestParam<TModel>.CreateForProperty(_defJson, prop));
                return this;
            }

            public ParamBuilder AddTestsFor(Expression<Func<TModel, bool>> prop)
            {
                _params.AddRange(ModelTestParam<TModel>.CreateForProperty(_defJson, prop));
                return this;
            }
            
            public ParamBuilder AddTestsFor(Expression<Func<TModel, long>> prop)
            {
                _params.AddRange(ModelTestParam<TModel>.CreateForProperty(_defJson, prop));
                return this;
            }
            
            public ParamBuilder AddTestsFor(Expression<Func<TModel, double>> prop)
            {
                _params.AddRange(ModelTestParam<TModel>.CreateForProperty(_defJson, prop));
                return this;
            }

            public ParamBuilder AddTestsFor(Expression<Func<TModel, Guid>> prop)
            {
                _params.AddRange(ModelTestParam<TModel>.CreateForProperty(_defJson, prop));
                return this;
            }

            public ParamBuilder AddTestsFor(Expression<Func<TModel, DateTime>> prop)
            {
                _params.AddRange(ModelTestParam<TModel>.CreateForProperty(_defJson, prop));
                return this;
            }

            public ParamBuilder AddTestsFor<TV>(Expression<Func<TModel, IDictionary<string, TV>>> prop, IDictionary<string, TV> nonEmptyValue)
            {
                _params.AddRange(ModelTestParam<TModel>.CreateForProperty(_defJson, prop, nonEmptyValue));
                return this;
            }

            public ParamBuilder AddTestsFor<TV>(Expression<Func<TModel, IReadOnlyList<TV>>> prop, IReadOnlyList<TV> nonEmptyValue)
            {
                _params.AddRange(ModelTestParam<TModel>.CreateForProperty(_defJson, prop, nonEmptyValue));
                return this;
            }

            public ModelTestParam<TModel>[] ToArray() => _params.ToArray();
        }
        
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
        public void ParseCorrectly(ModelTestParam<TModel> param)
        {
            Output.WriteLine("Parsing JSON: " + param.Description);
            Output.WriteLine(param.Json);
            
            var parsed = JsonSerializer.Deserialize<TModel>(param.Json);

            var expected = param.ModifyExpected(Expected);
            
            Assert.NotNull(expected);
            Assert.NotNull(parsed);
            Compare(expected, parsed);
        }
    }
}
