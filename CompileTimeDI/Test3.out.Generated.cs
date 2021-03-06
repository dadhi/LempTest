﻿using System;

static class X
{
    internal static bool TryGetUsedInstance(this IResolverContext r, int serviceTypeHash, Type serviceType, out object instance)
    {
        instance = null;
        return r.CurrentScope ? .TryGetUsedInstance(r, serviceTypeHash, serviceType, out instance) == true || r.SingletonScope.TryGetUsedInstance(r, serviceTypeHash, serviceType, out instance) : @__empty__;
        
    }
}