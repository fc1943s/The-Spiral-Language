module SpiralExample.Main
let cuda_kernels = """
#include "cub/cub.cuh"

extern "C" {
    typedef float(*FunPointer0)(float, float);
    __global__ void method_7(float * var_0, float * var_1, float * var_2);
    __global__ void method_10(float * var_0, float * var_1);
    __global__ void method_12(float * var_0, float * var_1, float * var_2);
    __global__ void method_20(float var_0, float var_1, float * var_2, float * var_3, float * var_4);
    __global__ void method_22(float * var_0, float * var_1, float * var_2, float * var_3);
    __global__ void method_24(float * var_0, float * var_1, float * var_2, float * var_3, float * var_4);
    __global__ void method_28(float * var_0, float * var_1, float * var_2, float * var_3, float * var_4);
    __device__ void method_8(long long int var_0, long long int var_1, long long int var_2, float * var_3, float * var_4, float * var_5, long long int var_6, long long int var_7, long long int var_8, long long int var_9);
    __device__ void method_11(float * var_0, float * var_1, long long int var_2);
    __device__ float method_13(float * var_0, float * var_1, float var_2, long long int var_3);
    __device__ float method_14(float var_0, float var_1);
    __device__ void method_21(float var_0, float var_1, float * var_2, float * var_3, float * var_4, long long int var_5);
    __device__ void method_23(float * var_0, float * var_1, float * var_2, float * var_3, long long int var_4);
    __device__ void method_25(long long int var_0, long long int var_1, long long int var_2, float * var_3, float * var_4, float * var_5, float * var_6, float * var_7, long long int var_8, long long int var_9, long long int var_10, long long int var_11);
    __device__ void method_29(long long int var_0, long long int var_1, long long int var_2, float * var_3, float * var_4, float * var_5, float * var_6, float * var_7, long long int var_8, long long int var_9, long long int var_10, long long int var_11);
    __device__ void method_9(float * var_0, long long int var_1, float * var_2, float * var_3, long long int var_4);
    __device__ float method_26(long long int var_0, float * var_1, float * var_2, float * var_3, float * var_4, float var_5, long long int var_6);
    __device__ void method_27(float * var_0, long long int var_1, float * var_2, long long int var_3, float var_4, long long int var_5);
    __device__ void method_30(float * var_0, long long int var_1, float * var_2, float * var_3, float * var_4, float * var_5, long long int var_6);
    
    __global__ void method_7(float * var_0, float * var_1, float * var_2) {
        long long int var_3 = threadIdx.x;
        long long int var_4 = threadIdx.y;
        long long int var_5 = threadIdx.z;
        long long int var_6 = blockIdx.x;
        long long int var_7 = blockIdx.y;
        long long int var_8 = blockIdx.z;
        long long int var_9 = (16 * var_6);
        long long int var_10 = (var_3 + var_9);
        method_8(var_6, var_7, var_8, var_0, var_1, var_2, var_3, var_4, var_5, var_10);
    }
    __global__ void method_10(float * var_0, float * var_1) {
        long long int var_2 = threadIdx.x;
        long long int var_3 = threadIdx.y;
        long long int var_4 = threadIdx.z;
        long long int var_5 = blockIdx.x;
        long long int var_6 = blockIdx.y;
        long long int var_7 = blockIdx.z;
        long long int var_8 = (var_5 * 128);
        long long int var_9 = (var_8 + var_2);
        method_11(var_0, var_1, var_9);
    }
    __global__ void method_12(float * var_0, float * var_1, float * var_2) {
        long long int var_3 = threadIdx.x;
        long long int var_4 = threadIdx.y;
        long long int var_5 = threadIdx.z;
        long long int var_6 = blockIdx.x;
        long long int var_7 = blockIdx.y;
        long long int var_8 = blockIdx.z;
        long long int var_9 = (var_6 * 128);
        long long int var_10 = (var_9 + var_3);
        float var_11 = 0;
        float var_12 = method_13(var_0, var_1, var_11, var_10);
        FunPointer0 var_15 = method_14;
        float var_16 = cub::BlockReduce<float,128>().Reduce(var_12, var_15);
        char var_17 = (var_3 == 0);
        if (var_17) {
            char var_18 = (var_6 >= 0);
            char var_20;
            if (var_18) {
                var_20 = (var_6 < 4);
            } else {
                var_20 = 0;
            }
            char var_21 = (var_20 == 0);
            if (var_21) {
                // unprinted assert;
            } else {
            }
            var_2[var_6] = var_16;
        } else {
        }
    }
    __global__ void method_20(float var_0, float var_1, float * var_2, float * var_3, float * var_4) {
        long long int var_5 = threadIdx.x;
        long long int var_6 = threadIdx.y;
        long long int var_7 = threadIdx.z;
        long long int var_8 = blockIdx.x;
        long long int var_9 = blockIdx.y;
        long long int var_10 = blockIdx.z;
        long long int var_11 = (var_8 * 128);
        long long int var_12 = (var_11 + var_5);
        method_21(var_0, var_1, var_2, var_3, var_4, var_12);
    }
    __global__ void method_22(float * var_0, float * var_1, float * var_2, float * var_3) {
        long long int var_4 = threadIdx.x;
        long long int var_5 = threadIdx.y;
        long long int var_6 = threadIdx.z;
        long long int var_7 = blockIdx.x;
        long long int var_8 = blockIdx.y;
        long long int var_9 = blockIdx.z;
        long long int var_10 = (var_7 * 128);
        long long int var_11 = (var_10 + var_4);
        method_23(var_0, var_1, var_2, var_3, var_11);
    }
    __global__ void method_24(float * var_0, float * var_1, float * var_2, float * var_3, float * var_4) {
        long long int var_5 = threadIdx.x;
        long long int var_6 = threadIdx.y;
        long long int var_7 = threadIdx.z;
        long long int var_8 = blockIdx.x;
        long long int var_9 = blockIdx.y;
        long long int var_10 = blockIdx.z;
        long long int var_11 = (16 * var_8);
        long long int var_12 = (var_5 + var_11);
        method_25(var_8, var_9, var_10, var_0, var_1, var_2, var_3, var_4, var_5, var_6, var_7, var_12);
    }
    __global__ void method_28(float * var_0, float * var_1, float * var_2, float * var_3, float * var_4) {
        long long int var_5 = threadIdx.x;
        long long int var_6 = threadIdx.y;
        long long int var_7 = threadIdx.z;
        long long int var_8 = blockIdx.x;
        long long int var_9 = blockIdx.y;
        long long int var_10 = blockIdx.z;
        long long int var_11 = (16 * var_8);
        long long int var_12 = (var_5 + var_11);
        method_29(var_8, var_9, var_10, var_0, var_1, var_2, var_3, var_4, var_5, var_6, var_7, var_12);
    }
    __device__ void method_8(long long int var_0, long long int var_1, long long int var_2, float * var_3, float * var_4, float * var_5, long long int var_6, long long int var_7, long long int var_8, long long int var_9) {
        char var_10 = (var_9 < 16);
        if (var_10) {
            char var_11 = (var_9 >= 0);
            char var_12 = (var_11 == 0);
            if (var_12) {
                // unprinted assert;
            } else {
            }
            long long int var_13 = (8 * var_1);
            long long int var_14 = (var_7 + var_13);
            method_9(var_3, var_9, var_4, var_5, var_14);
            long long int var_15 = (var_9 + 16);
            method_8(var_0, var_1, var_2, var_3, var_4, var_5, var_6, var_7, var_8, var_15);
        } else {
        }
    }
    __device__ void method_11(float * var_0, float * var_1, long long int var_2) {
        char var_3 = (var_2 < 512);
        if (var_3) {
            char var_4 = (var_2 >= 0);
            char var_5 = (var_4 == 0);
            if (var_5) {
                // unprinted assert;
            } else {
            }
            if (var_5) {
                // unprinted assert;
            } else {
            }
            float var_6 = var_0[var_2];
            float var_7 = var_1[var_2];
            float var_8 = (-var_6);
            float var_9 = exp(var_8);
            float var_10 = (1 + var_9);
            float var_11 = (1 / var_10);
            var_1[var_2] = var_11;
            long long int var_12 = (var_2 + 512);
            method_11(var_0, var_1, var_12);
        } else {
        }
    }
    __device__ float method_13(float * var_0, float * var_1, float var_2, long long int var_3) {
        char var_4 = (var_3 < 512);
        if (var_4) {
            char var_5 = (var_3 >= 0);
            char var_6 = (var_5 == 0);
            if (var_6) {
                // unprinted assert;
            } else {
            }
            float var_7 = var_0[var_3];
            float var_8 = var_1[var_3];
            float var_9 = (var_8 - var_7);
            float var_10 = (var_9 * var_9);
            float var_11 = (var_2 + var_10);
            long long int var_12 = (var_3 + 512);
            return method_13(var_0, var_1, var_11, var_12);
        } else {
            return var_2;
        }
    }
    __device__ float method_14(float var_0, float var_1) {
        return (var_0 + var_1);
    }
    __device__ void method_21(float var_0, float var_1, float * var_2, float * var_3, float * var_4, long long int var_5) {
        char var_6 = (var_5 < 512);
        if (var_6) {
            char var_7 = (var_5 >= 0);
            char var_8 = (var_7 == 0);
            if (var_8) {
                // unprinted assert;
            } else {
            }
            if (var_8) {
                // unprinted assert;
            } else {
            }
            float var_9 = var_2[var_5];
            float var_10 = var_3[var_5];
            float var_11 = var_4[var_5];
            float var_12 = (var_9 - var_10);
            float var_13 = (2 * var_12);
            float var_14 = (var_0 * var_13);
            float var_15 = (var_11 + var_14);
            var_4[var_5] = var_15;
            long long int var_16 = (var_5 + 512);
            method_21(var_0, var_1, var_2, var_3, var_4, var_16);
        } else {
        }
    }
    __device__ void method_23(float * var_0, float * var_1, float * var_2, float * var_3, long long int var_4) {
        char var_5 = (var_4 < 512);
        if (var_5) {
            char var_6 = (var_4 >= 0);
            char var_7 = (var_6 == 0);
            if (var_7) {
                // unprinted assert;
            } else {
            }
            if (var_7) {
                // unprinted assert;
            } else {
            }
            float var_8 = var_0[var_4];
            float var_9 = var_1[var_4];
            float var_10 = var_2[var_4];
            float var_11 = var_3[var_4];
            float var_12 = (1 - var_10);
            float var_13 = (var_10 * var_12);
            float var_14 = (var_9 * var_13);
            float var_15 = (var_11 + var_14);
            var_3[var_4] = var_15;
            long long int var_16 = (var_4 + 512);
            method_23(var_0, var_1, var_2, var_3, var_16);
        } else {
        }
    }
    __device__ void method_25(long long int var_0, long long int var_1, long long int var_2, float * var_3, float * var_4, float * var_5, float * var_6, float * var_7, long long int var_8, long long int var_9, long long int var_10, long long int var_11) {
        char var_12 = (var_11 < 16);
        if (var_12) {
            char var_13 = (var_11 >= 0);
            char var_14 = (var_13 == 0);
            if (var_14) {
                // unprinted assert;
            } else {
            }
            if (var_14) {
                // unprinted assert;
            } else {
            }
            long long int var_15 = (8 * var_1);
            long long int var_16 = (var_9 + var_15);
            float var_17 = 0;
            float var_18 = method_26(var_11, var_3, var_4, var_5, var_6, var_17, var_16);
            __shared__ float var_19[112];
            char var_20 = (var_8 >= 0);
            char var_22;
            if (var_20) {
                var_22 = (var_8 < 16);
            } else {
                var_22 = 0;
            }
            char var_23 = (var_22 == 0);
            if (var_23) {
                // unprinted assert;
            } else {
            }
            long long int var_24 = (var_8 * 7);
            char var_25 = (var_9 != 0);
            if (var_25) {
                char var_26 = (var_9 >= 1);
                char var_28;
                if (var_26) {
                    var_28 = (var_9 < 8);
                } else {
                    var_28 = 0;
                }
                char var_29 = (var_28 == 0);
                if (var_29) {
                    // unprinted assert;
                } else {
                }
                long long int var_30 = (var_9 - 1);
                long long int var_31 = (var_24 + var_30);
                var_19[var_31] = var_18;
            } else {
            }
            __syncthreads();
            char var_32 = (var_9 == 0);
            if (var_32) {
                long long int var_33 = 1;
                method_27(var_19, var_24, var_7, var_11, var_18, var_33);
            } else {
            }
            long long int var_34 = (var_11 + 16);
            method_25(var_0, var_1, var_2, var_3, var_4, var_5, var_6, var_7, var_8, var_9, var_10, var_34);
        } else {
        }
    }
    __device__ void method_29(long long int var_0, long long int var_1, long long int var_2, float * var_3, float * var_4, float * var_5, float * var_6, float * var_7, long long int var_8, long long int var_9, long long int var_10, long long int var_11) {
        char var_12 = (var_11 < 16);
        if (var_12) {
            char var_13 = (var_11 >= 0);
            char var_14 = (var_13 == 0);
            if (var_14) {
                // unprinted assert;
            } else {
            }
            long long int var_15 = (8 * var_1);
            long long int var_16 = (var_9 + var_15);
            method_30(var_3, var_11, var_4, var_5, var_6, var_7, var_16);
            long long int var_17 = (var_11 + 16);
            method_29(var_0, var_1, var_2, var_3, var_4, var_5, var_6, var_7, var_8, var_9, var_10, var_17);
        } else {
        }
    }
    __device__ void method_9(float * var_0, long long int var_1, float * var_2, float * var_3, long long int var_4) {
        char var_5 = (var_4 < 32);
        if (var_5) {
            char var_6 = (var_4 >= 0);
            char var_7 = (var_6 == 0);
            if (var_7) {
                // unprinted assert;
            } else {
            }
            long long int var_8 = (var_4 * 16);
            char var_9 = (var_1 >= 0);
            char var_11;
            if (var_9) {
                var_11 = (var_1 < 16);
            } else {
                var_11 = 0;
            }
            char var_12 = (var_11 == 0);
            if (var_12) {
                // unprinted assert;
            } else {
            }
            long long int var_13 = (var_8 + var_1);
            if (var_7) {
                // unprinted assert;
            } else {
            }
            char var_15;
            if (var_9) {
                var_15 = (var_1 < 16);
            } else {
                var_15 = 0;
            }
            char var_16 = (var_15 == 0);
            if (var_16) {
                // unprinted assert;
            } else {
            }
            float var_17 = var_0[var_1];
            float var_18 = var_2[var_13];
            float var_19 = var_3[var_13];
            float var_20 = (var_17 + var_18);
            var_3[var_13] = var_20;
            long long int var_21 = (var_4 + 8);
            method_9(var_0, var_1, var_2, var_3, var_21);
        } else {
        }
    }
    __device__ float method_26(long long int var_0, float * var_1, float * var_2, float * var_3, float * var_4, float var_5, long long int var_6) {
        char var_7 = (var_6 < 32);
        if (var_7) {
            char var_8 = (var_6 >= 0);
            char var_9 = (var_8 == 0);
            if (var_9) {
                // unprinted assert;
            } else {
            }
            long long int var_10 = (var_6 * 16);
            char var_11 = (var_0 >= 0);
            char var_13;
            if (var_11) {
                var_13 = (var_0 < 16);
            } else {
                var_13 = 0;
            }
            char var_14 = (var_13 == 0);
            if (var_14) {
                // unprinted assert;
            } else {
            }
            long long int var_15 = (var_10 + var_0);
            float var_16 = var_1[var_15];
            float var_17 = var_2[var_15];
            float var_18 = var_3[var_15];
            float var_19 = var_4[var_0];
            float var_20 = (var_5 + var_17);
            long long int var_21 = (var_6 + 8);
            return method_26(var_0, var_1, var_2, var_3, var_4, var_20, var_21);
        } else {
            return var_5;
        }
    }
    __device__ void method_27(float * var_0, long long int var_1, float * var_2, long long int var_3, float var_4, long long int var_5) {
        char var_6 = (var_5 < 8);
        if (var_6) {
            char var_7 = (var_5 >= 1);
            char var_8 = (var_7 == 0);
            if (var_8) {
                // unprinted assert;
            } else {
            }
            long long int var_9 = (var_5 - 1);
            long long int var_10 = (var_1 + var_9);
            float var_11 = var_0[var_10];
            float var_12 = (var_4 + var_11);
            long long int var_13 = (var_5 + 1);
            method_27(var_0, var_1, var_2, var_3, var_12, var_13);
        } else {
            float var_14 = var_2[var_3];
            float var_15 = (var_4 + var_14);
            var_2[var_3] = var_15;
        }
    }
    __device__ void method_30(float * var_0, long long int var_1, float * var_2, float * var_3, float * var_4, float * var_5, long long int var_6) {
        char var_7 = (var_6 < 32);
        if (var_7) {
            char var_8 = (var_6 >= 0);
            char var_9 = (var_8 == 0);
            if (var_9) {
                // unprinted assert;
            } else {
            }
            long long int var_10 = (var_6 * 16);
            char var_11 = (var_1 >= 0);
            char var_13;
            if (var_11) {
                var_13 = (var_1 < 16);
            } else {
                var_13 = 0;
            }
            char var_14 = (var_13 == 0);
            if (var_14) {
                // unprinted assert;
            } else {
            }
            long long int var_15 = (var_10 + var_1);
            if (var_9) {
                // unprinted assert;
            } else {
            }
            char var_17;
            if (var_11) {
                var_17 = (var_1 < 16);
            } else {
                var_17 = 0;
            }
            char var_18 = (var_17 == 0);
            if (var_18) {
                // unprinted assert;
            } else {
            }
            float var_19 = var_0[var_1];
            float var_20 = var_2[var_15];
            float var_21 = var_3[var_15];
            float var_22 = var_4[var_15];
            float var_23 = var_5[var_15];
            float var_24 = (var_21 + var_23);
            var_5[var_15] = var_24;
            long long int var_25 = (var_6 + 8);
            method_30(var_0, var_1, var_2, var_3, var_4, var_5, var_25);
        } else {
        }
    }
}
"""

