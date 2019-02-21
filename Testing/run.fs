#if INTERACTIVE
#r @"..\packages\FParsec.1.0.3\lib\net40-client\FParsecCs.dll"
#r @"..\packages\FParsec.1.0.3\lib\net40-client\FParsec.dll"
#r @"..\packages\ManagedCuda-CUBLAS.8.0.22\lib\net46\CudaBlas.dll"
#r @"..\packages\ManagedCuda-CURAND.8.0.22\lib\net46\CudaRand.dll"
#r @"Microsoft.CSharp"
#r @"..\The Spiral Language\bin\Release\The_Spiral_Language.dll"
#endif

open Spiral.Lib
open Spiral.Tests
open System.IO
open System.Diagnostics
open Spiral.Types

let cfg = Spiral.Types.cfg_testing

let example: SpiralModule =
    {
    name="example"
    prerequisites=[]
    description="Module description."
    code=
    """
// https://gist.github.com/SimonDanisch/812e01d7183681ed414932c8f7b533d0#file-unroll-jl
// This is not quite the same as the above example, but Spiral's staging optimizations
// only work when code is written in a functionally pure manner. 

// `sin` currently is not a part of the core library so I use a macro to interop with it.
// If it were then this whole program would be evaluated at compile time.
// To see that, replace sin with tanh or sqrt for example.
// Spiral's semantics allow for any functionally pure computation at compile time.
inl sin x = macro.fs float64 [text:"sin"; args: x]

inl rec unroll f i s =
    assert (lit_is i) "i must be a literal."
    if i > 0 then unroll f (i-1) (f s) else s

inl test () =
    inl box = 1.0
    unroll sin 3 box

test ()

//let (var_0: float) = sin(1.000000)
//let (var_1: float) = sin(var_0)
//sin(var_1)
    """
    }

let example2: SpiralModule =
    {
    name="example2"
    prerequisites=[tuple; console]
    description="Module description."
    code=
    """
inl rec foo n = 
    assert (lit_is n) "n must be known at compile time."
    assert (n >= 0) "n must greater or equal to zero."
    if n = 0 then ()
    else (if n % 2 = 0 then int32 else string) :: foo (n - 1)

inl stringify = function _: int32 -> "int" | _ : string -> "string"
foo 10 |> Tuple.map stringify |> Console.writeline
    """
    }

let example3: SpiralModule =
    {
    name="example3"
    prerequisites=[]
    description="Module description."
    code=
    """
inl x = .blocke
// Combination of not `!` and injection `$` patterns.
// Since the module does not have the member `blocke`, the case returns "asd" at compile time.
match {block=()} with 
| {!($x)} -> "asd"
| _ -> "qwe"
    """
    }

let example4: SpiralModule =
    {
    name="parser_test"
    prerequisites=[loops; parsing]
    description="Parser test for compilation speed."
    code=
    """
Loops.for {static_from=0; near_to=40; body=inl {i} ->
    Parsing.sprintf "%i, %i, %i" 1 2 3 |> ignore
    Parsing.sprintf "%i, %i, %i, %i" 1 2 3 4 |> ignore
    Parsing.sprintf "%i, %i, %i, %i, %i" 1 2 3 5 6 |> ignore
    Parsing.sprintf "(%s, %i, %f)" "asd" 11 3.3 |> ignore
    }
    """
    }

let test1: SpiralModule =
    {
    name="test1"
    prerequisites=[]
    description="Does it run?"
    code=
    """
inl a = 5
inl b = 10
a + b
    """
    }

let test2: SpiralModule =
    {
    name="test2"
    prerequisites=[]
    description="Do the join points work?"
    code=
    """
inl a () = join 5
inl b () = join 10
a () + b ()
    """
    }

let test3: SpiralModule =
    {
    name="test3"
    prerequisites=[]
    description="Does `dyn` work?"
    code=
    """
inl a = dyn 5
inl b = dyn 10
a + b
    """
    }

let test4: SpiralModule =
    {
    name="test4"
    prerequisites=[]
    description="Does the and pattern work correctly?"
    code=
    """
inl f (a, b) (c, d) = dyn (a+c,b+d)
inl q & (a, b) = dyn (1,2)
inl w & (c, d) = dyn (3,4)
f q w
    """
    }

let test5: SpiralModule =
    {
    name="test5"
    prerequisites=[]
    description="Does basic pattern matching work?"
    code=
    """
inl f = function
    | .Add x y -> join (x + y)
    | .Sub x y -> join (x - y)
    | .Mult x y -> join (x * y)
inl a = f .Add 1 2
inl b = f .Sub 1 2
inl c = f .Mult 1 2
a, b, c
    """
    }

let test6: SpiralModule =
    {
    name="test6"
    prerequisites=[]
    description="Does returning type level methods from methods work?"
    code=
    """
inl min n = join
    inl tes a = join
        inl b -> join
            inl c -> join
                inl d -> join a,b,c
    tes 1 2 (2.2,3,4.5)
min 10
    """
    }

let test7: SpiralModule =
    {
    name="test7"
    prerequisites=[]
    description="Do active patterns work?"
    code=
    """
inl f op1 op2 op3 = function
    | !op1 (.Some, x) -> x
    | !op2 (.Some, x) -> x
    | !op3 (.Some, x) -> x

inl add = function
    | .Add -> .Some, inl x y -> x + y
    | _ -> .None
inl sub = function
    | .Sub -> .Some, inl x y -> x - y
    | _ -> .None
inl mult = function
    | .Mult -> .Some, inl x y -> x * y
    | _ -> .None

inl f = f add sub mult

inl a = f .Add 1 2
inl b = f .Sub 1 2
inl c = f .Mult 1 2
dyn (a, b, c)
    """
    }

let test8: SpiralModule =
    {
    name="test8"
    prerequisites=[]
    description="Does the basic union type work?"
    code=
    """
inl option_int = .Some, (1,2,3) \/ .None

inl x = join Type (box: .None to: option_int)
match x with
| #(.Some, x) -> x 
| #(.None) -> 0,0,0
    """
    }

let test9: SpiralModule =
    {
    name="test9"
    prerequisites=[]
    description="Does the partial evaluator optimize unused match cases?"
    code=
    """
inl ab = .A \/ .B
inl ab x = Type (box: x to: ab)
inl #a,#b,#c = join (ab .A, ab .A, ab .A)
match a,b,c with
| .A, _, _ -> 1
| _, .A, _ -> 2
| _, _, .A -> 3
| _ -> 4
    """
    }

let test10: SpiralModule =
    {
    name="test10"
    prerequisites=[]
    description="Do the join points get filtered?"
    code=
    """
inl ab x = Type (box: x to: (.A \/ .B))
inl #a,#b,#c,#d = join (ab .A, ab .A, ab .A, ab .A)
match a,b,c,d with
| .A, .A, _, _ -> join 1
| _, _, .A, .A -> join 2
| .A, .B, .A, .B -> join 3
| _ -> join 4
    """
    }

let test11: SpiralModule =
    {
    name="test11"
    prerequisites=[]
    description="Do the nested patterns work?"
    code=
    """
inl a = type (1,2)
inl b = type (1,a,a)
inl a,b = (inl x -> Type (box: x to: a)), (inl x -> Type (box: x to: b))
inl x = b (1, a (2,3), a (4,5))
match x with
| _, (x, _), (_, y) -> x + y
| _, _, _ -> 0
| _ :: () -> 0
    """
    }

let test12: SpiralModule =
    {
    name="test12"
    prerequisites=[]
    description="Does recursive pattern matching work on static data?"
    code=
    """
inl rec p = function
    | .Some, x -> p x
    | .None -> 0
p (.Some, .None)
    """
    }

let test13: SpiralModule =
    {
    name="test13"
    prerequisites=[]
    description="A more complex interpreter example on static data."
    code=
    """
inl rec expr x = 
    Type (
        name: "Arith"
        join:inl _ ->
            v: x
            \/ add: expr x, expr x
            \/ mult: expr x, expr x
        )
inl int_expr x = Type (box: x to: expr 0)
inl v x = int_expr (v: x)
inl add a b = int_expr (add: a, b)
inl mult a b = int_expr (mult: a, b)
inl a = add (v 1) (v 2)
inl b = add (v 3) (v 4)
inl c = mult a b

inl rec interpreter_static #x = 
    match x with
    | v: x -> x
    | add: a, b -> interpreter_static a + interpreter_static b
    | mult: a, b -> interpreter_static a * interpreter_static b
interpreter_static c
|> dyn
    """
    }

