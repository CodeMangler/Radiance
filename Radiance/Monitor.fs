namespace Radiance

open System
open System.Windows

open Radiance.Interop

type Monitor(window:Window) =
    let mutable _minimumBrightness = 0
    let mutable _currentBrightness = 0
    let mutable _maximumBrightness = 0


    member this.MinimumBrightness
        with get() = _minimumBrightness
        and set(value) = _minimumBrightness <- value

    member this.CurrentBrightness
        with get() = _currentBrightness
        and set(value) = _currentBrightness <- value

    member this.MaximumBrightness
        with get() = _maximumBrightness
        and set(value) = _currentBrightness <- value

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
