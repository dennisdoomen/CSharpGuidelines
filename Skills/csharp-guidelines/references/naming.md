# Naming Conventions (AV1700)

| Rule | Guideline |
|------|-----------|
| [AV1701](https://csharpcodingguidelines.com/naming-guidelines/#AV1701) | Use US English for all identifiers; favor readability and correctness over brevity. |
| [AV1702](https://csharpcodingguidelines.com/naming-guidelines/#AV1702) | Use PascalCase for types, members, namespaces, and constants; camelCase for parameters, local variables, and private fields. |
| [AV1704](https://csharpcodingguidelines.com/naming-guidelines/#AV1704) | Don't include numbers in variable, parameter, or member names. |
| [AV1705](https://csharpcodingguidelines.com/naming-guidelines/#AV1705) | Don't prefix fields with `_`, `m_`, `g_`, or `s_`. |
| [AV1706](https://csharpcodingguidelines.com/naming-guidelines/#AV1706) | Spell out names in full; avoid abbreviations except for widely accepted acronyms (e.g. `UI`, `Id`). |
| [AV1707](https://csharpcodingguidelines.com/naming-guidelines/#AV1707) | Name identifiers after their meaning, not their type. |
| [AV1708](https://csharpcodingguidelines.com/naming-guidelines/#AV1708) | Name types with nouns or noun phrases; don't include `Utility`, `Helper`, or `Manager` in a type name. |
| [AV1709](https://csharpcodingguidelines.com/naming-guidelines/#AV1709) | Prefix all generic type parameters with `T`; use a descriptive name unless a single `T` is completely unambiguous. |
| [AV1710](https://csharpcodingguidelines.com/naming-guidelines/#AV1710) | Don't repeat the enclosing class or enum name in its members. |
| [AV1711](https://csharpcodingguidelines.com/naming-guidelines/#AV1711) | Name collection/service members to match the conventions of the equivalent .NET Framework type. |
| [AV1712](https://csharpcodingguidelines.com/naming-guidelines/#AV1712) | Avoid names that are visually similar to other names (e.g. `l` vs `1`, `O` vs `0`). |
| [AV1715](https://csharpcodingguidelines.com/naming-guidelines/#AV1715) | Name boolean properties with affirmative phrases; consider prefixes `Is`, `Has`, `Can`, `Allows`, or `Supports`. |
| [AV1720](https://csharpcodingguidelines.com/naming-guidelines/#AV1720) | Name methods and local functions using a verb or verb-object pair; don't use `And` in a name. |
| [AV1725](https://csharpcodingguidelines.com/naming-guidelines/#AV1725) | Name namespaces using nouns, layers, or features; never embed a type name in a namespace name. |
| [AV1735](https://csharpcodingguidelines.com/naming-guidelines/#AV1735) | Name events with a verb or verb phrase (`Click`, `Deleted`, `Closing`). |
| [AV1737](https://csharpcodingguidelines.com/naming-guidelines/#AV1737) | Use `-ing`/`-ed` suffixes for pre/post events instead of `Before`/`After` prefixes. |
| [AV1738](https://csharpcodingguidelines.com/naming-guidelines/#AV1738) | Prefix event handler methods with `On` (e.g. `OnClosing`, `OkButtonOnClick`). |
| [AV1739](https://csharpcodingguidelines.com/naming-guidelines/#AV1739) | Use `_` (single discard in C# 9+) for irrelevant lambda parameters. |
| [AV1745](https://csharpcodingguidelines.com/naming-guidelines/#AV1745) | Group extension methods in a class suffixed with `Extensions`. |
| [AV1755](https://csharpcodingguidelines.com/naming-guidelines/#AV1755) | Postfix async methods returning `Task` or `Task<T>` with `Async`; use `TaskAsync` if a sync counterpart already exists. |
