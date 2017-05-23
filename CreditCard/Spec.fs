namespace CreditCard

type Number = string

module Spec =
  type NumberRange(first: int, last:int) =
    member this.Matches (num: int) =
      num >= first && num <= last
    member this.First = first
    member this.Last = last

  module Prefix =
    type t = string -> Number -> bool
    let Matches (prefix: string) (s: Number) =
      s.StartsWith prefix
    let OfPrefixes(prefixes: string list) =
      List.map (fun prefix -> Matches prefix) prefixes

  module PrefixRange =
    type t = NumberRange -> int ->  Number -> bool
    let Matches (range: NumberRange) (len: int) (s: Number) =
      let prefix = (int) (s.[0..len-1])
      range.Matches(prefix)

  type Spec =
    | Spec of Spec list
    | PrefixSpec of Prefix.t
    | PrefixRangeSpec of PrefixRange.t

  type Matcher = Number -> bool

  type IBrand =
    abstract member Name : unit -> string
    abstract member Matches : Number -> bool

  let AD = List.append (Prefix.OfPrefixes ["3095"; "36"]) []

  let MakeCardBrand (name: string) (spec: Matcher list) = {
    new IBrand with
      member this.Name() = name
      member this.Matches(s: Number) = true
  }

  let VISA = MakeCardBrand "VISA" [Prefix.Matches "4"]
  let MasterCard = MakeCardBrand "MakeCard" [Prefix.Matches "5"]
  let AmericanExpress = MakeCardBrand "Amex" (Prefix.OfPrefixes ["34"; "37"])
  let JCB = MakeCardBrand "JCB" [PrefixRange.Matches (NumberRange(3528, 3589)) 4]
  let DinersClub = MakeCardBrand "Diners Club" (List.append (Prefix.OfPrefixes ["3095"; "36"]) [PrefixRange.Matches (NumberRange(300, 300)) 3; PrefixRange.Matches (NumberRange(38, 39)) 2])
