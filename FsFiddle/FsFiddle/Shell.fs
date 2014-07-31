namespace Shell

open System
open System.ComponentModel.Composition
open System.ComponentModel.Composition.Hosting
open System.ComponentModel.Composition.Primitives
open System.Reflection
open System.Windows
open Caliburn.Micro
open MahApps.Metro.Controls

type IShell = interface end

type MetroWindowManager() =
    inherit WindowManager()

    let getResourceDictionary s =
        let uriString = sprintf "pack://application:,,,/MahApps.Metro;component/Styles/%s.xaml" s
        ResourceDictionary(Source = Uri(uriString, UriKind.RelativeOrAbsolute))

    let resources =
        [|
            "Controls"
            "Fonts"
            "Colors"
            "Accents/Blue"
            "Accents/BaseLight"
            "Controls.AnimatedTabControl"
         |] |> Array.map getResourceDictionary

    override self.EnsureWindow(model, view, isDialog) =
        match view with
        | :? MetroWindow as window ->
            match self.InferOwnerOf window, isDialog with
            | o, true -> window.Owner <- o
            | _ -> ()
            window
        | _ ->
            let window = MetroWindow(Content = view,
                                     SizeToContent = SizeToContent.Manual,
                                     MinHeight = 350.,
                                     MinWidth = 300.,
                                     Width = 1000.,
                                     Height = 700.)
            resources |> Seq.iter window.Resources.MergedDictionaries.Add
            window.SetValue(View.IsGeneratedProperty, true)
            match self.InferOwnerOf window with
            | null ->
                window.WindowStartupLocation <- WindowStartupLocation.CenterOwner
            | owner ->
                window.WindowStartupLocation <- WindowStartupLocation.CenterOwner
                window.Owner <- owner
            window
        :> Window

type AppBootstrapper() as self =
    inherit BootstrapperBase()

    do self.Initialize()

    [<DefaultValue>] val mutable private container: CompositionContainer

    override self.OnStartup(sender, e) =
        self.DisplayRootViewFor<IShell>()

    override self.Configure() =
        self.container <-
            new CompositionContainer(
                new AggregateCatalog(
                    AssemblySource.Instance
                    |> Seq.map (fun i -> new AssemblyCatalog(i) :> ComposablePartCatalog)))

        let batch = new CompositionBatch()

        batch.AddExportedValue<IWindowManager>(MetroWindowManager()) |> ignore
        batch.AddExportedValue<IEventAggregator>(EventAggregator()) |> ignore
        batch.AddExportedValue(self.container) |> ignore

        self.container.Compose batch

    override self.GetInstance(serviceType, key) =
        let contract = match key with
                       | null
                       | "" -> AttributedModelServices.GetContractName serviceType
                       | s -> s

        let exports = self.container.GetExportedValues contract

        if exports |> Seq.isEmpty then
            failwithf "Could not locate any instances of contract %s." contract
        exports |> Seq.head

    override self.GetAllInstances(serviceType) =
        self.container.GetExportedValues(
            AttributedModelServices.GetContractName serviceType)

    override self.SelectAssemblies() =
        Seq.singleton (Assembly.GetExecutingAssembly())

    override self.OnUnhandledException(sender, e) =
        base.OnUnhandledException(sender, e)

