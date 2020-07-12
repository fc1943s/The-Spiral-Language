﻿module Spiral.BlockParsing
open Spiral.ParserCombinators
open Spiral.Tokenize
type Range = {line : int; from : int; nearTo : int}
type Env = {
    tokens : (Range * SpiralToken) []
    comments : Tokenize.LineComment option []
    i : int ref
    semicolon_line : int
    symbol_line : int
    is_top_down : bool
    } with

    member t.Index with get() = t.i.contents and set(i) = t.i := i

let inline try_current_template (t : Env) on_succ on_fail =
    let i = t.Index
    if 0 <= i && i < t.tokens.Length then on_succ t.tokens.[i]
    else on_fail()

let inline try_current t f = try_current_template t f (fun () -> Error [])
let print_current t = try_current t (fun x -> printfn "%A" x; Ok()) // For parser debugging purposes.
let fail t er = try_current t (fun x -> Error [fst x,er])
let inline line_template t f = try_current_template t f (fun _ -> -1)
   
let skip' (t : Env) i = t.i := t.i.contents+i
let skip t = skip' t 1

let peek_special t =
    try_current t <| function
        | p, TokKeyword(t') -> Ok(t')
        | _ -> Error []

    //member d.PeekSpecial =
    //    d.TryCurrent <| function

    //member d.SkipSpecial(t) =
    //    d.TryCurrent <| function
    //        | TokSpecial(_,t') when t = t' -> d.Skip; Ok()
    //        | _ -> d.FailWith(ExpectedSpecial t)

    //member d.ReadUnaryOp =
    //    d.TryCurrent <| function
    //        | TokUnaryOperator(p,t') -> d.Skip; d.Ok(p,"~"+t')
    //        | _ -> d.FailWith(ExpectedUnaryOperator')

    //member d.ReadOp =
    //    d.TryCurrent <| function
    //        | TokOperator(p,t') -> d.Skip; d.Ok(p,t')
    //        | _ -> d.FailWith(ExpectedOperator')

    //member d.SkipOperator(t) =
    //    d.TryCurrent <| function
    //        | TokOperator(_,t') when t' = t -> d.Skip; Ok t'
    //        | _ -> d.FailWith(ExpectedOperator t)

    //member d.SkipUnaryOperator(t) =
    //    d.TryCurrent <| function
    //        | TokUnaryOperator(_,t') when t' = t -> d.Skip; Ok t'
    //        | _ -> d.FailWith(ExpectedUnaryOperator t)

    //member d.ReadSmallVar =
    //    d.TryCurrent <| function
    //        | TokSmallVar(p,t') -> d.Skip; d.Ok(p,t')
    //        | _ -> d.FailWith(ExpectedSmallVar)

    //member d.ReadBigVar =
    //    d.TryCurrent <| function
    //        | TokBigVar(p,t') -> d.Skip; d.Ok(p,t')
    //        | _ -> d.FailWith(ExpectedBigVar)

    //member d.ReadValue =
    //    d.TryCurrent <| function
    //        | TokValue(p,t') -> d.Skip; Ok(t')
    //        | _ -> d.FailWith(ExpectedLit)

    //member d.ReadDefaultValue =
    //    d.TryCurrent <| function
    //        | TokDefaultValue(p,t') -> d.Skip; d.Ok(p,t')
    //        | _ -> d.FailWith(ExpectedLit)

    //member d.ReadKeyword =
    //    d.TryCurrent <| function
    //        | TokKeyword(p,t') -> d.Skip; d.Ok(p,t')
    //        | _ -> d.FailWith(ExpectedKeyword)

    //member d.ReadKeywordUnary' =
    //    d.TryCurrent <| function
    //        | TokKeywordUnary(p,t') -> d.Skip; Ok(t')
    //        | _ -> d.FailWith(ExpectedKeywordUnary)


type SymbolString = string
type VarString = string

type Literal = Tokenize.Literal

type Op =
    // Type
    | TypeAnnot
    | TypeToVar
    | Box

    // Unsafe casts
    | UnsafeConvert

    // StringOps
    | StringLength
    | StringIndex
    | StringSlice

    // Tuple
    | TupleCreate

    // Record
    | RecordMap
    | RecordFilter
    | RecordFoldL
    | RecordFoldR
    | RecordLength

    // Braching
    | If
    | While

    // BinOps
    | Add
    | Sub
    | Mult 
    | Div 
    | Mod 
    | Pow
    | LTE
    | LT
    | EQ
    | NEQ
    | GT
    | GTE 
    | BoolAnd
    | BoolOr
    | BitwiseAnd
    | BitwiseOr
    | BitwiseXor
    | ShiftLeft
    | ShiftRight

    // Application related
    | Apply

    // Array
    | ArrayCreate
    | ArrayLength
    | ArrayIndex
    | ArrayIndexSet
   
    // Static unary operations
    | PrintStatic
    | ErrorNonUnit
    | ErrorType
    | ErrorPatMiss
    | LitIs
    | PrimIs
    | SymbolIs
    | EqType

    // UnOps
    | Neg
    | FailWith

    // Auxiliary math ops
    | Tanh
    | Log
    | Exp
    | Sqrt
    | NanIs

    // Infinity
    | Infinity

