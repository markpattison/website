module Home.View

open Fable.Core
open Fable.Core.JsInterop
open Fable.Helpers.React
open Fable.Helpers.React.Props

let root =
  div
    [ ClassName "content" ]
    [ h1
        []
        [ str "Mark's website" ]
      p
        []
        [
          str "This is a simple website to host my experiments with "
          a
            [ Href "http://fable.io"; Target "_blank" ]
            [ str "Fable" ]
          str "."
        ]
      p
        []
        [
          strong [] [ str "Cricket: " ]
          a
            [ Href "http://mark-cricket.azurewebsites.net"; Target "_blank" ]
            [ str "http://mark-cricket.azurewebsites.net" ]
        ]
      p
        []
        [
          strong [] [ str "Fractals: " ]
          a
            [ Href "http://mark-fractal.azurewebsites.net"; Target "_blank" ]
            [ str "http://mark-fractal.azurewebsites.net" ]
        ]
    ]