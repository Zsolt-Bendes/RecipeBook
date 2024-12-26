// <auto-generated/>
#pragma warning disable
using Home.Recipes.Domain.Recipes;
using Marten.Internal;
using Marten.Internal.Storage;
using Marten.Schema;
using Marten.Schema.Arguments;
using Npgsql;
using System;
using System.Collections.Generic;
using Weasel.Core;
using Weasel.Postgresql;

namespace Marten.Generated.DocumentStorage
{
    // START: UpsertRecipeOperation978002805
    public class UpsertRecipeOperation978002805 : Marten.Internal.Operations.StorageOperation<Home.Recipes.Domain.Recipes.Recipe, System.Guid>
    {
        private readonly Home.Recipes.Domain.Recipes.Recipe _document;
        private readonly System.Guid _id;
        private readonly System.Collections.Generic.Dictionary<System.Guid, System.Guid> _versions;
        private readonly Marten.Schema.DocumentMapping _mapping;

        public UpsertRecipeOperation978002805(Home.Recipes.Domain.Recipes.Recipe document, System.Guid id, System.Collections.Generic.Dictionary<System.Guid, System.Guid> versions, Marten.Schema.DocumentMapping mapping) : base(document, id, versions, mapping)
        {
            _document = document;
            _id = id;
            _versions = versions;
            _mapping = mapping;
        }



        public override void Postprocess(System.Data.Common.DbDataReader reader, System.Collections.Generic.IList<System.Exception> exceptions)
        {
            if (postprocessRevision(reader, exceptions))
            {
            }

        }


        public override async System.Threading.Tasks.Task PostprocessAsync(System.Data.Common.DbDataReader reader, System.Collections.Generic.IList<System.Exception> exceptions, System.Threading.CancellationToken token)
        {
            if (await postprocessRevisionAsync(reader, exceptions, token))
            {
            }

        }


        public override Marten.Internal.Operations.OperationRole Role()
        {
            return Marten.Internal.Operations.OperationRole.Upsert;
        }


        public override NpgsqlTypes.NpgsqlDbType DbType()
        {
            return NpgsqlTypes.NpgsqlDbType.Uuid;
        }


        public override void ConfigureParameters(Weasel.Postgresql.IGroupedParameterBuilder parameterBuilder, Weasel.Postgresql.ICommandBuilder builder, Home.Recipes.Domain.Recipes.Recipe document, Marten.Internal.IMartenSession session)
        {
            builder.Append("select public.mt_upsert_recipe(");
            var parameter0 = parameterBuilder.AppendParameter(session.Serializer.ToJson(_document));
            parameter0.NpgsqlDbType = NpgsqlTypes.NpgsqlDbType.Jsonb;
            // .Net Class Type
            var parameter1 = parameterBuilder.AppendParameter(_document.GetType().FullName);
            parameter1.NpgsqlDbType = NpgsqlTypes.NpgsqlDbType.Varchar;
            var parameter2 = parameterBuilder.AppendParameter(document.Id);
            setCurrentRevisionParameter(parameterBuilder);
            builder.Append(')');
        }

    }

    // END: UpsertRecipeOperation978002805
    
    
    // START: InsertRecipeOperation978002805
    public class InsertRecipeOperation978002805 : Marten.Internal.Operations.StorageOperation<Home.Recipes.Domain.Recipes.Recipe, System.Guid>
    {
        private readonly Home.Recipes.Domain.Recipes.Recipe _document;
        private readonly System.Guid _id;
        private readonly System.Collections.Generic.Dictionary<System.Guid, System.Guid> _versions;
        private readonly Marten.Schema.DocumentMapping _mapping;

        public InsertRecipeOperation978002805(Home.Recipes.Domain.Recipes.Recipe document, System.Guid id, System.Collections.Generic.Dictionary<System.Guid, System.Guid> versions, Marten.Schema.DocumentMapping mapping) : base(document, id, versions, mapping)
        {
            _document = document;
            _id = id;
            _versions = versions;
            _mapping = mapping;
        }



        public override System.Threading.Tasks.Task PostprocessAsync(System.Data.Common.DbDataReader reader, System.Collections.Generic.IList<System.Exception> exceptions, System.Threading.CancellationToken token)
        {
            return System.Threading.Tasks.Task.CompletedTask;
            return System.Threading.Tasks.Task.CompletedTask;
        }


        public override Marten.Internal.Operations.OperationRole Role()
        {
            return Marten.Internal.Operations.OperationRole.Insert;
        }


        public override NpgsqlTypes.NpgsqlDbType DbType()
        {
            return NpgsqlTypes.NpgsqlDbType.Uuid;
        }


