using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;
using NetOpnApi.JsonConverters;

namespace NetOpnApi.Tests
{
    internal static class PropHelper
    {
        public static T SetPropValue<T>(this PropertyInfo prop, T model, object value)
        {
            prop.SetValue(model, value);
            return model;
        }
    }

    public class ModelTestParam<T>
    {
        public string Description { get; }
        public string Json        { get; }

        public Func<T, T> ModifyExpected { get; }

        private static T NoChanges(T model)
        {
            return model;
        }

        public ModelTestParam(string d, string j, Func<T, T> m = null)
        {
            Description    = d;
            Json           = j;
            ModifyExpected = m ?? NoChanges;
        }

        public override string ToString() => Description;

        public static ModelTestParam<T> CreateExplicit(string defaultJson, Expression<Func<T, object>> property, object valueForJson, object expectedModelValue)
        {
            var mex = property.Body as MemberExpression;
            if (mex is null &&
                property.Body is UnaryExpression uex &&
                (uex.NodeType == ExpressionType.Convert || uex.NodeType == ExpressionType.ConvertChecked))
            {
                mex = uex.Operand as MemberExpression;
            }

            if (mex is null) throw new ArgumentException("must be a property selecting expression", nameof(property));

            var propInfo     = (PropertyInfo) mex.Member;
            var jsonPropName = propInfo.GetCustomAttribute<JsonPropertyNameAttribute>();
            var jsonName     = jsonPropName?.Name ?? propInfo.Name;
            var propName     = propInfo.Name;
            var dict         = JsonSerializer.Deserialize<Dictionary<string, object>>(defaultJson);

            dict[jsonName] = valueForJson;
            return new ModelTestParam<T>(
                $"{propName} = {JsonSerializer.Serialize(dict[jsonName])}",
                JsonSerializer.Serialize(dict),
                m => propInfo.SetPropValue(m, expectedModelValue)
            );
        }

        public static IEnumerable<ModelTestParam<T>> CreateForProperty(string defaultJson, Expression<Func<T, string>> property)
        {
            var propInfo     = (PropertyInfo) ((MemberExpression) property.Body).Member;
            var jsonPropName = propInfo.GetCustomAttribute<JsonPropertyNameAttribute>();
            var jsonName     = jsonPropName?.Name ?? propInfo.Name;
            var propName     = propInfo.Name;
            var dict         = JsonSerializer.Deserialize<Dictionary<string, object>>(defaultJson);

            // test 1: "Hello World"
            dict[jsonName] = "Hello World";
            yield return new ModelTestParam<T>(
                $"{propName} = {JsonSerializer.Serialize(dict[jsonName])}",
                JsonSerializer.Serialize(dict),
                m => propInfo.SetPropValue(m, "Hello World")
            );

            // test 2: "   "
            dict[jsonName] = "   ";
            yield return new ModelTestParam<T>(
                $"{propName} = {JsonSerializer.Serialize(dict[jsonName])}",
                JsonSerializer.Serialize(dict),
                m => propInfo.SetPropValue(m, "   ")
            );

            // test 3: ""
            dict[jsonName] = "";
            yield return new ModelTestParam<T>(
                $"{propName} = {JsonSerializer.Serialize(dict[jsonName])}",
                JsonSerializer.Serialize(dict),
                m => propInfo.SetPropValue(m, "")
            );

            // test 4: null
            dict[jsonName] = null;
            yield return new ModelTestParam<T>(
                $"{propName} = {JsonSerializer.Serialize(dict[jsonName])}",
                JsonSerializer.Serialize(dict),
                m => propInfo.SetPropValue(m, null)
            );
        }

