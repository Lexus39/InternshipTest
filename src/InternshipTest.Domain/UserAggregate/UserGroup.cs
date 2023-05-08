namespace InternshipTest.Domain.UserAggregate
{
    public class UserGroup
    {
        public int Id { get; }
        public string Code { get; private set; } = null!;
        public string Description { get; set; } = null!;

        public UserGroup(int id, string code, string description)
        {
            Id = id;
            UpdateCode(code);
            Description = description;
        }

        private void UpdateCode(string code)
        {
            if (code != "Admin" && code != "User")
                throw new ArgumentException("The code value must be either Admin or User. " + 
                    $"You passed {nameof(code)} = {code}");
            Code = code;
        }
    }
}