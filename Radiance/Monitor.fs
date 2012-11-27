namespace Radiance

open System
open System.Windows

open Radiance.Interop

type Monitor(window:Window) =
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
        and set(value) = _currentBrightness <- value

    member this.Initialize =
        let hMonitor = MonitorFromWindow(Hwnd(window), 0ul)
        let mutable min = Unchecked.defaultof<uint32>
        let mutable current = Unchecked.defaultof<uint32>
        let mutable max = Unchecked.defaultof<uint32>
        let success = GetMonitorBrightness(hMonitor, &min, &current, &max)
        match success with
            | true -> 
                this.MinimumBrightness <- min
                this.CurrentBrightness <- current
                this.MaximumBrightness <- max
            | _ -> ()
