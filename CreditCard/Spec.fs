namespace CreditCard

module Spec =
  type Matcher = CardNumber -> bool

  type NumberRange(first: int, last:int) =
    member this.Matches (num: int) =
      num >= first && num <= last
    member this.First = first
    member this.Last = last

  let Matches (specs: Matcher list) (s: CardNumber) =
    let rec matches specs s =
      match specs with
        | [] -> false
        | hd::tail ->
          if hd s then true else matches tail s
    matches specs s

  let Build (specs: Matcher list) = Matches specs

  let StartsWith (prefix: string) (s: CardNumber) = s.StartsWith prefix
  let StartsWithOne (prefixes: string list) (s: CardNumber) =
    Matches (List.map (fun prefix -> StartsWith prefix) prefixes) s
  
  let RangeOfDigits (spec: NumberRange * int) (s: CardNumber) =
    let range, len = spec
    let prefix = (int) (s.[0..len-1])
    range.Matches(prefix)

  let RangeOfDigitsOne (specs: (NumberRange * int) list) (s: CardNumber) =
    Matches (List.map (fun spec -> RangeOfDigits spec) specs) s
