﻿using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Darker;
using Microsoft.EntityFrameworkCore;
using ToDoCore.Adaptors.Db;
using ToDoCore.Ports.Queries;

namespace ToDoCore.Ports.Handlers
{
    public class ToDoByIdQueryHandlerAsync : AsyncQueryHandler<ToDoByIdQuery, ToDoByIdQuery.Result>
    {
        private readonly DbContextOptions<ToDoContext> _options;

        public ToDoByIdQueryHandlerAsync(DbContextOptions<ToDoContext> options)
        {
            _options = options;
        }

        public override async Task<ToDoByIdQuery.Result> ExecuteAsync(ToDoByIdQuery request, CancellationToken cancellationToken = default(CancellationToken))
        {
           using (var uow = new ToDoContext(_options))
            {
                var toDoItem = await uow.ToDoItems.SingleAsync(t => t.Id == request.Id, cancellationToken: cancellationToken);
                return new ToDoByIdQuery.Result(toDoItem);
            }
        }
    }
}