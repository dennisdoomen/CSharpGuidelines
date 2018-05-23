---
title: Maintainability Guidelines
permalink: /maintainability-guidelines/
classes: wide
sidebar:
  nav: "sidebar"
---

### <a name="av1500"></a> Methods should not exceed 7 statements (AV1500) ![](/assets/images/1.png)
A method that requires more than 7 statements is simply doing too much or has too many responsibilities. It also requires the human mind to analyze the exact statements to understand what the code is doing. Break it down into multiple small and focused methods with self-explaining names, but make sure the high-level algorithm is still clear.

### <a name="av1501"></a> Make all members `private` and types `internal sealed` by default (AV1501) ![](/assets/images/1.png)
To make a more conscious decision on which members to make available to other classes, first restrict the scope as much as possible. Then carefully decide what to expose as a public member or type.

### <a name="av1502"></a> Avoid conditions with double negatives (AV1502) ![](/assets/images/2.png)
Although a property like `customer.HasNoOrders` makes sense, avoid using it in a negative condition like this:

	bool hasOrders = !customer.HasNoOrders;

Double negatives are more difficult to grasp than simple expressions, and people tend to read over the double negative easily.

### <a name="av1505"></a> Name assemblies after their contained namespace (AV1505) ![](/assets/images/3.png)
All DLLs should be named according to the pattern *Company*.*Component*.dll where *Company* refers to your company's name and *Component* contains one or more dot-separated clauses. For example `AvivaSolutions.Web.Controls.dll`.

As an example, consider a group of classes organized under the namespace `AvivaSolutions.Web.Binding` exposed by a certain assembly. According to this guideline, that assembly should be called `AvivaSolutions.Web.Binding.dll`. 

**Exception:** If you decide to combine classes from multiple unrelated namespaces into one assembly, consider suffixing the assembly name with `Core`, but do not use that suffix in the namespaces. For instance, `AvivaSolutions.Consulting.Core.dll`.

### <a name="av1506"></a> Name a source file to the type it contains (AV1506) ![](/assets/images/3.png)
Use Pascal casing to name the file and don't use underscores. Don't include (the number of) generic type parameters in the file name.

### <a name="av1507"></a> Limit the contents of a source code file to one type (AV1507) ![](/assets/images/3.png)
**Exception:** Nested types should be part of the same file.

**Exception:** Types that only differ by their number of generic type parameters should be part of the same file.

### <a name="av1508"></a> Name a source file to the logical function of the partial type (AV1508) ![](/assets/images/3.png)
When using partial types and allocating a part per file, name each file after the logical part that part plays. For example:

	// In MyClass.cs
	public partial class MyClass
	{...}
	
	// In MyClass.Designer.cs	
	public partial class MyClass
	{...}

### <a name="av1510"></a> Use `using` statements instead of fully qualified type names (AV1510) ![](/assets/images/3.png)
Limit usage of fully qualified type names to prevent name clashing. For example, don't do this:

	var list = new System.Collections.Generic.List<string>();

Instead, do this:

	using System.Collections.Generic;
	
	var list = new List<string>();

If you do need to prevent name clashing, use a `using` directive to assign an alias:

	using Label = System.Web.UI.WebControls.Label;

### <a name="av1515"></a> Don't use "magic" numbers (AV1515) ![](/assets/images/1.png)
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

### <a name="av1520"></a> Only use `var` when the type is very obvious (AV1520) ![](/assets/images/1.png)
Only use `var` as the result of a LINQ query, or if the type is very obvious from the same statement and using it would improve readability. So don't

	var item = 3;                              // what type? int? uint? float?
	var myfoo = MyFactoryMethod.Create("arg"); // Not obvious what base-class or			
	                                           // interface to expect. Also
	                                           // difficult to refactor if you can't
	                                           // search for the class

