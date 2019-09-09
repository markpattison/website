module Website.Global

open Fable.React.Props

type Page =
  | Home
  | About

let toHash page =
  match page with
  | About -> "#about"
  | Home -> "#home"

let href route =
  Href (toHash route)