let test14: SpiralModule =
    {
    name="test14"
    prerequisites=[]
    description="Does recursive pattern matching work on partially static data?"
    code=
    """
inl rec expr x = 
    Type (
        name: "Arith"
        join:inl _ ->
            v: x
            \/ add: expr x, expr x
            \/ mult: expr x, expr x
        )
inl int_expr x = Type (box: x to: expr 0)
inl v x = int_expr (v: x)
inl add a b = int_expr (add: a, b)
inl mult a b = int_expr (mult: a, b)
inl a = add (v 1) (v 2)
inl b = add (v 3) (v 4)
inl c = dyn (mult a b)

inl rec inter x = join
    inl #x = x
    match x with
    | v: x -> x
    | add: a, b -> inter a + inter b
    | mult: a, b -> inter a * inter b
    : 0

inter c
    """
    }

let test15: SpiralModule =
    {
    name="test15"
    prerequisites=[extern_]
    description="Does basic .NET interop work?"
    code=
    """
() // TODO
//open Extern
//inl builder_type = fs [text: "System.Text.StringBuilder"]
//inl b = FS.Constructor builder_type ("Qwe", 128i32)
//inl a x =
//    FS.Method b .Append x builder_type |> ignore
//    FS.Method b .AppendLine () builder_type |> ignore
//a 123
//a 123i16
//a "qwe"
//inl str = FS.Method b .ToString () string

//inl console = fs [text: "System.Console"]
//FS.StaticMethod console .Write str ()

//inl key = 1, 1
//inl value = {a=1;b=2;c=3}
//inl dictionary_type = fs [text: "System.Collections.Generic.Dictionary"; types: type (key,value)]
//inl dict = FS.Constructor dictionary_type 128i32
//FS.Method dict .Add (key,value) ()
//FS.Method dict .Item (key :: ()) value |> dyn |> ignore
//0
    """
    }

let test16: SpiralModule =
    {
    name="test16"
    prerequisites=[]
    description="Do var union types work?"
    code=
    """
inl t = 0 \/ 0.0
if dyn true then Type (box: 0 to: t)
else Type (box: 0.0 to: t)
    """
    }

let test17: SpiralModule =
    {
    name="test17"
    prerequisites=[]
    description="Do modules work?"
    code=
    """
inl m =
    inl x = 2
    inl y = 3.4
    inl z = "123"
    {x y z}
dyn (m.x, m.y, m.z)
    """
    }

let test18: SpiralModule =
    {
    name="test18"
    prerequisites=[]
    description="Do arrays and references work?"
    code=
    """
() // TODO
//inl a = ref 0
//a := 5
//a() |> ignore

//inl a = ref () // Is not supposed to be printed due to being ().
//a := ()
//a()

//inl a = ref <| term_cast (inl a, b -> a + b) (int64,int64)
//a := term_cast (inl a, b -> a * b) (int64,int64)
//a() |> ignore

//inl a = array_create int64 10
//a 3 <- 2
//a 3 |> ignore

//inl a = array_create id 3 // Is supposed to be unit and not printed.
//a 1 <- id
//a 1 |> ignore
    """
    }

let test19: SpiralModule =
    {
    name="test19"
    prerequisites=[]
    description="Does the term casting of functions work?"
    code=
    """
inl add a b (c, (d, e), f) = a + b + c + d + e + f
inl f = Type (term_cast: add 8 (dyn 7) with: type (0,(0,0),0))
f (1,(2,5),3)
    """
    }

let test20: SpiralModule =
    {
    name="test20"
    prerequisites=[]
    description="Does pattern matching on union non-tuple types work? Do type annotation patterns work?"
    code=
    """
inl t = 0 \/ 0.0
inl #x = Type (box: 3.5 to: t)
match x with
| x : 0 -> x * x
| x : 0.0 -> x + x
|> dyn
    """
    }

let test21: SpiralModule =
    {
    name="test21"
    prerequisites=[]
    description="Does defining user operators work?"
    code=
    """
inl (.+) a b = a + b
inl x = 2 * 22 .+ 33

inl f op a b = op a b
f (*) 2 x
    """
    }

let test22: SpiralModule =
    {
    name="test22"
    prerequisites=[]
    description="Do unary operators work?"
    code=
    """
() // TODO: Slated for removal.
//inl t1 x = dyn <| -x
//inl t3 x = .(x)
//t1 2.2, t3 "asd"
    """
    }

let test23: SpiralModule =
    {
    name="test23"
    prerequisites=[]
    description="Do when and as patterns work?"
    code=
    """
inl f = function
    | a,b,c as q when a < 10 -> q
    | _ -> 0,0,0
dyn (f (1,2,3))
    """
    }

let test24: SpiralModule =
    {
    name="test24"
    prerequisites=[]
    description="Do literal pattern matchers work? Does partial evaluation of equality work?"
    code=
    """
inl f x = 
    match x with
    | 0 -> "0", x
    | 1 -> "1", x
    | false -> "false", x
    | true -> "true", x
    | "asd" -> "asd", x
    | 1i8 -> "1i8", x
    | 5.5 -> "5.5", x
    | _ -> "unknown", x

dyn (f 0, f 1, f false, f true, f "asd", f 1i8, f 5.5, f 5f64)
    """
    }

let test25: SpiralModule =
    {
    name="test25"
    prerequisites=[]
    description="Does the tuple cons pattern work?"
    code=
    """
inl f = function x1 :: x2 :: x3 :: xs -> 3 | x1 :: x2 :: xs -> 2 | x1 :: xs -> 1 | () -> 0

dyn (f (), f (1 :: ()), f (1,2))
    """
    }

let test26: SpiralModule =
    {
    name="test26"
    prerequisites=[tuple]
    description="Does tuple map work? This also tests rev and foldl."
    code=
    """
// TODO
//Tuple.map (inl x -> x * 2) (1,2,3)
    """
    }

let test27: SpiralModule =
    {
    name="test27"
    prerequisites=[tuple]
    description="Do tuple zip and unzip work?"
    code=
    """
// TODO
//inl j = 2,3.3
//inl k = 4.4,55
//inl l = 66,77
//inl m = 88,99
//inl n = 123,456
//Tuple.zip ((j,k),(l,m),n) |> Tuple.unzip
    """
    }

let test28: SpiralModule =
    {
    name="test28"
    prerequisites=[extern_]
    description="Does string indexing work?"
    code=
    """
// TODO
//open Extern
//inl console_type = fs [text: "System.Console"]
//inl a = "qwe"
//inl b = FS.StaticMethod console_type .ReadLine() string
//a(0),b(0)
    """
    }

let test29: SpiralModule =
    {
    name="test29"
    prerequisites=[]
    description="Does pattern matching work redux?"
    code=
    """
inl t = 0, 0 \/ 0

inl #x = Type (box: 1,1 to: t) |> dyn
match x with
| a,b -> 0
| c -> c
    """
    }

let test30: SpiralModule =
    {
    name="test30"
    prerequisites=[]
    description="Do recursive algebraic datatypes work?"
    code=
    """
inl rec List x = 
    Type (
        name: "List"
        join: inl _ -> .nil \/ cons: x, List x
        )

inl t x = Type (box: x to: List 0)
inl nil = t .nil
inl cons x xs = t (cons: x, xs)

inl rec sum (!dyn s) l = join
    inl #l = l
    match l with
    | cons: x, xs -> sum (s + x) xs
    | .nil -> s
    : 0

nil |> cons 3 |> cons 2 |> cons 1 |> dyn |> sum 0
        """
    }

let test31: SpiralModule =
    {
    name="test31"
    prerequisites=[]
    description="Does passing types into types work?"
    code=
    """
inl a = .A, (0, 0) \/ .B, ""
inl b = a \/ .Hello
inl box a #b = Type (box: b to: a)
(.A, (2,3)) |> box a |> dyn |> box b
    """
    }

let test32: SpiralModule =
    {
    name="test32"
    prerequisites=[extern_]
    description="Do the .NET methods work inside methods?"
    code=
    """
() TODO
//open Extern
//inl convert_type = fs [text: "System.Convert"]
//inl to_int64 x = FS.StaticMethod convert_type .ToInt64 x int64
//met f = to_int64 (dyn 'a')
//f
    """
    }

let test33: SpiralModule =
    {
    name="test33"
    prerequisites=[]
    description="Do the module map and fold functions work?"
    code=
    """
inl m = {a=1;b=2;c=3}
inl m' = Record (map:inl (key:value:) -> value * 2) m
dyn (m', Record (foldl: inl (key:state:value:) -> state + value) 0 m')
    """
    }