type Union0 =
    | Union0Case0 of Tuple1
    | Union0Case1
and Tuple1 =
    struct
    val mem_0: ManagedCuda.BasicTypes.CUdeviceptr
    new(arg_mem_0) = {mem_0 = arg_mem_0}
    end
and Env2 =
    struct
    val mem_0: Env3
    val mem_1: int64
    new(arg_mem_0, arg_mem_1) = {mem_0 = arg_mem_0; mem_1 = arg_mem_1}
    end
and Env3 =
    struct
    val mem_0: (Union0 ref)
    new(arg_mem_0) = {mem_0 = arg_mem_0}
    end
and Union4 =
    | Union4Case0 of Tuple5
    | Union4Case1
and Tuple5 =
    struct
    val mem_0: float32
    new(arg_mem_0) = {mem_0 = arg_mem_0}
    end
let rec method_0 ((var_0: System.Diagnostics.DataReceivedEventArgs)): unit =
    let (var_1: string) = var_0.get_Data()
    let (var_2: string) = System.String.Format("{0}",var_1)
    System.Console.WriteLine(var_2)
and method_1((var_0: (Union0 ref))): ManagedCuda.BasicTypes.CUdeviceptr =
    let (var_1: Union0) = (!var_0)
    match var_1 with
    | Union0Case0(var_2) ->
        var_2.mem_0
    | Union0Case1 ->
        (failwith "A Cuda memory cell that has been disposed has been tried to be accessed.")
