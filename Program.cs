using System.CommandLine;
using vz_generator.Commands;
using vz_generator.Initializer;

class Program
{
    static async Task<int> Main(string[] args)
    {
        var rootCommand = new RootCommand("VZeroSoft CLI Tools");

        var initCommand = new InitCommand();
        // initCommand.SetOptions();
        initCommand.SetHandler(async (ctx) => await initCommand.InitAsync(ctx));
        rootCommand.AddCommand(initCommand);

        var generateCommand = new GenerateCommand();
        generateCommand.AddAlias("g");
        generateCommand.SetHandler(async (ctx) => await generateCommand.GenerateAsync(ctx));
        rootCommand.AddCommand(generateCommand);

        return await rootCommand.InvokeAsync(args);
    }
}
// Use Case:
// 1. 根据指定的单个文件（或目录中多个）模版（liquid语法），结合指定的json对象（作为liquid变量），生成文件；
// 1.2 文件名和路径名都可以支持变量替换；
// 1.3 在 .vz 目录下可以有一个配置文件，指定多个固定的选项，对应各自的一套配置；
// 1.4 文件生成所需要的配置，如果未配齐全，则可以通过命令行交互的方式进行配置补充；
// 1.5 对于 .vz 目录下的预定义配置，可以通过命令选项进行配置覆盖，可以通过选项提供命令行交互可能涉及的所有配置
// CLI 非功能性需求：
// a. 执行 generate 子命令前，检查当前目录中是否存在.vz目录，如果没有，则终止执行，并提示用户执行 init 子命令进行初始化
// b. init 子命令，生成实例配置以及示例模版，其中示例模版按各种开发框架进行区分
// TODO:c. 模板引擎可以切换，以支持不同的模板语法
// d. 兼容 __name__(camelCase) 语法，预处理为 {{ name | camelCase}}, done: 不兼容， ___ 替换为 |
// e. 自定义指令 directive
// 命名规范的处理 & 单词复数
// TODO: 所有输出信息的本地化