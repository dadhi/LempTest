namespace AnotherLib
{
    public class A
    {
        
    }

    public class B
    {
        
    }
}

namespace AnotherLib.Experimental
{
    public class X
    {
        public A A;
        public B B;
        public X(A a, B b) 
        {
            A = a;
            B = b;
        }
    }
}