﻿#region License
// The MIT License (MIT)
// 
// Copyright (c) 2017 João Simões
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.
#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace SimpleSoft.Database.Migrator
{
    /// <summary>
    /// Represents a configured migrator host
    /// </summary>
    public class MigratorHost<TContext> : IMigratorHost<TContext> where TContext : IMigrationContext
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ILoggerFactory _loggerFactory;
        private readonly IConfiguration _configuration;
        private readonly IMigrationManager<TContext> _manager;
        private readonly List<Type> _migrationsAscendingByName;

        /// <summary>
        /// Creates a new instance
        /// </summary>
        /// <param name="serviceProvider">The service provider</param>
        /// <param name="loggerFactory">The logger factory instance</param>
        /// <param name="configuration">The configuration to use</param>
        /// <param name="manager">The migration manager</param>
        /// <param name="migrations">The migrations found</param>
        /// <exception cref="ArgumentNullException"></exception>
        public MigratorHost(
            IServiceProvider serviceProvider, ILoggerFactory loggerFactory, IConfiguration configuration, 
            IMigrationManager<TContext> manager, IEnumerable<Type> migrations)
        {
            if (serviceProvider == null)
                throw new ArgumentNullException(nameof(serviceProvider));
            if (loggerFactory == null)
                throw new ArgumentNullException(nameof(loggerFactory));
            if (configuration == null)
                throw new ArgumentNullException(nameof(configuration));
            if (manager == null)
                throw new ArgumentNullException(nameof(manager));
            if (migrations == null)
                throw new ArgumentNullException(nameof(migrations));

            ContextType = typeof(TContext);

            _serviceProvider = serviceProvider;
            _loggerFactory = loggerFactory;
            _configuration = configuration;
            _manager = manager;
            _migrationsAscendingByName = migrations.OrderBy(e => e.Name).ToList();
        }

        #region Implementation of IMigratorHost<TContext>

        /// <inheritdoc />
        public Type ContextType { get; }

        #endregion
    }
}
