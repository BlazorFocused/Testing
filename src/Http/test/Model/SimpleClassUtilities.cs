// -------------------------------------------------------
// Copyright (c) BlazorFocused All rights reserved.
// Licensed under the MIT License
// -------------------------------------------------------

using Bogus;

namespace BlazorFocused.Testing.Http.Test.Model;

public class SimpleClassUtilities
{
    public static SimpleClass GetRandomSimpleClass() => new Faker<SimpleClass>()
            .RuleForType(typeof(string), fake => fake.Random.AlphaNumeric(GetRandomInteger()))
            .Generate();

    public static SimpleClass GetStaticSimpleClass(string input) => new Faker<SimpleClass>()
            .RuleForType(typeof(string), _ => input)
            .Generate();

    public static int GetRandomInteger() => new Random().Next(5, 20);
}
