# vz-generator

Command Line Tool (CLI) for generate files.  

[![NuGet version](https://badge.fury.io/nu/vz-generator.svg)](https://badge.fury.io/nu/vz-generator)

This command is built following [dotnet core global tools](https://docs.microsoft.com/zh-cn/dotnet/core/tools/global-tools).  

[中文版readme](README_zh.md)

# Install

    dotnet tool install -g vz-generator

# Usage

## init to load settings and sample templates

    vz init 

sample names:

    vz init --list-samples

only load specific sample:

    vz init --with-sample abp --with-sample vue.pinia

After init, please see options in `.vz/generate.settings.json`

## Settings

TODO syntax liquid help

TODO tpl path to output path
TODO file to file
TODO file to folder
TODO folder to folder

## Template Syntax

The basic liquid syntax and builtin functions follow [Scriban](https://github.com/scriban/scriban/blob/master/doc/builtins.md#string-functions).

**Notice**

    As name of paths or files not allowed the char '|', so by convention, we use '___' instead of '|'.

### Extended naming functions

``` liquid
 {{'nameIt'|pascal_case}} =>  NameIt 
 {{'NameIt'|camel_case}}  =>  nameIt 
 {{'NameIt'|kebab_case}}  =>  name-it 
 {{'NameIt'|snake_case}}  =>  name_it 
 {{'person'|pluralize}}   =>  people 
 {{'people'|singularize}} =>  person 
```

## Generate any files with subpaths as you want

    vz g

By default, g(generate) subcommand will ask what option you want to run.

You also can use `-p <your option in .vz/generate.settings.json>` to provide answer 

    vz g -p 'Create Abp'

It will continue to ask you for some other setting which not store in `.vz/generate.settings.json`, such as the value of the variables. Also, can be provided as below:

    vz g -p 'Create Abp' --var project=MyCompany.MyProject --var entity=User

# vscode setup

`vz init` will create `.vscode/settings.json` to associate `.vz/generate.settings.schema.json` with `.vz/generate.settings.json`. if `.vscode/settings.json` is already exists, it will not override it, and you should update the file yourself with the content it will show you.

# Just do what you want.

Be free to modify the file `.vz/generate.settings.json`, `vz g` will search the templates as you specified, load variables as you declared, and output the generated files to where you specified.

Even `vz g -c <file path to your own settings.json>` can override the default path to `generate.settings.json`, but the content of it should match the schema as `.vz/generate.settings.schema.json` stored.

Follow the Usage `vz g -h`.

# Contribute

Be FREE to submit (PRs) your own templates as default samples (for any languages any scenarios) which may helpful to others.

## Develop on MacOS

IDE: VS Code

DEBUG mode will use `.vzx` instead of `.vz`

- For DEBUG SubCommand `init`:
  - should check `"args"` in `.vscode/launch.json`
  - should check `dependsOn` of `build` task in `.vscode/tasks.json`, before build should run `clean .vzx` and `zip samples`
- For DEBUG SubCommand `g`:
  - should check `"args"` in `.vscode/launch.json`, and strings in it will treated as tokens (whitespace will not parsed).
  - should commented out the line `dependsOn` of `build` task in `.vscode/tasks.json`

All things work well.

## Develop on windows

IDE: VS Code

DEBUG mode will use `.vzx` instead of `.vz`

- For DEBUG SubCommand `init`:
  - should check `"args"` in `.vscode/launch.json`
  - should check `dependsOn` of `build` task in `.vscode/tasks.json`, before build should run `clean .vzx` and `zip samples`
  - the commands in task `clean .vzx` should be change to powershell `Remove-Item` or sth works in the specific shell.
  - the commands in task `zip samples` should be change to powershell `Compress-Archive` or sth works in the specific shell.
- For DEBUG SubCommand `g`:
  - should check `"args"` in `.vscode/launch.json`, and strings in it will treated as tokens (whitespace will not parsed).
  - should commented out the line `dependsOn` of `build` task in `.vscode/tasks.json`


`resgen` maybe problem on windows when develop with pure dotnet core with vs code, but visual studio should work (not confrim). 

## Template Development

the `-w` option of subommand `g`, will watch the folder which template files belong to, and regenerate the output in time. It facilitates the development of templates.

    vz g -w 

It will not watch the setting file, or ask you for any options or inputs again (only watch templates).

# MIT

# References

- UseCases like [codeBelt/generate-template-files](https://github.com/codeBelt/generate-template-files), but here we got Liquid syntax for template files and paths.

- Liquid [scriban](https://github.com/scriban/scriban)

- [shibayan/Sharprompt](https://github.com/shibayan/Sharprompt)

- [rvegajr/Pluralize.NET.Core](https://github.com/rvegajr/Pluralize.NET.Core)

- Naming [jquense/StringUtils](https://github.com/jquense/StringUtils)