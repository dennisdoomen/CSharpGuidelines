# Maintainability (AV1500)

| Rule | Guideline |
|------|-----------|
| [AV1500](https://csharpcodingguidelines.com/maintainability-guidelines/#AV1500) | Keep methods under 15 statements and at a single level of abstraction. |
| [AV1501](https://csharpcodingguidelines.com/maintainability-guidelines/#AV1501) | Default all members to `private` and types to `internal sealed`. |
| [AV1502](https://csharpcodingguidelines.com/maintainability-guidelines/#AV1502) | Avoid conditions with double negatives. |
| [AV1505](https://csharpcodingguidelines.com/maintainability-guidelines/#AV1505) | Name assemblies after their contained namespace (`Company.Component.dll`). |
| [AV1506](https://csharpcodingguidelines.com/maintainability-guidelines/#AV1506) | Name each source file after the type it contains, in PascalCase with no underscores. |
| [AV1507](https://csharpcodingguidelines.com/maintainability-guidelines/#AV1507) | Limit a source file to one type (exceptions: nested types, same-type-different-generic-arity). |
| [AV1508](https://csharpcodingguidelines.com/maintainability-guidelines/#AV1508) | Name partial type files after the logical role they play (e.g. `MyClass.Designer.cs`). |
| [AV1510](https://csharpcodingguidelines.com/maintainability-guidelines/#AV1510) | Use `using` directives instead of fully qualified type names; use aliases to resolve conflicts. |
| [AV1515](https://csharpcodingguidelines.com/maintainability-guidelines/#AV1515) | Replace literal numeric and string values with named constants or enum members. |
| [AV1520](https://csharpcodingguidelines.com/maintainability-guidelines/#AV1520) | Only use `var` when the type is evident from the right-hand side; never use `var` for built-in types. |
| [AV1521](https://csharpcodingguidelines.com/maintainability-guidelines/#AV1521) | Declare and initialize each variable at the point of first use, not at the top of the block. |
| [AV1522](https://csharpcodingguidelines.com/maintainability-guidelines/#AV1522) | Assign each variable in a separate statement; don't chain assignments. |
| [AV1523](https://csharpcodingguidelines.com/maintainability-guidelines/#AV1523) | Favor object and collection initializers over separate property-assignment statements. |
| [AV1525](https://csharpcodingguidelines.com/maintainability-guidelines/#AV1525) | Don't compare `bool` expressions to `true` or `false` explicitly. |
| [AV1530](https://csharpcodingguidelines.com/maintainability-guidelines/#AV1530) | Don't modify a `for` loop variable inside the loop body; use `break` or `continue` instead. |
| [AV1532](https://csharpcodingguidelines.com/maintainability-guidelines/#AV1532) | Avoid nested loops; prefer LINQ joins or extracted methods. |
| [AV1535](https://csharpcodingguidelines.com/maintainability-guidelines/#AV1535) | Always add curly braces after `if`, `else`, `do`, `while`, `for`, `foreach`, and `case`. |
| [AV1536](https://csharpcodingguidelines.com/maintainability-guidelines/#AV1536) | Always include a `default` block in `switch`; add a comment or throw if it should never be reached. |
| [AV1537](https://csharpcodingguidelines.com/maintainability-guidelines/#AV1537) | Finish every `if`-`else if` chain with a final `else` block. |
| [AV1540](https://csharpcodingguidelines.com/maintainability-guidelines/#AV1540) | Be reluctant with multiple `return` statements; one entry / one exit improves readability in most cases. |
| [AV1545](https://csharpcodingguidelines.com/maintainability-guidelines/#AV1545) | Use direct assignment over `if`-`else`; leverage `??`, `??=`, `?.`, and ternary operators. |
| [AV1546](https://csharpcodingguidelines.com/maintainability-guidelines/#AV1546) | Prefer interpolated strings (`$"…"`) over `string.Format` or concatenation. |
| [AV1547](https://csharpcodingguidelines.com/maintainability-guidelines/#AV1547) | Encapsulate complex inline expressions in a clearly named method, property, or local function. |
| [AV1551](https://csharpcodingguidelines.com/maintainability-guidelines/#AV1551) | Chain overloads so simpler ones call the most-complete overload; make the most-complete overload `virtual`. |
| [AV1553](https://csharpcodingguidelines.com/maintainability-guidelines/#AV1553) | Only use optional parameters as a shorthand for overloads; never use `null` defaults for strings, collections, or tasks. |
| [AV1554](https://csharpcodingguidelines.com/maintainability-guidelines/#AV1554) | Don't add optional parameters to interface methods. |
| [AV1555](https://csharpcodingguidelines.com/maintainability-guidelines/#AV1555) | Avoid named arguments; exception: use them to clarify a `bool` parameter you don't control. |
| [AV1561](https://csharpcodingguidelines.com/maintainability-guidelines/#AV1561) | Limit signatures to 3 parameters; don't use tuple parameters; don't return tuples with more than 2 elements. |
| [AV1562](https://csharpcodingguidelines.com/maintainability-guidelines/#AV1562) | Avoid `ref` and `out` parameters; return a tuple or value type instead. Exception: `TryParse`-style patterns. |
| [AV1564](https://csharpcodingguidelines.com/maintainability-guidelines/#AV1564) | Avoid `bool` parameters that switch behavior; use two methods or an enum instead. |
| [AV1570](https://csharpcodingguidelines.com/maintainability-guidelines/#AV1570) | Use `is`-pattern matching instead of `as`-then-null-check casts. |
| [AV1575](https://csharpcodingguidelines.com/maintainability-guidelines/#AV1575) | Never commit commented-out code; use an issue tracker for deferred work. |
| [AV1580](https://csharpcodingguidelines.com/maintainability-guidelines/#AV1580) | Write code that is easy to debug; break long call chains into named intermediate variables. |
| [AV1582](https://csharpcodingguidelines.com/maintainability-guidelines/#AV1582) | Use raw string literals (`"""…"""`) for multi-line or escape-heavy strings (C# 11+). |
| [AV1585](https://csharpcodingguidelines.com/maintainability-guidelines/#AV1585) | Mark properties `required` when they must be set during object initialization (C# 11+). |
