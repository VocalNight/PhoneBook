using ConsoleTableExt;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneBook
{
    public class CreateTableEngine
    {
        public static void ShowTable<T>( List<T> tableData, [AllowNull] string tableName ) where T : class
        {
            Console.Clear();
            if (tableName == null)
                tableName = "";

            Console.WriteLine("\n\n");

            ConsoleTableBuilder
                .From(tableData)
                .WithColumn("Id", "Name", "Phone Number", "E-mail", "Category")
                .ExportAndWriteLine();
            Console.WriteLine("\n\n");
        }
    }
}
