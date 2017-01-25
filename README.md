Coding Guidelines for C# 3.0, 4.0 and 5.0
================

[![Join the chat at https://gitter.im/dennisdoomen/csharpguidelines](https://badges.gitter.im/Join%20Chat.svg)](https://gitter.im/dennisdoomen/csharpguidelines?utm_source=badge&utm_medium=badge&utm_campaign=pr-badge&utm_content=badge)
[![Build status](https://ci.appveyor.com/api/projects/status/abdiejvl9jp9h60l?svg=true)](https://ci.appveyor.com/project/dennisdoomen/csharpguidelines)

##What is this##
This document attempts to provide guidelines (or coding standards if you like) for coding in C# 3.0, 4.0 or 5.0 that are both useful and pragmatic. Of course, if you create such a document you should practice what you preach. So rest assured, these guidelines are representative to what we at [Aviva Solutions](http://www.avivasolutions.nl) do in our day-to-day work. Notice that not all guidelines have a clear rationale. Some of them are simply choices we made at Aviva Solutions. In the end, it doesn't matter what choice you made, as long as you make one and apply it consistently.

##Why would you use this document?##
Although some might see coding guidelines as undesired overhead or something that limits creativity, this approach has already proven its value for many years. This is because not every developer:

- is aware that code is generally read 10 times more than it is changed;
- is aware of the potential pitfalls of certain constructions in C#;
- is up to speed on certain conventions when using the .NET Framework such as `IDisposable` or the deferred execution nature of LINQ;
- is aware of the impact of using (or neglecting to use) particular solutions on aspects like security, performance, multi-language support, etc;
- realizes that not every developer is as capable, skilled or experienced to understand elegant, but potentially very abstract solutions;

##Where do I get them?##
Go to the [Releases](https://github.com/dennisdoomen/CSharpGuidelines/releases) page to find the latest HTML, PDF and other related files.

##Can I create my own version?##
Absolutely. The corresponding [license](https://github.com/dennisdoomen/CSharpGuidelines/blob/master/LICENSE.md) allows you to fork, adapt and distribute that modified version within your organization as long as you refer back to the original version here. It's not required, but you would make me a very happy man if you credit me as the original author. And if you have any great ideas, recommendations or corrections, either submit an issue, or even better, fork the repository and provide me with a pull request. Just run the following command-line to compile the Markdown versions of the guidelines and cheatsheet to self-contained HTML files.

  `build.bat`
##Are there any other languages available?##
Yes, [Sergey Russkikh](https://twitter.com/Russkikh_Sergey) maintains a [Russian translation](https://github.com/SergeyRusskih/CSharpGuidelines.Russian) through his fork.
