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

    rawPrecompute(LNode.Call(CodeSymbols.Fn, LNode.List(LNode.Id(CodeSymbols.Void), LNode.Id((Symbol) "Z"), LNode.Call(CodeSymbols.AltList), LNode.Call(CodeSymbols.Braces).SetStyle(NodeStyle.StatementBlock))))
}

	//includeFile("ServiceRegistrations.ecs.include")