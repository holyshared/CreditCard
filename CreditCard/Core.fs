namespace CreditCard

module Core =
  type CardNumber = string

  type FormattedCardNumber = string

  type NumberRange(first: int, last:int) =
    member this.Matches (num: int) =
      num >= first && num <= last
    member this.First = first
    member this.Last = last
