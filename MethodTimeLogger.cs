using System;
using System.Diagnostics;
using System.Reflection;

public static class MethodTimeLogger
{
    public static void Log(MethodBase methodBase, long milliseconds, string? message = null)
    {
        var name = methodBase.DeclaringType?.FullName + "." + methodBase.Name;
        if (!string.IsNullOrEmpty(message))
        {
            Console.WriteLine($"{name} took {milliseconds}ms. {message}");
        }
        else
        {
            Console.WriteLine($"{name} took {milliseconds}ms.");
        }
    }
}