        public override void ConfigureParameters(Weasel.Postgresql.IGroupedParameterBuilder parameterBuilder, Weasel.Postgresql.ICommandBuilder builder, Home.Recipes.Domain.Recipes.Recipe document, Marten.Internal.IMartenSession session)
        {
            builder.Append("select public.mt_insert_recipe(");
            var parameter0 = parameterBuilder.AppendParameter(session.Serializer.ToJson(_document));
            parameter0.NpgsqlDbType = NpgsqlTypes.NpgsqlDbType.Jsonb;
            // .Net Class Type
            var parameter1 = parameterBuilder.AppendParameter(_document.GetType().FullName);
            parameter1.NpgsqlDbType = NpgsqlTypes.NpgsqlDbType.Varchar;
            var parameter2 = parameterBuilder.AppendParameter(document.Id);
            setCurrentRevisionParameter(parameterBuilder);
            builder.Append(')');
        }

    }

    // END: InsertRecipeOperation978002805
    
    
    // START: UpdateRecipeOperation978002805
    public class UpdateRecipeOperation978002805 : Marten.Internal.Operations.StorageOperation<Home.Recipes.Domain.Recipes.Recipe, System.Guid>
    {
        private readonly Home.Recipes.Domain.Recipes.Recipe _document;
        private readonly System.Guid _id;
        private readonly System.Collections.Generic.Dictionary<System.Guid, System.Guid> _versions;
        private readonly Marten.Schema.DocumentMapping _mapping;

        public UpdateRecipeOperation978002805(Home.Recipes.Domain.Recipes.Recipe document, System.Guid id, System.Collections.Generic.Dictionary<System.Guid, System.Guid> versions, Marten.Schema.DocumentMapping mapping) : base(document, id, versions, mapping)
        {
            _document = document;
            _id = id;
            _versions = versions;
            _mapping = mapping;
        }



        public override void Postprocess(System.Data.Common.DbDataReader reader, System.Collections.Generic.IList<System.Exception> exceptions)
        {
            if (postprocessRevision(reader, exceptions))
            {
            }

        }


        public override async System.Threading.Tasks.Task PostprocessAsync(System.Data.Common.DbDataReader reader, System.Collections.Generic.IList<System.Exception> exceptions, System.Threading.CancellationToken token)
        {
            if (await postprocessRevisionAsync(reader, exceptions, token))
            {
            }

        }


        public override Marten.Internal.Operations.OperationRole Role()
        {
            return Marten.Internal.Operations.OperationRole.Update;
        }


        public override NpgsqlTypes.NpgsqlDbType DbType()
        {
            return NpgsqlTypes.NpgsqlDbType.Uuid;
        }


        public override void ConfigureParameters(Weasel.Postgresql.IGroupedParameterBuilder parameterBuilder, Weasel.Postgresql.ICommandBuilder builder, Home.Recipes.Domain.Recipes.Recipe document, Marten.Internal.IMartenSession session)
        {
            builder.Append("select public.mt_update_recipe(");
            var parameter0 = parameterBuilder.AppendParameter(session.Serializer.ToJson(_document));
            parameter0.NpgsqlDbType = NpgsqlTypes.NpgsqlDbType.Jsonb;
            // .Net Class Type
            var parameter1 = parameterBuilder.AppendParameter(_document.GetType().FullName);
            parameter1.NpgsqlDbType = NpgsqlTypes.NpgsqlDbType.Varchar;
            var parameter2 = parameterBuilder.AppendParameter(document.Id);
            setCurrentRevisionParameter(parameterBuilder);
            builder.Append(')');
        }

    }

    // END: UpdateRecipeOperation978002805
    
    
    // START: QueryOnlyRecipeSelector978002805
    public class QueryOnlyRecipeSelector978002805 : Marten.Internal.CodeGeneration.DocumentSelectorWithOnlySerializer, Marten.Linq.Selectors.ISelector<Home.Recipes.Domain.Recipes.Recipe>
    {
        private readonly Marten.Internal.IMartenSession _session;
        private readonly Marten.Schema.DocumentMapping _mapping;

        public QueryOnlyRecipeSelector978002805(Marten.Internal.IMartenSession session, Marten.Schema.DocumentMapping mapping) : base(session, mapping)
        {
            _session = session;
            _mapping = mapping;
        }



        public Home.Recipes.Domain.Recipes.Recipe Resolve(System.Data.Common.DbDataReader reader)
        {

            Home.Recipes.Domain.Recipes.Recipe document;
            document = _serializer.FromJson<Home.Recipes.Domain.Recipes.Recipe>(reader, 0);
            return document;
        }


        public async System.Threading.Tasks.Task<Home.Recipes.Domain.Recipes.Recipe> ResolveAsync(System.Data.Common.DbDataReader reader, System.Threading.CancellationToken token)
        {

            Home.Recipes.Domain.Recipes.Recipe document;
            document = await _serializer.FromJsonAsync<Home.Recipes.Domain.Recipes.Recipe>(reader, 0, token).ConfigureAwait(false);
            return document;
        }

    }

