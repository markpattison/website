module Website.Types

open Website.Global

type Msg =
  | Nothing

type Model = {
    currentPage: Page
  }
