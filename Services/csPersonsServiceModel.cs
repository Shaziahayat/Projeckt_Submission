using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

using Configuration;
using Models;
using Models.DTO;

using DbModels;
using DbContext;
using DbRepos;
using Services;
using System.Linq;

//Service namespace is an abstraction of using services without detailed knowledge
//of how the service is implemented.
//Service is used by the application layer using interfaces. Thus, the actual
//implementation of a service can be application dependent without changing code
//at application
namespace Services
{
    //IFriendsService ensures application layer can access csFriendsServiceModel
    //Person model (without database) OR access csFriendsServiceDbRepos
    //FriendsDbM model (with database)class csFriendsServiceDbRepos without
    //without any code change
    public class csPersonServiceModel : IPersonsService
    {
        private ILogger<csPersonServiceModel> _logger = null;
        private object _locker = new object();

        #region only for layer verification
        private Guid _guid = Guid.NewGuid();
        private string _instanceHello;

        public string InstanceHello => _instanceHello;

        static public string Hello { get; } = $"Hello from namespace {nameof(Services)}, class {nameof(csPersonServiceModel)}" +

            // added after project references is setup
            $"\n   - {csPersonDbRepos.Hello}" +
            $"\n   - {csMainDbContext.Hello}";
        #endregion

        #region constructors
        public csPersonServiceModel()
        {
            
        }
        public csPersonServiceModel(ILogger<csPersonServiceModel> logger)
        {
            _logger = logger;

            //only for layer verification
            _instanceHello = $"Hello from class {this.GetType()} with instance Guid {_guid}. " +
                $"Will use ModelOnly, no repo.";

            _logger.LogInformation(_instanceHello);
        }
        #endregion

        private List<csPerson> _persons = new List<csPerson>();

        public List<csPerson> SeedandRead(int nrofitems)
        {
            
            var sg = new csSeedGenerator();
            _persons = sg.ToList<csPerson>(100);

            //for (int i = 0; i < nrofitems; i++)
            //{
            //    var f = new csPerson();
            //    f.Seed(sg);
            //    _persons.Add(f);
            //}
            return _persons;
        }

        public Task<adminInfoDbDto> RemoveSeedAsync(loginUserSessionDto usr, bool seeded) => Task.Run(() =>
        {
            lock (_locker) { return RemoveSeed(usr, seeded); }
        });
        public adminInfoDbDto RemoveSeed(loginUserSessionDto usr, bool seeded)
            => throw new NotImplementedException();


        public Task<adminInfoDbDto> SeedAsync(loginUserSessionDto usr, int nrOfItems) => Task.Run(() =>
        {
            lock (_locker) { return Seed(usr, nrOfItems); }
        });
        public adminInfoDbDto Seed(loginUserSessionDto usr, int nrOfItems)
        {
           var _seed = new csSeedGenerator();
            _persons = _seed.ToList<csPerson>(nrOfItems);

            var _info = new adminInfoDbDto();
            _info.nrSeededPersons = _persons.Count;
            return _info;

        }
          // => throw new NotImplementedException();

        //In order to make ReadAsync it has to return a deep copy of _Persons.
        //Otherwise another Task could seed or removeseed on the list while first
        //Task is working on the list. Use copy constructor pattern
        public Task<List<IPerson>> ReadPersonsAsync(loginUserSessionDto usr, bool seeded, bool flat, string filter, int pageNumber, int pageSize) => Task.Run(() =>
        {
            lock (_locker) {

                //to create a a copy is simple using linq and copy constructor pattern
                var list = (_persons != null) ? _persons.Select(f => new csPerson(f)).ToList<IPerson>() : null;
                return list;
            }
        });
        public List<IPerson> ReadPersons(loginUserSessionDto usr, bool seeded, bool flat, string filter, int pageNumber, int pageSize) => _persons.ToList<IPerson>();


        public Task<gstusrInfoAllDto> InfoAsync => Task.Run(() =>
        {
            lock (_locker) { return Info; }
        });
        public gstusrInfoAllDto Info
           => throw new NotImplementedException();


        #region not implemented
        
        public Task<IPerson> ReadPersonAsync(loginUserSessionDto usr, Guid id, bool flat) => throw new NotImplementedException();
        public Task<IPerson> DeletePersonAsync(loginUserSessionDto usr, Guid id) => throw new NotImplementedException();
        public Task<IPerson> UpdatePersonAsync(loginUserSessionDto usr, csPersonCUdto item) => throw new NotImplementedException();
        public Task<IPerson> CreatePersonAsync(loginUserSessionDto usr, csPersonCUdto item) => throw new NotImplementedException();

        public Task<List<IAddress>> ReadAddressesAsync(loginUserSessionDto usr, bool seeded, bool flat, string filter, int pageNumber, int pageSize) => throw new NotImplementedException();
        public Task<IAddress> ReadAddressAsync(loginUserSessionDto usr, Guid id, bool flat) => throw new NotImplementedException();
        public Task<IAddress> DeleteAddressAsync(loginUserSessionDto usr, Guid id) => throw new NotImplementedException();
        public Task<IAddress> UpdateAddressAsync(loginUserSessionDto usr, csLocationsCUdto item) => throw new NotImplementedException();
        public Task<IAddress> CreateAddressAsync(loginUserSessionDto usr, csLocationsCUdto item) => throw new NotImplementedException();

        public Task<List<IComment>> ReadCommentsAsync(loginUserSessionDto usr, bool seeded, bool flat, string filter, int pageNumber, int pageSize) => throw new NotImplementedException();
        public Task<IComment> ReadCommentAsync(loginUserSessionDto usr, Guid id, bool flat) => throw new NotImplementedException();
        public Task<IComment> DeleteCommentAsync(loginUserSessionDto usr, Guid id) => throw new NotImplementedException();
        public Task<IComment> UpdateCommentAsync(loginUserSessionDto usr, csCommentCUdto item) => throw new NotImplementedException();
        public Task<IComment> CreateCommentAsync(loginUserSessionDto usr, csCommentCUdto item) => throw new NotImplementedException();

        public Task<List<IAttraction>> ReadAttractionsAsync(loginUserSessionDto usr, bool seeded, bool flat, string filter, int pageNumber, int pageSize) => throw new NotImplementedException();
        public Task<IAttraction> ReadAttractionAsync(loginUserSessionDto usr, Guid id, bool flat) => throw new NotImplementedException();
        public Task<IAttraction> DeleteAttractionAsync(loginUserSessionDto usr, Guid id) => throw new NotImplementedException();
        public Task<IAttraction> UpdateAttractionAsync(loginUserSessionDto usr, csAttractionCUdto item) => throw new NotImplementedException();
        public Task<IAttraction> CreateAttractionAsync(loginUserSessionDto usr, csAttractionCUdto item) => throw new NotImplementedException();

        
        #endregion
    }
}
