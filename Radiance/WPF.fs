namespace Radiance

open System.Windows
open System.Windows.Markup
open System.Xml

module WPF =
    let createWindow(file:string) = 
        using (XmlReader.Create(file)) (fun stream -> (XamlReader.Load(stream) :?> Window))
    
    let createWindowFromResource(resourcePath:string) =
        Application.LoadComponent(new System.Uri(resourcePath, System.UriKind.Relative)) :?> Window
