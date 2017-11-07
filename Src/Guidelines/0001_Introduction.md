<!--
NOTE: Requires Markdown Extra. See http://michelf.ca/projects/php-markdown/extra/
 --> 

# 1. Introduction
## 1.1. What is this?

This document describes guidelines for coding in C# 3.0, 4.0 or 5.0. Of course, if you create such a document you should practice what you preach. So rest assured, these guidelines are representative to what we at [Aviva Solutions](http://www.avivasolutions.nl) do in our day-to-day work. Notice that not all guidelines have a clear rationale. Some of them are simply choices we made at Aviva Solutions. In the end, your choice doesn't matter, as long as you make one and apply it consistently.

Visual Studio's [Static Code Analysis](http://msdn.microsoft.com/en-us/library/dd264939.aspx) (which is also known as FxCop) and [StyleCop](http://stylecop.codeplex.com/) can automatically enforce a lot of coding and design rules by analyzing the compiled assemblies. You can configure it to do that at compile time or as part of a continuous or daily build. The companion site [www.csharpcodingguidelines.com](http://www.csharpcodingguidelines.com) provides a list of code analysis rules depending on the type of code base you're dealing with. This document provides an additional set of rules and recommendations that should help you achieve a more maintainable code base.

## 1.2. Why would you use this document?

Although some might see coding guidelines as undesired overhead or something that limits creativity, this approach has already proven its value for many years. This is because not every developer:

- is aware that code is generally read 10 times more than it is changed;
- is aware of the potential pitfalls of certain constructions in C#;
- is up to speed on certain conventions when using the .NET Framework such as `IDisposable` or the deferred execution nature of LINQ;
- is aware of the impact of using (or neglecting to use) particular solutions on aspects like security, performance, multi-language support, etc;
- realizes that not every developer is capable, skilled or experienced enough to understand elegant, but abstract solutions;

## 1.3. Basic principles

There are many unexpected things I run into during my work as a consultant, each deserving at least one guideline. Unfortunately, I still need to keep this document within a reasonable size. But unlike what some junior developers believe, that doesn't mean that something is okay just because it is not mentioned in this document.

In general, if I have a discussion with a colleague about a smell that this document does not cover, I'll refer back to a set of basic principles that apply to all situations, regardless of context. These include:

