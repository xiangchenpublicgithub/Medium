﻿using Dapper;
using MediatR;

namespace Medium.DomainModel
{
    public class DeletePostCommandHandler : IRequestHandler<DeletePostCommand, bool>
    {
        public bool Handle(DeletePostCommand command)
        {
            using (var connection = SqlConnectionFactory.Create())
            {
                var param = new { Slug = command.PostSlug };

                connection
                    .Execute("DELETE FROM [Junction] WHERE [PostSlug] = @Slug", param);

                var deletedPosts = connection
                    .Execute("DELETE FROM [Posts] WHERE [Slug] = @Slug", param);

                return deletedPosts == 1;
            }
        }
    }
}