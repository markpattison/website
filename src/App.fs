module Website.View

open Elmish
open Elmish.Navigation
open Elmish.UrlParser
open Fable.Core.JsInterop
open Fable.React
open Fulma

open Website.Types
open Website.State
open Website.Global

importAll "../sass/main.sass"

let menuItem label page currentPage =
  Menu.Item.li
    [ Menu.Item.IsActive (page = currentPage)
      Menu.Item.Props [ href page ] ]
    [ str label ]

let menu currentPage =
  Menu.menu []
    [ Menu.label []
        [ str "General" ]
      Menu.list []
        [ menuItem "Home" Home currentPage
          menuItem "About" About currentPage ] ]

let root model dispatch =

  let pageHtml = function
    | About -> Info.root
    | Home -> Home.root

  div []
    [ Navbar.root
      Section.section []
        [ Container.container []
            [ Columns.columns []
                [ Column.column
                    [ Column.Width (Screen.All, Column.Is3) ]
                    [ menu model.currentPage ]
                  Column.column []
                    [ pageHtml model.currentPage ] ] ] ] ]

open Elmish.React
open Elmish.Debug
open Elmish.HMR

// App
Program.mkProgram init update root
|> Program.toNavigable (parseHash pageParser) urlUpdate
#if DEBUG
|> Program.withDebugger
#endif
|> Program.withReactBatched "elmish-app"
|> Program.run