        public static IEnumerable<ModelTestParam<T>> CreateForProperty(string defaultJson, Expression<Func<T, int>> property)
        {
            var propInfo     = (PropertyInfo) ((MemberExpression) property.Body).Member;
            var jsonPropName = propInfo.GetCustomAttribute<JsonPropertyNameAttribute>();
            var jsonName     = jsonPropName?.Name ?? propInfo.Name;
            var propName     = propInfo.Name;
            var dict         = JsonSerializer.Deserialize<Dictionary<string, object>>(defaultJson);

            // test 1: 0
            dict[jsonName] = 0;
            yield return new ModelTestParam<T>(
                $"{propName} = {JsonSerializer.Serialize(dict[jsonName])}",
                JsonSerializer.Serialize(dict),
                m => propInfo.SetPropValue(m, 0)
            );

            // test 2: 100
            dict[jsonName] = 100;
            yield return new ModelTestParam<T>(
                $"{propName} = {JsonSerializer.Serialize(dict[jsonName])}",
                JsonSerializer.Serialize(dict),
                m => propInfo.SetPropValue(m, 100)
            );

            // test 3: -100
            dict[jsonName] = -100;
            yield return new ModelTestParam<T>(
                $"{propName} = {JsonSerializer.Serialize(dict[jsonName])}",
                JsonSerializer.Serialize(dict),
                m => propInfo.SetPropValue(m, -100)
            );

            // test 4: "0"
            dict[jsonName] = "0";
            yield return new ModelTestParam<T>(
                $"{propName} = {JsonSerializer.Serialize(dict[jsonName])}",
                JsonSerializer.Serialize(dict),
                m => propInfo.SetPropValue(m, 0)
            );

            // test 5: "100"
            dict[jsonName] = "100";
            yield return new ModelTestParam<T>(
                $"{propName} = {JsonSerializer.Serialize(dict[jsonName])}",
                JsonSerializer.Serialize(dict),
                m => propInfo.SetPropValue(m, 100)
            );

            // test 6: "-100"
            dict[jsonName] = "-100";
            yield return new ModelTestParam<T>(
                $"{propName} = {JsonSerializer.Serialize(dict[jsonName])}",
                JsonSerializer.Serialize(dict),
                m => propInfo.SetPropValue(m, -100)
            );

            // test 7: 123.45
            dict[jsonName] = 123.45;
            yield return new ModelTestParam<T>(
                $"{propName} = {JsonSerializer.Serialize(dict[jsonName])}",
                JsonSerializer.Serialize(dict),
                m => propInfo.SetPropValue(m, 123)
            );

            // test 8: min value
            dict[jsonName] = int.MinValue;
            yield return new ModelTestParam<T>(
                $"{propName} = {JsonSerializer.Serialize(dict[jsonName])}",
                JsonSerializer.Serialize(dict),
                m => propInfo.SetPropValue(m, int.MinValue)
            );

            // test 9: max value
            dict[jsonName] = int.MaxValue;
            yield return new ModelTestParam<T>(
                $"{propName} = {JsonSerializer.Serialize(dict[jsonName])}",
                JsonSerializer.Serialize(dict),
                m => propInfo.SetPropValue(m, int.MaxValue)
            );
        }

