using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

using Configuration;
using Models;
using Models.DTO;

namespace DbModels
{
    public class csAttractionDbM : csAttraction, ISeed<csAttractionDbM>
    {
        [Key]       // for EFC Code first
        public override Guid AttractionId { get; set; }

        [NotMapped] //correcting the first migration error 
        public override IPerson Person { get => csPersonDbM; set => new NotImplementedException(); }

        [JsonIgnore]// this is implemented in database model
        public virtual csPersonDbM csPersonDbM { get; set; } = null;

        

        #region randomly seed this instance
        public override csAttractionDbM Seed(csSeedGenerator sgen)
        {
            base.Seed(sgen);
            return this;
        }
        #endregion

        #region Update from DTO
        public csAttractionDbM UpdateFromDTO(csAttractionCUdto org)
        {
            if (org == null) return null;
            
            Category = org.Category;
            CategoryName = org.CategoryName;
            

            //We will set this when DbM model is finished
            //FriendId = org.FriendId;

            return this;
        }
        #endregion

        #region constructors
        public csAttractionDbM() { }
        public csAttractionDbM(csAttractionCUdto org)
        {
            AttractionId = Guid.NewGuid();
            UpdateFromDTO(org);
        }
        #endregion
    }
}

