module Caliburn.Micro

open Caliburn.Micro
open Microsoft.FSharp.Linq.QuotationEvaluation
open Microsoft.FSharp.Quotations
open System.Linq.Expressions
open System

type PropertyChangedBase with
    member self.NotifyOfPropertyChange (expr: Expr<'a>) =
        let body = expr.ToLinqExpression()
        let func = Expression.Lambda<Func<'a>>(body)
        self.NotifyOfPropertyChange func
