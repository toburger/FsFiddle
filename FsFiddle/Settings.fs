namespace FsFiddler

type Settings =
    { IgnoreSSL : bool
      IgnoreRedirects : bool
      IgnoreMissingMimeType : bool }

[<CompilationRepresentation(CompilationRepresentationFlags.ModuleSuffix)>]
[<RequireQualifiedAccess>]
module Settings =
    let mutable Default =
        { IgnoreSSL = true
          IgnoreRedirects = false
          IgnoreMissingMimeType = true }