    // END: QueryOnlyRecipeSelector978002805
    
    
    // START: LightweightRecipeSelector978002805
    public class LightweightRecipeSelector978002805 : Marten.Internal.CodeGeneration.DocumentSelectorWithOnlySerializer, Marten.Linq.Selectors.ISelector<Home.Recipes.Domain.Recipes.Recipe>
    {
        private readonly Marten.Internal.IMartenSession _session;
        private readonly Marten.Schema.DocumentMapping _mapping;

        public LightweightRecipeSelector978002805(Marten.Internal.IMartenSession session, Marten.Schema.DocumentMapping mapping) : base(session, mapping)
        {
            _session = session;
            _mapping = mapping;
        }



        public Home.Recipes.Domain.Recipes.Recipe Resolve(System.Data.Common.DbDataReader reader)
        {
            var id = reader.GetFieldValue<System.Guid>(0);

            Home.Recipes.Domain.Recipes.Recipe document;
            document = _serializer.FromJson<Home.Recipes.Domain.Recipes.Recipe>(reader, 1);
            var version = reader.GetFieldValue<int>(2);
            _session.MarkAsDocumentLoaded(id, document);
            return document;
        }


        public async System.Threading.Tasks.Task<Home.Recipes.Domain.Recipes.Recipe> ResolveAsync(System.Data.Common.DbDataReader reader, System.Threading.CancellationToken token)
        {
            var id = await reader.GetFieldValueAsync<System.Guid>(0, token);

            Home.Recipes.Domain.Recipes.Recipe document;
            document = await _serializer.FromJsonAsync<Home.Recipes.Domain.Recipes.Recipe>(reader, 1, token).ConfigureAwait(false);
            var version = await reader.GetFieldValueAsync<int>(2, token);
            _session.MarkAsDocumentLoaded(id, document);
            return document;
        }

    }

    // END: LightweightRecipeSelector978002805
    
    
    // START: IdentityMapRecipeSelector978002805
    public class IdentityMapRecipeSelector978002805 : Marten.Internal.CodeGeneration.RevisionedDocumentSelectorWithIdentityMap<Home.Recipes.Domain.Recipes.Recipe, System.Guid>, Marten.Linq.Selectors.ISelector<Home.Recipes.Domain.Recipes.Recipe>
    {
        private readonly Marten.Internal.IMartenSession _session;
        private readonly Marten.Schema.DocumentMapping _mapping;

        public IdentityMapRecipeSelector978002805(Marten.Internal.IMartenSession session, Marten.Schema.DocumentMapping mapping) : base(session, mapping)
        {
            _session = session;
            _mapping = mapping;
        }



        public Home.Recipes.Domain.Recipes.Recipe Resolve(System.Data.Common.DbDataReader reader)
        {
            var id = reader.GetFieldValue<System.Guid>(0);
            if (_identityMap.TryGetValue(id, out var existing)) return existing;

            Home.Recipes.Domain.Recipes.Recipe document;
            document = _serializer.FromJson<Home.Recipes.Domain.Recipes.Recipe>(reader, 1);
            var version = reader.GetFieldValue<int>(2);
            _session.MarkAsDocumentLoaded(id, document);
            _identityMap[id] = document;
            return document;
        }


        public async System.Threading.Tasks.Task<Home.Recipes.Domain.Recipes.Recipe> ResolveAsync(System.Data.Common.DbDataReader reader, System.Threading.CancellationToken token)
        {
            var id = await reader.GetFieldValueAsync<System.Guid>(0, token);
            if (_identityMap.TryGetValue(id, out var existing)) return existing;

            Home.Recipes.Domain.Recipes.Recipe document;
            document = await _serializer.FromJsonAsync<Home.Recipes.Domain.Recipes.Recipe>(reader, 1, token).ConfigureAwait(false);
            var version = await reader.GetFieldValueAsync<int>(2, token);
            _session.MarkAsDocumentLoaded(id, document);
            _identityMap[id] = document;
            return document;
        }

    }

    // END: IdentityMapRecipeSelector978002805
    
    
    // START: DirtyTrackingRecipeSelector978002805
    public class DirtyTrackingRecipeSelector978002805 : Marten.Internal.CodeGeneration.RevisionedDocumentSelectorWithDirtyChecking<Home.Recipes.Domain.Recipes.Recipe, System.Guid>, Marten.Linq.Selectors.ISelector<Home.Recipes.Domain.Recipes.Recipe>
    {
        private readonly Marten.Internal.IMartenSession _session;
        private readonly Marten.Schema.DocumentMapping _mapping;

