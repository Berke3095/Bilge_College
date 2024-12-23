﻿using BilgeCollege.MODELS.Abstracts;
using Microsoft.AspNetCore.Identity;

namespace BilgeCollege.MODELS.Concretes.CustomUser
{
    public class User : IdentityUser, I_IdentityBase
    {
        public User()
        {
            CreatedDate = DateTime.Now;
        }

        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public List<Message>? SentMessages { get; set; }
        public List<Message>? ReceivedMessages { get; set; }
    }
}