        public static IEnumerable<ModelTestParam<T>> CreateForProperty(string defaultJson, Expression<Func<T, long>> property)
        {
            var propInfo     = (PropertyInfo) ((MemberExpression) property.Body).Member;
            var jsonPropName = propInfo.GetCustomAttribute<JsonPropertyNameAttribute>();
            var jsonName     = jsonPropName?.Name ?? propInfo.Name;
            var propName     = propInfo.Name;
            var dict         = JsonSerializer.Deserialize<Dictionary<string, object>>(defaultJson);

            // test 1: 0
            dict[jsonName] = 0;
            yield return new ModelTestParam<T>(
                $"{propName} = {JsonSerializer.Serialize(dict[jsonName])}",
                JsonSerializer.Serialize(dict),
                m => propInfo.SetPropValue(m, 0)
            );

            // test 2: 100
            dict[jsonName] = 100;
            yield return new ModelTestParam<T>(
                $"{propName} = {JsonSerializer.Serialize(dict[jsonName])}",
                JsonSerializer.Serialize(dict),
                m => propInfo.SetPropValue(m, 100)
            );

            // test 3: -100
            dict[jsonName] = -100;
            yield return new ModelTestParam<T>(
                $"{propName} = {JsonSerializer.Serialize(dict[jsonName])}",
                JsonSerializer.Serialize(dict),
                m => propInfo.SetPropValue(m, -100)
            );

            // test 4: "0"
            dict[jsonName] = "0";
            yield return new ModelTestParam<T>(
                $"{propName} = {JsonSerializer.Serialize(dict[jsonName])}",
                JsonSerializer.Serialize(dict),
                m => propInfo.SetPropValue(m, 0)
            );

            // test 5: "100"
            dict[jsonName] = "100";
            yield return new ModelTestParam<T>(
                $"{propName} = {JsonSerializer.Serialize(dict[jsonName])}",
                JsonSerializer.Serialize(dict),
                m => propInfo.SetPropValue(m, 100)
            );

            // test 6: "-100"
            dict[jsonName] = "-100";
            yield return new ModelTestParam<T>(
                $"{propName} = {JsonSerializer.Serialize(dict[jsonName])}",
                JsonSerializer.Serialize(dict),
                m => propInfo.SetPropValue(m, -100)
            );

            // test 7: 123.45
            dict[jsonName] = 123.45;
            yield return new ModelTestParam<T>(
                $"{propName} = {JsonSerializer.Serialize(dict[jsonName])}",
                JsonSerializer.Serialize(dict),
                m => propInfo.SetPropValue(m, 123)
            );

            // test 8: min value
            dict[jsonName] = long.MinValue;
            yield return new ModelTestParam<T>(
                $"{propName} = {JsonSerializer.Serialize(dict[jsonName])}",
                JsonSerializer.Serialize(dict),
                m => propInfo.SetPropValue(m, long.MinValue)
            );

            // test 9: max value
            dict[jsonName] = long.MaxValue;
            yield return new ModelTestParam<T>(
                $"{propName} = {JsonSerializer.Serialize(dict[jsonName])}",
                JsonSerializer.Serialize(dict),
                m => propInfo.SetPropValue(m, long.MaxValue)
            );
        }

