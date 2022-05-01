public delegate T Query<T>();

public static class QueryExtensions
{
    public static Query<bool> IsEqualTo(this Query<int> a, Query<int> b)
    { return () => a() == b(); }
    public static Query<bool> IsEqualTo(this Query<int> a, int b)
    { return a.IsEqualTo(() => b); }

    public static Query<bool> IsGreaterThan(this Query<int> a, Query<int> b)
    { return () => a() > b(); }
    public static Query<bool> IsGreaterThan(this Query<int> a, int b)
    { return a.IsGreaterThan(() => b); }

    public static Query<bool> IsGreaterOrEqualTo(this Query<int> a, Query<int> b)
    { return () => a() >= b(); }
    public static Query<bool> IsGreaterOrEqualTo(this Query<int> a, int b)
    { return a.IsGreaterOrEqualTo(() => b); }

    public static Query<bool> IsLessThan(this Query<int> a, Query<int> b)
    { return () => a() < b(); }
    public static Query<bool> IsLessThan(this Query<int> a, int b)
    { return a.IsLessThan(() => b); }

    public static Query<bool> IsLessOrEqualTo(this Query<int> a, Query<int> b)
    { return () => a() <= b(); }
    public static Query<bool> IsLessOrEqualTo(this Query<int> a, int b)
    { return a.IsLessOrEqualTo(() => b); }
}