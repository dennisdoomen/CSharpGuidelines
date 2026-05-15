<!--
NOTE: Requires Markdown Extra. See http://michelf.ca/projects/php-markdown/extra/
 -->
<link href="style.css" type="text/css" rel="stylesheet"/>

<table width="100%">
<tr>
<td class="title" width="70%">Coding Guidelines for C# v14 Cheat Sheet</td>
<td rowspan="2" style="text-align:right">![logo](assets/images/logo.svg)</td>
</tr>
<tr>
<td><div class="subTitle">General, Design & Maintainability</div></td>
</tr>
</table>

<table width="100%">
<tr>
<td class="column" markdown="1">
<div markdown="1" class="sidebar">
**Core principles**

* Understand the boundaries of your codebase
* Prefer composition over inheritance
* Apply the Principle of Least Surprise
* Keep It Simple Stupid
* You Ain't Gonna Need It
* Don't Repeat Yourself
* Treat AI-generated code as your own
</div>

**General**

* Understand the boundaries of your codebase ({{ site.default_rule_prefix }}0100)
* Prefer composition over inheritance ({{ site.default_rule_prefix }}0110)
* Apply the Principle of Least Surprise ({{ site.default_rule_prefix }}0112)
* Keep It Simple Stupid (KISS) ({{ site.default_rule_prefix }}0115)
* You Ain't Gonna Need It (YAGNI) ({{ site.default_rule_prefix }}0120)
* Don't Repeat Yourself (DRY), but only within boundaries ({{ site.default_rule_prefix }}0125)
* Apply the four pillars of object-oriented programming ({{ site.default_rule_prefix }}0130)
* Treat AI-generated code as your own ({{ site.default_rule_prefix }}0135)

<br/>
**Class Design**

* A class or interface should have a single purpose ({{ site.default_rule_prefix }}1000)
* Only include constructor parameters that most or all members need ({{ site.default_rule_prefix }}1002)
* Use an interface rather than a base class to support multiple implementations ({{ site.default_rule_prefix }}1004)
* Use an interface to decouple classes from each other ({{ site.default_rule_prefix }}1005)
* It should be possible to treat a derived type as if it were a base type ({{ site.default_rule_prefix }}1011)
* Avoid bidirectional dependencies ({{ site.default_rule_prefix }}1020)
* Classes should protect the consistency of their internal state ({{ site.default_rule_prefix }}1026)
* Know when to use a record and when to use a class ({{ site.default_rule_prefix }}1030)
* Consider a named delegate instead of an interface with a single method ({{ site.default_rule_prefix }}1032)
</td>
<td class="column">
**Member Design**

* Allow properties to be set in any order ({{ site.default_rule_prefix }}1100)
* Don't use mutually exclusive properties ({{ site.default_rule_prefix }}1110)
* A property, method or local function should do only one thing ({{ site.default_rule_prefix }}1115)
* Don't hide dependencies behind static members ({{ site.default_rule_prefix }}1125)
* Return interfaces to unchangeable collections ({{ site.default_rule_prefix }}1130)
* Define parameters as specific and narrow as possible ({{ site.default_rule_prefix }}1137)
* Consider creating domain-specific types rather than using primitives ({{ site.default_rule_prefix }}1140)
* Use extension members to add behavior without modifying the original type ({{ site.default_rule_prefix }}1145)

<br/>
**Miscellaneous Design**

* Throw exceptions rather than returning some kind of status value ({{ site.default_rule_prefix }}1200)
* Throw the most specific exception that is appropriate ({{ site.default_rule_prefix }}1205)
* Don't swallow errors by catching generic exceptions ({{ site.default_rule_prefix }}1210)
* Properly handle exceptions in asynchronous code ({{ site.default_rule_prefix }}1215)
* Use a protected virtual method to raise each event ({{ site.default_rule_prefix }}1225)
* Materialize the result of a LINQ expression before returning it ({{ site.default_rule_prefix }}1250)
* Do not use `this` and `base` prefixes unless it is required ({{ site.default_rule_prefix }}1251)
</td>
<td class="column">
**Maintainability**

