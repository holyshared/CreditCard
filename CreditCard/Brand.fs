namespace CreditCard

module Brand =
  open Core
  open Spec

  type IBrand =
    abstract member Name : string
    abstract member Matches : CardNumber -> bool
    abstract member Format : CardNumber -> string
 
  let Create (name: string) (digits: IDigits) (specs: Matcher list) = {
    new IBrand with
      member this.Name with get () = name
      member this.Matches(s: CardNumber) =
        let matcher = And (MatchAll [(LengthEquals digits)]) (MatchAny specs)
        matcher s
      member this.Format(s: CardNumber) = digits.Format(s)
  }
  let NumberDigits (digits: int list) f = f (Digits digits)
  let PrefixRules (specs: Matcher list) f = f specs

  let MustBe (m: Matcher) = [m]
  let Or (m: Matcher) (specs : Matcher list) = m::specs

  let VISA =
    Create "VISA" |>
    NumberDigits [4; 4; 4; 4] |>
    PrefixRules (MustBe (StartsWith "4"))

  (*
    Current         : 510000 – 559999
    After July 2017 : 222100 – 272099

    http://newsroom.mastercard.com/asia-pacific/ja/news-briefs/bin-range/
  *)
  let MasterCard =
    let rangeRules = [
      (NumberRange(510000, 559999), 6);
      (NumberRange(222100, 272099), 6);
    ]
    let rule = RangeOfDigitsOne rangeRules
    Create "MasterCard"
      |> NumberDigits [4; 4; 4; 4]
      |> PrefixRules [rule]

  let AmericanExpress =
    Create "Amex" |> 
    NumberDigits [4; 6; 5] |>
    PrefixRules (MustBe (StartsWithOne ["34"; "37"]))

  let JCB =
    Create "JCB" |> 
    NumberDigits [4; 4; 4; 4] |>
    PrefixRules (MustBe (RangeOfDigits (NumberRange(3528, 3589), 4)))

  let DinersClub =
    Create "Diners Club" |> 
    NumberDigits [4; 6; 4] |>
    PrefixRules (
      MustBe (StartsWithOne ["3095"; "36"]) |>
      Or (RangeOfDigitsOne [(NumberRange(300, 305), 3); (NumberRange(38, 39), 2)])
    )

  let SupportBrands = [VISA; MasterCard; AmericanExpress; JCB; DinersClub]

  let DetectFrom (brands: IBrand list) (s: CardNumber) =
    let rec detect (brands: IBrand list) =
      match brands with
        | [] -> None
        | hd::tail ->
          if hd.Matches(s) then Some hd else detect tail
    detect brands

  let Detect (s: CardNumber) =
    DetectFrom SupportBrands s
