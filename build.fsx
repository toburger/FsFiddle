#r @"packages/FAKE/tools/FakeLib.dll"
open Fake

RestorePackages()

let buildDir = "./build/"

Target "Clean" (fun _ ->
    CleanDir buildDir
)

Target "BuildApp" (fun _ ->
    !! "**/*.fsproj"
      |> MSBuildRelease buildDir "Build"
      |> Log "AppBuild-Output: "
)

Target "Default" id

"Clean"
 ==> "BuildApp"
 ==> "Default"

RunTargetOrDefault "Default"