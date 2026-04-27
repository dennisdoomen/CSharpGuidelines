# Layout (AV2400) — Detailed Reference

## AV2400 — Use a common layout [Must]

- Keep the length of each line under 130 characters.

- Use an indentation of 4 spaces, and don't use tabs

- Keep one space between keywords like `if` and the expression, but don't add spaces after `(` and before `)` such as: `if (condition == null)`.

- Add a space around operators like `+`, `-`, `==`, etc.

- Always put opening and closing curly braces on a new line.

  Exception: simple properties/events without a body, such as: `public string Value { get; set; } = "default";`

- Don't indent object/collection initializers and initialize each property on a new line, so use a format like this:

		var dto = new ConsumerDto
		{
			Id = 123,
			Name = "Microsoft",
			PartnerShip = PartnerShip.Gold,
			ShoppingCart =
			{
				["VisualStudio"] = 1
			}
		};

- Don't indent lambda statement blocks and use a format like this:

		methodThatTakesAnAction.Do(source =>
		{
			// do something like this
		}

- Keep expression-bodied-members on one line. Break long lines after the arrow sign, like this:

		private string GetLongText =>
			"ABC ABC ABC ABC ABC ABC ABC ABC ABC ABC ABC ABC ABC ABC ABC ABC ABC ABC ABC ABC ABC";

- Put the entire LINQ statement on one line, or start each keyword at the same indentation, like this:

		var query = from product in products where product.Price > 10 select product;

  	or

		var query =
			from product in products
			where product.Price > 10
			select product;

- Start the LINQ statement with all the `from` expressions and don't interweave them with restrictions.

- Remove redundant parentheses in expressions if they do not clarify precedence. Add parentheses in expressions to avoid non-obvious precedence. For example, in nested conditional expressions: `overruled || (enabled && active)`, bitwise and shift operations: `foo | (bar >> size)`.

- Add an empty line between multi-line statements, between multi-line members, after the closing curly braces, between unrelated code blocks, and between the `using` statements of different root namespaces.

## AV2402 — Order and group namespaces according to the company [May]

// System namespaces come first.
	using System;
	using System.Collections.Generic;
	using System.Xml;

	// Then any other namespaces in alphabetic order.
	using AvivaSolutions.Business;
	using AvivaSolutions.Standard;
	using Telerik.WebControls;
	using Telerik.Ajax;

Using static directives and using alias directives should be written below regular using directives.
Always place these directives at the top of the file, before any namespace declarations (not inside them).

## AV2406 — Place members in a well-defined order [Must]

Maintaining a common order allows other team members to find their way in your code more easily. In general, a source file should be readable from top to bottom, as if reading a book, to prevent readers from having to browse up and down through the code file.

1. Private fields and constants
2. Public constants
3. Public static read-only fields
4. Factory methods
5. Constructors and the finalizer
6. Events
7. Public properties
8. Other methods and private properties in calling order

Declare local functions at the bottom of their containing method bodies (after all executable code).

## AV2407 — Do not use `#region` [Must]

Regions require extra work without increasing the quality or the readability of code. Instead they make code harder to view and refactor.

## AV2410 — Use expression-bodied members appropriately [Must]

Favor expression-bodied member syntax over regular member syntax only when:

- the body consists of a single statement and
- the body fits on a single line.

