# Naming Conventions (AV1700)

| Rule | Severity | Guideline |
|------|----------|-----------|
| AV1701 | Must | Use US English for all identifiers; favor readability and correctness over brevity. |
| AV1702 | Must | Use PascalCase for types, members, namespaces, and constants; camelCase for parameters, local variables, and private fields. |
| AV1704 | May | Don't include numbers in variable, parameter, or member names. |
| AV1705 | Must | Don't prefix fields with `_`, `m_`, `g_`, or `s_`. |
| AV1706 | Should | Spell out names in full; avoid abbreviations except for widely accepted acronyms (e.g. `UI`, `Id`). |
| AV1707 | Should | Name identifiers after their meaning, not their type. |
| AV1708 | Should | Name types with nouns or noun phrases; don't include `Utility`, `Helper`, or `Manager` in a type name. |
| AV1709 | Should | Prefix all generic type parameters with `T`; use a descriptive name unless a single `T` is completely unambiguous. |
| AV1710 | Must | Don't repeat the enclosing class or enum name in its members. |
| AV1711 | May | Name collection/service members to match the conventions of the equivalent .NET Framework type. |
| AV1712 | Must | Avoid names that are visually similar to other names (e.g. `l` vs `1`, `O` vs `0`). |
| AV1715 | Should | Name boolean properties with affirmative phrases; consider prefixes `Is`, `Has`, `Can`, `Allows`, or `Supports`. |
| AV1720 | Should | Name methods and local functions using a verb or verb-object pair; don't use `And` in a name. |
| AV1725 | May | Name namespaces using nouns, layers, or features; never embed a type name in a namespace name. |
| AV1735 | Should | Name events with a verb or verb phrase (`Click`, `Deleted`, `Closing`). |
| AV1737 | May | Use `-ing`/`-ed` suffixes for pre/post events instead of `Before`/`After` prefixes. |
| AV1738 | May | Prefix event handler methods with `On` (e.g. `OnClosing`, `OkButtonOnClick`). |
| AV1739 | May | Use `_` (single discard in C# 9+) for irrelevant lambda parameters. |
| AV1745 | May | Group extension methods in a class suffixed with `Extensions`. |
| AV1755 | Should | Postfix async methods returning `Task` or `Task<T>` with `Async`; use `TaskAsync` if a sync counterpart already exists. |
