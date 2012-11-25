namespace Radiance

open System
open System.Windows

open Radiance

module Main =
    [<STAThread>]
    [<EntryPoint>]
    let main(_) =
        let radiance = Radiance.Radiance()
        (new Application()).Run(radiance.Window)

//        let window = Application.LoadComponent(new System.Uri("/Radiance;component/Main.xaml", System.UriKind.Relative)) :?> Window