* Methods should not exceed 15 statements ({{ site.default_rule_prefix }}1500)
* Make all members `private` and types `internal sealed` by default ({{ site.default_rule_prefix }}1501)
* Don't use "magic" numbers ({{ site.default_rule_prefix }}1515)
* Only use `var` when the type is evident ({{ site.default_rule_prefix }}1520)
* Favor object and collection initializers over separate statements ({{ site.default_rule_prefix }}1523)
* Always add a block after the keywords `if`, `else`, `do`, `while`, `for`, `foreach` and `case` ({{ site.default_rule_prefix }}1535)
* Use concise conditional expressions instead of `if`-`else` blocks ({{ site.default_rule_prefix }}1545)
* Prefer interpolated strings over concatenation or `string.Format` ({{ site.default_rule_prefix }}1546)
* Don't use `ref` or `out` parameters ({{ site.default_rule_prefix }}1562)
* Avoid signatures that take a `bool` parameter ({{ site.default_rule_prefix }}1564)
* Align projects and folders with deployment units, not architectural layers ({{ site.default_rule_prefix }}1578)
* Make properties required when they must be set during initialization ({{ site.default_rule_prefix }}1585)

<br/>
**Performance**

* Consider using `Any()` to determine whether an `IEnumerable<T>` is empty ({{ site.default_rule_prefix }}1800)
* Only use `async`/`await` for I/O-bound or long-running activities ({{ site.default_rule_prefix }}1820)
* Prefer `Task.Run` for CPU-intensive activities ({{ site.default_rule_prefix }}1825)
* Beware of `async`/`await` deadlocks in UI frameworks ({{ site.default_rule_prefix }}1835)
</td>
</tr>
</table>

<table width="100%" class="footer">
<tr>
<td>
  Dennis Doomen
  Version %semver% (%commitdate%)
