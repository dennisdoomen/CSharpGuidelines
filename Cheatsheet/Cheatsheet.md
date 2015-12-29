<link href="style.css" type="text/css" rel="stylesheet"></link>

<table width="100%">
    <tr>
        <td class="title">Coding Guidelines for C# 3.0, 4.0 and 5.0 Cheat Sheet</td>
        <td markdown="1" rowspan="2" style="text-align:right">
            <img src='images/logo.png' />
        </td>
    </tr>
    <tr>
        <td><div class="subTitle">Design & Maintainability</div> (level 1 and 2 only)</td>
    </tr>
</table>
<table width="100%">
    <tr>
        <td class="column" valign="top">
            <b>Basic Principles</b>
            <table>
                <tr><td>• The Principle of Least Surprise</td></tr>
                <tr><td>• Keep It Simple Stupid</td></tr>
                <tr>
                    <td>• You Ain’t Gonne Need It</td>
                </tr>
                <tr><td>• Don’t Repeat Yourself</td></tr>
            </table>

            <b>Class Design</b>
            <table>

                <tr>
                    <td>
                        •  A class or interface should have a single purpose (AV1000)
                    </td>
                </tr>
                <tr>
                    <td>
                        •  An interface should be small and focused (AV1003)
                    </td>
                </tr>
                <tr>
                    <td>
                        •  Use an interface to decouple classes from each other (AV1005)
                    </td>
                </tr>
                <tr>
                    <td>
                        •  Don’t hide inherited members with the `new` keyword (AV1010)
                    </td>
                </tr>
                <tr>
                    <td>
                        •  It should be possible to treat a derived object as if it were a base class object (AV1011)
                    </td>
                </tr>
                <tr>
                    <td>
                        •  Don’t refer to derived classes from the base class (AV1013)
                    </td>
                </tr>
                <tr>
                    <td>
                        •  Avoid exposing the objects an object depends on (AV1014)
                    </td>
                </tr>
                <tr>
                    <td>
                        •  Avoid bidirectional dependencies (AV1020)
                    </td>
                </tr>
                <tr>
                    <td>
                        • Classes should have state and behavior (AV1025)
                    </td>
                </tr>

            </table>
            <br />
            <b>Member Design</b>
            <table>

                <tr>
                    <td>
                        • Allow properties to be set in any order (AV1100)
                    </td>
                </tr>
                <tr>
                    <td>
                        • Don’t use mutual exclusive properties (AV1110)
                    </td>
                </tr>
                <tr>
                    <td>
                        • A method or property should do only one thing (AV1115)
                    </td>
                </tr>
                <tr>
                    <td>
                        • Don’t expose stateful objects through static members (AV1125)
                    </td>
                </tr>
                <tr>
                    <td>
                        • Return an `IEnumerable<t>` or `ICollection<t>` instead of a concrete collection class (AV1130)
                    </td>
                </tr>
                <tr>
                    <td>
                        • Properties, methods and arguments representing strings or collections should never be `null` (AV1135)
                    </td>
                </tr>
                <tr>
                    <td>
                        • Define parameters as specific as possible (AV1137)
                    </td>
                </tr>

            </table>
        </td>
        <td class="column" valign="top">
            <b>Miscellaneous Design</b>
            <table>
                <tr><td>• Throw exceptions rather than returning status values (AV1200)</td></tr>
                <tr><td>• Provide a rich and meaningful exception message text (AV1202)  </td></tr>
                <tr><td>• Don’t swallow errors by catching generic exceptions  (AV1210)   </td></tr>
                <tr><td>• Always check an event handler delegate for `null` (AV1220)   </td></tr>
                <tr><td>• Properly handle exceptions in asynchronous code (AV1215)   </td></tr>
                <tr><td>• Use a protected virtual method to raise each event (AV1225)   </td></tr>
                <tr><td>• Don’t pass `null` as the sender parameter when raising an event (AV1235)  </td></tr>
                <tr><td>• Use generic constraints if applicable (AV1240)   </td></tr>
                <tr><td>• Evaluate the result of a LINQ expression before returning it (AV1250)   </td></tr>
            </table>
            <br />
            <b>Maintainability</b>
            <table>

                <tr>
                    <td>
                        • Methods should not exceed 7 statements (AV1500)
                    </td>
                </tr>
                <tr>
                    <td>
                        • Make all members `private` and types `internal` by default (AV1501)
                    </td>
                </tr>
                <tr>
                    <td>
                        • Avoid conditions with double negatives (AV1502)
                    </td>
                </tr>
                <tr>
                    <td>
                        • Don’t use "magic numbers" (AV1515)
                    </td>
                </tr>
                <tr>
                    <td>
                        • Only use `var` when the type is very obvious (AV1520)
                    </td>
                </tr>
                <tr>
                    <td>
                        • Declare and initialize variables as late as possible (AV1521)
                    </td>
                </tr>
                <tr>
                    <td>
                        • Favor Object and Collection Initializers over separate statements (AV1523)
                    </td>
                </tr>
                <tr>
                    <td>
                        • Don’t make explicit comparisons to `true` or `false` (AV1525)
                    </td>
                </tr>
                <tr>
                    <td>
                        • Don’t change a loop variable inside a `for` or `foreach` loop (AV1530)
                    </td>
                </tr>
                <tr>
                    <td>
                        • Avoid nested loops (AV1532)
                    </td>
                </tr>

            </table>
        </td>
        <td class="column" valign="top">
            <ul>
                <li> Always add a block after keywords such `if`, `else`, `while`, `for`, `foreach` and `case` (AV1535)</li>
                <li> Always add a `default` block after the last `case` in a `switch` statement (AV1536)</li>
                <li> Finish every `if`-`else`-`if `statement with an `else`-part (AV1537)</li>
                <li> Be reluctant with multiple `return` statements (AV1540)</li>
                <li> Don’t use `if`-`else` statements instead of a simple (conditional) assignment  (AV1545)</li>
                <li> Encapsulate complex expressions in a method or property (AV1547)</li>
                <li> Call the most overloaded method from other overloads (AV1551)</li>
                <li> Only use optional arguments to replace overloads (AV1553)</li>
                <li> Avoid using named arguments (AV1555)</li>
                <li> Don’t allow methods and constructors with more than three parameters (AV1561)</li>
                <li> Don’t use `ref` or `out` parameters (AV1562)</li>
                <li> Avoid methods that take a `bool` flag (AV1564)</li>
                <li> Always check the result of an `as` operation (AV1570)</li>
                <li></li> Don’t comment-out code (AV1575)</li>
            </ul>
            <br />
            <b>Framework Guidelines</b>
            <ul>

                <li>Use C# type aliases instead of the types from the `System` namespace (AV2201)
                <li>Build with the highest warning level (AV2210)
                <li>Use Lambda expressions instead of delegates (AV2221)
                <li>Only use the `dynamic` keyword when talking to a dynamic object (AV2230)
                <li>Favor `async`/`await` over the `Task` (AV2235)
            </ul>

        </td>
    </tr>
    <tr>
        <td colspan="3">
            <table width="100%" markdown="1" class="footer">
                <tr>
                    <td>
                        August 2014
                        Dennis Doomen
                    </td>
                    <td style="text-align:right">
                        [www.csharpcodingguidelines.com](http://www.csharpcodingguidelines.com)
                        [www.dennisdoomen.net](http://www.dennisdoomen.net)
                        [www.avivasolutions.nl](http://www.avivasolutions)
                    </td>
                </tr>
            </table>
        </td>
    </tr>
</table>

<table width="100%" style="page-break-before: always;">
    <tr>
        <td class="title">Coding Guidelines for C# 3.0, 4.0 and 5.0 Cheat Sheet</td>
        <td markdown="1" rowspan="2" style="text-align:right"><img src='images/logo.png' /></td>
    </tr>
    <tr>
        <td><div class="subTitle">Naming & Layout</div> (level 1 and 2 only)</td>
    </tr>
</table>
<table width="100%">
    <tr>
        <td class="column" markdown="1" valign="top">
            <table>
                <tr><th colspan="2"><b>Pascal Casing</b></th></tr>
                <tr>
                    <td>Class, Struct</td>
                    <td>`AppDomain`</td>
                </tr>
                <tr><td>Interface</td><td>`IBusinessService`</td></tr>
                <tr><td>Enumeration type</td><td>`ErrorLevel`</td></tr>
                <tr><td>Enumeratiion values</td><td>`FatalError`</td></tr>
                <tr><td>Event</td><td>`Click`</td></tr>
                <tr><td>Protected field</td><td>`MainPanel`</td></tr>
                <tr><td>Const field</td><td>`MaximumItems`</td></tr>
                <tr>
                    <td>Read-only static field&nbsp;&nbsp;</td>
                    <td>`RedValue`	</td>
                </tr>
                <tr><td>Method</td><td>`ToString`</td></tr>
                <tr><td>Namespace</td><td>`System.Drawing`</td></tr>
                <tr><td>Property</td><td>`BackColor`</td></tr>
                <tr><td>Type Parameter</td><td>`TEntity`</td></tr>
            </table>
            <table>
                <tr>
                    <th colspan="2"><b>Camel Casing</b></th>
                </tr>
                <tr><td>Private field</td><td>`listItem`</td></tr>
                <tr><td>Variable </td><td>`listOfValues`</td></tr>
                <tr><td>Const variable</td><td>`maximumItems`</td></tr>
                <tr><td>Parameter</td><td>`typeName`</td></tr>
            </table>
            <br />
            <b>Naming</b>
            <table>

                <tr>• <td>Use US English (AV1701)</td></tr>
                <tr>• <td>Don’t include numbers in variables, parameters and type members  (AV1704)</td> </tr>
                <tr>• <td>Don’t prefix fields (AV1705)</td> </tr>
                <tr>• <td>Don’t use abbreviations (AV1706)</td> </tr>
                <tr>• <td>Name members, parameters or variables according its meaning and not its type (AV1707)</td> </tr>
                <tr>• <td>Name types using nouns, noun phrases or adjective phrases (AV1708)</td> </tr>
                <tr>• <td>Don’t repeat the name of a class or enumeration in its members (AV1710)</td> </tr>
                <tr>• <td>Avoid short names or names that can be mistaken with other names (AV1712)</td> </tr>
                <tr>• <td>Name methods using verb-object pair (AV1720)</td> </tr>
                <tr>• <td>Name namespaces using names, layers, verbs and features (AV1725)</td> </tr>

            </table>
        </td>
        <td class="column" valign="top">
            <table>
                <tr>
                    <td>

                        •
                        Use an underscore for irrelevant lambda parameters (AV1739)
                    </td>
                </tr>
            </table>
            <b>Documentation</b>
            <table>
                <tr><td>• Write comments and documentation in US English (AV2301) </td></tr>
                <tr><td>• Document all public, protected and internal types and members (AV2305) </td></tr>
                <tr><td>• Avoid inline comments (AV2310) </td></tr>
                <tr><td>• Only write comments to explain complex algorithms or decisions (AV2316) </td></tr>
                <tr><td>• Don’t use comments for tracking work to be done later (AV2318) </td></tr>

            </table>

            <br />
            <b>Layout</b>
            <table>

                <tr>
                    <td>
                        • Maximum line length is 130 characters.
                    </td>
                </tr>
                <tr>
                    <td>
                        • Indent 4 spaces, don’t use Tabs
                    </td>
                </tr>
                <tr>
                    <td>
                        • Keep one white-space between keywords like `if` and the expression, but don’t add white-spaces after `(` and before `)`.
                    </td>
                </tr>
                <tr>
                    <td>
                        • Add a white-space around operators, like `+`, `-`, `==`, etc.
                    </td>
                </tr>
                <tr>
                    <td>
                        • Always add parentheses after keywords `if`, `else`, `do`, `while`, `for` and `foreach`
                    </td>
                </tr>
                <tr>
                    <td>
                        • Always put opening and closing parentheses on a new line.
                    </td>
                </tr>
                <tr>
                    <td>
                        • Don’t indent object initializers and initialize each property on a new line.
                    </td>
                </tr>
                <tr>
                    <td>
                        • Don’t indent lambda statements
                    </td>
                </tr>
                <tr>
                    <td>
                        • Put the entire LINQ statement on one line, or start each keyword at the same indentation.
                    </td>
                </tr>
                <tr>
                    <td>
                        • Add braces around comparison conditions, but don’t add braces around a singular condition.
                    </td>
                </tr>

            </table>
        </td>
        <td class="column" valign="top">
            <div markdown="1" class="sidebar">
                <table>
                    <tr>
                        <th><b>Empty lines</b> </th>
                    </tr>

                    <tr>
                        <td>
                            • Between members
                        </td>
                    </tr>
                    <tr><td>• After the closing parentheses </td></tr>
                    <tr><td>• Between multi-line statements </td></tr>
                    <tr><td>• Between unrelated code blocks  </td></tr>
                    <tr><td>• Around the `#region` keyword </td></tr>
                    <tr><td>• Between the `using` statements of different root namespaces. </td></tr>

                </table>

            </div>

            <div markdown="1" class="sidebar">
                <table>
                    <tr>
                        <th><b>Member order</b> </th>
                    </tr>
                    <tr>
                        <td>
                            1. Private fields and constants
                        </td>
                    </tr>
                    <tr>
                        <td>
                            2. Public constants
                        </td>
                    </tr>
                    <tr>
                        <td>
                            3. Public read-only static fields
                        </td>
                    </tr>
                    <tr>
                        <td>
                            4. Factory Methods
                        </td>
                    </tr>
                    <tr>
                        <td>
                            5. Constructors and the Finalizer
                        </td>
                    </tr>
                    <tr>
                        <td>
                            6. Events
                        </td>
                    </tr>
                    <tr>
                        <td>
                            7. Public Properties
                        </td>
                    </tr>
                    <tr>
                        <td>
                            8. Other methods and private properties in calling order
                        </td>
                    </tr>
                </table>
            </div>
            <div markdown="1" class="sidebar">
                <table>
                    <tr>
                        <th><b>Important Note</b></th>
                    </tr>
                    <tr><td>These coding guidelines are an extension to Visual Studio's Code Analysis functionalty, so make sure you enable that for all your projects. Check the full document for more details.</td></tr>
                </table>
            </div>
        </td>
    </tr>
    <tr>
        <td colspan="3">
            <table width="100%" markdown="1" class="footer">
                <tr>
                    <td>
                        August 2014
                        Dennis Doomen
                    </td>
                    <td style="text-align:right">
                        <a href="www.csharpcodingguidelines.com">http://www.csharpcodingguidelines.com"</a>
                        <a href="www.dennisdoomen.net">http://www.dennisdoomen.net" </a>
                        <a href="www.avivasolutions.nl">http://www.avivasolutions" </a>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
</table>
