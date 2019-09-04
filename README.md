Coding Guidelines for C# (up to version 7.3)
================

See the landing page at https://www.csharpcodingguidelines.com

## How to create the PDFs

1. From the root of the repo, run the `build.bat` script to build the HTML versions of the guidelines and cheat sheet.
2. From the `Artifacts` folder, open the `CSharpCodingGuidelines.htm` into your favorite browser and print as PDF. Use the default margins, no headers and footers, and set the scaling to 80% for A4 sheets.
3. Do the same for the `CSharpCodingGuidelinesCheatsheet.htm`, but use  landscape orientation and minimum margins. A scaling of 84% should allow you to print the cheat sheet as a double-sided A4 sheet. Just make sure you disable headers and footers, and  enable printing background graphics to keep the colored boxes with auxiliary information.

## How to build this site

### Prerequisites

* Ruby 2.4.x (note: 2.5 and higher may fail due to the `ffi` ruby lib only supporting < 2.5)
* Ruby DevKit
* The `bundler` gem (`gem install bundler`)

### Building

* Clone this repository
* `cd` into the root of the repository
* Run `bundle install`
* Run `bundle exec jekyll serve`

### Troubleshooting

* Do you receive an error around `jekyll-remote-theme` and `libcurl`? See [this issue on the pages-gem repo](https://github.com/github/pages-gem/issues/526).
* Do you receive an error `Liquid Exception: SSL_connect returned=1 errno=0 state=error: certificate verify failed`? Check out [this solution in the Jekyll repo.](https://github.com/jekyll/jekyll/issues/3985#issuecomment-294266874)