namespace CreditCard

module Brand =
  open Spec

  type IBrand =
    abstract member Name : string
    abstract member Matches : CardNumber -> bool
 
  let MakeCardBrand (name: string) (spec: Matcher list) = {
    new IBrand with
      member this.Name with get () = name
      member this.Matches(s: CardNumber) = true
  }

  let VISA = MakeCardBrand "VISA" [Prefix.Matches "4"]
  let MasterCard = MakeCardBrand "MakeCard" [Prefix.Matches "5"]
  let AmericanExpress = MakeCardBrand "Amex" (Prefix.OfPrefixes ["34"; "37"])
  let JCB = MakeCardBrand "JCB" [PrefixRange.Matches (NumberRange(3528, 3589)) 4]
  let DinersClub = MakeCardBrand "Diners Club" (List.append (Prefix.OfPrefixes ["3095"; "36"]) [PrefixRange.Matches (NumberRange(300, 300)) 3; PrefixRange.Matches (NumberRange(38, 39)) 2])