        public DirtyTrackingRecipeSelector978002805(Marten.Internal.IMartenSession session, Marten.Schema.DocumentMapping mapping) : base(session, mapping)
        {
            _session = session;
            _mapping = mapping;
        }



        public Home.Recipes.Domain.Recipes.Recipe Resolve(System.Data.Common.DbDataReader reader)
        {
            var id = reader.GetFieldValue<System.Guid>(0);
            if (_identityMap.TryGetValue(id, out var existing)) return existing;

            Home.Recipes.Domain.Recipes.Recipe document;
            document = _serializer.FromJson<Home.Recipes.Domain.Recipes.Recipe>(reader, 1);
            var version = reader.GetFieldValue<int>(2);
            _session.MarkAsDocumentLoaded(id, document);
            _identityMap[id] = document;
            StoreTracker(_session, document);
            return document;
        }


        public async System.Threading.Tasks.Task<Home.Recipes.Domain.Recipes.Recipe> ResolveAsync(System.Data.Common.DbDataReader reader, System.Threading.CancellationToken token)
        {
            var id = await reader.GetFieldValueAsync<System.Guid>(0, token);
            if (_identityMap.TryGetValue(id, out var existing)) return existing;

            Home.Recipes.Domain.Recipes.Recipe document;
            document = await _serializer.FromJsonAsync<Home.Recipes.Domain.Recipes.Recipe>(reader, 1, token).ConfigureAwait(false);
            var version = await reader.GetFieldValueAsync<int>(2, token);
            _session.MarkAsDocumentLoaded(id, document);
            _identityMap[id] = document;
            StoreTracker(_session, document);
            return document;
        }

    }

    // END: DirtyTrackingRecipeSelector978002805
    
    
    // START: OverwriteRecipeOperation978002805
    public class OverwriteRecipeOperation978002805 : Marten.Internal.Operations.StorageOperation<Home.Recipes.Domain.Recipes.Recipe, System.Guid>
    {
        private readonly Home.Recipes.Domain.Recipes.Recipe _document;
        private readonly System.Guid _id;
        private readonly System.Collections.Generic.Dictionary<System.Guid, System.Guid> _versions;
        private readonly Marten.Schema.DocumentMapping _mapping;

        public OverwriteRecipeOperation978002805(Home.Recipes.Domain.Recipes.Recipe document, System.Guid id, System.Collections.Generic.Dictionary<System.Guid, System.Guid> versions, Marten.Schema.DocumentMapping mapping) : base(document, id, versions, mapping)
        {
            _document = document;
            _id = id;
            _versions = versions;
            _mapping = mapping;
        }



        public override void Postprocess(System.Data.Common.DbDataReader reader, System.Collections.Generic.IList<System.Exception> exceptions)
        {
            if (postprocessRevision(reader, exceptions))
            {
            }

        }


        public override async System.Threading.Tasks.Task PostprocessAsync(System.Data.Common.DbDataReader reader, System.Collections.Generic.IList<System.Exception> exceptions, System.Threading.CancellationToken token)
        {
            if (await postprocessRevisionAsync(reader, exceptions, token))
            {
            }

        }


        public override Marten.Internal.Operations.OperationRole Role()
        {
            return Marten.Internal.Operations.OperationRole.Update;
        }


        public override NpgsqlTypes.NpgsqlDbType DbType()
        {
            return NpgsqlTypes.NpgsqlDbType.Uuid;
        }


        public override void ConfigureParameters(Weasel.Postgresql.IGroupedParameterBuilder parameterBuilder, Weasel.Postgresql.ICommandBuilder builder, Home.Recipes.Domain.Recipes.Recipe document, Marten.Internal.IMartenSession session)
        {
            builder.Append("select public.mt_overwrite_recipe(");
            var parameter0 = parameterBuilder.AppendParameter(session.Serializer.ToJson(_document));
            parameter0.NpgsqlDbType = NpgsqlTypes.NpgsqlDbType.Jsonb;
            // .Net Class Type
            var parameter1 = parameterBuilder.AppendParameter(_document.GetType().FullName);
            parameter1.NpgsqlDbType = NpgsqlTypes.NpgsqlDbType.Varchar;
            var parameter2 = parameterBuilder.AppendParameter(document.Id);
            setCurrentRevisionParameter(parameterBuilder);
            builder.Append(')');
        }

    }

    // END: OverwriteRecipeOperation978002805
    
    
    // START: QueryOnlyRecipeDocumentStorage978002805
    public class QueryOnlyRecipeDocumentStorage978002805 : Marten.Internal.Storage.QueryOnlyDocumentStorage<Home.Recipes.Domain.Recipes.Recipe, System.Guid>
    {
        private readonly Marten.Schema.DocumentMapping _document;

        public QueryOnlyRecipeDocumentStorage978002805(Marten.Schema.DocumentMapping document) : base(document)
        {
            _document = document;
        }