and method_2((var_0: uint64), (var_1: System.Collections.Generic.Stack<Env2>), (var_2: uint64), (var_3: int64)): Env3 =
    let (var_4: int32) = var_1.get_Count()
    let (var_5: bool) = (var_4 > 0)
    if var_5 then
        let (var_6: Env2) = var_1.Peek()
        let (var_7: Env3) = var_6.mem_0
        let (var_8: int64) = var_6.mem_1
        let (var_9: (Union0 ref)) = var_7.mem_0
        let (var_10: Union0) = (!var_9)
        match var_10 with
        | Union0Case0(var_11) ->
            let (var_12: ManagedCuda.BasicTypes.CUdeviceptr) = var_11.mem_0
            method_3((var_12: ManagedCuda.BasicTypes.CUdeviceptr), (var_0: uint64), (var_2: uint64), (var_3: int64), (var_1: System.Collections.Generic.Stack<Env2>), (var_7: Env3), (var_8: int64))
        | Union0Case1 ->
            let (var_14: Env2) = var_1.Pop()
            let (var_15: Env3) = var_14.mem_0
            let (var_16: int64) = var_14.mem_1
            method_2((var_0: uint64), (var_1: System.Collections.Generic.Stack<Env2>), (var_2: uint64), (var_3: int64))
    else
        method_4((var_0: uint64), (var_2: uint64), (var_3: int64), (var_1: System.Collections.Generic.Stack<Env2>))
