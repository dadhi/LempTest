using System;
using System.Collections.Generic;
using System.Linq.Expressions;
namespace LibWithEcs
{
    public class A { }	// todo: @test just for test here, move to another assembly 
    public interface IResolver
    {
        object Resolve(Type serviceType);
    }

    public partial class CompileTimeDI : IResolver
    {
        public Dictionary<Type, Expression<Func<IResolver, object>>> Factories;
    
        public CompileTimeDI()
        {
            Factories = new Dictionary<Type, Expression<Func<IResolver, object>>>();
        }
    
        public void Register<T>(Expression<Func<IResolver, object>> factory)
        {
            Factories.Add(typeof(T), factory);
        }
    
        public object Resolve(Type serviceType) => null;
    }
}
namespace LibWithEcs
{
    partial class CompileTimeDI
    {
        object Get_A() => "new A()";
    }
}