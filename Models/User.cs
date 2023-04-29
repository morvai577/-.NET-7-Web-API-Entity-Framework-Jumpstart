namespace dotnet_rpg.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public byte[] PasswordHash { get; set; } = new byte[0]; // 1. The PasswordHash property is a byte array. The byte array is used to store the password hash. The password hash is a one-way hash of the password and is used to verify the password. The hash is designed in such a way that it is computationally infeasible to reverse the process and obtain the original password from the hash. When the user attempts to log in, their entered password is hashed and compared to the stored hash. If the hashes match, the user is authenticated.
        public byte[] PasswordSalt { get; set; } = new byte[0]; // 2. The PasswordSalt property is a byte array. The byte array is used to store the password salt. The password salt is a random value that is added to the password before hashing. This makes it more difficult for attackers to use precomputed rainbow tables to crack the password hashes. Without the salt, an attacker could potentially precompute a table of hash values for commonly used passwords and compare them to the stored hashes to quickly crack many passwords.
        public List<Character>? Characters { get; set; } // 3. The Characters property is a **navigation property**. Navigation properties are used in entity framework to define relationships between entities. In this example, the Characters property is used to define a one-to-many relationship between the User and Character entities. A User can have many Characters, but a Character can only have one User. The ? after the List<Character> type indicates that the Characters property is nullable. This means that a User can exist without any Characters. The ? is shorthand for the following code: List<Character> Characters { get; set; } = null;
    }
}