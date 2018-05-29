---
title: Naming Guidelines
permalink: /naming-guidelines/
classes: wide
sidebar:
  nav: "sidebar"
---

### <a name="av1701"></a> Use US English (AV1701) ![](/assets/images/1.png)

All identifiers (such as types, type members, parameters and variables) should be named using words from the American English language.

- Choose easily readable, preferably grammatically correct names. For example, `HorizontalAlignment` is more readable than `AlignmentHorizontal`.
- Favor readability over brevity. The property name `CanScrollHorizontally` is better than `ScrollableX` (an obscure reference to the X-axis).
- Avoid using names that conflict with keywords of widely used programming languages.

### <a name="av1702"></a> Use proper casing for language elements (AV1702) ![](/assets/images/1.png) 

| Language element&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;|Casing&nbsp;&nbsp;&nbsp;&nbsp;|Example|
|--------------------|----------|:-----------
| Namespace | Pascal | `System.Drawing` |
| Type parameter | Pascal | `TView` |
| Interface | Pascal | `IBusinessService`
| Class, struct | Pascal | `AppDomain`
| Enum | Pascal | `ErrorLevel` |
| Enum member | Pascal | `FatalError` |
| Resource key | Pascal | `SaveButtonTooltipText` |
| Constant field | Pascal | `MaximumItems` |
| Private static readonly field | Pascal | `RedValue` |
| Private field | Camel | `listItem` |
| Non-private field | Pascal | `MainPanel` |
| Property | Pascal | `BackColor` |
| Event | Pascal | `Click` |
| Method | Pascal | `ToString` |
| Local function | Pascal | `FormatText` |
| Parameter | Camel | `typeName` |
| Tuple element names | Pascal | `(string First, string Last) name = ("John", "Doe");` |
| | | `var name = (First: "John", Last: "Doe");` |
| | | `(string First, string Last) GetName() => ("John", "Doe");` |
| Variables declared using tuple syntax | Camel | `(string first, string last) = ("John", "Doe");` |
| | | `var (first, last) = ("John", "Doe");` |
| Local variable | Camel | `listOfValues` |

**Note:** in case of ambiguity, the rule higher in the table wins.

### <a name="av1704"></a> Don't include numbers in variables, parameters and type members (AV1704) ![](/assets/images/3.png)
In most cases they are a lazy excuse for not defining a clear and intention-revealing name.

### <a name="av1705"></a> Don't prefix fields (AV1705) ![](/assets/images/1.png)

For example, don't use `g_` or `s_` to distinguish static from non-static fields. A method in which it is difficult to distinguish local variables from member fields is generally too big. Examples of incorrect identifier names are: `_currentUser`, `mUserName`, `m_loginTime`.

### <a name="av1706"></a> Don't use abbreviations (AV1706) ![](/assets/images/2.png)
For example, use `OnButtonClick` rather than `OnBtnClick`. Avoid single character variable names, such as `i` or `q`. Use `index` or `query` instead.

**Exceptions:** Use well-known acronyms and abbreviations that are widely accepted or well-known in your work domain. For instance, use acronym `UI` instead of `UserInterface` and abbreviation `Id` instead of `Identity`.

### <a name="av1707"></a> Name members, parameters and variables according to their meaning and not their type (AV1707) ![](/assets/images/2.png)
- Use functional names. For example, `GetLength` is a better name than `GetInt`.
- Don't use terms like `Enum`, `Class` or `Struct` in a name.
- Identifiers that refer to a collection type should have plural names.

### <a name="av1708"></a> Name types using nouns, noun phrases or adjective phrases (AV1708) ![](/assets/images/2.png)
For example, the name IComponent uses a descriptive noun, ICustomAttributeProvider uses a noun phrase and IPersistable uses an adjective.
Bad examples include `SearchExamination` (a page to search for examinations), `Common` (does not end with a noun, and does not explain its purpose) and `SiteSecurity` (although the name is technically okay, it does not say anything about its purpose).

Don't include terms like `Utility` or `Helper` in classes. Classes with names like that are usually static classes and are introduced without considering object-oriented principles (see also AV1008).

### <a name="av1709"></a> Name generic type parameters with descriptive names (AV1709) ![](/assets/images/2.png)
- Always prefix type parameter names with the letter `T`.
- Always use a descriptive name unless a single-letter name is completely self-explanatory and a longer name would not add value. Use the single letter `T` as the type parameter in that case.
- Consider indicating constraints placed on a type parameter in the name of the parameter. For example, a parameter constrained to `ISession` may be called `TSession`.