and method_5((var_0: (Union0 ref))): ManagedCuda.BasicTypes.CUdeviceptr =
    let (var_1: Union0) = (!var_0)
    match var_1 with
    | Union0Case0(var_2) ->
        var_2.mem_0
    | Union0Case1 ->
        (failwith "A Cuda memory cell that has been disposed has been tried to be accessed.")
and method_6((var_0: ManagedCuda.CudaBlas.CudaBlasHandle), (var_1: (Union0 ref)), (var_2: (Union0 ref)), (var_3: (Union0 ref))): unit =
    let (var_4: ManagedCuda.CudaBlas.Operation) = ManagedCuda.CudaBlas.Operation.NonTranspose
    let (var_5: ManagedCuda.CudaBlas.Operation) = ManagedCuda.CudaBlas.Operation.NonTranspose
    let (var_6: (float32 ref)) = (ref 1.000000f)
    let (var_7: ManagedCuda.BasicTypes.CUdeviceptr) = method_5((var_1: (Union0 ref)))
    let (var_8: ManagedCuda.BasicTypes.CUdeviceptr) = method_5((var_2: (Union0 ref)))
    let (var_9: (float32 ref)) = (ref 0.000000f)
    let (var_10: ManagedCuda.BasicTypes.CUdeviceptr) = method_5((var_3: (Union0 ref)))
    let (var_11: ManagedCuda.CudaBlas.CublasStatus) = ManagedCuda.CudaBlas.CudaBlasNativeMethods.cublasSgemm_v2(var_0, var_4, var_5, 32, 16, 6, var_6, var_7, 32, var_8, 6, var_9, var_10, 32)
    if var_11 <> ManagedCuda.CudaBlas.CublasStatus.Success then raise <| new ManagedCuda.CudaBlas.CudaBlasException(var_11)
and method_19((var_0: (Union0 ref)), (var_1: ManagedCuda.CudaContext), (var_2: (Union4 ref)), (var_3: (Union4 ref))): float32 =
    let (var_4: Union4) = (!var_3)
    match var_4 with
    | Union4Case0(var_5) ->
        var_5.mem_0
    | Union4Case1 ->
        let (var_7: float32) = method_17((var_0: (Union0 ref)), (var_1: ManagedCuda.CudaContext), (var_2: (Union4 ref)))
        var_3 := (Union4Case0(Tuple5(var_7)))
        var_7
and method_18((var_0: (Union0 ref)), (var_1: ManagedCuda.CudaContext), (var_2: (Union4 ref))): float32 =
    let (var_3: Union4) = (!var_2)
    match var_3 with
    | Union4Case0(var_4) ->
        var_4.mem_0
    | Union4Case1 ->
        let (var_6: float32) = method_15((var_0: (Union0 ref)), (var_1: ManagedCuda.CudaContext))
        var_2 := (Union4Case0(Tuple5(var_6)))
        var_6
and method_31((var_0: ManagedCuda.CudaBlas.CudaBlasHandle), (var_1: (Union0 ref)), (var_2: (Union0 ref)), (var_3: (Union0 ref))): unit =
    let (var_4: ManagedCuda.CudaBlas.Operation) = ManagedCuda.CudaBlas.Operation.Transpose
    let (var_5: ManagedCuda.CudaBlas.Operation) = ManagedCuda.CudaBlas.Operation.NonTranspose
    let (var_6: (float32 ref)) = (ref 1.000000f)
    let (var_7: ManagedCuda.BasicTypes.CUdeviceptr) = method_5((var_1: (Union0 ref)))
    let (var_8: ManagedCuda.BasicTypes.CUdeviceptr) = method_5((var_2: (Union0 ref)))
    let (var_9: (float32 ref)) = (ref 1.000000f)
    let (var_10: ManagedCuda.BasicTypes.CUdeviceptr) = method_5((var_3: (Union0 ref)))
    let (var_11: ManagedCuda.CudaBlas.CublasStatus) = ManagedCuda.CudaBlas.CudaBlasNativeMethods.cublasSgemm_v2(var_0, var_4, var_5, 6, 16, 32, var_6, var_7, 32, var_8, 32, var_9, var_10, 6)
    if var_11 <> ManagedCuda.CudaBlas.CublasStatus.Success then raise <| new ManagedCuda.CudaBlas.CudaBlasException(var_11)
and method_3((var_0: ManagedCuda.BasicTypes.CUdeviceptr), (var_1: uint64), (var_2: uint64), (var_3: int64), (var_4: System.Collections.Generic.Stack<Env2>), (var_5: Env3), (var_6: int64)): Env3 =
    let (var_7: ManagedCuda.BasicTypes.SizeT) = var_0.Pointer
    let (var_8: uint64) = uint64 var_7
    let (var_9: uint64) = uint64 var_6
    let (var_10: uint64) = (var_8 - var_1)
    let (var_11: uint64) = (var_10 + var_9)
    let (var_12: uint64) = uint64 var_3
    let (var_13: uint64) = (var_12 + var_11)
    let (var_14: bool) = (var_13 <= var_2)
    let (var_15: bool) = (var_14 = false)
    if var_15 then
        (failwith "Cache size has been exceeded in the allocator.")
    else
        ()
    let (var_16: uint64) = (var_8 + var_9)
    let (var_17: ManagedCuda.BasicTypes.SizeT) = ManagedCuda.BasicTypes.SizeT(var_16)
    let (var_18: ManagedCuda.BasicTypes.CUdeviceptr) = ManagedCuda.BasicTypes.CUdeviceptr(var_17)
    let (var_19: (Union0 ref)) = (ref (Union0Case0(Tuple1(var_18))))
    var_4.Push((Env2((Env3(var_19)), var_3)))
    (Env3(var_19))
and method_4((var_0: uint64), (var_1: uint64), (var_2: int64), (var_3: System.Collections.Generic.Stack<Env2>)): Env3 =
    let (var_4: uint64) = uint64 var_2
    let (var_5: bool) = (var_4 <= var_1)
    let (var_6: bool) = (var_5 = false)
    if var_6 then
        (failwith "Cache size has been exceeded in the allocator.")
    else
        ()
    let (var_7: ManagedCuda.BasicTypes.SizeT) = ManagedCuda.BasicTypes.SizeT(var_0)
    let (var_8: ManagedCuda.BasicTypes.CUdeviceptr) = ManagedCuda.BasicTypes.CUdeviceptr(var_7)
    let (var_9: (Union0 ref)) = (ref (Union0Case0(Tuple1(var_8))))
    var_3.Push((Env2((Env3(var_9)), var_2)))
    (Env3(var_9))
and method_17((var_0: (Union0 ref)), (var_1: ManagedCuda.CudaContext), (var_2: (Union4 ref))): float32 =
    let (var_3: float32) = method_18((var_0: (Union0 ref)), (var_1: ManagedCuda.CudaContext), (var_2: (Union4 ref)))
    (var_3 / 32.000000f)
