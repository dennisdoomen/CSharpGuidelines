# General Guidelines (AV0100)

| Rule | Severity | Guideline |
|------|----------|-----------|
| AV0100 | Must | Understand the logical and physical boundaries of your codebase; cross-boundary communication must go through well-defined contracts. |
| AV0105 | Should | Use design patterns to communicate intent (*Repository*, *Factory*, *Strategy*, etc.). |
| AV0110 | Should | Prefer composition over inheritance; reserve inheritance for true "is-a" relationships. |
| AV0112 | Must | Apply the Principle of Least Surprise: name things predictably, avoid hidden side effects, follow platform conventions. |
| AV0115 | Must | Keep It Simple (KISS): the simplest solution that fully solves the problem is almost always the best. |
| AV0120 | Must | You Ain't Gonna Need It (YAGNI): build what is needed now, not what might be needed someday. |
| AV0125 | Must | Don't Repeat Yourself (DRY) within a bounded context; allow limited duplication across boundaries to avoid coupling. |
| AV0130 | Should | Apply the four pillars of OOP (encapsulation, abstraction, inheritance, polymorphism) as tools, not goals. |
| AV0135 | Must | Treat AI-generated code as your own: review, understand, and verify every line before committing it. |
