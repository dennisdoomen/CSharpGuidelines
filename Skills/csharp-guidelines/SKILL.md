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

Consult the `references/` files for the full set of rules per category:

- [General (AV0100)](references/general.md) — KISS, YAGNI, DRY, OOP pillars, AI code
- [Class Design (AV1000)](references/class-design.md) — SRP, LSP, LoD, coupling
- [Member Design (AV1100)](references/member-design.md) — properties, methods, parameters, null rules
- [Miscellaneous Design (AV1200)](references/misc-design.md) — exceptions, events, generics, LINQ
- [Maintainability (AV1500)](references/maintainability.md) — method size, var, control flow, naming files
- [Testability (AV1600)](references/testability.md) — test naming, builders, public API testing
- [Naming (AV1700)](references/naming.md) — casing, prefixes, abbreviations, async suffix
- [Performance (AV1800)](references/performance.md) — async/await, Task, ValueTask
- [Framework Usage (AV2200)](references/framework.md) — idiomatic C#, dynamic, LINQ syntax
- [Documentation (AV2300)](references/documentation.md) — XML docs, inline comments
- [Layout (AV2400)](references/layout.md) — indentation, member order, regions
