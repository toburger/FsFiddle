namespace Resources

open System
open System.Reflection
open System.Windows.Media
open System.Windows.Media.Imaging


module ResourceStreamHelper =
    let loadResourceStream =
        let assm = Assembly.GetExecutingAssembly()
        fun name -> assm.GetManifestResourceStream(name)

type ImageResource (name: string) =
    let loadMediaResource name =
        let bi = BitmapImage()
        bi.BeginInit()
        use stream = ResourceStreamHelper.loadResourceStream name
        bi.StreamSource <- stream
        bi.EndInit()
        bi :> ImageSource

    let imageSource = loadMediaResource name

    member __.Name = name
    member __.ImageSource = imageSource

type IconResource (name: string) =
    let loadIconResource name =
        new System.Drawing.Icon(ResourceStreamHelper.loadResourceStream name)

    let image = loadIconResource name

    member __.Name = name
    member __.Icon = image

[<RequireQualifiedAccess>]
[<CompilationRepresentation(CompilationRepresentationFlags.ModuleSuffix)>]
module IconResource =
    let FsFiddle = ImageResource "FsFiddle.ico"
