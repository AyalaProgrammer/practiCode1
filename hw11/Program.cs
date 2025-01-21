
using System;
using System.CommandLine;
using System.CommandLine.IO;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

 var rootCommand = new RootCommand();

 var command = new Command("bundle", "Executes the bundle command.");

var option1 = new Option<string>("--language", "רשימת שפות תכנה.");
option1.AddAlias("-l");
var option2 = new Option<FileInfo>("--output", "שם קובץ הבונדל המיוצא.");
 option2.AddAlias("-o");
 var option3 = new Option<bool>("--note", "אם להוסיף הערת מקור לקובץ.");
option3.AddAlias("-n");
var option4 = new Option<string>("--sort", "סדר העתקת קבצי הקוד.");
option4.AddAlias("-s");
var option5 = new Option<bool>("--remove-empty-lines", "אם למחוק שורות ריקות.");
option5.AddAlias("-r");
var option6 = new Option<string>("--author", "שם יוצר הקובץ.");
 option6.AddAlias("-a");

        command.AddOption(option1);
        command.AddOption(option2);
        command.AddOption(option3);
        command.AddOption(option4);
        command.AddOption(option5);
        command.AddOption(option6);

command.SetHandler(async (string language, FileInfo output, bool note, string sort, bool removeEmptyLines, string author) =>
{
            
   if (output == null || string.IsNullOrWhiteSpace(output.FullName))
   {
     Console.WriteLine("eneter output file.");
     return; 
   }

   if (string.IsNullOrWhiteSpace(language))
   {
       Console.WriteLine("enter a lang.");
       return; 
   }

    var files = Directory.GetFiles(Directory.GetCurrentDirectory());      
    var bundleContent = new StringBuilder();
    foreach (var file in files)
    {
               
       if (Path.GetExtension(file)==language)
       {
         var fileContent = File.ReadAllText(file);

                  
        if (removeEmptyLines)
        {
          fileContent = string.Join(Environment.NewLine, fileContent.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries));
        }

        bundleContent.AppendLine(fileContent);        
         if (note)
         {
            bundleContent.AppendLine($"// src: {file}");
         }
       }
               
    }

           
  try
  {
   File.WriteAllText(output.FullName, bundleContent.ToString());
   Console.WriteLine($"created: {output.FullName}");
  }
  catch (Exception ex)
  {
   Console.WriteLine($"error in save: {ex.Message}");
  }

            
     Console.WriteLine($"שפת תכנה: {language}");
     Console.WriteLine($"שם קובץ פלט: {output}");
     Console.WriteLine($"האם לרשום מקור קוד: {note}");
     Console.WriteLine($"סדר העתקה: {sort}");
     Console.WriteLine($"האם למחוק שורות ריקות: {removeEmptyLines}");
     Console.WriteLine($"שם יוצר הקובץ: {author}");
 },option1,option2,option3,option4,option5,option6);

var rspCommand = new Command("create_rsp", "easy way to use bundle command");
rspCommand.SetHandler(() =>
{
    try
    {
        string lang = "", output, author;
        char sort;
        bool note, remove;
        string command = "bundle";
        try
        {
            Console.WriteLine("what is  the first languages you want? enter :");
            lang = Console.ReadLine();
            while (lang != "-1")
            {
                command += " -l" + lang;
                Console.WriteLine("Do you want more languages? enter, to exit enter -1?");
                lang = Console.ReadLine();
            }
            Console.WriteLine("enter the path to create your bundle, if you don't want enter -1");
            output = Console.ReadLine();
            Console.WriteLine("do you want to sort according type (T) or name (N)");
            sort = char.Parse(Console.ReadLine());
            Console.WriteLine("do you want to save the source of each file? enter true/false");
            note = bool.Parse(Console.ReadLine());
            Console.WriteLine("write the author, if you don't want enter -1");
            author = Console.ReadLine();
            Console.WriteLine("do you want to remove empty lines?true/false");
            remove = bool.Parse(Console.ReadLine());

            if (output == "-1") { output = Path.Combine(Directory.GetCurrentDirectory(), "bundle.txt"); }
            command += $" -o \"{output}\"";
            if (sort == 'T') { command += " -s type"; } else { command += " -s name"; }
            if (note) { command += " -n"; }
            if (remove) { command += " -r"; }
            if (author != "-1") { command += $" -a \"{author}\""; }
            string fileName = "response.rsp";
            File.WriteAllText(fileName, command);
            Console.WriteLine("now enter: hw11 @response.rsp");
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
    }
    catch (Exception e) { Console.WriteLine(e.Message); }
});
rootCommand.AddCommand(rspCommand); 
rootCommand.AddCommand(command);
await rootCommand.InvokeAsync(args);
  

