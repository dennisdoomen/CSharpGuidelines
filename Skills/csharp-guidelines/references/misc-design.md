# Miscellaneous Design (AV1200)

| Rule | Severity | Guideline |
|------|----------|-----------|
| AV1200 | Should | Throw exceptions rather than returning status values or error codes. |
| AV1202 | Should | Provide a rich, meaningful exception message that explains the cause and how to avoid it. |
| AV1205 | May | Throw the most specific exception type appropriate (e.g. `ArgumentNullException` over plain `ArgumentException`). |
| AV1210 | Must | Don't swallow errors by catching `Exception`; only do so at top-level handlers for logging or graceful shutdown. |
| AV1215 | Should | Handle exceptions correctly in asynchronous code; understand how `async`/`await` propagates exceptions. |
| AV1225 | Should | Use a `protected virtual OnXxx` method to raise each event so derived classes can intercept or suppress it. |
| AV1235 | Must | Never pass `null` as the `sender` argument when raising an event; pass `EventArgs.Empty` instead of `null` args. |
| AV1240 | Should | Use generic constraints (`where`) instead of casting to/from `object` in generic code. |
| AV1250 | Must | Materialize LINQ results before returning them from a method (call `ToList()` or `ToArray()`). |
| AV1251 | Must | Don't use `this.` or `base.` prefixes unless required to disambiguate. |
