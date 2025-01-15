//בס"ד
using System.CommandLine;
var rootCommand = new RootCommand();
var command = new Command("bundle", "Executes the bundle command.");
rootCommand.AddCommand(command);
command.SetHandler(() => {
    Console.WriteLine("!!! wow !!!");
});
rootCommand.InvokeAsync(args);
var option1 = new Option<string>("--language");
command.AddOption(option1);
var option2 = new Option<FileInfo>("--output");
command.AddOption(option2);
var option3 = new Option<bool>("--note");
command.AddOption(option3);
var option4 = new Option<string>("--sort");
command.AddOption(option4);
var option5 = new Option<bool>("--remove-empty-lines");
command.AddOption(option5);
var option6 = new Option<string>("--author");
command.AddOption(option6);