let test34: SpiralModule =
    {
    name="test34"
    prerequisites=[]
    description="Does a simple stackified function work?"
    code=
    """
inl a = dyn 1
inl b = dyn 2
inl add c d = a + b + c + d
inl f g c d = join g c d
f (stack add) (dyn 3) (dyn 4)
    """
    }

let test35: SpiralModule =
    {
    name="test35"
    prerequisites=[]
    description="Does case on union types with recursive types work properly?"
    code=
    """
inl rec List x = 
    Type (
        name: "List"
        join: inl _ -> .nil \/ cons: x, List x
        )

inl Res =
    0
    \/ 0, 0
    \/ List 0

inl #x = Type (box: 1 to:Res) |> dyn
match x with
| _ : 0 -> 1
| (a, b) -> 2
| _ : (List 0) -> 3
    """
    }

let test36: SpiralModule =
    {
    name="test36"
    prerequisites=[]
    description="Does a simple heapified function work?"
    code=
    """
inl a = dyn 1
inl b = dyn 2
inl add c d = a + b + c + d
inl f g c d = join g c d
f (heap add) (dyn 3) (dyn 4)
    """
    }

let test37: SpiralModule =
    {
    name="test37"
    prerequisites=[]
    description="Does a simple heapified module work?"
    code=
    """
inl m = heap { a=dyn 1; b=dyn 2 }
inl add c d = 
    inl {a b} = indiv m
    a + b + c + d
inl f g c d = join g c d
f (heap: add) (dyn 3) (dyn 4)
    """
    }

let test38: SpiralModule =
    {
    name="test38"
    prerequisites=[]
    description="Is type constructor of an int64 an int64?"
    code=
    """
Type (box: dyn 1 to: 0)
    """
    }

let test39: SpiralModule =
    {
    name="test39"
    prerequisites=[]
    description="Does the mutable layout type get unpacked multiple times?"
    code=
    """
inl box a b = Type (box: b to: a)
inl q = heapm <| dyn {a=1;b=2;c=3} \/ heapm <| dyn {a=1;b=2} \/ heap <| dyn (1,2,3)
inl #x = {a=1;b=2;c=3} |> dyn |> heapm |> box q |> dyn
match indiv x with
| {a} as x ->
    inl {b} = x
    match x with
    | {c} -> a+b+c
    | _ -> a+b
| a,b,c -> a*b*c
    """
    }

let test40: SpiralModule =
    {
    name="test40"
    prerequisites=[]
    description="Does this compile into just one method? Are the arguments reversed in the method call?"
    code=
    """
inl rec f a b = join
    if dyn true then f b a
    else a + b
    : 0
f (dyn 1) (dyn 2)
    """
    }

let test41: SpiralModule =
    {
    name="test41"
    prerequisites=[]
    description="Does result in a `type ()`?"
    code=
    """
inl ty = .Up \/ .Down \/ heap <| dyn {q=1;block=(1,(),3)}
inl r =
    inl #x = dyn (Type (box: .Up to: ty))
    match x with
    | .Up -> {q=1;block=(1,(),3)}
    | .Down -> {q=2;block=(2,(),4)}
    | _ -> {q=1;block=(1,(),3)}
Type (box: heap r to: ty)
    """
    }

let test43: SpiralModule =
    {
    name="test43"
    prerequisites=[array]
    description="Do the Array constructors work?"
    code=
    """
// TODO
//open Array

//empty int64, singleton 2.2
    """
    }

let test46: SpiralModule =
    {
    name="test46"
    prerequisites=[]
    description="Does the module pattern work?"
    code=
    """
inl f {a b c} = a + b + c
inl x =
    {
    a=1
    b=2
    c=3
    }

dyn (f {x with a = 4})
    """
    }

let test47: SpiralModule =
    {
    name="test47"
    prerequisites=[]
    description="Does the nested module pattern work?"
    code=
    """
inl f {name p={x y}} = name,(x,y)
inl x = { name = "Coord" }

f {x with 
    p = { x = 1
          y = 2 }}
|> dyn
    """
    }

let test48: SpiralModule =
    {
    name="test48"
    prerequisites=[]
    description="Does the nested module pattern with rebinding work?"
    code=
    """
inl f {name p={y=y' x=x'}} = name,(x',y')
inl x = { name = "Coord" }
f {x with 
    p = { x = 1
          y = 2 }}
|> dyn
    """
    }

let test49: SpiralModule =
    {
    name="test49"
    prerequisites=[]
    description="Does the lens pattern work? Does self work? Does the semicolon get parsed properly?"
    code=
    """
inl x = { a = { b = { c = 3 } } }

inl f {a={b={c q}}} = c,q
f {x.a.b with q = 4; c = this + 3; d = {q = 12; w = 23}}
|> dyn
    """
    }

let test50: SpiralModule =
    {
    name="test50"
    prerequisites=[array]
    description="Do the Array init and fold work?"
    code=
    """
// TODO
open Array

inl ar = init 6 (inl x -> x+1)
foldl (+) (dyn 0) ar, foldr (*) ar (dyn 1)
    """
    }

let test51: SpiralModule =
    {
    name="test51"
    prerequisites=[array]
    description="Do the Array map and filter work?"
    code=
    """
// TODO
open Array

inl ar = init 16 id
map ((*) 2) ar
|> filter ((<) 15)
    """
    }

let test52: SpiralModule =
    {
    name="test52"
    prerequisites=[array]
    description="Does the Array concat work?"
    code=
    """
// TODO
open Array

inl ar = init 4 (inl _ -> init 8 id)
concat ar
    """
    }

let test53: SpiralModule =
    {
    name="test53"
    prerequisites=[array]
    description="Does the Array append work?"
    code=
    """
// TODO
open Array

inl ar = inl _ -> init 4 id
append (ar (), ar (), ar())
    """
    }

let test54: SpiralModule =
    {
    name="test54"
    prerequisites=[tuple]
    description="Does the monadic bind `inm` work?"
    code=
    """
// TODO
inl on_succ a = (a,())
inl on_log x = ((),Tuple.singleton x)
inl (>>=) (a,w) f = // The writer monad.
    inl a',w' = f a
    (a',Tuple.append w w')

inl add x y = x + y |> on_succ

inm x = add 1 1
inm _ = on_log x
inm y = add 3 4
inm _ = on_log y
inm z = add 5 6
inm _ = on_log z
on_succ (x+y+z) // Tuple2(20L, Tuple1(2L, 7L, 11L))
    """
    }

let test56: SpiralModule =
    {
    name="test56"
    prerequisites=[]
    description="Does term casting with an unit return get printed properly?"
    code=
    """
inl add a, b = ()
inl k = Type (term_cast: add with: 0,0)
k (1, 2)
    """
    }

let test57: SpiralModule =
    {
    name="test57"
    prerequisites=[]
    description="Does the new module creation syntax work?"
    code=
    """
inl a = 1
inl b = 2
inl d = 4
dyn {a b c = 3; d; e = 5}
    """
    }

let test58: SpiralModule =
    {
    name="test58"
    prerequisites=[array]
    description="Does the fold function get duplicated?"
    code=
    """
// TODO
inl ar = array_create (int64,int64) 128
Array.foldl (inl a,b c,d -> a+c,b+d) (dyn (1,2)) ar
|> inl a,b -> a*b
    """
    }

let test60: SpiralModule =
    {
    name="test60"
    prerequisites=[]
    description="Is the trace being correctly propagated for TyTs?"
    code=
    """
inl a = dyn 1
inl b = dyn 2
inl c = dyn 3
4 + type 0
    """
    }

let test61: SpiralModule =
    {
    name="test61"
    prerequisites=[]
    description="Does the partial evaluation of if statements work?"
    code=
    """
inl x = dyn false
inl _ = dyn (x && (x || x && (x || x)))
inl _ = dyn ((x && x || x) || (x || true))
inl _ = dyn (if x then false else x)
dyn (if x then false else true)

//let ((var_1 : bool)) = false
//let ((var_2 : bool)) = true
//let ((var_3 : bool)) = false
//let ((var_4 : bool)) = var_1 = false
    """
    }

let test62: SpiralModule =
    {
    name="test62"
    prerequisites=[]
    description="Do && and || work correctly?"
    code=
    """
inl a,b,c,d,e = dyn (true, false, true, false, true)
a && b || c && d || e
    """
    }

