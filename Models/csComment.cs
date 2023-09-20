using System;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Models
{
    public class csComment : IComment, ISeed<csComment>
    {
        
        public virtual Guid CommentId { get; set; }
        public virtual string CommentText { get; set; }
        public virtual int Rating { get; set; }

        //One person can have many comments
        public virtual List<IPerson> Persons { get; set; } = null;

        #region constructors
        public csComment() { }

        public csComment(UserComments usercomments)
        {
            CommentId = Guid.NewGuid();
            CommentText = usercomments.CommentText;
            Rating = usercomments.Rating;
            Seeded = true;
        }

        #endregion

        #region randomly seed this instance
        public bool Seeded { get; set; } = false;

        public virtual csComment Seed(csSeedGenerator sgen)
        {
            //QuoteId = Guid.NewGuid();

            //var _quote = sgen.Quote;
            //Quote = _quote.Quote;
            //Author = _quote.Author;
            CommentId = Guid.NewGuid();

            var _comment = sgen.comment;
            CommentText = _comment.ToString();
            
            return this;
        }
        #endregion
    }
}

