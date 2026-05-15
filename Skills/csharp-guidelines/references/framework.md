# Framework Usage (AV2200)

| Rule | Severity | Guideline |
|------|----------|-----------|
| AV2202 | Must | Prefer idiomatic C# syntax over verbose .NET API calls: tuple literals, `is null`, `is not null`, `??=`, `[]` collection expressions, and C# 14 null-coalescing assignment. |
| AV2210 | Must | Build with the highest warning level and treat warnings as errors. |
| AV2220 | May | Prefer LINQ extension method syntax over query syntax for simple expressions. |
| AV2225 | May | Use tuple/record deconstruction to avoid intermediate variables and simplify assignments. |
| AV2230 | Must | Only use the `dynamic` keyword for interop with dynamic languages or APIs; avoid it in hot code paths. |
