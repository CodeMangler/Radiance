namespace Radiance

open Radiance
open Radiance.Interop
open Radiance.WPF

type Keys() as this =
    let window = createWindow("Keys.xaml")
    do window.Loaded.AddHandler(fun obj args -> this.Initialize |> ignore)

    member this.Window = window

    member this.Initialize =
        do System.Console.Out.WriteLine("Foo")
