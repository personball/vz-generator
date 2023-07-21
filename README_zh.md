# vz-generator

用于模板化生成文件的命令行工具(CLI).  

[![NuGet version](https://badge.fury.io/nu/vz-generator.svg)](https://badge.fury.io/nu/vz-generator)

本命令行工具遵循 [dotnet core global tools](https://docs.microsoft.com/zh-cn/dotnet/core/tools/global-tools) 进行开发。  

[English readme](README.md)

# 安装

    dotnet tool install -g vz-generator

# 用法

## 初始化默认配置和示例模板

    vz init 

查看示例名称列表:

    vz init --list-samples

仅加载指定的示例:

    vz init --with-sample abp --with-sample vue.pinia

完成初始化后，请打开配置文件 `.vz/generate.settings.json` 查看或编辑可用选项。

### vscode setup

`vz init` 会创建 `.vscode/settings.json` 以关联 `.vz/generate.settings.schema.json` 和 `.vz/generate.settings.json`. 如果 `.vscode/settings.json` 已经存在，则不会覆盖它，那么你可以复制命令输出的内容自己编辑该文件。

## Settings

TODO path variable

TODO tpl path to output path
TODO file to file
TODO file to folder
TODO folder to folder

## 模板语法

基本的 liquid 语法和内置函数功能请查看 [Scriban](https://github.com/scriban/scriban/blob/master/doc/builtins.md#string-functions).

**请注意**

    由于文件名和路径名不允许出现'|', 故约定对于路径和文件名中需要'|'调用函数的地方，以三个下划线代替，即 '___' 会被替换为 '|'


### 已扩展的命名相关函数

``` liquid
 {{'nameIt'|pascal_case}} =>  NameIt 
 {{'NameIt'|camel_case}}  =>  nameIt 
 {{'NameIt'|kebab_case}}  =>  name-it 
 {{'NameIt'|snake_case}}  =>  name_it 
 {{'person'|pluralize}}   =>  people 
 {{'people'|singularize}} =>  person 
```

## 按你所需模板化生成多个文件以及相应子目录结构

    vz g

默认情况下, g(generate) 子命令会交互式询问你需要执行哪套配置。

你可以用 `-p <your option in .vz/generate.settings.json>` 选项来指定要执行的配置。 

    vz g -p 'Create Abp'

命令会继续询问 `.vz/generate.settings.json` 配置文件中没有指定的内容，比如所声明变量的值， 也可以通过命令行选项 `--var` 提供，如下，可声明多次:

    vz g -p 'Create Abp' --var project=MyCompany.MyProject --var entity=User

# Just do what you want.

可以任意修改 `.vz/generate.settings.json` 配置文件的内容（只要符合`.vz/generate.settings.json` 的定义）, `vz g` 会加载你指定的模板（单个文件或整个目录）, 加载你声明的变量（以及对应的值，未提供则询问）, 最后输出所有文件到你指定的地方。

甚至 `vz g -c <file path to your own settings.json>` 可以覆盖配置文件 `generate.settings.json` 的路径。（文件名也可以不同，不过内容必须匹配 `.vz/generate.settings.schema.json` 的定义）

其他说明请查看 `vz g -h`.

# 参与项目

欢迎任何人 PR 提交可能对其他人有用的任何开发语言、任何场景的模板文件作为默认示例模板。

## Develop on MacOS

IDE: VS Code

DEBUG 模式下使用 `.vzx` 路径，而非 `.vz`。

- 如需调试子命令 `init`:
  - 请检查 `.vscode/launch.json` 中的 `"args"` 配置；
  - 请检查 `.vscode/tasks.json` 中 `build` 任务的 `dependsOn` 配置, 生成前需要执行 `clean .vzx` 和 `zip samples` 任务。
- 如需调试子命令 `g`:
  - 请检查 `.vscode/launch.json` 中的 `"args"` 配置（每个字符串会被视为一个整体，空格不会被拆分）。
  - 请注释掉 `.vscode/tasks.json` 中的 `build` 任务的 `dependsOn` 配置

MacOS 是本项目的默认开发环境，所有默认配置基本可用。

## Develop on windows

IDE: VS Code

DEBUG 模式下使用 `.vzx` 路径，而非 `.vz`。

- 如需调试子命令 `init`:
  - 请检查 `.vscode/launch.json` 中的 `"args"` 配置；
  - 请检查 `.vscode/tasks.json` 中 `build` 任务的 `dependsOn` 配置, 生成前需要执行 `clean .vzx` 和 `zip samples` 任务；
  - `clean .vzx` 任务中所使用的命令应替换为 powershell 的 `Remove-Item` 或者开发者所使用的具体 shell 下的其他等效命令；
  - `zip samples` 任务中所使用的命令应替换为 powershell 的 `Compress-Archive` 或者开发者所使用的具体 shell 下的其他等效命令。
- 如需调试子命令 `g`:
  - 请检查 `.vscode/launch.json` 中的 `"args"` 配置（每个字符串会被视为一个整体，空格不会被拆分）。
  - 请注释掉 `.vscode/tasks.json` 中的 `build` 任务的 `dependsOn` 配置


仅基于 dotnet core + vscode 的开发环境可能会遇到 `resgen` 命令无法使用的问题， visual studio 应支持，但未验证。

# MIT
