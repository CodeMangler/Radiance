namespace Radiance

open System.Windows.Controls

open Radiance
open Radiance.Interop
open Radiance.WPF

type Radiance() as this =
    let window = createWindow("Radiance.xaml")
    do window.Loaded.AddHandler(fun obj args -> this.Initialize |> ignore)

    member this.Window = window

    member this.Initialize = 
        let slider = window.FindName("brightness") :?> Slider
        let display = Monitor(window)
        do display.Initialize
        do slider.Minimum <- float(display.MinimumBrightness)
        do slider.Maximum <- float(display.MaximumBrightness)
        do slider.Value <- float(display.CurrentBrightness)
