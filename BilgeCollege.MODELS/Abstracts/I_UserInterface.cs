﻿namespace BilgeCollege.MODELS.Abstracts
{
    public interface I_UserInterface
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string TCK { get; set; }
        public string PhoneNumber { get; set; }
        public string UserId { get; set; }
    }
}
