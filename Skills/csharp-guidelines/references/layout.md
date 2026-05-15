# Layout (AV2400)

| Rule | Guideline |
|------|-----------|
| [AV2400](https://csharpcodingguidelines.com/layout-guidelines/#AV2400) | Max 130 chars/line; 4-space indent (no tabs); space between keyword and `(`; spaces around operators; opening and closing braces on their own lines; empty lines between unrelated blocks. |
| [AV2402](https://csharpcodingguidelines.com/layout-guidelines/#AV2402) | Order `using` directives: System namespaces first, then third-party/own in alphabetical order; place `using static` and alias directives last. |
| [AV2406](https://csharpcodingguidelines.com/layout-guidelines/#AV2406) | Order members: private fields/constants → public constants → public static readonly fields → factory methods → constructors → events → public properties → other methods; local functions at the bottom of their method. |
| [AV2407](https://csharpcodingguidelines.com/layout-guidelines/#AV2407) | Don't use `#region`; they hide code and add friction without improving readability. |
| [AV2410](https://csharpcodingguidelines.com/layout-guidelines/#AV2410) | Use expression-bodied member syntax only when the body is a single statement that fits on one line. |
