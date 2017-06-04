//
// Spec.fs
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

module CreditCard.Spec

open Core

type Matcher = CardNumber -> bool

let MatchAll (specs: Matcher list) (s: CardNumber) =
  let rec matches specs s =
    match specs with
      | [] -> true
      | hd::tail ->
        if hd s then matches tail s else false
  matches specs s

let MatchAny (specs: Matcher list) (s: CardNumber) =
  let rec matches specs s =
    match specs with
      | [] -> false
      | hd::tail ->
        if hd s then true else matches tail s
  matches specs s

let StartsWith (prefix: string) (s: CardNumber) = s.StartsWith prefix

let StartsWithOne (prefixes: string list) (s: CardNumber) =
  let matchers = (List.map (fun prefix -> StartsWith prefix) prefixes)
  MatchAny matchers s

let RangeOfDigits (range: int * int) (s: CardNumber) =
  let scope = RangeOfBIN range
  let at = scope.NumberOfDigits - 1
  let prefix = (s.[0..at]).Replace("*", "0") // FIXME Fill 0 except numbers
  scope.Contains((int) prefix)

let RangeOfDigitsOne (ranges: (int * int) list) (s: CardNumber) =
  let matchers = List.map (fun range -> RangeOfDigits range) ranges
  MatchAny matchers s

type IDigits =
  abstract member Length: int
  abstract member Format: CardNumber -> FormattedCardNumber

let Digits (digits: int list): IDigits = {
  new IDigits with
    member this.Length with get () = List.fold (fun sum v -> sum + v) 0 digits
    member this.Format(s: CardNumber) =
      let format (pos, formatted) (i, n) =
        let next_pos = pos + n
        let substring = s.[pos..(pos + n - 1)]
        if i = 0 then
          (next_pos, substring)
        else
          (next_pos, formatted + "-" + substring)
      List.fold format (0, "") (List.indexed digits) |> fun (_, r) -> r
  }

let LengthEquals (digits: IDigits) (s: CardNumber) =
  digits.Length = s.Length