let test63: SpiralModule =
    {
    name="test63"
    prerequisites=[list]
    description="Do the list constructors work?"
    code=
    """
// TODO
open List
cons 1 (cons 2 (singleton 3))
    """
    }

let test64: SpiralModule =
    {
    name="test64"
    prerequisites=[tuple]
    description="Do the tuple foldl_map and foldr_map work?"
    code=
    """
// TODO
inl l = 2,3,4
{
a = Tuple.foldl_map (inl s x -> x*x, s + x*x) 0 l
b = Tuple.foldr_map (inl x s -> x*x, s + x*x) l 0
}
    """
    }

let test65: SpiralModule =
    {
    name="test65"
    prerequisites=[tuple; list]
    description="Do the list module folds work?"
    code=
    """
// TODO
open List

foldl (+) (dyn 0.0) (dyn (empty float64)),
foldr (+) (dyn (empty float64)) (dyn 0.0f64)
    """
    }

let test66: SpiralModule =
    {
    name="test66"
    prerequisites=[tuple; list]
    description="Does the list module concat (and by extension append) work?"
    code=
    """
// TODO
open List

inl a = cons 3 () |> cons 2 |> cons 1 |> dyn
inl b = cons 6 () |> cons 5 |> cons 4 |> dyn
inl c = dyn (cons a (singleton b))
concat c
    """
    }

let test67: SpiralModule =
    {
    name="test67"
    prerequisites=[tuple; list]
    description="Does the list module map work?"
    code=
    """
// TODO
open List

inl a = cons 3 () |> cons 2 |> cons 1 |> dyn

map ((*) 2) a
    """
    }

let test68: SpiralModule =
    {
    name="test68"
    prerequisites=[tuple; list]
    description="Is it possible to make a list of lists?"
    code=
    """
// TODO
open List

inl a = empty int64 |> dyn
empty a
    """
    }

let test69: SpiralModule =
    {
    name="test69"
    prerequisites=[tuple; list]
    description="Does the list module init work?"
    code=
    """
// TODO
open List

init 10 (inl x -> 2.2)
    """
    }

let test70: SpiralModule =
    {
    name="test70"
    prerequisites=[]
    description="Does the argument get printed on a type error?"
    code=
    """
inl a : 0f64 = 5
()
    """
    }

let test71: SpiralModule =
    {
    name="test71"
    prerequisites=[]
    description="Does the recent change to error printing work? This one should give an error."
    code=
    """
55 + id
    """
    }

let test78: SpiralModule =
    {
    name="test78"
    prerequisites=[tuple]
    description="Do the tuple scan functions work?"
    code=
    """
// TODO
inl x = 1,2,3,4
Tuple.scanl (+) 0 x, Tuple.scanr (+) x 0
    """
    }

let test79: SpiralModule =
    {
    name="test79"
    prerequisites=[host_tensor]
    description="Does the Tensor init work? Do set and index for the new array module work?"
    code=
    """
// TODO
inl tns = Tensor.init (10,10) (inl a b -> a*b)
inl x = tns 2 2 .get
tns 2 2 .set (x+100)
tns 2 2 .get
    """
    }

let test80: SpiralModule =
    {
    name="test80"
    prerequisites=[queue; console]
    description="Does the Queue module work?"
    code=
    """
// TODO
open Console
open Queue
inl queue = create int64 1
inl rec dequeue' n =
    if n > 0 then dequeue queue |> writeline; dequeue' (n-1)
    else ()
Tuple.iter (enqueue queue) (Tuple.range (1,4))
dequeue' 2
Tuple.iter (enqueue queue) (Tuple.range (1,4))
Tuple.iter (enqueue queue) (Tuple.range (1,4))
dequeue' 2
dequeue' 4
dequeue' 4
    """
    }

let test81: SpiralModule =
    {
    name="test81"
    prerequisites=[]
    description="Does structural polymorphic equality work?"
    code=
    """
{a=1;b=dyn 2;c=dyn 3} = {a=1;b=2;c=3}
    """
    }

let test82: SpiralModule =
    {
    name="test82"
    prerequisites=[list]
    description="Does structural polymorphic equality work on recursive datatypes?"
    code=
    """
// TODO
inl a = List.empty int64 |> dyn
inl b = List.empty int64 |> dyn
a = b
    """
    }

let test83: SpiralModule =
    {
    name="test83"
    prerequisites=[]
    description="Does this destructure trigger an error?"
    code=
    """
inl q = true && dyn true
()
    """
    }

let test84: SpiralModule =
    {
    name="test84"
    prerequisites=[host_tensor]
    description="Does the scalar tensor work?"
    code=
    """
// TODO
open Tensor
inl ar = init () 5
ar .get
    """
    }

let test85: SpiralModule =
    {
    name="test85"
    prerequisites=[host_tensor]
    description="Does the split work?"
    code=
    """
// TODO
open Tensor
inl ar = init (32*32) id |> split (const (16,64))
(ar 0 0, ar 0 1, ar 0 2, ar 1 0, ar 1 1, ar 1 2) |> Tuple.map (inl x -> x.get)
    """
    }

let test86: SpiralModule =
    {
    name="test86"
    prerequisites=[host_tensor]
    description="Is the type of host tensor for the TOA layout correct? Does it work on the singleton dimensions?"
    code=
    """
// TODO
open Tensor
inl ar = init 10 id
ar 5 .get
    """
    }

let test88: SpiralModule =
    {
    name="test88"
    prerequisites=[extern_]
    description="Does the => related stuff work?"
    code=
    """
// TODO
open Extern
inl closure_type = (int64 => int64 => int64)
inl add a b = a + b
inl clo_add = closure_of add closure_type
match clo_add with
| (a: int64) => (b: (int64 => int64)) -> clo_add 1 2
    """
    }

let test89: SpiralModule =
    {
    name="test89"
    prerequisites=[]
    description="Does changing layout type work?"
    code=
    """
{a=1; b=2} |> dyn |> stack |> heap |> indiv
    """
    }

let test90: SpiralModule =
    {
    name="test90"
    prerequisites=[host_tensor]
    description="Does the tensor map work?"
    code=
    """
// TODO
open Tensor
init (2,2) (inl a b -> a*2+b)
|> map ((*) 2)
    """
    }

let test91: SpiralModule =
    {
    name="test91"
    prerequisites=[array; host_tensor]
    description="Does assert_size work? Does converting from array to tensor work?"
    code=
    """
// TODO
open Tensor
inl tns =
    Array.init 6 id
    |> array_to_tensor
    |> split (dyn (2,3) |> const)
    |> assert_size (2,3)
    
tns 1 0 .get |> ignore
    """
    }

let test92: SpiralModule =
    {
    name="test92"
    prerequisites=[]
    description="Does the CSE work as expected?"
    code=
    """
inl !dyn a,b = 2,3
(a+b)*(a+b)
    """
    }

let test93: SpiralModule =
    {
    name="test93"
    prerequisites=[]
    description="Does the string format work as expected?"
    code=
    """
inl l = 2,2.3,"qwe"
inl q = 1,2
String (format: "{0,-5}{1,-5}{2,-5}" args: l) |> dyn |> ignore
String (format: "{0,-5}{1,-5}{2,-5}" args: dyn l) |> ignore
String (format: (dyn "{0} = {1}") args: dyn q) |> ignore
    """
    }

let test94: SpiralModule =
    {
    name="test94"
    prerequisites=[array]
    description="Does the string concat work as expected?"
    code=
    """
// TODO
inl string_format a b = String (format: a args: b)
inl string_concat a b = String (concat: a args: b)
Array.init 8 (string_format "{0}") |> string_concat "; " |> string_format "[|{0}|]" |> dyn |> ignore
(2,2.3,"qwe") |> Tuple.map (string_format "{0}") |> string_concat "; " |> string_format "[{0}]" |> dyn |> ignore
    """
    }

let test95: SpiralModule =
    {
    name="test95"
    prerequisites=[extern_; array]
    description="Does the show work?"
    code=
    """
// TODO
open Extern
Array.init 8 (inl i -> {x = to float64 i; y = to float64 i-30.0} |> dyn |> stack) |> show
    """
    }

let test96: SpiralModule =
    {
    name="test96"
    prerequisites=[host_tensor; console]
    description="Does the show from Tensor work?"
    code=
    """
// TODO
open Tensor
init (2,3,4) (inl a b c -> a*b*c)  
|> show |> Console.writeline
    """
    }

