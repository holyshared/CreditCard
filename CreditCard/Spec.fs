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

namespace CreditCard

module Spec =
  open Core

  type Matcher = CardNumber -> bool

  let Range (scope: int * int) =
    let first, last = scope
    NumberRange(first, last)

  let MatchAll (specs: Matcher list) (s: CardNumber) =
    List.forall (fun m -> m s) specs

  let MatchAny (specs: Matcher list) (s: CardNumber) =
    let rec matches specs s =
      match specs with
        | [] -> false
        | hd::tail ->
          if hd s then true else matches tail s
    matches specs s

  let And (m1: Matcher) (m2: Matcher) (s: CardNumber) =
    if m1 s then m2 s else false

  let StartsWith (prefix: string) (s: CardNumber) = s.StartsWith prefix

  let StartsWithOne (prefixes: string list) (s: CardNumber) =
    let matchers = (List.map (fun prefix -> StartsWith prefix) prefixes)
    MatchAny matchers s

  let RangeOfDigits (spec: NumberRange * int) (s: CardNumber) =
    let range, len = spec
    let prefix = (int) (s.[0..len-1])
    range.Matches(prefix)

  let RangeOfDigitsOne (specs: (NumberRange * int) list) (s: CardNumber) =
    let matchers = List.map (fun spec -> RangeOfDigits spec) specs
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
