# Maintainability (AV1500) — Detailed Reference

> **Severity legend:** **Must** (1) · **Should** (2) · **May** (3)

---

## AV1500 — Methods should not exceed 15 statements [Must]

A method that requires more than 15 statements is simply doing too much or has too many responsibilities. It also requires the human mind to analyze the exact statements to understand what the code is doing. Break it down into multiple small and focused methods with self-explaining names, but make sure the high-level algorithm is still clear.

Also ensure that each method operates at a **single level of abstraction**. Mixing high-level functional calls (the "what") with low-level implementation details (the "how") in the same method makes it harder to understand the intent of the code.

```csharp
// Avoid: mixing abstraction levels in the same method
void ProcessOrder(Order order)
{
    if (IsPremiumCustomer(order.CustomerId))  // functional; the "what"
    {
        ApplyDiscount(order);
    }

    if (Regex.IsMatch(order.Email.ToUpper().Trim(), @"^[^@]+@[^@]+\.[^@]+$"))  // technical; the "how"
    {
        SendConfirmation(order.Email);
    }
}

// Better: extract the technical detail behind an abstraction
void ProcessOrder(Order order)
{
    if (IsPremiumCustomer(order.CustomerId))
    {
        ApplyDiscount(order);
    }

    if (IsValidEmail(order.Email))
    {
        SendConfirmation(order.Email);
    }
}
```

---

## AV1501 — Make all members `private` and types `internal sealed` by default [Must]

To make a more conscious decision on which members to make available to other classes, first restrict the scope as much as possible. Then carefully decide what to expose as a public member or type.

---

## AV1502 — Avoid conditions with double negatives [Should]

Although a property like `customer.HasNoOrders` makes sense, avoid using it in a negative condition like this:

```csharp
bool hasOrders = !customer.HasNoOrders;
```

Double negatives are more difficult to grasp than simple expressions, and people tend to read over the double negative easily.

---

## AV1505 — Name assemblies after their contained namespace [May]

All DLLs should be named according to the pattern *Company*.*Component*.dll. As an example, consider a group of classes organized under the namespace `AvivaSolutions.Web.Binding`. According to this guideline, that assembly should be called `AvivaSolutions.Web.Binding.dll`.

**Exception:** If you decide to combine classes from multiple unrelated namespaces into one assembly, consider suffixing the assembly name with `Core`, but do not use that suffix in the namespaces.

---

## AV1506 — Name a source file to the type it contains [May]

Use Pascal casing to name the file and don't use underscores. Don't include the number of generic type parameters in the file name.

---

## AV1507 — Limit the contents of a source code file to one type [May]

**Exception:** Nested types should be part of the same file.

**Exception:** Types that only differ by their number of generic type parameters should be part of the same file.

---

## AV1508 — Name a source file to the logical function of the partial type [May]

When using partial types and allocating a part per file, name each file after the logical part that part plays. For example:

```csharp
// In MyClass.cs
public partial class MyClass { }

// In MyClass.Designer.cs
public partial class MyClass { }
```

---

## AV1510 — Use `using` statements instead of fully qualified type names [May]

Limit usage of fully qualified type names to prevent name clashing. If you do need to prevent name clashing, use a `using` directive to assign an alias:

```csharp
using Label = System.Web.UI.WebControls.Label;
```

---

## AV1515 — Don't use "magic" numbers [Must]

Don't use literal values, either numeric or strings, in your code, other than to define symbolic constants. For example:

```csharp
public class Whatever
{
    public static readonly Color PapayaWhip = new Color(0xFFEFD5);
    public const int MaxNumberOfWheels = 18;
    public const byte ReadCreateOverwriteMask = 0b0010_1100;
}
```

Strings intended for logging or tracing are exempt from this rule. Literals are allowed when their meaning is clear from the context and not subject to future changes:

```csharp
mean = (a + b) / 2; // okay
WaitMilliseconds(waitTimeInSeconds * 1000); // clear enough
```

---

## AV1520 — Only use `var` when the type is evident [Must]

Use `var` for anonymous types (typically resulting from a LINQ query), or if the type is evident. Never use `var` for built-in types.

```csharp
// Projection into anonymous type — use var.
var largeOrders =
    from order in dbContext.Orders
    where order.Items.Count > 10 && order.TotalAmount > 1000
    select new { order.Id, order.TotalAmount };

// Built-in types — don't use var.
bool isValid = true;
string phoneNumber = "(unavailable)";
uint pageSize = Math.Max(itemCount, MaxPageSize);

// Types are evident — use var.
var customer = new Customer();
var invoice = Invoice.Create(customer.Id);
var subscribers = new List<Subscriber>();

// All other cases — don't use var.
IQueryable<Order> recentOrders = ApplyFilter(order => order.CreatedAt > DateTime.Now.AddDays(-30));
IDictionary<Category, Product> productsPerCategory =
    shoppingBasket.Products.ToDictionary(product => product.Category);
```

