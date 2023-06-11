using PhoneBook;


CreateDb();

static void CreateDb()
{
    using (var db = new AppDb())
    {
        db.Database.EnsureDeleted();
        db.Database.EnsureCreated();
        Commands.PopulateTestData(db);
        Console.WriteLine("Database created!");
    }

    do
    {
        Console.WriteLine("Welcome to the Phonebook App, use the numpad to select an option");
        Console.WriteLine(@"
1 - Add new contact
2 - Update a contact
3 - Delete a contact
4 - List all contacts
5 - Search for specific name");

        ConsoleKey key = Console.ReadKey().Key;

        switch (key)
        {
            case ConsoleKey.NumPad1:
                Commands.AddPhonebook();
                break;
            case ConsoleKey.NumPad2:
                Commands.UpdatePhone();
                break;
            case ConsoleKey.NumPad3:
                Commands.DeletePhone();
                break;
            case ConsoleKey.NumPad4:
                Commands.ListAllContacts();
                break;
            case ConsoleKey.NumPad5:
                Commands.LookUpSpecificPhone();
                break;
        }

    } while (true);
}