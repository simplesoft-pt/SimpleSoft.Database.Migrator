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

namespace SimpleSoft.Database.Migrator
{
    /// <summary>
    /// Options for a SQL Server migration context.
    /// </summary>
    public class SqlServerMigrationOptions : RelationalMigrationOptions, ISqlServerMigrationOptions
    {
        /// <summary>
        /// Creates a new instance.
        /// </summary>
        /// <param name="contextName">The context name</param>
        /// <param name="connectionString">The database connection string</param>
        /// <param name="tableName">The table name</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        public SqlServerMigrationOptions(string contextName, string connectionString, string tableName = "__DbMigratorHistory") 
            : base(contextName, connectionString, tableName)
        {

        }
    }

    /// <summary>
    /// Options for a SQL Server migration context.
    /// </summary>
    /// <typeparam name="TContext">The context type</typeparam>
    public class SqlServerMigrationOptions<TContext> : SqlServerMigrationOptions, ISqlServerMigrationOptions<TContext>
        where TContext : ISqlServerMigrationContext
    {
        /// <summary>
        /// Creates a new instance.
        /// </summary>
        /// <param name="connectionString">The database connection string</param>
        /// <param name="tableName">The table name</param>
        /// <exception cref="ArgumentNullException"></exception>
        public SqlServerMigrationOptions(string connectionString, string tableName = "__DbMigratorHistory")
            : base(typeof(TContext).Name, connectionString, tableName)
        {

        }

        /// <inheritdoc />
        public void AddMigration<TMigration>() where TMigration : IMigration<TContext>
        {
            base.AddMigration(typeof(TMigration));
        }
    }
}
