﻿using System;
using System.Collections.Generic;

#if !NET45_CONTRACTS
// ReSharper disable once CheckNamespace
namespace System.Diagnostics.Contracts {
    [Conditional("JUST_A_SHIM")]
    internal class ContractArgumentValidatorAttribute : Attribute {}

    public static class Contract {
        [Conditional("JUST_A_SHIM")]
        public static void EndContractBlock() {
        }
    }
}
#endif