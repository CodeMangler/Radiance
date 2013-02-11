namespace Radiance

open System
open System.Management

type WMIMonitor() as this =
    let setBrightness = new ManagementClass("WmiMonitorBrightnessMethods")
    do setBrightness.Scope <- new ManagementScope(@"\\.\root\wmi")
    let monitorInstances = setBrightness.GetInstances()

    interface IDisposable with
        member this.Dispose() = 
            for monitorInstance in monitorInstances do
                monitorInstance.Dispose()
            done
            do setBrightness.Dispose()
    
    member this.SetBrightness(percent) =
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
