# Maintainability (AV1500) — Detailed Reference

## AV1500 — Methods should not exceed 7 statements [Must]

A method that requires more than 7 statements is simply doing too much or has too many responsibilities. It also requires the human mind to analyze the exact statements to understand what the code is doing. Break it down into multiple small and focused methods with self-explaining names, but make sure the high-level algorithm is still clear.

## AV1501 — Make all members `private` and types `internal sealed` by default [Must]

To make a more conscious decision on which members to make available to other classes, first restrict the scope as much as possible. Then carefully decide what to expose as a public member or type.

## AV1502 — Avoid conditions with double negatives [Should]

Although a property like `customer.HasNoOrders` makes sense, avoid using it in a negative condition like this:

	bool hasOrders = !customer.HasNoOrders;

Double negatives are more difficult to grasp than simple expressions, and people tend to read over the double negative easily.

## AV1505 — Name assemblies after their contained namespace [May]

All DLLs should be named according to the pattern *Company*.*Component*.dll where *Company* refers to your company's name and *Component* contains one or more dot-separated clauses. For example `AvivaSolutions.Web.Controls.dll`.

As an example, consider a group of classes organized under the namespace `AvivaSolutions.Web.Binding` exposed by a certain assembly. According to this guideline, that assembly should be called `AvivaSolutions.Web.Binding.dll`.

**Exception:** If you decide to combine classes from multiple unrelated namespaces into one assembly, consider suffixing the assembly name with `Core`, but do not use that suffix in the namespaces. For instance, `AvivaSolutions.Consulting.Core.dll`.

## AV1506 — Name a source file to the type it contains [May]

Use Pascal casing to name the file and don't use underscores. Don't include (the number of) generic type parameters in the file name.

## AV1507 — Limit the contents of a source code file to one type [May]

**Exception:** Nested types should be part of the same file.

**Exception:** Types that only differ by their number of generic type parameters should be part of the same file.

## AV1508 — Name a source file to the logical function of the partial type [May]

When using partial types and allocating a part per file, name each file after the logical part that part plays. For example:

	// In MyClass.cs
	public partial class MyClass
	{...}

	// In MyClass.Designer.cs
	public partial class MyClass
	{...}

## AV1510 — Use `using` statements instead of fully qualified type names [May]

Limit usage of fully qualified type names to prevent name clashing. For example, don't do this:

	var list = new System.Collections.Generic.List<string>();

Instead, do this:

	using System.Collections.Generic;

	var list = new List<string>();

If you do need to prevent name clashing, use a `using` directive to assign an alias:

	using Label = System.Web.UI.WebControls.Label;

## AV1515 — Don't use "magic" numbers [Must]

Don't use literal values, either numeric or strings, in your code, other than to define symbolic constants. For example:

	public class Whatever
	{
		public static readonly Color PapayaWhip = new Color(0xFFEFD5);
		public const int MaxNumberOfWheels = 18;
		public const byte ReadCreateOverwriteMask = 0b0010_1100;
	}

Strings intended for logging or tracing are exempt from this rule. Literals are allowed when their meaning is clear from the context, and not subject to future changes, For example:

	mean = (a + b) / 2; // okay
	WaitMilliseconds(waitTimeInSeconds * 1000); // clear enough

If the value of one constant depends on the value of another, attempt to make this explicit in the code.

	public class SomeSpecialContainer
	{
		public const int MaxItems = 32;
		public const int HighWaterMark = 3 * MaxItems / 4; // at 75%
	}

**Note:** An enumeration can often be used for certain types of symbolic constants.

## AV1520 — Only use `var` when the type is evident [Must]

