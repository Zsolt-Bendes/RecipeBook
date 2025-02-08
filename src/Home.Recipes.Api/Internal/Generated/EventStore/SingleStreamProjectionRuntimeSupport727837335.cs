// <auto-generated/>
#pragma warning disable
using Marten;
using Marten.Events.Aggregation;
using Marten.Internal.Storage;
using System;
using System.Linq;

namespace Marten.Generated.EventStore
{
    // START: SingleStreamProjectionLiveAggregation727837335
    public class SingleStreamProjectionLiveAggregation727837335 : Marten.Events.Aggregation.SyncLiveAggregatorBase<Home.Recipes.Domain.RecipeHistory.RecipeHistory>
    {
        private readonly Marten.Events.Aggregation.SingleStreamProjection<Home.Recipes.Domain.RecipeHistory.RecipeHistory> _singleStreamProjection;

        public SingleStreamProjectionLiveAggregation727837335(Marten.Events.Aggregation.SingleStreamProjection<Home.Recipes.Domain.RecipeHistory.RecipeHistory> singleStreamProjection)
        {
            _singleStreamProjection = singleStreamProjection;
        }



        public override Home.Recipes.Domain.RecipeHistory.RecipeHistory Build(System.Collections.Generic.IReadOnlyList<Marten.Events.IEvent> events, Marten.IQuerySession session, Home.Recipes.Domain.RecipeHistory.RecipeHistory snapshot)
        {
            if (!events.Any()) return snapshot;
            var usedEventOnCreate = snapshot is null;
            snapshot ??= Create(events[0], session);;
            if (snapshot is null)
            {
                usedEventOnCreate = false;
                snapshot = CreateDefault(events[0]);
                if (snapshot != null) _singleStreamProjection.ApplyMetadata(snapshot, events[0]);
            }

            foreach (var @event in events.Skip(usedEventOnCreate ? 1 : 0))
            {
                snapshot = Apply(@event, snapshot, session);
                if (snapshot != null) _singleStreamProjection.ApplyMetadata(snapshot, @event);
            }

            return snapshot;
        }


        public Home.Recipes.Domain.RecipeHistory.RecipeHistory Create(Marten.Events.IEvent @event, Marten.IQuerySession session)
        {
            switch (@event)
            {
                case Marten.Events.IEvent<Home.Recipes.Domain.RecipeHistory.Events.RecipeCooked> event_RecipeCooked31:
                    return Home.Recipes.Domain.RecipeHistory.RecipeHistory.Create(event_RecipeCooked31.Data);
                    break;
            }

            return null;
        }


        public Home.Recipes.Domain.RecipeHistory.RecipeHistory Apply(Marten.Events.IEvent @event, Home.Recipes.Domain.RecipeHistory.RecipeHistory aggregate, Marten.IQuerySession session)
        {
            return aggregate;
        }

    }

    // END: SingleStreamProjectionLiveAggregation727837335
    
    
    // START: SingleStreamProjectionInlineHandler727837335
    public class SingleStreamProjectionInlineHandler727837335 : Marten.Events.Aggregation.AggregationRuntime<Home.Recipes.Domain.RecipeHistory.RecipeHistory, System.Guid>
    {
        private readonly Marten.IDocumentStore _store;
        private readonly Marten.Events.Aggregation.IAggregateProjection _projection;
        private readonly Marten.Events.Aggregation.IEventSlicer<Home.Recipes.Domain.RecipeHistory.RecipeHistory, System.Guid> _slicer;
        private readonly Marten.Internal.Storage.IDocumentStorage<Home.Recipes.Domain.RecipeHistory.RecipeHistory, System.Guid> _storage;
        private readonly Marten.Events.Aggregation.SingleStreamProjection<Home.Recipes.Domain.RecipeHistory.RecipeHistory> _singleStreamProjection;

        public SingleStreamProjectionInlineHandler727837335(Marten.IDocumentStore store, Marten.Events.Aggregation.IAggregateProjection projection, Marten.Events.Aggregation.IEventSlicer<Home.Recipes.Domain.RecipeHistory.RecipeHistory, System.Guid> slicer, Marten.Internal.Storage.IDocumentStorage<Home.Recipes.Domain.RecipeHistory.RecipeHistory, System.Guid> storage, Marten.Events.Aggregation.SingleStreamProjection<Home.Recipes.Domain.RecipeHistory.RecipeHistory> singleStreamProjection) : base(store, projection, slicer, storage)
        {
            _store = store;
            _projection = projection;
            _slicer = slicer;
            _storage = storage;
            _singleStreamProjection = singleStreamProjection;
        }



        public override async System.Threading.Tasks.ValueTask<Home.Recipes.Domain.RecipeHistory.RecipeHistory> ApplyEvent(Marten.IQuerySession session, Marten.Events.Projections.EventSlice<Home.Recipes.Domain.RecipeHistory.RecipeHistory, System.Guid> slice, Marten.Events.IEvent evt, Home.Recipes.Domain.RecipeHistory.RecipeHistory aggregate, System.Threading.CancellationToken cancellationToken)
        {
            switch (evt)
            {
                case Marten.Events.IEvent<Home.Recipes.Domain.RecipeHistory.Events.RecipeCooked> event_RecipeCooked33:
                    aggregate = Home.Recipes.Domain.RecipeHistory.RecipeHistory.Create(event_RecipeCooked33.Data);
                    return aggregate;
            }

            return aggregate;
        }


        public Home.Recipes.Domain.RecipeHistory.RecipeHistory Create(Marten.Events.IEvent @event, Marten.IQuerySession session)
        {
            switch (@event)
            {
                case Marten.Events.IEvent<Home.Recipes.Domain.RecipeHistory.Events.RecipeCooked> event_RecipeCooked32:
                    return Home.Recipes.Domain.RecipeHistory.RecipeHistory.Create(event_RecipeCooked32.Data);
                    break;
            }

            return null;
        }

    }

    // END: SingleStreamProjectionInlineHandler727837335
    
    
}

