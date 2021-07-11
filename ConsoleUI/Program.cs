using System;
using System.Net.Http;
using System.Threading.Tasks;
using Business.Abstracts;
using Business.Concretes;
using Core.DataAccess.MongoDb;
using Core.Entities.Concretes;
using DataAccess.Concretes;
using MongoDB.Bson;
using MongoDB.Driver;

namespace ConsoleUI
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var user = new User()
            {
               FirstName = "Test",
               LastName = "Test",
               Email = "test@test.com",

            };

            Console.WriteLine("Fields");
            foreach (var field in typeof(User).GetFields())
            {
                Console.WriteLine(field.Name);
            }
            Console.WriteLine("----------------------");
            Console.WriteLine("Properties");
            foreach (var field in typeof(User).GetProperties())
            {
                Console.WriteLine(field.Name);
                Console.WriteLine(field.GetValue(user));
            }


            //var data = Connect().GetCollection<BsonDocument>("Users").Find(new BsonDocument()).FirstOrDefault();
            //Console.WriteLine(data.ToString());

            

            //var data2 = Connect().GetCollection<BsonDocument>("Users").InsertOneAsync(user.ToBsonDocument());
            //Console.WriteLine(data2);

            /* MongoDbRepositoryBase<User> repository = new MongoDbRepositoryBase<User>();
            var result = repository.Insert(user);
            var result1 = repository.GetById("60eb08f4b628c1d39a6fa898");

            Console.WriteLine(result.Wait(1000));
            foreach (var user1 in repository.GetAll())
            {
                Console.WriteLine("{0} id {1} email",user1.Id, user1.Email);
            }
            Console.WriteLine("-------------------------");
            Console.WriteLine(result1.Email); */

            //if (!result.HasLastErrorMessage)
            //{
            //  Console.WriteLine("Kayıt başarı ile eklendi");
            //}

            /* IUserService userService = new UserManager(new UserDal());
            var result = userService.Add(user); 
            Console.WriteLine(result.Message);
            //Console.WriteLine(GetUrlContentLengthAsync());
            */

            /* Task<int> returnedTaskTResult = GetTaskOfTResultAsync();
            int intResult = await returnedTaskTResult;
            // Single line
            // int intResult = await GetTaskOfTResultAsync();
            Console.WriteLine(intResult);
            Task returnedTask = GetTaskAsync();
            await returnedTask;
            // Single line
            await GetTaskAsync(); */
        }

        public IMongoDatabase Connect()
        {
            var settings = MongoClientSettings.FromConnectionString(
                "mongodb+srv://drCmd:gyuayhd3hhDfIeUA@mymessageappdb.dw6rz.mongodb.net/MyMessage?retryWrites=true&w=majority");
            var client = new MongoClient(settings);
            var database = client.GetDatabase("MyMessage");
            return database;
        }

        public static async Task<string> GetUrlContentLengthAsync()
        {
            var client = new HttpClient();

            Task<string> getStringTask =
                client.GetStringAsync("https://docs.microsoft.com/dotnet");

            DoIndependentWork();

            string contents = await getStringTask;

            return contents;
        }

        static void DoIndependentWork()
        {
            Console.WriteLine("Working...");
        }

        static async Task<int> GetTaskOfTResultAsync()
        {
            int hours = 5;
            await Task.Delay(10);

            return hours;
        }


        

        async Task GetTaskAsync()
        {
            await Task.Delay(0);
            // No return statement needed
        }

        
    }
}
