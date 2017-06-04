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

module CreditCard.Brand

open Core
open Spec

type IBrand =
  abstract member Name : string
  abstract member Matches : CardNumber -> bool
  abstract member Format : CardNumber -> string
 
let Create (name: string) (digits: IDigits) (spec: Matcher) = {
  new IBrand with
    member this.Name with get () = name
    member this.Matches(s: CardNumber) = spec s
    member this.Format(s: CardNumber) = digits.Format(s)
  }

let VISA =
  let digits = Digits [4; 4; 4; 4]
  let matcher = And (LengthEquals digits) (StartsWith "4")
  Create "VISA" digits matcher

(*
  Current         : 510000 – 559999
  After July 2017 : 222100 – 272099

  http://newsroom.mastercard.com/asia-pacific/ja/news-briefs/bin-range/
*)
let MasterCard =
  let binRule = MatchAny ([RangeOfDigitsOne [(510000, 559999); (222100, 272099)]])
  let digits = Digits [4; 4; 4; 4]
  let matcher = And (LengthEquals digits) binRule
  Create "MasterCard" digits matcher

let AmericanExpress =
  let digits = Digits [4; 6; 5]
  let matcher = And (LengthEquals digits) (MatchAny [StartsWithOne ["34"; "37"]])
  Create "Amex" digits matcher

let JCB =
  let digits = Digits [4; 4; 4; 4]
  let matcher = And (LengthEquals digits) (MatchAny [RangeOfDigits (3528, 3589)])
  Create "JCB" digits matcher

let DinersClub =
  let digits = Digits [4; 6; 4]
  let binStartWith = MatchAny [(StartsWithOne ["3095"; "36"]); (RangeOfDigitsOne [(300, 305); (38, 39)])]
  let matcher = And (LengthEquals digits) binStartWith
  Create "Diners Club" digits matcher

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