        public static IEnumerable<ModelTestParam<T>> CreateForProperty(string defaultJson, Expression<Func<T, double>> property)
        {
            var propInfo     = (PropertyInfo) ((MemberExpression) property.Body).Member;
            var jsonPropName = propInfo.GetCustomAttribute<JsonPropertyNameAttribute>();
            var jsonName     = jsonPropName?.Name ?? propInfo.Name;
            var propName     = propInfo.Name;
            var dict         = JsonSerializer.Deserialize<Dictionary<string, object>>(defaultJson);

            // test 1: 0
            dict[jsonName] = 0;
            yield return new ModelTestParam<T>(
                $"{propName} = {JsonSerializer.Serialize(dict[jsonName])}",
                JsonSerializer.Serialize(dict),
                m => propInfo.SetPropValue(m, 0.0)
            );

            // test 2: 100
            dict[jsonName] = 100;
            yield return new ModelTestParam<T>(
                $"{propName} = {JsonSerializer.Serialize(dict[jsonName])}",
                JsonSerializer.Serialize(dict),
                m => propInfo.SetPropValue(m, 100.0)
            );

            // test 3: -100
            dict[jsonName] = -100;
            yield return new ModelTestParam<T>(
                $"{propName} = {JsonSerializer.Serialize(dict[jsonName])}",
                JsonSerializer.Serialize(dict),
                m => propInfo.SetPropValue(m, -100.0)
            );

            // test 4: "0"
            dict[jsonName] = "0";
            yield return new ModelTestParam<T>(
                $"{propName} = {JsonSerializer.Serialize(dict[jsonName])}",
                JsonSerializer.Serialize(dict),
                m => propInfo.SetPropValue(m, 0.0)
            );

            // test 5: "100"
            dict[jsonName] = "100";
            yield return new ModelTestParam<T>(
                $"{propName} = {JsonSerializer.Serialize(dict[jsonName])}",
                JsonSerializer.Serialize(dict),
                m => propInfo.SetPropValue(m, 100.0)
            );

            // test 6: "-100"
            dict[jsonName] = "-100";
            yield return new ModelTestParam<T>(
                $"{propName} = {JsonSerializer.Serialize(dict[jsonName])}",
                JsonSerializer.Serialize(dict),
                m => propInfo.SetPropValue(m, -100.0)
            );

            // test 7: 123.45
            dict[jsonName] = 123.45;
            yield return new ModelTestParam<T>(
                $"{propName} = {JsonSerializer.Serialize(dict[jsonName])}",
                JsonSerializer.Serialize(dict),
                m => propInfo.SetPropValue(m, 123.45)
            );

            // test 8: 0.1
            dict[jsonName] = 0.1;
            yield return new ModelTestParam<T>(
                $"{propName} = {JsonSerializer.Serialize(dict[jsonName])}",
                JsonSerializer.Serialize(dict),
                m => propInfo.SetPropValue(m, 0.1)
            );

            // test 9: -0.1
            dict[jsonName] = -0.1;
            yield return new ModelTestParam<T>(
                $"{propName} = {JsonSerializer.Serialize(dict[jsonName])}",
                JsonSerializer.Serialize(dict),
                m => propInfo.SetPropValue(m, -0.1)
            );

            // test 10: "123.45"
            dict[jsonName] = "123.45";
            yield return new ModelTestParam<T>(
                $"{propName} = {JsonSerializer.Serialize(dict[jsonName])}",
                JsonSerializer.Serialize(dict),
                m => propInfo.SetPropValue(m, 123.45)
            );

            // test 11: "0.1"
            dict[jsonName] = "0.1";
            yield return new ModelTestParam<T>(
                $"{propName} = {JsonSerializer.Serialize(dict[jsonName])}",
                JsonSerializer.Serialize(dict),
                m => propInfo.SetPropValue(m, 0.1)
            );

            // test 12: "-0.1"
            dict[jsonName] = "-0.1";
            yield return new ModelTestParam<T>(
                $"{propName} = {JsonSerializer.Serialize(dict[jsonName])}",
                JsonSerializer.Serialize(dict),
                m => propInfo.SetPropValue(m, -0.1)
            );
        }

        public static IEnumerable<ModelTestParam<T>> CreateForProperty(string defaultJson, Expression<Func<T, bool>> property)
        {
            var propInfo     = (PropertyInfo) ((MemberExpression) property.Body).Member;
            var jsonPropName = propInfo.GetCustomAttribute<JsonPropertyNameAttribute>();
            var jsonName     = jsonPropName?.Name ?? propInfo.Name;
            var propName     = propInfo.Name;
            var dict         = JsonSerializer.Deserialize<Dictionary<string, object>>(defaultJson);

            // test 1: true
            dict[jsonName] = true;
            yield return new ModelTestParam<T>(
                $"{propName} = {JsonSerializer.Serialize(dict[jsonName])}",
                JsonSerializer.Serialize(dict),
                m => propInfo.SetPropValue(m, true)
            );

            // test 2: false
            dict[jsonName] = false;
            yield return new ModelTestParam<T>(
                $"{propName} = {JsonSerializer.Serialize(dict[jsonName])}",
                JsonSerializer.Serialize(dict),
                m => propInfo.SetPropValue(m, false)
            );

            // test 3: 1
            dict[jsonName] = 1;
            yield return new ModelTestParam<T>(
                $"{propName} = {JsonSerializer.Serialize(dict[jsonName])}",
                JsonSerializer.Serialize(dict),
                m => propInfo.SetPropValue(m, true)
            );

            // test 4: -1
            dict[jsonName] = -1;
            yield return new ModelTestParam<T>(
                $"{propName} = {JsonSerializer.Serialize(dict[jsonName])}",
                JsonSerializer.Serialize(dict),
                m => propInfo.SetPropValue(m, true)
            );

            // test 5: 101
            dict[jsonName] = 101;
            yield return new ModelTestParam<T>(
                $"{propName} = {JsonSerializer.Serialize(dict[jsonName])}",
                JsonSerializer.Serialize(dict),
                m => propInfo.SetPropValue(m, true)
            );

            // test 6: -101
            dict[jsonName] = -101;
            yield return new ModelTestParam<T>(
                $"{propName} = {JsonSerializer.Serialize(dict[jsonName])}",
                JsonSerializer.Serialize(dict),
                m => propInfo.SetPropValue(m, true)
            );

            // test 7: 0
            dict[jsonName] = 0;
            yield return new ModelTestParam<T>(
                $"{propName} = {JsonSerializer.Serialize(dict[jsonName])}",
                JsonSerializer.Serialize(dict),
                m => propInfo.SetPropValue(m, false)
            );

            foreach (var v in AlwaysBool.TrueValues)
            {
                dict[jsonName] = v;
                yield return new ModelTestParam<T>(
                    $"{propName} = {JsonSerializer.Serialize(dict[jsonName])}",
                    JsonSerializer.Serialize(dict),
                    m => propInfo.SetPropValue(m, true)
                );
            }

            foreach (var v in AlwaysBool.FalseValues)
            {
                dict[jsonName] = v;
                yield return new ModelTestParam<T>(
                    $"{propName} = {JsonSerializer.Serialize(dict[jsonName])}",
                    JsonSerializer.Serialize(dict),
                    m => propInfo.SetPropValue(m, false)
                );
            }
        }

