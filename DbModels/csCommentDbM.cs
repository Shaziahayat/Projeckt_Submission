using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Reflection.Emit;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

using Models;
using Models.DTO;

namespace DbModels
{
    public class csCommentDbM : csComment, ISeed<csCommentDbM>, IEquatable<csCommentDbM>
    {
        [Key]       // for EFC Code first
        public override Guid CommentId { get; set; }


        [NotMapped] //application layer can continue to read a List of attractions without code change
        public override List<IPerson> Persons { get => PersonDbMs?.ToList<IPerson>(); set => new NotImplementedException(); }

        [JsonIgnore]
        public virtual List<csPersonDbM> PersonDbMs { get; set; } = null;
        #region constructors
        public csCommentDbM() : base() { }
        public csCommentDbM(UserComments usercomments) : base(usercomments) { }
        public csCommentDbM(csCommentCUdto org)
        {
            CommentId = Guid.NewGuid();
            UpdateFromDTO(org);
        }
        #endregion
        
        #region implementing IEquatable

        public bool Equals(csCommentDbM other) => (other != null) ? ((CommentText, Rating) ==
            (other.CommentText, other.Rating)) : false;

        public override bool Equals(object obj) => Equals(obj as csCommentDbM);
        public override int GetHashCode() => (CommentText, Rating).GetHashCode();

        #endregion

        #region randomly seed this instance
        public override csCommentDbM Seed(csSeedGenerator sgen)
        {
            base.Seed(sgen);
            return this;
        }
        #endregion

        #region Update from DTO
        public csComment UpdateFromDTO(csCommentCUdto org)
        {
            if (org == null) return null;

            CommentText = org.CommentText;
            Rating = org.Rating;

            return this;
        }
        #endregion
    }
}

