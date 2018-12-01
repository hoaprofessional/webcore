using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using WebCore.EntityFramework.Data;

namespace WebCore.EntityFramework.Seeds
{
    public class LanguageDetailSeeder : BaseSeeder
    {
        public override void InitDb(WebCoreDbContext context)
        {
            try
            {
                if (!context.LanguageDetails.Any())
                {
                    string currentDirectory = AppDomain.CurrentDomain.BaseDirectory;
                    string baseDir = currentDirectory.Substring(0, currentDirectory.IndexOf("bin\\"))
                      + "Data\\SqlScripts";
                    string path = Path.Combine(baseDir, "StandingData");
                    foreach (string file in Directory.GetFiles(path, "*.sql"))
                    {
                        var lines = File.ReadAllLines(file);
                        foreach(var line in lines)
                        {
                            if(!line.ToLower().Trim().Equals("go") && !String.IsNullOrWhiteSpace(line))
                            {
                                context.Database.ExecuteSqlCommand(line);
                            }
                        }
                    }
                }
            }
            catch(Exception e)
            {
                int a = 5;
            }
            
        }
    }
}
