﻿using System;
using System.Collections.Frozen;
using System.Linq;

namespace FastEnumUtility.Internals;



/// <summary>
/// Provides cached information about the specified <see cref="Enum"/> type.
/// </summary>
/// <typeparam name="T"><see cref="Enum"/> type</typeparam>
internal static class EnumInfo<T>
    where T : struct, Enum
{
    #region Fields
    public static readonly Type s_type;
    public static readonly Type s_underlyingType;
    public static readonly string[] s_names;
    public static readonly T[] s_values;
    public static readonly Member<T>[] s_members;
    public static readonly Member<T>[] s_orderedMembers;
    public static readonly FrozenDictionary<string, Member<T>> s_memberByName;
    public static readonly FrozenDictionary<T, Member<T>> s_memberByValue;
    public static readonly T s_minValue;
    public static readonly T s_maxValue;
    public static readonly bool s_isEmpty;
    public static readonly bool s_isFlags;
    #endregion


    #region Constructors
    static EnumInfo()
    {
        s_type = typeof(T);
        s_underlyingType = Enum.GetUnderlyingType(s_type);
        s_names = Enum.GetNames(s_type);
        s_values = (T[])Enum.GetValues(s_type);
        s_members = s_names.Select(static x => new Member<T>(x)).ToArray();
        s_orderedMembers = s_members.OrderBy(static x => x.Value).ToArray();
        s_memberByName = s_members.ToFrozenDictionary(static x => x.Name);
        s_memberByValue
            = s_orderedMembers
            .DistinctBy(static x => x.Value)
            .ToFrozenDictionary(static x => x.Value);
        s_minValue = s_values.DefaultIfEmpty().Min();
        s_maxValue = s_values.DefaultIfEmpty().Max();
        s_isEmpty = s_values.Length is 0;
        s_isFlags = s_type.IsDefined(typeof(FlagsAttribute), true);
    }
    #endregion
}
