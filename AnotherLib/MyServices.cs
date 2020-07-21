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
        public Y Y;
        public X(A a, B b, Y y) 
        {
            A = a;
            B = b;
            Y = y;
        }
    }

    public class Y {}
}