let test97: SpiralModule =
    {
    name="test97"
    prerequisites=[host_tensor; console]
    description="Does the view indexing work?"
    code=
    """
// TODO
open Tensor
inl w = 2,3,4
init (2,3,4) (inl a b c -> a*b*c) (1,{from=1},{from=1; by=2})
|> show |> Console.writeline
    """
    }

let test99: SpiralModule =
    {
    name="test99"
    prerequisites=[]
    description="Does the binary . operator apply if it is directly next to an expression?"
    code=
    """
inl f = function
    | .Hello as x -> .Bye

inl g = function
    | .Bye -> ()

g f.Hello
    """
    }

let test100: SpiralModule =
    {
    name="test100"
    prerequisites=[]
    description="Does the unit closure get printed correctly."
    code=
    """
inl rec loop f i =
    inl f, i = Type (term_cast: f with: ()), dyn i
    inl body _ = if i < 10 then loop (inl _ -> f() + 1) (i + 1) else f()
    join (body() : 0)

loop (inl _ -> 0) 0
    """
    }

let test102: SpiralModule =
    {
    name="test102"
    prerequisites=[loops]
    description="Does the unroll work?"
    code=
    """
// TODO
inl f x = x ()
inl dyn = dyn >> ignore
inl x _ = 
    dyn "1" 
    inl _ ->
        dyn "2"
        inl _ ->
            dyn "3"
            inl rec loop _ = dyn "..."; loop
            loop
Loops.unroll f x
    """
    }

let test103: SpiralModule =
    {
    name="test103"
    prerequisites=[loops]
    description="Does the foru work?"
    code=
    """
// TODO
inl f x = x ()
inl dyn = dyn >> ignore
inl x _ = 
    dyn "1" 
    inl _ ->
        dyn "2"
        inl _ ->
            dyn "3"
            inl rec loop _ = dyn "..."; loop
            loop
Loops.foru {from=0; near_to=30; state = x; body = inl {state i} -> state ()}
    """
    }

let test104: SpiralModule =
    {
    name="test104"
    prerequisites=[tuple]
    description="Does the map2 work?"
    code=
    """
// TODO
inl a = 1,2,3
inl b = 4,5,6
Tuple.map2 (inl a b -> a + b) a b
    """
    }

let test105: SpiralModule =
    {
    name="test105"
    prerequisites=[tuple]
    description="Does the foldl2 work?"
    code=
    """
// TODO
inl a = 1,2,3
inl b = 4,5,6
Tuple.foldl2 (inl s a b -> s + a + b) 0 a b
    """
    }

let test106: SpiralModule =
    {
    name="test106"
    prerequisites=[]
    description="Does the injection pattern work?"
    code=
    """
inl m = {
    a = 123
    b = 456
    }
inl f i {$i=x} = x
dyn (f .a m, f .b m)
    """
    }

let test107: SpiralModule =
    {
    name="test107"
    prerequisites=[]
    description="Does the injection constructor work?"
    code=
    """
inl f i v m = {m with $i=v}
{}
|> f .a 123
|> f .b 456
|> inl {a b} -> a,b
|> dyn
    """
    }

let test108: SpiralModule =
    {
    name="test108"
    prerequisites=[]
    description="Does the parser give an error on an indented expression after a statement?"
    code=
    """
1 |> ignore
    2
    """
    }

let test109: SpiralModule =
    {
    name="test109"
    prerequisites=[]
    description="Does the newline after a semicolon work correctly?"
    code=
    """
dyn
    {a=1; b=2; 
     c=3}
    """
    }

let test110: SpiralModule =
    {
    name="test110"
    prerequisites=[]
    description="Does returning from join points work on nested structures?"
    code=
    """
inl q = {q=1;w=2;e=3}
inl w = {a=q;b=q}
inl e = {z=w;x=w}
inl e = join e
inl e = join e
()
    """
    }

let test111: SpiralModule =
    {
    name="test111"
    prerequisites=[]
    description="Does structural equality work correctly on union types?"
    code=
    """
inl Q = 0,0 \/ 0
inl f x = join Type (box: x to: Q)
inl a, b = f 1, f 1
a = b
//inl f x =
//    match x with
//    | q,w,e,r,t,z,x,c,v,b,m -> 0
//    | (((),a,b) | ({q w e r t y z a b}, _, _)) -> 
//        inl f a b = a + b
//        f a b
//    | a,b -> a + b
//dyn (f ({q=(); w=(); e=(); r=(); t=(); y=(); z=(); a=1; b=2},2,3))
    """
    }

let test112: SpiralModule =
    {
    name="test112"
    prerequisites=[]
    description="Does the () module-with pattern work?"
    code=
    """
inl k = .q
inl m = { $k = { b = 2 }}

{(m).(k) with a = 1}
    """
    }

let test113: SpiralModule =
    {
    name="test113"
    prerequisites=[host_tensor_range_view; console]
    description="Do the tensor range views work?"
    code=
    """
inl tns =
    Tensor.init (2,3,4) (inl a b c -> a*b*c)  
    |> ViewR.wrap ({from=2; near_to=4},{from=2; near_to=5},{from=2; near_to=6})

inl tns = tns ((), {from=3; by=2}, {from=3})
tns .basic |> Tensor.print
    """
    }

let test114: SpiralModule =
    {
    name="test114"
    prerequisites=[host_tensor_tree_view; console]
    description="Do the tensor tree views work?"
    code=
    """
inl tns =
    Tensor.init (2,3,4) (inl a b c -> a*b*c)  
    |> View.wrap ({a=1; b=1},{a=1; b=2},{a=1; b={q=3}})

inl tns = tns ({b=()}, {b=()}, {b={q=()}})
tns .basic |> Tensor.print
    """
    }

let test115: SpiralModule =
    {
    name="test115"
    prerequisites=[host_tensor_tree_view; console]
    description="Does the tensor view's create function work?"
    code=
    """
inl tns = View.create {dim={a=1; b=1}, {a=1; b=2}, {a=1; b={q=3}}; elem_type=float32}

inl tns = tns ({b=()}, {b=()}, {b={q=()}})
tns .basic |> Tensor.print
    """
    }

let test116: SpiralModule =
    {
    name="test116"
    prerequisites=[host_tensor_tree_view; console]
    description="Do the tensor tree partial views work?"
    code=
    """
inl tns =
    Tensor.init (4,16) (inl a b -> a,b)
    |> View.wrap ((), {a={b=4; c=4}; d={e=4; f=4}})

tns ((),{d=()}) .basic |> Tensor.print
    """
    }

let test117: SpiralModule =
    {
    name="test117"
    prerequisites=[host_tensor; console]
    description="Do the tensor expand_singular work?"
    code=
    """
open Tensor
init (1,5) (inl a b -> a, b)
|> Tensor.expand_singular (5,5) 
|> Tensor.print
    """
    }

let test118: SpiralModule =
    {
    name="test118"
    prerequisites=[]
    description="Do type_catch and type_raise work?"
    code=
    """
type_catch
    dyn "a" |> ignore
    dyn "b" |> ignore
    dyn "c" |> ignore
    type_raise .(3)
|> inl .(x) -> x
    """
    }

let parsing1: SpiralModule =
    {
    name="parsing1"
    prerequisites=[parsing; console]
    description="Does the Parsing module work?"
    code=
    """
open Parsing
open Console

inl p = 
    succ 1
    |>> writeline

run_with_unit_ret (readall()) p
    """
    }

let parsing2: SpiralModule =
    {
    name="parsing2"
    prerequisites=[parsing; console]
    description="Does the Parsing module work?"
    code=
    """
open Parsing
open Console

inl p = 
    pdigit
    |>> writeline

run_with_unit_ret (dyn "2") p
    """
    }

let parsing3: SpiralModule =
    {
    name="parsing3"
    prerequisites=[parsing; console]
    description="Does the Parsing module work?"
    code=
    """
open Parsing
open Console

inl p = 
    pstring "qwe"
    |>> writeline

run_with_unit_ret (dyn "qwerty") p
    """
    }

let parsing4: SpiralModule =
    {
    name="parsing4"
    prerequisites=[parsing; console]
    description="Does the Parsing module work?"
    code=
    """
open Parsing
open Console

inl p = 
    parse_int
    |>> writeline

run_with_unit_ret (dyn "1 2 3") p
    """
    }

let parsing5: SpiralModule =
    {
    name="parsing5"
    prerequisites=[parsing; console]
    description="Does the Parsing module work?"
    code=
    """
open Parsing
open Console

inl p = 
    parse_array {parser=parse_int; typ=int64; n=16}
    >>. succ ()

run_with_unit_ret (readall()) p
    """
    }

