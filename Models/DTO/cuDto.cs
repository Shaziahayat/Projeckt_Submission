using System;
using System.ComponentModel.Design;
using System.Diagnostics.Metrics;
using System.Reflection.Emit;
using System.Xml.Linq;

namespace Models.DTO
{
    //DTO is a DataTransferObject, can be instanstiated by the controller logic
    //and represents a, fully instatiable, subset of the Database models
    //for a specific purpose.

    //These DTO are simplistic and used to Update and Create objects in the database
    public class csPersonCUdto
    {
        public virtual Guid? PersonId { get; set; }

        public virtual string FirstName { get; set; }
        public virtual string LastName { get; set; }

        public virtual string Email { get; set; }

        public DateTime? Birthday { get; set; } = null;

        public virtual Guid? AddressId { get; set; } = null;

        public virtual List<Guid> AttractionId { get; set; } = null;

        public virtual List<Guid> CommentId { get; set; } = null;

        public csPersonCUdto() { }
        public csPersonCUdto(IPerson org)
        {
            PersonId = org.PersonId;
            FirstName = org.FirstName;
            LastName = org.LastName;
            Email = org.Email;
            Birthday = org.Birthday;

            AddressId = org?.Address?.AddressId;
            AttractionId = org.Attractions?.Select(i => i.AttractionId).ToList();
            CommentId = org.Comments?.Select(i => i.CommentId).ToList();
        }
    }

    public class csLocationsCUdto
    {
        public virtual Guid? AddressId { get; set; }

        public virtual string StreetAddress { get; set; }
        public virtual int ZipCode { get; set; }
        public virtual string City { get; set; }
        public virtual string Country { get; set; }

        public csLocationsCUdto() { }
        public csLocationsCUdto(IAddress org)
        {
            AddressId = org.AddressId;
            StreetAddress = org.StreetAddress;
            ZipCode = org.ZipCode;
            City = org.City;
            Country = org.Country;
        }
    }

    public class csAttractionCUdto
    {
        //cannot be nullable as a Pets has to have an owner even when created
        public virtual Guid PersonId { get; set; }

        public virtual Guid? AttractionId { get; set; }

        public virtual enCategory Category { get; set; }
        public string CategoryName { get; set; }

        public csAttractionCUdto() { }
        public csAttractionCUdto(IAttraction org)
        {
            PersonId = org.Person.PersonId;
            AttractionId = org.AttractionId;
            Category = org.Category;
        }
    }
    public class csCommentCUdto
    {
        public virtual Guid? CommentId { get; set; }
        public int Rating { get; set; }
        public string CommentText { get; set; }

        public csCommentCUdto() { }
        public csCommentCUdto(IComment org)
        {
            CommentId = org.CommentId;

            CommentText = org.CommentText;
            Rating = org.Rating;
        }
    }
}

