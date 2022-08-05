namespace Poker.Extensions
{
    internal static class IEnumerable
    {
        internal static Dictionary<TKey, TValue> AssociateWith<TKey, TValue>(this IEnumerable<TKey> keys, Func<TValue> valueMapper) where TKey : notnull
        {
            var destination = new Dictionary<TKey, TValue>();
            foreach (var key in keys) { destination[key] = valueMapper(); }
            return destination;
        }
    }
}
