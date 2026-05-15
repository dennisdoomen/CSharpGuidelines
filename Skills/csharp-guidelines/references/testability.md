# Testability (AV1600)

| Rule | Severity | Guideline |
|------|----------|-----------|
| AV1600 | Should | Write test names as short, present-tense sentences describing observable behavior, not implementation details. |
| AV1602 | May | Suffix test classes with `Specs` instead of `Tests` to encourage a behavior-driven mindset. |
| AV1605 | Must | Test observable behavior through public APIs only; never test private methods or internal state directly. |
| AV1608 | Should | Make the important parts of a test visible in the test body; hide irrelevant setup in helper methods or builders. |
| AV1610 | Should | Use Test Data Builders for flexible construction of complex test objects; use Object Mothers for canonical fixed fixtures. |
| AV1615 | May | Prefer inline literal values in assertions when the value itself communicates the intent of the test. |
| AV1618 | Must | Assert against independently known expected values; never derive an expected value from the production code under test. |
| AV1620 | Should | Test reusable components (validators, serializers, domain services) in isolation, independently of their consumers. |
| AV1622 | Should | Access internal implementation details through the public API of the component that uses them, not directly. |
