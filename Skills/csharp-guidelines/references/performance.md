# Performance (AV1800) — Detailed Reference

> **Severity legend:** **Must** (1) · **Should** (2) · **May** (3)

---

## AV1800 — Consider using `Any()` to determine whether an `IEnumerable<T>` is empty [May]

When a member or local function returns an `IEnumerable<T>` or other collection class that does not expose a `Count` property, use the `Any()` extension method rather than `Count()` to determine whether the collection contains items. If you do use `Count()`, you risk that iterating over the entire collection might have a significant impact (such as when it really is an `IQueryable<T>` to a persistent store).

---

## AV1820 — Only use `async`/`await` for I/O-bound or long-running activities [Must]

The use of `async`/`await` won't automagically run something on a worker thread as `Task.Run` does. It just suspends execution at the `await` point and resumes execution after the task has completed. In other words, use `async`/`await` only for I/O-bound operations.

**Exception:** Tasks returned from `Task.Run` (which starts a background operation in parallel) can eventually be awaited to obtain their results, or passed to a method like `Task.WhenAll` that is awaited.

---

## AV1825 — Prefer `Task.Run` for CPU-intensive activities [Must]

If you need to execute a CPU-bound operation, use `Task.Run` to offload the work to a thread from the Thread Pool. Remember that you have to marshal the result back to your main thread manually.

For long-running operations, use `Task.Factory.StartNew` with `TaskCreationOptions.LongRunning` to hint the runtime to use a dedicated thread instead of a thread pool thread.

---

## AV1830 — Beware of mixing up `async`/`await` with `Task.Wait` [Must]

`await` does not block the current thread but simply instructs the compiler to generate a state-machine. However, `Task.Wait` blocks the thread and may even cause deadlocks (see AV1835).

---

## AV1835 — Beware of `async`/`await` deadlocks in UI frameworks (e.g. WPF, WinForms) [Must]

Consider the following asynchronous method:

```csharp
private async Task<string> GetDataAsync()
{
    var result = await MyWebService.GetDataAsync();
    return result.ToString();
}
```

When a button event handler is implemented like this:

```csharp
public async void Button1_Click(object sender, RoutedEventArgs e)
{
    var data = GetDataAsync().Result;
    textBox1.Text = data;
}
```

You will likely end up with a deadlock. The `Result` property getter blocks until the `async` operation has completed, but since an `async` method could automatically marshal the result back to the original thread (depending on the current `SynchronizationContext`) and WPF/WinForms uses a single-threaded synchronization context, they'll be waiting on each other.

---

## AV1840 — Await `ValueTask` and `ValueTask<T>` directly and exactly once [Must]

The consumption of `ValueTask` and `ValueTask<T>` is more restrictive than consuming `Task` or `Task<T>`. The safest way to consume a `ValueTask`/`ValueTask<T>` is to directly `await` it once, or call `.AsTask()` to get a `Task`/`Task<T>`:

```csharp
// OK — await directly once
int bytesRead = await stream.ReadAsync(buffer, cancellationToken);

// OK — with ConfigureAwait
int bytesRead = await stream.ReadAsync(buffer, cancellationToken).ConfigureAwait(false);

// OK — convert to Task to overcome ValueTask limitations
Task<int> task = stream.ReadAsync(buffer, cancellationToken).AsTask();
```

Other usage patterns (e.g. saving the `ValueTask` into a variable and awaiting later) may still work, but may lead to misuse eventually, especially when the underlying object is pooled/reused after it has been awaited.
