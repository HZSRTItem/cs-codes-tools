# TFRecordCore
Cross-platform .NET 5.0 C# Library to Read and Write TFRecord files, with out-of-the-box support for Tensorflow's Example and Feature protobufs.

# Description
Line-per-line C# conversion of the Java sources for **TFRecordReader** and **TFRecordWriter** taken from Tensorflow's Ecosystem repo (Apache Licence 2.0):

https://github.com/tensorflow/ecosystem/blob/master/hadoop/src/main/java/org/tensorflow/hadoop/util/ 

Method names and variable names were kept the same, with small differences related to casing conventions (e.g. read -> Read) or the peculiarities of the .NET Framework vs. the Java Library (e.g. ByteBuffer -> BitConverter).

# Requirements
.NET 5.0

Tested under Windows 10 and macOS. It's reasonable to assume it should work unmodified under Linux distros supporting .NET 5.0

# NuGet Dependencies
- [Crc32.NET](https://www.nuget.org/packages/Crc32.NET)
- [Google.Protobuf](https://www.nuget.org/packages/Google.Protobuf)

# Updating the Protobufs
This repository works right out of the box because it includes working C# files for the *Example* and *Feature* protobufs (external/Example.cs and external/Feature.cs)

However, Tensorflow's definition for the *Example* and *Feature* protobufs could change.
These two scripts will download the latest *proto* files from the Tensorflow repository and run Google's **protoc** to regenerate the C# files:
- external/update-win64.bat
- external/update-macos.sh

Google's **protoc** binaries (required for the scripts above) version 3.17.1 are also included. If there's ever the need to use a newer version, update them manually and change the scripts accordingly.

# Usage

You can use the source code in the **Testing** project as a starting point:

```csharp
using TensorFlow;

...

var example = new Example();
example.Features = new Features();
var feat1 = new Feature();
feat1.Int64List = new Int64List();
feat1.Int64List.Value.Add(1L);
feat1.Int64List.Value.Add(2L);
example.Features.Feature.Add("feat", feat1);
var feat2 = new Feature();
feat2.FloatList = new FloatList();
feat2.FloatList.Value.Add(0.1f);
feat2.FloatList.Value.Add(0.2f);
example.Features.Feature.Add("feat2", feat2);

// Write
Console.WriteLine("Written to file:");
Console.WriteLine(example);
using (var tw = new TFRecordWriter(File.Create("testing.tfrecord"))) tw.Write(example);

// Read
byte[] readbytes;
using (var tr = new TFRecordReader(File.OpenRead("testing.tfrecord"), true)) readbytes = tr.Read();
var readexample = Example.Parser.ParseFrom(readbytes);
Console.WriteLine("Read from file:");
Console.WriteLine(readexample);
```

Program output:

```json
Written to file:
{ "features": { "feature": { "feat": { "int64List": { "value": [ "1", "2" ] } }, "feat2": { "floatList": { "value": [ 0.1, 0.2 ] } } } } }
Read from file:
{ "features": { "feature": { "feat": { "int64List": { "value": [ "1", "2" ] } }, "feat2": { "floatList": { "value": [ 0.1, 0.2 ] } } } } }
```
