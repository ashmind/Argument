using System;
using System.Collections.Generic;

#if !NET45 && !DOTNET
// ReSharper disable once CheckNamespace
namespace System.Diagnostics.Contracts {
    [Conditional("JUST_A_SHIM")]
    internal class ContractArgumentValidatorAttribute : Attribute {}

    internal static class Contract {
        [Conditional("JUST_A_SHIM")]
        public static void EndContractBlock() {
        }
    }
}
#endif