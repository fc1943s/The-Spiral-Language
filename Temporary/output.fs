module SpiralExample.Main
let cuda_kernels = """

extern "C" {
    
}
"""

let rec method_0((var_0: (int64 [])), (var_1: int64)): unit =
    let (var_2: bool) = (var_1 < 5L)
    if var_2 then
        let (var_3: bool) = (var_1 >= 1L)
        let (var_4: bool) = (var_3 = false)
        if var_4 then
            (failwith "Argument out of bounds.")
        else
            ()
        let (var_5: int64) = (var_1 - 1L)
        let (var_6: int64) = (var_5 * 30L)
        let (var_7: int64) = 0L
        method_1((var_1: int64), (var_0: (int64 [])), (var_6: int64), (var_7: int64))
        let (var_8: int64) = (var_1 + 1L)
        method_0((var_0: (int64 [])), (var_8: int64))
    else
        ()
and method_3((var_0: System.Text.StringBuilder), (var_1: int64)): unit =
    let (var_2: bool) = (var_1 < 0L)
    if var_2 then
        let (var_3: System.Text.StringBuilder) = var_0.Append(' ')
        let (var_4: int64) = (var_1 + 1L)
        method_3((var_0: System.Text.StringBuilder), (var_4: int64))
    else
        ()
and method_4((var_0: System.Text.StringBuilder), (var_1: string), (var_2: (int64 [])), (var_3: int64)): unit =
    let (var_4: bool) = (var_3 < 2L)
    if var_4 then
        let (var_5: bool) = (var_3 >= 0L)
        let (var_6: bool) = (var_5 = false)
        if var_6 then
            (failwith "Argument out of bounds.")
        else
            ()
        let (var_7: int64) = (var_3 * 30L)
        let (var_8: int64) = (30L + var_7)
        let (var_9: int64) = (var_8 + 10L)
        let (var_10: int64) = 0L
        method_5((var_0: System.Text.StringBuilder), (var_10: int64))
        let (var_11: System.Text.StringBuilder) = var_0.AppendLine("[|")
        let (var_12: int64) = 0L
        method_6((var_0: System.Text.StringBuilder), (var_1: string), (var_2: (int64 [])), (var_9: int64), (var_12: int64))
        let (var_13: int64) = 0L
        method_5((var_0: System.Text.StringBuilder), (var_13: int64))
        let (var_14: System.Text.StringBuilder) = var_0.AppendLine("|]")
        let (var_15: int64) = (var_3 + 1L)
        method_4((var_0: System.Text.StringBuilder), (var_1: string), (var_2: (int64 [])), (var_15: int64))
    else
        ()
and method_1((var_0: int64), (var_1: (int64 [])), (var_2: int64), (var_3: int64)): unit =
    let (var_4: bool) = (var_3 < 3L)
    if var_4 then
        let (var_5: bool) = (var_3 >= 0L)
        let (var_6: bool) = (var_5 = false)
        if var_6 then
            (failwith "Argument out of bounds.")
        else
            ()
        let (var_7: int64) = (var_3 * 10L)
        let (var_8: int64) = (var_2 + var_7)
        let (var_9: int64) = 0L
        method_2((var_0: int64), (var_3: int64), (var_1: (int64 [])), (var_8: int64), (var_9: int64))
        let (var_10: int64) = (var_3 + 1L)
        method_1((var_0: int64), (var_1: (int64 [])), (var_2: int64), (var_10: int64))
    else
        ()
and method_5((var_0: System.Text.StringBuilder), (var_1: int64)): unit =
    let (var_2: bool) = (var_1 < 4L)
    if var_2 then
        let (var_3: System.Text.StringBuilder) = var_0.Append(' ')
        let (var_4: int64) = (var_1 + 1L)
        method_5((var_0: System.Text.StringBuilder), (var_4: int64))
    else
        ()