        public static IEnumerable<ModelTestParam<T>> CreateForProperty<TV>(string defaultJson, Expression<Func<T, IDictionary<string, TV>>> property, IDictionary<string, TV> nonEmptyValue)
        {
            var propInfo     = (PropertyInfo) ((MemberExpression) property.Body).Member;
            var jsonPropName = propInfo.GetCustomAttribute<JsonPropertyNameAttribute>();
            var jsonName     = jsonPropName?.Name ?? propInfo.Name;
            var propName     = propInfo.Name;
            var dict         = JsonSerializer.Deserialize<Dictionary<string, object>>(defaultJson);

            // test 1: non empty value
            dict[jsonName] = nonEmptyValue;
            yield return new ModelTestParam<T>(
                $"{propName} = non-empty dictionary",
                JsonSerializer.Serialize(dict),
                m => propInfo.SetPropValue(m, nonEmptyValue)
            );

            // test 2: null
            dict[jsonName] = null;
            yield return new ModelTestParam<T>(
                $"{propName} = {JsonSerializer.Serialize(dict[jsonName])}",
                JsonSerializer.Serialize(dict),
                m => propInfo.SetPropValue(m, null)
            );

            // test 2: ""    (pfSense did this a lot)
            dict[jsonName] = "";
            yield return new ModelTestParam<T>(
                $"{propName} = {JsonSerializer.Serialize(dict[jsonName])}",
                JsonSerializer.Serialize(dict),
                m => propInfo.SetPropValue(m, null)
            );

            // test 3: []    (empty PHP array)
            dict[jsonName] = new string[0];
            yield return new ModelTestParam<T>(
                $"{propName} = {JsonSerializer.Serialize(dict[jsonName])}",
                JsonSerializer.Serialize(dict),
                m => propInfo.SetPropValue(m, new Dictionary<string, TV>())
            );

            // test 4: {}
            dict[jsonName] = new string[0];
            yield return new ModelTestParam<T>(
                $"{propName} = {JsonSerializer.Serialize(dict[jsonName])}",
                JsonSerializer.Serialize(dict),
                m => propInfo.SetPropValue(m, new Dictionary<string, TV>())
            );
        }

