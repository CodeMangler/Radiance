namespace Radiance

open System
open System.Management

type WMIMonitor() =
    let brightness = new ManagementClass(@"\\.\root\wmi", "WmiMonitorBrightness", ObjectGetOptions())
    let brightnessSetter = new ManagementClass(@"\\.\root\wmi", "WmiMonitorBrightnessMethods", ObjectGetOptions())

    let mutable _minimumBrightness = 0ul
    let mutable _currentBrightness = 0ul
    let mutable _maximumBrightness = 0ul

    member this.MinimumBrightness
        with get() = _minimumBrightness
        and set(value) = _minimumBrightness <- value

    member this.CurrentBrightness
        with get() = _currentBrightness
        and set(value) = _currentBrightness <- value

    member this.MaximumBrightness
        with get() = _maximumBrightness
        and set(value) = _maximumBrightness <- value
    
    member this.GetBrightness() =
            use monitorInstances = brightness.GetInstances()
            for monitorInstance in monitorInstances do
                let active = monitorInstance.GetPropertyValue("Active")
                match (active :?> bool) with
                    | true ->
                        this.CurrentBrightness <- System.UInt32.Parse(monitorInstance.GetPropertyValue("CurrentBrightness").ToString())
                    | _ ->
                        this.CurrentBrightness <- uint32(0)
            done

    member this.SetBrightness(percent) =
            use monitorInstances = brightnessSetter.GetInstances()
            for monitorInstance in monitorInstances do
                let active = monitorInstance.GetPropertyValue("Active")
                match (active :?> bool) with
                    | true ->
                        let timeout = 1 //seconds
                        let brightness = percent
                        (monitorInstance :?> ManagementObject).InvokeMethod("WmiSetBrightness", [|timeout; brightness|]) |> ignore
                    | _ -> 
                        Console.Out.WriteLine("Skipping inactive monitor") |> ignore
            done

    member this.Initialize() = 
        do this.MinimumBrightness <- uint32(0)
        do this.MaximumBrightness <- uint32(100)
        do this.GetBrightness()

    interface IDisposable with
        member this.Dispose() = 
            do brightness.Dispose()
            do brightnessSetter.Dispose()
