namespace ViewModels

open System
open System.ComponentModel.Composition
open Caliburn.Micro
open Shell
open Models
open Fiddler

[<Export(typeof<IShell>)>]
type ShellViewModel() =
    inherit Screen(DisplayName = "FsFiddle")

    let captures = new BindableCollection<Capture>()
    [<DefaultValue>] val mutable selectedCapture: Capture
    
    do Fiddler.captures.Publish |> Observable.add captures.Add
    
    member self.Captures = captures

    member self.SelectedCapture
        with get() = self.selectedCapture
        and set(v) =
            self.selectedCapture <- v
            self.NotifyOfPropertyChange(<@ self.SelectedCapture @>)
    
    member self.StartCapturing() =
        startCapturing()

    member self.StopCapturing() =
        stopCapturing()

    override self.OnDeactivate(close) =
        if close then stopCapturing()

    interface IShell