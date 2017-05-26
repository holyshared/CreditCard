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
  let JCB = MakeCardBrand "JCB" [RangeOfDigits (NumberRange(3528, 3589), 4)]
  let DinersClub = MakeCardBrand "Diners Club" [ (StartsWithOne ["3095"; "36"]); (RangeOfDigitsOne [(NumberRange(300, 305), 3); (NumberRange(38, 39), 2)]) ]

  let Detect (s: CardNumber) =
    let brands = [VISA; MasterCard; AmericanExpress; JCB; DinersClub]
    let rec detect (brands: IBrand list) =
      match brands with
        | [] -> false
        | hd::tail ->
          if hd.Matches(s) then true else detect tail
    detect brands
