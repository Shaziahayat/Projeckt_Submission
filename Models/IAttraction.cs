using System;
namespace Models
{
    public enum enCategory { Museum, Natur, Historia, Arkitektur,Kultur };
    public interface IAttraction
    {
        public Guid AttractionId { get; set; }
        public enCategory Category { get; set; }
        public string CategoryName { get; set; }

        public IPerson Person { get; set; }
    }
}