---

## AV1521 — Declare and initialize variables as late as possible [Should]

Avoid the C and Visual Basic styles where all variables have to be defined at the beginning of a block. Instead define and initialize each variable at the point where it is needed.

---

## AV1522 — Assign each variable in a separate statement [Must]

Don't use confusing constructs like:

```csharp
var result = someField = GetSomeMethod();
```

**Exception:** Multiple assignments per statement are allowed using `out` variables, `is`-patterns or tuple deconstruction:

```csharp
bool success = int.TryParse(text, out int result);

if ((items[0] is string text) || (items[1] is Action action)) { }

(string name, string value) = SplitNameValuePair(text);
```

---

## AV1523 — Favor object and collection initializers over separate statements [Should]

Instead of:

```csharp
var startInfo = new ProcessStartInfo("myapp.exe");
startInfo.StandardOutput = Console.Output;
startInfo.UseShellExecute = true;

var countries = new List<string>();
countries.Add("Netherlands");
countries.Add("United States");
```

Use object and collection initializers:

```csharp
var startInfo = new ProcessStartInfo("myapp.exe")
{
    StandardOutput = Console.Output,
    UseShellExecute = true
};

var countries = new List<string> { "Netherlands", "United States" };
```

---

## AV1525 — Don't make explicit comparisons to `true` or `false` [Must]

```csharp
while (condition == false) // wrong; bad style
while (condition != true)  // also wrong
while (condition)          // OK
```

---

## AV1530 — Don't change a loop variable inside a `for` loop [Should]

Updating the loop variable within the loop body is generally considered confusing, even more so if the loop variable is modified in more than one place.

```csharp
for (int index = 0; index < 10; ++index)
{
    if (someCondition)
    {
        index = 11; // Wrong! Use 'break' or 'continue' instead.
    }
}
```

---

## AV1532 — Avoid nested loops [Should]

A method that nests loops is more difficult to understand than one with only a single loop. In fact, in most cases nested loops can be replaced with a much simpler LINQ query that uses the `from` keyword twice or more to *join* the data.

---

## AV1535 — Always add a block after keywords `if`, `else`, `do`, `while`, `for`, `foreach` and `case` [Should]

```csharp
// Confusing
if (isActive) if (isVisible) Foo(); else Bar(); // which 'if' goes with the 'else'?

// Right
if (isActive)
{
    if (isVisible)
    {
        Foo();
    }
    else
    {
        Bar();
    }
}
```

---

## AV1536 — Always add a `default` block after the last `case` in a `switch` statement [Must]

Add a descriptive comment if the `default` block is supposed to be empty. If that block is not supposed to be reached throw an `InvalidOperationException` to detect future changes that may fall through the existing cases.

```csharp
void Foo(string answer)
{
    switch (answer)
    {
        case "no":
        {
            Console.WriteLine("You answered with No");
            break;
        }
        case "yes":
        {
            Console.WriteLine("You answered with Yes");
            break;
        }
        default:
        {
            // Not supposed to end up here.
            throw new InvalidOperationException("Unexpected answer " + answer);
        }
    }
}
```

---

## AV1537 — Finish every `if`-`else`-`if` statement with an `else` clause [Should]

Always include an explicit `else` at the end of an `if`-`else`-`if` chain to make it clear what happens when no condition is met.

---

## AV1540 — Be reluctant with multiple `return` statements [Should]

One entry, one exit is a sound principle and keeps control flow readable. However, if the method body is very small and complies with AV1500 then multiple return statements may actually improve readability over some central boolean flag that is updated at various points.

---

## AV1545 — Don't use an `if`-`else` construct instead of a simple (conditional) assignment [Should]

Express your intentions directly:

```csharp
// Instead of:
bool isPositive;
if (value > 0) isPositive = true;
else           isPositive = false;

// Write:
bool isPositive = value > 0;

// Instead of:
return value > 0 ? "positive" : "negative";

// Instead of null-coalesce:
return offset ?? -1;

// Instead of:
firstJobStartedAt ??= DateTime.UtcNow;
```

---

## AV1546 — Prefer interpolated strings over concatenation or `string.Format` [Must]

Since .NET 6, interpolated strings are optimized at compile-time. In .NET 8 and later they use `DefaultInterpolatedStringHandler`, making them the fastest option in most scenarios.

```csharp
// GOOD
string result = $"Welcome, {firstName} {lastName}!";

// BAD
string result = string.Format("Welcome, {0} {1}!", firstName, lastName);

// BAD
string result = "Welcome, " + firstName + " " + lastName + "!";
```

