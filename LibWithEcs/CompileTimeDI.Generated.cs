using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace LibWithEcs
{
    public partial class CompileTimeDI
    {
        public Dictionary<Type, Expression<Func<object>>> Factories;
    
        public CompileTimeDI() { }	// ecs construct fot test
    }
}