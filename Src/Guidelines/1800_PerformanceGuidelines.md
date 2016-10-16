<!--
NOTE: Requires Markdown Extra. See http://michelf.ca/projects/php-markdown/extra/
 --> 

#7. Performance Guidelines

### Consider using `Any()` to determine whether an `IEnumerable<T>` is empty (AV1800) ![](images/3.png)
When a method or other member returns an `IEnumerable<T>` or other collection class that does not expose a `Count` property, use the `Any()` extension method rather than `Count()` to determine whether the collection contains items. If you do use `Count()`, you risk that iterating over the entire collection might have a significant impact (such as when it really is an `IQueryable<T>` to a persistent store).

**Note:** If you return an `IEnumerable<T>` to prevent editing from outside the owner as explained in AV1130, and you're developing in .NET 4.5 or higher, consider the new read-only classes.

### Only use `async` for low-intensive long-running activities (AV1820) ![](images/1.png)
The usage of `async` won't automagically run something on a worker thread like `Task.Run` does. It just adds the necessary logic to allow releasing the current thread, and marshal the result back on that same thread if a long-running asynchronous operation has completed. In other words, use `async` only for I/O bound operations.

### Prefer `Task.Run` for CPU-intensive activities (AV1825) ![](images/1.png)
If you do need to execute a CPU bound operation, use `Task.Run` to offload the work to a thread from the Thread Pool. Remember that you have to marshal the result back to your main thread manually.

### Beware of mixing up `await`/`async` with `Task.Wait` (AV1830) ![](images/1.png)
`await` does not block the current thread but simply instructs the compiler to generate a state-machine. However, `Task.Wait` blocks the thread and may even cause deadlocks (see AV1835).

### Beware of `async`/`await` deadlocks in single-threaded environments (AV1835) ![](images/1.png)
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

You end up with a deadlock. Why? Because the `Result` property getter will block until the `async` operation has completed, but since an `async` method will automatically marshal the result back to the original thread and ASP.NET uses a single-threaded synchronization context, they'll be waiting on each other. A similar problem can also happen on WPF, Silverlight or a Windows Store C#/XAML app. Read more about this [here](http://blogs.msdn.com/b/pfxteam/archive/2011/01/13/10115163.aspx).
