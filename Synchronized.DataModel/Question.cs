﻿using Synchronized.Domain;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Synchronized.Domain
{
    public class Question : VotedPost
    {

        public string Title { get; set; }
        public int Points { get; set; }
        public int Deleted { get; set; }

        public List<Answer> Answers { get; set; }
        public List<QuestionTag> QuestionTags { get; set; }
        public List<QuestionView> QuestionViews { get; set; }


        public bool Answered() {
            if (Answers == null)
            {
                return false;
            }

            bool answered = false;
            Answers.ToList().ForEach(a => {
                if (a.IsAccepted)
                    answered = true;                    
            });
            return answered;
        }
    }
}