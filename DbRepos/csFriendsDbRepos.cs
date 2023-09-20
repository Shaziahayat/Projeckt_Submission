using Configuration;
using Models;
using Models.DTO;
using DbModels;
using DbContext;

using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Reflection.Metadata;

//DbRepos namespace is a layer to abstract the detailed plumming of
//retrieveing and modifying and data in the database using EFC.

//DbRepos implements database CRUD functionality using the DbContext
namespace DbRepos;

public class csPersonDbRepos
{
    private ILogger<csPersonDbRepos> _logger = null;

    #region used before csLoginService is implemented
    private string _dblogin = "sysadmin";
    //private string _dblogin = "gstusr";
    //private string _dblogin = "usr";
    //private string _dblogin = "supusr";
    #endregion


    #region only for layer verification
    private Guid _guid = Guid.NewGuid();
    private string _instanceHello = null;

    static public string Hello { get; } = $"Hello from namespace {nameof(DbRepos)}, class {nameof(csPersonDbRepos)}";
    public string InstanceHello => _instanceHello;
    #endregion


    #region contructors
    public csPersonDbRepos()
    {
        _instanceHello = $"Hello from class {this.GetType()} with instance Guid {_guid}.";
    }
    public csPersonDbRepos(ILogger<csPersonDbRepos> logger):this()
    {
        _logger = logger;
        _logger.LogInformation(_instanceHello);
    }
    #endregion


    #region Admin repo methods
    //implementation using View
    public async Task<gstusrInfoAllDto> InfoAsync()
        => throw new NotImplementedException();

    public async Task<adminInfoDbDto> SeedAsync(loginUserSessionDto usr, int nrOfItems)
        => throw new NotImplementedException();
  
    public async Task<adminInfoDbDto> RemoveSeed(loginUserSessionDto usr, bool seeded)
        => throw new NotImplementedException();
    #endregion


    #region Friends repo methods
    public async Task<IPerson> ReadPersonAsync(loginUserSessionDto usr, Guid id, bool flat)
        => throw new NotImplementedException();

    public async Task<List<IPerson>> ReadPersonsAsync(loginUserSessionDto usr, bool seeded, bool flat, string filter, int pageNumber, int pageSize)
        => throw new NotImplementedException();

    public async Task<IPerson> DeletePersonAsync(loginUserSessionDto usr, Guid id)
       => throw new NotImplementedException();

    public async Task<IPerson> UpdatePersonAsync(loginUserSessionDto usr, csPersonCUdto itemDto)
       => throw new NotImplementedException();

    public async Task<IPerson> CreatePesonAsync(loginUserSessionDto usr, csPersonCUdto itemDto)
       => throw new NotImplementedException();
    #endregion


    #region Addresses repo methods
    public async Task<IAddress> ReadAddressAsync(loginUserSessionDto usr, Guid id, bool flat)
       => throw new NotImplementedException();

    public async Task<List<IAddress>> ReadAddressesAsync(loginUserSessionDto usr, bool seeded, bool flat, string filter, int pageNumber, int pageSize)
       => throw new NotImplementedException();

    public async Task<IAddress> DeleteAddressAsync(loginUserSessionDto usr, Guid id)
       => throw new NotImplementedException();

    public async Task<IAddress> UpdateAddressAsync(loginUserSessionDto usr, csLocationsCUdto itemDto)
       => throw new NotImplementedException();

    public async Task<IAddress> CreateAddressAsync(loginUserSessionDto usr, csLocationsCUdto itemDto)
       => throw new NotImplementedException();
    #endregion


    #region Comment repo methods
    public async Task<IComment> ReadCommentAsync(loginUserSessionDto usr, Guid id, bool flat)
       => throw new NotImplementedException();

    public async Task<List<IComment>> ReadCommentsAsync(loginUserSessionDto usr, bool seeded, bool flat, string filter, int pageNumber, int pageSize)
       => throw new NotImplementedException();

    public async Task<IComment> DeleteCommentAsync(loginUserSessionDto usr, Guid id)
       => throw new NotImplementedException();

    public async Task<IComment> UpdateCommentAsync(loginUserSessionDto usr, csCommentCUdto itemDto)
       => throw new NotImplementedException();

    public async Task<IComment> CreateCommentAsync(loginUserSessionDto usr, csCommentCUdto itemDto)
       => throw new NotImplementedException();
    #endregion


    #region Pets repo methods
    public async Task<IAttraction> ReadAttractionAsync(loginUserSessionDto usr, Guid id, bool flat)
       => throw new NotImplementedException();

    public async Task<List<IAttraction>> ReadAttractionsAsync(loginUserSessionDto usr, bool seeded, bool flat, string filter, int pageNumber, int pageSize)
       => throw new NotImplementedException();

    public async Task<IAttraction> DeleteAttractionAsync(loginUserSessionDto usr, Guid id)
       => throw new NotImplementedException();

    public async Task<IAttraction> UpdateAttractionAsync(loginUserSessionDto usr, csAttractionCUdto itemDto)
       => throw new NotImplementedException();

    public async Task<IAttraction> CreateAttractionAsync(loginUserSessionDto usr, csAttractionCUdto itemDto)

       => throw new NotImplementedException();

    public Task<IPerson> CreatePersonAsync(loginUserSessionDto usr, csPersonCUdto item)
    {
        throw new NotImplementedException();
    }

    #endregion
}
