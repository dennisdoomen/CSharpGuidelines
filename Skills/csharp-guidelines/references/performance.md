# Performance (AV1800)

| Rule | Guideline |
|------|-----------|
| [AV1800](https://csharpcodingguidelines.com/performance-guidelines/#AV1800) | Use `Any()` instead of `Count()` to check whether an `IEnumerable<T>` is empty. |
| [AV1820](https://csharpcodingguidelines.com/performance-guidelines/#AV1820) | Use `async`/`await` only for I/O-bound work; it does not run code on a thread-pool thread by itself. |
| [AV1825](https://csharpcodingguidelines.com/performance-guidelines/#AV1825) | Use `Task.Run` for CPU-bound work; use `TaskCreationOptions.LongRunning` for long-running background operations. |
| [AV1830](https://csharpcodingguidelines.com/performance-guidelines/#AV1830) | Don't block on async code with `.Wait()` or `.Result`; it can deadlock. |
| [AV1835](https://csharpcodingguidelines.com/performance-guidelines/#AV1835) | Blocking on a `Task` in UI frameworks (WPF, WinForms) causes deadlocks because of the single-threaded sync context. |
| [AV1840](https://csharpcodingguidelines.com/performance-guidelines/#AV1840) | Await `ValueTask`/`ValueTask<T>` directly and exactly once, or call `.AsTask()` to get a reusable `Task`. |
