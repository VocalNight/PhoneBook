using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneBook
{
    public static class Commands
    {
        public enum contactCategory { family, friends, work }

        public static void ListAllContacts()
        {
            using (var db = new AppDb())
            {
                CreateTableEngine.ShowTable(db.PhoneBook.AsNoTracking().ToList(), "Contacts");
            }
        }

        public static void AddPhonebook()
        {
            PhoneBook phonebook = CreatePhonebook();

            using (var db = new AppDb())
            {
                db.PhoneBook.Add(phonebook);
                db.SaveChanges();
            }
        }

        private static PhoneBook CreatePhonebook()
        {
            Console.WriteLine("------------");
            Console.Write("Contact name: ");
            string name = Console.ReadLine();

            Console.Write("Contact Phone: ");
            string phone = Console.ReadLine();

            Console.Write("Contact Email: ");
            string email = Console.ReadLine();

            Console.WriteLine("What category?");
            Console.WriteLine(@"
1 - Family
2 - Work
3 - Friends");
            string category = Console.ReadKey().Key switch
            {
                ConsoleKey.NumPad1 => Commands.contactCategory.family.ToString(),
                ConsoleKey.NumPad2 => Commands.contactCategory.work.ToString(),
                ConsoleKey.NumPad3 => Commands.contactCategory.friends.ToString(),
            };

            Console.Clear();

            return new PhoneBook()
            {
                Name = name,
                PhoneNumber = phone,
                Email = email,
                Category = category,
            };         
        }

        private static PhoneBook QueryForPhoneId(int phoneId)
        {
            using (var db = new AppDb())
            {
                PhoneBook? phone = db.PhoneBook
                    .Where(phone => phone.PhoneBookId == phoneId)
                    .FirstOrDefault();

                return phone;
            }
        }

        private static PhoneBook QueryForPhoneName( string phoneName )
        {
            using (var db = new AppDb())
            {
                PhoneBook? phone = db.PhoneBook
                    .Where(phone => phone.Name == phoneName)
                    .FirstOrDefault();

                return phone;
            }
        }

        public static void UpdatePhone()
        {

            Console.Clear();

            Console.WriteLine("What id you want to update? ");
            int id = int.Parse(Console.ReadLine());

            PhoneBook phonebook = QueryForPhoneId(id);

            if (phonebook == null)
            {
                Console.WriteLine("Id not found!");
                return;
            }

            PhoneBook newPhonebook = CreatePhonebook();

            using (var db = new AppDb())
            {
                phonebook = newPhonebook;
                db.SaveChanges();
            } 
        }

        public static void DeletePhone()
        {
            Console.Clear();

            Console.WriteLine("What id you want to delete? ");
            int id = int.Parse(Console.ReadLine());

            PhoneBook phonebook = QueryForPhoneId(id);

            if (phonebook == null)
            {
                Console.WriteLine("Id not found!");
                return;
            }

            using (var db = new AppDb())
            {
                db.Remove(QueryForPhoneId(id));
                db.SaveChanges();
            }
        }

        public static void LookUpSpecificPhone()
        {
            Console.Clear();

            Console.WriteLine("What name you want to look up? ");
            string contactName = Console.ReadLine();

            List<PhoneBook> phonebookList = new();
            PhoneBook phonebook = QueryForPhoneName(contactName);
            phonebookList.Add(phonebook);

            if (phonebook == null)
            {
                Console.WriteLine("Id not found!");
                return;
            }

            CreateTableEngine.ShowTable(phonebookList, "Contacts");
        }

        public static void PopulateTestData( this AppDb db )
        {
            var phone = new List<PhoneBook>
            {
                new PhoneBook
                {
                    Name = "Test",
                    Category = contactCategory.family.ToString(),
                    PhoneNumber = "33518131",
                    Email = "test@test.com"
                }
            };
            
            db.PhoneBook.AddRange( phone );
            db.SaveChanges();
        }
    }
}
