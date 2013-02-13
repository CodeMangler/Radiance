namespace Radiance

open System
open System.Runtime.InteropServices
open System.Windows
open System.Windows.Interop

module Interop =
    [<System.Runtime.InteropServices.DllImport(@"dxva2.dll", EntryPoint="GetMonitorBrightness")>]
    extern [<return: MarshalAs(UnmanagedType.Bool)>] bool GetMonitorBrightness([<In>] nativeint hMonitor, [<Out>] uint32& minimumBrightness, [<Out>] uint32& currentBrightness, [<Out>] uint32& maximumBrightness);
    
    [<System.Runtime.InteropServices.DllImport(@"user32.dll", EntryPoint="MonitorFromWindow")>]
    extern nativeint MonitorFromWindow ([<In>] nativeint handle, [<In>] uint32 dwFlags);

    let Hwnd(wpfWindow:Window) = WindowInteropHelper(wpfWindow).Handle

    [<System.Runtime.InteropServices.DllImport("user32.dll", EntryPoint="RegisterHotKey")>]
    extern int RegisterHotKey([<In>] nativeint hWnd, int id, int fsModifiers, int vk);

    [<System.Runtime.InteropServices.DllImport("user32.dll", EntryPoint="UnregisterHotKey")>]
    extern int UnregisterHotKey([<In>] nativeint hWnd, int id);
