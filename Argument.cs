using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;

#if NET45_CONTRACTS
using System.Diagnostics.Contracts;
#endif

// ReSharper disable CheckNamespace
/// <summary>
/// Provides methods for verification of argument preconditions.
/// </summary>
public static class Argument {
// ReSharper restore CheckNamespace
    /// <summary>
    /// Provides an extensibility point that can be used by custom argument validation extension methods.
    /// </summary>
    /// <seealso cref="Argument.Ex"/>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public sealed class Extensible {
        #pragma warning disable 1591
        [EditorBrowsable(EditorBrowsableState.Never)]
        public override bool Equals(object obj) { throw new NotSupportedException(); }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public override int GetHashCode() { throw new NotSupportedException(); }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public new Type GetType() { return base.GetType(); }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public override String ToString() { throw new NotSupportedException(); }
        #pragma warning disable 1591
    }
    
    /// <summary>
    /// Verifies that a given argument value is not <c>null</c> and returns the value provided.
    /// </summary>
    /// <typeparam name="T">Type of the <paramref name="name" />.</typeparam>
    /// <param name="name">Argument name.</param>
    /// <param name="value">Argument value.</param>
    /// <exception cref="ArgumentNullException">Thrown if <paramref name="value"/> is <c>null</c>.</exception>
    /// <returns><paramref name="value"/> if it is not <c>null</c>.</returns>
    #if NET45_CONTRACTS
    [ContractArgumentValidator]
    #endif
    public static T NotNull<T>(string name, T value) 
        where T : class
    {
        if (value == null)
            throw new ArgumentNullException(name);

        #if NET45_CONTRACTS
        Contract.EndContractBlock();
        #endif

        return value;
    }

    /// <summary>
    /// Verifies that a given argument value is not <c>null</c> or empty and returns the value provided.
    /// </summary>
    /// <param name="name">Argument name.</param>
    /// <param name="value">Argument value.</param>
    /// <exception cref="ArgumentNullException">Thrown if <paramref name="value"/> is <c>null</c>.</exception>
    /// <exception cref="ArgumentException">Thrown if <paramref name="value"/> is empty.</exception>
    /// <returns><paramref name="value"/> if it is not <c>null</c> or empty.</returns>
    #if NET45_CONTRACTS
    [ContractArgumentValidator]
    #endif
    public static string NotNullOrEmpty(string name, string value) {
        Argument.NotNull(name, value);
        if (value.Length == 0)
            throw NewArgumentEmptyException(name);

        #if NET45_CONTRACTS
        Contract.EndContractBlock();
        #endif

        return value;
    }

    /// <summary>
    /// Verifies that a given argument value is not <c>null</c> or empty and returns the value provided.
    /// </summary>
    /// <param name="name">Argument name.</param>
    /// <param name="value">Argument value.</param>
    /// <exception cref="ArgumentNullException">Thrown if <paramref name="value"/> is <c>null</c>.</exception>
    /// <exception cref="ArgumentException">Thrown if <paramref name="value"/> is empty.</exception>
    /// <returns><paramref name="value"/> if it is not <c>null</c> or empty.</returns>
    #if NET45_CONTRACTS
    [ContractArgumentValidator]
    #endif
    public static T[] NotNullOrEmpty<T>(string name, T[] value) {
        Argument.NotNull(name, value);
        if (value.Length == 0)
            throw NewArgumentEmptyException(name);

        #if NET45_CONTRACTS
        Contract.EndContractBlock();
        #endif

        return value;
    }

    /// <summary>
    /// Verifies that a given argument value is not <c>null</c> or empty and returns the value provided.
    /// </summary>
    /// <param name="name">Argument name.</param>
    /// <param name="value">Argument value.</param>
    /// <exception cref="ArgumentNullException">Thrown if <paramref name="value"/> is <c>null</c>.</exception>
    /// <exception cref="ArgumentException">Thrown if <paramref name="value"/> is empty.</exception>
    /// <returns><paramref name="value"/> if it is not <c>null</c> or empty.</returns>
    #if NET45_CONTRACTS
    [ContractArgumentValidator]
    #endif
    public static TCollection NotNullOrEmpty<TCollection>(string name, TCollection value) 
        where TCollection : class, ICollection
    {
        Argument.NotNull(name, value);
        if (value.Count == 0)
            throw NewArgumentEmptyException(name);

        #if NET45_CONTRACTS
        Contract.EndContractBlock();
        #endif

        return value;
    }

    private static Exception NewArgumentEmptyException(string name) {
        return new ArgumentException("Value can not be empty.", name);
    }

    /// <summary>
    /// Casts a given argument into a given type if possible.
    /// </summary>
    /// <typeparam name="T">Type to cast <paramref name="value"/> into.</typeparam>
    /// <param name="name">Argument name.</param>
    /// <param name="value">Argument value.</param>
    /// <exception cref="ArgumentException">Thrown if <paramref name="value"/> can not be cast into type <typeparamref name="T"/>.</exception>
    /// <returns><paramref name="value"/> cast into <typeparamref name="T"/>.</returns>
    #if NET45_CONTRACTS
    [ContractArgumentValidator]
    #endif
    public static T Cast<T>(string name, object value) {
        if (!(value is T))
            throw new ArgumentException(string.Format("The value \"{0}\" isn't of type \"{1}\".", value, typeof(T)), name);

        #if NET45_CONTRACTS
        Contract.EndContractBlock();
        #endif

        return (T)value;
    }

    /// <summary>
    /// Verfies that a given argument is not null and casts it into a given type if possible.
    /// </summary>
    /// <typeparam name="T">Type to cast <paramref name="value"/> into.</typeparam>
    /// <param name="name">Argument name.</param>
    /// <param name="value">Argument value.</param>
    /// <exception cref="ArgumentNullException">Thrown if <paramref name="value"/> is <c>null</c>.</exception>
    /// <exception cref="ArgumentException">Thrown if <paramref name="value"/> can not be cast into type <typeparamref name="T"/>.</exception>
    /// <returns><paramref name="value"/> cast into <typeparamref name="T"/>.</returns>
    #if NET45_CONTRACTS
    [ContractArgumentValidator]
    #endif
    public static T NotNullAndCast<T>(string name, object value) {
        Argument.NotNull(name, value);
        return Argument.Cast<T>(name, value);
    }

    /// <summary>
    /// Verifies that a given argument value is greater than or equal to zero and returns the value provided.
    /// </summary>
    /// <param name="name">Argument name.</param>
    /// <param name="value">Argument value.</param>
    /// <exception cref="ArgumentOutOfRangeException">Thrown if <paramref name="value"/> is less than zero.</exception>
    /// <returns><paramref name="value"/> if it is greater than or equal to zero.</returns>
    #if NET45_CONTRACTS
    [ContractArgumentValidator]
    #endif
    public static int PositiveOrZero(string name, int value) {
        if (value < 0) {
            #if !PORTABLE
            throw new ArgumentOutOfRangeException(name, value, "Value must be positive or zero.");
            #else
            throw new ArgumentOutOfRangeException(name, string.Format("Value must be positive or zero.{0}Actual value was {1}.", Environment.NewLine, value));
            #endif
        }

        #if NET45_CONTRACTS
        Contract.EndContractBlock();
        #endif

        return value;
    }

    /// <summary>
    /// Verifies that a given argument value is greater than zero and returns the value provided.
    /// </summary>
    /// <param name="name">Argument name.</param>
    /// <param name="value">Argument value.</param>
    /// <exception cref="ArgumentOutOfRangeException">Thrown if <paramref name="value"/> is less than or equal to zero.</exception>
    /// <returns><paramref name="value"/> if it is greater than zero.</returns>
    #if NET45_CONTRACTS
    [ContractArgumentValidator]
    #endif
    public static int PositiveNonZero(string name, int value) {
        if (value < 0) {
            #if !PORTABLE
            throw new ArgumentOutOfRangeException(name, value, "Value must be positive and not zero.");
            #else
            throw new ArgumentOutOfRangeException(name, string.Format("Value must be positive and not zero.{0}Actual value was {1}.", Environment.NewLine, value));
            #endif
        }

        #if NET45_CONTRACTS
        Contract.EndContractBlock();
        #endif

        return value;
    }

    /// <summary>
    /// Provides an extensibility point that can be used by custom argument validation extension methods.
    /// </summary>
    /// <remarks>Always returns <c>null</c>, extension methods for <see cref="Extensible"/> should ignore <c>this</c> value.</remarks>
    public static Extensible Ex {
        get { return null; }
    }
}