Use `var` for anonymous types (typically resulting from a LINQ query), or if the type is [evident](https://www.jetbrains.com/help/resharper/2021.3/Using_var_Keyword_in_Declarations.html#use-var-when-evident-details).
Never use `var` for [built-in types](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/built-in-types).

	// Projection into anonymous type.
	var largeOrders =
		from order in dbContext.Orders
		where order.Items.Count > 10 && order.TotalAmount > 1000
		select new { order.Id, order.TotalAmount };

	// Built-in types.
	bool isValid = true;
	string phoneNumber = "(unavailable)";
	uint pageSize = Math.Max(itemCount, MaxPageSize);

	// Types are evident.
	var customer = new Customer();
	var invoice = Invoice.Create(customer.Id);
	var user = sessionCache.Resolve<User>("john.doe@mail.com");
	var subscribers = new List<Subscriber>();
	var summary = shoppingBasket.ToOrderSummary();

	// All other cases.
	IQueryable<Order> recentOrders = ApplyFilter(order => order.CreatedAt > DateTime.Now.AddDays(-30));
	LoggerMessage message = Compose(context);
	ReadOnlySpan<char> key = ExtractKeyFromPair("email=john.doe@mail.com");
	IDictionary<Category, Product> productsPerCategory =
		shoppingBasket.Products.ToDictionary(product => product.Category);

## AV1521 — Declare and initialize variables as late as possible [Should]

Avoid the C and Visual Basic styles where all variables have to be defined at the beginning of a block, but rather define and initialize each variable at the point where it is needed.

## AV1522 — Assign each variable in a separate statement [Must]

Don't use confusing constructs like the one below:

	var result = someField = GetSomeMethod();

**Exception:** Multiple assignments per statement are allowed by using out variables, is-patterns or deconstruction into tuples. Examples:

	bool success = int.TryParse(text, out int result);

	if ((items[0] is string text) || (items[1] is Action action))
	{
	}

	(string name, string value) = SplitNameValuePair(text);

## AV1523 — Favor object and collection initializers over separate statements [Should]

Instead of:

	var startInfo = new ProcessStartInfo("myapp.exe");
	startInfo.StandardOutput = Console.Output;
	startInfo.UseShellExecute = true;

	var countries = new List();
	countries.Add("Netherlands");
	countries.Add("United States");

	var countryLookupTable = new Dictionary<string, string>();
	countryLookupTable.Add("NL", "Netherlands");
	countryLookupTable.Add("US", "United States");

Use [Object and Collection Initializers](http://msdn.microsoft.com/en-us/library/bb384062.aspx):

	var startInfo = new ProcessStartInfo("myapp.exe")
	{
		StandardOutput = Console.Output,
		UseShellExecute = true
	};

	var countries = new List { "Netherlands", "United States" };

	var countryLookupTable = new Dictionary<string, string>
	{
		["NL"] = "Netherlands",
		["US"] = "United States"
	};

## AV1525 — Don't make explicit comparisons to `true` or `false` [Must]

It is usually bad style to compare a `bool`-type expression to `true` or `false`. For example:

	while (condition == false) // wrong; bad style
	while (condition != true) // also wrong
	while (((condition == true) == true) == true) // where do you stop?
	while (condition) // OK

## AV1530 — Don't change a loop variable inside a `for` loop [Should]

Updating the loop variable within the loop body is generally considered confusing, even more so if the loop variable is modified in more than one place.

	for (int index = 0; index < 10; ++index)
	{
		if (someCondition)
		{
			index = 11; // Wrong! Use 'break' or 'continue' instead.
		}
	}

## AV1532 — Avoid nested loops [Should]

A method that nests loops is more difficult to understand than one with only a single loop. In fact, in most cases nested loops can be replaced with a much simpler LINQ query that uses the `from` keyword twice or more to *join* the data.

## AV1535 — Always add a block after the keywords `if`, `else`, `do`, `while`, `for`, `foreach` and `case` [Should]

Please note that this also avoids possible confusion in statements of the form:

	if (isActive) if (isVisible) Foo(); else Bar(); // which 'if' goes with the 'else'?

	// The right way:
	if (isActive)
	{
		if (isVisible)
		{
			Foo();
		}
		else
		{
			Bar();
		}
	}

## AV1536 — Always add a `default` block after the last `case` in a `switch` statement [Must]

Add a descriptive comment if the `default` block is supposed to be empty. Moreover, if that block is not supposed to be reached throw an `InvalidOperationException` to detect future changes that may fall through the existing cases. This ensures better code, because all paths the code can travel have been thought about.

	void Foo(string answer)
	{
		switch (answer)
		{
			case "no":
			{
			  Console.WriteLine("You answered with No");
			  break;
			}

			case "yes":
			{
			  Console.WriteLine("You answered with Yes");
			  break;
			}

			default:
			{
			  // Not supposed to end up here.
			  throw new InvalidOperationException("Unexpected answer " + answer);
			}
		}
	}

## AV1537 — Finish every `if`-`else`-`if` statement with an `else` clause [Should]

For example:

	void Foo(string answer)
	{
		if (answer == "no")
		{
			Console.WriteLine("You answered with No");
		}
		else if (answer == "yes")
		{
			Console.WriteLine("You answered with Yes");
		}
		else
		{
			// What should happen when this point is reached? Ignored? If not,
			// throw an InvalidOperationException.
		}
	}

## AV1540 — Be reluctant with multiple `return` statements [Should]

One entry, one exit is a sound principle and keeps control flow readable. However, if the method body is very small and complies with guideline  then multiple return statements may actually improve readability over some central boolean flag that is updated at various points.

## AV1545 — Don't use an `if`-`else` construct instead of a simple (conditional) assignment [Should]

Express your intentions directly. For example, rather than:

	bool isPositive;

	if (value > 0)
	{
		isPositive = true;
	}
	else
	{
		isPositive = false;
	}

write:

	bool isPositive = value > 0;

Or instead of:

	string classification;

	if (value > 0)
	{
		classification = "positive";
	}
	else
	{
		classification = "negative";
	}

	return classification;

write:

	return value > 0 ? "positive" : "negative";

Or instead of:

	int result;

	if (offset == null)
	{
		result = -1;
	}
	else
	{
		result = offset.Value;
	}

	return result;

write:

	return offset ?? -1;

Or instead of:

	private DateTime? firstJobStartedAt;

	public void RunJob()
	{
		if (firstJobStartedAt == null)
		{
			firstJobStartedAt = DateTime.UtcNow;
		}
	}

write:

	private DateTime? firstJobStartedAt;

	public void RunJob()
	{
		firstJobStartedAt ??= DateTime.UtcNow;
	}

Or instead of:

	if (employee.Manager != null)
	{
		return employee.Manager.Name;
	}
	else
	{
		return null;
	}

write:

	return employee.Manager?.Name;

## AV1546 — Prefer interpolated strings over concatenation or `string.Format`. [Must]

Since .NET 6, interpolated strings are optimized at compile-time, which inlines constants and reduces memory allocations due to boxing and string copying.

	// GOOD
	string result = $"Welcome, {firstName} {lastName}!";

	// BAD
	string result = string.Format("Welcome, {0} {1}!", firstName, lastName);

	// BAD
	string result = "Welcome, " + firstName + " " + lastName + "!";

	// BAD
	string result = string.Concat("Welcome, ", firstName, " ", lastName, "!");

## AV1547 — Encapsulate complex expressions in a property, method or local function [Must]

Consider the following example:

	if (member.HidesBaseClassMember && member.NodeType != NodeType.InstanceInitializer)
	{
		// do something
	}

In order to understand what this expression is about, you need to analyze its exact details and all of its possible outcomes. Obviously, you can add an explanatory comment on top of it, but it is much better to replace this complex expression with a clearly named method:

	if (NonConstructorMemberUsesNewKeyword(member))
	{
		// do something
	}

	private bool NonConstructorMemberUsesNewKeyword(Member member)
	{
		return member.HidesBaseClassMember &&
			member.NodeType != NodeType.InstanceInitializer;
	}

You still need to understand the expression if you are modifying it, but the calling code is now much easier to grasp.

## AV1551 — Call the more overloaded method from other overloads [Should]

This guideline only applies to overloads that are intended to provide optional arguments. Consider, for example, the following code snippet:

	public class MyString
	{
		private string someText;

		public int IndexOf(string phrase)
		{
			return IndexOf(phrase, 0);
		}

		public int IndexOf(string phrase, int startIndex)
		{
			return IndexOf(phrase, startIndex, someText.Length - startIndex);
		}

		public virtual int IndexOf(string phrase, int startIndex, int count)
		{
			return someText.IndexOf(phrase, startIndex, count);
		}
	}

The class `MyString` provides three overloads for the `IndexOf` method, but two of them simply call the one with one more parameter. Notice that the same rule applies to class constructors; implement the most complete overload and call that one from the other overloads using the `this()` operator. Also notice that the parameters with the same name should appear in the same position in all overloads.

**Important:** If you also want to allow derived classes to override these methods, define the most complete overload as a non-private `virtual` method that is called by all overloads.

## AV1553 — Only use optional parameters to replace overloads [Must]

The only valid reason for using C# 4.0's optional parameters is to replace the example from rule  with a single method like:

	public virtual int IndexOf(string phrase, int startIndex = 0, int count = -1)
	{
		int length = count == -1 ? someText.Length - startIndex : count;
		return someText.IndexOf(phrase, startIndex, length);
	}

Since strings, collections and tasks should never be `null` according to rule , if you have an optional parameter of these types with default value `null` then you must use overloaded methods instead.

Strings, unlike other reference types, can have non-null default values. So an optional string parameter may be used to replace overloads with the condition of having a non-null default value.

Regardless of optional parameters' types, following caveats always apply:

1) The default values of the optional parameters are stored at the caller side. As such, changing the default argument without recompiling the calling code will not apply the new default value. Unless your method is private or internal, this aspect should be carefully considered before choosing optional parameters over method overloads.

2) If optional parameters cause the method to follow and/or exit from alternative paths, overloaded methods are probably a better fit for your case.

