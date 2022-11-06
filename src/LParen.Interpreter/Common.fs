module LParen.Interpreter.Common

open System.Collections.Generic

type Atom =
    | Integer of int
    | Symbol of string
    | List of Atom list
    | Lambda of Lambda
and
    Lambda = {
        Parameters: Atom list
        Body: Atom
        Environment: Environment
    }
and 
    Environment = {
        Symbols: Dictionary<Atom, Atom>
    }