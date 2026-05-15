---
name: csharp-guidelines
description: >-
  Apply the C# coding and design guidelines by Dennis Doomen when writing, reviewing, or refactoring
  C# code. Use this skill whenever you generate or evaluate C# to ensure it follows the established
  rules for class design, member design, maintainability, naming, performance, testability, and more.
user-invocable: true
---

# C# Coding Guidelines

Apply these rules whenever writing or reviewing C# code.

**Severity:** **Must** = always enforce · **Should** = follow unless there is a clear reason not to · **May** = optional good practice.

For full rule text, examples, and tips see the `references/` files linked at the bottom of each section.

---

## General (AV0100)

| Rule | Severity | Summary |
|------|----------|---------|
| AV0100 | Must | Understand the logical and physical boundaries of your codebase; cross-boundary code goes through well-defined contracts. |
| AV0105 | Should | Use design patterns to communicate intent — *Repository*, *Factory*, *Strategy*, etc. |
| AV0110 | Should | Prefer composition over inheritance; reserve inheritance for true "is-a" relationships. |
| AV0112 | Must | Apply the Principle of Least Surprise: name things predictably, avoid hidden side effects, follow conventions. |
| AV0115 | Must | Keep It Simple Stupid (KISS): the simplest solution that fully solves the problem is almost always the best. |
| AV0120 | Must | You Ain't Gonna Need It (YAGNI): build what is needed now, not what you think might be needed someday. |
| AV0125 | Must | Don't Repeat Yourself (DRY) within a boundary, but allow small duplications across module boundaries to avoid coupling. |
| AV0130 | Should | Apply the four pillars of OOP: encapsulation, abstraction, inheritance, and polymorphism as tools, not goals. |
| AV0135 | Must | Treat AI-generated code as your own: review, understand and verify every line before committing it. |

→ Full details: [references/general.md](references/general.md)

---

## Class Design (AV1000)

| Rule | Severity | Summary |
|------|----------|---------|
| AV1000 | Must | A class or interface should have a single purpose (Single Responsibility Principle). |
| AV1001 | May | Constructors should return a fully usable object without extra property-setting afterward. |
| AV1003 | Should | Interfaces should be small, focused, and named to explain their role (Interface Segregation Principle). |
| AV1004 | May | Expose extension points as interfaces, not base classes. |
| AV1005 | Should | Use interfaces to decouple classes: prevent bidirectional associations and enable DI and testability. |
| AV1008 | May | Avoid static classes except for extension method containers; they are hard to test. |
| AV1010 | Must | Don't suppress compiler warning CS0114 with `new`; fix the design instead (breaks polymorphism). |
| AV1011 | Should | Honor the Liskov Substitution Principle: a derived type must be usable wherever its base is expected. |
| AV1013 | Must | Don't refer to derived classes from a base class. |
| AV1014 | Should | Avoid exposing the objects a class depends on (Law of Demeter). |
| AV1020 | Must | Avoid bidirectional dependencies between classes. |
| AV1025 | Must | Classes should have both state and behavior; avoid data-only/behavior-only splits. |
| AV1026 | Must | Protect internal state consistency: validate public arguments and guard invariants. |

→ Full details: [references/class-design.md](references/class-design.md)

---

## Member Design (AV1100)

| Rule | Severity | Summary |
|------|----------|---------|
| AV1100 | Must | Properties should be stateless with respect to each other (order-independent). |
| AV1105 | May | Use a method instead of a property when work is expensive, conversion-like, non-deterministic, or has side effects. |
| AV1110 | Must | Don't use mutually exclusive properties; they signal two conflicting concepts. |
| AV1115 | May | A property, method, or local function should do only one thing. |
| AV1125 | Should | Don't hide dependencies behind static members; inject them explicitly. |
| AV1130 | Should | Return read-only collection interfaces (`IEnumerable<T>`, `IReadOnlyCollection<T>`, etc.). |
| AV1135 | Must | Strings, collections, and tasks should never be `null`; return empty equivalents instead. |
| AV1137 | Should | Define parameters as specific and narrow as possible ("Don't ship the truck if you only need a package"). |
| AV1140 | May | Consider creating domain-specific value types rather than using primitives. |
| AV1145 | May | Use C# 14 extension members to add behavior to types you don't own or to keep domain logic close to the type. |
| AV1150 | May | Avoid local functions; prefer extracting to a named method. Exception: recursion, iterators, many captures. |
| AV1155 | May | Use the C# 14 `field` keyword in auto-properties when validation or transformation logic is needed. |

→ Full details: [references/member-design.md](references/member-design.md)

---

## Miscellaneous Design (AV1200)

