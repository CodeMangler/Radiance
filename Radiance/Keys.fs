namespace Radiance

open System.Windows.Controls
open System.Windows.Input

open Radiance
open Radiance.Interop
open Radiance.WPF

type Keys() as this =
    let window = createWindow("Keys.xaml")
    do window.Loaded.AddHandler(fun obj args -> this.Initialize |> ignore)

    member this.Window = window

    member this.Initialize =
        let brighterInput = window.FindName("brighter") :?> TextBox
        let darkerInput = window.FindName("darker") :?> TextBox
        
        brighterInput.KeyDown.Add(fun(keyEventArgs:KeyEventArgs) -> this.KeyPressed(keyEventArgs, brighterInput))
        darkerInput.KeyDown.Add(fun(keyEventArgs:KeyEventArgs) -> this.KeyPressed(keyEventArgs, darkerInput))

    member this.KeyPressed(keyArgs:KeyEventArgs, input:TextBox) =
        do input.Text <- keyArgs.Key.ToString()
        do System.Diagnostics.Debug.WriteLine(keyArgs.ToString())
