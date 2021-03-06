﻿using Synchronized.UI.Utilities.Interfaces;
using System;
using System.Collections.Generic;
using Synchronized.ServiceModel;
using Synchronized.ViewModel.UsersViewModels;
using Synchronized.ViewModelFactories.Interfaces;
using Synchronized.Core.Factories.Interfaces;
using Microsoft.Extensions.Logging;

namespace Synchronized.UI.Utilities
{
    /// <summary>
    /// This is a concrete Converter for converting between ServiceModel Users and ViewModel types.
    /// </summary>
    public class UsersConverter : IUsersConverter
    {

        private IViewModelFactory _viewModelFactory;
        private IServiceModelFactory _serviceModelFactory;
        private IPostsConverter _postsConverter;
        ILogger<UsersConverter> _logger;

        public UsersConverter(IViewModelFactory viewModelFactory, IServiceModelFactory serviceModelFactory, IPostsConverter postsConverter, ILogger<UsersConverter> logger)
        {
            _viewModelFactory = viewModelFactory;
            _serviceModelFactory = serviceModelFactory;
            _postsConverter = postsConverter;
            _logger = logger;
        }

        public UserViewModel Convert(User from)
        {
            _logger.LogInformation("Entering Conver.");
            var user = _viewModelFactory.GetUser();
            user.Address = String.Copy(from.Address);
            user.Id = String.Copy(from.Id);
            user.ImageUri = String.Copy(from.ImageUri);
            user.Name = String.Copy(from.Name);
            user.Email = String.Copy(from.Email);
            user.JoiningDate = from.JoiningDate;
            user.Points = from.Points;
            if (from.Roles != null)
            {
                user.Roles = "";
                foreach (string roleName in from.Roles)
                {
                    user.Roles += roleName;
                    if (from.Roles.IndexOf(roleName) != from.Roles.Count - 1)
                    {
                        user.Roles += ",";
                    }
                }
            }
            if (from.Questions != null)
            {
                foreach (Question q in from.Questions)
                {
                    user.ActivePosts.Add(((IHomeViewConverter)_postsConverter).Convert(q));
                }
            }
            _logger.LogInformation("Leaving Convert.");
            return user;
        }

        public User Convert(UserViewModel from)
        {
            throw new NotImplementedException();
        }

        public List<UserViewModel> Convert(ICollection<User> from)
        {
            throw new NotImplementedException();
        }

        public List<User> Convert(ICollection<UserViewModel> from)
        {
            throw new NotImplementedException();
        }
    }
}
