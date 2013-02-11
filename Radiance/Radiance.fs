namespace Radiance

open System.Windows.Controls

open Radiance
open Radiance.Interop
open Radiance.WPF

type Radiance() as this =
    let window = createWindow("Radiance.xaml")
    do window.Loaded.AddHandler(fun obj args -> this.Initialize |> ignore)

    member this.Window = window

//    member this.Initialize = 
//        let slider = window.FindName("brightness") :?> Slider
//        let display = Monitor(window)
//        do display.Initialize
//        do slider.Minimum <- float(display.MinimumBrightness)
//        do slider.Maximum <- float(display.MaximumBrightness)
//        do slider.Value <- float(display.CurrentBrightness)

    member this.Initialize = 
        let slider = window.FindName("brightness") :?> Slider
        slider.ValueChanged.Add(fun _ -> this.SyncBrightness)
        do slider.Minimum <- 0.0
        do slider.Maximum <- 100.0
        do slider.Value <- 50.0

    member this.SyncBrightness = 
        let slider = window.FindName("brightness") :?> Slider
        let display = WMIMonitor()
        do display.SetBrightness(slider.Value)
