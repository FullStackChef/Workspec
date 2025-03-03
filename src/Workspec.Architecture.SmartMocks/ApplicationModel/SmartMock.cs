using Moq;
using Moq.Language.Flow;
using System.Collections.Concurrent;
using System.Linq.Expressions;

namespace Workspec.Architecture.SmartMocks.ApplicationModel;


public class SmartMock<T> : Mock<T> where T : class
{
    // We map propertyName -> Delegate (where the delegate is actually Func<TProp, string>)
    private readonly ConcurrentDictionary<string, Delegate> _descriptions = [];

    /// <summary>
    /// Associates a description delegate with a property (or method) on this mock.
    /// E.g.:
    ///     smartMock.Describe(s => s.Authenticated, val => val ? "the user is authenticated" : "the user is not");
    /// </summary>
    public ISetupGetter<T, TProp> Describe<TProp>(
        Expression<Func<T, TProp>> memberExpression,
        Func<TProp, string> descriptionBuilder)
    {
        var memberName = GetMemberName(memberExpression);
        _descriptions[memberName] = descriptionBuilder;
        return SetupGet(memberExpression);
    }

    /// <summary>
    /// Retrieves the description for a given property expression + actual value.
    /// If we previously set up a description, we invoke that delegate with 'val'.
    /// Otherwise, we default to val.ToString() or some fallback.
    /// E.g.:
    ///     var desc = smartMock.DescriptionFor(s => s.Authenticated, true);
    /// </summary>
    public string DescriptionFor<TProp>(
        Expression<Func<T, TProp>> memberExpression,
        TProp val)
    {
        var memberName = GetMemberName(memberExpression);

        if (_descriptions.TryGetValue(memberName, out var del))
        {
            // 'del' is actually a Func<TProp, string>
            if (del is Func<TProp, string> typedDel)
            {
                return typedDel(val);
            }
        }

        // If we don't find any stored delegate, fall back
        return val?.ToString() ?? string.Empty;
    }

    /// <summary>
    /// Helper to extract the property or method name from the expression.
    /// </summary>
    private static string GetMemberName<TProp>(Expression<Func<T, TProp>> expr)
    {
        if (expr.Body is MemberExpression member)
        {
            return member.Member.Name;
        }
        throw new InvalidOperationException("Expression is not a property or method access.");
    }
}
