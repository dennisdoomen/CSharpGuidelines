# Testability (AV1600) — Detailed Reference

## AV1600 — Use short concise functional test names [Should]

A test name should describe the behavior being verified in plain language, not the implementation detail being exercised. It should read like a sentence that a non-developer can understand, preferably in the present tense. Avoid words like "should" and "would".

```csharp
// Avoid
[Fact]
public void TestCalculations1() { ... }

[Fact]
public void CalculateTotal_WhenDiscountIsApplied_ShouldReturnReducedAmount() { ... }

// Prefer
[Fact]
public void Total_is_reduced_when_a_discount_is_applied() { ... }
```

Use underscores to separate words if your test framework supports it. Keep names short enough to fit on one line, but long enough to communicate intent without reading the test body.

## AV1602 — Postfix test classes with `Specs` instead of `Tests` [May]

The suffix `Specs` signals that a class describes the observable behavior (specifications) of a component, not just a set of mechanical tests. This encourages a behavior-driven mindset.

```csharp
// Avoid
public class OrderServiceTests { ... }

// Prefer
public class OrderServiceSpecs { ... }

// Or nest specs per scenario
public class OrderServiceSpecs
{
    public class OrderPlacement { ... }
}
```

Nested classes group related scenarios together and make the test report more readable.

## AV1605 — Test observable behavior, not private implementation details [Must]

Tests should verify what a component does, not how it does it internally. Tests that depend on private state, internal method calls, or specific execution sequences are brittle: they break when the implementation changes, even when the behavior stays correct.

Specifically:
- Test through public APIs only.
- Don't assert on the number of times a method was called unless call count is part of the observable contract.
- Don't reach into private fields or methods using reflection.
- Prefer asserting on the returned value, the state visible through the public interface, or the side effects that callers can observe.

When you feel the urge to test a private method directly, consider whether that method should become its own class with its own public API.

## AV1608 — Show what's important in a test, hide what's not [Should]

Every test has a specific intent. The data, setup and assertions that are directly relevant to that intent should be visible in the test body. Everything else should be hidden behind [Test Data Builders](https://wiki.c2.com/?TestDataBuilder) or [Object Mothers](https://wiki.c2.com/?ObjectMother) (see AV1610).

## AV1610 — Use Test Data Builders or Object Mothers to construct test objects [Should]

When setting up test data, avoid scattering `new SomeObject(...)` constructors across many tests. This makes tests hard to maintain when the object's constructor changes, and makes it hard to understand which properties matter to the test.

Use a **Test Data Builder** when you need flexible control over many properties:

```csharp
var order = new OrderBuilder()
    .WithCustomer(customer)
    .WithStatus(OrderStatus.Pending)
    .Build();
```

Use an **Object Mother** (a simple static factory method) when you have a small number of canonical test objects that are reused as-is:

```csharp
var order = TestOrders.PendingOrder();
var order = TestOrders.ShippedOrder();
```

Both approaches make tests more readable and resilient to object construction changes. Prefer builders when flexibility is needed, and object mothers when a few fixed examples are sufficient.

## AV1615 — Prefer inline literals over constant variables in tests [May]

In production code, extracting magic values into named constants is valuable. In tests, inline literals often make the test more readable because the value itself is part of what the test communicates.

```csharp
// Less clear: reader must look up what ValidEmail means
private const string ValidEmail = "alice@example.com";

[Fact]
public void Accepts_a_valid_email_address()
{
    var result = EmailValidator.Validate(ValidEmail);
    result.Should().BeTrue();
}

// Clearer: the value is right there
[Fact]
public void Accepts_a_valid_email_address()
{
    var result = EmailValidator.Validate("alice@example.com");
    result.Should().BeTrue();
}
```

Use constants in tests when the same value is used across many tests and a change would require updating all of them, or when the value has a specific name that adds meaning beyond the literal (e.g. `MaxSizeInBytes = 128`).

## AV1618 — Don't use production code in test assertions [Must]

If your assertion calls the same production code that you're testing, you may be testing that the code is consistent with itself, not that it produces the correct result.

```csharp
// Wrong: uses production formatter to assert the formatter's output
[Fact]
public void FormatCurrency_formats_amount_correctly()
{
    var result = CurrencyFormatter.Format(9.95m, "EUR");
    result.Should().Be(CurrencyFormatter.Format(9.95m, "EUR")); // always passes
}

// Correct: asserts against a known expected value
[Fact]
public void FormatCurrency_formats_amount_correctly()
{
    var result = CurrencyFormatter.Format(9.95m, "EUR");
    result.Should().Be("€9.95");
}
```

Always assert against concrete, independently known expected values.

## AV1620 — Test reusable components separately from their consumers [Should]

Components that are designed to be reused (serializers, validators, domain services, extension methods) deserve their own focused test suite. Don't rely on tests of the consumer to cover the reusable component.

Benefits:
- Failures in the component are reported directly against it, not buried in a consumer's tests.
- The component's behavior is documented independently.
- Refactoring the consumer doesn't accidentally reduce coverage of the shared component.

```csharp
// Don't rely on OrderServiceSpecs to cover EmailValidator
public class EmailValidatorSpecs
{
    [Fact]
    public void Rejects_an_address_without_at_sign() { ... }

    [Fact]
    public void Accepts_a_standard_email_address() { ... }
}
```

## AV1622 — Test concrete implementations as part of a larger integration scope [Should]

Internal implementation details that are not individually reusable are best tested through the component that uses them, not in isolation. Testing every internal type independently ties your test suite to the internal structure and makes refactoring harder.

```csharp
// Avoid testing internal infrastructure directly
public class SqlOrderRepositoryInternalsSpecs
{
    [Fact]
    public void BuildsCorrectSqlQuery() { ... } // fragile implementation detail
}

// Prefer testing through the public contract
public class OrderRepositorySpecs
{
    [Fact]
    public void Retrieves_an_order_by_id() { ... } // observable behavior
}
```

Reserve unit tests for logic that is independently reusable or complex enough to warrant isolation. For the rest, integration or component tests through the public API provide better coverage with less maintenance overhead.
