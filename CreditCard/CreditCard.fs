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
    let prefix = (int) (number.[0..3])
    prefix >= 3528 && prefix <= 3589

module DinersClub =
  let name = "Diners club"

  let rangeMatches (prefixNumber:int) (first:int) (last:int) =
    prefixNumber >= first && prefixNumber <= last

  let intOfPrefix (number:string) (len:int) =
    (int) (number.[0..(len - 1)])

  let matches (number:string) =
    let range1 = rangeMatches (intOfPrefix number 3) 300 305
    let range2 = rangeMatches (intOfPrefix number 2) 38 39
    let startWith = number.StartsWith "3095" || number.StartsWith "36"
    range1 || range2 || startWith
