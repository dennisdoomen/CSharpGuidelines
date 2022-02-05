Coding Guidelines for C# (up to version 9.0)
================

See the landing page at https://www.csharpcodingguidelines.com

## How to create the PDFs

1. From the root of the repo, run the `build.bat` script to build the HTML versions of the guidelines and cheat sheet.
2. From the `Artifacts` folder, open the `CSharpCodingGuidelines.htm` into your favorite browser and print as PDF. Use the default margins, no headers and footers, and set the scaling to 80% for A4 sheets.
3. Do the same for the `CSharpCodingGuidelinesCheatsheet.htm`, but use  landscape orientation and minimum margins. A scaling of 84% should allow you to print the cheat sheet as a double-sided A4 sheet. Just make sure you disable headers and footers, and  enable printing background graphics to keep the colored boxes with auxiliary information.

## How to build this site

### Prerequisites

* Ruby 3.1. 
    * An easy way to install is to use `choco install ruby`.
    * Or use the **Ruby+Devkit installer** from [RubyInstaller for Windows](https://rubyinstaller.org/downloads/archives/).
    * Note that you may have to reopen your command shell to get the `ruby --version` command to work,
* The `bundler` 
    * Run `gem install bundler` to install it. If you receive SSL-related errors while running gem install, try running `refreshenv` first.

### Building

* Clone this repository
* Run `bundle install`
* Run `bundle exec jekyll serve`. To have it monitor your working directory for changes, add the `--incremental` option.

### Troubleshooting

* Do you receive an error around `jekyll-remote-theme` and `libcurl`? See [this issue on the pages-gem repo](https://github.com/github/pages-gem/issues/526).
* Do you receive an error `Liquid Exception: SSL_connect returned=1 errno=0 state=error: certificate verify failed`? Check out [this solution in the Jekyll repo.](https://github.com/jekyll/jekyll/issues/3985#issuecomment-294266874)
