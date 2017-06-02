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

type NumberRange(first: int, last:int) =
  member this.Matches (num: int) =
    num >= first && num <= last
  member this.First = first
  member this.Last = last

let RangeOfBIN (first: int, last: int) =
  if ((string) first).Length = ((string) last).Length then
    raise (System.ArgumentException("The range of BIN is invalid"))
  else
    NumberRange(first, last)