        public override System.Guid AssignIdentity(Home.Recipes.Domain.Recipes.Recipe document, string tenantId, Marten.Storage.IMartenDatabase database)
        {
            if (document.Id == Guid.Empty) _setter(document, Marten.Schema.Identity.CombGuidIdGeneration.NewGuid());
            return document.Id;
        }


        public override Marten.Internal.Operations.IStorageOperation Update(Home.Recipes.Domain.Recipes.Recipe document, Marten.Internal.IMartenSession session, string tenant)
        {
            if (session.Concurrency == Marten.Services.ConcurrencyChecks.Disabled)
            {

                return new Marten.Generated.DocumentStorage.OverwriteRecipeOperation978002805
                (
                    document, Identity(document),
                    null,
                    _document
                    
                );
            }

            else
            {

                return new Marten.Generated.DocumentStorage.UpdateRecipeOperation978002805
                (
                    document, Identity(document),
                    null,
                    _document
                    
                );
            }

        }


        public override Marten.Internal.Operations.IStorageOperation Insert(Home.Recipes.Domain.Recipes.Recipe document, Marten.Internal.IMartenSession session, string tenant)
        {

            return new Marten.Generated.DocumentStorage.InsertRecipeOperation978002805
            (
                document, Identity(document),
                null,
                _document
                
            );
        }


        public override Marten.Internal.Operations.IStorageOperation Upsert(Home.Recipes.Domain.Recipes.Recipe document, Marten.Internal.IMartenSession session, string tenant)
        {
            if (session.Concurrency == Marten.Services.ConcurrencyChecks.Disabled)
            {

                return new Marten.Generated.DocumentStorage.OverwriteRecipeOperation978002805
                (
                    document, Identity(document),
                    null,
                    _document
                    
                );
            }

            else
            {

                return new Marten.Generated.DocumentStorage.UpsertRecipeOperation978002805
                (
                    document, Identity(document),
                    null,
                    _document
                    
                );
            }

        }


        public override Marten.Internal.Operations.IStorageOperation Overwrite(Home.Recipes.Domain.Recipes.Recipe document, Marten.Internal.IMartenSession session, string tenant)
        {

            return new Marten.Generated.DocumentStorage.OverwriteRecipeOperation978002805
            (
                document, Identity(document),
                null,
                _document
                
            );

            return new Marten.Generated.DocumentStorage.OverwriteRecipeOperation978002805
            (
                document, Identity(document),
                null,
                _document
                
            );
        }


        public override System.Guid Identity(Home.Recipes.Domain.Recipes.Recipe document)
        {
            return document.Id;
        }


        public override Marten.Linq.Selectors.ISelector BuildSelector(Marten.Internal.IMartenSession session)
        {
            return new Marten.Generated.DocumentStorage.QueryOnlyRecipeSelector978002805(session, _document);
        }


        public override object RawIdentityValue(System.Guid id)
        {
            return id;
        }


        public override Npgsql.NpgsqlParameter BuildManyIdParameter(System.Guid[] ids)
        {
            return base.BuildManyIdParameter(ids);
        }

    }

    // END: QueryOnlyRecipeDocumentStorage978002805
    
    
    // START: LightweightRecipeDocumentStorage978002805
    public class LightweightRecipeDocumentStorage978002805 : Marten.Internal.Storage.LightweightDocumentStorage<Home.Recipes.Domain.Recipes.Recipe, System.Guid>
    {
        private readonly Marten.Schema.DocumentMapping _document;

        public LightweightRecipeDocumentStorage978002805(Marten.Schema.DocumentMapping document) : base(document)
        {
            _document = document;
        }



        public override System.Guid AssignIdentity(Home.Recipes.Domain.Recipes.Recipe document, string tenantId, Marten.Storage.IMartenDatabase database)
        {
            if (document.Id == Guid.Empty) _setter(document, Marten.Schema.Identity.CombGuidIdGeneration.NewGuid());
            return document.Id;
        }


        public override Marten.Internal.Operations.IStorageOperation Update(Home.Recipes.Domain.Recipes.Recipe document, Marten.Internal.IMartenSession session, string tenant)
        {
            if (session.Concurrency == Marten.Services.ConcurrencyChecks.Disabled)
            {

                return new Marten.Generated.DocumentStorage.OverwriteRecipeOperation978002805
                (
                    document, Identity(document),
                    null,
                    _document
                    
                );
            }

            else
            {

                return new Marten.Generated.DocumentStorage.UpdateRecipeOperation978002805
                (
                    document, Identity(document),
                    null,
                    _document
                    
                );
            }

        }


