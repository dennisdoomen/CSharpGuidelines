# AGENTS.md — C# Coding Guidelines Repository

## About this repository

This is the **dennisdoomen/CSharpGuidelines** repository — a community-maintained reference for C# coding standards covering all C# versions up to v10. The site is built with **Jekyll** (Ruby) and hosted on GitHub Pages.

## Repository structure

| Path | Purpose |
|------|---------|
| `_rules/*.md` | Individual guideline rules. Each file has YAML frontmatter (`rule_id`, `rule_category`, `title`, `severity`) followed by Markdown body. |
| `_pages/*.md` | Category landing pages that aggregate rules by `rule_category`. |
| `_includes/` | Shared Markdown partials (introduction, cheatsheet content). |
| `_layouts/` | Jekyll layouts, including `rule-category` which renders rules per page. |
| `_data/navigation.yml` | Sidebar navigation definition. |
| `_sass/` | SCSS styles. |
| `_config.yml` | Jekyll site configuration. |

## Working with rules

- Each rule lives in `_rules/<id>.md`. The filename matches `rule_id`.
- `severity` values: `1` = Must · `2` = Should · `3` = May
- Valid `rule_category` values: `class-design`, `member-design`, `misc`, `maintainability`, `naming-conventions`, `performance`, `dotnet-framework-usage`, `commenting`, `layout`
- Rule body is standard Markdown. Jekyll/Liquid template syntax (`{{ site.default_rule_prefix }}`) is used for cross-references — preserve this when editing existing rules.

## Building the site

```bash
bundle install
bundle exec jekyll serve
```

Or use `start_site.bat` on Windows.

## Skills

When writing or reviewing **C# code** (not site/template code), apply the guidelines defined in:

→ [`.agents/skills/csharp-guidelines/SKILL.md`](.agents/skills/csharp-guidelines/SKILL.md)
