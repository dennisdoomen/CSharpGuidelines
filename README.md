Coding Guidelines for C# 1.0, 2.0, 3.0, 4.0, 5.0, 6.0, 7.0, 7.1, 7.2 and 7.3
================

See the landing page at http://www.csharpcodingguidelines.com

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

## Troubleshooting

* Do you receive an error around `jekyll-remote-theme` and `libcurl`? See [this issue on the pages-gem repo](https://github.com/github/pages-gem/issues/526).
* Do you receive an error `Liquid Exception: SSL_connect returned=1 errno=0 state=error: certificate verify failed`? Check out [this solution in the Jekyll repo.](https://github.com/jekyll/jekyll/issues/3985#issuecomment-294266874)