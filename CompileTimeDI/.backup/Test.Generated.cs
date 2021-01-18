using System.Text;
using System;
using Loyc.Syntax;
using Loyc.Ecs;
using Loyc;

string Ack(string s) => s + "!";

string GetHello()
{
    var names = new[] { 
        "Holly", "Molly"
    };
    var b = new StringBuilder();
    // This works!
    // b << "Hello " << (names, Ack) << " here"; // todo: #121- does not work
    _numerror("Syntax error in expression at 'return'; possibly missing semicolon");
}
using System.Text;

static class X
{
    null;

    public static string Ack(string s) => s + "!";
    public static steing SayHello(StringBuilder b)
    {
        var names = new[] { 
            "Holly", "Molly"
        };
        var its = names;
        for (var i = 0; i < its.Count; ++i)
            (i > 0 ? b.Append("Hello ").Append(", ") : b.Append("Hello ")).Append(Ack(its[i]));
        b.Append("Hello ").Append(" here");
        return b.ToString();
    }
}