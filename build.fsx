// include Fake libs
#r "packages/FAKE/tools/FakeLib.dll"

// TODO: once migrated to FAKE 5 proper, remove this plus paket references to Newtonsoft.Json and GitHub file
#r "packages/Newtonsoft.Json/lib/netstandard2.0/Newtonsoft.Json.dll"
open Newtonsoft.Json.Linq
#load "paket-files/fsharp/FAKE/src/app/Fake.DotNet.Cli/DotNet.fs"
//

open Fake
open Fake.Core.TargetOperators
open Fake.IO.Globbing.Operators

// Filesets
let fableReferences = 
    !! "src/website.fsproj"

let fableDirectory = "src"

let dotnetcliVersion = "2.1.201"

let install = lazy Fake.DotNet.DotNet.install (fun p -> { p with Version = Fake.DotNet.DotNet.CliVersion.Version dotnetcliVersion })

// Targets
Fake.Core.Target.create "Clean" (fun _ ->
    [ fableReferences ]
    |> Seq.concat
    |> Seq.iter (fun proj -> Fake.DotNet.DotNet.exec id "clean" proj |> ignore)
)

Fake.Core.Target.create "Restore" (fun _ ->
    [ fableReferences ]
    |> Seq.concat
    |> Seq.iter (fun proj -> Fake.DotNet.DotNet.restore id proj)
)

Fake.Core.Target.create "NpmInstall" (fun _ ->
    Fake.JavaScript.Npm.install (fun p ->
        { p with WorkingDirectory = fableDirectory })
)

Fake.Core.Target.create "BuildFable" (fun _ ->
    fableReferences
    |> Seq.iter (fun proj ->
        Fake.DotNet.DotNet.exec (fun p ->
            { p with WorkingDirectory = fableDirectory }) "fable npm-build" proj |> ignore)
)

Fake.Core.Target.create "RunFable" (fun _ ->
    fableReferences
    |> Seq.iter (fun proj ->
        Fake.DotNet.DotNet.exec (fun p ->
            { p with WorkingDirectory = fableDirectory }) "fable npm-start " proj |> ignore)
)

// Build order

"Clean"
    ==> "Restore"
    ==> "NpmInstall"

"NpmInstall" ==> "BuildFable"
"NpmInstall" ==> "RunFable"

// start build
Fake.Core.Target.runOrDefault "BuildFable"