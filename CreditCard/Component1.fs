namespace CreditCard

module VISA =
  let name = "VISA"

  let matches (number:string) =
    number.StartsWith "4"

module MasterCard =
  let name = "MasterCard"

  let matches (number:string) =
    number.StartsWith "5"

module AmericanExpress =
  let name = "Amex"

  let matches (number:string) =
    (number.StartsWith "34") || (number.StartsWith "37")

module JCB =
  let name = "JCB"

  let matches (number:string) =
    let prefix = (int) (number.Substring 4)
    prefix >= 3528 && prefix <= 3589

module DinersClub =
  let name = "Diners club"

  let rangeMatches (number:string) (len:int) (first:int) (last:int) =
    let prefix = (int) (number.Substring len)
    prefix >= first && prefix <= last

  let matches (number:string) =
    let range1 = rangeMatches number 3 300 305
    let range2 = rangeMatches number 2 38 39
    let startWith = number.StartsWith "3095" || number.StartsWith "36"
    range1 || range2 || startWith
