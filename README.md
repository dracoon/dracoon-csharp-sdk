[![Build Status](https://travis-ci.com/dracoon/dracoon-csharp-sdk.svg?branch=master)](https://travis-ci.com/dracoon/)
[![GitHub license](https://img.shields.io/github/license/dracoon/dracoon-csharp-sdk.svg)](http://www.apache.org/licenses/LICENSE-2.0)
[![NuGet](https://img.shields.io/nuget/v/Dracoon.Sdk.svg)](https://www.nuget.org/packages/Dracoon.Sdk/)
[![NuGet downloads](https://img.shields.io/nuget/dt/Dracoon.Sdk.svg?label=nuget-downloads&colorB=F03C20)](https://www.nuget.org/packages/Dracoon.Sdk/)
![GitHub issues](https://img.shields.io/github/issues-raw/dracoon/dracoon-csharp-sdk.svg)
# DRACOON C# SDK

A library to access the DRACOON REST API.

## Setup

#### Minimum Requirements

.NET Standard 2.0
API version: 4.33.0

#### Download

##### NuGet
In nuget, you can find the DRACOON SDK [here](https://www.nuget.org/packages/Dracoon.Sdk/).

If you are using NuGet with package management "Packages.config", then edit your project's "packages.config" and add this to the packages section:
```xml
<package id="Dracoon.Sdk" version="3.1.0" />
```
If you are using Visual Studio 2017 (or higher) and you are using NuGet with package management "PackageReference" then edit your .csproj file and add this to the package dependency group:
```xml
<PackageReference Include="Dracoon.Sdk" Version="3.1.0" />
```

Note that you also need to include the following dependencies:
1. Bouncy Castle Provider Portable (v1.9.0): https://www.nuget.org/packages/Portable.BouncyCastle/
2. Dracoon Crypto SDK (v3.0.0): https://www.nuget.org/packages/Dracoon.Crypto.Sdk/
3. NewtonSoft.Json (v13.0.1): https://www.nuget.org/packages/Newtonsoft.Json/
4. RestSharp (v106.15.0): https://www.nuget.org/packages/RestSharp/

## Example

A full example of the SDK usage can be found [here](DracoonSdkExample/DracoonExamples.cs).\
A full example of the OAuth usage can be found [here](DracoonSdkExample/OAuthExamples.cs).

The following example shows how simple the SDK can be used. It shows how to get all root rooms.

```c#
DracoonAuth auth = new DracoonAuth("access-token");

DracoonClient client = new DracoonClient(new Uri("https://dracoon.team"), auth);

NodeList resultList = client.Nodes.GetNodes();
foreach (Node currentNode in resultList.Items) {
	Console.WriteLine("NodeId: " + currentNode.Id + "; NodeName: " + currentNode.Name);
}
```

## Documentation

Documentation of all public classes and methods are provided through the standard `<summary></summary>` xml tags. 
The easiest way to view these is through Visual Studio's built in "Object Browser" (VIEW -> Object Browser, or CTRL+W, J).

## Contribution

If you would like to contribute code, fork the repository and send a pull request. We don't use the GitHub Flow, so please create a feature branch of the develop branch and make your changes there.

## Copyright and License

Copyright ©2021 Dracoon GmbH. All rights reserved.

Licensed under the Apache License, verison 2.0 (the "License"); you may not use this file except in compliance with the License. You may optain a copy of the License at

http://www.apache.org/licenses/LICENSE-2.0

Unless required by applicable law or agreed to in writing, software distributed under the License is
distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or
implied. See the License for the specific language governing permissions and limitations under the
License.