| Rule | Severity | Summary |
|------|----------|---------|
| AV1200 | Should | Throw exceptions rather than returning status values. |
| AV1202 | Should | Provide a rich, meaningful exception message that explains the cause and how to avoid it. |
| AV1205 | May | Throw the most specific exception that is appropriate (e.g. `ArgumentNullException` over `ArgumentException`). |
| AV1210 | Must | Don't swallow errors by catching generic `Exception`; only do so at top-level handlers for logging/shutdown. |
| AV1215 | Should | Handle exceptions correctly in asynchronous code (`async`/`await` propagation rules). |
| AV1225 | Should | Use a protected virtual `OnXxx` method to raise each event. |
| AV1235 | Must | Don't pass `null` as the `sender` argument when raising an event; pass `EventArgs.Empty` instead of `null`. |
| AV1240 | Should | Use generic constraints (`where`) instead of casting to/from `object` in generic types. |
| AV1250 | Must | Materialize the result of a LINQ expression before returning it (use `ToList()` / `ToArray()`). |
| AV1251 | Must | Do not use `this` and `base` prefixes unless required. |

→ Full details: [references/misc-design.md](references/misc-design.md)

---

## Maintainability (AV1500)

| Rule | Severity | Summary |
|------|----------|---------|
| AV1500 | Must | Methods should not exceed 15 statements; each method should operate at a single level of abstraction. |
| AV1501 | Must | Make all members `private` and types `internal sealed` by default. |
| AV1502 | Should | Avoid conditions with double negatives. |
| AV1505 | May | Name assemblies after their contained namespace (`Company.Component.dll`). |
| AV1506 | May | Name a source file after the type it contains (PascalCase, no underscores). |
| AV1507 | May | Limit a source file to one type (exceptions: nested types, same-type-different-arity). |
| AV1508 | May | Name partial type files after the logical part they play. |
| AV1510 | May | Use `using` statements instead of fully qualified type names. |
| AV1515 | Must | Don't use magic numbers; use named constants or enums. |
| AV1520 | Must | Only use `var` when the type is evident from the right-hand side; never for built-in types. |
| AV1521 | Should | Declare variables at the point of first use, not at the beginning of a block. |
| AV1522 | Must | Don't use assignment in a condition expression. |
| AV1523 | Should | Favor object initializers over property-setting sequences after construction. |
| AV1525 | Must | Don't compare `bool` expressions to `true` or `false` explicitly. |
| AV1530 | Should | Don't modify the loop variable inside a `for` loop body. |
| AV1532 | Should | Avoid nested loops; extract to a method instead. |
| AV1535 | Should | Always add a block after keywords like `if`, `else`, `while`, `for`, `foreach`, `do`. |
| AV1536 | Must | Always add a `default` block in `switch` statements; add a descriptive comment if it is intentionally empty. |
| AV1537 | Should | Finish every `if`-`else if` chain with an `else` block. |
| AV1540 | Should | Prefer early returns and `continue`/`break` to reduce nesting (one entry, one exit is a guideline, not a law). |
| AV1545 | Should | Use direct assignment instead of an `if`-`else` block; leverage `??`, `??=`, `?.`, and ternary operators. |
| AV1546 | Must | Use `$"…"` interpolated strings over `string.Format`; concatenate constants with `+` at compile time. |
| AV1547 | Must | Don't use deeply nested lambda expressions inline; extract to a named method or variable. |
| AV1551 | Should | Call the most-overloaded method from simpler overloads; make the most complete overload `virtual`. |
| AV1553 | Must | Only use C# optional parameters to replace overloads for simple default arguments. |
| AV1554 | Must | Don't use optional parameters in interface methods; default values are ignored at call sites. |
| AV1555 | Must | Avoid named arguments except for `bool` parameters where the name clarifies intent. |
| AV1561 | Must | No more than 3 parameters per method, constructor, or delegate; no tuples with more than 2 elements returned. |
| AV1562 | Must | Don't use `ref` or `out` parameters; return a compound type or tuple instead. Exception: TryParse pattern. |
| AV1564 | Should | Avoid `bool` parameters that change a method's behavior; use overloads or an enum instead. |
| AV1570 | Must | Prefer `is` patterns over `as`-then-null-check casts. |
| AV1575 | Must | Never check in commented-out code. |
| AV1580 | Should | Write code that is easy to debug: break deep method-call chains into intermediate named variables. |

→ Full details: [references/maintainability.md](references/maintainability.md)

---

## Testability (AV1600)

| Rule | Severity | Summary |
|------|----------|---------|
| AV1600 | Should | Use short, concise, functional test names in present tense that describe behavior, not implementation. |
| AV1602 | May | Postfix test classes with `Specs` instead of `Tests` to encourage a behavior-driven mindset. |
| AV1605 | Must | Test observable behavior through public APIs, not private implementation details. |
| AV1608 | Should | Show what's important in a test body; hide irrelevant setup behind Test Data Builders or Object Mothers. |
| AV1610 | Should | Use Test Data Builders for flexible object construction; use Object Mothers for fixed canonical test objects. |
| AV1615 | May | Prefer inline literals over named constants in tests when the value itself communicates the test's intent. |
| AV1618 | Must | Don't use production code in test assertions; always assert against independently known expected values. |
| AV1620 | Should | Test reusable components (validators, serializers, domain services) separately from their consumers. |
| AV1622 | Should | Test internal implementation details through the public API of the component that uses them. |

