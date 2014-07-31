module MainApp

open System
open System.Windows

type App() =
    inherit Application()
    do Shell.AppBootstrapper() |> ignore

[<STAThread>]
[<EntryPoint>]
let main _ =
    let app = App()
    app.Run()
