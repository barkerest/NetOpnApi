using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Logging;
using Xunit;
using Xunit.Abstractions;

namespace NetOpnApi.Tests
{
    public abstract class BaseCommandFactTest<TCommand> : ILogger where TCommand : class,ICommand,new()
    {
        private readonly ITestOutputHelper _output;

        protected virtual SpecialTest IsSpecialTest { get; } = SpecialTest.None;
        protected TCommand Command { get; }

        private Type CommandType { get; } = typeof(TCommand);

        protected BaseCommandFactTest(ITestOutputHelper output)
        {
            _output = output ?? throw new ArgumentNullException(nameof(output));
            if (!OpnSenseDevice.Instance.Configured) throw new InvalidOperationException("Device is not configured.");
            Command = new TCommand {Config = OpnSenseDevice.Instance, Logger = this};
        }
        
        void ILogger.Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
            => _output.WriteLine(formatter(state, exception));

        bool ILogger.IsEnabled(LogLevel logLevel) => true;

        IDisposable ILogger.BeginScope<TState>(TState state) => null;

        protected virtual void SetParameters()
        {
            
        }

        protected virtual void CheckResponse()
        {
            
        }

        [SkippableFact()]
        public void Execute()
        {
            Skip.If(OpnSenseDevice.Instance.SpecialTest != SpecialTest.None && IsSpecialTest != OpnSenseDevice.Instance.SpecialTest);
            Skip.If(OpnSenseDevice.Instance.SpecialTest == SpecialTest.None && IsSpecialTest != SpecialTest.None);

            var type = IsSpecialTest == SpecialTest.None ? "normal" : "special";
            _output.WriteLine($"Running {type} test: {CommandType}");
            
            SetParameters();
            Command.Execute();
            CheckResponse();
        }
        
    }
    
    public abstract class BaseCommandTheoryTest<TCommand, TParam, TParamList> : ILogger 
        where TCommand : class, ICommand, new()
        where TParamList : class, IEnumerable<TParam>, new()
    {
        private readonly ITestOutputHelper _output;

        protected virtual SpecialTest IsSpecialTest { get; } = SpecialTest.None;
        
        protected TCommand Command { get; }

        private Type CommandType { get; } = typeof(TCommand);

        protected BaseCommandTheoryTest(ITestOutputHelper output)
        {
            _output = output ?? throw new ArgumentNullException(nameof(output));
            if (!OpnSenseDevice.Instance.Configured) throw new InvalidOperationException("Device is not configured.");
            Command = new TCommand {Config = OpnSenseDevice.Instance, Logger = this};
        }
        
        void ILogger.Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
            => _output.WriteLine(formatter(state, exception));

        bool ILogger.IsEnabled(LogLevel logLevel) => true;

        IDisposable ILogger.BeginScope<TState>(TState state) => null;

        protected virtual void SetParameters(TParam args)
        {
            
        }

        protected virtual void CheckResponse(TParam args)
        {
            
        }

        public static IEnumerable<object[]> Data => new TParamList().Select(x => new object[]{x});
        
        [SkippableTheory()]
        [MemberData(nameof(Data))]
        public void Execute(TParam args)
        {
            Skip.If(OpnSenseDevice.Instance.SpecialTest != SpecialTest.None && IsSpecialTest != OpnSenseDevice.Instance.SpecialTest);
            Skip.If(OpnSenseDevice.Instance.SpecialTest == SpecialTest.None && IsSpecialTest != SpecialTest.None);

            var type = IsSpecialTest == SpecialTest.None ? "normal" : "special";
            _output.WriteLine($"Running {type} test: {CommandType}");
            
            SetParameters(args);
            Command.Execute();
            CheckResponse(args);
        }
    }
}
