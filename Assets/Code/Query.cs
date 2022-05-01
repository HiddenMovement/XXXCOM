public delegate T Query<T>();

//****"left"/"right"
//****less verbose because no longer part of narrative?
public static class QueryExtensions
{
    //****Shorten some way?
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