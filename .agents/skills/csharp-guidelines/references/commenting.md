# Documentation & Comments (AV2300) — Detailed Reference

## AV2301 — Write comments and documentation in US English [Must]



## AV2305 — Document all `public`, `protected` and `internal` types and members [Should]

Documenting your code allows Visual Studio, [Visual Studio Code](https://code.visualstudio.com/) or [Jetbrains Rider](https://www.jetbrains.com/rider/) to pop-up the documentation when your class is used somewhere else. Furthermore, by properly documenting your classes, tools can generate professionally looking class documentation.

## AV2306 — Write XML documentation with other developers in mind [Should]

Write the documentation of your type with other developers in mind. Assume they will not have access to the source code and try to explain how to get the most out of the functionality of your type.

## AV2307 — Write MSDN-style documentation [May]

Following the MSDN online help style and word choice helps developers find their way through your documentation more easily.

## AV2310 — Avoid inline comments [Should]

If you feel the need to explain a block of code using a comment, consider replacing that block with a method with a clear name.

## AV2316 — Only write comments to explain complex algorithms or decisions [Must]

Try to focus comments on the *why* and *what* of a code block and not the *how*. Avoid explaining the statements in words, but instead help the reader understand why you chose a certain solution or algorithm and what you are trying to achieve. If applicable, also mention that you chose an alternative solution because you ran into a problem with the obvious solution.

## AV2318 — Don't use comments for tracking work to be done later [May]

Annotating a block of code or some work to be done using a *TODO* or similar comment may seem a reasonable way of tracking work-to-be-done. But in reality, nobody really searches for comments like that. Use a work item tracking system to keep track of leftovers.

