# Maintainability (AV1500)

| Rule | Severity | Guideline |
|------|----------|-----------|
| AV1500 | Must | Keep methods under 15 statements and at a single level of abstraction. |
| AV1501 | Must | Default all members to `private` and types to `internal sealed`. |
| AV1502 | Should | Avoid conditions with double negatives. |
| AV1505 | May | Name assemblies after their contained namespace (`Company.Component.dll`). |
| AV1506 | May | Name each source file after the type it contains, in PascalCase with no underscores. |
| AV1507 | May | Limit a source file to one type (exceptions: nested types, same-type-different-generic-arity). |
| AV1508 | May | Name partial type files after the logical role they play (e.g. `MyClass.Designer.cs`). |
| AV1510 | May | Use `using` directives instead of fully qualified type names; use aliases to resolve conflicts. |
| AV1515 | Must | Replace literal numeric and string values with named constants or enum members. |
| AV1520 | Must | Only use `var` when the type is evident from the right-hand side; never use `var` for built-in types. |
| AV1521 | Should | Declare and initialize each variable at the point of first use, not at the top of the block. |
| AV1522 | Must | Assign each variable in a separate statement; don't chain assignments. |
| AV1523 | Should | Favor object and collection initializers over separate property-assignment statements. |
| AV1525 | Must | Don't compare `bool` expressions to `true` or `false` explicitly. |
| AV1530 | Should | Don't modify a `for` loop variable inside the loop body; use `break` or `continue` instead. |
| AV1532 | Should | Avoid nested loops; prefer LINQ joins or extracted methods. |
| AV1535 | Should | Always add curly braces after `if`, `else`, `do`, `while`, `for`, `foreach`, and `case`. |
| AV1536 | Must | Always include a `default` block in `switch`; add a comment or throw if it should never be reached. |
| AV1537 | Should | Finish every `if`-`else if` chain with a final `else` block. |
| AV1540 | Should | Be reluctant with multiple `return` statements; one entry / one exit improves readability in most cases. |
| AV1545 | Should | Use direct assignment over `if`-`else`; leverage `??`, `??=`, `?.`, and ternary operators. |
| AV1546 | Must | Prefer interpolated strings (`$"…"`) over `string.Format` or concatenation. |
| AV1547 | Must | Encapsulate complex inline expressions in a clearly named method, property, or local function. |
| AV1551 | Should | Chain overloads so simpler ones call the most-complete overload; make the most-complete overload `virtual`. |
| AV1553 | Must | Only use optional parameters as a shorthand for overloads; never use `null` defaults for strings, collections, or tasks. |
| AV1554 | Must | Don't add optional parameters to interface methods. |
| AV1555 | Must | Avoid named arguments; exception: use them to clarify a `bool` parameter you don't control. |
| AV1561 | Must | Limit signatures to 3 parameters; don't use tuple parameters; don't return tuples with more than 2 elements. |
| AV1562 | Must | Avoid `ref` and `out` parameters; return a tuple or value type instead. Exception: `TryParse`-style patterns. |
| AV1564 | Should | Avoid `bool` parameters that switch behavior; use two methods or an enum instead. |
| AV1570 | Must | Use `is`-pattern matching instead of `as`-then-null-check casts. |
| AV1575 | Must | Never commit commented-out code; use an issue tracker for deferred work. |
| AV1580 | Should | Write code that is easy to debug; break long call chains into named intermediate variables. |
| AV1582 | May | Use raw string literals (`"""…"""`) for multi-line or escape-heavy strings (C# 11+). |
| AV1585 | Should | Mark properties `required` when they must be set during object initialization (C# 11+). |
