﻿using Synchronized.Core.Interfaces;
using Synchronized.Model;
using Synchronized.Repository.Interfaces;
using UtilsLib.HtmlUtils.HtmlParser;
using Synchronized.Core.Utilites;
using System.Threading.Tasks;
using Synchronized.SharedLib.Utilities;
using System.Collections.Generic;

namespace Synchronized.Core
{
    public class QuestionsService : DataService<Question>, IQuestionsService, IViewQuestions
    {
        private readonly HtmlParser parser;

        public QuestionsService(IQuestionsRepository repo, HtmlParser parser) : base(repo)
        {
            this.parser = parser;
        }

        public async Task<PaginatedList<Question>> GetQuestionsPageAsync(int pageIndex, int pageSize)
        {
            var questions = await ((IQuestionsRepository)repo).GetQuestionsPageAsync(pageIndex, pageSize);
            return await CreatePage(questions, pageIndex, pageSize);
        }

        public async Task<PaginatedList<Question>> GetQuestionsPageWithUsersAsync(int pageIndex, int pageSize)
        {
            var questions = ((IQuestionsRepository)repo).GetQuestionsPageWithUsersAsync(pageIndex, pageSize);
            return await CreatePage(questions, pageIndex, pageSize);
        }

        public Question FindQuestionById(string questionId)
        {
            return ((IQuestionsRepository)repo).FindQuestionById(questionId);
        }

        public async Task<PaginatedList<Question>> GetQuestionsPageWithUsersAsync(int pageIndex, int pageSize, string sortOrder, string filter)
        {
            var questions = ((IQuestionsRepository)repo).GetQuestionsPageWithUsersAsync(pageIndex, pageSize, sortOrder, filter);
            return await CreatePage(questions, pageIndex, pageSize);
        }

        private async Task<PaginatedList<Question>> CreatePage(List<Question> questions, int pageIndex, int pageSize)
        {
            int count = await repo.GetCount();
            Utils.MinimizeContent(parser, questions);
            return new PaginatedList<Question>(questions, count, pageIndex, pageSize);
        }

        public Answer FindAnswerById(string answerId)
        {
            return ((IQuestionsRepository)repo).FindAnswerById(answerId);
        }

        public void UpdateQuestion(Question question)
        {
            ((IQuestionsRepository)repo).UpdateQuestion(question);
            //repo.Update();
        }

        public void UpdateAnswer(Answer answer)
        {
            // TODO: Use something like: ((IQuestionsRepository)repo).UpdateAnswer(answer);
            ((IQuestionsRepository)repo).UpdateAnswer(answer);
            //repo.Update();
        }

        public Task ViewQuestion(string UserId, string questionId)
        {
            throw new System.NotImplementedException();
        }
    }
}
