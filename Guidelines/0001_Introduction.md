<!--
NOTE: Requires Markdown Extra. See http://michelf.ca/projects/php-markdown/extra/
 --> 

#Introduction#
##What is this?##

This document attempts to provide guidelines (or coding standards if you like) for coding in C# 3.0, 4.0 or 5.0 that are both useful and pragmatic. Of course, if you create such a document you should practice what you preach. So rest assured, these guidelines are representative to what we at [Aviva Solutions](http://www.avivasolutions.nl) do in our day-to-day work. Of course, not all coding guidelines have a clear rationale. Some of them are simply choices we made at Aviva Solutions.

Visual Studio's [Static Code Analysis](http://msdn.microsoft.com/en-us/library/dd264939.aspx) (which is also known as FxCop) and [StyleCop](http://stylecop.codeplex.com/) can already automatically enforce a lot of coding and design rules by analyzing the compiled assemblies. You can configure to do that at compile time or as part of a continuous or daily build. This document just adds additional rules and recommendations but its companion site [www.csharpcodingguidelines.com](http://www.csharpcodingguidelines.com) provides a list of code analysis rules depending on the type of code base you're dealing with.

##Why would I use this document?##

Although some might see coding guidelines as undesired overhead or something that limits creativity, this approach has already proven its value for many years. Why? Well, because not every developer

- is aware that code is generally read 10 times more than it is changed;
- is aware of the potential pitfalls of certain constructions in C#;
- is introduced into certain conventions when using the .NET Framework such as IDisposable or the deferred execution nature of LINQ;
- is aware of the impact of using (or neglecting to use) particular solutions on aspects like security, performance, multi-language support, etc;
- knows that not every developer is as capable in understanding an elegant, but abstract, solution as the original developer;

##Basic Principles##

There are many unexpected things I run into during my work as a consultant, each deserving at least one guideline. Unfortunately, I still need to keep this document within a reasonable size. But unlike to what some junior developers believe, that doesn't mean that when something is not mentioned in this guidelines it must be okay.

In general, if I have a discussion with a colleague about a smell that this document does not provide absolution for, I'll refer back to a set of basic principles that apply to all situations, regardless of context. These include:

- The Principle of Least Surprise (or Astonishment), which means that you should choose a solution that does include any things people might not understand, or put on the wrong track.
- Keep It Simple Stupid (a.k.a. KISS), a funny way of saying that the simplest solution is more than sufficient.
- You Ain't Gonne Need It (a.k.a. YAGNI), which tells you to create a solution for the current problem rather than the ones you think will happen later on (since when can you predict the future?)
- Don't Repeat Yourself (a.k.a. DRY), which encourages you to prevent duplication in your code base without forgetting the [Rule of Three](http://lostechies.com/derickbailey/2012/10/31/abstraction-the-rule-of-three/) heuristic.

Regardless of the elegancy of somebody's solution, if it's too complex for the ordinary developer, exposes unusual behavior, or tries to solve many possible future issues, it is very likely the wrong solution and needs redesign.

##How do I get started?##