type PrimitiveType =
    | UInt8T
    | UInt16T
    | UInt32T
    | UInt64T
    | Int8T
    | Int16T
    | Int32T
    | Int64T
    | Float32T
    | Float64T
    | BoolT
    | StringT
    | CharT

type RawRecordTestPattern =
    | RawRecordTestSymbol of symbol: SymbolString * name: VarString
    | RawRecordTestInjectVar of var: VarString * name: VarString
and RawRecordWithPattern =
    | RawRecordWithSymbol of SymbolString * RawExpr
    | RawRecordWithSymbolModify of SymbolString * RawExpr
    | RawRecordWithInjectVar of VarString * RawExpr
    | RawRecordWithInjectSymbolModify of SymbolString * RawExpr
    | RawRecordWithoutSymbol of SymbolString
    | RawRecordWithoutInjectVar of VarString
and PatRecordMembersItem =
    | PatRecordMembersSymbol of symbol: SymbolString * name: Pattern
    | PatRecordMembersInjectVar of var: VarString * name: Pattern

and Pattern =
    | PatB
    | PatE
    | PatVar of VarString
    | PatOperator of VarString // Isn't actually a pattern. Is just here to help the inl/let statement parser.
    | PatBox of Pattern
    | PatAnnot of Pattern * RawTExpr

    | PatPair of Pattern * Pattern
    | PatSymbol of string * Pattern list
    | PatRecordMembers of PatRecordMembersItem list
    | PatActive of RawExpr * Pattern
    | PatUnion of SymbolString * Pattern list
    | PatOr of Pattern list
    | PatAnd of Pattern list
    | PatValue of Literal
    | PatDefaultValue of VarString
    | PatWhen of Pattern * RawExpr
    | PatNominal of VarString * Pattern

and RawExpr =
    | RawB
    | RawV of VarString
    | RawLit of Literal
    | RawDefaultLit of string
    | RawInline of RawExpr // Acts as a join point for the prepass specifically.
    | RawType of RawTExpr
    | RawInl of VarString * RawExpr
    | RawForall of VarString * RawExpr
    | RawSymbolCreate of SymbolString * RawExpr []
    | RawRecordWith of RawExpr [] * RawRecordWithPattern []
    | RawOp of Op * RawExpr []
    | RawJoinPoint of RawTExpr option * RawExpr
    | RawAnnot of RawTExpr * RawExpr
    | RawTypecase of RawTExpr * (RawTExpr * RawExpr) []
    | RawModuleOpen of VarString list * on_succ: RawExpr
    | RawLet of var: VarString * bind: RawExpr * on_succ: RawExpr
    | RawRecBlock of (VarString * RawExpr) [] * on_succ: RawExpr
    | RawPairTest of var0: VarString * var1: VarString * bind: VarString * on_succ: RawExpr * on_fail: RawExpr
    | RawSymbolTest of SymbolString * vars: VarString [] * bind: VarString * on_succ: RawExpr * on_fail: RawExpr
    | RawRecordTest of RawRecordTestPattern [] * bind: VarString * on_succ: RawExpr * on_fail: RawExpr
    | RawAnnotTest of RawTExpr * bind: VarString * on_succ: RawExpr * on_fail: RawExpr
    | RawValueTest of Literal * bind: VarString * on_succ: RawExpr * on_fail: RawExpr
    | RawDefaultLitTest of string * bind: VarString * on_succ: RawExpr * on_fail: RawExpr
    | RawUnionTest of name: SymbolString * vars: VarString [] * bind: VarString * on_succ: RawExpr * on_fail: RawExpr
    | RawBTest of bind: VarString * on_succ: RawExpr * on_fail: RawExpr
    | RawAnd of RawExpr
and RawTExpr =
    | RawTVar of VarString
    | RawTPair of RawTExpr * RawTExpr
    | RawTFun of RawTExpr * RawTExpr

    | RawTRecord of Map<string,RawTExpr>
    | RawTSymbol of SymbolString * RawTExpr []
    | RawTApply of RawTExpr * RawTExpr
    | RawTForall of (VarString * RawKindExpr) * RawTExpr
    | RawTB
    | RawTPrim of PrimitiveType
    | RawTArray of RawTExpr
and RawKindExpr =
    | RawKindStar
    | RawKindFun of RawKindExpr * RawKindExpr

let index (t : Env) = t.Index
let index_set v (t : Env) = t.Index <- v

let comments line_near_to character (s : Env) = 
    let rec loop line d =
        if line < 0 then 
            match s.comments.[line] with
            | Some(r,text) when r.from = character -> loop (line-1) (text :: d)
            | _ -> d
        else d
    loop (line_near_to-1) []
    |> String.concat "\n"
    |> fun x -> x.TrimEnd()

let top_let s = failwith "TODO" s
let top_inl s = failwith "TODO" s

let top_statement s =
    let i = index s
    let inline (+) a b = alt i a b
    (top_let + top_inl) s