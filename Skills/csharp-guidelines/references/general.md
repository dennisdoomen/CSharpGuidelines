# General Guidelines (AV0100) — Detailed Reference

## AV0100 — Understand the boundaries of your codebase [Must]

Every codebase has logical and physical boundaries: a microservice, a module, a library, or a bounded context. Code within a boundary can evolve freely and share abstractions, but code that crosses a boundary should go through well-defined contracts (e.g. interfaces, DTOs, or API contracts).

Understanding these boundaries matters for several reasons:

- It prevents unnecessary coupling between components that have different lifecycles or are maintained by different teams.
- It guides when to introduce an abstraction (across a boundary) versus when to skip it (within a boundary).
- It helps decide when to duplicate small pieces of logic instead of pulling in a shared dependency that would increase coupling.

**Tip:** When in doubt whether something belongs inside or outside a boundary, ask yourself: "If this boundary were a separate deployable unit, would this dependency still make sense?"

## AV0105 — Use design patterns to communicate intent [Should]

[Design patterns](https://en.wikipedia.org/wiki/Software_design_pattern) are proven, named solutions to recurring design problems. Using them lets you communicate the structure and intent of your code in a single recognizable term: *Repository*, *Factory*, *Strategy*, *Observer*, *Decorator*, and so on.

When you refactor a complex piece of code into a well-known pattern:
- Other developers immediately understand the structure without having to reverse-engineer it.
- The code gains a shared vocabulary that transcends the codebase.
- It becomes easier to reason about responsibilities and extension points.

**Tip:** If you cannot assign a single design pattern to a class, it may be doing more than one thing. Consider splitting it up.

**Tip:** Don't force a pattern onto code that doesn't need it — a pattern is a tool, not a goal. Prefer simplicity when the problem doesn't warrant the extra structure.

## AV0110 — Prefer composition over inheritance [Should]

Inheritance creates a tight coupling between a base class and its derived classes. Any change to the base class can unexpectedly break derived classes, and the inheritance hierarchy can become difficult to understand as it grows. Composition — building behavior by combining small, focused objects or interfaces — avoids these problems.

Prefer composition when:
- The relationship is "has-a" rather than "is-a".
- You want to reuse behavior without exposing or depending on the internals of another class.
- You need to vary behavior at runtime (e.g. through the [Strategy pattern](https://en.wikipedia.org/wiki/Strategy_pattern)).

**Exception:** Inheritance is appropriate when a true "is-a" relationship exists and when derived classes genuinely extend (rather than replace) the behavior of their base class. Always follow the [Liskov Substitution Principle](https://en.wikipedia.org/wiki/Liskov_substitution_principle) when using inheritance.

## AV0112 — Apply the Principle of Least Surprise [Must]

Choose solutions that are intuitive and predictable. A developer reading your code should never be surprised by what a method does, how a type is named, or what side effects an operation has.

This principle manifests in many ways:

- Name things so that the name fully describes what they do, without requiring the reader to look at the implementation.
- Avoid side effects in properties and methods that appear to only read state.
- Follow established conventions for your language, framework and domain.
- Don't deviate from common patterns without a compelling reason.

When in doubt, ask yourself: "Would a reasonably experienced developer expect this behavior from this name and signature?"

## AV0115 — Keep It Simple Stupid (KISS) [Must]

The simplest solution that fully solves the problem is almost always the best one. Complexity accumulates quickly and makes code harder to read, test, debug and change.

Avoid:
- Over-engineering for problems that don't yet exist.
- Layers of abstraction that add no value in the current context.
- Clever tricks that are hard to understand without deep knowledge of the language.

A method that a junior developer can read and understand in one pass is almost always preferable to an elegant but opaque alternative. Simplicity is a feature, not a shortcut.

## AV0120 — You Ain't Gonna Need It (YAGNI) [Must]

Build what is needed now, not what you think might be needed someday. Speculative generality creates complexity without delivering value, and the future requirements you're anticipating rarely materialize in the form you expected.

Don't:
- Add extension points, configuration switches or abstraction layers for hypothetical future use.
- Generalize code before you have at least three concrete use cases.
- Write infrastructure code before you have a feature that needs it.

Refactor when the requirements change, and you have actual information. Code that was never needed is code that still has to be read, understood and maintained.

## AV0125 — Don't Repeat Yourself (DRY), but only within boundaries [Must]

Avoid duplicating logic, knowledge or decisions within a component, service or [bounded context](https://martinfowler.com/bliki/BoundedContext.html). When the same logic exists in multiple places, a change in requirements requires multiple edits, and inconsistencies creep in over time.

However, although the DRY principle is valuable within such a boundary, blindly applying it across module or service boundaries can introduce coupling that is harder to manage than a small amount of duplication.

If sharing a tiny piece of logic between two separate modules would require adding a dependency on a shared library, exposing internal types across a boundary, or introducing a third project just to hold a few lines of code — duplicating the logic is often the simpler choice.

```csharp
// In Module A
private static bool IsValidEmail(string value)
    => value.Contains('@') && value.Contains('.');

// In Module B — a small duplication is fine here
private static bool IsValidEmail(string value)
    => value.Contains('@') && value.Contains('.');
```

Apply the [Rule of Three](https://en.wikipedia.org/wiki/Rule_of_three_(computer_programming)): refactor duplication after the third occurrence when you have identified a real pattern.

**Exception:** Duplication in tests is often beneficial as it makes tests easier to understand without the need to dig into shared helper methods.

## AV0130 — Apply the four pillars of object-oriented programming [Should]

Object-oriented design rests on four fundamental concepts. Understanding and applying them correctly is essential to writing maintainable code:

- **Encapsulation**: hide implementation details behind a well-defined interface. Expose only what callers need, and protect internal invariants (see AV1025 and AV1026).
- **Abstraction**: model the essential characteristics of a concept, ignoring irrelevant details. Good abstractions hide complexity and provide stable, meaningful interfaces.
- **Inheritance**: share behavior by forming "is-a" relationships between types. Use sparingly — prefer composition when the relationship is primarily about code reuse rather than substitutability (see AV0110).
- **Polymorphism**: let objects of different types be treated through a common interface. Prefer polymorphism over type-checking and casting (see AV1011 and AV1013).

These are tools, not goals. Apply them where they add clarity and maintainability.

## AV0135 — Treat AI-generated code as your own [Must]

Using AI tools to generate, suggest or refactor code is fine, but you are fully responsible for every line that ends up in the codebase. Do not merge AI-generated code without reviewing it, understanding it and verifying that it meets the same quality bar as hand-written code.

Specifically:
- Read and understand all generated code before committing it.
- Verify that it is correct, secure and appropriate for the context.
- Ensure it follows the same conventions and guidelines as the rest of the codebase.
- Do not use "the AI wrote it" as an excuse during a code review.

AI is a productivity tool, not a substitute for engineering judgment.
