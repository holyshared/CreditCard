//
// Core.fs
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

module CreditCard.Core

type CardNumber = string

type FormattedCardNumber = string

type 'a IRange =
  abstract member First: 'a
  abstract member Last: 'a
  abstract member Contains: num: 'a -> bool

type NumberRange(first: int, last:int) =
  interface int IRange with
    member this.First with get () = first
    member this.Last with get () = last
    member this.Contains(num: int) =
      num >= first && num <= last

type BINRange(first: int, last:int) =
  let range = NumberRange(first, last)
  let numberOfDigits = (string first).Length
  member this.Contains (num: int) = (range :> int IRange).Contains(num)
  member this.First = (range :> int IRange).First
  member this.Last = (range :> int IRange).Last
  member this.NumberOfDigits = numberOfDigits

let RangeOfBIN (first: int, last: int) =
  if not (((string) first).Length = ((string) last).Length) then
    raise (System.ArgumentException("The range of BIN is invalid"))
  else
    BINRange(first, last)
