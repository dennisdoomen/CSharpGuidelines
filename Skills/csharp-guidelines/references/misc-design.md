# Miscellaneous Design (AV1200) — Detailed Reference

## AV1200 — Throw exceptions rather than returning some kind of status value [Should]

A code base that uses return values to report success or failure tends to have nested if-statements sprinkled all over the code. Quite often, a caller forgets to check the return value anyway. Structured exception handling has been introduced to allow you to throw exceptions and catch or replace them at a higher layer. In most systems it is quite common to throw exceptions whenever an unexpected situation occurs.

## AV1202 — Provide a rich and meaningful exception message text [Should]

The message should explain the cause of the exception, and clearly describe what needs to be done to avoid the exception.

## AV1205 — Throw the most specific exception that is appropriate [May]

For example, if a method receives a `null` argument, it should throw `ArgumentNullException` instead of its base type `ArgumentException`.

## AV1210 — Don't swallow errors by catching generic exceptions [Must]

Avoid swallowing errors by catching non-specific exceptions, such as `Exception`, `SystemException`, and so on, in application code. Only in top-level code, such as a last-chance exception handler, you should catch a non-specific exception for logging purposes and a graceful shutdown of the application.

## AV1215 — Properly handle exceptions in asynchronous code [Should]

When throwing or handling exceptions in code that uses `async`/`await` or a `Task` remember the following two rules:

- Exceptions that occur within an `async`/`await` block and inside a `Task`'s action are propagated to the awaiter.
- Exceptions that occur in the code preceding the asynchronous block are propagated to the caller.

## AV1225 — Use a protected virtual method to raise each event [Should]

Complying with this guideline allows derived classes to handle a base class event by overriding the protected method. The name of the protected virtual method should be the same as the event name prefixed with `On`. For example, the protected virtual method for an event named `TimeChanged` is named `OnTimeChanged`.

**Note:** Derived classes that override the protected virtual method are not required to call the base class implementation. The base class must continue to work correctly even if its implementation is not called.

## AV1235 — Don't pass `null` as the `sender` argument when raising an event [Must]

Often an event handler is used to handle similar events from multiple senders. The sender argument is then used to get to the source of the event. Always pass a reference to the source (typically `this`) when raising the event. Furthermore don't pass `null` as the event data parameter when raising an event. If there is no event data, pass `EventArgs.Empty` instead of `null`.

**Exception:** On static events, the sender argument should be `null`.

## AV1240 — Use generic constraints if applicable [Should]

Instead of casting to and from the object type in generic types or methods, use `where` constraints or the `as` operator to specify the exact characteristics of the generic parameter. For example:

```csharp
// Don't
class MyClass
{
    void SomeMethod<T>(T t)
    {
        object temp = t;
        SomeClass obj = (SomeClass)temp;
    }
}

// Do
class MyClass where T : SomeClass
{
    void SomeMethod<T>(T t)
    {
        SomeClass obj = t;
    }
}
```

## AV1250 — Materialize the result of a LINQ expression before returning it [Must]

Since LINQ queries use deferred execution, returning a query expression will actually return the expression tree representing the query. Each time the caller evaluates this result using a `foreach` loop or similar, the entire query is re-executed resulting in new instances every time. Consequently, you cannot use the `==` operator to compare multiple instances. Instead, always explicitly evaluate the result of a LINQ query using `ToList()`, `ToArray()` or similar methods.

## AV1251 — Do not use `this` and `base` prefixes unless it is required [Must]

In a class hierarchy, it is not necessary to know at which level a member is declared to use it. Refactoring derived classes is harder if that level is fixed in the code.
