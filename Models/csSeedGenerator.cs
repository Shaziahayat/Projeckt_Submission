using System.Collections.Generic;
using System.Xml.Linq;

namespace Models
{
    public class UserComments 
    {
        public string CommentText { get; set; }
        public int Rating { get; set; }
        public UserComments() { }

        public UserComments(string commentText, int rating)
        {
            CommentText = commentText;
            Rating = Math.Clamp(rating, 1, 10);
        }
    }

    public interface ISeed<T>
    {
        //In order to separate from real and seeded instances
        public bool Seeded { get; set; }

        //Seeded The instance
        public T Seed(csSeedGenerator seedGenerator);
    }

    public class csSeedGenerator : Random
    {

        string[] _firstnames = "Harry, Lord, Hermione, Albus, Severus, Ron, Draco,Frodo, Gandalf, Sam, Peregrin, Saruman, Smith, Johnson,Williams, Jones".Split(",");
    

        string[] _lastnames = "Potter, Voldemort, Granger, Dumbledore, Snape, Malfoy, Baggins, the Gray, Gamgee, Took, the White".Split(", ");



        string[][] _city =
            {
                "Stockholm, Göteborg, Malmö, Uppsala, Linköping, Örebro".Split(", "),
                "Oslo, Bergen, Trondheim, Stavanger, Dramen".Split(", "),
                "Köpenhamn, Århus, Odense, Aahlborg, Esbjerg".Split(", "),
                "Helsingfors, Espoo, Tampere, Vaanta, Oulu".Split(", "),
                "Södermalm, Haga, Västra Hamnen, Gamla Stan,Kungsholmen".Split(","),
                "Kalmar,Trelleborg,Kristianstad,Karlskrona,Sköv,Visby,Örnsköldsvik,Kiruna".Split(","),
             };

        string[][] _address =
            {
                "Svedjevägen, Ringvägen, Vasagatan, Odenplan, Birger Jarlsgatan, Äppelviksvägen, Kvarnbacksvägen".Split(", "),
                "Bygdoy alle, Frognerveien, Pilestredet, Vidars gate, Sågveien, Toftes gate, Gardeveiend".Split(", "),
                "Rolighedsvej, Fensmarkgade, Svanevej, Gröndalsvej, Githersgade, Classensgade, Moltekesvej".Split(", "),
                "Arkandiankatu, Liisankatu, Ruoholahdenkatu, Pohjoistranta, Eerikinkatu, Vauhtitie, Itainen Vaideki".Split(", "),
                

              };

        string[] _CategoryName = "Vasa Museum, Skansen,ABBA The Museum, Munchmuseet, Frammuseet, ARKEN Museum,Rosenborgs".Split(", ");

        string[] _country = "Sweden, Norway, Denmark, Finland ".Split(", ");


        string[] _domains = "icloud.com, me.com, mac.com, hotmail.com, gmail.com".Split(", ");



        UserComments[] _commentText = {

            //About 
            new UserComments("Nice place for a walk, but not very exciting.", 3),
            new UserComments("Beautiful architecture and tranquil atmosphere", 4),
            new UserComments("One of my favorite places in Copenhagen!", 6),
            new UserComments("A must for anyone who loves history.", 9),
            new UserComments("A good place to get panoramic views of the city", 4),
            new UserComments("A nice place to visit with the family?", 2),
            new UserComments("The architecture was really impressive.", 2),
            new UserComments("The tower was okay, but expected more.", 4),
            new UserComments("Visited during a school trip, had a fantastic time.", 4),
            new UserComments("The most beautiful place I visited.", 4),
            new UserComments("Lovely place to relax at.",4),
            new UserComments("Worth a visit for the view from the top.", 7),
            new UserComments("Interesting, but not as impressive as I expected.", 9),
            new UserComments("A bit crowded, but worth a visit.", 8),
            new UserComments("A peaceful oasis in the middle of the city.",6),
            new UserComments("Fantastic attraction!",9),
            new UserComments("Magnificent waterfall and great photo opportunities.",9),
            new UserComments("Beautiful place, but it can get crowded during peak season.", 6),
            new UserComments("Incredible exhibits and engaging stories.",2),
            new UserComments("The best place to learn about Iceland's history..", 5),
            new UserComments("Fantastic view of the city.", 8),
            new UserComments("Nice place for a walk, but not very exciting.", 6),
            new UserComments("Beautiful architecture and tranquil atmosphere", 8),
            new UserComments("One of my favorite places in Copenhagen!", 3),
            new UserComments("Fantastic museum! A must-visit in Stockholm",8),
            new UserComments("Impressive ships, but a bit crowded.",7),
            new UserComments("Loved the history and exhibitions.",4),
            new UserComments("Great place to visit.",2),
            new UserComments("A lovely place to spend the day, especially with kids.",7),
            new UserComments("Fun for the whole family.",3),
            new UserComments("So much to see and learn!", 6),
            new UserComments("I recommend a visit",5),
            new UserComments("Super fun amusement park! Highly recommended..",3),
            new UserComments("Good rides, but a bit expensive.",5),
            new UserComments("An amazing place for both children and adults.",7),
            new UserComments("Interesting exhibitions, but could use some updates.",2),
        };

        public string CategoryName => _CategoryName[this.Next(0, _CategoryName.Length)];

