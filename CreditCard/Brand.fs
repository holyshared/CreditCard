//
// Brand.fs
//
// Author:
//       Noritaka Horio <holy.shared.design@gmail.com>
//
// Copyright (c) 2017 Noritaka Horio
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.

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

  let MasterCard = 
    Create "MasterCard" |>
    NumberDigits [4; 4; 4; 4] |>
    PrefixRules (MustBe (StartsWith "5"))

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
