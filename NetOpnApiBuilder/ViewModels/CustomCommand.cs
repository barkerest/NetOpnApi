using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using Microsoft.Extensions.Logging;
using NetOpnApi;
using NetOpnApiBuilder.Enums;
using NetOpnApiBuilder.Models;

namespace NetOpnApiBuilder.ViewModels
{
    public class CustomCommand : ICommandWithResponseAndParameterSet<JsonElement>
    {
        private readonly string[]                   _urlKeys;
        private readonly Dictionary<string, string> _urlParams;
        private readonly Dictionary<string, string> _queryMap;
        private readonly Dictionary<string, string> _queryParams;

        public CustomCommand(ApiCommand cmd)
        {
            Module     = cmd.Controller.Module.ApiName;
            Controller = cmd.Controller.ApiName;
            Command    = cmd.ApiName;
            UsePost    = cmd.UsePost;

            if (cmd.ResponseBodyDataType.HasValue &&
                (cmd.ResponseBodyDataType.Value & ApiDataType.ArrayOfStrings) == ApiDataType.ArrayOfStrings)
            {
                ResponseRootElementValueKind = JsonValueKind.Array;
            }
            else
            {
                ResponseRootElementValueKind = JsonValueKind.Object;
            }

            ResponseRootElementName = cmd.ResponseBodyPropertyName;

            if (cmd.UrlParams.Any())
            {
                _urlKeys   = cmd.UrlParams.Select(x => x.ClrName).ToArray();
                _urlParams = cmd.UrlParams.ToDictionary(x => x.ClrName, _ => (string) null);
            }
            else
            {
                _urlKeys   = null;
                _urlParams = null;
            }

            if (cmd.QueryParams.Any())
            {
                _queryParams = cmd.QueryParams.ToDictionary(x => x.ClrName, _ => (string) null);
                _queryMap    = cmd.QueryParams.ToDictionary(x => x.ClrName, x => x.ApiName);
            }
            else
            {
                _queryMap    = null;
                _queryParams = null;
            }
        }

        public string        Module                       { get; }
        public string        Controller                   { get; }
        public string        Command                      { get; }
        public IDeviceConfig Config                       { get; set; }
        public ILogger       Logger                       { get; set; }
        public bool          UsePost                      { get; }
        public string        ResponseRootElementName      { get; }
        public JsonValueKind ResponseRootElementValueKind { get; }
        public JsonElement   Response                     { get; set; }

        public IReadOnlyList<string> GetUrlParameters()
        {
            if (_urlKeys is null) return null;

            var ret = new List<string>();
            foreach (var key in _urlKeys)
            {
                var v = _urlParams[key];
                if (v is null) break;
                ret.Add(v);
            }

            return ret;
        }

        public IReadOnlyList<KeyValuePair<string, string>> GetQueryParameters()
        {
            return _queryMap
                   ?.Select(x => new KeyValuePair<string, string>(x.Value, _queryParams[x.Key]))
                   .Where(x => !(x.Value is null))
                   .ToArray();
        }

        public object GetRequestPayload()
        {
            throw new NotImplementedException();
        }

        public Type GetRequestPayloadDataType()
        {
            throw new NotImplementedException();
        }
    }
}
