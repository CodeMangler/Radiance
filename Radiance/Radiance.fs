namespace Radiance

open System.Windows.Controls

open Radiance
open Radiance.Interop
open Radiance.WPF

type Radiance() as this =
    let window = createWindow("Radiance.xaml")
    do window.Loaded.AddHandler(fun obj args -> this.Initialize |> ignore)
    let monitor = new WMIMonitor()
    do monitor.Initialize()

    member this.Window = window

    member this.Initialize = 
        let slider = window.FindName("brightness") :?> Slider
        slider.ValueChanged.Add(fun _ -> this.SyncBrightness())
        do slider.Minimum <- float(monitor.MinimumBrightness)
        do slider.Maximum <- float(monitor.MaximumBrightness)
        do slider.Value <- float(monitor.CurrentBrightness)

    member this.SyncBrightness() = 
        let slider = window.FindName("brightness") :?> Slider
        let display = new WMIMonitor()
        do display.SetBrightness(slider.Value)
