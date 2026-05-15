# Testability (AV1600)

| Rule | Guideline |
|------|-----------|
| [AV1600](https://csharpcodingguidelines.com/testability-guidelines/#AV1600) | Write test names as short, present-tense sentences describing observable behavior, not implementation details. |
| [AV1602](https://csharpcodingguidelines.com/testability-guidelines/#AV1602) | Suffix test classes with `Specs` instead of `Tests` to encourage a behavior-driven mindset. |
| [AV1605](https://csharpcodingguidelines.com/testability-guidelines/#AV1605) | Test observable behavior through public APIs only; never test private methods or internal state directly. |
| [AV1608](https://csharpcodingguidelines.com/testability-guidelines/#AV1608) | Make the important parts of a test visible in the test body; hide irrelevant setup in helper methods or builders. |
| [AV1610](https://csharpcodingguidelines.com/testability-guidelines/#AV1610) | Use Test Data Builders for flexible construction of complex test objects; use Object Mothers for canonical fixed fixtures. |
| [AV1615](https://csharpcodingguidelines.com/testability-guidelines/#AV1615) | Prefer inline literal values in assertions when the value itself communicates the intent of the test. |
| [AV1618](https://csharpcodingguidelines.com/testability-guidelines/#AV1618) | Assert against independently known expected values; never derive an expected value from the production code under test. |
| [AV1620](https://csharpcodingguidelines.com/testability-guidelines/#AV1620) | Test reusable components (validators, serializers, domain services) in isolation, independently of their consumers. |
| [AV1622](https://csharpcodingguidelines.com/testability-guidelines/#AV1622) | Access internal implementation details through the public API of the component that uses them, not directly. |
