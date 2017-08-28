## Overview

This project includes a single class, `Argument`, useful for argument validation/guard methods.  
You can get the NuGet package at: https://nuget.org/packages/Argument.

[![Build status](https://ci.appveyor.com/api/projects/status/m15csoxhl0hg6t13)](https://ci.appveyor.com/project/ashmind/argument)

## Example

    public MyService(OtherService other) {
       _other = Argument.NotNull("other", other);
    }

## Features

1. ReSharper annotations (Argument.ExternalAnnotations.xml).  
   Note that there is `[NotNull]` annotation on values that are being tested — that is so you do not forget to add `[NotNull]` to your arguments.
2. For .NET 4.5 Code Contracts: `[ContractArgumentValidator]`.  
   I did not have time to test if it actually works in Visual Studio though.
3. `Argument.Ex` is an extensibility point. Example:
         
        public static T Magic<T>(this Argument.Extensible _, string name, T value) {
            //..
        }

        Argument.Ex.Magic("name", value);
