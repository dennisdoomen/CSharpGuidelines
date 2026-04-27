---
name: csharp-guidelines
description: 'Apply the Aviva Solutions C# coding guidelines when writing, reviewing, or refactoring C# code. Use this skill whenever generating or evaluating C# to ensure it follows the established style, design, naming, and maintainability rules.'
---

# C# Coding Guidelines

Apply these rules whenever writing or reviewing C# code. Severity: **Must** = always enforce · **Should** = follow unless there is a clear reason not to · **May** = optional good practice.

For detailed explanations and examples, see the reference files in `references/`.

## Class Design

| Rule | Severity | Summary |
|------|----------|---------|
| AV1000 | Must | A class or interface should have a single purpose within the system it functions in. |
| AV1001 | May | There should be no need to set additional properties before the object can be used for whatever purpose it was designed. |
| AV1003 | Should | Interfaces should have a name that clearly explains their purpose or role in the system. |
| AV1004 | May | If you want to expose an extension point from your class, expose it as an interface rather than as a base class. |
| AV1005 | Should | Interfaces are a very effective mechanism for decoupling classes from each other: - They can prevent bidirectional as... |
| AV1008 | May | With the exception of extension method containers, static classes very often lead to badly designed code. |
| AV1010 | Must | Compiler warning [CS0114](https://docs.microsoft.com/en-us/dotnet/csharp/misc/cs0114) is issued when breaking [Polymo... |
| AV1011 | Should | In other words, you should be able to pass an instance of a derived class wherever its base class is expected, withou... |
| AV1013 | Must | Having dependencies from a base class to its sub-classes goes against proper object-oriented design and might prevent... |
| AV1014 | Should | If you find yourself writing code like this then you might be violating the [Law of Demeter](http://en.wikipedia.org/... |
| AV1020 | Must | This means that two classes know about each other's public members or rely on each other's internal behavior. |
| AV1025 | Must | In general, if you find a lot of data-only classes in your code base, you probably also have a few (static) classes w... |
| AV1026 | Must | Validate incoming arguments from public members. |

→ Full details: [references/class-design.md](references/class-design.md)

## Member Design

| Rule | Severity | Summary |
|------|----------|---------|
| AV1100 | Must | Properties should be stateless with respect to other properties, i.e. |
| AV1105 | May | - If the work is more expensive than setting a field value. |
| AV1110 | Must | Having properties that cannot be used at the same time typically signals a type that represents two conflicting conce... |
| AV1115 | Must | Similarly to rule a method body should have a single responsibility. |
| AV1125 | Should | A stateful object is an object that contains many properties and lots of behavior behind it. |
| AV1130 | Should | You generally don't want callers to be able to change an internal collection, so don't return arrays, lists or other ... |
| AV1135 | Must | Returning `null` can be unexpected by the caller. |
| AV1137 | Should | If your method or local function needs a specific piece of data, define parameters as specific as that and don't take... |
| AV1140 | May | Instead of using strings, integers and decimals for representing domain-specific types such as an ISBN number, an ema... |

→ Full details: [references/member-design.md](references/member-design.md)

## Miscellaneous Design

| Rule | Severity | Summary |
|------|----------|---------|
| AV1200 | Should | A code base that uses return values to report success or failure tends to have nested if-statements sprinkled all ove... |
| AV1202 | Should | The message should explain the cause of the exception, and clearly describe what needs to be done to avoid the except... |
| AV1205 | May | For example, if a method receives a `null` argument, it should throw `ArgumentNullException` instead of its base type... |
| AV1210 | Must | Avoid swallowing errors by catching non-specific exceptions, such as `Exception`, `SystemException`, and so on, in ap... |
| AV1215 | Should | When throwing or handling exceptions in code that uses `async`/`await` or a `Task` remember the following two rules: ... |
| AV1220 | Must | An event that has no subscribers is `null`. |
| AV1225 | Should | Complying with this guideline allows derived classes to handle a base class event by overriding the protected method. |
| AV1230 | May | Consider providing events that are raised when certain properties are changed. |
| AV1235 | Must | Often an event handler is used to handle similar events from multiple senders. |
| AV1240 | Should | Instead of casting to and from the object type in generic types or methods, use `where` constraints or the `as` opera... |
| AV1250 | Must | Consider the following code snippet public IEnumerable<GoldMember> GetGoldMemberCustomers() { const decimal GoldMembe... |
| AV1251 | Must | In a class hierarchy, it is not necessary to know at which level a member is declared to use it. |

→ Full details: [references/misc-design.md](references/misc-design.md)

## Maintainability

| Rule | Severity | Summary |
|------|----------|---------|
| AV1500 | Must | A method that requires more than 7 statements is simply doing too much or has too many responsibilities. |
| AV1501 | Must | To make a more conscious decision on which members to make available to other classes, first restrict the scope as mu... |
| AV1502 | Should | Although a property like `customer.HasNoOrders` makes sense, avoid using it in a negative condition like this: bool h... |
| AV1505 | May | All DLLs should be named according to the pattern *Company*.*Component*.dll where *Company* refers to your company's ... |
| AV1506 | May | Use Pascal casing to name the file and don't use underscores. |
| AV1507 | May | **Exception:** Nested types should be part of the same file. |
| AV1508 | May | When using partial types and allocating a part per file, name each file after the logical part that part plays. |
| AV1510 | May | Limit usage of fully qualified type names to prevent name clashing. |
| AV1515 | Must | Don't use literal values, either numeric or strings, in your code, other than to define symbolic constants. |
| AV1520 | Must | Use `var` for anonymous types (typically resulting from a LINQ query), or if the type is [evident](https://www.jetbra... |
| AV1521 | Should | Avoid the C and Visual Basic styles where all variables have to be defined at the beginning of a block, but rather de... |
| AV1522 | Must | Don't use confusing constructs like the one below: var result = someField = GetSomeMethod(); **Exception:** Multiple ... |
| AV1523 | Should | Instead of: var startInfo = new ProcessStartInfo("myapp.exe"); startInfo.StandardOutput = Console.Output; startInfo.U... |
| AV1525 | Must | It is usually bad style to compare a `bool`-type expression to `true` or `false`. |
| AV1530 | Should | Updating the loop variable within the loop body is generally considered confusing, even more so if the loop variable ... |
| AV1532 | Should | A method that nests loops is more difficult to understand than one with only a single loop. |
| AV1535 | Should | Please note that this also avoids possible confusion in statements of the form: if (isActive) if (isVisible) Foo(); e... |
| AV1536 | Must | Add a descriptive comment if the `default` block is supposed to be empty. |
| AV1537 | Should | For example: void Foo(string answer) { if (answer == "no") { Console.WriteLine("You answered with No"); } else if (an... |
| AV1540 | Should | One entry, one exit is a sound principle and keeps control flow readable. |
| AV1545 | Should | Express your intentions directly. |
| AV1546 | Must | Since .NET 6, interpolated strings are optimized at compile-time, which inlines constants and reduces memory allocati... |
| AV1547 | Must | Consider the following example: if (member.HidesBaseClassMember && member.NodeType != NodeType.InstanceInitializer) {... |
| AV1551 | Should | This guideline only applies to overloads that are intended to provide optional arguments. |
| AV1553 | Must | The only valid reason for using C# 4.0's optional parameters is to replace the example from rule with a single method... |
| AV1554 | Must | When an interface method defines an optional parameter, its default value is discarded during overload resolution unl... |
| AV1555 | Must | C# 4.0's named arguments have been introduced to make it easier to call COM components that are known for offering ma... |
| AV1561 | Must | To keep constructors, methods, delegates and local functions small and focused, do not use more than three parameters. |
| AV1562 | Must | They make code less understandable and might cause people to introduce bugs. |
| AV1564 | Should | Consider the following method signature: public Customer CreateCustomer(bool platinumLevel) { } On first sight this s... |
| AV1568 | May | Never use a parameter as a convenient variable for storing temporary state. |
| AV1570 | Must | If you use 'as' to safely upcast an interface reference to a certain type, always verify that the operation does not ... |
| AV1575 | Must | Never check in code that is commented out. |
| AV1580 | Should | Because debugger breakpoints cannot be set inside expressions, avoid overuse of nested method calls. |

→ Full details: [references/maintainability.md](references/maintainability.md)

## Naming Conventions

| Rule | Severity | Summary |
|------|----------|---------|
| AV1701 | Must | All identifiers (such as types, type members, parameters and variables) should be named using words from the American... |
| AV1702 | Must | &#124; Language element &#124; Casing&#124; Example &#124; &#124;:--------------------&#124;:----------&#124;:-----------&#124; &#124; Namespace &#124; Pascal &#124; `Syste... |
| AV1704 | May | In most cases they are a lazy excuse for not defining a clear and intention-revealing name. |
| AV1705 | Must | For example, don't use `g_` or `s_` to distinguish static from non-static fields. |
| AV1706 | Should | For example, use `ButtonOnClick` rather than `BtnOnClick`. |
| AV1707 | Should | - Use functional names. |
| AV1708 | Should | For example, the name IComponent uses a descriptive noun, ICustomAttributeProvider uses a noun phrase and IPersistabl... |
| AV1709 | Should | - Always prefix type parameter names with the letter `T`. |
| AV1710 | Must | class Employee { // Wrong! static GetEmployee() {...} DeleteEmployee() {...} // Right. |
| AV1711 | May | .NET developers are already accustomed to the naming patterns the framework uses, so following this same pattern help... |
| AV1712 | Must | Although technically correct, statements like the following can be confusing: bool b001 = lo == l0 ? I1 == 11 : lOl !... |
| AV1715 | Should | - Name properties with nouns, noun phrases, or occasionally adjective phrases. |
| AV1720 | Should | Name a method or local function using a verb like `Show` or a verb-object pair such as `ShowDialog`. |
| AV1725 | May | For instance, the following namespaces are good examples of that guideline. |
| AV1735 | Should | Name events with a verb or a verb phrase. |
| AV1737 | May | For example, a close event that is raised before a window is closed would be called `Closing`, and one that is raised... |
| AV1738 | May | It is good practice to prefix the method that handles an event with "On". |
| AV1739 | May | If you use a lambda expression (for instance, to subscribe to an event) and the actual parameters of the event are ir... |
| AV1745 | May | If the name of an extension method conflicts with another member or extension method, you must prefix the call with t... |
| AV1755 | Should | The general convention for methods and local functions that return `Task` or `Task<TResult>` is to postfix them with ... |

→ Full details: [references/naming.md](references/naming.md)

## Performance

| Rule | Severity | Summary |
|------|----------|---------|
| AV1800 | May | When a member or local function returns an `IEnumerable<T>` or other collection class that does not expose a `Count` ... |
| AV1820 | Must | The usage of `async` won't automagically run something on a worker thread like `Task.Run` does. |
| AV1825 | Must | If you do need to execute a CPU bound operation, use `Task.Run` to offload the work to a thread from the Thread Pool. |
| AV1830 | Must | `await` does not block the current thread but simply instructs the compiler to generate a state-machine. |
| AV1835 | Must | Consider the following asynchronous method: private async Task<string> GetDataAsync() { var result = await MyWebServi... |
| AV1840 | Must | The consumption of the newer and performance related `ValueTask` and `ValueTask<T>` types is more restrictive than co... |

→ Full details: [references/performance.md](references/performance.md)

## Framework Usage

| Rule | Severity | Summary |
|------|----------|---------|
| AV2201 | Must | For instance, use `object` instead of `Object`, `string` instead of `String`, and `int` instead of `Int32`. |
| AV2202 | Must | Language syntax makes code more concise. |
| AV2207 | May | Examples include connection strings, server addresses, etc. |
| AV2210 | Must | Configure the development environment to use the highest available warning level for the C# compiler, and enable the ... |
| AV2220 | May | Rather than: var query = from item in items where item.Length > 0 select item; prefer the use of extension methods fr... |
| AV2221 | Should | Lambda expressions provide a more elegant alternative for anonymous methods. |
| AV2230 | Must | The `dynamic` keyword has been introduced for interop with languages where properties and methods can appear and disa... |
| AV2235 | Must | Using the new C# 5.0 keywords results in code that can still be read sequentially and also improves maintainability a... |

→ Full details: [references/framework.md](references/framework.md)

## Documentation & Comments

| Rule | Severity | Summary |
|------|----------|---------|
| AV2301 | Must |  |
| AV2305 | Should | Documenting your code allows Visual Studio, [Visual Studio Code](https://code.visualstudio.com/) or [Jetbrains Rider]... |
| AV2306 | Should | Write the documentation of your type with other developers in mind. |
| AV2307 | May | Following the MSDN online help style and word choice helps developers find their way through your documentation more ... |
| AV2310 | Should | If you feel the need to explain a block of code using a comment, consider replacing that block with a method with a c... |
| AV2316 | Must | Try to focus comments on the *why* and *what* of a code block and not the *how*. |
| AV2318 | May | Annotating a block of code or some work to be done using a *TODO* or similar comment may seem a reasonable way of tra... |

→ Full details: [references/commenting.md](references/commenting.md)

## Layout

| Rule | Severity | Summary |
|------|----------|---------|
| AV2400 | Must | - Keep the length of each line under 130 characters. |
| AV2402 | May | // System namespaces come first. |
| AV2406 | Must | Maintaining a common order allows other team members to find their way in your code more easily. |
| AV2407 | Must | Regions require extra work without increasing the quality or the readability of code. |
| AV2410 | Must | Favor expression-bodied member syntax over regular member syntax only when: - the body consists of a single statement... |

→ Full details: [references/layout.md](references/layout.md)

