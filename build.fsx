// include Fake libs
#r "packages/FAKE/tools/FakeLib.dll"

open Fake
open Fake.Core.TargetOperators
open Fake.IO.Globbing.Operators

// Filesets
let fableReferences = 
    !! "src/website.fsproj"

let fableDirectory = "src"

let dotnetcliVersion = "2.1.201"
let mutable dotnetExePath = "dotnet"

// Targets
Fake.Core.Target.create "InstallDotNetCore" (fun _ ->
    dotnetExePath <- DotNetCli.InstallDotNetSDK dotnetcliVersion
)

Fake.Core.Target.create "Clean" (fun _ ->
    [ fableReferences ]
    |> Seq.concat
    |> Seq.iter (fun proj -> DotNetCli.RunCommand id ("clean " + proj))
)

Fake.Core.Target.create "Restore" (fun _ ->
    [ fableReferences ]
    |> Seq.concat
    |> Seq.iter (fun proj -> DotNetCli.Restore (fun p -> { p with Project = proj; ToolPath = dotnetExePath; AdditionalArgs = [ "--no-dependencies" ] }))
)

Fake.Core.Target.create "NpmInstall" (fun _ ->
    Fake.JavaScript.Npm.install (fun p ->
        { p with WorkingDirectory = fableDirectory })
)

Fake.Core.Target.create "BuildFable" (fun _ ->
    fableReferences
    |> Seq.iter (fun proj -> DotNetCli.RunCommand (fun p -> { p with WorkingDir = fableDirectory; ToolPath = dotnetExePath }) ("fable npm-build " + proj))
)

Fake.Core.Target.create "RunFable" (fun _ ->
    fableReferences
    |> Seq.iter (fun proj -> DotNetCli.RunCommand (fun p -> { p with WorkingDir = fableDirectory; ToolPath = dotnetExePath }) ("fable npm-start " + proj))
)

// Build order
"InstallDotNetCore"
    ==> "Clean"
//    ==> "UpdateVersionNumber"
    ==> "Restore"
    ==> "NpmInstall"

"NpmInstall" ==> "BuildFable"
"NpmInstall" ==> "RunFable"

// start build
Fake.Core.Target.runOrDefault "BuildFable"