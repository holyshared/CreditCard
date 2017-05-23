namespace CreditCard

module Spec =
  type Matcher = CardNumber -> bool

  type NumberRange(first: int, last:int) =
    member this.Matches (num: int) =
      num >= first && num <= last
    member this.First = first
    member this.Last = last

  module Prefix =
    type t = string -> CardNumber -> bool
    let Matches (prefix: string) (s: CardNumber) =
      s.StartsWith prefix
    let OfPrefixes(prefixes: string list) =
      List.map (fun prefix -> Matches prefix) prefixes

  module PrefixRange =
    type t = NumberRange -> int ->  CardNumber -> bool
    let Matches (range: NumberRange) (len: int) (s: CardNumber) =
      let prefix = (int) (s.[0..len-1])
      range.Matches(prefix)
