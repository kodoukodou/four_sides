using UnityEngine;
using UnityEngine.Profiling;

public class Example : MonoBehaviour
{
    private void Update()
    {
        var monoUsedSize = Profiler.GetMonoUsedSizeLong() / 1024f / 1024f;
        var monoReservedSize = Profiler.GetMonoHeapSizeLong() / 1024f / 1024f;
        var unityUsedSize = Profiler.GetTotalAllocatedMemoryLong() / 1024f / 1024f;
        var unityReservedSize = Profiler.GetTotalReservedMemoryLong() / 1024f / 1024f;

        Debug.Log(nameof(monoUsedSize) + $" : {monoUsedSize}MB");
        Debug.Log(nameof(monoReservedSize) + $" : {monoReservedSize}MB");
        Debug.Log(nameof(unityUsedSize) + $" : {unityUsedSize}MB");
        Debug.Log(nameof(unityReservedSize) + $" : {unityReservedSize}MB");
    }
}