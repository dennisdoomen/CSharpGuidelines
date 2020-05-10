<!--
NOTE: Requires Markdown Extra. See http://michelf.ca/projects/php-markdown/extra/
 -->
<link href="style.css" type="text/css" rel="stylesheet"/>

<table width="100%">
<tr>
<td class="title" width="70%">Coding Guidelines for C# 1.0 - 7.3 Cheat Sheet</td>
<td rowspan="2" style="text-align:right">![logo](assets/images/logo.png)</td>
</tr>
<tr>
<td><div class="subTitle">Design & Maintainability</div> (level 1 and 2 only)</td>
</tr>
</table>

<table width="100%">
<tr>
<td class="column" markdown="1">
<div markdown="1" class="sidebar">
**Basic Principles**

* The Principle of Least Surprise
* Keep It Simple Stupid
* You Ain't Gonna Need It
* Don't Repeat Yourself
* OOP: Encapsulation, abstraction, inheritance, polymorphism
</div>

**Class Design**

* A class or interface should have a single purpose ({{ site.default_rule_prefix }}1000)
* An interface should be small and focused ({{ site.default_rule_prefix }}1003)
* Use an interface to decouple classes from each other ({{ site.default_rule_prefix }}1005)
* Don't suppress compiler warnings using the `new` keyword ({{ site.default_rule_prefix }}1010)
* It should be possible to treat a derived object as if it were a base class object ({{ site.default_rule_prefix }}1011)
* Don't refer to derived classes from the base class ({{ site.default_rule_prefix }}1013)
* Avoid exposing the other objects an object depends on ({{ site.default_rule_prefix }}1014)
* Avoid bidirectional dependencies ({{ site.default_rule_prefix }}1020)
* Classes should have state and behavior ({{ site.default_rule_prefix }}1025)
* Classes should protect the consistency of their internal state ({{ site.default_rule_prefix }}1026)

<br/>
**Member Design**

* Allow properties to be set in any order ({{ site.default_rule_prefix }}1100)
* Don't use mutually exclusive properties ({{ site.default_rule_prefix }}1110)
* A property, method or local function should do only one thing ({{ site.default_rule_prefix }}1115)
* Don't expose stateful objects through static members ({{ site.default_rule_prefix }}1125)
* Return an `IEnumerable<T>` or `ICollection<T>` instead of a concrete collection class ({{ site.default_rule_prefix }}1130)
* Properties, arguments and return values representing strings, collections or tasks should never be `null` ({{ site.default_rule_prefix }}1135)
* Define parameters as specific as possible ({{ site.default_rule_prefix }}1137)
</td>
<td class="column">
**Miscellaneous Design**

* Throw exceptions rather than returning some kind of status value ({{ site.default_rule_prefix }}1200)
* Provide a rich and meaningful exception message text ({{ site.default_rule_prefix }}1202)
* Don't swallow errors by catching generic exceptions ({{ site.default_rule_prefix }}1210)
* Properly handle exceptions in asynchronous code ({{ site.default_rule_prefix }}1215)
* Always check an event handler delegate for `null` ({{ site.default_rule_prefix }}1220)
* Use a protected virtual method to raise each event ({{ site.default_rule_prefix }}1225)
* Don't pass `null` as the `sender` argument when raising an event ({{ site.default_rule_prefix }}1235)
* Use generic constraints if applicable ({{ site.default_rule_prefix }}1240)
* Evaluate the result of a LINQ expression before returning it ({{ site.default_rule_prefix }}1250)
* Do not use `this` and `base` prefixes unless it is required ({{ site.default_rule_prefix }}1251)

<br/>
**Maintainability**

* Methods should not exceed 7 statements ({{ site.default_rule_prefix }}1500)
* Make all members `private` and types `internal sealed` by default ({{ site.default_rule_prefix }}1501)
* Avoid conditions with double negatives ({{ site.default_rule_prefix }}1502)
* Don't use "magic" numbers ({{ site.default_rule_prefix }}1515)
* Only use `var` when the type is very obvious ({{ site.default_rule_prefix }}1520)
* Declare and initialize variables as late as possible ({{ site.default_rule_prefix }}1521)
* Assign each variable in a separate statement ({{ site.default_rule_prefix }}1522)
* Favor object and collection initializers over separate statements ({{ site.default_rule_prefix }}1523)
* Don't make explicit comparisons to `true` or `false` ({{ site.default_rule_prefix }}1525)
* Don't change a loop variable inside a `for` loop ({{ site.default_rule_prefix }}1530)
* Avoid nested loops ({{ site.default_rule_prefix }}1532)
</td>
<td class="column">
* Always add a block after the keywords `if`, `else`, `do`, `while`, `for`, `foreach` and `case` ({{ site.default_rule_prefix }}1535)
* Always add a `default` block after the last `case` in a `switch` statement ({{ site.default_rule_prefix }}1536)
* Finish every `if`-`else`-`if` statement with an `else` clause ({{ site.default_rule_prefix }}1537)
* Be reluctant with multiple `return` statements ({{ site.default_rule_prefix }}1540)
* Don't use an `if`-`else` construct instead of a simple (conditional) assignment ({{ site.default_rule_prefix }}1545)
* Encapsulate complex expressions in a property, method or local function ({{ site.default_rule_prefix }}1547)
* Call the more overloaded method from other overloads ({{ site.default_rule_prefix }}1551)
* Only use optional parameters to replace overloads ({{ site.default_rule_prefix }}1553)
* Do not use optional parameters in interface methods or their concrete implementations ({{ site.default_rule_prefix }}1554)
* Avoid using named arguments ({{ site.default_rule_prefix }}1555)
* Don't declare signatures with more than 3 parameters ({{ site.default_rule_prefix }}1561)
* Don't use `ref` or `out` parameters ({{ site.default_rule_prefix }}1562)
* Avoid signatures that take a `bool` flag ({{ site.default_rule_prefix }}1564)
* Prefer `is` patterns over `as` operations ({{ site.default_rule_prefix }}1570)
* Don't comment out code ({{ site.default_rule_prefix }}1575)

