# Naming Conventions (AV1700) — Detailed Reference

## AV1701 — Use US English [Must]

All identifiers (such as types, type members, parameters and variables) should be named using words from the American English language.

- Choose easily readable, preferably grammatically correct names. For example, `HorizontalAlignment` is more readable than `AlignmentHorizontal`.
- Favor readability over brevity. The property name `CanScrollHorizontally` is better than `ScrollableX` (an obscure reference to the X-axis).
- Avoid using names that conflict with keywords of widely used programming languages.

## AV1702 — Use proper casing for language elements [Must]

| Language element | Casing| Example |
|:--------------------|:----------|:-----------|
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
| Tuple element names | Pascal | `(string First, string Last) name = ("John", "Doe");` <br/>`var name = (First: "John", Last: "Doe");` <br/>`(string First, string Last) GetName() => ("John", "Doe");` |
| Variables declared using tuple syntax | Camel | `(string first, string last) = ("John", "Doe");` <br/>`var (first, last) = ("John", "Doe");` <br/> |
| Local variable | Camel | `listOfValues` |

**Note:** in case of ambiguity, the rule higher in the table wins.

## AV1704 — Don't include numbers in variables, parameters and type members [May]

In most cases they are a lazy excuse for not defining a clear and intention-revealing name.

## AV1705 — Don't prefix fields [Must]

For example, don't use `g_` or `s_` to distinguish static from non-static fields. A method in which it is difficult to distinguish local variables from member fields is generally too big. Examples of incorrect identifier names are: `_currentUser`, `mUserName`, `m_loginTime`.

## AV1706 — Don't use abbreviations [Should]

For example, use `ButtonOnClick` rather than `BtnOnClick`. Avoid single character variable names, such as `i` or `q`. Use `index` or `query` instead.

**Exceptions:** Use well-known acronyms and abbreviations that are widely accepted or well-known in your work domain. For instance, use acronym `UI` instead of `UserInterface` and abbreviation `Id` instead of `Identity`.

## AV1707 — Name members, parameters and variables according to their meaning and not their type [Should]

- Use functional names. For example, `GetLength` is a better name than `GetInt`.
- Don't use terms like `Enum`, `Class` or `Struct` in a name.
- Identifiers that refer to a collection type should have plural names.
- Don't include the type in variable names, except to avoid clashes with other variables.

## AV1708 — Name types using nouns, noun phrases or adjective phrases [Should]

For example, the name IComponent uses a descriptive noun, ICustomAttributeProvider uses a noun phrase and IPersistable uses an adjective.
Bad examples include `SearchExamination` (a page to search for examinations), `Common` (does not end with a noun, and does not explain its purpose) and `SiteSecurity` (although the name is technically okay, it does not say anything about its purpose).

Don't include terms like `Utility` or `Helper` in classes. Classes with names like that are usually static classes and are introduced without considering object-oriented principles (see also ).

## AV1709 — Name generic type parameters with descriptive names [Should]

- Always prefix type parameter names with the letter `T`.
- Always use a descriptive name unless a single-letter name is completely self-explanatory and a longer name would not add value. Use the single letter `T` as the type parameter in that case.
- Consider indicating constraints placed on a type parameter in the name of the parameter. For example, a parameter constrained to `ISession` may be called `TSession`.

## AV1710 — Don't repeat the name of a class or enumeration in its members [Must]

class Employee
	{
		// Wrong!
		static GetEmployee() {...}
		DeleteEmployee() {...}

		// Right.
		static Get() {...}
		Delete() {...}

		// Also correct.
		AddNewJob() {...}
		RegisterForMeeting() {...}
	}

## AV1711 — Name members similarly to members of related .NET Framework classes [May]

.NET developers are already accustomed to the naming patterns the framework uses, so following this same pattern helps them find their way in your classes as well. For instance, if you define a class that behaves like a collection, provide members like `Add`, `Remove` and `Count` instead of `AddItem`, `Delete` or `NumberOfItems`.

## AV1712 — Avoid short names or names that can be mistaken for other names [Must]

Although technically correct, statements like the following can be confusing:

	bool b001 = lo == l0 ? I1 == 11 : lOl != 101;

## AV1715 — Properly name properties [Should]

- Name properties with nouns, noun phrases, or occasionally adjective phrases.
- Name boolean properties with an affirmative phrase. E.g. `CanSeek` instead of `CannotSeek`.
- Consider prefixing boolean properties with `Is`, `Has`, `Can`, `Allows`, or `Supports`.
- Consider giving a property the same name as its type. When you have a property that is strongly typed to an enumeration, the name of the property can be the same as the name of the enumeration. For example, if you have an enumeration named `CacheLevel`, a property that returns one of its values can also be named `CacheLevel`.

## AV1720 — Name methods and local functions using verbs or verb-object pairs [Should]

Name a method or local function using a verb like `Show` or a verb-object pair such as `ShowDialog`. A good name should give a hint on the *what* of a member, and if possible, the *why*.

Also, don't include `And` in the name of a method or local function. That implies that it is doing more than one thing, which violates the Single Responsibility Principle explained in .

## AV1725 — Name namespaces using names, layers, verbs and features [May]

For instance, the following namespaces are good examples of that guideline.

	AvivaSolutions.Commerce.Web
	NHibernate.Extensibility
	Microsoft.ServiceModel.WebApi
	Microsoft.VisualStudio.Debugging
	FluentAssertion.Primitives
	CaliburnMicro.Extensions

**Note:** Never allow namespaces to contain the name of a type, but a noun in its plural form (e.g. `Collections`) is usually OK.

## AV1735 — Use a verb or verb phrase to name an event [Should]

Name events with a verb or a verb phrase. For example: `Click`, `Deleted`, `Closing`, `Minimizing`, and `Arriving`. For example, the declaration of the `Search` event may look like this:

	public event EventHandler<SearchArgs> Search;

## AV1737 — Use `-ing` and `-ed` to express pre-events and post-events [May]

For example, a close event that is raised before a window is closed would be called `Closing`, and one that is raised after the window is closed would be called `Closed`. Don't use `Before` or `After` prefixes or suffixes to indicate pre and post events.

Suppose you want to define events related to the deletion of an object. Avoid defining the `Deleting` and `Deleted` events as `BeginDelete` and `EndDelete`. Define those events as follows:

- `Deleting`: Occurs just before the object is getting deleted.
- `Delete`: Occurs when the object needs to be deleted by the event handler.
- `Deleted`: Occurs when the object is already deleted.

## AV1738 — Prefix an event handler with "On" [May]

It is good practice to prefix the method that handles an event with "On". For example, a method that handles its own `Closing` event should be named `OnClosing`. And a method that handles the `Click` event of its `okButton` field should be named `OkButtonOnClick`.

## AV1739 — Use an underscore for irrelevant lambda parameters [May]

If you use a lambda expression (for instance, to subscribe to an event) and the actual parameters of the event are irrelevant, use the following convention to make that explicit:

	button.Click += (_, __) => HandleClick();

**Note** If using C# 9 or higher, use a single underscore (discard) for all unused lambda parameters and variables.

## AV1745 — Group extension methods in a class suffixed with Extensions [May]

If the name of an extension method conflicts with another member or extension method, you must prefix the call with the class name. Having them in a dedicated class with the `Extensions` suffix improves readability.

## AV1755 — Postfix asynchronous methods with `Async` or `TaskAsync` [Should]

The general convention for methods and local functions that return `Task` or `Task<TResult>` is to postfix them with `Async`. But if such a method already exists, use `TaskAsync` instead.

