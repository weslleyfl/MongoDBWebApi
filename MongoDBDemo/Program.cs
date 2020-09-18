using static System.Console;

namespace MongoDBDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            WriteLine("Hello World!");

            MongoCRUD db = new MongoCRUD("AddressBook");

            //PersonModel person = new PersonModel()
            //{
            //    FristName = "Joao",
            //    LastName = "Da Silva",
            //    PrimaryAddress = new AddressModel
            //    {
            //        StreetAddress = "101 oak Street",
            //        City = "Contagem",
            //        State = "MG",
            //        ZipCode = "32323232"
            //    }
            //};

            // create

            //db.InsertRecord("User", new PersonModel() { FristName = "Maravilha", LastName = "Lopes" });
            //db.InsertRecord("User", person);

            // list

            //var recs = db.LoadRecords<PersonModel>("User");

            //foreach (var rec in recs)
            //{
            //    WriteLine($"{rec.Id} : {rec.FristName} {rec.LastName}");

            //    if (rec.PrimaryAddress != null)
            //    {
            //        WriteLine(rec.PrimaryAddress.City);
            //    }

            //}

            var recs = db.LoadRecords<NameModel>("User");

            foreach (var rec in recs)
            {
                WriteLine($"{rec.FristName} {rec.LastName}");
                WriteLine();
            }

            // Get by Id

            //var person = db.LoadRecordById<PersonModel>("User", Guid.Parse("0af03115-b9be-480c-822c-418b3019eed3"));

            // update
            //person.DateOfBirth = new DateTime(1979, 10, 31, 0, 0, 0, DateTimeKind.Utc);
            //db.UpdateInsertRecord("User", person.Id, person);

            // delete
            //db.DeleteRecord<PersonModel>("User", person.Id);

            ReadLine();
        }
    }
}
