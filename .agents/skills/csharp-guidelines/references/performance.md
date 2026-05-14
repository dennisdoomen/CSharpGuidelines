# Performance (AV1800) — Detailed Reference

## AV1800 — Consider using `Any()` to determine whether an `IEnumerable<T>` is empty [May]

When a member or local function returns an `IEnumerable<T>` or other collection class that does not expose a `Count` property, use the `Any()` extension method rather than `Count()` to determine whether the collection contains items. If you do use `Count()`, you risk that iterating over the entire collection might have a significant impact (such as when it really is an `IQueryable<T>` to a persistent store).

**Note:** If you return an `IEnumerable<T>` to prevent changes from calling code as explained in , and you're developing in .NET 4.5 or higher, consider the new read-only classes.

## AV1820 — Only use `async` for low-intensive long-running activities [Must]

The usage of `async` won't automagically run something on a worker thread like `Task.Run` does. It just adds the necessary logic to allow releasing the current thread, and marshal the result back on that same thread if a long-running asynchronous operation has completed. In other words, use `async` only for I/O bound operations.

## AV1825 — Prefer `Task.Run` or `Task.Factory.StartNew` for CPU-intensive activities [Must]

If you do need to execute a CPU bound operation, use `Task.Run` to offload the work to a thread from the Thread Pool. For long-running operations use `Task.Factory.StartNew` with `TaskCreationOptions.LongRunning` parameter to create a new thread. Remember that you have to marshal the result back to your main thread manually.

## AV1830 — Beware of mixing up `async`/`await` with `Task.Wait` [Must]

`await` does not block the current thread but simply instructs the compiler to generate a state-machine. However, `Task.Wait` blocks the thread and may even cause deadlocks (see ).

## AV1835 — Beware of `async`/`await` deadlocks in special environments (e.g. WPF) [Must]

Consider the following asynchronous method:

	private async Task<string> GetDataAsync()
	{
		var result = await MyWebService.GetDataAsync();
		return result.ToString();
	}

Now when a button event handler is implemented like this:

	public async void Button1_Click(object sender, RoutedEventArgs e)
	{
		var data = GetDataAsync().Result;
		textBox1.Text = data;
	}

You will likely end up with a deadlock. Why? Because the `Result` property getter will block until the `async` operation has completed, but since an `async` method _could_ automatically marshal the result back to the original thread (depending on the current `SynchronizationContext` or `TaskScheduler`) and WPF uses a single-threaded synchronization context, they'll be waiting on each other. A similar problem can also happen on UWP, WinForms, classical ASP.NET (not ASP.NET Core) or a Windows Store C#/XAML app. Read more about this [here](https://devblogs.microsoft.com/pfxteam/await-and-ui-and-deadlocks-oh-my/).

## AV1840 — Await `ValueTask` and `ValueTask<T>` directly and exactly once [Must]

The consumption of the newer and performance related `ValueTask` and `ValueTask<T>` types is more restrictive than consuming `Task` or `Task<T>`. Starting with .NET Core 2.1 the `ValueTask<T>` is not only able to wrap the result `T` or a `Task<T>`, with this version it is also possible to wrap a `IValueTaskSource` / `IValueTaskSource<T>` which gives the developer extra support for reuse and pooling. This enhanced support might lead to unwanted side-effects, as the ValueTask-returning developer might reuse the underlying object after it got awaited. The safest way to consume a `ValueTask` / `ValueTask<T>` is to directly `await` it once, or call `.AsTask()` to get a `Task` / `Task<T>` to overcome these limitations.

	// OK / GOOD
	int bytesRead = await stream.ReadAsync(buffer, cancellationToken);

	// OK / GOOD
	int bytesRead = await stream.ReadAsync(buffer, cancellationToken).ConfigureAwait(false);

	// OK / GOOD - Get task if you want to overcome the limitations exposed by ValueTask / ValueTask<T>
	Task<int> task = stream.ReadAsync(buffer, cancellationToken).AsTask();

Other usage patterns might still work (like saving the `ValueTask` / `ValueTask<T>` into a variable and awaiting later), but may lead to misuse eventually. Not awaiting a `ValueTask` / `ValueTask<T>` may also cause unwanted side-effects. Read more about `ValueTask` / `ValueTask<T>` and the correct usage [here](https://devblogs.microsoft.com/dotnet/understanding-the-whys-whats-and-whens-of-valuetask/).