→ Full details: [references/testability.md](references/testability.md)

---

## Naming (AV1700)

| Rule | Severity | Summary |
|------|----------|---------|
| AV1701 | Must | Use US English for all identifiers; prefer readability over brevity. |
| AV1702 | Must | Use correct casing: Pascal for types/members/namespaces, camel for fields/parameters/variables. |
| AV1704 | May | Don't include numbers in variable or member names. |
| AV1705 | Must | Don't prefix fields with `_`, `m_`, `g_`, or `s_`. |
| AV1706 | Should | Don't use abbreviations in names. |
| AV1707 | Should | Name members, parameters and variables according to their meaning, not their type. |
| AV1708 | Should | Name interfaces with a noun, noun phrase, or adjective phrase. |
| AV1709 | Should | Always prefix type parameter names with `T`; add a descriptive segment when more than one type parameter. |
| AV1710 | Must | Don't repeat the class or enum name in its members. |
| AV1711 | May | Name members similarly to .NET Framework members where appropriate. |
| AV1712 | Must | Avoid short names or names that can be mistaken for each other (e.g. `l` vs `1`, `O` vs `0`). |
| AV1715 | Should | Name properties with nouns, noun phrases, or adjective phrases. |
| AV1720 | Should | Name methods and local functions with a verb or verb-object pair; don't use `And` in a method name. |
| AV1725 | May | Name namespaces using nouns, layers, or features; never include a type name in a namespace. |
| AV1735 | Should | Name events with a verb or verb phrase (`Click`, `Deleted`, `Closing`). |
| AV1739 | May | Use `_` for irrelevant lambda parameters (C# 9+). |
| AV1745 | May | Group extension methods in a class suffixed with `Extensions`. |
| AV1755 | Should | Postfix asynchronous methods returning `Task` or `Task<T>` with `Async`. |

→ Full details: [references/naming.md](references/naming.md)

---

## Performance (AV1800)

| Rule | Severity | Summary |
|------|----------|---------|
| AV1800 | May | Use `Any()` instead of `Count()` to check whether an `IEnumerable<T>` is empty. |
| AV1820 | Must | Only use `async`/`await` for I/O-bound or long-running activities; it does not run code on a worker thread. |
| AV1825 | Must | Use `Task.Run` for CPU-intensive work; use `Task.Factory.StartNew` with `LongRunning` for long operations. |
| AV1830 | Must | Don't mix `async`/`await` with `.Wait()` or `.Result`; it can cause deadlocks. |
| AV1835 | Must | Beware of `async`/`await` deadlocks in UI frameworks (WPF, WinForms) when blocking on tasks. |
| AV1840 | Must | Await `ValueTask`/`ValueTask<T>` directly and exactly once, or call `.AsTask()` first. |

→ Full details: [references/performance.md](references/performance.md)

---

## Framework Usage (AV2200)

| Rule | Severity | Summary |
|------|----------|---------|
| AV2202 | Must | Prefer idiomatic C# syntax over equivalent .NET Framework API calls (tuples, `is null`, `??=`, collection expressions, C# 14 features). |
| AV2210 | Must | Build with the highest warning level and treat warnings as errors. |
| AV2220 | May | Avoid LINQ query syntax for simple expressions; prefer extension method syntax. |
| AV2225 | May | Use deconstruction to simplify variable assignments from tuples, records, and collections. |
| AV2230 | Must | Only use the `dynamic` keyword for interop with dynamic languages; avoid it in hot code paths. |

→ Full details: [references/framework.md](references/framework.md)

---

## Documentation & Comments (AV2300)

| Rule | Severity | Summary |
|------|----------|---------|
| AV2301 | Must | Write all comments and documentation in US English. |
| AV2305 | Should | Document all `public`, `protected`, and `internal` types and members with XML doc comments. |
| AV2306 | Should | Write documentation with other developers in mind; describe the intent, not the implementation. |
| AV2308 | Should | Document the purpose of a member (the *why* and *what*), not its implementation details. |
| AV2310 | Should | Avoid inline comments; extract complex blocks into well-named methods instead. |
| AV2316 | Must | Focus comments on the *why* and *what*, not the *how*. |
| AV2318 | May | Don't use TODO comments as a substitute for a proper issue tracker. |

→ Full details: [references/documentation.md](references/documentation.md)

---

## Layout (AV2400)

| Rule | Severity | Summary |
|------|----------|---------|
| AV2400 | Must | Follow common layout rules: max 130 chars/line, 4-space indent, spaces around operators, braces on own lines. |
| AV2402 | May | Order `using` directives: System namespaces first, then third-party, then own. |
| AV2406 | Must | Place members in a well-defined order: fields → constants → factory methods → constructors → events → properties → methods. |
| AV2407 | Must | Don't use `#region`; regions hide code and require extra work without improving readability. |
| AV2410 | Must | Use expression-bodied member syntax only when the body is a single statement that fits on one line. |

→ Full details: [references/layout.md](references/layout.md)