Instead, use `var` like this:

	var query = from order in orders where order.Items > 10 and order.TotalValue > 1000;
	var repository = new RepositoryFactory.Get();	
	var list = new ReadOnlyCollection();

In all of three above examples it is clear what type to expect. For a more detailed rationale about the advantages and disadvantages of using `var`, read Eric Lippert's [Uses and misuses of implicit typing](http://blogs.msdn.com/b/ericlippert/archive/2011/04/20/uses-and-misuses-of-implicit-typing.aspx).

### <a name="av1521"></a> Declare and initialize variables as late as possible (AV1521) ![](/assets/images/2.png)
Avoid the C and Visual Basic styles where all variables have to be defined at the beginning of a block, but rather define and initialize each variable at the point where it is needed.

### <a name="av1522"></a> Assign each variable in a separate statement (AV1522) ![](/assets/images/1.png)
Don't use confusing constructs like the one below:

	var result = someField = GetSomeMethod();

**Exception:** Multiple assignments per statement are allowed by using out variables, is-patterns or deconstruction into tuples. Examples:

	bool success = int.TryParse(text, out int result);
	
	if ((items[0] is string text) || (items[1] is Action action))
	{
	}

	(string name, string value) = SplitNameValuePair(text);

### <a name="av1523"></a> Favor object and collection initializers over separate statements (AV1523) ![](/assets/images/2.png)
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

### <a name="av1525"></a> Don't make explicit comparisons to `true` or `false` (AV1525) ![](/assets/images/1.png)

It is usually bad style to compare a `bool`-type expression to `true` or `false`. For example:

	while (condition == false) // wrong; bad style  
	while (condition != true) // also wrong  
	while (((condition == true) == true) == true) // where do you stop?  
	while (condition) // OK

### <a name="av1530"></a> Don't change a loop variable inside a `for` loop (AV1530) ![](/assets/images/2.png)
Updating the loop variable within the loop body is generally considered confusing, even more so if the loop variable is modified in more than one place.

	for (int index = 0; index < 10; ++index)  
	{  
		if (someCondition)
		{
			index = 11; // Wrong! Use 'break' or 'continue' instead.  
		}
	}

### <a name="av1532"></a> Avoid nested loops (AV1532) ![](/assets/images/2.png)
A method that nests loops is more difficult to understand than one with only a single loop. In fact, in most cases nested loops can be replaced with a much simpler LINQ query that uses the `from` keyword twice or more to *join* the data.

### <a name="av1535"></a> Always add a block after the keywords `if`, `else`, `do`, `while`, `for`, `foreach` and `case` (AV1535) ![](/assets/images/2.png)
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

### <a name="av1536"></a> Always add a `default` block after the last `case` in a `switch` statement (AV1536) ![](/assets/images/1.png)
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

### <a name="av1537"></a> Finish every `if`-`else`-`if` statement with an `else` clause (AV1537) ![](/assets/images/2.png)
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

### <a name="av1540"></a> Be reluctant with multiple `return` statements (AV1540) ![](/assets/images/2.png)
One entry, one exit is a sound principle and keeps control flow readable. However, if the method body is very small and complies with guideline AV1500 then multiple return statements may actually improve readability over some central boolean flag that is updated at various points.

### <a name="av1545"></a> Don't use an `if`-`else` construct instead of a simple (conditional) assignment (AV1545) ![](/assets/images/2.png)
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

	bool isPositive = (value > 0);

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

	return (value > 0) ? "positive" : "negative";

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

### <a name="av1547"></a> Encapsulate complex expressions in a property, method or local function (AV1547) ![](/assets/images/1.png)
Consider the following example:

	if (member.HidesBaseClassMember && (member.NodeType != NodeType.InstanceInitializer))
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
		return
			(member.HidesBaseClassMember &&
			(member.NodeType != NodeType.InstanceInitializer)  
	}

You still need to understand the expression if you are modifying it, but the calling code is now much easier to grasp.

