﻿using System.Collections.Generic;
using Coolector.Common.Types;
using Coolector.Core.Commands;
using Nancy;
using Nancy.ModelBinding;

namespace Coolector.Api.Modules.Base
{
    public class ModuleBase : NancyModule
    {
        protected readonly ICommandDispatcher CommandDispatcher;

        public ModuleBase(ICommandDispatcher commandDispatcher, string modulePath = "")
            :base(modulePath)
        {
            CommandDispatcher = commandDispatcher;
        }

        protected T BindRequest<T>() => this.Bind<T>();

        //TODO: Add headers etc.
        protected IEnumerable<T> FromPagedResult<T>(Maybe<PagedResult<T>> result)
        {
            return result.HasValue ? result.Value.Items : new List<T>();
        }
    }
}