        public static IEnumerable<ModelTestParam<T>> CreateForProperty<TV>(string defaultJson, Expression<Func<T, IReadOnlyList<TV>>> property, IReadOnlyList<TV> nonEmptyValue)
        {
            var propInfo     = (PropertyInfo) ((MemberExpression) property.Body).Member;
            var jsonPropName = propInfo.GetCustomAttribute<JsonPropertyNameAttribute>();
            var jsonName     = jsonPropName?.Name ?? propInfo.Name;
            var propName     = propInfo.Name;
            var dict         = JsonSerializer.Deserialize<Dictionary<string, object>>(defaultJson);

            // test 1: non-empty
            dict[jsonName] = nonEmptyValue;
            yield return new ModelTestParam<T>(
                $"{propName} = non-empty list",
                JsonSerializer.Serialize(dict),
                m => propInfo.SetPropValue(m, nonEmptyValue)
            );

            // test 2: null
            dict[jsonName] = null;
            yield return new ModelTestParam<T>(
                $"{propName} = {JsonSerializer.Serialize(dict[jsonName])}",
                JsonSerializer.Serialize(dict),
                m => propInfo.SetPropValue(m, null)
            );

            // test 2: ""    (pfSense did this a lot)
            dict[jsonName] = "";
            yield return new ModelTestParam<T>(
                $"{propName} = {JsonSerializer.Serialize(dict[jsonName])}",
                JsonSerializer.Serialize(dict),
                m => propInfo.SetPropValue(m, null)
            );

            // test 3: []    (empty PHP array)
            dict[jsonName] = new TV[0];
            yield return new ModelTestParam<T>(
                $"{propName} = {JsonSerializer.Serialize(dict[jsonName])}",
                JsonSerializer.Serialize(dict),
                m => propInfo.SetPropValue(m, new TV[0])
            );
        }


        public static IEnumerable<ModelTestParam<T>> CreateForProperty(string defaultJson, Expression<Func<T, Guid>> property)
        {
            var propInfo     = (PropertyInfo) ((MemberExpression) property.Body).Member;
            var jsonPropName = propInfo.GetCustomAttribute<JsonPropertyNameAttribute>();
            var jsonName     = jsonPropName?.Name ?? propInfo.Name;
            var propName     = propInfo.Name;
            var dict         = JsonSerializer.Deserialize<Dictionary<string, object>>(defaultJson);

            var g = Guid.NewGuid();
            // test 1: g
            dict[jsonName] = g;
            yield return new ModelTestParam<T>(
                $"{propName} = {JsonSerializer.Serialize(dict[jsonName])}",
                JsonSerializer.Serialize(dict),
                m => propInfo.SetPropValue(m, g)
            );

            // test 2: Guid.Empty
            dict[jsonName] = Guid.Empty;
            yield return new ModelTestParam<T>(
                $"{propName} = {JsonSerializer.Serialize(dict[jsonName])}",
                JsonSerializer.Serialize(dict),
                m => propInfo.SetPropValue(m, Guid.Empty)
            );

            // test 3: null
            dict[jsonName] = null;
            yield return new ModelTestParam<T>(
                $"{propName} = {JsonSerializer.Serialize(dict[jsonName])}",
                JsonSerializer.Serialize(dict),
                m => propInfo.SetPropValue(m, Guid.Empty)
            );

            // test 4: 0
            dict[jsonName] = 0;
            yield return new ModelTestParam<T>(
                $"{propName} = {JsonSerializer.Serialize(dict[jsonName])}",
                JsonSerializer.Serialize(dict),
                m => propInfo.SetPropValue(m, Guid.Empty)
            );

            // test 5: "0"
            dict[jsonName] = "0";
            yield return new ModelTestParam<T>(
                $"{propName} = {JsonSerializer.Serialize(dict[jsonName])}",
                JsonSerializer.Serialize(dict),
                m => propInfo.SetPropValue(m, Guid.Empty)
            );

            // test 6: ""
            dict[jsonName] = "";
            yield return new ModelTestParam<T>(
                $"{propName} = {JsonSerializer.Serialize(dict[jsonName])}",
                JsonSerializer.Serialize(dict),
                m => propInfo.SetPropValue(m, Guid.Empty)
            );

            // test 7: g("N")
            dict[jsonName] = g.ToString("N");
            yield return new ModelTestParam<T>(
                $"{propName} = {JsonSerializer.Serialize(dict[jsonName])}",
                JsonSerializer.Serialize(dict),
                m => propInfo.SetPropValue(m, g)
            );

            // test 8: g("B")
            dict[jsonName] = g.ToString("B");
            yield return new ModelTestParam<T>(
                $"{propName} = {JsonSerializer.Serialize(dict[jsonName])}",
                JsonSerializer.Serialize(dict),
                m => propInfo.SetPropValue(m, g)
            );

            // test 9: g("P")
            dict[jsonName] = g.ToString("P");
            yield return new ModelTestParam<T>(
                $"{propName} = {JsonSerializer.Serialize(dict[jsonName])}",
                JsonSerializer.Serialize(dict),
                m => propInfo.SetPropValue(m, g)
            );

            // test 10: g("X")
            dict[jsonName] = g.ToString("X");
            yield return new ModelTestParam<T>(
                $"{propName} = {JsonSerializer.Serialize(dict[jsonName])}",
                JsonSerializer.Serialize(dict),
                m => propInfo.SetPropValue(m, g)
            );
        }

