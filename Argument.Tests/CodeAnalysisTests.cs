using System;
using System.Collections.Generic;
using System.Text;

// nothing here should generate warnings
public class CodeAnalysisTests {
    public int CA1062_ArgumentCheck_NotRequired_NotNull(string argument) {
        Argument.NotNull(nameof(argument), argument);
        return argument.Length;
    }

    public int CA1062ArgumentCheck_NotRequired_NotNullOrEmpty_String(string argument) {
        Argument.NotNullOrEmpty(nameof(argument), argument);
        return argument.Length;
    }

    public int CA1062_ArgumentCheck_NotRequired_NotNullOrEmpty_Array(int[] argument) {
        Argument.NotNullOrEmpty(nameof(argument), argument);
        return argument.Length;
    }

    public int CA1062_ArgumentCheck_NotRequired_NotNullOrEmpty_List(IReadOnlyList<int> argument) {
        Argument.NotNullOrEmpty(nameof(argument), argument);
        return argument.Count;
    }

    public int CA1062_ArgumentCheck_NotRequired_NotNullAndCast(string argument) {
        Argument.NotNullAndCast<string>(nameof(argument), argument);
        return argument.Length;
    }
}