- The Principle of Least Surprise (or Astonishment): you should choose a solution that everyone can understand, and that keeps them on the right track.
- Keep It Simple Stupid (KISS): the simplest solution is more than sufficient.
- You Ain't Gonna Need It (YAGNI): create a solution for the problem at hand, not for the ones you think may happen later on. Can you predict the future?
- Don't Repeat Yourself (DRY): avoid duplication within a component, a source control repository or a [bounded context](http://martinfowler.com/bliki/BoundedContext.html), without forgetting the [Rule of Three](http://lostechies.com/derickbailey/2012/10/31/abstraction-the-rule-of-three/) heuristic.
- The [four principles of object-oriented programming](https://anampiu.github.io/blog/OOP-principles/): encapsulation, abstraction, inheritance and polymorphism.
- Generated code does not have to comply with coding guidelines. However, if it is possible to modify the templates used for generation, try to make them generate code that complies as much as possible.

Regardless of the elegance of someone's solution, if it is too complex for the ordinary developer, exposes unusual behavior, or tries to solve many possible future issues, it is very likely the wrong solution and needs redesign. The worst response a developer can give you to these principles is: "But it works?". 

## 1.4. How do you get started?

- Ask all developers to carefully read this document at least once. This will give them a sense of the kind of guidelines the document contains. 
- Make sure there are always a few hard copies of the [Quick Reference](http://www.csharpcodingguidelines.com/) close at hand. 
- Include the most critical coding guidelines in your [Project Checklist](http://www.continuousimprover.com/2010/03/alm-practices-5-checklists.html) and verify the remainder as part of your [Peer Review](http://www.dennisdoomen.net/2010/02/tfs-development-practices-part-2-peer.html). 
- Consider forking the [original sources](https://github.com/dennisdoomen/csharpguidelines) on [GitHub](https://github.com/) and create your own [internal](https://github.com/dennisdoomen/csharpguidelines/blob/master/LICENSE.md) version of the document.
- Decide which CA rules are applicable for your project and store these somewhere, such as your source control system, or create a custom Visual Studio Rule Set. The [companion site](http://www.csharpcodingguidelines.com/) offers rule sets for both line-of-business applications and more generic code bases, like frameworks and class libraries.
- Add a custom [Code Analysis Dictionary](http://msdn.microsoft.com/en-us/library/bb514188.aspx) containing your domain or company-specific terms, names and concepts. If you don't, Static Analysis will report warnings for (parts of) phrases that are not in its internal dictionary. 
- Configure Visual Studio to verify the selected CA rules as part of the Release build. Then they won't interfere with normal developing and debugging activities, but still can be run by switching to the Release configuration. 
- Add an item to your project checklist to make sure all new code is verified against CA violations, or use something like [Check-in Policy](http://msdn.microsoft.com/en-us/library/ms182075(v=vs.110).aspx) or a [Git commit hook](http://git-scm.com/book/en/Customizing-Git-Git-Hooks) to prevent any code from violating CA rules at all. 
- [ReSharper](http://www.jetbrains.com/resharper/) has an intelligent code inspection engine that, with some configuration, already supports many aspects of the Coding Guidelines. It automatically highlights any code that does not match the rules for naming members, such as Pascal or Camel casing, detects dead code, and many other things. One click of the mouse button (or the corresponding keyboard shortcut) is usually enough to fix it. 
- ReSharper also has a File Structure window that displays an overview of the members of your class or interface, and allows you to rearrange them using drag-and-drop. 
- [GhostDoc](http://submain.com/products/ghostdoc.aspx) can generate XML comments for any member using a keyboard shortcut. The beauty of it is that it closely follows the MSDN-style of documentation. However, you have to be careful not to misuse this tool, and use it as a starter only. 

## 1.5. Why did we create it?

The idea started in 2002 when Vic Hartog at Philips Medical Systems and I were assigned to write a [coding standard](http://www.tiobe.com/content/paperinfo/gemrcsharpcs.pdf) for C# 1.0. Since then, I've added, removed and changed rules based on experiences, feedback from the community and new tooling support offered by a continuous stream of new Visual Studio releases.

Additionally, after reading [Robert C. Martin](https://sites.google.com/site/unclebobconsultingllc/)'s book [Clean Code: A Handbook of Agile Software Craftsmanship](http://www.amazon.com/Clean-Code-Handbook-Software-Craftsmanship/dp/0132350882), I became a big fan of his ideas and decided to include some of his smells and heuristics as guidelines. This doesn't mean that this document is a replacement for his book. I sincerely recommend that you read his book to understand the rationale behind his recommendations.

I've also decided to include some design guidelines in addition to simple coding guidelines. They are too important to ignore as they drastically improve code quality.

## 1.6. Is this a coding standard?

The document does not state that projects must comply with these guidelines, neither does it say which guidelines are more important than others. However, we encourage projects to decide themselves which guidelines are important, what deviations a project will use, who is the consultant in case doubts arise, and what kind of layout must be used for source code. Obviously, you should make these decisions before starting the real coding work.

To help you decide, each guidelines has a level of importance:

![](images/1.png) Guidelines that you should never skip and should be applicable to all situations

![](images/2.png) Strongly recommended guidelines

![](images/3.png) May not be applicable in all situations

## 1.7. Feedback and disclaimer

This document has been compiled using many contributions from community members, blog posts, on-line discussions and many years of developing in C#. If you have questions, comments or suggestions, let me know by sending me an email at [dennis.doomen@avivasolutions.nl](mailto:dennis.doomen@avivasolutions.nl), [creating an issue](https://github.com/dennisdoomen/csharpguidelines/issues) or Pull Request on GitHub, or ping me at [http://twitter.com/ddoomen](http://twitter.com/ddoomen). I will try to revise and republish this document with new insights, experiences and remarks on a regular basis.

Notice though that it merely reflects my view on proper C# code so Aviva Solutions will not be liable for any direct or indirect damages caused by applying the guidelines of this document. This document is published under a Creative Commons license, specifically the [Creative Commons Attribution-ShareAlike 4.0](http://creativecommons.org/licenses/by-sa/4.0/) license. 