let parsing6: SpiralModule =
    {
    name="parsing6"
    prerequisites=[parsing; console]
    description="Do the printf's work?"
    code=
    """
open Parsing

inl a,b,c = dyn (1,2,3)
sprintf "%i + %i = %i" a b c |> ignore
    """
    }

let parsing7: SpiralModule =
    {
    name="parsing7"
    prerequisites=[array; console; parsing; extern_]
    description="Does the parsing library work? Birthday Cake Candles problem."
    code=
    """
//https://www.hackerrank.com/challenges/birthday-cake-candles

open Extern
open Console
open Parsing

inl int64_minvalue = FS.Constant "System.Int64.MinValue" int64

inl p = 
    inm n = parse_int
    inm ar = parse_array {parser=parse_int; typ=int64; n} 
    Array.foldl (inl (min,score as s) x ->
        if x > score then (1,x)
        elif x = score then (min+1,score)
        else s
        ) (dyn (0,int64_minvalue)) ar
    |> fst
    |> writeline
    |> succ
        
run_with_unit_ret (readall()) p
    """
    }

let parsing8: SpiralModule =
    {
    name="parsing8"
    prerequisites=[array; console; parsing]
    description="Does the parsing library work? Diagonal Sum Difference problem."
    code=
    """
//https://www.hackerrank.com/challenges/diagonal-difference
open Console
open Parsing

inl abs x = if x >= 0 then x else -x

inl f =
    inm n = parse_int
    inm ar = parse_array {parser=parse_int; typ=int64; n=n*n}
    inl load row col = 
        inl f x = x >= 0 || x < n
        assert (f row && f col) "Out of bounds."
        ar (n * row + col)
    met rec loop (!dyn i) (d1,d2 as s) =
        if i < n then loop (i+1) (d1 + load i i, d2 + load i (n-i-1))
        else s
        : s
    inl a,b = loop 0 (0,0)
    abs (a-b) 
    |> writeline
    |> succ

run_with_unit_ret (readall()) f
        """
    }

let loop1: SpiralModule =
    {
    name="loop1"
    prerequisites=[loops; console]
    description="Does the Loop module work?"
    code=
    """
open Console
open Loops
//If we list all the natural numbers below 10 that are multiples of 3 or 5, we get 3, 5, 6 and 9. The sum of these multiples is 23.
//Find the sum of all the multiples of 3 or 5 below 1000.

for {from=dyn 999; down_to=dyn 3; by=dyn -1; state=dyn 0; body = inl {state i} ->
    if i % 3 = 0 || i % 5 = 0 then state+i
    else state
    }
|> writeline
    """
    }

let loop2: SpiralModule =
    {
    name="loop2"
    prerequisites=[loops; console]
    description="Does the Loop module work?"
    code=
    """
open Console
open Loops

for {from=dyn 3; to=dyn 999; state=dyn 0; body = inl {state i} ->
    if i % 3 = 0 || i % 5 = 0 then state+i
    else state
    }
|> writeline
    """
    }

let loop3: SpiralModule =
    {
    name="loop3"
    prerequisites=[loops; console]
    description="Does the Loop module work?"
    code=
    """
open Console
open Loops

for {static_from=6; down_to=3; by= -1; state=0; body = inl {state i} ->
    if i % 3 = 0 || i % 5 = 0 then state+i
    else state
    }
|> writeline
    """
    }

let loop5: SpiralModule =
    {
    name="loop5"
    prerequisites=[loops]
    description="Does the Loop module work?"
    code=
    """
open Loops

for {static_from=2; to=2; body = inl {i} -> ()}
    """
    }

let loop6: SpiralModule =
    {
    name="loop6"
    prerequisites=[loops; console]
    description="Do state changing nested loops work?"
    code=
    """
open Loops
open Console
inl compare_pos (a_row,a_col) (b_row,b_col) = a_row = b_row && a_col = b_col
inl ret = {
    some = inl state -> printfn "Success." ()
    none = inl state -> failwith () "Failure."
    }
inl princess_pos = dyn (0,0)
inl mario_pos = dyn (1,1)
inl n = 5
for' {from=dyn 0; near_to=n; state={}; 
    body=inl {next=row i=r state} ->
        for' {from=dyn 0; near_to=n; state;
            body=inl {next=col i=c state} ->
                printfn "I am at ({0},{1})" (r, c)
                inl ret state = 
                    match state with
                    | {mario princess} -> ret .some state
                    | _ -> col state
                if compare_pos (r,c) mario_pos then ret {state with mario=mario_pos}
                elif compare_pos (r,c) princess_pos then ret {state with princess=princess_pos}
                else ret state
            finally=row
            }
    finally=ret .none
    }
    """
    }

let loop7: SpiralModule =
    {
    name="loop7"
    prerequisites=[console]
    description="Do state changing nested loops work?"
    code=
    """
open Console
inl compare_pos (a_row,a_col) (b_row,b_col) = a_row = b_row && a_col = b_col
inl ret = {
    some = inl state -> printfn "Success." ()
    none = inl state -> failwith () "Failure."
    }
inl princess_pos = dyn (0,0)
inl mario_pos = dyn (1,1)
inl n = dyn 5
met rec row {from=r near_to state} as d =
    met rec col {from=c near_to state} as d =
        if c < near_to then
            printfn "I am at ({0},{1})" (r, c)
            inl ret = function
                | {mario princess} as state -> ret .some state
                | state -> col {d with state from=c+1}
            if compare_pos (r,c) mario_pos then 
                printfn "I've found Mario." ()
                ret {state with mario=mario_pos}
            elif compare_pos (r,c) princess_pos then 
                printfn "I've found Princess." ()
                ret {state with princess=princess_pos}
            else ret state
        else 
            row {d with from=r+1}
        : ()
    if r < near_to then col {from=dyn 0; near_to state}
    else ret .none () 
    : ()
row {from=dyn 0; near_to=dyn n; state={}}
    """
    }

let loop8: SpiralModule =
    {
    name="loop8"
    prerequisites=[loops; console]
    description="Do state changing nested loops work?"
    code=
    """
open Console

met rec for {from=(!dyn from) near_to state body finally} =
    if from < near_to then 
        inl next state = for {from=from+1; near_to state body finally} 
        body {next state i=from}
    else finally state
    : finally state

inl compare_pos (a_row,a_col) (b_row,b_col) = a_row = b_row && a_col = b_col
inl ret = {
    some = inl state -> printfn "Success." ()
    none = inl state -> failwith () "Failure."
    }
inl princess_pos = dyn (0,0)
inl mario_pos = dyn (1,1)
inl n = dyn 5
for {from=0; near_to=n; state={};
    body = inl {next=row i=r state} ->
        for {from=0; near_to=n; state;
            body = inl {next=col i=c state} ->
                printfn "I am at ({0},{1})" (r, c)
                inl ret = function
                    | {mario princess} as state -> ret .some state
                    | state -> col state
                if compare_pos (r,c) mario_pos then 
                    printfn "I've found Mario." ()
                    ret {state with mario=mario_pos}
                elif compare_pos (r,c) princess_pos then 
                    printfn "I've found Princess." ()
                    ret {state with princess=princess_pos}
                else ret state
            finally = row
            }
    finally = ret .none
    }
    """
    }

let euler2: SpiralModule =
    {
    name="euler2"
    prerequisites=[loops; console]
    description="Even Fibonacci Numbers."
    code=
    """
open Loops
open Console

while {
    state={sum=dyn 0; a=dyn 1; b=dyn 2}
    cond=inl {b} -> if b <= 4*1000*1000 then true else false
    body=inl {sum a b} -> {sum=if b % 2 = 0 then sum+b else sum; a=b; b=a+b}
    }
|> inl {sum} -> writeline sum
    """
    }

let euler3: SpiralModule =
    {
    name="euler3"
    prerequisites=[array; loops; console; option; extern_]
    description="Largest prime factor"
    code=
    """
open Extern
open Loops
open Console
open Array
open Option

// The prime factors of 13195 are 5, 7, 13 and 29.
// What is the largest prime factor of the number 600851475143 ?

inl math_type = fs [text: "System.Math"]

inl target = dyn 600851475143

inl sieve_length = 
    FS.StaticMethod math_type .Sqrt(to float64 target) float64
    |> to int64

inl sieve = Array.init (sieve_length+1) (inl _ -> true)
for {from=2; to=sieve_length; body = inl {i} ->
    if sieve i = true then
        for {from=i+i; to=sieve_length; by=i; body = inl {i} -> 
            sieve i <- false
            }
    }

for' {from=sieve_length; to=2; by= -1; state=none int64; body = inl {next state i} ->
    if sieve i = true && target % i = 0 then some i
    else next state
    }
|>  function
    | .Some, result -> writeline result // 6857
    | .None -> failwith () "No prime factor found!"
    """
    }

