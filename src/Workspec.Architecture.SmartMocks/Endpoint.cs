using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;

namespace Workspec.Architecture.SmartMocks
{
    public class Endpoint : IHideDefautMethods, IEquatable<Endpoint>
    {
        private string _currentRoute = string.Empty;
        private string _currentMethod = HttpMethod.Get.Method;

        public void Get(string route) => _currentRoute = route;

        public void Put(string route)
        {
            _currentRoute = route;
            _currentMethod = HttpMethod.Put.Method;
        }

        public void Post(string route)
        {
            _currentRoute = route;
            _currentMethod = HttpMethod.Post.Method;
        }

        public void Delete(string route)
        {
            _currentRoute = route;
            _currentMethod = HttpMethod.Delete.Method;
        }

        internal string GetCurrentRoute() => _currentRoute;
        internal string GetCurrentMethod() => _currentMethod;

        #region Equality Members

        public override bool Equals(object? obj)=> obj is Endpoint other && Equals(other);

        public bool Equals(Endpoint? other)
        {
            if (other is null)
                return false;

            // Consider endpoints equal if they have the same route and HTTP method.
            // String comparisons are case-insensitive.
            return string.Equals(_currentRoute, other._currentRoute, StringComparison.OrdinalIgnoreCase) &&
                   string.Equals(_currentMethod, other._currentMethod, StringComparison.OrdinalIgnoreCase);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                // Use case-insensitive hash codes.
                int hash = 17;
                hash = hash * 23 + (_currentRoute?.ToLowerInvariant().GetHashCode() ?? 0);
                hash = hash * 23 + (_currentMethod?.ToLowerInvariant().GetHashCode() ?? 0);
                return hash;
            }
        }

        public static bool operator ==(Endpoint left, Endpoint right)
        {
            if (ReferenceEquals(left, right))
                return true;
            if (left is null || right is null)
                return false;
            return left.Equals(right);
        }

        public static bool operator !=(Endpoint left, Endpoint right)
        {
            return !(left == right);
        }

        #endregion
    }

    internal static class EndpointExtensions
    {
        internal static void MapEndpointStep(this RouteGroupBuilder routeBuilder, Endpoint endpoint, Delegate handler)
        {
            routeBuilder.MapMethods(endpoint.GetCurrentRoute(), [endpoint.GetCurrentMethod()], handler);
        }
    }
}