and method_15((var_0: (Union0 ref)), (var_1: ManagedCuda.CudaContext)): float32 =
    let (var_2: ManagedCuda.BasicTypes.CUdeviceptr) = method_5((var_0: (Union0 ref)))
    let (var_3: (float32 [])) = Array.zeroCreate<float32> (System.Convert.ToInt32(4L))
    var_1.CopyToHost(var_3, var_2)
    var_1.Synchronize()
    let (var_4: float32) = 0.000000f
    let (var_5: int64) = 0L
    method_16((var_3: (float32 [])), (var_4: float32), (var_5: int64))
and method_16((var_0: (float32 [])), (var_1: float32), (var_2: int64)): float32 =
    let (var_3: bool) = (var_2 < 4L)
    if var_3 then
        let (var_4: bool) = (var_2 >= 0L)
        let (var_5: bool) = (var_4 = false)
        if var_5 then
            (failwith "Argument out of bounds.")
        else
            ()
        let (var_6: float32) = var_0.[int32 var_2]
        let (var_7: float32) = (var_1 + var_6)
        let (var_8: int64) = (var_2 + 1L)
        method_16((var_0: (float32 [])), (var_7: float32), (var_8: int64))
    else
        var_1
let (var_0: string) = cuda_kernels
let (var_1: ManagedCuda.CudaContext) = ManagedCuda.CudaContext(false)
var_1.Synchronize()
let (var_2: string) = System.Environment.get_CurrentDirectory()
let (var_3: string) = System.IO.Path.Combine(var_2, "nvcc_router.bat")
let (var_4: System.Diagnostics.ProcessStartInfo) = System.Diagnostics.ProcessStartInfo()
var_4.set_RedirectStandardOutput(true)
var_4.set_RedirectStandardError(true)
var_4.set_UseShellExecute(false)
var_4.set_FileName(var_3)
let (var_5: System.Diagnostics.Process) = System.Diagnostics.Process()
var_5.set_StartInfo(var_4)
let (var_7: (System.Diagnostics.DataReceivedEventArgs -> unit)) = method_0
var_5.OutputDataReceived.Add(var_7)
var_5.ErrorDataReceived.Add(var_7)
let (var_8: string) = System.IO.Path.Combine("C:\\Program Files (x86)\\Microsoft Visual Studio\\2017\\Community", "VC\\Auxiliary\\Build\\vcvars64.bat")
let (var_9: string) = System.IO.Path.Combine("C:\\Program Files (x86)\\Microsoft Visual Studio\\2017\\Community", "VC\\Tools\\MSVC\\14.11.25503\\bin\\Hostx64\\x64")
let (var_10: string) = System.IO.Path.Combine("C:\\Program Files\\NVIDIA GPU Computing Toolkit\\CUDA\\v9.0", "include")
let (var_11: string) = System.IO.Path.Combine("C:\\Program Files (x86)\\Microsoft Visual Studio\\2017\\Community", "VC\\Tools\\MSVC\\14.11.25503\\include")
let (var_12: string) = System.IO.Path.Combine("C:\\Program Files\\NVIDIA GPU Computing Toolkit\\CUDA\\v9.0", "bin\\nvcc.exe")
let (var_13: string) = System.IO.Path.Combine(var_2, "cuda_kernels.ptx")
let (var_14: string) = System.IO.Path.Combine(var_2, "cuda_kernels.cu")
let (var_15: bool) = System.IO.File.Exists(var_14)
if var_15 then
    System.IO.File.Delete(var_14)
else
    ()
System.IO.File.WriteAllText(var_14, var_0)
let (var_16: bool) = System.IO.File.Exists(var_3)
if var_16 then
    System.IO.File.Delete(var_3)
else
    ()
let (var_17: System.IO.FileStream) = System.IO.File.OpenWrite(var_3)
let (var_18: System.IO.StreamWriter) = System.IO.StreamWriter(var_17)
var_18.WriteLine("SETLOCAL")
let (var_19: string) = String.concat "" [|"CALL "; "\""; var_8; "\""|]
var_18.WriteLine(var_19)
let (var_20: string) = String.concat "" [|"SET PATH=%PATH%;"; "\""; var_9; "\""|]
var_18.WriteLine(var_20)
let (var_21: string) = String.concat "" [|"\""; var_12; "\" -gencode=arch=compute_30,code=\\\"sm_30,compute_30\\\" --use-local-env --cl-version 2017 -I\""; var_10; "\" -I\"C:\\cub-1.7.4\" -I\""; var_11; "\" --keep-dir \""; var_2; "\" -maxrregcount=0  --machine 64 -ptx -cudart static  -o \""; var_13; "\" \""; var_14; "\""|]
var_18.WriteLine(var_21)
var_18.Dispose()
var_17.Dispose()
let (var_22: System.Diagnostics.Stopwatch) = System.Diagnostics.Stopwatch.StartNew()
let (var_23: bool) = var_5.Start()
let (var_24: bool) = (var_23 = false)
if var_24 then
    (failwith "NVCC failed to run.")
else
    ()
var_5.BeginOutputReadLine()
var_5.BeginErrorReadLine()
var_5.WaitForExit()
let (var_25: int32) = var_5.get_ExitCode()
let (var_26: bool) = (var_25 = 0)
let (var_27: bool) = (var_26 = false)
if var_27 then
    let (var_28: string) = System.String.Format("{0}",var_25)
    let (var_29: string) = String.concat ", " [|"NVCC failed compilation."; var_28|]
    let (var_30: string) = System.String.Format("[{0}]",var_29)
    (failwith var_30)
else
    ()
