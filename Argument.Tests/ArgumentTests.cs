using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq.Expressions;
using System.Reflection;
using Xunit;

// ReSharper disable CheckNamespace
public class ArgumentTests {
    [Theory]
    [MemberData("NullVariants")]
    public void NotNullVariant_ThrowsArgumentNullException_WithCorrectMessage_WhenValueIsNull(Expression<Action<string>> validateExpression) {
        var validate = validateExpression.Compile();
        var exception = Assert.Throws<ArgumentNullException>(() => validate("x"));
        Assert.Equal(new ArgumentNullException("x").Message, exception.Message);
    }

    [Theory]
    [MemberData("NullVariants")]
    public void NotNullVariant_ThrowsArgumentNullException_WithCorrectParamName_WhenValueIsNull(Expression<Action<string>> validateExpression) {
        var validate = validateExpression.Compile();
        var exception = Assert.Throws<ArgumentNullException>(() => validate("x"));
        Assert.Equal("x", exception.ParamName);
    }

    [Theory]
    [MemberData("EmptyVariants")]
    public void NotNullOrEmptyVariant_ThrowsArgumentException_WithCorrectParamName_WhenValueIsEmpty(Expression<Action<string>> validateExpression) {
        var validate = validateExpression.Compile();
        var exception = Assert.Throws<ArgumentException>(() => validate("x"));
        Assert.Equal("x", exception.ParamName);
    }

    [Theory]
    [MemberData("IncorrectTypeVariants")]
    public void CastVariant_ThrowsArgumentException_WithCorrectParamName_WhenValueHasIncorrectType(Expression<Action<string>> validateExpression) {
        var validate = validateExpression.Compile();
        var exception = Assert.Throws<ArgumentException>(() => validate("x"));
        Assert.Equal("x", exception.ParamName);
    }

    [Theory]
    [MemberData("OutOfRangeVariants")]
    public void RangeVariant_ThrowsArgumentOutOfRangeException_WithCorrectParamName_WhenValueIsOutOfRange(Expression<Action<string>> validateExpression) {
        var validate = validateExpression.Compile();
        var exception = Assert.Throws<ArgumentOutOfRangeException>(() => validate("x"));
        Assert.Equal("x", exception.ParamName);
    }
    
    [Theory]
    [MemberData("SuccessVariants")]
    public void AnyVariant_ReturnsArgumentValue_WhenSuccessful<T>(T argument, Expression<Func<T, T>> validateExpression) {
        var validate = validateExpression.Compile();
        var result = validate(argument);

        if (typeof(T).GetTypeInfo().IsValueType) {
            Assert.Equal(argument, result);
        }
        else {
            Assert.Same(argument, result);
        }
    }

    public static IEnumerable<object[]> NullVariants {
        get {
            yield return SimpleDataRow(name => Argument.NotNull(name, (object)null));
            yield return SimpleDataRow(name => Argument.NotNull(name, (int?)null));
            yield return SimpleDataRow(name => Argument.NotNullOrEmpty(name, null));
            yield return SimpleDataRow(name => Argument.NotNullOrEmpty(name, (object[])null));
            yield return SimpleDataRow(name => Argument.NotNullOrEmpty(name, (List<object>)null));
            yield return SimpleDataRow(name => Argument.NotNullAndCast<object>(name, null));
        }
    }

    public static IEnumerable<object[]> EmptyVariants {
        get {
            yield return SimpleDataRow(name => Argument.NotNullOrEmpty(name, ""));
            yield return SimpleDataRow(name => Argument.NotNullOrEmpty(name, new object[0]));
            yield return SimpleDataRow(name => Argument.NotNullOrEmpty(name, new List<object>()));
            yield return SimpleDataRow(name => Argument.NotNullOrEmpty(name, new List<int>()));
            yield return SimpleDataRow(name => Argument.NotNullOrEmpty(name, (IReadOnlyCollection<int>)new List<int>()));
            yield return SimpleDataRow(name => Argument.NotNullOrEmpty(name, ImmutableList.Create<int>()));
        }
    }

    public static IEnumerable<object[]> IncorrectTypeVariants {
        get {
            yield return SimpleDataRow(name => Argument.Cast<string>(name, new object()));
        }
    }

    public static IEnumerable<object[]> OutOfRangeVariants {
        get {
            yield return SimpleDataRow(name => Argument.PositiveNonZero(name, -1));
            yield return SimpleDataRow(name => Argument.PositiveNonZero(name, int.MinValue));
            yield return SimpleDataRow(name => Argument.PositiveNonZero(name, 0));
            yield return SimpleDataRow(name => Argument.PositiveOrZero(name, -1));
            yield return SimpleDataRow(name => Argument.PositiveOrZero(name, int.MinValue));
        }
    }

    public static IEnumerable<object[]> SuccessVariants {
        get {
            yield return SuccessDataRow(new object(),  value => Argument.NotNull("x", value));
            yield return SuccessDataRow("abc",         value => Argument.NotNullOrEmpty("x", value));
            yield return SuccessDataRow(new object[1], value => Argument.NotNullOrEmpty("x", value));
            yield return SuccessDataRow(new List<object> { new object() }, value => Argument.NotNullOrEmpty("x", value));

            yield return SuccessDataRow("abc",         value => Argument.Cast<string>("x", value));
            yield return SuccessDataRow("abc",         value => Argument.NotNullAndCast<string>("x", value));
            
            yield return SuccessDataRow(1,             value => Argument.PositiveNonZero("x", value));
            yield return SuccessDataRow(int.MaxValue,  value => Argument.PositiveNonZero("x", value));
            yield return SuccessDataRow(1,             value => Argument.PositiveOrZero("x", value));
            yield return SuccessDataRow(int.MaxValue,  value => Argument.PositiveOrZero("x", value));
            yield return SuccessDataRow(0,             value => Argument.PositiveOrZero("x", value));

        }
    }

    private static object[] SimpleDataRow(Expression<Action<string>> action) {
        return new object[] { action };
    }

    private static object[] SuccessDataRow<T>(T argument, Expression<Func<T, T>> validate) {
        return new object[] { argument, validate };
    }
}