        public override Marten.Internal.Operations.IStorageOperation Insert(Home.Recipes.Domain.Recipes.Recipe document, Marten.Internal.IMartenSession session, string tenant)
        {

            return new Marten.Generated.DocumentStorage.InsertRecipeOperation978002805
            (
                document, Identity(document),
                null,
                _document
                
            );
        }


        public override Marten.Internal.Operations.IStorageOperation Upsert(Home.Recipes.Domain.Recipes.Recipe document, Marten.Internal.IMartenSession session, string tenant)
        {
            if (session.Concurrency == Marten.Services.ConcurrencyChecks.Disabled)
            {

                return new Marten.Generated.DocumentStorage.OverwriteRecipeOperation978002805
                (
                    document, Identity(document),
                    null,
                    _document
                    
                );
            }

            else
            {

                return new Marten.Generated.DocumentStorage.UpsertRecipeOperation978002805
                (
                    document, Identity(document),
                    null,
                    _document
                    
                );
            }

        }


        public override Marten.Internal.Operations.IStorageOperation Overwrite(Home.Recipes.Domain.Recipes.Recipe document, Marten.Internal.IMartenSession session, string tenant)
        {

            return new Marten.Generated.DocumentStorage.OverwriteRecipeOperation978002805
            (
                document, Identity(document),
                null,
                _document
                
            );

            return new Marten.Generated.DocumentStorage.OverwriteRecipeOperation978002805
            (
                document, Identity(document),
                null,
                _document
                
            );
        }


        public override System.Guid Identity(Home.Recipes.Domain.Recipes.Recipe document)
        {
            return document.Id;
        }


        public override Marten.Linq.Selectors.ISelector BuildSelector(Marten.Internal.IMartenSession session)
        {
            return new Marten.Generated.DocumentStorage.LightweightRecipeSelector978002805(session, _document);
        }


        public override object RawIdentityValue(System.Guid id)
        {
            return id;
        }


        public override Npgsql.NpgsqlParameter BuildManyIdParameter(System.Guid[] ids)
        {
            return base.BuildManyIdParameter(ids);
        }

    }

    // END: LightweightRecipeDocumentStorage978002805
    
    
    // START: IdentityMapRecipeDocumentStorage978002805
    public class IdentityMapRecipeDocumentStorage978002805 : Marten.Internal.Storage.IdentityMapDocumentStorage<Home.Recipes.Domain.Recipes.Recipe, System.Guid>
    {
        private readonly Marten.Schema.DocumentMapping _document;

        public IdentityMapRecipeDocumentStorage978002805(Marten.Schema.DocumentMapping document) : base(document)
        {
            _document = document;
        }



        public override System.Guid AssignIdentity(Home.Recipes.Domain.Recipes.Recipe document, string tenantId, Marten.Storage.IMartenDatabase database)
        {
            if (document.Id == Guid.Empty) _setter(document, Marten.Schema.Identity.CombGuidIdGeneration.NewGuid());
            return document.Id;
        }


        public override Marten.Internal.Operations.IStorageOperation Update(Home.Recipes.Domain.Recipes.Recipe document, Marten.Internal.IMartenSession session, string tenant)
        {
            if (session.Concurrency == Marten.Services.ConcurrencyChecks.Disabled)
            {

                return new Marten.Generated.DocumentStorage.OverwriteRecipeOperation978002805
                (
                    document, Identity(document),
                    null,
                    _document
                    
                );
            }

            else
            {

                return new Marten.Generated.DocumentStorage.UpdateRecipeOperation978002805
                (
                    document, Identity(document),
                    null,
                    _document
                    
                );
            }

        }


        public override Marten.Internal.Operations.IStorageOperation Insert(Home.Recipes.Domain.Recipes.Recipe document, Marten.Internal.IMartenSession session, string tenant)
        {

            return new Marten.Generated.DocumentStorage.InsertRecipeOperation978002805
            (
                document, Identity(document),
                null,
                _document
                
            );
        }


        public override Marten.Internal.Operations.IStorageOperation Upsert(Home.Recipes.Domain.Recipes.Recipe document, Marten.Internal.IMartenSession session, string tenant)
        {
            if (session.Concurrency == Marten.Services.ConcurrencyChecks.Disabled)
            {

                return new Marten.Generated.DocumentStorage.OverwriteRecipeOperation978002805
                (
                    document, Identity(document),
                    null,
                    _document
                    
                );
            }

            else
            {

                return new Marten.Generated.DocumentStorage.UpsertRecipeOperation978002805
                (
                    document, Identity(document),
                    null,
                    _document
                    
                );
            }

        }


        public override Marten.Internal.Operations.IStorageOperation Overwrite(Home.Recipes.Domain.Recipes.Recipe document, Marten.Internal.IMartenSession session, string tenant)
        {

            return new Marten.Generated.DocumentStorage.OverwriteRecipeOperation978002805
            (
                document, Identity(document),
                null,
                _document
                
            );

            return new Marten.Generated.DocumentStorage.OverwriteRecipeOperation978002805
            (
                document, Identity(document),
                null,
                _document
                
            );
        }