let (var_31: System.TimeSpan) = var_22.get_Elapsed()
printfn "The time it took to compile the Cuda kernels is: %A" var_31
let (var_32: ManagedCuda.BasicTypes.CUmodule) = var_1.LoadModulePTX(var_13)
var_5.Dispose()
let (var_33: string) = String.concat "" [|"Compiled the kernels into the following directory: "; var_2|]
let (var_34: string) = System.String.Format("{0}",var_33)
System.Console.WriteLine(var_34)
let (var_35: ManagedCuda.CudaDeviceProperties) = var_1.GetDeviceInfo()
let (var_36: ManagedCuda.BasicTypes.SizeT) = var_35.get_TotalGlobalMemory()
let (var_37: int64) = int64 var_36
let (var_38: float) = float var_37
let (var_39: float) = (0.700000 * var_38)
let (var_40: int64) = int64 var_39
let (var_41: ManagedCuda.BasicTypes.SizeT) = ManagedCuda.BasicTypes.SizeT(var_40)
let (var_42: ManagedCuda.BasicTypes.CUdeviceptr) = var_1.AllocateMemory(var_41)
let (var_43: (Union0 ref)) = (ref (Union0Case0(Tuple1(var_42))))
let (var_44: System.Collections.Generic.Stack<Env2>) = System.Collections.Generic.Stack<Env2>()
let (var_45: ManagedCuda.BasicTypes.CUdeviceptr) = method_1((var_43: (Union0 ref)))
let (var_46: ManagedCuda.BasicTypes.SizeT) = var_45.Pointer
let (var_47: uint64) = uint64 var_46
let (var_48: uint64) = uint64 var_40
let (var_49: ManagedCuda.CudaStream) = ManagedCuda.CudaStream()
let (var_50: ManagedCuda.CudaRand.GeneratorType) = ManagedCuda.CudaRand.GeneratorType.PseudoDefault
let (var_51: ManagedCuda.CudaRand.CudaRandDevice) = ManagedCuda.CudaRand.CudaRandDevice(var_50)
let (var_52: ManagedCuda.BasicTypes.CUstream) = var_49.get_Stream()
var_51.SetStream(var_52)
let (var_53: ManagedCuda.CudaBlas.PointerMode) = ManagedCuda.CudaBlas.PointerMode.Host
let (var_54: ManagedCuda.CudaBlas.AtomicsMode) = ManagedCuda.CudaBlas.AtomicsMode.Allowed
let (var_55: ManagedCuda.CudaBlas.CudaBlas) = ManagedCuda.CudaBlas.CudaBlas(var_53, var_54)
let (var_56: ManagedCuda.CudaBlas.CudaBlasHandle) = var_55.get_CublasHandle()
let (var_57: ManagedCuda.BasicTypes.CUstream) = var_49.get_Stream()
var_55.set_Stream(var_57)
let (var_58: int64) = 768L
let (var_59: Env3) = method_2((var_47: uint64), (var_44: System.Collections.Generic.Stack<Env2>), (var_48: uint64), (var_58: int64))
let (var_60: (Union0 ref)) = var_59.mem_0
let (var_61: ManagedCuda.BasicTypes.CUdeviceptr) = method_5((var_60: (Union0 ref)))
let (var_62: ManagedCuda.BasicTypes.SizeT) = ManagedCuda.BasicTypes.SizeT(192L)
var_51.GenerateNormal32(var_61, var_62, 0.000000f, 1.000000f)
let (var_63: int64) = 384L
let (var_64: Env3) = method_2((var_47: uint64), (var_44: System.Collections.Generic.Stack<Env2>), (var_48: uint64), (var_63: int64))
let (var_65: (Union0 ref)) = var_64.mem_0
let (var_66: ManagedCuda.BasicTypes.CUdeviceptr) = method_5((var_65: (Union0 ref)))
let (var_67: ManagedCuda.BasicTypes.SizeT) = ManagedCuda.BasicTypes.SizeT(96L)
var_51.GenerateNormal32(var_66, var_67, 0.000000f, 1.000000f)
let (var_68: int64) = 384L
let (var_69: Env3) = method_2((var_47: uint64), (var_44: System.Collections.Generic.Stack<Env2>), (var_48: uint64), (var_68: int64))
let (var_70: (Union0 ref)) = var_69.mem_0
let (var_71: ManagedCuda.BasicTypes.CUdeviceptr) = method_5((var_70: (Union0 ref)))
let (var_72: ManagedCuda.BasicTypes.CUstream) = var_49.get_Stream()
let (var_73: ManagedCuda.BasicTypes.SizeT) = ManagedCuda.BasicTypes.SizeT(384L)
var_1.ClearMemoryAsync(var_71, 0uy, var_73, var_72)
let (var_74: int64) = 64L
let (var_75: Env3) = method_2((var_47: uint64), (var_44: System.Collections.Generic.Stack<Env2>), (var_48: uint64), (var_74: int64))
let (var_76: (Union0 ref)) = var_75.mem_0
let (var_77: ManagedCuda.BasicTypes.CUdeviceptr) = method_5((var_76: (Union0 ref)))
let (var_78: ManagedCuda.BasicTypes.CUstream) = var_49.get_Stream()
let (var_79: ManagedCuda.BasicTypes.SizeT) = ManagedCuda.BasicTypes.SizeT(64L)
var_1.ClearMemoryAsync(var_77, 0uy, var_79, var_78)
let (var_80: int64) = 64L
let (var_81: Env3) = method_2((var_47: uint64), (var_44: System.Collections.Generic.Stack<Env2>), (var_48: uint64), (var_80: int64))
let (var_82: (Union0 ref)) = var_81.mem_0
let (var_83: ManagedCuda.BasicTypes.CUdeviceptr) = method_5((var_82: (Union0 ref)))
let (var_84: ManagedCuda.BasicTypes.CUstream) = var_49.get_Stream()
let (var_85: ManagedCuda.BasicTypes.SizeT) = ManagedCuda.BasicTypes.SizeT(64L)
var_1.ClearMemoryAsync(var_83, 0uy, var_85, var_84)
let (var_86: int64) = 2048L
let (var_87: Env3) = method_2((var_47: uint64), (var_44: System.Collections.Generic.Stack<Env2>), (var_48: uint64), (var_86: int64))
let (var_88: (Union0 ref)) = var_87.mem_0
let (var_89: ManagedCuda.BasicTypes.CUdeviceptr) = method_5((var_88: (Union0 ref)))
let (var_90: ManagedCuda.BasicTypes.CUstream) = var_49.get_Stream()
let (var_91: ManagedCuda.BasicTypes.SizeT) = ManagedCuda.BasicTypes.SizeT(2048L)
var_1.ClearMemoryAsync(var_89, 0uy, var_91, var_90)
let (var_92: int64) = 2048L
let (var_93: Env3) = method_2((var_47: uint64), (var_44: System.Collections.Generic.Stack<Env2>), (var_48: uint64), (var_92: int64))
let (var_94: (Union0 ref)) = var_93.mem_0
method_6((var_56: ManagedCuda.CudaBlas.CudaBlasHandle), (var_60: (Union0 ref)), (var_65: (Union0 ref)), (var_94: (Union0 ref)))
let (var_95: int64) = 2048L
let (var_96: Env3) = method_2((var_47: uint64), (var_44: System.Collections.Generic.Stack<Env2>), (var_48: uint64), (var_95: int64))
let (var_97: (Union0 ref)) = var_96.mem_0
let (var_98: ManagedCuda.BasicTypes.CUdeviceptr) = method_5((var_97: (Union0 ref)))
let (var_99: ManagedCuda.BasicTypes.CUstream) = var_49.get_Stream()
let (var_100: ManagedCuda.BasicTypes.SizeT) = ManagedCuda.BasicTypes.SizeT(2048L)
var_1.ClearMemoryAsync(var_98, 0uy, var_100, var_99)
let (var_102: int64) = 2048L
let (var_103: Env3) = method_2((var_47: uint64), (var_44: System.Collections.Generic.Stack<Env2>), (var_48: uint64), (var_102: int64))
let (var_104: (Union0 ref)) = var_103.mem_0
let (var_105: ManagedCuda.BasicTypes.CUdeviceptr) = method_5((var_76: (Union0 ref)))
let (var_106: ManagedCuda.BasicTypes.CUdeviceptr) = method_5((var_94: (Union0 ref)))
let (var_107: ManagedCuda.BasicTypes.CUdeviceptr) = method_5((var_104: (Union0 ref)))
// Cuda join point
// method_7((var_105: ManagedCuda.BasicTypes.CUdeviceptr), (var_106: ManagedCuda.BasicTypes.CUdeviceptr), (var_107: ManagedCuda.BasicTypes.CUdeviceptr))
let (var_109: (System.Object [])) = [|var_105; var_106; var_107|]: (System.Object [])
let (var_110: ManagedCuda.CudaKernel) = ManagedCuda.CudaKernel("method_7", var_32, var_1)
let (var_111: ManagedCuda.VectorTypes.dim3) = ManagedCuda.VectorTypes.dim3(1u, 1u, 1u)
var_110.set_GridDimensions(var_111)
let (var_112: ManagedCuda.VectorTypes.dim3) = ManagedCuda.VectorTypes.dim3(16u, 8u, 1u)
var_110.set_BlockDimensions(var_112)
let (var_113: ManagedCuda.BasicTypes.CUstream) = var_49.get_Stream()
var_110.RunAsync(var_113, var_109)
let (var_114: int64) = 2048L
let (var_115: Env3) = method_2((var_47: uint64), (var_44: System.Collections.Generic.Stack<Env2>), (var_48: uint64), (var_114: int64))
let (var_116: (Union0 ref)) = var_115.mem_0
let (var_117: ManagedCuda.BasicTypes.CUdeviceptr) = method_5((var_116: (Union0 ref)))
let (var_118: ManagedCuda.BasicTypes.CUstream) = var_49.get_Stream()
let (var_119: ManagedCuda.BasicTypes.SizeT) = ManagedCuda.BasicTypes.SizeT(2048L)
var_1.ClearMemoryAsync(var_117, 0uy, var_119, var_118)
let (var_124: int64) = 2048L
let (var_125: Env3) = method_2((var_47: uint64), (var_44: System.Collections.Generic.Stack<Env2>), (var_48: uint64), (var_124: int64))
let (var_126: (Union0 ref)) = var_125.mem_0
let (var_127: ManagedCuda.BasicTypes.CUdeviceptr) = method_5((var_104: (Union0 ref)))
let (var_128: ManagedCuda.BasicTypes.CUdeviceptr) = method_5((var_126: (Union0 ref)))
// Cuda join point
// method_10((var_127: ManagedCuda.BasicTypes.CUdeviceptr), (var_128: ManagedCuda.BasicTypes.CUdeviceptr))
let (var_130: (System.Object [])) = [|var_127; var_128|]: (System.Object [])
let (var_131: ManagedCuda.CudaKernel) = ManagedCuda.CudaKernel("method_10", var_32, var_1)
let (var_132: ManagedCuda.VectorTypes.dim3) = ManagedCuda.VectorTypes.dim3(4u, 1u, 1u)
var_131.set_GridDimensions(var_132)
let (var_133: ManagedCuda.VectorTypes.dim3) = ManagedCuda.VectorTypes.dim3(128u, 1u, 1u)
var_131.set_BlockDimensions(var_133)
let (var_134: ManagedCuda.BasicTypes.CUstream) = var_49.get_Stream()
var_131.RunAsync(var_134, var_130)
let (var_135: int64) = 2048L
let (var_136: Env3) = method_2((var_47: uint64), (var_44: System.Collections.Generic.Stack<Env2>), (var_48: uint64), (var_135: int64))
let (var_137: (Union0 ref)) = var_136.mem_0
let (var_138: ManagedCuda.BasicTypes.CUdeviceptr) = method_5((var_137: (Union0 ref)))
let (var_139: ManagedCuda.BasicTypes.CUstream) = var_49.get_Stream()
let (var_140: ManagedCuda.BasicTypes.SizeT) = ManagedCuda.BasicTypes.SizeT(2048L)
var_1.ClearMemoryAsync(var_138, 0uy, var_140, var_139)
let (var_141: ManagedCuda.BasicTypes.CUdeviceptr) = method_5((var_126: (Union0 ref)))
let (var_142: ManagedCuda.BasicTypes.CUdeviceptr) = method_5((var_88: (Union0 ref)))
let (var_145: int64) = 16L
let (var_146: Env3) = method_2((var_47: uint64), (var_44: System.Collections.Generic.Stack<Env2>), (var_48: uint64), (var_145: int64))
let (var_147: (Union0 ref)) = var_146.mem_0
let (var_148: ManagedCuda.BasicTypes.CUdeviceptr) = method_5((var_147: (Union0 ref)))
// Cuda join point
// method_12((var_141: ManagedCuda.BasicTypes.CUdeviceptr), (var_142: ManagedCuda.BasicTypes.CUdeviceptr), (var_148: ManagedCuda.BasicTypes.CUdeviceptr))
let (var_150: (System.Object [])) = [|var_141; var_142; var_148|]: (System.Object [])
let (var_151: ManagedCuda.CudaKernel) = ManagedCuda.CudaKernel("method_12", var_32, var_1)
let (var_152: ManagedCuda.VectorTypes.dim3) = ManagedCuda.VectorTypes.dim3(4u, 1u, 1u)
var_151.set_GridDimensions(var_152)
let (var_153: ManagedCuda.VectorTypes.dim3) = ManagedCuda.VectorTypes.dim3(128u, 1u, 1u)
var_151.set_BlockDimensions(var_153)
let (var_154: ManagedCuda.BasicTypes.CUstream) = var_49.get_Stream()
var_151.RunAsync(var_154, var_150)
let (var_156: (Union4 ref)) = (ref Union4Case1)
let (var_157: (float32 ref)) = (ref 0.000000f)
let (var_159: (Union4 ref)) = (ref Union4Case1)
let (var_160: (float32 ref)) = (ref 0.000000f)
let (var_161: float32) = method_19((var_147: (Union0 ref)), (var_1: ManagedCuda.CudaContext), (var_156: (Union4 ref)), (var_159: (Union4 ref)))
let (var_162: string) = System.String.Format("{0}",var_161)
let (var_163: string) = String.concat ", " [|"Cost is:"; var_162|]
let (var_164: string) = System.String.Format("[{0}]",var_163)
System.Console.WriteLine(var_164)
var_160 := 1.000000f
let (var_165: float32) = method_19((var_147: (Union0 ref)), (var_1: ManagedCuda.CudaContext), (var_156: (Union4 ref)), (var_159: (Union4 ref)))
let (var_166: float32) = (!var_160)
let (var_167: float32) = method_18((var_147: (Union0 ref)), (var_1: ManagedCuda.CudaContext), (var_156: (Union4 ref)))
let (var_168: float32) = (var_166 / 32.000000f)
let (var_169: float32) = (!var_157)
let (var_170: float32) = (var_169 + var_168)
var_157 := var_170
let (var_171: float32) = method_18((var_147: (Union0 ref)), (var_1: ManagedCuda.CudaContext), (var_156: (Union4 ref)))
let (var_172: float32) = (!var_157)
let (var_173: ManagedCuda.BasicTypes.CUdeviceptr) = method_5((var_126: (Union0 ref)))
let (var_174: ManagedCuda.BasicTypes.CUdeviceptr) = method_5((var_88: (Union0 ref)))
let (var_175: ManagedCuda.BasicTypes.CUdeviceptr) = method_5((var_137: (Union0 ref)))
// Cuda join point
// method_20((var_172: float32), (var_171: float32), (var_173: ManagedCuda.BasicTypes.CUdeviceptr), (var_174: ManagedCuda.BasicTypes.CUdeviceptr), (var_175: ManagedCuda.BasicTypes.CUdeviceptr))
let (var_177: (System.Object [])) = [|var_172; var_171; var_173; var_174; var_175|]: (System.Object [])
let (var_178: ManagedCuda.CudaKernel) = ManagedCuda.CudaKernel("method_20", var_32, var_1)
let (var_179: ManagedCuda.VectorTypes.dim3) = ManagedCuda.VectorTypes.dim3(4u, 1u, 1u)
var_178.set_GridDimensions(var_179)
let (var_180: ManagedCuda.VectorTypes.dim3) = ManagedCuda.VectorTypes.dim3(128u, 1u, 1u)
var_178.set_BlockDimensions(var_180)
let (var_181: ManagedCuda.BasicTypes.CUstream) = var_49.get_Stream()
var_178.RunAsync(var_181, var_177)
let (var_182: ManagedCuda.BasicTypes.CUdeviceptr) = method_5((var_104: (Union0 ref)))
let (var_183: ManagedCuda.BasicTypes.CUdeviceptr) = method_5((var_137: (Union0 ref)))
let (var_184: ManagedCuda.BasicTypes.CUdeviceptr) = method_5((var_126: (Union0 ref)))
let (var_185: ManagedCuda.BasicTypes.CUdeviceptr) = method_5((var_116: (Union0 ref)))
// Cuda join point
// method_22((var_182: ManagedCuda.BasicTypes.CUdeviceptr), (var_183: ManagedCuda.BasicTypes.CUdeviceptr), (var_184: ManagedCuda.BasicTypes.CUdeviceptr), (var_185: ManagedCuda.BasicTypes.CUdeviceptr))
let (var_187: (System.Object [])) = [|var_182; var_183; var_184; var_185|]: (System.Object [])
let (var_188: ManagedCuda.CudaKernel) = ManagedCuda.CudaKernel("method_22", var_32, var_1)
let (var_189: ManagedCuda.VectorTypes.dim3) = ManagedCuda.VectorTypes.dim3(4u, 1u, 1u)
var_188.set_GridDimensions(var_189)
let (var_190: ManagedCuda.VectorTypes.dim3) = ManagedCuda.VectorTypes.dim3(128u, 1u, 1u)
var_188.set_BlockDimensions(var_190)
let (var_191: ManagedCuda.BasicTypes.CUstream) = var_49.get_Stream()
var_188.RunAsync(var_191, var_187)
let (var_192: ManagedCuda.BasicTypes.CUdeviceptr) = method_5((var_94: (Union0 ref)))
let (var_193: ManagedCuda.BasicTypes.CUdeviceptr) = method_5((var_116: (Union0 ref)))
let (var_194: ManagedCuda.BasicTypes.CUdeviceptr) = method_5((var_104: (Union0 ref)))
let (var_195: ManagedCuda.BasicTypes.CUdeviceptr) = method_5((var_76: (Union0 ref)))
let (var_196: ManagedCuda.BasicTypes.CUdeviceptr) = method_5((var_82: (Union0 ref)))
// Cuda join point
// method_24((var_192: ManagedCuda.BasicTypes.CUdeviceptr), (var_193: ManagedCuda.BasicTypes.CUdeviceptr), (var_194: ManagedCuda.BasicTypes.CUdeviceptr), (var_195: ManagedCuda.BasicTypes.CUdeviceptr), (var_196: ManagedCuda.BasicTypes.CUdeviceptr))
let (var_198: (System.Object [])) = [|var_192; var_193; var_194; var_195; var_196|]: (System.Object [])
let (var_199: ManagedCuda.CudaKernel) = ManagedCuda.CudaKernel("method_24", var_32, var_1)
let (var_200: ManagedCuda.VectorTypes.dim3) = ManagedCuda.VectorTypes.dim3(1u, 1u, 1u)
var_199.set_GridDimensions(var_200)
let (var_201: ManagedCuda.VectorTypes.dim3) = ManagedCuda.VectorTypes.dim3(16u, 8u, 1u)
var_199.set_BlockDimensions(var_201)
let (var_202: ManagedCuda.BasicTypes.CUstream) = var_49.get_Stream()
var_199.RunAsync(var_202, var_198)
let (var_203: ManagedCuda.BasicTypes.CUdeviceptr) = method_5((var_76: (Union0 ref)))
let (var_204: ManagedCuda.BasicTypes.CUdeviceptr) = method_5((var_94: (Union0 ref)))
let (var_205: ManagedCuda.BasicTypes.CUdeviceptr) = method_5((var_116: (Union0 ref)))
let (var_206: ManagedCuda.BasicTypes.CUdeviceptr) = method_5((var_104: (Union0 ref)))
let (var_207: ManagedCuda.BasicTypes.CUdeviceptr) = method_5((var_97: (Union0 ref)))
// Cuda join point
// method_28((var_203: ManagedCuda.BasicTypes.CUdeviceptr), (var_204: ManagedCuda.BasicTypes.CUdeviceptr), (var_205: ManagedCuda.BasicTypes.CUdeviceptr), (var_206: ManagedCuda.BasicTypes.CUdeviceptr), (var_207: ManagedCuda.BasicTypes.CUdeviceptr))
let (var_209: (System.Object [])) = [|var_203; var_204; var_205; var_206; var_207|]: (System.Object [])
let (var_210: ManagedCuda.CudaKernel) = ManagedCuda.CudaKernel("method_28", var_32, var_1)
let (var_211: ManagedCuda.VectorTypes.dim3) = ManagedCuda.VectorTypes.dim3(1u, 1u, 1u)
var_210.set_GridDimensions(var_211)
let (var_212: ManagedCuda.VectorTypes.dim3) = ManagedCuda.VectorTypes.dim3(16u, 8u, 1u)
var_210.set_BlockDimensions(var_212)
let (var_213: ManagedCuda.BasicTypes.CUstream) = var_49.get_Stream()
var_210.RunAsync(var_213, var_209)
method_31((var_56: ManagedCuda.CudaBlas.CudaBlasHandle), (var_60: (Union0 ref)), (var_97: (Union0 ref)), (var_70: (Union0 ref)))
var_147 := Union0Case1
var_137 := Union0Case1
var_126 := Union0Case1
var_116 := Union0Case1
var_104 := Union0Case1
var_97 := Union0Case1
var_94 := Union0Case1
var_88 := Union0Case1
var_82 := Union0Case1
var_76 := Union0Case1
var_70 := Union0Case1
var_65 := Union0Case1
var_60 := Union0Case1
var_55.Dispose()
var_51.Dispose()
var_49.Dispose()
let (var_214: ManagedCuda.BasicTypes.CUdeviceptr) = method_1((var_43: (Union0 ref)))
var_1.FreeMemory(var_214)
var_43 := Union0Case1
var_1.Dispose()

