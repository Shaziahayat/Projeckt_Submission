using System;
using System.Collections.Generic;
using Configuration;

//DbModels namespace is the layer which contains all the C# models of
//the database tables Select queries as well as results from a call to a View,
//Stored procedure, or Function.

//C# classes corresponds to table structure (no suffix) or
//specific search results (DTO suffix)
namespace Models;

public class csPerson : IPerson, ISeed<csPerson>
{
    static public string Hello { get; } = $"Hello from namespace {nameof(Models)}, class {nameof(csPerson)}";

    public virtual Guid PersonId { get; set; }

    public virtual string FirstName { get; set; }
    public virtual string LastName { get; set; }

    public virtual string Email { get; set; }
    public DateTime? Birthday { get; set; } = null;

    //One person can only have one address
    public virtual IAddress Address { get; set; } = null;

    //One person can have many favorite Attractions


    public virtual List<IAttraction> Attractions { get; set; } = null;

    //One person can have many  Comment
    public virtual List<IComment> Comments { get; set; } = null;


    public string FullName => $"{FirstName} {LastName}";




    public override string ToString()
    {
        var sRet = $"{FullName} [{PersonId}]";

        if (Comments != null)
            sRet += $"\n   {Comments}";
        else
            sRet += $"\n  - Has no comments";


        if (Attractions != null && Attractions.Count > 0)
        {
            sRet += $"\n  - Have seen attractions";
            foreach (var Attraction in Attractions)
            {
                sRet += $"\n     {Attraction}";
            }
        }
        else
            sRet += $"\n  - Have not seen attrctions";

        if (Birthday != null)
        {
            sRet += $"\n  - Has birthday on {Birthday:D}";
        }

        if(Email != null)
        {
            sRet += $"\n Email : {Email}";
        }

        return sRet;
    }

    #region contructors
    public csPerson() { }

    public csPerson(csPerson org)
    {
        this.Seeded = org.Seeded;

        this.PersonId = org.PersonId;
        this.FirstName = org.FirstName;
        this.LastName = org.LastName;
        this.Email = org.Email;

        //use the ternary operator to create only if the orginal is not null
        this.Address = (org.Address != null)? new csAddress((csAddress)org.Address): null;

        //using Linq Select and copy contructor to create a list copy
        this.Attractions = (org.Attractions != null) ? org.Attractions.Select(p => new csAttraction((csAttraction) p)).ToList<IAttraction>() : null;
    }
    #endregion

    #region randomly seed this instance
    public bool Seeded { get; set; } = false;

    public virtual csPerson Seed(csSeedGenerator sgen)
    {
        this.PersonId = Guid.NewGuid();
        this.FirstName = sgen.FirstName;
        this.LastName = sgen.LastName;
        this.Birthday = sgen.getDateTime(1990, 2020);
        this.Email = sgen.Email(this.FirstName, this.LastName).ToString();

        var attList = new List<csAttraction>();
        for (int i = 0; i < sgen.Next(0,5); i++)
        {
            var at = new csAttraction();
            at.Seed(sgen);
            attList.Add(at);
        }
        this.Attractions = attList.ToList<IAttraction>();

        //this.Comments = sgen.Comments(10).ToList<IComment>();

        //var List = new List<csAttraction>();
        //for (int i = 0; i < sgen.Next(0, 10); i++)
        //{
        //    var at = new csComment();
        //    at.Seed(sgen);
        //    attList.Add(at);
        //}
        //var comment = new csComment();
        //comment.Seed(sgen);
        //this.Comments
        
        //this.Comments = pcomment.ToList<IComment>();
        return this;
    }
    #endregion
}

