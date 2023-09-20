using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;
using System.Linq;

using Configuration;
using Models;
using Models.DTO;

//DbModels namespace is the layer which contains all the C# models of
//the database tables Select queries as well as results from a call to a View,
//Stored procedure, or Function.

//C# classes corresponds to table structure (no suffix) or
//specific search results (DTO suffix)
namespace DbModels;

public class csPersonDbM : csPerson, ISeed<csPersonDbM>
{
    static public new string Hello { get; } = $"Hello from namespace {nameof(DbModels)}, class {nameof(csPersonDbM)}";

    [Key]       // for EFC Code first
    public override Guid PersonId { get; set; }

 

    [JsonIgnore]
    [ForeignKey("AdressID")]
    public virtual csAddressDbM CsAddressDbM { get; set; }

    [NotMapped]
    public override IAddress Address { get => CsAddressDbM; set => new NotImplementedException(); }

    [NotMapped] //application layer can continue to read a List of attractions without code change
    public override List<IAttraction> Attractions { get => AttractionsDbm?.ToList<IAttraction>(); set => new NotImplementedException(); }

    [JsonIgnore]
    public virtual List<csAttractionDbM> AttractionsDbm { get; set; } = null;

    //a person can have 0 or many Comments
    [NotMapped] //application layer can continue to read a List of comments without code change
    public override List<IComment> Comments { get => CommentsDbm?.ToList<IComment>(); set => new NotImplementedException(); }

    [JsonIgnore]
    public virtual List<csCommentDbM> CommentsDbm { get; set; } = null;


    #region randomly seed this instance
    public override csPersonDbM Seed(csSeedGenerator sgen)
    {
        base.Seed(sgen);
        return this;
    }
    #endregion

    #region Update from DTO
    public csPersonDbM UpdateFromDTO(csPersonCUdto org)
    {
        FirstName = org.FirstName;
        LastName = org.LastName;
        Birthday = org.Birthday;

        return this;
    }
    #endregion

    #region constructors
    public csPersonDbM() { }
    public csPersonDbM(csPersonCUdto org)
    {
        PersonId = Guid.NewGuid();
        UpdateFromDTO(org);
    }
    #endregion

}

