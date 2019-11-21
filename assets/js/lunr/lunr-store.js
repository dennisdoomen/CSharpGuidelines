---
layout: null
---

var store = [
  {%- assign docs = site.pages | where_exp:'doc','doc.search == true' -%}
  {%- for doc in docs -%}
    {
      "title": {{ doc.title | jsonify }},
      "excerpt":
        {%- if site.search_full_content == true -%}
          {{ doc.content |
            replace:"</p>", " " |
            replace:"</h1>", " " |
            replace:"</h2>", " " |
            replace:"</h3>", " " |
            replace:"</h4>", " " |
            replace:"</h5>", " " |
            replace:"</h6>", " "|
          strip_html | strip_newlines | jsonify }},
        {%- else -%}
          {{ doc.content |
            replace:"</p>", " " |
            replace:"</h1>", " " |
            replace:"</h2>", " " |
            replace:"</h3>", " " |
            replace:"</h4>", " " |
            replace:"</h5>", " " |
            replace:"</h6>", " "|
          strip_html | strip_newlines | truncatewords: 50 | jsonify }},
        {%- endif -%}
      "categories": {{ doc.categories | jsonify }},
      "tags": {{ doc.tags | jsonify }},
      "url": {{ doc.url | absolute_url | jsonify }},
      "teaser":
        {%- if teaser contains "://" -%}
          {{ teaser | jsonify }}
        {%- else -%}
          {{ teaser | absolute_url | jsonify }}
        {%- endif -%}
    }{%- unless forloop.last and l -%},{%- endunless -%}
  {%- endfor -%}
  {%- for rule in site.rules -%}
    {% assign full_rule_id = site.default_rule_prefix | append: rule.rule_id  %}
    {% assign category_page = site.pages | where: "rule_category", rule.rule_category | first  %}
    {
      "title": {{ full_rule_id | append: ": " | append: rule.title | jsonify }},
      "excerpt":
        {%- if site.search_full_content == true -%}
          {{ rule.content |
            replace:"</p>", " " |
            replace:"</h1>", " " |
            replace:"</h2>", " " |
            replace:"</h3>", " " |
            replace:"</h4>", " " |
            replace:"</h5>", " " |
            replace:"</h6>", " "|
          strip_html | strip_newlines | jsonify }},
        {%- else -%}
          {{ rule.content |
            replace:"</p>", " " |
            replace:"</h1>", " " |
            replace:"</h2>", " " |
            replace:"</h3>", " " |
            replace:"</h4>", " " |
            replace:"</h5>", " " |
            replace:"</h6>", " "|
          strip_html | strip_newlines | truncatewords: 50 | jsonify }},
        {%- endif -%}
      "categories": {{ rule.categories | jsonify }},
      "tags": {{ rule.tags | jsonify }},
      "url": {{ category_page.permalink | append: "#" | append: full_rule_id | absolute_url | jsonify }},
      "teaser":
        {%- if teaser contains "://" -%}
          {{ teaser | jsonify }}
        {%- else -%}
          {{ teaser | absolute_url | jsonify }}
        {%- endif -%}
    }{%- unless forloop.last and l -%},{%- endunless -%}
  {%- endfor -%}
]