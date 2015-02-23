<!--
NOTE: Requires Markdown Extra. See http://michelf.ca/projects/php-markdown/extra/
 --> 

#Documentation Guidelines

### Write comments and documentation in US English  (AV2301) ![](images/1.png)

### Document all public, protected and internal types and members  (AV2305) ![](images/2.png)
Documenting your code allows Visual Studio to pop-up the documentation when your class is used somewhere else. Furthermore, by properly documenting your classes, tools can generate professionally looking class documentation.

### Write XML documentation with other developers in mind  (AV2306) ![](images/2.png)
Write the documentation of your type with other developers in mind. Assume they will not have access to the source code and try to explain how to get the most out of the functionality of your type.

### Write MSDN-style documentation  (AV2307) ![](images/3.png)
Following the MSDN online help style and word choice helps developers find their way through your documentation more easily.

**Tip:** The tool [GhostDoc](http://submain.com/products/ghostdoc.aspx) can generate a starting point for documenting code with a shortcut key.

### Avoid inline comments  (AV2310) ![](images/2.png)
If you feel the need to explain a block of code using a comment, consider replacing that block with a method with a clear name.

### Only write comments to explain complex algorithms or decisions  (AV2316) ![](images/1.png)
Try to focus comments on the *why* and *what* of a code block and not the *how*. Avoid explaining the statements in words, but instead help the reader understand why you chose a certain solution or algorithm and what you are trying to achieve. If applicable, also mention that you chose an alternative solution because you ran into a problem with the obvious solution.

### Don't use comments for tracking work to be done later  (AV2318) ![](images/3.png)
Annotating a block of code or some work to be done using a *TODO* or similar comment may seem a reasonable way of tracking work-to-be-done. But in reality, nobody really searches for comments like that. Use a work item tracking system such as Team Foundation Server to keep track of leftovers.