        public override System.Guid Identity(Home.Recipes.Domain.Recipes.Recipe document)
        {
            return document.Id;
        }


        public override Marten.Linq.Selectors.ISelector BuildSelector(Marten.Internal.IMartenSession session)
        {
            return new Marten.Generated.DocumentStorage.IdentityMapRecipeSelector978002805(session, _document);
        }


        public override object RawIdentityValue(System.Guid id)
        {
            return id;
        }


        public override Npgsql.NpgsqlParameter BuildManyIdParameter(System.Guid[] ids)
        {
            return base.BuildManyIdParameter(ids);
        }

    }

    // END: IdentityMapRecipeDocumentStorage978002805
    
    
    // START: DirtyTrackingRecipeDocumentStorage978002805
    public class DirtyTrackingRecipeDocumentStorage978002805 : Marten.Internal.Storage.DirtyCheckedDocumentStorage<Home.Recipes.Domain.Recipes.Recipe, System.Guid>
    {
        private readonly Marten.Schema.DocumentMapping _document;

        public DirtyTrackingRecipeDocumentStorage978002805(Marten.Schema.DocumentMapping document) : base(document)
        {
            _document = document;
        }



        public override System.Guid AssignIdentity(Home.Recipes.Domain.Recipes.Recipe document, string tenantId, Marten.Storage.IMartenDatabase database)
        {
            if (document.Id == Guid.Empty) _setter(document, Marten.Schema.Identity.CombGuidIdGeneration.NewGuid());
            return document.Id;
        }


        public override Marten.Internal.Operations.IStorageOperation Update(Home.Recipes.Domain.Recipes.Recipe document, Marten.Internal.IMartenSession session, string tenant)
        {
            if (session.Concurrency == Marten.Services.ConcurrencyChecks.Disabled)
            {

                return new Marten.Generated.DocumentStorage.OverwriteRecipeOperation978002805
                (
                    document, Identity(document),
                    null,
                    _document
                    
                );
            }

            else
            {

                return new Marten.Generated.DocumentStorage.UpdateRecipeOperation978002805
                (
                    document, Identity(document),
                    null,
                    _document
                    
                );
            }

        }


        public override Marten.Internal.Operations.IStorageOperation Insert(Home.Recipes.Domain.Recipes.Recipe document, Marten.Internal.IMartenSession session, string tenant)
        {

            return new Marten.Generated.DocumentStorage.InsertRecipeOperation978002805
            (
                document, Identity(document),
                null,
                _document
                
            );
        }


        public override Marten.Internal.Operations.IStorageOperation Upsert(Home.Recipes.Domain.Recipes.Recipe document, Marten.Internal.IMartenSession session, string tenant)
        {
            if (session.Concurrency == Marten.Services.ConcurrencyChecks.Disabled)
            {

                return new Marten.Generated.DocumentStorage.OverwriteRecipeOperation978002805
                (
                    document, Identity(document),
                    null,
                    _document
                    
                );
            }

            else
            {

                return new Marten.Generated.DocumentStorage.UpsertRecipeOperation978002805
                (
                    document, Identity(document),
                    null,
                    _document
                    
                );
            }

        }


        public override Marten.Internal.Operations.IStorageOperation Overwrite(Home.Recipes.Domain.Recipes.Recipe document, Marten.Internal.IMartenSession session, string tenant)
        {

            return new Marten.Generated.DocumentStorage.OverwriteRecipeOperation978002805
            (
                document, Identity(document),
                null,
                _document
                
            );

            return new Marten.Generated.DocumentStorage.OverwriteRecipeOperation978002805
            (
                document, Identity(document),
                null,
                _document
                
            );
        }


        public override System.Guid Identity(Home.Recipes.Domain.Recipes.Recipe document)
        {
            return document.Id;
        }


        public override Marten.Linq.Selectors.ISelector BuildSelector(Marten.Internal.IMartenSession session)
        {
            return new Marten.Generated.DocumentStorage.DirtyTrackingRecipeSelector978002805(session, _document);
        }


        public override object RawIdentityValue(System.Guid id)
        {
            return id;
        }


        public override Npgsql.NpgsqlParameter BuildManyIdParameter(System.Guid[] ids)
        {
            return base.BuildManyIdParameter(ids);
        }

    }

    // END: DirtyTrackingRecipeDocumentStorage978002805
    
    
    // START: RecipeBulkLoader978002805
    public class RecipeBulkLoader978002805 : Marten.Internal.CodeGeneration.BulkLoader<Home.Recipes.Domain.Recipes.Recipe, System.Guid>
    {
        private readonly Marten.Internal.Storage.IDocumentStorage<Home.Recipes.Domain.Recipes.Recipe, System.Guid> _storage;

