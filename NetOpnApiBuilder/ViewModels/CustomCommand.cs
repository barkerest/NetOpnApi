using System;
using System.Collections.Generic;
using System.Text.Json;
using Microsoft.Extensions.Logging;
using NetOpnApi;
using NetOpnApiBuilder.Enums;
using NetOpnApiBuilder.Models;

namespace NetOpnApiBuilder.ViewModels
{
    public class CustomCommand : ICommandWithResponseAndParameterSet<JsonElement>
    {
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
            
            
        }
        
        public string        Module                       { get; }
        public string        Controller                   { get; }
        public string        Command                      { get; }
        public IDeviceConfig Config                       { get; set; }
        public ILogger       Logger                       { get; set; }
        public bool          UsePost                      { get; }
        public string        ResponseRootElementName      { get; set; }
        public JsonValueKind ResponseRootElementValueKind { get; }
        public JsonElement   Response                     { get; set; }
        
        
        
        public IReadOnlyList<string>                       GetUrlParameters()
        {
            throw new NotImplementedException();
        }

        public IReadOnlyList<KeyValuePair<string, string>> GetQueryParameters()
        {
            throw new NotImplementedException();
        }

        public object                                      GetRequestPayload()
        {
            throw new NotImplementedException();
        }

        public Type                                        GetRequestPayloadDataType()
        {
            throw new NotImplementedException();
        }
    }
}
