using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;


using Configuration;
using Models;
using Models.DTO;
using Services;

using DbContext;
using DbModels;
using DbRepos;
using Microsoft.Extensions.Logging;
using System.Data.Common;

//ConsoleAPP namespace is the top layer in the stack and contains the business
//, i.e. application logic. Using the other layers this layer can easily
//be switched from one type of application to another

//Once all the layers are setup with its own references. ConsoleApp will
//depend ONLY on Configuration, DbModels and Services.
//This allows the Application to be independed from any database implementation
namespace ConsoleApp;

class Program
{
    //used for seeding
    const int _nrSeeds = 1000;
    const int _nrUsers = 40;
    const int _nrSuperUsers = 40;

    //used when huge nr of data, read pages of _readerPageSize items, instead of all items
    const int _readerPageSize = 1000;

    static async Task Main(string[] args)
    {
        //Allows a Console App to use .NET Dependecy Injection pattern,
        //by runnign the App within a host
        HostApplicationBuilder builder = Host.CreateApplicationBuilder(args);

        //DI injects the csFriendService
        builder.Services.AddSingleton<IPersonsService, csPersonServiceModel>();

        //Build the host
        using IHost host = builder.Build();

        #region To be removed for the real application
        Verification(host);

        //var sg = new csSeedGenerator();
        //var person = new List<csPerson>();
        //for(int i = 0;  i < 100; i++)
        //{
        //    var f = new csPerson();
        //    f.Seed(sg);
        //    person.Add(f);
        //}

        // hård koppling mellan service och appconsole

        var service = host.Services.CreateAsyncScope().ServiceProvider.GetRequiredService<IPersonsService>();
        //var service1 = new csPersonServiceModel();
        var _info = service.Seed(null,100);

        var person = service.ReadPersons(null,false,false,null,0,0);


        Console.WriteLine(person.Count);
        Console.WriteLine(person.First());
        Console.WriteLine();
        Console.WriteLine(person.Last());
        Console.WriteLine();
        Console.WriteLine("************************************");



        #endregion

        //Terminate the host and the Application properly

        await host.RunAsync();
    }

    #region used for basic verificaton
    private static void Verification(IHost host)
    {
        Console.WriteLine("Verification start");
        Console.WriteLine("------------------");

        //to verify the layers are accessible
        Console.WriteLine("\nLayer access:");
        Console.WriteLine(csAppConfig.Hello);
        Console.WriteLine(csPerson.Hello);

        Console.WriteLine(csPersonDbM.Hello);
        Console.WriteLine(csMainDbContext.Hello);
        Console.WriteLine(csPersonDbRepos.Hello);

        Console.WriteLine(csLoginService.Hello);
        Console.WriteLine(csJWTService.Hello);
        Console.WriteLine(csPersonServiceModel.Hello);
        Console.WriteLine(csPersonsServiceDb.Hello);

        //to verify connection strings can be read from appsettings.json
        Console.WriteLine($"\nDbConnections:\nDbLocation: {csAppConfig.DbSetActive.DbLocation}" +
            $"\nDbServer: {csAppConfig.DbSetActive.DbServer}");
        Console.WriteLine("DbUserLogins in DbSet:");
        foreach (var item in csAppConfig.DbSetActive.DbLogins)
        {
            Console.WriteLine($"   DbUserLogin: {item.DbUserLogin}" +
                              $"\n   DbConnection: {item.DbConnection}\n   ConString: <secret>");
        }

        Console.WriteLine("\nVerification end");
        Console.WriteLine("------------------\n\n");
    }
    #endregion

    #region used when seeding of model IFriend, IAddress, IPet, IQuote
    private static async Task FriendServiceSnapshot(IPersonsService friendService)
    {

        loginUserSessionDto _usr = new loginUserSessionDto { UserRole = "sysadmin" };

        var _info = await friendService.RemoveSeedAsync(_usr, true);
        Console.WriteLine($"\n{_info.nrSeededPersons} friends removed");

        _info = await friendService.SeedAsync(_usr, _nrSeeds);
        Console.WriteLine($"{_info.nrSeededPersons} friends seeded");

        var _list = await friendService.ReadPersonsAsync(_usr, true, false, null, 0, _readerPageSize);
        Console.WriteLine("\nFirst 5 friends");
        _list.Take(5).ToList().ForEach(f => Console.WriteLine(f));

        Console.WriteLine("\nLast 5 friends");
        _list.TakeLast(5).ToList().ForEach(f => Console.WriteLine(f));
    }

    private static async Task FriendServiceInfo(IPersonsService PersonService)
    {
        var info = await PersonService.InfoAsync;
        Console.WriteLine($"\nFriendServiceInfo:");
        Console.WriteLine($"Nr of seeded friends: {info.Db.nrSeededPersons}");
        Console.WriteLine($"Nr of unseeded friends: {info.Db.nrUnseededPersons}");
        Console.WriteLine($"Nr of friends with address: {info.Db.nrPersonWithAddress}");

        Console.WriteLine($"Nr of addresses: {info.Db.nrSeededAddresses}");
        Console.WriteLine($"Nr of unseeded addresses: {info.Db.nrUnseededAddresses}");

        Console.WriteLine($"Nr of pets: {info.Db.nrSeededAttractions}");
        Console.WriteLine($"Nr of unseeded pets: {info.Db.nrUnseededAttractions}");

        Console.WriteLine($"Nr of quotes: {info.Db.nrSeededComments}");
        Console.WriteLine($"Nr of unseeded quotes: {info.Db.nrUnseededComments}");
        Console.WriteLine();
    }
    #endregion

    #region used when seeding of model IUser
    private static async Task LoginServiceSnapshot(ILoginService loginService)
    {
        var _info = await loginService.SeedAsync(_nrUsers, _nrSuperUsers);
        Console.WriteLine($"{_info.NrUsers} users seeded");
        Console.WriteLine($"{_info.NrSuperUsers} superusers seeded");
    }
    #endregion

    #region used for login
    private static async Task LoginServiceLogin(ILoginService loginService)
    {
        var _usrCreds = new loginCredentialsDto { UserNameOrEmail = "user1", Password = "user1" };

        try
        {
            var _usr = await loginService.LoginUserAsync(_usrCreds);
            Console.WriteLine($"\n{_usr.UserName} logged in");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
    #endregion
}

