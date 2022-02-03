## 1.1. What is this?

This document attempts to provide guidelines (or coding standards if you like) for all versions of C# up to and including 9.0 that are both valuable and pragmatic. Of course, if you create such a document you should practice what you preach. So rest assured, these guidelines are representative to what we at [Aviva Solutions](https://www.avivasolutions.nl) do in our day-to-day work. Notice that not all guidelines have a clear rationale. Some of them are simply choices we made at Aviva Solutions. In the end, it doesn't matter what choice you made, as long as you make one and apply it consistently.

## 1.2. Why would you use this document?

Although some might see coding guidelines as undesired overhead or something that limits creativity, this approach has already proven its value for many years. This is because not every developer:

- is aware that code is generally read 10 times more than it is changed;
- is aware of the potential pitfalls of certain constructs in C#;
- is up to speed on certain conventions when using the .NET Framework such as `IDisposable`, `async`/`await`, or the deferred execution nature of LINQ;
- is aware of the impact of using (or neglecting to use) particular solutions on aspects like security, performance, multi-language support, etc;
- realizes that not every developer is as capable, skilled or experienced to understand elegant, but potentially very abstract solutions;

## 1.3. Basic principles

There are many unexpected things I run into during my work as a consultant, each deserving at least one guideline. Unfortunately, I still need to keep this document within a reasonable size. But unlike what some junior developers believe, that doesn't mean that something is okay just because it is not mentioned in this document.

In general, if I have a discussion with a colleague about a smell that this document does not cover, I'll refer back to a set of basic principles that apply to all situations, regardless of context. These include:

