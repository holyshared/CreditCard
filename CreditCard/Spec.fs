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

  module PrefixRange =
    type t = NumberRange -> int ->  Number -> bool
    let Matches (range: NumberRange) (len: int) (s: Number) =
      let prefix = (int) (s.[0..len-1])
      range.Matches(prefix)

  type Spec =
    | Spec of Spec list
    | PrefixSpec of Prefix.t
    | PrefixRangeSpec of PrefixRange.t

  module VISA =
    let spec =
      [Prefix.Matches "4"]

  module MasterCard =
    let spec =
      [Prefix.Matches "5"]

  module AmericanExpress =
    let spec =
      [Prefix.Matches "34"; Prefix.Matches "37"]

  module JCB =
    let spec =
      [PrefixRange.Matches (NumberRange(3528, 3589)) 4]

  module DinersClub =
    let spec =
      [
        Prefix.Matches "3095";
        Prefix.Matches "36";
        PrefixRange.Matches (NumberRange(300, 300)) 3;
        PrefixRange.Matches (NumberRange(38, 39)) 2;
      ]
