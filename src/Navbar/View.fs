module Navbar.View

open Fable.React
open Fable.React.Props

let navButton classy href faClass txt =
    p
        [ ClassName "control" ]
        [ a
            [ ClassName (sprintf "button %s" classy)
              Href href ]
            [ span
                [ ClassName "icon" ]
                [ i
                    [ ClassName (sprintf "fa %s" faClass) ]
                    [ ] ]
              span
                [ ]
                [ str txt ] ] ]

let navButtons =
    span
        [ ClassName "navbar-item" ]
        [ div
            [ ClassName "field is-grouped" ]
            [ navButton "twitter" "https://twitter.com/mark_pattison" "fa-twitter" "Twitter"
              navButton "github" "https://github.com/markpattison" "fa-github" "GitHub" ] ]

let root =
    nav
        [ ClassName "navbar is-dark" ]
        [ div
            [ ClassName "navbar-brand" ]
            [ h1
                [ ClassName "navbar-item title is-4" ]
                [ str "Mark's website" ] ]
          div
            [ ClassName "navbar-menu" ]
                [ div
                    [ ClassName "navbar-end" ]
                    [ navButtons ] ] ]
