# Class Design (AV1000)

| Rule | Guideline |
|------|-----------|
| [AV1000](https://csharpcodingguidelines.com/class-design-guidelines/#AV1000) | A class or interface should have a single, well-defined purpose (Single Responsibility Principle). |
| [AV1001](https://csharpcodingguidelines.com/class-design-guidelines/#AV1001) | Constructors should return a fully initialized, usable object without requiring extra property-setting afterward. |
| [AV1002](https://csharpcodingguidelines.com/class-design-guidelines/#AV1002) | Only pass to a constructor the dependencies that most or all methods need. |
| [AV1003](https://csharpcodingguidelines.com/class-design-guidelines/#AV1003) | Interfaces should be small, focused, and named to explain their role (Interface Segregation Principle). |
| [AV1004](https://csharpcodingguidelines.com/class-design-guidelines/#AV1004) | Expose extension points as interfaces, not base classes. |
| [AV1005](https://csharpcodingguidelines.com/class-design-guidelines/#AV1005) | Use interfaces to decouple classes, prevent bidirectional associations, and enable dependency injection. |
| [AV1008](https://csharpcodingguidelines.com/class-design-guidelines/#AV1008) | Avoid static classes except for extension method containers; they are hard to test and violate DI. |
| [AV1010](https://csharpcodingguidelines.com/class-design-guidelines/#AV1010) | Don't suppress compiler warning CS0114 using `new`; fix the design instead (breaks polymorphism). |
| [AV1011](https://csharpcodingguidelines.com/class-design-guidelines/#AV1011) | Honor the Liskov Substitution Principle: a derived type must be usable wherever its base type is expected. |
| [AV1013](https://csharpcodingguidelines.com/class-design-guidelines/#AV1013) | Don't refer to derived classes from a base class. |
| [AV1014](https://csharpcodingguidelines.com/class-design-guidelines/#AV1014) | Avoid exposing the objects a class depends on (Law of Demeter / "Don't talk to strangers"). |
| [AV1020](https://csharpcodingguidelines.com/class-design-guidelines/#AV1020) | Avoid bidirectional dependencies between classes or assemblies. |
| [AV1025](https://csharpcodingguidelines.com/class-design-guidelines/#AV1025) | Classes should have both state and behavior; avoid data-only or behavior-only splits. |
| [AV1026](https://csharpcodingguidelines.com/class-design-guidelines/#AV1026) | Protect internal state consistency: validate public inputs and guard invariants at construction and mutation. |
