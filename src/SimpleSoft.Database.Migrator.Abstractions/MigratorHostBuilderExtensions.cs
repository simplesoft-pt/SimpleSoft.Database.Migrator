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
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace SimpleSoft.Database.Migrator
{
    /// <summary>
    /// Extension methods for <see cref="IMigratorHostBuilder"/>
    /// </summary>
    public static class MigratorHostBuilderExtensions
    {
        /// <summary>
        /// Adds the handler to the <see cref="IMigratorHostBuilder.LoggingConfigurationHandlers"/> collection.
        /// </summary>
        /// <typeparam name="TBuilder">The builder type</typeparam>
        /// <param name="builder">The builder instance</param>
        /// <param name="handler">The handler to add</param>
        /// <returns>The builder instance</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static TBuilder ConfigureLogging<TBuilder>(this TBuilder builder, Action<ILoggerFactory> handler)
            where TBuilder : IMigratorHostBuilder
        {
            if (builder == null) throw new ArgumentNullException(nameof(builder));

            builder.AddLoggingConfigurator(handler);
            return builder;
        }

        #region ConfigureServices

        /// <summary>
        /// Adds the handler to the <see cref="IMigratorHostBuilder.ServiceConfigurationHandlers"/> collection.
        /// </summary>
        /// <typeparam name="TBuilder">The builder type</typeparam>
        /// <param name="builder">The builder instance</param>
        /// <param name="handler">The handler to add</param>
        /// <returns>The builder instance</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static TBuilder ConfigureServices<TBuilder>(this TBuilder builder, Action<IServiceCollection, ILoggerFactory> handler)
            where TBuilder : IMigratorHostBuilder
        {
            if (builder == null) throw new ArgumentNullException(nameof(builder));

            builder.AddServiceConfigurator(handler);
            return builder;
        }

        /// <summary>
        /// Adds the handler to the <see cref="IMigratorHostBuilder.ServiceConfigurationHandlers"/> collection.
        /// </summary>
        /// <typeparam name="TBuilder">The builder type</typeparam>
        /// <param name="builder">The builder instance</param>
        /// <param name="handler">The handler to add</param>
        /// <returns>The builder instance</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static TBuilder ConfigureServices<TBuilder>(this TBuilder builder, Action<IServiceCollection> handler)
            where TBuilder : IMigratorHostBuilder
        {
            if (builder == null) throw new ArgumentNullException(nameof(builder));
            if (handler == null) throw new ArgumentNullException(nameof(handler));

            builder.AddServiceConfigurator((services, factory) => handler(services));
            return builder;
        }

        #endregion

        #region Configure

        /// <summary>
        /// Adds the handler to the <see cref="IMigratorHostBuilder.ConfigurationHandlers"/> collection.
        /// </summary>
        /// <typeparam name="TBuilder">The builder type</typeparam>
        /// <param name="builder">The builder instance</param>
        /// <param name="handler">The handler to add</param>
        /// <returns>The builder instance</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static TBuilder Configure<TBuilder>(this TBuilder builder, Action<IServiceProvider, ILoggerFactory> handler)
            where TBuilder : IMigratorHostBuilder
        {
            if (builder == null) throw new ArgumentNullException(nameof(builder));

            builder.AddConfigurator(handler);
            return builder;
        }

        /// <summary>
        /// Adds the handler to the <see cref="IMigratorHostBuilder.ConfigurationHandlers"/> collection.
        /// </summary>
        /// <typeparam name="TBuilder">The builder type</typeparam>
        /// <param name="builder">The builder instance</param>
        /// <param name="handler">The handler to add</param>
        /// <returns>The builder instance</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static TBuilder Configure<TBuilder>(this TBuilder builder, Action<IServiceProvider> handler)
            where TBuilder : IMigratorHostBuilder
        {
            if (builder == null) throw new ArgumentNullException(nameof(builder));
            if (handler == null) throw new ArgumentNullException(nameof(handler));

            builder.AddConfigurator((provider, factory) => handler(provider));
            return builder;
        }

        #endregion

        #region UseServiceProvider

        /// <summary>
        /// Uses the given handler to build the <see cref="IServiceProvider"/> that
        /// will be used by the <see cref="IMigratorHost"/> to build.
        /// </summary>
        /// <typeparam name="TBuilder">The builder type</typeparam>
        /// <param name="builder">The builder instance</param>
        /// <param name="buildServiceProvider">The builder function</param>
        /// <returns>The builder instance</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static TBuilder UseServiceProvider<TBuilder>(this TBuilder builder, Func<IServiceCollection, ILoggerFactory, IServiceProvider> buildServiceProvider)
            where TBuilder : IMigratorHostBuilder
        {
            if (builder == null) throw new ArgumentNullException(nameof(builder));

            builder.SetServiceProviderBuilder(buildServiceProvider);
            return builder;
        }

        /// <summary>
        /// Uses the given handler to build the <see cref="IServiceProvider"/> that
        /// will be used by the <see cref="IMigratorHost"/> to build.
        /// </summary>
        /// <typeparam name="TBuilder">The builder type</typeparam>
        /// <param name="builder">The builder instance</param>
        /// <param name="buildServiceProvider">The builder function</param>
        /// <returns>The builder instance</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static TBuilder UseServiceProvider<TBuilder>(this TBuilder builder, Func<IServiceCollection, IServiceProvider> buildServiceProvider)
            where TBuilder : IMigratorHostBuilder
        {
            if (builder == null) throw new ArgumentNullException(nameof(builder));
            if (buildServiceProvider == null) throw new ArgumentNullException(nameof(buildServiceProvider));

            builder.SetServiceProviderBuilder((provider, factory) => buildServiceProvider(provider));
            return builder;
        }

        /// <summary>
        /// Uses the given handler to build the <see cref="IServiceProvider"/> that
        /// will be used by the <see cref="IMigratorHost"/> to build.
        /// </summary>
        /// <typeparam name="TBuilder">The builder type</typeparam>
        /// <param name="builder">The builder instance</param>
        /// <param name="buildServiceProvider">The builder function</param>
        /// <returns>The builder instance</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static TBuilder UseServiceProvider<TBuilder>(this TBuilder builder, Func<IServiceProvider> buildServiceProvider)
            where TBuilder : IMigratorHostBuilder
        {
            if (builder == null) throw new ArgumentNullException(nameof(builder));
            if (buildServiceProvider == null) throw new ArgumentNullException(nameof(buildServiceProvider));

            builder.SetServiceProviderBuilder((provider, factory) => buildServiceProvider());
            return builder;
        }

        #endregion

        /// <summary>
        /// Assigns the given <see cref="ILoggerFactory"/> to be used
        /// by the <see cref="IMigratorHost"/>.
        /// </summary>
        /// <typeparam name="TBuilder">The builder type</typeparam>
        /// <param name="builder">The builder instance</param>
        /// <param name="loggerFactory">The logger factory to use</param>
        /// <returns>The builder instance</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static TBuilder UseLoggerFactory<TBuilder>(this TBuilder builder, ILoggerFactory loggerFactory)
            where TBuilder : IMigratorHostBuilder
        {
            if (builder == null) throw new ArgumentNullException(nameof(builder));

            builder.SetLoggerFactory(loggerFactory);
            return builder;
        }

        #region UseSetting

        /// <summary>
        /// Assigns the collection of settings to the <see cref="IMigratorHostBuilder"/> instance.
        /// </summary>
        /// <typeparam name="TBuilder">The builder type</typeparam>
        /// <param name="builder">The builder instance</param>
        /// <param name="key">The setting key</param>
        /// <param name="value">The setting value</param>
        /// <returns>The builder instance</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static TBuilder UseSetting<TBuilder>(this TBuilder builder, string key, string value)
            where TBuilder : IMigratorHostBuilder
        {
            if (builder == null) throw new ArgumentNullException(nameof(builder));

            builder.SetSetting(key, value);
            return builder;
        }

        /// <summary>
        /// Assigns the collection of settings to the <see cref="IMigratorHostBuilder"/> instance.
        /// </summary>
        /// <typeparam name="TBuilder">The builder type</typeparam>
        /// <param name="builder">The builder instance</param>
        /// <param name="settings">The settings collection to use</param>
        /// <returns>The builder instance</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static TBuilder UseSetting<TBuilder>(this TBuilder builder, IEnumerable<KeyValuePair<string, string>> settings)
            where TBuilder : IMigratorHostBuilder
        {
            if (builder == null) throw new ArgumentNullException(nameof(builder));
            if (settings == null) throw new ArgumentNullException(nameof(settings));

            foreach (var setting in settings)
                builder.SetSetting(setting.Key, setting.Value);

            return builder;
        }

        #endregion
    }
}