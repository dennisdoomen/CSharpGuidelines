# Member Design (AV1100)

| Rule | Severity | Guideline |
|------|----------|-----------|
| AV1100 | Must | Properties should be stateless with respect to each other; property access order must not matter. |
| AV1105 | May | Use a method instead of a property when the operation is expensive, conversion-like, non-deterministic, or has observable side effects. |
| AV1110 | Must | Don't use mutually exclusive properties; they signal two conflicting concepts in one type. |
| AV1115 | May | A property, method, or local function should do exactly one thing. |
| AV1125 | Should | Don't hide dependencies behind static members; inject them explicitly. |
| AV1130 | Should | Return `IEnumerable<T>`, `IReadOnlyCollection<T>`, or `IReadOnlyList<T>` instead of concrete mutable collections. |
| AV1135 | Must | Strings, collections, and tasks should never be `null`; return empty equivalents instead. |
| AV1137 | Should | Define parameters as specific and narrow as possible; don't require callers to pass more than you need. |
| AV1140 | May | Consider creating domain-specific value types rather than passing primitives everywhere. |
| AV1145 | May | Use C# 14 extension members to add behavior to types you don't own or to keep domain logic close to its type. |
| AV1150 | May | Avoid local functions; prefer extracting to a named private method. Exceptions: recursion, iterators, or many captured variables. |
| AV1155 | May | Use the C# 14 `field` keyword in auto-properties when you need validation or transformation on get/set without a full backing field. |
