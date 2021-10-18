![GitHub Workflow Status](https://img.shields.io/github/workflow/status/squideyes/basics/Deploy%20to%20NuGet?label=build)
![NuGet Version](https://img.shields.io/nuget/v/SquidEyes.Basics)
![Downloads](https://img.shields.io/nuget/dt/squideyes.basics)
![License](https://img.shields.io/github/license/squideyes/Basics)

**SquidEyes.Basics** is a somewhat quixotic collection of helper classes and extension methods.  The software includes a comprehensive set of unit-tests, and it has been open-sourced under a standard MIT license (see License.md for further details).  Even so, the code is mostly for the author's own personal use so there is no documentation on offer, nor is there any intent to document the code in the near future.

If you want to see what it's all about, please check out SquidEyes.UnitTests. As you will see, the code is rather, well . . . basic (validation extenders, JSON converters, string manipulation methods, etc.).  Even so, there are a number of standouts:

|Class|Description|
|---|---|
|FastArrayReader|A bit like a BinaryReader, but at least 10x faster; for arrays, not streams|
|HttpHelper|Fetches strings and JSON objects via HTTP(S) endpoints, with easy URL construction like Flurl.Http, but in a lighter weight object that supports an injectable HttpClientHandler|
|CsvEnumerator|A fast, lightweight, super-easy-to-use CSV parser / enumerator that allows CSV files to be read with minimal memory collection pressure|
|DialSet|A "dial" collection that supports upsertable configuration setting updates; with 14 common data types, including enums|
|SlidingBuffer|A fixed-size generic sliding buffer that supports forward and reverse iteration and indexing|

#
Contributions are always welcome (see [CONTRIBUTING.md](https://github.com/squideyes/Basics/blob/master/CONTRIBUTING.md) for details)

**Supper-Duper Extra-Important Caveat**:  THE SOFTWARE IS PROVIDED “AS IS”, WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT.




