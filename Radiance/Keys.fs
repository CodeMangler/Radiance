namespace Radiance

open System.Windows.Controls
open System.Windows.Input
open System.Configuration

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
        let ok = window.FindName("ok") :?> Button
        let cancel = window.FindName("cancel") :?> Button

        this.LoadConfiguration()

        brighterInput.PreviewKeyDown.Add(fun(keyEventArgs:KeyEventArgs) -> this.KeyPressed(keyEventArgs, brighterInput))
        darkerInput.PreviewKeyDown.Add(fun(keyEventArgs:KeyEventArgs) -> this.KeyPressed(keyEventArgs, darkerInput))

        ok.Click.Add(fun _ -> this.okPressed())
        cancel.Click.Add(fun _ -> this.cancelPressed())

    member this.LoadConfiguration() =
        let brighterInput = window.FindName("brighter") :?> TextBox
        let darkerInput = window.FindName("darker") :?> TextBox
        brighterInput.Text <- ConfigurationManager.AppSettings.["brighter"]
        darkerInput.Text <- ConfigurationManager.AppSettings.["darker"]

    member this.SaveConfiguration() =
        let brighterInput = window.FindName("brighter") :?> TextBox
        let darkerInput = window.FindName("darker") :?> TextBox
        let configuration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None)
        let appSettings = configuration.GetSection("appSettings") :?> AppSettingsSection
        appSettings.Settings.Remove("brighter")
        appSettings.Settings.Remove("darker")
        appSettings.Settings.Add("brighter", brighterInput.Text)
        appSettings.Settings.Add("darker", darkerInput.Text)
        configuration.Save(ConfigurationSaveMode.Modified)

    member this.KeyPressed(keyArgs:KeyEventArgs, input:TextBox) =
        do keyArgs.Handled <- true
        do input.Text <- keyArgs.Key.ToString()

    member this.okPressed() =
        this.SaveConfiguration()
        do this.Window.Close()

    member this.cancelPressed() =
        do this.Window.Close()
