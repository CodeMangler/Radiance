namespace Radiance

open System
open System.Windows

open Radiance.Interop

type Monitor(window:Window) =
    
    member val MinimumBrightness = 0 with get, set
    member val CurrentBrightness = 0 with get, set
    member val MaximumBrightness = 0 with get, set

    member this.Initialize =
        let hMonitor = MonitorFromWindow(Hwnd(window), 0ul)
        let min = new IntPtr()
        let current = new IntPtr()
        let max = new IntPtr()
        let success = GetMonitorBrightness(hMonitor, min, current, max)
        match success with
            | true -> 
                this.MinimumBrightness <- min.ToInt32()
                this.CurrentBrightness <- current.ToInt32()
                this.MaximumBrightness <- max.ToInt32()
            | _ -> ()
