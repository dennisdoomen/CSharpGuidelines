# Documentation (AV2300) — Detailed Reference

> **Severity legend:** **Must** (1) · **Should** (2) · **May** (3)

---

## AV2301 — Write comments and documentation in US English [Must]

All XML documentation and inline comments should use American English.

---

## AV2305 — Document all `public`, `protected` and `internal` types and members [Should]

Good functional documentation allows Visual Studio, Visual Studio Code or JetBrains Rider to display the documentation when your class is used somewhere else. Furthermore, by properly documenting your classes, tools can generate professionally looking class documentation.

**Exception:** Sometimes the purpose is so obvious that the documentation would become repetitive. Don't do that.

**Note:** You don't need to use `/// <inheritdoc/>` on overriding or implementing members. Visual Studio and Rider will automatically inherit documentation from the base type or interface.

---

## AV2306 — Write XML documentation with other developers in mind [Should]

Write the documentation of your type with other developers in mind. Assume they will not have access to the source code and try to explain how to get the most out of the functionality of your type.

---

## AV2308 — Document the purpose of a member, instead of describing its implementation [Should]

A member's name should describe its intent from a consumer perspective. Documentation should provide the *why* and the *what*, not describe the internals.

```csharp
// Avoid — describes implementation
/// <summary>
/// Iterates over the list and sets IsActive to false for each item.
/// </summary>
public void Deactivate(IEnumerable<Item> items)

// Prefer — describes purpose
/// <summary>
/// Marks all items as inactive so they are excluded from future processing.
/// </summary>
public void Deactivate(IEnumerable<Item> items)
```

Documentation that simply restates the implementation gives no extra value and becomes a maintenance burden when the implementation changes.

---

## AV2310 — Avoid inline comments [Should]

If you feel the need to explain a block of code using a comment, consider replacing that block with a method with a clear name.

---

## AV2316 — Only write comments to explain complex algorithms or decisions [Must]

Try to focus comments on the *why* and *what* of a code block and not the *how*. Avoid explaining the statements in words, but instead help the reader understand why you chose a certain solution or algorithm and what you are trying to achieve. If applicable, also mention that you chose an alternative solution because you ran into a problem with the obvious solution.

---

## AV2318 — Don't use comments for tracking work to be done later [May]

Annotating a block of code or some work to be done using a *TODO* or similar comment may seem a reasonable way of tracking work-to-be-done. But in reality, nobody really searches for comments like that. Use a work item tracking system to keep track of leftovers.
