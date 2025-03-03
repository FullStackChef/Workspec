using System.ComponentModel;

namespace Workspec.Architecture.SmartMocks
{
    internal interface IHideDefautMethods
    {

        [EditorBrowsable(EditorBrowsableState.Never)]
        bool Equals(object? obj);
        [EditorBrowsable(EditorBrowsableState.Never)]
        int GetHashCode();
        [EditorBrowsable(EditorBrowsableState.Never)]
        public Type GetType();
        [EditorBrowsable(EditorBrowsableState.Never)]
        string? ToString();
    }
}
