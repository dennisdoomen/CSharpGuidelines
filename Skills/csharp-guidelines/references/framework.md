# Framework Usage (AV2200) — Detailed Reference

> **Severity legend:** **Must** (1) · **Should** (2) · **May** (3)

---

## AV2202 — Prefer idiomatic C# over .NET Framework APIs [Must]

C#'s language syntax makes code more concise. The abstractions make later refactorings easier and sometimes allow for extra optimizations.

```csharp
// Prefer tuple syntax over ValueTuple<...>
(string, int) tuple = ("", 1);
// NOT: ValueTuple<string, int> tuple = new ValueTuple<string, int>("", 1);

// Prefer nullable shorthand
DateTime? startDate;
// NOT: Nullable<DateTime> startDate;

// Prefer is null pattern
if (startDate is null) ...
// NOT: if (startDate == null) ...

// Prefer is not null pattern  
if (startDate is not null) ...
// NOT: if (startDate.HasValue) ...

// Prefer direct comparison on nullable
if (startDate > DateTime.Now) ...
// NOT: if (startDate.HasValue && startDate.Value > DateTime.Now) ...

// Prefer collection expression (C# 12+)
List<string> items = [];
// NOT: List<string> items = new List<string>();

// Prefer null-coalescing assignment (C# 14)
list ??= [];
// NOT: if (list == null) list = [];
```

---

## AV2210 — Build with the highest warning level [Must]

Configure the development environment to use the highest available warning level for the C# compiler, and enable the option **Treat warnings as errors**. This allows the compiler to enforce the highest possible code quality.

---

## AV2220 — Avoid LINQ query syntax for simple expressions [May]

Rather than:

```csharp
var query = from item in items where item.Length > 0 select item;
```

prefer the use of extension methods from the `System.Linq` namespace:

```csharp
var query = items.Where(item => item.Length > 0);
```

The second example is a bit less convoluted.

---

## AV2225 — Use deconstruction to simplify variable assignments [May]

C# supports deconstructing tuples, records, and any type that defines a `Deconstruct` method. Use this to avoid intermediate variables and make the intent of your code clearer.

```csharp
// Instead of:
public record Point(int X, int Y);
Point point = GetOrigin();
int x = point.X;
int y = point.Y;

// Write:
(int x, int y) = GetOrigin();
```

Deconstruct arrays and collections with pattern matching to avoid index-based access:

```csharp
if (items is [int first, int second, ..])
{
    // use first and second directly
}
```

Deconstruction also works in `foreach` loops:

```csharp
foreach ((int key, int value) in dictionary)
{
    Console.WriteLine($"{key}: {value}");
}
```

---

## AV2230 — Only use the `dynamic` keyword when talking to a dynamic object [Must]

The `dynamic` keyword has been introduced for interop with languages where properties and methods can appear and disappear at runtime. Using it can introduce a serious performance bottleneck, because various compile-time checks (such as overload resolution) need to happen at runtime, again and again on each invocation. You'll get better performance using cached reflection lookups, `Activator.CreateInstance()` or pre-compiled expressions.

While using `dynamic` may improve code readability, try to avoid it in library code (especially in hot code paths). However, keep things in perspective: we're talking microseconds here, so perhaps you'll gain more by optimizing your SQL statements first.
