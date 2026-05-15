## 1.1. What is this?

This document provides practical and valuable coding guidelines (or coding standards if you like) for all versions of C# up to and including v14. If you create such a document, you should practice what you preach, so these guidelines are representative of what we at [Aviva Solutions](https://www.avivasolutions.nl) do in our day-to-day work. Not all guidelines have a universal rationale; some are explicit choices we made at Aviva Solutions. In the end, what matters most is that you make deliberate choices and apply them consistently.

These guidelines are also used by many projects inside and outside Aviva Solutions, in particular the [.NET Starter Kit](https://github.com/dennisdoomen/dotnet-starter-kit), a great companion project to see these guidelines in action.

If you want to get started quickly:

- Read this document once end-to-end to understand its scope and intent.
- Decide which guidelines are mandatory for your project and document any agreed deviations.
- Enforce those decisions through code reviews, analyzers and IDE tooling.

## 1.2. Why would you use this document?

Although some might see coding guidelines as undesired overhead or something that limits creativity, this approach has already proven its value for many years. This is because not every developer:

- realizes that code is generally read 10 times more than it is changed;
- knows the potential pitfalls of certain C# constructs;
- is up to speed on conventions in .NET such as `IDisposable`, `async`/`await`, and the deferred execution nature of LINQ;
- recognizes the impact of using (or neglecting) particular solutions on aspects such as security, performance, and multi-language support;
- realizes that not every developer is as capable, skilled or experienced to understand elegant, but potentially very abstract solutions;

## 1.3. Basic principles

There are many unexpected things I run into during my work as a consultant, each deserving at least one guideline. Unfortunately, I still need to keep this document within a reasonable size. But unlike what some junior developers believe, that doesn't mean that something is okay just because it is not mentioned in this document.

In general, if I have a discussion with a colleague about a smell that this document does not cover, I'll refer back to the [General Guidelines](/general-guidelines/) that apply to all situations, regardless of context.

The only remaining exception is generated code; it should not need to comply with coding guidelines. However, if it is possible to modify the templates used for generation, try to make them generate code that complies as much as possible.

Regardless of the elegance of someone's solution, if it's too complex for the ordinary developer, exposes unusual behavior, or tries to solve many possible future issues, it is very likely the wrong solution and needs redesign. A common anti-pattern response to these principles is: "But it works."

## 1.4. How do you get started?

- Ask all developers to carefully read this document at least once. This will give them a sense of the kind of guidelines the document contains.
- Make sure there are always a few hard copies of the [Cheat Sheet](https://github.com/dennisdoomen/CSharpGuidelines/releases/latest) close at hand.
- Include the most critical coding guidelines on your [Pull Request template](https://docs.github.com/en/communities/using-templates-to-encourage-useful-issues-and-pull-requests/creating-a-pull-request-template-for-the-repository) and verify the remainder as part of your [Peer Review](https://www.continuousimprover.com/2010/02/tfs-development-practices-part-2-peer.html).
- Install the [Skills](https://github.com/dennisdoomen/csharpguidelines/blob/master/Skills) into your project so your preferred AI agent can use them during code reviews.
- Consider forking the [original sources](https://github.com/dennisdoomen/csharpguidelines) on [GitHub](https://github.com/) and creating your own [internal](https://github.com/dennisdoomen/csharpguidelines/blob/master/LICENSE.md) version of the document.
- JetBrains' [ReSharper](https://www.jetbrains.com/resharper/) and their fully fledged Visual Studio replacement [Rider](https://www.jetbrains.com/rider/) have an intelligent code inspection engine that, with some configuration, already supports many aspects of the Coding Guidelines. It automatically highlights any code that does not match the rules for naming members (e.g. Pascal or Camel casing), detects dead code, and many other things. One click of the mouse button (or the corresponding keyboard shortcut) is usually enough to fix it.
- Both ReSharper and Rider have a File Structure window that displays an overview of the members of your class or interface, and allows you to easily rearrange them using a simple drag-and-drop action.
- [CSharpGuidelinesAnalyzer](https://github.com/bkoelman/CSharpGuidelinesAnalyzer) verifies over 40 of our guidelines while typing code in Visual Studio 2017-2022 and during CI builds. An updated ReSharper settings file is included.
- Many of these guidelines are also enforced by [Roslyn analyzers](https://learn.microsoft.com/en-us/visualstudio/code-quality/roslyn-analyzers-overview). You can configure them centrally to apply them to all projects in your solution. Refer to the [.NET Library Starter Kit](https://github.com/dennisdoomen/dotnet-library-starter-kit/tree/main/templates/Source) for a `Directory.Build.props` and `.editorconfig` to get you started.
- If you need to use newer C# language features in projects targeting older .NET versions, consider [PolySharp](https://github.com/Sergio0694/PolySharp). It provides polyfills for many modern C# features, allowing you to write modern C# even when targeting older runtimes.

## 1.5. Why did we create it?

The idea started in 2002 when Vic Hartog (Philips Medical Systems) and I were assigned to write a coding standard for C# 1.0. Since then, I've regularly added, removed, and changed rules based on practical experience, community feedback, and new tooling support in the .NET ecosystem. Special thanks go to [Bart Koelman](https://github.com/bkoelman) for being a very active contributor over all those years.

After reading [Robert C. Martin](https://cleancoder.com/)'s book [Clean Code: A Handbook of Agile Software Craftsmanship](http://www.amazon.com/Clean-Code-Handbook-Software-Craftsmanship/dp/0132350882), I became a big fan of his ideas and included some of his smells and heuristics as guidelines. Notice, though, that this document is in no way a replacement for his book. I sincerely recommend reading it to gain a solid understanding of the rationale behind his recommendations.

I've also included design guidelines in addition to coding guidelines. They are too important to ignore and have a big influence on reaching high-quality code.

## 1.6. Is this a coding standard?

The document does not state that projects must comply with these guidelines, neither does it say which guidelines are more important than others. However, we encourage projects to decide themselves which guidelines are important, what deviations a project will use, who is the consultant in case doubts arise, and what kind of layout must be used for source code. Obviously, you should make these decisions before starting the real coding work.

## 1.7. Feedback and disclaimer

This document has been compiled using many contributions from community members, blog posts, online discussions, and two decades of C# development experience. If you have questions, comments, or suggestions, let me know by sending an email to [dennis.doomen@avivasolutions.nl](mailto:dennis.doomen@avivasolutions.nl), [creating an issue](https://github.com/dennisdoomen/csharpguidelines/issues) or pull request on GitHub, or pinging me on [Bluesky](https://bsky.app/profile/ddoomen.bsky.social) or [Mastodon](https://mastodon.social/@ddoomen). I will try to revise and republish this document regularly with new insights, experiences, and remarks.

Notice, though, that this document merely reflects my view on proper C# code, so Aviva Solutions will not be liable for any direct or indirect damages caused by applying these guidelines. This document is published under a permissive license, specifically the [Creative Commons Attribution-ShareAlike 4.0](http://creativecommons.org/licenses/by-sa/4.0/) license.

## 1.8 Can I create my own version?

Absolutely. The corresponding [license](https://github.com/dennisdoomen/CSharpGuidelines/blob/master/LICENSE.md) allows you to [fork](https://github.com/dennisdoomen/csharpguidelines/fork), adapt, and distribute a modified version within your organization, as long as you refer back to the original version here. It's not required, but I would really appreciate it if you credit me as the original author. And if you have great ideas, recommendations, or corrections, either submit an issue or, even better, fork the repository and provide a pull request.
