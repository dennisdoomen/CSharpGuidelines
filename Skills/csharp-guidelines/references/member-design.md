# Member Design (AV1100) — Detailed Reference

## AV1100 — Allow properties to be set in any order [Must]

Properties should be stateless with respect to other properties, i.e. there should not be a difference between first setting property `DataSource` and then `DataMember` or vice-versa.

## AV1105 — Use a method instead of a property [May]

Use a method instead of a property when:
- The work is more expensive than setting a field value.
- It represents a conversion such as the `Object.ToString` method.
- It returns a different result each time it is called, even if the arguments didn't change (e.g. `NewGuid`).
- The operation causes a side effect such as changing some internal state not directly related to the property (which violates the [Command Query Separation](http://martinfowler.com/bliki/CommandQuerySeparation.html) principle).

**Exception:** Populating an internal cache or implementing [lazy-loading](http://www.martinfowler.com/eaaCatalog/lazyLoad.html) is a good exception.

## AV1110 — Don't use mutually exclusive properties [Must]

Having properties that cannot be used at the same time typically signals a type that represents two conflicting concepts. Even though those concepts may share some of their behavior and states, they obviously have different rules that do not cooperate. This violation is often seen in domain models and introduces all kinds of conditional logic related to those conflicting rules, causing a ripple effect that significantly increases the maintenance burden.

## AV1115 — A property, method or local function should do only one thing [May]

Similarly to AV1000 (class single responsibility), a method body should have a single responsibility.

## AV1125 — Don't hide dependencies behind static members [Should]

A well-known example is `HttpContext.Current` from classic ASP.NET. Modern .NET still has plenty of cases where static members expose global or environment-dependent state, such as `Environment.MachineName`, `DateTime.UtcNow`, `Environment.GetEnvironmentVariable`, and `File.Exists`. These are problematic because:
- They hide implicit dependencies, making code harder to understand.
- They make unit testing difficult, especially running tests in parallel.

Instead, make dependencies explicit by injecting them through a constructor or method parameter, or by injecting an abstraction (e.g., `TimeProvider` instead of calling `DateTime.UtcNow` directly).

## AV1130 — Return interfaces to unchangeable collections [Should]

You generally don't want callers to be able to change an internal collection, so don't return arrays, lists or other mutable collection classes directly. Instead, return an `IEnumerable<T>`, `IAsyncEnumerable<T>`, `IReadOnlyCollection<T>`, `IReadOnlyList<T>`, `IReadOnlySet<T>` or `IReadOnlyDictionary<TKey, TValue>`.

Be aware that `IEnumerable<T>` is often perceived as lazy-evaluated. If your collection is already materialized, consider returning `IReadOnlyCollection<T>` or `IReadOnlyList<T>` to make the intent clear.

**Exception:** Immutable collections such as `ImmutableArray<T>`, `ImmutableList<T>`, `ImmutableDictionary<TKey, TValue>`, `FrozenSet<T>` and `FrozenDictionary<TKey, TValue>` prevent modifications from the outside and are thus allowed.

## AV1135 — Properties, arguments and return values representing strings, collections or tasks should never be `null` [Must]

Returning `null` can be unexpected by the caller. Always return an empty collection or an empty string instead of a `null` reference. When your member returns `Task` or `Task<T>`, return `Task.CompletedTask` or `Task.FromResult()`. This also prevents cluttering your code base with additional checks for `null`, or even worse, `string.IsNullOrEmpty()`.

## AV1137 — Define parameters as specific and narrow as possible [Should]

If your method or local function needs a specific piece of data, define parameters as specific as that and don't take a container object instead. For instance, consider a method that needs a connection string that is exposed through a central `IConfiguration` interface. Rather than taking a dependency on the entire configuration, just define a parameter for the connection string. This not only prevents unnecessary coupling, it also improves maintainability in the long run.

**Note:** An easy trick to remember this guideline is *Don't ship the truck if you only need a package*.

## AV1140 — Consider creating domain-specific types rather than using primitives [May]

Instead of using strings, integers and decimals for representing domain-specific types such as an ISBN number, an email address or amount of money, consider creating dedicated value objects that wrap both the data and the validation rules that apply to it. By doing this, you prevent ending up having multiple implementations of the same business rules, which both improves maintainability and prevents bugs.

## AV1145 — Use extension members to add behavior without modifying the original type [May]

C# 14 introduced extension members (also called extension blocks), which allow you to add properties, methods and other members to existing types without modifying the original type definition.

```csharp
// C# 14 extension block
extension(Order order)
{
    public bool IsOverdue => order.DueDate < DateTimeOffset.UtcNow && !order.IsCompleted;

    public void MarkAsShipped(DateTimeOffset shippedAt)
    {
        order.Status = OrderStatus.Shipped;
        order.ShippedAt = shippedAt;
    }
}
```

Use extension members when:
- You want to add behavior to a type you don't own (e.g. from a third-party library or the BCL).
- You want to keep domain logic close to the type it operates on without polluting the type's own definition.
- You are adding utility methods that are only relevant in a specific layer or context.

Don't use extension members if you can add that method directly to the type.

## AV1150 — Avoid local functions [May]

Local functions (methods defined inside another method) can be useful in rare cases, but they often obscure a method's structure and make unit testing harder. Their usage often indicates a lack of proper decomposition.

Local functions can be a good fit for:
- Trivial private recursion.
- Iterator implementations (validate inputs upfront in the containing method, instead of during deferred execution).
- Extracting to a method would require passing many parameters that are otherwise captured.

## AV1155 — Use the `field` keyword in auto-properties when additional logic is needed [May]

C# 14 introduced the `field` keyword, which provides access to the underlying storage of a property without the need to declare a private field. This eliminates boilerplate while still allowing validation or transformation logic.

```csharp
// Before C# 14: required a manual backing field
private int maxLength;
public int MaxLength
{
    get => maxLength;
    set
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(value, 0);
        maxLength = value;
    }
}

// With C# 14: use the field keyword
public int MaxLength
{
    get;
    set
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(value, 0);
        field = value;
    }
}
```

Use the `field` keyword when you need getter or setter logic that goes beyond a simple assignment, but don't want to sacrifice the clarity of an auto-property.

Don't use `field` when the backing field itself carries meaning (e.g. needs its own name or XML documentation).
