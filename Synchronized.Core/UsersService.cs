﻿using System.Threading.Tasks;
using Synchronized.Core.Factories.Interfaces;
using Synchronized.Core.Interfaces;
using Synchronized.Core.Utilities.Interfaces;
using Synchronized.Repository.Interfaces;
using Synchronized.ServiceModel;
using Synchronized.SharedLib.Utilities;
using Microsoft.Extensions.Logging;
using Synchronized.Domain;
using Microsoft.AspNetCore.Identity;
using Synchronized.SharedLib;
using System;
using System.Threading;

namespace Synchronized.Core
{
    public class UsersService : DataService<ApplicationUser, User>, IUsersService
    {
        public UsersService(IUsersRepository repo, IServiceModelFactory factory, IDataConverter converter, ILogger<UsersService> logger) : base(repo, factory, converter, logger)
        {
        }

        public async override Task<User> GetById(string id)
        {
            var user = await _repo.GetById(id);
            var coreUser = _converter.Convert(user);
            return coreUser;
        }

        public PaginatedList<User> GetUsersPage(int pageIndex, int pageSize, string sortOrder, string searchTerm)
        {
            _logger.LogInformation("Entering GetUsersPage.");
            var users = ((IUsersRepository)_repo).GetPage(pageIndex, pageSize, sortOrder, searchTerm);
            var coreUsers = _converter.Convert(users);
            var usersPage = _factory.GetUsersPage(coreUsers, _repo.GetCount(), pageSize, pageIndex);
            usersPage.ForEach(u => {
                _logger.LogDebug("User --->\n\t\tAddress: {0}\n" +
                    "\t\tEmail: {1}\n" +
                    "\t\tName: {2}\n" +
                    "\t\tPoints: {3}", u.Address, u.Email, u.Name, u.Points);
            });
            _logger.LogInformation("Leaving GetUsersPage.");
            return usersPage;
        }

        public async Task UpdateUserRoles(String userId)
        {
            var user = await _repo.GetById(userId);
            await UpdateUserRoles(user);
        }

        public async Task UpdateUserRoles(ApplicationUser user)
        {
            if (Constants.MODERATOR_MINIMUM_RANK <= user.Points)
            {
                var userIsInRole = await ((IUserRoleStore<ApplicationUser>)_repo).IsInRoleAsync(user, Constants.MODERATOR, new CancellationToken());
                if (!userIsInRole)
                {
                    await((IUserRoleStore<ApplicationUser>)_repo).AddToRoleAsync(user, Constants.MODERATOR, new CancellationToken());
                }
            }
            else if (Constants.EDITOR_MINIMUM_RANK <= user.Points)
            {
                var userIsInRole = await ((IUserRoleStore<ApplicationUser>)_repo).IsInRoleAsync(user, Constants.MODERATOR, new CancellationToken());
                if (userIsInRole)
                {
                    await((IUserRoleStore<ApplicationUser>)_repo).RemoveFromRoleAsync(user, Constants.MODERATOR, new CancellationToken());
                }
                userIsInRole = await((IUserRoleStore<ApplicationUser>)_repo).IsInRoleAsync(user, Constants.EDITOR, new CancellationToken());
                if (!userIsInRole)
                {
                    await((IUserRoleStore<ApplicationUser>)_repo).AddToRoleAsync(user, Constants.EDITOR, new CancellationToken());
                }
            }
            else if (Constants.VOTER_MINIMUM_RANK <= user.Points)
            {
                var userIsInRole = await ((IUserRoleStore<ApplicationUser>)_repo).IsInRoleAsync(user, Constants.MODERATOR, new CancellationToken());
                if (userIsInRole)
                {
                    await((IUserRoleStore<ApplicationUser>)_repo).RemoveFromRoleAsync(user, Constants.MODERATOR, new CancellationToken());
                }
                userIsInRole = await((IUserRoleStore<ApplicationUser>)_repo).IsInRoleAsync(user, Constants.EDITOR, new CancellationToken());
                if (userIsInRole)
                {
                    await((IUserRoleStore<ApplicationUser>)_repo).RemoveFromRoleAsync(user, Constants.EDITOR, new CancellationToken());
                }
                userIsInRole = await((IUserRoleStore<ApplicationUser>)_repo).IsInRoleAsync(user, Constants.VOTER, new CancellationToken());
                if (!userIsInRole)
                {
                    await((IUserRoleStore<ApplicationUser>)_repo).AddToRoleAsync(user, Constants.VOTER, new CancellationToken());
                }
            }
            else
            {
                var userIsInRole = await((IUserRoleStore<ApplicationUser>)_repo).IsInRoleAsync(user, Constants.MODERATOR, new CancellationToken());
                if (userIsInRole)
                {
                    await((IUserRoleStore<ApplicationUser>)_repo).RemoveFromRoleAsync(user, Constants.MODERATOR, new CancellationToken());
                }
                userIsInRole = await((IUserRoleStore<ApplicationUser>)_repo).IsInRoleAsync(user, Constants.EDITOR, new CancellationToken());
                if (userIsInRole)
                {
                    await((IUserRoleStore<ApplicationUser>)_repo).RemoveFromRoleAsync(user, Constants.EDITOR, new CancellationToken());
                }
                userIsInRole = await((IUserRoleStore<ApplicationUser>)_repo).IsInRoleAsync(user, Constants.VOTER, new CancellationToken());
                if (userIsInRole)
                {
                    await((IUserRoleStore<ApplicationUser>)_repo).RemoveFromRoleAsync(user, Constants.VOTER, new CancellationToken());
                }
            }
        }
    }
}
