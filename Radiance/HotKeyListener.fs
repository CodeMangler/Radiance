namespace Radiance

open Radiance.Interop
open System.Windows.Forms

type HotKeyListener() as this =
    inherit NativeWindow()

    let createParams = new CreateParams()
    do createParams.Caption <- "__RadianceGlobalHotkeyListener__"
    do createParams.ClassName <- "STATIC"
    
    do createParams.X <- 0
    do createParams.Y <- 0
    do createParams.Height <- 0
    do createParams.Width <- 0

    do createParams.Style <- 0x20000000 // TODO: Replace with WindowStyles.WS_MINIMIZE
    do createParams.ExStyle <- 0x08000000 // TODO: Replace with WindowStyles.WS_EX_NOACTIVATE

    do this.CreateHandle(createParams)

    member this.RegisterHotkey(id, fsModifiers, vKey) =
        try
            RegisterHotKey(this.Handle, id, fsModifiers, vKey)
        with
            | ex -> System.Diagnostics.Debug.WriteLine(ex.ToString()); -1

    member this.UnregisterHotkey(id) =
        try
            UnregisterHotKey(this.Handle, id)
        with
            | ex -> System.Diagnostics.Debug.WriteLine(ex.ToString()); -1
