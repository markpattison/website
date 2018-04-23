module Home.View

open Fable.Core
open Fable.Core.JsInterop
open Fable.Helpers.React
open Fable.Helpers.React.Props

open Fulma

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
      Tile.ancestor []
        [
          Tile.parent [ Tile.IsVertical ]
            [
              Tile.child [ Tile.Size Tile.Is4 ]
                [
                  a
                    [ Href "/cricket"; Target "_blank" ]
                    [
                      Notification.notification
                        [ Notification.Color IsSuccess ]
                        [
                          Heading.p [] [ str "Cricket" ]
                          str """  A cricket "game" (or simulation)"""
                        ]
                    ]
                ]
              Tile.child [ Tile.Size Tile.Is4 ]
                [
                  a
                    [ Href "/fractals"; Target "_blank" ]
                    [
                      Notification.notification
                        [ Notification.Color IsInfo ]
                        [
                          Heading.p [] [ str "Fractals" ]
                          str "Draw fractals!"
                        ]
                    ]
                ]
            ]
        ]
    ]