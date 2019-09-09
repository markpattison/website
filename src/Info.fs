module Website.Info

open Fable.React
open Fulma

let root =
  Content.content []
    [ h1 []
        [ str "About this website" ]
      p []
        [ str "This template is a simple application build with Fable + Elmish + React." ] ]
