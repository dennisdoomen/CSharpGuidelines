# Performance (AV1800)

| Rule | Severity | Guideline |
|------|----------|-----------|
| AV1800 | May | Use `Any()` instead of `Count()` to check whether an `IEnumerable<T>` is empty. |
| AV1820 | Must | Use `async`/`await` only for I/O-bound work; it does not run code on a thread-pool thread by itself. |
| AV1825 | Must | Use `Task.Run` for CPU-bound work; use `TaskCreationOptions.LongRunning` for long-running background operations. |
| AV1830 | Must | Don't block on async code with `.Wait()` or `.Result`; it can deadlock. |
| AV1835 | Must | Blocking on a `Task` in UI frameworks (WPF, WinForms) causes deadlocks because of the single-threaded sync context. |
| AV1840 | Must | Await `ValueTask`/`ValueTask<T>` directly and exactly once, or call `.AsTask()` to get a reusable `Task`. |
