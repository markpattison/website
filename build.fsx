// include Fake libs
#r "packages/FAKE/tools/FakeLib.dll"

open Fake
open Fake.NpmHelper

// Filesets
let fableReferences = 
    !! "src/website.fsproj"

let fableDirectory = "src"

let dotnetcliVersion = "2.1.105"
let mutable dotnetExePath = "dotnet"

// Targets
Target "InstallDotNetCore" (fun _ ->
    dotnetExePath <- DotNetCli.InstallDotNetSDK dotnetcliVersion
)

Target "Clean" (fun _ ->
    [ fableReferences ]
    |> Seq.concat
    |> Seq.iter (fun proj -> DotNetCli.RunCommand id ("clean " + proj))
)

// Target "UpdateVersionNumber" (fun _ ->
//     let release =
//         ReadFile "RELEASE_NOTES.md"
//         |> ReleaseNotesHelper.parseReleaseNotes
//     let revisionFromCI = environVarOrNone "BUILD_BUILDID"
//     let version =
//         match revisionFromCI with
//         | None -> release.AssemblyVersion
//         | Some s -> sprintf "%s build %s" release.AssemblyVersion s
//     let versionFiles = !! "**/Version.fs"
//     FileHelper.RegexReplaceInFilesWithEncoding @"VersionNumber = "".*""" (sprintf @"VersionNumber = ""%s""" version) System.Text.Encoding.UTF8 versionFiles
//     TraceHelper.trace (sprintf @"Version = %s" version)
// )

Target "Restore" (fun _ ->
    [ fableReferences ]
    |> Seq.concat
    |> Seq.iter (fun proj -> DotNetCli.Restore (fun p -> { p with Project = proj; ToolPath = dotnetExePath; AdditionalArgs = [ "--no-dependencies" ] }))
)

Target "NpmInstall" (fun _ ->
    Npm (fun p ->
        { p with Command = Install Standard; WorkingDirectory = fableDirectory })
)

Target "BuildFable" (fun _ ->
    fableReferences
    |> Seq.iter (fun proj -> DotNetCli.RunCommand (fun p -> { p with WorkingDir = fableDirectory; ToolPath = dotnetExePath }) ("fable npm-build " + proj))
)

Target "RunFable" (fun _ ->
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
RunTargetOrDefault "BuildFable"