<br/>
**Framework Guidelines**

* Use C# type aliases instead of the types from the `System` namespace ({{ site.default_rule_prefix }}2201)
* Prefer language syntax over explicit calls to underlying implementations ({{ site.default_rule_prefix }}2202)
* Build with the highest warning level ({{ site.default_rule_prefix }}2210)
* Use lambda expressions instead of anonymous methods ({{ site.default_rule_prefix }}2221)
* Only use the `dynamic` keyword when talking to a dynamic object ({{ site.default_rule_prefix }}2230)
* Favor `async`/`await` over `Task` continuations ({{ site.default_rule_prefix }}2235)
</td>
<tr>

<table width="100%" class="footer">
<tr>
<td>
  Dennis Doomen
  Version %semver% (%commitdate%)
</td>
<td style="text-align:right">
  [www.csharpcodingguidelines.com](http://www.csharpcodingguidelines.com)
  [www.continuousimprover.com](www.continuousimprover.com)
  [www.avivasolutions.nl](http://www.avivasolutions.nl)
</td>
</tr>
</table>

<table width="100%" style="page-break-before: always;">
 <tr>
  <td class="title" width="70%">Coding Guidelines for C# 1.0 - 7.3 Cheat Sheet</td>
  <td markdown="1" rowspan="2" style="text-align:right">![logo](assets/images/logo.png)</td>
 </tr>
 <tr>
 <td><div class="subTitle">Naming & Layout</div> (level 1 and 2 only)</td>
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
**Naming**

* Use US English ({{ site.default_rule_prefix }}1701)
* Don't prefix fields ({{ site.default_rule_prefix }}1705)
* Don't use abbreviations ({{ site.default_rule_prefix }}1706)
* Name members, parameters and variables according to their meaning and not their type ({{ site.default_rule_prefix }}1707)
* Name types using nouns, noun phrases or adjective phrases ({{ site.default_rule_prefix }}1708)
* Name generic type parameters with descriptive names ({{ site.default_rule_prefix }}1709)
* Don't repeat the name of a class or enumeration in its members ({{ site.default_rule_prefix }}1710)
* Avoid short names or names that can be mistaken for other names ({{ site.default_rule_prefix }}1712)
* Properly name properties ({{ site.default_rule_prefix }}1715)
* Name methods and local functions using verbs or verb-object pairs ({{ site.default_rule_prefix }}1720)
* Use a verb or verb phrase to name an event ({{ site.default_rule_prefix }}1735)
* Postfix asynchronous methods with `Async` or `TaskAsync` ({{ site.default_rule_prefix }}1755)
</td>
<td class="column">

* Use an underscore for irrelevant parameters ({{ site.default_rule_prefix }}1739)


**Documentation**

* Write comments and documentation in US English ({{ site.default_rule_prefix }}2301)
* Document all `public`, `protected` and `internal` types and members ({{ site.default_rule_prefix }}2305)
* Write XML documentation with other developers in mind ({{ site.default_rule_prefix }}2306)
* Avoid inline comments ({{ site.default_rule_prefix }}2310)
* Only write comments to explain complex algorithms or decisions ({{ site.default_rule_prefix }}2316)
<br/>

**Layout**

* Maximum line length is 130 characters
* Indent 4 spaces, don't use tabs
* Keep one space between keywords like `if` and the expression, but don't add spaces after `(` and before `)`
* Add a space around operators, like `+`, `-`, `==`, etc.
* Always add curly braces after the keywords `if`, `else`, `do`, `while`, `for`, `foreach` and `case` ({{ site.default_rule_prefix }}1535)
* Always put opening and closing curly braces on a new line
* Don't indent object/collection initializers and initialize each property on a new line
* Don't indent lambda statement blocks
* Keep expression-bodied-members on one line; break long lines after the arrow sign
* Put the entire LINQ statement on one line, or start each keyword at the same indentation
* Remove redundant parentheses in expressions if they do not clarify precedence; add parentheses in expressions to avoid non-obvious precedence
<br/>

* Do not use `#region` ({{ site.default_rule_prefix }}2407)
* Use expression-bodied members appropriately ({{ site.default_rule_prefix }}2410)
</td>
<td class="column">

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

<td/>
<tr>

<table width="100%" class="footer">
<tr>
 <td>
   Dennis Doomen
   Version %semver% (%commitdate%)
 </td>
 <td style="text-align:right">
  [www.csharpcodingguidelines.com](http://www.csharpcodingguidelines.com)
  [www.continuousimprover.com](www.continuousimprover.com)
  [www.avivasolutions.nl](http://www.avivasolutions.nl)
  </td>
</tr>
</table>
