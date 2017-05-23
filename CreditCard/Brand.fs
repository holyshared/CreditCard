namespace CreditCard

module Brand =
  open Spec

  type IBrand =
    abstract member Name : string
    abstract member Matches : CardNumber -> bool
 
  let MakeCardBrand (name: string) (specs: Matcher list) = {
    new IBrand with
      member this.Name with get () = name
      member this.Matches(s: CardNumber) = Matches specs s
  }

  let VISA = MakeCardBrand "VISA" [StartsWith "4"]
  let MasterCard = MakeCardBrand "MakeCard" [StartsWith "5"]
  let AmericanExpress = MakeCardBrand "Amex" [StartsWithOne ["34"; "37"]]
  let JCB = MakeCardBrand "JCB" [RangeOfDigits (NumberRange(3528, 3589)) 4]
  let DinersClub = MakeCardBrand "Diners Club" (List.append [StartsWithOne ["3095"; "36"]] [RangeOfDigits (NumberRange(300, 300)) 3; RangeOfDigits (NumberRange(38, 39)) 2])
