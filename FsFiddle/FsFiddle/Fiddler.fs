module Fiddler

open Models
open Fiddler
open System
open System.Text

let captures = Event<Capture>()

let toMap (hdrs: seq<HTTPHeaderItem>) =
    hdrs
    |> Seq.map (fun h -> h.Name, h.Value)
    |> Map.ofSeq

let afterSessionComplete (session: Session) =
    if session.RequestMethod = "CONNECT" then ()
    if session = null || session.oRequest = null || session.oRequest.headers = null then ()
    else
        let url = session.fullUrl
        let requestHeaders = session.oRequest.headers |> toMap
        let requestBody = lazy (session.GetRequestBodyAsString())
        let responseCode = session.responseCode
        let responseHeaders = session.oResponse.headers |> toMap
        let responseBody = lazy (session.GetResponseBodyAsString())
        { Url = url
          Request = { Header = requestHeaders
                      Body = requestBody }
          Response = { Code = responseCode
                       MimeType = session.oResponse.MIMEType
                       Header = responseHeaders
                       Body = responseBody } }
        |> captures.Trigger
    
let afterSessionCompleteHandler = SessionStateHandler(afterSessionComplete)

let tracefn fmt = Printf.kprintf System.Diagnostics.Trace.WriteLine fmt

let log = EventHandler<LogEventArgs>(fun _ l ->
    tracefn "FIDDLER: %s" l.LogString)

let isCapturing() = FiddlerApplication.IsStarted()

let startCapturing() =
    FiddlerApplication.Log.OnLogString.AddHandler(log)
    FiddlerApplication.add_AfterSessionComplete(afterSessionCompleteHandler)
    FiddlerApplication.Startup(8888, bRegisterAsSystemProxy = true,
                               bDecryptSSL = true, bAllowRemote = false)

let stopCapturing() =
    FiddlerApplication.Log.OnLogString.RemoveHandler(log)
    FiddlerApplication.remove_AfterSessionComplete(afterSessionCompleteHandler)
    if FiddlerApplication.IsStarted() then FiddlerApplication.Shutdown()