        public static IEnumerable<ModelTestParam<T>> CreateForProperty(string defaultJson, Expression<Func<T, DateTime>> property)
        {
            var propInfo     = (PropertyInfo) ((MemberExpression) property.Body).Member;
            var jsonPropName = propInfo.GetCustomAttribute<JsonPropertyNameAttribute>();
            var jsonName     = jsonPropName?.Name ?? propInfo.Name;
            var propName     = propInfo.Name;
            var dict         = JsonSerializer.Deserialize<Dictionary<string, object>>(defaultJson);

            var msRes = new DateTime(2015, 9, 15, 19, 45, 30, 25, DateTimeKind.Utc);
            var sRes  = new DateTime(2015, 9, 15, 19, 45, 30, DateTimeKind.Utc);
            var mRes  = new DateTime(2015, 9, 15, 19, 45, 0, DateTimeKind.Utc);
            var dRes  = new DateTime(2015, 9, 15, 0, 0, 0, DateTimeKind.Utc);
            var zRes  = new DateTime(2015, 9, 15, 19, 45, 0, DateTimeKind.Local);

            // test 1: yyyy-mm-dd
            dict[jsonName] = dRes.ToString("yyyy-MM-dd");
            yield return new ModelTestParam<T>(
                $"{propName} = {JsonSerializer.Serialize(dict[jsonName])}",
                JsonSerializer.Serialize(dict),
                m => propInfo.SetPropValue(m, dRes)
            );

            // test 2: yyyy-m-d
            dict[jsonName] = dRes.ToString("yyyy-M-d");
            yield return new ModelTestParam<T>(
                $"{propName} = {JsonSerializer.Serialize(dict[jsonName])}",
                JsonSerializer.Serialize(dict),
                m => propInfo.SetPropValue(m, dRes)
            );

            // test 3: d/m/yyyy
            dict[jsonName] = dRes.ToString("d/M/yyyy");
            yield return new ModelTestParam<T>(
                $"{propName} = {JsonSerializer.Serialize(dict[jsonName])}",
                JsonSerializer.Serialize(dict),
                m => propInfo.SetPropValue(m, dRes)
            );

            // test 4: d mmm yyyy
            dict[jsonName] = dRes.ToString("d MMM yyyy");
            yield return new ModelTestParam<T>(
                $"{propName} = {JsonSerializer.Serialize(dict[jsonName])}",
                JsonSerializer.Serialize(dict),
                m => propInfo.SetPropValue(m, dRes)
            );

            // test 5: d mmm, yyyy
            dict[jsonName] = dRes.ToString("d MMM, yyyy");
            yield return new ModelTestParam<T>(
                $"{propName} = {JsonSerializer.Serialize(dict[jsonName])}",
                JsonSerializer.Serialize(dict),
                m => propInfo.SetPropValue(m, dRes)
            );

            // test 6: mmmm d yyyy
            dict[jsonName] = dRes.ToString("MMMM d yyyy");
            yield return new ModelTestParam<T>(
                $"{propName} = {JsonSerializer.Serialize(dict[jsonName])}",
                JsonSerializer.Serialize(dict),
                m => propInfo.SetPropValue(m, dRes)
            );

            // test 7: mmmm dd, yyyy
            dict[jsonName] = dRes.ToString("MMMM dd, yyyy");
            yield return new ModelTestParam<T>(
                $"{propName} = {JsonSerializer.Serialize(dict[jsonName])}",
                JsonSerializer.Serialize(dict),
                m => propInfo.SetPropValue(m, dRes)
            );

            // Time formats are common between all date formats, so we don't have to test every combination.

            // test 8: yyyy-mm-dd hh:mm:ss.fff
            dict[jsonName] = msRes.ToString("yyyy-MM-dd HH:mm:ss.fff");
            yield return new ModelTestParam<T>(
                $"{propName} = {JsonSerializer.Serialize(dict[jsonName])}",
                JsonSerializer.Serialize(dict),
                m => propInfo.SetPropValue(m, msRes)
            );

            // test 9: yyyy-mm-dd h:mm:ss.fff tt
            dict[jsonName] = msRes.ToString("yyyy-MM-dd h:mm:ss.fff tt");
            yield return new ModelTestParam<T>(
                $"{propName} = {JsonSerializer.Serialize(dict[jsonName])}",
                JsonSerializer.Serialize(dict),
                m => propInfo.SetPropValue(m, msRes)
            );

            // test 10: yyyy-mm-dd hh:mm:ss zzz
            dict[jsonName] = zRes.ToString("yyyy-MM-dd HH:mm:ss zzz");
            yield return new ModelTestParam<T>(
                $"{propName} = {JsonSerializer.Serialize(dict[jsonName])}",
                JsonSerializer.Serialize(dict),
                m => propInfo.SetPropValue(m, zRes.ToUniversalTime())
            );

            // test 11: yyyy-mm-dd hh:mm:ss
            dict[jsonName] = sRes.ToString("yyyy-MM-dd HH:mm:ss");
            yield return new ModelTestParam<T>(
                $"{propName} = {JsonSerializer.Serialize(dict[jsonName])}",
                JsonSerializer.Serialize(dict),
                m => propInfo.SetPropValue(m, sRes)
            );

            // test 12: yyyy-mm-dd hh:mm
            dict[jsonName] = mRes.ToString("yyyy-MM-dd HH:mm");
            yield return new ModelTestParam<T>(
                $"{propName} = {JsonSerializer.Serialize(dict[jsonName])}",
                JsonSerializer.Serialize(dict),
                m => propInfo.SetPropValue(m, mRes)
            );

            // test 13: yyyy-mm-dd hh:mm tt
            dict[jsonName] = mRes.ToString("yyyy-MM-dd hh:mm tt");
            yield return new ModelTestParam<T>(
                $"{propName} = {JsonSerializer.Serialize(dict[jsonName])}",
                JsonSerializer.Serialize(dict),
                m => propInfo.SetPropValue(m, mRes)
            );

            // test 14: yyyy-mm-dd hh:mm tt zzz
            dict[jsonName] = zRes.ToString("yyyy-MM-dd hh:mm tt zzz");
            yield return new ModelTestParam<T>(
                $"{propName} = {JsonSerializer.Serialize(dict[jsonName])}",
                JsonSerializer.Serialize(dict),
                m => propInfo.SetPropValue(m, zRes.ToUniversalTime())
            );

            // test 15: d mmm hh:mm:ss yyyy
            dict[jsonName] = sRes.ToString("d MMM HH:mm:ss yyyy");
            yield return new ModelTestParam<T>(
                $"{propName} = {JsonSerializer.Serialize(dict[jsonName])}",
                JsonSerializer.Serialize(dict),
                m => propInfo.SetPropValue(m, sRes)
            );

            // test 16: mmm d hh:mm:ss yyyy
            dict[jsonName] = sRes.ToString("MMM d HH:mm:ss yyyy");
            yield return new ModelTestParam<T>(
                $"{propName} = {JsonSerializer.Serialize(dict[jsonName])}",
                JsonSerializer.Serialize(dict),
                m => propInfo.SetPropValue(m, sRes)
            );
        }
    }
}