let euler4: SpiralModule =
    {
    name="euler4"
    prerequisites=[array; loops; console]
    description="Largest palindrome product"
    code=
    """
//A palindromic number reads the same both ways. The largest palindrome made from the product of two 2-digit numbers is 9009 = 91 � 99.
//Find the largest palindrome made from the product of two 3-digit numbers.

open Loops
open Console

inl reverse_number x =
    while {
        cond=inl {x} -> x > 0 
        state={x x' = dyn 0}
        body=inl {x x'} -> {x=x/10; x'= x'*10+x%10}
        }
    |> inl {x'} -> x'
inl is_palindrome x = x = reverse_number x
for {from=dyn 100; to=dyn 999; state={highest_palindrome=dyn 0}; body=inl {state i} ->
    for {from=i; to=dyn 999; state; body=inl {{state with highest_palindrome} i=j} ->
        inl x = i*j
        if is_palindrome x && highest_palindrome < x then {highest_palindrome=x} else state
        }
    } 
|> inl {highest_palindrome} -> writeline highest_palindrome
    """
    }

let euler5: SpiralModule =
    {
    name="euler5"
    prerequisites=[tuple; loops; console; extern_]
    description="Smallest multiple"
    code=
    """
//2520 is the smallest number that can be divided by each of the numbers from 1 to 10 without any remainder.
//What is the smallest positive number that is evenly divisible by all of the numbers from 1 to 20?

open Extern
open Loops
open Console

inl primes = 2,3,5,11,13,17,19
inl non_primes = Tuple.range (2,20) |> Tuple.filter (Tuple.contains primes >> not)
inl step = Tuple.foldl (*) 1 primes
inl int64_maxvalue = FS.Constant "System.Int64.MaxValue" int64
for' {from=step; to=int64_maxvalue; by=step; state= -1; body=inl {next state i} ->
    if Tuple.forall (inl x -> i % x = 0) non_primes then i
    else next state
    }
|> writeline
    """
    }

let hacker_rank_1: SpiralModule =
    {
    name="hacker_rank_1"
    prerequisites=[extern_]
    description="The very first warmup exercise : https://www.hackerrank.com/challenges/solve-me-first"
    code=
    """
open Extern
inl console = fs [text: "System.Console"]
inl int32_type = fs [text: "System.Int32"]
inl parse_int32 str = FS.StaticMethod int32_type .Parse str int32
inl read_line () = FS.StaticMethod console .ReadLine() string
inl write x = FS.StaticMethod console .Write x ()
inl read_int () = read_line() |> parse_int32
inl a, b = read_int(), read_int()
write (a + b)
    """
    }

let hacker_rank_2: SpiralModule =
    {
    name="hacker_rank_2"
    prerequisites=[tuple; array; host_tensor; loops; list; option; parsing; console]
    description="Save The Princess"
    code=
    """
// https://www.hackerrank.com/challenges/saveprincess
// A simple dynamic programming problem. It wouldn't be hard to do in F#, but Spiral
// gives some novel challenges regarding it.
open Parsing
open Console
open Option
open Array
open Loops

inl Cell = .Empty \/ .Princess \/ .Mario

inl empty = pchar '-' >>% box Cell .Empty
inl princess = pchar 'p' >>% box Cell .Princess
inl mario = pchar 'm' >>% box Cell .Mario

inl cell = empty <|> princess <|> mario

inl parse_cols n = parse_array {parser=cell; typ=Cell; n} .>> spaces
inl parse_field n = parse_array {parser=parse_cols n; typ=type (array_create Cell 0); n}
inl parser = 
    inm n = parse_int 
    inm field = parse_field n
    inl no_pos = none (int64,int64)
    for' {from = 0; near_to=n; state={princess=no_pos; mario=no_pos}; body = inl {next=row state i=r} ->
        for' {from = 0; near_to=n; state; 
            body = inl {next=col state i=c} ->
                match field r c with
                | .Mario -> 
                    inl state = {state with mario=some (r,c)}
                    match state with
                    | {princess=.Some, _} -> state
                    | _ -> col state
                | .Princess -> 
                    inl state = {state with princess=some (r,c)}
                    match state with
                    | {mario=.Some, _} -> state
                    | _ -> col state
                | _ -> col state
            finally = row
            }
        }
    |> function
        | {mario=.Some, (mario_row, mario_col as mario_pos) princess=.Some, (princess_row, princess_col as princess_pos)} ->
            inl cells_visited = Tensor.init (n,n) (inl _ _ -> false)
            cells_visited mario_row mario_col .set true

            inl up_string = dyn "UP"
            inl down_string = dyn "DOWN"
            inl left_string = dyn "LEFT"
            inl right_string = dyn "RIGHT"

            inl up (row,col), prev_moves = (row-1,col), List.cons up_string prev_moves
            inl down (row,col), prev_moves = (row+1,col), List.cons down_string prev_moves
            inl left (row,col), prev_moves = (row,col-1), List.cons left_string prev_moves
            inl right (row,col), prev_moves = (row,col+1), List.cons right_string prev_moves

            inl next_moves = up,down,left,right

            inl is_valid x = x >= 0 && x < n
            inl is_in_range (row,col), _ = is_valid row && is_valid col
            inl is_princess_in_state (row,col), _ = row = princess_row && col = princess_col

            inl start_queue = Array.singleton (mario_pos, List.empty string)
            inl state_type = start_queue.elem_type

            inl solution = ref (none state_type)
            met rec loop queue =
                inl queue =
                    Array.map (inl mario_pos, prev_moves as state ->
                        inl potential_new_states = 
                            Tuple.map (inl move -> 
                                inl (pos_row, pos_col),_ as new_state = move state
                                inl is_valid =
                                    if is_in_range new_state && cells_visited pos_row pos_col .get = false then 
                                        if is_princess_in_state new_state then solution := some new_state
                                        cells_visited pos_row pos_col .set true
                                        true
                                    else false
                                new_state, is_valid
                                ) next_moves
                        inl bool_to_int x = if x then 1 else 0
                        inl number_of_valid_states = Tuple.foldl (inl s (_,!bool_to_int x) -> s + x) 0 potential_new_states
                        inl new_states = array_create state_type number_of_valid_states
                        Tuple.foldl (inl i (state,is_valid) -> 
                            if is_valid then new_states i <- state; i+1
                            else i
                            ) 0 potential_new_states |> ignore
                        new_states
                        ) queue
                    |> Array.concat
                match solution() with
                | .None -> loop queue
                | .Some, (_, path) -> List.foldr (inl x _ -> Console.writeline x) path ()
                : ()
            loop start_queue
        | _ -> failwith () "Current position not found."
    |> succ

//inl str = dyn "3
//---
//-m-
//p--
//    "
run_with_unit_ret (readall()) parser
    """
    }

