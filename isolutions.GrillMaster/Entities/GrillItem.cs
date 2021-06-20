using System;

namespace isolutions.GrillMaster.Entities
{
    public class GrillItem
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Length { get; set; }
        public int Width { get; set; }
        public string Duration { get; set; }
        public int Quantity { get; set; }

        public override string ToString()
        {
            return "Id: " + Id + " Name: " + Name + " Length: " + Length + " Width: " + Width + " Duration: " +
                   Duration + " Quantity: " + Quantity;
        }
    }
}