        public string FirstName => _firstnames[this.Next(0, _firstnames.Length)];
        public string LastName => _lastnames[this.Next(0, _lastnames.Length)];
        public string FullName => $"{FirstName} {LastName}";

        public DateTime getDateTime(int? fromYear = null, int? toYear = null)
        {
            bool dateOK = false;
            DateTime _date = default;
            while (!dateOK)
            {
                fromYear ??= DateTime.Today.Year;
                toYear ??= DateTime.Today.Year + 1;

                try
                {
                    int year = this.Next(Math.Min(fromYear.Value, toYear.Value),
                        Math.Max(fromYear.Value, toYear.Value));
                    int month = this.Next(1, 13);
                    int day = this.Next(1, 32);

                    _date = new DateTime(year, month, day);
                    dateOK = true;
                }
                catch
                {
                    dateOK = false;
                }
            }

            return DateTime.SpecifyKind(_date, DateTimeKind.Utc);  //Used for Postgres compatibility - only UTC is supported
        }

        // General random truefalse
        public bool Bool => (this.Next(0, 10) < 5) ? true : false;

       

        public string Email(string fname = null, string lname = null)
        {
            fname ??= FirstName;
            lname ??= LastName;

            return $"{fname}.{lname}@{_domains[this.Next(0, _domains.Length)]}";
        }

        public string Phone => $"{this.Next(700, 800)} {this.Next(100, 1000)} {this.Next(100, 1000)}";

        public string Country => _country[this.Next(0, _country.Length)];

        public string City(string Country = null)
        {

            var cIdx = this.Next(0, _city.Length);
            if (Country != null)
            {
                //Give a City in that specific country
                cIdx = Array.FindIndex(_country, c => c.ToLower() == Country.Trim().ToLower());

                if (cIdx == -1) throw new Exception("Country not found");
            }

            return _city[cIdx][this.Next(0, _city[cIdx].Length)];
        }

    public string StreetAddress(string Country = null)
    {

        var cIdx = this.Next(0, _city.Length);
        if (Country != null)
        {
            //Give a City in that specific country
            cIdx = Array.FindIndex(_country, c => c.ToLower() == Country.Trim().ToLower());

            if (cIdx == -1) throw new Exception("Country not found");
        }

        return $"{_address[cIdx][this.Next(0, _address[cIdx].Length)]} {this.Next(1, 51)}";
    }

    public int ZipCode => this.Next(10101, 100000);

    #region Seed from own datastructures
    public TEnum FromEnum<TEnum>() where TEnum : struct
    {
        if (typeof(TEnum).IsEnum)
        {

            var _names = typeof(TEnum).GetEnumNames();
            var _name = _names[this.Next(0, _names.Length)];

            return Enum.Parse<TEnum>(_name);
        }
        throw new ArgumentException("Not an enum type");
    }

        //Create a list of seeded  Enum items
        public IAttraction FromList<IAttraction>(List<IAttraction> Category)
    {
        return Category[this.Next(0, Category.Count)];
    }
    #endregion

    #region generate seeded Lists
    public List<TItem> ToList<TItem>(int NrOfItems)
        where TItem : ISeed<TItem>, new()
    {
        //Create a list of seeded items
        var _list = new List<TItem>();
        for (int c = 0; c < NrOfItems; c++)
        {
            _list.Add(new TItem().Seed(this));
        }
        return _list;
    }

    public List<TItem> ToListUnique<TItem>(int tryNrOfItems, List<TItem> appendToUnique = null)
         where TItem : ISeed<TItem>, IEquatable<TItem>, new()
    {
        //Create a list of uniquely seeded items
        HashSet<TItem> _set = (appendToUnique == null) ? new HashSet<TItem>() : new HashSet<TItem>(appendToUnique);

        while (_set.Count < tryNrOfItems)
        {
            var _item = new TItem().Seed(this);

            int _preCount = _set.Count();
            int tries = 0;
            do
            {
                _set.Add(_item);
                if (++tries >= 5)
                {
                    //it takes more than 5 tries to generate a random item.
                    //Assume this is it. return the list
                    return _set.ToList();
                }
            } while (!(_set.Count > _preCount));
        }

        return _set.ToList();
    }


    public List<TItem> FromListUnique<TItem>(int tryNrOfItems, List<TItem> list = null)
    where TItem : ISeed<TItem>, IEquatable<TItem>, new()
    {
        //Create a list of uniquely seeded items
        HashSet<TItem> _set = new HashSet<TItem>();

        while (_set.Count < tryNrOfItems)
        {
            var _item = list[this.Next(0, list.Count)];

            int _preCount = _set.Count();
            int tries = 0;
            do
            {
                _set.Add(_item);
                if (++tries >= 5)
                {
                    //it takes more than 5 tries to generate a random item.
                    //Assume this is it. return the list
                    return _set.ToList();
                }
            } while (!(_set.Count > _preCount));
        }

        return _set.ToList();
    }

        #endregion
       

        #region Comment
        public List<UserComments> AllComments => _commentText.ToList<UserComments>();

        public List<UserComments> Comments(int NrOfItems)
        {
            //Create a list of seeded items
            var _list = new List<UserComments>();
            for (int c = 0; c < NrOfItems; c++)
            {
                _list.Add(new UserComments { CommentText = comment.CommentText });
            }
            return _list;
        }
        public UserComments comment => _commentText[this.Next(0, _commentText.Length)];


        #endregion
    }
}