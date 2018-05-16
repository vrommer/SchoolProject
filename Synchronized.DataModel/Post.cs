﻿using Synchronized.Domain;
using System;
using System.Collections.Generic;

namespace Synchronized.Model
{
    public abstract class Post : IEntity
    {
        public string Id { get; set; }
        public DateTime DatePosted { get; set; }
        public string PublisherId { get; set; }
        public string Body { get; set; }

        public ICollection<PostFlag> PostFlags { get; set; }
        public ICollection<DeleteVote> DeleteVotes { get; set; }
        //public ICollection<Vote> Votes { get; set; }

        public ApplicationUser Publisher { get; set; }
    }
}