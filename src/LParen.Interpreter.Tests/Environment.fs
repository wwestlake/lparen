module LParen.Interpreter.Tests.Environment

open System.Collections.Generic
open Xunit

open LParen.Interpreter.Common
open LParen.Interpreter.Environment

[<Fact>]
let ``Can set and and find symbol in local environment`` () =
    let environment: Environment = { 
        Symbols = Dictionary<Atom, Atom>()
        Parent = None
    }
    
    environment.Symbols.Add(Symbol "Foo", Symbol "Bar")
    
    Assert.Equal(Some (Symbol "Bar"), find (Symbol "Foo") environment)
    
[<Fact>]
let ``find returns None when symbol does not exist`` () =
    let environment: Environment = { 
        Symbols = Dictionary<Atom, Atom>()
        Parent = None
    }
        
    Assert.Equal(None, find (Symbol "Foo") environment)
    
[<Fact>]
let ``Find locates symbol in parent environment`` () =
    let parentEnvironment: Environment = { 
        Symbols = Dictionary<Atom, Atom>()
        Parent = None
    }
    
    let childEnvironment: Environment = { 
        Symbols = Dictionary<Atom, Atom>()
        Parent = Some parentEnvironment
    }
    
    parentEnvironment.Symbols.Add(Symbol "Foo", Symbol "Bar")
    
    Assert.Equal(Some (Symbol "Bar"), find (Symbol "Foo") childEnvironment)
    
[<Fact>]
let ``Find returns lowest level environment symbol`` () =
    let parentEnvironment: Environment = { 
        Symbols = Dictionary<Atom, Atom>()
        Parent = None
    }
    
    let childEnvironment: Environment = { 
        Symbols = Dictionary<Atom, Atom>()
        Parent = Some parentEnvironment
    }
    
    childEnvironment.Symbols.Add(Symbol "Foo", Symbol "Barchild")
    parentEnvironment.Symbols.Add(Symbol "Foo", Symbol "Barparent")
    
    Assert.Equal(Some (Symbol "Barchild"), find (Symbol "Foo") childEnvironment)