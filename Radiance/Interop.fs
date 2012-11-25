namespace Radiance

open System
open System.Windows
open System.Windows.Interop

module Interop =
    [<System.Runtime.InteropServices.DllImport(@"dxva2.dll", EntryPoint="GetMonitorBrightness")>]
    extern bool GetMonitorBrightness(IntPtr hMonitor, IntPtr minimumBrightness, IntPtr currentBrightness, IntPtr maximumBrightness);
    
    [<System.Runtime.InteropServices.DllImport(@"user32.dll", EntryPoint="MonitorFromWindow")>]
    extern IntPtr MonitorFromWindow (IntPtr handle, uint32 dwFlags);

    let Hwnd(wpfWindow:Window) = WindowInteropHelper(wpfWindow).Handle
