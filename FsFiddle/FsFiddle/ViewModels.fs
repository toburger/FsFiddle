﻿namespace ViewModels

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

//    do captures.Add(
//        { Url = "hallo"
//          Request =
//            { Request.Header = [("asdf", "asdf")] |> Map.ofList
//              Request.Body = lazy("asdf") }
//          Response =
//            { Response.Header = [("asdf", "asdf2")] |> Map.ofList
//              Response.Body = lazy("asdfasfd") } })

    member self.SelectedCapture
        with get() = self.selectedCapture
        and set(v) =
            self.selectedCapture <- v
            self.NotifyOfPropertyChange(<@ self.SelectedCapture @>)
    
    member self.StartCapturing() =
        startCapturing()

//    member self.CanStartCapturing() =
//        not (isCapturing())

    member self.StopCapturing() =
        stopCapturing()

//    member self.CanStopCapturing() =
//        isCapturing()

    override self.OnDeactivate(close) =
        if close then stopCapturing()

    interface IShell