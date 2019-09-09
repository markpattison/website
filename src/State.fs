module Website.State

open Browser
open Elmish
open Elmish.Navigation
open Elmish.UrlParser

open Website.Global
open Website.Types

let pageParser: Parser<Page->Page,Page> =
  oneOf [
    map About (s "about")
    map Home (s "home")
  ]

let urlUpdate (result: Option<Page>) model =
  match result with
  | None ->
    console.error("Error parsing url")
    model,Navigation.modifyUrl (toHash model.currentPage)
  | Some page ->
      { model with currentPage = page }, []

let init result =
  let (model, cmd) =
    urlUpdate result
      { currentPage = Home }
  model, Cmd.batch [ cmd ]

let update msg model =
  match msg with
  | Nothing -> model, []
