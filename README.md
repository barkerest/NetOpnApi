# NetOpnApi

An interface library for the OPNsense API.


## Usage

```sh
dotnet add package NetOpnApi
```

```c#
// Create a class that implements the IDeviceConfig interface.
class OpnSenseDevice : IDeviceConfig
{
    public OpnSenseDevice(int id)
    {
        // load settings from configuration or database.
        ...
    }
    ...
}

// Create an instance your configuration.
IDeviceConfig config = new OpnSenseDevice(12345);

// All commands are self-contained.
var command = new NetOpnApi.Commands.Core.System.Reboot();
// And they all need a configuration set ...
command.Config = config;
// before they can be executed.
command.Execute();

var command = new NetOpnApi.Commands.Core.Menu.Search();
command.Config = config;
// Commands also support logging.
command.Logger = SomeLoggerInstance;
// Some commands can take parameters.
command.ParameterSet.SearchTerm = "Reboot";
command.Execute();
// And many commands return a response.
var menuMatches = command.Response;

// Custom commands can be performed by implementing the ICommand interface.
public class MyCommand : ICommand
{
    ...
}

// Or implementing one of the generic ICommandWith* interfaces.
public class MyCommand : ICommandWithResponse<MyResponseType>
{
    ...
}

public class MyCommand : ICommandWithParameterSet<MyParameterSetType>
{
    ...
}

public class MyCommand : ICommandWithResponseAndParameterSet<MyResponseType, MyParameterSetType>
{
    ...
}
```


## License

Licensed under the [MIT License](https://opensource.org/licenses/MIT).

Copyright (C) 2020 Beau Barker

Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.