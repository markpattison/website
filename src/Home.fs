module Website.Home

open Fable.React
open Fable.React.Props
open Fulma

let root =
  Content.content []
    [ h1 []
        [ str "Mark's website" ]
      p []
        [ str "This is a simple website to host my experiments with "
          a
            [ Href "http://fable.io"; Target "_blank" ]
            [ str "Fable" ]
          str "." ]
      Tile.ancestor []
        [ Tile.parent
            [ Tile.IsVertical; Tile.Size Tile.Is6 ]
            [ Tile.child []
                [ a
                    [ Href "/cricket" ]
                    [ Notification.notification
                        [ Notification.Color IsSuccess ]
                        [ Heading.p [] [ str "Cricket" ]
                          str """  A cricket "game" (or simulation)""" ] ] ]
              Tile.child []
                [ a
                    [ Href "/fractals" ]
                    [ Notification.notification
                        [ Notification.Color IsInfo ]
                        [ Heading.p [] [ str "Fractals" ]
                          str "Draw fractals!" ] ] ]
              Tile.child []
                [ a
                    [ Href "/fableelmishrecharts" ]
                    [ Notification.notification
                        [ Notification.Color IsWarning ]
                        [ Heading.p [] [ str "FableElmishRecharts" ]
                          str "An example app using Recharts" ] ] ] ] ] ]
