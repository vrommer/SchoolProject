﻿using Synchronized.Domain.Factories.Interfaces;
using System;
using System.Collections.Generic;

namespace Synchronized.Domain.Factories
{
    public class DomainModelFactory : IDomainModelFactory
    {
        public T GetInstance<T>()
        {
            object obj = Activator.CreateInstance(typeof(T));
            return ((T)obj);
        }

        public Answer GetAnswer()
        {
            return new Answer();
        }

        public Comment GetComment()
        {
            return new Comment();
        }

        public Question GetQuestion()
        {
            var question = new Question
            {
                QuestionTags = new List<QuestionTag>()
            };
            return question;
        }

        public List<Question> GetQuestionsList()
        {
            return new List<Question>();
        }

        public QuestionTag GetQuestionTag()
        {
            return new QuestionTag();
        }
    }
}
