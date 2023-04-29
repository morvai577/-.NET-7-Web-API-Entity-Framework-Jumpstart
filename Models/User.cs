namespace dotnet_rpg.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public byte[] PasswordHash { get; set; } = new byte[0]; // 1. The PasswordHash property is a byte array. The byte array is used to store the password hash. The password hash is a one-way hash of the password and is used to verify the password. The hash is designed in such a way that it is computationally infeasible to reverse the process and obtain the original password from the hash. When the user attempts to log in, their entered password is hashed and compared to the stored hash. If the hashes match, the user is authenticated.
        public byte[] PasswordSalt { get; set; } = new byte[0]; // 2. The PasswordSalt property is a byte array. The byte array is used to store the password salt. The password salt is a random value that is added to the password before hashing. This makes it more difficult for attackers to use precomputed rainbow tables to crack the password hashes. Without the salt, an attacker could potentially precompute a table of hash values for commonly used passwords and compare them to the stored hashes to quickly crack many passwords.
    }
}