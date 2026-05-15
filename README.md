Coding Guidelines for C# (up to v14)
================

See the landing page at https://www.csharpcodingguidelines.com

## How to create the PDFs

From the root of the repo, run:

```
.\build.ps1        # Windows
./build.sh         # macOS / Linux
```

This compiles all guideline rules into two PDF files in the `Artifacts` folder:

- `CSharpCodingGuidelines.pdf` — the full guidelines document
- `CSharpCodingGuidelinesCheatsheet.pdf` — the two-page cheat sheet

Pandoc is downloaded automatically on first run. [Google Chrome](https://www.google.com/chrome/) must be installed for PDF rendering.

## How to preview the site locally

Run the following script from the root of the repo:

```
.\launch-website.ps1        # Windows
./build.sh LaunchWebsite    # macOS / Linux
```

This installs Ruby 3.3 (if not already present), runs `bundle install`, and starts the Jekyll development server at `http://localhost:4000`. The server monitors for changes when using the `--incremental` flag.

### Prerequisites

* [.NET 10 SDK](https://dotnet.microsoft.com/download)
* [Google Chrome](https://www.google.com/chrome/) (for PDF generation)
* Ruby 3.3 is installed automatically when launching the website locally

### Troubleshooting

* Do you receive an error around `jekyll-remote-theme` and `libcurl`? See [this issue on the pages-gem repo](https://github.com/github/pages-gem/issues/526).
* Do you receive an error `Liquid Exception: SSL_connect returned=1 errno=0 state=error: certificate verify failed`? Check out [this solution in the Jekyll repo.](https://github.com/jekyll/jekyll/issues/3985#issuecomment-294266874)
