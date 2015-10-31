using System;
using System.Collections.Generic;
using System.Diagnostics;

#pragma warning disable 1591
// ReSharper disable UnusedMember.Global
// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global
// ReSharper disable IntroduceOptionalParameters.Global
// ReSharper disable MemberCanBeProtected.Global
// ReSharper disable InconsistentNaming

// ReSharper disable once CheckNamespace
namespace JetBrains.Annotations {
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Parameter | AttributeTargets.Property | AttributeTargets.Delegate | AttributeTargets.Field | AttributeTargets.Event)]
    internal sealed class CanBeNullAttribute : Attribute { }

    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Parameter | AttributeTargets.Property | AttributeTargets.Delegate | AttributeTargets.Field | AttributeTargets.Event)]
    internal sealed class NotNullAttribute : Attribute { }

    [AttributeUsage(AttributeTargets.Parameter)]
    internal sealed class InvokerParameterNameAttribute : Attribute { }

    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    internal sealed class ContractAnnotationAttribute : Attribute {
        public ContractAnnotationAttribute([NotNull] string contract)
        : this(contract, false) { }

        public ContractAnnotationAttribute([NotNull] string contract, bool forceFullStates) {
            Contract = contract;
            ForceFullStates = forceFullStates;
        }

        public string Contract { get; private set; }
        public bool ForceFullStates { get; private set; }
    }

    [AttributeUsage(AttributeTargets.All)]
    [Conditional("JETBRAINS_ANNOTATIONS")]
    internal sealed class UsedImplicitlyAttribute : Attribute {
        public UsedImplicitlyAttribute()
            : this(ImplicitUseKindFlags.Default, ImplicitUseTargetFlags.Default) { }

        public UsedImplicitlyAttribute(ImplicitUseKindFlags useKindFlags)
            : this(useKindFlags, ImplicitUseTargetFlags.Default) { }

        public UsedImplicitlyAttribute(ImplicitUseTargetFlags targetFlags)
            : this(ImplicitUseKindFlags.Default, targetFlags) { }

        public UsedImplicitlyAttribute(ImplicitUseKindFlags useKindFlags, ImplicitUseTargetFlags targetFlags) {
            UseKindFlags = useKindFlags;
            TargetFlags = targetFlags;
        }

        public ImplicitUseKindFlags UseKindFlags { get; private set; }
        public ImplicitUseTargetFlags TargetFlags { get; private set; }
    }

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.GenericParameter)]
    [Conditional("JETBRAINS_ANNOTATIONS")]
    internal sealed class MeansImplicitUseAttribute : Attribute {
        public MeansImplicitUseAttribute() : this(ImplicitUseKindFlags.Default, ImplicitUseTargetFlags.Default) { }

        public MeansImplicitUseAttribute(ImplicitUseKindFlags useKindFlags)
            : this(useKindFlags, ImplicitUseTargetFlags.Default) { }

        public MeansImplicitUseAttribute(ImplicitUseTargetFlags targetFlags)
            : this(ImplicitUseKindFlags.Default, targetFlags) { }

        public MeansImplicitUseAttribute(ImplicitUseKindFlags useKindFlags, ImplicitUseTargetFlags targetFlags) {
            UseKindFlags = useKindFlags;
            TargetFlags = targetFlags;
        }

        [UsedImplicitly] public ImplicitUseKindFlags UseKindFlags { get; private set; }
        [UsedImplicitly] public ImplicitUseTargetFlags TargetFlags { get; private set; }
    }

    [Flags]
    internal enum ImplicitUseKindFlags {
        Default = Access | Assign | InstantiatedWithFixedConstructorSignature,
        Access = 1,
        Assign = 2,
        InstantiatedWithFixedConstructorSignature = 4,
        InstantiatedNoFixedConstructorSignature = 8,
    }

    [Flags]
    internal enum ImplicitUseTargetFlags {
        Default = Itself,
        Itself = 1,
        Members = 2,
        WithMembers = Itself | Members
    }

    [MeansImplicitUse]
    [Conditional("JETBRAINS_ANNOTATIONS")]
    internal sealed class PublicAPIAttribute : Attribute {
        public PublicAPIAttribute() { }
        public PublicAPIAttribute([NotNull] string comment) {
            Comment = comment;
        }

        public string Comment { get; private set; }
    }

    [AttributeUsage(AttributeTargets.Parameter)]
    internal sealed class InstantHandleAttribute : Attribute { }

    [AttributeUsage(AttributeTargets.Method)]
    internal sealed class AssertionMethodAttribute : Attribute { }

    [AttributeUsage(AttributeTargets.Parameter)]
    internal sealed class AssertionConditionAttribute : Attribute {
        public AssertionConditionAttribute(AssertionConditionType conditionType) {
            ConditionType = conditionType;
        }

        public AssertionConditionType ConditionType { get; private set; }
    }

    internal enum AssertionConditionType {
        IS_TRUE = 0,
        IS_FALSE = 1,
        IS_NULL = 2,
        IS_NOT_NULL = 3,
    }

    [AttributeUsage(AttributeTargets.Parameter)]
    public sealed class NoEnumerationAttribute : Attribute { }
}