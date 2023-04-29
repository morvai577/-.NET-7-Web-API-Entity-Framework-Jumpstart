namespace dotnet_rpg.Models
{
    public class Character
    {
        public int Id { get; set; }
        public string Name { get; set; } = "Frodo"; // Default value
        public int HitPoints { get; set; } = 100;
        public int Strength { get; set; } = 10;
        public int Defense { get; set; } = 10;
        public int Intelligence { get; set; } = 10;
        public RpgClass Class { get; set; } = RpgClass.Knight;
        public User? User { get; set; } // 1. The User property is a **navigation property**. Navigation properties are used in entity framework to define relationships between entities. In this example, the User property is used to define a one-to-many relationship between the Character and User entities. A Character can have one User, but a User can have many Characters. The ? after the User type indicates that the User property is nullable. This means that a Character can exist without a User. The ? is shorthand for the following code: User User { get; set; } = null;
    }
}