For building strings in loops or with many parts, consider using `StringBuilder` or raw string literals (C# 11+) when appropriate.

---

## AV1547 — Encapsulate complex expressions in a property, method or local function [Must]

Replace complex conditionals with a clearly named method:

```csharp
// Before
if (member.HidesBaseClassMember && member.NodeType != NodeType.InstanceInitializer)
{ /* ... */ }

// After
if (NonConstructorMemberUsesNewKeyword(member))
{ /* ... */ }

private bool NonConstructorMemberUsesNewKeyword(Member member) =>
    member.HidesBaseClassMember && member.NodeType != NodeType.InstanceInitializer;
```

---

## AV1551 — Call the most overloaded method from other overloads [Should]

Only applies to overloads intended to provide optional arguments. Chain shorter overloads to the one with the most parameters:

```csharp
public class MyString
{
    public int IndexOf(string phrase) => IndexOf(phrase, 0);
    public int IndexOf(string phrase, int startIndex) => IndexOf(phrase, startIndex, someText.Length - startIndex);
    public virtual int IndexOf(string phrase, int startIndex, int count) => someText.IndexOf(phrase, startIndex, count);
}
```

---

## AV1553 — Only use optional parameters to replace overloads [Must]

The only valid reason for using optional parameters is to replace the overload chain from AV1551 with a single method. Since strings, collections, and tasks should never be `null` (per AV1135), if you have an optional parameter of these types with default value `null`, use overloaded methods instead.

---

## AV1555 — Avoid using named arguments [Must]

If you need named arguments to improve the readability of the call to a method, that method is probably doing too much and should be refactored.

**Exception:** Use named arguments for `bool` parameters on methods you don't control:

```csharp
object[] myAttributes = type.GetCustomAttributes(typeof(MyAttribute), inherit: false);
```

---

## AV1561 — Don't declare signatures with more than 3 parameters [Must]

To keep constructors, methods, delegates and local functions small and focused, do not use more than three parameters. Do not use tuple parameters. Do not return tuples with more than two elements.

If you need more parameters, use a structure or class to pass multiple arguments (see the Specification design pattern). **Exception:** A parameter that is a collection of tuples is allowed.

---

## AV1564 — Avoid signatures that take a `bool` parameter [Should]

A method taking a `bool` parameter is often doing more than one thing and needs to be refactored into two or more methods. An alternative solution is to replace the `bool` with an enumeration.

```csharp
// Unclear at call site
Customer customer = CreateCustomer(true);

// Better
Customer customer = CreateCustomer(CustomerLevel.Platinum);
```

---

## AV1570 — Prefer `is` patterns over `as` operations [Must]

Pattern matching syntax prevents `NullReferenceException` and improves readability:

```csharp
// Avoid
var remoteUser = user as RemoteUser;
if (remoteUser != null) { }

// Prefer
if (user is RemoteUser remoteUser) { }
```

---

## AV1575 — Don't comment out code [Must]

Never check in code that is commented out. Use a work item tracking system to keep track of work to be done. Nobody knows what to do when they encounter a block of commented-out code.

---

## AV1582 — Use raw string literals for multi-line or escape-heavy strings [May]

C# 11 introduced raw string literals, which make it much easier to write strings that contain quotes, backslashes, or multiple lines without escape sequences.

```csharp
// Avoid escape-heavy strings
string json = "{\n  \"name\": \"Alice\",\n  \"age\": 30\n}";

// Prefer raw string literals (C# 11+)
string json = """
    {
      "name": "Alice",
      "age": 30
    }
    """;
```

Raw string literals:
- Start and end with three or more double-quote characters (`"""`).
- Can contain any characters, including quotes and backslashes, without escaping.
- Support interpolation: `$"""..."""` (use `$$"""..."""` for JSON to avoid conflicting with `{}`).
- Trim leading indentation automatically based on the position of the closing `"""`.

---

## AV1585 — Make properties `required` when they must be set during initialization [Should]

C# 11 introduced the `required` modifier. When a property is marked `required`, the compiler enforces that any object initializer sets it, preventing accidentally uninitialized state.

```csharp
// With required: the compiler will error if OrderId is not set
public class Order
{
    public required Guid OrderId { get; set; }
    public required decimal TotalAmount { get; set; }
    public string? Notes { get; set; }
}

var order = new Order
{
    OrderId = Guid.NewGuid(),
    TotalAmount = 99.95m
    // Notes is optional, so it's fine to omit
};
```

**Note:** Combine `required` with `init`-only properties (`public required string Name { get; init; }`) to enforce both initialization and immutability.