## AV1554 — Do not use optional parameters in interface methods or their concrete implementations [Must]

When an interface method defines an optional parameter, its default value is discarded during overload resolution unless you call the concrete class through the interface reference.

When a concrete implementation of an interface method sets a default argument for a parameter, the default value is discarded during overload resolution if you call the concrete class through the interface reference.

See the series on optional argument corner cases by Eric Lippert (part [one](https://docs.microsoft.com/en-us/archive/blogs/ericlippert/optional-argument-corner-cases-part-one), [two](https://docs.microsoft.com/en-us/archive/blogs/ericlippert/optional-argument-corner-cases-part-two), [three](https://docs.microsoft.com/en-us/archive/blogs/ericlippert/optional-argument-corner-cases-part-three), [four](https://docs.microsoft.com/en-us/archive/blogs/ericlippert/optional-argument-corner-cases-part-four)) for more details.

## AV1555 — Avoid using named arguments [Must]

C# 4.0's named arguments have been introduced to make it easier to call COM components that are known for offering many optional parameters. If you need named arguments to improve the readability of the call to a method, that method is probably doing too much and should be refactored.

**Exception:** The only exception where named arguments improve readability is when calling a method of some code base you don't control that has a `bool` parameter, like this:

	object[] myAttributes = type.GetCustomAttributes(typeof(MyAttribute), inherit: false);

## AV1561 — Don't declare signatures with more than 3 parameters [Must]

To keep constructors, methods, delegates and local functions small and focused, do not use more than three parameters. Do not use tuple parameters. Do not return tuples with more than two elements.

If you want to use more parameters, use a structure or class to pass multiple arguments, as explained in the [Specification design pattern](http://en.wikipedia.org/wiki/Specification_pattern).
In general, the fewer the parameters, the easier it is to understand the method. Additionally, unit testing a method with many parameters requires many scenarios to test.

**Exception:** A parameter that is a collection of tuples is allowed.

## AV1562 — Don't use `ref` or `out` parameters [Must]

They make code less understandable and might cause people to introduce bugs. Instead, return compound objects or tuples.

**Exception:** Calling and declaring members that implement the [TryParse](https://docs.microsoft.com/en-us/dotnet/api/system.int32.tryparse) pattern is allowed. For example:

	bool success = int.TryParse(text, out int number);

## AV1564 — Avoid signatures that take a `bool` parameter [Should]

Consider the following method signature:

	public Customer CreateCustomer(bool platinumLevel)
	{
	}

On first sight this signature seems perfectly fine, but when calling this method you will lose this purpose completely:

	Customer customer = CreateCustomer(true);

Often, a method taking such a bool is doing more than one thing and needs to be refactored into two or more methods. An alternative solution is to replace the bool with an enumeration.

## AV1568 — Don't use parameters as temporary variables [May]

Never use a parameter as a convenient variable for storing temporary state. Even though the type of your temporary variable may be the same, the name usually does not reflect the purpose of the temporary variable.

## AV1570 — Prefer `is` patterns over `as` operations [Must]

If you use 'as' to safely upcast an interface reference to a certain type, always verify that the operation does not return `null`. Failure to do so may cause a `NullReferenceException` at a later stage if the object did not implement that interface.
Pattern matching syntax prevents this and improves readability. For example, instead of:

	var remoteUser = user as RemoteUser;
	if (remoteUser != null)
	{
	}

write:

	if (user is RemoteUser remoteUser)
	{
	}

## AV1575 — Don't comment out code [Must]

Never check in code that is commented out. Instead, use a work item tracking system to keep track of some work to be done. Nobody knows what to do when they encounter a block of commented-out code. Was it temporarily disabled for testing purposes? Was it copied as an example? Should I delete it?

## AV1580 — Write code that is easy to debug [Should]

Because debugger breakpoints cannot be set inside expressions, avoid overuse of nested method calls. For example, a line like:

	string result = ConvertToXml(ApplyTransforms(ExecuteQuery(GetConfigurationSettings(source))));

requires extra steps to inspect intermediate method return values. On the other hard, were this expression broken into intermediate variables, setting a breakpoint on one of them would be sufficient.

**Note** This does not apply to chaining method calls, which is a common pattern in fluent APIs.

