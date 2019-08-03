---
title: Performance Guidelines
permalink: /performance-guidelines/
classes: wide
search: true
sidebar:
  nav: "sidebar"
---

### <a name="av1800"></a> Consider using `Any()` to determine whether an `IEnumerable<T>` is empty (AV1800) ![](/assets/images/3.png)
When a member or local function returns an `IEnumerable<T>` or other collection class that does not expose a `Count` property, use the `Any()` extension method rather than `Count()` to determine whether the collection contains items. If you do use `Count()`, you risk that iterating over the entire collection might have a significant impact (such as when it really is an `IQueryable<T>` to a persistent store).

**Note:** If you return an `IEnumerable<T>` to prevent changes from calling code as explained in AV1130, and you're developing in .NET 4.5 or higher, consider the new read-only classes.

### <a name="av1820"></a> Only use `async` for low-intensive long-running activities (AV1820) ![](/assets/images/1.png)
The usage of `async` won't automagically run something on a worker thread like `Task.Run` does. It just adds the necessary logic to allow releasing the current thread, and marshal the result back on that same thread if a long-running asynchronous operation has completed. In other words, use `async` only for I/O bound operations.

### <a name="av1825"></a> Prefer `Task.Run` for CPU-intensive activities (AV1825) ![](/assets/images/1.png)
If you do need to execute a CPU bound operation, use `Task.Run` to offload the work to a thread from the Thread Pool. Remember that you have to marshal the result back to your main thread manually.

### <a name="av1830"></a> Beware of mixing up `async`/`await` with `Task.Wait` (AV1830) ![](/assets/images/1.png)
`await` does not block the current thread but simply instructs the compiler to generate a state-machine. However, `Task.Wait` blocks the thread and may even cause deadlocks (see AV1835).

### <a name="av1835"></a> Beware of `async`/`await` deadlocks in single-threaded environments (AV1835) ![](/assets/images/1.png)
Consider the following asynchronous method:

	private async Task GetDataAsync()
	{
		var result = await MyWebService.GetDataAsync();
		return result.ToString();
	}

Now when an ASP.NET MVC controller action does this:

	public ActionResult ActionAsync()
	{
		var data = GetDataAsync().Result;
		
		return View(data);  
	}

You end up with a deadlock. Why? Because the `Result` property getter will block until the `async` operation has completed, but since an `async` method _could_ automatically marshal the result back to the original thread (depending on the current `SynchronizationContext` or `TaskScheduler`) and ASP.NET uses a single-threaded synchronization context, they'll be waiting on each other. A similar problem can also happen on UWP, WPF or a Windows Store C#/XAML app. Read more about this [here](http://blogs.msdn.com/b/pfxteam/archive/2011/01/13/10115163.aspx).
