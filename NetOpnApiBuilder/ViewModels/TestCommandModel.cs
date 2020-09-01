using System;
using System.Net;
using NetOpnApiBuilder.Models;

namespace NetOpnApiBuilder.ViewModels
{
    public class TestCommandModel
    {
        public TestCommandModel(ApiCommand command)
        {
            Command = command ?? throw new ArgumentNullException(nameof(command));
        }

        public ApiCommand Command { get; }
        
        public bool TestRun { get; set; }
        
        public HttpStatusCode TestStatusCode { get; set; }
        
        public string TestResponse { get; set; }
        
        
        
    }
}
