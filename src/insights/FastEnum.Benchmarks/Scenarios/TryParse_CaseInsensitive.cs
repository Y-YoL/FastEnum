﻿extern alias FastEnumV1;

using System;
using BenchmarkDotNet.Attributes;
using EnumsNET;
using FastEnumUtility.Benchmarks.Models;
using FastEnum1 = FastEnumV1::FastEnumUtility.FastEnum;
using FastEnum2 = FastEnumUtility.FastEnum;

namespace FastEnumUtility.Benchmarks.Scenarios;



public class TryParse_CaseInsensitive
{
    private const string Value = nameof(Fruits.WaterMelon);


    [GlobalSetup]
    public void Setup()
    {
        _ = Enum.GetNames<Fruits>();
        _ = Enums.GetMembers<Fruits>();
        _ = FastEnum1.GetMembers<Fruits>();
        _ = FastEnum2.GetMembers<Fruits>();
    }


    [Benchmark(Baseline = true)]
    public bool NetCore()
        => Enum.TryParse<Fruits>(Value, true, out _);


    [Benchmark]
    public bool EnumsNet()
        => Enums.TryParse<Fruits>(Value, true, out _);


    [Benchmark]
    public bool FastEnum_v1()
        => FastEnum1.TryParse<Fruits>(Value, true, out _);


    [Benchmark]
    public bool FastEnum_v2()
        => FastEnum2.TryParse<Fruits>(Value, true, out _);
}
