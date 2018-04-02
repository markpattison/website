module Navbar.View

open Fable.Helpers.React
open Fable.Helpers.React.Props

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
        [ ClassName "nav-item" ]
        [ div
            [ ClassName "field is-grouped" ]
            [ navButton "twitter" "https://twitter.com/mark_pattison" "fa-twitter" "Twitter"
              navButton "github" "https://github.com/markpattison/website" "fa-github" "Fork me" ] ]

let root =
    nav
        [ ClassName "nav" ]
        [ div
            [ ClassName "nav-left" ]
            [ h1
                [ ClassName "nav-item is-brand title is-4" ]
                [ str "Mark's website" ] ]
          div
            [ ClassName "nav-right" ]
            [ navButtons ] ]