and method_6((var_0: System.Text.StringBuilder), (var_1: string), (var_2: (int64 [])), (var_3: int64), (var_4: int64)): unit =
    let (var_5: bool) = (var_4 < 2L)
    if var_5 then
        let (var_6: bool) = (var_4 >= 0L)
        let (var_7: bool) = (var_6 = false)
        if var_7 then
            (failwith "Argument out of bounds.")
        else
            ()
        let (var_8: int64) = (var_4 * 10L)
        let (var_9: int64) = (var_3 + var_8)
        let (var_10: int64) = (var_9 + 1L)
        let (var_11: int64) = 0L
        method_7((var_0: System.Text.StringBuilder), (var_11: int64))
        let (var_12: System.Text.StringBuilder) = var_0.Append("[|")
        let (var_13: int64) = 0L
        let (var_14: string) = method_8((var_0: System.Text.StringBuilder), (var_2: (int64 [])), (var_10: int64), (var_13: int64), (var_1: string))
        let (var_15: System.Text.StringBuilder) = var_0.AppendLine("|]")
        let (var_16: int64) = (var_4 + 1L)
        method_6((var_0: System.Text.StringBuilder), (var_1: string), (var_2: (int64 [])), (var_3: int64), (var_16: int64))
    else
        ()
and method_2((var_0: int64), (var_1: int64), (var_2: (int64 [])), (var_3: int64), (var_4: int64)): unit =
    let (var_5: bool) = (var_4 < 10L)
    if var_5 then
        let (var_6: bool) = (var_4 >= 0L)
        let (var_7: bool) = (var_6 = false)
        if var_7 then
            (failwith "Argument out of bounds.")
        else
            ()
        let (var_8: int64) = (var_3 + var_4)
        let (var_9: int64) = (var_0 * var_1)
        let (var_10: int64) = (var_9 * var_4)
        var_2.[int32 var_8] <- var_10
        let (var_11: int64) = (var_4 + 1L)
        method_2((var_0: int64), (var_1: int64), (var_2: (int64 [])), (var_3: int64), (var_11: int64))
    else
        ()
and method_7((var_0: System.Text.StringBuilder), (var_1: int64)): unit =
    let (var_2: bool) = (var_1 < 8L)
    if var_2 then
        let (var_3: System.Text.StringBuilder) = var_0.Append(' ')
        let (var_4: int64) = (var_1 + 1L)
        method_7((var_0: System.Text.StringBuilder), (var_4: int64))
    else
        ()
and method_8((var_0: System.Text.StringBuilder), (var_1: (int64 [])), (var_2: int64), (var_3: int64), (var_4: string)): string =
    let (var_5: bool) = (var_3 < 3L)
    if var_5 then
        let (var_6: System.Text.StringBuilder) = var_0.Append(var_4)
        let (var_7: bool) = (var_3 >= 0L)
        let (var_8: bool) = (var_7 = false)
        if var_8 then
            (failwith "Argument out of bounds.")
        else
            ()
        let (var_9: int64) = (var_2 + var_3)
        let (var_10: int64) = var_1.[int32 var_9]
        let (var_11: string) = System.String.Format("{0}",var_10)
        let (var_12: System.Text.StringBuilder) = var_0.Append(var_11)
        let (var_13: string) = "; "
        let (var_14: int64) = (var_3 + 1L)
        method_8((var_0: System.Text.StringBuilder), (var_1: (int64 [])), (var_2: int64), (var_14: int64), (var_13: string))
    else
        var_4
let (var_0: (int64 [])) = Array.zeroCreate<int64> (System.Convert.ToInt32(120L))
let (var_1: int64) = 1L
method_0((var_0: (int64 [])), (var_1: int64))
let (var_2: System.Text.StringBuilder) = System.Text.StringBuilder()
let (var_3: string) = ""
let (var_4: int64) = 0L
method_3((var_2: System.Text.StringBuilder), (var_4: int64))
let (var_5: System.Text.StringBuilder) = var_2.AppendLine("[|")
let (var_6: int64) = 0L
method_4((var_2: System.Text.StringBuilder), (var_3: string), (var_0: (int64 [])), (var_6: int64))
let (var_7: int64) = 0L
method_3((var_2: System.Text.StringBuilder), (var_7: int64))
let (var_8: System.Text.StringBuilder) = var_2.AppendLine("|]")
var_2.ToString()
