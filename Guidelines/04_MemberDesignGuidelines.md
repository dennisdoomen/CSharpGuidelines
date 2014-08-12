<!--
NOTE: Requires Markdown Extra. See http://michelf.ca/projects/php-markdown/extra/
 --> 

#Member Design Guidelines

### Allow properties to be set in any order (AV1100) ![](images/1.png)

Properties should be stateless with respect to other properties, i.e. there should not be a difference between first setting property DataSource and then DataMember or vice versa.

### Use a method instead of a property (AV1105) ![](images/3.png)

- If the work is more expensive than setting a field value. 
- If it represents a conversion such as the `Object.ToString` method.
- If it returns a different result each time it is called, even if the arguments didn't change. For example, the `NewGuid` method returns a different value each time it is called.
- If the operation causes a side effect such as changing some internal state not directly related the property (which violates the [Command Query Separation](http://martinfowler.com/bliki/CommandQuerySeparation.html) principle). 

**Exception** Populating an internal cache or implementing [lazy-loading](http://www.martinfowler.com/eaaCatalog/lazyLoad.html) is a good exception.

### Don't use mutual exclusive properties (AV1110) ![](images/1.png)

Having properties that cannot be used at the same time typically signals a type that is representing two conflicting concepts. Even though those concepts may share some of the behavior and state, they obviously have different rules that do not cooperate.

This violation is often seen in domain models and introduces all kinds of conditional logic related to those conflicting rules, causing a ripple effect that significantly worsens the maintenance burden.

### A method or property should do only one thing (AV1115) ![](images/1.png)

Similarly to rule AV1000, a method should have a single responsibility.

### Don't expose stateful objects through static members (AV1125) ![](images/2.png)

A stateful object is an object that contains many properties and lots of behavior behind that. If you expose such an object through a static property or method of some other object, it will be very difficult to refactor or unit test a class that relies on such a stateful object. In general, introducing a construction like that is a great example of violating many of the guidelines of this chapter.

A classic example of this is the `HttpContext.Current` property, part of ASP.NET. Many see the `HttpContext` class as a source for a lot of ugly code. In fact, the testing guideline [Isolate the Ugly Stuff](http://msdn.microsoft.com/en-us/magazine/dd263069.aspx#id0070015) often refers to this class.

### Return an `IEnumerable<T>` or `ICollection<T>` instead of a concrete collection class (AV1130) ![](images/2.png)

In general, you don't want callers to be able to change an internal collection, so don't return arrays, lists or other collection classes directly. Instead, return an `IEnumerable<T>`, or, if the caller must be able to determine the count, an `ICollection<T>`.

**Note** If you're using .NET 4.5, you can also use `IReadOnlyCollection<T>`, `IReadOnlyList<T>` or `IReadOnlyDictionary<TKey, TValue>`.

### Properties, methods and arguments representing strings or collections should never be `null` (AV1135) ![](images/1.png)

Returning `null` can be unexpected by the caller. Always return an empty collection or an empty string instead of a `null` reference. This also prevents cluttering your code base with additional checks for `null`, or even worse, `string.IsNotNullOrEmpty()`.

### Define parameters as specific as possible (AV1137) ![](images/2.png)

If your member needs a specific piece of data, define parameters as specific as that and don't take a container object instead. For instance, consider a method that needs a connection string that is exposed through some central `IConfiguration` interface. Rather than taking a dependency on the entire configuration, just define a parameter for the connection string. This not only prevents unnecessary coupling, it also improved maintainability in the long run.

### Consider using domain-specific value types rather than primitives (AV1140) ![](images/3.png)

Instead of using strings, integers and decimals for representing domain specific types such as an ISBN number, an email address or amount of money, consider created dedicated value objects that wrap both the data and the validation rules that apply to it. By doing this, you prevent ending up having multiple implementations of the same business rules, which both improves maintainability and prevents bugs.