- Ask all developers to carefully read this document at least once. This will give them a sense of the kind of guidelines the document contains. 
- Make sure there are always a few hard copies of the [Quick Reference](http://www.csharpcodingguidelines.com/) close at hand. 
- Include the most critical coding guidelines on your [Project Checklist](http://www.dennisdoomen.net/2010/03/alm-practices-5-checklists.html) and verify the remainder as part of your [Peer Review](http://www.dennisdoomen.net/2010/02/tfs-development-practices-part-2-peer.html). 
- Decide which CA rules are applicable for your project and write these down somewhere, such as your TFS team site, or create a custom Visual Studio 2010/2012 Rule Set. The [companion site](http://www.csharpcodingguidelines.com/) offers rule sets for both line-of-business applications and more generic code bases like frameworks and class libraries.
- Add a custom [Code Analysis Dictionary](http://msdn.microsoft.com/en-us/library/bb514188.aspx) containing your domain- or company-specific terms, names and concepts. If you don't, Static Analysis will report warnings for (parts of) phrases that are not part of its internal dictionary. 
- Configure Visual Studio to verify the selected CA rules as part of the Release build. Then they won't interfere with normal developing and debugging activities, but still can be run by switching to the Release configuration. 
- Add an item to your project checklist to make sure all new code is verified against CA violations, or use the corresponding [Check-in Policy](http://msdn.microsoft.com/en-us/library/ms182075(v=vs.110).aspx) if you want to prevent any code from violating CA rules at all. 
- [ReSharper](http://www.jetbrains.com/resharper/) has an intelligent code inspection engine that, with some configuration, already supports many aspects of the Coding Guidelines. It will automatically highlight any code that does not match the rules for naming members (e.g. Pascal or Camel casing), detect dead code, and many other things. One click of the mouse button (or the corresponding keyboard shortcut) is usually enough to fix it. 
- ReSharper also has a File Structure window that shows an overview of the members of your class or interface and allows you to easily rearrange them using a simple drag-and-drop action. 
- Using [GhostDoc](http://submain.com/products/ghostdoc.aspx) you can generate XML comments for any member using a keyboard shortcut. The beauty of it, is that it closely follows the MSDN-style of documentation. However, you have to be careful not to misuse this tool, and use it as a starter only. 

##Why did you create it?##

The idea started in 2002 when Vic Hartog (Philips Medical Systems) and I were assigned the task of writing up a [coding standard](http://www.tiobe.com/content/paperinfo/gemrcsharpcs.pdf) for C# 1.0.Since then, I've regularly added, removed and changed rules based on experiences, feedback from the community and new tooling support such as offered by Visual Studio 2010.

Additionally, after reading [Robert C. Martin](http://www.objectmentor.com/omTeam/martin_r.html)'s book [Clean Code: A Handbook of Agile Software Craftsmanship](http://www.amazon.com/Clean-Code-Handbook-Software-Craftsmanship/dp/0132350882), I became a big fan of his ideas and decided to include some of his smells and heuristics as guidelines. Notice though that this document is in no way a replacement for his book. I sincerely recommend that you read his book to gain a solid understanding of the rationale behind his recommendations.

I've also decided to include some design guidelines in addition to simple coding guidelines. They are too important to ignore and have a big influence in reaching high quality code.

##Is this a coding standard?##

The document does not state that projects must comply with these guidelines, neither does it say which guidelines are more important than others. However,we encourage projects todecide themselves what guidelines are important, what deviations a project will use, who is the consultant in case doubts arise, and what kind of layout must be used for source code. Obviously, you should make these decisions before starting the real coding work.

To help you in this decision, I've assigned a level of importance to each guideline:

![](images/1.png) Guidelines that you should never skip and should be applicable to all situations

![](images/2.png) Strongly recommended guidelines

![](images/3.png) that may not be applicable in all situations

In general, generated code should not need to comply with coding guidelines. However, if it is possible to modify the templates used for generation, try to make them generate code that complies as much as possible.

##Feedback and disclaimer##

This document has been compiledusing many contributions from community members,blog posts, on-line discussionsand many years of developing in C#. If you have questions, comments or suggestions, just let me know by sendingme an email at [dennis.doomen@avivasolutions.nl](mailto:dennis.doomen@avivasolutions.nl) or tweet me at [http://twitter.com/ddoomen](http://twitter.com/ddoomen). I will try to revise and republish this document with new insights, experiences and remarks on a regular basis.

Notice though that it merely reflects my view on proper C# code so Aviva Solutions will not be liable for any direct or indirect damages caused by applying the guidelines of this document.

It is allowed to copy, adapt, and redistribute this document and its companion quick reference guide for non-commercial purposes or internal usage. However, you may not republish this document, or publish or distribute any adaptation of this document for commercial use without first obtaining express written approval from Aviva Solutions.