        public RecipeBulkLoader978002805(Marten.Internal.Storage.IDocumentStorage<Home.Recipes.Domain.Recipes.Recipe, System.Guid> storage) : base(storage)
        {
            _storage = storage;
        }


        public const string MAIN_LOADER_SQL = "COPY public.mt_doc_recipe(\"mt_dotnet_type\", \"id\", \"mt_version\", \"data\") FROM STDIN BINARY";

        public const string TEMP_LOADER_SQL = "COPY mt_doc_recipe_temp(\"mt_dotnet_type\", \"id\", \"mt_version\", \"data\") FROM STDIN BINARY";

        public const string COPY_NEW_DOCUMENTS_SQL = "insert into public.mt_doc_recipe (\"id\", \"data\", \"mt_dotnet_type\", \"mt_version\", mt_last_modified) (select mt_doc_recipe_temp.\"id\", mt_doc_recipe_temp.\"data\", mt_doc_recipe_temp.\"mt_dotnet_type\", mt_doc_recipe_temp.\"mt_version\", transaction_timestamp() from mt_doc_recipe_temp left join public.mt_doc_recipe on mt_doc_recipe_temp.id = public.mt_doc_recipe.id where public.mt_doc_recipe.id is null)";

        public const string OVERWRITE_SQL = "update public.mt_doc_recipe target SET data = source.data, mt_dotnet_type = source.mt_dotnet_type, mt_version = source.mt_version, mt_last_modified = transaction_timestamp() FROM mt_doc_recipe_temp source WHERE source.id = target.id";

        public const string CREATE_TEMP_TABLE_FOR_COPYING_SQL = "create temporary table mt_doc_recipe_temp (like public.mt_doc_recipe including defaults)";


        public override string CreateTempTableForCopying()
        {
            return CREATE_TEMP_TABLE_FOR_COPYING_SQL;
        }


        public override string CopyNewDocumentsFromTempTable()
        {
            return COPY_NEW_DOCUMENTS_SQL;
        }


        public override string OverwriteDuplicatesFromTempTable()
        {
            return OVERWRITE_SQL;
        }


        public override void LoadRow(Npgsql.NpgsqlBinaryImporter writer, Home.Recipes.Domain.Recipes.Recipe document, Marten.Storage.Tenant tenant, Marten.ISerializer serializer)
        {
            writer.Write(document.GetType().FullName, NpgsqlTypes.NpgsqlDbType.Varchar);
            writer.Write(document.Id, NpgsqlTypes.NpgsqlDbType.Uuid);
            writer.Write(1, NpgsqlTypes.NpgsqlDbType.Integer);
            writer.Write(serializer.ToJson(document), NpgsqlTypes.NpgsqlDbType.Jsonb);
        }


        public override async System.Threading.Tasks.Task LoadRowAsync(Npgsql.NpgsqlBinaryImporter writer, Home.Recipes.Domain.Recipes.Recipe document, Marten.Storage.Tenant tenant, Marten.ISerializer serializer, System.Threading.CancellationToken cancellation)
        {
            await writer.WriteAsync(document.GetType().FullName, NpgsqlTypes.NpgsqlDbType.Varchar, cancellation);
            await writer.WriteAsync(document.Id, NpgsqlTypes.NpgsqlDbType.Uuid, cancellation);
            await writer.WriteAsync(1, NpgsqlTypes.NpgsqlDbType.Integer, cancellation);
            await writer.WriteAsync(serializer.ToJson(document), NpgsqlTypes.NpgsqlDbType.Jsonb, cancellation);
        }


        public override string MainLoaderSql()
        {
            return MAIN_LOADER_SQL;
        }


        public override string TempLoaderSql()
        {
            return TEMP_LOADER_SQL;
        }

    }

    // END: RecipeBulkLoader978002805
    
    
    // START: RecipeProvider978002805
    public class RecipeProvider978002805 : Marten.Internal.Storage.DocumentProvider<Home.Recipes.Domain.Recipes.Recipe>
    {
        private readonly Marten.Schema.DocumentMapping _mapping;

        public RecipeProvider978002805(Marten.Schema.DocumentMapping mapping) : base(new RecipeBulkLoader978002805(new QueryOnlyRecipeDocumentStorage978002805(mapping)), new QueryOnlyRecipeDocumentStorage978002805(mapping), new LightweightRecipeDocumentStorage978002805(mapping), new IdentityMapRecipeDocumentStorage978002805(mapping), new DirtyTrackingRecipeDocumentStorage978002805(mapping))
        {
            _mapping = mapping;
        }


    }

    // END: RecipeProvider978002805
    
    
}