</td>
<td markdown="1" style="text-align:right">
  [www.csharpcodingguidelines.com](http://www.csharpcodingguidelines.com)
  [www.dennisdoomen.com](https://www.dennisdoomen.com)
  [www.avivasolutions.nl](http://www.avivasolutions.nl)
</td>
</tr>
</table>

<table width="100%" style="page-break-before: always;">
<tr>
<td class="title" width="70%">Coding Guidelines for C# v14 Cheat Sheet</td>
<td markdown="1" rowspan="2" style="text-align:right">![logo](assets/images/logo.svg)</td>
</tr>
<tr>
<td><div class="subTitle">Testability, Naming & Style</div></td>
</tr>
</table>

<table width="100%">
<tr>
<td class="column" markdown="1">
<div class="sidebar">

| **Symbol kind**                       | **Example**                                                 |
|:--------------------------------------|-------------------------------------------------------------|
| Namespace                             | `System.Drawing`                                            |
| Type parameter                        | `TView`                                                     |
| Interface                             | `IBusinessService`                                          |
| Class, struct                         | `AppDomain`                                                 |
| Enum                                  | `ErrorLevel`                                                |
| Enum member                           | `FatalError`                                                |
| Resource key                          | `SaveButtonTooltipText`                                     |
| Constant field                        | `MaximumItems`                                              |
| Private static readonly field         | `RedValue`                                                  |
| Non-private field                     | `MainPanel`                                                 |
| Property                              | `BackColor`                                                 |
| Event                                 | `Click`                                                     |
| Method                                | `ToString`                                                  |
| Local function                        | `FormatText`                                                |
| Private field                         | `listItem`                                                  |
| Parameter                             | `typeName`                                                  |
| Local variable                        | `listOfValues`                                              |

</div>

<br/>
**Testability**

* Use short concise functional test names ({{ site.default_rule_prefix }}1600)
* Test observable behavior, not private implementation details ({{ site.default_rule_prefix }}1605)
* Show what's important in a test, hide what's not ({{ site.default_rule_prefix }}1608)
* Use Test Data Builders or Object Mothers to construct test objects ({{ site.default_rule_prefix }}1610)
* Don't use production code in test assertions ({{ site.default_rule_prefix }}1618)
* Test reusable components separately from their consumers ({{ site.default_rule_prefix }}1620)
* Test concrete implementations as part of a larger integration scope ({{ site.default_rule_prefix }}1622)
</td>
<td class="column">
**Naming**

* Use US English ({{ site.default_rule_prefix }}1701)
* Use proper casing for language elements ({{ site.default_rule_prefix }}1702)
* Don't prefix fields ({{ site.default_rule_prefix }}1705)
* Don't use abbreviations ({{ site.default_rule_prefix }}1706)
* Name members, parameters and variables according to their meaning and not their type ({{ site.default_rule_prefix }}1707)
* Name generic type parameters with descriptive names ({{ site.default_rule_prefix }}1709)
* Properly name properties ({{ site.default_rule_prefix }}1715)
* Name methods and local functions using verbs or verb-object pairs ({{ site.default_rule_prefix }}1720)
* Use a verb or verb phrase to name an event ({{ site.default_rule_prefix }}1735)
* Use an underscore for irrelevant lambda parameters ({{ site.default_rule_prefix }}1739)
* Only suffix methods with `Async` or `TaskAsync` when both synchronous and asynchronous variants exist ({{ site.default_rule_prefix }}1755)

<br/>
**Documentation**

* Write comments and documentation in US English ({{ site.default_rule_prefix }}2301)
* Document all `public`, `protected` and `internal` types and members ({{ site.default_rule_prefix }}2305)
* Write XML documentation with other developers in mind ({{ site.default_rule_prefix }}2306)
* Document the purpose of a member, instead of describing its implementation ({{ site.default_rule_prefix }}2308)
* Avoid inline comments ({{ site.default_rule_prefix }}2310)
* Only write comments to explain complex algorithms or decisions ({{ site.default_rule_prefix }}2316)
</td>
<td class="column">
**Framework**

* Prefer idiomatic C# over .NET Framework APIs ({{ site.default_rule_prefix }}2202)
* Build with the highest warning level ({{ site.default_rule_prefix }}2210)
* Avoid LINQ query syntax for simple expressions ({{ site.default_rule_prefix }}2220)
* Use deconstruction to simplify variable assignments ({{ site.default_rule_prefix }}2225)
* Only use the `dynamic` keyword when talking to a dynamic object ({{ site.default_rule_prefix }}2230)

<br/>
**Layout**

* Use a common layout ({{ site.default_rule_prefix }}2400)
* Order and group namespaces according to the company ({{ site.default_rule_prefix }}2402)
* Place members in a well-defined order ({{ site.default_rule_prefix }}2406)
* Do not use `#region` ({{ site.default_rule_prefix }}2407)
* Use expression-bodied members appropriately ({{ site.default_rule_prefix }}2410)
* Indent 4 spaces, don't use tabs
* Add spaces around operators and after keywords such as `if`
* Keep expression-bodied members on one line when they remain readable

<div markdown="1" class="sidebar">
**Empty lines**

* Between multi-line statements
* Between multi-line members
* After the closing curly brace
* Between unrelated code blocks
* Between the `using` statements of different root namespaces
</div>

<div class="sidebar">
**Member order**

1. Private fields and constants
2. Public constants
3. Public static readonly fields
4. Factory methods
5. Constructors and the finalizer
6. Events
7. Public properties
8. Other methods and private properties in calling order
</div>
</td>
</tr>
</table>

<table width="100%" class="footer">
<tr>
 <td>
   Dennis Doomen
   Version %semver% (%commitdate%)
 </td>
 <td markdown="1" style="text-align:right">
  [www.csharpcodingguidelines.com](http://www.csharpcodingguidelines.com)
  [www.dennisdoomen.com](https://www.dennisdoomen.com)
  [www.avivasolutions.nl](http://www.avivasolutions.nl)
  </td>
</tr>
</table>
