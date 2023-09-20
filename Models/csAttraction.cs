
using System;
using System.Diagnostics.Metrics;
using System.Reflection.Emit;
using Microsoft.VisualBasic;

namespace Models
{
    public class csAttraction : IAttraction, ISeed<csAttraction>
    {
        public virtual Guid AttractionId { get; set; }

        public virtual enCategory Category { get; set; }

        public string CategoryName { get; set; }


        public virtual IPerson Person { get; set; }


        public override string ToString() => $" {CategoryName} is {Category} ";

        #region constructors
        public csAttraction() { }
        public csAttraction(csAttraction org)
        {
            this.Seeded = org.Seeded;

            this.AttractionId = org.AttractionId;
            this.Category = org.Category;
            this.CategoryName = org.CategoryName;
        }
        #endregion

        #region randomly seed this instance
        public bool Seeded { get; set; } = false;

        public virtual csAttraction Seed(csSeedGenerator sgen)
        {
            Seeded = true;
            this.AttractionId = Guid.NewGuid();
            this.CategoryName = sgen.CategoryName;
            Category = sgen.FromEnum<enCategory>();
            return this;
        }
        
            //=> throw new NotImplementedException();
        #endregion
    }
}

