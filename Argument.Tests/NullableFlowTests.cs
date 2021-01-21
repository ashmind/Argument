using System.Collections.Generic;

// nothing here should generate warnings (which are set to produce errors anyways)
public class NullableFlowTests {
    public int CS8602_ValueIsNotNullable_After_NotNull(string? argument) {
        Argument.NotNull(nameof(argument), argument!);
        return argument.Length;
    }

    public int CS8602_ValueIsNotNullable_After_NotNullOrEmpty_String(string? argument) {
        Argument.NotNullOrEmpty(nameof(argument), argument!);
        return argument.Length;
    }

    public int CS8602_ValueIsNotNullable_After_NotNullOrEmpty_Array(int[]? argument) {
        Argument.NotNullOrEmpty(nameof(argument), argument!);
        return argument.Length;
    }

    public int CS8602_ValueIsNotNullable_After_NotNullOrEmpty_List(IReadOnlyList<int>? argument) {
        Argument.NotNullOrEmpty(nameof(argument), argument!);
        return argument.Count;
    }

    public int CS8602_ValueIsNotNullable_After_NotNullOrWhiteSpace_String(string? argument) {
        Argument.NotNullOrWhiteSpace(nameof(argument), argument!);
        return argument.Length;
    }

    public int CS8602_ValueIsNotNullable_After_NotNullAndCast(string? argument) {
        Argument.NotNullAndCast<string>(nameof(argument), argument!);
        return argument.Length;
    }
}
