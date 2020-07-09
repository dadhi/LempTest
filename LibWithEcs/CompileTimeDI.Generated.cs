using System;
using System.Collections.Generic;
using System.Linq.Expressions;
namespace LibWithEcs
{
    public partial class CompileTimeDI
    {
        public Dictionary<Type, Expression<Func<object>>> Factories;
    
        public CompileTimeDI()
        {
            Factories = new Dictionary<Type, Expression<Func<object>>>();
        }
    }
}
namespace LibWithEcs
{
    partial class CompileTimeDI
    {
        public static CompileTimeDI Default = new CompileTimeDI();
    }
}