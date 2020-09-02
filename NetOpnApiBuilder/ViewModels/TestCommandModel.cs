using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Text.Json;
using NetOpnApiBuilder.Enums;
using NetOpnApiBuilder.Models;

namespace NetOpnApiBuilder.ViewModels
{
    public class TestCommandModel : IValidatableObject
    {
        private static string DefaultFor(ApiDataType type)
        {
            if ((type & ApiDataType.ArrayOfStrings) == ApiDataType.ArrayOfStrings)
            {
                return "[\n  \n]";
            }
            
            if ((type & ApiDataType.DictionaryOfStrings) == ApiDataType.DictionaryOfStrings)
            {
                return "{\n  \n}";
            }

            return (type & ApiDataType.Object) switch
            {
                ApiDataType.String      => "\"\"",
                ApiDataType.Boolean     => "false",
                ApiDataType.Integer     => "0",
                ApiDataType.LongInteger => "0",
                ApiDataType.Double      => "0.0",
                ApiDataType.DateTime    => DateTime.Today.ToString("\"yyyy-MM-dd HH:mm:ss\""),
                ApiDataType.Guid        => $"{Guid.Empty}",
                ApiDataType.Object      => "{\n  \n}",
                _                       => ""
            };
        }
        
        public class Param
        {
            public string ClrName    { get; }
            public string FormName   { get; }
            public bool   Required   { get; }
            public string Value      { get; set; }
            public bool   IsUrlParam { get; }

            public ApiDataType DataType     { get; }
            public string      ErrorMessage { get; set; } = null;
            
            public Param(ApiUrlParam param)
            {
                IsUrlParam = true;
                ClrName    = param.ClrName;
                FormName   = $"up{param.ID}";
                Required   = !param.AllowNull;
                DataType   = param.DataType;
                Value      = "";
            }

            public Param(ApiQueryParam param)
            {
                IsUrlParam = false;
                ClrName    = param.ClrName;
                FormName   = $"qp{param.ID}";
                Required   = !param.AllowNull;
                DataType   = param.DataType;
                Value      = "";
            }
        }

        
        public TestCommandModel(ApiCommand command)
        {
            Command = command ?? throw new ArgumentNullException(nameof(command));
            Params = command.UrlParams.Select(x => new Param(x))
                             .Union(command.QueryParams.Select(x => new Param(x)))
                             .OrderBy(x => x.ClrName)
                             .ThenBy(x => x.FormName)
                             .ToArray();

            if (command.UsePost && command.PostBodyDataType.HasValue)
            {
                UseJsonBody = true;
                JsonBody    = DefaultFor(command.PostBodyDataType.Value);
            }
            else
            {
                UseJsonBody = false;
                JsonBody    = null;
            }
            
        }

        public ApiCommand Command { get; }
        
        public IReadOnlyList<Param> Params { get; }

        public bool UseJsonBody { get; }
        
        public string JsonBody { get; set; }

        
        public bool TestRun { get; set; } = false;
        
        public string TestResponse { get; set; }
        
        public string TestError { get; set; }

        
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (!string.IsNullOrEmpty(JsonBody))
            {
                var isValid = true;
                
                try
                {
                    JsonDocument.Parse(JsonBody);
                }
                catch (JsonException)
                {
                    isValid = false;
                }

                if (!isValid)
                {
                    yield return new ValidationResult("must be a valid JSON value or blank", new[] {nameof(JsonBody)});
                }
            }
            
            foreach (var param in Params)
            {
                if (param.Required &&
                    string.IsNullOrEmpty(param.Value))
                {
                    param.ErrorMessage = "this parameter is required";
                    yield return new ValidationResult("this parameter is required", new[] {param.FormName});
                }
                else
                {
                    param.ErrorMessage = null;
                }
            }
        }
    }
}
