namespace InternshipTest.Domain.UserAggregate
{
    public class UserState
    {
        public int Id { get; set; }
        public string Code { get; private set; } = null!;
        public string Description { get; set; } = null!;

        public UserState(int id, string code, string description) 
        {
            Id= id;
            UpdateCode(code);
            Description = description;
        }

        private void UpdateCode(string code)
        {
            if (code != "Active" && code != "Blocked")
                throw new ArgumentException("The code value must be either Active or Blocked." +
                    $"You passed {nameof(code)} = {code}");
            Code = code;
        }
    }
}