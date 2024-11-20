// $ dotnet tool install -g dotnet-script
// $ dotnet script benchmark.cs

// === Performance Statistics ===

// CPU Time Statistics:
// Average Time: 218.20 ms
// Min Time: 206 ms
// Max Time: 237 ms

// Memory Usage Statistics:
// Average Memory: 76.32 MB
// Min Memory: 76.32 MB
// Max Memory: 76.32 MB

// Variability Statistics:
// CPU Time Std Dev: 13.17 ms
// Memory Usage Std Dev: 0.00 MB

using System;
using System.Diagnostics;
using System.Runtime;
using System.Collections.Generic;

const int NUMBER_OF_RUNS = 5;
var executionTimes = new List<long>();
var memoryUsages = new List<double>();

// Run multiple times
for (int run = 0; run < NUMBER_OF_RUNS; run++)
{
    Console.WriteLine($"\nRun {run + 1}/{NUMBER_OF_RUNS}:");
    Console.WriteLine("Starting memory measurement test...");

    // Force GC collection before measurement
    GC.Collect();
    GC.WaitForPendingFinalizers();
    GC.Collect();

    var startMemory = GC.GetTotalMemory(true);
    var stopwatch = Stopwatch.StartNew();

    // Create a list to hold objects in heap memory
    var objectList = new List<double[]>();

    // Create and store multiple arrays to demonstrate heap memory usage
    for (var iteration = 0; iteration < 1000; iteration++)
    {
        // Allocate new array in each iteration - forces heap allocation
        var dataArray = new double[10000];
        for (var i = 0; i < dataArray.Length; i++)
        {
            dataArray[i] = Math.Sqrt(i) * Math.Sin(i);
        }
        objectList.Add(dataArray);  // Keep reference to prevent GC
    }

    stopwatch.Stop();

    // Measure immediately before GC can run
    var endMemory = GC.GetTotalMemory(false);  // false to prevent forced GC
    var memoryUsedMB = (endMemory - startMemory) / 1024.0 / 1024.0;
    var executionTimeMs = stopwatch.ElapsedMilliseconds;

    // Store metrics
    executionTimes.Add(executionTimeMs);
    memoryUsages.Add(memoryUsedMB);

    // Output results
    Console.WriteLine($"Memory Used: {memoryUsedMB:F2} MB");
    Console.WriteLine($"Execution Time: {executionTimeMs} ms");

    // Display detailed memory statistics
    Console.WriteLine("\nDetailed Memory Statistics:");
    Console.WriteLine($"Initial Memory: {startMemory / 1024.0 / 1024.0:F2} MB");
    Console.WriteLine($"Final Memory: {endMemory / 1024.0 / 1024.0:F2} MB");

    // Prevent list from being optimized away by compiler
    Console.WriteLine($"Total Objects Created: {objectList.Count}");
}

// Calculate and display statistics
Console.WriteLine("\n=== Performance Statistics ===");
Console.WriteLine("\nCPU Time Statistics:");
Console.WriteLine($"Average Time: {executionTimes.Average():F2} ms");
Console.WriteLine($"Min Time: {executionTimes.Min()} ms");
Console.WriteLine($"Max Time: {executionTimes.Max()} ms");

Console.WriteLine("\nMemory Usage Statistics:");
Console.WriteLine($"Average Memory: {memoryUsages.Average():F2} MB");
Console.WriteLine($"Min Memory: {memoryUsages.Min():F2} MB");
Console.WriteLine($"Max Memory: {memoryUsages.Max():F2} MB");

// Calculate standard deviations
double cpuStdDev = Math.Sqrt(executionTimes.Select(x => Math.Pow(x - executionTimes.Average(), 2)).Average());
double memoryStdDev = Math.Sqrt(memoryUsages.Select(x => Math.Pow(x - memoryUsages.Average(), 2)).Average());

Console.WriteLine("\nVariability Statistics:");
Console.WriteLine($"CPU Time Std Dev: {cpuStdDev:F2} ms");
Console.WriteLine($"Memory Usage Std Dev: {memoryStdDev:F2} MB");