- The Principle of Least Surprise (or Astonishment): you should choose a solution that everyone can understand, and that keeps them on the right track.
- Keep It Simple Stupid (a.k.a. KISS): the simplest solution is more than sufficient.
- You Ain't Gonna Need It (a.k.a. YAGNI): create a solution for the problem at hand, not for the ones you think may happen later on. Can you predict the future?
- Don't Repeat Yourself (a.k.a. DRY): avoid duplication within a component, a source control repository or  a [bounded context](http://martinfowler.com/bliki/BoundedContext.html), without forgetting the [Rule of Three](http://lostechies.com/derickbailey/2012/10/31/abstraction-the-rule-of-three/) heuristic.
- The [four principles of object-oriented programming](https://github.com/TelerikAcademy/Object-Oriented-Programming/tree/master/Topics/04.%20OOP-Principles-Part-1): encapsulation, abstraction, inheritance and polymorphism.
- In general, generated code should not need to comply with coding guidelines. However, if it is possible to modify the templates used for generation, try to make them generate code that complies as much as possible.

Regardless of the elegance of someone's solution, if it's too complex for the ordinary developer, exposes unusual behavior, or tries to solve many possible future issues, it is very likely the wrong solution and needs redesign. The worst response a developer can give you to these principles is: "But it works?".

## 1.4. How do you get started?

- Ask all developers to carefully read this document at least once. This will give them a sense of the kind of guidelines the document contains.
- Make sure there are always a few hard copies of the [Cheat Sheet](https://github.com/dennisdoomen/CSharpGuidelines/releases/latest) close at hand.
- Include the most critical coding guidelines on your [Project Checklist](https://www.continuousimprover.com/2010/03/alm-practices-5-checklists.html) and verify the remainder as part of your [Peer Review](https://www.continuousimprover.com/2010/02/tfs-development-practices-part-2-peer.html).
- Consider forking the [original sources](https://github.com/dennisdoomen/csharpguidelines) on [GitHub](https://github.com/) and create your own [internal](https://github.com/dennisdoomen/csharpguidelines/blob/master/LICENSE.md) version of the document.
- Jetbrain's [ReSharper](http://www.jetbrains.com/resharper/) and their fully fledged Visual Studio replacement [Rider](https://www.jetbrains.com/rider/), has an intelligent code inspection engine that, with some configuration, already supports many aspects of the Coding Guidelines. It automatically highlights any code that does not match the rules for naming members (e.g. Pascal or Camel casing), detects dead code, and many other things. One click of the mouse button (or the corresponding keyboard shortcut) is usually enough to fix it.
- ReSharper also has a File Structure window that displays an overview of the members of your class or interface, and allows you to easily rearrange them using a simple drag-and-drop action.
- [CSharpGuidelinesAnalyzer](https://github.com/bkoelman/CSharpGuidelinesAnalyzer) verifies over 40 of our guidelines, while typing code in Visual Studio 2017 and during CI builds. An updated Resharper settings file is included.

## 1.5. Why did we create it?

The idea started in 2002 when Vic Hartog (Philips Medical Systems) and I were assigned the task of writing up a [coding standard](http://www.tiobe.com/content/paperinfo/gemrcsharpcs.pdf) for C# 1.0. Since then, I've regularly added, removed and changed rules based on experiences, feedback from the community and new tooling support offered by a continuous stream of new developments in the .NET ecosystem. Special thanks go to [Bart Koelman](https://github.com/bkoelman) for being a very active contributor over all those years.

Additionally, after reading [Robert C. Martin](https://sites.google.com/site/unclebobconsultingllc/)'s book [Clean Code: A Handbook of Agile Software Craftsmanship](http://www.amazon.com/Clean-Code-Handbook-Software-Craftsmanship/dp/0132350882), I became a big fan of his ideas and decided to include some of his smells and heuristics as guidelines. Notice though, that this document is in no way a replacement for his book. I sincerely recommend that you read his book to gain a solid understanding of the rationale behind his recommendations.

I've also decided to include some design guidelines in addition to simple coding guidelines. They are too important to ignore and have a big influence in reaching high quality code.

## 1.6. Is this a coding standard?

The document does not state that projects must comply with these guidelines, neither does it say which guidelines are more important than others. However, we encourage projects to decide themselves which guidelines are important, what deviations a project will use, who is the consultant in case doubts arise, and what kind of layout must be used for source code. Obviously, you should make these decisions before starting the real coding work.

To help you in this decision, I've assigned a level of importance to each guideline:

![](/assets/images/1.png) Guidelines that you should never skip and should be applicable to all situations

![](/assets/images/2.png) Strongly recommended guidelines

![](/assets/images/3.png) May not be applicable in all situations

## 1.7. Feedback and disclaimer

This document has been compiled using many contributions from community members, blog posts, on-line discussions and two decades of developing in C#. If you have questions, comments or suggestions, just let me know by sending me an email at [dennis.doomen@avivasolutions.nl](mailto:dennis.doomen@avivasolutions.nl), [creating an issue](https://github.com/dennisdoomen/csharpguidelines/issues) or Pull Request on GitHub, ping me at [http://twitter.com/ddoomen](http://twitter.com/ddoomen) or join the [Gitter discussions](https://gitter.im/dennisdoomen/CSharpGuidelines). I will try to revise and republish this document with new insights, experiences and remarks on a regular basis.

Notice though that it merely reflects my view on proper C# code so Aviva Solutions will not be liable for any direct or indirect damages caused by applying the guidelines of this document. This document is published under a Creative Commons license, specifically the [Creative Commons Attribution-ShareAlike 4.0](http://creativecommons.org/licenses/by-sa/4.0/) license.

## 1.8 Can I create my own version?

Absolutely. The corresponding [license](https://github.com/dennisdoomen/CSharpGuidelines/blob/master/LICENSE.md) allows you to [fork](https://github.com/dennisdoomen/csharpguidelines/fork), adapt and distribute that modified version within your organization as long as you refer back to the original version here. It's not required, but you would make me a very happy man if you credit me as the original author. And if you have any great ideas, recommendations or corrections, either submit an issue, or even better, fork the repository and provide me with a pull request.