### <a name="av1710"></a> Don't repeat the name of a class or enumeration in its members (AV1710) ![](/assets/images/1.png)

	class Employee
	{
		// Wrong!
		static GetEmployee() {...}
		DeleteEmployee() {...}
		
		// Right
		static Get() {...}
		Delete() {...}
		
		// Also correct.
		AddNewJob() {...}
		RegisterForMeeting() {...}
	}

### <a name="av1711"></a> Name members similarly to members of related .NET Framework classes (AV1711) ![](/assets/images/3.png)

.NET developers are already accustomed to the naming patterns the framework uses, so following this same pattern helps them find their way in your classes as well. For instance, if you define a class that behaves like a collection, provide members like `Add`, `Remove` and `Count` instead of `AddItem`, `Delete` or `NumberOfItems`.

### <a name="av1712"></a> Avoid short names or names that can be mistaken for other names (AV1712) ![](/assets/images/1.png)
Although technically correct, statements like the following can be confusing:

	bool b001 = (lo == l0) ? (I1 == 11) : (lOl != 101);

### <a name="av1715"></a> Properly name properties (AV1715) ![](/assets/images/2.png)
- Name properties with nouns, noun phrases, or occasionally adjective phrases. 
- Name boolean properties with an affirmative phrase. E.g. `CanSeek` instead of `CannotSeek`.
- Consider prefixing boolean properties with `Is`, `Has`, `Can`, `Allows`, or `Supports`.
- Consider giving a property the same name as its type. When you have a property that is strongly typed to an enumeration, the name of the property can be the same as the name of the enumeration. For example, if you have an enumeration named `CacheLevel`, a property that returns one of its values can also be named `CacheLevel`.

### <a name="av1720"></a> Name methods and local functions using verbs or verb-object pairs (AV1720) ![](/assets/images/2.png)
Name a method or local function using a verb like `Show` or a verb-object pair such as `ShowDialog`. A good name should give a hint on the *what* of a member, and if possible, the *why*.

Also, don't include `And` in the name of a method or local function. That implies that it is doing more than one thing, which violates the Single Responsibility Principle explained in AV1115.

### <a name="av1725"></a> Name namespaces using names, layers, verbs and features (AV1725) ![](/assets/images/3.png)
For instance, the following namespaces are good examples of that guideline.

	AvivaSolutions.Commerce.Web
	NHibernate.Extensibility
	Microsoft.ServiceModel.WebApi
	Microsoft.VisualStudio.Debugging
	FluentAssertion.Primitives
	CaliburnMicro.Extensions

**Note:** Never allow namespaces to contain the name of a type, but a noun in its plural form (e.g. `Collections`) is usually OK.

### <a name="av1735"></a> Use a verb or verb phrase to name an event (AV1735) ![](/assets/images/2.png)
Name events with a verb or a verb phrase. For example: `Click`, `Deleted`, `Closing`, `Minimizing`, and `Arriving`. For example, the declaration of the `Search` event may look like this:

	public event EventHandler<SearchArgs> Search;

### <a name="av1737"></a> Use `-ing` and `-ed` to express pre-events and post-events (AV1737) ![](/assets/images/3.png)
For example, a close event that is raised before a window is closed would be called `Closing`, and one that is raised after the window is closed would be called `Closed`. Don't use `Before` or `After` prefixes or suffixes to indicate pre and post events.

Suppose you want to define events related to the deletion of an object. Avoid defining the `Deleting` and `Deleted` events as `BeginDelete` and `EndDelete`. Define those events as follows:

- `Deleting`: Occurs just before the object is getting deleted
- `Delete`: Occurs when the object needs to be deleted by the event handler.
- `Deleted`: Occurs when the object is already deleted.

### <a name="av1738"></a> Prefix an event handler with "On" (AV1738) ![](/assets/images/3.png)
It is good practice to prefix the method that handles an event with "On". For example, a method that handles the `Closing` event can be named `OnClosing`.

### <a name="av1739"></a> Use an underscore for irrelevant parameters (AV1739) ![](/assets/images/3.png)
If you use a lambda expression (for instance, to subscribe to an event) and the actual arguments of the event are irrelevant, use discards to make that explicit:

	button.Click += (_, _) => HandleClick();

### <a name="av1745"></a> Group extension methods in a class suffixed with Extensions (AV1745) ![](/assets/images/3.png)
If the name of an extension method conflicts with another member or extension method, you must prefix the call with the class name. Having them in a dedicated class with the `Extensions` suffix improves readability.

### <a name="av1755"></a> Postfix asynchronous methods with `Async` or `TaskAsync` (AV1755) ![](/assets/images/2.png)
The general convention for methods and local functions that return `Task` or `Task<TResult>` is to postfix them with `Async`. But if such a method already exists, use `TaskAsync` instead.