### <a name="av1551"></a> Call the more overloaded method from other overloads (AV1551) ![](/assets/images/2.png)
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

### <a name="av1553"></a> Only use optional arguments to replace overloads (AV1553) ![](/assets/images/1.png)
The only valid reason for using C# 4.0's optional arguments is to replace the example from rule AV1551 with a single method like:

    public virtual int IndexOf(string phrase, int startIndex = 0, int count = -1)
    {
        int length = (count == -1) ? (someText.Length - startIndex) : count;
        return someText.IndexOf(phrase, startIndex, length);
    }

If the optional parameter is a reference type then it can only have a default value of `null`. But since strings, lists and collections should never be `null` according to rule AV1135, you must use overloaded methods instead.

**Note:** The default values of the optional parameters are stored at the caller side. As such, changing the default value without recompiling the calling code will not apply the new default value properly.

**Note:** When an interface method defines an optional parameter, its default value is not considered during overload resolution unless you call the concrete class through the interface reference. See [this post by Eric Lippert](http://blogs.msdn.com/b/ericlippert/archive/2011/05/09/optional-argument-corner-cases-part-one.aspx) for more details.

### <a name="av1555"></a> Avoid using named arguments (AV1555) ![](/assets/images/1.png)
C# 4.0's named arguments have been introduced to make it easier to call COM components that are known for offering many optional parameters. If you need named arguments to improve the readability of the call to a method, that method is probably doing too much and should be refactored.

**Exception:** The only exception where named arguments improve readability is when calling a method of some code base you don't control that has a `bool` parameter, like this:  

	object[] myAttributes = type.GetCustomAttributes(typeof(MyAttribute), inherit: false);

### <a name="av1561"></a> Don't declare signatures with more than 3 parameters (AV1561) ![](/assets/images/1.png)
To keep constructors, methods, delegates and local functions small and focused, do not use more than three parameters. Do not use tuple parameters. Do not return tuples with more than two elements.

If you want to use more parameters, use a structure or class to pass multiple arguments, as explained in the [Specification design pattern](http://en.wikipedia.org/wiki/Specification_pattern). 
In general, the fewer the parameters, the easier it is to understand the method. Additionally, unit testing a method with many parameters requires many scenarios to test.

**Exception:** A parameter that is a collection of tuples is allowed.

### <a name="av1562"></a> Don't use `ref` or `out` parameters (AV1562) ![](/assets/images/1.png)
They make code less understandable and might cause people to introduce bugs. Instead, return compound objects or tuples.

**Exception:** Calling and declaring members that implement the [TryParse](https://docs.microsoft.com/en-us/dotnet/api/system.int32.tryparse) pattern is allowed. For example:

	bool success = int.TryParse(text, out int number);

### <a name="av1564"></a> Avoid signatures that take a `bool` parameter (AV1564) ![](/assets/images/2.png)
Consider the following method signature:

	public Customer CreateCustomer(bool platinumLevel) {}

On first sight this signature seems perfectly fine, but when calling this method you will lose this purpose completely:

	Customer customer = CreateCustomer(true);

Often, a method taking such a bool is doing more than one thing and needs to be refactored into two or more methods. An alternative solution is to replace the bool with an enumeration.

### <a name="av1568"></a> Don't use parameters as temporary variables (AV1568) ![](/assets/images/3.png)
Never use a parameter as a convenient variable for storing temporary state. Even though the type of your temporary variable may be the same, the name usually does not reflect the purpose of the temporary variable.

### <a name="av1570"></a> Prefer `is` patterns over `as` operations (AV1570) ![](/assets/images/1.png)

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

### <a name="av1575"></a> Don't comment out code (AV1575) ![](/assets/images/1.png)

Never check in code that is commented out. Instead, use a work item tracking system to keep track of some work to be done. Nobody knows what to do when they encounter a block of commented-out code. Was it temporarily disabled for testing purposes? Was it copied as an example? Should I delete it?