let hacker_rank_3: SpiralModule =
    {
    name="hacker_rank_3"
    prerequisites=[tuple; array; host_tensor; loops; list; parsing; console; queue]
    description="Save The Princess 2"
    code=
    """
// https://www.hackerrank.com/challenges/saveprincess2
// A version of this similar to the previous one made in order to test the new queue.
open Parsing
open Console
open Array
open Loops

inl Cell = .Empty \/ .Princess \/ .Mario

inl empty = pchar '-' >>% box Cell .Empty
inl princess = pchar 'p' >>% box Cell .Princess
inl mario = pchar 'm' >>% box Cell .Mario

inl cell = empty <|> princess <|> mario

inl parse_cols n = parse_array {parser=cell; typ=Cell; n} .>> spaces
inl parse_field n = parse_array {parser=parse_cols n; typ=type (array_create Cell 0); n}
inl parser ret = 
    inm n = parse_int .>> parse_int .>> parse_int
    inm field = parse_field n
    for' {from = 0; near_to=n; state={n field}; 
        body = inl {next=row state i=r} ->
            for' {from = 0; near_to=n; state; 
                body = inl {next=col state i=c} ->
                    inl ret = function
                        | {mario princess} as state -> ret .some state
                        | state -> col state
                    match field r c with
                    | .Mario -> ret {state with mario=r,c}
                    | .Princess -> ret {state with princess=r,c}
                    | _ -> col state
                finally = row
                }
        finally = ret .none
        }
    |> succ

inl main = {
    some = met {n field mario=(mario_row, mario_col as mario_pos) princess=(princess_row, princess_col as princess_pos)} ->
        inl cells_visited = Tensor.init (n,n) (inl _ _ -> false)
        cells_visited mario_row mario_col .set true

        inl up_string = dyn "UP"
        inl down_string = dyn "DOWN"
        inl left_string = dyn "LEFT"
        inl right_string = dyn "RIGHT"

        inl up (row,col), prev_moves = (row-1,col), List.cons up_string prev_moves
        inl down (row,col), prev_moves = (row+1,col), List.cons down_string prev_moves
        inl left (row,col), prev_moves = (row,col-1), List.cons left_string prev_moves
        inl right (row,col), prev_moves = (row,col+1), List.cons right_string prev_moves

        inl is_valid x = x >= 0 && x < n
        inl is_in_range (row,col), _ = is_valid row && is_valid col
        inl is_princess_in_state (row,col), _ = row = princess_row && col = princess_col

        inl init_state = (mario_pos, List.empty string)
        inl state_type = type init_state 

        open Queue
        inl queue = create state_type ()
        enqueue queue init_state

        met print_solution _, path = //List.foldr (inl x _ -> Console.writeline x) path ()
            match List.last path with
            | .Some, x -> Console.writeline x
            | .None -> failwith () "Error: No moves taken."

        met evaluate_move state move on_fail =
            inl (pos_row, pos_col),_ as new_state = move state
            if is_in_range new_state && cells_visited pos_row pos_col .get = false then 
                if is_princess_in_state new_state then print_solution new_state
                else
                    cells_visited pos_row pos_col .set true
                    enqueue queue new_state
                    on_fail ()
            else on_fail ()
            
        met rec loop () =
            inl next_moves = up, down, left, right
            inl state = dequeue queue
            Tuple.foldr (inl move next () -> evaluate_move state move next) next_moves loop ()
            : ()

        loop ()
    none = inl _ -> failwith () "Current position not found."
    }

run_with_unit_ret (readall()) (parser main)
    """
    }

let hacker_rank_4: SpiralModule =
    {
    name="hacker_rank_4"
    prerequisites=[tuple; array; parsing; console; option]
    description="Game of Stones"
    code=
    """
// https://www.hackerrank.com/challenges/game-of-stones-1
open Parsing
open Console
open Loops
open Option

inl Player = .First \/ .Second

inl first = box Player .First
inl second = box Player .Second
inl not_visited = none Player

inl max_n = 100
inl solutions = Array.init (max_n+1) (const not_visited)

met rec solve (!dyn player, !dyn opposing_player) (!dyn n) =
    inl take amount on_fail = 
        if n >= amount && solve (opposing_player,player) (n-amount) = player then player
        else on_fail ()

    met run () = Tuple.foldr (inl take on_fail _ -> take on_fail) (take 2, take 3, take 5) (const opposing_player) () 

    if player = first then
        match solutions n with
        | .None -> 
            inl x = run()
            solutions n <- some x
            x
        | .Some, x -> x
    else run()
    : player

inl show .(x) = writeline x
inl parser = 
    inm t = parse_int
    repeat t (inl i -> parse_int |>> (solve (first,second) >> show))

run_with_unit_ret (readall()) parser
    """
    }

let hacker_rank_5: SpiralModule =
    {
    name="hacker_rank_5"
    prerequisites=[parsing; console]
    description="Game of Stones"
    code=
    """
// https://www.hackerrank.com/challenges/tower-breakers-1
open Parsing
open Console

// This is the solution from the discussion forum that I've been spoilered on.
inl solve n,m = if m = 1 || n % 2 = 0 then 2 else 1

inl parser = 
    inm t = parse_int
    repeat t (inl i -> parse_int .>>. parse_int |>> (solve >> writeline))

run_with_unit_ret (readall()) parser
    """
    }

let hacker_rank_6: SpiralModule =
    {
    name="hacker_rank_6"
    prerequisites=[tuple; array; host_tensor; parsing; console; option]
    description="A Chessboard Game"
    code=
    """
// https://www.hackerrank.com/challenges/a-chessboard-game-1
open Parsing
open Console
open Option

inl num_players = 2
inl first = 0
inl second = 1

inl max_t = 15

inl cache = 
    inl cache = Tensor.init (max_t,max_t,num_players) (inl _ _ _ -> none first)
    inl op (x,y,player_one,player_two) -> cache (x-1) (y-1) player_one op

met rec solve !dyn (x,y,player_one,player_two) as d = 
    inl new_positions = (x-2,y+1),(x-2,y-1),(x+1,y-2),(x-1,y-2)
    inl is_in_range x = x >= 1 && x <= 15
    inl try x,y on_fail =
        if is_in_range x && is_in_range y && solve (x,y,player_two,player_one) = player_one then player_one
        else on_fail()
    match cache.get d with
    | .None -> Tuple.foldr (inl pos next () -> try pos next) new_positions (const player_two) () |> inl x -> some x |> cache.set d; x
    | .Some, x -> x
    : player_one

inl show = function
    | x when x = first -> writeline "First"
    | _ -> writeline "Second"

inl parser = 
    inm t = parse_int
    repeat t (inl i -> parse_int .>>. parse_int |>> inl x,y -> solve (x,y,first,second) |> show)

run_with_unit_ret (readall()) parser
    """
    }

let hacker_rank_7: SpiralModule =
    {
    name="hacker_rank_7"
    prerequisites=[tuple; array; parsing; console; option]
    description="Introduction to Nim Game"
    code=
    """
// https://www.hackerrank.com/challenges/nim-game-1/problem

open Parsing
open Console
open Array

inl solve = Array.foldl (^^^) 0
inl show = function
    | 0 -> writeline "Second"
    | _ -> writeline "First"

inl parser = 
    inm t = parse_int
    repeat t (inl _ -> 
        inm n = parse_int 
        parse_array {n parser=parse_int; typ=int64} |>> (solve >> show)
        )

run_with_unit_ret (readall()) parser
    """
    }

let hacker_rank_8: SpiralModule =
    {
    name="hacker_rank_8"
    prerequisites=[tuple; array; parsing; console; option]
    description="Misere Nim"
    code=
    """
// https://www.hackerrank.com/challenges/misere-nim-1

open Parsing
open Console
open Array

// https://mathoverflow.net/questions/71802/analysis-of-misere-nim
inl solve ar = 
    inl r = Array.foldl (^^^) 0 ar
    if Array.forall ((=) 1) ar then r ^^^ 1
    else r
    
inl show = function
    | 0 -> writeline "Second"
    | _ -> writeline "First"

inl parser = 
    inm t = parse_int
    repeat t (inl _ -> 
        inm n = parse_int 
        parse_array {n parser=parse_int; typ=int64} |>> (solve >> show)
        )

run_with_unit_ret (readall()) parser
    """
    }

let hacker_rank_9: SpiralModule =
    {
    name="hacker_rank_9"
    prerequisites=[tuple; array; host_tensor_range_view; parsing; console; option]
    description="The Power Sum"
    code=
    """
// https://www.hackerrank.com/challenges/the-power-sum

open Parsing
open Console
open Array
open Loops

inl x_range = {from=1; to=1000}
inl n_range = {from=2; to=10}

inl x_to_n = 
    inl cache = ViewR.create {dim=x_range,n_range; elem_type=int64}
    for {x_range with body=inl {i=x} ->
        for {n_range with body=inl {i=n} ->
            for {from=2; to=n; state=x; body=inl {state=x'} -> x*x'}
            |> cache x n .set
            }
        }
    inl x -> cache x .get

met rec solve !dyn state !dyn sum !dyn from to,n =
    for' {from to state body=inl {next state i=x} ->
        inl sum = sum + x_to_n (x,n)
        if sum = to then state + 1
        elif sum < to then next (solve state sum (x+1) (to,n))
        else state
        }
    : state

inl parser = parse_int .>>. parse_int |>> (solve 0 0 1 >> writeline)
run_with_unit_ret (readall()) parser 
    """
    }


//rewrite_test_cache tests cfg None //(Some(0,40))
output_test_to_temp cfg (Path.Combine(__SOURCE_DIRECTORY__ , @"..\Temporary\output.fs")) test20
|> printfn "%s"
|> ignore
