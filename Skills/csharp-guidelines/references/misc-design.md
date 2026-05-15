# Miscellaneous Design (AV1200)

| Rule | Guideline |
|------|-----------|
| [AV1200](https://csharpcodingguidelines.com/misc-design-guidelines/#AV1200) | Throw exceptions rather than returning status values or error codes. |
| [AV1202](https://csharpcodingguidelines.com/misc-design-guidelines/#AV1202) | Provide a rich, meaningful exception message that explains the cause and how to avoid it. |
| [AV1205](https://csharpcodingguidelines.com/misc-design-guidelines/#AV1205) | Throw the most specific exception type appropriate (e.g. `ArgumentNullException` over plain `ArgumentException`). |
| [AV1210](https://csharpcodingguidelines.com/misc-design-guidelines/#AV1210) | Don't swallow errors by catching `Exception`; only do so at top-level handlers for logging or graceful shutdown. |
| [AV1215](https://csharpcodingguidelines.com/misc-design-guidelines/#AV1215) | Handle exceptions correctly in asynchronous code; understand how `async`/`await` propagates exceptions. |
| [AV1225](https://csharpcodingguidelines.com/misc-design-guidelines/#AV1225) | Use a `protected virtual OnXxx` method to raise each event so derived classes can intercept or suppress it. |
| [AV1235](https://csharpcodingguidelines.com/misc-design-guidelines/#AV1235) | Never pass `null` as the `sender` argument when raising an event; pass `EventArgs.Empty` instead of `null` args. |
| [AV1240](https://csharpcodingguidelines.com/misc-design-guidelines/#AV1240) | Use generic constraints (`where`) instead of casting to/from `object` in generic code. |
| [AV1250](https://csharpcodingguidelines.com/misc-design-guidelines/#AV1250) | Materialize LINQ results before returning them from a method (call `ToList()` or `ToArray()`). |
| [AV1251](https://csharpcodingguidelines.com/misc-design-guidelines/#AV1251) | Don't use `this.` or `base.` prefixes unless required to disambiguate. |
